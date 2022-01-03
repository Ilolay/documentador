<%@ WebHandler Language="VB" Class="cfrmdocumentos_ashx" %>

Imports System
Imports System.Web
Imports System.Data
Imports clsCusimDOC
 
Public Class cfrmdocumentos_ashx : Implements IHttpHandler
    Implements IRequiresSessionState
    Public Sub ProcessRequest(ByVal context As HttpContext) Implements IHttpHandler.ProcessRequest
        If context.Session Is Nothing Then
            Exit Sub
        End If
        If context.Session("conn") Is Nothing _
        Or context.Session("empcod") Is Nothing Then
            Exit Sub
        End If
        
        Dim auxConn As clsHrcConnClient = context.Session("conn")
        auxConn.gComponent_CreateInstance()
        Dim auxEmpCod As Integer = -1
        auxEmpCod = auxConn.gField_GetInt(context.Session("empcod"), -1)
        Dim auxView As String = ""
        If context.Request.Form("action") IsNot Nothing Then
            auxView = context.Request.Form("action").ToLower
        End If
        Dim auxReturn As String = ""
        Select Case auxView
            Case "grddata_getdata","grddata_first"
                Dim auxGridCacheID As String = ""
                auxGridCacheID = context.Request.Form("grddataid").ToLower
                Dim auxNode As Integer = -1
                Dim auxLevel As Integer = -1
                If context.Request.Form("nodeid") IsNot Nothing Then
                    auxNode = context.Request.Form("nodeid")
                    auxLevel = Val(context.Request.Form("n_level").ToString)
                End If
                'Dim auxClass As New clsCusimDOC
                Dim prvGridData As Object
                Try
                    Dim auxClientCon As New imClientConnection
                    prvGridData = auxClientCon.gObjectTmp_Download(auxGridCacheID)
                    'AddHandler prvGridData.BlockDataBound, AddressOf gGrdData_BlockDataBound
                    auxReturn = prvGridData.gDataBind_GetJSONtoClient(auxNode, auxLevel)
                    'auxClass.gTRACE_add(-1, 10, "Return:" & auxReturn)
                Catch ex As Exception
                    'auxClass.gTRACE_add(-1, 1, "Exception in cfrmdocumentos.ashx." & ex.Message & "." & ex.StackTrace)
                End Try

        End Select
        
        context.Response.ContentType = "application/json"
        context.Response.Write(auxReturn)
    End Sub
 
    Public ReadOnly Property IsReusable() As Boolean Implements IHttpHandler.IsReusable
        Get
            Return False
        End Get
    End Property
    
End Class