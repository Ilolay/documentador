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
Public Class frmTiposdeprocesos_detajax
Inherits System.Web.Services.WebService
<ScriptMethod()> _ 
<WebMethod()> _ 
Public Function editctrl000005_Get (ByVal prefixText As String, ByVal count As Integer) As String()
Dim auxList As New List(Of String)
Dim auxConn As  SqlConnection=gConn_open
Dim auxDA As New SqlDataAdapter("SELECT  DISTINCT DOC_APA.dsc FROM DOC_APA   WHERE (dsc LIKE @prefixText) AND (DOC_APA.baja = '1900-01-01T00:00:00' OR DOC_APA.baja  IS NULL)",auxConn)
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
Public Function editctrl000004_Get (ByVal prefixText As String, ByVal count As Integer) As String()
Dim auxList As New List(Of String)
Dim auxConn As  SqlConnection=gConn_open
Dim auxDA As New SqlDataAdapter("SELECT  DISTINCT DOC_APA.abrev FROM DOC_APA   WHERE (abrev LIKE @prefixText) AND (DOC_APA.baja = '1900-01-01T00:00:00' OR DOC_APA.baja  IS NULL)",auxConn)
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
Public Function insctrl000005_Get (ByVal prefixText As String, ByVal count As Integer) As String()
Dim auxList As New List(Of String)
Dim auxConn As  SqlConnection=gConn_open
Dim auxDA As New SqlDataAdapter("SELECT  DISTINCT DOC_APA.dsc FROM DOC_APA   WHERE (dsc LIKE @prefixText) AND (DOC_APA.baja = '1900-01-01T00:00:00' OR DOC_APA.baja  IS NULL)",auxConn)
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
Public Function insctrl000004_Get (ByVal prefixText As String, ByVal count As Integer) As String()
Dim auxList As New List(Of String)
Dim auxConn As  SqlConnection=gConn_open
Dim auxDA As New SqlDataAdapter("SELECT  DISTINCT DOC_APA.abrev FROM DOC_APA   WHERE (abrev LIKE @prefixText) AND (DOC_APA.baja = '1900-01-01T00:00:00' OR DOC_APA.baja  IS NULL)",auxConn)
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

