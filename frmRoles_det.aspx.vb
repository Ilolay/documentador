Imports System.Data
Imports System.Data.SqlClient
Imports Intelimedia.imComponentes
Public Class frmRoles_det
Inherits System.Web.UI.Page
Friend m_PermFormDelete as Boolean=False
Friend m_PermFormEdit as Boolean=False
Friend m_PermFormNew as Boolean=False
Protected Sub itemctrl000007_Databound(ByVal sender As Object, ByVal e As System.EventArgs)
CType(sender,Label).Text=Eval("cod").ToString

End Sub

Protected Sub itemctrl000005_Databound(ByVal sender As Object, ByVal e As System.EventArgs)
CType(sender,Label).Text=Replace(Eval("dsc").ToString,Chr(10),"<br />")

End Sub

Protected Sub itemctrl000004_Databound(ByVal sender As Object, ByVal e As System.EventArgs)
CType(sender,Label).Text=Eval("orden").ToString

End Sub

Protected Sub itemctrl000003view_DataBinding(ByVal sender As Object, ByVal e As System.EventArgs)
If IsDBNull(Eval("qsecsid")) Then 
     Ctype(sender,Control).Visible=False
Else
  If Eval("qsecsid") = -1 Then 
     Ctype(sender,Control).Visible=False
  End If
End If

End Sub

Protected Sub itemctrl000002_Databound(ByVal sender As Object, ByVal e As System.EventArgs)
CType(sender,Label).Text=Eval("qsecdatetime").ToString

End Sub

Protected Sub cmdFormViewItemCancel_Click (ByVal sender As Object, ByVal e As System.EventArgs)
gForm_Close()
End Sub

Protected Sub cmdSecPermView_DataBinding(ByVal sender As Object, ByVal e As System.EventArgs)
sender.visible=Session("isadmin") Or CType(Session("security"),clsHrcSecurityClient).gSID_CheckAccess(-1)
End Sub

Protected Sub frmdatos_ItemDeleted(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.FormViewDeletedEventArgs)
If e.Exception IsNot Nothing Then
  lblerror.Text &="//Error actualizando datos. Vuelva a realizar la operacion!"
  
Else
    Dim auxClass As New clscusimDOC
    auxClass.Conn.gConn_Open
         
    Dim auxDataKeyCod as Integer=e.Keys(0)
    Dim auxResult as String=auxClass.gSystem_PostAction(6,13,auxDataKeyCod) 
    auxClass.gCluster_LoadStaticTables_DOC_ROL(auxDataKeyCod) 
    lblerror.Text &=auxResult
    If auxResult ="" Then
gForm_Close
   End If
    auxClass.Conn.gConn_Close
End If

End Sub
Protected Sub frmdatos_ItemInserted(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.FormViewInsertedEventArgs)
If e.Exception IsNot Nothing Then
    lblerror.Text &="//Error insertando nuevos datos. Vuelva a realizar la operacion!"
    
Else
    Dim auxDataKeyCod as Integer=CInt(dsdatos.InsertParameters("querynextcod").DefaultValue)
    Dim auxClass As New clscusimDOC
    auxClass.Conn.gConn_Open
    
    Dim auxResult as String=auxClass.gSystem_PostAction(6,11,auxDataKeyCod) 
    auxClass.gCluster_LoadStaticTables_DOC_ROL(auxDataKeyCod) 
    lblerror.Text &=auxResult
    If auxResult ="" Then
    End If
    auxClass.Conn.gConn_Close
End If

End Sub
Protected Sub frmdatos_ItemSelected(ByVal sender As Object, ByVal e As System.EventArgs)
If frmdatos.DataItem IsNot Nothing Then
   Page.Title = "Roles| " &  Databinder.Eval(frmdatos.DataItem ,"dsc").ToString
   lblsubtitle.Text = Databinder.Eval(frmdatos.DataItem ,"dsc").ToString
Else
   Page.Title = "Roles| Nuevo"
   lblsubtitle.Text = "Nuevo"
End If


End Sub
Protected Sub frmdatos_ItemUpdated(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.FormViewUpdatedEventArgs)
If e.Exception IsNot Nothing Then
  lblerror.Text &="//Error actualizando datos. Vuelva a realizar la operacion!"
     
Else
    Dim auxClass As New clscusimDOC
    auxClass.Conn.gConn_Open
    Dim auxDataKeyCod as Integer= e.Keys(0)
         
    Dim auxResult as String=auxClass.gSystem_PostAction(6,12,auxDataKeyCod) 
    auxClass.gCluster_LoadStaticTables_DOC_ROL(auxDataKeyCod) 
    lblerror.Text &=auxResult
    If auxResult ="" Then
gForm_Close
     End If
    auxClass.Conn.gConn_Close
End If

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
         Response.Redirect(Session("ROOTWEBURL") & "frmRoles.aspx?_mode_=7&_closea_=0")
     End If

End Sub
Protected Sub frmRoles_det_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
If Session("isadmin") = False AND  CType(Session("security"),clsHrcSecurityClient).gSID_CheckAccess(4)=False Then Response.Redirect(System.Web.Hosting.HostingEnvironment.ApplicationVirtualPath)
m_PermFormEdit=Session("isadmin") OR CType(Session("security"),clsHrcSecurityClient).gSID_CheckAccess(3)
m_PermFormNew=Session("isadmin") OR CType(Session("security"),clsHrcSecurityClient).gSID_CheckAccess(2)
m_PermFormDelete=Session("isadmin") OR CType(Session("security"),clsHrcSecurityClient).gSID_CheckAccess(5)
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

