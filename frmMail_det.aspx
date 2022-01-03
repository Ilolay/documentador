<%@ Page Language="VB" CodeFile="frmMail_det.aspx.vb" MasterPageFile="general.master" AutoEventWireup="True" Inherits="frmMail_det" Title="Mail" %>
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
<tr><td  style="width:32px;" colspan="1" ><img  src="imagenes/icon00000028.png" width="32px" height="32px" alt="Mail" hspace="4"/></td>
<td  style="text-align:left;" colspan="1" ><span class="form-title">Mail</span> |<asp:Label  runat="server" ID="lblsubtitle" CssClass="form-subtitle" />
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
<asp:Button  runat="server" ID="cmdSecPermView" Text="Permisos" CssClass="boton-acciones" CausesValidation="False" OnDataBinding="cmdSecPermView_DataBinding" onClientClick='<%# "javascript:if (window.showModalDialog) {window.showModalDialog(" & chr(39) & "frmsecperm.aspx??_mode_=0&_closea_=1&param1=-1&timestamp=" & chr(39) & " + new Date().getTime() + " & chr(39) & "" & chr(39) & ", " & chr(39) & "" & chr(39) & ", " & chr(39) & "menubar=no;resizable=yes;toolbar=no;status=no;center=yes;dialogWidth=750px;dialogHeight=570px;" & chr(39) & ");} else {window.open(" & chr(39) & "frmsecperm.aspx??_mode_=0&_closea_=1&param1=-1&timestamp=" & chr(39) & " + new Date().getTime() + " & chr(39) & "" & chr(39) & ", " & chr(39) & "" & chr(39) & ", " & chr(39) & "modal=yes;directories=no;menubar=no;resizable=yes;toolbar=no;status=no;center=yes;dialogWidth=750px;dialogHeight=570px;" & chr(39) & ");};return false;" %>' />
<asp:Button  runat="server" ID="cmdFormViewItemUpdate" Text="Editar" CssClass="boton-acciones" CausesValidation="False" OnClick="cmdFormViewItemUpdate_Click" OnDataBinding="cmdFormViewItemUpdate_DataBinding" />
</div>
<table width="100%" cellpadding="1" cellspacing="1"><tr>
<td  colspan="1" >
Código
</td>
<td  colspan="1" >
<asp:Label  runat="server" ID="itemctrl000008" CssClass="form-control-read" width="100%" OnDataBinding="itemctrl000008_Databound" />
</td>
</tr>
<tr>
<td  colspan="1" >
Descripción
</td>
<td  colspan="1" >
<asp:Label  runat="server" ID="itemctrl000006" CssClass="form-control-read" width="100%" OnDataBinding="itemctrl000006_Databound" />
</td>
</tr>
<tr>
<td  colspan="2" >
Contenido
</td>
</tr>
<tr>
<td  colspan="2" >
<asp:Label  runat="server" ID="itemctrl000005" CssClass="form-control-read" width="100%" OnDataBinding="itemctrl000005_Databound" BorderStyle="Solid" BorderWidth="1" BorderColor="LightGray" />
</td>
</tr>
<tr>
<td  colspan="1" >
Asunto
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
<asp:LinkButton runat="server" ID="itemctrl000003view" Text='<%# Eval("qsecsiddsc") %>' CssClass="form-control-read" OnDataBinding="itemctrl000003view_DataBinding" CausesValidation="False" OnClientClick='<%# "javascript:if (window.showModalDialog) {window.showModalDialog(" & chr(39) & "frmUsuarios_det.aspx?_mode_=0&_closea_=1&param1=" & Eval("qsecsid") & "&timestamp=" & chr(39) & " + new Date().getTime() + " & chr(39) & "" & chr(39) & ", " & chr(39) & "" & chr(39) & ", " & chr(39) & "menubar=no;resizable=yes;toolbar=no;status=no;center=yes;dialogWidth=750px;dialogHeight=570px;" & chr(39) & ");} else {window.open(" & chr(39) & "frmUsuarios_det.aspx?_mode_=0&_closea_=1&param1=" & Eval("qsecsid") & "&timestamp=" & chr(39) & " + new Date().getTime() + " & chr(39) & "" & chr(39) & ", " & chr(39) & "" & chr(39) & ", " & chr(39) & "modal=yes;directories=no;menubar=no;resizable=yes;toolbar=no;status=no;center=yes;dialogWidth=750px;dialogHeight=570px;" & chr(39) & ");};return false;" %>' PostBackUrl='<%# "javascript:if (window.showModalDialog) {window.showModalDialog(" & chr(39) & "frmUsuarios_det.aspx?_mode_=0&_closea_=1&param1=" & Eval("qsecsid") & "&timestamp=" & chr(39) & " + new Date().getTime() + " & chr(39) & "" & chr(39) & ", " & chr(39) & "" & chr(39) & ", " & chr(39) & "menubar=no;resizable=yes;toolbar=no;status=no;center=yes;dialogWidth=750px;dialogHeight=570px;" & chr(39) & ");} else {window.open(" & chr(39) & "frmUsuarios_det.aspx?_mode_=0&_closea_=1&param1=" & Eval("qsecsid") & "&timestamp=" & chr(39) & " + new Date().getTime() + " & chr(39) & "" & chr(39) & ", " & chr(39) & "" & chr(39) & ", " & chr(39) & "modal=yes;directories=no;menubar=no;resizable=yes;toolbar=no;status=no;center=yes;dialogWidth=750px;dialogHeight=570px;" & chr(39) & ");};return false;" %>' />

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
<b>Descripción</b>
</td>
<td  colspan="1" >
<asp:TextBox  runat="server" ID="editctrl000006" CssClass="form-control" Width="400px" MaxLength="50" OnDataBinding="editctrl000006_DataBound_frmdatos" Tooltip="Campo146[]" TextMode="MultiLine" onkeypress="return textCounter(this,50);" onkeydown="return textCounter(this,50);" onkeyup="return textCounter(this,50);" onfocus="return textCounter_GetFocus(this,50);" onblur="return textCounter_LostFocus(this);" /> <span class='error'><b>*</b></span>
<ajaxkit:AutoCompleteExtender runat="server" ID="autoeditctrl000006" TargetControlID="editctrl000006" ServicePath="~/frmMail_detajax.asmx" ServiceMethod="editctrl000006_get" MinimumPrefixLength="3" CompletionInterval="300" EnableCaching="True" CompletionSetCount="20" DelimiterCharacters=";, :" />
<asp:RequiredFieldValidator  runat="server" ID="vrqeditctrl000006" SetFocusOnError="true" CssClass="error" Display="Dynamic" ControltoValidate="editctrl000006" Text="Obligatorio!!! ErrorMessage=´Descripción:es un dato obligatorio!´" ValidationGroup="vgedit" />
<ajaxkit:ValidatorCalloutExtender  runat="server" ID="ajaxvrqeditctrl000006" TargetControlID="vrqeditctrl000006" /><asp:CompareValidator  runat="server" ID="vcdvaleditctrl000006" SetFocusOnError="true" CssClass="error" ControltoValidate="editctrl000006" Display="Dynamic" Text="Ingrese un texto" ErrorMessage="Descripción:Ingrese un texto" Type="String" Operator="DataTypeCheck" ValidationGroup="vgedit" />
<asp:RegularExpressionValidator  runat="server" ID="vrgvaleditctrl000006" SetFocusOnError="true" CssClass="error" Display="Dynamic" ControltoValidate="editctrl000006" ValidationExpression="[\w\s-+/*^()!|°$%&=-_,:;.ñÑ~áéíóúÁÉÍÓÚ#}{]{0,50}" Text="No mayor a 50 caracteres. Deben ser letras o numeros." ErrorMessage="Descripción:No mayor a 50 caracteres. Deben ser letras o numeros." ValidationGroup="vgedit" />
<ajaxkit:ValidatorCalloutExtender  runat="server" ID="ajaxvrgeditctrl000006" TargetControlID="vrgvaleditctrl000006" />

</td>
</tr>
<tr>
<td  colspan="2" >
Contenido
</td>
</tr>
<tr>
<td  colspan="2" >
<asp:HiddenField  runat="server" ID="editctrl000005" OnDataBinding="editctrl000005_DataBound"/><iframe  runat="server" id="fmeeditctrl000005" frameborder="0" width="500" height="200"></iframe><br />

</td>
</tr>
<tr>
<td  colspan="1" >
<b>Asunto</b>
</td>
<td  colspan="1" >
<asp:TextBox  runat="server" ID="editctrl000004" CssClass="form-control" Width="400px" MaxLength="250" OnDataBinding="editctrl000004_DataBound_frmdatos" Tooltip="Campo148[]" TextMode="MultiLine" onkeypress="return textCounter(this,250);" onkeydown="return textCounter(this,250);" onkeyup="return textCounter(this,250);" onfocus="return textCounter_GetFocus(this,250);" onblur="return textCounter_LostFocus(this);" /> <span class='error'><b>*</b></span>
<ajaxkit:AutoCompleteExtender runat="server" ID="autoeditctrl000004" TargetControlID="editctrl000004" ServicePath="~/frmMail_detajax.asmx" ServiceMethod="editctrl000004_get" MinimumPrefixLength="3" CompletionInterval="300" EnableCaching="True" CompletionSetCount="20" DelimiterCharacters=";, :" />
<asp:RequiredFieldValidator  runat="server" ID="vrqeditctrl000004" SetFocusOnError="true" CssClass="error" Display="Dynamic" ControltoValidate="editctrl000004" Text="Obligatorio!!! ErrorMessage=´Asunto:es un dato obligatorio!´" ValidationGroup="vgedit" />
<ajaxkit:ValidatorCalloutExtender  runat="server" ID="ajaxvrqeditctrl000004" TargetControlID="vrqeditctrl000004" /><asp:CompareValidator  runat="server" ID="vcdvaleditctrl000004" SetFocusOnError="true" CssClass="error" ControltoValidate="editctrl000004" Display="Dynamic" Text="Ingrese un texto" ErrorMessage="Asunto:Ingrese un texto" Type="String" Operator="DataTypeCheck" ValidationGroup="vgedit" />
<asp:RegularExpressionValidator  runat="server" ID="vrgvaleditctrl000004" SetFocusOnError="true" CssClass="error" Display="Dynamic" ControltoValidate="editctrl000004" ValidationExpression="[\w\s-+/*^()!|°$%&=-_,:;.ñÑ~áéíóúÁÉÍÓÚ#}{]{0,250}" Text="No mayor a 250 caracteres. Deben ser letras o numeros." ErrorMessage="Asunto:No mayor a 250 caracteres. Deben ser letras o numeros." ValidationGroup="vgedit" />
<ajaxkit:ValidatorCalloutExtender  runat="server" ID="ajaxvrgeditctrl000004" TargetControlID="vrgvaleditctrl000004" />

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
<asp:LinkButton runat="server" ID="editctrl000003view" Text='<%# Eval("qsecsiddsc") %>' CssClass="form-control-read" OnDataBinding="editctrl000003view_DataBinding" CausesValidation="False" OnClientClick='<%# "javascript:if (window.showModalDialog) {window.showModalDialog(" & chr(39) & "frmUsuarios_det.aspx?_mode_=0&_closea_=1&param1=" & Eval("qsecsid") & "&timestamp=" & chr(39) & " + new Date().getTime() + " & chr(39) & "" & chr(39) & ", " & chr(39) & "" & chr(39) & ", " & chr(39) & "menubar=no;resizable=yes;toolbar=no;status=no;center=yes;dialogWidth=750px;dialogHeight=570px;" & chr(39) & ");} else {window.open(" & chr(39) & "frmUsuarios_det.aspx?_mode_=0&_closea_=1&param1=" & Eval("qsecsid") & "&timestamp=" & chr(39) & " + new Date().getTime() + " & chr(39) & "" & chr(39) & ", " & chr(39) & "" & chr(39) & ", " & chr(39) & "modal=yes;directories=no;menubar=no;resizable=yes;toolbar=no;status=no;center=yes;dialogWidth=750px;dialogHeight=570px;" & chr(39) & ");};return false;" %>' PostBackUrl='<%# "javascript:if (window.showModalDialog) {window.showModalDialog(" & chr(39) & "frmUsuarios_det.aspx?_mode_=0&_closea_=1&param1=" & Eval("qsecsid") & "&timestamp=" & chr(39) & " + new Date().getTime() + " & chr(39) & "" & chr(39) & ", " & chr(39) & "" & chr(39) & ", " & chr(39) & "menubar=no;resizable=yes;toolbar=no;status=no;center=yes;dialogWidth=750px;dialogHeight=570px;" & chr(39) & ");} else {window.open(" & chr(39) & "frmUsuarios_det.aspx?_mode_=0&_closea_=1&param1=" & Eval("qsecsid") & "&timestamp=" & chr(39) & " + new Date().getTime() + " & chr(39) & "" & chr(39) & ", " & chr(39) & "" & chr(39) & ", " & chr(39) & "modal=yes;directories=no;menubar=no;resizable=yes;toolbar=no;status=no;center=yes;dialogWidth=750px;dialogHeight=570px;" & chr(39) & ");};return false;" %>' />

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
<b>Descripción</b>
</td>
<td  colspan="1" >
<asp:TextBox  runat="server" ID="insctrl000006" CssClass="form-control" Width="400px" MaxLength="50" Tooltip="Campo146[]" TextMode="MultiLine" onkeypress="return textCounter(this,50);" onkeydown="return textCounter(this,50);" onkeyup="return textCounter(this,50);" onfocus="return textCounter_GetFocus(this,50);" onblur="return textCounter_LostFocus(this);" /> <span class='error'><b>*</b></span>
<ajaxkit:AutoCompleteExtender runat="server" ID="autoinsctrl000006" TargetControlID="insctrl000006" ServicePath="~/frmMail_detajax.asmx" ServiceMethod="insctrl000006_get" MinimumPrefixLength="3" CompletionInterval="300" EnableCaching="True" CompletionSetCount="20" DelimiterCharacters=";, :" />
<asp:RequiredFieldValidator  runat="server" ID="vrqinsctrl000006" SetFocusOnError="true" CssClass="error" Display="Dynamic" ControltoValidate="insctrl000006" Text="Obligatorio!!! ErrorMessage=´Descripción:es un dato obligatorio!´" ValidationGroup="vgins" />
<ajaxkit:ValidatorCalloutExtender  runat="server" ID="ajaxvrqinsctrl000006" TargetControlID="vrqinsctrl000006" /><asp:CompareValidator  runat="server" ID="vcdvalinsctrl000006" SetFocusOnError="true" CssClass="error" ControltoValidate="insctrl000006" Display="Dynamic" Text="Ingrese un texto" ErrorMessage="Descripción:Ingrese un texto" Type="String" Operator="DataTypeCheck" ValidationGroup="vgins" />
<asp:RegularExpressionValidator  runat="server" ID="vrgvalinsctrl000006" SetFocusOnError="true" CssClass="error" Display="Dynamic" ControltoValidate="insctrl000006" ValidationExpression="[\w\s-+/*^()!|°$%&=-_,:;.ñÑ~áéíóúÁÉÍÓÚ#}{]{0,50}" Text="No mayor a 50 caracteres. Deben ser letras o numeros." ErrorMessage="Descripción:No mayor a 50 caracteres. Deben ser letras o numeros." ValidationGroup="vgins" />
<ajaxkit:ValidatorCalloutExtender  runat="server" ID="ajaxvrginsctrl000006" TargetControlID="vrgvalinsctrl000006" />

</td>
</tr>
<tr>
<td  colspan="2" >
Contenido
</td>
</tr>
<tr>
<td  colspan="2" >
<asp:HiddenField  runat="server" ID="insctrl000005" OnDataBinding="insctrl000005_DataBound"/><iframe  runat="server" id="fmeinsctrl000005" frameborder="0" width="500" height="200"></iframe><br />

</td>
</tr>
<tr>
<td  colspan="1" >
<b>Asunto</b>
</td>
<td  colspan="1" >
<asp:TextBox  runat="server" ID="insctrl000004" CssClass="form-control" Width="400px" MaxLength="250" Tooltip="Campo148[]" TextMode="MultiLine" onkeypress="return textCounter(this,250);" onkeydown="return textCounter(this,250);" onkeyup="return textCounter(this,250);" onfocus="return textCounter_GetFocus(this,250);" onblur="return textCounter_LostFocus(this);" /> <span class='error'><b>*</b></span>
<ajaxkit:AutoCompleteExtender runat="server" ID="autoinsctrl000004" TargetControlID="insctrl000004" ServicePath="~/frmMail_detajax.asmx" ServiceMethod="insctrl000004_get" MinimumPrefixLength="3" CompletionInterval="300" EnableCaching="True" CompletionSetCount="20" DelimiterCharacters=";, :" />
<asp:RequiredFieldValidator  runat="server" ID="vrqinsctrl000004" SetFocusOnError="true" CssClass="error" Display="Dynamic" ControltoValidate="insctrl000004" Text="Obligatorio!!! ErrorMessage=´Asunto:es un dato obligatorio!´" ValidationGroup="vgins" />
<ajaxkit:ValidatorCalloutExtender  runat="server" ID="ajaxvrqinsctrl000004" TargetControlID="vrqinsctrl000004" /><asp:CompareValidator  runat="server" ID="vcdvalinsctrl000004" SetFocusOnError="true" CssClass="error" ControltoValidate="insctrl000004" Display="Dynamic" Text="Ingrese un texto" ErrorMessage="Asunto:Ingrese un texto" Type="String" Operator="DataTypeCheck" ValidationGroup="vgins" />
<asp:RegularExpressionValidator  runat="server" ID="vrgvalinsctrl000004" SetFocusOnError="true" CssClass="error" Display="Dynamic" ControltoValidate="insctrl000004" ValidationExpression="[\w\s-+/*^()!|°$%&=-_,:;.ñÑ~áéíóúÁÉÍÓÚ#}{]{0,250}" Text="No mayor a 250 caracteres. Deben ser letras o numeros." ErrorMessage="Asunto:No mayor a 250 caracteres. Deben ser letras o numeros." ValidationGroup="vgins" />
<ajaxkit:ValidatorCalloutExtender  runat="server" ID="ajaxvrginsctrl000004" TargetControlID="vrgvalinsctrl000004" />

</td>
</tr>
</table>
<!-- End Plantilla insercion --></InsertItemTemplate>

</asp:FormView>

			<asp:SqlDataSource  runat="server" ID="dsdatos" ProviderName="System.Data.SqlClient" CancelSelectOnNullParameter="False" onInit="dsdatos_Init" SelectCommandType="Text"
 SelectCommand="SELECT MAILS.cod,MAILS.dsc,MAILS.content,MAILS.subject,CASE MAILSQSECSID.sidtypecod WHEN -3 THEN (SELECT TOP 1 Q_SECPGRP.grpcod FROM Q_SECPGRP WHERE Q_SECPGRP.sidcod=MAILSQSECSID.sidcod) WHEN -2 THEN (SELECT TOP 1 Q_SECPLOGIN.seccod FROM Q_SECPLOGIN WHERE Q_SECPLOGIN.sidcod=MAILSQSECSID.sidcod) END as qsecsidvalue,CASE MAILSQSECSID.sidtypecod WHEN -3 THEN (SELECT TOP 1 Q_SECPGRP.grpdsc FROM Q_SECPGRP WHERE Q_SECPGRP.grpcod= (SELECT TOP 1 Q_SECPGRP.grpcod FROM Q_SECPGRP WHERE Q_SECPGRP.sidcod=MAILSQSECSID.sidcod)) WHEN -2 THEN (SELECT TOP 1 Q_SECPLOGIN.secdsc FROM Q_SECPLOGIN WHERE Q_SECPLOGIN.seccod= (SELECT TOP 1 Q_SECPLOGIN.seccod FROM Q_SECPLOGIN WHERE Q_SECPLOGIN.sidcod=MAILSQSECSID.sidcod)) END as qsecsiddsc,(MAILSQSECSID.sidtypecod) as qsecsiddsc,MAILS.qsecdatetime,MAILS.qsecsid FROM MAILS  LEFT JOIN Q_SECPSID AS MAILSQSECSID ON MAILSQSECSID.sidcod=MAILS.qsecsid  WHERE MAILS.cod = @param1" onselected="dsdatos_Selected" UpdateCommandType="Text"
 UpdateCommand="UPDATE MAILS SET dsc=@dsc,content=@content,subject=@subject,qsecsid=@qsecsid,qsecdatetime=getdate() WHERE MAILS.cod = @param1" onupdated="dsdatos_Updated" InsertCommandType="Text"
 InsertCommand="DECLARE @querynextcodcod int  SET @querynextcodcod =(SELECT ISNULL(MAX(cod),0)+1 FROM MAILS WHERE cod > 0 ) INSERT INTO MAILS (cod,dsc,content,subject,qsecsid,qsecdatetime) VALUES(@querynextcodcod,@dsc,@content,@subject,@qsecsid,getdate()) SELECT @querynextcod =@querynextcodcod" onInserted="dsdatos_Inserted" DeleteCommandType="Text"
 DeleteCommand="DELETE FROM MAILS WHERE cod = @param1" ondeleted="dsdatos_Deleted" >
<InsertParameters>
<asp:Parameter  Name="querynextcod" Direction="Output" Type="Int32" />
<asp:ControlParameter  Name="dsc" ControlID="frmdatos$insctrl000006" ConvertEmptyStringToNull="True" Direction="InputOutput" Type="String" Size="50" PropertyName="Text" />
<asp:Parameter  Name="content" Direction="InputOutput" Type="String" Size="10000" />
<asp:ControlParameter  Name="subject" ControlID="frmdatos$insctrl000004" ConvertEmptyStringToNull="True" Direction="InputOutput" Type="String" Size="250" PropertyName="Text" />
<asp:SessionParameter  Name="qsecsid" SessionField="secsid" Direction="Input" />
<asp:Parameter  Name="param1" Direction="ReturnValue" />
</InsertParameters>
<SelectParameters>
<asp:QueryStringParameter  Name="param1" QueryStringField="param1" ConvertEmptyStringToNull="True" Type="String" Size="200" />
</SelectParameters>
<UpdateParameters>
<asp:QueryStringParameter  Name="param1" QueryStringField="param1" ConvertEmptyStringToNull="True" Type="String" Size="200" />
<asp:ControlParameter  Name="dsc" ControlID="frmdatos$editctrl000006" ConvertEmptyStringToNull="True" Direction="InputOutput" Type="String" Size="50" PropertyName="Text" />
<asp:Parameter  Name="content" Direction="InputOutput" Type="String" Size="10000" />
<asp:ControlParameter  Name="subject" ControlID="frmdatos$editctrl000004" ConvertEmptyStringToNull="True" Direction="InputOutput" Type="String" Size="250" PropertyName="Text" />
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

