Public Module ExtensionMethod
    <Extension()>
    Public Async Function PickSingleFileWP8Async(filePicker As Pickers.FileOpenPicker) As Task(Of StorageFile)
        Dim tmpList = Await App.SingleFileSelectorHelper(filePicker)
        Dim selfile As StorageFile = Nothing

        If tmpList Is Nothing Then
            'No file selected
        Else
            If tmpList.Count >= 1 Then
                selFile = tmpList(0)
            End If
        End If

        Return selfile

    End Function

End Module
