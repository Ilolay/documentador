<%@ Page Language="VB" CodeFile="frmsysparam_det.aspx.vb" MasterPageFile="general.master" AutoEventWireup="True" Inherits="frmsysparam_det" Title="Parámetros del sistema" %>
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
<tr><td  style="width:32px;" colspan="1" ><img  src="imagenes/iconParametro.png" width="32px" height="32px" alt="Parámetros del sistema" hspace="4"/></td>
<td  style="text-align:left;" colspan="1" ><span class="form-title">Parámetros del sistema</span> |<asp:Label  runat="server" ID="lblsubtitle" CssClass="form-subtitle" />
</td></tr>
</table>
</td></tr>
<tr><td  colspan="1" >				<!-- Init -->
<table id="body" style="background-color:#F0F0F0;" width="100%">
					<br /><asp:Label  runat="server" ID="lblerror" CssClass="error" width="100%" />			<asp:ValidationSummary  runat="server" ID="frmdatosvalsumary" />
<asp:FormView  ID="frmdatos" runat="server" DataSourceID="dsdatos" DataKeyNames="sysparamcod" DefaultMode="ReadOnly" OnItemDeleted="frmdatos_ItemDeleted" OnItemUpdated="frmdatos_ItemUpdated" OnItemInserted="frmdatos_ItemInserted" OnDataBound="frmdatos_ItemSelected" >
<ItemTemplate>

<!-- Init Plantilla item --><div  runat="server" Visible='<%# Request.Querystring("_view_")<> 1 %>' ><asp:Button  runat="server" ID="cmdrefresh" Text="Refrescar" CssClass="boton-acciones" CausesValidation="False" />
<asp:Button  runat="server" ID="cmdFormViewItemCancel" Text="Cancelar" CssClass="boton-acciones" CausesValidation="False" OnClick="cmdFormViewItemCancel_Click" />
<asp:Button  runat="server" ID="cmdFormViewItemUpdate" Text="Editar" CssClass="boton-acciones" CausesValidation="False" OnClick="cmdFormViewItemUpdate_Click" />
</div>
<table width="100%" cellpadding="1" cellspacing="1"><tr>
<td  colspan="1" >
Código
</td>
<td  colspan="1" >
<asp:Label  runat="server" ID="itemctrl000007" CssClass="form-control-read" width="100%" Text='<%# Eval("sysparamcod").ToString %>' />
</td>
</tr>
<tr>
<td  colspan="1" >
Código
</td>
<td  colspan="1" >
<asp:Label  runat="server" ID="itemctrl000005" CssClass="form-control-read" width="100%" Text='<%# Eval("sysparamcod").ToString %>' />
</td>
</tr>
<tr>
<td  colspan="1" >
Nombre
</td>
<td  colspan="1" >
<asp:Label  runat="server" ID="itemctrl000004" CssClass="form-control-read" width="100%" Text='<%# Replace(Eval("sysparamobs").ToString,Chr(10),"<br />") %>' />
</td>
</tr>
<tr>
<td  colspan="1" >
Valor
</td>
<td  colspan="1" >
<asp:Label  runat="server" ID="itemctrl000003" CssClass="form-control-read" width="100%" Text='<%# Replace(Eval("sysparamdsc").ToString,Chr(10),"<br />") %>' />
</td>
</tr>
<tr>
<td  colspan="1" >
ID
</td>
<td  colspan="1" >
<asp:Label  runat="server" ID="itemctrl000002" CssClass="form-control-read" width="100%" Text='<%# Eval("sysparamID").ToString %>' />
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
Código
</td>
<td  colspan="1" >
<asp:Label  runat="server" ID="editctrl000005" CssClass="form-control-read" width="100%" Text='<%# Eval("sysparamcod").ToString %>' />
</td>
</tr>
<tr>
<td  colspan="1" >
Nombre
</td>
<td  colspan="1" >
<asp:Label  runat="server" ID="editctrl000004" CssClass="form-control-read" width="100%" Text='<%# Replace(Eval("sysparamobs").ToString,Chr(10),"<br />") %>' />
</td>
</tr>
<tr>
<td  colspan="1" >
Valor
</td>
<td  colspan="1" >
<asp:TextBox  runat="server" ID="editctrl000003" CssClass="form-control" Width="400px" MaxLength="200" Text='<%# Eval("sysparamdsc").ToString %>' Tooltip="Campo100097[]" TextMode="MultiLine" onkeypress="return textCounter(this,200);" onkeydown="return textCounter(this,200);" onkeyup="return textCounter(this,200);" onfocus="return textCounter_GetFocus(this,200);" onblur="return textCounter_LostFocus(this);" />
<asp:CompareValidator  runat="server" ID="vcdvaleditctrl000003" SetFocusOnError="true" CssClass="error" ControltoValidate="editctrl000003" Display="Dynamic" Text="Ingrese un texto" ErrorMessage="Valor:Ingrese un texto" Type="String" Operator="DataTypeCheck" ValidationGroup="vgedit" />
<asp:RegularExpressionValidator  runat="server" ID="vrgvaleditctrl000003" SetFocusOnError="true" CssClass="error" Display="Dynamic" ControltoValidate="editctrl000003" ValidationExpression="[\w\s-+/*^()!|°$%&=-_,:;.ñÑ~áéíóúÁÉÍÓÚ#}{]{0,200}" Text="No mayor a 200 caracteres. Deben ser letras o numeros." ErrorMessage="Valor:No mayor a 200 caracteres. Deben ser letras o numeros." ValidationGroup="vgedit" />
<ajaxkit:ValidatorCalloutExtender  runat="server" ID="ajaxvrgeditctrl000003" TargetControlID="vrgvaleditctrl000003" />

</td>
</tr>
<tr>
<td  colspan="1" >
ID
</td>
<td  colspan="1" >
<asp:Label  runat="server" ID="editctrl000002" CssClass="form-control-read" width="100%" Text='<%# Eval("sysparamID").ToString %>' />
</td>
</tr>
</table>
<!-- End Plantilla edicion --></EditItemTemplate>

</asp:FormView>

			<asp:SqlDataSource  runat="server" ID="dsdatos" ProviderName="System.Data.SqlClient" CancelSelectOnNullParameter="False" onInit="dsdatos_Init" SelectCommandType="Text"
 SelectCommand="SELECT Q_SYSPARAM.sysparamcod,Q_SYSPARAM.sysparamobs,Q_SYSPARAM.sysparamdsc,Q_SYSPARAM.sysparamID FROM Q_SYSPARAM   WHERE Q_SYSPARAM.sysparamcod = @param1" onselected="dsdatos_Selected" UpdateCommandType="Text"
 UpdateCommand="UPDATE Q_SYSPARAM SET sysparamcod=@sysparamcod,sysparamdsc=@sysparamdsc WHERE Q_SYSPARAM.sysparamcod = @param1" onupdated="dsdatos_Updated" >
<SelectParameters>
<asp:QueryStringParameter  Name="param1" QueryStringField="param1" ConvertEmptyStringToNull="True" Type="String" Size="200" />
</SelectParameters>
<UpdateParameters>
<asp:QueryStringParameter  Name="param1" QueryStringField="param1" ConvertEmptyStringToNull="True" Type="String" Size="200" />
<asp:ControlParameter  Name="sysparamcod" ControlID="frmdatos$editctrl000005" ConvertEmptyStringToNull="True" Direction="InputOutput" Type="Int32" PropertyName="Text" />
<asp:ControlParameter  Name="sysparamdsc" ControlID="frmdatos$editctrl000003" ConvertEmptyStringToNull="True" Direction="InputOutput" Type="String" Size="200" PropertyName="Text" />
</UpdateParameters>
</asp:SqlDataSource>
</table>
<!-- End -->
</td></tr></table>
</asp:Content>

