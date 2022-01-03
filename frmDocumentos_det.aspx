<%@ Page Language="VB" CodeFile="frmDocumentos_det.aspx.vb" MasterPageFile="general.master" AutoEventWireup="True" Inherits="frmDocumentos_det" Title="Documentos" %>
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
<tr><td  style="width:32px;" colspan="1" ><img  src="imagenes/icon00000001.png" width="32px" height="32px" alt="Documentos" hspace="4"/></td>
<td  style="text-align:left;" colspan="1" ><span class="form-title">Documentos</span> |<asp:Label  runat="server" ID="lblsubtitle" CssClass="form-subtitle" EnableViewState="False" />
</td></tr>
</table>
</td></tr>
</table>
</asp:Content>

