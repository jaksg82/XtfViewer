<Assembly: CLSCompliant(False)> 
<Assembly: System.Runtime.InteropServices.ComVisible(False)> 

Public Class XtfMainHeaderX36
    Dim inRecProgName, inRecProgVersion, inSonarName As String
    Dim inNote, inFileName As String
    Dim inChanInfo As New List(Of ChanInfo)

    ''' <summary>
    ''' This is the number that identify the start of each packet header
    ''' </summary>
    ''' <value>0xFACE</value>
    ''' <returns>UInt16 value of 64206</returns>
    ''' <remarks>This value MUST be as per Triton documentation</remarks>
    Public Shared ReadOnly Property MagicNumber As UInt16
        Get
            Return 64206
        End Get
    End Property

    ''' <summary>
    ''' Xtf file format version
    ''' </summary>
    ''' <value></value>
    ''' <returns>File format version</returns>
    ''' <remarks>This value should be 123 as per Triton documentation.</remarks>
    Public Property FileFormat As Byte

    ''' <summary>
    ''' Xtf system type
    ''' </summary>
    ''' <value></value>
    ''' <returns>Sistem type</returns>
    ''' <remarks>This value should be 1 as per Triton documentation.</remarks>
    Public Property SystemType As Byte

    ''' <summary>
    ''' Name of the program used to create this xtf file.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks>The maximum size of the name is of 8 characters. Longer strings will be shortened.</remarks>
    Public Property RecordingProgramName As String
        Set(value As String)
            If value Is Nothing Then value = " "c
            If String.IsNullOrEmpty(value) Then value = " "c
            Dim tmpvalue As String = value.Replace(ChrW(0), " "c)
            If tmpvalue.Length > 8 Then
                inRecProgName = tmpvalue.Substring(0, 8)
            Else
                inRecProgName = tmpvalue.PadRight(8)
            End If
        End Set
        Get
            Return inRecProgName
        End Get
    End Property

    ''' <summary>
    ''' Version of the program used to create this xtf file.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks>It's best to use 223, for compability reasons, or 341 for newer format improvments.</remarks>
    Public Property RecordingProgramVersion As String
        Set(value As String)
            If value Is Nothing Then value = " "c
            If String.IsNullOrEmpty(value) Then value = " "c
            Dim tmpvalue As String = value.Replace(ChrW(0), " "c)
            If tmpvalue.Length > 8 Then
                inRecProgVersion = tmpvalue.Substring(0, 8)
            Else
                inRecProgVersion = tmpvalue.PadRight(8)
            End If
        End Set
        Get
            Return inRecProgVersion
        End Get
    End Property

    ''' <summary>
    ''' Name of server used to access sonar.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks>The maximum size of the name is of 16 characters. Longer strings will be shortened.</remarks>
    Public Property SonarName As String
        Set(value As String)
            If value Is Nothing Then value = " "c
            If String.IsNullOrEmpty(value) Then value = " "c
            Dim tmpvalue As String = value.Replace(ChrW(0), " "c)
            If tmpvalue.Length > 16 Then
                inSonarName = tmpvalue.Substring(0, 16)
            Else
                inSonarName = tmpvalue.PadRight(16)
            End If
        End Set
        Get
            Return inSonarName
        End Get
    End Property

    ''' <summary>
    ''' ID of the sonar type used to acquire the data.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property SonarType As UInt16

    ''' <summary>
    ''' Notes
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks>Max 64 chars.</remarks>
    Public Property NoteString As String
        Set(value As String)
            If value Is Nothing Then value = " "c
            If String.IsNullOrEmpty(value) Then value = " "c
            Dim tmpvalue As String = value.Replace(ChrW(0), " "c)
            If tmpvalue.Length > 64 Then
                inNote = tmpvalue.Substring(0, 64)
            Else
                inNote = tmpvalue.PadRight(64)
            End If
        End Set
        Get
            Return inNote
        End Get
    End Property

    ''' <summary>
    ''' Name of this file.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks>Max 64 chars.</remarks>
    Public Property ThisFileName As String
        Set(value As String)
            If value Is Nothing Then value = " "c
            If String.IsNullOrEmpty(value) Then value = " "c
            Dim tmpvalue As String = value.Replace(ChrW(0), " "c)
            If tmpvalue.Length > 64 Then
                inFileName = tmpvalue.Substring(0, 64)
            Else
                inFileName = tmpvalue.PadRight(64)
            End If
        End Set
        Get
            Return inFileName
        End Get
    End Property

    ''' <summary>
    ''' Coordinate units.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks>0 for meters or 3 for Lat/Long.</remarks>
    Public Property NavigationCoordinateUnits As UInt16

    Public Property NumberOfSonarChannels As UInt16
    Public Property NumberOfBathymetryChannels As UInt16
    Public Property NumberOfSnippetChannels As Byte
    Public Property NumberOfForwardLookArrays As Byte
    Public Property NumberOfEchoStrengthChannels As UInt16
    Public Property NumberOfInterferometryChannels As Byte

    ''' <summary>
    ''' Height of reference point above water line (m)
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property ReferencePointHeight As Single

    ''' <summary>
    ''' Latency of nav system in milliceconds.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks>Usually GPS latency.</remarks>
    Public Property NavigationLatency As Int32

    ''' <summary>
    ''' Orientation of positive Y is forward.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property NavigationOffsetY As Single

    ''' <summary>
    ''' Orientation of positive X is to starboard.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property NavigationOffsetX As Single

    ''' <summary>
    ''' Orientation of positive Z is down.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property NavigationOffsetZ As Single

    ''' <summary>
    ''' Orientation of positive Yaw is turn to right.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property NavigationOffsetYaw As Single

    ''' <summary>
    ''' Orientation of positive Y is forward.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property MRUOffsetY As Single

    ''' <summary>
    ''' Orientation of positive X is to starboard.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property MRUOffsetX As Single

    ''' <summary>
    ''' Orientation of positive Z is down.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property MRUOffsetZ As Single

    ''' <summary>
    ''' Orientation of positive Yaw is turn to right.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property MRUOffsetYaw As Single

    ''' <summary>
    ''' Orientation of positive Pitch is nose up.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property MRUOffsetPitch As Single

    ''' <summary>
    ''' Orientation of positive Roll is lean to starboard.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property MRUOffsetRoll As Single

    ''' <summary>
    ''' Data for each channel.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks>All sidescan channels will always precede the bathymetry channels.</remarks>
    Public ReadOnly Property ChannelInfo As List(Of ChanInfo)
        Get
            Return inChanInfo
        End Get
    End Property

    ''' <summary>
    ''' Create an empty xtf file header
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub New()
        RecordingProgramName = "nd"
        RecordingProgramVersion = "223"
        SonarName = "nd"
        SonarType = 0
        NoteString = "nd"
        ThisFileName = "nd"
        NavigationCoordinateUnits = 0
        NumberOfSonarChannels = 0
        NumberOfBathymetryChannels = 0
        NumberOfSnippetChannels = 0
        NumberOfForwardLookArrays = 0
        NumberOfEchoStrengthChannels = 0
        NumberOfInterferometryChannels = 0
        ReferencePointHeight = 0
        NavigationLatency = 0
        NavigationOffsetY = 0
        NavigationOffsetX = 0
        NavigationOffsetZ = 0
        NavigationOffsetYaw = 0
        MRUOffsetY = 0
        MRUOffsetX = 0
        MRUOffsetZ = 0
        MRUOffsetYaw = 0
        MRUOffsetPitch = 0
        MRUOffsetRoll = 0
        ChannelInfo.Clear()
        ChannelInfo.Add(New ChanInfo)
    End Sub

    ''' <summary>
    ''' Create the xtf header from the given bytes
    ''' </summary>
    ''' <param name="ByteArray">1024 byte or more that contain the header of the xtf file</param>
    ''' <remarks></remarks>
    Public Sub New(byteArray As Byte())

        Using xtf As New BinaryReader(ByteArrayToMemoryStream(byteArray))
            xtf.ReadByte() 'FileFormat
            xtf.ReadByte() 'SystemType
            RecordingProgramName = xtf.ReadChars(8)
            RecordingProgramVersion = xtf.ReadChars(8)
            SonarName = xtf.ReadChars(16)
            SonarType = xtf.ReadUInt16
            NoteString = xtf.ReadChars(64)
            ThisFileName = xtf.ReadChars(64)
            NavigationCoordinateUnits = xtf.ReadUInt16
            NumberOfSonarChannels = xtf.ReadUInt16
            NumberOfBathymetryChannels = xtf.ReadUInt16
            NumberOfSnippetChannels = xtf.ReadByte
            NumberOfForwardLookArrays = xtf.ReadByte
            NumberOfEchoStrengthChannels = xtf.ReadUInt16
            NumberOfInterferometryChannels = xtf.ReadByte
            xtf.ReadByte()   'Reserved1
            xtf.ReadUInt16() 'Reserved2
            ReferencePointHeight = xtf.ReadSingle
            xtf.ReadBytes(12)   'ProjectionType Not currently used
            xtf.ReadBytes(10)   'SpheroidType Not currently used
            NavigationLatency = xtf.ReadInt32
            xtf.ReadSingle() 'OriginY Not currently used
            xtf.ReadSingle() 'OriginX Not currently used
            NavigationOffsetY = xtf.ReadSingle
            NavigationOffsetX = xtf.ReadSingle
            NavigationOffsetZ = xtf.ReadSingle
            NavigationOffsetYaw = xtf.ReadSingle
            MRUOffsetY = xtf.ReadSingle
            MRUOffsetX = xtf.ReadSingle
            MRUOffsetZ = xtf.ReadSingle
            MRUOffsetYaw = xtf.ReadSingle
            MRUOffsetPitch = xtf.ReadSingle
            MRUOffsetRoll = xtf.ReadSingle
            ChannelInfo.Clear()
            For ch = 0 To NumberOfSonarChannels + NumberOfBathymetryChannels - 1
                ChannelInfo.Add(New ChanInfo(xtf.ReadBytes(128)))
            Next
        End Using
    End Sub

End Class
