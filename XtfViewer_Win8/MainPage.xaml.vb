Imports XtfViewerWin8.Common
Imports XtfViewerDataModel
Imports XtfViewerAppCommons

Public NotInheritable Class MainPage
    Inherits Page

    Private WithEvents _navigationHelper As New NavigationHelper(Me)
    Public Shared LoadedFileToken As String
    Public Shared SelectedGroup As Integer
    Public Shared ResStrings As New AppStrings

    ''' <summary>
    ''' A page that displays a grouped collection of items.
    ''' </summary>
    Public Sub New()
        InitializeComponent()

        NavigationCacheMode = NavigationCacheMode.Required

    End Sub

    Private Sub HubPage_Loaded(sender As Object, e As RoutedEventArgs) Handles Me.Loaded
        ContentSelector.ItemsSource = App.XtfData.GetGroupStrings

        If ContentSelector.SelectedIndex < 0 Then
            If SelectedGroup > ContentSelector.Items.Count - 1 Then
                SelectedGroup = 0
                ContentSelector.SelectedIndex = 0
            Else
                ContentSelector.SelectedIndex = SelectedGroup
            End If
        End If
        Dim LocalSets = ApplicationData.Current.LocalSettings
        LocalSets.Values("SelectedIndex") = SelectedGroup

        OpenButton.Label = ResStrings.AppOpenFile
        InfoButton.Label = ResStrings.AppAboutButton
        Frame.BackStack.Clear()

    End Sub

    Private Sub ContentSelector_SelectionChanged() Handles ContentSelector.SelectionChanged

        SelectedGroup = If(ContentSelector.SelectedIndex = -1, 0, ContentSelector.SelectedIndex)
        If SelectedGroup = 0 Then
            'Get the main header
            ContentLister.ItemsSource = App.XtfData.HeaderToObservableCollection
        Else
            If SelectedGroup <= App.XtfData.Header.ChannelInfo.Count Then
                'Get the channel info
                ContentLister.ItemsSource = App.XtfData.ChannelInfoToObservableCollection(SelectedGroup - 1)
            Else
                'Get the packet group info
                Dim PacketID As Byte
                Dim tmpStrings() As String
                'Retrieve the header type from the displayed string
                tmpStrings = ContentSelector.SelectedItem.ToString.Split(" "c)
                PacketID = CByte(tmpStrings(tmpStrings.Count - 1))
                ContentLister.ItemsSource = App.XtfData.GroupInfoToObservableCollection(PacketID)
            End If
        End If
        'Store the new value
        Dim LocalSets = ApplicationData.Current.LocalSettings
        LocalSets.Values("SelectedIndex") = SelectedGroup

    End Sub

    Private Async Sub OpenButton_Click() Handles OpenButton.Click
        ProgAnim.IsActive = True
        ProgAnim.Visibility = Visibility.Visible

        Dim openPicker As New Pickers.FileOpenPicker

        openPicker.ViewMode = Pickers.PickerViewMode.List
        openPicker.SuggestedStartLocation = Pickers.PickerLocationId.DocumentsLibrary
        openPicker.FileTypeFilter.Add(".xtf")

        Dim storageFiles = Await openPicker.PickSingleFileAsync
        'Dim selFile As StorageFile
        If storageFiles Is Nothing Then
            'No file selected
        Else
            Dim XtfStream As Stream = Await storageFiles.OpenStreamForReadAsync()
            Dim ind As New XtfIndex
            Dim LoadResult As Boolean
            LoadResult = Await ind.LoadFromXtfFileAsync(XtfStream)
            App.XtfData = ind
            XtfStream.Dispose()
            SelectedGroup = 0
            ContentSelector.ItemsSource = App.XtfData.GetGroupStrings
            ContentSelector.SelectedIndex = SelectedGroup
            LoadedFileToken = AccessCache.StorageApplicationPermissions.FutureAccessList.Add(storageFiles)
            'Store the info relative to the loaded file
            Dim AppDataLocal As StorageFolder = ApplicationData.Current.LocalFolder
            Dim sampleFile As StorageFile = Await AppDataLocal.CreateFileAsync("LoadedXtfIndex.xml", CreationCollisionOption.ReplaceExisting)
            Await FileIO.WriteTextAsync(sampleFile, App.XtfData.ToXmlDocument.ToString)
            Dim LocalSets = ApplicationData.Current.LocalSettings
            LocalSets.Values("SelectedIndex") = SelectedGroup
            LocalSets.Values("LoadedFileToken") = LoadedFileToken

        End If

        ProgAnim.IsActive = False
        ProgAnim.Visibility = Visibility.Collapsed

    End Sub


    Public Sub InfoButtonClick() Handles InfoButton.Click
        Frame.Navigate(GetType(AboutPage))

    End Sub


    ''' <summary>
    ''' Gets the <see cref="NavigationHelper"/> associated with this <see cref="Page"/>.
    ''' </summary>
    Public ReadOnly Property NavigationHelper As NavigationHelper
        Get
            Return _navigationHelper
        End Get
    End Property



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
