Imports Intelimedia.imComponentes
Imports System.Data
Imports clsCusimDOC
Partial Class frmsecperm
    Inherits System.Web.UI.Page
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            Dim auxClass As New clsCusimDOC
            Dim auxSidCod As Integer = auxClass.Conn.gField_GetInt(Request.QueryString("param1"))
            If auxSidCod < 1 Then
                If auxClass.Sec.gSID_CheckAccess(enumAccessType.coSYSGlobalCambiarpermisos) = False Then
                    cmdFormViewItemCancel_Click(Nothing, Nothing)
                Else
                    lblsubtitle.Text = "Global"
                    gData_Get()
                End If
            Else
                Dim auxSittypeCod As Integer = auxClass.Conn.gConn_QueryValue("SELECT sidtypecod FROM Q_SECPSID WHERE sidcod =" & auxSidCod)
                Dim auxAccessType As enumAccessType = enumAccessType.coSYSGlobalCambiarpermisos
                Dim auxAccess As Boolean = False
                Select Case auxSittypeCod
                    Case enumEntities.coEntityDOC_DOC
                        auxAccess = auxClass.Sec.gSID_CheckAccess(auxSidCod, enumAccessType.coSYSGlobalCambiarpermisos)
                        'Case enumEntities.coEntityPR
                        '    auxAccess = auxSecurity.gSID_CheckAccess(auxSidCod, enumAccessType.coPROREVRevisionesTotal)
                End Select
                If auxAccess = False Then
                    auxAccess = auxClass.Sec.gSID_CheckAccess(enumAccessType.coSYSGlobalCambiarpermisos)
                End If
                'auxAccess = True
                If auxAccess Then
                    Select Case auxSittypeCod
                        Case enumEntities.coEntityDOC_DOC
                            Dim auxDT As DataTable = auxClass.Conn.gConn_Query("SELECT dsc FROM DOC_DOC WHERE qsidcod =" & auxSidCod)
                            If auxDT.Rows.Count <> 0 Then
                                lblsubtitle.Text = auxClass.Conn.gField_GetString(auxDT.Rows(0)("dsc"))
                            End If
                            'Case enumEntities.coEntityPROREV
                            '    Dim auxDT As DataTable = auxConn.gConn_Query("SELECT fecha FROM PROREV WHERE qsidcod =" & auxSidCod)
                            '    If auxDT.Rows.Count <> 0 Then
                            '        lblsubtitle.Text = auxConn.gField_GetString(auxDT.Rows(0)("fecha"))
                            '    End If
                    End Select
                    gData_Get()
                Else
                    cmdFormViewItemCancel_Click(Nothing, Nothing)
                End If
            End If
        End If
    End Sub

    Protected Sub cmdFormViewItemCancel_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdFormViewItemCancel.Click
        If Request.QueryString("_closea_") = "1" Then
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "CloseWindow", "self.close();", True)
        End If
    End Sub

    Protected Sub grpACLperm_RowCommand(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles grpACLperm.RowCommand
        Select Case e.CommandName
            Case "cDelete"
                Dim auxClass As New clsCusimDOC
                auxClass.Sec.gACL_DelEntry(Val(e.CommandArgument))
                gData_Get()
            Case "cInsert"
                frmupdpanelfrmsecperm.ChangeMode(FormViewMode.Insert)
                lblfrmupdpanelsecprmsubtitle.Text = "Nuevo permiso"
                updpanelfrmsecperm.Show()
                gHierarchyControl_SetValue(-1, -1, "", edtacctypeview, edtacctype, edtacctypetype, edtacctypedelete, True)
                gHierarchyControl_SetValue(-1, -1, "", edtsidcodview, hdnsidcod, hdnsidcodtype, edtsidcoddelete, True)
                updupdpanelfrmsecperm.Update()
        End Select
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
    Protected Sub grdobjectexplorer_PageIndexChanging(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewPageEventArgs)
        tvwmodalpopuppnlobjectexplorer_SelectedNodeChanged(tvwmodalpopuppnlobjectexplorer, Nothing)
        sender.PageIndex = e.NewPageIndex
        sender.DataBind()
        updpnlobjectexplorer.Update()

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
            Case 1
                auxQuery = "SELECT UND.cod,UND.dsc,1 as objecttype FROM UND   WHERE ((UND.undcodsup = {#PARAM1#}) AND (UND.baja = 0 OR UND.baja  IS NULL)) AND UND.cod >= 1"
        End Select
        Dim auxConn As clsHrcConnClient = Session("conn")
        auxConn = auxConn.gComponent_CreateInstance
        auxQuery = Replace(auxQuery, "{#PARAM1#}", pCod)
        Dim auxDT As DataTable = auxConn.gConn_Query(auxQuery)
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
    Public Sub modalpopuppnlobjectexplorer_Select(ByVal pValue As Integer, ByVal pObjectType As Integer, ByVal pText As String)
        Dim auxText As String = ""
        auxText = "<img width=20 height=20 border=0 src='imagenes/icon" & Format(pObjectType, "00000000") & ".png' />" & pText
        Dim auxLinkButton As LinkButton = CType(FindControl(ViewState("modalpopuppnlobjectexplorer_controlid")), LinkButton)
        objectexplorer_SetValue(pValue, pObjectType, pText, auxLinkButton, CType(FindControl(ViewState("modalpopuppnlobjectexplorer_value")), HiddenField), CType(FindControl(ViewState("modalpopuppnlobjectexplorer_type")), HiddenField), CType(FindControl(ViewState("modalpopuppnlobjectexplorer_delete")), ImageButton))
        If TypeOf (auxLinkButton.Parent) Is Control Then
            If TypeOf (auxLinkButton.Parent.Parent) Is UpdatePanel Then
                If CType(auxLinkButton.Parent.Parent, UpdatePanel).UpdateMode = UpdatePanelUpdateMode.Conditional Then
                    CType(auxLinkButton.Parent.Parent, UpdatePanel).Update()
                End If
            End If
        End If
        ViewState("modalpopuppnlobjectexplorer_mode") = Nothing
        pnlobjectexplorer.Hide()

    End Sub
    Public Sub objectexplorer_SetValue(ByVal pValue As Integer, ByVal pObjectType As Integer, ByVal pText As String, ByVal pLinkButton As LinkButton, ByVal pHiddenValue As HiddenField, ByVal pHiddentype As HiddenField, ByVal pDeleteButton As ImageButton)
        pHiddenValue.Value = pValue
        pHiddentype.Value = pObjectType
        If pValue < 1 Then
            pLinkButton.Visible = False
            If pDeleteButton IsNot Nothing Then pDeleteButton.Visible = False
            Exit Sub
        End If
        Dim auxText As String = ""
        Select Case pObjectType
            Case -2
                auxText = "<img width=20 height=20 border=0 src='imagenes/objuser.png' />" & pText
                'pLinkButton.OnClientClick = "javascript:if (window.showModalDialog) {window.showModalDialog(" & Chr(39) & "frmUnidades_det.aspx?_mode_=0&_closea_=1&param1=" & pValue & "&timestamp=" & Chr(39) & " + new Date().getTime() + " & Chr(39) & "" & Chr(39) & ", " & Chr(39) & "" & Chr(39) & ", " & Chr(39) & "menubar=no;resizable=yes;toolbar=no;status=no;center=yes;dialogWidth=750px;dialogHeight=570px;" & Chr(39) & ");} else {window.open(" & Chr(39) & "frmUnidades_det.aspx?_mode_=0&_closea_=1&param1=" & pValue & "&timestamp=" & Chr(39) & " + new Date().getTime() + " & Chr(39) & "" & Chr(39) & ", " & Chr(39) & "" & Chr(39) & ", " & Chr(39) & "modal=yes;directories=no;menubar=no;resizable=yes;toolbar=no;status=no;center=yes;dialogWidth=750px;dialogHeight=570px;" & Chr(39) & ");};return false;"
                pLinkButton.OnClientClick = ""
            Case -3
                auxText = "<img width=20 height=20 border=0 src='imagenes/objgroup.png' />" & pText
                'pLinkButton.OnClientClick = "javascript:if (window.showModalDialog) {window.showModalDialog(" & Chr(39) & "frmColaboradores_det.aspx?_mode_=0&_closea_=1&param1=" & pValue & "&timestamp=" & Chr(39) & " + new Date().getTime() + " & Chr(39) & "" & Chr(39) & ", " & Chr(39) & "" & Chr(39) & ", " & Chr(39) & "menubar=no;resizable=yes;toolbar=no;status=no;center=yes;dialogWidth=750px;dialogHeight=570px;" & Chr(39) & ");} else {window.open(" & Chr(39) & "frmColaboradores_det.aspx?_mode_=0&_closea_=1&param1=" & pValue & "&timestamp=" & Chr(39) & " + new Date().getTime() + " & Chr(39) & "" & Chr(39) & ", " & Chr(39) & "" & Chr(39) & ", " & Chr(39) & "modal=yes;directories=no;menubar=no;resizable=yes;toolbar=no;status=no;center=yes;dialogWidth=750px;dialogHeight=570px;" & Chr(39) & ");};return false;"
                pLinkButton.OnClientClick = ""
            Case -6
                auxText = "<img width=20 height=20 border=0 src='imagenes/objacctype.png' />" & pText
                'pLinkButton.OnClientClick = "javascript:if (window.showModalDialog) {window.showModalDialog(" & Chr(39) & "frmColaboradores_det.aspx?_mode_=0&_closea_=1&param1=" & pValue & "&timestamp=" & Chr(39) & " + new Date().getTime() + " & Chr(39) & "" & Chr(39) & ", " & Chr(39) & "" & Chr(39) & ", " & Chr(39) & "menubar=no;resizable=yes;toolbar=no;status=no;center=yes;dialogWidth=750px;dialogHeight=570px;" & Chr(39) & ");} else {window.open(" & Chr(39) & "frmColaboradores_det.aspx?_mode_=0&_closea_=1&param1=" & pValue & "&timestamp=" & Chr(39) & " + new Date().getTime() + " & Chr(39) & "" & Chr(39) & ", " & Chr(39) & "" & Chr(39) & ", " & Chr(39) & "modal=yes;directories=no;menubar=no;resizable=yes;toolbar=no;status=no;center=yes;dialogWidth=750px;dialogHeight=570px;" & Chr(39) & ");};return false;"
                pLinkButton.OnClientClick = ""
        End Select
        pLinkButton.Text = auxText
        pLinkButton.Visible = True
        If pDeleteButton IsNot Nothing Then pDeleteButton.Visible = True

    End Sub
    Public Sub objectexplorer_Show(ByVal pControlIDValue As String, ByVal pControlIDText As String, ByVal pControlIDType As String, ByVal pObjectTvwType As String, ByVal pObjectGrdType As String, ByVal pControlIDDelete As String)
        tvwmodalpopuppnlobjectexplorer.Nodes.Clear()
        Dim auxStartNode As TreeNode = Nothing
        Dim auxObjectTypes As String = ""
        For Each auxString As String In Split(pObjectTvwType, "{#CHR34#}")
            Select Case Val(auxString.Trim)
                Case -1
                    auxStartNode = New TreeNode("Todos", -1)
            End Select
        Next
        tvwmodalpopuppnlobjectexplorer.Nodes.Add(auxStartNode)
        For Each auxString As String In Split(pObjectGrdType, "{#CHR34#}")
            auxString = auxString.Trim()
            If auxString.Trim <> "" Then
                auxObjectTypes &= "{#CHR34#}" & auxString & "{#CHR34#}"
                Select Case Val(auxString.Trim)
                    Case -2
                        Dim auxStartSubNode As New TreeNode("<img width=20 height=20 border=0 src='imagenes/objuser.png' />Usuarios", -2)
                        tvwmodalpopuppnlobjectexplorer.Nodes.Add(auxStartSubNode)
                    Case -3
                        Dim auxStartSubNode As New TreeNode("<img width=20 height=20 border=0 src='imagenes/objgroup.png' />Grupos", -3)
                        tvwmodalpopuppnlobjectexplorer.Nodes.Add(auxStartSubNode)
                    Case -6
                        Dim auxStartSubNode As New TreeNode("<img width=20 height=20 border=0 src='imagenes/objacctype.png' />Permisos", -6)
                        tvwmodalpopuppnlobjectexplorer.Nodes.Add(auxStartSubNode)
                End Select
            End If
        Next

        'modalpopuppnlobjectexplorer_Load(-1, auxStartNode, pObjectTvwType)
        grdobjectexplorer.Visible = True
        tvwmodalpopuppnlobjectexplorer.Nodes(0).Select()
        tvwmodalpopuppnlobjectexplorer_SelectedNodeChanged(tvwmodalpopuppnlobjectexplorer, Nothing)
        tvwmodalpopuppnlobjectexplorer.Nodes(0).Expand()
        ViewState("modalpopuppnlobjectexplorer_controlid") = pControlIDText
        ViewState("modalpopuppnlobjectexplorer_value") = pControlIDValue
        ViewState("modalpopuppnlobjectexplorer_type") = pControlIDType
        ViewState("modalpopuppnlobjectexplorer_delete") = pControlIDDelete


        ViewState("modalpopuppnlobjectexplorer_mode") = auxObjectTypes
        grdobjectexplorer.Visible = True
        cmdobjectexplorerfilter.Visible = True
        txtobjectexplorerfilter.Visible = True
        lblobjectexplorerfilter.Visible = True
        pnlobjectexplorer.Show()
        updpnlobjectexplorer.Update()

    End Sub
    Public Sub tvwmodalpopuppnlobjectexplorer_SelectedNodeChanged(ByVal sender As Object, ByVal e As System.EventArgs)
        If grdobjectexplorer.Visible = False Then
            modalpopuppnlobjectexplorer_Select(CType(sender, TreeView).SelectedNode.Value, 1, CType(sender, TreeView).SelectedNode.Text)
        Else

            Dim auxQuery As String = ""
            If InStr(ViewState("modalpopuppnlobjectexplorer_mode"), "{#CHR34#}-2{#CHR34#}") > 0 Then
                If auxQuery <> "" Then auxQuery &= " UNION ALL "
                auxQuery &= "(SELECT seccod as cod,secdsc as dsc,-2 as objecttype,'Usuarios' as objecttypedsc FROM Q_SECPLOGIN WHERE ((((" & CType(sender, TreeView).SelectedNode.Value & " IN(-2,-1) AND '" & txtobjectexplorerfilter.Text.Trim & "'='')  OR  (Q_SECPLOGIN.secdsc LIKE '%" & txtobjectexplorerfilter.Text.Trim & "%' AND '" & txtobjectexplorerfilter.Text.Trim & "'<>''))) AND (Q_SECPLOGIN.secdisabled = 0 OR Q_SECPLOGIN.secdisabled  IS NULL)) AND Q_SECPLOGIN.seccod >= 1)"
            End If
            If InStr(ViewState("modalpopuppnlobjectexplorer_mode"), "{#CHR34#}-3{#CHR34#}") > 0 Then
                If auxQuery <> "" Then auxQuery &= " UNION ALL "
                auxQuery &= "(SELECT grpcod as cod,grpdsc as dsc,-3 as objecttype,'Grupos' as objecttypedsc FROM Q_SECPGRP WHERE ((((" & CType(sender, TreeView).SelectedNode.Value & " IN(-3,-1) AND '" & txtobjectexplorerfilter.Text.Trim & "'='')  OR  (Q_SECPGRP.grpdsc LIKE '%" & txtobjectexplorerfilter.Text.Trim & "%' AND '" & txtobjectexplorerfilter.Text.Trim & "'<>''))) AND (Q_SECPGRP.grpdisabled = 0 OR Q_SECPGRP.grpdisabled IS NULL)) AND Q_SECPGRP.grpcod >= 1)"
            End If
            If InStr(ViewState("modalpopuppnlobjectexplorer_mode"), "{#CHR34#}-6{#CHR34#}") > 0 Then
                If auxQuery <> "" Then auxQuery &= " UNION ALL "
                auxQuery &= "(SELECT acctypecod as cod,acctypedsc as dsc,-6 as objecttype,'Permisos' as objecttypedsc FROM Q_SECACCTYPE   WHERE ((((" & CType(sender, TreeView).SelectedNode.Value & " IN(-6,-1) AND '" & txtobjectexplorerfilter.Text.Trim & "'='')  OR  (Q_SECACCTYPE.acctypedsc LIKE '%" & txtobjectexplorerfilter.Text.Trim & "%' AND '" & txtobjectexplorerfilter.Text.Trim & "'<>''))) ) AND Q_SECACCTYPE.acctypecod >= 1)"
            End If
            auxQuery &= " ORDER BY dsc,objecttype,cod"
            txtobjectexplorerfilter.Text = ""
            Dim auxConn As clsHrcConnClient = Session("conn")
            auxConn = auxConn.gComponent_CreateInstance
            grdobjectexplorer.DataSource = auxConn.gConn_Query(auxQuery)
            auxConn.gConn_Close()
            grdobjectexplorer.DataBind()
            updpnlobjectexplorer.Update()
        End If

    End Sub

    '////////////

    Protected Sub frmupdpanelfrmsecperm_ItemInserted(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.FormViewInsertedEventArgs) Handles frmupdpanelfrmsecperm.ItemInserted

    End Sub


    Protected Sub cmdfrmupdpanelsecprmupdcancel_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        updpanelfrmsecperm.Hide()
    End Sub

    Protected Sub cmdedtacctypeshowpanel_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        objectexplorer_Show(edtacctype.UniqueID, edtacctypeview.UniqueID, edtacctypetype.UniqueID, "-1", "{#CHR34#}-6{#CHR34#}", edtacctypedelete.UniqueID)
    End Sub

    Protected Sub edtsidcoddelete_DataBinding(ByVal sender As Object, ByVal e As System.EventArgs)

    End Sub

    Protected Sub edtsidcoddelete_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs)
        gHierarchyControl_SetValue(-1, -1, "", edtsidcodview, hdnsidcod, hdnsidcodtype, edtsidcoddelete, True)
    End Sub

    Protected Sub edtsidcodview_DataBinding(ByVal sender As Object, ByVal e As System.EventArgs)

    End Sub

    Protected Sub edtacctypeview_DataBinding(ByVal sender As Object, ByVal e As System.EventArgs)

    End Sub

    Protected Sub edtacctypedelete_DataBinding(ByVal sender As Object, ByVal e As System.EventArgs)

    End Sub

    Protected Sub edtacctypedelete_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs)
        gHierarchyControl_SetValue(-1, -1, "", edtacctypeview, edtacctype, edtacctypetype, edtacctypedelete, True)
    End Sub

    Protected Sub cmdedtsidcodshowpanel_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        objectexplorer_Show(hdnsidcod.UniqueID, edtsidcodview.UniqueID, hdnsidcodtype.UniqueID, "{#CHR34#}-1{#CHR34#}", "{#CHR34#}-2{#CHR34#}{#CHR34#}-3{#CHR34#}", edtsidcoddelete.UniqueID)
    End Sub

    Protected Sub cmdfrmupdpanelsecprmupdconfirm_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        updpanelfrmsecperm.Hide()
        Dim auxClass As New clsCusimDOC
        Dim auxSidCod As Integer = auxClass.Conn.gField_GetInt(Request.QueryString("param1"))
        Dim auxAccTypeCod As Integer = auxClass.Conn.gField_GetInt(edtacctype.Value)
        Select Case auxClass.Conn.gField_GetInt(hdnsidcodtype.Value)
            Case -2
                auxClass.Sec.gACL_AddLogin(auxClass.Sec.gSID_GetACL(auxSidCod), auxClass.Conn.gField_GetInt(hdnsidcod.Value), auxAccTypeCod)
            Case -3
                auxClass.Sec.gACL_AddGroup(auxClass.Sec.gSID_GetACL(auxSidCod), auxClass.Conn.gField_GetInt(hdnsidcod.Value), auxAccTypeCod)
        End Select
        auxClass.Conn.gConn_Close()
        Page.DataBind()
        gData_Get()
    End Sub
    Protected Sub cmdSidReorganize_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim auxconn As clsHrcConnClient = Session("conn")
        Dim auxSidCod As Integer = auxconn.gField_GetInt(Request.QueryString("param1"))
        Dim auxSecurity As clsHrcSecurityClient = Session("security")
        auxSecurity.gSID_Reorganize(auxSidCod)
        gData_Get()

    End Sub
    Private Sub gData_Get()
        Dim auxClass As New clsCusimDOC
        Dim auxSidCod As Integer = auxClass.Conn.gField_GetInt(Request.QueryString("param1"))
        If auxSidCod < 1 Then
            grdProPerm.DataSource = auxClass.Sec.gSID_ResolveLogins(True)
            grdProPerm.DataBind()
            grpACLperm.DataSource = auxClass.Sec.gSID_GetGlobalEntries
            grpACLperm.DataBind()
        Else
            grdProPerm.DataSource = auxClass.Sec.gSID_ResolveLogins(auxSidCod, True)
            grdProPerm.DataBind()
            grpACLperm.DataSource = auxClass.Sec.gSID_GetEntries(auxSidCod)
            grpACLperm.DataBind()
        End If
        If grdProPerm.DataSource.Rows.Count = 0 Then
            Dim auxRow As DataRow = grdProPerm.DataSource.NewRow
            'auxRow("sidtypecod") = -1
            grdProPerm.DataSource.Rows.Add(auxRow)
        End If
        If grpACLperm.DataSource.Rows.Count = 0 Then
            Dim auxRow As DataRow = grpACLperm.DataSource.NewRow
            'auxRow("sidtypecod") = -1
            grpACLperm.DataSource.Rows.Add(auxRow)
        End If
        auxClass.Conn.gConn_Close()
    End Sub
End Class



