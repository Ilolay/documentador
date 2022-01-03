Imports System.Data
Imports System.Data.SqlClient
Imports Intelimedia.imComponentes
Public Class frmRoles
Inherits System.Web.UI.Page
Friend m_PermFormDelete as Boolean=False
Friend m_PermFormEdit as Boolean=False
Friend m_PermFormNew as Boolean=False
Protected Sub ctrl000012colitem1_Databound(ByVal sender As Object, ByVal e As System.EventArgs)
CType(sender,Label).Text=Eval("orden").ToString

End Sub

Protected Sub ctrl000012colitem2view_DataBinding(ByVal sender As Object, ByVal e As System.EventArgs)
If IsDBNull(Eval("cod")) Then 
     Ctype(sender,Control).Visible=False
Else
  If Eval("cod") = -1 Then 
     Ctype(sender,Control).Visible=False
  End If
End If

End Sub

Protected Sub cmdItemUp_Click (ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs)
Dim auxClass as new clscusimDOC
auxClass.gEntity_DOC_ROL_ItemChangeOrder(pCod:=sender.CommandArgument,pToUp:=True)
Dim auxPageIndex as Integer=ctrl000012.PageIndex
ctrl000012.DataBind
ctrl000012.PageIndex = auxPageIndex
If CType(ctrl000012.Parent.Parent, UpdatePanel).UpdateMode = UpdatePanelUpdateMode.Conditional Then
  CType(ctrl000012.Parent.Parent, UpdatePanel).Update()
End If

End Sub
Protected Sub cmdItemUp_DataBinding(ByVal sender As Object, ByVal e As System.EventArgs)
Ctype(sender,Control).Visible = Not IsDBNull(Eval("orden"))
End Sub

Protected Sub cmdItemDown_Click (ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs)
Dim auxClass as new clscusimDOC
auxClass.gEntity_DOC_ROL_ItemChangeOrder(pCod:=sender.CommandArgument,pToUp:=False)
Dim auxPageIndex as Integer=ctrl000012.PageIndex
ctrl000012.DataBind
ctrl000012.PageIndex = auxPageIndex
If CType(ctrl000012.Parent.Parent, UpdatePanel).UpdateMode = UpdatePanelUpdateMode.Conditional Then
  CType(ctrl000012.Parent.Parent, UpdatePanel).Update()
End If

End Sub
Protected Sub cmdItemDown_DataBinding(ByVal sender As Object, ByVal e As System.EventArgs)
Ctype(sender,Control).Visible = Not IsDBNull(Eval("orden"))
End Sub

Protected Sub ctrl000012_ItemDeleted(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewCommandEventArgs)

End Sub
Protected Sub ctrl000012_ItemInserted(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewCommandEventArgs)

End Sub
Protected Sub ctrl000012_ItemSelected(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewCommandEventArgs)

End Sub
Protected Sub ctrl000012_ItemUpdated(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewCommandEventArgs)

End Sub
Protected Sub ctrl000012_RowCommand(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewCommandEventArgs)
Select Case e.CommandName.ToUpper()
End Select

End Sub
Protected Sub ctrl000012_RowCreated(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs)
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

Protected Sub dsctrl000012_Init(ByVal sender As Object,ByVal e As System.EventArgs)
sender.ConnectionString=Session("connectionstringname")
End Sub
Protected Sub dsctrl000012_Deleted(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.SqlDataSourceStatusEventArgs)
If e.Exception IsNot Nothing Then
  lblerror.Text &="//Error borrando datos (" & e.Exception.Message & ")"
    
Else
    
End If

End Sub
Protected Sub dsctrl000012_Inserted(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.SqlDataSourceStatusEventArgs)
If e.Exception IsNot Nothing Then
  lblerror.Text &="//Error insertando datos (" & e.Exception.Message & ")"
    
Else
    
End If

End Sub
Protected Sub dsctrl000012_Selected(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.SqlDataSourceStatusEventArgs)
If e.Exception IsNot Nothing Then
  lblerror.Text &="//Error buscando datos (" & e.Exception.Message & ")"
    
Else
    
End If

End Sub
Protected Sub dsctrl000012_Selecting(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.SqlDataSourceSelectingEventArgs)
If Session("isadmin") Then
   e.Command.CommandText = e.Command.CommandText.Replace("@qsidcod_list"," 1=1")
ElseIf CType(Session("security"),clsHrcSecurityClient).gSID_CheckAccess(4) Then
   e.Command.CommandText = e.Command.CommandText.Replace("@qsidcod_list"," 1=1")
Else
   e.Command.CommandText = e.Command.CommandText.Replace("@qsidcod_list"," 1=0")
End If

End Sub
Protected Sub dsctrl000012_Updated(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.SqlDataSourceStatusEventArgs)
If e.Exception IsNot Nothing Then
  lblerror.Text &="//Error actualizando datos (" & e.Exception.Message & ")"
    
Else
    
End If

End Sub

Protected Sub frmRoles_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
m_PermFormEdit=Session("isadmin") OR CType(Session("security"),clsHrcSecurityClient).gSID_CheckAccess( 3)
m_PermFormNew=Session("isadmin") OR CType(Session("security"),clsHrcSecurityClient).gSID_CheckAccess(2)
m_PermFormDelete=CType(Session("security"),clsHrcSecurityClient).gSID_CheckAccess(5)
End Sub
 Protected Overrides Sub OnInit(ByVal e As System.EventArgs)
MyBase.OnInit(e)
ViewStateUserKey = Session.SessionID

End Sub

End Class

