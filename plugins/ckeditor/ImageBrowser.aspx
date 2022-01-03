<%@ page language="C#" autoeventwireup="true" inherits="ImageBrowserPage, App_Web_mfhicx1g" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
	<title>Búsqueda de imágenes</title>
	<link href="../../estilos.css" rel="stylesheet" type="text/css" />   
	<style type="text/css">
		body { margin: 0px; }
		form { width:750px; background-color: #E3E3C7; }
		h1 { padding: 15px; margin: 0px; padding-bottom: 0px; font-family:Arial; font-size: 14pt; color: #737357; }
	</style>
</head>
<body>
	<form id="form1" runat="server">
	<div>
		<asp:ScriptManager ID="ScriptManager1" runat="server" />
		
		<h1>Búsqueda de imágenes</h1>
		
		<asp:UpdatePanel ID="UpdatePanel1" runat="server">
			<ContentTemplate>
			
				<table width="720px" cellpadding="10" cellspacing="0" border="1" style="background-color:#F1F1E3; margin:15px;">
					<tr>
						<td style="width: 396px;" valign="middle" align="center">
							<asp:Image ID="Image1" runat="server" Style="max-height: 450px; max-width: 380px;" />
						</td>
						<td style="width: 324px;" valign="top">						
    						Subir imágen: (10 MB max)
							<asp:FileUpload ID="UploadedImageFile" runat="server" />
							<asp:Button ID="UploadButton" runat="server" Text="Subir" OnClick="Upload" />							
							<br />
							<br />
							Opcional: Cambiar tamaño
							<asp:ListBox ID="ImageList" runat="server" Style="width: 280px; height: 50px;" 
                                OnSelectedIndexChanged="SelectImage" AutoPostBack="true" Rows="1" />
							<asp:HiddenField ID="NewImageName" runat="server" />
							<asp:Button ID="RenameImageButton" runat="server" Text="Renombrar imágen" 
                                OnClick="RenameImage" Visible="False" />
							<br />
							<br />
							Cambiar tamaño:<br />
							<asp:TextBox ID="ResizeWidth" runat="server" Width="50" OnTextChanged="ResizeWidthChanged" />
							x
							<asp:TextBox ID="ResizeHeight" runat="server" Width="50" OnTextChanged="ResizeHeightChanged" />
							<asp:HiddenField ID="ImageAspectRatio" runat="server" />
							<asp:Button ID="ResizeImageButton" runat="server" Text="Cambiar tamaño" OnClick="ResizeImage" /><br />
							<asp:Label ID="ResizeMessage" runat="server" ForeColor="Red" />
							<br />
						</td>
					</tr>
				</table>
				
				<center>
					<asp:Button ID="OkButton" runat="server" Text="Insertar imágen" 
                        OnClick="Clear" />
					<asp:Button ID="CancelButton" runat="server" Text="Cancelar" OnClientClick="window.top.close(); window.top.opener.focus();" OnClick="Clear" />
					<br /><br />
				</center>
		
			</ContentTemplate>
			<Triggers>
				<asp:PostBackTrigger ControlID="UploadButton" />
			</Triggers>
		</asp:UpdatePanel>
	</div>
	</form>
</body>
</html>
