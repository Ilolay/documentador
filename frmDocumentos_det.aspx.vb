Imports System.Data
Imports System.Data.SqlClient
Imports Intelimedia.imComponentes
Public Class frmDocumentos_det
Inherits System.Web.UI.Page
Friend m_PermFormDelete as Boolean=False
Friend m_PermFormEdit as Boolean=False
Friend m_PermFormNew as Boolean=False
Private Sub gForm_Close()
     If Request.QueryString("_closea_") = "1" Then
      ScriptManager.RegisterStartupScript(Me, Me.GetType(), "CloseWindow", "self.close();", True)
     Else
         Response.Redirect("frmDocumentos.aspx?_mode_=7&_closea_=0")
     End If

End Sub
Protected Sub frmDocumentos_det_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

If Request.QueryString("_mode_") = "1" _ 
   Or Request.QueryString("_mode_") = "25" Then
    Page.Response.Clear()
    Server.Transfer("frmDocumentos3_det.aspx?" & Request.QueryString.ToString)
Else
    Try
        Dim auxConn As New SqlConnection(ConfigurationManager.ConnectionStrings("imdoc").ConnectionString)
        auxConn.Open
        Dim auxDA As New SqlDataAdapter("SELECT cod,(SELECT TOP 1 wfwstpcod FROM Q_WFWTHR WHERE Q_WFWTHR.widcod = DOC_DOC.qwfwwid) as wfwstpcod FROM DOC_DOC  WHERE cod=@param1", auxConn)
        auxDA.SelectCommand.Parameters.Add("@param1", SqlDbType.Int, 4).Value = Request.QueryString("param1")
        Dim auxDT As New DataTable
        auxDA.Fill(auxDT)
        auxConn.Close
        If auxDT.Rows.Count <> 0 Then
            If IsDBNull(auxDT.Rows(0)("wfwstpcod")) = False Then
                Page.Response.Clear()
                Server.Transfer("frmDocumentos" & Format(If(auxDT.Rows(0)("wfwstpcod")< 1, 3, auxDT.Rows(0)("wfwstpcod")), "0") & "_det.aspx?" & Request.QueryString.ToString)
            End If
        End If
    Catch ex as sqlException
    End Try
End If

End Sub
 Protected Overrides Sub OnInit(ByVal e As System.EventArgs)
MyBase.OnInit(e)
ViewStateUserKey = Session.SessionID

End Sub

End Class

