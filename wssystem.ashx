<%@ WebHandler Language="VB" Class="wssystem" %>

Imports System
Imports System.Web

Public Class wssystem : Implements IHttpHandler
    
    Public Sub ProcessRequest(ByVal context As HttpContext) Implements IHttpHandler.ProcessRequest
        Dim auxData As String
       
        If HttpContext.Current.Request.InputStream Is Nothing Then
            Exit Sub
        End If
        Dim auxInputLenght As Integer = HttpContext.Current.Request.InputStream.Length
        If auxInputLenght < 301 Then
            Exit Sub
        End If
        Dim auxSReader As New IO.StreamReader(HttpContext.Current.Request.InputStream)
        auxData = auxSReader.ReadToEnd
        Dim auxType As Integer = Val(Left(auxData, 2).Trim)
        Dim pPsw As String = Mid(auxData, 3, 300).Trim
        Dim pData As String = Mid(auxData, 303, auxInputLenght - 302).Trim
        If pData <> "" Then
            HttpContext.Current.Server.ScriptTimeout = 60 * 60
            'Dim auxClass As New clscusinControl
            Dim auxIP As String = HttpContext.Current.Request.ServerVariables("REMOTE_ADDR")
            Dim auxReturn As String = ""
            Dim auxClass As New clsCusimDOC 
            If auxClass.DebugLogOn Then
                auxClass.gSys_DebugLogAdd("wssystem.ashx-Receive")
            End If
            If auxClass.gWS_CheckPsw(pPsw, auxIP) Then
                auxClass.gSystem_Init()
                If auxClass.DebugLogOn Then
                    auxClass.gSys_DebugLogAdd("Execute tasks: conn=" & auxClass.Conn.LastErrorDescription)
                End If
                auxReturn = auxClass.gWS_ArriveMessage(pData)
            End If
            auxClass.gSystem_End()
            If auxReturn = "" Then
                auxReturn = " "
            End If
            context.Response.BinaryWrite(System.Text.Encoding.UTF8.GetBytes(auxReturn))
            context.Response.Flush()
        End If
        
    End Sub
 
    Public ReadOnly Property IsReusable() As Boolean Implements IHttpHandler.IsReusable
        Get
            Return False
        End Get
    End Property

End Class