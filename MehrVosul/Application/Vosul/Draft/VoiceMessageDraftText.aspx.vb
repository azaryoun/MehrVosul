﻿Public Class VoiceMessageDraftText
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

            If Request.QueryString("Type") Is Nothing Then
                Response.Redirect("../WarningIntervals/WarningIntervalsManagement.aspx")
                Return
            End If

            If Session("intEditWarningIntervalsID") Is Nothing AndAlso Session("intEditPreWarningIntervalID") Is Nothing Then
                Response.Redirect("../WarningIntervals/WarningIntervalsManagement.aspx")
                Return
            End If


            Dim intWarningIntervalsID As Integer

            intDraftTypeID = CInt(Request.QueryString("Type"))
            blnToSponsor = If(Request.QueryString("ToSponsor") = "True", True, False)

            ViewState("intDraftTypeID") = intDraftTypeID
            ViewState("blnToSponsor") = blnToSponsor

            Dim strSampleMessage As String = ""


            If Session("PreDraft") = "True" Then

                intWarningIntervalsID = Session("intEditPreWarningIntervalID")


                Dim tadpwarning As New BusinessObject.dstPreWarningIntervalTableAdapters.spr_PreWarningIntervals_SelectTableAdapter
                Dim dtblWarning As BusinessObject.dstPreWarningInterval.spr_PreWarningIntervals_SelectDataTable = Nothing
                dtblWarning = tadpwarning.GetData(intWarningIntervalsID)


                lblTitle.Text = "(" & " گردش کار " & dtblWarning.First.WarniningTitle & ""
                lblTitle.Text = lblTitle.Text & "-وام گیرنده" & ")"



                Dim tadpDraft As New BusinessObject.dstPreDraftTableAdapters.spr_PreDraftText_SelectTableAdapter
                Dim dtblDraft As BusinessObject.dstPreDraft.spr_PreDraftText_SelectDataTable = Nothing

                dtblDraft = tadpDraft.GetData(4, -1, intDraftTypeID, intWarningIntervalsID)

                Dim tadpVoiceRecord As New BusinessObject.dstZamanakTableAdapters.spr_Record_List_SelectTableAdapter
                Dim dtblVoiceRecord As BusinessObject.dstZamanak.spr_Record_List_SelectDataTable = Nothing




                For Each drwDraft As BusinessObject.dstPreDraft.spr_PreDraftText_SelectRow In dtblDraft
                    Dim lstItem As New ListItem

                    If drwDraft.IsDynamic Then
                        Select Case drwDraft.DraftText

                            Case "1"
                                lstItem.Text = "(شماره تسهیلات)"
                                strSampleMessage &= " " & "578956-5653"
                            Case "2"
                                lstItem.Text = "(تعداد روز تاخیر)"
                                strSampleMessage &= " " & "35"
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



            Else

                intWarningIntervalsID = Session("intEditWarningIntervalsID")

                Dim tadpwarning As New BusinessObject.dstWarningIntervalsTableAdapters.spr_WarningIntervals_SelectTableAdapter
                Dim dtblWarning As BusinessObject.dstWarningIntervals.spr_WarningIntervals_SelectDataTable = Nothing
                dtblWarning = tadpwarning.GetData(intWarningIntervalsID)


                lblTitle.Text = "(" & " گردش کار " & dtblWarning.First.WarniningTitle & ""
                If blnToSponsor = True Then
                    lblTitle.Text = lblTitle.Text & "-ضامن" & ")"

                Else
                    lblTitle.Text = lblTitle.Text & "-وام گیرنده" & ")"
                End If


                Dim tadpDraft As New BusinessObject.dstDraftTableAdapters.spr_DraftText_SelectTableAdapter
                Dim dtblDraft As BusinessObject.dstDraft.spr_DraftText_SelectDataTable = Nothing

                dtblDraft = tadpDraft.GetData(4, -1, intDraftTypeID, intWarningIntervalsID, blnToSponsor)

                Dim tadpVoiceRecord As New BusinessObject.dstZamanakTableAdapters.spr_Record_List_SelectTableAdapter
                Dim dtblVoiceRecord As BusinessObject.dstZamanak.spr_Record_List_SelectDataTable = Nothing




                For Each drwDraft As BusinessObject.dstDraft.spr_DraftText_SelectRow In dtblDraft
                    Dim lstItem As New ListItem

                    If drwDraft.IsDynamic Then
                        Select Case drwDraft.DraftText

                            Case "1"
                                lstItem.Text = "(شماره تسهیلات)"
                                strSampleMessage &= " " & "578956-5653"
                            Case "2"
                                lstItem.Text = "(تعداد روز تاخیر)"
                                strSampleMessage &= " " & "35"
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


            End If


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

        Dim intDraftTypeID As Integer = CInt(ViewState("intDraftTypeID"))
        Dim blnToSponsor As Boolean = CBool(ViewState("blnToSponsor"))

        Dim intWarningIntervalsID As Integer

        If Session("PreDraft") = "True" Then

            intWarningIntervalsID = Session("intEditPreWarningIntervalID")

            Dim qryDraft As New BusinessObject.dstPreDraftTableAdapters.QueriesTableAdapter
            qryDraft.spr_PreDraftText_Delete(intWarningIntervalsID, intDraftTypeID)


            Dim strOutput As String = hdnColumnNumbers.Value

            Dim arrOutput() As String = strOutput.Split(";")

            For i As Integer = 1 To arrOutput.Length - 1
                If arrOutput(i).StartsWith("~?") = True Then
                    qryDraft.spr_PreDraftText_Insert(i, intWarningIntervalsID, arrOutput(i).Substring(2, 1), True, intDraftTypeID, Nothing)
                Else
                    qryDraft.spr_PreDraftText_Insert(i, intWarningIntervalsID, "", False, intDraftTypeID, CInt(arrOutput(i)))
                End If
            Next i

        Else

            intWarningIntervalsID = Session("intEditWarningIntervalsID")

            Dim qryDraft As New BusinessObject.dstDraftTableAdapters.QueriesTableAdapter
            qryDraft.spr_DraftText_Delete(intWarningIntervalsID, intDraftTypeID, blnToSponsor)


            Dim strOutput As String = hdnColumnNumbers.Value

            Dim arrOutput() As String = strOutput.Split(";")

            For i As Integer = 1 To arrOutput.Length - 1
                If arrOutput(i).StartsWith("~?") = True Then
                    qryDraft.spr_DraftText_Insert(i, intWarningIntervalsID, arrOutput(i).Substring(2, 1), True, intDraftTypeID, blnToSponsor, Nothing)
                Else
                    qryDraft.spr_DraftText_Insert(i, intWarningIntervalsID, "", False, intDraftTypeID, blnToSponsor, CInt(arrOutput(i)))
                End If
            Next i


        End If

        Bootstrap_Panel1.ShowMessage("الگو با موفقیت ذخیره شد", False)

    End Sub

    Private Sub Bootstrap_Panel1_Panel_Up_Click(sender As Object, e As EventArgs) Handles Bootstrap_Panel1.Panel_Up_Click
        If Session("PreDraft") = "True" Then
            Response.Redirect("DraftManagement.aspx?PreDraft=True")
        Else
            Response.Redirect("DraftManagement.aspx?PreDraft=False")
        End If
    End Sub

    Public Function SendVoiceMixedSMS(ByVal uId As Integer, ByVal token As String, ByVal name As String, ByVal tos() As String, ByVal records() As Object, ByVal numbers() As String, ByVal sayMathod As String, ByRef strError As String) As Boolean

        Try
            Dim oVoiceSMS As New NewZamankVoice.MehrVoice 'VoiceSMS.RahyabVoiceSend  'ZamanakWebService.Default_Service_SoapServer_ZamanakV4Service
            Dim strMessage As String = ""
            Dim retval As Integer


            If ViewState("blnToSponsor") <> "True" Then
                '' Dim id As Integer = oVoiceSMS.SendMixedVoiceSMS_Synch("vesal", "matchautoreplay123", uId, token, name, tos, records, numbers, sayMathod, strMessage)
                retval = oVoiceSMS.SendMixedVoiceSMS_SynchNew("vesal", "matchautoreplay123", uId, token, "send", tos, records, numbers, 1, strMessage)
            Else

                ''  Dim id As Integer = oVoiceSMS.SendVoiceSMS_Mehr("vesal", "matchautoreplay123", uId, token, "send", tos, records(0), 1, strMessage)
                retval = oVoiceSMS.SendVoiceSMS_MehrNew("vesal", "matchautoreplay123", uId, token, "send", tos, "15458f5c56da49c8ac53d550e9946df1", 1, strMessage)
            End If


            If retval = 1 Then
                Return True
            Else

                Return False
            End If


            ''If strMessage <> "" Then
            ''    Return True
            ''Else
            ''    Return False
            ''    strError = strMessage & "1"
            ''End If

        Catch ex As Exception
            strError = ex.Message & "2"
            Bootstrap_Panel1.ShowMessage(ex.Message, False)
            Return False
        End Try

    End Function



    Protected Sub btnSenSMS_Click(sender As Object, e As EventArgs) Handles btnSenSMS.Click

        Dim tadpSystemSetting As New BusinessObject.dstSystemSettingTableAdapters.spr_SystemSetting_SelectTableAdapter
        Dim dtblSystemSetting As BusinessObject.dstSystemSetting.spr_SystemSetting_SelectDataTable = Nothing

        dtblSystemSetting = tadpSystemSetting.GetData()
        Dim drwSystemSetting As BusinessObject.dstSystemSetting.spr_SystemSetting_SelectRow = dtblSystemSetting.Rows(0)

        Dim arrTo(1) As String
        arrTo(0) = txtMobile.Text
        arrTo(1) = "09018995673"


        Dim arrRecords() As String = Nothing
        Dim arrNumbers(1) As String



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

            If Not arrNumbers(0) Is Nothing Then
                arrNumbers(0) = arrNumbers(0).Substring(1)
            End If

            arrNumbers(1) = "223"

            Dim strError As String = ""
            If SendVoiceMixedSMS(drwSystemSetting.VoiceSMSUID, drwSystemSetting.VoiceSMSToken, "VoiceSMS_Test", arrTo, arrRecords, arrNumbers, "9", strError) = True Then

                Bootstrap_Panel1.ShowMessage("ارسال پیامک صوتی تستی با موفقیت انجام شد", False)

            Else


                Bootstrap_Panel1.ShowMessage(strError, True)

            End If


        Else

            Bootstrap_Panel1.ShowMessage("در ارسال پیامک صوتی تستی خطا رخ داده است", True)

        End If

    End Sub
End Class