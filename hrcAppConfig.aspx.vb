
Partial Class hrcAppConfig
    Inherits System.Web.UI.Page
    Protected Sub cmdImageEmpty_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdImageEmpty.Click
        Dim auxClass As New clshrcGeneral
        auxClass.Conn.gConn_Open()
        auxClass.Conn.gConn_ImageToBLOB(fleimageempty.FileName, fleimageempty.FileBytes, 100, 100, -1, 100, 100)
        auxClass.Conn.gConn_Close()
    End Sub
    Protected Sub cmdLicCheck_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdLICCheck.Click
        Dim auxClass As New clshrcGeneral
        auxClass.gSystem_Init()
        auxClass.gLicensing_Recheck()
        auxClass.gSystem_End()
        hrcGlobalValue = Nothing
        Session.Abandon()

    End Sub
    Protected Sub cmdReorganizeGrp_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdReorganizeGrp.Click

        Dim auxClass As New clshrcGeneral
        auxClass.DebugLogOn = True
        auxClass.Conn.gConn_Open()
        Page.Server.ScriptTimeout = 60 * 60 * 3
        auxClass.Sec.gTools_ReimpactGroups()
        lblerror.Text &= "Last:" & auxClass.Conn.LastErrorDescription
        auxClass.Conn.gConn_Close()
    End Sub
    Protected Sub cmdTME_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdTME.Click
        hrcProcessQueue.gProcessor_Stop_Now()
        Dim auxClass As New clshrcGeneral
        auxClass.gSystem_Init()
        Dim auxDate As Date = auxClass.Conn.gField_GetDate(txtTME.Text)
        auxClass.gSystem_GotoToDateTme(auxDate)
        auxClass.gSystem_End()
        hrcGlobalValue = Nothing
        Session.Abandon()

    End Sub
    Protected Sub cmdTMEGoNow_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdTMEGoNow.Click
        Dim auxClass As New clshrcGeneral
        auxClass.gSystem_Init()
        auxClass.gSystem_GotoToNow()
        auxClass.gSystem_End()
        hrcProcessQueue.gProcessor_Stop_Now()
        hrcGlobalValue = Nothing
        Session.Abandon()

    End Sub
    Private Sub gcmdSys_CommandHandler(ByVal pControl As clsHrcJSControlBasic, ByVal pValues As clshrcBagValues)
        Dim auxAsyncState_Initial As Boolean = False
        If pValues.gValue_Get("HRCASYNCSTATE") = "1" Then
            auxAsyncState_Initial = True
        End If

        Select Case auxAsyncState_Initial
            Case True

                Select Case pControl.BagValues.gValue_Get("ACTION").ToString
                    Case "SYS_UPDATE"
                        Dim auxClass As New clshrcGeneral
                        pControl.BagValues.gValue_Add("RESULT_MSG", auxClass.gSys_Update)
                    Case "SYS_INIT"
                        Dim auxClass As New clshrcGeneral
                        pControl.BagValues.gValue_Add("RESULT_MSG", auxClass.gDB_Initialize)
                End Select

            Case False
                Dim auxMsg As String = pControl.BagValues.gValue_Get("RESULT_MSG")
                If auxMsg = "" Then
                    auxMsg = "Terminado!"
                End If
                pValues.gValue_Add("HRC_RESULTS", "[{""result"":""1"",""RESULT_MSG"":""" & auxMsg & """}]")
            Case Else
        End Select
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim auxPermRead As Boolean = False
        Dim auxIsInitialization As Boolean = False
        If Session("Isadmin") Then
            auxPermRead = True
        ElseIf LicParam Is Nothing Then
            If ConfigurationManager.AppSettings("Initialize_Avail") IsNot Nothing Then
                If ConfigurationManager.AppSettings("Initialize_Avail").ToString = "1" Then
                    auxIsInitialization = True
                End If
            End If
        End If
        If auxPermRead = False And auxIsInitialization = False Then
            Server.Transfer("gerror.aspx?msg=No posee acceso")
        ElseIf Not IsPostBack Then
            'If ConfigurationManager.AppSettings("Initialize_Avail") <> "1" Then
            'Response.Redirect("~/gerror.aspx?msg=no se encuentra disponible la actualización del sistema")
            'Else
            Dim auxClass As New clshrcGeneral
            Dim auxScript As String = ""
            Dim auxClientCon As New imClientConnection

            Dim auxButton As clsHrcJSButton
            'Boton de inicialización
            If auxIsInitialization Then
                auxButton = New clsHrcJSButton("cmdsysinit", "Inicializar sistema", "boton-acciones")
                AddHandler auxButton.EventCommandHandler, AddressOf gcmdSys_CommandHandler
                auxButton.BagValues.gValue_Add("ACTION", "SYS_INIT")
                auxButton.Title = "Inicializar sistema"
                auxButton.AsyncCallEnabled = True
                auxButton.RaiseCommandOnClick = True
                auxButton.EventOnAsyncCallSucess = "$('#" & lblerror.ClientID & "').html(pdata[0]['RESULT_MSG']);"
                lstsysinit.InnerHtml = auxButton.gControl_GetBodyDefinition
                ScriptManager.RegisterStartupScript(Me, Me.GetType, "hrc" & auxButton.ControlID, _
                                                   auxButton.gControl_GetStartupScripts, True)

            End If
            'Boton de actualización
            auxButton = New clsHrcJSButton("cmdsysupdate", "Actualizar sistema", "boton-acciones")
            AddHandler auxButton.EventCommandHandler, AddressOf gcmdSys_CommandHandler
            auxButton.BagValues.gValue_Add("ACTION", "SYS_UPDATE")
            auxButton.Title = "Actualizar sistema"
            auxButton.AsyncCallEnabled = True
            auxButton.RaiseCommandOnClick = True
            auxButton.EventOnAsyncCallSucess = "$('#" & lblerror.ClientID & "').html(pdata[0]['RESULT_MSG']);"
            lstsysupdate.InnerHtml = auxButton.gControl_GetBodyDefinition
            ScriptManager.RegisterStartupScript(Me, Me.GetType, "hrc" & auxButton.ControlID, _
                                               auxButton.gControl_GetStartupScripts, True)


            txtTME.Text = Now.AddSeconds(Val(auxClass.gSystem_GetParameterByID(enumSysIDParams.SystemDatetimeAdjust)))
            lblVersionAvail.Text = "Versión disponible:" & auxClass.coVersion.ToString & " ( " & auxClass.coVersionDateTime.ToString("d/M/yyyy") & " )"
            lblVersionCur.Text = "Versión aplicada:" & auxClass.gSystem_GetParameterByID(1)
            'End If
        End If

    End Sub
End Class
