<%@ Page Language="VB" CodeFile="frmTiposdedocumentos_det.aspx.vb" MasterPageFile="general.master" AutoEventWireup="True" Inherits="frmTiposdedocumentos_det" Title="Tipos de documentos" %>
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
<tr><td  style="width:32px;" colspan="1" ><img  src="imagenes/icon00000004.png" width="32px" height="32px" alt="Tipos de documentos" hspace="4"/></td>
<td  style="text-align:left;" colspan="1" ><span class="form-title">Tipos de documentos</span> |<asp:Label  runat="server" ID="lblsubtitle" CssClass="form-subtitle" />
</td></tr>
</table>
</td></tr>
<tr><td  colspan="1" >				<!-- Init -->
<table id="body" style="background-color:#F0F0F0;" width="100%">
					<br /><asp:Label  runat="server" ID="lblerror" CssClass="error" width="100%" />			<asp:ValidationSummary  runat="server" ID="frmdatosvalsumary" />
<asp:FormView  ID="frmdatos" runat="server" DataSourceID="dsdatos" DataKeyNames="cod" DefaultMode="ReadOnly" OnItemDeleted="frmdatos_ItemDeleted" OnItemUpdated="frmdatos_ItemUpdated" OnItemInserted="frmdatos_ItemInserted" OnItemUpdating="frmdatos_ItemUpdating" OnItemInserting="frmdatos_ItemInserting" OnDataBound="frmdatos_ItemSelected" >
<ItemTemplate>

<!-- Init Plantilla item --><div  runat="server" Visible='<%# Request.Querystring("_view_")<> 1 %>' ><asp:Button  runat="server" ID="cmdFormViewConfirmDelete" Text="Borrar" CssClass="boton-acciones" CommandName="Delete" CausesValidation="True" Style="color:#FF0000; font-style:italic;" onClientClick="javascript:return confirm('Confirma la eliminación?')" />
<asp:Button  runat="server" ID="cmdrefresh" Text="Refrescar" CssClass="boton-acciones" CausesValidation="False" />
<asp:Button  runat="server" ID="cmdFormViewItemCancel" Text="Cancelar" CssClass="boton-acciones" CausesValidation="False" OnClick="cmdFormViewItemCancel_Click" />
<asp:Button  runat="server" ID="cmdSecPermView" Text="Permisos" CssClass="boton-acciones" CausesValidation="False" OnDataBinding="cmdSecPermView_DataBinding" onClientClick='<%# "javascript:if (window.showModalDialog) {window.showModalDialog(" & chr(39) & "frmsecperm.aspx??_mode_=0&_closea_=1&isDlg=1&param1=-1&timestamp=" & chr(39) & " + new Date().getTime() + " & chr(39) & "" & chr(39) & ", " & chr(39) & "" & chr(39) & ", " & chr(39) & "menubar=no;resizable=yes;toolbar=no;status=no;center=yes;dialogWidth=750px;dialogHeight=570px;" & chr(39) & ");} else {window.open(" & chr(39) & "frmsecperm.aspx??_mode_=0&_closea_=1&isDlg=1&param1=-1&timestamp=" & chr(39) & " + new Date().getTime() + " & chr(39) & "" & chr(39) & ", " & chr(39) & "" & chr(39) & ", " & chr(39) & "modal=yes;directories=no;menubar=no;resizable=yes;toolbar=no;status=no;center=yes;dialogWidth=750px;dialogHeight=570px;" & chr(39) & ");};return false;" %>' />
<asp:Button  runat="server" ID="cmdFormViewItemUpdate" Text="Editar" CssClass="boton-acciones" CausesValidation="False" OnClick="cmdFormViewItemUpdate_Click" OnDataBinding="cmdFormViewItemUpdate_DataBinding" />
</div>
<table width="100%" cellpadding="1" cellspacing="1"><tr>
<td  colspan="1" >
Codigo
</td>
<td  colspan="1" >
<asp:Label  runat="server" ID="itemctrl000033" CssClass="form-control-read" width="100%" OnDataBinding="itemctrl000033_Databound" />
</td>
</tr>
<tr>
<td  colspan="2" >
<ajaxkit:TabContainer ID="itemtabPanel" runat="server" >
<ajaxkit:TabPanel ID="itemtabPanel001" runat="server" HeaderText="General" Tooltip="General" >
<ContentTemplate>
<table>
<tr>
<td  colspan="1" >
Descripcion
</td>
<td  colspan="1" >
<asp:Label  runat="server" ID="itemctrl000031" CssClass="form-control-read" width="100%" OnDataBinding="itemctrl000031_Databound" />
</td>
</tr>
<tr>
<td  colspan="1" >
Orden
</td>
<td  colspan="1" >
<asp:Label  runat="server" ID="itemctrl000030" CssClass="form-control-read" width="100%" OnDataBinding="itemctrl000030_Databound" />
</td>
</tr>
<tr>
<td  colspan="1" >
Abreviatura
</td>
<td  colspan="1" >
<asp:Label  runat="server" ID="itemctrl000029" CssClass="form-control-read" width="100%" OnDataBinding="itemctrl000029_Databound" />
</td>
</tr>
<tr>
<td  colspan="1" >
No permite documentos especificos
</td>
<td  colspan="1" >
<asp:Label  runat="server" ID="itemctrl000028" CssClass="form-control-read" width="100%" OnDataBinding="itemctrl000028_Databound" />
</td>
</tr>
<tr>
<td  colspan="1" >
Formato general
</td>
<td  colspan="1" >
<asp:Label  runat="server" ID="itemctrl000027" CssClass="form-control-read" width="100%" OnDataBinding="itemctrl000027_Databound" />
</td>
</tr>
<tr>
<td  colspan="1" >
Formato específico predeterminado
</td>
<td  colspan="1" >
<asp:Label  runat="server" ID="itemctrl000026" CssClass="form-control-read" width="100%" OnDataBinding="itemctrl000026_Databound" />
</td>
</tr>
<tr>
<td  colspan="2" >
Plantilla cabecera
</td>
</tr>
<tr>
<td  colspan="2" >
<asp:Label  runat="server" ID="itemctrl000025" CssClass="form-control-read" width="100%" OnDataBinding="itemctrl000025_Databound" BorderStyle="Solid" BorderWidth="1" BorderColor="LightGray" />
</td>
</tr>
<tr>
<td  colspan="2" >
Plantilla cuerpo
</td>
</tr>
<tr>
<td  colspan="2" >
<asp:Label  runat="server" ID="itemctrl000024" CssClass="form-control-read" width="100%" OnDataBinding="itemctrl000024_Databound" BorderStyle="Solid" BorderWidth="1" BorderColor="LightGray" />
</td>
</tr>
<tr>
<td  colspan="1" >
Plantilla pie personalizada
</td>
<td  colspan="1" >
<asp:Label  runat="server" ID="itemctrl000023" CssClass="form-control-read" width="100%" OnDataBinding="itemctrl000023_Databound" />
</td>
</tr>
<tr>
<td  colspan="1" >
Plantilla pie izquierda
</td>
<td  colspan="1" >
<asp:Label  runat="server" ID="itemctrl000022" CssClass="form-control-read" width="100%" OnDataBinding="itemctrl000022_Databound" />
</td>
</tr>
<tr>
<td  colspan="1" >
Plantilla pie centro
</td>
<td  colspan="1" >
<asp:Label  runat="server" ID="itemctrl000021" CssClass="form-control-read" width="100%" OnDataBinding="itemctrl000021_Databound" />
</td>
</tr>
<tr>
<td  colspan="1" >
Plantilla pie derecha
</td>
<td  colspan="1" >
<asp:Label  runat="server" ID="itemctrl000020" CssClass="form-control-read" width="100%" OnDataBinding="itemctrl000020_Databound" />
</td>
</tr>
</table>
</ContentTemplate>
</ajaxkit:TabPanel>
<ajaxkit:TabPanel ID="itemtabPanel002" runat="server" HeaderText="Vigente" Tooltip="Vigente" >
<ContentTemplate>
<table>
<tr>
<td  colspan="1" >
Permite cambiar datos en estado vigente
</td>
<td  colspan="1" >
<asp:Label  runat="server" ID="itemctrl000019" CssClass="form-control-read" width="100%" OnDataBinding="itemctrl000019_Databound" />
</td>
</tr>
<tr>
<td  colspan="1" >
Permite cambiar título en estado vigente
</td>
<td  colspan="1" >
<asp:Label  runat="server" ID="itemctrl000018" CssClass="form-control-read" width="100%" OnDataBinding="itemctrl000018_Databound" />
</td>
</tr>
</table>
</ContentTemplate>
</ajaxkit:TabPanel>
<ajaxkit:TabPanel ID="itemtabPanel003" runat="server" HeaderText="Edición" Tooltip="Edición" >
<ContentTemplate>
<table>
<tr>
<td  colspan="1" >
Permite cambiar el título en edición
</td>
<td  colspan="1" >
<asp:Label  runat="server" ID="itemctrl000017" CssClass="form-control-read" width="100%" OnDataBinding="itemctrl000017_Databound" />
</td>
</tr>
<tr>
<td  colspan="1" >
Permite cambiar datos de clasificación en edición
</td>
<td  colspan="1" >
<asp:Label  runat="server" ID="itemctrl000016" CssClass="form-control-read" width="100%" OnDataBinding="itemctrl000016_Databound" />
</td>
</tr>
<tr>
<td  colspan="1" >
Permite cambiar roles en edición
</td>
<td  colspan="1" >
<asp:Label  runat="server" ID="itemctrl000015" CssClass="form-control-read" width="100%" OnDataBinding="itemctrl000015_Databound" />
</td>
</tr>
</table>
</ContentTemplate>
</ajaxkit:TabPanel>
<ajaxkit:TabPanel ID="itemtabPanel004" runat="server" HeaderText="Opciones avanzadas" Tooltip="Opciones avanzadas" >
<ContentTemplate>
<table>
<tr>
<td  colspan="1" >
Siempre permite cambiar roles
</td>
<td  colspan="1" >
<asp:Label  runat="server" ID="itemctrl000014" CssClass="form-control-read" width="100%" OnDataBinding="itemctrl000014_Databound" />
</td>
</tr>
<tr>
<td  colspan="1" >
Rol editor opcional
</td>
<td  colspan="1" >
<asp:Label  runat="server" ID="itemctrl000013" CssClass="form-control-read" width="100%" OnDataBinding="itemctrl000013_Databound" />
</td>
</tr>
<tr>
<td  colspan="1" >
Rol revisor opcional
</td>
<td  colspan="1" >
<asp:Label  runat="server" ID="itemctrl000012" CssClass="form-control-read" width="100%" OnDataBinding="itemctrl000012_Databound" />
</td>
</tr>
<tr>
<td  colspan="1" >
Rol aprobador opcional
</td>
<td  colspan="1" >
<asp:Label  runat="server" ID="itemctrl000011" CssClass="form-control-read" width="100%" OnDataBinding="itemctrl000011_Databound" />
</td>
</tr>
<tr>
<td  colspan="1" >
Rol cancelador opcional
</td>
<td  colspan="1" >
<asp:Label  runat="server" ID="itemctrl000010" CssClass="form-control-read" width="100%" OnDataBinding="itemctrl000010_Databound" />
</td>
</tr>
<tr>
<td  colspan="1" >
Rol publicador opcional
</td>
<td  colspan="1" >
<asp:Label  runat="server" ID="itemctrl000009" CssClass="form-control-read" width="100%" OnDataBinding="itemctrl000009_Databound" />
</td>
</tr>
<tr>
<td  colspan="1" >
Rol lector opcional
</td>
<td  colspan="1" >
<asp:Label  runat="server" ID="itemctrl000008" CssClass="form-control-read" width="100%" OnDataBinding="itemctrl000008_Databound" />
</td>
</tr>
<tr>
<td  colspan="1" >
Permite reedición
</td>
<td  colspan="1" >
<asp:Label  runat="server" ID="itemctrl000007" CssClass="form-control-read" width="100%" OnDataBinding="itemctrl000007_Databound" />
</td>
</tr>
<tr>
<td  colspan="1" >
Auto edición habilitada
</td>
<td  colspan="1" >
<asp:Label  runat="server" ID="itemctrl000006" CssClass="form-control-read" width="100%" OnDataBinding="itemctrl000006_Databound" />
</td>
</tr>
<tr>
<td  colspan="1" >
Días de autoedición
</td>
<td  colspan="1" >
<asp:Label  runat="server" ID="itemctrl000005" CssClass="form-control-read" width="100%" OnDataBinding="itemctrl000005_Databound" />
</td>
</tr>
<tr>
<td  colspan="1" >
Logo
</td>
<td  colspan="1" >
<asp:LinkButton  runat="server" ID="itemctrl000004href" CssClass="form-control" OnClientClick='<%# "javascript:if (window.showModalDialog) {window.showModalDialog(" & chr(39) & "getbinary.aspx?prv=0&id=" & Eval("logo") & "&timestamp=" & chr(39) & " + new Date().getTime() + " & chr(39) & "" & chr(39) & ", " & chr(39) & "" & chr(39) & ", " & chr(39) & "menubar=no;resizable=yes;toolbar=no;status=no;center=yes;dialogWidth=750px;dialogHeight=570px;" & chr(39) & ");} else {window.open(" & chr(39) & "getbinary.aspx?prv=0&id=" & Eval("logo") & "&timestamp=" & chr(39) & " + new Date().getTime() + " & chr(39) & "" & chr(39) & ", " & chr(39) & "" & chr(39) & ", " & chr(39) & "modal=yes;directories=no;menubar=no;resizable=yes;toolbar=no;status=no;center=yes;dialogWidth=750px;dialogHeight=570px;" & chr(39) & ");};return false;" %>' Visible='<%# iif(Eval("logo").ToString <> "-1","True","False")  %>' >
<asp:Image  runat="server" ID="itemctrl000004img" Text="Logo" CssClass="form-control" Width="50px" Visible='<%# iif(Eval("logo").ToString <> "-1","True","False")  %>' GenerateEmptyAlternateText="True" ImageURL='<%# "getbinary.aspx?prv=1&id=" & Eval("logo") %>' BorderColor="LightGray" BorderWidth="1" BorderStyle="Solid" AlternateText="Imagen no disp." />
</asp:LinkButton>

</td>
</tr>
</table>
</ContentTemplate>
</ajaxkit:TabPanel>
<ajaxkit:TabPanel ID="itemtabPanel005" runat="server" HeaderText="Estado y auditoría" Tooltip="Estado y auditoría" >
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
<td  colspan="2" >
<ajaxkit:TabContainer ID="edittabPanel" runat="server" >
<ajaxkit:TabPanel ID="edittabPanel001" runat="server" HeaderText="General <span class='error'><b>*</b></span>" Tooltip="General" >
<ContentTemplate>
<table>
<tr>
<td  colspan="1" >
<b>Descripcion</b>
</td>
<td  colspan="1" >
<asp:TextBox  runat="server" ID="editctrl000031" CssClass="form-control" Width="400px" MaxLength="50" OnDataBinding="editctrl000031_DataBound_edittabPanel001" Tooltip="Campo4[]" TextMode="MultiLine" onkeypress="return textCounter(this,50);" onkeydown="return textCounter(this,50);" onkeyup="return textCounter(this,50);" onfocus="return textCounter_GetFocus(this,50);" onblur="return textCounter_LostFocus(this);" /> <span class='error'><b>*</b></span>
<asp:RequiredFieldValidator  runat="server" ID="vrqeditctrl000031" SetFocusOnError="true" CssClass="error" Display="Dynamic" ControltoValidate="editctrl000031" Text="Obligatorio!!! ErrorMessage=´Descripcion:es un dato obligatorio!´" ValidationGroup="vgedit" />
<ajaxkit:ValidatorCalloutExtender  runat="server" ID="ajaxvrqeditctrl000031" TargetControlID="vrqeditctrl000031" /><asp:CompareValidator  runat="server" ID="vcdvaleditctrl000031" SetFocusOnError="true" CssClass="error" ControltoValidate="editctrl000031" Display="Dynamic" Text="Ingrese un texto" ErrorMessage="Descripcion:Ingrese un texto" Type="String" Operator="DataTypeCheck" ValidationGroup="vgedit" />
<asp:RegularExpressionValidator  runat="server" ID="vrgvaleditctrl000031" SetFocusOnError="true" CssClass="error" Display="Dynamic" ControltoValidate="editctrl000031" ValidationExpression="[\w\s-+/*^()!|°$%&=-_,:;.ñÑ~áéíóúÁÉÍÓÚ#}{¿]{0,50}" Text="No mayor a 50 caracteres. Deben ser letras o numeros." ErrorMessage="Descripcion:No mayor a 50 caracteres. Deben ser letras o numeros." ValidationGroup="vgedit" />
<ajaxkit:ValidatorCalloutExtender  runat="server" ID="ajaxvrgeditctrl000031" TargetControlID="vrgvaleditctrl000031" />

</td>
</tr>
<tr>
<td  colspan="1" >
Orden
</td>
<td  colspan="1" >
<asp:TextBox  runat="server" ID="editctrl000030" CssClass="form-control" Width="104px" MaxLength="8" OnDataBinding="editctrl000030_DataBound_edittabPanel001" Tooltip="Campo223[]" />
<asp:CompareValidator  runat="server" ID="vcdvaleditctrl000030" SetFocusOnError="true" CssClass="error" ControltoValidate="editctrl000030" Display="Dynamic" Text='Ingrese un entero' ErrorMessage='Orden:Ingrese un entero' Type="Integer" Operator="DataTypeCheck" ValidationGroup="vgedit" />
<asp:RangeValidator  runat="server" ID="vrnvaleditctrl000030" SetFocusOnError="true" CssClass="error" Display="Dynamic" ControltoValidate="editctrl000030" Text="Ingrese un valor entre 1 y 2000000000" ErrorMessage="Orden:Ingrese un valor entre 1 y 2000000000" Type="Integer" CultureInvariantValues="True" MaximumValue="2000000000" MinimumValue="1" ValidationGroup="vgedit" />
<ajaxkit:ValidatorCalloutExtender  runat="server" ID="ajaxvrneditctrl000030" TargetControlID="vrnvaleditctrl000030" />

</td>
</tr>
<tr>
<td  colspan="1" >
Abreviatura
</td>
<td  colspan="1" >
<asp:TextBox  runat="server" ID="editctrl000029" CssClass="form-control" Width="400px" MaxLength="50" OnDataBinding="editctrl000029_DataBound_edittabPanel001" Tooltip="Campo9[]" TextMode="MultiLine" onkeypress="return textCounter(this,50);" onkeydown="return textCounter(this,50);" onkeyup="return textCounter(this,50);" onfocus="return textCounter_GetFocus(this,50);" onblur="return textCounter_LostFocus(this);" />
<asp:CompareValidator  runat="server" ID="vcdvaleditctrl000029" SetFocusOnError="true" CssClass="error" ControltoValidate="editctrl000029" Display="Dynamic" Text="Ingrese un texto" ErrorMessage="Abreviatura:Ingrese un texto" Type="String" Operator="DataTypeCheck" ValidationGroup="vgedit" />
<asp:RegularExpressionValidator  runat="server" ID="vrgvaleditctrl000029" SetFocusOnError="true" CssClass="error" Display="Dynamic" ControltoValidate="editctrl000029" ValidationExpression="[\w\s-+/*^()!|°$%&=-_,:;.ñÑ~áéíóúÁÉÍÓÚ#}{¿]{0,50}" Text="No mayor a 50 caracteres. Deben ser letras o numeros." ErrorMessage="Abreviatura:No mayor a 50 caracteres. Deben ser letras o numeros." ValidationGroup="vgedit" />
<ajaxkit:ValidatorCalloutExtender  runat="server" ID="ajaxvrgeditctrl000029" TargetControlID="vrgvaleditctrl000029" />

</td>
</tr>
<tr>
<td  colspan="1" >
No permite documentos especificos
</td>
<td  colspan="1" >
<asp:RadioButtonList  runat="server" ID="editctrl000028" CssClass="form-control" RepeatLayout="Flow" RepeatDirection="Horizontal" OnDataBinding="editctrl000028_Databound" >
<asp:ListItem  Text="Sí" Value="True" />
<asp:ListItem  Text="No" Value="False" />
<asp:ListItem  Text="Sin definir" Value="" Selected="True" />
</asp:RadioButtonList>

</td>
</tr>
<tr>
<td  colspan="1" >
Formato general
</td>
<td  colspan="1" >
<asp:TextBox  runat="server" ID="editctrl000027" CssClass="form-control" Width="400px" MaxLength="50" OnDataBinding="editctrl000027_DataBound_edittabPanel001" Tooltip="Campo41[Formato de identificación. Se pueden utilizar las siguientes variables:{#s#} código de sector, {#n#} código de apartado. {#z#} nro. de documento]" TextMode="MultiLine" onkeypress="return textCounter(this,50);" onkeydown="return textCounter(this,50);" onkeyup="return textCounter(this,50);" onfocus="return textCounter_GetFocus(this,50);" onblur="return textCounter_LostFocus(this);" />
<asp:CompareValidator  runat="server" ID="vcdvaleditctrl000027" SetFocusOnError="true" CssClass="error" ControltoValidate="editctrl000027" Display="Dynamic" Text="Ingrese un texto" ErrorMessage="Formato general:Ingrese un texto" Type="String" Operator="DataTypeCheck" ValidationGroup="vgedit" />
<asp:RegularExpressionValidator  runat="server" ID="vrgvaleditctrl000027" SetFocusOnError="true" CssClass="error" Display="Dynamic" ControltoValidate="editctrl000027" ValidationExpression="[\w\s-+/*^()!|°$%&=-_,:;.ñÑ~áéíóúÁÉÍÓÚ#}{¿]{0,50}" Text="No mayor a 50 caracteres. Deben ser letras o numeros." ErrorMessage="Formato general:No mayor a 50 caracteres. Deben ser letras o numeros." ValidationGroup="vgedit" />
<ajaxkit:ValidatorCalloutExtender  runat="server" ID="ajaxvrgeditctrl000027" TargetControlID="vrgvaleditctrl000027" />

</td>
</tr>
<tr>
<td  colspan="1" >
<b>Formato específico predeterminado</b>
</td>
<td  colspan="1" >
<asp:TextBox  runat="server" ID="editctrl000026" CssClass="form-control" Width="400px" MaxLength="50" OnDataBinding="editctrl000026_DataBound_edittabPanel001" Tooltip="Campo49[Formato de identificación predeterminado. Se utilizando cuando la unidad especificada no tiene definido formato. Se pueden utilizar las siguientes variables:{#s#} código de sector, {#n#} código de apartado. {#z#} nro. de documento]" TextMode="MultiLine" onkeypress="return textCounter(this,50);" onkeydown="return textCounter(this,50);" onkeyup="return textCounter(this,50);" onfocus="return textCounter_GetFocus(this,50);" onblur="return textCounter_LostFocus(this);" /> <span class='error'><b>*</b></span>
<asp:RequiredFieldValidator  runat="server" ID="vrqeditctrl000026" SetFocusOnError="true" CssClass="error" Display="Dynamic" ControltoValidate="editctrl000026" Text="Obligatorio!!! ErrorMessage=´Formato específico predeterminado:es un dato obligatorio!´" ValidationGroup="vgedit" />
<ajaxkit:ValidatorCalloutExtender  runat="server" ID="ajaxvrqeditctrl000026" TargetControlID="vrqeditctrl000026" /><asp:CompareValidator  runat="server" ID="vcdvaleditctrl000026" SetFocusOnError="true" CssClass="error" ControltoValidate="editctrl000026" Display="Dynamic" Text="Ingrese un texto" ErrorMessage="Formato específico predeterminado:Ingrese un texto" Type="String" Operator="DataTypeCheck" ValidationGroup="vgedit" />
<asp:RegularExpressionValidator  runat="server" ID="vrgvaleditctrl000026" SetFocusOnError="true" CssClass="error" Display="Dynamic" ControltoValidate="editctrl000026" ValidationExpression="[\w\s-+/*^()!|°$%&=-_,:;.ñÑ~áéíóúÁÉÍÓÚ#}{¿]{0,50}" Text="No mayor a 50 caracteres. Deben ser letras o numeros." ErrorMessage="Formato específico predeterminado:No mayor a 50 caracteres. Deben ser letras o numeros." ValidationGroup="vgedit" />
<ajaxkit:ValidatorCalloutExtender  runat="server" ID="ajaxvrgeditctrl000026" TargetControlID="vrgvaleditctrl000026" />

</td>
</tr>
<tr>
<td  colspan="2" >
Plantilla cabecera
</td>
</tr>
<tr>
<td  colspan="2" >
<asp:HiddenField  runat="server" ID="editctrl000025" OnDataBinding="editctrl000025_DataBound"/><iframe  runat="server" id="fmeeditctrl000025" frameborder="0" width="500" height="200"></iframe><br />

</td>
</tr>
<tr>
<td  colspan="2" >
Plantilla cuerpo
</td>
</tr>
<tr>
<td  colspan="2" >
<asp:HiddenField  runat="server" ID="editctrl000024" OnDataBinding="editctrl000024_DataBound"/><iframe  runat="server" id="fmeeditctrl000024" frameborder="0" width="500" height="200"></iframe><br />

</td>
</tr>
<tr>
<td  colspan="1" >
Plantilla pie personalizada
</td>
<td  colspan="1" >
<asp:Checkbox  runat="server" ID="editctrl000023" CssClass="form-control" OnDataBinding="editctrl000023_Databound" />
</td>
</tr>
<tr>
<td  colspan="1" >
Plantilla pie izquierda
</td>
<td  colspan="1" >
<asp:TextBox  runat="server" ID="editctrl000022" CssClass="form-control" Width="400px" MaxLength="250" OnDataBinding="editctrl000022_DataBound_edittabPanel001" Tooltip="Campo217[]" TextMode="MultiLine" onkeypress="return textCounter(this,250);" onkeydown="return textCounter(this,250);" onkeyup="return textCounter(this,250);" onfocus="return textCounter_GetFocus(this,250);" onblur="return textCounter_LostFocus(this);" />
<asp:CompareValidator  runat="server" ID="vcdvaleditctrl000022" SetFocusOnError="true" CssClass="error" ControltoValidate="editctrl000022" Display="Dynamic" Text="Ingrese un texto" ErrorMessage="Plantilla pie izquierda:Ingrese un texto" Type="String" Operator="DataTypeCheck" ValidationGroup="vgedit" />
<asp:RegularExpressionValidator  runat="server" ID="vrgvaleditctrl000022" SetFocusOnError="true" CssClass="error" Display="Dynamic" ControltoValidate="editctrl000022" ValidationExpression="[\w\s-+/*^()!|°$%&=-_,:;.ñÑ~áéíóúÁÉÍÓÚ#}{¿]{0,250}" Text="No mayor a 250 caracteres. Deben ser letras o numeros." ErrorMessage="Plantilla pie izquierda:No mayor a 250 caracteres. Deben ser letras o numeros." ValidationGroup="vgedit" />
<ajaxkit:ValidatorCalloutExtender  runat="server" ID="ajaxvrgeditctrl000022" TargetControlID="vrgvaleditctrl000022" />

</td>
</tr>
<tr>
<td  colspan="1" >
Plantilla pie centro
</td>
<td  colspan="1" >
<asp:TextBox  runat="server" ID="editctrl000021" CssClass="form-control" Width="400px" MaxLength="250" OnDataBinding="editctrl000021_DataBound_edittabPanel001" Tooltip="Campo218[]" TextMode="MultiLine" onkeypress="return textCounter(this,250);" onkeydown="return textCounter(this,250);" onkeyup="return textCounter(this,250);" onfocus="return textCounter_GetFocus(this,250);" onblur="return textCounter_LostFocus(this);" />
<asp:CompareValidator  runat="server" ID="vcdvaleditctrl000021" SetFocusOnError="true" CssClass="error" ControltoValidate="editctrl000021" Display="Dynamic" Text="Ingrese un texto" ErrorMessage="Plantilla pie centro:Ingrese un texto" Type="String" Operator="DataTypeCheck" ValidationGroup="vgedit" />
<asp:RegularExpressionValidator  runat="server" ID="vrgvaleditctrl000021" SetFocusOnError="true" CssClass="error" Display="Dynamic" ControltoValidate="editctrl000021" ValidationExpression="[\w\s-+/*^()!|°$%&=-_,:;.ñÑ~áéíóúÁÉÍÓÚ#}{¿]{0,250}" Text="No mayor a 250 caracteres. Deben ser letras o numeros." ErrorMessage="Plantilla pie centro:No mayor a 250 caracteres. Deben ser letras o numeros." ValidationGroup="vgedit" />
<ajaxkit:ValidatorCalloutExtender  runat="server" ID="ajaxvrgeditctrl000021" TargetControlID="vrgvaleditctrl000021" />

</td>
</tr>
<tr>
<td  colspan="1" >
Plantilla pie derecha
</td>
<td  colspan="1" >
<asp:TextBox  runat="server" ID="editctrl000020" CssClass="form-control" Width="400px" MaxLength="250" OnDataBinding="editctrl000020_DataBound_edittabPanel001" Tooltip="Campo219[]" TextMode="MultiLine" onkeypress="return textCounter(this,250);" onkeydown="return textCounter(this,250);" onkeyup="return textCounter(this,250);" onfocus="return textCounter_GetFocus(this,250);" onblur="return textCounter_LostFocus(this);" />
<asp:CompareValidator  runat="server" ID="vcdvaleditctrl000020" SetFocusOnError="true" CssClass="error" ControltoValidate="editctrl000020" Display="Dynamic" Text="Ingrese un texto" ErrorMessage="Plantilla pie derecha:Ingrese un texto" Type="String" Operator="DataTypeCheck" ValidationGroup="vgedit" />
<asp:RegularExpressionValidator  runat="server" ID="vrgvaleditctrl000020" SetFocusOnError="true" CssClass="error" Display="Dynamic" ControltoValidate="editctrl000020" ValidationExpression="[\w\s-+/*^()!|°$%&=-_,:;.ñÑ~áéíóúÁÉÍÓÚ#}{¿]{0,250}" Text="No mayor a 250 caracteres. Deben ser letras o numeros." ErrorMessage="Plantilla pie derecha:No mayor a 250 caracteres. Deben ser letras o numeros." ValidationGroup="vgedit" />
<ajaxkit:ValidatorCalloutExtender  runat="server" ID="ajaxvrgeditctrl000020" TargetControlID="vrgvaleditctrl000020" />

</td>
</tr>
</table>
</ContentTemplate>
</ajaxkit:TabPanel>
<ajaxkit:TabPanel ID="edittabPanel002" runat="server" HeaderText="Vigente" Tooltip="Vigente" >
<ContentTemplate>
<table>
<tr>
<td  colspan="1" >
Permite cambiar datos en estado vigente
</td>
<td  colspan="1" >
<asp:Checkbox  runat="server" ID="editctrl000019" CssClass="form-control" OnDataBinding="editctrl000019_Databound" />
</td>
</tr>
<tr>
<td  colspan="1" >
Permite cambiar título en estado vigente
</td>
<td  colspan="1" >
<asp:Checkbox  runat="server" ID="editctrl000018" CssClass="form-control" OnDataBinding="editctrl000018_Databound" />
</td>
</tr>
</table>
</ContentTemplate>
</ajaxkit:TabPanel>
<ajaxkit:TabPanel ID="edittabPanel003" runat="server" HeaderText="Edición" Tooltip="Edición" >
<ContentTemplate>
<table>
<tr>
<td  colspan="1" >
Permite cambiar el título en edición
</td>
<td  colspan="1" >
<asp:Checkbox  runat="server" ID="editctrl000017" CssClass="form-control" OnDataBinding="editctrl000017_Databound" />
</td>
</tr>
<tr>
<td  colspan="1" >
Permite cambiar datos de clasificación en edición
</td>
<td  colspan="1" >
<asp:Checkbox  runat="server" ID="editctrl000016" CssClass="form-control" OnDataBinding="editctrl000016_Databound" />
</td>
</tr>
<tr>
<td  colspan="1" >
Permite cambiar roles en edición
</td>
<td  colspan="1" >
<asp:Checkbox  runat="server" ID="editctrl000015" CssClass="form-control" OnDataBinding="editctrl000015_Databound" />
</td>
</tr>
</table>
</ContentTemplate>
</ajaxkit:TabPanel>
<ajaxkit:TabPanel ID="edittabPanel004" runat="server" HeaderText="Opciones avanzadas" Tooltip="Opciones avanzadas" >
<ContentTemplate>
<table>
<tr>
<td  colspan="1" >
Siempre permite cambiar roles
</td>
<td  colspan="1" >
<asp:Checkbox  runat="server" ID="editctrl000014" CssClass="form-control" OnDataBinding="editctrl000014_Databound" />
</td>
</tr>
<tr>
<td  colspan="1" >
Rol editor opcional
</td>
<td  colspan="1" >
<asp:Checkbox  runat="server" ID="editctrl000013" CssClass="form-control" OnDataBinding="editctrl000013_Databound" />
</td>
</tr>
<tr>
<td  colspan="1" >
Rol revisor opcional
</td>
<td  colspan="1" >
<asp:Checkbox  runat="server" ID="editctrl000012" CssClass="form-control" OnDataBinding="editctrl000012_Databound" />
</td>
</tr>
<tr>
<td  colspan="1" >
Rol aprobador opcional
</td>
<td  colspan="1" >
<asp:Checkbox  runat="server" ID="editctrl000011" CssClass="form-control" OnDataBinding="editctrl000011_Databound" />
</td>
</tr>
<tr>
<td  colspan="1" >
Rol cancelador opcional
</td>
<td  colspan="1" >
<asp:Checkbox  runat="server" ID="editctrl000010" CssClass="form-control" OnDataBinding="editctrl000010_Databound" />
</td>
</tr>
<tr>
<td  colspan="1" >
Rol publicador opcional
</td>
<td  colspan="1" >
<asp:Checkbox  runat="server" ID="editctrl000009" CssClass="form-control" OnDataBinding="editctrl000009_Databound" />
</td>
</tr>
<tr>
<td  colspan="1" >
Rol lector opcional
</td>
<td  colspan="1" >
<asp:Checkbox  runat="server" ID="editctrl000008" CssClass="form-control" OnDataBinding="editctrl000008_Databound" />
</td>
</tr>
<tr>
<td  colspan="1" >
Permite reedición
</td>
<td  colspan="1" >
<asp:Checkbox  runat="server" ID="editctrl000007" CssClass="form-control" OnDataBinding="editctrl000007_Databound" />
</td>
</tr>
<tr>
<td  colspan="1" >
Auto edición habilitada
</td>
<td  colspan="1" >
<asp:RadioButtonList  runat="server" ID="editctrl000006" CssClass="form-control" RepeatLayout="Flow" RepeatDirection="Horizontal" OnDataBinding="editctrl000006_Databound" >
<asp:ListItem  Text="Sí" Value="True" />
<asp:ListItem  Text="No" Value="False" />
<asp:ListItem  Text="Sin definir" Value="" Selected="True" />
</asp:RadioButtonList>

</td>
</tr>
<tr>
<td  colspan="1" >
Días de autoedición
</td>
<td  colspan="1" >
<asp:TextBox  runat="server" ID="editctrl000005" CssClass="form-control" Width="120px" MaxLength="10" OnDataBinding="editctrl000005_DataBound_edittabPanel004" Tooltip="Campo259[]" />
<asp:CompareValidator  runat="server" ID="vcdvaleditctrl000005" SetFocusOnError="true" CssClass="error" ControltoValidate="editctrl000005" Display="Dynamic" Text='Ingrese un entero' ErrorMessage='Días de autoedición:Ingrese un entero' Type="Integer" Operator="DataTypeCheck" ValidationGroup="vgedit" />
<asp:RangeValidator  runat="server" ID="vrnvaleditctrl000005" SetFocusOnError="true" CssClass="error" Display="Dynamic" ControltoValidate="editctrl000005" Text="Ingrese un valor entre 1 y 2000000000" ErrorMessage="Días de autoedición:Ingrese un valor entre 1 y 2000000000" Type="Integer" CultureInvariantValues="True" MaximumValue="2000000000" MinimumValue="1" ValidationGroup="vgedit" />
<ajaxkit:ValidatorCalloutExtender  runat="server" ID="ajaxvrneditctrl000005" TargetControlID="vrnvaleditctrl000005" />

</td>
</tr>
<tr>
<td  colspan="1" >
Logo
</td>
<td  colspan="1" >
<asp:HiddenField  runat="server" ID="editctrl000004" OnDataBinding="editctrl000004_DataBound"/><iframe  runat="server" id="fmeeditctrl000004" frameborder="0" width="500" height="60"></iframe><br />
</td>
</tr>
</table>
</ContentTemplate>
</ajaxkit:TabPanel>
<ajaxkit:TabPanel ID="edittabPanel005" runat="server" HeaderText="Estado y auditoría" Tooltip="Estado y auditoría" >
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
<td  colspan="2" >
<ajaxkit:TabContainer ID="instabPanel" runat="server" >
<ajaxkit:TabPanel ID="instabPanel001" runat="server" HeaderText="General <span class='error'><b>*</b></span>" Tooltip="General" >
<ContentTemplate>
<table>
<tr>
<td  colspan="1" >
<b>Descripcion</b>
</td>
<td  colspan="1" >
<asp:TextBox  runat="server" ID="insctrl000031" CssClass="form-control" Width="400px" MaxLength="50" Tooltip="Campo4[]" TextMode="MultiLine" onkeypress="return textCounter(this,50);" onkeydown="return textCounter(this,50);" onkeyup="return textCounter(this,50);" onfocus="return textCounter_GetFocus(this,50);" onblur="return textCounter_LostFocus(this);" /> <span class='error'><b>*</b></span>
<asp:RequiredFieldValidator  runat="server" ID="vrqinsctrl000031" SetFocusOnError="true" CssClass="error" Display="Dynamic" ControltoValidate="insctrl000031" Text="Obligatorio!!! ErrorMessage=´Descripcion:es un dato obligatorio!´" ValidationGroup="vgins" />
<ajaxkit:ValidatorCalloutExtender  runat="server" ID="ajaxvrqinsctrl000031" TargetControlID="vrqinsctrl000031" /><asp:CompareValidator  runat="server" ID="vcdvalinsctrl000031" SetFocusOnError="true" CssClass="error" ControltoValidate="insctrl000031" Display="Dynamic" Text="Ingrese un texto" ErrorMessage="Descripcion:Ingrese un texto" Type="String" Operator="DataTypeCheck" ValidationGroup="vgins" />
<asp:RegularExpressionValidator  runat="server" ID="vrgvalinsctrl000031" SetFocusOnError="true" CssClass="error" Display="Dynamic" ControltoValidate="insctrl000031" ValidationExpression="[\w\s-+/*^()!|°$%&=-_,:;.ñÑ~áéíóúÁÉÍÓÚ#}{¿]{0,50}" Text="No mayor a 50 caracteres. Deben ser letras o numeros." ErrorMessage="Descripcion:No mayor a 50 caracteres. Deben ser letras o numeros." ValidationGroup="vgins" />
<ajaxkit:ValidatorCalloutExtender  runat="server" ID="ajaxvrginsctrl000031" TargetControlID="vrgvalinsctrl000031" />

</td>
</tr>
<tr>
<td  colspan="1" >
Orden
</td>
<td  colspan="1" >
<asp:TextBox  runat="server" ID="insctrl000030" CssClass="form-control" Width="104px" MaxLength="8" Tooltip="Campo223[]" />
<asp:CompareValidator  runat="server" ID="vcdvalinsctrl000030" SetFocusOnError="true" CssClass="error" ControltoValidate="insctrl000030" Display="Dynamic" Text='Ingrese un entero' ErrorMessage='Orden:Ingrese un entero' Type="Integer" Operator="DataTypeCheck" ValidationGroup="vgins" />
<asp:RangeValidator  runat="server" ID="vrnvalinsctrl000030" SetFocusOnError="true" CssClass="error" Display="Dynamic" ControltoValidate="insctrl000030" Text="Ingrese un valor entre 1 y 2000000000" ErrorMessage="Orden:Ingrese un valor entre 1 y 2000000000" Type="Integer" CultureInvariantValues="True" MaximumValue="2000000000" MinimumValue="1" ValidationGroup="vgins" />
<ajaxkit:ValidatorCalloutExtender  runat="server" ID="ajaxvrninsctrl000030" TargetControlID="vrnvalinsctrl000030" />

</td>
</tr>
<tr>
<td  colspan="1" >
Abreviatura
</td>
<td  colspan="1" >
<asp:TextBox  runat="server" ID="insctrl000029" CssClass="form-control" Width="400px" MaxLength="50" Tooltip="Campo9[]" TextMode="MultiLine" onkeypress="return textCounter(this,50);" onkeydown="return textCounter(this,50);" onkeyup="return textCounter(this,50);" onfocus="return textCounter_GetFocus(this,50);" onblur="return textCounter_LostFocus(this);" />
<asp:CompareValidator  runat="server" ID="vcdvalinsctrl000029" SetFocusOnError="true" CssClass="error" ControltoValidate="insctrl000029" Display="Dynamic" Text="Ingrese un texto" ErrorMessage="Abreviatura:Ingrese un texto" Type="String" Operator="DataTypeCheck" ValidationGroup="vgins" />
<asp:RegularExpressionValidator  runat="server" ID="vrgvalinsctrl000029" SetFocusOnError="true" CssClass="error" Display="Dynamic" ControltoValidate="insctrl000029" ValidationExpression="[\w\s-+/*^()!|°$%&=-_,:;.ñÑ~áéíóúÁÉÍÓÚ#}{¿]{0,50}" Text="No mayor a 50 caracteres. Deben ser letras o numeros." ErrorMessage="Abreviatura:No mayor a 50 caracteres. Deben ser letras o numeros." ValidationGroup="vgins" />
<ajaxkit:ValidatorCalloutExtender  runat="server" ID="ajaxvrginsctrl000029" TargetControlID="vrgvalinsctrl000029" />

</td>
</tr>
<tr>
<td  colspan="1" >
No permite documentos especificos
</td>
<td  colspan="1" >
<asp:RadioButtonList  runat="server" ID="insctrl000028" CssClass="form-control" RepeatLayout="Flow" RepeatDirection="Horizontal" >
<asp:ListItem  Text="Sí" Value="True" />
<asp:ListItem  Text="No" Value="False" />
<asp:ListItem  Text="Sin definir" Value="" Selected="True" />
</asp:RadioButtonList>

</td>
</tr>
<tr>
<td  colspan="1" >
Formato general
</td>
<td  colspan="1" >
<asp:TextBox  runat="server" ID="insctrl000027" CssClass="form-control" Width="400px" MaxLength="50" Tooltip="Campo41[Formato de identificación. Se pueden utilizar las siguientes variables:{#s#} código de sector, {#n#} código de apartado. {#z#} nro. de documento]" TextMode="MultiLine" onkeypress="return textCounter(this,50);" onkeydown="return textCounter(this,50);" onkeyup="return textCounter(this,50);" onfocus="return textCounter_GetFocus(this,50);" onblur="return textCounter_LostFocus(this);" />
<asp:CompareValidator  runat="server" ID="vcdvalinsctrl000027" SetFocusOnError="true" CssClass="error" ControltoValidate="insctrl000027" Display="Dynamic" Text="Ingrese un texto" ErrorMessage="Formato general:Ingrese un texto" Type="String" Operator="DataTypeCheck" ValidationGroup="vgins" />
<asp:RegularExpressionValidator  runat="server" ID="vrgvalinsctrl000027" SetFocusOnError="true" CssClass="error" Display="Dynamic" ControltoValidate="insctrl000027" ValidationExpression="[\w\s-+/*^()!|°$%&=-_,:;.ñÑ~áéíóúÁÉÍÓÚ#}{¿]{0,50}" Text="No mayor a 50 caracteres. Deben ser letras o numeros." ErrorMessage="Formato general:No mayor a 50 caracteres. Deben ser letras o numeros." ValidationGroup="vgins" />
<ajaxkit:ValidatorCalloutExtender  runat="server" ID="ajaxvrginsctrl000027" TargetControlID="vrgvalinsctrl000027" />

</td>
</tr>
<tr>
<td  colspan="1" >
<b>Formato específico predeterminado</b>
</td>
<td  colspan="1" >
<asp:TextBox  runat="server" ID="insctrl000026" CssClass="form-control" Width="400px" MaxLength="50" Tooltip="Campo49[Formato de identificación predeterminado. Se utilizando cuando la unidad especificada no tiene definido formato. Se pueden utilizar las siguientes variables:{#s#} código de sector, {#n#} código de apartado. {#z#} nro. de documento]" TextMode="MultiLine" onkeypress="return textCounter(this,50);" onkeydown="return textCounter(this,50);" onkeyup="return textCounter(this,50);" onfocus="return textCounter_GetFocus(this,50);" onblur="return textCounter_LostFocus(this);" /> <span class='error'><b>*</b></span>
<asp:RequiredFieldValidator  runat="server" ID="vrqinsctrl000026" SetFocusOnError="true" CssClass="error" Display="Dynamic" ControltoValidate="insctrl000026" Text="Obligatorio!!! ErrorMessage=´Formato específico predeterminado:es un dato obligatorio!´" ValidationGroup="vgins" />
<ajaxkit:ValidatorCalloutExtender  runat="server" ID="ajaxvrqinsctrl000026" TargetControlID="vrqinsctrl000026" /><asp:CompareValidator  runat="server" ID="vcdvalinsctrl000026" SetFocusOnError="true" CssClass="error" ControltoValidate="insctrl000026" Display="Dynamic" Text="Ingrese un texto" ErrorMessage="Formato específico predeterminado:Ingrese un texto" Type="String" Operator="DataTypeCheck" ValidationGroup="vgins" />
<asp:RegularExpressionValidator  runat="server" ID="vrgvalinsctrl000026" SetFocusOnError="true" CssClass="error" Display="Dynamic" ControltoValidate="insctrl000026" ValidationExpression="[\w\s-+/*^()!|°$%&=-_,:;.ñÑ~áéíóúÁÉÍÓÚ#}{¿]{0,50}" Text="No mayor a 50 caracteres. Deben ser letras o numeros." ErrorMessage="Formato específico predeterminado:No mayor a 50 caracteres. Deben ser letras o numeros." ValidationGroup="vgins" />
<ajaxkit:ValidatorCalloutExtender  runat="server" ID="ajaxvrginsctrl000026" TargetControlID="vrgvalinsctrl000026" />

</td>
</tr>
<tr>
<td  colspan="2" >
Plantilla cabecera
</td>
</tr>
<tr>
<td  colspan="2" >
<asp:HiddenField  runat="server" ID="insctrl000025" OnDataBinding="insctrl000025_DataBound"/><iframe  runat="server" id="fmeinsctrl000025" frameborder="0" width="500" height="200"></iframe><br />

</td>
</tr>
<tr>
<td  colspan="2" >
Plantilla cuerpo
</td>
</tr>
<tr>
<td  colspan="2" >
<asp:HiddenField  runat="server" ID="insctrl000024" OnDataBinding="insctrl000024_DataBound"/><iframe  runat="server" id="fmeinsctrl000024" frameborder="0" width="500" height="200"></iframe><br />

</td>
</tr>
<tr>
<td  colspan="1" >
Plantilla pie personalizada
</td>
<td  colspan="1" >
<asp:Checkbox  runat="server" ID="insctrl000023" CssClass="form-control" />
</td>
</tr>
<tr>
<td  colspan="1" >
Plantilla pie izquierda
</td>
<td  colspan="1" >
<asp:TextBox  runat="server" ID="insctrl000022" CssClass="form-control" Width="400px" MaxLength="250" Tooltip="Campo217[]" TextMode="MultiLine" onkeypress="return textCounter(this,250);" onkeydown="return textCounter(this,250);" onkeyup="return textCounter(this,250);" onfocus="return textCounter_GetFocus(this,250);" onblur="return textCounter_LostFocus(this);" />
<asp:CompareValidator  runat="server" ID="vcdvalinsctrl000022" SetFocusOnError="true" CssClass="error" ControltoValidate="insctrl000022" Display="Dynamic" Text="Ingrese un texto" ErrorMessage="Plantilla pie izquierda:Ingrese un texto" Type="String" Operator="DataTypeCheck" ValidationGroup="vgins" />
<asp:RegularExpressionValidator  runat="server" ID="vrgvalinsctrl000022" SetFocusOnError="true" CssClass="error" Display="Dynamic" ControltoValidate="insctrl000022" ValidationExpression="[\w\s-+/*^()!|°$%&=-_,:;.ñÑ~áéíóúÁÉÍÓÚ#}{¿]{0,250}" Text="No mayor a 250 caracteres. Deben ser letras o numeros." ErrorMessage="Plantilla pie izquierda:No mayor a 250 caracteres. Deben ser letras o numeros." ValidationGroup="vgins" />
<ajaxkit:ValidatorCalloutExtender  runat="server" ID="ajaxvrginsctrl000022" TargetControlID="vrgvalinsctrl000022" />

</td>
</tr>
<tr>
<td  colspan="1" >
Plantilla pie centro
</td>
<td  colspan="1" >
<asp:TextBox  runat="server" ID="insctrl000021" CssClass="form-control" Width="400px" MaxLength="250" Tooltip="Campo218[]" TextMode="MultiLine" onkeypress="return textCounter(this,250);" onkeydown="return textCounter(this,250);" onkeyup="return textCounter(this,250);" onfocus="return textCounter_GetFocus(this,250);" onblur="return textCounter_LostFocus(this);" />
<asp:CompareValidator  runat="server" ID="vcdvalinsctrl000021" SetFocusOnError="true" CssClass="error" ControltoValidate="insctrl000021" Display="Dynamic" Text="Ingrese un texto" ErrorMessage="Plantilla pie centro:Ingrese un texto" Type="String" Operator="DataTypeCheck" ValidationGroup="vgins" />
<asp:RegularExpressionValidator  runat="server" ID="vrgvalinsctrl000021" SetFocusOnError="true" CssClass="error" Display="Dynamic" ControltoValidate="insctrl000021" ValidationExpression="[\w\s-+/*^()!|°$%&=-_,:;.ñÑ~áéíóúÁÉÍÓÚ#}{¿]{0,250}" Text="No mayor a 250 caracteres. Deben ser letras o numeros." ErrorMessage="Plantilla pie centro:No mayor a 250 caracteres. Deben ser letras o numeros." ValidationGroup="vgins" />
<ajaxkit:ValidatorCalloutExtender  runat="server" ID="ajaxvrginsctrl000021" TargetControlID="vrgvalinsctrl000021" />

</td>
</tr>
<tr>
<td  colspan="1" >
Plantilla pie derecha
</td>
<td  colspan="1" >
<asp:TextBox  runat="server" ID="insctrl000020" CssClass="form-control" Width="400px" MaxLength="250" Tooltip="Campo219[]" TextMode="MultiLine" onkeypress="return textCounter(this,250);" onkeydown="return textCounter(this,250);" onkeyup="return textCounter(this,250);" onfocus="return textCounter_GetFocus(this,250);" onblur="return textCounter_LostFocus(this);" />
<asp:CompareValidator  runat="server" ID="vcdvalinsctrl000020" SetFocusOnError="true" CssClass="error" ControltoValidate="insctrl000020" Display="Dynamic" Text="Ingrese un texto" ErrorMessage="Plantilla pie derecha:Ingrese un texto" Type="String" Operator="DataTypeCheck" ValidationGroup="vgins" />
<asp:RegularExpressionValidator  runat="server" ID="vrgvalinsctrl000020" SetFocusOnError="true" CssClass="error" Display="Dynamic" ControltoValidate="insctrl000020" ValidationExpression="[\w\s-+/*^()!|°$%&=-_,:;.ñÑ~áéíóúÁÉÍÓÚ#}{¿]{0,250}" Text="No mayor a 250 caracteres. Deben ser letras o numeros." ErrorMessage="Plantilla pie derecha:No mayor a 250 caracteres. Deben ser letras o numeros." ValidationGroup="vgins" />
<ajaxkit:ValidatorCalloutExtender  runat="server" ID="ajaxvrginsctrl000020" TargetControlID="vrgvalinsctrl000020" />

</td>
</tr>
</table>
</ContentTemplate>
</ajaxkit:TabPanel>
<ajaxkit:TabPanel ID="instabPanel002" runat="server" HeaderText="Vigente" Tooltip="Vigente" >
<ContentTemplate>
<table>
<tr>
<td  colspan="1" >
Permite cambiar datos en estado vigente
</td>
<td  colspan="1" >
<asp:Checkbox  runat="server" ID="insctrl000019" CssClass="form-control" />
</td>
</tr>
<tr>
<td  colspan="1" >
Permite cambiar título en estado vigente
</td>
<td  colspan="1" >
<asp:Checkbox  runat="server" ID="insctrl000018" CssClass="form-control" />
</td>
</tr>
</table>
</ContentTemplate>
</ajaxkit:TabPanel>
<ajaxkit:TabPanel ID="instabPanel003" runat="server" HeaderText="Edición" Tooltip="Edición" >
<ContentTemplate>
<table>
<tr>
<td  colspan="1" >
Permite cambiar el título en edición
</td>
<td  colspan="1" >
<asp:Checkbox  runat="server" ID="insctrl000017" CssClass="form-control" />
</td>
</tr>
<tr>
<td  colspan="1" >
Permite cambiar datos de clasificación en edición
</td>
<td  colspan="1" >
<asp:Checkbox  runat="server" ID="insctrl000016" CssClass="form-control" />
</td>
</tr>
<tr>
<td  colspan="1" >
Permite cambiar roles en edición
</td>
<td  colspan="1" >
<asp:Checkbox  runat="server" ID="insctrl000015" CssClass="form-control" />
</td>
</tr>
</table>
</ContentTemplate>
</ajaxkit:TabPanel>
<ajaxkit:TabPanel ID="instabPanel004" runat="server" HeaderText="Opciones avanzadas" Tooltip="Opciones avanzadas" >
<ContentTemplate>
<table>
<tr>
<td  colspan="1" >
Siempre permite cambiar roles
</td>
<td  colspan="1" >
<asp:Checkbox  runat="server" ID="insctrl000014" CssClass="form-control" />
</td>
</tr>
<tr>
<td  colspan="1" >
Rol editor opcional
</td>
<td  colspan="1" >
<asp:Checkbox  runat="server" ID="insctrl000013" CssClass="form-control" />
</td>
</tr>
<tr>
<td  colspan="1" >
Rol revisor opcional
</td>
<td  colspan="1" >
<asp:Checkbox  runat="server" ID="insctrl000012" CssClass="form-control" />
</td>
</tr>
<tr>
<td  colspan="1" >
Rol aprobador opcional
</td>
<td  colspan="1" >
<asp:Checkbox  runat="server" ID="insctrl000011" CssClass="form-control" />
</td>
</tr>
<tr>
<td  colspan="1" >
Rol cancelador opcional
</td>
<td  colspan="1" >
<asp:Checkbox  runat="server" ID="insctrl000010" CssClass="form-control" />
</td>
</tr>
<tr>
<td  colspan="1" >
Rol publicador opcional
</td>
<td  colspan="1" >
<asp:Checkbox  runat="server" ID="insctrl000009" CssClass="form-control" />
</td>
</tr>
<tr>
<td  colspan="1" >
Rol lector opcional
</td>
<td  colspan="1" >
<asp:Checkbox  runat="server" ID="insctrl000008" CssClass="form-control" />
</td>
</tr>
<tr>
<td  colspan="1" >
Permite reedición
</td>
<td  colspan="1" >
<asp:Checkbox  runat="server" ID="insctrl000007" CssClass="form-control" />
</td>
</tr>
<tr>
<td  colspan="1" >
Auto edición habilitada
</td>
<td  colspan="1" >
<asp:RadioButtonList  runat="server" ID="insctrl000006" CssClass="form-control" RepeatLayout="Flow" RepeatDirection="Horizontal" >
<asp:ListItem  Text="Sí" Value="True" />
<asp:ListItem  Text="No" Value="False" />
<asp:ListItem  Text="Sin definir" Value="" Selected="True" />
</asp:RadioButtonList>

</td>
</tr>
<tr>
<td  colspan="1" >
Días de autoedición
</td>
<td  colspan="1" >
<asp:TextBox  runat="server" ID="insctrl000005" CssClass="form-control" Width="120px" MaxLength="10" Tooltip="Campo259[]" />
<asp:CompareValidator  runat="server" ID="vcdvalinsctrl000005" SetFocusOnError="true" CssClass="error" ControltoValidate="insctrl000005" Display="Dynamic" Text='Ingrese un entero' ErrorMessage='Días de autoedición:Ingrese un entero' Type="Integer" Operator="DataTypeCheck" ValidationGroup="vgins" />
<asp:RangeValidator  runat="server" ID="vrnvalinsctrl000005" SetFocusOnError="true" CssClass="error" Display="Dynamic" ControltoValidate="insctrl000005" Text="Ingrese un valor entre 1 y 2000000000" ErrorMessage="Días de autoedición:Ingrese un valor entre 1 y 2000000000" Type="Integer" CultureInvariantValues="True" MaximumValue="2000000000" MinimumValue="1" ValidationGroup="vgins" />
<ajaxkit:ValidatorCalloutExtender  runat="server" ID="ajaxvrninsctrl000005" TargetControlID="vrnvalinsctrl000005" />

</td>
</tr>
</ContentTemplate>
</ajaxkit:TabPanel>
</ajaxkit:TabContainer>
</td>
</tr>
</table>
<!-- End Plantilla insercion --></InsertItemTemplate>

</asp:FormView>

			<asp:SqlDataSource  runat="server" ID="dsdatos" ProviderName="System.Data.SqlClient" CancelSelectOnNullParameter="False" onInit="dsdatos_Init" SelectCommandType="Text"
 SelectCommand="SELECT DOC_DOCTIP.cod,DOC_DOCTIP.dsc,DOC_DOCTIP.orden,DOC_DOCTIP.abrev,DOC_DOCTIP.noespecificos,DOC_DOCTIP.formato,DOC_DOCTIP.formatoespecifico,DOC_DOCTIP.templatehead,DOC_DOCTIP.templatebody,DOC_DOCTIP.templatefootcustom,DOC_DOCTIP.templatefootleft,DOC_DOCTIP.templatefootcenter,DOC_DOCTIP.templatefootright,DOC_DOCTIP.permedicioncambiadsc,DOC_DOCTIP.permedicioncambiaotros,DOC_DOCTIP.permedicioncambiaroles,DOC_DOCTIP.permcambia,DOC_DOCTIP.opceditor,DOC_DOCTIP.opcrevisor,DOC_DOCTIP.opcaprobador,DOC_DOCTIP.opccancelador,DOC_DOCTIP.opcpublicador,DOC_DOCTIP.opclector,DOC_DOCTIP.reedicion,DOC_DOCTIP.baja,DOC_DOCTIP.permvigcambiaotros,DOC_DOCTIP.permvigcambiadsc,DOC_DOCTIP.autoeditionenabled,DOC_DOCTIP.autoeditiondayscicle,(DOC_DOCTIPLOGO.dsc) as logodsc,(DOC_DOCTIPQSECSID.secdsc) as qsecsiddsc,DOC_DOCTIP.qsecdatetime,DOC_DOCTIP.logo,DOC_DOCTIP.qsecsid FROM DOC_DOCTIP  LEFT JOIN CONBINARIES AS DOC_DOCTIPLOGO ON DOC_DOCTIPLOGO.cod=DOC_DOCTIP.logo LEFT JOIN Q_SECPLOGIN AS DOC_DOCTIPQSECSID ON DOC_DOCTIPQSECSID.sidcod=DOC_DOCTIP.qsecsid  WHERE (DOC_DOCTIP.cod = @param1) AND (DOC_DOCTIP.baja = 0 OR DOC_DOCTIP.baja  IS NULL)" onselected="dsdatos_Selected" UpdateCommandType="Text"
 UpdateCommand="UPDATE DOC_DOCTIP SET dsc=@dsc,abrev=@abrev,templatehead=@templatehead,templatebody=@templatebody,formato=@formato,formatoespecifico=@formatoespecifico,noespecificos=@noespecificos,opceditor=@opceditor,opcrevisor=@opcrevisor,opcaprobador=@opcaprobador,opccancelador=@opccancelador,opcpublicador=@opcpublicador,opclector=@opclector,permedicioncambiadsc=@permedicioncambiadsc,permedicioncambiaotros=@permedicioncambiaotros,templatefootleft=@templatefootleft,templatefootcenter=@templatefootcenter,templatefootright=@templatefootright,permedicioncambiaroles=@permedicioncambiaroles,templatefootcustom=@templatefootcustom,reedicion=@reedicion,orden=@orden,permcambia=@permcambia,permvigcambiaotros=@permvigcambiaotros,permvigcambiadsc=@permvigcambiadsc,autoeditiondayscicle=@autoeditiondayscicle,autoeditionenabled=@autoeditionenabled,qsecsid=@qsecsid,qsecdatetime=getdate() WHERE DOC_DOCTIP.cod = @param1" onupdated="dsdatos_Updated" InsertCommandType="Text"
 InsertCommand="DECLARE @querynextcodcod int  SET @querynextcodcod =(SELECT ISNULL(MAX(cod),0)+1 FROM DOC_DOCTIP WHERE cod > 0 ) INSERT INTO DOC_DOCTIP (cod,dsc,orden,abrev,noespecificos,formato,formatoespecifico,templatehead,templatebody,templatefootcustom,templatefootleft,templatefootcenter,templatefootright,permedicioncambiadsc,permedicioncambiaotros,permedicioncambiaroles,permcambia,opceditor,opcrevisor,opcaprobador,opccancelador,opcpublicador,opclector,reedicion,baja,permvigcambiaotros,permvigcambiadsc,autoeditionenabled,autoeditiondayscicle,qsecsid,qsecdatetime) VALUES(@querynextcodcod,@dsc,@orden,@abrev,@noespecificos,@formato,@formatoespecifico,@templatehead,@templatebody,@templatefootcustom,@templatefootleft,@templatefootcenter,@templatefootright,@permedicioncambiadsc,@permedicioncambiaotros,@permedicioncambiaroles,@permcambia,@opceditor,@opcrevisor,@opcaprobador,@opccancelador,@opcpublicador,@opclector,@reedicion,NULL,@permvigcambiaotros,@permvigcambiadsc,@autoeditionenabled,@autoeditiondayscicle,@qsecsid,getdate()) SELECT @querynextcod =@querynextcodcod" onInserted="dsdatos_Inserted" DeleteCommandType="Text"
 DeleteCommand="UPDATE DOC_DOCTIP SET baja=1 WHERE cod = @param1" ondeleted="dsdatos_Deleted" >
<InsertParameters>
<asp:Parameter  Name="querynextcod" Direction="Output" Type="Int32" />
<asp:ControlParameter  Name="dsc" ControlID="frmdatos$instabPanel$instabPanel001$insctrl000031" ConvertEmptyStringToNull="True" Direction="InputOutput" Type="String" Size="50" PropertyName="Text" />
<asp:ControlParameter  Name="orden" ControlID="frmdatos$instabPanel$instabPanel001$insctrl000030" ConvertEmptyStringToNull="True" Direction="InputOutput" Type="Int32" PropertyName="Text" />
<asp:ControlParameter  Name="abrev" ControlID="frmdatos$instabPanel$instabPanel001$insctrl000029" ConvertEmptyStringToNull="True" Direction="InputOutput" Type="String" Size="50" PropertyName="Text" />
<asp:ControlParameter  Name="noespecificos" ControlID="frmdatos$instabPanel$instabPanel001$insctrl000028" ConvertEmptyStringToNull="True" Direction="InputOutput" Type="Boolean" PropertyName="SelectedValue" />
<asp:ControlParameter  Name="formato" ControlID="frmdatos$instabPanel$instabPanel001$insctrl000027" ConvertEmptyStringToNull="True" Direction="InputOutput" Type="String" Size="50" PropertyName="Text" />
<asp:ControlParameter  Name="formatoespecifico" ControlID="frmdatos$instabPanel$instabPanel001$insctrl000026" ConvertEmptyStringToNull="True" Direction="InputOutput" Type="String" Size="50" PropertyName="Text" />
<asp:Parameter  Name="templatehead" Direction="InputOutput" Type="String" Size="8001" />
<asp:Parameter  Name="templatebody" Direction="InputOutput" Type="String" Size="8001" />
<asp:ControlParameter  Name="templatefootcustom" ControlID="frmdatos$instabPanel$instabPanel001$insctrl000023" ConvertEmptyStringToNull="True" Direction="InputOutput" Type="Boolean" PropertyName="Checked" />
<asp:ControlParameter  Name="templatefootleft" ControlID="frmdatos$instabPanel$instabPanel001$insctrl000022" ConvertEmptyStringToNull="True" Direction="InputOutput" Type="String" Size="250" PropertyName="Text" />
<asp:ControlParameter  Name="templatefootcenter" ControlID="frmdatos$instabPanel$instabPanel001$insctrl000021" ConvertEmptyStringToNull="True" Direction="InputOutput" Type="String" Size="250" PropertyName="Text" />
<asp:ControlParameter  Name="templatefootright" ControlID="frmdatos$instabPanel$instabPanel001$insctrl000020" ConvertEmptyStringToNull="True" Direction="InputOutput" Type="String" Size="250" PropertyName="Text" />
<asp:ControlParameter  Name="permvigcambiaotros" ControlID="frmdatos$instabPanel$instabPanel002$insctrl000019" ConvertEmptyStringToNull="True" Direction="InputOutput" Type="Boolean" PropertyName="Checked" />
<asp:ControlParameter  Name="permvigcambiadsc" ControlID="frmdatos$instabPanel$instabPanel002$insctrl000018" ConvertEmptyStringToNull="True" Direction="InputOutput" Type="Boolean" PropertyName="Checked" />
<asp:ControlParameter  Name="permedicioncambiadsc" ControlID="frmdatos$instabPanel$instabPanel003$insctrl000017" ConvertEmptyStringToNull="True" Direction="InputOutput" Type="Boolean" PropertyName="Checked" />
<asp:ControlParameter  Name="permedicioncambiaotros" ControlID="frmdatos$instabPanel$instabPanel003$insctrl000016" ConvertEmptyStringToNull="True" Direction="InputOutput" Type="Boolean" PropertyName="Checked" />
<asp:ControlParameter  Name="permedicioncambiaroles" ControlID="frmdatos$instabPanel$instabPanel003$insctrl000015" ConvertEmptyStringToNull="True" Direction="InputOutput" Type="Boolean" PropertyName="Checked" />
<asp:ControlParameter  Name="permcambia" ControlID="frmdatos$instabPanel$instabPanel004$insctrl000014" ConvertEmptyStringToNull="True" Direction="InputOutput" Type="Boolean" PropertyName="Checked" />
<asp:ControlParameter  Name="opceditor" ControlID="frmdatos$instabPanel$instabPanel004$insctrl000013" ConvertEmptyStringToNull="True" Direction="InputOutput" Type="Boolean" PropertyName="Checked" />
<asp:ControlParameter  Name="opcrevisor" ControlID="frmdatos$instabPanel$instabPanel004$insctrl000012" ConvertEmptyStringToNull="True" Direction="InputOutput" Type="Boolean" PropertyName="Checked" />
<asp:ControlParameter  Name="opcaprobador" ControlID="frmdatos$instabPanel$instabPanel004$insctrl000011" ConvertEmptyStringToNull="True" Direction="InputOutput" Type="Boolean" PropertyName="Checked" />
<asp:ControlParameter  Name="opccancelador" ControlID="frmdatos$instabPanel$instabPanel004$insctrl000010" ConvertEmptyStringToNull="True" Direction="InputOutput" Type="Boolean" PropertyName="Checked" />
<asp:ControlParameter  Name="opcpublicador" ControlID="frmdatos$instabPanel$instabPanel004$insctrl000009" ConvertEmptyStringToNull="True" Direction="InputOutput" Type="Boolean" PropertyName="Checked" />
<asp:ControlParameter  Name="opclector" ControlID="frmdatos$instabPanel$instabPanel004$insctrl000008" ConvertEmptyStringToNull="True" Direction="InputOutput" Type="Boolean" PropertyName="Checked" />
<asp:ControlParameter  Name="reedicion" ControlID="frmdatos$instabPanel$instabPanel004$insctrl000007" ConvertEmptyStringToNull="True" Direction="InputOutput" Type="Boolean" PropertyName="Checked" />
<asp:ControlParameter  Name="autoeditionenabled" ControlID="frmdatos$instabPanel$instabPanel004$insctrl000006" ConvertEmptyStringToNull="True" Direction="InputOutput" Type="Boolean" PropertyName="SelectedValue" />
<asp:ControlParameter  Name="autoeditiondayscicle" ControlID="frmdatos$instabPanel$instabPanel004$insctrl000005" ConvertEmptyStringToNull="True" Direction="InputOutput" Type="Int32" PropertyName="Text" />
<asp:SessionParameter  Name="qsecsid" SessionField="secsid" Direction="Input" />
<asp:Parameter  Name="param1" Direction="ReturnValue" />
</InsertParameters>
<SelectParameters>
<asp:QueryStringParameter  Name="param1" QueryStringField="param1" ConvertEmptyStringToNull="True" Type="String" Size="200" />
</SelectParameters>
<UpdateParameters>
<asp:QueryStringParameter  Name="param1" QueryStringField="param1" ConvertEmptyStringToNull="True" Type="String" Size="200" />
<asp:ControlParameter  Name="dsc" ControlID="frmdatos$edittabPanel$edittabPanel001$editctrl000031" ConvertEmptyStringToNull="True" Direction="InputOutput" Type="String" Size="50" PropertyName="Text" />
<asp:ControlParameter  Name="orden" ControlID="frmdatos$edittabPanel$edittabPanel001$editctrl000030" ConvertEmptyStringToNull="True" Direction="InputOutput" Type="Int32" PropertyName="Text" />
<asp:ControlParameter  Name="abrev" ControlID="frmdatos$edittabPanel$edittabPanel001$editctrl000029" ConvertEmptyStringToNull="True" Direction="InputOutput" Type="String" Size="50" PropertyName="Text" />
<asp:ControlParameter  Name="noespecificos" ControlID="frmdatos$edittabPanel$edittabPanel001$editctrl000028" ConvertEmptyStringToNull="True" Direction="InputOutput" Type="Boolean" PropertyName="SelectedValue" />
<asp:ControlParameter  Name="formato" ControlID="frmdatos$edittabPanel$edittabPanel001$editctrl000027" ConvertEmptyStringToNull="True" Direction="InputOutput" Type="String" Size="50" PropertyName="Text" />
<asp:ControlParameter  Name="formatoespecifico" ControlID="frmdatos$edittabPanel$edittabPanel001$editctrl000026" ConvertEmptyStringToNull="True" Direction="InputOutput" Type="String" Size="50" PropertyName="Text" />
<asp:Parameter  Name="templatehead" Direction="InputOutput" Type="String" Size="8001" />
<asp:Parameter  Name="templatebody" Direction="InputOutput" Type="String" Size="8001" />
<asp:ControlParameter  Name="templatefootcustom" ControlID="frmdatos$edittabPanel$edittabPanel001$editctrl000023" ConvertEmptyStringToNull="True" Direction="InputOutput" Type="Boolean" PropertyName="Checked" />
<asp:ControlParameter  Name="templatefootleft" ControlID="frmdatos$edittabPanel$edittabPanel001$editctrl000022" ConvertEmptyStringToNull="True" Direction="InputOutput" Type="String" Size="250" PropertyName="Text" />
<asp:ControlParameter  Name="templatefootcenter" ControlID="frmdatos$edittabPanel$edittabPanel001$editctrl000021" ConvertEmptyStringToNull="True" Direction="InputOutput" Type="String" Size="250" PropertyName="Text" />
<asp:ControlParameter  Name="templatefootright" ControlID="frmdatos$edittabPanel$edittabPanel001$editctrl000020" ConvertEmptyStringToNull="True" Direction="InputOutput" Type="String" Size="250" PropertyName="Text" />
<asp:ControlParameter  Name="permvigcambiaotros" ControlID="frmdatos$edittabPanel$edittabPanel002$editctrl000019" ConvertEmptyStringToNull="True" Direction="InputOutput" Type="Boolean" PropertyName="Checked" />
<asp:ControlParameter  Name="permvigcambiadsc" ControlID="frmdatos$edittabPanel$edittabPanel002$editctrl000018" ConvertEmptyStringToNull="True" Direction="InputOutput" Type="Boolean" PropertyName="Checked" />
<asp:ControlParameter  Name="permedicioncambiadsc" ControlID="frmdatos$edittabPanel$edittabPanel003$editctrl000017" ConvertEmptyStringToNull="True" Direction="InputOutput" Type="Boolean" PropertyName="Checked" />
<asp:ControlParameter  Name="permedicioncambiaotros" ControlID="frmdatos$edittabPanel$edittabPanel003$editctrl000016" ConvertEmptyStringToNull="True" Direction="InputOutput" Type="Boolean" PropertyName="Checked" />
<asp:ControlParameter  Name="permedicioncambiaroles" ControlID="frmdatos$edittabPanel$edittabPanel003$editctrl000015" ConvertEmptyStringToNull="True" Direction="InputOutput" Type="Boolean" PropertyName="Checked" />
<asp:ControlParameter  Name="permcambia" ControlID="frmdatos$edittabPanel$edittabPanel004$editctrl000014" ConvertEmptyStringToNull="True" Direction="InputOutput" Type="Boolean" PropertyName="Checked" />
<asp:ControlParameter  Name="opceditor" ControlID="frmdatos$edittabPanel$edittabPanel004$editctrl000013" ConvertEmptyStringToNull="True" Direction="InputOutput" Type="Boolean" PropertyName="Checked" />
<asp:ControlParameter  Name="opcrevisor" ControlID="frmdatos$edittabPanel$edittabPanel004$editctrl000012" ConvertEmptyStringToNull="True" Direction="InputOutput" Type="Boolean" PropertyName="Checked" />
<asp:ControlParameter  Name="opcaprobador" ControlID="frmdatos$edittabPanel$edittabPanel004$editctrl000011" ConvertEmptyStringToNull="True" Direction="InputOutput" Type="Boolean" PropertyName="Checked" />
<asp:ControlParameter  Name="opccancelador" ControlID="frmdatos$edittabPanel$edittabPanel004$editctrl000010" ConvertEmptyStringToNull="True" Direction="InputOutput" Type="Boolean" PropertyName="Checked" />
<asp:ControlParameter  Name="opcpublicador" ControlID="frmdatos$edittabPanel$edittabPanel004$editctrl000009" ConvertEmptyStringToNull="True" Direction="InputOutput" Type="Boolean" PropertyName="Checked" />
<asp:ControlParameter  Name="opclector" ControlID="frmdatos$edittabPanel$edittabPanel004$editctrl000008" ConvertEmptyStringToNull="True" Direction="InputOutput" Type="Boolean" PropertyName="Checked" />
<asp:ControlParameter  Name="reedicion" ControlID="frmdatos$edittabPanel$edittabPanel004$editctrl000007" ConvertEmptyStringToNull="True" Direction="InputOutput" Type="Boolean" PropertyName="Checked" />
<asp:ControlParameter  Name="autoeditionenabled" ControlID="frmdatos$edittabPanel$edittabPanel004$editctrl000006" ConvertEmptyStringToNull="True" Direction="InputOutput" Type="Boolean" PropertyName="SelectedValue" />
<asp:ControlParameter  Name="autoeditiondayscicle" ControlID="frmdatos$edittabPanel$edittabPanel004$editctrl000005" ConvertEmptyStringToNull="True" Direction="InputOutput" Type="Int32" PropertyName="Text" />
<asp:SessionParameter  Name="qsecsid" SessionField="secsid" Direction="Input" />
</UpdateParameters>
<DeleteParameters>
<asp:QueryStringParameter  Name="param1" QueryStringField="param1" ConvertEmptyStringToNull="True" Type="String" Size="200" />
<asp:SessionParameter  Name="qsecsid" SessionField="secsid" Direction="Input" />
</DeleteParameters>
</asp:SqlDataSource>
</table>
<!-- End -->
</td></tr></table>
</asp:Content>

