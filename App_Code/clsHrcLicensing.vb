'Version 25/1/2014  V12
Imports Microsoft.VisualBasic
Imports System.Data
Imports Intelimedia.Hercules.Storage

Imports System.Security.Cryptography
Imports System.IO
Imports System.Text
Imports System.Web

Imports clshrcLicensing
Public Module clshrcLicensing
    Friend hrcLicParam As clshrcBagValues
    Public ReadOnly Property LicParam As clshrcBagValues
        Get
            Return hrcLicParam
        End Get
    End Property
    Friend hrcDemoMode As String = ""
    Public ReadOnly Property DemoMode As String
        Get
            Return hrcDemoMode
        End Get
    End Property
End Module
Partial Public Class clshrcimDOC
    Private Const hrcKeyPRV As String = ""
    Private Const hrcKeyPUB As String = ""
    Public Sub gLicensing_Recheck()
        hrcLicParam = Nothing
        gLicensing_Check()
    End Sub
    Public Sub gLicensing_Check()
        Dim auxObject As New Object
        SyncLock auxObject
            If hrcLicParam Is Nothing Then
                Dim auxDT As DataTable = m_Conn.gConn_Query("SELECT licdsc,licresult " _
                                                 & " FROM Q_LIC WHERE liccod > 0 " _
                                                 & " AND licenabled={#TRUE#}")
                Dim auxAuthorize As Boolean = False
                Dim auxLicParam As clshrcBagValues
                For Each auxRow As DataRow In auxDT.Rows
                    Dim auxLicResult As String = m_Conn.gField_GetString(auxRow("licresult"), "").Trim
                    Dim auxKYAKey As String
                    'Lee la clave privada para la autorización
                    auxKYAKey = hrcLICKey
                    Dim auxCrypto As New clsCrypto
                    auxCrypto.ActivateLicense(coDBKey)
                    auxCrypto.EncryptionType = clsCrypto.enumEncryptionTypes.coRSA
                    Dim auxBagValuesLicense As New clshrcBagValues(auxLicResult)
                    Dim auxLicenseSign As String = auxBagValuesLicense.gValue_Get("LICENSE_SIGN", "")
                    Dim auxLicense As String = auxBagValuesLicense.gValue_Get("LICENSE_CONTENT", "")
                    auxCrypto.gKey_ReadPublicFromString(hrcLICAutKey)
                    If auxCrypto.gRSA_VerifyMessageDataWithPublicKey(auxLicense, auxLicenseSign) Then
                        auxCrypto.gKey_ReadPrivateFromString(auxKYAKey)
                        Dim auxURL As String = HttpContext.Current.Request.RawUrl
                        Dim auxBagValues As New clshrcBagValues(auxCrypto.DecryptMessageData(auxLicense))
                        auxLicParam = New clshrcBagValues(auxLicResult)
                        auxLicParam.gValue_Add(auxBagValues)
                        Dim auxRunInDemo As String = auxLicParam.gValue_Get("LICENSE_DEMO")
                        If auxRunInDemo <> "" Then
                            Select Case auxRunInDemo
                                Case "TRUE", "1"
                                    hrcDemoMode = "1"
                            End Select
                        End If

                        Dim auxLicenseFrom As Date = m_Conn.gField_GetDate(auxLicParam.gValue_Get("LICENSE_FROM"))
                        Dim auxLicenseTo As Date = m_Conn.gField_GetDate(auxLicParam.gValue_Get("LICENSE_TO"))

                        If auxLicenseFrom <> Nothing And auxLicenseTo <> Nothing Then
                            If auxLicenseFrom < Now And Now < auxLicenseTo.AddDays(1) Then
                                auxAuthorize = True
                            End If
                        ElseIf auxLicenseFrom <> Nothing Then
                            If auxLicenseFrom < Now Then
                                auxAuthorize = True
                            End If
                        ElseIf auxLicenseTo <> Nothing Then
                            If Now < auxLicenseTo Then
                                auxAuthorize = True
                            End If
                        Else
                            'No tiene rango de fechas
                            auxAuthorize = True
                        End If
                        If InStr("," & auxBagValues.gValue_Get("HOSTNAME", "").ToString.ToUpper & ",", "," & Environment.MachineName.ToUpper & ",") = 0 Then
                            auxAuthorize = False
                        End If
                        If auxAuthorize Then
                            Dim auxSystemType As String = auxLicParam.gValue_Get("SYSTEM_TYPE")
                            'If auxSystemType <> "" Then
                            coSystemType = Val(auxSystemType)
                            'End If
                            hrcLicParam = auxLicParam
                            Exit For
                        End If
                    End If
                Next

                If auxAuthorize Then
                    hrcLicParam.gReadOnly_Set()
                    hrcLicParam.gExport_Disabled()
                Else
                    hrcLicParam = Nothing
                End If
            End If
        End SyncLock
    End Sub
    Public Function gLicesing_Work(ByVal pCod As Integer) As String
        gSys_DebugLogAdd("Licensing-Work-Start")
        Dim auxReturn As String = ""
        Dim auxDT As DataTable = m_Conn.gConn_Query("SELECT licdsc,licrequestID,licrequest,licresult,lickyaautcod,lickyacod " _
                                                    & " FROM Q_LIC WHERE liccod=" & pCod)
        If auxDT.Rows.Count <> 0 Then
            Dim auxLicRequestID As String = m_Conn.gField_GetString(auxDT.Rows(0)("licrequestID"), "").Trim
            Dim auxLicDsc As String = m_Conn.gField_GetString(auxDT.Rows(0)("licdsc"), "").Trim
            Dim auxLicRequest As String = m_Conn.gField_GetString(auxDT.Rows(0)("licrequest"), "").Trim
            Dim auxLicResult As String = m_Conn.gField_GetString(auxDT.Rows(0)("licresult"), "").Trim
            Dim auxLicKyaAutcod As Integer = m_Conn.gField_GetInt(auxDT.Rows(0)("lickyaautcod"), -1)
            Dim auxLicKyaCod As Integer = m_Conn.gField_GetInt(auxDT.Rows(0)("lickyacod"), -1)
            If auxLicRequestID = "" And auxLicRequest = "" And auxLicKyaAutcod > 0 And auxLicKyaCod > 0 Then
                'Servidor
                Dim auxBagValues As New clshrcBagValues
                'Generacion del ID de autorizacion
                auxLicRequestID = m_Conn.gField_GetUniqueID.Replace("-", "")
                auxLicDsc = "Solicitud:"
                auxLicDsc &= m_Conn.gConn_QueryValueString("SELECT kyadsc FROM Q_KYA WHERE kyacod =" & auxLicKyaCod)
                auxLicDsc &= auxLicRequestID
                For Each auxRow As DataRow In m_Conn.gConn_Query("SELECT licdetdsc,licdetval FROM Q_LICDET WHERE liccod=" & pCod).Rows
                    auxBagValues.gValue_Add(auxRow("licdetdsc").ToString, auxRow("licdetval").ToString)
                Next
                If auxBagValues.gValue_Get("LICENSE_DSC", "") <> "" Then
                    auxLicDsc &= "-" & auxBagValues.gValue_Get("LICENSE_DSC", "")
                End If
                m_Conn.gConn_Update("UPDATE Q_LIC SET " _
                            & " licrequestID=" & m_Conn.gFieldDB_GetString(auxLicRequestID) _
                            & ",licdsc=" & m_Conn.gFieldDB_GetString(auxLicDsc) _
                            & " WHERE liccod =" & pCod)
            ElseIf auxLicRequestID <> "" And auxLicResult = "" And auxLicKyaAutcod < 1 And auxLicKyaCod < 1 Then
                'Cliente
                'Es el pedido del autorizacion de licencia
                Dim auxCrypto As New clsCrypto
                auxCrypto.ActivateLicense(coDBKey)
                auxCrypto.EncryptionType = clsCrypto.enumEncryptionTypes.coRSA
                auxCrypto.gKey_ReadPublicFromString(hrcLICAutKey)
                Dim auxURL As String = HttpContext.Current.Request.Url.Authority & "/" & HttpContext.Current.Request.ApplicationPath
                Dim auxBagValue As New clshrcBagValues
                auxBagValue.gValue_Add("requestid", auxLicRequestID)
                auxBagValue.gValue_Add("hostname", Environment.MachineName)
                auxBagValue.gValue_Add("date", m_Conn.gFieldDB_GetDateTimeStandard(Now.ToUniversalTime))
                auxBagValue.gValue_Add("URL1", auxURL)
                auxBagValue.gValue_Add("NUMBER_OF_PROCESSOR", Environment.ProcessorCount)
                auxBagValue.gValue_Add("SYS_DIR", Environment.SystemDirectory)
                auxBagValue.gValue_Add("OS_VERSION", Environment.OSVersion.ToString)
                auxBagValue.gValue_Add("USERNAME", Environment.UserName)
                auxBagValue.gValue_Add("USERDOMAIN", Environment.UserDomainName)
                auxLicRequest = auxCrypto.EncryptMessageData(auxBagValue.Config_GetStream)
                auxLicDsc = "Solicitud:" & auxLicRequestID & "-" & Now
                m_Conn.gConn_Update("UPDATE Q_LIC SET " _
                                 & " licrequest=" & m_Conn.gFieldDB_GetString(auxLicRequest) _
                                 & ",licdsc=" & m_Conn.gFieldDB_GetString(auxLicDsc) _
                                 & " WHERE liccod =" & pCod)
            ElseIf auxLicResult = "" And auxLicKyaAutcod > 0 And auxLicKyaCod > 0 Then
                'Sevidor
                'Es la generacion de la licencia

                'Lee la clave privada para la autorización
                Dim auxKeyAutoriz_Prv As String = m_Conn.gConn_QueryValueString("SELECT kyaprv FROM Q_KYA " _
                                                        & " WHERE kyacod =" & auxLicKyaAutcod)
                Dim auxCrypto As New clsCrypto
                auxCrypto.ActivateLicense(coDBKey)
                auxCrypto.EncryptionType = clsCrypto.enumEncryptionTypes.coRSA
                auxCrypto.gKey_ReadPrivateFromString(auxKeyAutoriz_Prv)
                Dim auxURL As String = HttpContext.Current.Request.RawUrl
                Dim auxBagValues As New clshrcBagValues(auxCrypto.DecryptMessageData(auxLicRequest))
                If auxBagValues.gValue_Get("requestid", "") <> auxLicRequestID Then
                    auxReturn = "Emisión:no coinciden los IDs de requerimientos"
                Else
                    'Coincide la autorización
                    'Lee la clave publica para la emisión de la licencia
                    Dim auxKYAKey As String = m_Conn.gConn_QueryValueString("SELECT kyapub FROM Q_KYA " _
                                                       & " WHERE kyacod =" & auxLicKyaCod)
                    auxCrypto.gKey_ReadPublicFromString(auxKYAKey)
                    auxBagValues.gValue_Add("AUTORIZ_DATE", m_Conn.gFieldDB_GetDateTimeStandard(Now.ToUniversalTime))
                    If auxLicDsc <> "" Then
                        auxBagValues.gValue_Add("LICENSE_DSC", m_Conn.gField_GetString(auxLicDsc))
                    End If
                    Dim auxLICDET As New List(Of String)

                    For Each auxRow As DataRow In m_Conn.gConn_Query("SELECT licdetdsc,licdetval FROM Q_LICDET WHERE liccod=" & pCod).Rows
                        auxBagValues.gValue_Add(auxRow("licdetdsc").ToString, auxRow("licdetval").ToString)
                        auxLICDET.Add(auxRow("licdetdsc").ToString.ToUpper)
                    Next
                    For Each auxValue As KeyValuePair(Of String, Object) In auxBagValues.Values
                        Select Case auxValue.Key
                            Case "HOSTNAME"
                                If auxLICDET.IndexOf(auxValue.Key) = -1 Then
                                    m_Conn.gConn_Insert("INSERT INTO Q_LICDET (licdetcod,liccod,licdetdsc,licdetval ) " _
                                                        & " VALUES({#QUERYNEXTCOD#}," & pCod & "," & m_Conn.gFieldDB_GetString(auxValue.Key) & "," & m_Conn.gFieldDB_GetString(auxValue.Value.ToString) & ")")
                                End If
                        End Select
                    Next
                    'auxBagValues_.gValue_Add("REQUESTID", m_Conn.gFieldDB_GetDateTimeStandard(Now.ToUniversalTime))
                    auxLicResult = auxCrypto.EncryptMessageData(auxBagValues.Config_GetStream)
                    Dim auxBagValuesLicense As New clshrcBagValues
                    auxBagValuesLicense.gValue_Add("LICENSE_CONTENT", auxLicResult)
                    'Crea la firma del request para controlar que viene de _intelimedia
                    auxCrypto.gKey_ReadPrivateFromString(auxKeyAutoriz_Prv)
                    Dim auxLicenseSign As String = auxCrypto.gRSA_SignMessageData(auxLicResult)
                    auxBagValuesLicense.gValue_Add("LICENSE_SIGN", auxLicenseSign)

                    'auxCrypto.gKey_ReadPublicFromString(hrcLICAutKey)
                    'auxCrypto.gRSA_VerifyMessageData(auxLicResult, auxLicenseSign)
                    m_Conn.gConn_Update("UPDATE Q_LIC SET " _
                                     & " licrequest=" & m_Conn.gFieldDB_GetString(auxLicRequest) _
                                     & ",licresult=" & m_Conn.gFieldDB_GetString(auxBagValuesLicense.Config_GetStream) _
                                     & ",licdsc=" & m_Conn.gFieldDB_GetString(auxLicDsc) _
                                     & " WHERE liccod =" & pCod)
                End If


            ElseIf auxLicResult <> "" And auxLicKyaAutcod < 1 And auxLicKyaCod < 1 Then
                'Cliente
                'Es el ingreso de la licencia en el cliente
                Dim auxKYAKey As String
                'Lee la clave privada para la autorización
                auxKYAKey = hrcLICKey
                Dim auxCrypto As New clsCrypto
                auxCrypto.ActivateLicense(coDBKey)
                auxCrypto.EncryptionType = clsCrypto.enumEncryptionTypes.coRSA
                Dim auxBagValuesLicense As New clshrcBagValues(auxLicResult)
                Dim auxLicenseSign As String = auxBagValuesLicense.gValue_Get("LICENSE_SIGN", "")
                Dim auxLicense As String = auxBagValuesLicense.gValue_Get("LICENSE_CONTENT", "")
                auxCrypto.gKey_ReadPublicFromString(hrcLICAutKey)
                If auxCrypto.gRSA_VerifyMessageDataWithPublicKey(auxLicense, auxLicenseSign) Then
                    auxCrypto.gKey_ReadPrivateFromString(auxKYAKey)
                    Dim auxURL As String = HttpContext.Current.Request.RawUrl
                    Dim auxBagValues As New clshrcBagValues(auxCrypto.DecryptMessageData(auxLicense))
                    If auxBagValues.gValue_Get("requestid", "") <> auxLicRequestID Then
                    Else
                        'Coincide la autorización
                        auxLicDsc = auxBagValues.gValue_Get("LICENSE_DSC", auxLicDsc)
                        m_Conn.gConn_Update("UPDATE Q_LIC SET " _
                                            & " licresult=" & m_Conn.gFieldDB_GetString(auxLicResult) _
                                            & ",licdsc=" & m_Conn.gFieldDB_GetString(auxLicDsc) _
                                            & ",licenabled={#TRUE#}" _
                                            & " WHERE liccod =" & pCod)
                    End If
                Else
                    auxReturn &= "Licencia inválida:no emitida correctamente"
                End If

            End If

        End If
        gSys_DebugLogAdd("Licensing-Work-End")
        Return auxReturn
    End Function
End Class






