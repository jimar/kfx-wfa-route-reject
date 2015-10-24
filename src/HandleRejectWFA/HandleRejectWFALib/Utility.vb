''' <summary>
''' Module for utility functions
''' </summary>
''' <remarks></remarks>
Module Utility

#Region "Singleton ConfigSettings object"

    Private _settings As ConfigSettings = Nothing

    ''' <summary>
    ''' Configuration settings object
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public ReadOnly Property Settings As ConfigSettings
        Get

            If _settings Is Nothing Then _settings = New ConfigSettings
            Return _settings

        End Get
    End Property

#End Region

    ''' <summary>
    ''' Ensures the path ends in a backslash
    ''' </summary>
    ''' <param name="path"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function PathFix(ByVal path As String) As String
        If Right(path, 1) <> "\" Then
            path = path & "\"
        End If
        Return path
    End Function

End Module
