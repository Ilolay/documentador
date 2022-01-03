Imports Intelimedia.imComponentes

Partial Class hrcJobQueue
    Inherits imWebPage
    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        Dim auxCacheType As clsHrcJSControlBasic.enumCacheStoreType = clsHrcJSControlBasic.enumCacheStoreType.coTemporarySession
        Dim auxID As String = Request.QueryString("tmpid")
        Dim auxClient As New imClientConnection
        'Dim auxHrcContext As clsHrcJSContext = Session("hrccontext")
        Dim auxHrcContext As New clsHrcJSContext("context", Session("conn"), Nothing, Nothing)
        Dim auxJobForm As clsHrcJSJobForm
        If auxID <> "" Then
            auxJobForm = auxClient.gObjectTmp_Download(auxID)
        Else
            auxID = Request.QueryString("tmpidg")
            If auxID <> "" Then
                auxJobForm = auxClient.gObjectTmp_DownloadFromGlobal(auxID)
            Else
                auxID = Request.QueryString("tmpidc")
                If auxID <> "" Then
                    auxJobForm = auxHrcContext.gObjectTmp_Download(auxID)
                End If
            End If
        End If

        Dim auxTemplate As clsHrcJSJobForm.enumTemplate = clsHrcJSJobForm.enumTemplate.coBasic
        If auxJobForm Is Nothing Then
            auxJobForm = New clsHrcJSJobForm("jobform", hrcProcessQueue, auxHrcContext)
            If Session("isadmin") Then
                Select Case Val(Request.QueryString("_view_"))
                    Case 1
                        auxTemplate = clsHrcJSJobForm.enumTemplate.coAdminFull
                    Case 2
                        auxTemplate = clsHrcJSJobForm.enumTemplate.coAdmin
                    Case 3
                        auxTemplate = clsHrcJSJobForm.enumTemplate.coBasic
                    Case 4
                        auxTemplate = clsHrcJSJobForm.enumTemplate.coDebug
                    Case Else
                        auxTemplate = clsHrcJSJobForm.enumTemplate.coAdmin
                End Select
            Else
                Select Case Val(Request.QueryString("_view_"))
                    Case 3
                        auxTemplate = clsHrcJSJobForm.enumTemplate.coBasic
                End Select
            End If
        End If


       
        If auxJobForm IsNot Nothing Then
            Dim auxBagResult As clshrcBagValues = auxJobForm.gJobForm_Generate(auxHrcContext, auxTemplate)
            fmeContent.InnerHtml = auxBagResult.gValue_Get("CONTENT", "")
            lblTitle.InnerHtml = auxBagResult.gValue_Get("TITLE", "")
            fmeContent.InnerHtml = fmeContent.InnerHtml.Replace("{#TITLE#}", "")
            ScriptManager.RegisterStartupScript(Me, Me.GetType, "hrcForm", _
                                auxBagResult.gValue_Get("SCRIPT", ""), True)
        End If
    
    End Sub

  

End Class


