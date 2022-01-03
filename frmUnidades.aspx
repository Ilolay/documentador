<%@ Page Language="VB" CodeFile="frmUnidades.aspx.vb" MasterPageFile="general.master" AutoEventWireup="True" Inherits="frmUnidades" Title="Unidades" %>
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
<tr><td  style="width:32px;" colspan="1" ><img  src="imagenes/icon00000010.png" width="32px" height="32px" alt="Unidades" hspace="4"/></td>
<td  style="text-align:left;" colspan="1" ><span class="form-title">Unidades</span></td></tr>
</table>
</td></tr>
<tr><td  colspan="1" >									<asp:UpdatePanel  ID="updupdpnlSearch" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="True" >
<Triggers>
  <asp:AsyncPostBackTrigger  ControlID="ctrl000025" EventName="Click" />
  <asp:AsyncPostBackTrigger  ControlID="ctrl000023" EventName="Click" />
</Triggers>
<ContentTemplate>
<!-- Init Filtros de búsqueda -->
<table id="Search" class="hide_print" style="background-color:#F0F0F0;" width="100%">
<tr>
<td  class="search-title" colspan="5" >Filtros de búsqueda</td></tr>
									<tr><td  class="search-item-title" style="vertical-align:top;width:15%" colspan="1" >										Descripción										</td>										<td  class="search-table" style="vertical-align:top;width:35%" colspan="1" >										<asp:TextBox  runat="server" ID="ctrl000028" CssClass="form-control" Width="280px" MaxLength="30" Tooltip="Campo31[]" />
<asp:CompareValidator  runat="server" ID="vcdvalctrl000028" SetFocusOnError="true" CssClass="error" ControltoValidate="ctrl000028" Display="Dynamic" Text="Ingrese un texto" ErrorMessage="Descripción:Ingrese un texto" Type="String" Operator="DataTypeCheck" ValidationGroup="vg" />
<asp:RegularExpressionValidator  runat="server" ID="vrgvalctrl000028" SetFocusOnError="true" CssClass="error" Display="Dynamic" ControltoValidate="ctrl000028" ValidationExpression="[\w\s-+/*^()!|°$%&=-_,:;.ñÑ~áéíóúÁÉÍÓÚ#}{¿]{0,30}" Text="No mayor a 30 caracteres. Deben ser letras o numeros." ErrorMessage="Descripción:No mayor a 30 caracteres. Deben ser letras o numeros." ValidationGroup="vg" />
<ajaxkit:ValidatorCalloutExtender  runat="server" ID="ajaxvrgctrl000028" TargetControlID="vrgvalctrl000028" />
										</td><td  class="search-item-title" style="vertical-align:top;width:15%" colspan="1" >									Unidad superior en jerarquía									</td>									<td  class="search-table" style="vertical-align:top;width:35%" colspan="1" >									<asp:UpdatePanel  ID="updupdpnlupdctrl000029fs" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="True" >
<Triggers>
  <asp:AsyncPostBackTrigger  ControlID="ctrl000029delete" EventName="Click" />
</Triggers>
<ContentTemplate>
<asp:UpdatePanel  ID="updupdctrl000029fs" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="True" >
<ContentTemplate>
<asp:Button  runat="server" ID="cmdctrl000029showpanel" Text="..." CssClass="boton-acciones" CausesValidation="False" OnClick="cmdctrl000029showpanel_Click" />

<asp:ImageButton runat="server" ID="ctrl000029delete" ImageURL="~/imagenes/actdel.png" Width="16" Height="16" Tooltip="Quitar Unidad superior en jerarquía" Visible="False" OnClick="ctrl000029delete_Click" OnDataBinding="ctrl000029delete_DataBinding" CausesValidation="False" />
<asp:LinkButton runat="server" ID="ctrl000029view" CssClass="boton-acciones-subbutton" CausesValidation="False" />
<asp:HiddenField  runat="server" ID="ctrl000029type"/>
<asp:HiddenField  runat="server" ID="ctrl000029" onLoad="ctrl000029_Load"/>
</ContentTemplate>
</asp:UpdatePanel>
<asp:UpdateProgress  runat="server" AssociatedUpdatePanelID="updupdctrl000029fs" >
<ProgressTemplate><span  class="ajaxupd" ><div style="position:absolute;top:2px;left:50%; border-color:#333333; border:1; border-style:solid; background-color:#FFFFFF;padding:3px;"><image src="imagenes/ajaxupdate.gif" border=0 />Actualizando</div></span></ProgressTemplate>
</asp:UpdateProgress>
</ContentTemplate>
</asp:UpdatePanel>
<asp:UpdateProgress  runat="server" AssociatedUpdatePanelID="updupdpnlupdctrl000029fs" >
<ProgressTemplate><span  class="ajaxupd" ><div style="position:absolute;top:2px;left:50%; border-color:#333333; border:1; border-style:solid; background-color:#FFFFFF;padding:3px;"><image src="imagenes/ajaxupdate.gif" border=0 />Actualizando</div></span></ProgressTemplate>
</asp:UpdateProgress>
									</td>								</tr>								<asp:SqlDataSource  runat="server" ID="dsctrl000029" CancelSelectOnNullParameter="False" onInit="dsctrl000029_Init" SelectCommandType="Text"
 SelectCommand="SELECT UND.dsc,UND.cod FROM UND   WHERE (UND.baja = 0 OR UND.baja  IS NULL) ORDER BY UND.orden" onselected="dsctrl000029_Selected" >
<SelectParameters>
</SelectParameters>
</asp:SqlDataSource>
						<tr><td  class="search-item-title" style="vertical-align:top;width:15%" colspan="1" >							Editor							</td>							<td  class="search-table" style="vertical-align:top;width:35%" colspan="1" >							<asp:UpdatePanel  ID="updupdpnlupdctrl000030fs" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="True" >
<Triggers>
  <asp:AsyncPostBackTrigger  ControlID="ctrl000030delete" EventName="Click" />
</Triggers>
<ContentTemplate>
<asp:UpdatePanel  ID="updupdctrl000030fs" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="True" >
<ContentTemplate>
<asp:Button  runat="server" ID="cmdctrl000030showpanel" Text="..." CssClass="boton-acciones" CausesValidation="False" OnClick="cmdctrl000030showpanel_Click" />

<asp:ImageButton runat="server" ID="ctrl000030delete" ImageURL="~/imagenes/actdel.png" Width="16" Height="16" Tooltip="Quitar Editor" Visible="False" OnClick="ctrl000030delete_Click" OnDataBinding="ctrl000030delete_DataBinding" CausesValidation="False" />
<asp:LinkButton runat="server" ID="ctrl000030view" CssClass="boton-acciones-subbutton" CausesValidation="False" />
<asp:HiddenField  runat="server" ID="ctrl000030type"/>
<asp:HiddenField  runat="server" ID="ctrl000030" onLoad="ctrl000030_Load"/>
</ContentTemplate>
</asp:UpdatePanel>
<asp:UpdateProgress  runat="server" AssociatedUpdatePanelID="updupdctrl000030fs" >
<ProgressTemplate><span  class="ajaxupd" ><div style="position:absolute;top:2px;left:50%; border-color:#333333; border:1; border-style:solid; background-color:#FFFFFF;padding:3px;"><image src="imagenes/ajaxupdate.gif" border=0 />Actualizando</div></span></ProgressTemplate>
</asp:UpdateProgress>
</ContentTemplate>
</asp:UpdatePanel>
<asp:UpdateProgress  runat="server" AssociatedUpdatePanelID="updupdpnlupdctrl000030fs" >
<ProgressTemplate><span  class="ajaxupd" ><div style="position:absolute;top:2px;left:50%; border-color:#333333; border:1; border-style:solid; background-color:#FFFFFF;padding:3px;"><image src="imagenes/ajaxupdate.gif" border=0 />Actualizando</div></span></ProgressTemplate>
</asp:UpdateProgress>
							</td>						<asp:SqlDataSource  runat="server" ID="dsctrl000030" CancelSelectOnNullParameter="False" onInit="dsctrl000030_Init" SelectCommandType="Text"
 SelectCommand="SELECT EMP.dsc,EMP.cod FROM EMP   WHERE (EMP.baja = 0 OR EMP.baja  IS NULL) ORDER BY EMP.dsc" onselected="dsctrl000030_Selected" >
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
  <asp:AsyncPostBackTrigger  ControlID="ctrl000023" EventName="Click" />
</Triggers>
<ContentTemplate>
<!-- Init -->
<table id="Actions" class="hide_print" style="background-color:#F0F0F0;" width="100%">
<tr>
<td  class="search-title" colspan="5" >								<asp:Button  runat="server" ID="ctrl000026" Text="Buscar" CssClass="boton-acciones" CausesValidation="True" OnClick="ctrl000026_Click" ValidationGroup="vg" />							<asp:Button  runat="server" ID="ctrl000025" Text="Limpiar filtros" CssClass="boton-acciones" CausesValidation="True" OnClick="ctrl000025_Click" ValidationGroup="vg" />						<asp:Button  runat="server" ID="ctrl000024" Text="Nuevo" CssClass="boton-acciones" CausesValidation="False" OnLoad="ctrl000024_Load" PostBackURL="frmUnidades_det.aspx?_mode_=1&_closea_=0" />					<asp:Button  runat="server" ID="ctrl000023" Text="Refrescar" CssClass="boton-acciones" CausesValidation="True" ValidationGroup="vg" />		</td></tr>
</table>
<!-- End -->
</ContentTemplate>
</asp:UpdatePanel>
<asp:UpdateProgress  runat="server" AssociatedUpdatePanelID="updupdpnlActions" >
<ProgressTemplate><span  class="ajaxupd" ><div style="position:absolute;top:2px;left:50%; border-color:#333333; border:1; border-style:solid; background-color:#FFFFFF;padding:3px;"><image src="imagenes/ajaxupdate.gif" border=0 />Actualizando</div></span></ProgressTemplate>
</asp:UpdateProgress>
</td></tr><tr><td  colspan="1" >					<asp:UpdatePanel  ID="updupdpnlKeys" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="True" >
<Triggers>
  <asp:AsyncPostBackTrigger  ControlID="ctrl000026" EventName="Click" />
  <asp:AsyncPostBackTrigger  ControlID="ctrl000025" EventName="Click" />
  <asp:AsyncPostBackTrigger  ControlID="ctrl000023" EventName="Click" />
</Triggers>
<ContentTemplate>
<!-- Init -->
<table id="Keys" style="background-color:#F0F0F0;" width="100%">
					<tr>						<td  class="search-table" style="vertical-align:top;" colspan="4" >						<asp:UpdatePanel  ID="updupdpnlctrl000032" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="True" >
<Triggers>
  <asp:AsyncPostBackTrigger  ControlID="ctrl000026" EventName="Click" />
  <asp:AsyncPostBackTrigger  ControlID="ctrl000025" EventName="Click" />
</Triggers>
<ContentTemplate>
<asp:UpdatePanel  ID="updupdctrl000032" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="True" >
<Triggers>
  <asp:AsyncPostBackTrigger  ControlID="ctrl000032" EventName="PageIndexChanging" />
</Triggers>
<ContentTemplate>

<asp:GridView  runat="server" ID="ctrl000032" AllowPaging="True" AllowSorting="True" AutoGenerateColumns="False" CssClass="form-control" PageSize="50"
 GridLines="None" ShowHeader="True" ShowFooter="True" Width="100%" UseAccessibleHeader="False" DataKeyNames="cod" CellPadding="2" DataSourceID="dsctrl000032" onRowCreated="ctrl000032_RowCreated" >
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
<asp:Image  runat="server" ID="rowctrl000032imageimg" Width="12px" GenerateEmptyAlternateText="True" ImageURL="imagenes/icon00000010.png" BorderWidth="0" AlternateText="Imagen no disp." />
</ItemTemplate>
</asp:TemplateField>
<asp:TemplateField   AccessibleHeaderText="Orden" HeaderText="Orden" SortExpression="orden" ItemStyle-CssClass="tabla-celda" HeaderStyle-CssClass="tabla-titulo" FooterStyle-CssClass="tabla-celda">
<ItemStyle  HorizontalAlign="Left" CssClass="searchresult-table-cell" />
<ItemTemplate>
<asp:Label  runat="server" ID="ctrl000032colitem1" width="100%" OnDataBinding="ctrl000032colitem1_Databound" /></ItemTemplate>
</asp:TemplateField>
<asp:TemplateField   AccessibleHeaderText="Descripción" HeaderText="Descripción" SortExpression="dsc" ItemStyle-CssClass="tabla-celda" HeaderStyle-CssClass="tabla-titulo" FooterStyle-CssClass="tabla-celda">
<ItemStyle  HorizontalAlign="Left" CssClass="searchresult-table-cell" />
<ItemTemplate>
<asp:LinkButton runat="server" ID="ctrl000032colitem2view" Text='<%# Eval("dsc") %>' CssClass="form-control-read" OnDataBinding="ctrl000032colitem2view_DataBinding" CausesValidation="False" OnClientClick='<%# "javascript:if (window.showModalDialog) {window.showModalDialog(" & chr(39) & "frmUnidades_det.aspx?_mode_=0&_closea_=1&isDlg=1&param1=" & Eval("cod") & "&timestamp=" & chr(39) & " + new Date().getTime() + " & chr(39) & "" & chr(39) & ", " & chr(39) & "" & chr(39) & ", " & chr(39) & "menubar=no;resizable=yes;toolbar=no;status=no;center=yes;dialogWidth=750px;dialogHeight=570px;" & chr(39) & ");} else {window.open(" & chr(39) & "frmUnidades_det.aspx?_mode_=0&_closea_=1&isDlg=1&param1=" & Eval("cod") & "&timestamp=" & chr(39) & " + new Date().getTime() + " & chr(39) & "" & chr(39) & ", " & chr(39) & "" & chr(39) & ", " & chr(39) & "modal=yes;directories=no;menubar=no;resizable=yes;toolbar=no;status=no;center=yes;dialogWidth=750px;dialogHeight=570px;" & chr(39) & ");};return false;" %>' PostBackUrl='<%# "javascript:if (window.showModalDialog) {window.showModalDialog(" & chr(39) & "frmUnidades_det.aspx?_mode_=0&_closea_=1&isDlg=1&param1=" & Eval("cod") & "&timestamp=" & chr(39) & " + new Date().getTime() + " & chr(39) & "" & chr(39) & ", " & chr(39) & "" & chr(39) & ", " & chr(39) & "menubar=no;resizable=yes;toolbar=no;status=no;center=yes;dialogWidth=750px;dialogHeight=570px;" & chr(39) & ");} else {window.open(" & chr(39) & "frmUnidades_det.aspx?_mode_=0&_closea_=1&isDlg=1&param1=" & Eval("cod") & "&timestamp=" & chr(39) & " + new Date().getTime() + " & chr(39) & "" & chr(39) & ", " & chr(39) & "" & chr(39) & ", " & chr(39) & "modal=yes;directories=no;menubar=no;resizable=yes;toolbar=no;status=no;center=yes;dialogWidth=750px;dialogHeight=570px;" & chr(39) & ");};return false;" %>' />
</ItemTemplate>
</asp:TemplateField>
<asp:TemplateField   AccessibleHeaderText="Responsable de la unidad" HeaderText="Responsable de la unidad" SortExpression="resp" ItemStyle-CssClass="tabla-celda" HeaderStyle-CssClass="tabla-titulo" FooterStyle-CssClass="tabla-celda">
<ItemStyle  HorizontalAlign="Left" CssClass="searchresult-table-cell" />
<ItemTemplate>
<asp:LinkButton runat="server" ID="ctrl000032colitem3view" Text='<%# Eval("respdsc") %>' CssClass="form-control-read" OnDataBinding="ctrl000032colitem3view_DataBinding" CausesValidation="False" OnClientClick='<%# "javascript:if (window.showModalDialog) {window.showModalDialog(" & chr(39) & "frmColaboradores_det.aspx?_mode_=0&_closea_=1&isDlg=1&param1=" & Eval("resp") & "&timestamp=" & chr(39) & " + new Date().getTime() + " & chr(39) & "" & chr(39) & ", " & chr(39) & "" & chr(39) & ", " & chr(39) & "menubar=no;resizable=yes;toolbar=no;status=no;center=yes;dialogWidth=750px;dialogHeight=570px;" & chr(39) & ");} else {window.open(" & chr(39) & "frmColaboradores_det.aspx?_mode_=0&_closea_=1&isDlg=1&param1=" & Eval("resp") & "&timestamp=" & chr(39) & " + new Date().getTime() + " & chr(39) & "" & chr(39) & ", " & chr(39) & "" & chr(39) & ", " & chr(39) & "modal=yes;directories=no;menubar=no;resizable=yes;toolbar=no;status=no;center=yes;dialogWidth=750px;dialogHeight=570px;" & chr(39) & ");};return false;" %>' PostBackUrl='<%# "javascript:if (window.showModalDialog) {window.showModalDialog(" & chr(39) & "frmColaboradores_det.aspx?_mode_=0&_closea_=1&isDlg=1&param1=" & Eval("resp") & "&timestamp=" & chr(39) & " + new Date().getTime() + " & chr(39) & "" & chr(39) & ", " & chr(39) & "" & chr(39) & ", " & chr(39) & "menubar=no;resizable=yes;toolbar=no;status=no;center=yes;dialogWidth=750px;dialogHeight=570px;" & chr(39) & ");} else {window.open(" & chr(39) & "frmColaboradores_det.aspx?_mode_=0&_closea_=1&isDlg=1&param1=" & Eval("resp") & "&timestamp=" & chr(39) & " + new Date().getTime() + " & chr(39) & "" & chr(39) & ", " & chr(39) & "" & chr(39) & ", " & chr(39) & "modal=yes;directories=no;menubar=no;resizable=yes;toolbar=no;status=no;center=yes;dialogWidth=750px;dialogHeight=570px;" & chr(39) & ");};return false;" %>' />
<asp:HiddenField  runat="server" ID="ctrl000032colitem3type"/>
<asp:HiddenField  runat="server" ID="ctrl000032colitem3" OnDataBinding="ctrl000032colitem3_DataBound"/>
</ItemTemplate>
</asp:TemplateField>
<asp:TemplateField   AccessibleHeaderText="Unidad superior en jerarquía" HeaderText="Unidad superior en jerarquía" SortExpression="undcodsup" ItemStyle-CssClass="tabla-celda" HeaderStyle-CssClass="tabla-titulo" FooterStyle-CssClass="tabla-celda">
<ItemStyle  HorizontalAlign="Left" CssClass="searchresult-table-cell" />
<ItemTemplate>
<asp:LinkButton runat="server" ID="ctrl000032colitem4view" Text='<%# Eval("undcodsupdsc") %>' CssClass="form-control-read" OnDataBinding="ctrl000032colitem4view_DataBinding" CausesValidation="False" OnClientClick='<%# "javascript:if (window.showModalDialog) {window.showModalDialog(" & chr(39) & "frmUnidades_det.aspx?_mode_=0&_closea_=1&isDlg=1&param1=" & Eval("undcodsup") & "&timestamp=" & chr(39) & " + new Date().getTime() + " & chr(39) & "" & chr(39) & ", " & chr(39) & "" & chr(39) & ", " & chr(39) & "menubar=no;resizable=yes;toolbar=no;status=no;center=yes;dialogWidth=750px;dialogHeight=570px;" & chr(39) & ");} else {window.open(" & chr(39) & "frmUnidades_det.aspx?_mode_=0&_closea_=1&isDlg=1&param1=" & Eval("undcodsup") & "&timestamp=" & chr(39) & " + new Date().getTime() + " & chr(39) & "" & chr(39) & ", " & chr(39) & "" & chr(39) & ", " & chr(39) & "modal=yes;directories=no;menubar=no;resizable=yes;toolbar=no;status=no;center=yes;dialogWidth=750px;dialogHeight=570px;" & chr(39) & ");};return false;" %>' PostBackUrl='<%# "javascript:if (window.showModalDialog) {window.showModalDialog(" & chr(39) & "frmUnidades_det.aspx?_mode_=0&_closea_=1&isDlg=1&param1=" & Eval("undcodsup") & "&timestamp=" & chr(39) & " + new Date().getTime() + " & chr(39) & "" & chr(39) & ", " & chr(39) & "" & chr(39) & ", " & chr(39) & "menubar=no;resizable=yes;toolbar=no;status=no;center=yes;dialogWidth=750px;dialogHeight=570px;" & chr(39) & ");} else {window.open(" & chr(39) & "frmUnidades_det.aspx?_mode_=0&_closea_=1&isDlg=1&param1=" & Eval("undcodsup") & "&timestamp=" & chr(39) & " + new Date().getTime() + " & chr(39) & "" & chr(39) & ", " & chr(39) & "" & chr(39) & ", " & chr(39) & "modal=yes;directories=no;menubar=no;resizable=yes;toolbar=no;status=no;center=yes;dialogWidth=750px;dialogHeight=570px;" & chr(39) & ");};return false;" %>' />
<asp:HiddenField  runat="server" ID="ctrl000032colitem4type"/>
<asp:HiddenField  runat="server" ID="ctrl000032colitem4" OnDataBinding="ctrl000032colitem4_DataBound"/>
</ItemTemplate>
</asp:TemplateField>
<asp:TemplateField   AccessibleHeaderText="Identificador interno" HeaderText="Identificador interno" SortExpression="undnro" ItemStyle-CssClass="tabla-celda" HeaderStyle-CssClass="tabla-titulo" FooterStyle-CssClass="tabla-celda">
<ItemStyle  HorizontalAlign="Left" CssClass="searchresult-table-cell" />
<ItemTemplate>
<asp:Label  runat="server" ID="ctrl000032colitem5" width="100%" OnDataBinding="ctrl000032colitem5_Databound" /></ItemTemplate>
</asp:TemplateField>
<asp:TemplateField   AccessibleHeaderText="Formato especifico" HeaderText="Formato especifico" SortExpression="formatoespecifico" ItemStyle-CssClass="tabla-celda" HeaderStyle-CssClass="tabla-titulo" FooterStyle-CssClass="tabla-celda">
<ItemStyle  HorizontalAlign="Left" CssClass="searchresult-table-cell" />
<ItemTemplate>
<asp:Label  runat="server" ID="ctrl000032colitem6" width="100%" OnDataBinding="ctrl000032colitem6_Databound" /></ItemTemplate>
</asp:TemplateField>
<asp:TemplateField   AccessibleHeaderText="Editor" HeaderText="Editor" SortExpression="editor" ItemStyle-CssClass="tabla-celda" HeaderStyle-CssClass="tabla-titulo" FooterStyle-CssClass="tabla-celda">
<ItemStyle  HorizontalAlign="Left" CssClass="searchresult-table-cell" />
<ItemTemplate>
<asp:LinkButton runat="server" ID="ctrl000032colitem7view" Text='<%# Eval("editordsc") %>' CssClass="form-control-read" OnDataBinding="ctrl000032colitem7view_DataBinding" CausesValidation="False" OnClientClick='<%# "javascript:if (window.showModalDialog) {window.showModalDialog(" & chr(39) & "frmColaboradores_det.aspx?_mode_=0&_closea_=1&isDlg=1&param1=" & Eval("editor") & "&timestamp=" & chr(39) & " + new Date().getTime() + " & chr(39) & "" & chr(39) & ", " & chr(39) & "" & chr(39) & ", " & chr(39) & "menubar=no;resizable=yes;toolbar=no;status=no;center=yes;dialogWidth=750px;dialogHeight=570px;" & chr(39) & ");} else {window.open(" & chr(39) & "frmColaboradores_det.aspx?_mode_=0&_closea_=1&isDlg=1&param1=" & Eval("editor") & "&timestamp=" & chr(39) & " + new Date().getTime() + " & chr(39) & "" & chr(39) & ", " & chr(39) & "" & chr(39) & ", " & chr(39) & "modal=yes;directories=no;menubar=no;resizable=yes;toolbar=no;status=no;center=yes;dialogWidth=750px;dialogHeight=570px;" & chr(39) & ");};return false;" %>' PostBackUrl='<%# "javascript:if (window.showModalDialog) {window.showModalDialog(" & chr(39) & "frmColaboradores_det.aspx?_mode_=0&_closea_=1&isDlg=1&param1=" & Eval("editor") & "&timestamp=" & chr(39) & " + new Date().getTime() + " & chr(39) & "" & chr(39) & ", " & chr(39) & "" & chr(39) & ", " & chr(39) & "menubar=no;resizable=yes;toolbar=no;status=no;center=yes;dialogWidth=750px;dialogHeight=570px;" & chr(39) & ");} else {window.open(" & chr(39) & "frmColaboradores_det.aspx?_mode_=0&_closea_=1&isDlg=1&param1=" & Eval("editor") & "&timestamp=" & chr(39) & " + new Date().getTime() + " & chr(39) & "" & chr(39) & ", " & chr(39) & "" & chr(39) & ", " & chr(39) & "modal=yes;directories=no;menubar=no;resizable=yes;toolbar=no;status=no;center=yes;dialogWidth=750px;dialogHeight=570px;" & chr(39) & ");};return false;" %>' />
<asp:HiddenField  runat="server" ID="ctrl000032colitem7type"/>
<asp:HiddenField  runat="server" ID="ctrl000032colitem7" OnDataBinding="ctrl000032colitem7_DataBound"/>
</ItemTemplate>
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
<asp:HyperLinkField  HeaderText="Ver" DataNavigateUrlFields="cod" DataNavigateUrlFormatString="frmUnidades_det.aspx?_mode_=0&_closea_=0&param1={0}" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="16" Text="&lt;img src=./imagenes/actview.png border=0&gt alt='Ver' width='16' ;" >
<HeaderStyle  CssClass="tabla-titulo" />
</asp:HyperLinkField>
<asp:TemplateField   AccessibleHeaderText="Editar" HeaderText="Editar" ItemStyle-CssClass="tabla-celda" HeaderStyle-CssClass="tabla-titulo" FooterStyle-CssClass="tabla-celda">
<ItemStyle  HorizontalAlign="Left" Width="16px" />
<ItemTemplate>
<asp:ImageButton runat="server" ID="cmdctrl000032edit" ImageURL="./imagenes/actmod.png" Width="16px" Height="16px" OnDataBinding="cmdctrl000032edit_DataBinding" CausesValidation="False" PostBackURL='<%# "frmUnidades_det.aspx?_mode_=2&_closea_=0&param1=" & Eval("cod") & "" %>' /></ItemTemplate>
</asp:TemplateField>
<asp:TemplateField   AccessibleHeaderText="Borrar" HeaderText="Borrar" ItemStyle-CssClass="tabla-celda" HeaderStyle-CssClass="tabla-titulo" FooterStyle-CssClass="tabla-celda">
<ItemStyle  HorizontalAlign="Left" Width="16px" />
<ItemTemplate>
<asp:ImageButton runat="server" ID="cmdctrl000032delete" ImageURL="./imagenes/actdel.png" Width="16px" Height="16px" OnDataBinding="cmdctrl000032delete_DataBinding" CausesValidation="False" PostBackURL='<%# "frmUnidades_det.aspx?_mode_=3&_closea_=0&param1=" & Eval("cod") & "" %>' /></ItemTemplate>
</asp:TemplateField>
<asp:TemplateField   AccessibleHeaderText="Copiar" HeaderText="Copiar" ItemStyle-CssClass="tabla-celda" HeaderStyle-CssClass="tabla-titulo" FooterStyle-CssClass="tabla-celda">
<ItemStyle  HorizontalAlign="Left" Width="16px" />
<ItemTemplate>
<asp:ImageButton runat="server" ID="cmdctrl000032copy" ImageURL="./imagenes/actcopy.png" Width="16px" Height="16px" OnDataBinding="cmdctrl000032copy_DataBinding" CausesValidation="False" PostBackURL='<%# "frmUnidades_det.aspx?_mode_=25&_closea_=0&param1=" & Eval("cod") & "" %>' /></ItemTemplate>
</asp:TemplateField>
</Columns>
</asp:GridView>
</ContentTemplate>
</asp:UpdatePanel>
<asp:UpdateProgress  runat="server" AssociatedUpdatePanelID="updupdctrl000032" >
<ProgressTemplate><span  class="ajaxupd" ><div style="position:absolute;top:2px;left:50%; border-color:#333333; border:1; border-style:solid; background-color:#FFFFFF;padding:3px;"><image src="imagenes/ajaxupdate.gif" border=0 />Actualizando</div></span></ProgressTemplate>
</asp:UpdateProgress>

</ContentTemplate>
</asp:UpdatePanel>
<asp:UpdateProgress  runat="server" AssociatedUpdatePanelID="updupdpnlctrl000032" >
<ProgressTemplate><span  class="ajaxupd" ><div style="position:absolute;top:2px;left:50%; border-color:#333333; border:1; border-style:solid; background-color:#FFFFFF;padding:3px;"><image src="imagenes/ajaxupdate.gif" border=0 />Actualizando</div></span></ProgressTemplate>
</asp:UpdateProgress>
					<asp:SqlDataSource  runat="server" ID="dsctrl000032" ProviderName="System.Data.SqlClient" CancelSelectOnNullParameter="False" onInit="dsctrl000032_Init" SelectCommandType="Text"
 SelectCommand="SELECT UND.cod,UND.orden,UND.dsc,(UNDRESP.dsc) as respdsc,(UNDUNDCODSUP.dsc) as undcodsupdsc,UND.undnro,UND.formatoespecifico,(UNDEDITOR.dsc) as editordsc,UND.resp,UND.undcodsup,UND.editor FROM UND  LEFT JOIN UND AS UNDUNDCODSUP ON UNDUNDCODSUP.cod=UND.undcodsup LEFT JOIN EMP AS UNDRESP ON UNDRESP.cod=UND.resp LEFT JOIN EMP AS UNDEDITOR ON UNDEDITOR.cod=UND.editor  WHERE (((UND.dsc LIKE '%' + @ctrl000028 + '%' OR @ctrl000028  IS NULL) AND (UND.undcodsup = @ctrl000029 OR @ctrl000029 = -1) AND (UND.editor = @ctrl000030 OR @ctrl000030 = -1) AND @qsidcod_list ) AND (UND.baja = 0 OR UND.baja  IS NULL)) AND UND.cod >= 1 ORDER BY UND.orden,UND.cod" onselected="dsctrl000032_Selected" onSelecting="dsctrl000032_Selecting" >
<SelectParameters>
<asp:ControlParameter  Name="ctrl000028" ControlID="ctrl000028" ConvertEmptyStringToNull="True" Direction="InputOutput" Type="String" Size="50" PropertyName="Text" />
<asp:ControlParameter  Name="ctrl000029" ControlID="ctrl000029" ConvertEmptyStringToNull="True" Direction="InputOutput" Type="Int32" PropertyName="Value" />
<asp:ControlParameter  Name="ctrl000030" ControlID="ctrl000030" ConvertEmptyStringToNull="True" Direction="InputOutput" Type="Int32" PropertyName="Value" />
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

