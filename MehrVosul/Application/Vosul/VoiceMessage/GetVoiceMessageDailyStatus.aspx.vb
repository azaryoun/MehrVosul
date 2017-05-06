Public Class GetVoiceMessageDailyStatus
    Inherits System.Web.UI.Page
    Public Structure STC_Status
        Public ReceiverNumber As String
        Public Status As String
        Public AnswerDuration As Integer
        Public Response As String
    End Structure
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

    End Sub

    Protected Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click

        Dim tadpSystemSetting As New BusinessObject.dstSystemSettingTableAdapters.spr_SystemSetting_SelectTableAdapter
        Dim dtblSystemSetting As BusinessObject.dstSystemSetting.spr_SystemSetting_SelectDataTable = Nothing

        dtblSystemSetting = tadpSystemSetting.GetData()
        Dim drwSystemSetting As BusinessObject.dstSystemSetting.spr_SystemSetting_SelectRow = dtblSystemSetting.Rows(0)

        ''get total send of day 
        Dim oVoiceSMS As New VoiceSMS.RahyabVoiceSend  'ZamanakWebService.Default_Service_SoapServer_ZamanakV4Service
        Dim strMessage As String = ""

        Dim intTotalContact = oVoiceSMS.DailyVoiceCallCount_Mehr("vesal", "matchautoreplay123", drwSystemSetting.VoiceSMSUID, drwSystemSetting.VoiceSMSToken, strMessage)

        ''get status sent voice sms of the current day
        Dim tadpGetCurrentDayVoiceSMS As New BusinessObject.dstZamanakTableAdapters.spr_GetCurrentDayVoiceSMS_SelectTableAdapter
        Dim dtblGetCurrentDayVoiceSMS As BusinessObject.dstZamanak.spr_GetCurrentDayVoiceSMS_SelectDataTable = Nothing

        Dim dteThisDate As Date = Date.Now
        dtblGetCurrentDayVoiceSMS = tadpGetCurrentDayVoiceSMS.GetData(dteThisDate)


        Dim VoiceStatus As VoiceSMS.STC_Status()

        For Each drwGetCurrentDayVoiceSMS As BusinessObject.dstZamanak.spr_GetCurrentDayVoiceSMS_SelectRow In dtblGetCurrentDayVoiceSMS

            Dim strCampin() As String = drwGetCurrentDayVoiceSMS.strMessage.Split("#")
            Dim intCampain As Integer = CInt(strCampin(0))

            '' VoiceStatus = oVoiceSMS.StatusVoiceSMS_Details("09905389810", "mehrvosul", intCampain, 1, strMessage)
            VoiceStatus = oVoiceSMS.StatusVoiceSMS_Details_Mehr("09905389810", "mehrvosul", 9899, "ZamanakSoapV45815e7f52aaab", intCampain, 1, strMessage)

        Next




    End Sub
End Class