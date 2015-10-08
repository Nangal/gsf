'*******************************************************************************************************
'  DataPacket.vb - IEEE C37.118 data packet
'  Copyright � 2005 - TVA, all rights reserved - Gbtc
'
'  Build Environment: VB.NET, Visual Studio 2003
'  Primary Developer: James R Carroll, System Analyst [TVA]
'      Office: COO - TRNS/PWR ELEC SYS O, CHATTANOOGA, TN - MR 2W-C
'       Phone: 423/751-2827
'       Email: jrcarrol@tva.gov
'
'  Code Modification History:
'  -----------------------------------------------------------------------------------------------------
'  11/12/2004 - James R Carroll
'       Initial version of source generated
'
'*******************************************************************************************************

Imports System.Buffer
Imports TVA.Interop
Imports TVA.Shared.Math
Imports TVA.EE.Phasor.IEEEC37_118.Common

Namespace EE.Phasor.IEEEC37_118

    ' This is essentially a "row" of PMU data at a given timestamp
    Public Class DataFrame

        Inherits DataFrameBase

        Public FractionSec As Int32

        Public Sub New()

            MyBase.New(New DataCellCollection)

        End Sub

        Public Sub New(ByVal parsedFrameHeader As FrameHeader, ByVal configurationFrame As IConfigurationFrame, ByVal binaryImage As Byte(), ByVal startIndex As Integer)

            MyBase.New(New DataFrameParsingState(New DataCellCollection, GetType(DataCell), parsedFrameHeader.FrameLength, configurationFrame), binaryImage, startIndex)

        End Sub

        Public Sub New(ByVal dataFrame As IDataFrame)

            MyBase.New(dataFrame)

        End Sub

        Public Overrides ReadOnly Property InheritedType() As System.Type
            Get
                Return Me.GetType()
            End Get
        End Property

        Public Shadows ReadOnly Property Cells() As DataCellCollection
            Get
                Return MyBase.Cells
            End Get
        End Property

        'Public Property PacketNumber() As Byte
        '    Get
        '        Return m_packetNumber
        '    End Get
        '    Set(ByVal Value As Byte)
        '        If Value < 1 Then Throw New ArgumentOutOfRangeException("Data packets must be numbered from 1 to 255")
        '        m_packetNumber = Value
        '    End Set
        'End Property

        'Public Property SampleNumber() As Int16
        '    Get
        '        Return m_sampleNumber
        '    End Get
        '    Set(ByVal Value As Int16)
        '        m_sampleNumber = Value
        '    End Set
        'End Property

        'Public Property DataCellCount() As Int16
        '    Get
        '        Return m_dataCellCount
        '    End Get
        '    Set(ByVal Value As Int16)
        '        m_dataCellCount = Value
        '    End Set
        'End Property

        Protected Overrides ReadOnly Property HeaderLength() As Int16
            Get
                Return 20
            End Get
        End Property

        Protected Overrides ReadOnly Property HeaderImage() As Byte()
            Get
                'Dim buffer As Byte() = Array.CreateInstance(GetType(Byte), HeaderLength)

                'buffer(0) = SyncByte
                'buffer(1) = Convert.ToByte(1)
                'EndianOrder.BigEndian.CopyBytes(Convert.ToInt16(buffer.Length \ 2), buffer, 2)
                'EndianOrder.BigEndian.CopyBytes(Convert.ToUInt32(TimeTag.Value), buffer, 4)
                'EndianOrder.BigEndian.CopyBytes(Convert.ToInt16(m_sampleNumber), buffer, 8)
                'EndianOrder.BigEndian.CopyBytes(Convert.ToInt16(Cells.Count), buffer, 10)

                'Return buffer
            End Get
        End Property

        Protected Overrides Sub ParseHeaderImage(ByVal state As IChannelParsingState, ByVal binaryImage() As Byte, ByVal startIndex As Integer)

            'Dim configurationFrame As PDCStream.ConfigurationFrame = DirectCast(state, IDataFrameParsingState).ConfigurationFrame

            If binaryImage(startIndex) <> Common.SyncByte Then
                Throw New InvalidOperationException("Bad Data Stream: Expected sync byte &HAA as first byte in PDCstream data frame, got " & binaryImage(startIndex).ToString("x"c).PadLeft(2, "0"c))
            End If

            ' versionFrameType = binaryImage(startIndex + 1)

            ParsedFrameLength = EndianOrder.BigEndian.ToInt16(binaryImage, startIndex + 2)
            IDCode = EndianOrder.BigEndian.ToInt16(binaryImage, startIndex + 4)
            TimeTag = New Unix.TimeTag(EndianOrder.BigEndian.ToInt32(binaryImage, startIndex + 6))
            'FractionSec = EndianOrder.BigEndian.ToInt16(binaryImage, startIndex + 10)

        End Sub

        ' TODO: place this in proper override...
        'Public Overrides ReadOnly Property ProtocolSpecificDataLength() As Short
        '    Get
        '        Return 12
        '    End Get
        'End Property

        'Public Overrides ReadOnly Property ProtocolSpecificDataImage() As Byte()
        '    Get
        '        Dim buffer As Byte() = Array.CreateInstance(GetType(Byte), ProtocolSpecificDataLength)

        '        buffer(0) = Common.SyncByte
        '        buffer(1) = Convert.ToByte(1)
        '        EndianOrder.BigEndian.CopyBytes(Convert.ToInt16(buffer.Length \ 2), buffer, 2)
        '        EndianOrder.BigEndian.CopyBytes(Convert.ToUInt32(TimeTag.Value), buffer, 4)
        '        EndianOrder.BigEndian.CopyBytes(Convert.ToInt16(m_sampleNumber), buffer, 8)
        '        EndianOrder.BigEndian.CopyBytes(Convert.ToInt16(Cells.Count), buffer, 10)

        '        Return buffer
        '    End Get
        'End Property

        'Private m_configFile As ConfigurationFrame
        'Private m_timeTag As Unix.TimeTag
        'Private m_timeStamp As DateTime
        'Private m_sampleNumber As Integer

        'Public Cells As DataCell()
        'Public Published As Boolean

        'Public Const SyncByte As Byte = &HAA

        'Public Sub New(ByVal configFile As ConfigurationFrame, ByVal timeStamp As DateTime, ByVal index As Integer)

        '    m_configFile = configFile
        '    m_timeTag = New Unix.TimeTag(timeStamp)
        '    m_sampleNumber = index

        '    ' We precalculate a regular .NET timestamp with milliseconds sitting in the middle of the sample index
        '    m_timeStamp = timeStamp.AddMilliseconds((m_sampleNumber + 0.5@) * (1000@ / m_configFile.SampleRate))

        '    With m_configFile
        '        Cells = Array.CreateInstance(GetType(DataCell), .PMUCount)

        '        For x As Integer = 0 To Cells.Length - 1
        '            'Cells(x) = New DataCell(.PMU(x), index)
        '        Next
        '    End With

        'End Sub

        'Public ReadOnly Property TimeTag() As Unix.TimeTag
        '    Get
        '        Return m_timeTag
        '    End Get
        'End Property

        'Public ReadOnly Property Index() As Integer
        '    Get
        '        Return m_sampleNumber
        '    End Get
        'End Property

        'Public ReadOnly Property TimeStamp() As DateTime
        '    Get
        '        Return m_timeStamp
        '    End Get
        'End Property

        'Public ReadOnly Property ReadyToPublish() As Boolean
        '    Get
        '        Static packetReady As Boolean

        '        If Not packetReady Then
        '            Dim isReady As Boolean = True

        '            ' If we have data for each cell in the row, we can go ahead and publish it...
        '            For x As Integer = 0 To Cells.Length - 1
        '                If Cells(x).IsEmpty Then
        '                    isReady = False
        '                    Exit For
        '                End If
        '            Next

        '            If isReady Then packetReady = True
        '            Return isReady
        '        Else
        '            Return True
        '        End If
        '    End Get
        'End Property

        'Public ReadOnly Property BinaryLength() As Integer
        '    Get
        '        Dim length As Integer = 14

        '        For x As Integer = 0 To Cells.Length - 1
        '            length += Cells(x).BinaryLength
        '        Next

        '        Return length
        '    End Get
        'End Property

        'Public ReadOnly Property BinaryImage() As Byte()
        '    Get
        '        Dim buffer As Byte() = Array.CreateInstance(GetType(Byte), BinaryLength)
        '        Dim pmuID As Byte()
        '        Dim index As Integer

        '        buffer(0) = SyncByte
        '        buffer(1) = Convert.ToByte(1)
        '        EndianOrder.BigEndian.CopyBytes(Convert.ToInt16(buffer.Length \ 2), buffer, 2)
        '        EndianOrder.BigEndian.CopyBytes(Convert.ToUInt32(m_timeTag.Value), buffer, 4)
        '        EndianOrder.BigEndian.CopyBytes(Convert.ToInt16(m_sampleNumber), buffer, 8)
        '        EndianOrder.BigEndian.CopyBytes(Convert.ToInt16(Cells.Length), buffer, 10)
        '        index = 12

        '        For x As Integer = 0 To Cells.Length - 1
        '            BlockCopy(Cells(x).BinaryImage, 0, buffer, index, Cells(x).BinaryLength)
        '            index += Cells(x).BinaryLength
        '        Next

        '        ' Add check sum
        '        BlockCopy(BitConverter.GetBytes(XorCheckSum(buffer, 0, index)), 0, buffer, index, 2)

        '        Return buffer
        '    End Get
        'End Property

    End Class

End Namespace