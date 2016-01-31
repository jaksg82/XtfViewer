Imports Windows.Storage

Public Module FileHelper

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
