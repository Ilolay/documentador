Imports System.Data
Imports System.Data.SqlClient
Imports Intelimedia.imComponentes
Public Class frmFuncionesyequipos_det
Inherits System.Web.UI.Page
Friend m_PermFormDelete as Boolean=False
Friend m_PermFormEdit as Boolean=False
Friend m_PermFormNew as Boolean=False
Protected Sub itemctrl000009_Databound(ByVal sender As Object, ByVal e As System.EventArgs)
CType(sender,Label).Text=Eval("cod").ToString

End Sub

Protected Sub itemctrl000007_Databound(ByVal sender As Object, ByVal e As System.EventArgs)
CType(sender,Label).Text=Replace(Eval("dsc").ToString,Chr(10),"<br />")

End Sub

Protected Sub itemctrl000006view_DataBinding(ByVal sender As Object, ByVal e As System.EventArgs)
Dim auxValue as Int32=-1
If IsDBNull(Eval("miembrosgrpcod")) Then 
     Ctype(sender,Control).Visible=False
Else
  If Eval("miembrosgrpcod") < 1 Then 
     Ctype(sender,Control).Visible=False
  End If
End If

End Sub
Protected Sub itemctrl000006_Databound(ByVal sender As Object, ByVal e As System.EventArgs)
If IsDBNull(Eval("miembrosgrpcod")) Then
 CType(sender,HiddenField).Value=""
Else
 CType(sender,HiddenField).Value=Eval("miembrosgrpcod").ToString()
End If

End Sub

Protected Sub itemctrl000004colitem1view_DataBinding(ByVal sender As Object, ByVal e As System.EventArgs)
Dim auxValue as Int32=-1
If IsDBNull(Eval("mbrtypecodvalue")) Then 
     Ctype(sender,Control).Visible=False
Else
objectexplorer_SetValue(Eval("mbrtypecodvalue"),Eval("mbrtypecod"),Eval("mbrtypecoddsc").ToString,CType(sender.parent.FindControl("itemctrl000004colitem1view"),LinkButton),CType(sender.parent.FindControl("itemctrl000004colitem1value"),HiddenField),CType(sender.parent.FindControl("itemctrl000004colitem1"),HiddenField),Nothing)
End If

End Sub
Protected Sub itemctrl000004colitem1value_Databound(ByVal sender As Object, ByVal e As System.EventArgs)
If IsDBNull(Eval("mbrtypecodvalue")) Then
 CType(sender,HiddenField).Value=""
Else
 CType(sender,HiddenField).Value=Eval("mbrtypecodvalue").ToString()
End If

End Sub

Protected Sub itemctrl000004_ItemDeleted(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewCommandEventArgs)

End Sub
Protected Sub itemctrl000004_ItemInserted(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewCommandEventArgs)

End Sub
Protected Sub itemctrl000004_ItemSelected(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewCommandEventArgs)

End Sub
Protected Sub itemctrl000004_ItemUpdated(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewCommandEventArgs)

End Sub
Protected Sub itemctrl000004_RowCommand(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewCommandEventArgs)
Select Case e.CommandName.ToUpper()
End Select

End Sub
Protected Sub itemctrl000004_RowCreated(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs)
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

Protected Sub editctrl000007_DataBound_frmdatos(ByVal sender As Object, ByVal e As System.EventArgs)
CType(sender,TextBox).Text=Eval("dsc").ToString

End Sub

Protected Sub editctrl000006view_DataBinding(ByVal sender As Object, ByVal e As System.EventArgs)
Dim auxValue as Int32=-1
If IsDBNull(Eval("miembrosgrpcod")) Then 
     Ctype(sender,Control).Visible=False
Else
  If Eval("miembrosgrpcod") < 1 Then 
     Ctype(sender,Control).Visible=False
  End If
End If

End Sub
Protected Sub editctrl000006_Databound(ByVal sender As Object, ByVal e As System.EventArgs)
If IsDBNull(Eval("miembrosgrpcod")) Then
 CType(sender,HiddenField).Value=""
Else
 CType(sender,HiddenField).Value=Eval("miembrosgrpcod").ToString()
End If

End Sub

Protected Sub editctrl000004colitem1view_DataBinding(ByVal sender As Object, ByVal e As System.EventArgs)
Dim auxValue as Int32=-1
If IsDBNull(Eval("mbrtypecodvalue")) Then 
     Ctype(sender,Control).Visible=False
Else
objectexplorer_SetValue(Eval("mbrtypecodvalue"),Eval("mbrtypecod"),Eval("mbrtypecoddsc").ToString,CType(sender.parent.FindControl("editctrl000004colitem1view"),LinkButton),CType(sender.parent.FindControl("editctrl000004colitem1value"),HiddenField),CType(sender.parent.FindControl("editctrl000004colitem1"),HiddenField),Nothing)
End If

End Sub
Protected Sub editctrl000004colitem1value_Databound(ByVal sender As Object, ByVal e As System.EventArgs)
If IsDBNull(Eval("mbrtypecodvalue")) Then
 CType(sender,HiddenField).Value=""
Else
 CType(sender,HiddenField).Value=Eval("mbrtypecodvalue").ToString()
End If

End Sub

Protected Sub editctrl000004_ItemDeleted(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewCommandEventArgs)

End Sub
Protected Sub editctrl000004_ItemInserted(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewCommandEventArgs)

End Sub
Protected Sub editctrl000004_ItemSelected(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewCommandEventArgs)

End Sub
Protected Sub editctrl000004_ItemUpdated(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewCommandEventArgs)

End Sub
Protected Sub editctrl000004_RowCommand(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewCommandEventArgs)
Select Case e.CommandName.ToUpper()
  Case "SELECT"
          dsfrmupdpanelctrl000004.SelectParameters(frmupdpanelfrmupdpanelctrl000004.DataKeyNames(0)).DefaultValue=e.CommandArgument
          frmupdpanelfrmupdpanelctrl000004.ChangeMode(FormViewMode.Edit)
          frmupdpanelfrmupdpanelctrl000004.DataBind()
      lblfrmupdpanelctrl000004subtitle.Text="Visualizacion de Funciones-Miembros"
          updpanelfrmupdpanelctrl000004.Show()
          updupdpanelfrmupdpanelctrl000004.Update()
  Case "UPDATE"
  Case "EDIT"
          dsfrmupdpanelctrl000004.SelectParameters(frmupdpanelfrmupdpanelctrl000004.DataKeyNames(0)).DefaultValue=e.CommandArgument
          frmupdpanelfrmupdpanelctrl000004.ChangeMode(FormViewMode.Edit)
          frmupdpanelfrmupdpanelctrl000004.DataBind()
      lblfrmupdpanelctrl000004subtitle.Text="Edición de Funciones-Miembros"
          updpanelfrmupdpanelctrl000004.Show()
          updupdpanelfrmupdpanelctrl000004.Update()
  Case "HRC_INSERT"
          frmupdpanelfrmupdpanelctrl000004.ChangeMode(FormViewMode.Insert)
      lblfrmupdpanelctrl000004subtitle.Text="Nuevo en Funciones-Miembros"
      updpanelfrmupdpanelctrl000004.Show()
          updupdpanelfrmupdpanelctrl000004.Update()
End Select

End Sub
Protected Sub editctrl000004_RowCreated(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs)
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
         Response.Redirect(Session("ROOTWEBURL") & "frmFuncionesyequipos.aspx?_mode_=7&_closea_=0")
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
    Dim auxResult as String=auxClass.gSystem_PostAction(14,13,auxDataKeyCod) 
    auxClass.gCluster_LoadStaticTables_DOC_EQU(auxDataKeyCod) 
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
    
    Dim auxResult as String=auxClass.gSystem_PostAction(14,11,auxDataKeyCod) 
    auxClass.gCluster_LoadStaticTables_DOC_EQU(auxDataKeyCod) 
    lblerror.Text &=auxResult
    If auxResult ="" Then
    End If
    auxClass.Conn.gConn_Close
End If

End Sub
Protected Sub frmdatos_ItemSelected(ByVal sender As Object, ByVal e As System.EventArgs)
If frmdatos.DataItem IsNot Nothing Then
   Page.Title = "Funciones y equipos| " &  Databinder.Eval(frmdatos.DataItem ,"dsc").ToString
   lblsubtitle.Text = Databinder.Eval(frmdatos.DataItem ,"dsc").ToString
Else
   Page.Title = "Funciones y equipos| Nuevo"
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
     Dim auxMax As Integer = Val(LicParam.gValue_Get("DOC_EQU.MAX", -1))
     If auxMax >= 0 Then
         Dim auxConn As clsHrcConnClient = Session("conn")
         auxConn.gComponent_CreateInstance()
         auxConn.gConn_Open()
         If Val(auxConn.gConn_QueryValueInt("SELECT COUNT(*) FROM DOC_EQU", 0)) > auxMax Then
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
        Dim auxDA As New SqlDataAdapter("SELECT DOC_EQU.cod,DOC_EQU.dsc,(DOC_EQUUNDCOD.dsc) as undcoddsc,(DOC_EQUMIEMBROSGRPCOD.grpdsc) as miembrosgrpcoddsc,DOC_EQU.baja,(DOC_EQUQSECSID.secdsc) as qsecsiddsc,DOC_EQU.qsecdatetime,DOC_EQU.undcod,DOC_EQU.miembrosgrpcod,DOC_EQU.qsecsid FROM DOC_EQU  LEFT JOIN Q_SECPGRP AS DOC_EQUMIEMBROSGRPCOD ON DOC_EQUMIEMBROSGRPCOD.grpcod=DOC_EQU.miembrosgrpcod LEFT JOIN UND AS DOC_EQUUNDCOD ON DOC_EQUUNDCOD.cod=DOC_EQU.undcod LEFT JOIN Q_SECPLOGIN AS DOC_EQUQSECSID ON DOC_EQUQSECSID.sidcod=DOC_EQU.qsecsid  WHERE (DOC_EQU.cod=@param1) AND (DOC_EQU.baja = 0 OR DOC_EQU.baja  IS NULL)", auxConn)
        auxDA.SelectCommand.Parameters.Add("@param1", SqlDbType.Int, 4).Value = Request.QueryString("param1")
        Dim auxDT As New DataTable
        auxDA.Fill(auxDT)
        auxConn.Close
        If auxDT.Rows.Count <> 0 Then
             If IsDBNull(auxDT.Rows(0)("dsc")) = False Then
             CType(sender.parent.FindControl("frmdatos$insctrl000007"),TextBox).Text=auxDT.Rows(0)("dsc")
             End If 
        End If
    Catch ex as sqlException
    End Try
End Select
Select Case Request.QueryString("_mode_")
 Case "1","25"
Dim auxClass As New clscusimDOC
auxClass.Conn.gConn_Open()
Dim auxBagValues As clshrcBagValues = auxClass.gSystem_PostAction_v2(14,Val(Request.QueryString("_mode_")), -1, Nothing)
If auxBagValues IsNot Nothing Then
     Dim auxValue As Object
End If
auxClass.Conn.gConn_Close()
End Select

Select Case Request.QueryString("_mode_")
 Case "2"
Dim auxClass As New clscusimDOC
auxClass.Conn.gConn_Open()
Dim auxBagValues As clshrcBagValues = auxClass.gSystem_PostAction_v2(14,Val(Request.QueryString("_mode_")),Databinder.Eval(frmdatos.DataItem ,"cod"), Nothing)
If auxBagValues IsNot Nothing Then
     Dim auxValue As Object
End If
auxClass.Conn.gConn_Close()
End Select

Select Case Request.QueryString("_mode_")
 Case "2"
Dim auxClass As New clscusimDOC
auxClass.Conn.gConn_Open()
Dim auxBagValues As clshrcBagValues = auxClass.gSystem_PostAction_v2(14,Val(Request.QueryString("_mode_")),Databinder.Eval(frmdatos.DataItem ,"cod"), Nothing)
If auxBagValues IsNot Nothing Then
     Dim auxValue As Object
End If
auxClass.Conn.gConn_Close()
End Select

Select Case Request.QueryString("_mode_")
 Case "2"
Dim auxClass As New clscusimDOC
auxClass.Conn.gConn_Open()
Dim auxBagValues As clshrcBagValues = auxClass.gSystem_PostAction_v2(14,Val(Request.QueryString("_mode_")),Databinder.Eval(frmdatos.DataItem ,"cod"), Nothing)
If auxBagValues IsNot Nothing Then
     Dim auxValue As Object
End If
auxClass.Conn.gConn_Close()
End Select

Select Case Request.QueryString("_mode_")
 Case "2"
Dim auxClass As New clscusimDOC
auxClass.Conn.gConn_Open()
Dim auxBagValues As clshrcBagValues = auxClass.gSystem_PostAction_v2(14,Val(Request.QueryString("_mode_")),Databinder.Eval(frmdatos.DataItem ,"cod"), Nothing)
If auxBagValues IsNot Nothing Then
     Dim auxValue As Object
End If
auxClass.Conn.gConn_Close()
End Select

Select Case Request.QueryString("_mode_")
 Case "2"
Dim auxClass As New clscusimDOC
auxClass.Conn.gConn_Open()
Dim auxBagValues As clshrcBagValues = auxClass.gSystem_PostAction_v2(14,Val(Request.QueryString("_mode_")),Databinder.Eval(frmdatos.DataItem ,"cod"), Nothing)
If auxBagValues IsNot Nothing Then
     Dim auxValue As Object
End If
auxClass.Conn.gConn_Close()
End Select

Select Case Request.QueryString("_mode_")
 Case "2"
Dim auxClass As New clscusimDOC
auxClass.Conn.gConn_Open()
Dim auxBagValues As clshrcBagValues = auxClass.gSystem_PostAction_v2(14,Val(Request.QueryString("_mode_")),Databinder.Eval(frmdatos.DataItem ,"cod"), Nothing)
If auxBagValues IsNot Nothing Then
     Dim auxValue As Object
End If
auxClass.Conn.gConn_Close()
End Select

Select Case Request.QueryString("_mode_")
 Case "2"
Dim auxClass As New clscusimDOC
auxClass.Conn.gConn_Open()
Dim auxBagValues As clshrcBagValues = auxClass.gSystem_PostAction_v2(14,Val(Request.QueryString("_mode_")),Databinder.Eval(frmdatos.DataItem ,"cod"), Nothing)
If auxBagValues IsNot Nothing Then
     Dim auxValue As Object
End If
auxClass.Conn.gConn_Close()
End Select

Select Case Request.QueryString("_mode_")
 Case "2"
Dim auxClass As New clscusimDOC
auxClass.Conn.gConn_Open()
Dim auxBagValues As clshrcBagValues = auxClass.gSystem_PostAction_v2(14,Val(Request.QueryString("_mode_")),Databinder.Eval(frmdatos.DataItem ,"cod"), Nothing)
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
         
    Dim auxResult as String=auxClass.gSystem_PostAction(14,12,auxDataKeyCod) 
    auxClass.gCluster_LoadStaticTables_DOC_EQU(auxDataKeyCod) 
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

Protected Sub itemdsctrl000004_Init(ByVal sender As Object,ByVal e As System.EventArgs)
sender.ConnectionString=Session("connectionstringname")
End Sub
Protected Sub itemdsctrl000004_Deleted(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.SqlDataSourceStatusEventArgs)
If e.Exception IsNot Nothing Then
  lblerror.Text &="//Error borrando datos (" & e.Exception.Message & ")"
    
Else
    
End If

End Sub
Protected Sub itemdsctrl000004_Inserted(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.SqlDataSourceStatusEventArgs)
If e.Exception IsNot Nothing Then
  lblerror.Text &="//Error insertando datos (" & e.Exception.Message & ")"
    
Else
    
End If

End Sub
Protected Sub itemdsctrl000004_Selected(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.SqlDataSourceStatusEventArgs)
If e.Exception IsNot Nothing Then
  lblerror.Text &="//Error buscando datos (" & e.Exception.Message & ")"
    
Else
    
End If

End Sub
Protected Sub itemdsctrl000004_Updated(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.SqlDataSourceStatusEventArgs)
If e.Exception IsNot Nothing Then
  lblerror.Text &="//Error actualizando datos (" & e.Exception.Message & ")"
    
Else
    
End If

End Sub

Protected Sub editdsctrl000004_Init(ByVal sender As Object,ByVal e As System.EventArgs)
sender.ConnectionString=Session("connectionstringname")
End Sub
Protected Sub editdsctrl000004_Deleted(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.SqlDataSourceStatusEventArgs)
If e.Exception IsNot Nothing Then
  lblerror.Text &="//Error borrando datos (" & e.Exception.Message & ")"
    
Else
      Dim auxClass As New clscusimDOC
  auxClass.gConn_Open()
  Dim auxResult as String=auxClass.gSystem_PostAction(18,13,e.Command.Parameters("@cod").Value) 
  auxClass.Conn.gConn_Close()

End If

End Sub
Protected Sub editdsctrl000004_Inserted(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.SqlDataSourceStatusEventArgs)
If e.Exception IsNot Nothing Then
  lblerror.Text &="//Error insertando datos (" & e.Exception.Message & ")"
    
Else
    sender.InsertParameters("querynextcod").DefaultValue = e.Command.Parameters("@querynextcod").Value
End If

End Sub
Protected Sub editdsctrl000004_Selected(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.SqlDataSourceStatusEventArgs)
If e.Exception IsNot Nothing Then
  lblerror.Text &="//Error buscando datos (" & e.Exception.Message & ")"
    
Else
    
End If

End Sub
Protected Sub editdsctrl000004_Updated(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.SqlDataSourceStatusEventArgs)
If e.Exception IsNot Nothing Then
  lblerror.Text &="//Error actualizando datos (" & e.Exception.Message & ")"
    
Else
    
End If

End Sub

Protected Sub dsedtmbrtypecod_Init(ByVal sender As Object,ByVal e As System.EventArgs)
sender.ConnectionString=Session("connectionstringname")
End Sub
Protected Sub dsedtmbrtypecod_Deleted(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.SqlDataSourceStatusEventArgs)
If e.Exception IsNot Nothing Then
    
Else
    
End If

End Sub
Protected Sub dsedtmbrtypecod_Inserted(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.SqlDataSourceStatusEventArgs)
If e.Exception IsNot Nothing Then
    
Else
    
End If

End Sub
Protected Sub dsedtmbrtypecod_Selected(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.SqlDataSourceStatusEventArgs)
If e.Exception IsNot Nothing Then
    
Else
    
End If

End Sub
Protected Sub dsedtmbrtypecod_Updated(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.SqlDataSourceStatusEventArgs)
If e.Exception IsNot Nothing Then
    
Else
    
End If

End Sub

Protected Sub edtmbrtypecodview_DataBinding(ByVal sender As Object, ByVal e As System.EventArgs)
Dim auxValue as Int32=-1
If IsDBNull(Eval("mbrtypecodvalue")) Then 
     Ctype(sender,Control).Visible=False
Else
objectexplorer_SetValue(Eval("mbrtypecodvalue"),Eval("mbrtypecod"),Eval("mbrtypecoddsc").ToString,CType(sender.parent.FindControl("edtmbrtypecodview"),LinkButton),CType(sender.parent.FindControl("edtmbrtypecodvalue"),HiddenField),CType(sender.parent.FindControl("edtmbrtypecod"),HiddenField),edtmbrtypecoddelete)
End If
If  Eval("mbrtypecod")=10
 Dim auxConn As clsHrcConnClient = Session("conn")
 auxConn = auxConn.gComponent_CreateInstance
 auxConn.gConn_Open()
 Dim auxDT As DataTable = auxConn.gConn_Query("SELECT * FROM DOC_EQUMBRUND WHERE equmbrcod = " & Eval("cod"))
 If auxDT.Rows.Count <> 0 Then
     If IsDBNull(auxDT.Rows(0)("grupombrdir"))=False Then sender.findcontrol("edtmbrtypecodvalue_10_grupombrdir").Checked= auxDT.Rows(0)("grupombrdir")
If IsDBNull(auxDT.Rows(0)("grupoprjver"))=False Then sender.findcontrol("edtmbrtypecodvalue_10_grupoprjver").Checked= auxDT.Rows(0)("grupoprjver")
If IsDBNull(auxDT.Rows(0)("grupoeditores"))=False Then sender.findcontrol("edtmbrtypecodvalue_10_grupoeditores").Checked= auxDT.Rows(0)("grupoeditores")
If IsDBNull(auxDT.Rows(0)("grupomiembros"))=False Then sender.findcontrol("edtmbrtypecodvalue_10_grupomiembros").Checked= auxDT.Rows(0)("grupomiembros")
If IsDBNull(auxDT.Rows(0)("gruporesp"))=False Then sender.findcontrol("edtmbrtypecodvalue_10_gruporesp").Checked= auxDT.Rows(0)("gruporesp")

 End If
 auxConn.gConn_Close()
End If


End Sub
Protected Sub edtmbrtypecoddelete_Click (ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs)
edtmbrtypecod.Value=-1
edtmbrtypecodvalue.Value=-1
edtmbrtypecodview.Visible=False
edtmbrtypecoddelete.Visible=False

End Sub
Protected Sub edtmbrtypecoddelete_DataBinding(ByVal sender As Object, ByVal e As System.EventArgs)
If Not IsPostBack  Then 
 If IsDBNull(Eval("mbrtypecodvalue")) Then 
    CType(sender,Control).Visible=False
  ElseIf Eval("mbrtypecodvalue") < 1 Then
    CType(sender,Control).Visible=False
  Else
    CType(sender,Control).Visible=True
 End If
End If

End Sub
Protected Sub cmdedtmbrtypecodshowpanel_Click (ByVal sender As Object, ByVal e As System.EventArgs)
objectexplorer_Show(CType(sender.parent.FindControl("edtmbrtypecodvalue"),HiddenField).UniqueID,CType(sender.parent.FindControl("edtmbrtypecodview"),LinkButton).UniqueID,CType(sender.parent.FindControl("edtmbrtypecod"),HiddenField).UniqueID,"10{#CHR34#}","9{#CHR34#}10{#CHR34#}",CType(sender.parent.FindControl("edtmbrtypecoddelete"),ImageButton).UniqueID)
End Sub
Protected Sub valbcbf2015c2f04633ab432f34dceeb656_OnServerValidate(ByVal sender As Object, ByVal args As ServerValidateEventArgs)
If Val(edtmbrtypecodvalue.Value) < 1 Then args.IsValid = False

End Sub

Protected Sub edtmbrtypecodvalue_Databound(ByVal sender As Object, ByVal e As System.EventArgs)
If IsDBNull(Eval("mbrtypecodvalue")) Then
 CType(sender,HiddenField).Value=""
Else
 CType(sender,HiddenField).Value=Eval("mbrtypecodvalue").ToString()
End If

End Sub
Protected Sub edtmbrtypecodvalue_10_Load(ByVal sender As Object, ByVal e As System.EventArgs)
sender.Parent.FindControl("pnledtmbrtypecodvalue_10").Visible=False

End Sub

Protected Sub cmdfrmupdpanelctrl000004updcancel_Click (ByVal sender As Object, ByVal e As System.EventArgs)
updpanelfrmupdpanelctrl000004.Hide()
End Sub

Protected Sub cmdfrmupdpanelctrl000004updconfirm_Click (ByVal sender As Object, ByVal e As System.EventArgs)
Dim auxEndOK as Boolean=True
Try
updpanelfrmupdpanelctrl000004.Hide()
Dim auxPageIndex as Integer=editctrl000004.PageIndex
editctrl000004.DataBind
editctrl000004.PageIndex = auxPageIndex
If CType(editctrl000004.Parent.Parent, UpdatePanel).UpdateMode = UpdatePanelUpdateMode.Conditional Then
  CType(editctrl000004.Parent.Parent, UpdatePanel).Update()
End If

frmupdpanelfrmupdpanelctrl000004.DefaultMode=FormViewMode.Edit
frmupdpanelfrmupdpanelctrl000004.UpdateItem(True)
Catch ex as sqlException
  auxEndOK=False
lblfrmupdpanelctrl000004error.Text &= "//Error de actualizacion (" & ex.Message & ")"
End Try

End Sub

Protected Sub dsinsmbrtypecod_Init(ByVal sender As Object,ByVal e As System.EventArgs)
sender.ConnectionString=Session("connectionstringname")
End Sub
Protected Sub dsinsmbrtypecod_Deleted(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.SqlDataSourceStatusEventArgs)
If e.Exception IsNot Nothing Then
    
Else
    
End If

End Sub
Protected Sub dsinsmbrtypecod_Inserted(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.SqlDataSourceStatusEventArgs)
If e.Exception IsNot Nothing Then
    
Else
    
End If

End Sub
Protected Sub dsinsmbrtypecod_Selected(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.SqlDataSourceStatusEventArgs)
If e.Exception IsNot Nothing Then
    
Else
    
End If

End Sub
Protected Sub dsinsmbrtypecod_Updated(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.SqlDataSourceStatusEventArgs)
If e.Exception IsNot Nothing Then
    
Else
    
End If

End Sub

Protected Sub insmbrtypecoddelete_Click (ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs)
insmbrtypecod.Value=-1
insmbrtypecodvalue.Value=-1
insmbrtypecodview.Visible=False
insmbrtypecoddelete.Visible=False

End Sub
Protected Sub insmbrtypecoddelete_DataBinding(ByVal sender As Object, ByVal e As System.EventArgs)
If Not IsPostBack  Then 
 If IsDBNull(Eval("mbrtypecodvalue")) Then 
    CType(sender,Control).Visible=False
  ElseIf Eval("mbrtypecodvalue") < 1 Then
    CType(sender,Control).Visible=False
  Else
    CType(sender,Control).Visible=True
 End If
End If

End Sub
Protected Sub cmdinsmbrtypecodshowpanel_Click (ByVal sender As Object, ByVal e As System.EventArgs)
objectexplorer_Show(CType(sender.parent.FindControl("insmbrtypecodvalue"),HiddenField).UniqueID,CType(sender.parent.FindControl("insmbrtypecodview"),LinkButton).UniqueID,CType(sender.parent.FindControl("insmbrtypecod"),HiddenField).UniqueID,"10{#CHR34#}","9{#CHR34#}10{#CHR34#}",CType(sender.parent.FindControl("insmbrtypecoddelete"),ImageButton).UniqueID)
End Sub
Protected Sub insmbrtypecodvalue_10_Load(ByVal sender As Object, ByVal e As System.EventArgs)
sender.Parent.FindControl("pnlinsmbrtypecodvalue_10").Visible=False

End Sub

Protected Sub cmdfrmupdpanelctrl000004cancel_Click (ByVal sender As Object, ByVal e As System.EventArgs)
updpanelfrmupdpanelctrl000004.Hide()
End Sub

Protected Sub cmdfrmupdpanelctrl000004insert_Click (ByVal sender As Object, ByVal e As System.EventArgs)
Dim auxEndOK as Boolean=True
Try
frmupdpanelfrmupdpanelctrl000004.InsertItem(False)
updpanelfrmupdpanelctrl000004.Hide()
Dim auxPageIndex as Integer=editctrl000004.PageIndex
editctrl000004.DataBind
editctrl000004.PageIndex = auxPageIndex
If CType(editctrl000004.Parent.Parent, UpdatePanel).UpdateMode = UpdatePanelUpdateMode.Conditional Then
  CType(editctrl000004.Parent.Parent, UpdatePanel).Update()
End If
Catch ex as sqlException
  auxEndOK=False
lblfrmupdpanelctrl000004error.Text &= "//Error de actualizacion (" & ex.Message & ")"
End Try

End Sub

Protected Sub frmupdpanelfrmupdpanelctrl000004_ItemInserted(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.FormViewInsertedEventArgs)
If e.Exception IsNot Nothing Then
    lblfrmupdpanelctrl000004error.Text &="//Error insertando nuevos datos. Vuelva a realizar la operacion!"
    
Else
    Dim auxDataKeyCod as Integer=CInt(dsfrmupdpanelctrl000004.InsertParameters("querynextcod").DefaultValue)
    Dim auxClass As New clscusimDOC
    auxClass.Conn.gConn_Open
    auxClass.Conn.gConn_Delete("DELETE FROM DOC_EQUMBREMP WHERE equmbrcod = " & auxDataKeyCod & "")
auxClass.Conn.gConn_Delete("DELETE FROM DOC_EQUMBRUND WHERE equmbrcod = " & auxDataKeyCod & "")
Select Case Val(CType(sender.FindControl("insmbrtypecod"),HiddenField).Value)
Case 9
auxClass.gEntity_DOC_EQUMBREMP_Insert(pequmbrcod:=auxDataKeyCod,pempcod:=CType(sender.FindControl("insmbrtypecodvalue"),HiddenField).Value)
Case 10
auxClass.gEntity_DOC_EQUMBRUND_Insert(pequmbrcod:=auxDataKeyCod,pundcod:=CType(sender.FindControl("insmbrtypecodvalue"),HiddenField).Value,pgruporesp:=sender.FindControl("insmbrtypecodvalue_10_gruporesp").Checked,pgrupomiembros:=sender.FindControl("insmbrtypecodvalue_10_grupomiembros").Checked,pgrupoeditores:=sender.FindControl("insmbrtypecodvalue_10_grupoeditores").Checked,pgrupoprjver:=sender.FindControl("insmbrtypecodvalue_10_grupoprjver").Checked,pgrupombrdir:=sender.FindControl("insmbrtypecodvalue_10_grupombrdir").Checked)
End Select

    Dim auxResult as String=auxClass.gSystem_PostAction(18,11,auxDataKeyCod) 
    lblfrmupdpanelctrl000004error.Text &=auxResult
    If auxResult ="" Then
editctrl000004.Databind()
    End If
    auxClass.Conn.gConn_Close
End If

End Sub
Protected Sub frmupdpanelfrmupdpanelctrl000004_ItemUpdated(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.FormViewUpdatedEventArgs)
If e.Exception IsNot Nothing Then
  lblfrmupdpanelctrl000004error.Text &="//Error actualizando datos. Vuelva a realizar la operacion!"
     
Else
    Dim auxClass As New clscusimDOC
    auxClass.Conn.gConn_Open
    Dim auxDataKeyCod as Integer= e.Keys(0)
         auxClass.Conn.gConn_Delete("DELETE FROM DOC_EQUMBREMP WHERE equmbrcod = " & auxDataKeyCod & "")
auxClass.Conn.gConn_Delete("DELETE FROM DOC_EQUMBRUND WHERE equmbrcod = " & auxDataKeyCod & "")
Select Case Val(CType(sender.FindControl("edtmbrtypecod"),HiddenField).Value)
Case 9
auxClass.gEntity_DOC_EQUMBREMP_Insert(pequmbrcod:=auxDataKeyCod,pempcod:=CType(sender.FindControl("edtmbrtypecodvalue"),HiddenField).Value)
Case 10
auxClass.gEntity_DOC_EQUMBRUND_Insert(pequmbrcod:=auxDataKeyCod,pundcod:=CType(sender.FindControl("edtmbrtypecodvalue"),HiddenField).Value,pgruporesp:=sender.FindControl("edtmbrtypecodvalue_10_gruporesp").Checked,pgrupomiembros:=sender.FindControl("edtmbrtypecodvalue_10_grupomiembros").Checked,pgrupoeditores:=sender.FindControl("edtmbrtypecodvalue_10_grupoeditores").Checked,pgrupoprjver:=sender.FindControl("edtmbrtypecodvalue_10_grupoprjver").Checked,pgrupombrdir:=sender.FindControl("edtmbrtypecodvalue_10_grupombrdir").Checked)
End Select

    Dim auxResult as String=auxClass.gSystem_PostAction(18,12,auxDataKeyCod) 
    lblfrmupdpanelctrl000004error.Text &=auxResult
    If auxResult ="" Then
updpanelfrmupdpanelctrl000004.Hide()
Dim auxPageIndex as Integer=editctrl000004.PageIndex
editctrl000004.DataBind
editctrl000004.PageIndex = auxPageIndex
If CType(editctrl000004.Parent.Parent, UpdatePanel).UpdateMode = UpdatePanelUpdateMode.Conditional Then
  CType(editctrl000004.Parent.Parent, UpdatePanel).Update()
End If

     End If
    auxClass.Conn.gConn_Close
End If

End Sub
Protected Sub dsfrmupdpanelctrl000004_Init(ByVal sender As Object,ByVal e As System.EventArgs)
sender.ConnectionString=Session("connectionstringname")
End Sub
Protected Sub dsfrmupdpanelctrl000004_Deleted(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.SqlDataSourceStatusEventArgs)
If e.Exception IsNot Nothing Then
    
Else
    
End If

End Sub
Protected Sub dsfrmupdpanelctrl000004_Inserted(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.SqlDataSourceStatusEventArgs)
If e.Exception IsNot Nothing Then
    
Else
    sender.InsertParameters("querynextcod").DefaultValue = e.Command.Parameters("@querynextcod").Value
End If

End Sub
Protected Sub dsfrmupdpanelctrl000004_Selected(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.SqlDataSourceStatusEventArgs)
If e.Exception IsNot Nothing Then
    
Else
    
End If

End Sub
Protected Sub dsfrmupdpanelctrl000004_Updated(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.SqlDataSourceStatusEventArgs)
If e.Exception IsNot Nothing Then
    
Else
    
End If

End Sub

Protected Sub cmdobjectexplorerfilter_Click (ByVal sender As Object, ByVal e As System.EventArgs)
tvwmodalpopuppnlobjectexplorer_SelectedNodeChanged(tvwmodalpopuppnlobjectexplorer,Nothing)
End Sub
Protected Sub grdobjectexplorer_ItemDeleted(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewCommandEventArgs)

End Sub
Protected Sub grdobjectexplorer_ItemInserted(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewCommandEventArgs)

End Sub
Protected Sub grdobjectexplorer_ItemSelected(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewCommandEventArgs)

End Sub
Protected Sub grdobjectexplorer_ItemUpdated(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewCommandEventArgs)

End Sub
Protected Sub grdobjectexplorer_PageIndexChanging(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewPageEventArgs)
tvwmodalpopuppnlobjectexplorer_SelectedNodeChanged(tvwmodalpopuppnlobjectexplorer,Nothing)
sender.PageIndex = e.NewPageIndex
sender.DataBind()

End Sub
Protected Sub grdobjectexplorer_RowCommand(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewCommandEventArgs)
Select Case e.CommandName.ToUpper()
  Case "UPDATE"
      grdobjectexplorer_ItemUpdated(sender,e)
  Case "HRC_INSERT"
      grdobjectexplorer_ItemInserted(sender,e)
End Select

End Sub
Protected Sub grdobjectexplorer_RowCreated(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs)
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
Protected Sub grdobjectexplorer_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs)
modalpopuppnlobjectexplorer_Select(grdobjectexplorer.SelectedDataKey(0), grdobjectexplorer.SelectedDataKey(1), CType(grdobjectexplorer.SelectedRow.Cells(2).Controls(1),LinkButton).Text)
End Sub
Protected Sub cmdobjectexplorercancel_Click (ByVal sender As Object, ByVal e As System.EventArgs)
modalpopuppnlobjectexplorer_Cancel()
End Sub
Protected Sub cmdobjectexplorerselect_Click (ByVal sender As Object, ByVal e As System.EventArgs)
modalpopuppnlobjectexplorer_Select(grdobjectexplorer.SelectedDataKey(0), grdobjectexplorer.SelectedDataKey(1), grdobjectexplorer.SelectedRow.Cells(2).Text)
End Sub
Public Sub modalpopuppnlobjectexplorer_Cancel
ViewState("modalpopuppnlobjectexplorer_mode") = Nothing
pnlobjectexplorer.Hide()

End Sub
Public Sub modalpopuppnlobjectexplorer_Load(ByVal pCod As Integer, ByVal pParentNode As TreeNode, ByVal pObjectType As Integer)
Dim auxQuery As String = ""
Select Case pObjectType
Case 10
    auxQuery = "SELECT cod as cod,dsc as dsc,10 as objecttype FROM UND   WHERE ((UND.undcodsup = {#PARAM1#}) AND (UND.baja = 0 OR UND.baja  IS NULL)) AND UND.cod >= 1"
Case 100013
    auxQuery = "SELECT Q_SECPGRP.grpcod,Q_SECPGRP.grpdsc,100013 as objecttype FROM Q_SECPGRP   WHERE (Q_SECPGRP.grpinherit = {#PARAM1#}) AND Q_SECPGRP.grpcod >= 1"
End Select
auxQuery = Replace(auxQuery,"{#PARAM1#}",pCod)
Dim auxConn As clsHrcConnClient = Session("conn")
 auxConn = auxConn.gComponent_CreateInstance
auxConn.gConn_Open
Dim auxDT As DataTable = auxConn.gConn_Query(auxQuery )
auxConn.gConn_Close
For Each auxRow As DataRow In auxDT.Rows
  Dim auxNode As New TreeNode(If(IsDBNull(auxRow(1)),"Todos",auxRow(1)), Format(auxRow(2), "00000000") & auxRow(0).ToString)
  If pParentNode Is Nothing Then
      tvwmodalpopuppnlobjectexplorer.Nodes.Add(auxNode)
  Else
      pParentNode.ChildNodes.Add(auxNode)
  End If
      If CInt(auxRow(0)) <> pCod Then
          modalpopuppnlobjectexplorer_Load(CInt(auxRow(0)), auxNode,pObjectType)
      End If
Next

End Sub
Public Sub modalpopuppnlobjectexplorer_Select(ByVal pValue As Integer,ByVal pObjectType As Integer,ByVal pText As String)
    Dim auxText As String = ""
    auxText = "<img width=14 height=14 border=0 src='imagenes/icon" & Format(pObjectType, "00000000") & ".png' />" & pText
    Dim auxLinkButton as LinkButton=CType(FindControl(ViewState("modalpopuppnlobjectexplorer_controlid")),LinkButton)
    objectexplorer_SetValue(pValue,pObjectType,pText,auxLinkButton,CType(FindControl(ViewState("modalpopuppnlobjectexplorer_value")), HiddenField),CType(FindControl(ViewState("modalpopuppnlobjectexplorer_type")), HiddenField),CType(FindControl(ViewState("modalpopuppnlobjectexplorer_delete")), ImageButton))
If TypeOf (auxLinkButton.Parent) Is Control Then
    If TypeOf (auxLinkButton.Parent.Parent) Is UpdatePanel Then
        If CType(auxLinkButton.Parent.Parent, UpdatePanel).UpdateMode = UpdatePanelUpdateMode.Conditional Then
            CType(auxLinkButton.Parent.Parent, UpdatePanel).Update()
        End If
    End If
End If
ViewState("modalpopuppnlobjectexplorer_mode") = Nothing
ViewState("modalpopuppnlobjectexplorer_type") = Nothing
ViewState("modalpopuppnlobjectexplorer_controlid") = Nothing
ViewState("modalpopuppnlobjectexplorer_delete") = Nothing
ViewState("modalpopuppnlobjectexplorer_value") = Nothing
pnlobjectexplorer.Hide()

End Sub
Public Sub objectexplorer_SetValue(ByVal pValue As Integer,ByVal pObjectType As Integer,ByVal pText As String, ByVal pLinkButton As LinkButton, ByVal pHiddenValue As HiddenField, ByVal pHiddentype As HiddenField, ByVal pDeleteButton As ImageButton)
If pHiddenValue.ID = "insmbrtypecodvalue" Then
If pObjectType = 10 Then
pLinkButton.Parent.FindControl("pnlinsmbrtypecodvalue_10").Visible=True
Else
pLinkButton.Parent.FindControl("pnlinsmbrtypecodvalue_10").Visible=False
End If
End If

If pHiddenValue.ID = "edtmbrtypecodvalue" Then
If pObjectType = 10 Then
pLinkButton.Parent.FindControl("pnledtmbrtypecodvalue_10").Visible=True
Else
pLinkButton.Parent.FindControl("pnledtmbrtypecodvalue_10").Visible=False
End If
End If

If pHiddenValue IsNot Nothing Then pHiddenValue.Value = pValue
If pHiddentype IsNot Nothing Then pHiddentype.Value = pObjectType
If pValue < 1 Then
   pLinkButton.Visible = False
   If pDeleteButton IsNot Nothing Then pDeleteButton.Visible = False
   Exit Sub
End If
Dim auxText As String = ""
auxText = "<img width=14 height=14 border=0 src='imagenes/icon" & Format(pObjectType, "00000000") & ".png' />" & pText
pLinkButton.Text = auxText
Select Case pObjectType
    Case 9
        pLinkButton.OnClientClick = "javascript:if (window.showModalDialog) {window.showModalDialog(" & chr(39) & "frmColaboradores_det.aspx?_mode_=0&_closea_=1&isDlg=1&param1=" & pValue & "&timestamp=" & chr(39) & " + new Date().getTime() + " & chr(39) & "" & chr(39) & ", " & chr(39) & "" & chr(39) & ", " & chr(39) & "menubar=no;resizable=yes;toolbar=no;status=no;center=yes;dialogWidth=750px;dialogHeight=570px;" & chr(39) & ");} else {window.open(" & chr(39) & "frmColaboradores_det.aspx?_mode_=0&_closea_=1&isDlg=1&param1=" & pValue & "&timestamp=" & chr(39) & " + new Date().getTime() + " & chr(39) & "" & chr(39) & ", " & chr(39) & "" & chr(39) & ", " & chr(39) & "modal=yes;directories=no;menubar=no;resizable=yes;toolbar=no;status=no;center=yes;dialogWidth=750px;dialogHeight=570px;" & chr(39) & ");};return false;"
    Case 10
        pLinkButton.OnClientClick = "javascript:if (window.showModalDialog) {window.showModalDialog(" & chr(39) & "frmUnidades_det.aspx?_mode_=0&_closea_=1&isDlg=1&param1=" & pValue & "&timestamp=" & chr(39) & " + new Date().getTime() + " & chr(39) & "" & chr(39) & ", " & chr(39) & "" & chr(39) & ", " & chr(39) & "menubar=no;resizable=yes;toolbar=no;status=no;center=yes;dialogWidth=750px;dialogHeight=570px;" & chr(39) & ");} else {window.open(" & chr(39) & "frmUnidades_det.aspx?_mode_=0&_closea_=1&isDlg=1&param1=" & pValue & "&timestamp=" & chr(39) & " + new Date().getTime() + " & chr(39) & "" & chr(39) & ", " & chr(39) & "" & chr(39) & ", " & chr(39) & "modal=yes;directories=no;menubar=no;resizable=yes;toolbar=no;status=no;center=yes;dialogWidth=750px;dialogHeight=570px;" & chr(39) & ");};return false;"
    Case 100013
        pLinkButton.OnClientClick = "javascript:if (window.showModalDialog) {window.showModalDialog(" & chr(39) & "frmGrupos_det.aspx?_mode_=0&_closea_=1&isDlg=1&param1=" & pValue & "&timestamp=" & chr(39) & " + new Date().getTime() + " & chr(39) & "" & chr(39) & ", " & chr(39) & "" & chr(39) & ", " & chr(39) & "menubar=no;resizable=yes;toolbar=no;status=no;center=yes;dialogWidth=750px;dialogHeight=570px;" & chr(39) & ");} else {window.open(" & chr(39) & "frmGrupos_det.aspx?_mode_=0&_closea_=1&isDlg=1&param1=" & pValue & "&timestamp=" & chr(39) & " + new Date().getTime() + " & chr(39) & "" & chr(39) & ", " & chr(39) & "" & chr(39) & ", " & chr(39) & "modal=yes;directories=no;menubar=no;resizable=yes;toolbar=no;status=no;center=yes;dialogWidth=750px;dialogHeight=570px;" & chr(39) & ");};return false;"
End Select
pLinkButton.Visible = True
If pDeleteButton IsNot Nothing Then pDeleteButton.Visible = True

End Sub
Public Sub objectexplorer_Show(ByVal pControlIDValue As String, ByVal pControlIDText As String,  ByVal pControlIDType As String,ByVal pObjectTvwType As String, ByVal pObjectGrdType As String,ByVal pControlIDDelete As String)
tvwmodalpopuppnlobjectexplorer.Nodes.Clear()
Dim auxStartNode As TreeNode=Nothing
Dim auxObjectTypes As String = ""
For Each auxString As String In Split(pObjectTvwType, "{#CHR34#}")
  Dim auxObjectID as Integer = Val(auxString)
  auxObjectTypes &= "{#CHR34#}" & auxObjectID & "{#CHR34#}"
  Select Case Val(auxString)
  Case 0
  Case -1
      auxStartNode = New TreeNode("Todos", -1)
      tvwmodalpopuppnlobjectexplorer.Nodes.Add(auxStartNode)
       modalpopuppnlobjectexplorer_Load(-1, auxStartNode, pObjectTvwType)
    Case 10
      auxStartNode= New TreeNode("<img width=14 height=14 border=0 src='imagenes/icon00000010.png' />Unidades", "00000010")
tvwmodalpopuppnlobjectexplorer.Nodes.Add(auxStartNode)
modalpopuppnlobjectexplorer_Load(-1, auxStartNode, auxObjectID)
    Case 100013
      auxStartNode= New TreeNode("<img width=14 height=14 border=0 src='imagenes/icon00100013.png' />Grupos", "00100013")
tvwmodalpopuppnlobjectexplorer.Nodes.Add(auxStartNode)
modalpopuppnlobjectexplorer_Load(-1, auxStartNode, auxObjectID)
  End Select
Next
grdobjectexplorer.Visible=True
tvwmodalpopuppnlobjectexplorer.Nodes(0).Select()
tvwmodalpopuppnlobjectexplorer_SelectedNodeChanged(tvwmodalpopuppnlobjectexplorer,Nothing)
tvwmodalpopuppnlobjectexplorer.Nodes(0).Expand()
ViewState("modalpopuppnlobjectexplorer_controlid") = pControlIDText
ViewState("modalpopuppnlobjectexplorer_value") = pControlIDValue
ViewState("modalpopuppnlobjectexplorer_type") = pControlIDType
ViewState("modalpopuppnlobjectexplorer_delete") = pControlIDDelete
Dim auxObjectGrid As String = ""
For Each auxString As String In Split(pObjectGrdType, "{#CHR34#}")
  If auxString.Trim <> "" Then
      auxObjectGrid &= "{#CHR34#}" & auxString & "{#CHR34#}"
  End If
Next
ViewState("modalpopuppnlobjectexplorer_mode") = auxObjectGrid
grdobjectexplorer.Visible=True
cmdobjectexplorerfilter.Visible=True
txtobjectexplorerfilter.Visible=True
lblobjectexplorerfilter.Visible=True
tvwmodalpopuppnlobjectexplorer_SelectedNodeChanged(tvwmodalpopuppnlobjectexplorer,Nothing)
pnlobjectexplorer.Show()
updpnlobjectexplorer.Update()

End Sub
Public Sub tvwmodalpopuppnlobjectexplorer_SelectedNodeChanged(ByVal sender As Object, ByVal e As System.EventArgs)
Dim auxSelectedValue as String=CType(sender, TreeView).SelectedNode.Value
Dim auxObjectType as String=""
auxObjectType=Val(auxSelectedValue.Substring(0, 8))
auxSelectedValue = Mid(auxSelectedValue, 9, 8)
If CType(sender, TreeView).SelectedNode.Depth=0 Then
    auxSelectedValue=-1
End If
If grdobjectexplorer.Visible = False Then
    modalpopuppnlobjectexplorer_Select(auxSelectedValue, 10, CType(sender, TreeView).SelectedNode.Text)
Else
    Dim auxQuery As String = ""
    If (auxObjectType = "10") And Instr(ViewState("modalpopuppnlobjectexplorer_mode"), "{#CHR34#}9{#CHR34#}") > 0 Then
        If auxQuery <> "" Then auxQuery &= " UNION ALL "
        auxQuery &= "(SELECT EMP.cod as cod,EMP.dsc as dsc,9 as objecttype,'Colaborador' as objecttypedsc FROM EMP   WHERE ((((EMP.undcod =" & auxSelectedValue & " AND '" & txtobjectexplorerfilter.Text.Trim & "'='')  OR  (EMP.dsc LIKE '%" & txtobjectexplorerfilter.Text.Trim & "%' AND '" & txtobjectexplorerfilter.Text.Trim & "'<>''))) AND (EMP.baja = 0 OR EMP.baja  IS NULL)) AND EMP.cod >= 1)"
    End If
    If (auxObjectType = "10") And Instr(ViewState("modalpopuppnlobjectexplorer_mode"), "{#CHR34#}10{#CHR34#}") > 0 Then
        If auxQuery <> "" Then auxQuery &= " UNION ALL "
        auxQuery &= "(SELECT UND.cod as cod,UND.dsc as dsc,10 as objecttype,'Unidad' as objecttypedsc FROM UND   WHERE ((((UND.undcodsup =" & auxSelectedValue & " AND '" & txtobjectexplorerfilter.Text.Trim & "'='')  OR  (UND.dsc LIKE '%" & txtobjectexplorerfilter.Text.Trim & "%' AND '" & txtobjectexplorerfilter.Text.Trim & "'<>''))) AND (UND.baja = 0 OR UND.baja  IS NULL)) AND UND.cod >= 1)"
    End If
    If (auxObjectType = "100013") And Instr(ViewState("modalpopuppnlobjectexplorer_mode"), "{#CHR34#}100013{#CHR34#}") > 0 Then
        If auxQuery <> "" Then auxQuery &= " UNION ALL "
        auxQuery &= "(SELECT grpcod as cod,grpdsc as dsc,100013 as objecttype,'Grupo' as objecttypedsc FROM Q_SECPGRP   WHERE ((((Q_SECPGRP.grpinherit =" & auxSelectedValue & ") AND '" & txtobjectexplorerfilter.Text.Trim & "'='')  OR  (Q_SECPGRP.grpdsc LIKE '%" & txtobjectexplorerfilter.Text.Trim & "%' AND '" & txtobjectexplorerfilter.Text.Trim & "'<>''))) AND Q_SECPGRP.grpcod >= 1)"
    End If
    auxQuery &=" ORDER BY dsc,objecttype,cod"
    If e IsNot Nothing Then
        txtobjectexplorerfilter.Text=""
    End If
    Dim auxConn As clsHrcConnClient = Session("conn")
    auxConn.gConn_Open
    grdobjectexplorer.DataSource= auxConn.gConn_Query(auxQuery)
    grdobjectexplorer.DataBind()
    updpnlobjectexplorer.Update()
    auxConn.gConn_Close
End If

End Sub

Private Sub gForm_Close()
     If Request.QueryString("_closea_") = "1" Then
      ScriptManager.RegisterStartupScript(Me, Me.GetType(), "CloseWindow", "self.close();", True)
     Else
         Response.Redirect(Session("ROOTWEBURL") & "frmFuncionesyequipos.aspx?_mode_=7&_closea_=0")
     End If

End Sub
Protected Sub frmFuncionesyequipos_det_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
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

