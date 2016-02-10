Imports XtfViewerDataModel
Imports XtfViewerPhone8.Common
Imports XtfViewerAppCommons

''' <summary>
''' Pagina vuota che può essere utilizzata autonomamente oppure esplorata all'interno di un frame.
''' </summary>
Public NotInheritable Class LoadingPage
    Inherits Page

    Private WithEvents _navigationHelper As New NavigationHelper(Me)

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


    Private Async Sub LoadingPage_Loaded(sender As Object, e As RoutedEventArgs) Handles Me.Loaded
        'Create an appropriate data model for your problem domain to replace the sample data
        Dim AppDataLocal As StorageFolder = ApplicationData.Current.LocalFolder
        Dim AppLocalSettings = ApplicationData.Current.LocalSettings
        Dim FileExist As Boolean
        Try
            If AppLocalSettings.Values.ContainsKey("LoadedFileToken") Then
                Dim chkFile As String = CType(AppLocalSettings.Values("LoadedFileToken"), String)
                FileExist = Await IsFilePresent(chkFile)
                If FileExist Then
                    Dim sampleFile As StorageFile = Await AppDataLocal.GetFileAsync("LoadedXtfIndex.xml")
                    Dim tmpstream As New MemoryStream
                    Dim enc As New Text.UTF8Encoding
                    Dim XmlString As String = Await FileIO.ReadTextAsync(sampleFile)
                    Dim arrBytData() As Byte = enc.GetBytes(XmlString)
                    tmpstream.Write(arrBytData, 0, arrBytData.Length)
                    tmpstream.Seek(0, SeekOrigin.Begin)
                    Await App.XtfData.LoadFromIndexFileAsync(tmpstream)
                    If AppLocalSettings.Values.ContainsKey("SelectedIndex") Then
                        'ContentSelector.SelectedIndex = CType(AppLocalSettings.Values("SelectedIndex"), Integer)
                        MainPage.SelectedGroup = CType(AppLocalSettings.Values("SelectedIndex"), Integer)
                    Else
                        'ContentSelector.SelectedIndex = 0
                        MainPage.SelectedGroup = 0
                    End If
                    tmpstream.Dispose()
                Else
                    App.XtfData = New XtfIndex
                    MainPage.SelectedGroup = 0

                End If
            End If

        Catch e1 As Exception
            ' Timestamp not found
            App.XtfData = New XtfIndex
            MainPage.SelectedGroup = 0

        End Try
        Frame.Navigate(GetType(MainPage))
    End Sub



End Class
