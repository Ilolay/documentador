<%@ Page Language="VB" AutoEventWireup="false" CodeFile="hrcKeepAlive.aspx.vb" Inherits="hrcKeepAlive" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Keep Alive</title>        <meta id="MetaRefresh" http-equiv="refresh" content="21600;url=hrcKeepAlive.aspx" runat="server" />

        <script language="javascript" type="text/javascript" >
            window.status = "<%=WindowStatusText%>";
        </script>
</head>
<body>
    <form id="form1" runat="server">
    </form>
</body>
</html>
