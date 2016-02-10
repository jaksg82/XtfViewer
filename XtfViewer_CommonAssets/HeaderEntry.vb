''' <summary>
''' Represent the touple of information contained in the XtfHeaderTypes and XtfSonarTypes classes
''' </summary>
Public Class HeaderEntry
    Public Property Value As Byte
    Public Property Name As String
    Public Property Description As String

    Public Sub New()
        Me.Value = 0
        Me.Name = "None"
        Me.Description = "Default"
    End Sub

    Public Sub New(NewValue As Byte, NewName As String, NewDescription As String)
        Me.Value = NewValue
        Me.Name = NewName
        Me.Description = NewDescription
    End Sub

End Class
