<%@ Page Title="" Language="VB" AutoEventWireup="false" CodeFile="cfrmdocumentos_impresion.aspx.vb" Inherits="cfrmdocumentos_impresion" EnableViewState="false" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server" enableviewstate="false" >
    <title>Impresión</title>
    <link href="hrcTheme/style.css" rel="stylesheet" type="text/css" />    
    <link href="plugins/ckeditor/contents.css" rel="stylesheet" type="text/css" />    
    <script src='plugins/dateformat/date-es-AR.js' type='text/javascript'></script>
<script src='plugins/jquery/1.7.2/jquery-1.7.2.min.js' type='text/javascript'></script>
<script src='plugins/jquery/jquery-ui-1.8.22.custom.min.j' type='text/javascript'></script>
    <script type="text/javascript" src="plugins/ckeditor/plugins/mathjax/lib/MathJax.js?config=TeX-AMS_HTML"></script>
    <script src='hrc/hrcGeneral.js' type='text/javascript'></script>
    <link href="estilos.css" rel="stylesheet" type="text/css" />    
    <style runat="server" ID="lblStyles" enableviewstate="false" />
    <base target="_self" />
    </head>
<body runat="server" id="pnlcuerpo" enableviewstate="false" style="margin:0;padding:0" >
    <form runat="server" id="form1" enableviewstate="false"  target="_self" enctype="multipart/form-data"  >
        <asp:Literal  EnableViewState=false runat="server" ID="lblTexto"  Mode="PassThrough" Text="Generando reporte"   />    
  </form>
</body>
</html>


