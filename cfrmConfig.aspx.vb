Imports Intelimedia.imComponentes
Imports Intelimedia.Hercules.Communications
Imports clsCusimDOC
Imports System.Data
Partial Class cfrmConfig
    Inherits System.Web.UI.Page

    Protected Sub cmdSysReplicate_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdReplicate.Click
        Dim auxClass As New clsCusimDOC
        auxClass.gWS_ArriveMessage("REPLICATE")
        lblerror.Text = "Terminado:"
    End Sub

    Protected Sub cmdProcess_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdProceso.Click
        Page.Server.ScriptTimeout = 60 * 60 * 6
        Dim auxBagBalues As New clshrcBagValues
        Dim auxList As New List(Of Integer)
        Dim auxCod As Integer
        Dim auxClass As New clsCusimDOC
        auxClass.Conn.gConn_Open()
        Dim auxWhere As String = ""
        '25/11/2015 reorganizacion de SID.
        For Each auxRow As DataRow In auxClass.Conn.gConn_Query("SELECT cod,qsidcod" _
                                                               & " FROM DOC_DOC" _
                                                               & " WHERE COD > 0 " _
                                                               & auxWhere).Rows
            auxClass.gSys_DebugLogAdd("process:" & auxRow("cod"))
            auxClass.Sec.gSID_Reorganize(auxRow("qsidcod"))
        Next

        Exit Sub
        '25/11/2015 Los 442 documentos que están en “solicitud de nueva versión” pasarlos a “nueva versión” los documentos que no tengan firmas se informaran al Administrador.
        For Each auxRow As DataRow In auxClass.Conn.gConn_Query("SELECT COD" _
                                                               & " FROM DOC_DOC" _
                                                               & " WHERE COD > 0 " _
                                                               & " AND wfwstatus IN (" & enumWorkflowStep.coWFWSTPDOC_DOCSolicitudautomaticanuevaversion & "," _
                                                               & enumWorkflowStep.coWFWSTPDOC_DOCSolicitudnuevaversion & ")" _
                                                               & auxWhere).Rows
            auxList.Add(auxRow("cod"))
        Next

        For Each auxCod In auxList
            auxClass.Conn.gConn_Delete("DELETE FROM DOC_DOCSGN " _
                                       & " WHERE  doccod= " & auxCod _
                                       & " AND doclogcod in (SELECT cod from DOC_DOCLOG where wfwstepnext IN (" _
                                       & enumWorkflowStep.coWFWSTPDOC_DOCSolicitudautomaticanuevaversion _
                                       & "," & enumWorkflowStep.coWFWSTPDOC_DOCSolicitudnuevaversion _
                                       & "))")
            auxClass.gSys_DebugLogAdd("process:" & auxCod)
            auxBagBalues.gValue_Add("cod", auxCod)
            auxBagBalues.gValue_Add("empcod", "-1")
            auxBagBalues.gValue_Add("delempcod", "-1")
            auxBagBalues.gValue_Add("gotostep", enumWorkflowStep.coWFWSTPDOC_DOCnuevaversion)
            auxBagBalues.gValue_Add("jobqueuelevel", "1")
            auxBagBalues.gValue_Add("maildisabled", True)
            auxClass.gWorkflow_GotoStep_EXE(auxBagBalues, 1)
        Next
        auxClass.gSys_DebugLogAdd("process-end")
        Exit Sub
    End Sub
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        '   Dim auxClass As New clsCusimDOC
        '   Dim auxDocCod As Integer
        'For Each auxRow As DataRow In auxClass.Conn.gConn_Query("SELECT DISTINCT doccod FROM DOC_DOCLOG" _
        '                                                        & " WHERE fecha > '2014-06-01T00:00:00' and obs like'Cambio automáti%'").Rows
        '    auxDocCod = auxClass.Conn.gField_GetInt(auxRow("doccod"), -1)
        '    Dim auxValues As New clshrcBagValues()
        '    auxValues.gValue_Add("Cod", auxDocCod)
        '    auxValues.gValue_Add("gotostep", enumWorkflowStep.coWFWSTPDOC_DOCnuevaversionNOK)
        '    auxClass.gWorkflow_GotoStep_EXE(auxValues, 1)
        'Next
        If Session("Isadmin") = False Then
            Server.Transfer(hrcFormInitial & ".aspx")
        ElseIf Not IsPostBack Then
        End If

    End Sub

End Class
