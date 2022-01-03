Imports Intelimedia.imComponentes
Imports clsCusimDOC
Imports System.Data
Partial Class hrcMyConfig
    Inherits System.Web.UI.Page

    Protected Sub cmdSave_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdSave.Click
        Dim auxClass As New clsCusimDOC
        auxClass.gLoginPreference_SetValue(enumPrefType.coprfmyconfig, "hlpmode", optHlpMode.SelectedValue)
        If chkAlertsONscreen.Checked Then
            auxClass.gLoginPreference_SetValue(enumPrefType.coprfmyconfig, "alertsonscreen", "1")
        Else
            auxClass.gLoginPreference_SetValue(enumPrefType.coprfmyconfig, "alertsonscreen", "0")
        End If

        auxClass.gPreference_Save(auxClass.Sec.CurrentSidCod, enumPrefType.coprfmyconfig)
        auxClass.gUser_ResolveMenu()
        Session("hlpmode") = optHlpMode.SelectedValue
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        If Not IsPostBack Then
            Dim auxClass As New clsCusimDOC
            Dim auxSecCod As Integer = auxClass.Sec.CurrentSourceSecCod
            If auxSecCod < 1 Then
                auxSecCod = auxClass.Sec.CurrentSecCod
            End If
            If auxSecCod < 1 Then
                Exit Sub
            End If
            lblsubtitle.Text = auxClass.Conn.gConn_QueryValueString("SELECT dsc FROM EMP WHERE seccod = " & auxSecCod)
            optHlpMode.Items.Add(New ListItem("Sin ayuda", 1))
            optHlpMode.Items.Add(New ListItem("Ayuda básica", 10))
            'optHlpMode.Items.Add(New ListItem("Ayuda intermedia", 30))

            'If auxClass.Sec.gSID_CheckAccess(enumAccessType.coQHLPAyudasModificar) Then
            '    optHlpMode.Items.Add(New ListItem("Edición de ayuda", 70))
            'End If
            Dim auxHlpMode As Integer = auxClass.Conn.gField_GetInt(auxClass.gLoginPreference_GetValue(enumPrefType.coprfmyconfig, "hlpmode"), 1)
            Dim auxItem As ListItem = optHlpMode.Items.FindByValue(auxHlpMode)
            If auxItem IsNot Nothing Then
                optHlpMode.SelectedIndex = optHlpMode.Items.IndexOf(auxItem)
            Else
                optHlpMode.SelectedIndex = 0
            End If

            Dim auxAlertsonScreen As Boolean = False
            If auxClass.gLoginPreference_GetValue(enumPrefType.coprfmyconfig, "alertsonscreen") = "1" Then
                auxAlertsonScreen = True
            Else
                auxAlertsonScreen = False
            End If
            chkAlertsONscreen.Checked = auxAlertsonScreen


            gData_Get()
        End If
    End Sub

    Protected Sub cmdDeleteAllPreferences_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdDeleteAllPreferences.Click
        Dim auxClass As New clsCusimDOC
        auxClass.gLoginPreference_DeleteAll()
    End Sub

    Protected Sub edtsidcoddelete_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs)
        gHierarchyControl_SetValue(-1, -1, "", edtsidcodview, hdnsidcod, hdnsidcodtype, edtsidcoddelete, True)
    End Sub

    Protected Sub cmdfrmupdpanelsecprmupdcancel_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        updpanelfrmsecperm.Hide()
    End Sub
    Protected Sub cmdedtsidcodshowpanel_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        objectexplorer_Show(hdnsidcod.UniqueID, edtsidcodview.UniqueID, hdnsidcodtype.UniqueID, "{#CHR34#}" & enumEntities.coEntityUND & "{#CHR34#}", _
                            "{#CHR34#}" & enumEntities.coEntityUND & "{#CHR34#}{#CHR34#}" & enumEntities.coEntityEMP & "{#CHR34#}{#CHR34#}" & enumEntities.coEntityDOC_EQU & "{#CHR34#}", _
                            edtsidcoddelete.UniqueID)
    End Sub

    Protected Sub cmdfrmupdpanelsecprmupdconfirm_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        updpanelfrmsecperm.Hide()
        Dim auxClass As New clsCusimDOC
        Dim auxSidCod As Integer = auxClass.Conn.gField_GetInt(Request.QueryString("param1"))
        Select Case auxClass.Conn.gField_GetInt(hdnsidcodtype.Value)
            Case enumEntities.coEntityEMP
                Dim auxCod As Integer = auxClass.Conn.gConn_QueryValue("SELECT seccod FROM EMP WHERE cod=" & auxClass.Conn.gField_GetInt(hdnsidcod.Value))
                auxClass.Sec.gLogin_CreateDelegationFromCurrentLoginToOtherLogin(auxCod, _
                                                                           auxClass.Conn.gField_GetDateInUTC(CType(frmupdpanelfrmsecperm.FindControl("txtdeldatetimefrom"), TextBox).Text), _
                                                                           auxClass.Conn.gField_GetDateInUTC(CType(frmupdpanelfrmsecperm.FindControl("txtdeldatetimeto"), TextBox).Text), "1")
            Case enumEntities.coEntityDOC_EQU
                Dim auxCod As Integer = auxClass.Conn.gConn_QueryValue("SELECT grpcod FROM DOC_EQU WHERE cod=" & auxClass.Conn.gField_GetInt(hdnsidcod.Value))
                auxClass.Sec.gLogin_CreateDelegationFromCurrentLoginToGroup(auxCod, _
                                                                           auxClass.Conn.gField_GetDateInUTC(CType(frmupdpanelfrmsecperm.FindControl("txtdeldatetimefrom"), TextBox).Text), _
                                                                           auxClass.Conn.gField_GetDateInUTC(CType(frmupdpanelfrmsecperm.FindControl("txtdeldatetimeto"), TextBox).Text), "1")
            Case enumEntities.coEntityUND
                Dim auxCod As Integer = auxClass.Conn.gConn_QueryValue("SELECT grpcodresp FROM UND WHERE cod=" & auxClass.Conn.gField_GetInt(hdnsidcod.Value))
                auxClass.Sec.gLogin_CreateDelegationFromCurrentLoginToGroup(auxCod, _
                                                                           auxClass.Conn.gField_GetDateInUTC(CType(frmupdpanelfrmsecperm.FindControl("txtdeldatetimefrom"), TextBox).Text), _
                                                                           auxClass.Conn.gField_GetDateInUTC(CType(frmupdpanelfrmsecperm.FindControl("txtdeldatetimeto"), TextBox).Text), "1")
        End Select
        Page.DataBind()
        gData_Get()
    End Sub
    Protected Sub cmdToggleIsAdmin_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdToggleIsAdmin.Click
        Dim auxclass As New clsCusimDOC
        Session("isadmin") = Not Session("isadmin")
        If Session("isadmin") Then
            cmdToggleIsAdmin.Text = "Desactivar perfil administrador"
            auxclass.gLoginPreference_SetValue(enumPrefType.coprfmyconfig, "isadmin_enabled", "1")
        Else
            auxclass.gLoginPreference_SetValue(enumPrefType.coprfmyconfig, "isadmin_enabled", "0")
            cmdToggleIsAdmin.Text = "Activar perfil administrador"
        End If
        auxclass.gPreference_Save(auxclass.Sec.CurrentSidCod, enumPrefType.coprfmyconfig)
        auxclass.gSystem_SetContext() '(pAdminEnabled:=Session("isadmin"))
        auxclass.gUser_ResolveMenu()
    End Sub
    Private Sub gData_Get()
        Dim auxClass As New clsCusimDOC

        Dim auxIsAdmin As Boolean = auxClass.Sec.gMember_IsInGroupByID(auxClass.Sec.CurrentSidCod, coGroupIDAdmins)
        If auxIsAdmin = False Then
            If auxIsAdmin = False Then
                auxIsAdmin = auxClass.Sec.gMember_IsInGroupByID(auxClass.Sec.CurrentSidCod, coGroupDocumentadorAdministradores)
            End If
        End If
        If auxIsAdmin Then
            cmdToggleIsAdmin.Visible = True
            If Session("isadmin") Then
                cmdToggleIsAdmin.Text = "Desactivar perfil administrador"
            Else
                cmdToggleIsAdmin.Text = "Activar perfil administrador"
            End If
        End If


        Dim auxDT As DataTable = auxClass.Sec.gLogin_ResolveDelegatedSessionFromCurrentLogin
        Dim auxRow As DataRow = auxDT.NewRow
        auxRow("sidcod") = -1
        auxRow("sidtypecod") = 0
        auxDT.Rows.Add(auxRow)
        grdDelegations.DataSource = auxDT
        grdDelegations.DataBind()


        'grdDelegationsToMy.DataSource = auxClass.Sec.gLogin_ResolveDelegatedSessionToCurrentLogin
        'grdDelegationsToMy.DataBind()
        If auxClass.Sec.IsDelegatedSession Then
            fmeDelegations.ActiveViewIndex = 0
            fmeDelegations.ActiveViewIndex = -1
            lbldeactivate.Text = "Actualmente ha iniciado una sesión delegada por " & auxClass.Sec.CurrentSecDsc
            ' row_help_title.Visible = False
            ' row_tools.Visible = False
            ' row_tools_title.Visible = False
        Else
            fmeDelegations.ActiveViewIndex = 1
        End If
    End Sub
    Protected Sub grdDelegations_RowCommand(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles grdDelegations.RowCommand
        Select Case e.CommandName
            Case "CMDDELDELETE"
                Dim auxClass As New clsCusimDOC
                auxClass.Sec.gLogin_DeleteDelegation(e.CommandArgument)
                gData_Get()
            Case "CMDDELINSERT"
                frmupdpanelfrmsecperm.ChangeMode(FormViewMode.Insert)
                lblfrmupdpanelsecprmsubtitle.Text = "Nueva delegación"
                updpanelfrmsecperm.Show()
                gHierarchyControl_SetValue(-1, -1, "", edtsidcodview, hdnsidcod, hdnsidcodtype, edtsidcoddelete, True)
                updupdpanelfrmsecperm.Update()
            Case "CMDACTIVATE"
                Dim auxClass As New clsCusimDOC
                If auxClass.gSystem_LogonDelegatedSession(e.CommandArgument) Then
                    gData_Get()
                End If
        End Select
    End Sub
    Protected Sub cmdDeactivate_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim auxClass As New clsCusimDOC
        auxClass.gSystem_LogoffDelegatedSession()
        gData_Get()
    End Sub
    Protected Sub grdDelegations_RowCreated(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles grdDelegations.RowDataBound
        If e.Row.RowType = DataControlRowType.DataRow Then
            If e.Row.DataItem IsNot Nothing Then
                If CInt(e.Row.DataItem("sidcod")) = -1 Then
                    e.Row.Visible = False
                Else
                End If
            End If
        End If
    End Sub
    '/////////////
    Protected Sub cmdobjectexplorerfilter_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        tvwmodalpopuppnlobjectexplorer_SelectedNodeChanged(tvwmodalpopuppnlobjectexplorer, Nothing)
    End Sub
    Protected Sub grdobjectexplorer_ItemDeleted(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewCommandEventArgs)

    End Sub
    Protected Sub grdobjectexplorer_ItemInserted(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewCommandEventArgs)

    End Sub
    Protected Sub grdobjectexplorer_ItemSelected(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewCommandEventArgs)

    End Sub
    Protected Sub grdobjectexplorer_ItemUpdated(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewCommandEventArgs)

    End Sub
    Protected Sub grdobjectexplorer_RowCommand(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewCommandEventArgs)
        Select Case e.CommandName.ToUpper()
            Case "UPDATE"
                grdobjectexplorer_ItemUpdated(sender, e)
            Case "INSERT"
                grdobjectexplorer_ItemInserted(sender, e)
        End Select

    End Sub
    Protected Sub grdobjectexplorer_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs)
        modalpopuppnlobjectexplorer_Select(grdobjectexplorer.SelectedDataKey(0), grdobjectexplorer.SelectedDataKey(1), CType(grdobjectexplorer.SelectedRow.Cells(2).Controls(1), LinkButton).Text)
    End Sub
    Protected Sub cmdobjectexplorercancel_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        modalpopuppnlobjectexplorer_Cancel()
    End Sub
    Protected Sub cmdobjectexplorerselect_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        modalpopuppnlobjectexplorer_Select(grdobjectexplorer.SelectedDataKey(0), grdobjectexplorer.SelectedDataKey(1), grdobjectexplorer.SelectedRow.Cells(2).Text)
    End Sub
    Public Sub modalpopuppnlobjectexplorer_Cancel()
        ViewState("modalpopuppnlobjectexplorer_mode") = Nothing
        pnlobjectexplorer.Hide()

    End Sub
    Public Sub modalpopuppnlobjectexplorer_Load(ByVal pCod As Integer, ByVal pParentNode As TreeNode, ByVal pObjectType As Integer)
        Dim auxQuery As String = ""
        Select Case pObjectType
            Case enumEntities.coEntityUND
                auxQuery = "SELECT UND.cod,UND.dsc," & enumEntities.coEntityUND & " as objecttype FROM UND   WHERE ((UND.undcodsup = {#PARAM1#}) AND (UND.baja = 0 OR UND.baja  IS NULL)) AND UND.cod >= 1"
        End Select
        
        Dim auxDT As DataTable
        Dim auxConn As clsHrcConnClient = Session("conn")
        auxConn = auxConn.gComponent_CreateInstance
        auxQuery = Replace(auxQuery, "{#PARAM1#}", pCod)
        auxDT = auxConn.gConn_Query(auxQuery)
        auxConn.gConn_Close()
        For Each auxRow As DataRow In auxDT.Rows
            Dim auxNode As New TreeNode(If(IsDBNull(auxRow(1)), "Todos", auxRow(1)), Format(auxRow(0)))
            If pParentNode Is Nothing Then
                tvwmodalpopuppnlobjectexplorer.Nodes.Add(auxNode)
            Else
                pParentNode.ChildNodes.Add(auxNode)
            End If
            modalpopuppnlobjectexplorer_Load(CInt(auxRow(0)), auxNode, pObjectType)
        Next

    End Sub
    Private Sub modalpopuppnlobjectexplorer_AddItem(ByVal pContainer As System.Web.UI.HtmlControls.HtmlGenericControl, _
                  ByVal pValue As Integer, _
                  ByVal pobjectType As Integer, _
                  ByVal pText As String, _
                  ByVal pControlID As String, _
                  ByVal pDeleteEnabled As Boolean)
        Dim auxDiv As New System.Web.UI.HtmlControls.HtmlGenericControl("DIV")
        auxDiv.EnableViewState = False
        auxDiv.Attributes.Add("style", "float:left;border:1px solid #d8d8d8;background-color:#d8d8d8;background-image:url(imagenes/boton_fondo.jpg);padding:3px;padding-top:5px;margin-right:3px; margin-bottom:3px;")
        Dim auxLinkButton As New LinkButton
        auxDiv.Controls.Add(auxLinkButton)
        If pDeleteEnabled Then
            Dim auxDeleteButton As New ImageButton
            auxDeleteButton.ImageUrl = "~/imagenes/actdel.png"
            auxDeleteButton.Width = 12
            auxDeleteButton.Attributes.Add("hrc_id", pContainer.Controls.Count)
            auxDeleteButton.Attributes.Add("hrc_grp", pControlID)
            gHierarchyControl_SetValue(pValue, pobjectType, pText, auxLinkButton, Nothing, Nothing, auxDeleteButton, True)
            AddHandler auxDeleteButton.Click, AddressOf modalpopuppnlobjectexplorer_DeleteItem
            auxDiv.Controls.Add(auxDeleteButton)
        Else
            gHierarchyControl_SetValue(pValue, pobjectType, pText, auxLinkButton, Nothing, Nothing, Nothing, False)
        End If
        pContainer.Controls.Add(auxDiv)

    End Sub
    Public Sub modalpopuppnlobjectexplorer_DeleteItem(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim auxString As String = ""
        Dim auxItemToDelete As Short = Val(CType(sender, ImageButton).Attributes("hrc_id"))
        Dim auxPlaceHolder As PlaceHolder = CType(FindControl(CType(sender, ImageButton).Attributes("hrc_grp") & "view"), PlaceHolder)
        Dim auxValue As HiddenField = CType(FindControl(CType(sender, ImageButton).Attributes("hrc_grp")), HiddenField)
        Dim auxItems() As String = Split(auxValue.Value, "{#CHR9#}")
        For auxI As Integer = 0 To UBound(auxItems)
            If auxItems(auxI) <> "" Then
                If auxI <> auxItemToDelete Then
                    auxString &= auxItems(auxI) & "{#CHR9#}"
                Else
                    If auxPlaceHolder.Controls.Count > 0 Then
                        Dim auxMasterDiv As System.Web.UI.HtmlControls.HtmlGenericControl = auxPlaceHolder.Controls(0)
                        auxMasterDiv.Controls.RemoveAt(auxItemToDelete)
                    End If
                End If
            End If
        Next
        auxValue.Value = auxString
        If auxString = "" Then
            auxPlaceHolder.Controls.Clear()
        End If

        If TypeOf (auxPlaceHolder.Parent) Is Control Then
            If TypeOf (auxPlaceHolder.Parent.Parent) Is UpdatePanel Then
                If CType(auxPlaceHolder.Parent.Parent, UpdatePanel).UpdateMode = UpdatePanelUpdateMode.Conditional Then
                    CType(auxPlaceHolder.Parent.Parent, UpdatePanel).Update()
                End If
            End If
        End If
    End Sub    '////
    Public Sub modalpopuppnlobjectexplorer_Select(ByVal pValue As Integer, ByVal pObjectType As Integer, ByVal pText As String)
        'Cambiado
        Dim auxValues As HiddenField = CType(FindControl(ViewState("modalpopuppnlobjectexplorer_value")), HiddenField)
        Dim auxValueType As Object = FindControl(ViewState("modalpopuppnlobjectexplorer_type"))
        If auxValueType IsNot Nothing Then
            Dim auxText As String = ""
            auxText = "<img width=20 height=20 border=0 src='imagenes/icon" & Format(pObjectType, "00000000") & ".png' />" & pText
            Dim auxLinkButton As LinkButton = CType(FindControl(ViewState("modalpopuppnlobjectexplorer_controlid")), LinkButton)
            gHierarchyControl_SetValue(pValue, pObjectType, pText, auxLinkButton, CType(FindControl(ViewState("modalpopuppnlobjectexplorer_value")), HiddenField), CType(FindControl(ViewState("modalpopuppnlobjectexplorer_type")), HiddenField), CType(FindControl(ViewState("modalpopuppnlobjectexplorer_delete")), ImageButton), True)
            If TypeOf (auxLinkButton.Parent) Is Control Then
                If TypeOf (auxLinkButton.Parent.Parent) Is UpdatePanel Then
                    If CType(auxLinkButton.Parent.Parent, UpdatePanel).UpdateMode = UpdatePanelUpdateMode.Conditional Then
                        CType(auxLinkButton.Parent.Parent, UpdatePanel).Update()
                    End If
                End If
            End If
        Else
            auxValues.Value &= pValue & "{#CHR34#}" & pObjectType & "{#CHR34#}" & pText & "{#CHR34#}" & "{#CHR9#}"
            Dim auxPlaceHolder As PlaceHolder = CType(FindControl(ViewState("modalpopuppnlobjectexplorer_controlid")), PlaceHolder)
            If auxPlaceHolder.Controls.Count > 0 Then
                Dim auxMasterDiv As System.Web.UI.HtmlControls.HtmlGenericControl = auxPlaceHolder.Controls(0)
                modalpopuppnlobjectexplorer_AddItem(auxMasterDiv, pValue, pObjectType, pText, auxValues.UniqueID, True)
            End If
        End If

        ViewState("modalpopuppnlobjectexplorer_mode") = Nothing
        ViewState("modalpopuppnlobjectexplorer_type") = Nothing
        ViewState("modalpopuppnlobjectexplorer_controlid") = Nothing
        ViewState("modalpopuppnlobjectexplorer_delete") = Nothing
        ViewState("modalpopuppnlobjectexplorer_value") = Nothing
        pnlobjectexplorer.Hide()

    End Sub
    Public Sub objectexplorer_Show(ByVal pControlIDValue As String, ByVal pControlIDText As String, ByVal pControlIDType As String, ByVal pObjectTvwType As String, ByVal pObjectGrdType As String, ByVal pControlIDDelete As String)
        tvwmodalpopuppnlobjectexplorer.Nodes.Clear()
        Dim auxStartNode As TreeNode = Nothing
        Dim auxObjectTypes As String = ""
        For Each auxString As String In Split(pObjectTvwType, "{#CHR34#}")
            Dim auxObjectID As Integer = Val(auxString)
            auxObjectTypes &= "{#CHR34#}" & auxObjectID & "{#CHR34#}"
            Select Case Val(auxString)
                Case 0
                Case -1
                    auxStartNode = New TreeNode("Todos", -1)
                    tvwmodalpopuppnlobjectexplorer.Nodes.Add(auxStartNode)
                    modalpopuppnlobjectexplorer_Load(-1, auxStartNode, pObjectTvwType)
                Case enumEntities.coEntityUND
                    auxStartNode = New TreeNode("<img width=14 height=14 border=0 src='imagenes/icon00000002.png' />Unidades", "00000002")
                    tvwmodalpopuppnlobjectexplorer.Nodes.Add(auxStartNode)
                    modalpopuppnlobjectexplorer_Load(-1, auxStartNode, auxObjectID)
            End Select
        Next
        grdobjectexplorer.Visible = True
        tvwmodalpopuppnlobjectexplorer.Nodes(0).Select()
        tvwmodalpopuppnlobjectexplorer_SelectedNodeChanged(tvwmodalpopuppnlobjectexplorer, Nothing)
        tvwmodalpopuppnlobjectexplorer.Nodes(0).Expand()
        ViewState("modalpopuppnlobjectexplorer_controlid") = pControlIDText
        ViewState("modalpopuppnlobjectexplorer_value") = pControlIDValue
        ViewState("modalpopuppnlobjectexplorer_type") = pControlIDType
        ViewState("modalpopuppnlobjectexplorer_delete") = pControlIDDelete
        Dim auxObjectGrid As String = ""
        For Each auxString As String In Split(pObjectGrdType, "{#CHR34#}")
            If auxString.Trim <> "" Then
                auxObjectGrid &= "{#CHR34#}" & auxString & "{#CHR34#}"
            End If
        Next
        ViewState("modalpopuppnlobjectexplorer_mode") = auxObjectGrid
        grdobjectexplorer.Visible = True
        cmdobjectexplorerfilter.Visible = True
        txtobjectexplorerfilter.Visible = True
        lblobjectexplorerfilter.Visible = True
        tvwmodalpopuppnlobjectexplorer_SelectedNodeChanged(tvwmodalpopuppnlobjectexplorer, Nothing)
        pnlobjectexplorer.Show()
        updpnlobjectexplorer.Update()

    End Sub
    Public Sub tvwmodalpopuppnlobjectexplorer_SelectedNodeChanged(ByVal sender As Object, ByVal e As System.EventArgs)
        If grdobjectexplorer.Visible = False Then
            modalpopuppnlobjectexplorer_Select(CType(sender, TreeView).SelectedNode.Value, 1, CType(sender, TreeView).SelectedNode.Text)
        Else
            Dim auxQuery As String = ""
            If InStr(ViewState("modalpopuppnlobjectexplorer_mode"), "{#CHR34#}" & enumEntities.coEntityUND & "{#CHR34#}") > 0 Then
                If auxQuery <> "" Then auxQuery &= " UNION ALL "
                auxQuery &= "(SELECT UND.cod AS cod,UND.dsc as dsc," & enumEntities.coEntityUND & " as objecttype FROM UND   WHERE ((((UND.undcodsup =" & CType(sender, TreeView).SelectedNode.Value & " AND '" & txtobjectexplorerfilter.Text.Trim & "'='')  OR  (UND.dsc LIKE '%" & txtobjectexplorerfilter.Text.Trim & "%' AND '" & txtobjectexplorerfilter.Text.Trim & "'<>''))) AND (UND.baja = 0 OR UND.baja  IS NULL)) AND UND.cod >= 1)"
            End If
            If InStr(ViewState("modalpopuppnlobjectexplorer_mode"), "{#CHR34#}" & enumEntities.coEntityDOC_EQU & "{#CHR34#}") > 0 Then
                If auxQuery <> "" Then auxQuery &= " UNION ALL "
                auxQuery &= "(SELECT DOC_EQU.cod as cod,DOC_EQU.dsc as dsc," & enumEntities.coEntityDOC_EQU & " as objecttype FROM DOC_EQU  WHERE ((((DOC_EQU.undcod =" & CType(sender, TreeView).SelectedNode.Value & " AND '" & txtobjectexplorerfilter.Text.Trim & "'='')  OR  (DOC_EQU.dsc LIKE '%" & txtobjectexplorerfilter.Text.Trim & "%' AND '" & txtobjectexplorerfilter.Text.Trim & "'<>''))) ) AND DOC_EQU.cod >= 1 )"
            End If
            If InStr(ViewState("modalpopuppnlobjectexplorer_mode"), "{#CHR34#}" & enumEntities.coEntityEMP & "{#CHR34#}") > 0 Then
                If auxQuery <> "" Then auxQuery &= " UNION ALL "
                auxQuery &= "(SELECT EMP.cod as cod,EMP.dsc as dsc," & enumEntities.coEntityEMP & " as objecttype FROM EMP   WHERE ((((EMP.undcod =" & CType(sender, TreeView).SelectedNode.Value & " AND '" & txtobjectexplorerfilter.Text.Trim & "'='')  OR  (EMP.dsc LIKE '%" & txtobjectexplorerfilter.Text.Trim & "%' AND '" & txtobjectexplorerfilter.Text.Trim & "'<>''))) AND (EMP.baja = 0 OR EMP.baja  IS NULL)) AND EMP.cod >= 1)"
            End If

            auxQuery &= " ORDER BY dsc,objecttype,cod"
            If e IsNot Nothing Then
                'Si es treeview no es nothing
                txtobjectexplorerfilter.Text = ""
            End If
            Dim auxConn As clsHrcConnClient = Session("conn")
            auxConn = auxConn.gComponent_CreateInstance
            grdobjectexplorer.DataSource = auxConn.gConn_Query(auxQuery)
            auxConn.gConn_Close()
            grdobjectexplorer.DataBind()
            updpnlobjectexplorer.Update()
        End If
    End Sub

    Protected Sub grdobjectexplorer_PageIndexChanging(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles grdobjectexplorer.PageIndexChanging
        tvwmodalpopuppnlobjectexplorer_SelectedNodeChanged(tvwmodalpopuppnlobjectexplorer, Nothing)
        grdobjectexplorer.PageIndex = e.NewPageIndex
        grdobjectexplorer.DataBind()
        updpnlobjectexplorer.Update()
    End Sub

    '////////////

End Class
