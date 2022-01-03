Imports Intelimedia.imComponentes
Partial Class hrcbinaries
    Inherits System.Web.UI.Page

    'Protected Sub Page_Error(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Error
    '    Server.Transfer("gerror.aspx?msg=El archivo es muy grande")
    'End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        'Try
        Dim auxConn As New imClientConnection
        If Not IsPostBack Then
            If Request.QueryString("error413") = "1" Then
                Dim auxInstance As New Web.Configuration.HttpRuntimeSection
                Dim auxValue As Integer = auxInstance.MaxRequestLength
                lblerror.Text = "Tamaño muy grande. Excede " & auxValue & " KB"
                lblerror.Visible = True
            End If
            If Request.QueryString("upload") = "1" Then
                Dim auxID As Integer = -1
                auxID = Val(Request.QueryString("downloadinitialid"))
                If auxID < 1 Then
                    If Request.QueryString("tmpid").ToString <> "" Then
                    Else
                        auxID = Val(Request.QueryString("id"))
                    End If
                End If
                If auxID > 0 Then
                    fileview.Visible = True
                    cmdDelete.Visible = True
                    Select Case Request.QueryString("isimage")
                        Case "1"
                            fileview.Text = "<img border=0 src=" & Request.Path & "?prv=1&id=" & auxID & "&tmpstamp=" & Now.ToString("HHmmss") & " >"
                        Case Else
                            Dim auxCon As clsHrcConnClient = Session(auxConn.ConnClientSessionName)
                            fileview.Text = "Descargar " & auxCon.gConn_BLOBToBinaryDataMetadata(auxID).Filename
                    End Select
                    fileview.NavigateUrl = "getbinary.aspx?id=" & auxID
                Else
                    fileview.Visible = False
                End If
        ElseIf Request.QueryString("btmpid") <> "" And Request.QueryString("download") = "1" Then
            'Download=1, es la pnatalla para descargar
            Dim auxBinaryData As clsHrcConnClient.clsBinaryData = auxConn.gObjectTmp_Download(Request.QueryString("btmpid"))
            If auxBinaryData IsNot Nothing Then
                fleUpload.Visible = False
                fileview.Visible = True
                cmdDelete.Visible = False
                Select Case auxBinaryData.MimeType
                    Case clsHrcConnClient.enumMimeTypes.coBMP, clsHrcConnClient.enumMimeTypes.coGIF, clsHrcConnClient.enumMimeTypes.coJPG, clsHrcConnClient.enumMimeTypes.coPNG
                        fileview.Text = "<img border=0 src=" & Request.Path & "?prv=1&id=" & Request.QueryString("id") & "&tmpstamp=" & Now.ToString("HHmmss") & " >"
                    Case Else
                        fileview.Text = "Descargar " & auxBinaryData.Filename
                End Select
                fileview.NavigateUrl = "hrcbinaries.aspx?tmp=1&tmpid=" & Request.QueryString("btmpid")
                fileview.Target = "_self"
                End If
            ElseIf Request.QueryString("id") <> "" And Request.QueryString("download") = "1" Then
                fleUpload.Visible = False
                cmdDelete.Visible = False
                Dim auxID As Integer = -1
                auxID = Val(Request.QueryString("id"))
                If auxID > 0 Then
                    Select Case Request.QueryString("isimage")
                        Case "1"
                            fileview.Text = "<img border=0 src=" & Request.Path & "?prv=1&id=" & auxID & "&tmpstamp=" & Now.ToString("HHmmss") & " >"
                        Case Else
                            Dim auxCon As clsHrcConnClient = Session(auxConn.ConnClientSessionName)
                            fileview.Text = "Descargar " & auxCon.gConn_BLOBToBinaryDataMetadata(auxID).Filename
                    End Select
                    fileview.NavigateUrl = "getbinary.aspx?id=" & auxID
                End If
            Else
                fleUpload.Visible = False
                fileview.Visible = False
                Select Case Request.QueryString("inline")
                    Case "1"
                        auxConn.DownloadMode = imClientConnection.enumDownloadMode.coInline
                    Case Else
                End Select
                If Request.QueryString("tmp") = "" Then
                    auxConn.ConnClientSessionName = "conn"
                    auxConn.gFile_Download(CInt(Val(Request.QueryString("id"))), CBool(If(Request.QueryString("prv") = "1", True, False)))
                Else
                    auxConn.gFileTmp_Download(Request.QueryString("tmpid"), CBool(If(Request.QueryString("prv") = "1", True, False)))
                End If
        End If
        Else
        lblerror.Visible = False
        If Request.QueryString("upload") = "1" Then
            fleUpload.Visible = True
            fileview.Visible = True
            If fleUpload.HasFile Then
                If Request.QueryString("tmp") = "1" Then
                    Dim auxCacheID As String = Request.QueryString("tmpid")
                    If auxCacheID <> "" Then
                        auxConn.gFileTmp_Upload(fleUpload.FileName, fleUpload.FileBytes, 80, 80, auxCacheID)
                    End If
                Else
                    auxConn.gFile_Upload(fleUpload.FileName, fleUpload.FileBytes, CInt(Val(Request.QueryString("id"))), 100, 100, CInt(Val(Request.QueryString("tmpid"))))
                End If
                cmdDelete.Visible = True
                fileview.NavigateUrl = Request.Path & "?tmp=" & Request.QueryString("tmp") & "&tmpid=" & Request.QueryString("tmpid")
                Select Case Request.QueryString("isimage")
                    Case "1"
                        fileview.Text = "<img border=0 src=" & Request.Path & "?prv=1&tmp=" & Request.QueryString("tmp") & "&tmpid=" & Request.QueryString("tmpid") & "&tmpstamp=" & Now.ToString("HHmmss") & " >"
                    Case Else
                            fileview.Text = "Descargar " & fleUpload.FileName
                End Select
                fileview.Visible = True
            End If

        Else
            'cmdUpload.Visible = False
            fleUpload.Visible = False
                fileview.Visible = False
                Select Case Request.QueryString("inline")
                    Case "1"
                        auxConn.DownloadMode = imClientConnection.enumDownloadMode.coInline
                    Case Else
                End Select
                If Request.QueryString("tmp") = "" Then
                    auxConn.gFile_Download(CInt(Val(Request.QueryString("id"))), CBool(If(Request.QueryString("prv") = "1", True, False)))
                Else
                    auxConn.gFileTmp_Download(Request.QueryString("tmpid"), False)
                End If
        End If
        End If

        'Catch ex As Exception
        'Dim a As String = ex.Message
        'End Try

    End Sub

    Protected Sub cmdDelete_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim auxConn As New imClientConnection
        If Request.QueryString("upload") = "1" Then
            'cmdUpload.Visible = True
            fleUpload.Visible = True
            fileview.Visible = False
            'If fleUpload.HasFile Then
            If Request.QueryString("tmp") = "1" Then
                auxConn.gFileTmp_Delete(Request.QueryString("tmpid"))
                cmdDelete.Visible = False
            End If
            'End If
        End If
    End Sub
End Class
