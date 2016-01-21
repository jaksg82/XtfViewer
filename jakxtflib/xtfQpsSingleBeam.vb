Imports System.Globalization

Public Class XtfQpsSingleBeam
    Public Property NumberBytesThisRecord As UInt32

    Shared ReadOnly Property HeaderType As Byte
        Get
            Return 26 'XTF_HEADER_Q_SINGLEBEAM
        End Get
    End Property

    Public Property SubChannelNumber As Byte 'ID in CHANNELINFO structures
    Public Property TimeTag As UInt32
    Public Property Id As Int32
    Public Property SoundVelocity As Single
    Public Property Intensity As Single
    Public Property Quality As Int32
    Public Property TwoWayTravelTime As Single

    Public Property SingleBeamTime As Date

    Public Sub New()
        NumberBytesThisRecord = 53
        SubChannelNumber = 0
        TimeTag = 0
        SingleBeamTime = Date.MinValue
        Id = 0
        SoundVelocity = 0
        Intensity = 0
        Quality = 0
        TwoWayTravelTime = 0

    End Sub

    Public Sub New(byteArray As Byte())
        Dim Year, chkNumber, MilliSeconds As UInt16
        Dim Month, Day, Hour, Minutes, Seconds As Byte
        Dim TimeString As String

        NumberBytesThisRecord = 53
        SubChannelNumber = 0
        TimeTag = 0
        SingleBeamTime = Date.MinValue
        Id = 0
        SoundVelocity = 0
        Intensity = 0
        Quality = 0
        TwoWayTravelTime = 0

        Using dp As New BinaryReader(ByteArrayToMemoryStream(byteArray))
            chkNumber = dp.ReadUInt16
            If chkNumber = XtfMainHeaderX36.MagicNumber Then
                dp.ReadByte() 'HeaderType
                SubChannelNumber = dp.ReadByte
                dp.ReadUInt16() 'Unused
                dp.ReadUInt16() 'Unused
                dp.ReadUInt16() 'Unused
                NumberBytesThisRecord = dp.ReadUInt32 'NumBytesThisRecord
                TimeTag = dp.ReadUInt32
                Id = dp.ReadInt32
                SoundVelocity = dp.ReadSingle
                Intensity = dp.ReadSingle
                Quality = dp.ReadInt32
                TwoWayTravelTime = dp.ReadSingle
                Year = dp.ReadUInt16
                Month = dp.ReadByte
                Day = dp.ReadByte
                Hour = dp.ReadByte
                Minutes = dp.ReadByte
                Seconds = dp.ReadByte
                MilliSeconds = dp.ReadUInt16
                'Compose the fix time value
                TimeString = Year.ToString("0000", CultureInfo.InvariantCulture)
                TimeString = TimeString & "-" & Month.ToString("00", CultureInfo.InvariantCulture)
                TimeString = TimeString & "-" & Day.ToString("00", CultureInfo.InvariantCulture)
                TimeString = TimeString & "-" & Hour.ToString("00", CultureInfo.InvariantCulture)
                TimeString = TimeString & "-" & Minutes.ToString("00", CultureInfo.InvariantCulture)
                TimeString = TimeString & "-" & Seconds.ToString("00", CultureInfo.InvariantCulture)
                TimeString = TimeString & "-" & MilliSeconds.ToString("000", CultureInfo.InvariantCulture)
                If Date.TryParseExact(TimeString, "yyyy-MM-dd-HH-mm-ss-fff", CultureInfo.InvariantCulture, DateTimeStyles.None, SingleBeamTime) Then
                    'parsed OK
                Else
                    SingleBeamTime = Date.MinValue
                End If

            Else
                'something wrong
            End If
        End Using

    End Sub


End Class
