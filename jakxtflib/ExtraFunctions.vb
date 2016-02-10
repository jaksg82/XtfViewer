Public Module ExtraFunctions

    Public Function ByteArrayToMemoryStream(ByteArray As Byte()) As IO.MemoryStream
        Dim mem As New MemoryStream()
        Try
            mem.Write(ByteArray, 0, ByteArray.Length)
            mem.Seek(0, SeekOrigin.Begin)
            ByteArrayToMemoryStream = mem
        Finally
            mem.Dispose()
        End Try

    End Function
End Module
