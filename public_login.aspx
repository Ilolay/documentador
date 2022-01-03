<%@ Page Language="VB" MasterPageFile="general.master" AutoEventWireup="false" CodeFile="public_login.aspx.vb" Inherits="public_login" %>
<asp:Content ContentPlaceHolderID=conHeader runat=server ID ="cHeader" >

    <style type="text/css">
        .style1
        {
            height: 20px;
            width: 144px;
        }
        .style2
        {
            width: 144px;
        }
    </style>

</asp:Content>
<asp:Content id="cDatos" ContentPlaceHolderID="ContentDatos" runat="Server">
<table width="100%" border="0" cellpadding="0" cellspacing="0" height="100%">
  <tr>
    <td align="center" valign="middle" >
    <asp:Label runat="server" ID="lblTitle" CssClass="form-title"></asp:Label>
    <br />
<asp:Label runat="server" ID="lblSubtitle" CssClass="form-subtitle"></asp:Label>
    <table border="0" cellpadding="0" cellspacing="0" >
      <tr>
        <td  width="900px" style="height:350px;background-image:url(imagenes/fondo_sup.png)" runat="server" id="cell_bg">
        <table border="0" cellpadding="0" cellspacing="0" style="border-collapse: collapse;" width="100%" >
          <tr align="center" valign="middle">
            <td>
                <table border="0" cellpadding="0" cellspacing="0" style="width:500px;height:155px;">
                    <tr style="background-color:lightgray;height:32px ">
			            <td>
						    <span style="font-family: Verdana; font-size:small;" >
						                <img src="imagenes/spacer.gif" width="5px" height="1px" alt="" />
						                _intelimedia | acceso</span>
						</td>
          		    </tr>
                    <tr style="background-color:white">
                        <td  >
                            <table cellpadding="2" cellspacing="0">
                                <tr runat="server" id="row_user" >
                                    <td align="right" style="font-family: Verdana; font-size:small;" width="200px" >
                                        <asp:Label ID="UserNameLabel" runat="server" AssociatedControlID="txtuser" >Usuario:</asp:Label>
                                    </td>
                                    <td align="left" class="style1">
                                        <asp:TextBox ID="txtuser" runat="server" Font-Size="10pt" Width="150px" MaxLength="50"></asp:TextBox>
                                    </td>
                                    <td align="left" valign="middle" rowspan="4" style="width:80px" >
                                        <img border="0" src="imagenes/user.png" width="72px" height="72px" alt="" />
                                    </td>
                                </tr>
                                <tr runat="server" id="row_oldpsw" >
                                    <td align="right" style="font-family: Verdana; font-size:small;" >
                                        Contraseña:
                                    </td>
                                    <td  align="left" class="style2">
                                        <asp:TextBox ID="txtpwd" runat="server" Font-Size="10pt" Width="150px" MaxLength="25"
                                            TextMode="Password"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr runat="server" id="row_pswchange1" >
                                <td align="right" style="font-family: Verdana; font-size:small;" >
                                        Nueva contraseña:
                                    </td>
                                <td>
                                     <asp:TextBox ID="txtnewpwd1" runat="server" Font-Size="10pt" Width="150px" MaxLength="25"
                                            TextMode="Password"></asp:TextBox>
                                </td>
                                </tr>
                                 <tr runat="server" id="row_pswchange2" >
                                <td align="right" style="font-family: Verdana; font-size:small;" >
                                        Confirmación de nueva contraseña:
                                    </td>
                                <td>
                                       <asp:TextBox ID="txtnewpwd2" runat="server" Font-Size="10pt" Width="150px" MaxLength="25"
                                            TextMode="Password"></asp:TextBox>
                                </td>
                                </tr>
                                <tr>
                                    <td ></td>
                                    <td align="left" colspan="2" style="font-size: 10px; color: red; font-family: Verdana, Arial">
                                        <asp:Label runat="server" ID="lblmsg" EnableViewState="False"></asp:Label>
                                    </td>
                                </tr >                                
                                  <tr runat="server" id="row_check">
                                    <td   style="height: 22px" align="left">
                                        <div ID="divcheck" runat="server" />
                                    </td>
                                      <td>
                                            <asp:TextBox ID="txtcheck" runat="server" Font-Size="10pt" Width="150px" MaxLength="10"></asp:TextBox>

                                      </td>
                                </tr>
                                <tr>
                                    <td  colspan="2" style="height: 22px" align="left">
                                        <asp:Button ID="cmdLoginButton" runat="server" CssClass="button boton-acciones"
                                            CommandName="Login" Text="Ingresar"   />
                                         <asp:Button ID="cmdLogout" runat="server" Text="Salir" CssClass="button  boton-acciones"/>
                                         <asp:Button ID="cmdPswChange" runat="server" Text="Cambiar" CssClass="button  boton-acciones"/>
                                        <asp:Button ID="cmdLoginADButton" runat="server" CssClass="button boton-acciones"
                                            CommandName="LoginAD" Text="Ingresar con usuario dominio"   />
                                    </td>
                                </tr>
                                
                            </table>
                        </td>
                    </tr>

                </table>
            </td>
        </tr>
    </table>

 </td>
 </tr>
 </table>
 </td>
 </tr>

</table>

</asp:Content>
