
Partial Class general_master
    Inherits imWebMasterPage

    Protected Overloads Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        If ConfigurationManager.AppSettings("versionmode") = "2" Then
            Body1.Style.Add("style", "bgcolor:#e0e0e0")
        End If
        If Not Page.IsPostBack Then
            If InStr(Request.RawUrl, "/public_login.aspx") = 0 And Session("secsid") = -1 And LicParam IsNot Nothing Then
                Response.Redirect("~/public_login.aspx")
            End If
            lblSecDsc.Text = Session("secdsc")
            If lblSecDsc.Text = "" Then
                lblSecDsc.Text = "-----"
            End If
            If Request.QueryString("_closea_") = "1" Or Session("inframe") = True Then
                pnlHeader.Visible = False
            Else
                pnlHeader.Visible = True
            End If
        End If
        If Session("hrcAlerts") IsNot Nothing Then
            Dim auxAlerts As clsHrcAlertClient = Session("hrcAlerts")
            If auxAlerts.OnlineModeEnabled Then
                Dim auxScript As String = "gAlerts_StartScreenMonitor(" & auxAlerts.LastStatusMsgLevel & "," _
                                & auxAlerts.LastStatusMsgCant & ");"
                Page.ClientScript.RegisterStartupScript(Me.GetType, "hrcAlerts_Start", auxScript, True)
            End If
        End If

        If Session("user_menu_script") IsNot Nothing Then
            Dim auxScript As String = Session("user_menu_script")
            Page.ClientScript.RegisterStartupScript(Me.GetType, "hrcuser_menu_script", auxScript, False)
        End If

    End Sub
    'Public Sub GetMenu()
    '    If Request.QueryString("_closea_") = "1" Or Session("inframe") = True Then
    '        pnlHeader.Visible = False
    '    Else
    '        pnlHeader.Visible = True
    '        Response.Write(Session("user_menu"))
    '    End If


    'End Sub
End Class

