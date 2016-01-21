Imports System.Xml
Imports System.Xml.Serialization

Partial Public Class XtfIndex
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
