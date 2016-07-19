Public Class ClsZamanak

    Public Const strClientId As String = "api@zamanak.ir"
    Public Const strClientSecret As String = "9AmbEG61AgW3CQoSV1p3A4tS9CZ"
    Public Const intAgencyUid As Integer = 7192
    Public Const strAgencyToken As String = "ZamanakSoapV45671215b3cc34"

    '{"uid":"7304","token":"ZamanakSoapV455acd394522b5"}

#Region "ZamanakMethods"

    Public Function Authenticate(ByVal userName As String, ByVal passWord As String, ByRef intUid As Integer, ByRef strToken As String, ByRef strMessage As String) As Boolean

        Try

            Dim newService As New ZamanakWebService.Default_Service_SoapServer_ZamanakV4Service
            Dim result As Object() = newService.authenticate(strClientId, strClientSecret, userName, passWord)

            intUid = DirectCast(result(1), System.Xml.XmlElement).InnerText.Remove(0, 3)
            strToken = DirectCast(result(2), System.Xml.XmlElement).InnerText.Remove(0, 5)
            Return True

        Catch ex As Exception
            strMessage = ex.Message
            Return False
        End Try

    End Function

    Public Function UploadAudio(ByVal intUid As Integer, ByVal strToken As String, ByVal fileBytes As Byte(), ByRef strMessage As String) As Integer

        Try
            Dim newService As New ZamanakWebService.Default_Service_SoapServer_ZamanakV4Service
            Dim strFile As String = Convert.ToBase64String(fileBytes)
            Dim VoiceID As Integer = newService.uploadEncodedAudio(strClientId, strClientSecret, intUid, strToken, strFile)
            Return VoiceID

        Catch ex As Exception
            strMessage = ex.Message
            Return 0
        End Try

    End Function

    Public Function SendVoiceSMS(ByVal intUid As Integer, ByVal strToken As String, ByVal strName As String, ByVal arrDest() As Object, ByVal recordingId As Integer, ByVal startTime As String, ByVal stopTime As String, ByVal repeatTotal As Integer, ByRef strMessage As String) As Integer

        Try
            Dim newService As New ZamanakWebService.Default_Service_SoapServer_ZamanakV4Service
            Dim result As Object() = newService.newCampaignbyNumbers(strClientId, strClientSecret, intUid, strToken, strName, arrDest, recordingId, startTime, stopTime, repeatTotal)

            If result.Count() = 1 AndAlso result(0).IndexOf("Error") <> -1 Then
                strMessage = result(0)
                Return 0
            End If

            Dim campID As Integer = DirectCast(result(1), System.Xml.XmlElement).InnerText.Remove(0, 6)
            Return campID

        Catch ex As Exception
            strMessage = ex.Message
            Return 0
        End Try

    End Function

    Public Function SendVoiceSMS_Minute(ByVal intUid As Integer, ByVal strToken As String, ByVal strName As String, ByVal arrDest() As Object, ByVal recordingId As Integer, ByVal startTime As String, ByVal stopTime As String, ByVal repeatTotal As Integer, ByRef strMessage As String) As Integer

        Try
            Dim newService As New ZamanakWebService.Default_Service_SoapServer_ZamanakV4Service
            Dim result As Object() = newService.newCampaignMinute(strClientId, strClientSecret, intUid, strToken, strName, arrDest, recordingId, startTime, stopTime, repeatTotal)

            If result.Count() = 1 AndAlso result(0).IndexOf("Error") <> -1 Then
                strMessage = result(0)
                Return 0
            End If

            Dim voiceSMSID As Integer = DirectCast(result(1), System.Xml.XmlElement).InnerText.Remove(0, 6)
            Dim campID As Integer = DirectCast(result(1), System.Xml.XmlElement).InnerText.Remove(0, 6)
            Return campID

        Catch ex As Exception
            strMessage = ex.Message
            Return 0
        End Try

    End Function

    Public Function CalculatePackCount(ByVal intUid As Integer, ByVal strToken As String, ByVal strName As String, ByVal arrDest() As Object, ByVal intRecordID As Integer, ByRef strMessage As String) As Integer

        Try
            Dim intPackCount As Integer = 0
            Dim newService As New ZamanakWebService.Default_Service_SoapServer_ZamanakV4Service
            Dim result As Object() = newService.calculateCost(strClientId, strClientSecret, intUid, strToken, strName, arrDest, intRecordID, 1)

            intPackCount = DirectCast(result(7), System.Xml.XmlElement).InnerText.Remove(0, 10)
            Return intPackCount

        Catch ex As Exception
            strMessage = ex.Message
            Return 0
        End Try

    End Function

    Public Function GetCampaignStatus(ByVal intUid As Integer, ByVal strToken As String, ByVal campId As Integer, ByRef successCnt As Integer, ByRef unsuccessCnt As Integer, ByRef totalCnt As Integer, ByRef status As Integer, ByRef strMessage As String)

        Try
            Dim newService As New ZamanakWebService.Default_Service_SoapServer_ZamanakV4Service
            Dim result As Object() = newService.getCampaignStatus(strClientId, strClientSecret, intUid, strToken, campId)
            successCnt = DirectCast(result(9), System.Xml.XmlElement).InnerText.Remove(0, 11)
            unsuccessCnt = DirectCast(result(10), System.Xml.XmlElement).InnerText.Remove(0, 13)
            totalCnt = DirectCast(result(8), System.Xml.XmlElement).InnerText.Remove(0, 13)
            status = DirectCast(result(11), System.Xml.XmlElement).InnerText.Remove(0, 6)
        Catch ex As Exception
            strMessage = ex.Message
        End Try

    End Function

    Public Function SendPollVoiceSMS(ByVal intUid As Integer, ByVal strToken As String, ByVal strName As String, ByVal arrDest() As Object, ByVal recordingId As Integer, ByVal arrAnswers() As Object, ByVal startTime As String, ByVal stopTime As String, ByVal repeatTotal As Integer, ByRef strMessage As String) As Integer

        Try
            Dim newService As New ZamanakWebService.Default_Service_SoapServer_ZamanakV4Service
            Dim result As Object() = newService.newPollCampaignByNumbers(strClientId, strClientSecret, intUid, strToken, strName, arrDest, recordingId, arrAnswers, startTime, stopTime, repeatTotal)
            Dim campID As Integer = CInt(result(0))
            Return campID

        Catch ex As Exception
            strMessage = ex.Message
            Return 0
        End Try

    End Function

    Public Function TransferCredit(ByVal intUid As Integer, ByVal amount As String, ByRef strMessage As String) As Boolean

        Try
            Dim newService As New ZamanakWebService.Default_Service_SoapServer_ZamanakV4Service
            Dim result As String = newService.transfer(strClientId, strClientSecret, intAgencyUid, strAgencyToken, intUid, amount)
            Return True

        Catch ex As Exception
            strMessage = ex.Message
            Return False
        End Try

    End Function
#End Region

    ''Public Function CheckUserStatus(ByVal Username As String) As Boolean

    ''    Dim tadpUsers As New BusinessObject.dstUsersTableAdapters.spr_User_Check_SelectTableAdapter
    ''    Dim dtblUsers As BusinessObject.dstUsers.spr_User_Check_SelectDataTable = Nothing
    ''    dtblUsers = tadpUsers.GetData(Username)

    ''    If dtblUsers.Rows.Count > 0 Then
    ''        Return dtblUsers.Rows(0)("IsZamanak")
    ''    Else
    ''        Return False
    ''    End If

    ''End Function

    Public Function GetRegisteredUser(ByVal ID As Integer, ByVal Username As String, ByRef intUid As Integer, ByRef strToken As String) As String

        ''Dim tadpRegister As New BusinessObject.dstZamanakTableAdapters.spr_Register_SelectTableAdapter
        ''Dim dtblRegister As BusinessObject.dstZamanak.spr_Register_SelectDataTable = Nothing

        ''dtblRegister = tadpRegister.GetData(1, ID, Username, 0, "")

        ''If dtblRegister.Rows.Count > 0 Then
        ''    intUid = dtblRegister.Rows(0)("Uid")
        ''    strToken = dtblRegister.Rows(0)("Token")
        ''    Return dtblRegister.Rows(0)("Password")
        ''Else
        ''    Return ""
        ''End If

        intUid = 7304
        strToken = "ZamanakSoapV455acd394522b5"
        Return "ati6077"

    End Function

    Public Function GetUserVoiceCredit(ByVal UserID As Integer) As Double

        ''Dim tadpUserVoiceCredit As New BusinessObject.dstZamanakTableAdapters.spr_UserVoiceCredit_SelectTableAdapter
        ''Dim dtblUserVoiceCredit As BusinessObject.dstZamanak.spr_UserVoiceCredit_SelectDataTable = Nothing
        ''dtblUserVoiceCredit = tadpUserVoiceCredit.GetData(UserID)
        ''If dtblUserVoiceCredit.Rows.Count > 0 Then
        ''    If dtblUserVoiceCredit.Rows(0)(0) = -1 Then
        ''        Return Double.MaxValue
        ''    Else
        ''        Return dtblUserVoiceCredit.Rows(0)(0)
        ''    End If
        ''Else
        ''    Return 0
        ''End If

        Return Double.MaxValue
    End Function

    Public Function MinusUserCharge(ByVal UserID As Integer, ByVal CompanyID As Integer, ByVal strRemark As String, ByVal dblVoiceCount As Double, ByVal dblLeftVoiceCredit As Double)

        ''Dim qryUserCharge As New BusinessObject.dstUserChargeTableAdapters.QueriesTableAdapter
        ''qryUserCharge.spr_UserCharge_Insert(UserID, CompanyID, strRemark, dblVoiceCount, 8, True, 1386, Nothing, 1, 15, Nothing, "کسر اعتبار از صفحه ارسال مربوطه", dblLeftVoiceCredit)

    End Function

    Public Function GetRecordCount(ByVal intRecordID As Integer) As Integer

        Try
            Dim tadpRecords As New BusinessObject.dstZamanakTableAdapters.spr_Record_List_SelectTableAdapter
            Dim dtblRecords As BusinessObject.dstZamanak.spr_Record_List_SelectDataTable = Nothing
            dtblRecords = tadpRecords.GetData(2, -1, intRecordID, "", 0)
            If dtblRecords.Rows.Count > 0 Then
                Return dtblRecords.First().Duration
            Else
                Return 0
            End If

        Catch ex As Exception
            Return 0
        End Try

    End Function

    Public Function GetRecordID(ByVal intRecordID As Integer) As Integer

        Try
            Dim tadpRecords As New BusinessObject.dstZamanakTableAdapters.spr_Record_List_SelectTableAdapter
            Dim dtblRecords As BusinessObject.dstZamanak.spr_Record_List_SelectDataTable = Nothing
            dtblRecords = tadpRecords.GetData(2, -1, intRecordID, "", 0)
            If dtblRecords.Rows.Count > 0 Then
                Return dtblRecords.First().RecordID
            Else
                Return 0
            End If

        Catch ex As Exception
            Return 0
        End Try

    End Function

    Public Function GetRecordName(ByVal intRecordID As Integer) As String

        Try
            Dim tadpRecords As New BusinessObject.dstZamanakTableAdapters.spr_Record_List_SelectTableAdapter
            Dim dtblRecords As BusinessObject.dstZamanak.spr_Record_List_SelectDataTable = Nothing
            dtblRecords = tadpRecords.GetData(2, -1, intRecordID, "", 0)
            If dtblRecords.Rows.Count > 0 Then
                Return dtblRecords.First().Name
            Else
                Return ""
            End If

        Catch ex As Exception
            Return ""
        End Try

    End Function

    Public Function GetCampInfo(ByVal intCampHeaderID As Integer) As String()

        ''Try
        ''    Dim result(3) As String
        ''    Dim tadpCampHeader As New BusinessObject.dstZamanakTableAdapters.spr_CampaignHeader_SelectTableAdapter
        ''    Dim dtblCampHeader As BusinessObject.dstZamanak.spr_CampaignHeader_SelectDataTable = Nothing
        ''    dtblCampHeader = tadpCampHeader.GetData(1, intCampHeaderID, -1)
        ''    If dtblCampHeader.Rows.Count > 0 Then
        ''        result(0) = dtblCampHeader.First().CampaignID
        ''        result(1) = dtblCampHeader.First().Name
        ''        result(2) = dtblCampHeader.First().PollName
        ''        result(3) = dtblCampHeader.First().CampTypeID
        ''    End If

        ''    Return result
        ''Catch ex As Exception

        ''End Try

    End Function

    Public Function CheckDuplicateVoice(ByVal fileName As String) As Boolean

        Dim tadpRecords As New BusinessObject.dstZamanakTableAdapters.spr_Record_List_SelectTableAdapter
        Dim dtblRecords As BusinessObject.dstZamanak.spr_Record_List_SelectDataTable = Nothing
        dtblRecords = tadpRecords.GetData(3, 0, 0, fileName, 0)

        If dtblRecords.Rows.Count > 0 Then
            Return True
        Else
            Return False
        End If

    End Function

End Class
