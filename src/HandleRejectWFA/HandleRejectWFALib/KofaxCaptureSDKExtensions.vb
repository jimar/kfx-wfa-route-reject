Imports System.Runtime.CompilerServices
Imports Kofax.Capture.SDK.Workflow

''' <summary>
''' Extension functions on Kofax Capture SDK objects/types
''' </summary>
''' <remarks></remarks>
Module KofaxCaptureSDKExtensions

    <Extension()> _
    Public Sub AddChildrenToDictionary(ByVal obj As IACWorkflowModules, ByRef dict As Dictionary(Of String, IACWorkflowModule))

        For Each m As IACWorkflowModule In obj
            dict.Add(m.Name, m)
        Next

    End Sub

End Module
