Imports XtfLibrary
Imports System.Xml.Serialization
Imports System.Xml

<Assembly: CLSCompliant(False)>
<Assembly: Runtime.InteropServices.ComVisible(False)>

Public Class XtfIndex
    Public Property Header As XtfMainHeaderX36
    Public ReadOnly Property DataGroups As ObservableCollection(Of PacketGroup)

    Public Sub New()
        Header = New XtfMainHeaderX36
        DataGroups = New ObservableCollection(Of PacketGroup)()
    End Sub

    Public Async Function LoadFromXtfFileAsync(XtfFile As Stream) As Task(Of Boolean)
        Dim ActualByte, TotalBytes As Long

        TotalBytes = XtfFile.Length

        'Clear the actual content
        Header = New XtfMainHeaderX36
        DataGroups.Clear()

        Using xtf As IO.BinaryReader = New IO.BinaryReader(XtfFile)
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
        Return True
    End Function

    Public Async Function LoadFromIndexFileAsync(IndexFile As Stream) As Task(Of Boolean)
        Using stream As MemoryStream = New MemoryStream
            Await IndexFile.CopyToAsync(stream)
            stream.Seek(0, SeekOrigin.Begin)
            Dim s As XmlSerializer = New XmlSerializer(GetType(XtfIndex))
            Dim o As Object = s.Deserialize(XmlReader.Create(stream))
            If o.GetType() Is [GetType]() Then
                Dim index As XtfIndex
                index = CType(o, XtfIndex)
                Header = index.Header
                For Each dg In index.DataGroups
                    DataGroups.Add(dg)
                Next
            End If
        End Using
        Return True
    End Function

    Public Async Function SaveToIndexFileAsync() As Task(Of Stream)
        Dim result As New IO.MemoryStream
        Dim xmlSet As New XmlWriterSettings
        xmlSet.Indent = True
        xmlSet.CheckCharacters = True

        Using stream As New MemoryStream()
            Dim s As XmlSerializer = New XmlSerializer(GetType(XtfIndex))
            s.Serialize(XmlWriter.Create(stream, xmlSet), Me)
            stream.Flush()
            stream.Seek(0, SeekOrigin.Begin)
            Await stream.CopyToAsync(result)
        End Using
        Return result
    End Function


End Class

