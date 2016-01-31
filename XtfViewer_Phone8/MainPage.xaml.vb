﻿Imports XtfViewerPhone8.Common
Imports XtfViewerCommonAssets

Public NotInheritable Class MainPage
    Inherits Page

    Public Property TestDictionary As ObservableCollection(Of TitleDescriptionObject)
        Get
            Return CType(GetValue(Prop1Property), ObservableCollection(Of TitleDescriptionObject))
        End Get

        Set(ByVal value As ObservableCollection(Of TitleDescriptionObject))
            SetValue(Prop1Property, value)
        End Set
    End Property

    Public Shared ReadOnly Prop1Property As DependencyProperty =
                           DependencyProperty.Register("TestDictionary",
                           GetType(ObservableCollection(Of TitleDescriptionObject)), GetType(MainPage),
                           New PropertyMetadata(Nothing))



    Public Property AvailableGroups As ObservableCollection(Of String)
        Get
            Return CType(GetValue(AvailableGroupsProperty), ObservableCollection(Of String))
        End Get

        Set(ByVal value As ObservableCollection(Of String))
            SetValue(AvailableGroupsProperty, value)
        End Set
    End Property

    Public Shared ReadOnly AvailableGroupsProperty As DependencyProperty =
                           DependencyProperty.Register("AvailableGroups",
                           GetType(ObservableCollection(Of String)), GetType(MainPage),
                           New PropertyMetadata(New ObservableCollection(Of String)()))


    Public Property SampleData As XtfIndex
    Public Property AvailableHeaderTypes As XtfHeaderTypes
    Public Property LoadedFileToken As String

    Dim ResStrings As XtfViewerAppCommons.AppStrings
    Dim SelectedGroup As Integer

    Private WithEvents _navigationHelper As New NavigationHelper(Me)

    ''' <summary>
    ''' A page that displays a grouped collection of items.
    ''' </summary>
    Public Sub New()
        InitializeComponent()

        NavigationCacheMode = NavigationCacheMode.Required
        ResStrings = New XtfViewerAppCommons.AppStrings
        SelectedGroup = 0
        LoadedFileToken = ""
        SampleData = New XtfIndex

    End Sub

    Private Sub HubPage_Loaded(sender As Object, e As RoutedEventArgs) Handles Me.Loaded
        TestDictionary = SampleData.HeaderToObservableCollection
        AvailableGroups = SampleData.GetGroupStrings
        If ContentSelector.SelectedIndex < 0 Then
            If SelectedGroup > ContentSelector.Items.Count - 1 Then
                SelectedGroup = 0
                ContentSelector.SelectedIndex = 0
            Else
                ContentSelector.SelectedIndex = SelectedGroup
            End If
        End If
        OpenButton.Label = ResStrings.AppOpenFile
        InfoButton.Label = ResStrings.AppAboutButton

    End Sub

    Private Sub ContentSelector_SelectionChanged(sender As Object, e As SelectionChangedEventArgs) Handles ContentSelector.SelectionChanged
        If ContentSelector.SelectedIndex = SelectedGroup Then
            'Selection not changed
        Else
            SelectedGroup = If(ContentSelector.SelectedIndex = -1, 0, ContentSelector.SelectedIndex)
            If SelectedGroup = 0 Then
                'Get the main header
                TestDictionary = SampleData.HeaderToObservableCollection
            Else
                If SelectedGroup <= SampleData.Header.ChannelInfo.Count Then
                    'Get the channel info
                    TestDictionary = SampleData.ChannelInfoToObservableCollection(SelectedGroup - 1)
                Else
                    'Get the packet group info
                    Dim PacketID As Byte
                    Dim tmpStrings() As String
                    'Retrieve the header type from the displayed string
                    tmpStrings = ContentSelector.SelectedItem.ToString.Split(" "c)
                    PacketID = CByte(tmpStrings(tmpStrings.Count - 1))
                    TestDictionary = SampleData.GroupInfoToObservableCollection(PacketID)
                End If
            End If
        End If

    End Sub

    Private Async Sub OpenButton_Click(sender As Object, e As RoutedEventArgs) Handles OpenButton.Click
        ProgAnim.IsActive = True
        ProgAnim.Visibility = Visibility.Visible

        Dim openPicker As New Pickers.FileOpenPicker

        openPicker.ViewMode = Pickers.PickerViewMode.List
        openPicker.SuggestedStartLocation = Pickers.PickerLocationId.DocumentsLibrary
        openPicker.FileTypeFilter.Add(".xtf")

        Dim storageFiles = Await openPicker.PickSingleFileWP8Async
        'Dim selFile As StorageFile
        If storageFiles Is Nothing Then
            'No file selected
        Else
            Dim XtfStream As Stream = Await storageFiles.OpenStreamForReadAsync()
            Dim ind As New XtfIndex
            Dim LoadResult As Boolean
            LoadResult = Await ind.LoadFromXtfFileAsync(XtfStream)
            SampleData = ind
            XtfStream.Dispose()
            SelectedGroup = 0
            AvailableGroups = SampleData.GetGroupStrings
            ContentSelector.SelectedIndex = SelectedGroup
            LoadedFileToken = AccessCache.StorageApplicationPermissions.FutureAccessList.Add(storageFiles)

        End If

        ProgAnim.IsActive = False
        ProgAnim.Visibility = Visibility.Collapsed

    End Sub


    Public Sub InfoButtonClick() Handles InfoButton.Click
        Frame.Navigate(GetType(XtfViewerAppCommons.AboutPage))

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
    ''' Populates the page with content passed during navigation.  Any saved state is also
    ''' provided when recreating a page from a prior session.
    ''' </summary>
    ''' <param name="sender">
    ''' The source of the event; typically <see cref="NavigationHelper"/>.
    ''' </param>
    ''' <param name="e">Event data that provides both the navigation parameter passed to
    ''' <see cref="Frame.Navigate"/> when this page was initially requested and
    ''' a dictionary of state preserved by this page during an earlier
    ''' session. The state will be null the first time a page is visited.</param>
    Private Async Sub NavigationHelper_LoadState(sender As Object, e As LoadStateEventArgs) Handles _navigationHelper.LoadState
        'Create an appropriate data model for your problem domain to replace the sample data
        Dim AppDataLocal As StorageFolder = ApplicationData.Current.LocalFolder
        Dim AppLocalSettings = ApplicationData.Current.LocalSettings
        Dim FileExist As Boolean
        Try
            If AppLocalSettings.Values.ContainsKey("LoadedFileToken") Then
                Dim chkFile As String = CType(AppLocalSettings.Values("LoadedFileToken"), String)
                FileExist = Await isFilePresent(chkFile)
                If FileExist Then
                    Dim sampleFile As StorageFile = Await AppDataLocal.GetFileAsync("LoadedXtfIndex.xml")
                    Dim tmpstream As New MemoryStream
                    Dim enc As New Text.UTF8Encoding
                    Dim XmlString As String = Await FileIO.ReadTextAsync(sampleFile)
                    Dim arrBytData() As Byte = enc.GetBytes(XmlString)
                    tmpstream.Write(arrBytData, 0, arrBytData.Length)
                    tmpstream.Seek(0, SeekOrigin.Begin)
                    Await SampleData.LoadFromIndexFileAsync(tmpstream)
                    If AppLocalSettings.Values.ContainsKey("SelectedIndex") Then
                        ContentSelector.SelectedIndex = CType(AppLocalSettings.Values("SelectedIndex"), Integer)
                        SelectedGroup = ContentSelector.SelectedIndex
                    Else
                        ContentSelector.SelectedIndex = 0
                        SelectedGroup = 0
                    End If
                    tmpstream.Dispose()
                Else
                    SampleData = New XtfIndex
                    SelectedGroup = 0

                End If
            End If

        Catch e1 As Exception
            ' Timestamp not found
            SampleData = New XtfIndex
            SelectedGroup = 0

        End Try

    End Sub

    ''' <summary>
    ''' Preserves state associated with this page in case the application is suspended or the
    ''' page is discarded from the navigation cache.  Values must conform to the serialization
    ''' requirements of <see cref="SuspensionManager.SessionState"/>.
    ''' </summary>
    ''' <param name="sender">The source of the event; typically <see cref="NavigationHelper"/></param>
    ''' <param name="e">Event data that provides an empty dictionary to be populated with
    ''' serializable state.</param>
    Private Async Sub NavigationHelper_SaveState(sender As Object, e As SaveStateEventArgs) Handles _navigationHelper.SaveState
        'Save the unique state of the page here.
        Dim AppDataLocal As StorageFolder = ApplicationData.Current.LocalFolder
        Dim sampleFile As StorageFile = Await AppDataLocal.CreateFileAsync("LoadedXtfIndex.xml", CreationCollisionOption.ReplaceExisting)
        Await FileIO.WriteTextAsync(sampleFile, SampleData.ToXmlDocument.ToString)
        Dim LocalSets = ApplicationData.Current.LocalSettings
        LocalSets.Values("SelectedIndex") = ContentSelector.SelectedIndex.ToString
        LocalSets.Values("LoadedFileToken") = LoadedFileToken
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