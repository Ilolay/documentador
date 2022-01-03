<%@ Page Language="VB" CodeFile="frmsysparam.aspx.vb" MasterPageFile="general.master" AutoEventWireup="True" Inherits="frmsysparam" Title="Parámetros del sistema" %>
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
<tr><td  style="width:32px;" colspan="1" ><img  src="imagenes/iconParametro.png" width="32px" height="32px" alt="Parámetros del sistema" hspace="4"/></td>
<td  style="text-align:left;" colspan="1" ><span class="form-title">Parámetros del sistema</span></td></tr>
</table>
</td></tr>
<tr><td  colspan="1" >							<asp:UpdatePanel  ID="updupdpnlSearch" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="True" >
<Triggers>
  <asp:AsyncPostBackTrigger  ControlID="ctrl000010" EventName="Click" />
  <asp:AsyncPostBackTrigger  ControlID="ctrl000009" EventName="Click" />
</Triggers>
<ContentTemplate>
<!-- Init Filtros de busqueda -->
<table id="Search" class="hide_print" style="background-color:#F0F0F0;" width="100%">
<tr>
<td  class="search-title" colspan="5" >Filtros de busqueda</td></tr>
							<tr><td  class="search-item-title" style="vertical-align:top;width:15%" colspan="1" >								Valor								</td>								<td  class="search-table" style="vertical-align:top;width:35%" colspan="1" >								<asp:TextBox  runat="server" ID="ctrl000013" CssClass="form-control" Width="280px" MaxLength="30" Tooltip="Campo100097[]" />
<asp:CompareValidator  runat="server" ID="vcdvalctrl000013" SetFocusOnError="true" CssClass="error" ControltoValidate="ctrl000013" Display="Dynamic" Text="Ingrese un texto" ErrorMessage="Valor:Ingrese un texto" Type="String" Operator="DataTypeCheck" ValidationGroup="vg" />
<asp:RegularExpressionValidator  runat="server" ID="vrgvalctrl000013" SetFocusOnError="true" CssClass="error" Display="Dynamic" ControltoValidate="ctrl000013" ValidationExpression="[\w\s-+/*^()!|°$%&=-_,:;.ñÑ~áéíóúÁÉÍÓÚ#}{]{0,30}" Text="No mayor a 30 caracteres. Deben ser letras o numeros." ErrorMessage="Valor:No mayor a 30 caracteres. Deben ser letras o numeros." ValidationGroup="vg" />
<ajaxkit:ValidatorCalloutExtender  runat="server" ID="ajaxvrgctrl000013" TargetControlID="vrgvalctrl000013" />
								</td><td  class="search-item-title" style="vertical-align:top;width:15%" colspan="1" >							ID							</td>							<td  class="search-table" style="vertical-align:top;width:35%" colspan="1" >							<asp:TextBox  runat="server" ID="ctrl000014" CssClass="form-control" Width="40px" Tooltip="Campo100098[]" />
<asp:CompareValidator  runat="server" ID="vcdvalctrl000014" SetFocusOnError="true" CssClass="error" ControltoValidate="ctrl000014" Display="Dynamic" Text='Ingrese un entero' ErrorMessage='ID:Ingrese un entero' Type="Integer" Operator="DataTypeCheck" ValidationGroup="vg" />
<asp:RangeValidator  runat="server" ID="vrnvalctrl000014" SetFocusOnError="true" CssClass="error" Display="Dynamic" ControltoValidate="ctrl000014" Text="Ingrese un valor entre 1 y 2000000000" ErrorMessage="ID:Ingrese un valor entre 1 y 2000000000" Type="Integer" CultureInvariantValues="True" MaximumValue="2000000000" MinimumValue="1" ValidationGroup="vg" />
<ajaxkit:ValidatorCalloutExtender  runat="server" ID="ajaxvrnctrl000014" TargetControlID="vrnvalctrl000014" />
							</td>						</tr>					<tr><td  class="search-item-title" style="vertical-align:top;width:15%" colspan="1" >						Nombre						</td>						<td  class="search-table" style="vertical-align:top;width:35%" colspan="1" >						<asp:TextBox  runat="server" ID="ctrl000015" CssClass="form-control" Width="280px" MaxLength="30" Tooltip="Campo100096[]" />
<asp:CompareValidator  runat="server" ID="vcdvalctrl000015" SetFocusOnError="true" CssClass="error" ControltoValidate="ctrl000015" Display="Dynamic" Text="Ingrese un texto" ErrorMessage="Nombre:Ingrese un texto" Type="String" Operator="DataTypeCheck" ValidationGroup="vg" />
<asp:RegularExpressionValidator  runat="server" ID="vrgvalctrl000015" SetFocusOnError="true" CssClass="error" Display="Dynamic" ControltoValidate="ctrl000015" ValidationExpression="[\w\s-+/*^()!|°$%&=-_,:;.ñÑ~áéíóúÁÉÍÓÚ#}{]{0,30}" Text="No mayor a 30 caracteres. Deben ser letras o numeros." ErrorMessage="Nombre:No mayor a 30 caracteres. Deben ser letras o numeros." ValidationGroup="vg" />
<ajaxkit:ValidatorCalloutExtender  runat="server" ID="ajaxvrgctrl000015" TargetControlID="vrgvalctrl000015" />
						</td>			</table>
<!-- End Filtros de busqueda -->
</ContentTemplate>
</asp:UpdatePanel>
<asp:UpdateProgress  runat="server" AssociatedUpdatePanelID="updupdpnlSearch" >
<ProgressTemplate><span  class="ajaxupd" ><div style="position:absolute;top:2px;left:50%; border-color:#333333; border:1; border-style:solid; background-color:#FFFFFF;padding:3px;"><image src="imagenes/ajaxupdate.gif" border=0 />Actualizando</div></span></ProgressTemplate>
</asp:UpdateProgress>
</td></tr><tr><td  colspan="1" >						<asp:UpdatePanel  ID="updupdpnlActions" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="True" >
<Triggers>
  <asp:AsyncPostBackTrigger  ControlID="ctrl000009" EventName="Click" />
</Triggers>
<ContentTemplate>
<!-- Init -->
<table id="Actions" class="hide_print" style="background-color:#F0F0F0;" width="100%">
<tr>
<td  class="search-title" colspan="5" >							<asp:Button  runat="server" ID="ctrl000011" Text="Buscar" CssClass="boton-acciones" CausesValidation="True" OnClick="ctrl000011_Click" ValidationGroup="vg" />						<asp:Button  runat="server" ID="ctrl000010" Text="Limpiar filtros" CssClass="boton-acciones" CausesValidation="True" OnClick="ctrl000010_Click" ValidationGroup="vg" />					<asp:Button  runat="server" ID="ctrl000009" Text="Refrescar" CssClass="boton-acciones" CausesValidation="True" ValidationGroup="vg" />		</td></tr>
</table>
<!-- End -->
</ContentTemplate>
</asp:UpdatePanel>
<asp:UpdateProgress  runat="server" AssociatedUpdatePanelID="updupdpnlActions" >
<ProgressTemplate><span  class="ajaxupd" ><div style="position:absolute;top:2px;left:50%; border-color:#333333; border:1; border-style:solid; background-color:#FFFFFF;padding:3px;"><image src="imagenes/ajaxupdate.gif" border=0 />Actualizando</div></span></ProgressTemplate>
</asp:UpdateProgress>
</td></tr><tr><td  colspan="1" >					<asp:UpdatePanel  ID="updupdpnlKeys" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="True" >
<Triggers>
  <asp:AsyncPostBackTrigger  ControlID="ctrl000011" EventName="Click" />
  <asp:AsyncPostBackTrigger  ControlID="ctrl000010" EventName="Click" />
  <asp:AsyncPostBackTrigger  ControlID="ctrl000009" EventName="Click" />
</Triggers>
<ContentTemplate>
<!-- Init -->
<table id="Keys" style="background-color:#F0F0F0;" width="100%">
					<tr>						<td  class="search-table" style="vertical-align:top;" colspan="4" >						<asp:UpdatePanel  ID="updupdpnlctrl000017" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="True" >
<Triggers>
  <asp:AsyncPostBackTrigger  ControlID="ctrl000011" EventName="Click" />
  <asp:AsyncPostBackTrigger  ControlID="ctrl000010" EventName="Click" />
</Triggers>
<ContentTemplate>
<asp:UpdatePanel  ID="updupdctrl000017" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="True" >
<Triggers>
  <asp:AsyncPostBackTrigger  ControlID="ctrl000017" EventName="PageIndexChanging" />
</Triggers>
<ContentTemplate>

<asp:GridView  runat="server" ID="ctrl000017" AllowPaging="True" AllowSorting="True" AutoGenerateColumns="False" CssClass="form-control" PageSize="50"
 GridLines="None" ShowHeader="True" ShowFooter="True" Width="100%" UseAccessibleHeader="False" DataKeyNames="sysparamcod" CellPadding="2" DataSourceID="dsctrl000017" onRowCreated="ctrl000017_RowCreated" >
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
<asp:Image  runat="server" ID="rowctrl000017imageimg" Width="12px" GenerateEmptyAlternateText="True" ImageURL="imagenes/iconParametro.png" BorderWidth="0" AlternateText="Imagen no disp." />
</ItemTemplate>
</asp:TemplateField>
<asp:TemplateField   AccessibleHeaderText="Nombre" HeaderText="Nombre" SortExpression="sysparamobs" ItemStyle-CssClass="tabla-celda" HeaderStyle-CssClass="tabla-titulo" FooterStyle-CssClass="tabla-celda">
<ItemStyle  HorizontalAlign="Left" CssClass="searchresult-table-cell" />
<ItemTemplate>
<asp:LinkButton runat="server" ID="ctrl000017colitem1view" Text='<%# Replace(Eval("sysparamobs").ToString,Chr(10),"<br />") %>' CssClass="form-control-read" OnDataBinding="ctrl000017colitem1view_DataBinding" CausesValidation="False" OnClientClick='<%# "javascript:if (window.showModalDialog) {window.showModalDialog(" & chr(39) & "frmsysparam_det.aspx?_mode_=0&_closea_=1&param1=" & Eval("sysparamcod") & "&timestamp=" & chr(39) & " + new Date().getTime() + " & chr(39) & "" & chr(39) & ", " & chr(39) & "" & chr(39) & ", " & chr(39) & "menubar=no;resizable=yes;toolbar=no;status=no;center=yes;dialogWidth=750px;dialogHeight=570px;" & chr(39) & ");} else {window.open(" & chr(39) & "frmsysparam_det.aspx?_mode_=0&_closea_=1&param1=" & Eval("sysparamcod") & "&timestamp=" & chr(39) & " + new Date().getTime() + " & chr(39) & "" & chr(39) & ", " & chr(39) & "" & chr(39) & ", " & chr(39) & "modal=yes;directories=no;menubar=no;resizable=yes;toolbar=no;status=no;center=yes;dialogWidth=750px;dialogHeight=570px;" & chr(39) & ");};return false;" %>' PostBackUrl='<%# "javascript:if (window.showModalDialog) {window.showModalDialog(" & chr(39) & "frmsysparam_det.aspx?_mode_=0&_closea_=1&param1=" & Eval("sysparamcod") & "&timestamp=" & chr(39) & " + new Date().getTime() + " & chr(39) & "" & chr(39) & ", " & chr(39) & "" & chr(39) & ", " & chr(39) & "menubar=no;resizable=yes;toolbar=no;status=no;center=yes;dialogWidth=750px;dialogHeight=570px;" & chr(39) & ");} else {window.open(" & chr(39) & "frmsysparam_det.aspx?_mode_=0&_closea_=1&param1=" & Eval("sysparamcod") & "&timestamp=" & chr(39) & " + new Date().getTime() + " & chr(39) & "" & chr(39) & ", " & chr(39) & "" & chr(39) & ", " & chr(39) & "modal=yes;directories=no;menubar=no;resizable=yes;toolbar=no;status=no;center=yes;dialogWidth=750px;dialogHeight=570px;" & chr(39) & ");};return false;" %>' />
</ItemTemplate>
</asp:TemplateField>
<asp:TemplateField   AccessibleHeaderText="Valor" HeaderText="Valor" SortExpression="sysparamdsc" ItemStyle-CssClass="tabla-celda" HeaderStyle-CssClass="tabla-titulo" FooterStyle-CssClass="tabla-celda">
<ItemStyle  HorizontalAlign="Left" CssClass="searchresult-table-cell" />
<ItemTemplate>
<asp:Label  runat="server" ID="ctrl000017colitem2" width="100%" Text='<%# Replace(Eval("sysparamdsc").ToString,Chr(10),"<br />") %>' /></ItemTemplate>
</asp:TemplateField>
<asp:TemplateField   AccessibleHeaderText="ID" HeaderText="ID" SortExpression="sysparamID" ItemStyle-CssClass="tabla-celda" HeaderStyle-CssClass="tabla-titulo" FooterStyle-CssClass="tabla-celda">
<ItemStyle  HorizontalAlign="Left" CssClass="searchresult-table-cell" />
<ItemTemplate>
<asp:Label  runat="server" ID="ctrl000017colitem3" width="100%" Text='<%# Eval("sysparamID").ToString %>' /></ItemTemplate>
</asp:TemplateField>
<asp:HyperLinkField  HeaderText="Ver" DataNavigateUrlFields="sysparamcod" DataNavigateUrlFormatString="frmsysparam_det.aspx?_mode_=0&_closea_=0&param1={0}" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="16" Text="&lt;img src=./imagenes/actview.png border=0&gt alt='Ver' width='16' ;" >
<HeaderStyle  CssClass="tabla-titulo" />
</asp:HyperLinkField>
<asp:TemplateField   AccessibleHeaderText="Editar" HeaderText="Editar" ItemStyle-CssClass="tabla-celda" HeaderStyle-CssClass="tabla-titulo" FooterStyle-CssClass="tabla-celda">
<ItemStyle  HorizontalAlign="Left" Width="16px" />
<ItemTemplate>
<asp:ImageButton runat="server" ID="cmdctrl000017edit" ImageURL="./imagenes/actmod.png" Width="16px" Height="16px" CausesValidation="False" PostBackURL='<%# "frmsysparam_det.aspx?_mode_=2&_closea_=0&param1=" & Eval("sysparamcod") & "" %>' /></ItemTemplate>
</asp:TemplateField>
</Columns>
</asp:GridView>
</ContentTemplate>
</asp:UpdatePanel>
<asp:UpdateProgress  runat="server" AssociatedUpdatePanelID="updupdctrl000017" >
<ProgressTemplate><span  class="ajaxupd" ><div style="position:absolute;top:2px;left:50%; border-color:#333333; border:1; border-style:solid; background-color:#FFFFFF;padding:3px;"><image src="imagenes/ajaxupdate.gif" border=0 />Actualizando</div></span></ProgressTemplate>
</asp:UpdateProgress>

</ContentTemplate>
</asp:UpdatePanel>
<asp:UpdateProgress  runat="server" AssociatedUpdatePanelID="updupdpnlctrl000017" >
<ProgressTemplate><span  class="ajaxupd" ><div style="position:absolute;top:2px;left:50%; border-color:#333333; border:1; border-style:solid; background-color:#FFFFFF;padding:3px;"><image src="imagenes/ajaxupdate.gif" border=0 />Actualizando</div></span></ProgressTemplate>
</asp:UpdateProgress>
					<asp:SqlDataSource  runat="server" ID="dsctrl000017" ProviderName="System.Data.SqlClient" CancelSelectOnNullParameter="False" onInit="dsctrl000017_Init" SelectCommandType="Text"
 SelectCommand="SELECT Q_SYSPARAM.sysparamcod,Q_SYSPARAM.sysparamobs,Q_SYSPARAM.sysparamdsc,Q_SYSPARAM.sysparamID FROM Q_SYSPARAM   WHERE ((Q_SYSPARAM.sysparamdsc LIKE '%' + @ctrl000013 + '%' OR @ctrl000013  IS NULL) AND (Q_SYSPARAM.sysparamID = @ctrl000014 OR @ctrl000014  IS NULL) AND (Q_SYSPARAM.sysparamobs LIKE '%' + @ctrl000015 + '%' OR @ctrl000015  IS NULL) AND Q_SYSPARAM.sysparamID NOT IN (1,27,26,20,6)) AND Q_SYSPARAM.sysparamcod >= 1 ORDER BY Q_SYSPARAM.sysparamobs,Q_SYSPARAM.sysparamcod" onselected="dsctrl000017_Selected" >
<SelectParameters>
<asp:ControlParameter  Name="ctrl000013" ControlID="ctrl000013" ConvertEmptyStringToNull="True" Direction="InputOutput" Type="String" Size="200" PropertyName="Text" />
<asp:ControlParameter  Name="ctrl000014" ControlID="ctrl000014" ConvertEmptyStringToNull="True" Direction="InputOutput" Type="Int32" PropertyName="Text" />
<asp:ControlParameter  Name="ctrl000015" ControlID="ctrl000015" ConvertEmptyStringToNull="True" Direction="InputOutput" Type="String" Size="50" PropertyName="Text" />
</SelectParameters>
</asp:SqlDataSource>
				<br /><asp:Label  runat="server" ID="lblerror" CssClass="error" width="100%" />	</table>
<!-- End -->
</ContentTemplate>
</asp:UpdatePanel>
<asp:UpdateProgress  runat="server" AssociatedUpdatePanelID="updupdpnlKeys" >
<ProgressTemplate><span  class="ajaxupd" ><div style="position:absolute;top:2px;left:50%; border-color:#333333; border:1; border-style:solid; background-color:#FFFFFF;padding:3px;"><image src="imagenes/ajaxupdate.gif" border=0 />Actualizando</div></span></ProgressTemplate>
</asp:UpdateProgress>
</td></tr></table>
</asp:Content>

