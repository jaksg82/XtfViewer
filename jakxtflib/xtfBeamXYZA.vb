Public Class XtfBeamXYZA

    Public Property PositionOffsetX As Double
    Public Property PositionOffsetY As Double
    Public Property Depth As Single
    Public Property Time As Double
    Public Property Amplitude As Int16
    Public Property Quality As Byte

    Public Sub New()
        PositionOffsetX = 0
        PositionOffsetY = 0
        Depth = 0
        Time = 0
        Amplitude = 0
        Quality = 0

    End Sub

    Public Sub New(byteArray As Byte())
        PositionOffsetX = 0
        PositionOffsetY = 0
        Depth = 0
        Time = 0
        Amplitude = 0
        Quality = 0

        Using dp As New BinaryReader(ByteArrayToMemoryStream(byteArray))
            PositionOffsetY = dp.ReadDouble
            PositionOffsetX = dp.ReadDouble
            Depth = dp.ReadSingle
            Time = dp.ReadDouble
            Amplitude = dp.ReadInt16
            Quality = dp.ReadByte

        End Using
    End Sub

End Class
