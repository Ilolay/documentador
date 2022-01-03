<%@ Page Language="VB" CodeFile="frmPlantillasderoles_det.aspx.vb" MasterPageFile="general.master" AutoEventWireup="True" Inherits="frmPlantillasderoles_det" Title="Plantillas de roles" %>
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
<tr><td  style="width:32px;" colspan="1" ><img  src="imagenes/icon00000035.png" width="32px" height="32px" alt="Plantillas de roles" hspace="4"/></td>
<td  style="text-align:left;" colspan="1" ><span class="form-title">Plantillas de roles</span> |<asp:Label  runat="server" ID="lblsubtitle" CssClass="form-subtitle" />
</td></tr>
</table>
</td></tr>
<tr><td  colspan="1" >								<!-- Init -->
<table id="body" style="background-color:#F0F0F0;" width="100%">
								<tr>									<td  style="vertical-align:top;" colspan="2" >									<asp:HiddenField  runat="server" ID="q_hdnhrcdftgencod"/>									</td>								</tr>								<br /><asp:Label  runat="server" ID="lblerror" CssClass="error" width="100%" />						<asp:ValidationSummary  runat="server" ID="frmdatosvalsumary" />
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
<td  colspan="2" >
Plantilla de roles-Roles
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
 GridLines="None" ShowHeader="True" ShowFooter="True" Width="100%" UseAccessibleHeader="False" DataKeyNames="cod" CellPadding="2" DataSourceID="itemdsctrl000004" onRowCreated="itemctrl000004_RowCreated" >
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
<asp:Image  runat="server" ID="rowctrl000004imageimg" Width="12px" GenerateEmptyAlternateText="True" ImageURL="imagenes/icon00000037.png" BorderWidth="0" AlternateText="Imagen no disp." />
</ItemTemplate>
</asp:TemplateField>
<asp:TemplateField   AccessibleHeaderText="Rol" HeaderText="Rol" SortExpression="rolcoddsc" ItemStyle-CssClass="tabla-celda" HeaderStyle-CssClass="tabla-titulo" FooterStyle-CssClass="tabla-celda">
<ItemStyle  HorizontalAlign="Left" CssClass="searchresult-table-cell" />
<ItemTemplate>
<asp:LinkButton runat="server" ID="itemctrl000004colitem1view" Text='<%# Eval("rolcoddsc") %>' CssClass="form-control-read" OnDataBinding="itemctrl000004colitem1view_DataBinding" CausesValidation="False" OnClientClick='<%# "javascript:if (window.showModalDialog) {window.showModalDialog(" & chr(39) & "frmRoles_det.aspx?_mode_=0&_closea_=1&isDlg=1&param1=" & Eval("rolcod") & "&timestamp=" & chr(39) & " + new Date().getTime() + " & chr(39) & "" & chr(39) & ", " & chr(39) & "" & chr(39) & ", " & chr(39) & "menubar=no;resizable=yes;toolbar=no;status=no;center=yes;dialogWidth=750px;dialogHeight=570px;" & chr(39) & ");} else {window.open(" & chr(39) & "frmRoles_det.aspx?_mode_=0&_closea_=1&isDlg=1&param1=" & Eval("rolcod") & "&timestamp=" & chr(39) & " + new Date().getTime() + " & chr(39) & "" & chr(39) & ", " & chr(39) & "" & chr(39) & ", " & chr(39) & "modal=yes;directories=no;menubar=no;resizable=yes;toolbar=no;status=no;center=yes;dialogWidth=750px;dialogHeight=570px;" & chr(39) & ");};return false;" %>' PostBackUrl='<%# "javascript:if (window.showModalDialog) {window.showModalDialog(" & chr(39) & "frmRoles_det.aspx?_mode_=0&_closea_=1&isDlg=1&param1=" & Eval("rolcod") & "&timestamp=" & chr(39) & " + new Date().getTime() + " & chr(39) & "" & chr(39) & ", " & chr(39) & "" & chr(39) & ", " & chr(39) & "menubar=no;resizable=yes;toolbar=no;status=no;center=yes;dialogWidth=750px;dialogHeight=570px;" & chr(39) & ");} else {window.open(" & chr(39) & "frmRoles_det.aspx?_mode_=0&_closea_=1&isDlg=1&param1=" & Eval("rolcod") & "&timestamp=" & chr(39) & " + new Date().getTime() + " & chr(39) & "" & chr(39) & ", " & chr(39) & "" & chr(39) & ", " & chr(39) & "modal=yes;directories=no;menubar=no;resizable=yes;toolbar=no;status=no;center=yes;dialogWidth=750px;dialogHeight=570px;" & chr(39) & ");};return false;" %>' />
</ItemTemplate>
</asp:TemplateField>
<asp:TemplateField   AccessibleHeaderText="Miiembro de rol" HeaderText="Miiembro de rol" SortExpression="rolcodtype" ItemStyle-CssClass="tabla-celda" HeaderStyle-CssClass="tabla-titulo" FooterStyle-CssClass="tabla-celda">
<ItemStyle  HorizontalAlign="Left" CssClass="searchresult-table-cell" />
<ItemTemplate>
<asp:LinkButton runat="server" ID="itemctrl000004colitem2view" Text='<%# Eval("rolcodtypedsc") %>' CssClass="form-control-read" OnDataBinding="itemctrl000004colitem2view_DataBinding" CausesValidation="False" />
<asp:HiddenField  runat="server" ID="itemctrl000004colitem2"/>
<asp:HiddenField  runat="server" ID="itemctrl000004colitem2value" OnDataBinding="itemctrl000004colitem2value_DataBound"/>
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
<b>Descripción</b>
</td>
<td  colspan="1" >
<asp:TextBox  runat="server" ID="editctrl000007" CssClass="form-control" Width="400px" MaxLength="50" OnDataBinding="editctrl000007_DataBound_frmdatos" Tooltip="Campo201[]" TextMode="MultiLine" onkeypress="return textCounter(this,50);" onkeydown="return textCounter(this,50);" onkeyup="return textCounter(this,50);" onfocus="return textCounter_GetFocus(this,50);" onblur="return textCounter_LostFocus(this);" /> <span class='error'><b>*</b></span>
<asp:RequiredFieldValidator  runat="server" ID="vrqeditctrl000007" SetFocusOnError="true" CssClass="error" Display="Dynamic" ControltoValidate="editctrl000007" Text="Obligatorio!!! ErrorMessage=´Descripción:es un dato obligatorio!´" ValidationGroup="vgedit" />
<ajaxkit:ValidatorCalloutExtender  runat="server" ID="ajaxvrqeditctrl000007" TargetControlID="vrqeditctrl000007" /><asp:CompareValidator  runat="server" ID="vcdvaleditctrl000007" SetFocusOnError="true" CssClass="error" ControltoValidate="editctrl000007" Display="Dynamic" Text="Ingrese un texto" ErrorMessage="Descripción:Ingrese un texto" Type="String" Operator="DataTypeCheck" ValidationGroup="vgedit" />
<asp:RegularExpressionValidator  runat="server" ID="vrgvaleditctrl000007" SetFocusOnError="true" CssClass="error" Display="Dynamic" ControltoValidate="editctrl000007" ValidationExpression="[\w\s-+/*^()!|°$%&=-_,:;.ñÑ~áéíóúÁÉÍÓÚ#}{¿]{0,50}" Text="No mayor a 50 caracteres. Deben ser letras o numeros." ErrorMessage="Descripción:No mayor a 50 caracteres. Deben ser letras o numeros." ValidationGroup="vgedit" />
<ajaxkit:ValidatorCalloutExtender  runat="server" ID="ajaxvrgeditctrl000007" TargetControlID="vrgvaleditctrl000007" />

</td>
</tr>
<tr>
<td  colspan="2" >
Plantilla de roles-Roles
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
 GridLines="None" ShowHeader="True" ShowFooter="True" Width="100%" UseAccessibleHeader="False" DataKeyNames="cod" CellPadding="2" DataSourceID="editdsctrl000004" onRowCreated="editctrl000004_RowCreated" onRowCommand="editctrl000004_RowCommand" >
<FooterStyle  CssClass="tabla-footer" />
<RowStyle  CssClass="tabla-fila" />
<AlternatingRowStyle  CssClass="tabla-fila-alternativa" />
<SelectedRowStyle  CssClass="tabla-fila" />
<PagerStyle  CssClass="tabla-pager" />
<HeaderStyle  CssClass="tabla-titulo" ForeColor="White" />
<EmptyDataTemplate>
<p  class="tabla-fila">Sin datos</p>
</br></br></br></br></br><asp:Button  runat="server" ID="cmdFormViewInsert" Text="Nuevo" CssClass="boton-acciones" CommandName="HRC_INSERT" CausesValidation="False" /></EmptyDataTemplate>
<Columns>
<asp:TemplateField   ItemStyle-CssClass="tabla-celda" HeaderStyle-CssClass="tabla-titulo" FooterStyle-CssClass="tabla-celda">
<ItemStyle  HorizontalAlign="Left" Width="12px" CssClass="searchresult-table-cell" />
<ItemTemplate>
<asp:Image  runat="server" ID="rowctrl000004imageimg" Width="12px" GenerateEmptyAlternateText="True" ImageURL="imagenes/icon00000037.png" BorderWidth="0" AlternateText="Imagen no disp." />
</ItemTemplate>
</asp:TemplateField>
<asp:TemplateField   AccessibleHeaderText="Rol" HeaderText="Rol" SortExpression="rolcoddsc" ItemStyle-CssClass="tabla-celda" HeaderStyle-CssClass="tabla-titulo" FooterStyle-CssClass="tabla-celda">
<ItemStyle  HorizontalAlign="Left" CssClass="searchresult-table-cell" />
<ItemTemplate>
<asp:LinkButton runat="server" ID="editctrl000004colitem1view" Text='<%# Eval("rolcoddsc") %>' CssClass="form-control-read" OnDataBinding="editctrl000004colitem1view_DataBinding" CausesValidation="False" OnClientClick='<%# "javascript:if (window.showModalDialog) {window.showModalDialog(" & chr(39) & "frmRoles_det.aspx?_mode_=0&_closea_=1&isDlg=1&param1=" & Eval("rolcod") & "&timestamp=" & chr(39) & " + new Date().getTime() + " & chr(39) & "" & chr(39) & ", " & chr(39) & "" & chr(39) & ", " & chr(39) & "menubar=no;resizable=yes;toolbar=no;status=no;center=yes;dialogWidth=750px;dialogHeight=570px;" & chr(39) & ");} else {window.open(" & chr(39) & "frmRoles_det.aspx?_mode_=0&_closea_=1&isDlg=1&param1=" & Eval("rolcod") & "&timestamp=" & chr(39) & " + new Date().getTime() + " & chr(39) & "" & chr(39) & ", " & chr(39) & "" & chr(39) & ", " & chr(39) & "modal=yes;directories=no;menubar=no;resizable=yes;toolbar=no;status=no;center=yes;dialogWidth=750px;dialogHeight=570px;" & chr(39) & ");};return false;" %>' PostBackUrl='<%# "javascript:if (window.showModalDialog) {window.showModalDialog(" & chr(39) & "frmRoles_det.aspx?_mode_=0&_closea_=1&isDlg=1&param1=" & Eval("rolcod") & "&timestamp=" & chr(39) & " + new Date().getTime() + " & chr(39) & "" & chr(39) & ", " & chr(39) & "" & chr(39) & ", " & chr(39) & "menubar=no;resizable=yes;toolbar=no;status=no;center=yes;dialogWidth=750px;dialogHeight=570px;" & chr(39) & ");} else {window.open(" & chr(39) & "frmRoles_det.aspx?_mode_=0&_closea_=1&isDlg=1&param1=" & Eval("rolcod") & "&timestamp=" & chr(39) & " + new Date().getTime() + " & chr(39) & "" & chr(39) & ", " & chr(39) & "" & chr(39) & ", " & chr(39) & "modal=yes;directories=no;menubar=no;resizable=yes;toolbar=no;status=no;center=yes;dialogWidth=750px;dialogHeight=570px;" & chr(39) & ");};return false;" %>' />
</ItemTemplate>
</asp:TemplateField>
<asp:TemplateField   AccessibleHeaderText="Miiembro de rol" HeaderText="Miiembro de rol" SortExpression="rolcodtype" ItemStyle-CssClass="tabla-celda" HeaderStyle-CssClass="tabla-titulo" FooterStyle-CssClass="tabla-celda">
<ItemStyle  HorizontalAlign="Left" CssClass="searchresult-table-cell" />
<ItemTemplate>
<asp:LinkButton runat="server" ID="editctrl000004colitem2view" Text='<%# Eval("rolcodtypedsc") %>' CssClass="form-control-read" OnDataBinding="editctrl000004colitem2view_DataBinding" CausesValidation="False" />
<asp:HiddenField  runat="server" ID="editctrl000004colitem2"/>
<asp:HiddenField  runat="server" ID="editctrl000004colitem2value" OnDataBinding="editctrl000004colitem2value_DataBound"/>
</ItemTemplate>
</asp:TemplateField>
<asp:TemplateField   AccessibleHeaderText="Acciones" HeaderText="Acciones">
<ItemStyle  HorizontalAlign="Center" Width="3%" />
<ItemTemplate>
 <asp:ImageButton  ID="imge" runat="server" CausesValidation="False" CommandName="Edit" CommandArgument='<%# Eval("cod") %>' ImageUrl="./imagenes/actmod.png" Width="16" Text="Editar" />
 <asp:ImageButton  ID="imgd" runat="server" CausesValidation="False" CommandName="Delete" CommandArgument='<%# Eval("cod") %>' ImageUrl="./imagenes/actdel.png" Text="Borrar" Width="16" OnClientClick=" return confirm('Confirma la eliminación?');" />
</ItemTemplate>
<FooterTemplate>
<asp:Button  ID="cmdi" runat="server" CausesValidation="False" CommandName="HRC_INSERT" CssClass="boton-acciones" Text="Agregar" />
</FooterTemplate>
</asp:TemplateField>
<asp:TemplateField   ItemStyle-CssClass="tabla-celda" HeaderStyle-CssClass="tabla-titulo" FooterStyle-CssClass="tabla-celda">
<ItemStyle  HorizontalAlign="Left" Width="16" />
<ItemTemplate>
<asp:ImageButton runat="server" ID="cmdVer" ImageURL="./imagenes/actview.png" Width="16" Height="16" CommandName="Select" CausesValidation="False" CommandArgument='<%# Eval("cod") %>' /></ItemTemplate>
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
<b>Descripción</b>
</td>
<td  colspan="1" >
<asp:TextBox  runat="server" ID="insctrl000007" CssClass="form-control" Width="400px" MaxLength="50" Tooltip="Campo201[]" TextMode="MultiLine" onkeypress="return textCounter(this,50);" onkeydown="return textCounter(this,50);" onkeyup="return textCounter(this,50);" onfocus="return textCounter_GetFocus(this,50);" onblur="return textCounter_LostFocus(this);" /> <span class='error'><b>*</b></span>
<asp:RequiredFieldValidator  runat="server" ID="vrqinsctrl000007" SetFocusOnError="true" CssClass="error" Display="Dynamic" ControltoValidate="insctrl000007" Text="Obligatorio!!! ErrorMessage=´Descripción:es un dato obligatorio!´" ValidationGroup="vgins" />
<ajaxkit:ValidatorCalloutExtender  runat="server" ID="ajaxvrqinsctrl000007" TargetControlID="vrqinsctrl000007" /><asp:CompareValidator  runat="server" ID="vcdvalinsctrl000007" SetFocusOnError="true" CssClass="error" ControltoValidate="insctrl000007" Display="Dynamic" Text="Ingrese un texto" ErrorMessage="Descripción:Ingrese un texto" Type="String" Operator="DataTypeCheck" ValidationGroup="vgins" />
<asp:RegularExpressionValidator  runat="server" ID="vrgvalinsctrl000007" SetFocusOnError="true" CssClass="error" Display="Dynamic" ControltoValidate="insctrl000007" ValidationExpression="[\w\s-+/*^()!|°$%&=-_,:;.ñÑ~áéíóúÁÉÍÓÚ#}{¿]{0,50}" Text="No mayor a 50 caracteres. Deben ser letras o numeros." ErrorMessage="Descripción:No mayor a 50 caracteres. Deben ser letras o numeros." ValidationGroup="vgins" />
<ajaxkit:ValidatorCalloutExtender  runat="server" ID="ajaxvrginsctrl000007" TargetControlID="vrgvalinsctrl000007" />

</td>
</tr>
</table>
<!-- End Plantilla insercion --></InsertItemTemplate>

</asp:FormView>

						<asp:SqlDataSource  runat="server" ID="dsdatos" ProviderName="System.Data.SqlClient" CancelSelectOnNullParameter="False" onInit="dsdatos_Init" SelectCommandType="Text"
 SelectCommand="(SELECT DOC_TRO.cod,DOC_TRO.dsc,DOC_TRO.baja,DOC_TRO.custom,(DOC_TROQSECSID.secdsc) as qsecsiddsc,DOC_TRO.qsecdatetime,DOC_TRO.qsecsid FROM DOC_TRO  LEFT JOIN Q_SECPLOGIN AS DOC_TROQSECSID ON DOC_TROQSECSID.sidcod=DOC_TRO.qsecsid  WHERE (DOC_TRO.cod NOT IN (SELECT qdft_cod FROM DOC_TRO_DFTREL WHERE dftdidgencod=@dftdidgencod) AND DOC_TRO.cod = @param1) AND (DOC_TRO.baja = '' OR DOC_TRO.baja  IS NULL)) UNION (SELECT DOC_TRO_DFT.cod,DOC_TRO_DFT.dsc,DOC_TRO_DFT.baja,DOC_TRO_DFT.custom,(DOC_TRO_DFTQSECSID.secdsc) as qsecsiddsc,DOC_TRO_DFT.qsecdatetime,DOC_TRO_DFT.qsecsid FROM DOC_TRO_DFT  LEFT JOIN Q_SECPLOGIN AS DOC_TRO_DFTQSECSID ON DOC_TRO_DFTQSECSID.sidcod=DOC_TRO_DFT.qsecsid  WHERE (DOC_TRO_DFT.dftdidgencod=@dftdidgencod AND DOC_TRO_DFT.cod = @param1) AND (DOC_TRO_DFT.baja = '' OR DOC_TRO_DFT.baja  IS NULL))" onselected="dsdatos_Selected" UpdateCommandType="Text"
 UpdateCommand="UPDATE DOC_TRO_DFT SET dsc=@dsc,qsecsid=@qsecsid,qsecdatetime=getdate() WHERE DOC_TRO_DFT.cod = @param1 AND DOC_TRO_DFT.dftdidgencod= @dftdidgencod" onupdated="dsdatos_Updated" InsertCommandType="Text"
 InsertCommand="UPDATE DOC_TRO_DFT SET dsc=@dsc,qsecsid=@qsecsid,qsecdatetime=getdate() WHERE DOC_TRO_DFT.cod = -101 AND DOC_TRO_DFT.dftdidgencod= @dftdidgencod" onInserted="dsdatos_Inserted" DeleteCommandType="Text"
 DeleteCommand="UPDATE DOC_TRO SET baja={#CHR39#}1{#CHR39#} WHERE cod = @param1" ondeleted="dsdatos_Deleted" >
<InsertParameters>
<asp:Parameter  Name="querynextcod" Direction="Output" Type="Int32" />
<asp:ControlParameter  Name="dsc" ControlID="frmdatos$insctrl000007" ConvertEmptyStringToNull="True" Direction="InputOutput" Type="String" Size="50" PropertyName="Text" />
<asp:SessionParameter  Name="qsecsid" SessionField="secsid" Direction="Input" />
<asp:ControlParameter  Name="dftdidgencod" ControlID="q_hdnhrcdftgencod" ConvertEmptyStringToNull="True" Direction="Input" Type="String" Size="200" PropertyName="Value" />
</InsertParameters>
<SelectParameters>
<asp:QueryStringParameter  Name="param1" QueryStringField="param1" ConvertEmptyStringToNull="True" Type="String" Size="200" />
<asp:ControlParameter  Name="dftdidgencod" ControlID="q_hdnhrcdftgencod" ConvertEmptyStringToNull="True" Direction="Input" Type="String" Size="200" PropertyName="Value" />
</SelectParameters>
<UpdateParameters>
<asp:QueryStringParameter  Name="param1" QueryStringField="param1" ConvertEmptyStringToNull="True" Type="String" Size="200" />
<asp:ControlParameter  Name="dsc" ControlID="frmdatos$editctrl000007" ConvertEmptyStringToNull="True" Direction="InputOutput" Type="String" Size="50" PropertyName="Text" />
<asp:SessionParameter  Name="qsecsid" SessionField="secsid" Direction="Input" />
<asp:ControlParameter  Name="dftdidgencod" ControlID="q_hdnhrcdftgencod" ConvertEmptyStringToNull="True" Direction="Input" Type="String" Size="200" PropertyName="Value" />
</UpdateParameters>
<DeleteParameters>
<asp:QueryStringParameter  Name="param1" QueryStringField="param1" ConvertEmptyStringToNull="True" Type="String" Size="200" />
<asp:SessionParameter  Name="qsecsid" SessionField="secsid" Direction="Input" />
</DeleteParameters>
</asp:SqlDataSource>
					<asp:SqlDataSource  runat="server" ID="itemdsctrl000004" ProviderName="System.Data.SqlClient" CancelSelectOnNullParameter="False" onInit="itemdsctrl000004_Init" SelectCommandType="Text"
 SelectCommand="(SELECT DOC_TROROL.cod,(DOC_TROROLTROCOD.dsc) as trocoddsc,(DOC_TROROLROLCOD.dsc) as rolcoddsc,DOC_TROROL.rolcodtype,CASE DOC_TROROL.rolcodtype WHEN 9 THEN (SELECT TOP 1 DOC_TROROLEMP.empcod FROM DOC_TROROLEMP WHERE DOC_TROROLEMP.trorolcod=DOC_TROROL.cod) WHEN 10 THEN (SELECT TOP 1 DOC_TROROLUND.undcod FROM DOC_TROROLUND WHERE DOC_TROROLUND.trorolcod=DOC_TROROL.cod) WHEN 14 THEN (SELECT TOP 1 DOC_TROROLEQU.equcod FROM DOC_TROROLEQU WHERE DOC_TROROLEQU.trorolcod=DOC_TROROL.cod) WHEN 43 THEN (SELECT TOP 1 DOC_TROROLDYNGRP.dyngrpcod FROM DOC_TROROLDYNGRP WHERE DOC_TROROLDYNGRP.trorolcod=DOC_TROROL.cod) END as rolcodtypevalue,CASE DOC_TROROL.rolcodtype WHEN 9 THEN (SELECT TOP 1 EMP.dsc FROM EMP WHERE EMP.cod= (SELECT TOP 1 DOC_TROROLEMP.empcod FROM DOC_TROROLEMP WHERE DOC_TROROLEMP.trorolcod=DOC_TROROL.cod)) WHEN 10 THEN (SELECT TOP 1 UND.dsc FROM UND WHERE UND.cod= (SELECT TOP 1 DOC_TROROLUND.undcod FROM DOC_TROROLUND WHERE DOC_TROROLUND.trorolcod=DOC_TROROL.cod)) WHEN 14 THEN (SELECT TOP 1 DOC_EQU.dsc FROM DOC_EQU WHERE DOC_EQU.cod= (SELECT TOP 1 DOC_TROROLEQU.equcod FROM DOC_TROROLEQU WHERE DOC_TROROLEQU.trorolcod=DOC_TROROL.cod)) WHEN 43 THEN (SELECT TOP 1 DOC_DYNGRP.dsc FROM DOC_DYNGRP WHERE DOC_DYNGRP.cod= (SELECT TOP 1 DOC_TROROLDYNGRP.dyngrpcod FROM DOC_TROROLDYNGRP WHERE DOC_TROROLDYNGRP.trorolcod=DOC_TROROL.cod)) END as rolcodtypedsc,(DOC_TROROLQSECSID.secdsc) as qsecsiddsc,DOC_TROROL.qsecdatetime,DOC_TROROL.trocod,DOC_TROROL.rolcod,DOC_TROROL.qsecsid FROM DOC_TROROL  LEFT JOIN DOC_TRO AS DOC_TROROLTROCOD ON DOC_TROROLTROCOD.cod=DOC_TROROL.trocod LEFT JOIN DOC_ROL AS DOC_TROROLROLCOD ON DOC_TROROLROLCOD.cod=DOC_TROROL.rolcod LEFT JOIN Q_SECPLOGIN AS DOC_TROROLQSECSID ON DOC_TROROLQSECSID.sidcod=DOC_TROROL.qsecsid  WHERE DOC_TROROL.cod NOT IN (SELECT qdft_cod FROM DOC_TROROL_DFTREL WHERE dftdidgencod=@dftdidgencod) AND DOC_TROROL.trocod= @param1) UNION (SELECT DOC_TROROL_DFT.cod,(DOC_TROROL_DFTTROCOD.dsc) as trocoddsc,(DOC_TROROL_DFTROLCOD.dsc) as rolcoddsc,DOC_TROROL_DFT.rolcodtype,CASE DOC_TROROL_DFT.rolcodtype WHEN 9 THEN (SELECT TOP 1 DOC_TROROLEMP_DFT.empcod FROM DOC_TROROLEMP_DFT WHERE DOC_TROROLEMP_DFT.trorolcod=DOC_TROROL_DFT.cod AND DOC_TROROLEMP_DFT.dftdidgencod=DOC_TROROL_DFT.dftdidgencod) WHEN 10 THEN (SELECT TOP 1 DOC_TROROLUND_DFT.undcod FROM DOC_TROROLUND_DFT WHERE DOC_TROROLUND_DFT.trorolcod=DOC_TROROL_DFT.cod AND DOC_TROROLUND_DFT.dftdidgencod=DOC_TROROL_DFT.dftdidgencod) WHEN 14 THEN (SELECT TOP 1 DOC_TROROLEQU_DFT.equcod FROM DOC_TROROLEQU_DFT WHERE DOC_TROROLEQU_DFT.trorolcod=DOC_TROROL_DFT.cod AND DOC_TROROLEQU_DFT.dftdidgencod=DOC_TROROL_DFT.dftdidgencod) WHEN 43 THEN (SELECT TOP 1 DOC_TROROLDYNGRP_DFT.dyngrpcod FROM DOC_TROROLDYNGRP_DFT WHERE DOC_TROROLDYNGRP_DFT.trorolcod=DOC_TROROL_DFT.cod AND DOC_TROROLDYNGRP_DFT.dftdidgencod=DOC_TROROL_DFT.dftdidgencod) END as rolcodtypevalue,CASE DOC_TROROL_DFT.rolcodtype WHEN 9 THEN (SELECT TOP 1 EMP.dsc FROM EMP WHERE EMP.cod= (SELECT TOP 1 DOC_TROROLEMP_DFT.empcod FROM DOC_TROROLEMP_DFT WHERE DOC_TROROLEMP_DFT.trorolcod=DOC_TROROL_DFT.cod AND DOC_TROROLEMP_DFT.dftdidgencod=DOC_TROROL_DFT.dftdidgencod)) WHEN 10 THEN (SELECT TOP 1 UND.dsc FROM UND WHERE UND.cod= (SELECT TOP 1 DOC_TROROLUND_DFT.undcod FROM DOC_TROROLUND_DFT WHERE DOC_TROROLUND_DFT.trorolcod=DOC_TROROL_DFT.cod AND DOC_TROROLUND_DFT.dftdidgencod=DOC_TROROL_DFT.dftdidgencod)) WHEN 14 THEN (SELECT TOP 1 DOC_EQU.dsc FROM DOC_EQU WHERE DOC_EQU.cod= (SELECT TOP 1 DOC_TROROLEQU_DFT.equcod FROM DOC_TROROLEQU_DFT WHERE DOC_TROROLEQU_DFT.trorolcod=DOC_TROROL_DFT.cod AND DOC_TROROLEQU_DFT.dftdidgencod=DOC_TROROL_DFT.dftdidgencod)) WHEN 43 THEN (SELECT TOP 1 DOC_DYNGRP.dsc FROM DOC_DYNGRP WHERE DOC_DYNGRP.cod= (SELECT TOP 1 DOC_TROROLDYNGRP_DFT.dyngrpcod FROM DOC_TROROLDYNGRP_DFT WHERE DOC_TROROLDYNGRP_DFT.trorolcod=DOC_TROROL_DFT.cod AND DOC_TROROLDYNGRP_DFT.dftdidgencod=DOC_TROROL_DFT.dftdidgencod)) END as rolcodtypedsc,(DOC_TROROL_DFTQSECSID.secdsc) as qsecsiddsc,DOC_TROROL_DFT.qsecdatetime,DOC_TROROL_DFT.trocod,DOC_TROROL_DFT.rolcod,DOC_TROROL_DFT.qsecsid FROM DOC_TROROL_DFT  LEFT JOIN DOC_TRO_DFT AS DOC_TROROL_DFTTROCOD ON DOC_TROROL_DFTTROCOD.cod=DOC_TROROL_DFT.trocod LEFT JOIN DOC_ROL AS DOC_TROROL_DFTROLCOD ON DOC_TROROL_DFTROLCOD.cod=DOC_TROROL_DFT.rolcod LEFT JOIN Q_SECPLOGIN AS DOC_TROROL_DFTQSECSID ON DOC_TROROL_DFTQSECSID.sidcod=DOC_TROROL_DFT.qsecsid  WHERE DOC_TROROL_DFT.dftdidgencod=@dftdidgencod AND DOC_TROROL_DFT.trocod= @param1)" onselected="itemdsctrl000004_Selected" >
<SelectParameters>
<asp:ControlParameter  Name="param1" ControlID="frmdatos" ConvertEmptyStringToNull="True" Direction="InputOutput" Type="String" Size="200" PropertyName="SelectedValue" />
<asp:ControlParameter  Name="dftdidgencod" ControlID="q_hdnhrcdftgencod" ConvertEmptyStringToNull="True" Direction="Input" Type="String" Size="200" PropertyName="Value" />
</SelectParameters>
</asp:SqlDataSource>
				<asp:SqlDataSource  runat="server" ID="editdsctrl000004" ProviderName="System.Data.SqlClient" CancelSelectOnNullParameter="False" onInit="editdsctrl000004_Init" SelectCommandType="Text"
 SelectCommand="(SELECT DOC_TROROL.cod,(DOC_TROROLTROCOD.dsc) as trocoddsc,(DOC_TROROLROLCOD.dsc) as rolcoddsc,DOC_TROROL.rolcodtype,CASE DOC_TROROL.rolcodtype WHEN 9 THEN (SELECT TOP 1 DOC_TROROLEMP.empcod FROM DOC_TROROLEMP WHERE DOC_TROROLEMP.trorolcod=DOC_TROROL.cod) WHEN 10 THEN (SELECT TOP 1 DOC_TROROLUND.undcod FROM DOC_TROROLUND WHERE DOC_TROROLUND.trorolcod=DOC_TROROL.cod) WHEN 14 THEN (SELECT TOP 1 DOC_TROROLEQU.equcod FROM DOC_TROROLEQU WHERE DOC_TROROLEQU.trorolcod=DOC_TROROL.cod) WHEN 43 THEN (SELECT TOP 1 DOC_TROROLDYNGRP.dyngrpcod FROM DOC_TROROLDYNGRP WHERE DOC_TROROLDYNGRP.trorolcod=DOC_TROROL.cod) END as rolcodtypevalue,CASE DOC_TROROL.rolcodtype WHEN 9 THEN (SELECT TOP 1 EMP.dsc FROM EMP WHERE EMP.cod= (SELECT TOP 1 DOC_TROROLEMP.empcod FROM DOC_TROROLEMP WHERE DOC_TROROLEMP.trorolcod=DOC_TROROL.cod)) WHEN 10 THEN (SELECT TOP 1 UND.dsc FROM UND WHERE UND.cod= (SELECT TOP 1 DOC_TROROLUND.undcod FROM DOC_TROROLUND WHERE DOC_TROROLUND.trorolcod=DOC_TROROL.cod)) WHEN 14 THEN (SELECT TOP 1 DOC_EQU.dsc FROM DOC_EQU WHERE DOC_EQU.cod= (SELECT TOP 1 DOC_TROROLEQU.equcod FROM DOC_TROROLEQU WHERE DOC_TROROLEQU.trorolcod=DOC_TROROL.cod)) WHEN 43 THEN (SELECT TOP 1 DOC_DYNGRP.dsc FROM DOC_DYNGRP WHERE DOC_DYNGRP.cod= (SELECT TOP 1 DOC_TROROLDYNGRP.dyngrpcod FROM DOC_TROROLDYNGRP WHERE DOC_TROROLDYNGRP.trorolcod=DOC_TROROL.cod)) END as rolcodtypedsc,(DOC_TROROLQSECSID.secdsc) as qsecsiddsc,DOC_TROROL.qsecdatetime,DOC_TROROL.trocod,DOC_TROROL.rolcod,DOC_TROROL.qsecsid FROM DOC_TROROL  LEFT JOIN DOC_TRO AS DOC_TROROLTROCOD ON DOC_TROROLTROCOD.cod=DOC_TROROL.trocod LEFT JOIN DOC_ROL AS DOC_TROROLROLCOD ON DOC_TROROLROLCOD.cod=DOC_TROROL.rolcod LEFT JOIN Q_SECPLOGIN AS DOC_TROROLQSECSID ON DOC_TROROLQSECSID.sidcod=DOC_TROROL.qsecsid  WHERE DOC_TROROL.cod NOT IN (SELECT qdft_cod FROM DOC_TROROL_DFTREL WHERE dftdidgencod=@dftdidgencod) AND DOC_TROROL.trocod= @param1) UNION (SELECT DOC_TROROL_DFT.cod,(DOC_TROROL_DFTTROCOD.dsc) as trocoddsc,(DOC_TROROL_DFTROLCOD.dsc) as rolcoddsc,DOC_TROROL_DFT.rolcodtype,CASE DOC_TROROL_DFT.rolcodtype WHEN 9 THEN (SELECT TOP 1 DOC_TROROLEMP_DFT.empcod FROM DOC_TROROLEMP_DFT WHERE DOC_TROROLEMP_DFT.trorolcod=DOC_TROROL_DFT.cod AND DOC_TROROLEMP_DFT.dftdidgencod=DOC_TROROL_DFT.dftdidgencod) WHEN 10 THEN (SELECT TOP 1 DOC_TROROLUND_DFT.undcod FROM DOC_TROROLUND_DFT WHERE DOC_TROROLUND_DFT.trorolcod=DOC_TROROL_DFT.cod AND DOC_TROROLUND_DFT.dftdidgencod=DOC_TROROL_DFT.dftdidgencod) WHEN 14 THEN (SELECT TOP 1 DOC_TROROLEQU_DFT.equcod FROM DOC_TROROLEQU_DFT WHERE DOC_TROROLEQU_DFT.trorolcod=DOC_TROROL_DFT.cod AND DOC_TROROLEQU_DFT.dftdidgencod=DOC_TROROL_DFT.dftdidgencod) WHEN 43 THEN (SELECT TOP 1 DOC_TROROLDYNGRP_DFT.dyngrpcod FROM DOC_TROROLDYNGRP_DFT WHERE DOC_TROROLDYNGRP_DFT.trorolcod=DOC_TROROL_DFT.cod AND DOC_TROROLDYNGRP_DFT.dftdidgencod=DOC_TROROL_DFT.dftdidgencod) END as rolcodtypevalue,CASE DOC_TROROL_DFT.rolcodtype WHEN 9 THEN (SELECT TOP 1 EMP.dsc FROM EMP WHERE EMP.cod= (SELECT TOP 1 DOC_TROROLEMP_DFT.empcod FROM DOC_TROROLEMP_DFT WHERE DOC_TROROLEMP_DFT.trorolcod=DOC_TROROL_DFT.cod AND DOC_TROROLEMP_DFT.dftdidgencod=DOC_TROROL_DFT.dftdidgencod)) WHEN 10 THEN (SELECT TOP 1 UND.dsc FROM UND WHERE UND.cod= (SELECT TOP 1 DOC_TROROLUND_DFT.undcod FROM DOC_TROROLUND_DFT WHERE DOC_TROROLUND_DFT.trorolcod=DOC_TROROL_DFT.cod AND DOC_TROROLUND_DFT.dftdidgencod=DOC_TROROL_DFT.dftdidgencod)) WHEN 14 THEN (SELECT TOP 1 DOC_EQU.dsc FROM DOC_EQU WHERE DOC_EQU.cod= (SELECT TOP 1 DOC_TROROLEQU_DFT.equcod FROM DOC_TROROLEQU_DFT WHERE DOC_TROROLEQU_DFT.trorolcod=DOC_TROROL_DFT.cod AND DOC_TROROLEQU_DFT.dftdidgencod=DOC_TROROL_DFT.dftdidgencod)) WHEN 43 THEN (SELECT TOP 1 DOC_DYNGRP.dsc FROM DOC_DYNGRP WHERE DOC_DYNGRP.cod= (SELECT TOP 1 DOC_TROROLDYNGRP_DFT.dyngrpcod FROM DOC_TROROLDYNGRP_DFT WHERE DOC_TROROLDYNGRP_DFT.trorolcod=DOC_TROROL_DFT.cod AND DOC_TROROLDYNGRP_DFT.dftdidgencod=DOC_TROROL_DFT.dftdidgencod)) END as rolcodtypedsc,(DOC_TROROL_DFTQSECSID.secdsc) as qsecsiddsc,DOC_TROROL_DFT.qsecdatetime,DOC_TROROL_DFT.trocod,DOC_TROROL_DFT.rolcod,DOC_TROROL_DFT.qsecsid FROM DOC_TROROL_DFT  LEFT JOIN DOC_TRO_DFT AS DOC_TROROL_DFTTROCOD ON DOC_TROROL_DFTTROCOD.cod=DOC_TROROL_DFT.trocod LEFT JOIN DOC_ROL AS DOC_TROROL_DFTROLCOD ON DOC_TROROL_DFTROLCOD.cod=DOC_TROROL_DFT.rolcod LEFT JOIN Q_SECPLOGIN AS DOC_TROROL_DFTQSECSID ON DOC_TROROL_DFTQSECSID.sidcod=DOC_TROROL_DFT.qsecsid  WHERE DOC_TROROL_DFT.dftdidgencod=@dftdidgencod AND DOC_TROROL_DFT.trocod= @param1)" onselected="editdsctrl000004_Selected" InsertCommandType="Text"
 InsertCommand="SET @querynextcod=(SELECT ISNULL(MIN(cod),-100)-1 FROM DOC_TROROL_DFT WHERE cod< -100 AND dftdidgencod=@dftdidgencod) INSERT INTO DOC_TROROL_DFT (cod,trocod,rolcod,rolcodtype,qsecdatetime,dftdidgencod) VALUES(@querynextcod,@trocod,@rolcod,@rolcodtype,getdate(),@dftdidgencod)INSERT INTO DOC_TROROL_DFTREL (cod,qdft_cod,dftdidgencod) VALUES(-1,@querynextcod,@dftdidgencod) SELECT @querynextcod AS cod" onInserted="editdsctrl000004_Inserted" DeleteCommandType="Text"
 DeleteCommand=" DELETE FROM DOC_TROROL_DFT WHERE dftdidgencod=@dftdidgencod AND cod=@cod DELETE FROM DOC_TROROL_DFTREL WHERE dftdidgencod=@dftdidgencod AND qdft_cod=@cod UPDATE DOC_TROROL SET qdraftstate=1 WHERE cod=@cod INSERT INTO DOC_TROROL_DFTREL (dftdidgencod,cod,qdft_cod) VALUES(@dftdidgencod,-1,@cod)" ondeleted="editdsctrl000004_Deleted" >
<InsertParameters>
<asp:ControlParameter  Name="param1" ControlID="frmdatos" ConvertEmptyStringToNull="True" Direction="InputOutput" Type="String" Size="200" PropertyName="SelectedValue" />
<asp:ControlParameter  Name="dftdidgencod" ControlID="q_hdnhrcdftgencod" ConvertEmptyStringToNull="True" Direction="Input" Type="String" Size="200" PropertyName="Value" />
<asp:SessionParameter  Name="querynextcod" SessionField="4" Direction="Output" Type="Int32" />
</InsertParameters>
<SelectParameters>
<asp:ControlParameter  Name="param1" ControlID="frmdatos" ConvertEmptyStringToNull="True" Direction="InputOutput" Type="String" Size="200" PropertyName="SelectedValue" />
<asp:ControlParameter  Name="dftdidgencod" ControlID="q_hdnhrcdftgencod" ConvertEmptyStringToNull="True" Direction="Input" Type="String" Size="200" PropertyName="Value" />
</SelectParameters>
<DeleteParameters>
<asp:ControlParameter  Name="param1" ControlID="frmdatos" ConvertEmptyStringToNull="True" Direction="InputOutput" Type="String" Size="200" PropertyName="SelectedValue" />
<asp:ControlParameter  Name="dftdidgencod" ControlID="q_hdnhrcdftgencod" ConvertEmptyStringToNull="True" Direction="Input" Type="String" Size="200" PropertyName="Value" />
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
<asp:FormView  ID="frmupdpanelfrmupdpanelctrl000004" runat="server" DataSourceID="dsfrmupdpanelctrl000004" DataKeyNames="cod" DefaultMode="Insert" OnItemUpdated="frmupdpanelfrmupdpanelctrl000004_ItemUpdated" OnItemInserted="frmupdpanelfrmupdpanelctrl000004_ItemInserted" >
<EditItemTemplate>

<!-- Init Plantilla edicion --><div  runat="server" Visible='<%# Request.Querystring("_view_")<> 1 %>' ><asp:Button  runat="server" ID="cmdfrmupdpanelctrl000004updcancel" Text="Cancelar" CssClass="boton-acciones" CausesValidation="False" OnClick="cmdfrmupdpanelctrl000004updcancel_Click" />
<asp:Button  runat="server" ID="cmdfrmupdpanelctrl000004updconfirm" Text="Guardar" CssClass="boton-acciones" CausesValidation="True" OnClick="cmdfrmupdpanelctrl000004updconfirm_Click" ValidationGroup="vgeditupdpanelfrmupdpanelctrl000004" />
</div>
<table class="form" width="100%" cellpadding="1" cellspacing="1"><tr>
<td  colspan="1" >

</td>
<td  colspan="1" >
<asp:SqlDataSource  runat="server" ID="dsedtrolcod" CancelSelectOnNullParameter="False" onInit="dsedtrolcod_Init" SelectCommandType="Text"
 SelectCommand="SELECT DOC_ROL.dsc,DOC_ROL.cod FROM DOC_ROL   WHERE DOC_ROL.cod >= 1 ORDER BY DOC_ROL.orden" onselected="dsedtrolcod_Selected" >
<SelectParameters>
</SelectParameters>
</asp:SqlDataSource>

</td>
</tr>
<tr>
<td  colspan="1" >
Rol
</td>
<td  colspan="1" >
<asp:UpdatePanel  ID="updupdedtrolcodfs" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="True" >
<ContentTemplate>
<asp:DropDownList  runat="server" ID="edtrolcod" CssClass="form-control" DataSourceID="dsedtrolcod" AppendDataBoundItems="False" DataTextField="dsc" DataValueField="cod" Enabled="True" onDataBound="edtrolcod_DataBound" >
</asp:DropDownList>
<asp:LinkButton runat="server" ID="edtrolcodnew" Text="Nuevo..." CssClass="boton-acciones-subbutton" CausesValidation="False" OnClientClick='<%# "javascript:if (window.showModalDialog) {window.showModalDialog(" & chr(39) & "frmRoles_det.aspx?_mode_=1&_closea_=1&isDlg=1&timestamp=" & chr(39) & " + new Date().getTime() + " & chr(39) & "" & chr(39) & ", " & chr(39) & "" & chr(39) & ", " & chr(39) & "menubar=no;resizable=yes;toolbar=no;status=no;center=yes;dialogWidth=750px;dialogHeight=570px;" & chr(39) & ");} else {window.open(" & chr(39) & "frmRoles_det.aspx?_mode_=1&_closea_=1&isDlg=1&timestamp=" & chr(39) & " + new Date().getTime() + " & chr(39) & "" & chr(39) & ", " & chr(39) & "" & chr(39) & ", " & chr(39) & "modal=yes;directories=no;menubar=no;resizable=yes;toolbar=no;status=no;center=yes;dialogWidth=750px;dialogHeight=570px;" & chr(39) & ");};return false;" %>' PostBackUrl='<%# "javascript:if (window.showModalDialog) {window.showModalDialog(" & chr(39) & "frmRoles_det.aspx?_mode_=1&_closea_=1&isDlg=1&timestamp=" & chr(39) & " + new Date().getTime() + " & chr(39) & "" & chr(39) & ", " & chr(39) & "" & chr(39) & ", " & chr(39) & "menubar=no;resizable=yes;toolbar=no;status=no;center=yes;dialogWidth=750px;dialogHeight=570px;" & chr(39) & ");} else {window.open(" & chr(39) & "frmRoles_det.aspx?_mode_=1&_closea_=1&isDlg=1&timestamp=" & chr(39) & " + new Date().getTime() + " & chr(39) & "" & chr(39) & ", " & chr(39) & "" & chr(39) & ", " & chr(39) & "modal=yes;directories=no;menubar=no;resizable=yes;toolbar=no;status=no;center=yes;dialogWidth=750px;dialogHeight=570px;" & chr(39) & ");};return false;" %>' />
<asp:LinkButton runat="server" ID="edtrolcod_comborefresh" Text="Refrescar" CssClass="boton-acciones-subbutton" OnClick="edtrolcod_comborefresh_Click" CausesValidation="False" />
<asp:LinkButton runat="server" ID="edtrolcodview" Text='<%# Eval("rolcoddsc") %>' CssClass="boton-acciones-subbutton" OnDataBinding="edtrolcodview_DataBinding" CausesValidation="False" OnClientClick='<%# "javascript:if (window.showModalDialog) {window.showModalDialog(" & chr(39) & "frmRoles_det.aspx?_mode_=0&_closea_=1&isDlg=1&param1=" & Eval("rolcod") & "&timestamp=" & chr(39) & " + new Date().getTime() + " & chr(39) & "" & chr(39) & ", " & chr(39) & "" & chr(39) & ", " & chr(39) & "menubar=no;resizable=yes;toolbar=no;status=no;center=yes;dialogWidth=750px;dialogHeight=570px;" & chr(39) & ");} else {window.open(" & chr(39) & "frmRoles_det.aspx?_mode_=0&_closea_=1&isDlg=1&param1=" & Eval("rolcod") & "&timestamp=" & chr(39) & " + new Date().getTime() + " & chr(39) & "" & chr(39) & ", " & chr(39) & "" & chr(39) & ", " & chr(39) & "modal=yes;directories=no;menubar=no;resizable=yes;toolbar=no;status=no;center=yes;dialogWidth=750px;dialogHeight=570px;" & chr(39) & ");};return false;" %>' PostBackUrl='<%# "javascript:if (window.showModalDialog) {window.showModalDialog(" & chr(39) & "frmRoles_det.aspx?_mode_=0&_closea_=1&isDlg=1&param1=" & Eval("rolcod") & "&timestamp=" & chr(39) & " + new Date().getTime() + " & chr(39) & "" & chr(39) & ", " & chr(39) & "" & chr(39) & ", " & chr(39) & "menubar=no;resizable=yes;toolbar=no;status=no;center=yes;dialogWidth=750px;dialogHeight=570px;" & chr(39) & ");} else {window.open(" & chr(39) & "frmRoles_det.aspx?_mode_=0&_closea_=1&isDlg=1&param1=" & Eval("rolcod") & "&timestamp=" & chr(39) & " + new Date().getTime() + " & chr(39) & "" & chr(39) & ", " & chr(39) & "" & chr(39) & ", " & chr(39) & "modal=yes;directories=no;menubar=no;resizable=yes;toolbar=no;status=no;center=yes;dialogWidth=750px;dialogHeight=570px;" & chr(39) & ");};return false;" %>' />
</ContentTemplate>
</asp:UpdatePanel>
<asp:UpdateProgress  runat="server" AssociatedUpdatePanelID="updupdedtrolcodfs" >
<ProgressTemplate><span  class="ajaxupd" ><div style="position:absolute;top:2px;left:50%; border-color:#333333; border:1; border-style:solid; background-color:#FFFFFF;padding:3px;"><image src="imagenes/ajaxupdate.gif" border=0 />Actualizando</div></span></ProgressTemplate>
</asp:UpdateProgress>

</td>
</tr>
<tr>
<td  colspan="1" >

</td>
<td  colspan="1" >
<asp:SqlDataSource  runat="server" ID="dsedtrolcodtype" CancelSelectOnNullParameter="False" onInit="dsedtrolcodtype_Init" SelectCommandType="Text"
 SelectCommand="SELECT DOC_TROROL.(sin seleccion),DOC_TROROL.cod FROM DOC_TROROL   WHERE DOC_TROROL.cod >= 1 ORDER BY DOC_TROROL.cod" onselected="dsedtrolcodtype_Selected" >
<SelectParameters>
</SelectParameters>
</asp:SqlDataSource>

</td>
</tr>
<tr>
<td  colspan="1" >
<b>Miiembro de rol</b>
</td>
<td  colspan="1" >
<asp:UpdatePanel  ID="updupdpnlupdedtrolcodtypefs" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="True" >
<Triggers>
  <asp:AsyncPostBackTrigger  ControlID="edtrolcodtypedelete" EventName="Click" />
</Triggers>
<ContentTemplate>
<asp:UpdatePanel  ID="updupdedtrolcodtypefs" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="True" >
<ContentTemplate>
<asp:Button  runat="server" ID="cmdedtrolcodtypeshowpanel" Text="..." CssClass="boton-acciones" CausesValidation="False" OnClick="cmdedtrolcodtypeshowpanel_Click" />
 <span class='error'><b>*</b></span>
<asp:ImageButton runat="server" ID="edtrolcodtypedelete" ImageURL="~/imagenes/actdel.png" Width="16" Height="16" Tooltip="Quitar Miiembro de rol" Visible="False" OnClick="edtrolcodtypedelete_Click" OnDataBinding="edtrolcodtypedelete_DataBinding" CausesValidation="False" />
<asp:LinkButton runat="server" ID="edtrolcodtypeview" Text='<%# Eval("rolcodtypedsc") %>' CssClass="boton-acciones-subbutton" OnDataBinding="edtrolcodtypeview_DataBinding" CausesValidation="False" />
<asp:HiddenField  runat="server" ID="edtrolcodtype"/>
<asp:HiddenField  runat="server" ID="edtrolcodtypevalue" OnDataBinding="edtrolcodtypevalue_DataBound"/><asp:CustomValidator  runat="server" ID="vcusval7d974b5798034b2381a14443a0c15680" SetFocusOnError="true" CssClass="error" Display="Dynamic" OnServerValidate="val7d974b5798034b2381a14443a0c15680_OnServerValidate" Text="Obligatorio!!! ErrorMessage=´Miiembro de rol:es un dato obligatorio!´" />


<br /><asp:Panel  ID="pnledtrolcodtypevalue_10" runat="server" BorderWidth="1" BorderStyle="solid" onLoad="edtrolcodtypevalue_10_Load" >
<tr><td  colspan="1" >
<asp:SqlDataSource  runat="server" ID="dsedtrolcodtypevalue_10_rolgrpcod" CancelSelectOnNullParameter="False" onInit="dsedtrolcodtypevalue_10_rolgrpcod_Init" SelectCommandType="Text"
 SelectCommand="SELECT ROLGRP.dsc,ROLGRP.cod FROM ROLGRP   ORDER BY ROLGRP.orden" onselected="dsedtrolcodtypevalue_10_rolgrpcod_Selected" >
<SelectParameters>
</SelectParameters>
</asp:SqlDataSource>

<br />
Rol en unidad
<asp:UpdatePanel  ID="updupdedtrolcodtypevalue_10_rolgrpcodfs" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="True" >
<ContentTemplate>
<asp:DropDownList  runat="server" ID="edtrolcodtypevalue_10_rolgrpcod" CssClass="form-control" DataSourceID="dsedtrolcodtypevalue_10_rolgrpcod" AppendDataBoundItems="False" DataTextField="dsc" DataValueField="cod" Enabled="True" >
</asp:DropDownList>
<asp:LinkButton runat="server" ID="edtrolcodtypevalue_10_rolgrpcodview" Text='<%# Eval("dsc") %>' CssClass="boton-acciones-subbutton" OnDataBinding="edtrolcodtypevalue_10_rolgrpcodview_DataBinding" CausesValidation="False" />
</ContentTemplate>
</asp:UpdatePanel>
<asp:UpdateProgress  runat="server" AssociatedUpdatePanelID="updupdedtrolcodtypevalue_10_rolgrpcodfs" >
<ProgressTemplate><span  class="ajaxupd" ><div style="position:absolute;top:2px;left:50%; border-color:#333333; border:1; border-style:solid; background-color:#FFFFFF;padding:3px;"><image src="imagenes/ajaxupdate.gif" border=0 />Actualizando</div></span></ProgressTemplate>
</asp:UpdateProgress>

<br />
</td></tr></asp:Panel>
</ContentTemplate>
</asp:UpdatePanel>
<asp:UpdateProgress  runat="server" AssociatedUpdatePanelID="updupdedtrolcodtypefs" >
<ProgressTemplate><span  class="ajaxupd" ><div style="position:absolute;top:2px;left:50%; border-color:#333333; border:1; border-style:solid; background-color:#FFFFFF;padding:3px;"><image src="imagenes/ajaxupdate.gif" border=0 />Actualizando</div></span></ProgressTemplate>
</asp:UpdateProgress>
</ContentTemplate>
</asp:UpdatePanel>
<asp:UpdateProgress  runat="server" AssociatedUpdatePanelID="updupdpnlupdedtrolcodtypefs" >
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
<asp:SqlDataSource  runat="server" ID="dsinsrolcod" CancelSelectOnNullParameter="False" onInit="dsinsrolcod_Init" SelectCommandType="Text"
 SelectCommand="SELECT DOC_ROL.dsc,DOC_ROL.cod FROM DOC_ROL   WHERE DOC_ROL.cod >= 1 ORDER BY DOC_ROL.orden" onselected="dsinsrolcod_Selected" >
<SelectParameters>
</SelectParameters>
</asp:SqlDataSource>

</td>
</tr>
<tr>
<td  colspan="1" >
Rol
</td>
<td  colspan="1" >
<asp:UpdatePanel  ID="updupdinsrolcodfs" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="True" >
<ContentTemplate>
<asp:DropDownList  runat="server" ID="insrolcod" CssClass="form-control" DataSourceID="dsinsrolcod" AppendDataBoundItems="False" DataTextField="dsc" DataValueField="cod" Enabled="True" onload="insrolcod_Load" >
<asp:ListItem value="-1">Todos</asp:ListItem>
</asp:DropDownList>
<asp:LinkButton runat="server" ID="insrolcodnew" Text="Nuevo..." CssClass="boton-acciones-subbutton" CausesValidation="False" OnClientClick='<%# "javascript:if (window.showModalDialog) {window.showModalDialog(" & chr(39) & "frmRoles_det.aspx?_mode_=1&_closea_=1&isDlg=1&timestamp=" & chr(39) & " + new Date().getTime() + " & chr(39) & "" & chr(39) & ", " & chr(39) & "" & chr(39) & ", " & chr(39) & "menubar=no;resizable=yes;toolbar=no;status=no;center=yes;dialogWidth=750px;dialogHeight=570px;" & chr(39) & ");} else {window.open(" & chr(39) & "frmRoles_det.aspx?_mode_=1&_closea_=1&isDlg=1&timestamp=" & chr(39) & " + new Date().getTime() + " & chr(39) & "" & chr(39) & ", " & chr(39) & "" & chr(39) & ", " & chr(39) & "modal=yes;directories=no;menubar=no;resizable=yes;toolbar=no;status=no;center=yes;dialogWidth=750px;dialogHeight=570px;" & chr(39) & ");};return false;" %>' PostBackUrl='<%# "javascript:if (window.showModalDialog) {window.showModalDialog(" & chr(39) & "frmRoles_det.aspx?_mode_=1&_closea_=1&isDlg=1&timestamp=" & chr(39) & " + new Date().getTime() + " & chr(39) & "" & chr(39) & ", " & chr(39) & "" & chr(39) & ", " & chr(39) & "menubar=no;resizable=yes;toolbar=no;status=no;center=yes;dialogWidth=750px;dialogHeight=570px;" & chr(39) & ");} else {window.open(" & chr(39) & "frmRoles_det.aspx?_mode_=1&_closea_=1&isDlg=1&timestamp=" & chr(39) & " + new Date().getTime() + " & chr(39) & "" & chr(39) & ", " & chr(39) & "" & chr(39) & ", " & chr(39) & "modal=yes;directories=no;menubar=no;resizable=yes;toolbar=no;status=no;center=yes;dialogWidth=750px;dialogHeight=570px;" & chr(39) & ");};return false;" %>' />
<asp:LinkButton runat="server" ID="insrolcod_comborefresh" Text="Refrescar" CssClass="boton-acciones-subbutton" OnClick="insrolcod_comborefresh_Click" CausesValidation="False" />
</ContentTemplate>
</asp:UpdatePanel>
<asp:UpdateProgress  runat="server" AssociatedUpdatePanelID="updupdinsrolcodfs" >
<ProgressTemplate><span  class="ajaxupd" ><div style="position:absolute;top:2px;left:50%; border-color:#333333; border:1; border-style:solid; background-color:#FFFFFF;padding:3px;"><image src="imagenes/ajaxupdate.gif" border=0 />Actualizando</div></span></ProgressTemplate>
</asp:UpdateProgress>

</td>
</tr>
<tr>
<td  colspan="1" >

</td>
<td  colspan="1" >
<asp:SqlDataSource  runat="server" ID="dsinsrolcodtype" CancelSelectOnNullParameter="False" onInit="dsinsrolcodtype_Init" SelectCommandType="Text"
 SelectCommand="SELECT DOC_TROROL.(sin seleccion),DOC_TROROL.cod FROM DOC_TROROL   WHERE DOC_TROROL.cod >= 1 ORDER BY DOC_TROROL.cod" onselected="dsinsrolcodtype_Selected" >
<SelectParameters>
</SelectParameters>
</asp:SqlDataSource>

</td>
</tr>
<tr>
<td  colspan="1" >
<b>Miiembro de rol</b>
</td>
<td  colspan="1" >
<asp:UpdatePanel  ID="updupdpnlupdinsrolcodtypefs" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="True" >
<Triggers>
  <asp:AsyncPostBackTrigger  ControlID="insrolcodtypedelete" EventName="Click" />
</Triggers>
<ContentTemplate>
<asp:UpdatePanel  ID="updupdinsrolcodtypefs" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="True" >
<ContentTemplate>
<asp:Button  runat="server" ID="cmdinsrolcodtypeshowpanel" Text="..." CssClass="boton-acciones" CausesValidation="False" OnClick="cmdinsrolcodtypeshowpanel_Click" />
 <span class='error'><b>*</b></span>
<asp:ImageButton runat="server" ID="insrolcodtypedelete" ImageURL="~/imagenes/actdel.png" Width="16" Height="16" Tooltip="Quitar Miiembro de rol" Visible="False" OnClick="insrolcodtypedelete_Click" OnDataBinding="insrolcodtypedelete_DataBinding" CausesValidation="False" />
<asp:LinkButton runat="server" ID="insrolcodtypeview" CssClass="boton-acciones-subbutton" CausesValidation="False" />
<asp:HiddenField  runat="server" ID="insrolcodtype"/>
<asp:HiddenField  runat="server" ID="insrolcodtypevalue"/>
<br /><asp:Panel  ID="pnlinsrolcodtypevalue_10" runat="server" BorderWidth="1" BorderStyle="solid" onLoad="insrolcodtypevalue_10_Load" >
<tr><td  colspan="1" >
<asp:SqlDataSource  runat="server" ID="dsinsrolcodtypevalue_10_rolgrpcod" CancelSelectOnNullParameter="False" onInit="dsinsrolcodtypevalue_10_rolgrpcod_Init" SelectCommandType="Text"
 SelectCommand="SELECT ROLGRP.dsc,ROLGRP.cod FROM ROLGRP   ORDER BY ROLGRP.orden" onselected="dsinsrolcodtypevalue_10_rolgrpcod_Selected" >
<SelectParameters>
</SelectParameters>
</asp:SqlDataSource>

<br />
Rol en unidad
<asp:UpdatePanel  ID="updupdinsrolcodtypevalue_10_rolgrpcodfs" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="True" >
<ContentTemplate>
<asp:DropDownList  runat="server" ID="insrolcodtypevalue_10_rolgrpcod" CssClass="form-control" DataSourceID="dsinsrolcodtypevalue_10_rolgrpcod" AppendDataBoundItems="False" DataTextField="dsc" DataValueField="cod" Enabled="True" >
</asp:DropDownList>
<asp:LinkButton runat="server" ID="insrolcodtypevalue_10_rolgrpcodview" Text='<%# Eval("rolgrpcoddsc") %>' CssClass="boton-acciones-subbutton" OnDataBinding="insrolcodtypevalue_10_rolgrpcodview_DataBinding" CausesValidation="False" />
</ContentTemplate>
</asp:UpdatePanel>
<asp:UpdateProgress  runat="server" AssociatedUpdatePanelID="updupdinsrolcodtypevalue_10_rolgrpcodfs" >
<ProgressTemplate><span  class="ajaxupd" ><div style="position:absolute;top:2px;left:50%; border-color:#333333; border:1; border-style:solid; background-color:#FFFFFF;padding:3px;"><image src="imagenes/ajaxupdate.gif" border=0 />Actualizando</div></span></ProgressTemplate>
</asp:UpdateProgress>

<br />
</td></tr></asp:Panel>
</ContentTemplate>
</asp:UpdatePanel>
<asp:UpdateProgress  runat="server" AssociatedUpdatePanelID="updupdinsrolcodtypefs" >
<ProgressTemplate><span  class="ajaxupd" ><div style="position:absolute;top:2px;left:50%; border-color:#333333; border:1; border-style:solid; background-color:#FFFFFF;padding:3px;"><image src="imagenes/ajaxupdate.gif" border=0 />Actualizando</div></span></ProgressTemplate>
</asp:UpdateProgress>
</ContentTemplate>
</asp:UpdatePanel>
<asp:UpdateProgress  runat="server" AssociatedUpdatePanelID="updupdpnlupdinsrolcodtypefs" >
<ProgressTemplate><span  class="ajaxupd" ><div style="position:absolute;top:2px;left:50%; border-color:#333333; border:1; border-style:solid; background-color:#FFFFFF;padding:3px;"><image src="imagenes/ajaxupdate.gif" border=0 />Actualizando</div></span></ProgressTemplate>
</asp:UpdateProgress>

</td>
</tr>
</table>
<!-- End Plantilla insercion --></InsertItemTemplate>

</asp:FormView>

<asp:SqlDataSource  runat="server" ID="dsfrmupdpanelctrl000004" ProviderName="System.Data.SqlClient" CancelSelectOnNullParameter="False" onInit="dsfrmupdpanelctrl000004_Init" SelectCommandType="Text"
 SelectCommand="SELECT DOC_TROROL_DFT.cod,(DOC_TROROL_DFTTROCOD.dsc) as trocoddsc,(DOC_TROROL_DFTROLCOD.dsc) as rolcoddsc,DOC_TROROL_DFT.rolcodtype,CASE DOC_TROROL_DFT.rolcodtype WHEN 9 THEN (SELECT TOP 1 DOC_TROROLEMP_DFT.empcod FROM DOC_TROROLEMP_DFT WHERE DOC_TROROLEMP_DFT.trorolcod=DOC_TROROL_DFT.cod AND DOC_TROROLEMP_DFT.dftdidgencod=DOC_TROROL_DFT.dftdidgencod) WHEN 10 THEN (SELECT TOP 1 DOC_TROROLUND_DFT.undcod FROM DOC_TROROLUND_DFT WHERE DOC_TROROLUND_DFT.trorolcod=DOC_TROROL_DFT.cod AND DOC_TROROLUND_DFT.dftdidgencod=DOC_TROROL_DFT.dftdidgencod) WHEN 14 THEN (SELECT TOP 1 DOC_TROROLEQU_DFT.equcod FROM DOC_TROROLEQU_DFT WHERE DOC_TROROLEQU_DFT.trorolcod=DOC_TROROL_DFT.cod AND DOC_TROROLEQU_DFT.dftdidgencod=DOC_TROROL_DFT.dftdidgencod) WHEN 43 THEN (SELECT TOP 1 DOC_TROROLDYNGRP_DFT.dyngrpcod FROM DOC_TROROLDYNGRP_DFT WHERE DOC_TROROLDYNGRP_DFT.trorolcod=DOC_TROROL_DFT.cod AND DOC_TROROLDYNGRP_DFT.dftdidgencod=DOC_TROROL_DFT.dftdidgencod) END as rolcodtypevalue,CASE DOC_TROROL_DFT.rolcodtype WHEN 9 THEN (SELECT TOP 1 EMP.dsc FROM EMP WHERE EMP.cod= (SELECT TOP 1 DOC_TROROLEMP_DFT.empcod FROM DOC_TROROLEMP_DFT WHERE DOC_TROROLEMP_DFT.trorolcod=DOC_TROROL_DFT.cod AND DOC_TROROLEMP_DFT.dftdidgencod=DOC_TROROL_DFT.dftdidgencod)) WHEN 10 THEN (SELECT TOP 1 UND.dsc FROM UND WHERE UND.cod= (SELECT TOP 1 DOC_TROROLUND_DFT.undcod FROM DOC_TROROLUND_DFT WHERE DOC_TROROLUND_DFT.trorolcod=DOC_TROROL_DFT.cod AND DOC_TROROLUND_DFT.dftdidgencod=DOC_TROROL_DFT.dftdidgencod)) WHEN 14 THEN (SELECT TOP 1 DOC_EQU.dsc FROM DOC_EQU WHERE DOC_EQU.cod= (SELECT TOP 1 DOC_TROROLEQU_DFT.equcod FROM DOC_TROROLEQU_DFT WHERE DOC_TROROLEQU_DFT.trorolcod=DOC_TROROL_DFT.cod AND DOC_TROROLEQU_DFT.dftdidgencod=DOC_TROROL_DFT.dftdidgencod)) WHEN 43 THEN (SELECT TOP 1 DOC_DYNGRP.dsc FROM DOC_DYNGRP WHERE DOC_DYNGRP.cod= (SELECT TOP 1 DOC_TROROLDYNGRP_DFT.dyngrpcod FROM DOC_TROROLDYNGRP_DFT WHERE DOC_TROROLDYNGRP_DFT.trorolcod=DOC_TROROL_DFT.cod AND DOC_TROROLDYNGRP_DFT.dftdidgencod=DOC_TROROL_DFT.dftdidgencod)) END as rolcodtypedsc,(DOC_TROROL_DFTQSECSID.secdsc) as qsecsiddsc,DOC_TROROL_DFT.qsecdatetime,(DOC_TROROL_DFTDFTDIDGENCOD.dsc) as dftdidgencoddsc,DOC_TROROL_DFT.trocod,DOC_TROROL_DFT.rolcod,DOC_TROROL_DFT.qsecsid,DOC_TROROL_DFT.dftdidgencod FROM DOC_TROROL_DFT  LEFT JOIN DOC_TRO_DFT AS DOC_TROROL_DFTTROCOD ON DOC_TROROL_DFTTROCOD.cod=DOC_TROROL_DFT.trocod LEFT JOIN DOC_ROL AS DOC_TROROL_DFTROLCOD ON DOC_TROROL_DFTROLCOD.cod=DOC_TROROL_DFT.rolcod LEFT JOIN Q_SECPLOGIN AS DOC_TROROL_DFTQSECSID ON DOC_TROROL_DFTQSECSID.sidcod=DOC_TROROL_DFT.qsecsid LEFT JOIN DOC_TRO_DFT AS DOC_TROROL_DFTDFTDIDGENCOD ON DOC_TROROL_DFTDFTDIDGENCOD.dftdidgencod=DOC_TROROL_DFT.dftdidgencod  WHERE DOC_TROROL_DFT.cod=@cod AND DOC_TROROL_DFT.dftdidgencod= @dftdidgencod" onselected="dsfrmupdpanelctrl000004_Selected" UpdateCommandType="Text"
 UpdateCommand="UPDATE DOC_TROROL_DFT SET cod=@cod,rolcod=@rolcod,rolcodtype=@rolcodtype WHERE DOC_TROROL_DFT.cod=@cod AND DOC_TROROL_DFT.dftdidgencod= @dftdidgencod" onupdated="dsfrmupdpanelctrl000004_Updated" InsertCommandType="Text"
 InsertCommand="SET @querynextcod=(SELECT ISNULL(MIN(cod),-100)-1 FROM DOC_TROROL_DFT WHERE cod< -100 AND dftdidgencod=@dftdidgencod) INSERT INTO DOC_TROROL_DFT (cod,trocod,rolcod,rolcodtype,qsecdatetime,dftdidgencod) VALUES(@querynextcod,@trocod,@rolcod,@rolcodtype,getdate(),@dftdidgencod)INSERT INTO DOC_TROROL_DFTREL (cod,qdft_cod,dftdidgencod) VALUES(-1,@querynextcod,@dftdidgencod) SELECT @querynextcod AS cod" onInserted="dsfrmupdpanelctrl000004_Inserted" >
<InsertParameters>
<asp:ControlParameter  Name="trocod" ControlID="frmdatos" ConvertEmptyStringToNull="True" Direction="Input" Type="String" Size="200" PropertyName="SelectedValue" />
<asp:ControlParameter  Name="rolcod" ControlID="frmupdpanelfrmupdpanelctrl000004$insrolcod" ConvertEmptyStringToNull="True" Direction="InputOutput" Type="Int32" PropertyName="SelectedValue" />
<asp:ControlParameter  Name="rolcodtype" ControlID="frmupdpanelfrmupdpanelctrl000004$insrolcodtype" ConvertEmptyStringToNull="True" Direction="InputOutput" Type="Int32" PropertyName="Value" />
<asp:Parameter  Name="querynextcod" Direction="Output" Type="Int32" />
<asp:ControlParameter  Name="dftdidgencod" ControlID="q_hdnhrcdftgencod" ConvertEmptyStringToNull="True" Direction="Input" Type="String" Size="200" PropertyName="Value" />
</InsertParameters>
<SelectParameters>
<asp:Parameter  Name="cod" Direction="InputOutput" Type="String" Size="200" />
<asp:ControlParameter  Name="dftdidgencod" ControlID="q_hdnhrcdftgencod" ConvertEmptyStringToNull="True" Direction="Input" Type="String" Size="200" PropertyName="Value" />
</SelectParameters>
<UpdateParameters>
<asp:Parameter  Name="cod" Direction="InputOutput" Type="String" Size="200" />
<asp:ControlParameter  Name="rolcod" ControlID="frmupdpanelfrmupdpanelctrl000004$edtrolcod" ConvertEmptyStringToNull="True" Direction="InputOutput" Type="Int32" PropertyName="SelectedValue" />
<asp:ControlParameter  Name="rolcodtype" ControlID="frmupdpanelfrmupdpanelctrl000004$edtrolcodtype" ConvertEmptyStringToNull="True" Direction="InputOutput" Type="Int32" PropertyName="Value" />
<asp:ControlParameter  Name="dftdidgencod" ControlID="q_hdnhrcdftgencod" ConvertEmptyStringToNull="True" Direction="Input" Type="String" Size="200" PropertyName="Value" />
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

