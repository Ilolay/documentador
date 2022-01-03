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
Public Class frmUnidades_detajax
Inherits System.Web.Services.WebService
<ScriptMethod()> _ 
<WebMethod()> _ 
Public Function editctrl000015_Get (ByVal prefixText As String, ByVal count As Integer) As String()
Dim auxList As New List(Of String)
Dim auxConn As  SqlConnection=gConn_open
Dim auxDA As New SqlDataAdapter("SELECT  DISTINCT UND.dsc FROM UND   WHERE (dsc LIKE @prefixText) AND (UND.baja = 0 OR UND.baja  IS NULL)",auxConn)
auxDA.SelectCommand.Parameters.Add(New SqlParameter("@prefixText", SqlDbType.Text)).Value ="%" & prefixText & "%"
Dim auxDT As New DataTable
auxDA.Fill(auxDT)
For Each auxRow As DataRow In auxDT.Rows
  auxList.Add(auxRow(0).ToString)
Next
Return auxList.ToArray
End Function

<ScriptMethod()> _ 
<WebMethod()> _ 
Public Function editctrl000010_Get (ByVal prefixText As String, ByVal count As Integer) As String()
Dim auxList As New List(Of String)
Dim auxConn As  SqlConnection=gConn_open
Dim auxDA As New SqlDataAdapter("SELECT  DISTINCT UND.undnro FROM UND   WHERE (undnro LIKE @prefixText) AND (UND.baja = 0 OR UND.baja  IS NULL)",auxConn)
auxDA.SelectCommand.Parameters.Add(New SqlParameter("@prefixText", SqlDbType.Text)).Value ="%" & prefixText & "%"
Dim auxDT As New DataTable
auxDA.Fill(auxDT)
For Each auxRow As DataRow In auxDT.Rows
  auxList.Add(auxRow(0).ToString)
Next
Return auxList.ToArray
End Function

<ScriptMethod()> _ 
<WebMethod()> _ 
Public Function editctrl000006_Get (ByVal prefixText As String, ByVal count As Integer) As String()
Dim auxList As New List(Of String)
Dim auxConn As  SqlConnection=gConn_open
Dim auxDA As New SqlDataAdapter("SELECT  DISTINCT UND.formatoespecifico FROM UND   WHERE (formatoespecifico LIKE @prefixText) AND (UND.baja = 0 OR UND.baja  IS NULL)",auxConn)
auxDA.SelectCommand.Parameters.Add(New SqlParameter("@prefixText", SqlDbType.Text)).Value ="%" & prefixText & "%"
Dim auxDT As New DataTable
auxDA.Fill(auxDT)
For Each auxRow As DataRow In auxDT.Rows
  auxList.Add(auxRow(0).ToString)
Next
Return auxList.ToArray
End Function

<ScriptMethod()> _ 
<WebMethod()> _ 
Public Function insctrl000015_Get (ByVal prefixText As String, ByVal count As Integer) As String()
Dim auxList As New List(Of String)
Dim auxConn As  SqlConnection=gConn_open
Dim auxDA As New SqlDataAdapter("SELECT  DISTINCT UND.dsc FROM UND   WHERE (dsc LIKE @prefixText) AND (UND.baja = 0 OR UND.baja  IS NULL)",auxConn)
auxDA.SelectCommand.Parameters.Add(New SqlParameter("@prefixText", SqlDbType.Text)).Value ="%" & prefixText & "%"
Dim auxDT As New DataTable
auxDA.Fill(auxDT)
For Each auxRow As DataRow In auxDT.Rows
  auxList.Add(auxRow(0).ToString)
Next
Return auxList.ToArray
End Function

<ScriptMethod()> _ 
<WebMethod()> _ 
Public Function insctrl000010_Get (ByVal prefixText As String, ByVal count As Integer) As String()
Dim auxList As New List(Of String)
Dim auxConn As  SqlConnection=gConn_open
Dim auxDA As New SqlDataAdapter("SELECT  DISTINCT UND.undnro FROM UND   WHERE (undnro LIKE @prefixText) AND (UND.baja = 0 OR UND.baja  IS NULL)",auxConn)
auxDA.SelectCommand.Parameters.Add(New SqlParameter("@prefixText", SqlDbType.Text)).Value ="%" & prefixText & "%"
Dim auxDT As New DataTable
auxDA.Fill(auxDT)
For Each auxRow As DataRow In auxDT.Rows
  auxList.Add(auxRow(0).ToString)
Next
Return auxList.ToArray
End Function

<ScriptMethod()> _ 
<WebMethod()> _ 
Public Function insctrl000006_Get (ByVal prefixText As String, ByVal count As Integer) As String()
Dim auxList As New List(Of String)
Dim auxConn As  SqlConnection=gConn_open
Dim auxDA As New SqlDataAdapter("SELECT  DISTINCT UND.formatoespecifico FROM UND   WHERE (formatoespecifico LIKE @prefixText) AND (UND.baja = 0 OR UND.baja  IS NULL)",auxConn)
auxDA.SelectCommand.Parameters.Add(New SqlParameter("@prefixText", SqlDbType.Text)).Value ="%" & prefixText & "%"
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

