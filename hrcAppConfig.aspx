<%@ Page Language="VB"  MasterPageFile="general.master" AutoEventWireup="false" CodeFile="hrcAppConfig.aspx.vb" Inherits="hrcAppConfig" %>
<asp:Content id="cDatos" ContentPlaceHolderID="ContentDatos" runat="Server">
<table width="100%" cellpadding="0" cellspacing="0">
    <tr>
        <td  colspan="1" >
        <table width="100%" cellpadding="0" cellspacing="0">
            <tr>
                <td  style="width:32px;" colspan="1" ><img  src="imagenes/iconconfiguracion.png" width="32px" height="32px" alt="Tipos de documentos" hspace="4"/></td>
                <td  style="text-align:left;" colspan="1" ><span class="form-title">Implementación</span></td>
            </tr>
        </table>
        </td>
    </tr>
    <tr>        <td colspan="2">            <table id="Search" width="100%" cellpadding="0" cellspacing="0">
            <tr>
                <td  class="search-title" colspan="3" >Importaciones y configuraciones</td>
            </tr>
            <tr>
                <td  class="form-tabla-control" colspan="2" >
                <asp:Label  runat="server" ID="lblerror" CssClass="error" width="100%"></asp:Label>
                </td>
            </tr>
            <tr>
                <td  class="tabla-fila" >Imágen vacía<br /><asp:FileUpload ID="fleimageempty" runat="server" CssClass="boton-acciones"  /></td>
                <td class="tabla-fila" >
                    <asp:Button  runat="server" ID="cmdImageEmpty" Text="Cargar imágen vacía" CssClass="boton-acciones"  />
                </td>
                <td class="tabla-fila" >
                    Esta opción permite cargar la imágen que se mostrará cuando no se encuentre cargada. En forma predeterminada se muestra en blanco<br />
                    </br>
                </td>
            </tr>
            <tr>
                <td  class="tabla-fila-alternativa" >Inicializar base de datos:
                </td>
                <td class="tabla-fila-alternativa">
                    <div runat="server" id="lstsysinit" ></div>
                </td>
                <td class="tabla-fila-alternativa" >
                    ATENCIÓN!!! Esta opción permite inicializar la base de datos del sistema.<br />
                </td>
            </tr>
            <tr>
                <td  class="tabla-fila-alternativa" >Actualizar sistema:
                    <br />
                    <asp:Label ID="lblVersionCur" runat="server"></asp:Label>
                    <br />
                    <asp:Label ID="lblVersionAvail" runat="server"></asp:Label>
                </td>
                <td class="tabla-fila-alternativa">
                    <div runat="server" id="lstsysupdate" ></div>
                </td>
                <td class="tabla-fila-alternativa" >
                    ATENCIÓN!!! Esta opción permite actualizar el sistema. Se debe utilizar ante nuevas versiones.<br />
                </td>
            </tr>
            <tr>
                <td  class="tabla-fila" >Seguimiento del sistema</td>
                <td  class="tabla-fila" >
                    <asp:Button  runat="server"  ID="cmdSYSTRACE" Text="Auditoría" CssClass="boton-acciones" CausesValidation="False"  />        
                </td>
                <td  class="tabla-fila" ></td>
            </tr>
            <tr>
                <td  class="tabla-fila" >Rechequeo de licencia</td>
                <td  class="tabla-fila" >
                    <asp:Button  runat="server"  ID="cmdLICCheck" Text="Rechequeo" CssClass="boton-acciones" CausesValidation="False"  />        
                </td>
                <td  class="tabla-fila" ></td>
            </tr>
            <tr>
                <td  class="tabla-fila" >Reimpacto de grupos</td>
                <td  class="tabla-fila" >
                    <asp:Button  runat="server"  ID="cmdReorganizeGrp" Text="Reimpactar" CssClass="boton-acciones" CausesValidation="False"  />        
                </td>
                <td  class="tabla-fila" ></td>
            </tr>
            <tr>
                <td  class="tabla-fila" ><img src="imagenes/tme.png" border=0 width="16px" />T.M.E (Time machine effect)</td>
                <td  class="tabla-fila" >
                    <asp:TextBox runat="server" ID="txtTME" CssClass="form-control" ></asp:TextBox>
                    <asp:Button  runat="server"  ID="cmdTME" Text="Aplicar" CssClass="boton-acciones" CausesValidation="False"  />     
                     <asp:Button  runat="server"  ID="cmdTMEGoNow" Text="Ir al momento actual" CssClass="boton-acciones" CausesValidation="False"  />      
                </td>
                <td  class="tabla-fila" ></td>
            </tr>
            </table>
            </td>
        </tr>
</table></asp:Content>



