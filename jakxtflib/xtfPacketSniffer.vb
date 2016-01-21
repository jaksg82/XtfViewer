Public Class XtfPacketSniffer
    Public Property MagicNumber As UInt16
    Public Property HeaderType As Byte
    Public Property SubChannelNumber As Byte
    Public Property NumberChannelsToFollow As UInt16
    Public Property Reserved1 As UInt16
    Public Property Reserved2 As UInt16
    Public Property NumberBytesThisRecord As UInt32
    Public Property PacketTime As Date

    Public Sub New()
        MagicNumber = 0
        HeaderType = 0
        SubChannelNumber = 0
        NumberChannelsToFollow = 0
        Reserved1 = 0
        Reserved2 = 0
        NumberBytesThisRecord = 0
        PacketTime = Date.MinValue
    End Sub

    ''' <summary>
    ''' Extract the packet basic information from the given array of bytes.
    ''' </summary>
    ''' <param name="byteArray">14 bytes array for basic information OR 256 byte for packet time.</param>
    Public Sub New(byteArray As Byte())
        MagicNumber = 0
        HeaderType = 0
        SubChannelNumber = 0
        NumberChannelsToFollow = 0
        Reserved1 = 0
        Reserved2 = 0
        NumberBytesThisRecord = 0
        PacketTime = Date.MinValue

        Using pd As New IO.BinaryReader(ByteArrayToMemoryStream(byteArray))
            If byteArray.Count >= 14 Then
                'Read the basic information of the packet
                MagicNumber = pd.ReadUInt16
                HeaderType = pd.ReadByte
                SubChannelNumber = pd.ReadByte
                NumberChannelsToFollow = pd.ReadUInt16
                Reserved1 = pd.ReadUInt16
                Reserved2 = pd.ReadUInt16
                NumberBytesThisRecord = pd.ReadUInt32
                If byteArray.Count >= 256 Then
                    'TODO: extract packet time from each packet type known
                    Select Case HeaderType
                        Case 0, 2, 4, 5, 8, 9, 10, 14, 16, 17, 18, 19, 22, 25, 27, 28, 60, 61, 62, 65, 66, 68, 69, 73 'Ping header types
                            Dim tmpHdr As New XtfPingHeader(byteArray)
                            PacketTime = tmpHdr.PingTime
                        Case 1 'Notes - text annotation
                            Dim tmpHdr As New XtfNotesHeader(byteArray)
                            PacketTime = tmpHdr.AnnotationTime
                        Case 3, 103
                            Dim tmpHdr As New XtfAttitudeData(byteArray)
                            PacketTime = tmpHdr.FixTime
                        Case 6
                            Dim tmpHdr As New XtfRawSerialHeader(byteArray)
                            PacketTime = tmpHdr.SerialDataTime
                        Case 11, 15
                            Dim tmpHdr As New XtfHighSpeedSensor(byteArray)
                            PacketTime = tmpHdr.SensorTime
                        Case 23, 84
                            Dim tmpHdr As New XtfGyro(byteArray)
                            PacketTime = tmpHdr.GyroTime
                        Case 26
                            Dim tmpHdr As New XtfQpsSingleBeam(byteArray)
                            PacketTime = tmpHdr.SingleBeamTime
                        Case 42
                            Dim tmpHdr As New XtfNavigation(byteArray)
                            PacketTime = tmpHdr.NavigationTime
                        Case 100, 107
                            Dim tmpHdr As New XtfPosRawNavigation(byteArray)
                            PacketTime = tmpHdr.PositionRawTime
                        Case Else
                            PacketTime = Date.MinValue
                    End Select

                End If
            Else
                'Not enough bytes
                'Return empty packet
            End If
        End Using
    End Sub

End Class
