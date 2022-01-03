<%@ Page Language="VB" CodeFile="frmGrupos_det.aspx.vb" MasterPageFile="general.master" AutoEventWireup="True" Inherits="frmGrupos_det" Title="Grupos" %>
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
<tr><td  style="width:32px;" colspan="1" ><img  src="imagenes/iconGrupo.png" width="32px" height="32px" alt="Grupos" hspace="4"/></td>
<td  style="text-align:left;" colspan="1" ><span class="form-title">Grupos</span> |<asp:Label  runat="server" ID="lblsubtitle" CssClass="form-subtitle" />
</td></tr>
</table>
</td></tr>
<tr><td  colspan="1" >							<!-- Init -->
<table id="body" style="background-color:#F0F0F0;" width="100%">
								<br /><asp:Label  runat="server" ID="lblerror" CssClass="error" width="100%" />						<asp:ValidationSummary  runat="server" ID="frmdatosvalsumary" />
<asp:FormView  ID="frmdatos" runat="server" DataSourceID="dsdatos" DataKeyNames="grpcod" DefaultMode="ReadOnly" OnItemDeleted="frmdatos_ItemDeleted" OnItemUpdated="frmdatos_ItemUpdated" OnItemInserted="frmdatos_ItemInserted" OnDataBound="frmdatos_ItemSelected" >
<ItemTemplate>

<!-- Init Plantilla item --><div  runat="server" Visible='<%# Request.Querystring("_view_")<> 1 %>' ><asp:Button  runat="server" ID="cmdFormViewConfirmDelete" Text="Borrar" CssClass="boton-acciones" CommandName="Delete" CausesValidation="True" Style="color:#FF0000; font-style:italic;" onClientClick="javascript:return confirm('Confirma la eliminación?')" />
<asp:Button  runat="server" ID="cmdrefresh" Text="Refrescar" CssClass="boton-acciones" CausesValidation="False" />
<asp:Button  runat="server" ID="cmdFormViewItemCancel" Text="Cancelar" CssClass="boton-acciones" CausesValidation="False" OnClick="cmdFormViewItemCancel_Click" />
<asp:Button  runat="server" ID="cmdFormViewItemUpdate" Text="Editar" CssClass="boton-acciones" CausesValidation="False" OnClick="cmdFormViewItemUpdate_Click" />
</div>
<table width="100%" cellpadding="1" cellspacing="1"><tr>
<td  colspan="1" >
Codigo
</td>
<td  colspan="1" >
<asp:Label  runat="server" ID="itemctrl000014" CssClass="form-control-read" width="100%" OnDataBinding="itemctrl000014_Databound" />
</td>
</tr>
<tr>
<td  colspan="1" >
Codigo
</td>
<td  colspan="1" >
<asp:Label  runat="server" ID="itemctrl000012" CssClass="form-control-read" width="100%" OnDataBinding="itemctrl000012_Databound" />
</td>
</tr>
<tr>
<td  colspan="1" >
Descripción
</td>
<td  colspan="1" >
<asp:Label  runat="server" ID="itemctrl000011" CssClass="form-control-read" width="100%" OnDataBinding="itemctrl000011_Databound" />
</td>
</tr>
<tr>
<td  colspan="1" >
Grupo de donde hereda
</td>
<td  colspan="1" >
<asp:LinkButton runat="server" ID="itemctrl000010view" Text='<%# Eval("grpinheritdsc") %>' CssClass="form-control-read" OnDataBinding="itemctrl000010view_DataBinding" CausesValidation="False" OnClientClick='<%# "javascript:if (window.showModalDialog) {window.showModalDialog(" & chr(39) & "frmGrupos_det.aspx?_mode_=0&_closea_=1&isDlg=1&param1=" & Eval("grpinherit") & "&timestamp=" & chr(39) & " + new Date().getTime() + " & chr(39) & "" & chr(39) & ", " & chr(39) & "" & chr(39) & ", " & chr(39) & "menubar=no;resizable=yes;toolbar=no;status=no;center=yes;dialogWidth=750px;dialogHeight=570px;" & chr(39) & ");} else {window.open(" & chr(39) & "frmGrupos_det.aspx?_mode_=0&_closea_=1&isDlg=1&param1=" & Eval("grpinherit") & "&timestamp=" & chr(39) & " + new Date().getTime() + " & chr(39) & "" & chr(39) & ", " & chr(39) & "" & chr(39) & ", " & chr(39) & "modal=yes;directories=no;menubar=no;resizable=yes;toolbar=no;status=no;center=yes;dialogWidth=750px;dialogHeight=570px;" & chr(39) & ");};return false;" %>' PostBackUrl='<%# "javascript:if (window.showModalDialog) {window.showModalDialog(" & chr(39) & "frmGrupos_det.aspx?_mode_=0&_closea_=1&isDlg=1&param1=" & Eval("grpinherit") & "&timestamp=" & chr(39) & " + new Date().getTime() + " & chr(39) & "" & chr(39) & ", " & chr(39) & "" & chr(39) & ", " & chr(39) & "menubar=no;resizable=yes;toolbar=no;status=no;center=yes;dialogWidth=750px;dialogHeight=570px;" & chr(39) & ");} else {window.open(" & chr(39) & "frmGrupos_det.aspx?_mode_=0&_closea_=1&isDlg=1&param1=" & Eval("grpinherit") & "&timestamp=" & chr(39) & " + new Date().getTime() + " & chr(39) & "" & chr(39) & ", " & chr(39) & "" & chr(39) & ", " & chr(39) & "modal=yes;directories=no;menubar=no;resizable=yes;toolbar=no;status=no;center=yes;dialogWidth=750px;dialogHeight=570px;" & chr(39) & ");};return false;" %>' />
<asp:HiddenField  runat="server" ID="itemctrl000010type"/>
<asp:HiddenField  runat="server" ID="itemctrl000010" OnDataBinding="itemctrl000010_DataBound"/>

</td>
</tr>
<tr>
<td  colspan="1" >
Grupo de modelo de donde hereda
</td>
<td  colspan="1" >
<asp:Label  runat="server" ID="itemctrl000009" CssClass="form-control-read" width="100%" OnDataBinding="itemctrl000009_Databound" />
</td>
</tr>
<tr>
<td  colspan="1" >
Deshabilitado
</td>
<td  colspan="1" >
<asp:Label  runat="server" ID="itemctrl000008" CssClass="form-control-read" width="100%" OnDataBinding="itemctrl000008_Databound" />
</td>
</tr>
<tr>
<td  colspan="1" >
ID de seguridad
</td>
<td  colspan="1" >
<asp:LinkButton runat="server" ID="itemctrl000007view" Text='<%# Eval("sidcoddsc") %>' CssClass="form-control-read" OnDataBinding="itemctrl000007view_DataBinding" CausesValidation="False" />
<asp:HiddenField  runat="server" ID="itemctrl000007"/>
<asp:HiddenField  runat="server" ID="itemctrl000007value" OnDataBinding="itemctrl000007value_DataBound"/>

</td>
</tr>
<tr>
<td  colspan="1" >
Identificador único de grupo
</td>
<td  colspan="1" >
<asp:Label  runat="server" ID="itemctrl000006" CssClass="form-control-read" width="100%" OnDataBinding="itemctrl000006_Databound" />
</td>
</tr>
<tr>
<td  colspan="2" >
Miembros de grupos
</td>
</tr>
<tr>
<td  colspan="2" >
<asp:UpdatePanel  ID="updupditemctrl000004" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="True" >
<Triggers>
  <asp:AsyncPostBackTrigger  ControlID="itemctrl000004" EventName="PageIndexChanging" />
</Triggers>
<ContentTemplate>

<asp:GridView  runat="server" ID="itemctrl000004" AllowPaging="True" AllowSorting="True" AutoGenerateColumns="False" CssClass="form-control" PageSize="50"
 GridLines="None" ShowHeader="True" ShowFooter="True" Width="100%" UseAccessibleHeader="False" DataKeyNames="grpmbrcod" CellPadding="2" DataSourceID="itemdsctrl000004" onRowCreated="itemctrl000004_RowCreated" >
<FooterStyle  CssClass="tabla-footer" />
<RowStyle  CssClass="tabla-fila" />
<AlternatingRowStyle  CssClass="tabla-fila-alternativa" />
<SelectedRowStyle  CssClass="tabla-fila" />
<PagerStyle  CssClass="tabla-pager" />
<HeaderStyle  CssClass="tabla-titulo" ForeColor="White" />
<EmptyDataTemplate>
<p  class="tabla-fila">Sin datos</p>
</EmptyDataTemplate>
<Columns>
<asp:TemplateField   ItemStyle-CssClass="tabla-celda" HeaderStyle-CssClass="tabla-titulo" FooterStyle-CssClass="tabla-celda">
<ItemStyle  HorizontalAlign="Left" Width="12px" CssClass="searchresult-table-cell" />
<ItemTemplate>
<asp:Image  runat="server" ID="rowctrl000004imageimg" Width="12px" GenerateEmptyAlternateText="True" ImageURL="imagenes/iconMiembro.png" BorderWidth="0" AlternateText="Imagen no disp." />
</ItemTemplate>
</asp:TemplateField>
<asp:TemplateField   AccessibleHeaderText="ID de seguridad" HeaderText="ID de seguridad" SortExpression="sidcoddsc" ItemStyle-CssClass="tabla-celda" HeaderStyle-CssClass="tabla-titulo" FooterStyle-CssClass="tabla-celda">
<ItemStyle  HorizontalAlign="Left" CssClass="searchresult-table-cell" />
<ItemTemplate>
<asp:LinkButton runat="server" ID="itemctrl000004colitem1view" Text='<%# Eval("sidcoddsc") %>' CssClass="form-control-read" OnDataBinding="itemctrl000004colitem1view_DataBinding" CausesValidation="False" />
</ItemTemplate>
</asp:TemplateField>
</Columns>
</asp:GridView>
</ContentTemplate>
</asp:UpdatePanel>
<asp:UpdateProgress  runat="server" AssociatedUpdatePanelID="updupditemctrl000004" >
<ProgressTemplate><span  class="ajaxupd" ><div style="position:absolute;top:2px;left:50%; border-color:#333333; border:1; border-style:solid; background-color:#FFFFFF;padding:3px;"><image src="imagenes/ajaxupdate.gif" border=0 />Actualizando</div></span></ProgressTemplate>
</asp:UpdateProgress>


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
Codigo
</td>
<td  colspan="1" >
<asp:Label  runat="server" ID="editctrl000012" CssClass="form-control-read" width="100%" OnDataBinding="editctrl000012_Databound" />
</td>
</tr>
<tr>
<td  colspan="1" >
Descripción
</td>
<td  colspan="1" >
<asp:TextBox  runat="server" ID="editctrl000011" CssClass="form-control" Width="400px" MaxLength="250" OnDataBinding="editctrl000011_DataBound_frmdatos" Tooltip="Campo100047[]" TextMode="MultiLine" onkeypress="return textCounter(this,250);" onkeydown="return textCounter(this,250);" onkeyup="return textCounter(this,250);" onfocus="return textCounter_GetFocus(this,250);" onblur="return textCounter_LostFocus(this);" />
<asp:CompareValidator  runat="server" ID="vcdvaleditctrl000011" SetFocusOnError="true" CssClass="error" ControltoValidate="editctrl000011" Display="Dynamic" Text="Ingrese un texto" ErrorMessage="Descripción:Ingrese un texto" Type="String" Operator="DataTypeCheck" ValidationGroup="vgedit" />
<asp:RegularExpressionValidator  runat="server" ID="vrgvaleditctrl000011" SetFocusOnError="true" CssClass="error" Display="Dynamic" ControltoValidate="editctrl000011" ValidationExpression="[\w\s-+/*^()!|°$%&=-_,:;.ñÑ~áéíóúÁÉÍÓÚ#}{]{0,250}" Text="No mayor a 250 caracteres. Deben ser letras o numeros." ErrorMessage="Descripción:No mayor a 250 caracteres. Deben ser letras o numeros." ValidationGroup="vgedit" />
<ajaxkit:ValidatorCalloutExtender  runat="server" ID="ajaxvrgeditctrl000011" TargetControlID="vrgvaleditctrl000011" />

</td>
</tr>
<tr>
<td  colspan="1" >

</td>
<td  colspan="1" >
<asp:SqlDataSource  runat="server" ID="dseditctrl000010" CancelSelectOnNullParameter="False" onInit="dseditctrl000010_Init" SelectCommandType="Text"
 SelectCommand="SELECT Q_SECPGRP.grpdsc,Q_SECPGRP.grpcod FROM Q_SECPGRP   ORDER BY Q_SECPGRP.grpdsc" onselected="dseditctrl000010_Selected" >
<SelectParameters>
</SelectParameters>
</asp:SqlDataSource>

</td>
</tr>
<tr>
<td  colspan="1" >
Grupo de donde hereda
</td>
<td  colspan="1" >
<asp:UpdatePanel  ID="updupdpnlupdeditctrl000010fs" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="True" >
<Triggers>
  <asp:AsyncPostBackTrigger  ControlID="editctrl000010delete" EventName="Click" />
</Triggers>
<ContentTemplate>
<asp:UpdatePanel  ID="updupdeditctrl000010fs" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="True" >
<ContentTemplate>
<asp:Button  runat="server" ID="cmdeditctrl000010showpanel" Text="..." CssClass="boton-acciones" CausesValidation="False" OnClick="cmdeditctrl000010showpanel_Click" />

<asp:ImageButton runat="server" ID="editctrl000010delete" ImageURL="~/imagenes/actdel.png" Width="16" Height="16" Tooltip="Quitar Grupo de donde hereda" Visible="False" OnClick="editctrl000010delete_Click" OnDataBinding="editctrl000010delete_DataBinding" CausesValidation="False" />
<asp:LinkButton runat="server" ID="editctrl000010view" Text='<%# Eval("grpinheritdsc") %>' CssClass="boton-acciones-subbutton" OnDataBinding="editctrl000010view_DataBinding" CausesValidation="False" OnClientClick='<%# "javascript:if (window.showModalDialog) {window.showModalDialog(" & chr(39) & "frmGrupos_det.aspx?_mode_=0&_closea_=1&isDlg=1&param1=" & Eval("grpinherit") & "&timestamp=" & chr(39) & " + new Date().getTime() + " & chr(39) & "" & chr(39) & ", " & chr(39) & "" & chr(39) & ", " & chr(39) & "menubar=no;resizable=yes;toolbar=no;status=no;center=yes;dialogWidth=750px;dialogHeight=570px;" & chr(39) & ");} else {window.open(" & chr(39) & "frmGrupos_det.aspx?_mode_=0&_closea_=1&isDlg=1&param1=" & Eval("grpinherit") & "&timestamp=" & chr(39) & " + new Date().getTime() + " & chr(39) & "" & chr(39) & ", " & chr(39) & "" & chr(39) & ", " & chr(39) & "modal=yes;directories=no;menubar=no;resizable=yes;toolbar=no;status=no;center=yes;dialogWidth=750px;dialogHeight=570px;" & chr(39) & ");};return false;" %>' PostBackUrl='<%# "javascript:if (window.showModalDialog) {window.showModalDialog(" & chr(39) & "frmGrupos_det.aspx?_mode_=0&_closea_=1&isDlg=1&param1=" & Eval("grpinherit") & "&timestamp=" & chr(39) & " + new Date().getTime() + " & chr(39) & "" & chr(39) & ", " & chr(39) & "" & chr(39) & ", " & chr(39) & "menubar=no;resizable=yes;toolbar=no;status=no;center=yes;dialogWidth=750px;dialogHeight=570px;" & chr(39) & ");} else {window.open(" & chr(39) & "frmGrupos_det.aspx?_mode_=0&_closea_=1&isDlg=1&param1=" & Eval("grpinherit") & "&timestamp=" & chr(39) & " + new Date().getTime() + " & chr(39) & "" & chr(39) & ", " & chr(39) & "" & chr(39) & ", " & chr(39) & "modal=yes;directories=no;menubar=no;resizable=yes;toolbar=no;status=no;center=yes;dialogWidth=750px;dialogHeight=570px;" & chr(39) & ");};return false;" %>' />
<asp:HiddenField  runat="server" ID="editctrl000010type"/>
<asp:HiddenField  runat="server" ID="editctrl000010" OnDataBinding="editctrl000010_DataBound"/>
</ContentTemplate>
</asp:UpdatePanel>
<asp:UpdateProgress  runat="server" AssociatedUpdatePanelID="updupdeditctrl000010fs" >
<ProgressTemplate><span  class="ajaxupd" ><div style="position:absolute;top:2px;left:50%; border-color:#333333; border:1; border-style:solid; background-color:#FFFFFF;padding:3px;"><image src="imagenes/ajaxupdate.gif" border=0 />Actualizando</div></span></ProgressTemplate>
</asp:UpdateProgress>
</ContentTemplate>
</asp:UpdatePanel>
<asp:UpdateProgress  runat="server" AssociatedUpdatePanelID="updupdpnlupdeditctrl000010fs" >
<ProgressTemplate><span  class="ajaxupd" ><div style="position:absolute;top:2px;left:50%; border-color:#333333; border:1; border-style:solid; background-color:#FFFFFF;padding:3px;"><image src="imagenes/ajaxupdate.gif" border=0 />Actualizando</div></span></ProgressTemplate>
</asp:UpdateProgress>

</td>
</tr>
<tr>
<td  colspan="1" >
Grupo de modelo de donde hereda
</td>
<td  colspan="1" >
<asp:TextBox  runat="server" ID="editctrl000009" CssClass="form-control" Width="40px" OnDataBinding="editctrl000009_DataBound_frmdatos" Tooltip="Campo100049[]" />
<asp:CompareValidator  runat="server" ID="vcdvaleditctrl000009" SetFocusOnError="true" CssClass="error" ControltoValidate="editctrl000009" Display="Dynamic" Text='Ingrese un entero' ErrorMessage='Grupo de modelo de donde hereda:Ingrese un entero' Type="Integer" Operator="DataTypeCheck" ValidationGroup="vgedit" />
<asp:RangeValidator  runat="server" ID="vrnvaleditctrl000009" SetFocusOnError="true" CssClass="error" Display="Dynamic" ControltoValidate="editctrl000009" Text="Ingrese un valor entre 1 y 2000000000" ErrorMessage="Grupo de modelo de donde hereda:Ingrese un valor entre 1 y 2000000000" Type="Integer" CultureInvariantValues="True" MaximumValue="2000000000" MinimumValue="1" ValidationGroup="vgedit" />
<ajaxkit:ValidatorCalloutExtender  runat="server" ID="ajaxvrneditctrl000009" TargetControlID="vrnvaleditctrl000009" />

</td>
</tr>
<tr>
<td  colspan="1" >
Deshabilitado
</td>
<td  colspan="1" >
<asp:Checkbox  runat="server" ID="editctrl000008" CssClass="form-control" OnDataBinding="editctrl000008_Databound" />
</td>
</tr>
<tr>
<td  colspan="1" >
ID de seguridad
</td>
<td  colspan="1" >
<asp:LinkButton runat="server" ID="editctrl000007view" Text='<%# Eval("sidcoddsc") %>' CssClass="form-control-read" OnDataBinding="editctrl000007view_DataBinding" CausesValidation="False" />
<asp:HiddenField  runat="server" ID="editctrl000007"/>
<asp:HiddenField  runat="server" ID="editctrl000007value" OnDataBinding="editctrl000007value_DataBound"/>

</td>
</tr>
<tr>
<td  colspan="1" >
Identificador único de grupo
</td>
<td  colspan="1" >
<asp:TextBox  runat="server" ID="editctrl000006" CssClass="form-control" Width="40px" OnDataBinding="editctrl000006_DataBound_frmdatos" Tooltip="Campo100052[Este campo indica el identificador único de grupo utilizado en desarrollo y la correspondencia en producción]" />
<asp:CompareValidator  runat="server" ID="vcdvaleditctrl000006" SetFocusOnError="true" CssClass="error" ControltoValidate="editctrl000006" Display="Dynamic" Text='Ingrese un entero' ErrorMessage='Identificador único de grupo:Ingrese un entero' Type="Integer" Operator="DataTypeCheck" ValidationGroup="vgedit" />
<asp:RangeValidator  runat="server" ID="vrnvaleditctrl000006" SetFocusOnError="true" CssClass="error" Display="Dynamic" ControltoValidate="editctrl000006" Text="Ingrese un valor entre 1 y 2000000000" ErrorMessage="Identificador único de grupo:Ingrese un valor entre 1 y 2000000000" Type="Integer" CultureInvariantValues="True" MaximumValue="2000000000" MinimumValue="1" ValidationGroup="vgedit" />
<ajaxkit:ValidatorCalloutExtender  runat="server" ID="ajaxvrneditctrl000006" TargetControlID="vrnvaleditctrl000006" />

</td>
</tr>
<tr>
<td  colspan="2" >
Miembros de grupos
</td>
</tr>
<tr>
<td  colspan="2" >
<asp:UpdatePanel  ID="updupdeditctrl000004" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="True" >
<Triggers>
  <asp:AsyncPostBackTrigger  ControlID="editctrl000004" EventName="PageIndexChanging" />
</Triggers>
<ContentTemplate>

<asp:GridView  runat="server" ID="editctrl000004" AllowPaging="True" AllowSorting="True" AutoGenerateColumns="False" CssClass="form-control" PageSize="50"
 GridLines="None" ShowHeader="True" ShowFooter="True" Width="100%" UseAccessibleHeader="False" DataKeyNames="grpmbrcod" CellPadding="2" DataSourceID="editdsctrl000004" onRowCreated="editctrl000004_RowCreated" onRowCommand="editctrl000004_RowCommand" >
<FooterStyle  CssClass="tabla-footer" />
<RowStyle  CssClass="tabla-fila" />
<AlternatingRowStyle  CssClass="tabla-fila-alternativa" />
<SelectedRowStyle  CssClass="tabla-fila" />
<PagerStyle  CssClass="tabla-pager" />
<HeaderStyle  CssClass="tabla-titulo" ForeColor="White" />
<EmptyDataTemplate>
<p  class="tabla-fila">Sin datos</p>
</br></br></br></br><asp:Button  runat="server" ID="cmdFormViewInsert" Text="Nuevo" CssClass="boton-acciones" CommandName="HRC_INSERT" CausesValidation="False" /></EmptyDataTemplate>
<Columns>
<asp:TemplateField   ItemStyle-CssClass="tabla-celda" HeaderStyle-CssClass="tabla-titulo" FooterStyle-CssClass="tabla-celda">
<ItemStyle  HorizontalAlign="Left" Width="12px" CssClass="searchresult-table-cell" />
<ItemTemplate>
<asp:Image  runat="server" ID="rowctrl000004imageimg" Width="12px" GenerateEmptyAlternateText="True" ImageURL="imagenes/iconMiembro.png" BorderWidth="0" AlternateText="Imagen no disp." />
</ItemTemplate>
</asp:TemplateField>
<asp:TemplateField   AccessibleHeaderText="ID de seguridad" HeaderText="ID de seguridad" SortExpression="sidcoddsc" ItemStyle-CssClass="tabla-celda" HeaderStyle-CssClass="tabla-titulo" FooterStyle-CssClass="tabla-celda">
<ItemStyle  HorizontalAlign="Left" CssClass="searchresult-table-cell" />
<ItemTemplate>
<asp:LinkButton runat="server" ID="editctrl000004colitem1view" Text='<%# Eval("sidcoddsc") %>' CssClass="form-control-read" OnDataBinding="editctrl000004colitem1view_DataBinding" CausesValidation="False" />
</ItemTemplate>
</asp:TemplateField>
<asp:TemplateField   AccessibleHeaderText="Acciones" HeaderText="Acciones">
<ItemStyle  HorizontalAlign="Center" Width="3%" />
<ItemTemplate>
 <asp:ImageButton  ID="imge" runat="server" CausesValidation="False" CommandName="Edit" CommandArgument='<%# Eval("grpmbrcod") %>' ImageUrl="./imagenes/actmod.png" Width="16" Text="Editar" />
 <asp:ImageButton  ID="imgd" runat="server" CausesValidation="False" CommandName="Delete" CommandArgument='<%# Eval("grpmbrcod") %>' ImageUrl="./imagenes/actdel.png" Text="Borrar" Width="16" OnClientClick=" return confirm('Confirma la eliminación?');" />
</ItemTemplate>
<FooterTemplate>
<asp:Button  ID="cmdi" runat="server" CausesValidation="False" CommandName="HRC_INSERT" CssClass="boton-acciones" Text="Agregar" />
</FooterTemplate>
</asp:TemplateField>
<asp:TemplateField   ItemStyle-CssClass="tabla-celda" HeaderStyle-CssClass="tabla-titulo" FooterStyle-CssClass="tabla-celda">
<ItemStyle  HorizontalAlign="Left" Width="16" />
<ItemTemplate>
<asp:ImageButton runat="server" ID="cmdVer" ImageURL="./imagenes/actview.png" Width="16" Height="16" CommandName="Select" CausesValidation="False" CommandArgument='<%# Eval("grpmbrcod") %>' /></ItemTemplate>
</asp:TemplateField>
</Columns>
</asp:GridView>
</ContentTemplate>
</asp:UpdatePanel>
<asp:UpdateProgress  runat="server" AssociatedUpdatePanelID="updupdeditctrl000004" >
<ProgressTemplate><span  class="ajaxupd" ><div style="position:absolute;top:2px;left:50%; border-color:#333333; border:1; border-style:solid; background-color:#FFFFFF;padding:3px;"><image src="imagenes/ajaxupdate.gif" border=0 />Actualizando</div></span></ProgressTemplate>
</asp:UpdateProgress>


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
Descripción
</td>
<td  colspan="1" >
<asp:TextBox  runat="server" ID="insctrl000011" CssClass="form-control" Width="400px" MaxLength="250" Tooltip="Campo100047[]" TextMode="MultiLine" onkeypress="return textCounter(this,250);" onkeydown="return textCounter(this,250);" onkeyup="return textCounter(this,250);" onfocus="return textCounter_GetFocus(this,250);" onblur="return textCounter_LostFocus(this);" />
<asp:CompareValidator  runat="server" ID="vcdvalinsctrl000011" SetFocusOnError="true" CssClass="error" ControltoValidate="insctrl000011" Display="Dynamic" Text="Ingrese un texto" ErrorMessage="Descripción:Ingrese un texto" Type="String" Operator="DataTypeCheck" ValidationGroup="vgins" />
<asp:RegularExpressionValidator  runat="server" ID="vrgvalinsctrl000011" SetFocusOnError="true" CssClass="error" Display="Dynamic" ControltoValidate="insctrl000011" ValidationExpression="[\w\s-+/*^()!|°$%&=-_,:;.ñÑ~áéíóúÁÉÍÓÚ#}{]{0,250}" Text="No mayor a 250 caracteres. Deben ser letras o numeros." ErrorMessage="Descripción:No mayor a 250 caracteres. Deben ser letras o numeros." ValidationGroup="vgins" />
<ajaxkit:ValidatorCalloutExtender  runat="server" ID="ajaxvrginsctrl000011" TargetControlID="vrgvalinsctrl000011" />

</td>
</tr>
<tr>
<td  colspan="1" >

</td>
<td  colspan="1" >
<asp:SqlDataSource  runat="server" ID="dsinsctrl000010" CancelSelectOnNullParameter="False" onInit="dsinsctrl000010_Init" SelectCommandType="Text"
 SelectCommand="SELECT Q_SECPGRP.grpdsc,Q_SECPGRP.grpcod FROM Q_SECPGRP   ORDER BY Q_SECPGRP.grpdsc" onselected="dsinsctrl000010_Selected" >
<SelectParameters>
</SelectParameters>
</asp:SqlDataSource>

</td>
</tr>
<tr>
<td  colspan="1" >
Grupo de donde hereda
</td>
<td  colspan="1" >
<asp:UpdatePanel  ID="updupdpnlupdinsctrl000010fs" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="True" >
<Triggers>
  <asp:AsyncPostBackTrigger  ControlID="insctrl000010delete" EventName="Click" />
</Triggers>
<ContentTemplate>
<asp:UpdatePanel  ID="updupdinsctrl000010fs" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="True" >
<ContentTemplate>
<asp:Button  runat="server" ID="cmdinsctrl000010showpanel" Text="..." CssClass="boton-acciones" CausesValidation="False" OnClick="cmdinsctrl000010showpanel_Click" />

<asp:ImageButton runat="server" ID="insctrl000010delete" ImageURL="~/imagenes/actdel.png" Width="16" Height="16" Tooltip="Quitar Grupo de donde hereda" Visible="False" OnClick="insctrl000010delete_Click" OnDataBinding="insctrl000010delete_DataBinding" CausesValidation="False" />
<asp:LinkButton runat="server" ID="insctrl000010view" CssClass="boton-acciones-subbutton" CausesValidation="False" />
<asp:HiddenField  runat="server" ID="insctrl000010type"/>
<asp:HiddenField  runat="server" ID="insctrl000010"/>
</ContentTemplate>
</asp:UpdatePanel>
<asp:UpdateProgress  runat="server" AssociatedUpdatePanelID="updupdinsctrl000010fs" >
<ProgressTemplate><span  class="ajaxupd" ><div style="position:absolute;top:2px;left:50%; border-color:#333333; border:1; border-style:solid; background-color:#FFFFFF;padding:3px;"><image src="imagenes/ajaxupdate.gif" border=0 />Actualizando</div></span></ProgressTemplate>
</asp:UpdateProgress>
</ContentTemplate>
</asp:UpdatePanel>
<asp:UpdateProgress  runat="server" AssociatedUpdatePanelID="updupdpnlupdinsctrl000010fs" >
<ProgressTemplate><span  class="ajaxupd" ><div style="position:absolute;top:2px;left:50%; border-color:#333333; border:1; border-style:solid; background-color:#FFFFFF;padding:3px;"><image src="imagenes/ajaxupdate.gif" border=0 />Actualizando</div></span></ProgressTemplate>
</asp:UpdateProgress>

</td>
</tr>
<tr>
<td  colspan="1" >
Grupo de modelo de donde hereda
</td>
<td  colspan="1" >
<asp:TextBox  runat="server" ID="insctrl000009" CssClass="form-control" Width="40px" Tooltip="Campo100049[]" />
<asp:CompareValidator  runat="server" ID="vcdvalinsctrl000009" SetFocusOnError="true" CssClass="error" ControltoValidate="insctrl000009" Display="Dynamic" Text='Ingrese un entero' ErrorMessage='Grupo de modelo de donde hereda:Ingrese un entero' Type="Integer" Operator="DataTypeCheck" ValidationGroup="vgins" />
<asp:RangeValidator  runat="server" ID="vrnvalinsctrl000009" SetFocusOnError="true" CssClass="error" Display="Dynamic" ControltoValidate="insctrl000009" Text="Ingrese un valor entre 1 y 2000000000" ErrorMessage="Grupo de modelo de donde hereda:Ingrese un valor entre 1 y 2000000000" Type="Integer" CultureInvariantValues="True" MaximumValue="2000000000" MinimumValue="1" ValidationGroup="vgins" />
<ajaxkit:ValidatorCalloutExtender  runat="server" ID="ajaxvrninsctrl000009" TargetControlID="vrnvalinsctrl000009" />

</td>
</tr>
<tr>
<td  colspan="1" >
Deshabilitado
</td>
<td  colspan="1" >
<asp:Checkbox  runat="server" ID="insctrl000008" CssClass="form-control" />
</td>
</tr>
<tr>
<td  colspan="1" >
Identificador único de grupo
</td>
<td  colspan="1" >
<asp:TextBox  runat="server" ID="insctrl000006" CssClass="form-control" Width="40px" Tooltip="Campo100052[Este campo indica el identificador único de grupo utilizado en desarrollo y la correspondencia en producción]" />
<asp:CompareValidator  runat="server" ID="vcdvalinsctrl000006" SetFocusOnError="true" CssClass="error" ControltoValidate="insctrl000006" Display="Dynamic" Text='Ingrese un entero' ErrorMessage='Identificador único de grupo:Ingrese un entero' Type="Integer" Operator="DataTypeCheck" ValidationGroup="vgins" />
<asp:RangeValidator  runat="server" ID="vrnvalinsctrl000006" SetFocusOnError="true" CssClass="error" Display="Dynamic" ControltoValidate="insctrl000006" Text="Ingrese un valor entre 1 y 2000000000" ErrorMessage="Identificador único de grupo:Ingrese un valor entre 1 y 2000000000" Type="Integer" CultureInvariantValues="True" MaximumValue="2000000000" MinimumValue="1" ValidationGroup="vgins" />
<ajaxkit:ValidatorCalloutExtender  runat="server" ID="ajaxvrninsctrl000006" TargetControlID="vrnvalinsctrl000006" />

</td>
</tr>
</table>
<!-- End Plantilla insercion --></InsertItemTemplate>

</asp:FormView>

						<asp:SqlDataSource  runat="server" ID="dsdatos" ProviderName="System.Data.SqlClient" CancelSelectOnNullParameter="False" onInit="dsdatos_Init" SelectCommandType="Text"
 SelectCommand="SELECT Q_SECPGRP.grpcod,Q_SECPGRP.grpdsc,(Q_SECPGRPGRPINHERIT.grpdsc) as grpinheritdsc,Q_SECPGRP.grpcodrel,Q_SECPGRP.grpdisabled,CASE Q_SECPGRPSIDCOD.sidtypecod WHEN -3 THEN (SELECT TOP 1 Q_SECPGRP.grpcod FROM Q_SECPGRP WHERE Q_SECPGRP.sidcod=Q_SECPGRPSIDCOD.sidcod) WHEN -2 THEN (SELECT TOP 1 Q_SECPLOGIN.seccod FROM Q_SECPLOGIN WHERE Q_SECPLOGIN.sidcod=Q_SECPGRPSIDCOD.sidcod) END as sidcodvalue,CASE Q_SECPGRPSIDCOD.sidtypecod WHEN -3 THEN (SELECT TOP 1 Q_SECPGRP.grpdsc FROM Q_SECPGRP WHERE Q_SECPGRP.grpcod= (SELECT TOP 1 Q_SECPGRP.grpcod FROM Q_SECPGRP WHERE Q_SECPGRP.sidcod=Q_SECPGRPSIDCOD.sidcod)) WHEN -2 THEN (SELECT TOP 1 Q_SECPLOGIN.secdsc FROM Q_SECPLOGIN WHERE Q_SECPLOGIN.seccod= (SELECT TOP 1 Q_SECPLOGIN.seccod FROM Q_SECPLOGIN WHERE Q_SECPLOGIN.sidcod=Q_SECPGRPSIDCOD.sidcod)) END as sidcoddsc,(Q_SECPGRPSIDCOD.sidtypecod) as sidcoddsc,Q_SECPGRP.grpid,(Q_SECPGRPQSECSID.secdsc) as qsecsiddsc,Q_SECPGRP.qsecdatetime,Q_SECPGRP.grpinherit,Q_SECPGRP.sidcod,Q_SECPGRP.qsecsid FROM Q_SECPGRP  LEFT JOIN Q_SECPGRP AS Q_SECPGRPGRPINHERIT ON Q_SECPGRPGRPINHERIT.grpcod=Q_SECPGRP.grpinherit LEFT JOIN Q_SECPSID AS Q_SECPGRPSIDCOD ON Q_SECPGRPSIDCOD.sidcod=Q_SECPGRP.sidcod LEFT JOIN Q_SECPLOGIN AS Q_SECPGRPQSECSID ON Q_SECPGRPQSECSID.sidcod=Q_SECPGRP.qsecsid  WHERE Q_SECPGRP.grpcod = @param1" onselected="dsdatos_Selected" UpdateCommandType="Text"
 UpdateCommand="UPDATE Q_SECPGRP SET grpcod=@grpcod,grpdsc=@grpdsc,grpinherit=@grpinherit,grpcodrel=@grpcodrel,grpdisabled=@grpdisabled,grpid=@grpid,qsecsid=@qsecsid,qsecdatetime=getdate() WHERE Q_SECPGRP.grpcod = @param1" onupdated="dsdatos_Updated" InsertCommandType="Text"
 InsertCommand="DECLARE @querynextcodgrpcod int  SET @querynextcodgrpcod =(SELECT ISNULL(MAX(grpcod),0)+1 FROM Q_SECPGRP WHERE grpcod > 0 ) INSERT INTO Q_SECPGRP (grpcod,grpdsc,grpinherit,grpcodrel,grpdisabled,grpid,qsecsid,qsecdatetime) VALUES(@querynextcodgrpcod,@grpdsc,@grpinherit,@grpcodrel,@grpdisabled,@grpid,@qsecsid,getdate()) SELECT @querynextcod =@querynextcodgrpcod" onInserted="dsdatos_Inserted" DeleteCommandType="Text"
 DeleteCommand="DELETE FROM Q_SECPGRP WHERE grpcod = @param1" ondeleted="dsdatos_Deleted" >
<InsertParameters>
<asp:Parameter  Name="querynextcod" Direction="Output" Type="Int32" />
<asp:ControlParameter  Name="grpdsc" ControlID="frmdatos$insctrl000011" ConvertEmptyStringToNull="True" Direction="InputOutput" Type="String" Size="250" PropertyName="Text" />
<asp:ControlParameter  Name="grpinherit" ControlID="frmdatos$insctrl000010" ConvertEmptyStringToNull="True" Direction="InputOutput" Type="Int32" PropertyName="Value" />
<asp:ControlParameter  Name="grpcodrel" ControlID="frmdatos$insctrl000009" ConvertEmptyStringToNull="True" Direction="InputOutput" Type="Int32" PropertyName="Text" />
<asp:ControlParameter  Name="grpdisabled" ControlID="frmdatos$insctrl000008" ConvertEmptyStringToNull="True" Direction="InputOutput" Type="Boolean" PropertyName="Checked" />
<asp:ControlParameter  Name="grpid" ControlID="frmdatos$insctrl000006" ConvertEmptyStringToNull="True" Direction="InputOutput" Type="Int32" PropertyName="Text" />
<asp:SessionParameter  Name="qsecsid" SessionField="secsid" Direction="Input" />
<asp:Parameter  Name="param1" Direction="ReturnValue" />
</InsertParameters>
<SelectParameters>
<asp:QueryStringParameter  Name="param1" QueryStringField="param1" ConvertEmptyStringToNull="True" Type="String" Size="200" />
</SelectParameters>
<UpdateParameters>
<asp:QueryStringParameter  Name="param1" QueryStringField="param1" ConvertEmptyStringToNull="True" Type="String" Size="200" />
<asp:ControlParameter  Name="grpcod" ControlID="frmdatos$editctrl000012" ConvertEmptyStringToNull="True" Direction="InputOutput" Type="Int32" PropertyName="Text" />
<asp:ControlParameter  Name="grpdsc" ControlID="frmdatos$editctrl000011" ConvertEmptyStringToNull="True" Direction="InputOutput" Type="String" Size="250" PropertyName="Text" />
<asp:ControlParameter  Name="grpinherit" ControlID="frmdatos$editctrl000010" ConvertEmptyStringToNull="True" Direction="InputOutput" Type="Int32" PropertyName="Value" />
<asp:ControlParameter  Name="grpcodrel" ControlID="frmdatos$editctrl000009" ConvertEmptyStringToNull="True" Direction="InputOutput" Type="Int32" PropertyName="Text" />
<asp:ControlParameter  Name="grpdisabled" ControlID="frmdatos$editctrl000008" ConvertEmptyStringToNull="True" Direction="InputOutput" Type="Boolean" PropertyName="Checked" />
<asp:ControlParameter  Name="grpid" ControlID="frmdatos$editctrl000006" ConvertEmptyStringToNull="True" Direction="InputOutput" Type="Int32" PropertyName="Text" />
<asp:SessionParameter  Name="qsecsid" SessionField="secsid" Direction="Input" />
</UpdateParameters>
<DeleteParameters>
<asp:QueryStringParameter  Name="param1" QueryStringField="param1" ConvertEmptyStringToNull="True" Type="String" Size="200" />
<asp:SessionParameter  Name="qsecsid" SessionField="secsid" Direction="Input" />
</DeleteParameters>
</asp:SqlDataSource>
					<asp:SqlDataSource  runat="server" ID="itemdsctrl000004" ProviderName="System.Data.SqlClient" CancelSelectOnNullParameter="False" onInit="itemdsctrl000004_Init" SelectCommandType="Text"
 SelectCommand="SELECT Q_SECPGRPMBR.grpmbrcod,(Q_SECPGRPMBRGRPCOD.grpdsc) as grpcoddsc,CASE Q_SECPGRPMBRSIDCOD.sidtypecod WHEN -3 THEN (SELECT TOP 1 Q_SECPGRP.grpcod FROM Q_SECPGRP WHERE Q_SECPGRP.sidcod=Q_SECPGRPMBRSIDCOD.sidcod) WHEN -2 THEN (SELECT TOP 1 Q_SECPLOGIN.seccod FROM Q_SECPLOGIN WHERE Q_SECPLOGIN.sidcod=Q_SECPGRPMBRSIDCOD.sidcod) END as sidcodvalue,CASE Q_SECPGRPMBRSIDCOD.sidtypecod WHEN -3 THEN (SELECT TOP 1 Q_SECPGRP.grpdsc FROM Q_SECPGRP WHERE Q_SECPGRP.grpcod= (SELECT TOP 1 Q_SECPGRP.grpcod FROM Q_SECPGRP WHERE Q_SECPGRP.sidcod=Q_SECPGRPMBRSIDCOD.sidcod)) WHEN -2 THEN (SELECT TOP 1 Q_SECPLOGIN.secdsc FROM Q_SECPLOGIN WHERE Q_SECPLOGIN.seccod= (SELECT TOP 1 Q_SECPLOGIN.seccod FROM Q_SECPLOGIN WHERE Q_SECPLOGIN.sidcod=Q_SECPGRPMBRSIDCOD.sidcod)) END as sidcoddsc,(Q_SECPGRPMBRSIDCOD.sidtypecod) as sidcoddsc,(Q_SECPGRPMBRQSECSID.secdsc) as qsecsiddsc,Q_SECPGRPMBR.qsecdatetime,Q_SECPGRPMBR.grpcod,Q_SECPGRPMBR.sidcod,Q_SECPGRPMBR.qsecsid FROM Q_SECPGRPMBR AS Q_SECPGRPMBR  LEFT JOIN Q_SECPGRP AS Q_SECPGRPMBRGRPCOD ON Q_SECPGRPMBRGRPCOD.grpcod=Q_SECPGRPMBR.grpcod LEFT JOIN Q_SECPSID AS Q_SECPGRPMBRSIDCOD ON Q_SECPGRPMBRSIDCOD.sidcod=Q_SECPGRPMBR.sidcod LEFT JOIN Q_SECPLOGIN AS Q_SECPGRPMBRQSECSID ON Q_SECPGRPMBRQSECSID.sidcod=Q_SECPGRPMBR.qsecsid  WHERE (Q_SECPGRPMBR.grpcod= @param1) AND Q_SECPGRPMBR.grpmbrcod >= 1" onselected="itemdsctrl000004_Selected" >
<SelectParameters>
<asp:ControlParameter  Name="param1" ControlID="frmdatos" ConvertEmptyStringToNull="True" Direction="InputOutput" Type="String" Size="200" PropertyName="SelectedValue" />
</SelectParameters>
</asp:SqlDataSource>
				<asp:SqlDataSource  runat="server" ID="editdsctrl000004" ProviderName="System.Data.SqlClient" CancelSelectOnNullParameter="False" onInit="editdsctrl000004_Init" SelectCommandType="Text"
 SelectCommand="SELECT Q_SECPGRPMBR.grpmbrcod,(Q_SECPGRPMBRGRPCOD.grpdsc) as grpcoddsc,CASE Q_SECPGRPMBRSIDCOD.sidtypecod WHEN -3 THEN (SELECT TOP 1 Q_SECPGRP.grpcod FROM Q_SECPGRP WHERE Q_SECPGRP.sidcod=Q_SECPGRPMBRSIDCOD.sidcod) WHEN -2 THEN (SELECT TOP 1 Q_SECPLOGIN.seccod FROM Q_SECPLOGIN WHERE Q_SECPLOGIN.sidcod=Q_SECPGRPMBRSIDCOD.sidcod) END as sidcodvalue,CASE Q_SECPGRPMBRSIDCOD.sidtypecod WHEN -3 THEN (SELECT TOP 1 Q_SECPGRP.grpdsc FROM Q_SECPGRP WHERE Q_SECPGRP.grpcod= (SELECT TOP 1 Q_SECPGRP.grpcod FROM Q_SECPGRP WHERE Q_SECPGRP.sidcod=Q_SECPGRPMBRSIDCOD.sidcod)) WHEN -2 THEN (SELECT TOP 1 Q_SECPLOGIN.secdsc FROM Q_SECPLOGIN WHERE Q_SECPLOGIN.seccod= (SELECT TOP 1 Q_SECPLOGIN.seccod FROM Q_SECPLOGIN WHERE Q_SECPLOGIN.sidcod=Q_SECPGRPMBRSIDCOD.sidcod)) END as sidcoddsc,(Q_SECPGRPMBRSIDCOD.sidtypecod) as sidcoddsc,(Q_SECPGRPMBRQSECSID.secdsc) as qsecsiddsc,Q_SECPGRPMBR.qsecdatetime,Q_SECPGRPMBR.grpcod,Q_SECPGRPMBR.sidcod,Q_SECPGRPMBR.qsecsid FROM Q_SECPGRPMBR AS Q_SECPGRPMBR  LEFT JOIN Q_SECPGRP AS Q_SECPGRPMBRGRPCOD ON Q_SECPGRPMBRGRPCOD.grpcod=Q_SECPGRPMBR.grpcod LEFT JOIN Q_SECPSID AS Q_SECPGRPMBRSIDCOD ON Q_SECPGRPMBRSIDCOD.sidcod=Q_SECPGRPMBR.sidcod LEFT JOIN Q_SECPLOGIN AS Q_SECPGRPMBRQSECSID ON Q_SECPGRPMBRQSECSID.sidcod=Q_SECPGRPMBR.qsecsid  WHERE (Q_SECPGRPMBR.grpcod= @param1) AND Q_SECPGRPMBR.grpmbrcod >= 1" onselected="editdsctrl000004_Selected" UpdateCommandType="Text"
 UpdateCommand="UPDATE Q_SECPGRPMBR SET grpmbrcod=@grpmbrcod,sidcod=@sidcod WHERE grpmbrcod=@grpmbrcod" onupdated="editdsctrl000004_Updated" InsertCommandType="Text"
 InsertCommand="INSERT INTO Q_SECPGRPMBR (grpmbrcod,sidcod) VALUES(@grpmbrcod,@sidcod)" onInserted="editdsctrl000004_Inserted" DeleteCommandType="Text"
 DeleteCommand="DELETE FROM Q_SECPGRPMBR WHERE grpmbrcod=@grpmbrcod" ondeleted="editdsctrl000004_Deleted" >
<InsertParameters>
<asp:ControlParameter  Name="param1" ControlID="frmdatos" ConvertEmptyStringToNull="True" Direction="InputOutput" Type="String" Size="200" PropertyName="SelectedValue" />
<asp:SessionParameter  Name="querynextcod" SessionField="4" Direction="Output" Type="Int32" />
</InsertParameters>
<SelectParameters>
<asp:ControlParameter  Name="param1" ControlID="frmdatos" ConvertEmptyStringToNull="True" Direction="InputOutput" Type="String" Size="200" PropertyName="SelectedValue" />
</SelectParameters>
<UpdateParameters>
<asp:ControlParameter  Name="param1" ControlID="frmdatos" ConvertEmptyStringToNull="True" Direction="InputOutput" Type="String" Size="200" PropertyName="SelectedValue" />
</UpdateParameters>
<DeleteParameters>
<asp:ControlParameter  Name="param1" ControlID="frmdatos" ConvertEmptyStringToNull="True" Direction="InputOutput" Type="String" Size="200" PropertyName="SelectedValue" />
</DeleteParameters>
</asp:SqlDataSource>
		<tr>			<td  style="vertical-align:top;" colspan="4" >			<!-- Panel updpanelfrmupdpanelctrl000004 -->
<asp:Label  runat="server" ID="lblpnlupdpanelfrmupdpanelctrl000004" width="100%" />
<ajaxkit:ModalPopupExtender  runat="server" ID="updpanelfrmupdpanelctrl000004" DropShadow="False" EnableViewState="True" PopupControlID="pnlupdpanelfrmupdpanelctrl000004" TargetControlID="lblpnlupdpanelfrmupdpanelctrl000004" BackgroundCssClass="bgmodalpopup" />
<asp:Panel  ID="pnlupdpanelfrmupdpanelctrl000004" runat="server" BorderWidth="1" BorderStyle="solid" >
<asp:UpdatePanel  ID="updupdpanelfrmupdpanelctrl000004" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="True" >
<ContentTemplate>
<table class="form" cellpadding="0" cellspacing="0"><tr><td  colspan="1" ><tr><td  colspan="1" >
<asp:Label  runat="server" ID="lblfrmupdpanelctrl000004subtitle" CssClass="form-subtitle" />
</td></tr>
<tr><td  colspan="1" >
<asp:Label  runat="server" ID="lblfrmupdpanelctrl000004error" CssClass="error" width="100%" />
</td></tr>
<tr><td  colspan="1" ><asp:ValidationSummary  runat="server" ID="frmupdpanelfrmupdpanelctrl000004valsumary" />
<asp:FormView  ID="frmupdpanelfrmupdpanelctrl000004" runat="server" DataSourceID="dsfrmupdpanelctrl000004" DataKeyNames="grpmbrcod" DefaultMode="Insert" OnItemUpdated="frmupdpanelfrmupdpanelctrl000004_ItemUpdated" OnItemInserted="frmupdpanelfrmupdpanelctrl000004_ItemInserted" >
<EditItemTemplate>

<!-- Init Plantilla edicion --><div  runat="server" Visible='<%# Request.Querystring("_view_")<> 1 %>' ><asp:Button  runat="server" ID="cmdfrmupdpanelctrl000004updcancel" Text="Cancelar" CssClass="boton-acciones" CausesValidation="False" OnClick="cmdfrmupdpanelctrl000004updcancel_Click" />
<asp:Button  runat="server" ID="cmdfrmupdpanelctrl000004updconfirm" Text="Guardar" CssClass="boton-acciones" CausesValidation="True" OnClick="cmdfrmupdpanelctrl000004updconfirm_Click" ValidationGroup="vgeditupdpanelfrmupdpanelctrl000004" />
</div>
<table class="form" width="100%" cellpadding="1" cellspacing="1"><tr>
<td  colspan="1" >

</td>
<td  colspan="1" >
<asp:SqlDataSource  runat="server" ID="dsedtsidcod" CancelSelectOnNullParameter="False" onInit="dsedtsidcod_Init" SelectCommandType="Text"
 SelectCommand="SELECT Q_SECPSID.sidtypecod,CASE Q_SECPSID.sidtypecod WHEN -3 THEN (SELECT TOP 1 Q_SECPGRP.grpcod FROM Q_SECPGRP WHERE Q_SECPGRP.sidcod=Q_SECPSID.sidcod) WHEN -2 THEN (SELECT TOP 1 Q_SECPLOGIN.seccod FROM Q_SECPLOGIN WHERE Q_SECPLOGIN.sidcod=Q_SECPSID.sidcod) END as sidtypecodvalue,CASE Q_SECPSID.sidtypecod WHEN -3 THEN (SELECT TOP 1 Q_SECPGRP.grpdsc FROM Q_SECPGRP WHERE Q_SECPGRP.grpcod= (SELECT TOP 1 Q_SECPGRP.grpcod FROM Q_SECPGRP WHERE Q_SECPGRP.sidcod=Q_SECPSID.sidcod)) WHEN -2 THEN (SELECT TOP 1 Q_SECPLOGIN.secdsc FROM Q_SECPLOGIN WHERE Q_SECPLOGIN.seccod= (SELECT TOP 1 Q_SECPLOGIN.seccod FROM Q_SECPLOGIN WHERE Q_SECPLOGIN.sidcod=Q_SECPSID.sidcod)) END as sidtypecoddsc,Q_SECPSID.sidcod FROM Q_SECPSID   ORDER BY Q_SECPSID.sidtypecod" onselected="dsedtsidcod_Selected" >
<SelectParameters>
</SelectParameters>
</asp:SqlDataSource>

</td>
</tr>
<tr>
<td  colspan="1" >
ID de seguridad
</td>
<td  colspan="1" >
<asp:UpdatePanel  ID="updupdpnlupdedtsidcodfs" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="True" >
<Triggers>
  <asp:AsyncPostBackTrigger  ControlID="edtsidcoddelete" EventName="Click" />
</Triggers>
<ContentTemplate>
<asp:UpdatePanel  ID="updupdedtsidcodfs" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="True" >
<ContentTemplate>
<asp:Button  runat="server" ID="cmdedtsidcodshowpanel" Text="..." CssClass="boton-acciones" CausesValidation="False" OnClick="cmdedtsidcodshowpanel_Click" />

<asp:ImageButton runat="server" ID="edtsidcoddelete" ImageURL="~/imagenes/actdel.png" Width="16" Height="16" Tooltip="Quitar ID de seguridad" Visible="False" OnClick="edtsidcoddelete_Click" OnDataBinding="edtsidcoddelete_DataBinding" CausesValidation="False" />
<asp:LinkButton runat="server" ID="edtsidcodview" Text='<%# Eval("sidcoddsc") %>' CssClass="boton-acciones-subbutton" OnDataBinding="edtsidcodview_DataBinding" CausesValidation="False" />
<asp:HiddenField  runat="server" ID="edtsidcod"/>
<asp:HiddenField  runat="server" ID="edtsidcodvalue" OnDataBinding="edtsidcodvalue_DataBound"/>
</ContentTemplate>
</asp:UpdatePanel>
<asp:UpdateProgress  runat="server" AssociatedUpdatePanelID="updupdedtsidcodfs" >
<ProgressTemplate><span  class="ajaxupd" ><div style="position:absolute;top:2px;left:50%; border-color:#333333; border:1; border-style:solid; background-color:#FFFFFF;padding:3px;"><image src="imagenes/ajaxupdate.gif" border=0 />Actualizando</div></span></ProgressTemplate>
</asp:UpdateProgress>
</ContentTemplate>
</asp:UpdatePanel>
<asp:UpdateProgress  runat="server" AssociatedUpdatePanelID="updupdpnlupdedtsidcodfs" >
<ProgressTemplate><span  class="ajaxupd" ><div style="position:absolute;top:2px;left:50%; border-color:#333333; border:1; border-style:solid; background-color:#FFFFFF;padding:3px;"><image src="imagenes/ajaxupdate.gif" border=0 />Actualizando</div></span></ProgressTemplate>
</asp:UpdateProgress>

</td>
</tr>
</table>
<!-- End Plantilla edicion --></EditItemTemplate>

<InsertItemTemplate>

<!-- Init Plantilla insercion --><div  runat="server" Visible='<%# Request.Querystring("_view_")<> 1 %>' ><asp:Button  runat="server" ID="cmdfrmupdpanelctrl000004cancel" Text="Cancelar" CssClass="boton-acciones" CausesValidation="False" OnClick="cmdfrmupdpanelctrl000004cancel_Click" />
<asp:Button  runat="server" ID="cmdfrmupdpanelctrl000004insert" Text="Guardar" CssClass="boton-acciones" CausesValidation="False" OnClick="cmdfrmupdpanelctrl000004insert_Click" />
</div>
<table class="form" width="100%" cellpadding="1" cellspacing="1"><tr>
<td  colspan="1" >

</td>
<td  colspan="1" >
<asp:SqlDataSource  runat="server" ID="dsinssidcod" CancelSelectOnNullParameter="False" onInit="dsinssidcod_Init" SelectCommandType="Text"
 SelectCommand="SELECT Q_SECPSID.sidtypecod,CASE Q_SECPSID.sidtypecod WHEN -3 THEN (SELECT TOP 1 Q_SECPGRP.grpcod FROM Q_SECPGRP WHERE Q_SECPGRP.sidcod=Q_SECPSID.sidcod) WHEN -2 THEN (SELECT TOP 1 Q_SECPLOGIN.seccod FROM Q_SECPLOGIN WHERE Q_SECPLOGIN.sidcod=Q_SECPSID.sidcod) END as sidtypecodvalue,CASE Q_SECPSID.sidtypecod WHEN -3 THEN (SELECT TOP 1 Q_SECPGRP.grpdsc FROM Q_SECPGRP WHERE Q_SECPGRP.grpcod= (SELECT TOP 1 Q_SECPGRP.grpcod FROM Q_SECPGRP WHERE Q_SECPGRP.sidcod=Q_SECPSID.sidcod)) WHEN -2 THEN (SELECT TOP 1 Q_SECPLOGIN.secdsc FROM Q_SECPLOGIN WHERE Q_SECPLOGIN.seccod= (SELECT TOP 1 Q_SECPLOGIN.seccod FROM Q_SECPLOGIN WHERE Q_SECPLOGIN.sidcod=Q_SECPSID.sidcod)) END as sidtypecoddsc,Q_SECPSID.sidcod FROM Q_SECPSID   ORDER BY Q_SECPSID.sidtypecod" onselected="dsinssidcod_Selected" >
<SelectParameters>
</SelectParameters>
</asp:SqlDataSource>

</td>
</tr>
<tr>
<td  colspan="1" >
ID de seguridad
</td>
<td  colspan="1" >
<asp:UpdatePanel  ID="updupdpnlupdinssidcodfs" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="True" >
<Triggers>
  <asp:AsyncPostBackTrigger  ControlID="inssidcoddelete" EventName="Click" />
</Triggers>
<ContentTemplate>
<asp:UpdatePanel  ID="updupdinssidcodfs" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="True" >
<ContentTemplate>
<asp:Button  runat="server" ID="cmdinssidcodshowpanel" Text="..." CssClass="boton-acciones" CausesValidation="False" OnClick="cmdinssidcodshowpanel_Click" />

<asp:ImageButton runat="server" ID="inssidcoddelete" ImageURL="~/imagenes/actdel.png" Width="16" Height="16" Tooltip="Quitar ID de seguridad" Visible="False" OnClick="inssidcoddelete_Click" OnDataBinding="inssidcoddelete_DataBinding" CausesValidation="False" />
<asp:LinkButton runat="server" ID="inssidcodview" CssClass="boton-acciones-subbutton" CausesValidation="False" />
<asp:HiddenField  runat="server" ID="inssidcod"/>
<asp:HiddenField  runat="server" ID="inssidcodvalue"/>
</ContentTemplate>
</asp:UpdatePanel>
<asp:UpdateProgress  runat="server" AssociatedUpdatePanelID="updupdinssidcodfs" >
<ProgressTemplate><span  class="ajaxupd" ><div style="position:absolute;top:2px;left:50%; border-color:#333333; border:1; border-style:solid; background-color:#FFFFFF;padding:3px;"><image src="imagenes/ajaxupdate.gif" border=0 />Actualizando</div></span></ProgressTemplate>
</asp:UpdateProgress>
</ContentTemplate>
</asp:UpdatePanel>
<asp:UpdateProgress  runat="server" AssociatedUpdatePanelID="updupdpnlupdinssidcodfs" >
<ProgressTemplate><span  class="ajaxupd" ><div style="position:absolute;top:2px;left:50%; border-color:#333333; border:1; border-style:solid; background-color:#FFFFFF;padding:3px;"><image src="imagenes/ajaxupdate.gif" border=0 />Actualizando</div></span></ProgressTemplate>
</asp:UpdateProgress>

</td>
</tr>
</table>
<!-- End Plantilla insercion --></InsertItemTemplate>

</asp:FormView>

<asp:SqlDataSource  runat="server" ID="dsfrmupdpanelctrl000004" ProviderName="System.Data.SqlClient" CancelSelectOnNullParameter="False" onInit="dsfrmupdpanelctrl000004_Init" SelectCommandType="Text"
 SelectCommand="SELECT Q_SECPGRPMBR.grpmbrcod,(Q_SECPGRPMBRGRPCOD.grpdsc) as grpcoddsc,CASE Q_SECPGRPMBRSIDCOD.sidtypecod WHEN -3 THEN (SELECT TOP 1 Q_SECPGRP.grpcod FROM Q_SECPGRP WHERE Q_SECPGRP.sidcod=Q_SECPGRPMBRSIDCOD.sidcod) WHEN -2 THEN (SELECT TOP 1 Q_SECPLOGIN.seccod FROM Q_SECPLOGIN WHERE Q_SECPLOGIN.sidcod=Q_SECPGRPMBRSIDCOD.sidcod) END as sidcodvalue,CASE Q_SECPGRPMBRSIDCOD.sidtypecod WHEN -3 THEN (SELECT TOP 1 Q_SECPGRP.grpdsc FROM Q_SECPGRP WHERE Q_SECPGRP.grpcod= (SELECT TOP 1 Q_SECPGRP.grpcod FROM Q_SECPGRP WHERE Q_SECPGRP.sidcod=Q_SECPGRPMBRSIDCOD.sidcod)) WHEN -2 THEN (SELECT TOP 1 Q_SECPLOGIN.secdsc FROM Q_SECPLOGIN WHERE Q_SECPLOGIN.seccod= (SELECT TOP 1 Q_SECPLOGIN.seccod FROM Q_SECPLOGIN WHERE Q_SECPLOGIN.sidcod=Q_SECPGRPMBRSIDCOD.sidcod)) END as sidcoddsc,(Q_SECPGRPMBRSIDCOD.sidtypecod) as sidcoddsc,(Q_SECPGRPMBRQSECSID.secdsc) as qsecsiddsc,Q_SECPGRPMBR.qsecdatetime,Q_SECPGRPMBR.grpcod,Q_SECPGRPMBR.sidcod,Q_SECPGRPMBR.qsecsid FROM Q_SECPGRPMBR  LEFT JOIN Q_SECPGRP AS Q_SECPGRPMBRGRPCOD ON Q_SECPGRPMBRGRPCOD.grpcod=Q_SECPGRPMBR.grpcod LEFT JOIN Q_SECPSID AS Q_SECPGRPMBRSIDCOD ON Q_SECPGRPMBRSIDCOD.sidcod=Q_SECPGRPMBR.sidcod LEFT JOIN Q_SECPLOGIN AS Q_SECPGRPMBRQSECSID ON Q_SECPGRPMBRQSECSID.sidcod=Q_SECPGRPMBR.qsecsid  WHERE Q_SECPGRPMBR.grpmbrcod=@grpmbrcod" onselected="dsfrmupdpanelctrl000004_Selected" UpdateCommandType="Text"
 UpdateCommand="UPDATE Q_SECPGRPMBR SET grpmbrcod=@grpmbrcod,sidcod=@sidcod WHERE Q_SECPGRPMBR.grpmbrcod=@grpmbrcod" onupdated="dsfrmupdpanelctrl000004_Updated" >
<SelectParameters>
<asp:Parameter  Name="grpmbrcod" Direction="InputOutput" Type="String" Size="200" />
</SelectParameters>
<UpdateParameters>
<asp:Parameter  Name="grpmbrcod" Direction="InputOutput" Type="String" Size="200" />
<asp:ControlParameter  Name="sidcod" ControlID="frmupdpanelfrmupdpanelctrl000004$edtsidcod" ConvertEmptyStringToNull="True" Direction="InputOutput" Type="Int32" PropertyName="Value" />
</UpdateParameters>
</asp:SqlDataSource>
</td></tr></td></tr></table></ContentTemplate>
</asp:UpdatePanel>
</asp:Panel>
<asp:UpdateProgress  runat="server" AssociatedUpdatePanelID="updupdpanelfrmupdpanelctrl000004" >
<ProgressTemplate><span  class="ajaxupd" ><div style="position:absolute;top:2px;left:50%; border-color:#333333; border:1; border-style:solid; background-color:#FFFFFF;padding:3px;"><image src="imagenes/ajaxupdate.gif" border=0 />Actualizando</div></span></ProgressTemplate>
</asp:UpdateProgress>
<!-- Fin panel updpanelfrmupdpanelctrl000004 -->
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
<asp:RegularExpressionValidator  runat="server" ID="vrgvaltxtobjectexplorerfilter" SetFocusOnError="true" CssClass="error" Display="Dynamic" ControltoValidate="txtobjectexplorerfilter" ValidationExpression="[\w\s-+/*^()!|°$%&=-_,:;.ñÑ~áéíóúÁÉÍÓÚ#}{]{0,20}" Text="No mayor a 20 caracteres. Deben ser letras o numeros." ErrorMessage=":No mayor a 20 caracteres. Deben ser letras o numeros." />
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

