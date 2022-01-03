<%@ Page Language="VB" CodeFile="hrclicensing.aspx.vb" MasterPageFile="general.master" AutoEventWireup="True" Inherits="hrclicensing" Title="Licencias" %>
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
<tr><td  style="width:32px;" colspan="1" ><img  src="imagenes/iconLicencia.png" width="32px" height="32px" alt="Licencias" hspace="4"/></td>
<td  style="text-align:left;" colspan="1" ><span class="form-title">Licencias</span></td></tr>
</table>
</td></tr>

<tr>
<td  colspan="1" >
					<asp:UpdatePanel  ID="updupdpnlSearch" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="True" >
<Triggers>
  <asp:AsyncPostBackTrigger  ControlID="ctrl000022" EventName="Click" />
  <asp:AsyncPostBackTrigger  ControlID="ctrl000020" EventName="Click" />
</Triggers>
<ContentTemplate>
<!-- Init Filtros de busqueda -->
<table id="Search" class="hide_print" style="background-color:#F0F0F0;" width="100%">
<tr>
<td  class="search-title" colspan="5" >Filtros de busqueda</td></tr>

					<tr>
<td  class="search-item-title" style="vertical-align:top;width:15%" colspan="1" >
						Descripcion
						</td>
						<td  class="search-table" style="vertical-align:top;width:35%" colspan="1" >
						<asp:TextBox  runat="server" ID="ctrl000025" CssClass="form-control" Width="280px" MaxLength="30" Tooltip="Campo100126[]" />
<asp:CompareValidator  runat="server" ID="vcdvalctrl000025" SetFocusOnError="true" CssClass="error" ControltoValidate="ctrl000025" Display="Dynamic" Text="Ingrese un texto" ErrorMessage="Descripcion:Ingrese un texto" Type="String" Operator="DataTypeCheck" ValidationGroup="vg" />
<asp:RegularExpressionValidator  runat="server" ID="vrgvalctrl000025" SetFocusOnError="true" CssClass="error" Display="Dynamic" ControltoValidate="ctrl000025" ValidationExpression="[\w\s-+/*^()!|°$%&=-_,:;.ñÑ~áéíóúÁÉÍÓÚ#}{]{0,30}" Text="No mayor a 30 caracteres. Deben ser letras o numeros." ErrorMessage="Descripcion:No mayor a 30 caracteres. Deben ser letras o numeros." ValidationGroup="vg" />
<ajaxkit:ValidatorCalloutExtender  runat="server" ID="ajaxvrgctrl000025" TargetControlID="vrgvalctrl000025" />

						</td>
			</table>
<!-- End Filtros de busqueda -->
</ContentTemplate>
</asp:UpdatePanel>
<asp:UpdateProgress  runat="server" AssociatedUpdatePanelID="updupdpnlSearch" >
<ProgressTemplate><span  class="ajaxupd" ><div style="position:absolute;top:2px;left:50%; border-color:#333333; border:1; border-style:solid; background-color:#FFFFFF;padding:3px;"><image src="imagenes/ajaxupdate.gif" border=0 />Actualizando</div></span></ProgressTemplate>
</asp:UpdateProgress>

</td>
</tr>
<tr>
<td  colspan="1" >
							<asp:UpdatePanel  ID="updupdpnlActions" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="True" >
<Triggers>
  <asp:AsyncPostBackTrigger  ControlID="ctrl000020" EventName="Click" />
</Triggers>
<ContentTemplate>
<!-- Init -->
<table id="Actions" class="hide_print" style="background-color:#F0F0F0;" width="100%">
<tr>
<td  class="search-title" colspan="5" >
								<asp:Button  runat="server" ID="ctrl000023" Text="Buscar" CssClass="boton-acciones" CausesValidation="True" OnClick="ctrl000023_Click" ValidationGroup="vg" />
							<asp:Button  runat="server" ID="ctrl000022" Text="Limpiar filtros" CssClass="boton-acciones" CausesValidation="True" OnClick="ctrl000022_Click" ValidationGroup="vg" />
						<asp:Button  runat="server" ID="ctrl000021" Text="Nuevo" CssClass="boton-acciones" CausesValidation="False" OnLoad="ctrl000021_Load" PostBackURL="hrclicensing_det.aspx?_mode_=1&_closea_=0" />
					<asp:Button  runat="server" ID="ctrl000020" Text="Refrescar" CssClass="boton-acciones" CausesValidation="True" ValidationGroup="vg" />
		</td></tr>
</table>
<!-- End -->
</ContentTemplate>
</asp:UpdatePanel>
<asp:UpdateProgress  runat="server" AssociatedUpdatePanelID="updupdpnlActions" >
<ProgressTemplate><span  class="ajaxupd" ><div style="position:absolute;top:2px;left:50%; border-color:#333333; border:1; border-style:solid; background-color:#FFFFFF;padding:3px;"><image src="imagenes/ajaxupdate.gif" border=0 />Actualizando</div></span></ProgressTemplate>
</asp:UpdateProgress>

</td>
</tr>
<tr>
<td  colspan="1" >
					<asp:UpdatePanel  ID="updupdpnlKeys" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="True" >
<Triggers>
  <asp:AsyncPostBackTrigger  ControlID="ctrl000023" EventName="Click" />
  <asp:AsyncPostBackTrigger  ControlID="ctrl000022" EventName="Click" />
  <asp:AsyncPostBackTrigger  ControlID="ctrl000020" EventName="Click" />
</Triggers>
<ContentTemplate>
<!-- Init -->
<table id="Keys" style="background-color:#F0F0F0;" width="100%">

					<tr>
						<td  class="search-table" style="vertical-align:top;" colspan="4" >
						<asp:UpdatePanel  ID="updupdpnlctrl000027" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="True" >
<Triggers>
  <asp:AsyncPostBackTrigger  ControlID="ctrl000023" EventName="Click" />
  <asp:AsyncPostBackTrigger  ControlID="ctrl000022" EventName="Click" />
</Triggers>
<ContentTemplate>
<asp:UpdatePanel  ID="updupdctrl000027" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="True" >
<Triggers>
  <asp:AsyncPostBackTrigger  ControlID="ctrl000027" EventName="PageIndexChanging" />
</Triggers>
<ContentTemplate>

<asp:GridView  runat="server" ID="ctrl000027" AllowPaging="True" AllowSorting="True" AutoGenerateColumns="False" CssClass="form-control" PageSize="50"
 GridLines="None" ShowHeader="True" ShowFooter="True" Width="100%" UseAccessibleHeader="False" DataKeyNames="liccod" CellPadding="2" DataSourceID="dsctrl000027" onRowCreated="ctrl000027_RowCreated" >
<FooterStyle  CssClass="tabla-footer" />
<RowStyle  CssClass="tabla-fila" />
<AlternatingRowStyle  CssClass="tabla-fila-alternativa" />
<SelectedRowStyle  CssClass="tabla-fila" />
<PagerStyle  CssClass="tabla-pager" />
<HeaderStyle  CssClass="tabla-titulo" ForeColor="White" />
<EmptyDataTemplate>
<p  class="tabla-fila"><img  src="./imagenes/evwarning.png" width="16px" alt="Sin datos"/>Datos no encontrados</p>
</EmptyDataTemplate>
<Columns>
<asp:TemplateField   ItemStyle-CssClass="tabla-celda" HeaderStyle-CssClass="tabla-titulo" FooterStyle-CssClass="tabla-celda">
<ItemStyle  HorizontalAlign="Left" Width="12px" CssClass="searchresult-table-cell" />
<ItemTemplate>
<asp:Image  runat="server" ID="rowctrl000027imageimg" Width="12px" GenerateEmptyAlternateText="True" ImageURL="imagenes/iconLicencia.png" BorderWidth="0" AlternateText="Imagen no disp." />
</ItemTemplate>
</asp:TemplateField>
<asp:TemplateField   AccessibleHeaderText="Descripcion" HeaderText="Descripcion" SortExpression="licdsc" ItemStyle-CssClass="tabla-celda" HeaderStyle-CssClass="tabla-titulo" FooterStyle-CssClass="tabla-celda">
<ItemStyle  HorizontalAlign="Left" CssClass="searchresult-table-cell" />
<ItemTemplate>
<asp:LinkButton runat="server" ID="ctrl000027colitem1view" Text='<%# Eval("licdsc") %>' CssClass="form-control-read" OnDataBinding="ctrl000027colitem1view_DataBinding" CausesValidation="False" OnClientClick='<%# "javascript:if (window.showModalDialog) {window.showModalDialog(" & chr(39) & "hrclicensing_det.aspx?_mode_=0&_closea_=1&isDlg=1&param1=" & Eval("liccod") & "&timestamp=" & chr(39) & " + new Date().getTime() + " & chr(39) & "" & chr(39) & ", " & chr(39) & "" & chr(39) & ", " & chr(39) & "menubar=no;resizable=yes;toolbar=no;status=no;center=yes;dialogWidth=750px;dialogHeight=570px;" & chr(39) & ");} else {window.open(" & chr(39) & "hrclicensing_det.aspx?_mode_=0&_closea_=1&isDlg=1&param1=" & Eval("liccod") & "&timestamp=" & chr(39) & " + new Date().getTime() + " & chr(39) & "" & chr(39) & ", " & chr(39) & "" & chr(39) & ", " & chr(39) & "modal=yes;directories=no;menubar=no;resizable=yes;toolbar=no;status=no;center=yes;dialogWidth=750px;dialogHeight=570px;" & chr(39) & ");};return false;" %>' PostBackUrl='<%# "javascript:if (window.showModalDialog) {window.showModalDialog(" & chr(39) & "hrclicensing_det.aspx?_mode_=0&_closea_=1&isDlg=1&param1=" & Eval("liccod") & "&timestamp=" & chr(39) & " + new Date().getTime() + " & chr(39) & "" & chr(39) & ", " & chr(39) & "" & chr(39) & ", " & chr(39) & "menubar=no;resizable=yes;toolbar=no;status=no;center=yes;dialogWidth=750px;dialogHeight=570px;" & chr(39) & ");} else {window.open(" & chr(39) & "hrclicensing_det.aspx?_mode_=0&_closea_=1&isDlg=1&param1=" & Eval("liccod") & "&timestamp=" & chr(39) & " + new Date().getTime() + " & chr(39) & "" & chr(39) & ", " & chr(39) & "" & chr(39) & ", " & chr(39) & "modal=yes;directories=no;menubar=no;resizable=yes;toolbar=no;status=no;center=yes;dialogWidth=750px;dialogHeight=570px;" & chr(39) & ");};return false;" %>' />
</ItemTemplate>
</asp:TemplateField>
<asp:HyperLinkField  HeaderText="Ver" DataNavigateUrlFields="liccod" DataNavigateUrlFormatString="hrclicensing_det.aspx?_mode_=0&_closea_=0&param1={0}" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="16" Text="&lt;img src=./imagenes/actview.png border=0&gt alt='Ver' width='16' ;" >
<HeaderStyle  CssClass="tabla-titulo" />
</asp:HyperLinkField>
<asp:TemplateField   AccessibleHeaderText="Editar" HeaderText="Editar" ItemStyle-CssClass="tabla-celda" HeaderStyle-CssClass="tabla-titulo" FooterStyle-CssClass="tabla-celda">
<ItemStyle  HorizontalAlign="Left" Width="16px" />
<ItemTemplate>
<asp:ImageButton runat="server" ID="cmdctrl000027edit" ImageURL="./imagenes/actmod.png" Width="16px" Height="16px" CausesValidation="False" PostBackURL='<%# "hrclicensing_det.aspx?_mode_=2&_closea_=0&param1=" & Eval("liccod") & "" %>' /></ItemTemplate>
</asp:TemplateField>
<asp:TemplateField   AccessibleHeaderText="Copiar" HeaderText="Copiar" ItemStyle-CssClass="tabla-celda" HeaderStyle-CssClass="tabla-titulo" FooterStyle-CssClass="tabla-celda">
<ItemStyle  HorizontalAlign="Left" Width="16px" />
<ItemTemplate>
<asp:ImageButton runat="server" ID="cmdctrl000027copy" ImageURL="./imagenes/actcopy.png" Width="16px" Height="16px" CausesValidation="False" PostBackURL='<%# "hrclicensing_det.aspx?_mode_=25&_closea_=0&param1=" & Eval("liccod") & "" %>' /></ItemTemplate>
</asp:TemplateField>
</Columns>
</asp:GridView>
</ContentTemplate>
</asp:UpdatePanel>
<asp:UpdateProgress  runat="server" AssociatedUpdatePanelID="updupdctrl000027" >
<ProgressTemplate><span  class="ajaxupd" ><div style="position:absolute;top:2px;left:50%; border-color:#333333; border:1; border-style:solid; background-color:#FFFFFF;padding:3px;"><image src="imagenes/ajaxupdate.gif" border=0 />Actualizando</div></span></ProgressTemplate>
</asp:UpdateProgress>

</ContentTemplate>
</asp:UpdatePanel>
<asp:UpdateProgress  runat="server" AssociatedUpdatePanelID="updupdpnlctrl000027" >
<ProgressTemplate><span  class="ajaxupd" ><div style="position:absolute;top:2px;left:50%; border-color:#333333; border:1; border-style:solid; background-color:#FFFFFF;padding:3px;"><image src="imagenes/ajaxupdate.gif" border=0 />Actualizando</div></span></ProgressTemplate>
</asp:UpdateProgress>

					<asp:SqlDataSource  runat="server" ID="dsctrl000027" ProviderName="System.Data.SqlClient" CancelSelectOnNullParameter="False" onInit="dsctrl000027_Init" SelectCommandType="Text"
 SelectCommand="SELECT Q_LIC.liccod,Q_LIC.licdsc FROM Q_LIC   WHERE ((Q_LIC.licdsc LIKE '%' + @ctrl000025 + '%' OR @ctrl000025  IS NULL) AND @qsidcod_list ) AND Q_LIC.liccod >= 1 ORDER BY Q_LIC.licdsc,Q_LIC.liccod" onselected="dsctrl000027_Selected" onSelecting="dsctrl000027_Selecting" >
<SelectParameters>
<asp:ControlParameter  Name="ctrl000025" ControlID="ctrl000025" ConvertEmptyStringToNull="True" Direction="InputOutput" Type="String" Size="250" PropertyName="Text" />
<asp:Parameter  Name="qsidcod_list" Direction="Input" Type="String" Size="200" />
</SelectParameters>
</asp:SqlDataSource>

				<br /><asp:Label  runat="server" ID="lblerror" CssClass="error" width="100%" />
	</table>
<!-- End -->
</ContentTemplate>
</asp:UpdatePanel>
<asp:UpdateProgress  runat="server" AssociatedUpdatePanelID="updupdpnlKeys" >
<ProgressTemplate><span  class="ajaxupd" ><div style="position:absolute;top:2px;left:50%; border-color:#333333; border:1; border-style:solid; background-color:#FFFFFF;padding:3px;"><image src="imagenes/ajaxupdate.gif" border=0 />Actualizando</div></span></ProgressTemplate>
</asp:UpdateProgress>

</td>
</tr>

</table>
</asp:Content>



