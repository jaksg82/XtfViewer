Imports System.Globalization

Public Class XtfPingHeader

    Shared ReadOnly Property HeaderType As Byte
        Get
            Return 0 'XTF_HEADER_SONAR
        End Get
    End Property
    Public Property SubChannelNumber As Byte
    Public Property NumberChannelsToFollow As UInt16
    Public Property NumberBytesThisRecord As UInt32

    Public Property PingTime As Date

    Public Property EventNumber As UInt32
    Public Property PingNumber As UInt32

    Public Property SoundVelocity As Single
    Public Property OceanTide As Single
    Public Property ConductivityFrequency As Single
    Public Property TemperatureFrequency As Single
    Public Property PressureFrequency As Single
    Public Property PressureTemp As Single
    Public Property Conductivity As Single
    Public Property WaterTemperature As Single
    Public Property Pressure As Single
    Public Property ComputedSoundVelocity As Single
    Public Property MagX As Single
    Public Property MagY As Single
    Public Property MagZ As Single
    Public Property AuxValue1 As Single
    Public Property AuxValue2 As Single
    Public Property AuxValue3 As Single
    Public Property AuxValue4 As Single
    Public Property AuxValue5 As Single
    Public Property AuxValue6 As Single
    Public Property SpeedLog As Single
    Public Property Turbidity As Single
    Public Property ShipSpeed As Single
    Public Property ShipGyro As Single
    Public Property ShipCoordinateY As Double
    Public Property ShipCoordinateX As Double
    Public Property ShipAltitude As UInt16
    Public Property ShipDepth As UInt16

    Public Property FixTime As Date

    Public Property SensorSpeed As Single
    Public Property KP As Single
    Public Property SensorCoordinateY As Double
    Public Property SensorCoordinateX As Double
    Public Property SonarStatus As UInt16
    Public Property RangeToFish As UInt16
    Public Property BearingToFish As UInt16
    Public Property CableOut As UInt16
    Public Property Layback As Single
    Public Property CableTension As Single
    Public Property SensorDepth As Single
    Public Property SensorPrimaryAltitude As Single
    Public Property SensorAuxAltitude As Single
    Public Property SensorPitch As Single
    Public Property SensorRoll As Single
    Public Property SensorHeading As Single
    Public Property Heave As Single
    Public Property Yaw As Single
    Public Property AttitudeTimeTag As UInt32
    Public Property DistanceOffTrack As Single
    Public Property NavigationFixMilliseconds As UInt32

    Public Property ComputerClockTime As Date

    Public Property FishPositionDeltaX As Int16
    Public Property FishPositionDeltaY As Int16
    Public Property FishPositionErrorCode As Byte


    Public Sub New()

        SubChannelNumber = 0
        NumberChannelsToFollow = 0
        NumberBytesThisRecord = 256

        PingTime = Date.MinValue

        EventNumber = 0
        PingNumber = 0

        SoundVelocity = 0
        OceanTide = 0
        ConductivityFrequency = 0
        TemperatureFrequency = 0
        PressureFrequency = 0
        PressureTemp = 0
        Conductivity = 0
        WaterTemperature = 0
        Pressure = 0
        ComputedSoundVelocity = 0
        MagX = 0
        MagY = 0
        MagZ = 0
        AuxValue1 = 0
        AuxValue2 = 0
        AuxValue3 = 0
        AuxValue4 = 0
        AuxValue5 = 0
        AuxValue6 = 0
        SpeedLog = 0
        Turbidity = 0
        ShipSpeed = 0
        ShipGyro = 0
        ShipCoordinateY = 0.0
        ShipCoordinateX = 0.0
        ShipAltitude = 0
        ShipDepth = 0

        FixTime = Date.MinValue

        SensorSpeed = 0
        KP = 0
        SensorCoordinateY = 0.0
        SensorCoordinateX = 0.0
        SonarStatus = 0
        RangeToFish = 0
        BearingToFish = 0
        CableOut = 0
        Layback = 0
        CableTension = 0
        SensorDepth = 0
        SensorPrimaryAltitude = 0
        SensorAuxAltitude = 0
        SensorPitch = 0
        SensorRoll = 0
        SensorHeading = 0
        Heave = 0
        Yaw = 0
        AttitudeTimeTag = 0
        DistanceOffTrack = 0
        NavigationFixMilliseconds = 0

        ComputerClockTime = Date.MinValue

        FishPositionDeltaX = 0
        FishPositionDeltaY = 0
        FishPositionErrorCode = 0

    End Sub

    Public Sub New(byteArray As Byte())

        SubChannelNumber = 0
        NumberChannelsToFollow = 0
        NumberBytesThisRecord = 256

        PingTime = Date.MinValue

        EventNumber = 0
        PingNumber = 0

        SoundVelocity = 0
        OceanTide = 0
        ConductivityFrequency = 0
        TemperatureFrequency = 0
        PressureFrequency = 0
        PressureTemp = 0
        Conductivity = 0
        WaterTemperature = 0
        Pressure = 0
        ComputedSoundVelocity = 0
        MagX = 0
        MagY = 0
        MagZ = 0
        AuxValue1 = 0
        AuxValue2 = 0
        AuxValue3 = 0
        AuxValue4 = 0
        AuxValue5 = 0
        AuxValue6 = 0
        SpeedLog = 0
        Turbidity = 0
        ShipSpeed = 0
        ShipGyro = 0
        ShipCoordinateY = 0.0
        ShipCoordinateX = 0.0
        ShipAltitude = 0
        ShipDepth = 0

        FixTime = Date.MinValue

        SensorSpeed = 0
        KP = 0
        SensorCoordinateY = 0.0
        SensorCoordinateX = 0.0
        SonarStatus = 0
        RangeToFish = 0
        BearingToFish = 0
        CableOut = 0
        Layback = 0
        CableTension = 0
        SensorDepth = 0
        SensorPrimaryAltitude = 0
        SensorAuxAltitude = 0
        SensorPitch = 0
        SensorRoll = 0
        SensorHeading = 0
        Heave = 0
        Yaw = 0
        AttitudeTimeTag = 0
        DistanceOffTrack = 0
        NavigationFixMilliseconds = 0

        ComputerClockTime = Date.MinValue

        FishPositionDeltaX = 0
        FishPositionDeltaY = 0
        FishPositionErrorCode = 0

        Dim Year, HSecons, chkNumber As UInt16
        Dim Month, Day, Hour, Minutes, Seconds As Byte
        Dim TimeString As String

        Using dp As New BinaryReader(ByteArrayToMemoryStream(byteArray))
            chkNumber = dp.ReadUInt16 '2
            If chkNumber = XtfMainHeaderX36.MagicNumber Then
                dp.ReadByte() 'HeaderType 3
                SubChannelNumber = dp.ReadByte '4
                NumberChannelsToFollow = dp.ReadUInt16 '6
                dp.ReadUInt16() 'Unused 8
                dp.ReadUInt16() 'Unused 10
                NumberBytesThisRecord = dp.ReadUInt32() 'NumBytesThisRecord 14
                'Read the ping time values
                Year = dp.ReadUInt16 '16
                Month = dp.ReadByte '17
                Day = dp.ReadByte '18
                Hour = dp.ReadByte '19
                Minutes = dp.ReadByte '20
                Seconds = dp.ReadByte '21
                HSecons = dp.ReadByte '22
                dp.ReadUInt16() 'JulianDay 24
                'Compose the ping time value
                TimeString = Year.ToString("0000", CultureInfo.InvariantCulture)
                TimeString = TimeString & "-" & Month.ToString("00", CultureInfo.InvariantCulture)
                TimeString = TimeString & "-" & Day.ToString("00", CultureInfo.InvariantCulture)
                TimeString = TimeString & "-" & Hour.ToString("00", CultureInfo.InvariantCulture)
                TimeString = TimeString & "-" & Minutes.ToString("00", CultureInfo.InvariantCulture)
                TimeString = TimeString & "-" & Seconds.ToString("00", CultureInfo.InvariantCulture)
                TimeString = TimeString & "-" & HSecons.ToString("00", CultureInfo.InvariantCulture)
                If Date.TryParseExact(TimeString, "yyyy-MM-dd-HH-mm-ss-ff", CultureInfo.InvariantCulture, DateTimeStyles.None, PingTime) Then
                    'parsed OK
                Else
                    PingTime = Date.MinValue
                End If

                EventNumber = dp.ReadUInt32 '28
                PingNumber = dp.ReadUInt32 '32
                SoundVelocity = dp.ReadSingle '36
                OceanTide = dp.ReadSingle '40
                dp.ReadUInt32() 'Unused 44
                ConductivityFrequency = dp.ReadSingle '48
                TemperatureFrequency = dp.ReadSingle '52
                PressureFrequency = dp.ReadSingle '56
                PressureTemp = dp.ReadSingle '60
                Conductivity = dp.ReadSingle '64
                WaterTemperature = dp.ReadSingle '68
                Pressure = dp.ReadSingle '72
                ComputedSoundVelocity = dp.ReadSingle '76
                MagX = dp.ReadSingle '80
                MagY = dp.ReadSingle '84
                MagZ = dp.ReadSingle '88
                AuxValue1 = dp.ReadSingle '92
                AuxValue2 = dp.ReadSingle '96
                AuxValue3 = dp.ReadSingle '100
                AuxValue4 = dp.ReadSingle '104
                AuxValue5 = dp.ReadSingle '108
                AuxValue6 = dp.ReadSingle '112
                SpeedLog = dp.ReadSingle '116
                Turbidity = dp.ReadSingle '120
                ShipSpeed = dp.ReadSingle '124
                ShipGyro = dp.ReadSingle '128
                ShipCoordinateY = dp.ReadDouble '136
                ShipCoordinateX = dp.ReadDouble '144
                ShipAltitude = dp.ReadUInt16 '146
                ShipDepth = dp.ReadUInt16 '148
                'Read the ping time values
                Hour = dp.ReadByte '149
                Minutes = dp.ReadByte '150
                Seconds = dp.ReadByte '151
                HSecons = dp.ReadByte '152
                'Compose the ping time value
                TimeString = PingTime.ToString("yyyy-MM-dd", CultureInfo.InvariantCulture)
                TimeString = TimeString & "-" & Hour.ToString("00", CultureInfo.InvariantCulture)
                TimeString = TimeString & "-" & Minutes.ToString("00", CultureInfo.InvariantCulture)
                TimeString = TimeString & "-" & Seconds.ToString("00", CultureInfo.InvariantCulture)
                TimeString = TimeString & "-" & HSecons.ToString("00", CultureInfo.InvariantCulture)
                If Date.TryParseExact(TimeString, "yyyy-MM-dd-HH-mm-ss-ff", CultureInfo.InvariantCulture, DateTimeStyles.None, FixTime) Then
                    'parsed OK
                Else
                    FixTime = Date.MinValue
                End If

                SensorSpeed = dp.ReadSingle '156
                KP = dp.ReadSingle '160
                SensorCoordinateY = dp.ReadDouble '168
                SensorCoordinateX = dp.ReadDouble '176
                SonarStatus = dp.ReadUInt16 '178
                RangeToFish = dp.ReadUInt16 '180
                BearingToFish = dp.ReadUInt16 '182
                CableOut = dp.ReadUInt16 '184
                Layback = dp.ReadSingle '188
                CableTension = dp.ReadSingle '192
                SensorDepth = dp.ReadSingle '196
                SensorPrimaryAltitude = dp.ReadSingle '200
                SensorAuxAltitude = dp.ReadSingle '204
                SensorPitch = dp.ReadSingle '208
                SensorRoll = dp.ReadSingle '212
                SensorHeading = dp.ReadSingle '216
                Heave = dp.ReadSingle '220
                Yaw = dp.ReadSingle '224
                AttitudeTimeTag = dp.ReadUInt32 '228
                DistanceOffTrack = dp.ReadSingle '232
                NavigationFixMilliseconds = dp.ReadUInt32 '236
                'Read the computer time values
                Hour = dp.ReadByte '237
                Minutes = dp.ReadByte '238
                Seconds = dp.ReadByte '239
                HSecons = dp.ReadByte '240
                'Compose the conputer time value
                TimeString = PingTime.ToString("yyyy-MM-dd", CultureInfo.InvariantCulture)
                TimeString = TimeString & "-" & Hour.ToString("00", CultureInfo.InvariantCulture)
                TimeString = TimeString & "-" & Minutes.ToString("00", CultureInfo.InvariantCulture)
                TimeString = TimeString & "-" & Seconds.ToString("00", CultureInfo.InvariantCulture)
                TimeString = TimeString & "-" & HSecons.ToString("00", CultureInfo.InvariantCulture)
                If Date.TryParseExact(TimeString, "yyyy-MM-dd-HH-mm-ss-ff", CultureInfo.InvariantCulture, DateTimeStyles.None, ComputerClockTime) Then
                    'parsed OK
                Else
                    ComputerClockTime = Date.MinValue
                End If

                FishPositionDeltaX = dp.ReadInt16 '242
                FishPositionDeltaY = dp.ReadInt16 '244
                FishPositionErrorCode = dp.ReadByte '245

            Else
                'something wrong
            End If
        End Using

    End Sub

End Class
