Public Class XtfQpsMultiTxEntry
    Public Property Id As Int32
    Public Property Intensity As Single
    Public Property Quality As Int32
    Public Property TwoWayTravelTime As Single
    Public Property DeltaTime As Single
    Public Property OffsetX As Single
    Public Property OffsetY As Single
    Public Property OffsetZ As Single

    Public Sub New()
        Id = 0
        Intensity = 0
        Quality = 0
        TwoWayTravelTime = 0
        DeltaTime = 0
        OffsetX = 0
        OffsetY = 0
        OffsetZ = 0

    End Sub

    Public Sub New(byteArray As Byte())
        Id = 0
        Intensity = 0
        Quality = 0
        TwoWayTravelTime = 0
        DeltaTime = 0
        OffsetX = 0
        OffsetY = 0
        OffsetZ = 0

        Using dp As New BinaryReader(ByteArrayToMemoryStream(byteArray))
            Id = dp.ReadInt32
            Intensity = dp.ReadSingle
            Quality = dp.ReadInt32
            TwoWayTravelTime = dp.ReadSingle
            DeltaTime = dp.ReadSingle
            OffsetX = dp.ReadSingle
            OffsetY = dp.ReadSingle
            OffsetZ = dp.ReadSingle

        End Using

    End Sub

End Class
