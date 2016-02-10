Imports System.Globalization

Public Class XtfPosRawNavigation
    Public Property NumberBytesThisRecord As UInt32

    Shared ReadOnly Property HeaderType As Byte
        Get
            Return 107 'XTF_HEADER_POS_RAW_NAVIGATION
        End Get
    End Property

    Public Property PositionRawTime As Date
    Public Property RawCoordinateY As Double
    Public Property RawCoordinateX As Double
    Public Property RawAltitude As Double
    Public Property Pitch As Single
    Public Property Roll As Single
    Public Property Heave As Single
    Public Property Heading As Single

    Public Sub New()
        NumberBytesThisRecord = 64
        PositionRawTime = Date.MinValue
        RawCoordinateY = 0
        RawCoordinateX = 0
        RawAltitude = 0
        Pitch = 0
        Roll = 0
        Heave = 0
        Heading = 0

    End Sub

    Public Sub New(byteArray As Byte())
        Dim Year, chkNumber, MicroSeconds As UInt16
        Dim Month, Day, Hour, Minutes, Seconds As Byte
        Dim TimeString As String

        NumberBytesThisRecord = 64
        PositionRawTime = Date.MinValue
        RawCoordinateY = 0
        RawCoordinateX = 0
        RawAltitude = 0
        Pitch = 0
        Roll = 0
        Heave = 0
        Heading = 0

        Using dp As New BinaryReader(ByteArrayToMemoryStream(byteArray))
            chkNumber = dp.ReadUInt16
            If chkNumber = XtfMainHeaderX36.MagicNumber Then
                dp.ReadByte() 'HeaderType
                dp.ReadByte() 'SubChannelNumber
                dp.ReadUInt16() 'NumChansToFollow
                dp.ReadUInt16() 'Reserved1[2]
                dp.ReadUInt16() 'Reserved1[2]
                NumberBytesThisRecord = dp.ReadUInt32 'NumBytesThisRecord
                Year = dp.ReadUInt16
                Month = dp.ReadByte
                Day = dp.ReadByte
                Hour = dp.ReadByte
                Minutes = dp.ReadByte
                Seconds = dp.ReadByte
                MicroSeconds = dp.ReadUInt16
                'Compose the fix time value
                TimeString = Year.ToString("0000", CultureInfo.InvariantCulture)
                TimeString = TimeString & "-" & Month.ToString("00", CultureInfo.InvariantCulture)
                TimeString = TimeString & "-" & Day.ToString("00", CultureInfo.InvariantCulture)
                TimeString = TimeString & "-" & Hour.ToString("00", CultureInfo.InvariantCulture)
                TimeString = TimeString & "-" & Minutes.ToString("00", CultureInfo.InvariantCulture)
                TimeString = TimeString & "-" & Seconds.ToString("00", CultureInfo.InvariantCulture)
                TimeString = TimeString & "-" & MicroSeconds.ToString("0000", CultureInfo.InvariantCulture)
                If Date.TryParseExact(TimeString, "yyyy-MM-dd-HH-mm-ss-ffff", CultureInfo.InvariantCulture, DateTimeStyles.None, PositionRawTime) Then
                    'parsed OK
                Else
                    PositionRawTime = Date.MinValue
                End If

                RawCoordinateY = dp.ReadDouble
                RawCoordinateX = dp.ReadDouble
                RawAltitude = dp.ReadDouble
                Pitch = dp.ReadSingle
                Roll = dp.ReadSingle
                Heave = dp.ReadSingle
                Heading = dp.ReadSingle

            Else
                'something wrong
            End If
        End Using

    End Sub


End Class
