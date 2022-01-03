<%@ Page Language="VB" CodeFile="hrckya.aspx.vb" MasterPageFile="general.master" AutoEventWireup="True" Inherits="hrckya" Title="Claves asimétricas" %>
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
<tr><td  style="width:32px;" colspan="1" ><img  src="imagenes/iconClave asimétrica.png" width="32px" height="32px" alt="Claves asimétricas" hspace="4"/></td>
<td  style="text-align:left;" colspan="1" ><span class="form-title">Claves asimétricas</span></td></tr>
</table>
</td></tr>
<tr><td  colspan="1" >					<asp:UpdatePanel  ID="updupdpnlSearch" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="True" >
<Triggers>
  <asp:AsyncPostBackTrigger  ControlID="ctrl000013" EventName="Click" />
  <asp:AsyncPostBackTrigger  ControlID="ctrl000011" EventName="Click" />
</Triggers>
<ContentTemplate>
<!-- Init Filtros de busqueda -->
<table id="Search" class="hide_print" style="background-color:#F0F0F0;" width="100%">
<tr>
<td  class="search-title" colspan="5" >Filtros de busqueda</td></tr>
					<tr><td  class="search-item-title" style="vertical-align:top;width:15%" colspan="1" >						Descripcion						</td>						<td  class="search-table" style="vertical-align:top;width:35%" colspan="1" >						<asp:TextBox  runat="server" ID="ctrl000016" CssClass="form-control" Width="280px" MaxLength="30" Tooltip="Campo100122[]" />
<asp:CompareValidator  runat="server" ID="vcdvalctrl000016" SetFocusOnError="true" CssClass="error" ControltoValidate="ctrl000016" Display="Dynamic" Text="Ingrese un texto" ErrorMessage="Descripcion:Ingrese un texto" Type="String" Operator="DataTypeCheck" ValidationGroup="vg" />
<asp:RegularExpressionValidator  runat="server" ID="vrgvalctrl000016" SetFocusOnError="true" CssClass="error" Display="Dynamic" ControltoValidate="ctrl000016" ValidationExpression="[\w\s-+/*^()!|°$%&=-_,:;.ñÑ~áéíóúÁÉÍÓÚ#}{]{0,30}" Text="No mayor a 30 caracteres. Deben ser letras o numeros." ErrorMessage="Descripcion:No mayor a 30 caracteres. Deben ser letras o numeros." ValidationGroup="vg" />
<ajaxkit:ValidatorCalloutExtender  runat="server" ID="ajaxvrgctrl000016" TargetControlID="vrgvalctrl000016" />
						</td>			</table>
<!-- End Filtros de busqueda -->
</ContentTemplate>
</asp:UpdatePanel>
<asp:UpdateProgress  runat="server" AssociatedUpdatePanelID="updupdpnlSearch" >
<ProgressTemplate><span  class="ajaxupd" ><div style="position:absolute;top:2px;left:50%; border-color:#333333; border:1; border-style:solid; background-color:#FFFFFF;padding:3px;"><image src="imagenes/ajaxupdate.gif" border=0 />Actualizando</div></span></ProgressTemplate>
</asp:UpdateProgress>
</td></tr><tr><td  colspan="1" >							<asp:UpdatePanel  ID="updupdpnlActions" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="True" >
<Triggers>
  <asp:AsyncPostBackTrigger  ControlID="ctrl000011" EventName="Click" />
</Triggers>
<ContentTemplate>
<!-- Init -->
<table id="Actions" class="hide_print" style="background-color:#F0F0F0;" width="100%">
<tr>
<td  class="search-title" colspan="5" >								<asp:Button  runat="server" ID="ctrl000014" Text="Buscar" CssClass="boton-acciones" CausesValidation="True" OnClick="ctrl000014_Click" ValidationGroup="vg" />							<asp:Button  runat="server" ID="ctrl000013" Text="Limpiar filtros" CssClass="boton-acciones" CausesValidation="True" OnClick="ctrl000013_Click" ValidationGroup="vg" />						<asp:Button  runat="server" ID="ctrl000012" Text="Nuevo" CssClass="boton-acciones" CausesValidation="False" OnLoad="ctrl000012_Load" PostBackURL="hrckya_det.aspx?_mode_=1&_closea_=0" />					<asp:Button  runat="server" ID="ctrl000011" Text="Refrescar" CssClass="boton-acciones" CausesValidation="True" ValidationGroup="vg" />		</td></tr>
</table>
<!-- End -->
</ContentTemplate>
</asp:UpdatePanel>
<asp:UpdateProgress  runat="server" AssociatedUpdatePanelID="updupdpnlActions" >
<ProgressTemplate><span  class="ajaxupd" ><div style="position:absolute;top:2px;left:50%; border-color:#333333; border:1; border-style:solid; background-color:#FFFFFF;padding:3px;"><image src="imagenes/ajaxupdate.gif" border=0 />Actualizando</div></span></ProgressTemplate>
</asp:UpdateProgress>
</td></tr><tr><td  colspan="1" >					<asp:UpdatePanel  ID="updupdpnlKeys" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="True" >
<Triggers>
  <asp:AsyncPostBackTrigger  ControlID="ctrl000014" EventName="Click" />
  <asp:AsyncPostBackTrigger  ControlID="ctrl000013" EventName="Click" />
  <asp:AsyncPostBackTrigger  ControlID="ctrl000011" EventName="Click" />
</Triggers>
<ContentTemplate>
<!-- Init -->
<table id="Keys" style="background-color:#F0F0F0;" width="100%">
					<tr>						<td  class="search-table" style="vertical-align:top;" colspan="4" >						<asp:UpdatePanel  ID="updupdpnlctrl000018" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="True" >
<Triggers>
  <asp:AsyncPostBackTrigger  ControlID="ctrl000014" EventName="Click" />
  <asp:AsyncPostBackTrigger  ControlID="ctrl000013" EventName="Click" />
</Triggers>
<ContentTemplate>
<asp:UpdatePanel  ID="updupdctrl000018" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="True" >
<Triggers>
  <asp:AsyncPostBackTrigger  ControlID="ctrl000018" EventName="PageIndexChanging" />
</Triggers>
<ContentTemplate>

<asp:GridView  runat="server" ID="ctrl000018" AllowPaging="True" AllowSorting="True" AutoGenerateColumns="False" CssClass="form-control" PageSize="50"
 GridLines="None" ShowHeader="True" ShowFooter="True" Width="100%" UseAccessibleHeader="False" DataKeyNames="kyacod" CellPadding="2" DataSourceID="dsctrl000018" onRowCreated="ctrl000018_RowCreated" >
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
<asp:Image  runat="server" ID="rowctrl000018imageimg" Width="12px" GenerateEmptyAlternateText="True" ImageURL="imagenes/iconClave asimétrica.png" BorderWidth="0" AlternateText="Imagen no disp." />
</ItemTemplate>
</asp:TemplateField>
<asp:TemplateField   AccessibleHeaderText="Descripcion" HeaderText="Descripcion" SortExpression="kyadsc" ItemStyle-CssClass="tabla-celda" HeaderStyle-CssClass="tabla-titulo" FooterStyle-CssClass="tabla-celda">
<ItemStyle  HorizontalAlign="Left" CssClass="searchresult-table-cell" />
<ItemTemplate>
<asp:LinkButton runat="server" ID="ctrl000018colitem1view" Text='<%# Eval("kyadsc") %>' CssClass="form-control-read" OnDataBinding="ctrl000018colitem1view_DataBinding" CausesValidation="False" OnClientClick='<%# "javascript:if (window.showModalDialog) {window.showModalDialog(" & chr(39) & "hrckya_det.aspx?_mode_=0&_closea_=1&isDlg=1&param1=" & Eval("kyacod") & "&timestamp=" & chr(39) & " + new Date().getTime() + " & chr(39) & "" & chr(39) & ", " & chr(39) & "" & chr(39) & ", " & chr(39) & "menubar=no;resizable=yes;toolbar=no;status=no;center=yes;dialogWidth=750px;dialogHeight=570px;" & chr(39) & ");} else {window.open(" & chr(39) & "hrckya_det.aspx?_mode_=0&_closea_=1&isDlg=1&param1=" & Eval("kyacod") & "&timestamp=" & chr(39) & " + new Date().getTime() + " & chr(39) & "" & chr(39) & ", " & chr(39) & "" & chr(39) & ", " & chr(39) & "modal=yes;directories=no;menubar=no;resizable=yes;toolbar=no;status=no;center=yes;dialogWidth=750px;dialogHeight=570px;" & chr(39) & ");};return false;" %>' PostBackUrl='<%# "javascript:if (window.showModalDialog) {window.showModalDialog(" & chr(39) & "hrckya_det.aspx?_mode_=0&_closea_=1&isDlg=1&param1=" & Eval("kyacod") & "&timestamp=" & chr(39) & " + new Date().getTime() + " & chr(39) & "" & chr(39) & ", " & chr(39) & "" & chr(39) & ", " & chr(39) & "menubar=no;resizable=yes;toolbar=no;status=no;center=yes;dialogWidth=750px;dialogHeight=570px;" & chr(39) & ");} else {window.open(" & chr(39) & "hrckya_det.aspx?_mode_=0&_closea_=1&isDlg=1&param1=" & Eval("kyacod") & "&timestamp=" & chr(39) & " + new Date().getTime() + " & chr(39) & "" & chr(39) & ", " & chr(39) & "" & chr(39) & ", " & chr(39) & "modal=yes;directories=no;menubar=no;resizable=yes;toolbar=no;status=no;center=yes;dialogWidth=750px;dialogHeight=570px;" & chr(39) & ");};return false;" %>' />
</ItemTemplate>
</asp:TemplateField>
<asp:HyperLinkField  HeaderText="Ver" DataNavigateUrlFields="kyacod" DataNavigateUrlFormatString="hrckya_det.aspx?_mode_=0&_closea_=0&param1={0}" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="16" Text="&lt;img src=./imagenes/actview.png border=0&gt alt='Ver' width='16' ;" >
<HeaderStyle  CssClass="tabla-titulo" />
</asp:HyperLinkField>
<asp:TemplateField   AccessibleHeaderText="Editar" HeaderText="Editar" ItemStyle-CssClass="tabla-celda" HeaderStyle-CssClass="tabla-titulo" FooterStyle-CssClass="tabla-celda">
<ItemStyle  HorizontalAlign="Left" Width="16px" />
<ItemTemplate>
<asp:ImageButton runat="server" ID="cmdctrl000018edit" ImageURL="./imagenes/actmod.png" Width="16px" Height="16px" CausesValidation="False" PostBackURL='<%# "hrckya_det.aspx?_mode_=2&_closea_=0&param1=" & Eval("kyacod") & "" %>' /></ItemTemplate>
</asp:TemplateField>
<asp:TemplateField   AccessibleHeaderText="Copiar" HeaderText="Copiar" ItemStyle-CssClass="tabla-celda" HeaderStyle-CssClass="tabla-titulo" FooterStyle-CssClass="tabla-celda">
<ItemStyle  HorizontalAlign="Left" Width="16px" />
<ItemTemplate>
<asp:ImageButton runat="server" ID="cmdctrl000018copy" ImageURL="./imagenes/actcopy.png" Width="16px" Height="16px" CausesValidation="False" PostBackURL='<%# "hrckya_det.aspx?_mode_=25&_closea_=0&param1=" & Eval("kyacod") & "" %>' /></ItemTemplate>
</asp:TemplateField>
</Columns>
</asp:GridView>
</ContentTemplate>
</asp:UpdatePanel>
<asp:UpdateProgress  runat="server" AssociatedUpdatePanelID="updupdctrl000018" >
<ProgressTemplate><span  class="ajaxupd" ><div style="position:absolute;top:2px;left:50%; border-color:#333333; border:1; border-style:solid; background-color:#FFFFFF;padding:3px;"><image src="imagenes/ajaxupdate.gif" border=0 />Actualizando</div></span></ProgressTemplate>
</asp:UpdateProgress>

</ContentTemplate>
</asp:UpdatePanel>
<asp:UpdateProgress  runat="server" AssociatedUpdatePanelID="updupdpnlctrl000018" >
<ProgressTemplate><span  class="ajaxupd" ><div style="position:absolute;top:2px;left:50%; border-color:#333333; border:1; border-style:solid; background-color:#FFFFFF;padding:3px;"><image src="imagenes/ajaxupdate.gif" border=0 />Actualizando</div></span></ProgressTemplate>
</asp:UpdateProgress>
					<asp:SqlDataSource  runat="server" ID="dsctrl000018" ProviderName="System.Data.SqlClient" CancelSelectOnNullParameter="False" onInit="dsctrl000018_Init" SelectCommandType="Text"
 SelectCommand="SELECT Q_KYA.kyacod,Q_KYA.kyadsc FROM Q_KYA   WHERE ((Q_KYA.kyadsc LIKE '%' + @ctrl000016 + '%' OR @ctrl000016  IS NULL) AND @qsidcod_list ) AND Q_KYA.kyacod >= 1 ORDER BY Q_KYA.kyadsc,Q_KYA.kyacod" onselected="dsctrl000018_Selected" onSelecting="dsctrl000018_Selecting" >
<SelectParameters>
<asp:ControlParameter  Name="ctrl000016" ControlID="ctrl000016" ConvertEmptyStringToNull="True" Direction="InputOutput" Type="String" Size="200" PropertyName="Text" />
<asp:Parameter  Name="qsidcod_list" Direction="Input" Type="String" Size="200" />
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

