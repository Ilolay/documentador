Imports System.Data
Imports System.Data.SqlClient
Imports System.Data.OleDb
Imports System.Data.OracleClient
Imports System.IO
Imports System.Collections.Generic
Imports System.Globalization
Imports Intelimedia.Hercules.Design
Imports Intelimedia.imComponentes

Imports System.Text
'Imports System.Reflection
'<Assembly: AssemblyKeyFile("connclient.snk")> 
Namespace Intelimedia.imComponentes_test
    Public Class imConnClient : Inherits Intelimedia.Hercules.Storage.clsHrcConnClient

    End Class
    Public Class clshrcDebugging
        Private m_Debug As clshrcDebug
        Public Sub gDebug_On(ByVal pDebugLog As String)
            If pDebugLog <> "" Then
                m_Debug = New clshrcDebug
                m_Debug.gDebug_On(pDebugLog)
            End If
        End Sub
        Public Sub gDebug_Off()
            m_Debug.gDebug_Off()
            m_Debug = Nothing
        End Sub
        Public Sub gDebug_Add(ByVal pMsg As String)
            If m_Debug IsNot Nothing Then
                m_Debug.gDebug_Add(pMsg)
            End If
        End Sub
    End Class
End Namespace
Namespace Intelimedia.Hercules.Storage_test

    Public Class clsHrcConnClient
        Public Const imProductVersion As Integer = 1518
        Public Const imProductName As String = "imConnClient"

        '///////////////////////////////////////////
        '//
        '//        iDBClient
        '//        Conexion con datos
        '//         'Version 0.4 -
        '//         'Version 0.5 - 3/6/2006 - GetIndexes, getTables, getFieldIndex
        '//         'Version 0.6 - 28/10/2006 -  BinariesTablePreviewName
        '//         'Version 0.71 - 12/10/2006 - New type webproxy, LoggingType,LogFolder,
        '//             Existe integridad transaccion/log - si se fijó transacciones y no se escribe el log, falla
        '//             Agregado gDBLOG_write, gDB_TRAN_Write, gConn_TransactionBeginExe,gConn_TransactionEndExe, gConn_TransactionFailExe
        '//         'Version 0.76 - 19/11/2006 - bastypeUndefined, Property basType
        '//         'Version 0.77 - 12/12/2006 - cambios en gConn_FileToBLOB. Guarda filename y preview File Name (campos dsc/obs)
        '//         'Ver 0.80 - 8/1/2007 - Agregado LogFileName/log_write escribe {#chr13#}/{#chr9#} en caracteres no imprimibles.bugs
        '//         'Ver 0.82 - 20/4/07 - Property isConnected
        '//         'Ver 0.84 - 4/6/07 - prvfs -> clsiFS,clsVBinteraction
        '//         'Ver 1.0 - 22/07/08 - ADO.NET completo - sin DAO
        '//         VER 1477 - Se agregó el control de m_debugenabled antes de llamar a debug.
        '///////////////////////////////////////////
        Public Enum hrcConnError As Integer
            coNoError = 0
            coGeneral = 1
            coActionDenied = 2
        End Enum

        Public Enum hrcBasType As Short
            coBasTypeUndefined = -1
            coBasTypeJet = 0
            coBasTypeODBC = 1
            coBasTypeExcel = 2

            coBasTypeText = 4   'Texto separado por caracter
            coBasTypeTextWithSeparator = 4   'Texto separado por caracter

            coBasTypeWebProxy = 5
            coBasTypeSQLServer = 6  'SQL Server genérico (2005 + )
            coBasTypeSQLServer2005 = 6  'SQL 2005
            coBasTypeOracle = 7
            coBasTypeSQLServerWithAttach = 8
            coBasTypeTextPosition = 9   'Archivo de texto posicional
            coBasTypeSQLServer2000 = 10
        End Enum
        Public Enum hrcConnectionType As Short
            coUndefined = -1
            coADONETSQL = 0
            coADONETOLEDB = 1
            coADONETOracle = 2
            coOther = 3
        End Enum
        Public Enum hrcLogFormat As Short
            coClear = 0
            coDBL = 1
        End Enum
        Public Enum hrcLoggingType As Short
            coLoggingOff = 0
            coLoggingUpdates = 1
            coLoggingQuery = 2
            coLoggingAll = 10
        End Enum
        Public Enum hrcSavingMode As Short
            coNoBinaries = -1   'No permite almacenar binarios
            coinDB = 0      'Almacena binarios en campos de las tablas
            coinDBTable = 2 'Almacena binarios en una tabla separada llamada CONBINARIES Y CONBINARIESPREVIEWS (los nombres pueden cambiar)
            inFiles = 1     'Almacena binarios en una carpeta del equipo
        End Enum
        Public Enum enumMimeTypes As Integer
            coUndefined = -1
            coGIF = 1
            coJPG = 2
            coPNG = 3
            coTIF = 4
            coBMP = 5
            coXLS = 6
            coPDF = 7
            coDOC = 8
            coZIP = 9
            coEXE = 10
            coSWF = 11
            coRTF = 12
            coDOCX = 13
            coXLSX = 14
            coPPTX = 15
            coPPT = 16
            coRAR = 17
            coMP3 = 30
            coMIDI = 31
            coWAV = 32
            coPPS = 33

            'video
            coMPG = 50
            coMPEG = 51
            coAVI = 52
            coFLV = 53
            coMOV = 54

            'HTML
            coHTML = 55

            coTXT = 56
            coVSD = 57

            coGZIP = 58
            coXLSB = 59

            coMSG = 60

            coCSS = 61
            coJS = 62
            coTTF = 63
        End Enum
        Public Function gConn_Query(ByVal pSelect As String) As DataTable

        End Function
        Public LastErrorDescription As String
        Private m_DebugEnabled As Boolean = False
        Private m_DebugLog As String = ""
        Public Sub gDebug_On(ByVal pDebugLog As String)
            If pDebugLog <> "" Then
                m_DebugEnabled = True
                'm_Debug = New clshrcDebug
                'm_Debug.gDebug_On(pDebugLog)Ç
                Dim auxFS As New clsiFS
                Dim auxFileName As String = auxFS.gFileName(pDebugLog)
                Dim auxFolder As String = Left(pDebugLog, Len(pDebugLog) - Len(auxFileName))
                auxFileName = auxFS.gFileName_GetStrictName(auxFileName)
                m_DebugLog = auxFolder & auxFileName
                If auxFolder <> "" Then
                    If auxFS.gDirExists(auxFolder) Then
                        gDebug_Add("Start debug on:" & Now.ToString)
                    Else
                        'No habilita para evitar problemas
                        m_DebugEnabled = False
                    End If
                End If


            End If
        End Sub
        Public Sub gDebug_Off()
            m_DebugEnabled = False
            'm_Debug.gDebug_Off()
        End Sub
        Private Sub gDebug_Add(ByVal pMsg As String, _
                               ByVal pLevel As Integer)
            If m_DebugEnabled Then
                'm_Debug.gDebug_Add(pMsg, 0)
                Try
                    Dim auxFileName As String = m_DebugLog.Replace("{#DATE#}", Today.ToString("ddMMyyyy"))
                    File.AppendAllText(auxFileName, Now.ToUniversalTime.ToString("yyyy-MM-dd HH:mm:ss:fff") _
                                       & "-" & pMsg & System.Environment.NewLine)
                Catch ex As IOException

                End Try
            End If
            'If m_DebugEnabled Then
            '    m_Debug.gDebug_Add(pMsg, pLevel)
            'End If 
        End Sub
        Private Sub gDebug_Add(ByVal pMsg As String)
            If m_DebugEnabled Then
                'm_Debug.gDebug_Add(pMsg, 0)
                Try
                    Dim auxFileName As String = m_DebugLog.Replace("{#DATE#}", Today.ToString("ddMMyyyy"))
                    File.AppendAllText(auxFileName, Now.ToUniversalTime.ToString("yyyy-MM-dd HH:mm:ss:fff") _
                                       & "-" & pMsg & System.Environment.NewLine)
                Catch ex As IOException

                End Try
            End If
        End Sub
    End Class

End Namespace

Namespace Intelimedia.imComponentes
    Friend Class clshrcDebug
        Private m_DebugLevel As Integer = 0
        Public Property DebugLevel() As Integer
            Get
                Return m_DebugLevel
            End Get
            Set(ByVal value As Integer)
                m_DebugLevel = value
            End Set
        End Property
        Private m_DebugEnabled As Boolean = False

        Public Property DebugEnabled() As Boolean
            Get
                Return m_DebugEnabled
            End Get
            Set(ByVal value As Boolean)
                m_DebugEnabled = value
            End Set
        End Property
        Private m_DebugLog As String = ""
        Public Property DebugLog() As String
            Get
                Return m_DebugLog
            End Get
            Set(ByVal value As String)
                m_DebugLog = value
            End Set
        End Property
        Private m_MaxDays As Integer = -1
        Public Property MaxDays() As Integer
            Get
                Return m_MaxDays
            End Get
            Set(ByVal value As Integer)
                m_MaxDays = value
            End Set
        End Property
        Public Enum enumFileMode As Short
            coDefault = -1
            coByDay = 1 'Reemplaza la variable {#DATE#} por la fecha
        End Enum
        Private m_FileMode As enumFileMode = enumFileMode.coDefault
        Public Property FileMode() As String
            Get
                Return m_FileMode
            End Get
            Set(ByVal value As String)
                m_FileMode = value
            End Set
        End Property
        Public Sub gDebug_On(ByVal pDebugLog As String)
            If pDebugLog <> "" Then
                m_DebugEnabled = True
                m_DebugLog = pDebugLog
                gDebug_Add("Start on:" & Now.ToString & "-Debug level:" & m_DebugLevel)
            End If
        End Sub
        Public Sub gDebug_Off()
            m_DebugEnabled = False
        End Sub
        Public Sub gDebug_Add(ByVal pMsg As String)
            gDebug_Add(pMsg, 0)
        End Sub
        Friend Sub gDebug_Add(ByVal pMsg As String, _
                              ByVal pLevel As Integer)
            If m_DebugEnabled Then
                If pLevel >= m_DebugLevel Then
                    Try
                        Dim auxFileName As String = m_DebugLog
                        ' If m_FileMode = enumFileMode.coByDay Then
                        auxFileName = auxFileName.Replace("{#DATE#}", Today.ToString("ddMMyyyy"))
                        'End If
                        IO.File.AppendAllText(auxFileName, Now.ToUniversalTime.ToString("yyyy-MM-dd HH:mm:ss.fff") & "-" & pMsg & System.Environment.NewLine)
                    Catch ex As Exception

                    End Try
                End If
            End If
        End Sub
    End Class
End Namespace