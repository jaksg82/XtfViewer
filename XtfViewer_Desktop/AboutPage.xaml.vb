Class AboutPage
    Public Sub New()

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.

    End Sub

    Private Sub AboutPage_Initialized(sender As Object, e As EventArgs) Handles Me.Initialized
        AboutTitle.Text = String.Format(Globalization.CultureInfo.CurrentCulture, XtfViewer.AppStrings.AppAboutTitle, My.Application.Info.ProductName)
        AboutText.Text = String.Format(Globalization.CultureInfo.CurrentCulture, XtfViewer.AppStrings.AppAboutText, My.Application.Info.Version.ToString)

    End Sub
End Class
