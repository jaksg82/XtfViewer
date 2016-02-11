Imports System.Xml
Imports System.Xml.Serialization

Partial Public Class XtfIndex
    ''' <summary>
    ''' Save this class as serialized xml document
    ''' </summary>
    ''' <returns></returns>
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

    ''' <summary>
    ''' Format the information stored in the file header inside a collection of Title and Description objects
    ''' </summary>
    ''' <returns>Localized collection of Title and Description objects</returns>
    Public Function HeaderToObservableCollection() As ObservableCollection(Of TitleDescriptionObject)
        Dim tmpColl As New ObservableCollection(Of TitleDescriptionObject)
        Dim CurrCult As Globalization.CultureInfo = Globalization.CultureInfo.CurrentCulture
        'Look for sonar type name and description
        Dim SonarTypes As New XtfSonarTypes
        Dim TypeString As String
        TypeString = Header.SonarType.ToString(CurrCult) & ": "
        TypeString = TypeString & SonarTypes.GetName(Header.SonarType) & vbCrLf
        TypeString = TypeString & SonarTypes.GetDescription(Header.SonarType)
        'Look for the navigation units
        Dim UnitString As String
        UnitString = Header.NavigationCoordinateUnits.ToString(CurrCult) & ": "
        Select Case Header.NavigationCoordinateUnits
            Case 0
                UnitString = UnitString & My.Strings.Strings.XtfUnitMeters
            Case 3
                UnitString = UnitString & My.Strings.Strings.XtfUnitDegrees
            Case Else
                UnitString = UnitString & My.Strings.Strings.XtfInfoNotAvailable
        End Select

        'Populate the collection
        tmpColl.Add(New TitleDescriptionObject(My.Strings.Strings.XtfHeaderFileFormat, Header.FileFormat.ToString(CurrCult)))
        tmpColl.Add(New TitleDescriptionObject(My.Strings.Strings.XtfHeaderSystemType, Header.SystemType.ToString(CurrCult)))
        tmpColl.Add(New TitleDescriptionObject(My.Strings.Strings.XtfHeaderRecordingProgramName, Header.RecordingProgramName))
        tmpColl.Add(New TitleDescriptionObject(My.Strings.Strings.XtfHeaderRecordingProgramVersion, Header.RecordingProgramVersion))
        tmpColl.Add(New TitleDescriptionObject(My.Strings.Strings.XtfHeaderSonarName, Header.SonarName))
        tmpColl.Add(New TitleDescriptionObject(My.Strings.Strings.XtfHeaderSonarType, TypeString))
        tmpColl.Add(New TitleDescriptionObject(My.Strings.Strings.XtfHeaderNoteString, Header.NoteString))
        tmpColl.Add(New TitleDescriptionObject(My.Strings.Strings.XtfHeaderThisFileName, Header.ThisFileName))
        tmpColl.Add(New TitleDescriptionObject(My.Strings.Strings.XtfHeaderNavigationCoordinateUnits, UnitString))
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

    ''' <summary>
    ''' Format the information stored in the channel info header inside a collection of Title and Description objects
    ''' </summary>
    ''' <returns>Localized collection of Title and Description objects</returns>
    Public Function ChannelInfoToObservableCollection(ChannelNumber As Integer) As ObservableCollection(Of TitleDescriptionObject)
        Dim tmpColl As New ObservableCollection(Of TitleDescriptionObject)
        Dim CurrCult As Globalization.CultureInfo = Globalization.CultureInfo.CurrentCulture

        If ChannelNumber >= 0 And ChannelNumber < Header.ChannelInfo.Count Then
            'Look for the channel type
            Dim ChanType As String
            ChanType = Header.ChannelInfo(ChannelNumber).TypeOfChannel.ToString(CurrCult) & ": "
            Select Case Header.ChannelInfo(ChannelNumber).TypeOfChannel
                Case 0
                    ChanType = ChanType & My.Strings.Strings.XtfHeaderChannelInfoSubbottom
                Case 1
                    ChanType = ChanType & My.Strings.Strings.XtfHeaderChannelInfoPort
                Case 2
                    ChanType = ChanType & My.Strings.Strings.XtfHeaderChannelInfoStarboard
                Case 3
                    ChanType = ChanType & My.Strings.Strings.XtfHeaderChannelInfoBathy
                Case Else
                    ChanType = ChanType & My.Strings.Strings.XtfInfoNotAvailable
            End Select

            tmpColl.Add(New TitleDescriptionObject(My.Strings.Strings.XtfHeaderChannelInfoTypeOfChannel, ChanType))
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

    ''' <summary>
    ''' Format the information stored in the packet groups inside a collection of Title and Description objects
    ''' </summary>
    ''' <returns>Localized collection of Title and Description objects</returns>
    Public Function GroupInfoToObservableCollection(GroupType As Byte) As ObservableCollection(Of TitleDescriptionObject)
        Dim tmpColl As New ObservableCollection(Of TitleDescriptionObject)
        Dim tmpID As Integer = Integer.MinValue
        Dim SubChannelNumberMin, SubChannelNumberMax As Byte
        Dim NumberChannelsToFollowMin, NumberChannelsToFollowMax As UInt16
        Dim NumberBytesThisRecordMin, NumberBytesThisRecordMax, TimeTagMin, TimeTagMax As UInt32
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
            TimeTagMin = DataGroups(tmpID).Packets(0).TimeTag
            TimeTagMax = DataGroups(tmpID).Packets(0).TimeTag

            For p = 1 To DataGroups(tmpID).Packets.Count - 1
                UpdateMinMax(DataGroups(tmpID).Packets(p).SubChannelNumber, SubChannelNumberMin, SubChannelNumberMax)
                UpdateMinMax(DataGroups(tmpID).Packets(p).NumberChannelsToFollow, NumberChannelsToFollowMin, NumberChannelsToFollowMax)
                UpdateMinMax(DataGroups(tmpID).Packets(p).NumberBytesThisRecord, NumberBytesThisRecordMin, NumberBytesThisRecordMax)
                UpdateMinMax(DataGroups(tmpID).Packets(p).PacketTime, PacketTimeMin, PacketTimeMax)
                UpdateMinMax(DataGroups(tmpID).Packets(p).TimeTag, TimeTagMin, TimeTagMax)
            Next
            'Format the stats
            Dim StatChanNumber, StatChanFollow, StatBytes, StatTimes, StatTags As String
            If SubChannelNumberMax = SubChannelNumberMin Then
                StatChanNumber = SubChannelNumberMin.ToString(CurrCult)
            Else
                StatChanNumber = String.Format(CurrCult, My.Strings.Strings.XtfPacketGroupFromTo, SubChannelNumberMin.ToString(CurrCult), SubChannelNumberMax.ToString(CurrCult))
            End If
            If NumberChannelsToFollowMin = NumberChannelsToFollowMax Then
                StatChanFollow = NumberChannelsToFollowMin.ToString(CurrCult)
            Else
                StatChanFollow = String.Format(CurrCult, My.Strings.Strings.XtfPacketGroupFromTo, NumberChannelsToFollowMin.ToString(CurrCult), NumberChannelsToFollowMax.ToString(CurrCult))
            End If
            If NumberBytesThisRecordMin = NumberBytesThisRecordMax Then
                StatBytes = NumberBytesThisRecordMin.ToString(CurrCult)
            Else
                StatBytes = String.Format(CurrCult, My.Strings.Strings.XtfPacketGroupFromTo, NumberBytesThisRecordMin.ToString(CurrCult), NumberBytesThisRecordMax.ToString(CurrCult))
            End If
            If PacketTimeMin = PacketTimeMax Then
                StatTimes = If(PacketTimeMin = Date.MinValue, My.Strings.Strings.XtfInfoNotAvailable, PacketTimeMin.ToString(CurrCult))
            Else
                StatTimes = String.Format(CurrCult, My.Strings.Strings.XtfPacketGroupFromTo, PacketTimeMin.ToString(CurrCult), PacketTimeMax.ToString(CurrCult))
            End If
            If TimeTagMin = TimeTagMax Then
                StatTags = If(TimeTagMin = 0, My.Strings.Strings.XtfInfoNotAvailable, TimeTagMin.ToString(CurrCult))
            Else
                StatTags = String.Format(CurrCult, My.Strings.Strings.XtfPacketGroupFromTo, TimeTagMin.ToString(CurrCult), TimeTagMax.ToString(CurrCult))
            End If
            'Look for the packet type name and description
            Dim hdrTypes As New XtfHeaderTypes
            Dim TypeString As String
            TypeString = DataGroups(tmpID).HeaderType.ToString(CurrCult) & ": "
            TypeString = TypeString & hdrTypes.GetName(DataGroups(tmpID).HeaderType) & vbCrLf
            TypeString = TypeString & hdrTypes.GetDescription(DataGroups(tmpID).HeaderType)

            'Add the infos to the collection
            tmpColl.Add(New TitleDescriptionObject(My.Strings.Strings.XtfPacketGroupCount, DataGroups(tmpID).Packets.Count.ToString(CurrCult)))
            tmpColl.Add(New TitleDescriptionObject(My.Strings.Strings.XtfPacketGroupHeaderType, TypeString))
            tmpColl.Add(New TitleDescriptionObject(My.Strings.Strings.XtfPacketGroupSubChannelNumber, StatChanNumber))
            tmpColl.Add(New TitleDescriptionObject(My.Strings.Strings.XtfPacketGroupNumberChannelsToFollow, StatChanFollow))
            tmpColl.Add(New TitleDescriptionObject(My.Strings.Strings.XtfPacketGroupNumberBytesThisRecord, StatBytes))
            tmpColl.Add(New TitleDescriptionObject(My.Strings.Strings.XtfPacketGroupPacketTime, StatTimes))
            tmpColl.Add(New TitleDescriptionObject(My.Strings.Strings.XtfPacketGroupTimeTag, StatTags))
        Else
            'Group not available
            tmpColl.Add(New TitleDescriptionObject(GroupType.ToString(CurrCult), GroupType.ToString(CurrCult)))
        End If

        Return tmpColl
    End Function

    ''' <summary>
    ''' Format the packet group Ids inside a collection of strings
    ''' </summary>
    ''' <returns>Localized collection of strings</returns>
    Public Function GetGroupStrings() As ObservableCollection(Of String)
        Dim AvailGroups As New ObservableCollection(Of String)

        'Fill the AvailableGroup property
        AvailGroups.Add(My.Strings.Strings.AppAvailGroupMainHeader)
        For g = 0 To Me.Header.ChannelInfo.Count - 1
            AvailGroups.Add(String.Format(Globalization.CultureInfo.CurrentCulture, My.Strings.Strings.AppAvailGroupChannelInfo, g))
        Next
        For p = 0 To Me.DataGroups.Count - 1
            AvailGroups.Add(String.Format(Globalization.CultureInfo.CurrentCulture, My.Strings.Strings.AppAvailGroupPacketGroup,
                                              Me.DataGroups(p).HeaderType.ToString(Globalization.CultureInfo.CurrentCulture)))
        Next
        Return AvailGroups
    End Function

End Class
