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

    ''' <summary>
    ''' Entry point into the workflow agent
    ''' </summary>
    ''' <param name="oWorkflowData"></param>
    ''' <remarks></remarks>
    Public Sub ProcessWorkflow(ByRef oWorkflowData As IACWorkflowData) Implements IACWorkflowAgent.ProcessWorkflow

        ' TODO

    End Sub

End Class
