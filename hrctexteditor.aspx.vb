'Version 2011-03-30
Imports Intelimedia.imComponentes
Partial Class hrctexteditor
    Inherits System.Web.UI.Page
    Implements System.Web.UI.ICallbackEventHandler
    'Callbacks
    Dim _callbackResult As String = Nothing
    Public Function GetCallbackResult() As String Implements System.Web.UI.ICallbackEventHandler.GetCallbackResult
        Return _callbackResult
    End Function
    Public Sub RaiseCallbackEvent(ByVal eventArgument As String) Implements System.Web.UI.ICallbackEventHandler.RaiseCallbackEvent
        Dim auxText As String = ""
        Dim auxTipo As Integer = Val(Request.QueryString("upload"))
        Select Case auxTipo
            Case 1
                'auxText = Server.HtmlDecode(eventArgument)
                auxText = eventArgument
                Dim auxConn As New imClientConnection
                Dim auxTmpID As String = Request.QueryString("tmpid")
                Dim auxClient As New imClientConnection
                Dim auxControl As Object
                auxControl = auxClient.gObjectTmp_Download(auxTmpID)
                If auxControl IsNot Nothing Then
                    If InStr(auxControl.ToString, "JSTextEditor", CompareMethod.Text) <> 0 Then
                        auxControl.BagValues.gValue_Add("CONTENT", auxText)
                    Else
                        '        auxText = Server.HtmlDecode(eventArgument)
                        auxConn.gTextTmp_Upload(auxText, auxTmpID)
                    End If
                End If
            Case 4
                Dim auxTmpID As String = Request.QueryString("tmpid")
                Dim auxClient As New imClientConnection
                Dim auxControl As Object
                auxControl = auxClient.gObjectTmp_Download(auxTmpID)
                If auxControl IsNot Nothing Then
                    auxText = Server.HtmlDecode(eventArgument)
                    If InStr(auxControl.ToString, "JSTextEditor", CompareMethod.Text) <> 0 Then
                        auxControl.BagValues.gValue_Add("CONTENT", auxText)
                        Dim auxValues As New clshrcBagValues
                        auxValues.gValue_Add("ACTION", "hrctexteditor_save")
                        auxControl.gRaise_Event(auxValues)
                        'Dim auxBinaryCod As Integer = Val(auxControl.gBagValues_Get("ID"))
                        'Dim auxConn As clsHrcConnClient = Session("conn")
                        'auxConn = auxConn.gComponent_CreateInstance
                        'auxConn.gConn_Open()
                        'Dim auxBinaryData As New clsHrcConnClient.clsBinaryData(clsHrcConnClient.enumMimeTypes.coHTML, _
                        '            auxControl.title, Encoding.Default.GetBytes(auxText))
                        'auxConn.gConn_BinaryDataToBLOB(auxBinaryData, auxBinaryCod, Nothing, False)
                        'auxConn.gConn_Close()
                    Else
                        auxText = Server.HtmlDecode(eventArgument)
                        auxClient.gTextTmp_Upload(auxText, auxTmpID)
                    End If

                End If

        End Select

        _callbackResult = ""
    End Sub
    'Callbacks - end
    Friend m_Toolbar As String = ""
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        fmebody.Attributes.Add("onload", "toggleAlert();")
        Try
            If Not IsPostBack Then
                lblTexto.InnerText = "Showing..."
                Dim auxTmpID As String = Request.QueryString("tmpid")
                Dim auxClient As New imClientConnection
                Dim auxControl As Object

                Dim auxpanels_EnabledCode As String = ""
                Dim auxPanels_Enabled As String = ""
                'If auxValuesBagID <> "" Then
                '    'Dim auxValuesBag As clshrcBagValues
                '    Dim auxValuesBag As SortedList(Of String, Object)
                '    auxValuesBag = auxClient.gObjectTmp_Download(auxValuesBagID)
                '    If auxValuesBag IsNot Nothing Then
                '        auxPanels_Enabled = auxValuesBag("Panels_Enabled")
                '    End If
                'Else
                Dim auxTipo As Short = Val(Request.QueryString("upload"))
                Dim auxiSForm As Boolean = False
                Dim auxBinaryCod As Integer = -1
                Dim auxContent As String = Nothing
                If auxTmpID <> "" Then
                    auxControl = auxClient.gObjectTmp_Download(auxTmpID)
                    If auxControl IsNot Nothing Then
                        '  If auxControl.isForm And auxWriteMode = False Then
                        'auxTipo = 4
                        'End If
                        If InStr(auxControl.ToString, "JSTextEditor", CompareMethod.Text) <> 0 Then
                            auxiSForm = auxControl.isForm
                            auxBinaryCod = Val(auxControl.BagValues.gValue_Get("ID"))
                            auxContent = auxControl.BagValues.gValue_Get("CONTENT")
                            If auxContent Is Nothing And auxBinaryCod > 0 Then
                                Dim auxConn As clsHrcConnClient = Session("conn")
                                auxConn = auxConn.gComponent_CreateInstance
                                auxConn.gConn_Open()
                                Dim auxBinaryData As clsHrcConnClient.clsBinaryData = auxConn.gConn_BLOBToBinaryData(auxBinaryCod, False)
                                auxConn.gConn_Close()
                                auxContent = Encoding.Default.GetString(auxBinaryData.Content)
                                auxControl.BagValues.gValue_Add("CONTENT", auxContent)
                                auxControl.TITLE = auxBinaryData.Filename
                            End If
                            auxPanels_Enabled = auxControl.BagValues.gValue_Get("Panels_Enabled")
                        Else
                            auxContent = auxControl.ToString
                        End If
                    Else

                    End If
                End If

                Select Case auxTipo
                    '1=Editor con Texto enriquecido.Y Save, graba los cambios.
                    '2=Imagen previa con botón EDITAR
                    '3=Muestra HTML desde ConnBinaries
                    '4=Muestra HTML con Tag INPUT habilitados. Y Save, graba los cambios.
                    '0=Muestra HTML de solo lectura
                    '5=muestra HTML como PDF
                    Case 1, 4
                        If Request.QueryString("toolbarform") = "1" Then
                            'm_Toolbar = "['Form', 'Checkbox', 'Radio', 'TextField', 'Textarea', 'Select', 'Button', 'ImageButton', 'HiddenField'],"
                            m_Toolbar = "['Form', 'Checkbox', 'Radio', 'TextField', 'Textarea', 'Select'],"
                        End If
                        lblPreview.Visible = False
                        Dim auxConn As New imClientConnection
                        Dim auxControlName As String = ""
                        Select Case auxTipo
                            Case 4
                                auxControlName = "lblTexto.outerHTML"    'lblTexto.ClientID & ".innerHTML"
                            Case Else
                                auxControlName = "CKEDITOR.instances.txtTexto.getData()"

                        End Select
                        cmdPreview.Visible = False
                        If auxTipo = 1 Then
                            lblTexto.Visible = False
                            txtTexto.Visible = True
                            txtTexto.Text = auxContent ' auxConn.gTextTmp_Download(auxTmpID)
                            'Preview
                            Dim auxPreview_ScriptCallback As String = ""
                            auxPreview_ScriptCallback &= "function gData_Preview_Callbackget(nothing, context) {" _
                                                                    & "hrcShowModal('" & ResolveUrl("hrctexteditor.aspx") & "?upload=0&toolbarform=" & Request.QueryString("toolbarform") & "&tmpid=" & auxTmpID & "');" _
                                                                    & "}"
                            Page.ClientScript.RegisterClientScriptBlock(Me.GetType, "hrcDataPreview", auxPreview_ScriptCallback, True)
                            Dim cbPreview_Reference As String = Page.ClientScript.GetCallbackEventReference(Me, "arg", "gData_Preview_Callbackget", "context")
                            Dim cbPreview_Script As String = "function gData_Preview_CallbackUse(arg,context)" & _
                                      "{" & cbPreview_Reference & ";" & "}"
                            Page.ClientScript.RegisterClientScriptBlock(Me.GetType(), "gData_Preview_CallbackUse", cbPreview_Script, True)

                            cmdPreview.Visible = True
                            cmdPreview.OnClientClick = "gData_Preview_CallbackUse(" & auxControlName & ");" _
                                    & "return false;"

                            'VerPDF
                            Dim auxPDF_ScriptCallback As String = ""
                            auxPDF_ScriptCallback &= "function gData_PDF_Callbackget(nothing, context) {" _
                                                                    & "hrcShowWindowNoModal('" & ResolveUrl("hrctexteditor.aspx") & "?upload=5&toolbarform=" & Request.QueryString("toolbarform") & "&tmpid=" & auxTmpID & "');" _
                                                                    & "}"
                            Page.ClientScript.RegisterClientScriptBlock(Me.GetType, "hrcDataPDF", auxPDF_ScriptCallback, True)
                            Dim cbPDF_Reference As String = Page.ClientScript.GetCallbackEventReference(Me, "arg", "gData_PDF_Callbackget", "context")
                            Dim cbPDF_Script As String = "function gData_PDF_CallbackUse(arg,context)" & _
                                      "{" & cbPDF_Reference & ";" & "}"
                            Page.ClientScript.RegisterClientScriptBlock(Me.GetType(), "gData_PDF_CallbackUse", cbPDF_Script, True)

                            cmdVerPDF.Visible = True
                            cmdVerPDF.OnClientClick = "gData_PDF_CallbackUse(" & auxControlName & ");" _
                                    & "return false;"

                            'cmdVerPDF.OnClientClick = "hrcShowWindowNoModal(""cfrmdocumentos_impresion.aspx?_closea_=1&_mode_=9&param1=" & m_Cod & """);return false;"
                        Else
                            lblTexto.Visible = True
                            txtTexto.Visible = False
                            lblTexto.InnerHtml = auxConn.gTextTmp_Download(auxTmpID)
                            'Page.ClientScript.RegisterClientScriptBlock(Me.GetType, "aa", "document.getElementById('lblTexto').value = '<br /> " & auxConn.gTextTmp_Download(auxTmpID) & "'", False)
                            '
                        End If
                        Dim auxScriptCallback As String = "" ' = "function GetDate() { gData_Save();}"
                        auxScriptCallback &= "function gData_Save_Callbackget(nothing, context) {window.close();}"
                        Page.ClientScript.RegisterClientScriptBlock(Me.GetType, "hrcDataSave", auxScriptCallback, True)
                        Dim cbReference As String = Page.ClientScript.GetCallbackEventReference(Me, "arg", "gData_Save_Callbackget", "context")
                        Dim cbScript As String = "function gData_Save_CallbackUse(arg,context)" & _
                                  "{" & cbReference & ";" & "}"
                        Page.ClientScript.RegisterClientScriptBlock(Me.GetType(), "gData_Save_CallbackUse", cbScript, True)

                        cmdSave.OnClientClick = "gData_Save_CallbackUse(" & auxControlName & ");return false;"
                        cmdSave.Visible = True
                        cmdEdit.Visible = False

                    Case 2    ' Muestra boton Editar e imagen previa
                        lblPreview.Visible = True
                        cmdEdit.Visible = True
                        'cmdEdit.OnClientClick = "hrcShowWindowNoModal('" & ResolveUrl("hrctexteditor.aspx") & "?upload=1&toolbarform=" & Request.QueryString("toolbarform") & "&tmpid=" & auxTmpID & "');"
                        cmdEdit.OnClientClick = "hrcShowWindowNoModal('" & ResolveUrl("hrctexteditor.aspx") & "?upload="
                        If auxiSForm Then
                            cmdEdit.OnClientClick &= "4"
                        Else
                            cmdEdit.OnClientClick &= "1"
                        End If
                        cmdEdit.OnClientClick &= "&toolbarform=" & Request.QueryString("toolbarform") & "&tmpid=" & auxTmpID & "');"
                        cmdSave.Visible = False
                        lblTexto.Visible = True
                        txtTexto.Visible = False
                        If auxControl IsNot Nothing Then
                            If auxContent Is Nothing Then
                                lblTexto.InnerHtml = "<div id=""fmelectura"" style=""border-width:1px; border-style:solid; border-color:Gray"" >" _
                                & "" & "</div>"
                            Else
                                lblTexto.InnerHtml = "Vista previa<div id=""fmelectura"" style=""border-width:1px; border-style:solid; border-color:Gray"" >" _
                                & Left(auxContent, 300) & "</div>"
                            End If
                        End If
                    Case "3"    'Muestra HTML desde conbinaries
                        Dim auxConn As New imClientConnection
                        auxConn.DownloadMode = imClientConnection.enumDownloadMode.coInline
                        auxConn.gFile_Download(CInt(Val(Request.QueryString("id"))), CBool(If(Request.QueryString("prv") = "1", True, False)))
                        txtTexto.Visible = False
                        cmdSave.Visible = False
                        cmdEdit.Visible = False
                        lblPreview.Visible = False
                    Case "5"
                        'Dim auxSesionID1 As String = AUXSec.gLogin_CreateDelegatedSessionToSystem
                        'Dim auxSesionID2 As String = auxClass.Sec.gLogin_CreateDelegatedSessionToSystem
                        Dim auxUserName As String = ""
                        Dim auxPassword As String = ""
                        Dim auxProcessUserName As String = ""
                        Dim auxProcessPassword As String = ""
                        If ConfigurationManager.AppSettings("adusuario") IsNot Nothing Then
                            auxProcessUserName = ConfigurationManager.AppSettings("adusuario").ToString
                        End If
                        If ConfigurationManager.AppSettings("addominio") IsNot Nothing Then
                            auxProcessUserName = ConfigurationManager.AppSettings("addominio") & "\" & auxUserName
                        End If
                        If ConfigurationManager.AppSettings("adpassword") IsNot Nothing Then
                            auxProcessPassword = ConfigurationManager.AppSettings("adpassword").ToString
                        End If
                        Dim auxMarcaDatos As String = ""
                        If ConfigurationManager.AppSettings("pdfusuario") IsNot Nothing Then
                            auxUserName = ConfigurationManager.AppSettings("pdfusuario").ToString
                        End If
                        If ConfigurationManager.AppSettings("pdfpassword") IsNot Nothing Then
                            auxPassword = ConfigurationManager.AppSettings("pdfpassword").ToString
                        End If

                        Dim auxTemporalFolder As String = ""
                        If ConfigurationManager.AppSettings("Tempfolder") IsNot Nothing Then
                            Try
                                auxTemporalFolder = Server.MapPath(ConfigurationManager.AppSettings("Tempfolder"))
                            Catch ex As Exception

                            End Try
                        End If

                        Dim auxConnection As New imClientConnection
                        Dim auxReturn As String = ""
                        Dim auxSecurity As clsHrcSecurityClient = Session("security")
                        Dim auxConn As clsHrcConnClient = Session("conn")
                        Dim auxSesionID1 As String = auxSecurity.gLogin_CreateDelegatedSessionToSystem
                        Dim auxFileName As String = "pdf" & Now.ToString("yyyyMMddHHmmss") & ".pdf"
                        Dim auxText As String = auxConnection.gTextTmp_Download(auxTmpID)
                        Dim auxObjectGlobalID As String = auxConn.gField_GetUniqueID

                        auxConnection.gObjectTmp_UploadinGlobal(auxText, auxObjectGlobalID)
                        Dim auxURL As String = Request.Url.Scheme & "://" & Request.Url.Authority & Request.Url.LocalPath & "?upload=0&toolbarform=" & Request.QueryString("toolbarform") & "&tmpidg=" & auxObjectGlobalID & "&_sesid_=" & auxSesionID1
                        Dim auxBinary As clsHrcConnClient.clsBinaryData = auxConnection.gFile_ToPDFBinary(auxFileName, auxURL, _
                                                             "", "", "", "", "", "", auxUserName, auxPassword, auxTemporalFolder, imClientConnection.enumPaperKind.coA4, auxProcessUserName, auxProcessPassword, -1, True, True, 0)
                        auxConnection.gFile_Download(auxBinary)
                        txtTexto.Visible = False
                        cmdSave.Visible = False
                        cmdEdit.Visible = False
                        lblPreview.Visible = False
                        lblTexto.InnerText = auxURL
                        lblTexto.Visible = True
                    Case Else
                        lblPreview.Visible = False
                        cmdEdit.Visible = False
                        cmdSave.Visible = False
                        lblTexto.Visible = True
                        txtTexto.Visible = False
                        Dim auxConn As New imClientConnection
                        Dim auxText As String = ""
                        If Request.QueryString("tmpidg") <> "" Then
                            auxText = auxConn.gObjectTmp_DownloadFromGlobal(Request.QueryString("tmpidg"))
                            auxConn.gObjectTmp_DeleteFromGlobal(Request.QueryString("tmpidg"))
                            fmebody.Attributes.Remove("style")
                        Else
                            auxText = auxConn.gTextTmp_Download(auxTmpID)
                        End If


                        auxText = Replace(auxText, "#SITEURL_", VirtualPathUtility.GetDirectory(Request.Path))
                        auxText = Replace(auxText, "http://siteurl/", VirtualPathUtility.GetDirectory(Request.Path))
                        lblTexto.InnerHtml = auxText
                End Select
            End If
            If txtTexto.Visible Then
                ''Mathjax',
                Dim auxMathajax As String = ",'Mathjax'"
                auxMathajax = ""
                Dim auxExtraPlugins As String = ",mathjax"
                auxExtraPlugins = ""
                Dim auxScript As String = "CKEDITOR.replace('" & txtTexto.ClientID & "', { baseHref: '" & ResolveUrl("~/plugins/ckeditor/") & "', height: 400, toolbar:" _
                  & "[" _
                  & "['Cut', 'Copy', 'Paste', 'PasteText', 'PasteFromWord', '-', 'SelectAll', 'RemoveFormat'], ['Styles', 'Format']," _
                  & "['Font', 'FontSize', 'Bold', 'Italic', 'Underline', 'Strike', '-', 'Subscript', 'Superscript']," _
               & "['NumberedList', 'BulletedList', '-', 'Outdent', 'Indent', 'Blockquote']," _
               & "['JustifyLeft', 'JustifyCenter', 'JustifyRight', 'JustifyBlock']," & m_Toolbar _
               & "['Link', 'Unlink', 'Anchor']," _
               & "['Image', 'Table', 'HorizontalRule', 'SpecialChar','hrcpasteformula'" & auxMathajax & ",'PageBreak'], ['TextColor', 'BGColor'], ['Undo', 'Redo', '-', 'Find', 'Replace'], [" _
               & "'Templates', '-', 'ShowBlocks', 'Source', 'SpellCheck', '-', 'About']," _
               & "], extraPlugins: 'colorbutton,font,pastebase64,justify,pagebreak,lineutils" & auxExtraPlugins & ",hrcpasteformula'" _
                 & "});" _
                 & "CKEDITOR.on('dialogDefinition', function(ev) {" _
                     & "if (ev.data.name == 'image') {" _
                         & "var btn = ev.data.definition.getContents('info').get('browse');" _
                         & "btn.hidden = false;" _
                         & "btn.onClick = function() { window.open(CKEDITOR.basePath + 'ImageBrowser.aspx', 'popuppage', 'scrollbars=no,width=780,height=630,left=' + ((screen.width - 780) / 2) + ',top=' + ((screen.height - 630) / 2) + ',resizable=no,toolbar=no,titlebar=no'); };" _
                     & "}" _
                     & "if (ev.data.name == 'link') {" _
                         & "var btn = ev.data.definition.getContents('info').get('browse');" _
                         & "btn.hidden = false;" _
                         & "btn.onClick = function() { window.open(CKEDITOR.basePath + 'LinkBrowser.aspx', 'popuppage', 'scrollbars=no,width=780,height=630,left=' + ((screen.width - 780) / 2) + ',top=' + ((screen.height - 630) / 2) + ',resizable=no,toolbar=no,titlebar=no'); };" _
                     & "}" _
                 & "});"
                'auxScript  &= "CKEDITOR.mathJaxClass ='" & ResolveUrl("~/plugins/ckeditor/") & "/plugins/mathjax/lib/MathJax.js';"
                Page.ClientScript.RegisterClientScriptInclude("hrcTextCKEditor", ResolveUrl("~/plugins/ckeditor/ckeditor.js"))
                'Page.ClientScript.RegisterClientScriptInclude("hrcTextCKEditorNetSpell", ResolveUrl("~/plugins/ckeditor/plugins/netspell/spell.js"))
                Page.ClientScript.RegisterStartupScript(Me.GetType, "hrcTextEditor", auxScript, True)
            End If
            '            If Request.QueryString("upload") = "2" Then
            '                Dim auxConn As New imClientConnection
            '                lblTexto.InnerHtml = "<div id=""fmelectura"" style=""border-width:1px; border-style:solid; border-color:Gray"" >" & Left(auxConn.gTextTmp_Download(auxTmpID), 300) & "</div>"
            '                'lblTexto.Text 
            '            End If
        Catch ex As Exception
            Dim a As String = ex.Message
            lblTexto.InnerText = "Exception" & a
        End Try
    End Sub
    Private Sub gForm_Close()
        'If Request.QueryString("_closea_") = "1" Then
        ScriptManager.RegisterStartupScript(Me, Me.GetType(), "CloseWindow", "self.close();", True)
        'End If
    End Sub
    'Protected Sub cmdSave_Click(ByVal sender As Object, ByVal e As System.EventArgs)
    '    Dim auxText As String = ""
    '    If Request.QueryString("upload") = "1" Then
    '        auxText = txtTexto.Text
    '    Else
    '        auxText = lblTexto.InnerHtml   ' lblTexto.Text
    '    End If
    '    Dim auxConn As New imClientConnection
    '    Dim auxTmpID As String = Request.QueryString("tmpid")
    '    auxConn.gTextTmp_Upload(auxText, auxTmpID)
    '    gForm_Close()
    'End Sub
End Class
