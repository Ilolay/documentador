Imports Intelimedia.imComponentes
Imports System.Data
Imports clsCusimDOC
Imports Intelimedia.Hercules.Language
Partial Class cfrmdocumentossgn_det
    Inherits System.Web.UI.Page
    Private m_IsAdmin As Boolean = False
    Friend m_mode As Short = 0
    Friend m_view As Short = 0
    Friend m_DocCod As Integer
    Friend m_WfwStateCod As Integer = -1
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim auxClient As New clsHrcCodeHTML
        Dim auxQueryString As clshrcBagValues = auxClient.gBagValues_GetFromQueryString(Request.QueryString.ToString)
        m_mode = Val(auxQueryString.gValue_Get("_mode_"))
        m_view = Val(auxQueryString.gValue_Get("_view_"))
        m_DocCod = Val(auxQueryString.gValue_Get("param1"))
        Call gData_Get()
       
        'End If
    End Sub
    Private Sub gData_Get()
    
        Dim auxSidCod As Integer = -1
        m_WfwStateCod = enumWorkflowStep.coWFWSTPDOC_DOCnuevaversion
        Select Case m_view
            Case 0
                Dim auxClass As New clsCusimDOC
                auxClass.Conn.gConn_Open()
                grdDOCSGN.DataSource = auxClass.Conn.gConn_Query("SELECT DOC_DOCSGN.*,EMP.dsc as empdsc," _
                                    & "(SELECT sysparamdsc FROM Q_SYSPARAM WHERE sysparamid=20101) as avisoscantmax" _
                                    & ",CASE WHEN DOC_DOCLOG.wfwstepnext =" & enumWorkflowStep.coWFWSTPDOC_DOCDocumentovigente & " THEN 'Lectura pendiente' ELSE Q_WFWSTP.wfwstpdsc END as wfwstpdsc " _
                                    & ",CASE WHEN DOC_DOC_HST.version IS NULL THEN DOC_DOC.version ELSE DOC_DOC_HST.version END as version" _
                                    & " FROM DOC_DOCSGN " _
                                    & " LEFT JOIN EMP ON DOC_DOCSGN.empcod=EMP.cod" _
                                    & " LEFT JOIN DOC_DOCLOG ON DOC_DOCSGN.doclogcod=DOC_DOCLOG.cod" _
                                    & " LEFT JOIN DOC_DOC_HST ON DOC_DOCLOG.hsthidgencod=DOC_DOC_HST.hstcod" _
                                    & " LEFT JOIN DOC_DOC ON DOC_DOCSGN.doccod=DOC_DOC.cod" _
                                    & " LEFT JOIN Q_WFWSTP ON DOC_DOCLOG.wfwstepnext = Q_WFWSTP.wfwstpcod" _
                                    & " WHERE DOC_DOCSGN.doccod = " & m_DocCod _
                                    & " ORDER BY DOC_DOCLOG.cod DESC, EMP.dsc")
                grdDOCSGN.DataBind()
                auxClass.Conn.gConn_Close()
            Case 1
                'Resumen de lectores y roles de acuerdo a los roles
                Dim auxClass As New clsCusimDOC
                auxClass.Conn.gConn_Open()
                Dim auxtrocod As Integer = auxClass.Conn.gConn_QueryValueInt("SELECT trocodcustom FROM DOC_DOC" _
                                                                           & " WHERE cod = " & m_DocCod)
                Dim auxDTroles As DataTable = auxClass.gTRO_Get(pTroCod:=auxtrocod)

                grdDOCSGN.DataSource = auxClass.Conn.gConn_Query("SELECT DOC_DOCSGN.*,EMP.dsc as empdsc," _
                                    & "(SELECT sysparamdsc FROM Q_SYSPARAM WHERE sysparamid=20101) as avisoscantmax" _
                                    & " FROM DOC_DOCSGN " _
                                    & " LEFT JOIN EMP ON DOC_DOCSGN.empcod=EMP.cod" _
                                    & " WHERE doccod = " & m_DocCod _
                                    & " ORDER BY EMP.dsc")
                auxClass.Conn.gConn_Close()
                grdDOCSGN.DataBind()
        End Select
        
    End Sub

    Protected Sub cmdFormViewItemCancel_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdFormViewItemCancel.Click
        If Request.QueryString("_closea_") = "1" Then
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "CloseWindow", "self.close();", True)
        Else
            Response.Redirect("cfrmdocumentos1_det.aspx?_mode_=0&_closea_=0&param1=" & Request.QueryString("param1"))
        End If
    End Sub

End Class



