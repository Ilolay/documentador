Imports Intelimedia.imComponentes
Partial Class public_login
    Inherits System.Web.UI.Page

    Protected Sub Button1_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdLoginButton.Click, cmdLoginADButton.Click
        Dim auxClass As New clshrcGeneral
        Dim auxClientCon As New imClientConnection

        Dim auxObject As Object = auxClientCon.gObjectTmp_Download(ViewState("checkid"))
        If auxObject Is Nothing Then
            'No existe el object en memoria. No se puede ingresar
            'lblmsg.Text = "Error durante la validación. Vuelva a ingresar"
            Response.Redirect("public_login.aspx")
            Exit Sub
        End If

        Dim auxCaptcha As clshrcJSCaptcha
        auxCaptcha = auxClientCon.gObjectTmp_Download(ViewState("checkid") & "_captcha")
        If auxCaptcha IsNot Nothing Then
            If auxCaptcha.gISOK(txtcheck.Text) = False Then
                lblmsg.Text = "Error en validación humana(2)"
                txtcheck.Text = ""
                auxCaptcha.gFactor_Init()
                Exit Sub
            End If
            'lblmsg.Text = "Error en validación humana(1)"
            'Exit Sub
        End If

        lblmsg.Text = ""
        If auxClass.Conn IsNot Nothing Then
            auxClass.Conn.gConn_Open()
            Dim auxHasAccess As Boolean = False
            Dim auxCommandName As String
            auxCommandName = CType(sender, Button).CommandName.ToUpper
            Select Case auxCommandName
                Case "LOGINAD"
                    auxHasAccess = auxClass.gSystem_CheckAccess()
                Case Else
                    If txtuser.Text = "" Then
                        lblmsg.Text = "Ingrese usuario y contraseña"
                        If auxCaptcha IsNot Nothing Then
                            auxCaptcha.gFactor_Init()
                        End If
                        Exit Sub
                    Else
                        auxHasAccess = auxClass.gSystem_CheckAccess(txtuser.Text, txtpwd.Text)
                    End If
            End Select

            Dim auxFirstPage As String = ""
            If auxHasAccess Then
                auxFirstPage = hrcFormInitial & ".aspx"
            End If
            auxClass.Conn.gConn_Close()
            If auxHasAccess Then
                Session("inframe") = False
                Response.Redirect("~/" & auxFirstPage)
            Else
                lblmsg.Text = "Usuario y/o contraseña inválido"
            End If
        End If
    End Sub
    Protected Sub cmdLogout_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdLogout.Click
        Dim auxClass As New clshrcGeneral
        If auxClass.Sec IsNot Nothing Then
            auxClass.Sec.gLogoff()
        End If

        Session.Clear()
        Session.Abandon()
        Response.Redirect("public_login.aspx?")
        'Response.Redirect(hrcFormInitial & ".aspx")
    End Sub
    'Protected Sub Button3_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdLogDisabled.Click
    '    Session("debugLogOn") = False
    '    lblmsg.Text = "Log deshabilitado"
    'End Sub
    'Protected Sub Button4_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdLogEnabled.Click
    '    Session("debugLogOn") = True
    '    lblmsg.Text = "Log Habilitado"
    'End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        'view
        '0=login (tiene en cuenta el estado actual)
        '1=cambio contraseña expirada
        '2=cambio de contraseña
        '3=logout
        '4=logon nivel 1
        '5=login nuevo
        '6=login de sesion 
        'If Session("isadmin") Then
        '    cmdLogDisabled.Visible = True
        '    cmdLogEnabled.Visible = True
        'Else
        '    cmdLogDisabled.Visible = False
        '    cmdLogEnabled.Visible = False
        'End If

        If Not IsPostBack Then
            Dim auxView As Integer = 0
            If Request.QueryString("_view_") IsNot Nothing Then
                auxView = Val(Request.QueryString("_view_"))
            End If
            If auxView = 6 Then
                'Es un login con sesion
                Dim auxSesID As String = Request.QueryString("param1")
                If auxSesID <> "" Then
                    Dim auxClass As New clshrcGeneral
                    auxClass.gSystem_Init()
                    auxClass.gSystem_CheckAccess(auxSesID)
                    Response.Redirect(hrcFormInitial & ".aspx")
                End If
            End If

            row_oldpsw.Visible = False
            row_user.Visible = False
            row_pswchange1.Visible = False
            row_pswchange2.Visible = False
            row_check.Visible = False
            Dim auxSeCod As Integer = -1
            Try
                auxSeCod = CType(Session("security"), clsHrcSecurityClient).CurrentSecCod
            Catch ex As Exception

            End Try
            Dim auxActivate As Boolean = False
            Dim auxIsAdmin As Boolean = False
            Try
                auxIsAdmin = Session("isadmin")
            Catch ex As Exception

            End Try
            Try
                auxActivate = Session("conn").activated
            Catch ex As Exception

            End Try

            If auxView <> 5 And (auxIsAdmin Or auxSeCod > 0 Or auxActivate = False) Then
                Select Case auxView
                    Case 0
                        cmdLogout.Visible = True
                        cmdLoginButton.Visible = False
                        cmdLoginADButton.Visible = False
                        cmdPswChange.Visible = False
                    Case 1  'Cambio de contraseña expirada
                        cmdLogout.Visible = False
                        cmdLoginButton.Visible = False
                        cmdLoginADButton.Visible = False
                        row_oldpsw.Visible = True
                        row_pswchange1.Visible = True
                        row_pswchange2.Visible = True
                        cmdPswChange.Visible = True
                        lblTitle.Text = "¡Su contraseña ha expirado!"
                        lblSubtitle.Text = "Ingrese una nueva contraseña."
                        ' Master.FindControl("lblmenu").Visible = False
                    Case 2  'Cambio de contraseña
                        lblTitle.Text = "Cambio de contraseña"
                        lblSubtitle.Text = "Ingrese una nueva contraseña."
                        cmdLogout.Visible = False
                        cmdLoginButton.Visible = False
                        cmdLoginADButton.Visible = False
                        If CType(Session("security"), clsHrcSecurityClient).SecNoChangePsw Then
                            row_oldpsw.Visible = False
                            row_pswchange1.Visible = False
                            row_pswchange2.Visible = False
                            cmdPswChange.Visible = False
                            Exit Sub
                        Else
                            row_oldpsw.Visible = True
                            row_pswchange1.Visible = True
                            row_pswchange2.Visible = True
                            cmdPswChange.Visible = True

                        End If
                    Case 3 'Logout
                        lblTitle.Text = "Salir del sistema"
                        lblSubtitle.Text = "Click en el siguiente botón para finalizar su sesión"
                        cmdLogout.Visible = False
                        row_pswchange1.Visible = False
                        row_pswchange2.Visible = False
                        cmdPswChange.Visible = False
                        cmdLoginButton.Visible = False
                        cmdLoginADButton.Visible = False
                        cmdLogout.Visible = True
                        cell_bg.Attributes.Remove("style")
                    Case 4 'level1
                        cmdLogout.Visible = False
                        cmdLoginButton.Visible = False
                        cmdLoginADButton.Visible = False
                        row_oldpsw.Visible = False
                        row_pswchange1.Visible = False
                        row_pswchange2.Visible = False

                        lblTitle.Text = "Transferencias"
                        lblSubtitle.Text = "Ingrese su contraseña para transferencias."

                        Dim auxClass As New clshrcGeneral
                        If Session("isadmin") = False And auxClass.Conn.gConn_Query("SELECT cod FROM EMP WHERE cod=" & Session("empcod") & " AND trnpsw=''").Rows.Count = 1 Then
                            'No tiene contraseña seteada
                            row_pswchange1.Visible = True
                            row_pswchange2.Visible = True
                            lblTitle.Text = "Transferencias"
                            lblSubtitle.Text = "Configure su contraseña para transferencias."
                            cmdPswChange.Visible = True
                        Else
                            row_oldpsw.Visible = True
                            cmdPswChange.Visible = False
                            cmdLoginButton.Visible = True
                            If auxClass.gNet_IsInInternal() Then
                                cmdLoginADButton.Visible = True
                            End If
                        End If
                    Case Else
                        cmdLogout.Visible = False
                        cmdLoginButton.Visible = False
                        cmdLoginADButton.Visible = False
                        row_oldpsw.Visible = False
                        row_pswchange1.Visible = False
                        row_pswchange2.Visible = False
                        cmdPswChange.Visible = False
                End Select

            ElseIf auxView = 7 Then
            Else

                lblTitle.Text = "Inicio de sesión"
                lblSubtitle.Text = "Ingrese sus datos"
                cmdLogout.Visible = False
                cmdLoginButton.Visible = True
                cmdLoginADButton.Visible = False
                Dim auxClass As New clshrcGeneral
                If auxClass.gNet_IsInInternal() Then
                    Dim auxIsIntegratedDomain As Boolean = False
                    If coSecDomainList.Trim <> "" Then
                        Dim auxUserName As String = HttpContext.Current.User.Identity.Name.ToString
                        Dim auxUsr() As String = auxUserName.Split("\")
                        If auxUsr.Count = 2 Then
                            If InStr(";" & coSecDomainList.ToUpper & ";", ";" & auxUsr(0).ToUpper & ";") <> 0 Then
                                auxIsIntegratedDomain = True
                            End If
                        End If
                        If auxIsIntegratedDomain Then
                            cmdLoginADButton.Visible = True
                            cmdLoginADButton.Text = "Ingresar con [" & auxUserName & "]"
                        End If
                    End If
                End If
                cmdPswChange.Visible = False
                row_user.Visible = True
                row_oldpsw.Visible = True

                If ViewState("checkid") = "" Then
                    ViewState("checkid") = auxClass.Conn.gField_GetUniqueID
                End If

                Dim auxScript As String
                Dim auxClientCon As New imClientConnection
                auxClientCon.gObjectTmp_Upload(Now, ViewState("checkid"))
                ' If auxClass.Conn.gField_GetBoolean(auxClass.gSystem_GetParameterByID(auxClass.coSysParamSecCaptchaEnabled), False) Then
                If auxClass.gNet_IsInInternal() = False Then
                    'Si es externo no muestra el header
                    'Session("inframe") = True
                    Title = "Acceso"
                    Dim auxCaptcha As clshrcJSCaptcha
                    auxCaptcha = auxClientCon.gObjectTmp_Download(ViewState("checkid") & "_captcha")
                    If auxCaptcha Is Nothing Then
                        auxCaptcha = New clshrcJSCaptcha("test", "test", "form-control")
                        auxClientCon.gObjectTmp_Upload(auxCaptcha, ViewState("checkid") & "_captcha")
                    End If
                    divcheck.InnerHtml = auxCaptcha.gControl_GetBodyDefinition
                    ScriptManager.RegisterStartupScript(Me, Me.GetType, "hrc" & auxCaptcha.ControlID, _
                                                       auxCaptcha.gControl_GetStartupScripts, True)
                    row_check.Visible = True
                End If
                'End If
            End If
        End If

    End Sub

    Protected Sub cmdPswChange_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdPswChange.Click
        If txtnewpwd1.Text <> txtnewpwd2.Text Then
            lblmsg.Text = "La nueva contraseña y su confirmación son diferentes."
        Else
            Dim auxClass As New clshrcGeneral
            If auxClass.Sec.CurrentSecCod > 0 Then
                Dim auxView As Integer = 0
                If Request.QueryString("_view_") IsNot Nothing Then
                    auxView = Val(Request.QueryString("_view_"))
                End If
                If auxView = 4 Then
                    'Contraseña para transferencias
                    auxClass.Conn.gConn_Open()
                    'lblmsg.Text = auxClass.gLogin_ChangeTrnPsw(auxClass.Sec.CurrentSecCod, txtnewpwd1.Text)
                    auxClass.Conn.gConn_Close()
                    If lblmsg.Text = "" Then
                        ScriptManager.RegisterStartupScript(Me, Me.GetType(), "CloseWindow", "self.close();", True)
                    End If
                Else
                    auxClass.Conn.gConn_Open()
                    If auxClass.Sec.gLogin_ChangePassword(txtpwd.Text, txtnewpwd1.Text) = False Then
                        lblmsg.Text = "No se ha cambiado la contraseña."
                    End If
                    auxClass.Conn.gConn_Close()
                    If lblmsg.Text = "" Then
                        lblTitle.Text = "Cambio de contraseña"
                        lblSubtitle.Text = ""
                        lblmsg.Text = "La contraseña se ha cambiado correctamente."
                        row_pswchange1.Visible = False
                        row_pswchange2.Visible = False
                        row_user.Visible = False
                        row_oldpsw.Visible = False
                        cmdPswChange.Visible = False
                        'Master.FindControl("lblmenu").Visible = True
                    End If
                End If
            End If

        End If

    End Sub
End Class
