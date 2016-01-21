
Class MainWindow

    Public Property TestDictionary As ObjectModel.ObservableCollection(Of XtfViewerCommonAssets.TitleDescriptionObject)
        Get
            Return CType(GetValue(Prop1Property), Global.System.Collections.ObjectModel.ObservableCollection(Of XtfViewerCommonAssets.TitleDescriptionObject))
        End Get

        Set(ByVal value As ObjectModel.ObservableCollection(Of XtfViewerCommonAssets.TitleDescriptionObject))
            SetValue(Prop1Property, value)
        End Set
    End Property

    Public Shared ReadOnly Prop1Property As DependencyProperty =
                           DependencyProperty.Register("TestDictionary",
                           GetType(ObjectModel.ObservableCollection(Of XtfViewerCommonAssets.TitleDescriptionObject)), GetType(MainWindow),
                           New PropertyMetadata(Nothing))



    Public Property AvailableGroups As ObjectModel.ObservableCollection(Of String)
        Get
            Return CType(GetValue(AvailableGroupsProperty), Global.System.Collections.ObjectModel.ObservableCollection(Of String))
        End Get

        Set(ByVal value As ObjectModel.ObservableCollection(Of String))
            SetValue(AvailableGroupsProperty, value)
        End Set
    End Property

    Public Shared ReadOnly AvailableGroupsProperty As DependencyProperty =
                           DependencyProperty.Register("AvailableGroups",
                           GetType(ObjectModel.ObservableCollection(Of String)), GetType(MainWindow),
                           New PropertyMetadata(New ObjectModel.ObservableCollection(Of String)()))


    Public Property SampleData As XtfViewerCommonAssets.XtfIndex
    Public Property AvailableHeaderTypes As XtfViewerCommonAssets.XtfHeaderTypes
    Public Property BaseDirectory As String
    Public Property SampleIndexPath As String
    Public Property HeaderTypesPath As String
    Public Property SonarTypesPath As String

    Dim SelectedGroup As Integer
    Dim SampleIndexIsAvailable, HeaderTypesIsAvailable, SonarTypesIsAvailable As Boolean
    Dim LastDir As String

    Public Sub New()
        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
        BaseDirectory = AppDomain.CurrentDomain.BaseDirectory
        SampleIndexPath = BaseDirectory & "DataModel\SampleData.XtfIndex"
        HeaderTypesPath = BaseDirectory & "DataModel\xtfHeaderTypes.xml"
        SonarTypesPath = BaseDirectory & "DataModel\xtfSonarTypes.xml"
        SampleIndexIsAvailable = IO.File.Exists(SampleIndexPath)
        HeaderTypesIsAvailable = IO.File.Exists(HeaderTypesPath)
        SonarTypesIsAvailable = IO.File.Exists(SonarTypesPath)
        SelectedGroup = 0
        OpenButton.Content = XtfViewer.AppStrings.AppOpenFile
        InfoButton.Content = XtfViewer.AppStrings.AppInfoButton
        AboutFrame.Visibility = Visibility.Collapsed
        LastDir = Environment.GetFolderPath(Environment.SpecialFolder.Desktop)

        SampleData = New XtfViewerCommonAssets.XtfIndex
        LoadData()

        TestDictionary = SampleData.HeaderToObservableCollection


    End Sub

    Private Async Sub LoadData()
        If SampleIndexIsAvailable Then
            'Load the sample data
            Dim ind As New XtfViewerCommonAssets.XtfIndex
            Dim XtfStream As New IO.FileStream(SampleIndexPath, IO.FileMode.Open)
            Dim LoadResult As Boolean
            LoadResult = Await ind.LoadFromIndexFileAsync(XtfStream)
            SampleData = ind
            XtfStream.Dispose()
        Else
            SampleData = New XtfViewerCommonAssets.XtfIndex
        End If

        If HeaderTypesIsAvailable Then
            'Load the sample data
            Dim hdr As New XtfViewerCommonAssets.XtfHeaderTypes
            Dim XmlHdr As New IO.FileStream(HeaderTypesPath, IO.FileMode.Open)
            Dim LoadResult As Boolean
            LoadResult = Await hdr.LoadFromFileAsync(XmlHdr)
            AvailableHeaderTypes = hdr
            XmlHdr.Dispose()
        Else
            AvailableHeaderTypes = New XtfViewerCommonAssets.XtfHeaderTypes
        End If

        TestDictionary = SampleData.HeaderToObservableCollection

        'Fill the AvailableGroup property
        AvailableGroups.Clear()
        AvailableGroups.Add(XtfViewer.AppStrings.AppAvailGroupHeader)
        For g = 0 To SampleData.Header.ChannelInfo.Count - 1
            AvailableGroups.Add(String.Format(XtfViewer.AppStrings.AppAvailGroupChannelInfo, g))
        Next
        For p = 0 To SampleData.DataGroups.Count - 1
            'TODO: Retrieve the header type of each group
            AvailableGroups.Add(String.Format(XtfViewer.AppStrings.AppAvailGroupPacketGroup, SampleData.DataGroups(p).HeaderType.ToString))
        Next
        ContentSelector.SelectedIndex = SelectedGroup

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
        Dim opdiag As New Microsoft.Win32.OpenFileDialog
        opdiag.CheckFileExists = True
        opdiag.DefaultExt = "xtf"
        opdiag.Filter = "Extended Triton Format (*.xtf)|*.xtf"
        opdiag.FilterIndex = 1
        opdiag.Multiselect = False
        opdiag.InitialDirectory = LastDir

        If opdiag.ShowDialog Then
            'Load the sample data
            Dim ind As New XtfViewerCommonAssets.XtfIndex
            Dim XtfStream As New IO.FileStream(opdiag.FileName, IO.FileMode.Open)
            Dim SelDir As New IO.FileInfo(opdiag.FileName)
            LastDir = SelDir.DirectoryName
            Dim LoadResult As Boolean
            LoadResult = Await ind.LoadFromXtfFileAsync(XtfStream)
            SampleData = ind
            XtfStream.Dispose()
            SelectedGroup = 0
            'Fill the AvailableGroup property
            AvailableGroups.Clear()
            AvailableGroups.Add(XtfViewer.AppStrings.AppAvailGroupHeader)
            For g = 0 To SampleData.Header.ChannelInfo.Count - 1
                AvailableGroups.Add(String.Format(XtfViewer.AppStrings.AppAvailGroupChannelInfo, g))
            Next
            For p = 0 To SampleData.DataGroups.Count - 1
                AvailableGroups.Add(String.Format(XtfViewer.AppStrings.AppAvailGroupPacketGroup, SampleData.DataGroups(p).HeaderType.ToString))
            Next
            ContentSelector.SelectedIndex = SelectedGroup

        Else
            'Do nothing
        End If

    End Sub

    Public Sub InfoButtonClick() Handles InfoButton.Click, AboutFrame.MouseLeftButtonUp
        If AboutFrame.Visibility = Visibility.Visible Then
            AboutFrame.Visibility = Visibility.Collapsed
        Else
            AboutFrame.Visibility = Visibility.Visible
        End If

    End Sub
End Class
