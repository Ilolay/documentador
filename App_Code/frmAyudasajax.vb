Imports System.Web
Imports System.Web.Services
Imports System.Web.Services.Protocols
Imports System.Data
Imports System.Data.SqlClient
Imports System.Collections.Generic
Imports System.Web.Script.Services
<ScriptService()> _
<WebService(Namespace:="http://tempuri.org/")> _
<WebServiceBinding(ConformsTo:=WsiProfiles.BasicProfile1_1)> _
<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Public Class frmAyudasajax
    Inherits System.Web.Services.WebService
    <ScriptMethod()> _
    <WebMethod()> _
    Public Function ctrl000016_Get(ByVal prefixText As String, ByVal count As Integer) As String()
        Dim auxList As New List(Of String)
        Dim auxConn As SqlConnection = gConn_Open()
        Dim auxDA As New SqlDataAdapter("SELECT  DISTINCT Q_HLP.hlpubic FROM Q_HLP   WHERE hlpubic LIKE @prefixText", auxConn)
        auxDA.SelectCommand.Parameters.Add(New SqlParameter("@prefixText", SqlDbType.Text)).Value = "%" & prefixText & "%"
        Dim auxDT As New DataTable
        auxDA.Fill(auxDT)
        For Each auxRow As DataRow In auxDT.Rows
            auxList.Add(auxRow(0).ToString)
        Next
        Return auxList.ToArray
    End Function

    <ScriptMethod()> _
    <WebMethod()> _
    Public Function ctrl000017_Get(ByVal prefixText As String, ByVal count As Integer) As String()
        Dim auxList As New List(Of String)
        Dim auxConn As SqlConnection = gConn_Open()
        Dim auxDA As New SqlDataAdapter("SELECT  DISTINCT Q_HLP.hlpdsc FROM Q_HLP   WHERE hlpdsc LIKE @prefixText", auxConn)
        auxDA.SelectCommand.Parameters.Add(New SqlParameter("@prefixText", SqlDbType.Text)).Value = "%" & prefixText & "%"
        Dim auxDT As New DataTable
        auxDA.Fill(auxDT)
        For Each auxRow As DataRow In auxDT.Rows
            auxList.Add(auxRow(0).ToString)
        Next
        Return auxList.ToArray
    End Function

    Private Function gConn_Open() As SqlConnection
        Dim auxConn As New SqlConnection(Session("connectionstringname"))
        Return auxConn
    End Function
End Class

