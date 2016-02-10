Imports Windows.ApplicationModel.Resources
Imports Windows.ApplicationModel.Resources.Core

''' <summary>
''' Resource manager with fallback for the UI strings
''' </summary>
Public Class AppStrings
    Dim _AppAboutTitle, _AppAboutText, _AppAboutWebLink, _AppAboutButton, _AppAboutVisitWeb As String
    Dim _AppAvailGroupChannelInfo, _AppAvailGroupHeader, _AppAvailGroupPacketGroup, _AppOpenFile As String

    Public ReadOnly Property AppAboutTitle As String
        Get
            Return _AppAboutTitle
        End Get
    End Property

    Public Shared ReadOnly AppAboutTitleProperty As DependencyProperty =
                           DependencyProperty.Register("AppAboutTitle",
                           GetType(String), GetType(AppStrings),
                           New PropertyMetadata(Nothing))

    Public ReadOnly Property AppAboutText As String
        Get
            Return _AppAboutText
        End Get
    End Property

    Public Shared ReadOnly AppAboutTextProperty As DependencyProperty =
                           DependencyProperty.Register("AppAboutText",
                           GetType(String), GetType(AppStrings),
                           New PropertyMetadata(Nothing))

    Public ReadOnly Property AppAboutWebLink As String
        Get
            Return _AppAboutWebLink
        End Get
    End Property

    Public Shared ReadOnly AppAboutWebLinkProperty As DependencyProperty =
                           DependencyProperty.Register("AppAboutWebLink",
                           GetType(String), GetType(AppStrings),
                           New PropertyMetadata(Nothing))

    Public ReadOnly Property AppAboutButton As String
        Get
            Return _AppAboutButton
        End Get
    End Property

    Public Shared ReadOnly AppAboutButtonProperty As DependencyProperty =
                           DependencyProperty.Register("AppAboutButton",
                           GetType(String), GetType(AppStrings),
                           New PropertyMetadata(Nothing))

    Public ReadOnly Property AppAboutVisitWeb As String
        Get
            Return _AppAboutVisitWeb
        End Get
    End Property

    Public Shared ReadOnly AppAboutVisitWebProperty As DependencyProperty =
                           DependencyProperty.Register("AppAboutVisitWeb",
                           GetType(String), GetType(AppStrings),
                           New PropertyMetadata(Nothing))

    Public ReadOnly Property AppAvailGroupChannelInfo As String
        Get
            Return _AppAvailGroupChannelInfo
        End Get
    End Property

    Public Shared ReadOnly AppAvailGroupChannelInfoProperty As DependencyProperty =
                           DependencyProperty.Register("AppAvailGroupChannelInfo",
                           GetType(String), GetType(AppStrings),
                           New PropertyMetadata(Nothing))

    Public ReadOnly Property AppAvailGroupHeader As String
        Get
            Return _AppAvailGroupHeader
        End Get
    End Property

    Public Shared ReadOnly AppAvailGroupHeaderProperty As DependencyProperty =
                           DependencyProperty.Register("AppAvailGroupHeader",
                           GetType(String), GetType(AppStrings),
                           New PropertyMetadata(Nothing))

    Public ReadOnly Property AppAvailGroupPacketGroup As String
        Get
            Return _AppAvailGroupPacketGroup
        End Get
    End Property

    Public Shared ReadOnly AppAvailGroupPacketGroupProperty As DependencyProperty =
                           DependencyProperty.Register("AppAvailGroupPacketGroup",
                           GetType(String), GetType(AppStrings),
                           New PropertyMetadata(Nothing))

    Public ReadOnly Property AppOpenFile As String
        Get
            Return _AppOpenFile
        End Get
    End Property

    Public Shared ReadOnly AppOpenFileProperty As DependencyProperty =
                           DependencyProperty.Register("AppOpenFile",
                           GetType(String), GetType(AppStrings),
                           New PropertyMetadata(Nothing))


    Public Sub New()
        'Default values
        _AppAboutTitle = "Information"
        _AppAboutText = "Version: {0}.{1}.{2}.{3}" & vbCrLf & "The MIT License (MIT)" & vbCrLf & "Copyright (c) 2016 Simone Giacomoni"
        _AppAboutWebLink = "http://jaksg82.github.io/XtfViewer"
        _AppAboutVisitWeb = "Visit the web site"
        _AppAboutButton = "About"
        _AppAvailGroupChannelInfo = "Channel {0} Information"
        _AppAvailGroupHeader = "Main Header"
        _AppAvailGroupPacketGroup = "Packets of type: {0}"
        _AppOpenFile = "Open"

        Dim tmpValue As String

        Try
            tmpValue = ResourceLoader.GetForCurrentView().GetString("AppAboutTitle")
            If Not String.IsNullOrEmpty(tmpValue) Then _AppAboutTitle = tmpValue
            tmpValue = ""
            tmpValue = ResourceLoader.GetForCurrentView().GetString("AppAboutText")
            If Not String.IsNullOrEmpty(tmpValue) Then _AppAboutText = tmpValue
            tmpValue = ""
            tmpValue = ResourceLoader.GetForCurrentView().GetString("AppAboutWebLink")
            If Not String.IsNullOrEmpty(tmpValue) Then _AppAboutWebLink = tmpValue
            tmpValue = ""
            tmpValue = ResourceLoader.GetForCurrentView().GetString("AppAboutVisitWeb")
            If Not String.IsNullOrEmpty(tmpValue) Then _AppAboutVisitWeb = tmpValue
            tmpValue = ""
            tmpValue = ResourceLoader.GetForCurrentView().GetString("AppAboutButton")
            If Not String.IsNullOrEmpty(tmpValue) Then _AppAboutButton = tmpValue
            tmpValue = ""
            tmpValue = ResourceLoader.GetForCurrentView().GetString("AppAvailGroupChannelInfo")
            If Not String.IsNullOrEmpty(tmpValue) Then _AppAvailGroupChannelInfo = tmpValue
            tmpValue = ""
            tmpValue = ResourceLoader.GetForCurrentView().GetString("AppAvailGroupHeader")
            If Not String.IsNullOrEmpty(tmpValue) Then _AppAvailGroupHeader = tmpValue
            tmpValue = ""
            tmpValue = ResourceLoader.GetForCurrentView().GetString("AppAvailGroupPacketGroup")
            If Not String.IsNullOrEmpty(tmpValue) Then _AppAvailGroupPacketGroup = tmpValue
            tmpValue = ""
            tmpValue = ResourceLoader.GetForCurrentView().GetString("AppOpenFile")
            If Not String.IsNullOrEmpty(tmpValue) Then _AppOpenFile = tmpValue
            tmpValue = ""
        Catch ex As Exception
            'Do nothing
        End Try

    End Sub






End Class
