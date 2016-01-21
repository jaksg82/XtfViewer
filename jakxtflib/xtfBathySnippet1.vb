Public Class XtfBathySnippet1
    Public Property Id As UInt32
    Public Property HeaderSize As UInt16
    Public Property DataSize As UInt16
    Public Property PingNumber As UInt32
    Public Property Beam As UInt16
    Public Property SnipSamples As UInt16
    Public Property GainStart As UInt16
    Public Property GainEnd As UInt16
    Public Property FragOffset As UInt16
    Public Property FragSamples As UInt16

    Public Sub New()
        Id = &H534E5031
        HeaderSize = 0
        DataSize = 0
        PingNumber = 0
        Beam = 0
        SnipSamples = 0
        GainStart = 0
        GainEnd = 0
        FragOffset = 0
        FragSamples = 0

    End Sub

    Public Sub New(byteArray As Byte())
        Dim chkNumber As UInt32
        Id = &H534E5031
        HeaderSize = 0
        DataSize = 0
        PingNumber = 0
        Beam = 0
        SnipSamples = 0
        GainStart = 0
        GainEnd = 0
        FragOffset = 0
        FragSamples = 0

        Using dp As New BinaryReader(ByteArrayToMemoryStream(byteArray))
            chkNumber = dp.ReadUInt32
            If chkNumber = Id Then
                HeaderSize = dp.ReadUInt16
                DataSize = dp.ReadUInt16
                PingNumber = dp.ReadUInt32
                Beam = dp.ReadUInt16
                SnipSamples = dp.ReadUInt16
                GainStart = dp.ReadUInt16
                GainEnd = dp.ReadUInt16
                FragOffset = dp.ReadUInt16
                FragSamples = dp.ReadUInt16

            Else
                'something wrong
            End If

        End Using

    End Sub


End Class
