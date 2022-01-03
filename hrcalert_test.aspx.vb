Imports Intelimedia.imComponentes
Partial Class hrcalert_test
    Inherits System.Web.UI.Page
    Protected Sub cmdMail_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdMail.Click
        Dim auxConn As clsHrcConnClient = Session("conn")
        Dim auxMail As New imMailing(coSMTPServer, coSMTPUser, coSMTPpsw, coSMTPfrom, Val(coSMTPport), coSystemTitle)
        auxMail.SMTPenableSSL = auxConn.gField_GetBoolean(coSMTPSSLEnabled, False)
        lblResult.Text = ""
        lblResult.Text &= "SMTP-Servidor:" & coSMTPServer & "<br />"
        lblResult.Text &= "SMTP-De:" & coSMTPfrom & "<br />"
        lblResult.Text &= "SMTP-SSL habilitado:" & auxMail.SMTPenableSSL & "<br />"
        Dim auxResult As Boolean = auxMail.gMail_Send(txtMail.Text, "", "", "Mail test", "Mail test del sistema:" & coSystemTitle & " a " & txtMail.Text & ".(" & Now.ToString & " hora local del servidor)")
        lblResult.Text &= "Resultado:"
        If auxResult Then
            lblResult.Text &= "Mail enviado" & "<br />"
        Else
            lblResult.Text &= "ERROR AL ENVIAR MAIL!" & "<br />"
            lblResult.Text &= "Error:" & auxMail.LastErrorDescription & "<br />"
        End If

    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session("Isadmin") = False Then
            Server.Transfer("gerror.aspx?msg=Access error")
        End If
    End Sub
End Class
