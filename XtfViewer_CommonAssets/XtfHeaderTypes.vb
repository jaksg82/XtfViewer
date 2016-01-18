Imports System.Xml.Serialization
Imports System.Xml

Public Class HeaderEntry
    Public Property Value As Byte
    Public Property Name As String
    Public Property Description As String

    Public Sub New()
        Me.Value = 0
        Me.Name = "XtfHeaderSonar"
        Me.Description = "Sidescan And Subbottom"
    End Sub

    Public Sub New(NewValue As Byte, NewName As String, NewDescription As String)
        Me.Value = NewValue
        Me.Name = NewName
        Me.Description = NewDescription
    End Sub

End Class

Public Class XtfHeaderTypes
    Public ReadOnly Property KnownTypes As ObservableCollection(Of HeaderEntry)

    Public Sub New()
        Me.KnownTypes = New ObservableCollection(Of HeaderEntry)()
    End Sub

    Public Async Function LoadFromFileAsync(HeaderEnumFile As Stream) As Task(Of Boolean)
        Using stream As MemoryStream = New MemoryStream
            Await HeaderEnumFile.CopyToAsync(stream)
            stream.Seek(0, SeekOrigin.Begin)
            Dim s As XmlSerializer = New XmlSerializer(GetType(XtfIndex))
            Dim o As Object = s.Deserialize(XmlReader.Create(stream))
            If o.GetType() Is Me.GetType() Then
                Dim index As XtfHeaderTypes
                index = CType(o, XtfHeaderTypes)
                For Each k In index.KnownTypes
                    KnownTypes.Add(k)
                Next

            End If
        End Using
        Return True
    End Function

    Public Async Function SaveToFileAsync() As Task(Of Stream)
        Dim result As New IO.MemoryStream
        Dim xmlSet As New XmlWriterSettings
        xmlSet.Indent = True
        xmlSet.CheckCharacters = True

        Using stream As New MemoryStream()
            Dim s As XmlSerializer = New XmlSerializer(GetType(XtfHeaderTypes))
            s.Serialize(XmlWriter.Create(stream, xmlSet), Me)
            stream.Flush()
            stream.Seek(0, SeekOrigin.Begin)
            Await stream.CopyToAsync(result)
        End Using
        Return result
    End Function

End Class
