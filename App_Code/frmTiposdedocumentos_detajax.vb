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
Public Class frmTiposdedocumentos_detajax
Inherits System.Web.Services.WebService
<ScriptMethod()> _ 
<WebMethod()> _ 
Public Function editctrl000029_Get (ByVal prefixText As String, ByVal count As Integer) As String()
Dim auxList As New List(Of String)
Dim auxConn As  SqlConnection=gConn_open
Dim auxDA As New SqlDataAdapter("SELECT  DISTINCT DOC_DOCTIP.dsc FROM DOC_DOCTIP   WHERE (dsc LIKE @prefixText) AND (DOC_DOCTIP.baja = 0 OR DOC_DOCTIP.baja  IS NULL)",auxConn)
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
Public Function editctrl000027_Get (ByVal prefixText As String, ByVal count As Integer) As String()
Dim auxList As New List(Of String)
Dim auxConn As  SqlConnection=gConn_open
Dim auxDA As New SqlDataAdapter("SELECT  DISTINCT DOC_DOCTIP.abrev FROM DOC_DOCTIP   WHERE (abrev LIKE @prefixText) AND (DOC_DOCTIP.baja = 0 OR DOC_DOCTIP.baja  IS NULL)",auxConn)
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
Public Function editctrl000025_Get (ByVal prefixText As String, ByVal count As Integer) As String()
Dim auxList As New List(Of String)
Dim auxConn As  SqlConnection=gConn_open
Dim auxDA As New SqlDataAdapter("SELECT  DISTINCT DOC_DOCTIP.formato FROM DOC_DOCTIP   WHERE (formato LIKE @prefixText) AND (DOC_DOCTIP.baja = 0 OR DOC_DOCTIP.baja  IS NULL)",auxConn)
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
Public Function editctrl000024_Get (ByVal prefixText As String, ByVal count As Integer) As String()
Dim auxList As New List(Of String)
Dim auxConn As  SqlConnection=gConn_open
Dim auxDA As New SqlDataAdapter("SELECT  DISTINCT DOC_DOCTIP.formatoespecifico FROM DOC_DOCTIP   WHERE (formatoespecifico LIKE @prefixText) AND (DOC_DOCTIP.baja = 0 OR DOC_DOCTIP.baja  IS NULL)",auxConn)
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
Public Function editctrl000020_Get (ByVal prefixText As String, ByVal count As Integer) As String()
Dim auxList As New List(Of String)
Dim auxConn As  SqlConnection=gConn_open
Dim auxDA As New SqlDataAdapter("SELECT  DISTINCT DOC_DOCTIP.templatefootleft FROM DOC_DOCTIP   WHERE (templatefootleft LIKE @prefixText) AND (DOC_DOCTIP.baja = 0 OR DOC_DOCTIP.baja  IS NULL)",auxConn)
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
Public Function editctrl000019_Get (ByVal prefixText As String, ByVal count As Integer) As String()
Dim auxList As New List(Of String)
Dim auxConn As  SqlConnection=gConn_open
Dim auxDA As New SqlDataAdapter("SELECT  DISTINCT DOC_DOCTIP.templatefootcenter FROM DOC_DOCTIP   WHERE (templatefootcenter LIKE @prefixText) AND (DOC_DOCTIP.baja = 0 OR DOC_DOCTIP.baja  IS NULL)",auxConn)
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
Public Function editctrl000018_Get (ByVal prefixText As String, ByVal count As Integer) As String()
Dim auxList As New List(Of String)
Dim auxConn As  SqlConnection=gConn_open
Dim auxDA As New SqlDataAdapter("SELECT  DISTINCT DOC_DOCTIP.templatefootright FROM DOC_DOCTIP   WHERE (templatefootright LIKE @prefixText) AND (DOC_DOCTIP.baja = 0 OR DOC_DOCTIP.baja  IS NULL)",auxConn)
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
Public Function insctrl000029_Get (ByVal prefixText As String, ByVal count As Integer) As String()
Dim auxList As New List(Of String)
Dim auxConn As  SqlConnection=gConn_open
Dim auxDA As New SqlDataAdapter("SELECT  DISTINCT DOC_DOCTIP.dsc FROM DOC_DOCTIP   WHERE (dsc LIKE @prefixText) AND (DOC_DOCTIP.baja = 0 OR DOC_DOCTIP.baja  IS NULL)",auxConn)
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
Public Function insctrl000027_Get (ByVal prefixText As String, ByVal count As Integer) As String()
Dim auxList As New List(Of String)
Dim auxConn As  SqlConnection=gConn_open
Dim auxDA As New SqlDataAdapter("SELECT  DISTINCT DOC_DOCTIP.abrev FROM DOC_DOCTIP   WHERE (abrev LIKE @prefixText) AND (DOC_DOCTIP.baja = 0 OR DOC_DOCTIP.baja  IS NULL)",auxConn)
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
Public Function insctrl000025_Get (ByVal prefixText As String, ByVal count As Integer) As String()
Dim auxList As New List(Of String)
Dim auxConn As  SqlConnection=gConn_open
Dim auxDA As New SqlDataAdapter("SELECT  DISTINCT DOC_DOCTIP.formato FROM DOC_DOCTIP   WHERE (formato LIKE @prefixText) AND (DOC_DOCTIP.baja = 0 OR DOC_DOCTIP.baja  IS NULL)",auxConn)
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
Public Function insctrl000024_Get (ByVal prefixText As String, ByVal count As Integer) As String()
Dim auxList As New List(Of String)
Dim auxConn As  SqlConnection=gConn_open
Dim auxDA As New SqlDataAdapter("SELECT  DISTINCT DOC_DOCTIP.formatoespecifico FROM DOC_DOCTIP   WHERE (formatoespecifico LIKE @prefixText) AND (DOC_DOCTIP.baja = 0 OR DOC_DOCTIP.baja  IS NULL)",auxConn)
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
Public Function insctrl000020_Get (ByVal prefixText As String, ByVal count As Integer) As String()
Dim auxList As New List(Of String)
Dim auxConn As  SqlConnection=gConn_open
Dim auxDA As New SqlDataAdapter("SELECT  DISTINCT DOC_DOCTIP.templatefootleft FROM DOC_DOCTIP   WHERE (templatefootleft LIKE @prefixText) AND (DOC_DOCTIP.baja = 0 OR DOC_DOCTIP.baja  IS NULL)",auxConn)
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
Public Function insctrl000019_Get (ByVal prefixText As String, ByVal count As Integer) As String()
Dim auxList As New List(Of String)
Dim auxConn As  SqlConnection=gConn_open
Dim auxDA As New SqlDataAdapter("SELECT  DISTINCT DOC_DOCTIP.templatefootcenter FROM DOC_DOCTIP   WHERE (templatefootcenter LIKE @prefixText) AND (DOC_DOCTIP.baja = 0 OR DOC_DOCTIP.baja  IS NULL)",auxConn)
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
Public Function insctrl000018_Get (ByVal prefixText As String, ByVal count As Integer) As String()
Dim auxList As New List(Of String)
Dim auxConn As  SqlConnection=gConn_open
Dim auxDA As New SqlDataAdapter("SELECT  DISTINCT DOC_DOCTIP.templatefootright FROM DOC_DOCTIP   WHERE (templatefootright LIKE @prefixText) AND (DOC_DOCTIP.baja = 0 OR DOC_DOCTIP.baja  IS NULL)",auxConn)
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

