''' <summary>
''' Lookup object for the packet header types defined in documentation
''' </summary>
Public Class XtfHeaderTypes
    ''' <summary>
    ''' A collection of the known header types
    ''' </summary>
    ''' <returns></returns>
    Public ReadOnly Property KnownTypes As ObservableCollection(Of HeaderEntry)

    Public Sub New()
        KnownTypes = New ObservableCollection(Of HeaderEntry)()
        KnownTypes.Add(New HeaderEntry(0, "XtfHeaderSonar", "Sidescan And subbottom"))
        KnownTypes.Add(New HeaderEntry(1, "XtfHeaderNotes", "Notes - text annotation"))
        KnownTypes.Add(New HeaderEntry(2, "XtfHeaderBathy", "Bathymetry (Seabat, Odom)"))
        KnownTypes.Add(New HeaderEntry(3, "XtfHeaderAttitude", "TSS Or MRU attitude (pitch, roll, heave, yaw)"))
        KnownTypes.Add(New HeaderEntry(4, "XtfHeaderForward", "Forward-look sonar (polar display)"))
        KnownTypes.Add(New HeaderEntry(5, "XtfHeaderElac", "Elac multibeam"))
        KnownTypes.Add(New HeaderEntry(6, "XtfHeaderRawSerial", "Raw data from serial port"))
        KnownTypes.Add(New HeaderEntry(7, "XtfHeaderEmbeddedHead", "Embedded header Structure"))
        KnownTypes.Add(New HeaderEntry(8, "XtfHeaderHiddenSonar", "Hidden (non-displayable) ping"))
        KnownTypes.Add(New HeaderEntry(9, "XtfHeaderSeaviewProcessedBathy", "Bathymetry (angles) for Seaview"))
        KnownTypes.Add(New HeaderEntry(10, "XtfHeaderSeaviewDepths", "Bathymetry from Seaview data (depths)"))
        KnownTypes.Add(New HeaderEntry(11, "XtfHeaderRsvdHighSpeedSensor", "Used by Klein. 0=roll, 1=yaw"))
        KnownTypes.Add(New HeaderEntry(12, "XtfHeaderEchoStrength", "Elac EchoStrength (10 values)"))
        KnownTypes.Add(New HeaderEntry(13, "XtfHeaderGeorec", "Used to store mosaic parameters"))
        KnownTypes.Add(New HeaderEntry(14, "XtfHeaderKleinRawBathy", "Bathymetry data from the Klein 5000"))
        KnownTypes.Add(New HeaderEntry(15, "XtfHeaderHighSpeedSensor2", "High speed sensor from Klein 5000"))
        KnownTypes.Add(New HeaderEntry(16, "XtfHeaderElacXse", "Elac dual-head"))
        KnownTypes.Add(New HeaderEntry(17, "XtfHeaderBathyXYZA", "Processed bathymetry data"))
        KnownTypes.Add(New HeaderEntry(18, "XtfHeaderK5000BathyIQ", "Raw IQ data from Klein 5000 server"))
        KnownTypes.Add(New HeaderEntry(19, "XtfHeaderBathySnippet", "Bathymetry snippet data"))
        KnownTypes.Add(New HeaderEntry(20, "XtfHeaderGps", "GPS Position"))
        KnownTypes.Add(New HeaderEntry(21, "XtfHeaderStat", "GPS statistics"))
        KnownTypes.Add(New HeaderEntry(22, "XtfHeaderSingleBeam", "Bathymetry data from singlebeam echosounder"))
        KnownTypes.Add(New HeaderEntry(23, "XtfHeaderGyro", "Heading/Speed Sensor"))
        KnownTypes.Add(New HeaderEntry(24, "XtfHeaderTrackPoint", ""))
        KnownTypes.Add(New HeaderEntry(25, "XtfHeaderMultiBeam", ""))
        KnownTypes.Add(New HeaderEntry(26, "XtfHeaderQpsSingleBeam", ""))
        KnownTypes.Add(New HeaderEntry(27, "XtfHeaderQpsMultiTx", ""))
        KnownTypes.Add(New HeaderEntry(28, "XtfHeaderQpsMultiBeam", ""))
        KnownTypes.Add(New HeaderEntry(42, "XtfHeaderNavigation", "Source time-stamped navigation data"))
        KnownTypes.Add(New HeaderEntry(50, "XtfHeaderTime", ""))
        KnownTypes.Add(New HeaderEntry(60, "XtfHeaderBenthosCaatiSara", "Custom Benthos data."))
        KnownTypes.Add(New HeaderEntry(61, "XtfHeader7125", "7125 Bathy Data"))
        KnownTypes.Add(New HeaderEntry(62, "XtfHeader7125Snippet", "7125 Bathy Data Snippets"))
        KnownTypes.Add(New HeaderEntry(65, "XtfHeaderQinsyR2SonicBathy", "QINSy R2Sonic bathymetry data"))
        KnownTypes.Add(New HeaderEntry(66, "XtfHeaderQinsyR2SonicFts", "QINSy R2Sonics Foot Print Time Series (snippets)"))
        KnownTypes.Add(New HeaderEntry(68, "XtfHeaderR2SonicBathy", "Triton R2Sonic bathymetry data"))
        KnownTypes.Add(New HeaderEntry(69, "XtfHeaderR2SonicFts", "Triton R2Sonic Footprint Time Series"))
        KnownTypes.Add(New HeaderEntry(70, "XtfHeaderCodaEchoscopeData", "Custom CODA Echoscope Data"))
        KnownTypes.Add(New HeaderEntry(71, "XtfHeaderCodaEchoscopeConfig", "Custom CODA Echoscope Data"))
        KnownTypes.Add(New HeaderEntry(72, "XtfHeaderCodaEchoscopeImage", "Custom CODA Echoscope Data"))
        KnownTypes.Add(New HeaderEntry(73, "XtfHeaderEdgetech4600", ""))
        KnownTypes.Add(New HeaderEntry(78, "XtfHeaderReson7018WaterColumn", ""))
        KnownTypes.Add(New HeaderEntry(79, "XtfHeaderR2SonicWaterColumn", ""))
        KnownTypes.Add(New HeaderEntry(84, "XtfHeaderSourceTimeGyro", "Source time-stamped gyro data"))
        KnownTypes.Add(New HeaderEntry(100, "XtfHeaderPosition", "Raw position packet - Reserved for use by Reson, Inc. RESON ONLY."))
        KnownTypes.Add(New HeaderEntry(102, "XtfHeaderBathyProcessed", ""))
        KnownTypes.Add(New HeaderEntry(103, "XtfHeaderAttitudeProcessed", ""))
        KnownTypes.Add(New HeaderEntry(104, "XtfHeaderSingleBeamProcessed", ""))
        KnownTypes.Add(New HeaderEntry(105, "XtfHeaderAuxProcessed", "Aux Channel + AuxAltitude + Magnetometer."))
        KnownTypes.Add(New HeaderEntry(106, "XtfHeaderKlein3000DataPage", ""))
        KnownTypes.Add(New HeaderEntry(107, "XtfHeaderPositionRawNavigation", ""))
        KnownTypes.Add(New HeaderEntry(108, "XtfHeaderKleinV4DataPage", ""))
        KnownTypes.Add(New HeaderEntry(199, "XtfHeaderCustom", "Cutom Vendor data"))
        KnownTypes.Add(New HeaderEntry(200, "XtfHeaderUserDefined", "This packet type is reserved for specific applications."))


    End Sub

    ''' <summary>
    ''' Get the name of the given packet header type
    ''' </summary>
    ''' <param name="Id">Header type code</param>
    ''' <returns>Return the associated name, or Not Available if not present.</returns>
    Public Function GetName(Id As Byte) As String
        Dim foundname As String = Nothing

        For Each hdr In KnownTypes
            If hdr.Value = Id Then foundname = hdr.Name
        Next

        Return If(String.IsNullOrWhiteSpace(foundname), My.Strings.Strings.XtfInfoNotAvailable, foundname)
    End Function

    ''' <summary>
    ''' Get the description of the given packet header type
    ''' </summary>
    ''' <param name="Id">Header type code</param>
    ''' <returns>Return the associated description, or Not Available if not present.</returns>
    Public Function GetDescription(Id As Byte) As String
        Dim foundname As String = Nothing

        For Each hdr In KnownTypes
            If hdr.Value = Id Then foundname = hdr.Description
        Next

        Return If(String.IsNullOrWhiteSpace(foundname), My.Strings.Strings.XtfInfoNotAvailable, foundname)
    End Function

End Class
