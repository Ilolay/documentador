Imports System.Data
Imports System.Data.SqlClient
Imports Intelimedia.imComponentes
Imports clsCusimDOC

Imports System.Web.UI.DataVisualization.Charting
Imports Intelimedia.Hercules.Language
Imports Intelimedia.Hercules.Storage.clsHrcConnClient


Imports System.Xml
Imports Intelimedia.inTasks


'Captcha
Imports Intelimedia.Hercules.Design
Imports System.IO
Imports System.Drawing
Imports System.Drawing.Drawing2D
Imports System.Drawing.Imaging
Public Class cfrmReportes
    Inherits System.Web.UI.Page
    Private auxEmpDT As DataTable
    Private prvGridData As clshrcGrdData
    Dim auxSelectCombo As Integer

    Dim auxTipoDocumentoCodList As New List(Of Integer)
    Dim auxUnidadDocumentoCodList As New List(Of Integer)
    Dim auxProcesoCodList As New List(Of Integer)
    Dim auxColaboradorCodList As New List(Of Integer)
    Dim auxUnidadColaboradorCodList As New List(Of Integer)
    Dim auxEstadoCodList As New List(Of Integer)



    Protected Function Hay_Error() As Boolean
        'lblerror.Text = ""

        'If txtfechadesde.Text = Nothing Then
        '    lblerror.Text = "ERROR: Fecha desde incompleta"
        '    Return True
        'End If
        'If txtfechaHasta.Text = Nothing Then
        '    lblerror.Text = "ERROR: Fecha hasta incompleta"
        '    Return True
        'End If
        'If CType(txtfechadesde.Text, Date) > CType(txtfechaHasta.Text, Date) Then
        '    lblerror.Text = "ERROR: Fecha desde posterior a Fecha hasta"
        '    Return True
        'End If
        Return False
    End Function
    Friend m_Mode As Short = 0
    Friend m_view As Short = 0
    Friend m_DocCod As Integer
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim auxClass As New clsCusimDOC
        auxClass.Conn.gConn_Close()
        Dim auxClient As New clsHrcCodeHTML
        Dim auxQueryString As clshrcBagValues = auxClient.gBagValues_GetFromQueryString(Request.QueryString.ToString)
        m_Mode = Val(auxQueryString.gValue_Get("_mode_"))
        m_view = Val(auxQueryString.gValue_Get("_view_"))
        If m_view = 10 Then
            m_view = -1
        End If
        m_DocCod = Val(auxQueryString.gValue_Get("param1"))
        If Not Page.IsPostBack Then
            If Request.QueryString("_param5_") Is Nothing Then
                'chrHorasCons.Visible = False
                'txtDesde.Text = Today.Date.AddDays(-30).ToString("d")
                'txtfechadesde.Text = "01/" & Right("0" & Today.AddMonths(-12).Month.ToString(), 2) & "/" & Today.AddMonths(-12).Year.ToString()
                'txtfechaHasta.Text = Today.Date.ToString("d")
            End If
            If m_Mode = 0 Then

                If m_view = 31 Or m_view = 33 Then
                    gData_Get_Buscar()
                    pnlTitle.Visible = False
                    pnlSearch.Visible = False
                    pnlActions.Visible = False
                Else
                    cargarCmb(m_Mode)
                    activarFiltros()

                End If

            ElseIf m_Mode = 1 Then 'Impresion de etiquetas
                Dim auxConnection As New imClientConnection
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
                If Request.QueryString("_sesid_") IsNot Nothing Then
                    auxHrcSesID = Request.QueryString("_sesid_").Trim
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
                auxConnection.gFile_DownloadAsPDF("etiqueta.pdf", _
                                                 Replace(Request.Url.AbsoluteUri, "_mode_=" & m_Mode, "_mode_=2"), _
                                                  "", "", "", "", auxUserName, auxPassword, auxTemporalFolder)
                Dim a As String = ""
            ElseIf m_Mode = 2 Then 'Solo gráfico
                pnlSearch.Visible = False
                pnlActions.Visible = False
                pnlTitle.Visible = False
                gData_Get()
            ElseIf m_Mode = 3 Then


            ElseIf m_Mode = 5 Then 'Reporte PDF
                Dim auxMode As Integer = Val((Request.QueryString("_paramMode_")))
                Dim auxValue As Integer = Val((Request.QueryString("_view_")))
                ' cuando se agreguen más opciones a los combos, contemplarlas aquí.


                If auxValue >= 21 Then
                    cargarCmb(auxMode)
                    activarFiltros()

                    cmbView.ClearSelection()
                    Dim auxItem As ListItem = cmbView.Items.FindByValue(auxValue)
                    If auxItem IsNot Nothing Then
                        auxItem.Selected = True
                    End If


                    'cmbView.SelectedValue = Val((Request.QueryString("_view_")))

                    If auxClass.Conn.gField_GetBigInt(Request.QueryString("_param2_"), -1) > 0 Then
                        auxUnidadDocumentoCodList.Add(Request.QueryString("_param2_"))
                    End If
                    cmbIndicador.SelectedValue = Val(Request.QueryString("_param3_"))
                    cmbGroup.SelectedValue = Val(Request.QueryString("_param4_"))
                    txtFechaDesde.Text = Request.QueryString("_param5_")
                    txtFechaHasta.Text = Request.QueryString("_param6_")


                    If auxClass.Conn.gField_GetBigInt(Request.QueryString("_param7_"), -1) > 0 Then
                        auxTipoDocumentoCodList.Add(Request.QueryString("_param7_"))
                    End If
                    If auxClass.Conn.gField_GetBigInt(Request.QueryString("_param8_"), -1) > 0 Then
                        auxProcesoCodList.Add(Request.QueryString("_param8_"))
                    End If
                    If auxClass.Conn.gField_GetBigInt(Request.QueryString("_param9_"), -1) > 0 Then
                        auxColaboradorCodList.Add(Request.QueryString("_param9_"))
                    End If
                    If auxClass.Conn.gField_GetBigInt(Request.QueryString("_param10_"), -1) > 0 Then
                        auxUnidadColaboradorCodList.Add(Request.QueryString("_param10_"))
                    End If


                    If auxClass.Conn.gField_GetString(Request.QueryString("_param11_")) <> "" Then
                        cmbAgrupacion.SelectedValue = Val(Request.QueryString("_param11_"))
                    End If

                    If auxClass.Conn.gField_GetString(Request.QueryString("_param12_")) <> "" Then
                        txtTituloDocumento.Text = Request.QueryString("_param12_")
                    End If

                    If auxClass.Conn.gField_GetBigInt(Request.QueryString("_param13_"), -1) > 0 Then
                        auxEstadoCodList.Add(Request.QueryString("_param13_"))
                    End If




                    Dim auxConnection As New imClientConnection
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
                    Dim auxSeSid As String = Request.QueryString("_sesid_")
                    If auxSeSid IsNot Nothing Then
                        auxSeSid = Request.QueryString("_sesid_")

                        auxClass.gSystem_CheckAccess(auxSeSid)
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
                    Dim auxDT As DataTable = Nothing

                    gData_Get_Buscar()

                Else
                    If auxValue < 8 Then
                        cargarCmb(0)    ' carga combos para reportes de gráficos
                        activarFiltros()
                    Else
                        cargarCmb(0)    ' carga combos par reportes de tiempos
                        activarFiltros()
                    End If
                    cmbView.ClearSelection()
                    Dim auxItem As ListItem = cmbView.Items.FindByValue(auxValue)
                    If auxItem IsNot Nothing Then
                        auxItem.Selected = True
                    End If
                    m_view = Val(Request.QueryString("_view_"))
                    If m_view = 14 Or m_view = 15 Then '
                        rowfechas.Visible = True
                    Else
                        rowfechas.Visible = False
                    End If

                    If auxClass.Conn.gField_GetBigInt(Request.QueryString("_param2_"), -1) > 0 Then
                        auxUnidadDocumentoCodList.Add(Request.QueryString("_param2_"))
                    End If
                    cmbIndicador.SelectedValue = Val(Request.QueryString("_param3_"))
                    cmbGroup.SelectedValue = Val(Request.QueryString("_param4_"))
                    txtFechaDesde.Text = Request.QueryString("_param5_")
                    txtFechaHasta.Text = Request.QueryString("_param6_")


                    gData_Get()
                End If
                pnlSearch.Visible = False
                pnlActions.Visible = False

            End If

        Else
            activarFiltros()
        End If
        auxClass.Conn.gConn_Close()


    End Sub

    Private Sub cargarCmb(ByVal pMode As String)
        ' Al agregar nuevas opciones a los combos, ver cómo funcionan en modo=5 (reporte PDF): línea 133 aprox.
        
        cmbView.Items.Add(New ListItem("Por unidad específica", 1))
        cmbView.Items.Add(New ListItem("Por tipo de documentos", 2))
        cmbView.Items.Add(New ListItem("Por proceso", 3))
        cmbView.Items.Add(New ListItem("Por clasificación", 4))
        cmbView.Items.Add(New ListItem("Por sistema", 5))

        cmbView.Items.Add(New ListItem("Lecturas", 21))
        cmbView.Items.Add(New ListItem("Versiones de documentos", 22))
        cmbView.Items.Add(New ListItem("Impresiones de documentos", 23))
        cmbView.Items.Add(New ListItem("Editores", 24))
        cmbView.Items.Add(New ListItem("Revisores", 29))
        cmbView.Items.Add(New ListItem("Publicadores", 30))
        cmbView.Items.Add(New ListItem("Impresores", 25))
        cmbView.Items.Add(New ListItem("Referencias entre documentos", 26))
        cmbView.Items.Add(New ListItem("Actividad", 27))
        cmbView.Items.Add(New ListItem("Acciones pendientes", 28))
        cmbView.Items.Add(New ListItem("Cantidad de lecturas", 31, False))
        cmbView.Items.Add(New ListItem("Resumen de etapas", 32))
        cmbView.Items.Add(New ListItem("Detalle de etapas", 33, False))
        cmbView.Items.Add(New ListItem("Documentos que requieren reidentificación", 34))


        cmbView.SelectedValue = 21

    End Sub



    Private Sub activarFiltros()

        If Not Page.IsPostBack Then

            Select Case cmbView.SelectedValue
                Case 21, 23, 24, 25, 27, 28, 29, 30
                    cmbAgrupacion.Items.Clear()
                    cmbAgrupacion.Items.Add(New ListItem("", 0))
                    cmbAgrupacion.Items.Add(New ListItem("Unidad", 1))
                    cmbAgrupacion.Items.Add(New ListItem("Colaborador", 2))
                    cmbAgrupacion.Items.Add(New ListItem("Tipo de sistema", 3))
                    cmbAgrupacion.Items.Add(New ListItem("Proceso", 4))
                    cmbAgrupacion.Items.Add(New ListItem("Tipo de documento", 5))
                    cmbAgrupacion.Items.Add(New ListItem("Estado", 6))
                    Select Case cmbView.SelectedValue
                        Case 24, 25, 29, 30
                            cmbAgrupacion.Items.Add(New ListItem("Documento", 7))
                        Case Else
                            cmbAgrupacion.Items.Add(New ListItem("Documento", 7, False))
                    End Select
                Case 22, 26
                    cmbAgrupacion.Items.Clear()
                    cmbAgrupacion.Items.Add(New ListItem("", 0))
                    cmbAgrupacion.Items.Add(New ListItem("Unidad", 1))
                    cmbAgrupacion.Items.Add(New ListItem("Colaborador", 2, False))
                    cmbAgrupacion.Items.Add(New ListItem("Tipo de sistema", 3))
                    cmbAgrupacion.Items.Add(New ListItem("Proceso", 4))
                    cmbAgrupacion.Items.Add(New ListItem("Tipo de documento", 5))
                    cmbAgrupacion.Items.Add(New ListItem("Estado", 6, False))
                    cmbAgrupacion.Items.Add(New ListItem("Documento", 7, False))
            End Select
        End If

        'Filas de filtros -------------------
        rowfechas.Visible = False
        row_001.Visible = False
        row_002.Visible = False
        row_003.Visible = False
        row_004.Visible = False
        '------------------------------------
        'lblund.Visible = False
        lblFechaDesde.Visible = False
        txtFechaDesde.Visible = False
        lblFechaHasta.Visible = False
        txtFechaHasta.Visible = False
        lblTipoDocumento.Visible = False
        lstTipoDocumento.Visible = False
        lblUnidadDocumento.Visible = False
        lstUnidadDocumento.Visible = False
        lblTituloDocumento.Visible = False
        txtTituloDocumento.Visible = False
        lblProceso.Visible = False
        lstProceso.Visible = False
        lblColaborador_UnidadColaborador.Visible = False
        lstColaborador_UnidadColaborador.Visible = False
        lblAgrupacion.Visible = False
        cmbAgrupacion.Visible = False

        Select Case cmbView.SelectedValue
            Case 1, 2, 3, 4, 5, 6
                lblUnidadDocumento.Visible = True
                lstUnidadDocumento.Visible = True
                row_001.Visible = True
                row_002.Visible = True

            Case 21, 23, 24, 25, 27, 28, 29, 30
                lblFechaDesde.Visible = True
                txtFechaDesde.Visible = True
                lblFechaHasta.Visible = True
                txtFechaHasta.Visible = True
                lblTipoDocumento.Visible = True
                lstTipoDocumento.Visible = True
                lblUnidadDocumento.Visible = True
                lstUnidadDocumento.Visible = True
                lblTituloDocumento.Visible = True
                txtTituloDocumento.Visible = True
                lblProceso.Visible = True
                lstProceso.Visible = True
                lblColaborador_UnidadColaborador.Visible = True
                lstColaborador_UnidadColaborador.Visible = True
                lblAgrupacion.Visible = True
                cmbAgrupacion.Visible = True
                row_001.Visible = True
                row_002.Visible = True
                row_003.Visible = True
                row_004.Visible = True
                rowfechas.Visible = True
            Case 34
                row_001.Visible = True
            Case 22
                lblAgrupacion.Visible = True
                cmbAgrupacion.Visible = True
                row_001.Visible = True
            Case 26
                lblFechaDesde.Visible = True
                txtFechaDesde.Visible = True
                lblFechaHasta.Visible = True
                txtFechaHasta.Visible = True
                lblTipoDocumento.Visible = True
                lstTipoDocumento.Visible = True
                lblUnidadDocumento.Visible = True
                lstUnidadDocumento.Visible = True
                lblTituloDocumento.Visible = True
                txtTituloDocumento.Visible = True
                lblProceso.Visible = True
                lstProceso.Visible = True
                lblAgrupacion.Visible = True
                cmbAgrupacion.Visible = True
                row_001.Visible = True
                row_002.Visible = True
                row_003.Visible = True
                row_004.Visible = True
                rowfechas.Visible = True
            Case 32
                lblFechaDesde.Visible = True
                txtFechaDesde.Visible = True
                lblFechaHasta.Visible = True
                txtFechaHasta.Visible = True
                lblColaborador_UnidadColaborador.Visible = True
                lstColaborador_UnidadColaborador.Visible = True
                row_001.Visible = True
                row_003.Visible = True
                rowfechas.Visible = True
        End Select

        '+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
        '------------------ AUTOSUGERIDOS --------------------------------------
        '+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++

        Dim auxClientCon As New imClientConnection
        Dim auxConn As clsHrcConnClient = Session("conn")

        Dim auxHrcContext As clsHrcJSContext
        If Environment.MachineName = "A16WIN8" Then
            auxHrcContext = auxClientCon.gObjectTmp_Download("test")
            If auxHrcContext Is Nothing Then
                auxHrcContext = New clsHrcJSContext("hrccontext", Session("conn"), Session("security"), Session("hrcAlerts"))
                auxClientCon.gObjectTmp_Upload(auxHrcContext, "test")
            End If
        Else
            auxHrcContext = Session("hrcContext")
        End If

        '---Tipo de documentos---
        Dim auxScript1 As String = ""
        Dim auxTipoDocumento As clshrcObjectExplorer
        auxTipoDocumento = auxHrcContext.gObjectTmp_Download("asu_Tipo_Documento_list")
        ' auxTipoDocumento = Nothing
        If auxTipoDocumento Is Nothing Then
            auxTipoDocumento = New clshrcObjectExplorer("asu_Tipo_Documento_list", "hrcGrdData.ashx", Nothing, auxConn, auxClientCon)

            auxTipoDocumento.gAutosuggest_Enabled("SELECT cod, dsc," & enumEntities.coEntityDOC_DOCTIP & " as q_type,'Tipo de documento' as line1 " _
                                                & " FROM DOC_DOCTIP " _
                                                & " WHERE cod > 0 " _
                                                & " AND dsc LIKE '%{#HRCDSC#}%' " _
                                                & " ORDER BY dsc,cod")
            auxTipoDocumento.AutosuggestWidth = 270
            auxTipoDocumento.StringStartText = "Tipo de documento"

            auxHrcContext.gObjectTmp_Upload(auxTipoDocumento, "asu_Tipo_Documento_list")
        End If

        auxScript1 = auxTipoDocumento.gControl_GetStartupScripts
        ScriptManager.RegisterStartupScript(Me, Me.GetType, "hrc" & auxTipoDocumento.ControlID, _
                                           auxScript1, True)
        lstTipoDocumento.InnerHtml = auxTipoDocumento.gControl_GetBodyDefinition
        cmdLimpiar.OnClientClick &= auxTipoDocumento.gJS_ClearAll


        '---Unidad de documentos---
        Dim auxScript2 As String = ""
        Dim auxUnidadDocumento As clshrcObjectExplorer
        auxUnidadDocumento = auxHrcContext.gObjectTmp_Download("asu_Unidad_Documento_list")
        'auxUnidadDocumento = Nothing
        If auxUnidadDocumento Is Nothing Then
            auxUnidadDocumento = New clshrcObjectExplorer("asu_Unidad_Documento_list", "hrcGrdData.ashx", Nothing, auxConn, auxClientCon)

            auxUnidadDocumento.gAutosuggest_Enabled("SELECT cod, dsc," & enumEntities.coEntityUND & " as q_type,'Unidad de documento' as line1 " _
                                                & " FROM UND " _
                                                & " WHERE cod > 0 " _
                                                & " AND dsc LIKE '%{#HRCDSC#}%' " _
                                                & " ORDER BY dsc,cod")
            auxUnidadDocumento.AutosuggestWidth = 270
            auxUnidadDocumento.StringStartText = "Unidad de documento"

            auxHrcContext.gObjectTmp_Upload(auxUnidadDocumento, "asu_Unidad_Documento_list")
        End If

        auxScript2 = auxUnidadDocumento.gControl_GetStartupScripts
        ScriptManager.RegisterStartupScript(Me, Me.GetType, "hrc" & auxUnidadDocumento.ControlID, _
                                           auxScript2, True)
        lstUnidadDocumento.InnerHtml = auxUnidadDocumento.gControl_GetBodyDefinition
        cmdLimpiar.OnClientClick &= auxUnidadDocumento.gJS_ClearAll

        '---Procesos---
        Dim auxScript3 As String = ""
        Dim auxProcesos As clshrcObjectExplorer
        auxProcesos = auxHrcContext.gObjectTmp_Download("asu_Proceso_list")
        ' auxProcesos = Nothing
        If auxProcesos Is Nothing Then
            auxProcesos = New clshrcObjectExplorer("asu_Proceso_list", "hrcGrdData.ashx", Nothing, auxConn, auxClientCon)

            auxProcesos.gAutosuggest_Enabled("SELECT cod, dsc," & enumEntities.coEntityDOC_PRO & " as q_type,'Proceso' as line1 " _
                                                & " FROM DOC_PRO " _
                                                & " WHERE cod > 0 " _
                                                & " AND dsc LIKE '%{#HRCDSC#}%' " _
                                                & " ORDER BY dsc,cod")
            auxProcesos.AutosuggestWidth = 270
            auxProcesos.StringStartText = "Proceso"

            auxHrcContext.gObjectTmp_Upload(auxProcesos, "asu_Proceso_list")
        End If

        auxScript3 = auxProcesos.gControl_GetStartupScripts
        ScriptManager.RegisterStartupScript(Me, Me.GetType, "hrc" & auxProcesos.ControlID, _
                                           auxScript3, True)
        lstProceso.InnerHtml = auxProcesos.gControl_GetBodyDefinition
        cmdLimpiar.OnClientClick &= auxProcesos.gJS_ClearAll

        '---Colaborador/Unidad de colaborador---
        Dim auxScript4 As String = ""
        Dim auxColaborador_UnidadColaborador As clshrcObjectExplorer
        auxColaborador_UnidadColaborador = auxHrcContext.gObjectTmp_Download("asu_Colaborador_UnidadColaborador_list")
        ' auxColaborador_UnidadColaborador = Nothing
        If auxColaborador_UnidadColaborador Is Nothing Then
            auxColaborador_UnidadColaborador = New clshrcObjectExplorer("asu_Colaborador_UnidadColaborador_list", "hrcGrdData.ashx", Nothing, auxConn, auxClientCon)

            auxColaborador_UnidadColaborador.gAutosuggest_Enabled("SELECT cod, dsc," & enumEntities.coEntityEMP & " as q_type,'Colaborador' as line1 " _
                                                & " FROM EMP " _
                                                & " WHERE cod > 0 " _
                                                & " AND dsc LIKE '%{#HRCDSC#}%' " _
                                                & " UNION " _
                                                & " SELECT cod, dsc," & enumEntities.coEntityUND & " as q_type,'Unidad de colaborador' as line1 " _
                                                & " FROM UND " _
                                                & " WHERE cod > 0 " _
                                                & " AND dsc LIKE '%{#HRCDSC#}%' " _
                                                & " ORDER BY dsc")
            auxColaborador_UnidadColaborador.AutosuggestWidth = 270
            auxColaborador_UnidadColaborador.StringStartText = "Colaborador/Unidad de colaborador"

            auxHrcContext.gObjectTmp_Upload(auxColaborador_UnidadColaborador, "asu_Colaborador_UnidadColaborador_list")
        End If

        auxScript4 = auxColaborador_UnidadColaborador.gControl_GetStartupScripts
        ScriptManager.RegisterStartupScript(Me, Me.GetType, "hrc" & auxColaborador_UnidadColaborador.ControlID, _
                                           auxScript4, True)
        lstColaborador_UnidadColaborador.InnerHtml = auxColaborador_UnidadColaborador.gControl_GetBodyDefinition
        cmdLimpiar.OnClientClick &= auxColaborador_UnidadColaborador.gJS_ClearAll


        '---Autosugerido Estado ---
        Dim auxScript5 As String = ""
        Dim auxEstados As clshrcObjectExplorer
        auxEstados = auxHrcContext.gObjectTmp_Download("asu_Estado_list")
        ' auxEstados = Nothing
        If auxEstados Is Nothing Then
            auxEstados = New clshrcObjectExplorer("asu_Estado_list", "hrcGrdData.ashx", Nothing, auxConn, auxClientCon)

            auxEstados.gAutosuggest_Enabled("SELECT wfwstpcod, wfwstpdsc," & 111111 & " as q_type,'Estado' as line1 " _
                                                & " FROM Q_WFWSTP " _
                                                & " WHERE wfwstpcod > 0 " _
                                                & " AND wfwstpdsc LIKE '%{#HRCDSC#}%' " _
                                                & " ORDER BY wfwstpdsc,wfwstpcod")
            auxEstados.AutosuggestWidth = 270
            auxEstados.StringStartText = "Estados"

            auxHrcContext.gObjectTmp_Upload(auxEstados, "asu_Estado_list")
        End If

        auxScript5 = auxEstados.gControl_GetStartupScripts
        ScriptManager.RegisterStartupScript(Me, Me.GetType, "hrc" & auxEstados.ControlID, _
                                           auxScript5, True)
        lstEstado.InnerHtml = auxEstados.gControl_GetBodyDefinition
        cmdLimpiar.OnClientClick &= auxEstados.gJS_ClearAll

    End Sub

    


    Private Sub gAutosugerido_Download()
        Dim auxClientCon As New imClientConnection

        Dim auxHrcContext As clsHrcJSContext
        If Environment.MachineName = "A16WIN8" Then
            auxHrcContext = auxClientCon.gObjectTmp_Download("test")
            If auxHrcContext Is Nothing Then
                auxHrcContext = New clsHrcJSContext("hrccontext", Session("conn"), Session("security"), Session("hrcAlerts"))
                auxClientCon.gObjectTmp_Upload(auxHrcContext, "test")
            End If
        Else
            auxHrcContext = Session("hrcContext")
        End If

        'Auto Sugerido 1 - Tipo de Documento
        Dim auxTipoDocumento As clshrcObjectExplorer
        auxTipoDocumento = auxHrcContext.gObjectTmp_Download("asu_Tipo_Documento_list")
        If auxTipoDocumento IsNot Nothing Then
            If auxTipoDocumento.ItemList IsNot Nothing Then
                For Each auxNode As clsNode In auxTipoDocumento.ItemList
                    auxTipoDocumentoCodList.Add(auxNode.Cod)
                Next
            End If
        End If
        'Auto Sugerido 2 - Unidad de Documento
        Dim auxUnidadDocumento As clshrcObjectExplorer
        auxUnidadDocumento = auxHrcContext.gObjectTmp_Download("asu_Unidad_Documento_list")
        If auxUnidadDocumento IsNot Nothing Then
            If auxUnidadDocumento.ItemList IsNot Nothing Then
                For Each auxNode As clsNode In auxUnidadDocumento.ItemList
                    auxUnidadDocumentoCodList.Add(auxNode.Cod)
                Next
            End If
        End If
        'Auto Sugerido 3 - Procesos
        Dim auxProcesos As clshrcObjectExplorer
        auxProcesos = auxHrcContext.gObjectTmp_Download("asu_Proceso_list")
        If auxProcesos IsNot Nothing Then
            If auxProcesos.ItemList IsNot Nothing Then
                For Each auxNode As clsNode In auxProcesos.ItemList
                    auxProcesoCodList.Add(auxNode.Cod)
                Next
            End If
        End If
        'Auto Sugerido 4 - Colaborador / Unidad de colaborador
        Dim auxColaborador_UnidadColaborador As clshrcObjectExplorer
        auxColaborador_UnidadColaborador = auxHrcContext.gObjectTmp_Download("asu_Colaborador_UnidadColaborador_list")
        If auxColaborador_UnidadColaborador IsNot Nothing Then
            If auxColaborador_UnidadColaborador.ItemList IsNot Nothing Then
                For Each auxNode As clsNode In auxColaborador_UnidadColaborador.ItemList
                    If auxNode.Type = enumEntities.coEntityEMP Then
                        auxColaboradorCodList.Add(auxNode.Cod)
                    ElseIf auxNode.Type = enumEntities.coEntityUND Then
                        auxUnidadColaboradorCodList.Add(auxNode.Cod)
                    End If
                Next
            End If
        End If
        'Auto Sugerido 5 - Estados
        Dim auxEstados As clshrcObjectExplorer
        auxEstados = auxHrcContext.gObjectTmp_Download("asu_Estado_list")
        If auxEstados IsNot Nothing Then
            If auxEstados.ItemList IsNot Nothing Then
                For Each auxNode As clsNode In auxEstados.ItemList
                    auxEstadoCodList.Add(auxNode.Cod)
                Next
            End If
        End If

    End Sub

    Private Function gData_Get_Buscar() As DataTable
        Dim auxClass As New clsCusimDOC
        auxClass.Conn.gConn_Open()

        Dim auxClient As New clsHrcCodeHTML
        Dim auxQueryString As clshrcBagValues = auxClient.gBagValues_GetFromQueryString(Request.QueryString.ToString)

        Dim auxDT As DataTable = Nothing
        gAutosugerido_Download()

        If cmbView.SelectedValue = "" Then
            m_view = Val(auxQueryString.gValue_Get("_view_"))
            cmbView.SelectedValue = m_view
        Else
            m_view = cmbView.SelectedValue
        End If


        Select Case m_view
            Case 21
                auxDT = gData_Get_Grilla(gReports_Data_GetSelectCommand(gReports_DocumentosMasLeidos(m_view, 1, auxClass.Conn.gField_GetInt(cmbAgrupacion.SelectedValue), auxTipoDocumentoCodList, auxUnidadDocumentoCodList, auxProcesoCodList, auxColaboradorCodList, auxUnidadColaboradorCodList, auxClass.Conn.gField_GetString(txtTituloDocumento.Text, ""), txtFechaDesde.Text, txtFechaHasta.Text, auxEstadoCodList)))
            Case 22
                auxDT = gData_Get_Grilla(gReports_Data_GetSelectCommand(gReports_VersionesDocumentos(m_view, 1, auxClass.Conn.gField_GetInt(cmbAgrupacion.SelectedValue))))
            Case 23
                auxDT = gData_Get_Grilla(gReports_Data_GetSelectCommand(gReports_ImpresionesDocumentos(m_view, 1, auxClass.Conn.gField_GetInt(cmbAgrupacion.SelectedValue), auxTipoDocumentoCodList, auxUnidadDocumentoCodList, auxProcesoCodList, auxColaboradorCodList, auxUnidadColaboradorCodList, auxClass.Conn.gField_GetString(txtTituloDocumento.Text, ""), txtFechaDesde.Text, txtFechaHasta.Text, auxEstadoCodList)))
            Case 24
                auxDT = gData_Get_Grilla(gReports_Data_GetSelectCommand(gReports_ColaboradoresEnDOC(m_view, 1, auxClass.Conn.gField_GetInt(cmbAgrupacion.SelectedValue), auxTipoDocumentoCodList, auxUnidadDocumentoCodList, auxProcesoCodList, auxColaboradorCodList, auxUnidadColaboradorCodList, auxClass.Conn.gField_GetString(txtTituloDocumento.Text, ""), txtFechaDesde.Text, txtFechaHasta.Text, auxEstadoCodList)))
            Case 25
                auxDT = gData_Get_Grilla(gReports_Data_GetSelectCommand(gReports_ColaboradoresEnDOC(m_view, 1, auxClass.Conn.gField_GetInt(cmbAgrupacion.SelectedValue), auxTipoDocumentoCodList, auxUnidadDocumentoCodList, auxProcesoCodList, auxColaboradorCodList, auxUnidadColaboradorCodList, auxClass.Conn.gField_GetString(txtTituloDocumento.Text, ""), txtFechaDesde.Text, txtFechaHasta.Text, auxEstadoCodList)))
            Case 26
                auxDT = gData_Get_Grilla(gReports_Data_GetSelectCommand(gReports_ReferenciaEntreDocumentos(m_view, 1, auxClass.Conn.gField_GetInt(cmbAgrupacion.SelectedValue), auxTipoDocumentoCodList, auxUnidadDocumentoCodList, auxProcesoCodList, auxColaboradorCodList, auxUnidadColaboradorCodList, auxClass.Conn.gField_GetString(txtTituloDocumento.Text, ""), txtFechaDesde.Text, txtFechaHasta.Text)))
            Case 27
                auxDT = gData_Get_Grilla(gReports_Data_GetSelectCommand(gReports_Actividad(m_view, 1, auxClass.Conn.gField_GetInt(cmbAgrupacion.SelectedValue), auxTipoDocumentoCodList, auxUnidadDocumentoCodList, auxProcesoCodList, auxColaboradorCodList, auxUnidadColaboradorCodList, auxClass.Conn.gField_GetString(txtTituloDocumento.Text, ""), txtFechaDesde.Text, txtFechaHasta.Text, auxEstadoCodList)))
            Case 28
                auxDT = gData_Get_Grilla(gReports_Data_GetSelectCommand(gReports_AccionesPendientes(m_view, 1, auxClass.Conn.gField_GetInt(cmbAgrupacion.SelectedValue), auxTipoDocumentoCodList, auxUnidadDocumentoCodList, auxProcesoCodList, auxColaboradorCodList, auxUnidadColaboradorCodList, auxClass.Conn.gField_GetString(txtTituloDocumento.Text, ""), txtFechaDesde.Text, txtFechaHasta.Text, auxEstadoCodList)))
            Case 29
                auxDT = gData_Get_Grilla(gReports_Data_GetSelectCommand(gReports_ColaboradoresEnDOC(m_view, 1, auxClass.Conn.gField_GetInt(cmbAgrupacion.SelectedValue), auxTipoDocumentoCodList, auxUnidadDocumentoCodList, auxProcesoCodList, auxColaboradorCodList, auxUnidadColaboradorCodList, auxClass.Conn.gField_GetString(txtTituloDocumento.Text, ""), txtFechaDesde.Text, txtFechaHasta.Text, auxEstadoCodList)))
            Case 30
                auxDT = gData_Get_Grilla(gReports_Data_GetSelectCommand(gReports_ColaboradoresEnDOC(m_view, 1, auxClass.Conn.gField_GetInt(cmbAgrupacion.SelectedValue), auxTipoDocumentoCodList, auxUnidadDocumentoCodList, auxProcesoCodList, auxColaboradorCodList, auxUnidadColaboradorCodList, auxClass.Conn.gField_GetString(txtTituloDocumento.Text, ""), txtFechaDesde.Text, txtFechaHasta.Text, auxEstadoCodList)))
            Case 31
                Dim auxDocCod As Integer = auxClass.Conn.gField_GetInt(Request.QueryString("_doccod_"), -1)
                auxDT = gData_Get_Grilla(gReports_Data_GetSelectCommand(gReports_CantidadLeidas(m_view, 1, auxTipoDocumentoCodList, auxUnidadDocumentoCodList, auxProcesoCodList, auxColaboradorCodList, auxUnidadColaboradorCodList, auxDocCod, auxEstadoCodList)))
            Case 32
                auxDT = gData_Get_Grilla(gReports_Data_GetSelectCommand(gReports_ResumenEtapas(m_view, 1, auxColaboradorCodList, auxUnidadColaboradorCodList, txtFechaDesde.Text, txtFechaHasta.Text)))
            Case 33
                Dim auxDocTipCod As Integer = auxClass.Conn.gField_GetInt(Request.QueryString("_doctipcod_"), -1)
                auxDT = gData_Get_Grilla(gReports_Data_GetSelectCommand(gReports_DetalleEtapas(m_view, 1, auxDocTipCod)))
            Case 34
                auxDT = gData_Get_Grilla(gReports_Data_GetSelectCommand(gReports_DOCReid()))
        End Select

        auxClass.Conn.gConn_Close()
        updupdpnlSearch.Update()
        updupdpnlResults.Update()

        Return auxDT
    End Function


    Private Function gData_Get_Grilla(ByVal pBagValues As clshrcBagValues) As DataTable


        Dim auxDT As DataTable = pBagValues.gValue_Get("DT")
        Dim auxError As String = pBagValues.gValue_Get("DTRESULT")
        Dim auxErrorDescripcion As String = pBagValues.gValue_Get("ErrorDescripcion")

        Dim auxIDFiltros As String = pBagValues.gValue_Get("_idfiltros_")

        'Dim auxClass As New clscusReport
        'Dim auxHierarchy As New clsHrcHierarchyTable(auxClass.Conn, "")
        Dim auxHierarchy As clsHrcHierarchyTable = pBagValues.gValue_Get("Hierarchy")
        Dim auxClass As New clsCusimDOC
        auxClass.Conn.gConn_Open()
        If auxErrorDescripcion = "" Then
            If auxError = "" Then
                If auxDT.Rows.Count > 0 Then
                    Dim auxClientCon As New imClientConnection
                    Dim auxGridCacheID As String = ""
                    auxGridCacheID = ViewState("griddata2")
                    Dim auxConn As clsHrcConnClient = Session("conn")
                    If auxGridCacheID Is Nothing Then
                        auxGridCacheID = auxConn.gField_GetUniqueID
                        ViewState("griddata2") = auxGridCacheID
                    Else
                        prvGridData = auxClientCon.gObjectTmp_Download(auxGridCacheID)
                    End If

                    If m_view = 31 Then
                        prvGridData = New clshrcGrdData("grddata2", auxGridCacheID, "Detalle de lectores", _
                                                         "hrcgrdData.ashx", _
                                                            auxDT, "", False, False)
                    ElseIf m_view = 33 Then
                        prvGridData = New clshrcGrdData("grddata2", auxGridCacheID, "Detalle de etapas", _
                                                         "hrcgrdData.ashx", _
                                                            auxDT, "", False, False)
                    Else '
                        prvGridData = New clshrcGrdData("grddata2", auxGridCacheID, cmbView.SelectedItem.Text, _
                                                         "hrcgrdData.ashx", _
                                                            auxDT, "", False, False)
                    End If

                    

                    Select Case cmbView.SelectedValue
                        Case 21, 22, 23, 24, 25, 26, 27, 28, 29, 30
                            prvGridData.gTreeGrid_Enabled("level", "parent", "isleaf", "expanded")
                    End Select

                    'prvGridData.gDebug_On("c:\Windows\temp\grddata.txt", "c:\Windows\temp\_grddata.txt")
                    prvGridData.gPager_EnableVirtual()
                    Dim auxScript As String = ""
                    Dim auxFormatter As String = ""
                    Dim auxHtml As New Intelimedia.Hercules.Language.clsHrcCodeHTML


                    '****************************************************************
                    '***** AGREGAR LAS GRILLAS ACA ***************************
                    '******************************************************

                    Select Case cmbView.SelectedValue
                        Case 21
                            'Column oculta
                            prvGridData.gColumn_Add("Cod", 0, "q_cod", clshrcGrdData.enumAlign.coCenter, True, "", "", False)

                            auxScript = "function griddata_formatcol0(pCod,pDsc,pType) {" _
                                         & "var auxReturn = '&nbsp;';" _
                                          & "if (pType == " & enumEntities.coEntityDOC_DOC & "){" _
                                         & "auxReturn='<img width=12px alt="""" src=imagenes/icon' + gNumber_pad(pType,8) + '.png />' + '<u style=color:Blue;><span style=cursor:pointer; onclick=hrcShowModal(""cfrmdocumentos1_det.aspx?_closea_=1&_mode_=0&param1=""+""' + pCod + '"") >" _
                                         & "'  + pDsc " _
                                         & "+ '</span></u>';" _
                                         & "}" _
                                         & "if (pType == " & 111111 & "){" _
                                         & "auxReturn=pDsc;" _
                                         & "}" _
                                         & "if (pType != " & 111111 & " && pType != " & enumEntities.coEntityDOC_DOC & "){" _
                                         & "var auxReturn = '<img width=12px alt="""" src=imagenes/icon' + gNumber_pad(pType,8) + '.png />' + pDsc;" _
                                         & "}" _
                                         & "return auxReturn;" _
                                         & "}"
                            auxFormatter = "griddata_formatcol0({#CURRENTROW_COD#},{#CURRENTROW_Q_DSC#},{#CURRENTROW_Q_TYPE#})"
                            prvGridData.gColumn_Add("Documento", 170, "q_dsc", clshrcGrdData.enumAlign.coLeft, False, auxFormatter, auxScript, False)

                            'Column 1: Cantidad
                            auxScript = "function griddata_formatcol1(pCantidad,pCod,pAgrupacionCod) {" _
                                & "var auxReturn = '&nbsp;';" _
                                 & "if (pCantidad != ''){" _
                                & "auxReturn='<u style=color:Blue;><span style=cursor:pointer; onclick=hrcShowModal(""cfrmReportes.aspx?_view_=31&_mode_=0&_closea_=1&_report_=21&_idfiltros_=" & auxIDFiltros & "&_doccod_=' + pCod + '&_agrupa_=' + pAgrupacionCod + '"") >" _
                                & "'  + pCantidad + '</span></u>';" _
                                & "}" _
                                & "return auxReturn;" _
                                & "}"
                            auxFormatter = "griddata_formatcol1({#CURRENTROW_CANTIDAD#},{#CURRENTROW_COD#},{#CURRENTROW_AGRUPACIONCOD#})"
                            prvGridData.gColumn_Add("Cantidad", 20, "CANTIDAD", clshrcGrdData.enumAlign.coRight, False, auxFormatter, auxScript, False)

                        Case 22, 26
                            'Column oculta
                            prvGridData.gColumn_Add("Cod", 0, "q_cod", clshrcGrdData.enumAlign.coCenter, True, "", "", False)

                            auxScript = "function griddata_formatcol0(pCod,pDsc,pType) {" _
                                        & "var auxReturn = '&nbsp;';" _
                                         & "if (pType == " & enumEntities.coEntityDOC_DOCVIG & "){" _
                                        & "auxReturn='<img width=12px alt="""" src=imagenes/icon' + gNumber_pad(pType,8) + '.png />' + '<u style=color:Blue;><span style=cursor:pointer; onclick=hrcShowModal(""cfrmdocumentos1_det.aspx?_closea_=1&_mode_=0&param1=""+""' + pCod + '"") >" _
                                        & "'  + pDsc " _
                                        & "+ '</span></u>';" _
                                        & "}" _
                                        & "if (pType == " & 111111 & "){" _
                                        & "var auxReturn = '<img width=12px alt="""" src=imagenes/icon' + gNumber_pad(16,8) + '.png />' + pDsc;" _
                                        & "}" _
                                        & "if (pType != " & 111111 & " && pType != " & enumEntities.coEntityDOC_DOCVIG & "){" _
                                        & "var auxReturn = '<img width=12px alt="""" src=imagenes/icon' + gNumber_pad(pType,8) + '.png />' + pDsc;" _
                                        & "}" _
                                        & "return auxReturn;" _
                                        & "}"
                            auxFormatter = "griddata_formatcol0({#CURRENTROW_COD#},{#CURRENTROW_Q_DSC#},{#CURRENTROW_Q_TYPE#})"
                            If cmbView.SelectedValue = 22 Then
                                prvGridData.gColumn_Add("Documento vigente", 170, "q_dsc", clshrcGrdData.enumAlign.coLeft, False, auxFormatter, auxScript, False)
                            ElseIf cmbView.SelectedValue = 26 Then
                                prvGridData.gColumn_Add("Referencias de documentos vigentes", 170, "q_dsc", clshrcGrdData.enumAlign.coLeft, False, auxFormatter, auxScript, False)
                            End If

                            'Column 1: VERSION
                            auxScript = "function griddata_formatcol1(pVersion) {" _
                                & "var auxReturn = '&nbsp;';" _
                                 & "if (pVersion != ''){" _
                                & "auxReturn= pVersion;" _
                                & "}" _
                                & "return auxReturn;" _
                                & "}"
                            auxFormatter = "griddata_formatcol1({#CURRENTROW_VERSION#})"
                            prvGridData.gColumn_Add("Versión", 30, "VERSION", clshrcGrdData.enumAlign.coRight, False, auxFormatter, auxScript, False)

                            'Column 2: FECHA DE VERSION
                            auxScript = "function griddata_formatcol2(pFecha){" _
                                & "var auxReturn = '&nbsp;';" _
                                 & "if (pFecha != ''){" _
                                & "auxReturn = " & auxHtml.gJS_Date_ParseAndFormat("pFecha", Intelimedia.Hercules.Language.clsHrcCodeHTML.enumDateTimeFormat.coDate) & ";" _
                                & "}" _
                                & "return auxReturn;" _
                                & "}"
                            auxFormatter = "griddata_formatcol2({#CURRENTROW_FECHA#})"
                            prvGridData.gColumn_Add("Fecha de versión", 30, "FECHA", clshrcGrdData.enumAlign.coRight, False, auxFormatter, auxScript, False)

                        Case 23
                            'Column oculta
                            prvGridData.gColumn_Add("Cod", 0, "q_cod", clshrcGrdData.enumAlign.coCenter, True, "", "", False)

                            auxScript = "function griddata_formatcol0(pCod,pDsc,pType) {" _
                                        & "var auxReturn = '&nbsp;';" _
                                         & "if (pType == " & enumEntities.coEntityDOC_DOC & "){" _
                                        & "auxReturn='<img width=12px alt="""" src=imagenes/icon' + gNumber_pad(pType,8) + '.png />' + '<u style=color:Blue;><span style=cursor:pointer; onclick=hrcShowModal(""cfrmdocumentos1_det.aspx?_closea_=1&_mode_=0&param1=""+""' + pCod + '"") >" _
                                        & "'  + pDsc " _
                                        & "+ '</span></u>';" _
                                        & "}" _
                                        & "if (pType == " & 111111 & "){" _
                                        & "auxReturn=pDsc;" _
                                        & "}" _
                                        & "if (pType != " & 111111 & " && pType != " & enumEntities.coEntityDOC_DOC & "){" _
                                        & "var auxReturn = '<img width=12px alt="""" src=imagenes/icon' + gNumber_pad(pType,8) + '.png />' + pDsc;" _
                                        & "}" _
                                        & "return auxReturn;" _
                                        & "}"
                            auxFormatter = "griddata_formatcol0({#CURRENTROW_COD#},{#CURRENTROW_Q_DSC#},{#CURRENTROW_Q_TYPE#})"
                            prvGridData.gColumn_Add("Documento", 170, "q_dsc", clshrcGrdData.enumAlign.coLeft, False, auxFormatter, auxScript, False)

                            'Column 1: Cantidad de copias controladas
                            auxScript = "function griddata_formatcol1(pCantidad) {" _
                                & "var auxReturn = '&nbsp;';" _
                                 & "if (pCantidad != ''){" _
                                & "auxReturn= pCantidad;" _
                                & "}" _
                                & "return auxReturn;" _
                                & "}"
                            auxFormatter = "griddata_formatcol1({#CURRENTROW_CANTIDAD_CONTROLADA#})"
                            prvGridData.gColumn_Add("Cantidad de copias controladas", 30, "CANTIDAD_CONTROLADA", clshrcGrdData.enumAlign.coLeft, False, auxFormatter, auxScript, False)

                            'Column 2: Cantidad de copias NO controladas
                            auxScript = "function griddata_formatcol2(pCantidad) {" _
                                & "var auxReturn = '&nbsp;';" _
                                 & "if (pCantidad != ''){" _
                                & "auxReturn= pCantidad;" _
                                & "}" _
                                & "return auxReturn;" _
                                & "}"
                            auxFormatter = "griddata_formatcol2({#CURRENTROW_CANTIDAD_NO_CONTROLADA#})"
                            prvGridData.gColumn_Add("Cantidad de copias no controladas", 30, "CANTIDAD_NO_CONTROLADA", clshrcGrdData.enumAlign.coLeft, False, auxFormatter, auxScript, False)

                        Case 24, 25, 29, 30
                            'Column oculta
                            prvGridData.gColumn_Add("Cod", 0, "q_cod", clshrcGrdData.enumAlign.coCenter, True, "", "", False)

                            auxScript = "function griddata_formatcol0(pCod,pDsc,pType) {" _
                                         & "var auxReturn = '&nbsp;';" _
                                          & "if (pType == " & enumEntities.coEntityDOC_DOC & "){" _
                                         & "auxReturn='<img width=12px alt="""" src=imagenes/icon' + gNumber_pad(pType,8) + '.png />' + '<u style=color:Blue;><span style=cursor:pointer; onclick=hrcShowModal(""cfrmdocumentos1_det.aspx?_closea_=1&_mode_=0&param1=""+""' + pCod + '"") >" _
                                         & "'  + pDsc " _
                                         & "+ '</span></u>';" _
                                         & "}" _
                                         & "if (pType == " & 111111 & "){" _
                                         & "auxReturn=pDsc;" _
                                         & "}" _
                                         & "if (pType != " & 111111 & " && pType != " & enumEntities.coEntityDOC_DOC & "){" _
                                         & "var auxReturn = '<img width=12px alt="""" src=imagenes/icon' + gNumber_pad(pType,8) + '.png />' + pDsc;" _
                                         & "}" _
                                         & "return auxReturn;" _
                                         & "}"
                            auxFormatter = "griddata_formatcol0({#CURRENTROW_COD#},{#CURRENTROW_Q_DSC#},{#CURRENTROW_Q_TYPE#})"
                            prvGridData.gColumn_Add("Documento", 170, "q_dsc", clshrcGrdData.enumAlign.coLeft, False, auxFormatter, auxScript, False)

                            'Column 1: VERSION
                            auxScript = "function griddata_formatcol1(pVersion) {" _
                                & "var auxReturn = '&nbsp;';" _
                                 & "if (pVersion != ''){" _
                                & "auxReturn= pVersion;" _
                                & "}" _
                                & "return auxReturn;" _
                                & "}"
                            auxFormatter = "griddata_formatcol1({#CURRENTROW_VERSION#})"
                            prvGridData.gColumn_Add("Versión", 30, "VERSION", clshrcGrdData.enumAlign.coRight, False, auxFormatter, auxScript, False)

                            'Column 2: FECHA DE VERSION
                            auxScript = "function griddata_formatcol2(pFecha){" _
                                & "var auxReturn = '&nbsp;';" _
                                 & "if (pFecha != ''){" _
                                & "auxReturn = " & auxHtml.gJS_Date_ParseAndFormat("pFecha", Intelimedia.Hercules.Language.clsHrcCodeHTML.enumDateTimeFormat.coDate) & ";" _
                                & "}" _
                                & "return auxReturn;" _
                                & "}"
                            auxFormatter = "griddata_formatcol2({#CURRENTROW_FECHA#})"
                            prvGridData.gColumn_Add("Fecha de versión", 30, "FECHA", clshrcGrdData.enumAlign.coRight, False, auxFormatter, auxScript, False)

                            'Column 3: FECHA DE IMPRESION
                            auxScript = "function griddata_formatcol3(pFecha){" _
                                & "var auxReturn = '&nbsp;';" _
                                 & "if (pFecha != ''){" _
                                & "auxReturn = " & auxHtml.gJS_Date_ParseAndFormat("pFecha", Intelimedia.Hercules.Language.clsHrcCodeHTML.enumDateTimeFormat.coDate) & ";" _
                                & "}" _
                                & "return auxReturn;" _
                                & "}"
                            auxFormatter = "griddata_formatcol3({#CURRENTROW_FECHA_IMPRESION#})"
                            prvGridData.gColumn_Add("Fecha de impresión", 30, "FECHA_IMPRESION", clshrcGrdData.enumAlign.coRight, False, auxFormatter, auxScript, False)


                        Case 27
                            'Column oculta
                            prvGridData.gColumn_Add("Cod", 0, "q_cod", clshrcGrdData.enumAlign.coCenter, True, "", "", False)

                            auxScript = "function griddata_formatcol0(pCod,pDsc,pType) {" _
                                        & "var auxReturn = '&nbsp;';" _
                                         & "if (pType == " & enumEntities.coEntityDOC_DOC & "){" _
                                        & "auxReturn='<img width=12px alt="""" src=imagenes/icon' + gNumber_pad(pType,8) + '.png />' + '<u style=color:Blue;><span style=cursor:pointer; onclick=hrcShowModal(""cfrmdocumentos1_det.aspx?_closea_=1&_mode_=0&param1=""+""' + pCod + '"") >" _
                                        & "'  + pDsc " _
                                        & "+ '</span></u>';" _
                                        & "}" _
                                        & "if (pType == " & 111111 & "){" _
                                        & "auxReturn=pDsc;" _
                                        & "}" _
                                        & "if (pType != " & 111111 & " && pType != " & enumEntities.coEntityDOC_DOC & "){" _
                                        & "var auxReturn = '<img width=12px alt="""" src=imagenes/icon' + gNumber_pad(pType,8) + '.png />' + pDsc;" _
                                        & "}" _
                                        & "return auxReturn;" _
                                        & "}"
                            auxFormatter = "griddata_formatcol0({#CURRENTROW_COD#},{#CURRENTROW_Q_DSC#},{#CURRENTROW_Q_TYPE#})"
                            prvGridData.gColumn_Add("Documento", 170, "q_dsc", clshrcGrdData.enumAlign.coLeft, False, auxFormatter, auxScript, False)

                            'Column 1: VERSION
                            auxScript = "function griddata_formatcol1(pVersion) {" _
                                & "var auxReturn = '&nbsp;';" _
                                 & "if (pVersion != ''){" _
                                & "auxReturn= pVersion;" _
                                & "}" _
                                & "return auxReturn;" _
                                & "}"
                            auxFormatter = "griddata_formatcol1({#CURRENTROW_VERSION#})"
                            prvGridData.gColumn_Add("Versión", 30, "VERSION", clshrcGrdData.enumAlign.coRight, False, auxFormatter, auxScript, False)

                            'Column 3: COLABORADOR
                            auxScript = "function griddata_formatcol3(pColaborador,pCod,pType) {" _
                                & "var auxReturn = '&nbsp;';" _
                                & "if (pColaborador != ''){" _
                                & "auxReturn='<img width=12px alt="""" src=imagenes/icon' + gNumber_pad(pType,8) + '.png />' + '<u style=color:Blue;><span style=cursor:pointer; onclick=hrcShowModal(""frmColaboradores_det.aspx?_closea_=1&_mode_=0&param1=""+""' + pCod + '"") >" _
                                & "'  + pColaborador " _
                                & "}" _
                                & "return auxReturn;" _
                                & "}"
                            auxFormatter = "griddata_formatcol3({#CURRENTROW_COLABORADOR#},{#CURRENTROW_EMPCOD#},{#CURRENTROW_EMPTYPE#})"
                            prvGridData.gColumn_Add("Colaborador", 30, "COLABORADOR", clshrcGrdData.enumAlign.coLeft, False, auxFormatter, auxScript, False)

                            'Column 2: FECHA DE VERSION
                            auxScript = "function griddata_formatcol2(pFecha){" _
                                & "var auxReturn = '&nbsp;';" _
                                 & "if (pFecha != ''){" _
                                & "auxReturn = " & auxHtml.gJS_Date_ParseAndFormat("pFecha", Intelimedia.Hercules.Language.clsHrcCodeHTML.enumDateTimeFormat.coDate) & ";" _
                                & "}" _
                                & "return auxReturn;" _
                                & "}"
                            auxFormatter = "griddata_formatcol2({#CURRENTROW_FECHA#})"
                            prvGridData.gColumn_Add("Fecha", 30, "FECHA", clshrcGrdData.enumAlign.coRight, False, auxFormatter, auxScript, False)

                            'Column 4: OBSERVACION
                            auxScript = "function griddata_formatcol4(pObs) {" _
                                & "var auxReturn = '&nbsp;';" _
                                 & "if (pObs != ''){" _
                                & "auxReturn= pObs;" _
                                & "}" _
                                & "return auxReturn;" _
                                & "}"
                            auxFormatter = "griddata_formatcol4({#CURRENTROW_OBS#})"
                            prvGridData.gColumn_Add("Observación", 30, "OBS", clshrcGrdData.enumAlign.coLeft, False, auxFormatter, auxScript, False)

                            'Column 5: ESTADO
                            auxScript = "function griddata_formatcol5(pEstado) {" _
                                & "var auxReturn = '&nbsp;';" _
                                 & "if (pEstado != ''){" _
                                & "auxReturn= pEstado;" _
                                & "}" _
                                & "return auxReturn;" _
                                & "}"
                            auxFormatter = "griddata_formatcol5({#CURRENTROW_ESTADO#})"
                            prvGridData.gColumn_Add("Estado", 30, "ESTADO", clshrcGrdData.enumAlign.coLeft, False, auxFormatter, auxScript, False)

                        Case 28
                            'Column oculta
                            prvGridData.gColumn_Add("Cod", 0, "q_cod", clshrcGrdData.enumAlign.coCenter, True, "", "", False)

                            auxScript = "function griddata_formatcol0(pCod,pDsc,pType) {" _
                                        & "var auxReturn = '&nbsp;';" _
                                         & "if (pType == " & enumEntities.coEntityDOC_DOC & "){" _
                                        & "auxReturn='<img width=12px alt="""" src=imagenes/icon' + gNumber_pad(pType,8) + '.png />' + '<u style=color:Blue;><span style=cursor:pointer; onclick=hrcShowModal(""cfrmdocumentos1_det.aspx?_closea_=1&_mode_=0&param1=""+""' + pCod + '"") >" _
                                        & "'  + pDsc " _
                                        & "+ '</span></u>';" _
                                        & "}" _
                                        & "if (pType == " & 111111 & "){" _
                                        & "auxReturn=pDsc;" _
                                        & "}" _
                                        & "if (pType != " & 111111 & " && pType != " & enumEntities.coEntityDOC_DOC & "){" _
                                        & "var auxReturn = '<img width=12px alt="""" src=imagenes/icon' + gNumber_pad(pType,8) + '.png />' + pDsc;" _
                                        & "}" _
                                        & "return auxReturn;" _
                                        & "}"
                            auxFormatter = "griddata_formatcol0({#CURRENTROW_COD#},{#CURRENTROW_Q_DSC#},{#CURRENTROW_Q_TYPE#})"
                            prvGridData.gColumn_Add("Documento", 170, "q_dsc", clshrcGrdData.enumAlign.coLeft, False, auxFormatter, auxScript, False)

                            'Column 1: VERSION
                            auxScript = "function griddata_formatcol1(pVersion) {" _
                                & "var auxReturn = '&nbsp;';" _
                                 & "if (pVersion != ''){" _
                                & "auxReturn= pVersion;" _
                                & "}" _
                                & "return auxReturn;" _
                                & "}"
                            auxFormatter = "griddata_formatcol1({#CURRENTROW_VERSION#})"
                            prvGridData.gColumn_Add("Versión", 30, "VERSION", clshrcGrdData.enumAlign.coRight, False, auxFormatter, auxScript, False)

                            'Column 2: FECHA DE VERSION
                            auxScript = "function griddata_formatcol2(pFecha){" _
                                & "var auxReturn = '&nbsp;';" _
                                 & "if (pFecha != ''){" _
                                & "auxReturn = " & auxHtml.gJS_Date_ParseAndFormat("pFecha", Intelimedia.Hercules.Language.clsHrcCodeHTML.enumDateTimeFormat.coDate) & ";" _
                                & "}" _
                                & "return auxReturn;" _
                                & "}"
                            auxFormatter = "griddata_formatcol2({#CURRENTROW_FECHA#})"
                            prvGridData.gColumn_Add("Fecha", 30, "FECHA", clshrcGrdData.enumAlign.coRight, False, auxFormatter, auxScript, False)

                            'Column 5: ESTADO
                            auxScript = "function griddata_formatcol5(pEstado) {" _
                                & "var auxReturn = '&nbsp;';" _
                                 & "if (pEstado != ''){" _
                                & "auxReturn= pEstado;" _
                                & "}" _
                                & "return auxReturn;" _
                                & "}"
                            auxFormatter = "griddata_formatcol5({#CURRENTROW_ESTADO#})"
                            prvGridData.gColumn_Add("Estado", 30, "ESTADO", clshrcGrdData.enumAlign.coLeft, False, auxFormatter, auxScript, False)

                            'Column 3: COLABORADOR
                            auxScript = "function griddata_formatcol3(pColaborador,pCod,pType) {" _
                                & "var auxReturn = '&nbsp;';" _
                                & "if (pColaborador != ''){" _
                                & "auxReturn='<img width=12px alt="""" src=imagenes/icon' + gNumber_pad(pType,8) + '.png />' + '<u style=color:Blue;><span style=cursor:pointer; onclick=hrcShowModal(""frmColaboradores_det.aspx?_closea_=1&_mode_=0&param1=""+""' + pCod + '"") >" _
                                & "'  + pColaborador " _
                                & "}" _
                                & "return auxReturn;" _
                                & "}"
                            auxFormatter = "griddata_formatcol3({#CURRENTROW_COLABORADOR#},{#CURRENTROW_EMPCOD#},{#CURRENTROW_EMPTYPE#})"
                            prvGridData.gColumn_Add("Colaborador", 60, "COLABORADOR", clshrcGrdData.enumAlign.coLeft, False, auxFormatter, auxScript, False)

                            'Column 4: ACCION
                            auxScript = "function griddata_formatcol4(pAccion) {" _
                                & "var auxReturn = '&nbsp;';" _
                                 & "if (pAccion != ''){" _
                                & "auxReturn= pAccion;" _
                                & "}" _
                                & "return auxReturn;" _
                                & "}"
                            auxFormatter = "griddata_formatcol4({#CURRENTROW_ACCION#})"
                            prvGridData.gColumn_Add("Acción pendiente", 30, "ACCION", clshrcGrdData.enumAlign.coLeft, False, auxFormatter, auxScript, False)

                            'Column 6: FECHA DE INICIO
                            auxScript = "function griddata_formatcol6(pFecha){" _
                                & "var auxReturn = '&nbsp;';" _
                                 & "if (pFecha != ''){" _
                                & "auxReturn = " & auxHtml.gJS_Date_ParseAndFormat("pFecha", Intelimedia.Hercules.Language.clsHrcCodeHTML.enumDateTimeFormat.coDate) & ";" _
                                & "}" _
                                & "return auxReturn;" _
                                & "}"
                            auxFormatter = "griddata_formatcol6({#CURRENTROW_FECHAINICIO#})"
                            prvGridData.gColumn_Add("Fecha inicial de firma", 30, "FECHAINICIO", clshrcGrdData.enumAlign.coRight, False, auxFormatter, auxScript, False)

                            'Column 7: FECHA ULT MAIL
                            auxScript = "function griddata_formatcol7(pFecha){" _
                                & "var auxReturn = '&nbsp;';" _
                                 & "if (pFecha != ''){" _
                                & "auxReturn = " & auxHtml.gJS_Date_ParseAndFormat("pFecha", Intelimedia.Hercules.Language.clsHrcCodeHTML.enumDateTimeFormat.coDate) & ";" _
                                & "}" _
                                & "return auxReturn;" _
                                & "}"
                            auxFormatter = "griddata_formatcol7({#CURRENTROW_FECHAULTMAIL#})"
                            prvGridData.gColumn_Add("Fecha último mail enviado", 30, "FECHAULTMAIL", clshrcGrdData.enumAlign.coRight, False, auxFormatter, auxScript, False)

                            'Column 8: FECHA ULT MAIL OBS
                            auxScript = "function griddata_formatcol8(pFecha) {" _
                                & "var auxReturn = '&nbsp;';" _
                                 & "if (pFecha != ''){" _
                                & "auxReturn= pFecha;" _
                                & "}" _
                                & "return auxReturn;" _
                                & "}"
                            auxFormatter = "griddata_formatcol8({#CURRENTROW_ULTMAILOBS#})"
                            prvGridData.gColumn_Add("Observaciones envío", -1, "ULTMAILOBS", clshrcGrdData.enumAlign.coLeft, False, auxFormatter, auxScript, False)

                        Case 32

                            'Column oculta
                            prvGridData.gColumn_Add("Cod", 0, "q_cod", clshrcGrdData.enumAlign.coCenter, True, "", "", False)

                            auxScript = "function griddata_formatcol0(pDsc,pType) {" _
                                        & "var auxReturn = '&nbsp;';" _
                                         & "if (pType == " & enumEntities.coEntityDOC_DOCTIP & "){" _
                                        & "auxReturn='<img width=12px alt="""" src=imagenes/icon' + gNumber_pad(pType,8) + '.png />' + pDsc;" _
                                        & "}" _
                                        & "return auxReturn;" _
                                        & "}"
                            auxFormatter = "griddata_formatcol0({#CURRENTROW_Q_DSC#},{#CURRENTROW_Q_TYPE#})"
                            prvGridData.gColumn_Add("Tipo documento", 170, "q_dsc", clshrcGrdData.enumAlign.coLeft, False, auxFormatter, auxScript, False)

                            'Column 2: CREACION
                            auxScript = "function griddata_formatcol2(pCreacion,pCod) {" _
                                & "var auxReturn = '&nbsp;';" _
                                & "if (pCreacion != ''){" _
                                & "if (pCreacion > 0){" _
                                & "auxReturn= '<u style=color:Blue;><span style=cursor:pointer; onclick=hrcShowModal(""cfrmReportes.aspx?_view_=33&_mode_=0&_closea_=1&_report_=32&_wfwstepdoc_=" & enumWorkflowStep.coWFWSTPDOC_DOCCreacion & "&_idfiltros_=" & auxIDFiltros & "&_doctipcod_=""+""' + pCod + '"") >'  + pCreacion + '</span></u>';" _
                                & "}else{" _
                                & "auxReturn= pCreacion;" _
                                & "}}" _
                                & "return auxReturn;" _
                                & "}"
                            auxFormatter = "griddata_formatcol2({#CURRENTROW_CREACION#},{#CURRENTROW_COD#})"
                            prvGridData.gColumn_Add("Creación", 20, "CREACION", clshrcGrdData.enumAlign.coRight, False, auxFormatter, auxScript, False)


                            'Column 3: EDICION
                            auxScript = "function griddata_formatcol3(pEdicion,pCod) {" _
                                & "var auxReturn = '&nbsp;';" _
                                 & "if (pEdicion != ''){" _
                                & "if (pEdicion > 0){" _
                                & "auxReturn= '<u style=color:Blue;><span style=cursor:pointer; onclick=hrcShowModal(""cfrmReportes.aspx?_view_=33&_mode_=0&_closea_=1&_report_=32&_wfwstepdoc_=" & enumWorkflowStep.coWFWSTPDOC_DOCEdicionOK & "&_idfiltros_=" & auxIDFiltros & "&_doctipcod_=""+""' + pCod + '"") >'  + pEdicion + '</span></u>';" _
                                & "}else{" _
                                & "auxReturn= pEdicion;" _
                                & "}}" _
                                & "return auxReturn;" _
                                & "}"
                            auxFormatter = "griddata_formatcol3({#CURRENTROW_EDICION#},{#CURRENTROW_COD#})"
                            prvGridData.gColumn_Add("Edición", 20, "EDICION", clshrcGrdData.enumAlign.coRight, False, auxFormatter, auxScript, False)

                            'Column 4: REVISION
                            auxScript = "function griddata_formatcol4(pRevision,pCod) {" _
                                & "var auxReturn = '&nbsp;';" _
                                 & "if (pRevision != ''){" _
                                & "if (pRevision > 0){" _
                                & "auxReturn= '<u style=color:Blue;><span style=cursor:pointer; onclick=hrcShowModal(""cfrmReportes.aspx?_view_=33&_mode_=0&_closea_=1&_report_=32&_wfwstepdoc_=" & enumWorkflowStep.coWFWSTPDOC_DOCrevisionOK & "&_idfiltros_=" & auxIDFiltros & "&_doctipcod_=""+""' + pCod + '"") >'  + pRevision + '</span></u>';" _
                                & "}else{" _
                                & "auxReturn= pRevision;" _
                                & "}}" _
                                & "return auxReturn;" _
                                & "}"
                            auxFormatter = "griddata_formatcol4({#CURRENTROW_REVISION#},{#CURRENTROW_COD#})"
                            prvGridData.gColumn_Add("Revisión", 20, "REVISION", clshrcGrdData.enumAlign.coRight, False, auxFormatter, auxScript, False)

                            'Column 5: Publicacion
                            auxScript = "function griddata_formatcol5(pPublicacion,pCod) {" _
                                & "var auxReturn = '&nbsp;';" _
                                & "if (pPublicacion != ''){" _
                                & "if (pPublicacion > 0){" _
                                & "auxReturn= '<u style=color:Blue;><span style=cursor:pointer; onclick=hrcShowModal(""cfrmReportes.aspx?_view_=33&_mode_=0&_closea_=1&_report_=32&_wfwstepdoc_=" & enumWorkflowStep.coWFWSTPDOC_DOCpublicacionOK & "&_idfiltros_=" & auxIDFiltros & "&_doctipcod_=""+""' + pCod + '"") >'  + pPublicacion + '</span></u>';" _
                                & "}else{" _
                                & "auxReturn= pPublicacion;" _
                                & "}}" _
                                & "return auxReturn;" _
                                & "}"
                            auxFormatter = "griddata_formatcol5({#CURRENTROW_PUBLICACION#},{#CURRENTROW_COD#})"
                            prvGridData.gColumn_Add("Publicación", 20, "PUBLICACION", clshrcGrdData.enumAlign.coRight, False, auxFormatter, auxScript, False)

                            'Column 6: Lectura
                            auxScript = "function griddata_formatcol6(pLectura,pCod) {" _
                                & "var auxReturn = '&nbsp;';" _
                                 & "if (pLectura != ''){" _
                                 & "if (pLectura > 0){" _
                                & "auxReturn= '<u style=color:Blue;><span style=cursor:pointer; onclick=hrcShowModal(""cfrmReportes.aspx?_view_=33&_mode_=0&_closea_=1&_report_=32&_wfwstepdoc_=" & enumWorkflowStep.coWFWSTPDOC_DOCLecturaOK & "&_idfiltros_=" & auxIDFiltros & "&_doctipcod_=""+""' + pCod + '"") >'  + pLectura + '</span></u>';" _
                                & "}else{" _
                                & "auxReturn= pLectura;" _
                                & "}}" _
                                & "return auxReturn;" _
                                & "}"
                            auxFormatter = "griddata_formatcol6({#CURRENTROW_LECTURA#},{#CURRENTROW_COD#})"
                            prvGridData.gColumn_Add("Lectura", 20, "LECTURA", clshrcGrdData.enumAlign.coRight, False, auxFormatter, auxScript, False)

                        Case 28
                            'Column oculta
                            prvGridData.gColumn_Add("Cod", 0, "q_cod", clshrcGrdData.enumAlign.coCenter, True, "", "", False)

                            auxScript = "function griddata_formatcol0(pCod,pDsc,pType) {" _
                                        & "var auxReturn = '&nbsp;';" _
                                         & "if (pType == " & enumEntities.coEntityDOC_DOC & "){" _
                                        & "auxReturn='<img width=12px alt="""" src=imagenes/icon' + gNumber_pad(pType,8) + '.png />' + '<u style=color:Blue;><span style=cursor:pointer; onclick=hrcShowModal(""cfrmdocumentos1_det.aspx?_closea_=1&_mode_=0&param1=""+""' + pCod + '"") >" _
                                        & "'  + pDsc " _
                                        & "+ '</span></u>';" _
                                        & "}" _
                                        & "if (pType == " & 111111 & "){" _
                                        & "auxReturn=pDsc;" _
                                        & "}" _
                                        & "if (pType != " & 111111 & " && pType != " & enumEntities.coEntityDOC_DOC & "){" _
                                        & "var auxReturn = '<img width=12px alt="""" src=imagenes/icon' + gNumber_pad(pType,8) + '.png />' + pDsc;" _
                                        & "}" _
                                        & "return auxReturn;" _
                                        & "}"
                            auxFormatter = "griddata_formatcol0({#CURRENTROW_COD#},{#CURRENTROW_Q_DSC#},{#CURRENTROW_Q_TYPE#})"
                            prvGridData.gColumn_Add("Documento", 170, "q_dsc", clshrcGrdData.enumAlign.coLeft, False, auxFormatter, auxScript, False)

                            'Column 1: VERSION
                            auxScript = "function griddata_formatcol1(pVersion) {" _
                                & "var auxReturn = '&nbsp;';" _
                                 & "if (pVersion != ''){" _
                                & "auxReturn= pVersion;" _
                                & "}" _
                                & "return auxReturn;" _
                                & "}"
                            auxFormatter = "griddata_formatcol1({#CURRENTROW_VERSION#})"
                            prvGridData.gColumn_Add("Versión", 30, "VERSION", clshrcGrdData.enumAlign.coRight, False, auxFormatter, auxScript, False)

                            'Column 2: FECHA DE VERSION
                            auxScript = "function griddata_formatcol2(pFecha){" _
                                & "var auxReturn = '&nbsp;';" _
                                 & "if (pFecha != ''){" _
                                & "auxReturn = " & auxHtml.gJS_Date_ParseAndFormat("pFecha", Intelimedia.Hercules.Language.clsHrcCodeHTML.enumDateTimeFormat.coDate) & ";" _
                                & "}" _
                                & "return auxReturn;" _
                                & "}"
                            auxFormatter = "griddata_formatcol2({#CURRENTROW_FECHA#})"
                            prvGridData.gColumn_Add("Fecha", 30, "FECHA", clshrcGrdData.enumAlign.coRight, False, auxFormatter, auxScript, False)

                            'Column 5: ESTADO
                            auxScript = "function griddata_formatcol5(pEstado) {" _
                                & "var auxReturn = '&nbsp;';" _
                                 & "if (pEstado != ''){" _
                                & "auxReturn= pEstado;" _
                                & "}" _
                                & "return auxReturn;" _
                                & "}"
                            auxFormatter = "griddata_formatcol5({#CURRENTROW_ESTADO#})"
                            prvGridData.gColumn_Add("Estado", 30, "ESTADO", clshrcGrdData.enumAlign.coLeft, False, auxFormatter, auxScript, False)

                            'Column 3: COLABORADOR
                            auxScript = "function griddata_formatcol3(pColaborador,pCod,pType) {" _
                                & "var auxReturn = '&nbsp;';" _
                                & "if (pColaborador != ''){" _
                                & "auxReturn='<img width=12px alt="""" src=imagenes/icon' + gNumber_pad(pType,8) + '.png />' + '<u style=color:Blue;><span style=cursor:pointer; onclick=hrcShowModal(""frmColaboradores_det.aspx?_closea_=1&_mode_=0&param1=""+""' + pCod + '"") >" _
                                & "'  + pColaborador " _
                                & "}" _
                                & "return auxReturn;" _
                                & "}"
                            auxFormatter = "griddata_formatcol3({#CURRENTROW_COLABORADOR#},{#CURRENTROW_EMPCOD#},{#CURRENTROW_EMPTYPE#})"
                            prvGridData.gColumn_Add("Colaborador", 60, "COLABORADOR", clshrcGrdData.enumAlign.coLeft, False, auxFormatter, auxScript, False)

                            'Column 4: ACCION
                            auxScript = "function griddata_formatcol4(pAccion) {" _
                                & "var auxReturn = '&nbsp;';" _
                                 & "if (pAccion != ''){" _
                                & "auxReturn= pAccion;" _
                                & "}" _
                                & "return auxReturn;" _
                                & "}"
                            auxFormatter = "griddata_formatcol4({#CURRENTROW_ACCION#})"
                            prvGridData.gColumn_Add("Acción pendiente", 30, "ACCION", clshrcGrdData.enumAlign.coLeft, False, auxFormatter, auxScript, False)

                            'Column 6: FECHA DE INICIO
                            auxScript = "function griddata_formatcol6(pFecha){" _
                                & "var auxReturn = '&nbsp;';" _
                                 & "if (pFecha != ''){" _
                                & "auxReturn = " & auxHtml.gJS_Date_ParseAndFormat("pFecha", Intelimedia.Hercules.Language.clsHrcCodeHTML.enumDateTimeFormat.coDate) & ";" _
                                & "}" _
                                & "return auxReturn;" _
                                & "}"
                            auxFormatter = "griddata_formatcol6({#CURRENTROW_FECHAINICIO#})"
                            prvGridData.gColumn_Add("Fecha inicial de firma", 30, "FECHAINICIO", clshrcGrdData.enumAlign.coRight, False, auxFormatter, auxScript, False)

                            'Column 7: FECHA ULT MAIL
                            auxScript = "function griddata_formatcol7(pFecha){" _
                                & "var auxReturn = '&nbsp;';" _
                                 & "if (pFecha != ''){" _
                                & "auxReturn = " & auxHtml.gJS_Date_ParseAndFormat("pFecha", Intelimedia.Hercules.Language.clsHrcCodeHTML.enumDateTimeFormat.coDate) & ";" _
                                & "}" _
                                & "return auxReturn;" _
                                & "}"
                            auxFormatter = "griddata_formatcol7({#CURRENTROW_FECHAULTMAIL#})"
                            prvGridData.gColumn_Add("Fecha último mail enviado", 30, "FECHAULTMAIL", clshrcGrdData.enumAlign.coRight, False, auxFormatter, auxScript, False)

                            'Column 8: FECHA ULT MAIL OBS
                            auxScript = "function griddata_formatcol8(pFecha) {" _
                                & "var auxReturn = '&nbsp;';" _
                                 & "if (pFecha != ''){" _
                                & "auxReturn= pFecha;" _
                                & "}" _
                                & "return auxReturn;" _
                                & "}"
                            auxFormatter = "griddata_formatcol8({#CURRENTROW_ULTMAILOBS#})"
                            prvGridData.gColumn_Add("Observaciones envío", -1, "ULTMAILOBS", clshrcGrdData.enumAlign.coLeft, False, auxFormatter, auxScript, False)

                        Case 34
                            'Column oculta
                            prvGridData.gColumn_Add("Cod", 0, "q_cod", clshrcGrdData.enumAlign.coCenter, True, "", "", False)

                            auxScript = "function griddata_formatcol0(pCod,pDsc,pType) {" _
                                        & "var auxReturn = '&nbsp;';" _
                                         & "if (pType == " & enumEntities.coEntityDOC_DOC & "){" _
                                        & "auxReturn='<img width=12px alt="""" src=imagenes/icon' + gNumber_pad(pType,8) + '.png />' + '<u style=color:Blue;><span style=cursor:pointer; onclick=hrcShowModal(""cfrmdocumentos1_det.aspx?_closea_=1&_mode_=0&param1=""+""' + pCod + '"") >" _
                                        & "'  + pDsc " _
                                        & "+ '</span></u>';" _
                                        & "}" _
                                        & "return auxReturn;" _
                                        & "}"
                            auxFormatter = "griddata_formatcol0({#CURRENTROW_COD#},{#CURRENTROW_DSC#},{#CURRENTROW_Q_TYPE#})"
                            prvGridData.gColumn_Add("Documento", -1, "q_dsc", clshrcGrdData.enumAlign.coLeft, False, auxFormatter, auxScript, False)

                            'Column 1: VERSION
                            auxScript = "function griddata_formatcol1(pVersion) {" _
                                & "var auxReturn = '&nbsp;';" _
                                 & "if (pVersion != ''){" _
                                & "auxReturn= pVersion;" _
                                & "}" _
                                & "return auxReturn;" _
                                & "}"
                            auxFormatter = "griddata_formatcol1({#CURRENTROW_VERSION#})"
                            prvGridData.gColumn_Add("Versión", 10, "VERSION", clshrcGrdData.enumAlign.coRight, False, auxFormatter, auxScript, False)


                            'Column 5: ESTADO
                            auxScript = "function griddata_formatcol5(pEstado) {" _
                                & "var auxReturn = '&nbsp;';" _
                                 & "if (pEstado != ''){" _
                                & "auxReturn= pEstado;" _
                                & "}" _
                                & "return auxReturn;" _
                                & "}"
                            auxFormatter = "griddata_formatcol5({#CURRENTROW_WFWSTPDSC#})"
                            prvGridData.gColumn_Add("Estado", 10, "ESTADO", clshrcGrdData.enumAlign.coLeft, False, auxFormatter, auxScript, False)


                            'Column 4: ACCION
                            auxScript = "function griddata_formatcol4(pID) {" _
                                & "var auxReturn = '&nbsp;';" _
                                 & "if (pID != ''){" _
                                & "auxReturn= pID;" _
                                & "}" _
                                & "return auxReturn;" _
                                & "}"
                            auxFormatter = "griddata_formatcol4({#CURRENTROW_IDENTIFICADOR#})"
                            prvGridData.gColumn_Add("Identificador actual", 10, "IDENTIFICADOR", clshrcGrdData.enumAlign.coLeft, False, auxFormatter, auxScript, False)


                            'Column 4: ACCION
                            auxScript = "function griddata_formatcol5(pID) {" _
                                & "var auxReturn = '&nbsp;';" _
                                 & "if (pID != ''){" _
                                & "auxReturn= pID;" _
                                & "}" _
                                & "return auxReturn;" _
                                & "}"
                            auxFormatter = "griddata_formatcol5({#CURRENTROW_NEW_IDENTIFICADOR#})"
                            prvGridData.gColumn_Add("Identificador nuevo", 10, "NEW_IDENTIFICADOR", clshrcGrdData.enumAlign.coLeft, False, auxFormatter, auxScript, False)
                        Case Else
                            If m_view = 31 Then

                                'Column oculta
                                prvGridData.gColumn_Add("Cod", 0, "q_cod", clshrcGrdData.enumAlign.coCenter, True, "", "", False)

                                auxScript = "function griddata_formatcol0(pCod,pDsc,pType) {" _
                                            & "var auxReturn = '&nbsp;';" _
                                             & "if (pType == " & enumEntities.coEntityEMP & "){" _
                                            & "auxReturn='<img width=12px alt="""" src=imagenes/icon' + gNumber_pad(pType,8) + '.png />' + '<u style=color:Blue;><span style=cursor:pointer; onclick=hrcShowModal(""frmColaboradores_det.aspx?_closea_=1&_mode_=0&param1=""+""' + pCod + '"") >" _
                                            & "'  + pDsc " _
                                            & "+ '</span></u>';" _
                                            & "}" _
                                            & "return auxReturn;" _
                                            & "}"
                                auxFormatter = "griddata_formatcol0({#CURRENTROW_COD#},{#CURRENTROW_Q_DSC#},{#CURRENTROW_Q_TYPE#})"
                                prvGridData.gColumn_Add("Colaborador", 70, "q_dsc", clshrcGrdData.enumAlign.coLeft, False, auxFormatter, auxScript, False)


                                'Column 2: VERSION
                                auxScript = "function griddata_formatcol2(pVersion) {" _
                                    & "var auxReturn = '&nbsp;';" _
                                     & "if (pVersion != ''){" _
                                    & "auxReturn= pVersion;" _
                                    & "}" _
                                    & "return auxReturn;" _
                                    & "}"
                                auxFormatter = "griddata_formatcol2({#CURRENTROW_VERSION#})"
                                prvGridData.gColumn_Add("Versión", 10, "VERSION", clshrcGrdData.enumAlign.coRight, False, auxFormatter, auxScript, False)

                                'Column 3: FECHA
                                auxScript = "function griddata_formatcol3(pFecha) {" _
                                    & "var auxReturn = '&nbsp;';" _
                                     & "if (pFecha != ''){" _
                                    & "auxReturn = " & auxHtml.gJS_Date_ParseAndFormat("pFecha", Intelimedia.Hercules.Language.clsHrcCodeHTML.enumDateTimeFormat.coDate) & ";" _
                                    & "}" _
                                    & "return auxReturn;" _
                                    & "}"
                                auxFormatter = "griddata_formatcol3({#CURRENTROW_QSECDATETIME#})"
                                prvGridData.gColumn_Add("Fecha", 30, "FECHA", clshrcGrdData.enumAlign.coRight, False, auxFormatter, auxScript, False)

                                'Column 4: UNIDAD
                                auxScript = "function griddata_formatcol4(pUnidad,pType) {" _
                                    & "var auxReturn = '&nbsp;';" _
                                     & "if (pUnidad != ''){" _
                                    & "auxReturn='<img width=12px alt="""" src=imagenes/icon' + gNumber_pad(pType,8) + '.png />' + pUnidad;" _
                                    & "}" _
                                    & "return auxReturn;" _
                                    & "}"
                                auxFormatter = "griddata_formatcol4({#CURRENTROW_UNDDSC#},{#CURRENTROW_UNDTYPE#})"
                                prvGridData.gColumn_Add("Unidad", 50, "UNIDAD", clshrcGrdData.enumAlign.coLeft, False, auxFormatter, auxScript, False)

                            ElseIf m_view = 33 Then

                                'Column oculta
                                prvGridData.gColumn_Add("Cod", 0, "q_cod", clshrcGrdData.enumAlign.coCenter, True, "", "", False)

                                auxScript = "function griddata_formatcol0(pCod,pDsc,pType) {" _
                                            & "var auxReturn = '&nbsp;';" _
                                             & "if (pType == " & enumEntities.coEntityDOC_DOC & "){" _
                                            & "auxReturn='<img width=12px alt="""" src=imagenes/icon' + gNumber_pad(pType,8) + '.png />' + '<u style=color:Blue;><span style=cursor:pointer; onclick=hrcShowModal(""cfrmdocumentos1_det.aspx?_closea_=1&_mode_=0&param1=""+""' + pCod + '"") >" _
                                            & "'  + pDsc " _
                                            & "+ '</span></u>';" _
                                            & "}" _
                                            & "return auxReturn;" _
                                            & "}"
                                auxFormatter = "griddata_formatcol0({#CURRENTROW_COD#},{#CURRENTROW_Q_DSC#},{#CURRENTROW_Q_TYPE#})"
                                prvGridData.gColumn_Add("Documento", 70, "q_dsc", clshrcGrdData.enumAlign.coLeft, False, auxFormatter, auxScript, False)

                                'Column 1: VERSION
                                auxScript = "function griddata_formatcol1(pVersion) {" _
                                    & "var auxReturn = '&nbsp;';" _
                                     & "if (pVersion != ''){" _
                                    & "auxReturn= pVersion;" _
                                    & "}" _
                                    & "return auxReturn;" _
                                    & "}"
                                auxFormatter = "griddata_formatcol1({#CURRENTROW_VERSION#})"
                                prvGridData.gColumn_Add("Versión", 30, "VERSION", clshrcGrdData.enumAlign.coRight, False, auxFormatter, auxScript, False)

                                'Column 2: FECHA DE VERSION
                                auxScript = "function griddata_formatcol2(pFecha){" _
                                    & "var auxReturn = '&nbsp;';" _
                                     & "if (pFecha != ''){" _
                                    & "auxReturn = " & auxHtml.gJS_Date_ParseAndFormat("pFecha", Intelimedia.Hercules.Language.clsHrcCodeHTML.enumDateTimeFormat.coDate) & ";" _
                                    & "}" _
                                    & "return auxReturn;" _
                                    & "}"
                                auxFormatter = "griddata_formatcol2({#CURRENTROW_FECHA#})"
                                prvGridData.gColumn_Add("Fecha de versión", 30, "FECHA", clshrcGrdData.enumAlign.coRight, False, auxFormatter, auxScript, False)

                            End If

                    End Select



                    If auxDT.Rows.Count < 100 Then
                        prvGridData.ExpandAllAfterLoad = True
                    End If

                    prvGridData.TreeGridExpandColapaseAllButton = True
                    'prvGridData.GridHeight = "100px"

                    auxScript = prvGridData.gControl_GetStartupScripts & ";"
                    ScriptManager.RegisterStartupScript(Me, Me.GetType, "hrc" & prvGridData.ControlID, _
                                                       auxScript, True)

                    lblGrid.Text = prvGridData.gControl_GetBodyDefinition

                    auxClientCon.gObjectTmp_Upload(prvGridData, auxGridCacheID)

                Else
                    lblerror.Visible = True
                    lblerror.Text = "Datos no encontrados"
                    lblerror.ForeColor = Drawing.Color.Black
                End If
            Else
                lblerror.Visible = True
                lblerror.Text = "Se han encontrado errores en la generación del reporte, solicite ayuda al administrador."
                lblerror.ForeColor = Drawing.Color.Red
            End If
        Else
            lblerror.Visible = True
            lblerror.Text = auxErrorDescripcion
            lblerror.ForeColor = Drawing.Color.Black
        End If

        auxClass.Conn.gConn_Close()
        updupdpnlResults.Update()
        'updupdpnlSearch.Update()

        Return auxDT
    End Function






    Protected Sub cmdLimpiar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdLimpiar.Click


        txtFechaDesde.Text = ""
        txtFechaHasta.Text = ""
        cmbIndicador.ClearSelection()

        txtTituloDocumento.Text = ""
        cmbAgrupacion.ClearSelection()

        updupdpnlSearch.Update()
        updupdpnlResults.Update()
    End Sub

    Protected Sub cmdRefrescar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdRefrescar.Click, cmdBuscar.Click

        If cmbView.SelectedValue < 21 Then
            gData_Get()
        Else
            gData_Get_Buscar()
        End If

    End Sub

    Private Function gData_Get() As DataTable
        'Dim auxView As Short = 0
        Dim auxChart As Chart
        'Dim auxFilter As Boolean = False
        'If txtfechadesde.Text <> "" Or txtfechaHasta.Text <> "" Or Val(hdnproproponeund.Value) > 0 Then
        '    auxFilter = True
        'End If

        Dim auxReturn As DataTable = Nothing
        Dim auxIndicador As Integer = 1 'auxindicador
        Dim auxSelectString As String = ""
        Dim auxClass As New clsCusimDOC
        auxClass.gConn_Open()
        Dim auxHrcSesID As String = ""
        If Request.QueryString("_sesid_") IsNot Nothing Then
            auxHrcSesID = Request.QueryString("_sesid_").Trim
        End If


        Dim auxFechaDesde As Date
        If txtFechaDesde.Visible Then
            auxFechaDesde = auxClass.Conn.gField_GetDate(txtFechaDesde.Text)
        End If

        Dim auxFechaHasta As Date
        If txtFechaHasta.Visible Then
            auxFechaHasta = auxClass.Conn.gField_GetDate(txtFechaHasta.Text)
        End If

        'Dim auxFechaDesde As Date = auxClass.Conn.gField_GetDate(txtFechaDesde.Text)
        'Dim auxFechaHasta As Date = auxClass.Conn.gField_GetDate(txtFechaHasta.Text)

        lblTitle.Text = cmbView.SelectedValue.ToString ' Título del Reporte
        ' Los filtros, van como subtítulos.
        Dim auxGeneralWhere As String = ""
        lblReportSubTitle.Text = "<br />"
        If auxFechaDesde <> Nothing Or auxFechaHasta <> Nothing Then
            If auxFechaDesde <> Nothing Then
                lblReportSubTitle.Text &= "Desde: " & auxFechaDesde.ToShortDateString & "."
                auxGeneralWhere &= " AND DOC_DOC.fecha >= " & auxClass.Conn.gField_GetDate(auxFechaDesde)
            End If
            If auxFechaHasta <> Nothing Then
                lblReportSubTitle.Text &= "Hasta: " & auxFechaHasta.ToShortDateString & "."
                auxGeneralWhere &= " AND DOC_DOC.fecha <= " & auxClass.Conn.gField_GetDate(auxFechaHasta).AddHours(23).AddMinutes(59).AddSeconds(59)
            End If
            lblReportSubTitle.Text &= "<br />"
        End If
        'lblReportSubTitle.Text &= IIf(hdnproproponeund.Value <> -1, "Unidad: " & lblund.Text & "<br />", "") _
        '                                & "Indicador: " & auxIndicador & IIf(cmbGroup.Enabled = True, " - Grupo: " & cmbGroup.SelectedItem.ToString, "") _
        '                                & "<br />" & "<br />"


        If Session("isadmin") = False Then
            auxGeneralWhere &= " AND DOC_DOC.qsidcod IN (" _
                & auxClass.Sec.gSID_GetQueryAccessFromAcctype(-1, glPermRead) & ")"
        End If
        Dim auxSerieName As String = ""
        'Forma de agrupacion
        Dim auxSeries As New List(Of String)
        Dim auxGroupBy As String = "STR(DATEPART(year,DOC_DOC.fecha), 4) +   STR(DATEPART(month, DOC_DOC.fecha), 2)"
        auxGroupBy = "Q_WFWSTP.wfwstpdsc"
        If m_view < 1 Then
            If cmbView.SelectedValue IsNot Nothing Then
                m_view = cmbView.SelectedValue
            End If
        End If

        Dim auxValue As String = ""

        Select Case auxIndicador
            Case Else
                auxValue = "ISNULL(COUNT(*),0)"
        End Select

        Dim auxWhere As String = ""
        Dim auxQuery As String = ""
        Dim auxGraphicCount As Short = 0
        drwGraphics.Controls.Clear()
        Select Case m_view
            Case 1, 2, 3, 4, 5
                Dim auxDTLevel1() As DataRow = Nothing
                Select Case m_view
                    Case 1  'Unidad especifica
                        'Dim auxUndCod As Integer = -1
                        'If Val(hdnproproponeund.Value) > 0 Then
                        '    auxUndCod = auxClass.Conn.gField_GetInt(hdnproproponeund.Value, 0)
                        'End If
                        gAutosugerido_Download()
                        If auxUnidadDocumentoCodList.Count > 0 Then
                            auxDTLevel1 = hrcEntityDT_UND.Select("cod IN (" & auxClass.Conn.gFieldDB_GetString(auxClass.gEntity_UND_GetChilds(auxUnidadDocumentoCodList)) & ")")
                        Else
                            auxDTLevel1 = hrcEntityDT_UND.Select
                        End If
                    Case 2
                        auxQuery = "SELECT cod,dsc FROM DOC_DOCTIP WHERE cod > 0"
                        auxDTLevel1 = auxClass.Conn.gConn_Query(auxQuery).Select
                    Case 3 'proceso
                        auxQuery = "SELECT cod,dsc FROM DOC_PRO WHERE cod > 0"
                        auxDTLevel1 = auxClass.Conn.gConn_Query(auxQuery).Select
                    Case 4 'clasificacion
                        auxQuery = "SELECT cod,dsc FROM DOC_CLA WHERE cod > 0"
                        auxDTLevel1 = auxClass.Conn.gConn_Query(auxQuery).Select
                    Case 5 'Sistema
                        auxQuery = "SELECT cod,dsc FROM DOC_SIS WHERE cod > 0"
                        auxDTLevel1 = auxClass.Conn.gConn_Query(auxQuery).Select
                End Select
                For Each auxRow As DataRow In auxDTLevel1
                    auxSelectString = ""
                    Select Case m_view
                        Case 1  'por unidad especifica
                            auxGeneralWhere = " AND DOC_DOC.cod IN (SELECT doccod FROM DOC_DOCUND WHERE undcod =" & auxRow("cod") & ")"
                        Case 2  'tipo de doc
                            auxGeneralWhere = " AND DOC_DOC.doctipcod=" & auxRow("cod")
                        Case 3  'PROCESO
                            auxGeneralWhere = " AND DOC_DOC.procod=" & auxRow("cod")
                        Case 4 'clasificacion
                            auxGeneralWhere = " AND DOC_DOC.clacod =" & auxRow("cod")
                        Case 5 'SISTEMA
                            auxGeneralWhere = " AND DOC_DOC.siscod  =" & auxRow("cod")
                    End Select
                    auxSelectString &= "SELECT " & auxGroupBy & " AS periodo," & auxValue & " AS [Cant por estado]" _
                     & " FROM DOC_DOC " _
                     & " LEFT JOIN Q_WFWSTP ON DOC_DOC.wfwstatus=Q_WFWSTP.wfwstpcod" _
                     & " WHERE (cod > 0) " _
                     & auxGeneralWhere _
                     & "GROUP BY " & auxGroupBy
                    Dim auxDT As DataTable = auxClass.Conn.gConn_Query(auxSelectString)

                    auxChart = gGraphics_Draw(auxRow("dsc"), auxDT)
                    If auxChart IsNot Nothing Then
                        Dim auxLabel As New Label
                        If (auxGraphicCount Mod 2) = 0 And auxGraphicCount <> 0 Then
                            auxLabel.Text &= "<br />"
                        End If
                        auxGraphicCount += 1
                        Dim auxMS As New IO.MemoryStream
                        auxChart.SaveImage(auxMS, ChartImageFormat.Png)
                        Dim auxCacheID As String = Guid.NewGuid.ToString
                        auxLabel.Text &= "<img src=""hrcbinaries.aspx?&tmp=1&isimage=1&tmpid=" & auxCacheID & """ />"
                        drwGraphics.Controls.Add(auxLabel)
                        Dim auxConnection As New imClientConnection
                        auxConnection.gFileTmp_Upload("grafico.jpg", auxMS.ToArray, -1, -1, auxCacheID)
                    End If
                Next

                auxChart = Nothing
                If auxChart IsNot Nothing Then
                    Dim auxLabel As New Label
                    If (auxGraphicCount Mod 2) = 0 And auxGraphicCount <> 0 Then
                        auxLabel.Text &= "<br />"
                    End If
                    auxGraphicCount += 1
                    Dim auxMS As New IO.MemoryStream
                    auxChart.SaveImage(auxMS, ChartImageFormat.Png)
                    Dim auxCacheID As String = Guid.NewGuid.ToString
                    auxLabel.Text &= "<img src=""hrcbinaries.aspx?&tmp=1&isimage=1&tmpid=" & auxCacheID & """ />"
                    drwGraphics.Controls.Clear()
                    drwGraphics.Controls.Add(auxLabel)
                    Dim auxConnection As New imClientConnection
                    auxConnection.gFileTmp_Upload("grafico.jpg", auxMS.ToArray, -1, -1, auxCacheID)
                End If

            Case 6 'Roles del documento
                lblTitle.Text = "Audiencia"
                imgicon.Src = "imagenes/actaudience.png"
                Dim auxDT2 As DataTable = auxClass.Conn.gConn_Query("SELECT cod as q_cod,dsc as q_dsc,NULL,NULL," & enumEntities.coEntityDOC_ROL & " FROM DOC_ROL WHERE cod > 0 ORDER BY orden")
                Dim auxtrocod As Integer = auxClass.Conn.gConn_QueryValueInt("SELECT trocod FROM DOC_DOC" _
                                                                       & " WHERE cod = " & m_DocCod)
                Dim auxDTroles As DataTable = auxClass.gTRO_Get(pTroCod:=auxtrocod)

                For Each auxRol As enumRoles In [Enum].GetValues(GetType(enumRoles))
                    auxClass.gTRO_ResolveThisRol(auxDTroles, auxRol, False, -1)
                Next
                auxDTroles = auxClass.gDTroles_Resolve(auxDTroles, -1)
                Dim auxDT1 As New DataTable
                auxDT1.Columns.Add(New DataColumn("q_cod", Type.GetType("System.Int32")))
                auxDT1.Columns.Add(New DataColumn("q_dsc", Type.GetType("System.String")))
                auxDT1.Columns.Add(New DataColumn("q_lev1", Type.GetType("System.Int32")))
                auxDT1.Columns.Add(New DataColumn("q_lev2", Type.GetType("System.Int32")))
                auxDT1.Columns.Add(New DataColumn("q_type", Type.GetType("System.Int32")))
                auxDT1.Columns.Add(New DataColumn("cod", Type.GetType("System.Int32")))
                Dim auxMemberCod As Integer
                Dim auxNewRow As DataRow

                For Each auxRowRol As DataRow In auxDT2.Rows
                    Dim auxLoginList As New List(Of Integer)
                    For Each auxRow As DataRow In auxDTroles.Select("rolcod=" & auxRowRol("q_cod"))
                        auxMemberCod = auxClass.Conn.gField_GetInt(auxRow("membersidcod"))
                        Select Case CType(auxRow("docmbrtype"), enumEntities)
                            Case enumEntities.coEntityEMP
                                If auxLoginList.IndexOf(auxMemberCod) = -1 Then
                                    auxLoginList.Add(auxMemberCod)
                                End If

                            Case enumEntities.coEntityDOC_EQU, enumEntities.coEntityUND
                                For Each auxMemberRow As DataRow In auxClass.Sec.gGroup_ResolveLogins(auxMemberCod).Rows
                                    If auxLoginList.IndexOf(auxMemberCod) = -1 Then
                                        auxLoginList.Add(auxMemberRow("seccod"))
                                    End If
                                Next

                            Case enumEntities.coEntityDOC_DYNGRP
                                '  Dim auxDTDYNGRP As DataTable = auxClass.gTRO_Get(auxDTroles, 1, "")
                                
                        End Select
                    Next
                    Select Case auxRowRol("q_cod")
                        Case enumRoles.coVisualizador
                            'Especificoa-Visualizadores
                            For Each auxUndRow As DataRow In auxClass.Conn.gConn_Query("SELECT miembrosgrpcod FROM DOC_DOCUND " _
                                                                                               & " LEFT JOIN UND ON DOC_DOCUND.undcod=UND.cod" _
                                                                                               & " WHERE doccod = " & m_DocCod _
                                                                                               & " AND miembrosgrpcod > 0").Rows
                                auxMemberCod = auxClass.Conn.gField_GetInt(auxUndRow(0))
                                For Each auxMemberRow As DataRow In auxClass.Sec.gGroup_ResolveLogins(auxMemberCod).Rows
                                    If auxLoginList.IndexOf(auxMemberCod) = -1 Then
                                        auxLoginList.Add(auxMemberRow("seccod"))
                                    End If
                                Next
                            Next
                        Case Else
                            
                    End Select


                    For Each auxEmpRow As DataRow In auxClass.Conn.gConn_Query("SELECT cod,dsc FROM EMP " _
                                                    & " WHERE (baja {#ISNULL#} OR baja = {#FALSE#}) " _
                                                    & " AND seccod IN (" & auxClass.Conn.gFieldDB_GetString(auxLoginList) & ")" _
                                                    & " ORDER BY dsc").Rows
                        auxNewRow = auxDT1.NewRow
                        auxNewRow("q_cod") = auxDT1.Rows.Count + 1
                        auxNewRow("q_dsc") = auxEmpRow("DSC")
                        auxNewRow("q_lev2") = auxRowRol("q_cod")
                        auxNewRow("q_type") = enumEntities.coEntityEMP
                        auxNewRow("cod") = auxEmpRow("cod")
                        auxDT1.Rows.Add(auxNewRow)
                    Next
                Next

                Dim auxDT As DataTable = Nothing
                auxDT = gData_get2(auxDT1, auxDT2)
                gData_get3(auxDT)
                'Return auxDT
        End Select

        If auxHrcSesID <> "" Then
            auxClass.Sec.gLogin_SessionLogoff(auxHrcSesID)
        End If
        auxClass.Conn.gConn_Close()

        'updupdpnlResults.Update()
        Return auxReturn
    End Function
    Private Function gData_get2(ByVal auxDT1 As DataTable, ByVal auxDT2 As DataTable) As DataTable
        Dim auxClass As New clsCusimDOC
        auxClass.Conn.gConn_Open()
        Dim auxDT As DataTable
        Dim auxHierarchy As New clsHrcHierarchyTable(auxClass.Conn, "")
        auxHierarchy.gGroup_Add(auxDT1)
        auxHierarchy.gGroup_Add(auxDT2)

        auxDT = auxHierarchy.gHierarchy_GenerateTable

        If auxDT.Rows.Count <> 0 Then
            Dim auxGrdData As New clshrcGrdData("", "", "", "", Nothing, "", False, True)
            auxGrdData.gTreeGrid_Enabled("level", "parent", "isleaf", "expanded")
            auxDT = auxGrdData.gDataTable_Prepare(auxDT)
        End If
        auxClass.Conn.gConn_Close()
        Return auxDT
    End Function
    Private Sub gData_get3(ByVal auxDT As DataTable)

        If auxDT.Rows.Count > 0 Then
            Dim auxClientCon As New imClientConnection
            Dim auxGridCacheID As String = ""
            auxGridCacheID = ViewState("griddata2")
            Dim auxConn As clsHrcConnClient = Session("conn")
            Dim auxGridData As clshrcGrdData
            If auxGridCacheID Is Nothing Then
                auxGridCacheID = auxConn.gField_GetUniqueID
                ViewState("griddata2") = auxGridCacheID
            Else
                auxGridData = auxClientCon.gObjectTmp_Download(auxGridCacheID)
            End If


            auxGridData = New clshrcGrdData("grddata2", auxGridCacheID, "", _
                                                 "hrcgrdData.ashx", _
                                                    auxDT, "", False, False)

            If cmbView.SelectedValue <> 20 Then
                auxGridData.gTreeGrid_Enabled("level", "parent", "isleaf", "expanded")
            End If

            auxGridData.gPager_EnableVirtual()
            Dim auxScript As String = ""
            Dim auxFormatter As String = ""
            Dim auxHtml As New Intelimedia.Hercules.Language.clsHrcCodeHTML


            'Column oculta
            auxGridData.gColumn_Add("Cod", 0, "q_cod", clshrcGrdData.enumAlign.coCenter, True, "", "", False)
            auxGridData.gColumn_AddAsHidden("CODIGO")


            'Column 0: requerimiento
            auxScript = "function griddata_formatcol0(pDsc,pType) {" _
                & "var auxReturn = '&nbsp;';" _
                & "if (pDsc != ''){" _
                & "auxReturn='<img width=12px alt="""" src=imagenes/icon' + gNumber_pad(pType,8) + '.png />' + pDsc;" _
                & "}" _
                & "return auxReturn;" _
                & "}"
            auxFormatter = "griddata_formatcol0({#CURRENTROW_Q_DSC#},{#CURRENTROW_Q_TYPE#})"
            auxGridData.gColumn_Add("Rol", -1, "q_dsc", clshrcGrdData.enumAlign.coLeft, False, auxFormatter, auxScript, True)


            auxGridData.TreeGridExpandColapaseAllButton = True
            'prvGridData.GridHeight = "100px"

            auxScript = auxGridData.gControl_GetStartupScripts & ";"
            ScriptManager.RegisterStartupScript(Me, Me.GetType, "hrc" & auxGridData.ControlID, _
                                               auxScript, True)
            lblGrid.Text = auxGridData.gControl_GetBodyDefinition


            auxClientCon.gObjectTmp_Upload(auxGridData, auxGridCacheID)
        Else
            'datos no encontrados.
        End If
        updupdpnlResults.Update()

    End Sub
    Private auxMaxlevel As Integer = -1
    'Private auxView As Integer = -1
    Protected Sub grdData_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs)
        e.Row.Cells(1).Visible = False
        e.Row.Cells(2).Visible = False
        e.Row.Cells(3).Visible = False
        e.Row.Cells(4).Visible = False
        e.Row.Cells(5).Visible = False
        e.Row.Cells(6).Visible = False
        If e.Row.RowType = DataControlRowType.DataRow Then

            'If e.Row.DataItem("q_type") <> 2 Then
            Dim auxLevel As Integer '= UBound(Split(e.Row.DataItem("q_cod"), "/"))
            If auxMaxlevel = -1 Then
                auxMaxlevel = CInt(e.Row.DataItem("q_group"))
            End If
            auxLevel = auxMaxlevel - Val(e.Row.DataItem("q_group").ToString)
            Select Case e.Row.DataItem("q_type")
                Case enumEntities.coEntityDOC_DOC
                    e.Row.Cells(0).Text = "<img style=""visibility:hidden"" width=" & (10 * auxLevel) & "px"" border=0 height=1 alt="""" />" _
                   & "<img src=imagenes/iconexpand.gif /> " _
                   & "<img width=10px"" alt="""" src=imagenes/icon" & Format(CInt(e.Row.DataItem("q_type")), "00000000") & ".png />"
                    If m_Mode <> 5 Then
                        e.Row.Cells(0).Text &= "<a href=# onclick=""hrcShowModal('cfrmrequerimientos1_det.aspx?_mode_=0&_closea_=1&param1=" & e.Row.DataItem("cod") & "');return false;"" >"
                    End If
                    e.Row.Cells(0).Text &= e.Row.DataItem("q_dsc")
                    If m_Mode <> 5 Then
                        e.Row.Cells(0).Text &= "</a>"
                    End If
                Case enumEntities.coEntityEMP
                    e.Row.Cells(0).Text = "<img style=""visibility:hidden"" width=" & (10 * auxLevel) & "px"" border=0 height=1 alt="""" />"
                    If m_Mode <> 5 Then
                        e.Row.Cells(0).Text &= "<a href=# onclick=""hrcShowModal('frmcolaboradores_det.aspx?_mode_=0&_closea_=1&param1=" & e.Row.DataItem("cod") & "');return false;"" >"
                    End If
                    e.Row.Cells(0).Text &= auxEmpDT.Select("cod=" & e.Row.DataItem("empcod"))(0)("dsc")
                    If m_Mode <> 5 Then
                        e.Row.Cells(0).Text &= "</a>"
                    End If
                Case enumEntities.coEntityUND
                    e.Row.Cells(0).Text = "<img style=""visibility:hidden"" width=" & (10 * auxLevel) & "px"" border=0 height=1 alt="""" />" _
                            & "<img src=imagenes/iconexpand.gif /> " _
                            & "<img width=10px"" alt="""" src=imagenes/icon" & Format(CInt(e.Row.DataItem("q_type")), "00000000") & ".png />"
                    If m_Mode <> 5 Then
                        e.Row.Cells(0).Text &= "<a href=# onclick=""hrcShowModal('frmunidades_det.aspx?_mode_=0&_closea_=1&param1=" & e.Row.DataItem("cod") & "');return false;"" >"
                    End If
                    e.Row.Cells(0).Text &= e.Row.DataItem("q_dsc")
                    If m_Mode <> 5 Then
                        e.Row.Cells(0).Text &= "</a>"
                    End If
                Case enumEntities.coEntityDOC_DOCTIP
                    e.Row.Cells(0).Text = "<img style=""visibility:hidden"" width=" & (10 * auxLevel) & "px"" border=0 height=1 alt="""" />" _
                            & "<img src=imagenes/iconexpand.gif /> " _
                            & "<img width=10px"" alt="""" src=imagenes/icon" & Format(CInt(e.Row.DataItem("q_type")), "00000000") & ".png />"
                    If m_Mode <> 5 Then
                        e.Row.Cells(0).Text &= "<a href=# onclick=""hrcShowModal('frmtiposderequerimientos_det.aspx?_mode_=0&_closea_=1&param1=" & e.Row.DataItem("cod") & "');return false;"" >"
                    End If
                    e.Row.Cells(0).Text &= e.Row.DataItem("q_dsc")
                    If m_Mode <> 5 Then
                        e.Row.Cells(0).Text &= "</a>"
                    End If
                Case -999
                    e.Row.Cells(0).Text = "<img style=""visibility:hidden"" width=" & (10 * auxLevel) & "px"" border=0 height=1 alt="""" />" _
                            & "<img src=imagenes/iconexpand.gif /> " _
                            & "* " & e.Row.DataItem("q_dsc")
            End Select

            e.Row.Cells(7).HorizontalAlign = HorizontalAlign.Left
            'e.Row.Cells(7).Text = "<img style=""visibility:hidden"" width=" & (10 * auxLevel) & "px"" border=0 height=1 alt="""" />" _
            '        & gMinutes_GetTimeProjectDisplay(Val(e.Row.Cells(7).Text), imGantt.enumTimeUnits.Hours)

            Select Case m_view
                Case 14, 15
                    e.Row.Cells(8).HorizontalAlign = HorizontalAlign.Left
                    e.Row.Cells(8).Text = "<img style=""visibility:hidden"" width=" & (10 * auxLevel) & "px"" border=0 height=1 alt="""" />" _
                            & e.Row.Cells(8).Text

            End Select

            Dim auxColor As String = "#" & Hex(200 + auxLevel * 8) _
                        & Hex(200 + auxLevel * 8) _
                        & Hex(200 + auxLevel * 8)
            e.Row.Attributes.Add("style", "background-color:" & auxColor & ";") _
                                  '& "border-top-style:solid;border-top-width:1px;border-top-color:#808080; " _
            '& "border-bottom-style:solid;border-bottom-width:1px;border-bottom-color:#C0c0c0;")
            '
            'Else
            '    e.Row.Cells(0).Text = "<img  style=""visibility:hidden"" width=" & 10 * (auxLevel + 1) & "px"" border=0 height=1 alt="""" height=0 />" _
            '            & "<img  width=10px"" alt="""" src=imagenes/icon" & Format(CInt(e.Row.DataItem("q_type")), "00000000") & ".png />" _
            '            & "<a href=# onclick=""hrcShowModal('cfrmrequerimientos1_det.aspx?_mode_=0&_closea_=1&param1=" & e.Row.DataItem("cod") & "');return false;"" >" & e.Row.DataItem("q_dsc") & "</a>"
            '    'e.Row.Cells(1).HorizontalAlign = HorizontalAlign.Right
            'End If
            e.Row.Cells(0).HorizontalAlign = HorizontalAlign.Left

        ElseIf e.Row.RowType = DataControlRowType.Header Then '. . . . . . . . . . . . . . . . . . . . .
            e.Row.Cells(0).Text = ""
            e.Row.Cells(0).Width = 300
            e.Row.Cells(7).Width = 100
            For Each auxCell As TableCell In e.Row.Cells
                Dim auxType As Integer = Val(Mid(auxCell.Text, 8, 6))
                Dim auxCod As Integer = Val(Mid(auxCell.Text, 15, 10))
                Select Case auxType
                    Case enumEntities.coEntityEMP 'sss
                        auxCell.Text = ""
                        If m_Mode <> 5 Then
                            auxCell.Text = "<a href=# onclick=""hrcShowModal('frmcolaboradores_det.aspx?_mode_=0&_closea_=1&param1=" & auxCod & "');return false;"" >"
                        End If
                        auxCell.Text &= auxEmpDT.Select("cod=" & auxCod)(0)("dsc")
                        If m_Mode <> 5 Then
                            auxCell.Text &= "</a>"
                        End If
                    Case enumEntities.coEntityUND
                        auxCell.Text = ""
                        If m_Mode <> 5 Then
                            auxCell.Text = "<a href=# onclick=""hrcShowModal('frmunidades_det.aspx?_mode_=0&_closea_=1&param1=" & auxCod & "');return false;"" >"
                        End If
                        auxCell.Text &= auxEmpDT.Select("cod=" & auxCod)(0)("dsc")
                        If m_Mode <> 5 Then
                            auxCell.Text &= "</a>"
                        End If
                    Case enumEntities.coEntityDOC_DOCTIP
                        auxCell.Text = ""
                        If m_Mode <> 5 Then
                            auxCell.Text = "<a href=# onclick=""hrcShowModal('frmtiposderequerimientos_det.aspx?_mode_=0&_closea_=1&param1=" & auxCod & "');return false;"" >"
                        End If
                        auxCell.Text &= auxEmpDT.Select("cod=" & auxCod)(0)("dsc")
                        If m_Mode <> 5 Then
                            auxCell.Text &= "</a>"
                        End If
                    Case enumEntities.coEntityDOC_DOC
                        auxCell.Text = ""
                        If m_Mode <> 5 Then
                            auxCell.Text = "<a href=# onclick=""hrcShowModal('cfrmrequerimientos1_det.aspx?_mode_=0&_closea_=1&param1=" & auxCod & "');return false;"" >"
                        End If
                        auxCell.Text &= auxEmpDT.Select("cod=" & auxCod)(0)("dsc")
                        If m_Mode <> 5 Then
                            auxCell.Text &= "</a>"
                        End If
                End Select

            Next

        End If
    End Sub


    Private Function gGraphics_Draw(ByVal ptitle As String, _
                                    ByVal pDT As DataTable) As Chart

        Dim auxSelectString As String = ""

        If pDT.Rows.Count = 0 Then
            Return Nothing
            Exit Function
        End If

        Dim auxGraphic As New Chart

        auxGraphic.Titles.Add(ptitle)
        auxGraphic.Width = 450
        auxGraphic.Height = 250
        'auxGraphic.ID = Left(Replace(ptitle, " ", ""), 10)
        auxGraphic.BackColor = Drawing.Color.WhiteSmoke
        auxGraphic.BackImageAlignment = ChartImageAlignmentStyle.Center

        For auxI As Integer = 1 To pDT.Columns.Count - 1
            Dim auxColumn As DataColumn = pDT.Columns(auxI)
            Dim auxSerie As Series = auxGraphic.Series.Add(auxColumn.ColumnName)
            auxSerie.Label = "#VALY"
            auxSerie.YValuesPerPoint = 1
            auxSerie.ChartType = SeriesChartType.Column
            auxSerie.ChartType = SeriesChartType.Bar
            auxSerie.XValueMember = "periodo"
            auxSerie.YValueMembers = auxColumn.ColumnName
            auxGraphic.Legends.Add(auxColumn.ColumnName)
            auxSerie.Font = New System.Drawing.Font("Verdana", 5, System.Drawing.FontStyle.Regular)
        Next

        'auxGraphic.Series(0).IsVisibleInLegend = False
        Dim auxChartArea As ChartArea = auxGraphic.ChartAreas.Add("ChartArea1")
        auxChartArea.BorderColor = Drawing.Color.DimGray
        auxChartArea.ShadowColor = Drawing.Color.LightSteelBlue

        'auxChartArea.AxisY.Title = "Cantidad"
        auxChartArea.AxisY.Minimum = 0
        'auxChartArea.AxisX.LabelStyle.Format = "C"
        'auxChartArea.AxisX.Title = "Período"
        auxChartArea.AxisY.LabelStyle.Font = New System.Drawing.Font("Verdana", 7, System.Drawing.FontStyle.Regular)
        auxChartArea.AxisX.IsLabelAutoFit = False
        auxChartArea.AxisX.TextOrientation = TextOrientation.Horizontal
        auxChartArea.AxisX.MajorGrid.LineDashStyle = ChartDashStyle.Dot
        auxChartArea.AxisX.MajorGrid.LineColor = Drawing.Color.Silver
        auxChartArea.AxisX.MinorGrid.LineDashStyle = ChartDashStyle.Dot
        auxChartArea.AxisY.MajorGrid.LineColor = Drawing.Color.Silver
        auxChartArea.AxisX.MajorGrid.LineWidth = 1
        auxChartArea.AxisX.LabelStyle.Font = New System.Drawing.Font("Verdana", 7, System.Drawing.FontStyle.Regular)
        'auxChartArea.AxisX.LabelStyle.Angle = -90
        auxChartArea.AxisX.Interval = 1

        'Para activar el corte de escala en Eje Y
        'auxChartArea.AxisY.ScaleBreakStyle.Enabled = True


        'auxChartArea.Area3DStyle.Enable3D = True

        auxGraphic.DataSource = pDT
        auxGraphic.ImageStorageMode = ImageStorageMode.UseHttpHandler
        auxGraphic.ImageType = ChartImageType.Png
        'auxGraphic.ImageLocation = "C:\temp"
        auxGraphic.DataBind()


        Return auxGraphic
    End Function
    '<asp:Chart ID="chrHorasCons" runat="server" Height="400px" Width="740px" 
    '          BackColor="WhiteSmoke" Visible="true"
    '         BackImageAlignment="Center" 
    '         AlternateText="Cargando gráfico..." 
    '         ToolTip="Gráfico de minutos consumidos por período" 
    '         >
    '         <series>
    '                  <asp:Series Name="Series1" 
    '                 YValuesPerPoint="1" ChartType="Line" 
    '                  XValueMember="fecha"
    '                   YValueMembers="cantidad" > 
    '             </asp:Series>
    '         </series>
    '         <chartareas>
    '             <asp:ChartArea Name="ChartArea1" BorderColor="DimGray">
    '                 <AxisY Title="Minutos" Minimum="0">
    '                     <MajorGrid LineDashStyle="Dot" />
    '                     <MinorGrid LineDashStyle="Dot" />
    '                 </AxisY>
    '                 <AxisX Title="Período" LineColor="DimGray" IsLabelAutoFit="False" 
    '                     TextOrientation="Horizontal" LabelAutoFitMaxFontSize="9">
    '                     <MajorGrid LineDashStyle="Dot" />
    '                     <LabelStyle ForeColor="Maroon" Angle="0" Enabled="true" IsStaggered="True" 
    '                         TruncatedLabels="True" Font="Microsoft Sans Serif, 7pt" />
    '                 </AxisX>
    '                 <Area3DStyle Enable3D="True" PointDepth="30" />
    '             </asp:ChartArea>
    '         </chartareas>
    '     </asp:Chart>

    '////////////////////////////

    Protected Sub cmdobjectexplorerfilter_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        tvwmodalpopuppnlobjectexplorer_SelectedNodeChanged(tvwmodalpopuppnlobjectexplorer, Nothing)
    End Sub
    Protected Sub grdobjectexplorer_ItemDeleted(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewCommandEventArgs)

    End Sub
    Protected Sub grdobjectexplorer_ItemInserted(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewCommandEventArgs)

    End Sub
    Protected Sub grdobjectexplorer_ItemSelected(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewCommandEventArgs)

    End Sub
    Protected Sub grdobjectexplorer_ItemUpdated(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewCommandEventArgs)

    End Sub
    Protected Sub grdobjectexplorer_RowCommand(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewCommandEventArgs)
        Select Case e.CommandName.ToUpper()
            Case "UPDATE"
                grdobjectexplorer_ItemUpdated(sender, e)
            Case "INSERT"
                grdobjectexplorer_ItemInserted(sender, e)
        End Select

    End Sub
    Protected Sub grdobjectexplorer_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs)
        modalpopuppnlobjectexplorer_Select(grdobjectexplorer.SelectedDataKey(0), grdobjectexplorer.SelectedDataKey(1), CType(grdobjectexplorer.SelectedRow.Cells(2).Controls(1), LinkButton).Text)
    End Sub
    Protected Sub cmdobjectexplorercancel_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        modalpopuppnlobjectexplorer_Cancel()
    End Sub
    Protected Sub cmdobjectexplorerselect_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        modalpopuppnlobjectexplorer_Select(grdobjectexplorer.SelectedDataKey(0), grdobjectexplorer.SelectedDataKey(1), grdobjectexplorer.SelectedRow.Cells(2).Text)
    End Sub
    Public Sub modalpopuppnlobjectexplorer_Cancel()
        ViewState("modalpopuppnlobjectexplorer_mode") = Nothing
        pnlobjectexplorer.Hide()

    End Sub
    Public Sub modalpopuppnlobjectexplorer_Load(ByVal pCod As Integer, ByVal pParentNode As TreeNode, ByVal pObjectType As Integer)
        Dim auxQuery As String = ""
        Select Case pObjectType
            Case enumEntities.coEntityUND
                auxQuery = "SELECT UND.cod,UND.dsc," & enumEntities.coEntityUND & " as objecttype FROM UND   WHERE ((UND.undcodsup = {#PARAM1#}) AND (UND.baja = 0 OR UND.baja  IS NULL)) AND UND.cod >= 1"
            Case enumEntities.coEntityEMP
                auxQuery = "SELECT EMP.cod,EMP.dsc," & enumEntities.coEntityEMP & " as objecttype FROM EMP   WHERE ((EMP.undcod = {#PARAM1#}) AND (EMP.baja = 0 OR EMP.baja  IS NULL)) AND EMP.cod >= 1"
        End Select
        Dim auxClass As New clsCusimDOC
        auxClass.Conn.gConn_Open()
        auxQuery = Replace(auxQuery, "{#PARAM1#}", pCod)
        Dim auxDT As DataTable = auxClass.Conn.gConn_Query(auxQuery)
        For Each auxRow As DataRow In auxDT.Rows
            Dim auxNode As New TreeNode(If(IsDBNull(auxRow(1)), "Todos", auxRow(1)), Format(auxRow(0)))
            If pParentNode Is Nothing Then
                tvwmodalpopuppnlobjectexplorer.Nodes.Add(auxNode)
            Else
                pParentNode.ChildNodes.Add(auxNode)
            End If
            modalpopuppnlobjectexplorer_Load(CInt(auxRow(0)), auxNode, pObjectType)
        Next
        auxClass.Conn.gConn_Close()
    End Sub
    Public Sub objectexplorer_SetValue(ByVal pValue As Integer, ByVal pObjectType As Integer, ByVal pText As String, ByVal pLinkButton As LinkButton, ByVal pHiddenValue As HiddenField, ByVal pHiddentype As HiddenField, ByVal pDeleteButton As ImageButton)
        If pHiddenValue IsNot Nothing Then pHiddenValue.Value = pValue
        If pHiddentype IsNot Nothing Then pHiddentype.Value = pObjectType
        If pValue < 1 Then
            pLinkButton.Visible = False
            If pDeleteButton IsNot Nothing Then pDeleteButton.Visible = False
            Exit Sub
        End If
        Dim auxText As String = ""
        auxText = "<img width=14 height=14 border=0 src='imagenes/icon" & Format(pObjectType, "00000000") & ".png' />" & pText
        pLinkButton.Text = auxText
        Select Case pObjectType
            Case 1
                pLinkButton.OnClientClick = "javascript:if (window.showModalDialog) {window.showModalDialog(" & Chr(39) & "frmColaboradores_det.aspx?_mode_=0&_closea_=1&param1=" & pValue & "&timestamp=" & Chr(39) & " + new Date().getTime() + " & Chr(39) & "" & Chr(39) & ", " & Chr(39) & "" & Chr(39) & ", " & Chr(39) & "menubar=no;resizable=yes;toolbar=no;status=no;center=yes;dialogWidth=750px;dialogHeight=570px;" & Chr(39) & ");} else {window.open(" & Chr(39) & "frmColaboradores_det.aspx?_mode_=0&_closea_=1&param1=" & pValue & "&timestamp=" & Chr(39) & " + new Date().getTime() + " & Chr(39) & "" & Chr(39) & ", " & Chr(39) & "" & Chr(39) & ", " & Chr(39) & "modal=yes;directories=no;menubar=no;resizable=yes;toolbar=no;status=no;center=yes;dialogWidth=750px;dialogHeight=570px;" & Chr(39) & ");};return false;"
            Case 2
                pLinkButton.OnClientClick = "javascript:if (window.showModalDialog) {window.showModalDialog(" & Chr(39) & "frmUnidades_det.aspx?_mode_=0&_closea_=1&param1=" & pValue & "&timestamp=" & Chr(39) & " + new Date().getTime() + " & Chr(39) & "" & Chr(39) & ", " & Chr(39) & "" & Chr(39) & ", " & Chr(39) & "menubar=no;resizable=yes;toolbar=no;status=no;center=yes;dialogWidth=750px;dialogHeight=570px;" & Chr(39) & ");} else {window.open(" & Chr(39) & "frmUnidades_det.aspx?_mode_=0&_closea_=1&param1=" & pValue & "&timestamp=" & Chr(39) & " + new Date().getTime() + " & Chr(39) & "" & Chr(39) & ", " & Chr(39) & "" & Chr(39) & ", " & Chr(39) & "modal=yes;directories=no;menubar=no;resizable=yes;toolbar=no;status=no;center=yes;dialogWidth=750px;dialogHeight=570px;" & Chr(39) & ");};return false;"
            Case 3
                pLinkButton.OnClientClick = "javascript:if (window.showModalDialog) {window.showModalDialog(" & Chr(39) & "frmTiposderequerimientos_det.aspx?_mode_=0&_closea_=1&param1=" & pValue & "&timestamp=" & Chr(39) & " + new Date().getTime() + " & Chr(39) & "" & Chr(39) & ", " & Chr(39) & "" & Chr(39) & ", " & Chr(39) & "menubar=no;resizable=yes;toolbar=no;status=no;center=yes;dialogWidth=750px;dialogHeight=570px;" & Chr(39) & ");} else {window.open(" & Chr(39) & "frmTiposderequerimientos_det.aspx?_mode_=0&_closea_=1&param1=" & pValue & "&timestamp=" & Chr(39) & " + new Date().getTime() + " & Chr(39) & "" & Chr(39) & ", " & Chr(39) & "" & Chr(39) & ", " & Chr(39) & "modal=yes;directories=no;menubar=no;resizable=yes;toolbar=no;status=no;center=yes;dialogWidth=750px;dialogHeight=570px;" & Chr(39) & ");};return false;"
            Case 5
                pLinkButton.OnClientClick = "javascript:if (window.showModalDialog) {window.showModalDialog(" & Chr(39) & "frmFunciones_det.aspx?_mode_=0&_closea_=1&param1=" & pValue & "&timestamp=" & Chr(39) & " + new Date().getTime() + " & Chr(39) & "" & Chr(39) & ", " & Chr(39) & "" & Chr(39) & ", " & Chr(39) & "menubar=no;resizable=yes;toolbar=no;status=no;center=yes;dialogWidth=750px;dialogHeight=570px;" & Chr(39) & ");} else {window.open(" & Chr(39) & "frmFunciones_det.aspx?_mode_=0&_closea_=1&param1=" & pValue & "&timestamp=" & Chr(39) & " + new Date().getTime() + " & Chr(39) & "" & Chr(39) & ", " & Chr(39) & "" & Chr(39) & ", " & Chr(39) & "modal=yes;directories=no;menubar=no;resizable=yes;toolbar=no;status=no;center=yes;dialogWidth=750px;dialogHeight=570px;" & Chr(39) & ");};return false;"
            Case 11
                pLinkButton.OnClientClick = "javascript:if (window.showModalDialog) {window.showModalDialog(" & Chr(39) & "frmProveedores_det.aspx?_mode_=0&_closea_=1&param1=" & pValue & "&timestamp=" & Chr(39) & " + new Date().getTime() + " & Chr(39) & "" & Chr(39) & ", " & Chr(39) & "" & Chr(39) & ", " & Chr(39) & "menubar=no;resizable=yes;toolbar=no;status=no;center=yes;dialogWidth=750px;dialogHeight=570px;" & Chr(39) & ");} else {window.open(" & Chr(39) & "frmProveedores_det.aspx?_mode_=0&_closea_=1&param1=" & pValue & "&timestamp=" & Chr(39) & " + new Date().getTime() + " & Chr(39) & "" & Chr(39) & ", " & Chr(39) & "" & Chr(39) & ", " & Chr(39) & "modal=yes;directories=no;menubar=no;resizable=yes;toolbar=no;status=no;center=yes;dialogWidth=750px;dialogHeight=570px;" & Chr(39) & ");};return false;"
            Case 37
                pLinkButton.OnClientClick = "javascript:if (window.showModalDialog) {window.showModalDialog(" & Chr(39) & "frmRecursos_det.aspx?_mode_=0&_closea_=1&param1=" & pValue & "&timestamp=" & Chr(39) & " + new Date().getTime() + " & Chr(39) & "" & Chr(39) & ", " & Chr(39) & "" & Chr(39) & ", " & Chr(39) & "menubar=no;resizable=yes;toolbar=no;status=no;center=yes;dialogWidth=750px;dialogHeight=570px;" & Chr(39) & ");} else {window.open(" & Chr(39) & "frmRecursos_det.aspx?_mode_=0&_closea_=1&param1=" & pValue & "&timestamp=" & Chr(39) & " + new Date().getTime() + " & Chr(39) & "" & Chr(39) & ", " & Chr(39) & "" & Chr(39) & ", " & Chr(39) & "modal=yes;directories=no;menubar=no;resizable=yes;toolbar=no;status=no;center=yes;dialogWidth=750px;dialogHeight=570px;" & Chr(39) & ");};return false;"
        End Select
        pLinkButton.Visible = True
        If pDeleteButton IsNot Nothing Then pDeleteButton.Visible = True

    End Sub
    Public Sub modalpopuppnlobjectexplorer_Select(ByVal pValue As Integer, ByVal pObjectType As Integer, ByVal pText As String)
        Dim auxText As String = ""
        auxText = "<img width=14 height=14 border=0 src='imagenes/icon" & Format(pObjectType, "00000000") & ".png' />" & pText
        Dim auxLinkButton As LinkButton = CType(FindControl(ViewState("modalpopuppnlobjectexplorer_controlid")), LinkButton)
        objectexplorer_SetValue(pValue, pObjectType, pText, auxLinkButton, CType(FindControl(ViewState("modalpopuppnlobjectexplorer_value")), HiddenField), CType(FindControl(ViewState("modalpopuppnlobjectexplorer_type")), HiddenField), CType(FindControl(ViewState("modalpopuppnlobjectexplorer_delete")), ImageButton))
        CType(FindControl(ViewState("modalpopuppnlobjectexplorer_value")), HiddenField).Value = pValue
        CType(FindControl(ViewState("modalpopuppnlobjectexplorer_type")), HiddenField).Value = pObjectType
        CType(FindControl(ViewState("modalpopuppnlobjectexplorer_delete")), ImageButton).Visible = True
        If TypeOf (auxLinkButton.Parent) Is Control Then
            If TypeOf (auxLinkButton.Parent.Parent) Is UpdatePanel Then
                If CType(auxLinkButton.Parent.Parent, UpdatePanel).UpdateMode = UpdatePanelUpdateMode.Conditional Then
                    CType(auxLinkButton.Parent.Parent, UpdatePanel).Update()
                End If
            End If
        End If
        ViewState("modalpopuppnlobjectexplorer_mode") = Nothing
        pnlobjectexplorer.Hide()
    End Sub
    Public Sub objectexplorer_Show(ByVal pControlIDValue As String, ByVal pControlIDText As String, ByVal pControlIDType As String, ByVal pObjectTvwType As Integer, ByVal pObjectGrdType As String, ByVal pControlIDDelete As String)
        tvwmodalpopuppnlobjectexplorer.Nodes.Clear()
        Dim auxStartNode As New TreeNode("<img width=20 height=20 border=0 src=imagenes/icon" & Format(pObjectTvwType, "00000000") & ".png />", -1)
        tvwmodalpopuppnlobjectexplorer.Nodes.Add(auxStartNode)
        modalpopuppnlobjectexplorer_Load(-1, auxStartNode, pObjectTvwType)
        grdobjectexplorer.Visible = True
        tvwmodalpopuppnlobjectexplorer.Nodes(0).Select()
        tvwmodalpopuppnlobjectexplorer_SelectedNodeChanged(tvwmodalpopuppnlobjectexplorer, Nothing)
        tvwmodalpopuppnlobjectexplorer.Nodes(0).Expand()
        ViewState("modalpopuppnlobjectexplorer_controlid") = pControlIDText
        ViewState("modalpopuppnlobjectexplorer_value") = pControlIDValue
        ViewState("modalpopuppnlobjectexplorer_type") = pControlIDType
        ViewState("modalpopuppnlobjectexplorer_delete") = pControlIDDelete
        Dim auxOnePanel As Boolean = True
        Dim auxObjectTypes As String = ""
        For Each auxString As String In Split(pObjectGrdType, "{#CHR34#}")
            If auxString.Trim <> "" Then
                auxObjectTypes &= "{#CHR34#}" & auxString.Trim & "{#CHR34#}"
                If auxOnePanel = True And Val(auxString) <> pObjectTvwType Then
                    auxOnePanel = False
                End If
            End If
        Next
        ViewState("modalpopuppnlobjectexplorer_mode") = auxObjectTypes
        grdobjectexplorer.Visible = True
        cmdobjectexplorerfilter.Visible = True
        txtobjectexplorerfilter.Visible = True
        lblobjectexplorerfilter.Visible = True
        pnlobjectexplorer.Show()
        updpnlobjectexplorer.Update()

    End Sub
    Public Sub tvwmodalpopuppnlobjectexplorer_SelectedNodeChanged(ByVal sender As Object, ByVal e As System.EventArgs)
        If grdobjectexplorer.Visible = False Then
            modalpopuppnlobjectexplorer_Select(CType(sender, TreeView).SelectedNode.Value, 1, CType(sender, TreeView).SelectedNode.Text)
        Else
            Dim auxConn As clsHrcConnClient = Session("conn")
            Dim auxQuery As String = ""
            If InStr(ViewState("modalpopuppnlobjectexplorer_mode"), "{#CHR34#}" & enumEntities.coEntityUND & "{#CHR34#}") > 0 Then
                If auxQuery <> "" Then auxQuery &= " UNION ALL "
                auxQuery &= "(SELECT UND.cod AS cod,UND.dsc as dsc," & enumEntities.coEntityUND & " as objecttype FROM UND   WHERE ((((UND.undcodsup =" & CType(sender, TreeView).SelectedNode.Value & " AND '" & txtobjectexplorerfilter.Text.Trim & "'='')  OR  (UND.dsc LIKE '%" & txtobjectexplorerfilter.Text.Trim & "%' AND '" & txtobjectexplorerfilter.Text.Trim & "'<>''))) AND (UND.baja = 0 OR UND.baja  IS NULL)) AND UND.cod >= 1)"
            End If
            If InStr(ViewState("modalpopuppnlobjectexplorer_mode"), "{#CHR34#}" & enumEntities.coEntityDOC_EQU & "{#CHR34#}") > 0 Then
                If auxQuery <> "" Then auxQuery &= " UNION ALL "
                auxQuery &= "(SELECT REQ_EQU.cod as cod,REQ_EQU.dsc as dsc," & enumEntities.coEntityDOC_EQU & " as objecttype FROM REQ_EQU  WHERE ((((REQ_EQU.undcod =" & CType(sender, TreeView).SelectedNode.Value & " AND '" & txtobjectexplorerfilter.Text.Trim & "'='')  OR  (REQ_EQU.dsc LIKE '%" & txtobjectexplorerfilter.Text.Trim & "%' AND '" & txtobjectexplorerfilter.Text.Trim & "'<>''))) AND (REQ_EQU.baja = 0 OR REQ_EQU.baja  IS NULL)) AND REQ_EQU.cod >= 1 )"
            End If
            If InStr(ViewState("modalpopuppnlobjectexplorer_mode"), "{#CHR34#}" & enumEntities.coEntityEMP & "{#CHR34#}") > 0 Then
                If auxQuery <> "" Then auxQuery &= " UNION ALL "
                auxQuery &= "(SELECT EMP.cod as cod,EMP.dsc as dsc," & enumEntities.coEntityEMP & " as objecttype FROM EMP   WHERE ((((EMP.undcod =" & CType(sender, TreeView).SelectedNode.Value & " AND '" & txtobjectexplorerfilter.Text.Trim & "'='')  OR  (EMP.dsc LIKE '%" & txtobjectexplorerfilter.Text.Trim & "%' AND '" & txtobjectexplorerfilter.Text.Trim & "'<>''))) AND (EMP.baja = 0 OR EMP.baja  IS NULL)) AND EMP.cod >= 1)"
            End If
            If InStr(ViewState("modalpopuppnlobjectexplorer_mode"), "{#CHR34#}" & enumEntities.coEntityDOC_DOCTIP & "{#CHR34#}") > 0 Then
                If auxQuery <> "" Then auxQuery &= " UNION ALL "
                auxQuery &= "(SELECT REQ_TIP.cod as cod,REQ_TIP.dsc as dsc," & enumEntities.coEntityDOC_DOCTIP & " as objecttype FROM REQ_TIP   WHERE ((((REQ_TIP.undcodresol =" & CType(sender, TreeView).SelectedNode.Value & " AND '" & txtobjectexplorerfilter.Text.Trim & "'='')  OR  (REQ_TIP.dsc LIKE '%" & txtobjectexplorerfilter.Text.Trim & "%' AND '" & txtobjectexplorerfilter.Text.Trim & "'<>''))) AND (REQ_TIP.baja = 0 OR REQ_TIP.baja  IS NULL)) AND REQ_TIP.cod >= 1)"
            End If

            auxQuery &= " ORDER BY dsc,objecttype,cod"
            If e IsNot Nothing Then
                'Si es treeview no es nothing
                txtobjectexplorerfilter.Text = ""
            End If
            grdobjectexplorer.DataSource = auxConn.gConn_Query(auxQuery)
            grdobjectexplorer.DataBind()
            updpnlobjectexplorer.Update()
        End If

    End Sub

    Protected Sub grdobjectexplorer_PageIndexChanging(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles grdobjectexplorer.PageIndexChanging
        tvwmodalpopuppnlobjectexplorer_SelectedNodeChanged(tvwmodalpopuppnlobjectexplorer, Nothing)
        grdobjectexplorer.PageIndex = e.NewPageIndex
        grdobjectexplorer.DataBind()
        updpnlobjectexplorer.Update()
    End Sub

    Protected Sub cmbView_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs)  'Handles cmbView.SelectedIndexChanged

        Dim auxMode As Integer
        auxMode = Val(Request.QueryString("_mode_"))
        If auxMode <> 5 Then
            auxSelectCombo = cmbAgrupacion.SelectedValue
        End If

        Select Case cmbView.SelectedValue
            Case 21, 23, 24, 25, 27, 28, 29, 30
                cmbAgrupacion.Items.Clear()
                cmbAgrupacion.Items.Add(New ListItem("", 0))
                cmbAgrupacion.Items.Add(New ListItem("Unidad", 1))
                cmbAgrupacion.Items.Add(New ListItem("Colaborador", 2))
                cmbAgrupacion.Items.Add(New ListItem("Tipo de sistema", 3))
                cmbAgrupacion.Items.Add(New ListItem("Proceso", 4))
                cmbAgrupacion.Items.Add(New ListItem("Tipo de documento", 5))
                cmbAgrupacion.Items.Add(New ListItem("Estado", 6))
                Select Case cmbView.SelectedValue
                    Case 24, 25, 29, 30
                        cmbAgrupacion.Items.Add(New ListItem("Documento", 7))
                    Case Else
                        cmbAgrupacion.Items.Add(New ListItem("Documento", 7, False))
                End Select
            Case 22, 26
                cmbAgrupacion.Items.Clear()
                cmbAgrupacion.Items.Add(New ListItem("", 0))
                cmbAgrupacion.Items.Add(New ListItem("Unidad", 1))
                cmbAgrupacion.Items.Add(New ListItem("Colaborador", 2, False))
                cmbAgrupacion.Items.Add(New ListItem("Tipo de sistema", 3))
                cmbAgrupacion.Items.Add(New ListItem("Proceso", 4))
                cmbAgrupacion.Items.Add(New ListItem("Tipo de documento", 5))
                cmbAgrupacion.Items.Add(New ListItem("Estado", 6, False))
                cmbAgrupacion.Items.Add(New ListItem("Documento", 7, False))
        End Select

        If auxMode <> 5 Then
            cmbAgrupacion.SelectedValue = auxSelectCombo
        End If

        'Filas de filtros -------------------
        rowfechas.Visible = False
        row_001.Visible = False
        row_002.Visible = False
        row_003.Visible = False
        row_004.Visible = False
        '------------------------------------
        'lblund.Visible = False
        lblFechaDesde.Visible = False
        txtFechaDesde.Visible = False
        lblFechaHasta.Visible = False
        txtFechaHasta.Visible = False
        lblTipoDocumento.Visible = False
        lstTipoDocumento.Visible = False
        lblUnidadDocumento.Visible = False
        lstUnidadDocumento.Visible = False
        lblTituloDocumento.Visible = False
        txtTituloDocumento.Visible = False
        lblProceso.Visible = False
        lstProceso.Visible = False
        lblColaborador_UnidadColaborador.Visible = False
        lstColaborador_UnidadColaborador.Visible = False
        lblAgrupacion.Visible = False
        cmbAgrupacion.Visible = False

        Select Case cmbView.SelectedValue
            Case 1, 2, 3, 4, 5, 6
                lblUnidadDocumento.Visible = True
                lstUnidadDocumento.Visible = True
                row_001.Visible = True
                row_002.Visible = True

            Case 21, 23, 24, 25, 27, 28, 29, 30
                lblFechaDesde.Visible = True
                txtFechaDesde.Visible = True
                lblFechaHasta.Visible = True
                txtFechaHasta.Visible = True
                lblTipoDocumento.Visible = True
                lstTipoDocumento.Visible = True
                lblUnidadDocumento.Visible = True
                lstUnidadDocumento.Visible = True
                lblTituloDocumento.Visible = True
                txtTituloDocumento.Visible = True
                lblProceso.Visible = True
                lstProceso.Visible = True
                lblColaborador_UnidadColaborador.Visible = True
                lstColaborador_UnidadColaborador.Visible = True
                lblAgrupacion.Visible = True
                cmbAgrupacion.Visible = True
                row_001.Visible = True
                row_002.Visible = True
                row_003.Visible = True
                row_004.Visible = True
                rowfechas.Visible = True
            Case 34
                row_001.Visible = True
            Case 22
                lblAgrupacion.Visible = True
                cmbAgrupacion.Visible = True
                row_001.Visible = True
            Case 26
                lblFechaDesde.Visible = True
                txtFechaDesde.Visible = True
                lblFechaHasta.Visible = True
                txtFechaHasta.Visible = True
                lblTipoDocumento.Visible = True
                lstTipoDocumento.Visible = True
                lblUnidadDocumento.Visible = True
                lstUnidadDocumento.Visible = True
                lblTituloDocumento.Visible = True
                txtTituloDocumento.Visible = True
                lblProceso.Visible = True
                lstProceso.Visible = True
                lblAgrupacion.Visible = True
                cmbAgrupacion.Visible = True
                row_001.Visible = True
                row_002.Visible = True
                row_003.Visible = True
                row_004.Visible = True
                rowfechas.Visible = True
            Case 32
                lblFechaDesde.Visible = True
                txtFechaDesde.Visible = True
                lblFechaHasta.Visible = True
                txtFechaHasta.Visible = True
                lblColaborador_UnidadColaborador.Visible = True
                lstColaborador_UnidadColaborador.Visible = True
                row_001.Visible = True
                row_003.Visible = True
                rowfechas.Visible = True
        End Select


        'updupdpnlSearch.Update()
        updupdpnlActions.Update()

    End Sub

    Protected Sub cmdReportePDF_Click(ByVal sender As Object, ByVal e As System.EventArgs) 'Handles cmdReportePDF.Click
        Dim auxConnection As New imClientConnection

        Dim auxUserName As String = ""
        Dim auxPassword As String = ""
        Dim auxProcessUserName As String = ""
        Dim auxProcessPassword As String = ""
        If ConfigurationManager.AppSettings("adusuario") IsNot Nothing Then
            auxProcessUserName = ConfigurationManager.AppSettings("adusuario").ToString
        End If
        If ConfigurationManager.AppSettings("addominio") IsNot Nothing Then
            If ConfigurationManager.AppSettings("addominio") <> "" Then
                auxProcessUserName = ConfigurationManager.AppSettings("addominio") & "\" & auxUserName
            End If
        End If
        If ConfigurationManager.AppSettings("adpassword") IsNot Nothing Then
            auxProcessPassword = ConfigurationManager.AppSettings("adpassword").ToString
        End If
        Dim auxHrcSesID As String = ""
        If Request.QueryString("_sesid_") IsNot Nothing Then
            auxHrcSesID = Request.QueryString("_sesid_").Trim
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

        Dim auxClass As New clsCusimDOC
        auxClass.Conn.gConn_Open()
        Dim imClientConnection1 As New imClientConnection
        'imClientConnection1.Debug = True
        Dim auxSesionID1 As String = auxClass.Sec.gLogin_CreateDelegatedSessionToSystem
        auxClass.Conn.gConn_Close()
        Dim auxURL As String = Request.Url.AbsoluteUri
        auxURL = IIf(InStr(auxURL, "?") > 0, Left(auxURL, InStr(auxURL, "?")), auxURL & "?")

        '******
        gAutosugerido_Download()


        Dim auxDateDesde As Date = auxClass.Conn.gField_GetDate(txtFechaDesde.Text)
        Dim auxDateHasta As Date = auxClass.Conn.gField_GetDate(txtFechaHasta.Text)
        If auxSesionID1 = "" Then
            lblerror.Text = "ERROR: No se encuentra logueado!"
            'updupdpnlResults.Update()
        Else

            Dim auxMode As Integer = (Request.QueryString("_mode_"))

            Dim auxHTML As New Intelimedia.Hercules.Language.clsHrcCodeHTML
            Dim auxBagValues As New clshrcBagValues
            auxBagValues.gValue_Add("_mode_", "5")
            auxBagValues.gValue_Add("_sesid_", auxSesionID1)
            auxBagValues.gValue_Add("_closea_", "1")
            auxBagValues.gValue_Add("_view_", cmbView.SelectedValue.ToString)
            auxBagValues.gValue_Add("_param2_", auxClass.Conn.gFieldDB_GetString(auxUnidadDocumentoCodList))
            auxBagValues.gValue_Add("_param3_", cmbIndicador.SelectedValue.ToString)
            auxBagValues.gValue_Add("_param4_", cmbGroup.SelectedValue.ToString)
            auxBagValues.gValue_Add("_param5_", auxClass.Conn.gFieldDB_GetDateTimeStandard(auxDateDesde))
            auxBagValues.gValue_Add("_param6_", auxClass.Conn.gFieldDB_GetDateTimeStandard(auxDateHasta))
            auxBagValues.gValue_Add("_param7_", auxClass.Conn.gFieldDB_GetString(auxTipoDocumentoCodList))
            auxBagValues.gValue_Add("_param8_", auxClass.Conn.gFieldDB_GetString(auxProcesoCodList))
            auxBagValues.gValue_Add("_param9_", auxClass.Conn.gFieldDB_GetString(auxColaboradorCodList))
            auxBagValues.gValue_Add("_param10_", auxClass.Conn.gFieldDB_GetString(auxUnidadColaboradorCodList))
            auxBagValues.gValue_Add("_param11_", cmbAgrupacion.SelectedValue.ToString)
            auxBagValues.gValue_Add("_param12_", auxClass.Conn.gField_GetString(txtTituloDocumento.Text, ""))
            auxBagValues.gValue_Add("_param13_", auxClass.Conn.gFieldDB_GetString(auxEstadoCodList))
            auxBagValues.gValue_Add("_paramMode_", auxMode.ToString)



            Dim auxbody As String = auxURL & auxHTML.gQueryString_Get(auxBagValues)

            'auxConnection.Debug = True
            Dim auxError As String = auxConnection.gFile_DownloadAsPDF("reporte.pdf", _
                                            auxbody, _
                                            "", "", Now.ToString("d/M/yyyy HH:mm:ss") & "-" & Session("secdsc"), "", auxUserName, auxPassword, auxTemporalFolder, auxProcessUserName, auxProcessPassword)
        End If
    End Sub

    Protected Sub cmdDownloadXLS_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdDownloadXLS.Click

        Dim auxClass As New clsCusimDOC
        auxClass.Conn.gConn_Open()
        gAutosugerido_Download()

        Dim auxDT As DataTable = Nothing

        Select Case cmbView.SelectedValue
            Case 21
                auxDT = gData_Get_Grilla(gReports_Data_GetSelectCommand(gReports_DocumentosMasLeidos(cmbView.SelectedValue, 1, auxClass.Conn.gField_GetInt(cmbAgrupacion.SelectedValue), auxTipoDocumentoCodList, auxUnidadDocumentoCodList, auxProcesoCodList, auxColaboradorCodList, auxUnidadColaboradorCodList, auxClass.Conn.gField_GetString(txtTituloDocumento.Text, ""), txtFechaDesde.Text, txtFechaHasta.Text, auxEstadoCodList)))
            Case 22
                auxDT = gData_Get_Grilla(gReports_Data_GetSelectCommand(gReports_VersionesDocumentos(cmbView.SelectedValue, 1, auxClass.Conn.gField_GetInt(cmbAgrupacion.SelectedValue))))
            Case 23
                auxDT = gData_Get_Grilla(gReports_Data_GetSelectCommand(gReports_ImpresionesDocumentos(cmbView.SelectedValue, 1, auxClass.Conn.gField_GetInt(cmbAgrupacion.SelectedValue), auxTipoDocumentoCodList, auxUnidadDocumentoCodList, auxProcesoCodList, auxColaboradorCodList, auxUnidadColaboradorCodList, auxClass.Conn.gField_GetString(txtTituloDocumento.Text, ""), txtFechaDesde.Text, txtFechaHasta.Text, auxEstadoCodList)))
            Case 24
                auxDT = gData_Get_Grilla(gReports_Data_GetSelectCommand(gReports_ColaboradoresEnDOC(cmbView.SelectedValue, 1, auxClass.Conn.gField_GetInt(cmbAgrupacion.SelectedValue), auxTipoDocumentoCodList, auxUnidadDocumentoCodList, auxProcesoCodList, auxColaboradorCodList, auxUnidadColaboradorCodList, auxClass.Conn.gField_GetString(txtTituloDocumento.Text, ""), txtFechaDesde.Text, txtFechaHasta.Text, auxEstadoCodList)))
            Case 25
                auxDT = gData_Get_Grilla(gReports_Data_GetSelectCommand(gReports_ColaboradoresEnDOC(cmbView.SelectedValue, 1, auxClass.Conn.gField_GetInt(cmbAgrupacion.SelectedValue), auxTipoDocumentoCodList, auxUnidadDocumentoCodList, auxProcesoCodList, auxColaboradorCodList, auxUnidadColaboradorCodList, auxClass.Conn.gField_GetString(txtTituloDocumento.Text, ""), txtFechaDesde.Text, txtFechaHasta.Text, auxEstadoCodList)))
            Case 26
                auxDT = gData_Get_Grilla(gReports_Data_GetSelectCommand(gReports_ReferenciaEntreDocumentos(cmbView.SelectedValue, 1, auxClass.Conn.gField_GetInt(cmbAgrupacion.SelectedValue), auxTipoDocumentoCodList, auxUnidadDocumentoCodList, auxProcesoCodList, auxColaboradorCodList, auxUnidadColaboradorCodList, auxClass.Conn.gField_GetString(txtTituloDocumento.Text, ""), txtFechaDesde.Text, txtFechaHasta.Text)))
            Case 27
                auxDT = gData_Get_Grilla(gReports_Data_GetSelectCommand(gReports_Actividad(cmbView.SelectedValue, 1, auxClass.Conn.gField_GetInt(cmbAgrupacion.SelectedValue), auxTipoDocumentoCodList, auxUnidadDocumentoCodList, auxProcesoCodList, auxColaboradorCodList, auxUnidadColaboradorCodList, auxClass.Conn.gField_GetString(txtTituloDocumento.Text, ""), txtFechaDesde.Text, txtFechaHasta.Text, auxEstadoCodList)))
            Case 28
                auxDT = gData_Get_Grilla(gReports_Data_GetSelectCommand(gReports_AccionesPendientes(cmbView.SelectedValue, 1, auxClass.Conn.gField_GetInt(cmbAgrupacion.SelectedValue), auxTipoDocumentoCodList, auxUnidadDocumentoCodList, auxProcesoCodList, auxColaboradorCodList, auxUnidadColaboradorCodList, auxClass.Conn.gField_GetString(txtTituloDocumento.Text, ""), txtFechaDesde.Text, txtFechaHasta.Text, auxEstadoCodList)))
            Case 29
                auxDT = gData_Get_Grilla(gReports_Data_GetSelectCommand(gReports_ColaboradoresEnDOC(cmbView.SelectedValue, 1, auxClass.Conn.gField_GetInt(cmbAgrupacion.SelectedValue), auxTipoDocumentoCodList, auxUnidadDocumentoCodList, auxProcesoCodList, auxColaboradorCodList, auxUnidadColaboradorCodList, auxClass.Conn.gField_GetString(txtTituloDocumento.Text, ""), txtFechaDesde.Text, txtFechaHasta.Text, auxEstadoCodList)))
            Case 30
                auxDT = gData_Get_Grilla(gReports_Data_GetSelectCommand(gReports_ColaboradoresEnDOC(cmbView.SelectedValue, 1, auxClass.Conn.gField_GetInt(cmbAgrupacion.SelectedValue), auxTipoDocumentoCodList, auxUnidadDocumentoCodList, auxProcesoCodList, auxColaboradorCodList, auxUnidadColaboradorCodList, auxClass.Conn.gField_GetString(txtTituloDocumento.Text, ""), txtFechaDesde.Text, txtFechaHasta.Text, auxEstadoCodList)))
            Case 32
                auxDT = gData_Get_Grilla(gReports_Data_GetSelectCommand(gReports_ResumenEtapas(m_view, 1, auxColaboradorCodList, auxUnidadColaboradorCodList, txtFechaDesde.Text, txtFechaHasta.Text)))
            Case 34
                auxDT = gData_Get_Grilla(gReports_Data_GetSelectCommand(gReports_DOCReid()))
        End Select
        'auxDT = gData_Get_Buscar()
        If cmbView.SelectedValue = 21 Then
            auxDT.Columns.RemoveAt(0)
            auxDT.Columns.RemoveAt(1)
            auxDT.Columns.RemoveAt(1)
            auxDT.Columns.RemoveAt(1)
            auxDT.Columns.RemoveAt(1)
            auxDT.Columns.RemoveAt(2)
            auxDT.Columns.RemoveAt(2)
            auxDT.Columns.RemoveAt(2)
            auxDT.Columns.RemoveAt(2)
            auxDT.Columns.RemoveAt(2)
            auxDT.Columns(0).ColumnName = "DOCUMENTOS"
        ElseIf cmbView.SelectedValue = 22 Then
            auxDT.Columns.RemoveAt(0)
            auxDT.Columns.RemoveAt(1)
            auxDT.Columns.RemoveAt(1)
            auxDT.Columns.RemoveAt(1)
            auxDT.Columns.RemoveAt(1)
            auxDT.Columns.RemoveAt(3)
            auxDT.Columns.RemoveAt(3)
            auxDT.Columns.RemoveAt(3)
            auxDT.Columns.RemoveAt(3)
            auxDT.Columns.RemoveAt(3)
            auxDT.Columns(0).ColumnName = "DOCUMENTOS_VIGENTES"
        ElseIf cmbView.SelectedValue = 23 Then
            auxDT.Columns.RemoveAt(0)
            auxDT.Columns.RemoveAt(1)
            auxDT.Columns.RemoveAt(1)
            auxDT.Columns.RemoveAt(1)
            auxDT.Columns.RemoveAt(1)
            auxDT.Columns.RemoveAt(3)
            auxDT.Columns.RemoveAt(3)
            auxDT.Columns.RemoveAt(3)
            auxDT.Columns.RemoveAt(3)
            auxDT.Columns(0).ColumnName = "DOCUMENTOS"
        ElseIf cmbView.SelectedValue = 24 Or cmbView.SelectedValue = 25 _
        Or cmbView.SelectedValue = 29 Or cmbView.SelectedValue = 30 Then
            auxDT.Columns.RemoveAt(0)
            auxDT.Columns.RemoveAt(1)
            auxDT.Columns.RemoveAt(1)
            auxDT.Columns.RemoveAt(1)
            auxDT.Columns.RemoveAt(1)
            auxDT.Columns.RemoveAt(1)
            auxDT.Columns.RemoveAt(4)
            auxDT.Columns.RemoveAt(4)
            auxDT.Columns.RemoveAt(4)
            auxDT.Columns.RemoveAt(4)
            auxDT.Columns.RemoveAt(4)
            auxDT.Columns.RemoveAt(4)
            auxDT.Columns.RemoveAt(4)
            auxDT.Columns(0).ColumnName = "DOCUMENTOS"
        ElseIf cmbView.SelectedValue = 26 Then
            auxDT.Columns.RemoveAt(0)
            auxDT.Columns.RemoveAt(1)
            auxDT.Columns.RemoveAt(1)
            auxDT.Columns.RemoveAt(1)
            auxDT.Columns.RemoveAt(1)
            auxDT.Columns.RemoveAt(1)
            auxDT.Columns.RemoveAt(3)
            auxDT.Columns.RemoveAt(3)
            auxDT.Columns.RemoveAt(3)
            auxDT.Columns.RemoveAt(3)
            auxDT.Columns(0).ColumnName = "DOCUMENTOS_VIGENTES"
        ElseIf cmbView.SelectedValue = 27 Then
            auxDT.Columns.RemoveAt(0)
            auxDT.Columns.RemoveAt(1)
            auxDT.Columns.RemoveAt(1)
            auxDT.Columns.RemoveAt(1)
            auxDT.Columns.RemoveAt(1)
            auxDT.Columns.RemoveAt(1)
            auxDT.Columns.RemoveAt(6)
            auxDT.Columns.RemoveAt(6)
            auxDT.Columns.RemoveAt(6)
            auxDT.Columns.RemoveAt(6)
            auxDT.Columns.RemoveAt(6)
            auxDT.Columns.RemoveAt(6)
            auxDT.Columns(0).ColumnName = "DOCUMENTOS"
        ElseIf cmbView.SelectedValue = 28 Then
            auxDT.Columns.RemoveAt(0)
            auxDT.Columns.RemoveAt(1)
            auxDT.Columns.RemoveAt(1)
            auxDT.Columns.RemoveAt(1)
            auxDT.Columns.RemoveAt(1)
            auxDT.Columns.RemoveAt(1)
            auxDT.Columns.RemoveAt(5)
            auxDT.Columns.RemoveAt(5)
            auxDT.Columns.RemoveAt(9)
            auxDT.Columns.RemoveAt(9)
            auxDT.Columns.RemoveAt(9)
            auxDT.Columns.RemoveAt(9)
            auxDT.Columns.RemoveAt(9)
            auxDT.Columns.RemoveAt(9)
            auxDT.Columns(0).ColumnName = "DOCUMENTOS"

        ElseIf cmbView.SelectedValue = 32 Then
            auxDT.Columns.RemoveAt(0)
            auxDT.Columns.RemoveAt(1)
            auxDT.Columns.RemoveAt(1)
            auxDT.Columns.RemoveAt(1)
            auxDT.Columns(0).ColumnName = "DOCUMENTOS"

        ElseIf cmbView.SelectedValue >= 1 And cmbView.SelectedValue <= 20 Then

            auxDT = gData_Get()

            '--- Tratamiento del Excel: eliminación de columnas no necesarias, cambios de nombres de columnas.
            Dim auxIndex As New List(Of Integer)
            For Each auxCol As DataColumn In auxDT.Columns
                Dim auxType As Integer = Val(Mid(auxCol.Caption, 8, 6))
                Dim auxCod As Integer = Val(Mid(auxCol.Caption, 15, 10))
                Select Case auxType
                    Case enumEntities.coEntityEMP 'sss
                        auxCol.ColumnName = auxEmpDT.Select("cod=" & auxCod)(0)("dsc")
                    Case enumEntities.coEntityUND
                        auxCol.ColumnName = auxEmpDT.Select("cod=" & auxCod)(0)("dsc")
                    Case enumEntities.coEntityDOC_DOCTIP
                        auxCol.ColumnName = auxEmpDT.Select("cod=" & auxCod)(0)("dsc")
                    Case enumEntities.coEntityDOC_DOC
                        auxCol.ColumnName = auxEmpDT.Select("cod=" & auxCod)(0)("dsc")
                End Select
                auxCol.ColumnName = auxCol.ColumnName.ToString.Replace(" ", "_").Replace(",", "").Replace(".", "")
                If Left(auxCol.ColumnName, 2).ToUpper = "Q_" Then
                    If auxCol.ColumnName.ToUpper = "Q_DSC" Then
                        auxCol.ColumnName = "DESCRIPCION"
                    Else
                        auxIndex.Add(auxDT.Columns.IndexOf(auxCol)) ' arma lista de columnas a remover
                    End If
                End If
            Next
            Dim auxCouDel As Integer = 0
            If auxIndex.Count > 0 Then
                For Each Val As Integer In auxIndex
                    auxDT.Columns.RemoveAt(Val - auxCouDel) ' Remueve las columnas de la lista
                    auxCouDel = auxCouDel + 1
                Next
            End If

        End If



        updupdpnlSearch.Update()

        auxClass.Conn.gConn_Close()

        Dim auxConnection As New imClientConnection
        Dim m_conn As clsHrcConnClient = Session("conn")
        Dim auxXLS As clsHrcConnClient.clsBinaryData = m_conn.gDataTable_ToExcelBinary(auxDT)
        Dim auxTmpGuid As String = m_conn.gField_GetUniqueID

        auxConnection.gFileTmp_Upload("reporte.xls", auxXLS.Content, -1, -1, auxTmpGuid)
        Dim auxScript As String = "hrcShowWindowNoModal(""hrcbinaries.aspx?tmp=1&tmpid=" & auxTmpGuid & """)"

        ClientScript.RegisterStartupScript(Page.GetType, "hrcdownloadXLS", "<script language=javascript>" & auxScript & "</script>")
    End Sub


    Public Function gReports_Data_GetSelectCommand(ByVal pBagValues As clshrcBagValues) As clshrcBagValues
        Dim auxClass As New clsCusimDOC
        auxClass.Conn.gConn_Open()
        Dim pCombo As Integer = pBagValues.gValue_Get("Combo")
        Dim auxDT1 As DataTable = pBagValues.gValue_Get("Consulta1")
        Dim auxDT2 As DataTable = pBagValues.gValue_Get("Consulta2")
        Dim auxDT3 As DataTable = pBagValues.gValue_Get("Consulta3")
        Dim auxIDFechas As String = pBagValues.gValue_Get("_idfiltros_")

        Dim auxDT3_Datos As Boolean = False
        If auxDT3 IsNot Nothing Then
            If auxDT3.Rows.Count > 0 Then
                auxDT3_Datos = True
            End If
        End If
        Dim auxError As Integer = pBagValues.gValue_Get("Error")
        Dim auxDTREsult As DataTable
        Dim auxReturn As New clshrcBagValues

        If auxClass.Conn.LastErrorDescription <> "" Then
            auxReturn.gValue_Add("DTRESULT", auxClass.Conn.LastErrorDescription)
            auxClass.gSys_DebugLogAdd("El Reporte [" & pCombo & "] fue cancelado por excepcion [" & auxClass.Conn.LastErrorDescription & "], el comando de error [" & auxClass.Conn.LastCommand & "]")
        End If

        Select Case pCombo
            Case 21, 22, 23, 24, 25, 26, 27, 28, 29, 30
                'Dim auxHierarchy As New clsHrcHierarchyTable '(Conn, "c:\windows\temp\hier.txt")
                Dim auxHierarchy As New clsHrcHierarchyTable(auxClass.Conn, "")
                auxHierarchy.gGroup_Add(auxDT1)
                If (pCombo = 24 And auxDT3 IsNot Nothing) Or (pCombo = 25 And auxDT3 IsNot Nothing) _
                Or (pCombo = 26 And auxDT3 IsNot Nothing) Or (pCombo = 22 And auxDT3 IsNot Nothing) _
                Or (pCombo = 29 And auxDT3 IsNot Nothing) Or (pCombo = 30 And auxDT3 IsNot Nothing) Then
                    auxHierarchy.gGroup_Add(auxDT2, -1, True)
                Else
                    auxHierarchy.gGroup_Add(auxDT2)
                End If
                auxHierarchy.gGroup_Add(auxDT3)

                '
                If pCombo = 22 Or pCombo = 24 Or pCombo = 25 Or pCombo = 26 Or pCombo = 29 Or pCombo = 30 Then
                    auxHierarchy.gLevel_SetColumnIndex(2, 5)
                End If


                'auxHierarchy.gDebug_On("c:\windows\temp\hier.txt")
                auxDTREsult = auxHierarchy.gHierarchy_GenerateTable
                auxReturn.gValue_Add("Hierarchy", auxHierarchy)

            Case Else
                auxDTREsult = auxDT1
        End Select


        Select pCombo
            Case 21, 22, 23, 24, 25, 26, 27, 28, 29, 30
                If auxDTREsult.Rows.Count <> 0 Then
                    Dim auxGrdData As New clshrcGrdData("", "", "", "", Nothing, "", False, True)
                    auxGrdData.gTreeGrid_Enabled("level", "parent", "isleaf", "expanded")
                    auxDTREsult = auxGrdData.gDataTable_Prepare(auxDTREsult)
                End If
        End Select

        Select Case pCombo
            Case 21, 32
                auxReturn.gValue_Add("_idfiltros_", auxIDFechas)
        End Select

        auxReturn.gValue_Add("DT", auxDTREsult)
        auxClass.Conn.gConn_Close()
        Return auxReturn


    End Function



    Public Function gReports_FiltrosWhere(ByVal pComboReporte As Integer, ByVal pTipoDocumento As List(Of Integer), ByVal pUnidadDocumento As List(Of Integer), ByVal pProceso As List(Of Integer), ByVal pColaborador As List(Of Integer), ByVal pUnidadColaborador As List(Of Integer), ByVal pTituloDocumento As String, ByVal pFechaDesde As String, ByVal pFechaHasta As String, ByVal pEstado As List(Of Integer)) As clshrcBagValues
        Dim auxClass As New clsCusimDOC
        auxClass.Conn.gConn_Close()
        Dim auxWhere As String = ""

        Dim FechaDesde As Date = auxClass.Conn.gField_GetDate(pFechaDesde)
        Dim FechaHasta As Date = auxClass.Conn.gField_GetDate(pFechaHasta)
        Select Case pComboReporte
            Case 21, 31
                If FechaDesde <> Nothing And FechaHasta <> Nothing Then
                    auxWhere &= " AND DOC_DOCREADS.qsecdatetime >= " & auxClass.Conn.gFieldDB_GetDateTime(FechaDesde) & " AND DOC_DOCREADS.qsecdatetime < " & auxClass.Conn.gFieldDB_GetDateTime(DateAdd(DateInterval.Day, 1, FechaHasta))
                ElseIf FechaDesde <> Nothing Then
                    auxWhere &= " AND DOC_DOCREADS.qsecdatetime >= " & auxClass.Conn.gFieldDB_GetDateTime(FechaDesde)
                ElseIf FechaHasta <> Nothing Then
                    auxWhere &= " AND DOC_DOCREADS.qsecdatetime < " & auxClass.Conn.gFieldDB_GetDateTime(DateAdd(DateInterval.Day, 1, FechaHasta))
                End If
            Case 23, 24, 25, 27, 29, 30, 32, 33
                If FechaDesde <> Nothing And FechaHasta <> Nothing Then
                    auxWhere &= " AND DOC_DOCLOG.fecha >= " & auxClass.Conn.gFieldDB_GetDateTime(FechaDesde) & " AND DOC_DOCLOG.fecha < " & auxClass.Conn.gFieldDB_GetDateTime(DateAdd(DateInterval.Day, 1, FechaHasta))
                ElseIf FechaDesde <> Nothing Then
                    auxWhere &= " AND DOC_DOCLOG.fecha  >= " & auxClass.Conn.gFieldDB_GetDateTime(FechaDesde)
                ElseIf FechaHasta <> Nothing Then
                    auxWhere &= " AND DOC_DOCLOG.fecha  < " & auxClass.Conn.gFieldDB_GetDateTime(DateAdd(DateInterval.Day, 1, FechaHasta))
                End If
            Case 26
                If FechaDesde <> Nothing And FechaHasta <> Nothing Then
                    auxWhere &= " AND DOC_DOCREF.qsecdatetime >= " & auxClass.Conn.gFieldDB_GetDateTime(FechaDesde) & " AND DOC_DOCREF.qsecdatetime < " & auxClass.Conn.gFieldDB_GetDateTime(DateAdd(DateInterval.Day, 1, FechaHasta))
                ElseIf FechaDesde <> Nothing Then
                    auxWhere &= " AND DOC_DOCREF.qsecdatetime  >= " & auxClass.Conn.gFieldDB_GetDateTime(FechaDesde)
                ElseIf FechaHasta <> Nothing Then
                    auxWhere &= " AND DOC_DOCREF.qsecdatetime  < " & auxClass.Conn.gFieldDB_GetDateTime(DateAdd(DateInterval.Day, 1, FechaHasta))
                End If
            Case 28
                If FechaDesde <> Nothing And FechaHasta <> Nothing Then
                    auxWhere &= " AND DOC_DOC.fecha >= " & auxClass.Conn.gFieldDB_GetDateTime(FechaDesde) & " AND DOC_DOC.fecha < " & auxClass.Conn.gFieldDB_GetDateTime(DateAdd(DateInterval.Day, 1, FechaHasta))
                ElseIf FechaDesde <> Nothing Then
                    auxWhere &= " AND DOC_DOC.fecha  >= " & auxClass.Conn.gFieldDB_GetDateTime(FechaDesde)
                ElseIf FechaHasta <> Nothing Then
                    auxWhere &= " AND DOC_DOC.fecha  < " & auxClass.Conn.gFieldDB_GetDateTime(DateAdd(DateInterval.Day, 1, FechaHasta))
                End If
        End Select

        If pTituloDocumento <> "" Then
            Select Case pComboReporte
                Case 21, 23, 24, 25, 27, 28, 29, 30, 31
                    auxWhere &= " AND DOC_DOC.dsc LIKE '%" & pTituloDocumento.Trim & "%' "
                Case 26
                    auxWhere &= " AND DOC_DOCREF.doccodref IN (SELECT DOC_DOCVIG.cod FROM DOC_DOCVIG WHERE DOC_DOCVIG.dsc LIKE '%" & pTituloDocumento.Trim & "%') "

            End Select
        End If

        If pTipoDocumento IsNot Nothing Then
            If pTipoDocumento.Count <> 0 Then
                Dim auxWhereaux As String = ""
                Select Case pComboReporte
                    Case 21, 23, 24, 25, 27, 28, 29, 30, 31
                        auxWhereaux &= "DOC_DOC.doctipcod IN (" & auxClass.Conn.gFieldDB_GetString(pTipoDocumento) & ") "
                    Case 26
                        auxWhereaux &= " DOC_DOCREF.doccodref IN (SELECT DOC_DOCVIG.cod FROM DOC_DOCVIG WHERE DOC_DOCVIG.doctipcod IN (" & auxClass.Conn.gFieldDB_GetString(pTipoDocumento) & ")) "

                End Select
                If auxWhereaux <> "" Then
                    auxWhere &= " AND (" & auxWhereaux & ")"
                End If
            End If
        End If

        If pUnidadDocumento IsNot Nothing Then
            If pUnidadDocumento.Count <> 0 Then
                Dim auxWhereaux As String = ""
                Select Case pComboReporte
                    Case 21, 23, 24, 25, 27, 28, 29, 30, 31
                        auxWhereaux &= "DOC_DOC.undcod IN (" & auxClass.Conn.gFieldDB_GetString(pUnidadDocumento) & ") "
                    Case 26
                        auxWhereaux &= " DOC_DOCREF.doccodref IN (SELECT DOC_DOCVIG.cod FROM DOC_DOCVIG WHERE DOC_DOCVIG.undcod IN (" & auxClass.Conn.gFieldDB_GetString(pUnidadDocumento) & ")) "

                End Select
                If auxWhereaux <> "" Then
                    auxWhere &= " AND (" & auxWhereaux & ")"
                End If
            End If
        End If

        If pProceso IsNot Nothing Then
            If pProceso.Count <> 0 Then
                Dim auxWhereaux As String = ""
                Select Case pComboReporte
                    Case 21, 23, 24, 25, 27, 28, 29, 30, 31
                        auxWhereaux &= "DOC_DOC.procod IN (" & auxClass.Conn.gFieldDB_GetString(pProceso) & ") "
                    Case 26
                        auxWhereaux &= " DOC_DOCREF.doccodref IN (SELECT DOC_DOCVIG.cod FROM DOC_DOCVIG WHERE DOC_DOCVIG.procod IN (" & auxClass.Conn.gFieldDB_GetString(pProceso) & ")) "


                End Select
                If auxWhereaux <> "" Then
                    auxWhere &= " AND (" & auxWhereaux & ")"
                End If
            End If
        End If

        If pColaborador IsNot Nothing Then
            If pColaborador.Count <> 0 Then
                Dim auxWhereaux As String = ""
                Select Case pComboReporte
                    Case 21, 31
                        auxWhereaux &= "DOC_DOCREADS.empcod IN (" & auxClass.Conn.gFieldDB_GetString(pColaborador) & ") "
                    Case 23, 24, 25, 27, 29, 30, 32, 33
                        auxWhereaux &= "DOC_DOCLOG.empcod IN (" & auxClass.Conn.gFieldDB_GetString(pColaborador) & ") "
                    Case 28
                        auxWhereaux &= "DOC_DOCSGN.empcod IN (" & auxClass.Conn.gFieldDB_GetString(pColaborador) & ") "

                End Select
                If auxWhereaux <> "" Then
                    auxWhere &= " AND (" & auxWhereaux & ")"
                End If
            End If
        End If

        If pUnidadColaborador IsNot Nothing Then
            If pUnidadColaborador.Count <> 0 Then
                Dim auxWhereaux As String = ""
                Select Case pComboReporte
                    Case 21, 31
                        auxWhereaux &= "DOC_DOCREADS.empcod IN (" _
                        & " SELECT cod FROM EMP WHERE undcod IN (" & auxClass.Conn.gFieldDB_GetString(pUnidadColaborador) & ") " _
                        & ") "
                    Case 23, 24, 25, 27, 29, 30, 32, 33
                        auxWhereaux &= "DOC_DOCLOG.empcod IN (" _
                        & " SELECT cod FROM EMP WHERE undcod IN (" & auxClass.Conn.gFieldDB_GetString(pUnidadColaborador) & ") " _
                        & ") "
                    Case 28
                        auxWhereaux &= "DOC_DOCSGN.empcod IN (" _
                        & " SELECT cod FROM EMP WHERE undcod IN (" & auxClass.Conn.gFieldDB_GetString(pUnidadColaborador) & ") " _
                        & ") "
                End Select
                If auxWhereaux <> "" Then
                    auxWhere &= " AND (" & auxWhereaux & ")"
                End If
            End If
        End If

        If pEstado IsNot Nothing Then
            If pEstado.Count <> 0 Then
                Dim auxWhereaux As String = ""
                Select Case pComboReporte
                    Case 21, 23, 24, 25, 27, 28, 29, 30, 31
                        auxWhereaux &= "DOC_DOC.wfwstatus IN (" & auxClass.Conn.gFieldDB_GetString(pEstado) & ") "
                End Select
                If auxWhereaux <> "" Then
                    auxWhere &= " AND (" & auxWhereaux & ")"
                End If
            End If
        End If

        auxClass.Conn.gConn_Close()
        Dim auxFiltrosWhere As New clshrcBagValues
        auxFiltrosWhere.gValue_Add("Where", auxWhere)
        auxFiltrosWhere.gValue_Add("Combo", pComboReporte)
        Return auxFiltrosWhere

        'Return auxWhere
    End Function

    Public Function gReports_FiltrosAgrupacion(ByVal pComboReporte As Integer, ByVal pAgrupacion As Integer) As clshrcBagValues

        'AGRUPACION
        Dim auxConsultaAnidar As String = ""
        Dim auxAnidarCon As String = ""
        Dim auxAnidarCampo As String = ""
        Dim auxAnidarEntidad As String = ""
        Dim auxTabla As String = ""
        If pAgrupacion > 0 Then
            If pAgrupacion = 1 Then 'Agrupa Por Unidad
                auxConsultaAnidar = "SELECT cod as q_cod,dsc as q_dsc,NULL,NULL, " _
                            & enumEntities.coEntityUND & " as q_type" _
                            & " FROM UND " _
                            & " ORDER BY dsc "
                auxAnidarCampo = "undcod "
                auxAnidarEntidad = enumEntities.coEntityUND
                auxTabla = "UND"
                Select Case pComboReporte
                    Case 21, 23, 24, 25, 27, 28, 29, 30, 31
                        auxAnidarCon = "DOC_DOC.undcod "
                    Case 22, 26
                        auxAnidarCon = "DOC_DOCVIG.undcod "
                End Select
            End If
            If pAgrupacion = 2 Then 'Agrupa Por Colaborador
                auxConsultaAnidar = "SELECT cod as q_cod,dsc as q_dsc,NULL,NULL, " _
                            & enumEntities.coEntityEMP & " as q_type" _
                            & " FROM EMP " _
                            & " ORDER BY dsc "
                auxAnidarCampo = "empcod "
                auxAnidarEntidad = enumEntities.coEntityEMP
                auxTabla = "EMP"
                Select Case pComboReporte
                    Case 21, 31
                        auxAnidarCon = "DOC_DOCREADS.empcod "
                    Case 23, 24, 25, 27, 29, 30
                        auxAnidarCon = "DOC_DOCLOG.empcod "
                    Case 28
                        auxAnidarCon = "DOC_DOCSGN.empcod "
                End Select
            End If
            If pAgrupacion = 3 Then 'Agrupa Por Tipo de sistema
                auxConsultaAnidar = "SELECT cod as q_cod,dsc as q_dsc,NULL,NULL, " _
                            & enumEntities.coEntityDOC_SIS & " as q_type" _
                            & " FROM DOC_SIS " _
                            & " ORDER BY dsc "
                auxAnidarCampo = "siscod "
                auxAnidarEntidad = enumEntities.coEntityDOC_SIS
                auxTabla = "DOC_SIS"
                Select Case pComboReporte
                    Case 21, 23, 24, 25, 27, 28, 29, 30, 31
                        auxAnidarCon = "DOC_DOC.siscod "
                    Case 22, 26
                        auxAnidarCon = "DOC_DOCVIG.siscod "
                End Select
            End If
            If pAgrupacion = 4 Then 'Agrupa Por Proceso
                auxConsultaAnidar = "SELECT cod as q_cod,dsc as q_dsc,NULL,NULL, " _
                            & enumEntities.coEntityDOC_PRO & " as q_type" _
                            & " FROM DOC_PRO " _
                            & " ORDER BY dsc "
                auxAnidarCampo = "procod "
                auxAnidarEntidad = enumEntities.coEntityDOC_PRO
                auxTabla = "DOC_PRO"
                Select Case pComboReporte
                    Case 21, 23, 24, 25, 27, 28, 29, 30, 31
                        auxAnidarCon = "DOC_DOC.procod "
                    Case 22, 26
                        auxAnidarCon = "DOC_DOCVIG.procod "
                End Select
            End If
            If pAgrupacion = 5 Then 'Agrupa Por Tipo de documento
                auxConsultaAnidar = "SELECT cod as q_cod,dsc as q_dsc,NULL,NULL, " _
                            & enumEntities.coEntityDOC_DOCTIP & " as q_type" _
                            & " FROM DOC_DOCTIP " _
                            & " ORDER BY dsc "
                auxAnidarCampo = "doctipcod "
                auxAnidarEntidad = enumEntities.coEntityDOC_DOCTIP
                auxTabla = "DOC_DOCTIP"
                Select Case pComboReporte
                    Case 21, 23, 24, 25, 27, 28, 29, 30, 31
                        auxAnidarCon = "DOC_DOC.doctipcod "
                    Case 22, 26
                        auxAnidarCon = "DOC_DOCVIG.doctipcod "
                End Select
            End If
            If pAgrupacion = 6 Then 'Agrupa Por Estado
                auxConsultaAnidar = "SELECT wfwstpcod as q_cod,wfwstpdsc as q_dsc,NULL,NULL, " _
                            & 111111 & " as q_type" _
                            & " FROM Q_WFWSTP " _
                            & " ORDER BY wfwstpdsc "
                auxAnidarCampo = "wfwstatus "
                auxAnidarEntidad = 111111
                auxTabla = "Q_WFWSTP"
                Select Case pComboReporte
                    Case 21, 23, 24, 25, 27, 28, 29, 30, 31
                        auxAnidarCon = "DOC_DOC.wfwstatus "
                End Select
            End If
            If pAgrupacion = 7 Then 'Agrupa Por Documentos
                auxConsultaAnidar = "SELECT cod as q_cod,dsc as q_dsc,NULL,NULL, " _
                            & enumEntities.coEntityDOC_DOC & " as q_type" _
                            & " FROM DOC_DOC " _
                            & " ORDER BY dsc "
                auxAnidarCampo = "cod "
                auxAnidarEntidad = enumEntities.coEntityDOC_DOC
                auxTabla = "DOC_DOC"
                Select Case pComboReporte
                    Case 24, 25, 29, 30
                        auxAnidarCon = "DOC_DOC.cod "
                End Select
            End If


        Else
            'no se agrupo
            auxConsultaAnidar = "NULL"
            auxAnidarCon = "NULL"
            auxAnidarCampo = "NULL"
            auxAnidarEntidad = -1
            auxTabla = "NULL"
        End If

        Dim auxFiltrosAgrupacion As New clshrcBagValues
        auxFiltrosAgrupacion.gValue_Add("auxConsultaAnidar", auxConsultaAnidar)
        auxFiltrosAgrupacion.gValue_Add("auxAnidarCon", auxAnidarCon)
        auxFiltrosAgrupacion.gValue_Add("auxAnidarCampo", auxAnidarCampo)
        auxFiltrosAgrupacion.gValue_Add("auxAnidarEntidad", auxAnidarEntidad)
        auxFiltrosAgrupacion.gValue_Add("auxTabla", auxTabla)
        auxFiltrosAgrupacion.gValue_Add("pComboReporte", pComboReporte)

        Return auxFiltrosAgrupacion

    End Function




    'Reporte 21 - Documentos mas leidos         
    Public Function gReports_DocumentosMasLeidos(ByVal pComboReporte As Integer, ByVal pTipoResultado As Integer, ByVal pAgrupacion As Integer, ByVal pTipoDocumento As List(Of Integer), ByVal pUnidadDocumento As List(Of Integer), ByVal pProceso As List(Of Integer), ByVal pColaborador As List(Of Integer), ByVal pUnidadColaborador As List(Of Integer), ByVal pTituloDocumento As String, ByVal pFechaDesde As String, ByVal pFechaHasta As String, ByVal pEstado As List(Of Integer)) As clshrcBagValues
        Dim auxClass As New clsCusimDOC
        auxClass.Conn.gConn_Open()
        'FILTROS
        Dim pBagValues As New clshrcBagValues
        pBagValues = gReports_FiltrosWhere(pComboReporte, pTipoDocumento, pUnidadDocumento, pProceso, pColaborador, pUnidadColaborador, pTituloDocumento, pFechaDesde, pFechaHasta, pEstado)
        Dim pWhere As String = pBagValues.gValue_Get("Where")

        'AGRUPACIONES
        Dim pBagValues2 As New clshrcBagValues
        pBagValues2 = gReports_FiltrosAgrupacion(pComboReporte, pAgrupacion)
        Dim auxConsultaAnidar As String = pBagValues2.gValue_Get("auxConsultaAnidar")
        Dim auxAnidarCon As String = pBagValues2.gValue_Get("auxAnidarCon")
        Dim auxTabla As String = pBagValues2.gValue_Get("auxTabla")

        Dim auxAnidarColaborador As String = ""
        If auxAnidarCon = "DOC_DOCREADS.empcod " Then
            auxAnidarColaborador = "," & auxAnidarCon
            auxAnidarCon = "tabla.empcod"
        End If

        Dim auxSelect2 As String = ""
        Dim auxFiltroAgrupacion As String = ""
        Dim auxAgrupacion As String = ""
        If pAgrupacion > 0 Then
            auxSelect2 = auxConsultaAnidar
            Dim auxCod As String = ""
            If auxTabla = "Q_WFWSTP" Then
                auxCod = ".wfwstpcod "
            Else
                auxCod = ".cod "
            End If
            auxFiltroAgrupacion = " LEFT JOIN " & auxTabla & " ON " & auxTabla & auxCod & " = " & auxAnidarCon & " "
            auxAgrupacion = " ," & auxTabla & auxCod & " as agrupacioncod "
        Else
            auxAgrupacion = " ,-1 as agrupacioncod "

        End If


        Dim auxSelect1 As String = " SELECT DOC_DOC.cod as q_cod,DOC_DOC.dsc as q_dsc,NULL," & auxAnidarCon & "," & enumEntities.coEntityDOC_DOC & " as q_type " _
                            & " ,DOC_DOC.cod, tabla.cantidad " & auxAgrupacion _
                            & " FROM DOC_DOC " _
                            & " LEFT JOIN ( " _
                                & " SELECT DOC_DOCREADS.doccod, COUNT(DOC_DOCREADS.cod) as cantidad " & auxAnidarColaborador _
                                & " FROM DOC_DOC " _
                                & " LEFT JOIN DOC_DOCREADS ON DOC_DOCREADS.doccod = DOC_DOC.cod " _
                                & " WHERE (DOC_DOC.baja = {#FALSE#} OR DOC_DOC.baja {#ISNULL#}) AND DOC_DOC.cod > 0 AND DOC_DOCREADS.cod > 0 " _
                                & pWhere _
                                & " GROUP BY DOC_DOCREADS.doccod " & auxAnidarColaborador _
                            & " ) as tabla ON tabla.doccod=DOC_DOC.cod " _
                            & auxFiltroAgrupacion _
                            & " WHERE tabla.cantidad > 0 " _
                            & " ORDER BY tabla.cantidad DESC"

        Dim auxDT1 As DataTable = auxClass.Conn.gConn_Query(auxSelect1)
        Dim auxi As Integer = 1
        For Each auxRow As DataRow In auxDT1.Rows
            auxRow("q_cod") = auxi
            auxi = auxi + 1
        Next
        auxi = 1 ' reset 
        Dim auxDT2 As DataTable = auxClass.Conn.gConn_Query(auxSelect2)


        'Paquete de valores - FECHAS ------------------
        Dim auxDateDesde As Date = auxClass.Conn.gField_GetDate(pFechaDesde)
        Dim auxDateHasta As Date = auxClass.Conn.gField_GetDate(pFechaHasta)

        Dim auxBagValues As New clshrcBagValues
        auxBagValues.gValue_Add("_desde_", auxClass.Conn.gFieldDB_GetDateTimeStandard(auxDateDesde))
        auxBagValues.gValue_Add("_hasta_", auxClass.Conn.gFieldDB_GetDateTimeStandard(auxDateHasta))
        auxBagValues.gValue_Add("_agrupacionCampo_", auxAnidarCon)
        auxBagValues.gValue_Add("_tituloDOC_", pTituloDocumento)


        Dim auxConnection As New imClientConnection
        Dim auxIDFechas As String = auxConnection.gObjectTmp_Upload(auxBagValues)
        '------------------------------------------


        Dim auxConsultasValues As New clshrcBagValues
        auxConsultasValues.gValue_Add("Consulta1", auxDT1)
        auxConsultasValues.gValue_Add("Consulta2", auxDT2)
        auxConsultasValues.gValue_Add("Consulta3", Nothing)
        auxConsultasValues.gValue_Add("_idfiltros_", auxIDFechas)
        auxConsultasValues.gValue_Add("Combo", pComboReporte)
        Return auxConsultasValues
    End Function

    'Reporte 22 - Versiones de documentos      
    Public Function gReports_VersionesDocumentos(ByVal pComboReporte As Integer, ByVal pTipoResultado As Integer, ByVal pAgrupacion As Integer) As clshrcBagValues
        Dim auxClass As New clsCusimDOC

        'AGRUPACIONES
        Dim pBagValues2 As New clshrcBagValues
        pBagValues2 = gReports_FiltrosAgrupacion(pComboReporte, pAgrupacion)
        Dim auxConsultaAnidar As String = pBagValues2.gValue_Get("auxConsultaAnidar")
        Dim auxAnidarCon As String = pBagValues2.gValue_Get("auxAnidarCon")
        Dim auxTabla As String = pBagValues2.gValue_Get("auxTabla")

        Dim auxSelect3 As String = ""
        Dim auxDT3 As DataTable = Nothing
        If pAgrupacion > 0 Then
            auxSelect3 = auxConsultaAnidar
            auxDT3 = auxClass.Conn.gConn_Query(auxSelect3)
        End If

        Dim auxSelect2 As String = " SELECT DISTINCT DOC_DOCVIG.version as q_cod,'Versión ' + CAST(DOC_DOCVIG.version as varchar(12)) as q_dsc,NULL,NULL," & 111111 & " as q_type " _
                            & " FROM DOC_DOCVIG " _
                            & " WHERE DOC_DOCVIG.cod > 0 "

        Dim auxSelect1 As String = " SELECT DOC_DOCVIG.cod as q_cod,DOC_DOCVIG.dsc as q_dsc,NULL,DOC_DOCVIG.version as anidar," & enumEntities.coEntityDOC_DOCVIG & " as q_type " _
                            & " ," & auxAnidarCon & ",DOC_DOCVIG.fecha,DOC_DOCVIG.version,DOC_DOCVIG.cod " _
                            & " FROM DOC_DOCVIG " _
                            & " WHERE DOC_DOCVIG.cod > 0 "

        Dim auxDT1 As DataTable = auxClass.Conn.gConn_Query(auxSelect1)
        Dim auxDT2 As DataTable = auxClass.Conn.gConn_Query(auxSelect2)

        Dim auxConsultasValues As New clshrcBagValues
        auxConsultasValues.gValue_Add("Consulta1", auxDT1)
        auxConsultasValues.gValue_Add("Consulta2", auxDT2)
        auxConsultasValues.gValue_Add("Consulta3", auxDT3)
        auxConsultasValues.gValue_Add("Combo", pComboReporte)
        Return auxConsultasValues
    End Function

    'Reporte 23 - Impresiones de documentos          
    Public Function gReports_ImpresionesDocumentos(ByVal pComboReporte As Integer, ByVal pTipoResultado As Integer, ByVal pAgrupacion As Integer, ByVal pTipoDocumento As List(Of Integer), ByVal pUnidadDocumento As List(Of Integer), ByVal pProceso As List(Of Integer), ByVal pColaborador As List(Of Integer), ByVal pUnidadColaborador As List(Of Integer), ByVal pTituloDocumento As String, ByVal pFechaDesde As String, ByVal pFechaHasta As String, ByVal pEstado As List(Of Integer)) As clshrcBagValues
        Dim auxClass As New clsCusimDOC
        'FILTROS
        Dim pBagValues As New clshrcBagValues
        pBagValues = gReports_FiltrosWhere(pComboReporte, pTipoDocumento, pUnidadDocumento, pProceso, pColaborador, pUnidadColaborador, pTituloDocumento, pFechaDesde, pFechaHasta, pEstado)
        Dim pWhere As String = pBagValues.gValue_Get("Where")

        'AGRUPACIONES
        Dim pBagValues2 As New clshrcBagValues
        pBagValues2 = gReports_FiltrosAgrupacion(pComboReporte, pAgrupacion)
        Dim auxConsultaAnidar As String = pBagValues2.gValue_Get("auxConsultaAnidar")
        Dim auxAnidarCon As String = pBagValues2.gValue_Get("auxAnidarCon")

        Dim auxSelect2 As String = ""
        If pAgrupacion > 0 Then
            auxSelect2 = auxConsultaAnidar
        End If


        Dim auxAnidarColaborador As String = ""
        If auxAnidarCon = "DOC_DOCLOG.empcod " Then
            auxAnidarColaborador = "," & auxAnidarCon
            auxAnidarCon = "tabla.empcod"
        End If

        Dim auxSelect1 As String = " SELECT DOC_DOC.cod as q_cod,DOC_DOC.dsc as q_dsc,NULL," & auxAnidarCon & "," & enumEntities.coEntityDOC_DOC & " as q_type " _
                            & " ,DOC_DOC.cod, tabla.cantidad as cantidad_controlada, tabla2.cantidad as cantidad_no_controlada " _
                            & " FROM DOC_DOC " _
                            & " LEFT JOIN ( " _
                                & " SELECT DOC_DOCLOG.doccod, COUNT(DOC_DOCLOG.cod) as cantidad " & auxAnidarColaborador _
                                & " FROM DOC_DOC " _
                                & " LEFT JOIN DOC_DOCLOG ON DOC_DOCLOG.doccod = DOC_DOC.cod " _
                                & " WHERE (DOC_DOC.baja = {#FALSE#} OR DOC_DOC.baja {#ISNULL#}) AND DOC_DOC.cod > 0 AND DOC_DOCLOG.cod > 0 " _
                                & " AND DOC_DOCLOG.dsc LIKE '%" & coCopiaControladaTexto & "%' " _
                                & pWhere _
                                & " GROUP BY DOC_DOCLOG.doccod " & auxAnidarColaborador _
                            & " ) as tabla ON tabla.doccod=DOC_DOC.cod " _
                            & " LEFT JOIN ( " _
                                & " SELECT DOC_DOCLOG.doccod, COUNT(DOC_DOCLOG.cod) as cantidad " & auxAnidarColaborador _
                                & " FROM DOC_DOC " _
                                & " LEFT JOIN DOC_DOCLOG ON DOC_DOCLOG.doccod = DOC_DOC.cod " _
                                & " WHERE (DOC_DOC.baja = {#FALSE#} OR DOC_DOC.baja {#ISNULL#}) AND DOC_DOC.cod > 0 AND DOC_DOCLOG.cod > 0 " _
                                & " AND DOC_DOCLOG.dsc LIKE '%" & coCopiaNoControladaTexto & "%' " _
                                & pWhere _
                                & " GROUP BY DOC_DOCLOG.doccod " & auxAnidarColaborador _
                            & " ) as tabla2 ON tabla2.doccod=DOC_DOC.cod " _
                            & " WHERE tabla.cantidad > 0 OR tabla2.cantidad > 0 " _
                            & " ORDER BY tabla.cantidad DESC,tabla2.cantidad DESC"

        Dim auxDT1 As DataTable = auxClass.Conn.gConn_Query(auxSelect1)
        Dim auxi As Integer = 1
        For Each auxRow As DataRow In auxDT1.Rows
            auxRow("q_cod") = auxi
            auxi = auxi + 1
        Next
        auxi = 1 ' reset 
        Dim auxDT2 As DataTable = auxClass.Conn.gConn_Query(auxSelect2)


        Dim auxConsultasValues As New clshrcBagValues
        auxConsultasValues.gValue_Add("Consulta1", auxDT1)
        auxConsultasValues.gValue_Add("Consulta2", auxDT2)
        auxConsultasValues.gValue_Add("Consulta3", Nothing)
        auxConsultasValues.gValue_Add("Combo", pComboReporte)
        Return auxConsultasValues
    End Function

    'Reporte 24 - Editores -- 25 - Impresores -- 29 - Revisores -- 30 - Publicadores              
    Public Function gReports_ColaboradoresEnDOC(ByVal pComboReporte As Integer, ByVal pTipoResultado As Integer, ByVal pAgrupacion As Integer, ByVal pTipoDocumento As List(Of Integer), ByVal pUnidadDocumento As List(Of Integer), ByVal pProceso As List(Of Integer), ByVal pColaborador As List(Of Integer), ByVal pUnidadColaborador As List(Of Integer), ByVal pTituloDocumento As String, ByVal pFechaDesde As String, ByVal pFechaHasta As String, ByVal pEstado As List(Of Integer)) As clshrcBagValues
        Dim auxClass As New clsCusimDOC
        'FILTROS
        Dim pBagValues As New clshrcBagValues
        pBagValues = gReports_FiltrosWhere(pComboReporte, pTipoDocumento, pUnidadDocumento, pProceso, pColaborador, pUnidadColaborador, pTituloDocumento, pFechaDesde, pFechaHasta, pEstado)
        Dim pWhere As String = pBagValues.gValue_Get("Where")

        'AGRUPACIONES
        Dim pBagValues2 As New clshrcBagValues
        pBagValues2 = gReports_FiltrosAgrupacion(pComboReporte, pAgrupacion)
        Dim auxConsultaAnidar As String = pBagValues2.gValue_Get("auxConsultaAnidar")
        Dim auxAnidarCon As String = pBagValues2.gValue_Get("auxAnidarCon")

        Dim auxSelect3 As String = ""
        Dim auxDT3 As DataTable = Nothing
        If pAgrupacion > 0 Then
            auxSelect3 = auxConsultaAnidar
            auxDT3 = auxClass.Conn.gConn_Query(auxSelect3)
        End If


        Dim auxSelect2 As String = "SELECT cod as q_cod,dsc as q_dsc,NULL,NULL, " _
                                 & enumEntities.coEntityEMP & " as q_type,cod " _
                                 & " FROM EMP WHERE cod > 0 " _
                                 & " UNION " _
                                 & " SELECT cod as q_cod,dsc as q_dsc,NULL,NULL, " _
                                 & enumEntities.coEntityDOC_SIS & " as q_type,cod " _
                                 & " FROM DOC_SIS WHERE cod > 0 " _
                                 & " ORDER BY dsc "
        Dim auxDT2 As DataTable = auxClass.Conn.gConn_Query(auxSelect2)
        Dim auxcod As Integer = 1
        For Each auxRows As DataRow In auxDT2.Rows
            auxRows("q_cod") = auxcod
            auxcod = auxcod + 1
        Next


        Dim auxFiltro As String = ""
        If pComboReporte = 24 Then 'Editores
            auxFiltro = " AND DOC_DOCLOG.wfwstepnext=" & enumWorkflowStep.coWFWSTPDOC_DOCEdicionOK
        ElseIf pComboReporte = 25 Then 'Impresores
            auxFiltro = " AND (DOC_DOCLOG.dsc LIKE '%" & coCopiaControladaTexto & "%' OR DOC_DOCLOG.dsc LIKE '%" & coCopiaNoControladaTexto & "%')  "
        ElseIf pComboReporte = 29 Then 'Revisores
            auxFiltro = " AND DOC_DOCLOG.wfwstepnext=" & enumWorkflowStep.coWFWSTPDOC_DOCrevisionOK
        ElseIf pComboReporte = 30 Then 'Publicadores
            auxFiltro = " AND DOC_DOCLOG.wfwstepnext=" & enumWorkflowStep.coWFWSTPDOC_DOCpublicacionOK
        End If

        Dim auxSelect1 As String = " SELECT DOC_DOC.cod as q_cod,DOC_DOC.dsc as q_dsc,NULL,(CASE WHEN DOC_DOCLOG.empcod = -1 THEN DOC_DOC.siscod ELSE DOC_DOCLOG.empcod END) as anida," & enumEntities.coEntityDOC_DOC & " as q_type " _
                            & "," & auxAnidarCon & " ,DOC_DOC.cod,DOC_DOC_HST.version,DOC_DOC_HST.fecha,DOC_DOCLOG.fecha as fecha_impresion " _
                            & " ,DOC_DOCLOG.hsthidgencod,DOC_DOCLOG.cod as logcod,(CASE WHEN DOC_DOCLOG.empcod = -1 THEN " & enumEntities.coEntityDOC_SIS & " ELSE " & enumEntities.coEntityEMP & " END) as empsiscod " _
                            & " FROM DOC_DOC " _
                            & " LEFT JOIN DOC_DOCLOG ON DOC_DOCLOG.doccod = DOC_DOC.cod " _
                            & " LEFT JOIN DOC_DOC_HST ON DOC_DOCLOG.hsthidgencod = DOC_DOC_HST.hstcod " _
                            & " WHERE (DOC_DOC.baja = {#FALSE#} OR DOC_DOC.baja {#ISNULL#}) AND DOC_DOC.cod > 0 AND DOC_DOCLOG.cod > 0 " _
                            & auxFiltro _
                            & pWhere


        Dim auxDT1 As DataTable = auxClass.Conn.gConn_Query(auxSelect1)
        Dim auxi As Integer = 1
        For Each auxRow As DataRow In auxDT1.Rows
            If auxClass.Conn.gField_GetInt(auxRow("hsthidgencod"), -1) = -1 Then
                Dim auxDT_AUX As DataTable = auxClass.Conn.gConn_Query("SELECT cod,hsthidgencod " _
                                                                     & " FROM DOC_DOCLOG " _
                                                                     & " WHERE doccod=" & auxClass.Conn.gFieldDB_GetInt(auxRow("cod")) _
                                                                     & " ORDER BY fecha DESC")

                If auxDT_AUX.Rows.Count > 0 Then
                    Dim auxEncontro As Boolean = False
                    For Each auxX As DataRow In auxDT_AUX.Rows
                        If auxEncontro = True Then
                            If auxX("hsthidgencod") > 0 Then
                                Dim auxDT_HST As DataTable = auxClass.Conn.gConn_Query("SELECT fecha,version " _
                                                            & " FROM DOC_DOC_HST " _
                                                            & " WHERE hstcod=" & auxClass.Conn.gFieldDB_GetInt(auxX("hsthidgencod")))

                                If auxDT_HST.Rows.Count > 0 Then
                                    auxRow("fecha") = auxDT_HST.Rows(0)("fecha")
                                    auxRow("version") = auxDT_HST.Rows(0)("version")
                                    auxEncontro = False
                                End If
                            End If
                        End If
                        If auxX("cod") = auxRow("logcod") Then
                            auxEncontro = True
                        End If
                    Next
                End If
            End If

            'Asigno codigo de anidacion o empcod o siscod segun corresponda
            auxRow("anida") = auxDT2.Select("cod=" & auxRow("anida") & " AND q_type=" & auxRow("empsiscod"))(0)("q_cod")


            auxRow("q_cod") = auxi
            auxi = auxi + 1
        Next
        auxi = 1 ' reset 



        Dim auxConsultasValues As New clshrcBagValues
        auxConsultasValues.gValue_Add("Consulta1", auxDT1)
        auxConsultasValues.gValue_Add("Consulta2", auxDT2)
        auxConsultasValues.gValue_Add("Consulta3", auxDT3)
        auxConsultasValues.gValue_Add("Combo", pComboReporte)
        Return auxConsultasValues
    End Function


    'Reporte 26 - Referencia entre documentos           
    Public Function gReports_ReferenciaEntreDocumentos(ByVal pComboReporte As Integer, ByVal pTipoResultado As Integer, ByVal pAgrupacion As Integer, ByVal pTipoDocumento As List(Of Integer), ByVal pUnidadDocumento As List(Of Integer), ByVal pProceso As List(Of Integer), ByVal pColaborador As List(Of Integer), ByVal pUnidadColaborador As List(Of Integer), ByVal pTituloDocumento As String, ByVal pFechaDesde As String, ByVal pFechaHasta As String) As clshrcBagValues
        Dim auxClass As New clsCusimDOC
        'FILTROS
        Dim pBagValues As New clshrcBagValues
        pBagValues = gReports_FiltrosWhere(pComboReporte, pTipoDocumento, pUnidadDocumento, pProceso, pColaborador, pUnidadColaborador, pTituloDocumento, pFechaDesde, pFechaHasta, Nothing)
        Dim pWhere As String = pBagValues.gValue_Get("Where")

        'AGRUPACIONES
        Dim pBagValues2 As New clshrcBagValues
        pBagValues2 = gReports_FiltrosAgrupacion(pComboReporte, pAgrupacion)
        Dim auxConsultaAnidar As String = pBagValues2.gValue_Get("auxConsultaAnidar")
        Dim auxAnidarCon As String = pBagValues2.gValue_Get("auxAnidarCon")

        Dim auxSelect3 As String = ""
        Dim auxDT3 As DataTable = Nothing
        If pAgrupacion > 0 Then
            auxSelect3 = auxConsultaAnidar
            auxDT3 = auxClass.Conn.gConn_Query(auxSelect3)
        End If


        Dim auxSelect2 As String = "SELECT cod as q_cod,dsc as q_dsc,NULL,NULL, " _
                           & enumEntities.coEntityDOC_DOCVIG & " as q_type,NULL,cod,version,fecha " _
                           & " FROM DOC_DOCVIG " _
                           & " ORDER BY dsc "


        Dim auxSelect1 As String = " SELECT DOC_DOCREF.doccodref as q_cod,(SELECT dsc FROM DOC_DOCVIG WHERE cod=DOC_DOCREF.doccodref) as q_dsc " _
                            & " ,NULL,DOC_DOCREF.doccod," & enumEntities.coEntityDOC_DOCVIG & " as q_type " _
                            & " ," & auxAnidarCon & ",DOC_DOCREF.doccodref as cod " _
                            & " ,(SELECT version FROM DOC_DOCVIG WHERE cod=DOC_DOCREF.doccodref) as version " _
                            & " ,(SELECT fecha FROM DOC_DOCVIG WHERE cod=DOC_DOCREF.doccodref) as fecha " _
                            & " FROM DOC_DOCVIG " _
                            & " INNER JOIN DOC_DOCREF ON DOC_DOCREF.doccod = DOC_DOCVIG.cod " _
                            & " WHERE DOC_DOCVIG.cod > 0 AND DOC_DOCREF.cod > 0 " _
                            & pWhere


        Dim auxDT1 As DataTable = auxClass.Conn.gConn_Query(auxSelect1)
        Dim auxi As Integer = 1
        For Each auxRow As DataRow In auxDT1.Rows
            auxRow("q_cod") = auxi
            auxi = auxi + 1
        Next
        auxi = 1 ' reset 
        Dim auxDT2 As DataTable = auxClass.Conn.gConn_Query(auxSelect2)


        Dim auxConsultasValues As New clshrcBagValues
        auxConsultasValues.gValue_Add("Consulta1", auxDT1)
        auxConsultasValues.gValue_Add("Consulta2", auxDT2)
        auxConsultasValues.gValue_Add("Consulta3", auxDT3)
        auxConsultasValues.gValue_Add("Combo", pComboReporte)
        Return auxConsultasValues
    End Function

    'Reporte 27 - Actividad          
    Public Function gReports_Actividad(ByVal pComboReporte As Integer, ByVal pTipoResultado As Integer, ByVal pAgrupacion As Integer, ByVal pTipoDocumento As List(Of Integer), ByVal pUnidadDocumento As List(Of Integer), ByVal pProceso As List(Of Integer), ByVal pColaborador As List(Of Integer), ByVal pUnidadColaborador As List(Of Integer), ByVal pTituloDocumento As String, ByVal pFechaDesde As String, ByVal pFechaHasta As String, ByVal pEstado As List(Of Integer)) As clshrcBagValues
        Dim auxClass As New clsCusimDOC
        'FILTROS
        Dim pBagValues As New clshrcBagValues
        pBagValues = gReports_FiltrosWhere(pComboReporte, pTipoDocumento, pUnidadDocumento, pProceso, pColaborador, pUnidadColaborador, pTituloDocumento, pFechaDesde, pFechaHasta, pEstado)
        Dim pWhere As String = pBagValues.gValue_Get("Where")

        'AGRUPACIONES
        Dim pBagValues2 As New clshrcBagValues
        pBagValues2 = gReports_FiltrosAgrupacion(pComboReporte, pAgrupacion)
        Dim auxConsultaAnidar As String = pBagValues2.gValue_Get("auxConsultaAnidar")
        Dim auxAnidarCon As String = pBagValues2.gValue_Get("auxAnidarCon")

        Dim auxSelect2 As String = ""
        Dim auxDT2 As DataTable = Nothing
        If pAgrupacion > 0 Then
            auxSelect2 = auxConsultaAnidar
            auxDT2 = auxClass.Conn.gConn_Query(auxSelect2)
        End If


        Dim auxSelect1 As String = " SELECT DOC_DOCLOG.cod as q_cod,DOC_DOC.dsc as q_dsc " _
                            & " ,NULL," & auxAnidarCon & "," & enumEntities.coEntityDOC_DOC & " as q_type " _
                            & " ,NULL,DOC_DOC.cod as cod,DOC_DOC.version,DOC_DOCLOG.fecha,DOC_DOCLOG.obs " _
                            & " ,(SELECT dsc FROM EMP WHERE cod=DOC_DOCLOG.empcod AND cod > 0) as colaborador " _
                            & " ,(SELECT wfwstpdsc FROM Q_WFWSTP WHERE wfwstpcod=DOC_DOCLOG.wfwstepnext AND wfwstpcod > 0) as estado " _
                            & " ,DOC_DOCLOG.empcod," & enumEntities.coEntityEMP & " as EMPTYPE " _
                            & " FROM DOC_DOCLOG " _
                            & " LEFT JOIN DOC_DOC ON DOC_DOCLOG.doccod = DOC_DOC.cod " _
                            & " WHERE DOC_DOCLOG.cod > 0 AND DOC_DOC.cod > 0 AND (DOC_DOC.baja = {#FALSE#} OR DOC_DOC.baja {#ISNULL#}) " _
                            & pWhere _
                            & " ORDER BY DOC_DOC.dsc,DOC_DOCLOG.fecha DESC "


        Dim auxDT1 As DataTable = auxClass.Conn.gConn_Query(auxSelect1)
        Dim auxi As Integer = 1
        For Each auxRow As DataRow In auxDT1.Rows
            auxRow("q_cod") = auxi
            auxi = auxi + 1
        Next
        auxi = 1 ' reset 

        Dim auxConsultasValues As New clshrcBagValues
        auxConsultasValues.gValue_Add("Consulta1", auxDT1)
        auxConsultasValues.gValue_Add("Consulta2", auxDT2)
        auxConsultasValues.gValue_Add("Consulta3", Nothing)
        auxConsultasValues.gValue_Add("Combo", pComboReporte)
        Return auxConsultasValues
    End Function

    'Reporte 28 - Acciones pendientes           
    Public Function gReports_AccionesPendientes(ByVal pComboReporte As Integer, ByVal pTipoResultado As Integer, ByVal pAgrupacion As Integer, ByVal pTipoDocumento As List(Of Integer), ByVal pUnidadDocumento As List(Of Integer), ByVal pProceso As List(Of Integer), ByVal pColaborador As List(Of Integer), ByVal pUnidadColaborador As List(Of Integer), ByVal pTituloDocumento As String, ByVal pFechaDesde As String, ByVal pFechaHasta As String, ByVal pEstado As List(Of Integer)) As clshrcBagValues
        Dim auxClass As New clsCusimDOC
        'FILTROS
        Dim pBagValues As New clshrcBagValues
        pBagValues = gReports_FiltrosWhere(pComboReporte, pTipoDocumento, pUnidadDocumento, pProceso, pColaborador, pUnidadColaborador, pTituloDocumento, pFechaDesde, pFechaHasta, pEstado)
        Dim pWhere As String = pBagValues.gValue_Get("Where")

        'AGRUPACIONES
        Dim pBagValues2 As New clshrcBagValues
        pBagValues2 = gReports_FiltrosAgrupacion(pComboReporte, pAgrupacion)
        Dim auxConsultaAnidar As String = pBagValues2.gValue_Get("auxConsultaAnidar")
        Dim auxAnidarCon As String = pBagValues2.gValue_Get("auxAnidarCon")

        Dim auxSelect2 As String = ""
        Dim auxDT2 As DataTable = Nothing
        If pAgrupacion > 0 Then
            auxSelect2 = auxConsultaAnidar
            auxDT2 = auxClass.Conn.gConn_Query(auxSelect2)
        End If


        Dim auxSelect1 As String = " SELECT DOC_DOC.cod as q_cod, DOC_DOC.dsc as q_dsc, NULL," & auxAnidarCon & "," & enumEntities.coEntityDOC_DOC & " as q_type " _
                            & " ,NULL,DOC_DOC.cod, DOC_DOC.version, DOC_DOC.fecha " _
                            & " ,(SELECT wfwstpdsc FROM Q_WFWSTP WHERE wfwstpcod=DOC_DOC.wfwstatus AND wfwstpcod > 0) as estado " _
                            & " ,(SELECT dsc FROM EMP WHERE cod=DOC_DOCSGN.empcod) as colaborador,DOC_DOCSGN.empcod, " & enumEntities.coEntityEMP & " as EMPTYPE " _
                            & " ,(SELECT wfwstpdsc FROM Q_WFWSTP WHERE wfwstpcod IN " _
                            & " (SELECT wfwstepnext FROM DOC_DOCLOG WHERE cod=DOC_DOCSGN.doclogcod)) as accion " _
                            & " , DOC_DOCSGN.fechainicio, DOC_DOCSGN.fechaultmail, DOC_DOCSGN.ultmailobs,DOC_DOC.wfwstatus, DOC_DOCLOG.wfwstepnext " _
                            & " FROM DOC_DOCSGN " _
                            & " LEFT JOIN DOC_DOC ON DOC_DOC.cod=DOC_DOCSGN.doccod " _
                            & " LEFT JOIN DOC_DOCLOG ON DOC_DOCLOG.cod = DOC_DOCSGN.doclogcod " _
                            & " WHERE DOC_DOCSGN.cod > 0 AND DOC_DOC.cod > 0 AND (DOC_DOC.baja = {#FALSE#} OR DOC_DOC.baja {#ISNULL#}) " _
                            & pWhere


        Dim auxDT1 As DataTable = auxClass.Conn.gConn_Query(auxSelect1)
        Dim auxi As Integer = 1
        For Each auxRow As DataRow In auxDT1.Rows
            If auxClass.Conn.gField_GetInt(auxRow("wfwstepnext"), -1) = enumWorkflowStep.coWFWSTPDOC_DOCDocumentovigente Then
                auxRow("accion") = "Lectura pendiente"
            End If

            auxRow("q_cod") = auxi
            auxi = auxi + 1
        Next
        auxi = 1 ' reset 


        Dim auxConsultasValues As New clshrcBagValues
        auxConsultasValues.gValue_Add("Consulta1", auxDT1)
        auxConsultasValues.gValue_Add("Consulta2", auxDT2)
        auxConsultasValues.gValue_Add("Consulta3", Nothing)
        auxConsultasValues.gValue_Add("Combo", pComboReporte)
        Return auxConsultasValues
    End Function
    'Reporte 34-documentos que requiere reid
    Public Function gReports_DOCReid() As clshrcBagValues
        Dim auxClass As New clsCusimDOC

        Dim auxDT1 As DataTable = auxClass.gDoc_ReIDCompare(False)
        auxDT1 = New DataView(auxDT1, "identificador <> new_identificador", "", DataViewRowState.CurrentRows).ToTable
        'Dim auxIdentificador As String
        '        For Each auxRow As DataRow In auxDT1.Rows
        'auxIdentificador = auxClass.gdoc
        'Next


        Dim auxConsultasValues As New clshrcBagValues
        auxConsultasValues.gValue_Add("Consulta1", auxDT1)
        auxConsultasValues.gValue_Add("Consulta2", Nothing)
        auxConsultasValues.gValue_Add("Consulta3", Nothing)
        'auxConsultasValues.gValue_Add("Combo", pComboReporte)
        Return auxConsultasValues
    End Function

    'Reporte 31 - Cantidad de leidas       
    Public Function gReports_CantidadLeidas(ByVal pComboReporte As Integer, ByVal pTipoResultado As Integer, ByVal pTipoDocumento As List(Of Integer), ByVal pUnidadDocumento As List(Of Integer), ByVal pProceso As List(Of Integer), ByVal pColaborador As List(Of Integer), ByVal pUnidadColaborador As List(Of Integer), ByVal pDocCod As Integer, ByVal pEstado As List(Of Integer)) As clshrcBagValues
        Dim auxClass As New clsCusimDOC
        Dim auxConnection As New imClientConnection
        Dim auxIDFiltros As String = Request.QueryString("_idfiltros_")
        Dim auxIDValores As clshrcBagValues = auxConnection.gObjectTmp_Download(auxIDFiltros)

        Dim pFechaDesde As String = ""
        Dim pFechaHasta As String = ""
        Dim auxAnidarCon As String = ""
        Dim pAgrupacion As Integer = Request.QueryString("_agrupa_")
        Dim pTituloDocumento As String = ""

        If auxIDValores IsNot Nothing Then
            pFechaDesde = auxClass.Conn.gField_GetString(auxIDValores.gValue_Get("_desde_"))
            pFechaHasta = auxClass.Conn.gField_GetString(auxIDValores.gValue_Get("_hasta_"))
            auxAnidarCon = auxClass.Conn.gField_GetString(auxIDValores.gValue_Get("_agrupacionCampo_"))
            pTituloDocumento = auxClass.Conn.gField_GetString(auxIDValores.gValue_Get("_tituloDOC_"))

        End If
        Dim auxFiltroAgrupa As String = ""
        If pAgrupacion > 0 Then
            If auxAnidarCon = "tabla.empcod" Then
                auxFiltroAgrupa = " AND DOC_DOCREADS.empcod IN (" & pAgrupacion & ") "
            Else
                auxFiltroAgrupa = " AND " & auxAnidarCon & " IN (" & pAgrupacion & ") "
            End If

        End If

        'FILTROS
        Dim pBagValues As New clshrcBagValues
        pBagValues = gReports_FiltrosWhere(pComboReporte, pTipoDocumento, pUnidadDocumento, pProceso, pColaborador, pUnidadColaborador, pTituloDocumento, pFechaDesde, pFechaHasta, pEstado)
        Dim pWhere As String = pBagValues.gValue_Get("Where")


        Dim auxSelect1 As String = "SELECT DOC_DOCREADS.empcod as cod," & enumEntities.coEntityEMP & " as q_type " _
                                & " ,(SELECT EMP.dsc FROM EMP WHERE EMP.cod=DOC_DOCREADS.empcod) as q_dsc, DOC_DOCREADS.version,DOC_DOCREADS.qsecdatetime " _
                                & " ,(SELECT UND.dsc FROM UND WHERE UND.cod IN (SELECT EMP.undcod FROM EMP WHERE EMP.cod=DOC_DOCREADS.empcod)) as unddsc," & enumEntities.coEntityUND & " as undtype " _
                                & " FROM DOC_DOCREADS " _
                                & " LEFT JOIN DOC_DOC ON DOC_DOCREADS.doccod = DOC_DOC.cod " _
                                & " WHERE (DOC_DOC.baja = {#FALSE#} OR DOC_DOC.baja {#ISNULL#}) AND DOC_DOC.cod > 0 AND DOC_DOCREADS.cod > 0 " _
                                & " AND DOC_DOCREADS.doccod=" & pDocCod _
                                & auxFiltroAgrupa _
                                & pWhere _
                                & " ORDER BY DOC_DOCREADS.version DESC, DOC_DOCREADS.qsecdatetime DESC "

        Dim auxDT1 As DataTable = auxClass.Conn.gConn_Query(auxSelect1)
        Dim auxDT2 As DataTable = auxClass.Conn.gConn_Query("")


        Dim auxConsultasValues As New clshrcBagValues
        auxConsultasValues.gValue_Add("Consulta1", auxDT1)
        auxConsultasValues.gValue_Add("Consulta2", auxDT2)
        auxConsultasValues.gValue_Add("Consulta3", Nothing)
        auxConsultasValues.gValue_Add("Combo", pComboReporte)
        Return auxConsultasValues
    End Function

    'Reporte 32 - Resumen de estapas      
    Public Function gReports_ResumenEtapas(ByVal pComboReporte As Integer, ByVal pTipoResultado As Integer, ByVal pColaborador As List(Of Integer), ByVal pUnidadColaborador As List(Of Integer), ByVal pFechaDesde As String, ByVal pFechaHasta As String) As clshrcBagValues
        Dim auxClass As New clsCusimDOC
        'FILTROS
        Dim pBagValues As New clshrcBagValues
        pBagValues = gReports_FiltrosWhere(pComboReporte, Nothing, Nothing, Nothing, pColaborador, pUnidadColaborador, "", pFechaDesde, pFechaHasta, Nothing)
        Dim pWhere As String = pBagValues.gValue_Get("Where")

        Dim auxSelect1 As String = "SELECT DOC_DOCTIP.cod as q_cod, DOC_DOCTIP.dsc as q_dsc, NULL,NULL," & enumEntities.coEntityDOC_DOCTIP & " as q_type " _
                                & " , DOC_DOCTIP.cod as cod, SUM(tabla.creacion) as creacion, SUM(tabla.edicion) as edicion, SUM(tabla.revision) as revision, SUM(tabla.publicacion) as publicacion, SUM(tabla.lectura) as lectura " _
                                & " FROM ( " _
                                    & " SELECT DOC_DOCLOG.doccod, DOC_DOCTIP.cod as doctip " _
                                    & " ,SUM(CASE WHEN DOC_DOCLOG.wfwstepnext=" & enumWorkflowStep.coWFWSTPDOC_DOCCreacion & " THEN 1 ELSE 0 END) as creacion " _
                                    & " ,SUM(CASE WHEN DOC_DOCLOG.wfwstepnext=" & enumWorkflowStep.coWFWSTPDOC_DOCEdicionOK & " THEN 1 ELSE 0 END) as edicion " _
                                    & " ,SUM(CASE WHEN DOC_DOCLOG.wfwstepnext=" & enumWorkflowStep.coWFWSTPDOC_DOCrevisionOK & " THEN 1 ELSE 0 END) as revision " _
                                    & " ,SUM(CASE WHEN DOC_DOCLOG.wfwstepnext=" & enumWorkflowStep.coWFWSTPDOC_DOCpublicacionOK & " THEN 1 ELSE 0 END) as publicacion " _
                                    & " ,SUM(CASE WHEN DOC_DOCLOG.wfwstepnext=" & enumWorkflowStep.coWFWSTPDOC_DOCLecturaOK & " THEN 1 ELSE 0 END) as lectura " _
                                    & " FROM DOC_DOCLOG " _
                                    & " LEFT JOIN DOC_DOC ON DOC_DOCLOG.doccod = DOC_DOC.cod " _
                                    & " LEFT JOIN DOC_DOCTIP ON DOC_DOCTIP.cod = DOC_DOC.doctipcod " _
                                    & " WHERE DOC_DOCLOG.cod > 0 " _
                                    & pWhere _
                                    & " GROUP BY DOC_DOCTIP.cod, DOC_DOCLOG.doccod " _
                                & " ) as tabla LEFT JOIN DOC_DOCTIP ON tabla.doctip = DOC_DOCTIP.cod " _
                                & " GROUP BY DOC_DOCTIP.cod , DOC_DOCTIP.dsc "

        Dim auxDT1 As DataTable = auxClass.Conn.gConn_Query(auxSelect1)

        Dim auxDT2 As DataTable = auxClass.Conn.gConn_Query("")


        'Paquete de valores - FECHAS ------------------
        Dim auxDateDesde As Date = auxClass.Conn.gField_GetDate(pFechaDesde)
        Dim auxDateHasta As Date = auxClass.Conn.gField_GetDate(pFechaHasta)

        Dim auxBagValues As New clshrcBagValues
        auxBagValues.gValue_Add("_desde_", auxClass.Conn.gFieldDB_GetDateTimeStandard(auxDateDesde))
        auxBagValues.gValue_Add("_hasta_", auxClass.Conn.gFieldDB_GetDateTimeStandard(auxDateHasta))
        Dim Col As String = ""
        If pColaborador.Count > 0 Then
            Dim primeraVez As Boolean = True
            For Each x As Integer In pColaborador
                If primeraVez = True Then
                    primeraVez = False
                    Col &= x
                Else
                    Col &= "," & x
                End If
            Next
        End If
        auxBagValues.gValue_Add("_colaborador_", Col)
        Dim Und As String = ""
        If pUnidadColaborador.Count > 0 Then
            Dim primeraVez As Boolean = True
            For Each x As Integer In pUnidadColaborador
                If primeraVez = True Then
                    primeraVez = False
                    Und &= x
                Else
                    Und &= "," & x
                End If
            Next
        End If
        auxBagValues.gValue_Add("_unidad_colaborador_", Und)


        Dim auxConnection As New imClientConnection
        Dim auxIDFechas As String = auxConnection.gObjectTmp_Upload(auxBagValues)
        '------------------------------------------


        Dim auxConsultasValues As New clshrcBagValues
        auxConsultasValues.gValue_Add("Consulta1", auxDT1)
        auxConsultasValues.gValue_Add("Consulta2", auxDT2)
        auxConsultasValues.gValue_Add("Consulta3", Nothing)
        auxConsultasValues.gValue_Add("_idfiltros_", auxIDFechas)
        auxConsultasValues.gValue_Add("Combo", pComboReporte)
        Return auxConsultasValues
    End Function

    'Reporte 33 - detalle de estapas      
    Public Function gReports_DetalleEtapas(ByVal pComboReporte As Integer, ByVal pTipoResultado As Integer, ByVal pTipDocCod As Integer) As clshrcBagValues
        Dim auxClass As New clsCusimDOC

        Dim auxConnection As New imClientConnection
        Dim auxIDFiltros As String = Request.QueryString("_idfiltros_")
        Dim auxIDValores As clshrcBagValues = auxConnection.gObjectTmp_Download(auxIDFiltros)

        Dim pFechaDesde As String = ""
        Dim pFechaHasta As String = ""
        Dim pColaborador As New List(Of Integer)
        Dim pUnidadColaborador As New List(Of Integer)
        Dim auxWfwStepDoc As Integer = auxClass.Conn.gField_GetInt(Request.QueryString("_wfwstepdoc_"))

        If auxIDValores IsNot Nothing Then
            pFechaDesde = auxClass.Conn.gField_GetString(auxIDValores.gValue_Get("_desde_"))
            pFechaHasta = auxClass.Conn.gField_GetString(auxIDValores.gValue_Get("_hasta_"))
            Dim pCol As String = auxClass.Conn.gField_GetString(auxIDValores.gValue_Get("_colaborador_"))
            If pCol <> "" Then
                For Each x As Integer In Split(pCol, ",")
                    pColaborador.Add(x)
                Next
            End If
            Dim pUndCol As String = auxClass.Conn.gField_GetString(auxIDValores.gValue_Get("_unidad_colaborador_"))
            If pUndCol <> "" Then
                For Each x As Integer In Split(pUndCol, ",")
                    pUnidadColaborador.Add(x)
                Next
            End If
        End If


        'FILTROS
        Dim pBagValues As New clshrcBagValues
        pBagValues = gReports_FiltrosWhere(pComboReporte, Nothing, Nothing, Nothing, pColaborador, pUnidadColaborador, "", pFechaDesde, pFechaHasta, Nothing)
        Dim pWhere As String = pBagValues.gValue_Get("Where")

        Dim auxSelect1 As String = "SELECT DOC_DOC.cod as q_cod, DOC_DOC.dsc as q_dsc,NULL,NULL," & enumEntities.coEntityDOC_DOC & " as q_type " _
                                & " ,DOC_DOC.cod, DOC_DOC.fecha, DOC_DOC.version  " _
                                & " FROM DOC_DOCLOG " _
                                & " LEFT JOIN DOC_DOC ON DOC_DOC.cod=DOC_DOCLOG.doccod " _
                                & " WHERE DOC_DOCLOG.cod > 0 " _
                                & " AND DOC_DOCLOG.wfwstepnext=" & auxWfwStepDoc _
                                & " AND DOC_DOC.doctipcod=" & pTipDocCod _
                                & pWhere


        Dim auxDT1 As DataTable = auxClass.Conn.gConn_Query(auxSelect1)
        Dim auxDT2 As DataTable = auxClass.Conn.gConn_Query("")



        Dim auxConsultasValues As New clshrcBagValues
        auxConsultasValues.gValue_Add("Consulta1", auxDT1)
        auxConsultasValues.gValue_Add("Consulta2", auxDT2)
        auxConsultasValues.gValue_Add("Consulta3", Nothing)
        auxConsultasValues.gValue_Add("Combo", pComboReporte)
        Return auxConsultasValues
    End Function
End Class

