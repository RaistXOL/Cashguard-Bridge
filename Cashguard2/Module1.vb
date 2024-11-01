﻿Imports System.IO
Imports System.Net
Imports System.Net.Sockets
Imports System.Text
Imports System.Threading.Tasks
Imports System.Runtime.CompilerServices

Module Module1
    Public aWindows(3, 5) As Integer
    Public nWindowsCount As Byte = 0
    Public aLevelWarning(16, 2) As String


    Public Const CG_STATUS_OK = 0
    Public Const CG_STATUS_BUSY = 1
    Public Const CG_STATUS_ERROR = 2
    Public Const CG_STATUS_WARNING = 3
    Public Const CG_STATUS_INPUT_PARAM_ERROR = 4
    Public Const CG_STATUS_CLOSED = 5
    Public Const CG_STATUS_TIMEOUT = 6
    Public Const CG_STATUS_SENDFAILED = 7
    Public Const CG_STATUS_PAYOUT_REST = 8
    Public Const CG_STATUS_PORT_ERROR = 9
    Public Const CG_STATUS_PAYCLEAR_ERROR = 10
    Public Const CG_STATUS_PAYOUT_LIMIT = 11
    Public Const CG_STATUS_NOT_SUPPORTED = 12

    Public Const CG_LEVEL_WARNING_EMPTY = 1
    Public Const CG_LEVEL_WARNING_LOW = 2
    Public Const CG_LEVEL_WARNING_HIGH = 3
    Public Const CG_LEVEL_WARNING_BLOCK = 4

    Public cFileLog As String = "LOG\" + DateTime.Today.ToString("dd_MM_yyyy") + ".LOG"

    Public oTCPClient As New Net.Sockets.TcpClient

    Public oLog As New IO.StreamWriter(File.Open(cFileLog, FileMode.Append, FileAccess.Write, FileShare.ReadWrite))

    Public Function WriteLogLine(ByVal cBuffer As String) As String

        Dim cLogString As String = ""

        cLogString = DateTime.Now.ToString("HH:mm:ss ") + cBuffer

        Return cLogString
    End Function


    Public Function ReceiveTCP(ByVal cIP As String, ByVal nPort As Integer, ByVal cPacket As String, Optional ByVal cWaitFor As String = "") As String
        Dim aBuffer(16384) As Byte
        Dim cBuffer As String = ""
        Dim nBytesRead

        Try

            If Not IsNothing(oTCPClient) AndAlso Not oTCPClient.Connected Then
                oTCPClient.Connect(New Net.IPEndPoint(IPAddress.Parse(cIP), nPort))
                oLog.WriteLine(WriteLogLine("(DBG) connessione a " + cIP + " porta " + nPort.ToString))
            End If

            Dim oTCPStream As NetworkStream = oTCPClient.GetStream

            If Not cPacket = "" Then
                oLog.WriteLine(WriteLogLine("(DBG) >> " + cPacket))
                oTCPStream.Write(System.Text.Encoding.Default.GetBytes(cPacket), 0, System.Text.Encoding.Default.GetBytes(cPacket).Length)
            End If


            Do While True

                If oTCPClient.Available > 0 Then
                    nBytesRead = oTCPStream.Read(aBuffer, 0, aBuffer.Length)

                    cBuffer = Encoding.UTF8.GetString(aBuffer).Substring(0, nBytesRead)
                    Console.Write(cBuffer)
                    oLog.WriteLine(WriteLogLine("(DBG) << " + cBuffer))
                End If

                If cWaitFor = "" Or (Not cWaitFor = "" And cBuffer.Contains(cWaitFor)) Then
                    Exit Do
                End If
            Loop

            'oTCPStream.Close()

        Catch ex As Exception

            MsgBox(ex.ToString)
            frm_main.cnt_status_general.Text = "cashprotect offline"
            frm_main.cnt_status_levelwarning.Image = My.Resources._error

        End Try

        Return cBuffer

    End Function

    Public oLogStream As FileStream

End Module


Module StringExtensions

    <Extension>
    Public Function ContainsAny(source As String, ParamArray values As String()) As Boolean
        Return values.Any(Function(s) source.Contains(s))
    End Function

End Module
