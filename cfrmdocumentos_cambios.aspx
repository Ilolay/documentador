<%@ Page Title="" Language="VB" MasterPageFile="general.master" AutoEventWireup="false" CodeFile="cfrmdocumentos_cambios.aspx.vb" Inherits="cfrmdocumentos_cambios" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="ajaxkit" %>
<asp:Content ContentPlaceHolderID ="conHeader" runat="server" ID="conheader">
    <style media="print">
.hide_print {display: none;}
</style>
<style type="text/css"  >
 
</style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentDatos" Runat="Server">
    <asp:ScriptManager  ID="ScriptManagerHrc" runat="server" EnableScriptLocalization="True" EnableScriptGlobalization="True" ScriptMode="Release" >
</asp:ScriptManager>
<asp:UpdatePanel  ID="updupdpnlActions" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="True" >
<ContentTemplate>
<table width="100%" cellpadding="0" cellspacing="0">
<tr><td  colspan="1" >
<table width="100%" cellpadding="0" cellspacing="0">
<tr><td  style="width:32px;" colspan="1" ><img src="imagenes/icon00000001.png" width="32" height="32" hspace="4" alt="Documentos" /></td>
<td  style="text-align:left;" colspan="1" ><span class="form-title">Cambios en versiones</span></td></tr>
</table>
</td></tr>
<tr>
<td  colspan="1" >

<!-- Init Filtros de busqueda -->
<table id="Search" class="hide_print" style="background-color:#F0F0F0;" width="100%">
<tr>
<td  class="search-title" colspan="5" >Filtros de búsqueda</td></tr>

											<tr>
<td  class="search-item-title" style="vertical-align:top;width:15%" colspan="1" >
												Versión a comparar
												</td>
												<td  class="search-table" style="vertical-align:top;width:35%" colspan="1" >												
                        <asp:DropDownList  runat="server" ID="cmbVersion" CssClass="form-control"  AppendDataBoundItems="False" DataTextField="hstdsc" DataValueField="hstcod" Enabled="True"  >
                        <asp:ListItem value="-1">Todos</asp:ListItem>
                        </asp:DropDownList>

												</td>
											

<td  class="search-item-title" style="vertical-align:top;width:15%" colspan="1" >
										</td>
										<td  class="search-table" style="vertical-align:top;width:35%" colspan="1" >
										</td>
									</tr>
			</table>
<!-- End Filtros de busqueda -->
</td>
</tr>
<tr>
<td  colspan="1" >

<!-- Init -->
<table id="Actions" class="hide_print" style="background-color:#F0F0F0;" width="100%">
<tr>
<td  class="search-title" >
    <table>
    <tr>
    <td>
        <asp:Button  runat="server" ID="cmdViewChanges" Text="Ver cambios" CssClass="boton-acciones" CausesValidation="True" EnableViewState="False" OnClick="cmdViewChanges_Click" ValidationGroup="vg" />
    </td>
    <td><b>Referencias:</b></td>
    <td><div style="background-color:Yellow; width:10px;height:10px"></td>
    <td>Agregado</td>
    <td><div style="background-color:Red; width:10px;height:10px"></td>
    <td>Eliminado</td>
    </tr>
    </table>

		</td></tr>		
</table>
<!-- End -->


</td>
</tr>
<tr>
<td  colspan="1" >				
<!-- Init -->
<table id="Keys" style="background-color:#F0F0F0;" width="100%">
		<tr>
			<td colspan="4" >						
			<div style="clear:both ;">
				<asp:Literal  runat="server" ID="lblCambios"  Mode="PassThrough"   />
				</div>
    		</td>
	    </tr>
	</table>
<!-- End -->
</td>
</tr>

</table>
</ContentTemplate>
</asp:UpdatePanel>
<asp:UpdateProgress ID="UpdateProgress5"  runat="server" AssociatedUpdatePanelID="updupdpnlActions" EnableViewState="False" >
<ProgressTemplate><span  class="ajaxupd" ><div style="position:absolute;top:2px;left:50%; border-color:#333333; border:1; border-style:solid; background-color:#FFFFFF;padding:3px;"><image src="imagenes/ajaxupdate.gif" border=0 />Actualizando</div></span></ProgressTemplate>
</asp:UpdateProgress>
</asp:Content>

