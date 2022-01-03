<%@ Page Language="VB" MasterPageFile="general.master" AutoEventWireup="false" CodeFile="cfrmtrace.aspx.vb" Inherits="cfrmtrace"  %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="ajaxkit" %>
<asp:Content ContentPlaceHolderID ="conHeader" runat="server" ID="conheader">
<style media="print">
.hide_print {display: none;}
</style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentDatos" Runat="Server">   
    <asp:ScriptManager ID="ScriptManager1" runat="server" EnableScriptLocalization="True" EnableScriptGlobalization="True" ScriptMode="Release" >
    </asp:ScriptManager> 
    <table width="100%" >
    <tr><td  style="text-align:center" colspan="1" ><span class="form-title">Seguimiento</span> |<asp:Label  runat="server" ID="lblsubtitle" CssClass="form-subtitle" /></td></tr>
    <tr>
        <td>
            <asp:Label  runat="server" ID="lblerror" CssClass="error" width="100%"></asp:Label>
        </td>
    </tr>
    <tr>
        <td  class="search-title" >  
            <asp:HiddenField runat="server" ID="hdnProCod"  Value="-1" />  
            <asp:Button  runat="server" ID="cmdrefresh" Text="Refrescar" CssClass="boton-acciones" CausesValidation="False" />
            <asp:Button  runat="server" ID="cmdFormViewItemCancel" Text="Salir" CssClass="boton-acciones" CausesValidation="False" OnClick="cmdFormViewItemCancel_Click" />
        </td>
    </tr>
    </table>
    <table style="width:100%;">
        <tr>
        <td>
            <asp:GridView ID="grdData" runat="server" AutoGenerateColumns="False" 
                 Width="100%"   PageSize="100"  AllowSorting=true 
                >
           <RowStyle CssClass="pryceldaotro"  />
           <HeaderStyle CssClass="pryceldatitulos" />                            
           <FooterStyle CssClass ="pryceldaotro"  />
           <Columns>
                   <asp:TemplateField ItemStyle-Width="20px" ItemStyle-HorizontalAlign="Center"  >
                   <ItemTemplate>
                       <asp:Image ID="Image1" runat="server"  ImageUrl="~/imagenes/objnotes.png" Width="12px" BorderStyle="None"  />
                   </ItemTemplate>
                   </asp:TemplateField>
                <asp:BoundField DataField="trcfecha" HeaderText="Fecha:" ItemStyle-Width="150px" SortExpression="trcfecha" DataFormatString="{0:d/M/yyyy HH:mm:ss.ms}" />
                <asp:BoundField DataField="secdsc" HeaderText="Usuario:" ItemStyle-Width="150px" SortExpression="secdsc"  />                
                <asp:BoundField DataField="trcdsc" HeaderText="Detalle:" SortExpression="trcdsc"  />
            </Columns>
            </asp:GridView>
        </td>
     </tr>
     </table>
</asp:Content>

