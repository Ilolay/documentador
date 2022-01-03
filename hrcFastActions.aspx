<%@ Page Language="VB" AutoEventWireup="false" CodeFile="hrcFastActions.aspx.vb" Inherits="hrcFastActions"  %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="estilos.css" rel="stylesheet" type="text/css" />  
       <base target="_self" />
 <script type="text/javascript" src="hrc/hrcGeneral.js" language="javascript" ></script>
 <script type="text/javascript" src="tooltip/js/balloon.config.js"></script>
 <script type="text/javascript" src="tooltip/js/balloon.js"></script>
 <script type="text/javascript" src="tooltip/js/box.js"></script>
 <script type="text/javascript" src="tooltip/js/yahoo-dom-event.js"></script>
 <script type="text/javascript">
     // Stemless blue balloon
     var blueBalloon = new Balloon;
     blueBalloon.balloonTextSize = '90%';
     blueBalloon.images = '/tooltip/images/';
     blueBalloon.balloonImage = 'balloon.png';
     blueBalloon.vOffset = 15;
     blueBalloon.shadow = 50;
     blueBalloon.stemHeight = 10;
     blueBalloon.stem = false;
     blueBalloon.ieImage = null;
     blueBalloon.displayTime = false;
</script>
</head>
<body style="width:600px;">
    <form id="form1" runat="server">
    <div>
        <table width="100%" cellpadding="0" cellspacing="0">
    <tr>
    <td>
        <asp:Label runat="server" CssClass="obs-controles" ID="lbltitle" ></asp:Label>
        <asp:Label runat="server" ID="lbldeactivate" ></asp:Label>
        <asp:Panel ID="fmeDeactivate" runat="server" Visible="false"  >
                      <asp:Button runat="server"   ID="cmdDeactivate"                                   
                        CssClass="boton-acciones"  Text="Utilizar mi usuario"  OnClick="cmdDeactivate_Click"
                        OnClientClick='<%# "return confirm(""Confirma que desea dejar de utilizar el usuario de " & Replace(Ctype(Session("security"),clsHrcSecurityClient).CurrentSecDsc,"\","\\") & "?"");" %>'  
                        CausesValidation="false" />
        </asp:Panel>
        <asp:Panel ID="fmeActivate" runat="server" Visible="false"  >                     
         <asp:GridView ID="grdDelegationsToMy" runat="server" AutoGenerateColumns="False" 
                     Width="100%"   ShowFooter="False"  ShowHeader="false" GridLines="Horizontal"  
                    >
               <RowStyle CssClass="pryceldaotro"  />
               <HeaderStyle CssClass="pryceldatitulos" />                            
               <FooterStyle CssClass ="pryceldaotro"  />
               <Columns>              
                <asp:TemplateField ItemStyle-Width="120px" >
                        <ItemTemplate>
                            <asp:Button  ID="imgd" runat="server"   CommandName ="CMDACTIVATE"  
                                CssClass="boton-acciones"   
                                CommandArgument='<%# Eval("delid") %>' 
                                Text='<%# "Utilizar [" & Eval("secdsc") & "]"  %>' />
                          </ItemTemplate>                              
                   </asp:TemplateField>    
                     <asp:TemplateField >
                        <ItemTemplate>
                            <asp:Label runat="server" ID="lbldelegadoa" Text='<%# "delegado a " & Eval("siddsc_dst")  %>' ></asp:Label>
                        </ItemTemplate>
                     </asp:TemplateField> 
                </Columns>
                </asp:GridView>
      </asp:Panel>
    </td>
    </tr>
    </table>
    </div>
     <script type="text/javascript">
         var divHeight;
         if ((document.body.offsetHeight) && (parent.window.document.getElementById("fmeFastActions")))
         { parent.window.document.getElementById("fmeFastActions").height = document.body.offsetHeight + 20; }
         else if (document.body.style.pixelHeight)
         { parent.window.document.getElementById("fmeFastActions").pixelHeight = document.body.pixelHeight + 20; }           
    </script>
    </form>
</body>
</html>
