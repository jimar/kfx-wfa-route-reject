Imports System.Xml

''' <summary>
''' Encapsulates some XML functions
''' </summary>
''' <remarks></remarks>
Module XmlHelper

    ''' <summary>
    ''' Returns the attribute value. If the attribute does not exist, it returns empty string.
    ''' </summary>
    ''' <param name="node"></param>
    ''' <param name="attributeName"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetAttributeValueSafe(ByVal node As XmlNode, ByVal attributeName As String) As String

        Dim val As String = ""
        Dim attribute As XmlNode = node.Attributes.GetNamedItem(attributeName)
        If Not attribute Is Nothing Then
            val = attribute.Value
        End If
        GetAttributeValueSafe = val

    End Function

    ''' <summary>
    ''' Returns the inner text value for a selected node.
    ''' </summary>
    ''' <param name="doc"></param>
    ''' <param name="xpath"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetTextValue(ByVal doc As XmlDocument, ByVal xpath As String) As String

        Dim outval As String = ""
        Dim node As XmlNode = doc.SelectSingleNode(xpath)
        If node IsNot Nothing Then
            outval = node.InnerText
        End If

        Return outval

    End Function

    ''' <summary>
    ''' Formats the attribute query into XPath format
    ''' </summary>
    ''' <param name="name"></param>
    ''' <param name="value"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function FormatXPathAttributeQuery(ByVal name As String, ByVal value As String) As String
        Return "[@" & name & "='" & value & "']"
    End Function

End Module
