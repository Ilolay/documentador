<%@ Page Language="VB" MasterPageFile="general.master" AutoEventWireup="false" CodeFile="cfrmdocumentossgn_det.aspx.vb" Inherits="cfrmdocumentossgn_det"  %>
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
    <tr><td  style="text-align:center" colspan="1" ><span class="form-title">Firmas pendientes</span> |<asp:Label  runat="server" ID="lblsubtitle" CssClass="form-subtitle" /></td></tr>
    <tr>
        <td>
            <asp:Label  runat="server" ID="lblerror" CssClass="error" width="100%"></asp:Label>
        </td>
    </tr>
    <tr>
        <td  class="search-title" >  
            <asp:Button  runat="server" ID="cmdrefresh" Text="Refrescar" CssClass="boton-acciones" CausesValidation="False" />
            <asp:Button  runat="server" ID="cmdFormViewItemCancel" Text="Salir" CssClass="boton-acciones" CausesValidation="False" OnClick="cmdFormViewItemCancel_Click" />
        </td>
    </tr>
    </table>
    <table style="width:100%;">
             <tr>
                <td>
              
                 <asp:GridView ID="grdDOCSGN" runat="server" AutoGenerateColumns="False" 
                    DataKeyNames="cod"  Width="100%"  AllowSorting="False" 
                    >
               <RowStyle CssClass="pryceldaotro"  />
               <HeaderStyle CssClass="pryceldatitulos" />                            
               <FooterStyle CssClass ="pryceldaotro"  />
               <Columns>
                 <asp:TemplateField ItemStyle-Width="20px" HeaderStyle-HorizontalAlign="Center"  ItemStyle-HorizontalAlign="Center" >
                        <ItemTemplate>
                        <asp:Image  runat="server" ID="rowalerta" Width="20px" GenerateEmptyAlternateText="True" 
                            ImageURL='<%# If(Eval("avisoscant") >= Eval("avisoscantmax"), "imagenes/everror.png", If(Eval("avisoscant") = 0, "imagenes/sinimagen.jpg", "imagenes/evwarning.png"))%>'
                            BorderWidth="0"  />                           
                        </ItemTemplate>
                    <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                    <ItemStyle HorizontalAlign="Center" Width="20px"></ItemStyle>
                 </asp:TemplateField>
                 <asp:TemplateField HeaderText="Usuario:"  HeaderStyle-HorizontalAlign="Center"  ItemStyle-HorizontalAlign="Center" >
                        <ItemTemplate>
                            <asp:Label ID="lblusuario" runat="server" Text='<%# Eval("empdsc") %>'  />
                        </ItemTemplate>
                    <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                    <ItemStyle HorizontalAlign="Center"></ItemStyle>
                </asp:TemplateField>
                  <asp:TemplateField HeaderText="Paso requerido:"  HeaderStyle-HorizontalAlign="Center"  ItemStyle-HorizontalAlign="Center" >
                        <ItemTemplate>
                            <asp:Label ID="lblwfwstpdsc" runat="server" Text='<%# Eval("wfwstpdsc") %>'  />
                        </ItemTemplate>
                    <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                    <ItemStyle HorizontalAlign="Center"></ItemStyle>
                </asp:TemplateField>
                  <asp:TemplateField HeaderText="Versión:"  HeaderStyle-HorizontalAlign="Center"  ItemStyle-HorizontalAlign="Center" >
                        <ItemTemplate>
                            <asp:Label ID="lblversion" runat="server" Text='<%# Eval("version") %>'  />
                        </ItemTemplate>
                    <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                    <ItemStyle HorizontalAlign="Center"></ItemStyle>
                </asp:TemplateField>
                  <asp:TemplateField HeaderText="Fecha firma requerida:"  HeaderStyle-HorizontalAlign="Center"  ItemStyle-HorizontalAlign="Center" >
                        <ItemTemplate>
                            <asp:Label ID="lblusuario" runat="server" Text='<%# cdate(Eval("fechainicio")).Tostring("d/M/yyyy HH:mm:ss") %>'  />
                        </ItemTemplate>
                    <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                    <ItemStyle HorizontalAlign="Center" Width="120px"></ItemStyle>
                </asp:TemplateField>
                 <asp:TemplateField ItemStyle-Width="120px" HeaderText="Avisos enviados" HeaderStyle-HorizontalAlign="Center"  ItemStyle-HorizontalAlign="Center"  >
                        <ItemTemplate>
                            <asp:Label ID="lblavisos" runat="server" Text='<%# Eval("avisoscant") %>'   ></asp:Label>
                        </ItemTemplate>
                    <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                    <ItemStyle HorizontalAlign="Center" Width="120px"></ItemStyle>
                 </asp:TemplateField>
                   <asp:TemplateField ItemStyle-Width="120px" HeaderText="Fecha ult.mail enviado" HeaderStyle-HorizontalAlign="Center"  ItemStyle-HorizontalAlign="Center"  >
                        <ItemTemplate>
                            <asp:Label ID="lblfechaultmail" runat="server" Text='<%# If(IsDBNull(Eval("fechaultmail")),"",cdate(Eval("fechaultmail")).ToString("d/M/yyyy")) %>'   ></asp:Label>
                        </ItemTemplate>
                    <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                    <ItemStyle HorizontalAlign="Center" Width="120px"></ItemStyle>
                 </asp:TemplateField>
                   <asp:TemplateField ItemStyle-Width="120px" HeaderText="Obs. ult.mail" HeaderStyle-HorizontalAlign="Center"  ItemStyle-HorizontalAlign="Center"  >
                        <ItemTemplate>
                            <asp:Label ID="lblultmailobs" runat="server" Text='<%# Eval("ultmailobs") %>'   ></asp:Label>
                        </ItemTemplate>
                    <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                    <ItemStyle HorizontalAlign="Center" Width="120px"></ItemStyle>
                 </asp:TemplateField>
                </Columns>
                <HeaderStyle BackColor="#F0F0F0" CssClass="prysubtablaceldatitulos" />
                </asp:GridView>
                </td>
             </tr>
        </table>


</asp:Content>

