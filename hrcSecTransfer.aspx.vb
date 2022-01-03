'v6/8/2013
Imports Intelimedia.Hercules.Communications
Partial Class hrcSecTransfer
    Inherits System.Web.UI.Page

    Protected Sub hrcSecTransfer_Load(sender As Object, e As EventArgs) Handles Me.Load
        Dim auxAppURL As String = Request.QueryString("appurl")
        If auxAppURL <> "" Then
            auxAppURL = HttpUtility.UrlDecode(auxAppURL)
            Dim auxLocalComm As New clshrcCommunications(auxAppURL & "wssystem.ashx", coWSsystempsw, "", "", "", "", 0, clshrcCommunications.enumContentType.coXML, clshrcCommunications.enumContentType.coXML, clshrcCommunications.enumProtocolType.coASHX, -1, False, "")
            Dim auxBagValues As New clshrcBagValues
            auxBagValues.gValue_Add("EVENT_NAME", "sec_session_start")
            Dim auxClass As New clshrcGeneral
            auxClass.Conn.gConn_Open()
            Dim auxSecurity As clsHrcSecurityClient = auxClass.Sec ' Session("security")
            Dim auxSecDsc As String = auxSecurity.CurrentSourceSecDsc
            If auxSecDsc = "" Then
                auxSecDsc = auxSecurity.CurrentSecDsc
            End If
            auxBagValues.gValue_Add("SECDSC", auxSecDsc) '  Session("secdsc").ToString)
            auxClass.gSys_DebugLogAdd("Seguridad integrada de [" & auxSecDsc & "] hacia [" & auxAppURL & "]")
            Dim auxBagValues_Return As clshrcBagValues = auxLocalComm.gDialog_SendBagValues(-1, auxBagValues)
            Dim auxSesID As String = auxBagValues_Return.gValue_Get("SESID")
            auxClass.gSys_DebugLogAdd("Seguridad integrada de [" & auxSecDsc & "].Session[" & auxSesID & "] hacia [" & auxAppURL & "]" & auxLocalComm.LastErrorDescription)
            Dim auxURL As String = ""
            If auxSesID = "" Then
                auxURL = auxAppURL & "public_login.aspx?"
            Else
                auxURL = auxAppURL & "public_login.aspx?_view_=6&param1=" & auxSesID
            End If

            auxClass.Conn.gConn_Close()
            Response.Redirect(auxURL, False)
        End If
        
    End Sub
End Class
