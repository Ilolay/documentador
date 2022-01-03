Imports System.Data
Imports System.Data.SqlClient
Imports Intelimedia.imComponentes
Public Class frmMail_det
Inherits System.Web.UI.Page
Friend m_PermFormDelete as Boolean=False
Friend m_PermFormEdit as Boolean=False
Friend m_PermFormNew as Boolean=False
Protected Sub itemctrl000008_Databound(ByVal sender As Object, ByVal e As System.EventArgs)
CType(sender,Label).Text=Eval("cod").ToString

End Sub

Protected Sub itemctrl000006_Databound(ByVal sender As Object, ByVal e As System.EventArgs)
CType(sender,Label).Text=Replace(Eval("dsc").ToString,Chr(10),"<br />")

End Sub

Protected Sub itemctrl000005_Databound(ByVal sender As Object, ByVal e As System.EventArgs)
CType(sender,Label).Text=Replace(Eval("content").ToString,Chr(10),"<br />")

End Sub

Protected Sub itemctrl000004_Databound(ByVal sender As Object, ByVal e As System.EventArgs)
CType(sender,Label).Text=Replace(Eval("subject").ToString,Chr(10),"<br />")

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

Protected Sub cmdFormViewItemUpdate_Click (ByVal sender As Object, ByVal e As System.EventArgs)
Server.Transfer(Request.Url.AbsolutePath & "?" & Replace(Request.QueryString.ToString, "_mode_=0", "_mode_=2"))
End Sub
Protected Sub cmdFormViewItemUpdate_DataBinding(ByVal sender As Object, ByVal e As System.EventArgs)
sender.visible=Session("isadmin") Or CType(Session("security"),clsHrcSecurityClient).gSID_CheckAccess(3)

End Sub

Protected Sub editctrl000006_DataBound_frmdatos(ByVal sender As Object, ByVal e As System.EventArgs)
CType(sender,TextBox).Text=Eval("dsc").ToString

End Sub

Protected Sub editctrl000005_Databound(ByVal sender As Object, ByVal e As System.EventArgs)
If Not IsPostBack Or CType(sender,HiddenField).Value ="" Then
    CType(sender,HiddenField).Value = Guid.NewGuid.ToString.Replace("-","")
    CType(sender.parent.FindControl("fmeeditctrl000005"),HtmlControl).Attributes("src") = "hrctexteditor.aspx?upload=2&tmp=1&tmpid=" & CType(sender,HiddenField).Value
    Dim auxConn As New imClientConnection
    auxConn.gTextTmp_Upload(Eval("content").ToString, CType(sender, HiddenField).Value)
End If

End Sub

Protected Sub editctrl000004_DataBound_frmdatos(ByVal sender As Object, ByVal e As System.EventArgs)
CType(sender,TextBox).Text=Eval("subject").ToString

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

Protected Sub editctrl000002_Databound(ByVal sender As Object, ByVal e As System.EventArgs)
CType(sender,Label).Text=Eval("qsecdatetime").ToString

End Sub

Protected Sub cmdFormUpdateItemView_Click (ByVal sender As Object, ByVal e As System.EventArgs)
Server.Transfer(Request.Url.AbsolutePath & "?" & Replace(Request.QueryString.ToString, "_mode_=2", "_mode_=0"))
End Sub

Protected Sub cmdFormViewCancelUpdate_Click (ByVal sender As Object, ByVal e As System.EventArgs)
gForm_Close()
End Sub

Protected Sub insctrl000005_Databound(ByVal sender As Object, ByVal e As System.EventArgs)
If Not IsPostBack Or CType(sender,HiddenField).Value ="" Then
    CType(sender,HiddenField).Value = Guid.NewGuid.ToString.Replace("-","")
    CType(sender.parent.FindControl("fmeinsctrl000005"),HtmlControl).Attributes("src") = "hrctexteditor.aspx?upload=2&tmp=1&tmpid=" & CType(sender,HiddenField).Value
    Dim auxConn As New imClientConnection
    auxConn.gTextTmp_Upload("", CType(sender, HiddenField).Value)
End If

End Sub

Protected Sub cmdFormViewReInsert_Click (ByVal sender As Object, ByVal e As System.EventArgs)
Dim auxEndOK as Boolean=True
Page.Validate("vgins")
If Page.IsValid Then
Try
frmdatos.InsertItem(False)
frmdatos.ChangeMode(FormViewMode.Insert)
Catch ex as sqlException
  auxEndOK=False
lblerror.Text &= "//Error de actualizacion (" & ex.Message & ")"
End Try
End If

End Sub

Protected Sub cmdFormViewInsert_Click (ByVal sender As Object, ByVal e As System.EventArgs)
Dim auxEndOK as Boolean=True
Page.Validate("vgins")
If Page.IsValid Then
Try
frmdatos.InsertItem(False)
Catch ex as sqlException
  auxEndOK=False
lblerror.Text &= "//Error de actualizacion (" & ex.Message & ")"
End Try
If auxEndOK Then 
     If Request.QueryString("_closea_") = "1" Then
      ScriptManager.RegisterStartupScript(Me, Me.GetType(), "CloseWindow", "self.close();", True)
     Else
         Response.Redirect("frmMail.aspx?_mode_=7&_closea_=0")
     End If
End If
End If

End Sub

Protected Sub cmdFormViewInsertCancel_Click (ByVal sender As Object, ByVal e As System.EventArgs)
gForm_Close
End Sub

Protected Sub frmdatos_ItemDeleted(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.FormViewDeletedEventArgs)
If e.Exception IsNot Nothing Then
  lblerror.Text &="//Error actualizando datos. Vuelva a realizar la operacion!"
  
Else
    Dim auxClass As New clscusimDOC
    auxClass.Conn.gConn_Open
         
    Dim auxDataKeyCod as Integer=e.Keys(0)
    Dim auxResult as String=auxClass.gSystem_PostAction(28,13,auxDataKeyCod) 
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
    
    Dim auxResult as String=auxClass.gSystem_PostAction(28,11,auxDataKeyCod) 
    lblerror.Text &=auxResult
    If auxResult ="" Then
    End If
    auxClass.Conn.gConn_Close
End If

End Sub
Protected Sub frmdatos_ItemInserting(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.FormViewInsertEventArgs)
CType(CType(sender,FormView).DataSourceObject,SqlDataSource).InsertParameters("content").DefaultValue = (New Intelimedia.imComponentes.imClientConnection).gTextTmp_Download(Ctype(frmdatos.FindControl("insctrl000005"), HiddenField).Value)

End Sub
Protected Sub frmdatos_ItemSelected(ByVal sender As Object, ByVal e As System.EventArgs)
If frmdatos.DataItem IsNot Nothing Then
   Page.Title = "Mail| " &  Databinder.Eval(frmdatos.DataItem ,"dsc").ToString
   lblsubtitle.Text = Databinder.Eval(frmdatos.DataItem ,"dsc").ToString
Else
   Page.Title = "Mail| Nuevo"
   lblsubtitle.Text = "Nuevo"
End If


If sender.FindControl("cmdFormViewConfirmDelete") IsNot Nothing Then
sender.FindControl("cmdFormViewConfirmDelete").Visible=m_PermFormDelete
End If

Select Case Request.QueryString("_mode_")
 Case "1"
 Case "25"
   lblsubtitle.Text = "Copiar"
    Try
        Dim auxConn As New SqlConnection(Session("connectionstringname"))
        auxConn.Open
        Dim auxDA As New SqlDataAdapter("SELECT MAILS.cod,MAILS.dsc,MAILS.content,MAILS.subject,CASE MAILSQSECSID.sidtypecod WHEN -3 THEN (SELECT TOP 1 Q_SECPGRP.grpcod FROM Q_SECPGRP WHERE Q_SECPGRP.sidcod=MAILSQSECSID.sidcod) WHEN -2 THEN (SELECT TOP 1 Q_SECPLOGIN.seccod FROM Q_SECPLOGIN WHERE Q_SECPLOGIN.sidcod=MAILSQSECSID.sidcod) END as qsecsidvalue,CASE MAILSQSECSID.sidtypecod WHEN -3 THEN (SELECT TOP 1 Q_SECPGRP.grpdsc FROM Q_SECPGRP WHERE Q_SECPGRP.grpcod= (SELECT TOP 1 Q_SECPGRP.grpcod FROM Q_SECPGRP WHERE Q_SECPGRP.sidcod=MAILSQSECSID.sidcod)) WHEN -2 THEN (SELECT TOP 1 Q_SECPLOGIN.secdsc FROM Q_SECPLOGIN WHERE Q_SECPLOGIN.seccod= (SELECT TOP 1 Q_SECPLOGIN.seccod FROM Q_SECPLOGIN WHERE Q_SECPLOGIN.sidcod=MAILSQSECSID.sidcod)) END as qsecsiddsc,(MAILSQSECSID.sidtypecod) as qsecsiddsc,MAILS.qsecdatetime,MAILS.qsecsid FROM MAILS  LEFT JOIN Q_SECPSID AS MAILSQSECSID ON MAILSQSECSID.sidcod=MAILS.qsecsid  WHERE MAILS.cod=@param1", auxConn)
        auxDA.SelectCommand.Parameters.Add("@param1", SqlDbType.Int, 4).Value = Request.QueryString("param1")
        Dim auxDT As New DataTable
        auxDA.Fill(auxDT)
        auxConn.Close
        If auxDT.Rows.Count <> 0 Then
             If IsDBNull(auxDT.Rows(0)("dsc")) = False Then
             CType(sender.parent.FindControl("frmdatos$insctrl000006"),TextBox).Text=auxDT.Rows(0)("dsc")
             End If 
             If IsDBNull(auxDT.Rows(0)("content")) = False Then
             CType(sender.parent.FindControl("frmdatos$insctrl000005"),TextBox).Text=auxDT.Rows(0)("content")
             End If 
             If IsDBNull(auxDT.Rows(0)("subject")) = False Then
             CType(sender.parent.FindControl("frmdatos$insctrl000004"),TextBox).Text=auxDT.Rows(0)("subject")
             End If 
        End If
    Catch ex as sqlException
    End Try
End Select

End Sub
Protected Sub frmdatos_ItemUpdated(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.FormViewUpdatedEventArgs)
If e.Exception IsNot Nothing Then
  lblerror.Text &="//Error actualizando datos. Vuelva a realizar la operacion!"
     
Else
    Dim auxClass As New clscusimDOC
    auxClass.Conn.gConn_Open
    Dim auxDataKeyCod as Integer= e.Keys(0)
         
    Dim auxResult as String=auxClass.gSystem_PostAction(28,12,auxDataKeyCod) 
    lblerror.Text &=auxResult
    If auxResult ="" Then
gForm_Close
     End If
    auxClass.Conn.gConn_Close
End If

End Sub
Protected Sub frmdatos_ItemUpdating(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.FormViewUpdateEventArgs)
CType(CType(sender,FormView).DataSourceObject,SqlDataSource).UpdateParameters("content").DefaultValue = (New Intelimedia.imComponentes.imClientConnection).gTextTmp_Download(CType(frmdatos.FindControl("editctrl000005"), HiddenField).Value)

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
    sender.InsertParameters("querynextcod").DefaultValue = e.Command.Parameters("@querynextcod").Value
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
         Response.Redirect("frmMail.aspx?_mode_=7&_closea_=0")
     End If

End Sub
Protected Sub frmMail_det_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
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

