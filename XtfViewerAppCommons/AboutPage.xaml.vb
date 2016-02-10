Imports Windows.UI.Xaml.Navigation
Imports XtfViewerAppCommons.Common

''' <summary>
''' A basic page that provides characteristics common to most applications.
''' </summary>
Public NotInheritable Class AboutPage
    Inherits Page

    Public Property PageStrings As AppStrings

    Private WithEvents _navigationHelper As New NavigationHelper(Me)
    Private ReadOnly _defaultViewModel As New ObservableDictionary()

    Public Sub New()

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.

    End Sub

    Private Sub AboutPage_Loaded(sender As Object, e As RoutedEventArgs) Handles Me.Loaded
        PageStrings = New AppStrings

        AppTitle.Text = Package.Current.Id.Name
        AboutTitle.Text = PageStrings.AppAboutTitle

        Dim pkgVer(3) As String
        pkgVer(0) = Package.Current.Id.Version.Major.ToString(Globalization.CultureInfo.CurrentCulture)
        pkgVer(1) = Package.Current.Id.Version.Minor.ToString(Globalization.CultureInfo.CurrentCulture)
        pkgVer(2) = Package.Current.Id.Version.Build.ToString(Globalization.CultureInfo.CurrentCulture)
        pkgVer(3) = Package.Current.Id.Version.Revision.ToString(Globalization.CultureInfo.CurrentCulture)

        AboutText.Text = String.Format(Globalization.CultureInfo.CurrentCulture, PageStrings.AppAboutText, pkgVer)
        WebLink.Content = PageStrings.AppAboutVisitWeb
        WebLink.NavigateUri = New Uri(PageStrings.AppAboutWebLink)

    End Sub

    Private Sub BackButton_Click(sender As Object, e As RoutedEventArgs) Handles BackButton.Click
        Frame.GoBack()
    End Sub

    Private Sub Grid_KeyUp(sender As Object, e As KeyRoutedEventArgs) Handles Me.KeyUp
        If e.Key = Windows.System.VirtualKey.Back Or e.Key = Windows.System.VirtualKey.GoBack Then
            Frame.GoBack()
        End If
    End Sub


    ''' <summary>
    ''' Gets the <see cref="NavigationHelper"/> associated with this <see cref="Page"/>.
    ''' </summary>
    Public ReadOnly Property NavigationHelper As NavigationHelper
        Get
            Return _navigationHelper
        End Get
    End Property

    ''' <summary>
    ''' Gets the view model for this <see cref="Page"/>.
    ''' This can be changed to a strongly typed view model.
    ''' </summary>
    Public ReadOnly Property DefaultViewModel As ObservableDictionary
        Get
            Return _defaultViewModel
        End Get
    End Property

    ''' <summary>
    ''' Populates the page with content passed during navigation. Any saved state is also
    ''' provided when recreating a page from a prior session.
    ''' </summary>
    ''' <param name="sender">
    ''' The source of the event; typically <see cref="NavigationHelper"/>.
    ''' </param>
    ''' <param name="e">Event data that provides both the navigation parameter passed to
    ''' <see cref="Frame.Navigate"/> when this page was initially requested and
    ''' a dictionary of state preserved by this page during an earlier.
    ''' session. The state will be null the first time a page is visited.</param>
    Private Sub NavigationHelper_LoadState(sender As Object, e As LoadStateEventArgs) Handles _navigationHelper.LoadState
        ' TODO: Load the saved state of the page here.
    End Sub

    ''' <summary>
    ''' Preserves state associated with this page in case the application is suspended or the
    ''' page is discarded from the navigation cache.  Values must conform to the serialization
    ''' requirements of <see cref="SuspensionManager.SessionState"/>.
    ''' </summary>
    ''' <param name="sender">The source of the event; typically <see cref="NavigationHelper"/></param>
    ''' <param name="e">Event data that provides an empty dictionary to be populated with
    ''' serializable state.</param>
    Private Sub NavigationHelper_SaveState(sender As Object, e As SaveStateEventArgs) Handles _navigationHelper.SaveState
        ' TODO: Save the unique state of the page here.
    End Sub

#Region "NavigationHelper registration"

    ''' <summary>
    ''' The methods provided in this section are simply used to allow
    ''' NavigationHelper to respond to the page's navigation methods.
    ''' <para>
    ''' Page specific logic should be placed in event handlers for the
    ''' <see cref="NavigationHelper.LoadState"/>
    ''' and <see cref="NavigationHelper.SaveState"/>.
    ''' The navigation parameter is available in the LoadState method
    ''' in addition to page state preserved during an earlier session.
    ''' </para>
    ''' </summary>
    ''' <param name="e">Event data that describes how this page was reached.</param>
    Protected Overrides Sub OnNavigatedTo(e As NavigationEventArgs)
        _navigationHelper.OnNavigatedTo(e)
    End Sub

    Protected Overrides Sub OnNavigatedFrom(e As NavigationEventArgs)
        _navigationHelper.OnNavigatedFrom(e)
    End Sub

#End Region

End Class
