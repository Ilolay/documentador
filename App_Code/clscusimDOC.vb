Imports Intelimedia.Hercules.Storage.clsHrcConnClient
Imports Intelimedia.imComponentes
Imports System.Data
Imports System.Data.SqlClient
Imports clshrcimDOC
Imports clshrcimDOC.enumWorkflowStep
Imports Intelimedia.inTasks
Imports Intelimedia.Hercules.Language

Public Module basimDOC
    Public Enum enumDOC_Tipos As Short
        coAnexo = 1
        coLink = 2
        coHTML = 3
        coForm = 4
    End Enum
    Public Enum enumDOCTIP As Integer
        coPresupuesto = 1
        coDictamen = 2
        coInformeCierre = 3
        coSolucion = 4
        coTesting = 5
        coVarios = 6
        coRecepcion = 7
        coSolicitud = 8
    End Enum
    Public Enum enumRoles As Short
        coLector = 1
        coEditor = 2
        coRevisor = 3
        coAprobador = 4
        coPublicador = 5
        coCancelador = 6
        coImpresor = 7
        coVisualizador = 8
        coReceptor = 9 'Responsable de versiones
        'aprueban solicitudes de versiones. En el caso de Williner, son los aprobadores (jefe mantequilla).
    End Enum
    Public Enum enumUND_Roles As Short
        coMiembro = 1
        coMiembroDirecto = 2
        coResponsable = 3
        coSuperior = 4
        coEditorDocs = 5
    End Enum
    Public Enum enumIdentidadesEspeciales As Short
        'coRequirente = 1
        'coValidadorRequirente = 2
        'coFinalizadordeDesarrollo = 3
        'coDuenioProceso = 4
        coAprobador = 5
        coEditores = 6
    End Enum
    Public glPermRead As String = enumAccessType.coSYSGlobalLeer & "," _
                        & enumAccessType.coSYSGlobalEliminar & "," _
                        & enumAccessType.coSYSGlobalCambiarestado & "," _
                         & enumAccessType.coSYSGlobalModificar & "," _
                         & enumAccessType.coSYSCreador & ", " _
                         & enumAccessType.coSYSImprimircopiascontroladas & ", " _
                         & enumAccessType.coSYSImprimircopiasnocontroladas & "," _
                         & enumAccessType.coDOCDOCVIGDocumentosvigentesVer & "," _
                         & enumAccessType.coSYSConfirmarlectura

    'coSYSConfirmarlectura :lo tienen los lectores con firmas pendientes (antes coDOCDOCDocumentosVer)
    'coSYSGlobalLeer: visualizadores
    'coSYSGlobalModificar: se utiliza para los editores-generales y administradores
    'coSYSConfirmarlectura: lo tienen los lectores con firmas pendientes
    'coDOCDOCVIGDocumentosvigentesVer: lo tienen los lectores con firmas pendientes
    'Public glPermCambiarEstado As String = enumAccessType.coSYSGlobalCambiarpermisos & "," _
    '                    & enumAccessType.coSYSGlobalEliminar & "," _
    '                     & enumAccessType.coDOCDOCDocumentosVer
    Public Const coCopiaControladaTexto As String = "Copia controlada"
    Public Const coCopiaNoControladaTexto As String = "Copia no controlada"
    Friend m_genPsw As String = "EncendidoAlternativo"
    Public Enum enumDTCache As Integer
        coDOCPROUserSelection_APA = 1
        coUNDHierarchy = 2
        coCLAHierarchy = 3
    End Enum
    Public Enum enumWfwMode As Short
        coStandard = 1
        coReID = 2
        coUserCreate = 3
    End Enum
    Public hrcReplicationServer As clsHrcReplicationClient
    Public hrcDT_Cache As New SortedList(Of enumDTCache, DataTable)

    Public Const coSysParamIDAvisosCantMax As Integer = 20101
    Public Const coSysParamIDAvisosDias As Integer = 20102
    Public Const coSysParamEprDsc As Integer = 20104
    Public Const coSysParamCheckDuplicateTitle As Integer = 20105
    Public Const coSysParamInboxDetailedEnabled As Integer = 20106
    Public Const coSysParamIDModoObligatorioSoloVigentes As Integer = 20107
    Public Const coSysParamIDAprobadorNoLee As Integer = 20108
    Public Const coSysParamIDModoObligatorioAdmins As Integer = 20109
    Public Const coSysParamIDModoObligatorioEditors As Integer = 20110
    Public Const coSysParamIDModoObligatorio As Integer = 20111
    Public Const coSysParamIDPublicQueue As Integer = 20001

    Public Const coSysParamEmpShowImage As Integer = 200112
    Public Const coSysParamIDObsVisible As Integer = 200113
    Public Const coSysParamIDPrnCfgCodDefault As Integer = 200114
    Public Const coSysParamIDDOCReIDAuto As Integer = 200115
    Public Const coSysParamIDDOCViewDefault As Integer = 200116

    Public Const coSysParamIDReqURL As Integer = 200117

    Public Const coSysParamIDPrnCopyBackImageDisabled As Integer = 200118
    Public Const coSysParamIDPrnOnlineBackImageDisabled As Integer = 200119

    Public Const coSysParamIDDOCAutoEditionDaysCicle As Integer = 200120
    Public Const coSysParamIDDOCNroEditEnabled As Integer = 200121

    Public Const coSysParamRepliDSCore As Integer = 200122
    Public Const coSysParamRepliEQU As Integer = 200123

    Public Const coSysParamIDDOCDscxVisible As Integer = 200124
    Public Const coSysParamIDDOCSGNUpdateEditionRoleDaily As Integer = 200125

    Public Const coSysParamIDUNDCreatorsMBRDIRDefault As Integer = 200126   'Este parámetro indica si los miembros directos de una unidad son editores de documentos de la unidad

    Public Const coSysParamIDDOCAllEditoresRequired As Integer = 200127   'Se requieren todos los editores
    Public Const coSysParamIDDOCAllRevisorRequired As Integer = 200128   'Se requieren todos los revisores
    Public Const coSysParamIDDOCAllAprobadorRequired As Integer = 200129   'Se requieren todos los aprobadores
    Public Const coSysParamIDDOCAllPublicadorRequired As Integer = 200130   'Se requieren todos los publicadores

    Public Const coSysParamIDDOCDeleteTotalEnabled As Integer = 200131    'Eliminacion total habilitada
    Public Const coGroupDocumentadorAdministradores As Integer = 101
    Public Const coGroupDocumentadorEditores As Integer = 102
    Public Const coGroupDocumentadorVisualizadores As Integer = 103

    Public Const coSysParamIDDocAutoEditionAlertSubject As Integer = 200132

    Public Const coSysParamIDDocVersionadorEditoresDefault As Integer = 200133
    Public Const coSysParamIDDocVersionadorAprobadoresDefault As Integer = 200134


    Public Const coSysParamIDPrnMarginLeftMM As Integer = 200135
    Public Const coSysParamIDPrnMarginRightMM As Integer = 200136
    Public Const coSysParamIDPrnMarginTopMM As Integer = 200137
    Public Const coSysParamIDPrnMarginBottomMM As Integer = 200138

    Public Const coSysParamIDDocContentTypeIDDefault As Integer = 200139

    Public Const coSysParamIDDocAllUsersCanAddDocuments As Integer = 200140

    Public Const coSysParamIDTRODefault As Integer = 200141
    Public Const coSysParamIDEQUReceptores As Integer = 200142
    Public Const coSysParamIDDOCNroAssignAtCreation As Integer = 200143 'Asignar el numero al crear el documento

    Public Const coSysParamIDDOCEspecificoA_UndResp As Integer = 200144   'Los responsables de unidad se incluyen en especifico a

    Friend Sub gAlerts_Raise(ByVal pAlertsDT As DataTable, _
                             ByVal pQueuesDT As System.Data.DataTable)
        hrcAlerts.gDebug_Add("Alertas de mensajes a enviar")

        Try
            'Envío de mails
            If pQueuesDT.Rows.Count = 0 Then
                hrcAlerts.gDebug_Add("No hay colas de envío")
            Else

                Dim auxConn As clsHrcConnClient = hrcSystemConn.gComponent_CreateInstance
                Dim auxMail As New imMailing(coSMTPServer, coSMTPUser, coSMTPpsw, coSMTPfrom, Val(coSMTPport), coSystemTitle)
                auxMail.SMTPenableSSL = auxConn.gField_GetBoolean(coSMTPSSLEnabled, False)

                Dim auxSiteURL As String = auxConn.gField_GetString(coExternalURL)
                If auxSiteURL = "" Then
                    auxSiteURL = VirtualPathUtility.GetDirectory(HttpRuntime.AppDomainAppVirtualPath)
                End If
                If Right(auxSiteURL, 1) <> "/" Then
                    auxSiteURL &= "/"
                End If
                Dim auxMailDisabled As Boolean
                Dim auxMsgOptions As clshrcBagValues
                Dim auxMailsTo As String = ""
                Dim auxRowEmp As DataRow
                For Each auxRow As DataRow In pQueuesDT.Rows
                    auxMailsTo = ""
                    'Limpia los destinatarios
                    auxMail.gMail_NewMail()
                    Select Case CType(auxRow("alequeentityid"), enumEntities)
                        Case enumEntities.coEntityEMP
                            auxRowEmp = hrcEntityDT_EMP_FindByKey(auxRow("alequecodid"))
                            If auxRowEmp Is Nothing Then
                                hrcAlerts.gDebug_Add("Envío mail-Imposible enviar a entidad:" & auxRow("alequeentityid") & ".cod:" & auxRow("alequecodid") & "." & auxConn.LastErrorDescription)
                            Else
                                Dim auxMailTo As String = auxConn.gField_GetString(auxRowEmp("mail"))
                                If auxMailTo = "" Then
                                    hrcAlerts.gDebug_Add("Envío mail-Error al agregar a entidad(dirección en blanco)." & auxRow("alequeentityid") & ".cod:" & auxRow("alequecodid") & "." & auxConn.LastErrorDescription)
                                Else
                                    If auxMail.gTo_Add(auxMailTo) Then
                                        auxMailsTo &= auxMailTo & ";"
                                        '   gTRACE_add(pCod, 1, "Nuevo mail de destino [" & auxMailTo & "].seccod[" & auxRow(0) & "]")
                                        'gSys_DebugLogAdd("de destino [" & auxMailTo & "]" & auxRow("alequeentityid") & ".cod:" & auxRow("alequecodid") & "." & auxConn.LastErrorDescription)
                                    Else
                                        hrcAlerts.gDebug_Add("Envío mail-Error al agregar a entidad(dirección inválida)." & auxMailTo & "." & auxRow("alequeentityid") & ".cod:" & auxRow("alequecodid") & "." & auxConn.LastErrorDescription)
                                        '  gTRACE_add(pCod, 1, "Dirección de mail inválida o ya fue agregada [" & auxMailTo & "].seccod[" & auxRow(0) & "]")
                                    End If

                                End If
                            End If
                        Case Else
                            'Case enumEntities.coEntityUND
                            hrcAlerts.gDebug_Add("Envío mail-Error al agregar entidad: " & auxRow("alequeentityid") & ".cod:" & auxRow("alequecodid") & "." & auxMail.LastErrorDescription)
                            'Case enumEntities.coEntityREQ_EQU
                    End Select
                    For Each auxRowAlert As DataRow In pAlertsDT.Rows
                        auxMsgOptions = New clshrcBagValues(auxRowAlert("alemsgoptions").ToString)
                        auxMailDisabled = auxConn.gField_GetBoolean(auxMsgOptions.gValue_Get("MAILDISABLED"))
                        hrcAlerts.gDebug_Add("Alertas de mensajes a enviar.MailDisabled:" & auxMailDisabled)
                        If auxMailDisabled = False Then
                            hrcAlerts.gDebug_Add("Enviando alerta:" & auxRowAlert("alemsgcod") & "-cat:" & auxRowAlert("alemsgcatcod"))
                            Select Case auxConn.gField_GetInt(auxRowAlert("alemsgcatcod"), -1)
                                Case 1
                                Case Else
                                    hrcAlerts.gDebug_Add("Enviando alerta:" & auxRowAlert("alemsgcod"))

                                    Dim auxFrom As String = ""
                                    Select Case CType(auxRowAlert("alequesrcentityid"), enumEntities)
                                        Case enumEntities.coEntityEMP
                                            auxRowEmp = hrcEntityDT_EMP_FindByKey(auxRowAlert("alequesrccodid"))
                                            If auxRowEmp IsNot Nothing Then
                                                auxFrom = auxRowEmp("dsc").ToString
                                            End If
                                        Case Else
                                    End Select

                                    Dim auxContent As String = "{#MAIL.TEXT#}" 'hrcEntityDT_MAILS_FindByKey(1)("content")
                                    Dim auxAleMsgObs As String = HttpUtility.HtmlDecode(auxRowAlert("alemsgobs"))
                                    auxContent = auxContent.Replace("{#MAIL.TEXT#}", auxAleMsgObs)
                                    auxContent = auxContent.Replace("{#TEXT#}", auxAleMsgObs)
                                    auxContent = auxContent.Replace("{#SYSTEM.TITLE#}", coSystemTitle)
                                    auxContent = auxContent.Replace("{#SYSTEM.SITEURL#}", auxSiteURL)
                                    auxContent = auxContent.Replace("{#ALERTS.FROM#}", auxFrom)

                                    Dim auxSubject As String = auxRowAlert("alemsgdsc")
                                    If auxSubject = "" Then
                                        auxSubject = "{#SYSTEM.TITLE#}-{#ALERTS.FROM#}"
                                    End If
                                    auxSubject = auxSubject.Replace("{#MAIL.TEXT#}", auxAleMsgObs)
                                    auxSubject = auxSubject.Replace("{#TEXT#}", auxAleMsgObs)
                                    auxSubject = auxSubject.Replace("{#SYSTEM.TITLE#}", coSystemTitle)
                                    auxSubject = auxSubject.Replace("{#SYSTEM.SITEURL#}", auxSiteURL)
                                    auxSubject = auxSubject.Replace("{#ALERTS.FROM#}", auxFrom)
                                    If auxMail.gMail_Send(auxSubject, auxContent) Then
                                        'gTRACE_add(pCod, 1, "Mail enviados!!!." & auxMail.LastErrorDescription)
                                        hrcAlerts.gDebug_Add("Mail enviado a:" & auxMailsTo)
                                    Else
                                        hrcAlerts.gDebug_Add("Error al enviar mail(" & auxMailsTo & ")." & auxMail.LastErrorDescription & "." & auxContent)
                                    End If
                            End Select
                        End If


                    Next
                Next
                auxMail.gEnd()
                auxConn.gConn_Close()
                auxConn = Nothing
            End If
        Catch ex As Exception
            hrcAlerts.gDebug_Add("Excepción envío de mail:" & ex.Message & "." & ex.StackTrace)
        End Try

    End Sub
    Public Function gQuery_DOCMBR_GetWidthDraft_bak(ByVal pDraftID As String, _
                                                ByVal pDocCod As Integer) As String
        Dim auxWhere As String = ""
        Return "SELECT DOC_DOCMBR.rolcod," _
                            & "DOC_ROL.dsc as DOC_DOCMBRroldsc,DOC_ROL.orden as orden," _
                            & " DOC_DOCMBR.docmbrcod, DOC_DOCMBR.doccod, DOC_DOCMBR.docmbrtype, UND.dsc AS unddsc, EMP.dsc AS empdsc, EQU.dsc AS equdsc, " _
                            & " CASE DOC_DOCMBR.docmbrtype WHEN " & enumEntities.coEntityEMP & " THEN EMP.dsc WHEN " & enumEntities.coEntityUND & " THEN UND.dsc + '(' + UNDROL.dsc + ')' WHEN " & enumEntities.coEntityDOC_EQU & " THEN EQU.dsc ELSE '' END AS DOC_DOCMBRdsc, " _
                            & " CASE DOC_DOCMBR.docmbrtype WHEN " & enumEntities.coEntityEMP & " THEN EMPUND.dsc WHEN " & enumEntities.coEntityUND & " THEN UNDUND.dsc WHEN " & enumEntities.coEntityDOC_EQU & " THEN EQUUND.dsc ELSE '' END AS DOC_DOCMBRareadsc, " _
                            & " CASE DOC_DOCMBR.docmbrtype WHEN " & enumEntities.coEntityEMP & " THEN EMP.cod WHEN " & enumEntities.coEntityUND & " THEN UND.cod WHEN " & enumEntities.coEntityDOC_EQU & " THEN EQU.cod ELSE '' END AS DOC_DOCMBRobjectcod," _
                            & " CASE DOC_DOCMBR.docmbrtype " _
                            & " WHEN " & enumEntities.coEntityEMP & " THEN EMP.seccod " _
                            & " WHEN " & enumEntities.coEntityUND & " THEN (CASE  DOC_DOCMBRUND.rolunidad " _
                                    & " WHEN 1 THEN UND.grpcodresp" _
                                    & " WHEN 2 THEN UND.miembrosgrpcod" _
                                    & " WHEN 3 THEN UND.grpcodeditor" _
                                    & " END )" _
                            & " WHEN " & enumEntities.coEntityDOC_EQU & " THEN EQU.miembrosgrpcod ELSE '' " _
                            & " END AS membersidcod, " _
                            & " EQUUND.dsc AS equunddsc, EMPUND.dsc AS empunddsc, UNDUND.dsc AS undunddsc " _
                            & " FROM DOC_DOCMBR " _
                            & " LEFT JOIN DOC_ROL ON DOC_DOCMBR.rolcod =DOC_ROL.cod" _
                            & " LEFT JOIN DOC_DOCMBRUND ON DOC_DOCMBRUND.docmbrcod = DOC_DOCMBR.docmbrcod " _
                            & " LEFT JOIN UNDROL ON DOC_DOCMBRUND.rolunidad = UNDROL.cod " _
                            & " LEFT JOIN UND AS UND ON DOC_DOCMBRUND.undcod = UND.cod " _
                            & " LEFT JOIN UND AS UNDUND ON UND.undcodsup = UNDUND.cod " _
                            & " LEFT JOIN DOC_DOCMBRUSU ON DOC_DOCMBRUSU.docmbrcod = DOC_DOCMBR.docmbrcod " _
                            & " LEFT JOIN EMP ON DOC_DOCMBRUSU.percod = EMP.cod " _
                            & " LEFT JOIN UND AS EMPUND ON EMP.undcod = EMPUND.cod" _
                            & " LEFT JOIN DOC_DOCMBREQU ON DOC_DOCMBREQU.docmbrcod = DOC_DOCMBR.docmbrcod " _
                            & " LEFT JOIN DOC_EQU AS EQU ON DOC_DOCMBREQU.equcod = EQU.cod" _
                            & " LEFT JOIN UND AS EQUUND ON EQU.undcod = EQUUND.cod" _
                            & " WHERE  (DOC_DOCMBR.docmbrcod =-1) OR (DOC_DOCMBR.doccod = " & pDocCod _
                            & " AND DOC_DOCMBR.docmbrcod NOT IN (SELECT qdft_docmbrcod FROM DOC_DOCMBR_DFTREL WHERE dftdidgencod='" & pDraftID & "'))" _
                            & " UNION " _
                            & " (SELECT DOC_DOCMBR_DFT.rolcod," _
                            & "DOC_ROL.dsc as DOC_DOCMBRroldsc,DOC_ROL.orden as orden," _
                            & " DOC_DOCMBR_DFT.docmbrcod, DOC_DOCMBR_DFT.doccod, DOC_DOCMBR_DFT.docmbrtype, UND_DFT.dsc AS unddsc, EMP_DFT.dsc AS empdsc, EQU_DFT.dsc AS equdsc, " _
                            & " CASE DOC_DOCMBR_DFT.docmbrtype WHEN " & enumEntities.coEntityEMP & " THEN EMP_DFT.dsc WHEN " & enumEntities.coEntityUND & " THEN UND_DFT.dsc +'(' + UNDROL.dsc + ')' WHEN " & enumEntities.coEntityDOC_EQU & " THEN EQU_DFT.dsc ELSE '' END AS DOC_DOCMBRdsc, " _
                            & " CASE DOC_DOCMBR_DFT.docmbrtype WHEN " & enumEntities.coEntityEMP & " THEN EMPUND_DFT.dsc WHEN " & enumEntities.coEntityUND & " THEN UNDUND_DFT.dsc WHEN " & enumEntities.coEntityDOC_EQU & " THEN EQUUND_DFT.dsc ELSE '' END AS DOC_DOCMBRareadsc, " _
                            & " CASE DOC_DOCMBR_DFT.docmbrtype WHEN " & enumEntities.coEntityEMP & " THEN EMP_DFT.cod WHEN " & enumEntities.coEntityUND & " THEN UND_DFT.cod WHEN " & enumEntities.coEntityDOC_EQU & " THEN EQU_DFT.cod ELSE '' END AS DOC_DOCMBRobjectcod," _
                            & " CASE DOC_DOCMBR_DFT.docmbrtype " _
                            & " WHEN " & enumEntities.coEntityEMP & " THEN EMP_DFT.seccod " _
                            & " WHEN " & enumEntities.coEntityUND & " THEN (CASE  DOC_DOCMBRUND_DFT.rolunidad " _
                                    & " WHEN 1 THEN UND_DFT.grpcodresp" _
                                    & " WHEN 2 THEN UND_DFT.miembrosgrpcod" _
                                    & " WHEN 3 THEN UND_DFT.grpcodeditor" _
                                    & " END )" _
                            & " WHEN " & enumEntities.coEntityDOC_EQU & " THEN EQU_DFT.miembrosgrpcod ELSE '' " _
                            & " END AS membersidcod, " _
                            & " EQUUND_DFT.dsc AS equunddsc, EMPUND_DFT.dsc AS empunddsc, UNDUND_DFT.dsc AS undunddsc " _
                            & " FROM DOC_DOCMBR_DFT " _
                            & " LEFT JOIN DOC_ROL ON DOC_DOCMBR_DFT.rolcod =DOC_ROL.cod" _
                            & " LEFT JOIN DOC_DOCMBRUND_DFT ON DOC_DOCMBRUND_DFT.docmbrcod = DOC_DOCMBR_DFT.docmbrcod AND DOC_DOCMBR_DFT.dftdidgencod=DOC_DOCMBRUND_DFT.dftdidgencod" _
                            & " LEFT JOIN UND AS UND_DFT ON DOC_DOCMBRUND_DFT.undcod = UND_DFT.cod " _
                            & " LEFT JOIN UND AS UNDUND_DFT ON UND_DFT.undcodsup = UNDUND_DFT.cod  " _
                            & " LEFT JOIN UNDROL ON DOC_DOCMBRUND_DFT.rolunidad = UNDROL.cod " _
                            & " LEFT JOIN DOC_DOCMBRUSU_DFT ON DOC_DOCMBRUSU_DFT.docmbrcod = DOC_DOCMBR_DFT.docmbrcod AND DOC_DOCMBR_DFT.dftdidgencod=DOC_DOCMBRUSU_DFT.dftdidgencod" _
                            & " LEFT JOIN EMP AS EMP_DFT ON DOC_DOCMBRUSU_DFT.percod = EMP_DFT.cod " _
                            & " LEFT JOIN UND AS EMPUND_DFT ON EMP_DFT.undcod = EMPUND_DFT.cod" _
                            & " LEFT JOIN DOC_DOCMBREQU_DFT ON DOC_DOCMBREQU_DFT.docmbrcod = DOC_DOCMBR_DFT.docmbrcod AND DOC_DOCMBR_DFT.dftdidgencod=DOC_DOCMBREQU_DFT.dftdidgencod" _
                            & " LEFT JOIN DOC_EQU AS EQU_DFT ON DOC_DOCMBREQU_DFT.equcod = EQU_DFT.cod" _
                            & " LEFT JOIN UND AS EQUUND_DFT ON EQU_DFT.undcod = EQUUND_DFT.cod" _
                            & " WHERE (DOC_DOCMBR_DFT.doccod = " & pDocCod & " AND DOC_DOCMBR_DFT.dftdidgencod='" & pDraftID & "')) " _
                            & " ORDER BY orden,DOC_DOCMBRdsc"
    End Function

    Public Function gQuery_DOCMBR_Get_bak(ByVal pDocCod As Integer, _
                                      ByVal pHstGenCod As Integer, _
                                      ByVal pRol As enumRoles) As String
        Dim auxFrom As String = ""
        Dim auxFromUSU As String = ""
        Dim auxFromUND As String = ""
        Dim auxFromEQU As String = ""
        Dim auxJoinUSU As String = ""
        Dim auxJoinUND As String = ""
        Dim auxJoinEQU As String = ""
        Dim auxWhere As String = ""
        If pHstGenCod > 0 Then
            auxFrom = "DOC_DOCMBR_HST AS"
            auxFromUND = "DOC_DOCMBRUND_HST AS "
            auxFromUSU = "DOC_DOCMBRUSU_HST AS "
            auxFromEQU = "DOC_DOCMBREQU_HST AS "
            auxJoinUND = " AND DOC_DOCMBRUND.hsthidgencod = DOC_DOCMBR.hsthidgencod "
            auxJoinUSU = " AND DOC_DOCMBRUSU.hsthidgencod = DOC_DOCMBR.hsthidgencod "
            auxJoinEQU = " AND DOC_DOCMBREQU.hsthidgencod = DOC_DOCMBR.hsthidgencod "
            auxWhere = " OR ((DOC_DOCMBR.hsthidgencod IS NULL OR DOC_DOCMBR.hsthidgencod =" & pHstGenCod & ") " _
                & " AND (DOC_DOCMBRUSU.hsthidgencod IS NULL OR DOC_DOCMBRUSU.hsthidgencod =" & pHstGenCod & ") " _
                & " AND (DOC_DOCMBRUND.hsthidgencod IS NULL OR DOC_DOCMBRUND.hsthidgencod =" & pHstGenCod & ") " _
                & " AND (DOC_DOCMBREQU.hsthidgencod IS NULL OR DOC_DOCMBREQU.hsthidgencod =" & pHstGenCod & ")) "
        End If

        If pRol > 0 Then
            auxWhere &= " AND DOC_DOCMBR.rolcod =" & pRol
        End If
        Return "SELECT DOC_DOCMBR.rolcod," _
                & " DOC_ROL.dsc as DOC_DOCMBRroldsc,DOC_ROL.orden as orden," _
                & " DOC_DOCMBR.docmbrcod, DOC_DOCMBR.doccod, DOC_DOCMBR.docmbrtype, " _
                & " CASE DOC_DOCMBR.docmbrtype WHEN " & enumEntities.coEntityEMP & " THEN EMP.dsc WHEN " & enumEntities.coEntityUND & " THEN UND.dsc + '(' + UNDROL.dsc + ')' WHEN " & enumEntities.coEntityDOC_EQU & " THEN EQU.dsc ELSE '' END AS DOC_DOCMBRdsc, " _
                & " CASE DOC_DOCMBR.docmbrtype WHEN " & enumEntities.coEntityEMP & " THEN EMPUND.dsc WHEN " & enumEntities.coEntityUND & " THEN UNDUND.dsc WHEN " & enumEntities.coEntityDOC_EQU & " THEN EQUUND.dsc ELSE '' END AS DOC_DOCMBRareadsc, " _
                & " CASE DOC_DOCMBR.docmbrtype " _
                            & " WHEN " & enumEntities.coEntityEMP & " THEN EMP.seccod " _
                            & " WHEN " & enumEntities.coEntityUND & " THEN (CASE  DOC_DOCMBRUND.rolunidad " _
                                    & " WHEN 1 THEN UND.grpcodresp" _
                                    & " WHEN 2 THEN UND.miembrosgrpcod" _
                                    & " WHEN 3 THEN UND.grpcodeditor" _
                                    & " END )" _
                            & " WHEN " & enumEntities.coEntityDOC_EQU & " THEN EQU.miembrosgrpcod ELSE '' " _
                            & " END AS membersidcod, " _
                & " CASE DOC_DOCMBR.docmbrtype WHEN " & enumEntities.coEntityEMP & " THEN EMP.cod WHEN " & enumEntities.coEntityUND & " THEN UND.cod WHEN " & enumEntities.coEntityDOC_EQU & " THEN EQU.cod ELSE '' END AS DOC_DOCMBRobjectcod," _
                & " EQUUND.dsc AS equunddsc, EMPUND.dsc AS empunddsc, UNDUND.dsc AS undunddsc, " _
                & " UND.dsc AS unddsc, EMP.dsc AS empdsc, EQU.dsc AS equdsc " _
                & " FROM " & auxFrom & " DOC_DOCMBR " _
                & " LEFT JOIN DOC_ROL ON DOC_DOCMBR.rolcod =DOC_ROL.cod" _
                & " LEFT JOIN " & auxFromUND & " DOC_DOCMBRUND ON DOC_DOCMBRUND.docmbrcod = DOC_DOCMBR.docmbrcod " & auxJoinUND _
                & " LEFT JOIN UND AS UND ON DOC_DOCMBRUND.undcod = UND.cod " _
                & " LEFT JOIN UND AS UNDUND ON UND.undcodsup = UNDUND.cod " _
                & " LEFT JOIN UNDROL ON DOC_DOCMBRUND.rolunidad = UNDROL.cod " _
                & " LEFT JOIN " & auxFromUSU & "DOC_DOCMBRUSU ON DOC_DOCMBRUSU.docmbrcod = DOC_DOCMBR.docmbrcod " & auxJoinUSU _
                & " LEFT JOIN EMP ON DOC_DOCMBRUSU.percod = EMP.cod " _
                & " LEFT JOIN UND AS EMPUND ON EMP.undcod = EMPUND.cod" _
                & " LEFT JOIN " & auxFromEQU & "DOC_DOCMBREQU ON DOC_DOCMBREQU.docmbrcod = DOC_DOCMBR.docmbrcod " & auxJoinEQU _
                & " LEFT JOIN DOC_EQU AS EQU ON DOC_DOCMBREQU.equcod = EQU.cod" _
                & " LEFT JOIN UND AS EQUUND ON EQU.undcod = EQUUND.cod" _
                & " WHERE (DOC_DOCMBR.doccod = " & pDocCod & ")" _
                & auxWhere _
                & " ORDER BY orden,DOC_DOCMBRdsc"
    End Function

End Module
Public Class clsCusimDOC
    Inherits clshrcimDOC





    'Public Enum enumUnidadesRoles As Short
    '    coResponsable = 1
    '    coMiembro = 2
    '    coEditorDocs = 3
    'End Enum
    Public m_mailDisabled As Boolean = False
    Public Function gEntity_DOC_DOCVIG_Copy(ByVal pCod As Integer) As Boolean
        Dim auxReturn As Boolean = False
        m_Conn.gConn_ExecuteProcedureQuery("DELETE FROM DOC_DOCVIG WHERE cod =" & pCod & " DELETE FROM DOC_DOCANX_VIG WHERE doccod =" & pCod _
                      & " INSERT INTO DOC_DOCVIG (cod,dsc,dsc0,dsc1,dsc2,nro,fecha,siscod,clacod,version,obs,wfwstatus,doctipcod,procod,cuerpo,undcod,eprcod,orden,identificador,docsupcod,archivo,contenttypeid,prncfgcod,qsecsid,qsecdatetime,qsidcod)  (SELECT cod,dsc,dsc0,dsc1,dsc2,nro,fecha,siscod,clacod,version,obs,wfwstatus,doctipcod,procod,cuerpo,undcod,eprcod,orden,identificador,docsupcod,archivo,contenttypeid,prncfgcod,qsecsid,qsecdatetime,qsidcod FROM DOC_DOC WHERE cod=" & pCod & ") " _
                      & " INSERT INTO DOC_DOCANX_VIG (cod,dsc,doccod,doc,doctipcod) (SELECT cod,dsc,doccod,doc,doctipcod FROM DOC_DOCANX WHERE doccod =" & pCod & ")")
        Return auxReturn
    End Function
    Public Sub gEntity_DOC_UpdateChilds(ByVal pCod As Integer)
        If pCod > 0 Then
            ' Dim auxParentValues As clshrcBagValues
            Dim auxEntities As New List(Of enumEntities)
            auxEntities.Add(enumEntities.coEntityDOC_DOC)
            auxEntities.Add(enumEntities.coEntityDOC_DOCVIG)
            Dim auxTroCod As Integer = -1
            Dim auxDocSupCod As Integer = -1
            Dim auxProCod As Integer = -1
            Dim auxSelect As String = ""
            Dim auxDTParent As DataTable = m_Conn.gConn_Query("SELECT procod,docsupcod,trocodcustomenabled,trocodcustom,trocod " _
                                                                     & " FROM DOC_DOC " _
                                                                     & " WHERE cod=" & pCod)
            If auxDTParent.Rows.Count <> 0 Then
                ' auxParentValues = m_Conn.gField_GetBagValuesFromArray(auxDTParent.Rows(0).ItemArray, auxDTParent.Columns)
                auxTroCod = m_Conn.gField_GetInt(auxDTParent.Rows(0)("trocodcustom"), -1)
                auxProCod = m_Conn.gField_GetInt(auxDTParent.Rows(0)("procod"), -1)
                auxDocSupCod = m_Conn.gField_GetInt(auxDTParent.Rows(0)("docsupcod"), -1)
                If auxDocSupCod > 0 Then
                    If m_Conn.gField_GetBoolean(auxDTParent.Rows(0)("trocodcustomenabled"), False) = False Then
                        'hereda del superior
                        auxTroCod = m_Conn.gConn_QueryValueInt("SELECT trocod FROM DOC_DOC " _
                                                                & " WHERE cod =" & auxDocSupCod, -1)
                    End If
                End If
                'Actualiza la plantilla del documento padre
                gEntity_DOC_DOC_Update(pcod:=pCod, ptrocod:=auxTroCod)

                Dim auxTroCodNewValue As Object
                For Each auxEntity As enumEntities In auxEntities
                    Select Case auxEntity
                        Case enumEntities.coEntityDOC_DOC
                            auxSelect = "SELECT cod,trocodcustomenabled FROM DOC_DOC " _
                                        & " WHERE docsupcod=" & pCod _
                                        & " AND (baja = {#FALSE#} OR baja {#ISNULL#})"
                        Case enumEntities.coEntityDOC_DOCVIG
                            auxSelect = "SELECT cod FROM DOC_DOCVIG " _
                                        & " WHERE docsupcod=" & pCod
                    End Select
                    For Each auxRow As DataRow In m_Conn.gConn_Query(auxSelect).Rows
                        Select Case auxEntity
                            Case enumEntities.coEntityDOC_DOC
                                auxTroCodNewValue = Nothing
                                If m_Conn.gField_GetBoolean(auxRow("trocodcustomenabled"), False) = False Then
                                    'hereda del superior
                                    auxTroCodNewValue = auxTroCod
                                End If
                                gEntity_DOC_DOC_Update(pcod:=auxRow("cod"), _
                                              pprocod:=auxProCod, _
                                              ptrocod:=auxTroCodNewValue)
                            Case enumEntities.coEntityDOC_DOCVIG
                                gEntity_DOC_DOCVIG_Update(pcod:=auxRow("cod"), _
                                              pprocod:=auxProCod)
                        End Select
                    Next
                Next


                'Actualiza los sub-documentos que heredan permisos y los actualiza
                Dim auxStack As New Stack(Of Integer)
                Dim auxListed As New List(Of Integer)
                auxStack.Push(pCod)
                Do While auxStack.Count <> 0
                    auxDocSupCod = auxStack.Pop
                    If auxListed.IndexOf(auxDocSupCod) = -1 Then
                        auxListed.Add(auxDocSupCod)
                        For Each auxSubDoc As DataRow In m_Conn.gConn_Query("SELECT cod FROM DOC_DOC " _
                                                                    & " WHERE docsupcod =" & auxDocSupCod _
                                                                    & " AND (trocodcustomenabled {#ISNULL#} OR trocodcustomenabled ={#FALSE#})").Rows
                            gEntity_DOC_DOC_SystemUpdate(pcod:=auxSubDoc("COD"), ptrocod:=auxTroCod)
                            auxStack.Push(auxSubDoc("COD"))
                        Next
                    End If
                Loop
            End If
            
        End If
    End Sub
    Public Function gQuery_TRO_GetWidthDraft(ByVal pDraftID As String, _
                                             ByVal pTroCod As Integer, _
                                             Optional pWhere As String = "") As String
        Dim auxReturn As String = ""

        auxReturn = "SELECT DOC_TROROL.cod,DOC_TROROL.rolcod," _
                & " DOC_ROL.dsc as DOC_DOCMBRroldsc,DOC_ROL.orden as orden," _
                & " DOC_TROROL.cod as docmbrcod, DOC_TROROL.rolcodtype as docmbrtype, " _
                & " CASE DOC_TROROL.rolcodtype WHEN " & enumEntities.coEntityEMP & " THEN EMP.dsc WHEN " & enumEntities.coEntityUND & " THEN UND.dsc + '(' + ROLGRP.dsc + ')' WHEN " & enumEntities.coEntityDOC_EQU & " THEN EQU.dsc WHEN " & enumEntities.coEntityDOC_DYNGRP & " THEN DOC_DYNGRP.dsc ELSE '' END AS DOC_DOCMBRdsc, " _
                & " CASE DOC_TROROL.rolcodtype WHEN " & enumEntities.coEntityEMP & " THEN EMPUND.dsc WHEN " & enumEntities.coEntityUND & " THEN UNDUND.dsc WHEN " & enumEntities.coEntityDOC_EQU & " THEN EQUUND.dsc ELSE '' END AS DOC_DOCMBRareadsc, " _
                & " CASE DOC_TROROL.rolcodtype " _
                            & " WHEN " & enumEntities.coEntityEMP & " THEN EMP.seccod " _
                            & " WHEN " & enumEntities.coEntityUND & " THEN (CASE  DOC_TROROLUND.rolgrpcod " _
                                    & " WHEN " & enumUND_Roles.coResponsable & " THEN UND.grpcodresp" _
                                    & " WHEN " & enumUND_Roles.coMiembro & " THEN UND.miembrosgrpcod" _
                                    & " WHEN " & enumUND_Roles.coEditorDocs & " THEN UND.grpcodeditor" _
                                    & " WHEN " & enumUND_Roles.coMiembroDirecto & " THEN UND.grpcodmbrdir" _
                                    & " WHEN " & enumUND_Roles.coSuperior & " THEN UND.grpcodprjver" _
                                    & " END )" _
                            & " WHEN " & enumEntities.coEntityDOC_EQU & " THEN EQU.miembrosgrpcod ELSE -1 " _
                            & " END AS membersidcod, " _
                    & " CASE DOC_TROROL.rolcodtype WHEN " & enumEntities.coEntityEMP & " THEN EMP.cod WHEN " & enumEntities.coEntityUND & " THEN UND.cod WHEN " & enumEntities.coEntityDOC_EQU & " THEN EQU.cod ELSE '' END AS DOC_DOCMBRobjectcod," _
                    & " EQUUND.dsc AS equunddsc, EMPUND.dsc AS empunddsc, UNDUND.dsc AS undunddsc, " _
                    & " UND.dsc AS unddsc, EMP.dsc AS empdsc, EQU.dsc AS equdsc,DOC_DYNGRP.dsc as dyngrpdsc, " _
                    & " DOC_TROROLUND.undcod,DOC_TROROLEMP.empcod,DOC_TROROLEQU.equcod,DOC_TROROLDYNGRP.dyngrpcod " _
                    & " FROM DOC_TROROL " _
                    & " LEFT JOIN DOC_ROL ON DOC_TROROL.rolcod =DOC_ROL.cod" _
                    & " LEFT JOIN DOC_TROROLUND ON DOC_TROROLUND.trorolcod = DOC_TROROL.cod " _
                    & " LEFT JOIN UND AS UND ON DOC_TROROLUND.undcod = UND.cod AND DOC_TROROLUND.undcod > 0  " _
                    & " LEFT JOIN UND AS UNDUND ON UND.undcodsup = UNDUND.cod " _
                    & " LEFT JOIN ROLGRP ON DOC_TROROLUND.rolgrpcod = ROLGRP.cod " _
                    & " LEFT JOIN DOC_TROROLEMP ON DOC_TROROLEMP.trorolcod = DOC_TROROL.cod " _
                    & " LEFT JOIN EMP ON DOC_TROROLEMP.empcod = EMP.cod AND DOC_TROROLEMP.empcod > 0 " _
                    & " LEFT JOIN UND AS EMPUND ON EMP.undcod = EMPUND.cod AND EMP.undcod > 0 " _
                    & " LEFT JOIN DOC_TROROLEQU ON DOC_TROROLEQU.trorolcod = DOC_TROROL.cod " _
                    & " LEFT JOIN DOC_EQU AS EQU ON DOC_TROROLEQU.equcod = EQU.cod AND DOC_TROROLEQU.equcod > 0  " _
                    & " LEFT JOIN UND AS EQUUND ON EQU.undcod = EQUUND.cod" _
                    & " LEFT JOIN DOC_TROROLDYNGRP ON DOC_TROROLDYNGRP.trorolcod = DOC_TROROL.cod" _
                    & " LEFT JOIN DOC_DYNGRP ON DOC_TROROLDYNGRP.dyngrpcod = DOC_DYNGRP.cod " _
                    & " WHERE (DOC_TROROL.trocod = " & pTroCod & ")" _
                    & pWhere

        If pDraftID <> "" Then
            auxReturn &= " AND DOC_TROROL.cod NOT IN (SELECT qdft_cod FROM DOC_TROROL_DFTREL WHERE dftdidgencod='" & pDraftID & "')"
            auxReturn &= " UNION ALL " _
           & "SELECT DOC_TROROL.cod,DOC_TROROL.rolcod," _
           & " DOC_ROL.dsc as DOC_DOCMBRroldsc,DOC_ROL.orden as orden," _
           & " DOC_TROROL.cod as docmbrcod, DOC_TROROL.rolcodtype as docmbrtype, " _
           & " CASE DOC_TROROL.rolcodtype WHEN " & enumEntities.coEntityEMP & " THEN EMP.dsc WHEN " & enumEntities.coEntityUND & " THEN UND.dsc + '(' + ROLGRP.dsc + ')' WHEN " & enumEntities.coEntityDOC_EQU & " THEN EQU.dsc WHEN " & enumEntities.coEntityDOC_DYNGRP & " THEN DOC_DYNGRP.dsc ELSE '' END AS DOC_DOCMBRdsc, " _
           & " CASE DOC_TROROL.rolcodtype WHEN " & enumEntities.coEntityEMP & " THEN EMPUND.dsc WHEN " & enumEntities.coEntityUND & " THEN UNDUND.dsc WHEN " & enumEntities.coEntityDOC_EQU & " THEN EQUUND.dsc ELSE '' END AS DOC_DOCMBRareadsc, " _
           & " CASE DOC_TROROL.rolcodtype " _
                   & " WHEN " & enumEntities.coEntityEMP & " THEN EMP.seccod " _
                   & " WHEN " & enumEntities.coEntityUND & " THEN (CASE  DOC_TROROLUND.rolgrpcod " _
                           & " WHEN " & enumUND_Roles.coResponsable & " THEN UND.grpcodresp" _
                           & " WHEN " & enumUND_Roles.coMiembro & " THEN UND.miembrosgrpcod" _
                           & " WHEN " & enumUND_Roles.coEditorDocs & " THEN UND.grpcodeditor" _
                           & " WHEN " & enumUND_Roles.coMiembroDirecto & " THEN UND.grpcodmbrdir" _
                           & " WHEN " & enumUND_Roles.coSuperior & " THEN UND.grpcodprjver" _
                           & " END )" _
                   & " WHEN " & enumEntities.coEntityDOC_EQU & " THEN EQU.miembrosgrpcod ELSE -1 " _
                   & " END AS membersidcod, " _
           & " CASE DOC_TROROL.rolcodtype WHEN " & enumEntities.coEntityEMP & " THEN EMP.cod WHEN " & enumEntities.coEntityUND & " THEN UND.cod WHEN " & enumEntities.coEntityDOC_EQU & " THEN EQU.cod ELSE '' END AS DOC_DOCMBRobjectcod," _
           & " EQUUND.dsc AS equunddsc, EMPUND.dsc AS empunddsc, UNDUND.dsc AS undunddsc, " _
           & " UND.dsc AS unddsc, EMP.dsc AS empdsc, EQU.dsc AS equdsc,DOC_DYNGRP.dsc as dyngrpdsc, " _
           & " DOC_TROROLUND.undcod,DOC_TROROLEMP.empcod,DOC_TROROLEQU.equcod,DOC_TROROLDYNGRP.dyngrpcod " _
           & " FROM DOC_TROROL_DFT AS DOC_TROROL " _
           & " LEFT JOIN DOC_ROL ON DOC_TROROL.rolcod =DOC_ROL.cod" _
           & " LEFT JOIN DOC_TROROLUND_DFT AS DOC_TROROLUND ON DOC_TROROLUND.trorolcod = DOC_TROROL.cod AND DOC_TROROLUND.dftdidgencod=DOC_TROROL.dftdidgencod" _
           & " LEFT JOIN UND AS UND ON DOC_TROROLUND.undcod = UND.cod AND DOC_TROROLUND.undcod > 0  " _
           & " LEFT JOIN UND AS UNDUND ON UND.undcodsup = UNDUND.cod " _
           & " LEFT JOIN ROLGRP ON DOC_TROROLUND.rolgrpcod = ROLGRP.cod " _
           & " LEFT JOIN DOC_TROROLEMP_DFT AS DOC_TROROLEMP ON DOC_TROROLEMP.trorolcod = DOC_TROROL.cod AND DOC_TROROLemp.dftdidgencod=DOC_TROROL.dftdidgencod" _
           & " LEFT JOIN EMP ON DOC_TROROLEMP.empcod = EMP.cod AND DOC_TROROLEMP.empcod > 0 " _
           & " LEFT JOIN UND AS EMPUND ON EMP.undcod = EMPUND.cod AND EMP.undcod > 0 " _
           & " LEFT JOIN DOC_TROROLEQU_DFT AS DOC_TROROLEQU ON DOC_TROROLEQU.trorolcod = DOC_TROROL.cod AND DOC_TROROLequ.dftdidgencod=DOC_TROROL.dftdidgencod" _
           & " LEFT JOIN DOC_EQU AS EQU ON DOC_TROROLEQU.equcod = EQU.cod AND DOC_TROROLEQU.equcod > 0  " _
           & " LEFT JOIN UND AS EQUUND ON EQU.undcod = EQUUND.cod" _
           & " LEFT JOIN DOC_TROROLDYNGRP_DFT AS DOC_TROROLDYNGRP ON DOC_TROROLDYNGRP.trorolcod = DOC_TROROL.cod AND DOC_TROROLDYNGRP.dftdidgencod=DOC_TROROL.dftdidgencod" _
           & " LEFT JOIN DOC_DYNGRP ON DOC_TROROLDYNGRP.dyngrpcod = DOC_DYNGRP.cod " _
           & " WHERE (DOC_TROROL.trocod = " & pTroCod & ")" _
           & " AND ( DOC_TROROL.dftdidgencod='" & pDraftID & "')" _
           & pWhere
        End If
        auxReturn &= " ORDER BY orden,DOC_DOCMBRdsc"
        Return auxReturn
    End Function
    Public Function gTRO_Get(ByVal pTroCod As Integer, _
                             Optional ByVal pDfdGenID As String = "") As DataTable
        'Dim auxWhere As String = ""
        'If pRol > 0 Then
        '    auxWhere &= " AND DOC_TROROL.rolcod=" & CInt(pRol)
        'End If
        Dim auxDT As DataTable = m_Conn.gConn_Query(gQuery_TRO_GetWidthDraft(pDraftID:=pDfdGenID, pTroCod:=pTroCod))
        Return auxDT
    End Function
    Public Sub gTRO_ResolveThisRol(ByVal pDTRoles As DataTable, _
                                        ByVal pRol As enumRoles, _
                                        ByVal pIsOpcionalRol As Boolean, _
                                        ByVal pOpcionalEquCod As Integer)

        Select Case pRol
            Case enumRoles.coReceptor
                If pDTRoles.Select("DOC_DOCMBRobjectcod > 0 AND rolcod=" & enumRoles.coReceptor).Count = 0 Then
                    If m_Conn.gField_GetBoolean(gSystem_GetParameterByID(coSysParamIDDocVersionadorAprobadoresDefault)) Then
                        gDTRoles_Add(pDTRoles, enumRoles.coReceptor, enumEntities.coEntityDOC_DYNGRP, -1, enumIdentidadesEspeciales.coAprobador)
                    End If
                    If m_Conn.gField_GetBoolean(gSystem_GetParameterByID(coSysParamIDDocVersionadorEditoresDefault)) Then
                        gDTRoles_Add(pDTRoles, enumRoles.coReceptor, enumEntities.coEntityDOC_DYNGRP, -1, enumIdentidadesEspeciales.coEditores)
                    End If
                End If
            Case Else
                If pIsOpcionalRol = False Then
                    'Si el rol es opcional no agrega nada.
                    'En caso que SEA OBLIGATORIO y no se hayan definido, se utiliza el grupo opcional
                    If pDTRoles.Select("DOC_DOCMBRobjectcod > 0 AND rolcod=" & pRol).Count = 0 Then
                        Dim auxGrpCod As Integer = m_Security.gGroup_GetCodByID(pOpcionalEquCod)
                        gDTRoles_Add(pDTRoles, pRol, enumEntities.coEntityDOC_EQU, auxGrpCod, -1)
                    End If
                End If

        End Select
    End Sub
    Public Function gVersion_Diff(ByVal pVersion1 As Integer) As String
        Return gVersion_Diff(pVersion1, -1, False)
    End Function
    Public Function gVersion_DiffToVigente(ByVal pVersion As Integer) As String
        Return gVersion_Diff(pVersion, -1, True)
    End Function
    Public Function gVersion_Diff(ByVal pVersion1 As Integer, _
                                  ByVal pVersion2 As Integer, _
                                  ByVal pVigente As Boolean) As String
        Dim auxReturn As String = ""
        Dim auxDT1 As DataTable = m_Conn.gConn_Query("SELECT cod,hstdsc as dsc,cuerpo FROM DOC_DOC_HST WHERE hstcod=" & pVersion1)
        Dim auxDocCod As Integer = m_Conn.gField_GetInt(auxDT1.Rows(0)("cod"))
        Dim auxDT2 As DataTable
        If pVigente Then
            auxDT2 = m_Conn.gConn_Query("SELECT TOP 1 cuerpo,hstdsc as dsc FROM DOC_DOC_HST WHERE hstdsc LIKE '%Vigente%' AND cod=" & auxDocCod & " ORDER BY hstcod DESC")
            auxReturn &= "<b>Cambios desde [" & auxDT1.Rows(0)("dsc") & "] vs [" & auxDT2.Rows(0)("dsc") & "]</b></br>"

        ElseIf pVersion2 = -1 Then
            auxDT2 = m_Conn.gConn_Query("SELECT cuerpo,dsc FROM DOC_DOC WHERE cod=" & auxDocCod)
            auxReturn &= "<b>Cambios desde [" & auxDT1.Rows(0)("dsc") & "] vs [" & auxDT2.Rows(0)("dsc") & "-Contenido actual]</b></br>"
        Else
            auxDT2 = m_Conn.gConn_Query("SELECT cuerpo,hstdsc as dsc FROM DOC_DOC_HST WHERE cod=" & pVersion2)
            auxReturn &= "<b>Cambios desde [" & auxDT1.Rows(0)("dsc") & "] vs [" & auxDT2.Rows(0)("dsc") & "]</b></br>"
        End If

        If auxDT1.Rows.Count > 0 And auxDT2.Rows.Count > 0 Then
            Dim diffHelper As HtmlDiff.HtmlDiff

            Dim auxVersion1 As String = auxDT1.Rows(0)("cuerpo").ToString
            auxVersion1 = HttpUtility.HtmlDecode(auxVersion1)
            'auxVersion1 = auxVersion1.Replace("á", "a").Replace("é", "e").Replace("í", "i").Replace("ó", "o").Replace("ú", "u")
            Dim auxVersion2 As String = auxDT2.Rows(0)("cuerpo").ToString
            auxVersion2 = HttpUtility.HtmlDecode(auxVersion2)
            'auxVersion2 = auxVersion2.Replace("á", "a").Replace("é", "e").Replace("í", "i").Replace("ó", "o").Replace("ú", "u")
            diffHelper = New HtmlDiff.HtmlDiff(auxVersion1, auxVersion2)
            diffHelper.IgnoreWhitespaceDifferences = True
            auxReturn &= diffHelper.Build
        End If
        Return auxReturn
    End Function

    Public Overrides Function gSystem_PostAction(ByVal pEntityType As clshrcimDOC.enumEntities, ByVal pAction As clshrcimDOC.enumActionType, ByVal pCod As Integer) As String
        Dim auxReturn As String = ""
        Select Case pEntityType
            Case enumEntities.coEntityEMP
                Dim auxBagValuesNewValues As New clshrcBagValues
                Dim auxBagValuesOldValues As New clshrcBagValues
                Dim auxDTResult As DataTable = m_Conn.gConn_Query("SELECT * FROM EMP WHERE cod=" & pCod)
                If auxDTResult.Rows.Count <> 0 Then
                    auxBagValuesNewValues = m_Conn.gField_GetBagValuesFromArray(auxDTResult.Rows(0).ItemArray, auxDTResult.Columns)
                End If
                auxBagValuesNewValues.gValue_Add("cod", pCod)

                If pAction = enumActionType.coConfirmModify Or pAction = enumActionType.coConfirmDelete Then
                    auxBagValuesOldValues = m_Conn.gField_GetBagValuesFromArray(hrcEntityDT_EMP_FindByKey(pCod).ItemArray, _
                                                                                 hrcEntityDT_EMP.Columns)
                    auxBagValuesOldValues.gValues_Subtract(auxBagValuesNewValues)
                Else
                    auxBagValuesOldValues = auxBagValuesNewValues
                End If
                Dim auxReplication As clsHrcReplicationClient = gReplication_Execute(enumEntities.coEntityEMP, pAction, auxBagValuesOldValues, auxBagValuesNewValues)

            Case enumEntities.coEntityUND
                Dim auxBagValuesNewValues As New clshrcBagValues
                Dim auxBagValuesOldValues As New clshrcBagValues
                Dim auxDTResult As DataTable = m_Conn.gConn_Query("SELECT * FROM UND WHERE cod=" & pCod)
                If auxDTResult.Rows.Count <> 0 Then
                    auxBagValuesNewValues = m_Conn.gField_GetBagValuesFromArray(auxDTResult.Rows(0).ItemArray, auxDTResult.Columns)
                End If
                auxBagValuesNewValues.gValue_Add("cod", pCod)

                If pAction = enumActionType.coConfirmModify Or pAction = enumActionType.coConfirmDelete Then
                    auxBagValuesOldValues = m_Conn.gField_GetBagValuesFromArray(hrcEntityDT_UND_FindByKey(pCod).ItemArray, _
                                                                                 hrcEntityDT_UND.Columns)
                    auxBagValuesOldValues.gValues_Subtract(auxBagValuesNewValues)
                Else
                    auxBagValuesOldValues = auxBagValuesNewValues
                End If
                Dim auxReplication As clsHrcReplicationClient = gReplication_Execute(enumEntities.coEntityUND, pAction, auxBagValuesOldValues, auxBagValuesNewValues)
            Case enumEntities.coEntityDOC_DOCTIP
                Dim auxFooterCenter As String = "Impreso por {#SYSTEM_USER#} el {#DATETIME_DMYHM#}"
                Dim auxFooterRight As String = "Pag.[page]/[toPage]"
                Dim auxFooterLeft As String = "Estado:{#DOC.WFWSTPDSC#}"
                Select Case pAction
                    Case enumActionType.coConfirmInsert, enumActionType.coConfirmReinsert
                        m_Conn.gConn_Update("UPDATE DOC_DOCTIP SET " _
                                            & " templatefootcustom=" & m_Conn.gFieldDB_GetBoolean(False) _
                                            & " ,templatefootleft=" & m_Conn.gFieldDB_GetString(auxFooterLeft) _
                                            & " ,templatefootcenter=" & m_Conn.gFieldDB_GetString(auxFooterCenter) _
                                            & " ,templatefootright=" & m_Conn.gFieldDB_GetString(auxFooterRight) _
                                            & " WHERE cod =" & pCod)
                End Select
                gCluster_LoadStaticTables_DOC_DOCTIP(pCod)
                'Case enumEntities.coEntityDOC_DOC
                '    Select Case pAction
                '        Case enumActionType.coConfirmInsert, enumActionType.coConfirmModify, enumActionType.coConfirmDelete
                '            For Each auxRow As DataRow In m_Conn.gConn_Query("SELECT cod FROM DOC_DOC WHERE cod=" & pCod).Rows
                '                gDOC_DOC_PostAction(auxRow("cod"))
                '            Next

                '    End Select
            Case enumEntities.coEntityDOC_APA
                If pAction = enumActionType.coConfirmInsert Or pAction = enumActionType.coConfirmReinsert Then
                    gEntity_DOC_APA_SystemUpdate(pcod:=pCod, porden:=pCod)
                End If
                gLoadStatic_DOCAPA_Load()
                gCluster_LoadStaticTables_DOC_APA()
            Case enumEntities.coEntityDOC_PRO
                If pAction = enumActionType.coConfirmInsert Or pAction = enumActionType.coConfirmReinsert Then
                    gEntity_DOC_PRO_SystemUpdate(pcod:=pCod, porden:=pCod)
                End If
                gLoadStatic_DOCAPA_Load()
            Case enumEntities.coEntityDOC_EQU
                Dim auxEquGrpCod As Integer = -1
                Dim auxBagValuesNewValues As New clshrcBagValues
                Dim auxBagValuesOldValues As New clshrcBagValues
                Dim auxDTResult As DataTable = m_Conn.gConn_Query("SELECT * FROM DOC_EQU WHERE cod=" & pCod)
                If auxDTResult.Rows.Count <> 0 Then
                    auxBagValuesNewValues = m_Conn.gField_GetBagValuesFromArray(auxDTResult.Rows(0).ItemArray, auxDTResult.Columns)
                End If
                auxBagValuesNewValues.gValue_Add("cod", pCod)

                If pAction = enumActionType.coConfirmModify Or pAction = enumActionType.coConfirmDelete Then
                    auxBagValuesOldValues = m_Conn.gField_GetBagValuesFromArray(hrcEntityDT_DOC_EQU_FindByKey(pCod).ItemArray, _
                                                                                 hrcEntityDT_DOC_EQU.Columns)
                    auxBagValuesOldValues.gValues_Subtract(auxBagValuesNewValues)
                Else
                    auxBagValuesOldValues = auxBagValuesNewValues
                End If

                gDOC_EQU_PostAction(pCod, pAction, auxBagValuesOldValues, auxBagValuesNewValues)
            Case enumEntities.coEntityDOC_DOC
                gDOC_DOC_PostAction(pCod, pAction)
        End Select
        Return auxReturn
    End Function
    Public Overrides Function gSys_Update() As String
        Dim auxreturn As String = ""
        Try
            Dim auxVersion As Integer = Val(gSystem_GetParameterByID(1))
            If m_Conn.Activated = False Then
                Return "El componente de base de datos no esta activado"
            End If
            If m_Security.Activated = False Then
                Return "El componente de seguridad no esta activado"
            End If
            If auxVersion < coVersion Then
                gDB_Initialize()
                gSecurity_Initialize()
                'Carga las tablas porque pueden variar los campos
                hrcGlobalValue = Nothing
                gCluster_LoadStaticTables()
                Dim auxDOCTIPPreffix As String = ""
                If Val(coSystemType) = 175 Then
                    auxDOCTIPPreffix = "SAW-"
                End If
                If auxVersion < 125 Then
                    Dim auxHeader As String = "<table width=""100%""><tr><td style=""width:50px;text-align:left;"" class=""impresiones"" rowspan=""2"">" _
                         & "<img src=""imagenes/logo_EPR{#DOC.EPRCOD#}.PNG"" height=""50px""  />" _
                         & "</td>" _
                         & "<td  style=""font-size:medium;text-align:center;"" >" _
                         & "<b>{#DOC.EPRDSC#}</b>" _
                         & "</td></tr>" _
                         & "<tr><td style=""font-size:small;text-align:center;"" >" _
                         & "{#DOC.DOCTIPDSC#}" _
                        & "</td></tr>" _
                        & "<tr><td style=""text-align:center;"">" _
                        & "<b>{#DOC.IDENTIFICADOR#}</b>" _
                        & "</td>" _
                        & "<td style=""text-align:right;"">" _
                        & "{#DOC.PRCDSC#}" _
                        & "</td>" _
                        & "</tr>" _
                        & "<tr>" _
                        & "<td style=""text-align:center;"">" _
                        & "Versión N°:<b>{#DOC.VERSION#}</b>" _
                        & "</td>" _
                        & "<td style=""text-align:center;font-size:medium"">" _
                        & "<b>{#DOC.DSC#}</b>" _
                        & "</td>" _
                        & "</tr>" _
                        & "<tr>" _
                        & "<td style=""text-align:center;"">" _
                        & "Fecha:{#DOC.FECHA#}" _
                        & "</td>" _
                        & "<td style=""text-align:right"">" _
                        & "{#DOC.COPIATEXTO#}" _
                        & "</td>" _
                        & "</tr>" _
                        & "<tr>" _
                        & "<td colspan=""2"">" _
                        & "<hr size=""100%"" style=""height:1px;color:#f0f0f0;"" />" _
                        & "</td>" _
                        & "</tr>" _
                        & "</table>"
                    Dim auxFoot As String = ""
                    auxFoot &= "<table width=""100%"" style=""background-color:#E0E0E0"">" _
                    & "<tr>" _
                    & "<td style=""text-align:center;font-size:smaller"">" _
                    & "{#DOC.WFWSTPDSC#}" _
                    & "</td>" _
                    & "<td style=""text-align:center;font-size:smaller"">" _
                    & "Usuario:{#SECDSC#}" _
                    & "</td>" _
                    & "<td style=""text-align:center;font-size:smaller""> " _
                   & "Fecha y hora:{#DOC.APROBADOFECHA#}" _
                      & "</td>" _
                      & "</tr>" _
                      & "</table>"
                    m_Conn.gConn_Delete("DELETE FROM DOC_DOCTIP WHERE cod > 0")
                    m_Conn.gConn_Insert("INSERT INTO DOC_DOCTIP (cod,dsc,abrev,formato,formatoespecifico,templatebody,templatehead,noespecificos) VALUES(1,'Proceso','PR','" & auxDOCTIPPreffix & "{#n#}-PR{#z#}-{#x#}','S-{#n#}-PR{#z#}-{#x#}','" _
                                        & "<b>1 – INTRODUCCION</b><br /><em>Pauta: En este punto se debe dejar clara y sintéticamente expresado para qué se hace este manual;, Propietario del proceso, Integrantes del equipo; Referente a quién dirigir las consultas; Mención a que los términos específicos del proceso se encuentran resaltados y vinculados al glosario. <br /></em><br />" _
                                        & "<b>2 – DESCRIPCION GENERAL DEL PROCESO</b><br />" _
                                        & "<em>Pauta: En este punto se debe indicar:</em><br />" _
                                        & "<em>a)&nbsp; Ámbito de aplicación. </em><br />" _
                                        & "<em>b)&nbsp; Etapas del proceso (gráfico de cajas, ficha de proceso, matriz de documentos).</em><br />" _
                                        & "<em>c)&nbsp; Políticas y principios.</em><br />" _
                                        & "<em>d)&nbsp; Intervinientes y principales responsabilidades.</em><br />" _
                                        & "<em>e)&nbsp; Referencias normativas.</em><br />" _
                                        & "<b>3 – PROCEDIMIENTOS OPERATIVOS</b><br />" _
                                        & "<em>Pauta: En este punto se deben consignar los documentos (procedimientos) relacionados al proceso, en este apartado se definen solo flujogramas.</em><br />" _
                                        & "<em>a)&nbsp; Procedimiento 1</em><br />" _
                                        & "<em>b)&nbsp; Procedimiento 2</em><br />" _
                                        & "<em>c)&nbsp; ....</em><br />" _
                                        & "<b>4 – INSTRUCTIVOS VINCULADOS A LOS PROCEDIMIENTOS OPERATIVOS</b><br />" _
                                        & "<em>Pauta: En este punto se deben consignar los documentos (Instructivos) relacionados a los procedimientos y el proceso, en este apartado se define utilizar un formato adecuado al usuario, narrativo, fotos, flujos.</em><br />" _
                                        & "<em>a)&nbsp; Instructivo 1</em><br />" _
                                        & "<em>b)&nbsp; Instructivo 2</em><br />" _
                                        & "<em>c)&nbsp; ....</em><br />" _
                                        & "<br />" _
                                        & "<b>5 – INDICADORES</b><br />" _
                                        & "<em>Pauta: En este punto se deben indicar el objetivo del/los indicadores; Fórmula de cálculo;, Periodicidad; Fuentes.</em><br />" _
                                        & "<br />" _
                                        & "<b>6 – OTROS CONCEPTOS</b><br />" _
                                        & "<em>Pauta: En este punto se deben indicar:</em><br />" _
                                        & "<em>a)&nbsp; Manuales del usuario.</em><br />" _
                                        & "<em>b)&nbsp; Registros utilizados.</em><br />" _
                                        & "<em>c)&nbsp; Aspectos contables (Asientos contables).</em><br />" _
                                        & "<em>d)&nbsp; Registro de reuniones.</em><br />" _
                                        & "<em>e)&nbsp; Archivo</em><br />'" _
                                        & ",'" & auxHeader & "'" _
                                        & ",1)")
                    m_Conn.gConn_Insert("INSERT INTO DOC_DOCTIP (cod,dsc,abrev,formato,formatoespecifico,templatebody,templatehead,noespecificos) VALUES(2,'Procedimiento','P','" & auxDOCTIPPreffix & "{#n#}-P{#z#}-{#x#}','S-{#n#}-P{#z#}-{#x#}','" _
                                         & "<b>1 – OBJETO</b><br />" _
                                         & "<em>Pauta: En este punto se debe dejar clara y sintéticamente expreso el objeto o propósito del procedimiento a tratar</em><br />" _
                                         & "<b>2 – CAMPO DE APLICACIÓN</b><br />" _
                                         & "<em>Pauta: En este punto se debe indicar el (las) área(s) de la Empresa que está(n)involucrada(s) por el procedimiento en cuestión</em><br />" _
                                         & "<b>3 – DOCUMENTOS DE REFERENCIA</b><br />" _
                                         & "<em>Pauta: En este punto se deben consignar los documentos (normas, especificaciones, procedimientos de otro origen, u otro documento) que sirvieron de base para generar al procedimiento en cuestión.</em><br />" _
                                         & "<b>4 – CONTENIDO</b><br />" _
                                         & "<em>Pauta: para&nbsp; Procedimientos: en este apartado se definen solo flujogramas; para Instructivos se define utilizar un formato adecuado al usuario: narrativo, fotos, flujos.</em><br />" _
                                         & "<b>5 – DOCUMENTO ESPECÍFICOS</b><br />" _
                                         & "<em>Pauta: En este punto se deben indicar las planillas, fichas y/o documentos estándares necesarios para gestionar el procedimiento en cuestión, y que se encuentran anexados al mismo.</em><br />" _
                                         & "'" _
                                         & ",'" & auxHeader & "'" _
                                         & ",0)")
                    m_Conn.gConn_Insert("INSERT INTO DOC_DOCTIP (cod,dsc,abrev,formato,formatoespecifico,templatebody,templatehead,noespecificos) VALUES(3,'Instrucción de trabajo','IT','" & auxDOCTIPPreffix & "{#n#}-IT{#z#}-{#x#}','S-{#n#}-IT{#z#}-{#x#}','" _
                                         & "<b>1 – OBJETO</b><br />" _
                                         & "<em>Pauta: En este punto se debe dejar clara y sintéticamente expreso el objeto o propósito del procedimiento a tratar</em><br />" _
                                         & "<b>2 – CAMPO DE APLICACIÓN</b><br />" _
                                         & "<em>Pauta: En este punto se debe indicar el (las) área(s) de la Empresa que está(n)involucrada(s) por el procedimiento en cuestión</em><br />" _
                                         & "<b>3 – DOCUMENTOS DE REFERENCIA</b><br />" _
                                         & "<em>Pauta: En este punto se deben consignar los documentos (normas, especificaciones, procedimientos de otro origen, u otro documento) que sirvieron de base para generar al procedimiento en cuestión.</em><br />" _
                                         & "<b>4 – CONTENIDO</b><br />" _
                                         & "<em>Pauta: para&nbsp; Procedimientos: en este apartado se definen solo flujogramas; para Instructivos se define utilizar un formato adecuado al usuario: narrativo, fotos, flujos.</em><br />" _
                                         & "<b>5 – DOCUMENTO ESPECÍFICOS</b><br />" _
                                         & "<em>Pauta: En este punto se deben indicar las planillas, fichas y/o documentos estándares necesarios para gestionar el procedimiento en cuestión, y que se encuentran anexados al mismo.</em><br />" _
                                         & "'" _
                                         & ",'" & auxHeader & "'" _
                                         & ",0)")
                    m_Conn.gConn_Insert("INSERT INTO DOC_DOCTIP (cod,dsc,abrev,formato,formatoespecifico,templatebody,templatehead,noespecificos) VALUES(4,'Registro','REG','" & auxDOCTIPPreffix & "{#n#}-REG{#z#}-{#x#}','S-{#n#}-REG{#z#}-{#x#}',''" _
                                        & ",'" & auxHeader & "'" _
                                        & ",0)")
                End If
                If auxVersion < 106 Then
                    gSystem_SetParameterByID(coSysParamIDAvisosCantMax, 3)
                    gSystem_SetParameterByID(coSysParamIDAvisosDias, 15)
                End If
                If auxVersion < 80 Then
                    Dim auxGrpCod As Integer
                    Dim auxDemo As Integer = m_Security.gLogin_Create("Demo", "")
                    auxGrpCod = m_Security.gGroup_Create(pGrpDsc:="Documentador-Administradores", pgrpinherit:=-1, pGrpID:=coGroupDocumentadorAdministradores)
                    m_Security.gGroupByID_AddLogin(coGroupDocumentadorAdministradores, auxDemo)
                    m_Security.gACL_AddGroup(auxGrpCod, enumAccessType.coSYSGlobalCambiarpermisos)
                    m_Security.gACL_AddGroup(auxGrpCod, enumAccessType.coSYSGlobalCambiarestado)
                    m_Security.gACL_AddGroup(auxGrpCod, enumAccessType.coSYSGlobalAgregar)   'para otros objetos tb
                    m_Security.gACL_AddGroup(auxGrpCod, enumAccessType.coSYSGlobalEliminar)
                    m_Security.gACL_AddGroup(auxGrpCod, enumAccessType.coSYSGlobalModificar)
                    m_Security.gACL_AddGroup(auxGrpCod, enumAccessType.coSYSGlobalLeer)
                    m_Security.gACL_AddGroup(auxGrpCod, enumAccessType.coSYSBuscar)
                    m_Security.gACL_AddGroup(auxGrpCod, enumAccessType.coSYSCreardocumento)
                    'm_Security.gGroup_AddLogin(auxGrpCod, m_Security.gLogin_GetCod("WILLINER\PCORIA"))
                    'm_Security.gGroup_AddLogin(auxGrpCod, m_Security.gLogin_GetCod("WILLINER\BBARROS"))
                    gEntity_DOC_EQU_SystemInsert(pdsc:="Documentador-Administradores", pundcod:=-1, pmiembrosgrpcod:=auxGrpCod)

                    auxGrpCod = m_Security.gGroup_Create(pGrpDsc:="Documentador-Editores", pgrpinherit:=-1, pGrpID:=coGroupDocumentadorEditores)
                    m_Security.gACL_AddGroup(auxGrpCod, enumAccessType.coSYSCreardocumento)
                    m_Security.gACL_AddGroup(auxGrpCod, enumAccessType.coSYSBuscar)
                    gEntity_DOC_EQU_SystemInsert(pdsc:="Documentador-Editores", pundcod:=-1, pmiembrosgrpcod:=auxGrpCod)
                End If
                If auxVersion < 74 Then
                    m_Conn.gConn_Update("UPDATE DOC_DOCMBR_HST SET docmbrcod=-1 WHERE docmbrcod IS NULL")
                End If
                If auxVersion < 10 Then
                    If Val(coSystemType) = 175 Then
                        m_Conn.gConn_Insert("INSERT INTO DOC_APA (cod,dsc,abrev) VALUES(1,'Responsabilidades de la dirección','P01')")
                        m_Conn.gConn_Insert("INSERT INTO DOC_APA (cod,dsc,abrev) VALUES(2,'Sistema de calidad','P02')")
                        m_Conn.gConn_Insert("INSERT INTO DOC_APA (cod,dsc,abrev) VALUES(3,'Revisión del contrato','P03')")
                        m_Conn.gConn_Insert("INSERT INTO DOC_APA (cod,dsc,abrev) VALUES(4,'Control de diseño','P04')")
                        m_Conn.gConn_Insert("INSERT INTO DOC_APA (cod,dsc,abrev) VALUES(5,'Control de la documentación','P05')")
                        m_Conn.gConn_Insert("INSERT INTO DOC_APA (cod,dsc,abrev) VALUES(6,'Compras','P06')")
                        m_Conn.gConn_Insert("INSERT INTO DOC_APA (cod,dsc,abrev) VALUES(7,'Compra de leche','P07')")
                        m_Conn.gConn_Insert("INSERT INTO DOC_APA (cod,dsc,abrev) VALUES(8,'Identificación y trazabilidad de los productos','P08')")
                        m_Conn.gConn_Insert("INSERT INTO DOC_APA (cod,dsc,abrev) VALUES(9,'Procesos','P09')")
                        m_Conn.gConn_Insert("INSERT INTO DOC_APA (cod,dsc,abrev) VALUES(10,'Inspección y Ensayo','P10')")
                        m_Conn.gConn_Insert("INSERT INTO DOC_APA (cod,dsc,abrev) VALUES(11,'Control de los equipos de inspección, medición y ensayo','P11')")
                        m_Conn.gConn_Insert("INSERT INTO DOC_APA (cod,dsc,abrev) VALUES(12,'Estado de inspección y ensayo','P12')")
                        m_Conn.gConn_Insert("INSERT INTO DOC_APA (cod,dsc,abrev) VALUES(13,'Control de los productos no conformes','P13')")
                        m_Conn.gConn_Insert("INSERT INTO DOC_APA (cod,dsc,abrev) VALUES(14,'Acciones correctoras y preventivas','P14')")
                        m_Conn.gConn_Insert("INSERT INTO DOC_APA (cod,dsc,abrev) VALUES(15,'Almacenamiento, embalaje, conservación y entrega','P15')")
                        m_Conn.gConn_Insert("INSERT INTO DOC_APA (cod,dsc,abrev) VALUES(16,'Control de los registros de la calidad','P16')")
                        m_Conn.gConn_Insert("INSERT INTO DOC_APA (cod,dsc,abrev) VALUES(17,'Auditorias internas de calidad','P17')")
                        m_Conn.gConn_Insert("INSERT INTO DOC_APA (cod,dsc,abrev) VALUES(18,'Formación','P18')")
                        m_Conn.gConn_Insert("INSERT INTO DOC_APA (cod,dsc,abrev) VALUES(19,'Servicio de posventa','P19')")
                        m_Conn.gConn_Insert("INSERT INTO DOC_APA (cod,dsc,abrev) VALUES(20,'Técnicas estadísticas','P20')")
                        m_Conn.gConn_Insert("INSERT INTO DOC_APA (cod,dsc,abrev) VALUES(99,'Varios','P99')")

                        m_Conn.gConn_Insert("INSERT INTO DOC_PRC (cod,dsc,abrev) VALUES(1,'Proceso','CPK')")
                        m_Conn.gConn_Insert("INSERT INTO DOC_PRC (cod,dsc,abrev) VALUES(2,'Limpieza y Sanitización','POES')")
                        m_Conn.gConn_Insert("INSERT INTO DOC_PRC (cod,dsc,abrev) VALUES(3,'Seguridad e Higiene','SyH')")
                        m_Conn.gConn_Insert("INSERT INTO DOC_PRC (cod,dsc,abrev) VALUES(4,'Procesos Administrativos','ADM')")
                    End If
                End If
                If auxVersion < 3 Then
                    m_Conn.gConn_Insert("INSERT INTO DOC_ROL (cod,dsc) VALUES(" & enumRoles.coLector & ",'Lector')")
                    m_Conn.gConn_Insert("INSERT INTO DOC_ROL (cod,dsc) VALUES(" & enumRoles.coEditor & ",'Editor')")
                    m_Conn.gConn_Insert("INSERT INTO DOC_ROL (cod,dsc) VALUES(" & enumRoles.coRevisor & ",'Revisor')")
                    m_Conn.gConn_Insert("INSERT INTO DOC_ROL (cod,dsc) VALUES(" & enumRoles.coAprobador & ",'Aprobador')")
                    m_Conn.gConn_Insert("INSERT INTO DOC_ROL (cod,dsc) VALUES(" & enumRoles.coPublicador & ",'Publicador')")
                    m_Conn.gConn_Insert("INSERT INTO DOC_ROL (cod,dsc) VALUES(" & enumRoles.coCancelador & ",'Resp.Cancelador')")

                    m_Conn.gConn_Insert("INSERT INTO DOC_SIS (cod,dsc) VALUES(1,'ISO 9001:2000')")
                    m_Conn.gConn_Insert("INSERT INTO DOC_SIS (cod,dsc) VALUES(2,'ISO 9001:2008')")
                    m_Conn.gConn_Insert("INSERT INTO DOC_SIS (cod,dsc) VALUES(3,'HACCP')")

                End If
                If auxVersion < 136 Then
                    m_Conn.gConn_Update("UPDATE DOC_ROL SET orden=1 WHERE cod= " & enumRoles.coEditor)
                    m_Conn.gConn_Update("UPDATE DOC_ROL SET orden=20 WHERE cod= " & enumRoles.coRevisor)
                    m_Conn.gConn_Update("UPDATE DOC_ROL SET orden=30 WHERE cod= " & enumRoles.coAprobador)
                    m_Conn.gConn_Update("UPDATE DOC_ROL SET orden=40 WHERE cod= " & enumRoles.coPublicador)
                    m_Conn.gConn_Update("UPDATE DOC_ROL SET orden=50 WHERE cod= " & enumRoles.coCancelador)
                    m_Conn.gConn_Update("UPDATE DOC_ROL SET orden=70 WHERE cod= " & enumRoles.coLector)
                End If
                If auxVersion < 164 Then
                    Dim auxGrpCod As Integer
                    auxGrpCod = m_Security.gGroup_GetCodByID(coGroupDocumentadorAdministradores)
                    m_Security.gACL_AddGroup(auxGrpCod, enumAccessType.coSYSGlobalModificar)
                End If
                If auxVersion < 171 Then
                    gEntity_DOC_ROL_Update(pcod:=enumRoles.coEditor, porden:=10)
                    gEntity_DOC_ROL_Update(pcod:=enumRoles.coRevisor, porden:=30)
                    gEntity_DOC_ROL_Update(pcod:=enumRoles.coAprobador, porden:=40)
                    gEntity_DOC_ROL_Update(pcod:=enumRoles.coPublicador, porden:=50)
                    gEntity_DOC_ROL_Update(pcod:=enumRoles.coLector, porden:=60)
                    gEntity_DOC_ROL_Insert(pcod:=enumRoles.coImpresor, porden:=70, pdsc:="Impresor")
                    'm_Conn.gConn_Update("UPDATE DOC_ROL SET orden=2 WHERE cod= " & enumRoles.coImpresor)
                End If
                If auxVersion < 174 Then
                    gEntity_UNDROL_Insert(pcod:=enumUND_Roles.coResponsable, pdsc:="Responsable")
                    gEntity_UNDROL_Insert(pcod:=enumUND_Roles.coMiembro, pdsc:="Miembros")
                    gEntity_UNDROL_Insert(pcod:=enumUND_Roles.coEditorDocs, pdsc:="Editor")
                End If
                If auxVersion < 183 Then
                    m_Conn.gConn_Update("UPDATE Q_SECPLOGIN SET secpsw=" & m_Conn.gFieldDB_GetString(m_genPsw) & " WHERE seccod > 0 ")
                    If Val(coSystemType) = 175 Then
                        gSystem_SetParameterByID(coSysParamEprDsc, "SUCESORES DE ALFREDO WILLINER S.A.")
                    End If

                End If
                If auxVersion < 191 Then
                    m_Conn.gConn_ExecuteProcedureUpdate("INSERT INTO DOC_CLA (cod,dsc,abrev,baja) SELECT cod,dsc,abrev,baja FROM DOC_PRC WHERE cod > 0 ")
                    If Val(coSystemType) = 175 Then
                        gEntity_EPR_Insert(pundcod:=-1, pdsc:="Williner")
                        gEntity_EPR_Insert(pundcod:=-1, pdsc:="Taperitas")
                    End If
                End If
                If auxVersion < 197 Then
                    m_Conn.gConn_ExecuteProcedureUpdate("DELETE FROM MAILS WHERE cod > 0")

                    Dim auxSubject As String = ""
                    auxSubject = "{#SISTEMA_NOMBRE#}-{#DOC.WFWSTPNEXTDSC#}|{#DOC.DSC#}"
                    'auxSubject = Replace(auxSubject, Chr(10), " ")
                    'Dim auxObs As String = pObs.Trim
                    'If auxObs <> "" Then
                    'auxObs = "<br />Comentarios:" & auxObs
                    'End If
                    Dim auxLink As String = "<a href={#DOC_LINK#}>Click aquí</a>"
                    Dim auxBody As String = "Le recordamos que debe acceder al documento ""{#DOC.DSC#}"" versión ""{#DOC.VERSION#}""" _
                        & " sobre el cual debe realizar la acción de ""{#DOC.WFWSTPNEXTDSC#}""" _
                        & "<br />{#DOCLOG.OBS#}" _
                        & " <br />Esta acción se encuentra pendiente desde el día {#DOCSGN.FECHAINICIO#}." _
                        & " <br />Este es el {#AVISOS_CANT#}° aviso que se le ha enviado. Se enviarán {#AVISOS_MAX#} avisos."
                    auxBody = "<span style=""font-family:Verdana;font-size:10.0pt;"">" _
                        & auxBody _
                        & "<br /> " _
                        & "<br />" & auxLink & "</span>"
                    'Return gEntity_MAILS_Insert(pdsc:="Aviso firma " & auxDTDOC.Rows(0)("nro") & "-" & Today.ToString("d/M/yy"), pcontent:=auxBody, psubject:=auxSubject)
                    gEntity_MAILS_Insert(pdsc:="Modelo general", _
                            psubject:=auxSubject, _
                            pcontent:=auxBody)

                End If
                If auxVersion < 200 Then
                    m_Conn.gConn_Update("UPDATE DOC_DOC SET eprcod=1 WHERE cod > 0")
                    m_Conn.gConn_Update("UPDATE DOC_DOCVIG SET eprcod=1 WHERE cod > 0")
                End If

                If auxVersion < 208 Then
                    Dim auxQueCod As Integer = m_Alerts.gQueue_Create(True, -1, -1)
                    gSystem_SetParameterByID(coSysParamIDPublicQueue, auxQueCod)
                End If
                If auxVersion < 209 Then
                    gEntity_DOC_ROL_Update(pcod:=enumRoles.coCancelador, porden:=65)
                End If

                If auxVersion < 240 Then
                    For Each auxRow As DataRow In m_Conn.gConn_Query("SELECT cod FROM DOC_DOCTIP WHERE cod > 0 ").Rows
                        gSystem_PostAction(enumEntities.coEntityDOC_DOCTIP, enumActionType.coConfirmInsert, auxRow("cod"))
                    Next
                End If

                If auxVersion < 244 Then
                    If ConfigurationManager.AppSettings("hrcJobQueue_Disabled") IsNot Nothing Then
                        gSystem_SetParameterByID(enumSysIDParams.JobQueueDisable, ConfigurationManager.AppSettings("hrcJobQueue_Disabled"))
                    End If
                    If ConfigurationManager.AppSettings("hrcJobQueue_Folder") IsNot Nothing Then
                        gSystem_SetParameterByID(enumSysIDParams.JobQueueFolder, ConfigurationManager.AppSettings("hrcJobQueue_Folder"))
                    End If
                    If ConfigurationManager.AppSettings("hrcJobQueue_TraceLog") IsNot Nothing Then
                        gSystem_SetParameterByID(enumSysIDParams.JobQueueTraceLog, ConfigurationManager.AppSettings("hrcJobQueue_TraceLog"))
                    End If

                    If ConfigurationManager.AppSettings("mail.servidor") IsNot Nothing Then
                        gSystem_SetParameterByID(enumSysIDParams.SMTPServer, ConfigurationManager.AppSettings("mail.servidor"))
                    End If
                    If ConfigurationManager.AppSettings("mail.usuario") IsNot Nothing Then
                        gSystem_SetParameterByID(enumSysIDParams.SMTPUser, ConfigurationManager.AppSettings("mail.usuario"))
                    End If
                    If ConfigurationManager.AppSettings("mail.password") IsNot Nothing Then
                        gSystem_SetParameterByID(enumSysIDParams.SMTPpsw, ConfigurationManager.AppSettings("mail.password"))
                    End If
                    If ConfigurationManager.AppSettings("mail.de") IsNot Nothing Then
                        gSystem_SetParameterByID(enumSysIDParams.SMTPfrom, ConfigurationManager.AppSettings("mail.de"))
                    End If
                    If ConfigurationManager.AppSettings("mail.smtpport") IsNot Nothing Then
                        gSystem_SetParameterByID(enumSysIDParams.SMTPport, ConfigurationManager.AppSettings("mail.smtpport"))
                    End If
                    If ConfigurationManager.AppSettings("mail.enablessl") IsNot Nothing Then
                        gSystem_SetParameterByID(enumSysIDParams.SMTPSSLEnabled, ConfigurationManager.AppSettings("mail.enablessl"))
                    End If
                    If ConfigurationManager.AppSettings("siteurl") IsNot Nothing Then
                        gSystem_SetParameterByID(enumSysIDParams.ExternalURL, ConfigurationManager.AppSettings("siteurl"))
                    End If
                    Dim auxWSSystempwd As String = "demo"
                    If ConfigurationManager.AppSettings("wssystempwd") IsNot Nothing Then
                        auxWSSystempwd = ConfigurationManager.AppSettings("wssystempwd")
                    End If
                    gSystem_SetParameterByID(enumSysIDParams.WSsystempsw, auxWSSystempwd)
                    gSystem_SetParameterByID(enumSysIDParams.SystemTitle, "_intelimedia QDOC") '<- El default es dOCUMENTADOR
                    gSystem_SetParameterByID(enumSysIDParams.SystemType, "100175")  '<- El default es 175


                    Dim auxSystemUniqueID As String = gSystem_GetParameterByID(enumSysIDParams.SystemUniqueID)
                    If auxSystemUniqueID = "" Then
                        auxSystemUniqueID = m_Conn.gField_GetUniqueID()
                        gSystem_SetParameterByID(enumSysIDParams.SystemUniqueID, auxSystemUniqueID)
                    End If
                End If
                If auxVersion < 245 Then
                    gSystem_SetParameterByID(coSysParamCheckDuplicateTitle, "1")
                    m_Conn.gConn_ExecuteProcedureUpdate("UPDATE Q_SYSPARAM SET sysparamobs='Habilita chequeo de duplicados de títulos' WHERE sysparamID=" & coSysParamCheckDuplicateTitle)
                    gSystem_SetParameterByID(coSysParamInboxDetailedEnabled, "0")
                    m_Conn.gConn_ExecuteProcedureUpdate("UPDATE Q_SYSPARAM SET sysparamobs='Vigentes-Habilita columnas detalladas' WHERE sysparamID=" & coSysParamInboxDetailedEnabled)
                    m_Conn.gConn_ExecuteProcedureUpdate("UPDATE Q_SYSPARAM SET sysparamobs='Empresa-Nombre' WHERE sysparamID=" & coSysParamEprDsc)
                End If
                If auxVersion < 247 Then
                    m_Conn.gConn_ExecuteProcedureUpdate("UPDATE Q_SYSPARAM SET sysparamobs='Cantidad de avisos' WHERE sysparamID=" & coSysParamIDAvisosCantMax)
                    m_Conn.gConn_ExecuteProcedureUpdate("UPDATE Q_SYSPARAM SET sysparamobs='Días de aviso' WHERE sysparamID=" & coSysParamIDAvisosDias)
                    hrcProcessQueue.gJobStore_Del("JOBSCHEDULING")
                    Dim auxSchedule As clsHrcSchedules
                    Dim auxparam As Intelimedia.inTasks.clsTaskinQueue
                    Dim auxTask As clsCustomBasicTask

                    'Crea tarea de doc_avisos
                    auxTask = New clsCustomBasicTask
                    auxTask.BagValues.gValue_Add("task_type", "doc_avisos")
                    auxparam = New Intelimedia.inTasks.clsTaskinQueue
                    auxparam.ExecutionDateFrom = Now
                    auxparam.ExecutionTitle = "Documentos | Avisos diarios"
                    auxparam.ExecutionJobID = "doc_avisos"
                    auxparam.ExecutionJobVersion = 2
                    auxSchedule = New clsHrcSchedules
                    auxSchedule.gSchedule_AddTime(New TimeSpan(1, 0, 0))
                    auxparam.ExecutionSchedule = auxSchedule
                    auxparam.Tasks.Add(1, auxTask)
                    hrcProcessQueue.gProcessor_AddTask(auxparam)
                End If
                If auxVersion < 248 Then
                    gSystem_SetParameterByID(coSysParamIDModoObligatorioSoloVigentes, "0")
                    m_Conn.gConn_ExecuteProcedureUpdate("UPDATE Q_SYSPARAM SET sysparamobs='Modo obligatorio sólo para pendientes de lectura' WHERE sysparamID=" & coSysParamIDModoObligatorioSoloVigentes)
                End If
                If auxVersion < 249 Then
                    gSystem_SetParameterByID(coSysParamIDAprobadorNoLee, "0")
                    m_Conn.gConn_ExecuteProcedureUpdate("UPDATE Q_SYSPARAM SET sysparamobs='Aprobador no requiere lectura' WHERE sysparamID=" & coSysParamIDAprobadorNoLee)
                    m_Conn.gConn_ExecuteProcedureUpdate("UPDATE Q_SYSPARAM SET sysparamid=20101 WHERE sysparamID=101")
                    m_Conn.gConn_ExecuteProcedureUpdate("UPDATE Q_SYSPARAM SET sysparamid=20102 WHERE sysparamID=102")
                    m_Conn.gConn_ExecuteProcedureUpdate("UPDATE Q_SYSPARAM SET sysparamid=20103 WHERE sysparamID=103")
                    m_Conn.gConn_ExecuteProcedureUpdate("UPDATE Q_SYSPARAM SET sysparamid=20104 WHERE sysparamID=104")
                    m_Conn.gConn_ExecuteProcedureUpdate("UPDATE Q_SYSPARAM SET sysparamid=20105 WHERE sysparamID=105")
                    m_Conn.gConn_ExecuteProcedureUpdate("UPDATE Q_SYSPARAM SET sysparamid=20106 WHERE sysparamID=106")
                    m_Conn.gConn_ExecuteProcedureUpdate("UPDATE Q_SYSPARAM SET sysparamid=20107 WHERE sysparamID=107")
                    gSystem_ChargeStatic(True)
                End If
                If auxVersion < 250 Then
                    For Each auxRow As DataRow In m_Conn.gConn_Query("SELECT cod,qsidcod FROM DOC_DOC WHERE cod > 0 ").Rows
                        Dim auxSidCod As Integer = m_Conn.gField_GetInt(auxRow("qsidcod"), -1)
                        If auxSidCod > 0 Then
                            Dim auxAclCod As Integer = m_Security.gACL_GetFromSIDcod(auxSidCod)
                            For Each auxSecRow As DataRow In m_Security.gACL_GetEntries(auxAclCod, enumAccessType.coSYSImprimircopiascontroladas, Nothing).Rows
                                '                            If auxSecRow("acctypecod") = enumAccessType.coSYSImprimircopiadas Then
                                m_Security.gACL_AddSID(auxAclCod, auxSecRow("sidcod"), enumAccessType.coSYSImprimircopiasnocontroladas)
                                'End If
                            Next
                        End If
                    Next
                End If
                If auxVersion < 252 Then
                    gEntity_DOC_DOCTIP_Update(pcod:=1, porden:=1)
                    gEntity_DOC_DOCTIP_Update(pcod:=2, porden:=2)
                    gEntity_DOC_DOCTIP_Update(pcod:=3, porden:=3)
                    If Val(coSystemType) = 175 Then
                        gEntity_DOC_DOCTIP_Update(pcod:=10, porden:=4)
                        gEntity_DOC_DOCTIP_Update(pcod:=5, porden:=5)
                    Else
                        gEntity_DOC_DOCTIP_Update(pcod:=4, porden:=4)
                    End If

                End If
                If auxVersion < 256 Then
                    m_Security.gMode_ChangeTo2()
                End If
                If auxVersion < 257 Then
                    m_Security.gGroup_AddGroup(m_Security.gGroup_GetCodByID(coGroupDocumentadorAdministradores), _
                                             m_Security.gGroup_GetCodByID(coGroupIDAdministradores))
                End If
                If auxVersion < 296 Then
                    gEntity_DOC_ROL_Insert(pcod:=CInt(enumRoles.coVisualizador), pdsc:="Visualizador", porden:=65)
                    m_Conn.gConn_Update("UPDATE DOC_EQUMBRUND set gruporesp=1,grupomiembros=0,grupoeditores=0 where equmbrcod > 0")
                    Dim auxBagValues As New clshrcBagValues
                    For Each auxRow As DataRow In m_Conn.gConn_Query("SELECT cod FROM UND WHERE cod > 0 ").Rows
                        auxBagValues = m_Conn.gField_GetBagValuesFromArray(auxRow.ItemArray, auxRow.Table.Columns)
                        gEntityUND_PostAction(enumActionType.coConfirmModify, auxBagValues, auxBagValues)
                    Next
                End If
                If auxVersion < 301 Then
                    Dim auxSchedule As clsHrcSchedules
                    Dim auxparam As Intelimedia.inTasks.clsTaskinQueue
                    Dim auxTask As clsCustomBasicTask

                    'Crea tarea de replication
                    auxTask = New clsCustomBasicTask
                    auxTask.BagValues.gValue_Add("task_type", "jobs_replication")
                    auxparam = New Intelimedia.inTasks.clsTaskinQueue
                    auxparam.ExecutionDateFrom = Now
                    auxparam.ExecutionTitle = "Sistema| Replicación"
                    auxparam.ExecutionJobID = "jobs_replication"
                    auxparam.ExecutionJobVersion = 1
                    auxSchedule = New clsHrcSchedules
                    auxSchedule.gSchedule_AddTime(New TimeSpan(1, 0, 0))
                    auxparam.ExecutionSchedule = auxSchedule
                    auxparam.Tasks.Add(1, auxTask)
                    hrcProcessQueue.gProcessor_AddTask(auxparam)
                End If

                If auxVersion < 302 Then
                    m_Conn.gConn_Update("UPDATE DOC_DOCMBRUND set rolunidad=" & enumUND_Roles.coResponsable _
                                                & " WHERE rolunidad=" & enumUND_Roles.coMiembro)
                End If
                If auxVersion < 304 Then
                    m_Security.gACL_AddGroupByID(coGroupDocumentadorEditores, enumAccessType.coSYSGlobalModificar)
                    m_Security.gACL_AddGroupByID(coGroupDocumentadorEditores, enumAccessType.coSYSGlobalEliminar)
                End If
                If auxVersion < 305 Then
                    For Each auxRow As DataRow In m_Conn.gConn_Query("SELECT cod FROM EMP WHERE cod > 0").Rows
                        gSystem_PostAction(enumEntities.coEntityEMP, enumActionType.coConfirmModify, auxRow("cod"))
                    Next
                End If
                If auxVersion < 327 Then
                    gSystem_CreateParameterByID(coSysParamIDModoObligatorioAdmins, "Modo obligatorio habilitado para administradores")
                    gSystem_CreateParameterByID(coSysParamIDModoObligatorioEditors, "Modo obligatorio habilitado para editores")
                End If
                If auxVersion < 329 Then
                    Dim auxSchedule As clsHrcSchedules
                    Dim auxparam As Intelimedia.inTasks.clsTaskinQueue
                    Dim auxTask As clsCustomBasicTask

                    'Crea tarea de doc_avisos
                    auxTask = New clsCustomBasicTask
                    auxTask.BagValues.gValue_Add("task_type", "doc_avisos")
                    auxparam = New Intelimedia.inTasks.clsTaskinQueue
                    auxparam.ExecutionDateFrom = Now
                    auxparam.ExecutionTitle = "Documentos | Avisos diarios"
                    auxparam.ExecutionJobID = "doc_avisos"
                    auxparam.ExecutionJobVersion = 2
                    auxSchedule = New clsHrcSchedules
                    auxSchedule.gSchedule_AddTime(New TimeSpan(1, 0, 0))
                    auxparam.ExecutionSchedule = auxSchedule
                    auxparam.Tasks.Add(1, auxTask)
                    hrcProcessQueue.gProcessor_AddTask(auxparam)

                    'Crea tarea de replication
                    auxTask = New clsCustomBasicTask
                    auxTask.BagValues.gValue_Add("task_type", "jobs_replication")
                    auxparam = New Intelimedia.inTasks.clsTaskinQueue
                    auxparam.ExecutionDateFrom = Now
                    auxparam.ExecutionTitle = "Sistema| Replicación"
                    auxparam.ExecutionJobID = "jobs_replication"
                    auxparam.ExecutionJobVersion = 1
                    auxSchedule = New clsHrcSchedules
                    auxSchedule.gSchedule_AddTime(New TimeSpan(1, 0, 0))
                    auxparam.ExecutionSchedule = auxSchedule
                    auxparam.Tasks.Add(1, auxTask)
                    hrcProcessQueue.gProcessor_AddTask(auxparam)

                    gSystem_CreateParameterByID(coSysParamIDModoObligatorio, "Modo obligatorio habilitado")
                    gSystem_SetParameterByID(coSysParamIDModoObligatorio, "1")
                End If
                If auxVersion < 341 Then
                    Dim auxGrpCod As Integer = m_Security.gGroup_GetCodByID(coGroupIDDocumentadorCreadores)
                    m_Security.gACL_AddGroup(auxGrpCod, enumAccessType.coSYSCreardocumento)
                    m_Security.gACL_AddGroup(auxGrpCod, enumAccessType.coSYSBuscar)
                    gEntity_DOC_EQU_SystemInsert(pdsc:="Documentador-Creadores de documentos", pundcod:=-1, pmiembrosgrpcod:=auxGrpCod)
                End If
                If auxVersion < 342 Then
                    gSystem_CreateParameterByID(coSysParamEmpShowImage, "Colaboradores-Mostrar imágen")
                End If
                If auxVersion < 343 Then
                    gSystem_CreateParameterByID(coSysParamIDObsVisible, "Documentos-Ver descripción detallada")
                    If coSystemType = "175" Then
                        gSystem_SetParameterByID(coSysParamIDObsVisible, "False")
                    Else
                        gSystem_SetParameterByID(coSysParamIDObsVisible, "True")
                    End If



                    gEntity_DOC_PRNCFG_SystemInsert(pcod:=1, pdsc:="Versión 1", pfont10px:="5mm", pfont12px:="6mm", pfont14px:="7mm", pfont16px:="8mm", pfont18px:="9mm", pfont20px:="10mm", pfont22px:="11mm")
                    gEntity_DOC_PRNCFG_SystemInsert(pcod:=2, pdsc:="Versión 2", pfont10px:="9mm", pfont12px:="10mm", pfont14px:="11mm", pfont16px:="12mm", pfont18px:="13mm", pfont20px:="14mm", pfont22px:="15mm")
                    gSystem_CreateParameterByID(coSysParamIDPrnCfgCodDefault, "")
                    gSystem_SetParameterByID(coSysParamIDPrnCfgCodDefault, "2")

                    m_Conn.gConn_Update("UPDATE DOC_DOC SET prncfgcod=2 WHERE cod > 0 " _
                                        & " AND wfwstatus IN (" & enumWorkflowStep.coWFWSTPDOC_DOCEdicion _
                                        & "," & enumWorkflowStep.coWFWSTPDOC_DOCRevisionSSG & ")")

                    m_Conn.gConn_Update("UPDATE DOC_DOC SET prncfgcod=1 WHERE cod > 0 " _
                                        & " AND wfwstatus NOT IN (" & enumWorkflowStep.coWFWSTPDOC_DOCEdicion _
                                        & "," & enumWorkflowStep.coWFWSTPDOC_DOCRevisionSSG & ")")

                    m_Conn.gConn_Update("UPDATE DOC_DOCVIG SET prncfgcod=1 WHERE cod > 0")
                End If
                If auxVersion < 350 Then
                    gEntity_DOC_PRNCFG_SystemUpdate(pcod:=1, pdsc:="Versión 1", pfont8px:="4mm", pfont9px:="4.5mm", pfont10px:="5mm", pfont11px:="5.5mm", pfont12px:="6mm", pfont14px:="7mm", pfont16px:="8mm", pfont18px:="9mm", pfont20px:="10mm", pfont22px:="11mm", pfont24px:="12mm", pfont26px:="13mm", pfont28px:="14mm", pfont36px:="16mm", pfont48px:="20mm", pfont72px:="26mm")
                    gEntity_DOC_PRNCFG_SystemUpdate(pcod:=2, pdsc:="Versión 2", pfont8px:="8mm", pfont9px:="8.5mm", pfont10px:="9mm", pfont11px:="9.5mm", pfont12px:="10mm", pfont14px:="11mm", pfont16px:="12mm", pfont18px:="13mm", pfont20px:="14mm", pfont22px:="15mm", pfont24px:="16mm", pfont26px:="17mm", pfont28px:="18mm", pfont36px:="20mm", pfont48px:="24mm", pfont72px:="30mm")
                End If
                If auxVersion < 351 Then
                    m_Conn.gConn_Update("UPDATE DOC_DOC SET wfwmode=" & enumWfwMode.coStandard & " WHERE cod > 0")
                    gSystem_CreateParameterByID(coSysParamIDDOCReIDAuto, "Documentos-Reidentificación automática")
                    gSystem_SetParameterByID(coSysParamIDDOCReIDAuto, "True")
                End If
                If auxVersion < 366 Then
                    gSystem_CreateParameterByID(coSysParamIDDOCViewDefault, "Documentos-Vista predeterminada")
                    gSystem_SetParameterByID(coSysParamIDDOCViewDefault, "2")
                End If
                If auxVersion < 368 Then
                    gSystem_CreateParameterByID(coSysParamIDReqURL, "Requio-URL")
                End If
                If auxVersion < 373 Then
                    gSystem_CreateParameterByID(coSysParamIDPrnCopyBackImageDisabled, "Documentos-Fondo deshabilitado en impresión copia no controlada")
                    gSystem_CreateParameterByID(coSysParamIDPrnOnlineBackImageDisabled, "Documentos-Fondo deshabilitado en impresión online")
                    'gSystem_SetParameterByID(coSysParamIDPrnCopyBackImageDisabled, "2")
                End If
                If auxVersion < 374 Then
                    gSystem_CreateParameterByID(coSysParamIDDOCAutoEditionDaysCicle, "Documentos-Ciclo de edición automática(días)")
                    If Val(coSystemType) = 175 Then
                        gSystem_SetParameterByID(coSysParamIDDOCAutoEditionDaysCicle, "365")
                    End If
                End If
                If auxVersion < 375 Then
                    gSystem_CreateParameterByID(coSysParamIDDOCNroEditEnabled, "Documentos-Edición de número habilitada")
                    gSystem_SetParameterByID(coSysParamIDDOCNroEditEnabled, "1")
                    If coSystemType = "175" Then
                    Else
                        gEntity_DOCTIP_Insert("Varios")
                    End If
                End If
                If auxVersion < 376 Then
                    If coSystemType = "175" Then
                    Else
                        For Each auxRow As DataRow In m_Conn.gConn_Query("SELECT cod FROM UND WHERE COD > 0").Rows
                            Dim auxStaticRow As DataRow = hrcEntityDT_UND_FindByKey(auxRow("cod"))
                            auxStaticRow("resp") = -1
                            gSystem_PostAction(enumEntities.coEntityUND, enumActionType.coConfirmModify, auxRow("cod"))
                        Next
                    End If
                End If
                If auxVersion < 389 Then
                    m_Conn.gConn_Update("UPDATE DOC_EQU SET miembrosgrpcod=grpcod")
                    m_Conn.gConn_Update("UPDATE DOC_EQUMBR SET equcod=wilequcod")
                End If
                If auxVersion < 394 Then
                    gSystem_CreateParameterByID(coSysParamRepliDSCore, "Replicación-Core-Datasource")
                    Dim auxConnectionString As String = ""
                    Try
                        auxConnectionString = ConfigurationManager.ConnectionStrings("imDoc_rplSource").ConnectionString
                    Catch ex As Exception

                    End Try
                    gSystem_SetParameterByID(coSysParamRepliDSCore, auxConnectionString)
                End If
                If auxVersion < 396 Then

                End If
                If auxVersion < 400 Then
                    gSystem_CreateParameterByID(coSysParamRepliEQU, "Replicación-Incluir equipos")
                    gSystem_SetParameterByID(coSysParamRepliEQU, "0")
                End If
                If auxVersion < 402 Then
                    Dim auxReplication As clsHrcReplicationClient
                    auxReplication = hrcReplicationServer.gComponent_CreateInstance(Me)
                    'Dim auxDTResults As DataTable = Nothing
                    'auxReplication.gReplication_Configure()

                End If
                If auxVersion < 404 Then
                    gSystem_CreateParameterByID(coSysParamIDDOCDscxVisible, "Documentos-Ver subtítulos")
                    If coSystemType = "175" Then
                        gSystem_SetParameterByID(coSysParamIDDOCDscxVisible, "True")
                    Else
                        gSystem_SetParameterByID(coSysParamIDDOCDscxVisible, "False")
                    End If
                End If
                If auxVersion < 406 Then
                    m_Conn.gConn_Update("UPDATE DOC_DOC SET dsc0=dsc WHERE cod > 0")
                    Dim auxDsc As String
                    For Each auxRow As DataRow In m_Conn.gConn_Query("SELECT cod,dsc,dsc0,dsc1,dsc2 FROM DOC_DOC WHERE cod > 0").Rows
                        auxDsc = auxRow("dsc0").ToString
                        If IsDBNull(auxRow("dsc1")) = False Then
                            auxDsc &= " " & auxRow("dsc1").ToString.Trim
                        End If
                        If IsDBNull(auxRow("dsc2")) = False Then
                            auxDsc &= " " & auxRow("dsc2").ToString.Trim
                        End If
                        gEntity_DOC_DOC_SystemUpdate(pcod:=auxRow("cod"), pdsc:=auxDsc)
                    Next
                    'VIGENTES
                    m_Conn.gConn_Update("UPDATE DOC_DOCVIG SET dsc0=dsc WHERE cod > 0")
                    For Each auxRow As DataRow In m_Conn.gConn_Query("SELECT cod,dsc,dsc0,dsc1,dsc2 FROM DOC_DOCVIG WHERE cod > 0").Rows
                        auxDsc = auxRow("dsc0").ToString
                        If IsDBNull(auxRow("dsc1")) = False Then
                            auxDsc &= " " & auxRow("dsc1").ToString.Trim
                        End If
                        If IsDBNull(auxRow("dsc2")) = False Then
                            auxDsc &= " " & auxRow("dsc2").ToString.Trim
                        End If
                        gEntity_DOC_DOCVIG_SystemUpdate(pcod:=auxRow("cod"), pdsc:=auxDsc)
                    Next
                End If
                If auxVersion < 408 Then
                    gSystem_CreateParameterByID(coSysParamIDDOCSGNUpdateEditionRoleDaily, "Documentos-Actualizar firmas de edición diariamente")
                    gSystem_SetParameterByID(coSysParamIDDOCSGNUpdateEditionRoleDaily, "True")
                    'For Each auxRow As DataRow In m_Conn.gConn_Query("SELECT cod FROM DOC_DOC " _
                    '                                                 & " WHERE wfwstatus=" & enumWorkflowStep.coWFWSTPDOC_DOCEdicion).Rows
                    '    gDoc_ReApply(auxRow("cod"))
                    'Next
                End If
                If auxVersion < 409 Then
                    gSystem_CreateParameterByID(coSysParamIDUNDCreatorsMBRDIRDefault, "Unidades-Miembros directos crean documentos")
                    gSystem_SetParameterByID(coSysParamIDUNDCreatorsMBRDIRDefault, "False")

                End If
                If auxVersion < 412 Then
                    gSystem_CreateParameterByID(coSysParamIDDOCAllEditoresRequired, "Documentos-Se requiere la firma de todos los editores")
                    gSystem_SetParameterByID(coSysParamIDDOCAllEditoresRequired, "True")

                    gSystem_CreateParameterByID(coSysParamIDDOCAllRevisorRequired, "Documentos-Se requiere la firma de todos los revisores")
                    gSystem_SetParameterByID(coSysParamIDDOCAllRevisorRequired, "True")

                    gSystem_CreateParameterByID(coSysParamIDDOCAllAprobadorRequired, "Documentos-Se requiere la firma de todos los aprobadores")
                    gSystem_SetParameterByID(coSysParamIDDOCAllAprobadorRequired, "True")

                    gSystem_CreateParameterByID(coSysParamIDDOCAllPublicadorRequired, "Documentos-Se requiere la firma de todos los publicadores")
                    gSystem_SetParameterByID(coSysParamIDDOCAllPublicadorRequired, "False")
                End If
                If auxVersion < 413 Then
                    Dim auxSchedule As clsHrcSchedules
                    Dim auxparam As Intelimedia.inTasks.clsTaskinQueue
                    Dim auxTask As clsCustomBasicTask

                    'Actualiza la tarea diaria, con timeout de 2 hs
                    auxTask = New clsCustomBasicTask
                    auxTask.BagValues.gValue_Add("task_type", "doc_avisos")
                    auxparam = New Intelimedia.inTasks.clsTaskinQueue
                    auxparam.ExecutionTimeOut = 2 * 60 * 60   ' 2 horas
                    auxparam.ExecutionDateFrom = Now
                    auxparam.ExecutionTitle = "Documentos | Tareas diarias"
                    auxparam.ExecutionJobID = "doc_avisos"
                    auxparam.ExecutionJobVersion = 3
                    auxSchedule = New clsHrcSchedules
                    auxSchedule.gSchedule_AddTime(New TimeSpan(1, 0, 0))
                    auxparam.ExecutionSchedule = auxSchedule
                    auxparam.Tasks.Add(1, auxTask)
                    hrcProcessQueue.gProcessor_AddTask(auxparam)
                    gSystem_CreateParameterByID(coSysParamIDDOCDeleteTotalEnabled, "Documentos-Eliminación total habilitada")
                    gSystem_SetParameterByID(coSysParamIDDOCDeleteTotalEnabled, "0")
                End If
                If auxVersion < 414 Then
                    m_Conn.gConn_Delete("DELETE FROM DOC_DOCSGN " _
                                        & " WHERE cod > 0 " _
                                        & " AND doccod NOT IN (" _
                                        & "SELECT cod FROM DOC_DOC WHERE (baja = {#FALSE#} OR baja {#ISNULL#})" _
                                        & ")")
                End If
                If auxVersion < 417 Then
                    gEntity_ROLGRP_SystemInsert(pcod:=enumUND_Roles.coMiembroDirecto, pdsc:="Miembro directo")
                    gEntity_ROLGRP_SystemInsert(pcod:=enumUND_Roles.coResponsable, pdsc:="Responsable")
                    gEntity_ROLGRP_SystemInsert(pcod:=enumUND_Roles.coMiembro, pdsc:="Miembros")
                    gEntity_ROLGRP_SystemInsert(pcod:=enumUND_Roles.coSuperior, pdsc:="Superiores")
                    gEntity_ROLGRP_SystemInsert(pcod:=enumUND_Roles.coEditorDocs, pdsc:="Editor")

                    gEntity_DOC_ROL_Insert(pcod:=CInt(enumRoles.coReceptor), pdsc:="Resp.versiones", porden:=8)
                    Dim auxTroCod As Integer
                    Dim auxUNDrolgrpCod As Integer
                    Dim auxTroRolCod As Integer
                    Dim auxRolcodtype As Integer
                    For Each auxRow As DataRow In m_Conn.gConn_Query("SELECT cod,trocodcustom FROM DOC_DOC " _
                                                                      & " WHERE cod >0" _
                                                                      & " AND (BAJA {#ISNULL#} OR BAJA ={#FALSE#})" _
                                                                      & " AND (trocodcustomenabled = {#FALSE#} OR trocodcustomenabled {#ISNULL#})").Rows
                        auxTroCod = m_Conn.gField_GetInt(auxRow("trocodcustom"), -1)
                        If auxTroCod < 1 Then
                            auxTroCod = gEntity_DOC_TRO_SystemInsert(pdsc:="Roles personalizados", pcustom:=True)
                            gEntity_DOC_DOC_SystemUpdate(pcod:=auxRow("cod"), ptrocodcustom:=auxTroCod, ptrocodcustomenabled:=True)
                            For Each auxRowMBR As DataRow In m_Conn.gConn_Query("SELECT DOC_DOCMBR.docmbrcod,DOC_DOCMBR.rolcod,DOC_DOCMBR.docmbrtype " _
                                                                     & ",DOC_DOCMBRUSU.percod" _
                                                                     & ",DOC_DOCMBRUND.undcod,DOC_DOCMBRUND.rolunidad" _
                                                                     & ",DOC_DOCMBREQU.equcod" _
                                                                     & " FROM DOC_DOCMBR " _
                                                                     & " LEFT JOIN DOC_DOCMBRUND ON DOC_DOCMBRUND.docmbrcod = DOC_DOCMBR.docmbrcod " _
                                                                     & " LEFT JOIN DOC_DOCMBRUSU ON DOC_DOCMBRUSU.docmbrcod = DOC_DOCMBR.docmbrcod " _
                                                                     & " LEFT JOIN DOC_DOCMBREQU ON DOC_DOCMBREQU.docmbrcod = DOC_DOCMBR.docmbrcod " _
                                                                     & " WHERE DOC_DOCMBR.DOCCOD =" & auxRow("COD")).Rows
                                auxRolcodtype = m_Conn.gField_GetInt(auxRowMBR("docmbrtype"), -1)
                                If auxRolcodtype < 1 Then
                                    auxRowMBR("docmbrtype") = enumEntities.coEntityEMP
                                End If
                                auxTroRolCod = gEntity_DOC_TROROL_SystemInsert(ptrocod:=auxTroCod, _
                                                                               prolcod:=auxRowMBR("rolcod"), _
                                                                               prolcodtype:=auxRolcodtype)

                                Select Case auxRowMBR("docmbrtype")
                                    Case enumEntities.coEntityEMP
                                        gEntity_DOC_TROROLEMP_Insert(ptrorolcod:=auxTroRolCod, pempcod:=m_Conn.gField_GetInt(auxRowMBR("percod"), -1))
                                    Case enumEntities.coEntityUND
                                        auxUNDrolgrpCod = -1
                                        Select Case m_Conn.gField_GetInt(auxRowMBR("rolunidad"))
                                            Case 1
                                                auxUNDrolgrpCod = enumUND_Roles.coResponsable
                                            Case 2
                                                auxUNDrolgrpCod = enumUND_Roles.coMiembro
                                            Case 3
                                                auxUNDrolgrpCod = enumUND_Roles.coEditorDocs
                                        End Select
                                        gEntity_DOC_TROROLUND_Insert(ptrorolcod:=auxTroRolCod, pundcod:=m_Conn.gField_GetInt(auxRowMBR("undcod"), -1), prolgrpcod:=auxUNDrolgrpCod)
                                    Case enumEntities.coEntityDOC_EQU
                                        gEntity_DOC_TROROLEQU_Insert(ptrorolcod:=auxTroRolCod, pequcod:=m_Conn.gField_GetInt(auxRowMBR("equcod"), -1))
                                End Select

                            Next
                        End If
                    Next
                End If
                If auxVersion < 424 Then
                    gEntity_DOC_DYNGRP_Insert(pcod:=enumIdentidadesEspeciales.coAprobador, pdsc:="Aprobadores")
                    gEntity_DOC_DYNGRP_Insert(pcod:=enumIdentidadesEspeciales.coEditores, pdsc:="Editores")
                End If
                If auxVersion < 429 Then
                    gSystem_CreateParameterByID(coSysParamIDDocAutoEditionAlertSubject, "Documentos-Texto alertas edición automática")
                    gSystem_SetParameterByID(coSysParamIDDocAutoEditionAlertSubject, "Revisión anual")
                End If
                If auxVersion < 430 Then
                    gSystem_CreateParameterByID(coSysParamIDDocVersionadorEditoresDefault, "Documentos-Editores son versionadores cuando no exista el rol")
                    If Val(coSystemType) = 175 Then
                        'williner
                        gSystem_SetParameterByID(coSysParamIDDocVersionadorEditoresDefault, "0")
                    Else
                        gSystem_SetParameterByID(coSysParamIDDocVersionadorEditoresDefault, "1")
                    End If
                    gSystem_CreateParameterByID(coSysParamIDDocVersionadorAprobadoresDefault, "Documentos-Aprobadores son versionadores cuando no exista el rol")
                    If Val(coSystemType) = 175 Then
                        'williner
                        gSystem_SetParameterByID(coSysParamIDDocVersionadorAprobadoresDefault, "1")
                    Else
                        gSystem_SetParameterByID(coSysParamIDDocVersionadorAprobadoresDefault, "0")
                    End If

                    'Ajusta los títulos del histórico
                    m_Conn.gConn_Update("UPDATE DOC_DOC_HST SET dsc0=dsc where dsc0 IS NULL and DSC1 is null and dsc2 is null AND hstcod > 0")
                End If
                If auxVersion < 435 Then
                    Dim auxSchedule As clsHrcSchedules
                    Dim auxparam As Intelimedia.inTasks.clsTaskinQueue
                    Dim auxTask As clsCustomBasicTask

                    'Actualiza la tarea diaria, con timeout de 2 hs
                    auxTask = New clsCustomBasicTask
                    auxTask.BagValues.gValue_Add("task_type", "doc_avisos")
                    auxparam = New Intelimedia.inTasks.clsTaskinQueue
                    auxparam.ExecutionTimeOut = 2 * 60 * 60 * 1000  ' 2 horas
                    auxparam.ExecutionDateFrom = Now
                    auxparam.ExecutionTitle = "Documentos | Tareas diarias"
                    auxparam.ExecutionJobID = "doc_avisos"
                    auxparam.ExecutionJobVersion = 4
                    auxSchedule = New clsHrcSchedules
                    auxSchedule.gSchedule_AddTime(New TimeSpan(1, 0, 0))
                    auxparam.ExecutionSchedule = auxSchedule
                    auxparam.Tasks.Add(1, auxTask)
                    hrcProcessQueue.gProcessor_AddTask(auxparam)
                End If

                If auxVersion < 437 Then
                    'Cambia el nuevo permiso coDOCDOCDocumentosVer > ConfirmarLectura
                    Dim auxDeletions As Boolean = False
                    For Each auxRow As DataRow In m_Conn.gConn_Query("SELECT cod,qsidcod FROM DOC_DOC WHERE cod > 0 ").Rows
                        Dim auxSidCod As Integer = m_Conn.gField_GetInt(auxRow("qsidcod"), -1)
                        If auxSidCod > 0 Then
                            auxDeletions = False
                            Dim auxAclCod As Integer = m_Security.gACL_GetFromSIDcod(auxSidCod)
                            For Each auxSecRow As DataRow In m_Security.gACL_GetEntries(auxAclCod, enumAccessType.coDOCDOCDocumentosVer, Nothing).Rows
                                m_Security.gACL_AddSID(auxAclCod, auxSecRow("sidcod"), enumAccessType.coSYSConfirmarlectura)
                                auxDeletions = True
                            Next
                            If auxDeletions Then
                                For Each auxSecRow As DataRow In m_Security.gACL_GetEntries(auxAclCod, enumAccessType.coDOCDOCDocumentosVer, Nothing).Rows
                                    m_Security.gACL_DelEntry(auxSecRow("acecod"))
                                Next
                            End If

                        End If
                    Next
                End If
                If auxVersion < 438 Then
                    Dim auxGrpCod As Integer
                    auxGrpCod = m_Security.gGroup_Create(pGrpDsc:="Equipo-Documentador-Visualizadores", pgrpinherit:=-1, pGrpID:=coGroupDocumentadorVisualizadores)
                    m_Security.gACL_AddGroup(auxGrpCod, enumAccessType.coSYSBuscar)
                    gEntity_DOC_EQU_SystemInsert(pdsc:="Documentador-Visualizadores", pundcod:=-1, pmiembrosgrpcod:=auxGrpCod)

                    For Each auxRow As DataRow In m_Conn.gConn_Query("SELECT cod,qsidcod FROM DOC_DOC WHERE cod > 0 ").Rows
                        Dim auxSidCod As Integer = m_Conn.gField_GetInt(auxRow("qsidcod"), -1)
                        If auxSidCod > 0 Then
                            Dim auxAclCod As Integer = m_Security.gACL_GetFromSIDcod(auxSidCod)
                            m_Security.gACL_AddGroup(auxAclCod, auxGrpCod, enumAccessType.coSYSGlobalLeer)
                        End If
                    Next
                End If
                If auxVersion < 442 Then
                    gSystem_CreateParameterByID(coSysParamIDPrnMarginBottomMM, "PDF-Margen inferior(milímetros)")
                    gSystem_CreateParameterByID(coSysParamIDPrnMarginTopMM, "PDF-Margen superior(milímetros)")
                    gSystem_CreateParameterByID(coSysParamIDPrnMarginLeftMM, "PDF-Margen izquierdo(milímetros)")
                    gSystem_CreateParameterByID(coSysParamIDPrnMarginRightMM, "PDF-Margen derecho(milímetros)")
                End If
                If auxVersion < 444 Then
                    m_Conn.gConn_Update("UPDATE DOC_DOC SET cuerpo=REPLACE(cuerpo,'http://siteurl/','') WHERE cod > 0")
                    m_Conn.gConn_Update("UPDATE DOC_DOCVIG SET cuerpo=REPLACE(cuerpo,'http://siteurl/','') WHERE cod > 0")
                End If
                If auxVersion < 449 Then
                    m_Conn.gConn_Update("UPDATE DOC_TRO SET CUSTOM=" & m_Conn.gFieldDB_GetBoolean(True) _
                                        & " WHERE dsc IN ('Roles','Roles personalizados')")
                End If
                If auxVersion < 450 Then
                    Dim auxSchedule As clsHrcSchedules
                    Dim auxparam As Intelimedia.inTasks.clsTaskinQueue
                    Dim auxTask As clsCustomBasicTask

                    'Actualiza la tarea diaria, con timeout de 2 hs
                    auxTask = New clsCustomBasicTask
                    auxTask.BagValues.gValue_Add("task_type", "doc_avisos")
                    auxparam = New Intelimedia.inTasks.clsTaskinQueue
                    auxparam.ExecutionTimeOut = 3 * 60 * 60   ' 3 horas
                    auxparam.ExecutionDateFrom = Now
                    auxparam.ExecutionTitle = "Documentos | Tareas diarias"
                    auxparam.ExecutionJobID = "doc_avisos"
                    auxparam.ExecutionJobVersion = 5
                    auxSchedule = New clsHrcSchedules
                    auxSchedule.gSchedule_AddTimeinLocalTime(New TimeSpan(1, 0, 0))
                    auxparam.ExecutionSchedule = auxSchedule
                    auxparam.Tasks.Add(1, auxTask)
                    hrcProcessQueue.gProcessor_AddTask(auxparam)
                End If
                If auxVersion < 451 Then
                    gSystem_CreateParameterByID(coSysParamIDDocContentTypeIDDefault, "Documentos-Tipo de contenido principal predeterminado")
                    gSystem_SetParameterByID(coSysParamIDDocContentTypeIDDefault, CInt(clsHrcConnClient.enumMimeTypes.coHTML))
                    m_Conn.gConn_Update("UPDATE DOC_DOC " _
                                        & " SET contenttypeid=" & CInt(clsHrcConnClient.enumMimeTypes.coHTML) _
                                        & " WHERE cod > 0 AND contenttypid {#ISNULL#} OR contenttypid=0")
                    m_Conn.gConn_Update("UPDATE DOC_DOCVIG " _
                                        & " SET contenttypeid=" & CInt(clsHrcConnClient.enumMimeTypes.coHTML) _
                                        & " WHERE  cod > 0 AND contenttypid {#ISNULL#} OR contenttypid=0")
                    m_Conn.gConn_Update("UPDATE DOC_DOC_HST " _
                                    & " SET contenttypeid=" & CInt(clsHrcConnClient.enumMimeTypes.coHTML) _
                                    & " WHERE  cod > 0 AND contenttypid {#ISNULL#} OR contenttypid=0")

                    m_Conn.gConn_Update("UPDATE DOC_APA " _
                                    & " SET orden=cod" _
                                    & " WHERE  cod > 0 ")

                    m_Conn.gConn_Update("UPDATE DOC_PRO " _
                                    & " SET orden=cod" _
                                    & " WHERE  cod > 0 ")

                End If
                If auxVersion < 454 Then
                    gSystem_SetParameterByID(coSysParamIDDOCViewDefault, "12")  'Metro
                End If
                If auxVersion < 457 Then
                    gSystem_CreateParameterByID(coSysParamIDDocAllUsersCanAddDocuments, "Documentos-Todos los usuarios pueden crear documentos")
                    gSystem_SetParameterByID(coSysParamIDDocAllUsersCanAddDocuments, "1")
                    Dim auxGrpCod As Integer
                    Dim auxEquCod As Integer
                    auxGrpCod = m_Security.gGroup_Create(pGrpDsc:="Documentador-Receptores", pgrpinherit:=-1, pGrpID:=coGroupIDDocumentadorReceptores)
                    auxEquCod = gEntity_DOC_EQU_SystemInsert(pdsc:="Documentador-Receptores", pundcod:=-1, pmiembrosgrpcod:=auxGrpCod)

                    'Dim auxEquCod As Integer = gEntity_DOC_EQU_Insert(pdsc:="Documentador-Receptores")
                    gSystem_PostAction(enumEntities.coEntityDOC_EQU, enumActionType.coConfirmInsert, auxEquCod)
                    Dim auxTroCod As Integer = gEntity_DOC_TRO_SystemInsert(pdsc:="Plantillas predeterminada", pcustom:=True)
                    Dim auxTroRolCod As Integer
                    auxTroRolCod = gEntity_DOC_TROROL_Insert(ptrocod:=auxTroCod, prolcod:=enumRoles.coEditor, prolcodtype:=enumEntities.coEntityDOC_EQU)
                    gEntity_DOC_TROROLEQU_Insert(ptrorolcod:=auxTroRolCod, pequcod:=auxEquCod)

                    gSystem_SetParameterByID(coSysParamIDTRODefault, auxTroCod)
                End If
                If auxVersion < 459 Then

                    Dim auxEquCod As Integer = m_Conn.gConn_QueryValueInt("SELECT cod FROM DOC_EQU WHERE dsc LIKE '%Documentador-Receptores%'", -1)
                    gSystem_SetParameterByID(coSysParamIDEQUReceptores, auxEquCod)
                End If
                If auxVersion < 461 Then
                    gEntity_DOC_ROL_Update(pcod:=CInt(enumRoles.coReceptor), pdsc:="Receptor")
                    Dim auxSisCod_Mandatory As Integer = -1
                    Dim auxRows() As DataRow = hrcEntityDT_DOC_SIS.Select("COD > 0 AND (baja IS NULL)")
                    If auxRows.Count = 1 Then
                        auxSisCod_Mandatory = auxRows(0)("cod")
                    End If
                    If auxSisCod_Mandatory <> -1 Then
                        m_Conn.gConn_Update("UPDATE DOC_DOC SET siscod= " & auxSisCod_Mandatory _
                                            & " WHERE siscod=-1 OR siscod = {#FALSE#} OR siscod {#ISNULL#} ")
                        m_Conn.gConn_Update("UPDATE DOC_DOCVIG SET siscod= " & auxSisCod_Mandatory _
                                            & " WHERE siscod=-1 OR siscod = {#FALSE#} OR siscod {#ISNULL#} ")
                    End If
                    gSystem_CreateParameterByID(coSysParamIDDocVersionadorEditoresDefault, "Documentos-Editores son receptores cuando no exista el rol")
                    gSystem_CreateParameterByID(coSysParamIDDocVersionadorAprobadoresDefault, "Documentos-Aprobadores son receptores cuando no exista el rol")
                End If
                If auxVersion < 463 Then
                    Dim auxEquCod As Integer = Val(gSystem_GetParameterByID(coSysParamIDEQUReceptores))
                    Dim auxTroRolCod As Integer
                    Dim auxtroCod As Integer
                    For Each auxRow As DataRow In m_Conn.gConn_Query("SELECT cod,trocodcustom FROM DOC_DOC WHERE (baja = {#FALSE#} OR baja {#ISNULL#})").Rows
                        auxtroCod = m_Conn.gField_GetInt(auxRow("trocodcustom"), -1)
                        If auxtroCod > 0 Then
                            auxTroRolCod = gEntity_DOC_TROROL_Insert(ptrocod:=auxtroCod, _
                                                            prolcod:=enumRoles.coReceptor, _
                                                            prolcodtype:=enumEntities.coEntityDOC_EQU)
                            gEntity_DOC_TROROLEQU_Insert(ptrorolcod:=auxTroRolCod, _
                                                            pequcod:=auxEquCod)
                        End If
                    Next
                End If
                If auxVersion < 465 Then
                    'DOC_TRO.baja varchar -> bit
                    m_Conn.gConn_Update("ALTER TABLE DOC_TRO ALTER COLUMN BAJA bit")
                End If
                If auxVersion < 468 Then
                    gSystem_CreateParameterByID(coSysParamIDDOCNroAssignAtCreation, "Documentos-Asignar número al crear")
                    gSystem_SetParameterByID(coSysParamIDDOCNroAssignAtCreation, "1")
                End If
                If auxVersion < 469 Then
                    m_Conn.gConn_Update("UPDATE DOC_DOC SET trocod=trocodcustom WHERE cod > 0 ")
                    m_Conn.gConn_Update("UPDATE DOC_DOC SET contenttypeid=" & clsHrcConnClient.enumMimeTypes.coHTML & " where contenttypeid=1 ")
                    m_Conn.gConn_Update("UPDATE DOC_DOC SET contenttypeid=" & clsHrcConnClient.enumMimeTypes.coHTML & "  where contenttypeid=1")
                    'm_Conn.gConn_Update("UPDATE DOC_DOC SET wfwmode=" & enumWfwMode.coUserCreate & " WHERE cod >0")
                End If
                If auxVersion < 470 Then
                    Dim auxMiembrosGrpcod As Integer
                    auxMiembrosGrpcod = m_Conn.gConn_QueryValueInt("SELECT miembrosgrpcod FROM DOC_EQU" _
                                                            & " WHERE dsc ='Documentador-Receptores'", -1)
                    m_Conn.gConn_Update("UPDATE Q_SECPGRP SET grpid=-1" _
                                       & " WHERE grpid=" & coGroupIDDocumentadorReceptores)
                    m_Conn.gConn_Update("UPDATE Q_SECPGRP SET grpid=" & coGroupIDDocumentadorReceptores _
                                        & " WHERE grpcod=" & auxMiembrosGrpcod)
                End If
                If auxVersion < 471 Then
                    gSystem_CreateParameterByID(coSysParamIDDOCEspecificoA_UndResp, "Documentos-Específico incluye a responsables de unidad")
                    gSystem_SetParameterByID(coSysParamIDDOCEspecificoA_UndResp, "1")
                End If
                gSystem_SetParameterByID(1, coVersion)
                auxreturn = MyBase.gSys_Update()
                If Val(coSystemType) <> 175 Then
                    gSys_DebugLogAdd("Actualización del sistema v" & coVersion)
                    gAlert_SendToAdmin("Sistema-Actualizacion  v" & coVersion, "Sistema-Actualización.A " & coVersion & "</br>Sistema:" & coSystemName & "." & coSystemTitle, clsHrcAlertClient.enumLevel.coInfo)
                End If
            End If

        Catch ex As Exception
            gSys_DebugLogAdd("Exception in update:" & ex.Message)
            Return ex.Message
        End Try
        Return auxreturn
    End Function
    Private Sub gAlert_SendToAdmin(ByVal pSubject As String, _
                                   ByVal pContent As String, _
                                   ByVal pLevel As clsHrcAlertClient.enumLevel)
        Dim auxQueueList As New List(Of Integer)
        For Each auxRow As DataRow In m_Security.gGroup_ResolveLogins(m_Security.gGroup_GetCodByID(coGroupIDAdministradores)).Rows
            Dim auxQueCod As Integer = m_Conn.gConn_QueryValue("SELECT alequecod FROM EMP WHERE seccod=" & auxRow("seccod"), -1)
            If auxQueCod > 0 Then
                auxQueueList.Add(auxQueCod)
            End If
        Next
        m_Alerts.gRoutes_InsertAlert(pSubject, pContent, clsHrcAlertClient.enumLevel.coInfo, clsHrcAlertClient.enumConfirmMode.coNoConfirm, -1, auxQueueList)
    End Sub

    Private Sub gAlert_SendToDev(ByVal pSubject As String, _
                            ByVal pContent As String, _
                            ByVal pLevel As clsHrcAlertClient.enumLevel)
        Dim auxMail As New imMailing(coSMTPServer, coSMTPUser, coSMTPpsw, coSMTPfrom, Val(coSMTPport), coSystemTitle)
        auxMail.SMTPenableSSL = m_Conn.gField_GetBoolean(coSMTPSSLEnabled, False)

        Dim auxSiteURL As String = m_Conn.gField_GetString(coExternalURL)
        If auxSiteURL = "" Then
            auxSiteURL = VirtualPathUtility.GetDirectory(HttpRuntime.AppDomainAppVirtualPath)
        End If
        If Right(auxSiteURL, 1) <> "/" Then
            auxSiteURL &= "/"
        End If
        auxMail.gTo_Add("jbecker@intelimedia.com.ar")
        If auxMail.gMail_Send(pSubject, pContent) Then

        End If
    End Sub
    Public Sub gSystem_Initialize()
        'Inicializa el sistema
        If gSystem_Init() = "" Then
            Dim auxDemoSecCod As Integer = m_Security.gLogin_Create("demo", "")
            If RunInDemo Then
                'm_Security.gGroup_AddLogin(ConfigurationManager.AppSettings("willinerpry.grpprjgc"), m_Security.gLogin_GetCod("owagner"))
            End If
        End If
    End Sub
    Public Sub gLoadStatic_UND_LoadUndHierarchy()
        If hrcDT_Cache.IndexOfKey(enumDTCache.coUNDHierarchy) = -1 Then
            hrcDT_Cache.Add(enumDTCache.coUNDHierarchy, Nothing)
        End If
        Dim auxQuery As String = "SELECT cod as q_cod,dsc as q_dsc,undcodsup,NULL," & enumEntities.coEntityUND & ",cod " _
               & " FROM UND WHERE cod > 0 AND (UND.baja = 0 OR UND.baja IS NULL)  ORDER BY dsc "
        Dim auxDT As DataTable = m_Conn.gHierarchy_GenerateTable(auxQuery)

        Dim auxGrid As New clshrcGrdData(m_Conn)
        auxGrid.gTreeGrid_Enabled("q_grdleft_field", "q_grdparent", "q_grdisleaf", "q_grdexpanded_field")
        auxDT = auxGrid.gDataTable_Prepare(auxDT)
        hrcDT_Cache(enumDTCache.coUNDHierarchy) = auxDT


    End Sub
    Public Sub gLoadStatic_DOCAPA_Load()
        If hrcDT_Cache.IndexOfKey(enumDTCache.coDOCPROUserSelection_APA) = -1 Then
            hrcDT_Cache.Add(enumDTCache.coDOCPROUserSelection_APA, Nothing)
        End If
        Dim auxQuery As String = "SELECT cod as q_cod,dsc as q_dsc,NULL,NULL," & enumEntities.coEntityDOC_APA & ",cod " _
                        & ",CAST( CASE WHEN (SELECT COUNT(*) FROM DOC_PRO WHERE (DOC_PRO.baja = {#FALSE#} OR DOC_PRO.baja {#ISNULL#})  " _
                        & " AND DOC_PRO.cod > 0 AND DOC_PRO.apacod = DOC_APA.cod) > 0  THEN 1 ELSE 0 END as bit)" _
                        & " FROM DOC_APA WHERE cod > 0 AND (DOC_APA.baja = {#FALSE#} OR DOC_APA.baja {#ISNULL#})  ORDER BY dsc "
        Dim auxDT As DataTable = m_Conn.gHierarchy_GenerateTable(auxQuery, "", "", Nothing, 6, True)
        Dim auxGrid As New clshrcGrdData(m_Conn)
        auxGrid.gTreeGrid_Enabled("q_grdleft_field", "q_grdparent", "q_grdisleaf", "q_grdexpanded_field")
        auxDT = auxGrid.gDataTable_Prepare(auxDT)
        hrcDT_Cache(enumDTCache.coDOCPROUserSelection_APA) = auxDT
    End Sub
    Public Sub gLoadStatic_UND_LoadClaHierarchy()
        If hrcDT_Cache.IndexOfKey(enumDTCache.coCLAHierarchy) = -1 Then
            hrcDT_Cache.Add(enumDTCache.coCLAHierarchy, Nothing)
        End If
        Dim auxQuery As String = "SELECT cod as q_cod,dsc as q_dsc,undcodsup,NULL," & enumEntities.coEntityUND & ",cod " _
               & " FROM UND WHERE cod > 0 AND (UND.baja = 0 OR UND.baja IS NULL)  ORDER BY dsc "
        Dim auxDT As DataTable = m_Conn.gHierarchy_GenerateTable(auxQuery)

        Dim auxGrid As New clshrcGrdData(m_Conn)
        auxGrid.gTreeGrid_Enabled("q_grdleft_field", "q_grdparent", "q_grdisleaf", "q_grdexpanded_field")
        auxDT = auxGrid.gDataTable_Prepare(auxDT)
        hrcDT_Cache(enumDTCache.coUNDHierarchy) = auxDT


    End Sub
    Public Overrides Function gSystem_Init() As String
        Dim auxReturn As String = ""
        Try
            Dim auxConnString As String = gSystem_GetParameterByID(26)
            Dim auxConn As New clsHrcConnClient
            Try
                auxConn.DateTimeAdjust = hrcSystemDateTimeAdjust
                auxConn.ActivateLicense(coDBKey)
                auxConn.ConnShared = False
                auxConn.gConn_Open(auxConnString, hrcBasType.coBasTypeSQLServerWithAttach, clsHrcConnClient.hrcSavingMode.coinDBTable)
                auxConn.gConn_Close()
                If ConfigurationManager.AppSettings("binariesfolder") IsNot Nothing Then
                    auxConn.BinariesFolder = auxConn.gField_GetString(ConfigurationManager.AppSettings("binariesfolder"))
                End If
                m_Conn = auxConn
                auxConn.DefaultBinaryCod = -1
                DebugLogOn = True

                If coDemoMode = "1" Then
                    RunInDemo = True
                End If
                DebugLogOn = False

                If Val(coLogLevel) > 0 Then
                    DebugLogOn = True
                End If


                If auxConn.LastError <> 0 Then
                    auxReturn &= auxConn.LastErrorDescription & "."
                End If
                If Val(coLogLevel) >= 10 Then
                    auxConn.gDebug_On(coLogFileName)
                End If
            Catch ex As Exception
                auxReturn &= ex.Message & "."
            End Try

            'auxConn.TransactionEnabled = True
            Dim auxSecurity As New clsHrcSecurityClient
            Try
                auxSecurity.ActivateLicense(coDBKey)
                auxSecurity.gInit(auxConn, True)
                If auxSecurity.LastError <> 0 Then
                    auxReturn &= auxSecurity.LastErrorDescription & "."
                End If
            Catch ex As Exception
                auxReturn &= ex.Message & "."
            End Try
            Try
                m_Conn = auxConn
                m_Security = auxSecurity
                m_Alerts = New clsHrcAlertClient(m_Conn)
                m_Alerts.DefaultMsgDsc = coSystemTitle & "|Mensaje"
                AddHandler m_Alerts.Alerts_Raise, AddressOf gAlerts_Raise
                'AddHandler m_Alerts.SessionCreated_Raise, AddressOf gSessionCreated_Event
            Catch ex As Exception
                auxReturn &= ex.Message & "."
            End Try

            'Carga las variables de session
            'Las carga al final, porque si se llama desde un WS no hay contexto
            Dim auxContext As HttpContext = HttpContext.Current
            If auxContext IsNot Nothing Then
                If auxContext.Session IsNot Nothing Then
                    auxContext.Session("conn") = m_Conn
                    auxContext.Session("security") = m_Security
                    auxContext.Session("runindemo") = RunInDemo
                    auxContext.Session("debuglogon") = DebugLogOn
                    auxContext.Session("hrcAlerts") = m_Alerts
                End If
            End If

            gTRACE_add(-1, 10, "Carga de tablas estaticas-start")
            m_Conn.gConn_Open()
            auxReturn = MyBase.gSystem_Init
            If m_Alerts IsNot Nothing And hrcAlerts IsNot Nothing Then
                m_Alerts.gServerMode_StartAsClient(hrcAlerts)
            End If
            gTRACE_add(-1, 10, "Carga de tablas estaticas.End")

            auxConn.gConn_Close()

        Catch ex As Exception
            gTRACE_add(-1, 1, "Exception en gSystem_Init:" & ex.Message & "." & ex.StackTrace)
            auxReturn &= ex.Message & "."
        End Try
        Return auxReturn
    End Function
    Private Sub gUserGenericHandler_Handler(ByVal pControl As clsHrcJSControlBasic, ByVal pValues As clshrcBagValues)
        Dim auxAction As String = pValues.gValue_Get("ACTION", "")
        Dim auxCommandName As String = pValues.gValue_Get("COMMANDNAME", "").ToString.ToUpper
        If auxCommandName = "USERJOBQUEUE_OPEN" Then
            Dim auxHrcContext As clsHrcJSContext = pControl.BagValues.gValue_Get("hrccontext")
            Dim auxJobform As New clsHrcJSJobForm("userjobform", hrcProcessQueue, auxHrcContext)
            auxJobform.Title = "Mis trabajos en curso"
            auxJobform.JobSearchFilter = "empcod='" & auxHrcContext.BagValues.gValue_Get("empcod") & "'"
            auxJobform.CacheStoreType = clsHrcJSControlBasic.enumCacheStoreType.coTemporarySession
            auxJobform.gCache_Save()
            pValues.gValue_Add("HRC_RESULTS", m_Conn.gField_GetJSONString("TMPID", auxJobform.CacheID))
        End If
    End Sub
    Private Sub gSessionCreated_Event(ByVal pAlert As clsHrcAlertClient, _
                                     ByVal pSession As clsHrcAlertClient.clsAlertSession, _
                                     ByVal pBagValues As clshrcBagValues)
        Dim auxDialogType As Integer = Val(pBagValues.gValue_Get("dialogtype"))
        Select Case auxDialogType
            Case 1
                pAlert.gSession_To_Add(pSession.AleSesID, "Requirente", -1, 1002, -1)
                pAlert.gSession_To_Add(pSession.AleSesID, "Resolutor", -1, 1003, -1)
            Case 2
                Dim auxList As New List(Of Integer)
                For Each auxRow As DataRow In m_Conn.gConn_Query("SELECT TOP 3 ALEMSGCOD FROM Q_ALEMSG " _
                                                                 & " order by alemsgtimegenerated desc ").Rows
                    auxList.Add(auxRow(0))
                Next
                pAlert.gSession_SetMsgFilter(pSession.AleSesID, auxList)
        End Select

    End Sub
    Public Overrides Function gCluster_LoadStaticTables() As Boolean
        Dim auxChange As Boolean = MyBase.gCluster_LoadStaticTables
        If auxChange Then
            hrcFormInitial = "cfrmdocumentos"
            gLoadStatic_DOCAPA_Load()
            gLoadStatic_UND_LoadUndHierarchy()
        End If
    End Function
    Public Overrides Function gSystem_Started() As String
        Dim auxList As New List(Of String)
        auxList.Add("empcod")
        'hrcProcessQueue.gDebug_On("c:\windows\temp\pro_jobqueue.txt")
        hrcProcessQueue.gJobSearch_Enable(auxList)
        gReplication_Configure()
        Dim auxReturn As String = MyBase.gSystem_Started
        gAlert_SendToAdmin("Aplicación iniciada!", m_Conn.gDate_GetNow.ToString, clsHrcAlertClient.enumLevel.coInfo)
        Return auxReturn
    End Function
    Private Sub gReplication_Configure()
        hrcReplicationServer = New clsHrcReplicationClient
        Dim auxClassGeneral As New clshrcGeneral
        'auxClassGeneral.gSystem_Init()
        Dim auxSecPsw As String = ""
        auxSecPsw = LicParam.gValue_Get("SYS_GENPSW", "").trim
        If auxSecPsw = "" Then
            auxSecPsw = m_genPsw
        End If

        hrcReplicationServer.gReplication_SetConfig(auxClassGeneral, m_genPsw)



        Dim auxTableID As Integer
        auxTableID = hrcReplicationServer.gTable_GetID(clsHrcReplicationClient.enumEntityType.coEMP)
        hrcReplicationServer.gTable_AddFieldString(auxTableID, "mail", "mail", 250, clsHrcReplicationClient.enumEntityFieldType.coUndefined)
        hrcReplicationServer.gTable_AddFieldString(auxTableID, "legajo", "legajo", 12, clsHrcReplicationClient.enumEntityFieldType.coUndefined)

        auxTableID = hrcReplicationServer.gTable_GetID(clsHrcReplicationClient.enumEntityType.coUND)
        hrcReplicationServer.gTable_AddFieldNumber(auxTableID, "undtipcod", "undtipcod", clsHrcReplicationClient.enumEntityFieldType.coUndefined)
        hrcReplicationServer.gTable_AddFieldString(auxTableID, "undnro", "undnro", 12, clsHrcReplicationClient.enumEntityFieldType.coUndefined)
        hrcReplicationServer.gTable_AddFieldNumber(auxTableID, "editor", "", clsHrcReplicationClient.enumEntityFieldType.coUndefined)


        auxTableID = hrcReplicationServer.gTable_GetID(clsHrcReplicationClient.enumEntityType.coEQUMBR)
        hrcReplicationServer.gTable_AddFieldBoolean(auxTableID, "grupoeditores", "", clsHrcReplicationClient.enumEntityFieldType.coUndefined)

        auxTableID = hrcReplicationServer.gTable_GetID(clsHrcReplicationClient.enumEntityType.coEQUMBRUND)
        hrcReplicationServer.gTable_AddFieldBoolean(auxTableID, "grupoeditores", "", clsHrcReplicationClient.enumEntityFieldType.coUndefined)
        hrcReplicationServer.gReplication_Initialize()
        auxClassGeneral.gSystem_End()
    End Sub

    Public Overloads Function gSystem_Logon(ByVal pSessionID As String) As String
        m_Security.gLogin_SessionLogin(pSessionID)
    End Function
    Public Function gSystem_LogonDelegatedSession(ByVal pDelID As String) As Boolean
        Dim auxReturn As Boolean = False
        Dim auxContext As HttpContext = HttpContext.Current
        If auxContext IsNot Nothing Then
            m_Security = auxContext.Session("security")
        End If
        'Dim auxSesID As String = m_Security.gLogin_CreateDelegatedSessionToCurrentLogin(pDelID, pSecCod)
        Dim auxSesID As String = m_Security.gLogin_CreateDelegatedSessionToCurrentLogin(pDelID)
        If auxSesID <> "" Then
            auxReturn = m_Security.gLogin_SessionLogin(auxSesID)

            If auxContext IsNot Nothing Then
                If auxContext.Session IsNot Nothing Then
                    auxContext.Session("sesid") = auxSesID
                    gSystem_SetContext() '(pAdminEnabled:=True)
                    gAlerts_Start(pEmpCod:=Val(auxContext.Session("empcod")))
                    gUser_ResolveMenu()
                End If
            End If
        End If
        Return auxReturn
    End Function
    Public Function gSystem_LogoffDelegatedSession() As Boolean
        Dim auxContext As HttpContext = HttpContext.Current
        If auxContext IsNot Nothing Then
            If auxContext.Session IsNot Nothing Then
                m_Security = auxContext.Session("security")
                m_Security.gLogin_DeleteDelegatedSession(auxContext.Session("sesid"))
                auxContext.Session("sesid") = ""
                gSystem_SetContext() '(pAdminEnabled:=True)
                gAlerts_Start(pEmpCod:=Val(auxContext.Session("empcod")))
                auxContext.Session("empcod") = m_Conn.gConn_QueryValue("SELECT cod FROM EMP WHERE seccod=" & m_Security.CurrentSecCod, -1)
                gUser_ResolveMenu()
            End If
        End If
        Return True
    End Function
    Private m_Security_ConvertToSafeUserName As Boolean = True
    Public Overrides Function gSystem_CheckAccess(ByVal pSecDsc As String, _
                                        ByVal pSecPsw As String) As Boolean

        ''/////////////////////////////////////////////
        Dim auxResult As Boolean = False

        Try
            Dim auxPC As String = ""
            Try
                'auxPC = System.Net.Dns.GetHostEntry(HttpContext.Current.Request.UserHostName).HostName()
                auxPC = System.Net.Dns.GetHostByAddress(HttpContext.Current.Request.ServerVariables("REMOTE_HOST")).HostName
            Catch ex As Exception
                auxPC = HttpContext.Current.Request.ServerVariables("REMOTE_HOST")
            End Try

            Dim auxContext As HttpContext = HttpContext.Current
            If auxContext IsNot Nothing Then
                Try
                    Dim auxClient As New imClientConnection
                    auxPC = auxClient.gContext_GetRemoteHost
                Catch ex As Exception
                End Try
                auxContext.Session("secsid") = -1
                auxContext.Session("seccod") = -1
                auxContext.Session("Isadmin") = False
                m_Security = auxContext.Session("security")
            End If
            '1.Si ingresa loguin, trata con eso
            '2.Sino ingresa login, y esta en RUINDEMO, ingresa como DEMO
            '3.Si no esta en RUNINDEMO, ingresa como el usuario.
            '4.Si no tiene usuario, ingresa como DEMO (SI ESTA HABILITADO).
            Dim auxSecDsc As String = pSecDsc.Trim.ToUpper
            Dim auxSecPsw As String = pSecPsw
            Dim auxContextUserName As String = ""
            Try
                auxContextUserName = auxContext.User.Identity.Name.ToString
            Catch ex As Exception

            End Try
            auxContextUserName = Replace(auxContextUserName, "0#.w|", "")
            gSys_DebugLogAdd("gSystem_CheckAccess-ContextUserName:" & auxContextUserName)

            Dim auxCurrentSecCod As Integer = -1
            Dim auxCurrentSidCod As Integer = -1
            Dim auxCurrentSecDsc As String = ""

            If hrcSystemStatus = 1 Or hrcSystemStatus = 10 Then
                auxCurrentSecCod = 1
                auxCurrentSecDsc = "Install"
                auxCurrentSidCod = 1
                hrcFormInitial = "hrcAppConfig"
            Else
                If auxSecDsc = "" Then
                    auxSecPsw = LicParam.gValue_Get("SYS_GENPSW", "")
                    If auxSecPsw = "" Then
                        auxSecPsw = m_genPsw
                    End If
                    If RunInDemo Then
                        auxSecDsc = "DEMO"
                        Dim auxDemoLoginCod As Integer = m_Security.gLogin_Create("demo", m_genPsw)
                        m_Security.gLogin_Enabled("demo")
                    Else
                        Dim auxUsr() As String = auxContextUserName.Split("\")
                        If UBound(auxUsr) = 0 Then
                            If auxUsr(0) <> "" Then
                                auxSecDsc = "WILLINER\" & auxUsr(0)
                            End If
                        Else
                            auxSecDsc = auxContextUserName
                            auxSecDsc = Replace(auxSecDsc, "WILLINER.COM.AR", "WILLINER")
                        End If

                    End If
                End If
            End If

            auxSecDsc = auxSecDsc.ToUpper.Trim
            gTRACE_add(-1, 1, "Intento de login de usuario [" & auxSecDsc & "] desde el equipo [" & auxPC & "]")
            Dim auxLoginResult As Boolean = m_Security.gLogin(auxSecDsc, auxSecPsw)
            If auxLoginResult = False And m_Security_ConvertToSafeUserName Then
                auxSecDsc = auxSecDsc.Replace("Ñ", "N")
                auxSecDsc = auxSecDsc.Replace("Á", "A")
                auxSecDsc = auxSecDsc.Replace("É", "E")
                auxLoginResult = m_Security.gLogin(auxSecDsc, auxSecPsw)
            End If
            If auxLoginResult Then
                auxCurrentSecCod = m_Security.CurrentSecCod
                auxCurrentSidCod = m_Security.CurrentSidCod
                auxCurrentSecDsc = m_Security.CurrentSecDsc.ToUpper
            End If

            If auxCurrentSecCod = -1 And RunInDemo And auxSecDsc = "DEMO" Then
                auxCurrentSecCod = 1
                auxCurrentSecDsc = "Demo"
                auxCurrentSidCod = 1
            End If
            If auxCurrentSecCod = -1 Then
                Dim auxUserName As String = HttpContext.Current.User.Identity.Name.ToString
                Dim auxSystemUserName As String = ""
                If ConfigurationManager.AppSettings("adusuario") IsNot Nothing Then
                    auxSystemUserName = ConfigurationManager.AppSettings("adusuario").ToString
                End If
                If auxSystemUserName <> "" And InStr(auxUserName, auxSystemUserName) <> 0 Then
                    auxCurrentSecCod = 1
                    auxCurrentSidCod = 1
                    auxCurrentSecDsc = "System"
                End If
            End If
            If auxCurrentSecCod < 1 Then
                gTRACE_add(-1, 1, "El usuario no ha podido ingresar [" & auxSecDsc & "]" & m_Security.LastErrorDescription & "." & m_Conn.LastErrorDescription & "]")
                m_Conn.gConn_Close()
            Else
                gTRACE_add(-1, 1, "Usuario ha ingresado correctamente [" & auxSecDsc & "] desde el equipo [" & auxPC & "]")
                auxResult = True
                If pSecDsc = "" Then
                    'SEguridad integrada
                    m_Security.SecNoChangePsw = True
                Else
                    Dim auxUsr() As String = auxSecDsc.Split("\")
                    If auxUsr.Count = 2 Then
                        If InStr(";" & coSecDomainList.ToUpper & ";", ";" & auxUsr(0).ToUpper & ";") <> 0 Then
                            gTRACE_add(-1, 1, "Dominio:" & auxUsr(0))
                            m_Security.SecNoChangePsw = True
                        Else
                            m_Security.SecNoChangePsw = False
                        End If
                    End If
                End If
                gSystem_SetContext() '(pAdminEnabled:=True)
            End If
            gTRACE_add(-1, 1, "Login finalizado!")
        Catch ex As Exception
            gTRACE_add(-1, 1, "Excepcion en proceso de login:" & ex.Message)
        End Try
        Return auxResult
    End Function
    Private m_UserGenericHandler As clsHrcJSGenericHandler
    Public Overrides Sub gSystem_SetContext() '(ByVal pAdminEnabled As Boolean)
        Dim auxSidCod As Integer = -1
        Dim auxSecCod As Integer = -1
        Dim auxSecDsc As String = ""
        Dim auxIsAdmin As Boolean = False
        Dim auxIsHlpAdmin As Boolean = False
        Dim auxreqhorEnabled As Boolean = False
        Dim auxContext As HttpContext = HttpContext.Current
        If hrcSystemStatus = 1 Or hrcSystemStatus = 10 Then
            auxIsAdmin = True
            auxSidCod = 1
            auxSecCod = 1
            auxSecDsc = "install"
        Else
            auxSidCod = m_Security.CurrentSidCod
            auxSecCod = m_Security.CurrentSecCod
            auxSecDsc = m_Security.CurrentSecDsc.ToUpper
            If RunInDemo And auxSecDsc = "DEMO" Then
                auxIsAdmin = True ' pAdminEnabled
                auxIsHlpAdmin = True
                Dim auxAdminGrpCod As Integer = m_Security.gGroup_GetCodByID(coGroupIDAdmins)
                m_Security.gGroup_DelLogin(auxAdminGrpCod, auxSecCod)
                m_Security.gGroup_AddLogin(auxAdminGrpCod, auxSecCod)
            Else
                'If pAdminEnabled Then
                auxIsAdmin = m_Security.gMember_IsInGroupByID(auxSidCod, coGroupIDAdmins) _
                    Or m_Security.gSID_CheckAccess(enumAccessType.coSYSGlobalCambiarpermisos)
                'End If
                'If auxIsAdmin = False Then
                '    auxIsAdmin = m_Security.gMember_IsInGroupByID(pSidCod, coGroupDocumentadorAdministradores)
                'End If
                auxIsHlpAdmin = m_Security.gSID_CheckAccess(enumAccessType.coQHLPAyudasModificar)
            End If
        End If


        If auxContext IsNot Nothing Then
            Dim auxHTML As New clsHrcCodeHTML
            Dim auxIsMobile As Boolean = auxHTML.gUserAgent_IsMobile(auxContext.Request.UserAgent)
            m_Alerts.gWriters_Stop()
            m_Alerts.gReaders_Stop()
            auxContext.Session("secsid") = auxSidCod
            auxContext.Session("seccod") = auxSecCod
            auxContext.Session("secdsc") = auxSecDsc
            auxContext.Session("user_menu") = ""
            auxContext.Session("user_menu_script") = ""
            Dim auxEmpCod As Integer = -1
            Dim auxEmpDsc As String = ""
            Dim auxEmp_UndCod As Integer = -1
            Dim auxRows() As DataRow
            auxRows = hrcEntityDT_EMP.Select("seccod=" & auxSecCod _
                                             & " AND (baja=0 OR baja IS NULL)")
            If auxRows.Count <> 0 Then
                auxEmpCod = auxRows(0)("cod")
                auxEmpDsc = auxRows(0)("dsc")
                auxEmp_UndCod = m_Conn.gField_GetInt(auxRows(0)("undcod"), -1)
            End If
            auxContext.Session("empcod") = auxEmpCod
            If auxEmpCod > 0 Then
                gAlerts_Start(pEmpCod:=auxEmpCod)
            End If
            'auxContext.Session("hrcalerts") = m_Alerts

            If m_Security.CurrentSecCod > 0 Then
                If Val(gLoginPreference_GetValue(enumPrefType.coprfmyconfig, "isadmin_enabled")) = 0 Then
                    auxIsAdmin = False
                End If
            End If
            Dim auxOffset As Integer = -3
            auxContext.Session("isadmin") = auxIsAdmin
            auxContext.Session("offset") = auxOffset

            Dim auxHrcContext As New clsHrcJSContext("hrcContext", m_Conn, m_Security, m_Alerts)
            auxHrcContext.BagValues.gValue_Add("empcod", auxEmpCod)
            auxHrcContext.BagValues.gValue_Add("empdsc", auxEmpDsc)
            auxHrcContext.OffsetSecond = auxOffset * 60 * 60
            auxContext.Session("hrcContext") = auxHrcContext
            'auxHrcContext.gJS_ControlReady()
            Dim auxUser_Header_Content As String = ""
            Dim auxJSSCripts As String = auxHrcContext.gJS_GeneralCodeGet
            auxContext.Session("JS_GENERAL") = auxJSSCripts

            m_UserGenericHandler = New clsHrcJSGenericHandler("reqUserGenericHandler")
            m_UserGenericHandler.BagValues.gValue_Add("hrccontext", auxHrcContext)
            'm_UserGenericHandler.BagValues.gValue_Add("user_jobqueue_ID", m_Conn.gField_GetUniqueID)
            AddHandler m_UserGenericHandler.EventCommandHandler, AddressOf gUserGenericHandler_Handler
            auxHrcContext.gObjectTmp_UploadControl(m_UserGenericHandler)

            m_Alerts.gReaders_StartPublicMonitors()
            m_Alerts.OnlineModeEnabled = True

            If auxIsAdmin Then
                m_Alerts.gWriters_Start(m_Conn.gField_GetInt(gSystem_GetParameterByID(coSysParamIDPublicQueue), -1), "Público", True)
                gTRACE_add(-1, 1, "El usuario es administrador")
            End If

            'Enq ue unidad permite insertar
            '0-ninguna
            '-1 todas
            '> 0unidad especifica

            Dim auxDOC_DOC_Und_PermEditor As String = ""
            Dim auxDOC_DOC_New_PermEditorRoles As Boolean = False   'Puede editar roles
            Dim auxUndCodListEditor As New List(Of Integer)  'Lista de unidad de la que es editor
            If auxIsAdmin Then
                auxDOC_DOC_New_PermEditorRoles = True
                auxDOC_DOC_Und_PermEditor = "-1"
            Else
                If m_Security.gMember_IsInGroupByID(coGroupDocumentadorEditores) _
                        Or m_Security.gMember_IsInGroupByID(coGroupDocumentadorAdministradores) _
                        Or m_Security.gMember_IsInGroupByID(coGroupIDDocumentadorCreadores) _
                        Or m_Security.gSID_CheckAccess(enumAccessType.coSYSCreardocumento) Then
                    auxDOC_DOC_New_PermEditorRoles = True
                    auxDOC_DOC_Und_PermEditor = "-1"
                    gSys_DebugLogAdd("Editor is master")
                End If
                If auxDOC_DOC_Und_PermEditor = "" And auxEmpCod > 0 Then
                    auxRows = hrcEntityDT_UND.Select("(baja IS NULL OR baja = 0) " _
                                                     & " AND ((editor=" & auxEmpCod & ")" _
                                                     & " OR (editor < 1 AND resp=" & auxEmpCod & ")" _
                                                     & " OR (editor IS NULL AND resp=" & auxEmpCod & ")" _
                                                     & " )")
                    If auxRows.Count <> 0 Then
                        'Es editor
                        For Each auxRow As DataRow In auxRows
                            If auxRow("cod") > 0 Then
                                auxUndCodListEditor.AddRange(gEntity_UND_GetChilds(auxRow("cod"), True, True))
                                auxDOC_DOC_New_PermEditorRoles = True
                            End If
                        Next
                    Else
                        'no es editor
                        If auxEmp_UndCod > 0 Then
                            Dim auxCanCreateDocs As Boolean = False
                            Dim auxRow As DataRow = hrcEntityDT_UND_FindByKey(auxEmp_UndCod)
                            If auxRow IsNot Nothing Then
                                'Permisos de edición de roles al crear
                                If IsDBNull(auxRow("creatorsmbrdir")) Then
                                    auxCanCreateDocs = m_Conn.gField_GetBoolean(gSystem_GetParameterByID(coSysParamIDUNDCreatorsMBRDIRDefault), False)
                                Else
                                    auxCanCreateDocs = m_Conn.gField_GetBoolean(auxRow("creatorsmbrdir"), False)
                                End If
                                If auxCanCreateDocs Then
                                    'Si puede crear documentos en su unidad, lo agrega como editor de esa unidad
                                    'NO se le permite cambiar roles
                                    auxUndCodListEditor.Add(auxEmp_UndCod)
                                End If
                            End If
                        End If
                    End If
                End If
            End If
            If auxDOC_DOC_Und_PermEditor = "" Then
                auxDOC_DOC_Und_PermEditor = m_Conn.gFieldDB_GetString(auxUndCodListEditor)
            End If
            gSys_DebugLogAdd("Permeditor:" & auxDOC_DOC_Und_PermEditor)
            auxContext.Session("DOC_DOC_Und_PermEditor") = auxDOC_DOC_Und_PermEditor    'Lista de unidades que es editor.-1=admin,blanco=nada
            auxContext.Session("DOC_DOC_New_PermEditorRoles") = auxDOC_DOC_New_PermEditorRoles

            Dim auxHeadersBag As New clshrcBagValues
            auxHeadersBag = auxHrcContext.gUser_GetHTMLHeader(auxIsMobile, clsHrcCodeHTML.enumWindowMode.coNormal)
            'HEADER
            auxUser_Header_Content = auxHeadersBag.gValue_Get("USER_HEADER_CONTENT", "").ToString
            auxContext.Session("user_header_content") = auxUser_Header_Content
            auxContext.Session("user_headerwrapper_content") = auxHeadersBag.gValue_Get("user_headerwrapper_content", "").ToString
            auxContext.Session("User_Header_ScriptsInUpdatePanel") = auxHeadersBag.gValue_Get("User_Header_ScriptsInUpdatePanel")
            'MASTER-CONTENIDO
            auxContext.Session("user_masterheader_content") = auxHeadersBag.gValue_Get("user_masterheader_content", "").ToString
            auxContext.Session("user_master_content_end") = auxHeadersBag.gValue_Get("user_master_content_end", "").ToString
            If gModoObligado_Check() = False Then
                'auxContext.Session("user_menu") = () ' gUser_GetMenu(auxIsAdmin, auxIsHlpAdmin)
                gUser_ResolveMenu()
            End If
        End If
        'If RunInDemo And auxSecDsc = "DEMO" Then
        '    If auxContext IsNot Nothing Then
        '        auxIsAdmin = True
        '    End If
        '    Dim auxAdminGrpCod As Integer = m_Security.gGroup_GetCodByID(coGroupDocumentadorAdministradores)
        '    m_Security.gGroup_DelLogin(auxAdminGrpCod, auxCurrentSecCod)
        '    m_Security.gGroup_AddLogin(auxAdminGrpCod, auxCurrentSecCod)
        'Else
        '    If auxContext IsNot Nothing Then
        '        auxIsAdmin = m_Security.gMember_IsInGroupByID(auxCurrentSidCod, coGroupDocumentadorAdministradores)
        '        If auxIsAdmin = False Then
        '            auxIsAdmin = m_Security.gMember_IsInGroupByID(auxCurrentSidCod, coGroupIDAdministradores)
        '        End If
        '    End If
        'End If

    End Sub
    Public Overrides Sub gAlerts_Started()
        gSys_DebugLogAdd("Alerts-Started")
        If Val(coLogLevel) > 9 Then
            hrcAlerts.gDebug_On("c:\windows\temp\qdoc_" & coSystemUniqueID & ".txt")
        End If
        AddHandler hrcAlerts.Alerts_Raise, AddressOf gAlerts_Raise
        gSys_DebugLogAdd("Alerts-Started-End")
    End Sub
    Private Sub gUser_ResolveDocs()
        'Dim aux
    End Sub
    Public Overrides Function gUser_ResolveMenu() As String
        Dim auxContext As HttpContext = HttpContext.Current
        If auxContext IsNot Nothing Then
            If auxContext.Session IsNot Nothing Then
                Dim auxEmpCod As Integer = auxContext.Session("empcod")
                Dim pIsAdmin As Boolean = auxContext.Session("isadmin")
                Dim pIsHlpAdmin As Boolean = auxContext.Session("ishlpadmin")
                Dim auxReqhorEnabled As Boolean = auxContext.Session("reqhorenabled")
                Dim auxReturn As String = ""
                Dim pAlertsOnScreen As Boolean = m_Conn.gField_GetBoolean(gLoginPreference_GetValue(enumPrefType.coprfmyconfig, "alertsonscreen"), False)
                Dim auxTMEEnabled As Boolean = False
                auxReturn &= "<ul class=""pureCssMenu pureCssMenum"">"
                If hrcSystemStatus = 1 Or hrcSystemStatus = 10 Then
                    auxReturn &= "<li class=""pureCssMenui""><a class=""pureCssMenui"" >| Administración </a>"
                    auxReturn &= "<ul class=""pureCssMenum"">"

                    auxReturn &= "<li class=""pureCssMenum""><a href=hrcAppConfig.aspx><img src=imagenes/iconconfiguracion.png width=10 />Implementación</a></li>"
                    auxReturn &= "<li class=""pureCssMenum""><a href=hrcLicensing.aspx><img src=imagenes/iconlicencia.png width=10 />Licencias</a></li>"
                    auxReturn &= "<li class=""pureCssMenum""><a href=public_login.aspx?_view_=3><img src=imagenes/actlogout.png width=10 />Logoff/Cerrar</a></li>"

                    auxReturn &= "</ul>"
                    auxReturn &= "</li>"
                Else
                    If Val(hrcSystemDateTimeAdjust) <> 0 Then
                        auxTMEEnabled = True
                    End If

                    Dim auxSysMode As String
                    auxSysMode = LicParam.gValue_Get("SYSMODE", "")
                    m_Alerts.OnlineModeEnabled = pAlertsOnScreen
                    If m_Conn.gField_GetBoolean(gSystem_GetParameterByID(coSysParamEmpShowImage), False) Then
                        Dim auxImagenCod As Integer = m_Conn.gField_GetInt(hrcEntityDT_EMP_FindByKey(auxEmpCod)("imagencod"), -1)
                        If auxImagenCod > 0 Then
                            auxReturn &= "<li class=""pureCssMenui"" >"
                            auxReturn &= "<img src=getbinary.aspx?id=" & auxImagenCod & " border=0 width=64px >"
                            auxReturn &= "</li>"
                        End If
                    End If

                    If pAlertsOnScreen Then
                        auxReturn &= "<li class=""pureCssMenui"" ><a class=""pureCssMenui"" title=""Click aquí para ver alertas""  onclick=""blueBalloon.showTooltip(event,'<iframe id=fmealerts frameborder=0 scrolling=no style=border:0;position:block;float:right src=hrcAlerts.aspx?param1=1></iframe>',1,500,500);return false;"" >"
                        auxReturn &= "<div id=""hrcAlert_alequemsg"" ><img src=imagenes/semaphore_blue.png width=10px height=10px >0</div></a>"
                        auxReturn &= "</li>"
                    End If
                    auxReturn &= "<li class=""pureCssMenui""><a class=""pureCssMenui"" title=Delegaciones href=""#"" onclick=""blueBalloon.showTooltip(event,'<iframe id=fmehelp frameborder=0 scrolling=no style=border:0 src=hrcfastActions.aspx?param1=1 ></iframe>',1,750,300);return false;"" >"
                    auxReturn &= "&nbsp<img width=12px src=imagenes/" _
                           & If(m_Security.IsDelegatedSession, "objfastaccess_delegations_yes.png", "objfastaccess_delegations_no.png") _
                           & " border=0 " _
                           & "></a>"
                    auxReturn &= "</li>"
                    auxReturn &= "<li class=""pureCssMenui""><a class=""pureCssMenui"" href=""cfrmdocumentos.aspx?_view_=12"">" _
                        & "&nbsp<img width=12 src=imagenes/objhome.png width=14px >" _
                        & "</a>"
                    auxReturn &= "</li>"
                    If pIsAdmin Then
                        auxReturn &= "<li class=""pureCssMenui""><a class=""pureCssMenui"" href=""#"">| Ir a </a>"
                        auxReturn &= "<ul class=""pureCssMenum"">"
                        'auxReturn &= "<li class=""pureCssMenum""><a href=cfrmdocumentos.aspx?_view_=8><img src=imagenes/biblioteca_documentos.png width=10 />Documentos vigentes</a></li>"
                        auxReturn &= "<li class=""pureCssMenum""><a href=cfrmdocumentos.aspx?_view_=2><img src=imagenes/doc_vigentes.png width=10 />Documentos vigentes(por tipo de proceso)</a></li>"
                        auxReturn &= "<li class=""pureCssMenum""><a href=cfrmdocumentos.aspx?_view_=3><img src=imagenes/doc_vigentes.png width=10 />Documentos vigentes(por unidades)</a></li>"
                        auxReturn &= "<li class=""pureCssMenum""><a href=cfrmdocumentos.aspx?_view_=4><img src=imagenes/doc_vigentes.png width=10 />Documentos vigentes(por tipo de documento)</a></li>"
                        If Val(coSystemType) <> 175 Then
                            auxReturn &= "<li class=""pureCssMenum""><a href=cfrmdocumentos.aspx?_view_=10><img src=imagenes/doc_vigentes.png width=10 />Documentos vigentes(por sistema)</a></li>"
                        End If
                        auxReturn &= "<li class=""pureCssMenum""><hr /></li>"
                        auxReturn &= "<li class=""pureCssMenum""><a href=cfrmdocumentos.aspx?_view_=20><img src=imagenes/doc_vigentes.png width=10 />Lectura pendiente</a></li>"

                        auxReturn &= "</ul>"
                        auxReturn &= "</li>"
                    End If

                    auxReturn &= "<li class=""pureCssMenui""><a class=""pureCssMenui"" href=""#"">| Biblioteca</a>"
                    auxReturn &= "<ul class=""pureCssMenum"">"
                    'auxReturn &= "<li class=""pureCssMenum""><a href=cfrmdocumentos.aspx?_view_=9><img src=imagenes/doc_vigentes.png width=10 />Biblioteca </a></li>"
                    auxReturn &= "<li class=""pureCssMenum""><a href=cfrmdocumentos.aspx?_view_=5><img src=imagenes/biblioteca_documentos.png width=10 />Biblioteca (por tipo de proceso)</a></li>"
                    auxReturn &= "<li class=""pureCssMenum""><a href=cfrmdocumentos.aspx?_view_=6><img src=imagenes/biblioteca_documentos.png width=10 />Biblioteca (por unidades)</a></li>"
                    auxReturn &= "<li class=""pureCssMenum""><a href=cfrmdocumentos.aspx?_view_=7><img src=imagenes/biblioteca_documentos.png width=10 />Biblioteca (por tipo de documento)</a></li>"

                    If Val(coSystemType) <> 175 Then
                        auxReturn &= "<li class=""pureCssMenum""><a href=cfrmdocumentos.aspx?_view_=11><img src=imagenes/biblioteca_documentos.png width=10 />Biblioteca (por sistema)</a></li>"
                    End If
                    auxReturn &= "<li class=""pureCssMenum""><hr /></li>"
                    '                    auxReturn &= "<li class=""pureCssMenum""><a href=cfrmdocumentos.aspx?_view_=20><img src=imagenes/biblioteca_documentos.png width=10 />Lectura pendiente</a></li>"
                    auxReturn &= "<li class=""pureCssMenum""><a href=cfrmdocumentos.aspx?_view_=13><img src=imagenes/biblioteca_documentos.png width=10 />Solicitud nuevos documentos</a></li>"
                    auxReturn &= "<li class=""pureCssMenum""><a href=cfrmdocumentos.aspx?_view_=14><img src=imagenes/biblioteca_documentos.png width=10 />Solicitud nuevas versiones</a></li>"
                    auxReturn &= "<li class=""pureCssMenum""><a href=cfrmdocumentos.aspx?_view_=15><img src=imagenes/biblioteca_documentos.png width=10 />Edición</a></li>"
                    auxReturn &= "<li class=""pureCssMenum""><a href=cfrmdocumentos.aspx?_view_=16><img src=imagenes/biblioteca_documentos.png width=10 />Revisión</a></li>"
                    auxReturn &= "<li class=""pureCssMenum""><a href=cfrmdocumentos.aspx?_view_=17><img src=imagenes/biblioteca_documentos.png width=10 />Aprobación</a></li>"
                    auxReturn &= "<li class=""pureCssMenum""><a href=cfrmdocumentos.aspx?_view_=18><img src=imagenes/biblioteca_documentos.png width=10 />Publicación</a></li>"
                    auxReturn &= "<li class=""pureCssMenum""><a href=cfrmdocumentos.aspx?_view_=19><img src=imagenes/biblioteca_documentos.png width=10 />Cancelación</a></li>"
                    Dim auxGrpCodList As New List(Of Integer)
                    auxGrpCodList.Add(coGroupDocumentadorAdministradores)
                    auxGrpCodList.Add(coGroupDocumentadorEditores)
                    auxGrpCodList.Add(coGroupIDAdministradores)
                    Dim auxGestion As Boolean = m_Security.gMember_IsInGroupByID(auxGrpCodList)
                    If Val(coSystemType) <> 175 And (pIsAdmin Or auxGestion) Then
                        auxReturn &= "<li class=""pureCssMenum""><hr /></li>"
                        'auxReturn &= "<li class=""pureCssMenum""><a href=cfrmreportes.aspx?_view_=10><img src=imagenes/objchart.png width=10 />Resumenes</a></li>"
                        auxReturn &= "<li class=""pureCssMenum""><a href=cfrmreportes.aspx?_mode_=0><img src=imagenes/objchart.png width=10 />Reportes</a></li>"
                    End If
                    'auxReturn &= "<li class=""pureCssMenum""><a href=cfrmdocumentos.aspx?_view_=11><img src=imagenes/icon00000001.png width=10 />Biblioteca (por sistema)</a></li>"
                    auxReturn &= "</ul>"
                    auxReturn &= "</li>"

                    If pIsAdmin Or auxGestion Then
                        auxReturn &= "<li class=""pureCssMenui""><a class=""pureCssMenui"" href=""#"">| Gestión </a>"
                        auxReturn &= "<ul class=""pureCssMenum"">"
                        auxReturn &= "<li class=""pureCssMenum""><a href=frmunidades.aspx><img src=imagenes/icon00000010.png width=10 />Unidades</a></li>"
                        auxReturn &= "<li class=""pureCssMenum""><a href=frmFuncionesyequipos.aspx><img src=imagenes/icon00000014.png width=10 />Funciones y equipos</a></li>"
                        auxReturn &= "<li class=""pureCssMenum""><a href=frmcolaboradores.aspx><img src=imagenes/icon00000009.png width=10 />Colaboradores</a></li>"
                        auxReturn &= "<li class=""pureCssMenum""><a href=frmtiposdedocumentos.aspx><img src=imagenes/icon00000004.png width=10 />Tipos de documentos</a></li>"
                        auxReturn &= "<li class=""pureCssMenum""><a href=frmsistemas.aspx><img src=imagenes/icon00000002.png width=10 />Sistemas</a></li>"
                        auxReturn &= "<li class=""pureCssMenum""><a href=frmtiposdeprocesos.aspx><img src=imagenes/icon00000003.png width=10 />Tipos de procesos</a></li>"
                        auxReturn &= "<li class=""pureCssMenum""><a href=frmprocesos.aspx><img src=imagenes/icon00000031.png width=10 />Procesos</a></li>"
                        auxReturn &= "<li class=""pureCssMenum""><a href=frmclasificaciones.aspx><img src=imagenes/icon00000011.png width=10 />Clasificaciones</a></li>"
                        auxReturn &= "<li class=""pureCssMenum""><a href=frmempresas.aspx><img src=imagenes/icon00000022.png width=10 />Empresas</a></li>"
                        auxReturn &= "<li class=""pureCssMenum""><a href=cfrmdocumentos1_det.aspx?_view_=6><img src=imagenes/icon00000035.png width=10 />Plantillas de roles</a></li>"
                        auxReturn &= "<li class=""pureCssMenum""><a href=frmroles.aspx><img src=imagenes/icon00000006.png width=10 />Roles</a></li>"
                        auxReturn &= "</ul>"
                        auxReturn &= "</li>"
                        If pIsAdmin Then
                            auxReturn &= "<li class=""pureCssMenui""><a class=""pureCssMenui"" href=""#"">| Administración </a>"
                            auxReturn &= "<ul class=""pureCssMenum"">"

                            auxReturn &= "<li class=""pureCssMenui"" ><a class=""pureCssMenui"" title=""Cola de trabajos""  onclick=""blueBalloon.showTooltip(event,'<iframe id=fmejobqueue frameborder=0 scrolling=no style=border:0;position:block;float:right src=hrcJobQueue.aspx?_closea_=1></iframe>',1,700,500);return false;"" >"
                            auxReturn &= "<img src=imagenes/hrcJob.png width=10 />Cola de trabajos</a>"
                            auxReturn &= "</li>"

                            auxReturn &= "<li class=""pureCssMenum""><a href=frmmail.aspx><img src=imagenes/icon00000028.png width=10 />Modelo de mails</a></li>"
                            auxReturn &= "<li class=""pureCssMenum""><a href=frmgrupos.aspx><img src=imagenes/icongrupo.png width=10 />Grupos</a></li>"
                            auxReturn &= "<li class=""pureCssMenum""><a href=frmusuarios.aspx><img src=imagenes/iconusuario.png width=10 />Usuarios</a></li>"
                            'auxReturn &= "<li class=""pureCssMenum""><a href=cfrmConfig.aspx><img src=imagenes/iconconfiguracion.png width=10 />Configuración</a></li>"
                            If Val(coSystemType) <> 175 Or Environment.MachineName = "A16WIN8" Then
                                auxReturn &= "<li class=""pureCssMenum""><a href=frmsysparam.aspx><img src=imagenes/iconparametro.png width=10 />Parámetros del sistema</a></li>"
                                auxReturn &= "<li class=""pureCssMenum""><a href=hrcKYA.aspx><img src=""imagenes/iconClave asimétrica.png"" width=10 />Claves asimétricas</a></li>"
                                auxReturn &= "<li class=""pureCssMenum""><a href=hrcAppConfig.aspx><img src=imagenes/iconconfiguracion.png width=10 />Implementación</a></li>"
                                auxReturn &= "<li class=""pureCssMenum""><a href=hrcLicensing.aspx><img src=imagenes/iconlicencia.png width=10 />Licencias</a></li>"
                            End If
                            auxReturn &= "<li class=""pureCssMenum""><a href=cfrmtest.aspx><img src=imagenes/objtest.png width=10 />Test</a></li>"
                            auxReturn &= "<li class=""pureCssMenum""><a href=hrcComponentsTest.aspx><img src=imagenes/objtest.png width=10 />Test de componentes</a></li>"
                            auxReturn &= "<li class=""pureCssMenum""><a href=frmabout.aspx><img src=imagenes/objinfo.png width=10 />Acerca de...</a></li>"

                            auxReturn &= "<li class=""pureCssMenum""><hr /></li>"
                            auxReturn &= "<li class=""pureCssMenui"" ><a class=""pureCssMenui"" title=""Test envío mail""  onclick=""blueBalloon.showTooltip(event,'<iframe id=fmejobqueue frameborder=0 scrolling=no style=border:0;position:block;float:right src=hrcalert_test.aspx?></iframe>',1,500,500);return false;"" >"
                            auxReturn &= "<img src=imagenes/objMail.png width=10 />Test envío mail</a>"
                            auxReturn &= "</li>"
                            auxReturn &= "</ul>"
                            auxReturn &= "</li>"
                        End If

                    End If


                    'If gNet_IsInInternal() = False Or m_Security.SecNoChangePsw = False Then
                    auxReturn &= "<li class=""pureCssMenui""><a class=""pureCssMenui"" href=""#"">|Usuario</a>"
                    auxReturn &= "<ul class=""pureCssMenum"">"

                    Dim auxUserJobQueue_Code As String = ""
                    Dim auxHTML As New clsHrcCodeHTML
                    auxUserJobQueue_Code = m_UserGenericHandler.gJSCommand_GetCall("UserJobqueue_open", Nothing, _
                                                                                 "blueBalloon.showTooltip(event,'<iframe id=fmejobqueue frameborder=0 scrolling=no style=border:0;position:block;float:right " _
                                                                                 & " src=hrcJobqueue.aspx?_closea_=1&tmpid='+" & auxHTML.gJS_WebService_GetValueFromResult("TMPID") _
                                                                                 & "+'></iframe>',1,500,500);" _
                                                                                 , "hrcConsole_log(textStatus);") _
                                        & "return false;"

                    auxReturn &= "<li class=""pureCssMenum""><a href=hrcmyconfig.aspx?><img src=imagenes/objFastAccess_config_yes.png width=10 />Mi configuración</a></li>"
                    If coSystemType <> "175" Then
                        auxReturn &= "<li class=""pureCssMenum""><a class=""pureCssMenui"" title=""Mis trabajos""  onclick=""" & auxUserJobQueue_Code & """ ><img src=imagenes/hrcJob.png width=10 />Mis trabajos</a></li>"
                        If m_Security.SecNoChangePsw = False Then
                            auxReturn &= "<li class=""pureCssMenum""><a href=public_login.aspx?_view_=2><img src=imagenes/objacctype.png width=10 />Cambiar mi contraseña</a></li>"
                        End If
                        'End If
                        auxReturn &= "<li class=""pureCssMenum""><a href=public_login.aspx?_view_=3><img src=imagenes/actlogout.png width=10 />Logoff/Cerrar</a></li>"

                        auxReturn &= "<li class=""pureCssMenum""><hr /></li>"
                        Dim auxURLWS As String = gSystem_GetParameterByID(coSysParamIDReqURL)
                        If auxURLWS <> "" Then
                            If Right(auxURLWS, 1) <> "/" Then
                                auxURLWS &= "/"
                            End If
                            auxReturn &= "<li class=""pureCssMenum""><a target=_blank  href='hrcSecTransfer.aspx?appurl=" & HttpUtility.UrlEncode(auxURLWS) & "' ><img src=imagenes/objFastAccess_config_yes.png width=10 />Requio</a></li>"
                        End If
                    End If
                    auxReturn &= "</ul>"
                    auxReturn &= "</li>"

                    If auxTMEEnabled Then
                        auxReturn &= "<li class=""pureCssMenui""><a class=""pureCssMenui"" href=""#"">|Entorno</a>"
                        auxReturn &= "<ul class=""pureCssMenum"">"
                        auxReturn &= "<li class=""pureCssMenum""><a href=#><img src=""" & m_WebRootFolder & "imagenes/tme.png"" width=12px bordeR=0 ><span id=tme_date /></a></li>"
                        auxReturn &= "</ul></il>"

                        auxReturn &= "<script type=""text/javascript"" src=""" & m_WebRootFolder & "plugins/jquery/tme/tme.js""></script>"
                        auxReturn &= "<script type=""text/javascript"">" _
                            & "$(document).ready(function() {digitalclock(" & hrcSystemDateTimeAdjust & ",'tme')});" _
                            & "</script>"
                    End If
                    End If

                    auxReturn &= "</ul>"
                    auxContext.Session("user_menu") = auxReturn
            End If

        End If
    End Function


    Friend Sub gAlerts_Start(ByVal pEmpCod As Integer)
        gSys_DebugLogAdd("Starting alerts to:" & pEmpCod)
        Dim auxAleQueCod As Integer
        m_Alerts.gWriters_Stop()
        m_Alerts.gReaders_Stop()

        Dim auxAlertsPublicCod As Integer = Val(gSystem_GetParameterByID(coSysParamIDPublicQueue))
        m_Alerts.gWriters_Start(auxAlertsPublicCod, "Administradores", False)
        If pEmpCod > 0 Then
            Dim auxEmpDT As DataTable = m_Conn.gConn_Query("SELECT alequecod,dsc FROM EMP WHERE cod=" & pEmpCod)
            If auxEmpDT.Rows.Count <> 0 Then
                auxAleQueCod = m_Conn.gField_GetInt(auxEmpDT.Rows(0)("alequecod"), -1)
                m_Alerts.gReaders_StartMonitor(auxAleQueCod)
                m_Alerts.gWriters_Start(auxAleQueCod, auxEmpDT.Rows(0)("dsc"), True)
                'm_Alerts.DefaultFromDsc = auxEmpDT.Rows(0)("dsc")
                gSys_DebugLogAdd("Default queue alerts " & auxAleQueCod)
            End If
            m_Alerts.gReaders_StartPublicMonitors()
            For Each auxRow As DataRow In m_Conn.gConn_Query("SELECT DISTINCT REQ_EQU.alequecod " _
                                                         & " FROM REQ_EQUEMP " _
                                                         & " LEFT JOIN REQ_EQU ON REQ_EQUEMP.equcod=REQ_EQU.cod " _
                                                         & " WHERE empcod=" & pEmpCod).Rows
                m_Alerts.gReaders_StartMonitor(auxRow("alequecod"))
            Next
        End If
        'primero agregar el default
        If m_Security.gMember_IsInGroupByID(m_Security.CurrentSidCod, coGroupIDAdministradores) Then
            m_Alerts.gWriters_StartPublic()
            For Each auxRow As DataRow In m_Conn.gConn_Query("SELECT alequecod,dsc " _
                                                             & " FROM EMP WHERE (baja = 0 OR baja is null) AND alequecod > 0 ").Rows
                m_Alerts.gWriters_Start(auxRow("alequecod"), auxRow("dsc"), False)
            Next
        End If


    End Sub
    Public Sub gTRACE_add(ByVal pProCod As String, _
                           ByVal pTrclevel As Integer, _
                           ByVal pTrcDsc As String)
        Try
            If DebugLogOn Then
                pTrcDsc = Left(pTrcDsc, 500)
                pTrcDsc = Replace(pTrcDsc, "'", "´")
                m_Conn.gConn_Insert("INSERT INTO PRO_TRACELOG (procod,trcfecha,trcdsc,trclevel,qsecsid,qsecdatetime) VALUES(" & pProCod & ",getdate(),'" & pTrcDsc & "'," & pTrclevel & "," & Sec.CurrentSidCod & ",getdate())")
            End If
        Catch ex As Exception
        End Try
    End Sub

    Private Function gReplication_Execute(ByVal pEntityType As enumEntities, _
                                            ByVal pActionType As enumActionType, _
                                            ByVal pOldBagValues As clshrcBagValues, _
                                            ByVal pNewBagValues As clshrcBagValues, _
                                             Optional ByVal pReplicate As Boolean = True, _
                                             Optional ByVal pResyncSecurity As Boolean = False) As clsHrcReplicationClient
        Try


            'Se instancia clshrcGeneral y se setea con las clases del contexto del usuario(m_conn y m_security)
            'Por lo tanto la ejecucion de la replicación será con ese contexto.
            Dim auxClass As New clshrcGeneral
            auxClass.gInit(m_Conn, m_Security)
            auxClass.gSystem_Init()
            Dim auxReplication As clsHrcReplicationClient

            auxReplication = hrcReplicationServer.gComponent_CreateInstance(auxClass)
            Dim auxDTResults As DataTable = Nothing
            If pOldBagValues Is Nothing Then
                'Datasource 1
                Dim auxRemoteConn As clsHrcConnClient
                auxRemoteConn = New clsHrcConnClient
                auxRemoteConn.ActivateLicense(coDBKey)
                Dim auxConnectionString As String = ""
                If auxConnectionString = "" Then
                    auxConnectionString = gSystem_GetParameterByID(coSysParamRepliDSCore)
                End If
                auxRemoteConn.gConn_Open(auxConnectionString, hrcBasType.coBasTypeSQLServerWithAttach, Intelimedia.Hercules.Storage.clsHrcConnClient.hrcSavingMode.coinDBTable)
                gSys_DebugLogAdd("Replicate status:" & auxRemoteConn.LastErrorDescription & ".isConnected:" & auxRemoteConn.isConnected & "." & "1")
                Dim auxDateTime As DateTime '= m_Conn.gField_GetDate(gSystem_GetParameterByID(coSysParamIDLastReplication), New DateTime(1900, 1, 1))
                Dim auxReplicateEQU As Boolean = m_Conn.gField_GetBoolean(gSystem_GetParameterByID(coSysParamRepliEQU), False)
                auxReplication.gReplication_SetRemoteConfig(auxRemoteConn, auxDateTime, -1, True, True, auxReplicateEQU)
                gSys_DebugLogAdd("Replicate-stepA")
                If pResyncSecurity Then
                    auxReplication.gReplication_ReSync()
                    m_Security.gTools_ReimpactGroups()
                End If
                If pReplicate Then
                    auxReplication.gReplication_Replicate()
                End If
                Dim auxMsg As String = ""

                For Each auxDebugMsg As String In auxReplication.gDebug_Get
                    auxMsg &= auxDebugMsg & "</br>"
                    gSys_DebugLogAdd(auxDebugMsg)
                Next
                auxRemoteConn.gConn_Close()
                auxRemoteConn = Nothing
                gSys_DebugLogAdd("Replicate-stepB")
            Else
                auxReplication.gEntity_AddItemChanged(pEntityType, pActionType, pOldBagValues, pNewBagValues)
                auxDTResults = auxReplication.DTChanges
            End If
            auxDTResults = auxReplication.DTChanges
            If auxDTResults IsNot Nothing Then
                Dim auxOldValues As clshrcBagValues
                Dim auxNewValues As clshrcBagValues
                For Each auxRow As DataRow In auxDTResults.Rows
                    auxOldValues = New clshrcBagValues(auxRow("OldValues").ToString)
                    auxNewValues = New clshrcBagValues(auxRow("NewValues").ToString)
                    Select Case auxRow("entityBuildType")
                        Case clsHrcReplicationClient.enumEntityType.coEMP
                            gEntityEMP_PostAction(auxRow("actiontype"), auxOldValues, auxNewValues)
                        Case clsHrcReplicationClient.enumEntityType.coUND
                            gEntityUND_PostAction(auxRow("actiontype"), auxOldValues, auxNewValues)
                        Case clsHrcReplicationClient.enumEntityType.coEQU
                            'gEntityEQU_PostAction(auxRow("actiontype"), auxOldValues, auxNewValues)
                        Case clsHrcReplicationClient.enumEntityType.coEQUMBR
                            gEntityEQUMBR_PostAction(auxRow("actiontype"), auxOldValues, auxNewValues)
                        Case clsHrcReplicationClient.enumEntityType.coUNDROL
                            'gEntityUNDROL_PostAction(auxRow("actiontype"), auxOldValues, auxNewValues)
                    End Select
                Next
                'Dim auxEventBagValue As New clshrcBagValues
                'Dim auxDS As New DataSet
                'auxDS.Tables.Add(auxDTResults)
                'auxEventBagValue.gValue_Add("DT", auxDS.GetXml)
                'hrcEvents.gChange_Create("ReplicationHercules", auxEventBagValue)
            End If
            For Each auxDebugMsg As String In auxReplication.gDebug_Get
                gSys_DebugLogAdd(auxDebugMsg)
            Next
            auxClass.Conn.gConn_Close()
            auxClass.gSystem_End()
            Return auxReplication
        Catch ex As Exception
            gSys_DebugLogAdd("Replication exception:" & ex.Message)
        End Try
    End Function
    Public Function gEntityEQUMBR_PostAction(ByVal pAction As enumActionType, _
                                     ByVal pOldValues As clshrcBagValues, _
                                     ByVal pNewValues As clshrcBagValues) As String
        Try
            Dim auxReturn As String = ""
            Dim auxMiembrosGrpCod As Integer = -1
            If pOldValues Is Nothing Then
                pOldValues = New clshrcBagValues
            End If
            Dim auxCod As Integer = m_Conn.gField_GetInt(pNewValues.gValue_Get("cod"), -1)
            Dim auxEQURow As DataRow = hrcEntityDT_DOC_EQU_FindByKey(pNewValues.gValue_Get("equcod", -1))
            auxMiembrosGrpCod = m_Conn.gField_GetInt(auxEQURow("miembrosgrpcod"), -1)
            Select Case pNewValues.gValue_Get("mbrtypecod")
                Case enumEntities.coEntityUND
                    Dim auxUndCod As Integer = m_Conn.gField_GetInt(pNewValues.gValue_Get("undcod"), -1)
                    'Responsables

                    If pOldValues.gValue_Get("grupoeditores") IsNot Nothing Then
                        Dim auxGrupoEditores As Boolean = m_Conn.gField_GetBoolean(pNewValues.gValue_Get("grupoeditores"), False)
                        Dim auxUndRow As DataRow = hrcEntityDT_UND_FindByKey(auxUndCod)
                        Dim auxGrpCod As Integer = m_Conn.gField_GetInt(auxUndRow("grpcodeditor"), -1)
                        If auxGrupoEditores Then
                            pAction = enumActionType.coConfirmInsert
                        Else
                            pAction = enumActionType.coConfirmDelete
                        End If
                        If pAction = enumActionType.coConfirmInsert Then
                            m_Security.gGroup_AddGroup(auxMiembrosGrpCod, auxGrpCod)
                        ElseIf pAction = enumActionType.coConfirmDelete Then
                            m_Security.gGroup_DelGroup(auxMiembrosGrpCod, auxGrpCod)
                        End If
                    End If
            End Select
        Catch ex As Exception
            gSys_DebugLogAdd("gEntityEQUMBR_PostAction-Exception:" & ex.Message)
        End Try
    End Function
    Public Sub gReplication_Start()

        Dim auxConn As clsHrcConnClient
        auxConn = New clsHrcConnClient
        auxConn.ActivateLicense(coDBKey)
        Dim auxConnectionString As String = ""

        If auxConnectionString = "" Then
            auxConnectionString = gSystem_GetParameterByID(coSysParamRepliDSCore)
        End If
        auxConn.gConn_Open(auxConnectionString, hrcBasType.coBasTypeSQLServerWithAttach, Intelimedia.Hercules.Storage.clsHrcConnClient.hrcSavingMode.coinDBTable)
        gSys_DebugLogAdd("Replicate status:" & auxConn.LastErrorDescription & ".isConnected:" & auxConn.isConnected)
        Dim auxDateTime As DateTime '= m_Conn.gField_GetDate(gSystem_GetParameterByID(coSysParamIDLastReplication), New DateTime(1900, 1, 1))

        Dim auxClassGeneral As New clshrcGeneral
        auxClassGeneral.gSystem_Init()
        Dim auxReplication As clsHrcReplicationClient = gReplication_Execute(-1, enumActionType.coUndefined, Nothing, Nothing)

        auxClassGeneral.gSystem_End()


        'gSystem_SetParameterByID(coSysParamIDLastReplication, auxDateTime.ToString("yyyy-MM-ddTHH:mm:ss"))

        Dim auxQueueList As New List(Of Integer)
        For Each auxRow As DataRow In m_Security.gGroup_ResolveLogins(m_Security.gGroup_GetCodByID(coGroupDocumentadorAdministradores)).Rows
            Dim auxQueCod As Integer = m_Conn.gConn_QueryValue("SELECT alequecod FROM EMP WHERE seccod=" & auxRow("seccod"), -1)
            If auxQueCod > 0 Then
                auxQueueList.Add(auxQueCod)
            End If
        Next
    End Sub
    Private Sub gJobs_ClearOld()
        Dim auxDebugFechaDelete As Date = m_Conn.gDate_GetToday.AddDays(-90)
        gTRACE_add(-1, 10, "Elimina logs anteriores a " & auxDebugFechaDelete.ToShortDateString)
        m_Conn.gConn_ExecuteProcedureUpdate("DELETE DEBUGLOG WHERE fecha <  " & m_Conn.gFieldDB_GetDateTime(auxDebugFechaDelete), 60 * 5)
        m_Conn.gConn_ExecuteProcedureUpdate("DELETE PRO_TRACELOG WHERE qsecdatetime <  " & m_Conn.gFieldDB_GetDateTime(auxDebugFechaDelete), 60 * 30)

        m_Security.gLogin_DeleteOldDelegations()
        m_Security.gLogin_DeleteOldSessions()

        m_Alerts.gSystemUser_Enabled()
        m_Alerts.gSystem_DeleteOldAlerts(15)
        m_Alerts.gSystemUser_Disabled()

    End Sub
    Public Sub gJobs_Replication()
        gSys_DebugLogAdd("Replicate")
        gReplication_Start()
        gSys_DebugLogAdd("Replicate end. Send alerts")
    End Sub
    Private Sub gJobs_DailyProcess(ByVal pTask As clsCustomBasicTask)
        'Crear cola de mensajes a colaboradores con usuario y sin cola
        Dim auxNewValues As New clshrcBagValues
        For Each auxrow As DataRow In m_Conn.gConn_Query("SELECT cod FROM EMP " _
                                                         & " WHERE seccod > 0 AND alequecod < 0" _
                                                         & " AND (EMP.baja {#ISNULL#} OR EMP.baja={#FALSE#})").Rows
            auxNewValues.gValue_Add("COD", auxrow("cod"))
            gEntityEMP_PostAction(enumActionType.coConfirmModify, auxNewValues, auxNewValues)
        Next
        'Resyincronizar seguridad
        Dim auxReplication As clsHrcReplicationClient = gReplication_Execute(-1, enumActionType.coUndefined, Nothing, Nothing, pReplicate:=False, pResyncSecurity:=True)

        'autoedicion
        gDOC_AutoEdition()
        gDOC_AutoPublicacion()
        Dim auxUpdateEditionRole As Boolean = m_Conn.gField_GetBoolean(gSystem_GetParameterByID(coSysParamIDDOCSGNUpdateEditionRoleDaily), False)
        Dim auxCod As Integer
        Dim auxErrors As New List(Of String)
        Dim auxResult As clshrcBagValues
        Dim axuResultError As List(Of String)
        For Each auxDocRow As DataRow In m_Conn.gConn_Query("SELECT cod " _
                                              & " FROM DOC_DOC " _
                                              & " WHERE cod > 0 " _
                                              & " AND (baja {#ISNULL#} OR baja = {#FALSE#})" _
                                                ).Rows
            auxCod = auxDocRow("cod")
            auxResult = gDoc_ReApply(auxCod, auxUpdateEditionRole, -1, True)
            axuResultError = auxResult.gValue_Get("ERRORS")
            auxErrors.AddRange(axuResultError)
        Next
        If auxErrors.Count <> 0 Then
            Dim auxMsgContent As String = "Se han analizado los documentos y se han encontrado los siguientes problemas:<br />"
            For Each auxError As String In auxErrors
                auxMsgContent &= "<br />" & auxError
            Next
            gAlert_SendToAdmin("Documentos|roles", auxMsgContent, clsHrcAlertClient.enumLevel.coError)
        End If
        gDoc_ReIDAuto()
        gDOC_SendMails()
        gJobs_ClearOld()
        If m_Conn.LastErrorDescription <> "" Then
            gSys_DebugLogAdd(pTask.ExecutionJobID & "-Error:" & m_Conn.LastErrorDescription)
        End If
        gSys_DebugLogAdd(pTask.ExecutionJobID & "-end")
    End Sub
    Public Overrides Function gSystem_ExecuteTask(ByVal pTask As clsCustomBasicTask) As String
        Dim auxReturn As String = ""
        gSystem_Init()
        m_Conn.gConn_Open()
        Dim auxTask_Type As String = ""
        If pTask.BagValues.gValue_Get("task_type") IsNot Nothing Then
            auxTask_Type = pTask.BagValues.gValue_Get("task_type").ToString.ToLower
        End If
        Dim auxSESID As String = pTask.BagValues.gValue_Get("sesid")
        gSys_DebugLogAdd("Schedule tasks start.Task_type:" & auxTask_Type)
        gSys_DebugLogAdd("SesID:" & auxSESID)
        If auxSESID <> "" Then
            gSystem_CheckAccess(auxSESID)
        End If
        Dim auxParams As clshrcBagValues = pTask.BagValues.gValue_Get("params")
        If m_Conn.gField_GetBoolean(pTask.BagValues.gValue_Get("maildisabled"), False) Then
            m_mailDisabled = True
        End If
        Try
            Select Case auxTask_Type
                Case "jobs_replication"
                    gJobs_Replication()
                Case "workflow_changestep"
                    m_Security.gLogin_SessionLogin(auxSESID)
                    gWorkflow_GotoStep_EXE(pTask.BagValues, 1)
                Case "doc_avisos"
                    gJobs_DailyProcess(pTask)
                Case "doc_postaction"
                    Dim auxCod As Integer = Val(pTask.BagValues.gValue_Get("cod"))
                    If auxCod > 0 Then
                        Dim auxAction As Integer = Val(pTask.BagValues.gValue_Get("action"))
                        gTRACE_add(auxCod, 10, "Doc-PostAction")
                        gEntity_DOC_UpdateChilds(auxCod)
                        Dim auxResult() As DataRow
                        Dim auxDT As DataTable = m_Conn.gConn_Query("SELECT doctipcod FROM DOC_DOCVIG WHERE cod=" & auxCod)
                        If auxDT.Rows.Count <> 0 Then
                            auxResult = hrcEntityDT_DOC_DOCTIP.Select("cod=" & auxDT.Rows(0)("doctipcod"))
                            If auxResult.Count <> 0 Then
                                Dim auxDTDOC As DataTable = m_Conn.gConn_Query("SELECT doctipcod,procod,clacod,siscod,dsc,obs,undcod,docsupcod " _
                                                            & " FROM DOC_DOC WHERE cod=" & auxCod)
                                If m_Conn.gField_GetBoolean(auxResult(0)("permvigcambiadsc"), False) Then
                                    gEntity_DOC_DOCVIG_SystemUpdate(pcod:=auxCod, pdsc:=auxDTDOC.Rows(0)("dsc"))
                                End If

                                If m_Conn.gField_GetBoolean(auxResult(0)("permvigcambiaotros"), False) Then
                                    gEntity_DOC_DOCVIG_SystemUpdate(pcod:=auxCod, _
                                                                     pobs:=auxDTDOC.Rows(0)("obs"), _
                                                                     psiscod:=auxDTDOC.Rows(0)("siscod"), _
                                                                     pprocod:=auxDTDOC.Rows(0)("procod"), _
                                                                     pclacod:=auxDTDOC.Rows(0)("procod"), _
                                                                     pdoctipcod:=auxDTDOC.Rows(0)("doctipcod"), _
                                                                     pundcod:=auxDTDOC.Rows(0)("undcod"), _
                                                                     pdocsupcod:=auxDTDOC.Rows(0)("docsupcod"))
                                End If
                            End If
                        End If
                    End If
                  

                    gTRACE_add(auxCod, 10, "Doc-PostAction-end")
                Case "equ_postaction"
                    Dim auxCod As Integer = Val(pTask.BagValues.gValue_Get("cod"))
                    Dim auxAction As Integer = Val(pTask.BagValues.gValue_Get("action"))
                    Dim auxBagValuesOldValues As clshrcBagValues = pTask.BagValues.gValue_Get("oldvalues")
                    Dim auxBagValuesNewValues As clshrcBagValues = pTask.BagValues.gValue_Get("newvalues")
                    gSys_DebugLogAdd("DOC_EQU-PostAction")

                    Dim auxReplication As clsHrcReplicationClient = gReplication_Execute(enumEntities.coEntityDOC_EQU, _
                                                                                         auxAction, auxBagValuesOldValues, auxBagValuesNewValues)
            End Select
        Catch ex As Exception
            gSys_DebugLogAdd("Excepción de jobs-" & m_Conn.gDate_GetNow & "-" & ex.Message _
                            & "<br />" & ex.Message.ToString)
            gAlert_SendToDev("Excepción de jobs" _
                             , m_Conn.gDate_GetNow & "-" & ex.Message _
                             & "<br />" & coWebRootFolder _
                             & "<br />" & ex.Message _
                             & "<br />" & ex.InnerException.ToString _
                              , clsHrcAlertClient.enumLevel.coError)
        End Try
        m_Conn.ConnShared = False
        m_Conn.gConn_Close()
        gSystem_End()
        Return MyBase.gSystem_ExecuteTask(pTask)
        Return auxReturn
    End Function
    Public Overloads Function gWorkflow_GotoStep(ByVal pValues As clshrcBagValues) As String
        Return gWorkflow_GotoStep_EXE(pValues, 0)
    End Function
    Public Overloads Sub gWorkflow_GotoStep_inQueue(ByVal pValues As clshrcBagValues)
        If hrcProcessQueue Is Nothing Then
            Dim auxResult As String = "La cola de procesos no se encuentra activada. Contacte al administrador"
            pValues.gValue_Add("HRC_RESULT", auxResult)
            Exit Sub
        End If

        Dim auxSESID As String = m_Security.gLogin_CreateDelegatedSessionToSystem
        Dim auxEmpCod As Integer = m_Conn.gField_GetInt(pValues.gValue_Get("empcod", -1))

        Dim auxSecCod As Integer = m_Security.CurrentSourceSecCod
        If Val(pValues.gValue_Get("seccod")) > 0 Then
            auxSecCod = Val(pValues.gValue_Get("seccod"))
        End If
        Dim auxDelEmpCod As Integer = -1
        Dim auxRows() As DataRow
        auxRows = hrcEntityDT_EMP.Select("seccod=" & auxSecCod)
        If auxRows.Count <> 0 Then
            auxDelEmpCod = auxRows(0)("cod")
            ' auxWFWTitle &= "(" & auxRows(0)("dsc").ToString & ")"
        End If
        '  auxWFWTitle &= m_Conn.gDate_GetNow.ToString("d/M/yyyy HH:mm:ss")
        If auxDelEmpCod < 1 Then
            auxDelEmpCod = auxEmpCod
        End If
        If auxDelEmpCod < 1 Then
            auxDelEmpCod = -1
        End If
        pValues.gValue_Add("delempcod", auxDelEmpCod)

        Dim auxparam As New Intelimedia.inTasks.clsTaskinQueue
        pValues.gValue_Add("task_type", "workflow_changestep")
        pValues.gValue_Add("sesid", auxSESID)

        Dim auxExecutionParam As New clshrcBagValues

        auxExecutionParam.gValue_Add("empcod", auxEmpCod)
        auxparam.gExecutionParams_Set(auxExecutionParam)


        Dim auxTask As New clsCustomBasicTask
        auxTask.BagValues = pValues
        auxparam.Tasks.Add(1, auxTask)
        Dim auxCod As String = pValues.gValue_Get("cod")
        Dim auxGotoStep As Integer = m_Conn.gField_GetInt(pValues.gValue_Get("gotoStep", -1))
        Dim auxWFWTitle As String = "Workflow|Doc." & auxCod & "|" & hrcEntityDT_Q_WFWSTP_FindByKey(auxGotoStep)("wfwstpdsc")
        auxparam.ExecutionTitle = auxWFWTitle
        hrcProcessQueue.gProcessor_AddTask(auxparam)
        pValues.gValue_Add("HRC_EXECUTIONQUEUEID", auxparam.ExecutionQueueID)
    End Sub
    Private Function gDoc_GetIdentificador(ByVal pDocTipCod As Integer, _
                                           ByVal pDocUndCod As Integer, _
                                           ByVal pDocApaCod As Integer, _
                                           ByVal pDocNro As Integer, _
                                           ByVal pDocVersion As Integer) As String
        Dim auxReturn As String = ""
        Dim auxRowDocTip As DataRow = hrcEntityDT_DOC_DOCTIP_FindByKey(pDocTipCod)
        Dim auxRowUnd As DataRow = hrcEntityDT_UND(pDocUndCod)

        If m_Conn.gField_GetBoolean(auxRowDocTip("noespecificos")) Then
            'Si el tipo de documento no soporta un formato especifico, se toma el formato general
            auxReturn = m_Conn.gField_GetString(auxRowDocTip("formato"))
        Else
            If pDocUndCod > 0 Then
                If m_Conn.gField_GetString(auxRowUnd("formatoespecifico")) <> "" Then
                    'Si la unidad tiene un formato especifico toma ese
                    auxReturn = m_Conn.gField_GetString(auxRowUnd("formatoespecifico"))
                Else
                    'Si el tipo de documento SOPORTA formatos especificos y la unidad no tiene nada definido
                    ', se toma el formato especifico predeterminado
                    auxReturn = m_Conn.gField_GetString(auxRowDocTip("formatoespecifico"))
                End If
            Else
                auxReturn = m_Conn.gField_GetString(auxRowDocTip("formato"))
            End If
        End If
        auxReturn = auxReturn.ToUpper.Trim
        'Abrev del tipo de documento
        auxReturn = Replace(auxReturn, "{#T#}", m_Conn.gField_GetString(auxRowDocTip("abrev")))
        'Codigo de sector
        If pDocUndCod > 0 Then
            If auxRowUnd IsNot Nothing Then
                auxReturn = Replace(auxReturn, "{#S#}", m_Conn.gField_GetInt(auxRowUnd("undnro")))
                auxReturn = Replace(auxReturn, "{#SS#}", m_Conn.gField_GetInt(auxRowUnd("undnro")).ToString("00"))
                auxReturn = Replace(auxReturn, "{#SSS#}", m_Conn.gField_GetInt(auxRowUnd("undnro")).ToString("000"))
            End If
        End If
        auxReturn = Replace(auxReturn, "{#S#}", "")
        auxReturn = Replace(auxReturn, "{#SS#}", "")
        auxReturn = Replace(auxReturn, "{#SSS#}", "")

        'numero de documento
        If pDocNro > 0 Then
            auxReturn = Replace(auxReturn, "{#Z#}", pDocNro)
            auxReturn = Replace(auxReturn, "{#ZZ#}", pDocNro.ToString("00"))
            auxReturn = Replace(auxReturn, "{#ZZZ#}", pDocNro.ToString("000"))
        End If
        auxReturn = Replace(auxReturn, "{#Z#}", "")
        auxReturn = Replace(auxReturn, "{#ZZ#}", "")
        auxReturn = Replace(auxReturn, "{#ZZZ#}", "")
        'apartado
        Dim auxRowAPA As DataRow = hrcEntityDT_DOC_APA_FindByKey(pDocApaCod)
        If auxRowAPA IsNot Nothing Then
            auxReturn = Replace(auxReturn, "{#N#}", m_Conn.gField_GetString(auxRowAPA("abrev")))
            auxReturn = Replace(auxReturn, "{#NN#}", m_Conn.gField_GetString(auxRowAPA("abrev")).ToString)
            auxReturn = Replace(auxReturn, "{#NNN#}", m_Conn.gField_GetString(auxRowAPA("abrev")).ToString)
        End If
        auxReturn = Replace(auxReturn, "{#N#}", "")
        auxReturn = Replace(auxReturn, "{#NN#}", "")
        auxReturn = Replace(auxReturn, "{#NNN#}", "")

        'Numero de version
        If pDocVersion > 0 Then
            auxReturn = Replace(auxReturn, "{#X#}", pDocVersion)
            auxReturn = Replace(auxReturn, "{#XX#}", pDocVersion.ToString("00"))
            auxReturn = Replace(auxReturn, "{#XXX#}", pDocVersion.ToString("000"))
        End If
        auxReturn = Replace(auxReturn, "{#X#}", "")
        auxReturn = Replace(auxReturn, "{#XX#}", "")
        auxReturn = Replace(auxReturn, "{#XXX#}", "")
        Return auxReturn
    End Function
    
    Public Overloads Function gWorkflow_GotoStep_EXE(ByVal pValues As clshrcBagValues, _
                                           ByVal pJobQueueLevel As Short) As String
        Dim auxResult As String = ""
        Dim auxJobQueueLevel As Short = pJobQueueLevel + 1
        Dim pCod As Integer = m_Conn.gField_GetInt(pValues.gValue_Get("Cod"))
        Dim pGotoStep As Integer = m_Conn.gField_GetInt(pValues.gValue_Get("gotoStep"))
        Dim pDsc As String = m_Conn.gField_GetString(pValues.gValue_Get("dsc"))
        Dim pObs As String = m_Conn.gField_GetString(pValues.gValue_Get("obs"))
        Dim pOnlyRunChecks As Boolean = m_Conn.gField_GetInt(pValues.gValue_Get("onlyrunchecks"))
        Dim pDftGenCod As String = m_Conn.gField_GetString(pValues.gValue_Get("dftgencod"))
        Dim auxEmpCod As Integer = m_Conn.gField_GetInt(pValues.gValue_Get("empcod"), -1)
        Dim auxDelEmpCod As Integer = m_Conn.gField_GetInt(pValues.gValue_Get("delempcod"), -1)
        If pGotoStep < 1 Then
            Return ""
            Exit Function
        End If
        Dim auxSecCod As Integer = -1
        If auxEmpCod > 0 Then
            auxSecCod = m_Conn.gField_GetInt(hrcEntityDT_EMP_FindByKey(auxEmpCod)("seccod"), -1)
        End If
        If pOnlyRunChecks = False Then
            gTRACE_add(pCod, 1, "Proceso de item [" & pCod & "] para pasar al paso [" & pGotoStep & "][" & CType(pGotoStep, enumWorkflowStep).ToString & "].Empcod=" & auxEmpCod & ".QueueLevel=" & auxJobQueueLevel)
        End If
        Try
            Dim auxTableName As String = "DOC_DOC"
            Dim auxWhere As String = ""
            If pDftGenCod <> "" Then
                auxTableName = "DOC_DOC_DFT AS DOC_DOC"
                auxWhere &= " AND DOC_DOC.dftdidgencod='" & pDftGenCod & "'"
            End If
            Dim auxDT As DataTable = m_Conn.gConn_Query("SELECT DOC_DOC.cod,DOC_DOC.dsc,DOC_DOC.nro,DOC_DOC.fecha,DOC_DOC.siscod,DOC_DOC.procod,DOC_DOC.clacod,DOC_DOC.version,DOC_DOC.obs,DOC_DOC.wfwstatus,DOC_DOC.doctipcod,DOC_DOC.undcod,DOC_DOC.identificador,DOC_DOC.orden,DOC_DOC.qsidcod,DOC_DOC.trocod,DOC_DOC.wfwmode,DOC_DOC.docsupcod" _
                                                               & ",DOC_PRO.apacod" _
                                                               & ",wfwstatus as wfwstpcod" _
                                                               & " ,(SELECT TOP 1 wfwstpdsc FROM Q_WFWSTP WHERE Q_WFWSTP.wfwstpcod= DOC_DOC.wfwstatus) as wfwstpdsc  " _
                                                               & " FROM " & auxTableName _
                                                               & " LEFT JOIN DOC_PRO ON DOC_DOC.procod=DOC_PRO.cod " _
                                                               & " LEFT JOIN DOC_APA ON DOC_PRO.apacod=DOC_APA.cod " _
                                                               & " LEFT JOIN DOC_CLA ON DOC_DOC.clacod=DOC_CLA.cod " _
                                                               & " WHERE DOC_DOC.cod=" & pCod & auxWhere)
            If auxDT.Rows.Count = 0 Then
                gTRACE_add(pCod, 1, "No existe el item.Error:[" & m_Conn.LastErrorDescription & "]")
                Return ""
                Exit Function
            End If
            Dim auxWfwStpCodCurrent As enumWorkflowStep = m_Conn.gField_GetInt(auxDT.Rows(0)("wfwstatus"), -1)
            If auxJobQueueLevel = 1 Then
                gTRACE_add(pCod, 5, "Bloqueo de item [" & pCod & "].Estado[" & auxWfwStpCodCurrent & "]")
                gEntity_DOC_DOC_Update(pcod:=pCod, pwfwlocked:=True)
            End If
            'Busca datos de seguridad
            Dim auxDocTipCod As Integer = m_Conn.gField_GetInt(auxDT.Rows(0)("doctipcod"))
            Dim auxsidCod As Integer = m_Conn.gField_GetInt(auxDT.Rows(0)(m_Security.SidCodField))
            Dim auxAclCod As Integer = m_Security.gACL_GetFromSIDcod(auxsidCod)
            'Dim auxDocVersion As Integer = m_Conn.gField_GetInt(auxDT.Rows(0)("version"))
            Dim auxTroCod As Integer = m_Conn.gField_GetInt(auxDT.Rows(0)("trocod"), -1)
            Dim auxSendMails As Boolean = False
            'Busca datos de workflow
            Dim auxRowDOCTIP As DataRow = hrcEntityDT_DOC_DOCTIP_FindByKey(auxDocTipCod)
            If auxRowDOCTIP Is Nothing Then
                auxResult = "No existe el tipo de documento."
            End If

            Dim auxInitialize As Boolean = False
            If auxsidCod < 1 Then
                auxInitialize = True
            ElseIf pValues.gValue_Get("initialize") = "1" Then
                auxInitialize = True
            End If
            Dim auxForce As Boolean = False
            If pValues.gValue_Get("force") = "1" Then
                gTRACE_add(pCod, 5, "Forzado!")
                auxForce = True
            End If
            If auxJobQueueLevel >= 2 And auxInitialize Then
                gTRACE_add(pCod, 5, "Inicializacion")
                Dim auxUpdateStr As String = ""
                If auxsidCod < 1 Then
                    If auxAclCod < 1 Then
                        auxAclCod = m_Security.gACL_Create(pDsc, -1, enumEntities.coEntityDOC_DOC)
                    End If
                    gTRACE_add(pCod, 5, "Inicializacion-Nueva ACL...")
                    auxsidCod = m_Security.gSID_NewSID(enumEntities.coEntityDOC_DOC, auxAclCod)
                    auxUpdateStr &= ",qsidcod=" & auxsidCod

                    'Permiso a quien la crea - puede que no sea el requirente
                    m_Security.gACL_AddLogin(auxAclCod, m_Security.CurrentSecCod, enumAccessType.coSYSGlobalCambiarestado)

                    'Permiso a quien la crea de creador, para poder mantener la lectura durante los cambios de estado
                    m_Security.gACL_AddLogin(auxAclCod, m_Security.CurrentSecCod, enumAccessType.coSYSCreador)

                    'permiso a los creados de documentos
                    'm_Security.gACL_AddGroupByID(auxAclCod, coGroupIDDocumentadorCreadores, enumAccessType.coSYSGlobalCambiarestado)
                End If
                If m_Conn.gField_GetBoolean(gSystem_GetParameterByID(coSysParamIDDOCNroAssignAtCreation), False) Then
                    Dim auxNro As Integer
                    auxNro = m_Conn.gField_GetInt(auxDT.Rows(0)("nro"), -1)
                    If auxNro < 1 Then
                        auxNro = m_Conn.gConn_QueryValue("SELECT MAX(nro) FROM DOC_DOC WHERE cod > 0 ", -1) + 1
                        auxUpdateStr &= ",nro=" & auxNro
                        gTRACE_add(pCod, 5, "Inicializacion-Asigna número:" & auxNro)
                        auxSendMails = True
                    End If
                End If
                If auxUpdateStr <> "" Then
                    m_Conn.gConn_Update("UPDATE DOC_DOC SET wfwstatus=" & coWFWSTPDOC_DOCCreacion _
                                    & auxUpdateStr & " WHERE cod =" & pCod)
                End If


                'Permisos a administradores
                '    'Otorgar permiso de escritura al usuario actual (creador) y a quien propone
                '    'El usuario actual se debe agregar, porque si aún no se cargó quien propone
                '    'El usuario que propone es quien tiene realmente los permisos de 
                m_Security.gACL_AddGroupByID(auxAclCod, coGroupDocumentadorAdministradores, enumAccessType.coSYSGlobalCambiarpermisos)
                'm_Security.gACL_AddGroupByID(auxAclCod, coGroupDocumentadorAdministradores, enumAccessType.coSYSGlobalCambiarestado)
                m_Security.gACL_AddGroupByID(auxAclCod, coGroupDocumentadorAdministradores, enumAccessType.coSYSGlobalEliminar)
                m_Security.gACL_AddGroupByID(auxAclCod, coGroupDocumentadorAdministradores, enumAccessType.coSYSGlobalLeer)
                m_Security.gACL_AddGroupByID(auxAclCod, coGroupDocumentadorAdministradores, enumAccessType.coSYSGlobalModificar)

                'm_Security.gACL_AddGroupByID(auxAclCod, coGroupDocumentadorEditores, enumAccessType.coSYSGlobalCambiarestado)
                m_Security.gACL_AddGroupByID(auxAclCod, coGroupDocumentadorEditores, enumAccessType.coSYSGlobalEliminar)
                m_Security.gACL_AddGroupByID(auxAclCod, coGroupDocumentadorEditores, enumAccessType.coSYSGlobalLeer)
                m_Security.gACL_AddGroupByID(auxAclCod, coGroupDocumentadorEditores, enumAccessType.coSYSGlobalModificar)

                m_Security.gACL_AddGroupByID(auxAclCod, coGroupDocumentadorVisualizadores, enumAccessType.coSYSGlobalLeer)

                gTRACE_add(pCod, 5, "Terminado inicializacion")
            End If
            Dim auxWFWMode As Integer = m_Conn.gField_GetInt(auxDT.Rows(0)("wfwmode"))
            Dim auxWfwStpDscCurrent As String = m_Conn.gField_GetString(auxDT.Rows(0)("wfwstpdsc"))
            Dim auxWfwStpDscNext As String = ""
            Dim auxWfwNextEst As DataTable = m_Conn.gConn_Query("SELECT wfwstpdsc FROM Q_WFWSTP WHERE wfwstpcod = " & pGotoStep)
            If auxWfwNextEst.Rows.Count > 0 Then
                auxWfwStpDscNext = auxWfwNextEst.Rows(0)(0)
            End If
            'Dim auxEventDsc As String = "Cambio de estado " & auxWfwStpDscCurrent & " a " & auxWfwStpDscNext
            Dim auxEventObs As String = ""
            Dim auxGotoStepNext As enumWorkflowStep = -1
            Dim auxGotoStepToSave As enumWorkflowStep = pGotoStep
            Dim auxGotoStepCancel As Boolean = False
            Dim auxHstHidGenCod As Integer = -1

            Dim auxOrder As Integer = -1
            Select Case pGotoStep
                Case coWFWSTPDOC_DOCCreacion
                    If auxWfwStpCodCurrent = coWFWSTPDOC_DOCSolicitudnuevodocumentorechazada Then
                    Else
                        Dim auxDsc As String = m_Conn.gField_GetString(auxDT.Rows(0)("dsc"), "")
                        If auxDsc = "" Then
                            auxResult &= "El título del documento es obligatorio."
                        ElseIf m_Conn.gConn_Query("SELECT cod FROM DOC_DOC WHERE cod <> " & pCod & " AND dsc =" & m_Conn.gFieldDB_GetString(auxDsc)).Rows.Count <> 0 Then
                            auxResult &= "Debe ingresar un título no utilizado."
                        End If
                    End If
                    If auxResult = "" And pOnlyRunChecks = False Then
                        If auxJobQueueLevel = 1 Then
                            gWorkflow_GotoStep_inQueue(pValues)
                        Else
                            gDOCSGN_Delete(pCod, -1, -1, Nothing)
                            Select Case auxWFWMode
                                Case enumWfwMode.coStandard
                                    gDOCLOG_Add(pDsc, pObs, pCod, auxGotoStepToSave, auxHstHidGenCod, False, auxWfwStpCodCurrent, auxWfwStpDscNext, auxsidCod, enumAccessType.coSYSGlobalCambiarestado, auxEmpCod, auxDelEmpCod)
                                    auxSendMails = True
                                Case enumWfwMode.coUserCreate
                                    gDOCLOG_Add(pDsc, pObs, pCod, auxGotoStepToSave, auxHstHidGenCod, True, auxWfwStpCodCurrent, auxWfwStpDscNext, auxsidCod, enumAccessType.coSYSGlobalCambiarestado, auxEmpCod, auxDelEmpCod)
                                    Dim auxDTroles As DataTable = gTRO_Get(pTroCod:=auxTroCod)
                                    gDocPermission_Add(pCod, auxDTroles, auxAclCod, enumRoles.coEditor, enumAccessType.coSYSGlobalCambiarestado, True)
                            End Select
                        End If
                    End If
                Case coWFWSTPDOC_DOCnuevaversion
                    If pOnlyRunChecks = False Then
                        If auxJobQueueLevel = 1 Then
                            gWorkflow_GotoStep_inQueue(pValues)
                        Else
                            Dim auxNewVersion As Integer = m_Conn.gConn_QueryValueInt("SELECT version FROM DOC_DOCVIG WHERE cod=" & pCod, 0) + 1
                            m_Conn.gConn_Update("UPDATE DOC_DOC SET version=" & auxNewVersion & " WHERE cod=" & pCod)
                            gDOCSGN_Delete(pCod, -1, -1, enumWorkflowStep.coWFWSTPDOC_DOCDocumentovigente)
                            Select Case auxWFWMode
                                Case enumWfwMode.coUserCreate
                                    gDOCLOG_Add(pDsc, pObs, pCod, auxGotoStepToSave, auxHstHidGenCod, True, auxWfwStpCodCurrent, auxWfwStpDscNext, auxsidCod, enumAccessType.coSYSGlobalCambiarestado, auxEmpCod, auxDelEmpCod)
                                Case Else
                                    gDOCLOG_Add(pDsc, pObs, pCod, auxGotoStepToSave, auxHstHidGenCod, False, auxWfwStpCodCurrent, auxWfwStpDscNext, auxsidCod, enumAccessType.coSYSGlobalCambiarestado, auxEmpCod, auxDelEmpCod)
                            End Select

                            Select Case auxWFWMode
                                Case enumWfwMode.coStandard
                                    If m_Conn.gField_GetBoolean(auxRowDOCTIP("permedicioncambiaroles"), False) Then
                                        gTRACE_add(pCod, 10, "Existe el permiso de cambiar roles en edición.Saltea el paso")
                                        auxGotoStepNext = coWFWSTPDOC_DOCEdicion
                                        auxOrder = 50
                                    End If
                                Case enumWfwMode.coUserCreate
                                    Dim auxDTroles As DataTable = gTRO_Get(pTroCod:=auxTroCod)
                                    gDocPermission_Add(pCod, auxDTroles, auxAclCod, enumRoles.coEditor, enumAccessType.coSYSGlobalCambiarestado, True)
                            End Select
                        End If
                    End If
                Case coWFWSTPDOC_DOCEdicion, coWFWSTPDOC_DOCSolicitudnuevodocumento, coWFWSTPDOC_DOCSolicitudnuevodocumentoconfirmada
                    'Dim auxDTRoles As DataTable = gDOC_DOCMBR_Get(auxTroCod, pDftGenCod, -1, _
                    '                                             m_Conn.gField_GetBoolean(auxRowDOCTIP("opceditor"), False), _
                    '                                             m_Conn.gField_GetBoolean(auxRowDOCTIP("opcrevisor"), False), _
                    '                                              -1)

                    If pGotoStep = coWFWSTPDOC_DOCSolicitudnuevodocumento Then
                        'auxTroCod = m_Conn.gField_GetInt(gSystem_GetParameterByID(coSysParamIDTRODefault), -1)
                    End If

                    Dim auxDTroles As DataTable = gTRO_Get(pTroCod:=auxTroCod) ' pRol:=enumRoles.coEditor)

                    Dim auxDsc As String = m_Conn.gField_GetString(auxDT.Rows(0)("dsc"), "")
                    If auxDsc = "" Then
                        auxResult &= "El título del documento es obligatorio"
                    ElseIf Val(gSystem_GetParameterByID(coSysParamCheckDuplicateTitle)) > 0 Then
                        If m_Conn.gConn_Query("SELECT cod FROM DOC_DOCVIG WHERE cod > 0 " _
                                             & " AND cod <> " & pCod _
                                             & " AND dsc = " & m_Conn.gFieldDB_GetString(auxDsc) _
                                             ).Rows.Count <> 0 Then
                            auxResult &= "El título ingresado ya se encuentra vigente."
                        End If
                    End If
                    Dim auxNro As Integer = -1
                    Dim auxIdentificador As String = ""
                    If pGotoStep = coWFWSTPDOC_DOCEdicion Or pGotoStep = coWFWSTPDOC_DOCSolicitudnuevodocumentoconfirmada Then
                        If m_Conn.gField_GetInt(auxDT.Rows(0)("siscod")) < 1 Then
                            auxResult &= "Seleccione un sistema."
                        End If
                        If m_Conn.gField_GetInt(auxDT.Rows(0)("procod")) < 1 Then
                            If m_Conn.gField_GetInt(auxDT.Rows(0)("docsupcod")) < 1 Then
                                auxResult &= "Seleccione una dependencia(proceso o documento)."
                            End If
                        End If
                        If m_Conn.gField_GetInt(auxDT.Rows(0)("clacod")) < 1 Then
                            auxResult &= "Seleccione una clasificación."
                        End If

                        If m_Conn.gField_GetInt(auxDT.Rows(0)("undcod")) < 1 Then
                            auxResult &= "Seleccione por lo menos una unidad."
                        End If

                        auxNro = m_Conn.gField_GetInt(auxDT.Rows(0)("nro"), -1)
                        Dim auxDocNroEditEnabled As Boolean = m_Conn.gField_GetBoolean(gSystem_GetParameterByID(coSysParamIDDOCNroEditEnabled), False)
                        If auxDocNroEditEnabled = False Then
                            If auxNro < 1 Then
                                auxNro = m_Conn.gConn_QueryValue("SELECT MAX(nro) FROM DOC_DOC WHERE cod > 0 ", -1) + 1
                            End If
                        End If

                        If auxNro < 1 Then
                            auxResult &= "Ingrese un número de documento mayor o igual a 1"
                        Else
                            'If m_Conn.gConn_Query("SELECT cod FROM DOC_DOC WHERE cod > 0 " _
                            '                  & " AND cod <> " & pCod _
                            '                  & " AND (baja {#ISNULL#} OR baja = {#FALSE#})" _
                            '                  & " AND nro=" & auxNro).Rows.Count <> 0 Then
                            '    auxResult &= "El identificador ya existe, revise los parámetros del documento."
                            'End If
                        End If
                        'Chequea que se encuentren cargados todos los roles
                        If auxDocTipCod < 1 Then
                            auxResult &= "Seleccione un tipo de documento."
                        End If
                        If auxDocTipCod > 0 Then
                            Dim auxRoles(10) As Boolean
                            For Each auxRow As DataRow In auxDTroles.Rows
                                If auxRow("DOC_DOCMBRdsc").ToString <> "" Then
                                    auxRoles(auxRow("rolcod")) = True
                                End If
                            Next
                            If m_Conn.gField_GetBoolean(auxRowDOCTIP("opclector"), False) = False _
                                And auxRoles(enumRoles.coLector) = False Then
                                auxResult &= "Se requiere el rol Lector."
                            End If
                            If m_Conn.gField_GetBoolean(auxRowDOCTIP("opceditor"), False) = False _
                                And auxRoles(enumRoles.coEditor) = False Then
                                auxResult &= "Se requiere el rol Editor."
                            End If
                            If m_Conn.gField_GetBoolean(auxRowDOCTIP("opcrevisor"), False) = False _
                                And auxRoles(enumRoles.coRevisor) = False Then
                                auxResult &= "Se requiere el rol Revisor."
                            End If
                            If m_Conn.gField_GetBoolean(auxRowDOCTIP("opcaprobador"), False) = False _
                            And auxRoles(enumRoles.coAprobador) = False Then
                                auxResult &= "Se requiere el rol Aprobador."
                            End If
                            If m_Conn.gField_GetBoolean(auxRowDOCTIP("opcpublicador"), False) = False _
                            And auxRoles(enumRoles.coPublicador) = False Then
                                auxResult &= "Se requiere el rol Publicador."
                            End If
                            If m_Conn.gField_GetBoolean(auxRowDOCTIP("opccancelador"), False) = False _
                                And auxRoles(enumRoles.coCancelador) = False Then
                                auxResult &= "Se requiere el rol Cancelador."
                            End If
                        End If

                        'No se tiene que repetir para el tipo de documento, (debe controlar que para Proceso no hay otro número igual, dela misma forma se es un instructivo , etc.
                        If auxDT.Rows(0)("version") = 1 Then
                            If m_Conn.gConn_Query("SELECT cod FROM DOC_DOCVIG WHERE cod > 0 " _
                                                    & " AND cod <> " & pCod _
                                                    & " AND doctipcod=" & auxDT.Rows(0)("doctipcod") _
                                                    & " AND nro = " & auxNro).Rows.Count <> 0 Then
                                auxResult &= "El número ingresado ya existe para este tipo de documento."
                            End If
                        End If
                        auxIdentificador = gDoc_GetIdentificador(auxDocTipCod, _
                                                                       auxDT.Rows(0)("undcod"), _
                                                                       auxDT.Rows(0)("apacod"), _
                                                                       auxNro, _
                                                                       auxDT.Rows(0)("version"))
                        If m_Conn.gConn_Query("SELECT cod FROM DOC_DOC WHERE cod > 0 " _
                                                & " AND cod <> " & pCod _
                                                & " AND (baja {#ISNULL#} OR baja = {#FALSE#})" _
                                                & " AND identificador=" & m_Conn.gFieldDB_GetString(auxIdentificador)).Rows.Count <> 0 Then
                            auxResult &= "El identificador ya existe, revise los parámetros del documento(" & auxIdentificador & ")"
                        End If
                    End If



                    Select Case auxWfwStpCodCurrent
                        Case coWFWSTPDOC_DOCRevisionrechazada, coWFWSTPDOC_DOCAprobacionrechazada
                            auxResult = ""
                    End Select
                    If auxResult = "" And pOnlyRunChecks = False Then
                        If auxJobQueueLevel = 1 Then
                            gWorkflow_GotoStep_inQueue(pValues)
                        Else
                            If pGotoStep = coWFWSTPDOC_DOCSolicitudnuevodocumentoconfirmada Then
                                Dim auxAllEditoresRequired As Boolean = False
                                m_Security.gACL_DelLogin(auxAclCod, m_Security.CurrentSecCod, enumAccessType.coSYSGlobalCambiarestado)
                                auxHstHidGenCod = gEntity_DOC_DOC_NewVersion(-1, pCod, "v" & auxDT.Rows(0)("version") & " - Edición - " & m_Security.CurrentSecDsc)
                                If auxEmpCod > 0 Then
                                    gDOCSGN_Delete(pCod, enumWorkflowStep.coWFWSTPDOC_DOCSolicitudnuevodocumento, auxEmpCod, Nothing)
                                End If
                                gDOCLOG_Add(pDsc, pObs, pCod, auxGotoStepToSave, auxHstHidGenCod, False, auxWfwStpCodCurrent, auxWfwStpDscNext, auxsidCod, enumAccessType.coSYSGlobalCambiarestado, auxEmpCod, auxDelEmpCod)
                                Dim auxChanges As String = ""
                                'Si no tenía numero, agrega el autocalculado
                                If m_Conn.gField_GetInt(auxDT.Rows(0)("nro"), -1) < 1 And auxNro > 0 Then
                                    auxChanges &= ",nro=" & auxNro
                                End If
                                auxChanges &= ",identificador=" & m_Conn.gFieldDB_GetString(auxIdentificador)
                                m_Conn.gConn_Update("UPDATE DOC_DOC " _
                                                    & " SET prncfgcod=" & Val(gSystem_GetParameterByID(coSysParamIDPrnCfgCodDefault)) _
                                                    & auxChanges _
                                                    & " WHERE cod=" & pCod)
                                auxSendMails = True

                                If auxAllEditoresRequired Then
                                    If auxForce = False And m_Conn.gConn_Query("SELECT cod FROM DOC_DOCSGN WHERE doccod=" & pCod _
                                                    & " AND doclogcod IN (SELECT cod FROM DOC_DOCLOG WHERE doccod=" & pCod & " AND wfwstepnext=" & enumWorkflowStep.coWFWSTPDOC_DOCEdicion & ")").Rows.Count <> 0 Then
                                        auxGotoStepCancel = True
                                    Else
                                        auxGotoStepNext = coWFWSTPDOC_DOCRevisionSSG
                                        auxOrder = 50
                                    End If
                                Else
                                    gTRACE_add(pCod, 10, "No se requieren todos los editores")
                                    auxGotoStepNext = coWFWSTPDOC_DOCRevisionSSG
                                    auxOrder = 50
                                End If

                            Else
                                If pGotoStep = coWFWSTPDOC_DOCEdicion Then
                                    'gEntity_DOC_DOC_SystemUpdate(pcod:=pCod, pwfwmode:=enumWfwMode.coStandard)
                                    gTRO_ResolveThisRol(auxDTroles, enumRoles.coEditor, _
                                                        m_Conn.gField_GetBoolean(auxRowDOCTIP("opceditor"), False), coGroupDocumentadorEditores)
                                    gDocPermission_Add(pCod, auxDTroles, auxAclCod, enumRoles.coEditor, enumAccessType.coSYSGlobalCambiarestado, True)
                                    gDOCSGN_Delete(pCod, -1, -1, enumWorkflowStep.coWFWSTPDOC_DOCDocumentovigente)
                                    If gDOCLOG_Add(pDsc, pObs, pCod, auxGotoStepToSave, auxHstHidGenCod, True, auxWfwStpCodCurrent, auxWfwStpDscNext, auxsidCod, enumAccessType.coSYSGlobalCambiarestado, auxEmpCod, auxDelEmpCod) = False Then
                                        auxGotoStepCancel = True
                                        auxResult &= "No se encuentran colaboradores para el cambio de estado (Consulte a su administrador)."
                                    Else
                                        'm_Security.gACL_DelEntriesByAccessType(auxAclCod, enumAccessType.coSYSImprimircopiasnocontroladas)
                                        'gDocPermission_Add(pCod,auxDTRoles, auxAclCod, enumRoles.coEditor, enumAccessType.coSYSImprimircopiasnocontroladas, True)
                                        auxOrder = 50
                                        Dim auxChanges As String = ""
                                        If auxWfwStpCodCurrent = coWFWSTPDOC_DOCCreacion Or auxWfwStpCodCurrent = -1 Then
                                            Dim auxtemplate As String = m_Conn.gField_GetString(auxRowDOCTIP("templatebody")) ' m_Conn.gConn_QueryValueString("SELECT templatebod2y FROM DOC_DOCTIP where cod=" & auxDT.Rows(0)("doctipcod"))
                                            auxChanges &= ",cuerpo=(CASE WHEN LEN(ISNULL(cuerpo,'')) = 0 THEN '" & auxtemplate & "' ELSE cuerpo END) "
                                        End If
                                        'Si no tenía numero, agrega el autocalculado
                                        If m_Conn.gField_GetInt(auxDT.Rows(0)("nro"), -1) < 1 And auxNro > 0 Then
                                            auxChanges &= ",nro=" & auxNro
                                        End If
                                        auxChanges &= ",identificador=" & m_Conn.gFieldDB_GetString(auxIdentificador)

                                        m_Conn.gConn_Update("UPDATE DOC_DOC " _
                                                            & " SET prncfgcod=" & Val(gSystem_GetParameterByID(coSysParamIDPrnCfgCodDefault)) _
                                                            & auxChanges _
                                                            & " WHERE cod=" & pCod)
                                        auxSendMails = True
                                    End If
                                ElseIf pGotoStep = coWFWSTPDOC_DOCSolicitudnuevodocumento Then
                                    'gDOCSGN_Delete(pCod, -1, -1, enumWorkflowStep.coWFWSTPDOC_DOCSolicitudnuevodocumento)
                                    gTRO_ResolveThisRol(auxDTroles, enumRoles.coReceptor, _
                                                        True, coGroupIDDocumentadorReceptores)
                                    gDocPermission_Add(pCod, auxDTroles, auxAclCod, enumRoles.coReceptor, enumAccessType.coSYSGlobalCambiarestado, True)
                                    If gDOCLOG_Add(pDsc, pObs, pCod, auxGotoStepToSave, auxHstHidGenCod, True, auxWfwStpCodCurrent, auxWfwStpDscNext, auxsidCod, enumAccessType.coSYSGlobalCambiarestado, auxEmpCod, auxDelEmpCod) = False Then
                                        auxGotoStepCancel = True
                                        auxResult &= "No se encuentran colaboradores para el cambio de estado (Consulte a su administrador)."
                                    Else
                                        auxSendMails = True
                                    End If

                                End If
                            End If
                        End If
                    End If


                Case coWFWSTPDOC_DOCEdicionOK
                        If auxWfwStpCodCurrent = enumWorkflowStep.coWFWSTPDOC_DOCSolicitudnuevodocumentoconfirmada Then
                            'Si el paso anterior fue la confirmación de documento, no debe chequear la firma
                        Else
                            If auxForce = False And m_Conn.gConn_Query("SELECT cod FROM DOC_DOCSGN " _
                                                       & " WHERE doccod=" & pCod _
                                                       & " AND empcod=" & auxEmpCod _
                                                       & " AND doclogcod IN (SELECT TOP 1 cod FROM DOC_DOCLOG WHERE doccod=" & pCod _
                                                       & " AND wfwstepnext=" & enumWorkflowStep.coWFWSTPDOC_DOCEdicion & " ORDER BY cod DESC)").Rows.Count = 0 Then
                                auxResult &= "No posee firma pendiente."
                            End If

                        End If
                        If auxResult = "" And pOnlyRunChecks = False Then
                            If auxJobQueueLevel = 1 Then
                                gWorkflow_GotoStep_inQueue(pValues)
                            Else
                                'Es forzado o bien debe firmar
                                Dim auxAllEditoresRequired As Boolean = False
                                m_Security.gACL_DelLogin(auxAclCod, m_Security.CurrentSecCod, enumAccessType.coSYSGlobalCambiarestado)
                                auxHstHidGenCod = gEntity_DOC_DOC_NewVersion(-1, pCod, "v" & auxDT.Rows(0)("version") & " - Edición - " & m_Security.CurrentSecDsc)
                                If auxEmpCod > 0 Then
                                    gDOCSGN_Delete(pCod, enumWorkflowStep.coWFWSTPDOC_DOCEdicion, auxEmpCod, Nothing)
                                    auxAllEditoresRequired = m_Conn.gField_GetBoolean(gSystem_GetParameterByID(coSysParamIDDOCAllEditoresRequired), False)
                                End If
                            gDOCLOG_Add(pDsc, pObs, pCod, auxGotoStepToSave, auxHstHidGenCod, False, auxWfwStpCodCurrent, auxWfwStpDscNext, auxsidCod, enumAccessType.coSYSGlobalCambiarestado, auxEmpCod, auxDelEmpCod)
                                If auxAllEditoresRequired Then
                                    If auxForce = False And m_Conn.gConn_Query("SELECT cod FROM DOC_DOCSGN WHERE doccod=" & pCod _
                                                    & " AND doclogcod IN (SELECT cod FROM DOC_DOCLOG WHERE doccod=" & pCod & " AND wfwstepnext=" & enumWorkflowStep.coWFWSTPDOC_DOCEdicion & ")").Rows.Count <> 0 Then
                                        auxGotoStepCancel = True
                                    Else
                                        auxGotoStepNext = coWFWSTPDOC_DOCRevisionSSG
                                        auxOrder = 50
                                    End If
                                Else
                                    gTRACE_add(pCod, 10, "No se requieren todos los editores")
                                    auxGotoStepNext = coWFWSTPDOC_DOCRevisionSSG
                                    auxOrder = 50
                                End If
                            End If
                        End If

                Case coWFWSTPDOC_DOCRevisionSSG
                    If pOnlyRunChecks = False Then
                        If auxJobQueueLevel = 1 Then
                            gWorkflow_GotoStep_inQueue(pValues)
                        Else
                            Dim auxIdentificador As String = gDoc_GetIdentificador(auxDocTipCod, _
                                                                                        auxDT.Rows(0)("undcod"), _
                                                                                        auxDT.Rows(0)("apacod"), _
                                                                                        auxDT.Rows(0)("nro"), _
                                                                                        auxDT.Rows(0)("version"))
                            gEntity_DOC_DOC_SystemUpdate(pcod:=pCod, pidentificador:=auxIdentificador)
                            'Dim auxDTRoles As DataTable = gDOC_DOCMBR_Get(auxTroCod, pDftGenCod, -1, _
                            '                                    m_Conn.gField_GetBoolean(auxRowDOCTIP("opceditor"), False), _
                            '                                    m_Conn.gField_GetBoolean(auxRowDOCTIP("opcrevisor"), False), _
                            '                                    enumRoles.coRevisor)
                            Dim auxDTroles As DataTable = gTRO_Get(pTroCod:=auxTroCod)

                            gTRO_ResolveThisRol(auxDTroles, enumRoles.coRevisor, _
                                                m_Conn.gField_GetBoolean(auxRowDOCTIP("opcrevisor"), False), coGroupDocumentadorEditores)

                            'If m_Conn.gField_GetBoolean(auxRowDOCTIP("opcrevisor"), False) Then
                            '    If auxDTroles.Select("rolcod=" & enumRoles.coRevisor).Count = 0 Then
                            '        'No hay editores y el rol EDICION es opcional-> utiliza DOCUMENTADOREDITORES
                            '        Dim auxEditoresGrpCod As Integer = m_Security.gGroup_GetCodByID(coGroupDocumentadorEditores)
                            '        gTRACE_add(pCod, 1, "No hay editores cargados y la opción de revisor opcional esta habilitada.Se intenta cursar a EDITORES. GrpCod=" & auxEditoresGrpCod)
                            '        gDTRoles_Add(auxDTroles, enumRoles.coRevisor, enumEntities.coEntityDOC_EQU, auxEditoresGrpCod, -1)
                            '    End If
                            'End If
                            gDocPermission_Add(pCod, auxDTroles, auxAclCod, enumRoles.coRevisor, enumAccessType.coSYSGlobalCambiarestado, True)
                            gDOCSGN_Delete(pCod, -1, -1, enumWorkflowStep.coWFWSTPDOC_DOCDocumentovigente)
                            If gDOCLOG_Add(pDsc, pObs, pCod, auxGotoStepToSave, auxHstHidGenCod, True, auxWfwStpCodCurrent, auxWfwStpDscNext, auxsidCod, enumAccessType.coSYSGlobalCambiarestado, auxEmpCod, auxDelEmpCod) = False Then
                                If m_Conn.gField_GetBoolean(auxRowDOCTIP("opcrevisor"), False) = False Then
                                    auxGotoStepCancel = True
                                    auxResult &= "No se encuentran colaboradores para el cambio de estado (Consulte a su administrador)."
                                Else
                                    gTRACE_add(pCod, 10, "Revisor opcional y no se encontraron revisores. Pasa a aprobación")
                                    auxGotoStepNext = coWFWSTPDOC_DOCAprobacion
                                End If
                            Else
                                auxSendMails = True
                            End If
                        End If
                    End If


                Case coWFWSTPDOC_DOCrevisionOK
                        If auxForce = False And m_Conn.gConn_Query("SELECT cod FROM DOC_DOCSGN " _
                                                      & " WHERE doccod=" & pCod _
                                                      & " AND empcod=" & auxEmpCod _
                                                      & " AND doclogcod IN (SELECT TOP 1 cod FROM DOC_DOCLOG WHERE doccod=" & pCod _
                                                      & " AND wfwstepnext=" & enumWorkflowStep.coWFWSTPDOC_DOCRevisionSSG & " ORDER BY cod DESC)").Rows.Count = 0 Then
                            auxResult &= "No posee firma pendiente."
                        End If
                        If auxResult = "" And pOnlyRunChecks = False Then
                            If auxJobQueueLevel = 1 Then
                                gWorkflow_GotoStep_inQueue(pValues)
                            Else
                                m_Security.gACL_DelLogin(auxAclCod, m_Security.CurrentSecCod, enumAccessType.coSYSGlobalCambiarestado)
                                If auxEmpCod > 0 Then
                                    gDOCSGN_Delete(pCod, enumWorkflowStep.coWFWSTPDOC_DOCRevisionSSG, auxEmpCod, Nothing)
                                End If
                            gDOCLOG_Add(pDsc, pObs, pCod, auxGotoStepToSave, auxHstHidGenCod, False, auxWfwStpCodCurrent, auxWfwStpDscNext, auxsidCod, enumAccessType.coSYSGlobalCambiarestado, auxEmpCod, auxDelEmpCod)
                                Dim auxAllRevisorRequired As Boolean = m_Conn.gField_GetBoolean(gSystem_GetParameterByID(coSysParamIDDOCAllRevisorRequired), False)
                                If auxAllRevisorRequired Then
                                    If auxForce = False And m_Conn.gConn_Query("SELECT cod FROM DOC_DOCSGN WHERE doccod=" & pCod _
                                                            & " AND doclogcod IN (SELECT cod FROM DOC_DOCLOG WHERE doccod=" & pCod & " AND wfwstepnext=" & enumWorkflowStep.coWFWSTPDOC_DOCRevisionSSG & ")").Rows.Count <> 0 Then
                                        auxGotoStepCancel = True
                                    Else
                                        auxGotoStepNext = coWFWSTPDOC_DOCAprobacion
                                        auxOrder = 50
                                    End If
                                Else
                                    gTRACE_add(pCod, 10, "No se requieren todos los revisores")
                                    auxGotoStepNext = coWFWSTPDOC_DOCAprobacion
                                    auxOrder = 50
                                End If
                            End If
                        End If
                Case coWFWSTPDOC_DOCAprobacion
                    If pOnlyRunChecks = False Then
                        If auxJobQueueLevel = 1 Then
                            gWorkflow_GotoStep_inQueue(pValues)
                        Else
                            Dim auxDTroles As DataTable = gTRO_Get(pTroCod:=auxTroCod)
                            gDocPermission_Add(pCod, auxDTroles, auxAclCod, enumRoles.coAprobador, enumAccessType.coSYSGlobalCambiarestado, True)
                            gDOCSGN_Delete(pCod, -1, -1, enumWorkflowStep.coWFWSTPDOC_DOCDocumentovigente)
                            If gDOCLOG_Add(pDsc, pObs, pCod, auxGotoStepToSave, auxHstHidGenCod, True, auxWfwStpCodCurrent, auxWfwStpDscNext, auxsidCod, enumAccessType.coSYSGlobalCambiarestado, auxEmpCod, auxDelEmpCod) = False Then
                                If m_Conn.gField_GetBoolean(auxRowDOCTIP("opcaprobador"), False) = False Then
                                    auxGotoStepCancel = True
                                    auxResult &= "No se encuentran colaboradores para el cambio de estado (Consulte a su administrador)."
                                Else
                                    gTRACE_add(pCod, 10, "Aprobador opcional y no se encontraron aprobadores. Pasa a publicación")
                                    auxGotoStepNext = coWFWSTPDOC_DOCPublicacion
                                End If
                            Else
                                auxSendMails = True
                            End If
                        End If
                    End If

                Case coWFWSTPDOC_DOCaprobacionOK
                        If auxForce = False And m_Conn.gConn_Query("SELECT cod FROM DOC_DOCSGN " _
                                                      & " WHERE doccod=" & pCod _
                                                      & " AND empcod=" & auxEmpCod _
                                                      & " AND doclogcod IN (SELECT TOP 1 cod FROM DOC_DOCLOG WHERE doccod=" & pCod _
                                                      & " AND wfwstepnext=" & enumWorkflowStep.coWFWSTPDOC_DOCAprobacion & " ORDER BY cod DESC)").Rows.Count = 0 Then
                            auxResult &= "No posee firma pendiente."
                        End If
                        If auxResult = "" And pOnlyRunChecks = False Then
                            If auxJobQueueLevel = 1 Then
                                gWorkflow_GotoStep_inQueue(pValues)
                            Else
                                m_Security.gACL_DelLogin(auxAclCod, m_Security.CurrentSecCod, enumAccessType.coSYSGlobalCambiarestado)
                                If auxEmpCod > 0 Then
                                    gDOCSGN_Delete(pCod, enumWorkflowStep.coWFWSTPDOC_DOCAprobacion, auxEmpCod, Nothing)
                                End If
                            gDOCLOG_Add(pDsc, pObs, pCod, auxGotoStepToSave, auxHstHidGenCod, False, auxWfwStpCodCurrent, auxWfwStpDscNext, auxsidCod, enumAccessType.coSYSGlobalCambiarestado, auxEmpCod, auxDelEmpCod)
                                Dim auxAllAprobadoresRequired As Boolean = m_Conn.gField_GetBoolean(gSystem_GetParameterByID(coSysParamIDDOCAllAprobadorRequired), False)
                                If auxAllAprobadoresRequired Then
                                    If auxForce = False And m_Conn.gConn_Query("SELECT cod FROM DOC_DOCSGN WHERE doccod=" & pCod _
                                                & " AND doclogcod IN (SELECT cod FROM DOC_DOCLOG WHERE doccod=" & pCod & " AND wfwstepnext=" & enumWorkflowStep.coWFWSTPDOC_DOCAprobacion & ")").Rows.Count <> 0 Then
                                        auxGotoStepCancel = True
                                    End If
                                Else
                                    gTRACE_add(pCod, 10, "No se requiren todos los aprobadores")
                                End If
                                If auxGotoStepCancel = False Then
                                    auxOrder = 50
                                    auxGotoStepNext = coWFWSTPDOC_DOCPublicacion
                                End If
                            End If
                        End If

                Case coWFWSTPDOC_DOCPublicacion
                    If pOnlyRunChecks = False Then
                        If auxJobQueueLevel = 1 Then
                            gWorkflow_GotoStep_inQueue(pValues)
                        Else
                            Dim auxDTroles As DataTable = gTRO_Get(pTroCod:=auxTroCod)
                            gDocPermission_Add(pCod, auxDTroles, auxAclCod, enumRoles.coPublicador, enumAccessType.coSYSGlobalCambiarestado, True)
                            gDOCSGN_Delete(pCod, -1, -1, enumWorkflowStep.coWFWSTPDOC_DOCDocumentovigente)
                            If gDOCLOG_Add(pDsc, pObs, pCod, auxGotoStepToSave, auxHstHidGenCod, True, auxWfwStpCodCurrent, auxWfwStpDscNext, auxsidCod, enumAccessType.coSYSGlobalCambiarestado, auxEmpCod, auxDelEmpCod) = False Then
                                If m_Conn.gField_GetBoolean(auxRowDOCTIP("opcpublicador"), False) = False Then
                                    auxGotoStepCancel = True
                                    auxResult &= "No se encuentran colaboradores para el cambio de estado (Consulte a su administrador)."
                                Else
                                    gTRACE_add(pCod, 10, "Publicador opcional y no se encontraron publicadores. Pasa a vigente")
                                    auxGotoStepNext = coWFWSTPDOC_DOCDocumentovigente
                                End If
                            Else
                                auxSendMails = True
                            End If
                        End If
                    End If

                Case coWFWSTPDOC_DOCpublicacionOK
                    If auxForce = False And m_Conn.gConn_Query("SELECT cod FROM DOC_DOCSGN " _
                                                    & " WHERE doccod=" & pCod _
                                                    & " AND empcod=" & auxEmpCod _
                                                    & " AND doclogcod IN (SELECT TOP 1 cod FROM DOC_DOCLOG WHERE doccod=" & pCod _
                                                    & " AND wfwstepnext=" & enumWorkflowStep.coWFWSTPDOC_DOCPublicacion & " ORDER BY cod DESC)").Rows.Count = 0 Then
                        auxResult &= "No posee firma pendiente."
                    End If
                    If auxResult = "" And pOnlyRunChecks = False Then
                        If auxJobQueueLevel = 1 Then
                            gWorkflow_GotoStep_inQueue(pValues)
                        Else
                            m_Security.gACL_DelLogin(auxAclCod, m_Security.CurrentSecCod, enumAccessType.coSYSGlobalCambiarestado)
                            If auxEmpCod > 0 Then
                                gDOCSGN_Delete(pCod, enumWorkflowStep.coWFWSTPDOC_DOCPublicacion, auxEmpCod, Nothing)
                            End If
                            'lIMPIA la fecha de publicacion automatica
                            gEntity_DOC_DOC_SystemUpdate(pcod:=pCod, ppublicacionauto:="NULL")
                            gDOCLOG_Add(pDsc, pObs, pCod, auxGotoStepToSave, auxHstHidGenCod, False, auxWfwStpCodCurrent, auxWfwStpDscNext, auxsidCod, enumAccessType.coSYSGlobalCambiarestado, auxEmpCod, auxDelEmpCod)
                            Dim auxAllPublicadoresRequired As Boolean = m_Conn.gField_GetBoolean(gSystem_GetParameterByID(coSysParamIDDOCAllPublicadorRequired), False)
                            If auxAllPublicadoresRequired Then
                                If auxForce = False And m_Conn.gConn_Query("SELECT cod FROM DOC_DOCSGN WHERE doccod=" & pCod _
                                            & " AND doclogcod IN (SELECT cod FROM DOC_DOCLOG WHERE doccod=" & pCod & " AND wfwstepnext=" & enumWorkflowStep.coWFWSTPDOC_DOCPublicacion & ")").Rows.Count <> 0 Then
                                    auxGotoStepCancel = True
                                End If
                            Else
                                gTRACE_add(pCod, 10, "No se requiren todos los publicadores")
                            End If
                            If auxGotoStepCancel = False Then
                                auxOrder = 50
                                auxGotoStepNext = coWFWSTPDOC_DOCDocumentovigente
                            End If
                        End If
                    End If

                Case coWFWSTPDOC_DOCReidentificacion
                    If auxWfwStpCodCurrent <> coWFWSTPDOC_DOCDocumentovigente Then
                        auxResult &= "El documento debe estar vigente."
                    End If
                    If auxResult = "" Then
                        If pOnlyRunChecks = False Then
                            If auxJobQueueLevel = 1 Then
                                gWorkflow_GotoStep_inQueue(pValues)
                            Else
                                Dim auxIdentificador As String = gDoc_GetIdentificador(auxDocTipCod, _
                                                                                        auxDT.Rows(0)("undcod"), _
                                                                                        auxDT.Rows(0)("apacod"), _
                                                                                        auxDT.Rows(0)("nro"), _
                                                                                        auxDT.Rows(0)("version"))
                                If auxIdentificador = auxDT.Rows(0)("identificador") Then
                                    auxGotoStepCancel = True
                                Else
                                    Dim auxNewVersion As Integer = m_Conn.gConn_QueryValueInt("SELECT version FROM DOC_DOCVIG WHERE cod=" & pCod, 0) + 1
                                    m_Conn.gConn_Update("UPDATE DOC_DOC SET version=" & auxNewVersion & " WHERE cod=" & pCod)
                                    gEntity_DOC_DOC_Update(pcod:=pCod, pwfwmode:=enumWfwMode.coReID, pidentificador:=auxIdentificador, pversion:=auxNewVersion)
                                    pObs = auxDT.Rows(0)("identificador") & "->" & auxIdentificador
                                    pValues.gValue_Add("obs", "Se ha reidentificado el documento:anterior[" & auxDT.Rows(0)("identificador") & "] -> Nuevo[" & auxIdentificador & "]")
                                    gDOCLOG_Add(pDsc, pObs, pCod, auxGotoStepToSave, auxHstHidGenCod, False, auxWfwStpCodCurrent, auxWfwStpDscNext, auxsidCod, enumAccessType.coSYSGlobalCambiarestado, auxEmpCod, auxDelEmpCod)
                                    auxGotoStepNext = coWFWSTPDOC_DOCDocumentovigente
                                End If
                            End If
                        End If
                    End If

                Case coWFWSTPDOC_DOCLecturaOK
                        If pOnlyRunChecks = False Then
                            If auxJobQueueLevel = 1 Then
                                gWorkflow_GotoStep_inQueue(pValues)
                            Else
                                Dim auxLecturaPendiente As Integer = Val(m_Conn.gConn_QueryValueString("SELECT COUNT(*) FROM DOC_DOCSGN " _
                                  & " LEFT JOIN DOC_DOCLOG ON DOC_DOCSGN.doclogcod = DOC_DOCLOG.cod " _
                                  & "  WHERE DOC_DOCLOG.wfwstepnext=" & enumWorkflowStep.coWFWSTPDOC_DOCDocumentovigente _
                                  & " AND DOC_DOCSGN.empcod=" & auxEmpCod _
                                  & " AND DOC_DOCSGN.doccod=" & pCod))
                                If auxLecturaPendiente = 0 Then
                                    gTRACE_add(pCod, 10, "El colaborador ya ha confirmado la lectura(" & auxEmpCod & ")")
                                Else
                                    m_Security.gACL_DelLogin(auxAclCod, m_Security.CurrentSecCod, enumAccessType.coSYSConfirmarlectura)
                                    If auxEmpCod > 0 Then
                                        gDOCSGN_Delete(pCod, enumWorkflowStep.coWFWSTPDOC_DOCDocumentovigente, auxEmpCod, Nothing)
                                    End If
                                gDOCLOG_Add(pDsc, pObs, pCod, auxGotoStepToSave, auxHstHidGenCod, False, auxWfwStpCodCurrent, auxWfwStpDscNext, auxsidCod, enumAccessType.coSYSGlobalCambiarestado, auxEmpCod, auxDelEmpCod)

                                    If m_Conn.gConn_Query("SELECT cod FROM DOC_DOCSGN WHERE doccod=" & pCod _
                                                        & " AND doclogcod IN (SELECT cod FROM DOC_DOCLOG WHERE doccod=" & pCod & " AND wfwstepnext=" & enumWorkflowStep.coWFWSTPDOC_DOCDocumentovigente & ")").Rows.Count = 0 Then
                                        gEntity_DOC_DOC_SystemUpdate(pcod:=pCod, porden:=50)
                                    End If

                                End If
                                auxGotoStepCancel = True
                            End If
                        End If
                Case coWFWSTPDOC_DOCSolnuevaversionOK
                        If pOnlyRunChecks = False Then
                            If auxJobQueueLevel = 1 Then
                                gWorkflow_GotoStep_inQueue(pValues)
                            Else
                                m_Security.gACL_DelLogin(auxAclCod, m_Security.CurrentSecCod, enumAccessType.coSYSGlobalCambiarestado)
                            gDOCLOG_Add(pDsc, pObs, pCod, auxGotoStepToSave, auxHstHidGenCod, False, auxWfwStpCodCurrent, auxWfwStpDscNext, auxsidCod, enumAccessType.coSYSGlobalCambiarestado, auxEmpCod, auxDelEmpCod)
                                auxGotoStepNext = coWFWSTPDOC_DOCRevisionSSG
                            End If
                        End If


                Case coWFWSTPDOC_DOCnuevaversionNOK
                        If pOnlyRunChecks = False Then
                            If auxJobQueueLevel = 1 Then
                                gWorkflow_GotoStep_inQueue(pValues)
                            Else
                            m_Security.gACL_DelLogin(auxAclCod, m_Security.CurrentSecCod, enumAccessType.coSYSGlobalCambiarestado)
                            'gDOCSGN_Delete(pCod, enumWorkflowStep.coWFWSTPDOC_DOCnuevaversion, auxEmpCod, Nothing)
                            Dim auxOldVersion As Integer = m_Conn.gConn_QueryValueInt("SELECT version FROM DOC_DOCVIG WHERE cod=" & pCod, 0)
                            If auxOldVersion > 0 Then
                                m_Conn.gConn_Update("UPDATE DOC_DOC SET version=" & auxOldVersion & " WHERE cod=" & pCod)
                            End If
                            gDOCLOG_Add(pDsc, pObs, pCod, auxGotoStepToSave, auxHstHidGenCod, False, auxWfwStpCodCurrent, auxWfwStpDscNext, auxsidCod, enumAccessType.coSYSGlobalCambiarestado, auxEmpCod, auxDelEmpCod)
                           
                             Select auxWFWMode
                                Case enumWfwMode.coStandard
                                    auxGotoStepNext = m_Conn.gConn_QueryValue("SELECT TOP 1 wfwstepnext " _
                                           & " FROM DOC_DOCLOG WHERE doccod=" & pCod _
                                           & " AND wfwstepnext =" & coWFWSTPDOC_DOCDocumentovigente _
                                           & " ORDER BY cod DESC", coWFWSTPDOC_DOCCreacion)
                                Case enumWfwMode.coUserCreate
                                    auxGotoStepNext = coWFWSTPDOC_DOCnuevaversion
                            End Select
                            End If
                        End If


                Case coWFWSTPDOC_DOCRevisionrechazada
                    If pOnlyRunChecks = False Then
                        If auxJobQueueLevel = 1 Then
                            gWorkflow_GotoStep_inQueue(pValues)
                        Else
                            gDOCSGN_Delete(pCod, enumWorkflowStep.coWFWSTPDOC_DOCRevisionSSG, -1, -1)
                            m_Security.gACL_DelLogin(auxAclCod, m_Security.CurrentSecCod, enumAccessType.coSYSGlobalCambiarestado)
                            gDOCLOG_Add(pDsc, pObs, pCod, auxGotoStepToSave, auxHstHidGenCod, False, auxWfwStpCodCurrent, auxWfwStpDscNext, auxsidCod, enumAccessType.coSYSGlobalCambiarestado, auxEmpCod, auxDelEmpCod)
                            auxGotoStepNext = enumWorkflowStep.coWFWSTPDOC_DOCEdicion
                            'auxGotoStepNext = m_Conn.gConn_QueryValue("SELECT TOP 1 wfwstepnext " _
                            '         & " FROM DOC_DOCLOG WHERE doccod=" & pCod _
                            '         & " AND wfwstepnext IN (" & enumWorkflowStep.coWFWSTPDOC_DOCCreacion _
                            '                     & "," & enumWorkflowStep.coWFWSTPDOC_DOCnuevaversion _
                            '                     & "," & enumWorkflowStep.coWFWSTPDOC_DOCEdicion _
                            '                     & ")" _
                            '         & " ORDER BY cod DESC", -1)
                            'If auxGotoStepNext = -1 Then
                            '    If auxWFWMode = enumWfwMode.coStandard Then
                            '        auxGotoStepNext = enumWorkflowStep.coWFWSTPDOC_DOCEdicion
                            '    Else
                            '         auxGotoStepNext = enumWorkflowStep.coWFWSTPDOC_DOCCreacion
                            '    End If
                            'End If
                        End If
                    End If

                Case coWFWSTPDOC_DOCAprobacionrechazada
                        If pOnlyRunChecks = False Then
                            If auxJobQueueLevel = 1 Then
                                gWorkflow_GotoStep_inQueue(pValues)
                            Else
                                gDOCSGN_Delete(pCod, enumWorkflowStep.coWFWSTPDOC_DOCAprobacion, -1, -1)
                                m_Security.gACL_DelLogin(auxAclCod, m_Security.CurrentSecCod, enumAccessType.coSYSGlobalCambiarestado)
                            gDOCLOG_Add(pDsc, pObs, pCod, auxGotoStepToSave, auxHstHidGenCod, False, auxWfwStpCodCurrent, auxWfwStpDscNext, auxsidCod, enumAccessType.coSYSGlobalCambiarestado, auxEmpCod, auxDelEmpCod)
                                auxGotoStepNext = coWFWSTPDOC_DOCEdicion
                            End If
                        End If

                Case coWFWSTPDOC_DOCCancelacion
                    If auxResult = "" And pOnlyRunChecks = False Then
                        If auxJobQueueLevel = 1 Then
                            gWorkflow_GotoStep_inQueue(pValues)
                        Else
                            Dim auxDTroles As DataTable = gTRO_Get(pTroCod:=auxTroCod)
                            If m_Conn.gField_GetBoolean(auxRowDOCTIP("opccancelador"), False) = False Then
                                gTRO_ResolveThisRol(auxDTroles, enumRoles.coReceptor, _
                                               True, coGroupIDDocumentadorReceptores)
                            End If
                            'Analizar canceladores
                            gDocPermission_Add(pCod, auxDTroles, auxAclCod, enumRoles.coCancelador, enumAccessType.coSYSGlobalCambiarestado, True)
                            gDOCSGN_Delete(pCod, -1, -1, enumWorkflowStep.coWFWSTPDOC_DOCDocumentovigente)
                            If gDOCLOG_Add(pDsc, pObs, pCod, auxGotoStepToSave, auxHstHidGenCod, True, auxWfwStpCodCurrent, auxWfwStpDscNext, auxsidCod, enumAccessType.coSYSGlobalCambiarestado, auxEmpCod, auxDelEmpCod) = False Then
                                auxGotoStepCancel = True
                                auxResult &= "No se encuentran colaboradores para el cambio de estado (Consulte a su administrador)."
                            Else
                                auxSendMails = True
                                auxOrder = 50
                            End If

                        End If
                    End If


                Case coWFWSTPDOC_DOCRechazarcancelacion
                        If pOnlyRunChecks = False Then
                            If auxJobQueueLevel = 1 Then
                                gWorkflow_GotoStep_inQueue(pValues)
                            Else
                                gDOCSGN_Delete(pCod, enumWorkflowStep.coWFWSTPDOC_DOCCancelacion, -1, -1)
                                m_Security.gACL_DelLogin(auxAclCod, m_Security.CurrentSecCod, enumAccessType.coSYSGlobalCambiarestado)
                            gDOCLOG_Add(pDsc, pObs, pCod, auxGotoStepToSave, auxHstHidGenCod, False, auxWfwStpCodCurrent, auxWfwStpDscNext, auxsidCod, enumAccessType.coSYSGlobalCambiarestado, auxEmpCod, auxDelEmpCod)
                                If auxWFWMode = enumWfwMode.coStandard Then
                                    auxGotoStepNext = m_Conn.gConn_QueryValue("SELECT TOP 1 wfwstepnext " _
                                            & " FROM DOC_DOCLOG WHERE doccod=" & pCod _
                                            & " AND wfwstepnext IN (" & enumWorkflowStep.coWFWSTPDOC_DOCEdicion & "," & enumWorkflowStep.coWFWSTPDOC_DOCRevisionSSG & "," & enumWorkflowStep.coWFWSTPDOC_DOCAprobacion & ")" _
                                            & " ORDER BY cod DESC")
                                Else
                                auxGotoStepNext = m_Conn.gConn_QueryValue("SELECT TOP 1 wfwstepnext " _
                                        & " FROM DOC_DOCLOG WHERE doccod=" & pCod _
                                        & " AND wfwstepnext IN (" & enumWorkflowStep.coWFWSTPDOC_DOCCreacion & "," & enumWorkflowStep.coWFWSTPDOC_DOCEdicion & "," & enumWorkflowStep.coWFWSTPDOC_DOCRevisionSSG & "," & enumWorkflowStep.coWFWSTPDOC_DOCAprobacion & ")" _
                                        & " ORDER BY cod DESC", enumWorkflowStep.coWFWSTPDOC_DOCCreacion)
                                End If
                            End If
                        End If

                Case coWFWSTPDOC_DOCSolicitudnuevodocumentorechazada
                    'solicitud aprobación
                    If auxResult = "" And pOnlyRunChecks = False Then
                        If auxJobQueueLevel = 1 Then
                            gWorkflow_GotoStep_inQueue(pValues)
                        Else

                            Select Case auxWFWMode
                                Case enumWfwMode.coStandard
                                    'No aplica porque en el wfw estandar son creados por administradores
                                    auxGotoStepCancel = True
                                Case enumWfwMode.coUserCreate
                                    Dim auxDTroles As DataTable = gTRO_Get(pTroCod:=auxTroCod)
                                    gTRO_ResolveThisRol(auxDTroles, enumRoles.coReceptor, _
                                        True, coGroupDocumentadorEditores)
                                    gDocPermission_Add(pCod, auxDTroles, auxAclCod, enumRoles.coReceptor, enumAccessType.coSYSGlobalCambiarestado, True)
                                    gDOCSGN_Delete(pCod, -1, -1, enumWorkflowStep.coWFWSTPDOC_DOCSolicitudnuevodocumento)
                                    auxGotoStepNext = coWFWSTPDOC_DOCCreacion
                                    auxSendMails = True
                                    auxOrder = 50
                            End Select
                        End If
                    End If


                Case coWFWSTPDOC_DOCSolicitudeliminacion
                    If pOnlyRunChecks = False Then
                        If auxResult = "" Then
                            If auxJobQueueLevel = 1 Then
                                gWorkflow_GotoStep_inQueue(pValues)
                            Else
                                Dim auxDTroles As DataTable = gTRO_Get(pTroCod:=auxTroCod)
                                gTRO_ResolveThisRol(auxDTroles, enumRoles.coReceptor, _
                                            True, coGroupIDDocumentadorReceptores)

                                gDocPermission_Add(pCod, auxDTroles, auxAclCod, enumRoles.coReceptor, enumAccessType.coSYSGlobalCambiarestado, True)
                                gDOCSGN_Delete(pCod, -1, -1, enumWorkflowStep.coWFWSTPDOC_DOCDocumentovigente)
                                If gDOCLOG_Add(pDsc, pObs, pCod, auxGotoStepToSave, auxHstHidGenCod, True, auxWfwStpCodCurrent, auxWfwStpDscNext, auxsidCod, enumAccessType.coSYSGlobalCambiarestado, auxEmpCod, auxDelEmpCod) = False Then
                                    auxGotoStepCancel = True
                                    auxResult &= "No se encuentran colaboradores para el cambio de estado (Consulte a su administrador)."
                                Else
                                    auxSendMails = True
                                    auxOrder = 50
                                End If
                            End If
                        End If
                    End If

                Case coWFWSTPDOC_DOCSolicitudnuevaversion

                        If auxResult = "" And pOnlyRunChecks = False Then
                            If auxJobQueueLevel = 1 Then
                                gWorkflow_GotoStep_inQueue(pValues)
                            Else
                                Dim auxDTroles As DataTable = gTRO_Get(pTroCod:=auxTroCod)
                                gTRO_ResolveThisRol(auxDTroles, enumRoles.coReceptor, _
                                                    True, -1)
                                gDocPermission_Add(pCod, auxDTroles, auxAclCod, enumRoles.coReceptor, enumAccessType.coSYSGlobalCambiarestado, True)
                                gDOCSGN_Delete(pCod, -1, -1, enumWorkflowStep.coWFWSTPDOC_DOCDocumentovigente)
                            If gDOCLOG_Add(pDsc, pObs, pCod, auxGotoStepToSave, auxHstHidGenCod, True, auxWfwStpCodCurrent, auxWfwStpDscNext, auxsidCod, enumAccessType.coSYSGlobalCambiarestado, auxEmpCod, auxDelEmpCod) = False Then
                                auxGotoStepCancel = True
                                auxResult &= "No se encuentran colaboradores para el cambio de estado (Consulte a su administrador)."
                            Else
                                auxSendMails = True
                                auxOrder = 50
                            End If
                            End If
                        End If
                Case coWFWSTPDOC_DOCSolicitudautomaticanuevaversion

                    If auxResult = "" And pOnlyRunChecks = False Then
                        If auxJobQueueLevel = 1 Then
                            gWorkflow_GotoStep_inQueue(pValues)
                        Else
                            Dim auxDTroles As DataTable = gTRO_Get(pTroCod:=auxTroCod)
                            Select Case auxWFWMode
                                Case enumWfwMode.coStandard
                                    gTRO_ResolveThisRol(auxDTroles, enumRoles.coReceptor, _
                                                        True, -1)
                                    gDocPermission_Add(pCod, auxDTroles, auxAclCod, enumRoles.coReceptor, enumAccessType.coSYSGlobalCambiarestado, True)
                                    gDOCSGN_Delete(pCod, -1, -1, enumWorkflowStep.coWFWSTPDOC_DOCDocumentovigente)
                                    If gDOCLOG_Add(pDsc, pObs, pCod, auxGotoStepToSave, auxHstHidGenCod, True, auxWfwStpCodCurrent, auxWfwStpDscNext, auxsidCod, enumAccessType.coSYSGlobalCambiarestado, auxEmpCod, auxDelEmpCod) = False Then
                                        auxGotoStepCancel = True
                                        auxResult &= "No se encuentran colaboradores para el cambio de estado (Consulte a su administrador)."
                                    Else
                                        auxSendMails = True
                                        auxOrder = 50
                                    End If
                                Case enumWfwMode.coUserCreate
                                    auxGotoStepNext = coWFWSTPDOC_DOCnuevaversion
                            End Select
                        End If
                    End If

                Case coWFWSTPDOC_DOCEliminacionOK
                    If pOnlyRunChecks = False Then
                        If auxJobQueueLevel = 1 Then
                            gWorkflow_GotoStep_inQueue(pValues)
                        Else
                            If m_Conn.gConn_Query("SELECT cod " _
                                                        & " FROM DOC_DOCVIG " _
                                                        & " WHERE docsupcod=" & pCod).Rows.Count <> 0 Then
                                auxResult &= "No es posible eliminar el documento porque posee documentos que dependen de él."
                                auxGotoStepCancel = True
                            End If
                            If auxResult = "" Then
                                m_Security.gACL_DelLogin(auxAclCod, m_Security.CurrentSecCod, enumAccessType.coSYSGlobalCambiarestado)
                                gDOCSGN_Delete(pCod, enumWorkflowStep.coWFWSTPDOC_DOCSolicitudeliminacion, auxEmpCod, Nothing)
                                gDOCLOG_Add(pDsc, pObs, pCod, auxGotoStepToSave, auxHstHidGenCod, False, auxWfwStpCodCurrent, auxWfwStpDscNext, auxsidCod, enumAccessType.coSYSGlobalCambiarestado, auxEmpCod, auxDelEmpCod)
                                If auxForce = False And m_Conn.gConn_Query("SELECT cod " _
                                                                           & " FROM DOC_DOCSGN WHERE doccod=" & pCod _
                                                                           & " AND doclogcod IN (SELECT cod FROM DOC_DOCLOG WHERE doccod=" & pCod & " AND wfwstepnext=" & enumWorkflowStep.coWFWSTPDOC_DOCSolicitudeliminacion & ")").Rows.Count <> 0 Then
                                    auxGotoStepCancel = True
                                Else
                                    auxGotoStepNext = coWFWSTPDOC_DOCDocumentoobsoleto
                                    auxOrder = 50
                                End If
                            End If
                            
                        End If
                    End If

                Case coWFWSTPDOC_DOCVersionanulada     'ANULACIÓN DE VERSION
                    If pOnlyRunChecks = False Then
                        If auxJobQueueLevel = 1 Then
                            gWorkflow_GotoStep_inQueue(pValues)
                        Else
                            Dim auxOldVersion As Integer = m_Conn.gConn_QueryValueInt("SELECT version FROM DOC_DOCVIG WHERE cod=" & pCod, 0)
                            If auxOldVersion > 0 Then
                                m_Conn.gConn_Update("UPDATE DOC_DOC SET version=" & auxOldVersion & " WHERE cod=" & pCod)
                            End If
                            gDOCLOG_Add(pDsc, pObs, pCod, auxGotoStepToSave, auxHstHidGenCod, False, auxWfwStpCodCurrent, auxWfwStpDscNext, auxsidCod, enumAccessType.coSYSGlobalCambiarestado, auxEmpCod, auxDelEmpCod)
                            If auxResult = "" Then
                                If m_Conn.gConn_Query("SELECT TOP 1 wfwstepnext " _
                                        & " FROM DOC_DOCLOG WHERE doccod=" & pCod _
                                        & " AND wfwstepnext =" & coWFWSTPDOC_DOCDocumentovigente _
                                        & " ORDER BY cod DESC").Rows.Count = 0 Then
                                    auxOrder = 50
                                    auxGotoStepToSave = coWFWSTPDOC_DOCCreacion
                                Else
                                    auxGotoStepNext = coWFWSTPDOC_DOCDocumentovigente
                                End If
                            End If
                        End If
                    End If


                Case coWFWSTPDOC_DOCRechazareliminacion
                    If pOnlyRunChecks = False Then
                        If auxJobQueueLevel = 1 Then
                            gWorkflow_GotoStep_inQueue(pValues)
                        Else
                            gDOCLOG_Add(pDsc, pObs, pCod, auxGotoStepToSave, auxHstHidGenCod, False, auxWfwStpCodCurrent, auxWfwStpDscNext, auxsidCod, enumAccessType.coSYSGlobalCambiarestado, auxEmpCod, auxDelEmpCod)
                            auxGotoStepNext = coWFWSTPDOC_DOCDocumentovigente
                        End If
                    End If

                Case coWFWSTPDOC_DOCDocumentoobsoleto
                        If pOnlyRunChecks = False Then
                            If auxJobQueueLevel = 1 Then
                                gWorkflow_GotoStep_inQueue(pValues)
                            Else
                                m_Conn.gConn_ExecuteProcedureUpdate("DELETE FROM DOC_DOCVIG WHERE cod=" & pCod)
                                gDoc_ReApply(pCod:=pCod, pUpdateWfwRoles:=False, pDocMbrLevelForced:=-1)
                                'm_Security.gACL_DelEntriesByAccessType(auxAclCod, enumAccessType.coSYSGlobalCambiarestado)
                                'm_Security.gACL_DelEntriesByAccessType(auxAclCod, enumAccessType.coSYSConfirmarlectura)
                                'm_Security.gACL_DelEntriesByAccessType(auxAclCod, enumAccessType.coSYSGlobalLeer)
                                'm_Security.gACL_AddGroupByID(auxAclCod, coGroupDocumentadorEditores, enumAccessType.coSYSGlobalCambiarestado)
                                'm_Security.gACL_AddGroupByID(auxAclCod, coGroupDocumentadorAdministradores, enumAccessType.coSYSGlobalCambiarestado)
                                gDOCSGN_Delete(pCod, -1, -1, Nothing)
                            gDOCLOG_Add(pDsc, pObs, pCod, auxGotoStepToSave, auxHstHidGenCod, False, auxWfwStpCodCurrent, auxWfwStpDscNext, auxsidCod, enumAccessType.coSYSGlobalCambiarestado, auxEmpCod, auxDelEmpCod)
                            End If
                        End If

                Case coWFWSTPDOC_DOCEliminaciontotal
                        If pOnlyRunChecks = False Then
                            If auxJobQueueLevel = 1 Then
                                gWorkflow_GotoStep_inQueue(pValues)
                            Else
                                m_Conn.gConn_ExecuteProcedureUpdate("DELETE FROM DOC_DOCVIG WHERE cod=" & pCod)
                                gDoc_ReApply(pCod:=pCod, pUpdateWfwRoles:=False, pDocMbrLevelForced:=-1)
                                gEntity_DOC_DOC_Delete(pcod:=pCod)
                                gDOCSGN_Delete(pCod, -1, -1, Nothing)
                            gDOCLOG_Add(pDsc, pObs, pCod, auxGotoStepToSave, auxHstHidGenCod, False, auxWfwStpCodCurrent, auxWfwStpDscNext, auxsidCod, enumAccessType.coSYSGlobalCambiarestado, auxEmpCod, auxDelEmpCod)
                            End If
                        End If

                Case coWFWSTPDOC_DOCDocumentovigente
                    If pOnlyRunChecks = False Then
                        If auxJobQueueLevel = 1 Then
                            gWorkflow_GotoStep_inQueue(pValues)
                        Else
                          

                            If auxResult = "" Then
                                'Debe calcularlo de nuevo porque pueden cambiar los datos undcod,apacod....
                                Dim auxIdentificador As String = gDoc_GetIdentificador(auxDocTipCod, _
                                                                                            auxDT.Rows(0)("undcod"), _
                                                                                            auxDT.Rows(0)("apacod"), _
                                                                                            auxDT.Rows(0)("nro"), _
                                                                                            auxDT.Rows(0)("version"))
                                gEntity_DOC_DOC_SystemUpdate(pcod:=pCod, pidentificador:=auxIdentificador)
                                m_Security.gACL_DelEntriesByAccessType(auxAclCod, enumAccessType.coSYSGlobalCambiarestado)
                                m_Security.gACL_AddGroupByID(auxAclCod, coGroupDocumentadorAdministradores, enumAccessType.coSYSGlobalCambiarestado)
                                m_Security.gACL_AddGroupByID(auxAclCod, coGroupDocumentadorEditores, enumAccessType.coSYSGlobalCambiarestado)
                                'gDocPermission_Add(pCod,auxDTRoles, auxPROACLcod, enumRoles.coeLector,  enumAccessType.coSYSGlobalCambiarestado, True)
                                Dim auxDTroles As DataTable = gTRO_Get(pTroCod:=auxTroCod)
                                gDocPermission_Add(pCod, auxDTroles, auxAclCod, enumRoles.coEditor, enumAccessType.coSYSGlobalCambiarestado, True)
                                Dim auxRequiredSignature As Boolean = True
                                auxSendMails = True
                                If auxWfwStpCodCurrent = coWFWSTPDOC_DOCReidentificacion _
                                    Or auxWfwStpCodCurrent = coWFWSTPDOC_DOCDocumentoobsoleto Then
                                    auxRequiredSignature = False
                                    auxSendMails = False
                                End If
                                If auxWfwStpCodCurrent = coWFWSTPDOC_DOCRechazareliminacion _
                                    Or auxWfwStpCodCurrent = coWFWSTPDOC_DOCnuevaversionNOK _
                                    Or auxWfwStpCodCurrent = coWFWSTPDOC_DOCVersionanulada Then
                                    gDOCSGN_Delete(pCod, -1, -1, enumWorkflowStep.coWFWSTPDOC_DOCDocumentovigente)
                                    Dim auxDTVigente As DataTable = m_Conn.gConn_Query("SELECT version,fecha FROM DOC_DOCVIG WHERE cod=" & pCod)
                                    If auxDTVigente.Rows.Count <> 0 Then
                                        gEntity_DOC_DOC_SystemUpdate(pcod:=pCod, _
                                                                        pfecha:=auxDTVigente.Rows(0)("fecha"), _
                                                                        pversion:=auxDTVigente.Rows(0)("version"))
                                    End If
                                Else
                                    gEntity_DOC_DOC_SystemUpdate(pcod:=pCod, _
                                                                    pfecha:=m_Conn.gDate_GetNow)
                                    gEntity_DOC_DOCVIG_Copy(pCod)
                                    gTRACE_add(pCod, 1, "Seteo de permisos al entrar en vigencia")
                                    'Utliza un permiso diferente a globalLeer para controlar las firmas
                                    'globalLeer-> especificoA/visualizadores/administradoresyotros
                                    'coSYSConfirmarlectura->lectores
                                    m_Security.gACL_DelEntriesByAccessType(auxAclCod, enumAccessType.coSYSConfirmarlectura)
                                    gDocPermission_Add(pCod, auxDTroles, auxAclCod, enumRoles.coLector, enumAccessType.coSYSConfirmarlectura, True)
                                    'Solo cuando ha pasado por publicación. Porque puede ser originado en 
                                    gDOCSGN_Delete(pCod, -1, -1, Nothing)
                                    auxHstHidGenCod = gEntity_DOC_DOC_NewVersion(-1, pCod, "v" & auxDT.Rows(0)("version") & " - Vigente")


                                    gDOCLOG_Add("versión" & auxDT.Rows(0)("version"), pObs, pCod, auxGotoStepToSave, auxHstHidGenCod, auxRequiredSignature, auxWfwStpCodCurrent, auxWfwStpDscNext, auxsidCod, enumAccessType.coSYSConfirmarlectura, auxEmpCod, auxDelEmpCod)

                                    'm_Security.gACL_DelEntriesByAccessType(auxPROACLcod, enumAccessType.coWILDOCDocumentosModificar)
                                    'Permisos de lectura
                                    '1. Rol Lectores
                                    '2. Miembros de unidades especificadas
                                    '3. Administradores
                                    gDoc_ReApplyRole(auxDTroles, enumRoles.coLector, pCod, auxAclCod, auxsidCod)
                                    gDoc_ReApplyRole(auxDTroles, enumRoles.coVisualizador, pCod, auxAclCod, auxsidCod)
                                    gDoc_ReApplyRole(auxDTroles, enumRoles.coImpresor, pCod, auxAclCod, auxsidCod)

                                    'Si el parametro de aprobador no lee esta activo, no se requiere la firma de quienes hayan aprobado
                                    If m_Conn.gField_GetBoolean(gSystem_GetParameterByID(coSysParamIDAprobadorNoLee), False) Then
                                        For Each auxRow As DataRow In m_Conn.gConn_Query("SELECT EMP.cod as [emp*cod],EMP.seccod as [emp*seccod],EMP.dsc as [emp*dsc]" _
                                                                                         & " FROM DOC_DOCLOG " _
                                                                                         & " LEFT JOIN EMP ON DOC_DOCLOG.empcod=EMP.cod " _
                                                                                         & " WHERE DOC_DOCLOG.wfwstepnext=" & enumWorkflowStep.coWFWSTPDOC_DOCaprobacionOK _
                                                                                         & " AND DOC_DOCLOG.cod > ISNULL((SELECT TOP 1 cod FROM DOC_DOCLOG WHERE doccod=" & pCod _
                                                                                                & " AND wfwstepnext IN (" & enumWorkflowStep.coWFWSTPDOC_DOCnuevaversion & "," & enumWorkflowStep.coWFWSTPDOC_DOCReidentificacion & ") " _
                                                                                                & " ORDER BY cod DESC),0)" _
                                                                                         & " AND DOC_DOCLOG.doccod=" & pCod).Rows
                                            gTRACE_add(pCod, 1, "Aprobador-NoLee activo:[" & auxRow("emp*dsc") & "]")
                                            pValues.gValue_Add("empcod", auxRow("emp*cod"))
                                            pValues.gValue_Add("seccod", auxRow("emp*seccod"))
                                            pValues.gValue_Add("obs", "Lectura confirmada automáticamente por aprobación")
                                            pValues.gValue_Add("gotoStep", enumWorkflowStep.coWFWSTPDOC_DOCLecturaOK)
                                            gWorkflow_GotoStep_EXE(pValues, 1)
                                            ' Dim auxQueueList As New List(Of Integer)
                                            ' Dim auxQueCod As Integer = hrcEntityDT_EMP_FindByKey(auxRow("emp*cod"))("alequecod")
                                            ' If auxQueCod > 0 Then
                                            'auxQueueList.Add(auxQueCod)
                                            'End If
                                            'm_Alerts.gRoutes_InsertAlert(pSubject, pContent, clsHrcAlertClient.enumLevel.coInfo, clsHrcAlertClient.enumConfirmMode.coNoConfirm, -1, auxQueueList)
                                        Next
                                    End If
                                End If
                            End If
                        End If
                    End If
            End Select
            'Chequeos antes de entrar a vigencia
            If auxGotoStepCancel = False _
                And auxResult = "" _
                And auxGotoStepNext = coWFWSTPDOC_DOCDocumentovigente Then
                If m_Conn.gField_GetInt(auxDT.Rows(0)("procod")) < 1 Then
                    Dim auxDocSupCod As Integer = m_Conn.gField_GetInt(auxDT.Rows(0)("docsupcod"), -1)
                    If auxDocSupCod > 0 Then
                        If m_Conn.gConn_Query("SELECT cod " _
                                              & " FROM DOC_DOCVIG " _
                                              & " WHERE cod=" & auxDocSupCod).Rows.Count = 0 Then
                            auxResult &= "El documento no puede pasar a vigente porque depende de un documento que aún no se encuentra vigente."
                            auxGotoStepCancel = True
                        End If
                    End If
                End If
            End If

            If pOnlyRunChecks = False And auxJobQueueLevel > 1 Then
                If auxResult = "" Then
                    gModoObligado_Check()

                    If auxGotoStepCancel = False Then
                        'Pasa el siguiente paso
                        Dim auxOrderChange As String = ""
                        If auxOrder > 0 Then
                            auxOrderChange = ",orden=" & auxOrder
                        End If
                        m_Conn.gConn_Update("UPDATE DOC_DOC SET wfwstatus = " & auxGotoStepToSave & auxOrderChange & " WHERE cod = " & pCod)
                        If auxGotoStepNext <> pGotoStep And auxGotoStepNext <> -1 Then
                            Dim auxValues As New clshrcBagValues(pValues)
                            auxValues.gValue_Add("Cod", pCod)
                            auxValues.gValue_Add("gotostep", auxGotoStepNext)
                            auxValues.gValue_Add("dsc", "")
                            auxResult = gWorkflow_GotoStep_EXE(auxValues, auxJobQueueLevel)
                            'auxResult = gWorkflow_GotoStep(pDocCod, auxGotoStepNext, "", pObs, pOnlyRunChecks, pDftGenCod)
                        End If
                        If auxSendMails Then
                            gDOC_SendMails(pCod)
                        End If
                    End If
                Else
                    gTRACE_add(pCod, 1, "el item [" & pCod & "] no cambio de estado por el siguiente motivo [" & auxResult & "]")
                End If
            End If
            If auxJobQueueLevel = 2 Or _
               (auxJobQueueLevel = 1 And auxResult <> "") Then
                gTRACE_add(pCod, 5, "Bloqueo liberado de item [" & pCod & "]")
                gEntity_DOC_DOC_Update(pcod:=pCod, pwfwlocked:=False)
            End If
        Catch ex As Exception
            auxResult &= "//Error en cambio de paso:" & ex.Message & "."
            gTRACE_add(pCod, 1, "Proceso de item [" & pCod & "] cancelado por excepcion [" & auxResult & "." & ex.StackTrace & "]")
        End Try
        gTRACE_add(pCod, 1, "Proceso de item [" & pCod & "] terminado paso [" & pGotoStep & "]" & ".QueueLevel=" & auxJobQueueLevel)
        Return auxResult
    End Function

    Public Function gModoObligado_Check() As Boolean
        Dim auxReturn As Boolean = False
        Dim auxContext As HttpContext = HttpContext.Current
        If auxContext IsNot Nothing Then
            Dim auxEmpCod As Integer = auxContext.Session("empcod")
            Dim auxWhere As String = ""
            Dim auxModoObligatorioEnabled As Boolean = m_Conn.gField_GetBoolean(gSystem_GetParameterByID(coSysParamIDModoObligatorio), False)
            If auxModoObligatorioEnabled Then
                If m_Conn.gField_GetBoolean(gSystem_GetParameterByID(coSysParamIDModoObligatorioAdmins), False) Then
                    If m_Security.gMember_IsInGroupByID(coGroupDocumentadorAdministradores) Then
                        auxModoObligatorioEnabled = False
                    End If
                End If
                If auxModoObligatorioEnabled Then
                    If m_Conn.gField_GetBoolean(gSystem_GetParameterByID(coSysParamIDModoObligatorioEditors), False) Then
                        If m_Security.gMember_IsInGroupByID(coGroupDocumentadorEditores) Then
                            auxModoObligatorioEnabled = False
                        End If
                    End If
                End If
            End If
            If auxModoObligatorioEnabled Then
                If m_Conn.gField_GetBoolean(gSystem_GetParameterByID(coSysParamIDModoObligatorioSoloVigentes), True) Then
                    auxWhere &= " AND DOC_DOCLOG.wfwstepnext=" & enumWorkflowStep.coWFWSTPDOC_DOCDocumentovigente
                End If
                If m_Conn.gConn_Query("SELECT DOC_DOCSGN.cod " _
                                      & " FROM DOC_DOCSGN " _
                                      & " LEFT JOIN DOC_DOCLOG ON DOC_DOCSGN.doclogcod=DOC_DOCLOG.cod " _
                                      & " WHERE DOC_DOCSGN.empcod=" & auxEmpCod _
                                      & " AND DOC_DOCSGN.doccod NOT IN (SELECT cod FROM DOC_DOC WHERE baja=" & m_Conn.gFieldDB_GetBoolean(True) & ")" _
                                      & auxWhere _
                                      & " AND DOC_DOCSGN.avisoscant >=" & gSystem_GetParameterByID(coSysParamIDAvisosCantMax)).Rows.Count = 0 Then
                    auxReturn = False
                    gTRACE_add(-1, 1, "Modo obligatorio desactivado para [" & auxEmpCod & "]")
                Else
                    'Modo obligatorio habilitado para este usuario y tiene firmas vencidas
                    auxReturn = True
                    gTRACE_add(-1, 1, "Modo obligatorio activado para [" & auxEmpCod & "]")
                End If
            Else
                auxReturn = False
            End If

            auxContext.Session("modo_obligado") = auxReturn
        End If
        Return auxReturn
    End Function
    Public Sub gDTRoles_Add(ByVal pDTRoles As DataTable, _
                             ByVal pRol As enumRoles, _
                             ByVal pDocMbrType As enumEntities, _
                             ByVal pMemberSidCod As Integer, _
                             ByVal pdyngrpcod As Integer)
        Select Case pDocMbrType
            Case enumEntities.coEntityDOC_DYNGRP
                If pdyngrpcod > 0 Then
                    Dim auxNewRow As DataRow
                    auxNewRow = pDTRoles.NewRow
                    auxNewRow("rolcod") = pRol
                    auxNewRow("docmbrcod") = 1000
                    auxNewRow("docmbrtype") = pDocMbrType
                    auxNewRow("dyngrpcod") = pdyngrpcod
                    pDTRoles.Rows.Add(auxNewRow)
                End If
            Case Else
                If pMemberSidCod > 0 Then
                    Dim auxNewRow As DataRow
                    auxNewRow = pDTRoles.NewRow
                    auxNewRow("rolcod") = pRol
                    auxNewRow("docmbrcod") = 1000
                    auxNewRow("docmbrtype") = pDocMbrType
                    auxNewRow("membersidcod") = pMemberSidCod
                    pDTRoles.Rows.Add(auxNewRow)
                End If
        End Select


    End Sub
    Private Function gDocPermission_Add(ByVal pDocCod As Integer, _
                                  ByVal pDTRoles As DataTable, _
                                  ByVal pACLcod As Integer, _
                                  ByVal pRol As enumRoles, _
                                  ByVal pAccessType As enumAccessType, _
                                  ByVal pAddGroupMembers As Boolean, _
                                  Optional pLoginListExcluded As List(Of Integer) = Nothing, _
                                  Optional pGroupListExcluded As List(Of Integer) = Nothing) As List(Of Integer)
        'Toma la plantillay aplica los permisos de acuerdo al rol
        'Devuelve la lista de SID agregados
        Dim auxRolCod As enumRoles
        Dim auxMemberSidCod As Integer
        Dim auxSidListExcluded As New List(Of Integer)
        If pLoginListExcluded Is Nothing Then
            pLoginListExcluded = New List(Of Integer)
        End If
        If pGroupListExcluded Is Nothing Then
            pGroupListExcluded = New List(Of Integer)
        End If
        Dim auxDTRoles As DataTable = gDTroles_Resolve(pDTRoles, pRol)
        For Each auxRow As DataRow In auxDTRoles.Rows
            auxRolCod = CType(auxRow("rolcod"), enumRoles)
            If auxRolCod = pRol Then
                auxMemberSidCod = m_Conn.gField_GetInt(auxRow("membersidcod"))
                Select Case CType(auxRow("docmbrtype"), enumEntities)
                    Case clsHrcSecurityClient.enumSIDType.coUser
                        If m_Security.gACL_AddLogin(pACLcod, auxMemberSidCod, pAccessType) Then
                            gTRACE_add(pProCod:=pDocCod, pTrclevel:=10, pTrcDsc:="Acceso a login directo [" & auxMemberSidCod & "]" & pAccessType.ToString)
                        End If
                        auxSidListExcluded.Add(m_Security.gLogin_GetSidCod(auxMemberSidCod))
                    Case clsHrcSecurityClient.enumSIDType.coGroup
                        If m_Security.gACL_AddGroup(pACLcod, auxMemberSidCod, pAccessType) Then
                            gTRACE_add(pProCod:=pDocCod, pTrclevel:=10, pTrcDsc:="Acceso a grupo directo [" & auxMemberSidCod & "]" & pAccessType.ToString)
                        End If
                        auxSidListExcluded.Add(m_Security.gGroup_GetSidCod(auxMemberSidCod))
                    Case enumEntities.coEntityEMP
                        If pLoginListExcluded.IndexOf(auxMemberSidCod) = -1 Then
                            pLoginListExcluded.Add(auxMemberSidCod)
                            If m_Security.gACL_AddLogin(pACLcod, auxMemberSidCod, pAccessType) Then
                                gTRACE_add(pProCod:=pDocCod, pTrclevel:=10, pTrcDsc:="Acceso a login [" & auxMemberSidCod & "]" & pAccessType.ToString)
                            End If
                            auxSidListExcluded.Add(m_Security.gLogin_GetSidCod(auxMemberSidCod))
                        End If
                    Case enumEntities.coEntityDOC_EQU, enumEntities.coEntityUND
                        If pAddGroupMembers Then
                            For Each auxLoginRow As DataRow In m_Security.gGroup_ResolveLogins(auxMemberSidCod).Rows
                                If pLoginListExcluded.IndexOf(auxLoginRow("seccod")) = -1 Then
                                    pLoginListExcluded.Add(auxLoginRow("seccod"))
                                    Dim auxLoginSidCod As Integer = m_Security.gLogin_GetSidCod(auxLoginRow("seccod"))
                                    If auxSidListExcluded.IndexOf(auxLoginSidCod) = -1 Then
                                        If m_Security.gACL_AddLogin(pACLcod, auxLoginRow("seccod"), pAccessType) Then
                                            gTRACE_add(pProCod:=pDocCod, pTrclevel:=10, pTrcDsc:="Acceso a login(miembro) [" & auxLoginRow("seccod") & "]." & pAccessType.ToString)
                                        End If
                                        auxSidListExcluded.Add(auxLoginSidCod)
                                    End If
                                End If
                            Next
                        Else
                            If pGroupListExcluded.IndexOf(auxMemberSidCod) = -1 Then
                                pGroupListExcluded.Add(auxMemberSidCod)
                                If m_Security.gACL_AddGroup(pACLcod, auxMemberSidCod, pAccessType) Then
                                    gTRACE_add(pProCod:=pDocCod, pTrclevel:=10, pTrcDsc:="Acceso a grupo [" & auxMemberSidCod & "]" & pAccessType.ToString)
                                End If
                                auxSidListExcluded.Add(m_Security.gGroup_GetSidCod(auxMemberSidCod))
                            End If
                        End If
                End Select
            End If
        Next

        m_Security.gACL_DelEntriesByAccessType(pACLcod, pAccessType, auxSidListExcluded)
        Return auxSidListExcluded
    End Function
    Public Function gDTroles_Resolve(ByVal pDTRoles As DataTable, _
                                      ByVal pRol As enumRoles) As DataTable
        'Devuelve la plantilla de roles, resolviendo:
        '-Roles dinámicos
        Dim auxRolCod As enumRoles
        Dim auxResolveRolCod As enumRoles
        Dim auxDTRoles As DataTable = pDTRoles.Clone
        Dim auxRowTmp As DataRow
        For Each auxRow As DataRow In pDTRoles.Rows
            auxRolCod = CType(auxRow("rolcod"), enumRoles)
            If auxRolCod = pRol Or pRol = -1 Then
                Select Case CType(auxRow("docmbrtype"), enumEntities)
                    Case enumEntities.coEntityDOC_DYNGRP
                        auxResolveRolCod = -1
                        Select Case CType(auxRow("dyngrpcod"), enumIdentidadesEspeciales)
                            Case enumIdentidadesEspeciales.coAprobador
                                auxResolveRolCod = enumRoles.coAprobador
                            Case enumIdentidadesEspeciales.coEditores
                                auxResolveRolCod = enumRoles.coEditor
                        End Select
                        If auxResolveRolCod > 0 Then
                            For Each auxRowSub As DataRow In pDTRoles.Select("rolcod=" & auxResolveRolCod)
                                auxRowTmp = auxDTRoles.NewRow
                                auxRowTmp.ItemArray = auxRowSub.ItemArray
                                auxRowTmp("rolcod") = auxRolCod
                                auxDTRoles.Rows.Add(auxRowTmp)
                            Next
                        End If
                    Case Else
                        auxRowTmp = auxDTRoles.NewRow
                        auxRowTmp.ItemArray = auxRow.ItemArray
                        auxDTRoles.Rows.Add(auxRowTmp)
                End Select
            End If
        Next

        'For Each auxRow As DataRow In auxDTRoles.Rows
        '    auxMemberSidCod = m_Conn.gField_GetInt(auxRow("membersidcod"))
        '    If auxMemberSidCod > 0 Then
        '        auxNode = Nothing
        '        Select Case CType(auxRow("docmbrtype"), enumEntities)
        '            Case enumEntities.coEntityEMP
        '                auxNode = New clsNode(clsHrcSecurityClient.enumSIDType.coUser, auxMemberSidCod)
        '            Case enumEntities.coEntityDOC_EQU, enumEntities.coEntityUND
        '                auxNode = New clsNode(clsHrcSecurityClient.enumSIDType.coGroup, auxMemberSidCod)
        '        End Select
        '        If auxNode IsNot Nothing Then
        '            auxReturn.Add(auxNode)
        '        End If
        '    End If
        'Next

        Return auxDTRoles
    End Function
    Public Overrides Function gSystem_ExecuteScheduledTask() As String
        '1. Primera tarea, es sincronizar grupos
        DebugLogOn = True
        gSystem_Init()
        gTRACE_add(-1, 1, "Tareas programadas activadas")
        'If m_Security.gMember_IsInGroupByID(m_Security.CurrentSidCod, coGroupIDAdmins)
        'gAD_importUsers()
        '2. Enviar mails diarios
        gDOC_SendMails()
        '3.Enviar mails de la semana
        '5. BORRAR drafts antiguos
        gDraft_DeleteAll(m_Conn.gDate_GetNow.AddDays(-5))
        gTRACE_add(-1, 1, "Tareas programadas terminadas")

    End Function
    Public Function gContenido_ChangeVars(ByVal pContenido As String, _
                                      ByVal pChangeGlobal As Boolean) As String
        Return gContenido_ChangeVars(pContenido, pChangeGlobal, "", "", "", "", "", "", 0, "", "", "", "", "", "", "", "", "")
    End Function
    Public Function gContenido_ChangeVars(ByVal pContenido As String, _
                                          ByVal pChangeGlobal As Boolean, _
                                          ByVal pDsc As String, _
                                          ByVal pDsc0 As String, _
                                          ByVal pDsc1 As String, _
                                          ByVal pDsc2 As String, _
                                          ByVal pWfwStpDsc As String, _
                                          ByVal pIdentificador As String, _
                                          ByVal pVersion As Integer, _
                                          ByVal pPrcDsc As String, _
                                          ByVal pEprCod As String, _
                                          ByVal pEprDsc As String, _
                                          ByVal pFecha As String, _
                                          ByVal pCopiaTexto As String, _
                                          ByVal pDocTipDsc As String, _
                                          ByVal pAprobadoPor As String, _
                                          ByVal pAprobadoFecha As String, _
                                          ByVal pEspecificoA As String) As String
        Dim auxNow As Date = m_Conn.gDate_GetNow
        If pChangeGlobal Then
            pContenido = pContenido.Replace("{#DATETIME#}", auxNow.ToString("d/M/yyyy HH:mm:ss"))
            pContenido = pContenido.Replace("{#DATETIME_DMYHM#}", auxNow.ToString("d/M/yyyy HH:mm"))
            pContenido = pContenido.Replace("{#DATE#}", auxNow.ToString("d/M/yyyy"))
            pContenido = pContenido.Replace("{#SECDSC#}", m_Security.CurrentSecDsc)
            pContenido = pContenido.Replace("{#SYSTEM_USER#}", m_Security.CurrentSecDsc)
        End If
        If pDsc <> "" Then
            pContenido = pContenido.Replace("{#DOC.DSC#}", pDsc)
        End If
        pContenido = pContenido.Replace("{#DOC.DSC#}", "")


        If pDsc0 <> "" Then
            pContenido = pContenido.Replace("{#DOC.DSC0#}", pDsc0)
        End If
        pContenido = pContenido.Replace("{#DOC.DSC0#}", "")

        If pDsc1 <> "" Then
            pContenido = pContenido.Replace("{#DOC.DSC1#}", pDsc1)
        End If
        pContenido = pContenido.Replace("{#DOC.DSC1#}", "")


        If pDsc2 <> "" Then
            pContenido = pContenido.Replace("{#DOC.DSC2#}", pDsc2)
        End If
        pContenido = pContenido.Replace("{#DOC.DSC2#}", "")


        If pWfwStpDsc <> "" Then
            pContenido = pContenido.Replace("{#DOC.WFWSTPDSC#}", pWfwStpDsc)
        End If
        pContenido = pContenido.Replace("{#DOC.WFWSTPDSC#}", "")

        If pDocTipDsc <> "" Then
            pContenido = pContenido.Replace("{#DOCTIP.DSC#}", pDocTipDsc)
            pContenido = pContenido.Replace("{#DOC.DOCTIPDSC#}", pDocTipDsc)
        End If
        pContenido = pContenido.Replace("{#DOCTIP.DSC#}", "")
        pContenido = pContenido.Replace("{#DOC.DOCTIPDSC#}", "")

        If pIdentificador <> "" Then
            pContenido = pContenido.Replace("{#DOC.IDENTIFICADOR#}", pIdentificador)
        End If
        pContenido = pContenido.Replace("{#DOC.IDENTIFICADOR#}", "")

        If pVersion > 0 Then
            pContenido = pContenido.Replace("{#DOC.VERSION#}", pVersion.ToString("0000"))
        End If
        pContenido = pContenido.Replace("{#DOC.VERSION#}", "")

        If pPrcDsc <> "" Then
            pContenido = pContenido.Replace("{#DOC.PRCDSC#}", pPrcDsc)
        End If
        pContenido = pContenido.Replace("{#DOC.PRCDSC#}", "")

        If pEprDsc <> "" Then
            pContenido = pContenido.Replace("{#DOC.EPRDSC#}", pEprDsc)
        End If
        pContenido = pContenido.Replace("{#DOC.EPRDSC#}", "")

        If pEprCod <> "" Then
            pContenido = pContenido.Replace("{#DOC.EPRCOD#}", pEprCod)
        End If
        pContenido = pContenido.Replace("{#DOC.EPRCOD#}", "")

        If pFecha <> "" Then
            pContenido = pContenido.Replace("{#DOC.FECHA#}", pFecha)
        End If
        pContenido = pContenido.Replace("{#DOC.FECHA#}", "")

        If pCopiaTexto <> "" Then
            pContenido = pContenido.Replace("{#DOC.COPIATEXTO#}", pCopiaTexto)
        End If
        pContenido = pContenido.Replace("{#DOC.COPIATEXTO#}", "")

        If pAprobadoPor <> "" Then
            pContenido = pContenido.Replace("{#DOC.APROBADOPOR#}", pAprobadoPor)
        End If
        pContenido = pContenido.Replace("{#DOC.APROBADOPOR#}", "")

        If pAprobadoFecha <> "" Then
            pContenido = pContenido.Replace("{#DOC.APROBADOFECHA#}", pAprobadoFecha)
        End If
        pContenido = pContenido.Replace("{#DOC.APROBADOFECHA#}", "")

        If pEspecificoA <> "" Then
            pContenido = pContenido.Replace("{#DOC.ESPECIFICOA#}", pEspecificoA)
        End If
        pContenido = pContenido.Replace("{#DOC.ESPECIFICOA#}", "")
        Return pContenido
    End Function
    Private Sub gDOC_DOC_PostAction(ByVal pDocCod As Integer, _
                                   ByVal pAction As enumActionType)
        Dim auxparam As New Intelimedia.inTasks.clsTaskinQueue
        Dim auxValues As New clshrcBagValues
        auxValues.gValue_Add("task_type", "doc_postaction")
        auxValues.gValue_Add("cod", pDocCod)
        auxValues.gValue_Add("action", pAction)
        Dim auxTask As New clsCustomBasicTask
        auxTask.BagValues = auxValues
        auxparam.Tasks.Add(1, auxTask)
        auxparam.ExecutionTitle = "Documentos|Actualización|" & pAction.ToString & "|" & pDocCod
        hrcProcessQueue.gProcessor_AddTask(auxparam)
        auxValues.gValue_Add("HRC_EXECUTIONQUEUEID", auxparam.ExecutionQueueID)
    End Sub
    Private Sub gDOC_EQU_PostAction(ByVal pEquCod As Integer, _
                                    ByVal pAction As enumActionType, _
                                    ByVal pOldValues As clshrcBagValues, _
                                    ByVal pNewValues As clshrcBagValues)
        Dim auxparam As New Intelimedia.inTasks.clsTaskinQueue
        Dim auxValues As New clshrcBagValues
        auxValues.gValue_Add("task_type", "equ_postaction")
        auxValues.gValue_Add("cod", pEquCod)
        auxValues.gValue_Add("action", pAction)
        Dim auxTask As New clsCustomBasicTask
        auxTask.BagValues = auxValues
        auxparam.Tasks.Add(1, auxTask)
        auxTask.BagValues.gValue_Add("oldvalues", pOldValues)
        auxTask.BagValues.gValue_Add("newvalues", pNewValues)
        auxparam.ExecutionTimeOut = 60 * 60 * 1000  '1h
        auxparam.ExecutionTitle = "Equipos|Reaplicación de permisos"
        hrcProcessQueue.gProcessor_AddTask(auxparam)
        auxValues.gValue_Add("HRC_EXECUTIONQUEUEID", auxparam.ExecutionQueueID)
    End Sub
    Public Function gDoc_ReApply(ByVal pCod As Integer, _
                            ByVal pUpdateWfwRoles As Boolean, _
                            ByVal pDocMbrLevelForced As Integer, _
                            Optional ByVal pCheckEmptyRoles As Boolean = False) As clshrcBagValues
        Dim auxReturn As New clshrcBagValues
        Dim auxDT As DataTable

        auxDT = m_Conn.gConn_Query("SELECT dsc,qsidcod,wfwstatus,wfwmode,trocod,doctipcod " _
                                   & " FROM DOC_DOC " _
                                   & " WHERE cod =" & pCod _
                                   & " AND (baja {#ISNULL#} OR baja = {#FALSE#})")
        Dim auxErrors As New List(Of String)
        If auxDT.Rows.Count <> 0 Then
            Dim auxDocDsc As String = auxDT.Rows(0)("dsc")
            Dim auxTieneVigente As Boolean = False
            Dim auxSidCod As Integer
            Dim auxDocTipCod As Integer
            Dim auxDTRoles As DataTable
            Dim auxWfwStatus As enumWorkflowStep
            auxSidCod = m_Conn.gField_GetInt(auxDT.Rows(0)("qsidcod"), -1)
            auxDocTipCod = m_Conn.gField_GetInt(auxDT.Rows(0)("doctipcod"), -1)
            Dim auxRowDOCTIP As DataRow = hrcEntityDT_DOC_DOCTIP_FindByKey(auxDocTipCod)
            auxWfwStatus = m_Conn.gField_GetInt(auxDT.Rows(0)("wfwstatus"), -1)
            Dim auxWfwMode As enumWfwMode = m_Conn.gField_GetInt(auxDT.Rows(0)("wfwmode"), -1)
            Dim auxTroCod As Integer = m_Conn.gField_GetInt(auxDT.Rows(0)("trocod"), -1)
            auxDT = m_Conn.gConn_Query("SELECT qsidcod FROM DOC_DOCVIG WHERE cod =" & pCod)
            If auxDT.Rows.Count <> 0 Then
                auxTieneVigente = True
                auxSidCod = m_Conn.gField_GetInt(auxDT.Rows(0)("qsidcod"), -1)
            End If
            Dim auxAclCod As Integer = -1
            If auxSidCod > 0 Then
                auxAclCod = m_Security.gSID_GetACL(auxSidCod)
            End If
            auxDTRoles = gTRO_Get(pTroCod:=auxTroCod)
            'gDoc_ReApplyRole(auxDTRoles, enumRoles.coReceptor, pCod, auxAclCod, auxSidCod)
            If auxTieneVigente Then
                gDoc_ReApplyRole(auxDTRoles, enumRoles.coLector, pCod, auxAclCod, auxSidCod)
                gDoc_ReApplyRole(auxDTRoles, enumRoles.coVisualizador, pCod, auxAclCod, auxSidCod)
                gDoc_ReApplyRole(auxDTRoles, enumRoles.coImpresor, pCod, auxAclCod, auxSidCod)
            Else
                m_Security.gACL_DelEntriesByAccessType(auxAclCod, enumAccessType.coSYSConfirmarlectura)
                m_Security.gACL_DelEntriesByAccessType(auxAclCod, enumAccessType.coSYSGlobalLeer)
                m_Security.gACL_DelEntriesByAccessType(auxAclCod, enumAccessType.coDOCDOCVIGDocumentosvigentesVer)
                m_Security.gACL_AddGroupByID(auxAclCod, coGroupDocumentadorAdministradores, enumAccessType.coSYSGlobalCambiarpermisos)
            End If
            If pUpdateWfwRoles Then
                Select Case auxWfwStatus
                    Case coWFWSTPDOC_DOCEdicion, coWFWSTPDOC_DOCnuevaversion
                        gDoc_ReApplyRole(auxDTRoles, enumRoles.coEditor, pCod, auxAclCod, auxSidCod)
                        If pCheckEmptyRoles Then
                            If m_Conn.gField_GetBoolean(auxRowDOCTIP("opceditor"), False) = False Then
                                If m_Security.gSID_ResolveLoginsByAccessType(auxSidCod, enumAccessType.coSYSGlobalCambiarestado).Rows.Count = 0 Then
                                    auxErrors.Add("Documento:" & auxDocDsc & " no posee rol editor")
                                End If
                            End If
                        End If
                    Case coWFWSTPDOC_DOCAprobacion
                        gDoc_ReApplyRole(auxDTRoles, enumRoles.coAprobador, pCod, auxAclCod, auxSidCod)
                        If pCheckEmptyRoles Then
                            If m_Conn.gField_GetBoolean(auxRowDOCTIP("opcaprobador"), False) = False Then
                                If m_Security.gSID_ResolveLoginsByAccessType(auxSidCod, enumAccessType.coSYSGlobalCambiarestado).Rows.Count = 0 Then
                                    auxErrors.Add("Documento:" & auxDocDsc & " no posee rol aprobador")
                                End If
                            End If
                        End If
                    Case coWFWSTPDOC_DOCRevisionSSG
                        gDoc_ReApplyRole(auxDTRoles, enumRoles.coRevisor, pCod, auxAclCod, auxSidCod)
                        If pCheckEmptyRoles Then
                            If m_Conn.gField_GetBoolean(auxRowDOCTIP("opcrevisor"), False) = False Then
                                If m_Security.gSID_ResolveLoginsByAccessType(auxSidCod, enumAccessType.coSYSGlobalCambiarestado).Rows.Count = 0 Then
                                    auxErrors.Add("Documento:" & auxDocDsc & " no posee rol revisor")
                                End If
                            End If
                        End If
                    Case coWFWSTPDOC_DOCPublicacion
                        gDoc_ReApplyRole(auxDTRoles, enumRoles.coPublicador, pCod, auxAclCod, auxSidCod)
                        If pCheckEmptyRoles Then
                            If m_Conn.gField_GetBoolean(auxRowDOCTIP("opcpublicador"), False) = False Then
                                If m_Security.gSID_ResolveLoginsByAccessType(auxSidCod, enumAccessType.coSYSGlobalCambiarestado).Rows.Count = 0 Then
                                    auxErrors.Add("Documento:" & auxDocDsc & " no posee rol publicador")
                                End If
                            End If
                        End If
                    Case coWFWSTPDOC_DOCCancelacion
                        If m_Conn.gField_GetBoolean(auxRowDOCTIP("opccancelador"), False) = False Then
                            gTRO_ResolveThisRol(auxDTRoles, enumRoles.coReceptor, _
                                           True, coGroupIDDocumentadorReceptores)
                        End If
                        gDoc_ReApplyRole(auxDTRoles, enumRoles.coCancelador, pCod, auxAclCod, auxSidCod)
                        If pCheckEmptyRoles Then
                            If m_Conn.gField_GetBoolean(auxRowDOCTIP("opccancelador"), False) = False Then
                                If m_Security.gSID_ResolveLoginsByAccessType(auxSidCod, enumAccessType.coSYSGlobalCambiarestado).Rows.Count = 0 Then
                                    auxErrors.Add("Documento:" & auxDocDsc & " no posee rol cancelador")
                                End If
                            End If
                        End If

                    Case coWFWSTPDOC_DOCSolicitudeliminacion
                        gDoc_ReApplyRole(auxDTRoles, enumRoles.coReceptor, pCod, auxAclCod, auxSidCod)

                    Case coWFWSTPDOC_DOCSolicitudautomaticanuevaversion, coWFWSTPDOC_DOCSolicitudnuevaversion
                        gDoc_ReApplyRole(auxDTRoles, enumRoles.coReceptor, pCod, auxAclCod, auxSidCod)
                End Select
            End If
        End If
        auxReturn.gValue_Add("ERRORS", auxErrors)
        Return auxReturn
    End Function
    Private Sub gDoc_ReApplyRole(ByVal pDTRoles As DataTable, _
                                 ByVal pRol As enumRoles, _
                                 ByVal pDocCod As Integer, _
                                 ByVal pAclCod As Integer, _
                                 ByVal pSidCod As Integer)
        'gTRACE_add(-1, 10, "Proceso chequeo nuevos roles-Inicio:" & pRol.ToString)
        Try
            Dim auxDocWhere As String = ""
            If pDocCod > 0 Then
                auxDocWhere &= " AND cod =" & pDocCod
            End If
            Dim auxSidCod As Integer = pSidCod
            Dim auxAclCod As Integer = pAclCod

            Dim auxDocCod As Integer = pDocCod
            Dim auxDTroles As DataTable = pDTRoles
            'gTRACE_add(auxDocCod, 10, "Proceso chequeo roles:" & pRol.ToString)
            'Busca la lista de logins actuales
            Select Case pRol
                Case enumRoles.coVisualizador
                    Dim auxGroupsList As New List(Of Integer)
                    Dim auxGrpCod As Integer
                    auxGrpCod = m_Security.gGroup_GetCodByID(coGroupIDAdministradores)
                    gDTRoles_Add(auxDTroles, enumRoles.coVisualizador, clsHrcSecurityClient.enumSIDType.coGroup, auxGrpCod, False)

                    auxGrpCod = m_Security.gGroup_GetCodByID(coGroupDocumentadorAdministradores)
                    gDTRoles_Add(auxDTroles, enumRoles.coVisualizador, clsHrcSecurityClient.enumSIDType.coGroup, auxGrpCod, False)

                    auxGrpCod = m_Security.gGroup_GetCodByID(coGroupDocumentadorEditores)
                    gDTRoles_Add(auxDTroles, enumRoles.coVisualizador, clsHrcSecurityClient.enumSIDType.coGroup, auxGrpCod, False)

                    auxGrpCod = m_Security.gGroup_GetCodByID(coGroupDocumentadorVisualizadores)
                    gDTRoles_Add(auxDTroles, enumRoles.coVisualizador, clsHrcSecurityClient.enumSIDType.coGroup, auxGrpCod, False)

                    'Otorga permisos a la lista de unidad especifico a
                    Dim auxUndCod As Integer
                    Dim auxUndList As New List(Of Integer)
                    auxUndCod = m_Conn.gConn_QueryValueInt("SELECT undcod FROM DOC_DOC WHERE cod=" & auxDocCod, -1)
                    auxUndList.Add(auxUndCod)
                    For Each auxRow As DataRow In m_Conn.gConn_Query("SELECT undcod " _
                                                                    & " FROM DOC_DOCUND " _
                                                                    & " WHERE undcod > 0 " _
                                                                    & " AND doccod = " & auxDocCod).Rows
                        auxUndCod = auxRow("undcod")
                        If auxUndList.IndexOf(auxUndCod) = -1 Then
                            auxUndList.Add(auxUndCod)
                        End If
                    Next
                    Dim auxUNDRow As DataRow
                    For Each auxUndCod In auxUndList
                        If auxUndCod > 0 Then
                            auxUNDRow = hrcEntityDT_UND_FindByKey(auxUndCod)
                            If auxUNDRow IsNot Nothing Then
                                If m_Conn.gField_GetBoolean(auxUNDRow("baja"), False) = False Then
                                    auxGrpCod = m_Conn.gField_GetInt(auxUNDRow("miembrosgrpcod"), -1)
                                    If auxGrpCod > 0 Then
                                        gDTRoles_Add(auxDTroles, enumRoles.coVisualizador, enumEntities.coEntityUND, auxGrpCod, False)
                                    Else
                                        gTRACE_add(auxDocCod, 1, "Asignando permiso de lectura-Grupo miembros no existente en unidad:" & auxUNDRow("cod") & "[" & auxUNDRow("dsc") & "]")
                                    End If
                                    If m_Conn.gField_GetBoolean(gSystem_GetParameterByID(coSysParamIDDOCEspecificoA_UndResp), False) Then
                                        'Incluir al responsable de unidad
                                        For Each auxSubUndCod As Integer In gEntity_UND_GetChilds(auxUndCod, True, True)
                                            auxUNDRow = hrcEntityDT_UND_FindByKey(auxSubUndCod)
                                            If auxUNDRow IsNot Nothing Then
                                                If m_Conn.gField_GetBoolean(auxUNDRow("baja"), False) = False Then
                                                    'Busca grupo de responsables
                                                    auxGrpCod = m_Conn.gField_GetInt(auxUNDRow("grpcodresp"), -1)
                                                    If auxGrpCod > 0 Then
                                                        gDTRoles_Add(auxDTroles, enumRoles.coVisualizador, enumEntities.coEntityUND, auxGrpCod, False)
                                                    Else
                                                        gTRACE_add(auxDocCod, 1, "Asignando permiso de lectura-Grupo responsable no existente en unidad:" & auxUNDRow("cod") & "[" & auxUNDRow("dsc") & "]")
                                                    End If
                                                End If
                                            End If
                                        Next
                                    End If
                                End If

                            End If
                        End If

                    Next
                    'Otorgar permiso a los roles agregados
                    gDocPermission_Add(pDocCod:=pDocCod, pDTRoles:=auxDTroles, pACLcod:=auxAclCod, pRol:=pRol, _
                                       pAccessType:=enumAccessType.coSYSGlobalLeer, pAddGroupMembers:=False, pGroupListExcluded:=auxGroupsList)

                Case enumRoles.coImpresor
                    gDocPermission_Add(pDocCod, auxDTroles, auxAclCod, pRol, enumAccessType.coSYSImprimircopiascontroladas, False)
                    'gDocPermission_Add(auxDTroles, auxAclCod, pRol, enumAccessType.coSYSImprimircopiasnocontroladas, False)
                Case Else 'enumRoles.coLector, _
                    'enumRoles.coEditor, enumRoles.coRevisor, enumRoles.coPublicador, enumRoles.coAprobador, enumRoles.coCancelador,
                    'Aplica los roles como firmas
                    Dim auxAccessType As enumAccessType
                    Dim auxWfwstatusinLog As enumWorkflowStep
                    Dim auxWfwstatusSignPendinginLog As New List(Of Integer)    '<--- no utilizar enumworkflowstep, porque gfielddb_getstring no funciona
                    Select Case pRol
                        Case enumRoles.coLector
                            gDocPermission_Add(pDocCod:=pDocCod, pDTRoles:=auxDTroles, pACLcod:=auxAclCod, pRol:=pRol, _
                                     pAccessType:=enumAccessType.coDOCDOCVIGDocumentosvigentesVer, pAddGroupMembers:=False, pGroupListExcluded:=Nothing)

                            auxAccessType = enumAccessType.coSYSConfirmarlectura
                            auxWfwstatusinLog = coWFWSTPDOC_DOCLecturaOK
                            auxWfwstatusSignPendinginLog.Add(coWFWSTPDOC_DOCDocumentovigente)

                        Case enumRoles.coEditor
                            auxAccessType = enumAccessType.coSYSGlobalCambiarestado
                            auxWfwstatusinLog = coWFWSTPDOC_DOCEdicionOK
                            auxWfwstatusSignPendinginLog.Add(coWFWSTPDOC_DOCEdicion)
                            auxWfwstatusSignPendinginLog.Add(coWFWSTPDOC_DOCnuevaversion)
                        Case enumRoles.coRevisor
                            auxAccessType = enumAccessType.coSYSGlobalCambiarestado
                            auxWfwstatusinLog = coWFWSTPDOC_DOCrevisionOK
                            auxWfwstatusSignPendinginLog.Add(coWFWSTPDOC_DOCRevisionSSG)
                        Case enumRoles.coAprobador
                            auxAccessType = enumAccessType.coSYSGlobalCambiarestado
                            auxWfwstatusinLog = coWFWSTPDOC_DOCaprobacionOK
                            auxWfwstatusSignPendinginLog.Add(coWFWSTPDOC_DOCAprobacion)
                        Case enumRoles.coPublicador
                            auxAccessType = enumAccessType.coSYSGlobalCambiarestado
                            auxWfwstatusinLog = coWFWSTPDOC_DOCpublicacionOK
                            auxWfwstatusSignPendinginLog.Add(coWFWSTPDOC_DOCPublicacion)
                        Case enumRoles.coCancelador
                            auxAccessType = enumAccessType.coSYSGlobalCambiarestado
                            auxWfwstatusinLog = -1
                            auxWfwstatusSignPendinginLog.Add(coWFWSTPDOC_DOCCancelacion)
                        Case enumRoles.coReceptor
                            Dim auxGrpCod As Integer
                            auxGrpCod = m_Security.gGroup_GetCodByID(coGroupIDDocumentadorReceptores)
                            gDocPermission_Add(pDocCod:=pDocCod, pDTRoles:=auxDTroles, pACLcod:=auxAclCod, pRol:=pRol, _
                                   pAccessType:=enumAccessType.coSYSGlobalModificar, pAddGroupMembers:=False, pGroupListExcluded:=Nothing)

                            auxAccessType = enumAccessType.coSYSGlobalCambiarestado
                            auxWfwstatusinLog = coWFWSTPDOC_DOCnuevaversion
                            auxWfwstatusSignPendinginLog.Add(coWFWSTPDOC_DOCSolicitudnuevodocumento)
                            auxWfwstatusSignPendinginLog.Add(coWFWSTPDOC_DOCSolicitudnuevaversion)
                            auxWfwstatusSignPendinginLog.Add(coWFWSTPDOC_DOCSolicitudautomaticanuevaversion)
                            auxWfwstatusSignPendinginLog.Add(coWFWSTPDOC_DOCSolicitudeliminacion)
                    End Select
                    Dim auxWhere As String = ""
                    'Busca el último DOCLOG del evento principal. Por ejemplo: EDICION, PUBLICACION
                    Dim auxDocLogCod As Integer
                    auxDocLogCod = m_Conn.gConn_QueryValueInt("SELECT TOP 1 cod FROM DOC_DOCLOG " _
                                                                    & " WHERE doccod =" & auxDocCod _
                                                                    & " AND wfwstepnext IN (" & m_Conn.gFieldDB_GetString(auxWfwstatusSignPendinginLog) & ")" _
                                                                    & " ORDER BY cod DESC", -1)
                    If auxWfwstatusinLog > 0 And auxDocLogCod > 0 Then
                        'Busca los colaboradores que firmaron
                        Dim auxEmpList_HasSigned As New List(Of Integer)
                        Dim auxLoginList_HasSigned As New List(Of Integer)
                        Dim auxEmpList_WithSignPending As New List(Of Integer)
                        Dim auxEmpCod As Integer
                        Dim auxSecCod As Integer
                        Dim auxEmpRow As DataRow
                        For Each auxRowTmp As DataRow In m_Conn.gConn_Query("SELECT empcod" _
                                                                       & " FROM DOC_DOCLOG " _
                                                                       & " WHERE doccod=" & auxDocCod _
                                                                       & " AND wfwstepnext=" & auxWfwstatusinLog _
                                                                       & " AND cod >= " & auxDocLogCod).Rows
                            auxEmpCod = m_Conn.gField_GetInt(auxRowTmp("empcod"), -1)
                            auxEmpRow = hrcEntityDT_EMP_FindByKey(auxEmpCod)
                            If auxEmpRow Is Nothing Then
                                gTRACE_add(auxDocCod, 5, "Error buscando firma.Rol:" & pRol.ToString & ".No existe [" & auxEmpCod & "]")
                            Else
                                If auxEmpList_HasSigned.IndexOf(auxEmpCod) = -1 Then
                                    auxEmpList_HasSigned.Add(auxEmpCod)
                                    auxSecCod = m_Conn.gField_GetInt(auxEmpRow("seccod"), -1)
                                    If auxSecCod > 0 Then
                                        If auxLoginList_HasSigned.IndexOf(auxSecCod) = -1 Then
                                            auxLoginList_HasSigned.Add(auxSecCod)
                                        End If
                                    End If
                                End If
                            End If
                        Next
                        'Lista de firmas
                        Dim auxDTDOCSGN As DataTable = m_Conn.gConn_Query("SELECT cod,empcod " _
                                                                            & " FROM DOC_DOCSGN " _
                                                                            & " WHERE doccod=" & auxDocCod _
                                                                            & " AND doclogcod=" & auxDocLogCod)
                        For Each auxRowTmp As DataRow In auxDTDOCSGN.Rows
                            auxEmpCod = m_Conn.gField_GetInt(auxRowTmp("empcod"), -1)
                            If auxEmpCod > 0 Then
                                If auxEmpList_WithSignPending.IndexOf(auxEmpCod) = -1 Then
                                    auxEmpList_WithSignPending.Add(auxEmpCod)
                                End If
                            End If
                        Next


                        'Asigna los permisos,EXCEPTO los que ya firmaron y sin borrar los que sobran
                        gDocPermission_Add(pDocCod:=pDocCod, pDTRoles:=auxDTroles, pACLcod:=auxAclCod, pRol:=pRol, _
                                           pAccessType:=auxAccessType, pAddGroupMembers:=True, _
                                           pLoginListExcluded:=auxLoginList_HasSigned)
                        'Busca los logins que deberían firmar de acuerdo a la plantilla
                        Dim auxEmpList_MustSign As New List(Of Integer)
                        Dim auxRows() As DataRow
                        For Each auxLoginRow As DataRow In m_Security.gSID_ResolveLoginsByAccessType(auxSidCod, auxAccessType).Rows
                            auxRows = hrcEntityDT_EMP.Select("seccod=" & auxLoginRow("seccod"))
                            If auxRows.Count <> 0 Then
                                auxEmpCod = auxRows(0)("cod")
                                If auxEmpCod > 0 Then
                                    If auxEmpList_MustSign.IndexOf(auxEmpCod) = -1 Then
                                        auxEmpList_MustSign.Add(auxEmpCod)
                                    End If
                                End If
                            End If
                        Next

                        ''Busca las firmas pendientes
                        auxWhere = ""
                        For Each auxEmpCod In auxEmpList_MustSign
                            If auxEmpList_HasSigned.IndexOf(auxEmpCod) = -1 And auxEmpList_WithSignPending.IndexOf(auxEmpCod) = -1 Then
                                auxEmpList_WithSignPending.Add(auxEmpCod)
                                'Agrega si no tiene la firma
                                auxEmpRow = hrcEntityDT_EMP_FindByKey(auxEmpCod)
                                If auxEmpRow Is Nothing Then
                                    gTRACE_add(auxDocCod, 5, "Error agrega colaborador.Rol:" & pRol.ToString & ".No existe [" & auxEmpCod & "]")
                                Else
                                    If m_Conn.gField_GetBoolean(auxEmpRow("baja"), False) = False Then
                                        gTRACE_add(auxDocCod, 10, "Agrega colaborador.Rol:" & pRol.ToString & "." & auxEmpRow("cod") & "[" & auxEmpRow("dsc") & "]")
                                        gEntity_DOC_DOCSGN_Insert(pdoccod:=auxDocCod, pempcod:=auxEmpCod, _
                                                                        pfechainicio:=m_Conn.gDate_GetToday, _
                                                                        pavisoscant:="0", _
                                                                        pfechaultmail:=Nothing, pultmailobs:=Nothing, _
                                                                        pdoclogcod:=auxDocLogCod, pmailscod:=1)
                                    End If
                                End If
                            End If
                        Next

                        'Quita los logins que no deben tener firma/permiso
                        For Each auxRowTmp As DataRow In auxDTDOCSGN.Rows
                            auxEmpCod = m_Conn.gField_GetInt(auxRowTmp("empcod"), -1)
                            If auxEmpCod > 0 Then
                                'Quita si no tiene obligación de firmar o ya firmó
                                If auxEmpList_MustSign.IndexOf(auxEmpCod) = -1 Or auxEmpList_HasSigned.IndexOf(auxEmpCod) <> -1 Then
                                    auxEmpRow = hrcEntityDT_EMP_FindByKey(auxEmpCod)
                                    If auxEmpRow Is Nothing Then
                                        gTRACE_add(auxDocCod, 5, "Error elimina colaborador.Rol:" & pRol.ToString & ".No existe [" & auxEmpCod & "]")
                                    Else
                                        gTRACE_add(auxDocCod, 10, "Elimina colaborador.Rol:" & pRol.ToString & "." & auxEmpCod & "[" & auxEmpRow("dsc") & "]")
                                        gEntity_DOC_DOCSGN_Delete(pcod:=auxRowTmp("cod"))
                                        auxSecCod = m_Conn.gField_GetInt(auxEmpRow("seccod"), -1)
                                        If auxSecCod > 0 Then
                                            m_Security.gACL_DelLogin(auxSidCod, auxEmpRow("seccod"), auxAccessType)
                                        End If
                                    End If
                                End If
                            End If
                        Next
                    End If

            End Select

        Catch ex As Exception
            gTRACE_add(-1, 1, "Excepción en chequeo de nuevos roles[" & pRol.ToString & "]:" & ex.Message)
        End Try
        'gTRACE_add(-1, 10, "Proceso chequeo nuevos roles-fin")
    End Sub
    Public Function gDoc_ReIDCompare(ByVal pVigente As Boolean) As DataTable
        'Si extradata=true entonces devuelve el tipo de entidad y dsc del doc
        Dim auxReturn As DataTable
        Dim auxSelect As String = ""
        If pVigente Then
            auxSelect = "SELECT DOC_DOC.cod,DOC_DOC.nro,DOC_DOC.version,DOC_DOC.wfwstatus,DOC_DOC.doctipcod,DOC_DOC.undcod,DOC_DOC.identificador,DOC_PRO.apacod" _
                                       & ",'' as new_identificador," & enumEntities.coEntityDOC_DOC & " as q_type,DOC_DOC.dsc" _
                                       & " FROM DOC_DOCVIG AS DOC_DOC" _
                                       & " LEFT JOIN DOC_PRO ON DOC_DOC.procod=DOC_PRO.cod " _
                                       & " LEFT JOIN DOC_APA ON DOC_PRO.apacod=DOC_APA.cod " _
                                       & " WHERE DOC_DOC.cod > 0 "
        Else
            auxSelect = "SELECT DOC_DOC.cod,DOC_DOC.nro,DOC_DOC.version,DOC_DOC.wfwstatus,DOC_DOC.doctipcod,DOC_DOC.undcod,DOC_DOC.identificador,DOC_PRO.apacod" _
                                        & ",'' as new_identificador," & enumEntities.coEntityDOC_DOC & " as q_type,DOC_DOC.dsc" _
                                        & " FROM DOC_DOC" _
                                        & " LEFT JOIN DOC_PRO ON DOC_DOC.procod=DOC_PRO.cod " _
                                        & " LEFT JOIN DOC_APA ON DOC_PRO.apacod=DOC_APA.cod " _
                                        & " WHERE DOC_DOC.cod > 0 " _
                                        & " AND DOC_DOC.wfwstatus > 0" _
                                        & " AND (DOC_DOC.baja = {#FALSE#} OR DOC_DOC.baja {#ISNULL#})"
        End If
        auxReturn = m_Conn.gConn_Query(auxSelect)
        auxReturn.Columns.Add(New DataColumn("wfwstpdsc", System.Type.GetType("System.String")))
        For Each auxRow As DataRow In auxReturn.Rows
            If pVigente Then
                auxRow("wfwstpdsc") = hrcEntityDT_Q_WFWSTP_FindByKey(enumWorkflowStep.coWFWSTPDOC_DOCDocumentovigente)("wfwstpdsc")
            Else
                auxRow("wfwstpdsc") = hrcEntityDT_Q_WFWSTP_FindByKey(auxRow("wfwstatus"))("wfwstpdsc")
            End If
            auxRow("new_identificador") = gDoc_GetIdentificador(auxRow("doctipcod") _
                                                                , m_Conn.gField_GetInt(auxRow("undcod")) _
                                                                , m_Conn.gField_GetInt(auxRow("apacod")) _
                                                                , m_Conn.gField_GetInt(auxRow("nro")) _
                                                                , m_Conn.gField_GetInt(auxRow("version")))
        Next

        Return auxReturn
    End Function
    Private Function gDOC_AutoPublicacion() As String
        Dim auxFecha As Date = Today.AddDays(1)
        gTRACE_add(-1, 10, "AutoPublicacion-start:" & auxFecha)
        Dim auxResult As String = ""
        For Each auxRow As DataRow In m_Conn.gConn_Query("SELECT DOC_DOC.cod" _
                                                 & ",DOC_DOC.publicacionauto " _
                                                 & " FROM DOC_DOC " _
                                                 & " WHERE DOC_DOC.cod > 0 " _
                                                 & " AND (DOC_DOC.baja = {#FALSE#} OR DOC_DOC.baja {#ISNULL#}) " _
                                                 & " AND DOC_DOC.publicacionauto < " & m_Conn.gFieldDB_GetDateTime(auxFecha) _
                                                 & " AND DOC_DOC.publicacionauto > " & m_Conn.gFieldDB_GetDateTime(New Date(2000, 1, 1)) _
                                                 & " AND DOC_DOC.wfwstatus=" & enumWorkflowStep.coWFWSTPDOC_DOCPublicacion).Rows
            gTRACE_add(auxRow("cod"), 10, "Autopublicacion del documento")
            Dim auxValues As New clshrcBagValues()
            auxValues.gValue_Add("Cod", auxRow("cod"))
            auxValues.gValue_Add("force", "1")
            auxValues.gValue_Add("gotostep", enumWorkflowStep.coWFWSTPDOC_DOCpublicacionOK)
            auxResult = gWorkflow_GotoStep_EXE(auxValues, 1)
        Next
        gTRACE_add(-1, 10, "AutoPublicacion-end")
    End Function
    Private Function gDOC_AutoEdition() As String
        Dim auxParamSysDocAutoEditionDaysCicle As Integer = m_Conn.gField_GetInt(gSystem_GetParameterByID(coSysParamIDDOCAutoEditionDaysCicle), 0)
        'If auxDocAutoEditionDaysCicle < 1 Then
        '    gSys_DebugLogAdd("Autoedicion deshabilitada= ciclo menor a 1")
        '    Exit Function
        'End If

        gTRACE_add(-1, 10, "Autoedición-start:" & auxParamSysDocAutoEditionDaysCicle)
        Dim auxResult As String = ""
        Dim auxFecha As Date
        Dim auxDocEditionEnabled As Boolean
        Dim auxDocAutoEditionDaysCicle As Integer
        Dim auxToday As Date = m_Conn.gDate_GetToday
        Dim auxDTTemp As DataTable
        For Each auxRow As DataRow In m_Conn.gConn_Query("SELECT DOC_DOCVIG.cod" _
                                                         & ",DOC_DOCVIG.fecha " _
                                                         & ",DOC_DOCTIP.autoeditionenabled,DOC_DOCTIP.autoeditiondayscicle" _
                                                         & " FROM DOC_DOCVIG " _
                                                         & " LEFT JOIN DOC_DOC ON DOC_DOCVIG.cod=DOC_DOC.cod" _
                                                         & " LEFT JOIN DOC_DOCTIP ON DOC_DOCVIG.doctipcod=DOC_DOCTIP.cod" _
                                                         & " WHERE DOC_DOCVIG.cod > 0 " _
                                                         & " AND (DOC_DOCTIP.autoeditionenabled = {#TRUE#} OR DOC_DOCTIP.autoeditionenabled {#ISNULL#}) " _
                                                         & " AND DOC_DOC.wfwstatus=" & enumWorkflowStep.coWFWSTPDOC_DOCDocumentovigente).Rows
            'gTRACE_add(auxRow("cod"), 10, "Autoedición del documento-Análisis de nueva versión")
            auxDocEditionEnabled = False
            If IsDBNull(auxRow("autoeditionenabled")) Then
                If auxParamSysDocAutoEditionDaysCicle > 0 Then
                    auxDocEditionEnabled = True
                End If
            Else
                auxDocEditionEnabled = auxRow("autoeditionenabled")
            End If
            If auxDocEditionEnabled Then
                auxFecha = m_Conn.gField_GetDate(auxRow("fecha"), auxToday)
                auxDocAutoEditionDaysCicle = 0
                If IsDBNull(auxRow("autoeditiondayscicle")) Then
                    auxDocAutoEditionDaysCicle = auxParamSysDocAutoEditionDaysCicle
                Else
                    auxDocAutoEditionDaysCicle = auxRow("autoeditiondayscicle")
                End If
                If auxDocAutoEditionDaysCicle > 0 _
                And auxFecha.AddDays(auxDocAutoEditionDaysCicle) < auxToday Then
                    'Autoedicion habilitado + la ult. fecha vigente pasa el ciclo
                    'Analisis si no fue rechazada la nueva version
                    auxDTTemp = m_Conn.gConn_Query("SELECT TOP 1 fecha " _
                                                & " FROM DOC_DOCLOG " _
                                                & " WHERE doccod=" & auxRow("cod") _
                                                & " AND wfwstepnext IN (" & enumWorkflowStep.coWFWSTPDOC_DOCSolicitudnuevaversion _
                                                    & "," & enumWorkflowStep.coWFWSTPDOC_DOCSolicitudautomaticanuevaversion _
                                                    & "," & enumWorkflowStep.coWFWSTPDOC_DOCVersionanulada _
                                                    & "," & enumWorkflowStep.coWFWSTPDOC_DOCnuevaversion _
                                                    & "," & enumWorkflowStep.coWFWSTPDOC_DOCnuevaversionNOK _
                                                    & ")" _
                                                & " ORDER BY cod DESC")
                    If auxDTTemp.Rows.Count <> 0 Then
                        If m_Conn.gField_GetDate(auxDTTemp.Rows(0)("fecha")) > auxFecha Then
                            auxFecha = m_Conn.gField_GetDate(auxDTTemp.Rows(0)("fecha"))
                        End If
                    End If
                    If auxFecha.AddDays(auxDocAutoEditionDaysCicle) < auxToday Then
                        'El ultimo rechazo es anterior al ciclo
                        gTRACE_add(auxRow("cod"), 10, "Autoedición del documento-Nueva versión:" & m_Conn.gField_GetDate(auxRow("fecha")).ToString("d/M/yyyy") & ".Días de tolerancia:" & auxDocAutoEditionDaysCicle)
                        Dim auxValues As New clshrcBagValues()
                        auxValues.gValue_Add("Cod", auxRow("cod"))
                        auxValues.gValue_Add("gotostep", enumWorkflowStep.coWFWSTPDOC_DOCSolicitudautomaticanuevaversion)
                        auxResult = gWorkflow_GotoStep_EXE(auxValues, 1)
                    End If
                End If
            End If
        Next
        gTRACE_add(-1, 10, "Autoedición-end")
        Return auxResult
    End Function
    Private Function gDoc_ReIDAuto() As String
        gSys_DebugLogAdd("Reidentificación-start")
        Dim auxDOCReidAuto As Boolean = m_Conn.gField_GetBoolean(gSystem_GetParameterByID(coSysParamIDDOCReIDAuto), False)
        If auxDOCReidAuto = False Then
            gSys_DebugLogAdd("Reidentificación automática deshabilitada")
            Exit Function
        End If
        Dim auxIdentificador As String = ""
        Dim auxResult As String = ""
        Dim auxObs As String
      
        For Each auxRow As DataRow In gDoc_ReIDCompare(False).Rows
            auxIdentificador = auxRow("new_identificador").ToString
            If auxIdentificador = "" Then
                gAlert_SendToDev("Indentificador invalido", "Identificador en blanco para el documento:" & auxRow("cod"), clsHrcAlertClient.enumLevel.coError)
            Else
                If auxIdentificador <> auxRow("identificador") Then
                    Select Case auxRow("wfwstatus")
                        Case enumWorkflowStep.coWFWSTPDOC_DOCDocumentovigente
                            gTRACE_add(auxRow("cod"), 10, "Reidentificación automática-Nueva versión")
                            Dim auxValues As New clshrcBagValues()
                            auxValues.gValue_Add("Cod", auxRow("cod"))
                            auxValues.gValue_Add("gotostep", enumWorkflowStep.coWFWSTPDOC_DOCReidentificacion)
                            auxResult = gWorkflow_GotoStep_EXE(auxValues, 1)
                            'Case enumWorkflowStep.coWFWSTPDOC_DOCEdicion, enumWorkflowStep.coWFWSTPDOC_DOCRevisionSSG
                        Case Else
                            gTRACE_add(auxRow("cod"), 10, "Reidentificación automática-Solo cambio de ID. Estado:" & auxRow("wfwstatus"))
                            auxObs = auxRow("identificador").ToString & "->" & auxIdentificador
                            gDOCLOG_Add("", auxObs, auxRow("cod"), enumWorkflowStep.coWFWSTPDOC_DOCReidentificacion, -1, False, -1, hrcEntityDT_Q_WFWSTP_FindByKey(enumWorkflowStep.coWFWSTPDOC_DOCReidentificacion)("wfwstpdsc").ToString, -1, -1, -1, -1)
                            gEntity_DOC_DOC_SystemUpdate(pcod:=auxRow("cod"), pidentificador:=auxIdentificador)
                            ' gEntity_DOC_DOCVIG_SystemUpdate(pcod:=auxRow("cod"), pidentificador:=auxIdentificador)
                    End Select
                End If

            End If
        Next
        gSys_DebugLogAdd("Reidentificación-end")
    End Function
    Public Function gDOC_SendMails() As String
        Return gDOC_SendMails(-1)
    End Function
    Public Function gDOC_SendMails(ByVal pCod As Integer) As String
        Dim auxReturn As String = ""

        Try
            gTRACE_add(pCod, 10, "Inicio proceso envío de alertas")
            If m_Alerts Is Nothing Then
                gTRACE_add(pCod, 1, "Servicio de alertas no instanciado!")
                Return "Servicio de alertas no activo!"
                Exit Function
            End If

            Dim auxWhere As String = ""
            If pCod > 0 Then
                auxWhere &= " AND DOC_DOCSGN.doccod=" & pCod
            End If
            Dim auxToday As Date = m_Conn.gDate_GetToday
            Dim auxQueueList As New List(Of Integer)
            Dim auxFechaControlEnvios As Date = auxToday.AddDays(-Val(gSystem_GetParameterByID(coSysParamIDAvisosDias)))
            Dim auxAvisosMax As Integer = gSystem_GetParameterByID(coSysParamIDAvisosCantMax)
            Dim auxDT As DataTable = m_Conn.gConn_Query("SELECT DOC_DOCSGN.cod,DOC_DOCSGN.avisoscant,DOC_DOCSGN.doccod,DOC_DOCSGN.empcod,DOC_DOCSGN.fechainicio" _
                                                        & ",EMP.mail as emp_mail, EMP.alequecod" _
                                                        & ",DOC_DOC.dsc as doc_dsc,DOC_DOC.version as doc_version,DOC_DOC.nro" _
                                                        & ",DOC_DOCLOG.wfwstepnext as doclog_wfwstepnext,DOC_DOCLOG.obs as doclog_obs" _
                                                        & ",MAILS.content as mails_content,MAILS.subject AS mails_subject " _
                                                  & " FROM DOC_DOCSGN " _
                                                  & " LEFT JOIN EMP ON EMP.cod=DOC_DOCSGN.empcod " _
                                                  & " LEFT JOIN DOC_DOCLOG ON DOC_DOCLOG.cod=DOC_DOCSGN.doclogcod " _
                                                  & " LEFT JOIN DOC_DOC ON DOC_DOC.cod=DOC_DOCLOG.doccod " _
                                                  & " LEFT JOIN MAILS ON MAILS.cod=DOC_DOCSGN.mailscod " _
                                                  & " WHERE DOC_DOCSGN.cod > 0 " & auxWhere _
                                                  & " AND (DOC_DOCSGN.fechaultmail IS NULL OR DOC_DOCSGN.fechaultmail <= " & m_Conn.gFieldDB_GetDateTime(auxFechaControlEnvios) & ")" _
                                                  & " AND DOC_DOCSGN.avisoscant <" & auxAvisosMax)

            Dim auxSiteURL As String = m_Conn.gField_GetString(coExternalURL)
            If Right(auxSiteURL, 1) <> "/" Then
                auxSiteURL &= "/"
            End If

            Dim auxSystemName As String = coSystemTitle
            Dim auxMail As New imMailing(coSMTPServer, coSMTPUser, coSMTPpsw, coSMTPfrom, Val(coSMTPport), coSystemTitle)
            auxMail.SMTPenableSSL = m_Conn.gField_GetBoolean(coSMTPSSLEnabled, False)
            Dim auxContent As String = ""
            Dim auxSubject As String = ""
            Dim auxError As String = ""
            Dim auxNow As Date = m_Conn.gDate_GetNow
            For Each auxRow As DataRow In auxDT.Rows
                auxMail.gMail_NewMail()
                Dim auxDocCod As Integer = m_Conn.gField_GetInt(auxRow("doccod"))
                Dim auxMailTo As String = m_Conn.gField_GetString(auxRow("emp_mail"))
                Dim auxultmailobs As String = ""
                Dim auxTraceDsc As String = ""
                If auxMail.gTo_Add(auxMailTo) Then
                    Dim auxWfwStpCod As Integer = auxRow("doclog_wfwstepnext")
                    Dim auxwfwStpDsc As String = ""
                    If auxWfwStpCod > 0 Then
                        auxwfwStpDsc = hrcEntityDT_Q_WFWSTP.Select("wfwstpcod=" & auxWfwStpCod)(0)("wfwstpdsc")
                    End If
                    Select Case auxWfwStpCod
                        Case enumWorkflowStep.coWFWSTPDOC_DOCDocumentovigente
                            auxwfwStpDsc = "Lectura pendiente"
                        Case enumWorkflowStep.coWFWSTPDOC_DOCSolicitudautomaticanuevaversion
                            auxwfwStpDsc = gSystem_GetParameterByID(coSysParamIDDocAutoEditionAlertSubject)
                            If auxwfwStpDsc = "" Then
                                auxwfwStpDsc = "Solicitud automática nueva versión"
                            End If
                        Case enumWorkflowStep.coWFWSTPDOC_DOCCreacion
                            auxwfwStpDsc = "Se ha creado el documento " & auxRow("NRO")
                    End Select

                    Dim auxText As String = m_Conn.gField_GetString(auxRow("doc_dsc"), "") & "(" & auxwfwStpDsc & ")"
                    auxText = auxText.Replace(Chr(10), " ")

                    auxSubject = m_Conn.gField_GetString(auxRow("mails_subject"), "")
                    auxSubject = auxSubject.Replace("{#SISTEMA_NOMBRE#}", auxSystemName)
                    auxSubject = auxSubject.Replace("{#TEXT#}", auxText)
                    auxSubject = auxSubject.Replace("{#SYSTEM.TITLE#}", coSystemTitle)
                    auxSubject = auxSubject.Replace("{#DOCSGN.FECHAINICIO#}", m_Conn.gField_GetDate(auxRow("fechainicio")).ToString("d/M/yyyy"))
                    auxSubject = auxSubject.Replace("{#DOC.DSC#}", m_Conn.gField_GetString(auxRow("doc_dsc")))
                    auxSubject = auxSubject.Replace("{#DOC.VERSION#}", m_Conn.gField_GetString(auxRow("doc_version")))

                    Select Case auxWfwStpCod
                        Case enumWorkflowStep.coWFWSTPDOC_DOCpublicacionOK
                            auxSubject = auxSubject.Replace("{#DOC_LINK#}", auxSiteURL & "cfrmdocumentos_impresion.aspx?_mode_=8&param1=" & auxDocCod)
                        Case Else
                            auxSubject = auxSubject.Replace("{#DOC_LINK#}", auxSiteURL & "cfrmdocumentos1_det.aspx?_mode_=0&param1=" & auxDocCod)
                    End Select

                    auxSubject = auxSubject.Replace("{#DOC.WFWSTPNEXTDSC#}", auxwfwStpDsc)

                    auxContent = m_Conn.gField_GetString(auxRow("mails_content").ToString, "")
                    'auxContent = auxContent.Replace("{#MAIL.TEXT#}", pText)
                    auxContent = auxContent.Replace("{#TEXT#}", auxText)
                    auxContent = auxContent.Replace("{#SYSTEM.TITLE#}", coSystemTitle)
                    auxContent = auxContent.Replace("{#DOCSGN.FECHAINICIO#}", m_Conn.gField_GetString(auxRow("fechainicio")))
                    auxContent = auxContent.Replace("{#DOC.DSC#}", m_Conn.gField_GetString(auxRow("doc_dsc")))
                    auxContent = auxContent.Replace("{#DOC.VERSION#}", m_Conn.gField_GetString(auxRow("doc_version")))
                    Select Case auxWfwStpCod
                        Case enumWorkflowStep.coWFWSTPDOC_DOCpublicacionOK
                            auxContent = auxContent.Replace("{#DOC_LINK#}", auxSiteURL & "cfrmdocumentos_impresion.aspx?_mode_=8&param1=" & auxDocCod)
                        Case Else
                            auxContent = auxContent.Replace("{#DOC_LINK#}", auxSiteURL & "cfrmdocumentos1_det.aspx?_mode_=0&param1=" & auxDocCod)
                    End Select


                    auxContent = auxContent.Replace("{#DOC.WFWSTPNEXTDSC#}", auxwfwStpDsc)
                    Dim auxDocLogObs As String = ""
                    If auxRow("doclog_obs").ToString <> "" Then
                        auxDocLogObs &= "Comentarios:" & m_Conn.gField_GetString(auxRow("doclog_obs"))
                    End If
                    auxContent = auxContent.Replace("{#DOCLOG.OBS#}", auxDocLogObs)
                    auxContent = auxContent.Replace("{#AVISOS_MAX#}", auxAvisosMax)
                    Dim auxAvisosCant As Integer = m_Conn.gField_GetInt(auxRow("avisoscant"), 0) + 1
                    auxContent = auxContent.Replace("{#AVISOS_CANT#}", auxAvisosCant)

                    auxTraceDsc &= "Mail a [" & auxMailTo & "].empcod[" & auxRow("empcod") & "].Asunto [" & auxSubject & "].Cuerpo [" & auxContent & "]."
                    gTRACE_add(pCod, 1, "Enviando a servicio de alertas!")
                    Dim auxAlertOptions As New clshrcBagValues
                    If m_mailDisabled Then
                        auxAlertOptions.gValue_Add("MAILDISABLED", True)
                    End If
                    auxQueueList.Clear()
                    auxQueueList.Add(auxRow("alequecod"))
                    m_Alerts.gRoutes_InsertAlert(auxSubject, auxContent, clsHrcAlertClient.enumLevel.coInfo, clsHrcAlertClient.enumConfirmMode.coNoConfirm, 1, _
                                       auxQueueList, Nothing, auxAlertOptions)
                    gTRACE_add(pCod, 1, "Fin generación alertas")
                    If auxMail.gMail_Send(auxSubject, auxContent) Then
                        auxTraceDsc &= "Enviado!!!(" & auxMail.LastErrorDescription & ")"
                        gEntity_DOC_DOCSGN_Update(pcod:=auxRow("cod"), pavisoscant:=auxAvisosCant, pdoccod:=Nothing, pfechainicio:=Nothing, pfechaultmail:=auxNow, pempcod:=Nothing, pultmailobs:=auxultmailobs)
                        Dim auxDTSgn As DataTable = m_Conn.gConn_Query("SELECT " _
                                    & " (SELECT COUNT(*) FROM DOC_DOCSGN WHERE doccod=" & auxDocCod & " AND avisoscant >= " & auxAvisosMax & ")," _
                                    & " (SELECT COUNT(*) FROM DOC_DOCSGN WHERE doccod=" & auxDocCod & " AND avisoscant  < " & auxAvisosMax & ")")
                        Dim auxOrden As String = ""
                        If auxDTSgn.Rows(0)(1) = 0 And auxDTSgn.Rows(0)(0) <> 0 Then
                            'son todos avisos excedidos. Orden=10
                            gTRACE_add(auxDocCod, 1, "Avisos excedidos")
                            auxOrden = "10"
                        ElseIf auxDTSgn.Rows(0)(1) <> 0 And auxDTSgn.Rows(0)(0) <> 0 Then
                            'hay más de un aviso excedido, pero no todos
                            auxOrden = "25"
                        ElseIf auxDTSgn.Rows(0)(1) <> 0 And auxDTSgn.Rows(0)(0) = 0 Then
                            'hay avisos NO EXCEDIDOS
                            auxOrden = (46 - auxDTSgn.Rows(0)(1)).ToString
                        End If
                        If auxOrden <> "" Then
                            m_Conn.gConn_Update("UPDATE DOC_DOC SET orden=" & auxOrden & " WHERE cod=" & auxDocCod)
                        End If
                    Else
                        auxultmailobs = auxNow.ToString("d/M/yyyy HH:mm:ss") & "-" & auxMail.LastErrorDescription
                        auxTraceDsc &= "Error al enviar mail [" & auxMail.LastErrorDescription & "]."
                    End If
                Else
                    auxTraceDsc &= "Dirección de mail inválida o ya fue agregada [" & auxMailTo & "]."
                    auxultmailobs = "Dirección de mail inválida"
                End If
                gTRACE_add(auxDocCod, 1, auxTraceDsc)
                If auxultmailobs <> "" Then
                    gEntity_DOC_DOCSGN_Update(pcod:=auxRow("cod"), pfechaultmail:=auxNow, pultmailobs:=auxultmailobs)
                End If
            Next

            gTRACE_add(-1, 1, "Fin proceso envío de mail de avisos")
        Catch ex As Exception
            gTRACE_add(-1, 1, "Excepcion en proceso envío de mail.Error:" & ex.Message)
            auxReturn = ex.Message
        End Try
        Return auxReturn
    End Function
    Private Sub gDOCSGN_Delete(ByVal pDocCod As Integer, _
                               ByVal pWfwStpCod As Integer, _
                               ByVal pEmpCod As Integer, _
                               ByVal pWfwStpCod_Exclusion As Integer)
        Dim auxWhere As String = ""
        If pWfwStpCod <> -1 Then
            auxWhere &= " AND doclogcod IN (" _
                         & "SELECT cod FROM DOC_DOCLOG WHERE doccod=" & pDocCod & " AND wfwstepnext = " & pWfwStpCod _
                         & ")"
        End If
        If pWfwStpCod_Exclusion > 0 Then
            auxWhere &= " AND doclogcod NOT IN (" _
                         & "SELECT cod FROM DOC_DOCLOG WHERE doccod=" & pDocCod & " AND wfwstepnext = " & pWfwStpCod_Exclusion _
                         & ")"
        End If
        If pEmpCod <> -1 Then
            auxWhere &= " AND empcod=" & pEmpCod
        End If
        m_Conn.gConn_Delete("DELETE FROM DOC_DOCSGN " _
                            & " WHERE doccod=" & pDocCod _
                            & auxWhere)
    End Sub
    Private Function gDOCLOG_Add(ByVal pDsc As String, _
                                 ByVal pObs As String, _
                           ByVal pDocCod As Integer, _
                           ByVal pWfwStpCod As Integer, _
                           ByVal phsthidgencod As Integer, _
                           ByVal pRequiredSignature As Boolean, _
                           ByVal pWfwStpCurrent As Integer, _
                           ByVal pWfwStpNextDsc As String, _
                           ByVal pDocSidCod As Integer, _
                           ByVal pAccessToSignature As enumAccessType, _
                           ByVal pEmpCod As Integer, _
                           ByVal pDelEmpCod As Integer) As Boolean
        'Si requiere firma devuelve TRUE solo si hay por lo menos una firma. 
        'Si no requiere firma y termina bien, devuelve TRUE
        Dim auxReturn As Boolean = False
        Try
            Dim auxDocSgnList As New List(Of Integer)
            Dim auxWfwStpNextDsc As String = pWfwStpNextDsc
            If pWfwStpCod = coWFWSTPDOC_DOCDocumentovigente Then
                auxWfwStpNextDsc = "Lectura"
                'Si es vigente siempre esta OK
                auxReturn = True
            End If
            Dim auxRows() As DataRow
            Dim auxEmpCodList As New List(Of Integer)
            If pRequiredSignature Then
                For Each auxRow As DataRow In m_Security.gSID_ResolveLoginsByAccessType(pDocSidCod, pAccessToSignature, False).Rows
                    auxRows = hrcEntityDT_EMP.Select("seccod=" & auxRow(0))
                    If auxRows.Count <> 0 Then
                        Dim auxEmpCod As Integer = auxRows(0)("cod")
                        If auxEmpCod > 0 And auxEmpCodList.IndexOf(auxEmpCod) = -1 Then
                            auxDocSgnList.Add(gEntity_DOC_DOCSGN_Insert(pdoccod:=pDocCod, pempcod:=auxEmpCod, pfechainicio:=m_Conn.gDate_GetNow, pavisoscant:="0", _
                                                                        pfechaultmail:=Nothing, pultmailobs:=Nothing, pdoclogcod:=-1, pmailscod:=1))
                            auxEmpCodList.Add(auxEmpCod)
                        End If
                        auxReturn = True
                    End If
                Next
            Else
                auxReturn = True
            End If
            If auxReturn Then
                Dim auxDocLogCod As Integer = gEntity_DOC_DOCLOG_Insert(pdoccod:=pDocCod, _
                 pdsc:=pDsc, _
                 pobs:=pObs, _
                 pwfwstpprev:=pWfwStpCurrent, _
                 pwfwstepnext:=pWfwStpCod, _
                 pempcod:=pEmpCod, _
                 pdelempcod:=pDelEmpCod, _
                 pfecha:=m_Conn.gDate_GetNow, _
                 phsthidgencod:=phsthidgencod)
                If auxDocSgnList.Count <> 0 Then
                    m_Conn.gConn_Update("UPDATE DOC_DOCSGN " _
                                        & " SET doclogcod=" & auxDocLogCod _
                                        & " WHERE cod IN (" & m_Conn.gFieldDB_GetString(auxDocSgnList.ToArray) & ")")
                End If
            End If
        Catch ex As Exception
            'gPro_TraceAdd(pProCod, 1, "Excepción envío de mail:" & ex.Message)
        End Try
        Return auxReturn
    End Function

    Public Overrides Function gWS_ArriveMessage(ByVal pData As String) As String
        Dim auxReturn As String = ""
        Try
            Select Case pData
                Case "REPLICATE"
                    gReplication_Start()
                Case Else
                    Dim auxComMsg As New Intelimedia.Hercules.Communications.clshrcCommunications.clsHrcComMsg(pData)

                    gTRACE_add(-1, 10, "Arrive message[" & pData & "]")
                    Select Case auxComMsg.Method
                        Case 89 'Reporte de imagenes
                            Dim auxContent As String = auxComMsg.Content.ToString
                            gTRACE_add(-1, 10, "Length:" & auxComMsg.Content.ToString.Length & "." & Mid(auxContent, 1, 20))
                            Dim auxReport As Intelimedia.Hercules.Communications.clshrcCommunications.clsReport
                            auxReport = (New Intelimedia.Hercules.Communications.clshrcCommunications).gMessage_GetReport(auxContent)
                            gTRACE_add(-1, 10, "Arrive report 89 [" & auxReport.ReportTable.Rows.Count & "]")
                            Dim auxBinaryData As clsBinaryData
                            Dim auxImageCod As Integer = -1
                            For Each auxRow As DataRow In auxReport.ReportTable.Rows
                                Dim auxUserName As String = auxRow("sAMAccountName").ToString.Trim
                                Dim auxImagejpeg As Byte() = Convert.FromBase64String(auxRow("jpegphoto"))
                                If auxImagejpeg.Count <> 0 And auxUserName <> "" Then
                                    Dim auxRows() As DataRow = hrcEntityDT_EMP.Select("cod > 0 " _
                                                & " AND (BAJA IS NULL or BAJA = 0 ) " _
                                                & " AND (empusername='" & auxUserName & "' " _
                                                & " OR empusername LIKE '%\" & auxUserName & "')")
                                    If auxRows.Count = 0 Then
                                        gTRACE_add(-1, 10, "Importing user photo. Not exist user[" & auxUserName & "]")
                                    Else
                                        'Recorre todas por si hay varios dominios con el mismo usuario
                                        For Each auxEmpRow As DataRow In auxRows
                                            auxBinaryData = New clsHrcConnClient.clsBinaryData(enumMimeTypes.coJPG, "foto.jpg", auxImagejpeg)
                                            auxImageCod = m_Conn.gField_GetInt(auxEmpRow("imagencod"), -1)
                                            If auxImageCod < 1 Then
                                                auxImageCod = m_Conn.gConn_ImageToBLOB(auxBinaryData.Filename, auxBinaryData.Content, 128, 128, -1, 64, 64)
                                                gEntity_EMP_SystemUpdate(pcod:=auxEmpRow("cod"), pimagencod:=auxImageCod)
                                            Else
                                                m_Conn.gConn_ImageToBLOB(auxBinaryData.Filename, auxBinaryData.Content, 128, 128, auxImageCod, 64, 64)
                                            End If
                                        Next
                                    End If
                                End If
                            Next
                        Case -1
                            Dim auxBagValues As New clshrcBagValues(auxComMsg.Content.ToString)
                            Dim auxMethod As String = auxBagValues.gValue_Get("EVENT_NAME").ToString.ToLower
                            gTRACE_add(-1, 10, "Method:" & auxMethod & ".BagValues:[" & auxComMsg.Content & "]. Values count:" & auxBagValues.Values.Count)
                            auxBagValues = hrcEvents.gListen_Receive(auxBagValues)
                            Select Case auxMethod
                                Case "sec_session_start"
                                    Dim auxSecDsc As String = auxBagValues.gValue_Get("SECDSC").ToString.ToLower
                                    Dim auxSesID As String = m_Security.gLogin_CreateDelegatedSessionToSystem(auxSecDsc)
                                    auxBagValues.gValues_Clear()
                                    auxBagValues.gValue_Add("SESID", auxSesID)
                                Case "sec_session_end"
                            End Select
                            auxReturn = New Intelimedia.Hercules.Communications.clshrcCommunications.clsHrcComMsg(auxBagValues).gStream_Get
                    End Select
            End Select
        Catch ex As Exception
            gTRACE_add(-1, 10, "Arrive message exception:" & ex.Message)
        End Try
        gTRACE_add(-1, 10, "Response:" & auxReturn)
        Return auxReturn
        Return ""
    End Function
    Public Overrides Function gWS_CheckPsw(ByVal pPsw As String, ByVal pIP As String) As Boolean
        Dim auxpwd As String = "demo"
        auxpwd = coWSsystempsw
        If pPsw = auxpwd Then
            Return True
        Else
            Return False
        End If
    End Function

    Public Function gEntityEMP_PostAction(ByVal pAction As clshrcimDOC.enumActionType, _
                                          ByVal pOldValues As clshrcBagValues, _
                                          ByVal pNewValues As clshrcBagValues) As String
        Try
            Select Case pAction
                Case enumActionType.coConfirmInsert, enumActionType.coConfirmModify
                    Dim pCod As Integer = pNewValues.gValue_Get("cod")
                    Dim auxStaticRow As DataRow = hrcEntityDT_EMP_FindByKey(pCod)
                    If auxStaticRow IsNot Nothing Then
                        Dim auxAleQueCod As Integer = m_Conn.gField_GetInt(auxStaticRow("alequecod"), -1)
                        If auxAleQueCod < 0 Then
                            auxAleQueCod = m_Alerts.gQueue_Create(pAleQuePublic:=False, _
                                                                pAleQueEntityID:=enumEntities.coEntityEMP, _
                                                                pAleQueCodID:=pCod)
                            gEntity_EMP_SystemUpdate(pcod:=pCod, palequecod:=auxAleQueCod)
                        End If
                    End If

                    'If pOldValues.gValue_Get("baja") Then
                    '    Dim auxBagValues As New clshrcBagValues
                    '    auxBagValues.gValue_Add("cod", auxStaticRow("undcod"))
                    '    auxBagValues.gValue_Add("editor", -2)
                    '    gEntityUND_PostAction(enumActionType.coConfirmModify, auxBagValues, auxBagValues)
                    'End If
            End Select


        Catch ex As Exception
            gSys_DebugLogAdd("EMP postAction:" & ex.Message)
        End Try
    End Function
    Public Function gEntityUND_PostAction(ByVal pAction As clshrcimDOC.enumActionType, _
                                         ByVal pOldValues As clshrcBagValues, _
                                         ByVal pNewValues As clshrcBagValues) As String
        Try
            Dim auxReturn As String = ""
            Dim pCod As Integer = pNewValues.gValue_Get("cod")
            Dim auxGrpCodEditor As Integer = -1
            Dim auxGrpCodResp As Integer = -1

            Dim auxEditorSecCod As Integer = -1
            Dim auxRows() As DataRow
            Dim auxEMPRow As DataRow
            Dim auxInitialize As Boolean = False
            Dim auxIsBaja As Boolean = False
            auxGrpCodEditor = m_Conn.gField_GetInt(pNewValues.gValue_Get("grpcodeditor"), -1)
            If auxGrpCodEditor < 0 Then
                auxInitialize = True
            End If
            If pOldValues.gValue_Get("dsc") IsNot Nothing Then
                auxInitialize = True
            End If
            If pOldValues.gValue_Get("editor") IsNot Nothing Then
                auxInitialize = True
            End If
            If pOldValues.gValue_Get("baja") IsNot Nothing Then
                Dim auxBaja_New As Boolean = m_Conn.gField_GetBoolean(pNewValues.gValue_Get("baja"), False)
                Dim auxBaja_Old As Boolean = m_Conn.gField_GetBoolean(pOldValues.gValue_Get("baja"), False)
                If pAction = enumActionType.coConfirmDelete Then
                    auxBaja_New = True
                End If
                If auxBaja_New Then
                    auxIsBaja = True
                    auxInitialize = False
                End If
                Select Case auxBaja_New
                    Case True
                        If auxGrpCodEditor > 0 Then
                            m_Security.gGroup_Disabled(auxGrpCodEditor)
                        End If
                    Case False
                        If auxGrpCodEditor > 0 Then
                            m_Security.gGroup_Enabled(auxGrpCodEditor)
                        End If
                End Select
            End If
            Dim auxGrpCodEditor_Change As Object
            If auxInitialize Then
                Dim auxUndDsc As String = ""
                auxUndDsc = m_Conn.gField_GetString(pNewValues.gValue_Get("dsc"), "")
                If auxGrpCodEditor < 1 Then
                    auxGrpCodEditor = m_Security.gGroup_Create("Und-" & auxUndDsc & "-Editores")
                Else
                    m_Security.gGroup_Rename(auxGrpCodEditor, "Und-" & auxUndDsc & "-Editores")
                End If
                auxGrpCodEditor_Change = auxGrpCodEditor
                auxEditorSecCod = -1
                Dim auxEditor As Integer = m_Conn.gField_GetInt(pNewValues.gValue_Get("editor"), -1)
                If auxEditor > 0 Then
                    auxEMPRow = hrcEntityDT_EMP_FindByKey(auxEditor)
                    If auxEMPRow IsNot Nothing Then
                        auxEditorSecCod = auxEMPRow("seccod")
                    End If
                End If
                If auxEditorSecCod = -1 Then
                    Dim auxResp As Integer = m_Conn.gField_GetInt(pNewValues.gValue_Get("resp"), -1)
                    If auxResp > 0 Then
                        auxEMPRow = hrcEntityDT_EMP_FindByKey(auxResp)
                        If auxEMPRow IsNot Nothing Then
                            auxEditorSecCod = auxEMPRow("seccod")
                        End If
                    End If
                End If

                For Each auxMember As DataRow In m_Security.gGroup_GetLogins(auxGrpCodEditor).Rows
                    If auxMember("seccod") <> auxEditorSecCod Then
                        m_Security.gGroup_DelLogin(auxGrpCodEditor, auxMember("seccod"))
                    End If
                Next
                If auxEditorSecCod > 0 Then
                    m_Security.gGroup_AddLogin(auxGrpCodEditor, auxEditorSecCod)
                End If
                gEntity_UND_SystemUpdate(pcod:=pCod, pgrpcodeditor:=auxGrpCodEditor_Change)
            End If

        Catch ex As Exception
            gSys_DebugLogAdd("UND postAction:" & ex.Message)
        End Try

    End Function
End Class


'///////////////
'v2
'Shared
Public Class imWebPage
    'Inherits Microsoft.SharePoint.WebControls.LayoutsPageBase  '
    Inherits System.Web.UI.Page
    Protected Sub Page_PreInit(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.PreInit
        Try

            If Request.UserAgent.IndexOf("Robot") <> -1 Then
                Exit Sub
            End If
            If Session("conn") Is Nothing Then
                'Inicializa
                'Dim auxClass As New clscusWeb
                'auxClass.gSystem_Init()
            End If
            'If Session("ismobile") Then
            '    Me.MasterPageFile = "general.master"
            'ElseIf InStr(Page.Request.FilePath, "/hrclicensing", CompareMethod.Text) = 0 Then
            'Else
            '    Me.MasterPageFile = "general.master"
            'End If
            Me.MasterPageFile = "general.master"
        Catch ex As Exception
        End Try

    End Sub
End Class

Public Class imWebMasterPage : Inherits System.Web.UI.MasterPage

    Private Sub Page_Init(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Init

        If Request.UserAgent.IndexOf("Robot") = -1 Then
            If Session("conn") Is Nothing Or Session("hrccontext") Is Nothing Then
                Dim auxClass As New clshrcGeneral
                auxClass.gSystem_Init()
                auxClass.gSys_DebugLogAdd("MasterPage-Page_init")
                auxClass.gSys_DebugLogAdd("MasterPage-Page_init-Name:" & Me.ID)
                auxClass.gSystem_End()
            End If
        End If

        Dim auxLiteral As New Literal
        auxLiteral.Text &= Session("user_headermeta_Content") _
            & Session("user_header_content") _
            & Session("user_masterheader_content")
        'If InStr(Page.Request.FilePath, "/people.aspx", CompareMethod.Text) <> 0 Then

        '    Exit Sub
        'End If
        'Thema
        auxLiteral.Text &= "<link href=estilos.css rel=stylesheet type=text/css />   "
        Try
            CType(Me.FindControl("conmasterheader"), ContentPlaceHolder).Controls.Add(auxLiteral)
        Catch ex As Exception

        End Try

        ' Me.Page.Header.Controls.Add(auxLiteral)
        Try
            Dim auxI As Integer = 1
            For Each auxString In CType(Session("User_Header_ScriptsInUpdatePanel"), List(Of String))
                ScriptManager.RegisterClientScriptInclude(Me, Me.GetType, "hrcScripts" & auxI, _
                                  auxString)
                auxI += 1
            Next
        Catch ex As Exception

        End Try
    End Sub
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Init

        'If Not Page.IsPostBack Then
        'If Session("user_menu") = "" Then

        'Dim auxClass As New clscusWeb
        'auxClass.gSystem_Init()
        'Session("user_menu") = auxClass.gUser_GetMenu
        'End If
        'End If
        Dim auxPreffixControl As String = ""

        If Not Page.IsPostBack Then

            Try
                If Request.QueryString("_closea_") = "1" Or Session("inframe") = True Then
                    CType(Me.FindControl("pnlHeader"), HtmlGenericControl).Visible = False
                    Try
                        CType(Me.FindControl("pnlHeader2"), HtmlGenericControl).Visible = False
                    Catch ex As Exception

                    End Try

                    Try
                        Dim auxFooter As HtmlGenericControl = CType(Me.FindControl("footer"), HtmlGenericControl)
                        auxFooter.Visible = False
                        auxFooter.Attributes.Add("display", "none")
                    Catch ex As Exception

                    End Try
                    Try
                        CType(Me.FindControl("body1"), HtmlControl).Attributes.Add("class", "div_body_popup")
                    Catch ex As Exception

                    End Try
                Else
                    CType(Me.FindControl("pnlHeader"), HtmlGenericControl).Visible = True
                    Try
                        CType(Me.FindControl("pnlHeader2"), HtmlGenericControl).Visible = True
                    Catch ex As Exception

                    End Try

                    Try
                        CType(Me.FindControl("back"), HtmlControl).Attributes.Add("class", "div_body_back ui-page-active ui-page ui-page-theme-a")
                        CType(Me.FindControl("pagewrap"), HtmlControl).Style.Add("width", "900px")
                        CType(Me.FindControl("pagewrap"), HtmlControl).Attributes.Add("class", "page_wrap")
                        CType(Me.FindControl("page_wrap"), HtmlControl).Attributes.Add("class", "div_mainpage")
                        CType(Me.FindControl("body1"), HtmlControl).Attributes.Add("class", "div_body")
                    Catch ex As Exception

                    End Try
                End If

            Catch ex As Exception

            End Try
        End If
        Try
            ' CType(Me.FindControl("body1"), HtmlControl).Attributes.Add("class", "div_body")
        Catch ex As Exception

        End Try

        Try
            If Request.QueryString("_closea_") <> "1" Then
                CType(Me.FindControl(auxPreffixControl & "lblmenu"), Literal).Text = Session("user_menu")
            End If

        Catch ex As Exception

        End Try
        Try
            CType(Me.FindControl(auxPreffixControl & "lblagenda"), Literal).Text = Session("agenda_body")
            ScriptManager.RegisterStartupScript(Me, Me.GetType, "hrcagenda", _
                         Session("agenda_script"), True)
        Catch ex As Exception

        End Try

        Try
            CType(Me.FindControl(auxPreffixControl & "lblButtons"), Literal).Text = Session("user_buttons")
            ScriptManager.RegisterStartupScript(Me, Me.GetType, "hrcUserButtons", _
                       Session("user_buttons_script"), True)
        Catch ex As Exception

        End Try

        'ScriptManager.RegisterClientScriptInclude(Me, Me.GetType, "hrcScripts_master", Session("user_master_script"))


        Try
            If Request.QueryString("_closea_") <> "1" Then
                CType(Me.FindControl(auxPreffixControl & "lblsupmenu"), Literal).Text = Session("user_superiormenu")
            End If
        Catch ex As Exception

        End Try
        Try
            If Request.QueryString("_closea_") <> "1" Then
                CType(Me.FindControl(auxPreffixControl & "lblquickmenu"), HtmlGenericControl).InnerHtml = Session("user_quickmenu")
            End If
        Catch ex As Exception

        End Try


    End Sub

End Class