' 04-24-06

Imports System.Data.SqlClient
Imports System.ComponentModel
Imports Tva.Data.Common
Imports Tva.Configuration.Common

Namespace Ssam

    Public Class SsamApi

        Private m_server As SsamServer
        Private m_keepConnectionOpen As Boolean
        Private m_connection As SqlConnection
        Private m_connectionState As SsamConnectionState

        Public Event InitializationException(ByVal ex As Exception)

        Public Enum SsamServer As Integer
            Development
            <EditorBrowsable(EditorBrowsableState.Never), Browsable(False)> _
            Acceptance
            Production
        End Enum

        Public Enum SsamConnectionState As Integer
            Open
            OpenAndActive
            OpenAndInactive
            Closed
        End Enum

        Public Sub New()
            MyClass.New(SsamServer.Development)
        End Sub

        Public Sub New(ByVal server As SsamServer)
            MyClass.New(server, True)
        End Sub

        Public Sub New(ByVal server As SsamServer, ByVal keepConnectionOpen As Boolean)
            MyBase.New()
            m_connection = New SqlConnection()
            m_connectionState = SsamConnectionState.Closed
            MyClass.Server = server
            MyClass.KeepConnectionOpen = keepConnectionOpen
            Initialize()
        End Sub

        Public Property Server() As SsamServer
            Get
                Return m_server
            End Get
            Set(ByVal value As SsamServer)
                ' If connection to a SSAM server is open, disconnect from it and connect to the selected server.
                If m_connectionState <> SsamConnectionState.Closed Then
                    Disconnect()
                    Connect()
                End If
                m_server = value
            End Set
        End Property

        Public Property KeepConnectionOpen() As Boolean
            Get
                Return m_keepConnectionOpen
            End Get
            Set(ByVal value As Boolean)
                ' We will only close the connection with SSAM if it is already open, but is not to be
                ' kept open after logging an event. However, if the connection is to be kept open after
                ' logging an event, but is closed at present, we will only open it when the first event
                ' is logged and keep if open until it is explicitly closed.
                If value = False AndAlso m_connectionState <> SsamConnectionState.Closed Then
                    Disconnect()
                End If
                m_keepConnectionOpen = value
            End Set
        End Property

        Public ReadOnly Property ConnectionState() As SsamConnectionState
            Get
                Return m_connectionState
            End Get
        End Property

        Public ReadOnly Property ConnectionString() As String
            Get
                ' Return the connection string for connecting to the selected SSAM server from the config
                ' of the application that is using the API.
                Dim server As String = ""
                Select Case m_server
                    Case SsamServer.Development
                        server = "Development"
                    Case SsamServer.Production
                        server = "Production"
                End Select
                Return CategorizedSettings("ssam")(server).Value()
            End Get
        End Property

        Public Sub Connect()

            If m_connection.State <> System.Data.ConnectionState.Closed Then Disconnect()
            m_connection.ConnectionString = MyClass.ConnectionString()
            m_connection.Open()
            m_connectionState = SsamConnectionState.OpenAndInactive

        End Sub

        Public Sub Disconnect()

            If m_connection.State <> System.Data.ConnectionState.Closed Then m_connection.Close()
            m_connectionState = SsamConnectionState.Closed

        End Sub

        Public Function LogEvent(ByVal newEvent As SsamEvent) As Boolean

            Dim logResult As Boolean = False
            Try
                If m_connectionState = SsamConnectionState.Closed Then
                    Connect()
                End If
                m_connectionState = SsamConnectionState.OpenAndActive

                ' Log the event to SSAM.
                ExecuteScalar("sp_LogSsamEvent", m_connection, _
                    New Object() {newEvent.EventType(), newEvent.EntityType(), newEvent.EntityId(), newEvent.ErrorNumber(), newEvent.Message(), newEvent.Description()})
                logResult = True
            Catch ex As Exception
                logResult = False
                Throw
            Finally
                m_connectionState = SsamConnectionState.OpenAndInactive
                If m_keepConnectionOpen = False Then
                    ' Connection with SSAM is not to be kept open after logging an event, so close it.
                    Disconnect()
                End If
            End Try
            Return logResult

        End Function

        Private Sub Initialize()

            Try
                ' Make sure all of the SSAM connection strings are present in the config file of the 
                ' application using the API.
                CategorizedSettings("ssam").Add("Development", "Server=RGOCSQLD;Database=Ssam;Trusted_Connection=True;", _
                    "Connection string for connecting to development SSAM server.", True)
                'CategorizedSettings("ssam").Add("Acceptance", "N/A", _
                '    "Connection string for connecting to acceptance SSAM server.", True)
                CategorizedSettings("ssam").Add("Production", "Server=OPSSAMSQL;Database=Ssam;Trusted_Connection=True;", _
                    "Connection string for connecting to production SSAM server.", True)
                SaveSettings()
            Catch ex As Exception
                RaiseEvent InitializationException(ex)
            End Try

        End Sub

    End Class

End Namespace