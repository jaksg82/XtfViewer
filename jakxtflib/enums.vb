

Public Enum XtfHeaderType 'Stored as Byte in the xtf file
    XtfHeaderSonar = 0     ' Sidescan And subbottom
    XtfHeaderNotes = 1     ' Notes - text annotation
    XtfHeaderBathy = 2     ' Bathymetry (Seabat, Odom)
    XtfHeaderAttitude = 3     ' TSS Or MRU attitude (pitch, roll, heave, yaw)
    XtfHeaderForward = 4     ' Forward-look sonar (polar display)
    XtfHeaderElac = 5     ' Elac multibeam
    XtfHeaderRawSerial = 6     ' Raw data from serial port
    XtfHeaderEmbeddedHead = 7     ' Embedded header Structure
    XtfHeaderHiddenSonar = 8     ' Hidden (non-displayable) ping
    XtfHeaderSeaviewProcessedBathy = 9     ' Bathymetry (angles) for Seaview
    XtfHeaderSeaviewDepths = 10     ' Bathymetry from Seaview data (depths).
    XtfHeaderRsvdHighSpeedSensor = 11     ' Used by Klein. 0=roll, 1=yaw.
    XtfHeaderEchoStrength = 12     ' Elac EchoStrength (10 values).
    XtfHeaderGeorec = 13     ' Used to store mosaic parameters.
    XtfHeaderKleinRawBathy = 14     ' Bathymetry data from the Klein 5000.
    XtfHeaderHighSpeedSensor2 = 15     ' High speed sensor from Klein 5000.
    XtfHeaderElacXse = 16     ' Elac dual-head.
    XtfHeaderBathyXYZA = 17     ' 
    XtfHeaderK5000BathyIQ = 18     ' Raw IQ data from Klein 5000 server
    XtfHeaderBathySnippet = 19     ' 
    XtfHeaderGps = 20     ' GPS Position.
    XtfHeaderStat = 21     ' GPS statistics.
    XtfHeaderSingleBeam = 22     ' 
    XtfHeaderGyro = 23     ' Heading/Speed Sensor.
    XtfHeaderTrackPoint = 24     ' 
    XtfHeaderMultiBeam = 25     ' 
    XtfHeaderQpsSingleBeam = 26     ' 
    XtfHeaderQpsMultiTx = 27     ' 
    XtfHeaderQpsMultiBeam = 28     ' 
    XtfHeaderNavigation = 42     ' Source time-stamped navigation data
    XtfHeaderTime = 50     ' 
    XtfHeaderBenthosCaatiSara = 60     ' Custom Benthos data.
    XtfHeader7125 = 61     ' 7125 Bathy Data
    XtfHeader7125Snippet = 62     ' 7125 Bathy Data Snippets
    XtfHeaderQinsyR2SonicBathy = 65     ' QINSy R2Sonic bathymetry data
    XtfHeaderQinsyR2SonicFts = 66     ' QINSy R2Sonics Foot Print Time Series (snippets)
    XtfHeaderR2SonicBathy = 68     ' Triton R2Sonic bathymetry data
    XtfHeaderR2SonicFts = 69     ' Triton R2Sonic Footprint Time Series
    XtfHeaderCodaEchoscopeData = 70     ' Custom CODA Echoscope Data
    XtfHeaderCodaEchoscopeConfig = 71     ' Custom CODA Echoscope Data
    XtfHeaderCodaEchoscopeImage = 72     ' Custom CODA Echoscope Data
    XtfHeaderEdgetech4600 = 73     ' 
    XtfHeaderReson7018WaterColumn = 78     ' 
    XtfHeaderR2SonicWaterColumn = 79     ' 
    XtfHeaderSourceTimeGyro = 84     ' Source time-stamped gyro data
    XtfHeaderPosition = 100     ' Raw position packet - Reserved for use by Reson, Inc. RESON ONLY.
    XtfHeaderBathyProcessed = 102     ' 
    XtfHeaderAttitudeProcessed = 103     ' 
    XtfHeaderSingleBeamProcessed = 104     ' 
    XtfHeaderAuxProcessed = 105     ' Aux Channel + AuxAltitude + Magnetometer.
    XtfHeaderKlein3000DataPage = 106     ' 
    XtfHeaderPositionRawNavigation = 107     ' 
    XtfHeaderKleinV4DataPage = 108     ' 
    XtfHeaderCustom = 199     ' Cutom Vendor data
    XtfHeaderUserDefined = 200     ' This packet type is reserved for specific applications.
End Enum

Public Enum XtfSonarType 'Stored as UInt16 in the xtf file
    None = 0     ' Default
    Jamstec = 1     ' Jamstec chirp 2-channel subbottom
    AnalogC31 = 2     ' PC31 8-channel
    Sis1000 = 3     ' Chirp SIS-1000 sonar
    Analog32Chan = 4     ' Spectrum with 32-channel DSPlink card
    Klein2000 = 5     ' Klein system 2000 with digital interface
    Rws = 6     ' Standard PC31 analog with special nav code
    Df1000 = 7     ' EG&G DF1000 digital interface
    Seabat = 8     ' Reson SEABAT 900x analog/serial
    Klein595 = 9     ' 4-chan Klein 595, same as ANALOG_C31
    Egg260 = 10     ' 2-channel EGG260, same as ANALOG_C31
    SonatechDds = 11     ' Sonatech Diver Detection System on Spectrum DSP32C
    Echoscan = 12     ' Odom EchoScanII multibeam (with simultaneous analog sidescan)
    Elac = 13     ' Elac multibeam system
    Klein5000 = 14     ' Klein system 5000 with digital interface
    Reson8101 = 15     ' Reson Seabat 8101
    Imagenex858 = 16     ' Imagenex model 858
    UsnSilos = 17     ' USN SILOS with 3-channel analog
    SonatechShr = 18     ' Sonatech Super-high res sidescan sonar
    DelphAu32 = 19     ' Delph AU32 Analog input (2 channel)
    GenericMemory = 20     ' Generic sonar using the memory-mapped file interface
    SimradSm2000 = 21     ' Simrad SM2000 Multibeam Echo Sounder
    Audio = 22     ' Standard multimedia audio
    EdgetechAci = 23     ' Edgetech (EG&G) ACI card for 260 sonar through PC31 card
    EdgetechBlackBox = 24     ' Edgetech Black Box
    FugroDeepTow = 25     ' Fugro deeptow
    EdgetechCC = 26     ' C&C Edgetech Chirp conversion program
    DtiSas = 27     ' DTI SAS Synthetic Aperture processor (memmap file)
    OsirisSss = 28     ' Fugro Osiris AUV Sidescan data
    OsirisMbes = 29     ' Fugro Osiris AUV Multibeam data
    GeoacousticsSls = 30     ' Geoacoustics SLS
    SimradEm2000 = 31     ' Simrad EM2000/EM3000
    Klein3000 = 32     ' Klein system 3000
    ShrSss = 33     ' SHRSSS Chirp system
    BenthosC3D = 34     ' Benthos C3D SARA/CAATI
    EdgetechMpx = 35     ' Edgetech MP-X
    Cmax = 36     ' CMAX
    BenthosSis1624 = 37     ' Benthos sis1624
    Edgetech4200 = 38     ' Edgetech 4200
    BenthosSis1500 = 39     ' Benthos SIS1500
    BenthosSis1502 = 40     ' Benthos SIS1502
    BenthosSis3000 = 41     ' Benthos SIS3000
    BenthosSis7000 = 42     ' Benthos SIS7000
    Df1000Dcu = 43     ' DF1000 DCU
    NoneSideScan = 44     ' NONE_SIDESCAN
    NoneMultiBeam = 45     ' NONE_MULTIBEAM
    Reson7125 = 46     ' Reson 7125
    Coda = 47     ' CODA Echoscope
    KongsbergSas = 48     ' Kongsberg SAS
    Qinsy = 49     ' QINSy
    GeoacousticsDsss = 50     ' GeoAcoustics DSSS
    CmaxUsb = 51     ' CMAX_USB
    SwathPlusBathy = 52     ' SwathPlus Bathy
    R2SonicQinsy = 53     ' R2Sonic QINSy
    SwathPlusBathyConverted = 54     ' Converted SwathPlus Bathy
    R2SonicTriton = 55     ' R2Sonic Triton
    Edgetech4600 = 56     ' Edgetech 4600
    Klein3500 = 57     ' Klein 3500
    Klein5900 = 58     ' Klein 5900
End Enum

Public Enum XtfTypeOfChannel 'Stored as Byte in the xtf file
    SubBottom = 0
    Port = 1
    Starboard = 2
    Bathymetry = 3
End Enum

Public Enum XtfChannelSampleFormat 'Stored as Byte in the xtf file - Not used after X26 version
    Legacy = 0
    IBMFloat = 1
    Integer4Byte = 2
    Integer2Byte = 3
    Unused4 = 4
    IEEEFloat = 5
    Unused6 = 6
    Unused7 = 7
    Integer1Byte = 8
End Enum

Public Enum XtfNoteSubChannel 'Stored as Byte in the xtf file
    Generic = 0
    VesselName = 1
    SurveyArea = 2
    OperatorName = 3
End Enum


' Define an Enum with FlagsAttribute.
<Flags>
Public Enum XtfSnippetFilters 'Stored as UInt16 in the xtf file
    None = 0
    Range = 1
    Depth = 2
End Enum

<Flags>
Public Enum XtfSnippetFlags 'Stored as UInt16 in the xtf file
    None = 0
    Range = 1
    Depth = 2
End Enum

