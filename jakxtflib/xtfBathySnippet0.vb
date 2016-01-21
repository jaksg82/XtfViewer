Public Class XtfBathySnippet0
    Public Property Id As UInt32
    Public Property HeaderSize As UInt16
    Public Property DataSize As UInt16
    Public Property PingNumber As UInt32
    Public Property Seconds As UInt32
    Public Property Milliseconds As UInt32
    Public Property Latency As UInt16
    Public Property SonarId1 As UInt16
    Public Property SonarId2 As UInt16
    Public Property SonarModel As UInt16
    Public Property Frequency As UInt16
    Public Property SoundSpeed As UInt16
    Public Property SampleRate As UInt16
    Public Property PingRate As UInt16
    Public Property Range As UInt16
    Public Property Power As UInt16
    Public Property Gain As UInt16
    Public Property PulseWidth As UInt16
    Public Property Spread As UInt16
    Public Property Absorb As UInt16
    Public Property ProjectorType As UInt16
    Public Property ProjectorWidth As UInt16
    Public Property SpacingNumerator As UInt16
    Public Property SpacingDenominator As UInt16
    Public Property ProjectorAngle As Int16
    Public Property MinRange As UInt16
    Public Property MaxRange As UInt16
    Public Property MinDepth As UInt16
    Public Property MaxDepth As UInt16
    Public Property Filters As UInt16
    Public Property Flags As UInt16
    Public Property HeadTemp As Int16
    Public Property BeamCount As UInt16

    Public Sub New()

        Id = &H534E5030
        HeaderSize = 0
        DataSize = 0
        PingNumber = 0
        Seconds = 0
        Milliseconds = 0
        Latency = 0
        SonarId1 = 0
        SonarId2 = 0
        SonarModel = 0
        Frequency = 0
        SoundSpeed = 0
        SampleRate = 0
        PingRate = 0
        Range = 0
        Power = 0
        Gain = 0
        PulseWidth = 0
        Spread = 0
        Absorb = 0
        ProjectorType = 0
        ProjectorWidth = 0
        SpacingNumerator = 0
        SpacingDenominator = 0
        ProjectorAngle = 0
        MinRange = 0
        MaxRange = 0
        MinDepth = 0
        MaxDepth = 0
        Filters = 0
        Flags = 0
        HeadTemp = 0
        BeamCount = 0

    End Sub

    Public Sub New(byteArray As Byte())
        Dim chkNumber As UInt32
        Id = &H534E5030
        HeaderSize = 0
        DataSize = 0
        PingNumber = 0
        Seconds = 0
        Milliseconds = 0
        Latency = 0
        SonarId1 = 0
        SonarId2 = 0
        SonarModel = 0
        Frequency = 0
        SoundSpeed = 0
        SampleRate = 0
        PingRate = 0
        Range = 0
        Power = 0
        Gain = 0
        PulseWidth = 0
        Spread = 0
        Absorb = 0
        ProjectorType = 0
        ProjectorWidth = 0
        SpacingNumerator = 0
        SpacingDenominator = 0
        ProjectorAngle = 0
        MinRange = 0
        MaxRange = 0
        MinDepth = 0
        MaxDepth = 0
        Filters = 0
        Flags = 0
        HeadTemp = 0
        BeamCount = 0

        Using dp As New BinaryReader(ByteArrayToMemoryStream(byteArray))
            chkNumber = dp.ReadUInt32
            If chkNumber = &H534E5030 Then
                HeaderSize = dp.ReadUInt16
                DataSize = dp.ReadUInt16
                PingNumber = dp.ReadUInt32
                Seconds = dp.ReadUInt32
                Milliseconds = dp.ReadUInt32
                Latency = dp.ReadUInt16
                SonarId1 = dp.ReadUInt16
                SonarId2 = dp.ReadUInt16
                SonarModel = dp.ReadUInt16
                Frequency = dp.ReadUInt16
                SoundSpeed = dp.ReadUInt16
                SampleRate = dp.ReadUInt16
                PingRate = dp.ReadUInt16
                Range = dp.ReadUInt16
                Power = dp.ReadUInt16
                Gain = dp.ReadUInt16
                PulseWidth = dp.ReadUInt16
                Spread = dp.ReadUInt16
                Absorb = dp.ReadUInt16
                ProjectorType = dp.ReadUInt16
                ProjectorWidth = dp.ReadUInt16
                SpacingNumerator = dp.ReadUInt16
                SpacingDenominator = dp.ReadUInt16
                ProjectorAngle = dp.ReadInt16
                MinRange = dp.ReadUInt16
                MaxRange = dp.ReadUInt16
                MinDepth = dp.ReadUInt16
                MaxDepth = dp.ReadUInt16
                Filters = dp.ReadUInt16
                Flags = dp.ReadUInt16
                HeadTemp = dp.ReadInt16
                BeamCount = dp.ReadUInt16

            Else
                'something wrong
            End If

        End Using

    End Sub

End Class
