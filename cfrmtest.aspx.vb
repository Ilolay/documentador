Imports Intelimedia.imComponentes
'Imports clsCusimprojects
Imports System.Diagnostics
Imports System.Data
Partial Class cfrmtest
    Inherits System.Web.UI.Page

    Protected Sub cmdTest_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdTest.Click
        'EventLog.WriteEntry("imDoc", "Test start:" & Server.MapPath("~/wkhtmltopdf/wkhtmltopdf.exe"))
        'Dim auxClass As New clsCusimDOC
        'Dim auxMail As New imMailing(coSMTPServer, coSMTPUser, coSMTPpsw, coSMTPfrom, Val(coSMTPport), coSystemTitle)
        'Response.Write(auxMail.gMail_Send("jbecker@intelimedia.com.ar", "", "", coSystemTitle & "-test", "El mail enviado anteriormente corresponde a un test de envío"))
        'Response.Write(auxMail.LastErrorDescription)
        'Exit Sub
        'For Each auxrow As DataRow In auxClass.Conn.gConn_Query("select * from  DOC_DOCSGN " _
        '                & " LEFT JOIN EMP ON DOC_DOCSGN.empcod=EMP.COD " _
        '                & " where DOC_DOCSGN.doccod=94").Rows
        '    auxMail.gMail_Send(auxrow("mail"), "", "", coSystemTitle & "-test", "El mail enviado anteriormente corresponde a un test de envío")
        'Next
        'Exit Sub
        'Try
        '    Dim auxProcess As ProcessStartInfo = New ProcessStartInfo(Server.MapPath("~/plugins/wkhtmltopdf/wkhtmltopdf.exe"))
        '    'auxProcess = New ProcessStartInfo("c:\Windows\system32\notepad.exe")
        '    auxProcess.Arguments = " ""www.google.com.ar"" ""c:\temp\mtest.pdf""  --footer-font-size 7 --margin-top 27 --username intelimedia.ad\pesuti --password B@rcel0n@08"
        '    'auxProcess.Arguments = " >C:\inetpub\wwwroot\imProduccion\intelimedia_imDoc\documentos\t.txt"
        '    Dim pUsername As String = ""
        '    Dim pPassword As String = ""
        '    If pUsername <> "" Then
        '        Dim auxUsr() As String = Split(pUsername, "\")
        '        If UBound(auxUsr) = 1 Then
        '            auxProcess.UserName = auxUsr(1)
        '            auxProcess.Domain = auxUsr(0)
        '        Else
        '            auxProcess.UserName = auxUsr(0)
        '        End If
        '        If pPassword <> "" Then
        '            Dim auxPassword As String = pPassword
        '            auxProcess.Password = New System.Security.SecureString
        '            For Each auxChar As Char In auxPassword
        '                auxProcess.Password.AppendChar(auxChar)
        '            Next
        '            auxProcess.Password = New System.Security.SecureString
        '            For Each auxChar As Char In pPassword
        '                auxProcess.Password.AppendChar(auxChar)
        '            Next
        '        End If
        '    End If

        '    auxProcess.UseShellExecute = False
        '    auxProcess.RedirectStandardOutput = True
        '    auxProcess.RedirectStandardInput = True
        '    auxProcess.RedirectStandardError = True
        '    'auxProcess.Arguments = ""
        '    Dim proc As Process = Process.Start(auxProcess)
        '    'proc.WaitForInputIdle()
        '    proc.WaitForExit()
        '    Response.Write("imDoc" & proc.Id.ToString)
        'Catch ex As Exception
        '    'EventLog.WriteEntry("imDoc", ex.Message)
        '    Response.Write(ex.Message)
        'End Try

        Dim auxUser As System.Security.Principal.WindowsIdentity = System.Security.Principal.WindowsIdentity.GetCurrent
        lblResult.Text = ""
        lblResult.Text &= Request.Url.Scheme & "://" & Request.Url.Authority & Request.Url.LocalPath & "<br />"
        lblResult.Text &= "Windowsidentity.Name" & auxUser.Name & "<br />"
        Dim auxUserName As String = HttpContext.Current.User.Identity.Name.ToString
        lblResult.Text &= "Identity name" & auxUserName & "<br />"
        Dim auxClass As New clsCusimDOC
        Dim auxConn As clsHrcConnClient = Nothing
        If Session("conn") Is Nothing Then
            lblResult.Text &= "IMCONNCLIENT no instanciado<br />"
        Else
            auxConn = CType(Session("conn"), clsHrcConnClient)
            lblResult.Text &= "IMCONNCLIENT instanciado OK. Version: " & auxConn.ProductDescription & " <br />"
            auxConn.gConn_Open()
            lblResult.Text &= "IMCONNCLIENT activado: " & auxConn.Activated & "<br />"
            lblResult.Text &= "IMCONNCLIENT conectado:" & auxConn.isConnected & "<br />"
            lblResult.Text &= "IMCONNCLIENT ultimo error:" & auxConn.LastErrorDescription & "<br />"
        End If
        Dim auxSecDsc As String = ""
        Dim auxSecurity As clsHrcSecurityClient
        lblResult.Text &= "<br />"
        lblResult.Text &= "Pruebas IMSECURITY<br />"
        If Session("security") Is Nothing Then
            lblResult.Text &= "IMSECURITY no instanciado<br />"
            auxSecDsc = "Administrador"
        Else
            auxSecurity = CType(Session("security"), clsHrcSecurityClient)
            lblResult.Text &= "IMSECURITY instanciado OK. Version:" & auxSecurity.ProductDescription & " <br />"
            auxSecDsc = auxSecurity.CurrentSecDsc
            auxSecDsc = Replace(auxSecDsc, "WILLINER\", "")
            lblResult.Text &= "Ultimo error:" & auxSecurity.LastErrorDescription & " <br />"
            lblResult.Text &= "IMSECURITY activado: " & auxSecurity.Activated & "<br />"
            lblResult.Text &= "Ultimo error:" & auxSecurity.LastErrorDescription & " <br />"
            lblResult.Text &= "IMSECURITY Codigo de LOGIN:" & auxSecurity.CurrentSecCod & "<br />"
            lblResult.Text &= "Ultimo error:" & auxSecurity.LastErrorDescription & " <br />"
            lblResult.Text &= "IMSECURITY nombre de LOGIN:" & auxSecurity.CurrentSecDsc & "<br />"
            lblResult.Text &= "Ultimo error:" & auxSecurity.LastErrorDescription & " <br />"
            lblResult.Text &= "IMSECURITY SID de seguridad:" & auxSecurity.CurrentSidCod & "<br />"
            lblResult.Text &= "Ultimo error:" & auxSecurity.LastErrorDescription & " <br />"
            lblResult.Text &= "IMSECURITY LOGIN:" & auxSecurity.CurrentSecDsc & "<br />"
            lblResult.Text &= "Ultimo error:" & auxSecurity.LastErrorDescription & " <br />"
            lblResult.Text &= "REORGANIZANDO PERMISOS DEL USUARIO<br />"
            'auxSecurity.gLogin_Reorganize(auxSecurity.CurrentSecCod)
            lblResult.Text &= "Ultimo error:" & auxSecurity.LastErrorDescription & " <br />"
            lblResult.Text &= "<br />"
            lblResult.Text &= "CHEQUEO DE PERMISOS A SID solicitado en pantalla<br />"
            
        End If

        lblResult.Text &= "<br />"
        lblResult.Text &= auxClass.gConnectionStringName_Get
        'lblResult.Text &= "Prueba de IMADCLIENT<br />"
        'Try
        '    Dim auxAD As New imADClient
        '    If auxAD Is Nothing Then
        '        lblResult.Text &= "IMADCLIENT no instanciado<br />"
        '    Else
        '        lblResult.Text &= "IMADCLIENT instanciado OK. Version: " & auxAD.ProductDescription & "<br />"

        '        auxAD = auxClass.gAD_ConnectionAdd(auxAD)
        '        lblResult.Text &= "IMADCLIENT prueba de pertenencia del usuario actual al grupo de grpprjadmins<br />"
        '        Dim auxGrpAccesoDsc As String = ConfigurationManager.AppSettings("willinerpry.grpprjadmins")
        '        If auxAD.gUser_IsInGroup(auxSecDsc, auxGrpAccesoDsc) Then
        '            lblResult.Text &= "El usuario " & auxSecDsc & " se encuentra en el grupo " & auxGrpAccesoDsc & "<br />"
        '        Else
        '            lblResult.Text &= "El usuario " & auxSecDsc & " NO se encuentra en el grupo. Ultimo error: " & auxAD.LastErrorDescription & "<br />"
        '        End If
        '    End If
        'Catch ex As Exception

        'End Try
        'lblResult.Text &= "<br />"
        'lblResult.Text &= "Prueba de IMPWACLIENT<br />"
        'Dim auxPWA As New imPWAClient
        'auxPWA = auxClass.gPWA_Open(auxPWA)
        'If auxPWA.gProject_Login Then
        '    lblResult.Text &= "Login a PWA incorrecto<br />"
        'Else
        '    lblResult.Text &= "Login EXITOSO A PWA<br />"
        'End If
        ' ''
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        'If Session("security") IsNot Nothing Then
        '    txtSecCod.Text = CType(Session("security"), imSecurity).CurrentSecDsc
        'End If
    End Sub

    Protected Sub cmdTest0_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdTest0.Click
        '    Dim auxClass As New clsCusimprojects
        '     Dim auxResult As clsCusimprojects.clsprvValidadores = auxClass.gPRO_Validador(txtEmpCod.Text, txtprocod.Text)
        '     lblResult.Text = "Grupo=" & auxResult.ValGrpCod & "//Unidad=" & auxResult.ValUndCod & "//empleado=" & auxResult.ValEmpCod
    End Sub
End Class
