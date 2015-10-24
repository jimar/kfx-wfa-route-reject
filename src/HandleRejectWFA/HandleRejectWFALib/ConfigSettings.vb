Imports System.Collections.Generic
Imports System.Xml

''' <summary>
''' Configuration settings for this workflow agent
''' </summary>
''' <remarks>The configuration file should be named HandleRejectWFAConfig.xml and stored in the CaptureSV\Config directory.</remarks>
Class ConfigSettings

    Private Const CONFIG_FILE_NAME As String = "HandleRejectWFAConfig.xml"

    Private _batchClassSettings As Dictionary(Of String, BatchClassConfig) = Nothing

    ''' <summary>
    ''' Generic error message. Used when no other error message could be determined.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property GenericErrorMessage As String

    ''' <summary>
    ''' Batch class level configuration settings
    ''' </summary>
    ''' <param name="batchClass">Name of the batch class</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetBatchClassSettings(ByVal batchClass As String) As BatchClassConfig
        Return Nothing
    End Function

End Class

''' <summary>
''' Batch class level configuration settings
''' </summary>
''' <remarks></remarks>
Class BatchClassConfig

    ''' <summary>
    ''' Name of the batch class
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property BatchClass As String
    ''' <summary>
    ''' Batch field that stores the error message
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property ErrorMsgBatchField As String
    ''' <summary>
    ''' Name of the module to route to if we find an error
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property RouteModuleName As String

End Class