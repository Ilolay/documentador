<%@ Application Language="VB" %>
<script runat="server">

    Sub Application_Start(ByVal sender As Object, ByVal e As EventArgs)
        ' Code that runs on application startup
    
    End Sub
    
    Sub Application_End(ByVal sender As Object, ByVal e As EventArgs)
        ' Code that runs on application shutdown
    End Sub
        
    Sub Application_Error(ByVal sender As Object, ByVal e As EventArgs)
        ' Code that runs when an unhandled error occurs
        Dim exh As Exception = Server.GetLastError.GetBaseException()
        Dim txtError As String = exh.Message ' exh.GetHtmlErrorMessage().ToString()
        'Server.Transfer("gerror.aspx?msg=El sistema no se encuentra disponible. Intente luego. " & txtError)
        Dim auxSysParamName As String = Environment.GetEnvironmentVariable("APP_POOL_ID", EnvironmentVariableTarget.Process)
        Dim auxDebug As New Intelimedia.imComponentes.clshrcDebugging
        auxDebug.gDebug_On(Environ("WINDIR") & "\temp\hrcgeneral_" & auxSysParamName & "_start.txt")
        auxDebug.gDebug_Add("Application error 2:" & txtError)
        auxDebug.gDebug_Add(Request.RawUrl)
        auxDebug.gDebug_Off()
        auxDebug = Nothing
    End Sub
    
    Sub Session_Start(ByVal sender As Object, ByVal e As EventArgs)
        'Inicializacion de variables de session
        Dim auxSessionID As String = Session.SessionID
        Dim auxContext As HttpContext = HttpContext.Current
        If auxContext IsNot Nothing Then
            Session("empcod") = -1
            Session("secsid") = -1
            Session("seccod") = -1
            Session("secdsc") = ""
            Session("Isadmin") = False
            Session("modo_obligado") = False
            If Request.QueryString("inframe") = "1" Then
                Session("inframe") = True
            Else
                Session("inframe") = False
            End If
            Dim auxSysParamName As String = Environment.GetEnvironmentVariable("APP_POOL_ID", EnvironmentVariableTarget.Process)
            Dim auxClass As New clshrcGeneral
            Dim auxDebug As Intelimedia.imComponentes.clshrcDebugging
            auxDebug = New Intelimedia.imComponentes.clshrcDebugging
            auxDebug.gDebug_On(Environ("WINDIR") & "\temp\hrcgeneral_" & auxSysParamName & "_start.txt")
            auxDebug.gDebug_Add("Application start:" & Now.ToString)
            If auxContext.Request Is Nothing Then
                auxDebug.gDebug_Add(auxContext.Request.RawUrl)
            End If
            Dim auxError As String = auxClass.gSystem_Init()
            auxDebug.gDebug_Add("Start init-end")
            If auxClass.Conn Is Nothing Then
                auxDebug.gDebug_Add("Start init-failed: no connection:" & auxError)
                Exit Sub
            Else
                auxDebug.gDebug_Add("Start init-Connection:" & auxError)
            End If
            auxDebug.gDebug_Off()
            auxDebug = Nothing
            
            Try
                auxClass.Conn.gConn_Open()
            Catch ex As Exception
                auxDebug = New Intelimedia.imComponentes.clshrcDebugging
                auxDebug.gDebug_On(Environ("WINDIR") & "\temp\hrcgeneral_" & auxSysParamName & ".txt")
                auxDebug.gDebug_Add("Application error 1:" & ex.Message)
                auxDebug.gDebug_Add(Request.RawUrl)
                auxDebug.gDebug_Off()
                auxDebug = Nothing
                Server.Transfer("~/gerror.aspx?msg=Imposible iniciar el sistema.Error 1")
                Session.Abandon()
            End Try
            If LicParam Is Nothing Then
                Response.Redirect("hrclicensing.aspx")
                Exit Sub
            End If
            auxError = Replace(auxError, Chr(34), "")
            auxError = Replace(auxError, "'", "")
            auxError = Replace(auxError, Chr(10), "")
            auxError = Replace(auxError, Chr(13), "")
            Dim auxHrcSesID As String = ""
            If Request.QueryString("_sesid_") IsNot Nothing Then
                auxHrcSesID = Request.QueryString("_sesid_").Trim
            End If
            Dim auxRequestRawUrl As String = Request.RawUrl.ToLower
            If auxHrcSesID = "" And InStr(Request.RawUrl, "/hrcbinaries.aspx?") = 0 And InStr(Request.RawUrl, "/getbinary.aspx?") = 0 Then
                If auxError <> "" Then
                    auxClass.gSystem_End()
                    Server.Transfer("~/gerror.aspx?msg=Imposible iniciar el sistema [" & auxError & "]")
                    Session.Abandon()
                ElseIf HttpContext.Current.User.Identity.Name.ToString = "" Or _
                    (InStr(auxRequestRawUrl, "/public_login.aspx") <> 0 And Request.QueryString("_view_") Is Nothing) Then ' InStr(Request.QueryString.ToString, "_view_=3") <> 0) Then
                    auxClass.gSystem_End()
                    Server.Transfer("~/PUBLIC_LOGIN.ASPX?_view_=5")
                ElseIf auxClass.gSystem_CheckAccess = False Then
                    Session("secsid") = -1
                    Session("secdsc") = ""
                    auxClass.gSystem_End()
                    Server.Transfer("~/gerror.aspx?msg=No posee acceso al sistema [" & HttpContext.Current.User.Identity.Name.ToString & "]")
                    Session.Abandon()
                Else
                    auxClass.gSystem_End()
                    If InStr(auxRequestRawUrl, "/getbinary.aspx?") <> 0 Then
                        Response.Redirect("~/" & hrcFormInitial & ".aspx")
                    End If
                End If
            Else
                If auxClass.gSystem_CheckAccess(auxHrcSesID) = False Then
                    auxClass.gSystem_End()
                    Server.Transfer("~/gerror.aspx?msg=Imposible iniciar el sistema por sesion:" & auxHrcSesID & "[" & auxClass.Sec.LastErrorDescription & "]")
                    Session.Abandon()
                End If
            End If
          
        End If
    End Sub

    Sub Session_End(ByVal sender As Object, ByVal e As EventArgs)
        ' Code that runs when a session ends. 
        ' Note: The Session_End event is raised only when the sessionstate mode
        ' is set to InProc in the Web.config file. If session mode is set to StateServer 
        ' or SQLServer, the event is not raised.
    End Sub
       
</script>