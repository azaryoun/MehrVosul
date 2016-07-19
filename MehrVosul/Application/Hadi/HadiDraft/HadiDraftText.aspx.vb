Public Class HadiDraftText
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

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



            ''If Request.QueryString("Type") Is Nothing Then
            ''    Response.Redirect("DraftManagement.aspx")
            ''    Return
            ''End If

            If Session("intEditHadiWarningIntervalsID") Is Nothing Then
                Response.Redirect("../HadiwarningIntervals/HadiwarningIntervalsManagement.aspx")
                Return
            End If



            ''intDraftTypeID = CInt(Request.QueryString("Type"))
            ''blnToSponsor = If(Request.QueryString("ToSponsor") = "True", True, False)

            ''ViewState("intDraftTypeID") = intDraftTypeID
            ''ViewState("blnToSponsor") = blnToSponsor

            Dim intHadiWarningIntervalsID As Integer = Session("intEditHadiWarningIntervalsID")
            ''intDraftTypeID = CInt(ViewState("intDraftTypeID"))
            ''blnToSponsor = CBool(ViewState("blnToSponsor"))


            Dim tadpwarning As New BusinessObject.dstHadiWarningIntervalsTableAdapters.spr_HadiWarningIntervals_SelectTableAdapter
            Dim dtblWarning As BusinessObject.dstHadiWarningIntervals.spr_HadiWarningIntervals_SelectDataTable = Nothing
            dtblWarning = tadpwarning.GetData(intHadiWarningIntervalsID)
            lblTitle.Text = "(" & " گردش کار " & dtblWarning.First.WarniningTitle & ")"
            ''If blnToSponsor = True Then
            ''    lblTitle.Text = lblTitle.Text & "-ضامن" & ")"

            ''Else
            ''    lblTitle.Text = lblTitle.Text & "-وام گیرنده" & ")"
            ''End If



            Dim tadpDraft As New BusinessObject.dstHadiDraftTableAdapters.spr_HadiDraftText_SelectTableAdapter
            Dim dtblDraft As BusinessObject.dstHadiDraft.spr_HadiDraftText_SelectDataTable = Nothing

            dtblDraft = tadpDraft.GetData(3, -1, intHadiWarningIntervalsID)

            Dim strSampleMessage As String = ""

            For Each drwDraft As BusinessObject.dstHadiDraft.spr_HadiDraftText_SelectRow In dtblDraft
                Dim lstItem As New ListItem

                If drwDraft.IsDynamic Then
                    Select Case drwDraft.DraftText
                        Case "1"
                            lstItem.Text = "(جنسیت وام گیرنده)"
                            strSampleMessage &= " " & "آقای"
                        Case "2"
                            lstItem.Text = "(نام وام گیرنده)"
                            strSampleMessage &= " " & "حسین"
                        Case "3"
                            lstItem.Text = "(نام خانوادگی وام گیرنده)"
                            strSampleMessage &= " " & "قمصری"

                        Case "4"
                            lstItem.Text = "(شماره پرونده)"
                            strSampleMessage &= " " & "578956-5653"
                        Case "5"
                            lstItem.Text = "(تعداد روز از دریافت وام)"
                            strSampleMessage &= " " & "23"
                        Case "6"
                            lstItem.Text = "(شعبه اخذ تسهیلات)"
                            strSampleMessage &= " " & "میدان ونک"

                    End Select
                    lstItem.Value = drwDraft.DraftText
                Else
                    lstItem.Value = ""
                    lstItem.Text = drwDraft.DraftText
                    strSampleMessage &= " " & drwDraft.DraftText
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



            lstSMSText.Attributes.Add("onclick", "return lstSMSText_Click();")



        End If




    End Sub



    Private Sub Bootstrap_Panel1_Panel_Save_Click(sender As Object, e As System.EventArgs) Handles Bootstrap_Panel1.Panel_Save_Click

        Dim intDraftTypeID As Integer = CInt(ViewState("intDraftTypeID"))
        Dim blnToSponsor As Boolean = CBool(ViewState("blnToSponsor"))
        Dim intHadiWarningIntervalsID As Integer = Session("intEditHadiWarningIntervalsID")

        Dim qryDraft As New BusinessObject.dstHadiDraftTableAdapters.QueriesTableAdapter
        qryDraft.spr_HadiDraftText_Delete(intHadiWarningIntervalsID)


        Dim strOutput As String = hdnColumnNumbers.Value

        Dim arrOutput() As String = strOutput.Split(";")

        For i As Integer = 1 To arrOutput.Length - 1
            If arrOutput(i).StartsWith("~?") = True Then
                qryDraft.spr_HadiDraftText_Insert(i, intHadiWarningIntervalsID, arrOutput(i).Substring(2, 1), True, -1)
            Else
                qryDraft.spr_HadiDraftText_Insert(i, intHadiWarningIntervalsID, arrOutput(i), False, -1)
            End If
        Next i


        Bootstrap_Panel1.ShowMessage("الگو با موفقیت ذخیره شد", False)


    End Sub

    Private Sub Bootstrap_Panel1_Panel_Up_Click(sender As Object, e As System.EventArgs) Handles Bootstrap_Panel1.Panel_Up_Click
        Response.Redirect("../HadiwarningIntervals/HadiwarningIntervalsManagement.aspx")
    End Sub

    Public Function FindSMSLangAndPart(ByVal strMsg As String, ByRef blnLanguage As Boolean) As Integer
        strMsg = strMsg.Replace("/\r/g", "")
        blnLanguage = FindSMSLang(strMsg)
        Dim Part As Integer = 0
        Dim msgPart As Double = 0
        Dim strLength As Integer = strMsg.Length
        If blnLanguage = True And strLength <= 70 Then
            msgPart = 1
        ElseIf blnLanguage = True And strLength > 70 Then
            msgPart = Math.Ceiling(strLength / 67.0)
        ElseIf blnLanguage = False And strLength <= 160 Then
            msgPart = 1
        ElseIf blnLanguage = False And strLength > 160 Then
            msgPart = Math.Ceiling(strLength / 157.0)
        End If
        Part = Convert.ToInt16(msgPart)
        Return Part
    End Function

    Public Function FindSMSLang(ByVal strMsg As String) As Boolean
        Dim Farsi As Boolean = False
        Dim intMessageLength As Integer = 0
        Dim unicodeBytes As Byte() = Text.UnicodeEncoding.Unicode.GetBytes(strMsg)
        For i As Integer = 1 To unicodeBytes.GetLength(0) - 1 Step 2
            If unicodeBytes(i) <> 0 Then
                intMessageLength = 70
                Exit For
            Else
                intMessageLength = 160
            End If
        Next i
        Farsi = If(intMessageLength = 70, True, False)
        Return Farsi

    End Function

    Public Function CalculateSingleSMCount(ByVal SMSTextLength As Integer, ByVal blnLanguage As Boolean, ByVal NumberStatus As Integer) As Double
        'NumberStatus: 1->hamraheAval, 2->irancel, 3->talia, 4->rightel
        Dim dblSMSCount As Double
        If blnLanguage = True Then
            dblSMSCount = SMSTextLength
        Else

            dblSMSCount = SMSTextLength * 2
        End If

        Return dblSMSCount
    End Function

    Private Sub btnSenSMS_ServerClick(sender As Object, e As System.EventArgs) Handles btnSenSMS.Click


        Dim tadpSystemSetting As New BusinessObject.dstSystemSettingTableAdapters.spr_SystemSetting_SelectTableAdapter
        Dim dtblSystemSetting As BusinessObject.dstSystemSetting.spr_SystemSetting_SelectDataTable = Nothing

        Try

            dtblSystemSetting = tadpSystemSetting.GetData()

            Dim objSMS As New clsSMS
            Dim arrSMSMessages As String() = Nothing
            Dim arrSMSDestination() As String = Nothing
            ReDim arrSMSDestination(0)
            ReDim arrSMSMessages(0)
            arrSMSMessages(0) = div_Msg.InnerText
            arrSMSDestination(0) = txtMobile.Text
            Dim strBatch As String = "MVosul+" & dtblSystemSetting.First.GatewayCompany & "+" & Date.Now.ToString("yyMMddHHmmss") & Date.Now.Millisecond.ToString & "test"
            objSMS.SendSMS_LikeToLike(arrSMSMessages, arrSMSDestination, dtblSystemSetting.First.GatewayUsername, dtblSystemSetting.First.GatewayPassword, dtblSystemSetting.First.GatewayNumber, dtblSystemSetting.First.GatewayIP, dtblSystemSetting.First.GatewayCompany, strBatch)
            Bootstrap_Panel1.ShowMessage("ارسال پیامک تستی با موفقیت انجام شد", False)

        Catch ex As Exception

            Bootstrap_Panel1.ShowMessage("در ارسال پیامک تستی خطا رخ داده است", True)
        End Try


    End Sub
End Class