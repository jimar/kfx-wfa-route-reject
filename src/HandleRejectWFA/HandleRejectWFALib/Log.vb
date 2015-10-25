Imports System.IO

''' <summary>
''' Module that encapsulates logging functionality
''' </summary>
''' <remarks></remarks>
Module Log

    ' Format for Log Name
    Private Const LOG_NAME_FORMAT As String = "HandleRejectWFA_{0}_{1}_{2}.txt"

    ''' <summary>
    ''' Writes a log to a log file for this library using a generated file name based
    ''' on the date and the process.
    ''' </summary>
    ''' <param name="directory"></param>
    ''' <param name="message"></param>
    ''' <remarks></remarks>
    Public Sub WriteLog(ByVal directory As String, ByVal message As String)

        directory = Utility.PathFix(directory)

        ' Make sure the directory exists
        If Not System.IO.Directory.Exists(directory) Then
            System.IO.Directory.CreateDirectory(directory)
        End If

        ' Generate the log file name
        Dim dtmNow As Date = Date.UtcNow
        Dim yearMonth As String = dtmNow.Year.ToString() & "-" & Utility.PadString(dtmNow.Month.ToString(), "0", "", 2)
        Dim logFileName As String = String.Format(LOG_NAME_FORMAT, _
                                                  Utility.GetMachineName(), _
                                                  Utility.GetProcessID(), _
                                                  yearMonth)

        ' Make sure the file exists
        Dim logFilePath As String = directory & logFileName
        Dim writer As TextWriter
        If Not File.Exists(logFilePath) Then
            writer = File.CreateText(logFilePath)
        Else
            writer = File.AppendText(logFilePath)
        End If

        ' Format the message
        Dim formattedMsg As String = dtmNow.ToString() & vbTab & message

        ' Append the text and close
        writer.WriteLine(formattedMsg)
        writer.Close()
        writer = Nothing

    End Sub

End Module
