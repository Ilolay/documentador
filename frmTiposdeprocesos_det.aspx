<%@ Page Language="VB" CodeFile="frmTiposdeprocesos_det.aspx.vb" MasterPageFile="general.master" AutoEventWireup="True" Inherits="frmTiposdeprocesos_det" Title="Tipos de procesos" %>
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
<td  style="text-align:left;" colspan="1" ><span class="form-title">Tipos de procesos</span> |<asp:Label  runat="server" ID="lblsubtitle" CssClass="form-subtitle" />
</td></tr>
</table>
</td></tr>
<tr><td  colspan="1" >				<!-- Init -->
<table id="body" style="background-color:#F0F0F0;" width="100%">
					<br /><asp:Label  runat="server" ID="lblerror" CssClass="error" width="100%" />			<asp:ValidationSummary  runat="server" ID="frmdatosvalsumary" />
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
Codigo
</td>
<td  colspan="1" >
<asp:Label  runat="server" ID="itemctrl000008" CssClass="form-control-read" width="100%" OnDataBinding="itemctrl000008_Databound" />
</td>
</tr>
<tr>
<td  colspan="1" >
Descripcion
</td>
<td  colspan="1" >
<asp:Label  runat="server" ID="itemctrl000006" CssClass="form-control-read" width="100%" OnDataBinding="itemctrl000006_Databound" />
</td>
</tr>
<tr>
<td  colspan="1" >
Abreviatura
</td>
<td  colspan="1" >
<asp:Label  runat="server" ID="itemctrl000005" CssClass="form-control-read" width="100%" OnDataBinding="itemctrl000005_Databound" />
</td>
</tr>
<tr>
<td  colspan="1" >
Orden
</td>
<td  colspan="1" >
<asp:Label  runat="server" ID="itemctrl000004" CssClass="form-control-read" width="100%" OnDataBinding="itemctrl000004_Databound" />
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
<asp:TextBox  runat="server" ID="editctrl000006" CssClass="form-control" Width="400px" MaxLength="50" OnDataBinding="editctrl000006_DataBound_frmdatos" Tooltip="Campo6[]" TextMode="MultiLine" onkeypress="return textCounter(this,50);" onkeydown="return textCounter(this,50);" onkeyup="return textCounter(this,50);" onfocus="return textCounter_GetFocus(this,50);" onblur="return textCounter_LostFocus(this);" /> <span class='error'><b>*</b></span>
<asp:RequiredFieldValidator  runat="server" ID="vrqeditctrl000006" SetFocusOnError="true" CssClass="error" Display="Dynamic" ControltoValidate="editctrl000006" Text="Obligatorio!!! ErrorMessage=´Descripcion:es un dato obligatorio!´" ValidationGroup="vgedit" />
<ajaxkit:ValidatorCalloutExtender  runat="server" ID="ajaxvrqeditctrl000006" TargetControlID="vrqeditctrl000006" /><asp:CompareValidator  runat="server" ID="vcdvaleditctrl000006" SetFocusOnError="true" CssClass="error" ControltoValidate="editctrl000006" Display="Dynamic" Text="Ingrese un texto" ErrorMessage="Descripcion:Ingrese un texto" Type="String" Operator="DataTypeCheck" ValidationGroup="vgedit" />
<asp:RegularExpressionValidator  runat="server" ID="vrgvaleditctrl000006" SetFocusOnError="true" CssClass="error" Display="Dynamic" ControltoValidate="editctrl000006" ValidationExpression="[\w\s-+/*^()!|°$%&=-_,:;.ñÑ~áéíóúÁÉÍÓÚ#}{¿]{0,50}" Text="No mayor a 50 caracteres. Deben ser letras o numeros." ErrorMessage="Descripcion:No mayor a 50 caracteres. Deben ser letras o numeros." ValidationGroup="vgedit" />
<ajaxkit:ValidatorCalloutExtender  runat="server" ID="ajaxvrgeditctrl000006" TargetControlID="vrgvaleditctrl000006" />

</td>
</tr>
<tr>
<td  colspan="1" >
<b>Abreviatura</b>
</td>
<td  colspan="1" >
<asp:TextBox  runat="server" ID="editctrl000005" CssClass="form-control" Width="64px" MaxLength="3" OnDataBinding="editctrl000005_DataBound_frmdatos" Tooltip="Campo42[]" /> <span class='error'><b>*</b></span>
<asp:RequiredFieldValidator  runat="server" ID="vrqeditctrl000005" SetFocusOnError="true" CssClass="error" Display="Dynamic" ControltoValidate="editctrl000005" Text="Obligatorio!!! ErrorMessage=´Abreviatura:es un dato obligatorio!´" ValidationGroup="vgedit" />
<ajaxkit:ValidatorCalloutExtender  runat="server" ID="ajaxvrqeditctrl000005" TargetControlID="vrqeditctrl000005" /><asp:CompareValidator  runat="server" ID="vcdvaleditctrl000005" SetFocusOnError="true" CssClass="error" ControltoValidate="editctrl000005" Display="Dynamic" Text="Ingrese un texto" ErrorMessage="Abreviatura:Ingrese un texto" Type="String" Operator="DataTypeCheck" ValidationGroup="vgedit" />
<asp:RegularExpressionValidator  runat="server" ID="vrgvaleditctrl000005" SetFocusOnError="true" CssClass="error" Display="Dynamic" ControltoValidate="editctrl000005" ValidationExpression="[\w\s-+/*^()!|°$%&=-_,:;.ñÑ~áéíóúÁÉÍÓÚ#}{¿]{0,3}" Text="No mayor a 3 caracteres. Deben ser letras o numeros." ErrorMessage="Abreviatura:No mayor a 3 caracteres. Deben ser letras o numeros." ValidationGroup="vgedit" />
<ajaxkit:ValidatorCalloutExtender  runat="server" ID="ajaxvrgeditctrl000005" TargetControlID="vrgvaleditctrl000005" />

</td>
</tr>
<tr>
<td  colspan="1" >
Orden
</td>
<td  colspan="1" >
<asp:Label  runat="server" ID="editctrl000004" CssClass="form-control-read" width="100%" OnDataBinding="editctrl000004_Databound" />
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
<asp:TextBox  runat="server" ID="insctrl000006" CssClass="form-control" Width="400px" MaxLength="50" Tooltip="Campo6[]" TextMode="MultiLine" onkeypress="return textCounter(this,50);" onkeydown="return textCounter(this,50);" onkeyup="return textCounter(this,50);" onfocus="return textCounter_GetFocus(this,50);" onblur="return textCounter_LostFocus(this);" /> <span class='error'><b>*</b></span>
<asp:RequiredFieldValidator  runat="server" ID="vrqinsctrl000006" SetFocusOnError="true" CssClass="error" Display="Dynamic" ControltoValidate="insctrl000006" Text="Obligatorio!!! ErrorMessage=´Descripcion:es un dato obligatorio!´" ValidationGroup="vgins" />
<ajaxkit:ValidatorCalloutExtender  runat="server" ID="ajaxvrqinsctrl000006" TargetControlID="vrqinsctrl000006" /><asp:CompareValidator  runat="server" ID="vcdvalinsctrl000006" SetFocusOnError="true" CssClass="error" ControltoValidate="insctrl000006" Display="Dynamic" Text="Ingrese un texto" ErrorMessage="Descripcion:Ingrese un texto" Type="String" Operator="DataTypeCheck" ValidationGroup="vgins" />
<asp:RegularExpressionValidator  runat="server" ID="vrgvalinsctrl000006" SetFocusOnError="true" CssClass="error" Display="Dynamic" ControltoValidate="insctrl000006" ValidationExpression="[\w\s-+/*^()!|°$%&=-_,:;.ñÑ~áéíóúÁÉÍÓÚ#}{¿]{0,50}" Text="No mayor a 50 caracteres. Deben ser letras o numeros." ErrorMessage="Descripcion:No mayor a 50 caracteres. Deben ser letras o numeros." ValidationGroup="vgins" />
<ajaxkit:ValidatorCalloutExtender  runat="server" ID="ajaxvrginsctrl000006" TargetControlID="vrgvalinsctrl000006" />

</td>
</tr>
<tr>
<td  colspan="1" >
<b>Abreviatura</b>
</td>
<td  colspan="1" >
<asp:TextBox  runat="server" ID="insctrl000005" CssClass="form-control" Width="64px" MaxLength="3" Tooltip="Campo42[]" /> <span class='error'><b>*</b></span>
<asp:RequiredFieldValidator  runat="server" ID="vrqinsctrl000005" SetFocusOnError="true" CssClass="error" Display="Dynamic" ControltoValidate="insctrl000005" Text="Obligatorio!!! ErrorMessage=´Abreviatura:es un dato obligatorio!´" ValidationGroup="vgins" />
<ajaxkit:ValidatorCalloutExtender  runat="server" ID="ajaxvrqinsctrl000005" TargetControlID="vrqinsctrl000005" /><asp:CompareValidator  runat="server" ID="vcdvalinsctrl000005" SetFocusOnError="true" CssClass="error" ControltoValidate="insctrl000005" Display="Dynamic" Text="Ingrese un texto" ErrorMessage="Abreviatura:Ingrese un texto" Type="String" Operator="DataTypeCheck" ValidationGroup="vgins" />
<asp:RegularExpressionValidator  runat="server" ID="vrgvalinsctrl000005" SetFocusOnError="true" CssClass="error" Display="Dynamic" ControltoValidate="insctrl000005" ValidationExpression="[\w\s-+/*^()!|°$%&=-_,:;.ñÑ~áéíóúÁÉÍÓÚ#}{¿]{0,3}" Text="No mayor a 3 caracteres. Deben ser letras o numeros." ErrorMessage="Abreviatura:No mayor a 3 caracteres. Deben ser letras o numeros." ValidationGroup="vgins" />
<ajaxkit:ValidatorCalloutExtender  runat="server" ID="ajaxvrginsctrl000005" TargetControlID="vrgvalinsctrl000005" />

</td>
</tr>
</table>
<!-- End Plantilla insercion --></InsertItemTemplate>

</asp:FormView>

			<asp:SqlDataSource  runat="server" ID="dsdatos" ProviderName="System.Data.SqlClient" CancelSelectOnNullParameter="False" onInit="dsdatos_Init" SelectCommandType="Text"
 SelectCommand="SELECT DOC_APA.cod,DOC_APA.dsc,DOC_APA.abrev,DOC_APA.baja,DOC_APA.orden,(DOC_APAQSECSID.secdsc) as qsecsiddsc,DOC_APA.qsecdatetime,DOC_APA.qsecsid FROM DOC_APA  LEFT JOIN Q_SECPLOGIN AS DOC_APAQSECSID ON DOC_APAQSECSID.sidcod=DOC_APA.qsecsid  WHERE (DOC_APA.cod = @param1) AND (DOC_APA.baja = '1900-01-01T00:00:00' OR DOC_APA.baja  IS NULL)" onselected="dsdatos_Selected" UpdateCommandType="Text"
 UpdateCommand="UPDATE DOC_APA SET dsc=@dsc,abrev=@abrev,qsecsid=@qsecsid,qsecdatetime=getdate() WHERE DOC_APA.cod = @param1" onupdated="dsdatos_Updated" InsertCommandType="Text"
 InsertCommand="DECLARE @querynextcodcod int  SET @querynextcodcod =(SELECT ISNULL(MAX(cod),0)+1 FROM DOC_APA WHERE cod > 0 ) INSERT INTO DOC_APA (cod,dsc,abrev,qsecsid,qsecdatetime) VALUES(@querynextcodcod,@dsc,@abrev,@qsecsid,getdate()) SELECT @querynextcod =@querynextcodcod" onInserted="dsdatos_Inserted" DeleteCommandType="Text"
 DeleteCommand="UPDATE DOC_APA SET baja=getdate() WHERE cod = @param1" ondeleted="dsdatos_Deleted" >
<InsertParameters>
<asp:Parameter  Name="querynextcod" Direction="Output" Type="Int32" />
<asp:ControlParameter  Name="dsc" ControlID="frmdatos$insctrl000006" ConvertEmptyStringToNull="True" Direction="InputOutput" Type="String" Size="50" PropertyName="Text" />
<asp:ControlParameter  Name="abrev" ControlID="frmdatos$insctrl000005" ConvertEmptyStringToNull="True" Direction="InputOutput" Type="String" Size="3" PropertyName="Text" />
<asp:SessionParameter  Name="qsecsid" SessionField="secsid" Direction="Input" />
<asp:Parameter  Name="param1" Direction="ReturnValue" />
</InsertParameters>
<SelectParameters>
<asp:QueryStringParameter  Name="param1" QueryStringField="param1" ConvertEmptyStringToNull="True" Type="String" Size="200" />
</SelectParameters>
<UpdateParameters>
<asp:QueryStringParameter  Name="param1" QueryStringField="param1" ConvertEmptyStringToNull="True" Type="String" Size="200" />
<asp:ControlParameter  Name="dsc" ControlID="frmdatos$editctrl000006" ConvertEmptyStringToNull="True" Direction="InputOutput" Type="String" Size="50" PropertyName="Text" />
<asp:ControlParameter  Name="abrev" ControlID="frmdatos$editctrl000005" ConvertEmptyStringToNull="True" Direction="InputOutput" Type="String" Size="3" PropertyName="Text" />
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

