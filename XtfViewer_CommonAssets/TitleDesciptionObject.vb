''' <summary>
''' Base object for the list view item template
''' </summary>
Public Class TitleDescriptionObject
    Public Property Title As String
    Public Property Description As String

    Public Sub New()
        Title = "Title"
        Description = "Description"
    End Sub

    ''' <summary>
    ''' Create a new Title/Description object
    ''' </summary>
    ''' <param name="value1">Title</param>
    ''' <param name="value2">Description</param>
    Public Sub New(value1 As String, value2 As String)
        Me.Title = value1
        Me.Description = value2
    End Sub
End Class
