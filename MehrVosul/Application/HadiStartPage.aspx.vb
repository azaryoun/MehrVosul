﻿Public Class HadiStartPage
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load


        Dim dtblUserLogin As BusinessObject.dstUser.spr_User_Login_SelectDataTable = CType(HttpContext.Current.Session("dtblUserLogin"), BusinessObject.dstUser.spr_User_Login_SelectDataTable)
        Dim drwUserLogin As BusinessObject.dstUser.spr_User_Login_SelectRow = dtblUserLogin.Rows(0)

        Dim tadpUserRole As New BusinessObject.dstUserTableAdapters.spr_UserRole_SelectTableAdapter
        Dim dtblUserRole As BusinessObject.dstUser.spr_UserRole_SelectDataTable = Nothing



        Dim tadpBranch As New BusinessObject.dstBranchTableAdapters.spr_Branch_SelectTableAdapter
        Dim dtblBranch As BusinessObject.dstBranch.spr_Branch_SelectDataTable = Nothing

        ''check the access Group id
        ''Dim tadpAccessgroupUser As New BusinessObject.dstAccessgroupUserTableAdapters.spr_AccessgroupUserByID_SelectTableAdapter
        ''Dim dtblAccessgroupUser As BusinessObject.dstAccessgroupUser.spr_AccessgroupUserByID_SelectDataTable = Nothing

        ''dtblAccessgroupUser = tadpAccessgroupUser.GetData(drwUserLogin.ID, 3432)
        ''If dtblAccessgroupUser.Rows.Count = 0 Then

        ''    Response.Redirect("../HadiLogin.aspx")
        ''End If


        Dim tadpSetting As New BusinessObject.dstSystemSettingTableAdapters.spr_SystemSetting_SelectTableAdapter
        Dim dtblSetting As BusinessObject.dstSystemSetting.spr_SystemSetting_SelectDataTable = Nothing

        dtblSetting = tadpSetting.GetData()

        Dim tadpLogCurrentLCStatus As New BusinessObject.dstHadiLogLoanStatusTableAdapters.spr_HadiLogLoanStatus_ForDate_SelectTableAdapter
        Dim dtblLogCurrentLCStatus As BusinessObject.dstHadiLogLoanStatus.spr_HadiLogLoanStatus_ForDate_SelectDataTable
        Dim blnSuccess As Boolean
        Dim strSMSMessage As String = GetSMSMessage(blnSuccess)

        If blnSuccess = True Then

            lblSMSError.Visible = False
            lblSMSSuccess.Visible = True
            divSMSError.Visible = False
            divSMSSuccess.Visible = True
            lblSMSSuccess.Text = strSMSMessage

        Else

            lblSMSError.Visible = True
            lblSMSSuccess.Visible = False
            divSMSError.Visible = True
            divSMSSuccess.Visible = False
            lblSMSError.Text = strSMSMessage

        End If


        If Date.Now.Hour >= dtblSetting.First.UpdateTime_Loan.Hours Then

            Dim dteThisDate As Date = Date.Now
            dtblLogCurrentLCStatus = tadpLogCurrentLCStatus.GetData(dteThisDate)

            If dtblLogCurrentLCStatus.Rows.Count > 0 Then

                If dtblLogCurrentLCStatus.First.Success = True Then
                    divLogSuccess.Visible = True
                    divLogError.Visible = False

                    ' lblLogSuccess.Text = " دریافت اطلاعات با موفقیت انجام شده است"
                    lblLogSuccess.Text = "دریافت اطلاعات مربوط به مورخ " & mdlGeneral.GetPersianDate(Date.Now) & " با موفقیت انجام شده است."


                Else
                    divLogSuccess.Visible = False
                    divLogError.Visible = True

                    '    lblLogError.ForeColor = Drawing.Color.Red
                    lblLogError.Text = "' style='color:#a94442' >" & "دریافت اطلاعات با خطا مواجه شده است: " & If(dtblLogCurrentLCStatus.First.IsRemarksNull = False, dtblLogCurrentLCStatus.First.Remarks, "") & " " & "</a><a href='#' class='fa fa-history fa-2x' style='color:#31708f' title='خواندن دستی اطلاعات' onclick='return ReadData();'></a>"


                End If

            Else
                divLogSuccess.Visible = False
                divLogError.Visible = True
                '     lblLog.ForeColor = Drawing.Color.Red
                lblLogError.Text = "" & "دریافت اطلاعات مربوط به مورخ " & mdlGeneral.GetPersianDate(Date.Now) & " انجام نشده است،" & " لطفا با مدیر سیستم تماس بگیرید." & " " & "</a><a href='#' class='fa fa-history fa-2x' style='color:#31708f' title='خواندن دستی اطلاعات' onclick='return ReadData();'></a>"

            End If

        Else

            dtblLogCurrentLCStatus = tadpLogCurrentLCStatus.GetData(Date.Now.AddDays(-1))
            If dtblLogCurrentLCStatus.Rows.Count > 0 Then

                If dtblLogCurrentLCStatus.First.Success = True Then
                    divLogSuccess.Visible = True
                    divLogError.Visible = False

                    lblLogSuccess.Text = "دریافت اطلاعات مربوط به مورخ " & mdlGeneral.GetPersianDate(Date.Now.AddDays(-1)) & " با موفقیت انجام شده است."


                Else
                    divLogSuccess.Visible = False
                    divLogError.Visible = True
                    '      lblLog.ForeColor = Drawing.Color.Red
                    lblLogError.Text = " style='color:#a94442' >" & "دریافت اطلاعات با خطا مواجه شده است: " & If(dtblLogCurrentLCStatus.First.IsRemarksNull = False, dtblLogCurrentLCStatus.First.Remarks, "") & " " & "</a><a href='#' class='fa fa-history fa-2x' style='color:#31708f' title='خواندن دستی اطلاعات' onclick='return ReadData();'></a>"


                End If

            Else
                divLogSuccess.Visible = False
                divLogError.Visible = True
                '     lblLog.ForeColor = Drawing.Color.Red
                lblLogError.Text = " style='color:#a94442' >" & "دریافت اطلاعات مربوط به مورخ " & mdlGeneral.GetPersianDate(Date.Now.AddDays(-1)) & " انجام نشده است،" & " لطفا با مدیر سیستم تماس بگیرید." & " " & "</a><a href='#' class='fa fa-history fa-2x' style='color:#31708f' title='خواندن دستی اطلاعات' onclick='return ReadData();'></a>"



            End If

        End If

        ''Get last day Log Status Detailes


        dtblLogCurrentLCStatus = tadpLogCurrentLCStatus.GetData(Date.Now.AddDays(-1))

        If dtblLogCurrentLCStatus.Rows.Count = 0 Then

            lblLastDayStatus.Text = " برای مورخ " & mdlGeneral.GetPersianDate(Date.Now.AddDays(-1)) & " لاگ موجود نیست سرویس را ریست کنید" & ControlChars.NewLine & "code:0"


        Else

            Dim drwLCLog As BusinessObject.dstHadiLogLoanStatus.spr_HadiLogLoanStatus_ForDate_SelectRow = dtblLogCurrentLCStatus.Rows(0)
            If drwLCLog.Success = False Then

                lblLastDayStatus.Text = " برای مورخ " & mdlGeneral.GetPersianDate(Date.Now.AddDays(-1)) & "خواندن اطلاعات با خطا همراه شده است  مشکل در BI است " & drwLCLog.Remarks & ControlChars.NewLine & "code:1"
            Else

                lblLastDayStatus.Text = "OK"

                Dim tadpSMSCountLog As New BusinessObject.dstHadiWarningNotificationLogDetailTableAdapters.spr_HadiSMSCountLog_SelectTableAdapter
                Dim dtblSMSCountLog As BusinessObject.dstHadiWarningNotificationLogDetail.spr_HadiSMSCountLog_SelectDataTable = Nothing

                dtblSMSCountLog = tadpSMSCountLog.GetData(Date.Now.AddDays(-1).Date)

                If dtblSMSCountLog.Rows.Count <> 0 Then

                    Dim drwSMSCount As BusinessObject.dstHadiWarningNotificationLogDetail.spr_HadiSMSCountLog_SelectRow = dtblSMSCountLog.Rows(0)

                    Dim tmSpan As TimeSpan = drwSMSCount.LastSent.Subtract(drwSMSCount.FirstSent)

                    lblLastDayBI.Text = drwLCLog.STime.ToString("HH:mm")
                    lblLastDayFirstSent.Text = drwSMSCount.FirstSent.ToString("HH:mm")
                    lblLastDayLastSent.Text = drwSMSCount.LastSent.ToString("HH:mm")
                    lblLastDaySendTime.Text = Math.Floor(tmSpan.TotalHours) & "h" & Math.Floor(tmSpan.Minutes) & "m"
                    lblLastDaySMSCount.Text = drwSMSCount.SMSCount.ToString("n0")
                    lblBILastDayCount.Text = drwSMSCount.BITotal.ToString("n0")
                    lblLastDaySMSVoice.Text = drwSMSCount.SMSVoice.ToString("n0")
                    ''   lblLastDayPreSMS.Text = If(drwSMSCount.IsPreNotifySMSNull <> True, drwSMSCount.PreNotifySMS.ToString("n0"), "0")

                End If

            End If

        End If

        dtblLogCurrentLCStatus = tadpLogCurrentLCStatus.GetData(Date.Now.Date)

        If dtblLogCurrentLCStatus.Rows.Count = 0 Then

            lblTodayStatus.Text = " برای مورخ " & mdlGeneral.GetPersianDate(Date.Now) & " لاگ موجود نیست سرویس را ریست کنید" & ControlChars.NewLine & "code:0"
        Else

            Dim drwLCLog As BusinessObject.dstHadiLogLoanStatus.spr_HadiLogLoanStatus_ForDate_SelectRow = dtblLogCurrentLCStatus.Rows(0)
            If drwLCLog.Success = False Then

                lblTodayStatus.Text = " برای مورخ " & mdlGeneral.GetPersianDate(Date.Now) & "خواندن اطلاعات با خطا همراه شده است  مشکل در BI است " & drwLCLog.Remarks & ControlChars.NewLine & "code:1"

            Else

                lblTodayStatus.Text = "OK"
                Dim tadpSMSCountLog As New BusinessObject.dstHadiWarningNotificationLogDetailTableAdapters.spr_HadiSMSCountLog_SelectTableAdapter
                Dim dtblSMSCountLog As BusinessObject.dstHadiWarningNotificationLogDetail.spr_HadiSMSCountLog_SelectDataTable = Nothing

                dtblSMSCountLog = tadpSMSCountLog.GetData(Date.Now.Date)

                If dtblSMSCountLog.Rows.Count <> 0 Then

                    Dim drwSMSCount As BusinessObject.dstHadiWarningNotificationLogDetail.spr_HadiSMSCountLog_SelectRow = dtblSMSCountLog.Rows(0)
                    'Dim tmSpan As TimeSpan = drwWarningNotificationLogDetailFirstLastLog.Last.Subtract(drwWarningNotificationLogDetailFirstLastLog.First)
                    Dim tmSpan As TimeSpan = drwSMSCount.LastSent.Subtract(drwSMSCount.FirstSent)

                    lblTodayBI.Text = drwLCLog.STime.ToString("HH:mm")
                    lblTodayFirstSent.Text = drwSMSCount.FirstSent.ToString("HH:mm")
                    lblTodayLastSent.Text = drwSMSCount.LastSent.ToString("HH:mm")
                    lblTodaySendTime.Text = Math.Floor(tmSpan.TotalHours) & "h" & Math.Floor(tmSpan.Minutes) & "m"
                    lblTodaySMSCount.Text = drwSMSCount.SMSCount.ToString("n0")
                    lblBITodayCount.Text = drwSMSCount.BITotal.ToString("n0")
                    lblTodaySMSVoice.Text = drwSMSCount.SMSVoice.ToString("n0")
                    ''   lblTodayPreSMS.Text = If(drwSMSCount.IsPreNotifySMSNull <> True, drwSMSCount.PreNotifySMS.ToString("n0"), "0")

                End If


            End If

        End If




    End Sub

    Private Function GetSMSMessage(ByRef blnSuccess As Boolean) As String




        Dim strResultMessage As String = ""
        blnSuccess = False


        Dim tadpLCLog As New BusinessObject.dstHadiLogLoanStatusTableAdapters.spr_HadiLogLoanStatus_ForDate_SelectTableAdapter
        Dim dtblLCLog As BusinessObject.dstHadiLogLoanStatus.spr_HadiLogLoanStatus_ForDate_SelectDataTable = Nothing

        dtblLCLog = tadpLCLog.GetData(Date.Now.Date)


        If dtblLCLog.Rows.Count = 0 Then


            strResultMessage = "پیامکی در مورخ " & mdlGeneral.GetPersianDate(Date.Now) & " ارسال نشده است. "
            '  strResultMessage = " برای مورخ " & mdlGeneral.GetPersianDate(Date.Now.AddDays(-1)) & " لاگ موجود نیست سرویس را ریست کنید" & ControlChars.NewLine & "code:0"

        Else
            Dim drwLCLog As BusinessObject.dstHadiLogLoanStatus.spr_HadiLogLoanStatus_ForDate_SelectRow = dtblLCLog.Rows(0)
            If drwLCLog.Success = False Then

                '  strResultMessage = " برای مورخ " & mdlGeneral.GetPersianDate(Date.Now.AddDays(-1)) & "خواندن اطلاعات با خطا همراه شده است  مشکل در BI است " & drwLCLog.Remarks & ControlChars.NewLine & "code:1"
                strResultMessage = "پیامکی در مورخ " & mdlGeneral.GetPersianDate(Date.Now) & " ارسال نشده است. "

            Else

                ' strResultMessage = "اطلاعات برای مورخ " & mdlGeneral.GetPersianDate(Date.Now.AddDays(-1)) & " با موفقیت در ساعت " & drwLCLog.STime.Hour & " از BI خوانده شده است"

                Dim tadpSMSCount As New BusinessObject.dstHadiWarningNotificationLogDetailTableAdapters.spr_HadiWarningNotificationLogDetail_SMSCount_SelectTableAdapter
                Dim dtblSMSCount As BusinessObject.dstHadiWarningNotificationLogDetail.spr_HadiWarningNotificationLogDetail_SMSCount_SelectDataTable = Nothing
                dtblSMSCount = tadpSMSCount.GetData(1, Date.Now.Date)

                If dtblSMSCount.Rows.Count = 0 Then
                    ' strResultMessage &= ControlChars.NewLine & "ولی هیچ پیامکی تاکنون ارسال نشده است مشکل در سرویس ارسال" & ControlChars.NewLine & "code:2"
                    strResultMessage = "پیامکی در مورخ " & mdlGeneral.GetPersianDate(Date.Now) & " ارسال نشده است. "
                Else

                    Dim drwSMSCount As BusinessObject.dstHadiWarningNotificationLogDetail.spr_HadiWarningNotificationLogDetail_SMSCount_SelectRow = dtblSMSCount.Rows(0)
                    If drwSMSCount.IsHadiSMSCountNull = True OrElse drwSMSCount.HadiSMSCount = 0 Then
                        'strResultMessage &= ControlChars.NewLine & "ولی هیچ پیامکی تاکنون ارسال نشده است مشکل در سرویس ارسال" & ControlChars.NewLine & "code:2"
                        strResultMessage = "پیامکی در مورخ " & mdlGeneral.GetPersianDate(Date.Now) & " تا این لحظه ارسال نشده است. "

                    Else
                        strResultMessage &= " در مورخ" & mdlGeneral.GetPersianDate(Date.Now) & " تا کنون " & drwSMSCount.HadiSMSCount & " پیامک با موفقیت ارسال شده است."
                        blnSuccess = True
                    End If
                End If
            End If
        End If


        Return strResultMessage

    End Function

End Class