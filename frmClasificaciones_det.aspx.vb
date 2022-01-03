Imports System.Data
Imports System.Data.SqlClient
Imports Intelimedia.imComponentes
Public Class frmClasificaciones_det
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
CType(sender,Label).Text=Replace(Eval("abrev").ToString,Chr(10),"<br />")

End Sub

Protected Sub itemctrl000004_Databound(ByVal sender As Object, ByVal e As System.EventArgs)
If IsDBNull(Eval("notitle")) Then
CType(sender,Label).Text &=""
ElseIf Eval("notitle") Then
CType(sender,Label).Text &="<img src=imagenes/actexception.png alt=Excepcion width=12px border=0px />"
Else
CType(sender,Label).Text &=""
End If

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

Protected Sub editctrl000006_DataBound_frmdatos(ByVal sender As Object, ByVal e As System.EventArgs)
CType(sender,TextBox).Text=Eval("dsc").ToString

End Sub

Protected Sub editctrl000005_DataBound_frmdatos(ByVal sender As Object, ByVal e As System.EventArgs)
CType(sender,TextBox).Text=Eval("abrev").ToString

End Sub

Protected Sub editctrl000004_Databound(ByVal sender As Object, ByVal e As System.EventArgs)
    If IsDBNull(DataBinder.Eval(frmdatos.DataItem,"notitle"))  Then
        CType(sender,CheckBox).Checked=False
    Else
        CType(sender,CheckBox).Checked=DataBinder.Eval(frmdatos.DataItem,"notitle")
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
         Response.Redirect(Session("ROOTWEBURL") & "frmClasificaciones.aspx?_mode_=7&_closea_=0")
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
    Dim auxResult as String=auxClass.gSystem_PostAction(11,13,auxDataKeyCod) 
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
    
    Dim auxResult as String=auxClass.gSystem_PostAction(11,11,auxDataKeyCod) 
    lblerror.Text &=auxResult
    If auxResult ="" Then
    End If
    auxClass.Conn.gConn_Close
End If

End Sub
Protected Sub frmdatos_ItemSelected(ByVal sender As Object, ByVal e As System.EventArgs)
If frmdatos.DataItem IsNot Nothing Then
   Page.Title = "Clasificaciones| " &  Databinder.Eval(frmdatos.DataItem ,"dsc").ToString
   lblsubtitle.Text = Databinder.Eval(frmdatos.DataItem ,"dsc").ToString
Else
   Page.Title = "Clasificaciones| Nuevo"
   lblsubtitle.Text = "Nuevo"
End If


If sender.FindControl("cmdFormViewConfirmDelete") IsNot Nothing Then
sender.FindControl("cmdFormViewConfirmDelete").Visible=m_PermFormDelete
End If

Select Case Request.QueryString("_mode_")
Case "1","25"
 If LicParam Is Nothing Then
     gForm_Close()
 Else
     Dim auxMax As Integer = Val(LicParam.gValue_Get("DOC_CLA.MAX", -1))
     If auxMax >= 0 Then
         Dim auxConn As clsHrcConnClient = Session("conn")
         auxConn.gComponent_CreateInstance()
         auxConn.gConn_Open()
         If Val(auxConn.gConn_QueryValueInt("SELECT COUNT(*) FROM DOC_CLA", 0)) > auxMax Then
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
        Dim auxDA As New SqlDataAdapter("SELECT DOC_CLA.cod,DOC_CLA.dsc,DOC_CLA.abrev,DOC_CLA.notitle,DOC_CLA.baja,(DOC_CLAQSECSID.secdsc) as qsecsiddsc,DOC_CLA.qsecdatetime,DOC_CLA.qsecsid FROM DOC_CLA  LEFT JOIN Q_SECPLOGIN AS DOC_CLAQSECSID ON DOC_CLAQSECSID.sidcod=DOC_CLA.qsecsid  WHERE (DOC_CLA.cod=@param1) AND (DOC_CLA.baja = '1900-01-01T00:00:00' OR DOC_CLA.baja  IS NULL)", auxConn)
        auxDA.SelectCommand.Parameters.Add("@param1", SqlDbType.Int, 4).Value = Request.QueryString("param1")
        Dim auxDT As New DataTable
        auxDA.Fill(auxDT)
        auxConn.Close
        If auxDT.Rows.Count <> 0 Then
             If IsDBNull(auxDT.Rows(0)("dsc")) = False Then
             CType(sender.parent.FindControl("frmdatos$insctrl000006"),TextBox).Text=auxDT.Rows(0)("dsc")
             End If 
             If IsDBNull(auxDT.Rows(0)("abrev")) = False Then
             CType(sender.parent.FindControl("frmdatos$insctrl000005"),TextBox).Text=auxDT.Rows(0)("abrev")
             End If 
             If IsDBNull(auxDT.Rows(0)("notitle")) = False Then
             CType(sender.parent.FindControl("frmdatos$insctrl000004"),CheckBox).Checked=auxDT.Rows(0)("notitle")
             End If 
        End If
    Catch ex as sqlException
    End Try
End Select
Select Case Request.QueryString("_mode_")
 Case "1","25"
Dim auxClass As New clscusimDOC
auxClass.Conn.gConn_Open()
Dim auxBagValues As clshrcBagValues = auxClass.gSystem_PostAction_v2(11,Val(Request.QueryString("_mode_")), -1, Nothing)
If auxBagValues IsNot Nothing Then
     Dim auxValue As Object
End If
auxClass.Conn.gConn_Close()
End Select

Select Case Request.QueryString("_mode_")
 Case "2"
Dim auxClass As New clscusimDOC
auxClass.Conn.gConn_Open()
Dim auxBagValues As clshrcBagValues = auxClass.gSystem_PostAction_v2(11,Val(Request.QueryString("_mode_")),Databinder.Eval(frmdatos.DataItem ,"cod"), Nothing)
If auxBagValues IsNot Nothing Then
     Dim auxValue As Object
End If
auxClass.Conn.gConn_Close()
End Select

Select Case Request.QueryString("_mode_")
 Case "2"
Dim auxClass As New clscusimDOC
auxClass.Conn.gConn_Open()
Dim auxBagValues As clshrcBagValues = auxClass.gSystem_PostAction_v2(11,Val(Request.QueryString("_mode_")),Databinder.Eval(frmdatos.DataItem ,"cod"), Nothing)
If auxBagValues IsNot Nothing Then
     Dim auxValue As Object
End If
auxClass.Conn.gConn_Close()
End Select

Select Case Request.QueryString("_mode_")
 Case "2"
Dim auxClass As New clscusimDOC
auxClass.Conn.gConn_Open()
Dim auxBagValues As clshrcBagValues = auxClass.gSystem_PostAction_v2(11,Val(Request.QueryString("_mode_")),Databinder.Eval(frmdatos.DataItem ,"cod"), Nothing)
If auxBagValues IsNot Nothing Then
     Dim auxValue As Object
End If
auxClass.Conn.gConn_Close()
End Select

Select Case Request.QueryString("_mode_")
 Case "2"
Dim auxClass As New clscusimDOC
auxClass.Conn.gConn_Open()
Dim auxBagValues As clshrcBagValues = auxClass.gSystem_PostAction_v2(11,Val(Request.QueryString("_mode_")),Databinder.Eval(frmdatos.DataItem ,"cod"), Nothing)
If auxBagValues IsNot Nothing Then
     Dim auxValue As Object
End If
auxClass.Conn.gConn_Close()
End Select

Select Case Request.QueryString("_mode_")
 Case "2"
Dim auxClass As New clscusimDOC
auxClass.Conn.gConn_Open()
Dim auxBagValues As clshrcBagValues = auxClass.gSystem_PostAction_v2(11,Val(Request.QueryString("_mode_")),Databinder.Eval(frmdatos.DataItem ,"cod"), Nothing)
If auxBagValues IsNot Nothing Then
     Dim auxValue As Object
End If
auxClass.Conn.gConn_Close()
End Select

Select Case Request.QueryString("_mode_")
 Case "2"
Dim auxClass As New clscusimDOC
auxClass.Conn.gConn_Open()
Dim auxBagValues As clshrcBagValues = auxClass.gSystem_PostAction_v2(11,Val(Request.QueryString("_mode_")),Databinder.Eval(frmdatos.DataItem ,"cod"), Nothing)
If auxBagValues IsNot Nothing Then
     Dim auxValue As Object
End If
auxClass.Conn.gConn_Close()
End Select

Select Case Request.QueryString("_mode_")
 Case "2"
Dim auxClass As New clscusimDOC
auxClass.Conn.gConn_Open()
Dim auxBagValues As clshrcBagValues = auxClass.gSystem_PostAction_v2(11,Val(Request.QueryString("_mode_")),Databinder.Eval(frmdatos.DataItem ,"cod"), Nothing)
If auxBagValues IsNot Nothing Then
     Dim auxValue As Object
End If
auxClass.Conn.gConn_Close()
End Select

Select Case Request.QueryString("_mode_")
 Case "2"
Dim auxClass As New clscusimDOC
auxClass.Conn.gConn_Open()
Dim auxBagValues As clshrcBagValues = auxClass.gSystem_PostAction_v2(11,Val(Request.QueryString("_mode_")),Databinder.Eval(frmdatos.DataItem ,"cod"), Nothing)
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
         
    Dim auxResult as String=auxClass.gSystem_PostAction(11,12,auxDataKeyCod) 
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
         Response.Redirect(Session("ROOTWEBURL") & "frmClasificaciones.aspx?_mode_=7&_closea_=0")
     End If

End Sub
Protected Sub frmClasificaciones_det_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
Dim auxSecurity as clsHrcSecurityClient=CType( Session("security"),clsHrcSecurityClient)
Dim auxConnn as clsHrcConnClient= CType(Session("conn"),clsHrcConnClient)
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

