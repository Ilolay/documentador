<%@ WebHandler Language="VB" Class="hrcTextEditor" %>

Imports System
Imports System.Web

Public Class hrcTextEditor : Implements IHttpHandler
    
    Public Sub ProcessRequest(ByVal context As HttpContext) Implements IHttpHandler.ProcessRequest
        If context.Session Is Nothing Then
            Exit Sub
        End If
        Dim auxView As String = context.Request.Form("action").ToLower.Trim
        If auxView = "" Then
            Exit Sub
        End If
        If context.Session("conn") Is Nothing _
            Or context.Session("security") Is Nothing Then
            Exit Sub
        End If
        
        Dim auxConn As clsHrcConnClient = context.Session("conn")
        Dim auxSec As clsHrcSecurityClient = context.Session("security")
           
        Select Case auxView
            Case "hrctexteditor_savetmpcontent"
                Dim auxText As String = context.Session("content")
                auxText = context.Server.HtmlDecode(auxText)
                Dim auxTmpID As String = context.Request.Form("tmpid")
                Dim auxClient As New imClientConnection
                auxClient.gTextTmp_Upload(auxText, auxTmpID)
        End Select
    End Sub
 
    Public ReadOnly Property IsReusable() As Boolean Implements IHttpHandler.IsReusable
        Get
            Return False
        End Get
    End Property

End Class