<%@ Page Language="VB" MasterPageFile="general.master" AutoEventWireup="false" CodeFile="cfrmdocumentosref_det.aspx.vb" Inherits="cfrmdocumentosref_det"  %>
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
    <tr><td  style="text-align:center" colspan="1" >
        <span ><asp:Label  runat="server" ID="lblTitle" CssClass="form-title" /></span> |<asp:Label  runat="server" ID="lblsubtitle" CssClass="form-subtitle" />
        </td></tr>
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
                <asp:SqlDataSource runat="server" ID="dsPROSGN"                       
                  >
                <SelectParameters>
                <asp:QueryStringParameter QueryStringField="param1" DefaultValue="-1" Name="doccod" />
                </SelectParameters>
                </asp:SqlDataSource>
                 <asp:GridView ID="grdDOCSGN" runat="server" AutoGenerateColumns="False" 
                    DataKeyNames="cod" DataSourceID="dsPROSGN" Width="100%"  AllowSorting="False" 
                    >
               <RowStyle CssClass="pryceldaotro"  />
               <HeaderStyle CssClass="pryceldatitulos" />                            
               <FooterStyle CssClass ="pryceldaotro"  />
                    <EmptyDataTemplate>
                    <p  class="tabla-fila"><img  src="./imagenes/evwarning.png" width="16px" alt="Sin datos"/>No hay referencias a otros documentos</p>
                    </EmptyDataTemplate>
               <Columns>               
                 <asp:TemplateField HeaderText="Referencias a los siguientes documentos:"  HeaderStyle-HorizontalAlign="Center"  ItemStyle-HorizontalAlign="Center" >
                        <ItemTemplate>
                            <asp:Label ID="lbldocdsc" runat="server" Text='<%# Eval("docdsc")%>'  />
                        </ItemTemplate>
                    <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                    <ItemStyle HorizontalAlign="Left"></ItemStyle>
                </asp:TemplateField>
                </Columns>
                <HeaderStyle BackColor="#F0F0F0" CssClass="prysubtablaceldatitulos" />
                </asp:GridView>
                </td>
             </tr>
             <tr>
             <td>
              <asp:SqlDataSource runat="server" ID="dsReferido" 
                  >
                <SelectParameters>
                <asp:QueryStringParameter QueryStringField="param1" DefaultValue="-1" Name="doccod" />
                </SelectParameters>
                </asp:SqlDataSource>
                 <asp:GridView ID="grdReferido" runat="server" AutoGenerateColumns="False" 
                    DataKeyNames="cod" DataSourceID="dsReferido" Width="100%"  AllowSorting="False" 
                    >
               <RowStyle CssClass="pryceldaotro"  />
               <HeaderStyle CssClass="pryceldatitulos" />                            
               <FooterStyle CssClass ="pryceldaotro"  />
                   <EmptyDataTemplate>
                    <p  class="tabla-fila"><img  src="./imagenes/evwarning.png" width="16px" alt="Sin datos"/>
                        No hay documentos que referencien a este documento</p>
                    </EmptyDataTemplate>

               <Columns>               
                 <asp:TemplateField HeaderText="Documentos que refieren a este documento:"  HeaderStyle-HorizontalAlign="Center"  ItemStyle-HorizontalAlign="Center" >
                        <ItemTemplate>
                            <asp:Label ID="lbldocdsc" runat="server" Text='<%# Eval("docdsc")%>'  />
                        </ItemTemplate>
                    <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                    <ItemStyle HorizontalAlign="Left"></ItemStyle>
                </asp:TemplateField>
                </Columns>
                <HeaderStyle BackColor="#F0F0F0" CssClass="prysubtablaceldatitulos" />
                </asp:GridView>
             </td>
             </tr>
        </table>


</asp:Content>

