'***********************************************************************
'  EventQueue.vb - DatAWare Event Queue
'  Copyright � 2004 - TVA, all rights reserved
'
'  Build Environment: VB.NET, Visual Studio 2003
'  Primary Developer: James R Carroll, System Analyst [WESTAFF]
'      Office: COO - TRNS/PWR ELEC SYS O, CHATTANOOGA, TN - MR 2W-C
'       Phone: 423/751-2827
'       Email: jrcarrol@tva.gov
'
'  Code Modification History:
'  ---------------------------------------------------------------------
'  11/5/2004 - James R Carroll
'       Initial version of source generated for new Windows service
'       project "DatAWare PDC".
'
'***********************************************************************

Imports System.Text
Imports System.Threading
Imports TVA.Config.Common
Imports TVA.Shared.DateTime
Imports TVA.Services
Imports TVA.EE.Phasor.PDCstream

Public Class EventQueue

    Implements IServiceComponent
    Implements IDisposable

    Private m_parent As DatAWarePDC
    Private m_configFile As ConfigFile
    Private WithEvents m_dataQueue As DataQueue
    Private m_serverPoints As Hashtable
    Private m_enabled As Boolean
    Private m_packetsReceived As Long
    Private m_processedEvents As Long
    Private m_activeThreads As Integer

#Region " Setup and Class Definition Code "

    ' Class auto-generated using TVA service template at Fri Nov 5 09:43:23 EST 2004
    Public Sub New(ByVal parent As DatAWarePDC)

        m_parent = parent
        m_configFile = m_parent.Concentrator.ConfigFile
        m_dataQueue = m_parent.Concentrator.DataQueue
        m_serverPoints = New Hashtable
        m_enabled = True

    End Sub

    Protected Overrides Sub Finalize()

        MyBase.Finalize()
        Dispose()

    End Sub

    Public Overridable Sub Dispose() Implements IServiceComponent.Dispose, IDisposable.Dispose

        GC.SuppressFinalize(Me)

        ' Any needed shutdown code for your primary service process should be added here - note that this class
        ' instance is available for the duration of the service lifetime...

    End Sub

    ' This function handles updating status for the primary service process
    Public Sub UpdateStatus(ByVal Status As String, Optional ByVal LogStatusToEventLog As Boolean = False, Optional ByVal EntryType As System.Diagnostics.EventLogEntryType = EventLogEntryType.Information)

        m_parent.UpdateStatus(Status, LogStatusToEventLog, EntryType)

    End Sub

    Public Property Enabled() As Boolean
        Get
            Return m_enabled
        End Get
        Set(ByVal Value As Boolean)
            m_enabled = Value
        End Set
    End Property

#End Region

#Region " Event Queue Implementation "

    ' After a listener has been created, we will have a connection to the source DatAWare server - we use this
    ' connection to create a cross-reference list between DatAWare points and PMU data...
    ' Server point list should be reloaded any time new points are added...
    Public Sub RefreshPointList(ByVal listener As PDCListener)

        With listener
            ' We'll try three times to connect to DatAWare and refresh points before giving up...
            For x As Integer = 1 To 3
                ' Load all the point definitions from the associated DatAWare server (this takes a second)
                Try
                    Dim serverPoints As New PMUServerPoints(m_parent.Concentrator, .Connection, .UserName, .Password)

                    SyncLock m_serverPoints.SyncRoot
                        If m_serverPoints(.Connection.PlantCode) Is Nothing Then
                            m_serverPoints.Add(.Connection.PlantCode, serverPoints)
                        Else
                            m_serverPoints(.Connection.PlantCode) = serverPoints
                        End If
                    End SyncLock

                    ' If load was successful, we'll exit retry loop
                    Exit For
                Catch ex As Exception
                    If x = 3 Then
                        ' Log this exception to the event log, the local status log and any remote clients
                        UpdateStatus("Failed to reload point list from DatAWare server """ & .Connection.Server & " (" & _
                            .Connection.PlantCode & ")"" due to exception: " & ex.Message, True, EventLogEntryType.Error)
                        Throw ex
                    End If
                End Try
            Next
        End With

    End Sub

    Public ReadOnly Property Points(ByVal plantCode As String) As PMUServerPoints
        Get
            SyncLock m_serverPoints.SyncRoot
                Return DirectCast(m_serverPoints(plantCode), PMUServerPoints)
            End SyncLock
        End Get
    End Property

    Public ReadOnly Property Parent() As DatAWarePDC
        Get
            Return m_parent
        End Get
    End Property

    Public Sub QueueEventData(ByVal plantCode As String, ByVal eventBuffer As Byte(), ByVal offset As Integer, ByVal length As Integer)

        ' Note: although you could go ahead and sort items here directly without queuing this work up, doing this could
        ' cause the TCP stream from DatAWare to start to get backed-up causing inherent delays in real-time data
        If Enabled Then ThreadPool.QueueUserWorkItem(AddressOf ProcessEventBuffer, New DataPacket(plantCode, eventBuffer, offset, length))
        m_packetsReceived += 1

    End Sub

    ' We use this class to hold queued event data for worker thread
    Private Class DataPacket

        Public PlantCode As String
        Public EventBuffer As Byte()
        Public Offset As Integer
        Public Length As Integer

        Public Sub New(ByVal plantCode As String, ByVal eventBuffer As Byte(), ByVal offset As Integer, ByVal length As Integer)

            Me.PlantCode = plantCode
            Me.EventBuffer = eventBuffer
            Me.Offset = offset
            Me.Length = length

        End Sub

    End Class

    ' This method is expected to be run on an independent thread...
    Private Sub ProcessEventBuffer(ByVal stateInfo As Object)

        Try
            Interlocked.Increment(m_activeThreads)

            Dim eventData As DataPacket = DirectCast(stateInfo, DataPacket)
            Dim dataPoint As PMUDataPoint

            ' Parse events out of data packet and create a new PMU data point from timestamp and value
            For packetIndex As Integer = eventData.Offset To eventData.Length - 1 Step DatAWare.StandardEvent.BinaryLength
                If packetIndex + DatAWare.StandardEvent.BinaryLength < eventData.EventBuffer.Length Then
                    With New DatAWare.StandardEvent(eventData.EventBuffer, packetIndex)
                        ' Make sure we have a point defined for this value
                        If Points(eventData.PlantCode).GetPoint(.DatabaseIndex, dataPoint) Then
                            dataPoint.Timestamp = .TTag.ToDateTime
                            dataPoint.Value = .Value

                            ' If the new value is received on change, we'll update the samples in our current
                            ' data queue to use latest value...
                            If dataPoint.ReceivedOnChange Then
                                Points(eventData.PlantCode)(.DatabaseIndex) = dataPoint
                                UpdateReceivedOnChangePoints(dataPoint)
                            End If

                            ' Add this point value to the PDC concentrator data queue
                            m_dataQueue.SortDataPoint(dataPoint)
                        End If
                    End With

                    m_processedEvents += 1
                End If
            Next
        Catch ex As Exception
            UpdateStatus("Exception in DatAWare.EventQueue.ProcessEventBuffer worker thread: " & ex.Message)
            Throw ex
        Finally
            Interlocked.Decrement(m_activeThreads)
        End Try

    End Sub

    Private Sub m_dataQueue_DataSortingError(ByVal message As String) Handles m_dataQueue.DataSortingError

        ' When errors happen with data being processed at 30 samples per second - you can get a hefty volume of errors very quickly,
        ' so to keep from flooding the message queue - we'll only send a handful of messages every couple of seconds
        Static lastMessageTicks As Long
        Static sentMessageCount As Long
        Const messageTimespan As Integer = 2    ' Timespan, in seconds, over which to monitor message volume
        Const maximumMessages As Integer = 6    ' Maximum number of messages to be tolerated during timespan
        Dim sendMessage As Boolean

        If (DateTime.Now.Ticks - lastMessageTicks) / 10000000L < messageTimespan Then
            sendMessage = (sentMessageCount < maximumMessages)
            sentMessageCount += 1
        Else
            If sentMessageCount > maximumMessages Then m_parent.UpdateStatus("WARNING: " & (sentMessageCount - maximumMessages) & " error messages discarded to avoid flooding message queue...")
            sendMessage = True
            sentMessageCount = 0
            lastMessageTicks = DateTime.Now.Ticks
        End If

        If sendMessage Then m_parent.UpdateStatus(message)

    End Sub

    ' Prepopulate all received on changed points when new samples are created
    Private Sub m_dataQueue_NewDataSampleCreated(ByVal newDataSample As DataSample) Handles m_dataQueue.NewDataSampleCreated

        Dim timestamp As DateTime

        For x As Integer = 0 To newDataSample.Rows.Length - 1
            timestamp = newDataSample.Rows(x).TimeStamp

            For Each pointSet As PMUServerPoints In m_serverPoints.Values
                For Each dataPoint As PMUDataPoint In pointSet
                    If dataPoint.ReceivedOnChange Then
                        dataPoint.Timestamp = timestamp
                        m_dataQueue.SortDataPoint(dataPoint)
                    End If
                Next
            Next
        Next

    End Sub

    ' When we receive a new point marked as "received on change", we update all the values from this point in time on...
    Private Sub UpdateReceivedOnChangePoints(ByVal dataPoint As PMUDataPoint)

        Dim stepInterval As Decimal = 1000@ / m_configFile.SampleRate
        Dim timestamp As DateTime = dataPoint.Timestamp
        Dim baseTime As DateTime = BaselinedTimestamp(timestamp)
        Dim sampleIndex As Integer = m_dataQueue.GetSampleIndex(baseTime)

        If sampleIndex > -1 Then
            ' Update all values in current sample starting with current time interval
            For offset As Double = (Math.Floor(timestamp.Millisecond / stepInterval) + 0.5@) * stepInterval To 999 Step stepInterval
                dataPoint.Timestamp = baseTime.AddMilliseconds(offset)
                m_dataQueue.SortDataPoint(dataPoint)
            Next

            ' Update the point values in next sample
            If sampleIndex + 1 <= m_dataQueue.SampleCount - 1 Then
                With m_dataQueue.Sample(sampleIndex + 1)
                    For y As Integer = 0 To .Rows.Length - 1
                        dataPoint.Timestamp = .Rows(y).TimeStamp
                        m_dataQueue.SortDataPoint(dataPoint)
                    Next
                End With
            End If
        End If

    End Sub

#End Region

#Region " IService Component Implementation "

    ' Service component implementation
    Public ReadOnly Property Name() As String Implements IServiceComponent.Name
        Get
            Return Me.GetType.Name
        End Get
    End Property

    Public Sub ProcessStateChanged(ByVal NewState As ProcessState) Implements IServiceComponent.ProcessStateChanged

        ' This class executes as a result of a change in process state, so nothing to do...

    End Sub

    Public Sub ServiceStateChanged(ByVal NewState As ServiceState) Implements IServiceComponent.ServiceStateChanged

        ' We respect changes in service state by enabling or disabling our processing state as needed...
        Select Case NewState
            Case ServiceState.Paused
                Enabled = False
            Case ServiceState.Resumed
                Enabled = True
        End Select

    End Sub

    Public ReadOnly Property Status() As String Implements IServiceComponent.Status
        Get
            With New StringBuilder
                .Append("  Queue is currently: " & IIf(Enabled, "Enabled", "Disabled") & vbCrLf)
                .Append("    Packets received: " & m_packetsReceived & vbCrLf)
                .Append("    Processed events: " & m_processedEvents & vbCrLf)
                .Append("      Active threads: " & m_activeThreads & vbCrLf & vbCrLf)
                .Append("  Referencing points on the following DatAWare servers: " & vbCrLf & vbCrLf)

                For Each de As DictionaryEntry In m_serverPoints
                    .Append("        Plant Code " & de.Key & ": " & DirectCast(de.Value, PMUServerPoints).Count & " points")
                Next

                Return .ToString()
            End With
        End Get
    End Property

#End Region

End Class