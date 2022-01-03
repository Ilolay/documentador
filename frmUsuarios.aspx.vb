Imports System.Data
Imports System.Data.SqlClient
Imports Intelimedia.imComponentes
Public Class frmUsuarios
Inherits System.Web.UI.Page
Friend m_PermFormDelete as Boolean=False
Friend m_PermFormEdit as Boolean=False
Friend m_PermFormNew as Boolean=False
Protected Sub ctrl000015_Click (ByVal sender As Object, ByVal e As System.EventArgs)
ctrl000019.DataBind()

End Sub

Protected Sub ctrl000014_Click (ByVal sender As Object, ByVal e As System.EventArgs)
             CType(sender.parent.FindControl("ctrl000017"),TextBox).Text=""

End Sub

Protected Sub ctrl000019colitem1view_DataBinding(ByVal sender As Object, ByVal e As System.EventArgs)
If IsDBNull(Eval("seccod")) Then 
     Ctype(sender,Control).Visible=False
Else
  If Eval("seccod") = -1 Then 
     Ctype(sender,Control).Visible=False
  End If
End If

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
Dim auxSecurity as clsHrcSecurityClient= Session("security"),clsHrcSecurityClient
Dim auxConnn as clsHrcConnClient= Session("conn"),clsHrcConnClient
If Session("isadmin") Or auxSecurity.gMember_IsInGroupByID(clshrcimDOC.coGroupIDAdmins) Then
   e.Command.CommandText = e.Command.CommandText.Replace("@qsidcod_list"," 1=1")
Else
If auxSecurity.gMember_IsInGroupByID(clshrcimDOC.coGroupIDAccountAdmins) Then
   e.Command.CommandText = e.Command.CommandText.Replace("@qsidcod_list"," (Q_SECPLOGIN.seccod NOT IN (" & auxConnn.gFieldDB_GetString(auxSecurity.gGroup_ResolveLogins(auxSecurity.gGroup_GetCodByID(clshrcimDOC.coGroupIDAdmins)),"seccod") & "))")
Else
   e.Command.CommandText = e.Command.CommandText.Replace("@qsidcod_list"," 1=0")
End If
End If

End Sub
Protected Sub dsctrl000019_Updated(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.SqlDataSourceStatusEventArgs)
If e.Exception IsNot Nothing Then
  lblerror.Text &="//Error actualizando datos (" & e.Exception.Message & ")"
    
Else
    
End If

End Sub

Protected Sub frmUsuarios_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
Dim auxSecurity as clsHrcSecurityClient=Session("security")
If Session("isadmin") Or auxSecurity.gMember_IsInGroupByID(clshrcimDOC.coGroupIDAdmins) Then
    m_PermFormEdit=True
ElseIf auxSecurity.gMember_IsInGroupByID(clshrcimDOC.coGroupIDAccountAdmins) Then
   m_PermFormEdit=Not auxSecurity.gMember_IsInGroupByID(auxSecurity.gLogin_GetSidCod(CInt(Request.QueryString("param1"))),clshrcimDOC.coGroupIDAdmins)
End If
If m_PermFormEdit=False Then Response.Redirect(System.Web.Hosting.HostingEnvironment.ApplicationVirtualPath)
m_PermFormNew=Session("isadmin") OR auxSecurity.gSID_CheckAccess(1)
m_PermFormDelete=Session("isadmin") OR auxSecurity.gSID_CheckAccess(1)

End Sub
 Protected Overrides Sub OnInit(ByVal e As System.EventArgs)
MyBase.OnInit(e)
ViewStateUserKey = Session.SessionID

End Sub

End Class

