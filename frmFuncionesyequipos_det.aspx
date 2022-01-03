<%@ Page Language="VB" CodeFile="frmFuncionesyequipos_det.aspx.vb" MasterPageFile="general.master" AutoEventWireup="True" Inherits="frmFuncionesyequipos_det" Title="Funciones y equipos" %>
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
<td  style="text-align:left;" colspan="1" ><span class="form-title">Funciones y equipos</span> |<asp:Label  runat="server" ID="lblsubtitle" CssClass="form-subtitle" />
</td></tr>
</table>
</td></tr>
<tr><td  colspan="1" >							<!-- Init -->
<table id="body" style="background-color:#F0F0F0;" width="100%">
								<br /><asp:Label  runat="server" ID="lblerror" CssClass="error" width="100%" />						<asp:ValidationSummary  runat="server" ID="frmdatosvalsumary" />
<asp:FormView  ID="frmdatos" runat="server" DataSourceID="dsdatos" DataKeyNames="cod" DefaultMode="ReadOnly" OnItemDeleted="frmdatos_ItemDeleted" OnItemUpdated="frmdatos_ItemUpdated" OnItemInserted="frmdatos_ItemInserted" OnDataBound="frmdatos_ItemSelected" >
<ItemTemplate>

<!-- Init Plantilla item --><div  runat="server" Visible='<%# Request.Querystring("_view_")<> 1 %>' ><asp:Button  runat="server" ID="cmdFormViewConfirmDelete" Text="Borrar" CssClass="boton-acciones" CommandName="Delete" CausesValidation="True" Style="color:#FF0000; font-style:italic;" onClientClick="javascript:return confirm('Confirma la eliminación?')" />
<asp:Button  runat="server" ID="cmdrefresh" Text="Refrescar" CssClass="boton-acciones" CausesValidation="False" />
<asp:Button  runat="server" ID="cmdFormViewItemCancel" Text="Cancelar" CssClass="boton-acciones" CausesValidation="False" OnClick="cmdFormViewItemCancel_Click" />
<asp:Button  runat="server" ID="cmdSecPermView" Text="Permisos" CssClass="boton-acciones" CausesValidation="False" OnDataBinding="cmdSecPermView_DataBinding" onClientClick='<%# "javascript:if (window.showModalDialog) {window.showModalDialog(" & chr(39) & "frmsecperm.aspx??_mode_=0&_closea_=1&isDlg=1&param1=-1&timestamp=" & chr(39) & " + new Date().getTime() + " & chr(39) & "" & chr(39) & ", " & chr(39) & "" & chr(39) & ", " & chr(39) & "menubar=no;resizable=yes;toolbar=no;status=no;center=yes;dialogWidth=750px;dialogHeight=570px;" & chr(39) & ");} else {window.open(" & chr(39) & "frmsecperm.aspx??_mode_=0&_closea_=1&isDlg=1&param1=-1&timestamp=" & chr(39) & " + new Date().getTime() + " & chr(39) & "" & chr(39) & ", " & chr(39) & "" & chr(39) & ", " & chr(39) & "modal=yes;directories=no;menubar=no;resizable=yes;toolbar=no;status=no;center=yes;dialogWidth=750px;dialogHeight=570px;" & chr(39) & ");};return false;" %>' />
<asp:Button  runat="server" ID="cmdFormViewItemUpdate" Text="Editar" CssClass="boton-acciones" CausesValidation="False" OnClick="cmdFormViewItemUpdate_Click" OnDataBinding="cmdFormViewItemUpdate_DataBinding" />
</div>
<table width="100%" cellpadding="1" cellspacing="1"><tr>
<td  colspan="1" >
Código
</td>
<td  colspan="1" >
<asp:Label  runat="server" ID="itemctrl000009" CssClass="form-control-read" width="100%" OnDataBinding="itemctrl000009_Databound" />
</td>
</tr>
<tr>
<td  colspan="1" >
Descripcion
</td>
<td  colspan="1" >
<asp:Label  runat="server" ID="itemctrl000007" CssClass="form-control-read" width="100%" OnDataBinding="itemctrl000007_Databound" />
</td>
</tr>
<tr>
<td  colspan="1" >
Grupo de miembros
</td>
<td  colspan="1" >
<asp:LinkButton runat="server" ID="itemctrl000006view" Text='<%# Eval("miembrosgrpcoddsc") %>' CssClass="form-control-read" OnDataBinding="itemctrl000006view_DataBinding" CausesValidation="False" OnClientClick='<%# "javascript:if (window.showModalDialog) {window.showModalDialog(" & chr(39) & "frmGrupos_det.aspx?_mode_=0&_closea_=1&isDlg=1&param1=" & Eval("miembrosgrpcod") & "&timestamp=" & chr(39) & " + new Date().getTime() + " & chr(39) & "" & chr(39) & ", " & chr(39) & "" & chr(39) & ", " & chr(39) & "menubar=no;resizable=yes;toolbar=no;status=no;center=yes;dialogWidth=750px;dialogHeight=570px;" & chr(39) & ");} else {window.open(" & chr(39) & "frmGrupos_det.aspx?_mode_=0&_closea_=1&isDlg=1&param1=" & Eval("miembrosgrpcod") & "&timestamp=" & chr(39) & " + new Date().getTime() + " & chr(39) & "" & chr(39) & ", " & chr(39) & "" & chr(39) & ", " & chr(39) & "modal=yes;directories=no;menubar=no;resizable=yes;toolbar=no;status=no;center=yes;dialogWidth=750px;dialogHeight=570px;" & chr(39) & ");};return false;" %>' PostBackUrl='<%# "javascript:if (window.showModalDialog) {window.showModalDialog(" & chr(39) & "frmGrupos_det.aspx?_mode_=0&_closea_=1&isDlg=1&param1=" & Eval("miembrosgrpcod") & "&timestamp=" & chr(39) & " + new Date().getTime() + " & chr(39) & "" & chr(39) & ", " & chr(39) & "" & chr(39) & ", " & chr(39) & "menubar=no;resizable=yes;toolbar=no;status=no;center=yes;dialogWidth=750px;dialogHeight=570px;" & chr(39) & ");} else {window.open(" & chr(39) & "frmGrupos_det.aspx?_mode_=0&_closea_=1&isDlg=1&param1=" & Eval("miembrosgrpcod") & "&timestamp=" & chr(39) & " + new Date().getTime() + " & chr(39) & "" & chr(39) & ", " & chr(39) & "" & chr(39) & ", " & chr(39) & "modal=yes;directories=no;menubar=no;resizable=yes;toolbar=no;status=no;center=yes;dialogWidth=750px;dialogHeight=570px;" & chr(39) & ");};return false;" %>' />
<asp:HiddenField  runat="server" ID="itemctrl000006type"/>
<asp:HiddenField  runat="server" ID="itemctrl000006" OnDataBinding="itemctrl000006_DataBound"/>

</td>
</tr>
<tr>
<td  colspan="2" >
Funciones-Miembros
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
 GridLines="None" ShowHeader="True" ShowFooter="True" Width="100%" UseAccessibleHeader="False" DataKeyNames="cod" CellPadding="2" DataSourceID="itemdsctrl000004" onRowCreated="itemctrl000004_RowCreated" >
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
<asp:Image  runat="server" ID="rowctrl000004imageimg" Width="12px" GenerateEmptyAlternateText="True" ImageURL="imagenes/icon00000018.png" BorderWidth="0" AlternateText="Imagen no disp." />
</ItemTemplate>
</asp:TemplateField>
<asp:TemplateField   AccessibleHeaderText="Miembro" HeaderText="Miembro" SortExpression="mbrtypecod" ItemStyle-CssClass="tabla-celda" HeaderStyle-CssClass="tabla-titulo" FooterStyle-CssClass="tabla-celda">
<ItemStyle  HorizontalAlign="Left" CssClass="searchresult-table-cell" />
<ItemTemplate>
<asp:LinkButton runat="server" ID="itemctrl000004colitem1view" Text='<%# Eval("mbrtypecoddsc") %>' CssClass="form-control-read" OnDataBinding="itemctrl000004colitem1view_DataBinding" CausesValidation="False" />
<asp:HiddenField  runat="server" ID="itemctrl000004colitem1"/>
<asp:HiddenField  runat="server" ID="itemctrl000004colitem1value" OnDataBinding="itemctrl000004colitem1value_DataBound"/>
</ItemTemplate>
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
<b>Descripcion</b>
</td>
<td  colspan="1" >
<asp:TextBox  runat="server" ID="editctrl000007" CssClass="form-control" Width="400px" MaxLength="50" OnDataBinding="editctrl000007_DataBound_frmdatos" Tooltip="Campo53[]" TextMode="MultiLine" onkeypress="return textCounter(this,50);" onkeydown="return textCounter(this,50);" onkeyup="return textCounter(this,50);" onfocus="return textCounter_GetFocus(this,50);" onblur="return textCounter_LostFocus(this);" /> <span class='error'><b>*</b></span>
<asp:RequiredFieldValidator  runat="server" ID="vrqeditctrl000007" SetFocusOnError="true" CssClass="error" Display="Dynamic" ControltoValidate="editctrl000007" Text="Obligatorio!!! ErrorMessage=´Descripcion:es un dato obligatorio!´" ValidationGroup="vgedit" />
<ajaxkit:ValidatorCalloutExtender  runat="server" ID="ajaxvrqeditctrl000007" TargetControlID="vrqeditctrl000007" /><asp:CompareValidator  runat="server" ID="vcdvaleditctrl000007" SetFocusOnError="true" CssClass="error" ControltoValidate="editctrl000007" Display="Dynamic" Text="Ingrese un texto" ErrorMessage="Descripcion:Ingrese un texto" Type="String" Operator="DataTypeCheck" ValidationGroup="vgedit" />
<asp:RegularExpressionValidator  runat="server" ID="vrgvaleditctrl000007" SetFocusOnError="true" CssClass="error" Display="Dynamic" ControltoValidate="editctrl000007" ValidationExpression="[\w\s-+/*^()!|°$%&=-_,:;.ñÑ~áéíóúÁÉÍÓÚ#}{¿]{0,50}" Text="No mayor a 50 caracteres. Deben ser letras o numeros." ErrorMessage="Descripcion:No mayor a 50 caracteres. Deben ser letras o numeros." ValidationGroup="vgedit" />
<ajaxkit:ValidatorCalloutExtender  runat="server" ID="ajaxvrgeditctrl000007" TargetControlID="vrgvaleditctrl000007" />

</td>
</tr>
<tr>
<td  colspan="1" >
Grupo de miembros
</td>
<td  colspan="1" >
<asp:LinkButton runat="server" ID="editctrl000006view" Text='<%# Eval("miembrosgrpcoddsc") %>' CssClass="form-control-read" OnDataBinding="editctrl000006view_DataBinding" CausesValidation="False" OnClientClick='<%# "javascript:if (window.showModalDialog) {window.showModalDialog(" & chr(39) & "frmGrupos_det.aspx?_mode_=0&_closea_=1&isDlg=1&param1=" & Eval("miembrosgrpcod") & "&timestamp=" & chr(39) & " + new Date().getTime() + " & chr(39) & "" & chr(39) & ", " & chr(39) & "" & chr(39) & ", " & chr(39) & "menubar=no;resizable=yes;toolbar=no;status=no;center=yes;dialogWidth=750px;dialogHeight=570px;" & chr(39) & ");} else {window.open(" & chr(39) & "frmGrupos_det.aspx?_mode_=0&_closea_=1&isDlg=1&param1=" & Eval("miembrosgrpcod") & "&timestamp=" & chr(39) & " + new Date().getTime() + " & chr(39) & "" & chr(39) & ", " & chr(39) & "" & chr(39) & ", " & chr(39) & "modal=yes;directories=no;menubar=no;resizable=yes;toolbar=no;status=no;center=yes;dialogWidth=750px;dialogHeight=570px;" & chr(39) & ");};return false;" %>' PostBackUrl='<%# "javascript:if (window.showModalDialog) {window.showModalDialog(" & chr(39) & "frmGrupos_det.aspx?_mode_=0&_closea_=1&isDlg=1&param1=" & Eval("miembrosgrpcod") & "&timestamp=" & chr(39) & " + new Date().getTime() + " & chr(39) & "" & chr(39) & ", " & chr(39) & "" & chr(39) & ", " & chr(39) & "menubar=no;resizable=yes;toolbar=no;status=no;center=yes;dialogWidth=750px;dialogHeight=570px;" & chr(39) & ");} else {window.open(" & chr(39) & "frmGrupos_det.aspx?_mode_=0&_closea_=1&isDlg=1&param1=" & Eval("miembrosgrpcod") & "&timestamp=" & chr(39) & " + new Date().getTime() + " & chr(39) & "" & chr(39) & ", " & chr(39) & "" & chr(39) & ", " & chr(39) & "modal=yes;directories=no;menubar=no;resizable=yes;toolbar=no;status=no;center=yes;dialogWidth=750px;dialogHeight=570px;" & chr(39) & ");};return false;" %>' />
<asp:HiddenField  runat="server" ID="editctrl000006type"/>
<asp:HiddenField  runat="server" ID="editctrl000006" OnDataBinding="editctrl000006_DataBound"/>

</td>
</tr>
<tr>
<td  colspan="2" >
Funciones-Miembros
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
 GridLines="None" ShowHeader="True" ShowFooter="True" Width="100%" UseAccessibleHeader="False" DataKeyNames="cod" CellPadding="2" DataSourceID="editdsctrl000004" onRowCreated="editctrl000004_RowCreated" onRowCommand="editctrl000004_RowCommand" >
<FooterStyle  CssClass="tabla-footer" />
<RowStyle  CssClass="tabla-fila" />
<AlternatingRowStyle  CssClass="tabla-fila-alternativa" />
<SelectedRowStyle  CssClass="tabla-fila" />
<PagerStyle  CssClass="tabla-pager" />
<HeaderStyle  CssClass="tabla-titulo" ForeColor="White" />
<EmptyDataTemplate>
<p  class="tabla-fila">Sin datos</p>
</br></br></br></br><asp:Button  runat="server" ID="cmdFormViewInsert" Text="Nuevo" CssClass="boton-acciones" CommandName="HRC_INSERT" CausesValidation="False" /></EmptyDataTemplate>
<Columns>
<asp:TemplateField   ItemStyle-CssClass="tabla-celda" HeaderStyle-CssClass="tabla-titulo" FooterStyle-CssClass="tabla-celda">
<ItemStyle  HorizontalAlign="Left" Width="12px" CssClass="searchresult-table-cell" />
<ItemTemplate>
<asp:Image  runat="server" ID="rowctrl000004imageimg" Width="12px" GenerateEmptyAlternateText="True" ImageURL="imagenes/icon00000018.png" BorderWidth="0" AlternateText="Imagen no disp." />
</ItemTemplate>
</asp:TemplateField>
<asp:TemplateField   AccessibleHeaderText="Miembro" HeaderText="Miembro" SortExpression="mbrtypecod" ItemStyle-CssClass="tabla-celda" HeaderStyle-CssClass="tabla-titulo" FooterStyle-CssClass="tabla-celda">
<ItemStyle  HorizontalAlign="Left" CssClass="searchresult-table-cell" />
<ItemTemplate>
<asp:LinkButton runat="server" ID="editctrl000004colitem1view" Text='<%# Eval("mbrtypecoddsc") %>' CssClass="form-control-read" OnDataBinding="editctrl000004colitem1view_DataBinding" CausesValidation="False" />
<asp:HiddenField  runat="server" ID="editctrl000004colitem1"/>
<asp:HiddenField  runat="server" ID="editctrl000004colitem1value" OnDataBinding="editctrl000004colitem1value_DataBound"/>
</ItemTemplate>
</asp:TemplateField>
<asp:TemplateField   AccessibleHeaderText="Acciones" HeaderText="Acciones">
<ItemStyle  HorizontalAlign="Center" Width="3%" />
<ItemTemplate>
 <asp:ImageButton  ID="imge" runat="server" CausesValidation="False" CommandName="Edit" CommandArgument='<%# Eval("cod") %>' ImageUrl="./imagenes/actmod.png" Width="16" Text="Editar" />
 <asp:ImageButton  ID="imgd" runat="server" CausesValidation="False" CommandName="Delete" CommandArgument='<%# Eval("cod") %>' ImageUrl="./imagenes/actdel.png" Text="Borrar" Width="16" OnClientClick=" return confirm('Confirma la eliminación?');" />
</ItemTemplate>
<FooterTemplate>
<asp:Button  ID="cmdi" runat="server" CausesValidation="False" CommandName="HRC_INSERT" CssClass="boton-acciones" Text="Agregar" />
</FooterTemplate>
</asp:TemplateField>
<asp:TemplateField   ItemStyle-CssClass="tabla-celda" HeaderStyle-CssClass="tabla-titulo" FooterStyle-CssClass="tabla-celda">
<ItemStyle  HorizontalAlign="Left" Width="16" />
<ItemTemplate>
<asp:ImageButton runat="server" ID="cmdVer" ImageURL="./imagenes/actview.png" Width="16" Height="16" CommandName="Select" CausesValidation="False" CommandArgument='<%# Eval("cod") %>' /></ItemTemplate>
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
<b>Descripcion</b>
</td>
<td  colspan="1" >
<asp:TextBox  runat="server" ID="insctrl000007" CssClass="form-control" Width="400px" MaxLength="50" Tooltip="Campo53[]" TextMode="MultiLine" onkeypress="return textCounter(this,50);" onkeydown="return textCounter(this,50);" onkeyup="return textCounter(this,50);" onfocus="return textCounter_GetFocus(this,50);" onblur="return textCounter_LostFocus(this);" /> <span class='error'><b>*</b></span>
<asp:RequiredFieldValidator  runat="server" ID="vrqinsctrl000007" SetFocusOnError="true" CssClass="error" Display="Dynamic" ControltoValidate="insctrl000007" Text="Obligatorio!!! ErrorMessage=´Descripcion:es un dato obligatorio!´" ValidationGroup="vgins" />
<ajaxkit:ValidatorCalloutExtender  runat="server" ID="ajaxvrqinsctrl000007" TargetControlID="vrqinsctrl000007" /><asp:CompareValidator  runat="server" ID="vcdvalinsctrl000007" SetFocusOnError="true" CssClass="error" ControltoValidate="insctrl000007" Display="Dynamic" Text="Ingrese un texto" ErrorMessage="Descripcion:Ingrese un texto" Type="String" Operator="DataTypeCheck" ValidationGroup="vgins" />
<asp:RegularExpressionValidator  runat="server" ID="vrgvalinsctrl000007" SetFocusOnError="true" CssClass="error" Display="Dynamic" ControltoValidate="insctrl000007" ValidationExpression="[\w\s-+/*^()!|°$%&=-_,:;.ñÑ~áéíóúÁÉÍÓÚ#}{¿]{0,50}" Text="No mayor a 50 caracteres. Deben ser letras o numeros." ErrorMessage="Descripcion:No mayor a 50 caracteres. Deben ser letras o numeros." ValidationGroup="vgins" />
<ajaxkit:ValidatorCalloutExtender  runat="server" ID="ajaxvrginsctrl000007" TargetControlID="vrgvalinsctrl000007" />

</td>
</tr>
</table>
<!-- End Plantilla insercion --></InsertItemTemplate>

</asp:FormView>

						<asp:SqlDataSource  runat="server" ID="dsdatos" ProviderName="System.Data.SqlClient" CancelSelectOnNullParameter="False" onInit="dsdatos_Init" SelectCommandType="Text"
 SelectCommand="SELECT DOC_EQU.cod,DOC_EQU.dsc,(DOC_EQUUNDCOD.dsc) as undcoddsc,(DOC_EQUMIEMBROSGRPCOD.grpdsc) as miembrosgrpcoddsc,DOC_EQU.baja,(DOC_EQUQSECSID.secdsc) as qsecsiddsc,DOC_EQU.qsecdatetime,DOC_EQU.undcod,DOC_EQU.miembrosgrpcod,DOC_EQU.qsecsid FROM DOC_EQU  LEFT JOIN Q_SECPGRP AS DOC_EQUMIEMBROSGRPCOD ON DOC_EQUMIEMBROSGRPCOD.grpcod=DOC_EQU.miembrosgrpcod LEFT JOIN UND AS DOC_EQUUNDCOD ON DOC_EQUUNDCOD.cod=DOC_EQU.undcod LEFT JOIN Q_SECPLOGIN AS DOC_EQUQSECSID ON DOC_EQUQSECSID.sidcod=DOC_EQU.qsecsid  WHERE (DOC_EQU.cod = @param1) AND (DOC_EQU.baja = 0 OR DOC_EQU.baja  IS NULL)" onselected="dsdatos_Selected" UpdateCommandType="Text"
 UpdateCommand="UPDATE DOC_EQU SET dsc=@dsc,qsecsid=@qsecsid,qsecdatetime=getdate() WHERE DOC_EQU.cod = @param1" onupdated="dsdatos_Updated" InsertCommandType="Text"
 InsertCommand="DECLARE @querynextcodcod int  SET @querynextcodcod =(SELECT ISNULL(MAX(cod),0)+1 FROM DOC_EQU WHERE cod > 0 ) INSERT INTO DOC_EQU (cod,dsc,qsecsid,qsecdatetime) VALUES(@querynextcodcod,@dsc,@qsecsid,getdate()) SELECT @querynextcod =@querynextcodcod" onInserted="dsdatos_Inserted" DeleteCommandType="Text"
 DeleteCommand="UPDATE DOC_EQU SET baja=1 WHERE cod = @param1" ondeleted="dsdatos_Deleted" >
<InsertParameters>
<asp:Parameter  Name="querynextcod" Direction="Output" Type="Int32" />
<asp:ControlParameter  Name="dsc" ControlID="frmdatos$insctrl000007" ConvertEmptyStringToNull="True" Direction="InputOutput" Type="String" Size="50" PropertyName="Text" />
<asp:SessionParameter  Name="qsecsid" SessionField="secsid" Direction="Input" />
<asp:Parameter  Name="param1" Direction="ReturnValue" />
</InsertParameters>
<SelectParameters>
<asp:QueryStringParameter  Name="param1" QueryStringField="param1" ConvertEmptyStringToNull="True" Type="String" Size="200" />
</SelectParameters>
<UpdateParameters>
<asp:QueryStringParameter  Name="param1" QueryStringField="param1" ConvertEmptyStringToNull="True" Type="String" Size="200" />
<asp:ControlParameter  Name="dsc" ControlID="frmdatos$editctrl000007" ConvertEmptyStringToNull="True" Direction="InputOutput" Type="String" Size="50" PropertyName="Text" />
<asp:SessionParameter  Name="qsecsid" SessionField="secsid" Direction="Input" />
</UpdateParameters>
<DeleteParameters>
<asp:QueryStringParameter  Name="param1" QueryStringField="param1" ConvertEmptyStringToNull="True" Type="String" Size="200" />
<asp:SessionParameter  Name="qsecsid" SessionField="secsid" Direction="Input" />
</DeleteParameters>
</asp:SqlDataSource>
					<asp:SqlDataSource  runat="server" ID="itemdsctrl000004" ProviderName="System.Data.SqlClient" CancelSelectOnNullParameter="False" onInit="itemdsctrl000004_Init" SelectCommandType="Text"
 SelectCommand="SELECT DOC_EQUMBR.cod,(DOC_EQUMBREQUCOD.dsc) as equcoddsc,DOC_EQUMBR.mbrtypecod,CASE DOC_EQUMBR.mbrtypecod WHEN 9 THEN (SELECT TOP 1 DOC_EQUMBREMP.empcod FROM DOC_EQUMBREMP WHERE DOC_EQUMBREMP.equmbrcod=DOC_EQUMBR.cod) WHEN 10 THEN (SELECT TOP 1 DOC_EQUMBRUND.undcod FROM DOC_EQUMBRUND WHERE DOC_EQUMBRUND.equmbrcod=DOC_EQUMBR.cod) END as mbrtypecodvalue,CASE DOC_EQUMBR.mbrtypecod WHEN 9 THEN (SELECT TOP 1 EMP.dsc FROM EMP WHERE EMP.cod= (SELECT TOP 1 DOC_EQUMBREMP.empcod FROM DOC_EQUMBREMP WHERE DOC_EQUMBREMP.equmbrcod=DOC_EQUMBR.cod)) WHEN 10 THEN (SELECT TOP 1 UND.dsc FROM UND WHERE UND.cod= (SELECT TOP 1 DOC_EQUMBRUND.undcod FROM DOC_EQUMBRUND WHERE DOC_EQUMBRUND.equmbrcod=DOC_EQUMBR.cod)) END as mbrtypecoddsc,(DOC_EQUMBRQSECSID.secdsc) as qsecsiddsc,DOC_EQUMBR.qsecdatetime,DOC_EQUMBR.equcod,DOC_EQUMBR.qsecsid FROM DOC_EQUMBR AS DOC_EQUMBR  LEFT JOIN DOC_EQU AS DOC_EQUMBREQUCOD ON DOC_EQUMBREQUCOD.cod=DOC_EQUMBR.equcod LEFT JOIN Q_SECPLOGIN AS DOC_EQUMBRQSECSID ON DOC_EQUMBRQSECSID.sidcod=DOC_EQUMBR.qsecsid  WHERE (DOC_EQUMBR.equcod= @param1) AND DOC_EQUMBR.cod >= 1" onselected="itemdsctrl000004_Selected" >
<SelectParameters>
<asp:ControlParameter  Name="param1" ControlID="frmdatos" ConvertEmptyStringToNull="True" Direction="InputOutput" Type="String" Size="200" PropertyName="SelectedValue" />
</SelectParameters>
</asp:SqlDataSource>
				<asp:SqlDataSource  runat="server" ID="editdsctrl000004" ProviderName="System.Data.SqlClient" CancelSelectOnNullParameter="False" onInit="editdsctrl000004_Init" SelectCommandType="Text"
 SelectCommand="SELECT DOC_EQUMBR.cod,(DOC_EQUMBREQUCOD.dsc) as equcoddsc,DOC_EQUMBR.mbrtypecod,CASE DOC_EQUMBR.mbrtypecod WHEN 9 THEN (SELECT TOP 1 DOC_EQUMBREMP.empcod FROM DOC_EQUMBREMP WHERE DOC_EQUMBREMP.equmbrcod=DOC_EQUMBR.cod) WHEN 10 THEN (SELECT TOP 1 DOC_EQUMBRUND.undcod FROM DOC_EQUMBRUND WHERE DOC_EQUMBRUND.equmbrcod=DOC_EQUMBR.cod) END as mbrtypecodvalue,CASE DOC_EQUMBR.mbrtypecod WHEN 9 THEN (SELECT TOP 1 EMP.dsc FROM EMP WHERE EMP.cod= (SELECT TOP 1 DOC_EQUMBREMP.empcod FROM DOC_EQUMBREMP WHERE DOC_EQUMBREMP.equmbrcod=DOC_EQUMBR.cod)) WHEN 10 THEN (SELECT TOP 1 UND.dsc FROM UND WHERE UND.cod= (SELECT TOP 1 DOC_EQUMBRUND.undcod FROM DOC_EQUMBRUND WHERE DOC_EQUMBRUND.equmbrcod=DOC_EQUMBR.cod)) END as mbrtypecoddsc,(DOC_EQUMBRQSECSID.secdsc) as qsecsiddsc,DOC_EQUMBR.qsecdatetime,DOC_EQUMBR.equcod,DOC_EQUMBR.qsecsid FROM DOC_EQUMBR AS DOC_EQUMBR  LEFT JOIN DOC_EQU AS DOC_EQUMBREQUCOD ON DOC_EQUMBREQUCOD.cod=DOC_EQUMBR.equcod LEFT JOIN Q_SECPLOGIN AS DOC_EQUMBRQSECSID ON DOC_EQUMBRQSECSID.sidcod=DOC_EQUMBR.qsecsid  WHERE (DOC_EQUMBR.equcod= @param1) AND DOC_EQUMBR.cod >= 1" onselected="editdsctrl000004_Selected" UpdateCommandType="Text"
 UpdateCommand="UPDATE DOC_EQUMBR SET cod=@cod,mbrtypecod=@mbrtypecod WHERE cod=@cod" onupdated="editdsctrl000004_Updated" InsertCommandType="Text"
 InsertCommand="INSERT INTO DOC_EQUMBR (cod,equcod,mbrtypecod) VALUES(@cod,-1,@mbrtypecod)" onInserted="editdsctrl000004_Inserted" DeleteCommandType="Text"
 DeleteCommand="DELETE FROM DOC_EQUMBR WHERE cod=@cod" ondeleted="editdsctrl000004_Deleted" >
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
		<tr>			<td  style="vertical-align:top;" colspan="4" >			<!-- Panel updpanelfrmupdpanelctrl000004 -->
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
<asp:FormView  ID="frmupdpanelfrmupdpanelctrl000004" runat="server" DataSourceID="dsfrmupdpanelctrl000004" DataKeyNames="cod" DefaultMode="Insert" OnItemUpdated="frmupdpanelfrmupdpanelctrl000004_ItemUpdated" OnItemInserted="frmupdpanelfrmupdpanelctrl000004_ItemInserted" >
<EditItemTemplate>

<!-- Init Plantilla edicion --><div  runat="server" Visible='<%# Request.Querystring("_view_")<> 1 %>' ><asp:Button  runat="server" ID="cmdfrmupdpanelctrl000004updcancel" Text="Cancelar" CssClass="boton-acciones" CausesValidation="False" OnClick="cmdfrmupdpanelctrl000004updcancel_Click" />
<asp:Button  runat="server" ID="cmdfrmupdpanelctrl000004updconfirm" Text="Guardar" CssClass="boton-acciones" CausesValidation="True" OnClick="cmdfrmupdpanelctrl000004updconfirm_Click" ValidationGroup="vgeditupdpanelfrmupdpanelctrl000004" />
</div>
<table class="form" width="100%" cellpadding="1" cellspacing="1"><tr>
<td  colspan="1" >

</td>
<td  colspan="1" >
<asp:SqlDataSource  runat="server" ID="dsedtmbrtypecod" CancelSelectOnNullParameter="False" onInit="dsedtmbrtypecod_Init" SelectCommandType="Text"
 SelectCommand="SELECT DOC_EQUMBR.(sin seleccion),DOC_EQUMBR.cod FROM DOC_EQUMBR   WHERE DOC_EQUMBR.cod >= 1 ORDER BY DOC_EQUMBR.cod" onselected="dsedtmbrtypecod_Selected" >
<SelectParameters>
</SelectParameters>
</asp:SqlDataSource>

</td>
</tr>
<tr>
<td  colspan="1" >
<b>Miembro</b>
</td>
<td  colspan="1" >
<asp:UpdatePanel  ID="updupdpnlupdedtmbrtypecodfs" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="True" >
<Triggers>
  <asp:AsyncPostBackTrigger  ControlID="edtmbrtypecoddelete" EventName="Click" />
</Triggers>
<ContentTemplate>
<asp:UpdatePanel  ID="updupdedtmbrtypecodfs" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="True" >
<ContentTemplate>
<asp:Button  runat="server" ID="cmdedtmbrtypecodshowpanel" Text="..." CssClass="boton-acciones" CausesValidation="False" OnClick="cmdedtmbrtypecodshowpanel_Click" />
 <span class='error'><b>*</b></span>
<asp:ImageButton runat="server" ID="edtmbrtypecoddelete" ImageURL="~/imagenes/actdel.png" Width="16" Height="16" Tooltip="Quitar Miembro" Visible="False" OnClick="edtmbrtypecoddelete_Click" OnDataBinding="edtmbrtypecoddelete_DataBinding" CausesValidation="False" />
<asp:LinkButton runat="server" ID="edtmbrtypecodview" Text='<%# Eval("mbrtypecoddsc") %>' CssClass="boton-acciones-subbutton" OnDataBinding="edtmbrtypecodview_DataBinding" CausesValidation="False" />
<asp:HiddenField  runat="server" ID="edtmbrtypecod"/>
<asp:HiddenField  runat="server" ID="edtmbrtypecodvalue" OnDataBinding="edtmbrtypecodvalue_DataBound"/><asp:CustomValidator  runat="server" ID="vcusvalbcbf2015c2f04633ab432f34dceeb656" SetFocusOnError="true" CssClass="error" Display="Dynamic" OnServerValidate="valbcbf2015c2f04633ab432f34dceeb656_OnServerValidate" Text="Obligatorio!!! ErrorMessage=´Miembro:es un dato obligatorio!´" />


<br /><asp:Panel  ID="pnledtmbrtypecodvalue_10" runat="server" BorderWidth="1" BorderStyle="solid" onLoad="edtmbrtypecodvalue_10_Load" >
<tr><td  colspan="1" >Responsables
<asp:Checkbox  runat="server" ID="edtmbrtypecodvalue_10_gruporesp" CssClass="form-control" />
<br />
Miembros
<asp:Checkbox  runat="server" ID="edtmbrtypecodvalue_10_grupomiembros" CssClass="form-control" />
<br />
Editores
<asp:Checkbox  runat="server" ID="edtmbrtypecodvalue_10_grupoeditores" CssClass="form-control" />
<br />
Superiores
<asp:Checkbox  runat="server" ID="edtmbrtypecodvalue_10_grupoprjver" CssClass="form-control" />
<br />
Miembros directos
<asp:Checkbox  runat="server" ID="edtmbrtypecodvalue_10_grupombrdir" CssClass="form-control" />
<br />
</td></tr></asp:Panel>
</ContentTemplate>
</asp:UpdatePanel>
<asp:UpdateProgress  runat="server" AssociatedUpdatePanelID="updupdedtmbrtypecodfs" >
<ProgressTemplate><span  class="ajaxupd" ><div style="position:absolute;top:2px;left:50%; border-color:#333333; border:1; border-style:solid; background-color:#FFFFFF;padding:3px;"><image src="imagenes/ajaxupdate.gif" border=0 />Actualizando</div></span></ProgressTemplate>
</asp:UpdateProgress>
</ContentTemplate>
</asp:UpdatePanel>
<asp:UpdateProgress  runat="server" AssociatedUpdatePanelID="updupdpnlupdedtmbrtypecodfs" >
<ProgressTemplate><span  class="ajaxupd" ><div style="position:absolute;top:2px;left:50%; border-color:#333333; border:1; border-style:solid; background-color:#FFFFFF;padding:3px;"><image src="imagenes/ajaxupdate.gif" border=0 />Actualizando</div></span></ProgressTemplate>
</asp:UpdateProgress>

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

</td>
<td  colspan="1" >
<asp:SqlDataSource  runat="server" ID="dsinsmbrtypecod" CancelSelectOnNullParameter="False" onInit="dsinsmbrtypecod_Init" SelectCommandType="Text"
 SelectCommand="SELECT DOC_EQUMBR.(sin seleccion),DOC_EQUMBR.cod FROM DOC_EQUMBR   WHERE DOC_EQUMBR.cod >= 1 ORDER BY DOC_EQUMBR.cod" onselected="dsinsmbrtypecod_Selected" >
<SelectParameters>
</SelectParameters>
</asp:SqlDataSource>

</td>
</tr>
<tr>
<td  colspan="1" >
<b>Miembro</b>
</td>
<td  colspan="1" >
<asp:UpdatePanel  ID="updupdpnlupdinsmbrtypecodfs" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="True" >
<Triggers>
  <asp:AsyncPostBackTrigger  ControlID="insmbrtypecoddelete" EventName="Click" />
</Triggers>
<ContentTemplate>
<asp:UpdatePanel  ID="updupdinsmbrtypecodfs" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="True" >
<ContentTemplate>
<asp:Button  runat="server" ID="cmdinsmbrtypecodshowpanel" Text="..." CssClass="boton-acciones" CausesValidation="False" OnClick="cmdinsmbrtypecodshowpanel_Click" />
 <span class='error'><b>*</b></span>
<asp:ImageButton runat="server" ID="insmbrtypecoddelete" ImageURL="~/imagenes/actdel.png" Width="16" Height="16" Tooltip="Quitar Miembro" Visible="False" OnClick="insmbrtypecoddelete_Click" OnDataBinding="insmbrtypecoddelete_DataBinding" CausesValidation="False" />
<asp:LinkButton runat="server" ID="insmbrtypecodview" CssClass="boton-acciones-subbutton" CausesValidation="False" />
<asp:HiddenField  runat="server" ID="insmbrtypecod"/>
<asp:HiddenField  runat="server" ID="insmbrtypecodvalue"/>
<br /><asp:Panel  ID="pnlinsmbrtypecodvalue_10" runat="server" BorderWidth="1" BorderStyle="solid" onLoad="insmbrtypecodvalue_10_Load" >
<tr><td  colspan="1" >Responsables
<asp:Checkbox  runat="server" ID="insmbrtypecodvalue_10_gruporesp" CssClass="form-control" />
<br />
Miembros
<asp:Checkbox  runat="server" ID="insmbrtypecodvalue_10_grupomiembros" CssClass="form-control" />
<br />
Editores
<asp:Checkbox  runat="server" ID="insmbrtypecodvalue_10_grupoeditores" CssClass="form-control" />
<br />
Superiores
<asp:Checkbox  runat="server" ID="insmbrtypecodvalue_10_grupoprjver" CssClass="form-control" />
<br />
Miembros directos
<asp:Checkbox  runat="server" ID="insmbrtypecodvalue_10_grupombrdir" CssClass="form-control" />
<br />
</td></tr></asp:Panel>
</ContentTemplate>
</asp:UpdatePanel>
<asp:UpdateProgress  runat="server" AssociatedUpdatePanelID="updupdinsmbrtypecodfs" >
<ProgressTemplate><span  class="ajaxupd" ><div style="position:absolute;top:2px;left:50%; border-color:#333333; border:1; border-style:solid; background-color:#FFFFFF;padding:3px;"><image src="imagenes/ajaxupdate.gif" border=0 />Actualizando</div></span></ProgressTemplate>
</asp:UpdateProgress>
</ContentTemplate>
</asp:UpdatePanel>
<asp:UpdateProgress  runat="server" AssociatedUpdatePanelID="updupdpnlupdinsmbrtypecodfs" >
<ProgressTemplate><span  class="ajaxupd" ><div style="position:absolute;top:2px;left:50%; border-color:#333333; border:1; border-style:solid; background-color:#FFFFFF;padding:3px;"><image src="imagenes/ajaxupdate.gif" border=0 />Actualizando</div></span></ProgressTemplate>
</asp:UpdateProgress>

</td>
</tr>
</table>
<!-- End Plantilla insercion --></InsertItemTemplate>

</asp:FormView>

<asp:SqlDataSource  runat="server" ID="dsfrmupdpanelctrl000004" ProviderName="System.Data.SqlClient" CancelSelectOnNullParameter="False" onInit="dsfrmupdpanelctrl000004_Init" SelectCommandType="Text"
 SelectCommand="SELECT DOC_EQUMBR.cod,(DOC_EQUMBREQUCOD.dsc) as equcoddsc,DOC_EQUMBR.mbrtypecod,CASE DOC_EQUMBR.mbrtypecod WHEN 9 THEN (SELECT TOP 1 DOC_EQUMBREMP.empcod FROM DOC_EQUMBREMP WHERE DOC_EQUMBREMP.equmbrcod=DOC_EQUMBR.cod) WHEN 10 THEN (SELECT TOP 1 DOC_EQUMBRUND.undcod FROM DOC_EQUMBRUND WHERE DOC_EQUMBRUND.equmbrcod=DOC_EQUMBR.cod) END as mbrtypecodvalue,CASE DOC_EQUMBR.mbrtypecod WHEN 9 THEN (SELECT TOP 1 EMP.dsc FROM EMP WHERE EMP.cod= (SELECT TOP 1 DOC_EQUMBREMP.empcod FROM DOC_EQUMBREMP WHERE DOC_EQUMBREMP.equmbrcod=DOC_EQUMBR.cod)) WHEN 10 THEN (SELECT TOP 1 UND.dsc FROM UND WHERE UND.cod= (SELECT TOP 1 DOC_EQUMBRUND.undcod FROM DOC_EQUMBRUND WHERE DOC_EQUMBRUND.equmbrcod=DOC_EQUMBR.cod)) END as mbrtypecoddsc,(DOC_EQUMBRQSECSID.secdsc) as qsecsiddsc,DOC_EQUMBR.qsecdatetime,DOC_EQUMBR.equcod,DOC_EQUMBR.qsecsid FROM DOC_EQUMBR  LEFT JOIN DOC_EQU AS DOC_EQUMBREQUCOD ON DOC_EQUMBREQUCOD.cod=DOC_EQUMBR.equcod LEFT JOIN Q_SECPLOGIN AS DOC_EQUMBRQSECSID ON DOC_EQUMBRQSECSID.sidcod=DOC_EQUMBR.qsecsid  WHERE DOC_EQUMBR.cod=@cod" onselected="dsfrmupdpanelctrl000004_Selected" UpdateCommandType="Text"
 UpdateCommand="UPDATE DOC_EQUMBR SET cod=@cod,mbrtypecod=@mbrtypecod WHERE DOC_EQUMBR.cod=@cod" onupdated="dsfrmupdpanelctrl000004_Updated" InsertCommandType="Text"
 InsertCommand="DECLARE @querynextcodcod int  SET @querynextcodcod =(SELECT ISNULL(MAX(cod),0)+1 FROM DOC_EQUMBR WHERE cod > 0 ) INSERT INTO DOC_EQUMBR (cod,equcod,mbrtypecod) VALUES(@querynextcodcod,@equcod,@mbrtypecod) SELECT @querynextcod =@querynextcodcod" onInserted="dsfrmupdpanelctrl000004_Inserted" >
<InsertParameters>
<asp:ControlParameter  Name="equcod" ControlID="frmdatos" ConvertEmptyStringToNull="True" Direction="Input" Type="String" Size="200" PropertyName="SelectedValue" />
<asp:ControlParameter  Name="mbrtypecod" ControlID="frmupdpanelfrmupdpanelctrl000004$insmbrtypecod" ConvertEmptyStringToNull="True" Direction="InputOutput" Type="Int32" PropertyName="Value" />
<asp:Parameter  Name="querynextcod" Direction="Output" Type="Int32" />
</InsertParameters>
<SelectParameters>
<asp:Parameter  Name="cod" Direction="InputOutput" Type="String" Size="200" />
</SelectParameters>
<UpdateParameters>
<asp:Parameter  Name="cod" Direction="InputOutput" Type="String" Size="200" />
<asp:ControlParameter  Name="mbrtypecod" ControlID="frmupdpanelfrmupdpanelctrl000004$edtmbrtypecod" ConvertEmptyStringToNull="True" Direction="InputOutput" Type="Int32" PropertyName="Value" />
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
</td></tr></table>
<div  >
<!-- Panel pnlobjectexplorer -->
<asp:Label  runat="server" ID="lblpnlpnlobjectexplorer" width="100%" />
<ajaxkit:ModalPopupExtender  runat="server" ID="pnlobjectexplorer" DropShadow="False" EnableViewState="True" PopupControlID="pnlpnlobjectexplorer" TargetControlID="lblpnlpnlobjectexplorer" BackgroundCssClass="bgmodalpopup" />
<asp:Panel  ID="pnlpnlobjectexplorer" runat="server" BorderWidth="1" BorderStyle="solid" >
<asp:UpdatePanel  ID="updpnlobjectexplorer" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="True" >
<Triggers>
  <asp:AsyncPostBackTrigger  ControlID="tvwmodalpopuppnlobjectexplorer" EventName="SelectedNodeChanged" />
  <asp:AsyncPostBackTrigger  ControlID="grdobjectexplorer" EventName="RowCommand" />
</Triggers>
<ContentTemplate>
<table class="form" cellpadding="0" cellspacing="0"><tr><td  colspan="1" >
<table style="width:700px;height:300px; border: 2px solid #000000; padding: 2px; margin: 1px; background-color: #F0F0F0; vertical-align: top; text-align: left;">
<tr><td  class="tabla-titulo" colspan="2" >Seleccionar</td></tr>
<tr>
<td  class="search-title" style="height:15px;width:700px" colspan="2" >
<asp:Button  runat="server" ID="cmdobjectexplorercancel" Text="Cancelar" CssClass="boton-acciones" CausesValidation="False" OnClick="cmdobjectexplorercancel_Click" />
<asp:Label  runat="server" ID="lblobjectexplorerfilter" width="100" Text='Filtro rápido:' />
<asp:TextBox  runat="server" ID="txtobjectexplorerfilter" CssClass="form-control" Width="200px" MaxLength="20" />
<asp:CompareValidator  runat="server" ID="vcdvaltxtobjectexplorerfilter" SetFocusOnError="true" CssClass="error" ControltoValidate="txtobjectexplorerfilter" Display="Dynamic" Text="Ingrese un texto" ErrorMessage=":Ingrese un texto" Type="String" Operator="DataTypeCheck" />
<asp:RegularExpressionValidator  runat="server" ID="vrgvaltxtobjectexplorerfilter" SetFocusOnError="true" CssClass="error" Display="Dynamic" ControltoValidate="txtobjectexplorerfilter" ValidationExpression="[\w\s-+/*^()!|°$%&=-_,:;.ñÑ~áéíóúÁÉÍÓÚ#}{¿]{0,20}" Text="No mayor a 20 caracteres. Deben ser letras o numeros." ErrorMessage=":No mayor a 20 caracteres. Deben ser letras o numeros." />
<ajaxkit:ValidatorCalloutExtender  runat="server" ID="ajaxvrgtxtobjectexplorerfilter" TargetControlID="vrgvaltxtobjectexplorerfilter" />

<asp:Button  runat="server" ID="cmdobjectexplorerfilter" Text="Buscar" CssClass="boton-acciones" CausesValidation="False" OnClick="cmdobjectexplorerfilter_Click" />
</td>
</tr>
<tr>
<td  style="vertical-align:top; text-align:left;width:200px;height:315px;background-color:#F0F0F0;" colspan="1" >
<div style="height:315px;overflow-y:scroll;">
<asp:TreeView  runat="server" ID="tvwmodalpopuppnlobjectexplorer" ShowLines="True" AutoGenerateDataBindings="False" Width="200" NodeWrap="True" NodeIndent="5" ForeColor="Black" SelectedNodeStyle-BackColor="#C0C0C0" SelectedNodeStyle-CssClass="objectexplorer-selectnode" OnSelectedNodeChanged="tvwmodalpopuppnlobjectexplorer_SelectedNodeChanged" />
</div>
</td>
<td  style="vertical-align:top; text-align:left;width:450px;height:315px;" colspan="1" >
<div style="height:315px;width:450px; overflow-x:scroll;">
<asp:GridView  runat="server" ID="grdobjectexplorer" AllowPaging="True" AllowSorting="True" AutoGenerateColumns="False" PageSize="9"
 GridLines="None" ShowHeader="False" ShowFooter="False" Width="100%" UseAccessibleHeader="False" DataKeyNames="cod,objecttype" CellPadding="2" onRowCreated="grdobjectexplorer_RowCreated" onRowCommand="grdobjectexplorer_RowCommand" OnSelectedIndexChanged="grdobjectexplorer_SelectedIndexChanged" OnPageIndexChanging="grdobjectexplorer_PageIndexChanging" >
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
<asp:Button  runat="server" ID="grdobjectexplorerselectrow" Text="Seleccionar" CssClass="boton-acciones" CommandName="Select" CausesValidation="False" /></ItemTemplate>
</asp:TemplateField>
<asp:TemplateField   ItemStyle-CssClass="tabla-celda" HeaderStyle-CssClass="tabla-titulo" FooterStyle-CssClass="tabla-celda">
<ItemStyle  HorizontalAlign="Left" Width="12px" CssClass="searchresult-table-cell" />
<ItemTemplate>
<asp:Image  runat="server" ID="grdobjectexplorerrowimageimg" Width="12px" GenerateEmptyAlternateText="True" ImageURL='<%# "~/imagenes/icon" & format(Eval("objecttype"),"00000000") & ".png" %>' BorderColor="LightGray" BorderWidth="1" BorderStyle="Solid" AlternateText="Imagen no disp." />
</ItemTemplate>
</asp:TemplateField>
<asp:TemplateField   ItemStyle-CssClass="tabla-celda" HeaderStyle-CssClass="tabla-titulo" FooterStyle-CssClass="tabla-celda">
<ItemStyle  HorizontalAlign="Left" />
<ItemTemplate>
<asp:LinkButton runat="server" ID="grdobjectexplorerrowlabel" Text='<%# Eval("dsc") %>' CssClass="boton-acciones" CommandName="Select" CausesValidation="False" /></ItemTemplate>
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
<asp:UpdateProgress  runat="server" AssociatedUpdatePanelID="updpnlobjectexplorer" >
<ProgressTemplate><span  class="ajaxupd" ><div style="position:absolute;top:2px;left:50%; border-color:#333333; border:1; border-style:solid; background-color:#FFFFFF;padding:3px;"><image src="imagenes/ajaxupdate.gif" border=0 />Actualizando</div></span></ProgressTemplate>
</asp:UpdateProgress>
<!-- Fin panel pnlobjectexplorer -->


</div>
</asp:Content>

