<%@ Page Language="VB" AutoEventWireup="false" CodeFile="hrcbinaries.aspx.vb" Inherits="hrcbinaries" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <base target="_self" /> 
    <link href="estilos.css" rel="stylesheet" type="text/css" />   
</head>
<body style="xbackground-color: #F0F0F0;" >
    <form id="form1" target="_self" enctype="multipart/form-data" runat="server" >      
    <asp:FileUpload  Height="20" ID="fleUpload" runat="server" onchange="javascript:form1.submit()"  CssClass="boton-acciones" />    
    <asp:ImageButton runat="server" ID="cmdDelete" Visible="False"  ImageUrl="imagenes/actdel.png" Width="16" OnClick="cmdDelete_Click" />     
     <asp:HyperLink runat="server" ID="fileview"  Target ="_blank"  ></asp:HyperLink>                
      <asp:Label ID="lblerror" runat="server" Visible="false" CssClass="error" Text=""></asp:Label>
  </form>
</body>
</html>
