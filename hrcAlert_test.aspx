<%@ Page Language="VB" AutoEventWireup="false" CodeFile="hrcalert_test.aspx.vb" Inherits="hrcalert_test" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Test envío de mails</title>
    <link href="estilos.css" rel="stylesheet" type="text/css" />       
</head>
<body>
    <form id="form1" runat="server">  
    <table width="300px" cellpadding="0" cellspacing="0">
<tr>
    <td  >
    <img  src="imagenes/objmail.png" width="16px" height="16px"  hspace="4" />    
    <span class="form-subtitle" >Test envío de mails</span>
    <asp:Label  runat="server" ID="lbljobqueueState"  />
    </td>
</tr>
       <tr>
            <td  class=index-novedades" >
                Dirección destino</td>
            </tr>
            <tr>
            <td>
                    <asp:TextBox ID="txtMail" runat="server" CssClass="form-control" Width="400px" ></asp:TextBox>
            </td>
            </tr>
            <tr>
            <td>
                    <asp:Button ID="cmdMail" runat="server" Text="Enviar mail" CssClass="boton-acciones" />
            </td>
        </tr>
    </table>
    <p>
    <asp:Label ID="lblResult" runat="server" CssClass="error" ></asp:Label>
    </p>   
    </form>
</body>
</html>
