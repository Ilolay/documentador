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
Public Class wssystem
<WebMethod()> _ 
Public Overridable Function gSystem_ExecuteScheduledTask() As String
  HttpContext.Current.Server.ScriptTimeout = 1800
  Dim auxClass As New clscusimDOC
  auxClass.gSystem_Init()
  If auxClass.DebugLogOn Then
      auxClass.gSys_DebugLogAdd("Execute tasks: conn=" & auxClass.Conn.LastErrorDescription)
  End If
  Dim auxreturn As String = auxClass.gSystem_ExecuteScheduledTask()
  auxClass.gSystem_End()
  Return auxReturn
End Function

<WebMethod()> _ 
Public Overridable Function gSystem_SendMessage(ByVal pPsw As String,ByVal pData As String) As String
  HttpContext.Current.Server.ScriptTimeout = 1800
  Dim auxClass As New clscusimDOC
  Dim auxIP As String =  HttpContext.Current.Request.ServerVariables("REMOTE_ADDR")
  auxClass.gSystem_Init()
  If auxClass.gWS_CheckPsw(pPsw, auxIP) Then
      If auxClass.DebugLogOn Then
          auxClass.gSys_DebugLogAdd("Execute tasks: conn=" & auxClass.Conn.LastErrorDescription)
      End If
      Dim auxReturn As String = auxClass.gWS_ArriveMessage(pData)
      auxClass.gSystem_End()
      Return auxReturn
  Else
      Return ""
  End If
End Function

End Class

