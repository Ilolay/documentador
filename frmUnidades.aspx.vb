Imports System.Data
Imports System.Data.SqlClient
Imports Intelimedia.imComponentes
Public Class frmUnidades
Inherits System.Web.UI.Page
Friend m_PermFormDelete as Boolean=False
Friend m_PermFormEdit as Boolean=False
Friend m_PermFormNew as Boolean=False
Protected Sub ctrl000029delete_Click (ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs)
ctrl000029type.Value=-1
ctrl000029.Value=-1
ctrl000029view.Visible=False
ctrl000029delete.Visible=False

End Sub
Protected Sub ctrl000029delete_DataBinding(ByVal sender As Object, ByVal e As System.EventArgs)
If Not IsPostBack  Then 
 If IsDBNull(Eval("undcodsup")) Then 
    CType(sender,Control).Visible=False
  ElseIf Eval("undcodsup") < 1 Then
    CType(sender,Control).Visible=False
  Else
    CType(sender,Control).Visible=True
 End If
End If

End Sub
Protected Sub cmdctrl000029showpanel_Click (ByVal sender As Object, ByVal e As System.EventArgs)
objexplorer_Show(CType(sender.parent.FindControl("ctrl000029"),HiddenField).UniqueID,CType(sender.parent.FindControl("ctrl000029view"),LinkButton).UniqueID,CType(sender.parent.FindControl("ctrl000029type"),HiddenField).UniqueID,"10","10",CType(sender.parent.FindControl("ctrl000029delete"),ImageButton).UniqueID)
End Sub
Protected Sub ctrl000029_Load(ByVal sender As Object, ByVal e As System.EventArgs)
If Not IsPostBack Then CType(sender,HiddenField).Value=-1

End Sub

Protected Sub dsctrl000029_Init(ByVal sender As Object,ByVal e As System.EventArgs)
sender.ConnectionString=Session("connectionstringname")
End Sub
Protected Sub dsctrl000029_Deleted(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.SqlDataSourceStatusEventArgs)
If e.Exception IsNot Nothing Then
  lblerror.Text &="//Error borrando datos (" & e.Exception.Message & ")"
    
Else
    
End If

End Sub
Protected Sub dsctrl000029_Inserted(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.SqlDataSourceStatusEventArgs)
If e.Exception IsNot Nothing Then
  lblerror.Text &="//Error insertando datos (" & e.Exception.Message & ")"
    
Else
    
End If

End Sub
Protected Sub dsctrl000029_Selected(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.SqlDataSourceStatusEventArgs)
If e.Exception IsNot Nothing Then
  lblerror.Text &="//Error buscando datos (" & e.Exception.Message & ")"
    
Else
    
End If

End Sub
Protected Sub dsctrl000029_Updated(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.SqlDataSourceStatusEventArgs)
If e.Exception IsNot Nothing Then
  lblerror.Text &="//Error actualizando datos (" & e.Exception.Message & ")"
    
Else
    
End If

End Sub

Protected Sub ctrl000030delete_Click (ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs)
ctrl000030type.Value=-1
ctrl000030.Value=-1
ctrl000030view.Visible=False
ctrl000030delete.Visible=False

End Sub
Protected Sub ctrl000030delete_DataBinding(ByVal sender As Object, ByVal e As System.EventArgs)
If Not IsPostBack  Then 
 If IsDBNull(Eval("editor")) Then 
    CType(sender,Control).Visible=False
  ElseIf Eval("editor") < 1 Then
    CType(sender,Control).Visible=False
  Else
    CType(sender,Control).Visible=True
 End If
End If

End Sub
Protected Sub cmdctrl000030showpanel_Click (ByVal sender As Object, ByVal e As System.EventArgs)
objexplorer_Show(CType(sender.parent.FindControl("ctrl000030"),HiddenField).UniqueID,CType(sender.parent.FindControl("ctrl000030view"),LinkButton).UniqueID,CType(sender.parent.FindControl("ctrl000030type"),HiddenField).UniqueID,"10","9",CType(sender.parent.FindControl("ctrl000030delete"),ImageButton).UniqueID)
End Sub
Protected Sub ctrl000030_Load(ByVal sender As Object, ByVal e As System.EventArgs)
If Not IsPostBack Then CType(sender,HiddenField).Value=-1

End Sub

Protected Sub dsctrl000030_Init(ByVal sender As Object,ByVal e As System.EventArgs)
sender.ConnectionString=Session("connectionstringname")
End Sub
Protected Sub dsctrl000030_Deleted(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.SqlDataSourceStatusEventArgs)
If e.Exception IsNot Nothing Then
  lblerror.Text &="//Error borrando datos (" & e.Exception.Message & ")"
    
Else
    
End If

End Sub
Protected Sub dsctrl000030_Inserted(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.SqlDataSourceStatusEventArgs)
If e.Exception IsNot Nothing Then
  lblerror.Text &="//Error insertando datos (" & e.Exception.Message & ")"
    
Else
    
End If

End Sub
Protected Sub dsctrl000030_Selected(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.SqlDataSourceStatusEventArgs)
If e.Exception IsNot Nothing Then
  lblerror.Text &="//Error buscando datos (" & e.Exception.Message & ")"
    
Else
    
End If

End Sub
Protected Sub dsctrl000030_Updated(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.SqlDataSourceStatusEventArgs)
If e.Exception IsNot Nothing Then
  lblerror.Text &="//Error actualizando datos (" & e.Exception.Message & ")"
    
Else
    
End If

End Sub

Protected Sub ctrl000026_Click (ByVal sender As Object, ByVal e As System.EventArgs)
ctrl000032.DataBind()

End Sub

Protected Sub ctrl000025_Click (ByVal sender As Object, ByVal e As System.EventArgs)
             CType(sender.parent.FindControl("ctrl000028"),TextBox).Text=""
          CType(sender.parent.FindControl("ctrl000029"),HiddenField).Value="-1"
          CType(sender.parent.FindControl("ctrl000029view"),LinkButton).Text=""
          CType(sender.parent.FindControl("ctrl000029delete"),ImageButton).Visible=False
          CType(sender.parent.FindControl("ctrl000030"),HiddenField).Value="-1"
          CType(sender.parent.FindControl("ctrl000030view"),LinkButton).Text=""
          CType(sender.parent.FindControl("ctrl000030delete"),ImageButton).Visible=False

End Sub

Protected Sub ctrl000024_Load(ByVal sender As Object, ByVal e As System.EventArgs)
sender.Visible=m_PermFormNew

End Sub

Protected Sub ctrl000032colitem1_Databound(ByVal sender As Object, ByVal e As System.EventArgs)
CType(sender,Label).Text=Eval("orden").ToString

End Sub

Protected Sub ctrl000032colitem2view_DataBinding(ByVal sender As Object, ByVal e As System.EventArgs)
If IsDBNull(Eval("cod")) Then 
     Ctype(sender,Control).Visible=False
Else
  If Eval("cod") = -1 Then 
     Ctype(sender,Control).Visible=False
  End If
End If

End Sub

Protected Sub ctrl000032colitem3view_DataBinding(ByVal sender As Object, ByVal e As System.EventArgs)
Dim auxValue as Int32=-1
If IsDBNull(Eval("resp")) Then 
     Ctype(sender,Control).Visible=False
Else
  If Eval("resp") < 1 Then 
     Ctype(sender,Control).Visible=False
  End If
End If

End Sub
Protected Sub ctrl000032colitem3_Databound(ByVal sender As Object, ByVal e As System.EventArgs)
If IsDBNull(Eval("resp")) Then
 CType(sender,HiddenField).Value=""
Else
 CType(sender,HiddenField).Value=Eval("resp").ToString()
End If

End Sub

Protected Sub ctrl000032colitem4view_DataBinding(ByVal sender As Object, ByVal e As System.EventArgs)
Dim auxValue as Int32=-1
If IsDBNull(Eval("undcodsup")) Then 
     Ctype(sender,Control).Visible=False
Else
  If Eval("undcodsup") < 1 Then 
     Ctype(sender,Control).Visible=False
  End If
End If

End Sub
Protected Sub ctrl000032colitem4_Databound(ByVal sender As Object, ByVal e As System.EventArgs)
If IsDBNull(Eval("undcodsup")) Then
 CType(sender,HiddenField).Value=""
Else
 CType(sender,HiddenField).Value=Eval("undcodsup").ToString()
End If

End Sub

Protected Sub ctrl000032colitem5_Databound(ByVal sender As Object, ByVal e As System.EventArgs)
CType(sender,Label).Text=Replace(Eval("undnro").ToString,Chr(10),"<br />")

End Sub

Protected Sub ctrl000032colitem6_Databound(ByVal sender As Object, ByVal e As System.EventArgs)
CType(sender,Label).Text=Replace(Eval("formatoespecifico").ToString,Chr(10),"<br />")

End Sub

Protected Sub ctrl000032colitem7view_DataBinding(ByVal sender As Object, ByVal e As System.EventArgs)
Dim auxValue as Int32=-1
If IsDBNull(Eval("editor")) Then 
     Ctype(sender,Control).Visible=False
Else
  If Eval("editor") < 1 Then 
     Ctype(sender,Control).Visible=False
  End If
End If

End Sub
Protected Sub ctrl000032colitem7_Databound(ByVal sender As Object, ByVal e As System.EventArgs)
If IsDBNull(Eval("editor")) Then
 CType(sender,HiddenField).Value=""
Else
 CType(sender,HiddenField).Value=Eval("editor").ToString()
End If

End Sub

Protected Sub cmdItemUp_Click (ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs)
Dim auxClass as new clscusimDOC
auxClass.gEntity_UND_ItemChangeOrder(pCod:=sender.CommandArgument,pToUp:=True)
Dim auxPageIndex as Integer=ctrl000032.PageIndex
ctrl000032.DataBind
ctrl000032.PageIndex = auxPageIndex
If CType(ctrl000032.Parent.Parent, UpdatePanel).UpdateMode = UpdatePanelUpdateMode.Conditional Then
  CType(ctrl000032.Parent.Parent, UpdatePanel).Update()
End If

End Sub
Protected Sub cmdItemUp_DataBinding(ByVal sender As Object, ByVal e As System.EventArgs)
Ctype(sender,Control).Visible = Not IsDBNull(Eval("orden"))
End Sub

Protected Sub cmdItemDown_Click (ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs)
Dim auxClass as new clscusimDOC
auxClass.gEntity_UND_ItemChangeOrder(pCod:=sender.CommandArgument,pToUp:=False)
Dim auxPageIndex as Integer=ctrl000032.PageIndex
ctrl000032.DataBind
ctrl000032.PageIndex = auxPageIndex
If CType(ctrl000032.Parent.Parent, UpdatePanel).UpdateMode = UpdatePanelUpdateMode.Conditional Then
  CType(ctrl000032.Parent.Parent, UpdatePanel).Update()
End If

End Sub
Protected Sub cmdItemDown_DataBinding(ByVal sender As Object, ByVal e As System.EventArgs)
Ctype(sender,Control).Visible = Not IsDBNull(Eval("orden"))
End Sub

Protected Sub cmdctrl000032edit_DataBinding(ByVal sender As Object, ByVal e As System.EventArgs)
sender.Visible=m_PermFormedit
End Sub

Protected Sub cmdctrl000032delete_DataBinding(ByVal sender As Object, ByVal e As System.EventArgs)
sender.Visible=m_PermFormDelete

End Sub

Protected Sub cmdctrl000032copy_DataBinding(ByVal sender As Object, ByVal e As System.EventArgs)
sender.Visible=m_PermFormNew

End Sub

Protected Sub ctrl000032_ItemDeleted(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewCommandEventArgs)

End Sub
Protected Sub ctrl000032_ItemInserted(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewCommandEventArgs)

End Sub
Protected Sub ctrl000032_ItemSelected(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewCommandEventArgs)

End Sub
Protected Sub ctrl000032_ItemUpdated(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewCommandEventArgs)

End Sub
Protected Sub ctrl000032_RowCommand(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewCommandEventArgs)
Select Case e.CommandName.ToUpper()
End Select

End Sub
Protected Sub ctrl000032_RowCreated(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs)
Select Case e.Row.RowType
  Case DataControlRowType.DataRow
      Select Case e.Row.RowState
      Case DataControlRowState.Alternate
           e.Row.Attributes.Add("onmouseover", "this.className='tabla-fila-alternativa-over';")
           e.Row.Attributes.Add("onmouseout", "this.className='tabla-fila-alternativa';")
      Case Else
           e.Row.Attributes.Add("onmouseover", "this.className='tabla-fila-over';")
           e.Row.Attributes.Add("onmouseout", "this.className='tabla-fila';")
      End Select
End Select

End Sub

Protected Sub dsctrl000032_Init(ByVal sender As Object,ByVal e As System.EventArgs)
sender.ConnectionString=Session("connectionstringname")
End Sub
Protected Sub dsctrl000032_Deleted(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.SqlDataSourceStatusEventArgs)
If e.Exception IsNot Nothing Then
  lblerror.Text &="//Error borrando datos (" & e.Exception.Message & ")"
    
Else
    
End If

End Sub
Protected Sub dsctrl000032_Inserted(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.SqlDataSourceStatusEventArgs)
If e.Exception IsNot Nothing Then
  lblerror.Text &="//Error insertando datos (" & e.Exception.Message & ")"
    
Else
    
End If

End Sub
Protected Sub dsctrl000032_Selected(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.SqlDataSourceStatusEventArgs)
If e.Exception IsNot Nothing Then
  lblerror.Text &="//Error buscando datos (" & e.Exception.Message & ")"
    
Else
    
End If

End Sub
Protected Sub dsctrl000032_Selecting(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.SqlDataSourceSelectingEventArgs)
If Session("isadmin") Then
   e.Command.CommandText = e.Command.CommandText.Replace("@qsidcod_list"," 1=1")
ElseIf CType(Session("security"),clsHrcSecurityClient).gSID_CheckAccess(4) Then
   e.Command.CommandText = e.Command.CommandText.Replace("@qsidcod_list"," 1=1")
Else
   e.Command.CommandText = e.Command.CommandText.Replace("@qsidcod_list"," 1=0")
End If

End Sub
Protected Sub dsctrl000032_Updated(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.SqlDataSourceStatusEventArgs)
If e.Exception IsNot Nothing Then
  lblerror.Text &="//Error actualizando datos (" & e.Exception.Message & ")"
    
Else
    
End If

End Sub

Protected Sub cmdobjexplorerfilter_Click (ByVal sender As Object, ByVal e As System.EventArgs)
tvwmodalpopuppnlobjexplorer_SelectedNodeChanged(tvwmodalpopuppnlobjexplorer,Nothing)
End Sub
Protected Sub grdobjexplorer_ItemDeleted(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewCommandEventArgs)

End Sub
Protected Sub grdobjexplorer_ItemInserted(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewCommandEventArgs)

End Sub
Protected Sub grdobjexplorer_ItemSelected(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewCommandEventArgs)

End Sub
Protected Sub grdobjexplorer_ItemUpdated(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewCommandEventArgs)

End Sub
Protected Sub grdobjexplorer_PageIndexChanging(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewPageEventArgs)
tvwmodalpopuppnlobjexplorer_SelectedNodeChanged(tvwmodalpopuppnlobjexplorer,Nothing)
sender.PageIndex = e.NewPageIndex
sender.DataBind()

End Sub
Protected Sub grdobjexplorer_RowCommand(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewCommandEventArgs)
Select Case e.CommandName.ToUpper()
  Case "UPDATE"
      grdobjexplorer_ItemUpdated(sender,e)
  Case "HRC_INSERT"
      grdobjexplorer_ItemInserted(sender,e)
End Select

End Sub
Protected Sub grdobjexplorer_RowCreated(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs)
Select Case e.Row.RowType
  Case DataControlRowType.DataRow
      Select Case e.Row.RowState
      Case DataControlRowState.Alternate
           e.Row.Attributes.Add("onmouseover", "this.className='tabla-fila-alternativa-over';")
           e.Row.Attributes.Add("onmouseout", "this.className='tabla-fila-alternativa';")
      Case Else
           e.Row.Attributes.Add("onmouseover", "this.className='tabla-fila-over';")
           e.Row.Attributes.Add("onmouseout", "this.className='tabla-fila';")
      End Select
End Select

End Sub
Protected Sub grdobjexplorer_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs)
modalpopuppnlobjexplorer_Select(grdobjexplorer.SelectedDataKey(0), grdobjexplorer.SelectedDataKey(1), CType(grdobjexplorer.SelectedRow.Cells(2).Controls(1),LinkButton).Text)
End Sub
Protected Sub cmdobjexplorercancel_Click (ByVal sender As Object, ByVal e As System.EventArgs)
modalpopuppnlobjexplorer_Cancel()
End Sub
Protected Sub cmdobjexplorerselect_Click (ByVal sender As Object, ByVal e As System.EventArgs)
modalpopuppnlobjexplorer_Select(grdobjexplorer.SelectedDataKey(0), grdobjexplorer.SelectedDataKey(1), grdobjexplorer.SelectedRow.Cells(2).Text)
End Sub
Public Sub modalpopuppnlobjexplorer_Cancel
ViewState("modalpopuppnlobjexplorer_mode") = Nothing
pnlobjexplorer.Hide()

End Sub
Public Sub modalpopuppnlobjexplorer_Load(ByVal pCod As Integer, ByVal pParentNode As TreeNode, ByVal pObjectType As Integer)
Dim auxQuery As String = ""
Select Case pObjectType
Case 10
    auxQuery = "SELECT UND.cod,UND.dsc,10 as objecttype FROM UND   WHERE ((UND.undcodsup = {#PARAM1#}) AND (UND.baja = 0 OR UND.baja  IS NULL)) AND UND.cod >= 1"
End Select
auxQuery = Replace(auxQuery,"{#PARAM1#}",pCod)
Dim auxConn As clsHrcConnClient = Session("conn")
 auxConn = auxConn.gComponent_CreateInstance
auxConn.gConn_Open
Dim auxDT As DataTable = auxConn.gConn_Query(auxQuery )
auxConn.gConn_Close
For Each auxRow As DataRow In auxDT.Rows
  Dim auxNode As New TreeNode(If(IsDBNull(auxRow(1)),"Todos",auxRow(1)), auxRow(0).ToString)
  If pParentNode Is Nothing Then
      tvwmodalpopuppnlobjexplorer.Nodes.Add(auxNode)
  Else
      pParentNode.ChildNodes.Add(auxNode)
  End If
      If CInt(auxRow(0)) <> pCod Then
          modalpopuppnlobjexplorer_Load(CInt(auxRow(0)), auxNode,pObjectType)
      End If
Next

End Sub
Public Sub modalpopuppnlobjexplorer_Select(ByVal pValue As Integer,ByVal pObjectType As Integer,ByVal pText As String)
    Dim auxText As String = ""
    auxText = "<img width=14 height=14 border=0 src='imagenes/icon" & Format(pObjectType, "00000000") & ".png' />" & pText
    Dim auxLinkButton as LinkButton=CType(FindControl(ViewState("modalpopuppnlobjexplorer_controlid")),LinkButton)
    objexplorer_SetValue(pValue,pObjectType,pText,auxLinkButton,CType(FindControl(ViewState("modalpopuppnlobjexplorer_value")), HiddenField),CType(FindControl(ViewState("modalpopuppnlobjexplorer_type")), HiddenField),CType(FindControl(ViewState("modalpopuppnlobjexplorer_delete")), ImageButton))
If TypeOf (auxLinkButton.Parent) Is Control Then
    If TypeOf (auxLinkButton.Parent.Parent) Is UpdatePanel Then
        If CType(auxLinkButton.Parent.Parent, UpdatePanel).UpdateMode = UpdatePanelUpdateMode.Conditional Then
            CType(auxLinkButton.Parent.Parent, UpdatePanel).Update()
        End If
    End If
End If
ViewState("modalpopuppnlobjexplorer_mode") = Nothing
ViewState("modalpopuppnlobjexplorer_type") = Nothing
ViewState("modalpopuppnlobjexplorer_controlid") = Nothing
ViewState("modalpopuppnlobjexplorer_delete") = Nothing
ViewState("modalpopuppnlobjexplorer_value") = Nothing
pnlobjexplorer.Hide()

End Sub
Public Sub objexplorer_SetValue(ByVal pValue As Integer,ByVal pObjectType As Integer,ByVal pText As String, ByVal pLinkButton As LinkButton, ByVal pHiddenValue As HiddenField, ByVal pHiddentype As HiddenField, ByVal pDeleteButton As ImageButton)
If pHiddenValue IsNot Nothing Then pHiddenValue.Value = pValue
If pHiddentype IsNot Nothing Then pHiddentype.Value = pObjectType
If pValue < 1 Then
   pLinkButton.Visible = False
   If pDeleteButton IsNot Nothing Then pDeleteButton.Visible = False
   Exit Sub
End If
Dim auxText As String = ""
auxText = "<img width=14 height=14 border=0 src='imagenes/icon" & Format(pObjectType, "00000000") & ".png' />" & pText
pLinkButton.Text = auxText
Select Case pObjectType
    Case 9
        pLinkButton.OnClientClick = "javascript:if (window.showModalDialog) {window.showModalDialog(" & chr(39) & "frmColaboradores_det.aspx?_mode_=0&_closea_=1&isDlg=1&param1=" & pValue & "&timestamp=" & chr(39) & " + new Date().getTime() + " & chr(39) & "" & chr(39) & ", " & chr(39) & "" & chr(39) & ", " & chr(39) & "menubar=no;resizable=yes;toolbar=no;status=no;center=yes;dialogWidth=750px;dialogHeight=570px;" & chr(39) & ");} else {window.open(" & chr(39) & "frmColaboradores_det.aspx?_mode_=0&_closea_=1&isDlg=1&param1=" & pValue & "&timestamp=" & chr(39) & " + new Date().getTime() + " & chr(39) & "" & chr(39) & ", " & chr(39) & "" & chr(39) & ", " & chr(39) & "modal=yes;directories=no;menubar=no;resizable=yes;toolbar=no;status=no;center=yes;dialogWidth=750px;dialogHeight=570px;" & chr(39) & ");};return false;"
    Case 10
        pLinkButton.OnClientClick = "javascript:if (window.showModalDialog) {window.showModalDialog(" & chr(39) & "frmUnidades_det.aspx?_mode_=0&_closea_=1&isDlg=1&param1=" & pValue & "&timestamp=" & chr(39) & " + new Date().getTime() + " & chr(39) & "" & chr(39) & ", " & chr(39) & "" & chr(39) & ", " & chr(39) & "menubar=no;resizable=yes;toolbar=no;status=no;center=yes;dialogWidth=750px;dialogHeight=570px;" & chr(39) & ");} else {window.open(" & chr(39) & "frmUnidades_det.aspx?_mode_=0&_closea_=1&isDlg=1&param1=" & pValue & "&timestamp=" & chr(39) & " + new Date().getTime() + " & chr(39) & "" & chr(39) & ", " & chr(39) & "" & chr(39) & ", " & chr(39) & "modal=yes;directories=no;menubar=no;resizable=yes;toolbar=no;status=no;center=yes;dialogWidth=750px;dialogHeight=570px;" & chr(39) & ");};return false;"
End Select
pLinkButton.Visible = True
If pDeleteButton IsNot Nothing Then pDeleteButton.Visible = True

End Sub
Public Sub objexplorer_Show(ByVal pControlIDValue As String, ByVal pControlIDText As String,  ByVal pControlIDType As String,ByVal pObjectTvwType As String, ByVal pObjectGrdType As String,ByVal pControlIDDelete As String)
tvwmodalpopuppnlobjexplorer.Nodes.Clear()
Dim auxStartNode As TreeNode=Nothing
Dim auxObjectTypes As String = ""
For Each auxString As String In Split(pObjectTvwType, "{#CHR34#}")
  Dim auxObjectID as Integer = Val(auxString)
  auxObjectTypes &= "{#CHR34#}" & auxObjectID & "{#CHR34#}"
  Select Case Val(auxString)
  Case 0
  Case -1
      auxStartNode = New TreeNode("Todos", -1)
      tvwmodalpopuppnlobjexplorer.Nodes.Add(auxStartNode)
       modalpopuppnlobjexplorer_Load(-1, auxStartNode, pObjectTvwType)
    Case 10
      auxStartNode= New TreeNode("<img width=14 height=14 border=0 src='imagenes/icon00000010.png' />Unidades", "00000010")
tvwmodalpopuppnlobjexplorer.Nodes.Add(auxStartNode)
modalpopuppnlobjexplorer_Load(-1, auxStartNode, auxObjectID)
  End Select
Next
grdobjexplorer.Visible=True
tvwmodalpopuppnlobjexplorer.Nodes(0).Select()
tvwmodalpopuppnlobjexplorer_SelectedNodeChanged(tvwmodalpopuppnlobjexplorer,Nothing)
tvwmodalpopuppnlobjexplorer.Nodes(0).Expand()
ViewState("modalpopuppnlobjexplorer_controlid") = pControlIDText
ViewState("modalpopuppnlobjexplorer_value") = pControlIDValue
ViewState("modalpopuppnlobjexplorer_type") = pControlIDType
ViewState("modalpopuppnlobjexplorer_delete") = pControlIDDelete
Dim auxObjectGrid As String = ""
For Each auxString As String In Split(pObjectGrdType, "{#CHR34#}")
  If auxString.Trim <> "" Then
      auxObjectGrid &= "{#CHR34#}" & auxString & "{#CHR34#}"
  End If
Next
ViewState("modalpopuppnlobjexplorer_mode") = auxObjectGrid
grdobjexplorer.Visible=True
cmdobjexplorerfilter.Visible=True
txtobjexplorerfilter.Visible=True
lblobjexplorerfilter.Visible=True
tvwmodalpopuppnlobjexplorer_SelectedNodeChanged(tvwmodalpopuppnlobjexplorer,Nothing)
pnlobjexplorer.Show()
updpnlobjexplorer.Update()

End Sub
Public Sub tvwmodalpopuppnlobjexplorer_SelectedNodeChanged(ByVal sender As Object, ByVal e As System.EventArgs)
Dim auxSelectedValue as String=CType(sender, TreeView).SelectedNode.Value
Dim auxObjectType as String=""
If CType(sender, TreeView).SelectedNode.Depth=0 Then
    auxSelectedValue=-1
End If
If grdobjexplorer.Visible = False Then
    modalpopuppnlobjexplorer_Select(auxSelectedValue, 10, CType(sender, TreeView).SelectedNode.Text)
Else
    Dim auxQuery As String = ""
    If Instr(ViewState("modalpopuppnlobjexplorer_mode"), "{#CHR34#}9{#CHR34#}") > 0 Then
        If auxQuery <> "" Then auxQuery &= " UNION ALL "
        auxQuery &= "(SELECT cod as cod,dsc as dsc,9 as objecttype,'Colaborador' as objecttypedsc FROM EMP   WHERE (((((EMP.undcod =" & auxSelectedValue & ") AND '" & txtobjexplorerfilter.Text.Trim & "'='')  OR  (EMP.dsc LIKE '%" & txtobjexplorerfilter.Text.Trim & "%' AND '" & txtobjexplorerfilter.Text.Trim & "'<>''))) AND (EMP.baja = 0 OR EMP.baja  IS NULL)) AND EMP.cod >= 1)"
    End If
    If Instr(ViewState("modalpopuppnlobjexplorer_mode"), "{#CHR34#}10{#CHR34#}") > 0 Then
        If auxQuery <> "" Then auxQuery &= " UNION ALL "
        auxQuery &= "(SELECT cod as cod,dsc as dsc,10 as objecttype,'Unidad' as objecttypedsc FROM UND   WHERE (((((UND.undcodsup =" & auxSelectedValue & ") AND '" & txtobjexplorerfilter.Text.Trim & "'='')  OR  (UND.dsc LIKE '%" & txtobjexplorerfilter.Text.Trim & "%' AND '" & txtobjexplorerfilter.Text.Trim & "'<>''))) AND (UND.baja = 0 OR UND.baja  IS NULL)) AND UND.cod >= 1)"
    End If
    auxQuery &=" ORDER BY dsc,objecttype,cod"
    If e IsNot Nothing Then
        txtobjexplorerfilter.Text=""
    End If
    Dim auxConn As clsHrcConnClient = Session("conn")
    auxConn.gConn_Open
    grdobjexplorer.DataSource= auxConn.gConn_Query(auxQuery)
    grdobjexplorer.DataBind()
    updpnlobjexplorer.Update()
    auxConn.gConn_Close
End If

End Sub

Protected Sub frmUnidades_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
m_PermFormEdit=Session("isadmin") OR CType(Session("security"),clsHrcSecurityClient).gSID_CheckAccess( 3)
m_PermFormNew=Session("isadmin") OR CType(Session("security"),clsHrcSecurityClient).gSID_CheckAccess(2)
m_PermFormDelete=CType(Session("security"),clsHrcSecurityClient).gSID_CheckAccess(5)
End Sub
 Protected Overrides Sub OnInit(ByVal e As System.EventArgs)
MyBase.OnInit(e)
ViewStateUserKey = Session.SessionID

End Sub

End Class

