Imports clsCusimDOC
Imports System.Data
Imports System.Data.SqlClient
Partial Class cfrmdocumentos_cambios
    Inherits System.Web.UI.Page
    Private m_DocCod As Integer = -1
    Private m_IsAdminBasic As Boolean = False
    Private m_IsAdmin As Boolean = False
    Private m_PermEdit As Boolean = False
    Private m_PermNew As Boolean = False
    Friend m_mode As enumActionType = enumActionType.coViewDetail
    Friend m_WfwStateCod As enumWorkflowStep = -1
    Friend m_HstGenCod As Integer = -1
    Friend m_ViewCurrent As Boolean = False
    Friend m_PermNavegar As Boolean = False

    Private Sub gForm_GetVars()

    End Sub
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim auxView As Short = Val(Request.QueryString("_view_"))
        Dim auxCod As Integer = Val(Request.QueryString("param1"))
        If Not IsPostBack Then


        End If
        Dim auxWhere As String = ""
        Dim auxCompareToVigente As Boolean
        Select Case auxview
            Case 1
                'Vigentes
                auxWhere = " AND Q_HSTHIDGEN.hsthidgendsc LIKE '%Vigente%'"
                auxCompareToVigente = True
            Case Else
                'todos=vigentes y edicion
        End Select
        Dim auxClass As New clsCusimDOC
        auxClass.Conn.gConn_Open()
        Dim auxConn As clsHrcConnClient = auxClass.Conn
        Dim auxDT As DataTable = auxConn.gConn_Query("SELECT DOC_DOC_HST.hstcod,Q_HSTHIDGEN.hsthidgendsc as hstdsc " _
            & " FROM DOC_DOC_HST " _
            & " LEFT JOIN Q_HSTHIDGEN ON Q_HSTHIDGEN.hsthidgencod= DOC_DOC_HST.hsthidgencod" _
            & " WHERE DOC_DOC_HST.cod=" & auxCod _
            & auxWhere _
            & " ORDER BY DOC_DOC_HST.hstcod DESC")
        auxClass.Conn.gConn_Close()
        ' If auxDT.Rows.Count <> 0 Then
        '     auxDT.Rows.RemoveAt(0)
        ' End If
        If auxDT.Rows.Count = 0 Then
            cmbVersion.Visible = False
            cmdViewChanges.Visible = False
        Else
            cmbVersion.Visible = True
            cmdViewChanges.Visible = True
            Dim auxValue As String = ""
            If cmbVersion.SelectedItem IsNot Nothing Then
                auxValue = cmbVersion.SelectedValue
            End If
            cmbVersion.DataSource = auxDT
            cmbVersion.DataBind()
            If auxValue <> "" Then
                If cmbVersion.Items.FindByValue(auxValue) IsNot Nothing Then
                    cmbVersion.ClearSelection()
                    cmbVersion.Items.FindByValue(auxValue).Selected = True
                End If
            End If
            Select Case auxView
                Case 1
                    If auxDT.Rows.Count <> 0 Then
                        lblCambios.Text = auxClass.gVersion_Diff(auxDT.Rows(0)("hstcod"), -1, True)
                    End If
                Case Else
                    If auxDT.Rows.Count <> 0 Then
                        lblCambios.Text = auxClass.gVersion_Diff(auxDT.Rows(0)("hstcod"))
                    End If
            End Select
        End If

    End Sub
    Protected Sub cmdViewChanges_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        If cmbVersion.SelectedValue <> "" Then
            Dim auxClass As New clsCusimDOC
            auxClass.Conn.gConn_Open()
            Dim auxImpresion As String
            auxImpresion = auxClass.gVersion_Diff(cmbVersion.SelectedValue)
            auxImpresion = Replace(auxImpresion, "#SITEURL_", VirtualPathUtility.GetDirectory(Request.Path))
            auxImpresion = Replace(auxImpresion, "http://siteurl/", VirtualPathUtility.GetDirectory(Request.Path) & "/")
            lblCambios.Text = auxImpresion
            auxClass.Conn.gConn_Close()
        End If
    End Sub
End Class

