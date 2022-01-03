
Partial Class hrcBoot
    Inherits imWebPage

    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        Dim auxHrcContext As clsHrcJSContext = Session("hrccontext")
        If auxHrcContext IsNot Nothing Then
            Dim auxHTML As New Intelimedia.Hercules.Language.clsHrcCodeHTML
            ScriptManager.RegisterStartupScript(Me, Me.GetType, "hrc" & auxHrcContext.ControlID, _
                                                  auxHrcContext.gJS_BootCodeGet(auxHTML.gJS_GotoURL("'" & hrcFormInitial & "'")), True)
        End If
    End Sub
End Class
