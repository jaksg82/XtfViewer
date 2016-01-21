Imports System.Globalization

Public Class XtfRawSerialHeader

    Public Property NumberBytesThisRecord As UInt32

    Shared ReadOnly Property HeaderType As Byte
        Get
            Return 6 'XTF_HEADER_RAW_SERIAL
        End Get
    End Property
    Public Property SerialPort As Byte

    Public Property SerialDataTime As Date
    Public Property RawAsciiData As String

    Public Sub New()
        'from base class
        SerialPort = 0
        NumberBytesThisRecord = 64
        'this class
        SerialDataTime = Date.MinValue
        RawAsciiData = ""

    End Sub

    Public Sub New(byteArray As Byte())
        Dim Year, chkNumber, StringSize As UInt16
        Dim Month, Day, Hour, Minutes, Seconds, HSeconds As Byte
        Dim TimeString As String

        'Default values
        SerialPort = 0
        NumberBytesThisRecord = 64
        SerialDataTime = Date.MinValue
        RawAsciiData = ""

        Using dp As New BinaryReader(ByteArrayToMemoryStream(byteArray))
            chkNumber = dp.ReadUInt16
            If chkNumber = XtfMainHeaderX36.MagicNumber Then
                dp.ReadByte() 'HeaderType
                SerialPort = dp.ReadByte
                dp.ReadUInt16() 'Unused
                dp.ReadUInt16() 'Unused
                NumberBytesThisRecord = dp.ReadUInt32 'NumBytesThisRecord
                Year = dp.ReadUInt16
                Month = dp.ReadByte
                Day = dp.ReadByte
                Hour = dp.ReadByte
                Minutes = dp.ReadByte
                Seconds = dp.ReadByte
                HSeconds = dp.ReadByte
                dp.ReadUInt16() 'Days since 1st january
                dp.ReadUInt32() 'Millisecond timer value
                StringSize = dp.ReadUInt16
                RawAsciiData = dp.ReadChars(StringSize)
                'Compose the fix time value
                TimeString = Year.ToString("0000", CultureInfo.InvariantCulture)
                TimeString = TimeString & "-" & Month.ToString("00", CultureInfo.InvariantCulture)
                TimeString = TimeString & "-" & Day.ToString("00", CultureInfo.InvariantCulture)
                TimeString = TimeString & "-" & Hour.ToString("00", CultureInfo.InvariantCulture)
                TimeString = TimeString & "-" & Minutes.ToString("00", CultureInfo.InvariantCulture)
                TimeString = TimeString & "-" & Seconds.ToString("00", CultureInfo.InvariantCulture)
                TimeString = TimeString & "-" & HSeconds.ToString("00", CultureInfo.InvariantCulture)
                If Date.TryParseExact(TimeString, "yyyy-MM-dd-HH-mm-ss-ff", CultureInfo.InvariantCulture, DateTimeStyles.None, SerialDataTime) Then
                    'parsed OK
                Else
                    SerialDataTime = Date.MinValue
                End If
            Else
                'something wrong
            End If
        End Using

    End Sub

End Class
