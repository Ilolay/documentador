<%@ page language="VB" autoeventwireup="false" inherits="ckeditor_linkbrowser, App_Web_6zcoxatc" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="ajaxkit" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
      <title>Asistente de enlaces</title>
       <link href="../../estilos.css" rel="stylesheet" type="text/css" />   
       <script src='../dateformat/date-es-AR.js' type='text/javascript'></script>

    <script src='../jquery/1.7.2/jquery-1.7.2.min.js' type='text/javascript'></script>
<script src='../jquery/jquery-ui-1.8.13.min.js' type='text/javascript'></script>

<script type="text/javascript" src="../../hrc/hrcGeneral.js" language="javascript" ></script>
        <link href="../jquery/autosuggest/autoSuggest.css" rel="stylesheet" type="text/css" />    
        <script src="../jquery/autosuggest/jquery.autoSuggest.js" type="text/javascript"></script>
        <script src="../jquery/simplemodal/jquery.simplemodal-1.4.1.js" type="text/javascript"></script>


        <link rel="stylesheet" type="text/css" media="screen" href="../jquery/themes/redmond/jquery-ui-1.8.2.custom.css" />        <link rel="stylesheet" type="text/css" media="screen" href="../jquery/themes/ui.jqgrid.css" />        <script src="../jquery/jquery.layout.js" type="text/javascript"></script>        <script src="../jquery/i18n/grid.locale-sp.js" type="text/javascript"></script>        <script src="../jquery/jquery.jqGrid.min.js" type="text/javascript"></script>        <script src="../jquery/jquery.tablednd.js" type="text/javascript"></script>        <script src="../../hrc/core.js"  type="text/javascript"  />        <script src="../jquery/jquery.contextmenu.js" type="text/javascript"></script> 
         	<style type="text/css">
        body
        {
            margin: 0px;
        }
        
        form
        {
            width:500px;
            background-color: #E3E3C7;
        }
        
        h1
        {
            padding: 15px;
            margin: 0px;
            padding-bottom: 0px;
            font-family:Arial;
            font-size: 14pt;
            color: #737357;
        }
        
        .tab-panel .ajax__tab_body
        {
            background-color: #E3E3C7;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <asp:ScriptManager ID="ScriptManager1" runat="server" />        
        <h1>Asistente de enlaces</h1>        
        <table width="100%" cellpadding="10" cellspacing="0" border="1" style="background-color:#F1F1E3;">
            <tr>
                <td valign="top">
                    Subir archivos: (10 MB max)<br />
                    <asp:FileUpload ID="UploadedImageFile" runat="server" />
                    <asp:Button ID="UploadButton" runat="server" Text="Subir archivo" OnClick="Upload" />
                    <br />
                    <br />                                      
                    <asp:ListBox ID="ImageList" runat="server" Style="width: 300px; " AutoPostBack="true" Rows="1" />
                    <br />
                    <asp:Button ID="RenameImageButton" runat="server" Text="Renombrar archivo" Visible="false"  />
                    <br />
                </td>
            </tr>
            <tr>
            <td>
              Links a objetos del sistema  
              <br /><br />
                <div runat="server"  id="fmeDocCod" ></div>
                <asp:HiddenField  runat="server" ID="hdnDocCodID" />
               
            </td>
            </tr>            
        </table>

        <center>
            <asp:Button ID="OkButton" runat="server" Text="Insertar enlace" />
            <asp:Button ID="CancelButton" runat="server" Text="Cancelar" OnClientClick="window.top.close(); window.top.opener.focus();" OnClick="Clear" />
            <br /><br />
        </center>
    </div>
    </form>
</body>
</html>
