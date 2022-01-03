<%@ Page Language="VB" CodeFile="frmGrupos.aspx.vb" MasterPageFile="general.master" AutoEventWireup="True" Inherits="frmGrupos" Title="Grupos" %>
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
<td  style="text-align:left;" colspan="1" ><span class="form-title">Grupos</span></td></tr>
</table>
</td></tr>
<tr><td  colspan="1" >					<asp:UpdatePanel  ID="updupdpnlSearch" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="True" >
<Triggers>
  <asp:AsyncPostBackTrigger  ControlID="ctrl000018" EventName="Click" />
  <asp:AsyncPostBackTrigger  ControlID="ctrl000016" EventName="Click" />
</Triggers>
<ContentTemplate>
<!-- Init Filtros de busqueda -->
<table id="Search" class="hide_print" style="background-color:#F0F0F0;" width="100%">
<tr>
<td  class="search-title" colspan="5" >Filtros de busqueda</td></tr>
					<tr><td  class="search-item-title" style="vertical-align:top;width:15%" colspan="1" >						Descripción						</td>						<td  class="search-table" style="vertical-align:top;width:35%" colspan="1" >						<asp:TextBox  runat="server" ID="ctrl000021" CssClass="form-control" Width="280px" MaxLength="30" Tooltip="Campo100047[]" />
<asp:CompareValidator  runat="server" ID="vcdvalctrl000021" SetFocusOnError="true" CssClass="error" ControltoValidate="ctrl000021" Display="Dynamic" Text="Ingrese un texto" ErrorMessage="Descripción:Ingrese un texto" Type="String" Operator="DataTypeCheck" ValidationGroup="vg" />
<asp:RegularExpressionValidator  runat="server" ID="vrgvalctrl000021" SetFocusOnError="true" CssClass="error" Display="Dynamic" ControltoValidate="ctrl000021" ValidationExpression="[\w\s-+/*^()!|°$%&=-_,:;.ñÑ~áéíóúÁÉÍÓÚ#}{]{0,30}" Text="No mayor a 30 caracteres. Deben ser letras o numeros." ErrorMessage="Descripción:No mayor a 30 caracteres. Deben ser letras o numeros." ValidationGroup="vg" />
<ajaxkit:ValidatorCalloutExtender  runat="server" ID="ajaxvrgctrl000021" TargetControlID="vrgvalctrl000021" />
						</td>			</table>
<!-- End Filtros de busqueda -->
</ContentTemplate>
</asp:UpdatePanel>
<asp:UpdateProgress  runat="server" AssociatedUpdatePanelID="updupdpnlSearch" >
<ProgressTemplate><span  class="ajaxupd" ><div style="position:absolute;top:2px;left:50%; border-color:#333333; border:1; border-style:solid; background-color:#FFFFFF;padding:3px;"><image src="imagenes/ajaxupdate.gif" border=0 />Actualizando</div></span></ProgressTemplate>
</asp:UpdateProgress>
</td></tr><tr><td  colspan="1" >							<asp:UpdatePanel  ID="updupdpnlActions" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="True" >
<Triggers>
  <asp:AsyncPostBackTrigger  ControlID="ctrl000016" EventName="Click" />
</Triggers>
<ContentTemplate>
<!-- Init -->
<table id="Actions" class="hide_print" style="background-color:#F0F0F0;" width="100%">
<tr>
<td  class="search-title" colspan="5" >								<asp:Button  runat="server" ID="ctrl000019" Text="Buscar" CssClass="boton-acciones" CausesValidation="True" OnClick="ctrl000019_Click" ValidationGroup="vg" />							<asp:Button  runat="server" ID="ctrl000018" Text="Limpiar filtros" CssClass="boton-acciones" CausesValidation="True" OnClick="ctrl000018_Click" ValidationGroup="vg" />						<asp:Button  runat="server" ID="ctrl000017" Text="Nuevo" CssClass="boton-acciones" CausesValidation="False" OnLoad="ctrl000017_Load" PostBackURL="frmGrupos_det.aspx?_mode_=1&_closea_=0" />					<asp:Button  runat="server" ID="ctrl000016" Text="Refrescar" CssClass="boton-acciones" CausesValidation="True" ValidationGroup="vg" />		</td></tr>
</table>
<!-- End -->
</ContentTemplate>
</asp:UpdatePanel>
<asp:UpdateProgress  runat="server" AssociatedUpdatePanelID="updupdpnlActions" >
<ProgressTemplate><span  class="ajaxupd" ><div style="position:absolute;top:2px;left:50%; border-color:#333333; border:1; border-style:solid; background-color:#FFFFFF;padding:3px;"><image src="imagenes/ajaxupdate.gif" border=0 />Actualizando</div></span></ProgressTemplate>
</asp:UpdateProgress>
</td></tr><tr><td  colspan="1" >					<asp:UpdatePanel  ID="updupdpnlKeys" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="True" >
<Triggers>
  <asp:AsyncPostBackTrigger  ControlID="ctrl000019" EventName="Click" />
  <asp:AsyncPostBackTrigger  ControlID="ctrl000018" EventName="Click" />
  <asp:AsyncPostBackTrigger  ControlID="ctrl000016" EventName="Click" />
</Triggers>
<ContentTemplate>
<!-- Init -->
<table id="Keys" style="background-color:#F0F0F0;" width="100%">
					<tr>						<td  class="search-table" style="vertical-align:top;" colspan="4" >						<asp:UpdatePanel  ID="updupdpnlctrl000023" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="True" >
<Triggers>
  <asp:AsyncPostBackTrigger  ControlID="ctrl000019" EventName="Click" />
  <asp:AsyncPostBackTrigger  ControlID="ctrl000018" EventName="Click" />
</Triggers>
<ContentTemplate>
<asp:UpdatePanel  ID="updupdctrl000023" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="True" >
<Triggers>
  <asp:AsyncPostBackTrigger  ControlID="ctrl000023" EventName="PageIndexChanging" />
</Triggers>
<ContentTemplate>

<asp:GridView  runat="server" ID="ctrl000023" AllowPaging="True" AllowSorting="True" AutoGenerateColumns="False" CssClass="form-control" PageSize="50"
 GridLines="None" ShowHeader="True" ShowFooter="True" Width="100%" UseAccessibleHeader="False" DataKeyNames="grpcod" CellPadding="2" DataSourceID="dsctrl000023" onRowCreated="ctrl000023_RowCreated" >
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
<asp:Image  runat="server" ID="rowctrl000023imageimg" Width="12px" GenerateEmptyAlternateText="True" ImageURL="imagenes/iconGrupo.png" BorderWidth="0" AlternateText="Imagen no disp." />
</ItemTemplate>
</asp:TemplateField>
<asp:TemplateField   AccessibleHeaderText="Descripción" HeaderText="Descripción" SortExpression="grpdsc" ItemStyle-CssClass="tabla-celda" HeaderStyle-CssClass="tabla-titulo" FooterStyle-CssClass="tabla-celda">
<ItemStyle  HorizontalAlign="Left" CssClass="searchresult-table-cell" />
<ItemTemplate>
<asp:LinkButton runat="server" ID="ctrl000023colitem1view" Text='<%# Eval("grpdsc") %>' CssClass="form-control-read" OnDataBinding="ctrl000023colitem1view_DataBinding" CausesValidation="False" OnClientClick='<%# "javascript:if (window.showModalDialog) {window.showModalDialog(" & chr(39) & "frmGrupos_det.aspx?_mode_=0&_closea_=1&isDlg=1&param1=" & Eval("grpcod") & "&timestamp=" & chr(39) & " + new Date().getTime() + " & chr(39) & "" & chr(39) & ", " & chr(39) & "" & chr(39) & ", " & chr(39) & "menubar=no;resizable=yes;toolbar=no;status=no;center=yes;dialogWidth=750px;dialogHeight=570px;" & chr(39) & ");} else {window.open(" & chr(39) & "frmGrupos_det.aspx?_mode_=0&_closea_=1&isDlg=1&param1=" & Eval("grpcod") & "&timestamp=" & chr(39) & " + new Date().getTime() + " & chr(39) & "" & chr(39) & ", " & chr(39) & "" & chr(39) & ", " & chr(39) & "modal=yes;directories=no;menubar=no;resizable=yes;toolbar=no;status=no;center=yes;dialogWidth=750px;dialogHeight=570px;" & chr(39) & ");};return false;" %>' PostBackUrl='<%# "javascript:if (window.showModalDialog) {window.showModalDialog(" & chr(39) & "frmGrupos_det.aspx?_mode_=0&_closea_=1&isDlg=1&param1=" & Eval("grpcod") & "&timestamp=" & chr(39) & " + new Date().getTime() + " & chr(39) & "" & chr(39) & ", " & chr(39) & "" & chr(39) & ", " & chr(39) & "menubar=no;resizable=yes;toolbar=no;status=no;center=yes;dialogWidth=750px;dialogHeight=570px;" & chr(39) & ");} else {window.open(" & chr(39) & "frmGrupos_det.aspx?_mode_=0&_closea_=1&isDlg=1&param1=" & Eval("grpcod") & "&timestamp=" & chr(39) & " + new Date().getTime() + " & chr(39) & "" & chr(39) & ", " & chr(39) & "" & chr(39) & ", " & chr(39) & "modal=yes;directories=no;menubar=no;resizable=yes;toolbar=no;status=no;center=yes;dialogWidth=750px;dialogHeight=570px;" & chr(39) & ");};return false;" %>' />
</ItemTemplate>
</asp:TemplateField>
<asp:HyperLinkField  HeaderText="Ver" DataNavigateUrlFields="grpcod" DataNavigateUrlFormatString="frmGrupos_det.aspx?_mode_=0&_closea_=0&param1={0}" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="16" Text="&lt;img src=./imagenes/actview.png border=0&gt alt='Ver' width='16' ;" >
<HeaderStyle  CssClass="tabla-titulo" />
</asp:HyperLinkField>
<asp:TemplateField   AccessibleHeaderText="Editar" HeaderText="Editar" ItemStyle-CssClass="tabla-celda" HeaderStyle-CssClass="tabla-titulo" FooterStyle-CssClass="tabla-celda">
<ItemStyle  HorizontalAlign="Left" Width="16px" />
<ItemTemplate>
<asp:ImageButton runat="server" ID="cmdctrl000023edit" ImageURL="./imagenes/actmod.png" Width="16px" Height="16px" CausesValidation="False" PostBackURL='<%# "frmGrupos_det.aspx?_mode_=2&_closea_=0&param1=" & Eval("grpcod") & "" %>' /></ItemTemplate>
</asp:TemplateField>
<asp:TemplateField   AccessibleHeaderText="Borrar" HeaderText="Borrar" ItemStyle-CssClass="tabla-celda" HeaderStyle-CssClass="tabla-titulo" FooterStyle-CssClass="tabla-celda">
<ItemStyle  HorizontalAlign="Left" Width="16px" />
<ItemTemplate>
<asp:ImageButton runat="server" ID="cmdctrl000023delete" ImageURL="./imagenes/actdel.png" Width="16px" Height="16px" CausesValidation="False" PostBackURL='<%# "frmGrupos_det.aspx?_mode_=3&_closea_=0&param1=" & Eval("grpcod") & "" %>' /></ItemTemplate>
</asp:TemplateField>
<asp:TemplateField   AccessibleHeaderText="Copiar" HeaderText="Copiar" ItemStyle-CssClass="tabla-celda" HeaderStyle-CssClass="tabla-titulo" FooterStyle-CssClass="tabla-celda">
<ItemStyle  HorizontalAlign="Left" Width="16px" />
<ItemTemplate>
<asp:ImageButton runat="server" ID="cmdctrl000023copy" ImageURL="./imagenes/actcopy.png" Width="16px" Height="16px" CausesValidation="False" PostBackURL='<%# "frmGrupos_det.aspx?_mode_=25&_closea_=0&param1=" & Eval("grpcod") & "" %>' /></ItemTemplate>
</asp:TemplateField>
</Columns>
</asp:GridView>
</ContentTemplate>
</asp:UpdatePanel>
<asp:UpdateProgress  runat="server" AssociatedUpdatePanelID="updupdctrl000023" >
<ProgressTemplate><span  class="ajaxupd" ><div style="position:absolute;top:2px;left:50%; border-color:#333333; border:1; border-style:solid; background-color:#FFFFFF;padding:3px;"><image src="imagenes/ajaxupdate.gif" border=0 />Actualizando</div></span></ProgressTemplate>
</asp:UpdateProgress>

</ContentTemplate>
</asp:UpdatePanel>
<asp:UpdateProgress  runat="server" AssociatedUpdatePanelID="updupdpnlctrl000023" >
<ProgressTemplate><span  class="ajaxupd" ><div style="position:absolute;top:2px;left:50%; border-color:#333333; border:1; border-style:solid; background-color:#FFFFFF;padding:3px;"><image src="imagenes/ajaxupdate.gif" border=0 />Actualizando</div></span></ProgressTemplate>
</asp:UpdateProgress>
					<asp:SqlDataSource  runat="server" ID="dsctrl000023" ProviderName="System.Data.SqlClient" CancelSelectOnNullParameter="False" onInit="dsctrl000023_Init" SelectCommandType="Text"
 SelectCommand="SELECT Q_SECPGRP.grpcod,Q_SECPGRP.grpdsc FROM Q_SECPGRP   WHERE ((Q_SECPGRP.grpdsc LIKE '%' + @ctrl000021 + '%' OR @ctrl000021  IS NULL) AND @qsidcod_list ) AND Q_SECPGRP.grpcod >= 1 ORDER BY Q_SECPGRP.grpdsc,Q_SECPGRP.grpcod" onselected="dsctrl000023_Selected" onSelecting="dsctrl000023_Selecting" >
<SelectParameters>
<asp:ControlParameter  Name="ctrl000021" ControlID="ctrl000021" ConvertEmptyStringToNull="True" Direction="InputOutput" Type="String" Size="250" PropertyName="Text" />
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

