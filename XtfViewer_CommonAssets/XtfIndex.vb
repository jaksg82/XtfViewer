Imports XtfLibrary
Imports System.Xml.Serialization
Imports System.Xml

<Assembly: CLSCompliant(False)>
<Assembly: Runtime.InteropServices.ComVisible(False)>
Public Class PacketGroup
    Public Property HeaderType As Byte
    Public ReadOnly Property Packets As ObservableCollection(Of XtfPacketSniffer)

    Public Sub New()
        HeaderType = 0
        Packets = New ObservableCollection(Of XtfPacketSniffer)()
    End Sub

    Public Sub New(GroupType As Byte)
        HeaderType = GroupType
        Packets = New ObservableCollection(Of XtfPacketSniffer)()
    End Sub

End Class

Public Class XtfIndex
    Public Property Header As XtfMainHeaderX36
    Public ReadOnly Property DataGroups As ObservableCollection(Of PacketGroup)

    Public Sub New()
        Header = New XtfMainHeaderX36
        DataGroups = New ObservableCollection(Of PacketGroup)()
    End Sub

    Public Async Function LoadFromXtfFileAsync(XtfFile As Stream) As Task(Of Boolean)
        Dim InvCult As New Globalization.CultureInfo("en")
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

    Public Function ToXmlDocument() As XDocument
        Dim result As New XDocument
        Dim xmlSet As New XmlWriterSettings
        xmlSet.Indent = True
        xmlSet.CheckCharacters = True

        Using writer = result.CreateWriter()
            Dim s As XmlSerializer = New XmlSerializer(GetType(XtfIndex))
            s.Serialize(XmlWriter.Create(writer, xmlSet), Me)
        End Using
        Return result
    End Function

    Public Function HeaderToObservableCollection() As ObservableCollection(Of TitleDescriptionObject)
        Dim tmpColl As New ObservableCollection(Of TitleDescriptionObject)
        Dim CurrCult As Globalization.CultureInfo = Globalization.CultureInfo.CurrentCulture

        tmpColl.Add(New TitleDescriptionObject(My.Strings.Strings.XtfHeaderFileFormat, Header.FileFormat.ToString(CurrCult)))
        tmpColl.Add(New TitleDescriptionObject(My.Strings.Strings.XtfHeaderSystemType, Header.SystemType.ToString(CurrCult)))
        tmpColl.Add(New TitleDescriptionObject(My.Strings.Strings.XtfHeaderRecordingProgramName, Header.RecordingProgramName))
        tmpColl.Add(New TitleDescriptionObject(My.Strings.Strings.XtfHeaderRecordingProgramVersion, Header.RecordingProgramVersion))
        tmpColl.Add(New TitleDescriptionObject(My.Strings.Strings.XtfHeaderSonarName, Header.SonarName))
        tmpColl.Add(New TitleDescriptionObject(My.Strings.Strings.XtfHeaderSonarType, Header.SonarType.ToString(CurrCult)))
        tmpColl.Add(New TitleDescriptionObject(My.Strings.Strings.XtfHeaderNoteString, Header.NoteString))
        tmpColl.Add(New TitleDescriptionObject(My.Strings.Strings.XtfHeaderThisFileName, Header.ThisFileName))
        tmpColl.Add(New TitleDescriptionObject(My.Strings.Strings.XtfHeaderNavigationCoordinateUnits, Header.NavigationCoordinateUnits.ToString(CurrCult)))
        tmpColl.Add(New TitleDescriptionObject(My.Strings.Strings.XtfHeaderNumberOfSonarChannels, Header.NumberOfSonarChannels.ToString(CurrCult)))
        tmpColl.Add(New TitleDescriptionObject(My.Strings.Strings.XtfHeaderNumberOfBathymetryChannels, Header.NumberOfBathymetryChannels.ToString(CurrCult)))
        tmpColl.Add(New TitleDescriptionObject(My.Strings.Strings.XtfHeaderNumberOfSnippetChannels, Header.NumberOfSnippetChannels.ToString(CurrCult)))
        tmpColl.Add(New TitleDescriptionObject(My.Strings.Strings.XtfHeaderNumberOfForwardLookArrays, Header.NumberOfForwardLookArrays.ToString(CurrCult)))
        tmpColl.Add(New TitleDescriptionObject(My.Strings.Strings.XtfHeaderNumberOfEchoStrengthChannels, Header.NumberOfEchoStrengthChannels.ToString(CurrCult)))
        tmpColl.Add(New TitleDescriptionObject(My.Strings.Strings.XtfHeaderNumberOfInterferometryChannels, Header.NumberOfInterferometryChannels.ToString(CurrCult)))
        tmpColl.Add(New TitleDescriptionObject(My.Strings.Strings.XtfHeaderReferencePointHeight, Header.ReferencePointHeight.ToString(CurrCult)))
        tmpColl.Add(New TitleDescriptionObject(My.Strings.Strings.XtfHeaderNavigationLatency, Header.NavigationLatency.ToString(CurrCult)))
        tmpColl.Add(New TitleDescriptionObject(My.Strings.Strings.XtfHeaderNavigationOffsetY, Header.NavigationOffsetY.ToString(CurrCult)))
        tmpColl.Add(New TitleDescriptionObject(My.Strings.Strings.XtfHeaderNavigationOffsetX, Header.NavigationOffsetX.ToString(CurrCult)))
        tmpColl.Add(New TitleDescriptionObject(My.Strings.Strings.XtfHeaderNavigationOffsetZ, Header.NavigationOffsetZ.ToString(CurrCult)))
        tmpColl.Add(New TitleDescriptionObject(My.Strings.Strings.XtfHeaderNavigationOffsetYaw, Header.NavigationOffsetYaw.ToString(CurrCult)))
        tmpColl.Add(New TitleDescriptionObject(My.Strings.Strings.XtfHeaderMRUOffsetY, Header.MRUOffsetY.ToString(CurrCult)))
        tmpColl.Add(New TitleDescriptionObject(My.Strings.Strings.XtfHeaderMRUOffsetX, Header.MRUOffsetX.ToString(CurrCult)))
        tmpColl.Add(New TitleDescriptionObject(My.Strings.Strings.XtfHeaderMRUOffsetZ, Header.MRUOffsetZ.ToString(CurrCult)))
        tmpColl.Add(New TitleDescriptionObject(My.Strings.Strings.XtfHeaderMRUOffsetYaw, Header.MRUOffsetYaw.ToString(CurrCult)))
        tmpColl.Add(New TitleDescriptionObject(My.Strings.Strings.XtfHeaderMRUOffsetPitch, Header.MRUOffsetPitch.ToString(CurrCult)))
        tmpColl.Add(New TitleDescriptionObject(My.Strings.Strings.XtfHeaderMRUOffsetRoll, Header.MRUOffsetRoll.ToString(CurrCult)))

        Return tmpColl

    End Function

    Public Function ChannelInfoToObservableCollection(ChannelNumber As Integer) As ObservableCollection(Of TitleDescriptionObject)
        Dim tmpColl As New ObservableCollection(Of TitleDescriptionObject)
        Dim CurrCult As Globalization.CultureInfo = Globalization.CultureInfo.CurrentCulture

        If ChannelNumber >= 0 And ChannelNumber < Header.ChannelInfo.Count Then
            tmpColl.Add(New TitleDescriptionObject(My.Strings.Strings.XtfHeaderChannelInfoTypeOfChannel, Header.ChannelInfo(ChannelNumber).TypeOfChannel.ToString(CurrCult)))
            tmpColl.Add(New TitleDescriptionObject(My.Strings.Strings.XtfHeaderChannelInfoSubChannelNumber, Header.ChannelInfo(ChannelNumber).SubChannelNumber.ToString(CurrCult)))
            tmpColl.Add(New TitleDescriptionObject(My.Strings.Strings.XtfHeaderChannelInfoCorrectionFlags, Header.ChannelInfo(ChannelNumber).CorrectionFlags.ToString(CurrCult)))
            tmpColl.Add(New TitleDescriptionObject(My.Strings.Strings.XtfHeaderChannelInfoUnipolar, Header.ChannelInfo(ChannelNumber).Unipolar.ToString(CurrCult)))
            tmpColl.Add(New TitleDescriptionObject(My.Strings.Strings.XtfHeaderChannelInfoBytesPerSample, Header.ChannelInfo(ChannelNumber).BytesPerSample.ToString(CurrCult)))
            tmpColl.Add(New TitleDescriptionObject(My.Strings.Strings.XtfHeaderChannelInfoSamplesPerChannel, Header.ChannelInfo(ChannelNumber).SamplesPerChannel.ToString(CurrCult)))
            tmpColl.Add(New TitleDescriptionObject(My.Strings.Strings.XtfHeaderChannelInfoChannelName, Header.ChannelInfo(ChannelNumber).ChannelName))
            tmpColl.Add(New TitleDescriptionObject(My.Strings.Strings.XtfHeaderChannelInfoVoltScale, Header.ChannelInfo(ChannelNumber).VoltScale.ToString(CurrCult)))
            tmpColl.Add(New TitleDescriptionObject(My.Strings.Strings.XtfHeaderChannelInfoFrequency, Header.ChannelInfo(ChannelNumber).Frequency.ToString(CurrCult)))
            tmpColl.Add(New TitleDescriptionObject(My.Strings.Strings.XtfHeaderChannelInfoHorizontalBeamAngle, Header.ChannelInfo(ChannelNumber).HorizontalBeamAngle.ToString(CurrCult)))
            tmpColl.Add(New TitleDescriptionObject(My.Strings.Strings.XtfHeaderChannelInfoTiltAngle, Header.ChannelInfo(ChannelNumber).TiltAngle.ToString(CurrCult)))
            tmpColl.Add(New TitleDescriptionObject(My.Strings.Strings.XtfHeaderChannelInfoBeamWidth, Header.ChannelInfo(ChannelNumber).BeamWidth.ToString(CurrCult)))
            tmpColl.Add(New TitleDescriptionObject(My.Strings.Strings.XtfHeaderChannelInfoOffsetX, Header.ChannelInfo(ChannelNumber).OffsetX.ToString(CurrCult)))
            tmpColl.Add(New TitleDescriptionObject(My.Strings.Strings.XtfHeaderChannelInfoOffsetY, Header.ChannelInfo(ChannelNumber).OffsetY.ToString(CurrCult)))
            tmpColl.Add(New TitleDescriptionObject(My.Strings.Strings.XtfHeaderChannelInfoOffsetZ, Header.ChannelInfo(ChannelNumber).OffsetZ.ToString(CurrCult)))
            tmpColl.Add(New TitleDescriptionObject(My.Strings.Strings.XtfHeaderChannelInfoOffsetYaw, Header.ChannelInfo(ChannelNumber).OffsetYaw.ToString(CurrCult)))
            tmpColl.Add(New TitleDescriptionObject(My.Strings.Strings.XtfHeaderChannelInfoOffsetPitch, Header.ChannelInfo(ChannelNumber).OffsetPitch.ToString(CurrCult)))
            tmpColl.Add(New TitleDescriptionObject(My.Strings.Strings.XtfHeaderChannelInfoOffsetRoll, Header.ChannelInfo(ChannelNumber).OffsetRoll.ToString(CurrCult)))
            tmpColl.Add(New TitleDescriptionObject(My.Strings.Strings.XtfHeaderChannelInfoBeamsPerArray, Header.ChannelInfo(ChannelNumber).BeamsPerArray.ToString(CurrCult)))

        End If

        Return tmpColl

    End Function

    Public Function GroupInfoToObservableCollection(GroupType As Byte) As ObservableCollection(Of TitleDescriptionObject)
        Dim tmpColl As New ObservableCollection(Of TitleDescriptionObject)
        Dim tmpID As Integer = Integer.MinValue
        Dim SubChannelNumberMin, SubChannelNumberMax As Byte
        Dim NumberChannelsToFollowMin, NumberChannelsToFollowMax As UInt16
        Dim NumberBytesThisRecordMin, NumberBytesThisRecordMax As UInt32
        Dim PacketTimeMin, PacketTimeMax As Date
        Dim CurrCult As Globalization.CultureInfo = Globalization.CultureInfo.CurrentCulture

        'Check if the desired group exist
        For g = 0 To DataGroups.Count - 1
            If DataGroups(g).HeaderType = GroupType Then
                tmpID = g
                Exit For
            End If
        Next

        If tmpID <> Integer.MinValue Then
            'The group exist
            'Calculate the values to display
            SubChannelNumberMin = DataGroups(tmpID).Packets(0).SubChannelNumber
            SubChannelNumberMax = DataGroups(tmpID).Packets(0).SubChannelNumber
            NumberChannelsToFollowMin = DataGroups(tmpID).Packets(0).NumberChannelsToFollow
            NumberChannelsToFollowMax = DataGroups(tmpID).Packets(0).NumberChannelsToFollow
            NumberBytesThisRecordMin = DataGroups(tmpID).Packets(0).NumberBytesThisRecord
            NumberBytesThisRecordMax = DataGroups(tmpID).Packets(0).NumberBytesThisRecord
            PacketTimeMin = DataGroups(tmpID).Packets(0).PacketTime
            PacketTimeMax = DataGroups(tmpID).Packets(0).PacketTime

            For p = 1 To DataGroups(tmpID).Packets.Count - 1
                UpdateMinMax(DataGroups(tmpID).Packets(p).SubChannelNumber, SubChannelNumberMin, SubChannelNumberMax)
                UpdateMinMax(DataGroups(tmpID).Packets(p).NumberChannelsToFollow, NumberChannelsToFollowMin, NumberChannelsToFollowMax)
                UpdateMinMax(DataGroups(tmpID).Packets(p).NumberBytesThisRecord, NumberBytesThisRecordMin, NumberBytesThisRecordMax)
                UpdateMinMax(DataGroups(tmpID).Packets(p).PacketTime, PacketTimeMin, PacketTimeMax)
            Next
            'Add the infos to the collection
            tmpColl.Add(New TitleDescriptionObject(My.Strings.Strings.XtfPacketGroupSubChannelNumber, SubChannelNumberMin.ToString(CurrCult) & " > " & SubChannelNumberMax.ToString(CurrCult)))
            tmpColl.Add(New TitleDescriptionObject(My.Strings.Strings.XtfPacketGroupNumberChannelsToFollow, NumberChannelsToFollowMin.ToString(CurrCult) & " > " & NumberChannelsToFollowMax.ToString(CurrCult)))
            tmpColl.Add(New TitleDescriptionObject(My.Strings.Strings.XtfPacketGroupNumberBytesThisRecord, NumberBytesThisRecordMin.ToString(CurrCult) & " > " & NumberBytesThisRecordMax.ToString(CurrCult)))
            tmpColl.Add(New TitleDescriptionObject(My.Strings.Strings.XtfPacketGroupPacketTime, PacketTimeMin.ToString(CurrCult) & " > " & PacketTimeMax.ToString(CurrCult)))
            tmpColl.Add(New TitleDescriptionObject(My.Strings.Strings.XtfPacketGroupCount, DataGroups(tmpID).Packets.Count.ToString(CurrCult)))
        Else
            'Group not available
            tmpColl.Add(New TitleDescriptionObject(GroupType.ToString(CurrCult), GroupType.ToString(CurrCult)))
        End If

        Return tmpColl
    End Function

End Class

