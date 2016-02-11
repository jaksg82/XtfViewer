''' <summary>
''' Lookup object for the packet sonar models defined in documentation
''' </summary>
Public Class XtfSonarTypes
    ''' <summary>
    ''' A collection of the known sonar models
    ''' </summary>
    ''' <returns></returns>
    Public ReadOnly Property KnownTypes As ObservableCollection(Of HeaderEntry)

    Public Sub New()
        KnownTypes = New ObservableCollection(Of HeaderEntry)()
        KnownTypes.Add(New HeaderEntry(0, "None", "Default"))
        KnownTypes.Add(New HeaderEntry(1, "Jamstec", "Jamstec chirp 2-channel subbottom"))
        KnownTypes.Add(New HeaderEntry(2, "AnalogC31", "PC31 8-channel"))
        KnownTypes.Add(New HeaderEntry(3, "Sis1000", "Chirp SIS-1000 sonar"))
        KnownTypes.Add(New HeaderEntry(4, "Analog32Chan", "Spectrum with 32-channel DSPlink card"))
        KnownTypes.Add(New HeaderEntry(5, "Klein2000", "Klein system 2000 with digital interface"))
        KnownTypes.Add(New HeaderEntry(6, "Rws", "Standard PC31 analog with special nav code"))
        KnownTypes.Add(New HeaderEntry(7, "Df1000", "EG&G DF1000 digital interface"))
        KnownTypes.Add(New HeaderEntry(8, "Seabat", "Reson SEABAT 900x analog/serial"))
        KnownTypes.Add(New HeaderEntry(9, "Klein595", "4-chan Klein 595, same as ANALOG_C31"))
        KnownTypes.Add(New HeaderEntry(10, "Egg260", "2-channel EGG260, same as ANALOG_C31"))
        KnownTypes.Add(New HeaderEntry(11, "SonatechDds", "Sonatech Diver Detection System on Spectrum DSP32C"))
        KnownTypes.Add(New HeaderEntry(12, "Echoscan", "Odom EchoScanII multibeam (with simultaneous analog sidescan)"))
        KnownTypes.Add(New HeaderEntry(13, "Elac", "Elac multibeam system"))
        KnownTypes.Add(New HeaderEntry(14, "Klein5000", "Klein system 5000 with digital interface"))
        KnownTypes.Add(New HeaderEntry(15, "Reson8101", "Reson Seabat 8101"))
        KnownTypes.Add(New HeaderEntry(16, "Imagenex858", "Imagenex model 858"))
        KnownTypes.Add(New HeaderEntry(17, "UsnSilos", "USN SILOS with 3-channel analog"))
        KnownTypes.Add(New HeaderEntry(18, "SonatechShr", "Sonatech Super-high res sidescan sonar"))
        KnownTypes.Add(New HeaderEntry(19, "DelphAu32", "Delph AU32 Analog input (2 channel)"))
        KnownTypes.Add(New HeaderEntry(20, "GenericMemory", "Generic sonar using the memory-mapped file interface"))
        KnownTypes.Add(New HeaderEntry(21, "SimradSm2000", "Simrad SM2000 Multibeam Echo Sounder"))
        KnownTypes.Add(New HeaderEntry(22, "Audio", "Standard multimedia audio"))
        KnownTypes.Add(New HeaderEntry(23, "EdgetechAci", "Edgetech (EG&G) ACI card for 260 sonar through PC31 card"))
        KnownTypes.Add(New HeaderEntry(24, "EdgetechBlackBox", "Edgetech Black Box"))
        KnownTypes.Add(New HeaderEntry(25, "FugroDeepTow", "Fugro deeptow"))
        KnownTypes.Add(New HeaderEntry(26, "EdgetechCC", "C&C Edgetech Chirp conversion program"))
        KnownTypes.Add(New HeaderEntry(27, "DtiSas", "DTI SAS Synthetic Aperture processor (memmap file)"))
        KnownTypes.Add(New HeaderEntry(28, "OsirisSss", "Fugro Osiris AUV Sidescan data"))
        KnownTypes.Add(New HeaderEntry(29, "OsirisMbes", "Fugro Osiris AUV Multibeam data"))
        KnownTypes.Add(New HeaderEntry(30, "GeoacousticsSls", "Geoacoustics SLS"))
        KnownTypes.Add(New HeaderEntry(31, "SimradEm2000", "Simrad EM2000/EM3000"))
        KnownTypes.Add(New HeaderEntry(32, "Klein3000", "Klein system 3000"))
        KnownTypes.Add(New HeaderEntry(33, "ShrSss", "SHRSSS Chirp system"))
        KnownTypes.Add(New HeaderEntry(34, "BenthosC3D", "Benthos C3D SARA/CAATI"))
        KnownTypes.Add(New HeaderEntry(35, "EdgetechMpx", "Edgetech MP-X"))
        KnownTypes.Add(New HeaderEntry(36, "Cmax", "CMAX"))
        KnownTypes.Add(New HeaderEntry(37, "BenthosSis1624", "Benthos sis1624"))
        KnownTypes.Add(New HeaderEntry(38, "Edgetech4200", "Edgetech 4200"))
        KnownTypes.Add(New HeaderEntry(39, "BenthosSis1500", "Benthos SIS1500"))
        KnownTypes.Add(New HeaderEntry(40, "BenthosSis1502", "Benthos SIS1502"))
        KnownTypes.Add(New HeaderEntry(41, "BenthosSis3000", "Benthos SIS3000"))
        KnownTypes.Add(New HeaderEntry(42, "BenthosSis7000", "Benthos SIS7000"))
        KnownTypes.Add(New HeaderEntry(43, "Df1000Dcu", "DF1000 DCU"))
        KnownTypes.Add(New HeaderEntry(44, "NoneSideScan", "NONE_SIDESCAN"))
        KnownTypes.Add(New HeaderEntry(45, "NoneMultiBeam", "NONE_MULTIBEAM"))
        KnownTypes.Add(New HeaderEntry(46, "Reson7125", "Reson 7125"))
        KnownTypes.Add(New HeaderEntry(47, "Coda", "CODA Echoscope"))
        KnownTypes.Add(New HeaderEntry(48, "KongsbergSas", "Kongsberg SAS"))
        KnownTypes.Add(New HeaderEntry(49, "Qinsy", "QINSy"))
        KnownTypes.Add(New HeaderEntry(50, "GeoacousticsDsss", "GeoAcoustics DSSS"))
        KnownTypes.Add(New HeaderEntry(51, "CmaxUsb", "CMAX_USB"))
        KnownTypes.Add(New HeaderEntry(52, "SwathPlusBathy", "SwathPlus Bathy"))
        KnownTypes.Add(New HeaderEntry(53, "R2SonicQinsy", "R2Sonic QINSy"))
        KnownTypes.Add(New HeaderEntry(54, "SwathPlusBathyConverted", "Converted SwathPlus Bathy"))
        KnownTypes.Add(New HeaderEntry(55, "R2SonicTriton", "R2Sonic Triton"))
        KnownTypes.Add(New HeaderEntry(56, "Edgetech4600", "Edgetech 4600"))
        KnownTypes.Add(New HeaderEntry(57, "Klein3500", "Klein 3500"))
        KnownTypes.Add(New HeaderEntry(58, "Klein5900", "Klein 5900"))


    End Sub

    ''' <summary>
    ''' Get the name of the given sonar model
    ''' </summary>
    ''' <param name="Id">Sonar model code</param>
    ''' <returns>Return the associated name, or Not Available if not present.</returns>
    Public Function GetName(Id As UInt16) As String
        Dim foundname As String = Nothing

        For Each hdr In KnownTypes
            If hdr.Value = Id Then foundname = hdr.Name
        Next

        Return If(String.IsNullOrWhiteSpace(foundname), My.Strings.Strings.XtfInfoNotAvailable, foundname)
    End Function

    ''' <summary>
    ''' Get the description of the given sonar model
    ''' </summary>
    ''' <param name="Id">Sonar model code</param>
    ''' <returns>Return the associated description, or Not Available if not present.</returns>
    Public Function GetDescription(Id As UInt16) As String
        Dim foundname As String = Nothing

        For Each hdr In KnownTypes
            If hdr.Value = Id Then foundname = hdr.Description
        Next

        Return If(String.IsNullOrWhiteSpace(foundname), My.Strings.Strings.XtfInfoNotAvailable, foundname)
    End Function

End Class
