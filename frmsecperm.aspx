<%@ Page Language="VB" MasterPageFile="general.master" AutoEventWireup="false" CodeFile="frmsecperm.aspx.vb" Inherits="frmsecperm"  %>
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
    <tr><td  style="text-align:center" colspan="1" ><span class="form-title">Permisos</span> |<asp:Label  runat="server" ID="lblsubtitle" CssClass="form-subtitle" /></td></tr>
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
            <asp:Button  runat="server" ID="cmdSidReorganize" Text="Reorganizar" CssClass="boton-acciones" CausesValidation="False" OnClick="cmdSidReorganize_Click" />
        </td>
    </tr>
    </table>
    <table style="width:100%;">
          <tr>
        <td  class="form-subtitle">Permisos efectivos por usuario:
        </td>
        </tr>
        <tr>
        <td>
            <asp:GridView ID="grdProPerm" runat="server" AutoGenerateColumns="False" 
                 Width="100%"   PageSize="50"
                >
           <RowStyle CssClass="pryceldaotro"  />
           <HeaderStyle CssClass="pryceldatitulos" />                            
           <FooterStyle CssClass ="pryceldaotro"  />
           <Columns>
                   <asp:TemplateField HeaderText="Tipo:" SortExpression="sidtypecod" ItemStyle-Width="50px" ItemStyle-HorizontalAlign="Center"  >
                       <ItemTemplate>
                       <asp:Image ID="imgUser" runat="server"  ImageUrl="~/imagenes/objuser.png" Width="24px" ToolTip='<%# "[" & Eval("seccod").ToString  & "]-" & Eval("secdsc") %>' />
                       </ItemTemplate>
                   </asp:TemplateField>
                <asp:BoundField DataField="secdsc" HeaderText="Usuario:" ItemStyle-Width="250px" SortExpression="secdsc"   />                
                <asp:BoundField DataField="acctypedsc" HeaderText="Permiso:" SortExpression="acctypedsc" />
            </Columns>
            </asp:GridView>
        </td>
     </tr>
          <tr style="height:20px" >
          <td  ></td>
          </tr>
          <tr>
        <td class="form-subtitle" >Lista de permisos:
        </td>
        </tr>
        <tr>
        <td>
            <asp:GridView ID="grpACLperm" runat="server" AutoGenerateColumns="False" 
                 Width="100%"   PageSize="50"  ShowFooter="true" 
                >
           <RowStyle CssClass="pryceldaotro"  />
           <HeaderStyle CssClass="pryceldatitulos" />                            
           <FooterStyle CssClass ="pryceldaotro"  />
           <Columns>
           <asp:TemplateField HeaderText="Tipo:" SortExpression="sidtypecod" ItemStyle-Width="50px" ItemStyle-HorizontalAlign="Center"  >
           <ItemTemplate>
           <asp:Image runat="server"  ImageUrl='<%# if(Eval("sidtypecod") =-2,"~/imagenes/objuser.png","~/imagenes/objgroup.png") %>' Width="24px"
                ToolTip='<%#  "[" & Eval("sidcod").ToString  & "]-" & Eval("siddsc")  %>'
            />
           </ItemTemplate>
           <ItemStyle HorizontalAlign="Center" Width="50px"></ItemStyle>
           </asp:TemplateField>
                <asp:BoundField DataField="acctypedsc" HeaderText="Permiso:" 
                   SortExpression="acctypedsc" ItemStyle-Width="200px" >
            <ItemStyle Width="150px"></ItemStyle>
               </asp:BoundField>
                <asp:BoundField DataField="siddsc" HeaderText="Usuario/grupo"  
                   SortExpression="siddsc"   />
               <asp:TemplateField HeaderText="Acción:">
                    <ItemTemplate>
                        <asp:ImageButton  ID="imgd" runat="server" CausesValidation="False"  
                                CommandName="cDelete"  CommandArgument='<%# Eval("acecod") %>' ImageUrl="./imagenes/actdel.png" Text="Eliminar" Width="16" OnClientClick=" return confirm('Confirma la eliminacion?');" />                         
                      </ItemTemplate>                              
                   <ItemStyle Width="80px" />
                     <FooterTemplate >
                     <asp:Button runat="server" Text="Nuevo permiso"  ID="cmdInsert"                                   
                            CssClass="boton-acciones"   CommandName="cInsert"  CausesValidation="False" />
                     </FooterTemplate>
               </asp:TemplateField>
            </Columns>
            </asp:GridView>
        </td>
     </tr>
    </table>
    
    <div>
    
<!-- Panel updpanelfrmsecperm -->
<asp:Label  runat="server" ID="lblpnlupdpanelfrmsecperm" width="100%" />
<ajaxkit:ModalPopupExtender  runat="server" ID="updpanelfrmsecperm" DropShadow="True" EnableViewState="True" PopupControlID="pnlupdpanelfrmsecperm" TargetControlID="lblpnlupdpanelfrmsecperm" BackgroundCssClass="bgmodalpopup" />
<asp:Panel  ID="pnlupdpanelfrmsecperm" runat="server" BorderWidth="1" BorderStyle="solid" style="display:none;padding:0px;"  >
<asp:UpdatePanel  ID="updupdpanelfrmsecperm" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="True"  >
<Triggers>
    <asp:PostBackTrigger ControlID="frmupdpanelfrmsecperm$cmdfrmupdpanelsecprmupdconfirm" />
</Triggers>
<ContentTemplate>
<table class="form" cellpadding="0" cellspacing="0"   ><tr><td  colspan="1" ><tr><td  colspan="1" >
<asp:Label  runat="server" ID="lblfrmupdpanelsecprmsubtitle" CssClass="form-subtitle" />
</td></tr>
<tr><td  colspan="1" >
<asp:Label  runat="server" ID="lblfrmupdpanelsecprmerror" CssClass="error" width="100%" />
</td></tr>
<tr><td  colspan="1" ><asp:ValidationSummary  runat="server" ID="frmupdpanelfrmsecpermvalsumary" />
<asp:FormView  ID="frmupdpanelfrmsecperm" runat="server" DataKeyNames="cod" DefaultMode="Insert"  >
<InsertItemTemplate >
<!-- Init Plantilla edicion -->
<table width="100%" cellpadding="0" cellspacing="0"><tr>
<td  class="search-title" >
<asp:Button  runat="server" ID="cmdfrmupdpanelsecprmupdcancel" Text="Cancelar" CssClass="boton-acciones" CausesValidation="False" OnClick="cmdfrmupdpanelsecprmupdcancel_Click" />
<asp:Button  runat="server" ID="cmdfrmupdpanelsecprmupdconfirm" Text="Insertar" 
        CssClass="boton-acciones" CausesValidation="False" 
        onclick="cmdfrmupdpanelsecprmupdconfirm_Click"   />
</td>
</tr>
</table>
<table class="form" width="100%" cellpadding="1" cellspacing="1"><tr>
<td  colspan="1" >
Usuario/Grupo
</td>
<td  colspan="1" >
<asp:UpdatePanel  ID="updupdpnlsidcod" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="True" >
<Triggers>
  <asp:AsyncPostBackTrigger  ControlID="edtsidcoddelete" EventName="Click" />
</Triggers>
<ContentTemplate>
<asp:HiddenField  runat="server" ID="hdnsidcod" />
<asp:HiddenField  runat="server" ID="hdnsidcodtype" Value="-1"/>
<asp:Button  runat="server" ID="cmdsidcodshowpanel" Text="..." 
        CssClass="boton-acciones" CausesValidation="False" 
        OnClick="cmdedtsidcodshowpanel_Click" />
<asp:ImageButton runat="server" ID="edtsidcoddelete" ImageURL="~/imagenes/actdel.png" Width="16" Height="16" Tooltip="Quitar permiso" Visible="False" OnClick="edtsidcoddelete_Click" OnDataBinding="edtsidcoddelete_DataBinding" CausesValidation="False" />
<asp:LinkButton runat="server" ID="edtsidcodview"  CssClass="boton-acciones-subbutton" OnDataBinding="edtsidcodview_DataBinding" CausesValidation="False" />
</ContentTemplate>
</asp:UpdatePanel>
</td>
</tr>
<tr>
<td  colspan="1" >
Permiso
</td>
<td  colspan="1" >
<asp:UpdatePanel  ID="updupdpnlupdedtacctypefs" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="True" >
<Triggers>
  <asp:AsyncPostBackTrigger  ControlID="edtacctypedelete" EventName="Click" />
</Triggers>
<ContentTemplate>
<asp:UpdatePanel  ID="updupdedtacctypefs" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="True" >
<ContentTemplate>
<asp:HiddenField  runat="server" ID="edtacctype" />
<asp:HiddenField  runat="server" ID="edtacctypetype" Value="-1"/>
<asp:Button  runat="server" ID="cmdedtacctypeshowpanel" Text="..." CssClass="boton-acciones" CausesValidation="False" OnClick="cmdedtacctypeshowpanel_Click" />
<asp:ImageButton runat="server" ID="edtacctypedelete" ImageURL="~/imagenes/actdel.png" Width="16" Height="16" Tooltip="Quitar Revisión anterior" Visible="False" OnClick="edtacctypedelete_Click" OnDataBinding="edtacctypedelete_DataBinding" CausesValidation="False" />
<asp:LinkButton runat="server" ID="edtacctypeview" CssClass="boton-acciones-subbutton" OnDataBinding="edtacctypeview_DataBinding" CausesValidation="False" />
</ContentTemplate>
</asp:UpdatePanel>
</ContentTemplate>
</asp:UpdatePanel>

</td>
</tr>
</table>
<!-- End Plantilla edicion -->
</InsertItemTemplate>

</asp:FormView>

</td></tr><asp:UpdateProgress ID="UpdateProgress8"  runat="server" AssociatedUpdatePanelID="updupdpanelfrmsecperm" >
<ProgressTemplate><span  class="ajaxupd" >Actualizando...</span></ProgressTemplate>
</asp:UpdateProgress>
</td></tr></table></ContentTemplate>
</asp:UpdatePanel>
</asp:Panel>
<!-- Fin panel updpanelfrmsecperm -->
    </div>
    <div  >
<!-- Panel pnlobjectexplorer -->
<asp:Label  runat="server" ID="lblpnlpnlobjectexplorer" width="100%" />
<ajaxkit:ModalPopupExtender  runat="server" ID="pnlobjectexplorer" DropShadow="True" EnableViewState="True" PopupControlID="pnlpnlobjectexplorer" TargetControlID="lblpnlpnlobjectexplorer" BackgroundCssClass="bgmodalpopup" />
<asp:Panel  ID="pnlpnlobjectexplorer" runat="server" BorderWidth="1" BorderStyle="solid" style="display:none;padding:0px;" >
<asp:UpdatePanel  ID="updpnlobjectexplorer" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="True" >
<Triggers>
  <asp:AsyncPostBackTrigger  ControlID="tvwmodalpopuppnlobjectexplorer" EventName="SelectedNodeChanged" />
  <asp:AsyncPostBackTrigger  ControlID="grdobjectexplorer" EventName="RowCommand" />
  
</Triggers>
<ContentTemplate>
<table class="form" cellpadding="0" cellspacing="0"><tr><td  colspan="1" >
<table style="width:700px;height:300px; border: 1px double #000000; padding: 5px; margin: 3px; background-color: #F0F0F0; vertical-align: top; text-align: left;">
<tr><td  class="tabla-titulo" colspan="2" >Seleccionar</td></tr>
<tr>
<td  class="search-title" style="height:15px;width:700px" colspan="2" >
<asp:Button  runat="server" ID="cmdobjectexplorercancel" Text="Cancelar" CssClass="boton-acciones" CausesValidation="False" OnClick="cmdobjectexplorercancel_Click" />
<asp:Label  runat="server" ID="lblobjectexplorerfilter" width="100" Text='Filtro rápido:' />
<asp:TextBox  runat="server" ID="txtobjectexplorerfilter" CssClass="form-control" />
<asp:CompareValidator  runat="server" ID="vcdvaltxtobjectexplorerfilter" SetFocusOnError="true" CssClass="error" ControltoValidate="txtobjectexplorerfilter" Display="Dynamic" Text="Ingrese un texto" ErrorMessage=":Ingrese un texto" Type="String" Operator="DataTypeCheck" />


<asp:Button  runat="server" ID="cmdobjectexplorerfilter" Text="Buscar" CssClass="boton-acciones" CausesValidation="False" OnClick="cmdobjectexplorerfilter_Click" />
</td>
</tr>
<tr>
<td  style="vertical-align:top; text-align:left;width:200px;height:285px;background-color:#F0F0F0;" colspan="1" >
<asp:TreeView  runat="server" ID="tvwmodalpopuppnlobjectexplorer" ShowLines="True" AutoGenerateDataBindings="False" Width="200" ForeColor="Black" SelectedNodeStyle-BackColor="#C0C0C0" OnSelectedNodeChanged="tvwmodalpopuppnlobjectexplorer_SelectedNodeChanged" />
</td>
<td  style="vertical-align:top; text-align:left;width:500px;height:285px;" colspan="1" >
<asp:GridView  runat="server" ID="grdobjectexplorer" AllowPaging="True" AllowSorting="True" AutoGenerateColumns="False" PageSize="10"
 GridLines="None" ShowFooter="True" Width="100%" UseAccessibleHeader="False" DataKeyNames="cod,objecttype" onRowCommand="grdobjectexplorer_RowCommand" OnSelectedIndexChanged="grdobjectexplorer_SelectedIndexChanged" OnPageIndexChanging="grdobjectexplorer_PageIndexChanging">
<FooterStyle  CssClass="tabla-footer" />
<RowStyle  CssClass="tabla-fila" />
<AlternatingRowStyle  CssClass="tabla-fila-alternativa" />
<SelectedRowStyle  CssClass="tabla-fila" />
<PagerStyle  CssClass="tabla-pager" />
<HeaderStyle  CssClass="tabla-titulo" ForeColor="White" />
<EmptyDataTemplate>
<p  class="tabla-fila"></p>
</EmptyDataTemplate>
<Columns>
<asp:TemplateField   AccessibleHeaderText="Seleccionar" HeaderText="Seleccionar" ItemStyle-CssClass="tabla-celda">
<ItemStyle  HorizontalAlign="Left" />
<ItemTemplate>
<asp:Button  runat="server" ID="grdobjectexplorerselectrow" Text="Seleccionar" CssClass="boton-acciones" CommandName="Select" CausesValidation="False" /></ItemTemplate>
</asp:TemplateField>
<asp:TemplateField   ItemStyle-CssClass="tabla-celda">
<ItemStyle  HorizontalAlign="Left" />
<ItemTemplate>
<asp:Image  runat="server" ID="grdobjectexplorerrowimageimg" Width="20" BorderColor="LightGray" BorderWidth="2" BorderStyle="Solid" GenerateEmptyAlternateText="True" ImageURL='<%# "~/imagenes/icon" & format(Eval("objecttype"),"00000000") & ".png" %>' AlternateText="Imagen no disp." />
</ItemTemplate>
</asp:TemplateField>
<asp:TemplateField   ItemStyle-CssClass="tabla-celda">
<ItemStyle  HorizontalAlign="Left" />
<ItemTemplate>
<asp:LinkButton runat="server" ID="grdobjectexplorerrowlabel" Text='<%# Replace(Eval("dsc").ToString,Chr(10),"<br />") %>' CssClass="boton-acciones" CommandName="Select" CausesValidation="False" /></ItemTemplate>
</asp:TemplateField>
</Columns>
</asp:GridView>

</td>
</tr>
</table>
<asp:UpdateProgress ID="UpdateProgress1"  runat="server" AssociatedUpdatePanelID="updpnlobjectexplorer" >
<ProgressTemplate><span  class="ajaxupd" >Actualizando...</span></ProgressTemplate>
</asp:UpdateProgress>
</td></tr></table></ContentTemplate>
</asp:UpdatePanel>
</asp:Panel>
<!-- Fin panel pnlobjectexplorer -->
</div>
</asp:Content>

