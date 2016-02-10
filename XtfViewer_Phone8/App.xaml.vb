Imports XtfViewerPhone8.Common

<Assembly: CLSCompliant(False)>

''' <summary>
''' Provides application-specific behavior to supplement the default Application class.
''' </summary>
Public NotInheritable Class App
    Inherits Application

    Private _transitions As TransitionCollection

    ''' <summary>
    ''' Initializes the singleton application object. This is the first line of authored code
    ''' executed, and as such is the logical equivalent of main() or WinMain().
    ''' </summary>
    Public Sub New()
        InitializeComponent()
    End Sub

    ''' <summary>
    ''' Invoked when the application is launched normally by the end user. Other entry points
    ''' will be used when the application is launched to open a specific file, to display
    ''' search results, and so forth.
    ''' </summary>
    ''' <param name="e">Details about the launch request and process.</param>
    Protected Overrides Async Sub OnLaunched(e As LaunchActivatedEventArgs)

#If DEBUG Then
        If System.Diagnostics.Debugger.IsAttached Then
            DebugSettings.EnableFrameRateCounter = True
        End If
#End If

        Dim rootFrame As Frame = TryCast(Window.Current.Content, Frame)

        ' Do not repeat app initialization when the Window already has content,
        ' just ensure that the window is active.
        If rootFrame Is Nothing Then
            ' Create a Frame to act as the navigation context and navigate to the first page
            rootFrame = New Frame()

            ' Associate the frame with a SuspensionManager key.
            SuspensionManager.RegisterFrame(rootFrame, "AppFrame")

            ' TODO: Change this value to a cache size that is appropriate for your application.
            rootFrame.CacheSize = 1

            If e.PreviousExecutionState = ApplicationExecutionState.Terminated Then
                ' Restore the saved session state only when appropriate.
                Try
                    Await SuspensionManager.RestoreAsync()
                Catch ex As SuspensionManagerException
                    ' Something went wrong restoring state.
                    ' Assume there is no state and continue.
                End Try
            End If

            ' Place the frame in the current Window
            Window.Current.Content = rootFrame
        End If

        If rootFrame.Content Is Nothing Then
            ' Removes the turnstile navigation for startup.
            If rootFrame.ContentTransitions IsNot Nothing Then
                _transitions = New TransitionCollection()
                For Each transition As Transition In rootFrame.ContentTransitions
                    _transitions.Add(transition)
                Next
            End If

            rootFrame.ContentTransitions = Nothing
            AddHandler rootFrame.Navigated, AddressOf RootFrame_FirstNavigated

            ' When the navigation stack isn't restored navigate to the first page,
            ' configuring the new page by passing required information as a navigation
            ' parameter
            If Not rootFrame.Navigate(GetType(LoadingPage), e.Arguments) Then
                Throw New Exception("Failed to create initial page")
            End If
        End If

        ' Ensure the current window is active
        Window.Current.Activate()
    End Sub

    ''' <summary>
    ''' Restores the content transitions after the app has launched.
    ''' </summary>
    Private Sub RootFrame_FirstNavigated(sender As Object, e As NavigationEventArgs)
        Dim newTransitions As TransitionCollection
        If _transitions Is Nothing Then
            newTransitions = New TransitionCollection()
            newTransitions.Add(New NavigationThemeTransition())
        Else
            newTransitions = _transitions
        End If

        Dim rootFrame As Frame = DirectCast(sender, Frame)
        rootFrame.ContentTransitions = newTransitions
        RemoveHandler rootFrame.Navigated, AddressOf RootFrame_FirstNavigated
    End Sub

    ''' <summary>
    ''' Invoked when application execution is being suspended. Application state is saved
    ''' without knowing whether the application will be terminated or resumed with the contents
    ''' of memory still intact.
    ''' </summary>
    Private Async Sub OnSuspending(sender As Object, e As SuspendingEventArgs) Handles Me.Suspending
        Dim deferral As SuspendingDeferral = e.SuspendingOperation.GetDeferral()
        Await SuspensionManager.SaveAsync()
        deferral.Complete()
    End Sub


    Public Shared filePickerFiles As IReadOnlyList(Of StorageFile)
    Public folderPickerFolder As StorageFolder
    Public Shared XtfData As New XtfViewerDataModel.XtfIndex

    Protected Overrides Sub OnActivated(args As IActivatedEventArgs)
        'Me.OnActivated(args)

        If args.Kind = ActivationKind.PickFileContinuation Then
            Dim newargs As FileOpenPickerContinuationEventArgs = CType(args, FileOpenPickerContinuationEventArgs)
            filePickerFiles = newargs.Files

        End If

    End Sub

    Public Shared Async Function SingleFileSelectorHelper(filePicker As Pickers.FileOpenPicker) As Task(Of IReadOnlyList(Of StorageFile))

        filePickerFiles = Nothing
        Dim pickerOpen As Boolean = False
        While filePickerFiles Is Nothing

            If Not pickerOpen Then
                pickerOpen = True
                filePicker.PickSingleFileAndContinue()
            End If

            Await Task.Delay(New TimeSpan(0, 0, 0, 0, 200))
        End While

        Return filePickerFiles
    End Function

    'Public Event BackRequested As EventHandler(Of BackPressedEventArgs)

    Public Sub AppBackRequested() 'Handles Me.BackRequested
        Dim rootFrame As Frame = TryCast(Window.Current.Content, Frame)
        Dim ActivePage As XtfViewerAppCommons.AboutPage = TryCast(rootFrame.Content, XtfViewerAppCommons.AboutPage)

        If ActivePage IsNot Nothing Then
            rootFrame.Navigate(GetType(MainPage))
        End If

    End Sub

End Class