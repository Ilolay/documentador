<%@ Page Language="VB" CodeFile="frmUnidades_det.aspx.vb" MasterPageFile="general.master" AutoEventWireup="True" Inherits="frmUnidades_det" Title="Unidades" %>
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
<tr><td  style="width:32px;" colspan="1" ><img  src="imagenes/icon00000010.png" width="32px" height="32px" alt="Unidades" hspace="4"/></td>
<td  style="text-align:left;" colspan="1" ><span class="form-title">Unidades</span> |<asp:Label  runat="server" ID="lblsubtitle" CssClass="form-subtitle" />
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
Codigo
</td>
<td  colspan="1" >
<asp:Label  runat="server" ID="itemctrl000021" CssClass="form-control-read" width="100%" OnDataBinding="itemctrl000021_Databound" />
</td>
</tr>
<tr>
<td  colspan="2" >
<ajaxkit:TabContainer ID="itemtabPanel" runat="server" >
<ajaxkit:TabPanel ID="itemtabPanel001" runat="server" HeaderText="General" Tooltip="General" >
<ContentTemplate>
<table>
<tr>
<td  colspan="1" >
Descripción
</td>
<td  colspan="1" >
<asp:Label  runat="server" ID="itemctrl000019" CssClass="form-control-read" width="100%" OnDataBinding="itemctrl000019_Databound" />
</td>
</tr>
<tr>
<td  colspan="1" >
Tipo de unidad
</td>
<td  colspan="1" >
<asp:LinkButton runat="server" ID="itemctrl000018view" Text='<%# Eval("undtipcoddsc") %>' CssClass="form-control-read" OnDataBinding="itemctrl000018view_DataBinding" CausesValidation="False" />

</td>
</tr>
<tr>
<td  colspan="1" >
Responsable de la unidad
</td>
<td  colspan="1" >
<asp:LinkButton runat="server" ID="itemctrl000017view" Text='<%# Eval("respdsc") %>' CssClass="form-control-read" OnDataBinding="itemctrl000017view_DataBinding" CausesValidation="False" OnClientClick='<%# "javascript:if (window.showModalDialog) {window.showModalDialog(" & chr(39) & "frmColaboradores_det.aspx?_mode_=0&_closea_=1&isDlg=1&param1=" & Eval("resp") & "&timestamp=" & chr(39) & " + new Date().getTime() + " & chr(39) & "" & chr(39) & ", " & chr(39) & "" & chr(39) & ", " & chr(39) & "menubar=no;resizable=yes;toolbar=no;status=no;center=yes;dialogWidth=750px;dialogHeight=570px;" & chr(39) & ");} else {window.open(" & chr(39) & "frmColaboradores_det.aspx?_mode_=0&_closea_=1&isDlg=1&param1=" & Eval("resp") & "&timestamp=" & chr(39) & " + new Date().getTime() + " & chr(39) & "" & chr(39) & ", " & chr(39) & "" & chr(39) & ", " & chr(39) & "modal=yes;directories=no;menubar=no;resizable=yes;toolbar=no;status=no;center=yes;dialogWidth=750px;dialogHeight=570px;" & chr(39) & ");};return false;" %>' PostBackUrl='<%# "javascript:if (window.showModalDialog) {window.showModalDialog(" & chr(39) & "frmColaboradores_det.aspx?_mode_=0&_closea_=1&isDlg=1&param1=" & Eval("resp") & "&timestamp=" & chr(39) & " + new Date().getTime() + " & chr(39) & "" & chr(39) & ", " & chr(39) & "" & chr(39) & ", " & chr(39) & "menubar=no;resizable=yes;toolbar=no;status=no;center=yes;dialogWidth=750px;dialogHeight=570px;" & chr(39) & ");} else {window.open(" & chr(39) & "frmColaboradores_det.aspx?_mode_=0&_closea_=1&isDlg=1&param1=" & Eval("resp") & "&timestamp=" & chr(39) & " + new Date().getTime() + " & chr(39) & "" & chr(39) & ", " & chr(39) & "" & chr(39) & ", " & chr(39) & "modal=yes;directories=no;menubar=no;resizable=yes;toolbar=no;status=no;center=yes;dialogWidth=750px;dialogHeight=570px;" & chr(39) & ");};return false;" %>' />
<asp:HiddenField  runat="server" ID="itemctrl000017type"/>
<asp:HiddenField  runat="server" ID="itemctrl000017" OnDataBinding="itemctrl000017_DataBound"/>

</td>
</tr>
<tr>
<td  colspan="1" >
Unidad superior en jerarquía
</td>
<td  colspan="1" >
<asp:LinkButton runat="server" ID="itemctrl000016view" Text='<%# Eval("undcodsupdsc") %>' CssClass="form-control-read" OnDataBinding="itemctrl000016view_DataBinding" CausesValidation="False" OnClientClick='<%# "javascript:if (window.showModalDialog) {window.showModalDialog(" & chr(39) & "frmUnidades_det.aspx?_mode_=0&_closea_=1&isDlg=1&param1=" & Eval("undcodsup") & "&timestamp=" & chr(39) & " + new Date().getTime() + " & chr(39) & "" & chr(39) & ", " & chr(39) & "" & chr(39) & ", " & chr(39) & "menubar=no;resizable=yes;toolbar=no;status=no;center=yes;dialogWidth=750px;dialogHeight=570px;" & chr(39) & ");} else {window.open(" & chr(39) & "frmUnidades_det.aspx?_mode_=0&_closea_=1&isDlg=1&param1=" & Eval("undcodsup") & "&timestamp=" & chr(39) & " + new Date().getTime() + " & chr(39) & "" & chr(39) & ", " & chr(39) & "" & chr(39) & ", " & chr(39) & "modal=yes;directories=no;menubar=no;resizable=yes;toolbar=no;status=no;center=yes;dialogWidth=750px;dialogHeight=570px;" & chr(39) & ");};return false;" %>' PostBackUrl='<%# "javascript:if (window.showModalDialog) {window.showModalDialog(" & chr(39) & "frmUnidades_det.aspx?_mode_=0&_closea_=1&isDlg=1&param1=" & Eval("undcodsup") & "&timestamp=" & chr(39) & " + new Date().getTime() + " & chr(39) & "" & chr(39) & ", " & chr(39) & "" & chr(39) & ", " & chr(39) & "menubar=no;resizable=yes;toolbar=no;status=no;center=yes;dialogWidth=750px;dialogHeight=570px;" & chr(39) & ");} else {window.open(" & chr(39) & "frmUnidades_det.aspx?_mode_=0&_closea_=1&isDlg=1&param1=" & Eval("undcodsup") & "&timestamp=" & chr(39) & " + new Date().getTime() + " & chr(39) & "" & chr(39) & ", " & chr(39) & "" & chr(39) & ", " & chr(39) & "modal=yes;directories=no;menubar=no;resizable=yes;toolbar=no;status=no;center=yes;dialogWidth=750px;dialogHeight=570px;" & chr(39) & ");};return false;" %>' />
<asp:HiddenField  runat="server" ID="itemctrl000016type"/>
<asp:HiddenField  runat="server" ID="itemctrl000016" OnDataBinding="itemctrl000016_DataBound"/>

</td>
</tr>
<tr>
<td  colspan="1" >
Abreviatura
</td>
<td  colspan="1" >
<asp:Label  runat="server" ID="itemctrl000015" CssClass="form-control-read" width="100%" OnDataBinding="itemctrl000015_Databound" />
</td>
</tr>
<tr>
<td  colspan="1" >
Identificador interno
</td>
<td  colspan="1" >
<asp:Label  runat="server" ID="itemctrl000014" CssClass="form-control-read" width="100%" OnDataBinding="itemctrl000014_Databound" />
</td>
</tr>
<tr>
<td  colspan="1" >
Orden
</td>
<td  colspan="1" >
<asp:Label  runat="server" ID="itemctrl000013" CssClass="form-control-read" width="100%" OnDataBinding="itemctrl000013_Databound" />
</td>
</tr>
<tr>
<td  colspan="1" >
Grupo de miembros
</td>
<td  colspan="1" >
<asp:LinkButton runat="server" ID="itemctrl000012view" Text='<%# Eval("miembrosgrpcoddsc") %>' CssClass="form-control-read" OnDataBinding="itemctrl000012view_DataBinding" CausesValidation="False" OnClientClick='<%# "javascript:if (window.showModalDialog) {window.showModalDialog(" & chr(39) & "frmGrupos_det.aspx?_mode_=0&_closea_=1&isDlg=1&param1=" & Eval("miembrosgrpcod") & "&timestamp=" & chr(39) & " + new Date().getTime() + " & chr(39) & "" & chr(39) & ", " & chr(39) & "" & chr(39) & ", " & chr(39) & "menubar=no;resizable=yes;toolbar=no;status=no;center=yes;dialogWidth=750px;dialogHeight=570px;" & chr(39) & ");} else {window.open(" & chr(39) & "frmGrupos_det.aspx?_mode_=0&_closea_=1&isDlg=1&param1=" & Eval("miembrosgrpcod") & "&timestamp=" & chr(39) & " + new Date().getTime() + " & chr(39) & "" & chr(39) & ", " & chr(39) & "" & chr(39) & ", " & chr(39) & "modal=yes;directories=no;menubar=no;resizable=yes;toolbar=no;status=no;center=yes;dialogWidth=750px;dialogHeight=570px;" & chr(39) & ");};return false;" %>' PostBackUrl='<%# "javascript:if (window.showModalDialog) {window.showModalDialog(" & chr(39) & "frmGrupos_det.aspx?_mode_=0&_closea_=1&isDlg=1&param1=" & Eval("miembrosgrpcod") & "&timestamp=" & chr(39) & " + new Date().getTime() + " & chr(39) & "" & chr(39) & ", " & chr(39) & "" & chr(39) & ", " & chr(39) & "menubar=no;resizable=yes;toolbar=no;status=no;center=yes;dialogWidth=750px;dialogHeight=570px;" & chr(39) & ");} else {window.open(" & chr(39) & "frmGrupos_det.aspx?_mode_=0&_closea_=1&isDlg=1&param1=" & Eval("miembrosgrpcod") & "&timestamp=" & chr(39) & " + new Date().getTime() + " & chr(39) & "" & chr(39) & ", " & chr(39) & "" & chr(39) & ", " & chr(39) & "modal=yes;directories=no;menubar=no;resizable=yes;toolbar=no;status=no;center=yes;dialogWidth=750px;dialogHeight=570px;" & chr(39) & ");};return false;" %>' />
<asp:HiddenField  runat="server" ID="itemctrl000012type"/>
<asp:HiddenField  runat="server" ID="itemctrl000012" OnDataBinding="itemctrl000012_DataBound"/>

</td>
</tr>
<tr>
<td  colspan="1" >
Grupo de responsables
</td>
<td  colspan="1" >
<asp:LinkButton runat="server" ID="itemctrl000011view" Text='<%# Eval("grpcodrespdsc") %>' CssClass="form-control-read" OnDataBinding="itemctrl000011view_DataBinding" CausesValidation="False" OnClientClick='<%# "javascript:if (window.showModalDialog) {window.showModalDialog(" & chr(39) & "frmGrupos_det.aspx?_mode_=0&_closea_=1&isDlg=1&param1=" & Eval("grpcodresp") & "&timestamp=" & chr(39) & " + new Date().getTime() + " & chr(39) & "" & chr(39) & ", " & chr(39) & "" & chr(39) & ", " & chr(39) & "menubar=no;resizable=yes;toolbar=no;status=no;center=yes;dialogWidth=750px;dialogHeight=570px;" & chr(39) & ");} else {window.open(" & chr(39) & "frmGrupos_det.aspx?_mode_=0&_closea_=1&isDlg=1&param1=" & Eval("grpcodresp") & "&timestamp=" & chr(39) & " + new Date().getTime() + " & chr(39) & "" & chr(39) & ", " & chr(39) & "" & chr(39) & ", " & chr(39) & "modal=yes;directories=no;menubar=no;resizable=yes;toolbar=no;status=no;center=yes;dialogWidth=750px;dialogHeight=570px;" & chr(39) & ");};return false;" %>' PostBackUrl='<%# "javascript:if (window.showModalDialog) {window.showModalDialog(" & chr(39) & "frmGrupos_det.aspx?_mode_=0&_closea_=1&isDlg=1&param1=" & Eval("grpcodresp") & "&timestamp=" & chr(39) & " + new Date().getTime() + " & chr(39) & "" & chr(39) & ", " & chr(39) & "" & chr(39) & ", " & chr(39) & "menubar=no;resizable=yes;toolbar=no;status=no;center=yes;dialogWidth=750px;dialogHeight=570px;" & chr(39) & ");} else {window.open(" & chr(39) & "frmGrupos_det.aspx?_mode_=0&_closea_=1&isDlg=1&param1=" & Eval("grpcodresp") & "&timestamp=" & chr(39) & " + new Date().getTime() + " & chr(39) & "" & chr(39) & ", " & chr(39) & "" & chr(39) & ", " & chr(39) & "modal=yes;directories=no;menubar=no;resizable=yes;toolbar=no;status=no;center=yes;dialogWidth=750px;dialogHeight=570px;" & chr(39) & ");};return false;" %>' />
<asp:HiddenField  runat="server" ID="itemctrl000011type"/>
<asp:HiddenField  runat="server" ID="itemctrl000011" OnDataBinding="itemctrl000011_DataBound"/>

</td>
</tr>
<tr>
<td  colspan="1" >
Grupo de superiores
</td>
<td  colspan="1" >
<asp:LinkButton runat="server" ID="itemctrl000010view" Text='<%# Eval("grpcodprjverdsc") %>' CssClass="form-control-read" OnDataBinding="itemctrl000010view_DataBinding" CausesValidation="False" OnClientClick='<%# "javascript:if (window.showModalDialog) {window.showModalDialog(" & chr(39) & "frmGrupos_det.aspx?_mode_=0&_closea_=1&isDlg=1&param1=" & Eval("grpcodprjver") & "&timestamp=" & chr(39) & " + new Date().getTime() + " & chr(39) & "" & chr(39) & ", " & chr(39) & "" & chr(39) & ", " & chr(39) & "menubar=no;resizable=yes;toolbar=no;status=no;center=yes;dialogWidth=750px;dialogHeight=570px;" & chr(39) & ");} else {window.open(" & chr(39) & "frmGrupos_det.aspx?_mode_=0&_closea_=1&isDlg=1&param1=" & Eval("grpcodprjver") & "&timestamp=" & chr(39) & " + new Date().getTime() + " & chr(39) & "" & chr(39) & ", " & chr(39) & "" & chr(39) & ", " & chr(39) & "modal=yes;directories=no;menubar=no;resizable=yes;toolbar=no;status=no;center=yes;dialogWidth=750px;dialogHeight=570px;" & chr(39) & ");};return false;" %>' PostBackUrl='<%# "javascript:if (window.showModalDialog) {window.showModalDialog(" & chr(39) & "frmGrupos_det.aspx?_mode_=0&_closea_=1&isDlg=1&param1=" & Eval("grpcodprjver") & "&timestamp=" & chr(39) & " + new Date().getTime() + " & chr(39) & "" & chr(39) & ", " & chr(39) & "" & chr(39) & ", " & chr(39) & "menubar=no;resizable=yes;toolbar=no;status=no;center=yes;dialogWidth=750px;dialogHeight=570px;" & chr(39) & ");} else {window.open(" & chr(39) & "frmGrupos_det.aspx?_mode_=0&_closea_=1&isDlg=1&param1=" & Eval("grpcodprjver") & "&timestamp=" & chr(39) & " + new Date().getTime() + " & chr(39) & "" & chr(39) & ", " & chr(39) & "" & chr(39) & ", " & chr(39) & "modal=yes;directories=no;menubar=no;resizable=yes;toolbar=no;status=no;center=yes;dialogWidth=750px;dialogHeight=570px;" & chr(39) & ");};return false;" %>' />
<asp:HiddenField  runat="server" ID="itemctrl000010type"/>
<asp:HiddenField  runat="server" ID="itemctrl000010" OnDataBinding="itemctrl000010_DataBound"/>

</td>
</tr>
<tr>
<td  colspan="1" >
Grupo de miembros directos
</td>
<td  colspan="1" >
<asp:LinkButton runat="server" ID="itemctrl000009view" Text='<%# Eval("grpcodmbrdirdsc") %>' CssClass="form-control-read" OnDataBinding="itemctrl000009view_DataBinding" CausesValidation="False" OnClientClick='<%# "javascript:if (window.showModalDialog) {window.showModalDialog(" & chr(39) & "frmGrupos_det.aspx?_mode_=0&_closea_=1&isDlg=1&param1=" & Eval("grpcodmbrdir") & "&timestamp=" & chr(39) & " + new Date().getTime() + " & chr(39) & "" & chr(39) & ", " & chr(39) & "" & chr(39) & ", " & chr(39) & "menubar=no;resizable=yes;toolbar=no;status=no;center=yes;dialogWidth=750px;dialogHeight=570px;" & chr(39) & ");} else {window.open(" & chr(39) & "frmGrupos_det.aspx?_mode_=0&_closea_=1&isDlg=1&param1=" & Eval("grpcodmbrdir") & "&timestamp=" & chr(39) & " + new Date().getTime() + " & chr(39) & "" & chr(39) & ", " & chr(39) & "" & chr(39) & ", " & chr(39) & "modal=yes;directories=no;menubar=no;resizable=yes;toolbar=no;status=no;center=yes;dialogWidth=750px;dialogHeight=570px;" & chr(39) & ");};return false;" %>' PostBackUrl='<%# "javascript:if (window.showModalDialog) {window.showModalDialog(" & chr(39) & "frmGrupos_det.aspx?_mode_=0&_closea_=1&isDlg=1&param1=" & Eval("grpcodmbrdir") & "&timestamp=" & chr(39) & " + new Date().getTime() + " & chr(39) & "" & chr(39) & ", " & chr(39) & "" & chr(39) & ", " & chr(39) & "menubar=no;resizable=yes;toolbar=no;status=no;center=yes;dialogWidth=750px;dialogHeight=570px;" & chr(39) & ");} else {window.open(" & chr(39) & "frmGrupos_det.aspx?_mode_=0&_closea_=1&isDlg=1&param1=" & Eval("grpcodmbrdir") & "&timestamp=" & chr(39) & " + new Date().getTime() + " & chr(39) & "" & chr(39) & ", " & chr(39) & "" & chr(39) & ", " & chr(39) & "modal=yes;directories=no;menubar=no;resizable=yes;toolbar=no;status=no;center=yes;dialogWidth=750px;dialogHeight=570px;" & chr(39) & ");};return false;" %>' />
<asp:HiddenField  runat="server" ID="itemctrl000009type"/>
<asp:HiddenField  runat="server" ID="itemctrl000009" OnDataBinding="itemctrl000009_DataBound"/>

</td>
</tr>
</table>
</ContentTemplate>
</ajaxkit:TabPanel>
<ajaxkit:TabPanel ID="itemtabPanel002" runat="server" HeaderText="Documentador" Tooltip="Documentador" >
<ContentTemplate>
<table>
<tr>
<td  colspan="1" >
Miembros directos
</td>
<td  colspan="1" >
<asp:Label  runat="server" ID="itemctrl000008" CssClass="form-control-read" width="100%" OnDataBinding="itemctrl000008_Databound" />
</td>
</tr>
<tr>
<td  colspan="1" >
Formato especifico
</td>
<td  colspan="1" >
<asp:Label  runat="server" ID="itemctrl000007" CssClass="form-control-read" width="100%" OnDataBinding="itemctrl000007_Databound" />
</td>
</tr>
<tr>
<td  colspan="1" >
Editor
</td>
<td  colspan="1" >
<asp:LinkButton runat="server" ID="itemctrl000006view" Text='<%# Eval("editordsc") %>' CssClass="form-control-read" OnDataBinding="itemctrl000006view_DataBinding" CausesValidation="False" OnClientClick='<%# "javascript:if (window.showModalDialog) {window.showModalDialog(" & chr(39) & "frmColaboradores_det.aspx?_mode_=0&_closea_=1&isDlg=1&param1=" & Eval("editor") & "&timestamp=" & chr(39) & " + new Date().getTime() + " & chr(39) & "" & chr(39) & ", " & chr(39) & "" & chr(39) & ", " & chr(39) & "menubar=no;resizable=yes;toolbar=no;status=no;center=yes;dialogWidth=750px;dialogHeight=570px;" & chr(39) & ");} else {window.open(" & chr(39) & "frmColaboradores_det.aspx?_mode_=0&_closea_=1&isDlg=1&param1=" & Eval("editor") & "&timestamp=" & chr(39) & " + new Date().getTime() + " & chr(39) & "" & chr(39) & ", " & chr(39) & "" & chr(39) & ", " & chr(39) & "modal=yes;directories=no;menubar=no;resizable=yes;toolbar=no;status=no;center=yes;dialogWidth=750px;dialogHeight=570px;" & chr(39) & ");};return false;" %>' PostBackUrl='<%# "javascript:if (window.showModalDialog) {window.showModalDialog(" & chr(39) & "frmColaboradores_det.aspx?_mode_=0&_closea_=1&isDlg=1&param1=" & Eval("editor") & "&timestamp=" & chr(39) & " + new Date().getTime() + " & chr(39) & "" & chr(39) & ", " & chr(39) & "" & chr(39) & ", " & chr(39) & "menubar=no;resizable=yes;toolbar=no;status=no;center=yes;dialogWidth=750px;dialogHeight=570px;" & chr(39) & ");} else {window.open(" & chr(39) & "frmColaboradores_det.aspx?_mode_=0&_closea_=1&isDlg=1&param1=" & Eval("editor") & "&timestamp=" & chr(39) & " + new Date().getTime() + " & chr(39) & "" & chr(39) & ", " & chr(39) & "" & chr(39) & ", " & chr(39) & "modal=yes;directories=no;menubar=no;resizable=yes;toolbar=no;status=no;center=yes;dialogWidth=750px;dialogHeight=570px;" & chr(39) & ");};return false;" %>' />
<asp:HiddenField  runat="server" ID="itemctrl000006type"/>
<asp:HiddenField  runat="server" ID="itemctrl000006" OnDataBinding="itemctrl000006_DataBound"/>

</td>
</tr>
<tr>
<td  colspan="1" >
Grupo de editores
</td>
<td  colspan="1" >
<asp:LinkButton runat="server" ID="itemctrl000005view" Text='<%# Eval("grpcodeditordsc") %>' CssClass="form-control-read" OnDataBinding="itemctrl000005view_DataBinding" CausesValidation="False" OnClientClick='<%# "javascript:if (window.showModalDialog) {window.showModalDialog(" & chr(39) & "frmGrupos_det.aspx?_mode_=0&_closea_=1&isDlg=1&param1=" & Eval("grpcodeditor") & "&timestamp=" & chr(39) & " + new Date().getTime() + " & chr(39) & "" & chr(39) & ", " & chr(39) & "" & chr(39) & ", " & chr(39) & "menubar=no;resizable=yes;toolbar=no;status=no;center=yes;dialogWidth=750px;dialogHeight=570px;" & chr(39) & ");} else {window.open(" & chr(39) & "frmGrupos_det.aspx?_mode_=0&_closea_=1&isDlg=1&param1=" & Eval("grpcodeditor") & "&timestamp=" & chr(39) & " + new Date().getTime() + " & chr(39) & "" & chr(39) & ", " & chr(39) & "" & chr(39) & ", " & chr(39) & "modal=yes;directories=no;menubar=no;resizable=yes;toolbar=no;status=no;center=yes;dialogWidth=750px;dialogHeight=570px;" & chr(39) & ");};return false;" %>' PostBackUrl='<%# "javascript:if (window.showModalDialog) {window.showModalDialog(" & chr(39) & "frmGrupos_det.aspx?_mode_=0&_closea_=1&isDlg=1&param1=" & Eval("grpcodeditor") & "&timestamp=" & chr(39) & " + new Date().getTime() + " & chr(39) & "" & chr(39) & ", " & chr(39) & "" & chr(39) & ", " & chr(39) & "menubar=no;resizable=yes;toolbar=no;status=no;center=yes;dialogWidth=750px;dialogHeight=570px;" & chr(39) & ");} else {window.open(" & chr(39) & "frmGrupos_det.aspx?_mode_=0&_closea_=1&isDlg=1&param1=" & Eval("grpcodeditor") & "&timestamp=" & chr(39) & " + new Date().getTime() + " & chr(39) & "" & chr(39) & ", " & chr(39) & "" & chr(39) & ", " & chr(39) & "modal=yes;directories=no;menubar=no;resizable=yes;toolbar=no;status=no;center=yes;dialogWidth=750px;dialogHeight=570px;" & chr(39) & ");};return false;" %>' />
<asp:HiddenField  runat="server" ID="itemctrl000005type"/>
<asp:HiddenField  runat="server" ID="itemctrl000005" OnDataBinding="itemctrl000005_DataBound"/>

</td>
</tr>
</table>
</ContentTemplate>
</ajaxkit:TabPanel>
<ajaxkit:TabPanel ID="itemtabPanel003" runat="server" HeaderText="Opciones avanzadas" Tooltip="Opciones avanzadas" >
<ContentTemplate>
<table>
<tr>
<td  colspan="1" >
Decision
</td>
<td  colspan="1" >
<asp:Label  runat="server" ID="itemctrl000004" CssClass="form-control-read" width="100%" OnDataBinding="itemctrl000004_Databound" />
</td>
</tr>
</table>
</ContentTemplate>
</ajaxkit:TabPanel>
<ajaxkit:TabPanel ID="itemtabPanel004" runat="server" HeaderText="Estado y auditoría" Tooltip="Estado y auditoría" >
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
<td  colspan="2" >
<ajaxkit:TabContainer ID="edittabPanel" runat="server" >
<ajaxkit:TabPanel ID="edittabPanel001" runat="server" HeaderText="General <span class='error'><b>*</b></span>" Tooltip="General" >
<ContentTemplate>
<table>
<tr>
<td  colspan="1" >
<b>Descripción</b>
</td>
<td  colspan="1" >
<asp:TextBox  runat="server" ID="editctrl000019" CssClass="form-control" Width="400px" MaxLength="50" OnDataBinding="editctrl000019_DataBound_edittabPanel001" Tooltip="Campo31[]" TextMode="MultiLine" onkeypress="return textCounter(this,50);" onkeydown="return textCounter(this,50);" onkeyup="return textCounter(this,50);" onfocus="return textCounter_GetFocus(this,50);" onblur="return textCounter_LostFocus(this);" /> <span class='error'><b>*</b></span>
<asp:RequiredFieldValidator  runat="server" ID="vrqeditctrl000019" SetFocusOnError="true" CssClass="error" Display="Dynamic" ControltoValidate="editctrl000019" Text="Obligatorio!!! ErrorMessage=´Descripción:es un dato obligatorio!´" ValidationGroup="vgedit" />
<ajaxkit:ValidatorCalloutExtender  runat="server" ID="ajaxvrqeditctrl000019" TargetControlID="vrqeditctrl000019" /><asp:CompareValidator  runat="server" ID="vcdvaleditctrl000019" SetFocusOnError="true" CssClass="error" ControltoValidate="editctrl000019" Display="Dynamic" Text="Ingrese un texto" ErrorMessage="Descripción:Ingrese un texto" Type="String" Operator="DataTypeCheck" ValidationGroup="vgedit" />
<asp:RegularExpressionValidator  runat="server" ID="vrgvaleditctrl000019" SetFocusOnError="true" CssClass="error" Display="Dynamic" ControltoValidate="editctrl000019" ValidationExpression="[\w\s-+/*^()!|°$%&=-_,:;.ñÑ~áéíóúÁÉÍÓÚ#}{¿]{0,50}" Text="No mayor a 50 caracteres. Deben ser letras o numeros." ErrorMessage="Descripción:No mayor a 50 caracteres. Deben ser letras o numeros." ValidationGroup="vgedit" />
<ajaxkit:ValidatorCalloutExtender  runat="server" ID="ajaxvrgeditctrl000019" TargetControlID="vrgvaleditctrl000019" />

</td>
</tr>
<tr>
<td  colspan="1" >

</td>
<td  colspan="1" >
<asp:SqlDataSource  runat="server" ID="dseditctrl000018" CancelSelectOnNullParameter="False" onInit="dseditctrl000018_Init" SelectCommandType="Text"
 SelectCommand="SELECT DOCTIP.dsc,DOCTIP.cod FROM DOCTIP   ORDER BY DOCTIP.dsc" onselected="dseditctrl000018_Selected" >
<SelectParameters>
</SelectParameters>
</asp:SqlDataSource>

</td>
</tr>
<tr>
<td  colspan="1" >
Tipo de unidad
</td>
<td  colspan="1" >
<asp:UpdatePanel  ID="updupdeditctrl000018fs" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="True" >
<ContentTemplate>
<asp:DropDownList  runat="server" ID="editctrl000018" CssClass="form-control" DataSourceID="dseditctrl000018" AppendDataBoundItems="False" DataTextField="dsc" DataValueField="cod" Enabled="True" onDataBound="editctrl000018_DataBound" >
</asp:DropDownList>
<asp:LinkButton runat="server" ID="editctrl000018view" Text='<%# Eval("undtipcoddsc") %>' CssClass="boton-acciones-subbutton" OnDataBinding="editctrl000018view_DataBinding" CausesValidation="False" />
</ContentTemplate>
</asp:UpdatePanel>
<asp:UpdateProgress  runat="server" AssociatedUpdatePanelID="updupdeditctrl000018fs" >
<ProgressTemplate><span  class="ajaxupd" ><div style="position:absolute;top:2px;left:50%; border-color:#333333; border:1; border-style:solid; background-color:#FFFFFF;padding:3px;"><image src="imagenes/ajaxupdate.gif" border=0 />Actualizando</div></span></ProgressTemplate>
</asp:UpdateProgress>

</td>
</tr>
<tr>
<td  colspan="1" >

</td>
<td  colspan="1" >
<asp:SqlDataSource  runat="server" ID="dseditctrl000017" CancelSelectOnNullParameter="False" onInit="dseditctrl000017_Init" SelectCommandType="Text"
 SelectCommand="SELECT EMP.dsc,EMP.cod FROM EMP   WHERE (EMP.baja = 0 OR EMP.baja  IS NULL) ORDER BY EMP.dsc" onselected="dseditctrl000017_Selected" >
<SelectParameters>
</SelectParameters>
</asp:SqlDataSource>

</td>
</tr>
<tr>
<td  colspan="1" >
Responsable de la unidad
</td>
<td  colspan="1" >
<asp:UpdatePanel  ID="updupdpnlupdeditctrl000017fs" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="True" >
<Triggers>
  <asp:AsyncPostBackTrigger  ControlID="editctrl000017delete" EventName="Click" />
</Triggers>
<ContentTemplate>
<asp:UpdatePanel  ID="updupdeditctrl000017fs" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="True" >
<ContentTemplate>
<asp:Button  runat="server" ID="cmdeditctrl000017showpanel" Text="..." CssClass="boton-acciones" CausesValidation="False" OnClick="cmdeditctrl000017showpanel_Click" />

<asp:ImageButton runat="server" ID="editctrl000017delete" ImageURL="~/imagenes/actdel.png" Width="16" Height="16" Tooltip="Quitar Responsable de la unidad" Visible="False" OnClick="editctrl000017delete_Click" OnDataBinding="editctrl000017delete_DataBinding" CausesValidation="False" />
<asp:LinkButton runat="server" ID="editctrl000017view" Text='<%# Eval("respdsc") %>' CssClass="boton-acciones-subbutton" OnDataBinding="editctrl000017view_DataBinding" CausesValidation="False" OnClientClick='<%# "javascript:if (window.showModalDialog) {window.showModalDialog(" & chr(39) & "frmColaboradores_det.aspx?_mode_=0&_closea_=1&isDlg=1&param1=" & Eval("resp") & "&timestamp=" & chr(39) & " + new Date().getTime() + " & chr(39) & "" & chr(39) & ", " & chr(39) & "" & chr(39) & ", " & chr(39) & "menubar=no;resizable=yes;toolbar=no;status=no;center=yes;dialogWidth=750px;dialogHeight=570px;" & chr(39) & ");} else {window.open(" & chr(39) & "frmColaboradores_det.aspx?_mode_=0&_closea_=1&isDlg=1&param1=" & Eval("resp") & "&timestamp=" & chr(39) & " + new Date().getTime() + " & chr(39) & "" & chr(39) & ", " & chr(39) & "" & chr(39) & ", " & chr(39) & "modal=yes;directories=no;menubar=no;resizable=yes;toolbar=no;status=no;center=yes;dialogWidth=750px;dialogHeight=570px;" & chr(39) & ");};return false;" %>' PostBackUrl='<%# "javascript:if (window.showModalDialog) {window.showModalDialog(" & chr(39) & "frmColaboradores_det.aspx?_mode_=0&_closea_=1&isDlg=1&param1=" & Eval("resp") & "&timestamp=" & chr(39) & " + new Date().getTime() + " & chr(39) & "" & chr(39) & ", " & chr(39) & "" & chr(39) & ", " & chr(39) & "menubar=no;resizable=yes;toolbar=no;status=no;center=yes;dialogWidth=750px;dialogHeight=570px;" & chr(39) & ");} else {window.open(" & chr(39) & "frmColaboradores_det.aspx?_mode_=0&_closea_=1&isDlg=1&param1=" & Eval("resp") & "&timestamp=" & chr(39) & " + new Date().getTime() + " & chr(39) & "" & chr(39) & ", " & chr(39) & "" & chr(39) & ", " & chr(39) & "modal=yes;directories=no;menubar=no;resizable=yes;toolbar=no;status=no;center=yes;dialogWidth=750px;dialogHeight=570px;" & chr(39) & ");};return false;" %>' />
<asp:HiddenField  runat="server" ID="editctrl000017type"/>
<asp:HiddenField  runat="server" ID="editctrl000017" OnDataBinding="editctrl000017_DataBound"/>
</ContentTemplate>
</asp:UpdatePanel>
<asp:UpdateProgress  runat="server" AssociatedUpdatePanelID="updupdeditctrl000017fs" >
<ProgressTemplate><span  class="ajaxupd" ><div style="position:absolute;top:2px;left:50%; border-color:#333333; border:1; border-style:solid; background-color:#FFFFFF;padding:3px;"><image src="imagenes/ajaxupdate.gif" border=0 />Actualizando</div></span></ProgressTemplate>
</asp:UpdateProgress>
</ContentTemplate>
</asp:UpdatePanel>
<asp:UpdateProgress  runat="server" AssociatedUpdatePanelID="updupdpnlupdeditctrl000017fs" >
<ProgressTemplate><span  class="ajaxupd" ><div style="position:absolute;top:2px;left:50%; border-color:#333333; border:1; border-style:solid; background-color:#FFFFFF;padding:3px;"><image src="imagenes/ajaxupdate.gif" border=0 />Actualizando</div></span></ProgressTemplate>
</asp:UpdateProgress>

</td>
</tr>
<tr>
<td  colspan="1" >

</td>
<td  colspan="1" >
<asp:SqlDataSource  runat="server" ID="dseditctrl000016" CancelSelectOnNullParameter="False" onInit="dseditctrl000016_Init" SelectCommandType="Text"
 SelectCommand="SELECT UND.dsc,UND.cod FROM UND   WHERE (UND.baja = 0 OR UND.baja  IS NULL) ORDER BY UND.orden" onselected="dseditctrl000016_Selected" >
<SelectParameters>
</SelectParameters>
</asp:SqlDataSource>

</td>
</tr>
<tr>
<td  colspan="1" >
Unidad superior en jerarquía
</td>
<td  colspan="1" >
<asp:UpdatePanel  ID="updupdpnlupdeditctrl000016fs" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="True" >
<Triggers>
  <asp:AsyncPostBackTrigger  ControlID="editctrl000016delete" EventName="Click" />
</Triggers>
<ContentTemplate>
<asp:UpdatePanel  ID="updupdeditctrl000016fs" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="True" >
<ContentTemplate>
<asp:Button  runat="server" ID="cmdeditctrl000016showpanel" Text="..." CssClass="boton-acciones" CausesValidation="False" OnClick="cmdeditctrl000016showpanel_Click" />

<asp:ImageButton runat="server" ID="editctrl000016delete" ImageURL="~/imagenes/actdel.png" Width="16" Height="16" Tooltip="Quitar Unidad superior en jerarquía" Visible="False" OnClick="editctrl000016delete_Click" OnDataBinding="editctrl000016delete_DataBinding" CausesValidation="False" />
<asp:LinkButton runat="server" ID="editctrl000016view" Text='<%# Eval("undcodsupdsc") %>' CssClass="boton-acciones-subbutton" OnDataBinding="editctrl000016view_DataBinding" CausesValidation="False" OnClientClick='<%# "javascript:if (window.showModalDialog) {window.showModalDialog(" & chr(39) & "frmUnidades_det.aspx?_mode_=0&_closea_=1&isDlg=1&param1=" & Eval("undcodsup") & "&timestamp=" & chr(39) & " + new Date().getTime() + " & chr(39) & "" & chr(39) & ", " & chr(39) & "" & chr(39) & ", " & chr(39) & "menubar=no;resizable=yes;toolbar=no;status=no;center=yes;dialogWidth=750px;dialogHeight=570px;" & chr(39) & ");} else {window.open(" & chr(39) & "frmUnidades_det.aspx?_mode_=0&_closea_=1&isDlg=1&param1=" & Eval("undcodsup") & "&timestamp=" & chr(39) & " + new Date().getTime() + " & chr(39) & "" & chr(39) & ", " & chr(39) & "" & chr(39) & ", " & chr(39) & "modal=yes;directories=no;menubar=no;resizable=yes;toolbar=no;status=no;center=yes;dialogWidth=750px;dialogHeight=570px;" & chr(39) & ");};return false;" %>' PostBackUrl='<%# "javascript:if (window.showModalDialog) {window.showModalDialog(" & chr(39) & "frmUnidades_det.aspx?_mode_=0&_closea_=1&isDlg=1&param1=" & Eval("undcodsup") & "&timestamp=" & chr(39) & " + new Date().getTime() + " & chr(39) & "" & chr(39) & ", " & chr(39) & "" & chr(39) & ", " & chr(39) & "menubar=no;resizable=yes;toolbar=no;status=no;center=yes;dialogWidth=750px;dialogHeight=570px;" & chr(39) & ");} else {window.open(" & chr(39) & "frmUnidades_det.aspx?_mode_=0&_closea_=1&isDlg=1&param1=" & Eval("undcodsup") & "&timestamp=" & chr(39) & " + new Date().getTime() + " & chr(39) & "" & chr(39) & ", " & chr(39) & "" & chr(39) & ", " & chr(39) & "modal=yes;directories=no;menubar=no;resizable=yes;toolbar=no;status=no;center=yes;dialogWidth=750px;dialogHeight=570px;" & chr(39) & ");};return false;" %>' />
<asp:HiddenField  runat="server" ID="editctrl000016type"/>
<asp:HiddenField  runat="server" ID="editctrl000016" OnDataBinding="editctrl000016_DataBound"/>
</ContentTemplate>
</asp:UpdatePanel>
<asp:UpdateProgress  runat="server" AssociatedUpdatePanelID="updupdeditctrl000016fs" >
<ProgressTemplate><span  class="ajaxupd" ><div style="position:absolute;top:2px;left:50%; border-color:#333333; border:1; border-style:solid; background-color:#FFFFFF;padding:3px;"><image src="imagenes/ajaxupdate.gif" border=0 />Actualizando</div></span></ProgressTemplate>
</asp:UpdateProgress>
</ContentTemplate>
</asp:UpdatePanel>
<asp:UpdateProgress  runat="server" AssociatedUpdatePanelID="updupdpnlupdeditctrl000016fs" >
<ProgressTemplate><span  class="ajaxupd" ><div style="position:absolute;top:2px;left:50%; border-color:#333333; border:1; border-style:solid; background-color:#FFFFFF;padding:3px;"><image src="imagenes/ajaxupdate.gif" border=0 />Actualizando</div></span></ProgressTemplate>
</asp:UpdateProgress>

</td>
</tr>
<tr>
<td  colspan="1" >
Abreviatura
</td>
<td  colspan="1" >
<asp:Label  runat="server" ID="editctrl000015" CssClass="form-control-read" width="100%" OnDataBinding="editctrl000015_Databound" />
</td>
</tr>
<tr>
<td  colspan="1" >
Identificador interno
</td>
<td  colspan="1" >
<asp:TextBox  runat="server" ID="editctrl000014" CssClass="form-control" Width="136px" MaxLength="12" OnDataBinding="editctrl000014_DataBound_edittabPanel001" Tooltip="Campo77[]" />
<asp:CompareValidator  runat="server" ID="vcdvaleditctrl000014" SetFocusOnError="true" CssClass="error" ControltoValidate="editctrl000014" Display="Dynamic" Text="Ingrese un texto" ErrorMessage="Identificador interno:Ingrese un texto" Type="String" Operator="DataTypeCheck" ValidationGroup="vgedit" />
<asp:RegularExpressionValidator  runat="server" ID="vrgvaleditctrl000014" SetFocusOnError="true" CssClass="error" Display="Dynamic" ControltoValidate="editctrl000014" ValidationExpression="[\w\s-+/*^()!|°$%&=-_,:;.ñÑ~áéíóúÁÉÍÓÚ#}{¿]{0,12}" Text="No mayor a 12 caracteres. Deben ser letras o numeros." ErrorMessage="Identificador interno:No mayor a 12 caracteres. Deben ser letras o numeros." ValidationGroup="vgedit" />
<ajaxkit:ValidatorCalloutExtender  runat="server" ID="ajaxvrgeditctrl000014" TargetControlID="vrgvaleditctrl000014" />

</td>
</tr>
<tr>
<td  colspan="1" >
Orden
</td>
<td  colspan="1" >
<asp:TextBox  runat="server" ID="editctrl000013" CssClass="form-control" Width="104px" MaxLength="8" OnDataBinding="editctrl000013_DataBound_edittabPanel001" Tooltip="Campo152[]" />
<asp:CompareValidator  runat="server" ID="vcdvaleditctrl000013" SetFocusOnError="true" CssClass="error" ControltoValidate="editctrl000013" Display="Dynamic" Text='Ingrese un entero' ErrorMessage='Orden:Ingrese un entero' Type="Integer" Operator="DataTypeCheck" ValidationGroup="vgedit" />
<asp:RangeValidator  runat="server" ID="vrnvaleditctrl000013" SetFocusOnError="true" CssClass="error" Display="Dynamic" ControltoValidate="editctrl000013" Text="Ingrese un valor entre -2000000000 y 2000000000" ErrorMessage="Orden:Ingrese un valor entre -2000000000 y 2000000000" Type="Integer" CultureInvariantValues="True" MaximumValue="2000000000" MinimumValue="-2000000000" ValidationGroup="vgedit" />
<ajaxkit:ValidatorCalloutExtender  runat="server" ID="ajaxvrneditctrl000013" TargetControlID="vrnvaleditctrl000013" />

</td>
</tr>
<tr>
<td  colspan="1" >
Grupo de miembros
</td>
<td  colspan="1" >
<asp:LinkButton runat="server" ID="editctrl000012view" Text='<%# Eval("miembrosgrpcoddsc") %>' CssClass="form-control-read" OnDataBinding="editctrl000012view_DataBinding" CausesValidation="False" OnClientClick='<%# "javascript:if (window.showModalDialog) {window.showModalDialog(" & chr(39) & "frmGrupos_det.aspx?_mode_=0&_closea_=1&isDlg=1&param1=" & Eval("miembrosgrpcod") & "&timestamp=" & chr(39) & " + new Date().getTime() + " & chr(39) & "" & chr(39) & ", " & chr(39) & "" & chr(39) & ", " & chr(39) & "menubar=no;resizable=yes;toolbar=no;status=no;center=yes;dialogWidth=750px;dialogHeight=570px;" & chr(39) & ");} else {window.open(" & chr(39) & "frmGrupos_det.aspx?_mode_=0&_closea_=1&isDlg=1&param1=" & Eval("miembrosgrpcod") & "&timestamp=" & chr(39) & " + new Date().getTime() + " & chr(39) & "" & chr(39) & ", " & chr(39) & "" & chr(39) & ", " & chr(39) & "modal=yes;directories=no;menubar=no;resizable=yes;toolbar=no;status=no;center=yes;dialogWidth=750px;dialogHeight=570px;" & chr(39) & ");};return false;" %>' PostBackUrl='<%# "javascript:if (window.showModalDialog) {window.showModalDialog(" & chr(39) & "frmGrupos_det.aspx?_mode_=0&_closea_=1&isDlg=1&param1=" & Eval("miembrosgrpcod") & "&timestamp=" & chr(39) & " + new Date().getTime() + " & chr(39) & "" & chr(39) & ", " & chr(39) & "" & chr(39) & ", " & chr(39) & "menubar=no;resizable=yes;toolbar=no;status=no;center=yes;dialogWidth=750px;dialogHeight=570px;" & chr(39) & ");} else {window.open(" & chr(39) & "frmGrupos_det.aspx?_mode_=0&_closea_=1&isDlg=1&param1=" & Eval("miembrosgrpcod") & "&timestamp=" & chr(39) & " + new Date().getTime() + " & chr(39) & "" & chr(39) & ", " & chr(39) & "" & chr(39) & ", " & chr(39) & "modal=yes;directories=no;menubar=no;resizable=yes;toolbar=no;status=no;center=yes;dialogWidth=750px;dialogHeight=570px;" & chr(39) & ");};return false;" %>' />
<asp:HiddenField  runat="server" ID="editctrl000012type"/>
<asp:HiddenField  runat="server" ID="editctrl000012" OnDataBinding="editctrl000012_DataBound"/>

</td>
</tr>
<tr>
<td  colspan="1" >
Grupo de responsables
</td>
<td  colspan="1" >
<asp:LinkButton runat="server" ID="editctrl000011view" Text='<%# Eval("grpcodrespdsc") %>' CssClass="form-control-read" OnDataBinding="editctrl000011view_DataBinding" CausesValidation="False" OnClientClick='<%# "javascript:if (window.showModalDialog) {window.showModalDialog(" & chr(39) & "frmGrupos_det.aspx?_mode_=0&_closea_=1&isDlg=1&param1=" & Eval("grpcodresp") & "&timestamp=" & chr(39) & " + new Date().getTime() + " & chr(39) & "" & chr(39) & ", " & chr(39) & "" & chr(39) & ", " & chr(39) & "menubar=no;resizable=yes;toolbar=no;status=no;center=yes;dialogWidth=750px;dialogHeight=570px;" & chr(39) & ");} else {window.open(" & chr(39) & "frmGrupos_det.aspx?_mode_=0&_closea_=1&isDlg=1&param1=" & Eval("grpcodresp") & "&timestamp=" & chr(39) & " + new Date().getTime() + " & chr(39) & "" & chr(39) & ", " & chr(39) & "" & chr(39) & ", " & chr(39) & "modal=yes;directories=no;menubar=no;resizable=yes;toolbar=no;status=no;center=yes;dialogWidth=750px;dialogHeight=570px;" & chr(39) & ");};return false;" %>' PostBackUrl='<%# "javascript:if (window.showModalDialog) {window.showModalDialog(" & chr(39) & "frmGrupos_det.aspx?_mode_=0&_closea_=1&isDlg=1&param1=" & Eval("grpcodresp") & "&timestamp=" & chr(39) & " + new Date().getTime() + " & chr(39) & "" & chr(39) & ", " & chr(39) & "" & chr(39) & ", " & chr(39) & "menubar=no;resizable=yes;toolbar=no;status=no;center=yes;dialogWidth=750px;dialogHeight=570px;" & chr(39) & ");} else {window.open(" & chr(39) & "frmGrupos_det.aspx?_mode_=0&_closea_=1&isDlg=1&param1=" & Eval("grpcodresp") & "&timestamp=" & chr(39) & " + new Date().getTime() + " & chr(39) & "" & chr(39) & ", " & chr(39) & "" & chr(39) & ", " & chr(39) & "modal=yes;directories=no;menubar=no;resizable=yes;toolbar=no;status=no;center=yes;dialogWidth=750px;dialogHeight=570px;" & chr(39) & ");};return false;" %>' />
<asp:HiddenField  runat="server" ID="editctrl000011type"/>
<asp:HiddenField  runat="server" ID="editctrl000011" OnDataBinding="editctrl000011_DataBound"/>

</td>
</tr>
<tr>
<td  colspan="1" >
Grupo de superiores
</td>
<td  colspan="1" >
<asp:LinkButton runat="server" ID="editctrl000010view" Text='<%# Eval("grpcodprjverdsc") %>' CssClass="form-control-read" OnDataBinding="editctrl000010view_DataBinding" CausesValidation="False" OnClientClick='<%# "javascript:if (window.showModalDialog) {window.showModalDialog(" & chr(39) & "frmGrupos_det.aspx?_mode_=0&_closea_=1&isDlg=1&param1=" & Eval("grpcodprjver") & "&timestamp=" & chr(39) & " + new Date().getTime() + " & chr(39) & "" & chr(39) & ", " & chr(39) & "" & chr(39) & ", " & chr(39) & "menubar=no;resizable=yes;toolbar=no;status=no;center=yes;dialogWidth=750px;dialogHeight=570px;" & chr(39) & ");} else {window.open(" & chr(39) & "frmGrupos_det.aspx?_mode_=0&_closea_=1&isDlg=1&param1=" & Eval("grpcodprjver") & "&timestamp=" & chr(39) & " + new Date().getTime() + " & chr(39) & "" & chr(39) & ", " & chr(39) & "" & chr(39) & ", " & chr(39) & "modal=yes;directories=no;menubar=no;resizable=yes;toolbar=no;status=no;center=yes;dialogWidth=750px;dialogHeight=570px;" & chr(39) & ");};return false;" %>' PostBackUrl='<%# "javascript:if (window.showModalDialog) {window.showModalDialog(" & chr(39) & "frmGrupos_det.aspx?_mode_=0&_closea_=1&isDlg=1&param1=" & Eval("grpcodprjver") & "&timestamp=" & chr(39) & " + new Date().getTime() + " & chr(39) & "" & chr(39) & ", " & chr(39) & "" & chr(39) & ", " & chr(39) & "menubar=no;resizable=yes;toolbar=no;status=no;center=yes;dialogWidth=750px;dialogHeight=570px;" & chr(39) & ");} else {window.open(" & chr(39) & "frmGrupos_det.aspx?_mode_=0&_closea_=1&isDlg=1&param1=" & Eval("grpcodprjver") & "&timestamp=" & chr(39) & " + new Date().getTime() + " & chr(39) & "" & chr(39) & ", " & chr(39) & "" & chr(39) & ", " & chr(39) & "modal=yes;directories=no;menubar=no;resizable=yes;toolbar=no;status=no;center=yes;dialogWidth=750px;dialogHeight=570px;" & chr(39) & ");};return false;" %>' />
<asp:HiddenField  runat="server" ID="editctrl000010type"/>
<asp:HiddenField  runat="server" ID="editctrl000010" OnDataBinding="editctrl000010_DataBound"/>

</td>
</tr>
<tr>
<td  colspan="1" >
Grupo de miembros directos
</td>
<td  colspan="1" >
<asp:LinkButton runat="server" ID="editctrl000009view" Text='<%# Eval("grpcodmbrdirdsc") %>' CssClass="form-control-read" OnDataBinding="editctrl000009view_DataBinding" CausesValidation="False" OnClientClick='<%# "javascript:if (window.showModalDialog) {window.showModalDialog(" & chr(39) & "frmGrupos_det.aspx?_mode_=0&_closea_=1&isDlg=1&param1=" & Eval("grpcodmbrdir") & "&timestamp=" & chr(39) & " + new Date().getTime() + " & chr(39) & "" & chr(39) & ", " & chr(39) & "" & chr(39) & ", " & chr(39) & "menubar=no;resizable=yes;toolbar=no;status=no;center=yes;dialogWidth=750px;dialogHeight=570px;" & chr(39) & ");} else {window.open(" & chr(39) & "frmGrupos_det.aspx?_mode_=0&_closea_=1&isDlg=1&param1=" & Eval("grpcodmbrdir") & "&timestamp=" & chr(39) & " + new Date().getTime() + " & chr(39) & "" & chr(39) & ", " & chr(39) & "" & chr(39) & ", " & chr(39) & "modal=yes;directories=no;menubar=no;resizable=yes;toolbar=no;status=no;center=yes;dialogWidth=750px;dialogHeight=570px;" & chr(39) & ");};return false;" %>' PostBackUrl='<%# "javascript:if (window.showModalDialog) {window.showModalDialog(" & chr(39) & "frmGrupos_det.aspx?_mode_=0&_closea_=1&isDlg=1&param1=" & Eval("grpcodmbrdir") & "&timestamp=" & chr(39) & " + new Date().getTime() + " & chr(39) & "" & chr(39) & ", " & chr(39) & "" & chr(39) & ", " & chr(39) & "menubar=no;resizable=yes;toolbar=no;status=no;center=yes;dialogWidth=750px;dialogHeight=570px;" & chr(39) & ");} else {window.open(" & chr(39) & "frmGrupos_det.aspx?_mode_=0&_closea_=1&isDlg=1&param1=" & Eval("grpcodmbrdir") & "&timestamp=" & chr(39) & " + new Date().getTime() + " & chr(39) & "" & chr(39) & ", " & chr(39) & "" & chr(39) & ", " & chr(39) & "modal=yes;directories=no;menubar=no;resizable=yes;toolbar=no;status=no;center=yes;dialogWidth=750px;dialogHeight=570px;" & chr(39) & ");};return false;" %>' />
<asp:HiddenField  runat="server" ID="editctrl000009type"/>
<asp:HiddenField  runat="server" ID="editctrl000009" OnDataBinding="editctrl000009_DataBound"/>

</td>
</tr>
</table>
</ContentTemplate>
</ajaxkit:TabPanel>
<ajaxkit:TabPanel ID="edittabPanel002" runat="server" HeaderText="Documentador" Tooltip="Documentador" >
<ContentTemplate>
<table>
<tr>
<td  colspan="1" >
Miembros directos
</td>
<td  colspan="1" >
<asp:RadioButtonList  runat="server" ID="editctrl000008" CssClass="form-control" RepeatLayout="Flow" RepeatDirection="Horizontal" OnDataBinding="editctrl000008_Databound" >
<asp:ListItem  Text="Sí" Value="True" />
<asp:ListItem  Text="No" Value="False" />
<asp:ListItem  Text="Sin definir" Value="" Selected="True" />
</asp:RadioButtonList>

</td>
</tr>
<tr>
<td  colspan="1" >
Formato especifico
</td>
<td  colspan="1" >
<asp:TextBox  runat="server" ID="editctrl000007" CssClass="form-control" Width="400px" MaxLength="50" OnDataBinding="editctrl000007_DataBound_edittabPanel002" Tooltip="Campo124[Formato de identificación. Se pueden utilizar las siguientes variables:{#s#} código de sector, {#n#} código de apartado. {#z#} nro. de documento]" TextMode="MultiLine" onkeypress="return textCounter(this,50);" onkeydown="return textCounter(this,50);" onkeyup="return textCounter(this,50);" onfocus="return textCounter_GetFocus(this,50);" onblur="return textCounter_LostFocus(this);" />
<asp:CompareValidator  runat="server" ID="vcdvaleditctrl000007" SetFocusOnError="true" CssClass="error" ControltoValidate="editctrl000007" Display="Dynamic" Text="Ingrese un texto" ErrorMessage="Formato especifico:Ingrese un texto" Type="String" Operator="DataTypeCheck" ValidationGroup="vgedit" />
<asp:RegularExpressionValidator  runat="server" ID="vrgvaleditctrl000007" SetFocusOnError="true" CssClass="error" Display="Dynamic" ControltoValidate="editctrl000007" ValidationExpression="[\w\s-+/*^()!|°$%&=-_,:;.ñÑ~áéíóúÁÉÍÓÚ#}{¿]{0,50}" Text="No mayor a 50 caracteres. Deben ser letras o numeros." ErrorMessage="Formato especifico:No mayor a 50 caracteres. Deben ser letras o numeros." ValidationGroup="vgedit" />
<ajaxkit:ValidatorCalloutExtender  runat="server" ID="ajaxvrgeditctrl000007" TargetControlID="vrgvaleditctrl000007" />

</td>
</tr>
<tr>
<td  colspan="1" >

</td>
<td  colspan="1" >
<asp:SqlDataSource  runat="server" ID="dseditctrl000006" CancelSelectOnNullParameter="False" onInit="dseditctrl000006_Init" SelectCommandType="Text"
 SelectCommand="SELECT EMP.dsc,EMP.cod FROM EMP   WHERE (EMP.baja = 0 OR EMP.baja  IS NULL) ORDER BY EMP.dsc" onselected="dseditctrl000006_Selected" >
<SelectParameters>
</SelectParameters>
</asp:SqlDataSource>

</td>
</tr>
<tr>
<td  colspan="1" >
Editor
</td>
<td  colspan="1" >
<asp:UpdatePanel  ID="updupdpnlupdeditctrl000006fs" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="True" >
<Triggers>
  <asp:AsyncPostBackTrigger  ControlID="editctrl000006delete" EventName="Click" />
</Triggers>
<ContentTemplate>
<asp:UpdatePanel  ID="updupdeditctrl000006fs" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="True" >
<ContentTemplate>
<asp:Button  runat="server" ID="cmdeditctrl000006showpanel" Text="..." CssClass="boton-acciones" CausesValidation="False" OnClick="cmdeditctrl000006showpanel_Click" />

<asp:ImageButton runat="server" ID="editctrl000006delete" ImageURL="~/imagenes/actdel.png" Width="16" Height="16" Tooltip="Quitar Editor" Visible="False" OnClick="editctrl000006delete_Click" OnDataBinding="editctrl000006delete_DataBinding" CausesValidation="False" />
<asp:LinkButton runat="server" ID="editctrl000006view" Text='<%# Eval("editordsc") %>' CssClass="boton-acciones-subbutton" OnDataBinding="editctrl000006view_DataBinding" CausesValidation="False" OnClientClick='<%# "javascript:if (window.showModalDialog) {window.showModalDialog(" & chr(39) & "frmColaboradores_det.aspx?_mode_=0&_closea_=1&isDlg=1&param1=" & Eval("editor") & "&timestamp=" & chr(39) & " + new Date().getTime() + " & chr(39) & "" & chr(39) & ", " & chr(39) & "" & chr(39) & ", " & chr(39) & "menubar=no;resizable=yes;toolbar=no;status=no;center=yes;dialogWidth=750px;dialogHeight=570px;" & chr(39) & ");} else {window.open(" & chr(39) & "frmColaboradores_det.aspx?_mode_=0&_closea_=1&isDlg=1&param1=" & Eval("editor") & "&timestamp=" & chr(39) & " + new Date().getTime() + " & chr(39) & "" & chr(39) & ", " & chr(39) & "" & chr(39) & ", " & chr(39) & "modal=yes;directories=no;menubar=no;resizable=yes;toolbar=no;status=no;center=yes;dialogWidth=750px;dialogHeight=570px;" & chr(39) & ");};return false;" %>' PostBackUrl='<%# "javascript:if (window.showModalDialog) {window.showModalDialog(" & chr(39) & "frmColaboradores_det.aspx?_mode_=0&_closea_=1&isDlg=1&param1=" & Eval("editor") & "&timestamp=" & chr(39) & " + new Date().getTime() + " & chr(39) & "" & chr(39) & ", " & chr(39) & "" & chr(39) & ", " & chr(39) & "menubar=no;resizable=yes;toolbar=no;status=no;center=yes;dialogWidth=750px;dialogHeight=570px;" & chr(39) & ");} else {window.open(" & chr(39) & "frmColaboradores_det.aspx?_mode_=0&_closea_=1&isDlg=1&param1=" & Eval("editor") & "&timestamp=" & chr(39) & " + new Date().getTime() + " & chr(39) & "" & chr(39) & ", " & chr(39) & "" & chr(39) & ", " & chr(39) & "modal=yes;directories=no;menubar=no;resizable=yes;toolbar=no;status=no;center=yes;dialogWidth=750px;dialogHeight=570px;" & chr(39) & ");};return false;" %>' />
<asp:HiddenField  runat="server" ID="editctrl000006type"/>
<asp:HiddenField  runat="server" ID="editctrl000006" OnDataBinding="editctrl000006_DataBound"/>
</ContentTemplate>
</asp:UpdatePanel>
<asp:UpdateProgress  runat="server" AssociatedUpdatePanelID="updupdeditctrl000006fs" >
<ProgressTemplate><span  class="ajaxupd" ><div style="position:absolute;top:2px;left:50%; border-color:#333333; border:1; border-style:solid; background-color:#FFFFFF;padding:3px;"><image src="imagenes/ajaxupdate.gif" border=0 />Actualizando</div></span></ProgressTemplate>
</asp:UpdateProgress>
</ContentTemplate>
</asp:UpdatePanel>
<asp:UpdateProgress  runat="server" AssociatedUpdatePanelID="updupdpnlupdeditctrl000006fs" >
<ProgressTemplate><span  class="ajaxupd" ><div style="position:absolute;top:2px;left:50%; border-color:#333333; border:1; border-style:solid; background-color:#FFFFFF;padding:3px;"><image src="imagenes/ajaxupdate.gif" border=0 />Actualizando</div></span></ProgressTemplate>
</asp:UpdateProgress>

</td>
</tr>
<tr>
<td  colspan="1" >
Grupo de editores
</td>
<td  colspan="1" >
<asp:LinkButton runat="server" ID="editctrl000005view" Text='<%# Eval("grpcodeditordsc") %>' CssClass="form-control-read" OnDataBinding="editctrl000005view_DataBinding" CausesValidation="False" OnClientClick='<%# "javascript:if (window.showModalDialog) {window.showModalDialog(" & chr(39) & "frmGrupos_det.aspx?_mode_=0&_closea_=1&isDlg=1&param1=" & Eval("grpcodeditor") & "&timestamp=" & chr(39) & " + new Date().getTime() + " & chr(39) & "" & chr(39) & ", " & chr(39) & "" & chr(39) & ", " & chr(39) & "menubar=no;resizable=yes;toolbar=no;status=no;center=yes;dialogWidth=750px;dialogHeight=570px;" & chr(39) & ");} else {window.open(" & chr(39) & "frmGrupos_det.aspx?_mode_=0&_closea_=1&isDlg=1&param1=" & Eval("grpcodeditor") & "&timestamp=" & chr(39) & " + new Date().getTime() + " & chr(39) & "" & chr(39) & ", " & chr(39) & "" & chr(39) & ", " & chr(39) & "modal=yes;directories=no;menubar=no;resizable=yes;toolbar=no;status=no;center=yes;dialogWidth=750px;dialogHeight=570px;" & chr(39) & ");};return false;" %>' PostBackUrl='<%# "javascript:if (window.showModalDialog) {window.showModalDialog(" & chr(39) & "frmGrupos_det.aspx?_mode_=0&_closea_=1&isDlg=1&param1=" & Eval("grpcodeditor") & "&timestamp=" & chr(39) & " + new Date().getTime() + " & chr(39) & "" & chr(39) & ", " & chr(39) & "" & chr(39) & ", " & chr(39) & "menubar=no;resizable=yes;toolbar=no;status=no;center=yes;dialogWidth=750px;dialogHeight=570px;" & chr(39) & ");} else {window.open(" & chr(39) & "frmGrupos_det.aspx?_mode_=0&_closea_=1&isDlg=1&param1=" & Eval("grpcodeditor") & "&timestamp=" & chr(39) & " + new Date().getTime() + " & chr(39) & "" & chr(39) & ", " & chr(39) & "" & chr(39) & ", " & chr(39) & "modal=yes;directories=no;menubar=no;resizable=yes;toolbar=no;status=no;center=yes;dialogWidth=750px;dialogHeight=570px;" & chr(39) & ");};return false;" %>' />
<asp:HiddenField  runat="server" ID="editctrl000005type"/>
<asp:HiddenField  runat="server" ID="editctrl000005" OnDataBinding="editctrl000005_DataBound"/>

</td>
</tr>
</table>
</ContentTemplate>
</ajaxkit:TabPanel>
<ajaxkit:TabPanel ID="edittabPanel003" runat="server" HeaderText="Opciones avanzadas" Tooltip="Opciones avanzadas" >
<ContentTemplate>
<table>
<tr>
<td  colspan="1" >
Decision
</td>
<td  colspan="1" >
<asp:Checkbox  runat="server" ID="editctrl000004" CssClass="form-control" OnDataBinding="editctrl000004_Databound" />
</td>
</tr>
</table>
</ContentTemplate>
</ajaxkit:TabPanel>
<ajaxkit:TabPanel ID="edittabPanel004" runat="server" HeaderText="Estado y auditoría" Tooltip="Estado y auditoría" >
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
<b>Codigo</b>
</td>
<td  colspan="1" >
<asp:TextBox  runat="server" ID="insctrl000021" CssClass="form-control" Width="40px" Tooltip="Campo30[]" /> <span class='error'><b>*</b></span>
<asp:RequiredFieldValidator  runat="server" ID="vrqinsctrl000021" SetFocusOnError="true" CssClass="error" Display="Dynamic" ControltoValidate="insctrl000021" Text="Obligatorio!!! ErrorMessage=´Codigo:es un dato obligatorio!´" ValidationGroup="vgins" />
<ajaxkit:ValidatorCalloutExtender  runat="server" ID="ajaxvrqinsctrl000021" TargetControlID="vrqinsctrl000021" /><asp:CompareValidator  runat="server" ID="vcdvalinsctrl000021" SetFocusOnError="true" CssClass="error" ControltoValidate="insctrl000021" Display="Dynamic" Text='Ingrese un entero' ErrorMessage='Codigo:Ingrese un entero' Type="Integer" Operator="DataTypeCheck" ValidationGroup="vgins" />
<asp:RangeValidator  runat="server" ID="vrnvalinsctrl000021" SetFocusOnError="true" CssClass="error" Display="Dynamic" ControltoValidate="insctrl000021" Text="Ingrese un valor entre 1 y 2000000000" ErrorMessage="Codigo:Ingrese un valor entre 1 y 2000000000" Type="Integer" CultureInvariantValues="True" MaximumValue="2000000000" MinimumValue="1" ValidationGroup="vgins" />
<ajaxkit:ValidatorCalloutExtender  runat="server" ID="ajaxvrninsctrl000021" TargetControlID="vrnvalinsctrl000021" />

</td>
</tr>
<tr>
<td  colspan="2" >
<ajaxkit:TabContainer ID="instabPanel" runat="server" >
<ajaxkit:TabPanel ID="instabPanel001" runat="server" HeaderText="General <span class='error'><b>*</b></span>" Tooltip="General" >
<ContentTemplate>
<table>
<tr>
<td  colspan="1" >
<b>Descripción</b>
</td>
<td  colspan="1" >
<asp:TextBox  runat="server" ID="insctrl000019" CssClass="form-control" Width="400px" MaxLength="50" Tooltip="Campo31[]" TextMode="MultiLine" onkeypress="return textCounter(this,50);" onkeydown="return textCounter(this,50);" onkeyup="return textCounter(this,50);" onfocus="return textCounter_GetFocus(this,50);" onblur="return textCounter_LostFocus(this);" /> <span class='error'><b>*</b></span>
<asp:RequiredFieldValidator  runat="server" ID="vrqinsctrl000019" SetFocusOnError="true" CssClass="error" Display="Dynamic" ControltoValidate="insctrl000019" Text="Obligatorio!!! ErrorMessage=´Descripción:es un dato obligatorio!´" ValidationGroup="vgins" />
<ajaxkit:ValidatorCalloutExtender  runat="server" ID="ajaxvrqinsctrl000019" TargetControlID="vrqinsctrl000019" /><asp:CompareValidator  runat="server" ID="vcdvalinsctrl000019" SetFocusOnError="true" CssClass="error" ControltoValidate="insctrl000019" Display="Dynamic" Text="Ingrese un texto" ErrorMessage="Descripción:Ingrese un texto" Type="String" Operator="DataTypeCheck" ValidationGroup="vgins" />
<asp:RegularExpressionValidator  runat="server" ID="vrgvalinsctrl000019" SetFocusOnError="true" CssClass="error" Display="Dynamic" ControltoValidate="insctrl000019" ValidationExpression="[\w\s-+/*^()!|°$%&=-_,:;.ñÑ~áéíóúÁÉÍÓÚ#}{¿]{0,50}" Text="No mayor a 50 caracteres. Deben ser letras o numeros." ErrorMessage="Descripción:No mayor a 50 caracteres. Deben ser letras o numeros." ValidationGroup="vgins" />
<ajaxkit:ValidatorCalloutExtender  runat="server" ID="ajaxvrginsctrl000019" TargetControlID="vrgvalinsctrl000019" />

</td>
</tr>
<tr>
<td  colspan="1" >

</td>
<td  colspan="1" >
<asp:SqlDataSource  runat="server" ID="dsinsctrl000018" CancelSelectOnNullParameter="False" onInit="dsinsctrl000018_Init" SelectCommandType="Text"
 SelectCommand="SELECT DOCTIP.dsc,DOCTIP.cod FROM DOCTIP   ORDER BY DOCTIP.dsc" onselected="dsinsctrl000018_Selected" >
<SelectParameters>
</SelectParameters>
</asp:SqlDataSource>

</td>
</tr>
<tr>
<td  colspan="1" >
Tipo de unidad
</td>
<td  colspan="1" >
<asp:UpdatePanel  ID="updupdinsctrl000018fs" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="True" >
<ContentTemplate>
<asp:DropDownList  runat="server" ID="insctrl000018" CssClass="form-control" DataSourceID="dsinsctrl000018" AppendDataBoundItems="False" DataTextField="dsc" DataValueField="cod" Enabled="True" onload="insctrl000018_Load" >
<asp:ListItem value="-1">Todos</asp:ListItem>
</asp:DropDownList>
</ContentTemplate>
</asp:UpdatePanel>
<asp:UpdateProgress  runat="server" AssociatedUpdatePanelID="updupdinsctrl000018fs" >
<ProgressTemplate><span  class="ajaxupd" ><div style="position:absolute;top:2px;left:50%; border-color:#333333; border:1; border-style:solid; background-color:#FFFFFF;padding:3px;"><image src="imagenes/ajaxupdate.gif" border=0 />Actualizando</div></span></ProgressTemplate>
</asp:UpdateProgress>

</td>
</tr>
<tr>
<td  colspan="1" >

</td>
<td  colspan="1" >
<asp:SqlDataSource  runat="server" ID="dsinsctrl000017" CancelSelectOnNullParameter="False" onInit="dsinsctrl000017_Init" SelectCommandType="Text"
 SelectCommand="SELECT EMP.dsc,EMP.cod FROM EMP   WHERE (EMP.baja = 0 OR EMP.baja  IS NULL) ORDER BY EMP.dsc" onselected="dsinsctrl000017_Selected" >
<SelectParameters>
</SelectParameters>
</asp:SqlDataSource>

</td>
</tr>
<tr>
<td  colspan="1" >
Responsable de la unidad
</td>
<td  colspan="1" >
<asp:UpdatePanel  ID="updupdpnlupdinsctrl000017fs" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="True" >
<Triggers>
  <asp:AsyncPostBackTrigger  ControlID="insctrl000017delete" EventName="Click" />
</Triggers>
<ContentTemplate>
<asp:UpdatePanel  ID="updupdinsctrl000017fs" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="True" >
<ContentTemplate>
<asp:Button  runat="server" ID="cmdinsctrl000017showpanel" Text="..." CssClass="boton-acciones" CausesValidation="False" OnClick="cmdinsctrl000017showpanel_Click" />

<asp:ImageButton runat="server" ID="insctrl000017delete" ImageURL="~/imagenes/actdel.png" Width="16" Height="16" Tooltip="Quitar Responsable de la unidad" Visible="False" OnClick="insctrl000017delete_Click" OnDataBinding="insctrl000017delete_DataBinding" CausesValidation="False" />
<asp:LinkButton runat="server" ID="insctrl000017view" CssClass="boton-acciones-subbutton" CausesValidation="False" />
<asp:HiddenField  runat="server" ID="insctrl000017type"/>
<asp:HiddenField  runat="server" ID="insctrl000017" onLoad="insctrl000017_Load"/>
</ContentTemplate>
</asp:UpdatePanel>
<asp:UpdateProgress  runat="server" AssociatedUpdatePanelID="updupdinsctrl000017fs" >
<ProgressTemplate><span  class="ajaxupd" ><div style="position:absolute;top:2px;left:50%; border-color:#333333; border:1; border-style:solid; background-color:#FFFFFF;padding:3px;"><image src="imagenes/ajaxupdate.gif" border=0 />Actualizando</div></span></ProgressTemplate>
</asp:UpdateProgress>
</ContentTemplate>
</asp:UpdatePanel>
<asp:UpdateProgress  runat="server" AssociatedUpdatePanelID="updupdpnlupdinsctrl000017fs" >
<ProgressTemplate><span  class="ajaxupd" ><div style="position:absolute;top:2px;left:50%; border-color:#333333; border:1; border-style:solid; background-color:#FFFFFF;padding:3px;"><image src="imagenes/ajaxupdate.gif" border=0 />Actualizando</div></span></ProgressTemplate>
</asp:UpdateProgress>

</td>
</tr>
<tr>
<td  colspan="1" >

</td>
<td  colspan="1" >
<asp:SqlDataSource  runat="server" ID="dsinsctrl000016" CancelSelectOnNullParameter="False" onInit="dsinsctrl000016_Init" SelectCommandType="Text"
 SelectCommand="SELECT UND.dsc,UND.cod FROM UND   WHERE (UND.baja = 0 OR UND.baja  IS NULL) ORDER BY UND.orden" onselected="dsinsctrl000016_Selected" >
<SelectParameters>
</SelectParameters>
</asp:SqlDataSource>

</td>
</tr>
<tr>
<td  colspan="1" >
Unidad superior en jerarquía
</td>
<td  colspan="1" >
<asp:UpdatePanel  ID="updupdpnlupdinsctrl000016fs" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="True" >
<Triggers>
  <asp:AsyncPostBackTrigger  ControlID="insctrl000016delete" EventName="Click" />
</Triggers>
<ContentTemplate>
<asp:UpdatePanel  ID="updupdinsctrl000016fs" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="True" >
<ContentTemplate>
<asp:Button  runat="server" ID="cmdinsctrl000016showpanel" Text="..." CssClass="boton-acciones" CausesValidation="False" OnClick="cmdinsctrl000016showpanel_Click" />

<asp:ImageButton runat="server" ID="insctrl000016delete" ImageURL="~/imagenes/actdel.png" Width="16" Height="16" Tooltip="Quitar Unidad superior en jerarquía" Visible="False" OnClick="insctrl000016delete_Click" OnDataBinding="insctrl000016delete_DataBinding" CausesValidation="False" />
<asp:LinkButton runat="server" ID="insctrl000016view" CssClass="boton-acciones-subbutton" CausesValidation="False" />
<asp:HiddenField  runat="server" ID="insctrl000016type"/>
<asp:HiddenField  runat="server" ID="insctrl000016" onLoad="insctrl000016_Load"/>
</ContentTemplate>
</asp:UpdatePanel>
<asp:UpdateProgress  runat="server" AssociatedUpdatePanelID="updupdinsctrl000016fs" >
<ProgressTemplate><span  class="ajaxupd" ><div style="position:absolute;top:2px;left:50%; border-color:#333333; border:1; border-style:solid; background-color:#FFFFFF;padding:3px;"><image src="imagenes/ajaxupdate.gif" border=0 />Actualizando</div></span></ProgressTemplate>
</asp:UpdateProgress>
</ContentTemplate>
</asp:UpdatePanel>
<asp:UpdateProgress  runat="server" AssociatedUpdatePanelID="updupdpnlupdinsctrl000016fs" >
<ProgressTemplate><span  class="ajaxupd" ><div style="position:absolute;top:2px;left:50%; border-color:#333333; border:1; border-style:solid; background-color:#FFFFFF;padding:3px;"><image src="imagenes/ajaxupdate.gif" border=0 />Actualizando</div></span></ProgressTemplate>
</asp:UpdateProgress>

</td>
</tr>
<tr>
<td  colspan="1" >
Identificador interno
</td>
<td  colspan="1" >
<asp:TextBox  runat="server" ID="insctrl000014" CssClass="form-control" Width="136px" MaxLength="12" Tooltip="Campo77[]" />
<asp:CompareValidator  runat="server" ID="vcdvalinsctrl000014" SetFocusOnError="true" CssClass="error" ControltoValidate="insctrl000014" Display="Dynamic" Text="Ingrese un texto" ErrorMessage="Identificador interno:Ingrese un texto" Type="String" Operator="DataTypeCheck" ValidationGroup="vgins" />
<asp:RegularExpressionValidator  runat="server" ID="vrgvalinsctrl000014" SetFocusOnError="true" CssClass="error" Display="Dynamic" ControltoValidate="insctrl000014" ValidationExpression="[\w\s-+/*^()!|°$%&=-_,:;.ñÑ~áéíóúÁÉÍÓÚ#}{¿]{0,12}" Text="No mayor a 12 caracteres. Deben ser letras o numeros." ErrorMessage="Identificador interno:No mayor a 12 caracteres. Deben ser letras o numeros." ValidationGroup="vgins" />
<ajaxkit:ValidatorCalloutExtender  runat="server" ID="ajaxvrginsctrl000014" TargetControlID="vrgvalinsctrl000014" />

</td>
</tr>
<tr>
<td  colspan="1" >
Orden
</td>
<td  colspan="1" >
<asp:TextBox  runat="server" ID="insctrl000013" CssClass="form-control" Width="104px" MaxLength="8" Tooltip="Campo152[]" />
<asp:CompareValidator  runat="server" ID="vcdvalinsctrl000013" SetFocusOnError="true" CssClass="error" ControltoValidate="insctrl000013" Display="Dynamic" Text='Ingrese un entero' ErrorMessage='Orden:Ingrese un entero' Type="Integer" Operator="DataTypeCheck" ValidationGroup="vgins" />
<asp:RangeValidator  runat="server" ID="vrnvalinsctrl000013" SetFocusOnError="true" CssClass="error" Display="Dynamic" ControltoValidate="insctrl000013" Text="Ingrese un valor entre -2000000000 y 2000000000" ErrorMessage="Orden:Ingrese un valor entre -2000000000 y 2000000000" Type="Integer" CultureInvariantValues="True" MaximumValue="2000000000" MinimumValue="-2000000000" ValidationGroup="vgins" />
<ajaxkit:ValidatorCalloutExtender  runat="server" ID="ajaxvrninsctrl000013" TargetControlID="vrnvalinsctrl000013" />

</td>
</tr>
</table>
</ContentTemplate>
</ajaxkit:TabPanel>
<ajaxkit:TabPanel ID="instabPanel002" runat="server" HeaderText="Documentador" Tooltip="Documentador" >
<ContentTemplate>
<table>
<tr>
<td  colspan="1" >
Miembros directos
</td>
<td  colspan="1" >
<asp:RadioButtonList  runat="server" ID="insctrl000008" CssClass="form-control" RepeatLayout="Flow" RepeatDirection="Horizontal" >
<asp:ListItem  Text="Sí" Value="True" />
<asp:ListItem  Text="No" Value="False" />
<asp:ListItem  Text="Sin definir" Value="" Selected="True" />
</asp:RadioButtonList>

</td>
</tr>
<tr>
<td  colspan="1" >
Formato especifico
</td>
<td  colspan="1" >
<asp:TextBox  runat="server" ID="insctrl000007" CssClass="form-control" Width="400px" MaxLength="50" Tooltip="Campo124[Formato de identificación. Se pueden utilizar las siguientes variables:{#s#} código de sector, {#n#} código de apartado. {#z#} nro. de documento]" TextMode="MultiLine" onkeypress="return textCounter(this,50);" onkeydown="return textCounter(this,50);" onkeyup="return textCounter(this,50);" onfocus="return textCounter_GetFocus(this,50);" onblur="return textCounter_LostFocus(this);" />
<asp:CompareValidator  runat="server" ID="vcdvalinsctrl000007" SetFocusOnError="true" CssClass="error" ControltoValidate="insctrl000007" Display="Dynamic" Text="Ingrese un texto" ErrorMessage="Formato especifico:Ingrese un texto" Type="String" Operator="DataTypeCheck" ValidationGroup="vgins" />
<asp:RegularExpressionValidator  runat="server" ID="vrgvalinsctrl000007" SetFocusOnError="true" CssClass="error" Display="Dynamic" ControltoValidate="insctrl000007" ValidationExpression="[\w\s-+/*^()!|°$%&=-_,:;.ñÑ~áéíóúÁÉÍÓÚ#}{¿]{0,50}" Text="No mayor a 50 caracteres. Deben ser letras o numeros." ErrorMessage="Formato especifico:No mayor a 50 caracteres. Deben ser letras o numeros." ValidationGroup="vgins" />
<ajaxkit:ValidatorCalloutExtender  runat="server" ID="ajaxvrginsctrl000007" TargetControlID="vrgvalinsctrl000007" />

</td>
</tr>
<tr>
<td  colspan="1" >

</td>
<td  colspan="1" >
<asp:SqlDataSource  runat="server" ID="dsinsctrl000006" CancelSelectOnNullParameter="False" onInit="dsinsctrl000006_Init" SelectCommandType="Text"
 SelectCommand="SELECT EMP.dsc,EMP.cod FROM EMP   WHERE (EMP.baja = 0 OR EMP.baja  IS NULL) ORDER BY EMP.dsc" onselected="dsinsctrl000006_Selected" >
<SelectParameters>
</SelectParameters>
</asp:SqlDataSource>

</td>
</tr>
<tr>
<td  colspan="1" >
Editor
</td>
<td  colspan="1" >
<asp:UpdatePanel  ID="updupdpnlupdinsctrl000006fs" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="True" >
<Triggers>
  <asp:AsyncPostBackTrigger  ControlID="insctrl000006delete" EventName="Click" />
</Triggers>
<ContentTemplate>
<asp:UpdatePanel  ID="updupdinsctrl000006fs" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="True" >
<ContentTemplate>
<asp:Button  runat="server" ID="cmdinsctrl000006showpanel" Text="..." CssClass="boton-acciones" CausesValidation="False" OnClick="cmdinsctrl000006showpanel_Click" />

<asp:ImageButton runat="server" ID="insctrl000006delete" ImageURL="~/imagenes/actdel.png" Width="16" Height="16" Tooltip="Quitar Editor" Visible="False" OnClick="insctrl000006delete_Click" OnDataBinding="insctrl000006delete_DataBinding" CausesValidation="False" />
<asp:LinkButton runat="server" ID="insctrl000006view" CssClass="boton-acciones-subbutton" CausesValidation="False" />
<asp:HiddenField  runat="server" ID="insctrl000006type"/>
<asp:HiddenField  runat="server" ID="insctrl000006" onLoad="insctrl000006_Load"/>
</ContentTemplate>
</asp:UpdatePanel>
<asp:UpdateProgress  runat="server" AssociatedUpdatePanelID="updupdinsctrl000006fs" >
<ProgressTemplate><span  class="ajaxupd" ><div style="position:absolute;top:2px;left:50%; border-color:#333333; border:1; border-style:solid; background-color:#FFFFFF;padding:3px;"><image src="imagenes/ajaxupdate.gif" border=0 />Actualizando</div></span></ProgressTemplate>
</asp:UpdateProgress>
</ContentTemplate>
</asp:UpdatePanel>
<asp:UpdateProgress  runat="server" AssociatedUpdatePanelID="updupdpnlupdinsctrl000006fs" >
<ProgressTemplate><span  class="ajaxupd" ><div style="position:absolute;top:2px;left:50%; border-color:#333333; border:1; border-style:solid; background-color:#FFFFFF;padding:3px;"><image src="imagenes/ajaxupdate.gif" border=0 />Actualizando</div></span></ProgressTemplate>
</asp:UpdateProgress>

</td>
</tr>
</table>
</ContentTemplate>
</ajaxkit:TabPanel>
<ajaxkit:TabPanel ID="instabPanel003" runat="server" HeaderText="Opciones avanzadas" Tooltip="Opciones avanzadas" >
<ContentTemplate>
<table>
<tr>
<td  colspan="1" >
Decision
</td>
<td  colspan="1" >
<asp:Checkbox  runat="server" ID="insctrl000004" CssClass="form-control" />
</td>
</tr>
</ContentTemplate>
</ajaxkit:TabPanel>
</ajaxkit:TabContainer>
</td>
</tr>
</table>
<!-- End Plantilla insercion --></InsertItemTemplate>

</asp:FormView>

			<asp:SqlDataSource  runat="server" ID="dsdatos" ProviderName="System.Data.SqlClient" CancelSelectOnNullParameter="False" onInit="dsdatos_Init" SelectCommandType="Text"
 SelectCommand="SELECT UND.cod,UND.dsc,(UNDUNDTIPCOD.dsc) as undtipcoddsc,(UNDRESP.dsc) as respdsc,(UNDUNDCODSUP.dsc) as undcodsupdsc,UND.sectorid,UND.undnro,UND.orden,(UNDMIEMBROSGRPCOD.grpdsc) as miembrosgrpcoddsc,(UNDGRPCODRESP.grpdsc) as grpcodrespdsc,(UNDGRPCODPRJVER.grpdsc) as grpcodprjverdsc,UND.creatorsmbrdir,UND.formatoespecifico,(UNDEDITOR.dsc) as editordsc,(UNDGRPCODEDITOR.grpdsc) as grpcodeditordsc,UND.baja,UND.decision,(UNDGRPCODMBRDIR.grpdsc) as grpcodmbrdirdsc,(UNDQSECSID.secdsc) as qsecsiddsc,UND.qsecdatetime,UND.undtipcod,UND.resp,UND.undcodsup,UND.miembrosgrpcod,UND.grpcodresp,UND.grpcodprjver,UND.editor,UND.grpcodeditor,UND.grpcodmbrdir,UND.qsecsid FROM UND  LEFT JOIN UND AS UNDUNDCODSUP ON UNDUNDCODSUP.cod=UND.undcodsup LEFT JOIN EMP AS UNDRESP ON UNDRESP.cod=UND.resp LEFT JOIN Q_SECPGRP AS UNDGRPCODPRJVER ON UNDGRPCODPRJVER.grpcod=UND.grpcodprjver LEFT JOIN Q_SECPGRP AS UNDGRPCODRESP ON UNDGRPCODRESP.grpcod=UND.grpcodresp LEFT JOIN Q_SECPGRP AS UNDMIEMBROSGRPCOD ON UNDMIEMBROSGRPCOD.grpcod=UND.miembrosgrpcod LEFT JOIN EMP AS UNDEDITOR ON UNDEDITOR.cod=UND.editor LEFT JOIN Q_SECPGRP AS UNDGRPCODEDITOR ON UNDGRPCODEDITOR.grpcod=UND.grpcodeditor LEFT JOIN DOCTIP AS UNDUNDTIPCOD ON UNDUNDTIPCOD.cod=UND.undtipcod LEFT JOIN Q_SECPGRP AS UNDGRPCODMBRDIR ON UNDGRPCODMBRDIR.grpcod=UND.grpcodmbrdir LEFT JOIN Q_SECPLOGIN AS UNDQSECSID ON UNDQSECSID.sidcod=UND.qsecsid  WHERE (UND.cod = @param1) AND (UND.baja = 0 OR UND.baja  IS NULL)" onselected="dsdatos_Selected" UpdateCommandType="Text"
 UpdateCommand="UPDATE UND SET dsc=@dsc,undcodsup=@undcodsup,resp=@resp,undnro=@undnro,formatoespecifico=@formatoespecifico,editor=@editor,orden=@orden,undtipcod=@undtipcod,decision=@decision,creatorsmbrdir=@creatorsmbrdir,qsecsid=@qsecsid,qsecdatetime=getdate() WHERE UND.cod = @param1" onupdated="dsdatos_Updated" InsertCommandType="Text"
 InsertCommand="INSERT INTO UND (cod,dsc,undtipcod,resp,undcodsup,undnro,orden,creatorsmbrdir,formatoespecifico,editor,baja,decision,qsecsid,qsecdatetime) VALUES(@cod,@dsc,@undtipcod,@resp,@undcodsup,@undnro,@orden,@creatorsmbrdir,@formatoespecifico,@editor,NULL,@decision,@qsecsid,getdate())" onInserted="dsdatos_Inserted" DeleteCommandType="Text"
 DeleteCommand="UPDATE UND SET baja=1 WHERE cod = @param1" ondeleted="dsdatos_Deleted" >
<InsertParameters>
<asp:Parameter  Name="querynextcod" Direction="Output" Type="Int32" />
<asp:ControlParameter  Name="cod" ControlID="frmdatos$insctrl000021" ConvertEmptyStringToNull="True" Direction="InputOutput" Type="Int32" PropertyName="Text" />
<asp:ControlParameter  Name="dsc" ControlID="frmdatos$instabPanel$instabPanel001$insctrl000019" ConvertEmptyStringToNull="True" Direction="InputOutput" Type="String" Size="50" PropertyName="Text" />
<asp:ControlParameter  Name="undtipcod" ControlID="frmdatos$instabPanel$instabPanel001$insctrl000018" ConvertEmptyStringToNull="True" Direction="InputOutput" Type="Int32" PropertyName="SelectedValue" />
<asp:ControlParameter  Name="resp" ControlID="frmdatos$instabPanel$instabPanel001$insctrl000017" ConvertEmptyStringToNull="True" Direction="InputOutput" Type="Int32" PropertyName="Value" />
<asp:ControlParameter  Name="undcodsup" ControlID="frmdatos$instabPanel$instabPanel001$insctrl000016" ConvertEmptyStringToNull="True" Direction="InputOutput" Type="Int32" PropertyName="Value" />
<asp:ControlParameter  Name="undnro" ControlID="frmdatos$instabPanel$instabPanel001$insctrl000014" ConvertEmptyStringToNull="True" Direction="InputOutput" Type="String" Size="12" PropertyName="Text" />
<asp:ControlParameter  Name="orden" ControlID="frmdatos$instabPanel$instabPanel001$insctrl000013" ConvertEmptyStringToNull="True" Direction="InputOutput" Type="Int32" PropertyName="Text" />
<asp:ControlParameter  Name="creatorsmbrdir" ControlID="frmdatos$instabPanel$instabPanel002$insctrl000008" ConvertEmptyStringToNull="True" Direction="InputOutput" Type="Boolean" PropertyName="SelectedValue" />
<asp:ControlParameter  Name="formatoespecifico" ControlID="frmdatos$instabPanel$instabPanel002$insctrl000007" ConvertEmptyStringToNull="True" Direction="InputOutput" Type="String" Size="50" PropertyName="Text" />
<asp:ControlParameter  Name="editor" ControlID="frmdatos$instabPanel$instabPanel002$insctrl000006" ConvertEmptyStringToNull="True" Direction="InputOutput" Type="Int32" PropertyName="Value" />
<asp:ControlParameter  Name="decision" ControlID="frmdatos$instabPanel$instabPanel003$insctrl000004" ConvertEmptyStringToNull="True" Direction="InputOutput" Type="Boolean" PropertyName="Checked" />
<asp:SessionParameter  Name="qsecsid" SessionField="secsid" Direction="Input" />
<asp:Parameter  Name="param1" Direction="ReturnValue" />
</InsertParameters>
<SelectParameters>
<asp:QueryStringParameter  Name="param1" QueryStringField="param1" ConvertEmptyStringToNull="True" Type="String" Size="200" />
</SelectParameters>
<UpdateParameters>
<asp:QueryStringParameter  Name="param1" QueryStringField="param1" ConvertEmptyStringToNull="True" Type="String" Size="200" />
<asp:ControlParameter  Name="dsc" ControlID="frmdatos$edittabPanel$edittabPanel001$editctrl000019" ConvertEmptyStringToNull="True" Direction="InputOutput" Type="String" Size="50" PropertyName="Text" />
<asp:ControlParameter  Name="undtipcod" ControlID="frmdatos$edittabPanel$edittabPanel001$editctrl000018" ConvertEmptyStringToNull="True" Direction="InputOutput" Type="Int32" PropertyName="SelectedValue" />
<asp:ControlParameter  Name="resp" ControlID="frmdatos$edittabPanel$edittabPanel001$editctrl000017" ConvertEmptyStringToNull="True" Direction="InputOutput" Type="Int32" PropertyName="Value" />
<asp:ControlParameter  Name="undcodsup" ControlID="frmdatos$edittabPanel$edittabPanel001$editctrl000016" ConvertEmptyStringToNull="True" Direction="InputOutput" Type="Int32" PropertyName="Value" />
<asp:ControlParameter  Name="undnro" ControlID="frmdatos$edittabPanel$edittabPanel001$editctrl000014" ConvertEmptyStringToNull="True" Direction="InputOutput" Type="String" Size="12" PropertyName="Text" />
<asp:ControlParameter  Name="orden" ControlID="frmdatos$edittabPanel$edittabPanel001$editctrl000013" ConvertEmptyStringToNull="True" Direction="InputOutput" Type="Int32" PropertyName="Text" />
<asp:ControlParameter  Name="creatorsmbrdir" ControlID="frmdatos$edittabPanel$edittabPanel002$editctrl000008" ConvertEmptyStringToNull="True" Direction="InputOutput" Type="Boolean" PropertyName="SelectedValue" />
<asp:ControlParameter  Name="formatoespecifico" ControlID="frmdatos$edittabPanel$edittabPanel002$editctrl000007" ConvertEmptyStringToNull="True" Direction="InputOutput" Type="String" Size="50" PropertyName="Text" />
<asp:ControlParameter  Name="editor" ControlID="frmdatos$edittabPanel$edittabPanel002$editctrl000006" ConvertEmptyStringToNull="True" Direction="InputOutput" Type="Int32" PropertyName="Value" />
<asp:ControlParameter  Name="decision" ControlID="frmdatos$edittabPanel$edittabPanel003$editctrl000004" ConvertEmptyStringToNull="True" Direction="InputOutput" Type="Boolean" PropertyName="Checked" />
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

