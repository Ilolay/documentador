Imports System.Data
Imports System.Data.SqlClient
Imports Intelimedia.imComponentes
Public Class hrckya_det
Inherits System.Web.UI.Page
Friend m_PermFormDelete as Boolean=False
Friend m_PermFormEdit as Boolean=False
Friend m_PermFormNew as Boolean=False
Protected Sub itemctrl000009_Databound(ByVal sender As Object, ByVal e As System.EventArgs)
CType(sender,Label).Text=Eval("kyacod").ToString

End Sub

Protected Sub itemctrl000007_Databound(ByVal sender As Object, ByVal e As System.EventArgs)
CType(sender,Label).Text=Eval("kyacod").ToString

End Sub

Protected Sub itemctrl000006_Databound(ByVal sender As Object, ByVal e As System.EventArgs)
CType(sender,Label).Text=Replace(Eval("kyadsc").ToString,Chr(10),"<br />")

End Sub

Protected Sub itemctrl000005_Databound(ByVal sender As Object, ByVal e As System.EventArgs)
CType(sender,Label).Text=HttpUtility.HtmlEncode(Replace(Eval("kyapub").ToString,Chr(10),"<br />"))

End Sub

Protected Sub itemctrl000004_Databound(ByVal sender As Object, ByVal e As System.EventArgs)
CType(sender,Label).Text=HttpUtility.HtmlEncode(Replace(Eval("kyaprv").ToString,Chr(10),"<br />"))

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

Protected Sub cmdFormViewItemUpdate_Click (ByVal sender As Object, ByVal e As System.EventArgs)
Server.Transfer(Request.Url.AbsolutePath & "?" & Replace(Request.QueryString.ToString, "_mode_=0", "_mode_=2"))
End Sub

Protected Sub editctrl000007_Databound(ByVal sender As Object, ByVal e As System.EventArgs)
CType(sender,Label).Text=Eval("kyacod").ToString

End Sub

Protected Sub editctrl000006_DataBound_frmdatos(ByVal sender As Object, ByVal e As System.EventArgs)
CType(sender,TextBox).Text=Eval("kyadsc").ToString

End Sub

Protected Sub editctrl000005_Databound(ByVal sender As Object, ByVal e As System.EventArgs)
CType(sender,Label).Text=HttpUtility.HtmlEncode(Replace(Eval("kyapub").ToString,Chr(10),"<br />"))

End Sub

Protected Sub editctrl000004_Databound(ByVal sender As Object, ByVal e As System.EventArgs)
CType(sender,Label).Text=HttpUtility.HtmlEncode(Replace(Eval("kyaprv").ToString,Chr(10),"<br />"))

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
         Response.Redirect(coWebRootFolder & "hrckya.aspx?_mode_=7&_closea_=0")
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
    Dim auxResult as String=auxClass.gSystemConfig_PostAction("Q_KYA",13,auxDataKeyCod) 
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
    
    Dim auxResult as String=auxClass.gSystemConfig_PostAction("Q_KYA",11,auxDataKeyCod) 
    lblerror.Text &=auxResult
    If auxResult ="" Then
    End If
    auxClass.Conn.gConn_Close
End If

End Sub
Protected Sub frmdatos_ItemSelected(ByVal sender As Object, ByVal e As System.EventArgs)
If frmdatos.DataItem IsNot Nothing Then
   Page.Title = "Claves asimétricas| " &  Databinder.Eval(frmdatos.DataItem ,"kyadsc").ToString
   lblsubtitle.Text = Databinder.Eval(frmdatos.DataItem ,"kyadsc").ToString
Else
   Page.Title = "Claves asimétricas| Nuevo"
   lblsubtitle.Text = "Nuevo"
End If


Select Case Request.QueryString("_mode_")
Case "1","25"
 If LicParam Is Nothing Then
     gForm_Close()
 Else
     Dim auxMax As Integer = Val(LicParam.gValue_Get("Q_KYA.MAX", -1))
     If auxMax >= 0 Then
         Dim auxConn As clsHrcConnClient = Session("conn")
         auxConn.gComponent_CreateInstance()
         auxConn.gConn_Open()
         If Val(auxConn.gConn_QueryValueInt("SELECT COUNT(*) FROM Q_KYA", 0)) > auxMax Then
             gForm_Close()
         End If
         auxConn.gConn_Close()
     End If
 End If
End Select
Select Case Request.QueryString("_mode_")
 Case "1"
 Case "25"
   lblsubtitle.Text = "Copiar"
    Try
        Dim auxConn As New SqlConnection(Session("connectionstringname"))
        auxConn.Open
        Dim auxDA As New SqlDataAdapter("SELECT Q_KYA.kyacod,Q_KYA.kyadsc,Q_KYA.kyapub,Q_KYA.kyaprv,(Q_KYAQSECSID.secdsc) as qsecsiddsc,Q_KYA.qsecdatetime,Q_KYA.qsecsid FROM Q_KYA  LEFT JOIN Q_SECPLOGIN AS Q_KYAQSECSID ON Q_KYAQSECSID.sidcod=Q_KYA.qsecsid  WHERE Q_KYA.kyacod=@param1", auxConn)
        auxDA.SelectCommand.Parameters.Add("@param1", SqlDbType.Int, 4).Value = Request.QueryString("param1")
        Dim auxDT As New DataTable
        auxDA.Fill(auxDT)
        auxConn.Close
        If auxDT.Rows.Count <> 0 Then
             If IsDBNull(auxDT.Rows(0)("kyadsc")) = False Then
             CType(sender.parent.FindControl("frmdatos$insctrl000006"),TextBox).Text=auxDT.Rows(0)("kyadsc")
             End If 
        End If
    Catch ex as sqlException
    End Try
End Select
Select Case Request.QueryString("_mode_")
 Case "1","25"
Dim auxClass As New clscusimDOC
auxClass.Conn.gConn_Open()
Dim auxBagValues As clshrcBagValues = auxClass.gSystem_PostAction_v2(100027,Val(Request.QueryString("_mode_")), -1, Nothing)
If auxBagValues IsNot Nothing Then
     Dim auxValue As Object
End If
auxClass.Conn.gConn_Close()
End Select

Select Case Request.QueryString("_mode_")
 Case "2"
Dim auxClass As New clscusimDOC
auxClass.Conn.gConn_Open()
Dim auxBagValues As clshrcBagValues = auxClass.gSystem_PostAction_v2(100027,Val(Request.QueryString("_mode_")),Databinder.Eval(frmdatos.DataItem ,"kyacod"), Nothing)
If auxBagValues IsNot Nothing Then
     Dim auxValue As Object
End If
auxClass.Conn.gConn_Close()
End Select

Select Case Request.QueryString("_mode_")
 Case "2"
Dim auxClass As New clscusimDOC
auxClass.Conn.gConn_Open()
Dim auxBagValues As clshrcBagValues = auxClass.gSystem_PostAction_v2(100027,Val(Request.QueryString("_mode_")),Databinder.Eval(frmdatos.DataItem ,"kyacod"), Nothing)
If auxBagValues IsNot Nothing Then
     Dim auxValue As Object
End If
auxClass.Conn.gConn_Close()
End Select

Select Case Request.QueryString("_mode_")
 Case "2"
Dim auxClass As New clscusimDOC
auxClass.Conn.gConn_Open()
Dim auxBagValues As clshrcBagValues = auxClass.gSystem_PostAction_v2(100027,Val(Request.QueryString("_mode_")),Databinder.Eval(frmdatos.DataItem ,"kyacod"), Nothing)
If auxBagValues IsNot Nothing Then
     Dim auxValue As Object
End If
auxClass.Conn.gConn_Close()
End Select

Select Case Request.QueryString("_mode_")
 Case "2"
Dim auxClass As New clscusimDOC
auxClass.Conn.gConn_Open()
Dim auxBagValues As clshrcBagValues = auxClass.gSystem_PostAction_v2(100027,Val(Request.QueryString("_mode_")),Databinder.Eval(frmdatos.DataItem ,"kyacod"), Nothing)
If auxBagValues IsNot Nothing Then
     Dim auxValue As Object
End If
auxClass.Conn.gConn_Close()
End Select

Select Case Request.QueryString("_mode_")
 Case "2"
Dim auxClass As New clscusimDOC
auxClass.Conn.gConn_Open()
Dim auxBagValues As clshrcBagValues = auxClass.gSystem_PostAction_v2(100027,Val(Request.QueryString("_mode_")),Databinder.Eval(frmdatos.DataItem ,"kyacod"), Nothing)
If auxBagValues IsNot Nothing Then
     Dim auxValue As Object
End If
auxClass.Conn.gConn_Close()
End Select

Select Case Request.QueryString("_mode_")
 Case "2"
Dim auxClass As New clscusimDOC
auxClass.Conn.gConn_Open()
Dim auxBagValues As clshrcBagValues = auxClass.gSystem_PostAction_v2(100027,Val(Request.QueryString("_mode_")),Databinder.Eval(frmdatos.DataItem ,"kyacod"), Nothing)
If auxBagValues IsNot Nothing Then
     Dim auxValue As Object
End If
auxClass.Conn.gConn_Close()
End Select

Select Case Request.QueryString("_mode_")
 Case "2"
Dim auxClass As New clscusimDOC
auxClass.Conn.gConn_Open()
Dim auxBagValues As clshrcBagValues = auxClass.gSystem_PostAction_v2(100027,Val(Request.QueryString("_mode_")),Databinder.Eval(frmdatos.DataItem ,"kyacod"), Nothing)
If auxBagValues IsNot Nothing Then
     Dim auxValue As Object
End If
auxClass.Conn.gConn_Close()
End Select

Select Case Request.QueryString("_mode_")
 Case "2"
Dim auxClass As New clscusimDOC
auxClass.Conn.gConn_Open()
Dim auxBagValues As clshrcBagValues = auxClass.gSystem_PostAction_v2(100027,Val(Request.QueryString("_mode_")),Databinder.Eval(frmdatos.DataItem ,"kyacod"), Nothing)
If auxBagValues IsNot Nothing Then
     Dim auxValue As Object
End If
auxClass.Conn.gConn_Close()
End Select

Select Case Request.QueryString("_mode_")
 Case "2"
Dim auxClass As New clscusimDOC
auxClass.Conn.gConn_Open()
Dim auxBagValues As clshrcBagValues = auxClass.gSystem_PostAction_v2(100027,Val(Request.QueryString("_mode_")),Databinder.Eval(frmdatos.DataItem ,"kyacod"), Nothing)
If auxBagValues IsNot Nothing Then
     Dim auxValue As Object
End If
auxClass.Conn.gConn_Close()
End Select

End Sub
Protected Sub frmdatos_ItemUpdated(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.FormViewUpdatedEventArgs)
If e.Exception IsNot Nothing Then
  lblerror.Text &="//Error actualizando datos. Vuelva a realizar la operacion!"
     
Else
    Dim auxClass As New clscusimDOC
    auxClass.Conn.gConn_Open
    Dim auxDataKeyCod as Integer= e.Keys(0)
         
    Dim auxResult as String=auxClass.gSystemConfig_PostAction("Q_KYA",12,auxDataKeyCod) 
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
         Response.Redirect(coWebRootFolder & "hrckya.aspx?_mode_=7&_closea_=0")
     End If

End Sub
Protected Sub hrckya_det_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
Dim auxSecurity as clsHrcSecurityClient= Session("security"),clsHrcSecurityClient
Dim auxConnn as clsHrcConnClient= Session("conn"),clsHrcConnClient
Dim auxPermRead as Boolean=False
auxPermRead=True
If auxPermRead=False Then Response.Redirect(System.Web.Hosting.HostingEnvironment.ApplicationVirtualPath)

If m_PermFormEdit=False Then m_PermFormEdit=Session("isadmin") OR auxSecurity.gSID_CheckAccess( 1)
If m_PermFormEdit=False Then Response.Redirect(System.Web.Hosting.HostingEnvironment.ApplicationVirtualPath)
If m_PermFormNew=False Then m_PermFormNew=Session("isadmin") OR auxSecurity.gSID_CheckAccess(1)
If m_PermFormDelete=False Then m_PermFormDelete=Session("isadmin") OR auxSecurity.gSID_CheckAccess(1)
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

