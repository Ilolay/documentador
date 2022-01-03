Imports System.Data
Imports System.Data.SqlClient
Imports Intelimedia.imComponentes
Public Class frmUnidades_det
Inherits System.Web.UI.Page
Friend m_PermFormDelete as Boolean=False
Friend m_PermFormEdit as Boolean=False
Friend m_PermFormNew as Boolean=False
Protected Sub itemctrl000021_Databound(ByVal sender As Object, ByVal e As System.EventArgs)
CType(sender,Label).Text=Eval("cod").ToString

End Sub

Protected Sub itemctrl000019_Databound(ByVal sender As Object, ByVal e As System.EventArgs)
CType(sender,Label).Text=Replace(Eval("dsc").ToString,Chr(10),"<br />")

End Sub

Protected Sub itemctrl000018view_DataBinding(ByVal sender As Object, ByVal e As System.EventArgs)
If IsDBNull(Eval("undtipcod")) Then 
     Ctype(sender,Control).Visible=False
Else
  If Eval("undtipcod") = -1 Then 
     Ctype(sender,Control).Visible=False
  End If
End If

End Sub

Protected Sub itemctrl000017view_DataBinding(ByVal sender As Object, ByVal e As System.EventArgs)
Dim auxValue as Int32=-1
If IsDBNull(Eval("resp")) Then 
     Ctype(sender,Control).Visible=False
Else
  If Eval("resp") < 1 Then 
     Ctype(sender,Control).Visible=False
  End If
End If

End Sub
Protected Sub itemctrl000017_Databound(ByVal sender As Object, ByVal e As System.EventArgs)
If IsDBNull(Eval("resp")) Then
 CType(sender,HiddenField).Value=""
Else
 CType(sender,HiddenField).Value=Eval("resp").ToString()
End If

End Sub

Protected Sub itemctrl000016view_DataBinding(ByVal sender As Object, ByVal e As System.EventArgs)
Dim auxValue as Int32=-1
If IsDBNull(Eval("undcodsup")) Then 
     Ctype(sender,Control).Visible=False
Else
  If Eval("undcodsup") < 1 Then 
     Ctype(sender,Control).Visible=False
  End If
End If

End Sub
Protected Sub itemctrl000016_Databound(ByVal sender As Object, ByVal e As System.EventArgs)
If IsDBNull(Eval("undcodsup")) Then
 CType(sender,HiddenField).Value=""
Else
 CType(sender,HiddenField).Value=Eval("undcodsup").ToString()
End If

End Sub

Protected Sub itemctrl000015_Databound(ByVal sender As Object, ByVal e As System.EventArgs)
CType(sender,Label).Text=Replace(Eval("sectorid").ToString,Chr(10),"<br />")

End Sub

Protected Sub itemctrl000014_Databound(ByVal sender As Object, ByVal e As System.EventArgs)
CType(sender,Label).Text=Replace(Eval("undnro").ToString,Chr(10),"<br />")

End Sub

Protected Sub itemctrl000013_Databound(ByVal sender As Object, ByVal e As System.EventArgs)
CType(sender,Label).Text=Eval("orden").ToString

End Sub

Protected Sub itemctrl000012view_DataBinding(ByVal sender As Object, ByVal e As System.EventArgs)
Dim auxValue as Int32=-1
If IsDBNull(Eval("miembrosgrpcod")) Then 
     Ctype(sender,Control).Visible=False
Else
  If Eval("miembrosgrpcod") < 1 Then 
     Ctype(sender,Control).Visible=False
  End If
End If

End Sub
Protected Sub itemctrl000012_Databound(ByVal sender As Object, ByVal e As System.EventArgs)
If IsDBNull(Eval("miembrosgrpcod")) Then
 CType(sender,HiddenField).Value=""
Else
 CType(sender,HiddenField).Value=Eval("miembrosgrpcod").ToString()
End If

End Sub

Protected Sub itemctrl000011view_DataBinding(ByVal sender As Object, ByVal e As System.EventArgs)
Dim auxValue as Int32=-1
If IsDBNull(Eval("grpcodresp")) Then 
     Ctype(sender,Control).Visible=False
Else
  If Eval("grpcodresp") < 1 Then 
     Ctype(sender,Control).Visible=False
  End If
End If

End Sub
Protected Sub itemctrl000011_Databound(ByVal sender As Object, ByVal e As System.EventArgs)
If IsDBNull(Eval("grpcodresp")) Then
 CType(sender,HiddenField).Value=""
Else
 CType(sender,HiddenField).Value=Eval("grpcodresp").ToString()
End If

End Sub

Protected Sub itemctrl000010view_DataBinding(ByVal sender As Object, ByVal e As System.EventArgs)
Dim auxValue as Int32=-1
If IsDBNull(Eval("grpcodprjver")) Then 
     Ctype(sender,Control).Visible=False
Else
  If Eval("grpcodprjver") < 1 Then 
     Ctype(sender,Control).Visible=False
  End If
End If

End Sub
Protected Sub itemctrl000010_Databound(ByVal sender As Object, ByVal e As System.EventArgs)
If IsDBNull(Eval("grpcodprjver")) Then
 CType(sender,HiddenField).Value=""
Else
 CType(sender,HiddenField).Value=Eval("grpcodprjver").ToString()
End If

End Sub

Protected Sub itemctrl000009view_DataBinding(ByVal sender As Object, ByVal e As System.EventArgs)
Dim auxValue as Int32=-1
If IsDBNull(Eval("grpcodmbrdir")) Then 
     Ctype(sender,Control).Visible=False
Else
  If Eval("grpcodmbrdir") < 1 Then 
     Ctype(sender,Control).Visible=False
  End If
End If

End Sub
Protected Sub itemctrl000009_Databound(ByVal sender As Object, ByVal e As System.EventArgs)
If IsDBNull(Eval("grpcodmbrdir")) Then
 CType(sender,HiddenField).Value=""
Else
 CType(sender,HiddenField).Value=Eval("grpcodmbrdir").ToString()
End If

End Sub

Protected Sub itemctrl000008_Databound(ByVal sender As Object, ByVal e As System.EventArgs)
If IsDBNull(Eval("creatorsmbrdir")) Then
CType(sender,Label).Text &=""
ElseIf Eval("creatorsmbrdir") Then
CType(sender,Label).Text &="<img src=imagenes/actyes.gif alt=Si width=12px border=0px />"
Else
CType(sender,Label).Text &=""
End If

End Sub

Protected Sub itemctrl000007_Databound(ByVal sender As Object, ByVal e As System.EventArgs)
CType(sender,Label).Text=Replace(Eval("formatoespecifico").ToString,Chr(10),"<br />")

End Sub

Protected Sub itemctrl000006view_DataBinding(ByVal sender As Object, ByVal e As System.EventArgs)
Dim auxValue as Int32=-1
If IsDBNull(Eval("editor")) Then 
     Ctype(sender,Control).Visible=False
Else
  If Eval("editor") < 1 Then 
     Ctype(sender,Control).Visible=False
  End If
End If

End Sub
Protected Sub itemctrl000006_Databound(ByVal sender As Object, ByVal e As System.EventArgs)
If IsDBNull(Eval("editor")) Then
 CType(sender,HiddenField).Value=""
Else
 CType(sender,HiddenField).Value=Eval("editor").ToString()
End If

End Sub

Protected Sub itemctrl000005view_DataBinding(ByVal sender As Object, ByVal e As System.EventArgs)
Dim auxValue as Int32=-1
If IsDBNull(Eval("grpcodeditor")) Then 
     Ctype(sender,Control).Visible=False
Else
  If Eval("grpcodeditor") < 1 Then 
     Ctype(sender,Control).Visible=False
  End If
End If

End Sub
Protected Sub itemctrl000005_Databound(ByVal sender As Object, ByVal e As System.EventArgs)
If IsDBNull(Eval("grpcodeditor")) Then
 CType(sender,HiddenField).Value=""
Else
 CType(sender,HiddenField).Value=Eval("grpcodeditor").ToString()
End If

End Sub

Protected Sub itemctrl000004_Databound(ByVal sender As Object, ByVal e As System.EventArgs)
CType(sender,Label).Text="<img src=imagenes/"
If IsDBNull(Eval("decision")) Then
CType(sender,Label).Text &="actundefined.gif alt=S/D "
ElseIf Eval("decision") Then
CType(sender,Label).Text &="actyes.gif alt=Si "
Else
CType(sender,Label).Text &="actno.gif alt=No "
End If
CType(sender,Label).Text &=" width=12px border=0px />"

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

Protected Sub editctrl000019_DataBound_edittabPanel001(ByVal sender As Object, ByVal e As System.EventArgs)
CType(sender,TextBox).Text=Eval("dsc").ToString

End Sub

Protected Sub dseditctrl000018_Init(ByVal sender As Object,ByVal e As System.EventArgs)
sender.ConnectionString=Session("connectionstringname")
End Sub
Protected Sub dseditctrl000018_Deleted(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.SqlDataSourceStatusEventArgs)
If e.Exception IsNot Nothing Then
  lblerror.Text &="//Error borrando datos (" & e.Exception.Message & ")"
    
Else
    
End If

End Sub
Protected Sub dseditctrl000018_Inserted(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.SqlDataSourceStatusEventArgs)
If e.Exception IsNot Nothing Then
  lblerror.Text &="//Error insertando datos (" & e.Exception.Message & ")"
    
Else
    
End If

End Sub
Protected Sub dseditctrl000018_Selected(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.SqlDataSourceStatusEventArgs)
If e.Exception IsNot Nothing Then
  lblerror.Text &="//Error buscando datos (" & e.Exception.Message & ")"
    
Else
    
End If

End Sub
Protected Sub dseditctrl000018_Updated(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.SqlDataSourceStatusEventArgs)
If e.Exception IsNot Nothing Then
  lblerror.Text &="//Error actualizando datos (" & e.Exception.Message & ")"
    
Else
    
End If

End Sub

Protected Sub editctrl000018_Databound(ByVal sender As Object, ByVal e As System.EventArgs)
    If IsDBNull(DataBinder.Eval(frmdatos.DataItem,"undtipcod")) = False  Then
        If CType(sender,DropDownList).Items.FindByValue(DataBinder.Eval(frmdatos.DataItem,"undtipcod")) IsNot Nothing Then
            CType(sender,DropDownList).SelectedValue=DataBinder.Eval(frmdatos.DataItem,"undtipcod")
        End If
    End If

End Sub
Protected Sub editctrl000018view_DataBinding(ByVal sender As Object, ByVal e As System.EventArgs)
If IsDBNull(Eval("undtipcod")) Then 
     Ctype(sender,Control).Visible=False
Else
  If Eval("undtipcod") = -1 Then 
     Ctype(sender,Control).Visible=False
  End If
End If

End Sub

Protected Sub dseditctrl000017_Init(ByVal sender As Object,ByVal e As System.EventArgs)
sender.ConnectionString=Session("connectionstringname")
End Sub
Protected Sub dseditctrl000017_Deleted(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.SqlDataSourceStatusEventArgs)
If e.Exception IsNot Nothing Then
  lblerror.Text &="//Error borrando datos (" & e.Exception.Message & ")"
    
Else
    
End If

End Sub
Protected Sub dseditctrl000017_Inserted(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.SqlDataSourceStatusEventArgs)
If e.Exception IsNot Nothing Then
  lblerror.Text &="//Error insertando datos (" & e.Exception.Message & ")"
    
Else
    
End If

End Sub
Protected Sub dseditctrl000017_Selected(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.SqlDataSourceStatusEventArgs)
If e.Exception IsNot Nothing Then
  lblerror.Text &="//Error buscando datos (" & e.Exception.Message & ")"
    
Else
    
End If

End Sub
Protected Sub dseditctrl000017_Updated(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.SqlDataSourceStatusEventArgs)
If e.Exception IsNot Nothing Then
  lblerror.Text &="//Error actualizando datos (" & e.Exception.Message & ")"
    
Else
    
End If

End Sub

Protected Sub editctrl000017view_DataBinding(ByVal sender As Object, ByVal e As System.EventArgs)
Dim auxValue as Int32=-1
If IsDBNull(Eval("resp")) Then 
     Ctype(sender,Control).Visible=False
Else
  If Eval("resp") < 1 Then 
     Ctype(sender,Control).Visible=False
  End If
End If

End Sub
Protected Sub editctrl000017delete_Click (ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs)
editctrl000017type.Value=-1
editctrl000017.Value=-1
editctrl000017view.Visible=False
editctrl000017delete.Visible=False

End Sub
Protected Sub editctrl000017delete_DataBinding(ByVal sender As Object, ByVal e As System.EventArgs)
If Not IsPostBack  Then 
 If IsDBNull(Eval("resp")) Then 
    CType(sender,Control).Visible=False
  ElseIf Eval("resp") < 1 Then
    CType(sender,Control).Visible=False
  Else
    CType(sender,Control).Visible=True
 End If
End If

End Sub
Protected Sub cmdeditctrl000017showpanel_Click (ByVal sender As Object, ByVal e As System.EventArgs)
objectexplorer_Show(CType(sender.parent.FindControl("editctrl000017"),HiddenField).UniqueID,CType(sender.parent.FindControl("editctrl000017view"),LinkButton).UniqueID,CType(sender.parent.FindControl("editctrl000017type"),HiddenField).UniqueID,"10","9",CType(sender.parent.FindControl("editctrl000017delete"),ImageButton).UniqueID)
End Sub
Protected Sub editctrl000017_Databound(ByVal sender As Object, ByVal e As System.EventArgs)
If IsDBNull(Eval("resp")) Then
 CType(sender,HiddenField).Value=""
Else
 CType(sender,HiddenField).Value=Eval("resp").ToString()
End If

End Sub

Protected Sub dseditctrl000016_Init(ByVal sender As Object,ByVal e As System.EventArgs)
sender.ConnectionString=Session("connectionstringname")
End Sub
Protected Sub dseditctrl000016_Deleted(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.SqlDataSourceStatusEventArgs)
If e.Exception IsNot Nothing Then
  lblerror.Text &="//Error borrando datos (" & e.Exception.Message & ")"
    
Else
    
End If

End Sub
Protected Sub dseditctrl000016_Inserted(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.SqlDataSourceStatusEventArgs)
If e.Exception IsNot Nothing Then
  lblerror.Text &="//Error insertando datos (" & e.Exception.Message & ")"
    
Else
    
End If

End Sub
Protected Sub dseditctrl000016_Selected(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.SqlDataSourceStatusEventArgs)
If e.Exception IsNot Nothing Then
  lblerror.Text &="//Error buscando datos (" & e.Exception.Message & ")"
    
Else
    
End If

End Sub
Protected Sub dseditctrl000016_Updated(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.SqlDataSourceStatusEventArgs)
If e.Exception IsNot Nothing Then
  lblerror.Text &="//Error actualizando datos (" & e.Exception.Message & ")"
    
Else
    
End If

End Sub

Protected Sub editctrl000016view_DataBinding(ByVal sender As Object, ByVal e As System.EventArgs)
Dim auxValue as Int32=-1
If IsDBNull(Eval("undcodsup")) Then 
     Ctype(sender,Control).Visible=False
Else
  If Eval("undcodsup") < 1 Then 
     Ctype(sender,Control).Visible=False
  End If
End If

End Sub
Protected Sub editctrl000016delete_Click (ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs)
editctrl000016type.Value=-1
editctrl000016.Value=-1
editctrl000016view.Visible=False
editctrl000016delete.Visible=False

End Sub
Protected Sub editctrl000016delete_DataBinding(ByVal sender As Object, ByVal e As System.EventArgs)
If Not IsPostBack  Then 
 If IsDBNull(Eval("undcodsup")) Then 
    CType(sender,Control).Visible=False
  ElseIf Eval("undcodsup") < 1 Then
    CType(sender,Control).Visible=False
  Else
    CType(sender,Control).Visible=True
 End If
End If

End Sub
Protected Sub cmdeditctrl000016showpanel_Click (ByVal sender As Object, ByVal e As System.EventArgs)
objectexplorer_Show(CType(sender.parent.FindControl("editctrl000016"),HiddenField).UniqueID,CType(sender.parent.FindControl("editctrl000016view"),LinkButton).UniqueID,CType(sender.parent.FindControl("editctrl000016type"),HiddenField).UniqueID,"10","10",CType(sender.parent.FindControl("editctrl000016delete"),ImageButton).UniqueID)
End Sub
Protected Sub editctrl000016_Databound(ByVal sender As Object, ByVal e As System.EventArgs)
If IsDBNull(Eval("undcodsup")) Then
 CType(sender,HiddenField).Value=""
Else
 CType(sender,HiddenField).Value=Eval("undcodsup").ToString()
End If

End Sub

Protected Sub editctrl000015_Databound(ByVal sender As Object, ByVal e As System.EventArgs)
CType(sender,Label).Text=Replace(Eval("sectorid").ToString,Chr(10),"<br />")

End Sub

Protected Sub editctrl000014_DataBound_edittabPanel001(ByVal sender As Object, ByVal e As System.EventArgs)
CType(sender,TextBox).Text=Eval("undnro").ToString

End Sub

Protected Sub editctrl000013_DataBound_edittabPanel001(ByVal sender As Object, ByVal e As System.EventArgs)
CType(sender,TextBox).Text=Eval("orden").ToString

End Sub

Protected Sub editctrl000012view_DataBinding(ByVal sender As Object, ByVal e As System.EventArgs)
Dim auxValue as Int32=-1
If IsDBNull(Eval("miembrosgrpcod")) Then 
     Ctype(sender,Control).Visible=False
Else
  If Eval("miembrosgrpcod") < 1 Then 
     Ctype(sender,Control).Visible=False
  End If
End If

End Sub
Protected Sub editctrl000012_Databound(ByVal sender As Object, ByVal e As System.EventArgs)
If IsDBNull(Eval("miembrosgrpcod")) Then
 CType(sender,HiddenField).Value=""
Else
 CType(sender,HiddenField).Value=Eval("miembrosgrpcod").ToString()
End If

End Sub

Protected Sub editctrl000011view_DataBinding(ByVal sender As Object, ByVal e As System.EventArgs)
Dim auxValue as Int32=-1
If IsDBNull(Eval("grpcodresp")) Then 
     Ctype(sender,Control).Visible=False
Else
  If Eval("grpcodresp") < 1 Then 
     Ctype(sender,Control).Visible=False
  End If
End If

End Sub
Protected Sub editctrl000011_Databound(ByVal sender As Object, ByVal e As System.EventArgs)
If IsDBNull(Eval("grpcodresp")) Then
 CType(sender,HiddenField).Value=""
Else
 CType(sender,HiddenField).Value=Eval("grpcodresp").ToString()
End If

End Sub

Protected Sub editctrl000010view_DataBinding(ByVal sender As Object, ByVal e As System.EventArgs)
Dim auxValue as Int32=-1
If IsDBNull(Eval("grpcodprjver")) Then 
     Ctype(sender,Control).Visible=False
Else
  If Eval("grpcodprjver") < 1 Then 
     Ctype(sender,Control).Visible=False
  End If
End If

End Sub
Protected Sub editctrl000010_Databound(ByVal sender As Object, ByVal e As System.EventArgs)
If IsDBNull(Eval("grpcodprjver")) Then
 CType(sender,HiddenField).Value=""
Else
 CType(sender,HiddenField).Value=Eval("grpcodprjver").ToString()
End If

End Sub

Protected Sub editctrl000009view_DataBinding(ByVal sender As Object, ByVal e As System.EventArgs)
Dim auxValue as Int32=-1
If IsDBNull(Eval("grpcodmbrdir")) Then 
     Ctype(sender,Control).Visible=False
Else
  If Eval("grpcodmbrdir") < 1 Then 
     Ctype(sender,Control).Visible=False
  End If
End If

End Sub
Protected Sub editctrl000009_Databound(ByVal sender As Object, ByVal e As System.EventArgs)
If IsDBNull(Eval("grpcodmbrdir")) Then
 CType(sender,HiddenField).Value=""
Else
 CType(sender,HiddenField).Value=Eval("grpcodmbrdir").ToString()
End If

End Sub

Protected Sub editctrl000008_Databound(ByVal sender As Object, ByVal e As System.EventArgs)
    Dim auxItem as ListItem=CType(sender,RadioButtonList).Items.FindByValue(DataBinder.Eval(frmdatos.DataItem,"creatorsmbrdir").ToString())
    If auxItem IsNot Nothing
       CType(sender,RadioButtonList).ClearSelection()
       auxItem.Selected=True
    End If

End Sub

Protected Sub editctrl000007_DataBound_edittabPanel002(ByVal sender As Object, ByVal e As System.EventArgs)
CType(sender,TextBox).Text=Eval("formatoespecifico").ToString

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

Protected Sub editctrl000006view_DataBinding(ByVal sender As Object, ByVal e As System.EventArgs)
Dim auxValue as Int32=-1
If IsDBNull(Eval("editor")) Then 
     Ctype(sender,Control).Visible=False
Else
  If Eval("editor") < 1 Then 
     Ctype(sender,Control).Visible=False
  End If
End If

End Sub
Protected Sub editctrl000006delete_Click (ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs)
editctrl000006type.Value=-1
editctrl000006.Value=-1
editctrl000006view.Visible=False
editctrl000006delete.Visible=False

End Sub
Protected Sub editctrl000006delete_DataBinding(ByVal sender As Object, ByVal e As System.EventArgs)
If Not IsPostBack  Then 
 If IsDBNull(Eval("editor")) Then 
    CType(sender,Control).Visible=False
  ElseIf Eval("editor") < 1 Then
    CType(sender,Control).Visible=False
  Else
    CType(sender,Control).Visible=True
 End If
End If

End Sub
Protected Sub cmdeditctrl000006showpanel_Click (ByVal sender As Object, ByVal e As System.EventArgs)
objectexplorer_Show(CType(sender.parent.FindControl("editctrl000006"),HiddenField).UniqueID,CType(sender.parent.FindControl("editctrl000006view"),LinkButton).UniqueID,CType(sender.parent.FindControl("editctrl000006type"),HiddenField).UniqueID,"10","9",CType(sender.parent.FindControl("editctrl000006delete"),ImageButton).UniqueID)
End Sub
Protected Sub editctrl000006_Databound(ByVal sender As Object, ByVal e As System.EventArgs)
If IsDBNull(Eval("editor")) Then
 CType(sender,HiddenField).Value=""
Else
 CType(sender,HiddenField).Value=Eval("editor").ToString()
End If

End Sub

Protected Sub editctrl000005view_DataBinding(ByVal sender As Object, ByVal e As System.EventArgs)
Dim auxValue as Int32=-1
If IsDBNull(Eval("grpcodeditor")) Then 
     Ctype(sender,Control).Visible=False
Else
  If Eval("grpcodeditor") < 1 Then 
     Ctype(sender,Control).Visible=False
  End If
End If

End Sub
Protected Sub editctrl000005_Databound(ByVal sender As Object, ByVal e As System.EventArgs)
If IsDBNull(Eval("grpcodeditor")) Then
 CType(sender,HiddenField).Value=""
Else
 CType(sender,HiddenField).Value=Eval("grpcodeditor").ToString()
End If

End Sub

Protected Sub editctrl000004_Databound(ByVal sender As Object, ByVal e As System.EventArgs)
    If IsDBNull(DataBinder.Eval(frmdatos.DataItem,"decision"))  Then
        CType(sender,CheckBox).Checked=False
    Else
        CType(sender,CheckBox).Checked=DataBinder.Eval(frmdatos.DataItem,"decision")
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

Protected Sub dsinsctrl000018_Init(ByVal sender As Object,ByVal e As System.EventArgs)
sender.ConnectionString=Session("connectionstringname")
End Sub
Protected Sub dsinsctrl000018_Deleted(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.SqlDataSourceStatusEventArgs)
If e.Exception IsNot Nothing Then
  lblerror.Text &="//Error borrando datos (" & e.Exception.Message & ")"
    
Else
    
End If

End Sub
Protected Sub dsinsctrl000018_Inserted(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.SqlDataSourceStatusEventArgs)
If e.Exception IsNot Nothing Then
  lblerror.Text &="//Error insertando datos (" & e.Exception.Message & ")"
    
Else
    
End If

End Sub
Protected Sub dsinsctrl000018_Selected(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.SqlDataSourceStatusEventArgs)
If e.Exception IsNot Nothing Then
  lblerror.Text &="//Error buscando datos (" & e.Exception.Message & ")"
    
Else
    
End If

End Sub
Protected Sub dsinsctrl000018_Updated(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.SqlDataSourceStatusEventArgs)
If e.Exception IsNot Nothing Then
  lblerror.Text &="//Error actualizando datos (" & e.Exception.Message & ")"
    
Else
    
End If

End Sub

Protected Sub insctrl000018_Load(ByVal sender As Object, ByVal e As System.EventArgs)
If Not Page.IsPostBack Then 
    CType(sender,DropDownList).DataBind
    If CType(sender,DropDownList).Items.FindByValue("-1") IsNot Nothing Then
        CType(sender,DropDownList).Items.FindByValue("-1").Selected = True
    ElseIf CType(sender,DropDownList).Items.Count > 0 Then
        CType(sender,DropDownList).Items(0).Selected = True
    End If
End If

End Sub

Protected Sub dsinsctrl000017_Init(ByVal sender As Object,ByVal e As System.EventArgs)
sender.ConnectionString=Session("connectionstringname")
End Sub
Protected Sub dsinsctrl000017_Deleted(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.SqlDataSourceStatusEventArgs)
If e.Exception IsNot Nothing Then
  lblerror.Text &="//Error borrando datos (" & e.Exception.Message & ")"
    
Else
    
End If

End Sub
Protected Sub dsinsctrl000017_Inserted(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.SqlDataSourceStatusEventArgs)
If e.Exception IsNot Nothing Then
  lblerror.Text &="//Error insertando datos (" & e.Exception.Message & ")"
    
Else
    
End If

End Sub
Protected Sub dsinsctrl000017_Selected(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.SqlDataSourceStatusEventArgs)
If e.Exception IsNot Nothing Then
  lblerror.Text &="//Error buscando datos (" & e.Exception.Message & ")"
    
Else
    
End If

End Sub
Protected Sub dsinsctrl000017_Updated(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.SqlDataSourceStatusEventArgs)
If e.Exception IsNot Nothing Then
  lblerror.Text &="//Error actualizando datos (" & e.Exception.Message & ")"
    
Else
    
End If

End Sub

Protected Sub insctrl000017delete_Click (ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs)
insctrl000017type.Value=-1
insctrl000017.Value=-1
insctrl000017view.Visible=False
insctrl000017delete.Visible=False

End Sub
Protected Sub insctrl000017delete_DataBinding(ByVal sender As Object, ByVal e As System.EventArgs)
If Not IsPostBack  Then 
 If IsDBNull(Eval("resp")) Then 
    CType(sender,Control).Visible=False
  ElseIf Eval("resp") < 1 Then
    CType(sender,Control).Visible=False
  Else
    CType(sender,Control).Visible=True
 End If
End If

End Sub
Protected Sub cmdinsctrl000017showpanel_Click (ByVal sender As Object, ByVal e As System.EventArgs)
objectexplorer_Show(CType(sender.parent.FindControl("insctrl000017"),HiddenField).UniqueID,CType(sender.parent.FindControl("insctrl000017view"),LinkButton).UniqueID,CType(sender.parent.FindControl("insctrl000017type"),HiddenField).UniqueID,"10","9",CType(sender.parent.FindControl("insctrl000017delete"),ImageButton).UniqueID)
End Sub
Protected Sub insctrl000017_Load(ByVal sender As Object, ByVal e As System.EventArgs)
If Not IsPostBack Then CType(sender,HiddenField).Value=-1

End Sub

Protected Sub dsinsctrl000016_Init(ByVal sender As Object,ByVal e As System.EventArgs)
sender.ConnectionString=Session("connectionstringname")
End Sub
Protected Sub dsinsctrl000016_Deleted(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.SqlDataSourceStatusEventArgs)
If e.Exception IsNot Nothing Then
  lblerror.Text &="//Error borrando datos (" & e.Exception.Message & ")"
    
Else
    
End If

End Sub
Protected Sub dsinsctrl000016_Inserted(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.SqlDataSourceStatusEventArgs)
If e.Exception IsNot Nothing Then
  lblerror.Text &="//Error insertando datos (" & e.Exception.Message & ")"
    
Else
    
End If

End Sub
Protected Sub dsinsctrl000016_Selected(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.SqlDataSourceStatusEventArgs)
If e.Exception IsNot Nothing Then
  lblerror.Text &="//Error buscando datos (" & e.Exception.Message & ")"
    
Else
    
End If

End Sub
Protected Sub dsinsctrl000016_Updated(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.SqlDataSourceStatusEventArgs)
If e.Exception IsNot Nothing Then
  lblerror.Text &="//Error actualizando datos (" & e.Exception.Message & ")"
    
Else
    
End If

End Sub

Protected Sub insctrl000016delete_Click (ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs)
insctrl000016type.Value=-1
insctrl000016.Value=-1
insctrl000016view.Visible=False
insctrl000016delete.Visible=False

End Sub
Protected Sub insctrl000016delete_DataBinding(ByVal sender As Object, ByVal e As System.EventArgs)
If Not IsPostBack  Then 
 If IsDBNull(Eval("undcodsup")) Then 
    CType(sender,Control).Visible=False
  ElseIf Eval("undcodsup") < 1 Then
    CType(sender,Control).Visible=False
  Else
    CType(sender,Control).Visible=True
 End If
End If

End Sub
Protected Sub cmdinsctrl000016showpanel_Click (ByVal sender As Object, ByVal e As System.EventArgs)
objectexplorer_Show(CType(sender.parent.FindControl("insctrl000016"),HiddenField).UniqueID,CType(sender.parent.FindControl("insctrl000016view"),LinkButton).UniqueID,CType(sender.parent.FindControl("insctrl000016type"),HiddenField).UniqueID,"10","10",CType(sender.parent.FindControl("insctrl000016delete"),ImageButton).UniqueID)
End Sub
Protected Sub insctrl000016_Load(ByVal sender As Object, ByVal e As System.EventArgs)
If Not IsPostBack Then CType(sender,HiddenField).Value=-1

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

Protected Sub insctrl000006delete_Click (ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs)
insctrl000006type.Value=-1
insctrl000006.Value=-1
insctrl000006view.Visible=False
insctrl000006delete.Visible=False

End Sub
Protected Sub insctrl000006delete_DataBinding(ByVal sender As Object, ByVal e As System.EventArgs)
If Not IsPostBack  Then 
 If IsDBNull(Eval("editor")) Then 
    CType(sender,Control).Visible=False
  ElseIf Eval("editor") < 1 Then
    CType(sender,Control).Visible=False
  Else
    CType(sender,Control).Visible=True
 End If
End If

End Sub
Protected Sub cmdinsctrl000006showpanel_Click (ByVal sender As Object, ByVal e As System.EventArgs)
objectexplorer_Show(CType(sender.parent.FindControl("insctrl000006"),HiddenField).UniqueID,CType(sender.parent.FindControl("insctrl000006view"),LinkButton).UniqueID,CType(sender.parent.FindControl("insctrl000006type"),HiddenField).UniqueID,"10","9",CType(sender.parent.FindControl("insctrl000006delete"),ImageButton).UniqueID)
End Sub
Protected Sub insctrl000006_Load(ByVal sender As Object, ByVal e As System.EventArgs)
If Not IsPostBack Then CType(sender,HiddenField).Value=-1

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
         Response.Redirect(Session("ROOTWEBURL") & "frmUnidades.aspx?_mode_=7&_closea_=0")
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
    Dim auxResult as String=auxClass.gSystem_PostAction(10,13,auxDataKeyCod) 
    auxClass.gCluster_LoadStaticTables_UND(auxDataKeyCod) 
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
    
    Dim auxResult as String=auxClass.gSystem_PostAction(10,11,auxDataKeyCod) 
    auxClass.gCluster_LoadStaticTables_UND(auxDataKeyCod) 
    lblerror.Text &=auxResult
    If auxResult ="" Then
    End If
    auxClass.Conn.gConn_Close
End If

End Sub
Protected Sub frmdatos_ItemSelected(ByVal sender As Object, ByVal e As System.EventArgs)
If frmdatos.DataItem IsNot Nothing Then
   Page.Title = "Unidades| " &  Databinder.Eval(frmdatos.DataItem ,"dsc").ToString
   lblsubtitle.Text = Databinder.Eval(frmdatos.DataItem ,"dsc").ToString
Else
   Page.Title = "Unidades| Nuevo"
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
     Dim auxMax As Integer = Val(LicParam.gValue_Get("UND.MAX", -1))
     If auxMax >= 0 Then
         Dim auxConn As clsHrcConnClient = Session("conn")
         auxConn.gComponent_CreateInstance()
         auxConn.gConn_Open()
         If Val(auxConn.gConn_QueryValueInt("SELECT COUNT(*) FROM UND", 0)) > auxMax Then
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
        Dim auxDA As New SqlDataAdapter("SELECT UND.cod,UND.dsc,(UNDUNDTIPCOD.dsc) as undtipcoddsc,(UNDRESP.dsc) as respdsc,(UNDUNDCODSUP.dsc) as undcodsupdsc,UND.sectorid,UND.undnro,UND.orden,(UNDMIEMBROSGRPCOD.grpdsc) as miembrosgrpcoddsc,(UNDGRPCODRESP.grpdsc) as grpcodrespdsc,(UNDGRPCODPRJVER.grpdsc) as grpcodprjverdsc,UND.creatorsmbrdir,UND.formatoespecifico,(UNDEDITOR.dsc) as editordsc,(UNDGRPCODEDITOR.grpdsc) as grpcodeditordsc,UND.baja,UND.decision,(UNDGRPCODMBRDIR.grpdsc) as grpcodmbrdirdsc,(UNDQSECSID.secdsc) as qsecsiddsc,UND.qsecdatetime,UND.undtipcod,UND.resp,UND.undcodsup,UND.miembrosgrpcod,UND.grpcodresp,UND.grpcodprjver,UND.editor,UND.grpcodeditor,UND.grpcodmbrdir,UND.qsecsid FROM UND  LEFT JOIN UND AS UNDUNDCODSUP ON UNDUNDCODSUP.cod=UND.undcodsup LEFT JOIN EMP AS UNDRESP ON UNDRESP.cod=UND.resp LEFT JOIN Q_SECPGRP AS UNDGRPCODPRJVER ON UNDGRPCODPRJVER.grpcod=UND.grpcodprjver LEFT JOIN Q_SECPGRP AS UNDGRPCODRESP ON UNDGRPCODRESP.grpcod=UND.grpcodresp LEFT JOIN Q_SECPGRP AS UNDMIEMBROSGRPCOD ON UNDMIEMBROSGRPCOD.grpcod=UND.miembrosgrpcod LEFT JOIN EMP AS UNDEDITOR ON UNDEDITOR.cod=UND.editor LEFT JOIN Q_SECPGRP AS UNDGRPCODEDITOR ON UNDGRPCODEDITOR.grpcod=UND.grpcodeditor LEFT JOIN DOCTIP AS UNDUNDTIPCOD ON UNDUNDTIPCOD.cod=UND.undtipcod LEFT JOIN Q_SECPGRP AS UNDGRPCODMBRDIR ON UNDGRPCODMBRDIR.grpcod=UND.grpcodmbrdir LEFT JOIN Q_SECPLOGIN AS UNDQSECSID ON UNDQSECSID.sidcod=UND.qsecsid  WHERE (UND.cod=@param1) AND (UND.baja = 0 OR UND.baja  IS NULL)", auxConn)
        auxDA.SelectCommand.Parameters.Add("@param1", SqlDbType.Int, 4).Value = Request.QueryString("param1")
        Dim auxDT As New DataTable
        auxDA.Fill(auxDT)
        auxConn.Close
        If auxDT.Rows.Count <> 0 Then
             If IsDBNull(auxDT.Rows(0)("cod")) = False Then
             CType(sender.parent.FindControl("frmdatos$insctrl000021"),TextBox).Text=auxDT.Rows(0)("cod")
             End If 
             If IsDBNull(auxDT.Rows(0)("dsc")) = False Then
             CType(sender.parent.FindControl("frmdatos$instabPanel$instabPanel001$insctrl000019"),TextBox).Text=auxDT.Rows(0)("dsc")
             End If 
             If IsDBNull(auxDT.Rows(0)("undtipcod")) = False Then
             CType(sender.parent.FindControl("frmdatos$instabPanel$instabPanel001$insctrl000018"),DropDownList).DataBind
             If CType(sender.parent.FindControl("frmdatos$instabPanel$instabPanel001$insctrl000018"),DropDownList).Items.FindByValue(auxDT.Rows(0)("undtipcod")) IsNot Nothing Then
                 CType(sender.parent.FindControl("frmdatos$instabPanel$instabPanel001$insctrl000018"),DropDownList).ClearSelection()
                 CType(sender.parent.FindControl("frmdatos$instabPanel$instabPanel001$insctrl000018"),DropDownList).Items.FindByValue(auxDT.Rows(0)("undtipcod")).Selected = True
             End If
             End If 
             If IsDBNull(auxDT.Rows(0)("resp")) = False Then
     If IsDBNull("" & auxDT.Rows(0)("resp") & "")=False Then
         If CInt("" & auxDT.Rows(0)("resp") & "") > 0 Then
          CType(sender.parent.FindControl("frmdatos$instabPanel$instabPanel001$insctrl000017"),HiddenField).Value="" & auxDT.Rows(0)("resp") & ""
          CType(sender.parent.FindControl("frmdatos$instabPanel$instabPanel001$insctrl000017view"),LinkButton).Text=auxDT.Rows(0)("respdsc")
          CType(sender.parent.FindControl("frmdatos$instabPanel$instabPanel001$insctrl000017delete"),ImageButton).Visible=True
         End If
      End If
             End If 
             If IsDBNull(auxDT.Rows(0)("undcodsup")) = False Then
     If IsDBNull("" & auxDT.Rows(0)("undcodsup") & "")=False Then
         If CInt("" & auxDT.Rows(0)("undcodsup") & "") > 0 Then
          CType(sender.parent.FindControl("frmdatos$instabPanel$instabPanel001$insctrl000016"),HiddenField).Value="" & auxDT.Rows(0)("undcodsup") & ""
          CType(sender.parent.FindControl("frmdatos$instabPanel$instabPanel001$insctrl000016view"),LinkButton).Text=auxDT.Rows(0)("undcodsupdsc")
          CType(sender.parent.FindControl("frmdatos$instabPanel$instabPanel001$insctrl000016delete"),ImageButton).Visible=True
         End If
      End If
             End If 
             If IsDBNull(auxDT.Rows(0)("undnro")) = False Then
             CType(sender.parent.FindControl("frmdatos$instabPanel$instabPanel001$insctrl000014"),TextBox).Text=auxDT.Rows(0)("undnro")
             End If 
             If IsDBNull(auxDT.Rows(0)("orden")) = False Then
             CType(sender.parent.FindControl("frmdatos$instabPanel$instabPanel001$insctrl000013"),TextBox).Text=auxDT.Rows(0)("orden")
             End If 
             If IsDBNull(auxDT.Rows(0)("creatorsmbrdir")) = False Then
             CType(sender.parent.FindControl("frmdatos$instabPanel$instabPanel002$insctrl000008"),RadioButtonList).Items.FindByValue(auxDT.Rows(0)("creatorsmbrdir")).Selected=True
             End If 
             If IsDBNull(auxDT.Rows(0)("formatoespecifico")) = False Then
             CType(sender.parent.FindControl("frmdatos$instabPanel$instabPanel002$insctrl000007"),TextBox).Text=auxDT.Rows(0)("formatoespecifico")
             End If 
             If IsDBNull(auxDT.Rows(0)("editor")) = False Then
     If IsDBNull("" & auxDT.Rows(0)("editor") & "")=False Then
         If CInt("" & auxDT.Rows(0)("editor") & "") > 0 Then
          CType(sender.parent.FindControl("frmdatos$instabPanel$instabPanel002$insctrl000006"),HiddenField).Value="" & auxDT.Rows(0)("editor") & ""
          CType(sender.parent.FindControl("frmdatos$instabPanel$instabPanel002$insctrl000006view"),LinkButton).Text=auxDT.Rows(0)("editordsc")
          CType(sender.parent.FindControl("frmdatos$instabPanel$instabPanel002$insctrl000006delete"),ImageButton).Visible=True
         End If
      End If
             End If 
             If IsDBNull(auxDT.Rows(0)("decision")) = False Then
             CType(sender.parent.FindControl("frmdatos$instabPanel$instabPanel003$insctrl000004"),CheckBox).Checked=auxDT.Rows(0)("decision")
             End If 
        End If
    Catch ex as sqlException
    End Try
End Select
Select Case Request.QueryString("_mode_")
 Case "1","25"
Dim auxClass As New clscusimDOC
auxClass.Conn.gConn_Open()
Dim auxBagValues As clshrcBagValues = auxClass.gSystem_PostAction_v2(10,Val(Request.QueryString("_mode_")), -1, Nothing)
If auxBagValues IsNot Nothing Then
     Dim auxValue As Object
auxValue = auxBagValues.gValue_Get("undtipcod.FILTERVALUES")
If auxValue IsNot Nothing Then
 If auxBagValues.gValue_Get("HRC_CANCEL", False) Then
     gForm_Close()
 End If
 Dim auxValues As List(Of Integer) = auxValue
 CType(CType(sender.parent.FindControl("frmdatos$instabPanel$instabPanel001$insctrl000018"), DropDownList).DataSourceObject, SqlDataSource).FilterExpression="cod IN (" & auxClass.Conn.gFieldDB_GetString(auxValues) & ")"
 End If 
End If
auxClass.Conn.gConn_Close()
End Select

Select Case Request.QueryString("_mode_")
 Case "2"
Dim auxClass As New clscusimDOC
auxClass.Conn.gConn_Open()
Dim auxBagValues As clshrcBagValues = auxClass.gSystem_PostAction_v2(10,Val(Request.QueryString("_mode_")),Databinder.Eval(frmdatos.DataItem ,"cod"), Nothing)
If auxBagValues IsNot Nothing Then
     Dim auxValue As Object
auxValue = auxBagValues.gValue_Get("undtipcod.FILTERVALUES")
If auxValue IsNot Nothing Then
 Dim auxValues As List(Of Integer) = auxValue
 CType(CType(sender.parent.FindControl("frmdatos$edittabPanel$edittabPanel001$editctrl000018"), DropDownList).DataSourceObject, SqlDataSource).FilterExpression="cod IN (" & auxClass.Conn.gFieldDB_GetString(auxValues) & ")"
 End If 
End If
auxClass.Conn.gConn_Close()
End Select

Select Case Request.QueryString("_mode_")
 Case "2"
Dim auxClass As New clscusimDOC
auxClass.Conn.gConn_Open()
Dim auxBagValues As clshrcBagValues = auxClass.gSystem_PostAction_v2(10,Val(Request.QueryString("_mode_")),Databinder.Eval(frmdatos.DataItem ,"cod"), Nothing)
If auxBagValues IsNot Nothing Then
     Dim auxValue As Object
auxValue = auxBagValues.gValue_Get("undtipcod.FILTERVALUES")
If auxValue IsNot Nothing Then
 Dim auxValues As List(Of Integer) = auxValue
 CType(CType(sender.parent.FindControl("frmdatos$edittabPanel$edittabPanel001$editctrl000018"), DropDownList).DataSourceObject, SqlDataSource).FilterExpression="cod IN (" & auxClass.Conn.gFieldDB_GetString(auxValues) & ")"
 End If 
End If
auxClass.Conn.gConn_Close()
End Select

Select Case Request.QueryString("_mode_")
 Case "2"
Dim auxClass As New clscusimDOC
auxClass.Conn.gConn_Open()
Dim auxBagValues As clshrcBagValues = auxClass.gSystem_PostAction_v2(10,Val(Request.QueryString("_mode_")),Databinder.Eval(frmdatos.DataItem ,"cod"), Nothing)
If auxBagValues IsNot Nothing Then
     Dim auxValue As Object
auxValue = auxBagValues.gValue_Get("undtipcod.FILTERVALUES")
If auxValue IsNot Nothing Then
 Dim auxValues As List(Of Integer) = auxValue
 CType(CType(sender.parent.FindControl("frmdatos$edittabPanel$edittabPanel001$editctrl000018"), DropDownList).DataSourceObject, SqlDataSource).FilterExpression="cod IN (" & auxClass.Conn.gFieldDB_GetString(auxValues) & ")"
 End If 
End If
auxClass.Conn.gConn_Close()
End Select

Select Case Request.QueryString("_mode_")
 Case "2"
Dim auxClass As New clscusimDOC
auxClass.Conn.gConn_Open()
Dim auxBagValues As clshrcBagValues = auxClass.gSystem_PostAction_v2(10,Val(Request.QueryString("_mode_")),Databinder.Eval(frmdatos.DataItem ,"cod"), Nothing)
If auxBagValues IsNot Nothing Then
     Dim auxValue As Object
auxValue = auxBagValues.gValue_Get("undtipcod.FILTERVALUES")
If auxValue IsNot Nothing Then
 Dim auxValues As List(Of Integer) = auxValue
 CType(CType(sender.parent.FindControl("frmdatos$edittabPanel$edittabPanel001$editctrl000018"), DropDownList).DataSourceObject, SqlDataSource).FilterExpression="cod IN (" & auxClass.Conn.gFieldDB_GetString(auxValues) & ")"
 End If 
End If
auxClass.Conn.gConn_Close()
End Select

Select Case Request.QueryString("_mode_")
 Case "2"
Dim auxClass As New clscusimDOC
auxClass.Conn.gConn_Open()
Dim auxBagValues As clshrcBagValues = auxClass.gSystem_PostAction_v2(10,Val(Request.QueryString("_mode_")),Databinder.Eval(frmdatos.DataItem ,"cod"), Nothing)
If auxBagValues IsNot Nothing Then
     Dim auxValue As Object
auxValue = auxBagValues.gValue_Get("undtipcod.FILTERVALUES")
If auxValue IsNot Nothing Then
 Dim auxValues As List(Of Integer) = auxValue
 CType(CType(sender.parent.FindControl("frmdatos$edittabPanel$edittabPanel001$editctrl000018"), DropDownList).DataSourceObject, SqlDataSource).FilterExpression="cod IN (" & auxClass.Conn.gFieldDB_GetString(auxValues) & ")"
 End If 
End If
auxClass.Conn.gConn_Close()
End Select

Select Case Request.QueryString("_mode_")
 Case "2"
Dim auxClass As New clscusimDOC
auxClass.Conn.gConn_Open()
Dim auxBagValues As clshrcBagValues = auxClass.gSystem_PostAction_v2(10,Val(Request.QueryString("_mode_")),Databinder.Eval(frmdatos.DataItem ,"cod"), Nothing)
If auxBagValues IsNot Nothing Then
     Dim auxValue As Object
auxValue = auxBagValues.gValue_Get("undtipcod.FILTERVALUES")
If auxValue IsNot Nothing Then
 Dim auxValues As List(Of Integer) = auxValue
 CType(CType(sender.parent.FindControl("frmdatos$edittabPanel$edittabPanel001$editctrl000018"), DropDownList).DataSourceObject, SqlDataSource).FilterExpression="cod IN (" & auxClass.Conn.gFieldDB_GetString(auxValues) & ")"
 End If 
End If
auxClass.Conn.gConn_Close()
End Select

Select Case Request.QueryString("_mode_")
 Case "2"
Dim auxClass As New clscusimDOC
auxClass.Conn.gConn_Open()
Dim auxBagValues As clshrcBagValues = auxClass.gSystem_PostAction_v2(10,Val(Request.QueryString("_mode_")),Databinder.Eval(frmdatos.DataItem ,"cod"), Nothing)
If auxBagValues IsNot Nothing Then
     Dim auxValue As Object
auxValue = auxBagValues.gValue_Get("undtipcod.FILTERVALUES")
If auxValue IsNot Nothing Then
 Dim auxValues As List(Of Integer) = auxValue
 CType(CType(sender.parent.FindControl("frmdatos$edittabPanel$edittabPanel001$editctrl000018"), DropDownList).DataSourceObject, SqlDataSource).FilterExpression="cod IN (" & auxClass.Conn.gFieldDB_GetString(auxValues) & ")"
 End If 
End If
auxClass.Conn.gConn_Close()
End Select

Select Case Request.QueryString("_mode_")
 Case "2"
Dim auxClass As New clscusimDOC
auxClass.Conn.gConn_Open()
Dim auxBagValues As clshrcBagValues = auxClass.gSystem_PostAction_v2(10,Val(Request.QueryString("_mode_")),Databinder.Eval(frmdatos.DataItem ,"cod"), Nothing)
If auxBagValues IsNot Nothing Then
     Dim auxValue As Object
auxValue = auxBagValues.gValue_Get("undtipcod.FILTERVALUES")
If auxValue IsNot Nothing Then
 Dim auxValues As List(Of Integer) = auxValue
 CType(CType(sender.parent.FindControl("frmdatos$edittabPanel$edittabPanel001$editctrl000018"), DropDownList).DataSourceObject, SqlDataSource).FilterExpression="cod IN (" & auxClass.Conn.gFieldDB_GetString(auxValues) & ")"
 End If 
End If
auxClass.Conn.gConn_Close()
End Select

Select Case Request.QueryString("_mode_")
 Case "2"
Dim auxClass As New clscusimDOC
auxClass.Conn.gConn_Open()
Dim auxBagValues As clshrcBagValues = auxClass.gSystem_PostAction_v2(10,Val(Request.QueryString("_mode_")),Databinder.Eval(frmdatos.DataItem ,"cod"), Nothing)
If auxBagValues IsNot Nothing Then
     Dim auxValue As Object
auxValue = auxBagValues.gValue_Get("undtipcod.FILTERVALUES")
If auxValue IsNot Nothing Then
 Dim auxValues As List(Of Integer) = auxValue
 CType(CType(sender.parent.FindControl("frmdatos$edittabPanel$edittabPanel001$editctrl000018"), DropDownList).DataSourceObject, SqlDataSource).FilterExpression="cod IN (" & auxClass.Conn.gFieldDB_GetString(auxValues) & ")"
 End If 
End If
auxClass.Conn.gConn_Close()
End Select

Select Case Request.QueryString("_mode_")
 Case "2"
Dim auxClass As New clscusimDOC
auxClass.Conn.gConn_Open()
Dim auxBagValues As clshrcBagValues = auxClass.gSystem_PostAction_v2(10,Val(Request.QueryString("_mode_")),Databinder.Eval(frmdatos.DataItem ,"cod"), Nothing)
If auxBagValues IsNot Nothing Then
     Dim auxValue As Object
auxValue = auxBagValues.gValue_Get("undtipcod.FILTERVALUES")
If auxValue IsNot Nothing Then
 Dim auxValues As List(Of Integer) = auxValue
 CType(CType(sender.parent.FindControl("frmdatos$edittabPanel$edittabPanel001$editctrl000018"), DropDownList).DataSourceObject, SqlDataSource).FilterExpression="cod IN (" & auxClass.Conn.gFieldDB_GetString(auxValues) & ")"
 End If 
End If
auxClass.Conn.gConn_Close()
End Select

Select Case Request.QueryString("_mode_")
 Case "2"
Dim auxClass As New clscusimDOC
auxClass.Conn.gConn_Open()
Dim auxBagValues As clshrcBagValues = auxClass.gSystem_PostAction_v2(10,Val(Request.QueryString("_mode_")),Databinder.Eval(frmdatos.DataItem ,"cod"), Nothing)
If auxBagValues IsNot Nothing Then
     Dim auxValue As Object
auxValue = auxBagValues.gValue_Get("undtipcod.FILTERVALUES")
If auxValue IsNot Nothing Then
 Dim auxValues As List(Of Integer) = auxValue
 CType(CType(sender.parent.FindControl("frmdatos$edittabPanel$edittabPanel001$editctrl000018"), DropDownList).DataSourceObject, SqlDataSource).FilterExpression="cod IN (" & auxClass.Conn.gFieldDB_GetString(auxValues) & ")"
 End If 
End If
auxClass.Conn.gConn_Close()
End Select

Select Case Request.QueryString("_mode_")
 Case "2"
Dim auxClass As New clscusimDOC
auxClass.Conn.gConn_Open()
Dim auxBagValues As clshrcBagValues = auxClass.gSystem_PostAction_v2(10,Val(Request.QueryString("_mode_")),Databinder.Eval(frmdatos.DataItem ,"cod"), Nothing)
If auxBagValues IsNot Nothing Then
     Dim auxValue As Object
auxValue = auxBagValues.gValue_Get("undtipcod.FILTERVALUES")
If auxValue IsNot Nothing Then
 Dim auxValues As List(Of Integer) = auxValue
 CType(CType(sender.parent.FindControl("frmdatos$edittabPanel$edittabPanel001$editctrl000018"), DropDownList).DataSourceObject, SqlDataSource).FilterExpression="cod IN (" & auxClass.Conn.gFieldDB_GetString(auxValues) & ")"
 End If 
End If
auxClass.Conn.gConn_Close()
End Select

Select Case Request.QueryString("_mode_")
 Case "2"
Dim auxClass As New clscusimDOC
auxClass.Conn.gConn_Open()
Dim auxBagValues As clshrcBagValues = auxClass.gSystem_PostAction_v2(10,Val(Request.QueryString("_mode_")),Databinder.Eval(frmdatos.DataItem ,"cod"), Nothing)
If auxBagValues IsNot Nothing Then
     Dim auxValue As Object
auxValue = auxBagValues.gValue_Get("undtipcod.FILTERVALUES")
If auxValue IsNot Nothing Then
 Dim auxValues As List(Of Integer) = auxValue
 CType(CType(sender.parent.FindControl("frmdatos$edittabPanel$edittabPanel001$editctrl000018"), DropDownList).DataSourceObject, SqlDataSource).FilterExpression="cod IN (" & auxClass.Conn.gFieldDB_GetString(auxValues) & ")"
 End If 
End If
auxClass.Conn.gConn_Close()
End Select

Select Case Request.QueryString("_mode_")
 Case "2"
Dim auxClass As New clscusimDOC
auxClass.Conn.gConn_Open()
Dim auxBagValues As clshrcBagValues = auxClass.gSystem_PostAction_v2(10,Val(Request.QueryString("_mode_")),Databinder.Eval(frmdatos.DataItem ,"cod"), Nothing)
If auxBagValues IsNot Nothing Then
     Dim auxValue As Object
auxValue = auxBagValues.gValue_Get("undtipcod.FILTERVALUES")
If auxValue IsNot Nothing Then
 Dim auxValues As List(Of Integer) = auxValue
 CType(CType(sender.parent.FindControl("frmdatos$edittabPanel$edittabPanel001$editctrl000018"), DropDownList).DataSourceObject, SqlDataSource).FilterExpression="cod IN (" & auxClass.Conn.gFieldDB_GetString(auxValues) & ")"
 End If 
End If
auxClass.Conn.gConn_Close()
End Select

Select Case Request.QueryString("_mode_")
 Case "2"
Dim auxClass As New clscusimDOC
auxClass.Conn.gConn_Open()
Dim auxBagValues As clshrcBagValues = auxClass.gSystem_PostAction_v2(10,Val(Request.QueryString("_mode_")),Databinder.Eval(frmdatos.DataItem ,"cod"), Nothing)
If auxBagValues IsNot Nothing Then
     Dim auxValue As Object
auxValue = auxBagValues.gValue_Get("undtipcod.FILTERVALUES")
If auxValue IsNot Nothing Then
 Dim auxValues As List(Of Integer) = auxValue
 CType(CType(sender.parent.FindControl("frmdatos$edittabPanel$edittabPanel001$editctrl000018"), DropDownList).DataSourceObject, SqlDataSource).FilterExpression="cod IN (" & auxClass.Conn.gFieldDB_GetString(auxValues) & ")"
 End If 
End If
auxClass.Conn.gConn_Close()
End Select

Select Case Request.QueryString("_mode_")
 Case "2"
Dim auxClass As New clscusimDOC
auxClass.Conn.gConn_Open()
Dim auxBagValues As clshrcBagValues = auxClass.gSystem_PostAction_v2(10,Val(Request.QueryString("_mode_")),Databinder.Eval(frmdatos.DataItem ,"cod"), Nothing)
If auxBagValues IsNot Nothing Then
     Dim auxValue As Object
auxValue = auxBagValues.gValue_Get("undtipcod.FILTERVALUES")
If auxValue IsNot Nothing Then
 Dim auxValues As List(Of Integer) = auxValue
 CType(CType(sender.parent.FindControl("frmdatos$edittabPanel$edittabPanel001$editctrl000018"), DropDownList).DataSourceObject, SqlDataSource).FilterExpression="cod IN (" & auxClass.Conn.gFieldDB_GetString(auxValues) & ")"
 End If 
End If
auxClass.Conn.gConn_Close()
End Select

Select Case Request.QueryString("_mode_")
 Case "2"
Dim auxClass As New clscusimDOC
auxClass.Conn.gConn_Open()
Dim auxBagValues As clshrcBagValues = auxClass.gSystem_PostAction_v2(10,Val(Request.QueryString("_mode_")),Databinder.Eval(frmdatos.DataItem ,"cod"), Nothing)
If auxBagValues IsNot Nothing Then
     Dim auxValue As Object
auxValue = auxBagValues.gValue_Get("undtipcod.FILTERVALUES")
If auxValue IsNot Nothing Then
 Dim auxValues As List(Of Integer) = auxValue
 CType(CType(sender.parent.FindControl("frmdatos$edittabPanel$edittabPanel001$editctrl000018"), DropDownList).DataSourceObject, SqlDataSource).FilterExpression="cod IN (" & auxClass.Conn.gFieldDB_GetString(auxValues) & ")"
 End If 
End If
auxClass.Conn.gConn_Close()
End Select

Select Case Request.QueryString("_mode_")
 Case "2"
Dim auxClass As New clscusimDOC
auxClass.Conn.gConn_Open()
Dim auxBagValues As clshrcBagValues = auxClass.gSystem_PostAction_v2(10,Val(Request.QueryString("_mode_")),Databinder.Eval(frmdatos.DataItem ,"cod"), Nothing)
If auxBagValues IsNot Nothing Then
     Dim auxValue As Object
auxValue = auxBagValues.gValue_Get("undtipcod.FILTERVALUES")
If auxValue IsNot Nothing Then
 Dim auxValues As List(Of Integer) = auxValue
 CType(CType(sender.parent.FindControl("frmdatos$edittabPanel$edittabPanel001$editctrl000018"), DropDownList).DataSourceObject, SqlDataSource).FilterExpression="cod IN (" & auxClass.Conn.gFieldDB_GetString(auxValues) & ")"
 End If 
End If
auxClass.Conn.gConn_Close()
End Select

Select Case Request.QueryString("_mode_")
 Case "2"
Dim auxClass As New clscusimDOC
auxClass.Conn.gConn_Open()
Dim auxBagValues As clshrcBagValues = auxClass.gSystem_PostAction_v2(10,Val(Request.QueryString("_mode_")),Databinder.Eval(frmdatos.DataItem ,"cod"), Nothing)
If auxBagValues IsNot Nothing Then
     Dim auxValue As Object
auxValue = auxBagValues.gValue_Get("undtipcod.FILTERVALUES")
If auxValue IsNot Nothing Then
 Dim auxValues As List(Of Integer) = auxValue
 CType(CType(sender.parent.FindControl("frmdatos$edittabPanel$edittabPanel001$editctrl000018"), DropDownList).DataSourceObject, SqlDataSource).FilterExpression="cod IN (" & auxClass.Conn.gFieldDB_GetString(auxValues) & ")"
 End If 
End If
auxClass.Conn.gConn_Close()
End Select

Select Case Request.QueryString("_mode_")
 Case "2"
Dim auxClass As New clscusimDOC
auxClass.Conn.gConn_Open()
Dim auxBagValues As clshrcBagValues = auxClass.gSystem_PostAction_v2(10,Val(Request.QueryString("_mode_")),Databinder.Eval(frmdatos.DataItem ,"cod"), Nothing)
If auxBagValues IsNot Nothing Then
     Dim auxValue As Object
auxValue = auxBagValues.gValue_Get("undtipcod.FILTERVALUES")
If auxValue IsNot Nothing Then
 Dim auxValues As List(Of Integer) = auxValue
 CType(CType(sender.parent.FindControl("frmdatos$edittabPanel$edittabPanel001$editctrl000018"), DropDownList).DataSourceObject, SqlDataSource).FilterExpression="cod IN (" & auxClass.Conn.gFieldDB_GetString(auxValues) & ")"
 End If 
End If
auxClass.Conn.gConn_Close()
End Select

Select Case Request.QueryString("_mode_")
 Case "2"
Dim auxClass As New clscusimDOC
auxClass.Conn.gConn_Open()
Dim auxBagValues As clshrcBagValues = auxClass.gSystem_PostAction_v2(10,Val(Request.QueryString("_mode_")),Databinder.Eval(frmdatos.DataItem ,"cod"), Nothing)
If auxBagValues IsNot Nothing Then
     Dim auxValue As Object
auxValue = auxBagValues.gValue_Get("undtipcod.FILTERVALUES")
If auxValue IsNot Nothing Then
 Dim auxValues As List(Of Integer) = auxValue
 CType(CType(sender.parent.FindControl("frmdatos$edittabPanel$edittabPanel001$editctrl000018"), DropDownList).DataSourceObject, SqlDataSource).FilterExpression="cod IN (" & auxClass.Conn.gFieldDB_GetString(auxValues) & ")"
 End If 
End If
auxClass.Conn.gConn_Close()
End Select

Select Case Request.QueryString("_mode_")
 Case "2"
Dim auxClass As New clscusimDOC
auxClass.Conn.gConn_Open()
Dim auxBagValues As clshrcBagValues = auxClass.gSystem_PostAction_v2(10,Val(Request.QueryString("_mode_")),Databinder.Eval(frmdatos.DataItem ,"cod"), Nothing)
If auxBagValues IsNot Nothing Then
     Dim auxValue As Object
auxValue = auxBagValues.gValue_Get("undtipcod.FILTERVALUES")
If auxValue IsNot Nothing Then
 Dim auxValues As List(Of Integer) = auxValue
 CType(CType(sender.parent.FindControl("frmdatos$edittabPanel$edittabPanel001$editctrl000018"), DropDownList).DataSourceObject, SqlDataSource).FilterExpression="cod IN (" & auxClass.Conn.gFieldDB_GetString(auxValues) & ")"
 End If 
End If
auxClass.Conn.gConn_Close()
End Select

Select Case Request.QueryString("_mode_")
 Case "2"
Dim auxClass As New clscusimDOC
auxClass.Conn.gConn_Open()
Dim auxBagValues As clshrcBagValues = auxClass.gSystem_PostAction_v2(10,Val(Request.QueryString("_mode_")),Databinder.Eval(frmdatos.DataItem ,"cod"), Nothing)
If auxBagValues IsNot Nothing Then
     Dim auxValue As Object
auxValue = auxBagValues.gValue_Get("undtipcod.FILTERVALUES")
If auxValue IsNot Nothing Then
 Dim auxValues As List(Of Integer) = auxValue
 CType(CType(sender.parent.FindControl("frmdatos$edittabPanel$edittabPanel001$editctrl000018"), DropDownList).DataSourceObject, SqlDataSource).FilterExpression="cod IN (" & auxClass.Conn.gFieldDB_GetString(auxValues) & ")"
 End If 
End If
auxClass.Conn.gConn_Close()
End Select

Select Case Request.QueryString("_mode_")
 Case "2"
Dim auxClass As New clscusimDOC
auxClass.Conn.gConn_Open()
Dim auxBagValues As clshrcBagValues = auxClass.gSystem_PostAction_v2(10,Val(Request.QueryString("_mode_")),Databinder.Eval(frmdatos.DataItem ,"cod"), Nothing)
If auxBagValues IsNot Nothing Then
     Dim auxValue As Object
auxValue = auxBagValues.gValue_Get("undtipcod.FILTERVALUES")
If auxValue IsNot Nothing Then
 Dim auxValues As List(Of Integer) = auxValue
 CType(CType(sender.parent.FindControl("frmdatos$edittabPanel$edittabPanel001$editctrl000018"), DropDownList).DataSourceObject, SqlDataSource).FilterExpression="cod IN (" & auxClass.Conn.gFieldDB_GetString(auxValues) & ")"
 End If 
End If
auxClass.Conn.gConn_Close()
End Select

Select Case Request.QueryString("_mode_")
 Case "2"
Dim auxClass As New clscusimDOC
auxClass.Conn.gConn_Open()
Dim auxBagValues As clshrcBagValues = auxClass.gSystem_PostAction_v2(10,Val(Request.QueryString("_mode_")),Databinder.Eval(frmdatos.DataItem ,"cod"), Nothing)
If auxBagValues IsNot Nothing Then
     Dim auxValue As Object
auxValue = auxBagValues.gValue_Get("undtipcod.FILTERVALUES")
If auxValue IsNot Nothing Then
 Dim auxValues As List(Of Integer) = auxValue
 CType(CType(sender.parent.FindControl("frmdatos$edittabPanel$edittabPanel001$editctrl000018"), DropDownList).DataSourceObject, SqlDataSource).FilterExpression="cod IN (" & auxClass.Conn.gFieldDB_GetString(auxValues) & ")"
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
         
    Dim auxResult as String=auxClass.gSystem_PostAction(10,12,auxDataKeyCod) 
    auxClass.gCluster_LoadStaticTables_UND(auxDataKeyCod) 
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
    sender.InsertParameters("querynextcod").DefaultValue = e.Command.Parameters("@cod").Value
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
    auxQuery = "SELECT UND.cod,UND.dsc,10 as objecttype FROM UND   WHERE ((UND.undcodsup = {#PARAM1#}) AND (UND.baja = 0 OR UND.baja  IS NULL)) AND UND.cod >= 1"
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
        auxQuery &= "(SELECT cod as cod,dsc as dsc,9 as objecttype,'Colaborador' as objecttypedsc FROM EMP   WHERE (((((EMP.undcod =" & auxSelectedValue & ") AND '" & txtobjectexplorerfilter.Text.Trim & "'='')  OR  (EMP.dsc LIKE '%" & txtobjectexplorerfilter.Text.Trim & "%' AND '" & txtobjectexplorerfilter.Text.Trim & "'<>''))) AND (EMP.baja = 0 OR EMP.baja  IS NULL)) AND EMP.cod >= 1)"
    End If
    If (auxObjectType = "10") And Instr(ViewState("modalpopuppnlobjectexplorer_mode"), "{#CHR34#}10{#CHR34#}") > 0 Then
        If auxQuery <> "" Then auxQuery &= " UNION ALL "
        auxQuery &= "(SELECT cod as cod,dsc as dsc,10 as objecttype,'Unidad' as objecttypedsc FROM UND   WHERE (((((UND.undcodsup =" & auxSelectedValue & ") AND '" & txtobjectexplorerfilter.Text.Trim & "'='')  OR  (UND.dsc LIKE '%" & txtobjectexplorerfilter.Text.Trim & "%' AND '" & txtobjectexplorerfilter.Text.Trim & "'<>''))) AND (UND.baja = 0 OR UND.baja  IS NULL)) AND UND.cod >= 1)"
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
         Response.Redirect(Session("ROOTWEBURL") & "frmUnidades.aspx?_mode_=7&_closea_=0")
     End If

End Sub
Protected Sub frmUnidades_det_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
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

