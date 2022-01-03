Imports System.Data
Imports System.Data.SqlClient
Imports Intelimedia.imComponentes
Public Class frmAyudas_det
Inherits System.Web.UI.Page
Friend m_PermFormDelete as Boolean=False
Friend m_PermFormEdit as Boolean=False
Friend m_PermFormNew as Boolean=False
Protected Sub itemctrl000003view_DataBinding(ByVal sender As Object, ByVal e As System.EventArgs)
If IsDBNull(Eval("qsecsid")) Then 
     Ctype(sender,Control).Visible=False
Else
  If Eval("qsecsid") = -1 Then 
     Ctype(sender,Control).Visible=False
  End If
End If

End Sub

Protected Sub cmdFormViewItemCancel_Click (ByVal sender As Object, ByVal e As System.EventArgs)
gForm_Close()
End Sub

Protected Sub cmdSecPermView_DataBinding(ByVal sender As Object, ByVal e As System.EventArgs)
sender.visible=CType(Session("security"),clsHrcSecurityClient).gSID_CheckAccess(-1)
End Sub

Protected Sub cmdFormViewItemUpdate_Click (ByVal sender As Object, ByVal e As System.EventArgs)
Server.Transfer(Request.Url.AbsolutePath & "?" & Replace(Request.QueryString.ToString, "_mode_=0", "_mode_=2"))
End Sub
Protected Sub cmdFormViewItemUpdate_DataBinding(ByVal sender As Object, ByVal e As System.EventArgs)
sender.visible=CType(Session("security"),clsHrcSecurityClient).gSID_CheckAccess(22)

End Sub

Protected Sub editctrl000005_Databound(ByVal sender As Object, ByVal e As System.EventArgs)
If Not IsPostBack Or CType(sender,HiddenField).Value ="" Then
    CType(sender,HiddenField).Value = Guid.NewGuid.ToString.Replace("-","")
    CType(sender.parent.FindControl("fmeeditctrl000005"),HtmlControl).Attributes("src") = "hrctexteditor.aspx?upload=2&tmp=1&tmpid=" & CType(sender,HiddenField).Value
    Dim auxConn As New imClientConnection
    auxConn.gTextTmp_Upload(Eval("hlpobs").ToString, CType(sender, HiddenField).Value)
End If

End Sub

Protected Sub editctrl000003view_DataBinding(ByVal sender As Object, ByVal e As System.EventArgs)
If IsDBNull(Eval("qsecsid")) Then 
     Ctype(sender,Control).Visible=False
Else
  If Eval("qsecsid") = -1 Then 
     Ctype(sender,Control).Visible=False
  End If
End If

End Sub

Protected Sub cmdFormUpdateItemView_Click (ByVal sender As Object, ByVal e As System.EventArgs)
Server.Transfer(Request.Url.AbsolutePath & "?" & Replace(Request.QueryString.ToString, "_mode_=2", "_mode_=0"))
End Sub

Protected Sub cmdFormViewCancelUpdate_Click (ByVal sender As Object, ByVal e As System.EventArgs)
gForm_Close()
End Sub

Protected Sub frmdatos_ItemDeleted(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.FormViewDeletedEventArgs)
If e.Exception IsNot Nothing Then
  lblsubtitle.Text &="//Error actualizando datos. Vuelva a realizar la operacion!"
  
Else
    Dim auxClass As New clscusimDOC
    auxClass.Conn.gConn_Open
    Dim auxDataKeyCod as Integer=e.Keys(0)
    Dim auxResult as String=auxClass.gSystem_PostAction(36,13,auxDataKeyCod) 
    lblsubtitle.Text &=auxResult
    If auxResult ="" Then
         
gForm_Close
   End If
    auxClass.Conn.gConn_Close
End If

End Sub
Protected Sub frmdatos_ItemInserted(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.FormViewInsertedEventArgs)
If e.Exception IsNot Nothing Then
    lblsubtitle.Text &="//Error insertando nuevos datos. Vuelva a realizar la operacion!"
    
Else
    Dim auxDataKeyCod as Integer=CInt(dsdatos.InsertParameters("querynextcod").DefaultValue)
    Dim auxClass As New clscusimDOC
    auxClass.Conn.gConn_Open
    Dim auxResult as String=auxClass.gSystem_PostAction(36,11,auxDataKeyCod) 
    lblsubtitle.Text &=auxResult
    If auxResult ="" Then
        
    End If
    auxClass.Conn.gConn_Close
End If

End Sub
Protected Sub frmdatos_ItemSelected(ByVal sender As Object, ByVal e As System.EventArgs)
If frmdatos.DataItem IsNot Nothing Then
   Page.Title = "Ayudas| " &  Databinder.Eval(frmdatos.DataItem ,"hlpdsc").ToString
   lblsubtitle.Text = Databinder.Eval(frmdatos.DataItem ,"hlpdsc").ToString
Else
   Page.Title = "Ayudas| Nuevo"
   lblsubtitle.Text = "Nuevo"
End If


End Sub
Protected Sub frmdatos_ItemUpdated(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.FormViewUpdatedEventArgs)
If e.Exception IsNot Nothing Then
  lblsubtitle.Text &="//Error actualizando datos. Vuelva a realizar la operacion!"
     
Else
    Dim auxClass As New clscusimDOC
    auxClass.Conn.gConn_Open
    Dim auxDataKeyCod as Integer= e.Keys(0)
    Dim auxResult as String=auxClass.gSystem_PostAction(36,12,auxDataKeyCod) 
    lblsubtitle.Text &=auxResult
    If auxResult ="" Then
         
gForm_Close
     End If
    auxClass.Conn.gConn_Close
End If

End Sub
Protected Sub frmdatos_ItemUpdating(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.FormViewUpdateEventArgs)
CType(CType(sender,FormView).DataSourceObject,SqlDataSource).UpdateParameters("hlpobs").DefaultValue = (New Intelimedia.imComponentes.imClientConnection).gTextTmp_Download(CType(frmdatos.FindControl("editctrl000005"), HiddenField).Value)

End Sub

Protected Sub dsdatos_Init(ByVal sender As Object,ByVal e As System.EventArgs)
sender.ConnectionString=Session("connectionstringname")
End Sub
Protected Sub dsdatos_Deleted(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.SqlDataSourceStatusEventArgs)
If e.Exception IsNot Nothing Then
  lblerror.Text &="//Error borrando datos (" & e.Exception.Message & ")"
    
Else
    
End If

End Sub
Protected Sub dsdatos_Inserted(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.SqlDataSourceStatusEventArgs)
If e.Exception IsNot Nothing Then
  lblerror.Text &="//Error insertando datos (" & e.Exception.Message & ")"
    
Else
    
End If

End Sub
Protected Sub dsdatos_Selected(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.SqlDataSourceStatusEventArgs)
If e.Exception IsNot Nothing Then
  lblerror.Text &="//Error buscando datos (" & e.Exception.Message & ")"
    
Else
    
End If

End Sub
Protected Sub dsdatos_Updated(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.SqlDataSourceStatusEventArgs)
If e.Exception IsNot Nothing Then
  lblerror.Text &="//Error actualizando datos (" & e.Exception.Message & ")"
    
Else
    
End If

End Sub

Private Sub gForm_Close()
     If Request.QueryString("_closea_") = "1" Then
      ScriptManager.RegisterStartupScript(Me, Me.GetType(), "CloseWindow", "self.close();", True)
     Else
         Response.Redirect("frmAyudas.aspx?_mode_=7&_closea_=0")
     End If

End Sub
Protected Sub frmAyudas_det_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
m_PermFormEdit=CType(Session("security"),clsHrcSecurityClient).gSID_CheckAccess(22)
m_PermFormNew=CType(Session("security"),clsHrcSecurityClient).gSID_CheckAccess(-1)
m_PermFormDelete=CType(Session("security"),clsHrcSecurityClient).gSID_CheckAccess(-1)
If Not IsPostBack Then
lblerror.Text =""
frmdatos.ChangeMode(if(Request.QueryString("_mode_") = "1" Or Request.QueryString("_mode_") = "25",FormViewMode.Insert, if(Request.QueryString("_mode_") = "2",FormViewMode.Edit, FormViewMode.ReadOnly)))
End If

End Sub
 Protected Overrides Sub OnInit(ByVal e As System.EventArgs)
MyBase.OnInit(e)
ViewStateUserKey = Session.SessionID

End Sub

End Class

