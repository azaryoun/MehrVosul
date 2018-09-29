Public Class StartPage
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        If Page.IsPostBack = False Then

            Dim dtblUserLogin As BusinessObject.dstUser.spr_User_Login_SelectDataTable = CType(HttpContext.Current.Session("dtblUserLogin"), BusinessObject.dstUser.spr_User_Login_SelectDataTable)
            Dim drwUserLogin As BusinessObject.dstUser.spr_User_Login_SelectRow = dtblUserLogin.Rows(0)

            Dim tadpUserRole As New BusinessObject.dstUserTableAdapters.spr_UserRole_SelectTableAdapter
            Dim dtblUserRole As BusinessObject.dstUser.spr_UserRole_SelectDataTable = Nothing


            lblUserName1.Text = drwUserLogin.Username
            Dim tadpBranch As New BusinessObject.dstBranchTableAdapters.spr_Branch_SelectTableAdapter
            Dim dtblBranch As BusinessObject.dstBranch.spr_Branch_SelectDataTable = Nothing

            

            ''get Userrole
            If drwUserLogin.IsDataAdmin = True Then

                lblUserRole.Text = "کاربر ارشد"
                divBranchAdminInfo.Visible = True

                FillDataAdminInfo(drwUserLogin.Fk_ProvinceID)

            ElseIf drwUserLogin.IsDataUserAdmin = True Then

                ''check the access Group id
                Dim tadpAccessgroupUser As New BusinessObject.dstAccessgroupUserTableAdapters.spr_AccessgroupUserByID_SelectTableAdapter
                Dim dtblAccessgroupUser As BusinessObject.dstAccessgroupUser.spr_AccessgroupUserByID_SelectDataTable = Nothing

                dtblAccessgroupUser = tadpAccessgroupUser.GetData(drwUserLogin.ID, 3432)
                If dtblAccessgroupUser.Rows.Count > 0 Then

                    Response.Redirect("../Login.aspx")
                End If

                Dim blnAdminBranch As Boolean = False
                dtblAccessgroupUser = tadpAccessgroupUser.GetData(drwUserLogin.ID, 3431)
                If dtblAccessgroupUser.Rows.Count = 0 Then
                    dtblAccessgroupUser = tadpAccessgroupUser.GetData(drwUserLogin.ID, 3436)
                    If dtblAccessgroupUser.Rows.Count > 0 Then
                        blnAdminBranch = True
                    End If
                End If

                dtblBranch = tadpBranch.GetData(drwUserLogin.FK_BrnachID)
                divBranchAdminInfo.Visible = True
                If blnAdminBranch = True Then

                    FillBranchAdminInfo(drwUserLogin.Fk_ProvinceID, dtblBranch.First.BrnachCode)
                Else
                    FillItemAdminInfo(drwUserLogin.Fk_ProvinceID)

                End If

                dtblUserRole = tadpUserRole.GetData(drwUserLogin.ID)
                Dim strUserRoles As String = ""
                If dtblUserRole.Rows.Count = 0 Then

                    lblUserRole.Text = ""
                Else
                    lblUserRole.Text = dtblUserRole.First.UserRoles
                End If


                ''Fill HandyFollow Alarm 
                Dim tadpHandyFollowAlarm As New BusinessObject.dstAlarmTableAdapters.spr_HandyFollowAlarm_Select_CountTableAdapter
                Dim dtblHandyFollowAlarm As BusinessObject.dstAlarm.spr_HandyFollowAlarm_Select_CountDataTable = Nothing

                dtblHandyFollowAlarm = tadpHandyFollowAlarm.GetData(1, -1, drwUserLogin.FK_BrnachID)
                If dtblHandyFollowAlarm.Rows.Count > 0 Then
                    lblHandyFollowAlarmAdmin.Text = dtblHandyFollowAlarm.First.HandyFollow.ToString()
                End If

                ''Fill HandyFollow Alarm 
                Dim tadpHandyFollowCheckAlarm As New BusinessObject.dstAlarmTableAdapters.spr_HandyFollowCheckAlarm_Select_CountTableAdapter
                Dim dtblHandyFollowCheckAlarm As BusinessObject.dstAlarm.spr_HandyFollowCheckAlarm_Select_CountDataTable = Nothing

                dtblHandyFollowCheckAlarm = tadpHandyFollowCheckAlarm.GetData(1, -1, drwUserLogin.FK_BrnachID)
                If dtblHandyFollowAlarm.Rows.Count > 0 Then
                    lblHandyFollowCheckAlarmAdmin.Text = dtblHandyFollowCheckAlarm.First.HandyFollow.ToString()
                End If



            End If

            If drwUserLogin.IsDataAdmin = False AndAlso drwUserLogin.IsItemAdmin = False AndAlso drwUserLogin.IsDataUserAdmin = False Then

                dtblBranch = tadpBranch.GetData(drwUserLogin.FK_BrnachID)
                divUserAdminInfo.Visible = True
                ''Fill user role 
                dtblUserRole = tadpUserRole.GetData(drwUserLogin.ID)
                lblNormalUserRole.Text = dtblUserRole.First.UserRoles
                lblNormalUserName.Text = drwUserLogin.Username

                ''Fill Notice
                Dim tadplNoticeCount As New BusinessObject.dstNoticeTableAdapters.spr_NoticeStartPageCount_SelectTableAdapter
                Dim dtblNoticeCount As BusinessObject.dstNotice.spr_NoticeStartPageCount_SelectDataTable = Nothing

                dtblNoticeCount = tadplNoticeCount.GetData(1, -1)
                lblNormalPublicNotice.Text = dtblNoticeCount.First.NoticeCount

                dtblNoticeCount = tadplNoticeCount.GetData(2, drwUserLogin.Fk_ProvinceID)
                lblNormalProvinceNotice.Text = dtblNoticeCount.First.NoticeCount


                ''Fill Assigned Files
                Dim tadpAssigneFiles As New BusinessObject.dstHandyFollowTableAdapters.spr_HandyFollowAssignByUser_Count_SelectTableAdapter
                Dim dtblAssigneFiles As BusinessObject.dstHandyFollow.spr_HandyFollowAssignByUser_Count_SelectDataTable = Nothing

                dtblAssigneFiles = tadpAssigneFiles.GetData(1, drwUserLogin.ID)
                lblAssignedFiles.Text = dtblAssigneFiles.First.AssignedFile

                dtblAssigneFiles = tadpAssigneFiles.GetData(2, drwUserLogin.ID)
                If dtblAssigneFiles.First.AssignedFile > 0 Then
                    lblDefferedFiles.Text = dtblAssigneFiles.First.AssignedFile
                Else
                    lblDefferedFiles.Text = 0
                End If


                ''check the access Group id
                Dim tadpAccessgroupUser As New BusinessObject.dstAccessgroupUserTableAdapters.spr_AccessgroupUserByID_SelectTableAdapter
                Dim dtblAccessgroupUser As BusinessObject.dstAccessgroupUser.spr_AccessgroupUserByID_SelectDataTable = Nothing

                dtblAccessgroupUser = tadpAccessgroupUser.GetData(drwUserLogin.ID, 3432)
                If dtblAccessgroupUser.Rows.Count > 0 Then
                    Response.Redirect("../Login.aspx")
                End If

                dtblAccessgroupUser = tadpAccessgroupUser.GetData(drwUserLogin.ID, 3436)
                Dim blnAdminBranch As Boolean = False

                If dtblAccessgroupUser.Rows.Count > 0 Then
                    blnAdminBranch = True
                End If

                divLogError.Visible = False
                divLogSuccess.Visible = False
                divSMSError.Visible = False
                divSMSSuccess.Visible = False
                tblLogDetaile.Visible = False
                divItemAdmin.Visible = False
                divadmin.Visible = False

                If blnAdminBranch = False Then
                    divBranchAdmin.Visible = False
                    divBranchAdmin1.Visible = False
                    divBranchUser.Visible = True
                    divBranchAdmin3.Visible = False
                    divBranchAdmin4.Visible = False
                    divItemAdmin.Visible = True

                Else
                    divBranchUser.Visible = False
                    divBranchUser1.Visible = False
                    FillBranchAdminInfo(drwUserLogin.Fk_ProvinceID, dtblBranch.First.BrnachCode)
                End If

                ''''Fill Current day related Branch Manifest
                ''get related warninginterwal
                Dim tadpWarningIntrevalManifeste As New BusinessObject.dstWarningIntervalsTableAdapters.spr_WarningIntervalsManifest_SelectTableAdapter
                Dim dtblWarningIntervalManifest As BusinessObject.dstWarningIntervals.spr_WarningIntervalsManifest_SelectDataTable = Nothing


                dtblWarningIntervalManifest = tadpWarningIntrevalManifeste.GetData(drwUserLogin.FK_BrnachID)

                If dtblWarningIntervalManifest.Rows.Count > 0 Then

                    Dim tadpTotalDeffredForAssign As New BusinessObject.dstTotalDeffredLCTableAdapters.spr_TotalDeffredLCNoticeAssign_SelectTableAdapter
                    Dim dtblTotalDeffredForAssign As BusinessObject.dstTotalDeffredLC.spr_TotalDeffredLCNoticeAssign_SelectDataTable = Nothing

                    dtblBranch = tadpBranch.GetData(drwUserLogin.FK_BrnachID)

                    dtblTotalDeffredForAssign = tadpTotalDeffredForAssign.GetData(dtblBranch.First.BrnachCode, dtblWarningIntervalManifest.First.FromDay, dtblWarningIntervalManifest.First.ToDay, drwUserLogin.FK_BrnachID)

                    Dim strchklstFiles As String = ""

                    For Each drwAssignFile As BusinessObject.dstTotalDeffredLC.spr_TotalDeffredLCNoticeAssign_SelectRow In dtblTotalDeffredForAssign.Rows
                        strchklstFiles &= "<div class='checkbox'> <label> <i class='fa fa-1x'></i> " & drwAssignFile.CULN & "</label></div>"
                    Next drwAssignFile

                    divchklstAssignFiles.InnerHtml = strchklstFiles
                End If

                Dim tadpWarningIntrevalInvoice As New BusinessObject.dstWarningIntervalsTableAdapters.spr_WarningIntervalsInvoice_SelectTableAdapter
                Dim dtblWarningIntervalInvoice As BusinessObject.dstWarningIntervals.spr_WarningIntervalsInvoice_SelectDataTable = Nothing

                dtblWarningIntervalInvoice = tadpWarningIntrevalInvoice.GetData(drwUserLogin.FK_BrnachID)

                If dtblWarningIntervalInvoice.Rows.Count > 0 Then
                    Dim tadpTotalDeffredForAssign As New BusinessObject.dstTotalDeffredLCTableAdapters.spr_TotalDeffredLCNoticeAssign_SelectTableAdapter
                    Dim dtblTotalDeffredForAssign As BusinessObject.dstTotalDeffredLC.spr_TotalDeffredLCNoticeAssign_SelectDataTable = Nothing

                    dtblTotalDeffredForAssign = tadpTotalDeffredForAssign.GetData(dtblBranch.First.BrnachCode, dtblWarningIntervalInvoice.First.FromDay, dtblWarningIntervalInvoice.First.ToDay, drwUserLogin.FK_BrnachID)

                    Dim strchklstFiles As String = ""

                    For Each drwAssignFile As BusinessObject.dstTotalDeffredLC.spr_TotalDeffredLCNoticeAssign_SelectRow In dtblTotalDeffredForAssign.Rows
                        strchklstFiles &= "<div class='checkbox'> <label><i class='fa fa-1x'></i> " & drwAssignFile.CULN & "</label></div>"
                    Next drwAssignFile

                    divchklstAssignFiles1.InnerHtml = strchklstFiles
                End If

                ''''''''''''''''''''''''''''''''''''''

                Dim tadpSMSCountLog As New BusinessObject.dstWarningNotificationLogDetailTableAdapters.spr_SMSCountLog_SelectTableAdapter
                Dim dtblSMSCountLog As BusinessObject.dstWarningNotificationLogDetail.spr_SMSCountLog_SelectDataTable = Nothing

                dtblSMSCountLog = tadpSMSCountLog.GetData(Date.Now.Date)
                If dtblSMSCountLog.Rows.Count <> 0 Then
                    Dim drwSMSCount As BusinessObject.dstWarningNotificationLogDetail.spr_SMSCountLog_SelectRow = dtblSMSCountLog.Rows(0)

                    ''  lblItemAdmin.Text = "لطفا جهت ثبت پیگیری از ساعت 11 صبح به بعد اقدام نمایید." & " ساعت آخرین بروزرسانی سیستم " & drwSMSCount.LastSent.ToString("HH:mm")
                Else

                    ''    lblItemAdmin.Text = "لطفا جهت ثبت پیگیری از ساعت 11 صبح به بعد اقدام نمایید."

                End If


                ''Fill HandyFollow Alarm 
                Dim tadpHandyFollowAlarm As New BusinessObject.dstAlarmTableAdapters.spr_HandyFollowAlarm_Select_CountTableAdapter
                Dim dtblHandyFollowAlarm As BusinessObject.dstAlarm.spr_HandyFollowAlarm_Select_CountDataTable = Nothing

                dtblHandyFollowAlarm = tadpHandyFollowAlarm.GetData(2, drwUserLogin.ID, -1)
                If dtblHandyFollowAlarm.Rows.Count > 0 Then
                    lblHandyFollowAlarm.Text = dtblHandyFollowAlarm.First.HandyFollow.ToString()
                End If


                ''Fill HandyFollow Check Alarm 
                Dim tadpHandyFollowCheckAlarm As New BusinessObject.dstAlarmTableAdapters.spr_HandyFollowCheckAlarm_Select_CountTableAdapter
                Dim dtblHandyFollowCheckAlarm As BusinessObject.dstAlarm.spr_HandyFollowCheckAlarm_Select_CountDataTable = Nothing

                dtblHandyFollowCheckAlarm = tadpHandyFollowCheckAlarm.GetData(2, drwUserLogin.ID, -1)
                If dtblHandyFollowCheckAlarm.Rows.Count > 0 Then
                    lblHandyFollowCheckAlarm.Text = dtblHandyFollowCheckAlarm.First.HandyFollow.ToString()
                End If


            Else

                Dim tadpSetting As New BusinessObject.dstSystemSettingTableAdapters.spr_SystemSetting_SelectTableAdapter
                Dim dtblSetting As BusinessObject.dstSystemSetting.spr_SystemSetting_SelectDataTable = Nothing

                dtblSetting = tadpSetting.GetData()

                Dim tadpLogCurrentLCStatus As New BusinessObject.dstLogCurrentLCStatus_HTableAdapters.spr_LogCurrentLCStatus_H_ForDate_SelectTableAdapter
                Dim dtblLogCurrentLCStatus As BusinessObject.dstLogCurrentLCStatus_H.spr_LogCurrentLCStatus_H_ForDate_SelectDataTable = Nothing

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


                If Date.Now.Hour >= dtblSetting.First.UpdateTime.Hours Then

                    Dim dteThisDate As Date = Date.Now.AddDays(-1)
                    dtblLogCurrentLCStatus = tadpLogCurrentLCStatus.GetData(dteThisDate)

                    If dtblLogCurrentLCStatus.Rows.Count > 0 Then

                        If dtblLogCurrentLCStatus.First.Success = True Then
                            divLogSuccess.Visible = True
                            divLogError.Visible = False

                            ' lblLogSuccess.Text = " دریافت اطلاعات با موفقیت انجام شده است"
                            lblLogSuccess.Text = "دریافت اطلاعات مربوط به مورخ " & mdlGeneral.GetPersianDate(Date.Now.AddDays(-1)) & " با موفقیت انجام شده است."


                        Else
                            divLogSuccess.Visible = False
                            divLogError.Visible = True

                            '    lblLogError.ForeColor = Drawing.Color.Red
                            lblLogError.Text = " <a href='/Application/Vosul/Reports/LogCurrentLCStatusReport.aspx' style='color:#a94442' >" & "دریافت اطلاعات با خطا مواجه شده است: " & If(dtblLogCurrentLCStatus.First.IsRemarksNull = False, dtblLogCurrentLCStatus.First.Remarks, "") & " " & "</a><a href='#' class='fa fa-history fa-2x' style='color:#31708f' title='خواندن دستی اطلاعات' onclick='return ReadData();'></a>"


                        End If

                    Else
                        divLogSuccess.Visible = False
                        divLogError.Visible = True
                        '     lblLog.ForeColor = Drawing.Color.Red
                        lblLogError.Text = " <a href='/Application/Vosul/Reports/LogCurrentLCStatusReport.aspx' style='color:#a94442' >" & "دریافت اطلاعات مربوط به مورخ " & mdlGeneral.GetPersianDate(Date.Now.AddDays(-1)) & " انجام نشده است،" & " لطفا با مدیر سیستم تماس بگیرید." & " " & "</a><a href='#' class='fa fa-history fa-2x' style='color:#31708f' title='خواندن دستی اطلاعات' onclick='return ReadData();'></a>"

                    End If

                Else

                    dtblLogCurrentLCStatus = tadpLogCurrentLCStatus.GetData(Date.Now.AddDays(-2))
                    If dtblLogCurrentLCStatus.Rows.Count > 0 Then

                        If dtblLogCurrentLCStatus.First.Success = True Then
                            divLogSuccess.Visible = True
                            divLogError.Visible = False

                            lblLogSuccess.Text = "دریافت اطلاعات مربوط به مورخ " & mdlGeneral.GetPersianDate(Date.Now.AddDays(-2)) & " با موفقیت انجام شده است."


                        Else
                            divLogSuccess.Visible = False
                            divLogError.Visible = True
                            '      lblLog.ForeColor = Drawing.Color.Red
                            lblLogError.Text = " <a href='/Application/Vosul/Reports/LogCurrentLCStatusReport.aspx' style='color:#a94442' >" & "دریافت اطلاعات با خطا مواجه شده است: " & If(dtblLogCurrentLCStatus.First.IsRemarksNull = False, dtblLogCurrentLCStatus.First.Remarks, "") & " " & "</a><a href='#' class='fa fa-history fa-2x' style='color:#31708f' title='خواندن دستی اطلاعات' onclick='return ReadData();'></a>"


                        End If

                    Else
                        divLogSuccess.Visible = False
                        divLogError.Visible = True
                        '     lblLog.ForeColor = Drawing.Color.Red
                        lblLogError.Text = " <a href='/Application/Vosul/Reports/LogCurrentLCStatusReport.aspx' style='color:#a94442' >" & "دریافت اطلاعات مربوط به مورخ " & mdlGeneral.GetPersianDate(Date.Now.AddDays(-2)) & " انجام نشده است،" & " لطفا با مدیر سیستم تماس بگیرید." & " " & "</a><a href='#' class='fa fa-history fa-2x' style='color:#31708f' title='خواندن دستی اطلاعات' onclick='return ReadData();'></a>"



                    End If

                End If

                ''Get Log Status Detailes

                Dim tadpLCLog As New BusinessObject.dstLogCurrentLCStatus_HTableAdapters.spr_LogCurrentLCStatus_H_ForDate_SelectTableAdapter
                Dim dtblLCLog As BusinessObject.dstLogCurrentLCStatus_H.spr_LogCurrentLCStatus_H_ForDate_SelectDataTable = Nothing

                dtblLCLog = tadpLCLog.GetData(Date.Now.AddDays(-2).Date)

                If dtblLCLog.Rows.Count = 0 Then

                    lblLastDayStatus.Text = " برای مورخ " & mdlGeneral.GetPersianDate(Date.Now.AddDays(-1)) & " لاگ موجود نیست سرویس را ریست کنید" & ControlChars.NewLine & "code:0"


                Else

                    Dim drwLCLog As BusinessObject.dstLogCurrentLCStatus_H.spr_LogCurrentLCStatus_H_ForDate_SelectRow = dtblLCLog.Rows(0)
                    If drwLCLog.Success = False Then

                        lblLastDayStatus.Text = " برای مورخ " & mdlGeneral.GetPersianDate(Date.Now.AddDays(-1)) & "خواندن اطلاعات با خطا همراه شده است  مشکل در BI است " & drwLCLog.Remarks & ControlChars.NewLine & "code:1"
                    Else

                        lblLastDayStatus.Text = "OK"

                        Dim tadpSMSCountLog As New BusinessObject.dstWarningNotificationLogDetailTableAdapters.spr_SMSCountLog_SelectTableAdapter
                        Dim dtblSMSCountLog As BusinessObject.dstWarningNotificationLogDetail.spr_SMSCountLog_SelectDataTable = Nothing

                        dtblSMSCountLog = tadpSMSCountLog.GetData(Date.Now.Date.AddDays(-1))

                        If dtblSMSCountLog.Rows.Count <> 0 Then

                            Dim drwSMSCount As BusinessObject.dstWarningNotificationLogDetail.spr_SMSCountLog_SelectRow = dtblSMSCountLog.Rows(0)

                            Dim tmSpan As TimeSpan = drwSMSCount.LastSent.Subtract(drwSMSCount.FirstSent)

                            lblLastDayBI.Text = drwLCLog.STime.ToString("HH:mm")
                            lblLastDayFirstSent.Text = drwSMSCount.FirstSent.ToString("HH:mm")
                            lblLastDayLastSent.Text = drwSMSCount.LastSent.ToString("HH:mm")
                            lblLastDaySendTime.Text = Math.Floor(tmSpan.TotalHours) & "h" & Math.Floor(tmSpan.Minutes) & "m"
                            lblLastDaySMSCount.Text = drwSMSCount.SMSCount.ToString("n0")
                            lblBILastDayCount.Text = drwSMSCount.BITotal.ToString("n0")
                            lblLastDaySMSVoice.Text = drwSMSCount.SMSVoice.ToString("n0")
                            lblLastDayPreSMS.Text = If(drwSMSCount.IsPreNotifySMSNull <> True, drwSMSCount.PreNotifySMS.ToString("n0"), "0")

                        End If

                    End If

                End If

                dtblLCLog = tadpLCLog.GetData(Date.Now.AddDays(-1).Date)

                If dtblLCLog.Rows.Count = 0 Then

                    lblTodayStatus.Text = " برای مورخ " & mdlGeneral.GetPersianDate(Date.Now) & " لاگ موجود نیست سرویس را ریست کنید" & ControlChars.NewLine & "code:0"
                Else

                    Dim drwLCLog As BusinessObject.dstLogCurrentLCStatus_H.spr_LogCurrentLCStatus_H_ForDate_SelectRow = dtblLCLog.Rows(0)
                    If drwLCLog.Success = False Then

                        lblTodayStatus.Text = " برای مورخ " & mdlGeneral.GetPersianDate(Date.Now) & "خواندن اطلاعات با خطا همراه شده است  مشکل در BI است " & drwLCLog.Remarks & ControlChars.NewLine & "code:1"

                    Else

                        lblTodayStatus.Text = "OK"
                        Dim tadpSMSCountLog As New BusinessObject.dstWarningNotificationLogDetailTableAdapters.spr_SMSCountLog_SelectTableAdapter
                        Dim dtblSMSCountLog As BusinessObject.dstWarningNotificationLogDetail.spr_SMSCountLog_SelectDataTable = Nothing

                        dtblSMSCountLog = tadpSMSCountLog.GetData(Date.Now.Date)

                        If dtblSMSCountLog.Rows.Count <> 0 Then

                            Dim drwSMSCount As BusinessObject.dstWarningNotificationLogDetail.spr_SMSCountLog_SelectRow = dtblSMSCountLog.Rows(0)
                            'Dim tmSpan As TimeSpan = drwWarningNotificationLogDetailFirstLastLog.Last.Subtract(drwWarningNotificationLogDetailFirstLastLog.First)
                            Dim tmSpan As TimeSpan = drwSMSCount.LastSent.Subtract(drwSMSCount.FirstSent)

                            lblTodayBI.Text = drwLCLog.STime.ToString("HH:mm")
                            lblTodayFirstSent.Text = drwSMSCount.FirstSent.ToString("HH:mm")
                            lblTodayLastSent.Text = drwSMSCount.LastSent.ToString("HH:mm")
                            lblTodaySendTime.Text = Math.Floor(tmSpan.TotalHours) & "h" & Math.Floor(tmSpan.Minutes) & "m"
                            lblTodaySMSCount.Text = drwSMSCount.SMSCount.ToString("n0")
                            lblBITodayCount.Text = drwSMSCount.BITotal.ToString("n0")
                            lblTodaySMSVoice.Text = drwSMSCount.SMSVoice.ToString("n0")
                            lblTodayPreSMS.Text = If(drwSMSCount.IsPreNotifySMSNull <> True, drwSMSCount.PreNotifySMS.ToString("n0"), "0")

                        End If


                    End If

                End If
            End If

        End If

    End Sub


    Private Sub FillBranchAdminInfo(ByVal ProvinceID As Integer, ByVal BranchCode As String)

        Dim tadplNoticeCount As New BusinessObject.dstNoticeTableAdapters.spr_NoticeStartPageCount_SelectTableAdapter
        Dim dtblNoticeCount As BusinessObject.dstNotice.spr_NoticeStartPageCount_SelectDataTable = Nothing

        dtblNoticeCount = tadplNoticeCount.GetData(1, -1)
        lblPublicNews.Text = dtblNoticeCount.First.NoticeCount

        dtblNoticeCount = tadplNoticeCount.GetData(2, ProvinceID)
        lblProvinceNews.Text = dtblNoticeCount.First.NoticeCount

        Dim tadpTotalDeffredLCCount As New BusinessObject.dstTotalDeffredLCTableAdapters.spr_TotalDeffredLCCount_SelectTableAdapter
        Dim dtblTotalDeffredLCCount As BusinessObject.dstTotalDeffredLC.spr_TotalDeffredLCCount_SelectDataTable = Nothing

        Dim tadpAdminSetting As New BusinessObject.dstSystemSettingTableAdapters.spr_AdminSetting_SelectTableAdapter
        Dim dtblAdminSetting As BusinessObject.dstSystemSetting.spr_AdminSetting_SelectDataTable = Nothing

        dtblAdminSetting = tadpAdminSetting.GetData()

        ''Deffred Files Not Assign
        dtblTotalDeffredLCCount = tadpTotalDeffredLCCount.GetData(1, BranchCode, -1, -1, -1, -1, -1)
        lblNewFileNotAssign.Text = dtblTotalDeffredLCCount.First.retval


        ''Deffred Files Assigned But not follow
        dtblTotalDeffredLCCount = tadpTotalDeffredLCCount.GetData(2, BranchCode, -1, -1, -1, -1, -1)
        lblFileAssignNotDone.Text = dtblTotalDeffredLCCount.First.retval


        ''Massive Deffred Files
        dtblTotalDeffredLCCount = tadpTotalDeffredLCCount.GetData(3, BranchCode, dtblAdminSetting.First.MassiveFilePeriod, -1, -1, -1, -1)
        lblMassiveFile.Text = dtblTotalDeffredLCCount.First.retval

        ''Dar hale Sarresid Files
        dtblTotalDeffredLCCount = tadpTotalDeffredLCCount.GetData(4, BranchCode, -1, dtblAdminSetting.First.DueDateFilePeroid, -1, -1, -1)
        lblDueDateFile.Text = dtblTotalDeffredLCCount.First.retval

        ''saresid gozashte Files
        dtblTotalDeffredLCCount = tadpTotalDeffredLCCount.GetData(5, BranchCode, -1, -1, dtblAdminSetting.First.DueDateRecivedPeriod, dtblAdminSetting.First.DoubtfulPaidPeriod, -1)
        lblDueDateFileCount.Text = dtblTotalDeffredLCCount.First.retval


        ''mashkokoulvosul Files
        dtblTotalDeffredLCCount = tadpTotalDeffredLCCount.GetData(6, BranchCode, -1, -1, -1, dtblAdminSetting.First.DoubtfulPaidPeriod, -1)
        lblDoubtfulPaidCount.Text = dtblTotalDeffredLCCount.First.retval

        ''Deffred Period files
        dtblTotalDeffredLCCount = tadpTotalDeffredLCCount.GetData(7, BranchCode, -1, -1, -1, -1, dtblAdminSetting.First.DeferredPeriod)
        lblDeferredCount.Text = dtblTotalDeffredLCCount.First.retval



        ''''fill Amounts
        Dim tadpTotalDeffredLCAmount As New BusinessObject.dstTotalDeffredLCTableAdapters.spr_TotalDeffredLCAmount_SelectTableAdapter
        Dim dtblTotalDeffredLCAmount As BusinessObject.dstTotalDeffredLC.spr_TotalDeffredLCAmount_SelectDataTable = Nothing

        '' saresid gozashte Files amount
        dtblTotalDeffredLCAmount = tadpTotalDeffredLCAmount.GetData(1, BranchCode, -1, -1, dtblAdminSetting.First.DueDateRecivedAmount, dtblAdminSetting.First.DoubtfulPaidAmount, -1)
        lblDueDateFilePeroid.Text = dtblTotalDeffredLCAmount.First.retval.ToString("N0")

        ''mashkokoulvosul Files amount
        dtblTotalDeffredLCAmount = tadpTotalDeffredLCAmount.GetData(2, BranchCode, -1, -1, -1, dtblAdminSetting.First.DoubtfulPaidAmount, -1)
        lblDoubtfulPaidPeriod.Text = dtblTotalDeffredLCAmount.First.retval.ToString("N0")

        ''Deffred Period amount
        dtblTotalDeffredLCAmount = tadpTotalDeffredLCAmount.GetData(2, BranchCode, -1, -1, -1, -1, dtblAdminSetting.First.DeferredAmount)
        lblDeferredPeriod.Text = dtblTotalDeffredLCAmount.First.retval.ToString("N0")

    End Sub

    Private Sub FillItemAdminInfo(ByVal ProvinceID As Integer)

        Dim tadplNoticeCount As New BusinessObject.dstNoticeTableAdapters.spr_NoticeStartPageCount_SelectTableAdapter
        Dim dtblNoticeCount As BusinessObject.dstNotice.spr_NoticeStartPageCount_SelectDataTable = Nothing

        dtblNoticeCount = tadplNoticeCount.GetData(1, -1)
        lblPublicNews.Text = dtblNoticeCount.First.NoticeCount

        dtblNoticeCount = tadplNoticeCount.GetData(2, ProvinceID)
        lblProvinceNews.Text = dtblNoticeCount.First.NoticeCount

        Dim tadpTotalDeffredLCCount As New BusinessObject.dstTotalDeffredLCTableAdapters.spr_TotalDeffredLCCountByItemAdmin_SelectTableAdapter
        Dim dtblTotalDeffredLCCount As BusinessObject.dstTotalDeffredLC.spr_TotalDeffredLCCountByItemAdmin_SelectDataTable = Nothing

        Dim tadpAdminSetting As New BusinessObject.dstSystemSettingTableAdapters.spr_AdminSetting_SelectTableAdapter
        Dim dtblAdminSetting As BusinessObject.dstSystemSetting.spr_AdminSetting_SelectDataTable = Nothing

        dtblAdminSetting = tadpAdminSetting.GetData()

        ''Deffred Files Not Assign
        dtblTotalDeffredLCCount = tadpTotalDeffredLCCount.GetData(1, ProvinceID, -1, -1, -1, -1, -1)
        lblNewFileNotAssign.Text = dtblTotalDeffredLCCount.First.retval


        ''Deffred Files Assigned But not follow
        dtblTotalDeffredLCCount = tadpTotalDeffredLCCount.GetData(2, ProvinceID, -1, -1, -1, -1, -1)
        lblFileAssignNotDone.Text = dtblTotalDeffredLCCount.First.retval


        ''Massive Deffred Files
        dtblTotalDeffredLCCount = tadpTotalDeffredLCCount.GetData(3, ProvinceID, dtblAdminSetting.First.MassiveFilePeriod, -1, -1, -1, -1)
        lblMassiveFile.Text = dtblTotalDeffredLCCount.First.retval

        ''Dar hale Sarresid Files
        dtblTotalDeffredLCCount = tadpTotalDeffredLCCount.GetData(4, ProvinceID, -1, dtblAdminSetting.First.DueDateFilePeroid, -1, -1, -1)
        lblDueDateFile.Text = dtblTotalDeffredLCCount.First.retval

        ''saresid gozashte Files
        dtblTotalDeffredLCCount = tadpTotalDeffredLCCount.GetData(5, ProvinceID, -1, -1, dtblAdminSetting.First.DueDateRecivedPeriod, dtblAdminSetting.First.DoubtfulPaidPeriod, -1)
        lblDueDateFileCount.Text = dtblTotalDeffredLCCount.First.retval


        ''mashkokoulvosul Files
        dtblTotalDeffredLCCount = tadpTotalDeffredLCCount.GetData(6, ProvinceID, -1, -1, -1, dtblAdminSetting.First.DoubtfulPaidPeriod, -1)
        lblDoubtfulPaidCount.Text = dtblTotalDeffredLCCount.First.retval

        ''Deffred Period files
        dtblTotalDeffredLCCount = tadpTotalDeffredLCCount.GetData(7, ProvinceID, -1, -1, -1, -1, dtblAdminSetting.First.DeferredPeriod)
        lblDeferredCount.Text = dtblTotalDeffredLCCount.First.retval



        ''''fill Amounts
        Dim tadpTotalDeffredLCAmount As New BusinessObject.dstTotalDeffredLCTableAdapters.spr_ItemAdminTotalDeffredLCAmount_SelectTableAdapter
        Dim dtblTotalDeffredLCAmount As BusinessObject.dstTotalDeffredLC.spr_ItemAdminTotalDeffredLCAmount_SelectDataTable = Nothing

        '' saresid gozashte Files amount
        dtblTotalDeffredLCAmount = tadpTotalDeffredLCAmount.GetData(1, ProvinceID, -1, -1, dtblAdminSetting.First.DueDateRecivedAmount, dtblAdminSetting.First.DoubtfulPaidAmount, -1)
        lblDueDateFilePeroid.Text = dtblTotalDeffredLCAmount.First.retval.ToString("N0")

        ''mashkokoulvosul Files amount
        dtblTotalDeffredLCAmount = tadpTotalDeffredLCAmount.GetData(2, ProvinceID, -1, -1, -1, dtblAdminSetting.First.DoubtfulPaidAmount, -1)
        lblDoubtfulPaidPeriod.Text = dtblTotalDeffredLCAmount.First.retval.ToString("N0")

        ''Deffred Period amount
        dtblTotalDeffredLCAmount = tadpTotalDeffredLCAmount.GetData(2, ProvinceID, -1, -1, -1, -1, dtblAdminSetting.First.DeferredAmount)
        lblDeferredPeriod.Text = dtblTotalDeffredLCAmount.First.retval.ToString("N0")

    End Sub

    Private Sub FillDataAdminInfo(ByVal ProvinceID As Integer)

        Dim tadplNoticeCount As New BusinessObject.dstNoticeTableAdapters.spr_NoticeStartPageCount_SelectTableAdapter
        Dim dtblNoticeCount As BusinessObject.dstNotice.spr_NoticeStartPageCount_SelectDataTable = Nothing

        dtblNoticeCount = tadplNoticeCount.GetData(1, -1)
        lblPublicNews.Text = dtblNoticeCount.First.NoticeCount

        dtblNoticeCount = tadplNoticeCount.GetData(2, ProvinceID)
        lblProvinceNews.Text = dtblNoticeCount.First.NoticeCount

        Dim tadpTotalDeffredLCCount As New BusinessObject.dstTotalDeffredLCTableAdapters.spr_TotalDeffredLCCountByAdmin_SelectTableAdapter
        Dim dtblTotalDeffredLCCount As BusinessObject.dstTotalDeffredLC.spr_TotalDeffredLCCountByAdmin_SelectDataTable = Nothing

        Dim tadpAdminSetting As New BusinessObject.dstSystemSettingTableAdapters.spr_AdminSetting_SelectTableAdapter
        Dim dtblAdminSetting As BusinessObject.dstSystemSetting.spr_AdminSetting_SelectDataTable = Nothing

        dtblAdminSetting = tadpAdminSetting.GetData()

        ''Deffred Files Not Assign
        dtblTotalDeffredLCCount = tadpTotalDeffredLCCount.GetData(1, -1, -1, -1, -1, -1)
        lblNewFileNotAssign.Text = dtblTotalDeffredLCCount.First.retval


        ''Deffred Files Assigned But not follow
        dtblTotalDeffredLCCount = tadpTotalDeffredLCCount.GetData(2, -1, -1, -1, -1, -1)
        lblFileAssignNotDone.Text = dtblTotalDeffredLCCount.First.retval


        ''Massive Deffred Files
        dtblTotalDeffredLCCount = tadpTotalDeffredLCCount.GetData(3, dtblAdminSetting.First.MassiveFilePeriod, -1, -1, -1, -1)
        lblMassiveFile.Text = dtblTotalDeffredLCCount.First.retval

        ''Dar hale Sarresid Files
        dtblTotalDeffredLCCount = tadpTotalDeffredLCCount.GetData(4, -1, dtblAdminSetting.First.DueDateFilePeroid, -1, -1, -1)
        lblDueDateFile.Text = dtblTotalDeffredLCCount.First.retval

        ''saresid gozashte Files
        dtblTotalDeffredLCCount = tadpTotalDeffredLCCount.GetData(5, -1, -1, dtblAdminSetting.First.DueDateRecivedPeriod, dtblAdminSetting.First.DoubtfulPaidPeriod, -1)
        lblDueDateFileCount.Text = dtblTotalDeffredLCCount.First.retval


        ''mashkokoulvosul Files
        dtblTotalDeffredLCCount = tadpTotalDeffredLCCount.GetData(6, -1, -1, -1, dtblAdminSetting.First.DoubtfulPaidPeriod, -1)
        lblDoubtfulPaidCount.Text = dtblTotalDeffredLCCount.First.retval

        ''Deffred Period files
        dtblTotalDeffredLCCount = tadpTotalDeffredLCCount.GetData(7, -1, -1, -1, -1, dtblAdminSetting.First.DeferredPeriod)
        lblDeferredCount.Text = dtblTotalDeffredLCCount.First.retval



        ''''fill Amounts
        Dim tadpTotalDeffredLCAmount As New BusinessObject.dstTotalDeffredLCTableAdapters.spr_DataAdminTotalDeffredLCAmount_SelectTableAdapter
        Dim dtblTotalDeffredLCAmount As BusinessObject.dstTotalDeffredLC.spr_DataAdminTotalDeffredLCAmount_SelectDataTable = Nothing

        '' saresid gozashte Files amount
        dtblTotalDeffredLCAmount = tadpTotalDeffredLCAmount.GetData(1, -1, -1, dtblAdminSetting.First.DueDateRecivedAmount, dtblAdminSetting.First.DoubtfulPaidAmount, -1)
        lblDueDateFilePeroid.Text = dtblTotalDeffredLCAmount.First.retval.ToString("N0")

        ''mashkokoulvosul Files amount
        dtblTotalDeffredLCAmount = tadpTotalDeffredLCAmount.GetData(2, -1, -1, -1, dtblAdminSetting.First.DoubtfulPaidAmount, -1)
        lblDoubtfulPaidPeriod.Text = dtblTotalDeffredLCAmount.First.retval.ToString("N0")

        ''Deffred Period amount
        dtblTotalDeffredLCAmount = tadpTotalDeffredLCAmount.GetData(2, -1, -1, -1, -1, dtblAdminSetting.First.DeferredAmount)
        lblDeferredPeriod.Text = dtblTotalDeffredLCAmount.First.retval.ToString("N0")
    End Sub




    Private Function GetSMSMessage(ByRef blnSuccess As Boolean) As String




        Dim strResultMessage As String = ""
        blnSuccess = False


        Dim tadpLCLog As New BusinessObject.dstLogCurrentLCStatus_HTableAdapters.spr_LogCurrentLCStatus_H_ForDate_SelectTableAdapter
        Dim dtblLCLog As BusinessObject.dstLogCurrentLCStatus_H.spr_LogCurrentLCStatus_H_ForDate_SelectDataTable = Nothing

        dtblLCLog = tadpLCLog.GetData(Date.Now.AddDays(-1).Date)


        If dtblLCLog.Rows.Count = 0 Then


            strResultMessage = "پیامکی در مورخ " & mdlGeneral.GetPersianDate(Date.Now.AddDays(-1)) & " ارسال نشده است. "
            '  strResultMessage = " برای مورخ " & mdlGeneral.GetPersianDate(Date.Now.AddDays(-1)) & " لاگ موجود نیست سرویس را ریست کنید" & ControlChars.NewLine & "code:0"

        Else
            Dim drwLCLog As BusinessObject.dstLogCurrentLCStatus_H.spr_LogCurrentLCStatus_H_ForDate_SelectRow = dtblLCLog.Rows(0)
            If drwLCLog.Success = False Then

                '  strResultMessage = " برای مورخ " & mdlGeneral.GetPersianDate(Date.Now.AddDays(-1)) & "خواندن اطلاعات با خطا همراه شده است  مشکل در BI است " & drwLCLog.Remarks & ControlChars.NewLine & "code:1"
                strResultMessage = "پیامکی در مورخ " & mdlGeneral.GetPersianDate(Date.Now.AddDays(-1)) & " ارسال نشده است. "

            Else

                ' strResultMessage = "اطلاعات برای مورخ " & mdlGeneral.GetPersianDate(Date.Now.AddDays(-1)) & " با موفقیت در ساعت " & drwLCLog.STime.Hour & " از BI خوانده شده است"

                Dim tadpSMSCount As New BusinessObject.dstWarningNotificationLogDetailTableAdapters.spr_WarningNotificationLogDetail_SMSCount_SelectTableAdapter
                Dim dtblSMSCount As BusinessObject.dstWarningNotificationLogDetail.spr_WarningNotificationLogDetail_SMSCount_SelectDataTable = Nothing
                dtblSMSCount = tadpSMSCount.GetData(Date.Now.Date)

                If dtblSMSCount.Rows.Count = 0 Then
                    ' strResultMessage &= ControlChars.NewLine & "ولی هیچ پیامکی تاکنون ارسال نشده است مشکل در سرویس ارسال" & ControlChars.NewLine & "code:2"
                    strResultMessage = "پیامکی در مورخ " & mdlGeneral.GetPersianDate(Date.Now.AddDays(-1)) & " ارسال نشده است. "
                Else

                    Dim drwSMSCount As BusinessObject.dstWarningNotificationLogDetail.spr_WarningNotificationLogDetail_SMSCount_SelectRow = dtblSMSCount.Rows(0)
                    If drwSMSCount.IsExpr1Null = True OrElse drwSMSCount.Expr1 = 0 Then
                        'strResultMessage &= ControlChars.NewLine & "ولی هیچ پیامکی تاکنون ارسال نشده است مشکل در سرویس ارسال" & ControlChars.NewLine & "code:2"
                        strResultMessage = "پیامکی در مورخ " & mdlGeneral.GetPersianDate(Date.Now.AddDays(-1)) & " تا این لحظه ارسال نشده است. "

                    Else
                        strResultMessage &= " در مورخ" & mdlGeneral.GetPersianDate(Date.Now.AddDays(-1)) & " تا کنون " & drwSMSCount.Expr1 & " پیامک با موفقیت ارسال شده است."
                        blnSuccess = True
                    End If
                End If
            End If
        End If


        Return strResultMessage

    End Function

    Protected Sub lnkbtnUserManual_Click(sender As Object, e As EventArgs) Handles lnkbtnUserManual.Click

        Response.Clear()
        Response.ContentType = "Application/pdf"
        Response.AppendHeader("Content-Disposition", "attachment; filename=MehrUserManual.pdf")
        Response.WriteFile(Server.MapPath("~") & "\Application\MehrUserManual.pdf")
        Response.End()

    End Sub
End Class