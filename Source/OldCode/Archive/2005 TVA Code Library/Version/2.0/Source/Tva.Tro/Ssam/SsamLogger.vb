' 04-04-06

Imports System.Drawing
Imports System.ComponentModel
Imports Tva.Collections

Namespace Ssam

    <ToolboxBitmap(GetType(SsamLogger))> _
    Public Class SsamLogger
        Implements ISupportInitialize

        Private m_apiInstance As SsamApi
        Private WithEvents m_eventQueue As ProcessQueue(Of SsamEvent)

        Public Event LogException(ByVal ex As Exception)

        <Category("Configuration"), Description("The SSAM server to which event are to be logged."), DefaultValue(GetType(SsamApi.SsamServer), "Development")> _
        Public Property Server() As SsamApi.SsamServer
            Get
                Return m_apiInstance.Server()
            End Get
            Set(ByVal value As SsamApi.SsamServer)
                m_apiInstance.Server = value
            End Set
        End Property

        <Category("Configuration"), Description("Determines whether the connection with SSAM server is to be kept open after logging an event."), DefaultValue(GetType(Boolean), "True")> _
        Public Property KeepConnectionOpen() As Boolean
            Get
                Return m_apiInstance.KeepConnectionOpen()
            End Get
            Set(ByVal value As Boolean)
                m_apiInstance.KeepConnectionOpen = value
            End Set
        End Property

        <Browsable(False)> _
        Public ReadOnly Property ApiInstance() As SsamApi
            Get
                Return m_apiInstance
            End Get
        End Property

        <Browsable(False)> _
        Public ReadOnly Property EventQueue() As ProcessQueue(Of SsamEvent)
            Get
                Return m_eventQueue
            End Get
        End Property

        Public Sub LogEvent(ByVal entityID As String, ByVal entityType As SsamEvent.SsamEntityType, _
                ByVal eventType As SsamEvent.SsamEventType)

            LogEvent(entityID, entityType, eventType, "", "", "")

        End Sub

        Public Sub LogEvent(ByVal entityID As String, ByVal entityType As SsamEvent.SsamEntityType, _
                ByVal eventType As SsamEvent.SsamEventType, ByVal errorNumber As String, ByVal message As String, _
                ByVal description As String)

            LogEvent(New SsamEvent(entityID, entityType, eventType, errorNumber, message, description))

        End Sub

        Public Sub LogEvent(ByVal newEvent As SsamEvent)

            m_eventQueue.Add(newEvent)
            If Not m_eventQueue.Enabled() Then m_eventQueue.Start()

        End Sub

        Private Sub ProcessEvent(ByVal item As SsamEvent)

            m_apiInstance.LogEvent(item)

        End Sub

        Private Sub m_eventQueue_ProcessException(ByVal ex As Exception) Handles m_eventQueue.ProcessException

            RaiseEvent LogException(ex)

        End Sub

#Region " ISupportInitialize Implementation "

        Public Sub BeginInit() Implements System.ComponentModel.ISupportInitialize.BeginInit

            ' There is nothing we need to do before the component is initialized.

        End Sub

        Public Sub EndInit() Implements System.ComponentModel.ISupportInitialize.EndInit

            If Not DesignMode() Then m_apiInstance.Initialize()

        End Sub

#End Region

    End Class

End Namespace
