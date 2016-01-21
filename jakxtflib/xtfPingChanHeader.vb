Public Class XtfPingChanHeader

    Public Property ChannelNumber As UInt16
    Public Property DownSampleMethod As UInt16
    Public Property SlantRange As Single
    Public Property GroundRange As Single
    Public Property TimeDelay As Single
    Public Property TimeDuration As Single
    Public Property SecondsPerPing As Single
    Public Property ProcessingFlags As UInt16
    Public Property Frequency As UInt16
    Public Property InitialGainCode As UInt16
    Public Property GainCode As UInt16
    Public Property Bandwidth As UInt16
    Public Property ContactNumber As UInt32
    Public Property ContactClassification As UInt16
    Public Property ContactSubNumber As Byte
    Public Property ContactType As Byte
    Public Property NumberSamples As UInt32
    Public Property MillivoltScale As UInt16
    Public Property ContactTimeOffTrack As Single
    Public Property ContactCloseNumber As Byte
    Public Property FixedVSOP As Single
    Public Property Weight As Int16

    Public Sub New()
        ChannelNumber = 0
        DownSampleMethod = 0
        SlantRange = 0
        GroundRange = 0
        TimeDelay = 0
        TimeDuration = 0
        SecondsPerPing = 0
        ProcessingFlags = 0
        Frequency = 0
        InitialGainCode = 0
        GainCode = 0
        Bandwidth = 0
        ContactNumber = 0
        ContactClassification = 0
        ContactSubNumber = 0
        ContactType = 0
        NumberSamples = 0
        MillivoltScale = 0
        ContactTimeOffTrack = 0
        ContactCloseNumber = 0
        FixedVSOP = 0
        Weight = 0

    End Sub

    Public Sub New(byteArray As Byte())
        ChannelNumber = 0
        DownSampleMethod = 0
        SlantRange = 0
        GroundRange = 0
        TimeDelay = 0
        TimeDuration = 0
        SecondsPerPing = 0
        ProcessingFlags = 0
        Frequency = 0
        InitialGainCode = 0
        GainCode = 0
        Bandwidth = 0
        ContactNumber = 0
        ContactClassification = 0
        ContactSubNumber = 0
        ContactType = 0
        NumberSamples = 0
        MillivoltScale = 0
        ContactTimeOffTrack = 0
        ContactCloseNumber = 0
        FixedVSOP = 0
        Weight = 0

        Using dp As New BinaryReader(ByteArrayToMemoryStream(byteArray))
            ChannelNumber = dp.ReadUInt16
            DownSampleMethod = dp.ReadUInt16
            SlantRange = dp.ReadSingle
            GroundRange = dp.ReadSingle
            TimeDelay = dp.ReadSingle
            TimeDuration = dp.ReadSingle
            SecondsPerPing = dp.ReadSingle
            ProcessingFlags = dp.ReadUInt16
            Frequency = dp.ReadUInt16
            InitialGainCode = dp.ReadUInt16
            GainCode = dp.ReadUInt16
            Bandwidth = dp.ReadUInt16
            ContactNumber = dp.ReadUInt32
            ContactClassification = dp.ReadUInt16
            ContactSubNumber = dp.ReadByte
            ContactType = dp.ReadByte
            NumberSamples = dp.ReadUInt32
            MillivoltScale = dp.ReadUInt16
            ContactTimeOffTrack = dp.ReadSingle
            ContactCloseNumber = dp.ReadByte
            dp.ReadByte() 'Unused
            FixedVSOP = dp.ReadSingle
            Weight = dp.ReadInt16

        End Using
    End Sub

End Class
