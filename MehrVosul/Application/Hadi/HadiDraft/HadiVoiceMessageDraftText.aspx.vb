Public Class HadiVoiceMessageDraftText
    Inherits System.Web.UI.Page
    Public Const CLIENT_ID As String = "api@zamanak.ir"
    Public Const CLIENT_SECRET As String = "9AmbEG61AgW3CQoSV1p3A4tS9CZ"

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load


        Dim dtblUserLogin As BusinessObject.dstUser.spr_User_Login_SelectDataTable = CType(Session("dtblUserLogin"), BusinessObject.dstUser.spr_User_Login_SelectDataTable)
        Dim drwUserLogin As BusinessObject.dstUser.spr_User_Login_SelectRow = dtblUserLogin.Rows(0)

        ''Get ID from query string
        Page.Response.Cache.SetCacheability(HttpCacheability.NoCache)

        Bootstrap_Panel1.CanNew = False
        Bootstrap_Panel1.CanSave = True
        Bootstrap_Panel1.CanDelete = False
        Bootstrap_Panel1.CanSearch = False
        Bootstrap_Panel1.CanCancel = False
        Bootstrap_Panel1.CanUp = True
        Bootstrap_Panel1.CanWizard = False
        Bootstrap_Panel1.CanConfirmRequest = False
        Bootstrap_Panel1.CanReject = False
        Bootstrap_Panel1.CanDisplay = False
        Bootstrap_Panel1.CanExcel = False
        Bootstrap_Panel1.Enable_Save_Client_Validate = True
        btnSenSMS.Attributes.Add("onclick", "return btnSenSMS_Click();")
        txtMobile.Attributes.Add("onkeypress", "return numbersonly(event, false);")
        Bootstrap_Panel1.ClearMessage()

        Dim intDraftTypeID As Integer = -1
        Dim blnToSponsor As Boolean = False
        lstSMSText.Items.Clear()


        If Page.IsPostBack = False Then


            odsVoiceRecordsList.SelectParameters.Item("Action").DefaultValue = 1
            odsVoiceRecordsList.SelectParameters.Item("FK_UserID").DefaultValue = drwUserLogin.ID
            odsVoiceRecordsList.SelectParameters.Item("ID").DefaultValue = -1
            odsVoiceRecordsList.SelectParameters.Item("Name").DefaultValue = ""
            odsVoiceRecordsList.SelectParameters.Item("RecordID").DefaultValue = -1

            odsVoiceRecordsList.DataBind()

            cmbVoiceRecords.DataBind()

            If Session("intEditHadiWarningIntervalsID") Is Nothing Then
                Response.Redirect("../HadiWarningIntervals/HadiWarningIntervalsManagement.aspx")
                Return
            End If


            Dim intWarningIntervalsID As Integer = Session("intEditHadiWarningIntervalsID")


            Dim tadpwarning As New BusinessObject.dstHadiWarningIntervalsTableAdapters.spr_HadiWarningIntervals_SelectTableAdapter
            Dim dtblWarning As BusinessObject.dstHadiWarningIntervals.spr_HadiWarningIntervals_SelectDataTable = Nothing
            dtblWarning = tadpwarning.GetData(intWarningIntervalsID)


            lblTitle.Text = "(" & " گردش کار " & dtblWarning.First.WarniningTitle & ")"



            Dim tadpDraft As New BusinessObject.dstHadiDraftTableAdapters.spr_HadiDraftText_SelectTableAdapter
            Dim dtblDraft As BusinessObject.dstHadiDraft.spr_HadiDraftText_SelectDataTable = Nothing

            dtblDraft = tadpDraft.GetData(3, -1, intWarningIntervalsID)

            Dim tadpVoiceRecord As New BusinessObject.dstZamanakTableAdapters.spr_Record_List_SelectTableAdapter
            Dim dtblVoiceRecord As BusinessObject.dstZamanak.spr_Record_List_SelectDataTable = Nothing


            Dim strSampleMessage As String = ""

            For Each drwDraft As BusinessObject.dstHadiDraft.spr_HadiDraftText_SelectRow In dtblDraft
                Dim lstItem As New ListItem

                If drwDraft.IsDynamic Then
                    Select Case drwDraft.DraftText

                        Case "1"
                            lstItem.Text = "(شماره تسهیلات)"
                            strSampleMessage &= " " & "578956-5653"
                        Case "2"
                            lstItem.Text = "(تعداد روز دریافت وام)"
                            strSampleMessage &= " " & "23"
                    End Select
                    lstItem.Value = drwDraft.DraftText
                Else

                    lstItem.Value = drwDraft.FK_VoiceRecordID

                    dtblVoiceRecord = tadpVoiceRecord.GetData(2, -1, drwDraft.FK_VoiceRecordID, "", -1)
                    lstItem.Text = dtblVoiceRecord.First.Name
                    strSampleMessage &= dtblVoiceRecord.First.Name & " " & drwDraft.DraftText
                End If


                lstSMSText.Items.Add(lstItem)
            Next
            strSampleMessage = strSampleMessage.Trim
            div_Msg.InnerText = strSampleMessage

            Dim intSMSCount As Integer = Math.Ceiling(strSampleMessage.Length / 68)
            lblSMSCounter.Text = "(" & intSMSCount.ToString & ")"


            lblCharchterCounter.Text = 68 * intSMSCount - strSampleMessage.Length
            If intSMSCount > 1 Then
                lblSMSCounter.ForeColor = Drawing.Color.Red
            End If


        End If


        lstSMSText.Attributes.Add("onclick", "return lstSMSText_Click();")

    End Sub



    Private Sub Bootstrap_Panel1_Panel_Save_Click(sender As Object, e As EventArgs) Handles Bootstrap_Panel1.Panel_Save_Click


        Dim intWarningIntervalsID As Integer = Session("intEditHadiWarningIntervalsID")

        Dim qryDraft As New BusinessObject.dstHadiDraftTableAdapters.QueriesTableAdapter
        qryDraft.spr_HadiDraftText_Delete(intWarningIntervalsID)


        Dim strOutput As String = hdnColumnNumbers.Value

        Dim arrOutput() As String = strOutput.Split(";")

        For i As Integer = 1 To arrOutput.Length - 1
            If arrOutput(i).StartsWith("~?") = True Then
                qryDraft.spr_HadiDraftText_Insert(i, intWarningIntervalsID, arrOutput(i).Substring(2, 1), True, Nothing)
            Else
                qryDraft.spr_HadiDraftText_Insert(i, intWarningIntervalsID, "", False, CInt(arrOutput(i)))
            End If
        Next i


        Bootstrap_Panel1.ShowMessage("الگو با موفقیت ذخیره شد", False)

    End Sub

    Private Sub Bootstrap_Panel1_Panel_Up_Click(sender As Object, e As EventArgs) Handles Bootstrap_Panel1.Panel_Up_Click
        Response.Redirect("../HadiWarningIntervals/HadiWarningIntervalsManagement.aspx")
    End Sub


    Public Function SendVoiceMixedSMS(ByVal uId As Integer, ByVal token As String, ByVal name As String, ByVal tos() As Object, ByVal records() As Object, ByVal numbers() As String, ByVal sayMathod As String) As Boolean

        Try
            Dim oVoiceSMS As New VoiceSMS.RahyabVoiceSend  'ZamanakWebService.Default_Service_SoapServer_ZamanakV4Service

            Dim strMessage As String = ""
            oVoiceSMS.SendMixedVoiceSMS_SynchAsync("vesal", "matchautoreplay123", uId, token, name, tos, records, numbers, sayMathod, strMessage)
            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function


    Protected Sub btnSenSMS_Click(sender As Object, e As EventArgs) Handles btnSenSMS.Click

        Dim tadpSystemSetting As New BusinessObject.dstSystemSettingTableAdapters.spr_SystemSetting_SelectTableAdapter
        Dim dtblSystemSetting As BusinessObject.dstSystemSetting.spr_SystemSetting_SelectDataTable = Nothing

        dtblSystemSetting = tadpSystemSetting.GetData()
        Dim drwSystemSetting As BusinessObject.dstSystemSetting.spr_SystemSetting_SelectRow = dtblSystemSetting.Rows(0)

        Dim arrTo(0) As String
        arrTo(0) = txtMobile.Text

        Dim arrRecords() As Object = Nothing
        Dim arrNumbers(0) As String



        Dim tadpVoiceRecord As New BusinessObject.dstZamanakTableAdapters.spr_Record_List_SelectTableAdapter
        Dim dtblVoiceRecord As BusinessObject.dstZamanak.spr_Record_List_SelectDataTable = Nothing


        If hdnColumnNumbers.Value <> "" Then
            Dim strOutput As String = hdnColumnNumbers.Value

            Dim arrOutput() As String = strOutput.Split(";")

            Dim j As Integer = 0
            For i As Integer = 1 To arrOutput.Length - 1
                If arrOutput(i).StartsWith("~?") <> True Then
                    dtblVoiceRecord = tadpVoiceRecord.GetData(2, -1, CInt(arrOutput(i)), "", -1)
                    ReDim Preserve arrRecords(j)
                    arrRecords(j) = dtblVoiceRecord.First.RecordID 'arrOutput(i)
                    j += 1
                End If
            Next i

            Dim rnd As New Random

            For k As Integer = 0 To arrRecords.Length - 2
                Dim intTemp As Integer = Math.Floor(rnd.Next(10, 100))
                arrNumbers(0) &= "," & intTemp
            Next k

            arrNumbers(0) = arrNumbers(0).Substring(1)

            If SendVoiceMixedSMS(drwSystemSetting.VoiceSMSUID, drwSystemSetting.VoiceSMSToken, "VoiceSMS_Test", arrTo, arrRecords, arrNumbers, "9") = True Then

                Bootstrap_Panel1.ShowMessage("ارسال پیامک صوتی تستی با موفقیت انجام شد", False)

            Else

                Bootstrap_Panel1.ShowMessage("در ارسال پیامک صوتی تستی خطا رخ داده است", True)

            End If


        Else

            Bootstrap_Panel1.ShowMessage("در ارسال پیامک صوتی تستی خطا رخ داده است", True)

        End If

    End Sub
End Class