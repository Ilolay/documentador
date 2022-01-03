Imports Intelimedia.imComponentes
Imports System.Data
Imports clsCusimDOC
Partial Class cfrmdocumentoslog_det
    Inherits System.Web.UI.Page
    Private m_IsAdmin As Boolean = False
    Friend m_mode As Short = 0
    Friend m_DocCod As Integer
    Friend m_WfwStateCod As Integer = -1
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        m_mode = Val(Request.QueryString("_mode_"))
        m_DocCod = Val(Request.QueryString("param1"))
        'If Not IsPostBack Then
        Select Case Request.QueryString("_mode_")
            Case "0"    'Ver
                hdnProCod.Value = Request.QueryString("param1")
                Call gData_Get()
        End Select
        'End If
    End Sub
    Private Sub gData_Get()
        Dim auxClass As New clsCusimDOC
        auxClass.Conn.gConn_Open()
        Dim auxProCod As Integer = -1
        Dim auxDT As New DataTable
        Dim auxSidCod As Integer = -1
        Dim auxView As Integer = Val(Request.QueryString("_view_"))
        m_WfwStateCod = enumWorkflowStep.coWFWSTPDOC_DOCnuevaversion
        auxProCod = Val(Request.QueryString("param1"))
        auxDT = auxClass.Conn.gConn_Query("SELECT dsc,qsidcod FROM DOC_DOC WHERE cod=" & auxProCod)
        If auxDT.Rows.Count = 0 Then
            cmdFormViewItemCancel_Click(Nothing, Nothing)
            Exit Sub
        End If
        lblsubtitle.Text = auxDT.Rows(0)("dsc")
        auxSidCod = auxClass.Conn.gField_GetInt(auxDT.Rows(0)("qsidcod"))
        If Session("isadmin") Or auxClass.Sec.gSID_CheckAccess(auxSidCod, enumAccessType.coSYSGlobalCambiarpermisos _
                                                                & "," & enumAccessType.coSYSGlobalModificar) Then
            m_IsAdmin = True
        End If
        If m_IsAdmin = False And auxClass.Sec.gSID_CheckAccess(auxSidCod, enumAccessType.coSYSGlobalModificar) = False Then
            cmdFormViewItemCancel_Click(Nothing, Nothing)
            Exit Sub
        End If

        Dim auxQuery As String = ""
        Select Case auxView
            Case 0 'Historia
                auxQuery = "SELECT DOC_DOCLOG.*" _
                    & ",Q_WFWSTP.wfwstpdsc as wfwstpprevdsc" _
                    & ",CASE WHEN EMP.cod = -1 THEN 'Sistema' ELSE EMP.dsc END as empdsc " _
                    & ",CASE WHEN DEL.cod = -1 THEN '' ELSE DEL.dsc END as del_empdsc" _
                    & " FROM DOC_DOCLOG" _
                    & " LEFT JOIN EMP ON DOC_DOCLOG.empcod = EMP.cod" _
                    & " LEFT JOIN EMP as DEL ON DOC_DOCLOG.delempcod = DEL.cod " _
                    & " LEFT JOIN Q_WFWSTP ON DOC_DOCLOG.wfwstepnext = Q_WFWSTP.wfwstpcod AND DOC_DOCLOG.wfwstepnext > 0" _
                    & " WHERE doccod = " & auxProCod _
                    & " ORDER BY DOC_DOCLOG.cod DESC"

            Case 1 'Versiones
                auxQuery = "SELECT DOC_DOCLOG.*" _
                    & ",CASE WHEN wfwstepnext > 0 THEN Q_WFWSTP.wfwstpdsc ELSE '' END as wfwstpprevdsc" _
                    & ",CASE WHEN EMP.cod = -1 THEN 'Sistema' ELSE EMP.dsc END as empdsc " _
                    & ",CASE WHEN DEL.cod = -1 THEN '' ELSE DEL.dsc END as del_empdsc" _
                    & " FROM DOC_DOCLOG" _
                    & " LEFT JOIN EMP ON DOC_DOCLOG.empcod = EMP.cod" _
                    & " LEFT JOIN EMP as DEL ON DOC_DOCLOG.delempcod = DEL.cod " _
                    & " LEFT JOIN Q_WFWSTP ON DOC_DOCLOG.wfwstepnext = Q_WFWSTP.wfwstpcod" _
                    & " LEFT JOIN DOC_DOC_HST ON DOC_DOCLOG.hsthidgencod=DOC_DOC_HST.hsthidgencod" _
                    & " WHERE doccod = " & auxProCod _
                    & " AND DOC_DOCLOG.wfwstepnext=" & enumWorkflowStep.coWFWSTPDOC_DOCDocumentovigente _
                    & " ORDER BY DOC_DOCLOG.cod DESC"
                lblsubtitle.Text &= "-Versiones"

            Case 2 'Copias
                auxQuery = "SELECT DOC_DOCLOG.*" _
                  & ",CASE WHEN wfwstepnext > 0 THEN Q_WFWSTP.wfwstpdsc ELSE '' END as wfwstpprevdsc" _
                  & ",CASE WHEN EMP.cod  = -1 THEN 'Sistema' ELSE EMP.dsc END as empdsc " _
                  & ",CASE WHEN DEL.cod = -1 THEN '' ELSE DEL.dsc END as del_empdsc" _
                  & " FROM DOC_DOCLOG" _
                  & " LEFT JOIN EMP ON DOC_DOCLOG.empcod = EMP.cod" _
                  & " LEFT JOIN EMP as DEL ON DOC_DOCLOG.delempcod = DEL.cod " _
                  & " LEFT JOIN Q_WFWSTP ON DOC_DOCLOG.wfwstepnext = Q_WFWSTP.wfwstpcod" _
                  & " LEFT JOIN DOC_DOC_HST ON DOC_DOCLOG.hsthidgencod=DOC_DOC_HST.hsthidgencod" _
                  & " WHERE doccod = " & auxProCod _
                  & " AND DOC_DOCLOG.dsc IN ('" & coCopiaControladaTexto & "','" & coCopiaNoControladaTexto & "')" _
                  & " ORDER BY DOC_DOCLOG.cod DESC"
                lblsubtitle.Text &= "-Copias"
        End Select
        grdPROLOG.DataSource = auxClass.Conn.gConn_Query(auxQuery)
        auxClass.Conn.gConn_Close()
        'PROLOG
        grdPROLOG.DataBind()
    End Sub

    Protected Sub cmdFormViewItemCancel_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdFormViewItemCancel.Click
        If Request.QueryString("_closea_") = "1" Then
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "CloseWindow", "self.close();", True)
        Else
            Response.Redirect("cfrmdocumentos1_det.aspx?_mode_=0&_closea_=0&param1=" & Request.QueryString("param1"))
        End If
    End Sub

End Class



