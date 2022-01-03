Imports Intelimedia.Hercules.Language
Public Class hrcAlerts
    Inherits System.Web.UI.Page
   

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            Dim auxHrcContext As clsHrcJSContext = Context.Session("hrcContext")
            Dim auxalertForm As New clsHrcJSAlertForm(auxHrcContext.Alert, auxHrcContext.Conn)
            fmeAlerts.InnerHtml = auxalertForm.gControl_GetBodyDefinition
            ScriptManager.RegisterStartupScript(Me, Me.GetType, "hrc" & "ALERTS", _
                                                 auxalertForm.gControl_GetStartupScripts(), True)
        End If
    End Sub
End Class

