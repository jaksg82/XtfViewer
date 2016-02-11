Imports XtfLibrary

''' <summary>
''' Represent the group object of the XtfIndex class
''' </summary>
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
