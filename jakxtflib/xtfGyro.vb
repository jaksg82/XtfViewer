Imports System.Globalization

Public Class XtfGyro
    Public Property NumberBytesThisRecord As UInt32

    Shared ReadOnly Property HeaderType As Byte
        Get
            Return 84 'XTF_HEADER_SOURCETIME_GYRO
        End Get
    End Property

    Public Property GyroTime As Date
    Public Property TimeTag As UInt32
    Public Property SourceEpoch As UInt32
    Public Property Gyro As Single
    Public Property TimestampValidity As Byte


    Public Sub New()
        NumberBytesThisRecord = 64
        GyroTime = Date.MinValue
        TimestampValidity = 0
        SourceEpoch = 0
        Gyro = 0
        TimestampValidity = 0

    End Sub

    Public Sub New(byteArray As Byte())
        NumberBytesThisRecord = 64
        GyroTime = Date.MinValue
        TimestampValidity = 0
        SourceEpoch = 0
        Gyro = 0
        TimestampValidity = 0

        Dim Year, chkNumber As UInt16
        Dim Month, Day, Hour, Minutes, Seconds As Byte
        Dim MicroSeconds As UInt32
        Dim TimeString As String

        Using dp As New BinaryReader(ByteArrayToMemoryStream(byteArray))
            chkNumber = dp.ReadUInt16
            If chkNumber = XtfMainHeaderX36.MagicNumber Then
                dp.ReadByte() 'HeaderType
                dp.ReadByte() 'Unused
                dp.ReadUInt16() 'Unused
                dp.ReadUInt16() 'Unused
                dp.ReadUInt16() 'Unused
                NumberBytesThisRecord = dp.ReadUInt32

                'Read the ping time values
                Year = dp.ReadUInt16
                Month = dp.ReadByte
                Day = dp.ReadByte
                Hour = dp.ReadByte
                Minutes = dp.ReadByte
                Seconds = dp.ReadByte
                MicroSeconds = dp.ReadUInt32
                'Compose the ping time value
                TimeString = Year.ToString("0000", CultureInfo.InvariantCulture)
                TimeString = TimeString & "-" & Month.ToString("00", CultureInfo.InvariantCulture)
                TimeString = TimeString & "-" & Day.ToString("00", CultureInfo.InvariantCulture)
                TimeString = TimeString & "-" & Hour.ToString("00", CultureInfo.InvariantCulture)
                TimeString = TimeString & "-" & Minutes.ToString("00", CultureInfo.InvariantCulture)
                TimeString = TimeString & "-" & Seconds.ToString("00", CultureInfo.InvariantCulture)
                TimeString = TimeString & "-" & MicroSeconds.ToString("000000", CultureInfo.InvariantCulture)
                If Date.TryParseExact(TimeString, "yyyy-MM-dd-HH-mm-ss-ffffff", CultureInfo.InvariantCulture, DateTimeStyles.None, GyroTime) Then
                    'parsed OK
                Else
                    GyroTime = Date.MinValue
                End If

                SourceEpoch = dp.ReadUInt32
                TimeTag = dp.ReadUInt32
                Gyro = dp.ReadSingle
                TimestampValidity = dp.ReadByte

            Else
                'something wrong
            End If

        End Using


    End Sub

End Class
