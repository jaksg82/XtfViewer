Imports System.Globalization

Public Class XtfRawCustomHeader
    Public Property NumberBytesThisRecord As UInt32

    Shared ReadOnly Property HeaderType As Byte
        Get
            Return 199 'XTF_HEADER_CUSTOM
        End Get
    End Property

    Public Property ManufacturerId As Byte
    Public Property SonarId As UInt16
    Public Property PacketId As UInt16
    Public Property PacketTime As Date
    Public Property PingNumber As UInt32
    Public Property TimeTag As UInt32
    Public Property NumberCustomerBytes As UInt32

    Public Sub New()
        ManufacturerId = 0
        SonarId = 0
        PacketId = 0
        PacketTime = Date.MinValue
        PingNumber = 0
        TimeTag = 0
        NumberCustomerBytes = 0
        NumberBytesThisRecord = CUInt(NumberCustomerBytes + 64)
    End Sub

    Public Sub New(byteArray As Byte())
        Dim Year, chkNumber As UInt16
        Dim Month, Day, Hour, Minutes, Seconds, HSeconds As Byte
        Dim TimeString As String

        ManufacturerId = 0
        SonarId = 0
        PacketId = 0
        PacketTime = Date.MinValue
        PingNumber = 0
        TimeTag = 0
        NumberCustomerBytes = 0
        NumberBytesThisRecord = CUInt(NumberCustomerBytes + 64)

        Using dp As New BinaryReader(ByteArrayToMemoryStream(byteArray))
            chkNumber = dp.ReadUInt16
            If chkNumber = XtfMainHeaderX36.MagicNumber Then
                dp.ReadByte() 'HeaderType
                ManufacturerId = dp.ReadByte
                SonarId = dp.ReadUInt16
                PacketId = dp.ReadUInt16
                dp.ReadUInt16() 'Unused
                NumberBytesThisRecord = dp.ReadUInt32 'NumBytesThisRecord

                Year = dp.ReadUInt16
                Month = dp.ReadByte
                Day = dp.ReadByte
                Hour = dp.ReadByte
                Minutes = dp.ReadByte
                Seconds = dp.ReadByte
                HSeconds = dp.ReadByte
                'Compose the fix time value
                TimeString = Year.ToString("0000", CultureInfo.InvariantCulture)
                TimeString = TimeString & "-" & Month.ToString("00", CultureInfo.InvariantCulture)
                TimeString = TimeString & "-" & Day.ToString("00", CultureInfo.InvariantCulture)
                TimeString = TimeString & "-" & Hour.ToString("00", CultureInfo.InvariantCulture)
                TimeString = TimeString & "-" & Minutes.ToString("00", CultureInfo.InvariantCulture)
                TimeString = TimeString & "-" & Seconds.ToString("00", CultureInfo.InvariantCulture)
                TimeString = TimeString & "-" & HSeconds.ToString("00", CultureInfo.InvariantCulture)
                If Date.TryParseExact(TimeString, "yyyy-MM-dd-HH-mm-ss-ff", CultureInfo.InvariantCulture, DateTimeStyles.None, PacketTime) Then
                    'parsed OK
                Else
                    PacketTime = Date.MinValue
                End If

                dp.ReadUInt16() 'JulianDay
                dp.ReadUInt16() 'Unused
                dp.ReadUInt16() 'Unused
                PingNumber = dp.ReadUInt32
                TimeTag = dp.ReadUInt32
                NumberCustomerBytes = dp.ReadUInt32

            Else
                'something wrong
            End If
        End Using

    End Sub

End Class
