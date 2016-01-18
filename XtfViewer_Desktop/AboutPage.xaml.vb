Class AboutPage
    Public Sub New()

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
        AboutTitle.Text = String.Format(XtfViewer.AppStrings.AppAboutTitle, My.Application.Info.ProductName)
        AboutText.Text = String.Format(XtfViewer.AppStrings.AppAboutText, My.Application.Info.Version.ToString)

    End Sub

End Class
