Public Module ExtensionMethod
    <Extension()>
    Public Async Function PickSingleFileWP8Async(filePicker As Pickers.FileOpenPicker) As Task(Of StorageFile)
        Dim tmpList = Await App.SingleFileSelectorHelper(filePicker)
        Dim selfile As StorageFile

        If tmpList Is Nothing Then
            'No file selected
        Else
            If tmpList.Count >= 1 Then
                selFile = tmpList(0)
            End If
        End If

        Return selfile

    End Function


    Public Async Function IsFilePresent(fileToken As String) As Task(Of Boolean)

        Dim fileExists As Boolean = True
        Dim fileStream As Stream
        Dim file As StorageFile

        Try

            file = Await AccessCache.StorageApplicationPermissions.FutureAccessList.GetFileAsync(fileToken)
            fileStream = Await file.OpenStreamForReadAsync()
            fileStream.Dispose()

        Catch e1 As FileNotFoundException

            '// If the file dosn't exits it throws an exception, make fileExists false in this case 
            fileExists = False

        Finally

            If fileStream IsNot Nothing Then

                fileStream.Dispose()
            End If

        End Try

        Return fileExists

    End Function

End Module
