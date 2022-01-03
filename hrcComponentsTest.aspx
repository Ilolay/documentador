<%@ Page Language="VB" MasterPageFile="general.master" AutoEventWireup="false" CodeFile="hrcComponentsTest.aspx.vb" Inherits="hrcComponentsTest" %>
<asp:Content ContentPlaceHolderID=conHeader runat=server ID ="cHeader" >

<link href="plugins/jquery/autosuggest/autoSuggest.css" rel="stylesheet" type="text/css" />    
<script src="plugins/jquery/autosuggest/jquery.autoSuggest.js" type="text/javascript"></script>
<script src="plugins/jquery/simplemodal/jquery.simplemodal-1.4.1.js" type="text/javascript"></script>

<link href="plugins/jquery/themes/redmond/jquery-ui-1.8.2.custom.css" rel="stylesheet" type="text/css" />    
<script src="plugins/jquery/datepicker/datePicker.js"  type="text/javascript" />    
<script src="plugins/jquery/datepicker/jquery.ui.core.js"  type="text/javascript" />    
<script src="plugins/jquery/datepicker/jquery.ui.widget.js"  type="text/javascript" />    

<script src="plugins/jquery/datepicker/jquery.ui.datepicker-es.js"  type="text/javascript" /> 



<script language="javascript" type="text/javascript" src="hrcWinPopup.js"  ></script>
<script language="javascript" type="text/javascript" src="DataType_Validate.js"  ></script>
<script language="javascript" type="text/javascript" src="hrc/core.js"  ></script>


<link rel="stylesheet" type="text/css" media="screen" href="plugins/jquery/themes/redmond/jquery-ui-1.8.2.custom.css" />
<link rel="stylesheet" type="text/css" media="screen" href="plugins/jquery/themes/ui.jqgrid.css" />
<script src="plugins/jquery/jquery.layout.js" type="text/javascript"></script>
<script src="plugins/jquery/i18n/grid.locale-es.js" type="text/javascript"></script>
<script src="plugins/jquery/jquery-ui.core.js" type="text/javascript"></script>
<script src="plugins/jquery/jquery-ui.sortable.js" type="text/javascript"></script>
<script src="plugins/jquery/ui.multiselect.js" type="text/javascript"></script>
<script src="plugins/jquery/jquery.jqGrid.min.js" type="text/javascript"></script>
<script src="plugins/jquery/jquery.tablednd.js" type="text/javascript"></script>
<script src="plugins/jquery/jquery.contextmenu.js" type="text/javascript"></script> 


</asp:Content>
<asp:Content id="cDatos" ContentPlaceHolderID="ContentDatos" runat="Server">
<table width="100%" cellpadding="0" cellspacing="0">
<tr><td  colspan="1" >
<table width="100%" cellpadding="0" cellspacing="0">
<tr><td  style="width:32px;" colspan="1" ><img  src="imagenes/iconconfiguracion.png" width="32px" height="32px"  hspace="4"/></td>
<td  style="text-align:left;" colspan="1" ><span class="form-title">Test de componentes</span></td></tr>
</table>
</td></tr>
<tr>
<td  class="form-tabla-control" colspan="2" >
    <div  runat="server" ID="lblerror" class="error" />
</td>
</tr>
<tr>
<td colspan="2">
<table id="Search" width="100%" cellpadding="0" cellspacing="0">
<tr>
<td  class="tabla-fila" >Botones</td>
<td  class="tabla-fila" >
Normal
<div runat="server" id="lstButtonSync" ></div>
Asincrono
<div runat="server" id="lstButtonAsync" ></div>
</td>
</tr>
<tr>
<td  class="tabla-fila" >Autosugeridos</td>
<td  class="tabla-fila" >
<div runat="server" id="lstInput" ></div>
<div runat="server" id="lstInput2" ></div>
<div runat="server" id="lstInput3" ></div>
<asp:Button  runat="server" ID="cmdDialog" Text="mensajes" CssClass="boton-acciones" CausesValidation="False" />     </td>
<td  class="tabla-fila" ></td>
</tr>
</table>
</td>
</tr>
</table>
</td>
</tr>

</table></asp:Content>


