' The Section Page template is documented at http://go.microsoft.com/fwlink/?LinkId=234229

''' <summary>
''' A page that displays an overview of a single group, including a preview of the items
''' within the group.
''' </summary>
Public NotInheritable Class SectionPage
    Inherits Page

    ''' <summary>
    ''' NavigationHelper is used on each page to aid in navigation and 
    ''' process lifetime management
    ''' </summary>
    Public ReadOnly Property NavigationHelper As Common.NavigationHelper
        Get
            Return Me._navigationHelper
        End Get
    End Property
    Private _navigationHelper As Common.NavigationHelper

    ''' <summary>
    ''' This can be changed to a strongly typed view model.
    ''' </summary>
    Public ReadOnly Property DefaultViewModel As Common.ObservableDictionary
        Get
            Return Me._defaultViewModel
        End Get
    End Property
    Private _defaultViewModel As New Common.ObservableDictionary()


    Public Sub New()
        InitializeComponent()
        Me._navigationHelper = New Common.NavigationHelper(Me)
        AddHandler Me._navigationHelper.LoadState,
            AddressOf NavigationHelper_LoadState
    End Sub

    ''' <summary>
    ''' Populates the page with content passed during navigation.  Any saved state is also
    ''' provided when recreating a page from a prior session.
    ''' </summary>
    ''' <param name="sender">
    ''' The source of the event; typically <see cref="NavigationHelper"/>
    ''' </param>
    ''' <param name="e">Event data that provides both the navigation parameter passed to
    ''' <see cref="Frame.Navigate"/> when this page was initially requested and
    ''' a dictionary of state preserved by this page during an earlier
    ''' session.  The state will be null the first time a page is visited.</param>
    Private Async Sub NavigationHelper_LoadState(sender As Object, e As Common.LoadStateEventArgs)
        ' TODO: Create an appropriate data model for your problem domain to replace the sample data
        Dim group As Data.SampleDataGroup = Await Data.SampleDataSource.GetGroupAsync(DirectCast(e.NavigationParameter, String))
        Me.DefaultViewModel("Group") = group
        Me.DefaultViewModel("Items") = group.Items
    End Sub

    ''' <summary>
    ''' Invoked when an item is clicked.
    ''' </summary>
    ''' <param name="sender">The GridView (or ListView when the application is snapped)
    ''' displaying the item clicked.</param>
    ''' <param name="e">Event data that describes the item clicked.</param>
    Private Sub ItemView_ItemClick(sender As Object, e As ItemClickEventArgs)

        ' Navigate to the appropriate destination page, configuring the new page
        ' by passing required information as a navigation parameter
        Dim itemId As String = DirectCast(e.ClickedItem, Data.SampleDataItem).UniqueId
        Me.Frame.Navigate(GetType(ItemPage), itemId)
    End Sub

#Region "NavigationHelper registration"

    ''' The methods provided in this section are simply used to allow
    ''' NavigationHelper to respond to the page's navigation methods.
    ''' 
    ''' Page specific logic should be placed in event handlers for the  
    ''' <see cref="Common.NavigationHelper.LoadState"/>
    ''' and <see cref="Common.NavigationHelper.SaveState"/>.
    ''' The navigation parameter is available in the LoadState method 
    ''' in addition to page state preserved during an earlier session.

    Protected Overrides Sub OnNavigatedTo(e As NavigationEventArgs)
        _navigationHelper.OnNavigatedTo(e)
    End Sub

    Protected Overrides Sub OnNavigatedFrom(e As NavigationEventArgs)
        _navigationHelper.OnNavigatedFrom(e)
    End Sub

#End Region

End Class
