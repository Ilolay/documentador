Imports System.Data
Imports System.Data.SqlClient
Imports Intelimedia.imComponentes
Public Class frmEmpresas
Inherits System.Web.UI.Page
Friend m_PermFormDelete as Boolean=False
Friend m_PermFormEdit as Boolean=False
Friend m_PermFormNew as Boolean=False
Protected Sub ctrl000022_Click (ByVal sender As Object, ByVal e As System.EventArgs)
ctrl000023.DataBind()

End Sub

Protected Sub ctrl000021_Click (ByVal sender As Object, ByVal e As System.EventArgs)
             CType(sender.parent.FindControl("ctrl000014"),TextBox).Text=""

End Sub

Protected Sub ctrl000020_Load(ByVal sender As Object, ByVal e As System.EventArgs)
sender.Visible=m_PermFormNew

End Sub

Protected Sub ctrl000023colitem1view_DataBinding(ByVal sender As Object, ByVal e As System.EventArgs)
If IsDBNull(Eval("cod")) Then 
     Ctype(sender,Control).Visible=False
Else
  If Eval("cod") = -1 Then 
     Ctype(sender,Control).Visible=False
  End If
End If

End Sub

Protected Sub cmdctrl000023edit_DataBinding(ByVal sender As Object, ByVal e As System.EventArgs)
sender.Visible=m_PermFormedit
End Sub

Protected Sub cmdctrl000023delete_DataBinding(ByVal sender As Object, ByVal e As System.EventArgs)
sender.Visible=m_PermFormDelete

End Sub

Protected Sub cmdctrl000023copy_DataBinding(ByVal sender As Object, ByVal e As System.EventArgs)
sender.Visible=m_PermFormNew

End Sub

Protected Sub ctrl000023_ItemDeleted(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewCommandEventArgs)

End Sub
Protected Sub ctrl000023_ItemInserted(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewCommandEventArgs)

End Sub
Protected Sub ctrl000023_ItemSelected(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewCommandEventArgs)

End Sub
Protected Sub ctrl000023_ItemUpdated(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewCommandEventArgs)

End Sub
Protected Sub ctrl000023_RowCommand(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewCommandEventArgs)
Select Case e.CommandName.ToUpper()
End Select

End Sub
Protected Sub ctrl000023_RowCreated(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs)
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

Protected Sub dsctrl000023_Init(ByVal sender As Object,ByVal e As System.EventArgs)
sender.ConnectionString=Session("connectionstringname")
End Sub
Protected Sub dsctrl000023_Deleted(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.SqlDataSourceStatusEventArgs)
If e.Exception IsNot Nothing Then
  lblerror.Text &="//Error borrando datos (" & e.Exception.Message & ")"
    
Else
    
End If

End Sub
Protected Sub dsctrl000023_Inserted(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.SqlDataSourceStatusEventArgs)
If e.Exception IsNot Nothing Then
  lblerror.Text &="//Error insertando datos (" & e.Exception.Message & ")"
    
Else
    
End If

End Sub
Protected Sub dsctrl000023_Selected(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.SqlDataSourceStatusEventArgs)
If e.Exception IsNot Nothing Then
  lblerror.Text &="//Error buscando datos (" & e.Exception.Message & ")"
    
Else
    
End If

End Sub
Protected Sub dsctrl000023_Selecting(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.SqlDataSourceSelectingEventArgs)
If Session("isadmin") Then
   e.Command.CommandText = e.Command.CommandText.Replace("@qsidcod_list"," 1=1")
ElseIf CType(Session("security"),clsHrcSecurityClient).gSID_CheckAccess(4) Then
   e.Command.CommandText = e.Command.CommandText.Replace("@qsidcod_list"," 1=1")
Else
   e.Command.CommandText = e.Command.CommandText.Replace("@qsidcod_list"," 1=0")
End If

End Sub
Protected Sub dsctrl000023_Updated(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.SqlDataSourceStatusEventArgs)
If e.Exception IsNot Nothing Then
  lblerror.Text &="//Error actualizando datos (" & e.Exception.Message & ")"
    
Else
    
End If

End Sub

Protected Sub frmEmpresas_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
m_PermFormEdit=Session("isadmin") OR CType(Session("security"),clsHrcSecurityClient).gSID_CheckAccess( 3)
m_PermFormNew=Session("isadmin") OR CType(Session("security"),clsHrcSecurityClient).gSID_CheckAccess(2)
m_PermFormDelete=CType(Session("security"),clsHrcSecurityClient).gSID_CheckAccess(5)
End Sub
 Protected Overrides Sub OnInit(ByVal e As System.EventArgs)
MyBase.OnInit(e)
ViewStateUserKey = Session.SessionID

End Sub

End Class

