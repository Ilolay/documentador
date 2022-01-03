<%@ Page Language="VB" CodeFile="frmTiposdeprocesos.aspx.vb" MasterPageFile="general.master" AutoEventWireup="True" Inherits="frmTiposdeprocesos" Title="Tipos de procesos" %>
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
<tr><td  style="width:32px;" colspan="1" ><img  src="imagenes/icon00000003.png" width="32px" height="32px" alt="Tipos de procesos" hspace="4"/></td>
<td  style="text-align:left;" colspan="1" ><span class="form-title">Tipos de procesos</span></td></tr>
</table>
</td></tr>
<tr><td  colspan="1" >					<!-- Init -->
<table id="Actions" class="hide_print" style="background-color:#F0F0F0;" width="100%">
<tr>
<td  class="search-title" colspan="5" >						<asp:Button  runat="server" ID="ctrl000011" Text="Nuevo" CssClass="boton-acciones" CausesValidation="False" OnLoad="ctrl000011_Load" PostBackURL="frmTiposdeprocesos_det.aspx?_mode_=1&_closea_=0" />					<asp:Button  runat="server" ID="ctrl000010" Text="Refrescar" CssClass="boton-acciones" CausesValidation="True" ValidationGroup="vg" />		</td></tr>
</table>
<!-- End -->
</td></tr><tr><td  colspan="1" >					<!-- Init -->
<table id="Keys" style="background-color:#F0F0F0;" width="100%">
					<tr>						<td  class="search-table" style="vertical-align:top;" colspan="4" >						<asp:UpdatePanel  ID="updupdctrl000014" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="True" >
<Triggers>
  <asp:AsyncPostBackTrigger  ControlID="ctrl000014" EventName="PageIndexChanging" />
</Triggers>
<ContentTemplate>

<asp:GridView  runat="server" ID="ctrl000014" AllowPaging="True" AllowSorting="True" AutoGenerateColumns="False" CssClass="form-control" PageSize="50"
 GridLines="None" ShowHeader="True" ShowFooter="True" Width="100%" UseAccessibleHeader="False" DataKeyNames="cod" CellPadding="2" DataSourceID="dsctrl000014" onRowCreated="ctrl000014_RowCreated" >
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
<asp:Image  runat="server" ID="rowctrl000014imageimg" Width="12px" GenerateEmptyAlternateText="True" ImageURL="imagenes/icon00000003.png" BorderWidth="0" AlternateText="Imagen no disp." />
</ItemTemplate>
</asp:TemplateField>
<asp:TemplateField   AccessibleHeaderText="Orden" HeaderText="Orden" SortExpression="orden" ItemStyle-CssClass="tabla-celda" HeaderStyle-CssClass="tabla-titulo" FooterStyle-CssClass="tabla-celda">
<ItemStyle  HorizontalAlign="Left" CssClass="searchresult-table-cell" />
<ItemTemplate>
<asp:Label  runat="server" ID="ctrl000014colitem1" width="100%" OnDataBinding="ctrl000014colitem1_Databound" /></ItemTemplate>
</asp:TemplateField>
<asp:TemplateField   AccessibleHeaderText="Descripcion" HeaderText="Descripcion" SortExpression="dsc" ItemStyle-CssClass="tabla-celda" HeaderStyle-CssClass="tabla-titulo" FooterStyle-CssClass="tabla-celda">
<ItemStyle  HorizontalAlign="Left" CssClass="searchresult-table-cell" />
<ItemTemplate>
<asp:LinkButton runat="server" ID="ctrl000014colitem2view" Text='<%# Eval("dsc") %>' CssClass="form-control-read" OnDataBinding="ctrl000014colitem2view_DataBinding" CausesValidation="False" OnClientClick='<%# "javascript:if (window.showModalDialog) {window.showModalDialog(" & chr(39) & "frmTiposdeprocesos_det.aspx?_mode_=0&_closea_=1&isDlg=1&param1=" & Eval("cod") & "&timestamp=" & chr(39) & " + new Date().getTime() + " & chr(39) & "" & chr(39) & ", " & chr(39) & "" & chr(39) & ", " & chr(39) & "menubar=no;resizable=yes;toolbar=no;status=no;center=yes;dialogWidth=750px;dialogHeight=570px;" & chr(39) & ");} else {window.open(" & chr(39) & "frmTiposdeprocesos_det.aspx?_mode_=0&_closea_=1&isDlg=1&param1=" & Eval("cod") & "&timestamp=" & chr(39) & " + new Date().getTime() + " & chr(39) & "" & chr(39) & ", " & chr(39) & "" & chr(39) & ", " & chr(39) & "modal=yes;directories=no;menubar=no;resizable=yes;toolbar=no;status=no;center=yes;dialogWidth=750px;dialogHeight=570px;" & chr(39) & ");};return false;" %>' PostBackUrl='<%# "javascript:if (window.showModalDialog) {window.showModalDialog(" & chr(39) & "frmTiposdeprocesos_det.aspx?_mode_=0&_closea_=1&isDlg=1&param1=" & Eval("cod") & "&timestamp=" & chr(39) & " + new Date().getTime() + " & chr(39) & "" & chr(39) & ", " & chr(39) & "" & chr(39) & ", " & chr(39) & "menubar=no;resizable=yes;toolbar=no;status=no;center=yes;dialogWidth=750px;dialogHeight=570px;" & chr(39) & ");} else {window.open(" & chr(39) & "frmTiposdeprocesos_det.aspx?_mode_=0&_closea_=1&isDlg=1&param1=" & Eval("cod") & "&timestamp=" & chr(39) & " + new Date().getTime() + " & chr(39) & "" & chr(39) & ", " & chr(39) & "" & chr(39) & ", " & chr(39) & "modal=yes;directories=no;menubar=no;resizable=yes;toolbar=no;status=no;center=yes;dialogWidth=750px;dialogHeight=570px;" & chr(39) & ");};return false;" %>' />
</ItemTemplate>
</asp:TemplateField>
<asp:TemplateField   AccessibleHeaderText="Abreviatura" HeaderText="Abreviatura" SortExpression="abrev" ItemStyle-CssClass="tabla-celda" HeaderStyle-CssClass="tabla-titulo" FooterStyle-CssClass="tabla-celda">
<ItemStyle  HorizontalAlign="Left" CssClass="searchresult-table-cell" />
<ItemTemplate>
<asp:Label  runat="server" ID="ctrl000014colitem3" width="100%" OnDataBinding="ctrl000014colitem3_Databound" /></ItemTemplate>
</asp:TemplateField>
<asp:TemplateField   AccessibleHeaderText="Subir" HeaderText="Subir" ItemStyle-CssClass="tabla-celda" HeaderStyle-CssClass="tabla-titulo" FooterStyle-CssClass="tabla-celda">
<ItemStyle  HorizontalAlign="Left" Width="16px" />
<ItemTemplate>
<asp:ImageButton runat="server" ID="cmdItemUp" ImageURL="./imagenes/objup.png" Width="16" Height="16" CssClass="boton-acciones" CommandName="itemup" OnClick="cmdItemUp_Click" OnDataBinding="cmdItemUp_DataBinding" CausesValidation="False" CommandArgument='<%# Eval("cod") %>' /></ItemTemplate>
</asp:TemplateField>
<asp:TemplateField   AccessibleHeaderText="Bajar" HeaderText="Bajar" ItemStyle-CssClass="tabla-celda" HeaderStyle-CssClass="tabla-titulo" FooterStyle-CssClass="tabla-celda">
<ItemStyle  HorizontalAlign="Left" Width="16px" />
<ItemTemplate>
<asp:ImageButton runat="server" ID="cmdItemDown" ImageURL="./imagenes/objdown.png" Width="16" Height="16" CssClass="boton-acciones" CommandName="itemup" OnClick="cmdItemDown_Click" OnDataBinding="cmdItemDown_DataBinding" CausesValidation="False" CommandArgument='<%# Eval("cod") %>' /></ItemTemplate>
</asp:TemplateField>
<asp:HyperLinkField  HeaderText="Ver" DataNavigateUrlFields="cod" DataNavigateUrlFormatString="frmTiposdeprocesos_det.aspx?_mode_=0&_closea_=0&param1={0}" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="16" Text="&lt;img src=./imagenes/actview.png border=0&gt alt='Ver' width='16' ;" >
<HeaderStyle  CssClass="tabla-titulo" />
</asp:HyperLinkField>
<asp:TemplateField   AccessibleHeaderText="Editar" HeaderText="Editar" ItemStyle-CssClass="tabla-celda" HeaderStyle-CssClass="tabla-titulo" FooterStyle-CssClass="tabla-celda">
<ItemStyle  HorizontalAlign="Left" Width="16px" />
<ItemTemplate>
<asp:ImageButton runat="server" ID="cmdctrl000014edit" ImageURL="./imagenes/actmod.png" Width="16px" Height="16px" OnDataBinding="cmdctrl000014edit_DataBinding" CausesValidation="False" PostBackURL='<%# "frmTiposdeprocesos_det.aspx?_mode_=2&_closea_=0&param1=" & Eval("cod") & "" %>' /></ItemTemplate>
</asp:TemplateField>
<asp:TemplateField   AccessibleHeaderText="Borrar" HeaderText="Borrar" ItemStyle-CssClass="tabla-celda" HeaderStyle-CssClass="tabla-titulo" FooterStyle-CssClass="tabla-celda">
<ItemStyle  HorizontalAlign="Left" Width="16px" />
<ItemTemplate>
<asp:ImageButton runat="server" ID="cmdctrl000014delete" ImageURL="./imagenes/actdel.png" Width="16px" Height="16px" OnDataBinding="cmdctrl000014delete_DataBinding" CausesValidation="False" PostBackURL='<%# "frmTiposdeprocesos_det.aspx?_mode_=3&_closea_=0&param1=" & Eval("cod") & "" %>' /></ItemTemplate>
</asp:TemplateField>
<asp:TemplateField   AccessibleHeaderText="Copiar" HeaderText="Copiar" ItemStyle-CssClass="tabla-celda" HeaderStyle-CssClass="tabla-titulo" FooterStyle-CssClass="tabla-celda">
<ItemStyle  HorizontalAlign="Left" Width="16px" />
<ItemTemplate>
<asp:ImageButton runat="server" ID="cmdctrl000014copy" ImageURL="./imagenes/actcopy.png" Width="16px" Height="16px" OnDataBinding="cmdctrl000014copy_DataBinding" CausesValidation="False" PostBackURL='<%# "frmTiposdeprocesos_det.aspx?_mode_=25&_closea_=0&param1=" & Eval("cod") & "" %>' /></ItemTemplate>
</asp:TemplateField>
</Columns>
</asp:GridView>
</ContentTemplate>
</asp:UpdatePanel>
<asp:UpdateProgress  runat="server" AssociatedUpdatePanelID="updupdctrl000014" >
<ProgressTemplate><span  class="ajaxupd" ><div style="position:absolute;top:2px;left:50%; border-color:#333333; border:1; border-style:solid; background-color:#FFFFFF;padding:3px;"><image src="imagenes/ajaxupdate.gif" border=0 />Actualizando</div></span></ProgressTemplate>
</asp:UpdateProgress>

					<asp:SqlDataSource  runat="server" ID="dsctrl000014" ProviderName="System.Data.SqlClient" CancelSelectOnNullParameter="False" onInit="dsctrl000014_Init" SelectCommandType="Text"
 SelectCommand="SELECT DOC_APA.cod,DOC_APA.orden,DOC_APA.dsc,DOC_APA.abrev FROM DOC_APA   WHERE (( @qsidcod_list ) AND (DOC_APA.baja = '1900-01-01T00:00:00' OR DOC_APA.baja  IS NULL)) AND DOC_APA.cod >= 1 ORDER BY DOC_APA.orden,DOC_APA.cod" onselected="dsctrl000014_Selected" onSelecting="dsctrl000014_Selecting" >
<SelectParameters>
<asp:Parameter  Name="qsidcod_list" Direction="Input" Type="String" Size="200" />
</SelectParameters>
</asp:SqlDataSource>
				<br /><asp:Label  runat="server" ID="lblerror" CssClass="error" width="100%" />	</table>
<!-- End -->
</td></tr></table>
</asp:Content>

