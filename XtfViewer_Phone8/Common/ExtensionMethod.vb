Public Module ExtensionMethod
    <Extension()>
    Public Async Function PickSingleFileWP8Async(filePicker As Pickers.FileOpenPicker) As Task(Of IReadOnlyList(Of StorageFile))

        Return Await App.SingleFileSelectorHelper(filePicker)

    End Function

End Module
