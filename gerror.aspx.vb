
Partial Class gerror
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Request.QueryString("msg") IsNot Nothing Then
            lblerror.Text = Request.QueryString("msg")
        End If
    End Sub
End Class
