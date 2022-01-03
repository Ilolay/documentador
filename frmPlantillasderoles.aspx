<%@ Page Language="VB" CodeFile="frmPlantillasderoles.aspx.vb" MasterPageFile="general.master" AutoEventWireup="True" Inherits="frmPlantillasderoles" Title="Plantillas de roles" %>
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
<td  style="text-align:left;" colspan="1" ><span class="form-title">Plantillas de roles</span></td></tr>
</table>
</td></tr>
<tr><td  colspan="1" >					<!-- Init -->
<table id="Actions" class="hide_print" style="background-color:#F0F0F0;" width="100%">
<tr>
<td  class="search-title" colspan="5" >						<asp:Button  runat="server" ID="ctrl000012" Text="Nuevo" CssClass="boton-acciones" CausesValidation="False" OnLoad="ctrl000012_Load" PostBackURL="frmPlantillasderoles_det.aspx?_mode_=1&_closea_=0" />					<asp:Button  runat="server" ID="ctrl000011" Text="Refrescar" CssClass="boton-acciones" CausesValidation="True" ValidationGroup="vg" />		</td></tr>
</table>
<!-- End -->
</td></tr><tr><td  colspan="1" >					<!-- Init -->
<table id="Keys" style="background-color:#F0F0F0;" width="100%">
					<tr>						<td  class="search-table" style="vertical-align:top;" colspan="4" >						<asp:UpdatePanel  ID="updupdctrl000015" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="True" >
<Triggers>
  <asp:AsyncPostBackTrigger  ControlID="ctrl000015" EventName="PageIndexChanging" />
</Triggers>
<ContentTemplate>

<asp:GridView  runat="server" ID="ctrl000015" AllowPaging="True" AllowSorting="True" AutoGenerateColumns="False" CssClass="form-control" PageSize="50"
 GridLines="None" ShowHeader="True" ShowFooter="True" Width="100%" UseAccessibleHeader="False" DataKeyNames="cod" CellPadding="2" DataSourceID="dsctrl000015" onRowCreated="ctrl000015_RowCreated" >
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
<asp:Image  runat="server" ID="rowctrl000015imageimg" Width="12px" GenerateEmptyAlternateText="True" ImageURL="imagenes/icon00000035.png" BorderWidth="0" AlternateText="Imagen no disp." />
</ItemTemplate>
</asp:TemplateField>
<asp:TemplateField   AccessibleHeaderText="Descripción" HeaderText="Descripción" SortExpression="dsc" ItemStyle-CssClass="tabla-celda" HeaderStyle-CssClass="tabla-titulo" FooterStyle-CssClass="tabla-celda">
<ItemStyle  HorizontalAlign="Left" CssClass="searchresult-table-cell" />
<ItemTemplate>
<asp:LinkButton runat="server" ID="ctrl000015colitem1view" Text='<%# Eval("dsc") %>' CssClass="form-control-read" OnDataBinding="ctrl000015colitem1view_DataBinding" CausesValidation="False" OnClientClick='<%# "javascript:if (window.showModalDialog) {window.showModalDialog(" & chr(39) & "frmPlantillasderoles_det.aspx?_mode_=0&_closea_=1&isDlg=1&param1=" & Eval("cod") & "&timestamp=" & chr(39) & " + new Date().getTime() + " & chr(39) & "" & chr(39) & ", " & chr(39) & "" & chr(39) & ", " & chr(39) & "menubar=no;resizable=yes;toolbar=no;status=no;center=yes;dialogWidth=750px;dialogHeight=570px;" & chr(39) & ");} else {window.open(" & chr(39) & "frmPlantillasderoles_det.aspx?_mode_=0&_closea_=1&isDlg=1&param1=" & Eval("cod") & "&timestamp=" & chr(39) & " + new Date().getTime() + " & chr(39) & "" & chr(39) & ", " & chr(39) & "" & chr(39) & ", " & chr(39) & "modal=yes;directories=no;menubar=no;resizable=yes;toolbar=no;status=no;center=yes;dialogWidth=750px;dialogHeight=570px;" & chr(39) & ");};return false;" %>' PostBackUrl='<%# "javascript:if (window.showModalDialog) {window.showModalDialog(" & chr(39) & "frmPlantillasderoles_det.aspx?_mode_=0&_closea_=1&isDlg=1&param1=" & Eval("cod") & "&timestamp=" & chr(39) & " + new Date().getTime() + " & chr(39) & "" & chr(39) & ", " & chr(39) & "" & chr(39) & ", " & chr(39) & "menubar=no;resizable=yes;toolbar=no;status=no;center=yes;dialogWidth=750px;dialogHeight=570px;" & chr(39) & ");} else {window.open(" & chr(39) & "frmPlantillasderoles_det.aspx?_mode_=0&_closea_=1&isDlg=1&param1=" & Eval("cod") & "&timestamp=" & chr(39) & " + new Date().getTime() + " & chr(39) & "" & chr(39) & ", " & chr(39) & "" & chr(39) & ", " & chr(39) & "modal=yes;directories=no;menubar=no;resizable=yes;toolbar=no;status=no;center=yes;dialogWidth=750px;dialogHeight=570px;" & chr(39) & ");};return false;" %>' />
</ItemTemplate>
</asp:TemplateField>
<asp:HyperLinkField  HeaderText="Ver" DataNavigateUrlFields="cod" DataNavigateUrlFormatString="frmPlantillasderoles_det.aspx?_mode_=0&_closea_=0&param1={0}" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="16" Text="&lt;img src=./imagenes/actview.png border=0&gt alt='Ver' width='16' ;" >
<HeaderStyle  CssClass="tabla-titulo" />
</asp:HyperLinkField>
<asp:TemplateField   AccessibleHeaderText="Editar" HeaderText="Editar" ItemStyle-CssClass="tabla-celda" HeaderStyle-CssClass="tabla-titulo" FooterStyle-CssClass="tabla-celda">
<ItemStyle  HorizontalAlign="Left" Width="16px" />
<ItemTemplate>
<asp:ImageButton runat="server" ID="cmdctrl000015edit" ImageURL="./imagenes/actmod.png" Width="16px" Height="16px" OnDataBinding="cmdctrl000015edit_DataBinding" CausesValidation="False" PostBackURL='<%# "frmPlantillasderoles_det.aspx?_mode_=2&_closea_=0&param1=" & Eval("cod") & "" %>' /></ItemTemplate>
</asp:TemplateField>
<asp:TemplateField   AccessibleHeaderText="Borrar" HeaderText="Borrar" ItemStyle-CssClass="tabla-celda" HeaderStyle-CssClass="tabla-titulo" FooterStyle-CssClass="tabla-celda">
<ItemStyle  HorizontalAlign="Left" Width="16px" />
<ItemTemplate>
<asp:ImageButton runat="server" ID="cmdctrl000015delete" ImageURL="./imagenes/actdel.png" Width="16px" Height="16px" OnDataBinding="cmdctrl000015delete_DataBinding" CausesValidation="False" PostBackURL='<%# "frmPlantillasderoles_det.aspx?_mode_=3&_closea_=0&param1=" & Eval("cod") & "" %>' /></ItemTemplate>
</asp:TemplateField>
<asp:TemplateField   AccessibleHeaderText="Copiar" HeaderText="Copiar" ItemStyle-CssClass="tabla-celda" HeaderStyle-CssClass="tabla-titulo" FooterStyle-CssClass="tabla-celda">
<ItemStyle  HorizontalAlign="Left" Width="16px" />
<ItemTemplate>
<asp:ImageButton runat="server" ID="cmdctrl000015copy" ImageURL="./imagenes/actcopy.png" Width="16px" Height="16px" OnDataBinding="cmdctrl000015copy_DataBinding" CausesValidation="False" PostBackURL='<%# "frmPlantillasderoles_det.aspx?_mode_=25&_closea_=0&param1=" & Eval("cod") & "" %>' /></ItemTemplate>
</asp:TemplateField>
</Columns>
</asp:GridView>
</ContentTemplate>
</asp:UpdatePanel>
<asp:UpdateProgress  runat="server" AssociatedUpdatePanelID="updupdctrl000015" >
<ProgressTemplate><span  class="ajaxupd" ><div style="position:absolute;top:2px;left:50%; border-color:#333333; border:1; border-style:solid; background-color:#FFFFFF;padding:3px;"><image src="imagenes/ajaxupdate.gif" border=0 />Actualizando</div></span></ProgressTemplate>
</asp:UpdateProgress>

					<asp:SqlDataSource  runat="server" ID="dsctrl000015" ProviderName="System.Data.SqlClient" CancelSelectOnNullParameter="False" onInit="dsctrl000015_Init" SelectCommandType="Text"
 SelectCommand="SELECT DOC_TRO.cod,DOC_TRO.dsc FROM DOC_TRO   WHERE (( @qsidcod_list  AND custom=0 OR CUSTOM IS NULL) AND (DOC_TRO.baja = '' OR DOC_TRO.baja  IS NULL)) AND DOC_TRO.cod >= 1 ORDER BY DOC_TRO.dsc,DOC_TRO.cod" onselected="dsctrl000015_Selected" onSelecting="dsctrl000015_Selecting" >
<SelectParameters>
<asp:Parameter  Name="qsidcod_list" Direction="Input" Type="String" Size="200" />
</SelectParameters>
</asp:SqlDataSource>
				<br /><asp:Label  runat="server" ID="lblerror" CssClass="error" width="100%" />	</table>
<!-- End -->
</td></tr></table>
</asp:Content>

