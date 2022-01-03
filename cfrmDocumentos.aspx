<%@ Page Language="VB" CodeFile="cfrmDocumentos.aspx.vb" MasterPageFile="general.master" AutoEventWireup="True" Inherits="cfrmDocumentos" Title="Documentos" EnableViewState="false"  %>
<asp:Content id="cHeader" ContentPlaceHolderID="conHeader" runat="Server">
     <div runat="server" id="lblTitle" ></div>
</asp:Content>
<asp:Content id="cDatos" ContentPlaceHolderID="ContentDatos" runat="Server">
    <div runat="server" id="fmeContent"  />
    <input type="hidden" id="hdnPrincipalForm"  runat="server"  />
</asp:Content>





