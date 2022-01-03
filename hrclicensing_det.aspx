<%@ Page Language="VB" CodeFile="hrclicensing_det.aspx.vb" MasterPageFile="general.master" AutoEventWireup="True" Inherits="hrclicensing_det" Title="Licencias" ValidateRequest="False" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="ajaxkit" %>
<%@ Register Assembly = "AjaxControlToolkit"  Namespace="AjaxControlToolkit.HTMLEditor" TagPrefix="HTMLEditor" %>
<asp:Content id="cHeader" ContentPlaceHolderID="conHeader" runat="Server">
<style  media="print" type="text/css" > .hide_print {display: none;}</style>
</asp:Content>
<asp:Content id="cDatos" ContentPlaceHolderID="ContentDatos" runat="Server">
<asp:ScriptManager  ID="ScriptManagerHrc" runat="server" EnableScriptLocalization="True" EnableScriptGlobalization="True" ScriptMode="Release" >
</asp:ScriptManager>
<table width="100%" cellpadding="0" cellspacing="0">
<tr><td  colspan="1" >
<table width="100%" cellpadding="0" cellspacing="0">
<tr><td  style="width:32px;" colspan="1" ><img  src="imagenes/iconLicencia.png" width="32px" height="32px" alt="Licencias" hspace="4"/></td>
<td  style="text-align:left;" colspan="1" ><span class="form-title">Licencias</span> |<asp:Label  runat="server" ID="lblsubtitle" CssClass="form-subtitle" />
</td></tr>
</table>
</td></tr>

<tr>
<td  colspan="1" >
							<!-- Init -->
<table id="body" style="background-color:#F0F0F0;" width="100%">

								<br /><asp:Label  runat="server" ID="lblerror" CssClass="error" width="100%" />
						<asp:ValidationSummary  runat="server" ID="frmdatosvalsumary" />
<asp:FormView  ID="frmdatos" runat="server" DataSourceID="dsdatos" DataKeyNames="liccod" DefaultMode="ReadOnly" OnItemDeleted="frmdatos_ItemDeleted" OnItemUpdated="frmdatos_ItemUpdated" OnItemInserted="frmdatos_ItemInserted" OnDataBound="frmdatos_ItemSelected" >
<ItemTemplate>

<!-- Init Plantilla item --><div  runat="server" Visible='<%# Request.Querystring("_view_")<> 1 %>' ><asp:Button  runat="server" ID="cmdrefresh" Text="Refrescar" CssClass="boton-acciones" CausesValidation="False" />
<asp:Button  runat="server" ID="cmdFormViewItemCancel" Text="Cancelar" CssClass="boton-acciones" CausesValidation="False" OnClick="cmdFormViewItemCancel_Click" />
<asp:Button  runat="server" ID="cmdFormViewItemUpdate" Text="Editar" CssClass="boton-acciones" CausesValidation="False" OnClick="cmdFormViewItemUpdate_Click" />
</div>
<table width="100%" cellpadding="1" cellspacing="1"><tr>
<td  colspan="1" >
Código
</td>
<td  colspan="1" >
<asp:Label  runat="server" ID="itemctrl000018" CssClass="form-control-read" width="100%" OnDataBinding="itemctrl000018_Databound" />
</td>
</tr>
<tr>
<td  colspan="1" >
Código
</td>
<td  colspan="1" >
<asp:Label  runat="server" ID="itemctrl000016" CssClass="form-control-read" width="100%" OnDataBinding="itemctrl000016_Databound" />
</td>
</tr>
<tr>
<td  colspan="1" >
Descripcion
</td>
<td  colspan="1" >
<asp:Label  runat="server" ID="itemctrl000015" CssClass="form-control-read" width="100%" OnDataBinding="itemctrl000015_Databound" />
</td>
</tr>
<tr>
<td  colspan="1" >
Clave autorizante
</td>
<td  colspan="1" >
<asp:LinkButton runat="server" ID="itemctrl000014view" Text='<%# Eval("lickyaautcoddsc") %>' CssClass="form-control-read" OnDataBinding="itemctrl000014view_DataBinding" CausesValidation="False" OnClientClick='<%# "javascript:if (window.showModalDialog) {window.showModalDialog(" & chr(39) & "hrckya_det.aspx?_mode_=0&_closea_=1&isDlg=1&param1=" & Eval("lickyaautcod") & "&timestamp=" & chr(39) & " + new Date().getTime() + " & chr(39) & "" & chr(39) & ", " & chr(39) & "" & chr(39) & ", " & chr(39) & "menubar=no;resizable=yes;toolbar=no;status=no;center=yes;dialogWidth=750px;dialogHeight=570px;" & chr(39) & ");} else {window.open(" & chr(39) & "hrckya_det.aspx?_mode_=0&_closea_=1&isDlg=1&param1=" & Eval("lickyaautcod") & "&timestamp=" & chr(39) & " + new Date().getTime() + " & chr(39) & "" & chr(39) & ", " & chr(39) & "" & chr(39) & ", " & chr(39) & "modal=yes;directories=no;menubar=no;resizable=yes;toolbar=no;status=no;center=yes;dialogWidth=750px;dialogHeight=570px;" & chr(39) & ");};return false;" %>' PostBackUrl='<%# "javascript:if (window.showModalDialog) {window.showModalDialog(" & chr(39) & "hrckya_det.aspx?_mode_=0&_closea_=1&isDlg=1&param1=" & Eval("lickyaautcod") & "&timestamp=" & chr(39) & " + new Date().getTime() + " & chr(39) & "" & chr(39) & ", " & chr(39) & "" & chr(39) & ", " & chr(39) & "menubar=no;resizable=yes;toolbar=no;status=no;center=yes;dialogWidth=750px;dialogHeight=570px;" & chr(39) & ");} else {window.open(" & chr(39) & "hrckya_det.aspx?_mode_=0&_closea_=1&isDlg=1&param1=" & Eval("lickyaautcod") & "&timestamp=" & chr(39) & " + new Date().getTime() + " & chr(39) & "" & chr(39) & ", " & chr(39) & "" & chr(39) & ", " & chr(39) & "modal=yes;directories=no;menubar=no;resizable=yes;toolbar=no;status=no;center=yes;dialogWidth=750px;dialogHeight=570px;" & chr(39) & ");};return false;" %>' />

</td>
</tr>
<tr>
<td  colspan="1" >
Clave emisora
</td>
<td  colspan="1" >
<asp:LinkButton runat="server" ID="itemctrl000013view" Text='<%# Eval("lickyacoddsc") %>' CssClass="form-control-read" OnDataBinding="itemctrl000013view_DataBinding" CausesValidation="False" OnClientClick='<%# "javascript:if (window.showModalDialog) {window.showModalDialog(" & chr(39) & "hrckya_det.aspx?_mode_=0&_closea_=1&isDlg=1&param1=" & Eval("lickyacod") & "&timestamp=" & chr(39) & " + new Date().getTime() + " & chr(39) & "" & chr(39) & ", " & chr(39) & "" & chr(39) & ", " & chr(39) & "menubar=no;resizable=yes;toolbar=no;status=no;center=yes;dialogWidth=750px;dialogHeight=570px;" & chr(39) & ");} else {window.open(" & chr(39) & "hrckya_det.aspx?_mode_=0&_closea_=1&isDlg=1&param1=" & Eval("lickyacod") & "&timestamp=" & chr(39) & " + new Date().getTime() + " & chr(39) & "" & chr(39) & ", " & chr(39) & "" & chr(39) & ", " & chr(39) & "modal=yes;directories=no;menubar=no;resizable=yes;toolbar=no;status=no;center=yes;dialogWidth=750px;dialogHeight=570px;" & chr(39) & ");};return false;" %>' PostBackUrl='<%# "javascript:if (window.showModalDialog) {window.showModalDialog(" & chr(39) & "hrckya_det.aspx?_mode_=0&_closea_=1&isDlg=1&param1=" & Eval("lickyacod") & "&timestamp=" & chr(39) & " + new Date().getTime() + " & chr(39) & "" & chr(39) & ", " & chr(39) & "" & chr(39) & ", " & chr(39) & "menubar=no;resizable=yes;toolbar=no;status=no;center=yes;dialogWidth=750px;dialogHeight=570px;" & chr(39) & ");} else {window.open(" & chr(39) & "hrckya_det.aspx?_mode_=0&_closea_=1&isDlg=1&param1=" & Eval("lickyacod") & "&timestamp=" & chr(39) & " + new Date().getTime() + " & chr(39) & "" & chr(39) & ", " & chr(39) & "" & chr(39) & ", " & chr(39) & "modal=yes;directories=no;menubar=no;resizable=yes;toolbar=no;status=no;center=yes;dialogWidth=750px;dialogHeight=570px;" & chr(39) & ");};return false;" %>' />

</td>
</tr>
<tr>
<td  colspan="1" >
ID de solicitud
</td>
<td  colspan="1" >
<asp:Label  runat="server" ID="itemctrl000012" CssClass="form-control-read" width="100%" OnDataBinding="itemctrl000012_Databound" />
</td>
</tr>
<tr>
<td  colspan="1" >
Usuario de conexión
</td>
<td  colspan="1" >
<asp:Label  runat="server" ID="itemctrl000011" CssClass="form-control-read" width="100%" OnDataBinding="itemctrl000011_Databound" />
</td>
</tr>
<tr>
<td  colspan="1" >
Password de conexión
</td>
<td  colspan="1" >
<asp:Label  runat="server" ID="itemctrl000010" CssClass="form-control-read" width="100%" OnDataBinding="itemctrl000010_Databound" />
</td>
</tr>
<tr>
<td  colspan="2" >
Solicitud
</td>
</tr>
<tr>
<td  colspan="2" >
<asp:Label  runat="server" ID="itemctrl000009" CssClass="form-control-read" width="100%" OnDataBinding="itemctrl000009_Databound" BorderStyle="Solid" BorderWidth="1" BorderColor="LightGray" />
</td>
</tr>
<tr>
<td  colspan="2" >
Licencia emitida
</td>
</tr>
<tr>
<td  colspan="2" >
<asp:Label  runat="server" ID="itemctrl000008" CssClass="form-control-read" width="100%" OnDataBinding="itemctrl000008_Databound" BorderStyle="Solid" BorderWidth="1" BorderColor="LightGray" />
</td>
</tr>
<tr>
<td  colspan="1" >
Activado
</td>
<td  colspan="1" >
<asp:Label  runat="server" ID="itemctrl000007" CssClass="form-control-read" width="100%" OnDataBinding="itemctrl000007_Databound" />
</td>
</tr>
<tr>
<td  colspan="2" >
Valor
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
 GridLines="None" ShowHeader="True" ShowFooter="True" Width="100%" UseAccessibleHeader="False" DataKeyNames="licdetcod" CellPadding="2" DataSourceID="itemdsctrl000004" onRowCreated="itemctrl000004_RowCreated" >
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
<asp:Image  runat="server" ID="rowctrl000004imageimg" Width="12px" GenerateEmptyAlternateText="True" ImageURL="imagenes/iconValores.png" BorderWidth="0" AlternateText="Imagen no disp." />
</ItemTemplate>
</asp:TemplateField>
<asp:TemplateField   AccessibleHeaderText="Nombre" HeaderText="Nombre" SortExpression="licdetdsc" ItemStyle-CssClass="tabla-celda" HeaderStyle-CssClass="tabla-titulo" FooterStyle-CssClass="tabla-celda">
<ItemStyle  HorizontalAlign="Left" CssClass="searchresult-table-cell" />
<ItemTemplate>
<asp:LinkButton runat="server" ID="itemctrl000004colitem1view" Text='<%# Eval("licdetdsc") %>' CssClass="form-control-read" OnDataBinding="itemctrl000004colitem1view_DataBinding" CausesValidation="False" />
</ItemTemplate>
</asp:TemplateField>
<asp:TemplateField   AccessibleHeaderText="Valor" HeaderText="Valor" SortExpression="licdetval" ItemStyle-CssClass="tabla-celda" HeaderStyle-CssClass="tabla-titulo" FooterStyle-CssClass="tabla-celda">
<ItemStyle  HorizontalAlign="Left" CssClass="searchresult-table-cell" />
<ItemTemplate>
<asp:Label  runat="server" ID="itemctrl000004colitem2" width="100%" OnDataBinding="itemctrl000004colitem2_Databound" /></ItemTemplate>
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
Código
</td>
<td  colspan="1" >
<asp:Label  runat="server" ID="editctrl000016" CssClass="form-control-read" width="100%" OnDataBinding="editctrl000016_Databound" />
</td>
</tr>
<tr>
<td  colspan="1" >
Descripcion
</td>
<td  colspan="1" >
<asp:TextBox  runat="server" ID="editctrl000015" CssClass="form-control" Width="400px" MaxLength="250" OnDataBinding="editctrl000015_DataBound_frmdatos" Tooltip="Campo100126[]" TextMode="MultiLine" onkeypress="return textCounter(this,250);" onkeydown="return textCounter(this,250);" onkeyup="return textCounter(this,250);" onfocus="return textCounter_GetFocus(this,250);" onblur="return textCounter_LostFocus(this);" />
<asp:CompareValidator  runat="server" ID="vcdvaleditctrl000015" SetFocusOnError="true" CssClass="error" ControltoValidate="editctrl000015" Display="Dynamic" Text="Ingrese un texto" ErrorMessage="Descripcion:Ingrese un texto" Type="String" Operator="DataTypeCheck" ValidationGroup="vgedit" />
<asp:RegularExpressionValidator  runat="server" ID="vrgvaleditctrl000015" SetFocusOnError="true" CssClass="error" Display="Dynamic" ControltoValidate="editctrl000015" ValidationExpression="[\w\s-+/*^()!|°$%&=-_,:;.ñÑ~áéíóúÁÉÍÓÚ#}{]{0,250}" Text="No mayor a 250 caracteres. Deben ser letras o numeros." ErrorMessage="Descripcion:No mayor a 250 caracteres. Deben ser letras o numeros." ValidationGroup="vgedit" />
<ajaxkit:ValidatorCalloutExtender  runat="server" ID="ajaxvrgeditctrl000015" TargetControlID="vrgvaleditctrl000015" />

</td>
</tr>
<tr>
<td  colspan="1" >

</td>
<td  colspan="1" >
<asp:SqlDataSource  runat="server" ID="dseditctrl000014" CancelSelectOnNullParameter="False" onInit="dseditctrl000014_Init" SelectCommandType="Text"
 SelectCommand="SELECT Q_KYA.kyadsc,Q_KYA.kyacod FROM Q_KYA   ORDER BY Q_KYA.kyadsc" onselected="dseditctrl000014_Selected" >
<SelectParameters>
</SelectParameters>
</asp:SqlDataSource>

</td>
</tr>
<tr>
<td  colspan="1" >
Clave autorizante
</td>
<td  colspan="1" >
<asp:UpdatePanel  ID="updupdeditctrl000014fs" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="True" >
<ContentTemplate>
<asp:DropDownList  runat="server" ID="editctrl000014" CssClass="form-control" DataSourceID="dseditctrl000014" AppendDataBoundItems="False" DataTextField="kyadsc" DataValueField="kyacod" Enabled="True" onDataBound="editctrl000014_DataBound" >
</asp:DropDownList>
<asp:LinkButton runat="server" ID="editctrl000014new" Text="Nuevo..." CssClass="boton-acciones-subbutton" CausesValidation="False" OnClientClick='<%# "javascript:if (window.showModalDialog) {window.showModalDialog(" & chr(39) & "hrckya_det.aspx?_mode_=1&_closea_=1&isDlg=1&timestamp=" & chr(39) & " + new Date().getTime() + " & chr(39) & "" & chr(39) & ", " & chr(39) & "" & chr(39) & ", " & chr(39) & "menubar=no;resizable=yes;toolbar=no;status=no;center=yes;dialogWidth=750px;dialogHeight=570px;" & chr(39) & ");} else {window.open(" & chr(39) & "hrckya_det.aspx?_mode_=1&_closea_=1&isDlg=1&timestamp=" & chr(39) & " + new Date().getTime() + " & chr(39) & "" & chr(39) & ", " & chr(39) & "" & chr(39) & ", " & chr(39) & "modal=yes;directories=no;menubar=no;resizable=yes;toolbar=no;status=no;center=yes;dialogWidth=750px;dialogHeight=570px;" & chr(39) & ");};return false;" %>' PostBackUrl='<%# "javascript:if (window.showModalDialog) {window.showModalDialog(" & chr(39) & "hrckya_det.aspx?_mode_=1&_closea_=1&isDlg=1&timestamp=" & chr(39) & " + new Date().getTime() + " & chr(39) & "" & chr(39) & ", " & chr(39) & "" & chr(39) & ", " & chr(39) & "menubar=no;resizable=yes;toolbar=no;status=no;center=yes;dialogWidth=750px;dialogHeight=570px;" & chr(39) & ");} else {window.open(" & chr(39) & "hrckya_det.aspx?_mode_=1&_closea_=1&isDlg=1&timestamp=" & chr(39) & " + new Date().getTime() + " & chr(39) & "" & chr(39) & ", " & chr(39) & "" & chr(39) & ", " & chr(39) & "modal=yes;directories=no;menubar=no;resizable=yes;toolbar=no;status=no;center=yes;dialogWidth=750px;dialogHeight=570px;" & chr(39) & ");};return false;" %>' />
<asp:LinkButton runat="server" ID="editctrl000014_comborefresh" Text="Refrescar" CssClass="boton-acciones-subbutton" OnClick="editctrl000014_comborefresh_Click" CausesValidation="False" />
<asp:LinkButton runat="server" ID="editctrl000014view" Text='<%# Eval("lickyaautcoddsc") %>' CssClass="boton-acciones-subbutton" OnDataBinding="editctrl000014view_DataBinding" CausesValidation="False" OnClientClick='<%# "javascript:if (window.showModalDialog) {window.showModalDialog(" & chr(39) & "hrckya_det.aspx?_mode_=0&_closea_=1&isDlg=1&param1=" & Eval("lickyaautcod") & "&timestamp=" & chr(39) & " + new Date().getTime() + " & chr(39) & "" & chr(39) & ", " & chr(39) & "" & chr(39) & ", " & chr(39) & "menubar=no;resizable=yes;toolbar=no;status=no;center=yes;dialogWidth=750px;dialogHeight=570px;" & chr(39) & ");} else {window.open(" & chr(39) & "hrckya_det.aspx?_mode_=0&_closea_=1&isDlg=1&param1=" & Eval("lickyaautcod") & "&timestamp=" & chr(39) & " + new Date().getTime() + " & chr(39) & "" & chr(39) & ", " & chr(39) & "" & chr(39) & ", " & chr(39) & "modal=yes;directories=no;menubar=no;resizable=yes;toolbar=no;status=no;center=yes;dialogWidth=750px;dialogHeight=570px;" & chr(39) & ");};return false;" %>' PostBackUrl='<%# "javascript:if (window.showModalDialog) {window.showModalDialog(" & chr(39) & "hrckya_det.aspx?_mode_=0&_closea_=1&isDlg=1&param1=" & Eval("lickyaautcod") & "&timestamp=" & chr(39) & " + new Date().getTime() + " & chr(39) & "" & chr(39) & ", " & chr(39) & "" & chr(39) & ", " & chr(39) & "menubar=no;resizable=yes;toolbar=no;status=no;center=yes;dialogWidth=750px;dialogHeight=570px;" & chr(39) & ");} else {window.open(" & chr(39) & "hrckya_det.aspx?_mode_=0&_closea_=1&isDlg=1&param1=" & Eval("lickyaautcod") & "&timestamp=" & chr(39) & " + new Date().getTime() + " & chr(39) & "" & chr(39) & ", " & chr(39) & "" & chr(39) & ", " & chr(39) & "modal=yes;directories=no;menubar=no;resizable=yes;toolbar=no;status=no;center=yes;dialogWidth=750px;dialogHeight=570px;" & chr(39) & ");};return false;" %>' />
</ContentTemplate>
</asp:UpdatePanel>
<asp:UpdateProgress  runat="server" AssociatedUpdatePanelID="updupdeditctrl000014fs" >
<ProgressTemplate><span  class="ajaxupd" ><div style="position:absolute;top:2px;left:50%; border-color:#333333; border:1; border-style:solid; background-color:#FFFFFF;padding:3px;"><image src="imagenes/ajaxupdate.gif" border=0 />Actualizando</div></span></ProgressTemplate>
</asp:UpdateProgress>

</td>
</tr>
<tr>
<td  colspan="1" >

</td>
<td  colspan="1" >
<asp:SqlDataSource  runat="server" ID="dseditctrl000013" CancelSelectOnNullParameter="False" onInit="dseditctrl000013_Init" SelectCommandType="Text"
 SelectCommand="SELECT Q_KYA.kyadsc,Q_KYA.kyacod FROM Q_KYA   ORDER BY Q_KYA.kyadsc" onselected="dseditctrl000013_Selected" >
<SelectParameters>
</SelectParameters>
</asp:SqlDataSource>

</td>
</tr>
<tr>
<td  colspan="1" >
Clave emisora
</td>
<td  colspan="1" >
<asp:UpdatePanel  ID="updupdeditctrl000013fs" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="True" >
<ContentTemplate>
<asp:DropDownList  runat="server" ID="editctrl000013" CssClass="form-control" DataSourceID="dseditctrl000013" AppendDataBoundItems="False" DataTextField="kyadsc" DataValueField="kyacod" Enabled="True" onDataBound="editctrl000013_DataBound" >
</asp:DropDownList>
<asp:LinkButton runat="server" ID="editctrl000013new" Text="Nuevo..." CssClass="boton-acciones-subbutton" CausesValidation="False" OnClientClick='<%# "javascript:if (window.showModalDialog) {window.showModalDialog(" & chr(39) & "hrckya_det.aspx?_mode_=1&_closea_=1&isDlg=1&timestamp=" & chr(39) & " + new Date().getTime() + " & chr(39) & "" & chr(39) & ", " & chr(39) & "" & chr(39) & ", " & chr(39) & "menubar=no;resizable=yes;toolbar=no;status=no;center=yes;dialogWidth=750px;dialogHeight=570px;" & chr(39) & ");} else {window.open(" & chr(39) & "hrckya_det.aspx?_mode_=1&_closea_=1&isDlg=1&timestamp=" & chr(39) & " + new Date().getTime() + " & chr(39) & "" & chr(39) & ", " & chr(39) & "" & chr(39) & ", " & chr(39) & "modal=yes;directories=no;menubar=no;resizable=yes;toolbar=no;status=no;center=yes;dialogWidth=750px;dialogHeight=570px;" & chr(39) & ");};return false;" %>' PostBackUrl='<%# "javascript:if (window.showModalDialog) {window.showModalDialog(" & chr(39) & "hrckya_det.aspx?_mode_=1&_closea_=1&isDlg=1&timestamp=" & chr(39) & " + new Date().getTime() + " & chr(39) & "" & chr(39) & ", " & chr(39) & "" & chr(39) & ", " & chr(39) & "menubar=no;resizable=yes;toolbar=no;status=no;center=yes;dialogWidth=750px;dialogHeight=570px;" & chr(39) & ");} else {window.open(" & chr(39) & "hrckya_det.aspx?_mode_=1&_closea_=1&isDlg=1&timestamp=" & chr(39) & " + new Date().getTime() + " & chr(39) & "" & chr(39) & ", " & chr(39) & "" & chr(39) & ", " & chr(39) & "modal=yes;directories=no;menubar=no;resizable=yes;toolbar=no;status=no;center=yes;dialogWidth=750px;dialogHeight=570px;" & chr(39) & ");};return false;" %>' />
<asp:LinkButton runat="server" ID="editctrl000013_comborefresh" Text="Refrescar" CssClass="boton-acciones-subbutton" OnClick="editctrl000013_comborefresh_Click" CausesValidation="False" />
<asp:LinkButton runat="server" ID="editctrl000013view" Text='<%# Eval("lickyacoddsc") %>' CssClass="boton-acciones-subbutton" OnDataBinding="editctrl000013view_DataBinding" CausesValidation="False" OnClientClick='<%# "javascript:if (window.showModalDialog) {window.showModalDialog(" & chr(39) & "hrckya_det.aspx?_mode_=0&_closea_=1&isDlg=1&param1=" & Eval("lickyacod") & "&timestamp=" & chr(39) & " + new Date().getTime() + " & chr(39) & "" & chr(39) & ", " & chr(39) & "" & chr(39) & ", " & chr(39) & "menubar=no;resizable=yes;toolbar=no;status=no;center=yes;dialogWidth=750px;dialogHeight=570px;" & chr(39) & ");} else {window.open(" & chr(39) & "hrckya_det.aspx?_mode_=0&_closea_=1&isDlg=1&param1=" & Eval("lickyacod") & "&timestamp=" & chr(39) & " + new Date().getTime() + " & chr(39) & "" & chr(39) & ", " & chr(39) & "" & chr(39) & ", " & chr(39) & "modal=yes;directories=no;menubar=no;resizable=yes;toolbar=no;status=no;center=yes;dialogWidth=750px;dialogHeight=570px;" & chr(39) & ");};return false;" %>' PostBackUrl='<%# "javascript:if (window.showModalDialog) {window.showModalDialog(" & chr(39) & "hrckya_det.aspx?_mode_=0&_closea_=1&isDlg=1&param1=" & Eval("lickyacod") & "&timestamp=" & chr(39) & " + new Date().getTime() + " & chr(39) & "" & chr(39) & ", " & chr(39) & "" & chr(39) & ", " & chr(39) & "menubar=no;resizable=yes;toolbar=no;status=no;center=yes;dialogWidth=750px;dialogHeight=570px;" & chr(39) & ");} else {window.open(" & chr(39) & "hrckya_det.aspx?_mode_=0&_closea_=1&isDlg=1&param1=" & Eval("lickyacod") & "&timestamp=" & chr(39) & " + new Date().getTime() + " & chr(39) & "" & chr(39) & ", " & chr(39) & "" & chr(39) & ", " & chr(39) & "modal=yes;directories=no;menubar=no;resizable=yes;toolbar=no;status=no;center=yes;dialogWidth=750px;dialogHeight=570px;" & chr(39) & ");};return false;" %>' />
</ContentTemplate>
</asp:UpdatePanel>
<asp:UpdateProgress  runat="server" AssociatedUpdatePanelID="updupdeditctrl000013fs" >
<ProgressTemplate><span  class="ajaxupd" ><div style="position:absolute;top:2px;left:50%; border-color:#333333; border:1; border-style:solid; background-color:#FFFFFF;padding:3px;"><image src="imagenes/ajaxupdate.gif" border=0 />Actualizando</div></span></ProgressTemplate>
</asp:UpdateProgress>

</td>
</tr>
<tr>
<td  colspan="1" >
ID de solicitud
</td>
<td  colspan="1" >
<asp:TextBox  runat="server" ID="editctrl000012" CssClass="form-control" Width="400px" MaxLength="200" OnDataBinding="editctrl000012_DataBound_frmdatos" Tooltip="Campo100129[]" TextMode="MultiLine" onkeypress="return textCounter(this,200);" onkeydown="return textCounter(this,200);" onkeyup="return textCounter(this,200);" onfocus="return textCounter_GetFocus(this,200);" onblur="return textCounter_LostFocus(this);" />
<asp:CompareValidator  runat="server" ID="vcdvaleditctrl000012" SetFocusOnError="true" CssClass="error" ControltoValidate="editctrl000012" Display="Dynamic" Text="Ingrese un texto" ErrorMessage="ID de solicitud:Ingrese un texto" Type="String" Operator="DataTypeCheck" ValidationGroup="vgedit" />
<asp:RegularExpressionValidator  runat="server" ID="vrgvaleditctrl000012" SetFocusOnError="true" CssClass="error" Display="Dynamic" ControltoValidate="editctrl000012" ValidationExpression="[\w\s-+/*^()!|°$%&=-_,:;.ñÑ~áéíóúÁÉÍÓÚ#}{]{0,200}" Text="No mayor a 200 caracteres. Deben ser letras o numeros." ErrorMessage="ID de solicitud:No mayor a 200 caracteres. Deben ser letras o numeros." ValidationGroup="vgedit" />
<ajaxkit:ValidatorCalloutExtender  runat="server" ID="ajaxvrgeditctrl000012" TargetControlID="vrgvaleditctrl000012" />

</td>
</tr>
<tr>
<td  colspan="1" >
Usuario de conexión
</td>
<td  colspan="1" >
<asp:TextBox  runat="server" ID="editctrl000011" CssClass="form-control" Width="400px" MaxLength="200" OnDataBinding="editctrl000011_DataBound_frmdatos" Tooltip="Campo100130[]" TextMode="MultiLine" onkeypress="return textCounter(this,200);" onkeydown="return textCounter(this,200);" onkeyup="return textCounter(this,200);" onfocus="return textCounter_GetFocus(this,200);" onblur="return textCounter_LostFocus(this);" />
<asp:CompareValidator  runat="server" ID="vcdvaleditctrl000011" SetFocusOnError="true" CssClass="error" ControltoValidate="editctrl000011" Display="Dynamic" Text="Ingrese un texto" ErrorMessage="Usuario de conexión:Ingrese un texto" Type="String" Operator="DataTypeCheck" ValidationGroup="vgedit" />
<asp:RegularExpressionValidator  runat="server" ID="vrgvaleditctrl000011" SetFocusOnError="true" CssClass="error" Display="Dynamic" ControltoValidate="editctrl000011" ValidationExpression="[\w\s-+/*^()!|°$%&=-_,:;.ñÑ~áéíóúÁÉÍÓÚ#}{]{0,200}" Text="No mayor a 200 caracteres. Deben ser letras o numeros." ErrorMessage="Usuario de conexión:No mayor a 200 caracteres. Deben ser letras o numeros." ValidationGroup="vgedit" />
<ajaxkit:ValidatorCalloutExtender  runat="server" ID="ajaxvrgeditctrl000011" TargetControlID="vrgvaleditctrl000011" />

</td>
</tr>
<tr>
<td  colspan="1" >
Password de conexión
</td>
<td  colspan="1" >
<asp:TextBox  runat="server" ID="editctrl000010" CssClass="obs-controles" Width="400px" MaxLength="200" OnDataBinding="editctrl000010_DataBound_frmdatos" Tooltip="Campo100131[]" onTextChanged="editctrl000010_TextChanged" />
<br />
Confirmación de contraseña
<asp:TextBox  runat="server" ID="editctrl000010pwdconfirm" CssClass="obs-controles" Width="400px" MaxLength="200" OnDataBinding="editctrl000010_DataBound_frmdatos" Tooltip="Campo100131[]" onTextChanged="editctrl000010_TextChanged" />
<asp:CompareValidator  runat="server" ID="editctrl000010pwdconfirmval" CssClass="error" ControlToCompare="editctrl000010pwdconfirm" ControlToValidate="editctrl000010" Operator="Equal" Text="Confirme la contraseña" />

<asp:CompareValidator  runat="server" ID="vcdvaleditctrl000010" SetFocusOnError="true" CssClass="error" ControltoValidate="editctrl000010" Display="Dynamic" Text="Ingrese un texto" ErrorMessage="Password de conexión:Ingrese un texto" Type="String" Operator="DataTypeCheck" ValidationGroup="vgedit" />
<asp:RegularExpressionValidator  runat="server" ID="vrgvaleditctrl000010" SetFocusOnError="true" CssClass="error" Display="Dynamic" ControltoValidate="editctrl000010" ValidationExpression="[\w\s-+/*^()!|°$%&=-_,:;.ñÑ~áéíóúÁÉÍÓÚ#}{]{0,200}" Text="No mayor a 200 caracteres. Deben ser letras o numeros." ErrorMessage="Password de conexión:No mayor a 200 caracteres. Deben ser letras o numeros." ValidationGroup="vgedit" />
<ajaxkit:ValidatorCalloutExtender  runat="server" ID="ajaxvrgeditctrl000010" TargetControlID="vrgvaleditctrl000010" />

</td>
</tr>
<tr>
<td  colspan="2" >
Solicitud
</td>
</tr>
<tr>
<td  colspan="2" >
<asp:TextBox  runat="server" ID="editctrl000009" TextMode="MultiLine" OnDataBinding="editctrl000009_DataBound_frmdatos" style="width:800px;height:120px;" />
<asp:CompareValidator  runat="server" ID="vcdvaleditctrl000009" SetFocusOnError="true" CssClass="error" ControltoValidate="editctrl000009" Display="Dynamic" Text="Ingrese un texto" ErrorMessage="Solicitud:Ingrese un texto" Type="String" Operator="DataTypeCheck" ValidationGroup="vgedit" />


</td>
</tr>
<tr>
<td  colspan="2" >
Licencia emitida
</td>
</tr>
<tr>
<td  colspan="2" >
<asp:TextBox  runat="server" ID="editctrl000008" TextMode="MultiLine" OnDataBinding="editctrl000008_DataBound_frmdatos" style="width:800px;height:120px;" />
<asp:CompareValidator  runat="server" ID="vcdvaleditctrl000008" SetFocusOnError="true" CssClass="error" ControltoValidate="editctrl000008" Display="Dynamic" Text="Ingrese un texto" ErrorMessage="Licencia emitida:Ingrese un texto" Type="String" Operator="DataTypeCheck" ValidationGroup="vgedit" />


</td>
</tr>
<tr>
<td  colspan="1" >
Activado
</td>
<td  colspan="1" >
<asp:Checkbox  runat="server" ID="editctrl000007" CssClass="form-control" OnDataBinding="editctrl000007_Databound" />
</td>
</tr>
<tr>
<td  colspan="2" >
Valor
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
 GridLines="None" ShowHeader="True" ShowFooter="True" Width="100%" UseAccessibleHeader="False" DataKeyNames="licdetcod" CellPadding="2" DataSourceID="editdsctrl000004" onRowCreated="editctrl000004_RowCreated" onRowCommand="editctrl000004_RowCommand" >
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
<asp:Image  runat="server" ID="rowctrl000004imageimg" Width="12px" GenerateEmptyAlternateText="True" ImageURL="imagenes/iconValores.png" BorderWidth="0" AlternateText="Imagen no disp." />
</ItemTemplate>
</asp:TemplateField>
<asp:TemplateField   AccessibleHeaderText="Nombre" HeaderText="Nombre" SortExpression="licdetdsc" ItemStyle-CssClass="tabla-celda" HeaderStyle-CssClass="tabla-titulo" FooterStyle-CssClass="tabla-celda">
<ItemStyle  HorizontalAlign="Left" CssClass="searchresult-table-cell" />
<ItemTemplate>
<asp:LinkButton runat="server" ID="editctrl000004colitem1view" Text='<%# Eval("licdetdsc") %>' CssClass="form-control-read" OnDataBinding="editctrl000004colitem1view_DataBinding" CausesValidation="False" />
</ItemTemplate>
</asp:TemplateField>
<asp:TemplateField   AccessibleHeaderText="Valor" HeaderText="Valor" SortExpression="licdetval" ItemStyle-CssClass="tabla-celda" HeaderStyle-CssClass="tabla-titulo" FooterStyle-CssClass="tabla-celda">
<ItemStyle  HorizontalAlign="Left" CssClass="searchresult-table-cell" />
<ItemTemplate>
<asp:Label  runat="server" ID="editctrl000004colitem2" width="100%" OnDataBinding="editctrl000004colitem2_Databound" /></ItemTemplate>
</asp:TemplateField>
<asp:TemplateField   AccessibleHeaderText="Acciones" HeaderText="Acciones">
<ItemStyle  HorizontalAlign="Center" Width="3%" />
<ItemTemplate>
 <asp:ImageButton  ID="imge" runat="server" CausesValidation="False" CommandName="Edit" CommandArgument='<%# Eval("licdetcod") %>' ImageUrl="./imagenes/actmod.png" Width="16" Text="Editar" />
 <asp:ImageButton  ID="imgd" runat="server" CausesValidation="False" CommandName="Delete" CommandArgument='<%# Eval("licdetcod") %>' ImageUrl="./imagenes/actdel.png" Text="Borrar" Width="16" OnClientClick=" return confirm('Confirma la eliminación?');" />
</ItemTemplate>
<FooterTemplate>
<asp:Button  ID="cmdi" runat="server" CausesValidation="False" CommandName="HRC_INSERT" CssClass="boton-acciones" Text="Agregar" />
</FooterTemplate>
</asp:TemplateField>
<asp:TemplateField   ItemStyle-CssClass="tabla-celda" HeaderStyle-CssClass="tabla-titulo" FooterStyle-CssClass="tabla-celda">
<ItemStyle  HorizontalAlign="Left" Width="16" />
<ItemTemplate>
<asp:ImageButton runat="server" ID="cmdVer" ImageURL="./imagenes/actview.png" Width="16" Height="16" CommandName="Select" CausesValidation="False" CommandArgument='<%# Eval("licdetcod") %>' /></ItemTemplate>
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
Descripcion
</td>
<td  colspan="1" >
<asp:TextBox  runat="server" ID="insctrl000015" CssClass="form-control" Width="400px" MaxLength="250" Tooltip="Campo100126[]" TextMode="MultiLine" onkeypress="return textCounter(this,250);" onkeydown="return textCounter(this,250);" onkeyup="return textCounter(this,250);" onfocus="return textCounter_GetFocus(this,250);" onblur="return textCounter_LostFocus(this);" />
<asp:CompareValidator  runat="server" ID="vcdvalinsctrl000015" SetFocusOnError="true" CssClass="error" ControltoValidate="insctrl000015" Display="Dynamic" Text="Ingrese un texto" ErrorMessage="Descripcion:Ingrese un texto" Type="String" Operator="DataTypeCheck" ValidationGroup="vgins" />
<asp:RegularExpressionValidator  runat="server" ID="vrgvalinsctrl000015" SetFocusOnError="true" CssClass="error" Display="Dynamic" ControltoValidate="insctrl000015" ValidationExpression="[\w\s-+/*^()!|°$%&=-_,:;.ñÑ~áéíóúÁÉÍÓÚ#}{]{0,250}" Text="No mayor a 250 caracteres. Deben ser letras o numeros." ErrorMessage="Descripcion:No mayor a 250 caracteres. Deben ser letras o numeros." ValidationGroup="vgins" />
<ajaxkit:ValidatorCalloutExtender  runat="server" ID="ajaxvrginsctrl000015" TargetControlID="vrgvalinsctrl000015" />

</td>
</tr>
<tr>
<td  colspan="1" >

</td>
<td  colspan="1" >
<asp:SqlDataSource  runat="server" ID="dsinsctrl000014" CancelSelectOnNullParameter="False" onInit="dsinsctrl000014_Init" SelectCommandType="Text"
 SelectCommand="SELECT Q_KYA.kyadsc,Q_KYA.kyacod FROM Q_KYA   ORDER BY Q_KYA.kyadsc" onselected="dsinsctrl000014_Selected" >
<SelectParameters>
</SelectParameters>
</asp:SqlDataSource>

</td>
</tr>
<tr>
<td  colspan="1" >
Clave autorizante
</td>
<td  colspan="1" >
<asp:UpdatePanel  ID="updupdinsctrl000014fs" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="True" >
<ContentTemplate>
<asp:DropDownList  runat="server" ID="insctrl000014" CssClass="form-control" DataSourceID="dsinsctrl000014" AppendDataBoundItems="False" DataTextField="kyadsc" DataValueField="kyacod" Enabled="True" >
<asp:ListItem value="-1">Todos</asp:ListItem>
</asp:DropDownList>
<asp:LinkButton runat="server" ID="insctrl000014new" Text="Nuevo..." CssClass="boton-acciones-subbutton" CausesValidation="False" OnClientClick='<%# "javascript:if (window.showModalDialog) {window.showModalDialog(" & chr(39) & "hrckya_det.aspx?_mode_=1&_closea_=1&isDlg=1&timestamp=" & chr(39) & " + new Date().getTime() + " & chr(39) & "" & chr(39) & ", " & chr(39) & "" & chr(39) & ", " & chr(39) & "menubar=no;resizable=yes;toolbar=no;status=no;center=yes;dialogWidth=750px;dialogHeight=570px;" & chr(39) & ");} else {window.open(" & chr(39) & "hrckya_det.aspx?_mode_=1&_closea_=1&isDlg=1&timestamp=" & chr(39) & " + new Date().getTime() + " & chr(39) & "" & chr(39) & ", " & chr(39) & "" & chr(39) & ", " & chr(39) & "modal=yes;directories=no;menubar=no;resizable=yes;toolbar=no;status=no;center=yes;dialogWidth=750px;dialogHeight=570px;" & chr(39) & ");};return false;" %>' PostBackUrl='<%# "javascript:if (window.showModalDialog) {window.showModalDialog(" & chr(39) & "hrckya_det.aspx?_mode_=1&_closea_=1&isDlg=1&timestamp=" & chr(39) & " + new Date().getTime() + " & chr(39) & "" & chr(39) & ", " & chr(39) & "" & chr(39) & ", " & chr(39) & "menubar=no;resizable=yes;toolbar=no;status=no;center=yes;dialogWidth=750px;dialogHeight=570px;" & chr(39) & ");} else {window.open(" & chr(39) & "hrckya_det.aspx?_mode_=1&_closea_=1&isDlg=1&timestamp=" & chr(39) & " + new Date().getTime() + " & chr(39) & "" & chr(39) & ", " & chr(39) & "" & chr(39) & ", " & chr(39) & "modal=yes;directories=no;menubar=no;resizable=yes;toolbar=no;status=no;center=yes;dialogWidth=750px;dialogHeight=570px;" & chr(39) & ");};return false;" %>' />
<asp:LinkButton runat="server" ID="insctrl000014_comborefresh" Text="Refrescar" CssClass="boton-acciones-subbutton" OnClick="insctrl000014_comborefresh_Click" CausesValidation="False" />
</ContentTemplate>
</asp:UpdatePanel>
<asp:UpdateProgress  runat="server" AssociatedUpdatePanelID="updupdinsctrl000014fs" >
<ProgressTemplate><span  class="ajaxupd" ><div style="position:absolute;top:2px;left:50%; border-color:#333333; border:1; border-style:solid; background-color:#FFFFFF;padding:3px;"><image src="imagenes/ajaxupdate.gif" border=0 />Actualizando</div></span></ProgressTemplate>
</asp:UpdateProgress>

</td>
</tr>
<tr>
<td  colspan="1" >

</td>
<td  colspan="1" >
<asp:SqlDataSource  runat="server" ID="dsinsctrl000013" CancelSelectOnNullParameter="False" onInit="dsinsctrl000013_Init" SelectCommandType="Text"
 SelectCommand="SELECT Q_KYA.kyadsc,Q_KYA.kyacod FROM Q_KYA   ORDER BY Q_KYA.kyadsc" onselected="dsinsctrl000013_Selected" >
<SelectParameters>
</SelectParameters>
</asp:SqlDataSource>

</td>
</tr>
<tr>
<td  colspan="1" >
Clave emisora
</td>
<td  colspan="1" >
<asp:UpdatePanel  ID="updupdinsctrl000013fs" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="True" >
<ContentTemplate>
<asp:DropDownList  runat="server" ID="insctrl000013" CssClass="form-control" DataSourceID="dsinsctrl000013" AppendDataBoundItems="False" DataTextField="kyadsc" DataValueField="kyacod" Enabled="True" >
<asp:ListItem value="-1">Todos</asp:ListItem>
</asp:DropDownList>
<asp:LinkButton runat="server" ID="insctrl000013new" Text="Nuevo..." CssClass="boton-acciones-subbutton" CausesValidation="False" OnClientClick='<%# "javascript:if (window.showModalDialog) {window.showModalDialog(" & chr(39) & "hrckya_det.aspx?_mode_=1&_closea_=1&isDlg=1&timestamp=" & chr(39) & " + new Date().getTime() + " & chr(39) & "" & chr(39) & ", " & chr(39) & "" & chr(39) & ", " & chr(39) & "menubar=no;resizable=yes;toolbar=no;status=no;center=yes;dialogWidth=750px;dialogHeight=570px;" & chr(39) & ");} else {window.open(" & chr(39) & "hrckya_det.aspx?_mode_=1&_closea_=1&isDlg=1&timestamp=" & chr(39) & " + new Date().getTime() + " & chr(39) & "" & chr(39) & ", " & chr(39) & "" & chr(39) & ", " & chr(39) & "modal=yes;directories=no;menubar=no;resizable=yes;toolbar=no;status=no;center=yes;dialogWidth=750px;dialogHeight=570px;" & chr(39) & ");};return false;" %>' PostBackUrl='<%# "javascript:if (window.showModalDialog) {window.showModalDialog(" & chr(39) & "hrckya_det.aspx?_mode_=1&_closea_=1&isDlg=1&timestamp=" & chr(39) & " + new Date().getTime() + " & chr(39) & "" & chr(39) & ", " & chr(39) & "" & chr(39) & ", " & chr(39) & "menubar=no;resizable=yes;toolbar=no;status=no;center=yes;dialogWidth=750px;dialogHeight=570px;" & chr(39) & ");} else {window.open(" & chr(39) & "hrckya_det.aspx?_mode_=1&_closea_=1&isDlg=1&timestamp=" & chr(39) & " + new Date().getTime() + " & chr(39) & "" & chr(39) & ", " & chr(39) & "" & chr(39) & ", " & chr(39) & "modal=yes;directories=no;menubar=no;resizable=yes;toolbar=no;status=no;center=yes;dialogWidth=750px;dialogHeight=570px;" & chr(39) & ");};return false;" %>' />
<asp:LinkButton runat="server" ID="insctrl000013_comborefresh" Text="Refrescar" CssClass="boton-acciones-subbutton" OnClick="insctrl000013_comborefresh_Click" CausesValidation="False" />
</ContentTemplate>
</asp:UpdatePanel>
<asp:UpdateProgress  runat="server" AssociatedUpdatePanelID="updupdinsctrl000013fs" >
<ProgressTemplate><span  class="ajaxupd" ><div style="position:absolute;top:2px;left:50%; border-color:#333333; border:1; border-style:solid; background-color:#FFFFFF;padding:3px;"><image src="imagenes/ajaxupdate.gif" border=0 />Actualizando</div></span></ProgressTemplate>
</asp:UpdateProgress>

</td>
</tr>
<tr>
<td  colspan="1" >
ID de solicitud
</td>
<td  colspan="1" >
<asp:TextBox  runat="server" ID="insctrl000012" CssClass="form-control" Width="400px" MaxLength="200" Tooltip="Campo100129[]" TextMode="MultiLine" onkeypress="return textCounter(this,200);" onkeydown="return textCounter(this,200);" onkeyup="return textCounter(this,200);" onfocus="return textCounter_GetFocus(this,200);" onblur="return textCounter_LostFocus(this);" />
<asp:CompareValidator  runat="server" ID="vcdvalinsctrl000012" SetFocusOnError="true" CssClass="error" ControltoValidate="insctrl000012" Display="Dynamic" Text="Ingrese un texto" ErrorMessage="ID de solicitud:Ingrese un texto" Type="String" Operator="DataTypeCheck" ValidationGroup="vgins" />
<asp:RegularExpressionValidator  runat="server" ID="vrgvalinsctrl000012" SetFocusOnError="true" CssClass="error" Display="Dynamic" ControltoValidate="insctrl000012" ValidationExpression="[\w\s-+/*^()!|°$%&=-_,:;.ñÑ~áéíóúÁÉÍÓÚ#}{]{0,200}" Text="No mayor a 200 caracteres. Deben ser letras o numeros." ErrorMessage="ID de solicitud:No mayor a 200 caracteres. Deben ser letras o numeros." ValidationGroup="vgins" />
<ajaxkit:ValidatorCalloutExtender  runat="server" ID="ajaxvrginsctrl000012" TargetControlID="vrgvalinsctrl000012" />

</td>
</tr>
<tr>
<td  colspan="1" >
Usuario de conexión
</td>
<td  colspan="1" >
<asp:TextBox  runat="server" ID="insctrl000011" CssClass="form-control" Width="400px" MaxLength="200" Tooltip="Campo100130[]" TextMode="MultiLine" onkeypress="return textCounter(this,200);" onkeydown="return textCounter(this,200);" onkeyup="return textCounter(this,200);" onfocus="return textCounter_GetFocus(this,200);" onblur="return textCounter_LostFocus(this);" />
<asp:CompareValidator  runat="server" ID="vcdvalinsctrl000011" SetFocusOnError="true" CssClass="error" ControltoValidate="insctrl000011" Display="Dynamic" Text="Ingrese un texto" ErrorMessage="Usuario de conexión:Ingrese un texto" Type="String" Operator="DataTypeCheck" ValidationGroup="vgins" />
<asp:RegularExpressionValidator  runat="server" ID="vrgvalinsctrl000011" SetFocusOnError="true" CssClass="error" Display="Dynamic" ControltoValidate="insctrl000011" ValidationExpression="[\w\s-+/*^()!|°$%&=-_,:;.ñÑ~áéíóúÁÉÍÓÚ#}{]{0,200}" Text="No mayor a 200 caracteres. Deben ser letras o numeros." ErrorMessage="Usuario de conexión:No mayor a 200 caracteres. Deben ser letras o numeros." ValidationGroup="vgins" />
<ajaxkit:ValidatorCalloutExtender  runat="server" ID="ajaxvrginsctrl000011" TargetControlID="vrgvalinsctrl000011" />

</td>
</tr>
<tr>
<td  colspan="1" >
Password de conexión
</td>
<td  colspan="1" >
<asp:TextBox  runat="server" ID="insctrl000010" CssClass="obs-controles" Width="400px" MaxLength="200" Tooltip="Campo100131[]" onTextChanged="insctrl000010_TextChanged" />
<br />
Confirmación de contraseña
<asp:TextBox  runat="server" ID="insctrl000010pwdconfirm" CssClass="obs-controles" Width="400px" MaxLength="200" Tooltip="Campo100131[]" onTextChanged="insctrl000010_TextChanged" />
<asp:CompareValidator  runat="server" ID="insctrl000010pwdconfirmval" CssClass="error" ControlToCompare="insctrl000010pwdconfirm" ControlToValidate="insctrl000010" Operator="Equal" Text="Confirme la contraseña" />

<asp:CompareValidator  runat="server" ID="vcdvalinsctrl000010" SetFocusOnError="true" CssClass="error" ControltoValidate="insctrl000010" Display="Dynamic" Text="Ingrese un texto" ErrorMessage="Password de conexión:Ingrese un texto" Type="String" Operator="DataTypeCheck" ValidationGroup="vgins" />
<asp:RegularExpressionValidator  runat="server" ID="vrgvalinsctrl000010" SetFocusOnError="true" CssClass="error" Display="Dynamic" ControltoValidate="insctrl000010" ValidationExpression="[\w\s-+/*^()!|°$%&=-_,:;.ñÑ~áéíóúÁÉÍÓÚ#}{]{0,200}" Text="No mayor a 200 caracteres. Deben ser letras o numeros." ErrorMessage="Password de conexión:No mayor a 200 caracteres. Deben ser letras o numeros." ValidationGroup="vgins" />
<ajaxkit:ValidatorCalloutExtender  runat="server" ID="ajaxvrginsctrl000010" TargetControlID="vrgvalinsctrl000010" />

</td>
</tr>
<tr>
<td  colspan="2" >
Solicitud
</td>
</tr>
<tr>
<td  colspan="2" >
<asp:TextBox  runat="server" ID="insctrl000009" TextMode="MultiLine" style="width:800px;height:120px;" />
<asp:CompareValidator  runat="server" ID="vcdvalinsctrl000009" SetFocusOnError="true" CssClass="error" ControltoValidate="insctrl000009" Display="Dynamic" Text="Ingrese un texto" ErrorMessage="Solicitud:Ingrese un texto" Type="String" Operator="DataTypeCheck" ValidationGroup="vgins" />


</td>
</tr>
<tr>
<td  colspan="2" >
Licencia emitida
</td>
</tr>
<tr>
<td  colspan="2" >
<asp:TextBox  runat="server" ID="insctrl000008" TextMode="MultiLine" style="width:800px;height:120px;" />
<asp:CompareValidator  runat="server" ID="vcdvalinsctrl000008" SetFocusOnError="true" CssClass="error" ControltoValidate="insctrl000008" Display="Dynamic" Text="Ingrese un texto" ErrorMessage="Licencia emitida:Ingrese un texto" Type="String" Operator="DataTypeCheck" ValidationGroup="vgins" />


</td>
</tr>
<tr>
<td  colspan="1" >
Activado
</td>
<td  colspan="1" >
<asp:Checkbox  runat="server" ID="insctrl000007" CssClass="form-control" />
</td>
</tr>
</table>
<!-- End Plantilla insercion --></InsertItemTemplate>

</asp:FormView>


						<asp:SqlDataSource  runat="server" ID="dsdatos" ProviderName="System.Data.SqlClient" CancelSelectOnNullParameter="False" onInit="dsdatos_Init" SelectCommandType="Text"
 SelectCommand="SELECT Q_LIC.liccod,Q_LIC.licdsc,(Q_LICLICKYAAUTCOD.kyadsc) as lickyaautcoddsc,(Q_LICLICKYACOD.kyadsc) as lickyacoddsc,Q_LIC.licrequestID,Q_LIC.licconnuser,Q_LIC.licconnpwd,Q_LIC.licrequest,Q_LIC.licresult,Q_LIC.licenabled,(Q_LICQSECSID.secdsc) as qsecsiddsc,Q_LIC.qsecdatetime,Q_LIC.lickyaautcod,Q_LIC.lickyacod,Q_LIC.qsecsid FROM Q_LIC  LEFT JOIN Q_KYA AS Q_LICLICKYAAUTCOD ON Q_LICLICKYAAUTCOD.kyacod=Q_LIC.lickyaautcod LEFT JOIN Q_KYA AS Q_LICLICKYACOD ON Q_LICLICKYACOD.kyacod=Q_LIC.lickyacod LEFT JOIN Q_SECPLOGIN AS Q_LICQSECSID ON Q_LICQSECSID.sidcod=Q_LIC.qsecsid  WHERE Q_LIC.liccod = @param1" onselected="dsdatos_Selected" UpdateCommandType="Text"
 UpdateCommand="UPDATE Q_LIC SET liccod=@liccod,licdsc=@licdsc,lickyaautcod=@lickyaautcod,lickyacod=@lickyacod,licrequestID=@licrequestID,licconnuser=@licconnuser,licconnpwd=@licconnpwd,licrequest=@licrequest,licresult=@licresult,licenabled=@licenabled,qsecsid=@qsecsid,qsecdatetime=getdate() WHERE Q_LIC.liccod = @param1" onupdated="dsdatos_Updated" InsertCommandType="Text"
 InsertCommand="DECLARE @querynextcodliccod int  SET @querynextcodliccod =(SELECT ISNULL(MAX(liccod),0)+1 FROM Q_LIC WHERE liccod > 0 ) INSERT INTO Q_LIC (liccod,licdsc,lickyaautcod,lickyacod,licrequestID,licconnuser,licconnpwd,licrequest,licresult,licenabled,qsecsid,qsecdatetime) VALUES(@querynextcodliccod,@licdsc,@lickyaautcod,@lickyacod,@licrequestID,@licconnuser,@licconnpwd,@licrequest,@licresult,@licenabled,@qsecsid,getdate()) SELECT @querynextcod =@querynextcodliccod" onInserted="dsdatos_Inserted" >
<InsertParameters>
<asp:Parameter  Name="querynextcod" Direction="Output" Type="Int32" />
<asp:ControlParameter  Name="licdsc" ControlID="frmdatos$insctrl000015" ConvertEmptyStringToNull="True" Direction="InputOutput" Type="String" Size="250" PropertyName="Text" />
<asp:ControlParameter  Name="lickyaautcod" ControlID="frmdatos$insctrl000014" ConvertEmptyStringToNull="True" Direction="InputOutput" Type="Int32" PropertyName="SelectedValue" />
<asp:ControlParameter  Name="lickyacod" ControlID="frmdatos$insctrl000013" ConvertEmptyStringToNull="True" Direction="InputOutput" Type="Int32" PropertyName="SelectedValue" />
<asp:ControlParameter  Name="licrequestID" ControlID="frmdatos$insctrl000012" ConvertEmptyStringToNull="True" Direction="InputOutput" Type="String" Size="200" PropertyName="Text" />
<asp:ControlParameter  Name="licconnuser" ControlID="frmdatos$insctrl000011" ConvertEmptyStringToNull="True" Direction="InputOutput" Type="String" Size="200" PropertyName="Text" />
<asp:ControlParameter  Name="licconnpwd" ControlID="frmdatos$insctrl000010" ConvertEmptyStringToNull="True" Direction="InputOutput" Type="String" Size="200" PropertyName="Text" />
<asp:ControlParameter  Name="licrequest" ControlID="frmdatos$insctrl000009" ConvertEmptyStringToNull="True" Direction="InputOutput" Type="String" Size="10000" PropertyName="Text" />
<asp:ControlParameter  Name="licresult" ControlID="frmdatos$insctrl000008" ConvertEmptyStringToNull="True" Direction="InputOutput" Type="String" Size="10000" PropertyName="Text" />
<asp:ControlParameter  Name="licenabled" ControlID="frmdatos$insctrl000007" ConvertEmptyStringToNull="True" Direction="InputOutput" Type="Boolean" PropertyName="Checked" />
<asp:SessionParameter  Name="qsecsid" SessionField="secsid" Direction="Input" />
<asp:Parameter  Name="param1" Direction="ReturnValue" />
</InsertParameters>
<SelectParameters>
<asp:QueryStringParameter  Name="param1" QueryStringField="param1" ConvertEmptyStringToNull="True" Type="String" Size="200" />
</SelectParameters>
<UpdateParameters>
<asp:QueryStringParameter  Name="param1" QueryStringField="param1" ConvertEmptyStringToNull="True" Type="String" Size="200" />
<asp:ControlParameter  Name="liccod" ControlID="frmdatos$editctrl000016" ConvertEmptyStringToNull="True" Direction="InputOutput" Type="Int32" PropertyName="Text" />
<asp:ControlParameter  Name="licdsc" ControlID="frmdatos$editctrl000015" ConvertEmptyStringToNull="True" Direction="InputOutput" Type="String" Size="250" PropertyName="Text" />
<asp:ControlParameter  Name="lickyaautcod" ControlID="frmdatos$editctrl000014" ConvertEmptyStringToNull="True" Direction="InputOutput" Type="Int32" PropertyName="SelectedValue" />
<asp:ControlParameter  Name="lickyacod" ControlID="frmdatos$editctrl000013" ConvertEmptyStringToNull="True" Direction="InputOutput" Type="Int32" PropertyName="SelectedValue" />
<asp:ControlParameter  Name="licrequestID" ControlID="frmdatos$editctrl000012" ConvertEmptyStringToNull="True" Direction="InputOutput" Type="String" Size="200" PropertyName="Text" />
<asp:ControlParameter  Name="licconnuser" ControlID="frmdatos$editctrl000011" ConvertEmptyStringToNull="True" Direction="InputOutput" Type="String" Size="200" PropertyName="Text" />
<asp:ControlParameter  Name="licconnpwd" ControlID="frmdatos$editctrl000010" ConvertEmptyStringToNull="True" Direction="InputOutput" Type="String" Size="200" PropertyName="Text" />
<asp:ControlParameter  Name="licrequest" ControlID="frmdatos$editctrl000009" ConvertEmptyStringToNull="True" Direction="InputOutput" Type="String" Size="10000" PropertyName="Text" />
<asp:ControlParameter  Name="licresult" ControlID="frmdatos$editctrl000008" ConvertEmptyStringToNull="True" Direction="InputOutput" Type="String" Size="10000" PropertyName="Text" />
<asp:ControlParameter  Name="licenabled" ControlID="frmdatos$editctrl000007" ConvertEmptyStringToNull="True" Direction="InputOutput" Type="Boolean" PropertyName="Checked" />
<asp:SessionParameter  Name="qsecsid" SessionField="secsid" Direction="Input" />
</UpdateParameters>
</asp:SqlDataSource>

					<asp:SqlDataSource  runat="server" ID="itemdsctrl000004" ProviderName="System.Data.SqlClient" CancelSelectOnNullParameter="False" onInit="itemdsctrl000004_Init" SelectCommandType="Text"
 SelectCommand="SELECT Q_LICDET.licdetcod,(Q_LICDETLICCOD.licdsc) as liccoddsc,Q_LICDET.licdetdsc,Q_LICDET.licdetval,(Q_LICDETQSECSID.secdsc) as qsecsiddsc,Q_LICDET.qsecdatetime,Q_LICDET.liccod,Q_LICDET.qsecsid FROM Q_LICDET AS Q_LICDET  LEFT JOIN Q_LIC AS Q_LICDETLICCOD ON Q_LICDETLICCOD.liccod=Q_LICDET.liccod LEFT JOIN Q_SECPLOGIN AS Q_LICDETQSECSID ON Q_LICDETQSECSID.sidcod=Q_LICDET.qsecsid  WHERE (Q_LICDET.liccod= @param1) AND Q_LICDET.licdetcod >= 1" onselected="itemdsctrl000004_Selected" >
<SelectParameters>
<asp:ControlParameter  Name="param1" ControlID="frmdatos" ConvertEmptyStringToNull="True" Direction="InputOutput" Type="String" Size="200" PropertyName="SelectedValue" />
</SelectParameters>
</asp:SqlDataSource>

				<asp:SqlDataSource  runat="server" ID="editdsctrl000004" ProviderName="System.Data.SqlClient" CancelSelectOnNullParameter="False" onInit="editdsctrl000004_Init" SelectCommandType="Text"
 SelectCommand="SELECT Q_LICDET.licdetcod,(Q_LICDETLICCOD.licdsc) as liccoddsc,Q_LICDET.licdetdsc,Q_LICDET.licdetval,(Q_LICDETQSECSID.secdsc) as qsecsiddsc,Q_LICDET.qsecdatetime,Q_LICDET.liccod,Q_LICDET.qsecsid FROM Q_LICDET AS Q_LICDET  LEFT JOIN Q_LIC AS Q_LICDETLICCOD ON Q_LICDETLICCOD.liccod=Q_LICDET.liccod LEFT JOIN Q_SECPLOGIN AS Q_LICDETQSECSID ON Q_LICDETQSECSID.sidcod=Q_LICDET.qsecsid  WHERE (Q_LICDET.liccod= @param1) AND Q_LICDET.licdetcod >= 1" onselected="editdsctrl000004_Selected" UpdateCommandType="Text"
 UpdateCommand="UPDATE Q_LICDET SET licdetcod=@licdetcod,licdetdsc=@licdetdsc,licdetval=@licdetval WHERE licdetcod=@licdetcod" onupdated="editdsctrl000004_Updated" InsertCommandType="Text"
 InsertCommand="INSERT INTO Q_LICDET (licdetcod,licdetdsc,licdetval) VALUES(@licdetcod,@licdetdsc,@licdetval)" onInserted="editdsctrl000004_Inserted" DeleteCommandType="Text"
 DeleteCommand="DELETE FROM Q_LICDET WHERE licdetcod=@licdetcod" ondeleted="editdsctrl000004_Deleted" >
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

		<tr>
			<td  style="vertical-align:top;" colspan="4" >
			<!-- Panel updpanelfrmupdpanelctrl000004 -->
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
<asp:FormView  ID="frmupdpanelfrmupdpanelctrl000004" runat="server" DataSourceID="dsfrmupdpanelctrl000004" DataKeyNames="licdetcod" DefaultMode="Insert" OnItemUpdated="frmupdpanelfrmupdpanelctrl000004_ItemUpdated" OnItemInserted="frmupdpanelfrmupdpanelctrl000004_ItemInserted" >
<EditItemTemplate>

<!-- Init Plantilla edicion --><div  runat="server" Visible='<%# Request.Querystring("_view_")<> 1 %>' ><asp:Button  runat="server" ID="cmdfrmupdpanelctrl000004updcancel" Text="Cancelar" CssClass="boton-acciones" CausesValidation="False" OnClick="cmdfrmupdpanelctrl000004updcancel_Click" />
<asp:Button  runat="server" ID="cmdfrmupdpanelctrl000004updconfirm" Text="Guardar" CssClass="boton-acciones" CausesValidation="True" OnClick="cmdfrmupdpanelctrl000004updconfirm_Click" ValidationGroup="vgeditupdpanelfrmupdpanelctrl000004" />
</div>
<table class="form" width="100%" cellpadding="1" cellspacing="1"><tr>
<td  colspan="1" >
Nombre
</td>
<td  colspan="1" >
<asp:TextBox  runat="server" ID="edtlicdetdsc" CssClass="form-control" Width="400px" MaxLength="100" OnDataBinding="edtlicdetdsc_DataBound_frmupdpanelfrmupdpanelctrl000004" TextMode="MultiLine" onkeypress="return textCounter(this,100);" onkeydown="return textCounter(this,100);" onkeyup="return textCounter(this,100);" onfocus="return textCounter_GetFocus(this,100);" onblur="return textCounter_LostFocus(this);" />
<ajaxkit:AutoCompleteExtender runat="server" ID="autoedtlicdetdsc" TargetControlID="edtlicdetdsc" ServiceMethod="edtlicdetdsc_get" MinimumPrefixLength="3" CompletionInterval="300" EnableCaching="True" CompletionSetCount="20" DelimiterCharacters=";, :" />
<asp:CompareValidator  runat="server" ID="vcdvaledtlicdetdsc" SetFocusOnError="true" CssClass="error" ControltoValidate="edtlicdetdsc" Display="Dynamic" Text="Ingrese un texto" ErrorMessage="Nombre:Ingrese un texto" Type="String" Operator="DataTypeCheck" ValidationGroup="vgeditupdpanelfrmupdpanelctrl000004" />
<asp:RegularExpressionValidator  runat="server" ID="vrgvaledtlicdetdsc" SetFocusOnError="true" CssClass="error" Display="Dynamic" ControltoValidate="edtlicdetdsc" ValidationExpression="[\w\s-+/*^()!|°$%&=-_,:;.ñÑ~áéíóúÁÉÍÓÚ#}{]{0,100}" Text="No mayor a 100 caracteres. Deben ser letras o numeros." ErrorMessage="Nombre:No mayor a 100 caracteres. Deben ser letras o numeros." ValidationGroup="vgeditupdpanelfrmupdpanelctrl000004" />
<ajaxkit:ValidatorCalloutExtender  runat="server" ID="ajaxvrgedtlicdetdsc" TargetControlID="vrgvaledtlicdetdsc" />

</td>
</tr>
<tr>
<td  colspan="1" >
Valor
</td>
<td  colspan="1" >
<asp:TextBox  runat="server" ID="edtlicdetval" CssClass="form-control" Width="400px" MaxLength="100" OnDataBinding="edtlicdetval_DataBound_frmupdpanelfrmupdpanelctrl000004" TextMode="MultiLine" onkeypress="return textCounter(this,100);" onkeydown="return textCounter(this,100);" onkeyup="return textCounter(this,100);" onfocus="return textCounter_GetFocus(this,100);" onblur="return textCounter_LostFocus(this);" />
<ajaxkit:AutoCompleteExtender runat="server" ID="autoedtlicdetval" TargetControlID="edtlicdetval" ServiceMethod="edtlicdetval_get" MinimumPrefixLength="3" CompletionInterval="300" EnableCaching="True" CompletionSetCount="20" DelimiterCharacters=";, :" />
<asp:CompareValidator  runat="server" ID="vcdvaledtlicdetval" SetFocusOnError="true" CssClass="error" ControltoValidate="edtlicdetval" Display="Dynamic" Text="Ingrese un texto" ErrorMessage="Valor:Ingrese un texto" Type="String" Operator="DataTypeCheck" ValidationGroup="vgeditupdpanelfrmupdpanelctrl000004" />
<asp:RegularExpressionValidator  runat="server" ID="vrgvaledtlicdetval" SetFocusOnError="true" CssClass="error" Display="Dynamic" ControltoValidate="edtlicdetval" ValidationExpression="[\w\s-+/*^()!|°$%&=-_,:;.ñÑ~áéíóúÁÉÍÓÚ#}{]{0,100}" Text="No mayor a 100 caracteres. Deben ser letras o numeros." ErrorMessage="Valor:No mayor a 100 caracteres. Deben ser letras o numeros." ValidationGroup="vgeditupdpanelfrmupdpanelctrl000004" />
<ajaxkit:ValidatorCalloutExtender  runat="server" ID="ajaxvrgedtlicdetval" TargetControlID="vrgvaledtlicdetval" />

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
Nombre
</td>
<td  colspan="1" >
<asp:TextBox  runat="server" ID="inslicdetdsc" CssClass="form-control" Width="400px" MaxLength="100" TextMode="MultiLine" onkeypress="return textCounter(this,100);" onkeydown="return textCounter(this,100);" onkeyup="return textCounter(this,100);" onfocus="return textCounter_GetFocus(this,100);" onblur="return textCounter_LostFocus(this);" />
<ajaxkit:AutoCompleteExtender runat="server" ID="autoinslicdetdsc" TargetControlID="inslicdetdsc" ServiceMethod="inslicdetdsc_get" MinimumPrefixLength="3" CompletionInterval="300" EnableCaching="True" CompletionSetCount="20" DelimiterCharacters=";, :" />
<asp:CompareValidator  runat="server" ID="vcdvalinslicdetdsc" SetFocusOnError="true" CssClass="error" ControltoValidate="inslicdetdsc" Display="Dynamic" Text="Ingrese un texto" ErrorMessage="Nombre:Ingrese un texto" Type="String" Operator="DataTypeCheck" ValidationGroup="vgupdpanelfrmupdpanelctrl000004" />
<asp:RegularExpressionValidator  runat="server" ID="vrgvalinslicdetdsc" SetFocusOnError="true" CssClass="error" Display="Dynamic" ControltoValidate="inslicdetdsc" ValidationExpression="[\w\s-+/*^()!|°$%&=-_,:;.ñÑ~áéíóúÁÉÍÓÚ#}{]{0,100}" Text="No mayor a 100 caracteres. Deben ser letras o numeros." ErrorMessage="Nombre:No mayor a 100 caracteres. Deben ser letras o numeros." ValidationGroup="vgupdpanelfrmupdpanelctrl000004" />
<ajaxkit:ValidatorCalloutExtender  runat="server" ID="ajaxvrginslicdetdsc" TargetControlID="vrgvalinslicdetdsc" />

</td>
</tr>
<tr>
<td  colspan="1" >
Valor
</td>
<td  colspan="1" >
<asp:TextBox  runat="server" ID="inslicdetval" CssClass="form-control" Width="400px" MaxLength="100" TextMode="MultiLine" onkeypress="return textCounter(this,100);" onkeydown="return textCounter(this,100);" onkeyup="return textCounter(this,100);" onfocus="return textCounter_GetFocus(this,100);" onblur="return textCounter_LostFocus(this);" />
<ajaxkit:AutoCompleteExtender runat="server" ID="autoinslicdetval" TargetControlID="inslicdetval" ServiceMethod="inslicdetval_get" MinimumPrefixLength="3" CompletionInterval="300" EnableCaching="True" CompletionSetCount="20" DelimiterCharacters=";, :" />
<asp:CompareValidator  runat="server" ID="vcdvalinslicdetval" SetFocusOnError="true" CssClass="error" ControltoValidate="inslicdetval" Display="Dynamic" Text="Ingrese un texto" ErrorMessage="Valor:Ingrese un texto" Type="String" Operator="DataTypeCheck" ValidationGroup="vgupdpanelfrmupdpanelctrl000004" />
<asp:RegularExpressionValidator  runat="server" ID="vrgvalinslicdetval" SetFocusOnError="true" CssClass="error" Display="Dynamic" ControltoValidate="inslicdetval" ValidationExpression="[\w\s-+/*^()!|°$%&=-_,:;.ñÑ~áéíóúÁÉÍÓÚ#}{]{0,100}" Text="No mayor a 100 caracteres. Deben ser letras o numeros." ErrorMessage="Valor:No mayor a 100 caracteres. Deben ser letras o numeros." ValidationGroup="vgupdpanelfrmupdpanelctrl000004" />
<ajaxkit:ValidatorCalloutExtender  runat="server" ID="ajaxvrginslicdetval" TargetControlID="vrgvalinslicdetval" />

</td>
</tr>
</table>
<!-- End Plantilla insercion --></InsertItemTemplate>

</asp:FormView>

<asp:SqlDataSource  runat="server" ID="dsfrmupdpanelctrl000004" ProviderName="System.Data.SqlClient" CancelSelectOnNullParameter="False" onInit="dsfrmupdpanelctrl000004_Init" SelectCommandType="Text"
 SelectCommand="SELECT Q_LICDET.licdetcod,(Q_LICDETLICCOD.licdsc) as liccoddsc,Q_LICDET.licdetdsc,Q_LICDET.licdetval,(Q_LICDETQSECSID.secdsc) as qsecsiddsc,Q_LICDET.qsecdatetime,Q_LICDET.liccod,Q_LICDET.qsecsid FROM Q_LICDET  LEFT JOIN Q_LIC AS Q_LICDETLICCOD ON Q_LICDETLICCOD.liccod=Q_LICDET.liccod LEFT JOIN Q_SECPLOGIN AS Q_LICDETQSECSID ON Q_LICDETQSECSID.sidcod=Q_LICDET.qsecsid  WHERE Q_LICDET.licdetcod=@licdetcod" onselected="dsfrmupdpanelctrl000004_Selected" UpdateCommandType="Text"
 UpdateCommand="UPDATE Q_LICDET SET licdetcod=@licdetcod,licdetdsc=@licdetdsc,licdetval=@licdetval WHERE Q_LICDET.licdetcod=@licdetcod" onupdated="dsfrmupdpanelctrl000004_Updated" InsertCommandType="Text"
 InsertCommand="DECLARE @querynextcodlicdetcod int  SET @querynextcodlicdetcod =(SELECT ISNULL(MAX(licdetcod),0)+1 FROM Q_LICDET WHERE licdetcod > 0 ) INSERT INTO Q_LICDET (licdetcod,liccod,licdetdsc,licdetval) VALUES(@querynextcodlicdetcod,@liccod,@licdetdsc,@licdetval) SELECT @querynextcod =@querynextcodlicdetcod" onInserted="dsfrmupdpanelctrl000004_Inserted" >
<InsertParameters>
<asp:ControlParameter  Name="liccod" ControlID="frmdatos" ConvertEmptyStringToNull="True" Direction="Input" Type="String" Size="200" PropertyName="SelectedValue" />
<asp:ControlParameter  Name="licdetdsc" ControlID="frmupdpanelfrmupdpanelctrl000004$inslicdetdsc" ConvertEmptyStringToNull="True" Direction="InputOutput" Type="String" Size="100" PropertyName="Text" />
<asp:ControlParameter  Name="licdetval" ControlID="frmupdpanelfrmupdpanelctrl000004$inslicdetval" ConvertEmptyStringToNull="True" Direction="InputOutput" Type="String" Size="100" PropertyName="Text" />
<asp:Parameter  Name="querynextcod" Direction="Output" Type="Int32" />
</InsertParameters>
<SelectParameters>
<asp:Parameter  Name="licdetcod" Direction="InputOutput" Type="String" Size="200" />
</SelectParameters>
<UpdateParameters>
<asp:Parameter  Name="licdetcod" Direction="InputOutput" Type="String" Size="200" />
<asp:ControlParameter  Name="licdetdsc" ControlID="frmupdpanelfrmupdpanelctrl000004$edtlicdetdsc" ConvertEmptyStringToNull="True" Direction="InputOutput" Type="String" Size="100" PropertyName="Text" />
<asp:ControlParameter  Name="licdetval" ControlID="frmupdpanelfrmupdpanelctrl000004$edtlicdetval" ConvertEmptyStringToNull="True" Direction="InputOutput" Type="String" Size="100" PropertyName="Text" />
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

</td>
</tr>

</table>
</asp:Content>



