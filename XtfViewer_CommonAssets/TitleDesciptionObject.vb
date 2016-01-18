Public Class TitleDescriptionObject
    Public Property Title As String
    Public Property Description As String

    Public Sub New()
        Title = "Title"
        Description = "Description"
    End Sub

    Public Sub New(TitleString As String, DescriptionString As String)
        Me.Title = TitleString
        Me.Description = DescriptionString
    End Sub
End Class
