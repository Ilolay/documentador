Imports System.Data
Imports System.Data.SqlClient
Imports Intelimedia.imComponentes
Public Class frmTiposdedocumentos_det
Inherits System.Web.UI.Page
Friend m_PermFormDelete as Boolean=False
Friend m_PermFormEdit as Boolean=False
Friend m_PermFormNew as Boolean=False
Protected Sub itemctrl000033_Databound(ByVal sender As Object, ByVal e As System.EventArgs)
CType(sender,Label).Text=Eval("cod").ToString

End Sub

Protected Sub itemctrl000031_Databound(ByVal sender As Object, ByVal e As System.EventArgs)
CType(sender,Label).Text=Replace(Eval("dsc").ToString,Chr(10),"<br />")

End Sub

Protected Sub itemctrl000030_Databound(ByVal sender As Object, ByVal e As System.EventArgs)
CType(sender,Label).Text=Eval("orden").ToString

End Sub

Protected Sub itemctrl000029_Databound(ByVal sender As Object, ByVal e As System.EventArgs)
CType(sender,Label).Text=Replace(Eval("abrev").ToString,Chr(10),"<br />")

End Sub

Protected Sub itemctrl000028_Databound(ByVal sender As Object, ByVal e As System.EventArgs)
CType(sender,Label).Text="<img src=imagenes/"
If IsDBNull(Eval("noespecificos")) Then
CType(sender,Label).Text &="actundefined.gif alt=S/D "
ElseIf Eval("noespecificos") Then
CType(sender,Label).Text &="actyes.gif alt=Si "
Else
CType(sender,Label).Text &="actno.gif alt=No "
End If
CType(sender,Label).Text &=" width=12px border=0px />"

End Sub

Protected Sub itemctrl000027_Databound(ByVal sender As Object, ByVal e As System.EventArgs)
CType(sender,Label).Text=Replace(Eval("formato").ToString,Chr(10),"<br />")

End Sub

Protected Sub itemctrl000026_Databound(ByVal sender As Object, ByVal e As System.EventArgs)
CType(sender,Label).Text=Replace(Eval("formatoespecifico").ToString,Chr(10),"<br />")

End Sub

Protected Sub itemctrl000025_Databound(ByVal sender As Object, ByVal e As System.EventArgs)
CType(sender,Label).Text=Replace(Eval("templatehead").ToString,Chr(10),"<br />")

End Sub

Protected Sub itemctrl000024_Databound(ByVal sender As Object, ByVal e As System.EventArgs)
CType(sender,Label).Text=Replace(Eval("templatebody").ToString,Chr(10),"<br />")

End Sub

Protected Sub itemctrl000023_Databound(ByVal sender As Object, ByVal e As System.EventArgs)
CType(sender,Label).Text="<img src=imagenes/"
If IsDBNull(Eval("templatefootcustom")) Then
CType(sender,Label).Text &="actundefined.gif alt=S/D "
ElseIf Eval("templatefootcustom") Then
CType(sender,Label).Text &="actyes.gif alt=Si "
Else
CType(sender,Label).Text &="actno.gif alt=No "
End If
CType(sender,Label).Text &=" width=12px border=0px />"

End Sub

Protected Sub itemctrl000022_Databound(ByVal sender As Object, ByVal e As System.EventArgs)
CType(sender,Label).Text=Replace(Eval("templatefootleft").ToString,Chr(10),"<br />")

End Sub

Protected Sub itemctrl000021_Databound(ByVal sender As Object, ByVal e As System.EventArgs)
CType(sender,Label).Text=Replace(Eval("templatefootcenter").ToString,Chr(10),"<br />")

End Sub

Protected Sub itemctrl000020_Databound(ByVal sender As Object, ByVal e As System.EventArgs)
CType(sender,Label).Text=Replace(Eval("templatefootright").ToString,Chr(10),"<br />")

End Sub

Protected Sub itemctrl000019_Databound(ByVal sender As Object, ByVal e As System.EventArgs)
CType(sender,Label).Text="<img src=imagenes/"
If IsDBNull(Eval("permvigcambiaotros")) Then
CType(sender,Label).Text &="actundefined.gif alt=S/D "
ElseIf Eval("permvigcambiaotros") Then
CType(sender,Label).Text &="actyes.gif alt=Si "
Else
CType(sender,Label).Text &="actno.gif alt=No "
End If
CType(sender,Label).Text &=" width=12px border=0px />"

End Sub

Protected Sub itemctrl000018_Databound(ByVal sender As Object, ByVal e As System.EventArgs)
CType(sender,Label).Text="<img src=imagenes/"
If IsDBNull(Eval("permvigcambiadsc")) Then
CType(sender,Label).Text &="actundefined.gif alt=S/D "
ElseIf Eval("permvigcambiadsc") Then
CType(sender,Label).Text &="actyes.gif alt=Si "
Else
CType(sender,Label).Text &="actno.gif alt=No "
End If
CType(sender,Label).Text &=" width=12px border=0px />"

End Sub

Protected Sub itemctrl000017_Databound(ByVal sender As Object, ByVal e As System.EventArgs)
CType(sender,Label).Text="<img src=imagenes/"
If IsDBNull(Eval("permedicioncambiadsc")) Then
CType(sender,Label).Text &="actundefined.gif alt=S/D "
ElseIf Eval("permedicioncambiadsc") Then
CType(sender,Label).Text &="actyes.gif alt=Si "
Else
CType(sender,Label).Text &="actno.gif alt=No "
End If
CType(sender,Label).Text &=" width=12px border=0px />"

End Sub

Protected Sub itemctrl000016_Databound(ByVal sender As Object, ByVal e As System.EventArgs)
CType(sender,Label).Text="<img src=imagenes/"
If IsDBNull(Eval("permedicioncambiaotros")) Then
CType(sender,Label).Text &="actundefined.gif alt=S/D "
ElseIf Eval("permedicioncambiaotros") Then
CType(sender,Label).Text &="actyes.gif alt=Si "
Else
CType(sender,Label).Text &="actno.gif alt=No "
End If
CType(sender,Label).Text &=" width=12px border=0px />"

End Sub

Protected Sub itemctrl000015_Databound(ByVal sender As Object, ByVal e As System.EventArgs)
CType(sender,Label).Text="<img src=imagenes/"
If IsDBNull(Eval("permedicioncambiaroles")) Then
CType(sender,Label).Text &="actundefined.gif alt=S/D "
ElseIf Eval("permedicioncambiaroles") Then
CType(sender,Label).Text &="actyes.gif alt=Si "
Else
CType(sender,Label).Text &="actno.gif alt=No "
End If
CType(sender,Label).Text &=" width=12px border=0px />"

End Sub

Protected Sub itemctrl000014_Databound(ByVal sender As Object, ByVal e As System.EventArgs)
CType(sender,Label).Text="<img src=imagenes/"
If IsDBNull(Eval("permcambia")) Then
CType(sender,Label).Text &="actundefined.gif alt=S/D "
ElseIf Eval("permcambia") Then
CType(sender,Label).Text &="actyes.gif alt=Si "
Else
CType(sender,Label).Text &="actno.gif alt=No "
End If
CType(sender,Label).Text &=" width=12px border=0px />"

End Sub

Protected Sub itemctrl000013_Databound(ByVal sender As Object, ByVal e As System.EventArgs)
CType(sender,Label).Text="<img src=imagenes/"
If IsDBNull(Eval("opceditor")) Then
CType(sender,Label).Text &="actundefined.gif alt=S/D "
ElseIf Eval("opceditor") Then
CType(sender,Label).Text &="actyes.gif alt=Si "
Else
CType(sender,Label).Text &="actno.gif alt=No "
End If
CType(sender,Label).Text &=" width=12px border=0px />"

End Sub

Protected Sub itemctrl000012_Databound(ByVal sender As Object, ByVal e As System.EventArgs)
CType(sender,Label).Text="<img src=imagenes/"
If IsDBNull(Eval("opcrevisor")) Then
CType(sender,Label).Text &="actundefined.gif alt=S/D "
ElseIf Eval("opcrevisor") Then
CType(sender,Label).Text &="actyes.gif alt=Si "
Else
CType(sender,Label).Text &="actno.gif alt=No "
End If
CType(sender,Label).Text &=" width=12px border=0px />"

End Sub

Protected Sub itemctrl000011_Databound(ByVal sender As Object, ByVal e As System.EventArgs)
CType(sender,Label).Text="<img src=imagenes/"
If IsDBNull(Eval("opcaprobador")) Then
CType(sender,Label).Text &="actundefined.gif alt=S/D "
ElseIf Eval("opcaprobador") Then
CType(sender,Label).Text &="actyes.gif alt=Si "
Else
CType(sender,Label).Text &="actno.gif alt=No "
End If
CType(sender,Label).Text &=" width=12px border=0px />"

End Sub

Protected Sub itemctrl000010_Databound(ByVal sender As Object, ByVal e As System.EventArgs)
CType(sender,Label).Text="<img src=imagenes/"
If IsDBNull(Eval("opccancelador")) Then
CType(sender,Label).Text &="actundefined.gif alt=S/D "
ElseIf Eval("opccancelador") Then
CType(sender,Label).Text &="actyes.gif alt=Si "
Else
CType(sender,Label).Text &="actno.gif alt=No "
End If
CType(sender,Label).Text &=" width=12px border=0px />"

End Sub

Protected Sub itemctrl000009_Databound(ByVal sender As Object, ByVal e As System.EventArgs)
CType(sender,Label).Text="<img src=imagenes/"
If IsDBNull(Eval("opcpublicador")) Then
CType(sender,Label).Text &="actundefined.gif alt=S/D "
ElseIf Eval("opcpublicador") Then
CType(sender,Label).Text &="actyes.gif alt=Si "
Else
CType(sender,Label).Text &="actno.gif alt=No "
End If
CType(sender,Label).Text &=" width=12px border=0px />"

End Sub

Protected Sub itemctrl000008_Databound(ByVal sender As Object, ByVal e As System.EventArgs)
CType(sender,Label).Text="<img src=imagenes/"
If IsDBNull(Eval("opclector")) Then
CType(sender,Label).Text &="actundefined.gif alt=S/D "
ElseIf Eval("opclector") Then
CType(sender,Label).Text &="actyes.gif alt=Si "
Else
CType(sender,Label).Text &="actno.gif alt=No "
End If
CType(sender,Label).Text &=" width=12px border=0px />"

End Sub

Protected Sub itemctrl000007_Databound(ByVal sender As Object, ByVal e As System.EventArgs)
CType(sender,Label).Text="<img src=imagenes/"
If IsDBNull(Eval("reedicion")) Then
CType(sender,Label).Text &="actundefined.gif alt=S/D "
ElseIf Eval("reedicion") Then
CType(sender,Label).Text &="actyes.gif alt=Si "
Else
CType(sender,Label).Text &="actno.gif alt=No "
End If
CType(sender,Label).Text &=" width=12px border=0px />"

End Sub

Protected Sub itemctrl000006_Databound(ByVal sender As Object, ByVal e As System.EventArgs)
If IsDBNull(Eval("autoeditionenabled")) Then
CType(sender,Label).Text &=""
ElseIf Eval("autoeditionenabled") Then
CType(sender,Label).Text &="<img src=imagenes/actyes.gif alt=Si width=12px border=0px />"
Else
CType(sender,Label).Text &=""
End If

End Sub

Protected Sub itemctrl000005_Databound(ByVal sender As Object, ByVal e As System.EventArgs)
CType(sender,Label).Text=Eval("autoeditiondayscicle").ToString

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

Protected Sub editctrl000031_DataBound_edittabPanel001(ByVal sender As Object, ByVal e As System.EventArgs)
CType(sender,TextBox).Text=Eval("dsc").ToString

End Sub

Protected Sub editctrl000030_DataBound_edittabPanel001(ByVal sender As Object, ByVal e As System.EventArgs)
CType(sender,TextBox).Text=Eval("orden").ToString

End Sub

Protected Sub editctrl000029_DataBound_edittabPanel001(ByVal sender As Object, ByVal e As System.EventArgs)
CType(sender,TextBox).Text=Eval("abrev").ToString

End Sub

Protected Sub editctrl000028_Databound(ByVal sender As Object, ByVal e As System.EventArgs)
    Dim auxItem as ListItem=CType(sender,RadioButtonList).Items.FindByValue(DataBinder.Eval(frmdatos.DataItem,"noespecificos").ToString())
    If auxItem IsNot Nothing
       CType(sender,RadioButtonList).ClearSelection()
       auxItem.Selected=True
    End If

End Sub

Protected Sub editctrl000027_DataBound_edittabPanel001(ByVal sender As Object, ByVal e As System.EventArgs)
CType(sender,TextBox).Text=Eval("formato").ToString

End Sub

Protected Sub editctrl000026_DataBound_edittabPanel001(ByVal sender As Object, ByVal e As System.EventArgs)
CType(sender,TextBox).Text=Eval("formatoespecifico").ToString

End Sub

Protected Sub editctrl000025_Databound(ByVal sender As Object, ByVal e As System.EventArgs)
If Not IsPostBack Or CType(sender,HiddenField).Value ="" Then
    CType(sender,HiddenField).Value = Guid.NewGuid.ToString.Replace("-","")
    CType(sender.parent.FindControl("fmeeditctrl000025"),HtmlControl).Attributes("src") = "hrctexteditor.aspx?upload=2&tmp=1&tmpid=" & CType(sender,HiddenField).Value
    Dim auxConn As New imClientConnection
    auxConn.gTextTmp_Upload(Eval("templatehead").ToString, CType(sender, HiddenField).Value)
End If

End Sub

Protected Sub editctrl000024_Databound(ByVal sender As Object, ByVal e As System.EventArgs)
If Not IsPostBack Or CType(sender,HiddenField).Value ="" Then
    CType(sender,HiddenField).Value = Guid.NewGuid.ToString.Replace("-","")
    CType(sender.parent.FindControl("fmeeditctrl000024"),HtmlControl).Attributes("src") = "hrctexteditor.aspx?upload=2&tmp=1&tmpid=" & CType(sender,HiddenField).Value
    Dim auxConn As New imClientConnection
    auxConn.gTextTmp_Upload(Eval("templatebody").ToString, CType(sender, HiddenField).Value)
End If

End Sub

Protected Sub editctrl000023_Databound(ByVal sender As Object, ByVal e As System.EventArgs)
    If IsDBNull(DataBinder.Eval(frmdatos.DataItem,"templatefootcustom"))  Then
        CType(sender,CheckBox).Checked=False
    Else
        CType(sender,CheckBox).Checked=DataBinder.Eval(frmdatos.DataItem,"templatefootcustom")
    End If

End Sub

Protected Sub editctrl000022_DataBound_edittabPanel001(ByVal sender As Object, ByVal e As System.EventArgs)
CType(sender,TextBox).Text=Eval("templatefootleft").ToString

End Sub

Protected Sub editctrl000021_DataBound_edittabPanel001(ByVal sender As Object, ByVal e As System.EventArgs)
CType(sender,TextBox).Text=Eval("templatefootcenter").ToString

End Sub

Protected Sub editctrl000020_DataBound_edittabPanel001(ByVal sender As Object, ByVal e As System.EventArgs)
CType(sender,TextBox).Text=Eval("templatefootright").ToString

End Sub

Protected Sub editctrl000019_Databound(ByVal sender As Object, ByVal e As System.EventArgs)
    If IsDBNull(DataBinder.Eval(frmdatos.DataItem,"permvigcambiaotros"))  Then
        CType(sender,CheckBox).Checked=False
    Else
        CType(sender,CheckBox).Checked=DataBinder.Eval(frmdatos.DataItem,"permvigcambiaotros")
    End If

End Sub

Protected Sub editctrl000018_Databound(ByVal sender As Object, ByVal e As System.EventArgs)
    If IsDBNull(DataBinder.Eval(frmdatos.DataItem,"permvigcambiadsc"))  Then
        CType(sender,CheckBox).Checked=False
    Else
        CType(sender,CheckBox).Checked=DataBinder.Eval(frmdatos.DataItem,"permvigcambiadsc")
    End If

End Sub

Protected Sub editctrl000017_Databound(ByVal sender As Object, ByVal e As System.EventArgs)
    If IsDBNull(DataBinder.Eval(frmdatos.DataItem,"permedicioncambiadsc"))  Then
        CType(sender,CheckBox).Checked=False
    Else
        CType(sender,CheckBox).Checked=DataBinder.Eval(frmdatos.DataItem,"permedicioncambiadsc")
    End If

End Sub

Protected Sub editctrl000016_Databound(ByVal sender As Object, ByVal e As System.EventArgs)
    If IsDBNull(DataBinder.Eval(frmdatos.DataItem,"permedicioncambiaotros"))  Then
        CType(sender,CheckBox).Checked=False
    Else
        CType(sender,CheckBox).Checked=DataBinder.Eval(frmdatos.DataItem,"permedicioncambiaotros")
    End If

End Sub

Protected Sub editctrl000015_Databound(ByVal sender As Object, ByVal e As System.EventArgs)
    If IsDBNull(DataBinder.Eval(frmdatos.DataItem,"permedicioncambiaroles"))  Then
        CType(sender,CheckBox).Checked=False
    Else
        CType(sender,CheckBox).Checked=DataBinder.Eval(frmdatos.DataItem,"permedicioncambiaroles")
    End If

End Sub

Protected Sub editctrl000014_Databound(ByVal sender As Object, ByVal e As System.EventArgs)
    If IsDBNull(DataBinder.Eval(frmdatos.DataItem,"permcambia"))  Then
        CType(sender,CheckBox).Checked=False
    Else
        CType(sender,CheckBox).Checked=DataBinder.Eval(frmdatos.DataItem,"permcambia")
    End If

End Sub

Protected Sub editctrl000013_Databound(ByVal sender As Object, ByVal e As System.EventArgs)
    If IsDBNull(DataBinder.Eval(frmdatos.DataItem,"opceditor"))  Then
        CType(sender,CheckBox).Checked=False
    Else
        CType(sender,CheckBox).Checked=DataBinder.Eval(frmdatos.DataItem,"opceditor")
    End If

End Sub

Protected Sub editctrl000012_Databound(ByVal sender As Object, ByVal e As System.EventArgs)
    If IsDBNull(DataBinder.Eval(frmdatos.DataItem,"opcrevisor"))  Then
        CType(sender,CheckBox).Checked=False
    Else
        CType(sender,CheckBox).Checked=DataBinder.Eval(frmdatos.DataItem,"opcrevisor")
    End If

End Sub

Protected Sub editctrl000011_Databound(ByVal sender As Object, ByVal e As System.EventArgs)
    If IsDBNull(DataBinder.Eval(frmdatos.DataItem,"opcaprobador"))  Then
        CType(sender,CheckBox).Checked=False
    Else
        CType(sender,CheckBox).Checked=DataBinder.Eval(frmdatos.DataItem,"opcaprobador")
    End If

End Sub

Protected Sub editctrl000010_Databound(ByVal sender As Object, ByVal e As System.EventArgs)
    If IsDBNull(DataBinder.Eval(frmdatos.DataItem,"opccancelador"))  Then
        CType(sender,CheckBox).Checked=False
    Else
        CType(sender,CheckBox).Checked=DataBinder.Eval(frmdatos.DataItem,"opccancelador")
    End If

End Sub

Protected Sub editctrl000009_Databound(ByVal sender As Object, ByVal e As System.EventArgs)
    If IsDBNull(DataBinder.Eval(frmdatos.DataItem,"opcpublicador"))  Then
        CType(sender,CheckBox).Checked=False
    Else
        CType(sender,CheckBox).Checked=DataBinder.Eval(frmdatos.DataItem,"opcpublicador")
    End If

End Sub

Protected Sub editctrl000008_Databound(ByVal sender As Object, ByVal e As System.EventArgs)
    If IsDBNull(DataBinder.Eval(frmdatos.DataItem,"opclector"))  Then
        CType(sender,CheckBox).Checked=False
    Else
        CType(sender,CheckBox).Checked=DataBinder.Eval(frmdatos.DataItem,"opclector")
    End If

End Sub

Protected Sub editctrl000007_Databound(ByVal sender As Object, ByVal e As System.EventArgs)
    If IsDBNull(DataBinder.Eval(frmdatos.DataItem,"reedicion"))  Then
        CType(sender,CheckBox).Checked=False
    Else
        CType(sender,CheckBox).Checked=DataBinder.Eval(frmdatos.DataItem,"reedicion")
    End If

End Sub

Protected Sub editctrl000006_Databound(ByVal sender As Object, ByVal e As System.EventArgs)
    Dim auxItem as ListItem=CType(sender,RadioButtonList).Items.FindByValue(DataBinder.Eval(frmdatos.DataItem,"autoeditionenabled").ToString())
    If auxItem IsNot Nothing
       CType(sender,RadioButtonList).ClearSelection()
       auxItem.Selected=True
    End If

End Sub

Protected Sub editctrl000005_DataBound_edittabPanel004(ByVal sender As Object, ByVal e As System.EventArgs)
CType(sender,TextBox).Text=Eval("autoeditiondayscicle").ToString

End Sub

Protected Sub editctrl000004_Databound(ByVal sender As Object, ByVal e As System.EventArgs)
If Not IsPostBack Or CType(sender,HiddenField).Value ="" Then
    CType(sender,HiddenField).Value = HttpUtility.UrlEncode(Guid.NewGuid.ToString.Replace("-","") & "cod")
    CType(sender.parent.FindControl("fmeeditctrl000004"),HtmlControl).Attributes("src") = "hrcbinaries.aspx?upload=1&tmp=1&id=" & Eval("logo") & "&tmpid=" & CType(sender,HiddenField).Value & "&isimage=1"
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

Protected Sub insctrl000025_Databound(ByVal sender As Object, ByVal e As System.EventArgs)
If Not IsPostBack Or CType(sender,HiddenField).Value ="" Then
    CType(sender,HiddenField).Value = Guid.NewGuid.ToString.Replace("-","")
    CType(sender.parent.FindControl("fmeinsctrl000025"),HtmlControl).Attributes("src") = "hrctexteditor.aspx?upload=2&tmp=1&tmpid=" & CType(sender,HiddenField).Value
    Dim auxConn As New imClientConnection
    auxConn.gTextTmp_Upload("", CType(sender, HiddenField).Value)
End If

End Sub

Protected Sub insctrl000024_Databound(ByVal sender As Object, ByVal e As System.EventArgs)
If Not IsPostBack Or CType(sender,HiddenField).Value ="" Then
    CType(sender,HiddenField).Value = Guid.NewGuid.ToString.Replace("-","")
    CType(sender.parent.FindControl("fmeinsctrl000024"),HtmlControl).Attributes("src") = "hrctexteditor.aspx?upload=2&tmp=1&tmpid=" & CType(sender,HiddenField).Value
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
         Response.Redirect(Session("ROOTWEBURL") & "frmTiposdedocumentos.aspx?_mode_=7&_closea_=0")
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
    Dim auxResult as String=auxClass.gSystem_PostAction(4,13,auxDataKeyCod) 
    auxClass.gCluster_LoadStaticTables_DOC_DOCTIP(auxDataKeyCod) 
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
    
    Dim auxResult as String=auxClass.gSystem_PostAction(4,11,auxDataKeyCod) 
    auxClass.gCluster_LoadStaticTables_DOC_DOCTIP(auxDataKeyCod) 
    lblerror.Text &=auxResult
    If auxResult ="" Then
    End If
    auxClass.Conn.gConn_Close
End If

End Sub
Protected Sub frmdatos_ItemInserting(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.FormViewInsertEventArgs)
CType(CType(sender,FormView).DataSourceObject,SqlDataSource).InsertParameters("templatehead").DefaultValue = (New Intelimedia.imComponentes.imClientConnection).gTextTmp_Download(Ctype(frmdatos.FindControl("instabPanel").FindControl("instabPanel001").FindControl("insctrl000025"), HiddenField).Value)

CType(CType(sender,FormView).DataSourceObject,SqlDataSource).InsertParameters("templatebody").DefaultValue = (New Intelimedia.imComponentes.imClientConnection).gTextTmp_Download(Ctype(frmdatos.FindControl("instabPanel").FindControl("instabPanel001").FindControl("insctrl000024"), HiddenField).Value)

End Sub
Protected Sub frmdatos_ItemSelected(ByVal sender As Object, ByVal e As System.EventArgs)
If frmdatos.DataItem IsNot Nothing Then
   Page.Title = "Tipos de documentos| " &  Databinder.Eval(frmdatos.DataItem ,"dsc").ToString
   lblsubtitle.Text = Databinder.Eval(frmdatos.DataItem ,"dsc").ToString
Else
   Page.Title = "Tipos de documentos| Nuevo"
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
     Dim auxMax As Integer = Val(LicParam.gValue_Get("DOC_DOCTIP.MAX", -1))
     If auxMax >= 0 Then
         Dim auxConn As clsHrcConnClient = Session("conn")
         auxConn.gComponent_CreateInstance()
         auxConn.gConn_Open()
         If Val(auxConn.gConn_QueryValueInt("SELECT COUNT(*) FROM DOC_DOCTIP", 0)) > auxMax Then
             gForm_Close()
         End If
         auxConn.gConn_Close()
     End If
 End If
End Select
Select Case Request.QueryString("_mode_")
 Case "1"
             CType(sender.parent.FindControl("frmdatos$instabPanel$instabPanel001$insctrl000028"),RadioButtonList).Items.FindByValue(False).Selected=True
 Case "25"
   lblsubtitle.Text = "Copiar"
    Try
        Dim auxConn As New SqlConnection(Session("connectionstringname"))
        auxConn.Open
        Dim auxDA As New SqlDataAdapter("SELECT DOC_DOCTIP.cod,DOC_DOCTIP.dsc,DOC_DOCTIP.orden,DOC_DOCTIP.abrev,DOC_DOCTIP.noespecificos,DOC_DOCTIP.formato,DOC_DOCTIP.formatoespecifico,DOC_DOCTIP.templatehead,DOC_DOCTIP.templatebody,DOC_DOCTIP.templatefootcustom,DOC_DOCTIP.templatefootleft,DOC_DOCTIP.templatefootcenter,DOC_DOCTIP.templatefootright,DOC_DOCTIP.permedicioncambiadsc,DOC_DOCTIP.permedicioncambiaotros,DOC_DOCTIP.permedicioncambiaroles,DOC_DOCTIP.permcambia,DOC_DOCTIP.opceditor,DOC_DOCTIP.opcrevisor,DOC_DOCTIP.opcaprobador,DOC_DOCTIP.opccancelador,DOC_DOCTIP.opcpublicador,DOC_DOCTIP.opclector,DOC_DOCTIP.reedicion,DOC_DOCTIP.baja,DOC_DOCTIP.permvigcambiaotros,DOC_DOCTIP.permvigcambiadsc,DOC_DOCTIP.autoeditionenabled,DOC_DOCTIP.autoeditiondayscicle,(DOC_DOCTIPLOGO.dsc) as logodsc,(DOC_DOCTIPQSECSID.secdsc) as qsecsiddsc,DOC_DOCTIP.qsecdatetime,DOC_DOCTIP.logo,DOC_DOCTIP.qsecsid FROM DOC_DOCTIP  LEFT JOIN CONBINARIES AS DOC_DOCTIPLOGO ON DOC_DOCTIPLOGO.cod=DOC_DOCTIP.logo LEFT JOIN Q_SECPLOGIN AS DOC_DOCTIPQSECSID ON DOC_DOCTIPQSECSID.sidcod=DOC_DOCTIP.qsecsid  WHERE (DOC_DOCTIP.cod=@param1) AND (DOC_DOCTIP.baja = 0 OR DOC_DOCTIP.baja  IS NULL)", auxConn)
        auxDA.SelectCommand.Parameters.Add("@param1", SqlDbType.Int, 4).Value = Request.QueryString("param1")
        Dim auxDT As New DataTable
        auxDA.Fill(auxDT)
        auxConn.Close
        If auxDT.Rows.Count <> 0 Then
             If IsDBNull(auxDT.Rows(0)("dsc")) = False Then
             CType(sender.parent.FindControl("frmdatos$instabPanel$instabPanel001$insctrl000031"),TextBox).Text=auxDT.Rows(0)("dsc")
             End If 
             If IsDBNull(auxDT.Rows(0)("orden")) = False Then
             CType(sender.parent.FindControl("frmdatos$instabPanel$instabPanel001$insctrl000030"),TextBox).Text=auxDT.Rows(0)("orden")
             End If 
             If IsDBNull(auxDT.Rows(0)("abrev")) = False Then
             CType(sender.parent.FindControl("frmdatos$instabPanel$instabPanel001$insctrl000029"),TextBox).Text=auxDT.Rows(0)("abrev")
             End If 
             If IsDBNull(auxDT.Rows(0)("noespecificos")) = False Then
             CType(sender.parent.FindControl("frmdatos$instabPanel$instabPanel001$insctrl000028"),RadioButtonList).Items.FindByValue(auxDT.Rows(0)("noespecificos")).Selected=True
             End If 
             If IsDBNull(auxDT.Rows(0)("formato")) = False Then
             CType(sender.parent.FindControl("frmdatos$instabPanel$instabPanel001$insctrl000027"),TextBox).Text=auxDT.Rows(0)("formato")
             End If 
             If IsDBNull(auxDT.Rows(0)("formatoespecifico")) = False Then
             CType(sender.parent.FindControl("frmdatos$instabPanel$instabPanel001$insctrl000026"),TextBox).Text=auxDT.Rows(0)("formatoespecifico")
             End If 
             If IsDBNull(auxDT.Rows(0)("templatehead")) = False Then
             CType(sender.parent.FindControl("frmdatos$instabPanel$instabPanel001$insctrl000025"),TextBox).Text=auxDT.Rows(0)("templatehead")
             End If 
             If IsDBNull(auxDT.Rows(0)("templatebody")) = False Then
             CType(sender.parent.FindControl("frmdatos$instabPanel$instabPanel001$insctrl000024"),TextBox).Text=auxDT.Rows(0)("templatebody")
             End If 
             If IsDBNull(auxDT.Rows(0)("templatefootcustom")) = False Then
             CType(sender.parent.FindControl("frmdatos$instabPanel$instabPanel001$insctrl000023"),CheckBox).Checked=auxDT.Rows(0)("templatefootcustom")
             End If 
             If IsDBNull(auxDT.Rows(0)("templatefootleft")) = False Then
             CType(sender.parent.FindControl("frmdatos$instabPanel$instabPanel001$insctrl000022"),TextBox).Text=auxDT.Rows(0)("templatefootleft")
             End If 
             If IsDBNull(auxDT.Rows(0)("templatefootcenter")) = False Then
             CType(sender.parent.FindControl("frmdatos$instabPanel$instabPanel001$insctrl000021"),TextBox).Text=auxDT.Rows(0)("templatefootcenter")
             End If 
             If IsDBNull(auxDT.Rows(0)("templatefootright")) = False Then
             CType(sender.parent.FindControl("frmdatos$instabPanel$instabPanel001$insctrl000020"),TextBox).Text=auxDT.Rows(0)("templatefootright")
             End If 
             If IsDBNull(auxDT.Rows(0)("permvigcambiaotros")) = False Then
             CType(sender.parent.FindControl("frmdatos$instabPanel$instabPanel002$insctrl000019"),CheckBox).Checked=auxDT.Rows(0)("permvigcambiaotros")
             End If 
             If IsDBNull(auxDT.Rows(0)("permvigcambiadsc")) = False Then
             CType(sender.parent.FindControl("frmdatos$instabPanel$instabPanel002$insctrl000018"),CheckBox).Checked=auxDT.Rows(0)("permvigcambiadsc")
             End If 
             If IsDBNull(auxDT.Rows(0)("permedicioncambiadsc")) = False Then
             CType(sender.parent.FindControl("frmdatos$instabPanel$instabPanel003$insctrl000017"),CheckBox).Checked=auxDT.Rows(0)("permedicioncambiadsc")
             End If 
             If IsDBNull(auxDT.Rows(0)("permedicioncambiaotros")) = False Then
             CType(sender.parent.FindControl("frmdatos$instabPanel$instabPanel003$insctrl000016"),CheckBox).Checked=auxDT.Rows(0)("permedicioncambiaotros")
             End If 
             If IsDBNull(auxDT.Rows(0)("permedicioncambiaroles")) = False Then
             CType(sender.parent.FindControl("frmdatos$instabPanel$instabPanel003$insctrl000015"),CheckBox).Checked=auxDT.Rows(0)("permedicioncambiaroles")
             End If 
             If IsDBNull(auxDT.Rows(0)("permcambia")) = False Then
             CType(sender.parent.FindControl("frmdatos$instabPanel$instabPanel004$insctrl000014"),CheckBox).Checked=auxDT.Rows(0)("permcambia")
             End If 
             If IsDBNull(auxDT.Rows(0)("opceditor")) = False Then
             CType(sender.parent.FindControl("frmdatos$instabPanel$instabPanel004$insctrl000013"),CheckBox).Checked=auxDT.Rows(0)("opceditor")
             End If 
             If IsDBNull(auxDT.Rows(0)("opcrevisor")) = False Then
             CType(sender.parent.FindControl("frmdatos$instabPanel$instabPanel004$insctrl000012"),CheckBox).Checked=auxDT.Rows(0)("opcrevisor")
             End If 
             If IsDBNull(auxDT.Rows(0)("opcaprobador")) = False Then
             CType(sender.parent.FindControl("frmdatos$instabPanel$instabPanel004$insctrl000011"),CheckBox).Checked=auxDT.Rows(0)("opcaprobador")
             End If 
             If IsDBNull(auxDT.Rows(0)("opccancelador")) = False Then
             CType(sender.parent.FindControl("frmdatos$instabPanel$instabPanel004$insctrl000010"),CheckBox).Checked=auxDT.Rows(0)("opccancelador")
             End If 
             If IsDBNull(auxDT.Rows(0)("opcpublicador")) = False Then
             CType(sender.parent.FindControl("frmdatos$instabPanel$instabPanel004$insctrl000009"),CheckBox).Checked=auxDT.Rows(0)("opcpublicador")
             End If 
             If IsDBNull(auxDT.Rows(0)("opclector")) = False Then
             CType(sender.parent.FindControl("frmdatos$instabPanel$instabPanel004$insctrl000008"),CheckBox).Checked=auxDT.Rows(0)("opclector")
             End If 
             If IsDBNull(auxDT.Rows(0)("reedicion")) = False Then
             CType(sender.parent.FindControl("frmdatos$instabPanel$instabPanel004$insctrl000007"),CheckBox).Checked=auxDT.Rows(0)("reedicion")
             End If 
             If IsDBNull(auxDT.Rows(0)("autoeditionenabled")) = False Then
             CType(sender.parent.FindControl("frmdatos$instabPanel$instabPanel004$insctrl000006"),RadioButtonList).Items.FindByValue(auxDT.Rows(0)("autoeditionenabled")).Selected=True
             End If 
             If IsDBNull(auxDT.Rows(0)("autoeditiondayscicle")) = False Then
             CType(sender.parent.FindControl("frmdatos$instabPanel$instabPanel004$insctrl000005"),TextBox).Text=auxDT.Rows(0)("autoeditiondayscicle")
             End If 
        End If
    Catch ex as sqlException
    End Try
End Select
Select Case Request.QueryString("_mode_")
 Case "1","25"
Dim auxClass As New clscusimDOC
auxClass.Conn.gConn_Open()
Dim auxBagValues As clshrcBagValues = auxClass.gSystem_PostAction_v2(4,Val(Request.QueryString("_mode_")), -1, Nothing)
If auxBagValues IsNot Nothing Then
     Dim auxValue As Object
End If
auxClass.Conn.gConn_Close()
End Select

Select Case Request.QueryString("_mode_")
 Case "2"
Dim auxClass As New clscusimDOC
auxClass.Conn.gConn_Open()
Dim auxBagValues As clshrcBagValues = auxClass.gSystem_PostAction_v2(4,Val(Request.QueryString("_mode_")),Databinder.Eval(frmdatos.DataItem ,"cod"), Nothing)
If auxBagValues IsNot Nothing Then
     Dim auxValue As Object
End If
auxClass.Conn.gConn_Close()
End Select

Select Case Request.QueryString("_mode_")
 Case "2"
Dim auxClass As New clscusimDOC
auxClass.Conn.gConn_Open()
Dim auxBagValues As clshrcBagValues = auxClass.gSystem_PostAction_v2(4,Val(Request.QueryString("_mode_")),Databinder.Eval(frmdatos.DataItem ,"cod"), Nothing)
If auxBagValues IsNot Nothing Then
     Dim auxValue As Object
End If
auxClass.Conn.gConn_Close()
End Select

Select Case Request.QueryString("_mode_")
 Case "2"
Dim auxClass As New clscusimDOC
auxClass.Conn.gConn_Open()
Dim auxBagValues As clshrcBagValues = auxClass.gSystem_PostAction_v2(4,Val(Request.QueryString("_mode_")),Databinder.Eval(frmdatos.DataItem ,"cod"), Nothing)
If auxBagValues IsNot Nothing Then
     Dim auxValue As Object
End If
auxClass.Conn.gConn_Close()
End Select

Select Case Request.QueryString("_mode_")
 Case "2"
Dim auxClass As New clscusimDOC
auxClass.Conn.gConn_Open()
Dim auxBagValues As clshrcBagValues = auxClass.gSystem_PostAction_v2(4,Val(Request.QueryString("_mode_")),Databinder.Eval(frmdatos.DataItem ,"cod"), Nothing)
If auxBagValues IsNot Nothing Then
     Dim auxValue As Object
End If
auxClass.Conn.gConn_Close()
End Select

Select Case Request.QueryString("_mode_")
 Case "2"
Dim auxClass As New clscusimDOC
auxClass.Conn.gConn_Open()
Dim auxBagValues As clshrcBagValues = auxClass.gSystem_PostAction_v2(4,Val(Request.QueryString("_mode_")),Databinder.Eval(frmdatos.DataItem ,"cod"), Nothing)
If auxBagValues IsNot Nothing Then
     Dim auxValue As Object
End If
auxClass.Conn.gConn_Close()
End Select

Select Case Request.QueryString("_mode_")
 Case "2"
Dim auxClass As New clscusimDOC
auxClass.Conn.gConn_Open()
Dim auxBagValues As clshrcBagValues = auxClass.gSystem_PostAction_v2(4,Val(Request.QueryString("_mode_")),Databinder.Eval(frmdatos.DataItem ,"cod"), Nothing)
If auxBagValues IsNot Nothing Then
     Dim auxValue As Object
End If
auxClass.Conn.gConn_Close()
End Select

Select Case Request.QueryString("_mode_")
 Case "2"
Dim auxClass As New clscusimDOC
auxClass.Conn.gConn_Open()
Dim auxBagValues As clshrcBagValues = auxClass.gSystem_PostAction_v2(4,Val(Request.QueryString("_mode_")),Databinder.Eval(frmdatos.DataItem ,"cod"), Nothing)
If auxBagValues IsNot Nothing Then
     Dim auxValue As Object
End If
auxClass.Conn.gConn_Close()
End Select

Select Case Request.QueryString("_mode_")
 Case "2"
Dim auxClass As New clscusimDOC
auxClass.Conn.gConn_Open()
Dim auxBagValues As clshrcBagValues = auxClass.gSystem_PostAction_v2(4,Val(Request.QueryString("_mode_")),Databinder.Eval(frmdatos.DataItem ,"cod"), Nothing)
If auxBagValues IsNot Nothing Then
     Dim auxValue As Object
End If
auxClass.Conn.gConn_Close()
End Select

Select Case Request.QueryString("_mode_")
 Case "2"
Dim auxClass As New clscusimDOC
auxClass.Conn.gConn_Open()
Dim auxBagValues As clshrcBagValues = auxClass.gSystem_PostAction_v2(4,Val(Request.QueryString("_mode_")),Databinder.Eval(frmdatos.DataItem ,"cod"), Nothing)
If auxBagValues IsNot Nothing Then
     Dim auxValue As Object
End If
auxClass.Conn.gConn_Close()
End Select

Select Case Request.QueryString("_mode_")
 Case "2"
Dim auxClass As New clscusimDOC
auxClass.Conn.gConn_Open()
Dim auxBagValues As clshrcBagValues = auxClass.gSystem_PostAction_v2(4,Val(Request.QueryString("_mode_")),Databinder.Eval(frmdatos.DataItem ,"cod"), Nothing)
If auxBagValues IsNot Nothing Then
     Dim auxValue As Object
End If
auxClass.Conn.gConn_Close()
End Select

Select Case Request.QueryString("_mode_")
 Case "2"
Dim auxClass As New clscusimDOC
auxClass.Conn.gConn_Open()
Dim auxBagValues As clshrcBagValues = auxClass.gSystem_PostAction_v2(4,Val(Request.QueryString("_mode_")),Databinder.Eval(frmdatos.DataItem ,"cod"), Nothing)
If auxBagValues IsNot Nothing Then
     Dim auxValue As Object
End If
auxClass.Conn.gConn_Close()
End Select

Select Case Request.QueryString("_mode_")
 Case "2"
Dim auxClass As New clscusimDOC
auxClass.Conn.gConn_Open()
Dim auxBagValues As clshrcBagValues = auxClass.gSystem_PostAction_v2(4,Val(Request.QueryString("_mode_")),Databinder.Eval(frmdatos.DataItem ,"cod"), Nothing)
If auxBagValues IsNot Nothing Then
     Dim auxValue As Object
End If
auxClass.Conn.gConn_Close()
End Select

Select Case Request.QueryString("_mode_")
 Case "2"
Dim auxClass As New clscusimDOC
auxClass.Conn.gConn_Open()
Dim auxBagValues As clshrcBagValues = auxClass.gSystem_PostAction_v2(4,Val(Request.QueryString("_mode_")),Databinder.Eval(frmdatos.DataItem ,"cod"), Nothing)
If auxBagValues IsNot Nothing Then
     Dim auxValue As Object
End If
auxClass.Conn.gConn_Close()
End Select

Select Case Request.QueryString("_mode_")
 Case "2"
Dim auxClass As New clscusimDOC
auxClass.Conn.gConn_Open()
Dim auxBagValues As clshrcBagValues = auxClass.gSystem_PostAction_v2(4,Val(Request.QueryString("_mode_")),Databinder.Eval(frmdatos.DataItem ,"cod"), Nothing)
If auxBagValues IsNot Nothing Then
     Dim auxValue As Object
End If
auxClass.Conn.gConn_Close()
End Select

Select Case Request.QueryString("_mode_")
 Case "2"
Dim auxClass As New clscusimDOC
auxClass.Conn.gConn_Open()
Dim auxBagValues As clshrcBagValues = auxClass.gSystem_PostAction_v2(4,Val(Request.QueryString("_mode_")),Databinder.Eval(frmdatos.DataItem ,"cod"), Nothing)
If auxBagValues IsNot Nothing Then
     Dim auxValue As Object
End If
auxClass.Conn.gConn_Close()
End Select

Select Case Request.QueryString("_mode_")
 Case "2"
Dim auxClass As New clscusimDOC
auxClass.Conn.gConn_Open()
Dim auxBagValues As clshrcBagValues = auxClass.gSystem_PostAction_v2(4,Val(Request.QueryString("_mode_")),Databinder.Eval(frmdatos.DataItem ,"cod"), Nothing)
If auxBagValues IsNot Nothing Then
     Dim auxValue As Object
End If
auxClass.Conn.gConn_Close()
End Select

Select Case Request.QueryString("_mode_")
 Case "2"
Dim auxClass As New clscusimDOC
auxClass.Conn.gConn_Open()
Dim auxBagValues As clshrcBagValues = auxClass.gSystem_PostAction_v2(4,Val(Request.QueryString("_mode_")),Databinder.Eval(frmdatos.DataItem ,"cod"), Nothing)
If auxBagValues IsNot Nothing Then
     Dim auxValue As Object
End If
auxClass.Conn.gConn_Close()
End Select

Select Case Request.QueryString("_mode_")
 Case "2"
Dim auxClass As New clscusimDOC
auxClass.Conn.gConn_Open()
Dim auxBagValues As clshrcBagValues = auxClass.gSystem_PostAction_v2(4,Val(Request.QueryString("_mode_")),Databinder.Eval(frmdatos.DataItem ,"cod"), Nothing)
If auxBagValues IsNot Nothing Then
     Dim auxValue As Object
End If
auxClass.Conn.gConn_Close()
End Select

Select Case Request.QueryString("_mode_")
 Case "2"
Dim auxClass As New clscusimDOC
auxClass.Conn.gConn_Open()
Dim auxBagValues As clshrcBagValues = auxClass.gSystem_PostAction_v2(4,Val(Request.QueryString("_mode_")),Databinder.Eval(frmdatos.DataItem ,"cod"), Nothing)
If auxBagValues IsNot Nothing Then
     Dim auxValue As Object
End If
auxClass.Conn.gConn_Close()
End Select

Select Case Request.QueryString("_mode_")
 Case "2"
Dim auxClass As New clscusimDOC
auxClass.Conn.gConn_Open()
Dim auxBagValues As clshrcBagValues = auxClass.gSystem_PostAction_v2(4,Val(Request.QueryString("_mode_")),Databinder.Eval(frmdatos.DataItem ,"cod"), Nothing)
If auxBagValues IsNot Nothing Then
     Dim auxValue As Object
End If
auxClass.Conn.gConn_Close()
End Select

Select Case Request.QueryString("_mode_")
 Case "2"
Dim auxClass As New clscusimDOC
auxClass.Conn.gConn_Open()
Dim auxBagValues As clshrcBagValues = auxClass.gSystem_PostAction_v2(4,Val(Request.QueryString("_mode_")),Databinder.Eval(frmdatos.DataItem ,"cod"), Nothing)
If auxBagValues IsNot Nothing Then
     Dim auxValue As Object
End If
auxClass.Conn.gConn_Close()
End Select

Select Case Request.QueryString("_mode_")
 Case "2"
Dim auxClass As New clscusimDOC
auxClass.Conn.gConn_Open()
Dim auxBagValues As clshrcBagValues = auxClass.gSystem_PostAction_v2(4,Val(Request.QueryString("_mode_")),Databinder.Eval(frmdatos.DataItem ,"cod"), Nothing)
If auxBagValues IsNot Nothing Then
     Dim auxValue As Object
End If
auxClass.Conn.gConn_Close()
End Select

Select Case Request.QueryString("_mode_")
 Case "2"
Dim auxClass As New clscusimDOC
auxClass.Conn.gConn_Open()
Dim auxBagValues As clshrcBagValues = auxClass.gSystem_PostAction_v2(4,Val(Request.QueryString("_mode_")),Databinder.Eval(frmdatos.DataItem ,"cod"), Nothing)
If auxBagValues IsNot Nothing Then
     Dim auxValue As Object
End If
auxClass.Conn.gConn_Close()
End Select

Select Case Request.QueryString("_mode_")
 Case "2"
Dim auxClass As New clscusimDOC
auxClass.Conn.gConn_Open()
Dim auxBagValues As clshrcBagValues = auxClass.gSystem_PostAction_v2(4,Val(Request.QueryString("_mode_")),Databinder.Eval(frmdatos.DataItem ,"cod"), Nothing)
If auxBagValues IsNot Nothing Then
     Dim auxValue As Object
End If
auxClass.Conn.gConn_Close()
End Select

Select Case Request.QueryString("_mode_")
 Case "2"
Dim auxClass As New clscusimDOC
auxClass.Conn.gConn_Open()
Dim auxBagValues As clshrcBagValues = auxClass.gSystem_PostAction_v2(4,Val(Request.QueryString("_mode_")),Databinder.Eval(frmdatos.DataItem ,"cod"), Nothing)
If auxBagValues IsNot Nothing Then
     Dim auxValue As Object
End If
auxClass.Conn.gConn_Close()
End Select

Select Case Request.QueryString("_mode_")
 Case "2"
Dim auxClass As New clscusimDOC
auxClass.Conn.gConn_Open()
Dim auxBagValues As clshrcBagValues = auxClass.gSystem_PostAction_v2(4,Val(Request.QueryString("_mode_")),Databinder.Eval(frmdatos.DataItem ,"cod"), Nothing)
If auxBagValues IsNot Nothing Then
     Dim auxValue As Object
End If
auxClass.Conn.gConn_Close()
End Select

Select Case Request.QueryString("_mode_")
 Case "2"
Dim auxClass As New clscusimDOC
auxClass.Conn.gConn_Open()
Dim auxBagValues As clshrcBagValues = auxClass.gSystem_PostAction_v2(4,Val(Request.QueryString("_mode_")),Databinder.Eval(frmdatos.DataItem ,"cod"), Nothing)
If auxBagValues IsNot Nothing Then
     Dim auxValue As Object
End If
auxClass.Conn.gConn_Close()
End Select

Select Case Request.QueryString("_mode_")
 Case "2"
Dim auxClass As New clscusimDOC
auxClass.Conn.gConn_Open()
Dim auxBagValues As clshrcBagValues = auxClass.gSystem_PostAction_v2(4,Val(Request.QueryString("_mode_")),Databinder.Eval(frmdatos.DataItem ,"cod"), Nothing)
If auxBagValues IsNot Nothing Then
     Dim auxValue As Object
End If
auxClass.Conn.gConn_Close()
End Select

Select Case Request.QueryString("_mode_")
 Case "2"
Dim auxClass As New clscusimDOC
auxClass.Conn.gConn_Open()
Dim auxBagValues As clshrcBagValues = auxClass.gSystem_PostAction_v2(4,Val(Request.QueryString("_mode_")),Databinder.Eval(frmdatos.DataItem ,"cod"), Nothing)
If auxBagValues IsNot Nothing Then
     Dim auxValue As Object
End If
auxClass.Conn.gConn_Close()
End Select

Select Case Request.QueryString("_mode_")
 Case "2"
Dim auxClass As New clscusimDOC
auxClass.Conn.gConn_Open()
Dim auxBagValues As clshrcBagValues = auxClass.gSystem_PostAction_v2(4,Val(Request.QueryString("_mode_")),Databinder.Eval(frmdatos.DataItem ,"cod"), Nothing)
If auxBagValues IsNot Nothing Then
     Dim auxValue As Object
End If
auxClass.Conn.gConn_Close()
End Select

Select Case Request.QueryString("_mode_")
 Case "2"
Dim auxClass As New clscusimDOC
auxClass.Conn.gConn_Open()
Dim auxBagValues As clshrcBagValues = auxClass.gSystem_PostAction_v2(4,Val(Request.QueryString("_mode_")),Databinder.Eval(frmdatos.DataItem ,"cod"), Nothing)
If auxBagValues IsNot Nothing Then
     Dim auxValue As Object
End If
auxClass.Conn.gConn_Close()
End Select

Select Case Request.QueryString("_mode_")
 Case "2"
Dim auxClass As New clscusimDOC
auxClass.Conn.gConn_Open()
Dim auxBagValues As clshrcBagValues = auxClass.gSystem_PostAction_v2(4,Val(Request.QueryString("_mode_")),Databinder.Eval(frmdatos.DataItem ,"cod"), Nothing)
If auxBagValues IsNot Nothing Then
     Dim auxValue As Object
End If
auxClass.Conn.gConn_Close()
End Select

Select Case Request.QueryString("_mode_")
 Case "2"
Dim auxClass As New clscusimDOC
auxClass.Conn.gConn_Open()
Dim auxBagValues As clshrcBagValues = auxClass.gSystem_PostAction_v2(4,Val(Request.QueryString("_mode_")),Databinder.Eval(frmdatos.DataItem ,"cod"), Nothing)
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
         Dim auxfueditctrl000004=Ctype(sender.parent.FindControl("frmdatos$edittabPanel$edittabPanel004$editctrl000004"),HiddenField)
If auxfueditctrl000004 IsNot Nothing Then
  Dim auxauxfueditctrl000004conn As New Intelimedia.imComponentes.imClientConnection
  Dim auxfueditctrl000004binary As clsHrcConnClient.clsBinaryData = auxauxfueditctrl000004conn.gFileTmp_GetBinary(auxfueditctrl000004.Value, False)
  If auxfueditctrl000004binary IsNot Nothing Then
      Dim auxConneditctrl000004 As clsHrcConnClient = Session("conn")
      auxConneditctrl000004.gConn_Open
      If auxfueditctrl000004binary.FileName <> "" Then
          auxConneditctrl000004.gConn_ImageToBLOB(auxfueditctrl000004binary.FileName, auxfueditctrl000004binary.Content,"","UPDATE DOC_DOCTIP SET logo ={#FILECONTENT#} WHERE cod=  " & auxDataKeyCod,50,30,-1,-1)
      auxfueditctrl000004.Value=""
      Else
          auxConneditctrl000004.gConn_BLOBdelete ("UPDATE DOC_DOCTIP SET logo ={#FILECONTENT#} WHERE cod=  " & auxDataKeyCod)
      End If
      auxConneditctrl000004.gConn_Close
  End If
End If

    Dim auxResult as String=auxClass.gSystem_PostAction(4,12,auxDataKeyCod) 
    auxClass.gCluster_LoadStaticTables_DOC_DOCTIP(auxDataKeyCod) 
    lblerror.Text &=auxResult
    If auxResult ="" Then
gForm_Close
     End If
    auxClass.Conn.gConn_Close
End If

End Sub
Protected Sub frmdatos_ItemUpdating(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.FormViewUpdateEventArgs)
CType(CType(sender,FormView).DataSourceObject,SqlDataSource).UpdateParameters("templatehead").DefaultValue = (New Intelimedia.imComponentes.imClientConnection).gTextTmp_Download(CType(frmdatos.FindControl("edittabPanel").FindControl("edittabPanel001").FindControl("editctrl000025"), HiddenField).Value)

CType(CType(sender,FormView).DataSourceObject,SqlDataSource).UpdateParameters("templatebody").DefaultValue = (New Intelimedia.imComponentes.imClientConnection).gTextTmp_Download(CType(frmdatos.FindControl("edittabPanel").FindControl("edittabPanel001").FindControl("editctrl000024"), HiddenField).Value)

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
         Response.Redirect(Session("ROOTWEBURL") & "frmTiposdedocumentos.aspx?_mode_=7&_closea_=0")
     End If

End Sub
Protected Sub frmTiposdedocumentos_det_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
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

