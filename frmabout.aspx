<%@ Page Language="VB" CodeFile="frmabout.aspx.vb" MasterPageFile="general.master" AutoEventWireup="True" Inherits="frmabout" Title="Informacion del sistema" %>
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
<tr><td  style="width:32px;" colspan="1" ><img  src="imagenes/icon-00000001.png" width="32px" height="32px" alt="Informacion del sistema" hspace="4"/></td>
<td  style="text-align:left;" colspan="1" ><span class="form-title">Informacion del sistema</span></td></tr>
</table>
</td></tr>
<tr><td  colspan="1" >									<!-- Init -->
<table id="Actions" class="hide_print" style="background-color:#F0F0F0;" width="100%">
<tr>
<td  class="search-title" colspan="5" >									<tr><td  class="search-title" style="vertical-align:top;width:15%" colspan="1" >										Version de Hercules Schema										</td>										<td  class="search-title" style="vertical-align:top;width:35%" colspan="1" >										<asp:Label  runat="server" ID="ctrl000010" CssClass="form-control-read" width="100%" Text='73' />										</td><td  class="search-title" style="vertical-align:top;width:15%" colspan="1" >									Cliente									</td>									<td  class="search-title" style="vertical-align:top;width:35%" colspan="1" >									<asp:Label  runat="server" ID="ctrl000009" CssClass="form-control-read" width="100%" Text='_intelimedia | Documentación' />									</td>								</tr>							<tr><td  class="search-title" style="vertical-align:top;width:15%" colspan="1" >								Version del sistema								</td>								<td  class="search-title" style="vertical-align:top;width:35%" colspan="1" >								<asp:Label  runat="server" ID="ctrl000008" CssClass="form-control-read" width="100%" Text='244' />								</td><td  class="search-title" style="vertical-align:top;width:15%" colspan="1" >							Fecha de version							</td>							<td  class="search-title" style="vertical-align:top;width:35%" colspan="1" >							<asp:Label  runat="server" ID="ctrl000007" CssClass="form-control-read" width="100%" Text='10/11/2011 10:23:37 p.m.' />							</td>						</tr>						<asp:Button  runat="server" ID="ctrl000006" Text="Nuevo" CssClass="boton-acciones" CausesValidation="False" OnLoad="ctrl000006_Load" PostBackURL="frmabout_det.aspx?_mode_=1&_closea_=0" />					<asp:Button  runat="server" ID="ctrl000005" Text="Refrescar" CssClass="boton-acciones" CausesValidation="True" ValidationGroup="vg" />		</td></tr>
</table>
<!-- End -->
</td></tr><tr><td  colspan="1" >					<!-- Init -->
<table id="Keys" style="background-color:#F0F0F0;" width="100%">
					<tr>						<td  class="search-table" style="vertical-align:top;" colspan="4" >						<asp:UpdatePanel  ID="updupdctrl000013" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="True" >
<Triggers>
  <asp:AsyncPostBackTrigger  ControlID="ctrl000013" EventName="PageIndexChanging" />
</Triggers>
<ContentTemplate>

<asp:GridView  runat="server" ID="ctrl000013" AllowPaging="True" AllowSorting="True" AutoGenerateColumns="False" CssClass="form-control" PageSize="50"
 GridLines="None" ShowHeader="True" ShowFooter="True" Width="100%" UseAccessibleHeader="False" DataKeyNames="sysparamcod" CellPadding="2" DataSourceID="dsctrl000013" onRowCreated="ctrl000013_RowCreated" >
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
<asp:TemplateField   ItemStyle-CssClass="tabla-celda">
<ItemStyle  HorizontalAlign="Left" Width="12px" CssClass="searchresult-table-cell" />
<ItemTemplate>
<asp:Image  runat="server" ID="rowctrl000013imageimg" Width="12px" GenerateEmptyAlternateText="True" ImageURL="imagenes/iconParametro.png" BorderWidth="0" AlternateText="Imagen no disp." />
</ItemTemplate>
</asp:TemplateField>
<asp:TemplateField   AccessibleHeaderText="Nombre" HeaderText="Nombre" SortExpression="sysparamobs" ItemStyle-CssClass="tabla-celda">
<ItemStyle  HorizontalAlign="Left" CssClass="searchresult-table-cell" />
<ItemTemplate>
<asp:LinkButton runat="server" ID="ctrl000013colitem1view" Text='<%# Replace(Eval("sysparamobs").ToString,Chr(10),"<br />") %>' CssClass="form-control-read" OnDataBinding="ctrl000013colitem1view_DataBinding" CausesValidation="False" OnClientClick='<%# "javascript:if (window.showModalDialog) {window.showModalDialog(" & chr(39) & "frmsysparam_det.aspx?_mode_=0&_closea_=1&param1=" & Eval("sysparamcod") & "&timestamp=" & chr(39) & " + new Date().getTime() + " & chr(39) & "" & chr(39) & ", " & chr(39) & "" & chr(39) & ", " & chr(39) & "menubar=no;resizable=yes;toolbar=no;status=no;center=yes;dialogWidth=750px;dialogHeight=570px;" & chr(39) & ");} else {window.open(" & chr(39) & "frmsysparam_det.aspx?_mode_=0&_closea_=1&param1=" & Eval("sysparamcod") & "&timestamp=" & chr(39) & " + new Date().getTime() + " & chr(39) & "" & chr(39) & ", " & chr(39) & "" & chr(39) & ", " & chr(39) & "modal=yes;directories=no;menubar=no;resizable=yes;toolbar=no;status=no;center=yes;dialogWidth=750px;dialogHeight=570px;" & chr(39) & ");};return false;" %>' PostBackUrl='<%# "javascript:if (window.showModalDialog) {window.showModalDialog(" & chr(39) & "frmsysparam_det.aspx?_mode_=0&_closea_=1&param1=" & Eval("sysparamcod") & "&timestamp=" & chr(39) & " + new Date().getTime() + " & chr(39) & "" & chr(39) & ", " & chr(39) & "" & chr(39) & ", " & chr(39) & "menubar=no;resizable=yes;toolbar=no;status=no;center=yes;dialogWidth=750px;dialogHeight=570px;" & chr(39) & ");} else {window.open(" & chr(39) & "frmsysparam_det.aspx?_mode_=0&_closea_=1&param1=" & Eval("sysparamcod") & "&timestamp=" & chr(39) & " + new Date().getTime() + " & chr(39) & "" & chr(39) & ", " & chr(39) & "" & chr(39) & ", " & chr(39) & "modal=yes;directories=no;menubar=no;resizable=yes;toolbar=no;status=no;center=yes;dialogWidth=750px;dialogHeight=570px;" & chr(39) & ");};return false;" %>' />
</ItemTemplate>
</asp:TemplateField>
<asp:HyperLinkField  HeaderText="Ver" DataNavigateUrlFields="sysparamcod" DataNavigateUrlFormatString="frmabout_det.aspx?_mode_=0&_closea_=0&param1={0}" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="16" Text="&lt;img src=./imagenes/actview.png border=0&gt alt='Ver' width='16' ;" >
<HeaderStyle  CssClass="tabla-titulo" />
</asp:HyperLinkField>
<asp:TemplateField   AccessibleHeaderText="Editar" HeaderText="Editar" ItemStyle-CssClass="tabla-celda">
<ItemStyle  HorizontalAlign="Left" Width="16px" />
<ItemTemplate>
<asp:ImageButton runat="server" ID="cmdctrl000013edit" ImageURL="./imagenes/actmod.png" Width="16px" Height="16px" OnDataBinding="cmdctrl000013edit_DataBinding" CausesValidation="False" PostBackURL='<%# "frmabout_det.aspx?_mode_=2&_closea_=0&param1=" & Eval("sysparamcod") & "" %>' /></ItemTemplate>
</asp:TemplateField>
<asp:TemplateField   AccessibleHeaderText="Borrar" HeaderText="Borrar" ItemStyle-CssClass="tabla-celda">
<ItemStyle  HorizontalAlign="Left" Width="16px" />
<ItemTemplate>
<asp:ImageButton runat="server" ID="cmdctrl000013delete" ImageURL="./imagenes/actdel.png" Width="16px" Height="16px" OnDataBinding="cmdctrl000013delete_DataBinding" CausesValidation="False" PostBackURL='<%# "frmabout_det.aspx?_mode_=3&_closea_=0&param1=" & Eval("sysparamcod") & "" %>' /></ItemTemplate>
</asp:TemplateField>
<asp:TemplateField   AccessibleHeaderText="Copiar" HeaderText="Copiar" ItemStyle-CssClass="tabla-celda">
<ItemStyle  HorizontalAlign="Left" Width="16px" />
<ItemTemplate>
<asp:ImageButton runat="server" ID="cmdctrl000013copy" ImageURL="./imagenes/actcopy.png" Width="16px" Height="16px" OnDataBinding="cmdctrl000013copy_DataBinding" CausesValidation="False" PostBackURL='<%# "frmabout_det.aspx?_mode_=25&_closea_=0&param1=" & Eval("sysparamcod") & "" %>' /></ItemTemplate>
</asp:TemplateField>
</Columns>
</asp:GridView>
</ContentTemplate>
</asp:UpdatePanel>
<asp:UpdateProgress  runat="server" AssociatedUpdatePanelID="updupdctrl000013" >
<ProgressTemplate><span  class="ajaxupd" ><div style="position:absolute;top:2px;left:50%; border-color:#333333; border:1; border-style:solid; background-color:#FFFFFF;padding:3px;"><image src="imagenes/ajaxupdate.gif" border=0 />Actualizando</div></span></ProgressTemplate>
</asp:UpdateProgress>

					<asp:SqlDataSource  runat="server" ID="dsctrl000013" ProviderName="System.Data.SqlClient" CancelSelectOnNullParameter="False" onInit="dsctrl000013_Init" >
</asp:SqlDataSource>
				<br /><asp:Label  runat="server" ID="lblerror" CssClass="error" width="100%" />	</table>
<!-- End -->
</td></tr></table>
</asp:Content>

