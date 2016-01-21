' Channel information structure (contained in the file header).
' One-time information describing each channel.  64 bytes long.
' This Is data pertaining to each channel that will Not change
' during the course of a run.

''' <summary>
''' Channel information structure (contained in the file header).
''' One-time information describing each channel.
''' This is data pertaining to each channel that will NOT change during the course of a run.
''' </summary>
''' <remarks></remarks>
Public Class ChanInfo

    Dim inChannelName As String      ' Text describing channel.  i.e., "Port 500"

    ''' <summary>
    ''' Type of the channel:
    ''' 0 = Subbottom
    ''' 1 = Port
    ''' 2 = Starboard
    ''' 3 = Bathymetry
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property TypeOfChannel As Byte 'xtfTypeOfChannel

    ''' <summary>
    ''' Index for which ChanInfo structure this is.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property SubChannelNumber As Byte

    ''' <summary>
    ''' Sonar imagery stored as:
    ''' 1 = Slant-range (Raw)
    ''' 2 = Ground-range (Corrected)
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property CorrectionFlags As UInt16

    ''' <summary>
    ''' Polarity of the data.
    ''' 0 = data is bipolar
    ''' 1 = data is unipolar
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property Unipolar As UInt16

    ''' <summary>
    ''' Bytes per sample
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property BytesPerSample As UInt16

    ''' <summary>
    ''' Samples recorded in each ping of this channel. Usually a multiple of 1024 unless bathymetry.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks>To be used only for compability with some analog systems.</remarks>
    Public Property SamplesPerChannel As UInt32

    ''' <summary>
    ''' Text describing channel.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks>Max 16 chars.</remarks>
    Public Property ChannelName As String
        Get
            Return inChannelName
        End Get
        Set(value As String)
            If String.IsNullOrEmpty(value) Then value = " "c
            Dim tmpvalue As String = value.Replace(ChrW(0), " "c)
            If tmpvalue.Length > 16 Then
                inChannelName = tmpvalue.Substring(0, 16)
            Else
                inChannelName = tmpvalue.PadRight(16)
            End If
        End Set
    End Property

    Public Property VoltScale As Single           ' How many volts is represented by max sample value.  Typically 5.0.
    Public Property Frequency As Single           ' Center transmit frequency
    Public Property HorizontalBeamAngle As Single ' Typically 1 degree or so
    Public Property TiltAngle As Single           ' Typically 30 degrees
    Public Property BeamWidth As Single           ' 3dB beam width, Typically 50 degrees

    ' Orientation of these offsets:
    ' Positive Y is forward
    ' Positive X is to starboard
    ' Positive Z is down.  Just like depth.
    ' Positive roll is lean to starboard
    ' Positive pitch is nose up
    ' Positive yaw is turn to right

    Public Property OffsetX As Single   ' These offsets are entered in the Multibeam setup dialog box.
    Public Property OffsetY As Single
    Public Property OffsetZ As Single

    Public Property OffsetYaw As Single  ' If the multibeam sensor is reverse mounted (facing backwards), then OffsetYaw will be around 180 degrees.
    Public Property OffsetPitch As Single
    Public Property OffsetRoll As Single

    Public Property BeamsPerArray As UInt16
    'Public Property SampleFormat As Byte 'xtfChannelSampleFormat
    'Public Property Latency As Single

    ''' <summary>
    ''' Create an empty ChanInfo.
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub New()
        TypeOfChannel = 0
        SubChannelNumber = 0
        CorrectionFlags = 0
        Unipolar = 0
        BytesPerSample = 0
        SamplesPerChannel = 0
        ChannelName = " "
        VoltScale = 0
        Frequency = 0
        HorizontalBeamAngle = 0
        TiltAngle = 0
        BeamWidth = 0
        OffsetX = 0
        OffsetY = 0
        OffsetZ = 0
        OffsetYaw = 0
        OffsetPitch = 0
        OffsetRoll = 0
        BeamsPerArray = 0
        'Latency = 0
        'SampleFormat = 0
    End Sub

    ''' <summary>
    ''' Create the ChanInfo for header from the given bytes.
    ''' </summary>
    ''' <param name="ByteArray">128 bytes that contain the informations.</param>
    ''' <remarks></remarks>
    Public Sub New(byteArray As Byte())
        TypeOfChannel = 0
        SubChannelNumber = 0
        CorrectionFlags = 0
        Unipolar = 0
        BytesPerSample = 0
        SamplesPerChannel = 0
        ChannelName = " "
        VoltScale = 0
        Frequency = 0
        HorizontalBeamAngle = 0
        TiltAngle = 0
        BeamWidth = 0
        OffsetX = 0
        OffsetY = 0
        OffsetZ = 0
        OffsetYaw = 0
        OffsetPitch = 0
        OffsetRoll = 0
        BeamsPerArray = 0
        'Latency = 0
        'SampleFormat = 0

        Using ip As New BinaryReader(ByteArrayToMemoryStream(byteArray))
            TypeOfChannel = ip.ReadByte
            SubChannelNumber = ip.ReadByte
            CorrectionFlags = ip.ReadUInt16
            Unipolar = ip.ReadUInt16
            BytesPerSample = ip.ReadUInt16
            SamplesPerChannel = ip.ReadUInt32
            ChannelName = ip.ReadChars(16)
            VoltScale = ip.ReadSingle
            Frequency = ip.ReadSingle
            HorizontalBeamAngle = ip.ReadSingle
            TiltAngle = ip.ReadSingle
            BeamWidth = ip.ReadSingle
            OffsetX = ip.ReadSingle
            OffsetY = ip.ReadSingle
            OffsetZ = ip.ReadSingle
            OffsetYaw = ip.ReadSingle
            OffsetPitch = ip.ReadSingle
            OffsetRoll = ip.ReadSingle
            BeamsPerArray = ip.ReadUInt16
            'Latency = ip.ReadSingle

        End Using
    End Sub

End Class
