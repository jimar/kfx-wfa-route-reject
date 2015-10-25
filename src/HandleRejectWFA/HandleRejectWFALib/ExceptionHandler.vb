
''' <summary>
''' Module that encapsulates exception handling functionality
''' </summary>
''' <remarks></remarks>
Module ExceptionHandler

    ''' <summary>
    ''' Logs exceptions and optionally rethrows them
    ''' </summary>
    ''' <param name="ex"></param>
    ''' <param name="rethrow"></param>
    ''' <remarks></remarks>
    Public Sub HandleException(ByVal ex As Exception, Optional ByVal rethrow As Boolean = False)

        ' Generate the message
        Dim msg As String = ex.Message & vbCrLf & ex.StackTrace
        If Not ex.InnerException Is Nothing Then
            msg = msg & vbCrLf & "Inner Exception: " & ex.InnerException.Message & vbCrLf & ex.InnerException.StackTrace
        End If

        Dim logDirectory As String = "C:\"      ' Default -- just in case we haven't created the settings object yet
        If Not Utility.Settings Is Nothing Then
            logDirectory = Settings.LogDirectory
        End If

        Log.WriteLog(logDirectory, msg)

        If rethrow Then
            Throw ex
        End If

    End Sub

End Module