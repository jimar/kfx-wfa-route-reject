Imports System
Imports System.Runtime.InteropServices
Imports Kofax.Capture.ACWFLib
Imports Kofax.Capture.SDK.Workflow
Imports Kofax.Capture.SDK.Data

<GuidAttribute("1D360D54-E93F-410B-9757-3881EA65A505"), _
ClassInterface(ClassInterfaceType.None), _
ProgId("HandleRejectWFALib.Agent"), _
CLSCompliant(False)> _
Public Class Agent
    Implements IACWorkflowAgent

    Private Const STATUS_READY As String = "Ready"
    Private Const STATUS_ERROR As String = "Error"

    Private _workflowData As IACWorkflowData
    Private _runtimeElement As IACDataElement
    Private _batchElement As IACDataElement
    Private _batchClass As String
    Private _modules As New Dictionary(Of String, IACWorkflowModule) ' Keep a dictionary of modules, keying off the module name

    ''' <summary>
    ''' Entry point into the workflow agent
    ''' </summary>
    ''' <param name="oWorkflowData"></param>
    ''' <remarks></remarks>
    Public Sub ProcessWorkflow(ByRef oWorkflowData As IACWorkflowData) Implements IACWorkflowAgent.ProcessWorkflow

        Try

            _workflowData = oWorkflowData
            _runtimeElement = _workflowData.ExtractRuntimeACDataElement(0)
            _batchElement = _runtimeElement.FindChildElementByName("Batch")
            _batchClass = _batchElement("BatchClassName")
            _workflowData.PossibleModules.AddChildrenToDictionary(_modules)

            ' Get the configuration settings for this batch class
            Dim batchClassConfig As BatchClassConfig = Utility.Settings.GetBatchClassSettings(_batchClass)

            If batchClassConfig IsNot Nothing Then

                If _workflowData.NextState.Name = STATUS_ERROR Then

                    ' Check if there are any documents rejected
                    Dim errMsg As String = ""
                    Dim rejectReasons As String = ""
                    If AnyDocumentsRejected(rejectReasons) Or _workflowData.ErrorText <> "" Then

                        ' Format the error message
                        errMsg = IIf(_workflowData.ErrorText <> "", _workflowData.ErrorText, "") & IIf(rejectReasons <> "", ": ", "") & rejectReasons

                    Else

                        ' The state of the batch is Error but we don't have any error information.
                        ' Use a boilerplate error description.
                        errMsg = Utility.Settings.GenericErrorMessage

                    End If

                    ' Write the error message to the batch field if we have a configured value
                    If batchClassConfig.ErrorMsgBatchField <> "" Then
                        WriteBatchField(batchClassConfig.ErrorMsgBatchField, errMsg)
                    End If

                    ' Route the batch
                    RouteBatch(batchClassConfig.RouteModuleName, STATUS_READY)

                End If

            Else

                ' This batch class is not configured. Write to the log but don't do anything.
                Log.WriteLog(Utility.Settings.LogDirectory, "Batch class '" & _batchClass & "' is not configured. No action taken.")

            End If

        Catch ex As Exception

            ExceptionHandler.HandleException(ex)

            ' We don't expect to get here -- Update the Error Text with the exception message
            _workflowData.ErrorText = "Unhandled exception in Workflow Agent: " & ex.Message

        End Try

    End Sub


    ''' <summary>
    ''' Writes the batch field
    ''' </summary>
    ''' <param name="name"></param>
    ''' <param name="value"></param>
    ''' <remarks></remarks>
    Private Sub WriteBatchField(ByVal name As String, ByVal value As String)

        Try

            Dim batchFieldsElem As IACDataElement = _batchElement.FindChildElementByName("BatchFields")
            Dim batchFieldElem As IACDataElement = batchFieldsElem.FindChildElementByAttribute("BatchField", "Name", name)
            If batchFieldElem IsNot Nothing Then
                batchFieldElem("Value") = value
            End If

        Catch ex As Exception

            ' Log the error but don't blow up.
            ExceptionHandler.HandleException(ex)

        End Try


    End Sub

    ''' <summary>
    ''' Routes the batch
    ''' </summary>
    ''' <param name="moduleName"></param>
    ''' <param name="state"></param>
    ''' <remarks></remarks>
    Private Sub RouteBatch(ByVal moduleName As String, ByVal state As String)

        _workflowData.NextState = _workflowData.PossibleStates(state)
        _workflowData.NextModule = _modules(moduleName)

    End Sub

    ''' <summary>
    ''' Checks if any of the documents are rejected and returns the reject reasons for all of the documents that are rejected.
    ''' </summary>
    ''' <param name="rejectNote"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function AnyDocumentsRejected(ByRef rejectNote As String) As Boolean

        Dim docsElem As IACDataElement = _batchElement.FindChildElementByName("Documents")
        Dim docElemCol As IACDataElementCollection = docsElem.FindChildElementsByName("Document")

        rejectNote = ""
        For idx As Integer = 1 To docElemCol.Count

            Dim docElem As IACDataElement = docElemCol(idx)
            If docElem("Rejected") = "1" Then
                rejectNote &= IIf(rejectNote <> "", "; ", "") & "Document " & idx & " rejected: " & docElem("Note")
            End If

        Next
        Return rejectNote <> ""

    End Function

End Class
