Imports System.Data
Imports System.Data.SqlClient
Imports Intelimedia.imComponentes
Public Class frmtimeout
Inherits System.Web.UI.Page
Friend m_PermFormDelete as Boolean=False
Friend m_PermFormEdit as Boolean=False
Friend m_PermFormNew as Boolean=False
Protected Sub ctrl000006_Load(ByVal sender As Object, ByVal e As System.EventArgs)
sender.Visible=m_PermFormNew

End Sub

Protected Sub ctrl000009colitem1view_DataBinding(ByVal sender As Object, ByVal e As System.EventArgs)
If IsDBNull(Eval("sysparamcod")) Then 
     Ctype(sender,Control).Visible=False
Else
  If Eval("sysparamcod") = -1 Then 
     Ctype(sender,Control).Visible=False
  End If
End If

End Sub

Protected Sub cmdctrl000009edit_DataBinding(ByVal sender As Object, ByVal e As System.EventArgs)
sender.Visible=m_PermFormedit
End Sub

Protected Sub cmdctrl000009delete_DataBinding(ByVal sender As Object, ByVal e As System.EventArgs)
sender.Visible=m_PermFormDelete

End Sub

Protected Sub cmdctrl000009copy_DataBinding(ByVal sender As Object, ByVal e As System.EventArgs)
sender.Visible=m_PermFormNew

End Sub

Protected Sub ctrl000009_ItemDeleted(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewCommandEventArgs)

End Sub
Protected Sub ctrl000009_ItemInserted(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewCommandEventArgs)

End Sub
Protected Sub ctrl000009_ItemSelected(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewCommandEventArgs)

End Sub
Protected Sub ctrl000009_ItemUpdated(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewCommandEventArgs)

End Sub
Protected Sub ctrl000009_RowCommand(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewCommandEventArgs)
Select Case e.CommandName.ToUpper()
End Select

End Sub
Protected Sub ctrl000009_RowCreated(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs)
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

Protected Sub dsctrl000009_Init(ByVal sender As Object,ByVal e As System.EventArgs)
sender.ConnectionString=Session("connectionstringname")
End Sub
Protected Sub dsctrl000009_Deleted(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.SqlDataSourceStatusEventArgs)
If e.Exception IsNot Nothing Then
  lblerror.Text &="//Error borrando datos (" & e.Exception.Message & ")"
    
Else
    
End If

End Sub
Protected Sub dsctrl000009_Inserted(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.SqlDataSourceStatusEventArgs)
If e.Exception IsNot Nothing Then
  lblerror.Text &="//Error insertando datos (" & e.Exception.Message & ")"
    
Else
    
End If

End Sub
Protected Sub dsctrl000009_Selected(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.SqlDataSourceStatusEventArgs)
If e.Exception IsNot Nothing Then
  lblerror.Text &="//Error buscando datos (" & e.Exception.Message & ")"
    
Else
    
End If

End Sub
Protected Sub dsctrl000009_Updated(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.SqlDataSourceStatusEventArgs)
If e.Exception IsNot Nothing Then
  lblerror.Text &="//Error actualizando datos (" & e.Exception.Message & ")"
    
Else
    
End If

End Sub

Protected Sub frmtimeout_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
m_PermFormEdit=CType(Session("security"),clsHrcSecurityClient).gSID_CheckAccess( 3)
m_PermFormNew=CType(Session("security"),clsHrcSecurityClient).gSID_CheckAccess(2)
m_PermFormDelete=CType(Session("security"),clsHrcSecurityClient).gSID_CheckAccess(5)
End Sub
 Protected Overrides Sub OnInit(ByVal e As System.EventArgs)
MyBase.OnInit(e)
ViewStateUserKey = Session.SessionID

End Sub

End Class

