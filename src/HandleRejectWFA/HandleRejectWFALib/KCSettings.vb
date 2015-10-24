''' <summary>
''' Utility class that encapsulates Kofax Capture settings. Mostly from the registry.
''' </summary>
''' <remarks></remarks>
Class KCSettings

    Private Const REG_KC_SUBKEY As String = "HKEY_LOCAL_MACHINE\Software\Kofax Image Products\Ascent Capture\3.0"
    Private Const REG_KC_SERVER_PATH As String = "ServerPath"

    ''' <summary>
    ''' Kofax Capture Server Path
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared ReadOnly Property ServerPath As String
        Get
            Return GetKofaxCaptureRegValue(REG_KC_SERVER_PATH)
        End Get
    End Property

    ''' <summary>
    ''' Accesses registry and returns the Kofax Capture value
    ''' </summary>
    ''' <param name="valueName"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Shared Function GetKofaxCaptureRegValue(ByVal valueName As String) As String

        Return My.Computer.Registry.GetValue(REG_KC_SUBKEY, valueName, Nothing)

    End Function

End Class
