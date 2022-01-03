Imports System.Data
Partial Class hrcFastActions
    Inherits System.Web.UI.Page
    Private Sub gData_Get()
        Dim auxClass As New clsCusimDOC
        Dim auxView As Integer = 1

        Select Case auxView
            Case 1
                '<img border=0 src=imagenes/objFastAccess_delegations_yes.png width=24px ></img>
                lbltitle.Text = "Delegaciones<br />"
                If auxClass.Sec.IsDelegatedSession Then
                    fmeDeactivate.Visible = True
                    fmeActivate.Visible = False
                    lbldeactivate.Text = "Se encuentra actuando en nombre de " _
                        & "[" & auxClass.Conn.gConn_QueryValueString("SELECT dsc FROM EMP WHERE seccod=" & auxClass.Sec.CurrentSecCod) & "]" _
                        & ".<br />Clickee el botón inferior para finalizar el uso de esta identidad:<br />"
                Else
                    fmeDeactivate.Visible = False
                    fmeActivate.Visible = True
                    Dim auxDT As DataTable = auxClass.Sec.gLogin_ResolveDelegatedSessionToCurrentLogin
                    If auxDT.Rows.Count = 0 Then
                        lbldeactivate.Text = "No hay delegaciones disponibles"
                    Else
                        grdDelegationsToMy.DataSource = auxDT
                        grdDelegationsToMy.DataBind()
                        'grdDelegationsToMy.
                        lbldeactivate.Text = "Las delegaciones permiten actuar en nombre de otra identidad." _
                        & "Clickee la identidad a utilizar. <br />"
                    End If
                    
                End If
        End Select


    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            gData_Get()
        End If
    End Sub

    Protected Sub grdDelegations_RowCommand(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles grdDelegationsToMy.RowCommand
        Select Case e.CommandName
            Case "CMDACTIVATE"
                Dim auxClass As New clsCusimDOC
                If auxClass.gSystem_LogonDelegatedSession(e.CommandArgument) Then
                    'gData_Get()

                    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "dd", "parent.window.location='cfrmdocumentos.aspx';", True)
                End If
        End Select
    End Sub
    Protected Sub cmdDeactivate_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim auxClass As New clsCusimDOC
        auxClass.gSystem_LogoffDelegatedSession()
        'gData_Get()
        ScriptManager.RegisterStartupScript(Me, Me.GetType(), "dd", "parent.window.location='cfrmdocumentos.aspx';", True)
    End Sub
End Class
