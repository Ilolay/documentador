<%@ Page Language="VB" CodeFile="cfrmReportes.aspx.vb" MasterPageFile="general.master" AutoEventWireup="True" Inherits="cfrmReportes"   %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="ajaxkit" %>
<%@ Register assembly="System.Web.DataVisualization, Version=3.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" namespace="System.Web.UI.DataVisualization.Charting" tagprefix="asp" %>
<asp:Content id="cHeader" ContentPlaceHolderID="conHeader" runat="Server">

<script language="javascript" type="text/javascript" src="hrcWinPopup.js"  ></script>
<script language="javascript" type="text/javascript" src="DataType_Validate.js"  ></script>
<style type="text/css">
#dek {POSITION:absolute;VISIBILITY:hidden;Z-INDEX:200;}
</style>

<link rel="stylesheet" type="text/css" media="screen" href="plugins/jquery/themes/redmond/jquery-ui-1.8.2.custom.css" />
<link rel="stylesheet" type="text/css" media="screen" href="plugins/jquery/themes/ui.jqgrid.css" />

<script src="plugins/jquery/jquery.layout.js" type="text/javascript"></script>
<script src="plugins/jquery/i18n/grid.locale-sp.js" type="text/javascript"></script>
<script src="plugins/jquery/ui.multiselect.js" type="text/javascript"></script>
<script src="plugins/jquery/jquery.jqGrid.min.js" type="text/javascript"></script>
<script src="plugins/jquery/jquery.tablednd.js" type="text/javascript"></script>
<script src="plugins/jquery/jquery.contextmenu.js" type="text/javascript"></script>

<link href="plugins/jquery/autosuggest/autoSuggest.css" rel="stylesheet" type="text/css" />    
<script src="plugins/jquery/autosuggest/jquery.autoSuggest.js" type="text/javascript"></script>
<script src="plugins/jquery/simplemodal/jquery.simplemodal-1.4.1.js" type="text/javascript"></script>

</asp:Content>
<asp:Content ID="ContentDatos" runat="server" contentplaceholderid="ContentDatos">
    <asp:GridView  runat="server" ID="grdobjectexplorer" AllowPaging="True" 
            AllowSorting="True" AutoGenerateColumns="False" PageSize="10"
     GridLines="None" ShowFooter="False" ShowHeader="false" Width="100%" 
            UseAccessibleHeader="False" DataKeyNames="cod,objecttype" 
            onRowCommand="grdobjectexplorer_RowCommand" 
            OnSelectedIndexChanged="grdobjectexplorer_SelectedIndexChanged"  >
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
    <asp:TemplateField   AccessibleHeaderText="Seleccionar" HeaderText="Seleccionar">
    <ItemStyle  HorizontalAlign="Left" />
    <ItemTemplate>
    <asp:Button  runat="server" ID="grdobjectexplorerselectrow" Text="Seleccionar" 
            CssClass="boton-acciones" CommandName="Select" CausesValidation="False" /></ItemTemplate>
    </asp:TemplateField>
    <asp:TemplateField  >
    <ItemStyle  HorizontalAlign="Left" />
    <ItemTemplate>
    <asp:Image  runat="server" ID="grdobjectexplorerrowimageimg" Width="20" 
            BorderColor="LightGray" BorderWidth="2" BorderStyle="Solid" 
            GenerateEmptyAlternateText="True" 
            ImageURL='<%# "~/imagenes/icon" & format(Eval("objecttype"),"00000000") & ".png" %>' 
            AlternateText="Imagen no disp." />
    </ItemTemplate>
    </asp:TemplateField>
    <asp:TemplateField   SortExpression="dsc">
    <ItemStyle  HorizontalAlign="Left" />
    <ItemTemplate>
    <asp:LinkButton  runat="server" ID="grdobjectexplorerrowlabel" 
            CssClass="control-read" width="100%" Text='<%# Eval("dsc").ToString %>' 
            CommandName="Select" /></ItemTemplate>
    </asp:TemplateField>
    </Columns>
    </asp:GridView>
    
    <asp:ScriptManager  ID="ScriptManagerHrc" runat="server" EnableScriptLocalization="True" EnableScriptGlobalization="True" ScriptMode="Release" >
    </asp:ScriptManager>
<table width="100%" cellpadding="0" cellspacing="0">
<tr>
<td>
    <table width="100%" cellpadding="0" cellspacing="0" runat="server" id="pnlTitle" >
        <tr><td  style="width:32px;" colspan="1" ><img id="imgicon" runat="server"  src="imagenes/objchart.png" width="32" height="32" hspace="4"/>
        </td>
        <td  style="text-align:left;" colspan="1" ><span class="form-title">
             <asp:Label runat="server" ID="lblTitle" text="Reportes" ></asp:Label>
        </span>
    </td></tr>
    </table>
</td>
</tr>
    <tr>
    <td  colspan="1" >
    <asp:UpdatePanel  ID="updupdpnlSearch" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="True" >
    <Triggers>
      <asp:AsyncPostBackTrigger  ControlID="cmdLimpiar" EventName="Click" />      
      <asp:AsyncPostBackTrigger  ControlID="cmbView" EventName="SelectedIndexChanged" />
    </Triggers>
    <ContentTemplate>
    <!-- Init Filtros de búsqueda -->
    <table id="pnlSearch" runat="server" class="hide_print" style="background-color:#F0F0F0;" width="100%" cellpadding="0" cellspacing="0">
        <tr>
            <td class="search-title" colspan="4">Filtros de búsqueda</td></tr>
	     <tr id="row_001" runat="server">
	        <td class="search-item-title" style="vertical-align:top; width:160px;">
	            Vista:
	        </td>
	        <td style="vertical-align:top;" >
                <asp:DropDownList  runat="server" ID="cmbView" CssClass="form-control" OnSelectedIndexChanged="cmbView_SelectedIndexChanged"
                    Enabled="True"  AutoPostBack="true" />            
            </td>
            <td class="search-item-title" style="vertical-align:top; width:160px;">
                
                <asp:Label ID="lblAgrupacion" runat="server" Text="Agrupación:"></asp:Label>
            </td>
            <td>
            
                <asp:DropDownList runat="server" ID="cmbAgrupacion" CssClass="form-control" 
                                            AppendDataBoundItems="False" Enabled="True"></asp:DropDownList>
            </td>
	    </tr>
	    <tr id="row_002" runat="server">
	        <td class="search-item-title" style="vertical-align:top; width:160px;">
	            <asp:Label ID="lblTipoDocumento" runat="server" Text="Tipo de documento:"></asp:Label>
	        </td>
	        <td class="search-table" style="vertical-align:top; width:310px;" >
               <div runat="server" id="lstTipoDocumento" style="width:280px;" ></div>                             
            </td>
            <td class="search-item-title" style="vertical-align:top; width:160px;">
                  <asp:Label ID="lblUnidadDocumento" runat="server" Text="Unidad de documento:"></asp:Label>           
            </td>
	        <td style="vertical-align:top;" >
	             <div runat="server" id="lstUnidadDocumento" style="width:280px;" ></div>
            </td>
        </tr>
        <tr id="row_003" runat="server">
	        <td class="search-item-title" style="vertical-align:top; width:160px;">
	            <asp:Label ID="lblProceso" runat="server" Text="Proceso:"></asp:Label>
	        </td>
	        <td class="search-table" style="vertical-align:top; width:310px;" >
               <div runat="server" id="lstProceso" style="width:280px;" ></div>                             
            </td>
            <td class="search-item-title" style="vertical-align:top; width:160px;">
                  <asp:Label ID="lblColaborador_UnidadColaborador" runat="server" Text="Colaborador/Unidad de colaborador:"></asp:Label>          
            </td>
	        <td style="vertical-align:top;" >
	             <div runat="server" id="lstColaborador_UnidadColaborador" style="width:280px;" ></div> 
            </td>
        </tr>
        <tr id="row_004" runat="server">
	        <td class="search-item-title" style="vertical-align:top; width:160px;">
	             <asp:Label ID="lblTituloDocumento" runat="server" Text="Titulo del documento:"></asp:Label>  
	        </td>
	        <td class="search-table" style="vertical-align:top; width:310px;" >
	             <asp:TextBox runat="server" ID="txtTituloDocumento" CssClass="form-control" MaxLength="30" Width="150px"/>            
            </td>
            <td class="search-item-title" style="vertical-align:top; width:160px;">
                  <asp:Label ID="lblEstado" runat="server" Text="Estado:"></asp:Label>          
            </td>
	        <td style="vertical-align:top;" >
	             <div runat="server" id="lstEstado" style="width:280px;" viewstate=false></div> 
            </td>
        </tr>
	    <tr id="rowfechas" visible="false">
            <td class="search-item-title" style="vertical-align:top; width:160px;">
                <asp:Label ID="lblFechaDesde" runat="server" Text="Desde (dd/mm/aaaa):" /> 
            </td>
	         <td style="vertical-align:top;width:300px;" >
	            <asp:TextBox runat="server" ID="txtFechaDesde" CssClass="form-control" MaxLength="10" Width="100px"/>            
                 <asp:ImageButton runat="server" ID="imgcaltxtFechaDesde" ImageURL="imagenes/objcalender.png" Width="16" CausesValidation="False" />
                 <ajaxkit:CalendarExtender runat="server" ID="Caltxtfechadesde" TargetControlID="txtfechadesde" PopupButtonID="imgcaltxtfechadesde" Format="dd/MM/yyyy" />
                 <asp:CompareValidator  runat="server" ID="Comtxtfechadesde" SetFocusOnError="true" CssClass="error" ControltoValidate="txtfechadesde" Display="Dynamic" 
                        Text="Error: Fecha Desde inválida" ErrorMessage="Error: Fecha Desde inválida" Type="Date" Operator="DataTypeCheck" /> 
             </td>      
             <td class="search-item-title" style="vertical-align:top; width:160px;">
                <asp:Label ID="lblFechaHasta" runat="server" Text="Hasta (dd/mm/aaaa):" /> 
             </td>
	         <td style="vertical-align:top; width:300px;" >
	         <asp:TextBox runat="server" ID="txtFechaHasta" CssClass="form-control" MaxLength="10" Width="100px"/>            
                 <asp:ImageButton runat="server" ID="imgcaltxtFechaHasta" ImageURL="imagenes/objcalender.png" Width="16" CausesValidation="False" />
                 <ajaxkit:CalendarExtender runat="server" ID="CalendarExtender1" TargetControlID="txtfechahasta" PopupButtonID="imgcaltxtfechahasta" Format="dd/MM/yyyy" />
                 <asp:CompareValidator  runat="server" ID="Comtxtfechahasta" SetFocusOnError="true" CssClass="error" ControltoValidate="txtfechahasta" Display="Dynamic" 
                      Text="Error: Fecha Hasta inválida" ErrorMessage="Error: Fecha Hasta inválida" Type="Date" Operator="DataTypeCheck" />  
             </td>
        </tr>
        <tr visible=false >
             <td style="vertical-align:top; width:100px;">Indicador:</td>
             <td>
                <asp:DropDownList  runat="server" ID="cmbIndicador" CssClass="form-control" />                
             </td>
             <td style="vertical-align:top; width:100px;">Agrupación:</td>
             <td>
		        <asp:DropDownList runat="server" ID="cmbGroup" CssClass="form-control"  
		            AppendDataBoundItems="False" Enabled="False">
                    <asp:ListItem Value="1">Por día</asp:ListItem>
                    <asp:ListItem Selected="True" Value="2">Por Mes</asp:ListItem>
                </asp:DropDownList>
		    </td>
        </tr>		
    </table>
    <!-- End Filtros de búsqueda -->
    </ContentTemplate>
    </asp:UpdatePanel>  

    </td>
    </tr>
<tr>
<td  colspan="1" >
	<asp:UpdatePanel  ID="updupdpnlActions" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="True" >
        <Triggers>
          <asp:AsyncPostBackTrigger  ControlID="cmdRefrescar" EventName="Click" />  
          <asp:PostBackTrigger ControlID ="cmdDownloadXLS" />
        </Triggers>
    <ContentTemplate>
<!-- Init -->
        <table id="pnlActions" runat="server"  class="hide_print" style="background-color:#F0F0F0;" width="100%" cellpadding="0" cellspacing="0" >
        <tr>
        <td  class="search-title" >
			        <asp:Button  runat="server" ID="cmdBuscar" Text="Buscar" 
                        CssClass="boton-acciones" CausesValidation="True" EnableViewState="False" 
                        ValidationGroup="vg" />
			        <asp:Button  runat="server" ID="cmdLimpiar" Text="Limpiar filtros" CssClass="boton-acciones" CausesValidation="True" EnableViewState="False" OnClick="cmdLimpiar_Click" ValidationGroup="vg" />
			        <asp:Button  runat="server" ID="cmdRefrescar" Text="Refrescar" CssClass="boton-acciones" CausesValidation="True" EnableViewState="False" ValidationGroup="vg" />
                    <asp:Button  runat="server" ID="cmdReportePDF"  Text="Reporte PDF" CssClass="boton-acciones" CausesValidation="True" EnableViewState="False" OnClick="cmdReportePDF_Click" />
                    <asp:Button  runat="server" ID="cmdDownloadXLS" Text="Reporte XLS" CssClass="boton-acciones" CausesValidation="False"   />
                   
		        </td></tr>
        </table>
        <!-- End -->
    </ContentTemplate>
    </asp:UpdatePanel>
</td>
</tr>

<tr>
<td>
        <asp:UpdatePanel ID="updupdpnlResults" runat="server" UpdateMode="Conditional"  >
        <Triggers>
        <asp:PostBackTrigger ControlID ="cmdReportePDF"  />
        </Triggers>
            <ContentTemplate>

                <asp:Label  runat="server" ID="lblerror" CssClass="error" 
                                width="100%" EnableViewState="False" />

                <asp:Label  runat="server" ID="lblReportSubTitle" CssClass="index-novedades"
                                width="100%" EnableViewState="False" Text="" />                                                  
                <asp:Literal runat="server" ID="lblGrid" Mode="PassThrough"></asp:Literal>
                <asp:placeHolder runat="server" ID="drwGraphics" />
                <div runat="server" ID="drwGraphics2" />              
     </ContentTemplate>
    </asp:UpdatePanel>
    </td>                
</tr>
</table>
    <!-- Panel pnlobjectexplorer -->
<asp:Label  runat="server" ID="lblpnlpnlobjectexplorer" width="100%" />
<ajaxkit:ModalPopupExtender  runat="server" ID="pnlobjectexplorer"  EnableViewState="True" PopupControlID="pnlpnlobjectexplorer" TargetControlID="lblpnlpnlobjectexplorer" BackgroundCssClass="bgmodalpopup" />
<asp:Panel  ID="pnlpnlobjectexplorer" runat="server" BorderWidth="1" BorderStyle="solid" style="display:none;padding:0px;" >
<asp:UpdatePanel  ID="updpnlobjectexplorer" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="True" >
<Triggers>
  <asp:AsyncPostBackTrigger  ControlID="tvwmodalpopuppnlobjectexplorer" EventName="SelectedNodeChanged" />
  <asp:AsyncPostBackTrigger  ControlID="grdobjectexplorer" EventName="RowCommand" />
</Triggers>
<ContentTemplate>
<table class="form" cellpadding="0" cellspacing="0"><tr><td  colspan="1" >
    <table style="width:700px;height:300px; border: 2px solid #000000; padding: 2px; margin: 1px; background-color: #F0F0F0; vertical-align: top; text-align: left;">
    <tr><td  class="tabla-titulo" colspan="2" >Seleccionar</td></tr>
    <tr>
    <td  class="search-title" style="height:15px;width:700px" colspan="2" >
    <asp:Button  runat="server" ID="cmdobjectexplorercancel" Text="Cancelar" CssClass="boton-acciones" CausesValidation="False" OnClick="cmdobjectexplorercancel_Click" />
    <asp:Label  runat="server" ID="lblobjectexplorerfilter" width="100" Text='Filtro rápido:' />
    <asp:TextBox  runat="server" ID="txtobjectexplorerfilter" CssClass="form-control" Width="200" MaxLength="20" />
    <asp:CompareValidator  runat="server" ID="vcdvaltxtobjectexplorerfilter" SetFocusOnError="true" CssClass="error" ControltoValidate="txtobjectexplorerfilter" Display="Dynamic" Text="Ingrese un texto" ErrorMessage=":Ingrese un texto" Type="String" Operator="DataTypeCheck" />

    <asp:Button  runat="server" ID="cmdobjectexplorerfilter" Text="Buscar" CssClass="boton-acciones" CausesValidation="False" OnClick="cmdobjectexplorerfilter_Click" />
    </td>
    </tr>
    <tr>
    <td  style="vertical-align:top; text-align:left;width:200px;height:285px;background-color:#F0F0F0;" colspan="1" >
    <asp:TreeView  runat="server" ID="tvwmodalpopuppnlobjectexplorer" ShowLines="True" AutoGenerateDataBindings="False" Width="200" ForeColor="Black" SelectedNodeStyle-BackColor="#C0C0C0" OnSelectedNodeChanged="tvwmodalpopuppnlobjectexplorer_SelectedNodeChanged" />
    </td>
    <td  style="vertical-align:top; text-align:left;width:500px;height:285px;" colspan="1" >
        &nbsp;</td>
    </tr>
    </table>
<asp:UpdateProgress ID="UpdateProgress2"  runat="server" AssociatedUpdatePanelID="updpnlobjectexplorer" >
<ProgressTemplate><span  class="ajaxupd" >Actualizando...</span></ProgressTemplate>
</asp:UpdateProgress>
</td></tr></table></ContentTemplate>
</asp:UpdatePanel>
</asp:Panel>
<!-- Fin panel pnlobjectexplorer -->

	</asp:Content>

