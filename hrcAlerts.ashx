<%@ WebHandler Language="VB" Class="hrcAlertsHandler" %>
'Version 5
Imports System
Imports System.Web
Imports System.Data
Imports Intelimedia.Hercules.Language 
Imports Intelimedia.Hercules.Storage
Public Class hrcAlertsHandler
    Implements IHttpHandler
    Implements IRequiresSessionState
  
    Public Sub ProcessRequest(ByVal context As HttpContext) Implements IHttpHandler.ProcessRequest
       
        If context.Session IsNot Nothing Then
            If context.Session("hrcContext") IsNot Nothing Then
                Dim auxHrcContext As clsHrcJSContext = context.Session("hrcContext")
                Dim auxalertForm As New clsHrcJSAlertForm(auxHrcContext.Alert, auxHrcContext.Conn)
                If auxalertForm IsNot Nothing Then
                    Try
                        auxalertForm.gService_CommandResponse()
                    Catch ex As Exception

                    End Try
                End If
            End If
         
        End If
        
        
    End Sub
    Public ReadOnly Property IsReusable() As Boolean Implements IHttpHandler.IsReusable
        Get
            Return False
        End Get
    End Property
End Class