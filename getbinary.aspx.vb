Public Class getbinary
Inherits System.Web.UI.Page
Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
imClientConnection1.gFile_Download(CInt(Val(Request.QueryString("id"))), CBool(If(Request.QueryString("prv") = "1", True, False)))
End Sub

End Class

