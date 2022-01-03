Imports Intelimedia.imComponentes
Imports System.Data
Imports clsCusimDOC
Partial Class cfrmdocumentosref_det
    Inherits System.Web.UI.Page
    Private m_IsAdmin As Boolean = False
    Friend m_mode As Short = 0
    Friend m_DocCod As Integer
    Friend m_View As Integer = 1
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            m_mode = Val(Request.QueryString("_mode_"))
            m_IsAdmin = Session("isadmin")
            If Request.QueryString("_view_") IsNot Nothing Then
                m_View = Val(Request.QueryString("_view_").ToString)
            End If
            m_DocCod = Val(Request.QueryString("param1"))
            'If Not IsPostBack Then
            Select Case Request.QueryString("_mode_")
                Case "0"    'Ver
                    hdnProCod.Value = Request.QueryString("param1")
                    Call gData_Get()
            End Select
        End If
    End Sub
    Private Sub gData_Get()
        Dim auxClass As New clsCusimDOC
        auxClass.Conn.gConn_Open()
        Dim auxDT As New DataTable
        Dim auxSidCod As Integer = -1
        Dim auxSelectCommand As String = ""
        auxDT = auxClass.Conn.gConn_Query("SELECT dsc,qsidcod FROM DOC_DOC WHERE cod=" & m_DocCod)
        If auxDT.Rows.Count = 0 Then
            cmdFormViewItemCancel_Click(Nothing, Nothing)
            Exit Sub
        End If
        auxSidCod = auxClass.Conn.gField_GetInt(auxDT.Rows(0)("qsidcod"))

        If m_IsAdmin = False And auxClass.Sec.gSID_CheckAccess(auxSidCod, enumAccessType.coSYSGlobalCambiarpermisos) Then
            m_IsAdmin = True
        End If
        If m_IsAdmin = False And auxClass.Sec.gSID_CheckAccess(auxSidCod, enumAccessType.coSYSGlobalCambiarestado) = False Then
            cmdFormViewItemCancel_Click(Nothing, Nothing)
            Exit Sub
        End If
        lblTitle.Text = auxDT.Rows(0)("dsc")
        'Referencias del doc a otros
        lblsubtitle.Text = "Referencias"
        auxSelectCommand = "SELECT DOC_DOC.cod,DOC_DOCREF.refid,(DOC_DOC.identificador + '-' + DOC_DOC.dsc) as docdsc" _
                    & " FROM DOC_DOCREF " _
                    & " LEFT JOIN DOC_DOC ON DOC_DOCREF.doccodref =DOC_DOC.cod " _
                    & " WHERE DOC_DOCREF.doccod = " & m_DocCod
        dsPROSGN.SelectCommand = auxSelectCommand
        grdDOCSGN.DataBind()
        'Referencias de otros documentos al documento
        auxSelectCommand = "SELECT DOC_DOC.cod,DOC_DOCREF.refid,(DOC_DOC.identificador + '-' + DOC_DOC.dsc) as docdsc" _
                   & " FROM DOC_DOCREF " _
                   & " LEFT JOIN DOC_DOC ON DOC_DOCREF.doccod =DOC_DOC.cod " _
                   & " WHERE DOC_DOCREF.doccodref = " & m_DocCod
        dsReferido.SelectCommand = auxSelectCommand
        grdReferido.DataBind()
        'PROLOG
        auxClass.Conn.gConn_Close()
    End Sub

    Protected Sub cmdFormViewItemCancel_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdFormViewItemCancel.Click
        If Request.QueryString("_closea_") = "1" Then
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "CloseWindow", "self.close();", True)
        Else
            Response.Redirect("cfrmdocumentos1_det.aspx?_mode_=0&_closea_=0&param1=" & Request.QueryString("param1"))
        End If
    End Sub

    Protected Sub dsPROSGN_Init(ByVal sender As Object, ByVal e As System.EventArgs) Handles dsPROSGN.Init
        dsPROSGN.ConnectionString = Session("connectionstringname")
    End Sub

    Protected Sub dsReferido_Init(ByVal sender As Object, ByVal e As System.EventArgs) Handles dsReferido.Init
        dsReferido.ConnectionString = Session("connectionstringname")
    End Sub
End Class



