<%@ Page Language="VB" AutoEventWireup="false" CodeFile="cfrmtest.aspx.vb" Inherits="cfrmtest" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Untitled Page</title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <script language=javascript >
            
        </script>
    </div>
    <br />
    <table style="width:100%;">
        <tr>
            <td>
                Codigo de proyecto</td>
            <td>
    <asp:TextBox ID="txtSidCod" runat="server">1</asp:TextBox>
            </td>
            <td>
    <asp:Button ID="cmdTest" runat="server" Text="Realizar el test del proyecto" />
            </td>
        </tr>
        <tr>
            <td>
                Codigo de proyecto y de empleado</td>
            <td>
                <asp:TextBox ID="txtprocod" runat="server"></asp:TextBox>
                <asp:TextBox ID="txtEmpCod" runat="server"></asp:TextBox>
            </td>
            <td>
    <asp:Button ID="cmdTest0" runat="server" Text="Test de poder de decision" />
            </td>
        </tr>
        <tr>
            <td>
                &nbsp;</td>
            <td>
                &nbsp;</td>
            <td>
                &nbsp;</td>
        </tr>
    </table>
    <p>
    <asp:Label ID="lblResult" runat="server"></asp:Label>
    </p>
    <p>
        &nbsp;</p>
    </form>
</body>
</html>
