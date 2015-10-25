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


    ''' <summary>
    ''' Returns the name of the current machine
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetMachineName() As String
        Return System.Environment.MachineName
    End Function

    ''' <summary>
    ''' Returns the ID of the current process
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetProcessID() As String
        Return System.Diagnostics.Process.GetCurrentProcess().Id.ToString()
    End Function


    ''' <summary>
    ''' Pads the origVal parameter by adding padPrefix to the beginning and padSuffix to the end
    ''' as long as the string is less than maxLen
    ''' </summary>
    ''' <param name="origVal"></param>
    ''' <param name="padPrefix"></param>
    ''' <param name="padSuffix"></param>
    ''' <param name="maxLen"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function PadString(ByVal origVal As String, ByVal padPrefix As String, ByVal padSuffix As String, ByVal maxLen As Integer) As String

        Dim newVal As String = padPrefix & origVal & padSuffix
        If origVal.Length >= maxLen Then
            Return origVal
        ElseIf newVal.Length > maxLen Then
            Return origVal  ' The new value is longer than the max. Stay with the original value.
        Else
            Return PadString(newVal, padPrefix, padSuffix, maxLen)
        End If

    End Function

End Module
