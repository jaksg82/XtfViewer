Imports System.Globalization

''' <summary>
''' Attitude data packet.
''' </summary>
''' <remarks></remarks>
Public Class XtfAttitudeData
    Public Property NumberBytesThisRecord As UInt32

    Shared ReadOnly Property HeaderType As Byte
        Get
            Return 3 'XTF_HEADER_ATTITUDE
        End Get
    End Property

    Public Property Pitch As Single

    Public Property Roll As Single

    Public Property Heave As Single

    Public Property Yaw As Single

    Public Property Heading As Single

    Public Property TimeTag As UInt32

    Public Property FixTime As Date

    Public Property SourceEpoch As UInt32
    Public Property SourceEpochMicroseconds As UInt32

    Public Sub New()
        'from base class
        NumberBytesThisRecord = 64
        'this class
        Pitch = 0
        Roll = 0
        Heave = 0
        Yaw = 0
        TimeTag = 0
        Heading = 0
        FixTime = Date.MinValue
        SourceEpoch = 0
        SourceEpochMicroseconds = 0
    End Sub

    ''' <summary>
    ''' Create an Attitude data packet from the given bytes.
    ''' </summary>
    ''' <param name="ByteArray">Bytes that contain the informations.</param>
    ''' <remarks>The array MUST contain 64 byte values.</remarks>
    Public Sub New(byteArray As Byte())
        Dim Year, Milliseconds, chkNumber As UInt16
        Dim Month, Day, Hour, Minutes, Seconds As Byte
        Dim TimeString As String

        'Default values
        NumberBytesThisRecord = 64
        Pitch = 0
        Roll = 0
        Heave = 0
        Yaw = 0
        TimeTag = 0
        Heading = 0
        FixTime = Date.MinValue
        SourceEpoch = 0
        SourceEpochMicroseconds = 0

        Using dp As New BinaryReader(ByteArrayToMemoryStream(byteArray))
            chkNumber = dp.ReadUInt16
            If chkNumber = XtfMainHeaderX36.MagicNumber Then
                dp.ReadByte() 'HeaderType
                dp.ReadByte() 'Sub channel number
                dp.ReadUInt16() 'Num chan to follow
                dp.ReadUInt16() 'Reserved1[2]
                dp.ReadUInt16() 'Reserved1[2]
                dp.ReadUInt32() 'NumBytesThisRecord
                dp.ReadUInt32() 'Reserved2[2]
                dp.ReadUInt32() 'Reserved2[2]
                SourceEpochMicroseconds = dp.ReadUInt32() 'EpochMicroseconds
                SourceEpoch = dp.ReadUInt32() 'SourceEpoch
                Pitch = dp.ReadSingle
                Roll = dp.ReadSingle
                Heave = dp.ReadSingle
                Yaw = dp.ReadSingle
                TimeTag = dp.ReadUInt32
                Heading = dp.ReadSingle
                Year = dp.ReadUInt16
                Month = dp.ReadByte
                Day = dp.ReadByte
                Hour = dp.ReadByte
                Minutes = dp.ReadByte
                Seconds = dp.ReadByte
                Milliseconds = dp.ReadUInt16
                'Compose the fix time value
                TimeString = Year.ToString("0000", CultureInfo.InvariantCulture)
                TimeString = TimeString & "-" & Month.ToString("00", CultureInfo.InvariantCulture)
                TimeString = TimeString & "-" & Day.ToString("00", CultureInfo.InvariantCulture)
                TimeString = TimeString & "-" & Hour.ToString("00", CultureInfo.InvariantCulture)
                TimeString = TimeString & "-" & Minutes.ToString("00", CultureInfo.InvariantCulture)
                TimeString = TimeString & "-" & Seconds.ToString("00", CultureInfo.InvariantCulture)
                TimeString = TimeString & "-" & Milliseconds.ToString("000", CultureInfo.InvariantCulture)
                If Date.TryParseExact(TimeString, "yyyy-MM-dd-HH-mm-ss-fff", CultureInfo.InvariantCulture, DateTimeStyles.None, FixTime) Then
                    'parsed OK
                Else
                    FixTime = Date.MinValue
                End If
            Else
                'something wrong
            End If
        End Using

    End Sub

End Class
