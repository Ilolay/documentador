Imports System.Data
Imports Intelimedia.imComponentes
Imports Intelimedia.inTasks
Imports clsCusimDOC
Partial Class cfrmdocumentos_impresion
    Inherits System.Web.UI.Page
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim auxClass As New clsCusimDOC
        auxClass.Conn.gConn_Open()
        Try
            'auxClass.Conn.ConnShared = False
            Dim m_IsAdmin As Boolean = Session("isadmin")
            lblTexto.Text = ""
            Dim auxMode As Short = Val(Request.QueryString("_mode_"))
            '0=cuerpo DOC_DOC
            '1=cabecera
            '2=foot
            '3=original
            '4=PDF copia no controlada doc_docvig
            '5=PDF copia controlada doc_docvig
            '6=cuerpo DOC_DOCVIG
            '7=Version online doc_doc o doc_doc_hst  . 
            'param1=cod
            'param2=hstgencod
            '8=Version online vigente
            '9=PDF no controlada doc_doc
            Dim auxEsCopia As Boolean = False
            Dim auxCopyMode As Short = 0
            Dim auxHstGenCod As Integer = -1
            Dim auxCopiaTexto As String = " " 'pARA REEMPLAZAR POR UN ESPACIO
            Dim auxCod As String = -1
            If Request.QueryString("param1") IsNot Nothing Then
                auxCod = Request.QueryString("param1")
            ElseIf Request.QueryString("param2") IsNot Nothing Then
                auxHstGenCod = Request.QueryString("param2")
            End If
            If Request.QueryString("_copytype_") IsNot Nothing Then
                auxCopyMode = Val(Request.QueryString("_copytype_").ToString)
                Select Case auxCopyMode
                    Case 1  'Copia comun
                        auxEsCopia = True
                        auxCopiaTexto = "<span style=""color:red;font-size:8px"">* COPIA SIN VALOR</span>"
                    Case 2 'copia no controlada
                        auxEsCopia = False
                        auxCopiaTexto = "<span style=""color:red;font-size:8px"">* COPIA NO CONTROLADA</span>"
                End Select
                
            End If
            If auxMode = 7 Or auxMode = 9 Then  'Or auxMode = 4 
                auxEsCopia = True
            ElseIf auxMode = 6 Then
                auxEsCopia = False
            End If

            Dim auxWhere As String = ""
            Dim auxDOCTable As String = "DOC_DOC"
            Dim auxFields As String = "" ' "DOC_DOC.copianro,"
            Dim auxEmpCod As Integer = Session("empcod")
            If auxEsCopia Then
                If auxHstGenCod > 0 Then
                    auxDOCTable = "DOC_DOC_HST AS DOC_DOC"
                    auxFields &= " ,(SELECT TOP 1 wfwstpdsc FROM Q_WFWSTP WHERE Q_WFWSTP.wfwstpcod = DOC_DOC.wfwstatus) as wfwstpdsc  "
                    auxWhere &= " AND (DOC_DOC.baja {#ISNULL#} OR DOC_DOC.baja=0)" _
                        & " AND DOC_DOC.hsthidgencod = " & auxHstGenCod
                Else
                    auxWhere &= " AND (DOC_DOC.baja {#ISNULL#} OR DOC_DOC.baja=0)" _
                   & " AND DOC_DOC.cod=" & auxCod
                    auxFields &= " ,(SELECT TOP 1 wfwstpdsc FROM Q_WFWSTP WHERE Q_WFWSTP.wfwstpcod = DOC_DOC.wfwstatus) as wfwstpdsc  "
                End If
            Else
                auxWhere = " AND DOC_DOC.cod=" & auxCod
                auxDOCTable = "DOC_DOCVIG AS DOC_DOC"
                auxFields &= " ,(SELECT TOP 1 wfwstpdsc FROM Q_WFWSTP WHERE Q_WFWSTP.wfwstpcod = " & auxClass.enumWorkflowStep.coWFWSTPDOC_DOCDocumentovigente & ") as wfwstpdsc  "
            End If

                Dim auxEspecificoA As String = ""
                Dim auxConn As clsHrcConnClient = auxClass.Conn
                auxConn.gConn_Open()

            'auxClass.gTRACE_add(auxCod, 10, "Call:" & Request.QueryString.ToString)
                Dim auxDT As DataTable
            auxDT = auxConn.gConn_Query("SELECT DOC_DOC.cod,DOC_DOC.qsidcod,DOC_DOC.version,DOC_DOC.dsc,DOC_DOC.dsc0,DOC_DOC.dsc1,DOC_DOC.dsc2,DOC_DOC.cuerpo,DOC_DOC.identificador,DOC_DOC.version,DOC_DOC.fecha,DOC_DOC.undcod,DOC_DOC.contenttypeid,DOC_DOC.archivo" _
                     & ",DOC_DOCTIP.dsc AS wildoctipdsc,DOC_DOCTIP.templatehead,DOC_SIS.dsc as sisdsc" _
                         & ",DOC_APA.dsc as apadsc,DOC_CLA.notitle,DOC_CLA.dsc as cladsc" _
                         & ",DOC_DOCTIP.dsc as doctipdsc,DOC_DOCTIP.templatefootcustom as doctip_templatefootcustom,DOC_DOCTIP.templatefootleft as doctip_templatefootleft,DOC_DOCTIP.templatefootcenter as doctip_templatefootcenter,DOC_DOCTIP.templatefootright as doctip_templatefootright" _
                         & ",UND.dsc as unddsc,'' as proproponedsc" _
                         & ",DOC_DOC.wfwstatus as wfwstpcod" _
                         & ",'' as proproponeunddsc" _
                         & auxFields _
                         & ",DOC_DOC.eprcod as eprcod" _
                         & ",DOC_DOC.prncfgcod,DOC_PRNCFG.font8px,DOC_PRNCFG.font9px,DOC_PRNCFG.font10px,DOC_PRNCFG.font11px,DOC_PRNCFG.font12px,DOC_PRNCFG.font14px,DOC_PRNCFG.font16px,DOC_PRNCFG.font18px,DOC_PRNCFG.font20px,DOC_PRNCFG.font22px,DOC_PRNCFG.font24px,DOC_PRNCFG.font26px,DOC_PRNCFG.font28px,DOC_PRNCFG.font36px,DOC_PRNCFG.font48px,DOC_PRNCFG.font72px" _
                         & " FROM " & auxDOCTable _
                         & " LEFT JOIN DOC_SIS ON DOC_DOC.siscod = DOC_SIS.cod " _
                         & " LEFT JOIN DOC_PRO ON DOC_DOC.procod = DOC_PRO.cod " _
                         & " LEFT JOIN DOC_APA ON DOC_PRO.apacod = DOC_APA.cod " _
                         & " LEFT JOIN DOC_CLA ON DOC_DOC.clacod = DOC_CLA.cod " _
                         & " LEFT JOIN DOC_DOCTIP ON DOC_DOC.doctipcod = DOC_DOCTIP.cod " _
                         & " LEFT JOIN UND ON DOC_DOC.undcod = UND.cod " _
                         & " LEFT JOIN DOC_PRNCFG ON DOC_DOC.prncfgcod = DOC_PRNCFG.cod " _
                     & " WHERE DOC_DOC.cod >0 " _
                     & auxWhere)
                If auxDT.Rows.Count = 0 Then
                    Response.Write("No se encontraron datos")
                    auxClass.gTRACE_add(auxCod, 1, "No se encontraron datos:" & auxConn.LastErrorDescription & "." & auxConn.LastCommand)
                    Exit Sub
                Else
                    auxCod = auxDT.Rows(0)("cod")
                End If

                Dim auxPC As String = ""
                Try
                    'auxPC = System.Net.Dns.GetHostEntry(HttpContext.Current.Request.UserHostName).HostName()
                    auxPC = System.Net.Dns.GetHostByAddress(HttpContext.Current.Request.ServerVariables("REMOTE_HOST")).HostName
                Catch ex As Exception
                    auxPC = HttpContext.Current.Request.ServerVariables("REMOTE_HOST")
                End Try
                Dim auxUNDRow As DataRow = hrcEntityDT_UND_FindByKey(auxConn.gField_GetInt(auxDT.Rows(0)("undcod"), -1))
                If auxUNDRow IsNot Nothing Then
                    auxEspecificoA = auxUNDRow("dsc").ToString
                End If


                Dim auxImpresion As String = ""
                Dim auxSidCod As Integer = auxClass.Conn.gField_GetInt(auxDT.Rows(0)("qsidcod"))

                Dim auxClaDsc As String = auxClass.Conn.gField_GetString(auxDT.Rows(0)("cladsc"), "")
                If auxClass.Conn.gField_GetBoolean(auxDT.Rows(0)("notitle")) Then
                    auxClaDsc = ""
                End If
                Dim auxPrnCfgCod As Integer = auxClass.Conn.gField_GetInt(auxDT.Rows(0)("prncfgcod"), 1)
                Dim auxFooterCenter As String = "Impreso por " & auxClass.Sec.CurrentSecDsc & " el " & auxConn.gDate_GetNow.ToString("d/M/yyyy HH:mm")
                Dim auxFooterRight As String = "Pag.[page]/[toPage]"
                Dim auxFooterLeft As String = ""
                auxFooterLeft = "Estado:" & auxDT.Rows(0)("wfwstpdsc")
                Dim auxFechaStr As String = ""
                Dim auxFecha As DateTime = auxClass.Conn.gField_GetDate(auxDT.Rows(0)("fecha"))
                If auxEsCopia Then
                    auxFecha = auxConn.gDate_GetNow
                    auxFechaStr = "--/--/----"
                Else
                    If auxFecha <> Nothing Then
                        auxFechaStr = auxFecha.ToString("d/M/yyyy")
                    End If
                End If

                Dim auxEprCod As Integer = auxClass.Conn.gField_GetInt(auxDT.Rows(0)("eprcod"), 1)
                If auxEprCod < 0 Then
                    auxEprCod = 1
                End If
                Page.Title = "Documento:" & auxDT.Rows(0)("dsc") & "-" & auxConn.gDate_GetNow.ToString("d/M/yyyy")
                Dim auxFileName As String = Replace(auxDT.Rows(0)("dsc"), " ", "") & "_v" & auxConn.gField_GetInt(auxDT.Rows(0)("version"), 0) & "_" & auxConn.gDate_GetNow.ToString("ddMMyyyy") & ".pdf"

                Dim auxAprobadoPor As String = ""
                Dim auxAprobadoPorFecha As String = ""

                If auxEsCopia Then ' Or auxMode = 7 Or auxMode = 9 Then
                    auxAprobadoPor = "-------"
                    auxAprobadoPorFecha = "--/--/----"
                Else
                    Dim auxCodUltAprobacionDT As DataTable = auxConn.gConn_Query("SELECT TOP 1 cod,wfwstepnext " _
                                & " FROM DOC_DOCLOG " _
                                & " WHERE DOC_DOCLOG.doccod=" & auxCod _
                                & " AND wfwstepnext IN ( " & enumWorkflowStep.coWFWSTPDOC_DOCaprobacionOK & "," & enumWorkflowStep.coWFWSTPDOC_DOCpublicacionOK & "," & enumWorkflowStep.coWFWSTPDOC_DOCrevisionOK & "," & enumWorkflowStep.coWFWSTPDOC_DOCEdicionOK & ")" _
                                & " AND cod <=(" _
                                        & " SELECT TOP 1 cod from DOC_DOCLOG where doccod=" & auxCod & " AND wfwstepnext IN ( " & enumWorkflowStep.coWFWSTPDOC_DOCDocumentovigente & ") ORDER BY cod desc" _
                                & ")" _
                                & "ORDER BY cod DESC")
                    Dim auxUltAprobador_Cod As Integer = 1
                    Dim auxUltAprobador_WfwStepNext As Integer = enumWorkflowStep.coWFWSTPDOC_DOCaprobacionOK
                    If auxCodUltAprobacionDT.Rows.Count <> 0 Then
                        auxUltAprobador_Cod = auxCodUltAprobacionDT.Rows(0)("cod")
                        auxUltAprobador_WfwStepNext = auxCodUltAprobacionDT.Rows(0)("wfwstepnext")
                    End If

                    For Each auxRowAprobado As DataRow In auxConn.gConn_Query("SELECT DISTINCT EMP.dsc as emp_dsc,EMP.cod AS emp_cod,DOC_DOCLOG.fecha " _
                        & " FROM  DOC_DOCLOG " _
                        & " LEFT JOIN EMP ON DOC_DOCLOG.empcod=EMP.cod" _
                        & " WHERE wfwstepnext IN ( " & auxUltAprobador_WfwStepNext & ")" _
                        & " AND DOC_DOCLOG.doccod=" & auxCod _
                        & " AND DOC_DOCLOG.cod >=" & auxUltAprobador_Cod).Rows
                        If auxAprobadoPor <> "" Then
                            auxAprobadoPor &= ","
                        End If
                        If auxConn.gField_GetInt(auxRowAprobado("emp_cod"), -1) > 0 Then
                            auxAprobadoPor &= auxRowAprobado("emp_dsc")
                        Else
                            auxAprobadoPor &= "Sistema(automático)"
                        End If

                        auxAprobadoPorFecha = auxConn.gField_GetDate(auxRowAprobado("fecha")).ToString("d/M/yyyy")
                    Next
                    If auxAprobadoPor = "" Then
                        auxClass.gTRACE_add(auxCod, 10, "Aprobado por:" & auxConn.LastErrorDescription & ":" & auxConn.LastCommand)
                        auxAprobadoPor = "-------"
                        auxAprobadoPorFecha = "--/--/----"
                    End If
                End If




                If auxConn.gField_GetBoolean(auxDT.Rows(0)("doctip_templatefootcustom"), False) Then
                    auxFooterCenter = auxConn.gField_GetString(auxDT.Rows(0)("doctip_templatefootcenter"), "")
                    auxFooterCenter = auxClass.gContenido_ChangeVars(auxFooterCenter, _
                                    True, _
                                    auxClass.Conn.gField_GetString(auxDT.Rows(0)("dsc"), ""), _
                                      auxClass.Conn.gField_GetString(auxDT.Rows(0)("dsc0"), ""), _
                                    auxClass.Conn.gField_GetString(auxDT.Rows(0)("dsc1"), ""), _
                                    auxClass.Conn.gField_GetString(auxDT.Rows(0)("dsc2"), ""), _
                                    auxDT.Rows(0)("wfwstpdsc"), _
                                    auxClass.Conn.gField_GetString(auxDT.Rows(0)("identificador"), ""), _
                                    auxClass.Conn.gField_GetInt(auxDT.Rows(0)("version"), 0), _
                                    auxClaDsc, _
                                    auxEprCod, _
                                     auxClass.gSystem_GetParameterByID(coSysParamEprDsc), _
                                    auxFechaStr, _
                                    auxCopiaTexto, _
                                     auxClass.Conn.gField_GetString(auxDT.Rows(0)("wildoctipdsc"), ""), _
                                     auxAprobadoPor, auxAprobadoPorFecha, auxEspecificoA)
                    auxFooterRight = auxConn.gField_GetString(auxDT.Rows(0)("doctip_templatefootright"), "")
                    auxFooterRight = auxClass.gContenido_ChangeVars(auxFooterRight, _
                                    True, _
                                    auxClass.Conn.gField_GetString(auxDT.Rows(0)("dsc"), ""), _
                                      auxClass.Conn.gField_GetString(auxDT.Rows(0)("dsc0"), ""), _
                                    auxClass.Conn.gField_GetString(auxDT.Rows(0)("dsc1"), ""), _
                                    auxClass.Conn.gField_GetString(auxDT.Rows(0)("dsc2"), ""), _
                                    auxDT.Rows(0)("wfwstpdsc"), _
                                    auxClass.Conn.gField_GetString(auxDT.Rows(0)("identificador"), ""), _
                                    auxClass.Conn.gField_GetInt(auxDT.Rows(0)("version"), 0), _
                                    auxClaDsc, _
                                    auxEprCod, _
                                    auxClass.gSystem_GetParameterByID(coSysParamEprDsc), _
                                    auxFechaStr, _
                                    auxCopiaTexto, _
                                     auxClass.Conn.gField_GetString(auxDT.Rows(0)("wildoctipdsc"), ""), _
                                     auxAprobadoPor, auxAprobadoPorFecha, auxEspecificoA)
                    auxFooterLeft = auxConn.gField_GetString(auxDT.Rows(0)("doctip_templatefootleft"), "")
                    auxFooterLeft = auxClass.gContenido_ChangeVars(auxFooterLeft, _
                                         True, _
                                         auxClass.Conn.gField_GetString(auxDT.Rows(0)("dsc"), ""), _
                                           auxClass.Conn.gField_GetString(auxDT.Rows(0)("dsc0"), ""), _
                                         auxClass.Conn.gField_GetString(auxDT.Rows(0)("dsc1"), ""), _
                                         auxClass.Conn.gField_GetString(auxDT.Rows(0)("dsc2"), ""), _
                                         auxDT.Rows(0)("wfwstpdsc"), _
                                         auxClass.Conn.gField_GetString(auxDT.Rows(0)("identificador"), ""), _
                                         auxClass.Conn.gField_GetInt(auxDT.Rows(0)("version"), 0), _
                                         auxClaDsc, _
                                         auxEprCod, _
                                         auxClass.gSystem_GetParameterByID(coSysParamEprDsc), _
                                         auxFechaStr, _
                                         auxCopiaTexto, _
                                          auxClass.Conn.gField_GetString(auxDT.Rows(0)("wildoctipdsc"), ""), _
                                          auxAprobadoPor, auxAprobadoPorFecha, auxEspecificoA)
                End If

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
                Dim auxHrcSesID As String = ""
                '            If Request.QueryString("_sesid_") IsNot Nothing Then
                'auxHrcSesID = Request.QueryString("_sesid_").Trim
                'End If
                Dim auxMarcaDatos As String = ""
                If auxMode = 7 Or auxMode = 9 Or auxMode = 4 Or auxCopyMode = 1 Or auxCopyMode = 2 Then
                    auxMarcaDatos = "<span style=""color:red;"">*</span>"
                End If
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

                Dim auxMarginTopStr As String = ""
                Dim auxMarginBottomStr As String = ""
                Dim auxMarginLeftStr As String = ""
                Dim auxMarginRightStr As String = ""

                Dim auxValueInt As Integer
                auxValueInt = auxConn.gField_GetInt(auxClass.gSystem_GetParameterByID(coSysParamIDPrnMarginTopMM).Trim)
                If auxValueInt > 0 Then
                    auxMarginTopStr = auxValueInt
                Else
                    auxMarginTopStr = 28 ' 33    '28
                End If
                auxValueInt = auxConn.gField_GetInt(auxClass.gSystem_GetParameterByID(coSysParamIDPrnMarginBottomMM).Trim)
                If auxValueInt > 0 Then
                    auxMarginBottomStr = auxValueInt
                End If
                auxValueInt = auxConn.gField_GetInt(auxClass.gSystem_GetParameterByID(coSysParamIDPrnMarginLeftMM).Trim)
                If auxValueInt > 0 Then
                    auxMarginLeftStr = auxValueInt
                End If
                auxValueInt = auxConn.gField_GetInt(auxClass.gSystem_GetParameterByID(coSysParamIDPrnMarginRightMM).Trim)
                If auxValueInt > 0 Then
                    auxMarginRightStr = auxValueInt
                End If

                Dim auxHeaderSpacing As Integer = 0 ' 2
            Dim auxDepthLevel As Short = 0
            If auxMode = 8 Then
                If auxEmpCod > 0 Then
                    auxClass.gEntity_DOC_EMPDOC_Delete_ByFilter(" empcod =" & auxEmpCod _
                                                                 & " AND doccod=" & auxCod _
                                                                 & " AND reltypeid=1")
                    auxClass.gEntity_DOC_EMPDOC_SystemInsert(pempcod:=auxEmpCod, pdoccod:=auxCod, preltypeid:=1, pqsecdatetime:=Now)
                End If
            End If
                Select Case auxMode
                    Case 0, 6, 7, 8, 10 '0- Es el cuerpo para copia controlada,original//6=vigente//7=Vista online//8=vista online vigente
                        'Agrega un espacio para que no solape la cabecera
                        auxImpresion = ""
                        Dim auxLinkMode As String = "6"
                    Dim auxCuerpo As String = ""
                    Dim auxArchivo As Integer = auxClass.Conn.gField_GetInt(auxDT.Rows(0)("archivo"), -1)
                    If auxArchivo > 0 Then
                        Select Case Val(auxDT.Rows(0)("contenttypeid"))
                            Case clsHrcConnClient.enumMimeTypes.coHTML, clsHrcConnClient.enumMimeTypes.coUndefined
                                auxCuerpo = auxClass.Conn.gField_GetString(auxDT.Rows(0)("cuerpo"), "")
                            Case clsHrcConnClient.enumMimeTypes.coPNG, clsHrcConnClient.enumMimeTypes.coGIF, clsHrcConnClient.enumMimeTypes.coJPG
                                auxCuerpo = "<img src=""getbinary.aspx?id=" & auxArchivo & """ > "
                                'Case clsHrcConnClient.enumMimeTypes.coXLS, clsHrcConnClient.enumMimeTypes.coXLSX, clsHrcConnClient.enumMimeTypes.coDOC, clsHrcConnClient.enumMimeTypes.coDOCX
                                '    auxCuerpo = "<iframe src=""hrcbinaries.aspx?inline=1&id=" & auxArchivo & """ width=""100%"" height=""300"" type=""application/xls""></iframe>"
                            Case clsHrcConnClient.enumMimeTypes.coPDF
                                auxCuerpo = "<object data=""hrcbinaries.aspx?inline=1&id=" & auxArchivo & """ type=""application/pdf"" width=""100%"" height=""600px""> " _
                                    & "</object>"
                            Case Else
                                auxCuerpo = "<iframe src=""hrcbinaries.aspx?download=1&id=" & auxArchivo & """ frameborder=0 width=100% height=300 > " _
                                    & "</iframe>"
                        End Select
                    Else
                        auxCuerpo = auxClass.Conn.gField_GetString(auxDT.Rows(0)("cuerpo"), "")
                    End If
                    If auxMode = 7 Or auxMode = 8 Then
                        Dim auxFavoritesEnabled As Boolean = True

                        Dim auxActionButtons As String = ""

                        auxActionButtons &= "" _
                               & "<a href=""cfrmdocumentos.aspx?_view_=12"">" _
                                 & "<img style=""cursor: pointer;"" src=""" & auxClass.WebRootFolder & "imagenes/objhome.png"" class=hrcthemeimage height=""20px"" >" _
                               & "</a>"
                        If auxFavoritesEnabled Then
                            Dim auxButtonFavorite As New clsHrcJSButton("cmdFavorite", ".", "")
                            Dim auxIsFavorite As Boolean = False
                            If auxClass.Conn.gConn_QueryValueInt("SELECT cod FROM DOC_EMPDOC " _
                                                                & " WHERE doccod=" & auxCod _
                                                                & " AND empcod=" & auxEmpCod _
                                                                & " AND reltypeid=2", -1) > 0 Then
                                auxIsFavorite = True
                            End If
                            auxButtonFavorite.DesignType = clsHrcJSButton.enumDesignType.coHyperlink
                            AddHandler auxButtonFavorite.EventCommandHandler, AddressOf gAcciones_CommandHandler
                            auxButtonFavorite.BagValues.gValue_Add("DOCCOD", auxCod)
                            auxButtonFavorite.BagValues.gValue_Add("ACTION", "FAVORITO")
                            If auxIsFavorite Then
                                auxButtonFavorite.Title = "<img style=""cursor: pointer;"" class=hrcthemeimage src=""" & auxClass.WebRootFolder & "imagenes/objfavorite_yes.png"" height=""24px"" >"
                            Else
                                auxButtonFavorite.Title = "<img style=""cursor: pointer;"" class=hrcthemeimage src=""" & auxClass.WebRootFolder & "imagenes/objfavorite_no.png"" height=""24px"" >"
                            End If
                            auxButtonFavorite.HrcCod = auxCod
                            'auxButtonFavorite.AsyncCallEnabled = True
                            auxButtonFavorite.RaiseCommandOnClick = True
                            auxButtonFavorite.EventOnClick = "var auxResult = parseInt(pdata[0]['FAVORITO']);hrcConsole_log(auxResult);" _
                              & "if (auxResult==1){" _
                              & auxButtonFavorite.gJS_Title_Set("'<img style=""cursor: pointer;"" class=hrcthemeimage src=""" & auxClass.WebRootFolder & "imagenes/objfavorite_yes.png"" height=""24px"">'") _
                              & "}else{" _
                              & auxButtonFavorite.gJS_Title_Set("'<img style=""cursor: pointer;"" class=hrcthemeimage src=""" & auxClass.WebRootFolder & "imagenes/objfavorite_no.png"" height=""24px"">'") _
                              & "};"
                            auxActionButtons &= auxButtonFavorite.gControl_GetBodyDefinition
                            ScriptManager.RegisterStartupScript(Me, Me.GetType, "hrc" & auxButtonFavorite.ControlID, _
                                                               auxButtonFavorite.gControl_GetStartupScripts, True)
                        End If
                        If auxMode = 8 Then
                            'BOTON DE CONFIRMACION DE LECTURA
                            Dim auxLecturaPendiente As Integer = Val(auxClass.Conn.gConn_QueryValueString("SELECT COUNT(*) FROM DOC_DOCSGN LEFT JOIN DOC_DOCLOG ON DOC_DOCSGN.doclogcod = DOC_DOCLOG.cod " _
                                & "  WHERE DOC_DOCLOG.wfwstepnext=" & enumWorkflowStep.coWFWSTPDOC_DOCDocumentovigente _
                                & " AND DOC_DOCSGN.empcod=" & auxEmpCod _
                                & " AND DOC_DOCSGN.doccod=" & auxCod))
                            If auxLecturaPendiente > 0 Then
                                Dim auxButton As New clsHrcJSButton("cmdLecturaOK", "Confirmar lectura", "boton-acciones")
                                AddHandler auxButton.EventCommandHandler, AddressOf gAcciones_CommandHandler
                                auxButton.ConfirmMessage = "Confirma la lectura de todo el documento?"
                                auxButton.BagValues.gValue_Add("DOCCOD", auxCod)
                                auxButton.BagValues.gValue_Add("ACTION", "LECTURAOK")

                                auxButton.HrcCod = auxCod
                                auxButton.AsyncCallEnabled = True
                                auxButton.RaiseCommandOnClick = True
                                auxButton.EventOnAsyncCallSucess = "" _
                                  & "if (pdata[0]['RESULTADO']==''){" & auxButton.gHide & "}else{alert(pdata[0]['RESULTADO'])};"
                                auxActionButtons &= " " & auxButton.gControl_GetBodyDefinition & " "
                                ScriptManager.RegisterStartupScript(Me, Me.GetType, "hrc" & auxButton.ControlID, _
                                                                   auxButton.gControl_GetStartupScripts, True)
                            End If
                            If auxActionButtons <> "" Then
                                auxActionButtons &= "<hr />"
                            End If
                            'FIN BOTON DE CONFIRMACION DE LECTURA
                            auxImpresion = "<div style=positionx:fixed;width:100%;background-color:white >" _
                                        & auxActionButtons _
                                        & "</div>" _
                                        & "<div style=positionx:fixed;height:120px;width:100%;background-color:white >" _
                                        & auxClass.Conn.gField_GetString(auxDT.Rows(0)("templatehead"), "") _
                                        & "</div>" _
                                        & "<div style=height:20px;width:100%;position:float ></div>" _
                                        & "<div style=position:float;width:100%;background-color:white >" _
                                        & "Aprobado por:" & auxAprobadoPor & " el día " & auxAprobadoPorFecha & "."

                            auxCuerpo = Replace(auxCuerpo, "font-size: 20px", "font-size: 12px")
                            auxImpresion &= "</div><hr /><span style=font-size:-8px >" _
                                        & auxCuerpo & "</span>"
                            auxCopiaTexto = "<span style=""color:red;"">Vista online</span>"
                            Dim auxSysParamIDPrnOnlineBackImageDisabled As Boolean = auxClass.Conn.gField_GetBoolean(auxClass.gSystem_GetParameterByID(coSysParamIDPrnOnlineBackImageDisabled), False)
                            If auxSysParamIDPrnOnlineBackImageDisabled = False Then
                                pnlcuerpo.Attributes.Add("style", "background-image: url('imagenes/fondo_online.png'); background-attachment:fixed; background-position: 100% 100%; background-repeat: repeat;""")
                            End If


                        ElseIf auxMode = 7 Then
                            If auxActionButtons <> "" Then
                                auxActionButtons &= "<hr />"
                            End If
                            auxImpresion &= "<div style=positionx:fixed;width:100%;background-color:white >" _
                                        & auxActionButtons _
                                        & "</div>" _
                                        & "<div style=positionx:fixed;height:120px;width:100%;background-color:white >" _
                                        & auxClass.Conn.gField_GetString(auxDT.Rows(0)("templatehead"), "") _
                                        & "</div>" _
                                        & "<div style=height:20px;width:100%;position:float ></div>" _
                                        & "<div style=position:float;width:100%;background-color:white;overflow:hidden; >" _
                                        & "<span style=""color:red;"">"
                            'auxImpresion &= "Contenido en " & auxDT.Rows(0)("wfwstpdsc")
                            auxImpresion &= "</span>" _
                                        & "</div><hr />" _
                                        & "" & auxCuerpo & ""
                            auxCopiaTexto = "<span style=""color:red;"">*" & "Contenido en " & auxDT.Rows(0)("wfwstpdsc") & "</span>"

                            Dim auxSysParamIDPrnCopyBackImageDisabled As Boolean = auxClass.Conn.gField_GetBoolean(auxClass.gSystem_GetParameterByID(coSysParamIDPrnCopyBackImageDisabled), False)
                            If auxSysParamIDPrnCopyBackImageDisabled = False Then
                                pnlcuerpo.Attributes.Add("style", "background-image: url('imagenes/fondo_borrador.png'); background-attachment:fixed; background-position: 100% 100%; background-repeat: repeat;""")
                            End If


                        End If

                        auxLinkMode = auxMode

                        auxImpresion = Replace(auxImpresion, "#SITEURL_", VirtualPathUtility.GetDirectory(Request.Path))
                        auxImpresion = Replace(auxImpresion, "http://siteurl/", VirtualPathUtility.GetDirectory(Request.Path) & "/")
                        For Each auxRow As DataRow In auxConn.gConn_Query("SELECT doccodref FROM DOC_DOCREF WHERE doccod=" & auxCod).Rows
                            auxImpresion = Replace(auxImpresion, """#LINK_DOCUMENTO_" & Val(auxRow("doccodref")) & "_""", _
                                                 Request.Url.AbsolutePath & "?_mode_=" & auxLinkMode & "&param1=" & auxRow("doccodref"))
                        Next


                        auxClass.gEntity_DOC_DOCREADS_Insert(pdoccod:=auxCod, _
                                                             pempcod:=Session("empcod"), _
                                                             pversion:=auxDT.Rows(0)("version"), pfecha:=auxConn.gDate_GetNow)
                    Else ' If auxMode = 0 Then
                        'ES PDF
                        auxImpresion &= "<br />" & auxCuerpo
                        auxImpresion = "<div style=margin-left:12mm;width:50cm;font-size:9mm>" & auxImpresion & "</div>"
                        'Ajusta las imagenes
                        Try
                            Dim auxDoc As New HtmlAgilityPack.HtmlDocument
                            auxDoc.OptionReadEncoding = False
                            auxDoc.OptionUseIdAttribute = False
                            auxDoc.OptionCheckSyntax = False
                            auxDoc.LoadHtml(auxImpresion)

                            Dim auxLoc1 As Integer
                            Dim auxValue As String
                            Dim auxName As String
                            Dim auxStyleStr As String
                            ' auxDoc.DocumentNode.SelectNodes -> Esto puede dar Nothing porque no hay nodos
                            '

                            Dim auxHTMLCollection As HtmlAgilityPack.HtmlNodeCollection

                            Dim auxWidth As Integer
                            Dim auxHeight As Integer
                            Dim auxStackNode As New Stack(Of String)
                            auxStackNode.Push("//img")
                            If auxPrnCfgCod > 1 Then
                                auxStackNode.Push("//td")
                                auxStackNode.Push("//tr")
                                auxStackNode.Push("//table")
                                'auxStackNode.Push("//div")
                            End If

                            Do While auxStackNode.Count <> 0
                                auxHTMLCollection = auxDoc.DocumentNode.SelectNodes(auxStackNode.Pop)
                                'auxHTMLCollection = Nothing
                                If auxHTMLCollection IsNot Nothing Then
                                    For Each auxImg As HtmlAgilityPack.HtmlNode In auxHTMLCollection
                                        Dim auxStyles As String = auxImg.GetAttributeValue("style", "").Trim
                                        If auxStyles = "" Then
                                            auxStyleStr = auxImg.GetAttributeValue("width", "")
                                            If auxStyleStr <> "" Then
                                                auxStyles &= "width:" & auxStyleStr & ";"
                                            End If
                                            auxStyleStr = auxImg.GetAttributeValue("height", "")
                                            If auxStyleStr <> "" Then
                                                auxStyles &= "height:" & auxStyleStr & ";"
                                            End If
                                        End If

                                        If auxStyles <> "" Then
                                            Dim auxI As Integer = 0
                                            auxStyleStr = ""
                                            auxClass.gSys_DebugLogAdd("PDF node:" & auxStyles)
                                            For Each auxStyle As String In Split(auxStyles, ";")
                                                auxLoc1 = InStr(auxStyle, ":")
                                                If auxLoc1 <> 0 Then
                                                    auxName = Left(auxStyle, auxLoc1 - 1).Trim.ToLower
                                                    auxValue = Right(auxStyle, auxStyle.Length - auxLoc1)
                                                    Select Case auxName
                                                        Case "width"
                                                            If InStr(auxValue, "%") = 0 Then
                                                                auxWidth = Val(auxValue)
                                                                auxValue = ""
                                                            End If
                                                        Case "height"
                                                            If InStr(auxValue, "%") = 0 Then
                                                                auxHeight = Val(auxValue)
                                                                auxValue = ""
                                                            End If
                                                    End Select
                                                    If auxValue <> "" Then
                                                        auxStyleStr &= auxName & ":" & auxValue & ";"
                                                    End If
                                                End If
                                            Next
                                        End If

                                        'If auxHeight > 800 Then
                                        '    auxHeight = 800
                                        'End If
                                        'If auxWidth > 800 Then
                                        '    auxWidth = 800
                                        'End If
                                        If auxHeight > auxWidth Then
                                            auxWidth = auxWidth * (auxWidth / auxHeight)
                                            auxHeight = 0
                                        ElseIf auxWidth > 0 Then
                                            'auxHeight = 0
                                            'auxWidth = 100
                                        End If
                                        If auxPrnCfgCod > 1 Then
                                            If auxWidth > 0 Then
                                                If auxWidth > 800 Then
                                                    auxWidth = 800
                                                End If
                                                auxWidth = auxWidth / 800 * 100
                                                auxHeight = 0
                                                'auxHeight = auxWidth * 1.5
                                            End If
                                        End If
                                        'If auxHeight = 0 And auxWidth > 0 Then
                                        '    auxWidth = auxWidth / 800 * 100
                                        'ElseIf auxHeight > 0 And auxWidth = 0 Then
                                        '    auxHeight = auxHeight / 800 * 100
                                        'ElseIf auxWidth < auxHeight Then
                                        '    Dim auxPorc As Double = 1 ' auxWidth / auxHeight
                                        '    auxWidth = (auxWidth / 800) * auxPorc * 100
                                        '    auxHeight = (auxHeight / 800) * auxPorc * 100
                                        'ElseIf auxWidth > auxHeight Then
                                        '    Dim auxPorc As Double = 1 '  auxWidth / auxHeight
                                        '    auxWidth = (auxWidth / 800) * auxPorc * 100
                                        '    auxHeight = (auxHeight / 800) * auxPorc * 100
                                        'End If
                                        If auxWidth > 0 Then
                                            'auxStyleStr &= "width:" & CInt(auxWidth * 3 * 800 / 100) & "px;"
                                            auxStyleStr &= "width:" & auxWidth & "%;"
                                        End If
                                        If auxHeight > 0 Then
                                            'auxStyleStr &= "height:" & CInt(auxHeight * 12 * 800 / 100) & "px;"
                                            auxStyleStr &= "height:" & auxHeight & "%;"
                                        End If
                                        auxImg.SetAttributeValue("style", auxStyleStr)
                                    Next
                                End If
                            Loop

                            'Font
                            auxHTMLCollection = auxDoc.DocumentNode.SelectNodes("//font")
                            If auxHTMLCollection IsNot Nothing Then
                                For Each auxFont As HtmlAgilityPack.HtmlNode In auxHTMLCollection
                                    auxValue = auxFont.GetAttributeValue("size", "")
                                    If InStr(auxValue, "px") <> 0 Or InStr(auxValue, "pt") <> 0 Then
                                        Select Case Val(auxValue)
                                            Case 72
                                                auxValue = auxDT.Rows(0)("font72px")
                                            Case 48
                                                auxValue = auxDT.Rows(0)("font48px")
                                            Case 36
                                                auxValue = auxDT.Rows(0)("font36px")
                                            Case 28
                                                auxValue = auxDT.Rows(0)("font28px")
                                            Case 26
                                                auxValue = auxDT.Rows(0)("font26px")
                                            Case 24
                                                auxValue = auxDT.Rows(0)("font24px")
                                            Case 22
                                                auxValue = auxDT.Rows(0)("font22px") ' "11mm"
                                            Case 20
                                                auxValue = auxDT.Rows(0)("font20px")
                                            Case 18
                                                auxValue = auxDT.Rows(0)("font18px")
                                            Case 16
                                                auxValue = auxDT.Rows(0)("font16px")
                                            Case 14
                                                auxValue = auxDT.Rows(0)("font14px")
                                            Case 12
                                                auxValue = auxDT.Rows(0)("font12px")
                                            Case 11
                                                auxValue = auxDT.Rows(0)("font11px")
                                            Case 10
                                                auxValue = auxDT.Rows(0)("font10px")
                                            Case 9
                                                auxValue = auxDT.Rows(0)("font9px")
                                            Case 8
                                                auxValue = auxDT.Rows(0)("font8px")
                                        End Select
                                    ElseIf InStr(auxValue, "mm") = 0 Then
                                        Select Case Val(auxValue)
                                            Case 8
                                                auxValue = "11mm"
                                            Case 7
                                                auxValue = "10mm"
                                            Case 6
                                                auxValue = "9mm"
                                            Case 5
                                                auxValue = "8mm"
                                            Case 4
                                                auxValue = "7mm"
                                            Case 3
                                                auxValue = "6mm"
                                            Case 2
                                                auxValue = "5mm"
                                        End Select
                                    End If
                                    auxFont.SetAttributeValue("size", auxValue)
                                Next
                            End If
                            'Referencias
                            auxHTMLCollection = auxDoc.DocumentNode.SelectNodes("//span")
                            If auxHTMLCollection IsNot Nothing Then
                                For Each auxSpan As HtmlAgilityPack.HtmlNode In auxHTMLCollection
                                    Dim auxStyles As String = auxSpan.GetAttributeValue("style", "")
                                    Dim auxI As Integer = 0
                                    auxStyleStr = ""
                                    auxClass.gSys_DebugLogAdd("PDF node:" & auxStyles)
                                    For Each auxStyle As String In Split(auxStyles, ";")
                                        auxLoc1 = InStr(auxStyle, ":")
                                        If auxLoc1 <> 0 Then
                                            auxName = Left(auxStyle, auxLoc1 - 1).Trim.ToLower
                                            auxValue = Right(auxStyle, auxStyle.Length - auxLoc1)
                                            Select Case auxName
                                                Case "font-size"
                                                    If InStr(auxValue, "px") <> 0 Or InStr(auxValue, "pt") <> 0 Then
                                                        Select Case Val(auxValue)
                                                            Case 72
                                                                auxValue = auxDT.Rows(0)("font72px")
                                                            Case 48
                                                                auxValue = auxDT.Rows(0)("font48px")
                                                            Case 36
                                                                auxValue = auxDT.Rows(0)("font36px")
                                                            Case 28
                                                                auxValue = auxDT.Rows(0)("font28px")
                                                            Case 26
                                                                auxValue = auxDT.Rows(0)("font26px")
                                                            Case 24
                                                                auxValue = auxDT.Rows(0)("font24px")
                                                            Case 22
                                                                auxValue = auxDT.Rows(0)("font22px") ' "11mm"
                                                            Case 20
                                                                auxValue = auxDT.Rows(0)("font20px")
                                                            Case 18
                                                                auxValue = auxDT.Rows(0)("font18px")
                                                            Case 16
                                                                auxValue = auxDT.Rows(0)("font16px")
                                                            Case 14
                                                                auxValue = auxDT.Rows(0)("font14px")
                                                            Case 12
                                                                auxValue = auxDT.Rows(0)("font12px")
                                                            Case 11
                                                                auxValue = auxDT.Rows(0)("font11px")
                                                            Case 10
                                                                auxValue = auxDT.Rows(0)("font10px")
                                                            Case 9
                                                                auxValue = auxDT.Rows(0)("font9px")
                                                            Case 8
                                                                auxValue = auxDT.Rows(0)("font8px")
                                                        End Select
                                                    End If
                                            End Select
                                            If auxValue <> "" Then
                                                auxStyleStr &= auxName & ":" & auxValue & ";"
                                            End If
                                        End If
                                        auxSpan.SetAttributeValue("style", auxStyleStr)
                                    Next
                                Next
                            End If

                            'Span
                            auxHTMLCollection = auxDoc.DocumentNode.SelectNodes("//a")
                            auxHTMLCollection = Nothing
                            If auxHTMLCollection IsNot Nothing Then
                                For Each auxHRef As HtmlAgilityPack.HtmlNode In auxHTMLCollection
                                    auxHRef.SetAttributeValue("href", Replace(auxHRef.InnerHtml, "http://siteurl/", auxClass.gSystem_GetParameterByID(enumSysIDParams.ExternalURL)))
                                    auxHRef.InnerHtml = "Link a documento externo"
                                Next
                            End If

                            auxImpresion = auxDoc.DocumentNode.OuterHtml
                        Catch ex As Exception
                            'Dim auxClass As New clsCusimDOC
                            auxClass.gSys_DebugLogAdd("Exception generating PDF:" & ex.Message)
                        End Try

                        'auxImpresion = Replace(auxImpresion, "width: 960px; height: 634px", "width: 80%")
                        'auxImpresion = Replace(auxImpresion, "width: 960px; height: 829px", "width: 80%")
                        'auxImpresion = Replace(auxImpresion, "width: 800px; height: 200px", "width: 100%")
                        auxImpresion = Replace(auxImpresion, "font-size: 12px", "font-size:9mm")

                        lblStyles.InnerHtml = "h1 {font-size:15mm;}" _
                           & "h2 {font-size:14mm;}" _
                           & "h3 {font-size:12mm;}" _
                           & "h4 {font-size:11mm;}" _
                           & "h5 {font-size:10mm;}" _
                           & "h6 {font-size:9mm;}" _
                           & "li {left-margin:1mm}"
                        If auxEsCopia Then
                            Dim auxSysParamIDPrnCopyBackImageDisabled As Boolean = auxClass.Conn.gField_GetBoolean(auxClass.gSystem_GetParameterByID(coSysParamIDPrnCopyBackImageDisabled), False)
                            If auxSysParamIDPrnCopyBackImageDisabled = False Then
                                pnlcuerpo.Attributes.Add("style", "background-image: url('imagenes/fondo_copia.png'); background-attachment:fixed; background-position: 100% 100%; background-repeat: repeat;""")
                            End If
                        End If
                        'auxImpresion = Replace(auxImpresion, "#SITEURL_", "", , , CompareMethod.Text)
                        'auxImpresion = Replace(auxImpresion, "http://siteurl/", "", , , CompareMethod.Text)
                        'auxImpresion = Replace(auxImpresion, "#LINK_DOCUMENTO_", "", , , CompareMethod.Text)

                        auxImpresion = Replace(auxImpresion, "#SITEURL_", VirtualPathUtility.GetDirectory(Request.Path))
                        auxImpresion = Replace(auxImpresion, "http://siteurl/", VirtualPathUtility.GetDirectory(Request.Path) & "/")
                        For Each auxRow As DataRow In auxConn.gConn_Query("SELECT DOC_DOC.dsc,DOC_DOCREF.doccodref FROM DOC_DOCREF " _
                                                                          & " LEFT JOIN DOC_DOC ON DOC_DOCREF.doccodref = DOC_DOC.cod " _
                                                                          & " WHERE DOC_DOCREF.doccod=" & auxCod).Rows
                            auxImpresion = Replace(auxImpresion, """#LINK_DOCUMENTO_" & Val(auxRow("doccodref")) & "_""", _
                                                 "Link a documento [" & auxRow("dsc") & "]")
                        Next
                    End If

                    'auxClass.Sec.gLogin_SessionLogoff(auxHrcSesID)

                Case 1  'header
                        auxImpresion = auxClass.Conn.gField_GetString(auxDT.Rows(0)("templatehead"), "")
                        'auxImpresion = "<div style=height:" & (auxHeaderSpacing + auxMarginTop + 3) & "mm;font-size:3mm;overflow:hidden >" & auxImpresion & "</div>"
                        auxImpresion = "<div style=height:" & (auxHeaderSpacing + Val(auxMarginTopStr)) & "mm;font-size:10px;overflow-y:hidden >" & auxImpresion & "</div>"
                        lblStyles.InnerHtml = ".h1 {font-size:15mm;} " _
                               & ".h2 {font-size:14mm;} " _
                               & ".h3 {font-size:12mm;} " _
                               & ".h4 {font-size:11mm;} " _
                               & ".h5 {font-size:10mm;} " _
                               & ".h6 {font-size:9mm;} " _
                               & ".header-doc-dsc {font-size:12px;} "
                        'auxClass.Sec.gLogin_SessionLogoff(auxHrcSesID)
                Case 2 'foot
                Case 3                  'Original
                        If m_IsAdmin Or auxClass.Sec.gSID_CheckAccess(auxSidCod, auxClass.enumAccessType.coSYSImprimircopiascontroladas) Then
                            Dim auxConnection As New imClientConnection

                            'auxConnection.Debug = True
                            Dim auxSesionID1 As String = auxClass.Sec.gLogin_CreateDelegatedSessionToSystem
                            Dim auxSesionID2 As String = auxClass.Sec.gLogin_CreateDelegatedSessionToSystem
                            Dim auxReturn As String = ""
                            Dim auxBinary As clsHrcConnClient.clsBinaryData = auxConnection.gFile_ToPDFBinary(auxFileName, _
                                                                Replace(Request.Url.AbsoluteUri, "_mode_=" & auxMode, "_mode_=6") & "&_sesid_=" & auxSesionID1, _
                                                              Replace(Request.Url.AbsoluteUri, "_mode_=" & auxMode, "_mode_=1") & "&_sesid_=" & auxSesionID2, _
                                                              auxMarginTopStr, auxHeaderSpacing.ToString, auxFooterLeft, auxFooterCenter, auxFooterRight, auxUserName, auxPassword, auxTemporalFolder, imClientConnection.enumPaperKind.coA4, auxProcessUserName, auxProcessPassword, _
                                                               -1, True, True, auxDepthLevel, auxMarginLeftStr, auxMarginRightStr, auxMarginBottomStr)
                            If auxBinary IsNot Nothing Then
                                auxClass.gEntity_DOC_DOCLOG_Insert(pdoccod:=auxCod, pfecha:=auxConn.gDate_GetNow, pempcod:=Session("empcod"), pdelempcod:=Session("empcod"), _
                                       pdsc:="Copia controlada", pobs:="Impresión ORIGINAL desde equipo [" & auxPC & "]", _
                                        pwfwstpprev:=auxDT.Rows(0)("wfwstpcod"), pwfwstepnext:=auxDT.Rows(0)("wfwstpcod"), _
                                        phsthidgencod:=-1)
                                auxConnection.gFile_Download(auxBinary)
                                auxClass.Sec.gLogin_SessionLogoff(auxSesionID1)
                                auxClass.Sec.gLogin_SessionLogoff(auxSesionID2)
                            Else
                                auxClass.gTRACE_add(auxCod, 1, "Error generando PDF:" & auxReturn)
                                Response.Write("Error generando PDF:" & auxReturn)
                            End If
                        Else
                            gForm_Close()
                        End If
                Case 4
                        '4=Copy no controlada
                        If m_IsAdmin Or auxClass.Sec.gSID_CheckAccess(auxSidCod, auxClass.enumAccessType.coSYSGlobalLeer & "," & auxClass.enumAccessType.coSYSGlobalModificar & "," & auxClass.enumAccessType.coSYSImprimircopiasnocontroladas) Then
                            Dim auxConnection As New imClientConnection
                            'auxConnection.Debug = True
                            Dim auxSesionID1 As String = auxClass.Sec.gLogin_CreateDelegatedSessionToSystem
                            Dim auxSesionID2 As String = auxClass.Sec.gLogin_CreateDelegatedSessionToSystem
                            auxClass.gTRACE_add(auxCod, 10, "Creadas las sesiones:" & auxSesionID1 & " " & auxSesionID2)
                            Dim auxReturn As String = ""

                            ''Header
                            'Dim auxHeader As String = ""
                            'auxHeader = auxClass.Conn.gField_GetString(auxDT.Rows(0)("templatehead"), "")
                            'auxHeader = "<div style=height:" & (auxHeaderSpacing + auxMarginTop + 3) & "mm;font-size:3mm;overflow:hidden >" & auxImpresion & "</div>"
                            'lblStyles.InnerHtml = "h1 {font-size:15mm;}" _
                            '       & "h2 {font-size:14mm;}" _
                            '       & "h3 {font-size:12mm;}" _
                            '       & "h4 {font-size:11mm;}" _
                            '       & "h5 {font-size:10mm;}" _
                            '       & "h6 {font-size:9mm;}"
                            '////
                            '  
                            auxConnection.Debug = True
                            Dim auxBinary As clsHrcConnClient.clsBinaryData = auxConnection.gFile_ToPDFBinary(auxFileName, _
                                                              Replace(Request.Url.AbsoluteUri, "_mode_=" & auxMode, "_mode_=0&_copytype_=2") & "&_sesid_=" & auxSesionID1, _
                                                              Replace(Request.Url.AbsoluteUri, "_mode_=" & auxMode, "_mode_=1&_copytype_=2") & "&_sesid_=" & auxSesionID2, _
                                                             auxMarginTopStr, auxHeaderSpacing.ToString, auxFooterLeft, auxFooterCenter, auxFooterRight, auxUserName, auxPassword, auxTemporalFolder, imClientConnection.enumPaperKind.coA4, auxProcessUserName, auxProcessPassword, _
                                                             -1, True, True, auxDepthLevel, auxMarginLeftStr, auxMarginRightStr, auxMarginBottomStr)
                            If auxBinary IsNot Nothing Then
                                auxClass.gEntity_DOC_DOCLOG_Insert(pdoccod:=auxCod, pfecha:=auxConn.gDate_GetNow, pempcod:=Session("empcod"), pdelempcod:=Session("empcod"), _
                                                            pdsc:=coCopiaNoControladaTexto, pobs:="v" & auxDT.Rows(0)("version") & " - " & coCopiaNoControladaTexto.ToUpper & " desde equipo [" & auxPC & "]", _
                                                             pwfwstpprev:=-1, pwfwstepnext:=-1, _
                                                             phsthidgencod:=-1)
                                auxConnection.gFile_Download(auxBinary)
                                auxClass.Sec.gLogin_SessionLogoff(auxSesionID1)
                                auxClass.Sec.gLogin_SessionLogoff(auxSesionID2)
                            Else
                                'auxClass.gSys_DebugLogAdd("OK:" & auxUserName & auxPassword)
                                auxClass.gTRACE_add(auxCod, 1, "Error generando PDF:" & auxReturn)
                                Response.Write("Error generando PDF:" & auxReturn)
                            End If
                            Exit Sub
                        Else
                            auxClass.gTRACE_add(auxCod, 1, "Error generando PDF:No posee acceso a copias no controladas")
                            gForm_Close()
                        End If
                Case 5  'Copia controlada
                        If m_IsAdmin Or auxClass.Sec.gSID_CheckAccess(auxSidCod, auxClass.enumAccessType.coSYSGlobalModificar & "," & auxClass.enumAccessType.coSYSImprimircopiascontroladas) Then
                            'Dim auxCopiaNro As Integer = auxClass.Conn.gField_GetInt(auxDT.Rows(0)("copianro"), 0) + 1
                            Dim auxCopiaNro As Integer = Val(auxClass.Conn.gConn_QueryValue("SELECT copianro FROM DOC_DOC WHERE cod =" & auxCod, "0")) + 1

                            Dim auxConnection As New imClientConnection
                            'auxConnection.Debug = True
                            Dim auxSesionID1 As String = auxClass.Sec.gLogin_CreateDelegatedSessionToSystem
                            Dim auxSesionID2 As String = auxClass.Sec.gLogin_CreateDelegatedSessionToSystem
                            Dim auxReturn As String = ""
                            Dim auxBinary As clsHrcConnClient.clsBinaryData = auxConnection.gFile_ToPDFBinary(auxFileName, _
                                                                Replace(Request.Url.AbsoluteUri, "_mode_=" & auxMode, "_mode_=6") & "&_sesid_=" & auxSesionID1, _
                                                                Replace(Request.Url.AbsoluteUri, "_mode_=" & auxMode, "_mode_=1") & "&_sesid_=" & auxSesionID2, _
                                                                 auxMarginTopStr, auxHeaderSpacing.ToString, auxFooterLeft, auxFooterCenter, auxFooterRight, auxUserName, auxPassword, auxTemporalFolder, imClientConnection.enumPaperKind.coA4, auxProcessUserName, auxProcessPassword, _
                                                                -1, True, True, auxDepthLevel, auxMarginLeftStr, auxMarginRightStr, auxMarginBottomStr)


                            If auxBinary IsNot Nothing Then
                                auxClass.gEntity_DOC_DOCLOG_Insert(pdoccod:=auxCod, pfecha:=auxConn.gDate_GetNow, pempcod:=Session("empcod"), pdelempcod:=Session("empcod"), _
                                   pdsc:=coCopiaControladaTexto, pobs:="v" & auxDT.Rows(0)("version") & " - " & coCopiaControladaTexto.ToUpper & " nro." & auxCopiaNro & " desde equipo [" & auxPC & "]", _
                                    pwfwstpprev:=-1, pwfwstepnext:=-1, _
                                    phsthidgencod:=-1)
                                auxClass.gEntity_DOC_DOC_Update(pcod:=auxCod, pcopianro:=auxCopiaNro)

                                auxConnection.gFile_Download(auxBinary)
                                auxClass.Sec.gLogin_SessionLogoff(auxSesionID1)
                                auxClass.Sec.gLogin_SessionLogoff(auxSesionID2)
                            Else
                                auxClass.gTRACE_add(auxCod, 1, auxReturn)
                                Response.Write("Error generando PDF:" & auxReturn)
                            End If
                        Else
                            gForm_Close()
                        End If
                Case 9
                        '9=PDF doc_doc
                        If m_IsAdmin Or auxClass.Sec.gSID_CheckAccess(auxSidCod, auxClass.enumAccessType.coSYSGlobalCambiarestado & "," & auxClass.enumAccessType.coSYSGlobalCambiarpermisos) Then
                            Dim auxConnection As New imClientConnection
                            auxConnection.Debug = True
                            Dim auxSesionID1 As String = auxClass.Sec.gLogin_CreateDelegatedSessionToSystem
                            Dim auxSesionID2 As String = auxClass.Sec.gLogin_CreateDelegatedSessionToSystem
                            auxClass.gTRACE_add(auxCod, 10, "Creadas las sesiones:" & auxSesionID1 & " " & auxSesionID2)
                            Dim auxReturn As String = ""

                            Dim auxBinary As clsHrcConnClient.clsBinaryData = auxConnection.gFile_ToPDFBinary(auxFileName, _
                                                              Replace(Request.Url.AbsoluteUri, "_mode_=" & auxMode, "_mode_=0&_copytype_=1") & "&_sesid_=" & auxSesionID1, _
                                                              Replace(Request.Url.AbsoluteUri, "_mode_=" & auxMode, "_mode_=1&_copytype_=1") & "&_sesid_=" & auxSesionID2, _
                                                             auxMarginTopStr, auxHeaderSpacing.ToString, auxFooterLeft, auxFooterCenter, auxFooterRight, auxUserName, auxPassword, auxTemporalFolder, imClientConnection.enumPaperKind.coA4, auxProcessUserName, auxProcessPassword, _
                                                             -1, True, True, auxDepthLevel, auxMarginLeftStr, auxMarginRightStr, auxMarginBottomStr)
                            If auxBinary IsNot Nothing Then
                                auxConnection.gFile_Download(auxBinary)
                                auxClass.Sec.gLogin_SessionLogoff(auxSesionID1)
                                auxClass.Sec.gLogin_SessionLogoff(auxSesionID2)
                            Else
                                auxClass.gTRACE_add(auxCod, 1, auxReturn)
                                Response.Write("Error generando PDF:" & auxReturn)
                            End If
                            Exit Sub
                        Else
                            gForm_Close()
                        End If
            End Select
                If auxImpresion <> "" Then
                    auxImpresion = auxClass.gContenido_ChangeVars(auxImpresion, _
                                      True, _
                                      auxClass.Conn.gField_GetString(auxDT.Rows(0)("dsc"), ""), _
                                        auxClass.Conn.gField_GetString(auxDT.Rows(0)("dsc0"), ""), _
                                      auxClass.Conn.gField_GetString(auxDT.Rows(0)("dsc1"), ""), _
                                      auxClass.Conn.gField_GetString(auxDT.Rows(0)("dsc2"), ""), _
                                      " ", _
                                      auxClass.Conn.gField_GetString(auxDT.Rows(0)("identificador"), "") & auxMarcaDatos, _
                                      auxClass.Conn.gField_GetInt(auxDT.Rows(0)("version"), 0), _
                                      auxClaDsc, _
                                      auxEprCod, _
                                      auxClass.gSystem_GetParameterByID(coSysParamEprDsc), _
                                      auxFechaStr, _
                                      auxCopiaTexto, _
                                       auxClass.Conn.gField_GetString(auxDT.Rows(0)("wildoctipdsc"), ""), _
                                       auxAprobadoPor, auxAprobadoPorFecha, auxEspecificoA)
                    lblTexto.Text = auxImpresion
                End If

        Catch ex As Exception
            Dim a As String = ex.Message
            auxClass.gSys_DebugLogAdd(ex.Message)
            'lblTexto.Text = ex.Message
        End Try
        auxClass.Conn.gConn_Close()
    End Sub
    Private Sub gAcciones_CommandHandler(ByVal pControl As clsHrcJSControlBasic, ByVal pValues As clshrcBagValues)
        Dim auxAsyncState_Initial As Boolean = False
        If pValues.gValue_Get("HRCASYNCSTATE", "0") = "1" Then
            auxAsyncState_Initial = True
        End If
        If pControl.ControlID = "CMDFAVORITE" Then
            Dim auxCod As Integer = Val(pValues.gValue_Get("HRCCOD", -1))
            Dim auxEmpCod As Integer = Session("empcod")
            Dim auxClass As New clsCusimDOC
            auxClass.Conn.gConn_Open()
            Dim auxEmpDocCod As Integer = auxClass.Conn.gConn_QueryValueInt("SELECT cod FROM DOC_EMPDOC " _
                                                & " WHERE doccod=" & auxCod _
                                                & " AND empcod=" & auxEmpCod _
                                                & " AND reltypeid=2", -1)
            If auxEmpDocCod < 1 Then
                auxClass.gEntity_DOC_EMPDOC_Insert(pempcod:=auxEmpCod, pdoccod:=auxCod, preltypeid:=2)
                pValues.gValue_Add("HRC_RESULTS", "[{""RESULT"":""1""" _
                    & ",""FAVORITO"":""1""" _
                    & "}]")
            Else
                auxClass.gEntity_DOC_EMPDOC_Delete(pcod:=auxEmpDocCod)
                pValues.gValue_Add("HRC_RESULTS", "[{""RESULT"":""1""" _
                    & ",""FAVORITO"":""0""" _
                    & "}]")
            End If
            auxClass.Conn.gConn_Close()
        Else
            Select Case auxAsyncState_Initial
                Case True

                    Dim auxCod As Integer = Val(pValues.gValue_Get("HRCCOD", -1))
                    Dim auxValues As New clshrcBagValues
                    auxValues.gValue_Add("Cod", auxCod)
                    Dim auxAction As enumWorkflowStep = enumWorkflowStep.coWFWSTPDOC_DOCLecturaOK
                    auxValues.gValue_Add("GotoStep", auxAction)
                    auxValues.gValue_Add("empcod", Session("empcod"))
                    Dim auxClass As New clsCusimDOC
                    auxClass.Conn.gConn_Open()
                    auxClass.gWorkflow_GotoStep(auxValues)
                    pValues.gValue_Add("HRC_RESULTS", "[{""result"":""1"",""HRC_EXECUTIONQUEUEID"":""" & auxValues.gValue_Get("HRC_EXECUTIONQUEUEID") & """}]")
                    auxClass.Conn.gConn_Close()
                Case False
                    Dim auxCod As Integer = Val(pValues.gValue_Get("HRCCOD", -1))
                    Dim auxconn As clsHrcConnClient = Session("conn")
                    auxconn = auxconn.gComponent_CreateInstance()
                    auxconn.gConn_Open()
                    Dim auxLecturaPendiente As Boolean = False
                    If auxconn.gConn_Query("SELECT DOC_DOCSGN.cod FROM DOC_DOCSGN " _
                                & " LEFT JOIN DOC_DOCLOG ON DOC_DOCSGN.doclogcod= DOC_DOCLOG.cod" _
                                & " WHERE DOC_DOCLOG.wfwstepnext = " & enumWorkflowStep.coWFWSTPDOC_DOCDocumentovigente _
                                & " AND DOC_DOCLOG.doccod=" & auxCod & " AND DOC_DOCSGN.empcod=" & Session("empcod")).Rows.Count > 0 Then
                        auxLecturaPendiente = True
                    End If
                    'Val(auxconn.gConn_QueryValueString("SELECT COUNT(*) FROM DOC_DOCSGN LEFT JOIN DOC_DOCLOG ON DOC_DOCSGN.doclogcod = DOC_DOCLOG.cod " _
                    '                    & "  WHERE DOC_DOCLOG.wfwstepnext=" & enumWorkflowStep.coWFWSTPDOC_DOCDocumentovigente _
                    '                    & " AND DOC_DOCSGN.empcod=" & Session("empcod") _
                    '                    & " AND DOC_DOCSGN.doccod=" & auxCod))
                    If auxLecturaPendiente Then
                        pValues.gValue_Add("HRC_RESULTS", "[{""result"":""1""" _
                                & ",""RESULTADO"":""Error!""" _
                                & "}]")
                    Else
                        pValues.gValue_Add("HRC_RESULTS", "[{""result"":""1""" _
                                & ",""RESULTADO"":""""" _
                                & "}]")
                    End If

                    auxconn.gConn_Close()
            End Select
        End If

    End Sub
    Private Sub gForm_Close()
        'If Request.QueryString("_closea_") = "1" Then
        ScriptManager.RegisterStartupScript(Me, Me.GetType(), "CloseWindow", "self.close();", True)
        'End If
    End Sub
End Class
