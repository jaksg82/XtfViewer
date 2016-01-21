Imports System.Globalization

Public Class XtfNotesHeader
    Public Property NumberBytesThisRecord As UInt32

    Shared ReadOnly Property HeaderType As Byte
        Get
            Return 1 'XTF_HEADER_NOTES
        End Get
    End Property
    Public Property SubChannelNumber As Byte 'xtfNoteSubChannel

    Public Property AnnotationTime As Date

    Dim inNotesText As String
    Public Property NotesText As String
        Get
            Return inNotesText
        End Get
        Set(value As String)
            If value Is Nothing Then value = " "c
            If String.IsNullOrEmpty(value) Then value = " "c
            If value.Length > 16 Then
                inNotesText = value.Substring(0, 16)
            Else
                inNotesText = value.PadRight(16)
            End If
        End Set
    End Property

    Public Sub New()
        'from base class
        SubChannelNumber = 0
        NumberBytesThisRecord = 256
        'this class
        AnnotationTime = Date.MinValue
        NotesText = "nd"

    End Sub

    Public Sub New(byteArray As Byte())
        Dim Year, Milliseconds, chkNumber As UInt16
        Dim Month, Day, Hour, Minutes, Seconds As Byte
        Dim TimeString As String

        'Default values
        SubChannelNumber = 0
        AnnotationTime = Date.MinValue
        NotesText = "nd"

        Using dp As New BinaryReader(ByteArrayToMemoryStream(byteArray))
            chkNumber = dp.ReadUInt16
            If chkNumber = XtfMainHeaderX36.MagicNumber Then
                dp.ReadByte() 'HeaderType
                SubChannelNumber = dp.ReadByte
                dp.ReadUInt16() 'Unused
                dp.ReadUInt16() 'Unused
                dp.ReadUInt32() 'NumBytesThisRecord
                Year = dp.ReadUInt16
                Month = dp.ReadByte
                Day = dp.ReadByte
                Hour = dp.ReadByte
                Minutes = dp.ReadByte
                Seconds = dp.ReadByte
                dp.ReadBytes(35) 'Unused
                NotesText = dp.ReadChars(200)
                'Compose the fix time value
                TimeString = Year.ToString("0000", CultureInfo.InvariantCulture)
                TimeString = TimeString & "-" & Month.ToString("00", CultureInfo.InvariantCulture)
                TimeString = TimeString & "-" & Day.ToString("00", CultureInfo.InvariantCulture)
                TimeString = TimeString & "-" & Hour.ToString("00", CultureInfo.InvariantCulture)
                TimeString = TimeString & "-" & Minutes.ToString("00", CultureInfo.InvariantCulture)
                TimeString = TimeString & "-" & Seconds.ToString("00", CultureInfo.InvariantCulture)
                TimeString = TimeString & "-" & Milliseconds.ToString("000", CultureInfo.InvariantCulture)
                If Date.TryParseExact(TimeString, "yyyy-MM-dd-HH-mm-ss-fff", CultureInfo.InvariantCulture, DateTimeStyles.None, AnnotationTime) Then
                    'parsed OK
                Else
                    AnnotationTime = Date.MinValue
                End If
            Else
                'something wrong
            End If
        End Using

    End Sub

End Class
