Public Class XtfQpsMbesEntry
    Public Property Id As Int32
    Public Property Intensity As Double
    Public Property Quality As Int32
    Public Property TwoWayTravelTime As Double
    Public Property DeltaTime As Double
    Public Property BeamAngle As Double
    Public Property TiltAngle As Double

    Public Sub New()
        Id = 0
        Intensity = 0
        Quality = 0
        TwoWayTravelTime = 0
        DeltaTime = 0
        BeamAngle = 0
        TiltAngle = 0

    End Sub

    Public Sub New(byteArray As Byte())
        Id = 0
        Intensity = 0
        Quality = 0
        TwoWayTravelTime = 0
        DeltaTime = 0
        BeamAngle = 0
        TiltAngle = 0

        Using dp As New BinaryReader(ByteArrayToMemoryStream(byteArray))
            Id = dp.ReadInt32
            Intensity = dp.ReadDouble
            Quality = dp.ReadInt32
            TwoWayTravelTime = dp.ReadDouble
            DeltaTime = dp.ReadDouble
            BeamAngle = dp.ReadDouble
            TiltAngle = dp.ReadDouble
        End Using

    End Sub

End Class
