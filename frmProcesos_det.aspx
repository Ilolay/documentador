<%@ Page Language="VB" CodeFile="frmProcesos_det.aspx.vb" MasterPageFile="general.master" AutoEventWireup="True" Inherits="frmProcesos_det" Title="Procesos" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="ajaxkit" %>
<asp:Content id="cHeader" ContentPlaceHolderID="conHeader" runat="Server">
<style  media="print" type="text/css" > .hide_print {display: none;}</style>
</asp:Content>
<asp:Content id="cDatos" ContentPlaceHolderID="ContentDatos" runat="Server">
<asp:ScriptManager  ID="ScriptManagerHrc" runat="server" EnableScriptLocalization="True" EnableScriptGlobalization="True" ScriptMode="Release" >
</asp:ScriptManager>
<table width="100%" cellpadding="0" cellspacing="0">
<tr><td  colspan="1" >
<table width="100%" cellpadding="0" cellspacing="0">
<tr><td  style="width:32px;" colspan="1" ><img  src="imagenes/icon00000031.png" width="32px" height="32px" alt="Procesos" hspace="4"/></td>
<td  style="text-align:left;" colspan="1" ><span class="form-title">Procesos</span> |<asp:Label  runat="server" ID="lblsubtitle" CssClass="form-subtitle" />
</td></tr>
</table>
</td></tr>
<tr><td  colspan="1" >				<!-- Init -->
<table id="body" style="background-color:#F0F0F0;" width="100%">
					<br /><asp:Label  runat="server" ID="lblerror" CssClass="error" width="100%" />			<asp:ValidationSummary  runat="server" ID="frmdatosvalsumary" />
<asp:FormView  ID="frmdatos" runat="server" DataSourceID="dsdatos" DataKeyNames="cod" DefaultMode="ReadOnly" OnItemDeleted="frmdatos_ItemDeleted" OnItemUpdated="frmdatos_ItemUpdated" OnItemInserted="frmdatos_ItemInserted" OnDataBound="frmdatos_ItemSelected" >
<ItemTemplate>

<!-- Init Plantilla item --><div  runat="server" Visible='<%# Request.Querystring("_view_")<> 1 %>' ><asp:Button  runat="server" ID="cmdFormViewConfirmDelete" Text="Borrar" CssClass="boton-acciones" CommandName="Delete" CausesValidation="True" Style="color:#FF0000; font-style:italic;" onClientClick="javascript:return confirm('Confirma la eliminación?')" />
<asp:Button  runat="server" ID="cmdrefresh" Text="Refrescar" CssClass="boton-acciones" CausesValidation="False" />
<asp:Button  runat="server" ID="cmdFormViewItemCancel" Text="Cancelar" CssClass="boton-acciones" CausesValidation="False" OnClick="cmdFormViewItemCancel_Click" />
<asp:Button  runat="server" ID="cmdSecPermView" Text="Permisos" CssClass="boton-acciones" CausesValidation="False" OnDataBinding="cmdSecPermView_DataBinding" onClientClick='<%# "javascript:if (window.showModalDialog) {window.showModalDialog(" & chr(39) & "frmsecperm.aspx??_mode_=0&_closea_=1&isDlg=1&param1=-1&timestamp=" & chr(39) & " + new Date().getTime() + " & chr(39) & "" & chr(39) & ", " & chr(39) & "" & chr(39) & ", " & chr(39) & "menubar=no;resizable=yes;toolbar=no;status=no;center=yes;dialogWidth=750px;dialogHeight=570px;" & chr(39) & ");} else {window.open(" & chr(39) & "frmsecperm.aspx??_mode_=0&_closea_=1&isDlg=1&param1=-1&timestamp=" & chr(39) & " + new Date().getTime() + " & chr(39) & "" & chr(39) & ", " & chr(39) & "" & chr(39) & ", " & chr(39) & "modal=yes;directories=no;menubar=no;resizable=yes;toolbar=no;status=no;center=yes;dialogWidth=750px;dialogHeight=570px;" & chr(39) & ");};return false;" %>' />
<asp:Button  runat="server" ID="cmdFormViewItemUpdate" Text="Editar" CssClass="boton-acciones" CausesValidation="False" OnClick="cmdFormViewItemUpdate_Click" OnDataBinding="cmdFormViewItemUpdate_DataBinding" />
</div>
<table width="100%" cellpadding="1" cellspacing="1"><tr>
<td  colspan="1" >
Código
</td>
<td  colspan="1" >
<asp:Label  runat="server" ID="itemctrl000009" CssClass="form-control-read" width="100%" OnDataBinding="itemctrl000009_Databound" />
</td>
</tr>
<tr>
<td  colspan="1" >
Descripción
</td>
<td  colspan="1" >
<asp:Label  runat="server" ID="itemctrl000007" CssClass="form-control-read" width="100%" OnDataBinding="itemctrl000007_Databound" />
</td>
</tr>
<tr>
<td  colspan="1" >
Tipo de proceso
</td>
<td  colspan="1" >
<asp:LinkButton runat="server" ID="itemctrl000006view" Text='<%# Eval("apacoddsc") %>' CssClass="form-control-read" OnDataBinding="itemctrl000006view_DataBinding" CausesValidation="False" OnClientClick='<%# "javascript:if (window.showModalDialog) {window.showModalDialog(" & chr(39) & "frmTiposdeprocesos_det.aspx?_mode_=0&_closea_=1&isDlg=1&param1=" & Eval("apacod") & "&timestamp=" & chr(39) & " + new Date().getTime() + " & chr(39) & "" & chr(39) & ", " & chr(39) & "" & chr(39) & ", " & chr(39) & "menubar=no;resizable=yes;toolbar=no;status=no;center=yes;dialogWidth=750px;dialogHeight=570px;" & chr(39) & ");} else {window.open(" & chr(39) & "frmTiposdeprocesos_det.aspx?_mode_=0&_closea_=1&isDlg=1&param1=" & Eval("apacod") & "&timestamp=" & chr(39) & " + new Date().getTime() + " & chr(39) & "" & chr(39) & ", " & chr(39) & "" & chr(39) & ", " & chr(39) & "modal=yes;directories=no;menubar=no;resizable=yes;toolbar=no;status=no;center=yes;dialogWidth=750px;dialogHeight=570px;" & chr(39) & ");};return false;" %>' PostBackUrl='<%# "javascript:if (window.showModalDialog) {window.showModalDialog(" & chr(39) & "frmTiposdeprocesos_det.aspx?_mode_=0&_closea_=1&isDlg=1&param1=" & Eval("apacod") & "&timestamp=" & chr(39) & " + new Date().getTime() + " & chr(39) & "" & chr(39) & ", " & chr(39) & "" & chr(39) & ", " & chr(39) & "menubar=no;resizable=yes;toolbar=no;status=no;center=yes;dialogWidth=750px;dialogHeight=570px;" & chr(39) & ");} else {window.open(" & chr(39) & "frmTiposdeprocesos_det.aspx?_mode_=0&_closea_=1&isDlg=1&param1=" & Eval("apacod") & "&timestamp=" & chr(39) & " + new Date().getTime() + " & chr(39) & "" & chr(39) & ", " & chr(39) & "" & chr(39) & ", " & chr(39) & "modal=yes;directories=no;menubar=no;resizable=yes;toolbar=no;status=no;center=yes;dialogWidth=750px;dialogHeight=570px;" & chr(39) & ");};return false;" %>' />

</td>
</tr>
<tr>
<td  colspan="1" >
Dueño
</td>
<td  colspan="1" >
<asp:LinkButton runat="server" ID="itemctrl000005view" Text='<%# Eval("ownertypedsc") %>' CssClass="form-control-read" OnDataBinding="itemctrl000005view_DataBinding" CausesValidation="False" OnClientClick='<%# "javascript:if (window.showModalDialog) {window.showModalDialog(" & chr(39) & "frmProcesos_det.aspx?_mode_=0&_closea_=1&isDlg=1&param1=" & Eval("ownertypevalue") & "&timestamp=" & chr(39) & " + new Date().getTime() + " & chr(39) & "" & chr(39) & ", " & chr(39) & "" & chr(39) & ", " & chr(39) & "menubar=no;resizable=yes;toolbar=no;status=no;center=yes;dialogWidth=750px;dialogHeight=570px;" & chr(39) & ");} else {window.open(" & chr(39) & "frmProcesos_det.aspx?_mode_=0&_closea_=1&isDlg=1&param1=" & Eval("ownertypevalue") & "&timestamp=" & chr(39) & " + new Date().getTime() + " & chr(39) & "" & chr(39) & ", " & chr(39) & "" & chr(39) & ", " & chr(39) & "modal=yes;directories=no;menubar=no;resizable=yes;toolbar=no;status=no;center=yes;dialogWidth=750px;dialogHeight=570px;" & chr(39) & ");};return false;" %>' PostBackUrl='<%# "javascript:if (window.showModalDialog) {window.showModalDialog(" & chr(39) & "frmProcesos_det.aspx?_mode_=0&_closea_=1&isDlg=1&param1=" & Eval("ownertypevalue") & "&timestamp=" & chr(39) & " + new Date().getTime() + " & chr(39) & "" & chr(39) & ", " & chr(39) & "" & chr(39) & ", " & chr(39) & "menubar=no;resizable=yes;toolbar=no;status=no;center=yes;dialogWidth=750px;dialogHeight=570px;" & chr(39) & ");} else {window.open(" & chr(39) & "frmProcesos_det.aspx?_mode_=0&_closea_=1&isDlg=1&param1=" & Eval("ownertypevalue") & "&timestamp=" & chr(39) & " + new Date().getTime() + " & chr(39) & "" & chr(39) & ", " & chr(39) & "" & chr(39) & ", " & chr(39) & "modal=yes;directories=no;menubar=no;resizable=yes;toolbar=no;status=no;center=yes;dialogWidth=750px;dialogHeight=570px;" & chr(39) & ");};return false;" %>' />
<asp:HiddenField  runat="server" ID="itemctrl000005"/>
<asp:HiddenField  runat="server" ID="itemctrl000005value" OnDataBinding="itemctrl000005value_DataBound"/>

</td>
</tr>
<tr>
<td  colspan="1" >
Orden
</td>
<td  colspan="1" >
<asp:Label  runat="server" ID="itemctrl000004" CssClass="form-control-read" width="100%" OnDataBinding="itemctrl000004_Databound" />
</td>
</tr>
<tr>
<td  colspan="2" >
<ajaxkit:TabContainer ID="itemtabPanel" runat="server" >
<ajaxkit:TabPanel ID="itemtabPanel001" runat="server" HeaderText="Estado y auditoría" Tooltip="Estado y auditoría" >
<ContentTemplate>
<table>
<tr>
<td  colspan="1" >
Último en modificar
</td>
<td  colspan="1" >
<asp:LinkButton runat="server" ID="itemctrl000003view" Text='<%# Eval("qsecsiddsc") %>' CssClass="form-control-read" OnDataBinding="itemctrl000003view_DataBinding" CausesValidation="False" OnClientClick='<%# "javascript:if (window.showModalDialog) {window.showModalDialog(" & chr(39) & "frmUsuarios_det.aspx?_mode_=0&_closea_=1&isDlg=1&param1=" & Eval("qsecsid") & "&timestamp=" & chr(39) & " + new Date().getTime() + " & chr(39) & "" & chr(39) & ", " & chr(39) & "" & chr(39) & ", " & chr(39) & "menubar=no;resizable=yes;toolbar=no;status=no;center=yes;dialogWidth=750px;dialogHeight=570px;" & chr(39) & ");} else {window.open(" & chr(39) & "frmUsuarios_det.aspx?_mode_=0&_closea_=1&isDlg=1&param1=" & Eval("qsecsid") & "&timestamp=" & chr(39) & " + new Date().getTime() + " & chr(39) & "" & chr(39) & ", " & chr(39) & "" & chr(39) & ", " & chr(39) & "modal=yes;directories=no;menubar=no;resizable=yes;toolbar=no;status=no;center=yes;dialogWidth=750px;dialogHeight=570px;" & chr(39) & ");};return false;" %>' PostBackUrl='<%# "javascript:if (window.showModalDialog) {window.showModalDialog(" & chr(39) & "frmUsuarios_det.aspx?_mode_=0&_closea_=1&isDlg=1&param1=" & Eval("qsecsid") & "&timestamp=" & chr(39) & " + new Date().getTime() + " & chr(39) & "" & chr(39) & ", " & chr(39) & "" & chr(39) & ", " & chr(39) & "menubar=no;resizable=yes;toolbar=no;status=no;center=yes;dialogWidth=750px;dialogHeight=570px;" & chr(39) & ");} else {window.open(" & chr(39) & "frmUsuarios_det.aspx?_mode_=0&_closea_=1&isDlg=1&param1=" & Eval("qsecsid") & "&timestamp=" & chr(39) & " + new Date().getTime() + " & chr(39) & "" & chr(39) & ", " & chr(39) & "" & chr(39) & ", " & chr(39) & "modal=yes;directories=no;menubar=no;resizable=yes;toolbar=no;status=no;center=yes;dialogWidth=750px;dialogHeight=570px;" & chr(39) & ");};return false;" %>' />

</td>
</tr>
<tr>
<td  colspan="1" >
Última actualización
</td>
<td  colspan="1" >
<asp:Label  runat="server" ID="itemctrl000002" CssClass="form-control-read" width="100%" OnDataBinding="itemctrl000002_Databound" />
</td>
</tr>
</ContentTemplate>
</ajaxkit:TabPanel>
</ajaxkit:TabContainer>
</td>
</tr>
</table>
<!-- End Plantilla item --></ItemTemplate>

<EditItemTemplate>

<!-- Init Plantilla edicion --><div  runat="server" Visible='<%# Request.Querystring("_view_")<> 1 %>' ><asp:Button  runat="server" ID="cmdFormViewConfirmUpdate" Text="Guardar" CssClass="boton-acciones" CommandName="Update" CausesValidation="True" />
<asp:Button  runat="server" ID="cmdFormUpdateItemView" Text="Ver" CssClass="boton-acciones" CausesValidation="False" OnClick="cmdFormUpdateItemView_Click" />
<asp:Button  runat="server" ID="cmdFormViewCancelUpdate" Text="Cancelar" CssClass="boton-acciones" CausesValidation="False" OnClick="cmdFormViewCancelUpdate_Click" />
</div>
<table width="100%" cellpadding="1" cellspacing="1"><tr>
<td  colspan="1" >
<b>Descripción</b>
</td>
<td  colspan="1" >
<asp:TextBox  runat="server" ID="editctrl000007" CssClass="form-control" Width="400px" MaxLength="50" OnDataBinding="editctrl000007_DataBound_frmdatos" Tooltip="Campo164[]" TextMode="MultiLine" onkeypress="return textCounter(this,50);" onkeydown="return textCounter(this,50);" onkeyup="return textCounter(this,50);" onfocus="return textCounter_GetFocus(this,50);" onblur="return textCounter_LostFocus(this);" /> <span class='error'><b>*</b></span>
<asp:RequiredFieldValidator  runat="server" ID="vrqeditctrl000007" SetFocusOnError="true" CssClass="error" Display="Dynamic" ControltoValidate="editctrl000007" Text="Obligatorio!!! ErrorMessage=´Descripción:es un dato obligatorio!´" ValidationGroup="vgedit" />
<ajaxkit:ValidatorCalloutExtender  runat="server" ID="ajaxvrqeditctrl000007" TargetControlID="vrqeditctrl000007" /><asp:CompareValidator  runat="server" ID="vcdvaleditctrl000007" SetFocusOnError="true" CssClass="error" ControltoValidate="editctrl000007" Display="Dynamic" Text="Ingrese un texto" ErrorMessage="Descripción:Ingrese un texto" Type="String" Operator="DataTypeCheck" ValidationGroup="vgedit" />
<asp:RegularExpressionValidator  runat="server" ID="vrgvaleditctrl000007" SetFocusOnError="true" CssClass="error" Display="Dynamic" ControltoValidate="editctrl000007" ValidationExpression="[\w\s-+/*^()!|°$%&=-_,:;.ñÑ~áéíóúÁÉÍÓÚ#}{¿]{0,50}" Text="No mayor a 50 caracteres. Deben ser letras o numeros." ErrorMessage="Descripción:No mayor a 50 caracteres. Deben ser letras o numeros." ValidationGroup="vgedit" />
<ajaxkit:ValidatorCalloutExtender  runat="server" ID="ajaxvrgeditctrl000007" TargetControlID="vrgvaleditctrl000007" />

</td>
</tr>
<tr>
<td  colspan="1" >

</td>
<td  colspan="1" >
<asp:SqlDataSource  runat="server" ID="dseditctrl000006" CancelSelectOnNullParameter="False" onInit="dseditctrl000006_Init" SelectCommandType="Text"
 SelectCommand="SELECT DOC_APA.dsc,DOC_APA.cod FROM DOC_APA   WHERE ((DOC_APA.baja = '1900-01-01T00:00:00' OR DOC_APA.baja  IS NULL)) AND DOC_APA.cod >= 1 ORDER BY DOC_APA.orden" onselected="dseditctrl000006_Selected" >
<SelectParameters>
</SelectParameters>
</asp:SqlDataSource>

</td>
</tr>
<tr>
<td  colspan="1" >
Tipo de proceso
</td>
<td  colspan="1" >
<asp:UpdatePanel  ID="updupdeditctrl000006fs" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="True" >
<ContentTemplate>
<asp:TextBox  runat="server" ID="txteditctrl000006fs" CssClass="form-control" Width="15%" AutoPostBack="True" onTextChanged="txteditctrl000006fs_TextChanged" />
<asp:CompareValidator  runat="server" ID="vcdvaltxteditctrl000006fs" SetFocusOnError="true" CssClass="error" ControltoValidate="txteditctrl000006fs" Display="Dynamic" Text="Ingrese un texto" ErrorMessage=":Ingrese un texto" Type="String" Operator="DataTypeCheck" />
<asp:RegularExpressionValidator  runat="server" ID="vrgvaltxteditctrl000006fs" SetFocusOnError="true" CssClass="error" Display="Dynamic" ControltoValidate="txteditctrl000006fs" ValidationExpression="[\w\s-+/*^()!|°$%&=-_,:;.ñÑ~áéíóúÁÉÍÓÚ#}{¿]" Text=" Text=´Ingrese un texto´" ErrorMessage=": Text=´Ingrese un texto´" />
<ajaxkit:ValidatorCalloutExtender  runat="server" ID="ajaxvrgtxteditctrl000006fs" TargetControlID="vrgvaltxteditctrl000006fs" />
<asp:DropDownList  runat="server" ID="editctrl000006" CssClass="form-control" Width="80%" DataSourceID="dseditctrl000006" AppendDataBoundItems="False" DataTextField="dsc" DataValueField="cod" Enabled="True" onDataBound="editctrl000006_DataBound" >
</asp:DropDownList>
<asp:LinkButton runat="server" ID="editctrl000006new" Text="Nuevo..." CssClass="boton-acciones-subbutton" CausesValidation="False" OnClientClick='<%# "javascript:if (window.showModalDialog) {window.showModalDialog(" & chr(39) & "frmTiposdeprocesos_det.aspx?_mode_=1&_closea_=1&isDlg=1&timestamp=" & chr(39) & " + new Date().getTime() + " & chr(39) & "" & chr(39) & ", " & chr(39) & "" & chr(39) & ", " & chr(39) & "menubar=no;resizable=yes;toolbar=no;status=no;center=yes;dialogWidth=750px;dialogHeight=570px;" & chr(39) & ");} else {window.open(" & chr(39) & "frmTiposdeprocesos_det.aspx?_mode_=1&_closea_=1&isDlg=1&timestamp=" & chr(39) & " + new Date().getTime() + " & chr(39) & "" & chr(39) & ", " & chr(39) & "" & chr(39) & ", " & chr(39) & "modal=yes;directories=no;menubar=no;resizable=yes;toolbar=no;status=no;center=yes;dialogWidth=750px;dialogHeight=570px;" & chr(39) & ");};return false;" %>' PostBackUrl='<%# "javascript:if (window.showModalDialog) {window.showModalDialog(" & chr(39) & "frmTiposdeprocesos_det.aspx?_mode_=1&_closea_=1&isDlg=1&timestamp=" & chr(39) & " + new Date().getTime() + " & chr(39) & "" & chr(39) & ", " & chr(39) & "" & chr(39) & ", " & chr(39) & "menubar=no;resizable=yes;toolbar=no;status=no;center=yes;dialogWidth=750px;dialogHeight=570px;" & chr(39) & ");} else {window.open(" & chr(39) & "frmTiposdeprocesos_det.aspx?_mode_=1&_closea_=1&isDlg=1&timestamp=" & chr(39) & " + new Date().getTime() + " & chr(39) & "" & chr(39) & ", " & chr(39) & "" & chr(39) & ", " & chr(39) & "modal=yes;directories=no;menubar=no;resizable=yes;toolbar=no;status=no;center=yes;dialogWidth=750px;dialogHeight=570px;" & chr(39) & ");};return false;" %>' />
<asp:LinkButton runat="server" ID="editctrl000006_comborefresh" Text="Refrescar" CssClass="boton-acciones-subbutton" OnClick="editctrl000006_comborefresh_Click" CausesValidation="False" />
<asp:LinkButton runat="server" ID="editctrl000006view" Text='<%# Eval("apacoddsc") %>' CssClass="boton-acciones-subbutton" OnDataBinding="editctrl000006view_DataBinding" CausesValidation="False" OnClientClick='<%# "javascript:if (window.showModalDialog) {window.showModalDialog(" & chr(39) & "frmTiposdeprocesos_det.aspx?_mode_=0&_closea_=1&isDlg=1&param1=" & Eval("apacod") & "&timestamp=" & chr(39) & " + new Date().getTime() + " & chr(39) & "" & chr(39) & ", " & chr(39) & "" & chr(39) & ", " & chr(39) & "menubar=no;resizable=yes;toolbar=no;status=no;center=yes;dialogWidth=750px;dialogHeight=570px;" & chr(39) & ");} else {window.open(" & chr(39) & "frmTiposdeprocesos_det.aspx?_mode_=0&_closea_=1&isDlg=1&param1=" & Eval("apacod") & "&timestamp=" & chr(39) & " + new Date().getTime() + " & chr(39) & "" & chr(39) & ", " & chr(39) & "" & chr(39) & ", " & chr(39) & "modal=yes;directories=no;menubar=no;resizable=yes;toolbar=no;status=no;center=yes;dialogWidth=750px;dialogHeight=570px;" & chr(39) & ");};return false;" %>' PostBackUrl='<%# "javascript:if (window.showModalDialog) {window.showModalDialog(" & chr(39) & "frmTiposdeprocesos_det.aspx?_mode_=0&_closea_=1&isDlg=1&param1=" & Eval("apacod") & "&timestamp=" & chr(39) & " + new Date().getTime() + " & chr(39) & "" & chr(39) & ", " & chr(39) & "" & chr(39) & ", " & chr(39) & "menubar=no;resizable=yes;toolbar=no;status=no;center=yes;dialogWidth=750px;dialogHeight=570px;" & chr(39) & ");} else {window.open(" & chr(39) & "frmTiposdeprocesos_det.aspx?_mode_=0&_closea_=1&isDlg=1&param1=" & Eval("apacod") & "&timestamp=" & chr(39) & " + new Date().getTime() + " & chr(39) & "" & chr(39) & ", " & chr(39) & "" & chr(39) & ", " & chr(39) & "modal=yes;directories=no;menubar=no;resizable=yes;toolbar=no;status=no;center=yes;dialogWidth=750px;dialogHeight=570px;" & chr(39) & ");};return false;" %>' />
</ContentTemplate>
</asp:UpdatePanel>
<asp:UpdateProgress  runat="server" AssociatedUpdatePanelID="updupdeditctrl000006fs" >
<ProgressTemplate><span  class="ajaxupd" ><div style="position:absolute;top:2px;left:50%; border-color:#333333; border:1; border-style:solid; background-color:#FFFFFF;padding:3px;"><image src="imagenes/ajaxupdate.gif" border=0 />Actualizando</div></span></ProgressTemplate>
</asp:UpdateProgress>

</td>
</tr>
<tr>
<td  colspan="1" >

</td>
<td  colspan="1" >
<asp:SqlDataSource  runat="server" ID="dseditctrl000005" CancelSelectOnNullParameter="False" onInit="dseditctrl000005_Init" SelectCommandType="Text"
 SelectCommand="SELECT DOC_PRO.dsc,DOC_PRO.cod FROM DOC_PRO   WHERE (DOC_PRO.baja = 0 OR DOC_PRO.baja  IS NULL) ORDER BY DOC_PRO.orden" onselected="dseditctrl000005_Selected" >
<SelectParameters>
</SelectParameters>
</asp:SqlDataSource>

</td>
</tr>
<tr>
<td  colspan="1" >
Dueño
</td>
<td  colspan="1" >
<asp:UpdatePanel  ID="updupdpnlupdeditctrl000005fs" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="True" >
<Triggers>
  <asp:AsyncPostBackTrigger  ControlID="editctrl000005delete" EventName="Click" />
</Triggers>
<ContentTemplate>
<asp:UpdatePanel  ID="updupdeditctrl000005fs" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="True" >
<ContentTemplate>
<asp:Button  runat="server" ID="cmdeditctrl000005showpanel" Text="..." CssClass="boton-acciones" CausesValidation="False" OnClick="cmdeditctrl000005showpanel_Click" />

<asp:ImageButton runat="server" ID="editctrl000005delete" ImageURL="~/imagenes/actdel.png" Width="16" Height="16" Tooltip="Quitar Dueño" Visible="False" OnClick="editctrl000005delete_Click" OnDataBinding="editctrl000005delete_DataBinding" CausesValidation="False" />
<asp:LinkButton runat="server" ID="editctrl000005view" Text='<%# Eval("ownertypedsc") %>' CssClass="boton-acciones-subbutton" OnDataBinding="editctrl000005view_DataBinding" CausesValidation="False" OnClientClick='<%# "javascript:if (window.showModalDialog) {window.showModalDialog(" & chr(39) & "frmProcesos_det.aspx?_mode_=0&_closea_=1&isDlg=1&param1=" & Eval("ownertypevalue") & "&timestamp=" & chr(39) & " + new Date().getTime() + " & chr(39) & "" & chr(39) & ", " & chr(39) & "" & chr(39) & ", " & chr(39) & "menubar=no;resizable=yes;toolbar=no;status=no;center=yes;dialogWidth=750px;dialogHeight=570px;" & chr(39) & ");} else {window.open(" & chr(39) & "frmProcesos_det.aspx?_mode_=0&_closea_=1&isDlg=1&param1=" & Eval("ownertypevalue") & "&timestamp=" & chr(39) & " + new Date().getTime() + " & chr(39) & "" & chr(39) & ", " & chr(39) & "" & chr(39) & ", " & chr(39) & "modal=yes;directories=no;menubar=no;resizable=yes;toolbar=no;status=no;center=yes;dialogWidth=750px;dialogHeight=570px;" & chr(39) & ");};return false;" %>' PostBackUrl='<%# "javascript:if (window.showModalDialog) {window.showModalDialog(" & chr(39) & "frmProcesos_det.aspx?_mode_=0&_closea_=1&isDlg=1&param1=" & Eval("ownertypevalue") & "&timestamp=" & chr(39) & " + new Date().getTime() + " & chr(39) & "" & chr(39) & ", " & chr(39) & "" & chr(39) & ", " & chr(39) & "menubar=no;resizable=yes;toolbar=no;status=no;center=yes;dialogWidth=750px;dialogHeight=570px;" & chr(39) & ");} else {window.open(" & chr(39) & "frmProcesos_det.aspx?_mode_=0&_closea_=1&isDlg=1&param1=" & Eval("ownertypevalue") & "&timestamp=" & chr(39) & " + new Date().getTime() + " & chr(39) & "" & chr(39) & ", " & chr(39) & "" & chr(39) & ", " & chr(39) & "modal=yes;directories=no;menubar=no;resizable=yes;toolbar=no;status=no;center=yes;dialogWidth=750px;dialogHeight=570px;" & chr(39) & ");};return false;" %>' />
<asp:HiddenField  runat="server" ID="editctrl000005"/>
<asp:HiddenField  runat="server" ID="editctrl000005value" OnDataBinding="editctrl000005value_DataBound"/>
</ContentTemplate>
</asp:UpdatePanel>
<asp:UpdateProgress  runat="server" AssociatedUpdatePanelID="updupdeditctrl000005fs" >
<ProgressTemplate><span  class="ajaxupd" ><div style="position:absolute;top:2px;left:50%; border-color:#333333; border:1; border-style:solid; background-color:#FFFFFF;padding:3px;"><image src="imagenes/ajaxupdate.gif" border=0 />Actualizando</div></span></ProgressTemplate>
</asp:UpdateProgress>
</ContentTemplate>
</asp:UpdatePanel>
<asp:UpdateProgress  runat="server" AssociatedUpdatePanelID="updupdpnlupdeditctrl000005fs" >
<ProgressTemplate><span  class="ajaxupd" ><div style="position:absolute;top:2px;left:50%; border-color:#333333; border:1; border-style:solid; background-color:#FFFFFF;padding:3px;"><image src="imagenes/ajaxupdate.gif" border=0 />Actualizando</div></span></ProgressTemplate>
</asp:UpdateProgress>

</td>
</tr>
<tr>
<td  colspan="1" >
Orden
</td>
<td  colspan="1" >
<asp:Label  runat="server" ID="editctrl000004" CssClass="form-control-read" width="100%" OnDataBinding="editctrl000004_Databound" />
</td>
</tr>
<tr>
<td  colspan="2" >
<ajaxkit:TabContainer ID="edittabPanel" runat="server" >
<ajaxkit:TabPanel ID="edittabPanel001" runat="server" HeaderText="Estado y auditoría" Tooltip="Estado y auditoría" >
<ContentTemplate>
<table>
<tr>
<td  colspan="1" >
Último en modificar
</td>
<td  colspan="1" >
<asp:LinkButton runat="server" ID="editctrl000003view" Text='<%# Eval("qsecsiddsc") %>' CssClass="form-control-read" OnDataBinding="editctrl000003view_DataBinding" CausesValidation="False" OnClientClick='<%# "javascript:if (window.showModalDialog) {window.showModalDialog(" & chr(39) & "frmUsuarios_det.aspx?_mode_=0&_closea_=1&isDlg=1&param1=" & Eval("qsecsid") & "&timestamp=" & chr(39) & " + new Date().getTime() + " & chr(39) & "" & chr(39) & ", " & chr(39) & "" & chr(39) & ", " & chr(39) & "menubar=no;resizable=yes;toolbar=no;status=no;center=yes;dialogWidth=750px;dialogHeight=570px;" & chr(39) & ");} else {window.open(" & chr(39) & "frmUsuarios_det.aspx?_mode_=0&_closea_=1&isDlg=1&param1=" & Eval("qsecsid") & "&timestamp=" & chr(39) & " + new Date().getTime() + " & chr(39) & "" & chr(39) & ", " & chr(39) & "" & chr(39) & ", " & chr(39) & "modal=yes;directories=no;menubar=no;resizable=yes;toolbar=no;status=no;center=yes;dialogWidth=750px;dialogHeight=570px;" & chr(39) & ");};return false;" %>' PostBackUrl='<%# "javascript:if (window.showModalDialog) {window.showModalDialog(" & chr(39) & "frmUsuarios_det.aspx?_mode_=0&_closea_=1&isDlg=1&param1=" & Eval("qsecsid") & "&timestamp=" & chr(39) & " + new Date().getTime() + " & chr(39) & "" & chr(39) & ", " & chr(39) & "" & chr(39) & ", " & chr(39) & "menubar=no;resizable=yes;toolbar=no;status=no;center=yes;dialogWidth=750px;dialogHeight=570px;" & chr(39) & ");} else {window.open(" & chr(39) & "frmUsuarios_det.aspx?_mode_=0&_closea_=1&isDlg=1&param1=" & Eval("qsecsid") & "&timestamp=" & chr(39) & " + new Date().getTime() + " & chr(39) & "" & chr(39) & ", " & chr(39) & "" & chr(39) & ", " & chr(39) & "modal=yes;directories=no;menubar=no;resizable=yes;toolbar=no;status=no;center=yes;dialogWidth=750px;dialogHeight=570px;" & chr(39) & ");};return false;" %>' />

</td>
</tr>
<tr>
<td  colspan="1" >
Última actualización
</td>
<td  colspan="1" >
<asp:Label  runat="server" ID="editctrl000002" CssClass="form-control-read" width="100%" OnDataBinding="editctrl000002_Databound" />
</td>
</tr>
</ContentTemplate>
</ajaxkit:TabPanel>
</ajaxkit:TabContainer>
</td>
</tr>
</table>
<!-- End Plantilla edicion --></EditItemTemplate>

<InsertItemTemplate>

<!-- Init Plantilla insercion --><div  runat="server" Visible='<%# Request.Querystring("_view_")<> 1 %>' ><asp:Button  runat="server" ID="cmdFormViewReInsert" Text="Guardar y nuevo" CssClass="boton-acciones" CausesValidation="False" OnClick="cmdFormViewReInsert_Click" />
<asp:Button  runat="server" ID="cmdFormViewInsert" Text="Guardar" CssClass="boton-acciones" CausesValidation="False" OnClick="cmdFormViewInsert_Click" />
<asp:Button  runat="server" ID="cmdFormViewInsertCancel" Text="Cancelar" CssClass="boton-acciones" CausesValidation="False" OnClick="cmdFormViewInsertCancel_Click" />
</div>
<table width="100%" cellpadding="1" cellspacing="1"><tr>
<td  colspan="1" >
<b>Descripción</b>
</td>
<td  colspan="1" >
<asp:TextBox  runat="server" ID="insctrl000007" CssClass="form-control" Width="400px" MaxLength="50" Tooltip="Campo164[]" TextMode="MultiLine" onkeypress="return textCounter(this,50);" onkeydown="return textCounter(this,50);" onkeyup="return textCounter(this,50);" onfocus="return textCounter_GetFocus(this,50);" onblur="return textCounter_LostFocus(this);" /> <span class='error'><b>*</b></span>
<asp:RequiredFieldValidator  runat="server" ID="vrqinsctrl000007" SetFocusOnError="true" CssClass="error" Display="Dynamic" ControltoValidate="insctrl000007" Text="Obligatorio!!! ErrorMessage=´Descripción:es un dato obligatorio!´" ValidationGroup="vgins" />
<ajaxkit:ValidatorCalloutExtender  runat="server" ID="ajaxvrqinsctrl000007" TargetControlID="vrqinsctrl000007" /><asp:CompareValidator  runat="server" ID="vcdvalinsctrl000007" SetFocusOnError="true" CssClass="error" ControltoValidate="insctrl000007" Display="Dynamic" Text="Ingrese un texto" ErrorMessage="Descripción:Ingrese un texto" Type="String" Operator="DataTypeCheck" ValidationGroup="vgins" />
<asp:RegularExpressionValidator  runat="server" ID="vrgvalinsctrl000007" SetFocusOnError="true" CssClass="error" Display="Dynamic" ControltoValidate="insctrl000007" ValidationExpression="[\w\s-+/*^()!|°$%&=-_,:;.ñÑ~áéíóúÁÉÍÓÚ#}{¿]{0,50}" Text="No mayor a 50 caracteres. Deben ser letras o numeros." ErrorMessage="Descripción:No mayor a 50 caracteres. Deben ser letras o numeros." ValidationGroup="vgins" />
<ajaxkit:ValidatorCalloutExtender  runat="server" ID="ajaxvrginsctrl000007" TargetControlID="vrgvalinsctrl000007" />

</td>
</tr>
<tr>
<td  colspan="1" >

</td>
<td  colspan="1" >
<asp:SqlDataSource  runat="server" ID="dsinsctrl000006" CancelSelectOnNullParameter="False" onInit="dsinsctrl000006_Init" SelectCommandType="Text"
 SelectCommand="SELECT DOC_APA.dsc,DOC_APA.cod FROM DOC_APA   WHERE ((DOC_APA.baja = '1900-01-01T00:00:00' OR DOC_APA.baja  IS NULL)) AND DOC_APA.cod >= 1 ORDER BY DOC_APA.orden" onselected="dsinsctrl000006_Selected" >
<SelectParameters>
</SelectParameters>
</asp:SqlDataSource>

</td>
</tr>
<tr>
<td  colspan="1" >
Tipo de proceso
</td>
<td  colspan="1" >
<asp:UpdatePanel  ID="updupdinsctrl000006fs" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="True" >
<ContentTemplate>
<asp:TextBox  runat="server" ID="txtinsctrl000006fs" CssClass="form-control" Width="15%" AutoPostBack="True" onTextChanged="txtinsctrl000006fs_TextChanged" />
<asp:CompareValidator  runat="server" ID="vcdvaltxtinsctrl000006fs" SetFocusOnError="true" CssClass="error" ControltoValidate="txtinsctrl000006fs" Display="Dynamic" Text="Ingrese un texto" ErrorMessage=":Ingrese un texto" Type="String" Operator="DataTypeCheck" />
<asp:RegularExpressionValidator  runat="server" ID="vrgvaltxtinsctrl000006fs" SetFocusOnError="true" CssClass="error" Display="Dynamic" ControltoValidate="txtinsctrl000006fs" ValidationExpression="[\w\s-+/*^()!|°$%&=-_,:;.ñÑ~áéíóúÁÉÍÓÚ#}{¿]" Text=" Text=´Ingrese un texto´" ErrorMessage=": Text=´Ingrese un texto´" />
<ajaxkit:ValidatorCalloutExtender  runat="server" ID="ajaxvrgtxtinsctrl000006fs" TargetControlID="vrgvaltxtinsctrl000006fs" />
<asp:DropDownList  runat="server" ID="insctrl000006" CssClass="form-control" Width="80%" DataSourceID="dsinsctrl000006" AppendDataBoundItems="False" DataTextField="dsc" DataValueField="cod" Enabled="True" onload="insctrl000006_Load" >
</asp:DropDownList>
<asp:LinkButton runat="server" ID="insctrl000006new" Text="Nuevo..." CssClass="boton-acciones-subbutton" CausesValidation="False" OnClientClick='<%# "javascript:if (window.showModalDialog) {window.showModalDialog(" & chr(39) & "frmTiposdeprocesos_det.aspx?_mode_=1&_closea_=1&isDlg=1&timestamp=" & chr(39) & " + new Date().getTime() + " & chr(39) & "" & chr(39) & ", " & chr(39) & "" & chr(39) & ", " & chr(39) & "menubar=no;resizable=yes;toolbar=no;status=no;center=yes;dialogWidth=750px;dialogHeight=570px;" & chr(39) & ");} else {window.open(" & chr(39) & "frmTiposdeprocesos_det.aspx?_mode_=1&_closea_=1&isDlg=1&timestamp=" & chr(39) & " + new Date().getTime() + " & chr(39) & "" & chr(39) & ", " & chr(39) & "" & chr(39) & ", " & chr(39) & "modal=yes;directories=no;menubar=no;resizable=yes;toolbar=no;status=no;center=yes;dialogWidth=750px;dialogHeight=570px;" & chr(39) & ");};return false;" %>' PostBackUrl='<%# "javascript:if (window.showModalDialog) {window.showModalDialog(" & chr(39) & "frmTiposdeprocesos_det.aspx?_mode_=1&_closea_=1&isDlg=1&timestamp=" & chr(39) & " + new Date().getTime() + " & chr(39) & "" & chr(39) & ", " & chr(39) & "" & chr(39) & ", " & chr(39) & "menubar=no;resizable=yes;toolbar=no;status=no;center=yes;dialogWidth=750px;dialogHeight=570px;" & chr(39) & ");} else {window.open(" & chr(39) & "frmTiposdeprocesos_det.aspx?_mode_=1&_closea_=1&isDlg=1&timestamp=" & chr(39) & " + new Date().getTime() + " & chr(39) & "" & chr(39) & ", " & chr(39) & "" & chr(39) & ", " & chr(39) & "modal=yes;directories=no;menubar=no;resizable=yes;toolbar=no;status=no;center=yes;dialogWidth=750px;dialogHeight=570px;" & chr(39) & ");};return false;" %>' />
<asp:LinkButton runat="server" ID="insctrl000006_comborefresh" Text="Refrescar" CssClass="boton-acciones-subbutton" OnClick="insctrl000006_comborefresh_Click" CausesValidation="False" />
</ContentTemplate>
</asp:UpdatePanel>
<asp:UpdateProgress  runat="server" AssociatedUpdatePanelID="updupdinsctrl000006fs" >
<ProgressTemplate><span  class="ajaxupd" ><div style="position:absolute;top:2px;left:50%; border-color:#333333; border:1; border-style:solid; background-color:#FFFFFF;padding:3px;"><image src="imagenes/ajaxupdate.gif" border=0 />Actualizando</div></span></ProgressTemplate>
</asp:UpdateProgress>

</td>
</tr>
<tr>
<td  colspan="1" >

</td>
<td  colspan="1" >
<asp:SqlDataSource  runat="server" ID="dsinsctrl000005" CancelSelectOnNullParameter="False" onInit="dsinsctrl000005_Init" SelectCommandType="Text"
 SelectCommand="SELECT DOC_PRO.dsc,DOC_PRO.cod FROM DOC_PRO   WHERE (DOC_PRO.baja = 0 OR DOC_PRO.baja  IS NULL) ORDER BY DOC_PRO.orden" onselected="dsinsctrl000005_Selected" >
<SelectParameters>
</SelectParameters>
</asp:SqlDataSource>

</td>
</tr>
<tr>
<td  colspan="1" >
Dueño
</td>
<td  colspan="1" >
<asp:UpdatePanel  ID="updupdpnlupdinsctrl000005fs" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="True" >
<Triggers>
  <asp:AsyncPostBackTrigger  ControlID="insctrl000005delete" EventName="Click" />
</Triggers>
<ContentTemplate>
<asp:UpdatePanel  ID="updupdinsctrl000005fs" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="True" >
<ContentTemplate>
<asp:Button  runat="server" ID="cmdinsctrl000005showpanel" Text="..." CssClass="boton-acciones" CausesValidation="False" OnClick="cmdinsctrl000005showpanel_Click" />

<asp:ImageButton runat="server" ID="insctrl000005delete" ImageURL="~/imagenes/actdel.png" Width="16" Height="16" Tooltip="Quitar Dueño" Visible="False" OnClick="insctrl000005delete_Click" OnDataBinding="insctrl000005delete_DataBinding" CausesValidation="False" />
<asp:LinkButton runat="server" ID="insctrl000005view" CssClass="boton-acciones-subbutton" CausesValidation="False" />
<asp:HiddenField  runat="server" ID="insctrl000005"/>
<asp:HiddenField  runat="server" ID="insctrl000005value"/>
</ContentTemplate>
</asp:UpdatePanel>
<asp:UpdateProgress  runat="server" AssociatedUpdatePanelID="updupdinsctrl000005fs" >
<ProgressTemplate><span  class="ajaxupd" ><div style="position:absolute;top:2px;left:50%; border-color:#333333; border:1; border-style:solid; background-color:#FFFFFF;padding:3px;"><image src="imagenes/ajaxupdate.gif" border=0 />Actualizando</div></span></ProgressTemplate>
</asp:UpdateProgress>
</ContentTemplate>
</asp:UpdatePanel>
<asp:UpdateProgress  runat="server" AssociatedUpdatePanelID="updupdpnlupdinsctrl000005fs" >
<ProgressTemplate><span  class="ajaxupd" ><div style="position:absolute;top:2px;left:50%; border-color:#333333; border:1; border-style:solid; background-color:#FFFFFF;padding:3px;"><image src="imagenes/ajaxupdate.gif" border=0 />Actualizando</div></span></ProgressTemplate>
</asp:UpdateProgress>

</td>
</tr>
</table>
<!-- End Plantilla insercion --></InsertItemTemplate>

</asp:FormView>

			<asp:SqlDataSource  runat="server" ID="dsdatos" ProviderName="System.Data.SqlClient" CancelSelectOnNullParameter="False" onInit="dsdatos_Init" SelectCommandType="Text"
 SelectCommand="SELECT DOC_PRO.cod,DOC_PRO.dsc,(DOC_PROAPACOD.dsc) as apacoddsc,DOC_PRO.ownertype,CASE DOC_PRO.ownertype WHEN 9 THEN (SELECT TOP 1 DOC_PROEMP.empcod FROM DOC_PROEMP WHERE DOC_PROEMP.procod=DOC_PRO.cod) WHEN 10 THEN (SELECT TOP 1 DOC_PROUND.undcod FROM DOC_PROUND WHERE DOC_PROUND.procod=DOC_PRO.cod) END as ownertypevalue,CASE DOC_PRO.ownertype WHEN 9 THEN (SELECT TOP 1 EMP.dsc FROM EMP WHERE EMP.cod= (SELECT TOP 1 DOC_PROEMP.empcod FROM DOC_PROEMP WHERE DOC_PROEMP.procod=DOC_PRO.cod)) WHEN 10 THEN (SELECT TOP 1 UND.dsc FROM UND WHERE UND.cod= (SELECT TOP 1 DOC_PROUND.undcod FROM DOC_PROUND WHERE DOC_PROUND.procod=DOC_PRO.cod)) END as ownertypedsc,DOC_PRO.baja,DOC_PRO.orden,(DOC_PROQSECSID.secdsc) as qsecsiddsc,DOC_PRO.qsecdatetime,DOC_PRO.apacod,DOC_PRO.qsecsid FROM DOC_PRO  LEFT JOIN DOC_APA AS DOC_PROAPACOD ON DOC_PROAPACOD.cod=DOC_PRO.apacod LEFT JOIN Q_SECPLOGIN AS DOC_PROQSECSID ON DOC_PROQSECSID.sidcod=DOC_PRO.qsecsid  WHERE (DOC_PRO.cod = @param1) AND (DOC_PRO.baja = 0 OR DOC_PRO.baja  IS NULL)" onselected="dsdatos_Selected" UpdateCommandType="Text"
 UpdateCommand="UPDATE DOC_PRO SET dsc=@dsc,apacod=@apacod,ownertype=@ownertype,qsecsid=@qsecsid,qsecdatetime=getdate() WHERE DOC_PRO.cod = @param1" onupdated="dsdatos_Updated" InsertCommandType="Text"
 InsertCommand="DECLARE @querynextcodcod int  SET @querynextcodcod =(SELECT ISNULL(MAX(cod),0)+1 FROM DOC_PRO WHERE cod > 0 ) INSERT INTO DOC_PRO (cod,dsc,apacod,ownertype,baja,qsecsid,qsecdatetime) VALUES(@querynextcodcod,@dsc,@apacod,@ownertype,NULL,@qsecsid,getdate()) SELECT @querynextcod =@querynextcodcod" onInserted="dsdatos_Inserted" DeleteCommandType="Text"
 DeleteCommand="UPDATE DOC_PRO SET baja=1 WHERE cod = @param1" ondeleted="dsdatos_Deleted" >
<InsertParameters>
<asp:Parameter  Name="querynextcod" Direction="Output" Type="Int32" />
<asp:ControlParameter  Name="dsc" ControlID="frmdatos$insctrl000007" ConvertEmptyStringToNull="True" Direction="InputOutput" Type="String" Size="50" PropertyName="Text" />
<asp:ControlParameter  Name="apacod" ControlID="frmdatos$insctrl000006" ConvertEmptyStringToNull="True" Direction="InputOutput" Type="Int32" PropertyName="SelectedValue" />
<asp:ControlParameter  Name="ownertype" ControlID="frmdatos$insctrl000005" ConvertEmptyStringToNull="True" Direction="InputOutput" Type="Int32" PropertyName="Value" />
<asp:SessionParameter  Name="qsecsid" SessionField="secsid" Direction="Input" />
<asp:Parameter  Name="param1" Direction="ReturnValue" />
</InsertParameters>
<SelectParameters>
<asp:QueryStringParameter  Name="param1" QueryStringField="param1" ConvertEmptyStringToNull="True" Type="String" Size="200" />
</SelectParameters>
<UpdateParameters>
<asp:QueryStringParameter  Name="param1" QueryStringField="param1" ConvertEmptyStringToNull="True" Type="String" Size="200" />
<asp:ControlParameter  Name="dsc" ControlID="frmdatos$editctrl000007" ConvertEmptyStringToNull="True" Direction="InputOutput" Type="String" Size="50" PropertyName="Text" />
<asp:ControlParameter  Name="apacod" ControlID="frmdatos$editctrl000006" ConvertEmptyStringToNull="True" Direction="InputOutput" Type="Int32" PropertyName="SelectedValue" />
<asp:ControlParameter  Name="ownertype" ControlID="frmdatos$editctrl000005" ConvertEmptyStringToNull="True" Direction="InputOutput" Type="Int32" PropertyName="Value" />
<asp:SessionParameter  Name="qsecsid" SessionField="secsid" Direction="Input" />
</UpdateParameters>
<DeleteParameters>
<asp:QueryStringParameter  Name="param1" QueryStringField="param1" ConvertEmptyStringToNull="True" Type="String" Size="200" />
<asp:SessionParameter  Name="qsecsid" SessionField="secsid" Direction="Input" />
</DeleteParameters>
</asp:SqlDataSource>
</table>
<!-- End -->
</td></tr></table>
<div  >
<!-- Panel pnlobjectexplorer -->
<asp:Label  runat="server" ID="lblpnlpnlobjectexplorer" width="100%" />
<ajaxkit:ModalPopupExtender  runat="server" ID="pnlobjectexplorer" DropShadow="False" EnableViewState="True" PopupControlID="pnlpnlobjectexplorer" TargetControlID="lblpnlpnlobjectexplorer" BackgroundCssClass="bgmodalpopup" />
<asp:Panel  ID="pnlpnlobjectexplorer" runat="server" BorderWidth="1" BorderStyle="solid" >
<asp:UpdatePanel  ID="updpnlobjectexplorer" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="True" >
<Triggers>
  <asp:AsyncPostBackTrigger  ControlID="tvwmodalpopuppnlobjectexplorer" EventName="SelectedNodeChanged" />
  <asp:AsyncPostBackTrigger  ControlID="grdobjectexplorer" EventName="RowCommand" />
</Triggers>
<ContentTemplate>
<table class="form" cellpadding="0" cellspacing="0"><tr><td  colspan="1" >
<table style="width:700px;height:300px; border: 2px solid #000000; padding: 2px; margin: 1px; background-color: #F0F0F0; vertical-align: top; text-align: left;">
<tr><td  class="tabla-titulo" colspan="2" >Seleccionar</td></tr>
<tr>
<td  class="search-title" style="height:15px;width:700px" colspan="2" >
<asp:Button  runat="server" ID="cmdobjectexplorercancel" Text="Cancelar" CssClass="boton-acciones" CausesValidation="False" OnClick="cmdobjectexplorercancel_Click" />
<asp:Label  runat="server" ID="lblobjectexplorerfilter" width="100" Text='Filtro rápido:' />
<asp:TextBox  runat="server" ID="txtobjectexplorerfilter" CssClass="form-control" Width="200px" MaxLength="20" />
<asp:CompareValidator  runat="server" ID="vcdvaltxtobjectexplorerfilter" SetFocusOnError="true" CssClass="error" ControltoValidate="txtobjectexplorerfilter" Display="Dynamic" Text="Ingrese un texto" ErrorMessage=":Ingrese un texto" Type="String" Operator="DataTypeCheck" />
<asp:RegularExpressionValidator  runat="server" ID="vrgvaltxtobjectexplorerfilter" SetFocusOnError="true" CssClass="error" Display="Dynamic" ControltoValidate="txtobjectexplorerfilter" ValidationExpression="[\w\s-+/*^()!|°$%&=-_,:;.ñÑ~áéíóúÁÉÍÓÚ#}{¿]{0,20}" Text="No mayor a 20 caracteres. Deben ser letras o numeros." ErrorMessage=":No mayor a 20 caracteres. Deben ser letras o numeros." />
<ajaxkit:ValidatorCalloutExtender  runat="server" ID="ajaxvrgtxtobjectexplorerfilter" TargetControlID="vrgvaltxtobjectexplorerfilter" />

<asp:Button  runat="server" ID="cmdobjectexplorerfilter" Text="Buscar" CssClass="boton-acciones" CausesValidation="False" OnClick="cmdobjectexplorerfilter_Click" />
</td>
</tr>
<tr>
<td  style="vertical-align:top; text-align:left;width:200px;height:315px;background-color:#F0F0F0;" colspan="1" >
<div style="height:315px;overflow-y:scroll;">
<asp:TreeView  runat="server" ID="tvwmodalpopuppnlobjectexplorer" ShowLines="True" AutoGenerateDataBindings="False" Width="200" NodeWrap="True" NodeIndent="5" ForeColor="Black" SelectedNodeStyle-BackColor="#C0C0C0" SelectedNodeStyle-CssClass="objectexplorer-selectnode" OnSelectedNodeChanged="tvwmodalpopuppnlobjectexplorer_SelectedNodeChanged" />
</div>
</td>
<td  style="vertical-align:top; text-align:left;width:450px;height:315px;" colspan="1" >
<div style="height:315px;width:450px; overflow-x:scroll;">
<asp:GridView  runat="server" ID="grdobjectexplorer" AllowPaging="True" AllowSorting="True" AutoGenerateColumns="False" PageSize="9"
 GridLines="None" ShowHeader="False" ShowFooter="False" Width="100%" UseAccessibleHeader="False" DataKeyNames="cod,objecttype" CellPadding="2" onRowCreated="grdobjectexplorer_RowCreated" onRowCommand="grdobjectexplorer_RowCommand" OnSelectedIndexChanged="grdobjectexplorer_SelectedIndexChanged" OnPageIndexChanging="grdobjectexplorer_PageIndexChanging" >
<FooterStyle  CssClass="tabla-footer" />
<RowStyle  CssClass="tabla-fila" />
<AlternatingRowStyle  CssClass="tabla-fila-alternativa" />
<SelectedRowStyle  CssClass="tabla-fila" />
<PagerStyle  CssClass="tabla-pager" />
<HeaderStyle  CssClass="tabla-titulo" ForeColor="White" />
<EmptyDataTemplate>
<p  class="tabla-fila"></p>
</EmptyDataTemplate>
<Columns>
<asp:TemplateField   AccessibleHeaderText="Seleccionar" HeaderText="Seleccionar" ItemStyle-CssClass="tabla-celda" HeaderStyle-CssClass="tabla-titulo" FooterStyle-CssClass="tabla-celda">
<ItemStyle  HorizontalAlign="Left" Width="50px" CssClass="searchresult-table-cell" />
<ItemTemplate>
<asp:Button  runat="server" ID="grdobjectexplorerselectrow" Text="Seleccionar" CssClass="boton-acciones" CommandName="Select" CausesValidation="False" /></ItemTemplate>
</asp:TemplateField>
<asp:TemplateField   ItemStyle-CssClass="tabla-celda" HeaderStyle-CssClass="tabla-titulo" FooterStyle-CssClass="tabla-celda">
<ItemStyle  HorizontalAlign="Left" Width="12px" CssClass="searchresult-table-cell" />
<ItemTemplate>
<asp:Image  runat="server" ID="grdobjectexplorerrowimageimg" Width="12px" GenerateEmptyAlternateText="True" ImageURL='<%# "~/imagenes/icon" & format(Eval("objecttype"),"00000000") & ".png" %>' BorderColor="LightGray" BorderWidth="1" BorderStyle="Solid" AlternateText="Imagen no disp." />
</ItemTemplate>
</asp:TemplateField>
<asp:TemplateField   ItemStyle-CssClass="tabla-celda" HeaderStyle-CssClass="tabla-titulo" FooterStyle-CssClass="tabla-celda">
<ItemStyle  HorizontalAlign="Left" />
<ItemTemplate>
<asp:LinkButton runat="server" ID="grdobjectexplorerrowlabel" Text='<%# Eval("dsc") %>' CssClass="boton-acciones" CommandName="Select" CausesValidation="False" /></ItemTemplate>
</asp:TemplateField>
</Columns>
</asp:GridView>

</div>
</td>
</tr>
</table>
</td></tr></table></ContentTemplate>
</asp:UpdatePanel>
</asp:Panel>
<asp:UpdateProgress  runat="server" AssociatedUpdatePanelID="updpnlobjectexplorer" >
<ProgressTemplate><span  class="ajaxupd" ><div style="position:absolute;top:2px;left:50%; border-color:#333333; border:1; border-style:solid; background-color:#FFFFFF;padding:3px;"><image src="imagenes/ajaxupdate.gif" border=0 />Actualizando</div></span></ProgressTemplate>
</asp:UpdateProgress>
<!-- Fin panel pnlobjectexplorer -->


</div>
</asp:Content>

