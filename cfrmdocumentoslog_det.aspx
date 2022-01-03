<%@ Page Language="VB" MasterPageFile="general.master" AutoEventWireup="false" CodeFile="cfrmdocumentoslog_det.aspx.vb" Inherits="cfrmdocumentoslog_det"  %>
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
    <tr><td  style="text-align:center" colspan="1" ><span class="form-title">Historia</span> |<asp:Label  runat="server" ID="lblsubtitle" CssClass="form-subtitle" /></td></tr>
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

                 <asp:GridView ID="grdPROLOG" runat="server" AutoGenerateColumns="False" 
                    DataKeyNames="cod" Width="100%"  AllowSorting="True" 
                    >
               <RowStyle CssClass="pryceldaotro"  />
               <HeaderStyle CssClass="pryceldatitulos" />                            
               <FooterStyle CssClass ="pryceldaotro"  />
               <Columns>
                    <asp:TemplateField ItemStyle-Width="120px" HeaderText="Fecha:" HeaderStyle-HorizontalAlign="Center"  ItemStyle-HorizontalAlign="Center"
                        SortExpression="fecha">
                        <ItemTemplate>
                            <asp:Label ID="lblfecha" runat="server" Text='<%# If(IsDBNull(Eval("fecha")), "", CDate(Eval("fecha")).ToString("d/M/yyyy HH:mm:ss"))%>'   ></asp:Label>
                        </ItemTemplate>
                <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                <ItemStyle HorizontalAlign="Center" Width="120px"></ItemStyle>
                    </asp:TemplateField>
                    <asp:TemplateField ItemStyle-Width="120px" HeaderText="Usuario:" SortExpression="empdsc"  HeaderStyle-HorizontalAlign="Center"  ItemStyle-HorizontalAlign="Left" >
                        <ItemTemplate>
                            <asp:Label ID="lblusuario" runat="server" Text='<%# Eval("empdsc") %>'  />
                        </ItemTemplate>                                                                     
                <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                  <ItemStyle HorizontalAlign="Center" Width="120px"></ItemStyle>
                    </asp:TemplateField>
                             <asp:TemplateField ItemStyle-Width="120px" HeaderText="Actor:" SortExpression="del_empdsc"  HeaderStyle-HorizontalAlign="Center"  ItemStyle-HorizontalAlign="Left" >
                        <ItemTemplate>
                            <asp:Label ID="lbldelempdsc" runat="server" Text='<%# Eval("del_empdsc") %>'  />
                        </ItemTemplate>                                                                     
                <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                  <ItemStyle HorizontalAlign="Center" Width="120px"></ItemStyle>
                    </asp:TemplateField>
                    <asp:TemplateField ItemStyle-Width="200px" HeaderText="Paso:" SortExpression="wfwstpprevdsc"  HeaderStyle-HorizontalAlign="Center"  ItemStyle-HorizontalAlign="Left" >
                        <ItemTemplate>
                            <asp:Label ID="lblwfwstpnextdsc" runat="server" Text='<%# Eval("wfwstpprevdsc") %>'  />
                        </ItemTemplate>                                                                     
                    <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                <ItemStyle HorizontalAlign="Center" Width="120px"></ItemStyle>
                    </asp:TemplateField>
                    <asp:TemplateField ItemStyle-Width="200px" HeaderText="Evento:" SortExpression="dsc"  HeaderStyle-HorizontalAlign="Center"  ItemStyle-HorizontalAlign="Left" >
                        <ItemTemplate>
                            <asp:Label ID="lbldsc" runat="server" Text='<%# Eval("dsc") %>'  />
                        </ItemTemplate>                                                                     
                    <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                <ItemStyle HorizontalAlign="Left" Width="200px"></ItemStyle>
                    </asp:TemplateField>
                    <asp:TemplateField ItemStyle-Width="200px" HeaderText="Observaciones:" SortExpression="obs"  HeaderStyle-HorizontalAlign="Center"  ItemStyle-HorizontalAlign="Left" >
                        <ItemTemplate>
                            <asp:Label ID="lbldsc" runat="server" Text='<%# Eval("obs") %>'  />
                        </ItemTemplate>
                <HeaderStyle HorizontalAlign="Center"></HeaderStyle>

<ItemStyle HorizontalAlign="Left" Width="200px"></ItemStyle>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Estado previo">
                        <ItemTemplate>
                            <asp:LinkButton runat="server" ID="hyparchivo"   OnClientClick ='<%#  "javascript:if (window.showModalDialog) {window.showModalDialog(""cfrmdocumentos1_det.aspx?_mode_=0&_closea_=1&param1=-1&hstparam1=" & Eval("hsthidgencod").ToString & "&timestamp="" + new Date().getTime(), """", ""menubar=no;resizable=yes;toolbar=no;status=no;center=yes;dialogWidth="" +  (screen.availWidth - 10) + ""px;dialogHeight=600px;"");} else {window.open(""cfrmdocumentos1_det.aspx?_mode_=0&_closea_=1&param1=-1&hstparam1=" & Eval("hsthidgencod").ToString & "&timestamp="" + new Date().getTime(), """", ""modal=yes;directories=no;menubar=no;resizable=yes;toolbar=no;status=no;center=yes;dialogWidth="" +  (screen.availWidth -10 ) + ""px;dialogHeight=600px;"");};return false;" %>' 
                            Text="<img src=imagenes/file.png width=20 height=20 border=0 >"  Visible='<%# if(Eval("hsthidgencod") >=1,"True","False") %>' /> 
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
                <HeaderStyle BackColor="#F0F0F0" CssClass="prysubtablaceldatitulos" />
                </asp:GridView>
                </td>
             </tr>
        </table>


</asp:Content>

