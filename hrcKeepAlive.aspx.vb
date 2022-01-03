'v4 - 30/1/2014
Partial Class hrcKeepAlive
    Inherits System.Web.UI.Page
    Public WindowStatusText As String = ""
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim auxSeconds As Long = (Session.Timeout * 60) - 60
        MetaRefresh.Attributes("content") = Convert.ToString(auxSeconds) & ";url=hrcKeepAlive.aspx?q=" & DateTime.Now.Ticks

        Dim auxExpireDate As Date = Now.AddSeconds(auxSeconds - 1)
        Response.Cache.SetExpires(auxExpireDate)
        Response.Cache.SetCacheability(HttpCacheability.Private)
        WindowStatusText = "Ult.conexión:" & DateTime.Now.ToShortDateString() & " " & DateTime.Now.ToShortTimeString()
    End Sub
End Class
