Imports System.Data
Imports System.Data.SqlClient
Imports Intelimedia.imComponentes
Public Class frmProcesos
Inherits System.Web.UI.Page
Friend m_PermFormDelete as Boolean=False
Friend m_PermFormEdit as Boolean=False
Friend m_PermFormNew as Boolean=False
Protected Sub ctrl000016_Load(ByVal sender As Object, ByVal e As System.EventArgs)
If Not Page.IsPostBack Then 
    CType(sender,DropDownList).DataBind
    If CType(sender,DropDownList).Items.FindByValue("-1") IsNot Nothing Then
        CType(sender,DropDownList).Items.FindByValue("-1").Selected = True
    ElseIf CType(sender,DropDownList).Items.Count > 0 Then
        CType(sender,DropDownList).Items(0).Selected = True
    End If
End If

End Sub
Protected Sub txtctrl000016fs_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs)
Try
 If CType(sender, TextBox).Text <> "" Then
     Dim auxConn As New SqlConnection(Session("connectionstringname"))
     auxConn.Open()
     Dim auxDA As New SqlDataAdapter("SELECT DOC_APA.cod FROM DOC_APA   WHERE (abrev = '@param1') AND (DOC_APA.baja = '1900-01-01T00:00:00' OR DOC_APA.baja  IS NULL)", auxConn)
     auxDA.SelectCommand.Parameters.Add("@param1", SqlDbType.Text).Value = CType(sender, TextBox).Text
     Dim auxDT As New DataTable
     auxDA.Fill(auxDT)
     If auxDT.Rows.Count = 0 Then
         CType(sender, TextBox).Text = ""
         CType(sender, TextBox).Focus()
     Else
         If ctrl000016.Items.FindByValue(auxDT.Rows(0)(0)) Is Nothing Then
         ctrl000016.DataBind()
     End If
     If ctrl000016.Items.FindByValue(auxDT.Rows(0)(0)) Is Nothing Then
     Else
         ctrl000016.SelectedValue = auxDT.Rows(0)(0)
         ctrl000016.Focus
     End If
 End If
End If
Catch ex as Exception
 CType(sender, TextBox).Text = ""
 CType(sender, TextBox).Focus
End Try

End Sub

Protected Sub dsctrl000016_Init(ByVal sender As Object,ByVal e As System.EventArgs)
sender.ConnectionString=Session("connectionstringname")
End Sub
Protected Sub dsctrl000016_Deleted(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.SqlDataSourceStatusEventArgs)
If e.Exception IsNot Nothing Then
  lblerror.Text &="//Error borrando datos (" & e.Exception.Message & ")"
    
Else
    
End If

End Sub
Protected Sub dsctrl000016_Inserted(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.SqlDataSourceStatusEventArgs)
If e.Exception IsNot Nothing Then
  lblerror.Text &="//Error insertando datos (" & e.Exception.Message & ")"
    
Else
    
End If

End Sub
Protected Sub dsctrl000016_Selected(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.SqlDataSourceStatusEventArgs)
If e.Exception IsNot Nothing Then
  lblerror.Text &="//Error buscando datos (" & e.Exception.Message & ")"
    
Else
    
End If

End Sub
Protected Sub dsctrl000016_Updated(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.SqlDataSourceStatusEventArgs)
If e.Exception IsNot Nothing Then
  lblerror.Text &="//Error actualizando datos (" & e.Exception.Message & ")"
    
Else
    
End If

End Sub

Protected Sub ctrl000014_Click (ByVal sender As Object, ByVal e As System.EventArgs)
ctrl000019.DataBind()

End Sub

Protected Sub ctrl000013_Click (ByVal sender As Object, ByVal e As System.EventArgs)
CType(sender.parent.FindControl("ctrl000016"),DropDownList).ClearSelection()
                          CType(sender.parent.FindControl("txtctrl000016fs"),TextBox).Text=""

             CType(sender.parent.FindControl("ctrl000017"),TextBox).Text=""

End Sub

Protected Sub ctrl000012_Load(ByVal sender As Object, ByVal e As System.EventArgs)
sender.Visible=m_PermFormNew

End Sub

Protected Sub ctrl000019colitem1_Databound(ByVal sender As Object, ByVal e As System.EventArgs)
CType(sender,Label).Text=Eval("orden").ToString

End Sub

Protected Sub ctrl000019colitem2view_DataBinding(ByVal sender As Object, ByVal e As System.EventArgs)
If IsDBNull(Eval("cod")) Then 
     Ctype(sender,Control).Visible=False
Else
  If Eval("cod") = -1 Then 
     Ctype(sender,Control).Visible=False
  End If
End If

End Sub

Protected Sub ctrl000019colitem3view_DataBinding(ByVal sender As Object, ByVal e As System.EventArgs)
If IsDBNull(Eval("apacod")) Then 
     Ctype(sender,Control).Visible=False
Else
  If Eval("apacod") = -1 Then 
     Ctype(sender,Control).Visible=False
  End If
End If

End Sub

Protected Sub cmdItemUp_Click (ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs)
Dim auxClass as new clscusimDOC
auxClass.gEntity_DOC_PRO_ItemChangeOrder(pCod:=sender.CommandArgument,pToUp:=True)
Dim auxPageIndex as Integer=ctrl000019.PageIndex
ctrl000019.DataBind
ctrl000019.PageIndex = auxPageIndex
If CType(ctrl000019.Parent.Parent, UpdatePanel).UpdateMode = UpdatePanelUpdateMode.Conditional Then
  CType(ctrl000019.Parent.Parent, UpdatePanel).Update()
End If

End Sub
Protected Sub cmdItemUp_DataBinding(ByVal sender As Object, ByVal e As System.EventArgs)
Ctype(sender,Control).Visible = Not IsDBNull(Eval("orden"))
End Sub

Protected Sub cmdItemDown_Click (ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs)
Dim auxClass as new clscusimDOC
auxClass.gEntity_DOC_PRO_ItemChangeOrder(pCod:=sender.CommandArgument,pToUp:=False)
Dim auxPageIndex as Integer=ctrl000019.PageIndex
ctrl000019.DataBind
ctrl000019.PageIndex = auxPageIndex
If CType(ctrl000019.Parent.Parent, UpdatePanel).UpdateMode = UpdatePanelUpdateMode.Conditional Then
  CType(ctrl000019.Parent.Parent, UpdatePanel).Update()
End If

End Sub
Protected Sub cmdItemDown_DataBinding(ByVal sender As Object, ByVal e As System.EventArgs)
Ctype(sender,Control).Visible = Not IsDBNull(Eval("orden"))
End Sub

Protected Sub cmdctrl000019edit_DataBinding(ByVal sender As Object, ByVal e As System.EventArgs)
sender.Visible=m_PermFormedit
End Sub

Protected Sub cmdctrl000019delete_DataBinding(ByVal sender As Object, ByVal e As System.EventArgs)
sender.Visible=m_PermFormDelete

End Sub

Protected Sub cmdctrl000019copy_DataBinding(ByVal sender As Object, ByVal e As System.EventArgs)
sender.Visible=m_PermFormNew

End Sub

Protected Sub ctrl000019_ItemDeleted(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewCommandEventArgs)

End Sub
Protected Sub ctrl000019_ItemInserted(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewCommandEventArgs)

End Sub
Protected Sub ctrl000019_ItemSelected(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewCommandEventArgs)

End Sub
Protected Sub ctrl000019_ItemUpdated(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewCommandEventArgs)

End Sub
Protected Sub ctrl000019_RowCommand(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewCommandEventArgs)
Select Case e.CommandName.ToUpper()
End Select

End Sub
Protected Sub ctrl000019_RowCreated(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs)
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

Protected Sub dsctrl000019_Init(ByVal sender As Object,ByVal e As System.EventArgs)
sender.ConnectionString=Session("connectionstringname")
End Sub
Protected Sub dsctrl000019_Deleted(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.SqlDataSourceStatusEventArgs)
If e.Exception IsNot Nothing Then
  lblerror.Text &="//Error borrando datos (" & e.Exception.Message & ")"
    
Else
    
End If

End Sub
Protected Sub dsctrl000019_Inserted(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.SqlDataSourceStatusEventArgs)
If e.Exception IsNot Nothing Then
  lblerror.Text &="//Error insertando datos (" & e.Exception.Message & ")"
    
Else
    
End If

End Sub
Protected Sub dsctrl000019_Selected(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.SqlDataSourceStatusEventArgs)
If e.Exception IsNot Nothing Then
  lblerror.Text &="//Error buscando datos (" & e.Exception.Message & ")"
    
Else
    
End If

End Sub
Protected Sub dsctrl000019_Selecting(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.SqlDataSourceSelectingEventArgs)
If Session("isadmin") Then
   e.Command.CommandText = e.Command.CommandText.Replace("@qsidcod_list"," 1=1")
ElseIf CType(Session("security"),clsHrcSecurityClient).gSID_CheckAccess(4) Then
   e.Command.CommandText = e.Command.CommandText.Replace("@qsidcod_list"," 1=1")
Else
   e.Command.CommandText = e.Command.CommandText.Replace("@qsidcod_list"," 1=0")
End If

End Sub
Protected Sub dsctrl000019_Updated(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.SqlDataSourceStatusEventArgs)
If e.Exception IsNot Nothing Then
  lblerror.Text &="//Error actualizando datos (" & e.Exception.Message & ")"
    
Else
    
End If

End Sub

Protected Sub frmProcesos_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
m_PermFormEdit=Session("isadmin") OR CType(Session("security"),clsHrcSecurityClient).gSID_CheckAccess( 3)
m_PermFormNew=Session("isadmin") OR CType(Session("security"),clsHrcSecurityClient).gSID_CheckAccess(2)
m_PermFormDelete=CType(Session("security"),clsHrcSecurityClient).gSID_CheckAccess(5)
End Sub
 Protected Overrides Sub OnInit(ByVal e As System.EventArgs)
MyBase.OnInit(e)
ViewStateUserKey = Session.SessionID

End Sub

End Class

