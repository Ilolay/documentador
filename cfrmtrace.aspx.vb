Imports Intelimedia.imComponentes
Imports System.Data
Imports clsCusimDOC
Partial Class cfrmtrace
    Inherits System.Web.UI.Page
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        'If Not IsPostBack Then
        Dim auxClass As New clsCusimDOC
        Dim auxProCod As Integer = auxclass.Conn.gField_GetInt(Request.QueryString("param1"))
        Dim auxAccess As Boolean = False
        Dim auxIsAdmin As Boolean = False
        If Session("isadmin") Then
            auxIsAdmin = True
        End If
        Dim auxSittypeCod As Integer = enumEntities.coEntityDOC_DOC
        If auxProCod < 1 Then
            If Session("Isadmin") Then
                auxAccess = True
                lblsubtitle.Text = "Global"
                grdData.DataSource = auxClass.Conn.gConn_Query("SELECT PRO_TRACELOG.*,Q_SECPLOGIN.secdsc " _
                                                         & " FROM PRO_TRACELOG " _
                                                         & " LEFT JOIN Q_SECPLOGIN ON PRO_TRACELOG.qsecsid=Q_SECPLOGIN.sidcod" _
                                                         & " WHERE procod=-1" _
                                                         & " ORDER BY trcfecha DESC")
            End If
        Else
            Dim auxSidCod As Integer = auxClass.Conn.gConn_QueryValue("SELECT qsidcod FROM DOC_DOC WHERE cod =" & auxProCod)
            Dim auxAccessType As String = enumAccessType.coSYSGlobalCambiarpermisos _
                                & "," & enumAccessType.coSYSGlobalModificar
            Select Case auxSittypeCod
                Case enumEntities.coEntityDOC_DOC
                    auxAccess = auxIsAdmin Or auxClass.Sec.gSID_CheckAccess(auxSidCod, enumAccessType.coSYSGlobalCambiarpermisos)
                    If auxAccess Then
                        Dim auxDT As DataTable = auxClass.Conn.gConn_Query("SELECT dsc FROM DOC_DOC WHERE cod =" & auxProCod)
                        If auxDT.Rows.Count <> 0 Then
                            lblsubtitle.Text = auxClass.Conn.gField_GetString(auxDT.Rows(0)("dsc"))
                        End If
                        grdData.DataSource = auxClass.Conn.gConn_Query("SELECT PRO_TRACELOG.*,ISNULL(Q_SECPLOGIN.secdsc,'Sistema') as secdsc " _
                                                          & " FROM PRO_TRACELOG " _
                                                          & " LEFT JOIN Q_SECPLOGIN ON PRO_TRACELOG.qsecsid=Q_SECPLOGIN.sidcod" _
                                                          & " WHERE procod=" & auxProCod _
                                                          & " ORDER BY trcfecha DESC")
                    End If
            End Select
        End If
        auxClass.Conn.gConn_Close()
        If auxAccess Then
            grdData.DataBind()
        Else
           'cmdFormViewItemCancel_Click(Nothing, Nothing)
        End If
        'End If
    End Sub

    Protected Sub cmdFormViewItemCancel_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdFormViewItemCancel.Click
        If Request.QueryString("_closea_") = "1" Then
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "CloseWindow", "self.close();", True)
        End If
    End Sub

End Class



