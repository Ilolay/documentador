<%@ Page Language="VB" AutoEventWireup="false" CodeFile="hrctexteditor.aspx.vb" Inherits="hrctexteditor" ValidateRequest="false"  %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Edición de texto</title>
    <base target="_self" />
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
<meta http-equiv="X-UA-Compatible" content="IE=EmulateIE9" />
    <script src="plugins/jquery/jquery.min.js" type="text/javascript"></script>
    <script type="text/javascript" src="plugins/ckeditor/plugins/mathjax/lib/MathJax.js?config=TeX-AMS_HTML"></script>
    <script type="text/javascript" src="hrc/hrcGeneral.js" language="javascript" >
    </script>   
    <script type="text/javascript">
        function toggleAlert() {
            toggleDisabled(document.getElementById("fmelectura"));
        }

        function toggleDisabled(el) {
            try {
                el.disabled = el.disabled ? false : true;
            }
            catch (E) { }

            try {
                if (el.childNodes && el.childNodes.length > 0) {
                    for (var x = 0; x < el.childNodes.length; x++) {
                        toggleDisabled(el.childNodes[x]);
                    }
                }
            }
            catch (E) { }
        }
        </script>

      <link href="estilos.css" rel="stylesheet" type="text/css" />   
      <link href="plugins/ckeditor/contents.css" rel="stylesheet" type="text/css" />    
</head>
<body runat="server" id="fmebody" style="background-color: #F0F0F0;" >
    <form id="form1" runat="server"   >
    <div > 
        <asp:Button runat="server" Text="Guardar" CssClass="boton-acciones" ID="cmdSave" Visible=false  />         
          <asp:Button runat="server" Text="Editar" CssClass="boton-acciones" ID="cmdEdit" Visible=false  />   
          <asp:Button runat="server" Text="Vista previa" CssClass="boton-acciones" ID="cmdPreview" Visible=false />   
          <asp:Label runat="server" ID="lblPreview" Text="Vista previa:" Visible=false  />
            <asp:Button runat="server" ID="cmdVerPDF" CssClass="boton-acciones" 
                        Text="Ver PDF" Visible="false" CausesValidation="false" />
          <asp:Label ID="lblerror" runat="server" Visible="false" CssClass="error" ></asp:Label>
          <asp:TextBox ID="txtTexto" runat="server" TextMode="MultiLine" />          
          <div  id="lblTexto" runat="server"  Visible="false"   />          
    </div>
    <iframe ID="KeepAliveFrame" src="hrcKeepAlive.aspx" frameborder="0" width="0" height="0" runat="server"></iframe>
    </form>
</body>
</html>
