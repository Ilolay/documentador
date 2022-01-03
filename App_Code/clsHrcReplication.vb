'////////////////////////////////////////////////////////////
'///        Procesamiento de replication de entidades principales
'///                v44 - 25/11/2015
'////////////////////////////////////////////
Imports Intelimedia.Hercules.Storage.clsHrcReplication
Imports Intelimedia.Hercules.Storage.clsHrcConnClient
Imports Microsoft.VisualBasic
Imports System.Collections.Generic
Imports System.Data
Imports Intelimedia.imComponentes
Imports System.Linq
Imports System.Data.SqlClient
Imports Intelimedia.inTasks
Imports Intelimedia.Hercules.Design
Namespace Intelimedia.Hercules.Storage
    Public Class clsHrcReplication
        Public Enum enumActionType As Short
            coConfirmModify = 12
            coConfirmInsert = 11
            coConfirmDelete = 13
        End Enum
        Public Enum enumEntityType As Short
            coUndefined = -1
            coEMP = 1
            coUND = 2
            coEQU = 3
            coEQUMBR = 4
            coUNDROL = 5
            coEQUMBREMP = 6
            coEQUMBRUND = 7
            coEQUROL = 8
        End Enum
    End Class
    Public Class clsHrcReplicationClient
        Public Enum enumActionType As Short
            coConfirmModify = 12
            coConfirmInsert = 11
            coConfirmDelete = 13
        End Enum
        Public Enum enumEntityType As Short
            coUndefined = -1
            coEMP = 1
            coUND = 2
            coEQU = 3
            coEQUMBR = 4
            coUNDROL = 5
            coEQUMBREMP = 6
            coEQUMBRUND = 7
            coEQUROL = 8
        End Enum
        Public Enum enumEntityFieldType As Short
            coUndefined = -1
            coUNDDependency = 1
            coEMPDependency = 2
            coLOGINDependency = 3
            coGROUPDependency = 4
            coSECDatetime = 5
            coSECLOGINModified = 6
            coEntityType = 7
        End Enum
        Private Const hrcRPLPCFG As String = "Q_RPLPCFG"

        Private m_FieldsStatus As New SortedList(Of enumFieldClassType, Boolean)
        'Private m_FieldsLocalName As New SortedList(Of enumFieldClassType, String)
        Public Enum enumFieldClassType As Short
            coUndefined = -1
            coUNDGroupResp = 1
            coUNDGroupMemberDir = 2
            coUNDGroupMember = 3
            coUNDGroupSup = 4
            coEQUmiembrosgrpcod = 5
            coEQUMBRequcod = 6
        End Enum
        'Public Sub gEQU_SetFieldName(ByVal pEquMiembrosGrpCod As String, _
        '                                  ByVal pEQUMBREqucod As String)
        '    m_FieldsLocalName(enumFieldClassType.coEQUmiembrosgrpcod) = pEquMiembrosGrpCod
        '    m_FieldsLocalName(enumFieldClassType.coEQUMBRequcod) = pEQUMBREqucod
        'End Sub
        Public Enum enumGeneralRoles As Short
            coMiembro = 1
            coMiembroDirecto = 2
            coResponsable = 3
            coSuperior = 4
        End Enum
        Public Enum enumParams As Integer
            coEntityIDEMP = 1
            coEntityIDUND = 2
            coEntityIDEQU = 3
            coEntityIDEQUMBR = 4
            coEntityIDEMP_RPLREL = 5
            coEntityIDUND_RPLREL = 6
            coEntityIDEQU_RPLREL = 7
            coEntityIDEQUMBRUND = 8
            coEntityIDEQUMBREMP = 9
            coFieldDscEQUmembersgrpcod = 101
            coFieldDscEQUMBRequcod = 102

            coTableDscEMP = 51
            coTableDscUND = 52
            coTableDscEQU = 53
            coTableDscEQUMBR = 54
            coTableDscEQUMBREMP = 55
            coTableDscEQUMBRUND = 56
            coTableDscUNDROL = 57
            coTableDscEMP_RPLREL = 58
            coTableDscUND_RPLREL = 59
            coTableDscEQU_RPLREL = 60

            coSysRplLevel = 61
            coTableDscEQUROL = 62

        End Enum
        Public Function gComponent_CreateInstance(ByVal pClass As clshrcGeneral ) As clsHrcReplicationClient
            Dim auxReplicationClient As New clsHrcReplicationClient
            auxReplicationClient.m_Tables = m_Tables
            auxReplicationClient.m_TablesEntity = m_TablesEntity
            auxReplicationClient.m_Remote1Conn = m_Remote1Conn
            auxReplicationClient.m_Remote1BasCod = m_Remote1BasCod
            auxReplicationClient.m_EntityTypes = m_EntityTypes
            'auxReplicationClient.m_EntityRemoteTypes = m_EntityRemoteTypes
            auxReplicationClient.m_LocalEntityToBuildType = m_LocalEntityToBuildType
            auxReplicationClient.m_Conn = pClass.Conn
            auxReplicationClient.m_Sec = pClass.Sec
            auxReplicationClient.m_DTChanges = gReplication_ChangesTable_Create()
            auxReplicationClient.m_Class = m_Class
            auxReplicationClient.m_LoginDefaultPsw = m_LoginDefaultPsw
            Return auxReplicationClient
        End Function
        Private m_EntityRemoteTypes As SortedList(Of Integer, Integer)
        Private m_LocalEntityToBuildType As SortedList(Of Integer, enumEntityType)

        Private m_EntityTypes As SortedList(Of enumEntityType, Integer)
        'Replications
        Private Const coRplpreffix As String = "qrpl_"
        Private m_Remote1Conn As clsHrcConnClient
        Private m_Remote1BasCod As Integer = -1

        Private m_Conn As clsHrcConnClient
        Private m_Sec As clsHrcSecurityClient
        Private Class prvTable
            Public TabLocalQueryAll As String
            Public TabLocalQueryOne As String
            Public TabLocalDsc As String
            Public TabRemoteQuery As String
            Public TabRemoteDsc As String
            Public TabLocalRplRel_Dsc As String
            Public TabBuildType As enumEntityType = enumEntityType.coUndefined
            Public TabEntityType As Integer
            Public TabEntityRelType As Integer
            Public TabColumns As New SortedList(Of Integer, prvColumn)
            Public DT_REL As DataTable
            Public DT_Remote As DataTable
            Public DT_Local As DataTable
            Public UpdateString As String = ""
            Public TabLocalRplRel_PreLoad As Boolean = False
            Public TabIsReplicable As Boolean = False
            Public TabDeleteMrkField As prvColumn
        End Class
        Private Class prvColumn
            Public FieLocalDsc As String = ""
            Public FieRemoteDsc As String = ""
            Public FieType As hrcDataTypes = hrcDataTypes.coUndefinedType
            Public FieLength As Integer = -1
            Public FieBuildType As enumEntityFieldType = enumEntityFieldType.coUndefined
            Public FieAdditional As Boolean = False
            'Public FieClass As enumFieldClassType = enumFieldClassType.coUndefined
        End Class
        Private m_Tables As New SortedList(Of Integer, prvTable)
        Private m_TablesEntity As New SortedList(Of enumEntityType, prvTable)
        Public Function gTables_AddTableWithQuery(ByVal pLocalTableName As String, _
                                       ByVal pRemoteQuery As String, _
                                       ByVal pTableRelationName As String, _
                                       ByVal pTableBuildType As enumEntityType, _
                                       ByVal pTableEntityType As Integer, _
                                       ByVal pTableEntityRelType As Integer) As Integer
            Dim auxReturn As Integer = gTables_AddTable(pLocalTableName, "", pRemoteQuery, pTableRelationName, pTableBuildType, pTableEntityType, pTableEntityRelType)
            If pRemoteQuery <> "" Then
                m_Tables(auxReturn).TabIsReplicable = True
            End If
            Return auxReturn
        End Function
        Public Function gTables_AddTable(ByVal pLocalTableName As String, _
                                        ByVal pRemoteTableName As String, _
                                        ByVal pTableRelationName As String, _
                                        ByVal pTableBuildType As enumEntityType, _
                                        ByVal pTableEntityType As Integer, _
                                        ByVal pTableEntityRelType As Integer) As Integer
            Dim auxReturn As Integer = gTables_AddTable(pLocalTableName, pRemoteTableName, "", pTableRelationName, pTableBuildType, pTableEntityType, pTableEntityRelType)
            If pRemoteTableName <> "" Then
                m_Tables(auxReturn).TabIsReplicable = True
            End If
            Return auxReturn
        End Function
        Public Function gTable_GetID(ByVal pTableBuildType As enumEntityType) As Integer
            Dim auxReturn As Integer = -1
            Dim auxTable As prvTable = Nothing
            For Each auxValue As KeyValuePair(Of Integer, prvTable) In m_Tables
                If (auxValue.Value.TabBuildType = pTableBuildType And pTableBuildType > 0) Then
                    auxTable = auxValue.Value
                    auxReturn = auxValue.Key
                    Exit For
                End If
            Next
            Return auxReturn
        End Function
        Private Function gTables_AddTable(ByVal pLocalTableName As String, _
                                         ByVal pRemoteTableName As String, _
                                         ByVal pRemoteQuery As String, _
                                         ByVal pTableRelationName As String, _
                                         ByVal pTableBuildType As enumEntityType, _
                                         ByVal pTableEntityType As Integer, _
                                         ByVal pTableEntityRelType As Integer) As Integer
            Dim auxReturn As Integer = -1
            Dim auxTable As prvTable = Nothing
            For Each auxValue As KeyValuePair(Of Integer, prvTable) In m_Tables
                If (auxValue.Value.TabBuildType = pTableBuildType And pTableBuildType > 0) _
                    Or (auxValue.Value.TabLocalDsc = pLocalTableName) Then
                    auxTable = auxValue.Value
                    auxReturn = auxValue.Key
                    Exit For
                End If
            Next
            If auxTable Is Nothing Then
                auxTable = New prvTable
                auxReturn = m_Tables.Count + 1
                m_Tables.Add(auxReturn, auxTable)
                If pTableBuildType > 0 Then
                    m_TablesEntity.Add(pTableBuildType, auxTable)
                End If
            End If
            auxTable.TabLocalDsc = pLocalTableName.Trim.ToUpper
            auxTable.TabRemoteQuery = pRemoteQuery
            auxTable.TabRemoteDsc = pRemoteTableName.Trim.ToUpper
            auxTable.TabLocalRplRel_Dsc = pTableRelationName
            auxTable.TabBuildType = pTableBuildType
            auxTable.TabEntityType = pTableEntityType
            auxTable.TabEntityRelType = pTableEntityRelType

            Return auxReturn
        End Function
        Public Function gTable_AddFieldString(ByVal pTabID As Integer, _
                                     ByVal pLocalFieldDsc As String, _
                                      ByVal pRemoteFieldDsc As String, _
                                       ByVal pFieLength As Integer, _
                                       ByVal pFieEntityFieldBuildType As enumEntityFieldType) As Integer
            Return gTable_AddField(pTabID, pLocalFieldDsc, pRemoteFieldDsc, hrcDataTypes.coStringType, pFieLength, pFieEntityFieldBuildType)
        End Function
        Public Function gTable_AddFieldNumber(ByVal pTabID As Integer, _
                                     ByVal pLocalFieldDsc As String, _
                                      ByVal pRemoteFieldDsc As String, _
                                       ByVal pFieEntityFieldBuildType As enumEntityFieldType) As Integer
            Return gTable_AddField(pTabID, pLocalFieldDsc, pRemoteFieldDsc, hrcDataTypes.coNumberType, -1, pFieEntityFieldBuildType)
        End Function
        Public Function gTable_AddFieldBoolean(ByVal pTabID As Integer, _
                                     ByVal pLocalFieldDsc As String, _
                                      ByVal pRemoteFieldDsc As String, _
                                       ByVal pFieEntityFieldBuildType As enumEntityFieldType) As Integer
            Return gTable_AddField(pTabID, pLocalFieldDsc, pRemoteFieldDsc, hrcDataTypes.coBooleanType, -1, pFieEntityFieldBuildType)
        End Function
        Public Function gTable_AddFieldDateTime(ByVal pTabID As Integer, _
                                     ByVal pLocalFieldDsc As String, _
                                      ByVal pRemoteFieldDsc As String, _
                                       ByVal pFieEntityFieldBuildType As enumEntityFieldType) As Integer
            Return gTable_AddField(pTabID, pLocalFieldDsc, pRemoteFieldDsc, hrcDataTypes.coDateType, -1, pFieEntityFieldBuildType)
        End Function
        Public Sub gTable_SetDeleteMark(ByVal pTabID As Integer, _
                                        ByVal pFieldID As Integer)

            m_Tables(pTabID).TabDeleteMrkField = m_Tables(pTabID).TabColumns(pFieldID)
        End Sub
        Private Function gTable_AddField(ByVal pTabID As Integer, _
                                        ByVal pLocalFieldDsc As String, _
                                        ByVal pRemoteFieldDsc As String, _
                                        ByVal pFieType As hrcDataTypes, _
                                        ByVal pFieLength As Integer, _
                                        ByVal pFieEntityFieldBuildType As enumEntityFieldType) As Integer
            If m_Tables.IndexOfKey(pTabID) <> -1 Then
                Dim auxTable As prvTable = m_Tables(pTabID)
                Dim auxColumn As prvColumn = Nothing
                Dim auxcolumnID As Integer = -1
                For Each auxValue As KeyValuePair(Of Integer, prvColumn) In auxTable.TabColumns
                    If auxValue.Value.FieLocalDsc = pLocalFieldDsc Then
                        auxColumn = auxValue.Value
                        auxcolumnID = auxValue.Key
                        Exit For
                    End If
                Next
                If auxColumn Is Nothing Then
                    auxColumn = New prvColumn
                    auxcolumnID = auxTable.TabColumns.Count + 1
                    auxTable.TabColumns.Add(auxcolumnID, auxColumn)
                End If
                auxColumn.FieLocalDsc = pLocalFieldDsc.Trim.ToUpper
                auxColumn.FieRemoteDsc = pRemoteFieldDsc.Trim.ToUpper
                auxColumn.FieType = pFieType
                auxColumn.FieLength = pFieLength
                auxColumn.FieBuildType = pFieEntityFieldBuildType


                Return auxcolumnID
            End If
        End Function
        Dim auxMode As Short = 2
        Private Function gReplication_Load(ByVal pClass As clshrcGeneral, _
                                           ByVal pBasCod As Integer, _
                                           ByVal pConn As clsHrcConnClient) As Boolean
            '
            '1 - con Update en table
            '2 con precache 
            Dim auxSelect As String
            For Each auxTable As prvTable In m_Tables.Values
                If auxTable.TabLocalRplRel_PreLoad Then
                    Select Case auxMode
                        Case 1
                            auxTable.UpdateString = gReplication_GetChangesWithUpdate(auxTable)
                        Case 2
                    End Select
                End If

                auxSelect = gReplication_GetRemoteTableQuery(auxTable)
                If auxSelect <> "" Then
                    auxTable.DT_Remote = m_Remote1Conn.gConn_Query(auxSelect)
                    If m_Remote1Conn.LastErrorDescription <> "" Then
                        gSys_DebugLogAdd("Remote result:" & m_Remote1Conn.LastErrorDescription)
                    End If
                End If

                If auxTable.TabBuildType = enumEntityType.coEQUMBR Then
                    If auxTable.DT_Remote IsNot Nothing Then
                        Dim auxLocalEntityID As Integer
                        For Each auxRow As DataRow In auxTable.DT_Remote.Rows
                            auxLocalEntityID = -1
                            If m_EntityRemoteTypes.IndexOfKey(auxRow("mbrtypecod")) = -1 Then
                            Else
                                auxLocalEntityID = m_EntityRemoteTypes(auxRow("mbrtypecod"))
                            End If
                            If auxLocalEntityID = -1 Then
                                gSys_DebugLogAdd("Invalid remote entity:" & auxRow("mbrtypecod") & "-" & auxRow("cod"))
                            End If
                            auxRow("mbrtypecod") = auxLocalEntityID
                        Next
                    End If

                End If
                Select Case auxMode
                    Case 1
                    Case 2
                        If auxTable.DT_Remote IsNot Nothing Then
                            auxSelect = gReplication_GetLocalTableQuery(auxTable, False)
                            If auxSelect <> "" Then
                                auxTable.DT_Local = pConn.gConn_Query(auxSelect)
                                If auxTable.DT_Local.Columns.Count = 0 Then
                                    auxTable.DT_Local = Nothing
                                Else
                                    Dim auxDTCloned As DataTable = auxTable.DT_Local.Clone()
                                    'auxDTCloned.Columns(0).DataType = Type.GetType("System.String")
                                    'Dim auxDeleteMarkIsDateTime_Remote As Boolean = False
                                    'If auxTable.TabDeleteMrkField IsNot Nothing Then
                                    '    If auxTable.TabDeleteMrkField.FieRemoteDsc <> "" Then
                                    '        If auxTable.DT_Remote.Columns(auxTable.TabDeleteMrkField.FieRemoteDsc).DataType.Name.ToUpper = "DATETIME" Then
                                    '            auxDeleteMarkIsDateTime_Remote = True
                                    '        End If
                                    '    End If
                                    'End If

                                    For Each auxRow As DataRow In auxTable.DT_Remote.Rows
                                        auxDTCloned.ImportRow(auxRow)
                                    Next
                                    auxTable.DT_Remote = auxDTCloned
                                End If

                                ' If auxTable.DT_Local.Rows.Count = 0 Then
                                'auxTable.DT_Local = Nothing
                                'Else

                                'End If
                            End If
                        End If

                End Select
                If auxTable.TabLocalRplRel_Dsc <> "" Then
                    auxTable.DT_REL = pClass.Conn.gConn_Query("SELECT * FROM " & auxTable.TabLocalRplRel_Dsc)
                    If auxTable.DT_Local IsNot Nothing Then
                        Dim auxToDeleteList As New List(Of DataRow)
                        If auxTable.DT_REL IsNot Nothing Then
                            For Each auxRow As DataRow In auxTable.DT_REL.Rows
                                If auxRow("cod") > 0 Then
                                    If auxTable.DT_Local.Select("cod=" & auxRow("cod")).Count = 0 Then
                                        auxToDeleteList.Add(auxRow)
                                    End If
                                End If
                            Next
                            For Each auxRow As DataRow In auxToDeleteList
                                auxTable.DT_REL.Rows.Remove(auxRow)
                            Next
                        End If
                    End If
                End If
                ' End If
            Next
            pClass.Conn.gConn_Close()
            Return True
        End Function
        Private m_DebugMessage As New List(Of String)
        Private Sub gDebug_Clear()
            m_DebugMessage.Clear()
        End Sub
        Public Function gDebug_Get() As List(Of String)
            Return m_DebugMessage
        End Function
        Private Sub gSys_DebugLogAdd(ByVal pMsg As String)
            m_DebugMessage.Add(pMsg)
            'pClass.gSys_DebugLogAdd("Replication-" & pMsg)
        End Sub
        Private m_SouceLastReplicationOK As DateTime
        Private m_Class As clshrcGeneral
        Private m_LoginDefaultPsw As String
        Private Function gReplication_Parameter_Get(ByVal pConn As clsHrcConnClient, _
                                                    ByVal pParamID As enumParams) As String
            Dim auxReturn As String = ""
            auxReturn = pConn.gConn_QueryValueString("SELECT cfgdsc FROM " & hrcRPLPCFG _
                                                   & " WHERE cfgcod =" & pParamID)
            Return auxReturn
        End Function
      
        'Public Sub gUND_FieldMembersDirect_Disable()
        '    If m_FieldsStatus.IndexOfKey(enumFieldClassType.coUNDGroupMemberDir) <> -1 Then
        '        m_FieldsStatus.Remove(enumFieldClassType.coUNDGroupMemberDir)
        '    End If
        'End Sub
        'Public Sub gUND_FieldMembersDirect_Enable()
        '    If m_FieldsStatus.IndexOfKey(enumFieldClassType.coUNDGroupMemberDir) = -1 Then
        '        m_FieldsStatus.Add(enumFieldClassType.coUNDGroupMemberDir, True)
        '    End If
        'End Sub
        Public Sub gReplication_Configure_bak()
            Dim auxBagValuesNewValues As New clshrcBagValues
            Dim auxBagValuesOldValues As New clshrcBagValues

            Dim auxClassGeneral As New clshrcGeneral
            auxClassGeneral.gSystem_Init()
            Dim auxCod As Integer
            Dim auxConn As clsHrcConnClient = auxClassGeneral.Conn ' m_Conn.gComponent_CreateInstance
            auxConn.gConn_Open()
            For Each auxRow As DataRow In auxConn.gConn_Query("SELECT cod,dsc FROM UND WHERE COD > 0").Rows
                auxCod = auxRow("cod")
                Dim auxStaticRow As DataRow = m_Class.gReplication_DataGetByCod(m_EntityTypes(enumEntityType.coUND), auxCod)
                auxBagValuesNewValues = auxConn.gField_GetBagValuesFromArray(auxStaticRow.ItemArray, auxStaticRow.Table.Columns)
                gEntity_AddItemChanged(m_EntityTypes(enumEntityType.coUND), enumActionType.coConfirmModify, auxBagValuesNewValues, auxBagValuesNewValues)
            Next

            For Each auxRow As DataRow In auxConn.gConn_Query("SELECT cod,dsc FROM EMP WHERE COD > 0").Rows
                auxCod = auxRow("cod")
                Dim auxStaticRow As DataRow = m_Class.gReplication_DataGetByCod(m_EntityTypes(enumEntityType.coEMP), auxCod)
                auxBagValuesNewValues = auxConn.gField_GetBagValuesFromArray(auxStaticRow.ItemArray, auxStaticRow.Table.Columns)
                gEntity_AddItemChanged(m_EntityTypes(enumEntityType.coEMP), enumActionType.coConfirmModify, auxBagValuesNewValues, auxBagValuesNewValues)
            Next

            'Ajusta el nombre de los grupos de unidades para evitar solapamientos (dsc + nro)
            For Each auxRow As DataRow In auxConn.gConn_Query("SELECT cod,dsc,baja,grpcodresp,grpcodprjver,grpcodmbrdir,miembrosgrpcod FROM UND WHERE cod > 0").Rows
                If auxConn.gField_GetBoolean(auxRow("baja"), False) = False Then
                    auxClassGeneral.Sec.gGroup_Rename(auxConn.gField_GetInt(auxRow("grpcodresp"), -1), "Und-" & auxRow("dsc").ToString & "-Responsables")
                    auxClassGeneral.Sec.gGroup_Rename(auxConn.gField_GetInt(auxRow("grpcodprjver"), -1), "Und-" & auxRow("dsc").ToString & "-Superiores")
                    auxClassGeneral.Sec.gGroup_Rename(auxConn.gField_GetInt(auxRow("grpcodmbrdir"), -1), "Und-" & auxRow("dsc").ToString & "-Miembros directos")
                    auxClassGeneral.Sec.gGroup_Rename(auxConn.gField_GetInt(auxRow("miembrosgrpcod"), -1), "Und-" & auxRow("dsc").ToString & "-Miembros")
                Else
                    auxConn.gConn_Update("UPDATE UND SET grpcodresp=-1,grpcodprjver=-1,grpcodmbrdir=-1,miembrosgrpcod=-1 WHERE cod =" & auxRow("cod"))
                End If
            Next

            For Each auxRow As DataRow In auxConn.gConn_Query("SELECT cod,dsc,baja,miembrosgrpcod " _
                                                             & " FROM " & m_TablesEntity(enumEntityType.coEQU).TabLocalDsc _
                                                             & " WHERE cod > 0").Rows
                If auxConn.gField_GetBoolean(auxRow("baja"), False) = False Then
                    auxClassGeneral.Sec.gGroup_Rename(auxConn.gField_GetInt(auxRow("miembrosgrpcod"), -1), "Equ-" & auxRow("dsc").ToString & "-Miembros directos")
                Else
                    auxConn.gConn_Update("UPDATE " & m_TablesEntity(enumEntityType.coEQU).TabLocalDsc _
                                        & " SET miembrosgrpcod=-1 WHERE cod =" & auxRow("cod"))
                End If
            Next
            auxConn.gConn_Close()
            auxConn = Nothing
        End Sub
        Public Sub gReplication_SetConfig(ByVal pClass As clshrcGeneral, _
                                          ByVal pLoginDefaultPsw As String)
            m_LoginDefaultPsw = pLoginDefaultPsw
            m_Conn = pClass.Conn
            m_Sec = pClass.Sec  '.gComponent_CreateInstance(auxConn)
            m_Class = pClass
            m_DTChanges = gReplication_ChangesTable_Create()
            'If auxConn Is Nothing Then
            '    gSys_DebugLogAdd("Error in class.No connection")
            '    Exit Sub
            'End If 
            Dim auxTableID As Integer
            Dim auxFieldID As Integer
            Dim auxTable As prvTable

            m_LocalEntityToBuildType = New SortedList(Of Integer, enumEntityType)
            m_EntityTypes = New SortedList(Of enumEntityType, Integer)

            Dim auxLocalTableName As String = ""
            Dim auxLocalTableNameRPLREL As String = ""
            Dim auxLocalEntityType As Integer
            Dim auxLocalEntityTypeRPLREL As Integer
            ' ///////////////////////// EMP
            auxLocalTableName = gReplication_Parameter_Get(m_Conn, enumParams.coTableDscEMP)
            If auxLocalTableName = "" Then
                gSys_DebugLogAdd("Error in local replication configuration:EMP no name")
                auxLocalTableName = "EMP"
            End If

            auxLocalTableNameRPLREL = gReplication_Parameter_Get(m_Conn, enumParams.coTableDscEMP_RPLREL)
            If auxLocalTableName = "" Then
                gSys_DebugLogAdd("Error in local replication configuration:EMP_RPLREL no name")
            End If
            auxLocalEntityType = Val(gReplication_Parameter_Get(m_Conn, enumParams.coEntityIDEMP))
            If auxLocalEntityType < 1 Then
                auxLocalEntityType = -1
                gSys_DebugLogAdd("Error in local replication configuration:EMP no entity")
            Else
                m_EntityTypes.Add(enumEntityType.coEMP, auxLocalEntityType)
                m_LocalEntityToBuildType.Add(auxLocalEntityType, enumEntityType.coEMP)
            End If

            auxLocalEntityTypeRPLREL = Val(gReplication_Parameter_Get(m_Conn, enumParams.coEntityIDEMP_RPLREL))
            If auxLocalEntityTypeRPLREL < 1 Then
                auxLocalEntityTypeRPLREL = -1
                gSys_DebugLogAdd("Error in local replication configuration:EMP_RPLREL no entity")
            End If

            auxTableID = gTables_AddTable(auxLocalTableName, "", auxLocalTableNameRPLREL, clsHrcReplication.enumEntityType.coEMP, auxLocalEntityType, auxLocalEntityTypeRPLREL)
            auxTable = m_Tables(auxTableID)
            auxTable.TabLocalRplRel_PreLoad = True
            gTable_AddFieldNumber(auxTableID, "cod", "cod", enumEntityFieldType.coUndefined)
            gTable_AddFieldString(auxTableID, "dsc", "dsc", 200, enumEntityFieldType.coUndefined)
            gTable_AddFieldNumber(auxTableID, "undcod", "undcod", enumEntityFieldType.coUNDDependency)
            auxFieldID = gTable_AddFieldBoolean(auxTableID, "baja", "baja", enumEntityFieldType.coUndefined)
            gTable_SetDeleteMark(auxTableID, auxFieldID)
            gTable_AddFieldString(auxTableID, "empusername", "empusername", 200, enumEntityFieldType.coUndefined)
            'gTable_AddFieldNumber(auxTableID, "qsecsid", "qsecsid", enumEntityFieldType.coSECLOGINModified)
            'gTable_AddFieldDateTime(auxTableID, "qsecdatetime", "qsecdatetime", enumEntityFieldType.coSECDatetime)



            ' ///////////////////////// UND
            auxLocalTableName = gReplication_Parameter_Get(m_Conn, enumParams.coTableDscUND)
            If auxLocalTableName = "" Then
                gSys_DebugLogAdd("Error in local replication configuration:UND no name")
                auxLocalTableName = "UND"
            End If
            auxLocalEntityType = Val(gReplication_Parameter_Get(m_Conn, enumParams.coEntityIDUND))
            If auxLocalEntityType < 1 Then
                auxLocalEntityType = -1
                gSys_DebugLogAdd("Error in local replication configuration:UND no entity")
            Else
                m_EntityTypes.Add(enumEntityType.coUND, auxLocalEntityType)
                m_LocalEntityToBuildType.Add(auxLocalEntityType, enumEntityType.coUND)
            End If

            auxLocalTableNameRPLREL = gReplication_Parameter_Get(m_Conn, enumParams.coTableDscUND_RPLREL)
            If auxLocalTableNameRPLREL = "" Then
                gSys_DebugLogAdd("Error in local replication configuration:UND_RPLREL no name")
            End If
            auxLocalEntityTypeRPLREL = Val(gReplication_Parameter_Get(m_Conn, enumParams.coEntityIDUND_RPLREL))
            If auxLocalEntityTypeRPLREL < 1 Then
                auxLocalEntityTypeRPLREL = -1
                gSys_DebugLogAdd("Error in local replication configuration:UND_RPLREL no entity")
            End If
            auxTableID = gTables_AddTable(auxLocalTableName, "", auxLocalTableNameRPLREL, clsHrcReplication.enumEntityType.coUND, auxLocalEntityType, auxLocalEntityTypeRPLREL)
            auxTable = m_Tables(auxTableID)
            auxTable.TabLocalRplRel_PreLoad = True
            gTable_AddFieldNumber(auxTableID, "cod", "cod", enumEntityFieldType.coUndefined)
            gTable_AddFieldString(auxTableID, "dsc", "dsc", 200, enumEntityFieldType.coUndefined)
            gTable_AddFieldNumber(auxTableID, "undcodsup", "undcodsup", enumEntityFieldType.coUNDDependency)
            gTable_AddFieldNumber(auxTableID, "resp", "resp", enumEntityFieldType.coEMPDependency)
            auxFieldID = gTable_AddFieldBoolean(auxTableID, "baja", "baja", enumEntityFieldType.coUndefined)
            gTable_SetDeleteMark(auxTableID, auxFieldID)
            'gTable_AddFieldNumber(auxTableID, "qsecsid", "qsecsid", enumEntityFieldType.coUndefined)
            'gTable_AddFieldDateTime(auxTableID, "qsecdatetime", "qsecdatetime", enumEntityFieldType.coUndefined)


            ' ///////////////////////// EQU
            auxLocalTableName = gReplication_Parameter_Get(m_Conn, enumParams.coTableDscEQU)
            If auxLocalTableName = "" Then
                gSys_DebugLogAdd("Error in local replication configuration:EQU no name")
                auxLocalTableName = "EQU"
            End If
            auxLocalEntityType = Val(gReplication_Parameter_Get(m_Conn, enumParams.coEntityIDEQU))
            If auxLocalEntityType < 1 Then
                auxLocalEntityType = -1
                gSys_DebugLogAdd("Error in local replication configuration:EQU no entity")
            Else
                m_EntityTypes.Add(enumEntityType.coEQU, auxLocalEntityType)
                m_LocalEntityToBuildType.Add(auxLocalEntityType, enumEntityType.coEQU)
            End If
            auxLocalTableNameRPLREL = gReplication_Parameter_Get(m_Conn, enumParams.coTableDscEQU_RPLREL)
            If auxLocalTableNameRPLREL = "" Then
                gSys_DebugLogAdd("Error in local replication configuration:EQU_RPLREL no name")
            End If
            auxLocalEntityTypeRPLREL = Val(gReplication_Parameter_Get(m_Conn, enumParams.coEntityIDEQU_RPLREL))
            If auxLocalEntityTypeRPLREL < 1 Then
                auxLocalEntityTypeRPLREL = -1
                gSys_DebugLogAdd("Error in local replication configuration:EQU_RPLREL no entity")
            End If
            auxTableID = gTables_AddTable(auxLocalTableName, "", auxLocalTableNameRPLREL, clsHrcReplication.enumEntityType.coEQU, auxLocalEntityType, auxLocalEntityTypeRPLREL)
            auxTable = m_Tables(auxTableID)
            auxTable.TabLocalRplRel_PreLoad = True
            gTable_AddFieldNumber(auxTableID, "cod", "cod", enumEntityFieldType.coUndefined)
            gTable_AddFieldString(auxTableID, "dsc", "dsc", 200, enumEntityFieldType.coUndefined)
            gTable_AddFieldNumber(auxTableID, "undcod", "undcod", enumEntityFieldType.coUNDDependency)
            auxFieldID = gTable_AddFieldBoolean(auxTableID, "baja", "baja", enumEntityFieldType.coUndefined)
            gTable_SetDeleteMark(auxTableID, auxFieldID)
            'gTable_AddFieldNumber(auxTableID, "qsecsid", "qsecsid", enumEntityFieldType.coUndefined)
            'gTable_AddFieldDateTime(auxTableID, "qsecdatetime", "qsecdatetime", enumEntityFieldType.coUndefined)

            ' ///////////////////////// EQUMBR
            auxLocalTableName = gReplication_Parameter_Get(m_Conn, enumParams.coTableDscEQUMBR)
            If auxLocalTableName = "" Then
                gSys_DebugLogAdd("Error in local replication configuration:EQUMBR no name")
                auxLocalTableName = "EQUMBR"
            End If
            auxLocalEntityType = Val(gReplication_Parameter_Get(m_Conn, enumParams.coEntityIDEQUMBR))
            If auxLocalEntityType < 1 Then
                gSys_DebugLogAdd("Error in local replication configuration:EQUMBR")
            Else
                m_EntityTypes.Add(enumEntityType.coEQUMBR, auxLocalEntityType)
                m_LocalEntityToBuildType.Add(auxLocalEntityType, enumEntityType.coEQUMBR)
            End If
            auxLocalTableNameRPLREL = ""
            auxLocalEntityTypeRPLREL = -1
            auxTableID = gTables_AddTable(auxLocalTableName, "", auxLocalTableNameRPLREL, clsHrcReplication.enumEntityType.coEQUMBR, auxLocalEntityType, auxLocalEntityTypeRPLREL)
            auxTable = m_Tables(auxTableID)
            gTable_AddFieldNumber(auxTableID, "cod", "cod", enumEntityFieldType.coUndefined)
            gTable_AddFieldNumber(auxTableID, "equcod", "equcod", enumEntityFieldType.coUndefined)
            gTable_AddFieldNumber(auxTableID, "mbrtypecod", "mbrtypecod", enumEntityFieldType.coEntityType)
            gTable_AddFieldNumber(auxTableID, "gruporesp", "mbrtypecod", enumEntityFieldType.coUndefined)
            gTable_AddFieldNumber(auxTableID, "grupomiembros", "mbrtypecod", enumEntityFieldType.coUndefined)
            gTable_AddFieldNumber(auxTableID, "grupoprjver", "mbrtypecod", enumEntityFieldType.coUndefined)
            gTable_AddFieldNumber(auxTableID, "grupombrdir", "mbrtypecod", enumEntityFieldType.coUndefined)
            gTable_AddFieldNumber(auxTableID, "undcod", "undcod", enumEntityFieldType.coUNDDependency)
            gTable_AddFieldNumber(auxTableID, "empcod", "empcod", enumEntityFieldType.coEMPDependency)
            ' ///////////////////////// EQUMBREMP
            auxLocalTableName = gReplication_Parameter_Get(m_Conn, enumParams.coTableDscEQUMBREMP)
            If auxLocalTableName = "" Then
                gSys_DebugLogAdd("Error in local replication configuration:EQUMBREMP no name")
                auxLocalTableName = "EQUMBREMP"
            End If
            auxLocalEntityType = Val(gReplication_Parameter_Get(m_Conn, enumParams.coEntityIDEQUMBREMP))
            If auxLocalEntityType < 1 Then
                gSys_DebugLogAdd("Error in local replication configuration:EQUMBREMP")
            Else
                m_EntityTypes.Add(enumEntityType.coEQUMBREMP, auxLocalEntityType)
                m_LocalEntityToBuildType.Add(auxLocalEntityType, enumEntityType.coEQUMBREMP)
            End If
            auxTableID = gTables_AddTable(auxLocalTableName, "", "", clsHrcReplication.enumEntityType.coEQUMBREMP, auxLocalEntityType, -1)
            gTable_AddFieldNumber(auxTableID, "equmbrcod", "equmbrcod", enumEntityFieldType.coUndefined)
            gTable_AddFieldNumber(auxTableID, "empcod", "empcod", enumEntityFieldType.coEMPDependency)

            ' ///////////////////////// EQUMBRUND
            auxLocalTableNameRPLREL = ""
            auxLocalEntityTypeRPLREL = -1
            auxLocalTableName = gReplication_Parameter_Get(m_Conn, enumParams.coTableDscEQUMBRUND)
            If auxLocalTableName = "" Then
                gSys_DebugLogAdd("Error in local replication configuration:EQUMBRUND no name")
                auxLocalTableName = "EQUMBRUND"
            End If
            auxLocalEntityType = Val(gReplication_Parameter_Get(m_Conn, enumParams.coEntityIDEQUMBRUND))
            If auxLocalEntityType < 1 Then
                gSys_DebugLogAdd("Error in local replication configuration:EQUMBRUND")
            Else
                m_EntityTypes.Add(enumEntityType.coEQUMBRUND, auxLocalEntityType)
                m_LocalEntityToBuildType.Add(auxLocalEntityType, enumEntityType.coEQUMBRUND)
            End If
            auxTableID = gTables_AddTable(auxLocalTableName, "", "", clsHrcReplication.enumEntityType.coEQUMBRUND, auxLocalEntityType, -1)
            auxTable = m_Tables(auxTableID)
            gTable_AddFieldNumber(auxTableID, "equmbrcod", "equmbrcod", enumEntityFieldType.coUndefined)
            gTable_AddFieldNumber(auxTableID, "gruporesp", "mbrtypecod", enumEntityFieldType.coUndefined)
            gTable_AddFieldNumber(auxTableID, "grupomiembros", "mbrtypecod", enumEntityFieldType.coUndefined)
            gTable_AddFieldNumber(auxTableID, "grupoprjver", "mbrtypecod", enumEntityFieldType.coUndefined)
            gTable_AddFieldNumber(auxTableID, "grupombrdir", "mbrtypecod", enumEntityFieldType.coUndefined)
            gTable_AddFieldNumber(auxTableID, "undcod", "undcod", enumEntityFieldType.coUNDDependency)
            ' ///////////////////////// EQUMBRUNDROL
            auxLocalTableNameRPLREL = ""
            auxLocalEntityTypeRPLREL = -1
            auxLocalTableName = gReplication_Parameter_Get(m_Conn, enumParams.coTableDscUNDROL)
            If auxLocalTableName = "" Then
                gSys_DebugLogAdd("Error in local replication configuration:UNDROL no name")
                auxLocalTableName = "UNDROL"
            End If
            auxTableID = gTables_AddTable(auxLocalTableName, "", "", "", clsHrcReplication.enumEntityType.coUNDROL, -1, -1)
            auxTable = m_Tables(auxTableID)
            gTable_AddFieldNumber(auxTableID, "cod", "cod", enumEntityFieldType.coUndefined)
            gTable_AddFieldNumber(auxTableID, "undcod", "undcod", enumEntityFieldType.coUNDDependency)
            gTable_AddFieldNumber(auxTableID, "undroltypecod", "undroltypecod", enumEntityFieldType.coUNDDependency)
            gTable_AddFieldString(auxTableID, "domgrpname", "domgrpname", 200, enumEntityFieldType.coUndefined)
            gTable_AddFieldString(auxTableID, "objectsid", "objectsid", 100, enumEntityFieldType.coUndefined)


            ' ///////////////////////// EQUROL
            auxLocalTableNameRPLREL = ""
            auxLocalEntityTypeRPLREL = -1
            auxLocalTableName = gReplication_Parameter_Get(m_Conn, enumParams.coTableDscEQUROL)
            If auxLocalTableName = "" Then
                gSys_DebugLogAdd("Error in local replication configuration:EQUROL no name")
                auxLocalTableName = "EQUROL"
            End If

            auxTableID = gTables_AddTable(auxLocalTableName, "", "", "", clsHrcReplication.enumEntityType.coEQUROL, -1, -1)
            auxTable = m_Tables(auxTableID)
            gTable_AddFieldNumber(auxTableID, "cod", "cod", enumEntityFieldType.coUndefined)
            gTable_AddFieldNumber(auxTableID, "equcod", "equcod", enumEntityFieldType.coUndefined)
            gTable_AddFieldNumber(auxTableID, "equroltypecod", "equroltypecod", enumEntityFieldType.coUndefined)
            gTable_AddFieldString(auxTableID, "domgrpname", "domgrpname", 200, enumEntityFieldType.coUndefined)
            gTable_AddFieldString(auxTableID, "objectsid", "objectsid", 100, enumEntityFieldType.coUndefined)

        End Sub
        Public Sub gReplication_SetRemoteConfig(ByVal pRemoteConn As clsHrcConnClient, _
                                              ByVal pLastReplicationOK As DateTime, _
                                              ByVal pBasCod As Integer, _
                                              ByVal pReplicateEMP As Boolean, _
                                              ByVal pReplicateUND As Boolean, _
                                              ByVal pReplicateEQU As Boolean)
            m_Remote1Conn = pRemoteConn
            m_SouceLastReplicationOK = pLastReplicationOK

            m_Remote1BasCod = pBasCod
            m_Remote1Conn = pRemoteConn
            Dim auxConn As clsHrcConnClient = m_Class.Conn ' m_Conn.gComponent_CreateInstance
            auxConn.gConn_Open()
            'Estas tablas deben estar en este orden
            Dim auxTableID As Integer
            Dim auxTable As prvTable
            Dim auxRemoteTableName As String
            Dim auxRemoteEntityType As Integer

            'Cargar la tabla de entidades remotas
            Dim auxCancel As Boolean = False
            Dim auxValue As String = ""
            'Crea la tabla de relacion de entidades remotas-locales
            m_EntityRemoteTypes = New SortedList(Of Integer, Integer)
            ' ///////////////////////// EMP
            If pReplicateEMP Then
                auxRemoteTableName = gReplication_Parameter_Get(pRemoteConn, enumParams.coTableDscEMP)
                If auxRemoteTableName = "" Then
                    gSys_DebugLogAdd("Error in remote replication configuration:EMP no name")
                    auxRemoteTableName = "EMP"
                End If
                auxRemoteEntityType = Val(gReplication_Parameter_Get(pRemoteConn, enumParams.coEntityIDEMP))
                If auxRemoteEntityType < 1 Then
                    auxRemoteEntityType = -1
                    gSys_DebugLogAdd("Error in remote replication configuration:EMP no entity")
                Else
                    m_EntityRemoteTypes.Add(auxRemoteEntityType, m_EntityTypes(enumEntityType.coEMP))
                End If
                auxTableID = gTable_GetID(enumEntityType.coEMP) '
                If auxTableID > 0 Then
                    auxTable = m_Tables(auxTableID)
                    auxTable.TabRemoteDsc = auxRemoteTableName
                    auxTable.TabIsReplicable = True
                End If
            End If

            ' ///////////////////////// UND
            If pReplicateUND Then
                auxRemoteTableName = gReplication_Parameter_Get(pRemoteConn, enumParams.coTableDscUND)
                If auxRemoteTableName = "" Then
                    gSys_DebugLogAdd("Error in remote replication configuration:UND no name")
                    auxRemoteTableName = "UND"
                End If
                auxRemoteEntityType = Val(gReplication_Parameter_Get(pRemoteConn, enumParams.coEntityIDUND))
                If auxRemoteEntityType < 1 Then
                    auxRemoteEntityType = -1
                    gSys_DebugLogAdd("Error in remote replication configuration:UND no entity")
                Else
                    m_EntityRemoteTypes.Add(auxRemoteEntityType, m_EntityTypes(enumEntityType.coUND))
                End If
                auxTableID = gTable_GetID(enumEntityType.coUND)
                If auxTableID > 0 Then
                    auxTable = m_Tables(auxTableID)
                    auxTable.TabRemoteDsc = auxRemoteTableName
                    auxTable.TabIsReplicable = True
                End If
            End If

            ' ///////////////////////// EQU
            If pReplicateEQU Then
                auxRemoteTableName = gReplication_Parameter_Get(pRemoteConn, enumParams.coTableDscEQU)
                If auxRemoteTableName = "" Then
                    gSys_DebugLogAdd("Error in remote replication configuration:EQU no name")
                    auxRemoteTableName = "EQU"
                End If
                auxRemoteEntityType = Val(gReplication_Parameter_Get(pRemoteConn, enumParams.coEntityIDEQU))
                If auxRemoteEntityType < 1 Then
                    auxRemoteEntityType = -1
                    gSys_DebugLogAdd("Error in remote replication configuration:EQU no entity")
                Else
                    m_EntityRemoteTypes.Add(auxRemoteEntityType, m_EntityTypes(enumEntityType.coEQU))
                End If
                auxTableID = gTable_GetID(enumEntityType.coEQU)
                If auxTableID > 0 Then
                    auxTable = m_Tables(auxTableID)
                    auxTable.TabRemoteDsc = auxRemoteTableName
                    auxTable.TabIsReplicable = True
                End If



                ' ///////////////////////// EQUMBR
                auxRemoteTableName = gReplication_Parameter_Get(pRemoteConn, enumParams.coTableDscEQUMBR)
                If auxRemoteTableName = "" Then
                    gSys_DebugLogAdd("Error in remote replication configuration:EQUMBR no name")
                    auxRemoteTableName = "EQUMBR"
                End If
                auxRemoteEntityType = Val(gReplication_Parameter_Get(pRemoteConn, enumParams.coEntityIDEQUMBR))
                If auxRemoteEntityType < 1 Then
                    gSys_DebugLogAdd("Error in remote replication configuration:EQUMBR")
                    auxCancel = True
                Else
                    m_EntityRemoteTypes.Add(auxRemoteEntityType, m_EntityTypes(enumEntityType.coEQUMBR))
                End If
                auxTableID = gTable_GetID(enumEntityType.coEQUMBR)
                If auxTableID > 0 Then
                    auxTable = m_Tables(auxTableID)
                    auxTable.TabRemoteDsc = auxRemoteTableName
                    auxTable.TabIsReplicable = True
                End If

                ' ///////////////////////// EQUMBREMP
                auxRemoteTableName = gReplication_Parameter_Get(pRemoteConn, enumParams.coTableDscEQUMBREMP)
                If auxRemoteTableName = "" Then
                    gSys_DebugLogAdd("Error in remote replication configuration:EQUMBREMP no name")
                    auxRemoteTableName = "EQUMBREMP"
                End If
                auxTableID = gTable_GetID(enumEntityType.coEQUMBREMP)
                If auxTableID > 0 Then
                    auxTable = m_Tables(auxTableID)
                    auxTable.TabRemoteDsc = auxRemoteTableName
                End If
                ' ///////////////////////// EQUMBRUND
                auxRemoteTableName = gReplication_Parameter_Get(pRemoteConn, enumParams.coTableDscEQUMBRUND)
                If auxRemoteTableName = "" Then
                    gSys_DebugLogAdd("Error in remote replication configuration:EQUMBRUND no name")
                    auxRemoteTableName = "EQUMBRUND"
                End If
                auxTableID = gTable_GetID(enumEntityType.coEQUMBRUND)
                If auxTableID > 0 Then
                    auxTable = m_Tables(auxTableID)
                    auxTable.TabRemoteDsc = auxRemoteTableName
                End If
            End If
            auxConn.gConn_Close()
            auxConn = Nothing
        End Sub


        Public Sub gReplication_Initialize()
            Dim auxTable As prvTable
            'Crear los queryes
            'Lo debe hacer luego de inicializar, para tener los campos adicionales que se puedan agregar
            auxTable = m_TablesEntity(enumEntityType.coEQUMBR)
            m_TablesEntity(enumEntityType.coEQUMBR).TabLocalQueryOne = gReplication_GetLocalTableQuery(auxTable, True)


        End Sub
        Private m_EQUMBRChanges As New List(Of Integer)
        Private m_DTChanges As DataTable
        Public ReadOnly Property DTChanges As DataTable
            Get
                Return m_DTChanges
            End Get
        End Property

        Private Function gReplication_ChangesTable_Create() As DataTable
            Dim auxReturn As New DataTable
            auxReturn.Columns.Add("EntityType", Type.GetType("System.Int32"))
            auxReturn.Columns.Add("EntityBuildType", Type.GetType("System.Int32"))
            auxReturn.Columns.Add("ActionType", Type.GetType("System.Int32"))
            auxReturn.Columns.Add("OldValues", Type.GetType("System.String"))
            auxReturn.Columns.Add("NewValues", Type.GetType("System.String"))
            Return auxReturn
        End Function
        Private Function gReplication_GetRemoteTableQuery(ByVal pTable As prvTable) As String
            If pTable.TabRemoteQuery <> "" Then
                Return pTable.TabRemoteQuery
            ElseIf pTable.TabRemoteDsc = "" Then
                Return ""
            Else
                Dim auxSelect As String = ""

                Select Case pTable.TabBuildType
                    Case enumEntityType.coEQUMBR
                        Dim auxTableNameEQU As String = m_TablesEntity(enumEntityType.coEQU).TabRemoteDsc
                        Dim auxTableNameEQUMBR As String = m_TablesEntity(enumEntityType.coEQUMBR).TabRemoteDsc
                        Dim auxTableNameEQUMBRUND As String = m_TablesEntity(enumEntityType.coEQUMBRUND).TabRemoteDsc
                        Dim auxTableNameEQUMBREMP As String = m_TablesEntity(enumEntityType.coEQUMBREMP).TabRemoteDsc
                        auxSelect = ""
                        auxSelect &= "SELECT " & auxTableNameEQUMBR & ".cod," & auxTableNameEQUMBR & ".equcod" _
                                                & "," & auxTableNameEQU & ".miembrosgrpcod" _
                                                & "," & auxTableNameEQUMBR & ".mbrtypecod"
                        For Each auxValue As KeyValuePair(Of Integer, prvColumn) In m_TablesEntity(enumEntityType.coEQUMBREMP).TabColumns
                            Select Case auxValue.Value.FieRemoteDsc.ToUpper
                                Case "EQUMBRCOD", ""
                                Case Else
                                    auxSelect &= "," & auxTableNameEQUMBREMP & "." & auxValue.Value.FieLocalDsc
                            End Select
                        Next
                        For Each auxValue As KeyValuePair(Of Integer, prvColumn) In m_TablesEntity(enumEntityType.coEQUMBRUND).TabColumns
                            Select Case auxValue.Value.FieRemoteDsc.ToUpper
                                Case "EQUMBRCOD", ""
                                Case Else
                                    auxSelect &= "," & auxTableNameEQUMBRUND & "." & auxValue.Value.FieLocalDsc
                            End Select
                        Next
                        auxSelect &= " FROM " & auxTableNameEQUMBR & " " _
                                    & " LEFT JOIN " & auxTableNameEQU & " ON " & auxTableNameEQUMBR & ".equcod=" & auxTableNameEQU & ".cod" _
                                    & " LEFT JOIN " & auxTableNameEQUMBRUND & " ON " & auxTableNameEQUMBR & ".cod=" & auxTableNameEQUMBRUND & ".equmbrcod" _
                                    & " LEFT JOIN " & auxTableNameEQUMBREMP & " ON " & auxTableNameEQUMBR & ".cod=" & auxTableNameEQUMBREMP & ".equmbrcod" _
                                    & " WHERE " _
                                    & " " & auxTableNameEQUMBR & ".cod > 0"
                    Case enumEntityType.coEQUMBREMP, enumEntityType.coEQUMBRUND
                    Case Else
                        Dim auxFieldList As New List(Of String)
                        For Each auxColumn As prvColumn In pTable.TabColumns.Values
                            If auxColumn.FieRemoteDsc <> "" Then
                                auxFieldList.Add(auxColumn.FieRemoteDsc)
                            End If

                        Next
                        auxSelect = "SELECT " & m_Conn.gFieldDB_GetString(auxFieldList) _
                                              & " FROM " & pTable.TabRemoteDsc _
                                              & " WHERE cod > 0"
                End Select
                Return auxSelect
            End If

        End Function
        Private Function gReplication_GetLocalTableQuery(ByVal pTable As prvTable, _
                                                         ByVal pQueryOne As Boolean) As String
            Dim auxSelect As String = ""
            Select Case pTable.TabBuildType
                Case enumEntityType.coEQUMBR
                    Dim auxTableNameEQU As String = m_TablesEntity(enumEntityType.coEQU).TabLocalDsc
                    Dim auxTableNameEQUMBR As String = m_TablesEntity(enumEntityType.coEQUMBR).TabLocalDsc
                    Dim auxTableNameEQUMBRUND As String = m_TablesEntity(enumEntityType.coEQUMBRUND).TabLocalDsc
                    Dim auxTableNameEQUMBREMP As String = m_TablesEntity(enumEntityType.coEQUMBREMP).TabLocalDsc
                    auxSelect = ""
                    auxSelect &= "SELECT " & auxTableNameEQUMBR & ".cod," & auxTableNameEQUMBR & ".equcod" _
                                            & "," & auxTableNameEQU & ".miembrosgrpcod" _
                                            & "," & auxTableNameEQUMBR & ".mbrtypecod"
                    For Each auxValue As KeyValuePair(Of Integer, prvColumn) In m_TablesEntity(enumEntityType.coEQUMBREMP).TabColumns
                        Select Case auxValue.Value.FieLocalDsc.ToUpper
                            Case "EQUMBRCOD", ""
                            Case Else
                                auxSelect &= "," & auxTableNameEQUMBREMP & "." & auxValue.Value.FieLocalDsc
                        End Select
                    Next
                    For Each auxValue As KeyValuePair(Of Integer, prvColumn) In m_TablesEntity(enumEntityType.coEQUMBRUND).TabColumns
                        Select Case auxValue.Value.FieLocalDsc.ToUpper
                            Case "EQUMBRCOD", ""
                            Case Else
                                auxSelect &= "," & auxTableNameEQUMBRUND & "." & auxValue.Value.FieLocalDsc
                        End Select
                    Next
                    auxSelect &= " FROM " & auxTableNameEQUMBR & " " _
                                & " LEFT JOIN " & auxTableNameEQU & " ON " & auxTableNameEQUMBR & ".equcod=" & auxTableNameEQU & ".cod" _
                                & " LEFT JOIN " & auxTableNameEQUMBRUND & " ON " & auxTableNameEQUMBR & ".cod=" & auxTableNameEQUMBRUND & ".equmbrcod" _
                                & " LEFT JOIN " & auxTableNameEQUMBREMP & " ON " & auxTableNameEQUMBR & ".cod=" & auxTableNameEQUMBREMP & ".equmbrcod" _
                                & " WHERE " _
                                & " " & auxTableNameEQUMBR & ".equcod "
                    If pQueryOne Then
                        auxSelect &= " ="
                    Else
                        auxSelect &= " >0"
                    End If

                    'auxSelect = "SELECT EQUMBR.cod,EQUMBR.mbrtypecod,EQUMBR.equcod " _
                    '                & ",EQUMBRUND.gruporesp,EQUMBRUND.grupomiembros,EQUMBRUND.grupoprjver,EQUMBRUND.grupombrdir" _
                    '                & ",EQUMBRUND.undcod" _
                    '                & ",EQUMBREMP.empcod" _
                    '                & " FROM EQUMBR " _
                    '                & " LEFT JOIN EQU ON EQUMBR.equcod=EQU.cod " _
                    '                & " LEFT JOIN EQUMBRUND ON EQUMBR.cod= EQUMBRUND.equmbrcod" _
                    '                & " LEFT JOIN EQUMBREMP ON EQUMBR.cod= EQUMBREMP.equmbrcod" _
                    '                & " WHERE EQUMBR.cod > 0"
                Case enumEntityType.coEQUMBREMP, enumEntityType.coEQUMBRUND
                Case Else
                    Dim auxFieldList As New List(Of String)
                    For Each auxColumn As prvColumn In pTable.TabColumns.Values
                        If auxColumn.FieLocalDsc <> "" Then
                            auxFieldList.Add(auxColumn.FieLocalDsc)
                        End If
                    Next
                    auxSelect = "SELECT " & m_Conn.gFieldDB_GetString(auxFieldList) _
                                                      & " FROM " & pTable.TabLocalDsc _
                                                      & " WHERE cod "
                    If pQueryOne Then
                        auxSelect &= " ="
                    Else
                        auxSelect &= " >0"
                    End If
            End Select
            Return auxSelect
        End Function
        Private Function gReplication_GetChangesWithUpdate(ByVal pTable As prvTable) As String
            'Busca los campos relacionados en otras tablas para reemplazar
            Dim auxUpdate As String = ""
            Dim auxDeclareVar As String = ""
            Dim auxInsertVar As String = ""
            Dim auxInsertFields As String = ""
            Dim auxVarName As String = ""
            Dim auxTable_New As String = ""

            For Each auxColumn As prvColumn In pTable.TabColumns.Values
                auxVarName = "@var_" & auxColumn.FieLocalDsc
                Select Case auxColumn.FieType
                    Case hrcDataTypes.coBooleanType
                        auxTable_New &= auxColumn.FieLocalDsc & " BIT,"
                        auxInsertFields &= auxColumn.FieLocalDsc & ","
                        auxInsertVar &= "CASE WHEN " & auxColumn.FieLocalDsc & "=" & auxVarName & " THEN NULL ELSE " & auxColumn.FieLocalDsc & " END" & ","
                        auxDeclareVar &= " DECLARE " & auxVarName & " BIT SET " & auxVarName & "=" & "{#" & auxColumn.FieLocalDsc & "#}"
                        auxUpdate &= auxColumn.FieLocalDsc & "=" & auxVarName & ","
                    Case hrcDataTypes.coDateType
                        auxTable_New &= auxColumn.FieLocalDsc & " DATE,"
                        auxInsertFields &= auxColumn.FieLocalDsc & ","
                        auxInsertVar &= "CASE WHEN " & auxColumn.FieLocalDsc & "=" & auxVarName & " THEN NULL ELSE " & auxColumn.FieLocalDsc & " END" & ","
                        auxDeclareVar &= " DECLARE " & auxVarName & " DATE SET " & auxVarName & "=" & "{#" & auxColumn.FieLocalDsc & "#}"
                        auxUpdate &= auxColumn.FieLocalDsc & "=" & auxVarName & ","
                    Case hrcDataTypes.coStringType
                        auxTable_New &= auxColumn.FieLocalDsc & " VARCHAR(" & auxColumn.FieLength & "),"
                        auxInsertFields &= auxColumn.FieLocalDsc & ","
                        auxInsertVar &= "CASE WHEN " & auxColumn.FieLocalDsc & "=" & auxVarName & " THEN NULL ELSE " & auxColumn.FieLocalDsc & " END" & ","
                        auxDeclareVar &= " DECLARE " & auxVarName & " VARCHAR(" & auxColumn.FieLength & ") SET " & auxVarName & "=" & "{#" & auxColumn.FieLocalDsc & "#}"
                        auxUpdate &= auxColumn.FieLocalDsc & "=" & auxVarName & ","
                    Case hrcDataTypes.coNumberType
                        auxTable_New &= auxColumn.FieLocalDsc & " INT,"
                        auxInsertFields &= auxColumn.FieLocalDsc & ","
                        auxInsertVar &= "CASE WHEN " & auxColumn.FieLocalDsc & "=" & auxVarName & " THEN NULL ELSE " & auxColumn.FieLocalDsc & " END" & ","
                        auxDeclareVar &= " DECLARE " & auxVarName & " INT SET " & auxVarName & "=" & "{#" & auxColumn.FieLocalDsc & "#}"
                        auxUpdate &= auxColumn.FieLocalDsc & "=" & auxVarName & ","
                End Select
            Next
            If auxUpdate = "" Then
                Return ""
                Exit Function
            End If
            auxUpdate = Left(auxUpdate, auxUpdate.Length - 1)
            auxUpdate = " UPDATE " & pTable.TabLocalDsc & " SET " & auxUpdate _
                & " WHERE cod ={#COD#}" ' & auxCod
            If auxTable_New <> "" Then
                auxInsertVar = Left(auxInsertVar, auxInsertVar.Length - 1)
                auxInsertFields = Left(auxInsertFields, auxInsertFields.Length - 1)
                auxTable_New = Left(auxTable_New, auxTable_New.Length - 1)
                auxUpdate = "DECLARE @tabla_new TABLE(" & auxTable_New & ")" _
                                  & " " & auxDeclareVar _
                                  & " INSERT INTO @tabla_new (" & auxInsertFields & ")" _
                                  & " (SELECT " & auxInsertVar _
                                  & " FROM " & pTable.TabLocalDsc & " WHERE cod ={#COD#})" _
                                  & " " & auxUpdate _
                                  & " SELECT * FROM @tabla_new "
            End If
            auxUpdate = "BEGIN TRAN " _
                & auxUpdate _
                & "COMMIT TRAN "
            Return auxUpdate
        End Function
        Public Function gReplication_Replicate() As DataTable
            Dim auxConn As clsHrcConnClient = m_Conn
            auxConn.gConn_Open()
            If gReplication_Load(m_Class, m_Remote1BasCod, auxConn) Then
                gReplication_Start(m_Class)
            End If
            m_Class.Conn.gConn_Close()
            auxConn.gConn_Close()
            auxConn = Nothing
            Return m_DTChanges
        End Function
        Private Sub gReplication_Start(ByVal pClass As clshrcGeneral)
            'auxConn.gConn_Open()
            m_DTChanges = gReplication_ChangesTable_Create()
            'Las tablas que tienen datos obligatorios linkeados, deben ir al final.
            'Replica en 1 pasada
            'Inicializa la lista de campos a replicar
            '1. 
            'Si existe -> actualizar la tabla destino (USA UPDATE TRADICIONAL). Si hay valores aún no replicados (toma como -1 (nulo))
            'Si no existe -> inserta el valor de tabla (USA INSERT TRADICIONAL). Si hay valores aún no replicados (toma como -1 (nulo))
            '2. Recorre los valores nulos del registro (pendientes de insercion-estan en REL, pero <0).
            '   a. consulta la tabla origen y llama a la subrutina de replicación de ese registro
            '   b. Al volver, reemplaza el valor con el nuevo de REL. Y arma la sentencia de update.
            '   c. Marca al campo como update
            '3. Una vez recorrido, ejecuta el update con la lista de campos a replicar.
            '4. Ejecuta POST_ACTION.
            If m_Remote1Conn.isConnected = False Then
                gSys_DebugLogAdd("Replicate status:" & m_Remote1Conn.LastErrorDescription & ".isConnected:" & m_Remote1Conn.isConnected)
            End If

            Dim auxDateTime As DateTime = m_SouceLastReplicationOK
            If auxDateTime = Nothing Then
                auxDateTime = New Date(2000, 1, 1)
            End If
            If m_Remote1Conn.isConnected Then
                'Analiza las entidades que debe crear.
                'En caso que no existan las crea, sin datos. Solo para guardar el valor.
                Dim auxDT As New DataTable
                auxDT.Columns.Add(New DataColumn("entitytype", System.Type.GetType("System.Int32")))
                auxDT.Columns.Add(New DataColumn("EntityID", System.Type.GetType("System.Int32")))
                auxDT.Columns.Add(New DataColumn("Depempcod", System.Type.GetType("System.Int32")))
                auxDT.Columns.Add(New DataColumn("Depundcod", System.Type.GetType("System.Int32")))
                'auxDT.Columns.Add(New DataColumn("eventgenerated", System.Type.GetType("System.Boolean")))
                Dim auxStack As New Stack(Of DataRow)

                'Dim auxItemsAnalyzed As New SortedList(Of enumEntityType, Integer)
                'auxItemsAnalyzed.Add(enumEntityType.coEQUROL, -1)
                'auxItemsAnalyzed.Add(enumEntityType.coEQUMBR, -1)
                'auxItemsAnalyzed.Add(enumEntityType.coEQU, -1)
                'auxItemsAnalyzed.Add(enumEntityType.coUNDROL, -1)
                'auxItemsAnalyzed.Add(enumEntityType.coUND, -1)
                'auxItemsAnalyzed.Add(enumEntityType.coEMP, -1)

                Dim auxTablesToReplicate As New List(Of prvTable)
                auxTablesToReplicate.Add(m_TablesEntity(enumEntityType.coEMP))
                auxTablesToReplicate.Add(m_TablesEntity(enumEntityType.coUND))
                auxTablesToReplicate.Add(m_TablesEntity(enumEntityType.coUNDROL))
                auxTablesToReplicate.Add(m_TablesEntity(enumEntityType.coEQU))
                auxTablesToReplicate.Add(m_TablesEntity(enumEntityType.coEQUMBR))
                auxTablesToReplicate.Add(m_TablesEntity(enumEntityType.coEQUROL))
                For Each auxTable In m_Tables.Values
                    Select Case auxTable.TabBuildType
                        Case enumEntityType.coUndefined
                            auxTablesToReplicate.Add(auxTable)
                    End Select
                Next
                'Dim auxCurrentEntityType As enumEntityType
                Dim auxCurrentEntityID As Integer
                Dim auxRows() As DataRow
                Dim auxNewRow As DataRow
                ' Dim auxTable As prvTable
                Dim auxBagValuesNewValues As clshrcBagValues
                For Each auxTable As prvTable In auxTablesToReplicate ' auxItemsAnalyzed.Keys
                    If auxTable.TabEntityType < 1 Then
                        'If m_TablesEntity.IndexOfKey(auxCurrentEntityType) = -1 Then
                    Else
                        ' auxTable = m_TablesEntity(auxCurrentEntityType)
                        If auxTable.DT_Remote IsNot Nothing _
                            And (auxTable.DT_Local IsNot Nothing Or auxTable.DT_REL IsNot Nothing) Then
                            For Each auxRow As DataRow In auxTable.DT_Remote.Rows
                                If auxTable.DT_REL Is Nothing Then
                                    'No controla por el numero de relacion, sino por el numero local
                                    auxRows = auxTable.DT_Local.Select("cod=" & auxRow("cod"))
                                Else
                                    auxRows = auxTable.DT_REL.Select("cod=" & auxRow("cod"))
                                End If

                                If auxRows.Count = 0 Then
                                    auxBagValuesNewValues = m_Conn.gField_GetBagValuesFromArray(auxRow.ItemArray, auxRow.Table.Columns)
                                    'Elimina los datos relacionados a otras entidades
                                    'Inserta en la tabla local
                                    auxNewRow = auxTable.DT_Local.NewRow
                                    For Each auxField As prvColumn In auxTable.TabColumns.Values
                                        Select Case auxField.FieBuildType
                                            Case enumEntityFieldType.coUndefined
                                                If auxField.FieRemoteDsc <> "" And auxField.FieLocalDsc <> "" Then
                                                    auxNewRow(auxField.FieLocalDsc) = auxRow(auxField.FieRemoteDsc)
                                                End If
                                            Case Else
                                                auxBagValuesNewValues.gValue_Clear(auxField.FieLocalDsc)
                                        End Select
                                    Next
                                    auxCurrentEntityID = m_Class.gReplication_DataUpdate(auxTable.TabEntityType, enumActionType.coConfirmInsert, auxBagValuesNewValues)
                                    gSys_DebugLogAdd("Replicando NEW-" & auxTable.TabLocalDsc & "-" & auxRow("COD") & "->" & auxCurrentEntityID)
                                    auxTable.DT_Local.Rows.Add(auxNewRow)
                                    'Inserta en QRPL_REL
                                    If auxTable.DT_REL IsNot Nothing Then
                                        auxNewRow = auxTable.DT_REL.NewRow
                                        auxNewRow("cod") = auxCurrentEntityID
                                        auxNewRow("qrpl_bascod") = m_Remote1BasCod
                                        auxNewRow("qrpl_cod") = auxRow("cod")
                                        auxTable.DT_REL.Rows.Add(auxNewRow)
                                    End If
                                    If auxTable.TabEntityRelType > 0 Then
                                        Dim auxBagValuesNewValuesRPL As New clshrcBagValues
                                        auxBagValuesNewValuesRPL.gValue_Add("cod", auxCurrentEntityID)
                                        auxBagValuesNewValuesRPL.gValue_Add("qrpl_bascod", m_Remote1BasCod)
                                        auxBagValuesNewValuesRPL.gValue_Add("qrpl_cod", auxRow("cod"))
                                        m_Class.gReplication_DataUpdate(auxTable.TabEntityRelType, enumActionType.coConfirmInsert, auxBagValuesNewValuesRPL)
                                    End If
                                    Select Case auxTable.TabBuildType
                                        Case enumEntityType.coEMP
                                            gEntityEMP_PostAction(pClass, enumActionType.coConfirmInsert, auxBagValuesNewValues, auxBagValuesNewValues)
                                        Case enumEntityType.coUND
                                            gEntityUND_PostAction(pClass, enumActionType.coConfirmInsert, auxBagValuesNewValues, auxBagValuesNewValues)
                                        Case enumEntityType.coEQU
                                            gEntityEQU_PostAction(pClass, enumActionType.coConfirmInsert, auxBagValuesNewValues, auxBagValuesNewValues)
                                        Case enumEntityType.coEQUMBR
                                            'gEntityEQUMBR_PostAction(enumActionType.coConfirmInsert, auxBagValuesNewValues, auxBagValuesNewValues)
                                        Case enumEntityType.coUNDROL
                                            'gEntityUNDROL_PostAction(enumActionType.coConfirmInsert, auxBagValuesNewValues, auxBagValuesNewValues)
                                        Case enumEntityType.coEQUROL
                                        Case enumEntityType.coEQUMBREMP, enumEntityType.coEQUMBRUND
                                    End Select
                                    gReplication_ChangesTable_AddChange(pEntityBuildType:=auxTable.TabBuildType, _
                                              pEntityType:=auxTable.TabEntityType, _
                                              pActionType:=enumActionType.coConfirmInsert, _
                                              pOldValues:=auxBagValuesNewValues, _
                                              pNewValues:=auxBagValuesNewValues)
                                End If
                            Next
                        End If
                    End If
                Next
                auxDateTime = Nothing
                For Each auxTable In m_Tables.Values
                    If auxTable.TabIsReplicable Then
                        Try
                            gReplication_TableWork(pClass, auxTable, auxDateTime, -1, m_Conn)
                        Catch ex As Exception
                            gDebugSystem_Add("Replication exception:" & ex.Message)
                        End Try

                    End If

                    'Select Case auxTable.TabBuildType
                    '    Case enumEntityType.coEQUMBR
                    '    Case Else

                    '                    End Select
                Next
                If m_DTChanges IsNot Nothing Then
                    Dim auxOldValues As clshrcBagValues
                    Dim auxNewValues As clshrcBagValues

                    For Each auxRow As DataRow In m_DTChanges.Rows
                        auxOldValues = New clshrcBagValues(auxRow("OldValues").ToString)
                        auxNewValues = New clshrcBagValues(auxRow("NewValues").ToString)
                        Select Case auxRow("entityBuildType")
                            Case enumEntityType.coEMP
                                gEntityEMP_PostAction(pClass, auxRow("actiontype"), auxOldValues, auxNewValues)
                            Case enumEntityType.coUND
                                gEntityUND_PostAction(pClass, auxRow("actiontype"), auxOldValues, auxNewValues)
                            Case enumEntityType.coEQU
                                gEntityEQU_PostAction(pClass, auxRow("actiontype"), auxOldValues, auxNewValues)
                            Case enumEntityType.coEQUMBR
                                If m_EQUMBRChanges.IndexOf(auxNewValues.gValue_Get("equcod")) = -1 Then
                                    m_EQUMBRChanges.Add(auxNewValues.gValue_Get("equcod"))
                                End If
                            Case enumEntityType.coUNDROL
                                gEntityUNDROL_PostAction(auxRow("actiontype"), auxOldValues, auxNewValues)
                            Case enumEntityType.coEQUROL
                                gEntityEQUROL_PostAction(auxRow("actiontype"), auxOldValues, auxNewValues)
                        End Select
                    Next
                    For Each auxEquCod As Integer In m_EQUMBRChanges
                        gEntityEQUMBR_PostAction(pClass, auxEquCod)
                    Next
                End If
            End If
            'pConn.gConn_Close()
        End Sub
        Public Sub gReplication_Stop()

        End Sub
        Public Function gEntityUNDROL_PostAction(ByVal pAction As enumActionType, _
                                       ByVal pOldValues As clshrcBagValues, _
                                       ByVal pNewValues As clshrcBagValues) As String
            Try
                'gReplication_ChangesTable_AddChange(pEntityType:=enumEntityType.coUNDROL, _
                '                 pActionType:=pAction, _
                '                 pOldValues:=pOldValues, _
                '                 pNewValues:=pNewValues)

                Dim auxReturn As String = ""
            Catch ex As Exception
            End Try
        End Function
        Public Function gEntityEQUROL_PostAction(ByVal pAction As enumActionType, _
                                    ByVal pOldValues As clshrcBagValues, _
                                    ByVal pNewValues As clshrcBagValues) As String
            Try
                Dim auxReturn As String = ""
            Catch ex As Exception
            End Try
        End Function
        Public Function gEntity_AddItemChanged(ByVal pEntityType As Integer, _
                                            ByVal pActionType As enumActionType, _
                                         ByVal pOldBagValues As clshrcBagValues, _
                                         ByVal pNewBagValues As clshrcBagValues) As String

            pOldBagValues.gValue_Clear("QSECDATETIME")
            Dim auxEntityBuildType As enumEntityType = enumEntityType.coUndefined
            If m_LocalEntityToBuildType.IndexOfKey(pEntityType) <> -1 Then
                auxEntityBuildType = m_LocalEntityToBuildType(pEntityType)
            End If
            Dim pClass As clshrcGeneral = m_Class
            pClass.Conn.gConn_Open()
            If pOldBagValues.Values.Count <> 0 Then
                Select Case auxEntityBuildType
                    Case enumEntityType.coUND
                        gEntityUND_PostAction(pClass, pActionType, pOldBagValues, pNewBagValues)
                    Case enumEntityType.coEMP
                        gEntityEMP_PostAction(pClass, pActionType, pOldBagValues, pNewBagValues)
                    Case enumEntityType.coEQU
                        gEntityEQU_PostAction(pClass, pActionType, pOldBagValues, pNewBagValues)

                End Select
                'aGREGAR CAMBIOS DESPUES(!) de llamar a POSTACTION, porque puede ser que modifiquen valores (como en EMP, agrega SECCOD anterior y posterior
                gReplication_ChangesTable_AddChange(pEntityBuildType:=auxEntityBuildType, _
                                          pEntityType:=pEntityType, _
                                          pActionType:=pActionType, _
                                          pOldValues:=pOldBagValues, _
                                          pNewValues:=pNewBagValues)
            End If
            If auxEntityBuildType = enumEntityType.coEQU Then
                gEntityEQUMBR_PostAction(pClass, pNewBagValues.gValue_Get("cod"))
            End If
            pClass.Conn.gConn_Close()
            Return ""
        End Function
        Private Function gEntityUND_PostAction(ByVal pClass As clshrcGeneral, _
                                          ByVal pAction As enumActionType, _
                                         ByVal pOldValues As clshrcBagValues, _
                                         ByVal pNewValues As clshrcBagValues) As String
            Try
                'gReplication_ChangesTable_AddChange(pEntityType:=enumEntityType.coUND, _
                '                 pActionType:=pAction, _
                '                 pOldValues:=pOldValues, _
                '                 pNewValues:=pNewValues)

                Dim auxReturn As String = ""
                If pNewValues Is Nothing Then
                    pNewValues = New clshrcBagValues
                End If
                If pOldValues Is Nothing Then
                    pOldValues = New clshrcBagValues
                End If

                Dim auxCod As Integer = pClass.Conn.gField_GetInt(pNewValues.gValue_Get("cod"), -1)
                If auxCod < 1 Then
                    gSys_DebugLogAdd("Error-No UND cod:" & auxCod)
                    Exit Function
                End If
                Select Case pAction
                    Case enumActionType.coConfirmModify, enumActionType.coConfirmInsert, enumActionType.coConfirmDelete

                        'Dim auxStaticRow As DataRow = hrcEntityDT_UND_FindByKey(auxCod)
                        Dim auxStaticRow As DataRow = pClass.gReplication_DataGetByCod(m_EntityTypes(enumEntityType.coUND), auxCod)
                        If auxStaticRow Is Nothing Then
                            gSys_DebugLogAdd("Error-No UND Row:" & auxCod)
                        Else
                            'pOldValues.gValues_ClearNoValues()
                            Dim auxMiembrosGrpCod_Change As Object = Nothing
                            Dim auxGrpCodResp_Change As Object = Nothing
                            Dim auxGrpCodprjver_Change As Object = Nothing
                            Dim auxGrpCodMbrDir_Change As Object = Nothing
                            Dim auxInitialize As Boolean = False
                            If pAction = enumActionType.coConfirmInsert Then
                                auxInitialize = True
                            End If

                            Dim auxMiembrosGrpCod As Integer = pClass.Conn.gField_GetInt(auxStaticRow("miembrosgrpcod"), -1)
                            Dim auxGrpCodResp As Integer = pClass.Conn.gField_GetInt(auxStaticRow("grpcodresp"), -1)
                            Dim auxGrpCodprjver As Integer = pClass.Conn.gField_GetInt(auxStaticRow("grpcodprjver"), -1)
                            Dim auxGrpCodMbrDir As Integer = -1
                            auxGrpCodMbrDir = pClass.Conn.gField_GetInt(auxStaticRow("grpcodmbrdir"), -1)

                            If auxMiembrosGrpCod < 1 Or auxGrpCodResp < 1 Or auxGrpCodMbrDir < 1 Or auxGrpCodprjver < 1 Then
                                auxInitialize = True
                            End If
                            'Analiza si fue una baja
                            Dim auxIsBaja As Boolean = pClass.Conn.gField_GetBoolean(pNewValues.gValue_Get("baja"))
                            If pOldValues.gValue_Get("baja") IsNot Nothing Then
                                Dim auxBaja_New As Boolean = pClass.Conn.gField_GetBoolean(pNewValues.gValue_Get("baja"), False)
                                Dim auxBaja_Old As Boolean = pClass.Conn.gField_GetBoolean(pOldValues.gValue_Get("baja"), False)
                                If pAction = enumActionType.coConfirmDelete Then
                                    auxBaja_New = True
                                End If
                                If auxBaja_New Then
                                    auxIsBaja = True
                                    auxInitialize = False
                                Else
                                    auxInitialize = True
                                End If
                                Select Case auxBaja_New
                                    Case True
                                        If auxMiembrosGrpCod > 0 Then
                                            m_Sec.gGroup_Disabled(auxMiembrosGrpCod)
                                            auxMiembrosGrpCod_Change = -1
                                        End If
                                        If auxGrpCodResp > 0 Then
                                            m_Sec.gGroup_Disabled(auxGrpCodResp)
                                            auxGrpCodResp_Change = -1
                                        End If
                                        If auxGrpCodprjver > 0 Then
                                            m_Sec.gGroup_Disabled(auxGrpCodprjver)
                                            auxGrpCodprjver_Change = -1
                                        End If
                                        If auxGrpCodMbrDir > 0 Then
                                            m_Sec.gGroup_Disabled(auxGrpCodMbrDir)
                                            auxGrpCodMbrDir_Change = -1
                                        End If
                                    Case False
                                        If auxMiembrosGrpCod > 0 Then
                                            m_Sec.gGroup_Enabled(auxMiembrosGrpCod)
                                        End If
                                        If auxGrpCodResp > 0 Then
                                            m_Sec.gGroup_Enabled(auxGrpCodResp)
                                        End If
                                        If auxGrpCodprjver > 0 Then
                                            m_Sec.gGroup_Enabled(auxGrpCodprjver)
                                        End If
                                        If auxGrpCodMbrDir > 0 Then
                                            m_Sec.gGroup_Enabled(auxGrpCodMbrDir)
                                        End If
                                End Select

                            End If
                            If pOldValues.gValue_Get("dsc") IsNot Nothing Then
                                auxInitialize = True
                            End If
                            If auxInitialize Then
                                Dim auxGroupName As String = pNewValues.gValue_Get("dsc", "").ToString
                                'Cambio de nombre
                                auxGrpCodMbrDir = pClass.Conn.gField_GetInt(auxStaticRow("grpcodmbrdir"), -1)
                                If auxGrpCodMbrDir < 1 Then
                                    If auxIsBaja = False Then
                                        auxGrpCodMbrDir = m_Sec.gGroup_Create("Und-" & auxGroupName & "-Miembros directos", -1, -1, pClass.Conn.gField_GetBoolean(pNewValues.gValue_Get("baja"), False))
                                        auxGrpCodMbrDir_Change = auxGrpCodMbrDir
                                        pOldValues.gValue_Add("grpcodmbrdir", -1)
                                        pNewValues.gValue_Add("grpcodmbrdir", auxGrpCodMbrDir)
                                    End If
                                Else
                                    m_Sec.gGroup_Rename(auxGrpCodMbrDir, "Und-" & auxGroupName & "-Miembros directos")
                                End If
                                '//////////////////////
                                auxMiembrosGrpCod = pClass.Conn.gField_GetInt(auxStaticRow("miembrosgrpcod"), -1)
                                If auxMiembrosGrpCod < 1 Then
                                    If auxIsBaja = False Then
                                        auxMiembrosGrpCod = m_Sec.gGroup_Create("Und-" & auxGroupName & "-Miembros", -1, -1, pClass.Conn.gField_GetBoolean(pNewValues.gValue_Get("baja"), False))
                                        auxMiembrosGrpCod_Change = auxMiembrosGrpCod
                                        pOldValues.gValue_Add("miembrosgrpcod", -1)
                                        pNewValues.gValue_Add("miembrosgrpcod", auxMiembrosGrpCod)
                                    End If
                                Else
                                    m_Sec.gGroup_Rename(auxMiembrosGrpCod, "Und-" & auxGroupName & "-Miembros")
                                End If
                                '/////////////////////
                                auxGrpCodResp = pClass.Conn.gField_GetInt(auxStaticRow("grpcodresp"), -1)
                                If auxGrpCodResp < 1 Then
                                    If auxIsBaja = False Then
                                        auxGrpCodResp = m_Sec.gGroup_Create("Und-" & auxGroupName & "-Responsables", -1, -1, pClass.Conn.gField_GetBoolean(pNewValues.gValue_Get("baja"), False))
                                        auxGrpCodResp_Change = auxGrpCodResp
                                        pOldValues.gValue_Add("grpcodresp", -1)
                                        pNewValues.gValue_Add("grpcodresp", auxGrpCodResp)
                                    End If
                                Else
                                    m_Sec.gGroup_Rename(auxGrpCodResp, "Und-" & auxGroupName & "-Responsables")
                                End If
                                '/////////////////////
                                auxGrpCodprjver = pClass.Conn.gField_GetInt(auxStaticRow("grpcodprjver"), -1)
                                If auxGrpCodprjver < 1 Then
                                    If auxIsBaja = False Then
                                        auxGrpCodprjver = m_Sec.gGroup_Create("Und-" & auxGroupName & "-Superiores", -1, -1, pClass.Conn.gField_GetBoolean(pNewValues.gValue_Get("baja"), False))
                                        auxGrpCodprjver_Change = auxGrpCodprjver
                                        pOldValues.gValue_Add("grpcodprjver", -1)
                                        pNewValues.gValue_Add("grpcodprjver", auxGrpCodprjver)
                                    End If
                                Else
                                    m_Sec.gGroup_Rename(auxGrpCodprjver, "Und-" & auxGroupName & "-Superiores")
                                End If

                                If auxMiembrosGrpCod < 1 Or auxGrpCodResp < 1 Or auxGrpCodMbrDir < 1 Or auxGrpCodprjver < 1 Then
                                    gSys_DebugLogAdd("Warning UND:" & auxCod & "-Inconsistent groups:" & auxMiembrosGrpCod & "-" & auxGrpCodResp & "-" & auxGrpCodMbrDir & "-" & auxGrpCodprjver)
                                End If
                                If auxGrpCodMbrDir > 0 Then
                                    'Agregar los miembros directos en los miembros
                                    m_Sec.gGroup_AddGroup(auxMiembrosGrpCod, _
                                                               auxGrpCodMbrDir)
                                End If
                                'Agregar el grupo de responsable al grupo de superiores
                                m_Sec.gGroup_AddGroup(auxGrpCodprjver, auxGrpCodResp)
                            End If

                            Dim auxBagValues As New clshrcBagValues
                            auxBagValues.gValue_Add("cod", auxCod)
                            auxBagValues.gValue_Add("grpcodmbrdir", auxGrpCodMbrDir_Change)
                            auxBagValues.gValue_Add("miembrosgrpcod", auxMiembrosGrpCod_Change)
                            auxBagValues.gValue_Add("grpcodprjver", auxGrpCodprjver_Change)
                            auxBagValues.gValue_Add("grpcodresp", auxGrpCodResp_Change)
                            pClass.gReplication_DataUpdate(m_EntityTypes(enumEntityType.coUND), enumActionType.coConfirmModify, auxBagValues)

                            'm_Class.gEntity_UND_SystemUpdate(pcod:=auxCod, _
                            '                                  pgrpcodmbrdir:=auxGrpCodMbrDir_Change, _
                            '                                  pmiembrosgrpcod:=auxMiembrosGrpCod_Change, _
                            '                                  pgrpcodprjver:=auxGrpCodprjver_Change, _
                            '                                 pgrpcodresp:=auxGrpCodResp_Change)


                            'Cambio de responsable
                            If pOldValues.gValue_Get("resp") IsNot Nothing Then
                                Dim auxEmpRow As DataRow
                                Dim auxResp_Old As Integer = pClass.Conn.gField_GetInt(pOldValues.gValue_Get("resp"), -1)
                                Dim auxResp_New As Integer = pClass.Conn.gField_GetInt(pNewValues.gValue_Get("resp"), -1)
                                If auxResp_Old > 0 And auxResp_Old <> auxResp_New Then
                                    'auxEmpRow = hrcEntityDT_EMP_FindByKey(auxResp_Old)
                                    auxEmpRow = pClass.gReplication_DataGetByCod(m_EntityTypes(enumEntityType.coEMP), auxResp_Old)
                                    If auxEmpRow IsNot Nothing Then
                                        If auxEmpRow("cod") > 0 Then
                                            Dim auxRespSecCod As Integer = pClass.Conn.gField_GetInt(auxEmpRow("seccod"), -1)
                                            If auxRespSecCod > 0 Then
                                                m_Sec.gGroup_DelLogin(auxGrpCodResp, auxRespSecCod)
                                            End If
                                        End If
                                    End If
                                End If
                                If auxResp_New > 0 Then
                                    'Agrega el responsable al grupo de responsables
                                    'auxEmpRow = hrcEntityDT_EMP_FindByKey(auxResp_New)
                                    auxEmpRow = pClass.gReplication_DataGetByCod(m_EntityTypes(enumEntityType.coEMP), auxResp_New)
                                    If auxEmpRow IsNot Nothing Then
                                        If auxEmpRow("cod") > 0 Then
                                            Dim auxRespSecCod As Integer = pClass.Conn.gField_GetInt(auxEmpRow("seccod"), -1)
                                            If auxRespSecCod > 0 Then
                                                m_Sec.gGroup_AddLogin(auxGrpCodResp, auxRespSecCod)
                                            End If
                                        End If
                                    End If
                                End If
                            End If

                            'Cambio de unidad superior
                            If pOldValues.gValue_Get("undcodsup") IsNot Nothing Or auxInitialize Then
                                Dim auxUndRow As DataRow = Nothing
                                Dim auxUndCodSup_Old As Integer = pClass.Conn.gField_GetInt(pOldValues.gValue_Get("undcodsup"), -1)
                                Dim auxUndCodSup_New As Integer = pClass.Conn.gField_GetInt(pNewValues.gValue_Get("undcodsup"), -1)
                                'Trabaja con la ANTERIOR unidad superior
                                If auxUndCodSup_Old > 0 And auxUndCodSup_Old <> auxUndCodSup_New Then
                                    'auxUndRow = hrcEntityDT_UND_FindByKey(auxUndCodSup_Old)
                                    auxUndRow = pClass.gReplication_DataGetByCod(m_EntityTypes(enumEntityType.coUND), auxUndCodSup_Old)
                                    If auxUndRow IsNot Nothing Then
                                        If auxUndRow("cod") > 0 Then
                                            'Quita los miembros en el grupo de miembros de la unidad superior
                                            m_Sec.gGroup_DelGroup(pClass.Conn.gField_GetInt(auxUndRow("miembrosgrpcod"), -1), _
                                                                 auxMiembrosGrpCod)

                                            'Quita los superiores de la unidad superior a los superiores actuales
                                            m_Sec.gGroup_DelGroup(auxGrpCodprjver, _
                                                            pClass.Conn.gField_GetInt(auxUndRow("grpcodprjver"), -1))

                                        End If
                                    End If
                                End If

                                If auxUndCodSup_New > 0 Then
                                    'Trabaja con la NUEVA unidad superior
                                    'auxUndRow = hrcEntityDT_UND_FindByKey(auxUndCodSup_New)
                                    auxUndRow = pClass.gReplication_DataGetByCod(m_EntityTypes(enumEntityType.coUND), auxUndCodSup_New)
                                    If auxUndRow IsNot Nothing Then
                                        If auxUndRow("cod") > 0 Then
                                            'Agrega los miembros en el grupo de miembros de la unidad superior
                                            m_Sec.gGroup_AddGroup(pClass.Conn.gField_GetInt(auxUndRow("miembrosgrpcod"), -1), _
                                                                 auxMiembrosGrpCod)

                                            'Agrega los Superiores de la unidad superior a los superiores actuales
                                            m_Sec.gGroup_AddGroup(auxGrpCodprjver, _
                                                            pClass.Conn.gField_GetInt(auxUndRow("grpcodprjver"), -1))
                                        End If
                                    End If
                                End If
                            End If
                        End If
                        gEntityUNDROL_PostAction(enumActionType.coConfirmModify, pOldValues, pNewValues)

                        'Dim auxCod As Integer = pClass.Conn.gField_GetInt(pNewValues.gValue_Get("cod"), -1)

                End Select
            Catch ex As Exception
                gSys_DebugLogAdd("UND postAction:" & ex.Message)
            End Try

        End Function
        Private Function gEntityEQU_PostAction(ByVal pClass As clshrcGeneral, _
                                        ByVal pAction As enumActionType, _
                                        ByVal pOldValues As clshrcBagValues, _
                                        ByVal pNewValues As clshrcBagValues) As String
            Try
                'gReplication_ChangesTable_AddChange(pEntityType:=enumEntityType.coEQU, _
                '                  pActionType:=pAction, _
                '                  pOldValues:=pOldValues, _
                '                  pNewValues:=pNewValues)

                Dim auxReturn As String = ""
                If pNewValues Is Nothing Then
                    pNewValues = New clshrcBagValues
                End If
                If pOldValues Is Nothing Then
                    pOldValues = New clshrcBagValues
                End If
                Dim auxCod As Integer = pClass.Conn.gField_GetInt(pNewValues.gValue_Get("cod"), -1)
                Select Case pAction
                    Case enumActionType.coConfirmModify, enumActionType.coConfirmInsert, enumActionType.coConfirmDelete
                        'Dim auxStaticRow As DataRow = hrcEntityDT_EQU_FindByKey(auxCod)
                        Dim auxStaticRow As DataRow = pClass.gReplication_DataGetByCod(m_EntityTypes(enumEntityType.coEQU), auxCod)
                        If auxStaticRow Is Nothing Then ' auxDT.Rows.Count <> 0 Then
                            gSys_DebugLogAdd("Error-No EQU Row:" & auxCod)
                        Else
                            '                            pOldValues.gValues_ClearNoValues()
                            Dim auxMiembrosGrpCod_Change As Object = Nothing
                            Dim auxInitialize As Boolean = False

                            Dim auxMiembrosGrpCod As Integer = pClass.Conn.gField_GetInt(auxStaticRow("miembrosgrpcod"), -1)
                            If pAction = enumActionType.coConfirmInsert Then
                                auxInitialize = True
                            End If
                            Dim auxIsBaja As Boolean = pClass.Conn.gField_GetBoolean(pNewValues.gValue_Get("baja"))
                            If pOldValues.gValue_Get("baja") IsNot Nothing Then
                                Dim auxBaja_New As Boolean = pClass.Conn.gField_GetBoolean(pNewValues.gValue_Get("baja"), False)
                                Dim auxBaja_Old As Boolean = pClass.Conn.gField_GetBoolean(pOldValues.gValue_Get("baja"), False)
                                If pAction = enumActionType.coConfirmDelete Then
                                    auxBaja_New = True
                                End If
                                If auxBaja_New Then
                                    auxIsBaja = True
                                    auxInitialize = False
                                End If
                                Select Case auxBaja_New
                                    Case True
                                        If auxMiembrosGrpCod > 0 Then
                                            m_Sec.gGroup_Disabled(auxMiembrosGrpCod)
                                        End If
                                    Case False
                                        If auxMiembrosGrpCod > 0 Then
                                            m_Sec.gGroup_Enabled(auxMiembrosGrpCod)
                                        End If
                                End Select

                            End If

                            If pOldValues.gValue_Get("dsc") IsNot Nothing Then
                                auxInitialize = True
                            End If

                            If auxInitialize Then
                                Dim auxGroupName As String = pNewValues.gValue_Get("dsc", "").ToString
                                '/////////////
                                If auxMiembrosGrpCod < 1 Then
                                    If auxIsBaja = False Then
                                        auxMiembrosGrpCod = m_Sec.gGroup_Create("Equ-" & auxGroupName & "-Miembros directos", -1, -1, pClass.Conn.gField_GetBoolean(pNewValues.gValue_Get("baja"), False))
                                        auxMiembrosGrpCod_Change = auxMiembrosGrpCod
                                        auxInitialize = True
                                        pOldValues.gValue_Add("miembrosgrpcod", -1)
                                        pNewValues.gValue_Add("miembrosgrpcod", auxMiembrosGrpCod)
                                    End If
                                Else
                                    m_Sec.gGroup_Rename(auxMiembrosGrpCod, "Equ-" & auxGroupName & "-Miembros directos")
                                End If
                            End If
                            Dim auxBagValues As New clshrcBagValues
                            auxBagValues.gValue_Add("cod", auxCod)
                            auxBagValues.gValue_Add("miembrosgrpcod", auxMiembrosGrpCod_Change)
                            pClass.gReplication_DataUpdate(m_EntityTypes(enumEntityType.coEQU), enumActionType.coConfirmModify, auxBagValues)

                        End If
                        'gEntityEQU_PostActionMembers(auxCod)
                End Select
            Catch ex As Exception
                gSys_DebugLogAdd("EQU postAction-Exception:" & ex.Message)
            End Try

        End Function

        Private Function gEntityEQUMBR_PostAction(ByVal pClass As clshrcGeneral, _
                                                  ByVal pCod As Integer) As String
            Try
                Dim auxReturn As String = ""
                'If pOldValues Is Nothing Then
                '    pOldValues = New clshrcBagValues
                'End If


                Dim auxMiembrosGrpCod As Integer = -1
                Dim auxChanges As String = ""
                Dim auxCod As Integer = pCod ' pClass.Conn.gField_GetInt(pNewValues.gValue_Get("cod"), -1)
                'Dim auxTableNameEQU As String = m_TablesEntity(enumEntityType.coEQU).TabLocalDsc
                'Dim auxTableNameEQUMBR As String = m_TablesEntity(enumEntityType.coEQUMBR).TabLocalDsc
                'Dim auxTableNameEQUMBRUND As String = m_TablesEntity(enumEntityType.coEQUMBRUND).TabLocalDsc
                'Dim auxTableNameEQUMBREMP As String = m_TablesEntity(enumEntityType.coEQUMBREMP).TabLocalDsc
                'Dim auxDT As DataTable = m_Conn.gConn_Query("SELECT " & auxTableNameEQUMBR & ".cod," & auxTableNameEQUMBR & ".equcod" _
                '                        & "," & auxTableNameEQU & ".miembrosgrpcod" _
                '                        & "," & auxTableNameEQUMBR & ".mbrtypecod" _
                '                        & "," & auxTableNameEQUMBREMP & ".empcod" _
                '                        & "," & auxTableNameEQUMBRUND & ".undcod," & auxTableNameEQUMBRUND & ".gruporesp," & auxTableNameEQUMBRUND & ".grupomiembros," & auxTableNameEQUMBRUND & ".grupombrdir," & auxTableNameEQUMBRUND & ".grupoprjver" _
                '                        & " FROM " & auxTableNameEQUMBR & " " _
                '                        & " LEFT JOIN " & auxTableNameEQU & " ON " & auxTableNameEQUMBR & ".equcod=" & auxTableNameEQU & ".cod" _
                '                        & " LEFT JOIN " & auxTableNameEQUMBRUND & " ON " & auxTableNameEQUMBR & ".cod=" & auxTableNameEQUMBRUND & ".equmbrcod" _
                '                        & " LEFT JOIN " & auxTableNameEQUMBREMP & " ON " & auxTableNameEQUMBR & ".cod=" & auxTableNameEQUMBREMP & ".equmbrcod" _
                '                        & " WHERE " _
                '                        & " " & auxTableNameEQUMBR & ".equcod =" & auxCod)
                Dim auxDT As DataTable
                auxDT = pClass.Conn.gConn_Query(m_TablesEntity(enumEntityType.coEQUMBR).TabLocalQueryOne & auxCod)
                'Dim auxEQURow As DataRow = hrcEntityDT_EQU_FindByKey(auxCod)
                Dim auxSidList_New As New SortedList(Of Integer, List(Of Integer))
                Dim auxSidList_Old As New SortedList(Of Integer, List(Of Integer))
                Dim auxADGroupToAdd As New SortedList(Of String, String)

                Dim auxGrpCod As Integer = -1
                Dim auxDOMUSUcod As Integer = -1
                Dim auxADGroupParentName As String = ""
                Dim auxADGroupParentGUID As String = ""
                Dim auxADChildName As String = ""
                Dim auxADChildGUID As String = ""

                auxMiembrosGrpCod = pClass.Conn.gField_GetInt(auxDT.Rows(0)("miembrosgrpcod"), -1)
                If auxSidList_New.IndexOfKey(auxMiembrosGrpCod) = -1 Then
                    auxSidList_New.Add(auxMiembrosGrpCod, New List(Of Integer))
                End If
                If auxSidList_Old.IndexOfKey(auxMiembrosGrpCod) = -1 Then
                    auxSidList_Old.Add(auxMiembrosGrpCod, New List(Of Integer))
                End If
                Dim auxBagValuesNewValues As New clshrcBagValues
                Dim auxBagValuesOldValues As New clshrcBagValues
                '1. Primera toma la lista de miembros del grupo
                '2. Si no existe, lo agrega
                For Each auxMemberRow As DataRow In m_Sec.gGroup_GetMembers(auxMiembrosGrpCod).Rows
                    auxSidList_Old(auxMiembrosGrpCod).Add(auxMemberRow("sidcod"))
                Next
                For Each auxRow As DataRow In auxDT.Rows
                    Select Case CType(auxRow("mbrtypecod"), enumEntityType)
                        Case m_EntityTypes(enumEntityType.coEMP)
                            Dim auxEmpCod As Integer = pClass.Conn.gField_GetInt(auxRow("empcod"), -1)
                            If auxEmpCod > 0 Then
                                'Dim auxEMPRow As DataRow = hrcEntityDT_EMP_FindByKey(auxEmpCod)
                                Dim auxEMPRow As DataRow = pClass.gReplication_DataGetByCod(m_EntityTypes(enumEntityType.coEMP), auxEmpCod)
                                Dim auxSidCod As Integer = m_Sec.gLogin_GetSidCod(auxEMPRow("seccod"))
                                If auxSidList_New(auxMiembrosGrpCod).IndexOf(auxSidCod) = -1 Then
                                    If auxSidList_Old(auxMiembrosGrpCod).IndexOf(auxSidCod) = -1 Then
                                        m_Sec.gGroup_AddMember(auxMiembrosGrpCod, auxSidCod)
                                        auxBagValuesOldValues = pClass.Conn.gField_GetBagValuesFromArray(auxRow.ItemArray, auxRow.Table.Columns)
                                        auxBagValuesNewValues = auxBagValuesOldValues
                                        gReplication_ChangesTable_AddChange(pEntityBuildType:=enumEntityType.coEQUMBR, _
                                                                            pEntityType:=-1, _
                                                                            pActionType:=enumActionType.coConfirmInsert, _
                                                                            pOldValues:=auxBagValuesOldValues, _
                                                                            pNewValues:=auxBagValuesNewValues)
                                    End If
                                    auxSidList_New(auxMiembrosGrpCod).Add(auxSidCod)
                                End If

                            End If

                        Case m_EntityTypes(enumEntityType.coUND)
                            Dim auxUndCod As Integer = pClass.Conn.gField_GetInt(auxRow("undcod"), -1)
                            If auxUndCod > 0 Then
                                Dim auxSidCod As Integer = -1
                                auxADChildGUID = ""
                                auxADChildName = ""
                                auxGrpCod = -1
                                'Dim auxUndRow As DataRow = hrcEntityDT_UND_FindByKey(auxUndCod)
                                Dim auxUndRow As DataRow = pClass.gReplication_DataGetByCod(m_EntityTypes(enumEntityType.coUND), auxUndCod)
                                auxBagValuesOldValues = pClass.Conn.gField_GetBagValuesFromArray(auxRow.ItemArray, auxRow.Table.Columns)

                                auxBagValuesNewValues = New clshrcBagValues(auxBagValuesOldValues)
                                auxBagValuesOldValues.gValue_Clear("grupombrdir")
                                auxBagValuesOldValues.gValue_Clear("grupomiembros")
                                auxBagValuesOldValues.gValue_Clear("gruporesp")
                                auxBagValuesOldValues.gValue_Clear("grupoprjver")

                                'Miembros directos                                        
                                If pClass.Conn.gField_GetBoolean(auxRow("grupombrdir"), False) Then
                                    auxGrpCod = pClass.Conn.gField_GetInt(auxUndRow("grpcodmbrdir"), -1)
                                    auxSidCod = m_Sec.gGroup_GetSidCod(auxGrpCod)
                                    If auxSidList_New(auxMiembrosGrpCod).IndexOf(auxSidCod) = -1 Then
                                        If auxSidList_Old(auxMiembrosGrpCod).IndexOf(auxSidCod) = -1 Then
                                            auxBagValuesOldValues.gValue_Add("grupombrdir", 0)
                                            m_Sec.gGroup_AddMember(auxMiembrosGrpCod, auxSidCod)
                                            gReplication_ChangesTable_AddChange(pEntityBuildType:=enumEntityType.coEQUMBR, _
                                                                                pEntityType:=-1, _
                                                                                pActionType:=enumActionType.coConfirmInsert, _
                                                                                pOldValues:=auxBagValuesOldValues, _
                                                                                pNewValues:=auxBagValuesNewValues)
                                            auxBagValuesOldValues.gValue_Clear("grupombrdir")
                                        End If
                                        auxSidList_New(auxMiembrosGrpCod).Add(auxSidCod)
                                    End If
                                End If

                                If pClass.Conn.gField_GetBoolean(auxRow("grupomiembros"), False) Then
                                    auxGrpCod = pClass.Conn.gField_GetInt(auxUndRow("miembrosgrpcod"), -1)
                                    auxSidCod = m_Sec.gGroup_GetSidCod(auxGrpCod)
                                    If auxSidList_New(auxMiembrosGrpCod).IndexOf(auxSidCod) = -1 Then
                                        If auxSidList_Old(auxMiembrosGrpCod).IndexOf(auxSidCod) = -1 Then
                                            auxBagValuesOldValues.gValue_Add("grupomiembros", 0)
                                            m_Sec.gGroup_AddMember(auxMiembrosGrpCod, auxSidCod)
                                            gReplication_ChangesTable_AddChange(pEntityBuildType:=enumEntityType.coEQUMBR, _
                                                                              pEntityType:=-1, _
                                                                              pActionType:=enumActionType.coConfirmInsert, _
                                                                              pOldValues:=auxBagValuesOldValues, _
                                                                              pNewValues:=auxBagValuesNewValues)
                                            'auxBagValuesOldValues.gValue_Clear("grupombrdir")
                                            auxBagValuesOldValues.gValue_Clear("grupomiembros")
                                        End If
                                        auxSidList_New(auxMiembrosGrpCod).Add(auxSidCod)
                                    End If
                                End If

                                'Responsables

                                If pClass.Conn.gField_GetBoolean(auxRow("gruporesp"), False) Then
                                    auxGrpCod = pClass.Conn.gField_GetInt(auxUndRow("grpcodresp"), -1)
                                    auxSidCod = m_Sec.gGroup_GetSidCod(auxGrpCod)
                                    If auxSidList_New(auxMiembrosGrpCod).IndexOf(auxSidCod) = -1 Then
                                        If auxSidList_Old(auxMiembrosGrpCod).IndexOf(auxSidCod) = -1 Then
                                            auxBagValuesOldValues.gValue_Add("gruporesp", 0)
                                            m_Sec.gGroup_AddMember(auxMiembrosGrpCod, auxSidCod)
                                            gReplication_ChangesTable_AddChange(pEntityBuildType:=enumEntityType.coEQUMBR, _
                                                                               pEntityType:=-1, _
                                                                               pActionType:=enumActionType.coConfirmInsert, _
                                                                               pOldValues:=auxBagValuesOldValues, _
                                                                               pNewValues:=auxBagValuesNewValues)
                                            'auxBagValuesOldValues.gValue_Clear("grupombrdir")
                                            auxBagValuesOldValues.gValue_Clear("gruporesp")
                                        End If
                                        auxSidList_New(auxMiembrosGrpCod).Add(auxSidCod)
                                    End If

                                End If
                                'Superiores

                                If pClass.Conn.gField_GetBoolean(auxRow("grupoprjver"), False) Then
                                    auxGrpCod = pClass.Conn.gField_GetInt(auxUndRow("grpcodprjver"), -1)
                                    auxSidCod = m_Sec.gGroup_GetSidCod(auxGrpCod)
                                    If auxSidList_New(auxMiembrosGrpCod).IndexOf(auxSidCod) = -1 Then
                                        If auxSidList_Old(auxMiembrosGrpCod).IndexOf(auxSidCod) = -1 Then
                                            auxBagValuesOldValues.gValue_Add("grupoprjver", 0)
                                            m_Sec.gGroup_AddMember(auxMiembrosGrpCod, auxSidCod)
                                            gReplication_ChangesTable_AddChange(pEntityBuildType:=enumEntityType.coEQUMBR, _
                                                                              pEntityType:=-1, _
                                                                              pActionType:=enumActionType.coConfirmInsert, _
                                                                              pOldValues:=auxBagValuesOldValues, _
                                                                              pNewValues:=auxBagValuesNewValues)
                                            'auxBagValuesOldValues.gValue_Clear("grupombrdir")
                                            auxBagValuesOldValues.gValue_Clear("grupoprjver")
                                        End If

                                        auxSidList_New(auxMiembrosGrpCod).Add(auxSidCod)
                                    End If

                                End If

                            End If

                            For Each auxValue As KeyValuePair(Of Integer, prvColumn) In m_TablesEntity(enumEntityType.coEQUMBRUND).TabColumns
                                Select Case auxValue.Value.FieLocalDsc.ToUpper
                                    Case "EQUMBRCOD", "UNDCOD", "GRUPOMBRDIR", "GRUPORESP", "GRUPOPRJVER", "GRUPOMIEMBROS"
                                    Case Else
                                        auxBagValuesOldValues.gValue_Add(auxValue.Value.FieLocalDsc, auxRow(auxValue.Value.FieLocalDsc))
                                        gReplication_ChangesTable_AddChange(pEntityBuildType:=enumEntityType.coEQUMBR, _
                                                                          pEntityType:=-1, _
                                                                          pActionType:=enumActionType.coConfirmModify, _
                                                                          pOldValues:=auxBagValuesOldValues, _
                                                                          pNewValues:=auxBagValuesNewValues)
                                        auxBagValuesOldValues.gValue_Clear(auxValue.Value.FieLocalDsc)
                                End Select
                            Next
                    End Select
                Next
                Dim auxRows() As DataRow
                Dim auxExist As Boolean = False
                For Each auxValue As KeyValuePair(Of Integer, List(Of Integer)) In auxSidList_New
                    auxMiembrosGrpCod = auxValue.Key
                    For Each auxMemberSidCod As Integer In auxSidList_Old(auxMiembrosGrpCod)
                        auxExist = False
                        If auxValue.Value.IndexOf(auxMemberSidCod) <> -1 Then
                            auxExist = True
                        End If
                        If auxExist = False Then
                            gSys_DebugLogAdd("Deleting from group(" & auxMiembrosGrpCod & "-SID:" & auxMemberSidCod)
                            Select Case m_Sec.gSID_GetTypeCod(auxMemberSidCod)
                                Case m_Sec.enumSIDType.coGroup

                                    auxGrpCod = m_Sec.gGroup_GetCodFromSID(auxMemberSidCod)

                                    m_Sec.gGroup_DelGroup(auxMiembrosGrpCod, auxGrpCod)

                                    auxBagValuesOldValues = New clshrcBagValues
                                    auxBagValuesNewValues = New clshrcBagValues
                                    auxRows = pClass.gReplication_DataGetByFilter(m_EntityTypes(enumEntityType.coEQU), "miembrosgrpcod =" & auxMiembrosGrpCod)
                                    'hrcEntityDT_EQU.Select("miembrosgrpcod =" & auxMiembrosGrpCod)

                                    auxBagValuesNewValues.gValue_Add("miembrosgrpcod", auxMiembrosGrpCod)
                                    auxBagValuesNewValues.gValue_Add("equcod", auxRows(0)("cod"))
                                    auxBagValuesNewValues.gValue_Add("sidcod", auxMemberSidCod)

                                    'auxRows = hrcEntityDT_UND.Select("grpcodresp=" & auxGrpCod)
                                    auxRows = pClass.gReplication_DataGetByFilter(m_EntityTypes(enumEntityType.coUND), "grpcodresp=" & auxGrpCod)
                                    If auxRows.Count <> 0 Then
                                        auxBagValuesOldValues.gValue_Add("undcod", auxRows(0)("cod"))
                                        auxBagValuesOldValues.gValue_Add("mbrtypecod", m_EntityTypes(enumEntityType.coUND))
                                        auxBagValuesOldValues.gValue_Add("gruporesp", 1)
                                        auxBagValuesOldValues.gValue_Add("grpcodresp", auxGrpCod)
                                        auxBagValuesNewValues.gValue_Add("undcod", auxRows(0)("cod"))
                                        auxBagValuesNewValues.gValue_Add("mbrtypecod", m_EntityTypes(enumEntityType.coUND))
                                        auxBagValuesNewValues.gValue_Add("gruporesp", 0)
                                        auxBagValuesNewValues.gValue_Add("grpcodresp", -1)
                                    End If
                                    auxRows = pClass.gReplication_DataGetByFilter(m_EntityTypes(enumEntityType.coUND), "miembrosgrpcod=" & auxGrpCod)
                                    'auxRows = hrcEntityDT_UND.Select("miembrosgrpcod=" & auxGrpCod)

                                    If auxRows.Count <> 0 Then
                                        auxBagValuesOldValues.gValue_Add("undcod", auxRows(0)("cod"))
                                        auxBagValuesOldValues.gValue_Add("mbrtypecod", m_EntityTypes(enumEntityType.coUND))
                                        auxBagValuesOldValues.gValue_Add("grupomiembros", 1)
                                        auxBagValuesOldValues.gValue_Add("miembrosgrpcod", auxGrpCod)
                                        auxBagValuesNewValues.gValue_Add("undcod", auxRows(0)("cod"))
                                        auxBagValuesNewValues.gValue_Add("mbrtypecod", m_EntityTypes(enumEntityType.coUND))
                                        auxBagValuesNewValues.gValue_Add("grupomiembros", 0)
                                        auxBagValuesNewValues.gValue_Add("miembrosgrpcod", -1)
                                    End If
                                    auxRows = pClass.gReplication_DataGetByFilter(m_EntityTypes(enumEntityType.coUND), "grpcodmbrdir=" & auxGrpCod)
                                    'auxRows = hrcEntityDT_UND.Select("grpcodmbrdir=" & auxGrpCod)
                                    If auxRows.Count <> 0 Then
                                        auxBagValuesOldValues.gValue_Add("undcod", auxRows(0)("cod"))
                                        auxBagValuesOldValues.gValue_Add("mbrtypecod", m_EntityTypes(enumEntityType.coUND))
                                        auxBagValuesOldValues.gValue_Add("grupombrdir", 1)
                                        auxBagValuesOldValues.gValue_Add("miembrosgrpcod", auxGrpCod)
                                        auxBagValuesNewValues.gValue_Add("undcod", auxRows(0)("cod"))
                                        auxBagValuesNewValues.gValue_Add("mbrtypecod", m_EntityTypes(enumEntityType.coUND))
                                        auxBagValuesNewValues.gValue_Add("grupombrdir", 0)
                                        auxBagValuesNewValues.gValue_Add("miembrosgrpcod", -1)
                                    End If
                                    auxRows = pClass.gReplication_DataGetByFilter(m_EntityTypes(enumEntityType.coUND), "grpcodprjver=" & auxGrpCod)
                                    'auxRows = hrcEntityDT_UND.Select("grpcodprjver=" & auxGrpCod)
                                    If auxRows.Count <> 0 Then
                                        auxBagValuesOldValues.gValue_Add("undcod", auxRows(0)("cod"))
                                        auxBagValuesOldValues.gValue_Add("mbrtypecod", m_EntityTypes(enumEntityType.coUND))
                                        auxBagValuesOldValues.gValue_Add("grupoprjver", 1)
                                        auxBagValuesOldValues.gValue_Add("grpcodprjver", auxGrpCod)
                                        auxBagValuesNewValues.gValue_Add("undcod", auxRows(0)("cod"))
                                        auxBagValuesNewValues.gValue_Add("mbrtypecod", m_EntityTypes(enumEntityType.coUND))
                                        auxBagValuesNewValues.gValue_Add("grupoprjver", 0)
                                        auxBagValuesNewValues.gValue_Add("grpcodprjver", -1)
                                    End If
                                    gReplication_ChangesTable_AddChange(pEntityBuildType:=enumEntityType.coEQUMBR, _
                                                                              pEntityType:=-1, _
                                                                              pActionType:=enumActionType.coConfirmDelete, _
                                                                              pOldValues:=auxBagValuesOldValues, _
                                                                              pNewValues:=auxBagValuesNewValues)
                                    auxBagValuesOldValues.gValue_Clear("grupombrdir")

                                Case m_Sec.enumSIDType.coUser
                                    Dim auxSecCod As Integer = m_Sec.gLogin_GetCodFromSID(auxMemberSidCod)
                                    m_Sec.gGroup_DelLogin(auxMiembrosGrpCod, auxSecCod)

                                    auxBagValuesOldValues = New clshrcBagValues
                                    auxBagValuesNewValues = New clshrcBagValues
                                    'auxRows = hrcEntityDT_EQU.Select("miembrosgrpcod =" & auxMiembrosGrpCod)
                                    auxRows = pClass.gReplication_DataGetByFilter(m_EntityTypes(enumEntityType.coEQU), "miembrosgrpcod =" & auxMiembrosGrpCod)

                                    auxBagValuesNewValues.gValue_Add("miembrosgrpcod", auxMiembrosGrpCod)
                                    auxBagValuesNewValues.gValue_Add("equcod", auxRows(0)("cod"))

                                    'auxRows = hrcEntityDT_EMP.Select("seccod=" & auxSecCod)
                                    auxRows = pClass.gReplication_DataGetByFilter(m_EntityTypes(enumEntityType.coEMP), "seccod=" & auxSecCod)
                                    If auxRows.Count <> 0 Then
                                        auxBagValuesOldValues.gValue_Add("empcod", auxRows(0)("cod"))
                                        auxBagValuesOldValues.gValue_Add("mbrtypecod", m_EntityTypes(enumEntityType.coEMP))
                                        auxBagValuesOldValues.gValue_Add("seccod", auxGrpCod)
                                        auxBagValuesNewValues.gValue_Add("empcod", auxRows(0)("cod"))
                                        auxBagValuesNewValues.gValue_Add("mbrtypecod", m_EntityTypes(enumEntityType.coEMP))
                                        auxBagValuesNewValues.gValue_Add("seccod", -1)
                                        auxBagValuesNewValues.gValue_Add("sidcod", auxMemberSidCod)
                                    End If
                                    gReplication_ChangesTable_AddChange(pEntityBuildType:=enumEntityType.coEQUMBR, _
                                                                              pEntityType:=-1, _
                                                                              pActionType:=enumActionType.coConfirmDelete, _
                                                                              pOldValues:=auxBagValuesOldValues, _
                                                                              pNewValues:=auxBagValuesNewValues)

                            End Select

                        End If
                    Next
                Next

            Catch ex As Exception
                gSys_DebugLogAdd("EQUMBR postAction-Exception:" & ex.Message)
            End Try

        End Function

        Private Function gEntityEMP_PostAction(ByVal pClass As clshrcGeneral, _
                                      ByVal pAction As enumActionType, _
                                      ByVal pOldValues As clshrcBagValues, _
                                      ByVal pNewValues As clshrcBagValues) As String
            If pNewValues Is Nothing Then
                pNewValues = New clshrcBagValues
            End If
            If pOldValues Is Nothing Then
                pOldValues = New clshrcBagValues
            End If
            Dim auxCod As Integer = pClass.Conn.gField_GetInt(pNewValues.gValue_Get("cod"), -1)
            If auxCod < 1 Then
                gSys_DebugLogAdd("Error-No EMP cod:" & auxCod)
                Exit Function
            End If
            Select Case pAction
                Case enumActionType.coConfirmInsert, enumActionType.coConfirmModify
                    Dim auxStaticRow As DataRow = pClass.gReplication_DataGetByCod(m_EntityTypes(enumEntityType.coEMP), auxCod)
                    If auxStaticRow Is Nothing Then
                        gSys_DebugLogAdd("Error-No EMP Row:" & auxCod)
                    Else
                        'pOldValues.gValues_ClearNoValues()

                    End If

                    Dim auxSecCod As Integer = pClass.Conn.gField_GetInt(auxStaticRow("seccod"), -1)
                    Dim auxSecDsc As String = pClass.Conn.gField_GetString(auxStaticRow("empusername"), -1)
                    'Esta variable sirve para detectar cambios en el seccod. porque puede ser que no tenía login y luego si (o al reves). 
                    'Y en ese caso se debe agregar al grupo de unidad/quitar
                    Dim auxInitialize As Boolean = False
                    If pAction = enumActionType.coConfirmInsert Then
                        auxInitialize = True
                    End If

                    If auxSecDsc <> "" And auxSecCod < 1 Then
                        auxInitialize = True
                        gSys_DebugLogAdd("EMP Error1!" & auxCod & "-No seccod:" & auxSecCod & "-" & auxStaticRow("dsc"))
                        pOldValues.gValue_Add("empusername", "")
                        pNewValues.gValue_Add("empusername", auxSecDsc)
                    End If

                    If pOldValues.gValue_Get("empusername") IsNot Nothing Then
                        Dim auxEmpUsername_Old As String = pClass.Conn.gField_GetString(pOldValues.gValue_Get("empusername"), "")
                        Dim auxEmpUsername_New As String = pClass.Conn.gField_GetString(pNewValues.gValue_Get("empusername"), "")
                        'Hubo un cambio de usuario
                        If auxEmpUsername_New = "" Then
                            If auxSecCod > 0 Then
                                pOldValues.gValue_Add("seccod", auxSecCod)
                                pNewValues.gValue_Add("seccod", -1)
                                m_Sec.gLogin_Disabled(auxSecCod)
                                auxSecCod = -1
                                pClass.gEntity_EMP_SystemUpdate(pcod:=auxCod, pseccod:=-1)
                            End If
                        Else
                            If auxSecCod < 1 Then
                                auxSecCod = m_Sec.gLogin_Create(auxEmpUsername_New, m_LoginDefaultPsw, False)
                                pClass.gEntity_EMP_SystemUpdate(pcod:=auxCod, pseccod:=auxSecCod)
                                auxInitialize = True
                                pOldValues.gValue_Add("seccod", -1)
                                pNewValues.gValue_Add("seccod", auxSecCod)
                            Else
                                m_Sec.LoginRenameControl = False
                                m_Sec.gLogin_Rename(auxSecCod, auxEmpUsername_New)
                            End If
                            m_Sec.gLogin_Enabled(auxSecCod)
                        End If
                    End If
                    If auxSecCod < 1 Then
                        If auxSecDsc <> "" Then
                            gSys_DebugLogAdd("EMP Error!" & auxCod & "-No seccod:" & auxSecCod & "-" & auxStaticRow("dsc"))
                        End If
                    End If
                    Dim auxIsBaja As Boolean = pClass.Conn.gField_GetBoolean(pNewValues.gValue_Get("baja"))
                    If pOldValues.gValue_Get("baja") IsNot Nothing Or pAction = enumActionType.coConfirmDelete Then
                        If auxSecCod > 0 Then
                            Dim auxBaja_New As Boolean = pClass.Conn.gField_GetBoolean(pNewValues.gValue_Get("baja"), False)
                            Dim auxBaja_Old As Boolean = pClass.Conn.gField_GetBoolean(pOldValues.gValue_Get("baja"), False)
                            If pAction = enumActionType.coConfirmDelete Then
                                auxBaja_New = True
                            End If
                            'If auxBaja_New Then
                            '    auxIsBaja = True
                            '    auxInitialize = False
                            'End If
                            Select Case auxBaja_New
                                Case True
                                    m_Sec.gLogin_Disabled(auxSecCod)
                                Case False
                                    m_Sec.gLogin_Enabled(auxSecCod)
                            End Select
                        End If

                    End If

                    If pOldValues.gValue_Get("undcod") IsNot Nothing Then
                        auxInitialize = True
                    End If
                    Dim auxUndCod_New As Integer = pClass.Conn.gField_GetInt(pNewValues.gValue_Get("undcod"), -1)
                    If auxInitialize Then 'Or pOldValues.gValue_Get("undcod") IsNot Nothing Then
                        If auxSecCod > 0 Then
                            'Cambio de unidad
                            '-Quita anterior
                            If auxUndCod_New < 1 Then
                                auxUndCod_New = -1
                            End If
                            Dim auxUndCod_Old As Integer = pClass.Conn.gField_GetInt(pOldValues.gValue_Get("undcod"), -1)
                            If auxUndCod_Old < 1 Then
                                auxUndCod_Old = -1
                            End If

                            If auxUndCod_New <> auxUndCod_Old And auxUndCod_Old > 0 Then
                                'Dim auxUndAntRow As DataRow = hrcEntityDT_UND_FindByKey(auxUndCod_Old)
                                Dim auxUndAntRow As DataRow = pClass.gReplication_DataGetByCod(m_EntityTypes(enumEntityType.coUND), auxUndCod_Old)
                                If auxUndAntRow IsNot Nothing Then
                                    Dim auxUnd_GrpCod_Ant As Integer = pClass.Conn.gField_GetInt(auxUndAntRow("grpcodmbrdir"), -1)
                                    m_Sec.gGroup_DelLogin(auxUnd_GrpCod_Ant, auxSecCod)
                                End If
                            End If

                            'cambio de unidad-agrega al nuevo
                            'If auxDT.Rows(0)("undcod") > 0 Then
                            If auxUndCod_New > 0 Then
                                'Dim auxUndNewRow As DataRow = hrcEntityDT_UND_FindByKey(auxUndCod_New)
                                Dim auxUndNewRow As DataRow = pClass.gReplication_DataGetByCod(m_EntityTypes(enumEntityType.coUND), auxUndCod_New)
                                If auxUndNewRow IsNot Nothing Then
                                    Dim auxUnd_GrpCod As Integer = pClass.Conn.gField_GetInt(auxUndNewRow("grpcodmbrdir"), -1)
                                    m_Sec.gGroup_AddLogin(auxUnd_GrpCod, auxSecCod)
                                End If
                            End If
                        End If
                        'End If

                        'Analiza si debe realizar modificaciones sobre unidades
                        'If auxInitialize Then
                        If auxSecCod > 0 Then
                            'Es una inicialización. Entonces debe agregar el login en el grupo de miembros directos
                            If auxUndCod_New > 0 Then
                                'Dim auxRowUND As DataRow = hrcEntityDT_UND_FindByKey(auxUndCod_New)
                                Dim auxRowUND As DataRow = pClass.gReplication_DataGetByCod(m_EntityTypes(enumEntityType.coUND), auxUndCod_New)
                                If auxRowUND IsNot Nothing Then
                                    Dim auxGrpCodMbrDir As Integer = pClass.Conn.gField_GetInt(auxRowUND("grpcodmbrdir"), -1)
                                    If auxGrpCodMbrDir > 0 Then
                                        m_Sec.gGroup_AddLogin(auxGrpCodMbrDir, auxSecCod)
                                    End If
                                End If
                            End If

                            'Debe agregarlo como responsable en las unidades que esta incluido
                            'For Each auxRow As DataRow In hrcEntityDT_UND.Select("resp=" & auxCod)
                            For Each auxRow As DataRow In pClass.gReplication_DataGetByFilter(m_EntityTypes(enumEntityType.coUND), "resp=" & auxCod)
                                Dim auxGrpCodResp As Integer = pClass.Conn.gField_GetInt(auxRow("grpcodresp"), -1)
                                If auxGrpCodResp > 0 Then
                                    m_Sec.gGroup_AddLogin(auxGrpCodResp, auxSecCod)
                                End If
                            Next
                            'Debe agregarlo como miemnbro de los grupos
                            Dim auxTableNameEQU As String = m_TablesEntity(enumEntityType.coEQUMBR).TabLocalDsc
                            Dim auxTableNameEQUMBR As String = m_TablesEntity(enumEntityType.coEQUMBR).TabLocalDsc
                            Dim auxTableNameEQUMBRUND As String = m_TablesEntity(enumEntityType.coEQUMBRUND).TabLocalDsc
                            Dim auxTableNameEQUMBREMP As String = m_TablesEntity(enumEntityType.coEQUMBREMP).TabLocalDsc
                            For Each auxRow As DataRow In pClass.Conn.gConn_Query("SELECT " & auxTableNameEQUMBR & ".equcod," & auxTableNameEQU & ".miembrosgrpcod " _
                                                                & " FROM " & auxTableNameEQUMBREMP & " " _
                                                                & " LEFT JOIN " & auxTableNameEQUMBR & " ON " & auxTableNameEQUMBR & ".cod= " & auxTableNameEQUMBREMP & ".equmbrcod" _
                                                                & " LEFT JOIN " & auxTableNameEQU & " ON " & auxTableNameEQUMBR & ".cod= " & auxTableNameEQU & ".cod" _
                                                                & " WHERE " & auxTableNameEQUMBREMP & ".empcod=" & auxCod).Select
                                'Dim auxEQURow As DataRow = hrcEntityDT_EQU_FindByKey(auxRow("equcod"))
                                'If auxEQURow IsNot Nothing Then
                                Dim auxMiembrosGrpCod As Integer = pClass.Conn.gField_GetInt(auxRow("miembrosgrpcod"), -1)
                                If auxMiembrosGrpCod > 0 Then
                                    m_Sec.gGroup_AddLogin(auxMiembrosGrpCod, auxSecCod)
                                End If
                                'End If
                            Next
                        End If
                    End If
            End Select


        End Function
        Public Function gReplication_CheckStatus() As clshrcBagValues
            'Chequear las latencia

        End Function
        Private Sub gReplication_TableWork(ByVal pClass As clshrcGeneral, _
                                          ByVal pTable As prvTable, _
                                          ByVal pSecDateTime As DateTime, _
                                          ByVal pINCod As Integer, _
                                          ByVal pConn As clsHrcConnClient)
            Dim auxWhere As String = ""
            If pINCod <> -1 Then
                auxWhere &= " cod= " & pINCod
            ElseIf pSecDateTime <> Nothing Then
                auxWhere &= " cod > 0 AND qsecdatetime >= " & pConn.gFieldDB_GetDateTime(pSecDateTime)
            Else
                auxWhere &= " cod > 0"
            End If
            Dim auxTableRows() As DataRow
            If pTable.DT_Remote IsNot Nothing Then
                auxTableRows = pTable.DT_Remote.Select(auxWhere)
            ElseIf pTable.TabRemoteQuery <> "" Then
                auxTableRows = m_Remote1Conn.gConn_Query(pTable.TabRemoteQuery _
                                                           & auxWhere).Select
            Else
            End If

            'Select Case pTable.TabBuildType
            '    Case enumEntityType.coEQUMBR
            '        auxTablerows = pClass.Conn1.gConn_Query("SELECT EQUMBR.cod,EQUMBR.mbrtypecod,EQUMBR.equcod " _
            '                                                            & ",EQUMBREMP.empcod" _
            '                                                            & ",EQUMBRUND.gruporesp,EQUMBRUND.grupomiembros,EQUMBRUND.grupoprjver,EQUMBRUND.grupombrdir" _
            '                                                            & ",EQUMBRUND.undcod,EQUMBREMP.empcod" _
            '                                                            & " FROM EQUMBR " _
            '                                                            & " LEFT JOIN EQU ON EQUMBR.equcod=EQU.cod " _
            '                                                            & " LEFT JOIN EQUMBRUND ON EQUMBR.cod= EQUMBRUND.equmbrcod" _
            '                                                            & " LEFT JOIN EQUMBREMP ON EQUMBR.cod= EQUMBREMP.equmbrcod" _
            '                                                            & " WHERE EQUMBR.cod > 0" & auxWhere).Select
            '    Case Else
            '        auxTablerows = pTable.DT_Remote.Select(auxWhere)
            'End Select
            Dim auxAnalyzed As New List(Of Integer)
            Dim auxParentCod_Changes As New List(Of Integer)   'lleva la lista de cambios para los equipos
            If m_Remote1Conn.LastErrorDescription <> "" Then
                gSys_DebugLogAdd(m_Remote1Conn.LastCommand)
                gSys_DebugLogAdd(m_Remote1Conn.LastErrorDescription)
            End If
            Dim auxBagValuesNewValues As clshrcBagValues
            Dim auxBagValuesOldValues As clshrcBagValues
            Dim auxBagValuesOnlynewsValues As clshrcBagValues
            Dim auxHasChanges As Boolean
            Dim auxRows() As DataRow
            Dim auxCod As Integer
            Dim auxAction As enumActionType
            For Each auxRow As DataRow In auxTableRows
                auxBagValuesNewValues = pConn.gField_GetBagValuesFromArray(auxRow.ItemArray, auxRow.Table.Columns)
                auxBagValuesOldValues = Nothing
                auxBagValuesOnlynewsValues = Nothing
                auxHasChanges = False
                auxAction = enumActionType.coConfirmModify
                auxCod = pConn.gField_GetInt(auxRow("cod"))
                auxAnalyzed.Add(auxCod)
                'Busca si existe el registro
                If pTable.DT_REL Is Nothing Then
                    'No controla por el numero de relacion, sino por el numero local
                    auxRows = pTable.DT_Remote.Select("cod = " & auxCod)
                Else
                    auxRows = pTable.DT_REL.Select(coRplpreffix & "bascod =" & m_Remote1BasCod & " AND " & coRplpreffix & "cod = " & auxCod)
                End If

                If auxRows.Count = 0 Then
                    gSys_DebugLogAdd("Error! not exist:" & auxCod)
                Else
                    auxCod = auxRows(0)("cod")
                End If
                Dim auxUpdate As String = pTable.UpdateString
                If auxUpdate <> "" Then
                    For Each auxColumn As prvColumn In pTable.TabColumns.Values
                        Select Case auxColumn.FieType
                            Case hrcDataTypes.coBooleanType
                                auxUpdate = Replace(auxUpdate, "{#" & auxColumn.FieLocalDsc & "#}", _
                                                    pConn.gFieldDB_GetBoolean(auxRow(auxColumn.FieRemoteDsc)))
                            Case hrcDataTypes.coDateType
                                auxUpdate = Replace(auxUpdate, "{#" & auxColumn.FieLocalDsc & "#}", _
                                                    pConn.gFieldDB_GetDateTime(auxRow(auxColumn.FieRemoteDsc)))
                            Case hrcDataTypes.coStringType
                                auxUpdate = Replace(auxUpdate, "{#" & auxColumn.FieLocalDsc & "#}", _
                                                    pConn.gFieldDB_GetString(auxRow(auxColumn.FieRemoteDsc)))
                            Case hrcDataTypes.coNumberType
                                auxUpdate = Replace(auxUpdate, "{#" & auxColumn.FieLocalDsc & "#}", _
                                                    pConn.gFieldDB_GetInt(auxRow(auxColumn.FieRemoteDsc)))
                        End Select

                    Next
                End If

                'El analisis de cambios por ahora realizarlo contra la BD
                'No se puede hacer contra el caché porque puede ser antiguo (o ser modificado durante la replicación por el thread actual u otro).
                Dim auxDTResult As DataTable '
                If auxUpdate <> "" Then
                    'Utiliza la consulta con update
                    auxDTResult = pConn.gConn_ExecuteProcedureQuery(auxUpdate)
                    If auxDTResult.Rows.Count = 0 Then
                        gSys_DebugLogAdd("Changes in " & pTable.TabLocalDsc & ":" & auxCod & ".Error:" & pConn.LastErrorDescription)
                    Else
                        auxBagValuesOldValues = pConn.gField_GetBagValuesFromArray(auxDTResult.Rows(0).ItemArray, auxDTResult.Columns)
                        auxBagValuesOldValues.gValue_Clear("cod")
                        auxBagValuesOldValues.gValues_ClearNoValues()
                        If auxBagValuesOldValues.Values.Count <> 0 Then
                            auxHasChanges = True
                        End If
                    End If
                Else
                    'Utiliza el cacheo previo
                    auxRows = pTable.DT_Local.Select("cod=" & auxCod)
                    auxBagValuesOldValues = Nothing
                    If auxRows.Count = 0 Then
                        gSys_DebugLogAdd("Error in " & pTable.TabLocalDsc & "-Previous cache hasn't value charged:" & auxCod)
                    Else
                        auxBagValuesOldValues = pConn.gField_GetBagValuesFromArray(auxRows(0).ItemArray, pTable.DT_Local.Columns)
                        auxBagValuesOldValues.gValues_Subtract(auxBagValuesNewValues)
                        If auxBagValuesOldValues.Values.Count <> 0 Then
                            Select Case pTable.TabBuildType
                                Case enumEntityType.coEQUMBR
                                    'Debe agregar los registros en EQUMBREMP y EQUMBRUND
                                    'If auxBagValuesOldValues.gValue_Get("mbrtypecod") IsNot Nothing Then
                                    'Cambio el tipo de miembro
                                    'Elimina todos
                                    auxBagValuesOnlynewsValues = New clshrcBagValues(auxBagValuesNewValues)
                                    auxBagValuesOnlynewsValues.gValue_Add("equmbrcod", auxCod)
                                    pClass.gReplication_DataUpdate(m_EntityTypes(enumEntityType.coEQUMBREMP), enumActionType.coConfirmDelete, auxBagValuesOnlynewsValues)
                                    pClass.gReplication_DataUpdate(m_EntityTypes(enumEntityType.coEQUMBRUND), enumActionType.coConfirmDelete, auxBagValuesOnlynewsValues)
                                    Select Case Val(auxBagValuesNewValues.gValue_Get("mbrtypecod"))
                                        Case m_EntityTypes(enumEntityType.coEMP)
                                            'auxBagValuesOnlynewsValues = New clshrcBagValues(auxBagValuesNewValues)
                                            'auxBagValuesOnlynewsValues.gValue_Add("cod", auxCod)
                                            'auxBagValuesOnlynewsValues.gValue_Add("empcod", auxBagValuesNewValues.gValue_Get("empcod"))
                                            pClass.gReplication_DataUpdate(m_EntityTypes(enumEntityType.coEQUMBREMP), enumActionType.coConfirmInsert, auxBagValuesOnlynewsValues)
                                        Case m_EntityTypes(enumEntityType.coUND)
                                            'auxBagValuesOnlynewsValues = New clshrcBagValues(auxBagValuesNewValues)
                                            'auxBagValuesOnlynewsValues.gValue_Add("cod", auxCod)
                                            'auxBagValuesOnlynewsValues.gValue_Add("undcod", auxBagValuesNewValues.gValue_Get("undcod"))
                                            'auxBagValuesOnlynewsValues.gValue_Add("undcod", auxBagValuesNewValues.gValue_Get("undcod"))
                                            'auxBagValuesOnlynewsValues.gValue_Add("undcod", auxBagValuesNewValues.gValue_Get("undcod"))
                                            'auxBagValuesOnlynewsValues.gValue_Add("undcod", auxBagValuesNewValues.gValue_Get("undcod"))
                                            'auxBagValuesOnlynewsValues.gValue_Add("undcod", auxBagValuesNewValues.gValue_Get("undcod"))
                                            pClass.gReplication_DataUpdate(m_EntityTypes(enumEntityType.coEQUMBRUND), enumActionType.coConfirmInsert, auxBagValuesOnlynewsValues)
                                    End Select
                                    'End If
                                    auxBagValuesOnlynewsValues.gValue_Add("cod", auxCod)
                                    pClass.gReplication_DataUpdate(m_EntityTypes(enumEntityType.coEQUMBR), enumActionType.coConfirmModify, auxBagValuesOnlynewsValues)
                                    auxParentCod_Changes.Add(auxBagValuesNewValues.gValue_Get("equcod"))
                                Case Else
                                    'Calcula un bagvalues solo con los valores nuevos
                                    auxBagValuesOnlynewsValues = New clshrcBagValues(auxBagValuesNewValues)
                                    auxBagValuesOnlynewsValues.gValues_Subtract(pConn.gField_GetBagValuesFromArray(auxRows(0).ItemArray, pTable.DT_Local.Columns))
                                    auxBagValuesOnlynewsValues.gValue_Add("cod", auxCod)
                                    pClass.gReplication_DataUpdate(pTable.TabEntityType, enumActionType.coConfirmModify, auxBagValuesOnlynewsValues)
                            End Select

                            auxHasChanges = True
                        End If
                        auxBagValuesOldValues.gValue_Add("cod", auxCod)

                    End If



                End If

                If auxHasChanges Then
                    Select Case pTable.TabBuildType
                        Case enumEntityType.coEMP
                            'm_Class.gCluster_LoadStaticTables_EMP(auxCod)
                            gReplication_ChangesTable_AddChange(pEntityBuildType:=enumEntityType.coEMP, _
                                            pEntityType:=-1, _
                                            pActionType:=auxAction, _
                                            pOldValues:=auxBagValuesOldValues, _
                                            pNewValues:=auxBagValuesNewValues)
                        Case enumEntityType.coUND
                            m_Class.gCluster_LoadStaticTables_UND(auxCod)
                            gReplication_ChangesTable_AddChange(pEntityBuildType:=enumEntityType.coUND, _
                                             pEntityType:=-1, _
                                             pActionType:=auxAction, _
                                             pOldValues:=auxBagValuesOldValues, _
                                             pNewValues:=auxBagValuesNewValues)
                            'gEntityUND_PostAction(pAction:=auxPostAction, _
                            '  pOldValues:=auxBagValuesOldValues, _
                            '  pNewValues:=auxBagValuesNewValues)
                        Case enumEntityType.coEQU
                            'm_Class.gCluster_LoadStaticTables_EQU(auxCod)
                            gReplication_ChangesTable_AddChange(pEntityBuildType:=enumEntityType.coEQU, _
                                            pEntityType:=-1, _
                                            pActionType:=auxAction, _
                                            pOldValues:=auxBagValuesOldValues, _
                                            pNewValues:=auxBagValuesNewValues)
                            'gEntityEQU_PostAction(pAction:=auxPostAction, _
                            '  pOldValues:=auxBagValuesOldValues, _
                            '  pNewValues:=auxBagValuesNewValues)
                        Case enumEntityType.coEQUMBR
                            'gReplication_ChangesTable_AddChange(pEntityType:=enumEntityType.coEQUMBR, _
                            '   pActionType:=auxPostAction, _
                            '   pOldValues:=auxBagValuesOldValues, _
                            '   pNewValues:=auxBagValuesNewValues)
                            Dim auxEquCod As Integer = Val(auxBagValuesNewValues.gValue_Get("equcod"))
                            If m_EQUMBRChanges.IndexOf(auxEquCod) = -1 Then
                                m_EQUMBRChanges.Add(auxEquCod)
                            End If
                        Case Else
                            gReplication_ChangesTable_AddChange(pEntityBuildType:=pTable.TabBuildType, _
                               pEntityType:=pTable.TabEntityType, _
                               pActionType:=auxAction, _
                               pOldValues:=auxBagValuesOldValues, _
                               pNewValues:=auxBagValuesNewValues)
                    End Select

                End If

            Next

            If pTable.TabDeleteMrkField Is Nothing _
                And (m_EntityTypes.IndexOfKey(pTable.TabBuildType) <> -1 Or pTable.TabEntityType > 0) Then
                'No tiene marca de baja- Analiza en base a lo faltante
                'Analiza SOLO LAS QUE tiene una tabla relacionada local para poder eliminar
                For Each auxRow As DataRow In pTable.DT_Local.Rows
                    auxCod = auxRow("cod")
                    If auxAnalyzed.IndexOf(auxRow("cod")) = -1 Then
                        auxBagValuesNewValues = pConn.gField_GetBagValuesFromArray(auxRow.ItemArray, auxRow.Table.Columns)
                        If pTable.TabBuildType > 0 Then
                            Select Case pTable.TabBuildType
                                Case enumEntityType.coEMP

                                Case enumEntityType.coUND

                                Case enumEntityType.coEQU

                                Case enumEntityType.coEQUMBR
                                    auxBagValuesOnlynewsValues = New clshrcBagValues(auxBagValuesNewValues)
                                    auxBagValuesOnlynewsValues.gValue_Add("equmbrcod", auxCod)
                                    m_Class.gReplication_DataUpdate(m_EntityTypes(enumEntityType.coEQUMBREMP), enumActionType.coConfirmDelete, auxBagValuesOnlynewsValues)
                                    m_Class.gReplication_DataUpdate(m_EntityTypes(enumEntityType.coEQUMBRUND), enumActionType.coConfirmDelete, auxBagValuesOnlynewsValues)
                                    auxBagValuesOnlynewsValues.gValue_Add("cod", auxCod)
                                    m_Class.gReplication_DataUpdate(m_EntityTypes(enumEntityType.coEQUMBR), enumActionType.coConfirmDelete, auxBagValuesOnlynewsValues)

                                Case Else
                                    auxBagValuesOnlynewsValues = New clshrcBagValues(auxBagValuesNewValues)
                                    auxBagValuesOnlynewsValues.gValue_Add("cod", auxCod)
                                    m_Class.gReplication_DataUpdate(m_EntityTypes(pTable.TabBuildType), enumActionType.coConfirmDelete, auxBagValuesOnlynewsValues)
                            End Select
                            gReplication_ChangesTable_AddChange(pEntityBuildType:=pTable.TabBuildType, _
                                  pEntityType:=-1, _
                                  pActionType:=enumActionType.coConfirmDelete, _
                                  pOldValues:=auxBagValuesNewValues, _
                                  pNewValues:=auxBagValuesNewValues)
                        ElseIf pTable.TabEntityType > 0 Then
                            auxBagValuesOnlynewsValues = New clshrcBagValues(auxBagValuesNewValues)
                            auxBagValuesOnlynewsValues.gValue_Add("cod", auxCod)
                            m_Class.gReplication_DataUpdate(pTable.TabEntityType, enumActionType.coConfirmDelete, auxBagValuesOnlynewsValues)
                            gReplication_ChangesTable_AddChange(pEntityBuildType:=-1, _
                                 pEntityType:=pTable.TabEntityType, _
                                 pActionType:=enumActionType.coConfirmDelete, _
                                 pOldValues:=auxBagValuesNewValues, _
                                 pNewValues:=auxBagValuesNewValues)
                        End If

                    End If
                Next
            End If


        End Sub
        Private Sub gReplication_ChangesTable_AddChange(ByVal pEntityBuildType As enumEntityType, _
                                       ByVal pEntityType As Integer, _
                                       ByVal pActionType As enumActionType, _
                                       ByVal pOldValues As clshrcBagValues, _
                                       ByVal pNewValues As clshrcBagValues)
            Dim auxRow As DataRow = m_DTChanges.NewRow
            auxRow("EntityType") = pEntityType
            auxRow("EntityBuildType") = pEntityBuildType
            auxRow("ActionType") = pActionType
            auxRow("OldValues") = pOldValues.Config_GetStream
            auxRow("NewValues") = pNewValues.Config_GetStream
            m_DTChanges.Rows.Add(auxRow)
        End Sub
        Public Sub gReplication_ReSync()
            'Sincroniza las entidades con los grupos de seguridad
            Dim auxConn As clsHrcConnClient = m_Conn
            auxConn.gConn_Open()
            If gReplication_Load(m_Class, m_Remote1BasCod, auxConn) Then
                Dim auxDTToCompare As New DataTable
                auxDTToCompare.Columns.Add(New DataColumn("sidcod", System.Type.GetType("System.Int32")))
                Dim auxTable As prvTable
                'Control se login utilizados
                'No se pueden reutilizar los mismos seccod en distintos colaboradores


                Dim auxLoginsUsed As New List(Of Integer)
                Dim auxSecDsc As String
                Dim auxSecCod As Integer
                'Sincronización de EMP
                auxTable = m_TablesEntity(enumEntityType.coEMP)
                If auxTable IsNot Nothing Then
                    If auxTable.DT_Local IsNot Nothing Then
                        For Each auxRow As DataRow In auxTable.DT_Local.Rows
                            Dim auxStaticRow As DataRow = m_Class.gReplication_DataGetByCod(m_EntityTypes(enumEntityType.coEMP), auxRow("cod"))
                            gSys_DebugLogAdd("Emp:" & auxRow("cod"))
                            auxSecCod = -1
                            If m_Conn.gField_GetBoolean(auxStaticRow("baja"), False) = False Then
                                auxSecDsc = m_Conn.gField_GetString(auxStaticRow("empusername"), "")
                                auxSecCod = m_Conn.gField_GetInt(auxStaticRow("seccod"), -1)
                                If auxSecCod < 1 Then
                                    auxSecCod = -1
                                End If
                                If (auxSecDsc <> "" And auxSecCod < 1) _
                                    Or auxSecDsc <> "" And auxSecCod > 0 Then
                                    If auxLoginsUsed.IndexOf(auxSecCod) <> -1 Or auxSecCod = -1 Then
                                        auxStaticRow("seccod") = -1
                                        Dim auxNewValues As New clshrcBagValues
                                        auxNewValues.gValue_Add("cod", auxRow("cod"))
                                        auxNewValues.gValue_Add("empusername", auxSecCod)
                                        gEntityEMP_PostAction(m_Class, enumActionType.coConfirmModify, Nothing, auxNewValues)
                                        auxSecCod = auxStaticRow("seccod")
                                    End If
                                    auxLoginsUsed.Add(auxSecCod)
                                End If
                            End If
                        Next
                    End If
                End If

                'Control de grupos utlizados
                'No se pueden reutilizar los mismos grupos entre diferentes unidades
                Dim auxGroupsUsed As New List(Of Integer)
                Dim auxGrpCod As Integer

                'Sincronización de unidades
                auxTable = m_TablesEntity(enumEntityType.coUND)
                If auxTable IsNot Nothing Then
                    If auxTable.DT_Local IsNot Nothing Then
                        Dim auxNewRow As DataRow
                        Dim auxEmpRow As DataRow
                        Dim auxUndRow As DataRow
                        Dim auxCod As Integer
                        For Each auxRow As DataRow In auxTable.DT_Local.Rows
                            Dim auxStaticRow As DataRow = m_Class.gReplication_DataGetByCod(m_EntityTypes(enumEntityType.coUND), auxRow("cod"))
                            If m_Conn.gField_GetBoolean(auxStaticRow("baja"), False) = False Then
                                For auxI As Short = 1 To 4
                                    gSys_DebugLogAdd("Und:" & auxRow("cod") & "-" & auxI)
                                    auxDTToCompare.Rows.Clear()
                                    auxGrpCod = -1
                                    Select Case auxI
                                        Case 1
                                            'Responsable
                                            auxGrpCod = auxStaticRow("grpcodresp")
                                            If auxGroupsUsed.IndexOf(auxGrpCod) <> -1 Then
                                                auxStaticRow("grpcodresp") = -1
                                                Dim auxNewValues As New clshrcBagValues
                                                auxNewValues.gValue_Add("cod", auxRow("cod"))
                                                gEntityUND_PostAction(m_Class, enumActionType.coConfirmModify, Nothing, auxNewValues)
                                                auxGrpCod = auxStaticRow("grpcodresp")
                                            End If
                                            auxGroupsUsed.Add(auxGrpCod)
                                            For Each auxEmpRow In m_Class.gReplication_DataGetByFilter(m_EntityTypes(enumEntityType.coEMP), "(baja IS NULL OR baja=0) " _
                                                                                                        & " AND cod=" & m_Conn.gField_GetInt(auxStaticRow("resp"), -1))
                                                auxCod = m_Conn.gField_GetInt(auxEmpRow("seccod"), -1)
                                                If auxCod > 0 Then
                                                    auxNewRow = auxDTToCompare.NewRow
                                                    auxNewRow("sidcod") = m_Sec.gLogin_GetSidCod(auxCod)
                                                    auxDTToCompare.Rows.Add(auxNewRow)
                                                End If
                                            Next

                                        Case 2
                                            '//miembros directos
                                            auxGrpCod = auxStaticRow("grpcodmbrdir")
                                            If auxGroupsUsed.IndexOf(auxGrpCod) <> -1 Then
                                                auxStaticRow("grpcodmbrdir") = -1
                                                Dim auxNewValues As New clshrcBagValues
                                                auxNewValues.gValue_Add("cod", auxRow("cod"))
                                                gEntityUND_PostAction(m_Class, enumActionType.coConfirmModify, Nothing, auxNewValues)
                                                auxGrpCod = auxStaticRow("grpcodmbrdir")
                                            End If
                                            auxGroupsUsed.Add(auxGrpCod)
                                            For Each auxEmpRow In m_Class.gReplication_DataGetByFilter(m_EntityTypes(enumEntityType.coEMP), "(baja IS NULL OR baja=0) " _
                                                                                                        & " AND undcod=" & auxStaticRow("cod"))
                                                auxCod = m_Conn.gField_GetInt(auxEmpRow("seccod"), -1)
                                                If auxCod > 0 Then
                                                    auxNewRow = auxDTToCompare.NewRow
                                                    auxNewRow("sidcod") = m_Sec.gLogin_GetSidCod(auxCod)
                                                    auxDTToCompare.Rows.Add(auxNewRow)
                                                End If
                                            Next

                                        Case 3
                                            '//miembros
                                            'Agregar miembros directos
                                            auxGrpCod = auxStaticRow("miembrosgrpcod")
                                            If auxGroupsUsed.IndexOf(auxGrpCod) <> -1 Then
                                                auxStaticRow("miembrosgrpcod") = -1
                                                Dim auxNewValues As New clshrcBagValues
                                                auxNewValues.gValue_Add("cod", auxRow("cod"))
                                                gEntityUND_PostAction(m_Class, enumActionType.coConfirmModify, Nothing, auxNewValues)
                                                auxGrpCod = auxStaticRow("miembrosgrpcod")
                                            End If
                                            auxGroupsUsed.Add(auxGrpCod)
                                            auxCod = m_Conn.gField_GetInt(auxStaticRow("grpcodmbrdir"), -1)
                                            If auxCod > 0 Then
                                                auxNewRow = auxDTToCompare.NewRow
                                                auxNewRow("sidcod") = m_Sec.gGroup_GetSidCod(auxCod)
                                                auxDTToCompare.Rows.Add(auxNewRow)
                                            End If
                                            For Each auxUndRow In m_Class.gReplication_DataGetByFilter(m_EntityTypes(enumEntityType.coUND), "(baja IS NULL OR baja=0) " _
                                                                                                        & " AND undcodsup=" & auxStaticRow("cod"))
                                                'Agregar miembros de subunidades
                                                auxCod = m_Conn.gField_GetInt(auxUndRow("miembrosgrpcod"), -1)
                                                If auxCod > 0 Then
                                                    auxNewRow = auxDTToCompare.NewRow
                                                    auxNewRow("sidcod") = m_Sec.gGroup_GetSidCod(auxCod)
                                                    auxDTToCompare.Rows.Add(auxNewRow)
                                                End If

                                            Next

                                        Case 4
                                            '//superiores
                                            auxGrpCod = auxStaticRow("grpcodprjver")
                                            If auxGroupsUsed.IndexOf(auxGrpCod) <> -1 Then
                                                auxStaticRow("grpcodprjver") = -1
                                                Dim auxNewValues As New clshrcBagValues
                                                auxNewValues.gValue_Add("cod", auxRow("cod"))
                                                gEntityUND_PostAction(m_Class, enumActionType.coConfirmModify, Nothing, auxNewValues)
                                                auxGrpCod = auxStaticRow("grpcodprjver")
                                            End If
                                            auxGroupsUsed.Add(auxGrpCod)
                                            'Agrega responsables de la unidad
                                            auxCod = m_Conn.gField_GetInt(auxStaticRow("grpcodresp"), -1)
                                            If auxCod > 0 Then
                                                auxNewRow = auxDTToCompare.NewRow
                                                auxNewRow("sidcod") = m_Sec.gGroup_GetSidCod(auxCod)
                                                auxDTToCompare.Rows.Add(auxNewRow)
                                            End If
                                            For Each auxUndRow In m_Class.gReplication_DataGetByFilter(m_EntityTypes(enumEntityType.coUND), "(baja IS NULL OR baja=0) " _
                                                                                                        & " AND cod=" & auxStaticRow("undcodsup"))
                                                'Agrega superiores de la superior
                                                auxCod = m_Conn.gField_GetInt(auxUndRow("grpcodprjver"), -1)
                                                If auxCod > 0 Then
                                                    auxNewRow = auxDTToCompare.NewRow
                                                    auxNewRow("sidcod") = m_Sec.gGroup_GetSidCod(auxCod)
                                                    auxDTToCompare.Rows.Add(auxNewRow)
                                                End If
                                            Next

                                    End Select
                                    If auxGrpCod > 0 Then
                                        For Each auxRowToChange As DataRow In m_Sec.gGroup_CompareWithMembersTable(auxGrpCod, auxDTToCompare).Rows
                                            If auxRowToChange("added") Then
                                                gSys_DebugLogAdd("Group:" & auxGrpCod & ".Adding:" & auxRowToChange("sidcod"))
                                                m_Sec.gGroup_AddMember(auxGrpCod, auxRowToChange("sidcod"))
                                            ElseIf auxRowToChange("deleted") Then
                                                gSys_DebugLogAdd("Group:" & auxGrpCod & ".Deleting:" & auxRowToChange("sidcod"))
                                                m_Sec.gGroup_DelMember(auxGrpCod, auxRowToChange("sidcod"))
                                            End If
                                        Next
                                        m_Sec.gGroup_Reorganize(auxGrpCod)
                                    End If
                                Next
                            End If
                        Next
                    End If
                End If
                'Sincronización de equipos
                auxTable = m_TablesEntity(enumEntityType.coEQU)
                If auxTable IsNot Nothing Then
                    If auxTable.DT_Local IsNot Nothing Then
                        For Each auxRow As DataRow In auxTable.DT_Local.Rows
                            Dim auxStaticRow As DataRow = m_Class.gReplication_DataGetByCod(m_EntityTypes(enumEntityType.coEQU), auxRow("cod"))
                            gSys_DebugLogAdd("Equ:" & auxRow("cod"))
                            auxGrpCod = -1
                            If m_Conn.gField_GetBoolean(auxStaticRow("baja"), False) = False Then
                                auxGrpCod = m_Conn.gField_GetInt(auxStaticRow("miembrosgrpcod"), -1)
                                If auxGroupsUsed.IndexOf(auxGrpCod) <> -1 Then
                                    auxStaticRow("miembrosgrpcod") = -1
                                    Dim auxNewValues As New clshrcBagValues
                                    auxNewValues.gValue_Add("cod", auxRow("cod"))
                                    gEntityEQU_PostAction(m_Class, enumActionType.coConfirmModify, Nothing, auxNewValues)
                                    auxGrpCod = auxStaticRow("miembrosgrpcod")
                                End If
                                auxGroupsUsed.Add(auxGrpCod)
                                gEntityEQUMBR_PostAction(m_Class, auxRow("cod"))
                                '  m_Sec.gGroup_Reorganize(auxGrpCod)
                            End If
                        Next
                    End If
                End If
            End If

            m_Class.Conn.gConn_Close()
            auxConn.gConn_Close()
            auxConn = Nothing
        End Sub
        Public Sub New()
            MyBase.New()
            m_FieldsStatus.Add(enumFieldClassType.coUNDGroupMember, True)
            m_FieldsStatus.Add(enumFieldClassType.coUNDGroupMemberDir, True)
            m_FieldsStatus.Add(enumFieldClassType.coUNDGroupResp, True)
            m_FieldsStatus.Add(enumFieldClassType.coUNDGroupSup, True)

            'm_FieldsLocalName.Add(enumFieldClassType.coEQUmiembrosgrpcod, "miembrosgrpcod")
            'm_FieldsLocalName.Add(enumFieldClassType.coEQUMBRequcod, "equcod")
        End Sub
    End Class
End Namespace
