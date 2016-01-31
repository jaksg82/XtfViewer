Imports XtfLibrary
Imports XtfViewerCommonAssets
Imports Windows.Storage
Imports System.Xml
Imports System.Xml.Serialization

Partial Public Class XtfIndexUwp
    Inherits XtfIndex

    Public Overloads Async Function LoadFromXtfFileAsync(XtfFile As StorageFile) As Task(Of Boolean)
        Dim tmpStream As New MemoryStream
        Dim ActualByte, TotalBytes As Long
        Dim XtfStream = Await XtfFile.OpenStreamForReadAsync

        Try
            XtfStream.CopyTo(tmpStream)
            XtfStream.Dispose()

            'TotalBytes = XtfStream.Length
            TotalBytes = tmpStream.Length

            'Clear the actual content
            Header = New XtfMainHeaderX36
            DataGroups.Clear()

            Using xtf As IO.BinaryReader = New IO.BinaryReader(tmpStream)
                'Make sure to start from the first byte
                xtf.BaseStream.Seek(0, IO.SeekOrigin.Begin)
                'Create the MainHeader object
                Dim header As New XtfMainHeaderX36(xtf.ReadBytes(3072))
                'Store the MainHeader
                Me.Header = header
                'Compute the true size of the MainHeader
                If header.ChannelInfo.Count <= 6 Then
                    ActualByte = 1024
                Else
                    If header.ChannelInfo.Count <= 14 Then
                        ActualByte = 2048
                    Else
                        ActualByte = 3072 'up to 22 channels
                    End If
                End If
                'Make sure to start after the MainHeader
                xtf.BaseStream.Seek(ActualByte - 1, IO.SeekOrigin.Begin)
                'Parse the file looking for the packets
                Do Until TotalBytes - ActualByte < 257
                    Dim snif As New XtfPacketSniffer(xtf.ReadBytes(257))
                    If snif.MagicNumber = XtfMainHeaderX36.MagicNumber Then
                        Dim gid As Integer = Integer.MinValue
                        If DataGroups.Count >= 1 Then
                            For i = 0 To DataGroups.Count - 1
                                If DataGroups.Item(i).HeaderType = snif.HeaderType Then
                                    gid = i
                                    Exit For
                                End If
                            Next
                        End If
                        If gid <> Integer.MinValue Then
                            DataGroups(gid).Packets.Add(snif)
                        Else
                            DataGroups.Add(New PacketGroup(snif.HeaderType))
                            For i = 0 To DataGroups.Count - 1
                                If DataGroups.Item(i).HeaderType = snif.HeaderType Then
                                    gid = i
                                    Exit For
                                End If
                            Next
                            DataGroups(gid).Packets.Add(snif)
                        End If

                        'Update the position
                        ActualByte = ActualByte + snif.NumberBytesThisRecord - 1
                        xtf.BaseStream.Seek(ActualByte - 1, IO.SeekOrigin.Begin)
                    Else
                        ActualByte = ActualByte + 1
                        xtf.BaseStream.Seek(ActualByte - 1, IO.SeekOrigin.Begin)
                    End If

                Loop

            End Using
            tmpStream.Dispose()
        Catch ex As Exception
            tmpStream.Dispose()
            XtfStream.Dispose()
        End Try


        Return True

    End Function

    Public Overloads Function ToXmlDocument() As XDocument
        Dim result As New XDocument
        Dim xmlSet As New XmlWriterSettings
        xmlSet.Indent = True
        xmlSet.CheckCharacters = True

        Using writer = result.CreateWriter()
            Dim s As XmlSerializer = New XmlSerializer(GetType(XtfIndexUwp))
            s.Serialize(XmlWriter.Create(writer, xmlSet), Me)
        End Using
        Return result
    End Function

End Class
