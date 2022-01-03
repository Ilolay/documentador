Imports System.Data
Imports System.Data.SqlClient
Imports Intelimedia.imComponentes
Imports clsCusimDOC
Imports System.Web.Script.Services
Imports System.Web.Services
Imports System.Xml

'Imports Intelimedia.imComponentes2

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
Partial Class cfrmdocumentos1_det
    Inherits imWebPage
    Private Enum enumView As Short
        coDefault = 1
        coTro = 5
        coTroForm = 6
        coTroFormDetail = 7
    End Enum
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            Dim auxHrcContext As clsHrcJSContext = Session("hrccontext")
            Dim auxHTML As New clsHrcCodeHTML
            auxHTML.DateFormatEnabled = False
            Dim auxQueryValues As clshrcBagValues
            auxQueryValues = auxHTML.gBagValues_GetFromQueryString(Request.QueryString.ToString)
            Dim auxView As enumView
            auxView = Val(auxQueryValues.gValue_Get("_VIEW_", -1))
            If auxView < 1 Then
                auxView = enumView.coDefault
            End If
            Dim auxBagResult As clshrcBagValues
            Dim auxClass As New clsCusimDOC
            auxClass.Conn.gConn_Open()
            Dim auxIsAdmin As Boolean = Session("isadmin")
            Select Case auxView
                Case enumView.coDefault
                    auxBagResult = gFormDOC_Get(auxHrcContext)
                Case enumView.coTroForm
                    auxBagResult = gFormTRO_Get(auxClass, auxHrcContext, auxQueryValues, auxIsAdmin)
                Case enumView.coTroFormDetail
                    auxBagResult = gFormTRODetail_Get(auxClass, auxHrcContext, auxQueryValues, auxIsAdmin)
            End Select
            auxClass.Conn.gConn_Close()
            Dim auxIsMobile As Boolean = False
            If auxBagResult IsNot Nothing Then
                If auxIsMobile Then
                    'fmeContent.InnerHtml = auxBagResult.gValue_Get("CONTENT", "")
                    'fmeContent.InnerHtml = auxClass.gWrappers_GetMobile(fmeContent.InnerHtml)
                    'fmeContent.InnerHtml = fmeContent.InnerHtml.Replace("{#TITLE#}", auxBagResult.gValue_Get("TITLE", ""))
                Else
                    fmeContent.InnerHtml &= auxBagResult.gValue_Get("CONTENT", "")
                    'fmeContent.InnerHtml = auxClass.gWrappers_GetStandard(auxMaster_Content, auxPopup, fmeContent.InnerHtml, auxUser_Menu)
                    fmeContent.InnerHtml = fmeContent.InnerHtml.Replace("{#TITLE#}", "")
                End If
                ScriptManager.RegisterStartupScript(Me, Me.GetType, "hrcDocumentos1", _
                                    auxBagResult.gValue_Get("SCRIPT", ""), True)
            End If
        End If
    End Sub
    Private Function gPanelDOCUMENTS_Get(ByVal pClass As clsCusimDOC, _
                                         ByVal pHrcContext As clsHrcJSContext, _
                                         ByVal pPrincipalPanel As clsHrcJSPanel, _
                                         ByVal pEditMode As Boolean, _
                                         ByVal pDftGenCod As String, _
                                         ByVal pDocCod As Integer) As clshrcGrdData
        '////////////////7  REQDOC
        Dim auxControlGrdDoc_DocHTML As clshrcJSTextEditor
        Dim auxControlGrdDocs As clshrcGrdData = Nothing
        Dim auxControlGrdDocsID As String = ""
        auxControlGrdDocsID = pClass.Conn.gField_GetUniqueID
        auxControlGrdDocs = New clshrcGrdData("grddoc", auxControlGrdDocsID, "", _
                                "hrcgrdData.ashx", Nothing, "", True, True)
        '        auxControlGrdDocs.gDebug_On("C:\WINDOWS\temp\grddoc.txt", "c:\windows\temp\grddoc_cursor.txt")
        If pPrincipalPanel IsNot Nothing Then
            pPrincipalPanel.gControls_Add(auxControlGrdDocs)
        End If

        auxControlGrdDocs.RaiseEventBlockDataBoundONEveryRequest = True
        auxControlGrdDocs.HideLeafNodeIcon = True
        auxControlGrdDocs.HideColumnHeaders = True
        auxControlGrdDocs.TreeGridExpandColapaseAllButton = True
        auxControlGrdDocs.ExpandAllAfterLoad = True
        'auxControlGrdDocs.gTreeGrid_ExpandNodeListSet("-1")
        AddHandler auxControlGrdDocs.EventCommandHandler, AddressOf gPanelDOCUMENTS_CommandHandler
        auxControlGrdDocs.ButtonCSS = pHrcContext.ButtonCSS
        auxControlGrdDocs.TooltipCSS = "obs-controles"
        Dim auxCommandInsert As String = ""
        Dim auxCommandDelete As String = ""
        Dim auxCommandUpdate As String = ""
        If pEditMode Then
            auxCommandUpdate = "UPDATE DOC_DOCANX_DFT SET " _
                    & "dsc='{#Q_DSC#}',doctipcod={#DOCTIPCOD#},link='{#LINK#}',tipo={#TIPO#} WHERE cod={#COD#} AND dftdidgencod='" & pDftGenCod & "'"
            auxCommandInsert = "UPDATE DOC_DOCANX_DFT SET " _
                    & "dsc='{#Q_DSC#}',doctipcod={#DOCTIPCOD#},link='{#LINK#}',tipo={#TIPO#} WHERE cod={#COD#} AND dftdidgencod='" & pDftGenCod & "'"
        End If
        auxControlGrdDocs.DefaultPanel_Enable("SELECT DOC_DOCANX.cod as q_cod,DOC_DOCANX.dsc as q_dsc,NULL,DOC_DOCANX.doctipcod as sup," & enumEntities.coEntityDOC_DOCANX & ",DOC_DOCANX.cod,DOC_DOCANX.dsc,DOC_DOCANX.link,DOC_DOCANX.baja,DOC_DOCANX.doc,DOC_DOCANX.doctipcod,DOC_DOCANX.tipo,DOC_DOCANX.dochtml,DOCTIP.dsc as doctipdsc,DOC_DOCANX.doctipcod FROM DOC_DOCANX INNER JOIN DOCTIP ON DOC_DOCANX.doctipcod = DOCTIP.cod " _
                    & " WHERE ( DOC_DOCANX.baja {#ISNULL#} OR DOC_DOCANX.baja ={#FALSE#})" _
                    & " AND DOC_DOCANX.cod={#COD#} AND DOC_DOCANX.cod NOT IN (SELECT qdft_cod FROM DOC_DOCANX_DFTREL WHERE dftdidgencod='" & pDftGenCod & "')" _
                    & " UNION " _
                    & " SELECT DOC_DOCANX.cod as q_cod,DOC_DOCANX.dsc as q_dsc,NULL,DOC_DOCANX.doctipcod as sup," & enumEntities.coEntityDOC_DOCANX & ",DOC_DOCANX.cod,DOC_DOCANX.dsc,DOC_DOCANX.link,DOC_DOCANX.baja,DOC_DOCANX.doc,DOC_DOCANX.doctipcod,DOC_DOCANX.tipo,DOC_DOCANX.dochtml,DOCTIP.dsc as doctipdsc,DOC_DOCANX.doctipcod FROM DOC_DOCANX_DFT AS DOC_DOCANX INNER JOIN DOCTIP ON DOC_DOCANX.doctipcod = DOCTIP.cod " _
                    & " WHERE ( DOC_DOCANX.baja {#ISNULL#} OR DOC_DOCANX.baja ={#FALSE#}) " _
                    & " AND DOC_DOCANX.cod={#COD#} AND dftdidgencod='" & pDftGenCod & "'", _
                    auxCommandUpdate, auxCommandInsert, "")
        AddHandler auxControlGrdDocs.DefaultPanel.EventCommandHandler, AddressOf gPanelDOCUMENTS_CommandHandler
        'auxControlGrdDocs.DefaultPanel.Width = "500px"
        'auxControlGrdDocs.DefaultPanel.Height = "400px"

        Dim auxCommandData As New clshrcBagValues ' SortedList(Of String, String)
        auxCommandData.gValue_Add("COD", "{#CURRENTROW_COD#}")

        auxControlGrdDocs.gQueryParameter_Add("doccod", pDocCod)

        Dim auxControlGrdDoc_Cod As New clsHrcJSHidden("panelgrddoc_codigo", "cod")
        auxControlGrdDocs.DefaultPanel.gControls_Add(auxControlGrdDoc_Cod)

        Dim auxControlGrdDoc_Dsc As New clshrcJSTextBox("panelgrddoc_dsc", "Título", "q_dsc", pHrcContext.ControlTextBoxCSS, 200, "300px", "50px", True, "", "")
        'auxControlGrdDoc_Dsc.IsRequired = True
        auxControlGrdDocs.DefaultPanel.gControls_Add(auxControlGrdDoc_Dsc)

        Dim auxDOCTip As DataTable = New DataView(hrcEntityDT_DOCTIP, "COD > 0", "dsc", DataViewRowState.CurrentRows).ToTable
        Dim auxControlGrdDoc_Doctip As New clshrcJSComboBox("panelgrddoc_doctip", "Tipo", "doctipcod", pHrcContext.ControlComboBoxCSS, auxDOCTip, "300px", "50px")
        auxControlGrdDoc_Doctip.DefaultValue = "'" & enumDOCTIP.coVarios & "'"
        auxControlGrdDocs.DefaultPanel.gControls_Add(auxControlGrdDoc_Doctip)

        Dim auxControlGrdDoc_Link As clshrcJSTextBox
        auxControlGrdDoc_Link = New clshrcJSTextBox("panelgrddoc_link", "", "link", pHrcContext.ControlTextBoxCSS, 255, "300px", "", False, "", "")

        Dim auxControlGrdDoc_FileUpload As clshrcJSFileUpload
        ' Dim auxControlGrdDoc_FileUploadID As String = ViewState("panelgrddoc_doc_content")
        ' If auxControlGrdDoc_FileUploadID = "" Then
        auxControlGrdDoc_FileUpload = New clshrcJSFileUpload("panelgrddoc_doc_content", "", "doc", "300px", "50px")
        'auxControlGrdDoc_FileUploadID = auxControlGrdDoc_FileUpload.CacheID
        'ViewState("panelgrddoc_doc_content") = auxControlGrdDoc_FileUploadID
        'auxClientCon.gObjectTmp_Upload(auxControlGrdDoc_FileUpload, auxControlGrdDoc_FileUploadID)
        'Else
        '    auxControlGrdDoc_FileUpload = auxClientCon.gObjectTmp_Download(auxControlGrdDoc_FileUploadID)
        'End If



        'Dim auxControlGrdDoc_DocHTMLID As String = ViewState("panelgrddoc_dochtml")
        'If auxControlGrdDoc_DocHTMLID = "" Then
        auxControlGrdDoc_DocHTML = New clshrcJSTextEditor("panelgrddoc_dochtml_content", "", "dochtml", "300px", "80px")
        '   auxControlGrdDoc_DocHTMLID = auxControlGrdDoc_FileUpload.CacheID
        'ViewState("panelgrddoc_dochtml") = auxControlGrdDoc_FileUploadID
        'auxClientCon.gObjectTmp_Upload(auxControlGrdDoc_DocHTML, auxControlGrdDoc_DocHTMLID)
        'Else
        '    auxControlGrdDoc_DocHTML = auxClientCon.gObjectTmp_Download(auxControlGrdDoc_DocHTMLID)
        'panel
        'End If


        Dim auxDocTipos As New DataTable
        auxDocTipos.Columns.Add(New DataColumn("cod", Type.GetType("System.Int32")))
        auxDocTipos.Columns.Add(New DataColumn("dsc", Type.GetType("System.String")))
        Dim auxnewRow As DataRow
        auxnewRow = auxDocTipos.NewRow
        auxnewRow("cod") = enumDOC_Tipos.coAnexo
        auxnewRow("dsc") = "Anexo"
        auxDocTipos.Rows.Add(auxnewRow)

        auxnewRow = auxDocTipos.NewRow
        auxnewRow("cod") = enumDOC_Tipos.coLink
        auxnewRow("dsc") = "Link"
        auxDocTipos.Rows.Add(auxnewRow)

        auxnewRow = auxDocTipos.NewRow
        auxnewRow("cod") = enumDOC_Tipos.coHTML
        auxnewRow("dsc") = "Edición de documento"
        auxDocTipos.Rows.Add(auxnewRow)

        'auxnewRow = auxDocTipos.NewRow
        'auxnewRow("cod") = enumDOC_Tipos.coForm
        'auxnewRow("dsc") = "Formulario"
        'auxDocTipos.Rows.Add(auxnewRow)

        Dim auxControlGrdDoc_Doctipo As New clshrcJSComboBox("panelgrddoc_tipo", "Formato", "TIPO", pHrcContext.ControlComboBoxCSS, auxDocTipos, "300px", "50px")
        auxControlGrdDoc_Doctipo.DisplayMode = clshrcJSComboBox.enumDisplayMode.Radio
        Dim auxDocTipo_DefaultValue As String '= pClass.gLoginPreference_GetValue(enumPrefType.coReqDocInsert, "doctipcod")
        If auxDocTipo_DefaultValue = "" Then
            auxDocTipo_DefaultValue = enumDOC_Tipos.coAnexo
        End If
        auxControlGrdDoc_Doctipo.DefaultValue = "'" & auxDocTipo_DefaultValue & "'"
        auxControlGrdDoc_Doctipo.JSEventChange = "" _
                    & "switch (pdata[0]){" _
                    & "case 1:case '1':" _
                    & auxControlGrdDoc_Link.gJS_Hide & ";" _
                    & auxControlGrdDoc_FileUpload.gJS_Show & ";" _
                    & auxControlGrdDoc_DocHTML.gJS_Hide & ";" _
                    & "break;" _
                    & "case 2:case '2':" _
                    & auxControlGrdDoc_Link.gJS_Show & ";" _
                    & auxControlGrdDoc_FileUpload.gJS_Hide & ";" _
                    & auxControlGrdDoc_DocHTML.gJS_Hide & ";" _
                    & "break;" _
                    & "case 3:case '3':case 4:case '4': " _
                    & auxControlGrdDoc_Link.gJS_Hide & ";" _
                    & auxControlGrdDoc_FileUpload.gJS_Hide & ";" _
                    & auxControlGrdDoc_DocHTML.gJS_Show & ";" _
                    & "break;" _
                    & "};"

        auxControlGrdDocs.DefaultPanel.gControls_Add(auxControlGrdDoc_Doctipo)
        auxControlGrdDocs.DefaultPanel.gControls_Add(auxControlGrdDoc_Link)
        auxControlGrdDocs.DefaultPanel.gControls_Add(auxControlGrdDoc_FileUpload)
        auxControlGrdDocs.DefaultPanel.gControls_Add(auxControlGrdDoc_DocHTML)

        Dim auxScript As String = ""
        Dim auxFormatter As String = ""
        auxControlGrdDocs.gColumn_Add("Codigo", 0, "q_cod", clshrcGrdData.enumAlign.coLeft, True, "", "", False)
        auxScript = "function grddocs_dsc(pCod,pDsc,pType,pTipo,pDocHTML,pDoc,pLink) {" _
                    & "var auxReturn=pDsc;" _
                    & "var auxType = parseInt(pType);" _
                    & "if (auxType == " & enumEntities.coEntityDOC_DOCANX & "){" _
                        & "var auxTipo = parseInt(pTipo);" _
                        & "auxReturn = '<img  width=14px"" alt="""" src=imagenes/attach_' + auxTipo + '.png >" _
                            & "<a href=# onclick=hrcShowWindowNoModal_WithDecode(' + String.fromCharCode(39);" _
                            & "switch (auxTipo){"
        auxScript &= "case 1:auxReturn=auxReturn + 'getbinary.aspx?id='+pDoc+  String.fromCharCode(39) + ');return false;';break;"
        auxScript &= "case 2:auxReturn=auxReturn + encodeURIComponent(pLink + '?') + String.fromCharCode(39) + ');return false; ';break;"
        auxScript &= "case 3:case 4:auxReturn=auxReturn + 'hrctexteditor.aspx?upload=3&id='+pDocHTML+  String.fromCharCode(39) + ');return false;';break;"
        auxScript &= "};" _
                & "auxReturn = auxReturn + '>' + pDsc + '</a>';" _
            & "};" _
            & "return auxReturn;" _
            & "}"
        auxFormatter = "grddocs_dsc({#CURRENTROW_COD#},{#CURRENTROW_Q_DSC#},{#CURRENTROW_Q_TYPE#},{#CURRENTROW_TIPO#},{#CURRENTROW_DOCHTML#},{#CURRENTROW_DOC#},{#CURRENTROW_LINK#})"
        auxControlGrdDocs.gColumn_Add("Documento", -1, "Q_dsc", clshrcGrdData.enumAlign.coLeft, False, auxFormatter, auxScript, False)


        'auxControlGrdDocs.gQueryParameter_Add(
        'If auxGrdHitosEditmode Then
        auxControlGrdDocs.gJSCommand_Add("VIEW", "", auxControlGrdDocs.DefaultPanel)
        auxScript = "function griddocs_view(pType,pCOD,pQ_DSC,pViewCommand) {" _
                    & "var auxReturn='&nbsp';" _
                    & "var auxType = parseInt(pType);" _
                    & "if (auxType == " & enumEntities.coEntityDOC_DOCANX & "){" _
                        & "auxReturn ='<u><span style=cursor:pointer; onclick=' + String.fromCharCode(39) + '" & auxControlGrdDocs.gJSCommand_GetCall("VIEW", auxCommandData) & "' + String.fromCharCode(39) + ' >" _
                        & "<img src=imagenes/actview.png border=0 width=16px >'" _
                        & "+ '</a></u>';};" _
                    & "return auxReturn;" _
                    & "}"
        auxFormatter = "griddocs_view({#CURRENTROW_Q_TYPE#},{#CURRENTROW_COD#},{#CURRENTROW_Q_DSC#},{#CURRENTROW_CMD_VIEW#})"
        auxControlGrdDocs.gColumn_Add("Ver", 5, "cmd_view", clshrcGrdData.enumAlign.coLeft, False, auxFormatter, auxScript, False)
        'auxControlGrdDocs.gColumn_AddImageButton("", "imagenes/actview.png", "VIEW", auxCommandData, _
        '                                   20, "16px", "16px", clshrcGrdData.enumAlign.coCenter, False, Nothing)
        If pEditMode Then
            auxControlGrdDocs.gJSCommand_Add("EDIT", "", auxControlGrdDocs.DefaultPanel)
            auxScript = "function griddocs_edit(pType,pCOD,pQ_DSC,pViewCommand) {" _
                        & "var auxReturn='&nbsp';" _
                        & "var auxType = parseInt(pType);" _
                        & "if (auxType == " & enumEntities.coEntityDOC_DOCANX & "){" _
                            & "auxReturn ='<u><span style=cursor:pointer; onclick=' + String.fromCharCode(39) + '" & auxControlGrdDocs.gJSCommand_GetCall("EDIT", auxCommandData) & "' + String.fromCharCode(39) + ' >" _
                            & "<img src=imagenes/actmod.png border=0 width=16px >'" _
                            & "+ '</a></u>';};" _
                        & "return auxReturn;" _
                        & "}"
            auxFormatter = "griddocs_edit({#CURRENTROW_Q_TYPE#},{#CURRENTROW_COD#},{#CURRENTROW_Q_DSC#},{#CURRENTROW_CMD_EDIT#})"
            auxControlGrdDocs.gColumn_Add("", 5, "cmd_edit", clshrcGrdData.enumAlign.coLeft, False, auxFormatter, auxScript, False)

            auxControlGrdDocs.gJSCommand_Add("DELETE", "", auxControlGrdDocs.DefaultPanel)
            auxScript = "function griddocs_del(pType,pCOD,pQ_DSC,pViewCommand) {" _
                        & "var auxReturn='&nbsp';" _
                        & "var auxType = parseInt(pType);" _
                        & "if (auxType == " & enumEntities.coEntityDOC_DOCANX & "){" _
                            & "auxReturn ='<u><span style=cursor:pointer; onclick=' + String.fromCharCode(39) + '" & auxControlGrdDocs.gJSCommand_GetCall("DELETE", auxCommandData) & "' + String.fromCharCode(39) + ' >" _
                            & "<img src=imagenes/actdel.png border=0 width=16px >'" _
                            & "+ '</a></u>';};" _
                        & "return auxReturn;" _
                        & "}"
            auxFormatter = "griddocs_del({#CURRENTROW_Q_TYPE#},{#CURRENTROW_COD#},{#CURRENTROW_Q_DSC#},{#CURRENTROW_CMD_DELETE#})"
            auxControlGrdDocs.gColumn_Add("", 5, "cmd_delete", clshrcGrdData.enumAlign.coLeft, False, auxFormatter, auxScript, False)
        End If
        'auxControlGrdDocs.gColumn_AddImageButton("", "imagenes/actmod.png", "EDIT", auxCommandData, _
        '                                    20, "16px", "16px", clshrcGrdData.enumAlign.coCenter, False, Nothing)
        'auxControlGrdDocs.gColumn_AddImageButton("", "imagenes/actdel.png", "DELETE", auxCommandData, _
        '                                    20, "16px", "16px", clshrcGrdData.enumAlign.coCenter, False, Nothing)
        'End If
        ' auxControlGrdDocs.gColumn_AddAsHidden("q_type")

        auxControlGrdDocs.gColumn_AddAsHidden("doctipcod")
        auxControlGrdDocs.gColumn_AddAsHidden("dochtml")
        auxControlGrdDocs.gColumn_AddAsHidden("doc")
        auxControlGrdDocs.gColumn_AddAsHidden("tipo")
        auxControlGrdDocs.gColumn_AddAsHidden("link")
        auxControlGrdDocs.gColumn_AddAsHidden("cod")
        auxControlGrdDocs.gColumn_AddAsHidden("q_type")
        auxControlGrdDocs.gColumn_AddAsHidden("q_group")
        auxControlGrdDocs.gColumn_AddAsHidden("q_parent")
        'auxClientCon.gObjectTmp_Upload(auxControlGrdDocs, auxControlGrdDocsID)
        ' Else
        'auxControlGrdDocs = auxClientCon.gObjectTmp_Download(auxControlGrdDocsID)
        'auxControlGrdDocs.gJS_ControlReady()
        Return auxControlGrdDocs
    End Function
    Private Function gpanelDOCUMENTS_GetDT(ByVal pCod As Integer, _
                                ByVal pHstGenCod As Integer, _
                                ByVal pDftGenCod As String) As DataTable
        Dim auxSelectCommand As String = ""

        If pHstGenCod > 0 Then
            auxSelectCommand = "SELECT DOC_DOCANX.cod as q_cod,DOC_DOCANX.dsc as q_dsc,NULL,DOC_DOCANX.doctipcod as sup," & enumEntities.coEntityDOC_DOCANX & ",DOC_DOCANX.cod,DOC_DOCANX.dsc,DOC_DOCANX.link,DOC_DOCANX.baja,DOC_DOCANX.doc,DOC_DOCANX.doctipcod,DOC_DOCANX.tipo,DOC_DOCANX.dochtml,DOCTIP.dsc as doctipdsc,DOC_DOCANX.doctipcod " _
                & " FROM DOC_DOCANX_HST AS DOC_DOCANX INNER JOIN DOCTIP ON DOC_DOCANX.doctipcod = DOCTIP.cod " _
                & " WHERE DOC_DOCANX.cod = -1 OR ((DOC_DOCANX.baja = 0 OR DOC_DOCANX.baja IS NULL) AND DOC_DOCANX.hsthidgencod= " & pHstGenCod & ")"
        Else
            If pDftGenCod = "" Then
                auxSelectCommand = "SELECT DOC_DOCANX.cod as q_cod,DOC_DOCANX.dsc as q_dsc,NULL,DOC_DOCANX.doctipcod as sup," & enumEntities.coEntityDOC_DOCANX & ",DOC_DOCANX.cod,DOC_DOCANX.dsc,DOC_DOCANX.link,DOC_DOCANX.baja,DOC_DOCANX.doc,DOC_DOCANX.doctipcod,DOC_DOCANX.tipo,DOC_DOCANX.dochtml,DOCTIP.dsc as doctipdsc,DOC_DOCANX.doctipcod  FROM DOC_DOCANX INNER JOIN DOCTIP ON DOC_DOCANX.doctipcod = DOCTIP.cod WHERE  (isnull(DOC_DOCANX.baja,0) = 0 AND DOC_DOCANX.doccod= " & pCod & " )"
            Else
                auxSelectCommand = "SELECT DOC_DOCANX.cod as q_cod,DOC_DOCANX.dsc as q_dsc,NULL,DOC_DOCANX.doctipcod as sup," & enumEntities.coEntityDOC_DOCANX & ",DOC_DOCANX.cod,DOC_DOCANX.dsc,DOC_DOCANX.link,DOC_DOCANX.baja,DOC_DOCANX.doc,DOC_DOCANX.doctipcod,DOC_DOCANX.tipo,DOC_DOCANX.dochtml,DOCTIP.dsc as doctipdsc,DOC_DOCANX.doctipcod " _
                        & ",1 as cmd_view,1 as cmd_edit,1 as cmd_delete" _
                        & " FROM DOC_DOCANX INNER JOIN DOCTIP ON DOC_DOCANX.doctipcod = DOCTIP.cod WHERE (isnull(DOC_DOCANX.baja,0) = 0 AND DOC_DOCANX.doccod= " & pCod & " AND DOC_DOCANX.cod NOT IN (SELECT qdft_cod FROM DOC_DOCANX_DFTREL WHERE dftdidgencod='" & pDftGenCod & "'))" _
                        & " UNION " _
                        & " SELECT DOC_DOCANX.cod as q_cod,DOC_DOCANX.dsc as q_dsc,NULL,DOC_DOCANX.doctipcod as sup," & enumEntities.coEntityDOC_DOCANX & ",DOC_DOCANX.cod,DOC_DOCANX.dsc,DOC_DOCANX.link,DOC_DOCANX.baja,DOC_DOCANX.doc,DOC_DOCANX.doctipcod,DOC_DOCANX.tipo,DOC_DOCANX.dochtml,DOCTIP.dsc as doctipdsc,DOC_DOCANX.doctipcod " _
                        & ",1 as cmd_view,1 as cmd_edit,1 as cmd_delete" _
                        & " FROM DOC_DOCANX_DFT AS DOC_DOCANX INNER JOIN DOCTIP ON DOC_DOCANX.doctipcod = DOCTIP.cod WHERE  (ISNULL(DOC_DOCANX.baja,0) = 0 AND DOC_DOCANX.doccod= " & pCod & " AND dftdidgencod='" & pDftGenCod & "')"
            End If
        End If
        Dim auxDT As DataTable
        Dim auxConn As clsHrcConnClient = CType(Session("conn"), clsHrcConnClient).gComponent_CreateInstance
        auxConn.gConn_Open()
        auxDT = auxConn.gHierarchy_GenerateTable(auxSelectCommand _
                                                        & " ORDER BY DOC_DOCANX.doctipcod", _
                                                       "SELECT cod as q_cod,dsc as q_dsc,NULL,NULL," & enumEntities.coEntityDOCTIP & ",cod,dsc,null,null,null,null,null,null,null,null,null,null,null FROM DOCTIP WHERE cod > 0 ORDER BY cod", _
                                                       "")

        auxConn.gConn_Close()
        Return auxDT
    End Function
    Private Sub gPanelDOCUMENTS_CommandHandler(ByVal pControl As clsHrcJSControlBasic, ByVal pValues As clshrcBagValues)
        Dim auxAction As String = pValues.gValue_Get("ACTION")
        Dim auxCommandName As String = pValues.gValue_Get("ACTION")
        Dim auxError As String = ""
        Dim auxCod As Integer = Val(pValues.gValue_Get("COD"))
        Dim auxHstGenCod As Integer
        Select Case auxAction
            Case "grddata_command"
                Dim auxItemCod As Integer = Val(pControl.Parentcontrol.BagValues.gValue_Get("COD"))
                auxHstGenCod = Val(pControl.Parentcontrol.BagValues.gValue_Get("hstgencod"))
                Dim auxDftGenCod As String = pControl.Parentcontrol.BagValues.gValue_Get("dftgencod")
                Select Case pValues.gValue_Get("COMMANDNAME")
                    Case "GRDDATA_DATABIND"
                        Dim auxDT As DataTable = CType(pControl, clshrcGrdData).gDataTable_Prepare(gpanelDOCUMENTS_GetDT(auxItemCod, auxHstGenCod, auxDftGenCod))
                        CType(pControl, clshrcGrdData).gDataSource_Set(auxDT)
                        pValues.gValue_Add("HRC_RESULT", auxDT)
                    Case "DOC_INSERT_TYPE1"
                        Dim auxDT As DataTable = CType(pControl, clshrcGrdData).gDataTable_Prepare(gpanelDOCUMENTS_GetDT(auxItemCod, auxHstGenCod, auxDftGenCod))
                        pValues.gValue_Add("HRC_RESULT", auxDT)

                End Select
            Case "grddata_panelitem_get", "grddata_panelitem_ins_get"
                Dim auxDFTGenCod As String = pControl.Parentcontrol.Parentcontrol.BagValues.gValue_Get("dftgencod")
                If auxDFTGenCod <> "" Then

                    Dim auxDT As DataTable = Nothing
                    Dim auxClass As New clsCusimDOC
                    Dim auxConn As clsHrcConnClient = auxClass.Conn ' Session("conn")
                    Dim auxClient As New imClientConnection
                    'Dim auxGrid As clshrcGrdData = auxClient.gObjectTmp_Download(pValues.gValue_Get("HRCOBJID"))
                    'Dim auxPanel As clsHrcJSPanel = auxGrid.Panels.Values(CInt(pValues.gValue_Get("HRCPANEL")))
                    Dim auxPanel As clsHrcJSPanel = pControl
                    Dim auxDoc_Tipo As Integer = 1
                    If pValues.Values.IndexOfKey("HRC_COMMANDRESULTS") <> -1 Then
                        auxDoc_Tipo = Val(pValues.gValue_Get("HRC_COMMANDRESULTS").ROWS(0)("TIPO"))
                    End If
                    'auxPanel.gControl_Get("panelgrddoc_tipo").DefaultValue = "'1'"
                    Dim auxControl_Dochtml As clshrcJSTextEditor = auxPanel.gControl_Get("panelgrddoc_dochtml_content") ' auxClient.gObjectTmp_Download(auxDocHTMLCacheID)
                    If auxControl_Dochtml IsNot Nothing Then
                        If auxDoc_Tipo = enumDOC_Tipos.coForm Then
                            auxControl_Dochtml.IsForm = True
                        End If
                        Select Case auxAction
                            Case "grddata_panelitem_ins_get"
                                auxControl_Dochtml.BagValues.gValue_Add("ID", -1)
                                auxControl_Dochtml.BagValues.gValue_Add("CONTENT", Nothing)
                            Case "grddata_panelitem_get"
                                auxControl_Dochtml.BagValues.gValue_Add("ID", pValues.gValue_Get("HRC_COMMANDRESULTS").ROWS(0)("DOCHTML"))
                                auxControl_Dochtml.BagValues.gValue_Add("CONTENT", Nothing)
                                auxConn.gConn_Open()
                                'Dim auxDTReq As DataTable = auxConn.gConn_Query("SELECT reqtipcod,wfwstatus FROM REQ_REQ_DFT " _
                                '            & "WHERE cod=" & pValues.gValue_Get("DOCCOD") & " AND dftdidgencod=" & auxClass.Conn.gFieldDB_GetString(auxDFTGenCod))
                                ' If auxDTReq.Rows.Count <> 0 Then
                                'Dim auxReqtipCod As Integer = auxDTReq.Rows(0)("reqtipcod")
                                'Dim auxWfwStatus As enumWorkflowStep = auxDTReq.Rows(0)("wfwstatus")
                                '  Dim auxReqTipRow As DataRow = hrcEntityDT_REQ_TIP.Select("cod=" & auxReqtipCod)(0)

                                'Dim auxPanels_Enabled As String = ""
                                'If auxClass.Conn.gField_GetBoolean(auxReqTipRow("plantillapaneles"), False) Then
                                '    auxPanels_Enabled = "," & auxWfwStatus.ToString.Replace("coWFWSTPREQ_REQ", "") & ","
                                '    If auxWfwStatus = enumWorkflowStep.coWFWSTPREQ_REQdesarrollo Then
                                '        auxPanels_Enabled &= "ejecucion,"
                                '    End If
                                'End If
                                'auxControl_Dochtml.BagValues.gValue_Add("Panels_Enabled", auxPanels_Enabled)
                                'End If
                        End Select
                        auxClient.gObjectTmp_Upload(auxControl_Dochtml, auxControl_Dochtml.CacheID)
                    End If
                End If
            Case "grddata_panelitem_del"
                Dim auxDFTGenCod As String = pControl.Parentcontrol.Parentcontrol.BagValues.gValue_Get("dftgencod")
                If auxDFTGenCod <> "" Then
                    Dim auxClass As New clsCusimDOC
                    auxClass.Conn.gConn_Open()
                    auxClass.gEntity_DOC_DOCANX_DeleteInDraft(pdftdidgencod:=auxDFTGenCod, pcod:=auxCod)
                    auxClass.Conn.gConn_Close()
                End If


            Case "grddata_panelitem_mod", "grddata_panelitem_ins"
                Dim auxDFTGenCod As String = pControl.Parentcontrol.Parentcontrol.BagValues.gValue_Get("dftgencod")
                If auxDFTGenCod <> "" Then
                    Dim auxConn As clsHrcConnClient '= Session("conn")
                    Dim auxDT As DataTable = Nothing
                    Dim auxClass As New clsCusimDOC
                    auxConn = auxClass.Conn
                    auxConn.gConn_Open()
                    Dim auxDoc_Tipo As Integer = Val(pValues.gValue_Get("TIPO"))
                    Select Case auxAction
                        Case "grddata_panelitem_mod"
                            auxClass.gEntity_DOC_DOCANX_UpdateInDraft(pdftdidgencod:=auxDFTGenCod, _
                                                          pcod:=auxCod)
                        Case "grddata_panelitem_ins"
                            auxError = ""
                            Dim auxDsc As String = pValues.gValue_Get("Q_DSC")
                            If auxDsc = "" Then
                                auxError &= "Ingrese un título."
                            Else
                                Dim auxItemCod As Integer = pValues.gValue_Get("DOCCOD")
                                Dim auxDoctipCod As Integer = auxConn.gField_GetInt(pValues.gValue_Get("DOCTIPCOD"), -1)
                                If auxDoctipCod < 1 Then
                                    auxError &= "Debe seleccionar un tipo."
                                Else
                                    auxCod = auxClass.gEntity_DOC_DOCANX_InsertInDraft(pdoccod:=auxItemCod, pdftdidgencod:=auxDFTGenCod, _
                                                           pdsc:=Nothing, _
                                                           pdoctipcod:=auxDoctipCod, _
                                                           pbaja:=False, plink:=Nothing, pdoc:=Nothing, _
                                                           pdochtml:=-1, ptipo:=2)
                                End If

                                'auxClass.gLoginPreference_SetValue(enumPrefType.coReqDocInsert, "doctipcod", auxDoc_Tipo)
                                'auxClass.gPreference_Save(auxClass.Sec.CurrentSidCod, enumPrefType.coReqDocInsert)
                                pValues.gValue_Add("COD", auxCod)
                            End If
                            
                    End Select
                    If auxError = "" Then
                        Dim auxClient As New imClientConnection
                        'Dim auxGrid As clshrcGrdData = auxClient.gObjectTmp_Download(pValues.gValue_Get("HRCOBJID"))
                        'Dim auxPanel As clsHrcJSPanel = auxGrid.Panels.Values(CInt(pValues.gValue_Get("HRCPANEL")))
                        Dim auxPanel As clsHrcJSPanel = pControl
                        Dim auxControl_Doc As clshrcJSFileUpload = auxPanel.gControl_Get("panelgrddoc_doc_content")
                        Dim auxDoc_BinaryData As clsHrcConnClient.clsBinaryData = auxClient.gObjectTmp_Download(auxControl_Doc.FileTmpID)

                        If auxDoc_Tipo = enumDOC_Tipos.coLink Then
                        ElseIf auxDoc_Tipo = enumDOC_Tipos.coAnexo And auxDoc_BinaryData IsNot Nothing Then
                            auxConn.gConn_FileToBLOB(auxDoc_BinaryData.Filename, "", auxDoc_BinaryData.Content, "UPDATE DOC_DOCANX_DFT SET link='',doc={#FILECONTENT#} WHERE cod = " & auxCod & " AND dftdidgencod=" & auxClass.Conn.gFieldDB_GetString(auxDFTGenCod))
                        ElseIf auxDoc_Tipo = enumDOC_Tipos.coHTML Then
                            Dim auxControl_Dochtml As clshrcJSTextEditor = auxPanel.gControl_Get("panelgrddoc_dochtml_content") ' auxClient.gObjectTmp_Download(auxDocHTMLCacheID)
                            auxConn.gConn_FileToBLOB(auxControl_Dochtml.Title.Replace(" ", "") & ".html", "", _
                                                     Encoding.Default.GetBytes(auxControl_Dochtml.BagValues.gValue_Get("CONTENT")), _
                                                     "UPDATE DOC_DOCANX_DFT SET dochtml={#FILECONTENT#} WHERE cod = " & auxCod & " AND dftdidgencod=" & auxClass.Conn.gFieldDB_GetString(auxDFTGenCod))
                        ElseIf auxDoc_Tipo = enumDOC_Tipos.coForm Then
                            Dim auxClientConn As New imClientConnection
                            Dim auxControl_Dochtml As clshrcJSTextEditor = auxPanel.gControl_Get("panelgrddoc_dochtml_content") ' auxClient.gObjectTmp_Download(auxDocHTMLCacheID)
                            auxConn.gConn_FileToBLOB(auxControl_Dochtml.Title.Replace(" ", "") & ".html", "", _
                                                     Encoding.Default.GetBytes(auxControl_Dochtml.BagValues.gValue_Get("CONTENT")), _
                                                     "UPDATE DOC_DOCANX_DFT SET dochtml={#FILECONTENT#} WHERE cod = " & auxCod & " AND dftdidgencod=" & auxClass.Conn.gFieldDB_GetString(auxDFTGenCod))
                        End If
                        auxClass.Conn.gConn_Close()
                    End If

                    End If

        End Select
        If auxError <> "" Then
            pValues.gValue_Add("HRC_RESULTS_ERROR", auxError)
        End If
    End Sub
  
    Private Sub gFormTRO_CommandHandler(ByVal pControl As clsHrcJSControlBasic, ByVal pValues As clshrcBagValues)
        Dim auxAction As String = pValues.gValue_Get("ACTION")
        Dim auxCommandName As String = pValues.gValue_Get("COMMANDNAME")
        If pControl.ControlID = "GRIDDATA" And auxCommandName = "GRDDATA_DATABIND" Then
            Dim auxwhere As String = ""
            Dim auxclass As New clsCusimDOC
            Dim auxConn As clsHrcConnClient = auxclass.Conn
            auxConn.gConn_Open()
            Dim auxClient As New imClientConnection
            Dim auxPanel As clsHrcJSPanel = pControl.Parentcontrol
            Dim auxValues As clshrcBagValues = auxPanel.gFieldData_GetValues
            For Each auxValue As KeyValuePair(Of String, Object) In auxValues.Values
                Select Case auxValue.Key
                    Case "DSC"
                        auxwhere &= " AND dsc LIKE '%" & auxValue.Value.ToString & "%'"
                End Select
            Next
            Dim auxDT As DataTable = Nothing
            auxDT = auxConn.gConn_Query("SELECT cod,dsc" _
                                        & " FROM DOC_TRO " _
                                           & " WHERE cod > 0 " _
                                           & " AND (baja {#ISNULL#} OR baja ={#FALSE#}) " _
                                           & " AND (custom {#ISNULL#} OR custom ={#FALSE#}) " _
                                           & auxwhere _
                                           & " ORDER BY dsc")
            auxConn.gConn_Close()
            If auxDT IsNot Nothing Then
                'auxDT = CType(pControl, clshrcGrdData).gDataTable_Prepare(auxDT)
                'CType(pControl, clshrcGrdData).gDataSource_Set(auxDT)
                pValues.gValue_Add("HRC_RESULT", auxDT)
            End If
        End If

    End Sub
    Private Function gFormTRO_Get(ByVal pClass As clsCusimDOC, _
                            ByVal pHrcContext As clsHrcJSContext, _
                            ByVal pQueryValues As clshrcBagValues, _
                            ByVal pIsAdmin As Boolean) As clshrcBagValues
        Dim auxClass As New clsCusimDOC
        auxClass.Conn.gConn_Open()
        'Dim auxPrincipalPanel As clsHrcJSPanel
        'auxPrincipalPanel = gPanelRoles_Get(auxClass, pHrcContext, Nothing, True, -1, "", False)

        Dim auxHTML As New clsHrcCodeHTML
        'm_view = Val(auxQueryBagValues.gValue_Get("_view_"))
        'If m_view < 1 Then
        '    Exit Function
        'End If
        Dim auxClientCon As New imClientConnection
        Dim auxGrpCodList As New List(Of Integer)
        auxGrpCodList.Add(coGroupDocumentadorAdministradores)
        auxGrpCodList.Add(coGroupDocumentadorEditores)
        auxGrpCodList.Add(coGroupIDAdministradores)
        Dim auxGestion As Boolean = auxClass.Sec.gMember_IsInGroupByID(auxGrpCodList)
        If pIsAdmin = False And auxGestion = False Then
            Response.Redirect(hrcFormInitial)
        End If
        'Dim auxIsAdmin As Boolean = auxClass.Conn.gField_GetBoolean(Session("isadmin"))
        Dim auxMode As enumWindowMode = enumWindowMode.coNormal
        auxMode = Val(pQueryValues.gValue_Get("_mode_"))
        Dim auxPrincipalPanel As clsHrcJSPanel
        Dim auxScript As String = ""

        auxPrincipalPanel = New clsHrcJSPanel("principal_panel", "", pHrcContext.ButtonCSS, clsHrcJSPanel.enumPanelMode.coNoButton)
        Dim auxformID As String
        auxPrincipalPanel.gControl_SetHrcContext(pHrcContext)
        auxformID = auxPrincipalPanel.CacheID
        auxPrincipalPanel.BagValues.gValue_Add("mode", auxMode)
        auxPrincipalPanel.BagValues.gValue_Add("formid", auxformID) 'lo guarda para tenerlo disponible en PDF/vista previa
        auxPrincipalPanel.gPanelTitle_Enabled()
        auxPrincipalPanel.PanelTitle.gValue_Set("<img src=imagenes/icon00000035.png width=24px />" _
                                     & "Plantillas de roles")
        auxClientCon.gObjectTmp_UploadinGlobal(auxPrincipalPanel, auxformID)

        auxClass.Conn.gConn_Open()
        'Grilla
        Dim auxGeneralWhere As String = ""
        'Dim auxDT As DataTable
        'auxDT = auxClass.Conn.gConn_Query("SELECT cod,dsc FROM EMP WHERE cod > 0 " _
        '                                  & " WHERE dsc LIKE '%{#TXTFILTER_DSC#}'" _
        '                                  & " ORDER BY dsc")
        Dim auxGridData As clshrcGrdData
        auxGridData = New clshrcGrdData("griddata", "", pHrcContext)
        auxGridData.FieldData = "cod"
        auxGridData.gPager_EnableVirtual(20)

        'fILTROS
        Dim auxClearScript As String = ""
        Dim auxControlFilterDsc As New clshrcJSTextBox("txtfilter_dsc", "Nombre", "DSC", "form-control", 100, "280px", "", False, "", "")
        auxControlFilterDsc.ServerStateMantain = True
        auxPrincipalPanel.gControls_Add(auxControlFilterDsc)
        auxClearScript &= auxControlFilterDsc.gJS_Value_Set("''", True) & ";"

        
        Dim auxButtonSearch As New clsHrcJSButton("cmdSearch", "Buscar", pHrcContext.ButtonCSS)
        auxButtonSearch.EventOnClick = auxGridData.gJS_GetReloadCode
        auxPrincipalPanel.gControls_Add(auxButtonSearch)


        Dim auxButtonClear As New clsHrcJSButton("cmdclear", "Limpiar", pHrcContext.ButtonCSS)
        auxButtonClear.EventOnClick = auxClearScript
        auxPrincipalPanel.gControls_Add(auxButtonClear)

        'FILTROS-FIN
        auxGridData.DefaultPanel_Enable("SELECT cod,dsc " _
                                        & " FROM DOC_TRO WHERE cod={#COD#}", _
                                        "UPDATE DOC_TRO SET dsc='{#DSC#}'" _
                                        & " WHERE cod={#COD#}", _
                                        ".", ".")
        'auxGridData.gDataSource_Set(auxDT)
        auxPrincipalPanel.gControls_Add(auxGridData)
        'Dim auxPanelHTML As String = _
        '        "<div class=box>{#PANEL.BUTTONNOK#}{#PANEL.BUTTONOK#}{#PANEL.LABELERROR#}</div>" _
        '       & "<div class=box>" _
        '       & "<table class=form>" _
        '       & "<tr><td>Código</td><td>{#CONTROLS.PANEL_COD#}</td></tr>" _
        '       & "<tr><td>Título</td><td>{#CONTROLS.PANEL_DSC#}</td></tr>" _
        '       & "</table>" _
        '       & "</div>"
        'auxGridData.DefaultPanel.gHTML_SetTemplate(auxPanelHTML, "", "", "", pHrcContext.ButtonCSS)

        auxHTML.DateFormatEnabled = False
        auxGridData.RaiseEventBlockDataBoundONEveryRequest = True
        AddHandler auxGridData.EventCommandHandler, AddressOf gFormTRO_CommandHandler ' gPanelRoles_CommandHandler

        Dim auxScriptGeneral As String = ""
        auxScriptGeneral &= "function gTro_ShowModal(pWindow){" _
            & auxHTML.gJS_OpenModalDialog("grd_modal", "pWindow", False, 800, 500) & ";" _
            & "}"
        Dim auxCommandData As New clshrcBagValues
        auxCommandData.gValue_Add("COD", "{#CURRENTROW_COD#}")
        auxGridData.GridWidth = "100%"
        Dim auxFormatter As String = ""
        auxGridData.gColumn_Add("Código", 6, "cod", clshrcGrdData.enumAlign.coRight, True, "", "", False, hrcDataTypes.coNumberType)
        auxGridData.gColumn_Add("Nombre", -1, "DSC", clshrcGrdData.enumAlign.coLeft, False, "", "", False)

        auxScript = "function griddata_formatcol1(pCod) {" _
                    & "var auxReturn='&nbsp';" _
                    & "auxReturn ='<u><span style=cursor:pointer; onclick=gTro_ShowModal(""cfrmdocumentos1_det.aspx?_view_=7&_closea_=1&_mode_=0&param1=""+""' + pCod + '"",600,500) >';" _
                    & "auxReturn +='<img src=""imagenes/actview.png"" width=14px class=hrcthemeimage ></span>';" _
                    & "auxReturn +='</u>';" _
                    & "return auxReturn;" _
                    & "}"
        auxFormatter = "griddata_formatcol1({#CURRENTROW_COD#})"

        auxGridData.gColumn_Add("", 3, "", clshrcGrdData.enumAlign.coCenter, False, auxFormatter, auxScript, False)
        auxScript = "function griddata_formatcol2(pCod) {" _
                           & "var auxReturn='&nbsp';" _
                           & "auxReturn ='<u><span style=cursor:pointer; onclick=gTro_ShowModal(""cfrmdocumentos1_det.aspx?_view_=7&_closea_=1&_mode_=2&param1=""+""' + pCod + '"") >';" _
                           & "auxReturn +='<img src=""imagenes/actmod.png"" width=14px class=hrcthemeimage ></span>';" _
                           & "auxReturn +='</u>';" _
                           & "return auxReturn;" _
                           & "}"
        auxFormatter = "griddata_formatcol2({#CURRENTROW_COD#})"
        auxGridData.gColumn_Add("", 3, "", clshrcGrdData.enumAlign.coCenter, False, auxFormatter, auxScript, False)

        auxScript = "function griddata_formatcol_Del(pCod) {" _
                    & "var auxReturn='&nbsp';" _
                    & "auxReturn ='<u><span style=cursor:pointer; onclick=gTro_ShowModal(""cfrmdocumentos1_det.aspx?_view_=7&_closea_=1&_mode_=3&param1=""+""' + pCod + '"") >';" _
                    & "auxReturn +='<img src=""imagenes/actdel.png"" width=14px class=hrcthemeimage ></span>';" _
                    & "auxReturn +='</u>';" _
                    & "return auxReturn;" _
                    & "}"
        auxFormatter = "griddata_formatcol_Del({#CURRENTROW_COD#})"
        auxGridData.gColumn_Add("", 3, "", clshrcGrdData.enumAlign.coCenter, False, auxFormatter, auxScript, False)


        Dim auxGrid_Cod As New clsHrcJSHidden("panel_cod", "cod")
        auxGridData.DefaultPanel.gControls_Add(auxGrid_Cod)

        Dim auxGrid_Dsc As New clshrcJSTextBox("panel_dsc", "Título", "dsc", pHrcContext.ControlTextBoxCSS, 200, "300px", "", False, "", "")
        auxGrid_Dsc.IsRequired = True
        auxGridData.DefaultPanel.gControls_Add(auxGrid_Dsc)
        Dim auxPanelTRO As clsHrcJSPanel
        auxPanelTRO = gPanelRoles_Get(auxClass, pHrcContext, auxPrincipalPanel, True, -1, "", False, "")
        auxGridData.DefaultPanel.gControls_Add(auxPanelTRO.gControl_Get("grdtro"))

        auxGridData.ToolbarButtonAdd.EventOnClick = "gTro_ShowModal('cfrmdocumentos1_det.aspx?_view_=7&_mode_=1&_closea_=1')"
        'auxGridData.gToolbar_AddButtonCommand("CUSTOMNEW", "Agregar", "")
        'auxGrdTro.gJSCommand_Add("CUSTOMNEW", auxHTML.gJS_OpenModalWindow("cfrmdocumentos1_det.aspx?_view_=7&_mode_=1"))

        auxGridData.gJS_ControlReady()
        auxPrincipalPanel.gJS_ControlReady()
        Dim auxReturn As New clshrcBagValues
        auxReturn.gValue_Add("CONTENT", auxPrincipalPanel.gControl_GetBodyDefinition)
        auxReturn.gValue_Add("SCRIPT", auxScriptGeneral _
                             & auxPrincipalPanel.gControl_GetStartupScripts)
        auxReturn.gValue_Add("TITLE", "")
        Return auxReturn
    End Function
    Private Function gFormTRODetail_Get(ByVal pClass As clsCusimDOC, _
                          ByVal pHrcContext As clsHrcJSContext, _
                          ByVal pQueryValues As clshrcBagValues, _
                          ByVal pIsAdmin As Boolean) As clshrcBagValues
        Dim auxClass As New clsCusimDOC
        auxClass.Conn.gConn_Open()
        Dim auxPrincipalPanel As clsHrcJSPanel
     
        Dim auxHTML As New clsHrcCodeHTML
        'm_view = Val(auxQueryBagValues.gValue_Get("_view_"))
        'If m_view < 1 Then
        '    Exit Function
        'End If
        Dim auxClientCon As New imClientConnection
        If pIsAdmin = False Then
            Response.Redirect(hrcFormInitial)
        End If
        'Dim auxIsAdmin As Boolean = auxClass.Conn.gField_GetBoolean(Session("isadmin"))
        Dim auxWinMode As enumWindowMode = enumWindowMode.coNormal
        auxWinMode = Val(pQueryValues.gValue_Get("_winmode_"))
        Dim auxMode As enumActionType = Val(pQueryValues.gValue_Get("_mode_"))
        Dim auxTroCod As Integer = Val(pQueryValues.gValue_Get("param1"))
        Dim auxScript As String = ""
        Dim auxDfdGenCod_Tro As String
         Select auxMode
            Case enumActionType.coModify, enumActionType.coNew, enumActionType.coNewFromOther
                auxDfdGenCod_Tro = auxClass.gDraft_Create("Borrador", enumEntities.coEntityDOC_TRO)
                Select Case auxMode
                    Case enumActionType.coModify
                        auxClass.gEntity_DOC_TRO_UpdateInDraft(pdftdidgencod:=auxDfdGenCod_Tro, _
                                                                    pcod:=auxTroCod)
                    Case enumActionType.coNew, enumActionType.coNewFromOther
                        auxTroCod = auxClass.gEntity_DOC_TRO_InsertInDraft(pdftdidgencod:=auxDfdGenCod_Tro, pbaja:=False, pdsc:="", pcustom:=False)
                End Select
                
                For Each auxRow As DataRow In auxClass.gTRO_Get(pTroCod:=auxTroCod).Rows
                    auxClass.gEntity_DOC_TROROL_UpdateInDraft(pdftdidgencod:=auxDfdGenCod_Tro, _
                                                        pcod:=auxRow("cod"))
                    auxClass.gEntity_DOC_TROROLEMP_UpdateInDraft(pdftdidgencod:=auxDfdGenCod_Tro, _
                                                        ptrorolcod:=auxRow("cod"))
                    auxClass.gEntity_DOC_TROROLUND_UpdateInDraft(pdftdidgencod:=auxDfdGenCod_Tro, _
                                                        ptrorolcod:=auxRow("cod"))
                    auxClass.gEntity_DOC_TROROLEQU_UpdateInDraft(pdftdidgencod:=auxDfdGenCod_Tro, _
                                                        ptrorolcod:=auxRow("cod"))
                    auxClass.gEntity_DOC_TROROLDYNGRP_UpdateInDraft(pdftdidgencod:=auxDfdGenCod_Tro, _
                                                        ptrorolcod:=auxRow("cod"))
                Next
        End Select
        Dim auxTitle As String = ""
        Select Case auxMode
            Case enumActionType.coViewDetail
                auxTitle = "Ver"
            Case enumActionType.coDelete
                auxTitle = "Borrar"
            Case enumActionType.coNew
                auxTitle = "Nuevo"
            Case enumActionType.coModify
                auxTitle = "Modificar"
        End Select
        auxTitle = "<img src=imagenes/biblioteca_documentos.png width=20px />" & auxTitle

        auxPrincipalPanel = New clsHrcJSPanel("principal_panel", auxTitle, pHrcContext.ButtonCSS, clsHrcJSPanel.enumPanelMode.coOK_NOKEmbebed)
        Select Case auxMode
            Case enumActionType.coViewDetail
                auxPrincipalPanel.ButtonOK.Visible = False
            Case Else
                auxPrincipalPanel.JSConfirmed = auxHTML.gJS_CloseModalDialog & ";"
        End Select
        If pQueryValues.gValue_Get("_closea_") = "1" Then
            auxPrincipalPanel.ButtonNOK.EventOnClick = auxHTML.gJS_CloseModalDialog & ";"
        End If
        auxPrincipalPanel.PanelTitle.gValue_Set(auxTitle)
        auxPrincipalPanel.BagValues.gValue_Add("principal_panel", auxPrincipalPanel)
        AddHandler auxPrincipalPanel.EventCommandHandler, AddressOf gPanelRoles_CommandHandler
        Dim auxformID As String
        auxPrincipalPanel.gControl_SetHrcContext(pHrcContext)
        auxformID = auxPrincipalPanel.CacheID
        auxPrincipalPanel.BagValues.gValue_Add("mode", auxWinMode)
        auxPrincipalPanel.BagValues.gValue_Add("formid", auxformID) 'lo guarda para tenerlo disponible en PDF/vista previa
        auxPrincipalPanel.BagValues.gValue_Add("dftgencod_TRO", auxDfdGenCod_Tro)
        auxPrincipalPanel.BagValues.gValue_Add("trocod", auxTroCod)
        auxPrincipalPanel.CSSCell = ""
        auxClientCon.gObjectTmp_UploadinGlobal(auxPrincipalPanel, auxformID)
        Dim auxDT As DataTable
        Dim auxDTValues_Old As clshrcBagValues
        auxDTValues_Old = New clshrcBagValues
        Dim auxControlDsc_Edit As Boolean = False
        Dim auxControlTro_Edit As Boolean = False
        Select Case auxMode
            Case enumActionType.coModify, enumActionType.coNew, enumActionType.coNewFromOther
                auxControlDsc_Edit = True
                auxControlTro_Edit = True
        End Select
        auxClass.Conn.gConn_Open()
        Select Case auxMode
            Case enumActionType.coModify, enumActionType.coDelete, enumActionType.coViewDetail, enumActionType.coNewFromOther
                auxDT = auxClass.Conn.gConn_Query("SELECT * FROM DOC_TRO" _
                                                  & " WHERE cod= " & auxTroCod)
                If auxDT.Rows.Count <> 0 Then
                    auxDTValues_Old.gValue_Add(auxClass.Conn.gField_GetBagValuesFromArray(auxDT.Rows(0).ItemArray, auxDT.Columns))
                End If
        End Select



        If auxControlDsc_Edit Then
            Dim auxControl_Dsc As New clshrcJSTextBox("panel_dsc", "Nombre", "dsc", pHrcContext.ControlTextBoxCSS, 200, "300px", "", False, "", "")
            auxControl_Dsc.IsRequired = True
            auxControl_Dsc.gValue_Set(auxDTValues_Old.gValue_Get("dsc"))
            auxPrincipalPanel.gControls_Add(auxControl_Dsc)
        Else
            Dim auxControl_Dsc As New clsHrcJSLabel("panel_dsc", "Nombre", "dsc", "", "300px")
            auxControl_Dsc.gValue_Set(auxDTValues_Old.gValue_Get("dsc"))
            auxPrincipalPanel.gControls_Add(auxControl_Dsc)
        End If

        Dim auxPanelTRO As clsHrcJSPanel
        auxPanelTRO = gPanelRoles_Get(auxClass, pHrcContext, auxPrincipalPanel, auxControlTro_Edit, auxTroCod, auxDfdGenCod_Tro, False, "")


        auxPrincipalPanel.gControls_Add(auxPanelTRO)

        auxPrincipalPanel.gJS_ControlReady()
        Dim auxReturn As New clshrcBagValues
        auxReturn.gValue_Add("CONTENT", auxPrincipalPanel.gControl_GetBodyDefinition)
        auxReturn.gValue_Add("SCRIPT", auxPrincipalPanel.gControl_GetStartupScripts)
        auxReturn.gValue_Add("TITLE", "")
        Return auxReturn
    End Function
    Private Function gPanelRoles_Get(ByVal pClass As clsCusimDOC, _
                               ByVal pHrcContext As clsHrcJSContext, _
                               ByVal pPrincipalPanel As clsHrcJSPanel, _
                               ByVal pEditMode As Boolean, _
                               ByVal pTroCod As Integer, _
                               ByVal pDfdGenID As String, _
                               ByVal pCopyTROVisible As Boolean, _
                               ByVal pHTMLHeader As String) As clsHrcJSPanel
        Dim auxPanel As clsHrcJSPanel = Nothing
        Dim auxPanel_Mode As clsHrcJSPanel.enumPanelMode = clsHrcJSPanel.enumPanelMode.coNoButton  ' = clsHrcJSPanel.enumPanelMode.coOK_NOK
        auxPanel = New clsHrcJSPanel("pnlpanel_pnltro", "Roles", pHrcContext.ButtonCSS, auxPanel_Mode)
        AddHandler auxPanel.EventCommandHandler, AddressOf gPanelRoles_CommandHandler
        auxPanel.BagValues.gValue_Add("principal_panel", pPrincipalPanel)
        auxPanel.TooltipCSS = "obs-controles"
        auxPanel.ServerRaiseEvents = True
        'auxPanel.Width = "600px"
        'auxPanel.Height = "500px"
        '  auxPanel.Visible = False
        auxPanel.ServerStateMantain = True
        'auxPanel.ZIndex = 2020
        auxPanel.gHTML_SetTemplate("<table cellpadding=""0"" cellspacing=""0"" style=""width:100%;""> " _
                            & pHTMLHeader _
                            & "<tr>" _
                            & "<td style=""border-bottom: 1px solid gray;"">" _
                                    & "<table cellpadding=""0"" cellspacing=""0"" ><tr><td>{#PANEL.BUTTONNOK#}</td>" _
                                    & "<td >{#PANEL.BUTTONOK#}</td>" _
                                    & "<td >{#PANEL.BUTTONS#}</td>" _
                                    & "<td width=400px>{#CONTROLS.SECMODE#}</td>" _
                                    & "<td width=5px></td>" _
                                    & "<td><div id=panel_trocopy >" _
                                        & "<table cellpadding=""0"" cellspacing=""0""><tr>" _
                                        & "<td style=""border-left:1px solid gray"" width=10px></td>" _
                                        & "<td>{#CONTROLS.CMBTROCOPY.TITLE#}</td>" _
                                        & "<td>{#CONTROLS.CMBTROCOPY#}</td>" _
                                        & "<td>{#CONTROLS.PNLPANEL_TRO_BUTTON#}</td>" _
                                        & "<td>{#CONTROLS.PNLPANEL_TROCLEAR_BUTTON#}</td>" _
                                        & "</div></tr></table>" _
                                    & "</td>" _
                                    & "<td>{#PANEL.LABELERROR#}</td></tr></table>" _
                            & "</td>" _
                            & "</tr>" _
                            & "<tr>" _
                            & "<td style=height:100px >{#CONTROLS.GRDTRO#}" _
                            & "</td>" _
                            & "</tr>" _
                            & "</table>", "", "", "", pHrcContext.ButtonCSS)
        Dim auxScript As String = ""
        Dim auxContent As String = ""


        'GRDHITOS
        ' auxGrdHitosView = False
        Dim auxGrdHitosView As Boolean = True
        Dim auxClientCon As New imClientConnection
        Dim auxSelect As String = ""
        Dim auxConn As clsHrcConnClient = pClass.Conn
        Dim auxFormatter As String = ""
        If auxGrdHitosView Then

            Dim auxGridTRO As clshrcGrdData
            Dim auxControlGrdRecItemID As String = ""
            auxControlGrdRecItemID = auxConn.gField_GetUniqueID
            Dim auxQuery As String
            auxQuery = pClass.gQuery_TRO_GetWidthDraft(pDfdGenID, pTroCod)
            auxGridTRO = New clshrcGrdData("grdTRO", auxControlGrdRecItemID, "", _
                                   "hrcgrdData.ashx", _
                                    pClass.Conn, auxQuery, "cod", -1, True, False, "", "")
            'auxGridTRO = New clshrcGrdData("grdtro", auxControlGrdRecItemID, "", _
            '                    "hrcgrdData.ashx", Nothing, "", True, False)
            'auxControlRecItem.BagValues.gValue_Add("editionmode", auxeditionMode)
            auxGridTRO.TreeGridLoadOnRequest = True
            auxGridTRO.RaiseEventBlockDataBoundONEveryRequest = True
            auxGridTRO.BagValues.gValue_Add("principal_panel", pPrincipalPanel)

            AddHandler auxGridTRO.EventCommandHandler, AddressOf gPanelRoles_CommandHandler

            'auxGridTRO.GridWidth = "500px"
            ' auxGridTRO.GridHeight = "400px"
            auxGridTRO.HideColumnHeaders = False
            auxGridTRO.ButtonCSS = pHrcContext.ButtonCSS
            auxGridTRO.TooltipCSS = "obs-controles"
            auxSelect = pClass.gQuery_TRO_GetWidthDraft(pDfdGenID, pTroCod, " AND DOC_TROROL.cod ={#COD#} ")
            Dim auxCommandInsert As String = ""
            Dim auxCommandDelete As String = ""
            Dim auxCommandUpdate As String = ""
            If pEditMode Then
                auxCommandUpdate = "."
                auxCommandInsert = "."

            End If

            auxGridTRO.DefaultPanel_Enable(auxSelect, _
                            auxCommandUpdate _
                        , auxCommandInsert, _
                        auxCommandDelete)
            auxGridTRO.ColumnName_Cod = "cod"
            If pEditMode Then
                auxGridTRO.ToolbarButtonAdd.Title = "Agregar recurso"
            End If
            'auxControlRecItem.ColumnName_Type = "ent_type"
            'auxGridTRO.DefaultPanel.Width = "600px"
            auxGridTRO.DefaultPanel.Height = "500px"
            auxGridTRO.DefaultPanel.ZIndex = 10025
            Dim auxCommandData As New clshrcBagValues ' SortedList(Of String, String)
            auxCommandData.gValue_Add("COD", "{#CURRENTROW_COD#}")

            auxGridTRO.gQueryParameter_Add("TROCOD", pTroCod)
            auxGridTRO.gQueryParameter_Add("dftdidgencod", "$(""#" & pDfdGenID & """).val()")

            auxGridTRO.gColumn_AddAsHidden("cod")
            auxGridTRO.gColumn_AddAsHidden("ROLCOD")
            Dim auxnewRow As DataRow
            'PANEL EMP
            Dim auxDefaultPanel As clsHrcJSPanel = auxGridTRO.DefaultPanel
            '  auxControlRecItem.ToolbarControls.gValue_Get("pnl_" & auxControlRecItem.ControlID & "_buttonadd")
            auxDefaultPanel.CommandView = auxSelect
            '    & " AND REQ_PREREC.cod = {#COD#}"
            auxDefaultPanel.BagValues.gValue_Add("principal_panel", pPrincipalPanel)
            AddHandler auxDefaultPanel.EventCommandHandler, AddressOf gPanelRoles_CommandHandler
            Dim auxLabelCod As New clsHrcJSHidden("panel_codigo", "cod")
            auxDefaultPanel.gControls_Add(auxLabelCod)

            Dim auxDTRolGrp As DataTable = Nothing


            Dim auxControlPanel_Rol As clshrcJSComboBox
            auxDTRolGrp = New DataView(hrcEntityDT_DOC_ROL, "cod>0", "orden", DataViewRowState.CurrentRows).ToTable
            auxControlPanel_Rol = New clshrcJSComboBox("pnlpanel_rol", "Rol", "ROLCOD", "form-control", auxDTRolGrp, "300px", "50px")
            auxControlPanel_Rol.DisplayMode = clshrcJSComboBox.enumDisplayMode.DropDown
            auxControlPanel_Rol.DefaultValue = enumUND_Roles.coResponsable
            auxControlPanel_Rol.gValue_Set(enumUND_Roles.coResponsable)
            'auxControlPanel_Rol.Width = "250px"
            auxControlPanel_Rol.Title = "Rol en la planilla"
            ' auxControlPanel_Rol.ServerStateMantain = True
            auxControlPanel_Rol.FieldData = "ROLCOD"

            '--
            Dim auxControlPanel_EMP As clshrcObjectExplorer
            auxControlPanel_EMP = New clshrcObjectExplorer("pnlpanel_emp", "hrcGrdData.ashx", Nothing, pClass.Conn, auxClientCon)
            'auxControlPanel_EMP.Title = "Colaborador"
            auxControlPanel_EMP.SelectionLimit = 10
            auxControlPanel_EMP.gAutosuggest_Enabled("(SELECT EMP.cod as cod,EMP.dsc as dsc," & enumEntities.coEntityEMP & " as q_type,'Colaborador' " _
                                                   & " FROM EMP  WHERE ( (EMP.dsc LIKE '%{#HRCDSC#}%' ) AND (EMP.baja = 0 OR EMP.baja  IS NULL)) AND EMP.cod >= 1" _
                                                   & ")" _
                                                   & " ORDER BY dsc,q_type")
            'No quitarlos ya agregados porque puede ser responsable y miembro
            '& " AND EMP.cod NOT IN (SELECT empcod FROM REQ_PRERECEMP WHERE cod IN (SELECT cod FROM REQ_PREREC WHERE precod IN (SELECT precod FROM REQ_POO WHERE (REQ_POO.baja = 0 OR REQ_POO.baja  IS NULL))))" _
            auxControlPanel_EMP.FieldData = "empcod"

            Dim auxControlPanel_UND As clshrcObjectExplorer
            auxControlPanel_UND = New clshrcObjectExplorer("pnlpanel_und", "hrcGrdData.ashx", Nothing, pClass.Conn, auxClientCon)
            'auxControlPanel_UND.Title = "Unidad"
            auxControlPanel_UND.SelectionLimit = 1
            auxControlPanel_UND.gAutosuggest_Enabled("(SELECT UND.cod as cod,UND.dsc as dsc," & enumEntities.coEntityUND & " as q_type,'Unidad' " _
                                                     & " FROM UND  " _
                                                     & " WHERE ( (UND.dsc LIKE '%{#HRCDSC#}%' ) AND (UND.baja = 0 OR UND.baja  IS NULL)) AND UND.cod >= 1)" _
                                                    & " ORDER BY dsc,q_type")
            auxControlPanel_UND.FieldData = "undcod"

            auxDTRolGrp = New DataView(hrcEntityDT_ROLGRP, "cod =" & enumUND_Roles.coMiembro _
                                       & " OR cod =" & enumUND_Roles.coSuperior _
                                       & " OR cod =" & enumUND_Roles.coResponsable _
                                       & " OR cod =" & enumUND_Roles.coEditorDocs _
                                       , "dsc", DataViewRowState.CurrentRows).ToTable
            auxnewRow = auxDTRolGrp.NewRow
            auxnewRow("cod") = -1
            auxnewRow("dsc") = "Automatico"
            auxDTRolGrp.Rows.Add(auxnewRow)
            Dim auxControlPanel_UndRol As New clshrcJSComboBox("pnlpanel_undrol", "Rol", "undrolgrpcod", "form-control", auxDTRolGrp, "300px", "50px")
            auxControlPanel_UndRol.DisplayMode = clshrcJSComboBox.enumDisplayMode.DropDown
            auxControlPanel_UndRol.DefaultValue = -1
            auxControlPanel_UndRol.gValue_Set(-1)

            auxControlPanel_UndRol.Title = "Rol en la unidad"
            auxControlPanel_UndRol.FieldData = "undROLGRPCOD"


            Dim auxControlPanel_EQU As clshrcObjectExplorer
            auxControlPanel_EQU = New clshrcObjectExplorer("pnlpanel_EQU", "hrcGrdData.ashx", Nothing, pClass.Conn, auxClientCon)
            auxControlPanel_EQU.SelectionLimit = 10
            auxControlPanel_EQU.gAutosuggest_Enabled("(SELECT EQU.cod as cod,EQU.dsc as dsc," & enumEntities.coEntityDOC_EQU & " as q_type,'Equipo' " _
                                                     & " FROM DOC_EQU AS EQU  " _
                                                     & " WHERE ( (EQU.dsc LIKE '%{#HRCDSC#}%' ) AND (EQU.baja = 0 OR EQU.baja  IS NULL)) AND EQU.cod >= 1)" _
                                                    & " ORDER BY dsc,q_type")
            auxControlPanel_EQU.FieldData = "equcod"

            Dim auxControlPanel_DYNGRP As clshrcObjectExplorer
            auxControlPanel_DYNGRP = New clshrcObjectExplorer("pnlpanel_dyngrp", "hrcGrdData.ashx", Nothing, pClass.Conn, auxClientCon)
            auxControlPanel_DYNGRP.SelectionLimit = 1
            auxControlPanel_DYNGRP.gAutosuggest_Enabled("(SELECT DOC_DYNGRP.cod as cod,DOC_DYNGRP.dsc as dsc," & enumEntities.coEntityDOC_DYNGRP & " as q_type,'Especial' " _
                                                     & " FROM DOC_DYNGRP " _
                                                     & " WHERE ( DOC_DYNGRP.dsc LIKE '%{#HRCDSC#}%')  AND DOC_DYNGRP.cod >= 1)" _
                                                    & " ORDER BY dsc,q_type")
            auxControlPanel_DYNGRP.FieldData = "dyngrpcod"


            Dim auxRecTipDT As New DataTable
            auxRecTipDT.Columns.Add(New DataColumn("cod", Type.GetType("System.Int32")))
            auxRecTipDT.Columns.Add(New DataColumn("dsc", Type.GetType("System.String")))
            auxnewRow = auxRecTipDT.NewRow
            auxnewRow("cod") = enumEntities.coEntityEMP
            auxnewRow("dsc") = "<img src=imagenes/icon" & Format(CInt(enumEntities.coEntityEMP), "00000000") & ".png border=0 width=16px >Colaborador"
            auxRecTipDT.Rows.Add(auxnewRow)
            auxnewRow = auxRecTipDT.NewRow
            auxnewRow("cod") = enumEntities.coEntityUND
            auxnewRow("dsc") = "<img src=imagenes/icon" & Format(CInt(enumEntities.coEntityUND), "00000000") & ".png border=0 width=16px >Unidad"
            auxRecTipDT.Rows.Add(auxnewRow)
            auxnewRow = auxRecTipDT.NewRow
            auxnewRow("cod") = enumEntities.coEntityDOC_EQU
            auxnewRow("dsc") = "<img src=imagenes/icon" & Format(CInt(enumEntities.coEntityDOC_EQU), "00000000") & ".png border=0 width=16px >Equipo"
            auxRecTipDT.Rows.Add(auxnewRow)
            auxnewRow = auxRecTipDT.NewRow
            auxnewRow("cod") = enumEntities.coEntityDOC_DYNGRP
            auxnewRow("dsc") = "<img src=imagenes/icon" & Format(CInt(enumEntities.coEntityDOC_DYNGRP), "00000000") & ".png border=0 width=16px >Especiales"
            auxRecTipDT.Rows.Add(auxnewRow)


            Dim auxControlPanel_Tipo As New clshrcJSComboBox("pnlpanel_tipo", "Tipo", "DOCMBRTYPE", "form-control", auxRecTipDT, "300px", "50px")
            auxControlPanel_Tipo.DisplayMode = clshrcJSComboBox.enumDisplayMode.Radio
            Dim auxDocTipo_DefaultValue As String = enumEntities.coEntityEMP
            auxControlPanel_Tipo.DefaultValue = "'" & auxDocTipo_DefaultValue & "'"
            auxControlPanel_Tipo.JSEventChange = "hrcConsole_log('default'+ pdata[0]);" _
                        & "switch (pdata[0]){" _
                        & "case " & enumEntities.coEntityEMP & ":case '" & enumEntities.coEntityEMP & "':" _
                        & auxDefaultPanel.gShowRowOfControl(auxControlPanel_EMP) & ";" _
                        & auxDefaultPanel.gHideRowOfControl(auxControlPanel_UND) & ";" _
                        & auxDefaultPanel.gHideRowOfControl(auxControlPanel_UndRol) & ";" _
                        & auxDefaultPanel.gHideRowOfControl(auxControlPanel_EQU) & ";" _
                        & auxDefaultPanel.gHideRowOfControl(auxControlPanel_DYNGRP) & ";" _
                        & "break;" _
                        & "case " & enumEntities.coEntityUND & ":case '" & enumEntities.coEntityUND & "':" _
                        & auxDefaultPanel.gHideRowOfControl(auxControlPanel_EMP) & ";" _
                        & auxDefaultPanel.gShowRowOfControl(auxControlPanel_UND) & ";" _
                        & auxDefaultPanel.gShowRowOfControl(auxControlPanel_UndRol) & ";" _
                        & auxDefaultPanel.gHideRowOfControl(auxControlPanel_EQU) & ";" _
                        & auxDefaultPanel.gHideRowOfControl(auxControlPanel_DYNGRP) & ";" _
                         & "break;" _
                         & "case " & enumEntities.coEntityDOC_EQU & ":case '" & enumEntities.coEntityDOC_EQU & "':" _
                        & auxDefaultPanel.gHideRowOfControl(auxControlPanel_EMP) & ";" _
                        & auxDefaultPanel.gHideRowOfControl(auxControlPanel_UND) & ";" _
                        & auxDefaultPanel.gHideRowOfControl(auxControlPanel_UndRol) & ";" _
                        & auxDefaultPanel.gShowRowOfControl(auxControlPanel_EQU) & ";" _
                        & auxDefaultPanel.gHideRowOfControl(auxControlPanel_DYNGRP) & ";" _
                        & "break;" _
                        & "case " & enumEntities.coEntityDOC_DYNGRP & ":case '" & enumEntities.coEntityDOC_DYNGRP & "':" _
                        & auxDefaultPanel.gHideRowOfControl(auxControlPanel_EMP) & ";" _
                        & auxDefaultPanel.gHideRowOfControl(auxControlPanel_UND) & ";" _
                        & auxDefaultPanel.gHideRowOfControl(auxControlPanel_UndRol) & ";" _
                        & auxDefaultPanel.gHideRowOfControl(auxControlPanel_EQU) & ";" _
                        & auxDefaultPanel.gShowRowOfControl(auxControlPanel_DYNGRP) & ";" _
                        & "break;" _
                        & "default:" _
                        & auxDefaultPanel.gHideRowOfControl(auxControlPanel_EMP) & ";" _
                        & auxDefaultPanel.gHideRowOfControl(auxControlPanel_UND) & ";" _
                        & auxDefaultPanel.gHideRowOfControl(auxControlPanel_UndRol) & ";" _
                        & auxDefaultPanel.gHideRowOfControl(auxControlPanel_EQU) & ";" _
                        & auxDefaultPanel.gHideRowOfControl(auxControlPanel_DYNGRP) & ";" _
                        & "break;" _
                        & "};"
            auxControlPanel_Tipo.FieldData = "DOCMBRTYPE"
            auxDefaultPanel.gControls_Add(auxControlPanel_Rol)

            auxDefaultPanel.gControls_Add(auxControlPanel_Tipo)
            auxDefaultPanel.gControls_Add(auxControlPanel_EMP)
            auxDefaultPanel.gControls_Add(auxControlPanel_UND)
            auxDefaultPanel.gControls_Add(auxControlPanel_UndRol)
            auxDefaultPanel.gControls_Add(auxControlPanel_EQU)
            auxDefaultPanel.gControls_Add(auxControlPanel_DYNGRP)

            'COLUMNAS

            auxScript = "function griddata_prerol(pCod, pDsc,pRolCod) {" _
                & "var auxReturn = '&nbsp;';" _
                & "if (pDsc != ''){" _
                & "auxReturn='<img width=12px alt="""" src=imagenes/icondocrol' + gNumber_pad(pRolCod,8) + '.png />' +  pDsc;" _
                & "}" _
                & "return auxReturn;" _
                & "}"
            auxFormatter = "griddata_prerol({#CURRENTROW_COD#},{#CURRENTROW_DOC_DOCMBRROLDSC#},{#CURRENTROW_ROLCOD#})"
            auxGridTRO.gColumn_Add("Rol", 15, "COD", clshrcGrdData.enumAlign.coLeft, False, auxFormatter, auxScript, True)

            auxScript = "function griddata_dsc(pCod, pDsc,pType) {" _
                  & "var auxReturn = '&nbsp;';" _
                  & "if (pDsc != ''){" _
                  & "auxReturn='<img width=12px alt="""" src=imagenes/icon' + gNumber_pad(pType,8) + '.png />' +  pDsc;" _
                  & "}" _
                  & "return auxReturn;" _
                  & "}"
            auxFormatter = "griddata_dsc({#CURRENTROW_COD#},{#CURRENTROW_DOC_DOCMBRDSC#},{#CURRENTROW_DOCMBRTYPE#})"
            auxGridTRO.gColumn_Add("Recurso", -1, "COD", clshrcGrdData.enumAlign.coLeft, False, auxFormatter, auxScript, True)
            ' auxControlRecItem.gColumn_Add("Tipo", 80, "rectype_dsc", clshrcGrdData.enumAlign.coLeft, False, "", "", False)
            ' auxGridTRO.gColumn_Add("Rol en unidad", 80, "UNDrolgrp_dsc", clshrcGrdData.enumAlign.coLeft, False, "", "", False)



            If pEditMode Then
                auxGridTRO.gJSCommand_Add("EDIT", "", auxDefaultPanel)
                auxScript = "function gridrol_edit(pType,pCOD,pQ_DSC,pViewCommand) {" _
                            & "var auxReturn='&nbsp';" _
                            & "var auxType = parseInt(pType);" _
                                & "auxReturn ='<u><span style=cursor:pointer; onclick=' + String.fromCharCode(39) + '" & auxGridTRO.gJSCommand_GetCall("EDIT", auxCommandData) & "' + String.fromCharCode(39) + ' >" _
                                & "<img src=imagenes/actmod.png border=0 width=16px >'" _
                                & "+ '</a></u>';" _
                            & "return auxReturn;" _
                            & "}"
                auxFormatter = "gridrol_edit({#CURRENTROW_Q_TYPE#},{#CURRENTROW_COD#},{#CURRENTROW_Q_DSC#},0)"
                auxGridTRO.gColumn_Add("", 3, "cmd_edit", clshrcGrdData.enumAlign.coLeft, False, auxFormatter, auxScript, False)

                auxGridTRO.gJSCommand_Add("DELETE", "", auxDefaultPanel)
                auxScript = "function gridrol_del(pCOD,pQ_DSC) {" _
                            & "var auxReturn='&nbsp';" _
                                & "auxReturn ='<u><span style=cursor:pointer; onclick=' + String.fromCharCode(39) + '" & auxGridTRO.gJSCommand_GetCall("DELETE", auxCommandData) & "' + String.fromCharCode(39) + ' >" _
                                & "<img src=imagenes/actdel.png border=0 width=16px >'" _
                                & "+ '</a></u>';;" _
                            & "return auxReturn;" _
                            & "}"
                auxFormatter = "gridrol_del({#CURRENTROW_COD#},{#CURRENTROW_Q_DSC#})"
                auxGridTRO.gColumn_Add("", 3, "cmd_delete", clshrcGrdData.enumAlign.coLeft, False, auxFormatter, auxScript, False)
            End If
            auxPanel.gControls_Add(auxGridTRO)

            'Copy roles
            If pCopyTROVisible Then
                Dim auxTroClearCode As String = "if (confirm('Confirma la eliminación de todos los roles?')){" _
                            & auxPanel.gJSCommand_Get(pCommandName:="TROCLEAR") & ";" _
                            & auxGridTRO.gJS_GetReloadCode & ";" _
                            & "};"
                auxPanel.gJSCommand_Add(pCommandName:="TROCLEAR", pJSCode:="")
                auxGridTRO.gToolbar_AddButtonCommand("cmdclear", "Limpiar", "", "", auxTroClearCode)
                'Dim auxControlTROClearButton As New clsHrcJSButton("pnlpanel_troclear_button", "Limpiar", pHrcContext.ButtonCSS)
                'auxControlTROClearButton.EventOnClick = "if (confirm('Confirma la eliminación de todos los roles?')){" _
                '            & auxPanel.gJSCommand_Get(pCommandName:="TROCLEAR") & ";" _
                '            & auxGridTRO.gJS_GetReloadCode & ";" _
                '            & "};return false;"
                'auxPanel.gControls_Add(auxControlTROClearButton)

                auxPanel.gJSCommand_Add(pCommandName:="TROCOPY", pJSCode:="")
                Dim auxDTTro As DataTable = pClass.Conn.gConn_Query("SELECT cod,dsc FROM DOC_TRO WHERE cod > 0" _
                                                                & " AND (custom = {#FALSE#} OR custom {#ISNULL#})")
                If auxDTTro.Rows.Count <> 0 Then
                    Dim auxControlTRO As New clshrcJSComboBox("cmbtrocopy", "<img src=imagenes/qdoc_copytemplate.png class=hrcthemeimage width=24px>" _
                                                              & "Copiar plantilla de roles", "TROCOPY", pHrcContext.ControlComboBoxCSS, auxDTTro, "250px", "50px")
                    auxControlTRO.gValue_Set(auxDTTro.Rows(0)("cod"))
                    auxPanel.gControls_Add(auxControlTRO)
                    'auxControlTRO.Width = "200px"
                    auxControlTRO.ServerStateMantain = True
                    auxControlTRO.DisplayMode = clshrcJSComboBox.enumDisplayMode.DropDown

                    Dim auxControlTROButton As New clsHrcJSButton("pnlpanel_tro_button", "Copiar", pHrcContext.ButtonCSS)
                    auxControlTROButton.EventOnClick = "if (confirm('Confirma la copia de roles?')){" _
                                & auxPanel.gJSCommand_Get(pCommandName:="TROCOPY") & ";" _
                                & auxGridTRO.gJS_GetReloadCode & ";" _
                                & "};return false;"
                    auxPanel.gControls_Add(auxControlTROButton)


                End If


            End If




            pHrcContext.gObjectTmp_UploadControl(auxGridTRO)
            Return auxPanel
        End If


    End Function
    Private Sub gPanelRoles_CommandHandler(ByVal pControl As clsHrcJSControlBasic, ByVal pValues As clshrcBagValues)
        Dim auxCommandName As String = pValues.gValue_Get("ACTION").ToString.ToUpperInvariant
        'Dim auxDFTGenCod As String = ""
        Dim auxPrincipalForm As clsHrcJSPanel = pControl.BagValues.gValue_Get("principal_panel")
        If auxPrincipalForm Is Nothing Then
            If pControl.ControlID.ToUpper = "GRDTRO" Then
                auxPrincipalForm = pControl.Parentcontrol.Parentcontrol
            ElseIf pControl.ControlID = "PNL_GRDTRO_DEFAULTPANEL" Then
                auxPrincipalForm = pControl.Parentcontrol.Parentcontrol.Parentcontrol.Parentcontrol
                If auxPrincipalForm Is Nothing Then
                    auxPrincipalForm = pControl.Parentcontrol.Parentcontrol.Parentcontrol
                End If
            Else
                auxPrincipalForm = pControl.Parentcontrol.Parentcontrol.Parentcontrol
            End If
        End If
    

        Dim auxBagValues As clshrcBagValues = auxPrincipalForm.BagValues
        Dim auxDftDidGenCod As String = auxBagValues.gValue_Get("dftgencod_TRO", "")
        'auxDFTGenCod = pValues.gValue_Get("DFTDIDGENCOD")
        Dim auxTroRolCod As Integer = Val(pValues.gValue_Get("COD"))
        If pControl.ControlID = "PRINCIPAL_PANEL" And auxCommandName = "CONFIRMED" Then
            Dim auxClass As New clsCusimDOC
            auxClass.Conn.gConn_Open()
            Dim auxTroCod As Integer = auxPrincipalForm.BagValues.gValue_Get("troCod")
            Dim auxPrincipalPanel As clsHrcJSPanel = pControl
            Dim auxValues As clshrcBagValues = auxPrincipalPanel.gFieldData_GetValues
            auxClass.gEntity_DOC_TRO_Update(pcod:=auxTroCod, _
                                             pdftdidgencod:=auxDftDidGenCod, _
                                             pdsc:=auxValues.gValue_Get("dsc"))
            auxClass.gDraft_Confirm(auxDftDidGenCod)
            auxClass.gDraft_Delete(auxDftDidGenCod)
            auxClass.Conn.gConn_Close()
        ElseIf auxCommandName = "TROCOPY" Then
            Dim auxdftgencod As String = auxPrincipalForm.BagValues.gValue_Get("dftgencod_TRO")
            Dim auxCod As Integer = auxPrincipalForm.BagValues.gValue_Get("cod")
            Dim auxControlTRO As clshrcJSComboBox = pControl.gControl_Get("cmbtrocopy")
            Dim auxTroCod As Integer = auxPrincipalForm.BagValues.gValue_Get("troCod")
            Dim auxSourceTroRolCod As Integer = Val(auxControlTRO.gValue_Get)   ' Val(cmbDOC_TRO.SelectedValue)
            Dim auxClass As New clsCusimDOC
            auxClass.Conn.gConn_Open()
            If auxSourceTroRolCod > 0 Then
                Dim auxDT As DataTable
                auxDT = auxClass.gTRO_Get(pTroCod:=auxSourceTroRolCod)
                Dim auxRolesCheck As New List(Of enumRoles)
                For Each auxRow As DataRow In auxDT.Rows
                    auxTroRolCod = auxClass.gEntity_DOC_TROROL_InsertInDraft(pdftdidgencod:=auxdftgencod, _
                                                        prolcod:=auxRow("rolcod"), _
                                                        ptrocod:=auxTroCod, _
                                                        prolcodtype:=auxRow("docmbrtype"))
                    auxRolesCheck.Add(auxRow("rolcod"))

                    Select Case auxRow("docmbrtype")
                        Case enumEntities.coEntityDOC_EQU
                            auxClass.gEntity_DOC_TROROLEQU_InsertInDraft(pdftdidgencod:=auxdftgencod, _
                                                        ptrorolcod:=auxTroRolCod, _
                                                       pequcod:=auxRow("equcod"))
                        Case enumEntities.coEntityEMP
                            auxClass.gEntity_DOC_TROROLEMP_InsertInDraft(pdftdidgencod:=auxdftgencod, _
                           ptrorolcod:=auxTroRolCod, _
                           pempcod:=auxRow("empcod"))
                        Case enumEntities.coEntityUND
                            Dim auxRolUnidad As enumUND_Roles = auxRow("rolcod") ' enumUND_Roles.coMiembro
                            'auxRoles.Add(auxRow("rolcod"))
                            'Select Case CType(Val(auxRow("rolcod")), enumRoles)
                            '    'Case enumRoles.coLector
                            '    'auxRolUnidad = enumUnidadesRoles.coMiembro
                            '    Case enumRoles.coEditor
                            '        auxRolUnidad = enumUND_Roles.coEditorDocs
                            '    Case Else
                            '        auxRolUnidad = enumUND_Roles.coResponsable
                            'End Select
                            auxClass.gEntity_DOC_TROROLUND_InsertInDraft(pdftdidgencod:=auxdftgencod, _
                            ptrorolcod:=auxTroRolCod, _
                            pundcod:=auxRow("undcod"), _
                             prolgrpcod:=auxRolUnidad)

                    End Select
                Next
                auxDT = auxClass.gTRO_Get(pTroCod:=auxTroCod, pDfdGenID:=auxdftgencod)
                For Each auxRow As DataRow In auxDT.Rows
                    If auxRow("rolcod") > 0 Then
                        If (auxClass.Conn.gField_GetInt(auxRow("docmbrtype"), -1) < 1 Or _
                           (auxClass.Conn.gField_GetInt(auxRow("docmbrtype"), -1) = enumEntities.coEntityEMP And auxClass.Conn.gField_GetInt(auxRow("empcod"), -1) < 1)) _
                            And auxRolesCheck.IndexOf(auxRow("rolcod")) <> -1 Then
                            auxRolesCheck.Add(auxRow("rolcod"))
                            auxClass.gEntity_DOC_TROROLEMP_DeleteInDraft(auxdftgencod, auxRow("cod"))
                            auxClass.gEntity_DOC_TROROLUND_DeleteInDraft(auxdftgencod, auxRow("cod"))
                            auxClass.gEntity_DOC_TROROLEQU_DeleteInDraft(auxdftgencod, auxRow("cod"))
                            auxClass.gEntity_DOC_TROROLDYNGRP_DeleteInDraft(auxdftgencod, auxRow("cod"))
                            auxClass.gEntity_DOC_TROROL_DeleteInDraft(auxdftgencod, auxRow("cod"))
                        End If
                    End If
                Next
                'grdRecursos.DataBind()
                'updpnlrecursos.Update()
            End If
            auxClass.Conn.gConn_Close()
        ElseIf auxCommandName = "TROCLEAR" Then
            Dim auxdftgencod As String = auxPrincipalForm.BagValues.gValue_Get("dftgencod_TRO")
            Dim auxCod As Integer = auxPrincipalForm.BagValues.gValue_Get("cod")
            Dim auxControlTRO As clshrcJSComboBox = pControl.gControl_Get("cmbtrocopy")
            Dim auxTroCod As Integer = auxPrincipalForm.BagValues.gValue_Get("troCod")
            Dim auxClass As New clsCusimDOC
            auxClass.Conn.gConn_Open()
            Dim auxRoles As New List(Of enumRoles)
            Dim auxDT As DataTable
            auxDT = auxClass.gTRO_Get(pTroCod:=auxTroCod, pDfdGenID:=auxdftgencod)
            For Each auxRow As DataRow In auxDT.Rows
                If auxRow("rolcod") > 0 Then
                    auxClass.gEntity_DOC_TROROLEMP_DeleteInDraft(auxdftgencod, auxRow("cod"))
                    auxClass.gEntity_DOC_TROROLUND_DeleteInDraft(auxdftgencod, auxRow("cod"))
                    auxClass.gEntity_DOC_TROROLEQU_DeleteInDraft(auxdftgencod, auxRow("cod"))
                    auxClass.gEntity_DOC_TROROLDYNGRP_DeleteInDraft(auxdftgencod, auxRow("cod"))
                    If auxRoles.IndexOf(auxRow("rolcod")) <> -1 Then
                        'El primero lo deja
                        'Los siguientes elimina la linea
                        auxClass.gEntity_DOC_TROROL_DeleteInDraft(auxdftgencod, auxRow("cod"))
                    End If
                    auxRoles.Add(auxRow("rolcod"))
                End If
            Next
         
            auxClass.Conn.gConn_Close()
        ElseIf auxCommandName = "GRDDATA_PANELITEM_INS_GET" Then
            Dim auxResponse As String = ""
            Dim auxControl_Emp As clshrcObjectExplorer = pControl.gControl_Get("PNLpanel_emp")
            If auxControl_Emp IsNot Nothing Then
                auxControl_Emp.gItem_ClearAll()
            End If
            auxResponse &= ",""EMPCOD"":[]"

            Dim auxControl_Und As clshrcObjectExplorer = pControl.gControl_Get("pnlpanel_UND")
            If auxControl_Und IsNot Nothing Then
                auxControl_Und.gItem_ClearAll()
            End If
            auxResponse &= ",""UNDCOD"":[]"

            Dim auxControl_Equ As clshrcObjectExplorer = pControl.gControl_Get("pnlpanel_EQU")
            If auxControl_Equ IsNot Nothing Then
                auxControl_Equ.gItem_ClearAll()
            End If
            auxResponse &= ",""EQUCOD"":[]"

            Dim auxControl_DynGrp As clshrcObjectExplorer = pControl.gControl_Get("pnlpanel_DYNGRP")
            If auxControl_DynGrp IsNot Nothing Then
                auxControl_DynGrp.gItem_ClearAll()
            End If
            auxResponse &= ",""DYNGRPCOD"":[]"

            pValues.gValue_Add("HRC_RESULTS", "[{""COD"":""" & auxTroRolCod & """" & auxResponse.ToString & "}]")
        ElseIf auxCommandName = "GRDDATA_COMMAND" _
            Or auxCommandName = "GRDDATA_PANELITEM_GET" _
            Or auxCommandName = "SERVICE_COMMAND" Then
            If pValues.Values.IndexOfKey("COMMANDNAME") = -1 Then
                Dim auxDT As DataTable = pValues.gValue_Get("HRC_COMMANDRESULTS")
                Dim auxConn As clsHrcConnClient = Session("conn")

                'EMPCOD
                Dim auxControl_Emp As clshrcObjectExplorer = pControl.gControl_Get("PNLpanel_emp")
                If auxControl_Emp IsNot Nothing Then
                    auxControl_Emp.gItem_ClearAll()
                End If
                Dim auxEmpCod As Integer = auxConn.gField_GetInt(auxDT.Rows(0)("EMPCOD"), -1)
                Dim auxEmpDsc As String = ""
                If auxEmpCod > 0 Then
                    auxEmpDsc = hrcEntityDT_EMP_FindByKey(auxEmpCod)("dsc")
                End If
                'Cambia por -1 para luego encontarlo y reemplazarlo
                auxDT.Rows(0)("EMPCOD") = -1

                'UNDCOD
                Dim auxControl_Und As clshrcObjectExplorer = pControl.gControl_Get("pnlpanel_UND")
                If auxControl_Und IsNot Nothing Then
                    auxControl_Und.gItem_ClearAll()
                End If
                Dim auxUndCod As Integer = auxConn.gField_GetInt(auxDT.Rows(0)("UNDCOD"), -1)
                Dim auxUndDsc As String = ""
                If auxUndCod > 0 Then
                    auxUndDsc = hrcEntityDT_UND_FindByKey(auxUndCod)("dsc")
                End If
                'Cambia por -1 para luego encontarlo y reemplazarlo
                auxDT.Rows(0)("UNDCOD") = -1

                'EQUCOD
                Dim auxControl_Equ As clshrcObjectExplorer = pControl.gControl_Get("pnlpanel_EQU")
                If auxControl_Equ IsNot Nothing Then
                    auxControl_Equ.gItem_ClearAll()
                End If
                Dim auxEquCod As Integer = auxConn.gField_GetInt(auxDT.Rows(0)("EQUCOD"), -1)
                Dim auxEquDsc As String = ""
                If auxEquCod > 0 Then
                    auxEquDsc = hrcEntityDT_DOC_EQU_FindByKey(auxEquCod)("dsc")
                End If
                'Cambia por -1 para luego encontarlo y reemplazarlo
                auxDT.Rows(0)("EQUCOD") = -1

                'dyngrp
                Dim auxControl_DynGrp As clshrcObjectExplorer = pControl.gControl_Get("pnlpanel_DYNGRP")
                If auxControl_DynGrp IsNot Nothing Then
                    auxControl_DynGrp.gItem_ClearAll()
                End If
                Dim auxDynGrpCod As Integer = auxConn.gField_GetInt(auxDT.Rows(0)("dyngrpcod"), -1)
                Dim auxDynGrpDsc As String = ""
                If auxDynGrpCod > 0 Then
                    auxDynGrpDsc = hrcEntityDT_DOC_DYNGRP_FindByKey(auxDynGrpCod)("dsc")
                End If
                'Cambia por -1 para luego encontarlo y reemplazarlo
                auxDT.Rows(0)("DYNGRPCOD") = -1


                'Dim auxResponse As String = auxConn.gField_GetJSONString(auxDT, Nothing, True)
                Dim auxReturnValues As clshrcBagValues = auxConn.gField_GetBagValuesFromArray(auxDT.Rows(0).ItemArray, auxDT.Columns)
                'auxReturnValues.gValue_Add("ROLCOD_ISREADONLY", "1")
                Dim auxResponse As String = "[" & auxConn.gField_GetJSONString(auxReturnValues, True) & "]"
                If auxEmpCod > 0 Then
                    auxResponse = Replace(auxResponse, """EMPCOD"":""-1""", """EMPCOD"":[{""Q_COD"":""" & auxEmpCod & """,""Q_TYPE"":""" & enumEntities.coEntityEMP & """,""Q_DSC"":""" & auxEmpDsc & """}]")
                Else
                    auxResponse = Replace(auxResponse, """EMPCOD"":""-1""", """EMPCOD"":[]")
                End If
                If auxUndCod > 0 Then
                    auxResponse = Replace(auxResponse, """UNDCOD"":""-1""", """UNDCOD"":[{""Q_COD"":""" & auxUndCod & """,""Q_TYPE"":""" & enumEntities.coEntityUND & """,""Q_DSC"":""" & auxUndDsc & """}]")
                Else
                    auxResponse = Replace(auxResponse, """UNDCOD"":""-1""", """UNDCOD"":[]")
                End If
                If auxEquCod > 0 Then
                    auxResponse = Replace(auxResponse, """EQUCOD"":""-1""", """EQUCOD"":[{""Q_COD"":""" & auxEmpCod & """,""Q_TYPE"":""" & enumEntities.coEntityDOC_EQU & """,""Q_DSC"":""" & auxEquDsc & """}]")
                Else
                    auxResponse = Replace(auxResponse, """EQUCOD"":""-1""", """EQUCOD"":[]")
                End If
                If auxDynGrpCod > 0 Then
                    auxResponse = Replace(auxResponse, """DYNGRPCOD"":""-1""", """DYNGRPCOD"":[{""Q_COD"":""" & auxDynGrpCod & """,""Q_TYPE"":""" & enumEntities.coEntityDOC_DYNGRP & """,""Q_DSC"":""" & auxDynGrpDsc & """}]")
                Else
                    auxResponse = Replace(auxResponse, """DYNGRPCOD"":""-1""", """DYNGRPCOD"":[]")
                End If
                If auxDT.Rows(0)("docmbrtype") < 1 Then
                    auxResponse = Replace(auxResponse, """DOCMBRTYPE"":""-1""", """DOCMBRTYPE"":""" & enumEntities.coEntityEMP & """")
                End If

                pValues.gValue_Add("HRC_RESULTS", auxResponse.ToString)
            End If
        ElseIf auxCommandName = "GRDDATA_PANELITEM_INS" _
            Or auxCommandName = "GRDDATA_PANELITEM_MOD" Then
            Dim auxMode As enumActionType
            Select Case auxCommandName
                Case "GRDDATA_PANELITEM_MOD"
                    auxMode = enumActionType.coModify
                Case "GRDDATA_PANELITEM_INS"
                    auxMode = enumActionType.coNew
            End Select
            Dim auxRoles As New Stack(Of clshrcObjectExplorer)

            Dim auxClient As New imClientConnection
            ' Dim auxTroRolCod As Integer = Val(pValues.gValue_Get("COD", -1))
            Dim auxPanel As clsHrcJSPanel = pControl

            Dim auxTroCod As Integer = Val(auxBagValues.gValue_Get("trocod", -1))
            Dim auxRolCod As Integer = Val(pValues.gValue_Get("ROLCOD", -1))
            Dim auxClass As New clsCusimDOC
            Dim auxDTTro As DataTable
            auxDTTro = auxClass.gTRO_Get(pTroCod:=auxTroCod, pDfdGenID:=auxDftDidGenCod)
            Dim auxTipo As Integer = Val(pValues.gValue_Get("DOCMBRTYPE"))
            auxClass.gEntity_DOC_TROROLEMP_DeleteInDraft(ptrorolcod:=auxTroRolCod, pdftdidgencod:=auxDftDidGenCod)
            auxClass.gEntity_DOC_TROROLUND_DeleteInDraft(ptrorolcod:=auxTroRolCod, pdftdidgencod:=auxDftDidGenCod)
            auxClass.gEntity_DOC_TROROLEQU_DeleteInDraft(ptrorolcod:=auxTroRolCod, pdftdidgencod:=auxDftDidGenCod)
            auxClass.gEntity_DOC_TROROLDYNGRP_DeleteInDraft(ptrorolcod:=auxTroRolCod, pdftdidgencod:=auxDftDidGenCod)
            If auxTipo < 1 Then
                auxTipo = enumEntities.coEntityEMP
            End If
            Dim auxRolesCheck As New List(Of enumRoles)
            Select Case auxTipo
                Case enumEntities.coEntityEMP
                    Dim auxControlEmp As clshrcObjectExplorer = auxPanel.gControl_Get("pnlpanel_emp")
                    If auxControlEmp IsNot Nothing Then
                        For Each auxValue As clsNode In auxControlEmp.ItemList
                            If auxValue.Cod > 0 Then
                                auxRolesCheck.Add(auxRolCod)
                                If auxDTTro.Select("docmbrtype=" & auxTipo _
                                                   & " AND rolcod=" & auxRolCod _
                                                   & " AND DOC_DOCMBRobjectcod=" & auxValue.Cod).Count = 0 Then
                                    auxTroRolCod = auxClass.gEntity_DOC_TROROL_InsertInDraft(ptrocod:=auxTroCod, pdftdidgencod:=auxDftDidGenCod, _
                                              prolcodtype:=auxTipo, prolcod:=auxRolCod)
                                Else
                                    auxClass.gEntity_DOC_TROROL_Update(pcod:=auxTroRolCod, pdftdidgencod:=auxDftDidGenCod, _
                                                                       prolcodtype:=auxTipo, prolcod:=auxRolCod)
                                End If
                                auxClass.gEntity_DOC_TROROLEMP_InsertInDraft(ptrorolcod:=auxTroRolCod, pdftdidgencod:=auxDftDidGenCod, _
                                                                     pempcod:=auxValue.Cod)
                            End If
                        Next

                    End If
                Case enumEntities.coEntityUND
                    Dim auxControlUnd As clshrcObjectExplorer = auxPanel.gControl_Get("pnlpanel_und")
                    If auxControlUnd IsNot Nothing Then
                        Dim auxUNDRolGrpCod As Integer = Val(pValues.gValue_Get("undrolgrpcod"))
                        If auxUNDRolGrpCod < 1 Then
                            Select Case auxRolCod
                                Case enumRoles.coEditor
                                    auxUNDRolGrpCod = enumUND_Roles.coEditorDocs
                                Case enumRoles.coReceptor
                                    auxUNDRolGrpCod = enumUND_Roles.coEditorDocs
                                Case Else
                                    auxUNDRolGrpCod = enumUND_Roles.coResponsable
                            End Select
                        End If
                        For Each auxValue As clsNode In auxControlUnd.ItemList
                            If auxValue.Cod > 0 Then
                                auxRolesCheck.Add(auxRolCod)
                                If auxDTTro.Select("docmbrtype=" & auxTipo _
                                                   & " AND rolcod=" & auxRolCod _
                                                   & " AND DOC_DOCMBRobjectcod=" & auxValue.Cod).Count = 0 Then
                                    auxTroRolCod = auxClass.gEntity_DOC_TROROL_InsertInDraft(ptrocod:=auxTroCod, pdftdidgencod:=auxDftDidGenCod, _
                                            prolcodtype:=auxTipo, prolcod:=auxRolCod)
                                Else
                                    auxClass.gEntity_DOC_TROROL_Update(pcod:=auxTroRolCod, pdftdidgencod:=auxDftDidGenCod, _
                                                                       prolcodtype:=auxTipo, prolcod:=auxRolCod)
                                End If
                                auxClass.gEntity_DOC_TROROLUND_InsertInDraft(ptrorolcod:=auxTroRolCod, pdftdidgencod:=auxDftDidGenCod, _
                                                                            pundcod:=auxValue.Cod, _
                                                                            prolgrpcod:=auxUNDRolGrpCod)
                            End If
                        Next
                    End If
                Case enumEntities.coEntityDOC_EQU
                    Dim auxControlEqu As clshrcObjectExplorer = auxPanel.gControl_Get("pnlpanel_equ")
                    If auxControlEqu IsNot Nothing Then
                        For Each auxValue As clsNode In auxControlEqu.ItemList
                            If auxValue.Cod > 0 Then
                                auxRolesCheck.Add(auxRolCod)
                                If auxDTTro.Select("docmbrtype=" & auxTipo _
                                                  & " AND rolcod=" & auxRolCod _
                                                  & " AND DOC_DOCMBRobjectcod=" & auxValue.Cod).Count = 0 Then
                                    auxTroRolCod = auxClass.gEntity_DOC_TROROL_InsertInDraft(ptrocod:=auxTroCod, pdftdidgencod:=auxDftDidGenCod, _
                                                prolcodtype:=auxTipo, prolcod:=auxRolCod)
                                Else
                                    auxClass.gEntity_DOC_TROROL_Update(pcod:=auxTroRolCod, pdftdidgencod:=auxDftDidGenCod, _
                                                                      prolcodtype:=auxTipo, prolcod:=auxRolCod)
                                End If
                                auxClass.gEntity_DOC_TROROLEQU_InsertInDraft(ptrorolcod:=auxTroRolCod, pdftdidgencod:=auxDftDidGenCod, _
                                                                     pequcod:=auxValue.Cod)
                            End If
                        Next
                    End If
                Case enumEntities.coEntityDOC_DYNGRP
                    Dim auxControlDynGrp As clshrcObjectExplorer = auxPanel.gControl_Get("pnlpanel_dyngrp")
                    If auxControlDynGrp IsNot Nothing Then
                        For Each auxValue As clsNode In auxControlDynGrp.ItemList
                            If auxValue.Cod > 0 Then
                                If auxDTTro.Select("docmbrtype=" & auxTipo _
                                               & " AND rolcod=" & auxRolCod _
                                               & " AND DOC_DOCMBRobjectcod=" & auxValue.Cod).Count = 0 Then
                                    auxTroRolCod = auxClass.gEntity_DOC_TROROL_InsertInDraft(ptrocod:=auxTroCod, pdftdidgencod:=auxDftDidGenCod, _
                                              prolcodtype:=auxTipo, prolcod:=auxRolCod)
                                Else
                                    auxClass.gEntity_DOC_TROROL_Update(pcod:=auxTroRolCod, pdftdidgencod:=auxDftDidGenCod, _
                                                                        prolcodtype:=auxTipo, prolcod:=auxRolCod)
                                End If
                                auxClass.gEntity_DOC_TROROLDYNGRP_InsertInDraft(ptrorolcod:=auxTroRolCod, pdftdidgencod:=auxDftDidGenCod, _
                                                                      pdyngrpcod:=auxValue.Cod)
                            End If
                        Next
                    End If
            End Select

            For Each auxRow As DataRow In auxClass.gTRO_Get(pTroCod:=auxTroCod, pDfdGenID:=auxDftDidGenCod).Rows
                If auxRow("rolcod") = auxRolCod Then
                    If (auxClass.Conn.gField_GetInt(auxRow("docmbrtype"), -1) < 1 Or _
                       (auxClass.Conn.gField_GetInt(auxRow("docmbrtype"), -1) = enumEntities.coEntityEMP And auxClass.Conn.gField_GetInt(auxRow("empcod"), -1) < 1)) _
                        And auxRolesCheck.IndexOf(auxRow("rolcod")) <> -1 Then
                        auxClass.gEntity_DOC_TROROLEMP_DeleteInDraft(auxDftDidGenCod, auxRow("cod"))
                        auxClass.gEntity_DOC_TROROLUND_DeleteInDraft(auxDftDidGenCod, auxRow("cod"))
                        auxClass.gEntity_DOC_TROROLEQU_DeleteInDraft(auxDftDidGenCod, auxRow("cod"))
                        auxClass.gEntity_DOC_TROROLDYNGRP_DeleteInDraft(auxDftDidGenCod, auxRow("cod"))
                        auxClass.gEntity_DOC_TROROL_DeleteInDraft(auxDftDidGenCod, auxRow("cod"))
                    End If
                End If
            Next

            auxClass.Conn.gConn_Close()
            pValues.gValue_Add("COD", auxTroRolCod)
        ElseIf auxCommandName = "GRDDATA_PANELITEM_DEL" Then
            Dim auxClass As New clsCusimDOC
            'Dim auxBagValues As clshrcBagValues = pControl.Parentcontrol.Parentcontrol.BagValues
            'Dim auxDftDidGenCod As String = auxBagValues.gValue_Get("dftgencod_TRO", -1)
            'Dim auxTroRolCod As Integer = Val(pValues.gValue_Get("cod"))
            auxClass.gEntity_DOC_TROROLEMP_DeleteInDraft(ptrorolcod:=auxTroRolCod, pdftdidgencod:=auxDftDidGenCod)
            auxClass.gEntity_DOC_TROROLUND_DeleteInDraft(ptrorolcod:=auxTroRolCod, pdftdidgencod:=auxDftDidGenCod)
            auxClass.gEntity_DOC_TROROLEQU_DeleteInDraft(ptrorolcod:=auxTroRolCod, pdftdidgencod:=auxDftDidGenCod)
            auxClass.gEntity_DOC_TROROLDYNGRP_DeleteInDraft(ptrorolcod:=auxTroRolCod, pdftdidgencod:=auxDftDidGenCod)
            auxClass.gEntity_DOC_TROROL_DeleteInDraft(pcod:=auxTroRolCod, pdftdidgencod:=auxDftDidGenCod)
            auxClass.Conn.gConn_Close()
        End If

    End Sub
    Private Function gForm_Cancel(ByVal pQueryString As clshrcBagValues) As String
        Dim auxReturn As String = ""
        Dim auxHTML As New clsHrcCodeHTML

        If pQueryString.gValue_Get("_closea_") = "1" Then
            auxReturn = "self.close();"
        Else
            Dim auxURl As String = "cfrmdocumentos.aspx?_mode_=7&_closea_=0"
            For Each auxItem As KeyValuePair(Of String, Object) In pQueryString.Values
                Select Case auxItem.Key.ToLower
                    Case "_lastview_"
                        auxURl &= "&_view_=" & auxItem.Value
                    Case "apa", "pro", "param1"
                        If auxItem.Value > 0 Then
                            auxURl &= "&" & auxItem.Key.ToLower & "=" & auxItem.Value
                        End If
                    Case "del", "search"
                        auxURl &= "&" & auxItem.Key.ToLower & "=" & auxItem.Value
                    Case Else

                End Select
            Next
            auxReturn = auxHTML.gJS_GotoURL("'" & auxURl & "'") & ";"
        End If
        Return auxReturn
    End Function
    Protected Function gFormPNLLOG_Confirm(ByVal pFormValues As clshrcBagValues) As clshrcBagValues
        Dim auxReturn As clshrcBagValues
        Dim auxClass As New clsCusimDOC
        Dim auxConn As clsHrcConnClient = auxClass.Conn
        auxClass.Conn.gConn_Open()
        Dim auxClient As New imClientConnection
        Dim auxPrincipalForm As clsHrcJSPanel = auxClient.gObjectTmp_Download(ViewState("principal_form"))
        Dim auxCurrentEmpCod As Integer = auxClass.Conn.gField_GetInt(auxPrincipalForm.BagValues.gValue_Get("currentempcod"), -1)
        Dim auxBagValues As clshrcBagValues = auxPrincipalForm.BagValues
        Dim auxCod As Integer = auxPrincipalForm.BagValues.gValue_Get("cod")
        Dim auxIsAdmin As Boolean = auxPrincipalForm.BagValues.gValue_Get("isadmin")
        Dim auxdftgencod As String = auxPrincipalForm.BagValues.gValue_Get("dftgencod")
        Dim auxDTValues_Old As clshrcBagValues = auxPrincipalForm.BagValues.gValue_Get("DTVALUES_OLD")
        Dim auxDTValues_New As clshrcBagValues = auxPrincipalForm.BagValues.gValue_Get("DTVALUES_NEW")
        Dim auxWfwStpCodCurrent As enumWorkflowStep = auxBagValues.gValue_Get("wfwstpcod")
        Dim auxPnlLogPanel As clsHrcJSPanel = auxPrincipalForm.gControl_Get("pnlpanel_pnllog")
        Dim auxError As String = ""
        Dim auxDeleteDraft As Boolean = False
        Dim auxMode As enumActionType = auxClass.Conn.gField_GetInt(auxPrincipalForm.BagValues.gValue_Get("mode"), -1)
        Dim auxWfwStateCodNext As enumWorkflowStep = auxPrincipalForm.BagValues.gValue_Get("pnllog_wfwstpcodnext")
        Dim auxHasWorkflowStepChange As Boolean = False
        If auxWfwStateCodNext > 0 Then
            auxHasWorkflowStepChange = True
        End If
        Dim auxValues As New clshrcBagValues
        auxValues.gValue_Add("GotoStep", -1)
        If auxMode = enumActionType.coModify Then
            If Val(auxPrincipalForm.BagValues.gValue_Get("pnllog_actionmode", -1)) = enumActionType.coDelete Then
                auxMode = enumActionType.coDelete
            End If
        End If
       
        Select Case auxMode
            Case enumActionType.coDelete
                'gMsgBox_Hide()
                auxClass.gEntity_DOC_DOC_DeleteInDraft(auxdftgencod, auxCod)
                auxClass.gTRACE_add(auxCod, 1, "Eliminación confirmada [" & auxCod & "]")
                auxClass.gDraft_Confirm(auxdftgencod, False)
                auxClass.gSystem_PostAction(enumEntities.coEntityDOC_DOC, enumActionType.coConfirmDelete, auxCod)
                auxDeleteDraft = True
            Case enumActionType.coModify, enumActionType.coNew, enumActionType.coNewFromOther
                'gMsgBox_Hide()
                Dim auxAction As enumActionType
                Select Case auxMode
                    Case enumActionType.coModify
                        auxAction = enumActionType.coConfirmModify
                    Case enumActionType.coNew
                        auxAction = enumActionType.coConfirmInsert
                    Case enumActionType.coNewFromOther
                        auxAction = enumActionType.coConfirmReinsert
                End Select
                Dim auxDftgencod_tro As String = auxPrincipalForm.BagValues.gValue_Get("dftgencod_tro")
                Dim auxTroCod As Integer
                auxTroCod = auxClass.gDraft_Confirm(auxDftgencod_tro, False)
                If auxTroCod > 0 Then
                    'Actualiza el codigo de TROCOD
                    '                    auxClass.gEntity_DOC_DOC_Update(pcod:=auxCod, pdftdidgencod:=auxdftgencod, ptrocodcustom:=auxTroCod, ptrocodcustomenabled:=True)
                    auxClass.gEntity_DOC_DOC_Update(pcod:=auxCod, pdftdidgencod:=auxdftgencod, ptrocodcustom:=auxTroCod)
                End If
                auxCod = auxClass.gDraft_Confirm(auxdftgencod, False)
                If auxMode = enumActionType.coNew Or auxMode = enumActionType.coNewFromOther Then
                    'Atención! Trae el ultimo nro. creado. 
                    'auxCod = auxClass.gDraft_Confirm(auxdftgencod, False)
                    auxPrincipalForm.BagValues.gValue_Add("cod", auxCod)
                Else
                    auxClass.gDraft_Confirm(auxdftgencod, False)
                End If
                auxClass.gSystem_PostAction(enumEntities.coEntityDOC_DOC, auxAction, auxCod)

                auxClass.gTRACE_add(auxCod, 10, "Confirmando borrador...")
                If auxCod = -1 Then
                    auxClass.gTRACE_add(auxCod, 1, "Error confirmando borrador [" & auxdftgencod & "].Error=" & auxClass.Conn.LastErrorDescription & "]")
                Else

                    If auxMode = enumActionType.coNew Or auxMode = enumActionType.coNewFromOther Then
                        'ViewState("cod") = auxCod
                        'Cuando es una inserción, ejecutar el paso "1"
                        auxValues.gValue_Add("initialize", "1")
                        If auxHasWorkflowStepChange Then

                        Else
                            auxValues.gValue_Add("Cod", auxCod)
                            auxValues.gValue_Add("gotostep", enumWorkflowStep.coWFWSTPDOC_DOCCreacion)
                            auxValues.gValue_Add("empcod", auxCurrentEmpCod)
                            auxClass.gWorkflow_GotoStep(auxValues)
                        End If
                    Else
                        auxValues.gValue_Add("isadmin", auxIsAdmin)
                        auxValues.gValue_Add("Cod", auxCod)
                        auxValues.gValue_Add("empcod", auxCurrentEmpCod)
                        Dim auxCurrentWfwStpCod As enumWorkflowStep = auxPrincipalForm.BagValues.gValue_Get("wfwstpcod")
                    End If

                    'If auxHasWorkflowStepChange = False Then
                    '    auxError &= auxClass.gREQ_InitializeSubReq(auxValues)
                    'End If
                    If auxError = "" Then
                        If auxClass.Conn.LastErrorDescription <> "" Then
                            'auxClass.gPro_TraceAdd(m_Cod, 1, "Error de actualización [" & auxClass.Conn.LastErrorDescription & "]")
                            auxClass.gTRACE_add(auxCod, 1, "Error de actualización [" & auxClass.Conn.LastErrorDescription & "]")
                        Else
                            auxClass.gTRACE_add(auxCod, 1, "Item actualizado")
                        End If
                    End If
                End If
        End Select
        If auxError = "" Then
            If auxHasWorkflowStepChange = False Then
                auxDeleteDraft = True
            Else
                Dim auxEmp As Integer = -1
                Dim auxDsc As String = auxConn.gField_GetString(pFormValues.gValue_Get("pnllog_dsc"))
                'Dim auxSolucion As String = auxConn.gField_GetString(pFormValues.gValue_Get("pnllog_solucion"))
                'Dim auxFecha As String = auxConn.gField_GetString(pFormValues.gValue_Get("pnllog_Fecha"))
                Dim auxPrologTextRequired As Boolean = False

                Dim auxWfwStateCodNextDsc As String = auxBagValues.gValue_Get("pnllog_wfwstpdsc")
                Dim auxForce As Boolean = False
                auxForce = auxClass.Conn.gField_GetBoolean(pFormValues.gValue_Get("PNLLOG_FORCE"), False)

                Select Case auxWfwStateCodNext
                    Case enumWorkflowStep.coWFWSTPDOC_DOCAprobacionrechazada, enumWorkflowStep.coWFWSTPDOC_DOCCancelacion, enumWorkflowStep.coWFWSTPDOC_DOCnuevaversionNOK, enumWorkflowStep.coWFWSTPDOC_DOCRechazarcancelacion, enumWorkflowStep.coWFWSTPDOC_DOCRevisionrechazada
                        auxPrologTextRequired = True
                End Select
                If auxPrologTextRequired And auxDsc = "" Then
                    auxError = "<img src=imagenes/everror.png width=16px /> Ingrese un comentario"
                Else
                    'gMsgBox_Hide()
                    If auxForce Then
                        auxValues.gValue_Add("force", "1")
                    End If
                    auxValues.gValue_Add("Cod", auxCod)
                    auxValues.gValue_Add("GotoStep", auxWfwStateCodNext)
                    auxValues.gValue_Add("empcod", auxCurrentEmpCod)
                    auxValues.gValue_Add("dsc", auxBagValues.gValue_Get("pnllog_wfwstpdsc"))
                    Dim auxObs As String = auxDsc
                    'auxValues.gValue_Add("prolog_fecha", auxFecha)
                    'If auxFecha <> "" Then
                    '    auxObs = "Hasta el " & auxFecha & "." & auxObs
                    'End If
                    'If auxObs <> "" Then
                    '    auxObs = hrcEntityDT_Q_WFWSTP_FindByKey(auxWfwStateCodNext)("wfwstpdsc") & ":" & auxObs
                    'End If
                    auxValues.gValue_Add("obs", auxObs)
                    auxValues.gValue_Add("obsmail", auxObs)
                    'auxValues.gValue_Add("solucion", auxBagValues.gValue_Get("pnllog_solucion", ""))
                    auxError &= auxClass.gWorkflow_GotoStep(auxValues)
                    If auxError = "" Then
                        auxDeleteDraft = True
                    Else
                        auxError = "<img src=imagenes/everror.png width=16px /> " & Replace(auxError, Chr(10), "<br /> ")
                    End If
                End If

            End If

        End If
        If auxError <> "" Then
            auxReturn = New clshrcBagValues
            auxReturn.gValue_Add("HRC_RESULTS_ERROR", auxError)
        End If
        If auxDeleteDraft Then
            auxClass.gDraft_Delete(auxdftgencod)
            auxClient.gObjectTmp_Delete(ViewState("principal_form"))
        End If
        auxClass.Conn.gConn_Close()
        Return auxReturn
    End Function

    Private Function gFormPNLLOG_Get(ByVal pClass As clsCusimDOC, _
                                     ByVal pHrcContext As clsHrcJSContext, _
                                     ByVal pIsAdmin As Boolean, _
                                     ByVal pWfwStpCod As enumWorkflowStep, _
                                     ByVal pOldValues As clshrcBagValues, _
                                     ByVal pControlError As clsHrcJSControlBasic, _
                                     ByVal pSucessURL As String) As clsHrcJSPanel
        'Panel-Roles
        Dim auxPanel As clsHrcJSPanel = Nothing
        Dim auxPanel_Mode As clsHrcJSPanel.enumPanelMode = clsHrcJSPanel.enumPanelMode.coOK_NOK
        auxPanel = New clsHrcJSPanel("pnlpanel_pnllog", "", pHrcContext.ButtonCSS, auxPanel_Mode)
        auxPanel.TooltipCSS = "obs-controles"
        auxPanel.ServerRaiseEvents = True
        ' auxPanel.Width = "500px"
        ' auxPanel.Height = "100px"
        auxPanel.Visible = False
        auxPanel.ServerStateMantain = True
        'wfwstpcodnext
        Dim auxWfwStpCodNext = New clsHrcJSHidden("pnllog_wfwstpcodnext", "pnllog_wfwstpcodnext")
        auxPanel.gControls_Add(auxWfwStpCodNext)
        Dim auxPnlLog_Action = New clsHrcJSHidden("pnllog_action", "pnllog_action")
        auxPanel.gControls_Add(auxPnlLog_Action)


        Dim auxClientCon As New imClientConnection


        'Fecha
        Dim auxFechaNow = New clsHrcJSLabel("pnllog_fechanow", "Fecha", "pnllog_fechanow", "")
        auxPanel.gControls_Add(auxFechaNow)

        'Accion
        Dim auxWfwStpDsc = New clsHrcJSLabel("pnllog_wfwstpdsc", "Acción", "pnllog_wfwstpdsc")
        auxPanel.gControls_Add(auxWfwStpDsc)

        'dsc
        Dim auxDsc As New clshrcJSTextBox("pnllog_dsc", "Comentario ", "pnllog_dsc", pHrcContext.ControlTextBoxCSS, 1000, "450px", "60px", True, "", "")
        auxPanel.gControls_Add(auxDsc)

        Dim auxNewRow As DataRow
        'Forzar
        Dim auxSiNoDT As New DataTable
        auxSiNoDT.Columns.Add(New DataColumn("cod", Type.GetType("System.Int32")))
        auxSiNoDT.Columns.Add(New DataColumn("dsc", Type.GetType("System.String")))
        auxNewRow = auxSiNoDT.NewRow
        auxNewRow("cod") = 0
        auxNewRow("dsc") = "No"
        auxSiNoDT.Rows.Add(auxNewRow)
        auxNewRow = auxSiNoDT.NewRow
        auxNewRow("cod") = 1
        auxNewRow("dsc") = "Sí"
        auxSiNoDT.Rows.Add(auxNewRow)
        Dim auxSecurity As clsHrcSecurityClient = pClass.Sec
        Dim auxConn As clsHrcConnClient = pClass.Conn
        If pWfwStpCod <> enumWorkflowStep.coWFWSTPDOC_DOCCreacion _
            And (pIsAdmin _
                Or (auxSecurity.gMember_IsInGroupByID(coGroupDocumentadorEditores) _
                Or auxSecurity.gMember_IsInGroupByID(coGroupDocumentadorAdministradores))) _
            Then
            Dim auxControlForce As clshrcJSComboBox
            auxControlForce = New clshrcJSComboBox(pControlID:="pnllog_force", pTitle:="Forzar", pFieldData:="pnllog_force", pCSSClass:=pHrcContext.ControlComboBoxCSS, pDT:=auxSiNoDT, pWidth:="100px", pHeight:="")
            auxControlForce.DisplayMode = clshrcJSComboBox.enumDisplayMode.Radio
            auxControlForce.DefaultValue = "'0'"
            auxControlForce.ServerStateMantain = True
            auxPanel.gControls_Add(auxControlForce)
        End If


        Dim auxParams As New clshrcBagValues
        Dim auxHTML As New clsHrcCodeHTML
        auxParams.gValue_Add("wfwstpcodnext", auxWfwStpCodNext.gJS_Value_Get)
        auxParams.gValue_Add("pnllog_action", "1") ' auxPnlLog_Action.gJS_Value_Get)
        Dim auxJSSucessMode As String = auxHTML.gJS_GotoURL(pSucessURL)
        auxPanel.gJSCommand_AddWithReadWriteMode(pCommandName:="VIEW", pJSCode:="", pTitle:="dd", _
                                        pRowVisibleStateEnabled:=True, pRowChangeTitleEnabled:=True, _
                                        pParams:=auxParams, pJSSucessCode:=auxJSSucessMode, _
                                        pJSReadErrorCode:=pControlError.gJS_Value_Set("auxResultError") & ";")
        'auxParams.gValue_Add("pnllog_actionmode", enumActionType.coDelete)
        auxPanel.gJSCommand_AddWithReadWriteMode(pCommandName:="DOC_DELETE", pJSCode:="", pTitle:="dd", _
                                     pRowVisibleStateEnabled:=True, pRowChangeTitleEnabled:=True, _
                                     pParams:=auxParams, pJSSucessCode:=auxJSSucessMode, _
                                     pJSReadErrorCode:=pControlError.gJS_Value_Set("auxResultError") & ";")
        'auxParams.gValue_Add("pnllog_actionmode", enumActionType.coModify)
        auxPanel.gJSCommand_AddWithReadWriteMode(pCommandName:="DOC_UPDATE", pJSCode:="", pTitle:="dd", _
                                     pRowVisibleStateEnabled:=True, pRowChangeTitleEnabled:=True, _
                                     pParams:=auxParams, pJSSucessCode:=auxJSSucessMode, _
                                     pJSReadErrorCode:=pControlError.gJS_Value_Set("auxResultError") & ";")
        'auxPanel.gJSCommand_AddWithReadWriteMode(pCommandName:="DOC_INSERT", pJSCode:="", pTitle:="dd", _
        '                             pRowVisibleStateEnabled:=True, pRowChangeTitleEnabled:=True, _
        '                             pParams:=auxParams, pJSSucessCode:=auxJSSucessMode, _
        '                             pJSReadErrorCode:="$('#" & lblerror.ClientID & "').html(auxResultError);")

        auxParams.gValue_Clear("pnllog_actionmode")
        auxPanel.gJSCommand_Add(pCommandName:="DOCUNLOCK", pJSCode:="")
        auxPanel.gJSCommand_Add(pCommandName:="DOCROLREAPPLY", pJSCode:="")

        'auxPanel.ButtonOK.EventOnClick = auxPanel.gJSCommand_Get("VIEW_OK") _
        '    & ";hrcConsole_log(auxResultError);"

        AddHandler auxPanel.EventCommandHandler, AddressOf gFormPNLLOG_CommandHandler
        Return auxPanel
    End Function
    Private Sub gFormPNLDOC_CommandHandler(ByVal pControl As clsHrcJSControlBasic, ByVal pValues As clshrcBagValues)
        Dim auxAction As String = pValues.gValue_Get("ACTION").ToString.ToUpper
        Dim auxReturnBagValues As clshrcBagValues
        Dim auxClass As New clsCusimDOC
        Dim auxConn As clsHrcConnClient = auxClass.Conn
        Select Case auxAction
            Case "VIEW", "DOC_UPDATE", "DOC_DELETE"
        End Select
    End Sub
    Private Sub gFormPNLLOG_CommandHandler(ByVal pControl As clsHrcJSControlBasic, ByVal pValues As clshrcBagValues)
        Dim auxAction As String = pValues.gValue_Get("ACTION").ToString.ToUpper
        Dim auxReturnBagValues As clshrcBagValues
        Dim auxClass As New clsCusimDOC
        Dim auxConn As clsHrcConnClient = auxClass.Conn
        Select Case auxAction
            Case "VIEW", "DOC_UPDATE", "DOC_DELETE"
                auxReturnBagValues = New clshrcBagValues


                'Pasar al otro estado.
                Dim auxError As String = ""
                'If hdncurVersion.Value <> "" Then
                '    auxConn = auxConn.gComponent_CreateInstance
                '    auxConn.gConn_Open()
                '    Dim auxHstId As Integer = auxConn.gConn_QueryValueInt("SELECT hstid FROM REQ_REQ WHERE cod=" & m_Cod)
                '    auxConn.gConn_Close()
                '    If auxHstId > Val(hdncurVersion.Value) Then
                '        auxError &= "Esta visualizando una versión antigua.Cierre este requerimiento, espere 10 minutos y vuelva a ingresar."
                '    End If
                'End If
                If auxError = "" Then
                    auxError = gData_Update()
                End If
                If auxError = "" Then
                    Dim auxDTValuesNew As clshrcBagValues = pControl.Parentcontrol.BagValues.gValue_Get("DTVALUES_NEW")
                    auxReturnBagValues.gValue_Add("pnllog_fechanow", auxConn.gDate_GetNow.ToString("dd/MM/yyyy HH:mm"))
                    Dim auxWfwStpCodNext As enumWorkflowStep = -1
                    Dim auxPnlLog_Action As enumActionType '= pValues.gValue_Get("pnllog_action") ' enumWorkflowStep.coWFWSTPREQ_REQtomado
                    Select Case auxAction
                        Case "DOC_UPDATE"
                            auxPnlLog_Action = enumActionType.coModify
                            'Case "DOC_INSERT"
                            '    auxPnlLog_Action = enumActionType.coNew
                        Case "DOC_DELETE"
                            auxPnlLog_Action = enumActionType.coDelete
                        Case Else
                            auxWfwStpCodNext = Val(pValues.gValue_Get("wfwstpcodnext")) ' enumWorkflowStep.coWFWSTPREQ_REQtomado
                    End Select


                    Dim auxwfwstpdsc_obs As String = ""
                    Dim auxClientConn As New imClientConnection
                    Dim auxPrincipalForm As clsHrcJSPanel = pControl.Parentcontrol ' auxClientConn.gObjectTmp_Download(ViewState("principal_form"))
                    Dim auxBagValues As clshrcBagValues = auxPrincipalForm.BagValues
                    auxPrincipalForm.BagValues.gValue_Add("pnllog_wfwstpcodnext", auxWfwStpCodNext)

                    Dim auxWfwStpDsc_Msg As String = ""
                    Dim auxVersion As String = auxPrincipalForm.gControl_Get("version").gValue_Get
                    auxBagValues.gValue_Clear("pnllog_actionmode")
                    Select Case auxPnlLog_Action
                        Case enumActionType.coDelete
                            auxReturnBagValues.gValue_Add("pnllog_dsc_rowhide", 1)
                            auxReturnBagValues.gValue_Add("pnllog_force_rowhide", 1)
                            auxWfwStpDsc_Msg = "¿Confirma la eliminación TOTAL del documento?"
                            auxBagValues.gValue_Add("pnllog_actionmode", auxPnlLog_Action)
                        Case enumActionType.coNew, enumActionType.coNewFromOther
                            auxReturnBagValues.gValue_Add("pnllog_dsc_rowhide", 1)
                            auxReturnBagValues.gValue_Add("pnllog_force_rowhide", 1)
                            auxWfwStpDsc_Msg = "¿Confirma el nuevo documento?"
                            auxBagValues.gValue_Add("pnllog_actionmode", auxPnlLog_Action)
                        Case enumActionType.coModify
                            auxReturnBagValues.gValue_Add("pnllog_dsc_rowhide", 1)
                            auxReturnBagValues.gValue_Add("pnllog_force_rowhide", 1)
                            auxWfwStpDsc_Msg = "¿Confirma la actualización de datos?"
                            auxBagValues.gValue_Add("pnllog_actionmode", auxPnlLog_Action)
                            'auxReturnBagValues.gValue_Add("pnllog_wfwstpdsc_rowtitle", "")
                        Case Else

                            auxWfwStpDsc_Msg &= "<img src='" & auxClass.WebRootFolder & "imagenes/actsign.png' width=24px />"
                            Dim auxDocDsc As String = auxPrincipalForm.BagValues.gValue_Get("dsc")
                            auxWfwStpDsc_Msg &= "Confirmo la siguiente acción [" & hrcEntityDT_Q_WFWSTP_FindByKey(auxWfwStpCodNext)("wfwstpdsc").ToString.ToUpper & "] en el documento [" _
                                & auxDocDsc _
                                & "] versión [" & auxVersion & "]"

                            auxReturnBagValues.gValue_Add("pnllog_wfwstpdsc_obs", auxwfwstpdsc_obs)
                            auxBagValues.gValue_Add("pnllog_action", auxPnlLog_Action)
                    End Select
                    auxWfwStpDsc_Msg = "<span style=font-size:16px>" & auxWfwStpDsc_Msg & "</span>"
                    auxReturnBagValues.gValue_Add("pnllog_wfwstpdsc", auxWfwStpDsc_Msg)
                End If
                If auxError <> "" Then
                    auxReturnBagValues = New clshrcBagValues
                    auxReturnBagValues.gValue_Add("HRC_RESULTS_ERROR", auxError)
                End If
                Dim auxPnloptions_lblstatus As String = ""
            Case "CONTENIDOHTML_CHARGE"
                Dim auxPrincipalForm As clsHrcJSPanel = pControl
                Dim auxCuerpoID As String
                auxCuerpoID = auxPrincipalForm.BagValues.gValue_Get("contenido_html")
                If auxCuerpoID <> "" Then
                    Dim auxDoctipCod As Integer = Val(auxPrincipalForm.gControl_Get("tipcod").gValue_Get)
                    If auxDoctipCod > 0 Then
                        Dim auxrow As DataRow
                        auxrow = hrcEntityDT_DOC_DOCTIP_FindByKey(auxDoctipCod)
                        If auxrow IsNot Nothing Then
                            Dim auxClientCon As New imClientConnection
                            Dim auxCuerpo As String = auxClientCon.gTextTmp_Download(auxCuerpoID)
                            If auxCuerpo = "" Then
                                auxCuerpo = auxrow("templatebody").ToString.Trim
                                auxClientCon.gTextTmp_Upload(auxCuerpo, auxCuerpoID)
                            End If
                        End If
                    End If
                End If

            Case "VIEW_OK", "DOC_UPDATE_OK", "DOC_DELETE_OK"
                auxReturnBagValues = gFormPNLLOG_Confirm(pValues)
            Case "DOCUNLOCK"
                Dim auxPrincipalForm As clsHrcJSPanel = pControl.Parentcontrol
                Dim auxCod As Integer = auxPrincipalForm.BagValues.gValue_Get("cod")
                auxClass.gTRACE_add(pProCod:=auxCod, pTrcDsc:="El usuario " & auxClass.Sec.CurrentSecDsc & "[" & auxClass.Sec.CurrentSecCod & "] ha desbloqueado manualmente", pTrclevel:=5)
                auxClass.gEntity_DOC_DOC_Update(pcod:=auxCod, pwfwlocked:=False)

            Case "DOCROLREAPPLY"
                Dim auxPrincipalForm As clsHrcJSPanel = pControl.Parentcontrol
                Dim auxCod As Integer = auxPrincipalForm.BagValues.gValue_Get("cod")
                auxClass.gTRACE_add(pProCod:=auxCod, pTrcDsc:="El usuario " & auxClass.Sec.CurrentSecDsc & "[" & auxClass.Sec.CurrentSecCod & "] ha reaplicados los permisos de roles manualmente", pTrclevel:=5)
                auxClass.gDoc_ReApply(pCod:=auxCod, pUpdateWfwRoles:=True, pDocMbrLevelForced:=1)

        End Select
        auxClass.Conn.gConn_Close()
        If auxReturnBagValues IsNot Nothing Then
            Dim auxReturn As String = "[" & auxClass.Conn.gField_GetJSONString(auxReturnBagValues, True) & "]"
            pValues.gValue_Add("HRC_RESULTS", auxReturn)
        End If
    End Sub
    Private Class clsWorkflowButtons
        Public CSSClass As String = ""
        Public WorkflowStep As Integer
        Public Title As String
        Public ControlID As String
        Public WorkflowStepType As enumJobResult
        Public StartVisible As Boolean = True
        Public Sub New(ByVal pControlID As String, _
                       ByVal ptitle As String, _
                       ByVal pWorkflowStep As Integer, _
                       Optional ByVal pWorkflowStepType As enumJobResult = enumJobResult.coOK, _
                       Optional ByVal pStartVisible As Boolean = True)
            MyBase.New()
            ControlID = pControlID
            Title = ptitle
            WorkflowStep = pWorkflowStep
            WorkflowStepType = pWorkflowStepType
            StartVisible = pStartVisible
        End Sub
    End Class
    Private Function gFormDOC_Get(ByVal pHrcContext As clsHrcJSContext) As clshrcBagValues
        Dim auxHrcContext As clsHrcJSContext

        Dim auxClass As New clsCusimDOC
        auxClass.Conn.gConn_Open()
        Dim auxHTML As New clsHrcCodeHTML
        Dim auxConn As clsHrcConnClient = auxClass.Conn
        Dim auxSecurity As clsHrcSecurityClient = auxClass.Sec
        Dim auxDT As DataTable = Nothing
        Dim auxSidCod As Integer = -1
        Dim auxScript As String = ""
        Dim auxClientCon As New imClientConnection

        If Environment.MachineName = "A16WIN8x" Then
            auxHrcContext = auxClientCon.gObjectTmp_Download("test")
            If auxHrcContext Is Nothing Then
                auxHrcContext = New clsHrcJSContext("hrccontext", Session("conn"), Session("security"), Session("hrcAlerts"))
                auxClientCon.gObjectTmp_Upload(auxHrcContext, "test")
            End If
        Else
            auxHrcContext = pHrcContext
        End If
        Dim auxQueryStringValues As clshrcBagValues = auxHTML.gBagValues_GetFromQueryString(Request.QueryString.ToString)
        Dim auxMode As Integer
        auxMode = Val(auxQueryStringValues.gValue_Get("_mode_"))

        Dim auxView As Integer = 0
        ' If ViewState("_view_") Is Nothing Then
        auxView = Val(auxQueryStringValues.gValue_Get("_view_"))
        If auxView < 0 Then
            auxView = 0
        End If

        Dim auxHstGenCod As Integer = -1
        Dim auxDftGenCod As String = ""
        Dim auxTitle As String = ""
        Select Case auxView
            Case 1, 2, 3
            Case Else
                auxHstGenCod = Val(auxQueryStringValues.gValue_Get("hstparam1"))
                If auxDftGenCod = "" Then
                    If auxQueryStringValues.gValue_Get("param2") <> "" Then
                        auxDftGenCod = HttpUtility.UrlDecode(auxQueryStringValues.gValue_Get("param2"))
                    End If
                End If
        End Select
        'If auxSecurity.gSID_CheckAccess(enumAccessType.coSYSGlobalCambiarpermisos) Then
        Dim auxWfwLocked As Boolean = False
        Dim auxPermEdit As Boolean = False
        Dim auxPermNew As Boolean = False
        Dim auxPermDocVigente As Boolean = False
        Dim auxPermDocLector As Boolean = False
        Dim auxWfwStateCod As enumWorkflowStep = -1
        Dim auxIsViewDOCVIG As Boolean = False
        Dim auxPermNavegar As Boolean = False

        Dim auxIsAdmin As Boolean = False
        If auxConn.gField_GetBoolean(Session("isadmin")) Then
            auxIsAdmin = True
            auxPermEdit = True
            auxPermNavegar = True
        End If
        Dim auxCurrentCod As Integer = -1
        auxCurrentCod = Val(auxQueryStringValues.gValue_Get("param1"))
        If auxMode = enumActionType.coNewFromOther Then
            auxCurrentCod = 0
        End If
        Dim auxDscEditMode As Boolean = False

        Dim auxEmpCod As Integer = auxConn.gField_GetInt(Session("empcod"), -1)
        Dim auxDoctipCod As Integer = -1
        Dim auxTroCod As Integer = -1
        Dim auxCuerpoEdit As Boolean = False
        Dim auxCuerpoVisible As Boolean = False
        Dim auxNroEdit As Boolean = False
        Dim auxDocTipEdit As Boolean = False
        Dim auxProCodEdit As Boolean = False
        Dim auxUndcodEdit As Boolean = False
        Dim auxClacodEdit As Boolean = False
        Dim auxSiscodEdit As Boolean = False
        'Dim auxDscEdit As Boolean = False
        Dim auxDsc0Edit As Boolean = False
        Dim auxDsc1Edit As Boolean = False
        Dim auxObsEdit As Boolean = False
        Dim auxDocumentsEdit As Boolean = False
        Dim auxEditOptions As Boolean = False
        Dim auxEditOptions_Visible As Boolean = False
        Dim auxEditOptions_PublicacionAuto_Enabled As Boolean = False
        Dim auxPermCambiarEstado As Boolean = False
        Dim auxLecturaPendiente As Boolean = False
        Dim auxWfwMode As enumWfwMode = enumWfwMode.coStandard

        Dim auxFormBGColor As String = ""
        Dim auxAllUserCanCreatedDocuments As Boolean = auxClass.Conn.gField_GetBoolean(auxClass.gSystem_GetParameterByID(coSysParamIDDocAllUsersCanAddDocuments), True)
        'auxAllUserCanCreatedDocuments = True
        If auxAllUserCanCreatedDocuments Then
            auxWfwMode = enumWfwMode.coUserCreate
        End If

        Dim auxPanelRolesVisible As Boolean = False
        Dim auxDOC_DOC_UND_PermEditor As String = Session("DOC_DOC_Und_PermEditor")

        Dim auxFormCancel As Boolean = False
        If auxMode = enumActionType.coNew _
            Or auxMode = enumActionType.coNewFromOther Then
            auxDT = New DataTable
            If auxAllUserCanCreatedDocuments Then
                auxPermEdit = True
                auxPanelRolesVisible = False
            Else
                If auxDOC_DOC_UND_PermEditor <> "" Then
                    auxPermEdit = True
                End If
            End If

        Else
            Dim auxPROTablename As String = "DOC_DOC"
            Dim auxPROwhere As String = ""
            If auxIsViewDOCVIG Then
                auxPROTablename = "DOC_DOCVIG"
                auxPROwhere &= auxPROTablename & ".cod = " & auxCurrentCod
                auxPanelRolesVisible = False
            Else
                If auxHstGenCod >= 1 Then
                    auxPROTablename = "DOC_DOC_HST"
                    auxPROwhere &= "DOC_DOC.hsthidgencod = " & auxHstGenCod
                Else
                    auxPROwhere &= auxPROTablename & ".cod = " & auxCurrentCod & " AND (" & auxPROTablename & ".baja {#ISNULL#} OR " & auxPROTablename & ".baja=0)"
                End If
            End If
            auxDT = auxConn.gConn_Query("SELECT DOC_DOC.*," _
                     & "DOC_DOCTIP.dsc AS wildoctipdsc,DOC_DOCTIP.templatehead,DOC_SIS.dsc as sisdsc,DOC_APA.dsc as apadsc,DOC_PRO.dsc as prodsc,DOC_CLA.dsc as cladsc,DOC_DOCTIP.dsc as doctipdsc" _
                     & ",UND.dsc as unddsc,'' as proproponedsc" _
                     & ",DOC_DOC.wfwstatus as wfwstpcod" _
                     & ",'' as proproponeunddsc" _
                     & " ,(SELECT TOP 1 wfwstpdsc FROM Q_WFWSTP WHERE Q_WFWSTP.wfwstpcod = DOC_DOC.wfwstatus) as wfwstpdsc  " _
                     & ",(SELECT TOP 1 DOC_DOCVIG.fecha FROM DOC_DOCVIG WHERE cod=DOC_DOC.cod) as fechavigente " _
                     & " FROM " & auxPROTablename & " DOC_DOC " _
                     & " LEFT JOIN DOC_SIS ON DOC_DOC.siscod = DOC_SIS.cod " _
                     & " LEFT JOIN DOC_PRO ON DOC_DOC.procod = DOC_PRO.cod " _
                     & " LEFT JOIN DOC_APA ON DOC_PRO.cod = DOC_APA.cod " _
                     & " LEFT JOIN DOC_CLA ON DOC_DOC.clacod = DOC_CLA.cod " _
                     & " LEFT JOIN DOC_DOCTIP ON DOC_DOC.doctipcod = DOC_DOCTIP.cod " _
                     & " LEFT JOIN UND ON DOC_DOC.undcod = UND.cod " _
                     & " WHERE " & auxPROwhere)

            If auxDT.Rows.Count = 0 Then
                auxFormCancel = True
                Exit Function
            Else
                auxTroCod = auxConn.gField_GetInt(auxDT.Rows(0)("trocodcustom"))
                auxSidCod = auxConn.gField_GetInt(auxDT.Rows(0)("qsidcod"))
                If auxSecurity.gSID_CheckAccess(auxSidCod, enumAccessType.coSYSGlobalCambiarpermisos) Then
                    auxIsAdmin = True
                    auxPermEdit = True
                    auxPermNavegar = True
                End If

                If Not auxIsAdmin Then
                    If auxSecurity.gSID_CheckAccess(auxSidCod, enumAccessType.coSYSGlobalCambiarestado _
                                                & "," & enumAccessType.coSYSConfirmarlectura) Then
                        'Tiene permiso para cambiar el estado actual
                        auxPermEdit = True
                    End If
                    If auxSecurity.gSID_CheckAccess(auxSidCod, enumAccessType.coDOCDOCVIGDocumentosvigentesVer) Then
                        'Lectores-Tiene permiso para ver el vigente
                        auxPermDocVigente = True
                    End If
                    If auxSecurity.gSID_CheckAccess(auxSidCod, enumAccessType.coSYSGlobalLeer) Then
                        'Lectores-Tiene permiso para ver el vigente
                        auxPermDocLector = True
                    End If

                    If auxPermDocLector = False And auxPermDocVigente = False And auxPermEdit = False Then
                        If auxSecurity.gSID_CheckAccess(auxSidCod, glPermRead) = False Then
                            auxFormCancel = True
                        End If
                    End If

                    If auxHstGenCod > 0 Then
                        If auxSecurity.gSID_CheckAccess(auxSidCod, enumAccessType.coSYSGlobalModificar) = False Then
                            auxFormCancel = True
                        End If
                    Else

                        If auxIsViewDOCVIG Then
                            If auxSecurity.gSID_CheckAccess(auxSidCod, enumAccessType.coSYSGlobalLeer _
                                                            & "," & enumAccessType.coSYSImprimircopiasnocontroladas & "," & enumAccessType.coSYSImprimircopiascontroladas _
                                                            & "," & enumAccessType.coDOCDOCVIGDocumentosvigentesVer) = False Then
                                auxFormCancel = True
                            End If
                        Else
                            auxPermNavegar = auxSecurity.gSID_CheckAccess(auxSidCod, enumAccessType.coSYSGlobalModificar)
                            If auxPermDocLector = False And auxPermDocVigente = False And auxPermEdit = False Then
                                If auxSecurity.gSID_CheckAccess(auxSidCod, enumAccessType.coSYSGlobalLeer _
                                                          & "," & enumAccessType.coSYSGlobalModificar _
                                                          & "," & enumAccessType.coSYSGlobalCambiarestado _
                                                          & "," & enumAccessType.coSYSCreador _
                                                          & "," & enumAccessType.coSYSImprimircopiasnocontroladas & "," & enumAccessType.coSYSImprimircopiascontroladas _
                                                          & "," & enumAccessType.coSYSConfirmarlectura _
                                                          & "," & enumAccessType.coDOCDOCVIGDocumentosvigentesVer) = False Then
                                    auxFormCancel = True
                                End If
                            End If

                        End If
                    End If
                End If
                auxPermCambiarEstado = auxIsAdmin Or auxSecurity.gSID_CheckAccess(auxSidCod, enumAccessType.coSYSGlobalCambiarestado)
                If auxConn.gConn_Query("SELECT DOC_DOCSGN.cod FROM DOC_DOCSGN " _
                                 & " LEFT JOIN DOC_DOCLOG ON DOC_DOCSGN.doclogcod= DOC_DOCLOG.cod" _
                                 & " WHERE DOC_DOCLOG.wfwstepnext = " & enumWorkflowStep.coWFWSTPDOC_DOCDocumentovigente _
                                 & " AND DOC_DOCLOG.doccod=" & auxCurrentCod & " AND DOC_DOCSGN.empcod=" & auxEmpCod).Rows.Count > 0 Then
                    auxLecturaPendiente = True
                End If
                auxWfwStateCod = auxConn.gField_GetInt(auxDT.Rows(0)("wfwstpcod"), enumWorkflowStep.coWFWSTPDOC_DOCCreacion)
                auxDoctipCod = auxConn.gField_GetInt(auxDT.Rows(0)("doctipcod"), -1)

                If auxConn.gField_GetBoolean(auxDT.Rows(0)("wfwlocked"), False) And auxPermEdit And auxHstGenCod <= 0 Then
                    auxPermEdit = False
                    auxWfwLocked = True
                End If
                If auxHstGenCod > 0 Then
                    '    tablaglobal.BgColor = "#F8F8F8"
                    auxFormBGColor = "background-color:#F8F8F8;"
                    auxPermEdit = False
                    auxWfwLocked = False
                    auxMode = enumActionType.coViewDetail
                End If
            End If
        End If
        If auxHstGenCod < 1 And (auxMode = enumActionType.coModify Or auxMode = enumActionType.coViewDetail) _
            And auxWfwStateCod <> enumWorkflowStep.coWFWSTPDOC_DOCCreacion Then
            If auxIsAdmin _
             Or auxClass.Sec.gSID_CheckAccess(auxSidCod, enumAccessType.coSYSGlobalModificar _
                                              & "," & enumAccessType.coSYSGlobalCambiarpermisos) Then
                auxPanelRolesVisible = True
            ElseIf auxWfwStateCod = enumWorkflowStep.coWFWSTPDOC_DOCSolicitudnuevodocumento And _
                auxClass.Sec.gSID_CheckAccess(auxSidCod, enumAccessType.coSYSGlobalCambiarestado) Then
                auxPanelRolesVisible = True
            End If
        End If
        If auxWfwMode = enumWfwMode.coStandard Then
            auxPanelRolesVisible = True
        End If
        If auxFormCancel Then
            'Cancelar el formulario
            auxScript = gForm_Cancel(auxQueryStringValues)
            ScriptManager.RegisterStartupScript(Me, Me.GetType, "hrcFormClose", _
                                       auxScript, True)
        End If
        Dim auxPermRolesEdit As Boolean = False
        Select Case auxMode
            Case enumActionType.coNew, enumActionType.coNewFromOther, enumActionType.coModify
                Select Case auxWfwStateCod
                    Case enumWorkflowStep.coWFWSTPDOC_DOCCreacion
                        If auxAllUserCanCreatedDocuments Then
                            auxPermRolesEdit = False
                        Else
                            auxPermRolesEdit = auxConn.gField_GetBoolean(Session("DOC_DOC_New_PermEditorRoles"), False)
                        End If
                    Case Else
                        auxPermRolesEdit = auxConn.gField_GetBoolean(Session("DOC_DOC_New_PermEditorRoles"), False)
                End Select
        End Select
        Dim auxObsVisible As Boolean = auxConn.gField_GetBoolean(auxClass.gSystem_GetParameterByID(coSysParamIDObsVisible), False)
        Dim auxSisCod_Mandatory As Integer = -1
        Dim auxRows() As DataRow = hrcEntityDT_DOC_SIS.Select("COD > 0 AND (baja IS NULL)")
        If auxRows.Count = 1 Then
            auxSisCod_Mandatory = auxRows(0)("cod")
        End If

        Dim auxDfdGenCod As String = ""
        Dim auxDfdGenCod_TRO As String = ""
        Select Case auxMode
            Case enumActionType.coModify, enumActionType.coDelete
                If auxPermEdit Then
                    auxDfdGenCod = auxClass.gDraft_Create("Borrador", enumEntities.coEntityDOC_DOC)
                    auxClass.gEntity_DOC_DOC_UpdateInDraft(pdftdidgencod:=auxDfdGenCod, _
                                                                    pcod:=auxCurrentCod)
                    auxDfdGenCod_TRO = auxClass.gDraft_Create("Borrador", enumEntities.coEntityDOC_TRO)
                    auxClass.gEntity_DOC_TRO_UpdateInDraft(pdftdidgencod:=auxDfdGenCod_TRO, _
                                                            pcod:=auxTroCod)
                    For Each auxRow As DataRow In auxClass.gTRO_Get(pTroCod:=auxTroCod).Rows
                        auxClass.gEntity_DOC_TROROL_UpdateInDraft(pdftdidgencod:=auxDfdGenCod_TRO, _
                                                            pcod:=auxRow("cod"))
                        auxClass.gEntity_DOC_TROROLEMP_UpdateInDraft(pdftdidgencod:=auxDfdGenCod_TRO, _
                                                            ptrorolcod:=auxRow("cod"))
                        auxClass.gEntity_DOC_TROROLUND_UpdateInDraft(pdftdidgencod:=auxDfdGenCod_TRO, _
                                                            ptrorolcod:=auxRow("cod"))
                        auxClass.gEntity_DOC_TROROLEQU_UpdateInDraft(pdftdidgencod:=auxDfdGenCod_TRO, _
                                                            ptrorolcod:=auxRow("cod"))
                        auxClass.gEntity_DOC_TROROLDYNGRP_UpdateInDraft(pdftdidgencod:=auxDfdGenCod_TRO, _
                                                            ptrorolcod:=auxRow("cod"))
                    Next
                End If
            Case enumActionType.coNew, enumActionType.coNewFromOther
                auxWfwStateCod = enumWorkflowStep.coWFWSTPDOC_DOCCreacion
                auxDfdGenCod = auxClass.gDraft_Create("Borrador", enumEntities.coEntityDOC_DOC)
                auxDfdGenCod_TRO = auxClass.gDraft_Create("Borrador", enumEntities.coEntityDOC_TRO)
                Dim auxContentTypeIDDefault As Integer = auxConn.gField_GetInt(auxClass.gSystem_GetParameterByID(coSysParamIDDocContentTypeIDDefault))
                If auxContentTypeIDDefault < 1 Then
                    auxContentTypeIDDefault = clsHrcConnClient.enumMimeTypes.coHTML
                End If
                auxCurrentCod = auxClass.gEntity_DOC_DOC_InsertInDraft(pdftdidgencod:=auxDfdGenCod, pwfwstatus:=enumWorkflowStep.coWFWSTPDOC_DOCCreacion, _
                                                                psiscod:=auxSisCod_Mandatory, _
                                                                pdsc:=" ", pdsc0:="", pdsc1:="", pdsc2:="", pprocod:=-1, pdoctipcod:=-1, pnro:=-1, pclacod:=-1, _
                                                                pversion:=1, pcuerpo:="", pfecha:=Nothing, pobs:="", pundcod:=-1, peprcod:=-1, porden:=50, _
                                                                pidentificador:="", pcopianro:=0, pwfwlocked:=False, pbaja:=False, pwfwmode:=auxWfwMode, _
                                                                ptrocodcustom:=-1, ptrocodcustomenabled:=Nothing, ptrocod:=-1, ppublicacionauto:=Nothing, _
                                                                pcontenttypeid:=auxContentTypeIDDefault _
                                                                , pdocsupcod:=-1, parchivo:=-1)
                auxClass.gEntity_DOC_DOC_SystemUpdate(pcod:=auxCurrentCod, pdftdidgencod:=auxDfdGenCod, pwfwstatus:=enumWorkflowStep.coWFWSTPDOC_DOCCreacion)
                auxTroCod = auxClass.gEntity_DOC_TRO_InsertInDraft(pdftdidgencod:=auxDfdGenCod_TRO, _
                                                pdsc:="Roles", pbaja:=False, pcustom:=True)
                auxClass.gEntity_DOC_TRO_SystemUpdate(pdftdidgencod:=auxDfdGenCod_TRO, pcod:=auxTroCod, pcustom:=True)
                Dim auxTroRolCod As Integer
                Dim auxEquCod As Integer = Val(auxClass.gSystem_GetParameterByID(coSysParamIDEQUReceptores))
                auxTroRolCod = auxClass.gEntity_DOC_TROROL_InsertInDraft(ptrocod:=auxTroCod, _
                                                                        pdftdidgencod:=auxDfdGenCod_TRO, _
                                                                        prolcod:=enumRoles.coReceptor, _
                                                                        prolcodtype:=enumEntities.coEntityDOC_EQU)
                auxClass.gEntity_DOC_TROROLEQU_InsertInDraft(ptrorolcod:=auxTroRolCod, _
                                                            pdftdidgencod:=auxDfdGenCod_TRO, _
                                                            pequcod:=auxEquCod)

                auxTroRolCod = auxClass.gEntity_DOC_TROROL_InsertInDraft(ptrocod:=auxTroCod, _
                                                                        pdftdidgencod:=auxDfdGenCod_TRO, _
                                                                        prolcod:=enumRoles.coAprobador, _
                                                                        prolcodtype:=enumEntities.coEntityEMP)
                'auxClass.gEntity_DOC_TROROLEMP_InsertInDraft(ptrorolcod:=auxTroRolCod, _
                '                                                  pdftdidgencod:=auxDfdGenCod_TRO, _
                '                                                   pempcod:=-1)

                If auxPermRolesEdit And auxPanelRolesVisible Then
                    auxTroRolCod = auxClass.gEntity_DOC_TROROL_InsertInDraft(ptrocod:=auxTroCod, _
                                                                        pdftdidgencod:=auxDfdGenCod_TRO, _
                                                                        prolcod:=enumRoles.coEditor, _
                                                                        prolcodtype:=enumEntities.coEntityEMP)
                    auxClass.gEntity_DOC_TROROLEMP_InsertInDraft(ptrorolcod:=auxTroRolCod, _
                                                                    pdftdidgencod:=auxDfdGenCod_TRO, _
                                                                        pempcod:=-1)
                Else
                    'No tiene permiso para editar los roles
                    'Agrega el mismo colaborador como editor
                    auxTroRolCod = auxClass.gEntity_DOC_TROROL_InsertInDraft(ptrocod:=auxTroCod, _
                                                                        pdftdidgencod:=auxDfdGenCod_TRO, _
                                                                        prolcod:=enumRoles.coEditor, _
                                                                    prolcodtype:=enumEntities.coEntityEMP)
                    auxClass.gEntity_DOC_TROROLEMP_InsertInDraft(ptrorolcod:=auxTroRolCod, _
                                                                    pdftdidgencod:=auxDfdGenCod_TRO, _
                                                                        pempcod:=auxEmpCod)
                End If
                auxTroRolCod = auxClass.gEntity_DOC_TROROL_InsertInDraft(ptrocod:=auxTroCod, _
                                                                        pdftdidgencod:=auxDfdGenCod_TRO, _
                                                                    prolcod:=enumRoles.coLector, _
                                                                    prolcodtype:=enumEntities.coEntityEMP)
                auxClass.gEntity_DOC_TROROLEMP_InsertInDraft(ptrorolcod:=auxTroRolCod, _
                                                                    pdftdidgencod:=auxDfdGenCod_TRO, _
                                                                    pempcod:=-1)

                auxTroRolCod = auxClass.gEntity_DOC_TROROL_InsertInDraft(ptrocod:=auxTroCod, _
                                                                    pdftdidgencod:=auxDfdGenCod_TRO, _
                                                                    prolcod:=enumRoles.coPublicador, _
                                                                    prolcodtype:=enumEntities.coEntityEMP)
                auxClass.gEntity_DOC_TROROLEMP_InsertInDraft(ptrorolcod:=auxTroRolCod, _
                                                                    pdftdidgencod:=auxDfdGenCod_TRO, _
                                                                    pempcod:=-1)

                auxTroRolCod = auxClass.gEntity_DOC_TROROL_InsertInDraft(ptrocod:=auxTroCod, _
                                                                        pdftdidgencod:=auxDfdGenCod_TRO, _
                                                                    prolcod:=enumRoles.coCancelador, _
                                                                    prolcodtype:=enumEntities.coEntityEMP)
                auxClass.gEntity_DOC_TROROLEMP_InsertInDraft(ptrorolcod:=auxTroRolCod, _
                                                                    pdftdidgencod:=auxDfdGenCod_TRO, _
                                                                    pempcod:=-1)

                auxTroRolCod = auxClass.gEntity_DOC_TROROL_InsertInDraft(ptrocod:=auxTroCod, _
                                                                        pdftdidgencod:=auxDfdGenCod_TRO, _
                                                                    prolcod:=enumRoles.coRevisor, _
                                                                    prolcodtype:=enumEntities.coEntityEMP)
                auxClass.gEntity_DOC_TROROLEMP_InsertInDraft(ptrorolcod:=auxTroRolCod, _
                                                                    pdftdidgencod:=auxDfdGenCod_TRO, _
                                                                    pempcod:=-1)

                auxTroRolCod = auxClass.gEntity_DOC_TROROL_InsertInDraft(ptrocod:=auxTroCod, _
                                                                    pdftdidgencod:=auxDfdGenCod_TRO, _
                                                                    prolcod:=enumRoles.coImpresor, _
                                                                    prolcodtype:=enumEntities.coEntityEMP)
                auxClass.gEntity_DOC_TROROLEMP_InsertInDraft(ptrorolcod:=auxTroRolCod, _
                                                                    pdftdidgencod:=auxDfdGenCod_TRO, _
                                                                    pempcod:=-1)

        End Select

        Dim auxFormPNLLOG As clsHrcJSPanel = Nothing

        Dim auxPrincipalPanel As clsHrcJSPanel
        Dim auxPrincipalPanelID As String = ViewState("principal_form")
        Dim auxDTValues_Old As clshrcBagValues
        If auxPrincipalPanelID = "" Then
            auxDTValues_Old = New clshrcBagValues
            If auxDT.Rows.Count <> 0 Then
                auxDTValues_Old.gValue_Add(auxConn.gField_GetBagValuesFromArray(auxDT.Rows(0).ItemArray, auxDT.Columns))
                auxDTValues_Old.gValues_ClearNoValues()
                If auxClass.Conn.gField_GetInt(auxDT.Rows(0)("nro"), -1) < 1 Then
                    auxDTValues_Old.gValue_Add("nro", "")
                End If
                If auxConn.gField_GetInt(auxDTValues_Old.gValue_Get("trocodcustom")) > 0 _
                        And auxConn.gField_GetInt(auxDTValues_Old.gValue_Get("trocodcustom")) = auxConn.gField_GetInt(auxDTValues_Old.gValue_Get("trocod")) Then
                    auxDTValues_Old.gValue_Add("trocodcustomenabled", True)
                End If
                If auxConn.gField_GetInt(auxDT.Rows(0)("docsupcod"), -1) > 0 Then
                    auxDTValues_Old.gValue_Add("docsupcod_dsc", auxConn.gConn_QueryValueString("SELECT (identificador+'-'+dsc) as dsc" _
                            & " FROM DOC_DOC " _
                            & " WHERE cod =" & auxDT.Rows(0)("docsupcod")))
                End If
            End If
            auxPrincipalPanel = New clsHrcJSPanel("principal_form", "", "", clsHrcJSPanel.enumPanelMode.coNoButton)
            AddHandler auxPrincipalPanel.EventCommandHandler, AddressOf gFormPNLLOG_CommandHandler
            'auxPrincipalPanel.BagValues.gValue_Add("q_cod", m_Cod)
            auxPrincipalPanel.BagValues.gValue_Add("cod", auxCurrentCod)
            If auxWfwStateCod = enumWorkflowStep.coWFWSTPDOC_DOCCreacion Then
                If auxAllUserCanCreatedDocuments Then
                    auxPrincipalPanel.BagValues.gValue_Add("wfwmode", CInt(enumWfwMode.coUserCreate)) 'el usuario solicita
                Else
                    auxPrincipalPanel.BagValues.gValue_Add("wfwmode", CInt(enumWfwMode.coStandard)) 'el editor de la unidad solicita o crea
                End If
            End If
            auxPrincipalPanel.BagValues.gValue_Add("DTVALUES_OLD", auxDTValues_Old)
            auxPrincipalPanel.BagValues.gValue_Add("DTVALUES_NEW", New clshrcBagValues(auxDTValues_Old))
            auxPrincipalPanel.BagValues.gValue_Add("mode", auxMode)
            'auxPrincipalPanel.ServerStateMantain = True

            auxPrincipalPanel.BagValues.gValue_Add("currentempcod", auxEmpCod)
            auxPrincipalPanel.BagValues.gValue_Add("form_JScancel", gForm_Cancel(auxHTML.gBagValues_GetFromQueryString(Request.QueryString.ToString)))
            auxPrincipalPanel.BagValues.gValue_Add("wfwstpcod", auxWfwStateCod)
            auxPrincipalPanel.BagValues.gValue_Add("dftgencod", auxDfdGenCod)
            auxPrincipalPanel.BagValues.gValue_Add("dftgencod_TRO", auxDfdGenCod_TRO)
            auxPrincipalPanelID = auxClass.Conn.gField_GetUniqueID
            ViewState("principal_form") = auxPrincipalPanelID
            auxClientCon.gObjectTmp_Upload(auxPrincipalPanel, auxPrincipalPanelID)




        Else
            auxPrincipalPanel = auxClientCon.gObjectTmp_Download(auxPrincipalPanelID)
            auxDfdGenCod = auxPrincipalPanel.BagValues.gValue_Get("dftgencod")
            auxDfdGenCod_TRO = auxPrincipalPanel.BagValues.gValue_Get("dftgencod_tro")
        End If

        'Bloqueo
        Dim auxControlError As New clsHrcJSLabel("lblerror", pCSSClass:="error")
        auxPrincipalPanel.gControls_Add(auxControlError)



        'pnllog
        auxFormPNLLOG = gFormPNLLOG_Get(auxClass, auxHrcContext, auxIsAdmin, auxWfwStateCod, auxDTValues_Old, auxControlError, auxPrincipalPanel.BagValues.gValue_Get("form_JScancel"))
        auxPrincipalPanel.gControls_Add(auxFormPNLLOG)
        auxHrcContext.gObjectTmp_UploadControl(auxFormPNLLOG)

        If auxWfwLocked Then
            auxControlError.gValue_Set("(*) El requerimiento se encuentra en proceso de cambio de estado y por ello se ha bloqueado su edición. Espere 15 minutos y vuelva a ingresar.")
            Dim auxControlButton_Locked As New clsHrcJSButton("cmdunlocked", "Desbloquear", auxHrcContext.ButtonCSS)
            auxPrincipalPanel.gControls_Add(auxControlButton_Locked)
            auxControlButton_Locked.EventOnClick = "if (confirm('Confirma el desbloqueo?')){" _
                & auxFormPNLLOG.gJSCommand_Get(pCommandName:="DOCUNLOCK") & ";" _
                & auxPrincipalPanel.BagValues.gValue_Get("form_JScancel") & ";" _
                & "};return false;"
        End If

        Dim auxControlButton_Versions_Visible As Boolean = False
        Dim auxControlButton_Log_Visible As Boolean = False
        Dim auxcontrolbutton_PROPRN_Visible As Boolean = False
        Dim auxControlButton_PERM_Visible As Boolean = False
        Dim auxControlButton_Trace_Visible As Boolean = False
        Dim auxControlButton_ROL_EMP_Visible As Boolean = False
        Dim auxControlButton_Reapply_Visible As Boolean = False
        Dim auxControlButton_DOCREF_Visible As Boolean = False
        Dim auxControlButton_DOCSIGN_Visible As Boolean = False
        Dim auxControlButton_Refresh_Visible As Boolean = False
        Dim auxControlButton_ItemUpdate_Visible As Boolean = False
        Dim auxcontrolButton_ConfirmInsert_visible As Boolean = False
        Dim auxcontrolButton_ConfirmUpdate_visible As Boolean = False
        Dim auxcontrolButton_ConfirmDelete_visible As Boolean = False
        Dim auxControlButton_PDFView_Visible As Boolean = False
        Dim auxControlButton_VersionsChanges_Visible As Boolean = False
        Dim auxcontrolButton_CopyControled As Boolean = False
        Dim auxcontrolButton_CopyNoControled As Boolean = False
        'Carga valores
        If auxHstGenCod > 0 Then
            auxPermEdit = False
            'cmdPROPERM.Visible = False
            'cmdPROTRACE.Visible = False
            'cmdROL_EMP.Visible = False
            'cmdReapplyRoles.Visible = False
        ElseIf (auxMode = enumActionType.coViewDetail Or auxMode = enumActionType.coModify) _
            And auxIsViewDOCVIG = False Then
            'cmdPROVER.Visible = True
            If auxPermNavegar Or auxIsAdmin Or auxPermEdit Then
                auxControlButton_Versions_Visible = True
                auxControlButton_Log_Visible = True
                auxcontrolbutton_PROPRN_Visible = True
            End If
            If auxIsAdmin Then
                auxControlButton_PERM_Visible = True
                auxControlButton_Trace_Visible = True
                auxControlButton_ROL_EMP_Visible = True
                If auxMode = enumActionType.coModify _
                    Or auxMode = enumActionType.coViewDetail Then
                    auxControlButton_Reapply_Visible = True
                End If
            End If
            If auxPermEdit Then
                auxControlButton_DOCREF_Visible = True
                auxControlButton_DOCSIGN_Visible = True
            End If
            If auxMode = enumActionType.coViewDetail Then
                auxControlButton_Refresh_Visible = True
                If auxWfwStateCod <> enumWorkflowStep.coWFWSTPDOC_DOCCreacion Then
                    auxCuerpoVisible = True
                End If
                If auxPermEdit And (auxWfwStateCod = enumWorkflowStep.coWFWSTPDOC_DOCCreacion Or auxWfwStateCod = enumWorkflowStep.coWFWSTPDOC_DOCnuevaversion Or auxWfwStateCod = enumWorkflowStep.coWFWSTPDOC_DOCEdicion) Then
                    auxControlButton_ItemUpdate_Visible = True
                End If
            End If
        End If
        If auxMode = enumActionType.coNew Then
            auxcontrolButton_ConfirmInsert_visible = True
            If auxPermEdit Then
                'auxDTValues_Old.gValue_Add("wfw")
                'lblRwfwstatus.Text = "Creación"
                auxDTValues_Old.gValue_Add("version", "----")
                auxDTValues_Old.gValue_Add("identificador", "----")
                Dim auxDocNroEditEnabled As Boolean = auxClass.Conn.gField_GetBoolean(auxClass.gSystem_GetParameterByID(coSysParamIDDOCNroEditEnabled), False)
                If auxDocNroEditEnabled Then
                    auxDTValues_Old.gValue_Add("nro", auxClass.Conn.gConn_QueryValue("SELECT MAX(nro) FROM DOC_DOC WHERE cod > 0 ", 0) + 1)
                End If
                auxDTValues_Old.gValue_Add("fecha", "---")
                auxDTValues_Old.gValue_Add("nro", "---")
            End If
        End If
        If auxMode = enumActionType.coModify And auxPermEdit Then
            auxcontrolButton_ConfirmUpdate_visible = True
        End If
        If auxMode = enumActionType.coDelete _
            And auxWfwStateCod = enumWorkflowStep.coWFWSTPDOC_DOCCreacion _
            And auxPermEdit Then
            auxcontrolButton_ConfirmDelete_visible = True
        End If
        Dim auxPanelRoles_CopyTROVisible = False
        If (auxPermEdit Or auxPermDocVigente Or auxPermDocLector) _
            And (auxMode = enumActionType.coModify Or auxMode = enumActionType.coNew) Then
            'Control de pasos
            '
            'Or (auxClass.Sec.gSID_CheckAccess(auxSidCod, enumAccessType.coSYSCambiarroles) And auxConn.gField_GetBoolean(hrcEntityDT_DOC_DOCTIP.Select("cod=" & auxDoctipCod)(0)("permcambia"), False)) Then
            If auxIsAdmin _
                Or auxClass.Sec.gSID_CheckAccess(auxSidCod, enumAccessType.coSYSGlobalModificar & "," & enumAccessType.coSYSGlobalCambiarpermisos) _
                Then
                auxUndcodEdit = True
            End If
            Select Case auxWfwStateCod
                Case enumWorkflowStep.coWFWSTPDOC_DOCCreacion
                    Select Case auxMode
                        Case enumActionType.coDelete
                            If auxPermEdit Then
                                auxcontrolButton_ConfirmDelete_visible = True
                            End If
                        Case enumActionType.coModify, enumActionType.coNew
                            If auxPermEdit And auxMode = enumActionType.coModify Then
                                auxcontrolButton_ConfirmDelete_visible = True
                            End If
                            'cmdFormViewConfirmUpdate.Visible = True
                            'If coSystemType <> 175 Then
                            auxPanelRoles_CopyTROVisible = auxPermRolesEdit
                            'End If
                            '                            grdRecursos.Columns(grdRecursos.Columns.Count - 1).Visible = auxPermRolesEdit
                            '                            grdRecursos.ShowFooter = auxPermRolesEdit

                            auxUndcodEdit = True
                            If auxConn.gField_GetBoolean(auxClass.gSystem_GetParameterByID(coSysParamIDDOCNroEditEnabled), False) Then
                                Select Case auxWfwMode
                                    Case enumWfwMode.coStandard
                                        auxNroEdit = True
                                End Select
                            End If

                            auxDocTipEdit = True
                            auxProCodEdit = True
                            auxSiscodEdit = True
                            auxClacodEdit = True
                            auxDsc0Edit = True
                            auxObsEdit = True
                            auxDocumentsEdit = True
                            If auxAllUserCanCreatedDocuments Then
                                auxCuerpoEdit = True
                            End If
                            'grdPRODOC.Columns(grdPRODOC.Columns.Count - 1).Visible = True
                            'grdPRODOC.ShowFooter = True

                            If auxMode = enumActionType.coNew Then
                                auxcontrolButton_ConfirmInsert_visible = True
                            End If
                    End Select
                Case enumWorkflowStep.coWFWSTPDOC_DOCnuevaversion
                    Select Case auxMode
                        Case enumActionType.coDelete
                            'If m_PermEdit Then
                            'cmdFormViewConfirmDelete.Visible = True
                            'End If
                        Case enumActionType.coModify

                            auxDocumentsEdit = True

                            Select Case auxWfwMode
                                Case enumWfwMode.coStandard
                                    auxDocTipEdit = True
                                    If coSystemType <> 175 Then
                                        auxPanelRoles_CopyTROVisible = auxPermRolesEdit
                                    End If
                                    auxDoctipCod = True
                                    auxProCodEdit = True
                                    auxSiscodEdit = True
                                    auxClacodEdit = True
                                    auxObsEdit = True
                                    auxUndcodEdit = True
                                Case enumWfwMode.coUserCreate
                                    'If auxAllUserCanCreatedDocuments Then
                                    '    auxCuerpoEdit = True
                                    'End If
                                    auxCuerpoEdit = True
                                    If auxPermCambiarEstado Then
                                        auxCuerpoEdit = True
                                        '    Dim auxResult() As DataRow
                                        '    auxResult = hrcEntityDT_DOC_DOCTIP.Select("cod=" & auxDT.Rows(0)("doctipcod"))
                                        '    If auxResult.Count <> 0 Then
                                        '        If auxClass.Conn.gField_GetBoolean(auxResult(0)("permedicioncambiadsc"), False) Then
                                        '            auxDsc0Edit = True
                                        '        End If
                                        '        If auxClass.Conn.gField_GetBoolean(auxResult(0)("permedicioncambiaotros"), False) Then
                                        '            'auxDocTipEdit = True
                                        '            auxProCodEdit = True
                                        '            auxSiscodEdit = True
                                        '            auxClacodEdit = True
                                        '            auxObsEdit = True
                                        '            auxUndcodEdit = True
                                        '        End If
                                        '    End If
                                    End If
                            End Select
                    End Select

                Case enumWorkflowStep.coWFWSTPDOC_DOCEdicion, enumWorkflowStep.coWFWSTPDOC_DOCSolicitudnuevodocumento
                    Select Case auxMode
                        Case enumActionType.coModify
                            If auxPermCambiarEstado Then
                                auxCuerpoEdit = True
                                auxDocumentsEdit = True
                                If auxWfwStateCod = enumWorkflowStep.coWFWSTPDOC_DOCSolicitudnuevodocumento Then
                                    auxDsc0Edit = True
                                    auxProCodEdit = True
                                    auxSiscodEdit = True
                                    auxClacodEdit = True
                                    auxDocTipEdit = True
                                    auxObsEdit = True
                                    auxUndcodEdit = True
                                    auxPermRolesEdit = True
                                    auxPanelRoles_CopyTROVisible = True
                                    If auxConn.gField_GetBoolean(auxClass.gSystem_GetParameterByID(coSysParamIDDOCNroEditEnabled), False) Then
                                        auxNroEdit = True
                                    End If
                                Else
                                    If auxWfwMode = enumWfwMode.coStandard Or auxIsAdmin Then
                                        Dim auxResult() As DataRow
                                        auxResult = hrcEntityDT_DOC_DOCTIP.Select("cod=" & auxDT.Rows(0)("doctipcod"))
                                        If auxResult.Count <> 0 Then
                                            If auxClass.Conn.gField_GetBoolean(auxResult(0)("permedicioncambiadsc"), False) Then
                                                auxDsc0Edit = True
                                            End If
                                            If auxClass.Conn.gField_GetBoolean(auxResult(0)("permedicioncambiaotros"), False) Then
                                                auxDocTipEdit = True
                                                auxProCodEdit = True
                                                auxSiscodEdit = True
                                                auxClacodEdit = True
                                                auxObsEdit = True
                                                auxUndcodEdit = True
                                            End If
                                            If auxClass.Conn.gField_GetBoolean(auxResult(0)("permedicioncambiaroles"), False) Then
                                                If Val(coSystemType) <> 175 Then
                                                    auxPanelRoles_CopyTROVisible = auxPermRolesEdit
                                                End If
                                            End If
                                        End If
                                    End If

                                End If
                            End If
                    End Select
                Case enumWorkflowStep.coWFWSTPDOC_DOCRevisionSSG
                    Select Case auxMode
                        Case enumActionType.coModify
                            If auxPermCambiarEstado Then
                                auxCuerpoEdit = True
                                auxDocumentsEdit = True
                            End If
                    End Select
            End Select
        End If
        'Control de pasos
        Dim auxControlButton_GotoStepSolDoc_Visible As Boolean = False
        Dim auxControlButton_GotoStepSolDocOK_Visible As Boolean = False
        Dim auxControlButton_GotoStepSolDocNOK_Visible As Boolean = False
        Dim auxControlButton_GotoStepEdicion_Visible As Boolean = False
        Dim auxControlButton_GotoStepEdicionOK_Visible As Boolean = False
        Dim auxControlButton_GotoStepRevisionOK_Visible As Boolean = False
        Dim auxControlButton_GotoStepAprobacionOK_Visible As Boolean = False
        Dim auxControlButton_GotoStepPublicacionOK_Visible As Boolean = False
        Dim auxControlButton_GotoStepLecturaOK_Visible As Boolean = False
        Dim auxControlButton_GotoStepNuevaVersion_Visible As Boolean = False
        Dim auxControlButton_GotoStepCancelarNuevaVersion_Visible As Boolean = False
        Dim auxControlButton_GotoStepSolicitudNuevaVersion_Visible As Boolean = False
        Dim auxControlButton_GotoStepSolicitudNuevaVersionOK_Visible As Boolean = False
        Dim auxControlButton_GotoStepCancel_Visible As Boolean = False
        Dim auxControlButton_GotoStepCancelOK_Visible As Boolean = False
        Dim auxControlButton_GotoStepCancelNOK_Visible As Boolean = False
        Dim auxControlButton_GotoStepRevisionNOK_Visible As Boolean = False
        Dim auxControlButton_GotoStepAprobacionNOK_Visible As Boolean = False
        Dim auxControlButton_GotoStepSolicitudEliminacion_Visible As Boolean = False
        Dim auxControlButton_GotoStepEliminacionOK_Visible As Boolean = False
        Dim auxControlButton_GotoStepEliminacionNOK_Visible As Boolean = False
        Dim auxControlButton_GotoStepSolicitudNuevaVersionNOK_Visible As Boolean = False
        Dim auxControlButton_GotoStepReedicion_Visible As Boolean = False
        Dim auxControlButton_GotoStepReID_Visible As Boolean = False
        Dim auxControlButton_GotoStepEliminacionTotal_Visible As Boolean = False

        If auxLecturaPendiente Then
            auxControlButton_GotoStepLecturaOK_Visible = True
        End If
        If (auxPermEdit Or auxPermDocVigente Or auxPermDocLector) _
            And (auxMode = enumActionType.coViewDetail Or auxMode = enumActionType.coModify Or auxMode = enumActionType.coNew) Then

            Select Case auxWfwStateCod
                Case enumWorkflowStep.coWFWSTPDOC_DOCCreacion
                    'En creacion todos pueden solicitar documento y pasar a edicion
                    If auxAllUserCanCreatedDocuments Then
                        auxControlButton_GotoStepSolDoc_Visible = True
                    Else
                        auxControlButton_GotoStepEdicion_Visible = True
                    End If
                Case enumWorkflowStep.coWFWSTPDOC_DOCnuevaversion
                    'En nueva version, solo lo pueden hacer los editores
                    If auxPermCambiarEstado Then
                        Select Case auxWfwMode
                            Case enumWfwMode.coStandard
                                auxControlButton_GotoStepEdicion_Visible = True
                            Case enumWfwMode.coUserCreate
                                auxControlButton_GotoStepSolicitudNuevaVersion_Visible = True
                                auxControlButton_GotoStepCancelarNuevaVersion_Visible = True
                        End Select
                    End If
                    'Dim auxGrpCodList As New List(Of Integer)
                    'auxGrpCodList.Add(auxSecurity.gGroup_GetCodByID(coGroupDocumentadorEditores))
                    'auxGrpCodList.Add(auxSecurity.gGroup_GetCodByID(coGroupDocumentadorAdministradores))
                    'auxGrpCodList.Add(auxSecurity.gGroup_GetCodByID(coGroupIDAdministradores))
                    'If auxMode = enumActionType.coModify And (auxIsAdmin _
                    'Or auxSecurity.gMember_IsInGroup(auxGrpCodList)) Then
                    '    auxDocTipEdit = True
                    '    auxEditOptions = True
                    'End If

                Case enumWorkflowStep.coWFWSTPDOC_DOCEdicion, enumWorkflowStep.coWFWSTPDOC_DOCSolicitudnuevodocumento
                    auxEditOptions_Visible = True
                    If auxPermCambiarEstado Then
                        'If auxConn.gConn_Query("SELECT cod FROM DOC_DOCSGN " _
                        '& " WHERE doccod=" & m_Cod & " AND empcod=" & Session("empcod") _
                        '& " AND doclogcod IN (SELECT cod FROM DOC_DOCLOG WHERE doccod=" & pCod & " AND wfwstepnext=" & enumWorkflowStep.coWFWSTPDOC_DOCEdicion & ")").Rows.Count > 0 Then

                        If auxWfwStateCod = enumWorkflowStep.coWFWSTPDOC_DOCSolicitudnuevodocumento Then
                            auxControlButton_GotoStepSolDocNOK_Visible = True
                            auxControlButton_GotoStepSolDocOK_Visible = True
                        Else
                            auxControlButton_GotoStepEdicionOK_Visible = True
                            auxControlButton_GotoStepCancel_Visible = True
                        End If
                    End If
                    Dim auxGrpCodList As New List(Of Integer)
                    auxGrpCodList.Add(auxSecurity.gGroup_GetCodByID(coGroupDocumentadorEditores))
                    auxGrpCodList.Add(auxSecurity.gGroup_GetCodByID(coGroupDocumentadorAdministradores))
                    auxGrpCodList.Add(auxSecurity.gGroup_GetCodByID(coGroupIDAdministradores))
                    If auxMode = enumActionType.coModify And (auxIsAdmin _
                    Or auxSecurity.gMember_IsInGroup(auxGrpCodList)) Then
                        auxDocTipEdit = True
                        auxEditOptions = True
                    End If
                    'Dim auxGrpCodList As New List(Of Integer)
                    'auxGrpCodList.Add(auxSecurity.gGroup_GetCodByID(coGroupDocumentadorEditores))
                    'auxGrpCodList.Add(auxSecurity.gGroup_GetCodByID(coGroupDocumentadorAdministradores))
                    'auxGrpCodList.Add(auxSecurity.gGroup_GetCodByID(coGroupIDAdministradores))
                    'If auxMode = enumActionType.coModify And (auxIsAdmin _
                    '  Or auxSecurity.gMember_IsInGroup(auxGrpCodList)) Then
                    '    auxDocTipEdit = True
                    'End If
                    'Select Case auxWfwMode

                    'End Select
                    'If auxMode = enumActionType.coModify Then
                    '    If auxIsAdmin _
                    '        Or auxSecurity.gSID_CheckAccess(auxSidCod, enumAccessType.coSYSGlobalModificar _
                    '                          & "," & enumAccessType.coSYSGlobalCambiarpermisos _
                    '                          & "," & enumAccessType.coSYSGlobalCambiarestado) Then
                    '    End If
                    'End If
                Case enumWorkflowStep.coWFWSTPDOC_DOCRevisionSSG, enumWorkflowStep.coWFWSTPDOC_DOCEdicionOK
                    auxEditOptions_Visible = True
                    If auxPermCambiarEstado And auxWfwStateCod = enumWorkflowStep.coWFWSTPDOC_DOCRevisionSSG Then
                        auxControlButton_GotoStepRevisionOK_Visible = True
                        auxControlButton_GotoStepRevisionNOK_Visible = True
                        auxControlButton_GotoStepCancel_Visible = True
                    Else
                        auxControlButton_GotoStepEdicionOK_Visible = True
                    End If
                    Dim auxGrpCodList As New List(Of Integer)
                    auxGrpCodList.Add(auxSecurity.gGroup_GetCodByID(coGroupDocumentadorEditores))
                    auxGrpCodList.Add(auxSecurity.gGroup_GetCodByID(coGroupDocumentadorAdministradores))
                    auxGrpCodList.Add(auxSecurity.gGroup_GetCodByID(coGroupIDAdministradores))
                    If auxMode = enumActionType.coModify And (auxIsAdmin _
                      Or auxSecurity.gMember_IsInGroup(auxGrpCodList)) Then
                        auxDocTipEdit = True
                        auxEditOptions = True
                    End If
                Case enumWorkflowStep.coWFWSTPDOC_DOCRevisionrechazada
                    If auxIsAdmin Or auxPermEdit Then
                        auxControlButton_GotoStepRevisionNOK_Visible = True
                    End If
                Case enumWorkflowStep.coWFWSTPDOC_DOCAprobacion, enumWorkflowStep.coWFWSTPDOC_DOCrevisionOK
                    auxEditOptions_Visible = True
                    If auxIsAdmin _
                        Or auxPermCambiarEstado _
                        Or auxConn.gConn_Query("SELECT cod FROM DOC_DOCSGN WHERE doccod=" & auxCurrentCod & " AND empcod=" & auxEmpCod).Rows.Count > 0 Then
                        auxControlButton_GotoStepAprobacionNOK_Visible = True
                        auxControlButton_GotoStepAprobacionOK_Visible = True
                        auxControlButton_GotoStepCancel_Visible = True
                        auxEditOptions = True
                    End If
                    Dim auxGrpCodList As New List(Of Integer)
                    auxGrpCodList.Add(auxSecurity.gGroup_GetCodByID(coGroupDocumentadorEditores))
                    auxGrpCodList.Add(auxSecurity.gGroup_GetCodByID(coGroupDocumentadorAdministradores))
                    auxGrpCodList.Add(auxSecurity.gGroup_GetCodByID(coGroupIDAdministradores))
                    If auxMode = enumActionType.coModify And (auxIsAdmin _
                      Or auxSecurity.gMember_IsInGroup(auxGrpCodList)) Then
                        auxDocTipEdit = True
                    End If
                Case enumWorkflowStep.coWFWSTPDOC_DOCAprobacionrechazada
                    'If m_IsAdmin Or auxConn.gConn_Query("SELECT cod FROM DOC_DOCSGN WHERE doccod=" & m_Cod & " AND empcod=" & Session("empcod")).Rows.Count > 0 Then
                    If auxPermCambiarEstado Then
                        auxControlButton_GotoStepEdicion_Visible = True
                    End If

                Case enumWorkflowStep.coWFWSTPDOC_DOCPublicacion, enumWorkflowStep.coWFWSTPDOC_DOCaprobacionOK
                    auxEditOptions_Visible = True
                    auxEditOptions_PublicacionAuto_Enabled = True
                    If auxPermCambiarEstado Then
                        auxControlButton_GotoStepPublicacionOK_Visible = True
                        auxEditOptions = True
                    End If
                Case enumWorkflowStep.coWFWSTPDOC_DOCCancelacion
                    'If m_IsAdmin Or auxConn.gConn_Query("SELECT cod FROM DOC_DOCSGN WHERE doccod=" & m_Cod & " AND empcod=" & Session("empcod")).Rows.Count > 0 Then
                    If auxPermCambiarEstado Then
                        auxControlButton_GotoStepCancelOK_Visible = True
                        auxControlButton_GotoStepCancelNOK_Visible = True

                    End If
                Case enumWorkflowStep.coWFWSTPDOC_DOCDocumentoobsoleto
                    If auxIsAdmin Or auxPermEdit Then
                        If auxSecurity.gMember_IsInGroupByID(coGroupDocumentadorEditores) _
                            Or auxSecurity.gMember_IsInGroupByID(coGroupDocumentadorAdministradores) _
                            Or auxIsAdmin Then
                            If auxConn.gField_GetBoolean(hrcEntityDT_DOC_DOCTIP.Select("cod=" & auxDoctipCod)(0)("reedicion"), False) Then
                                auxControlButton_GotoStepReedicion_Visible = True
                            End If
                        End If
                    End If

                Case enumWorkflowStep.coWFWSTPDOC_DOCDocumentovigente, enumWorkflowStep.coWFWSTPDOC_DOCpublicacionOK, enumWorkflowStep.coWFWSTPDOC_DOCReidentificacion
                    If Session("modo_obligado") = False Then
                        'If m_PermNavegar Then
                        If (auxLecturaPendiente = False And auxPermDocVigente) Or auxPermCambiarEstado Then
                            Select Case auxWfwMode
                                Case enumWfwMode.coStandard
                                    auxControlButton_GotoStepSolicitudNuevaVersion_Visible = True
                                Case enumWfwMode.coUserCreate
                                    auxControlButton_GotoStepNuevaVersion_Visible = True
                            End Select
                            'auxControlButton_GotoStepNuevaVersion_Visible = True

                        End If
                        If auxPermCambiarEstado Then
                            auxControlButton_GotoStepSolicitudEliminacion_Visible = True

                            If coSystemType <> "175" Then
                                auxControlButton_GotoStepReID_Visible = True
                            End If
                        End If
                        Dim auxGrpCodList As New List(Of Integer)
                        auxGrpCodList.Add(auxSecurity.gGroup_GetCodByID(coGroupDocumentadorEditores))
                        auxGrpCodList.Add(auxSecurity.gGroup_GetCodByID(coGroupDocumentadorAdministradores))
                        auxGrpCodList.Add(auxSecurity.gGroup_GetCodByID(coGroupIDAdministradores))
                        If auxMode = enumActionType.coModify And (auxIsAdmin _
                         Or auxSecurity.gMember_IsInGroup(auxGrpCodList) _
                         Or auxPermCambiarEstado) Then
                            Dim auxResult() As DataRow
                            auxResult = hrcEntityDT_DOC_DOCTIP.Select("cod=" & auxDT.Rows(0)("doctipcod"))
                            If auxResult.Count <> 0 Then
                                If auxClass.Conn.gField_GetBoolean(auxResult(0)("permvigcambiadsc"), False) Then
                                    auxDsc0Edit = True
                                End If

                                If auxClass.Conn.gField_GetBoolean(auxResult(0)("permvigcambiaotros"), False) Then
                                    auxDocTipEdit = True
                                    auxProCodEdit = True
                                    auxSiscodEdit = True
                                    auxClacodEdit = True
                                    auxObsEdit = True
                                    auxUndcodEdit = True
                                End If
                            End If

                        End If

                        'End If
                    End If
                Case enumWorkflowStep.coWFWSTPDOC_DOCSolicitudnuevaversion, enumWorkflowStep.coWFWSTPDOC_DOCSolicitudautomaticanuevaversion
                    If auxPermCambiarEstado Then
                        auxControlButton_GotoStepSolicitudNuevaVersionOK_Visible = True
                        auxControlButton_GotoStepSolicitudNuevaVersionNOK_Visible = True
                    End If
                Case enumWorkflowStep.coWFWSTPDOC_DOCSolicitudeliminacion
                    If auxPermCambiarEstado Then '  auxSecurity.gSID_CheckAccess(auxSidCod, enumAccessType.coSYSGlobalCambiarestado) Then
                        auxControlButton_GotoStepEliminacionOK_Visible = True
                        auxControlButton_GotoStepEliminacionNOK_Visible = True
                    End If
            End Select

            If auxIsAdmin _
                And auxMode <> enumActionType.coNew _
                And auxConn.gField_GetBoolean(auxClass.gSystem_GetParameterByID(coSysParamIDDOCDeleteTotalEnabled), False) _
                And auxWfwStateCod = enumWorkflowStep.coWFWSTPDOC_DOCDocumentoobsoleto Then
                'muestra el boton de eliminacion total
                auxControlButton_GotoStepEliminacionTotal_Visible = True
            End If
        End If
        'Permisos de CAMBIO- solo cuando no es un historico
        If auxWfwLocked = False And (auxPermEdit And auxMode = enumActionType.coViewDetail) Then
            auxControlButton_ItemUpdate_Visible = True
        End If


        Dim auxFechaVigente As DateTime = Nothing
        'Carga los valores
        Dim auxSysParamIDDOCDscxVisible As Boolean = auxConn.gField_GetBoolean(auxClass.gSystem_GetParameterByID(coSysParamIDDOCDscxVisible), False)
        If auxSysParamIDDOCDscxVisible Then
            If auxDsc0Edit Then
                auxDsc1Edit = True
            End If
        End If
        If auxDT.Rows.Count = 0 Then
            'If m_PermEdit Then
            '    'auxDTValues_Old.gValue_Add("wfw")
            '    'lblRwfwstatus.Text = "Creación"
            '    auxDTValues_Old.gValue_Add("version", "----")
            '    auxDTValues_Old.gValue_Add("identificador", "----")
            '    'lblRVersion.Text = "--"
            '    'lblRIdentificador.Text = "---"
            '    Dim auxDocNroEditEnabled As Boolean = auxClass.Conn.gField_GetBoolean(auxClass.gSystem_GetParameterByID(coSysParamIDDOCNroEditEnabled), False)
            '    If auxDocNroEditEnabled Then
            '        txtEnro.Text = auxClass.Conn.gConn_QueryValue("SELECT MAX(nro) FROM DOC_DOC WHERE cod > 0 ", 0) + 1
            '    End If

            '    lblRprofecha.Text = "---"    ' Today.ToString("d/M/yyyy")
            'End If
        Else


            If auxUndcodEdit Then
                For Each auxDocUndRow As DataRow In auxClass.Conn.gConn_Query("SELECT cod FROM DOC_DOCUND " _
                                           & " WHERE undcod > 0 AND doccod=" & auxCurrentCod).Rows
                    auxClass.gEntity_DOC_DOCUND_UpdateInDraft(pcod:=auxDocUndRow("cod"), pdftdidgencod:=auxDfdGenCod)
                Next
            End If
            'If auxHstGenCod > 0 Then
            'auxCuerpoVisible = True
            'cmdRCuerpo.OnClientClick = "hrcShowWindowNoModal(""cfrmdocumentos_impresion.aspx?param2=" & auxHstGenCod & "&_closea_=1&_mode_=7"");return false;"
            'Else
            'If auxCuerpoEdit Then
            '    gCuerpo_SetValue(auxDT.Rows(0)("cuerpo").ToString, True, hdnECuerpo, cmdECuerpo, "hrctexteditor.aspx")
            'ElseIf m_WfwStateCod <> enumWorkflowStep.coWFWSTPDOC_DOCCreacion Then
            '    Select Case m_WfwStateCod
            '        Case enumWorkflowStep.coWFWSTPDOC_DOCCreacion
            '        Case enumWorkflowStep.coWFWSTPDOC_DOCDocumentovigente, enumWorkflowStep.coWFWSTPDOC_DOCSolicitudnuevaversion, enumWorkflowStep.coWFWSTPDOC_DOCSolicitudautomaticanuevaversion
            '            cmdRCuerpo.Visible = True
            '            cmdRCuerpo.OnClientClick = "hrcShowWindowNoModal(""cfrmdocumentos_impresion.aspx?param1=" & auxCurrentCod & "&_closea_=1&_mode_=8"");return false;"
            '        Case Else
            '            cmdRCuerpo.Visible = True
            '            cmdRCuerpo.OnClientClick = "hrcShowWindowNoModal(""cfrmdocumentos_impresion.aspx?param1=" & auxCurrentCod & "&_closea_=1&_mode_=7"");return false;"
            '    End Select
            'End If
            'End If

            auxFechaVigente = auxConn.gDate_GetToday
            ' m_WfwStateCod = enumWorkflowStep.coWFWSTPDOC_DOCDocumentovigente
            If auxHstGenCod > 0 Then
            Else
                If auxFechaVigente <> Nothing _
                And auxWfwStateCod <> enumWorkflowStep.coWFWSTPDOC_DOCCreacion _
                And auxWfwStateCod <> enumWorkflowStep.coWFWSTPDOC_DOCDocumentoobsoleto Then
                    Dim auxPermCopiasControladas As Boolean = auxIsAdmin Or auxSecurity.gSID_CheckAccess(auxSidCod, enumAccessType.coSYSGlobalCambiarpermisos & "," & enumAccessType.coSYSGlobalModificar & "," & enumAccessType.coSYSImprimircopiascontroladas)
                    If auxPermCopiasControladas Then
                        'Sólo los editores
                        auxcontrolButton_CopyControled = True
                        'No abrir modal porque no funciona en IE
                    End If
                    Dim auxPermCopiasNoControladas As Boolean = auxIsAdmin _
                                                Or auxSecurity.gSID_CheckAccess(auxSidCod, enumAccessType.coSYSGlobalCambiarpermisos _
                                                                                & "," & enumAccessType.coSYSGlobalModificar _
                                                                                & "," & enumAccessType.coSYSGlobalCambiarestado _
                                                                                & "," & enumAccessType.coSYSImprimircopiasnocontroladas _
                                                                                & "," & enumAccessType.coSYSImprimircopiascontroladas)
                    If auxPermCopiasNoControladas Then
                        auxcontrolButton_CopyNoControled = True
                    End If

                End If

                'If coSystemType <> 175 Then
                If auxIsAdmin Or auxSecurity.gSID_CheckAccess(auxSidCod, enumAccessType.coSYSGlobalModificar _
                                                                   & "," & enumAccessType.coSYSImprimircopiasnocontroladas) Then
                    auxControlButton_PDFView_Visible = True
                End If
                Select Case auxWfwStateCod
                    Case enumWorkflowStep.coWFWSTPDOC_DOCCreacion
                    Case Else
                        auxControlButton_VersionsChanges_Visible = True
                End Select
            End If
        End If

        'panel
        Dim auxPanelTRO As clsHrcJSPanel
        Dim auxGrdTro As clshrcGrdData
        auxPrincipalPanel.BagValues.gValue_Add("trocod", auxTroCod)
        If auxPanelRolesVisible Then
            Dim auxTROHTMLheader As String = "<tr><td class=""pryceldatitulos"">ROLES:</td></tr>"
            auxPanelTRO = gPanelRoles_Get(auxClass, auxHrcContext, auxPrincipalPanel, auxPermRolesEdit, auxTroCod, auxDfdGenCod_TRO, auxPermRolesEdit, auxTROHTMLheader)
            auxGrdTro = auxPanelTRO.gControl_Get("grdtro")
            auxPrincipalPanel.gControls_Add(auxPanelTRO)
            auxHrcContext.gObjectTmp_UploadControl(auxPanelTRO)
        End If
        Dim auxNewRow As DataRow

        Dim auxSecModeVisible = auxGrdTro IsNot Nothing
        Dim auxSecurityEdit As Boolean = False
        Dim auxControlSecurityInheritance As clshrcJSComboBox
        If auxSecModeVisible Then
            Dim auxSecMode As Short = 1
            If auxConn.gField_GetInt(auxDTValues_Old.gValue_Get("trocodcustom")) > 0 _
                And auxConn.gField_GetInt(auxDTValues_Old.gValue_Get("trocodcustom")) = auxConn.gField_GetInt(auxDTValues_Old.gValue_Get("trocod")) Then
                auxDTValues_Old.gValue_Add("trocodcustomenabled", True)
            End If
            If auxConn.gField_GetBoolean(auxDTValues_Old.gValue_Get("trocodcustomenabled")) Then
                auxSecMode = 2
            Else
                auxSecMode = 1
            End If


            Dim auxSecModeDT As New DataTable
            auxSecModeDT.Columns.Add(New DataColumn("cod", Type.GetType("System.Int32")))
            auxSecModeDT.Columns.Add(New DataColumn("dsc", Type.GetType("System.String")))
            auxNewRow = auxSecModeDT.NewRow
            auxNewRow("cod") = 1
            auxNewRow("dsc") = "<img src=imagenes/qdoc_roles_inherit.png class=hrcthemeimage width=24px >" _
                & "Hereda del documento padre"
            auxSecModeDT.Rows.Add(auxNewRow)
            auxNewRow = auxSecModeDT.NewRow
            auxNewRow("cod") = 2
            auxNewRow("dsc") = "<img src=imagenes/qdoc_roles_break.png class=hrcthemeimage width=24px >" _
                & "No hereda del documento padre"
            auxSecModeDT.Rows.Add(auxNewRow)
            auxControlSecurityInheritance = New clshrcJSComboBox("secmode", "Modo de seguridad", "secmode", pHrcContext.ControlComboBoxCSS, auxSecModeDT, "100px", "")
            auxControlSecurityInheritance.DisplayMode = clshrcJSComboBox.enumDisplayMode.Radio

            auxControlSecurityInheritance.gValue_Set(auxSecMode)
            'auxPanelTRO()
            auxControlSecurityInheritance.JSEventChange = "" _
                        & "switch (parseInt(" & auxControlSecurityInheritance.gJS_GetCodInEvent & ")){" _
                        & "case 1:" _
                        & auxGrdTro.gJS_Hide & ";" _
                        & "$('#panel_trocopy').hide();" _
                        & "break;" _
                        & "case 2:" _
                        & auxGrdTro.gJS_Show & ";" _
                        & "$('#panel_trocopy').show();" _
                        & "break;" _
                        & "};"

            auxControlSecurityInheritance.ServerStateMantain = True
            auxHrcContext.gObjectTmp_UploadControl(auxControlSecurityInheritance)

            If auxMode <> enumActionType.coModify Then
                auxControlSecurityInheritance.Disabled = True
            End If
            auxGrdTro.Parentcontrol.gControls_Add(auxControlSecurityInheritance)
        End If

        'dsc
        Dim auxControlDsc0 As clsHrcJSControlBasic
        Dim auxControlDsc1 As clsHrcJSControlBasic
        Dim auxControlDsc2 As clsHrcJSControlBasic
        If auxDsc0Edit Then
            auxControlDsc0 = New clshrcJSTextBox(pControlID:="dsc0", pTitle:="Titulo", pFieldData:="dsc0", pCSSClass:=auxHrcContext.ControlTextBoxCSS, pMaxLength:=200, pWidth:="99%", pHeight:="", pMultiline:=False, pFormatter:="", pUnFormatter:="")
            auxControlDsc0.ServerStateMantain = True
            auxHrcContext.gObjectTmp_UploadControl(auxControlDsc0)
        Else
            auxControlDsc0 = New clsHrcJSLabel(pControlID:="dsc0_ro")
        End If
        auxPrincipalPanel.gControls_Add(auxControlDsc0)
        auxControlDsc0.gValue_Set(auxDTValues_Old.gValue_Get("dsc0", "").ToString)

        'dsc0

        If auxDsc1Edit Then
            auxControlDsc1 = New clshrcJSTextBox(pControlID:="dsc1", pTitle:="Titulo parte 2", pFieldData:="dsc1", pCSSClass:=auxHrcContext.ControlTextBoxCSS, pMaxLength:=200, pWidth:="99%", pHeight:="", pMultiline:=False, pFormatter:="", pUnFormatter:="")
            auxControlDsc1.ServerStateMantain = True
            auxHrcContext.gObjectTmp_UploadControl(auxControlDsc1)
            auxControlDsc2 = New clshrcJSTextBox(pControlID:="dsc2", pTitle:="Titulo parte 3", pFieldData:="dsc2", pCSSClass:=auxHrcContext.ControlTextBoxCSS, pMaxLength:=200, pWidth:="99%", pHeight:="", pMultiline:=False, pFormatter:="", pUnFormatter:="")
            auxControlDsc2.ServerStateMantain = True
            auxHrcContext.gObjectTmp_UploadControl(auxControlDsc2)
            auxControlDsc1.gValue_Set(auxDTValues_Old.gValue_Get("dsc1", "").ToString)
            auxControlDsc2.gValue_Set(auxDTValues_Old.gValue_Get("dsc2", "").ToString)
        Else
            auxControlDsc1 = New clsHrcJSLabel(pControlID:="dsc1_ro")
            auxControlDsc2 = New clsHrcJSLabel(pControlID:="dsc2_ro")

            auxControlDsc1.gValue_Set(" " & auxDTValues_Old.gValue_Get("dsc1", "").ToString)
            auxControlDsc2.gValue_Set(" " & auxDTValues_Old.gValue_Get("dsc2", "").ToString)
        End If
        auxPrincipalPanel.gControls_Add(auxControlDsc1)
        auxPrincipalPanel.gControls_Add(auxControlDsc2)


        'OBS
        Dim auxControlObs As clsHrcJSControlBasic
        If auxObsEdit Then
            auxControlObs = New clshrcJSTextBox(pControlID:="obs", pTitle:="Breve descripción", pFieldData:="obs", pCSSClass:=auxHrcContext.ControlTextBoxCSS, pMaxLength:=1000, pWidth:="99%", pHeight:="", pMultiline:=True, pFormatter:="", pUnFormatter:="")
            auxControlObs.Height = "50px"
            auxControlObs.ServerStateMantain = True
            auxHrcContext.gObjectTmp_UploadControl(auxControlObs)
        Else
            auxControlObs = New clsHrcJSLabel(pControlID:="obs_ro")
        End If
        auxPrincipalPanel.gControls_Add(auxControlObs)
        auxControlObs.gValue_Set(auxDTValues_Old.gValue_Get("obs", "").ToString)

        'Sistema
        Dim auxControlSisCod As clsHrcJSControlBasic = auxPrincipalPanel.gControl_Get("siscod")
        If auxSisCod_Mandatory = -1 And auxSiscodEdit Then
            Dim auxSisCod As Integer = auxConn.gField_GetInt(auxDTValues_Old.gValue_Get("siscod"), -1)
            If auxControlSisCod Is Nothing Then
                Dim auxSISview As DataTable = auxConn.gConn_Query("SELECT cod,dsc " _
                                                                  & " FROM DOC_SIS " _
                                                                  & "WHERE " _
                                                                  & "(DOC_SIS.baja = {#FALSE#} OR DOC_SIS.baja  {#ISNULL#})")
                auxControlSisCod = New clshrcJSComboBox("siscod", "Sistema", "", auxHrcContext.ControlComboBoxCSS, auxSISview, "100px", "")
                auxPrincipalPanel.gControls_Add(auxControlSisCod)
                auxControlSisCod.gValue_Set(auxSisCod)
                auxControlSisCod.ServerStateMantain = True
                auxHrcContext.gObjectTmp_UploadControl(auxControlSisCod)
            End If
        Else
            auxControlSisCod = New clsHrcJSLabel(pControlID:="siscod_ro")
        End If

        Dim auxDependencyDocEnabled As Boolean = True
        'Proceso
        Dim auxControlProCod As clshrcObjectExplorer = auxPrincipalPanel.gControl_Get("procod")
        If auxControlProCod Is Nothing Then
            auxControlProCod = New clshrcObjectExplorer("procod", "hrcGrdData.ashx", hrcDT_Cache(enumDTCache.coDOCPROUserSelection_APA) _
                                                        , auxClass.Conn, auxClientCon)
            auxPrincipalPanel.gControls_Add(auxControlProCod)
            auxControlProCod.IsReadOnly = Not auxProCodEdit
            auxControlProCod.ObjectExplorerEnabled = True
            auxControlProCod.SelectionLimit = 1
            If auxDependencyDocEnabled Then
                auxControlProCod.gAutosuggest_Enabled("(SELECT DOC_PRO.cod as cod,DOC_PRO.dsc as dsc," & enumEntities.coEntityDOC_PRO & " as q_type,'Proceso' FROM DOC_PRO " _
                                                & " WHERE ( (DOC_PRO.dsc LIKE '%{#HRCDSC#}%' OR DOC_PRO.apacod IN (SELECT cod FROM DOC_APA WHERE cod > 0 AND dsc LIKE '%{#HRCDSC#}%' )) AND (DOC_PRO.baja = {#FALSE#} OR DOC_PRO.baja  {#ISNULL#})) " _
                                                & " AND DOC_PRO.cod >= 1)" _
                                                & " UNION ALL" _
                                                & " (SELECT DOC_DOC.cod as cod,(DOC_DOC.identificador+'-'+DOC_DOC.dsc) as dsc," & enumEntities.coEntityDOC_DOC & " as q_type,'Documento' " _
                                                & " FROM DOC_DOC " _
                                                & " WHERE  (DOC_DOC.dsc LIKE '%{#HRCDSC#}%' ) " _
                                                & " AND (DOC_DOC.baja = {#FALSE#} OR DOC_DOC.baja  {#ISNULL#})" _
                                                & " AND DOC_DOC.cod >= 1)" _
                                                & " ORDER BY dsc,q_type")

            Else
                auxControlProCod.gAutosuggest_Enabled("(SELECT DOC_PRO.cod as cod,DOC_PRO.dsc as dsc," & enumEntities.coEntityDOC_PRO & " as q_type,'Proceso' FROM DOC_PRO " _
                                                & " WHERE ( (DOC_PRO.dsc LIKE '%{#HRCDSC#}%' OR DOC_PRO.apacod IN (SELECT cod FROM DOC_APA WHERE cod > 0 AND dsc LIKE '%{#HRCDSC#}%' )) AND (DOC_PRO.baja = {#FALSE#} OR DOC_PRO.baja  {#ISNULL#})) " _
                                                & " AND DOC_PRO.cod >= 1)" _
                                                & " ORDER BY dsc,q_type")

            End If
            If auxConn.gField_GetInt(auxDTValues_Old.gValue_Get("docsupcod", -1)) > 0 Then
                auxControlProCod.gStartItem_Add(enumEntities.coEntityDOC_DOC, auxDTValues_Old.gValue_Get("docsupcod", -1), auxDTValues_Old.gValue_Get("docsupcod_dsc", ""))
            ElseIf auxConn.gField_GetInt(auxDTValues_Old.gValue_Get("procod", -1)) > 0 Then
                auxControlProCod.gStartItem_Add(enumEntities.coEntityDOC_PRO, auxDTValues_Old.gValue_Get("procod", -1), auxDTValues_Old.gValue_Get("prodsc", ""))
            Else
            End If

            If auxControlProCod.CentralGrid IsNot Nothing Then
                auxControlProCod.CentralGrid.gSelectList_Add(enumEntities.coEntityDOC_APA, enumEntities.coEntityDOC_PRO, _
                                                        "(SELECT DOC_PRO.cod as cod,DOC_PRO.dsc as dsc," & enumEntities.coEntityDOC_PRO & " as q_type FROM DOC_PRO" _
                                                        & " WHERE ((((DOC_PRO.apacod ={#HRCCOD#} AND '{#HRCDSC#}'='')  OR  (DOC_PRO.dsc LIKE '%{#HRCDSC#}%' AND '{#HRCDSC#}'<>''))) AND (DOC_PRO.baja = 0 OR DOC_PRO.baja  IS NULL)) AND DOC_PRO.cod >= 1)")
                auxControlProCod.CentralGrid.gSelectTypeFilter_Add(enumEntities.coEntityDOC_PRO)

                auxControlProCod.CentralGrid.gSelectList_Add(enumEntities.coEntityDOC_DOC, enumEntities.coEntityDOC_DOC, _
                                                        "(SELECT DOC_DOC.cod as cod,DOC_DOC.dsc as dsc," & enumEntities.coEntityDOC_DOC & " as q_type FROM DOC_DOC" _
                                                        & " WHERE (((('{#HRCDSC#}'='')  OR  (DOC_DOC.dsc LIKE '%{#HRCDSC#}%' AND '{#HRCDSC#}'<>''))) AND (DOC_DOC.baja = 0 OR DOC_DOC.baja  IS NULL)) AND DOC_DOC.cod >= 1)")
                auxControlProCod.CentralGrid.gSelectTypeFilter_Add(enumEntities.coEntityDOC_DOC)
            End If
            If auxControlSecurityInheritance IsNot Nothing Then
                If auxConn.gField_GetInt(auxDTValues_Old.gValue_Get("docsupcod", -1)) > 0 Then

                ElseIf auxConn.gField_GetInt(auxDTValues_Old.gValue_Get("procod", -1)) > 0 Then
                    auxControlSecurityInheritance.Visible = False
                Else
                End If
                auxControlProCod.JSEventChange = "" _
                   & "var auxType=parseInt(" & auxControlProCod.gJS_GetTypeInEvent & ");" _
                   & "switch (auxType){" _
                   & "case " & enumEntities.coEntityDOC_DOC & ":" _
                       & auxControlSecurityInheritance.gJS_Show & ";" _
                       & auxControlSecurityInheritance.gJS_Value_Set("1", True) & ";" _
                       & "break;" _
                    & "default:" _
                       & auxControlSecurityInheritance.gJS_Hide & ";" _
                       & auxControlSecurityInheritance.gJS_Value_Set("2", True) & ";" _
                       & "break;" _
                   & "};"
            End If

        End If


        'If auxDependencySelection Then
        '    Dim auxControlContentTypeID As clshrcJSComboBox
        '    auxControlContentTypeID = New clshrcJSComboBox("contentmode", "Tipo de contenido", "", auxHrcContext.ControlComboBoxCSS, auxContentTypeDT, "", "")
        '    auxControlContentTypeID.DisplayMode = clshrcJSComboBox.enumDisplayMode.Radio
        '    auxControlContentTypeID.gValue_Set(auxDTValues_Old.gValue_Get("contentypeid"))
        '    Select Case auxContentTypeID
        '        Case clsHrcConnClient.enumMimeTypes.coHTML
        '            auxControlContentTypeID.DefaultValue = "1" '.gValue_Set("'1'")
        '        Case Else
        '            auxControlContentTypeID.DefaultValue = "2" '.gValue_Set("'2'")
        '    End Select

        '    auxControlContentTypeID.JSEventChange = "" _
        '                & "switch (pvalue){" _
        '                & "case 1:case '1':" _
        '                & auxControlContenidoHTML.gJS_Show & ";" _
        '                & auxControlContenidoArchivo.gJS_Hide & ";" _
        '                & "break;" _
        '                & "case 2:case '2':" _
        '                & auxControlContenidoHTML.gJS_Hide & ";" _
        '                & auxControlContenidoArchivo.gJS_Show & ";" _
        '                & auxControlContenidoArchivo.gJS_Value_Set("'" & auxContentIDARchivo & "'") & ";" _
        '                & "break;" _
        '                & "};"
        '    auxControlContentTypeID.ServerStateMantain = True
        '    auxHrcContext.gObjectTmp_UploadControl(auxControlContentTypeID)
        '    auxPrincipalPanel.gControls_Add(auxControlContentTypeID)
        'End If

        'Version
        Dim auxControlversion As clsHrcJSLabel = auxPrincipalPanel.gControl_Get("version")
        If auxControlversion Is Nothing Then
            auxControlversion = New clsHrcJSLabel(pControlID:="version")
            auxPrincipalPanel.gControls_Add(auxControlversion)
        End If
        auxControlversion.gValue_Set(auxDTValues_Old.gValue_Get("version", "").ToString)

        'Estado
        Dim auxControlWfwStatus As clsHrcJSLabel = auxPrincipalPanel.gControl_Get("wfwstatus")
        If auxControlWfwStatus Is Nothing Then
            auxControlWfwStatus = New clsHrcJSLabel(pControlID:="wfwstatus")
            auxPrincipalPanel.gControls_Add(auxControlWfwStatus)
        End If
        Dim auxWfwStpDsc As String = hrcEntityDT_Q_WFWSTP_FindByKey(auxWfwStateCod)("wfwstpdsc").ToString
        auxControlWfwStatus.gValue_Set(auxWfwStpDsc)

        'nro
        Dim auxControlNro As clsHrcJSControlBasic
        If auxNroEdit Then
            auxControlNro = New clshrcJSTextBox(pControlID:="nro", pTitle:="Número", pFieldData:="nro", pCSSClass:=auxHrcContext.ControlTextBoxCSS, pMaxLength:=8, pWidth:="100px", pHeight:="", pMultiline:=False, pFormatter:="", pUnFormatter:="")
            auxControlNro.ServerStateMantain = True
            auxHrcContext.gObjectTmp_UploadControl(auxControlNro)
        Else
            auxControlNro = New clsHrcJSLabel(pControlID:="nro_ro")
        End If
        auxPrincipalPanel.gControls_Add(auxControlNro)
        auxControlNro.gValue_Set(auxDTValues_Old.gValue_Get("nro", "").ToString)

        'Tipos de documentos
        Dim auxControlTipCod As clsHrcJSControlBasic
        If auxDocTipEdit Then
            Dim auxTIPDT As DataTable = New DataView(hrcEntityDT_DOC_DOCTIP, "(baja IS NULL OR baja=0)" _
                                                                   , "dsc", DataViewRowState.CurrentRows).ToTable
            auxControlTipCod = New clshrcJSComboBox("tipcod", "Tipo de documento", "", auxHrcContext.ControlComboBoxCSS, auxTIPDT, "", "")
            auxControlTipCod.gValue_Set(auxDTValues_Old.gValue_Get("doctipcod"))
            auxControlTipCod.ServerStateMantain = True
            auxHrcContext.gObjectTmp_UploadControl(auxControlTipCod)
        Else
            auxControlTipCod = New clsHrcJSLabel(pControlID:="tipcod_ro")
            If auxConn.gField_GetInt(auxDTValues_Old.gValue_Get("doctipcod"), -1) > 0 Then
                auxControlTipCod.gValue_Set(auxDTValues_Old.gValue_Get("wildoctipdsc"))
            End If

        End If
        auxPrincipalPanel.gControls_Add(auxControlTipCod)


        Dim auxContentTypeID As Integer = Val(auxDTValues_Old.gValue_Get("contenttypeid"))
        If auxContentTypeID < 1 Then
            'auxContentTypeID = clsHrcConnClient.enumMimeTypes.coHTML
            auxContentTypeID = clsHrcConnClient.enumMimeTypes.coUndefined
        End If

        'Contenido-HTML
        Dim auxContent As String = auxDTValues_Old.gValue_Get("cuerpo", "")
        Dim auxContentID As String = auxClass.Conn.gField_GetUniqueID.ToString.Replace("-", "")
        auxClientCon.gTextTmp_Upload(auxContent, auxContentID)

        Dim auxControlContenidoHTML As clsHrcJSButton
        If auxHstGenCod > 0 Then
            auxControlContenidoHTML = New clsHrcJSButton("contenidohtml", "Ver contenido", auxHrcContext.ButtonCSS)
            auxControlContenidoHTML.EventOnClick = "hrcShowWindowNoModal(""cfrmdocumentos_impresion.aspx?param2=" & auxHstGenCod & "&_closea_=1&_mode_=7"");return false;"
        Else
            If auxCuerpoEdit Then
                auxControlContenidoHTML = New clsHrcJSButton("contenidohtml", "Editar contenido", auxHrcContext.ButtonCSS)
                auxPrincipalPanel.gJSCommand_Add("contenidohtml_charge", "")
                auxControlContenidoHTML.EventOnClick = auxPrincipalPanel.gJSCommand_GetCall("contenidohtml_charge", Nothing) & ";" _
                    & "hrcShowWindowNoModal(""hrctexteditor.aspx?upload=1&tmp=1&tmpid=" & auxContentID & """);"
                AddHandler auxControlContenidoHTML.EventCommandHandler, AddressOf gFormPNLDOC_CommandHandler
                auxPrincipalPanel.BagValues.gValue_Add("contenido_html", auxContentID)
                auxPrincipalPanel.gControls_Add(auxControlContenidoHTML)
            End If
        End If



        'Contenido-ARchivo
        Dim auxContentIDARchivo As String = auxClass.Conn.gField_GetUniqueID.ToString.Replace("-", "")
        Dim auxControlContenidoArchivo As clsHrcJSControlBasic
        If auxHstGenCod > 0 Then
            'auxControlContenidoHTML = New clsHrcJSButton("contenidoarchivo", "Ver contenido", auxHrcContext.ButtonCSS)
            'auxControlContenidoHTML.EventOnClick = "hrcShowWindowNoModal(""cfrmdocumentos_impresion.aspx?param2=" & auxHstGenCod & "&_closea_=1&_mode_=7"");return false;"
        Else
            If auxCuerpoEdit Then
                Dim auxControlContenidoArchivo_FU As clshrcJSFileUpload
                auxControlContenidoArchivo_FU = New clshrcJSFileUpload("contenidoarchivo", "", "contenidoarchivo", "100%", "40px")
                auxControlContenidoArchivo_FU.InitialDownloadFileID = Val(auxDTValues_Old.gValue_Get("archivo", ""))
                auxContentIDARchivo = auxControlContenidoArchivo_FU.FileTmpID
                auxPrincipalPanel.gControls_Add(auxControlContenidoArchivo_FU)
                auxControlContenidoArchivo = auxControlContenidoArchivo_FU
                auxPrincipalPanel.BagValues.gValue_Add("contenido_archivo", auxContentIDARchivo)
            End If
        End If
        Dim auxControlContenidoArchivoSaved As clsHrcJSButton
        If Val(auxDTValues_Old.gValue_Get("version")) > 0 Then
            auxControlContenidoArchivoSaved = New clsHrcJSButton("contenidoarchivo_ro", "Ver contenido almacenado", auxHrcContext.ButtonCSS)
            'If auxWfwStateCod = enumWorkflowStep.coWFWSTPDOC_DOCDocumentovigente Then
            '    auxControlContenidoArchivoSaved.EventOnClick = "hrcShowWindowNoModal(""cfrmdocumentos_impresion.aspx?param1=" & auxCurrentCod & "&_closea_=1&_mode_=7"");return false;"
            'Else
            '    auxControlContenidoArchivoSaved.EventOnClick = "hrcShowWindowNoModal(""cfrmdocumentos_impresion.aspx?param1=" & auxCurrentCod & "&_closea_=1&_mode_=8"");return false;"
            'End If
            auxPrincipalPanel.gControls_Add(auxControlContenidoArchivoSaved)
            Select Case auxWfwStateCod
                Case enumWorkflowStep.coWFWSTPDOC_DOCDocumentovigente
                    auxControlContenidoArchivoSaved.EventOnClick = "hrcShowWindowNoModal(""cfrmdocumentos_impresion.aspx?param1=" & auxCurrentCod & "&_closea_=1&_mode_=8"");return false;"
                Case Else
                    'auxControlContenidoArchivoSaved = New clsHrcJSButton("contenidoarchivo_ro", "Ver contenido almacenado", auxHrcContext.ButtonCSS)
                    auxControlContenidoArchivoSaved.EventOnClick = "hrcShowWindowNoModal(""cfrmdocumentos_impresion.aspx?param1=" & auxCurrentCod & "&_closea_=1&_mode_=7"");return false;"
                    ' auxPrincipalPanel.gControls_Add(auxControlContenidoArchivoSaved)
            End Select
        End If


        'Contentytpeid

        'Tipo de contenido
        Dim auxContentTypeDT As New DataTable
        auxContentTypeDT.Columns.Add(New DataColumn("cod", Type.GetType("System.Int32")))
        auxContentTypeDT.Columns.Add(New DataColumn("dsc", Type.GetType("System.String")))
        auxNewRow = auxContentTypeDT.NewRow
        auxNewRow("cod") = 1
        auxNewRow("dsc") = "<img src=imagenes/qdoc_contenttypehtml.png class=hrcthemeimage width=24px >" _
            & "Editor de contenido"
        auxContentTypeDT.Rows.Add(auxNewRow)
        auxNewRow = auxContentTypeDT.NewRow
        auxNewRow("cod") = 2
        auxNewRow("dsc") = "<img src=imagenes/qdoc_contenttypefile.png class=hrcthemeimage width=24px >" _
            & "Adjuntar archivo"
        auxContentTypeDT.Rows.Add(auxNewRow)


        If auxCuerpoEdit Then
            Dim auxControlContentTypeID As clshrcJSComboBox
            auxControlContentTypeID = New clshrcJSComboBox("contentmode", "Tipo de contenido", "", auxHrcContext.ControlComboBoxCSS, auxContentTypeDT, "", "")
            auxControlContentTypeID.DisplayMode = clshrcJSComboBox.enumDisplayMode.Radio
            auxControlContentTypeID.gValue_Set(auxDTValues_Old.gValue_Get("contentypeid"))
            Select Case auxContentTypeID
                Case clsHrcConnClient.enumMimeTypes.coHTML
                    auxControlContentTypeID.DefaultValue = "1" '.gValue_Set("'1'")
                Case Else
                    auxControlContentTypeID.DefaultValue = "2" '.gValue_Set("'2'")
            End Select

            auxControlContentTypeID.JSEventChange = "" _
                        & "switch (pvalue){" _
                        & "case 1:case '1':" _
                        & auxControlContenidoHTML.gJS_Show & ";" _
                        & auxControlContenidoArchivo.gJS_Hide & ";" _
                        & "break;" _
                        & "case 2:case '2':" _
                        & auxControlContenidoHTML.gJS_Hide & ";" _
                        & auxControlContenidoArchivo.gJS_Show & ";" _
                        & auxControlContenidoArchivo.gJS_Value_Set("'" & auxContentIDARchivo & "'") & ";" _
                        & "break;" _
                        & "};"
            auxControlContentTypeID.ServerStateMantain = True
            auxHrcContext.gObjectTmp_UploadControl(auxControlContentTypeID)
            auxPrincipalPanel.gControls_Add(auxControlContentTypeID)
        Else
            Dim auxControlContentTypeID As clsHrcJSLabel
            auxControlContentTypeID = New clsHrcJSLabel(pControlID:="contentmode_ro")
            Select Case auxContentTypeID
                Case clsHrcConnClient.enumMimeTypes.coHTML
                    auxControlContentTypeID.gValue_Set("Tipo contenido:" _
                                                       & "<img src=imagenes/qdoc_contenttypehtml.png class=hrcthemeimage width=24px >" _
                                                       & "Editor de contenido")
                Case Else
                    auxControlContentTypeID.gValue_Set("Tipo contenido:" _
                                                       & "<img src=imagenes/qdoc_contenttypefile.png class=hrcthemeimage width=24px >" _
                                                       & "Archivo")
            End Select
            auxPrincipalPanel.gControls_Add(auxControlContentTypeID)
        End If


        'Clasificacion
        Dim auxControlClaCod As clsHrcJSControlBasic
        If auxClacodEdit Then
            Dim auxCLADT As DataTable = auxConn.gConn_Query("SELECT cod,dsc " _
                                                            & " FROM DOC_CLA " _
                                                            & " WHERE (DOC_CLA.baja = {#FALSE#} OR DOC_CLA.baja  {#ISNULL#})")
            auxControlClaCod = New clshrcJSComboBox("clacod", "Clasificación", "", auxHrcContext.ControlComboBoxCSS, auxCLADT, "", "")
            auxPrincipalPanel.gControls_Add(auxControlClaCod)
            auxControlClaCod.gValue_Set(auxDTValues_Old.gValue_Get("clacod"))
            auxControlClaCod.ServerStateMantain = True
            auxHrcContext.gObjectTmp_UploadControl(auxControlClaCod)
        Else
            auxControlClaCod = New clsHrcJSLabel(pControlID:="clacod_ro")
            auxPrincipalPanel.gControls_Add(auxControlClaCod)
            auxControlClaCod.gValue_Set(auxDTValues_Old.gValue_Get("cladsc"))
        End If

        'Fecha version actual
        Dim auxControlprofecha As clsHrcJSLabel = auxPrincipalPanel.gControl_Get("profecha")
        If auxControlprofecha Is Nothing Then
            auxControlprofecha = New clsHrcJSLabel(pControlID:="profecha")
            auxPrincipalPanel.gControls_Add(auxControlprofecha)
        End If

        Dim auxProFecha As Date = auxConn.gField_GetDate(auxDTValues_Old.gValue_Get("fecha"))
        If auxProFecha = Nothing Then
            auxControlprofecha.gValue_Set("-----")
        Else
            auxControlprofecha.gValue_Set(auxProFecha.ToString("d/M/yyyy"))
        End If

        'Identificador
        Dim auxControlIdentificador As clsHrcJSLabel = auxPrincipalPanel.gControl_Get("identificador")
        If auxControlIdentificador Is Nothing Then
            auxControlIdentificador = New clsHrcJSLabel(pControlID:="identificador")
            auxPrincipalPanel.gControls_Add(auxControlIdentificador)
        End If
        auxControlIdentificador.gValue_Set(auxDTValues_Old.gValue_Get("identificador", "").ToString)

        'Especifico a
        Dim auxObjectExplorerUndCod As clshrcObjectExplorer = auxPrincipalPanel.gControl_Get("especificoa")
        If auxObjectExplorerUndCod Is Nothing Then
            auxObjectExplorerUndCod = New clshrcObjectExplorer("especificoa", "hrcGrdData.ashx", hrcDT_Cache(enumDTCache.coUNDHierarchy), auxConn, auxClientCon)
            auxPrincipalPanel.gControls_Add(auxObjectExplorerUndCod)
            If auxWfwMode = enumWfwMode.coUserCreate Then
                'Select Case auxWfwStateCod
                '    Case enumWorkflowStep.coWFWSTPDOC_DOCSolicitudnuevodocumento, enumWorkflowStep.coWFWSTPDOC_DOCSolicitudnuevaversion, enumWorkflowStep.coWFWSTPDOC_DOCnuevaversion
                auxDOC_DOC_UND_PermEditor = "-1"
                'End Select

            End If

            Select Case auxDOC_DOC_UND_PermEditor
                Case ""
                Case "-1"
                    auxObjectExplorerUndCod.gAutosuggest_Enabled("SELECT UND.cod AS cod,UND.dsc as dsc," & enumEntities.coEntityUND & " as q_type,'Unidad' FROM UND   WHERE (  (UND.dsc LIKE '%{#HRCDSC#}%' ) AND (UND.baja = 0 OR UND.baja  IS NULL)) AND UND.cod >= 1" _
                                                        & " ORDER BY dsc,q_type")
                    auxObjectExplorerUndCod.CentralGrid.gSelectList_Add(enumEntities.coEntityUND, enumEntities.coEntityUND, "(SELECT UND.cod AS cod,UND.dsc as dsc," & enumEntities.coEntityUND & " as q_type FROM UND   WHERE ((((UND.undcodsup = {#HRCCOD#} AND '{#HRCDSC#}'='')  OR  (UND.dsc LIKE '%{#HRCDSC#}%' AND '{#HRCDSC#}'<>''))) AND (UND.baja = 0 OR UND.baja  IS NULL)) AND UND.cod >= 1)")
                    auxObjectExplorerUndCod.CentralGrid.gSelectTypeFilter_Add(enumEntities.coEntityUND)
                    auxObjectExplorerUndCod.ObjectExplorerEnabled = True
                Case Else
                    auxObjectExplorerUndCod.gAutosuggest_Enabled("SELECT UND.cod AS cod,UND.dsc as dsc," & enumEntities.coEntityUND & " as q_type,'Unidad' FROM UND   WHERE (  (UND.dsc LIKE '%{#HRCDSC#}%' ) AND (UND.baja = 0 OR UND.baja  IS NULL)) AND UND.cod >= 1" _
                                                        & " AND cod IN (" & auxDOC_DOC_UND_PermEditor & ")" _
                                                        & " ORDER BY dsc,q_type")
                    auxObjectExplorerUndCod.ObjectExplorerEnabled = False 'solo permite autosugerido
            End Select

            If auxCurrentCod > 0 Then
                Dim auxUndCod As Integer = auxClass.Conn.gField_GetInt(auxDT.Rows(0)("undcod"), -1)
                If auxUndCod > 0 Then
                    auxObjectExplorerUndCod.gStartItem_Add(enumEntities.coEntityUND, auxUndCod, hrcEntityDT_UND_FindByKey(auxDT.Rows(0)("undcod"))("dsc"))
                End If

                For Each auxUndRow As DataRow In auxConn.gConn_Query("SELECT undcod,UND.dsc AS unddsc " _
                                                               & " FROM DOC_DOCUND " _
                                                               & " LEFT JOIN UND ON DOC_DOCUND.undcod=UND.cod " _
                                                               & " WHERE DOC_DOCUND.undcod > 0 AND DOC_DOCUND.doccod=" & auxCurrentCod).Rows
                    auxObjectExplorerUndCod.gStartItem_Add(enumEntities.coEntityUND, auxUndRow("undcod"), auxUndRow("unddsc"))
                Next
            ElseIf auxDOC_DOC_UND_PermEditor <> "" And auxDOC_DOC_UND_PermEditor <> "-1" Then
                'Es uno nuevo
                Dim auxFirstUND As Integer = Split(auxDOC_DOC_UND_PermEditor, ",")(0)
                auxObjectExplorerUndCod.gStartItem_Add(enumEntities.coEntityUND, auxFirstUND, hrcEntityDT_UND_FindByKey(auxFirstUND)("dsc"))
                auxClass.gEntity_DOC_DOCUND_InsertInDraft(pdoccod:=auxCurrentCod, pundcod:=auxFirstUND, pdftdidgencod:=auxDfdGenCod)
                auxObjectExplorerUndCod.BagValues.gValue_Add("doc_doc_und_permeditor", auxDOC_DOC_UND_PermEditor)
            End If
        End If
        auxObjectExplorerUndCod.IsReadOnly = Not auxUndcodEdit

        'PRODOC
        Dim auxDocuments As clshrcGrdData = gPanelDOCUMENTS_Get(auxClass, auxHrcContext, auxPrincipalPanel, auxDocumentsEdit, auxDfdGenCod, auxCurrentCod)
        If auxDocuments IsNot Nothing Then
            auxPrincipalPanel.gControls_Add(auxDocuments)
            auxHrcContext.gObjectTmp_UploadControl(auxDocuments)
        End If

        'If auxGrdTRO IsNot Nothing Then
        '    auxPrincipalPanel.gControls_Add(auxGrdTRO)
        'End If
        If auxEditOptions_PublicacionAuto_Enabled Then
            Dim auxControlOption_PublicacionAuto As clsHrcJSControlBasic
            If auxEditOptions Then
                auxControlOption_PublicacionAuto = New clshrcJSDateControl(pControlID:="publicacionauto", pTitle:="Fecha publicacion automática:", pFieldData:="publicacionauto" _
                                                                            , pCSSClass:=auxHrcContext.ControlTextBoxCSS, pNumberOfMonths:=1, pWidth:="150px", pFormat:="")
                auxControlOption_PublicacionAuto.ServerStateMantain = True
                auxHrcContext.gObjectTmp_UploadControl(auxControlOption_PublicacionAuto)
                auxControlOption_PublicacionAuto.gValue_Set(auxDTValues_Old.gValue_Get("publicacionauto", "").ToString)
            Else
                auxControlOption_PublicacionAuto = New clsHrcJSLabel(pControlID:="publicacionauto_ro", pTitle:="")
                Dim auxFechaPublicacionAuto As Date = auxClass.Conn.gField_GetDate(auxDTValues_Old.gValue_Get("publicacionauto", Nothing))
                If auxFechaPublicacionAuto = Nothing Then
                    auxControlOption_PublicacionAuto.gValue_Set("Sin fecha de publicación automática")
                Else
                    auxControlOption_PublicacionAuto.gValue_Set("Fecha publicacion automática:" & auxFechaPublicacionAuto.ToString("d/M/yyyy"))
                End If
            End If
            auxPrincipalPanel.gControls_Add(auxControlOption_PublicacionAuto)
        End If

        'Dim auxstt_publicacionautodias As New clshrcJSTextBox("stt_publicacionauto_dias", "Publicación automática(días)", "stt_publicacionauto_dias", auxHrcContext.ControlTextBoxCSS, 5, "100px", "", False, "", "")
        'auxstt_publicacionautodias.Tooltip = "0=Nunca"
        'auxPrincipalPanel.gControls_Add(auxstt_publicacionautodias)
        'AddHandler auxstt_homologacionautodias.EventCommandHandler, AddressOf gPnlOptions_CommandHandler

        'Diseño HTML
        Dim auxPanelHTML As String = ""
        Dim auxNavMenu As String = ""
        Select Case auxMode
            Case enumActionType.coViewDetail
                auxTitle = "Ver"
            Case enumActionType.coDelete
                auxTitle = "Borrar"
            Case enumActionType.coNew
                auxTitle = "Nuevo"
            Case enumActionType.coModify
                auxTitle = "Modificar"
        End Select
        auxNavMenu &= "<div style=""font-size:16px;margin-left:20px;margin-right:25px;"">" _
           & "<img src=imagenes/biblioteca_documentos.png width=20 />" & auxTitle _
           & "</div>"
        auxPanelHTML &= auxNavMenu _
                        & "<div style=""min-height:14px;margin-left:20px;margin-right:25px;font-size:14px;text-decoration:blink"" >{#CONTROLS.LBLERROR#}</div>" _
                        & "<div  class=""ui-tabs ui-widget-header ui-corner-all"" style="";margin-left:20px;margin-right:25px;"">" _
                        & "{#PANEL.BUTTONS#}" _
                        & "</div>" _
                        & "<div  class=""ui-tabs ui-widget ui-widget-content ui-corner-all"" style="";margin-left:20px;margin-right:25px;"">" _
                        & "<table cellpadding=""0"" cellspacing=""0"" style=""table-layout:fixed;width:100%;" & auxFormBGColor & """> " _
                        & "<tr>" _
                            & "<td class=""pryceldatitulos ui-widget-header"" style=""border-right-style:solid;width:40%;"" colspan=""2"" >Título:</td>" _
                            & "<td class=""pryceldatitulos ui-widget-header"" style=""border-right-style:solid;width:15%;""  >Tipo:</td>" _
                            & "<td class=""pryceldatitulos ui-widget-header"" style=""border-right-style:solid;width:15%;""  >Estado:</td>" _
                            & "<td class=""pryceldatitulos ui-widget-header"" style=""border-right-style:solid;width:15%;""  >Nro:</td>" _
                            & "<td class=""pryceldatitulos ui-widget-header"" style=""border-right-style:solid;width:15%;""  >Versión:</td>" _
                        & "</tr>" _
                        & "<tr>" _
                            & "<td class=""pryceldaotro"" style=""border-right-style:solid;"" colspan=""2"" >{#CONTROLS.DSC0#}{#CONTROLS.DSC1#}{#CONTROLS.DSC2#}{#CONTROLS.DSC0_RO#}{#CONTROLS.DSC1_RO#}{#CONTROLS.DSC2_RO#}</td>" _
                            & "<td class=""pryceldaotro"" style=""border-right-style:solid;""  >{#CONTROLS.TIPCOD#}{#CONTROLS.TIPCOD_RO#}</td>" _
                            & "<td class=""pryceldaotro"" style=""border-right-style:solid;""  >{#CONTROLS.WFWSTATUS#}</td>" _
                            & "<td class=""pryceldaotro"" style=""border-right-style:solid;""  >{#CONTROLS.NRO#}{#CONTROLS.NRO_RO#}</td>" _
                            & "<td class=""pryceldaotro"" style=""border-right-style:solid;""  >{#CONTROLS.VERSION#}</td>" _
                        & "</tr>"  
        If auxObsVisible Then
            auxPanelHTML &= "<tr>" _
                            & "<td class=""pryceldatitulos"" style=""border-right-style:solid;"" colspan=""6"" >Breve descripción:</td>" _
                        & "</tr>" _
                        & "<tr>" _
                            & "<td class=""pryceldaotro"" style=""border-right-style:solid;"" colspan=""6"" >{#CONTROLS.OBS#}</td>" _
                        & "</tr>"
        End If
        auxPanelHTML &= "<tr>"
        If auxSisCod_Mandatory = -1 Then
            auxPanelHTML &= "<td class=""pryceldatitulos"" style=""border-right-style:solid;"" >Específico a:</td>"
            auxPanelHTML &= "<td class=""pryceldatitulos"" style=""border-right-style:solid;"" >Sistema:</td>"
        Else
            auxPanelHTML &= "<td class=""pryceldatitulos"" style=""border-right-style:solid;"" colspan=""2""  >Específico a:</td>"
        End If
        auxPanelHTML &= "<td class=""pryceldatitulos"" style=""border-right-style:solid;"" colspan=""2"" >Dependencia:</td>" _
                      & "<td class=""pryceldatitulos"" style=""border-right-style:solid;""  >Clasificación:</td>" _
                      & "<td class=""pryceldatitulos"" style=""border-right-style:solid;""  >Identificador:</td>" _
                   & "</tr>"
        auxPanelHTML &= "<tr>"

        If auxSisCod_Mandatory = -1 Then
            auxPanelHTML &= "<td class=""pryceldaotro"" style=""border-right-style:solid;"" >{#CONTROLS.ESPECIFICOA#}</td>"
            auxPanelHTML &= "<td class=""pryceldaotro"" style=""border-right-style:solid;"" >{#CONTROLS.SISCOD#}{#CONTROLS.SISCOD_RO#}</td>"
        Else
            auxPanelHTML &= "<td class=""pryceldaotro"" style=""border-right-style:solid;"" colspan=""2"" >{#CONTROLS.ESPECIFICOA#}</td>"

        End If
        auxPanelHTML &= "<td class=""pryceldaotro"" style=""border-right-style:solid;"" colspan=""2"" >{#CONTROLS.PROCOD#}</td>" _
                        & "<td class=""pryceldaotro"" style=""border-right-style:solid;""  >{#CONTROLS.CLACOD#}{#CONTROLS.CLACOD_RO#}</td>" _
                        & "<td class=""pryceldaotro"" style=""border-right-style:solid;""  >{#CONTROLS.IDENTIFICADOR#}</td>" _
                        & "</tr>" _
                        & "<tr>" _
                            & "<td class=""pryceldatitulos"" style=""border-right-style:solid;"" colspan=""6"" >CONTENIDO:</td>" _
                        & "</tr>" _
                        & "<tr>" _
                            & "<td class=""pryceldaotro"" style=""border-top-color:gray;border-top-width:1px;border-top-style:solid;"" colspan=""6"" >" _
                            & "{#CONTROLS.CONTENIDOARCHIVO_RO#}{#CONTROLS.CONTENTMODE#}{#CONTROLS.CONTENTMODE_RO#}" _
                            & "<br />" _
                            & "{#CONTROLS.CONTENIDOHTML#}{#CONTROLS.CONTENIDOARCHIVO#}" _
                            & "</td>" _
                        & "</tr>"

        If auxEditOptions_Visible Then
            auxPanelHTML &= "<tr>" _
                            & "<td class=""pryceldatitulos"" style=""border-right-style:solid;"" colspan=""6"" >OPCIONES:</td>" _
                        & "</tr>" _
                        & "<tr>" _
                            & "<td class=""pryceldaotro"" style=""border-top-color:gray;border-top-width:1px;border-top-style:solid;padding:0px;"" colspan=""6"" >" _
                            & "{#CONTROLS.PUBLICACIONAUTO.TITLE#}{#CONTROLS.PUBLICACIONAUTO#}" _
                            & "{#CONTROLS.PUBLICACIONAUTO_RO.TITLE#}{#CONTROLS.PUBLICACIONAUTO_RO#}" _
                            & "</td>" _
                        & "</tr>"
        End If
        auxPanelHTML &= "<tr>" _
                            & "<td class=""pryceldatitulos"" style=""border-right-style:solid;"" colspan=""6"" >DOCUMENTOS ANEXOS:</td>" _
                        & "</tr>" _
                        & "<tr>" _
                            & "<td class=""pryceldaotro"" style=""border-top-color:gray;border-top-width:1px;border-top-style:solid;padding:0px;"" colspan=""6"" >{#CONTROLS.GRDDOC#}</td>" _
                        & "</tr>"
        auxPanelHTML &= "<tr>" _
            & "<td class=""pryceldaotro"" style=""border-top-color:gray;border-top-width:1px;border-top-style:solid;padding:0px;"" colspan=""6"" >{#CONTROLS.PNLPANEL_PNLTRO#}</td>" _
        & "</tr>" _
        & "</table>" _
        & "</div>" _
        & "<div>{#CONTROLS.PNLPANEL_PNLLOG#}</div>" _
        & "<div></div>"
        auxPrincipalPanel.gHTML_SetTemplate(auxPanelHTML, "", "", "", "")

        auxClass.Conn.gConn_Close()


        'Botones
        Dim auxControlButton_Cancel As New clsHrcJSButton("cmdFormViewItemCancel", "Cancelar", auxHrcContext.ButtonCSS)
        auxPrincipalPanel.gControls_Add(auxControlButton_Cancel)
        auxControlButton_Cancel.EventOnClick = auxPrincipalPanel.BagValues.gValue_Get("form_JScancel") & ";return false;"
        If auxcontrolButton_ConfirmUpdate_visible Then
            Dim auxControlButton_ConfirmSave As New clsHrcJSButton("cmdSave", "Guardar", auxHrcContext.ButtonCSS)
            auxPrincipalPanel.gControls_Add(auxControlButton_ConfirmSave)
            auxControlButton_ConfirmSave.EventOnClick = auxControlError.gJS_Value_Set("''") & ";" _
                        & auxFormPNLLOG.gJSCommand_Get(pCommandName:="DOC_UPDATE") & ";return false;"
        End If
        If auxcontrolButton_ConfirmInsert_visible Then
            Dim auxControlButton_ConfirmSave As New clsHrcJSButton("cmdSaveInsert", "Guardar", auxHrcContext.ButtonCSS)
            auxPrincipalPanel.gControls_Add(auxControlButton_ConfirmSave)
            auxControlButton_ConfirmSave.EventOnClick = auxControlError.gJS_Value_Set("''") & ";" _
                        & auxFormPNLLOG.gJSCommand_Get(pCommandName:="DOC_UPDATE") & ";return false;"
        End If
        If auxcontrolButton_ConfirmDelete_visible Then
            Dim auxControlButton_ConfirmSave As New clsHrcJSButton("cmdSaveDelete", "Borrar", auxHrcContext.ButtonCSS & " hrcwfwbutton_warning")
            auxPrincipalPanel.gControls_Add(auxControlButton_ConfirmSave)
            auxControlButton_ConfirmSave.EventOnClick = auxControlError.gJS_Value_Set("''") & ";" _
                        & auxFormPNLLOG.gJSCommand_Get(pCommandName:="DOC_DELETE") & ";return false;"
        End If
        If auxControlButton_ItemUpdate_Visible Then
            Dim auxControlButton_ConfirmSave As New clsHrcJSButton("cmdFormViewItemUpdate", "Editar", auxHrcContext.ButtonCSS)
            auxPrincipalPanel.gControls_Add(auxControlButton_ConfirmSave)
            auxQueryStringValues.gValue_Add("_mode_", CInt(enumActionType.coModify))
            auxControlButton_ConfirmSave.EventOnClick = auxHTML.gJS_GotoURL("'" & Request.Url.AbsolutePath & "?" & auxHTML.gQueryString_Get(auxQueryStringValues) & "'") & ";return false;"
        End If
        'Falta implementar nuevo con copia
        'If auxControlButton_ItemCopy_Visible Then
        '    Dim auxControlButton_ConfirmSave As New clsHrcJSButton("cmdFormViewItemCopy", "Nuevo con copia", auxHrcContext.ButtonCSS)
        '    auxPrincipalPanel.gControls_Add(auxControlButton_ConfirmSave)
        '    auxQueryStringValues.gValue_Add("_mode_", CInt(enumActionType.coNewFromOther))
        '    auxControlButton_ConfirmSave.EventOnClick = auxHTML.gJS_GotoURL("'" & Request.Url.AbsolutePath & "?" & auxHTML.gQueryString_Get(auxQueryStringValues) & "'") & ";return false;"
        'End If
        If auxControlButton_Reapply_Visible Then
            Dim auxControlButton_ReepplyRoles As New clsHrcJSButton("cmdReapplyRoles", "Reaplicar roles", auxHrcContext.ButtonCSS)
            auxPrincipalPanel.gControls_Add(auxControlButton_ReepplyRoles)
            auxControlButton_ReepplyRoles.EventOnClick = "if (confirm('Confirma que reaplicará los permisos?')){" _
                     & auxFormPNLLOG.gJSCommand_Get(pCommandName:="DOCROLREAPPLY") & ";" _
                     & auxPrincipalPanel.BagValues.gValue_Get("form_JScancel") & ";" _
                     & "};return false;"
        End If

        'Boton log
        If auxControlButton_Log_Visible Then
            Dim auxControlButton_Log As New clsHrcJSButton("cmdPROLOG", "Historia", auxHrcContext.ButtonCSS)
            auxPrincipalPanel.gControls_Add(auxControlButton_Log)
            auxControlButton_Log.EventOnClick = "hrcShowModal(""cfrmDocumentoslog_det.aspx?_mode_=0&_closea_=1&param1=" & auxCurrentCod & """);return false;"
        End If
        'Boton permisos
        If auxControlButton_PERM_Visible Then
            Dim auxControlButton_Perm As New clsHrcJSButton("cmdPERM", "Permisos", auxHrcContext.ButtonCSS)
            auxPrincipalPanel.gControls_Add(auxControlButton_Perm)
            auxControlButton_Perm.EventOnClick = "hrcShowModal(""frmsecperm.aspx?_mode_=0&_closea_=1&param1=" & auxSidCod & """);return false;"
        End If
        'Boton permisos
        If auxControlButton_ROL_EMP_Visible Then
            Dim auxControlButton_ROL_EMP As New clsHrcJSButton("cmdROL_EMP", "Audiencia", auxHrcContext.ButtonCSS)
            auxPrincipalPanel.gControls_Add(auxControlButton_ROL_EMP)
            auxControlButton_ROL_EMP.EventOnClick = "hrcShowModal(""cfrmreportes.aspx?_view_=6&_mode_=5&_closea_=1&param1=" & auxCurrentCod & """);return false;"
        End If
        'Boton debug
        If auxControlButton_Trace_Visible Then
            Dim auxControlButton_PROTRACE As New clsHrcJSButton("cmdPROTRACE", "Auditoría", auxHrcContext.ButtonCSS)
            auxPrincipalPanel.gControls_Add(auxControlButton_PROTRACE)
            auxControlButton_PROTRACE.EventOnClick = "hrcShowModal(""cfrmtrace.aspx?_mode_=0&_closea_=1&param1=" & auxCurrentCod & """);return false;"
        End If
        If auxControlButton_DOCSIGN_Visible Then
            Dim auxControlButton_DOCSIGN As New clsHrcJSButton("cmdDOCSIGN", "<img src=""" & auxClass.WebRootFolder & "imagenes/actsign.png"" width=16px />" _
                                                               & "Firmas pendientes", auxHrcContext.ButtonCSS)
            auxControlButton_DOCSIGN.DesignType = clsHrcJSButton.enumDesignType.coHyperlink
            auxPrincipalPanel.gControls_Add(auxControlButton_DOCSIGN)
            auxControlButton_DOCSIGN.EventOnClick = "hrcShowModal(""cfrmDocumentossgn_det.aspx?_mode_=0&_closea_=1&param1=" & auxCurrentCod & """);return false;"
        End If
        If auxControlButton_Versions_Visible Then
            Dim auxControlButton_PROVER As New clsHrcJSButton("cmdPROVER", "Versiones", auxHrcContext.ButtonCSS)
            auxPrincipalPanel.gControls_Add(auxControlButton_PROVER)
            auxControlButton_PROVER.EventOnClick = "hrcShowModal(""cfrmDocumentoslog_det.aspx?_mode_=0&_view_=1&_closea_=1&param1=" & auxCurrentCod & """);return false;"
        End If
        If auxControlButton_VersionsChanges_Visible Then
            Dim auxControlButton_EVersiones As New clsHrcJSButton("cmdEVersiones", "Ver cambios de versiones", auxHrcContext.ButtonCSS)
            auxPrincipalPanel.gControls_Add(auxControlButton_EVersiones)
            auxControlButton_EVersiones.EventOnClick = "hrcShowModal(""cfrmdocumentos_cambios.aspx?_closea_=1&param1=" & auxCurrentCod & """);return false;"
        End If

        If auxContentTypeID = clsHrcConnClient.enumMimeTypes.coHTML And auxcontrolButton_CopyNoControled Then
            Dim auxControlButton_ImprimirCopiaNoControlada As New clsHrcJSButton("cmdImprimirCopiaNoControlada", "Imprimir copia NO controlada", auxHrcContext.ButtonCSS)
            auxPrincipalPanel.gControls_Add(auxControlButton_ImprimirCopiaNoControlada)
            auxControlButton_ImprimirCopiaNoControlada.EventOnClick = "if (confirm('Confirma la impresión de copia NO CONTROLADA?')) {hrcShowWindowNoModal(""cfrmdocumentos_impresion.aspx?_closea_=1&_mode_=4&param1=" & auxCurrentCod & """);return false;};"
        End If
        If auxContentTypeID = clsHrcConnClient.enumMimeTypes.coHTML And auxcontrolButton_CopyControled Then
            Dim auxControlButton_ImprimirCopiaControlada As New clsHrcJSButton("cmdImprimirCopiaControlada", "Imprimir copia controlada", auxHrcContext.ButtonCSS)
            auxPrincipalPanel.gControls_Add(auxControlButton_ImprimirCopiaControlada)
            auxControlButton_ImprimirCopiaControlada.EventOnClick = "if (confirm('Confirma la impresión de COPIA CONTROLADA?')) {hrcShowWindowNoModal(""cfrmdocumentos_impresion.aspx?_closea_=1&_mode_=5&param1=" & auxCurrentCod & """);return false;};"
        End If
        If auxcontrolbutton_PROPRN_Visible Then
            Dim auxControlButton_PROPRN As New clsHrcJSButton("cmdPROPRN", "Copias", auxHrcContext.ButtonCSS)
            auxPrincipalPanel.gControls_Add(auxControlButton_PROPRN)
            auxControlButton_PROPRN.EventOnClick = "hrcShowModal(""cfrmDocumentoslog_det.aspx?_mode_=0&_view_=2&_closea_=1&param1=" & auxCurrentCod & """);return false;"
        End If
        If auxControlButton_DOCREF_Visible Then
            Dim auxControlButton_DOCREF As New clsHrcJSButton("cmdDOCREF", "Referencias", auxHrcContext.ButtonCSS)
            auxPrincipalPanel.gControls_Add(auxControlButton_DOCREF)
            auxControlButton_DOCREF.EventOnClick = "hrcShowModal(""cfrmDocumentosref_det.aspx?_mode_=0&_closea_=1&param1=" & auxCurrentCod & """);return false;"
        End If

        If auxContentTypeID = clsHrcConnClient.enumMimeTypes.coHTML And auxControlButton_PDFView_Visible Then
            Dim auxControlButton_VerPDF As New clsHrcJSButton("cmdVerPDF", "Ver PDF", auxHrcContext.ButtonCSS)
            auxPrincipalPanel.gControls_Add(auxControlButton_VerPDF)
            auxControlButton_VerPDF.EventOnClick = "hrcShowWindowNoModal(""cfrmdocumentos_impresion.aspx?_closea_=1&_mode_=9&param1=" & auxCurrentCod & "&param2=" & auxDfdGenCod & """);return false;"
        End If

        Dim auxpnllog_wfwstpcodnext As clsHrcJSHidden = auxFormPNLLOG.gControl_Get("pnllog_wfwstpcodnext")
        Dim auxpnllog_action As clsHrcJSHidden = auxFormPNLLOG.gControl_Get("pnllog_action")
        Dim auxWFWSTPButtons As New List(Of clsWorkflowButtons)
        If auxControlButton_GotoStepSolDoc_Visible Then
            auxWFWSTPButtons.Add(New clsWorkflowButtons("cmdGotoStepSolDoc", "Solicitar este documento", enumWorkflowStep.coWFWSTPDOC_DOCSolicitudnuevodocumento))
        End If
        If auxControlButton_GotoStepSolDocOK_Visible Then
            auxWFWSTPButtons.Add(New clsWorkflowButtons("cmdGotoStepSolDocOK", "Confirmar solicitud nuevo documento", enumWorkflowStep.coWFWSTPDOC_DOCSolicitudnuevodocumentoconfirmada))
        End If
        If auxControlButton_GotoStepSolDocNOK_Visible Then
            auxWFWSTPButtons.Add(New clsWorkflowButtons("cmdGotoStepSolDocNOK", "Rechazar solicitud nuevo documento", enumWorkflowStep.coWFWSTPDOC_DOCSolicitudnuevodocumentorechazada, enumJobResult.coWarning))
        End If
        If auxControlButton_GotoStepEdicion_Visible Then
            auxWFWSTPButtons.Add(New clsWorkflowButtons("cmdGotoStepEdicion", "Edición", enumWorkflowStep.coWFWSTPDOC_DOCEdicion))
        End If
        If auxControlButton_GotoStepEdicionOK_Visible Then
            auxWFWSTPButtons.Add(New clsWorkflowButtons("cmdGotoStepEdicionOK", "Confirmar edición", enumWorkflowStep.coWFWSTPDOC_DOCEdicionOK))
        End If
        If auxControlButton_GotoStepRevisionOK_Visible Then
            auxWFWSTPButtons.Add(New clsWorkflowButtons("cmdGotoStepRevisionOK", "Confirmar revisión", enumWorkflowStep.coWFWSTPDOC_DOCrevisionOK))
        End If
        If auxControlButton_GotoStepAprobacionOK_Visible Then
            auxWFWSTPButtons.Add(New clsWorkflowButtons("cmdGotoStepAprobacionOK", "Confirmar aprobación", enumWorkflowStep.coWFWSTPDOC_DOCaprobacionOK))
        End If
        If auxControlButton_GotoStepPublicacionOK_Visible Then
            auxWFWSTPButtons.Add(New clsWorkflowButtons("cmdGotoStepPublicacionOK", "Confirmar publicación", enumWorkflowStep.coWFWSTPDOC_DOCpublicacionOK))
        End If
        If auxControlButton_GotoStepLecturaOK_Visible Then
            auxWFWSTPButtons.Add(New clsWorkflowButtons("cmdGotoStepLecturaOK", "Confirmar lectura", enumWorkflowStep.coWFWSTPDOC_DOCLecturaOK))
        End If
        If auxControlButton_GotoStepSolicitudNuevaVersion_Visible Then
            auxWFWSTPButtons.Add(New clsWorkflowButtons("cmdGotoStepSolicitudNuevaVersion", "Solicitar nueva versión", enumWorkflowStep.coWFWSTPDOC_DOCSolicitudnuevaversion))
        End If
        If auxControlButton_GotoStepCancelarNuevaVersion_Visible Then
            auxWFWSTPButtons.Add(New clsWorkflowButtons("cmdGotoStepCancelarNuevaVersion", "Cancelar nueva versión", enumWorkflowStep.coWFWSTPDOC_DOCVersionanulada))
        End If
        If auxControlButton_GotoStepNuevaVersion_Visible Then
            auxWFWSTPButtons.Add(New clsWorkflowButtons("cmdGotoStepNuevaVersion", "Nueva versión", enumWorkflowStep.coWFWSTPDOC_DOCnuevaversion))
        End If
        If auxControlButton_GotoStepSolicitudNuevaVersionOK_Visible Then
            Select Case auxWfwMode
                Case enumWfwMode.coStandard
                    If Val(coSystemType) = 175 Then
                        auxWFWSTPButtons.Add(New clsWorkflowButtons("cmdGotoStepNuevaVersionOK", "Solicitar nueva versión", enumWorkflowStep.coWFWSTPDOC_DOCnuevaversion))
                    Else
                        auxWFWSTPButtons.Add(New clsWorkflowButtons("cmdGotoStepNuevaVersionOK", "Confirmar nueva versión", enumWorkflowStep.coWFWSTPDOC_DOCnuevaversion))
                    End If
                Case enumWfwMode.coUserCreate
                    auxWFWSTPButtons.Add(New clsWorkflowButtons("cmdGotoStepNuevaVersionOK", "Confirmar solicitud nueva versión", enumWorkflowStep.coWFWSTPDOC_DOCSolnuevaversionOK))
            End Select
        End If
        If auxControlButton_GotoStepCancel_Visible Then
            auxWFWSTPButtons.Add(New clsWorkflowButtons("cmdGotoStepCancelacion", "Cancelación", enumWorkflowStep.coWFWSTPDOC_DOCCancelacion))
        End If
        If auxControlButton_GotoStepCancelOK_Visible Then
            auxWFWSTPButtons.Add(New clsWorkflowButtons("cmdGotoStepCancelOK", "Confirmar cancelación", enumWorkflowStep.coWFWSTPDOC_DOCVersionanulada))
        End If
        If auxControlButton_GotoStepCancelNOK_Visible Then
            auxWFWSTPButtons.Add(New clsWorkflowButtons("cmdGotoStepCancelNOK", "Anular cancelación", enumWorkflowStep.coWFWSTPDOC_DOCRechazarcancelacion, enumJobResult.coWarning))
        End If
        If auxControlButton_GotoStepRevisionNOK_Visible Then
            auxWFWSTPButtons.Add(New clsWorkflowButtons("cmdGotoStepRevisionNOK", "Rechazar revisión", enumWorkflowStep.coWFWSTPDOC_DOCRevisionrechazada, enumJobResult.coWarning))
        End If
        If auxControlButton_GotoStepAprobacionNOK_Visible Then
            auxWFWSTPButtons.Add(New clsWorkflowButtons("cmdGotoStepAprobacionNOK", "Rechazar aprobación", enumWorkflowStep.coWFWSTPDOC_DOCAprobacionrechazada, enumJobResult.coWarning))
        End If
        If auxControlButton_GotoStepSolicitudEliminacion_Visible Then
            auxWFWSTPButtons.Add(New clsWorkflowButtons("cmdGotoStepSolicitudEliminacion", "Solicitar eliminación", enumWorkflowStep.coWFWSTPDOC_DOCSolicitudeliminacion, enumJobResult.coWarning))
        End If
        If auxControlButton_GotoStepEliminacionOK_Visible Then
            auxWFWSTPButtons.Add(New clsWorkflowButtons("cmdGotoStepEliminacionOK", "Confirmar eliminación", enumWorkflowStep.coWFWSTPDOC_DOCEliminacionOK, enumJobResult.coWarning))
        End If
        If auxControlButton_GotoStepEliminacionNOK_Visible Then
            auxWFWSTPButtons.Add(New clsWorkflowButtons("cmdGotoStepEliminacionNOK", "Rechazar eliminación", enumWorkflowStep.coWFWSTPDOC_DOCRechazareliminacion, enumJobResult.coWarning))
        End If
        If auxControlButton_GotoStepSolicitudNuevaVersionNOK_Visible Then
            Select Case auxWfwMode
                Case enumWfwMode.coStandard
                    If Val(coSystemType) = 175 Then
                        auxWFWSTPButtons.Add(New clsWorkflowButtons("cmdGotoStepNuevaversionNOK", "Confirmar versión actual", enumWorkflowStep.coWFWSTPDOC_DOCnuevaversionNOK, enumJobResult.coWarning))
                    Else
                        auxWFWSTPButtons.Add(New clsWorkflowButtons("cmdGotoStepNuevaversionNOK", "Rechazar nueva versión", enumWorkflowStep.coWFWSTPDOC_DOCnuevaversionNOK, enumJobResult.coWarning))
                    End If
                Case enumWfwMode.coUserCreate
                    auxWFWSTPButtons.Add(New clsWorkflowButtons("cmdGotoStepNuevaversionNOK", "Rechazar solicitud nueva versión", enumWorkflowStep.coWFWSTPDOC_DOCnuevaversionNOK, enumJobResult.coWarning))
            End Select

        End If
        If auxControlButton_GotoStepReedicion_Visible Then
            auxWFWSTPButtons.Add(New clsWorkflowButtons("cmdGotoStepReedicion", "Reeditar", enumWorkflowStep.coWFWSTPDOC_DOCDocumentovigente))
        End If
        If auxControlButton_GotoStepReID_Visible Then
            auxWFWSTPButtons.Add(New clsWorkflowButtons("cmdGotoStepReID", "Reidentificación", enumWorkflowStep.coWFWSTPDOC_DOCReidentificacion))
        End If
        If auxControlButton_GotoStepEliminacionTotal_Visible Then
            auxWFWSTPButtons.Add(New clsWorkflowButtons("cmdGotoStepEliminacionTotal", "Eliminación total", enumWorkflowStep.coWFWSTPDOC_DOCEliminaciontotal, enumJobResult.coWarning))
        End If
        Dim auxButtonCSS As String
        For Each auxWFWButton As clsWorkflowButtons In auxWFWSTPButtons
            auxButtonCSS = ""
            If auxWFWButton.WorkflowStepType = enumJobResult.coWarning Then
                auxButtonCSS = "hrcwfwbutton_warning"
            Else
                auxButtonCSS = "hrcwfwbutton_normal"
            End If
            Dim auxControlButton_WfwButton As New clsHrcJSButton(auxWFWButton.ControlID, auxWFWButton.Title, auxHrcContext.ButtonCSS & " " & auxButtonCSS)
            auxPrincipalPanel.gControls_Add(auxControlButton_WfwButton)
            auxControlButton_WfwButton.EventOnClick = auxControlError.gJS_Value_Set("''") & ";" _
            & auxpnllog_wfwstpcodnext.gJS_Value_Set(auxWFWButton.WorkflowStep) & ";" _
            & auxpnllog_action.gJS_Value_Set(enumActionType.coWfwGoStep) & ";" _
            & auxFormPNLLOG.gJSCommand_Get(pCommandName:="VIEW") & ";return false;"
            If auxWFWButton.StartVisible = False Then
                auxControlButton_WfwButton.Visible = False
            End If
        Next

        Dim auxReturn As New clshrcBagValues
        auxReturn.gValue_Add("CONTENT", auxPrincipalPanel.gControl_GetBodyDefinition)
        auxReturn.gValue_Add("SCRIPT", auxPrincipalPanel.gControl_GetStartupScripts)
        Return auxReturn
    End Function
    'Private Sub gCuerpo_SetValue(ByVal pText As String, _
    '                             ByVal pReadWrite As Boolean, _
    '                             ByVal phdnValue As HiddenField, _
    '                             ByVal pButtton As Button, _
    '                             ByVal pForm As String)
    '    Dim auxClass As New clsCusimDOC
    '    auxClass.Conn.gConn_Open()
    '    pText = auxClass.gContenido_ChangeVars(pText, True)
    '    auxClass.Conn.gConn_Close()

    '    Dim auxGuid As Guid = Guid.NewGuid
    '    Dim auxID As String = Replace(auxGuid.ToString, "-", "") & "cod"
    '    phdnValue.Value = auxID
    '    Dim auxConn As New imClientConnection
    '    auxConn.gTextTmp_Upload(pText, auxID)
    '    pButtton.Visible = True
    '    Select Case pReadWrite
    '        Case True
    '            pButtton.Text = "Editar contenido actual"
    '            'Hay otro alto!!!
    '            pButtton.OnClientClick = "javascript:if (window.showModalDialog) {window.showModalDialog(" & Chr(39) & pForm & "?upload=" & If(pReadWrite, "1", "0") & "&tmp=1&tmpid=" & auxID & "&timestamp=" & Chr(39) & " + new Date().getTime() + " & Chr(39) & Chr(39) & ", " & Chr(39) & "" & Chr(39) & ", " & Chr(39) & "menubar=no;resizable=yes;toolbar=no;status=no;center=yes;dialogWidth=750px;dialogHeight=600px;" & Chr(39) & ");} else {window.open(" & Chr(39) & pForm & "?upload=" & If(pReadWrite, "1", "0") & "&tmp=1&tmpid=" & auxID & "" & Chr(39) & ", " & Chr(39) & "" & Chr(39) & ", " & Chr(39) & "modal=yes;directories=no;menubar=no;resizable=yes;toolbar=no;status=no;center=yes;dialogWidth=750px;dialogHeight=600px;" & Chr(39) & ");};return false;"
    '            pButtton.OnClientClick = "hrcShowWindowNoModal(""" & pForm & "?upload=" & If(pReadWrite, "1", "0") & "&tmp=1&tmpid=" & auxID & """);return false;"
    '            pButtton.PostBackUrl = ""
    '        Case Else
    '            pButtton.Text = "Ver contenido actual"
    '            pButtton.OnClientClick = "window.document.forms[0].target='_blank';"
    '            pButtton.PostBackUrl = pForm & "?upload=" & If(pReadWrite, "1", "0") & "&tmp=1&tmpid=" & auxID
    '    End Select
    'End Sub
    Private Function gData_Update() As String
        Dim auxError As String = ""
        Dim auxClientCon As New imClientConnection
        Dim auxPrincipalForm As clsHrcJSPanel = auxClientCon.gObjectTmp_Download(ViewState("principal_form"))
        Dim auxClass As New clsCusimDOC
        Dim auxMode As enumActionType = auxClass.Conn.gField_GetInt(auxPrincipalForm.BagValues.gValue_Get("mode"), -1)
        If auxMode <> enumActionType.coNew And auxMode <> enumActionType.coNewFromOther And auxMode <> enumActionType.coModify Then
            Return ""
            Exit Function
        End If

        Dim auxConn As clsHrcConnClient = auxClass.Conn
        Dim auxWfwMode_Current As enumWfwMode = -1
        Dim auxWfwMode As Object = auxPrincipalForm.BagValues.gValue_Get("wfwmode")
        If auxWfwMode IsNot Nothing Then
            auxWfwMode_Current = auxWfwMode
        End If

        Dim auxDSc0 As Object = Nothing
        Dim auxControlDsc As clsHrcJSControlBasic = auxPrincipalForm.gControl_Get("dsc0")
        If auxControlDsc IsNot Nothing Then
            auxDSc0 = auxConn.gField_GetString(auxControlDsc.gValue_Get)
            If auxDSc0.Trim = "" Then
                auxError &= "El título es obligatorio."
            End If
        End If

        If auxError <> "" Then
            Return auxError
            Exit Function
        End If

        Dim auxCache As clshrcBagValues = auxPrincipalForm.BagValues
        Dim auxCurrentWfwStpCod As enumWorkflowStep = auxPrincipalForm.BagValues.gValue_Get("wfwstpcod")
        Dim auxCod As Integer = auxPrincipalForm.BagValues.gValue_Get("cod")
        Dim auxdftgencod As String = auxPrincipalForm.BagValues.gValue_Get("dftgencod")
        Dim auxDTValuesNew As clshrcBagValues = auxCache.gValue_Get("DTVALUES_NEW")
        Dim auxDTValuesOld As clshrcBagValues = auxCache.gValue_Get("DTVALUES_OLD")
        auxDTValuesOld = New clshrcBagValues(auxDTValuesOld)
        auxDTValuesOld.gValues_Subtract(auxDTValuesNew)
        Dim auxPanelValues As clshrcBagValues = auxPrincipalForm.gFieldData_GetValues(True, True)

        Dim auxUndCod As String = Nothing
        auxClass.Conn.gConn_Open()
        'Dim auxClientConn As New imClientConnection
        'Dim auxPrincipalForm As clsHrcJSPanel = auxClientConn.gObjectTmp_Download(ViewState("principal_form"))
        Dim auxDfdGenCod As String = auxPrincipalForm.BagValues.gValue_Get("dftgencod")
        'Dim auxDfdGenCod_TRO As String = auxPrincipalForm.BagValues.gValue_Get("dftgencod_tro")

        Dim auxObjectExplorerUndcod As clshrcObjectExplorer = auxPrincipalForm.gControl_Get("especificoa")
        Dim auxDOC_DOC_UND_PermEditor As String = auxObjectExplorerUndcod.BagValues.gValue_Get("doc_doc_und_permeditor", "")
        If auxObjectExplorerUndcod IsNot Nothing Then
            If auxObjectExplorerUndcod.IsReadOnly = False Or auxDOC_DOC_UND_PermEditor <> "" Then
                For Each auxNode As clsNode In auxObjectExplorerUndcod.ItemListDeleted
                    For Each auxDocUndRow As DataRow In auxClass.Conn.gConn_Query("SELECT cod FROM DOC_DOCUND_DFT " _
                                                           & " WHERE undcod=" & auxNode.Cod _
                                                           & " AND dftdidgencod=" & auxClass.Conn.gFieldDB_GetString(auxDfdGenCod)).Rows
                        auxClass.gEntity_DOC_DOCUND_DeleteInDraft(pcod:=auxDocUndRow("cod"), pdftdidgencod:=auxDfdGenCod)
                    Next
                Next
                For Each auxNode As clsNode In auxObjectExplorerUndcod.ItemListAdded
                    auxClass.gEntity_DOC_DOCUND_InsertInDraft(pdoccod:=auxCod, pundcod:=auxNode.Cod, pdftdidgencod:=auxDfdGenCod)
                Next

                auxObjectExplorerUndcod.gItem_ClearChanges()
                If auxObjectExplorerUndcod.ItemList.Count = 0 Then
                    If auxWfwMode_Current = enumWfwMode.coStandard Then
                        auxError &= "Debe agregar por lo menos una unidad."
                    End If
                Else
                    auxUndCod = auxObjectExplorerUndcod.ItemList(0).Cod
                End If
            End If
        End If

        Dim auxTipCod As Object = Nothing
        If auxPrincipalForm IsNot Nothing Then
            Dim auxControlTipCod As clshrcJSComboBox = auxPrincipalForm.gControl_Get("tipcod")
            If auxControlTipCod IsNot Nothing Then
                auxTipCod = auxControlTipCod.gValue_Get
            End If
        End If


        Dim auxContentTypeID As Object = Nothing
        Dim auxContentMode As Integer = 0
        If auxPrincipalForm IsNot Nothing Then
            Dim auxControlContentTypeID As clshrcJSComboBox = auxPrincipalForm.gControl_Get("contentmode")
            If auxControlContentTypeID IsNot Nothing Then
                auxContentMode = Val(auxControlContentTypeID.gValue_Get)
            End If
        End If

        Dim auxSisCod As Object = Nothing
        If auxPrincipalForm IsNot Nothing Then
            Dim auxControlSisCod As clshrcJSComboBox = auxPrincipalForm.gControl_Get("siscod")
            If auxControlSisCod IsNot Nothing Then
                auxSisCod = auxControlSisCod.gValue_Get
            End If
        End If

        Dim auxClaCod As Object = Nothing
        If auxPrincipalForm IsNot Nothing Then
            Dim auxControlClaCod As clshrcJSComboBox = auxPrincipalForm.gControl_Get("clacod")
            If auxControlClaCod IsNot Nothing Then
                auxClaCod = auxControlClaCod.gValue_Get
            End If
        End If
        Dim auxProCod As Object = Nothing
        Dim auxDocCodSup As Object = Nothing
        If auxPrincipalForm IsNot Nothing Then
            Dim auxControlProCod As clshrcObjectExplorer = auxPrincipalForm.gControl_Get("procod")
            If auxControlProCod IsNot Nothing Then
                If auxControlProCod.IsReadOnly = False Then
                    auxProCod = -1
                    If auxControlProCod.ItemList.Count <> 0 Then
                        If auxControlProCod.ItemList(0).Type = enumEntities.coEntityDOC_DOC Then
                            auxDocCodSup = auxControlProCod.ItemList(0).Cod
                            auxProCod = -1
                        Else
                            auxProCod = auxControlProCod.ItemList(0).Cod
                            auxDocCodSup = -1
                        End If
                    End If
                    If auxProCod < 1 Then
                        auxProCod = -1
                    End If
                    'If auxControlProCod.ItemList.Count = 0 Then
                    'auxError &= "Debe seleccionar el proceso."
                    'End If
                End If
            End If
        End If

        If auxError <> "" Then
            Return auxError
            Exit Function
        End If

        auxClass.gTRACE_add(auxCod, 10, "Actualizando...")

        Dim auxEprCod As String = Nothing


        Dim auxLinks As New List(Of Integer)
        Dim auxCuerpo As Object
        Dim auxArchivo As Object
        Select Case auxContentMode
            Case 1
                'html
                auxCuerpo = auxPrincipalForm.BagValues.gValue_Get("contenido_html")
                If auxCuerpo <> "" Then
                    auxCuerpo = auxClientCon.gTextTmp_Download(auxCuerpo)
                    'If auxCuerpoCache IsNot Nothing Then
                    'auxCuerpo = auxCuerpoCache
                    'End If
                    'Análisis de links
                    If auxCuerpo <> "" Then
                        auxCuerpo = gDOC_Cuerpo_Correct(auxCuerpo)
                    End If
                    auxCuerpo = Replace(auxCuerpo, "http://siteurl/", "")
                    Dim auxLoc1 As Integer = 1
                    Dim auxLoc2 As Integer = 1
                    Dim auxLink As String = ""
                    Dim auxLinksList As String = "-1"
                    Do
                        auxLoc1 = InStr(auxLoc1, auxCuerpo, "#LINK_DOCUMENTO_")
                        If auxLoc1 > 0 Then
                            auxLoc1 += 16
                            auxLoc2 = InStr(auxLoc1, auxCuerpo, "_""", CompareMethod.Text)
                            If auxLoc2 = 0 Then
                                auxLoc2 = auxLoc2 = InStr(auxLoc1, auxCuerpo, """", CompareMethod.Text)
                            End If
                            If auxLoc2 > 0 Then
                                Dim auxLen As Integer = auxLoc2 - auxLoc1
                                If auxLen > 8 Then
                                    auxLen = 8 'no puede ser mayor a 8
                                End If
                                auxLink = Val(Mid(auxCuerpo, auxLoc1, auxLoc1 + auxLen))
                                auxLinks.Add(auxLink)
                                auxLinksList &= "," & Val(auxLink).ToString
                                If auxClass.Conn.gConn_Query("SELECT cod FROM DOC_DOCREF WHERE doccod=" & auxCod & " AND doccodref=" & auxLink).Rows.Count = 0 Then
                                    auxClass.gEntity_DOC_DOCREF_Insert(pdoccod:=auxCod, pdoccodref:=auxLink, prefid:="")
                                End If
                                auxLoc1 = auxLoc2 + auxLen
                            End If
                            auxLoc1 += 1
                        End If
                    Loop Until auxLoc1 = 0
                    auxClass.Conn.gConn_Delete("DELETE FROM DOC_DOCREF WHERE doccod=" & auxCod & " AND doccodref NOT IN(" & auxLinksList & ")")
                    auxContentTypeID = clsHrcConnClient.enumMimeTypes.coHTML
                End If
            Case 2
                'archivo
                Dim auxContentIDarchivo As String = auxPrincipalForm.BagValues.gValue_Get("contenido_archivo")
                Dim auxControlContenidoArchivo_FU As clshrcJSFileUpload
                auxControlContenidoArchivo_FU = auxPrincipalForm.gControl_Get("contenidoarchivo")
                Dim auxDoc_BinaryData As clsHrcConnClient.clsBinaryData = auxClientCon.gObjectTmp_Download(auxContentIDarchivo)
                If auxDoc_BinaryData IsNot Nothing Then
                    auxArchivo = auxConn.gConn_FileToBLOB(auxDoc_BinaryData.Filename, "", _
                                               auxDoc_BinaryData.Content, _
                                               "UPDATE DOC_DOC_DFT SET archivo={#FILECONTENT#} WHERE cod = " & auxCod & " AND dftdidgencod=" & auxClass.Conn.gFieldDB_GetString(auxdftgencod))
                    auxContentTypeID = auxDoc_BinaryData.MimeType
                End If

        End Select
        

        Dim auxDsc As Object = Nothing

        Dim auxDSc1 As Object = Nothing
        Dim auxDSc2 As Object = Nothing

        Dim auxControlDsc1 As clsHrcJSControlBasic = auxPrincipalForm.gControl_Get("dsc1")
        If auxControlDsc1 IsNot Nothing Then
            auxDSc1 = auxConn.gField_GetString(auxControlDsc1.gValue_Get)
            auxDsc = auxDSc0 & " " & auxDSc1
            Dim auxControlDsc2 As clsHrcJSControlBasic = auxPrincipalForm.gControl_Get("dsc2")
            If auxControlDsc2 IsNot Nothing Then
                auxDSc2 = auxConn.gField_GetString(auxControlDsc2.gValue_Get)
                If auxDSc2 <> "" Then
                    auxDsc &= " " & auxDSc2
                End If
            End If
        End If

        Dim auxObs As Object = Nothing
        Dim auxControlObs As clsHrcJSControlBasic = auxPrincipalForm.gControl_Get("obs")
        If auxControlObs IsNot Nothing Then
            auxObs = auxControlObs.gValue_Get
        End If
       
        Dim auxNro As Object = Nothing
        Dim auxDocNroEditEnabled As Boolean = auxClass.Conn.gField_GetBoolean(auxClass.gSystem_GetParameterByID(coSysParamIDDOCNroEditEnabled), False)

        Dim auxControlNro As clsHrcJSControlBasic = auxPrincipalForm.gControl_Get("nro")
        If auxControlNro IsNot Nothing Then
            auxNro = auxControlNro.gValue_Get
        End If

        Dim auxPublicacionAuto As Object = auxPanelValues.gValue_Get("publicacionauto")
        Dim auxTroCodCustomEnabled As Object
        Dim auxValue As Object
        auxValue = auxPanelValues.gValue_Get("secmode")
        If auxValue IsNot Nothing Then
            If Val(auxValue) = 1 Then
                auxTroCodCustomEnabled = False
            Else
                auxTroCodCustomEnabled = True
            End If
        End If

        'Dim auxWfwStatus As Object
        'If auxCurrentWfwStpCod = enumWorkflowStep.coWFWSTPDOC_DOCCreacion Then
        '    auxWfwStatus = enumWorkflowStep.coWFWSTPDOC_DOCCreacion
        'End If
        'actualiza el valor de cache
        auxPrincipalForm.BagValues.gValue_Add("dsc", auxDsc)

        auxClass.gEntity_DOC_DOC_Update(pcod:=auxCod, pdftdidgencod:=auxDfdGenCod, _
            ptrocodcustomenabled:=auxTroCodCustomEnabled, _
            pprocod:=auxProCod, pdocsupcod:=auxDocCodSup, psiscod:=auxSisCod, pclacod:=auxClaCod, pdoctipcod:=auxTipCod, _
            pobs:=auxObs, pdsc:=auxDsc, pdsc0:=auxDSc0, pdsc1:=auxDSc1, pdsc2:=auxDSc2, pnro:=auxNro, ppublicacionauto:=auxPublicacionAuto, _
            pcuerpo:=auxCuerpo, pundcod:=auxUndCod, peprcod:=auxEprCod, parchivo:=auxArchivo, pcontenttypeid:=auxContentTypeID, pwfwmode:=auxWfwMode)


       
        auxClass.Conn.gConn_Close()
        Return ""
    End Function
    Private Function gDOC_Cuerpo_Correct(ByVal pCuerpo As String) As String
        Dim auxReturn As String = pCuerpo
        Dim auxDoc As New HtmlAgilityPack.HtmlDocument
        Dim auxLoc1 As Integer
        'Dim auxLoc2 As Integer
        'Realizar estos cambios porque sino se trata el widget hrcmathformula como un
        'Dim auxReplaces As New SortedList(Of String, String)
        'Dim auxStringFrom As String
        'Dim auxStringTo As String
        'auxLoc1 = 1
        'Do
        '    auxLoc1 = InStr(auxLoc1, pCuerpo, "hrcmathformula")
        '    If auxLoc1 > 0 Then
        '        auxLoc2 = InStr(auxLoc1, pCuerpo, "</div>")
        '        auxStringFrom = Mid(pCuerpo, auxLoc1 + 16, auxLoc2 - auxLoc1 - 16)
        '        auxStringTo = auxStringFrom.Replace("<", "&lt;")
        '        auxReplaces.Add(auxStringFrom, auxStringTo)
        '        auxLoc1 = auxLoc2 + 4
        '    End If
        'Loop Until auxLoc1 = 0
        'For Each auxvalueString As KeyValuePair(Of String, String) In auxReplaces
        '    pCuerpo = Replace(pCuerpo, auxvalueString.Key, auxvalueString.Value)
        'Next
        auxDoc.OptionReadEncoding = False
        auxDoc.OptionUseIdAttribute = False
        auxDoc.OptionCheckSyntax = False
        auxDoc.LoadHtml(pCuerpo)

        Dim auxHTMLCollection As HtmlAgilityPack.HtmlNodeCollection
        Dim auxStackNode As New Stack(Of String)
        auxStackNode.Push("//span")
        Dim auxID As String
        Dim auxName As String
        Dim auxValue As String
        Dim auxStyleStr As String
        Dim auxElementName As String
        Do While auxStackNode.Count <> 0
            auxElementName = auxStackNode.Pop
            auxHTMLCollection = auxDoc.DocumentNode.SelectNodes(auxElementName)
            If auxHTMLCollection IsNot Nothing Then
                For Each auxSpan As HtmlAgilityPack.HtmlNode In auxHTMLCollection
                    auxID = auxSpan.GetAttributeValue("id", "")
                    If auxElementName = "//span" And (Left(auxID, 7).ToLower = "cke_bm_" Or auxID = "") Then
                        'Quitar los span vacíos.
                        'Es un error de ckeditor http://dev.ckeditor.com/ticket/8232
                        auxStyleStr = ""
                        Dim auxStyles As String = auxSpan.GetAttributeValue("style", "")
                        For Each auxStyle As String In Split(auxStyles, ";")
                            auxLoc1 = InStr(auxStyle, ":")
                            If auxLoc1 <> 0 Then
                                auxName = Left(auxStyle, auxLoc1 - 1).Trim.ToLower
                                auxValue = Right(auxStyle, auxStyle.Length - auxLoc1)
                                Select Case auxName
                                    Case "display"
                                        If auxValue.ToLower.Trim = "none" Then
                                            auxValue = ""
                                        End If
                                End Select
                                If auxValue <> "" Then
                                    auxStyleStr &= auxName & ":" & auxValue & ";"
                                End If
                            End If
                        Next
                        auxSpan.SetAttributeValue("style", auxStyleStr)
                    End If
                Next
            End If
        Loop
        auxReturn = auxDoc.DocumentNode.OuterHtml
        Return auxReturn
    End Function
    'Private Function gValue_GetList(ByVal pValue As String) As List(Of String())
    '    Dim auxItems() As String = Split(pValue, "{#CHR13#}")
    '    Dim auxReturn As New List(Of String())

    '    For auxI As Integer = 0 To UBound(auxItems) - 1
    '        Dim auxCount As Short = 0
    '        Dim auxRowItems() As String = Split(auxItems(auxI), "{#CHR34#}")
    '        Dim auxItem(3) As String
    '        For Each auxString As String In auxRowItems
    '            Select Case auxCount
    '                Case 0, 1, 2, 3
    '                    auxItem(auxCount) = auxString
    '                    If UBound(auxRowItems) = auxCount Then
    '                        auxReturn.Add(auxItem)
    '                    End If
    '            End Select
    '            auxCount += 1
    '        Next
    '    Next
    '    Return auxReturn
    'End Function
    
    'Protected Sub Refresh_Click(ByVal sender As Object, ByVal e As System.EventArgs)
    '    Server.Transfer(Request.RawUrl, False)
    'End Sub
    
    'Protected Sub cmdFormViewItemCancel_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdFormViewItemCancel.Click
    '    Dim auxConn As clsHrcConnClient = Session("conn")
    '    Dim auxClient As New imClientConnection
    '    Dim auxPrincipalForm As clsHrcJSPanel = auxClient.gObjectTmp_Download(ViewState("principal_form"))
    '    If auxPrincipalForm IsNot Nothing Then

    '        Dim auxBagValues As clshrcBagValues = auxPrincipalForm.BagValues
    '        Dim auxdftgencod As String = auxPrincipalForm.BagValues.gValue_Get("dftgencod")
    '        auxClient.gObjectTmp_Delete(ViewState("principal_form")) ' .Value)
    '        If auxdftgencod <> "" Then
    '            Dim auxClass As New clsCusimDOC
    '            auxClass.Conn.gConn_Open()
    '            auxClass.gDraft_Delete(auxdftgencod)
    '            auxClass.Conn.gConn_Close()
    '        End If
    '    End If
    '    If Request.QueryString("_closea_") = "1" Then
    '        ScriptManager.RegisterStartupScript(Me, Me.GetType(), "CloseWindow", "self.close();", True)
    '    Else
    '        Dim auxURl As String = "cfrmdocumentos.aspx?_mode_=7&_closea_=0"
    '        For Each auxItem As String In Request.QueryString.Keys
    '            Select Case auxItem
    '                Case "del", "search"
    '                    auxURl &= "&" & auxItem & "=" & Request.QueryString(auxItem)
    '                Case Else

    '            End Select
    '        Next
    '        Response.Redirect(auxURl)

    '    End If
    'End Sub

End Class

