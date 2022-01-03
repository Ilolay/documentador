<%@ Page Language="VB" MasterPageFile="general.master" AutoEventWireup="false" CodeFile="hrcMyConfig.aspx.vb" Inherits="hrcMyConfig"  %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="ajaxkit" %>
<asp:Content id="cDatos" ContentPlaceHolderID="ContentDatos" runat="Server">
    <asp:ScriptManager ID="ScriptManager1" runat="server" EnableScriptLocalization="True" EnableScriptGlobalization="True" ScriptMode="Release" >
    </asp:ScriptManager> 
<table width="100%" cellpadding="0" cellspacing="0">
<tr id="Row_title" runat="server" >
    <td  style="width:32px;" colspan="1" ><img  src="imagenes/objFastAccess_config_yes.png" width="32px" height="32px" alt="Mi configuración" hspace="4"/></td>
    <td  style="text-align:left;" colspan="1" ><span class="form-title">Mi configuración</span> |<asp:Label  runat="server" ID="lblsubtitle" CssClass="form-subtitle"  />
    </td>
</tr>
<tr>
<td colspan="2">
    <table id="Search" width="100%" cellpadding="0" cellspacing="0">
    <tr id="row_gral_title"  runat="server" >
    <td  class="tabla-titulo"  runat="server"  colspan="4" >Ayuda</td></tr>
    <tr id="row_gral" runat="server"  >
        <td class="search-item-title" style="width:100px"  >
            Nivel de ayuda
        </td>
        <td  class="form-tabla-control" >        
        <asp:RadioButtonList runat="server" ID="optHlpMode" >
        </asp:RadioButtonList>
        <asp:Label  runat="server" ID="lblerror" CssClass="error" width="100%"></asp:Label>
        </td>
        <td class="search-item-title"  style="vertical-align:top;width:120px" >Mensajes en pantalla            
            </td>
            <td  style="vertical-align:top;width:100px" >
                <asp:CheckBox runat="server" ID="chkAlertsONscreen" />
            </td>
    </tr>
    <tr id="row_gral_save" runat="server" >
        <td colspan="4" class="search-title" style="height:20px" >
            <asp:Button  runat="server" ID="cmdSave" Text="Guardar" CssClass="boton-acciones" CausesValidation="False" EnableViewState="False" />
        </td>
    </tr>
    <tr id="row_tools_title"  runat="server" >
        <td  class="tabla-titulo" colspan="4" >Herramientas</td>
    </tr>
  <tr id="row_tools"  runat="server" >
        <td  class="form-tabla-control" colspan="4" >    
         <asp:Button runat="server" Id="cmdDeleteAllPreferences" 
                OnClientClick="javascript:return confirm('Confirma la eliminación de todas sus preferencias de usuario (filtros en formulario,nivel de ayuda, etc)?')" 
                CssClass="boton-acciones" Text="Eliminar todas las preferencias" />
         <asp:Button runat="server" Id="cmdToggleIsAdmin" 
                CssClass="boton-acciones" Text="Habilita perfil de administrador" Visible=false />
        </td>     
    </tr>
    <tr id="row_delegations_title"  runat="server"  >
        <td  class="tabla-titulo" colspan="4"  >Delegaciones</td></tr>
    <tr id="row_delegations" runat="server"  >
        <td class="search-item-title" colspan="4" valign="top"   >
            <asp:MultiView runat="server" ID="fmeDelegations" ActiveViewIndex="1">
            <asp:View runat="server"  >
                          <asp:Label runat="server" ID="lbldeactivate" ></asp:Label>
                          <asp:Button runat="server"   ID="cmdDeactivate"                                   
                            CssClass="boton-acciones"  Text="Cerrar delegación"  OnClick="cmdDeactivate_Click"
                            OnClientClick='<%# "return confirm(""Confirma que desea dejar de utilizar el usuario de " & Replace(Ctype(Session("security"),clsHrcSecurityClient).CurrentSecDsc,"\","\\") & "?"");" %>'  
                            CausesValidation="false" />
            </asp:View>
            <asp:View runat="server" >                        
            <table cellpadding="0" cellspacing ="0" width="100%" >           
            <tr>
            <td>
                <span class="obs-controles" >Los siguientes usuarios y grupos pueden actuar en su nombre. Si no especifica la fecha de fin, la delegación será permanente.</span>
            </td>
            </tr>
            <tr>
            <td>
                <asp:GridView ID="grdDelegations" runat="server" AutoGenerateColumns="False" 
                     Width="80%"   ShowFooter="true"  
                    >
               <RowStyle CssClass="pryceldaotro"  />
               <HeaderStyle CssClass="pryceldatitulos" />                            
               <FooterStyle CssClass ="pryceldaotro"  />
               <Columns>
               <asp:TemplateField HeaderText="Tipo:" SortExpression="sidtypecod" ItemStyle-Width="50px" ItemStyle-HorizontalAlign="Center"  >
                   <ItemTemplate>
                       <asp:Image ID="Image1" runat="server"  ImageUrl='<%# if(Eval("sidtypecod") =-2,"~/imagenes/objuser.png","~/imagenes/objgroup.png") %>' Width="24px"
                            ToolTip='<%#  "[" & Eval("sidcod").ToString  & "]-" & Eval("siddsc")  %>'
                        />
                   </ItemTemplate>
                   <ItemStyle HorizontalAlign="Center" Width="50px"></ItemStyle>
               </asp:TemplateField>
                    <asp:BoundField DataField="siddsc" HeaderText="Usuario/grupo"  
                       SortExpression="siddsc"    />
                    <asp:BoundField DataField="deldatetimestart" HeaderText="Desde"  
                       SortExpression="siddsc" DataFormatString="{0:d}" ItemStyle-Width="120px" ItemStyle-HorizontalAlign="Center"  />
                    <asp:BoundField DataField="deldatetimeend" HeaderText="Hasta"  
                       SortExpression="siddsc"  DataFormatString="{0:d}" ItemStyle-Width="120px"   ItemStyle-HorizontalAlign="Center"  />
                    <asp:TemplateField HeaderText="" ItemStyle-Width="50px">
                        <ItemTemplate>
                            <asp:Label  ID="lblpermanente" runat="server" Text='<%# if(IsDBNull(Eval("deldatetimeend")),"<b>Permanente</b>","") %>' ></asp:Label>
                          </ItemTemplate>                              
                   </asp:TemplateField>
                   <asp:TemplateField HeaderText="">
                        <ItemTemplate>
                            <asp:ImageButton  ID="imgd" runat="server" CausesValidation="False"  
                                    CommandName="CMDDELDELETE"  CommandArgument='<%# Eval("delid") %>' ImageUrl="./imagenes/actdel.png" Text="Eliminar" Width="16" OnClientClick=" return confirm('Confirma la eliminacion?');" />                         
                          </ItemTemplate>                              
                       <ItemStyle Width="80px" />
                       <FooterStyle  Width="80px" />
                       <FooterTemplate >
                         <asp:Button runat="server" Text="Delegar"  ID="cmdInsert"                                   
                                CssClass="boton-acciones"   CommandName="CMDDELINSERT"  CausesValidation="False" />
                       </FooterTemplate>
                   </asp:TemplateField>
                </Columns>
                </asp:GridView>
            </td>
            </tr>          
            </table>
            </asp:View>
            </asp:MultiView>          
        </td>
    </tr>
    </table>
</td>
</tr>
</table>
<div>
    
<!-- Panel updpanelfrmsecperm -->
<asp:Label  runat="server" ID="lblpnlupdpanelfrmsecperm" width="100%" />
<ajaxkit:ModalPopupExtender  runat="server" ID="updpanelfrmsecperm" DropShadow="True" EnableViewState="True" PopupControlID="pnlupdpanelfrmsecperm" TargetControlID="lblpnlupdpanelfrmsecperm" BackgroundCssClass="bgmodalpopup" />
<asp:Panel  ID="pnlupdpanelfrmsecperm" runat="server" BorderWidth="1" BorderStyle="solid" style="display:none;padding:0px; width:500px"  >
<asp:UpdatePanel  ID="updupdpanelfrmsecperm" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="True"  >
<Triggers>
    <asp:PostBackTrigger ControlID="frmupdpanelfrmsecperm$cmdfrmupdpanelsecprmupdconfirm" />
</Triggers>
<ContentTemplate>
<table class="form" cellpadding="0" cellspacing="0"  width="100%"  ><tr><td  colspan="1" ><tr><td  colspan="1" >
<asp:Label  runat="server" ID="lblfrmupdpanelsecprmsubtitle" CssClass="form-subtitle" />
</td></tr>
<tr><td  colspan="1" >
<asp:Label  runat="server" ID="lblfrmupdpanelsecprmerror" CssClass="error" width="100%" />
</td></tr>
<tr><td  colspan="1" ><asp:ValidationSummary  runat="server" ID="frmupdpanelfrmsecpermvalsumary" />
<asp:FormView  ID="frmupdpanelfrmsecperm" runat="server" DataKeyNames="cod" DefaultMode="Insert" Width="100%"  >
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
Delegar a
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
    <asp:ImageButton runat="server" ID="edtsidcoddelete" ImageURL="~/imagenes/actdel.png" Width="16" Height="16" Tooltip="Quitar" Visible="False" OnClick="edtsidcoddelete_Click" CausesValidation="False" />
<asp:LinkButton runat="server" ID="edtsidcodview"  CssClass="boton-acciones-subbutton" CausesValidation="False" />
</ContentTemplate>
</asp:UpdatePanel>
</td>
</tr>
<tr>
<td  colspan="1" >
Fecha desde:
</td>
    <td class="pryceldaotro" style="text-align:left ;">                    
              <asp:Label ID="lblDelDateTimeFrom" runat="server" />
              <asp:TextBox ID="txtDelDateTimeFrom" runat="server" MaxLength="15"  CssClass="form-control" Width="100px" ></asp:TextBox>
              <ajaxkit:CalendarExtender runat="server" ID="calDelDateTimeFrom" PopupButtonID="cmdDelDateTimeFrom" TargetControlID="txtDelDateTimeFrom" Format="d/M/yyyy"  OnClientShown="calendarShown" />        
              <asp:ImageButton runat="server" ID="cmdDelDateTimeFrom" ImageURL="imagenes/objcalender.png" Width="16" CausesValidation="False" />                                                         
              <span class="obs-controles">Fecha desde cuándo se delegará</span>  
    </td>
</tr>
<tr>
<td  colspan="1" >
Fecha hasta:
</td>
    <td class="pryceldaotro" style="text-align:left ;">                    
              <asp:Label ID="lblDelDateTimeTo" runat="server" />
              <asp:TextBox ID="txtDelDateTimeTo" runat="server" MaxLength="15"  CssClass="form-control" Width="100px" ></asp:TextBox>
              <ajaxkit:CalendarExtender runat="server" ID="calDelDateTimeTo" PopupButtonID="cmdDelDateTimeTo" TargetControlID="txtDelDateTimeTo" Format="d/M/yyyy"  OnClientShown="calendarShown" />        
              <asp:ImageButton runat="server" ID="cmdDelDateTimeTo" ImageURL="imagenes/objcalender.png" Width="16" CausesValidation="False" />                                                         
        <span class="obs-controles">Si no especifica esta fecha, será permanente</span>              
    </td>
</tr>
<tr>
<td colspan="2">
    Recuerde que estará delegando todas sus funciones durante el lapso de tiempo.
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
 GridLines="None" ShowFooter="True" Width="100%" UseAccessibleHeader="False" DataKeyNames="cod,objecttype" onRowCommand="grdobjectexplorer_RowCommand" OnSelectedIndexChanged="grdobjectexplorer_SelectedIndexChanged" >
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


