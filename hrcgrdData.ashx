<%@ WebHandler Language="VB" Class="hrcgrdData" %>
'Version 8 2/6/2013
'llamadas de sesion
Imports System
Imports System.Web
Imports System.Data
Imports hrcgrdData 
'Imports System.Data.SqlClient
Imports Intelimedia.Hercules.Storage
Imports Intelimedia.imComponentes
Imports Intelimedia.inTasks

Public Class hrcgrdData : Implements IHttpHandler
    Implements IRequiresSessionState
    Public Sub ProcessRequest(ByVal context As HttpContext) Implements IHttpHandler.ProcessRequest
        Dim auxCache As HttpCachePolicy = context.Response.Cache
        auxCache.SetCacheability(HttpCacheability.NoCache)
        If context.Session Is Nothing Then
            Exit Sub
        End If
        Dim auxView As String = ""
        If context.Session("conn") Is Nothing Then
            ' Exit Sub
        End If
        System.Diagnostics.Debug.Print(context.Request.Form("action"))
        
        context.Server.ScriptTimeout = 60 * 60
        If context.Request.Form("action") IsNot Nothing Then
            auxView = context.Request.Form("action").ToLower
        ElseIf context.Request.QueryString("action") IsNot Nothing Then
            auxView = context.Request.QueryString("action")
        End If
        
       
           
        Dim auxReturn As String = ""
        Select Case auxView
            Case "js_general"
                Dim auxJSContent As String = ""
                auxJSContent = context.Session("JS_GENERAL")
                context.Response.Clear()
                context.Response.ContentType = "application/javascript"
                context.Response.Write(auxJSContent)
                context.Response.Cache.SetCacheability(HttpCacheability.Private)
                context.Response.Cache.SetMaxAge(New TimeSpan(1, 0, 0))
            Case "taskstate_get"
                Dim auxConn As clsHrcConnClient = context.Session("conn")
                Dim auxQueueID As String = context.Request.Form("HRC_EXECUTIONQUEUEID")
                If auxQueueID = Nothing Then
                    auxReturn = auxConn.gField_GetJSONString("result", "0", True)
                Else
                    
                    If hrcProcessQueue.gProcess_TaskIsRunning(auxQueueID) Then
                        auxReturn = auxConn.gField_GetJSONString("result", "1", True)
                    Else
                        auxReturn = auxConn.gField_GetJSONString("result", "0", True)
                    End If
                End If
            Case "service_command", "grddata_expandnode", "grddata_collapsenode", "service_onchange", "autosuggest_additem", "autosuggest_delitem", "autosuggest_clearall"
                Dim auxControlCacheID As String = ""
                auxControlCacheID = context.Request.Form("tmpid")
                Dim auxControl As Object ' clshrcObjectExplorer
                Dim auxClientCon As New imClientConnection
                auxControl = auxClientCon.gObjectTmp_Download(auxControlCacheID)
                If auxControl Is Nothing Then
                    Dim auxTmpContextID As String = context.Request.Form("TMPIDC")
                    Dim auxHrcContext As clsHrcJSContext = context.Session("hrcContext")
                    auxControl = auxHrcContext.BagValues.gValue_Get("CACHETMP_" & auxTmpContextID)
                End If
                If auxControl IsNot Nothing Then
                    auxReturn = auxControl.gService_Command
                End If
              
            Case "grddata_getdata", "grddata_first", "grddata_next", "grddata_prev", "grddata_last", "grddata_nodeclick" _
                , "grddata_panelitem_get", "grddata_panelitem_ins", "grddata_panelitem_mod", "grddata_panelitem_del", "grddata_command" _
                , "grddata_panelitem_ins_get", "grddata_panelitem_mod_get", "grddata_panelitem_del_get"
                Dim auxGridCacheID As String = ""
                auxGridCacheID = context.Request.Form("grddataid").ToLower
                Dim auxPage As String = ""
                If context.Request.Form("page") IsNot Nothing Then
                    auxPage = context.Request.Form("page").ToLower
                End If
            
                Dim auxRows As String = ""
                If context.Request.Form("rows") IsNot Nothing Then
                    auxRows = context.Request.Form("rows").ToLower
                End If
            
                Dim auxNode As Integer = -1
                'Dim auxLevel As Integer = -1
                If context.Request.Form("nodeid") IsNot Nothing Then
                    auxNode = context.Request.Form("nodeid")
                    'auxLevel = Val(context.Request.Form("n_level").ToString)
                End If
                Dim auxFilters As Boolean = False
                If context.Request.Form("hrcfilters") IsNot Nothing Then
                    If context.Request.Form("hrcfilters") = "1" Then
                        auxFilters = True
                         
                    End If
                End If
            
            
                Try
                    Dim auxClientCon As New imClientConnection
                    Dim prvGridData As Object ' clshrcGrdData
                    prvGridData = auxClientCon.gObjectTmp_Download(auxGridCacheID)
                    
                    Dim auxValues As New clshrcBagValues
                    'If prvGridData.Controlid = "GRDDOC" Then
                    '    If context.Session("conta") Is Nothing Then
                    '        context.Session("conta") = "1"
                    '    Else
                    '        context.Session("conta") = Nothing
                    '        Exit Sub
                    '    End If
                    'End If
                    If auxFilters Then
                        Dim auxVarName As String
                        For auxI As Integer = 0 To context.Request.Form.Count - 1
                            auxVarName = context.Request.Form.GetKey(auxI).ToUpper
                            Select Case auxVarName
                                Case "GRDDATAID"
                                Case Else
                                    auxValues.gValue_Add(auxVarName, _
                                                  context.Request.Form(auxI))
                            End Select
                        Next
                        prvGridData.gDatasource_SetCursorWithFilter(auxValues)
                    End If
               
                    Select Case auxView
                        Case "grddata_panelitem_get"
                            auxReturn = prvGridData.DefaultPanel.gCommandView_GetJSONToClient_First(auxValues)
                            'Dim auxPanelIndex As Integer = Val(auxValues.gValue_Get("hrcPanel"))
                            'auxReturn = prvGridData.gPanels_Get(auxPanelIndex).gCommandView_GetJSONToClient_First(auxValues)
                        Case "grddata_panelitem_del"
                            auxReturn = prvGridData.DefaultPanel.gCommandDelete_GetJSONToClient(auxValues)
                        Case "grddata_panelitem_mod"
                            auxReturn = prvGridData.DefaultPanel.gCommandUpdate_GetJSONToClient(auxValues)
                        Case "grddata_panelitem_ins"
                            auxReturn = prvGridData.DefaultPanel.gCommandInsert_GetJSONToClient(auxValues)
                        Case "grddata_command", "grddata_panelitem_ins_get", "grddata_panelitem_mod_get", "grddata_panelitem_del_get"
                            
                            If prvGridData.defaultpanel Is Nothing Then
                                auxReturn = prvGridData.gservice_command
                            Else
                                auxReturn = prvGridData.DefaultPanel.gCommand_GetJSONToClient(auxValues)
                            End If
                        Case "grddata_all"
                            auxReturn = prvGridData.gDatabind_GetJSONToClient_All
                        Case "grddata_first"
                            auxReturn = prvGridData.gDatabind_GetJSONToClient_First
                        Case "grddata_next"
                            auxReturn = prvGridData.gDatabind_GetJSONToClient_Next
                        Case "grddata_prev"
                            auxReturn = prvGridData.gDatabind_GetJSONToClient_Previous
                        Case "grddata_last"
                            auxReturn = prvGridData.gDatabind_GetJSONToClient_Last
                            'Case "grddata_expandnode"
                            '    Dim auxGridDatacod As String = ""
                            '    auxGridDatacod = context.Request.Form("grddatacod")
                            '    prvGridData.gTreeGrid_ExpandNode(auxGridDatacod)
                            '    Dim auxConn As clsHrcConnClient = context.Session("conn")
                            '    auxReturn = auxConn.gField_GetJSONString("result", "1", True)
                            'Case "grddata_collapsenode"
                            '    Dim auxGridDatacod As String = ""
                            '    auxGridDatacod = context.Request.Form("grddatacod")
                            '    prvGridData.gTreeGrid_CollapseNode(auxGridDatacod)
                            '    Dim auxConn As clsHrcConnClient = context.Session("conn")
                            '    auxReturn = auxConn.gField_GetJSONString("result", "1", True)
                        Case "grddata_nodeclick"
                            Dim auxGridDatacod As String = ""
                            auxGridDatacod = context.Request.Form("grddatacod")
                            prvGridData.gTreeGrid_ExpandNode(auxGridDatacod)
                            Dim auxConn As clsHrcConnClient = context.Session("conn")
                            auxReturn = auxConn.gField_GetJSONString("result", "1", True)
                  
                        Case Else
                            auxReturn = prvGridData.gDataBind_GetJSONtoClient(auxNode)
                    End Select

                Catch ex As Exception

                End Try
              
                'Case "autosuggest_additem", "autosuggest_delitem"
                '    Dim auxObjectExplorerCacheID As String = ""
                '    auxObjectExplorerCacheID = context.Request.Form("tmpid")
                '    Dim prvObjectExplorer As Object ' clshrcObjectExplorer
                '    Dim auxClientCon As New imClientConnection
                '    prvObjectExplorer = auxClientCon.gObjectTmp_Download(auxObjectExplorerCacheID)
                '    If prvObjectExplorer IsNot Nothing Then
                '        auxReturn= prvObjectExplorer.gservice_command                                      
                '    End If
            Case "autosuggest", "autosuggest2"
                'autosuggest
                
                '2.Calcula resultado
               
                Dim auxClientCon As New imClientConnection
                              
                Dim auxParam As String = context.Request.QueryString("q")
                If auxView = "autosuggest2" Then
                    Dim auxControl As Object ' clshrcObjectExplorer
                    Dim auxControlTmpID As String = ""
                    auxControlTmpID = context.Request.QueryString("tmpid")
                    If auxControlTmpID <> "" Then
                        auxControl = auxClientCon.gObjectTmp_Download(auxControlTmpID)
                    Else
                        auxControlTmpID = context.Request.QueryString("tmpidg")
                        auxControl = auxClientCon.gObjectTmp_DownloadFromGlobal(auxControlTmpID)
                    End If
                    If auxControl IsNot Nothing Then
                        auxControl.gservice_command()
                        'prvObjectExplorer.Cursor.gSelectCommand_ApplyParameters(auxValues)
                        auxReturn = auxControl.gCursor_GetJsonList
                    End If
                    
                Else
                    Dim prvCursor As clsHrcCursor
                    Dim auxCursorCacheID As String = ""
                    auxCursorCacheID = context.Request.QueryString("tmpid")
                    prvCursor = auxClientCon.gObjectTmp_Download(auxCursorCacheID)
                    If prvCursor IsNot Nothing Then
                        Dim auxValues As New clshrcBagValues
                        Dim auxHTML As New Intelimedia.Hercules.Language.clsHrcCodeHTML
                        auxValues = auxHTML.gBagValues_GetFromQueryString(context.Request.QueryString.ToString)
                        prvCursor.gSelectCommand_ApplyParameters(auxValues)
                        For Each auxRow As DataRow In prvCursor.gCursor_GetAll.Rows
                            If auxReturn <> "" Then
                                auxReturn &= ","
                            End If
                            auxReturn &= "{""HRCCOD"":""" & auxRow(0) & """,""HRCDSC"":""" & auxRow(1) & """,""HRCTYPE"":""" & auxRow(2) & """,""HRCLINE1"":""" & auxRow(3) & """}"
                        Next
                        auxReturn = "[" _
                                    & auxReturn _
                                   & "]"
                    End If
                End If
            Case Else
                Dim auxControlTmpID As String = ""
                auxControlTmpID = context.Request.Form("tmpid")
                Dim auxControl As Object ' clsHrcJSControlBasic 
                Dim auxClientCon As New imClientConnection
                If auxControlTmpID <> "" Then
                    auxControl = auxClientCon.gObjectTmp_Download(auxControlTmpID)
                Else
                    auxControlTmpID = context.Request.Form("tmpidg")
                    auxControl = auxClientCon.gObjectTmp_DownloadFromGlobal(auxControlTmpID)
                    If auxControl Is Nothing Then
                        Dim auxTmpContextID As String = context.Request.Form("TMPIDC")
                        Dim auxHrcContext As clsHrcJSContext = context.Session("hrcContext")
                        auxControl = auxHrcContext.BagValues.gValue_Get("CACHETMP_" & auxTmpContextID)
                    End If
                End If
                If auxControl IsNot Nothing Then
                    auxReturn = auxControl.gservice_command
                End If
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


