Imports System.Data
Imports System.Data.SqlClient
Imports Intelimedia.imComponentes
Imports clsCusimDOC
Imports System.Web.Script.Services
Imports System.Web.Services
Imports System.Xml

Imports Intelimedia.inTasks

Imports System.IO
Imports Intelimedia.Hercules.Language
Imports Intelimedia.Hercules.Storage
Imports Intelimedia.Hercules.Language.clsHrcCodeHTML

Imports System
Imports System.Collections
Imports System.Collections.Generic
Imports System.Web
Imports Microsoft.VisualBasic

Imports System.Text

Imports Intelimedia.Hercules.Design
Imports Intelimedia.Hercules.Security

'
'Captcha
Imports System.Drawing
Imports System.Drawing.Drawing2D
Imports System.Drawing.Imaging
Public Class cfrmDocumentos
    Inherits imWebPage
    Private Enum enumView As Short
        coObligado = 1
        coVigente_PorTipoProceso = 2
        coVigente_PorUnidad = 3
        coVigente_PorTipoDoc = 4
        coVigente_PorTipoProcesoyProceso = 8
        coVigente_PorSistema = 10

        coBiblioteca_SinAnidar = 0
        coBiblioteca_PorTipoproceso = 5
        coBiblioteca_PorUnidad = 6
        coBiblioteca_PorTipoDoc = 7
        coBiblioteca_PorTipoProcesoyProceso = 9
        coBiblioteca_PorSistema = 11
        coMetro_Inicial = 12

        coBiblioteca_NuevosDoc = 13
        coBiblioteca_NuevasVersiones = 14
        coBiblioteca_Edicion = 15
        coBiblioteca_Revision = 16
        coBiblioteca_Aprobacion = 17
        coBiblioteca_Publicacion = 18
        coBiblioteca_Cancelacion = 19
        coBiblioteca_Lectura = 20

        coMetro_PorProceso = 21


        coBiblioteca_Creacion = 22
        coBiblioteca_Eliminacion = 23
        coMetro_PorTipoProceso = 24
    End Enum
    Private Function gView_IsVigente(ByVal pView As enumView) As Boolean
        Select pView
            Case enumView.coVigente_PorSistema, enumView.coVigente_PorTipoDoc, enumView.coVigente_PorTipoProceso, enumView.coVigente_PorTipoProcesoyProceso, enumView.coVigente_PorUnidad _
                      , enumView.coMetro_PorProceso, enumView.coMetro_PorTipoProceso, enumView.coMetro_Inicial
                Return True
            Case Else
                Return False
        End Select
    End Function
    Private Sub gDocumentsSearchPanel_CommandHandler(ByVal pControl As clsHrcJSControlBasic, ByVal pValues As clshrcBagValues)
        Dim auxAction As String = pValues.gValue_Get("ACTION")
        Dim auxCommandName As String = pValues.gValue_Get("COMMANDNAME")
        If pControl.ControlID = "GRDDOC" And auxCommandName = "GRDDATA_DATABIND" Then
            Dim auxPanel As clsHrcJSPanel = pControl.Parentcontrol
            Dim auxView As enumView = auxPanel.BagValues.gValue_Get("view")
            Dim auxInboxDetailedEnabled As Boolean = auxPanel.BagValues.gValue_Get("InboxDetailedEnabled")
            Dim auxEmpCod As Integer = auxPanel.BagValues.gValue_Get("empcod")
            Dim auxProCod As Integer = auxPanel.BagValues.gValue_Get("procod", -1)
            Dim auxApacod As Integer = auxPanel.BagValues.gValue_Get("apacod", -1)
            Dim auxIsAdmin As Boolean = auxPanel.BagValues.gValue_Get("isadmin")
            Dim auxClass As New clsCusimDOC
            auxClass.Conn.gConn_Open()
            'Select Case auxView
            '    Case enumView.coMetro_PorProceso, enumView.coMetro_PorTipoProceso
            'Quitarle el panel para que no realice búsquedas por el mismo
            '        auxPanel = Nothing
            'End Select
            Dim auxDT As DataTable = gData_GetDT(auxView, auxClass, auxPanel, auxInboxDetailedEnabled, auxEmpCod, auxIsAdmin, auxProCod, auxapaCod)
            auxClass.Conn.gConn_Close()
            Dim auxGrdData As clshrcGrdData = CType(pControl, clshrcGrdData)
            auxDT = auxGrdData.gDataTable_Prepare(auxDT)
            auxGrdData.gDataSource_Set(auxDT)
            If auxDT.Rows.Count < 15 Then
                auxGrdData.ExpandAllAfterLoad = True
            Else
                auxGrdData.ExpandAllAfterLoad = False
            End If
            pValues.gValue_Add("HRC_RESULT", auxDT)
        ElseIf auxAction = "EXPORTXLS" Then
            Dim auxPanel As clsHrcJSPanel = pControl
            Dim auxView As enumView = auxPanel.BagValues.gValue_Get("view")
            Dim auxInboxDetailedEnabled As Boolean = auxPanel.BagValues.gValue_Get("InboxDetailedEnabled")
            Dim auxEmpCod As Integer = auxPanel.BagValues.gValue_Get("empcod")
            Dim auxProCod As Integer = auxPanel.BagValues.gValue_Get("procod", -1)
            Dim auxApacod As Integer = auxPanel.BagValues.gValue_Get("apacod", -1)
            Dim auxIsAdmin As Boolean = auxPanel.BagValues.gValue_Get("isadmin")
            Dim auxClass As New clsCusimDOC
            auxClass.Conn.gConn_Open()
            Dim auxDT As DataTable = gData_GetDT(auxView, auxClass, auxPanel, auxInboxDetailedEnabled, auxEmpCod, auxIsAdmin, auxProCod, auxApacod)
            auxClass.Conn.gConn_Close()

            Dim auxFileTmpID As String = ""
            Dim auxTmpGuid As String = ""
            Dim auxScript As String = ""
            Dim auxBagValues As New clshrcBagValues
            auxBagValues.gValue_Add("_view_", auxView)
            auxBagValues.gValue_Add("XLS_DT", auxDT)
            auxTmpGuid = auxClass.Conn.gField_GetUniqueID()
            auxBagValues.gValue_Add("xls_tmpid", auxTmpGuid)
            auxFileTmpID = "reporte_" & auxClass.Conn.gDate_GetNow.ToString & ".xls"
            auxBagValues.gValue_Add("xls_filename", auxFileTmpID)
            gDownloadXLS_CommandHandler(Nothing, auxBagValues)
            pValues.gValue_Add("HRC_RESULTS", auxClass.Conn.gField_GetJSONString("xls_tmpid", auxTmpGuid, True))
            ScriptManager.RegisterStartupScript(Me, Me.GetType, "hrcdownload", _
                                                  auxScript, True)
        ElseIf pControl.ControlID = "CMDSEARCH" Then
            Dim auxPanel As clsHrcJSPanel = pControl.Parentcontrol
            Dim auxControl As clsHrcJSControlBasic
            auxControl = auxPanel.gControl_Get("txtdsc")
            Dim auxClass As New clsCusimDOC
            auxClass.Conn.gConn_Open()
            If auxControl IsNot Nothing Then
                auxClass.gLoginPreference_SetValue(enumPrefType.coDocumentossearch, "dsc", auxControl.gValue_Get)
            End If

            auxControl = auxPanel.gControl_Get("txtnro")
            If auxControl IsNot Nothing Then
                auxClass.gLoginPreference_SetValue(enumPrefType.coDocumentossearch, "nro", auxControl.gValue_Get)
            End If

            auxControl = auxPanel.gControl_Get("cmbapa")
            If auxControl IsNot Nothing Then
                auxClass.gLoginPreference_SetValue(enumPrefType.coDocumentossearch, "apacod", auxControl.gValue_Get)
            End If

            auxControl = auxPanel.gControl_Get("cmbcla")
            If auxControl IsNot Nothing Then
                auxClass.gLoginPreference_SetValue(enumPrefType.coDocumentossearch, "clacod", auxControl.gValue_Get)
            End If

            auxControl = auxPanel.gControl_Get("cmbdoctipcod")
            If auxControl IsNot Nothing Then
                auxClass.gLoginPreference_SetValue(enumPrefType.coDocumentossearch, "doctipcod", auxControl.gValue_Get)
            End If

            auxControl = auxPanel.gControl_Get("cmbwfwstp")
            If auxControl IsNot Nothing Then
                auxClass.gLoginPreference_SetValue(enumPrefType.coDocumentossearch, "wfwstp", auxControl.gValue_Get)
            End If

            auxControl = auxPanel.gControl_Get("chkmisacciones")
            If auxControl IsNot Nothing Then
                auxClass.gLoginPreference_SetValue(enumPrefType.coDocumentossearch, "misacciones", auxControl.gValue_Get)
            End If

            Dim auxSavedValues As clshrcBagValues = auxPanel.gFieldData_GetValues(True, True, True)

            auxClass.gLoginPreference_SetValue(enumPrefType.coDocumentossearch, "filters", auxSavedValues.Config_GetStream)


            auxClass.gLoginPreference_Save(enumPrefType.coDocumentossearch)
            auxClass.Conn.gConn_Close()
        End If
    End Sub

    ''/////////////////////////////////////////////
    Private Function gFormDocuments_GetMetroView(ByVal pView As enumView, _
                                             ByVal pEmpCod As Integer, _
                                             ByVal pIsAdmin As Boolean, _
                                             ByVal pProCod As Integer, _
                                             ByVal pApaCod As Integer) As clshrcBagValues
        Dim auxClass As New clsCusimDOC
        auxClass.Conn.gConn_Open()
        Dim auxCant As Integer
        Dim auxContent As String = ""
        Dim auxScript As String = ""
        auxContent &= "<div class=""metro"">"
        auxContent &= "<div class=""area"" style=""margin:10px;"">"

        Dim auxFGColor As String = "fg-darkCobalt hrcmetro-tile-title"
        'Dim auxTileBGColor As String = "ui-corner-all form-control-textbox"
        Dim auxTileBGColor As String = " ui-corner-all ui-widget-header"
        Dim auxPanelStyle As String = "style=""background-color:#5c9ccc;"""
        'Dim auxPanelStyle As String = "style=""background:white !important"""
        Dim auxBadgeStyle As String = "style=""background-color:gray"";"
        If pView = enumView.coMetro_Inicial Then
            'tile-group-column1
            auxContent &= "<div class=""tile-group quadro no-margin"" >"
            auxContent &= "<span class=""tile-group-title " & auxFGColor & """>" _
                & "<img src=""imagenes/biblioteca_documentos.png"" width=20px class=""hrcthemeimage metro-tile-group-title-icon"" >Mis acciones pendientes</span>"

            auxCant = auxClass.Conn.gConn_QueryValueInt("SELECT COUNT(*) " _
                                 & " FROM DOC_DOCSGN" _
                                 & " LEFT JOIN DOC_DOC ON DOC_DOCSGN.doccod=DOC_DOC.cod" _
                                 & " WHERE " _
                                 & " (DOC_DOC.baja  {#ISNULL#} OR DOC_DOC.baja ={#FALSE#}) " _
                                 & " AND empcod= " & pEmpCod _
                                   & " AND doclogcod IN (" _
                                   & "SELECT cod FROM DOC_DOCLOG WHERE wfwstepnext=" & enumWorkflowStep.coWFWSTPDOC_DOCDocumentovigente _
                                   & ")", 0)
            auxContent &= "<div class=""tile double " & auxTileBGColor & " icon"" " & auxPanelStyle & " >"
            auxContent &= "<a href=""cfrmdocumentos.aspx?myact=1&_view_=" & CInt(enumView.coBiblioteca_Lectura) & """ >"
            auxContent &= " <div class=""tile-content icon"">"
            auxContent &= "     <img style=""padding-bottom:15px;padding-left:5px;"" src=""imagenes/icondocrol00000001.png"">"
            auxContent &= " </div>"
            auxContent &= " <div class=""brand  bg-black"">"
            auxContent &= "		<span class=""label"">Lectura pendiente</span>"
            auxContent &= "     <span class=""badge"" " & auxBadgeStyle & ">" & auxCant & "</span>"
            auxContent &= " </div>"
            auxContent &= " </a>"
            auxContent &= "</div>"
            Dim auxWhere As String
            Dim auxWhereGeneral As String = ""
            If pIsAdmin Then
            Else
                auxWhereGeneral &= " AND qsidcod IN(" & auxClass.Sec.gSID_GetQueryAccessFromAcctype(-1, enumAccessType.coSYSGlobalCambiarestado _
                                                                         & "," & enumAccessType.coSYSGlobalModificar _
                                                                         & "," & enumAccessType.coSYSGlobalCambiarpermisos _
                                                                         & "," & enumAccessType.coSYSConfirmarlectura _
                                                                         & "," & enumAccessType.coSYSCreador _
                                                                         & "," & enumAccessType.coSYSImprimircopiascontroladas _
                                                                         & "," & enumAccessType.coDOCDOCVIGDocumentosvigentesVer _
                                                                         & "," & enumAccessType.coSYSImprimircopiasnocontroladas) & ")"

            End If



            'auxWhere = " AND (DOC_DOC.wfwstatus IN (" & enumWorkflowStep.coWFWSTPDOC_DOCEdicion _
            '             & "," & enumWorkflowStep.coWFWSTPDOC_DOCEdicionOK _
            '             & "," & enumWorkflowStep.coWFWSTPDOC_DOCnuevaversion _
            '             & ")" _
            '             & " OR " _
            '             & " (DOC_DOC.wfwmode=" & enumWfwMode.coUserCreate _
            '                 & " AND DOC_DOC.wfwstatus =" & enumWorkflowStep.coWFWSTPDOC_DOCCreacion & ")" _
            '             & ")"
            'auxWhere &= " AND DOC_DOC.cod IN " _
            '                  & "(SELECT DOC_DOCSGN.doccod " _
            '                  & " FROM DOC_DOCSGN " _
            '                  & " LEFT JOIN DOC_DOCLOG ON DOC_DOCSGN.doclogcod = DOC_DOCLOG.cod " _
            '                  & " WHERE DOC_DOCLOG.wfwstepnext IN (" & enumWorkflowStep.coWFWSTPDOC_DOCEdicion _
            '             & "," & enumWorkflowStep.coWFWSTPDOC_DOCEdicionOK _
            '             & "," & enumWorkflowStep.coWFWSTPDOC_DOCnuevaversion _
            '             & ")" _
            '                  & " AND DOC_DOCSGN.doccod=DOC_DOC.cod " _
            '                  & " AND DOC_DOCSGN.empcod=" & pEmpCod & ") "
            'auxCant = auxClass.Conn.gConn_QueryValueInt("SELECT COUNT(*) FROM DOC_DOC" _
            '                               & " WHERE " _
            '                               & " cod > 0 " _
            '                               & auxWhere, 0)
            auxWhere = " AND DOC_DOCSGN.doccod IN (SELECT cod FROM DOC_DOC WHERE DOC_DOC.wfwstatus IN (" & _
                enumWorkflowStep.coWFWSTPDOC_DOCEdicion _
                & "," & enumWorkflowStep.coWFWSTPDOC_DOCEdicionOK _
                & "," & enumWorkflowStep.coWFWSTPDOC_DOCnuevaversion _
                & "," & enumWorkflowStep.coWFWSTPDOC_DOCCreacion _
                & "))"
            auxCant = auxClass.Conn.gConn_QueryValueInt("SELECT COUNT(*) " _
                     & " FROM DOC_DOCSGN " _
                     & " LEFT JOIN DOC_DOC ON DOC_DOCSGN.doccod=DOC_DOC.cod" _
                     & " WHERE " _
                     & " (DOC_DOC.baja  {#ISNULL#} OR DOC_DOC.baja ={#FALSE#}) " _
                     & " AND empcod= " & pEmpCod _
                     & auxWhere _
                     & " AND doclogcod IN (" _
                     & "SELECT cod FROM DOC_DOCLOG WHERE wfwstepnext IN (" & enumWorkflowStep.coWFWSTPDOC_DOCEdicion _
                         & "," & enumWorkflowStep.coWFWSTPDOC_DOCEdicionOK _
                         & "," & enumWorkflowStep.coWFWSTPDOC_DOCnuevaversion _
                         & "," & enumWorkflowStep.coWFWSTPDOC_DOCCreacion _
                         & ")" _
                     & ")", 0)

            auxContent &= "<div class=""tile double " & auxTileBGColor & " icon"" " & auxPanelStyle & ">"
            auxContent &= "<a href=""cfrmdocumentos.aspx?myact=1&_view_=" & CInt(enumView.coBiblioteca_Edicion) & """ >"
            auxContent &= " <div class=""tile-content icon"">"
            auxContent &= "     <img style=""padding-bottom:15px;padding-left:5px;"" src=""imagenes/icondocrol00000002.png"">"
            auxContent &= " </div>"
            auxContent &= " <div class=""brand  bg-black"">"
            auxContent &= "		<span class=""label"">Creación y edición</span>"
            auxContent &= "     <span class=""badge"" " & auxBadgeStyle & """>" & auxCant & "</span>"
            auxContent &= " </div>"
            auxContent &= " </a>"
            auxContent &= "</div>"

            'auxCant = auxClass.Conn.gConn_QueryValueInt("SELECT COUNT(*) FROM DOC_DOC" _
            '                   & " WHERE " _
            '                   & " (wfwstatus= " & enumWorkflowStep.coWFWSTPDOC_DOCCreacion & " OR wfwstatus {#ISNULL#})" _
            '                   & auxWhere, 0)
            'auxContent &= "<div class=""tile double " & auxTileBGColor & " icon"" " & auxPanelStyle & ">"
            'auxContent &= "<a href=""cfrmdocumentos.aspx?myact=1&_view_=" & CInt(enumView.coBiblioteca_Creacion) & """ >"
            'auxContent &= " <div class=""tile-content icon"">"
            'auxContent &= "     <img style=""padding-bottom:15px;padding-left:5px;"" src=""imagenes/icondocrol00000009.png"">"
            'auxContent &= " </div>"
            'auxContent &= " <div class=""brand  bg-black"">"
            'auxContent &= "		<span class=""label"">Borradores</span>"
            'auxContent &= "     <span class=""badge"" " & auxBadgeStyle & """>" & auxCant & "</span>"
            'auxContent &= " </div>"
            'auxContent &= " </a>"
            'auxContent &= "</div>"
            auxWhere = " AND DOC_DOCSGN.doccod IN (SELECT cod FROM DOC_DOC WHERE DOC_DOC.wfwstatus IN (" & enumWorkflowStep.coWFWSTPDOC_DOCSolicitudnuevodocumento & "))"
            auxCant = auxClass.Conn.gConn_QueryValueInt("SELECT COUNT(*) " _
              & " FROM DOC_DOCSGN " _
              & " LEFT JOIN DOC_DOC ON DOC_DOCSGN.doccod=DOC_DOC.cod" _
              & " WHERE " _
              & " (DOC_DOC.baja  {#ISNULL#} OR DOC_DOC.baja ={#FALSE#}) " _
              & " AND empcod= " & pEmpCod _
                          & auxWhere _
                          & " AND doclogcod IN (" _
                          & "SELECT cod FROM DOC_DOCLOG WHERE wfwstepnext=" & enumWorkflowStep.coWFWSTPDOC_DOCSolicitudnuevodocumento _
                          & ")", 0)
            auxContent &= "<div class=""tile double " & auxTileBGColor & " icon"" " & auxPanelStyle & ">"
            auxContent &= "<a href=""cfrmdocumentos.aspx?myact=1&_view_=" & CInt(enumView.coBiblioteca_NuevosDoc) & """ >"
            auxContent &= " <div class=""tile-content icon"">"
            auxContent &= "     <img style=""padding-bottom:15px;padding-left:5px;"" src=""imagenes/icondocrol00000009.png"">"
            auxContent &= " </div>"
            auxContent &= " <div class=""brand  bg-black"">"
            auxContent &= "		<span class=""label"">Solicitud nuevos documentos</span>"
            auxContent &= "     <span class=""badge"" " & auxBadgeStyle & """>" & auxCant & "</span>"
            auxContent &= " </div>"
            auxContent &= " </a>"
            auxContent &= "</div>"



            auxWhere = " AND DOC_DOCSGN.doccod IN (SELECT cod FROM DOC_DOC WHERE DOC_DOC.wfwstatus IN (" & enumWorkflowStep.coWFWSTPDOC_DOCSolicitudnuevaversion & "," & enumWorkflowStep.coWFWSTPDOC_DOCSolicitudautomaticanuevaversion & "))"
            auxCant = auxClass.Conn.gConn_QueryValueInt("SELECT COUNT(*) " _
            & " FROM DOC_DOCSGN " _
            & " LEFT JOIN DOC_DOC ON DOC_DOCSGN.doccod=DOC_DOC.cod" _
            & " WHERE " _
            & " (DOC_DOC.baja  {#ISNULL#} OR DOC_DOC.baja ={#FALSE#}) " _
            & " AND empcod= " & pEmpCod _
                      & auxWhere _
                      & " AND doclogcod IN (" _
                      & "SELECT cod FROM DOC_DOCLOG WHERE wfwstepnext  IN (" & enumWorkflowStep.coWFWSTPDOC_DOCSolicitudnuevaversion & "," & enumWorkflowStep.coWFWSTPDOC_DOCSolicitudautomaticanuevaversion & ")" _
                      & ")", 0)
            auxContent &= "<div class=""tile " & auxTileBGColor & " icon"" " & auxPanelStyle & ">"
            auxContent &= "<a href=""cfrmdocumentos.aspx?myact=1&_view_=" & CInt(enumView.coBiblioteca_NuevasVersiones) & """ >"
            auxContent &= " <div class=""tile-content icon"">"
            auxContent &= "     <img style=""padding-bottom:15px;padding-left:5px;"" src=""imagenes/icondocrol00000009.png"">"
            auxContent &= " </div>"
            auxContent &= " <div class=""brand  bg-black"">"
            auxContent &= "		<span class=""label"">Solicitud nuevas versiones</span>"
            auxContent &= "     <span class=""badge"" " & auxBadgeStyle & """>" & auxCant & "</span>"
            auxContent &= " </div>"
            auxContent &= " </a>"
            auxContent &= "</div>"

            auxWhere = " AND DOC_DOCSGN.doccod IN (SELECT cod FROM DOC_DOC WHERE DOC_DOC.wfwstatus IN (" & enumWorkflowStep.coWFWSTPDOC_DOCRevisionSSG & "))"
            auxCant = auxClass.Conn.gConn_QueryValueInt("SELECT COUNT(*) " _
            & " FROM DOC_DOCSGN " _
            & " LEFT JOIN DOC_DOC ON DOC_DOCSGN.doccod=DOC_DOC.cod" _
            & " WHERE " _
            & " (DOC_DOC.baja  {#ISNULL#} OR DOC_DOC.baja ={#FALSE#}) " _
            & " AND empcod= " & pEmpCod _
                                    & auxWhere _
                                    & " AND doclogcod IN (" _
                                    & "SELECT cod FROM DOC_DOCLOG WHERE wfwstepnext=" & enumWorkflowStep.coWFWSTPDOC_DOCRevisionSSG _
                                    & ")", 0)
            auxContent &= "<div class=""tile " & auxTileBGColor & " icon"" " & auxPanelStyle & ">"
            auxContent &= "<a href=""cfrmdocumentos.aspx?myact=1&_view_=" & CInt(enumView.coBiblioteca_Revision) & """ >"
            auxContent &= " <div class=""tile-content icon"">"
            auxContent &= "     <img style=""padding-bottom:15px;padding-left:5px;"" src=""imagenes/icondocrol00000003.png"">"
            auxContent &= " </div>"
            auxContent &= " <div class=""brand  bg-black"">"
            auxContent &= "		<span class=""label"">Revisión</span>"
            auxContent &= "     <span class=""badge"" " & auxBadgeStyle & """>" & auxCant & "</span>"
            auxContent &= " </div>"
            auxContent &= " </a>"
            auxContent &= "</div>"
            auxWhere = " AND DOC_DOCSGN.doccod IN (SELECT cod FROM DOC_DOC WHERE DOC_DOC.wfwstatus IN (" & enumWorkflowStep.coWFWSTPDOC_DOCAprobacion & "))"
            auxCant = auxClass.Conn.gConn_QueryValueInt("SELECT COUNT(*) " _
            & " FROM DOC_DOCSGN " _
            & " LEFT JOIN DOC_DOC ON DOC_DOCSGN.doccod=DOC_DOC.cod" _
            & " WHERE " _
            & " (DOC_DOC.baja  {#ISNULL#} OR DOC_DOC.baja ={#FALSE#}) " _
            & " AND empcod= " & pEmpCod _
                                & auxWhere _
                                & " AND doclogcod IN (" _
                                & "SELECT cod FROM DOC_DOCLOG WHERE wfwstepnext=" & enumWorkflowStep.coWFWSTPDOC_DOCAprobacion _
                                & ")", 0)
            auxContent &= "<div class=""tile " & auxTileBGColor & " icon"" " & auxPanelStyle & ">"
            auxContent &= "<a href=""cfrmdocumentos.aspx?myact=1&_view_=" & CInt(enumView.coBiblioteca_Aprobacion) & """ >"
            auxContent &= " <div class=""tile-content icon"">"
            auxContent &= "     <img style=""padding-bottom:15px;padding-left:5px;"" src=""imagenes/icondocrol00000004.png"">"
            auxContent &= " </div>"
            auxContent &= " <div class=""brand  bg-black"">"
            auxContent &= "		<span class=""label"">Aprobación</span>"
            auxContent &= "     <span class=""badge"" " & auxBadgeStyle & """>" & auxCant & "</span>"
            auxContent &= " </div>"
            auxContent &= " </a>"
            auxContent &= "</div>"

            auxWhere = " AND DOC_DOCSGN.doccod IN (SELECT cod FROM DOC_DOC WHERE DOC_DOC.wfwstatus IN (" & enumWorkflowStep.coWFWSTPDOC_DOCPublicacion & "))"
            auxCant = auxClass.Conn.gConn_QueryValueInt("SELECT COUNT(*) " _
             & " FROM DOC_DOCSGN " _
             & " LEFT JOIN DOC_DOC ON DOC_DOCSGN.doccod=DOC_DOC.cod" _
             & " WHERE " _
             & " (DOC_DOC.baja  {#ISNULL#} OR DOC_DOC.baja ={#FALSE#}) " _
             & " AND empcod= " & pEmpCod _
                                 & auxWhere _
                                 & " AND doclogcod IN (" _
                                 & "SELECT cod FROM DOC_DOCLOG WHERE wfwstepnext=" & enumWorkflowStep.coWFWSTPDOC_DOCPublicacion _
                                 & ")", 0)
            auxContent &= "<div class=""tile " & auxTileBGColor & " icon"" " & auxPanelStyle & ">"
            auxContent &= "<a href=""cfrmdocumentos.aspx?myact=1&_view_=" & CInt(enumView.coBiblioteca_Publicacion) & """ >"
            auxContent &= " <div class=""tile-content icon"">"
            auxContent &= "     <img style=""padding-bottom:15px;padding-left:5px;"" src=""imagenes/icondocrol00000005.png"">"
            auxContent &= " </div>"
            auxContent &= " <div class=""brand  bg-black"">"
            auxContent &= "		<span class=""label"">Publicación</span>"
            auxContent &= "     <span class=""badge"" " & auxBadgeStyle & """>" & auxCant & "</span>"
            auxContent &= " </div>"
            auxContent &= " </a>"
            auxContent &= "</div>"

            auxWhere = " AND DOC_DOCSGN.doccod IN (SELECT cod FROM DOC_DOC WHERE DOC_DOC.wfwstatus IN (" & enumWorkflowStep.coWFWSTPDOC_DOCCancelacion & "))"
            auxCant = auxClass.Conn.gConn_QueryValueInt("SELECT COUNT(*) " _
            & " FROM DOC_DOCSGN " _
            & " LEFT JOIN DOC_DOC ON DOC_DOCSGN.doccod=DOC_DOC.cod" _
            & " WHERE " _
            & " (DOC_DOC.baja  {#ISNULL#} OR DOC_DOC.baja ={#FALSE#}) " _
            & " AND empcod= " & pEmpCod _
                           & auxWhere _
                           & " AND doclogcod IN (" _
                           & "SELECT cod FROM DOC_DOCLOG WHERE wfwstepnext=" & enumWorkflowStep.coWFWSTPDOC_DOCCancelacion _
                           & ")", 0)
            auxContent &= "<div class=""tile " & auxTileBGColor & " icon"" " & auxPanelStyle & ">"
            auxContent &= "<a href=""cfrmdocumentos.aspx?myact=1&_view_=" & CInt(enumView.coBiblioteca_Cancelacion) & """ >"
            auxContent &= " <div class=""tile-content icon"">"
            auxContent &= "     <img style=""padding-bottom:15px;padding-left:5px;"" src=""imagenes/icondocrol00000006.png"">"
            auxContent &= " </div>"
            auxContent &= " <div class=""brand  bg-black"">"
            auxContent &= "		<span class=""label"">Cancelación</span>"
            auxContent &= "     <span class=""badge"" " & auxBadgeStyle & """>" & auxCant & "</span>"
            auxContent &= " </div>"
            auxContent &= " </a>"
            auxContent &= "</div>"

            auxWhere = " AND DOC_DOCSGN.doccod IN (SELECT cod FROM DOC_DOC WHERE DOC_DOC.wfwstatus IN (" & enumWorkflowStep.coWFWSTPDOC_DOCEliminaciontotal & "))"
            auxCant = auxClass.Conn.gConn_QueryValueInt("SELECT COUNT(*) " _
            & " FROM DOC_DOCSGN " _
            & " LEFT JOIN DOC_DOC ON DOC_DOCSGN.doccod=DOC_DOC.cod" _
            & " WHERE " _
            & " (DOC_DOC.baja  {#ISNULL#} OR DOC_DOC.baja ={#FALSE#}) " _
            & " AND empcod= " & pEmpCod _
                        & " AND doclogcod IN (" _
                        & "SELECT cod FROM DOC_DOCLOG WHERE wfwstepnext=" & enumWorkflowStep.coWFWSTPDOC_DOCEliminaciontotal _
                        & ")", 0)
            auxContent &= "<div class=""tile " & auxTileBGColor & " icon"" " & auxPanelStyle & ">"
            auxContent &= "<a href=""cfrmdocumentos.aspx?myact=1&_view_=" & CInt(enumView.coBiblioteca_Eliminacion) & """ >"
            auxContent &= " <div class=""tile-content icon"">"
            auxContent &= "     <img style=""padding-bottom:15px;padding-left:5px;"" src=""imagenes/icondocrol00000010.png"">"
            auxContent &= " </div>"
            auxContent &= " <div class=""brand  bg-black"">"
            auxContent &= "		<span class=""label"">Eliminación</span>"
            auxContent &= "     <span class=""badge"" " & auxBadgeStyle & """>" & auxCant & "</span>"
            auxContent &= " </div>"
            auxContent &= " </a>"
            auxContent &= "</div>"

            'documentos-favritos
            Dim auxDTFav As DataTable
            auxDTFav = auxClass.Conn.gConn_Query(" SELECT TOP 10  " _
                                                & " DOC_DOCVIG.cod,DOC_DOCVIG.dsc " _
                                                & " FROM DOC_EMPDOC " _
                                                & " LEFT JOIN DOC_DOCVIG ON DOC_EMPDOC.doccod=DOC_DOCVIG.cod " _
                                                & " WHERE DOC_EMPDOC.empcod=" & pEmpCod _
                                                & " AND DOC_EMPDOC.reltypeid=2" _
                                                & " AND DOC_DOCVIG.cod IS NOT NULL" _
                                                & " ORDER BY DOC_EMPDOC.qsecdatetime DESC" _
                                                & "")
            If auxDTFav.Rows.Count <> 0 Then
                auxContent &= "<span class=""" & auxFGColor & """>" _
                    & "<img src=""imagenes/doc_vigentes.png"" width=20px class=""hrcthemeimage metro-tile-group-title-icon"" >Favoritos vigentes</span>"
                auxContent &= "<div class=""listview-outlook"" >"
                For Each auxRow As DataRow In auxDTFav.Rows
                    auxContent &= "<div class=""list"">" _
                       & "<div class=""list-content"">" _
                       & "<div class=""list-remark"">" _                       & "<a href=""cfrmdocumentos_impresion.aspx?_closea_=1&_mode_=8&param1=" & auxRow("cod") & """ >" _                       & auxRow("dsc") _                       & "</a>" _                       & "</div>" _
                       & "</div>" _
                       & "</div>"
                Next
                auxContent &= "</div>" 'listview-outlook
            End If



            'ultimos vigentes
            auxContent &= "<span class=""" & auxFGColor & """>" _
                & "<img src=""imagenes/doc_vigentes.png"" width=20px class=""hrcthemeimage metro-tile-group-title-icon"" >Últimos vigentes accedidos</span>"
            auxContent &= "<div class=""listview-outlook"" >"

            For Each auxRow As DataRow In auxClass.Conn.gConn_Query(" SELECT TOP 10  " _
                                                                    & " DOC_DOCVIG.cod,DOC_DOCVIG.dsc " _
                                                                    & " FROM DOC_EMPDOC " _
                                                                    & " LEFT JOIN DOC_DOCVIG ON DOC_EMPDOC.doccod=DOC_DOCVIG.cod " _
                                                                    & " WHERE DOC_EMPDOC.empcod=" & pEmpCod _
                                                                    & " AND DOC_EMPDOC.reltypeid=1" _
                                                                    & " ORDER BY DOC_EMPDOC.qsecdatetime DESC" _
                                                                    & "").Rows
                auxContent &= "<div class=""list"">" _
                   & "<div class=""list-content"">" _
                   & "<div class=""list-remark"">" _                   & "<a href=""cfrmdocumentos_impresion.aspx?_closea_=1&_mode_=8&param1=" & auxRow("cod") & """ >" _                   & auxRow("dsc") _                   & "</a>" _                   & "</div>" _
                   & "</div>" _
                   & "</div>"
            Next
            auxContent &= "</div>" 'listview-outlook

            auxContent &= "</div>" 'tile-group-column1

            'tile-group-column2
            auxContent &= "<div class=""tile-group quadro no-margin"" >"
            auxContent &= "<span class=""tile-group-title " & auxFGColor & """>" _
                & "<a href=cfrmdocumentos.aspx?_view_=" & enumView.coVigente_PorTipoProcesoyProceso & " >" _
                & "<img src=""imagenes/doc_vigentes.png""  width=28px class=""hrcthemeimage metro-tile-group-title-icon"" >" _
                & "Documentos vigentes" _
                & "</a>" _
                & "</span>"
            Dim auxIsFirst As Boolean = False
            Dim auxDT As DataTable = gData_GetDT(enumView.coVigente_PorTipoProcesoyProceso, auxClass, Nothing, False, pEmpCod, pIsAdmin, -1, -1)
            For Each auxRowAPA As DataRow In auxDT.Select("q_type=" & enumEntities.coEntityDOC_APA)
                auxIsFirst = True
                For Each auxRowPRO As DataRow In auxDT.Select("q_parent='" & auxRowAPA("q_cod") & "'")
                    If auxIsFirst Then
                        auxContent &= "<div class=""" & auxFGColor & """>" _
                            & "<a href=cfrmdocumentos.aspx?_view_=" & enumView.coMetro_PorTipoProceso & "&apa=" & auxRowAPA("cod") & " >" _
                            & "<img src=""imagenes/icon" & Format(CInt(enumEntities.coEntityDOC_APA), "00000000") & ".png"" class=hrcthemeimage width=14px > " _
                            & auxRowAPA("q_dsc") _
                            & "</a>" _
                            & "</div>"
                        auxContent &= "<div class=""listview-outlook"" >"
                        auxIsFirst = False
                    End If
                    auxContent &= "<div class=""list"">" _
                            & "<div class=""list-content"">" _
                            & "<div class=""list-remark"">" _                               & "<a href=""cfrmdocumentos.aspx?_view_=" & CInt(enumView.coMetro_PorProceso) & "&pro=" & auxRowPRO("cod") & """ >" _                               & "<img src=""imagenes/icon" & Format(CInt(enumEntities.coEntityDOC_PRO), "00000000") & ".png"" class=hrcthemeimage width=14px > " _                               & auxRowPRO("q_dsc") _                               & "</a>" _                            & "</div>" _
                            & "</div>" _
                            & "</div>"
                Next
                If auxIsFirst = False Then
                    auxContent &= "</div>" 'listview-outlook
                End If

            Next

            auxContent &= "</div>" 'tile-group-column2

        ElseIf pView = enumView.coMetro_PorProceso Then


            'tile-group-column2
            auxContent &= "<div class=""tile-group quadro no-margin"" >"


            Dim auxDTPro As DataTable = auxClass.Conn.gConn_Query("SELECT dsc FROM DOC_PRO" _
                                                                & " WHERE cod=" & pProCod)

            auxContent &= "<nav class=""breadcrumbs"">" _
               & "<ul>" _
                & "<li><a href=""cfrmdocumentos.aspx?_view_=" & CInt(enumView.coMetro_Inicial) & """>" _
                & "<img style=""cursor: pointer;"" src=""" & auxClass.WebRootFolder & "imagenes/objhome.png"" class=hrcthemeimage style=""height:16px"" >" _
                & "</a>" _
                & "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<img src=""imagenes/doc_vigentes.png"" width=20px class=""hrcthemeimage metro-tile-group-title-icon"" >Proceso " & auxDTPro.Rows(0)("dsc") _
                & "</li>" _
                & "</ul>" _
                & "</nav>"
            Dim auxIsFirst As Boolean = False
            Dim auxDT As DataTable = gData_GetDT(enumView.coVigente_PorTipoProceso, auxClass, Nothing, False, pEmpCod, pIsAdmin, pProCod, -1)
            'auxDT.Columns.Add(New DataColumn("q_isleaf", Type.GetType("System.Boolean")))
            'Dim auxAllID As New List(Of String)
            'Dim auxNoLeafIDs As New List(Of String)
            'For Each auxRow As DataRow In auxDT.Rows
            '    If auxAllID.IndexOf(auxRow("q_parent")) <> -1 Then
            '        auxNoLeafIDs.Add(auxRow("q_cod"))
            '    End If
            '    auxAllID.Add(auxRow("q_cod"))
            'Next
            Dim auxVisualizationMode As Short = 3
            If auxVisualizationMode = 1 Then
                For Each auxRowDOCTIP As DataRow In auxDT.Select("q_type=" & enumEntities.coEntityDOC_DOCTIP)
                    auxIsFirst = True
                    If auxVisualizationMode = 1 Then
                        'Con Outlooklist
                        For Each auxRowDOC As DataRow In auxDT.Select("q_parent='" & auxRowDOCTIP("q_cod") & "'")
                            If auxIsFirst Then
                                auxContent &= "<span class=""" & auxFGColor & """>" & auxRowDOCTIP("q_dsc") & "</span>"
                                auxContent &= "<div class=""listview-outlook"" >"
                                auxIsFirst = False
                            End If
                            auxContent &= "<div class=""list"">" _
                                    & "<div class=""list-content"">" _
                                    & "<div class=""list-remark"">" _                                    & "<a href=""cfrmdocumentos_impresion.aspx?_closea_=1&_mode_=8&param1=" & auxRowDOC("cod") & """ >" _                                       & auxRowDOC("q_dsc") _                                       & "</a>" _                                    & "</div>" _
                                    & "</div>" _
                                    & "</div>"
                        Next
                        If auxIsFirst = False Then
                            auxContent &= "</div>" 'listview-outlook
                        End If
                    Else
                    End If
                Next
            Else

                auxDT.Columns.Add(New DataColumn("q_tmpisleaf", Type.GetType("System.Boolean")))
                auxContent &= "<ul class=""treeview"" data-role=""treeview"">" _
                    & "<li class=""node collapse"">"

                'Con treeview
                Dim auxRows() As DataRow = auxDT.Select
                Dim auxCurrentRowIndex As Integer = 0
                Dim auxParentIDList As New List(Of String)
                Dim auxEnd As Boolean = auxRows.Count = 0
                Dim auxStack As New Stack(Of DataRow)
                Dim auxToggle As Boolean = False
                Dim auxSubEnd As Boolean = False
                Dim auxCurrentRow As DataRow
                Dim auxDirectionOUT As Boolean = False
                'FALSE=IN   -lee DT
                'TRUE=OUT   -saca de Stack
                Dim auxAddRow As Boolean = False
                Do While auxEnd = False
                    If auxDirectionOUT = False Then
                        If auxCurrentRowIndex < auxRows.Count Then
                            auxCurrentRow = auxRows(auxCurrentRowIndex)
                            If auxStack.Count = 0 Then
                                auxRows(auxCurrentRowIndex)("q_tmpisleaf") = True
                                auxStack.Push(auxRows(auxCurrentRowIndex))
                                auxCurrentRowIndex += 1
                            Else
                                If auxStack.Peek("q_cod") = auxCurrentRow("q_parent") Then
                                    If auxStack.Peek("q_tmpisleaf") = True Then
                                        'El PEEK es el padre- lo marca como NOLEAF
                                        auxStack.Peek("q_tmpisleaf") = False
                                        auxContent &= "<li class=""node"">"
                                        auxContent &= "<a href=""#"" ><span class=""node-toggle""></span>"
                                        auxContent &= "<span >" & auxStack.Peek("q_dsc") & "</span>"
                                        'auxContent &= "<div class=""listview-outlook"" >"
                                        auxContent &= "</a>"
                                        auxContent &= "<ul>"
                                    End If

                                    'Agrega la fila como isleaf
                                    auxCurrentRow("q_tmpisleaf") = True
                                    auxStack.Push(auxCurrentRow)
                                    auxCurrentRowIndex += 1
                                Else
                                    Dim auxLink As String = ""
                                    auxLink = "<a href=""cfrmdocumentos_impresion.aspx?_closea_=1&_mode_=8&param1=" & auxStack.Peek("cod") & """ >" _
                                        & "<img src=""imagenes/icon00000001.png"" width=16px class=hrcthemeimage >" _
                                        & auxStack.Peek("q_dsc") _
                                        & "</a>"
                                    auxContent &= "<li>" _
                                           & "<a href=# >" _                                              & auxLink _                                              & "</a>" _                                           & "</li>"

                                    auxDirectionOUT = True
                                End If
                            End If
                        Else
                            auxEnd = True
                        End If
                    Else
                        If auxStack.Count = 0 Then
                            auxDirectionOUT = False
                        Else
                            If auxDT.Rows(auxCurrentRowIndex)("q_parent") = auxStack.Peek("q_cod") Then
                                auxDirectionOUT = False
                            Else
                                auxCurrentRow = auxStack.Pop
                                If auxCurrentRow("q_tmpisleaf") = False Then
                                    auxContent &= "</ul>"
                                End If
                            End If
                        End If
                    End If
                Loop
                auxContent &= "</il>" _
                 & "</ul>"
            End If

            If auxVisualizationMode = 2 Then

            End If
            auxContent &= "</div>" 'tile-group-column2
        End If






        auxContent &= "</div>" 'area
        auxContent &= "</div>" 'metro

        auxClass.Conn.gConn_Close()
        Dim auxReturn As New clshrcBagValues
        auxReturn.gValue_Add("CONTENT", auxContent)
        auxReturn.gValue_Add("SCRIPT", auxScript)
        Return auxReturn
    End Function


    Protected Sub frmDocumentos_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        Dim auxHTML As New clsHrcCodeHTML
        auxHTML.DateFormatEnabled = False
        Dim auxQueryValues As clshrcBagValues
        auxQueryValues = auxHTML.gBagValues_GetFromQueryString(Request.QueryString.ToString)
        Dim auxClass As New clsCusimDOC
        auxClass.Conn.gConn_Open()

        Dim auxView As enumView
        If auxQueryValues.gValue_Get("_VIEW_") Is Nothing Then
            auxView = Val(auxClass.gSystem_GetParameterByID(coSysParamIDDOCViewDefault))
        Else
            auxView = Val(auxQueryValues.gValue_Get("_VIEW_", -1))
        End If
        If auxView < 1 Then
            auxView = Val(auxClass.gLoginPreference_GetValue(enumPrefType.coDocumentossearch, "view"))
        End If
        If auxView < 1 Then
            auxView = enumView.coMetro_Inicial
        End If
        Dim auxMode As clsHrcCodeHTML.enumWindowMode = Val(auxQueryValues.gValue_Get("_WINMODE_", -1))
        If auxMode < 1 Then
            auxMode = clsHrcCodeHTML.enumWindowMode.coNormal
        End If
       
        If Session("modo_obligado") Then
            auxView = enumView.coObligado   'Modo obligado
        End If
        Dim auxClientCon As New imClientConnection
        Dim auxBagResult As clshrcBagValues
        Dim auxEmpCod As Integer = Session("empcod")
        Dim auxIsAdmin As Boolean = Session("isadmin")
        If auxMode = clsHrcCodeHTML.enumWindowMode.coNormal Then
            Select Case auxView
                Case enumView.coMetro_Inicial ', enumView.coMetro_PorProceso   'metro
                    Dim auxProcod As Integer
                    If auxView = enumView.coMetro_PorProceso Then
                        auxProcod = Val(Request.QueryString("pro"))
                    End If
                    auxBagResult = gFormDocuments_GetMetroView(auxView, auxEmpCod, auxIsAdmin, auxProcod, -1)
                Case Else
                    Dim auxMisAccionesVisible As Boolean = False
                    Dim auxProcod As Integer = -1
                    Dim auxApacod As Integer = -1
                    Dim auxIncludeChildsVisible As Boolean = False

                    Select Case auxView
                        Case enumView.coVigente_PorTipoProceso, enumView.coVigente_PorUnidad, enumView.coVigente_PorTipoDoc, enumView.coVigente_PorTipoProcesoyProceso, enumView.coVigente_PorSistema _
                             , enumView.coBiblioteca_Edicion, enumView.coBiblioteca_Revision, enumView.coBiblioteca_Aprobacion, enumView.coBiblioteca_Cancelacion, enumView.coBiblioteca_Lectura, enumView.coBiblioteca_NuevasVersiones, enumView.coBiblioteca_NuevosDoc, enumView.coBiblioteca_PorSistema, enumView.coBiblioteca_PorTipoDoc, enumView.coBiblioteca_PorTipoproceso, enumView.coBiblioteca_Creacion, enumView.coBiblioteca_Eliminacion, enumView.coBiblioteca_PorUnidad
                            auxMisAccionesVisible = True
                        Case enumView.coMetro_PorProceso
                            auxProcod = Val(Request.QueryString("pro"))
                        Case enumView.coMetro_PorTipoProceso
                            auxApacod = Val(Request.QueryString("apa"))
                        Case Else
                    End Select
                    Select Case auxView
                        Case enumView.coBiblioteca_PorSistema, enumView.coBiblioteca_PorTipoDoc, enumView.coBiblioteca_PorTipoproceso, enumView.coBiblioteca_PorTipoProcesoyProceso, enumView.coBiblioteca_PorUnidad, _
                            enumView.coVigente_PorSistema, enumView.coVigente_PorTipoDoc, enumView.coVigente_PorTipoProceso, enumView.coVigente_PorTipoProcesoyProceso, enumView.coVigente_PorUnidad
                            auxIncludeChildsVisible = True
                    End Select
                    Dim auxMisAccionesOn As Short = -1
                    auxMisAccionesOn = auxClass.Conn.gField_GetInt(auxQueryValues.gValue_Get("myact", -1))
                    If auxMisAccionesOn = 1 Then
                        auxMisAccionesVisible = False
                    End If

                    Dim auxPermFormNew As Boolean = False
                    If Session("DOC_DOC_Und_PermEditor") <> "" Then
                        auxPermFormNew = True
                    End If
                    'todos pueden crear
                    auxPermFormNew = True
                    auxBagResult = gFormDocuments_Get(auxView, False, auxMisAccionesVisible, auxMisAccionesOn, auxIncludeChildsVisible, auxPermFormNew, auxEmpCod, auxIsAdmin, auxProcod, auxApacod)
            End Select
            ScriptManager.RegisterStartupScript(Me, Me.GetType, "cfrmdocumentos", _
                                     auxBagResult.gValue_Get("SCRIPT", ""), True)
            Page.Title = auxBagResult.gValue_Get("TITLE", "")
            fmeContent.InnerHtml = auxBagResult.gValue_Get("CONTENT", "")

       
        End If

        auxClass.Conn.gConn_Close()
    End Sub
    Private Sub gDownloadXLS_CommandHandler(ByVal pControl As clsHrcJSControlBasic, ByVal pValues As clshrcBagValues)
        Dim auxDT As DataTable = pValues.gValue_Get("XLS_dt")
        Dim auxView As enumView = pValues.gValue_Get("_view_")
        If auxDT Is Nothing Then
            Exit Sub
        ElseIf auxDT.Rows.Count = 0 Then
            Exit Sub
        End If
        auxDT.PrimaryKey = Nothing

        Dim auxColumnsToDelete As New List(Of String)
        auxColumnsToDelete.Add("q_cod")
        auxColumnsToDelete.Add("q_parent")
        auxColumnsToDelete.Add("q_group")
        'auxColumnsToDelete.Add("q_type")
        auxColumnsToDelete.Add("q_added")
        auxColumnsToDelete.Add("q_itemvalue")
        auxColumnsToDelete.Add("q_sourcegroup")

        Select Case auxView
            Case Else
                auxColumnsToDelete.Add("clacod")
                auxColumnsToDelete.Add("apacod")
                auxColumnsToDelete.Add("undcod")
                auxColumnsToDelete.Add("doctipcod")
                auxColumnsToDelete.Add("orden")
                auxColumnsToDelete.Add("docvig_read")
                auxColumnsToDelete.Add("wfwstpcod")
                auxColumnsToDelete.Add("qsidcod")
        End Select
        For Each auxColumnName As String In auxColumnsToDelete
            If auxDT.Columns.IndexOf(auxColumnName) <> -1 Then
                auxDT.Columns.Remove(auxColumnName)
            End If
        Next
        For Each auxColumn As DataColumn In auxDT.Columns
            auxColumn.ColumnName = Replace(auxColumn.ColumnName, ",", "_")
            auxColumn.ColumnName = Replace(auxColumn.ColumnName, " ", "_")
            auxColumn.ColumnName = Replace(auxColumn.ColumnName, "/", "_")
        Next
        Dim auxConnection As New imClientConnection

        Dim auxConn As clsHrcConnClient = Session("conn")

        auxConn = auxConn.gComponent_CreateInstance
        Dim auxTmpGuid As String = pValues.gValue_Get("xls_tmpid")
        Dim auxXLS As clsHrcConnClient.clsBinaryData = auxConn.gDataTable_ToExcelBinary(auxDT)
        auxXLS.Filename = pValues.gValue_Get("xls_filename")
        auxConnection.gObjectTmp_Upload(auxXLS, auxTmpGuid)
        'Dim auxTmpGuid As String = pValues.gValue_Get("xls_tmpid")
        'Dim auxFileName As String = pValues.gValue_Get("xls_filename")
        'auxConnection.gFileTmp_Upload(auxFileName, auxXLS.Content, -1, -1, auxTmpGuid)
    End Sub

    Private Function gData_GetDT(ByVal pView As enumView, _
                                 ByVal pClass As clsCusimDOC, _
                                 ByVal pPanel As clsHrcJSPanel, _
                                 ByVal pSysParamInboxDetailedEnabled As Boolean, _
                                 ByVal pEmpCod As Integer, _
                                 ByVal pIsAdmin As Boolean, _
                                 ByVal pProCod As Integer, _
                                 ByVal pApaCod As Integer) As DataTable
        Dim auxSelectParams As clshrcBagValues = Nothing
        Dim auxIncludeChilds As Boolean = False
        If pView <> enumView.coObligado And pPanel IsNot Nothing Then
            auxSelectParams = pPanel.gFieldData_GetValues(False, True)
            If auxSelectParams.Values.Count = 0 Then
                '  auxSelectParams = Nothing
            Else
                If pClass.Conn.gField_GetBoolean(auxSelectParams.gValue_Get("includechilds", False)) Then
                    auxIncludeChilds = True
                End If
            End If
            auxSelectParams.gValue_Add("misacciones_default", pPanel.BagValues.gValue_Get("misacciones_default"))
        End If

        Dim auxSelect As String = gData_GetSelectCommand(pView, pClass, auxSelectParams, pSysParamInboxDetailedEnabled, pEmpCod, pIsAdmin, pProCod, pApaCod)
        Dim auxSelect2 As String = ""
        Dim auxSelect3 As String = ""
        Dim auxSelect4 As String = ""
        auxSelect2 = "SELECT cod as q_cod,dsc as q_dsc,NULL,apacod," & enumEntities.coEntityDOC_PRO & ",cod FROM DOC_PRO ORDER BY dsc"
        auxSelect3 = "SELECT cod as q_cod,dsc as q_dsc,NULL,NULL," & enumEntities.coEntityDOC_APA & ",cod FROM DOC_APA ORDER BY dsc"

        Select Case pView
            Case enumView.coObligado  'Modo obligado
                auxSelect2 = "SELECT wfwstpcod as q_cod,wfwstpdsc q_dsc,NULL,wfwstpcod,-1,-1 FROM Q_WFWSTP ORDER BY wfwstpdsc"
            Case enumView.coVigente_PorTipoProceso   'Vigentes por tipo de proceso v2
                auxSelect2 = "SELECT (DOC_DOCTIP.cod * 500 + DOC_PRO.cod) as q_cod,DOC_DOCTIP.dsc as q_dsc,NULL,DOC_PRO.cod," & enumEntities.coEntityDOC_DOCTIP & ",(DOC_DOCTIP.cod * 500 + DOC_PRO.cod) FROM DOC_PRO,DOC_DOCTIP ORDER BY DOC_DOCTIP.orden,DOC_DOCTIP.dsc"
                auxSelect3 = "SELECT cod as q_cod,dsc as q_dsc,NULL,apacod," & enumEntities.coEntityDOC_PRO & ",cod FROM DOC_PRO ORDER BY dsc"
                auxSelect4 = "SELECT cod as q_cod,dsc as q_dsc,NULL,NULL," & enumEntities.coEntityDOC_APA & ",cod FROM DOC_APA ORDER BY dsc"
            Case enumView.coMetro_PorProceso
                auxSelect2 = "SELECT (DOC_DOCTIP.cod * 500 + DOC_PRO.cod) as q_cod,DOC_DOCTIP.dsc as q_dsc,NULL,DOC_PRO.cod," & enumEntities.coEntityDOC_DOCTIP & ",(DOC_DOCTIP.cod * 500 + DOC_PRO.cod) FROM DOC_PRO,DOC_DOCTIP ORDER BY DOC_DOCTIP.orden,DOC_DOCTIP.dsc"
                auxSelect3 = ""
                auxSelect4 = ""
            Case enumView.coMetro_PorTipoProceso
                auxSelect2 = "SELECT (DOC_DOCTIP.cod * 500 + DOC_PRO.cod) as q_cod,DOC_DOCTIP.dsc as q_dsc,NULL,DOC_PRO.cod," & enumEntities.coEntityDOC_DOCTIP & ",(DOC_DOCTIP.cod * 500 + DOC_PRO.cod) FROM DOC_PRO,DOC_DOCTIP ORDER BY DOC_DOCTIP.orden,DOC_DOCTIP.dsc"
                auxSelect3 = "SELECT cod as q_cod,dsc as q_dsc,NULL,apacod," & enumEntities.coEntityDOC_PRO & ",cod FROM DOC_PRO ORDER BY dsc"
                auxSelect4 = ""
            Case enumView.coVigente_PorUnidad  'Vigentes por unidad
                auxSelect2 = "SELECT (DOC_DOCTIP.cod * 500 + UND.cod) as q_cod,DOC_DOCTIP.dsc as q_dsc,NULL,UND.cod," & enumEntities.coEntityDOC_DOCTIP & ",(DOC_DOCTIP.cod * 500 + UND.cod) FROM UND,DOC_DOCTIP ORDER BY DOC_DOCTIP.orden,DOC_DOCTIP.dsc"
                auxSelect3 = "SELECT cod as q_cod,dsc as q_dsc,undcodsup,NULL," & enumEntities.coEntityUND & ",cod FROM UND ORDER BY dsc"
            Case enumView.coVigente_PorTipoDoc  'Vigentes  por tipos de documentos
                auxSelect2 = "SELECT DOC_DOCTIP.cod as q_cod,DOC_DOCTIP.dsc as q_dsc,NULL,NULL," & enumEntities.coEntityDOC_DOCTIP & ",DOC_DOCTIP.cod FROM DOC_DOCTIP ORDER BY DOC_DOCTIP.orden,DOC_DOCTIP.dsc"
            Case enumView.coBiblioteca_PorTipoproceso    'bIBLIOTECA  por tipo proceso v2
                auxSelect2 = "SELECT (DOC_DOCTIP.cod * 500 + DOC_PRO.cod) as q_cod,DOC_DOCTIP.dsc as q_dsc,NULL,DOC_PRO.cod," & enumEntities.coEntityDOC_DOCTIP & ",(DOC_DOCTIP.cod * 500 + DOC_PRO.cod) FROM DOC_PRO,DOC_DOCTIP " _
                    & " WHERE DOC_DOCTIP.cod > 0 " _
                    & " ORDER BY DOC_DOCTIP.orden,DOC_DOCTIP.dsc"
                auxSelect3 = "SELECT cod as q_cod,dsc as q_dsc,NULL,apacod," & enumEntities.coEntityDOC_PRO & ",cod FROM DOC_PRO ORDER BY dsc"
                auxSelect4 = "SELECT cod as q_cod,dsc as q_dsc,NULL,NULL," & enumEntities.coEntityDOC_APA & ",cod FROM DOC_APA ORDER BY dsc"

            Case enumView.coBiblioteca_PorUnidad  'bIBLIOTECA por unidad
                auxSelect2 = "SELECT (DOC_DOCTIP.cod * 500 + UND.cod) as q_cod,DOC_DOCTIP.dsc as q_dsc,NULL,UND.cod," & enumEntities.coEntityDOC_DOCTIP & ",(DOC_DOCTIP.cod * 500 + UND.cod) FROM UND,DOC_DOCTIP ORDER BY DOC_DOCTIP.orden,DOC_DOCTIP.dsc"
                auxSelect3 = "SELECT cod as q_cod,dsc as q_dsc,undcodsup,NULL," & enumEntities.coEntityUND & ",cod FROM UND ORDER BY dsc"
            Case enumView.coBiblioteca_PorTipoDoc   'bIBLIOTECA  por tipos de documentos
                auxSelect2 = "SELECT DOC_DOCTIP.cod as q_cod,DOC_DOCTIP.dsc as q_dsc,NULL,NULL," & enumEntities.coEntityDOC_DOCTIP & ",DOC_DOCTIP.cod FROM DOC_DOCTIP ORDER BY DOC_DOCTIP.orden,DOC_DOCTIP.dsc"
            Case enumView.coVigente_PorTipoProcesoyProceso   'Vigentes  por tipos de procesos>procesos
                'auxSelect2 = "SELECT cod as q_cod,dsc as q_dsc,NULL,apacod," & enumEntities.coEntityDOC_PRO & ",cod FROM DOC_PRO ORDER BY dsc"
                'auxSelect3 = "SELECT cod as q_cod,dsc as q_dsc,NULL,NULL," & enumEntities.coEntityDOC_APA & ",cod FROM DOC_APA ORDER BY dsc"
                auxSelect2 = "SELECT (DOC_DOCTIP.cod * 500 + DOC_PRO.cod) as q_cod,DOC_DOCTIP.dsc as q_dsc,NULL,DOC_PRO.cod," & enumEntities.coEntityDOC_DOCTIP & ",(DOC_DOCTIP.cod * 500 + DOC_PRO.cod) FROM DOC_PRO,DOC_DOCTIP ORDER BY DOC_DOCTIP.orden,DOC_DOCTIP.dsc"
                auxSelect3 = "SELECT cod as q_cod,dsc as q_dsc,NULL,apacod," & enumEntities.coEntityDOC_PRO & ",cod FROM DOC_PRO ORDER BY dsc"
                auxSelect4 = "SELECT cod as q_cod,dsc as q_dsc,NULL,NULL," & enumEntities.coEntityDOC_APA & ",cod FROM DOC_APA ORDER BY dsc"
            Case enumView.coBiblioteca_PorTipoProcesoyProceso   'bIBLIOTECA  por tipos de procesos>procesos
                auxSelect2 = "SELECT cod as q_cod,dsc as q_dsc,NULL,apacod," & enumEntities.coEntityDOC_PRO & ",cod FROM DOC_PRO ORDER BY dsc"
                auxSelect3 = "SELECT cod as q_cod,dsc as q_dsc,NULL,NULL," & enumEntities.coEntityDOC_APA & ",cod FROM DOC_APA ORDER BY dsc"

            Case enumView.coVigente_PorSistema   'Vigentes  por sistema
                auxSelect2 = "SELECT cod as q_cod,dsc as q_dsc,NULL,NULL," & enumEntities.coEntityDOC_SIS & ",cod FROM DOC_SIS  ORDER BY dsc"
            Case enumView.coBiblioteca_PorSistema   'bIBLIOTECA  por sistema
                auxSelect2 = "SELECT cod as q_cod,dsc as q_dsc,NULL,NULL," & enumEntities.coEntityDOC_SIS & ",cod FROM DOC_SIS ORDER BY dsc"

        End Select
        Dim auxHierarchyTable As New Intelimedia.Hercules.Storage.clsHrcHierarchyTable(Nothing, String.Empty)
        'auxHierarchyTable.gDebug_On("c:\windows\temp\doc_cache2.txt")
        If auxIncludeChilds Then
            Dim auxSelectionList As New SortedList(Of Integer, Intelimedia.Hercules.Storage.clsHrcHierarchyTable.enumSelectionType)
            For Each auxRow As DataRow In pClass.Conn.gConn_Query(auxSelect).Rows
                auxSelectionList.Add(auxRow("cod"), Intelimedia.Hercules.Storage.clsHrcHierarchyTable.enumSelectionType.coNestedParents _
                                     Or Intelimedia.Hercules.Storage.clsHrcHierarchyTable.enumSelectionType.coChilds)
            Next
            auxSelect = gData_GetSelectCommand(pView, pClass, Nothing, pSysParamInboxDetailedEnabled, pEmpCod, pIsAdmin, pProCod, pApaCod)
            auxHierarchyTable.gGroup_Add(pClass.Conn.gConn_Query(auxSelect), -1, False, "Documentos sin superior listado")
            auxHierarchyTable.gGroup_AddSelectionList(auxSelectionList)
        Else
            auxHierarchyTable.gGroup_Add(pClass.Conn.gConn_Query(auxSelect), -1, False, "Documentos sin superior listado")
        End If

        ' auxHierarchyTable.gGroup_Add(pClass.Conn.gConn_Query(auxSelect), -1, False, "Documentos sin superior listado")
        '        Dim auxIncludeChilds As Boolean = auxPanelValues.gValue_Get("includechilds", False)
        '        auxValue =
        'auxSelectionList.Add(13, Intelimedia.Hercules.Storage_test.clsHrcHierarchyTable.enumSelectionType.coNestedParents)
        'auxSelectionList.Add(52, Intelimedia.Hercules.Storage_test.clsHrcHierarchyTable.enumSelectionType.coNestedParents)
        'auxSelectionList.Add(57, Intelimedia.Hercules.Storage_test.clsHrcHierarchyTable.enumSelectionType.coNestedParents)

        'auxSelectionList.Add(57, Intelimedia.Hercules.Storage_test.clsHrcHierarchyTable.enumSelectionType.coChilds Or Intelimedia.Hercules.Storage_test.clsHrcHierarchyTable.enumSelectionType.coNestedParents)
        'auxSelectionList.Add(57, Intelimedia.Hercules.Storage_test.clsHrcHierarchyTable.enumSelectionType.coNestedParents)
        'auxSelectionList.Add(57, Intelimedia.Hercules.Storage_test.clsHrcHierarchyTable.enumSelectionType.coChilds)

        If auxSelect2 <> "" Then
            auxHierarchyTable.gGroup_Add(pClass.Conn.gConn_Query(auxSelect2), -1, False, "Documentos sin superior listado")
        End If

        If auxSelect3 <> "" Then
            auxHierarchyTable.gGroup_Add(pClass.Conn.gConn_Query(auxSelect3))
        End If

        If auxSelect4 <> "" Then
            auxHierarchyTable.gGroup_Add(pClass.Conn.gConn_Query(auxSelect4))
        End If
        'auxDTCache = auxHierarchyTable.gHierarchy_GenerateCache

        Dim auxReturn As DataTable
        auxReturn = auxHierarchyTable.gHierarchy_GenerateTable
        'Dim auxHierarchyTabletest As New Intelimedia.Hercules.Storage_test.clsHrcHierarchyTable(Nothing, String.Empty)
        'auxHierarchyTabletest.gGroup_Add(pClass.Conn.gConn_Query(auxSelect), -1, False, "Documentos sin superior listado")
        'If auxSelect2 <> "" Then
        '    auxHierarchyTabletest.gGroup_Add(pClass.Conn.gConn_Query(auxSelect2))
        'End If
        'If auxSelect3 <> "" Then
        '    auxHierarchyTabletest.gGroup_Add(pClass.Conn.gConn_Query(auxSelect3))
        'End If
        'If auxSelect4 <> "" Then
        '    auxHierarchyTabletest.gGroup_Add(pClass.Conn.gConn_Query(auxSelect4))
        'End If
        'auxHierarchyTabletest.gDebug_On("c:\windows\temp\doc_cache.txt")
        'auxReturn = auxHierarchyTabletest.gHierarchy_GenerateTable_WithCache(auxDTCache)
        'auxReturn = auxDTCache
        'auxReturn = auxHierarchyTable.gHierarchy_GenerateTable
        If auxReturn.Rows.Count = 0 Then
            pClass.gTRACE_add(-1, 5, "No rows in cfrmdocumentos:" & pClass.Conn.LastErrorDescription) '& "." & pSelectCommand)
        End If
        Return auxReturn
    End Function
    Private Function gData_GetSelectCommand(ByVal pView As enumView, _
                                            ByVal pClass As clsCusimDOC, _
                                            ByVal auxPanelValues As clshrcBagValues, _
                                            ByVal pSysParamInboxDetailedEnabled As Boolean, _
                                            ByVal pEmpCod As Integer, _
                                            ByVal pIsAdmin As Boolean, _
                                            ByVal pProCod As Integer, _
                                            ByVal pApaCod As Integer) As String
        Dim auxFilter As Boolean = False
        Dim auxSelect1 As String = String.Empty
        Dim auxFrom As String = ""
        Dim auxGroups As String = ""
        Dim auxWhere As String = ""
        Dim auxFields As String = ""
        If pProCod > 0 Then
            auxWhere &= " AND DOC_DOC.procod=" & pProCod
        ElseIf pApaCod > 0 Then
            auxWhere &= " AND DOC_DOC.procod IN (SELECT cod FROM DOC_PRO WHERE apacod=" & pApaCod & ")"
        End If

        Dim auxAccTypeList As String = glPermRead
        Dim auxOrderBy As String = " ORDER BY DOC_DOC.dsc"
        'Dim auxOrderBy As String = " ORDER BY DOC_DOC.priorizacion,REQ_REQ.orden,REQ_REQ.cod"
        'auxOrderBy = " ORDER BY DOC_DOCTIP.orden"
        Dim auxCodValue As String = "DOC_DOC.cod"
        auxFrom &= " FROM DOC_DOC AS DOC_DOC"
        auxGroups = "CASE WHEN DOC_DOC.docsupcod > 0 THEN DOC_DOC.docsupcod ELSE NULL END,DOC_DOC.procod,"
        Select Case pView
            Case enumView.coBiblioteca_SinAnidar    '0=BIBLIOTECA sin anidación
                auxFrom = " FROM DOC_DOC"
                auxGroups = "NULL,NULL,"
            Case enumView.coObligado   'MODO OBLIGADO
                auxFrom = " FROM DOC_DOCVIG AS DOC_DOC"
                'auxWhere &= " AND DOC_DOC.orden <=10 AND DOC_DOC.cod IN(SELECT doccod FROM DOC_DOCSGN WHERE empcod=" & Session("empcod") & ")"
                auxWhere &= " AND DOC_DOC.cod IN(SELECT doccod FROM DOC_DOCSGN WHERE empcod=" & pEmpCod & ")"
                auxGroups = "NULL,NULL,"
                'En el modo obligatorio debe poder editar, aprobar, leer.
            Case enumView.coVigente_PorTipoProceso   'Vigentes por TIPO DE PROCESO v2
                auxFrom = " FROM DOC_DOCVIG AS DOC_DOC"
                auxGroups = "DOC_DOC.docsupcod,(DOC_DOC.doctipcod * 500 + DOC_DOC.procod),"
            Case enumView.coMetro_PorProceso, enumView.coMetro_PorTipoProceso
                auxFrom = " FROM DOC_DOCVIG AS DOC_DOC"
                auxGroups = "DOC_DOC.docsupcod,(DOC_DOC.doctipcod * 500 + DOC_DOC.procod),"
            Case enumView.coVigente_PorUnidad  'Vigentes por unidad
                'auxSelect &= ",DOC_DOC.undcod as hierarchycod1"
                auxFrom = " FROM DOC_DOCVIG AS DOC_DOC"
                auxGroups = "DOC_DOC.docsupcod,(DOC_DOC.doctipcod * 500 + DOC_DOC.undcod),"
            Case enumView.coVigente_PorTipoDoc   'Vigentes  por tipos de documentos
                auxFrom = " FROM DOC_DOCVIG AS DOC_DOC"
                auxGroups = "DOC_DOC.docsupcod,(DOC_DOC.doctipcod),"
            Case enumView.coBiblioteca_PorTipoproceso   'bIBLIOTECA  por tipo de proceso v2
                auxFrom = " FROM DOC_DOC"
                auxGroups = "DOC_DOC.docsupcod,(DOC_DOC.doctipcod * 500 + DOC_DOC.procod),"
            Case enumView.coBiblioteca_PorUnidad  'bIBLIOTECA por unidad
                auxFrom = " FROM DOC_DOC"
                auxGroups = "DOC_DOC.docsupcod,(DOC_DOC.doctipcod * 500 + DOC_DOC.undcod),"
            Case enumView.coBiblioteca_PorTipoDoc   'bIBLIOTECA  por tipos de documentos
                auxFrom = " FROM DOC_DOC AS DOC_DOC"
                auxGroups = "DOC_DOC.docsupcod,(DOC_DOC.doctipcod),"
            Case enumView.coVigente_PorTipoProcesoyProceso
                auxFrom = " FROM DOC_DOCVIG AS DOC_DOC"
                'auxGroups = "DOC_DOC.docsupcod,DOC_DOC.procod,"
                auxGroups = "DOC_DOC.docsupcod,(DOC_DOC.doctipcod * 500 + DOC_DOC.procod),"
            Case enumView.coBiblioteca_PorTipoProcesoyProceso
                auxFrom = " FROM DOC_DOC AS DOC_DOC"
                auxGroups = "DOC_DOC.docsupcod,DOC_DOC.procod,"
            Case enumView.coVigente_PorSistema
                auxFrom = " FROM DOC_DOCVIG AS DOC_DOC"
                auxGroups = "DOC_DOC.docsupcod,DOC_DOC.siscod,"
            Case enumView.coBiblioteca_PorSistema
                auxFrom = " FROM DOC_DOC AS DOC_DOC"
                auxGroups = "DOC_DOC.docsupcod,DOC_DOC.siscod,"

        End Select
        Dim auxMisAcciones As Boolean = False

        If auxPanelValues IsNot Nothing Then
            Dim auxValue As Object
            auxValue = auxPanelValues.gValue_Get("dsc", "")
            If auxValue <> "" Then
                auxFilter = True
                Dim auxFilterDsc As String = auxValue.ToUpper.Trim
                auxWhere &= " AND ("
                'If IsNumeric(auxFilterDsc) Then
                'auxWhere &= " OR (DOC_DOC.nro=" & auxFilterDsc & ")"
                'Else
                auxFilterDsc = Replace(auxFilterDsc, " OR ", "%' OR {#FIELDNAME#} LIKE '%")
                auxFilterDsc = Replace(auxFilterDsc, " | ", "%' OR {#FIELDNAME#} LIKE '%")
                auxFilterDsc = Replace(auxFilterDsc, " AND ", "%' AND {#FIELDNAME#} LIKE '%")
                auxFilterDsc = Replace(auxFilterDsc, " + ", "%' AND {#FIELDNAME#} LIKE '%")
                auxWhere &= " ((DOC_DOC.dsc + ' ' + ISNULL(DOC_DOC.cuerpo,'') + identificador) LIKE '%" & Replace(auxFilterDsc, "{#FIELDNAME#}", "(DOC_DOC.dsc + ' ' + ISNULL(DOC_DOC.cuerpo,''))") & "%')"
                'End If
                auxWhere &= ")"
                '& " OR (DOC_DOC.cuerpo LIKE '%" & Replace(auxFilterDsc, "{#FIELDNAME#}", "DOC_DOC.cuerpo") & "%'))"
            End If
            auxValue = auxPanelValues.gValue_Get("nro", "")
            If auxValue <> "" Then
                auxFilter = True
                Dim auxFilterDsc As String = auxValue.Trim
                auxWhere &= " AND ("
                If auxFilterDsc <> "" Then
                    If IsNumeric(auxFilterDsc) Then
                        auxWhere &= " (DOC_DOC.nro=" & auxFilterDsc & ")"
                    Else
                        auxWhere &= " (DOC_DOC.identificador LIKE '%" & auxFilterDsc & "%')"
                    End If
                End If
                auxWhere &= ")"
            End If
            auxValue = auxPanelValues.gValue_Get("apacod", -1)
            If Val(auxValue) > 0 Then
                auxFilter = True
                auxWhere &= " AND (DOC_DOC.procod IN (SELECT cod FROM DOC_PRO WHERE apacod= " & auxValue & "))"
            End If
            auxValue = auxPanelValues.gValue_Get("clacod", -1)
            If Val(auxValue) > 0 Then
                auxFilter = True
                auxWhere &= " AND (DOC_DOC.clacod = " & auxValue & ")"
            End If
            auxValue = auxPanelValues.gValue_Get("doctipcod", -1)
            If Val(auxValue) > 0 Then
                auxFilter = True
                auxWhere &= " AND (DOC_DOC.doctipcod = " & auxValue & ")"
            End If

            If gView_IsVigente(pView) = False Then
                auxValue = auxPanelValues.gValue_Get("wfwstp", -1)
                If Val(auxValue) > 0 Then
                    auxFilter = True
                    auxWhere &= " AND (DOC_DOC.wfwstatus = " & auxValue & ")"
                End If
            End If

            auxValue = auxPanelValues.gValue_Get("misacciones", auxPanelValues.gValue_Get("misacciones_default"))
            If Val(auxValue) > 0 Then
                auxFilter = True
                auxWhere &= " AND (DOC_DOC.cod IN (SELECT doccod FROM DOC_DOCSGN WHERE empcod=" & pEmpCod & "))"
                auxMisAcciones = True
            End If

            auxValue = auxPanelValues.gValue_Get("und")
            If auxValue Is Nothing Then
                'pClass.gLoginPreference_SetValue(enumPrefType.coDocumentossearch, "UND", "")
            Else
                Dim auxUndListSelected As List(Of clsNode) = auxValue
                auxFilter = True
                Dim auxUndCod As Integer
                Dim auxUndList As New List(Of Integer)
                Dim auxUndSelectionList As New List(Of Integer)
                For Each auxNode As clsNode In auxUndListSelected
                    auxUndCod = -1
                    Select Case auxNode.Type
                        Case enumEntities.coEntityUND
                            auxUndCod = auxNode.Cod
                    End Select
                    If auxUndCod <> -1 And auxUndList.IndexOf(auxUndCod) = -1 Then
                        auxUndList.AddRange(pClass.gEntity_UND_GetChilds(auxUndCod))
                        auxUndSelectionList.Add(auxUndCod)
                    End If
                Next

                'pClass.gLoginPreference_SetValue(enumPrefType.coDocumentossearch, "UND", pClass.Conn.gFieldDB_GetString(auxUndSelectionList))
                auxWhere &= " AND (undcod IN(" _
                    & pClass.Conn.gFieldDB_GetString(auxUndList) & "))"
            End If

            auxValue = auxPanelValues.gValue_Get("doctarget")
            If auxValue IsNot Nothing Then
                Dim auxListSelected As List(Of clsNode) = auxValue
                auxFilter = True
                Dim auxDocTargetWhere As String = ""
                Dim auxSidList As New List(Of Integer)
                Dim auxSidcod As Integer
                Dim auxWhereRel As String = ""
                For Each auxNode As clsNode In auxListSelected
                    auxSidcod = -1
                    Select Case auxNode.Type
                        Case enumEntities.coEntityDOC_EQU
                            auxSidcod = pClass.Sec.gGroup_GetSidCod(pClass.Conn.gField_GetInt(hrcEntityDT_DOC_EQU_FindByKey(auxNode.Cod)("miembrosgrpcod"), -1))
                        Case enumEntities.coEntityUND
                            auxSidcod = pClass.Sec.gGroup_GetSidCod(pClass.Conn.gField_GetInt(hrcEntityDT_UND_FindByKey(auxNode.Cod)("miembrosgrpcod"), -1))
                        Case enumEntities.coEntityEMP
                            auxSidcod = pClass.Sec.gLogin_GetSidCod(pClass.Conn.gField_GetInt(hrcEntityDT_EMP_FindByKey(auxNode.Cod)("seccod"), -1))
                    End Select
                    If auxSidcod <> -1 And auxSidList.IndexOf(auxSidcod) = -1 Then
                        auxSidList.Add(auxSidcod)
                    End If
                Next
                If auxSidList.Count <> 0 Then
                    Dim auxAccTypeListDocTarget As New List(Of Integer)
                    auxAccTypeListDocTarget.Add(enumAccessType.coSYSGlobalLeer)
                    auxAccTypeListDocTarget.Add(enumAccessType.coSYSConfirmarlectura)
                    auxAccTypeListDocTarget.Add(enumAccessType.coDOCDOCVIGDocumentosvigentesVer)
                    auxWhere &= " AND (qsidcod IN(" _
                        & pClass.Sec.gSID_GetQueryAccessFromAcctype(auxSidList, auxAccTypeListDocTarget) _
                                                                & "))"
                End If
            End If


        End If

        If pSysParamInboxDetailedEnabled Then
            auxFields &= ",DOC_DOC.obs "
        End If
        Select Case pView
            Case enumView.coVigente_PorSistema, enumView.coVigente_PorTipoDoc, enumView.coVigente_PorTipoProceso, enumView.coVigente_PorTipoProcesoyProceso, enumView.coVigente_PorUnidad _
                , enumView.coMetro_PorProceso, enumView.coMetro_PorTipoProceso, enumView.coMetro_Inicial
                'Vigentes
                auxFields &= ",(SELECT COUNT(*) FROM DOC_DOCSGN LEFT JOIN DOC_DOCLOG ON DOC_DOCSGN.doclogcod = DOC_DOCLOG.cod " _
                        & "  WHERE DOC_DOCLOG.wfwstepnext=" & enumWorkflowStep.coWFWSTPDOC_DOCDocumentovigente _
                        & " AND DOC_DOCSGN.doccod=DOC_DOC.cod " _
                        & " AND DOC_DOCSGN.empcod=" & pEmpCod & ") as docvig_read"
                auxWhere &= " AND qsidcod IN(" & pClass.Sec.gSID_GetQueryAccessFromAcctype(-1, enumAccessType.coSYSGlobalLeer _
                                                                                             & "," & enumAccessType.coSYSGlobalModificar _
                                                                                             & "," & enumAccessType.coSYSGlobalCambiarpermisos _
                                                                                             & "," & enumAccessType.coDOCDOCVIGDocumentosvigentesVer _
                                                                                             & "," & enumAccessType.coSYSConfirmarlectura) & ")"
            Case enumView.coObligado
            Case Else

                'Biblioteca
                auxFields &= ",(SELECT COUNT(*) FROM DOC_DOCSGN LEFT JOIN DOC_DOCLOG ON DOC_DOCSGN.doclogcod = DOC_DOCLOG.cod " _
                      & "  WHERE DOC_DOCLOG.wfwstepnext=" & enumWorkflowStep.coWFWSTPDOC_DOCDocumentovigente _
                      & " AND DOC_DOCSGN.doccod=DOC_DOC.cod " _
                      & " AND DOC_DOCSGN.empcod=" & pEmpCod & ") as docvig_read"
                auxWhere &= " AND (DOC_DOC.baja {#ISNULL#} OR DOC_DOC.baja={#FALSE#})"
                Select Case pView
                    Case enumView.coBiblioteca_Edicion
                        auxWhere &= " AND DOC_DOC.wfwstatus IN (" & enumWorkflowStep.coWFWSTPDOC_DOCEdicion _
                            & "," & enumWorkflowStep.coWFWSTPDOC_DOCEdicionOK _
                            & "," & enumWorkflowStep.coWFWSTPDOC_DOCnuevaversion _
                            & "," & enumWorkflowStep.coWFWSTPDOC_DOCCreacion _
                            & ")"
                        If auxMisAcciones Then
                            auxWhere &= " AND DOC_DOC.cod IN " _
                                & "(SELECT DOC_DOCSGN.doccod " _
                                & " FROM DOC_DOCSGN " _
                                & " LEFT JOIN DOC_DOCLOG ON DOC_DOCSGN.doclogcod = DOC_DOCLOG.cod " _
                                & " WHERE DOC_DOCLOG.wfwstepnext IN (" & enumWorkflowStep.coWFWSTPDOC_DOCEdicion _
                                & "," & enumWorkflowStep.coWFWSTPDOC_DOCEdicionOK _
                                & "," & enumWorkflowStep.coWFWSTPDOC_DOCnuevaversion _
                                & "," & enumWorkflowStep.coWFWSTPDOC_DOCCreacion _
                                & ")" _
                                & " AND DOC_DOCSGN.doccod=DOC_DOC.cod " _
                                & " AND DOC_DOCSGN.empcod=" & pEmpCod & ") "
                        End If
                    Case enumView.coBiblioteca_Revision
                        auxWhere &= " AND DOC_DOC.wfwstatus IN (" & enumWorkflowStep.coWFWSTPDOC_DOCrevisionOK & "," & enumWorkflowStep.coWFWSTPDOC_DOCRevisionSSG & ")"
                        If auxMisAcciones Then
                            auxWhere &= " AND DOC_DOC.cod IN " _
                               & "(SELECT DOC_DOCSGN.doccod " _
                               & " FROM DOC_DOCSGN " _
                               & " LEFT JOIN DOC_DOCLOG ON DOC_DOCSGN.doclogcod = DOC_DOCLOG.cod " _
                               & " WHERE DOC_DOCLOG.wfwstepnext=" & enumWorkflowStep.coWFWSTPDOC_DOCRevisionSSG _
                               & " AND DOC_DOCSGN.doccod=DOC_DOC.cod " _
                               & " AND DOC_DOCSGN.empcod=" & pEmpCod & ") "
                        End If
                    Case enumView.coBiblioteca_Aprobacion
                        auxWhere &= " AND DOC_DOC.wfwstatus IN (" & enumWorkflowStep.coWFWSTPDOC_DOCAprobacion & "," & enumWorkflowStep.coWFWSTPDOC_DOCaprobacionOK & ")"
                        If auxMisAcciones Then
                            auxWhere &= " AND DOC_DOC.cod IN " _
                               & "(SELECT DOC_DOCSGN.doccod " _
                               & " FROM DOC_DOCSGN " _
                               & " LEFT JOIN DOC_DOCLOG ON DOC_DOCSGN.doclogcod = DOC_DOCLOG.cod " _
                               & " WHERE DOC_DOCLOG.wfwstepnext=" & enumWorkflowStep.coWFWSTPDOC_DOCAprobacion _
                               & " AND DOC_DOCSGN.doccod=DOC_DOC.cod " _
                               & " AND DOC_DOCSGN.empcod=" & pEmpCod & ") "
                        End If
                    Case enumView.coBiblioteca_Publicacion
                        auxWhere &= " AND DOC_DOC.wfwstatus IN (" & enumWorkflowStep.coWFWSTPDOC_DOCPublicacion & "," & enumWorkflowStep.coWFWSTPDOC_DOCpublicacionOK & ")"
                        If auxMisAcciones Then
                            auxWhere &= " AND DOC_DOC.cod IN " _
                               & "(SELECT DOC_DOCSGN.doccod " _
                               & " FROM DOC_DOCSGN " _
                               & " LEFT JOIN DOC_DOCLOG ON DOC_DOCSGN.doclogcod = DOC_DOCLOG.cod " _
                               & " WHERE DOC_DOCLOG.wfwstepnext=" & enumWorkflowStep.coWFWSTPDOC_DOCPublicacion _
                               & " AND DOC_DOCSGN.doccod=DOC_DOC.cod " _
                               & " AND DOC_DOCSGN.empcod=" & pEmpCod & ") "
                        End If
                    Case enumView.coBiblioteca_Cancelacion
                        auxWhere &= " AND DOC_DOC.wfwstatus IN (" & enumWorkflowStep.coWFWSTPDOC_DOCCancelacion & ")"
                        If auxMisAcciones Then
                            auxWhere &= " AND DOC_DOC.cod IN " _
                                & "(SELECT DOC_DOCSGN.doccod " _
                                & " FROM DOC_DOCSGN " _
                                & " LEFT JOIN DOC_DOCLOG ON DOC_DOCSGN.doclogcod = DOC_DOCLOG.cod " _
                                & " WHERE DOC_DOCLOG.wfwstepnext=" & enumWorkflowStep.coWFWSTPDOC_DOCCancelacion _
                                & " AND DOC_DOCSGN.doccod=DOC_DOC.cod " _
                                & " AND DOC_DOCSGN.empcod=" & pEmpCod & ") "
                        End If
                    Case enumView.coBiblioteca_NuevosDoc
                        auxWhere &= " AND DOC_DOC.wfwstatus=" & enumWorkflowStep.coWFWSTPDOC_DOCSolicitudnuevodocumento
                        If auxMisAcciones Then
                            auxWhere &= " AND DOC_DOC.cod IN " _
                            & "(SELECT DOC_DOCSGN.doccod " _
                            & " FROM DOC_DOCSGN " _
                            & " LEFT JOIN DOC_DOCLOG ON DOC_DOCSGN.doclogcod = DOC_DOCLOG.cod " _
                            & " WHERE DOC_DOCLOG.wfwstepnext=" & enumWorkflowStep.coWFWSTPDOC_DOCSolicitudnuevodocumento _
                            & " AND DOC_DOCSGN.doccod=DOC_DOC.cod " _
                            & " AND DOC_DOCSGN.empcod=" & pEmpCod & ") "
                        End If

                    Case enumView.coBiblioteca_NuevasVersiones
                        auxWhere &= " AND DOC_DOC.wfwstatus IN (" & enumWorkflowStep.coWFWSTPDOC_DOCSolicitudnuevaversion & "," & enumWorkflowStep.coWFWSTPDOC_DOCSolicitudautomaticanuevaversion & ")"
                        If auxMisAcciones Then
                            auxWhere &= " AND DOC_DOC.cod IN " _
                               & "(SELECT DOC_DOCSGN.doccod " _
                               & " FROM DOC_DOCSGN " _
                               & " LEFT JOIN DOC_DOCLOG ON DOC_DOCSGN.doclogcod = DOC_DOCLOG.cod " _
                               & " WHERE DOC_DOCLOG.wfwstepnext IN (" & enumWorkflowStep.coWFWSTPDOC_DOCSolicitudnuevaversion & "," & enumWorkflowStep.coWFWSTPDOC_DOCSolicitudautomaticanuevaversion & ")" _
                               & " AND DOC_DOCSGN.doccod=DOC_DOC.cod " _
                               & " AND DOC_DOCSGN.empcod=" & pEmpCod & ") "
                        End If
                    Case enumView.coBiblioteca_Lectura
                        auxWhere &= " AND DOC_DOC.cod IN " _
                           & "(SELECT DOC_DOCSGN.doccod " _
                           & " FROM DOC_DOCSGN " _
                           & " LEFT JOIN DOC_DOCLOG ON DOC_DOCSGN.doclogcod = DOC_DOCLOG.cod " _
                           & " WHERE DOC_DOCLOG.wfwstepnext=" & enumWorkflowStep.coWFWSTPDOC_DOCDocumentovigente _
                           & " AND DOC_DOCSGN.doccod=DOC_DOC.cod " _
                           & " AND DOC_DOCSGN.empcod=" & pEmpCod & ") "
                    Case enumView.coBiblioteca_Creacion
                        auxWhere &= " AND (wfwstatus= " & enumWorkflowStep.coWFWSTPDOC_DOCCreacion & " OR wfwstatus {#ISNULL#})"
                    Case enumView.coBiblioteca_Eliminacion
                        auxWhere &= " AND DOC_DOC.wfwstatus IN (" & enumWorkflowStep.coWFWSTPDOC_DOCSolicitudeliminacion & ")"
                        auxWhere &= " AND DOC_DOC.cod IN " _
                           & "(SELECT DOC_DOCSGN.doccod " _
                           & " FROM DOC_DOCSGN " _
                           & " LEFT JOIN DOC_DOCLOG ON DOC_DOCSGN.doclogcod = DOC_DOCLOG.cod " _
                           & " WHERE DOC_DOCLOG.wfwstepnext=" & enumWorkflowStep.coWFWSTPDOC_DOCSolicitudeliminacion _
                           & " AND DOC_DOCSGN.doccod=DOC_DOC.cod " _
                           & " AND DOC_DOCSGN.empcod=" & pEmpCod & ") "
                End Select
                If pIsAdmin Then
                Else
                    auxWhere &= " AND qsidcod IN(" & pClass.Sec.gSID_GetQueryAccessFromAcctype(-1, enumAccessType.coSYSGlobalCambiarestado _
                                                                             & "," & enumAccessType.coSYSGlobalModificar _
                                                                             & "," & enumAccessType.coSYSGlobalCambiarpermisos _
                                                                             & "," & enumAccessType.coSYSConfirmarlectura _
                                                                             & "," & enumAccessType.coSYSCreador _
                                                                             & "," & enumAccessType.coSYSImprimircopiascontroladas _
                                                                             & "," & enumAccessType.coDOCDOCVIGDocumentosvigentesVer _
                                                                             & "," & enumAccessType.coSYSImprimircopiasnocontroladas) & ")"

                End If
        End Select

        auxSelect1 = "SELECT " & auxCodValue & " as q_cod,(DOC_DOC.dsc ) as q_dsc," & auxGroups & enumEntities.coEntityDOC_DOC & " as q_type,DOC_DOC.cod" _
                & ",CASE WHEN DOC_DOC.nro >0 THEN DOC_DOC.nro ELSE '' END as nro,DOC_DOC.identificador" _
                & ",(SELECT TOP 1 DOC_DOCVIG.version FROM DOC_DOCVIG WHERE cod=DOC_DOC.cod) as version" _
                & ",(SELECT TOP 1 DOC_DOCVIG.fecha FROM DOC_DOCVIG WHERE cod=DOC_DOC.cod) as fechavigente" _
                & ",(SELECT TOP 1 Q_WFWSTP.wfwstpdsc FROM Q_WFWSTP WHERE Q_WFWSTP.wfwstpcod = DOC_DOC.wfwstatus) as wfwstpdsc," _
                & "(SELECT TOP 1 DOC_DOCLOG.obs FROM DOC_DOCLOG WHERE DOC_DOCLOG.doccod=DOC_DOC.cod AND wfwstpprev <> wfwstepnext ORDER BY DOC_DOCLOG.cod DESC) as doclogobs" _
                & auxFields _
                & auxFrom _
                & " WHERE DOC_DOC.cod > 0" _
                & auxWhere _
                & auxOrderBy
        If pView <> enumView.coObligado Then
            'No almacena el modo obligatorio
            'pClass.gLoginPreference_SetValue(enumPrefType.coDocumentossearch, "view", pView)
        End If
        'pClass.gLoginPreference_Save(enumPrefType.coDocumentossearch)
        Return auxSelect1
    End Function


    Private Function gFormDocuments_Get(ByVal pView As enumView, _
                                               ByVal pClearSearch As Boolean, _
                                               ByVal pMIsAccionesvisible As Boolean, _
                                               ByVal pMIsAccionesOn As Short, _
                                               ByVal pIncludeChildsVisible As Boolean, _
                                               ByVal pPermFormNew As Boolean, _
                                               ByVal pEmpCod As Integer, _
                                               ByVal pIsAdmin As Boolean, _
                                               ByVal pProCod As Integer, _
                                               ByVal pApaCod As Integer) As clshrcBagValues

        Dim auxHtml As New clsHrcCodeHTML
        auxHtml.DateFormatEnabled = False
        Dim auxClass As New clsCusimDOC
        Dim auxHrcContext As clsHrcJSContext
        Dim auxClientCon As New imClientConnection

        If Environment.MachineName = "A16WIN8" Then
            'auxHrcContext = auxClientCon.gObjectTmp_Download("test")
            If auxHrcContext Is Nothing Then
                auxHrcContext = New clsHrcJSContext("hrccontext", Session("conn"), Session("security"), Session("hrcAlerts"))
                auxClientCon.gObjectTmp_Upload(auxHrcContext, "test")
            End If
        Else
            auxHrcContext = Session("hrcContext")
        End If
        Dim auxInboxDetailedEnabled As Boolean = False
        If Val(auxClass.gSystem_GetParameterByID(coSysParamInboxDetailedEnabled)) > 0 Then
            auxInboxDetailedEnabled = True
        End If
        Dim auxTitle As String = ""
        Dim auxIcon As String = ""
        Dim auxFilterEnabled As Boolean = False
        Select Case pView
            Case enumView.coBiblioteca_PorSistema    '0=BIBLIOTECA sin anidación
                auxTitle = "Biblioteca de documentos"
                auxIcon = "imagenes/biblioteca_documentos.png"
                auxFilterEnabled = True
            Case enumView.coObligado  'MODO OBLIGADO
                auxTitle = "Documentos que debe leer"
                auxIcon = "imagenes/evError.png"
                auxFilterEnabled = True
            Case enumView.coVigente_PorTipoProceso   'Vigentes por TIPO DE PROCESO v2
                auxTitle = "Documentos vigentes (por tipo de proceso)"
                auxTitle = "<a href=#>" _
                & "<img src=""imagenes/doc_vigentes.png"" class=hrcthemeimage width=24px > " _
                & auxTitle _
                & "</a>"
                auxFilterEnabled = True
            Case enumView.coVigente_PorUnidad  'Vigentes por unidad
                'auxSelect &= ",DOC_DOC.undcod as hierarchycod1"
                auxTitle = "Documentos vigentes (por unidades)"
                auxTitle = "<a href=#>" _
               & "<img src=""imagenes/doc_vigentes.png"" class=hrcthemeimage width=24px > " _
               & auxTitle _
               & "</a>"
                auxFilterEnabled = True
            Case enumView.coVigente_PorTipoDoc   'Vigentes  por tipos de documentos
                auxTitle = "Documentos vigentes (por tipo de documentos)"
                auxTitle = "<a href=#>" _
               & "<img src=""imagenes/doc_vigentes.png"" class=hrcthemeimage width=24px > " _
               & auxTitle _
               & "</a>"
                auxFilterEnabled = True
            Case enumView.coBiblioteca_PorTipoproceso   'bIBLIOTECA  por tipo de proceso v2
                auxTitle = "Biblioteca de documentos (por tipo de proceso)"
                auxIcon = "imagenes/biblioteca_documentos.png"
                auxFilterEnabled = True
            Case enumView.coBiblioteca_PorUnidad  'bIBLIOTECA por unidad
                auxTitle = "Biblioteca de documentos (por unidades)"
                auxIcon = "imagenes/biblioteca_documentos.png"
                auxFilterEnabled = True
            Case enumView.coBiblioteca_PorTipoDoc  'bIBLIOTECA  por tipos de documentos
                auxTitle = "Biblioteca de documentos (por tipo de documentos)"
                auxIcon = "imagenes/biblioteca_documentos.png"
                auxFilterEnabled = True
            Case enumView.coVigente_PorTipoProcesoyProceso
                auxTitle = "Documentos vigentes"
                auxTitle = "<a href=#>" _
               & "<img src=""imagenes/doc_vigentes.png"" class=hrcthemeimage width=24px > " _
               & auxTitle _
               & "</a>"
                auxFilterEnabled = True
            Case enumView.coBiblioteca_PorTipoProcesoyProceso
                auxTitle = "Biblioteca de documentos"
                auxIcon = "imagenes/biblioteca_documentos.png"
                auxFilterEnabled = True
            Case enumView.coVigente_PorSistema
                auxTitle = "Documentos vigentes (por sistema)"
                auxTitle = "<a href=#>" _
               & "<img src=""imagenes/doc_vigentes.png"" class=hrcthemeimage width=24px > " _
               & auxTitle _
               & "</a>"
                auxFilterEnabled = True
            Case enumView.coBiblioteca_PorSistema
                auxTitle = "Biblioteca de documentos (por sistema)"
                auxIcon = "imagenes/biblioteca_documentos.png"
                auxFilterEnabled = True
            Case enumView.coBiblioteca_NuevosDoc
                auxTitle = "Solicitud nuevos documentos"
                auxIcon = "imagenes/biblioteca_documentos.png"
                auxFilterEnabled = True
            Case enumView.coBiblioteca_NuevasVersiones
                auxTitle = "Nuevas versiones"
                auxIcon = "imagenes/biblioteca_documentos.png"
                auxFilterEnabled = True
            Case enumView.coBiblioteca_Edicion
                auxTitle = hrcEntityDT_Q_WFWSTP_FindByKey(enumWorkflowStep.coWFWSTPDOC_DOCEdicion)("wfwstpdsc")
                auxIcon = "imagenes/biblioteca_documentos.png"
                auxFilterEnabled = True
            Case enumView.coBiblioteca_Revision
                auxTitle = hrcEntityDT_Q_WFWSTP_FindByKey(enumWorkflowStep.coWFWSTPDOC_DOCRevisionSSG)("wfwstpdsc")
                auxIcon = "imagenes/biblioteca_documentos.png"
                auxFilterEnabled = True
            Case enumView.coBiblioteca_Aprobacion
                auxTitle = hrcEntityDT_Q_WFWSTP_FindByKey(enumWorkflowStep.coWFWSTPDOC_DOCAprobacion)("wfwstpdsc")
                auxIcon = "imagenes/biblioteca_documentos.png"
                auxFilterEnabled = True
            Case enumView.coBiblioteca_Publicacion
                auxTitle = hrcEntityDT_Q_WFWSTP_FindByKey(enumWorkflowStep.coWFWSTPDOC_DOCPublicacion)("wfwstpdsc")
                auxIcon = "imagenes/biblioteca_documentos.png"
                auxFilterEnabled = True
            Case enumView.coBiblioteca_Cancelacion
                auxTitle = hrcEntityDT_Q_WFWSTP_FindByKey(enumWorkflowStep.coWFWSTPDOC_DOCCancelacion)("wfwstpdsc")
                auxIcon = "imagenes/biblioteca_documentos.png"
                auxFilterEnabled = True
            Case enumView.coBiblioteca_Lectura
                auxTitle = "Lecturas pendientes"
                auxIcon = "imagenes/doc_vigentes.png"
                auxFilterEnabled = True
            Case enumView.coBiblioteca_Creacion
                auxTitle = "Borradores"
                auxIcon = "imagenes/biblioteca_documentos.png"
                auxFilterEnabled = True
            Case enumView.coBiblioteca_Eliminacion
                auxTitle = hrcEntityDT_Q_WFWSTP_FindByKey(enumWorkflowStep.coWFWSTPDOC_DOCEliminaciontotal)("wfwstpdsc")
                auxIcon = "imagenes/biblioteca_documentos.png"
                auxFilterEnabled = True
            Case enumView.coMetro_PorProceso
                Dim auxDTPro As DataTable = auxClass.Conn.gConn_Query("SELECT DOC_PRO.dsc as pro_dsc " _
                                                                    & ",DOC_PRO.apacod" _
                                                                    & " FROM DOC_PRO" _
                                                                    & " WHERE DOC_PRO.cod=" & pProCod)
                If auxDTPro.Rows.Count <> 0 Then
                    Dim auxRowAPA = hrcEntityDT_DOC_APA_FindByKey(auxDTPro.Rows(0)("apacod"))
                    If auxRowAPA IsNot Nothing Then
                        auxTitle = "<a href=cfrmdocumentos.aspx?_view_=" & enumView.coVigente_PorTipoProcesoyProceso & " >" _
                    & "<img src=""imagenes/doc_vigentes.png"" class=hrcthemeimage width=24px > " _
                    & "Vigentes" _
                    & "</a>" _
                    & "</li><li>" _
                    & "<a href=cfrmdocumentos.aspx?_view_=" & enumView.coMetro_PorTipoProceso & "&apa=" & auxDTPro.Rows(0)("apacod") & " >" _
                    & "<img src=""imagenes/icon" & Format(CInt(enumEntities.coEntityDOC_APA), "00000000") & ".png"" class=hrcthemeimage width=24px > " _
                    & auxRowAPA("dsc") _
                    & "</a>" _
                    & "</li><li>" _
                    & "<a href=#>" _
                    & "<img src=""imagenes/icon" & Format(CInt(enumEntities.coEntityDOC_PRO), "00000000") & ".png"" class=hrcthemeimage width=24px > " _
                    & auxDTPro.Rows(0)("pro_dsc") _
                     & "</a>"
                    End If
                End If

                auxFilterEnabled = True
            Case enumView.coMetro_PorTipoProceso
                Dim auxRowAPA = hrcEntityDT_DOC_APA_FindByKey(pApaCod)
                If auxRowAPA IsNot Nothing Then
                    auxTitle = "<a href=cfrmdocumentos.aspx?_view_=" & enumView.coVigente_PorTipoProcesoyProceso & " >" _
                    & "<img src=""imagenes/doc_vigentes.png"" class=hrcthemeimage width=24px > " _
                    & "Vigentes" _
                    & "</a>" _
                    & "</li><li>" _
                    & "<a href=#>" _
                    & "<img src=""imagenes/icon" & Format(CInt(enumEntities.coEntityDOC_APA), "00000000") & ".png"" class=hrcthemeimage width=24px > " _
                    & auxRowAPA("dsc") _
                    & "</a>"
                End If

                auxFilterEnabled = True
        End Select


        Dim auxPrincipalPanel As clsHrcJSPanel = Nothing
        If hdnPrincipalForm.Value = "" Then
            auxPrincipalPanel = New clsHrcJSPanel("principal_form", "", "", clsHrcJSPanel.enumPanelMode.coNoButton)
            'hdnPrincipalForm.Value = auxClientCon.gObjectTmp_Upload(hdnPrincipalForm.Value)
            auxPrincipalPanel.ServerStateMantain = True
            hdnPrincipalForm.Value = auxHrcContext.gObjectTmp_UploadControl(auxPrincipalPanel)
            'dejar espacio a la derecha por el scroll
            Dim auxHTMLcontent As String = ""
            If auxFilterEnabled Then
                auxHTMLcontent &= "" _
                            & "<div class=""form-title"" style=""text-align:left;padding-left:20px;padding-right:20px"">" _
                            & "{#PANEL.TITLE#}" _
                            & "</div>" _
                            & "<div style="";margin-left:20px;margin-right:25px;"">" _
                            & "<div class=""ui-tabs ui-widget ui-widget-content ui-corner-all"" >" _
                            & "<div class=""ui-tabs ui-widget-header ui-corner-top"" style=""border:0px;width:100%;height:20px"">Filtros de búsqueda</div>" _
                            & "<table class="""" width=""100%"" cellpadding=""2px"" cellspacing=""0px"" >" _
                            & "<tr>" _
                                & "<td style=""width:15%"">Titulo y contenido:</td><td>{#CONTROLS.TXTDSC#}</td>" _
                                & "<td style=""width:15%"">Clasificación:</td><td>{#CONTROLS.CMBCLA#}</td>" _
                            & "</tr>" _
                            & "<tr>" _
                                & "<td>Número o identificador:</td><td>{#CONTROLS.TXTNRO#}</td>" _
                                & "<td>Tipo de proceso:</td><td>{#CONTROLS.CMBAPA#}</td>" _
                            & "</tr>" _
                            & "<tr>" _
                                & "<td>Tipo de documento:</td><td>{#CONTROLS.CMBDOCTIPCOD#}</td>" _
                                & "<td></td><td></td>" _
                            & "</tr>" _
                            & "<tr>" _
                                & "<td>Filtrar donde es lector o visualizador:</td><td>{#CONTROLS.FILTER_DOCTARGET#}</td>" _
                                & "<td>Específico a:</td><td>{#CONTROLS.FILTER_UND#}</td>" _
                            & "</tr>" _
                            & "<tr>" _
                                & "<td>{#CONTROLS.CMBWFWSTP.TITLE#}</td><td>{#CONTROLS.CMBWFWSTP#}</td>" _
                                & "<td id=""row_misaccionespendientes"">{#CONTROLS.CHKMISACCIONES.TITLE#}</td>" _
                                & "<td>{#CONTROLS.CHKMISACCIONES#}" _
                                & "</td>" _
                            & "</tr>" _
                            & "<tr>" _
                            & "<td>{#CONTROLS.CHKINCLUDECHILDS.TITLE#}</td><td>{#CONTROLS.CHKINCLUDECHILDS#}</td><td></td>" _
                            & "</tr>" _
                            & "</table>" _
                            & "<div class=""ui-tabs ui-widget-header ui-corner-bottom"" style=""width:100%"">{#PANEL.BUTTONS#}</div>" _
                            & "</div>" _
                            & "{#CONTROLS.GRDDOC#}" _
                            & "</div>"
            Else
                auxHTMLcontent &= "" _
                     & "<div class=""form-title"" style=""text-align:left;padding-left:20px;padding-right:20px"">" _
                            & "{#PANEL.TITLE#}" _
                            & "</div>" _
                            & "</div>" _
                            & "{#CONTROLS.GRDDOC#}" _
                            & "</div>"
            End If
            auxPrincipalPanel.gHTML_SetTemplate(auxHTMLcontent, "", "", "", auxHrcContext.ButtonCSS)
            AddHandler auxPrincipalPanel.EventCommandHandler, AddressOf gDocumentsSearchPanel_CommandHandler
        Else
            '  auxPrincipalPanel = auxClientCon.gObjectTmp_Download(hdnPrincipalForm.Value)
            auxPrincipalPanel = auxHrcContext.gObjectTmp_Download(hdnPrincipalForm.Value)
        End If

        'Filtros
        auxPrincipalPanel.BagValues.gValue_Add("view", CInt(pView))
        auxPrincipalPanel.BagValues.gValue_Add("InboxDetailedEnabled", auxInboxDetailedEnabled)
        auxPrincipalPanel.BagValues.gValue_Add("empcod", pEmpCod)
        auxPrincipalPanel.BagValues.gValue_Add("isadmin", pIsAdmin)
        auxPrincipalPanel.BagValues.gValue_Add("procod", pProCod)
        auxPrincipalPanel.BagValues.gValue_Add("apacod", pApaCod)
        Dim auxValue As Integer

        Dim auxClearScript As String = ""
        auxPrincipalPanel.Title = ""

        If auxIcon <> "" Then
            auxIcon = "<img src=""" & auxHrcContext.RootWebUrl & auxIcon & """ border=0 width=""24px"" class=""hrcthemeimage metro-tile-group-title-icon"" style=""margin-right:5px"" >"
        End If
        auxPrincipalPanel.Title &= auxTitle
        auxPrincipalPanel.Title = "<div class=metro><nav class=""breadcrumbs"">" _
               & "<ul>" _
                & "<li><a href=""cfrmdocumentos.aspx?_view_=" & CInt(enumView.coMetro_Inicial) & """>" _
                & "<img src=""imagenes/objhome.png"" style=""height:24px"" class=""hrcthemeimage metro-tile-group-title-icon"" >" _
                & "</a>" _
                & "</li>" _
                & "<li>" & auxIcon & auxTitle _
                & "</li>" _
                & "</ul>" _
                & "</nav></div>"
        Dim auxScript As String = ""
        If auxFilterEnabled Then
            Dim auxControlDsc As clshrcJSTextBox
            auxControlDsc = auxHrcContext.gObjectTmp_Download("txtdsc")
            If auxControlDsc Is Nothing Then
                auxControlDsc = New clshrcJSTextBox(pControlID:="txtdsc", pTitle:="", pFieldData:="dsc", pCSSClass:=auxHrcContext.ControlTextBoxCSS, pMaxLength:=200, pWidth:="99%", pHeight:="", pMultiline:=False, pFormatter:="", pUnFormatter:="")
                auxControlDsc.gValue_Set(auxClass.gLoginPreference_GetValue(enumPrefType.coDocumentossearch, "dsc"))
                'auxControlDsc.ServerStateMantain = True
                auxPrincipalPanel.gControls_Add(auxControlDsc)
                auxClearScript &= ";" & auxControlDsc.gJS_Value_Set("''", True)
            End If

            Dim auxControlNro As clshrcJSTextBox
            auxControlNro = New clshrcJSTextBox(pControlID:="txtnro", pTitle:="", pFieldData:="nro", pCSSClass:=auxHrcContext.ControlTextBoxCSS, pMaxLength:=200, pWidth:="99%", pHeight:="", pMultiline:=False, pFormatter:="", pUnFormatter:="")
            auxControlNro.gValue_Set(auxClass.gLoginPreference_GetValue(enumPrefType.coDocumentossearch, "nro"))
            'auxControlNro.ServerStateMantain = True
            auxPrincipalPanel.gControls_Add(auxControlNro)
            auxClearScript &= ";" & auxControlNro.gJS_Value_Set("''", True)

            auxValue = Val(auxClass.gLoginPreference_GetValue(enumPrefType.coDocumentossearch, "apacod"))
            Dim auxDTAPA As DataTable = auxClass.Conn.gConn_Query("SELECT * FROM DOC_APA " _
                                                                  & " WHERE (baja {#ISNULL#} OR baja={#FALSE#}) " _
                                                                  & " ORDER BY dsc")
            Dim auxControlAPA As clshrcJSComboBox
            auxControlAPA = New clshrcJSComboBox(pControlID:="cmbapa", pTitle:="", pFieldData:="apacod", pCSSClass:=auxHrcContext.ControlComboBoxCSS, pDT:=auxDTAPA, pWidth:="100px", pHeight:="")
            auxControlAPA.DefaultValue = auxValue
            auxControlAPA.Width = "200"
            'auxControlAPA.ServerStateMantain = True
            auxPrincipalPanel.gControls_Add(auxControlAPA)
            auxClearScript &= ";" & auxControlAPA.gJS_Value_Set("'-1'", True)


            auxValue = Val(auxClass.gLoginPreference_GetValue(enumPrefType.coDocumentossearch, "clacod"))
            Dim auxDTCLA As DataTable = auxClass.Conn.gConn_Query("SELECT * FROM DOC_CLA " _
                                                                  & " WHERE (baja {#ISNULL#} OR baja={#FALSE#}) " _
                                                                  & " ORDER BY dsc")
            Dim auxControlCLA As clshrcJSComboBox
            auxControlCLA = New clshrcJSComboBox(pControlID:="cmbCla", pTitle:="", pFieldData:="clacod", pCSSClass:=auxHrcContext.ControlComboBoxCSS, pDT:=auxDTCLA, pWidth:="100px", pHeight:="")
            auxControlCLA.DefaultValue = auxValue
            auxControlCLA.Width = "200"
            auxControlCLA.ServerStateMantain = True
            auxPrincipalPanel.gControls_Add(auxControlCLA)
            auxClearScript &= ";" & auxControlCLA.gJS_Value_Set("'-1'", True)

            auxValue = Val(auxClass.gLoginPreference_GetValue(enumPrefType.coDocumentossearch, "doctipcod"))
            Dim auxDTDOCTIP As DataTable = New DataView(hrcEntityDT_DOC_DOCTIP, "", "dsc", DataViewRowState.CurrentRows).ToTable
            Dim auxControlDOCTIP As clshrcJSComboBox
            auxControlDOCTIP = New clshrcJSComboBox(pControlID:="cmbDocTipcod", pTitle:="", pFieldData:="doctipcod", pCSSClass:=auxHrcContext.ControlComboBoxCSS, pDT:=auxDTDOCTIP, pWidth:="100px", pHeight:="")
            'auxControlDOCTIP.DefaultValue = auxValue
            auxControlDOCTIP.gValue_Set(auxValue)
            auxControlDOCTIP.Width = "200"
            auxControlDOCTIP.ServerStateMantain = True
            auxPrincipalPanel.gControls_Add(auxControlDOCTIP)
            auxClearScript &= ";" & auxControlDOCTIP.gJS_Value_Set("'-1'", True)


            Select Case pView
                Case enumView.coBiblioteca_PorTipoproceso, enumView.coBiblioteca_PorUnidad, enumView.coBiblioteca_PorTipoDoc, enumView.coBiblioteca_PorTipoProcesoyProceso, enumView.coBiblioteca_PorSistema
                    auxValue = Val(auxClass.gLoginPreference_GetValue(enumPrefType.coDocumentossearch, "wfwstp"))
                    '(1,2,3,4,5,6,7,9,10,16,11,27,-1)
                    Dim auxDTWFWSTP As DataTable = New DataView(hrcEntityDT_Q_WFWSTP, "wfwstpcod IN (" _
                                                                               & CInt(enumWorkflowStep.coWFWSTPDOC_DOCEdicion) _
                                                                               & "," & CInt(enumWorkflowStep.coWFWSTPDOC_DOCRevisionSSG) _
                                                                               & "," & CInt(enumWorkflowStep.coWFWSTPDOC_DOCAprobacion) _
                                                                               & "," & CInt(enumWorkflowStep.coWFWSTPDOC_DOCPublicacion) _
                                                                               & "," & CInt(enumWorkflowStep.coWFWSTPDOC_DOCDocumentovigente) _
                                                                               & "," & CInt(enumWorkflowStep.coWFWSTPDOC_DOCCancelacion) _
                                                                               & "," & CInt(enumWorkflowStep.coWFWSTPDOC_DOCDocumentoobsoleto) _
                                                                               & "," & CInt(enumWorkflowStep.coWFWSTPDOC_DOCnuevaversion) _
                                                                               & "," & CInt(enumWorkflowStep.coWFWSTPDOC_DOCCreacion) _
                                                                               & "," & CInt(enumWorkflowStep.coWFWSTPDOC_DOCSolicitudeliminacion) _
                                                                               & "," & CInt(enumWorkflowStep.coWFWSTPDOC_DOCSolicitudnuevaversion) _
                                                                               & "," & CInt(enumWorkflowStep.coWFWSTPDOC_DOCSolicitudautomaticanuevaversion) _
                                                                               & ",-1" _
                                                                               & ")", "", DataViewRowState.CurrentRows).ToTable

                    Dim auxControlWFWSTP As clshrcJSComboBox
                    auxControlWFWSTP = New clshrcJSComboBox(pControlID:="cmbWfwStp", pTitle:="Estado:", pFieldData:="wfwstp", pCSSClass:=auxHrcContext.ControlComboBoxCSS, pDT:=auxDTWFWSTP, pWidth:="100px", pHeight:="")
                    auxControlWFWSTP.DefaultValue = auxValue
                    auxControlWFWSTP.Width = "200"
                    auxControlWFWSTP.ServerStateMantain = True
                    auxPrincipalPanel.gControls_Add(auxControlWFWSTP)
                    auxClearScript &= ";" & auxControlWFWSTP.gJS_Value_Set("'-1'", True)
            End Select


            Dim auxnewRow As DataRow

            If pMIsAccionesOn >= 0 Then
                auxValue = pMIsAccionesOn
            Else
                auxValue = Val(auxClass.gLoginPreference_GetValue(enumPrefType.coDocumentossearch, "misacciones"))
            End If
            Dim auxSiNoDT As DataTable = auxHrcContext.gDT_GetYesNoTable(False)
            If pMIsAccionesvisible Then
                Dim auxControlmisacciones As clshrcJSComboBox
                auxControlmisacciones = New clshrcJSComboBox(pControlID:="chkmisacciones", pTitle:="Mis acciones pendientes:", pFieldData:="misacciones", pCSSClass:=auxHrcContext.ControlComboBoxCSS, pDT:=auxSiNoDT, pWidth:="100px", pHeight:="")
                auxControlmisacciones.DisplayMode = clshrcJSComboBox.enumDisplayMode.Radio
                'auxControlmisacciones.gValue_Set(auxValue)
                auxControlmisacciones.DefaultValue = auxValue

                auxControlmisacciones.ServerStateMantain = True
                auxPrincipalPanel.gControls_Add(auxControlmisacciones)
                auxClearScript &= ";" & auxControlmisacciones.gJS_Value_Set("'0'", True)
            Else
                If pMIsAccionesOn >= 0 Then
                    auxPrincipalPanel.BagValues.gValue_Add("misacciones_default", pMIsAccionesOn)
                End If
            End If

            If pIncludeChildsVisible Then
                Dim auxControlmisacciones As clshrcJSComboBox
                auxControlmisacciones = New clshrcJSComboBox(pControlID:="chkincludechilds", pTitle:="Incluir subdocumentos:", pFieldData:="includechilds", pCSSClass:=auxHrcContext.ControlComboBoxCSS, pDT:=auxSiNoDT, pWidth:="100px", pHeight:="")
                auxControlmisacciones.DisplayMode = clshrcJSComboBox.enumDisplayMode.Radio
                auxValue = Val(auxClass.gLoginPreference_GetValue(enumPrefType.coDocumentossearch, "includechilds"))
                auxControlmisacciones.DefaultValue = auxValue
                auxControlmisacciones.ServerStateMantain = True
                auxPrincipalPanel.gControls_Add(auxControlmisacciones)
                auxClearScript &= ";" & auxControlmisacciones.gJS_Value_Set("'0'", True)
            End If

            Dim auxSavedValues As New clshrcBagValues(auxClass.gLoginPreference_GetValue(enumPrefType.coDocumentossearch, "filters"))
            Dim auxNodeList As New clsNodeList


            'UND
            Dim auxUND As clshrcObjectExplorer
            auxUND = New clshrcObjectExplorer("filter_und", "hrcGrdData.ashx", Nothing, auxClass.Conn.gComponent_CreateInstance, auxClientCon)
            auxUND.FieldData = "und"
            auxUND.gAutosuggest_Enabled("SELECT UND.cod AS cod,UND.dsc as dsc," & enumEntities.coEntityUND & " as q_type,'Unidad' FROM UND   WHERE (  (UND.dsc LIKE '%{#HRCDSC#}%' ) AND (UND.baja = 0 OR UND.baja  IS NULL)) AND UND.cod >= 1" _
                                              & " ORDER BY dsc,q_type")
            auxUND.StringStartText = "Unidad específica"

            auxNodeList.Config_ImportFromBagValues(auxSavedValues, "UND")
            For Each auxUndNode As clsNode In auxNodeList.NodeList
                If auxUndNode.Cod > 0 Then
                    Select Case auxUndNode.Type
                        Case enumEntities.coEntityUND
                            auxUND.gStartItem_Add(enumEntities.coEntityUND, auxUndNode.Cod, hrcEntityDT_UND_FindByKey(auxUndNode.Cod)("dsc"))
                    End Select
                End If
            Next

            auxUND.SelectionLimit = 10
            auxPrincipalPanel.gControls_Add(auxUND)
            auxClearScript &= ";" & auxUND.gJS_ClearAll


            'DOCTARGET
            Dim auxDocTarget As clshrcObjectExplorer
            auxDocTarget = New clshrcObjectExplorer("filter_doctarget", "hrcGrdData.ashx", Nothing, auxClass.Conn.gComponent_CreateInstance, auxClientCon)
            auxDocTarget.FieldData = "doctarget"
            auxDocTarget.gAutosuggest_Enabled("SELECT UND.cod AS cod,UND.dsc as dsc," & enumEntities.coEntityUND & " as q_type,'Unidad' FROM UND   WHERE miembrosgrpcod > 0 AND  (  (UND.dsc LIKE '%{#HRCDSC#}%' ) AND (UND.baja = 0 OR UND.baja  IS NULL)) AND UND.cod >= 1" _
                                                & " UNION " _
                                                & " SELECT cod, dsc," & enumEntities.coEntityEMP & " as q_type,'Colaborador' as line1 " _
                                                & " FROM EMP WHERE cod > 0 AND seccod > 0 AND (baja = {#FALSE#} OR baja {#ISNULL#}) AND dsc LIKE '%{#HRCDSC#}%' " _
                                                & " UNION " _
                                                & " SELECT cod, dsc," & enumEntities.coEntityDOC_EQU & " as q_type,'Función o equipo' as line1 " _
                                                & " FROM DOC_EQU WHERE miembrosgrpcod > 0 AND cod > 0 AND dsc LIKE '%{#HRCDSC#}%' " _
                                                & " ORDER BY dsc,q_type")
            'auxDocTarget.ObjectExplorerEnabled = True
            'auxDocTarget.CentralGrid.gSelectList_Add(enumEntities.coEntityUND, enumEntities.coEntityUND, "(SELECT UND.cod AS cod,UND.dsc as dsc," & enumEntities.coEntityUND & " as q_type FROM UND   WHERE ((((UND.undcodsup = {#HRCCOD#} AND '{#HRCDSC#}'='')  OR  (UND.dsc LIKE '%{#HRCDSC#}%' AND '{#HRCDSC#}'<>''))) AND (UND.baja = 0 OR UND.baja  IS NULL)) AND UND.cod >= 1)")
            'auxDocTarget.CentralGrid.gSelectTypeFilter_Add(enumEntities.coEntityUND)
            'auxDocTarget.CentralGrid.gSelectList_Add(enumEntities.coEntityUND, enumEntities.coEntityEMP, "(SELECT EMP.cod AS cod,EMP.dsc as dsc," & enumEntities.coEntityEMP & " as q_type FROM EMP   WHERE ((((EMP.undcod = {#HRCCOD#} AND '{#HRCDSC#}'='')  OR  (EMP.dsc LIKE '%{#HRCDSC#}%' AND '{#HRCDSC#}'<>''))) AND (EMP.baja = 0 OR EMP.baja  IS NULL)) AND EMP.cod >= 1)")
            'auxDocTarget.CentralGrid.gSelectTypeFilter_Add(enumEntities.coEntityEMP)
            'auxDocTarget.CentralGrid.gSelectList_Add(enumEntities.coEntityUND, enumEntities.coEntityUND, "(SELECT DOC_EQU.cod AS cod,DOC_EQU.dsc as dsc," & enumEntities.coEntityDOC_EQU & " as q_type FROM DOC_EQU   WHERE ((((DOC_EQU.undcod = {#HRCCOD#} AND '{#HRCDSC#}'='')  OR  (DOC_EQU.dsc LIKE '%{#HRCDSC#}%' AND '{#HRCDSC#}'<>''))) AND (DOC_EQU.baja = 0 OR DOC_EQU.baja  IS NULL)) AND DOC_EQU.cod >= 1)")
            'auxDocTarget.CentralGrid.gSelectTypeFilter_Add(enumEntities.coEntityDOC_EQU)
            
            'ScriptManager.RegisterStartupScript(Me, Me.GetType, "hrc" & auxDocTarget.ControlID, _
            '                                   auxScript, True)
          
            auxNodeList.Config_ImportFromBagValues(auxSavedValues, "DOCTARGET")
            For Each auxDocTargetNode As clsNode In auxNodeList.NodeList
                If auxDocTargetNode.Cod > 0 Then
                    Select Case auxDocTargetNode.Type
                        Case enumEntities.coEntityEMP
                            auxDocTarget.gStartItem_Add(enumEntities.coEntityEMP, auxDocTargetNode.Cod, hrcEntityDT_EMP_FindByKey(auxDocTargetNode.Cod)("dsc"))
                        Case enumEntities.coEntityDOC_EQU
                            auxDocTarget.gStartItem_Add(enumEntities.coEntityDOC_EQU, auxDocTargetNode.Cod, hrcEntityDT_DOC_EQU_FindByKey(auxDocTargetNode.Cod)("dsc"))
                        Case enumEntities.coEntityUND
                            auxDocTarget.gStartItem_Add(enumEntities.coEntityUND, auxDocTargetNode.Cod, hrcEntityDT_UND_FindByKey(auxDocTargetNode.Cod)("dsc"))
                    End Select
                End If
            Next
            auxDocTarget.AutosuggestWidth = 270
            auxDocTarget.StringStartText = "Relacionado a"
            auxDocTarget.SelectionLimit = 10
            auxPrincipalPanel.gControls_Add(auxDocTarget)
            auxClearScript &= ";" & auxDocTarget.gJS_ClearAll
        End If

        Dim auxGridDOC As clshrcGrdData

        auxGridDOC = New clshrcGrdData("grddoc", "Documentos", auxHrcContext, True)
        AddHandler auxGridDOC.EventCommandHandler, AddressOf gDocumentsSearchPanel_CommandHandler
        'auxGridDOC.TopScroll = True
        auxGridDOC.CellFixedHeight = 25
        auxPrincipalPanel.gControls_Add(auxGridDOC)
        auxGridDOC.RaiseEventBlockDataBoundONEveryRequest = True
        'prvGridData.ToolbarPosition = clsHrcJSControlBasic.enumVerticalAlign.coTop
        'auxGridDOC.gDebug_On("c:\windows\temp\grilla.txt", "")
        'auxGridDOC.gDebugJS_On()
        auxGridDOC.ColumnName_Cod = "cod"
        auxGridDOC.ColumnName_Type = "q_type"
        auxGridDOC.TreeGridLoadOnRequest = True
        auxGridDOC.SaveExpandedNodesState = True
        auxGridDOC.HideLeafNodeIcon = True
        auxGridDOC.TreeGridExpandColapaseAllButton = True
        Select Case pView
            Case enumView.coMetro_PorProceso, enumView.coMetro_PorTipoProceso
                'auxGridDOC.TreeGridExpandColapaseAllButton = False
                '
                auxGridDOC.Title = ""

                auxGridDOC.HideColumnHeaders = True
            Case Else
                auxGridDOC.gPager_EnableVirtual(10)
        End Select
        Select Case pView
            Case enumView.coBiblioteca_Edicion, enumView.coBiblioteca_Revision, enumView.coBiblioteca_Aprobacion, enumView.coBiblioteca_Cancelacion, enumView.coBiblioteca_Lectura, enumView.coBiblioteca_NuevasVersiones, enumView.coBiblioteca_NuevosDoc, enumView.coBiblioteca_Creacion, enumView.coBiblioteca_Eliminacion
                auxGridDOC.ExpandAllAfterLoad = True
        End Select
        Dim auxColumnID As Integer
        Dim auxFormatter As String = ""
        auxGridDOC.gColumn_Add("Codigo", 0, "q_cod", clshrcGrdData.enumAlign.coCenter, True, "", "", False)

        auxScript = "function griddata_formatcol0(pType,pID) {" _
                    & "var auxType = parseInt(pType);" _
                    & "var auxReturn='&nbsp';" _
                    & "if ((auxType == " & enumEntities.coEntityDOC_DOC & ")){" _
                        & "auxReturn=pID;" _
                    & "};" _
                    & "return auxReturn;" _
                    & "}"
        auxFormatter = "griddata_formatcol0({#CURRENTROW_Q_TYPE#},{#CURRENTROW_IDENTIFICADOR#})"
        auxGridDOC.gColumn_Add("ID", 8, "identificador", clshrcGrdData.enumAlign.coRight, False, auxFormatter, auxScript, False)

        
        'Column 1

        auxScript = "function griddata_formatcol1(pType,pCod,pDsc) {" _
                    & "var auxType = parseInt(pType);" _
                    & "var auxReturn='&nbsp';" _
                    & "if (auxType == " & enumEntities.coEntityDOC_DOC & "){" _
                        & "auxReturn = '<a href=cfrmdocumentos1_det.aspx?_lastview_=" & pView & "&_mode_=0&param1='+pCod+'&apa=" & pApaCod & "&pro=" & pProCod & ">';" _
                    & "};" _
                    & "if (auxType >0){" _
                        & "auxReturn = auxReturn + '<img border=0 width=12px alt="""" src=imagenes/icon' + gNumber_pad(auxType, 8) + '.png />';" _
                    & "}else{;" _
                        & "auxReturn = auxReturn + '<img border=0 width=12px alt="""" src=imagenes/objnoparent.png />';" _
                    & "};" _
                    & "auxReturn = auxReturn + pDsc; " _
                    & "if (auxType == " & enumEntities.coEntityDOC_DOC & "){" _
                        & "auxReturn = auxReturn + '</a>'; " _
                    & "};" _
                    & "return auxReturn;" _
                    & "}"
        auxFormatter = "griddata_formatcol1({#CURRENTROW_Q_TYPE#},{#CURRENTROW_COD#},{#CURRENTROW_Q_DSC#})"
        auxColumnID = auxGridDOC.gColumn_Add("", -1, "q_dsc", clshrcGrdData.enumAlign.coLeft, False, auxFormatter, auxScript, False)
        auxGridDOC.gColumn_MarkAsExpandable(auxColumnID)
        Dim auxColumnWidth As Integer
        If auxInboxDetailedEnabled Then
            'column - Ver detalle
            auxScript = "function griddata_formatcol_obs(pType,pObs) {" _
                    & "var auxType = parseInt(pType);" _
                    & "var auxReturn='&nbsp';" _
                    & "if ((auxType == " & enumEntities.coEntityDOC_DOC & ") && pObs != ''){" _
                    & "auxReturn =  '<span style=color:#909090 >' + pObs + '</span>';" _
                    & "};" _
                    & "return auxReturn;" _
                    & "}"
            auxFormatter = "griddata_formatcol_obs({#CURRENTROW_Q_TYPE#},{#CURRENTROW_OBS#})"
            auxGridDOC.gColumn_Add("", 25, "", clshrcGrdData.enumAlign.coLeft, False, auxFormatter, auxScript, False)
        End If

        'column 1 - Ver vigente
        auxScript = "function griddata_formatcolvig(pType,pCod,pDsc,pFecha,pVersion,pRead) {" _
                    & "var auxType = parseInt(pType);" _
                    & "var auxVersion = parseInt(pVersion);" _
                    & "var auxReturn='&nbsp';" _
                    & "var auxFecha=Date.parse(pFecha);" _
                    & "if (auxFecha != null){" _
                        & "auxReturn ='<u><span style=cursor:pointer; onclick=hrcShowWindowNoModal(""cfrmdocumentos_impresion.aspx?_closea_=1&_mode_=8&param1=""+""' + pCod + '"") >" _
                        & "Ver vigente v' + auxVersion + '(' + " & auxHtml.gJS_Date_Format("auxFecha", clsHrcCodeHTML.enumDateTimeFormat.coDateShort) & " + ')'" _
                        & "+ '</a></u>';" _
                        & "if (pRead == 1){auxReturn = auxReturn + ' <img src=imagenes/doc_read.png border=0 width=14px alt=""Debe confirmar lectura"" >'};" _
                    & "};" _
                    & "return auxReturn;" _
                    & "}"
        auxFormatter = "griddata_formatcolvig({#CURRENTROW_Q_TYPE#},{#CURRENTROW_COD#},{#CURRENTROW_Q_DSC#},{#CURRENTROW_FECHAVIGENTE#},{#CURRENTROW_VERSION#},{#CURRENTROW_DOCVIG_READ#})"
        Select Case pView
            Case enumView.coMetro_PorProceso, enumView.coMetro_PorTipoProceso
                auxColumnWidth = 20
            Case Else
                auxColumnWidth = 12
        End Select
        auxGridDOC.gColumn_Add("", auxColumnWidth, "", clshrcGrdData.enumAlign.coCenter, False, auxFormatter, auxScript, False)


        'Column 2 - Ver cambios
        auxScript = "function griddata_formatcol2(pType,pCod,pVersion) {" _
                    & "var auxReturn='&nbsp';" _
                    & "var auxType = parseInt(pType);" _
                    & "if ((pVersion > 1) && (auxType == " & enumEntities.coEntityDOC_DOC & ")){" _
                        & "auxReturn ='<u><span style=cursor:pointer; onclick=hrcShowWindowNoModal(""cfrmdocumentos_cambios.aspx?_closea_=1&_mode_=6&_view_=1&param1=""+""' + pCod + '"") >" _
                        & "Revisar cambios'" _
                        & "+ '</a></u>';};" _
                    & "return auxReturn;" _
                    & "}"
        auxFormatter = "griddata_formatcol2({#CURRENTROW_Q_TYPE#},{#CURRENTROW_COD#},{#CURRENTROW_VERSION#})"
        Select Case pView
            Case enumView.coMetro_PorProceso, enumView.coMetro_PorTipoProceso
                auxColumnWidth = 15
            Case Else
                auxColumnWidth = 8
        End Select
        auxGridDOC.gColumn_Add("", auxColumnWidth, "", clshrcGrdData.enumAlign.coCenter, False, auxFormatter, auxScript, False)


        Select Case pView
            Case enumView.coBiblioteca_PorSistema, enumView.coBiblioteca_PorTipoDoc, enumView.coBiblioteca_PorTipoproceso, enumView.coBiblioteca_PorTipoProcesoyProceso, enumView.coBiblioteca_PorUnidad, enumView.coBiblioteca_SinAnidar _
                , enumView.coBiblioteca_Edicion, enumView.coBiblioteca_Revision, enumView.coBiblioteca_Aprobacion, enumView.coBiblioteca_Cancelacion, enumView.coBiblioteca_Lectura, enumView.coBiblioteca_NuevasVersiones, enumView.coBiblioteca_NuevosDoc, enumView.coBiblioteca_PorSistema, enumView.coBiblioteca_PorTipoDoc, enumView.coBiblioteca_PorTipoproceso, enumView.coBiblioteca_Creacion, enumView.coBiblioteca_Eliminacion
                auxScript = "function tooltip_show(pmsg){" _
                    & "blueBalloon.showTooltip(event,'<b>Ultimo mensaje</b><br />'+pmsg)" _
                    & "}"
                auxScript &= "function griddata_formatcol3(pWfwStpDsc,pObs) {" _
                    & "var auxReturn=pWfwStpDsc + '&nbsp';" _
                    & "if (pObs == null){pObs=''};" _
                    & "if (pObs != ''){" _
                        & "auxReturn =pWfwStpDsc + '<img src=./imagenes/objmessage.png border=0 width=16 onmouseout=kill()" _
                        & " onmouseover=tooltip_show(""' + pObs + '"") />';" _
                    & "};" _
                    & "return auxReturn;" _
                    & "}"
                auxFormatter = "griddata_formatcol3({#CURRENTROW_WFWSTPDSC#},{#CURRENTROW_DOCLOGOBS#})"
                auxGridDOC.gColumn_Add("Estado", 12, "", clshrcGrdData.enumAlign.coCenter, False, auxFormatter, auxScript, False)
                auxGridDOC.gColumn_Add("doclogobs", 0, "doclogobs", clshrcGrdData.enumAlign.coCenter, False, "", "", False)

                'Column 4  
                auxScript = "function griddata_formatcol4(pType,pCod) {" _
                    & "var auxReturn='&nbsp';" _
                    & "if (pType == '" & enumEntities.coEntityDOC_DOC & "'){" _
                    & "auxReturn ='<u><a href=cfrmdocumentos1_det.aspx?_lastview_=" & pView & "&_mode_=0&param1='+pCod+'&apa=" & pApaCod & "&pro=" & pProCod & ">Ver</a></u>';" _
                    & "};" _
                    & "return auxReturn;" _
                    & "}"
                auxFormatter = "griddata_formatcol4({#CURRENTROW_Q_TYPE#},{#CURRENTROW_COD#})"
                auxGridDOC.gColumn_Add("Ver", 3, "", clshrcGrdData.enumAlign.coCenter, False, auxFormatter, auxScript, False)

                'Column 5 
                auxScript = "function griddata_formatcol5(pType,pCod) {" _
                    & "var auxReturn='&nbsp';" _
                    & "var auxType = parseInt(pType);" _
                    & "if (auxType == " & enumEntities.coEntityDOC_DOC & "){" _
                    & "auxReturn ='<u><a href=cfrmdocumentos1_det.aspx?_lastview_=" & pView & "&_mode_=2&param1='+pCod+'&apa=" & pApaCod & "&pro=" & pProCod & ">Editar</a></u>';" _
                    & "};" _
                    & "return auxReturn;" _
                    & "}"
                auxFormatter = "griddata_formatcol5({#CURRENTROW_Q_TYPE#},{#CURRENTROW_COD#})"
                auxGridDOC.gColumn_Add("Editar", 4, "", clshrcGrdData.enumAlign.coCenter, False, auxFormatter, auxScript, False)

        End Select
        'auxGridDOC.gColumn_AddAsHidden("q_group")
        'auxGridDOC.gColumn_AddAsHidden("q_parent")
        auxGridDOC.gJS_ControlReady()
        auxGridDOC.ColumnName_Type = "q_Type"
        auxGridDOC.gNodeSelected_Clear()
        If Request.QueryString("param1") IsNot Nothing Then
            Dim auxPrevCod As Integer = Val(Request.QueryString("param1").ToString)
            If auxPrevCod > 0 Then
                auxGridDOC.gTreeGrid_ExpandNodeList_Clear()
                auxGridDOC.gTreeGrid_ExpandNode_ByTypeCod(enumEntities.coEntityDOC_DOC, auxPrevCod)
                auxGridDOC.gNodeSelected_Add(enumEntities.coEntityDOC_DOC, auxPrevCod)
            End If
        End If

        Dim auxButtonSearch As New clsHrcJSButton("cmdSearch", "Buscar", auxHrcContext.ButtonCSS)
        AddHandler auxButtonSearch.EventCommandHandler, AddressOf gDocumentsSearchPanel_CommandHandler
        auxButtonSearch.RaiseCommandOnClick = True
        auxButtonSearch.EventOnClick = auxGridDOC.gJS_GetReloadCode
        auxPrincipalPanel.gControls_Add(auxButtonSearch)

        Dim auxButtonClear As New clsHrcJSButton("cmdclear", "Limpiar", auxHrcContext.ButtonCSS)
        auxButtonClear.EventOnClick = auxClearScript _
                     & ";return false"
        auxPrincipalPanel.gControls_Add(auxButtonClear)

        'XLS export
        auxPrincipalPanel.gJSCommand_Add("EXPORTXLS", "")
        Dim auxBagValuesXLS As New clshrcBagValues
        Dim auxButtonXLSExport As New clsHrcJSButton("lstexportxls", "Exportar XLS", auxHrcContext.ButtonCSS)
        auxScript = auxPrincipalPanel.gJSCommand_GetCall("EXPORTXLS", Nothing, _
                                                auxHtml.gJS_OpenModalDialog(auxButtonXLSExport.ControlID, _
                                                "'hrcbinaries.aspx?download=1&tmp=1&btmpid='+" & auxHtml.gJS_WebService_GetValueFromResult("XLS_TMPID") _
                                                , False, 300, 200, , "Descargar", True) & ";" _
                                            , "")
        auxButtonXLSExport.EventOnClick = auxScript

        auxButtonXLSExport.RaiseCommandOnClick = True
        auxPrincipalPanel.gControls_Add(auxButtonXLSExport)


        'Select Case pView
        'Case enumView.coBiblioteca_PorSistema, enumView.coBiblioteca_PorTipoDoc, enumView.coBiblioteca_PorTipoproceso, enumView.coBiblioteca_PorTipoProcesoyProceso, enumView.coBiblioteca_NuevosDoc, enumView.coBiblioteca_Creacion, enumView.coBiblioteca_Edicion
        If pPermFormNew Then
            Dim auxButtonNew As New clsHrcJSButton("cmdNuevo", "Nuevo", auxHrcContext.ButtonCSS)
            auxButtonNew.EventOnClick = auxHtml.gJS_GotoURL("'cfrmDocumentos1_det.aspx?_mode_=1&_closea_=0'")
            auxPrincipalPanel.gControls_Add(auxButtonNew)
        End If
        'end Select


        Dim auxcontent As String = ""
        auxScript = auxPrincipalPanel.gControl_GetStartupScripts
        auxcontent = auxPrincipalPanel.gControl_GetBodyDefinition
        Dim auxReturn As New clshrcBagValues
        auxReturn.gValue_Add("TITLE", auxTitle)
        auxReturn.gValue_Add("CONTENT", auxcontent)
        auxReturn.gValue_Add("SCRIPT", auxScript)
        Return auxReturn
    End Function


End Class



