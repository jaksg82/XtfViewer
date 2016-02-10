
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

