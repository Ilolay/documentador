Imports Intelimedia.imComponentes

Imports System.Data
Imports Intelimedia.inTasks

Imports System.IO
Imports Intelimedia.Hercules.Language
Imports Intelimedia.Hercules.Storage
Partial Class hrcComponentsTest
    Inherits System.Web.UI.Page

    Private Sub gcmdButton1_CommandHandler(ByVal pControl As clsHrcJSControlBasic, ByVal pValues As clshrcBagValues)
        Dim auxAsyncState_Initial As Boolean = False
        If pValues.gValue_Get("HRCASYNCSTATE") = "1" Then
            auxAsyncState_Initial = True
        End If
        Dim auxAction As String = pControl.BagValues.gValue_Get("ACTION").ToString
        Select Case auxAsyncState_Initial
            Case True
                pControl.BagValues.gValue_Add("RESULT_MSG", "Action:" & auxAction & "-OK")
            Case False
                Dim auxMsg As String = pControl.BagValues.gValue_Get("RESULT_MSG")
                If auxMsg = "" Then
                    auxMsg = "Terminado!"
                End If
                auxMsg = pControl.ControlID & "-Action:" & auxAction & "-" & auxMsg & "-" & auxAsyncState_Initial
                pValues.gValue_Add("HRC_RESULTS", "[{""result"":""1"",""RESULT_MSG"":""" & auxMsg & """}]")
            Case Else
        End Select
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim auxConn As clsHrcConnClient = Session("conn")
        auxConn = auxConn.gComponent_CreateInstance

        If Not IsPostBack Then
            Dim auxHtml As New Intelimedia.Hercules.Language.clsHrcCodeHTML
            Dim auxBagValues As New clshrcBagValues
            auxBagValues.gValue_Add("reqcod", 2)
            auxBagValues.gValue_Add("dialogtype", 1)
            Dim auxHrcContext As clsHrcJSContext = Context.Session("hrcContext")
            Dim auxalertForm As New clsHrcJSAlertForm(auxHrcContext.Alert, auxHrcContext.Conn)

            cmdDialog.OnClientClick = auxalertForm.gAlerts_GetCodeShowAlertForm(auxBagValues) & ";return false;"
            cmdDialog.UseSubmitBehavior = False
        End If


        Dim auxScript As String
        Dim auxClientCon As New imClientConnection

        'Boton SYNC
        Dim auxButton1 As New clsHrcJSButton("CMDBUTTON1", "Tarea sincrónica", "boton-acciones")
        AddHandler auxButton1.EventCommandHandler, AddressOf gcmdButton1_CommandHandler
        auxButton1.BagValues.gValue_Add("ACTION", "SYS_BUTTON1")
        auxButton1.Title = "Tarea sincrónica"
        auxButton1.RaiseCommandOnClick = True
        auxButton1.EventOnClick = "$('#" & lblerror.ClientID & "').html(pdata[0]['RESULT_MSG']);"
        auxButton1.gJSCommand_Add("CLICK", "$('#" & lblerror.ClientID & "').html('Start sync...');")
        ScriptManager.RegisterStartupScript(Me, Me.GetType, "hrc" & auxButton1.ControlID, _
                                           auxButton1.gControl_GetStartupScripts, True)
        lstButtonSync.InnerHtml = auxButton1.gControl_GetBodyDefinition

        'Boton de actualización
        Dim auxButton2 As New clsHrcJSButton("CMDBUTTON2", "Tarea asincrónica", "boton-acciones")
        AddHandler auxButton2.EventCommandHandler, AddressOf gcmdButton1_CommandHandler
        auxButton2.BagValues.gValue_Add("ACTION", "SYS_BUTTON2")
        auxButton2.Title = "Tarea Asincrónica"
        auxButton2.AsyncCallEnabled = True
        auxButton2.RaiseCommandOnClick = True
        auxButton2.gJSCommand_Add("CLICK", "$('#" & lblerror.ClientID & "').html('Start async...');")
        auxButton2.EventOnClick = "$('#" & lblerror.ClientID & "').html(pdata[0]['RESULT_MSG']);"
        auxButton2.EventOnAsyncCallSucess = "$('#" & lblerror.ClientID & "').html(pdata[0]['RESULT_MSG']);"
        ScriptManager.RegisterStartupScript(Me, Me.GetType, "hrc" & auxButton2.ControlID, _
                                           auxButton2.gControl_GetStartupScripts, True)
        lstButtonAsync.InnerHtml = auxButton2.gControl_GetBodyDefinition

        Dim auxDT As DataTable

        auxDT = auxConn.gHierarchy_GenerateTable("SELECT cod as q_cod,dsc as q_dsc,undcodsup,NULL,1 as tipo,cod" _
                                                     & " FROM UND WHERE cod > 0 AND (baja {#ISNULL#} OR baja=0)", "")
        Dim auxColumnView As DataColumn

        auxColumnView = New DataColumn("q_grdleft_field", System.Type.GetType("System.String"))
        auxDT.Columns.Add(auxColumnView)
        auxColumnView = New DataColumn("q_grdparent", System.Type.GetType("System.String"))
        auxDT.Columns.Add(auxColumnView)
        auxColumnView = New DataColumn("q_grdisleaf", System.Type.GetType("System.String"))
        auxDT.Columns.Add(auxColumnView)
        auxColumnView = New DataColumn("q_grdexpanded_field", System.Type.GetType("System.String"))
        auxDT.Columns.Add(auxColumnView)

        Dim auxDTValues As New DataTable
        auxColumnView = New DataColumn("cod", System.Type.GetType("System.String"))
        auxDTValues.Columns.Add(auxColumnView)
        auxColumnView = New DataColumn("q_cod", System.Type.GetType("System.String"))
        auxDTValues.Columns.Add(auxColumnView)
        Dim auxNewRow As DataRow = auxDTValues.NewRow
        auxNewRow("cod") = "NULL"
        auxNewRow("q_cod") = "*"
        auxDTValues.Rows.Add(auxNewRow)
        Dim auxResults() As DataRow

        Dim auxParents As New List(Of Integer)
        Dim auxMaxLevel As Integer = -1
        Dim auxLevel As Integer = -1
        For Each auxrow As DataRow In auxDT.Rows
            auxResults = auxDTValues.Select("q_cod='" & auxrow("q_cod") & "'")
            Dim auxCod As Integer
            If auxResults.Length = 0 Then
                auxNewRow = auxDTValues.NewRow
                auxCod = auxDTValues.Rows.Count
                auxNewRow("cod") = auxCod
                auxNewRow("q_cod") = auxrow("q_cod")
                auxDTValues.Rows.Add(auxNewRow)
            Else
                auxCod = auxResults(0)("cod")
            End If
            auxrow("q_cod") = auxCod
            auxrow("q_grdisleaf") = "true"
            If auxrow("q_parent") <> "*" Then
                auxrow("q_grdparent") = auxDTValues.Select("q_cod='" & auxrow("q_parent") & "'")(0)("cod")
                auxParents.Add(auxrow("q_grdparent"))
            End If
            If auxrow("q_group") > auxMaxLevel Then
                auxMaxLevel = auxrow("q_group")
            End If
            auxLevel = auxMaxLevel - auxrow("q_group")

            auxrow("q_grdleft_field") = auxLevel   'Nivel
            auxrow("q_grdexpanded_field") = "true"
        Next

        For Each auxRow As DataRow In auxDT.Rows
            If auxParents.IndexOf(auxRow("q_cod")) <> -1 Then
                auxRow("q_grdisleaf") = "false"
            End If
        Next
        Dim auxObjectExplorer As New clshrcObjectExplorer("obj", "hrcGrdData.ashx", auxDT, auxConn, auxClientCon)

        'auxObjectExplorer.gAutosuggest_Enabled("SELECT UND.cod AS cod,UND.dsc as dsc," & enumEntities.coEntityUND & " as q_type,'Unidad' FROM UND   WHERE (  (UND.dsc LIKE '%{#HRCDSC#}%' ) AND (UND.baja = 0 OR UND.baja  IS NULL)) AND UND.cod >= 1" _
        '                                            & " UNION ALL " _
        '                                            & "(SELECT EMP.cod as cod,EMP.dsc as dsc," & enumEntities.coEntityEMP & " as q_type,'Colaborador' FROM EMP   WHERE ( (EMP.dsc LIKE '%{#HRCDSC#}%' ) AND (EMP.baja = 0 OR EMP.baja  IS NULL)) AND EMP.cod >= 1)" _
        '                                            & " ORDER BY dsc,q_type")

        auxObjectExplorer.gAutosuggest_Enabled("SELECT 1 ,UND.dsc as dsc,1 as q_type,'Unidad' FROM UND   WHERE (  (UND.dsc LIKE '%{#HRCDSC#}%' ) AND (UND.baja = 0 OR UND.baja  IS NULL)) AND UND.cod >= 1" _
                                                    & " UNION ALL " _
                                                    & "SELECT 1,UND.dsc as dsc,1 as q_type,'Unidad' FROM UND   WHERE (  (UND.dsc LIKE '%{#HRCDSC#}%' ) AND (UND.baja = 0 OR UND.baja  IS NULL)) AND UND.cod >= 1" _
                                                    & " ORDER BY dsc,q_type")

        auxObjectExplorer.ObjectExplorerEnabled = True
        auxObjectExplorer.SearchByDsc = True
        auxObjectExplorer.CentralGrid.gSelectList_Add(1, 1, "(SELECT UND.cod AS cod,UND.dsc as dsc,1 as q_type FROM UND   WHERE ((((UND.undcodsup = {#HRCCOD#} AND '{#HRCDSC#}'='')  OR  (UND.dsc LIKE '%{#HRCDSC#}%' AND '{#HRCDSC#}'<>''))) AND (UND.baja = 0 OR UND.baja  IS NULL)) AND UND.cod >= 1)")
        auxObjectExplorer.CentralGrid.gSelectList_Add(1, 2, "(SELECT EMP.cod as cod,EMP.dsc as dsc,2 as q_type FROM EMP   WHERE ((((EMP.undcod ={#HRCCOD#} AND '{#HRCDSC#}'='')  OR  (EMP.dsc LIKE '%{#HRCDSC#}%' AND '{#HRCDSC#}'<>''))) AND (EMP.baja = 0 OR EMP.baja  IS NULL)) AND EMP.cod >= 1)")
        auxObjectExplorer.CentralGrid.gSelectTypeFilter_Add(1)
        auxObjectExplorer.CentralGrid.gSelectTypeFilter_Add(2)
        auxScript = auxObjectExplorer.gControl_GetStartupScripts
        ScriptManager.RegisterStartupScript(Me, Me.GetType, "hrc" & auxObjectExplorer.ControlID, _
                                           auxScript, True)

        lstInput.InnerHtml = auxObjectExplorer.gControl_GetBodyDefinition
        '
        Dim auxObjectExplorer2 As New clshrcObjectExplorer("obj2", "hrcGrdData.ashx", auxDT, auxConn, auxClientCon)

        auxObjectExplorer2.gAutosuggest_Enabled("SELECT UND.cod AS cod,UND.dsc as dsc,1 as q_type,'Unidad' FROM UND   WHERE (  (UND.dsc LIKE '%{#HRCDSC#}%' ) AND (UND.baja = 0 OR UND.baja  IS NULL)) AND UND.cod >= 1" _
                                                    & " UNION ALL " _
                                                    & "(SELECT EMP.cod as cod,EMP.dsc as dsc,2 as q_type,'Colaborador' FROM EMP   WHERE ( (EMP.dsc LIKE '%{#HRCDSC#}%' ) AND (EMP.baja = 0 OR EMP.baja  IS NULL)) AND EMP.cod >= 1)" _
                                                    & " ORDER BY dsc,q_type")
        auxObjectExplorer2.ObjectExplorerEnabled = True
        auxObjectExplorer2.CentralGrid.gSelectList_Add(1, 2, "(SELECT UND.cod AS cod,UND.dsc as dsc,1 as q_type FROM UND   WHERE ((((UND.undcodsup = {#HRCCOD#} AND '{#HRCDSC#}'='')  OR  (UND.dsc LIKE '%{#HRCDSC#}%' AND '{#HRCDSC#}'<>''))) AND (UND.baja = 0 OR UND.baja  IS NULL)) AND UND.cod >= 1)")
        auxObjectExplorer2.CentralGrid.gSelectList_Add(1, 2, "(SELECT EMP.cod as cod,EMP.dsc as dsc,2 as q_type FROM EMP   WHERE ((((EMP.undcod ={#HRCCOD#} AND '{#HRCDSC#}'='')  OR  (EMP.dsc LIKE '%{#HRCDSC#}%' AND '{#HRCDSC#}'<>''))) AND (EMP.baja = 0 OR EMP.baja  IS NULL)) AND EMP.cod >= 1)")
        auxObjectExplorer2.CentralGrid.gSelectTypeFilter_Add(1)
        auxObjectExplorer2.CentralGrid.gSelectTypeFilter_Add(2)

        auxScript = auxObjectExplorer2.gControl_GetStartupScripts
        ScriptManager.RegisterStartupScript(Me, Me.GetType, "hrc" & auxObjectExplorer2.ControlID, _
                                           auxScript, True)
        lstInput2.InnerHtml = auxObjectExplorer2.gControl_GetBodyDefinition
        '////
        Dim auxObjectExplorer3 As New clshrcObjectExplorer("obj3", "hrcGrdData.ashx", Nothing, auxConn, auxClientCon)

        auxObjectExplorer3.gAutosuggest_Enabled("SELECT UND.cod AS cod,UND.dsc as dsc,1 as q_type,'Unidad' FROM UND   WHERE (  (UND.dsc LIKE '%{#HRCDSC#}%' ) AND (UND.baja = 0 OR UND.baja  IS NULL)) AND UND.cod >= 1" _
                                                    & " UNION ALL " _
                                                    & "(SELECT EMP.cod as cod,EMP.dsc as dsc,2 as q_type,'Colaborador' FROM EMP   WHERE ( (EMP.dsc LIKE '%{#HRCDSC#}%' ) AND (EMP.baja = 0 OR EMP.baja  IS NULL)) AND EMP.cod >= 1)" _
                                                    & " ORDER BY dsc,q_type")
        auxObjectExplorer3.StringStartText = "aa"
        auxScript = auxObjectExplorer3.gControl_GetStartupScripts
        ScriptManager.RegisterStartupScript(Me, Me.GetType, "hrc" & auxObjectExplorer3.ControlID, _
                                           auxScript, True)
        lstInput3.InnerHtml = auxObjectExplorer3.gControl_GetBodyDefinition

        '/////
        If Session("Isadmin") = False Then
            Server.Transfer("cfrmrequerimientos.aspx")
        Else
        End If
    End Sub

  
End Class




