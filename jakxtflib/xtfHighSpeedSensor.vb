Imports System.Globalization

Public Class XtfHighSpeedSensor
    Public Property NumberBytesThisRecord As UInt32
    Public Property NumberSensorBytes As UInt32

    Shared ReadOnly Property HeaderType As Byte
        Get
            Return 15 'XTF_HEADER_HIGHSPEED_SENSOR2
        End Get
    End Property
    Public Property SubChannelNumber As Byte
    Public Property SensorTime As Date
    Public Property RelativeBathyPingNumber As UInt32

    Public Sub New()
        'from base class
        NumberBytesThisRecord = 64
        'this class
        SubChannelNumber = 0
        SensorTime = Date.MinValue
        NumberSensorBytes = 0
        RelativeBathyPingNumber = 0

    End Sub

    Public Sub New(byteArray As Byte())
        'from base class
        NumberBytesThisRecord = 64
        'this class
        SubChannelNumber = 0
        SensorTime = Date.MinValue
        NumberSensorBytes = 0
        RelativeBathyPingNumber = 0

        Dim Year, HSecons, chkNumber As UInt16
        Dim Month, Day, Hour, Minutes, Seconds As Byte
        Dim TimeString As String

        Using dp As New BinaryReader(ByteArrayToMemoryStream(byteArray))
            chkNumber = dp.ReadUInt16
            If chkNumber = XtfMainHeaderX36.MagicNumber Then
                dp.ReadByte() 'HeaderType
                SubChannelNumber = dp.ReadByte
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
                HSecons = dp.ReadByte
                'Compose the ping time value
                TimeString = Year.ToString("0000", CultureInfo.InvariantCulture)
                TimeString = TimeString & "-" & Month.ToString("00", CultureInfo.InvariantCulture)
                TimeString = TimeString & "-" & Day.ToString("00", CultureInfo.InvariantCulture)
                TimeString = TimeString & "-" & Hour.ToString("00", CultureInfo.InvariantCulture)
                TimeString = TimeString & "-" & Minutes.ToString("00", CultureInfo.InvariantCulture)
                TimeString = TimeString & "-" & Seconds.ToString("00", CultureInfo.InvariantCulture)
                TimeString = TimeString & "-" & HSecons.ToString("00", CultureInfo.InvariantCulture)
                If Date.TryParseExact(TimeString, "yyyy-MM-dd-HH-mm-ss-ff", CultureInfo.InvariantCulture, DateTimeStyles.None, SensorTime) Then
                    'parsed OK
                Else
                    SensorTime = Date.MinValue
                End If

                NumberSensorBytes = dp.ReadUInt32
                RelativeBathyPingNumber = dp.ReadUInt32

            Else
                'something wrong
            End If

        End Using


    End Sub

End Class
