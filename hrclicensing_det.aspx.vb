Imports System.Data
Imports System.Data.SqlClient
Imports Intelimedia.imComponentes
Public Class hrclicensing_det
Inherits System.Web.UI.Page
Friend m_PermFormDelete as Boolean=False
Friend m_PermFormEdit as Boolean=False
Friend m_PermFormNew as Boolean=False
Protected Sub itemctrl000018_Databound(ByVal sender As Object, ByVal e As System.EventArgs)
CType(sender,Label).Text=Eval("liccod").ToString

End Sub

Protected Sub itemctrl000016_Databound(ByVal sender As Object, ByVal e As System.EventArgs)
CType(sender,Label).Text=Eval("liccod").ToString

End Sub

Protected Sub itemctrl000015_Databound(ByVal sender As Object, ByVal e As System.EventArgs)
CType(sender,Label).Text=Replace(Eval("licdsc").ToString,Chr(10),"<br />")

End Sub

Protected Sub itemctrl000014view_DataBinding(ByVal sender As Object, ByVal e As System.EventArgs)
If IsDBNull(Eval("lickyaautcod")) Then 
     Ctype(sender,Control).Visible=False
Else
  If Eval("lickyaautcod") = -1 Then 
     Ctype(sender,Control).Visible=False
  End If
End If

End Sub

Protected Sub itemctrl000013view_DataBinding(ByVal sender As Object, ByVal e As System.EventArgs)
If IsDBNull(Eval("lickyacod")) Then 
     Ctype(sender,Control).Visible=False
Else
  If Eval("lickyacod") = -1 Then 
     Ctype(sender,Control).Visible=False
  End If
End If

End Sub

Protected Sub itemctrl000012_Databound(ByVal sender As Object, ByVal e As System.EventArgs)
CType(sender,Label).Text=Replace(Eval("licrequestID").ToString,Chr(10),"<br />")

End Sub

Protected Sub itemctrl000011_Databound(ByVal sender As Object, ByVal e As System.EventArgs)
CType(sender,Label).Text=Replace(Eval("licconnuser").ToString,Chr(10),"<br />")

End Sub

Protected Sub itemctrl000010_Databound(ByVal sender As Object, ByVal e As System.EventArgs)
CType(sender,Label).Text="*****"

End Sub

Protected Sub itemctrl000009_Databound(ByVal sender As Object, ByVal e As System.EventArgs)
CType(sender,Label).Text=HttpUtility.HtmlEncode(Replace(Eval("licrequest").ToString,Chr(10),"<br />"))

End Sub

Protected Sub itemctrl000008_Databound(ByVal sender As Object, ByVal e As System.EventArgs)
CType(sender,Label).Text=HttpUtility.HtmlEncode(Replace(Eval("licresult").ToString,Chr(10),"<br />"))

End Sub

Protected Sub itemctrl000007_Databound(ByVal sender As Object, ByVal e As System.EventArgs)
CType(sender,Label).Text="<img src=imagenes/"
If IsDBNull(Eval("licenabled")) Then
CType(sender,Label).Text &="actundefined.gif alt=S/D "
ElseIf Eval("licenabled") Then
CType(sender,Label).Text &="actyes.gif alt=Si "
Else
CType(sender,Label).Text &="actno.gif alt=No "
End If
CType(sender,Label).Text &=" width=12px border=0px />"

End Sub

Protected Sub itemctrl000004colitem1view_DataBinding(ByVal sender As Object, ByVal e As System.EventArgs)
If IsDBNull(Eval("licdetcod")) Then 
     Ctype(sender,Control).Visible=False
Else
  If Eval("licdetcod") = -1 Then 
     Ctype(sender,Control).Visible=False
  End If
End If

End Sub

Protected Sub itemctrl000004colitem2_Databound(ByVal sender As Object, ByVal e As System.EventArgs)
CType(sender,Label).Text=Replace(Eval("licdetval").ToString,Chr(10),"<br />")

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

Protected Sub editctrl000016_Databound(ByVal sender As Object, ByVal e As System.EventArgs)
CType(sender,Label).Text=Eval("liccod").ToString

End Sub

Protected Sub editctrl000015_DataBound_frmdatos(ByVal sender As Object, ByVal e As System.EventArgs)
CType(sender,TextBox).Text=Eval("licdsc").ToString

End Sub

Protected Sub dseditctrl000014_Init(ByVal sender As Object,ByVal e As System.EventArgs)
sender.ConnectionString=Session("connectionstringname")
End Sub
Protected Sub dseditctrl000014_Deleted(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.SqlDataSourceStatusEventArgs)
If e.Exception IsNot Nothing Then
  lblerror.Text &="//Error borrando datos (" & e.Exception.Message & ")"
    
Else
    
End If

End Sub
Protected Sub dseditctrl000014_Inserted(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.SqlDataSourceStatusEventArgs)
If e.Exception IsNot Nothing Then
  lblerror.Text &="//Error insertando datos (" & e.Exception.Message & ")"
    
Else
    
End If

End Sub
Protected Sub dseditctrl000014_Selected(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.SqlDataSourceStatusEventArgs)
If e.Exception IsNot Nothing Then
  lblerror.Text &="//Error buscando datos (" & e.Exception.Message & ")"
    
Else
    
End If

End Sub
Protected Sub dseditctrl000014_Updated(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.SqlDataSourceStatusEventArgs)
If e.Exception IsNot Nothing Then
  lblerror.Text &="//Error actualizando datos (" & e.Exception.Message & ")"
    
Else
    
End If

End Sub

Protected Sub editctrl000014_Databound(ByVal sender As Object, ByVal e As System.EventArgs)
    If IsDBNull(DataBinder.Eval(frmdatos.DataItem,"lickyaautcod")) = False  Then
        If CType(sender,DropDownList).Items.FindByValue(DataBinder.Eval(frmdatos.DataItem,"lickyaautcod")) IsNot Nothing Then
            CType(sender,DropDownList).SelectedValue=DataBinder.Eval(frmdatos.DataItem,"lickyaautcod")
        End If
    End If

End Sub
Protected Sub editctrl000014_comborefresh_Click (ByVal sender As Object, ByVal e As System.EventArgs)
Dim auxeditctrl000014dropdownlist As DropDownList = CType(sender.parent.FindControl("editctrl000014"), DropDownList)
If auxeditctrl000014dropdownlist IsNot Nothing Then
    Dim auxeditctrl000014val As String = auxeditctrl000014dropdownlist.SelectedValue
             CType(auxeditctrl000014dropdownlist,DropDownList).DataBind
             If CType(auxeditctrl000014dropdownlist,DropDownList).Items.FindByValue(auxeditctrl000014val) IsNot Nothing Then
                 CType(auxeditctrl000014dropdownlist,DropDownList).ClearSelection()
                 CType(auxeditctrl000014dropdownlist,DropDownList).Items.FindByValue(auxeditctrl000014val).Selected = True
             End If
End If

End Sub
Protected Sub editctrl000014view_DataBinding(ByVal sender As Object, ByVal e As System.EventArgs)
If IsDBNull(Eval("lickyaautcod")) Then 
     Ctype(sender,Control).Visible=False
Else
  If Eval("lickyaautcod") = -1 Then 
     Ctype(sender,Control).Visible=False
  End If
End If

End Sub

Protected Sub dseditctrl000013_Init(ByVal sender As Object,ByVal e As System.EventArgs)
sender.ConnectionString=Session("connectionstringname")
End Sub
Protected Sub dseditctrl000013_Deleted(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.SqlDataSourceStatusEventArgs)
If e.Exception IsNot Nothing Then
  lblerror.Text &="//Error borrando datos (" & e.Exception.Message & ")"
    
Else
    
End If

End Sub
Protected Sub dseditctrl000013_Inserted(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.SqlDataSourceStatusEventArgs)
If e.Exception IsNot Nothing Then
  lblerror.Text &="//Error insertando datos (" & e.Exception.Message & ")"
    
Else
    
End If

End Sub
Protected Sub dseditctrl000013_Selected(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.SqlDataSourceStatusEventArgs)
If e.Exception IsNot Nothing Then
  lblerror.Text &="//Error buscando datos (" & e.Exception.Message & ")"
    
Else
    
End If

End Sub
Protected Sub dseditctrl000013_Updated(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.SqlDataSourceStatusEventArgs)
If e.Exception IsNot Nothing Then
  lblerror.Text &="//Error actualizando datos (" & e.Exception.Message & ")"
    
Else
    
End If

End Sub

Protected Sub editctrl000013_Databound(ByVal sender As Object, ByVal e As System.EventArgs)
    If IsDBNull(DataBinder.Eval(frmdatos.DataItem,"lickyacod")) = False  Then
        If CType(sender,DropDownList).Items.FindByValue(DataBinder.Eval(frmdatos.DataItem,"lickyacod")) IsNot Nothing Then
            CType(sender,DropDownList).SelectedValue=DataBinder.Eval(frmdatos.DataItem,"lickyacod")
        End If
    End If

End Sub
Protected Sub editctrl000013_comborefresh_Click (ByVal sender As Object, ByVal e As System.EventArgs)
Dim auxeditctrl000013dropdownlist As DropDownList = CType(sender.parent.FindControl("editctrl000013"), DropDownList)
If auxeditctrl000013dropdownlist IsNot Nothing Then
    Dim auxeditctrl000013val As String = auxeditctrl000013dropdownlist.SelectedValue
             CType(auxeditctrl000013dropdownlist,DropDownList).DataBind
             If CType(auxeditctrl000013dropdownlist,DropDownList).Items.FindByValue(auxeditctrl000013val) IsNot Nothing Then
                 CType(auxeditctrl000013dropdownlist,DropDownList).ClearSelection()
                 CType(auxeditctrl000013dropdownlist,DropDownList).Items.FindByValue(auxeditctrl000013val).Selected = True
             End If
End If

End Sub
Protected Sub editctrl000013view_DataBinding(ByVal sender As Object, ByVal e As System.EventArgs)
If IsDBNull(Eval("lickyacod")) Then 
     Ctype(sender,Control).Visible=False
Else
  If Eval("lickyacod") = -1 Then 
     Ctype(sender,Control).Visible=False
  End If
End If

End Sub

Protected Sub editctrl000012_DataBound_frmdatos(ByVal sender As Object, ByVal e As System.EventArgs)
CType(sender,TextBox).Text=Eval("licrequestID").ToString

End Sub

Protected Sub editctrl000011_DataBound_frmdatos(ByVal sender As Object, ByVal e As System.EventArgs)
CType(sender,TextBox).Text=Eval("licconnuser").ToString

End Sub

Protected Sub editctrl000010_DataBound_frmdatos(ByVal sender As Object, ByVal e As System.EventArgs)
CType(sender,TextBox).Text=Eval("licconnpwd").ToString()

End Sub
Protected Sub editctrl000010_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs)
Dim auxClass as New clscusimDOC
 ViewState(CType(sender, TextBox).ClientID & "_changed")=CType(sender, TextBox).Text
 CType(sender, TextBox).Text = auxClass.gCrypt_Encrypt(CType(sender, TextBox).Text)

End Sub

Protected Sub editctrl000009_DataBound_frmdatos(ByVal sender As Object, ByVal e As System.EventArgs)
CType(sender,TextBox).Text=Eval("licrequest").ToString
End Sub

Protected Sub editctrl000008_DataBound_frmdatos(ByVal sender As Object, ByVal e As System.EventArgs)
CType(sender,TextBox).Text=Eval("licresult").ToString
End Sub

Protected Sub editctrl000007_Databound(ByVal sender As Object, ByVal e As System.EventArgs)
    If IsDBNull(DataBinder.Eval(frmdatos.DataItem,"licenabled"))  Then
        CType(sender,CheckBox).Checked=False
    Else
        CType(sender,CheckBox).Checked=DataBinder.Eval(frmdatos.DataItem,"licenabled")
    End If

End Sub

Protected Sub editctrl000004colitem1view_DataBinding(ByVal sender As Object, ByVal e As System.EventArgs)
If IsDBNull(Eval("licdetcod")) Then 
     Ctype(sender,Control).Visible=False
Else
  If Eval("licdetcod") = -1 Then 
     Ctype(sender,Control).Visible=False
  End If
End If

End Sub

Protected Sub editctrl000004colitem2_Databound(ByVal sender As Object, ByVal e As System.EventArgs)
CType(sender,Label).Text=Replace(Eval("licdetval").ToString,Chr(10),"<br />")

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
      lblfrmupdpanelctrl000004subtitle.Text="Visualizacion de Valor"
          updpanelfrmupdpanelctrl000004.Show()
          updupdpanelfrmupdpanelctrl000004.Update()
  Case "UPDATE"
  Case "EDIT"
          dsfrmupdpanelctrl000004.SelectParameters(frmupdpanelfrmupdpanelctrl000004.DataKeyNames(0)).DefaultValue=e.CommandArgument
          frmupdpanelfrmupdpanelctrl000004.ChangeMode(FormViewMode.Edit)
          frmupdpanelfrmupdpanelctrl000004.DataBind()
      lblfrmupdpanelctrl000004subtitle.Text="Edición de Valor"
          updpanelfrmupdpanelctrl000004.Show()
          updupdpanelfrmupdpanelctrl000004.Update()
  Case "HRC_INSERT"
          frmupdpanelfrmupdpanelctrl000004.ChangeMode(FormViewMode.Insert)
      lblfrmupdpanelctrl000004subtitle.Text="Nuevo en Valor"
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

Protected Sub dsinsctrl000014_Init(ByVal sender As Object,ByVal e As System.EventArgs)
sender.ConnectionString=Session("connectionstringname")
End Sub
Protected Sub dsinsctrl000014_Deleted(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.SqlDataSourceStatusEventArgs)
If e.Exception IsNot Nothing Then
  lblerror.Text &="//Error borrando datos (" & e.Exception.Message & ")"
    
Else
    
End If

End Sub
Protected Sub dsinsctrl000014_Inserted(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.SqlDataSourceStatusEventArgs)
If e.Exception IsNot Nothing Then
  lblerror.Text &="//Error insertando datos (" & e.Exception.Message & ")"
    
Else
    
End If

End Sub
Protected Sub dsinsctrl000014_Selected(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.SqlDataSourceStatusEventArgs)
If e.Exception IsNot Nothing Then
  lblerror.Text &="//Error buscando datos (" & e.Exception.Message & ")"
    
Else
    
End If

End Sub
Protected Sub dsinsctrl000014_Updated(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.SqlDataSourceStatusEventArgs)
If e.Exception IsNot Nothing Then
  lblerror.Text &="//Error actualizando datos (" & e.Exception.Message & ")"
    
Else
    
End If

End Sub

Protected Sub insctrl000014_comborefresh_Click (ByVal sender As Object, ByVal e As System.EventArgs)
Dim auxinsctrl000014dropdownlist As DropDownList = CType(sender.parent.FindControl("insctrl000014"), DropDownList)
If auxinsctrl000014dropdownlist IsNot Nothing Then
    Dim auxinsctrl000014val As String = auxinsctrl000014dropdownlist.SelectedValue
             CType(auxinsctrl000014dropdownlist,DropDownList).DataBind
             If CType(auxinsctrl000014dropdownlist,DropDownList).Items.FindByValue(auxinsctrl000014val) IsNot Nothing Then
                 CType(auxinsctrl000014dropdownlist,DropDownList).ClearSelection()
                 CType(auxinsctrl000014dropdownlist,DropDownList).Items.FindByValue(auxinsctrl000014val).Selected = True
             End If
End If

End Sub

Protected Sub dsinsctrl000013_Init(ByVal sender As Object,ByVal e As System.EventArgs)
sender.ConnectionString=Session("connectionstringname")
End Sub
Protected Sub dsinsctrl000013_Deleted(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.SqlDataSourceStatusEventArgs)
If e.Exception IsNot Nothing Then
  lblerror.Text &="//Error borrando datos (" & e.Exception.Message & ")"
    
Else
    
End If

End Sub
Protected Sub dsinsctrl000013_Inserted(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.SqlDataSourceStatusEventArgs)
If e.Exception IsNot Nothing Then
  lblerror.Text &="//Error insertando datos (" & e.Exception.Message & ")"
    
Else
    
End If

End Sub
Protected Sub dsinsctrl000013_Selected(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.SqlDataSourceStatusEventArgs)
If e.Exception IsNot Nothing Then
  lblerror.Text &="//Error buscando datos (" & e.Exception.Message & ")"
    
Else
    
End If

End Sub
Protected Sub dsinsctrl000013_Updated(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.SqlDataSourceStatusEventArgs)
If e.Exception IsNot Nothing Then
  lblerror.Text &="//Error actualizando datos (" & e.Exception.Message & ")"
    
Else
    
End If

End Sub

Protected Sub insctrl000013_comborefresh_Click (ByVal sender As Object, ByVal e As System.EventArgs)
Dim auxinsctrl000013dropdownlist As DropDownList = CType(sender.parent.FindControl("insctrl000013"), DropDownList)
If auxinsctrl000013dropdownlist IsNot Nothing Then
    Dim auxinsctrl000013val As String = auxinsctrl000013dropdownlist.SelectedValue
             CType(auxinsctrl000013dropdownlist,DropDownList).DataBind
             If CType(auxinsctrl000013dropdownlist,DropDownList).Items.FindByValue(auxinsctrl000013val) IsNot Nothing Then
                 CType(auxinsctrl000013dropdownlist,DropDownList).ClearSelection()
                 CType(auxinsctrl000013dropdownlist,DropDownList).Items.FindByValue(auxinsctrl000013val).Selected = True
             End If
End If

End Sub

Protected Sub insctrl000010_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs)
Dim auxClass as New clscusimDOC
 ViewState(CType(sender, TextBox).ClientID & "_changed")=CType(sender, TextBox).Text
 CType(sender, TextBox).Text = auxClass.gCrypt_Encrypt(CType(sender, TextBox).Text)

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
         Response.Redirect(coWebRootFolder & "hrclicensing.aspx?_mode_=7&_closea_=0")
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
    Dim auxResult as String=auxClass.gSystemConfig_PostAction("Q_LIC",13,auxDataKeyCod) 
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
    
    Dim auxResult as String=auxClass.gSystemConfig_PostAction("Q_LIC",11,auxDataKeyCod) 
    lblerror.Text &=auxResult
    If auxResult ="" Then
    End If
    auxClass.Conn.gConn_Close
End If

End Sub
Protected Sub frmdatos_ItemSelected(ByVal sender As Object, ByVal e As System.EventArgs)
If frmdatos.DataItem IsNot Nothing Then
   Page.Title = "Licencias| " &  Databinder.Eval(frmdatos.DataItem ,"licdsc").ToString
   lblsubtitle.Text = Databinder.Eval(frmdatos.DataItem ,"licdsc").ToString
Else
   Page.Title = "Licencias| Nuevo"
   lblsubtitle.Text = "Nuevo"
End If


Select Case Request.QueryString("_mode_")
 Case "1"
 Case "25"
   lblsubtitle.Text = "Copiar"
    Try
        Dim auxConn As New SqlConnection(Session("connectionstringname"))
        auxConn.Open
        Dim auxDA As New SqlDataAdapter("SELECT Q_LIC.liccod,Q_LIC.licdsc,(Q_LICLICKYAAUTCOD.kyadsc) as lickyaautcoddsc,(Q_LICLICKYACOD.kyadsc) as lickyacoddsc,Q_LIC.licrequestID,Q_LIC.licconnuser,Q_LIC.licconnpwd,Q_LIC.licrequest,Q_LIC.licresult,Q_LIC.licenabled,(Q_LICQSECSID.secdsc) as qsecsiddsc,Q_LIC.qsecdatetime,Q_LIC.lickyaautcod,Q_LIC.lickyacod,Q_LIC.qsecsid FROM Q_LIC  LEFT JOIN Q_KYA AS Q_LICLICKYAAUTCOD ON Q_LICLICKYAAUTCOD.kyacod=Q_LIC.lickyaautcod LEFT JOIN Q_KYA AS Q_LICLICKYACOD ON Q_LICLICKYACOD.kyacod=Q_LIC.lickyacod LEFT JOIN Q_SECPLOGIN AS Q_LICQSECSID ON Q_LICQSECSID.sidcod=Q_LIC.qsecsid  WHERE Q_LIC.liccod=@param1", auxConn)
        auxDA.SelectCommand.Parameters.Add("@param1", SqlDbType.Int, 4).Value = Request.QueryString("param1")
        Dim auxDT As New DataTable
        auxDA.Fill(auxDT)
        auxConn.Close
        If auxDT.Rows.Count <> 0 Then
             If IsDBNull(auxDT.Rows(0)("licdsc")) = False Then
             CType(sender.parent.FindControl("frmdatos$insctrl000015"),TextBox).Text=auxDT.Rows(0)("licdsc")
             End If 
             If IsDBNull(auxDT.Rows(0)("lickyaautcod")) = False Then
             CType(sender.parent.FindControl("frmdatos$insctrl000014"),DropDownList).DataBind
             If CType(sender.parent.FindControl("frmdatos$insctrl000014"),DropDownList).Items.FindByValue(auxDT.Rows(0)("lickyaautcod")) IsNot Nothing Then
                 CType(sender.parent.FindControl("frmdatos$insctrl000014"),DropDownList).ClearSelection()
                 CType(sender.parent.FindControl("frmdatos$insctrl000014"),DropDownList).Items.FindByValue(auxDT.Rows(0)("lickyaautcod")).Selected = True
             End If
             End If 
             If IsDBNull(auxDT.Rows(0)("lickyacod")) = False Then
             CType(sender.parent.FindControl("frmdatos$insctrl000013"),DropDownList).DataBind
             If CType(sender.parent.FindControl("frmdatos$insctrl000013"),DropDownList).Items.FindByValue(auxDT.Rows(0)("lickyacod")) IsNot Nothing Then
                 CType(sender.parent.FindControl("frmdatos$insctrl000013"),DropDownList).ClearSelection()
                 CType(sender.parent.FindControl("frmdatos$insctrl000013"),DropDownList).Items.FindByValue(auxDT.Rows(0)("lickyacod")).Selected = True
             End If
             End If 
             If IsDBNull(auxDT.Rows(0)("licrequestID")) = False Then
             CType(sender.parent.FindControl("frmdatos$insctrl000012"),TextBox).Text=auxDT.Rows(0)("licrequestID")
             End If 
             If IsDBNull(auxDT.Rows(0)("licconnuser")) = False Then
             CType(sender.parent.FindControl("frmdatos$insctrl000011"),TextBox).Text=auxDT.Rows(0)("licconnuser")
             End If 
             If IsDBNull(auxDT.Rows(0)("licconnpwd")) = False Then
             CType(sender.parent.FindControl("frmdatos$insctrl000010"),TextBox).Text=auxDT.Rows(0)("licconnpwd")
             End If 
             If IsDBNull(auxDT.Rows(0)("licrequest")) = False Then
             CType(sender.parent.FindControl("frmdatos$insctrl000009"),TextBox).Text=auxDT.Rows(0)("licrequest")
             End If 
             If IsDBNull(auxDT.Rows(0)("licresult")) = False Then
             CType(sender.parent.FindControl("frmdatos$insctrl000008"),TextBox).Text=auxDT.Rows(0)("licresult")
             End If 
             If IsDBNull(auxDT.Rows(0)("licenabled")) = False Then
             CType(sender.parent.FindControl("frmdatos$insctrl000007"),CheckBox).Checked=auxDT.Rows(0)("licenabled")
             End If 
        End If
    Catch ex as sqlException
    End Try
End Select
Select Case Request.QueryString("_mode_")
 Case "1","25"
Dim auxClass As New clscusimDOC
auxClass.Conn.gConn_Open()
Dim auxBagValues As clshrcBagValues = auxClass.gSystem_PostAction_v2(100028,Val(Request.QueryString("_mode_")), -1, Nothing)
If auxBagValues IsNot Nothing Then
     Dim auxValue As Object
auxValue = auxBagValues.gValue_Get("lickyaautcod.FILTERVALUES")
If auxValue IsNot Nothing Then
 If auxBagValues.gValue_Get("HRC_CANCEL", False) Then
     gForm_Close()
 End If
 Dim auxValues As List(Of Integer) = auxValue
 CType(CType(sender.parent.FindControl("frmdatos$insctrl000014"), DropDownList).DataSourceObject, SqlDataSource).FilterExpression="liccod IN (" & auxClass.Conn.gFieldDB_GetString(auxValues) & ")"
 End If 
auxValue = auxBagValues.gValue_Get("lickyacod.FILTERVALUES")
If auxValue IsNot Nothing Then
 If auxBagValues.gValue_Get("HRC_CANCEL", False) Then
     gForm_Close()
 End If
 Dim auxValues As List(Of Integer) = auxValue
 CType(CType(sender.parent.FindControl("frmdatos$insctrl000013"), DropDownList).DataSourceObject, SqlDataSource).FilterExpression="liccod IN (" & auxClass.Conn.gFieldDB_GetString(auxValues) & ")"
 End If 
End If
auxClass.Conn.gConn_Close()
End Select

Select Case Request.QueryString("_mode_")
 Case "2"
Dim auxClass As New clscusimDOC
auxClass.Conn.gConn_Open()
Dim auxBagValues As clshrcBagValues = auxClass.gSystem_PostAction_v2(100028,Val(Request.QueryString("_mode_")),Databinder.Eval(frmdatos.DataItem ,"liccod"), Nothing)
If auxBagValues IsNot Nothing Then
     Dim auxValue As Object
auxValue = auxBagValues.gValue_Get("lickyaautcod.FILTERVALUES")
If auxValue IsNot Nothing Then
 Dim auxValues As List(Of Integer) = auxValue
 CType(CType(sender.parent.FindControl("frmdatos$editctrl000014"), DropDownList).DataSourceObject, SqlDataSource).FilterExpression="liccod IN (" & auxClass.Conn.gFieldDB_GetString(auxValues) & ")"
 End If 
auxValue = auxBagValues.gValue_Get("lickyacod.FILTERVALUES")
If auxValue IsNot Nothing Then
 Dim auxValues As List(Of Integer) = auxValue
 CType(CType(sender.parent.FindControl("frmdatos$editctrl000013"), DropDownList).DataSourceObject, SqlDataSource).FilterExpression="liccod IN (" & auxClass.Conn.gFieldDB_GetString(auxValues) & ")"
 End If 
End If
auxClass.Conn.gConn_Close()
End Select

Select Case Request.QueryString("_mode_")
 Case "2"
Dim auxClass As New clscusimDOC
auxClass.Conn.gConn_Open()
Dim auxBagValues As clshrcBagValues = auxClass.gSystem_PostAction_v2(100028,Val(Request.QueryString("_mode_")),Databinder.Eval(frmdatos.DataItem ,"liccod"), Nothing)
If auxBagValues IsNot Nothing Then
     Dim auxValue As Object
auxValue = auxBagValues.gValue_Get("lickyaautcod.FILTERVALUES")
If auxValue IsNot Nothing Then
 Dim auxValues As List(Of Integer) = auxValue
 CType(CType(sender.parent.FindControl("frmdatos$editctrl000014"), DropDownList).DataSourceObject, SqlDataSource).FilterExpression="liccod IN (" & auxClass.Conn.gFieldDB_GetString(auxValues) & ")"
 End If 
auxValue = auxBagValues.gValue_Get("lickyacod.FILTERVALUES")
If auxValue IsNot Nothing Then
 Dim auxValues As List(Of Integer) = auxValue
 CType(CType(sender.parent.FindControl("frmdatos$editctrl000013"), DropDownList).DataSourceObject, SqlDataSource).FilterExpression="liccod IN (" & auxClass.Conn.gFieldDB_GetString(auxValues) & ")"
 End If 
End If
auxClass.Conn.gConn_Close()
End Select

Select Case Request.QueryString("_mode_")
 Case "2"
Dim auxClass As New clscusimDOC
auxClass.Conn.gConn_Open()
Dim auxBagValues As clshrcBagValues = auxClass.gSystem_PostAction_v2(100028,Val(Request.QueryString("_mode_")),Databinder.Eval(frmdatos.DataItem ,"liccod"), Nothing)
If auxBagValues IsNot Nothing Then
     Dim auxValue As Object
auxValue = auxBagValues.gValue_Get("lickyaautcod.FILTERVALUES")
If auxValue IsNot Nothing Then
 Dim auxValues As List(Of Integer) = auxValue
 CType(CType(sender.parent.FindControl("frmdatos$editctrl000014"), DropDownList).DataSourceObject, SqlDataSource).FilterExpression="liccod IN (" & auxClass.Conn.gFieldDB_GetString(auxValues) & ")"
 End If 
auxValue = auxBagValues.gValue_Get("lickyacod.FILTERVALUES")
If auxValue IsNot Nothing Then
 Dim auxValues As List(Of Integer) = auxValue
 CType(CType(sender.parent.FindControl("frmdatos$editctrl000013"), DropDownList).DataSourceObject, SqlDataSource).FilterExpression="liccod IN (" & auxClass.Conn.gFieldDB_GetString(auxValues) & ")"
 End If 
End If
auxClass.Conn.gConn_Close()
End Select

Select Case Request.QueryString("_mode_")
 Case "2"
Dim auxClass As New clscusimDOC
auxClass.Conn.gConn_Open()
Dim auxBagValues As clshrcBagValues = auxClass.gSystem_PostAction_v2(100028,Val(Request.QueryString("_mode_")),Databinder.Eval(frmdatos.DataItem ,"liccod"), Nothing)
If auxBagValues IsNot Nothing Then
     Dim auxValue As Object
auxValue = auxBagValues.gValue_Get("lickyaautcod.FILTERVALUES")
If auxValue IsNot Nothing Then
 Dim auxValues As List(Of Integer) = auxValue
 CType(CType(sender.parent.FindControl("frmdatos$editctrl000014"), DropDownList).DataSourceObject, SqlDataSource).FilterExpression="liccod IN (" & auxClass.Conn.gFieldDB_GetString(auxValues) & ")"
 End If 
auxValue = auxBagValues.gValue_Get("lickyacod.FILTERVALUES")
If auxValue IsNot Nothing Then
 Dim auxValues As List(Of Integer) = auxValue
 CType(CType(sender.parent.FindControl("frmdatos$editctrl000013"), DropDownList).DataSourceObject, SqlDataSource).FilterExpression="liccod IN (" & auxClass.Conn.gFieldDB_GetString(auxValues) & ")"
 End If 
End If
auxClass.Conn.gConn_Close()
End Select

Select Case Request.QueryString("_mode_")
 Case "2"
Dim auxClass As New clscusimDOC
auxClass.Conn.gConn_Open()
Dim auxBagValues As clshrcBagValues = auxClass.gSystem_PostAction_v2(100028,Val(Request.QueryString("_mode_")),Databinder.Eval(frmdatos.DataItem ,"liccod"), Nothing)
If auxBagValues IsNot Nothing Then
     Dim auxValue As Object
auxValue = auxBagValues.gValue_Get("lickyaautcod.FILTERVALUES")
If auxValue IsNot Nothing Then
 Dim auxValues As List(Of Integer) = auxValue
 CType(CType(sender.parent.FindControl("frmdatos$editctrl000014"), DropDownList).DataSourceObject, SqlDataSource).FilterExpression="liccod IN (" & auxClass.Conn.gFieldDB_GetString(auxValues) & ")"
 End If 
auxValue = auxBagValues.gValue_Get("lickyacod.FILTERVALUES")
If auxValue IsNot Nothing Then
 Dim auxValues As List(Of Integer) = auxValue
 CType(CType(sender.parent.FindControl("frmdatos$editctrl000013"), DropDownList).DataSourceObject, SqlDataSource).FilterExpression="liccod IN (" & auxClass.Conn.gFieldDB_GetString(auxValues) & ")"
 End If 
End If
auxClass.Conn.gConn_Close()
End Select

Select Case Request.QueryString("_mode_")
 Case "2"
Dim auxClass As New clscusimDOC
auxClass.Conn.gConn_Open()
Dim auxBagValues As clshrcBagValues = auxClass.gSystem_PostAction_v2(100028,Val(Request.QueryString("_mode_")),Databinder.Eval(frmdatos.DataItem ,"liccod"), Nothing)
If auxBagValues IsNot Nothing Then
     Dim auxValue As Object
auxValue = auxBagValues.gValue_Get("lickyaautcod.FILTERVALUES")
If auxValue IsNot Nothing Then
 Dim auxValues As List(Of Integer) = auxValue
 CType(CType(sender.parent.FindControl("frmdatos$editctrl000014"), DropDownList).DataSourceObject, SqlDataSource).FilterExpression="liccod IN (" & auxClass.Conn.gFieldDB_GetString(auxValues) & ")"
 End If 
auxValue = auxBagValues.gValue_Get("lickyacod.FILTERVALUES")
If auxValue IsNot Nothing Then
 Dim auxValues As List(Of Integer) = auxValue
 CType(CType(sender.parent.FindControl("frmdatos$editctrl000013"), DropDownList).DataSourceObject, SqlDataSource).FilterExpression="liccod IN (" & auxClass.Conn.gFieldDB_GetString(auxValues) & ")"
 End If 
End If
auxClass.Conn.gConn_Close()
End Select

Select Case Request.QueryString("_mode_")
 Case "2"
Dim auxClass As New clscusimDOC
auxClass.Conn.gConn_Open()
Dim auxBagValues As clshrcBagValues = auxClass.gSystem_PostAction_v2(100028,Val(Request.QueryString("_mode_")),Databinder.Eval(frmdatos.DataItem ,"liccod"), Nothing)
If auxBagValues IsNot Nothing Then
     Dim auxValue As Object
auxValue = auxBagValues.gValue_Get("lickyaautcod.FILTERVALUES")
If auxValue IsNot Nothing Then
 Dim auxValues As List(Of Integer) = auxValue
 CType(CType(sender.parent.FindControl("frmdatos$editctrl000014"), DropDownList).DataSourceObject, SqlDataSource).FilterExpression="liccod IN (" & auxClass.Conn.gFieldDB_GetString(auxValues) & ")"
 End If 
auxValue = auxBagValues.gValue_Get("lickyacod.FILTERVALUES")
If auxValue IsNot Nothing Then
 Dim auxValues As List(Of Integer) = auxValue
 CType(CType(sender.parent.FindControl("frmdatos$editctrl000013"), DropDownList).DataSourceObject, SqlDataSource).FilterExpression="liccod IN (" & auxClass.Conn.gFieldDB_GetString(auxValues) & ")"
 End If 
End If
auxClass.Conn.gConn_Close()
End Select

Select Case Request.QueryString("_mode_")
 Case "2"
Dim auxClass As New clscusimDOC
auxClass.Conn.gConn_Open()
Dim auxBagValues As clshrcBagValues = auxClass.gSystem_PostAction_v2(100028,Val(Request.QueryString("_mode_")),Databinder.Eval(frmdatos.DataItem ,"liccod"), Nothing)
If auxBagValues IsNot Nothing Then
     Dim auxValue As Object
auxValue = auxBagValues.gValue_Get("lickyaautcod.FILTERVALUES")
If auxValue IsNot Nothing Then
 Dim auxValues As List(Of Integer) = auxValue
 CType(CType(sender.parent.FindControl("frmdatos$editctrl000014"), DropDownList).DataSourceObject, SqlDataSource).FilterExpression="liccod IN (" & auxClass.Conn.gFieldDB_GetString(auxValues) & ")"
 End If 
auxValue = auxBagValues.gValue_Get("lickyacod.FILTERVALUES")
If auxValue IsNot Nothing Then
 Dim auxValues As List(Of Integer) = auxValue
 CType(CType(sender.parent.FindControl("frmdatos$editctrl000013"), DropDownList).DataSourceObject, SqlDataSource).FilterExpression="liccod IN (" & auxClass.Conn.gFieldDB_GetString(auxValues) & ")"
 End If 
End If
auxClass.Conn.gConn_Close()
End Select

Select Case Request.QueryString("_mode_")
 Case "2"
Dim auxClass As New clscusimDOC
auxClass.Conn.gConn_Open()
Dim auxBagValues As clshrcBagValues = auxClass.gSystem_PostAction_v2(100028,Val(Request.QueryString("_mode_")),Databinder.Eval(frmdatos.DataItem ,"liccod"), Nothing)
If auxBagValues IsNot Nothing Then
     Dim auxValue As Object
auxValue = auxBagValues.gValue_Get("lickyaautcod.FILTERVALUES")
If auxValue IsNot Nothing Then
 Dim auxValues As List(Of Integer) = auxValue
 CType(CType(sender.parent.FindControl("frmdatos$editctrl000014"), DropDownList).DataSourceObject, SqlDataSource).FilterExpression="liccod IN (" & auxClass.Conn.gFieldDB_GetString(auxValues) & ")"
 End If 
auxValue = auxBagValues.gValue_Get("lickyacod.FILTERVALUES")
If auxValue IsNot Nothing Then
 Dim auxValues As List(Of Integer) = auxValue
 CType(CType(sender.parent.FindControl("frmdatos$editctrl000013"), DropDownList).DataSourceObject, SqlDataSource).FilterExpression="liccod IN (" & auxClass.Conn.gFieldDB_GetString(auxValues) & ")"
 End If 
End If
auxClass.Conn.gConn_Close()
End Select

Select Case Request.QueryString("_mode_")
 Case "2"
Dim auxClass As New clscusimDOC
auxClass.Conn.gConn_Open()
Dim auxBagValues As clshrcBagValues = auxClass.gSystem_PostAction_v2(100028,Val(Request.QueryString("_mode_")),Databinder.Eval(frmdatos.DataItem ,"liccod"), Nothing)
If auxBagValues IsNot Nothing Then
     Dim auxValue As Object
auxValue = auxBagValues.gValue_Get("lickyaautcod.FILTERVALUES")
If auxValue IsNot Nothing Then
 Dim auxValues As List(Of Integer) = auxValue
 CType(CType(sender.parent.FindControl("frmdatos$editctrl000014"), DropDownList).DataSourceObject, SqlDataSource).FilterExpression="liccod IN (" & auxClass.Conn.gFieldDB_GetString(auxValues) & ")"
 End If 
auxValue = auxBagValues.gValue_Get("lickyacod.FILTERVALUES")
If auxValue IsNot Nothing Then
 Dim auxValues As List(Of Integer) = auxValue
 CType(CType(sender.parent.FindControl("frmdatos$editctrl000013"), DropDownList).DataSourceObject, SqlDataSource).FilterExpression="liccod IN (" & auxClass.Conn.gFieldDB_GetString(auxValues) & ")"
 End If 
End If
auxClass.Conn.gConn_Close()
End Select

Select Case Request.QueryString("_mode_")
 Case "2"
Dim auxClass As New clscusimDOC
auxClass.Conn.gConn_Open()
Dim auxBagValues As clshrcBagValues = auxClass.gSystem_PostAction_v2(100028,Val(Request.QueryString("_mode_")),Databinder.Eval(frmdatos.DataItem ,"liccod"), Nothing)
If auxBagValues IsNot Nothing Then
     Dim auxValue As Object
auxValue = auxBagValues.gValue_Get("lickyaautcod.FILTERVALUES")
If auxValue IsNot Nothing Then
 Dim auxValues As List(Of Integer) = auxValue
 CType(CType(sender.parent.FindControl("frmdatos$editctrl000014"), DropDownList).DataSourceObject, SqlDataSource).FilterExpression="liccod IN (" & auxClass.Conn.gFieldDB_GetString(auxValues) & ")"
 End If 
auxValue = auxBagValues.gValue_Get("lickyacod.FILTERVALUES")
If auxValue IsNot Nothing Then
 Dim auxValues As List(Of Integer) = auxValue
 CType(CType(sender.parent.FindControl("frmdatos$editctrl000013"), DropDownList).DataSourceObject, SqlDataSource).FilterExpression="liccod IN (" & auxClass.Conn.gFieldDB_GetString(auxValues) & ")"
 End If 
End If
auxClass.Conn.gConn_Close()
End Select

Select Case Request.QueryString("_mode_")
 Case "2"
Dim auxClass As New clscusimDOC
auxClass.Conn.gConn_Open()
Dim auxBagValues As clshrcBagValues = auxClass.gSystem_PostAction_v2(100028,Val(Request.QueryString("_mode_")),Databinder.Eval(frmdatos.DataItem ,"liccod"), Nothing)
If auxBagValues IsNot Nothing Then
     Dim auxValue As Object
auxValue = auxBagValues.gValue_Get("lickyaautcod.FILTERVALUES")
If auxValue IsNot Nothing Then
 Dim auxValues As List(Of Integer) = auxValue
 CType(CType(sender.parent.FindControl("frmdatos$editctrl000014"), DropDownList).DataSourceObject, SqlDataSource).FilterExpression="liccod IN (" & auxClass.Conn.gFieldDB_GetString(auxValues) & ")"
 End If 
auxValue = auxBagValues.gValue_Get("lickyacod.FILTERVALUES")
If auxValue IsNot Nothing Then
 Dim auxValues As List(Of Integer) = auxValue
 CType(CType(sender.parent.FindControl("frmdatos$editctrl000013"), DropDownList).DataSourceObject, SqlDataSource).FilterExpression="liccod IN (" & auxClass.Conn.gFieldDB_GetString(auxValues) & ")"
 End If 
End If
auxClass.Conn.gConn_Close()
End Select

Select Case Request.QueryString("_mode_")
 Case "2"
Dim auxClass As New clscusimDOC
auxClass.Conn.gConn_Open()
Dim auxBagValues As clshrcBagValues = auxClass.gSystem_PostAction_v2(100028,Val(Request.QueryString("_mode_")),Databinder.Eval(frmdatos.DataItem ,"liccod"), Nothing)
If auxBagValues IsNot Nothing Then
     Dim auxValue As Object
auxValue = auxBagValues.gValue_Get("lickyaautcod.FILTERVALUES")
If auxValue IsNot Nothing Then
 Dim auxValues As List(Of Integer) = auxValue
 CType(CType(sender.parent.FindControl("frmdatos$editctrl000014"), DropDownList).DataSourceObject, SqlDataSource).FilterExpression="liccod IN (" & auxClass.Conn.gFieldDB_GetString(auxValues) & ")"
 End If 
auxValue = auxBagValues.gValue_Get("lickyacod.FILTERVALUES")
If auxValue IsNot Nothing Then
 Dim auxValues As List(Of Integer) = auxValue
 CType(CType(sender.parent.FindControl("frmdatos$editctrl000013"), DropDownList).DataSourceObject, SqlDataSource).FilterExpression="liccod IN (" & auxClass.Conn.gFieldDB_GetString(auxValues) & ")"
 End If 
End If
auxClass.Conn.gConn_Close()
End Select

Select Case Request.QueryString("_mode_")
 Case "2"
Dim auxClass As New clscusimDOC
auxClass.Conn.gConn_Open()
Dim auxBagValues As clshrcBagValues = auxClass.gSystem_PostAction_v2(100028,Val(Request.QueryString("_mode_")),Databinder.Eval(frmdatos.DataItem ,"liccod"), Nothing)
If auxBagValues IsNot Nothing Then
     Dim auxValue As Object
auxValue = auxBagValues.gValue_Get("lickyaautcod.FILTERVALUES")
If auxValue IsNot Nothing Then
 Dim auxValues As List(Of Integer) = auxValue
 CType(CType(sender.parent.FindControl("frmdatos$editctrl000014"), DropDownList).DataSourceObject, SqlDataSource).FilterExpression="liccod IN (" & auxClass.Conn.gFieldDB_GetString(auxValues) & ")"
 End If 
auxValue = auxBagValues.gValue_Get("lickyacod.FILTERVALUES")
If auxValue IsNot Nothing Then
 Dim auxValues As List(Of Integer) = auxValue
 CType(CType(sender.parent.FindControl("frmdatos$editctrl000013"), DropDownList).DataSourceObject, SqlDataSource).FilterExpression="liccod IN (" & auxClass.Conn.gFieldDB_GetString(auxValues) & ")"
 End If 
End If
auxClass.Conn.gConn_Close()
End Select

Select Case Request.QueryString("_mode_")
 Case "2"
Dim auxClass As New clscusimDOC
auxClass.Conn.gConn_Open()
Dim auxBagValues As clshrcBagValues = auxClass.gSystem_PostAction_v2(100028,Val(Request.QueryString("_mode_")),Databinder.Eval(frmdatos.DataItem ,"liccod"), Nothing)
If auxBagValues IsNot Nothing Then
     Dim auxValue As Object
auxValue = auxBagValues.gValue_Get("lickyaautcod.FILTERVALUES")
If auxValue IsNot Nothing Then
 Dim auxValues As List(Of Integer) = auxValue
 CType(CType(sender.parent.FindControl("frmdatos$editctrl000014"), DropDownList).DataSourceObject, SqlDataSource).FilterExpression="liccod IN (" & auxClass.Conn.gFieldDB_GetString(auxValues) & ")"
 End If 
auxValue = auxBagValues.gValue_Get("lickyacod.FILTERVALUES")
If auxValue IsNot Nothing Then
 Dim auxValues As List(Of Integer) = auxValue
 CType(CType(sender.parent.FindControl("frmdatos$editctrl000013"), DropDownList).DataSourceObject, SqlDataSource).FilterExpression="liccod IN (" & auxClass.Conn.gFieldDB_GetString(auxValues) & ")"
 End If 
End If
auxClass.Conn.gConn_Close()
End Select

Select Case Request.QueryString("_mode_")
 Case "2"
Dim auxClass As New clscusimDOC
auxClass.Conn.gConn_Open()
Dim auxBagValues As clshrcBagValues = auxClass.gSystem_PostAction_v2(100028,Val(Request.QueryString("_mode_")),Databinder.Eval(frmdatos.DataItem ,"liccod"), Nothing)
If auxBagValues IsNot Nothing Then
     Dim auxValue As Object
auxValue = auxBagValues.gValue_Get("lickyaautcod.FILTERVALUES")
If auxValue IsNot Nothing Then
 Dim auxValues As List(Of Integer) = auxValue
 CType(CType(sender.parent.FindControl("frmdatos$editctrl000014"), DropDownList).DataSourceObject, SqlDataSource).FilterExpression="liccod IN (" & auxClass.Conn.gFieldDB_GetString(auxValues) & ")"
 End If 
auxValue = auxBagValues.gValue_Get("lickyacod.FILTERVALUES")
If auxValue IsNot Nothing Then
 Dim auxValues As List(Of Integer) = auxValue
 CType(CType(sender.parent.FindControl("frmdatos$editctrl000013"), DropDownList).DataSourceObject, SqlDataSource).FilterExpression="liccod IN (" & auxClass.Conn.gFieldDB_GetString(auxValues) & ")"
 End If 
End If
auxClass.Conn.gConn_Close()
End Select

Select Case Request.QueryString("_mode_")
 Case "2"
Dim auxClass As New clscusimDOC
auxClass.Conn.gConn_Open()
Dim auxBagValues As clshrcBagValues = auxClass.gSystem_PostAction_v2(100028,Val(Request.QueryString("_mode_")),Databinder.Eval(frmdatos.DataItem ,"liccod"), Nothing)
If auxBagValues IsNot Nothing Then
     Dim auxValue As Object
auxValue = auxBagValues.gValue_Get("lickyaautcod.FILTERVALUES")
If auxValue IsNot Nothing Then
 Dim auxValues As List(Of Integer) = auxValue
 CType(CType(sender.parent.FindControl("frmdatos$editctrl000014"), DropDownList).DataSourceObject, SqlDataSource).FilterExpression="liccod IN (" & auxClass.Conn.gFieldDB_GetString(auxValues) & ")"
 End If 
auxValue = auxBagValues.gValue_Get("lickyacod.FILTERVALUES")
If auxValue IsNot Nothing Then
 Dim auxValues As List(Of Integer) = auxValue
 CType(CType(sender.parent.FindControl("frmdatos$editctrl000013"), DropDownList).DataSourceObject, SqlDataSource).FilterExpression="liccod IN (" & auxClass.Conn.gFieldDB_GetString(auxValues) & ")"
 End If 
End If
auxClass.Conn.gConn_Close()
End Select

Select Case Request.QueryString("_mode_")
 Case "2"
Dim auxClass As New clscusimDOC
auxClass.Conn.gConn_Open()
Dim auxBagValues As clshrcBagValues = auxClass.gSystem_PostAction_v2(100028,Val(Request.QueryString("_mode_")),Databinder.Eval(frmdatos.DataItem ,"liccod"), Nothing)
If auxBagValues IsNot Nothing Then
     Dim auxValue As Object
auxValue = auxBagValues.gValue_Get("lickyaautcod.FILTERVALUES")
If auxValue IsNot Nothing Then
 Dim auxValues As List(Of Integer) = auxValue
 CType(CType(sender.parent.FindControl("frmdatos$editctrl000014"), DropDownList).DataSourceObject, SqlDataSource).FilterExpression="liccod IN (" & auxClass.Conn.gFieldDB_GetString(auxValues) & ")"
 End If 
auxValue = auxBagValues.gValue_Get("lickyacod.FILTERVALUES")
If auxValue IsNot Nothing Then
 Dim auxValues As List(Of Integer) = auxValue
 CType(CType(sender.parent.FindControl("frmdatos$editctrl000013"), DropDownList).DataSourceObject, SqlDataSource).FilterExpression="liccod IN (" & auxClass.Conn.gFieldDB_GetString(auxValues) & ")"
 End If 
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
         
    Dim auxResult as String=auxClass.gSystemConfig_PostAction("Q_LIC",12,auxDataKeyCod) 
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
  Dim auxResult as String=auxClass.gSystem_PostAction(100028,12,e.Command.Parameters(0).Value) 
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

Protected Sub edtlicdetdsc_DataBound_frmupdpanelfrmupdpanelctrl000004(ByVal sender As Object, ByVal e As System.EventArgs)
CType(sender,TextBox).Text=Eval("licdetdsc").ToString

End Sub

Protected Sub edtlicdetval_DataBound_frmupdpanelfrmupdpanelctrl000004(ByVal sender As Object, ByVal e As System.EventArgs)
CType(sender,TextBox).Text=Eval("licdetval").ToString

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
    
    Dim auxResult as String=auxClass.gSystemConfig_PostAction("Q_LICDET",11,auxDataKeyCod) 
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
         
    Dim auxResult as String=auxClass.gSystemConfig_PostAction("Q_LICDET",12,auxDataKeyCod) 
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

Private Sub gForm_Close()
     If Request.QueryString("_closea_") = "1" Then
      ScriptManager.RegisterStartupScript(Me, Me.GetType(), "CloseWindow", "self.close();", True)
     Else
         Response.Redirect(coWebRootFolder & "hrclicensing.aspx?_mode_=7&_closea_=0")
     End If

End Sub
Protected Sub hrclicensing_det_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
Dim auxSecurity as clsHrcSecurityClient= Session("security"),clsHrcSecurityClient
Dim auxConnn as clsHrcConnClient= Session("conn"),clsHrcConnClient
Dim auxPermRead as Boolean=False
If LicParam Is Nothing Or Session("isadmin") Or auxSecurity.gMember_IsInGroupByID(clshrcimDOC.coGroupIDAdmins) Then
    auxPermRead=True
    m_PermFormEdit=auxPermRead
    m_PermFormNew=auxPermRead
    m_PermFormDelete=auxPermRead
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

