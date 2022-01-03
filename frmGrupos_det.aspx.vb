Imports System.Data
Imports System.Data.SqlClient
Imports Intelimedia.imComponentes
Public Class frmGrupos_det
Inherits System.Web.UI.Page
Friend m_PermFormDelete as Boolean=False
Friend m_PermFormEdit as Boolean=False
Friend m_PermFormNew as Boolean=False
Protected Sub itemctrl000014_Databound(ByVal sender As Object, ByVal e As System.EventArgs)
CType(sender,Label).Text=Eval("grpcod").ToString

End Sub

Protected Sub itemctrl000012_Databound(ByVal sender As Object, ByVal e As System.EventArgs)
CType(sender,Label).Text=Eval("grpcod").ToString

End Sub

Protected Sub itemctrl000011_Databound(ByVal sender As Object, ByVal e As System.EventArgs)
CType(sender,Label).Text=Replace(Eval("grpdsc").ToString,Chr(10),"<br />")

End Sub

Protected Sub itemctrl000010view_DataBinding(ByVal sender As Object, ByVal e As System.EventArgs)
Dim auxValue as Int32=-1
If IsDBNull(Eval("grpinherit")) Then 
     Ctype(sender,Control).Visible=False
Else
  If Eval("grpinherit") < 1 Then 
     Ctype(sender,Control).Visible=False
  End If
End If

End Sub
Protected Sub itemctrl000010_Databound(ByVal sender As Object, ByVal e As System.EventArgs)
If IsDBNull(Eval("grpinherit")) Then
 CType(sender,HiddenField).Value=""
Else
 CType(sender,HiddenField).Value=Eval("grpinherit").ToString()
End If

End Sub

Protected Sub itemctrl000009_Databound(ByVal sender As Object, ByVal e As System.EventArgs)
CType(sender,Label).Text=Eval("grpcodrel").ToString

End Sub

Protected Sub itemctrl000008_Databound(ByVal sender As Object, ByVal e As System.EventArgs)
CType(sender,Label).Text="<img src=imagenes/"
If IsDBNull(Eval("grpdisabled")) Then
CType(sender,Label).Text &="actundefined.gif alt=S/D "
ElseIf Eval("grpdisabled") Then
CType(sender,Label).Text &="actyes.gif alt=Si "
Else
CType(sender,Label).Text &="actno.gif alt=No "
End If
CType(sender,Label).Text &=" width=12px border=0px />"

End Sub

Protected Sub itemctrl000007view_DataBinding(ByVal sender As Object, ByVal e As System.EventArgs)
Dim auxValue as Int32=-1
If IsDBNull(Eval("sidcodvalue")) Then 
     Ctype(sender,Control).Visible=False
Else
objectexplorer_SetValue(Eval("sidcodvalue"),Eval("sidcod"),Eval("sidcoddsc").ToString,CType(sender.parent.FindControl("itemctrl000007view"),LinkButton),CType(sender.parent.FindControl("itemctrl000007value"),HiddenField),CType(sender.parent.FindControl("itemctrl000007"),HiddenField),Nothing)
End If

End Sub
Protected Sub itemctrl000007value_Databound(ByVal sender As Object, ByVal e As System.EventArgs)
If IsDBNull(Eval("sidcodvalue")) Then
 CType(sender,HiddenField).Value=""
Else
 CType(sender,HiddenField).Value=Eval("sidcodvalue").ToString()
End If

End Sub

Protected Sub itemctrl000006_Databound(ByVal sender As Object, ByVal e As System.EventArgs)
CType(sender,Label).Text=Eval("grpid").ToString

End Sub

Protected Sub itemctrl000004colitem1view_DataBinding(ByVal sender As Object, ByVal e As System.EventArgs)
If IsDBNull(Eval("sidcod")) Then 
     Ctype(sender,Control).Visible=False
Else
  If Eval("sidcod") = -1 Then 
     Ctype(sender,Control).Visible=False
  End If
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

Protected Sub cmdFormViewItemUpdate_Click (ByVal sender As Object, ByVal e As System.EventArgs)
Server.Transfer(Request.Url.AbsolutePath & "?" & Replace(Request.QueryString.ToString, "_mode_=0", "_mode_=2"))
End Sub

Protected Sub editctrl000012_Databound(ByVal sender As Object, ByVal e As System.EventArgs)
CType(sender,Label).Text=Eval("grpcod").ToString

End Sub

Protected Sub editctrl000011_DataBound_frmdatos(ByVal sender As Object, ByVal e As System.EventArgs)
CType(sender,TextBox).Text=Eval("grpdsc").ToString

End Sub

Protected Sub dseditctrl000010_Init(ByVal sender As Object,ByVal e As System.EventArgs)
sender.ConnectionString=Session("connectionstringname")
End Sub
Protected Sub dseditctrl000010_Deleted(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.SqlDataSourceStatusEventArgs)
If e.Exception IsNot Nothing Then
  lblerror.Text &="//Error borrando datos (" & e.Exception.Message & ")"
    
Else
    
End If

End Sub
Protected Sub dseditctrl000010_Inserted(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.SqlDataSourceStatusEventArgs)
If e.Exception IsNot Nothing Then
  lblerror.Text &="//Error insertando datos (" & e.Exception.Message & ")"
    
Else
    
End If

End Sub
Protected Sub dseditctrl000010_Selected(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.SqlDataSourceStatusEventArgs)
If e.Exception IsNot Nothing Then
  lblerror.Text &="//Error buscando datos (" & e.Exception.Message & ")"
    
Else
    
End If

End Sub
Protected Sub dseditctrl000010_Updated(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.SqlDataSourceStatusEventArgs)
If e.Exception IsNot Nothing Then
  lblerror.Text &="//Error actualizando datos (" & e.Exception.Message & ")"
    
Else
    
End If

End Sub

Protected Sub editctrl000010view_DataBinding(ByVal sender As Object, ByVal e As System.EventArgs)
Dim auxValue as Int32=-1
If IsDBNull(Eval("grpinherit")) Then 
     Ctype(sender,Control).Visible=False
Else
  If Eval("grpinherit") < 1 Then 
     Ctype(sender,Control).Visible=False
  End If
End If

End Sub
Protected Sub editctrl000010delete_Click (ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs)
editctrl000010type.Value=-1
editctrl000010.Value=-1
editctrl000010view.Visible=False
editctrl000010delete.Visible=False

End Sub
Protected Sub editctrl000010delete_DataBinding(ByVal sender As Object, ByVal e As System.EventArgs)
If Not IsPostBack  Then 
 If IsDBNull(Eval("grpinherit")) Then 
    CType(sender,Control).Visible=False
  ElseIf Eval("grpinherit") < 1 Then
    CType(sender,Control).Visible=False
  Else
    CType(sender,Control).Visible=True
 End If
End If

End Sub
Protected Sub cmdeditctrl000010showpanel_Click (ByVal sender As Object, ByVal e As System.EventArgs)
objectexplorer_Show(CType(sender.parent.FindControl("editctrl000010"),HiddenField).UniqueID,CType(sender.parent.FindControl("editctrl000010view"),LinkButton).UniqueID,CType(sender.parent.FindControl("editctrl000010type"),HiddenField).UniqueID,"100013","100013",CType(sender.parent.FindControl("editctrl000010delete"),ImageButton).UniqueID)
End Sub
Protected Sub editctrl000010_Databound(ByVal sender As Object, ByVal e As System.EventArgs)
If IsDBNull(Eval("grpinherit")) Then
 CType(sender,HiddenField).Value=""
Else
 CType(sender,HiddenField).Value=Eval("grpinherit").ToString()
End If

End Sub

Protected Sub editctrl000009_DataBound_frmdatos(ByVal sender As Object, ByVal e As System.EventArgs)
CType(sender,TextBox).Text=Eval("grpcodrel").ToString

End Sub

Protected Sub editctrl000008_Databound(ByVal sender As Object, ByVal e As System.EventArgs)
    If IsDBNull(DataBinder.Eval(frmdatos.DataItem,"grpdisabled"))  Then
        CType(sender,CheckBox).Checked=False
    Else
        CType(sender,CheckBox).Checked=DataBinder.Eval(frmdatos.DataItem,"grpdisabled")
    End If

End Sub

Protected Sub editctrl000007view_DataBinding(ByVal sender As Object, ByVal e As System.EventArgs)
Dim auxValue as Int32=-1
If IsDBNull(Eval("sidcodvalue")) Then 
     Ctype(sender,Control).Visible=False
Else
objectexplorer_SetValue(Eval("sidcodvalue"),Eval("sidcod"),Eval("sidcoddsc").ToString,CType(sender.parent.FindControl("editctrl000007view"),LinkButton),CType(sender.parent.FindControl("editctrl000007value"),HiddenField),CType(sender.parent.FindControl("editctrl000007"),HiddenField),Nothing)
End If

End Sub
Protected Sub editctrl000007value_Databound(ByVal sender As Object, ByVal e As System.EventArgs)
If IsDBNull(Eval("sidcodvalue")) Then
 CType(sender,HiddenField).Value=""
Else
 CType(sender,HiddenField).Value=Eval("sidcodvalue").ToString()
End If

End Sub

Protected Sub editctrl000006_DataBound_frmdatos(ByVal sender As Object, ByVal e As System.EventArgs)
CType(sender,TextBox).Text=Eval("grpid").ToString

End Sub

Protected Sub editctrl000004colitem1view_DataBinding(ByVal sender As Object, ByVal e As System.EventArgs)
If IsDBNull(Eval("sidcod")) Then 
     Ctype(sender,Control).Visible=False
Else
  If Eval("sidcod") = -1 Then 
     Ctype(sender,Control).Visible=False
  End If
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
SELECT CASE e.CommandName.ToUpper()
  CASE "DELETE"
       Dim auxSecurity as clsHrcSecurityClient=Session("security")
       Server.ScriptTimeout = 1800
        ScriptManager.GetCurrent(Me).AsyncPostBackTimeout = 360000
       auxSecurity.gGroup_DelMember(Val(e.CommandArgument))
END SELECT

Select Case e.CommandName.ToUpper()
  Case "SELECT"
          dsfrmupdpanelctrl000004.SelectParameters(frmupdpanelfrmupdpanelctrl000004.DataKeyNames(0)).DefaultValue=e.CommandArgument
          frmupdpanelfrmupdpanelctrl000004.ChangeMode(FormViewMode.Edit)
          frmupdpanelfrmupdpanelctrl000004.DataBind()
      lblfrmupdpanelctrl000004subtitle.Text="Visualizacion de Miembros de grupos"
          updpanelfrmupdpanelctrl000004.Show()
          updupdpanelfrmupdpanelctrl000004.Update()
  Case "UPDATE"
  Case "EDIT"
          dsfrmupdpanelctrl000004.SelectParameters(frmupdpanelfrmupdpanelctrl000004.DataKeyNames(0)).DefaultValue=e.CommandArgument
          frmupdpanelfrmupdpanelctrl000004.ChangeMode(FormViewMode.Edit)
          frmupdpanelfrmupdpanelctrl000004.DataBind()
      lblfrmupdpanelctrl000004subtitle.Text="Edición de Miembros de grupos"
          updpanelfrmupdpanelctrl000004.Show()
          updupdpanelfrmupdpanelctrl000004.Update()
  Case "HRC_INSERT"
          frmupdpanelfrmupdpanelctrl000004.ChangeMode(FormViewMode.Insert)
      lblfrmupdpanelctrl000004subtitle.Text="Nuevo en Miembros de grupos"
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

Protected Sub dsinsctrl000010_Init(ByVal sender As Object,ByVal e As System.EventArgs)
sender.ConnectionString=Session("connectionstringname")
End Sub
Protected Sub dsinsctrl000010_Deleted(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.SqlDataSourceStatusEventArgs)
If e.Exception IsNot Nothing Then
  lblerror.Text &="//Error borrando datos (" & e.Exception.Message & ")"
    
Else
    
End If

End Sub
Protected Sub dsinsctrl000010_Inserted(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.SqlDataSourceStatusEventArgs)
If e.Exception IsNot Nothing Then
  lblerror.Text &="//Error insertando datos (" & e.Exception.Message & ")"
    
Else
    
End If

End Sub
Protected Sub dsinsctrl000010_Selected(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.SqlDataSourceStatusEventArgs)
If e.Exception IsNot Nothing Then
  lblerror.Text &="//Error buscando datos (" & e.Exception.Message & ")"
    
Else
    
End If

End Sub
Protected Sub dsinsctrl000010_Updated(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.SqlDataSourceStatusEventArgs)
If e.Exception IsNot Nothing Then
  lblerror.Text &="//Error actualizando datos (" & e.Exception.Message & ")"
    
Else
    
End If

End Sub

Protected Sub insctrl000010delete_Click (ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs)
insctrl000010type.Value=-1
insctrl000010.Value=-1
insctrl000010view.Visible=False
insctrl000010delete.Visible=False

End Sub
Protected Sub insctrl000010delete_DataBinding(ByVal sender As Object, ByVal e As System.EventArgs)
If Not IsPostBack  Then 
 If IsDBNull(Eval("grpinherit")) Then 
    CType(sender,Control).Visible=False
  ElseIf Eval("grpinherit") < 1 Then
    CType(sender,Control).Visible=False
  Else
    CType(sender,Control).Visible=True
 End If
End If

End Sub
Protected Sub cmdinsctrl000010showpanel_Click (ByVal sender As Object, ByVal e As System.EventArgs)
objectexplorer_Show(CType(sender.parent.FindControl("insctrl000010"),HiddenField).UniqueID,CType(sender.parent.FindControl("insctrl000010view"),LinkButton).UniqueID,CType(sender.parent.FindControl("insctrl000010type"),HiddenField).UniqueID,"100013","100013",CType(sender.parent.FindControl("insctrl000010delete"),ImageButton).UniqueID)
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
         Response.Redirect(coWebRootFolder & "frmGrupos.aspx?_mode_=7&_closea_=0")
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
    Dim auxResult as String=auxClass.gSystemConfig_PostAction("Q_SECPGRP",13,auxDataKeyCod) 
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
    
    Dim auxResult as String=auxClass.gSystemConfig_PostAction("Q_SECPGRP",11,auxDataKeyCod) 
    lblerror.Text &=auxResult
    If auxResult ="" Then
    End If
    auxClass.Conn.gConn_Close
End If

End Sub
Protected Sub frmdatos_ItemSelected(ByVal sender As Object, ByVal e As System.EventArgs)
If frmdatos.DataItem IsNot Nothing Then
   Page.Title = "Grupos| " &  Databinder.Eval(frmdatos.DataItem ,"grpdsc").ToString
   lblsubtitle.Text = Databinder.Eval(frmdatos.DataItem ,"grpdsc").ToString
Else
   Page.Title = "Grupos| Nuevo"
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
     Dim auxMax As Integer = Val(LicParam.gValue_Get("Q_SECPGRP.MAX", -1))
     If auxMax >= 0 Then
         Dim auxConn As clsHrcConnClient = Session("conn")
         auxConn.gComponent_CreateInstance()
         auxConn.gConn_Open()
         If Val(auxConn.gConn_QueryValueInt("SELECT COUNT(*) FROM Q_SECPGRP", 0)) > auxMax Then
             gForm_Close()
         End If
         auxConn.gConn_Close()
     End If
 End If
End Select
Select Case Request.QueryString("_mode_")
 Case "1"
             CType(sender.parent.FindControl("frmdatos$insctrl000008"),CheckBox).Checked=False
 Case "25"
   lblsubtitle.Text = "Copiar"
    Try
        Dim auxConn As New SqlConnection(Session("connectionstringname"))
        auxConn.Open
        Dim auxDA As New SqlDataAdapter("SELECT Q_SECPGRP.grpcod,Q_SECPGRP.grpdsc,(Q_SECPGRPGRPINHERIT.grpdsc) as grpinheritdsc,Q_SECPGRP.grpcodrel,Q_SECPGRP.grpdisabled,CASE Q_SECPGRPSIDCOD.sidtypecod WHEN -3 THEN (SELECT TOP 1 Q_SECPGRP.grpcod FROM Q_SECPGRP WHERE Q_SECPGRP.sidcod=Q_SECPGRPSIDCOD.sidcod) WHEN -2 THEN (SELECT TOP 1 Q_SECPLOGIN.seccod FROM Q_SECPLOGIN WHERE Q_SECPLOGIN.sidcod=Q_SECPGRPSIDCOD.sidcod) END as sidcodvalue,CASE Q_SECPGRPSIDCOD.sidtypecod WHEN -3 THEN (SELECT TOP 1 Q_SECPGRP.grpdsc FROM Q_SECPGRP WHERE Q_SECPGRP.grpcod= (SELECT TOP 1 Q_SECPGRP.grpcod FROM Q_SECPGRP WHERE Q_SECPGRP.sidcod=Q_SECPGRPSIDCOD.sidcod)) WHEN -2 THEN (SELECT TOP 1 Q_SECPLOGIN.secdsc FROM Q_SECPLOGIN WHERE Q_SECPLOGIN.seccod= (SELECT TOP 1 Q_SECPLOGIN.seccod FROM Q_SECPLOGIN WHERE Q_SECPLOGIN.sidcod=Q_SECPGRPSIDCOD.sidcod)) END as sidcoddsc,(Q_SECPGRPSIDCOD.sidtypecod) as sidcoddsc,Q_SECPGRP.grpid,(Q_SECPGRPQSECSID.secdsc) as qsecsiddsc,Q_SECPGRP.qsecdatetime,Q_SECPGRP.grpinherit,Q_SECPGRP.sidcod,Q_SECPGRP.qsecsid FROM Q_SECPGRP  LEFT JOIN Q_SECPGRP AS Q_SECPGRPGRPINHERIT ON Q_SECPGRPGRPINHERIT.grpcod=Q_SECPGRP.grpinherit LEFT JOIN Q_SECPSID AS Q_SECPGRPSIDCOD ON Q_SECPGRPSIDCOD.sidcod=Q_SECPGRP.sidcod LEFT JOIN Q_SECPLOGIN AS Q_SECPGRPQSECSID ON Q_SECPGRPQSECSID.sidcod=Q_SECPGRP.qsecsid  WHERE Q_SECPGRP.grpcod=@param1", auxConn)
        auxDA.SelectCommand.Parameters.Add("@param1", SqlDbType.Int, 4).Value = Request.QueryString("param1")
        Dim auxDT As New DataTable
        auxDA.Fill(auxDT)
        auxConn.Close
        If auxDT.Rows.Count <> 0 Then
             If IsDBNull(auxDT.Rows(0)("grpdsc")) = False Then
             CType(sender.parent.FindControl("frmdatos$insctrl000011"),TextBox).Text=auxDT.Rows(0)("grpdsc")
             End If 
             If IsDBNull(auxDT.Rows(0)("grpinherit")) = False Then
     If IsDBNull("" & auxDT.Rows(0)("grpinherit") & "")=False Then
         If CInt("" & auxDT.Rows(0)("grpinherit") & "") > 0 Then
          CType(sender.parent.FindControl("frmdatos$insctrl000010"),HiddenField).Value="" & auxDT.Rows(0)("grpinherit") & ""
          CType(sender.parent.FindControl("frmdatos$insctrl000010view"),LinkButton).Text=auxDT.Rows(0)("grpinheritdsc")
          CType(sender.parent.FindControl("frmdatos$insctrl000010delete"),ImageButton).Visible=True
         End If
      End If
             End If 
             If IsDBNull(auxDT.Rows(0)("grpcodrel")) = False Then
             CType(sender.parent.FindControl("frmdatos$insctrl000009"),TextBox).Text=auxDT.Rows(0)("grpcodrel")
             End If 
             If IsDBNull(auxDT.Rows(0)("grpdisabled")) = False Then
             CType(sender.parent.FindControl("frmdatos$insctrl000008"),CheckBox).Checked=auxDT.Rows(0)("grpdisabled")
             End If 
             If IsDBNull(auxDT.Rows(0)("grpid")) = False Then
             CType(sender.parent.FindControl("frmdatos$insctrl000006"),TextBox).Text=auxDT.Rows(0)("grpid")
             End If 
        End If
    Catch ex as sqlException
    End Try
End Select
Select Case Request.QueryString("_mode_")
 Case "1","25"
Dim auxClass As New clscusimDOC
auxClass.Conn.gConn_Open()
Dim auxBagValues As clshrcBagValues = auxClass.gSystem_PostAction_v2(100013,Val(Request.QueryString("_mode_")), -1, Nothing)
If auxBagValues IsNot Nothing Then
     Dim auxValue As Object
End If
auxClass.Conn.gConn_Close()
End Select

Select Case Request.QueryString("_mode_")
 Case "2"
Dim auxClass As New clscusimDOC
auxClass.Conn.gConn_Open()
Dim auxBagValues As clshrcBagValues = auxClass.gSystem_PostAction_v2(100013,Val(Request.QueryString("_mode_")),Databinder.Eval(frmdatos.DataItem ,"grpcod"), Nothing)
If auxBagValues IsNot Nothing Then
     Dim auxValue As Object
End If
auxClass.Conn.gConn_Close()
End Select

Select Case Request.QueryString("_mode_")
 Case "2"
Dim auxClass As New clscusimDOC
auxClass.Conn.gConn_Open()
Dim auxBagValues As clshrcBagValues = auxClass.gSystem_PostAction_v2(100013,Val(Request.QueryString("_mode_")),Databinder.Eval(frmdatos.DataItem ,"grpcod"), Nothing)
If auxBagValues IsNot Nothing Then
     Dim auxValue As Object
End If
auxClass.Conn.gConn_Close()
End Select

Select Case Request.QueryString("_mode_")
 Case "2"
Dim auxClass As New clscusimDOC
auxClass.Conn.gConn_Open()
Dim auxBagValues As clshrcBagValues = auxClass.gSystem_PostAction_v2(100013,Val(Request.QueryString("_mode_")),Databinder.Eval(frmdatos.DataItem ,"grpcod"), Nothing)
If auxBagValues IsNot Nothing Then
     Dim auxValue As Object
End If
auxClass.Conn.gConn_Close()
End Select

Select Case Request.QueryString("_mode_")
 Case "2"
Dim auxClass As New clscusimDOC
auxClass.Conn.gConn_Open()
Dim auxBagValues As clshrcBagValues = auxClass.gSystem_PostAction_v2(100013,Val(Request.QueryString("_mode_")),Databinder.Eval(frmdatos.DataItem ,"grpcod"), Nothing)
If auxBagValues IsNot Nothing Then
     Dim auxValue As Object
End If
auxClass.Conn.gConn_Close()
End Select

Select Case Request.QueryString("_mode_")
 Case "2"
Dim auxClass As New clscusimDOC
auxClass.Conn.gConn_Open()
Dim auxBagValues As clshrcBagValues = auxClass.gSystem_PostAction_v2(100013,Val(Request.QueryString("_mode_")),Databinder.Eval(frmdatos.DataItem ,"grpcod"), Nothing)
If auxBagValues IsNot Nothing Then
     Dim auxValue As Object
End If
auxClass.Conn.gConn_Close()
End Select

Select Case Request.QueryString("_mode_")
 Case "2"
Dim auxClass As New clscusimDOC
auxClass.Conn.gConn_Open()
Dim auxBagValues As clshrcBagValues = auxClass.gSystem_PostAction_v2(100013,Val(Request.QueryString("_mode_")),Databinder.Eval(frmdatos.DataItem ,"grpcod"), Nothing)
If auxBagValues IsNot Nothing Then
     Dim auxValue As Object
End If
auxClass.Conn.gConn_Close()
End Select

Select Case Request.QueryString("_mode_")
 Case "2"
Dim auxClass As New clscusimDOC
auxClass.Conn.gConn_Open()
Dim auxBagValues As clshrcBagValues = auxClass.gSystem_PostAction_v2(100013,Val(Request.QueryString("_mode_")),Databinder.Eval(frmdatos.DataItem ,"grpcod"), Nothing)
If auxBagValues IsNot Nothing Then
     Dim auxValue As Object
End If
auxClass.Conn.gConn_Close()
End Select

Select Case Request.QueryString("_mode_")
 Case "2"
Dim auxClass As New clscusimDOC
auxClass.Conn.gConn_Open()
Dim auxBagValues As clshrcBagValues = auxClass.gSystem_PostAction_v2(100013,Val(Request.QueryString("_mode_")),Databinder.Eval(frmdatos.DataItem ,"grpcod"), Nothing)
If auxBagValues IsNot Nothing Then
     Dim auxValue As Object
End If
auxClass.Conn.gConn_Close()
End Select

Select Case Request.QueryString("_mode_")
 Case "2"
Dim auxClass As New clscusimDOC
auxClass.Conn.gConn_Open()
Dim auxBagValues As clshrcBagValues = auxClass.gSystem_PostAction_v2(100013,Val(Request.QueryString("_mode_")),Databinder.Eval(frmdatos.DataItem ,"grpcod"), Nothing)
If auxBagValues IsNot Nothing Then
     Dim auxValue As Object
End If
auxClass.Conn.gConn_Close()
End Select

Select Case Request.QueryString("_mode_")
 Case "2"
Dim auxClass As New clscusimDOC
auxClass.Conn.gConn_Open()
Dim auxBagValues As clshrcBagValues = auxClass.gSystem_PostAction_v2(100013,Val(Request.QueryString("_mode_")),Databinder.Eval(frmdatos.DataItem ,"grpcod"), Nothing)
If auxBagValues IsNot Nothing Then
     Dim auxValue As Object
End If
auxClass.Conn.gConn_Close()
End Select

Select Case Request.QueryString("_mode_")
 Case "2"
Dim auxClass As New clscusimDOC
auxClass.Conn.gConn_Open()
Dim auxBagValues As clshrcBagValues = auxClass.gSystem_PostAction_v2(100013,Val(Request.QueryString("_mode_")),Databinder.Eval(frmdatos.DataItem ,"grpcod"), Nothing)
If auxBagValues IsNot Nothing Then
     Dim auxValue As Object
End If
auxClass.Conn.gConn_Close()
End Select

Select Case Request.QueryString("_mode_")
 Case "2"
Dim auxClass As New clscusimDOC
auxClass.Conn.gConn_Open()
Dim auxBagValues As clshrcBagValues = auxClass.gSystem_PostAction_v2(100013,Val(Request.QueryString("_mode_")),Databinder.Eval(frmdatos.DataItem ,"grpcod"), Nothing)
If auxBagValues IsNot Nothing Then
     Dim auxValue As Object
End If
auxClass.Conn.gConn_Close()
End Select

Select Case Request.QueryString("_mode_")
 Case "2"
Dim auxClass As New clscusimDOC
auxClass.Conn.gConn_Open()
Dim auxBagValues As clshrcBagValues = auxClass.gSystem_PostAction_v2(100013,Val(Request.QueryString("_mode_")),Databinder.Eval(frmdatos.DataItem ,"grpcod"), Nothing)
If auxBagValues IsNot Nothing Then
     Dim auxValue As Object
End If
auxClass.Conn.gConn_Close()
End Select

Select Case Request.QueryString("_mode_")
 Case "2"
Dim auxClass As New clscusimDOC
auxClass.Conn.gConn_Open()
Dim auxBagValues As clshrcBagValues = auxClass.gSystem_PostAction_v2(100013,Val(Request.QueryString("_mode_")),Databinder.Eval(frmdatos.DataItem ,"grpcod"), Nothing)
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
         
    Dim auxResult as String=auxClass.gSystemConfig_PostAction("Q_SECPGRP",12,auxDataKeyCod) 
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
  Dim auxResult as String=auxClass.gSystem_PostAction(100013,12,e.Command.Parameters(0).Value) 
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

Protected Sub dsedtsidcod_Init(ByVal sender As Object,ByVal e As System.EventArgs)
sender.ConnectionString=Session("connectionstringname")
End Sub
Protected Sub dsedtsidcod_Deleted(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.SqlDataSourceStatusEventArgs)
If e.Exception IsNot Nothing Then
    
Else
    
End If

End Sub
Protected Sub dsedtsidcod_Inserted(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.SqlDataSourceStatusEventArgs)
If e.Exception IsNot Nothing Then
    
Else
    
End If

End Sub
Protected Sub dsedtsidcod_Selected(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.SqlDataSourceStatusEventArgs)
If e.Exception IsNot Nothing Then
    
Else
    
End If

End Sub
Protected Sub dsedtsidcod_Updated(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.SqlDataSourceStatusEventArgs)
If e.Exception IsNot Nothing Then
    
Else
    
End If

End Sub

Protected Sub edtsidcodview_DataBinding(ByVal sender As Object, ByVal e As System.EventArgs)
Dim auxValue as Int32=-1
If IsDBNull(Eval("sidcodvalue")) Then 
     Ctype(sender,Control).Visible=False
Else
objectexplorer_SetValue(Eval("sidcodvalue"),Eval("sidcod"),Eval("sidcoddsc").ToString,CType(sender.parent.FindControl("edtsidcodview"),LinkButton),CType(sender.parent.FindControl("edtsidcodvalue"),HiddenField),CType(sender.parent.FindControl("edtsidcod"),HiddenField),edtsidcoddelete)
End If

End Sub
Protected Sub edtsidcoddelete_Click (ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs)
edtsidcod.Value=-1
edtsidcodvalue.Value=-1
edtsidcodview.Visible=False
edtsidcoddelete.Visible=False

End Sub
Protected Sub edtsidcoddelete_DataBinding(ByVal sender As Object, ByVal e As System.EventArgs)
If Not IsPostBack  Then 
 If IsDBNull(Eval("sidcodvalue")) Then 
    CType(sender,Control).Visible=False
  ElseIf Eval("sidcodvalue") < 1 Then
    CType(sender,Control).Visible=False
  Else
    CType(sender,Control).Visible=True
 End If
End If

End Sub
Protected Sub cmdedtsidcodshowpanel_Click (ByVal sender As Object, ByVal e As System.EventArgs)
objectexplorer_Show(CType(sender.parent.FindControl("edtsidcodvalue"),HiddenField).UniqueID,CType(sender.parent.FindControl("edtsidcodview"),LinkButton).UniqueID,CType(sender.parent.FindControl("edtsidcod"),HiddenField).UniqueID,"100013{#CHR34#}100015{#CHR34#}","100013{#CHR34#}100015{#CHR34#}",CType(sender.parent.FindControl("edtsidcoddelete"),ImageButton).UniqueID)
End Sub
Protected Sub edtsidcodvalue_Databound(ByVal sender As Object, ByVal e As System.EventArgs)
If IsDBNull(Eval("sidcodvalue")) Then
 CType(sender,HiddenField).Value=""
Else
 CType(sender,HiddenField).Value=Eval("sidcodvalue").ToString()
End If

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

Protected Sub dsinssidcod_Init(ByVal sender As Object,ByVal e As System.EventArgs)
sender.ConnectionString=Session("connectionstringname")
End Sub
Protected Sub dsinssidcod_Deleted(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.SqlDataSourceStatusEventArgs)
If e.Exception IsNot Nothing Then
    
Else
    
End If

End Sub
Protected Sub dsinssidcod_Inserted(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.SqlDataSourceStatusEventArgs)
If e.Exception IsNot Nothing Then
    
Else
    
End If

End Sub
Protected Sub dsinssidcod_Selected(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.SqlDataSourceStatusEventArgs)
If e.Exception IsNot Nothing Then
    
Else
    
End If

End Sub
Protected Sub dsinssidcod_Updated(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.SqlDataSourceStatusEventArgs)
If e.Exception IsNot Nothing Then
    
Else
    
End If

End Sub

Protected Sub inssidcoddelete_Click (ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs)
inssidcod.Value=-1
inssidcodvalue.Value=-1
inssidcodview.Visible=False
inssidcoddelete.Visible=False

End Sub
Protected Sub inssidcoddelete_DataBinding(ByVal sender As Object, ByVal e As System.EventArgs)
If Not IsPostBack  Then 
 If IsDBNull(Eval("sidcodvalue")) Then 
    CType(sender,Control).Visible=False
  ElseIf Eval("sidcodvalue") < 1 Then
    CType(sender,Control).Visible=False
  Else
    CType(sender,Control).Visible=True
 End If
End If

End Sub
Protected Sub cmdinssidcodshowpanel_Click (ByVal sender As Object, ByVal e As System.EventArgs)
objectexplorer_Show(CType(sender.parent.FindControl("inssidcodvalue"),HiddenField).UniqueID,CType(sender.parent.FindControl("inssidcodview"),LinkButton).UniqueID,CType(sender.parent.FindControl("inssidcod"),HiddenField).UniqueID,"100013{#CHR34#}100015{#CHR34#}","100013{#CHR34#}100015{#CHR34#}",CType(sender.parent.FindControl("inssidcoddelete"),ImageButton).UniqueID)
End Sub

Protected Sub cmdfrmupdpanelctrl000004cancel_Click (ByVal sender As Object, ByVal e As System.EventArgs)
updpanelfrmupdpanelctrl000004.Hide()
End Sub

Protected Sub cmdfrmupdpanelctrl000004insert_Click (ByVal sender As Object, ByVal e As System.EventArgs)
Dim auxEndOK as Boolean=True
Try
    Dim auxSecurity as clsHrcSecurityClient=Session("security")
    Select Case inssidcod.Value
    Case 100013
      Server.ScriptTimeout = 1800
       ScriptManager.GetCurrent(Me).AsyncPostBackTimeout = 360000
      auxSecurity.gGroup_AddGroup(CInt(Request.QueryString("param1")), CInt(inssidcodvalue.Value))
    Case 100015
      Server.ScriptTimeout = 1800
       ScriptManager.GetCurrent(Me).AsyncPostBackTimeout = 360000
      auxSecurity.gGroup_AddLogin(CInt(Request.QueryString("param1")), CInt(inssidcodvalue.Value))
    End Select
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
    auxClass.Conn.gConn_Delete("DELETE FROM Q_SECPGRP WHERE grpcod = " & auxDataKeyCod & "")
auxClass.Conn.gConn_Delete("DELETE FROM Q_SECPLOGIN WHERE seccod = " & auxDataKeyCod & "")
Select Case Val(CType(sender.FindControl("inssidcod"),HiddenField).Value)
Case 100013
Case 100015
End Select

    Dim auxResult as String=auxClass.gSystemConfig_PostAction("Q_SECPGRPMBR",11,auxDataKeyCod) 
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
         auxClass.Conn.gConn_Delete("DELETE FROM Q_SECPGRP WHERE grpcod = " & auxDataKeyCod & "")
auxClass.Conn.gConn_Delete("DELETE FROM Q_SECPLOGIN WHERE seccod = " & auxDataKeyCod & "")
Select Case Val(CType(sender.FindControl("edtsidcod"),HiddenField).Value)
Case 100013
Case 100015
End Select

    Dim auxResult as String=auxClass.gSystemConfig_PostAction("Q_SECPGRPMBR",12,auxDataKeyCod) 
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
Case 100013
    auxQuery = "SELECT Q_SECPGRP.grpcod,Q_SECPGRP.grpdsc,100013 as objecttype FROM Q_SECPGRP   WHERE (Q_SECPGRP.grpinherit = {#PARAM1#}) AND Q_SECPGRP.grpcod >= 1"
Case 100015
    auxQuery = "SELECT seccod as cod,secdsc as dsc,100015 as objecttype FROM Q_SECPLOGIN   WHERE (((('{#PARAM2#}'='')  OR  (Q_SECPLOGIN.secdsc LIKE '%{#PARAM2#}%' AND '{#PARAM2#}'<>''))) AND (Q_SECPLOGIN.secbaja = 0 OR Q_SECPLOGIN.secbaja  IS NULL)) AND Q_SECPLOGIN.seccod >= 1"
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
    Case 100013
        pLinkButton.OnClientClick = "javascript:if (window.showModalDialog) {window.showModalDialog(" & chr(39) & "frmGrupos_det.aspx?_mode_=0&_closea_=1&isDlg=1&param1=" & pValue & "&timestamp=" & chr(39) & " + new Date().getTime() + " & chr(39) & "" & chr(39) & ", " & chr(39) & "" & chr(39) & ", " & chr(39) & "menubar=no;resizable=yes;toolbar=no;status=no;center=yes;dialogWidth=750px;dialogHeight=570px;" & chr(39) & ");} else {window.open(" & chr(39) & "frmGrupos_det.aspx?_mode_=0&_closea_=1&isDlg=1&param1=" & pValue & "&timestamp=" & chr(39) & " + new Date().getTime() + " & chr(39) & "" & chr(39) & ", " & chr(39) & "" & chr(39) & ", " & chr(39) & "modal=yes;directories=no;menubar=no;resizable=yes;toolbar=no;status=no;center=yes;dialogWidth=750px;dialogHeight=570px;" & chr(39) & ");};return false;"
    Case 100015
        pLinkButton.OnClientClick = "javascript:if (window.showModalDialog) {window.showModalDialog(" & chr(39) & "frmUsuarios_det.aspx?_mode_=0&_closea_=1&isDlg=1&param1=" & pValue & "&timestamp=" & chr(39) & " + new Date().getTime() + " & chr(39) & "" & chr(39) & ", " & chr(39) & "" & chr(39) & ", " & chr(39) & "menubar=no;resizable=yes;toolbar=no;status=no;center=yes;dialogWidth=750px;dialogHeight=570px;" & chr(39) & ");} else {window.open(" & chr(39) & "frmUsuarios_det.aspx?_mode_=0&_closea_=1&isDlg=1&param1=" & pValue & "&timestamp=" & chr(39) & " + new Date().getTime() + " & chr(39) & "" & chr(39) & ", " & chr(39) & "" & chr(39) & ", " & chr(39) & "modal=yes;directories=no;menubar=no;resizable=yes;toolbar=no;status=no;center=yes;dialogWidth=750px;dialogHeight=570px;" & chr(39) & ");};return false;"
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
    Case 100013
      auxStartNode= New TreeNode("<img width=14 height=14 border=0 src='imagenes/icon00100013.png' />Grupos", "00100013")
tvwmodalpopuppnlobjectexplorer.Nodes.Add(auxStartNode)
modalpopuppnlobjectexplorer_Load(-1, auxStartNode, auxObjectID)
    Case 100015
      auxStartNode= New TreeNode("<img width=14 height=14 border=0 src='imagenes/icon00100015.png' />Usuarios", "00100015")
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
    modalpopuppnlobjectexplorer_Select(auxSelectedValue, 100013, CType(sender, TreeView).SelectedNode.Text)
Else
    Dim auxQuery As String = ""
    If (auxObjectType = "100013") And Instr(ViewState("modalpopuppnlobjectexplorer_mode"), "{#CHR34#}100013{#CHR34#}") > 0 Then
        If auxQuery <> "" Then auxQuery &= " UNION ALL "
        auxQuery &= "(SELECT grpcod as cod,grpdsc as dsc,100013 as objecttype,'Grupo' as objecttypedsc FROM Q_SECPGRP   WHERE ((((Q_SECPGRP.grpinherit =" & auxSelectedValue & ") AND '" & txtobjectexplorerfilter.Text.Trim & "'='')  OR  (Q_SECPGRP.grpdsc LIKE '%" & txtobjectexplorerfilter.Text.Trim & "%' AND '" & txtobjectexplorerfilter.Text.Trim & "'<>''))) AND Q_SECPGRP.grpcod >= 1)"
    End If
    If (auxObjectType = "100015") And Instr(ViewState("modalpopuppnlobjectexplorer_mode"), "{#CHR34#}100015{#CHR34#}") > 0 Then
        If auxQuery <> "" Then auxQuery &= " UNION ALL "
        auxQuery &= "(SELECT Q_SECPLOGIN.seccod as cod,Q_SECPLOGIN.secdsc as dsc,100015 as objecttype,'Usuario' as objecttypedsc FROM Q_SECPLOGIN   WHERE (((('" & txtobjectexplorerfilter.Text.Trim & "'='')  OR  (Q_SECPLOGIN.secdsc LIKE '%" & txtobjectexplorerfilter.Text.Trim & "%' AND '" & txtobjectexplorerfilter.Text.Trim & "'<>''))) AND (Q_SECPLOGIN.secbaja = 0 OR Q_SECPLOGIN.secbaja  IS NULL)) AND Q_SECPLOGIN.seccod >= 1)"
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
         Response.Redirect(coWebRootFolder & "frmGrupos.aspx?_mode_=7&_closea_=0")
     End If

End Sub
Protected Sub frmGrupos_det_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
Dim auxSecurity as clsHrcSecurityClient= Session("security"),clsHrcSecurityClient
Dim auxConnn as clsHrcConnClient= Session("conn"),clsHrcConnClient
Dim auxPermRead as Boolean=False
If Session("isadmin") Or auxSecurity.gMember_IsInGroupByID(clshrcimDOC.coGroupIDAdmins) Then
    auxPermRead=True
ElseIf auxSecurity.gMember_IsInGroupByID(clshrcimDOC.coGroupIDAccountAdmins) Then
   auxPermRead=Not CInt(Request.QueryString("param1")) = auxSecurity.gGroup_GetCodByID(clshrcimDOC.coGroupIDAdmins)
End If
m_PermFormEdit=auxPermRead
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

