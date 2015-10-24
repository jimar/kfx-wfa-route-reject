Imports System.Collections.Generic
Imports System.Xml

''' <summary>
''' Configuration settings for this workflow agent
''' </summary>
''' <remarks>The configuration file should be named HandleRejectWFAConfig.xml and stored in the CaptureSV\Config directory.</remarks>
Class ConfigSettings

    ' Path of the config file
    Private Const CONFIG_FILE_PATH_FORMAT As String = "{0}Config\HandleRejectWFAConfig.xml"

    ' Xml nodes for config file
    Private Const XML_ROOT_NODE As String = "Config"
    Private Const XML_NODE_BATCH_CLASS_CONFIG As String = "BatchClassConfig"
    Private Const XML_PROP_BATCH_CLASS As String = "BatchClass"
    Private Const XML_NODE_ERR_MSG_BATCH_FIELD As String = "ErrorMsgBatchField"
    Private Const XML_NODE_ROUTE_TO_MODULE_ID As String = "RouteToModuleName"
    Private Const XML_NODE_GENERIC_ERROR_MESSAGE As String = "GenericErrorMessage"

    Private _batchClassSettings As Dictionary(Of String, BatchClassConfig) = Nothing
    Private _genericErrorMessage As String = Nothing

    ''' <summary>
    ''' Generic error message. Used when no other error message could be determined.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public ReadOnly Property GenericErrorMessage As String
        Get
            Return _genericErrorMessage
        End Get
    End Property

    ''' <summary>
    ''' Batch class level configuration settings
    ''' </summary>
    ''' <param name="batchClass">Name of the batch class</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetBatchClassSettings(ByVal batchClass As String) As BatchClassConfig
        Return _batchClassSettings(batchClass)
    End Function

    Public Sub New()

        ' Derive the config path
        Dim configFile As String = GetConfigFilePath()

        If System.IO.File.Exists(configFile) Then

            Try

                Dim xd As XmlDocument = New XmlDocument
                xd.Load(configFile)

                ' Generic error message
                _genericErrorMessage = GetTextValue(xd, XML_ROOT_NODE & "/" & XML_NODE_GENERIC_ERROR_MESSAGE)

                Dim batchClassConfigNodeList As XmlNodeList = xd.SelectNodes(XML_ROOT_NODE & "/" & XML_NODE_BATCH_CLASS_CONFIG)
                If Not batchClassConfigNodeList Is Nothing Then

                    _batchClassSettings = New Dictionary(Of String, BatchClassConfig)
                    For Each batchClassConfigNode As XmlNode In batchClassConfigNodeList

                        Dim b As BatchClassConfig = New BatchClassConfig
                        b.BatchClass = GetAttributeValueSafe(batchClassConfigNode, XML_PROP_BATCH_CLASS)

                        Dim xpathBatchClass As String = XML_ROOT_NODE & "/" & XML_NODE_BATCH_CLASS_CONFIG & FormatXPathAttributeQuery(XML_PROP_BATCH_CLASS, b.BatchClass) & "/"

                        b.ErrorMsgBatchField = GetTextValue(xd, xpathBatchClass & XML_NODE_ERR_MSG_BATCH_FIELD)
                        b.RouteModuleName = GetTextValue(xd, xpathBatchClass & XML_NODE_ROUTE_TO_MODULE_ID)

                        ' Add to the dictionary
                        _batchClassSettings.Add(b.BatchClass, b)

                    Next

                End If

                xd = Nothing

            Catch ex As Exception

                ' TODO: Any reason there might be some mistyping in the configuration file

            End Try

        Else
            Throw New ConfigFileDoesNotExistException()
        End If

    End Sub

    Private Function GetConfigFilePath() As String
        Return String.Format(CONFIG_FILE_PATH_FORMAT, PathFix(KCSettings.ServerPath))
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