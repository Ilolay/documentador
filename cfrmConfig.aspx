<%@ Page Language="VB" MasterPageFile="general.master" AutoEventWireup="false" CodeFile="cfrmConfig.aspx.vb" Inherits="cfrmConfig" %>
<asp:Content id="cDatos" ContentPlaceHolderID="ContentDatos" runat="Server">

<table width="100%" cellpadding="0" cellspacing="0">
<tr><td  colspan="1" >
<table width="100%" cellpadding="0" cellspacing="0">
<tr><td  style="width:32px;" colspan="1" ><img  src="imagenes/iconconfiguracion.png" width="32px" height="32px" alt="Tipos de documentos" hspace="4"/></td>
<td  style="text-align:left;" colspan="1" ><span class="form-title">Opciones de configuración</span></td></tr>
</table>
</td></tr>
<tr>
<td colspan="2">
<table id="Search" width="100%" cellpadding="0" cellspacing="0">
<tr>
<td  class="search-title" colspan="3" >Importaciones y configuraciones</td></tr>
<tr>
<td  class="form-tabla-control" colspan="2" >
    <asp:Label  runat="server" ID="lblerror" CssClass="error" width="100%"></asp:Label>
</td>
</tr>
<tr>
<td  class="tabla-fila-alternativa" >Replicar datos:
    </td>
<td class="tabla-fila-alternativa">
    <asp:Button  runat="server" ID="cmdReplicate" Text="Replicar ahora" 
        CssClass="boton-acciones"  />
    <asp:Button  runat="server" ID="cmdProceso" Text="Proceso" 
        CssClass="boton-acciones"  />
    </td>
    <td class="tabla-fila-alternativa" >
    <br />
</td>
</tr>
</table>
</td>
</tr>
</table></asp:Content>


