<%@ Page Language="VB" CodeFile="frmFuncionesyequipos.aspx.vb" MasterPageFile="general.master" AutoEventWireup="True" Inherits="frmFuncionesyequipos" Title="Funciones y equipos" %>
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
<tr><td  style="width:32px;" colspan="1" ><img  src="imagenes/icon00000014.png" width="32px" height="32px" alt="Funciones y equipos" hspace="4"/></td>
<td  style="text-align:left;" colspan="1" ><span class="form-title">Funciones y equipos</span></td></tr>
</table>
</td></tr>
<tr><td  colspan="1" >						<asp:UpdatePanel  ID="updupdpnlSearch" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="True" >
<Triggers>
  <asp:AsyncPostBackTrigger  ControlID="ctrl000013" EventName="Click" />
  <asp:AsyncPostBackTrigger  ControlID="ctrl000011" EventName="Click" />
</Triggers>
<ContentTemplate>
<!-- Init Filtros de búsqueda -->
<table id="Search" class="hide_print" style="background-color:#F0F0F0;" width="100%">
<tr>
<td  class="search-title" colspan="5" >Filtros de búsqueda</td></tr>
						<tr><td  class="search-item-title" style="vertical-align:top;width:15%" colspan="1" >							Unidad relacionada							</td>							<td  class="search-table" style="vertical-align:top;width:35%" colspan="1" >							<asp:UpdatePanel  ID="updupdpnlupdctrl000016fs" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="True" >
<Triggers>
  <asp:AsyncPostBackTrigger  ControlID="ctrl000016delete" EventName="Click" />
</Triggers>
<ContentTemplate>
<asp:UpdatePanel  ID="updupdctrl000016fs" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="True" >
<ContentTemplate>
<asp:Button  runat="server" ID="cmdctrl000016showpanel" Text="..." CssClass="boton-acciones" CausesValidation="False" OnClick="cmdctrl000016showpanel_Click" />

<asp:ImageButton runat="server" ID="ctrl000016delete" ImageURL="~/imagenes/actdel.png" Width="16" Height="16" Tooltip="Quitar Unidad relacionada" Visible="False" OnClick="ctrl000016delete_Click" OnDataBinding="ctrl000016delete_DataBinding" CausesValidation="False" />
<asp:LinkButton runat="server" ID="ctrl000016view" CssClass="boton-acciones-subbutton" CausesValidation="False" />
<asp:HiddenField  runat="server" ID="ctrl000016type"/>
<asp:HiddenField  runat="server" ID="ctrl000016" onLoad="ctrl000016_Load"/>
</ContentTemplate>
</asp:UpdatePanel>
<asp:UpdateProgress  runat="server" AssociatedUpdatePanelID="updupdctrl000016fs" >
<ProgressTemplate><span  class="ajaxupd" ><div style="position:absolute;top:2px;left:50%; border-color:#333333; border:1; border-style:solid; background-color:#FFFFFF;padding:3px;"><image src="imagenes/ajaxupdate.gif" border=0 />Actualizando</div></span></ProgressTemplate>
</asp:UpdateProgress>
</ContentTemplate>
</asp:UpdatePanel>
<asp:UpdateProgress  runat="server" AssociatedUpdatePanelID="updupdpnlupdctrl000016fs" >
<ProgressTemplate><span  class="ajaxupd" ><div style="position:absolute;top:2px;left:50%; border-color:#333333; border:1; border-style:solid; background-color:#FFFFFF;padding:3px;"><image src="imagenes/ajaxupdate.gif" border=0 />Actualizando</div></span></ProgressTemplate>
</asp:UpdateProgress>
							</td>						<asp:SqlDataSource  runat="server" ID="dsctrl000016" CancelSelectOnNullParameter="False" onInit="dsctrl000016_Init" SelectCommandType="Text"
 SelectCommand="SELECT UND.dsc,UND.cod FROM UND   WHERE (UND.baja = 0 OR UND.baja  IS NULL) ORDER BY UND.orden" onselected="dsctrl000016_Selected" >
<SelectParameters>
</SelectParameters>
</asp:SqlDataSource>
			</table>
<!-- End Filtros de búsqueda -->
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
<td  class="search-title" colspan="5" >								<asp:Button  runat="server" ID="ctrl000014" Text="Buscar" CssClass="boton-acciones" CausesValidation="True" OnClick="ctrl000014_Click" ValidationGroup="vg" />							<asp:Button  runat="server" ID="ctrl000013" Text="Limpiar filtros" CssClass="boton-acciones" CausesValidation="True" OnClick="ctrl000013_Click" ValidationGroup="vg" />						<asp:Button  runat="server" ID="ctrl000012" Text="Nuevo" CssClass="boton-acciones" CausesValidation="False" OnLoad="ctrl000012_Load" PostBackURL="frmFuncionesyequipos_det.aspx?_mode_=1&_closea_=0" />					<asp:Button  runat="server" ID="ctrl000011" Text="Refrescar" CssClass="boton-acciones" CausesValidation="True" ValidationGroup="vg" />		</td></tr>
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
 GridLines="None" ShowHeader="True" ShowFooter="True" Width="100%" UseAccessibleHeader="False" DataKeyNames="cod" CellPadding="2" DataSourceID="dsctrl000018" onRowCreated="ctrl000018_RowCreated" >
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
<asp:Image  runat="server" ID="rowctrl000018imageimg" Width="12px" GenerateEmptyAlternateText="True" ImageURL="imagenes/icon00000014.png" BorderWidth="0" AlternateText="Imagen no disp." />
</ItemTemplate>
</asp:TemplateField>
<asp:TemplateField   AccessibleHeaderText="Descripcion" HeaderText="Descripcion" SortExpression="dsc" ItemStyle-CssClass="tabla-celda" HeaderStyle-CssClass="tabla-titulo" FooterStyle-CssClass="tabla-celda">
<ItemStyle  HorizontalAlign="Left" CssClass="searchresult-table-cell" />
<ItemTemplate>
<asp:LinkButton runat="server" ID="ctrl000018colitem1view" Text='<%# Eval("dsc") %>' CssClass="form-control-read" OnDataBinding="ctrl000018colitem1view_DataBinding" CausesValidation="False" OnClientClick='<%# "javascript:if (window.showModalDialog) {window.showModalDialog(" & chr(39) & "frmFuncionesyequipos_det.aspx?_mode_=0&_closea_=1&isDlg=1&param1=" & Eval("cod") & "&timestamp=" & chr(39) & " + new Date().getTime() + " & chr(39) & "" & chr(39) & ", " & chr(39) & "" & chr(39) & ", " & chr(39) & "menubar=no;resizable=yes;toolbar=no;status=no;center=yes;dialogWidth=750px;dialogHeight=570px;" & chr(39) & ");} else {window.open(" & chr(39) & "frmFuncionesyequipos_det.aspx?_mode_=0&_closea_=1&isDlg=1&param1=" & Eval("cod") & "&timestamp=" & chr(39) & " + new Date().getTime() + " & chr(39) & "" & chr(39) & ", " & chr(39) & "" & chr(39) & ", " & chr(39) & "modal=yes;directories=no;menubar=no;resizable=yes;toolbar=no;status=no;center=yes;dialogWidth=750px;dialogHeight=570px;" & chr(39) & ");};return false;" %>' PostBackUrl='<%# "javascript:if (window.showModalDialog) {window.showModalDialog(" & chr(39) & "frmFuncionesyequipos_det.aspx?_mode_=0&_closea_=1&isDlg=1&param1=" & Eval("cod") & "&timestamp=" & chr(39) & " + new Date().getTime() + " & chr(39) & "" & chr(39) & ", " & chr(39) & "" & chr(39) & ", " & chr(39) & "menubar=no;resizable=yes;toolbar=no;status=no;center=yes;dialogWidth=750px;dialogHeight=570px;" & chr(39) & ");} else {window.open(" & chr(39) & "frmFuncionesyequipos_det.aspx?_mode_=0&_closea_=1&isDlg=1&param1=" & Eval("cod") & "&timestamp=" & chr(39) & " + new Date().getTime() + " & chr(39) & "" & chr(39) & ", " & chr(39) & "" & chr(39) & ", " & chr(39) & "modal=yes;directories=no;menubar=no;resizable=yes;toolbar=no;status=no;center=yes;dialogWidth=750px;dialogHeight=570px;" & chr(39) & ");};return false;" %>' />
</ItemTemplate>
</asp:TemplateField>
<asp:TemplateField   AccessibleHeaderText="Unidad relacionada" HeaderText="Unidad relacionada" SortExpression="undcod" ItemStyle-CssClass="tabla-celda" HeaderStyle-CssClass="tabla-titulo" FooterStyle-CssClass="tabla-celda">
<ItemStyle  HorizontalAlign="Left" CssClass="searchresult-table-cell" />
<ItemTemplate>
<asp:LinkButton runat="server" ID="ctrl000018colitem2view" Text='<%# Eval("undcoddsc") %>' CssClass="form-control-read" OnDataBinding="ctrl000018colitem2view_DataBinding" CausesValidation="False" OnClientClick='<%# "javascript:if (window.showModalDialog) {window.showModalDialog(" & chr(39) & "frmUnidades_det.aspx?_mode_=0&_closea_=1&isDlg=1&param1=" & Eval("undcod") & "&timestamp=" & chr(39) & " + new Date().getTime() + " & chr(39) & "" & chr(39) & ", " & chr(39) & "" & chr(39) & ", " & chr(39) & "menubar=no;resizable=yes;toolbar=no;status=no;center=yes;dialogWidth=750px;dialogHeight=570px;" & chr(39) & ");} else {window.open(" & chr(39) & "frmUnidades_det.aspx?_mode_=0&_closea_=1&isDlg=1&param1=" & Eval("undcod") & "&timestamp=" & chr(39) & " + new Date().getTime() + " & chr(39) & "" & chr(39) & ", " & chr(39) & "" & chr(39) & ", " & chr(39) & "modal=yes;directories=no;menubar=no;resizable=yes;toolbar=no;status=no;center=yes;dialogWidth=750px;dialogHeight=570px;" & chr(39) & ");};return false;" %>' PostBackUrl='<%# "javascript:if (window.showModalDialog) {window.showModalDialog(" & chr(39) & "frmUnidades_det.aspx?_mode_=0&_closea_=1&isDlg=1&param1=" & Eval("undcod") & "&timestamp=" & chr(39) & " + new Date().getTime() + " & chr(39) & "" & chr(39) & ", " & chr(39) & "" & chr(39) & ", " & chr(39) & "menubar=no;resizable=yes;toolbar=no;status=no;center=yes;dialogWidth=750px;dialogHeight=570px;" & chr(39) & ");} else {window.open(" & chr(39) & "frmUnidades_det.aspx?_mode_=0&_closea_=1&isDlg=1&param1=" & Eval("undcod") & "&timestamp=" & chr(39) & " + new Date().getTime() + " & chr(39) & "" & chr(39) & ", " & chr(39) & "" & chr(39) & ", " & chr(39) & "modal=yes;directories=no;menubar=no;resizable=yes;toolbar=no;status=no;center=yes;dialogWidth=750px;dialogHeight=570px;" & chr(39) & ");};return false;" %>' />
<asp:HiddenField  runat="server" ID="ctrl000018colitem2type"/>
<asp:HiddenField  runat="server" ID="ctrl000018colitem2" OnDataBinding="ctrl000018colitem2_DataBound"/>
</ItemTemplate>
</asp:TemplateField>
<asp:HyperLinkField  HeaderText="Ver" DataNavigateUrlFields="cod" DataNavigateUrlFormatString="frmFuncionesyequipos_det.aspx?_mode_=0&_closea_=0&param1={0}" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="16" Text="&lt;img src=./imagenes/actview.png border=0&gt alt='Ver' width='16' ;" >
<HeaderStyle  CssClass="tabla-titulo" />
</asp:HyperLinkField>
<asp:TemplateField   AccessibleHeaderText="Editar" HeaderText="Editar" ItemStyle-CssClass="tabla-celda" HeaderStyle-CssClass="tabla-titulo" FooterStyle-CssClass="tabla-celda">
<ItemStyle  HorizontalAlign="Left" Width="16px" />
<ItemTemplate>
<asp:ImageButton runat="server" ID="cmdctrl000018edit" ImageURL="./imagenes/actmod.png" Width="16px" Height="16px" OnDataBinding="cmdctrl000018edit_DataBinding" CausesValidation="False" PostBackURL='<%# "frmFuncionesyequipos_det.aspx?_mode_=2&_closea_=0&param1=" & Eval("cod") & "" %>' /></ItemTemplate>
</asp:TemplateField>
<asp:TemplateField   AccessibleHeaderText="Borrar" HeaderText="Borrar" ItemStyle-CssClass="tabla-celda" HeaderStyle-CssClass="tabla-titulo" FooterStyle-CssClass="tabla-celda">
<ItemStyle  HorizontalAlign="Left" Width="16px" />
<ItemTemplate>
<asp:ImageButton runat="server" ID="cmdctrl000018delete" ImageURL="./imagenes/actdel.png" Width="16px" Height="16px" OnDataBinding="cmdctrl000018delete_DataBinding" CausesValidation="False" PostBackURL='<%# "frmFuncionesyequipos_det.aspx?_mode_=3&_closea_=0&param1=" & Eval("cod") & "" %>' /></ItemTemplate>
</asp:TemplateField>
<asp:TemplateField   AccessibleHeaderText="Copiar" HeaderText="Copiar" ItemStyle-CssClass="tabla-celda" HeaderStyle-CssClass="tabla-titulo" FooterStyle-CssClass="tabla-celda">
<ItemStyle  HorizontalAlign="Left" Width="16px" />
<ItemTemplate>
<asp:ImageButton runat="server" ID="cmdctrl000018copy" ImageURL="./imagenes/actcopy.png" Width="16px" Height="16px" OnDataBinding="cmdctrl000018copy_DataBinding" CausesValidation="False" PostBackURL='<%# "frmFuncionesyequipos_det.aspx?_mode_=25&_closea_=0&param1=" & Eval("cod") & "" %>' /></ItemTemplate>
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
 SelectCommand="SELECT DOC_EQU.cod,DOC_EQU.dsc,(DOC_EQUUNDCOD.dsc) as undcoddsc,DOC_EQU.undcod FROM DOC_EQU  LEFT JOIN UND AS DOC_EQUUNDCOD ON DOC_EQUUNDCOD.cod=DOC_EQU.undcod  WHERE (((DOC_EQU.undcod = @ctrl000016 OR @ctrl000016 = -1) AND @qsidcod_list ) AND (DOC_EQU.baja = 0 OR DOC_EQU.baja  IS NULL)) AND DOC_EQU.cod >= 1 ORDER BY DOC_EQU.dsc,DOC_EQU.cod" onselected="dsctrl000018_Selected" onSelecting="dsctrl000018_Selecting" >
<SelectParameters>
<asp:ControlParameter  Name="ctrl000016" ControlID="ctrl000016" ConvertEmptyStringToNull="True" Direction="InputOutput" Type="Int32" PropertyName="Value" />
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
<div  >
<!-- Panel pnlobjexplorer -->
<asp:Label  runat="server" ID="lblpnlpnlobjexplorer" width="100%" />
<ajaxkit:ModalPopupExtender  runat="server" ID="pnlobjexplorer" DropShadow="False" EnableViewState="True" PopupControlID="pnlpnlobjexplorer" TargetControlID="lblpnlpnlobjexplorer" BackgroundCssClass="bgmodalpopup" />
<asp:Panel  ID="pnlpnlobjexplorer" runat="server" BorderWidth="1" BorderStyle="solid" >
<asp:UpdatePanel  ID="updpnlobjexplorer" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="True" >
<Triggers>
  <asp:AsyncPostBackTrigger  ControlID="tvwmodalpopuppnlobjexplorer" EventName="SelectedNodeChanged" />
  <asp:AsyncPostBackTrigger  ControlID="grdobjexplorer" EventName="RowCommand" />
</Triggers>
<ContentTemplate>
<table class="form" cellpadding="0" cellspacing="0"><tr><td  colspan="1" >
<table style="width:700px;height:300px; border: 2px solid #000000; padding: 2px; margin: 1px; background-color: #F0F0F0; vertical-align: top; text-align: left;">
<tr><td  class="tabla-titulo" colspan="2" >Seleccionar</td></tr>
<tr>
<td  class="search-title" style="height:15px;width:700px" colspan="2" >
<asp:Button  runat="server" ID="cmdobjexplorercancel" Text="Cancelar" CssClass="boton-acciones" CausesValidation="False" OnClick="cmdobjexplorercancel_Click" />
<asp:Label  runat="server" ID="lblobjexplorerfilter" width="100" Text='Filtro rápido:' />
<asp:TextBox  runat="server" ID="txtobjexplorerfilter" CssClass="form-control" Width="200px" MaxLength="20" />
<asp:CompareValidator  runat="server" ID="vcdvaltxtobjexplorerfilter" SetFocusOnError="true" CssClass="error" ControltoValidate="txtobjexplorerfilter" Display="Dynamic" Text="Ingrese un texto" ErrorMessage=":Ingrese un texto" Type="String" Operator="DataTypeCheck" />
<asp:RegularExpressionValidator  runat="server" ID="vrgvaltxtobjexplorerfilter" SetFocusOnError="true" CssClass="error" Display="Dynamic" ControltoValidate="txtobjexplorerfilter" ValidationExpression="[\w\s-+/*^()!|°$%&=-_,:;.ñÑ~áéíóúÁÉÍÓÚ#}{¿]{0,20}" Text="No mayor a 20 caracteres. Deben ser letras o numeros." ErrorMessage=":No mayor a 20 caracteres. Deben ser letras o numeros." />
<ajaxkit:ValidatorCalloutExtender  runat="server" ID="ajaxvrgtxtobjexplorerfilter" TargetControlID="vrgvaltxtobjexplorerfilter" />

<asp:Button  runat="server" ID="cmdobjexplorerfilter" Text="Buscar" CssClass="boton-acciones" CausesValidation="False" OnClick="cmdobjexplorerfilter_Click" />
</td>
</tr>
<tr>
<td  style="vertical-align:top; text-align:left;width:200px;height:315px;background-color:#F0F0F0;" colspan="1" >
<div style="height:315px;overflow-y:scroll;">
<asp:TreeView  runat="server" ID="tvwmodalpopuppnlobjexplorer" ShowLines="True" AutoGenerateDataBindings="False" Width="200" NodeWrap="True" NodeIndent="5" ForeColor="Black" SelectedNodeStyle-BackColor="#C0C0C0" SelectedNodeStyle-CssClass="objectexplorer-selectnode" OnSelectedNodeChanged="tvwmodalpopuppnlobjexplorer_SelectedNodeChanged" />
</div>
</td>
<td  style="vertical-align:top; text-align:left;width:450px;height:315px;" colspan="1" >
<div style="height:315px;width:450px; overflow-x:scroll;">
<asp:GridView  runat="server" ID="grdobjexplorer" AllowPaging="True" AllowSorting="True" AutoGenerateColumns="False" PageSize="9"
 GridLines="None" ShowHeader="False" ShowFooter="False" Width="100%" UseAccessibleHeader="False" DataKeyNames="cod,objecttype" CellPadding="2" onRowCreated="grdobjexplorer_RowCreated" onRowCommand="grdobjexplorer_RowCommand" OnSelectedIndexChanged="grdobjexplorer_SelectedIndexChanged" OnPageIndexChanging="grdobjexplorer_PageIndexChanging" >
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
<asp:Button  runat="server" ID="grdobjexplorerselectrow" Text="Seleccionar" CssClass="boton-acciones" CommandName="Select" CausesValidation="False" /></ItemTemplate>
</asp:TemplateField>
<asp:TemplateField   ItemStyle-CssClass="tabla-celda" HeaderStyle-CssClass="tabla-titulo" FooterStyle-CssClass="tabla-celda">
<ItemStyle  HorizontalAlign="Left" Width="12px" CssClass="searchresult-table-cell" />
<ItemTemplate>
<asp:Image  runat="server" ID="grdobjexplorerrowimageimg" Width="12px" GenerateEmptyAlternateText="True" ImageURL='<%# "~/imagenes/icon" & format(Eval("objecttype"),"00000000") & ".png" %>' BorderColor="LightGray" BorderWidth="1" BorderStyle="Solid" AlternateText="Imagen no disp." />
</ItemTemplate>
</asp:TemplateField>
<asp:TemplateField   ItemStyle-CssClass="tabla-celda" HeaderStyle-CssClass="tabla-titulo" FooterStyle-CssClass="tabla-celda">
<ItemStyle  HorizontalAlign="Left" />
<ItemTemplate>
<asp:LinkButton runat="server" ID="grdobjexplorerrowlabel" Text='<%# Eval("dsc") %>' CssClass="boton-acciones" CommandName="Select" CausesValidation="False" /></ItemTemplate>
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
<asp:UpdateProgress  runat="server" AssociatedUpdatePanelID="updpnlobjexplorer" >
<ProgressTemplate><span  class="ajaxupd" ><div style="position:absolute;top:2px;left:50%; border-color:#333333; border:1; border-style:solid; background-color:#FFFFFF;padding:3px;"><image src="imagenes/ajaxupdate.gif" border=0 />Actualizando</div></span></ProgressTemplate>
</asp:UpdateProgress>
<!-- Fin panel pnlobjexplorer -->


</div>
</asp:Content>

