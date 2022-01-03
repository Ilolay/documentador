Imports System.Data
Imports System.Data.SqlClient
Imports Intelimedia.imComponentes
Public Class frmProcesos_det
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
If IsDBNull(Eval("apacod")) Then 
     Ctype(sender,Control).Visible=False
Else
  If Eval("apacod") = -1 Then 
     Ctype(sender,Control).Visible=False
  End If
End If

End Sub

Protected Sub itemctrl000005view_DataBinding(ByVal sender As Object, ByVal e As System.EventArgs)
Dim auxValue as Int32=-1
If IsDBNull(Eval("ownertypevalue")) Then 
     Ctype(sender,Control).Visible=False
Else
objectexplorer_SetValue(Eval("ownertypevalue"),Eval("ownertype"),Eval("ownertypedsc").ToString,CType(sender.parent.FindControl("itemctrl000005view"),LinkButton),CType(sender.parent.FindControl("itemctrl000005value"),HiddenField),CType(sender.parent.FindControl("itemctrl000005"),HiddenField),Nothing)
End If

End Sub
Protected Sub itemctrl000005value_Databound(ByVal sender As Object, ByVal e As System.EventArgs)
If IsDBNull(Eval("ownertypevalue")) Then
 CType(sender,HiddenField).Value=""
Else
 CType(sender,HiddenField).Value=Eval("ownertypevalue").ToString()
End If

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

Protected Sub cmdFormViewItemUpdate_Click (ByVal sender As Object, ByVal e As System.EventArgs)
Server.Transfer(Request.Url.AbsolutePath & "?" & Replace(Request.QueryString.ToString, "_mode_=0", "_mode_=2"))
End Sub
Protected Sub cmdFormViewItemUpdate_DataBinding(ByVal sender As Object, ByVal e As System.EventArgs)
sender.visible=Session("isadmin") Or CType(Session("security"),clsHrcSecurityClient).gSID_CheckAccess(3)

End Sub

Protected Sub editctrl000007_DataBound_frmdatos(ByVal sender As Object, ByVal e As System.EventArgs)
CType(sender,TextBox).Text=Eval("dsc").ToString

End Sub

Protected Sub dseditctrl000006_Init(ByVal sender As Object,ByVal e As System.EventArgs)
sender.ConnectionString=Session("connectionstringname")
End Sub
Protected Sub dseditctrl000006_Deleted(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.SqlDataSourceStatusEventArgs)
If e.Exception IsNot Nothing Then
  lblerror.Text &="//Error borrando datos (" & e.Exception.Message & ")"
    
Else
    
End If

End Sub
Protected Sub dseditctrl000006_Inserted(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.SqlDataSourceStatusEventArgs)
If e.Exception IsNot Nothing Then
  lblerror.Text &="//Error insertando datos (" & e.Exception.Message & ")"
    
Else
    
End If

End Sub
Protected Sub dseditctrl000006_Selected(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.SqlDataSourceStatusEventArgs)
If e.Exception IsNot Nothing Then
  lblerror.Text &="//Error buscando datos (" & e.Exception.Message & ")"
    
Else
    
End If

End Sub
Protected Sub dseditctrl000006_Updated(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.SqlDataSourceStatusEventArgs)
If e.Exception IsNot Nothing Then
  lblerror.Text &="//Error actualizando datos (" & e.Exception.Message & ")"
    
Else
    
End If

End Sub

Protected Sub editctrl000006_Databound(ByVal sender As Object, ByVal e As System.EventArgs)
    If IsDBNull(DataBinder.Eval(frmdatos.DataItem,"apacod")) = False  Then
        If CType(sender,DropDownList).Items.FindByValue(DataBinder.Eval(frmdatos.DataItem,"apacod")) IsNot Nothing Then
            CType(sender,DropDownList).SelectedValue=DataBinder.Eval(frmdatos.DataItem,"apacod")
        End If
    End If

End Sub
Protected Sub txteditctrl000006fs_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs)
Try
 If CType(sender, TextBox).Text <> "" Then
     Dim auxConn As New SqlConnection(Session("connectionstringname"))
     auxConn.Open()
     Dim auxDA As New SqlDataAdapter("SELECT DOC_APA.cod FROM DOC_APA   WHERE (abrev = '@param1') AND (DOC_APA.baja = '1900-01-01T00:00:00' OR DOC_APA.baja  IS NULL)", auxConn)
     auxDA.SelectCommand.Parameters.Add("@param1", SqlDbType.Text).Value = CType(sender, TextBox).Text
     Dim auxDT As New DataTable
     auxDA.Fill(auxDT)
     If auxDT.Rows.Count = 0 Then
         CType(sender, TextBox).Text = ""
         CType(sender, TextBox).Focus()
     Else
         If editctrl000006.Items.FindByValue(auxDT.Rows(0)(0)) Is Nothing Then
         editctrl000006.DataBind()
     End If
     If editctrl000006.Items.FindByValue(auxDT.Rows(0)(0)) Is Nothing Then
     Else
         editctrl000006.SelectedValue = auxDT.Rows(0)(0)
         editctrl000006.Focus
     End If
 End If
End If
Catch ex as Exception
 CType(sender, TextBox).Text = ""
 CType(sender, TextBox).Focus
End Try

End Sub
Protected Sub editctrl000006_comborefresh_Click (ByVal sender As Object, ByVal e As System.EventArgs)
Dim auxeditctrl000006dropdownlist As DropDownList = CType(sender.parent.FindControl("editctrl000006"), DropDownList)
If auxeditctrl000006dropdownlist IsNot Nothing Then
    Dim auxeditctrl000006val As String = auxeditctrl000006dropdownlist.SelectedValue
             CType(auxeditctrl000006dropdownlist,DropDownList).DataBind
             If CType(auxeditctrl000006dropdownlist,DropDownList).Items.FindByValue(auxeditctrl000006val) IsNot Nothing Then
                 CType(auxeditctrl000006dropdownlist,DropDownList).ClearSelection()
                 CType(auxeditctrl000006dropdownlist,DropDownList).Items.FindByValue(auxeditctrl000006val).Selected = True
             End If
End If

End Sub
Protected Sub editctrl000006view_DataBinding(ByVal sender As Object, ByVal e As System.EventArgs)
If IsDBNull(Eval("apacod")) Then 
     Ctype(sender,Control).Visible=False
Else
  If Eval("apacod") = -1 Then 
     Ctype(sender,Control).Visible=False
  End If
End If

End Sub

Protected Sub dseditctrl000005_Init(ByVal sender As Object,ByVal e As System.EventArgs)
sender.ConnectionString=Session("connectionstringname")
End Sub
Protected Sub dseditctrl000005_Deleted(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.SqlDataSourceStatusEventArgs)
If e.Exception IsNot Nothing Then
  lblerror.Text &="//Error borrando datos (" & e.Exception.Message & ")"
    
Else
    
End If

End Sub
Protected Sub dseditctrl000005_Inserted(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.SqlDataSourceStatusEventArgs)
If e.Exception IsNot Nothing Then
  lblerror.Text &="//Error insertando datos (" & e.Exception.Message & ")"
    
Else
    
End If

End Sub
Protected Sub dseditctrl000005_Selected(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.SqlDataSourceStatusEventArgs)
If e.Exception IsNot Nothing Then
  lblerror.Text &="//Error buscando datos (" & e.Exception.Message & ")"
    
Else
    
End If

End Sub
Protected Sub dseditctrl000005_Updated(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.SqlDataSourceStatusEventArgs)
If e.Exception IsNot Nothing Then
  lblerror.Text &="//Error actualizando datos (" & e.Exception.Message & ")"
    
Else
    
End If

End Sub

Protected Sub editctrl000005view_DataBinding(ByVal sender As Object, ByVal e As System.EventArgs)
Dim auxValue as Int32=-1
If IsDBNull(Eval("ownertypevalue")) Then 
     Ctype(sender,Control).Visible=False
Else
objectexplorer_SetValue(Eval("ownertypevalue"),Eval("ownertype"),Eval("ownertypedsc").ToString,CType(sender.parent.FindControl("editctrl000005view"),LinkButton),CType(sender.parent.FindControl("editctrl000005value"),HiddenField),CType(sender.parent.FindControl("editctrl000005"),HiddenField),editctrl000005delete)
End If

End Sub
Protected Sub editctrl000005delete_Click (ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs)
editctrl000005.Value=-1
editctrl000005value.Value=-1
editctrl000005view.Visible=False
editctrl000005delete.Visible=False

End Sub
Protected Sub editctrl000005delete_DataBinding(ByVal sender As Object, ByVal e As System.EventArgs)
If Not IsPostBack  Then 
 If IsDBNull(Eval("ownertypevalue")) Then 
    CType(sender,Control).Visible=False
  ElseIf Eval("ownertypevalue") < 1 Then
    CType(sender,Control).Visible=False
  Else
    CType(sender,Control).Visible=True
 End If
End If

End Sub
Protected Sub cmdeditctrl000005showpanel_Click (ByVal sender As Object, ByVal e As System.EventArgs)
objectexplorer_Show(CType(sender.parent.FindControl("editctrl000005value"),HiddenField).UniqueID,CType(sender.parent.FindControl("editctrl000005view"),LinkButton).UniqueID,CType(sender.parent.FindControl("editctrl000005"),HiddenField).UniqueID,"10{#CHR34#}","9{#CHR34#}10{#CHR34#}",CType(sender.parent.FindControl("editctrl000005delete"),ImageButton).UniqueID)
End Sub
Protected Sub editctrl000005value_Databound(ByVal sender As Object, ByVal e As System.EventArgs)
If IsDBNull(Eval("ownertypevalue")) Then
 CType(sender,HiddenField).Value=""
Else
 CType(sender,HiddenField).Value=Eval("ownertypevalue").ToString()
End If

End Sub

Protected Sub editctrl000004_Databound(ByVal sender As Object, ByVal e As System.EventArgs)
CType(sender,Label).Text=Eval("orden").ToString

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

Protected Sub dsinsctrl000006_Init(ByVal sender As Object,ByVal e As System.EventArgs)
sender.ConnectionString=Session("connectionstringname")
End Sub
Protected Sub dsinsctrl000006_Deleted(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.SqlDataSourceStatusEventArgs)
If e.Exception IsNot Nothing Then
  lblerror.Text &="//Error borrando datos (" & e.Exception.Message & ")"
    
Else
    
End If

End Sub
Protected Sub dsinsctrl000006_Inserted(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.SqlDataSourceStatusEventArgs)
If e.Exception IsNot Nothing Then
  lblerror.Text &="//Error insertando datos (" & e.Exception.Message & ")"
    
Else
    
End If

End Sub
Protected Sub dsinsctrl000006_Selected(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.SqlDataSourceStatusEventArgs)
If e.Exception IsNot Nothing Then
  lblerror.Text &="//Error buscando datos (" & e.Exception.Message & ")"
    
Else
    
End If

End Sub
Protected Sub dsinsctrl000006_Updated(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.SqlDataSourceStatusEventArgs)
If e.Exception IsNot Nothing Then
  lblerror.Text &="//Error actualizando datos (" & e.Exception.Message & ")"
    
Else
    
End If

End Sub

Protected Sub insctrl000006_Load(ByVal sender As Object, ByVal e As System.EventArgs)
If Not Page.IsPostBack Then 
    CType(sender,DropDownList).DataBind
    If CType(sender,DropDownList).Items.FindByValue("-1") IsNot Nothing Then
        CType(sender,DropDownList).Items.FindByValue("-1").Selected = True
    ElseIf CType(sender,DropDownList).Items.Count > 0 Then
        CType(sender,DropDownList).Items(0).Selected = True
    End If
End If

End Sub
Protected Sub txtinsctrl000006fs_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs)
Try
 If CType(sender, TextBox).Text <> "" Then
     Dim auxConn As New SqlConnection(Session("connectionstringname"))
     auxConn.Open()
     Dim auxDA As New SqlDataAdapter("SELECT DOC_APA.cod FROM DOC_APA   WHERE (abrev = '@param1') AND (DOC_APA.baja = '1900-01-01T00:00:00' OR DOC_APA.baja  IS NULL)", auxConn)
     auxDA.SelectCommand.Parameters.Add("@param1", SqlDbType.Text).Value = CType(sender, TextBox).Text
     Dim auxDT As New DataTable
     auxDA.Fill(auxDT)
     If auxDT.Rows.Count = 0 Then
         CType(sender, TextBox).Text = ""
         CType(sender, TextBox).Focus()
     Else
         If insctrl000006.Items.FindByValue(auxDT.Rows(0)(0)) Is Nothing Then
         insctrl000006.DataBind()
     End If
     If insctrl000006.Items.FindByValue(auxDT.Rows(0)(0)) Is Nothing Then
     Else
         insctrl000006.SelectedValue = auxDT.Rows(0)(0)
         insctrl000006.Focus
     End If
 End If
End If
Catch ex as Exception
 CType(sender, TextBox).Text = ""
 CType(sender, TextBox).Focus
End Try

End Sub
Protected Sub insctrl000006_comborefresh_Click (ByVal sender As Object, ByVal e As System.EventArgs)
Dim auxinsctrl000006dropdownlist As DropDownList = CType(sender.parent.FindControl("insctrl000006"), DropDownList)
If auxinsctrl000006dropdownlist IsNot Nothing Then
    Dim auxinsctrl000006val As String = auxinsctrl000006dropdownlist.SelectedValue
             CType(auxinsctrl000006dropdownlist,DropDownList).DataBind
             If CType(auxinsctrl000006dropdownlist,DropDownList).Items.FindByValue(auxinsctrl000006val) IsNot Nothing Then
                 CType(auxinsctrl000006dropdownlist,DropDownList).ClearSelection()
                 CType(auxinsctrl000006dropdownlist,DropDownList).Items.FindByValue(auxinsctrl000006val).Selected = True
             End If
End If

End Sub

Protected Sub dsinsctrl000005_Init(ByVal sender As Object,ByVal e As System.EventArgs)
sender.ConnectionString=Session("connectionstringname")
End Sub
Protected Sub dsinsctrl000005_Deleted(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.SqlDataSourceStatusEventArgs)
If e.Exception IsNot Nothing Then
  lblerror.Text &="//Error borrando datos (" & e.Exception.Message & ")"
    
Else
    
End If

End Sub
Protected Sub dsinsctrl000005_Inserted(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.SqlDataSourceStatusEventArgs)
If e.Exception IsNot Nothing Then
  lblerror.Text &="//Error insertando datos (" & e.Exception.Message & ")"
    
Else
    
End If

End Sub
Protected Sub dsinsctrl000005_Selected(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.SqlDataSourceStatusEventArgs)
If e.Exception IsNot Nothing Then
  lblerror.Text &="//Error buscando datos (" & e.Exception.Message & ")"
    
Else
    
End If

End Sub
Protected Sub dsinsctrl000005_Updated(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.SqlDataSourceStatusEventArgs)
If e.Exception IsNot Nothing Then
  lblerror.Text &="//Error actualizando datos (" & e.Exception.Message & ")"
    
Else
    
End If

End Sub

Protected Sub insctrl000005delete_Click (ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs)
insctrl000005.Value=-1
insctrl000005value.Value=-1
insctrl000005view.Visible=False
insctrl000005delete.Visible=False

End Sub
Protected Sub insctrl000005delete_DataBinding(ByVal sender As Object, ByVal e As System.EventArgs)
If Not IsPostBack  Then 
 If IsDBNull(Eval("ownertypevalue")) Then 
    CType(sender,Control).Visible=False
  ElseIf Eval("ownertypevalue") < 1 Then
    CType(sender,Control).Visible=False
  Else
    CType(sender,Control).Visible=True
 End If
End If

End Sub
Protected Sub cmdinsctrl000005showpanel_Click (ByVal sender As Object, ByVal e As System.EventArgs)
objectexplorer_Show(CType(sender.parent.FindControl("insctrl000005value"),HiddenField).UniqueID,CType(sender.parent.FindControl("insctrl000005view"),LinkButton).UniqueID,CType(sender.parent.FindControl("insctrl000005"),HiddenField).UniqueID,"10{#CHR34#}","9{#CHR34#}10{#CHR34#}",CType(sender.parent.FindControl("insctrl000005delete"),ImageButton).UniqueID)
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
         Response.Redirect(Session("ROOTWEBURL") & "frmProcesos.aspx?_mode_=7&_closea_=0")
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
    Dim auxResult as String=auxClass.gSystem_PostAction(31,13,auxDataKeyCod) 
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
    auxClass.Conn.gConn_Delete("DELETE FROM DOC_PROEMP WHERE procod = " & auxDataKeyCod & "")
auxClass.Conn.gConn_Delete("DELETE FROM DOC_PROUND WHERE procod = " & auxDataKeyCod & "")
Select Case Val(CType(sender.FindControl("insctrl000005"),HiddenField).Value)
Case 9
auxClass.gEntity_DOC_PROEMP_Insert(pprocod:=auxDataKeyCod,pempcod:=CType(sender.FindControl("insctrl000005value"),HiddenField).Value)
Case 10
auxClass.gEntity_DOC_PROUND_Insert(pprocod:=auxDataKeyCod,pundcod:=CType(sender.FindControl("insctrl000005value"),HiddenField).Value)
End Select

    Dim auxResult as String=auxClass.gSystem_PostAction(31,11,auxDataKeyCod) 
    lblerror.Text &=auxResult
    If auxResult ="" Then
    End If
    auxClass.Conn.gConn_Close
End If

End Sub
Protected Sub frmdatos_ItemSelected(ByVal sender As Object, ByVal e As System.EventArgs)
If frmdatos.DataItem IsNot Nothing Then
   Page.Title = "Procesos| " &  Databinder.Eval(frmdatos.DataItem ,"dsc").ToString
   lblsubtitle.Text = Databinder.Eval(frmdatos.DataItem ,"dsc").ToString
Else
   Page.Title = "Procesos| Nuevo"
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
     Dim auxMax As Integer = Val(LicParam.gValue_Get("DOC_PRO.MAX", -1))
     If auxMax >= 0 Then
         Dim auxConn As clsHrcConnClient = Session("conn")
         auxConn.gComponent_CreateInstance()
         auxConn.gConn_Open()
         If Val(auxConn.gConn_QueryValueInt("SELECT COUNT(*) FROM DOC_PRO", 0)) > auxMax Then
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
        Dim auxDA As New SqlDataAdapter("SELECT DOC_PRO.cod,DOC_PRO.dsc,(DOC_PROAPACOD.dsc) as apacoddsc,DOC_PRO.ownertype,CASE DOC_PRO.ownertype WHEN 9 THEN (SELECT TOP 1 DOC_PROEMP.empcod FROM DOC_PROEMP WHERE DOC_PROEMP.procod=DOC_PRO.cod) WHEN 10 THEN (SELECT TOP 1 DOC_PROUND.undcod FROM DOC_PROUND WHERE DOC_PROUND.procod=DOC_PRO.cod) END as ownertypevalue,CASE DOC_PRO.ownertype WHEN 9 THEN (SELECT TOP 1 EMP.dsc FROM EMP WHERE EMP.cod= (SELECT TOP 1 DOC_PROEMP.empcod FROM DOC_PROEMP WHERE DOC_PROEMP.procod=DOC_PRO.cod)) WHEN 10 THEN (SELECT TOP 1 UND.dsc FROM UND WHERE UND.cod= (SELECT TOP 1 DOC_PROUND.undcod FROM DOC_PROUND WHERE DOC_PROUND.procod=DOC_PRO.cod)) END as ownertypedsc,DOC_PRO.baja,DOC_PRO.orden,(DOC_PROQSECSID.secdsc) as qsecsiddsc,DOC_PRO.qsecdatetime,DOC_PRO.apacod,DOC_PRO.qsecsid FROM DOC_PRO  LEFT JOIN DOC_APA AS DOC_PROAPACOD ON DOC_PROAPACOD.cod=DOC_PRO.apacod LEFT JOIN Q_SECPLOGIN AS DOC_PROQSECSID ON DOC_PROQSECSID.sidcod=DOC_PRO.qsecsid  WHERE (DOC_PRO.cod=@param1) AND (DOC_PRO.baja = 0 OR DOC_PRO.baja  IS NULL)", auxConn)
        auxDA.SelectCommand.Parameters.Add("@param1", SqlDbType.Int, 4).Value = Request.QueryString("param1")
        Dim auxDT As New DataTable
        auxDA.Fill(auxDT)
        auxConn.Close
        If auxDT.Rows.Count <> 0 Then
             If IsDBNull(auxDT.Rows(0)("dsc")) = False Then
             CType(sender.parent.FindControl("frmdatos$insctrl000007"),TextBox).Text=auxDT.Rows(0)("dsc")
             End If 
             If IsDBNull(auxDT.Rows(0)("apacod")) = False Then
             CType(sender.parent.FindControl("frmdatos$insctrl000006"),DropDownList).DataBind
             If CType(sender.parent.FindControl("frmdatos$insctrl000006"),DropDownList).Items.FindByValue(auxDT.Rows(0)("apacod")) IsNot Nothing Then
                 CType(sender.parent.FindControl("frmdatos$insctrl000006"),DropDownList).ClearSelection()
                 CType(sender.parent.FindControl("frmdatos$insctrl000006"),DropDownList).Items.FindByValue(auxDT.Rows(0)("apacod")).Selected = True
             End If
             End If 
             If IsDBNull(auxDT.Rows(0)("ownertype")) = False Then
     If IsDBNull("" & auxDT.Rows(0)("ownertype") & "")=False Then
         If CInt("" & auxDT.Rows(0)("ownertype") & "") > 0 Then
          CType(sender.parent.FindControl("frmdatos$insctrl000005"),HiddenField).Value="" & auxDT.Rows(0)("ownertype") & ""
          CType(sender.parent.FindControl("frmdatos$insctrl000005view"),LinkButton).Text=auxDT.Rows(0)("ownertypedsc")
          CType(sender.parent.FindControl("frmdatos$insctrl000005delete"),ImageButton).Visible=True
         End If
      End If
             End If 
        End If
    Catch ex as sqlException
    End Try
End Select
Select Case Request.QueryString("_mode_")
 Case "1","25"
Dim auxClass As New clscusimDOC
auxClass.Conn.gConn_Open()
Dim auxBagValues As clshrcBagValues = auxClass.gSystem_PostAction_v2(31,Val(Request.QueryString("_mode_")), -1, Nothing)
If auxBagValues IsNot Nothing Then
     Dim auxValue As Object
auxValue = auxBagValues.gValue_Get("apacod.FILTERVALUES")
If auxValue IsNot Nothing Then
 If auxBagValues.gValue_Get("HRC_CANCEL", False) Then
     gForm_Close()
 End If
 Dim auxValues As List(Of Integer) = auxValue
 CType(CType(sender.parent.FindControl("frmdatos$insctrl000006"), DropDownList).DataSourceObject, SqlDataSource).FilterExpression="cod IN (" & auxClass.Conn.gFieldDB_GetString(auxValues) & ")"
 End If 
End If
auxClass.Conn.gConn_Close()
End Select

Select Case Request.QueryString("_mode_")
 Case "2"
Dim auxClass As New clscusimDOC
auxClass.Conn.gConn_Open()
Dim auxBagValues As clshrcBagValues = auxClass.gSystem_PostAction_v2(31,Val(Request.QueryString("_mode_")),Databinder.Eval(frmdatos.DataItem ,"cod"), Nothing)
If auxBagValues IsNot Nothing Then
     Dim auxValue As Object
auxValue = auxBagValues.gValue_Get("apacod.FILTERVALUES")
If auxValue IsNot Nothing Then
 Dim auxValues As List(Of Integer) = auxValue
 CType(CType(sender.parent.FindControl("frmdatos$editctrl000006"), DropDownList).DataSourceObject, SqlDataSource).FilterExpression="cod IN (" & auxClass.Conn.gFieldDB_GetString(auxValues) & ")"
 End If 
End If
auxClass.Conn.gConn_Close()
End Select

Select Case Request.QueryString("_mode_")
 Case "2"
Dim auxClass As New clscusimDOC
auxClass.Conn.gConn_Open()
Dim auxBagValues As clshrcBagValues = auxClass.gSystem_PostAction_v2(31,Val(Request.QueryString("_mode_")),Databinder.Eval(frmdatos.DataItem ,"cod"), Nothing)
If auxBagValues IsNot Nothing Then
     Dim auxValue As Object
auxValue = auxBagValues.gValue_Get("apacod.FILTERVALUES")
If auxValue IsNot Nothing Then
 Dim auxValues As List(Of Integer) = auxValue
 CType(CType(sender.parent.FindControl("frmdatos$editctrl000006"), DropDownList).DataSourceObject, SqlDataSource).FilterExpression="cod IN (" & auxClass.Conn.gFieldDB_GetString(auxValues) & ")"
 End If 
End If
auxClass.Conn.gConn_Close()
End Select

Select Case Request.QueryString("_mode_")
 Case "2"
Dim auxClass As New clscusimDOC
auxClass.Conn.gConn_Open()
Dim auxBagValues As clshrcBagValues = auxClass.gSystem_PostAction_v2(31,Val(Request.QueryString("_mode_")),Databinder.Eval(frmdatos.DataItem ,"cod"), Nothing)
If auxBagValues IsNot Nothing Then
     Dim auxValue As Object
auxValue = auxBagValues.gValue_Get("apacod.FILTERVALUES")
If auxValue IsNot Nothing Then
 Dim auxValues As List(Of Integer) = auxValue
 CType(CType(sender.parent.FindControl("frmdatos$editctrl000006"), DropDownList).DataSourceObject, SqlDataSource).FilterExpression="cod IN (" & auxClass.Conn.gFieldDB_GetString(auxValues) & ")"
 End If 
End If
auxClass.Conn.gConn_Close()
End Select

Select Case Request.QueryString("_mode_")
 Case "2"
Dim auxClass As New clscusimDOC
auxClass.Conn.gConn_Open()
Dim auxBagValues As clshrcBagValues = auxClass.gSystem_PostAction_v2(31,Val(Request.QueryString("_mode_")),Databinder.Eval(frmdatos.DataItem ,"cod"), Nothing)
If auxBagValues IsNot Nothing Then
     Dim auxValue As Object
auxValue = auxBagValues.gValue_Get("apacod.FILTERVALUES")
If auxValue IsNot Nothing Then
 Dim auxValues As List(Of Integer) = auxValue
 CType(CType(sender.parent.FindControl("frmdatos$editctrl000006"), DropDownList).DataSourceObject, SqlDataSource).FilterExpression="cod IN (" & auxClass.Conn.gFieldDB_GetString(auxValues) & ")"
 End If 
End If
auxClass.Conn.gConn_Close()
End Select

Select Case Request.QueryString("_mode_")
 Case "2"
Dim auxClass As New clscusimDOC
auxClass.Conn.gConn_Open()
Dim auxBagValues As clshrcBagValues = auxClass.gSystem_PostAction_v2(31,Val(Request.QueryString("_mode_")),Databinder.Eval(frmdatos.DataItem ,"cod"), Nothing)
If auxBagValues IsNot Nothing Then
     Dim auxValue As Object
auxValue = auxBagValues.gValue_Get("apacod.FILTERVALUES")
If auxValue IsNot Nothing Then
 Dim auxValues As List(Of Integer) = auxValue
 CType(CType(sender.parent.FindControl("frmdatos$editctrl000006"), DropDownList).DataSourceObject, SqlDataSource).FilterExpression="cod IN (" & auxClass.Conn.gFieldDB_GetString(auxValues) & ")"
 End If 
End If
auxClass.Conn.gConn_Close()
End Select

Select Case Request.QueryString("_mode_")
 Case "2"
Dim auxClass As New clscusimDOC
auxClass.Conn.gConn_Open()
Dim auxBagValues As clshrcBagValues = auxClass.gSystem_PostAction_v2(31,Val(Request.QueryString("_mode_")),Databinder.Eval(frmdatos.DataItem ,"cod"), Nothing)
If auxBagValues IsNot Nothing Then
     Dim auxValue As Object
auxValue = auxBagValues.gValue_Get("apacod.FILTERVALUES")
If auxValue IsNot Nothing Then
 Dim auxValues As List(Of Integer) = auxValue
 CType(CType(sender.parent.FindControl("frmdatos$editctrl000006"), DropDownList).DataSourceObject, SqlDataSource).FilterExpression="cod IN (" & auxClass.Conn.gFieldDB_GetString(auxValues) & ")"
 End If 
End If
auxClass.Conn.gConn_Close()
End Select

Select Case Request.QueryString("_mode_")
 Case "2"
Dim auxClass As New clscusimDOC
auxClass.Conn.gConn_Open()
Dim auxBagValues As clshrcBagValues = auxClass.gSystem_PostAction_v2(31,Val(Request.QueryString("_mode_")),Databinder.Eval(frmdatos.DataItem ,"cod"), Nothing)
If auxBagValues IsNot Nothing Then
     Dim auxValue As Object
auxValue = auxBagValues.gValue_Get("apacod.FILTERVALUES")
If auxValue IsNot Nothing Then
 Dim auxValues As List(Of Integer) = auxValue
 CType(CType(sender.parent.FindControl("frmdatos$editctrl000006"), DropDownList).DataSourceObject, SqlDataSource).FilterExpression="cod IN (" & auxClass.Conn.gFieldDB_GetString(auxValues) & ")"
 End If 
End If
auxClass.Conn.gConn_Close()
End Select

Select Case Request.QueryString("_mode_")
 Case "2"
Dim auxClass As New clscusimDOC
auxClass.Conn.gConn_Open()
Dim auxBagValues As clshrcBagValues = auxClass.gSystem_PostAction_v2(31,Val(Request.QueryString("_mode_")),Databinder.Eval(frmdatos.DataItem ,"cod"), Nothing)
If auxBagValues IsNot Nothing Then
     Dim auxValue As Object
auxValue = auxBagValues.gValue_Get("apacod.FILTERVALUES")
If auxValue IsNot Nothing Then
 Dim auxValues As List(Of Integer) = auxValue
 CType(CType(sender.parent.FindControl("frmdatos$editctrl000006"), DropDownList).DataSourceObject, SqlDataSource).FilterExpression="cod IN (" & auxClass.Conn.gFieldDB_GetString(auxValues) & ")"
 End If 
End If
auxClass.Conn.gConn_Close()
End Select

Select Case Request.QueryString("_mode_")
 Case "2"
Dim auxClass As New clscusimDOC
auxClass.Conn.gConn_Open()
Dim auxBagValues As clshrcBagValues = auxClass.gSystem_PostAction_v2(31,Val(Request.QueryString("_mode_")),Databinder.Eval(frmdatos.DataItem ,"cod"), Nothing)
If auxBagValues IsNot Nothing Then
     Dim auxValue As Object
auxValue = auxBagValues.gValue_Get("apacod.FILTERVALUES")
If auxValue IsNot Nothing Then
 Dim auxValues As List(Of Integer) = auxValue
 CType(CType(sender.parent.FindControl("frmdatos$editctrl000006"), DropDownList).DataSourceObject, SqlDataSource).FilterExpression="cod IN (" & auxClass.Conn.gFieldDB_GetString(auxValues) & ")"
 End If 
End If
auxClass.Conn.gConn_Close()
End Select

Select Case Request.QueryString("_mode_")
 Case "2"
Dim auxClass As New clscusimDOC
auxClass.Conn.gConn_Open()
Dim auxBagValues As clshrcBagValues = auxClass.gSystem_PostAction_v2(31,Val(Request.QueryString("_mode_")),Databinder.Eval(frmdatos.DataItem ,"cod"), Nothing)
If auxBagValues IsNot Nothing Then
     Dim auxValue As Object
auxValue = auxBagValues.gValue_Get("apacod.FILTERVALUES")
If auxValue IsNot Nothing Then
 Dim auxValues As List(Of Integer) = auxValue
 CType(CType(sender.parent.FindControl("frmdatos$editctrl000006"), DropDownList).DataSourceObject, SqlDataSource).FilterExpression="cod IN (" & auxClass.Conn.gFieldDB_GetString(auxValues) & ")"
 End If 
End If
auxClass.Conn.gConn_Close()
End Select

Select Case Request.QueryString("_mode_")
 Case "2"
Dim auxClass As New clscusimDOC
auxClass.Conn.gConn_Open()
Dim auxBagValues As clshrcBagValues = auxClass.gSystem_PostAction_v2(31,Val(Request.QueryString("_mode_")),Databinder.Eval(frmdatos.DataItem ,"cod"), Nothing)
If auxBagValues IsNot Nothing Then
     Dim auxValue As Object
auxValue = auxBagValues.gValue_Get("apacod.FILTERVALUES")
If auxValue IsNot Nothing Then
 Dim auxValues As List(Of Integer) = auxValue
 CType(CType(sender.parent.FindControl("frmdatos$editctrl000006"), DropDownList).DataSourceObject, SqlDataSource).FilterExpression="cod IN (" & auxClass.Conn.gFieldDB_GetString(auxValues) & ")"
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
         auxClass.Conn.gConn_Delete("DELETE FROM DOC_PROEMP WHERE procod = " & auxDataKeyCod & "")
auxClass.Conn.gConn_Delete("DELETE FROM DOC_PROUND WHERE procod = " & auxDataKeyCod & "")
Select Case Val(CType(sender.FindControl("editctrl000005"),HiddenField).Value)
Case 9
auxClass.gEntity_DOC_PROEMP_Insert(pprocod:=auxDataKeyCod,pempcod:=CType(sender.FindControl("editctrl000005value"),HiddenField).Value)
Case 10
auxClass.gEntity_DOC_PROUND_Insert(pprocod:=auxDataKeyCod,pundcod:=CType(sender.FindControl("editctrl000005value"),HiddenField).Value)
End Select

    Dim auxResult as String=auxClass.gSystem_PostAction(31,12,auxDataKeyCod) 
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
End Select
auxQuery = Replace(auxQuery,"{#PARAM1#}",pCod)
Dim auxConn As clsHrcConnClient = Session("conn")
 auxConn = auxConn.gComponent_CreateInstance
auxConn.gConn_Open
Dim auxDT As DataTable = auxConn.gConn_Query(auxQuery )
auxConn.gConn_Close
For Each auxRow As DataRow In auxDT.Rows
  Dim auxNode As New TreeNode(If(IsDBNull(auxRow(1)),"Todos",auxRow(1)), auxRow(0).ToString)
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
    Case 9
        pLinkButton.OnClientClick = "javascript:if (window.showModalDialog) {window.showModalDialog(" & chr(39) & "frmColaboradores_det.aspx?_mode_=0&_closea_=1&isDlg=1&param1=" & pValue & "&timestamp=" & chr(39) & " + new Date().getTime() + " & chr(39) & "" & chr(39) & ", " & chr(39) & "" & chr(39) & ", " & chr(39) & "menubar=no;resizable=yes;toolbar=no;status=no;center=yes;dialogWidth=750px;dialogHeight=570px;" & chr(39) & ");} else {window.open(" & chr(39) & "frmColaboradores_det.aspx?_mode_=0&_closea_=1&isDlg=1&param1=" & pValue & "&timestamp=" & chr(39) & " + new Date().getTime() + " & chr(39) & "" & chr(39) & ", " & chr(39) & "" & chr(39) & ", " & chr(39) & "modal=yes;directories=no;menubar=no;resizable=yes;toolbar=no;status=no;center=yes;dialogWidth=750px;dialogHeight=570px;" & chr(39) & ");};return false;"
    Case 10
        pLinkButton.OnClientClick = "javascript:if (window.showModalDialog) {window.showModalDialog(" & chr(39) & "frmUnidades_det.aspx?_mode_=0&_closea_=1&isDlg=1&param1=" & pValue & "&timestamp=" & chr(39) & " + new Date().getTime() + " & chr(39) & "" & chr(39) & ", " & chr(39) & "" & chr(39) & ", " & chr(39) & "menubar=no;resizable=yes;toolbar=no;status=no;center=yes;dialogWidth=750px;dialogHeight=570px;" & chr(39) & ");} else {window.open(" & chr(39) & "frmUnidades_det.aspx?_mode_=0&_closea_=1&isDlg=1&param1=" & pValue & "&timestamp=" & chr(39) & " + new Date().getTime() + " & chr(39) & "" & chr(39) & ", " & chr(39) & "" & chr(39) & ", " & chr(39) & "modal=yes;directories=no;menubar=no;resizable=yes;toolbar=no;status=no;center=yes;dialogWidth=750px;dialogHeight=570px;" & chr(39) & ");};return false;"
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
If CType(sender, TreeView).SelectedNode.Depth=0 Then
    auxSelectedValue=-1
End If
If grdobjectexplorer.Visible = False Then
    modalpopuppnlobjectexplorer_Select(auxSelectedValue, 10, CType(sender, TreeView).SelectedNode.Text)
Else
    Dim auxQuery As String = ""
    If Instr(ViewState("modalpopuppnlobjectexplorer_mode"), "{#CHR34#}9{#CHR34#}") > 0 Then
        If auxQuery <> "" Then auxQuery &= " UNION ALL "
        auxQuery &= "(SELECT EMP.cod as cod,EMP.dsc as dsc,9 as objecttype,'Colaborador' as objecttypedsc FROM EMP   WHERE ((((EMP.undcod =" & auxSelectedValue & " AND '" & txtobjectexplorerfilter.Text.Trim & "'='')  OR  (EMP.dsc LIKE '%" & txtobjectexplorerfilter.Text.Trim & "%' AND '" & txtobjectexplorerfilter.Text.Trim & "'<>''))) AND (EMP.baja = 0 OR EMP.baja  IS NULL)) AND EMP.cod >= 1)"
    End If
    If Instr(ViewState("modalpopuppnlobjectexplorer_mode"), "{#CHR34#}10{#CHR34#}") > 0 Then
        If auxQuery <> "" Then auxQuery &= " UNION ALL "
        auxQuery &= "(SELECT UND.cod as cod,UND.dsc as dsc,10 as objecttype,'Unidad' as objecttypedsc FROM UND   WHERE ((((UND.undcodsup =" & auxSelectedValue & " AND '" & txtobjectexplorerfilter.Text.Trim & "'='')  OR  (UND.dsc LIKE '%" & txtobjectexplorerfilter.Text.Trim & "%' AND '" & txtobjectexplorerfilter.Text.Trim & "'<>''))) AND (UND.baja = 0 OR UND.baja  IS NULL)) AND UND.cod >= 1)"
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
         Response.Redirect(Session("ROOTWEBURL") & "frmProcesos.aspx?_mode_=7&_closea_=0")
     End If

End Sub
Protected Sub frmProcesos_det_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
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

