Imports System.IO
Public Class HadiWarningNotificationReport
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Page.Response.Cache.SetCacheability(HttpCacheability.NoCache)

        Bootstrap_Panel1.CanNew = False
        Bootstrap_Panel1.CanSave = False
        Bootstrap_Panel1.CanDelete = False
        Bootstrap_Panel1.CanSearch = False
        Bootstrap_Panel1.CanCancel = False
        Bootstrap_Panel1.CanUp = False
        Bootstrap_Panel1.CanWizard = False
        Bootstrap_Panel1.CanConfirmRequest = False
        Bootstrap_Panel1.CanReject = False
        Bootstrap_Panel1.CanDisplay = True
        Bootstrap_Panel1.CanExcel = True
        Bootstrap_Panel1.Enable_Display_Client_Validate = True
        Bootstrap_Panel1.Enable_Excel_Client_Validate = True
        cmbNotification.Attributes.Add("onchange", "cmbNotification_onchange();")
        lblInnerPageTitle.Text = "پرکردن کادرهای قرمز رنگ، اجباری است."

        If Page.IsPostBack = False Then


            Bootstrap_PersianDateTimePicker_From.GergorainDateTime = Date.Now.AddDays(-3).Date
            Bootstrap_PersianDateTimePicker_To.GergorainDateTime = Date.Now

            Bootstrap_PersianDateTimePicker_From.PickerLabel = "از"
            Bootstrap_PersianDateTimePicker_To.PickerLabel = "تا"

            Dim dtblUserLogin As BusinessObject.dstUser.spr_User_Login_SelectDataTable = CType(HttpContext.Current.Session("dtblUserLogin"), BusinessObject.dstUser.spr_User_Login_SelectDataTable)
            Dim drwUserLogin As BusinessObject.dstUser.spr_User_Login_SelectRow = dtblUserLogin.Rows(0)

            Dim tadpBrnachList As New BusinessObject.dstBranchTableAdapters.spr_Branch_List_SelectTableAdapter
            Dim dtblBranchList As BusinessObject.dstBranch.spr_Branch_List_SelectDataTable = Nothing

            Dim blnSingleBranch As Boolean = False

            If drwUserLogin.IsDataAdmin = True Then

                dtblBranchList = tadpBrnachList.GetData(1, -1)

            ElseIf drwUserLogin.IsDataAdmin = False And drwUserLogin.IsDataUserAdmin = True Then

                cmbProvince.SelectedValue = drwUserLogin.Fk_ProvinceID
                cmbProvince.Enabled = False
                dtblBranchList = tadpBrnachList.GetData(2, drwUserLogin.Fk_ProvinceID)

            Else

                cmbProvince.SelectedValue = drwUserLogin.Fk_ProvinceID
                cmbProvince.Enabled = False
                dtblBranchList = tadpBrnachList.GetData(2, drwUserLogin.Fk_ProvinceID)
                blnSingleBranch = True

            End If

            Dim strBrnachesList As String = ""

            If blnSingleBranch = True Then

                Dim tadpBranch As New BusinessObject.dstBranchTableAdapters.spr_Branch_SelectTableAdapter
                Dim dtblBranch As BusinessObject.dstBranch.spr_Branch_SelectDataTable = Nothing

                dtblBranch = tadpBranch.GetData(drwUserLogin.FK_BrnachID)

                strBrnachesList &= "<div class='checkbox'> <label> <input type='checkbox' value='" & drwUserLogin.FK_BrnachID & "' name='chklstBranch" & dtblBranch.First.ID & "'><i class='fa " & dtblBranch.First.ID & " fa-1x'></i> " & dtblBranch.First.BrnachCode & "(" & dtblBranch.First.BranchName & ")" & "</label></div>"


            Else
                For Each drwBranchList As BusinessObject.dstBranch.spr_Branch_List_SelectRow In dtblBranchList.Rows

                    strBrnachesList &= "<div class='checkbox'> <label> <input type='checkbox' value='" & drwBranchList.ID & "' name='chklstBranch" & drwBranchList.ID & "'><i class='fa " & drwBranchList.ID & " fa-1x'></i> " & drwBranchList.BrnachCode & "</label></div>"
                Next drwBranchList

            End If

            divBranches.InnerHtml = strBrnachesList

        Else


            Dim dtblUserLogin As BusinessObject.dstUser.spr_User_Login_SelectDataTable = CType(HttpContext.Current.Session("dtblUserLogin"), BusinessObject.dstUser.spr_User_Login_SelectDataTable)
            Dim drwUserLogin As BusinessObject.dstUser.spr_User_Login_SelectRow = dtblUserLogin.Rows(0)

            Dim tadpBrnachList As New BusinessObject.dstBranchTableAdapters.spr_Branch_List_SelectTableAdapter
            Dim dtblBranchList As BusinessObject.dstBranch.spr_Branch_List_SelectDataTable = Nothing

            Dim blnSingleBranch As Boolean = False

            If drwUserLogin.IsDataAdmin = True Then

                If cmbProvince.SelectedValue = -1 Then

                    dtblBranchList = tadpBrnachList.GetData(1, -1)

                Else
                    dtblBranchList = tadpBrnachList.GetData(2, CInt(cmbProvince.SelectedValue))

                End If


            ElseIf drwUserLogin.IsDataAdmin = False And drwUserLogin.IsDataUserAdmin = True Then

                cmbProvince.SelectedValue = drwUserLogin.Fk_ProvinceID
                cmbProvince.Enabled = False
                dtblBranchList = tadpBrnachList.GetData(2, drwUserLogin.Fk_ProvinceID)

            Else

                cmbProvince.SelectedValue = drwUserLogin.Fk_ProvinceID
                cmbProvince.Enabled = False
                dtblBranchList = tadpBrnachList.GetData(2, drwUserLogin.Fk_ProvinceID)
                blnSingleBranch = True

            End If

            Dim strBrnachesList As String = ""

            For Each drwBranchList As BusinessObject.dstBranch.spr_Branch_List_SelectRow In dtblBranchList.Rows

                Dim boolChecked As Boolean = False


                For i As Integer = 0 To Request.Form.Keys.Count - 2

                    If Request.Form.Keys(i).StartsWith("chklstBranch") = True Then

                        If CInt(Request.Form(i)) = drwBranchList.ID Then
                            boolChecked = True
                            Exit For
                        End If

                    End If

                Next

                If blnSingleBranch = False Then

                    If boolChecked = True Then
                        strBrnachesList &= "<div class='checkbox'> <label> <input type='checkbox' checked='checked' value='" & drwBranchList.ID & "' name='chklstBranch" & drwBranchList.ID & "'><i class='fa " & drwBranchList.ID & " fa-1x'></i> " & drwBranchList.BrnachCode & "</label></div>"

                    Else

                        strBrnachesList &= "<div class='checkbox'> <label> <input type='checkbox' value='" & drwBranchList.ID & "' name='chklstBranch" & drwBranchList.ID & "'><i class='fa " & drwBranchList.ID & " fa-1x'></i> " & drwBranchList.BrnachCode & "</label></div>"
                    End If
                End If


            Next drwBranchList

            divBranches.InnerHtml = strBrnachesList


            If blnSingleBranch = True Then

                Dim tadpBranch As New BusinessObject.dstBranchTableAdapters.spr_Branch_SelectTableAdapter
                Dim dtblBranch As BusinessObject.dstBranch.spr_Branch_SelectDataTable = Nothing

                dtblBranch = tadpBranch.GetData(drwUserLogin.FK_BrnachID)

                strBrnachesList &= "<div class='checkbox'> <label> <input type='checkbox' checked='checked' value='" & drwUserLogin.FK_BrnachID & "' name='chklstBranch" & dtblBranch.First.ID & "'><i class='fa " & dtblBranch.First.ID & " fa-1x'></i> " & dtblBranch.First.BrnachCode & "(" & dtblBranch.First.BranchName & ")" & "</label></div>"
                divBranches.InnerHtml = strBrnachesList



            End If
        End If


        Bootstrap_PersianDateTimePicker_From.ShowTimePicker = True
        Bootstrap_PersianDateTimePicker_To.ShowTimePicker = True

    End Sub



    Protected Sub cmbWarningType_DataBound(sender As Object, e As EventArgs) Handles cmbWarningType.DataBound
        Dim li As New ListItem
        li.Text = "(همه)"
        li.Value = -1
        cmbWarningType.Items.Insert(0, li)
    End Sub


    Private Sub Bootstrap_Panel1_Panel_Display_Click(sender As Object, e As System.EventArgs) Handles Bootstrap_Panel1.Panel_Display_Click

        Try

            GenerateReport()

        Catch ex As Exception

            Bootstrap_Panel1.ShowMessage(ex.Message, False)

        End Try


    End Sub

    Private Sub GenerateReport()



        Dim dtFromDate As Date = Bootstrap_PersianDateTimePicker_From.GergorainDateTime
        Dim dteToDate As Date = Bootstrap_PersianDateTimePicker_To.GergorainDateTime


        If rdbReportType.SelectedValue = 1 Then



            Dim blnSelectFile As Boolean = False

            If txtFile.Text.Trim() = "" Then

                Dim tadpNotificationReport As New BusinessObject.dstReportTableAdapters.spr_NotificationDetailes_ReportTableAdapter
                Dim dtblNotificationReport As BusinessObject.dstReport.spr_NotificationDetailes_ReportDataTable = Nothing

                If cmbNotification.SelectedIndex = 0 Then



                    If cmbProvince.SelectedValue <> -1 Then

                        If chkBranchSelectAll.Checked = True Then
                            dtblNotificationReport = tadpNotificationReport.GetData(5, dtFromDate, dteToDate, -1, -1, cmbProvince.SelectedValue)
                        Else

                            For i As Integer = 0 To Request.Form.Keys.Count - 1

                                If Request.Form.Keys(i).StartsWith("chklstBranch") = True Then
                                    Dim intBranchID As Integer = CInt(Request.Form(i))

                                    dtblNotificationReport = tadpNotificationReport.GetData(3, dtFromDate, dteToDate, -1, intBranchID, cmbProvince.SelectedValue)
                                    Exit For
                                End If

                            Next i


                        End If



                    Else

                        ''Action 1

                        dtblNotificationReport = tadpNotificationReport.GetData(1, dtFromDate, dteToDate, -1, -1, -1)


                    End If


                Else
                    ''with notificationtype Filter
                    If cmbProvince.SelectedValue <> -1 Then

                        If chkBranchSelectAll.Checked = True Then
                            dtblNotificationReport = tadpNotificationReport.GetData(6, dtFromDate, dteToDate, cmbNotification.SelectedValue, -1, cmbProvince.SelectedValue)
                        Else

                            For i As Integer = 0 To Request.Form.Keys.Count - 1

                                If Request.Form.Keys(i).StartsWith("chklstBranch") = True Then
                                    Dim intBranchID As Integer = CInt(Request.Form(i))

                                    dtblNotificationReport = tadpNotificationReport.GetData(4, dtFromDate, dteToDate, cmbNotification.SelectedValue, intBranchID, cmbProvince.SelectedValue)
                                    Exit For
                                End If

                            Next i


                        End If



                    Else

                        ''Action 2

                        dtblNotificationReport = tadpNotificationReport.GetData(2, dtFromDate, dteToDate, cmbNotification.SelectedValue, -1, -1)


                    End If




                End If

                Session("WarningNotificationReport") = dtblNotificationReport

                ''get total report
                Dim intCount As Integer = 0 'intFromRecord

                For Each drwNotificationReport As BusinessObject.dstReport.spr_NotificationDetailes_ReportRow In dtblNotificationReport

                    divResult.Visible = True
                    tblResult.Visible = True
                    tblResult2.Visible = False
                    tblResultCustomer.Visible = False

                    intCount += 1
                    Dim TbRow As New HtmlTableRow
                    Dim TbCell As HtmlTableCell

                    TbCell = New HtmlTableCell
                    TbCell.InnerHtml = "<input type='hidden' id='hdnAmounts" & CStr(intCount) & "' >" & CStr(intCount)
                    TbCell.Align = "center"
                    TbCell.NoWrap = True
                    TbRow.Cells.Add(TbCell)

                    TbCell = New HtmlTableCell
                    TbCell.InnerHtml = drwNotificationReport.ReceiverInfo
                    TbCell.NoWrap = True
                    TbCell.Align = "center"
                    TbRow.Cells.Add(TbCell)


                    TbCell = New HtmlTableCell

                    Dim intNotificationTypeID As Integer = drwNotificationReport.NotificationTypeID
                    Dim strNotification As String = ""
                    Select Case intNotificationTypeID
                        Case 1

                            strNotification = "پیامک"

                        Case 2
                            strNotification = "تلفن"


                        Case 3
                            strNotification = "دعوتنامه"

                        Case 4
                            strNotification = "اخطاریه"

                        Case 5
                            strNotification = "اظهارنامه"
                        Case 6
                            strNotification = "صوتی"

                    End Select


                    TbCell = New HtmlTableCell
                    TbCell.InnerHtml = strNotification
                    TbCell.NoWrap = True
                    TbCell.Align = "center"
                    TbRow.Cells.Add(TbCell)



                    TbCell = New HtmlTableCell
                    TbCell.InnerHtml = drwNotificationReport.LoanNumber
                    TbCell.NoWrap = True
                    TbCell.Align = "center"
                    TbRow.Cells.Add(TbCell)

                    ''TbCell = New HtmlTableCell
                    ''  TbCell.InnerHtml = drwNotificationReport.strMessage
                    ''TbCell.NoWrap = False
                    ''TbCell.Align = "center"
                    ''TbRow.Cells.Add(TbCell)


                    TbCell = New HtmlTableCell
                    If drwNotificationReport.IsSendDateNull = False Then
                        TbCell.InnerHtml = mdlGeneral.GetPersianDateTime(drwNotificationReport.SendDate)
                    Else
                        TbCell.InnerHtml = ""
                    End If

                    TbCell.NoWrap = True
                    TbCell.Align = "center"
                    TbCell.Attributes.Add("dir", "rtl")
                    TbRow.Cells.Add(TbCell)


                    'TbCell = New HtmlTableCell
                    'TbCell.InnerHtml = lnqWarningNotificationLogDetailListItem.tbl_WarningNotificationLog.tbl_WarningIntervals.FromDay.ToString() & "-" & lnqWarningNotificationLogDetailListItem.tbl_WarningNotificationLog.tbl_WarningIntervals.ToDay.ToString()
                    'TbCell.NoWrap = True
                    'TbCell.Align = "center"
                    'TbRow.Cells.Add(TbCell)

                    Dim chrDash As String() = drwNotificationReport.LoanNumber.Split("-")
                    ''get Branch
                    TbCell = New HtmlTableCell
                    TbCell.InnerHtml = drwNotificationReport.BranchName & "(" & chrDash(0).ToString() & ")"
                    TbCell.NoWrap = True
                    TbCell.Align = "center"
                    TbRow.Cells.Add(TbCell)

                    ''get customer no

                    Dim strCustomerNo As String = chrDash(2).ToString()

                    TbCell = New HtmlTableCell
                    TbCell.InnerHtml = strCustomerNo
                    TbCell.NoWrap = False
                    TbCell.Align = "center"
                    TbRow.Cells.Add(TbCell)

                    'TbCell = New HtmlTableCell
                    'Select Case lnqWarningNotificationLogDetailListItem.SendStatus
                    '    Case 1
                    '        TbCell.InnerHtml = "ارسال نشده"
                    '    Case 2
                    '        TbCell.InnerHtml = "ارسال شده"
                    '    Case 3
                    '        TbCell.InnerHtml = "رسیده به گوشی"
                    '    Case 4
                    '        TbCell.InnerHtml = "خطا در ارسال"
                    '    Case 5
                    '        TbCell.InnerHtml = "صادر شده"
                    '    Case 6
                    '        TbCell.InnerHtml = "ارسال شده"
                    '    Case 7
                    '        TbCell.InnerHtml = "تحویل شده"
                    '    Case 8
                    '        TbCell.InnerHtml = "تماس برقرار شده"
                    '    Case 9
                    '        TbCell.InnerHtml = "پاسخ داده شده"
                    'End Select
                    'TbCell.NoWrap = True
                    'TbCell.Align = "center"
                    'TbRow.Cells.Add(TbCell)

                    tblResult.Rows.Add(TbRow)


                    If intCount = 100 Then

                        Exit For
                    End If
                Next

                dtblNotificationReport = Nothing

            Else


                ''Customer NO
                blnSelectFile = True
                Dim tadpNotificationReport As New BusinessObject.dstReportTableAdapters.spr_NotificationByCustomerNO_ReportTableAdapter
                Dim dtblNotificationReport As BusinessObject.dstReport.spr_NotificationByCustomerNO_ReportDataTable = Nothing

                If cmbNotification.SelectedIndex = 0 Then
                    dtblNotificationReport = tadpNotificationReport.GetData(1, dtFromDate, dteToDate, -1, txtFile.Text)
                Else
                    dtblNotificationReport = tadpNotificationReport.GetData(2, dtFromDate, dteToDate, cmbNotification.SelectedValue, txtFile.Text)
                End If


                ''get total report
                Dim intCount As Integer = 0 'intFromRecord

                Session("WarningNotificationReport") = dtblNotificationReport

                For Each drwNotificationReport As BusinessObject.dstReport.spr_NotificationByCustomerNO_ReportRow In dtblNotificationReport

                    divResult.Visible = True
                    tblResult.Visible = False
                    tblResult2.Visible = False
                    tblResultCustomer.Visible = True


                    intCount += 1
                    Dim TbRow As New HtmlTableRow
                    Dim TbCell As HtmlTableCell

                    TbCell = New HtmlTableCell
                    TbCell.InnerHtml = "<input type='hidden' id='hdnAmounts" & CStr(intCount) & "' >" & CStr(intCount)
                    TbCell.Align = "center"
                    TbCell.NoWrap = True
                    TbRow.Cells.Add(TbCell)

                    TbCell = New HtmlTableCell
                    TbCell.InnerHtml = drwNotificationReport.ReceiverInfo
                    TbCell.NoWrap = True
                    TbCell.Align = "center"
                    TbRow.Cells.Add(TbCell)



                    TbCell = New HtmlTableCell

                    Dim intNotificationTypeID As Integer = drwNotificationReport.NotificationTypeID
                    Dim strNotification As String = ""
                    Select Case intNotificationTypeID
                        Case 1

                            strNotification = "پیامک"

                        Case 2
                            strNotification = "تلفن"


                        Case 3
                            strNotification = "دعوتنامه"

                        Case 4
                            strNotification = "اخطاریه"

                        Case 5
                            strNotification = "اظهارنامه"
                        Case 6
                            strNotification = "صوتی"

                    End Select


                    TbCell = New HtmlTableCell
                    TbCell.InnerHtml = strNotification
                    TbCell.NoWrap = True
                    TbCell.Align = "center"
                    TbRow.Cells.Add(TbCell)



                    TbCell = New HtmlTableCell
                    TbCell.InnerHtml = drwNotificationReport.LoanNumber
                    TbCell.NoWrap = False
                    TbCell.Align = "center"
                    TbRow.Cells.Add(TbCell)



                    TbCell = New HtmlTableCell
                    If drwNotificationReport.IsSendDateNull = False Then
                        TbCell.InnerHtml = mdlGeneral.GetPersianDateTime(drwNotificationReport.SendDate)
                    Else
                        TbCell.InnerHtml = ""
                    End If

                    TbCell.NoWrap = True
                    TbCell.Align = "center"
                    TbCell.Attributes.Add("dir", "ltr")
                    TbRow.Cells.Add(TbCell)


                    'TbCell = New HtmlTableCell
                    'TbCell.InnerHtml = lnqWarningNotificationLogDetailListItem.tbl_WarningNotificationLog.tbl_WarningIntervals.FromDay.ToString() & "-" & lnqWarningNotificationLogDetailListItem.tbl_WarningNotificationLog.tbl_WarningIntervals.ToDay.ToString()
                    'TbCell.NoWrap = True
                    'TbCell.Align = "center"
                    'TbRow.Cells.Add(TbCell)


                    ''get Branch
                    Dim chrDash As String() = drwNotificationReport.LoanNumber.Split("-")
                    TbCell = New HtmlTableCell
                    TbCell.InnerHtml = drwNotificationReport.BranchName & "(" & chrDash(0).ToString() & ")"
                    TbCell.NoWrap = False
                    TbCell.Align = "center"
                    TbRow.Cells.Add(TbCell)

                    ''get customer no
                    Dim strCustomerNo As String = chrDash(2).ToString()

                    TbCell = New HtmlTableCell
                    TbCell.InnerHtml = strCustomerNo
                    TbCell.NoWrap = False
                    TbCell.Align = "center"
                    TbRow.Cells.Add(TbCell)

                    TbCell = New HtmlTableCell
                    TbCell.InnerHtml = drwNotificationReport.strMessage
                    TbCell.NoWrap = False
                    TbCell.Align = "center"
                    TbRow.Cells.Add(TbCell)

                    TbCell = New HtmlTableCell
                    TbCell.InnerHtml = drwNotificationReport.CustomerName
                    TbCell.NoWrap = False
                    TbCell.Align = "center"
                    TbRow.Cells.Add(TbCell)



                    tblResultCustomer.Rows.Add(TbRow)


                    If intCount = 100 Then

                        Exit For
                    End If
                Next

                dtblNotificationReport = Nothing

            End If


        Else

            ''summary report
            Dim tadpNotificationSummaryReport As New BusinessObject.dstReportTableAdapters.spr_NotificationSummary_ReportTableAdapter
            Dim dtblNotificationSummaryReport As BusinessObject.dstReport.spr_NotificationSummary_ReportDataTable = Nothing


            If cmbNotification.SelectedIndex = 0 Then



                If cmbProvince.SelectedValue <> -1 Then

                    If chkBranchSelectAll.Checked = True Then
                        dtblNotificationSummaryReport = tadpNotificationSummaryReport.GetData(5, dtFromDate, dteToDate, -1, cmbProvince.SelectedValue, -1)
                    Else

                        For i As Integer = 0 To Request.Form.Keys.Count - 1

                            If Request.Form.Keys(i).StartsWith("chklstBranch") = True Then
                                Dim intBranchID As Integer = CInt(Request.Form(i))

                                dtblNotificationSummaryReport = tadpNotificationSummaryReport.GetData(3, dtFromDate, dteToDate, -1, -1, intBranchID)
                                Exit For
                            End If

                        Next i


                    End If



                Else

                    ''Action 1

                    dtblNotificationSummaryReport = tadpNotificationSummaryReport.GetData(1, dtFromDate, dteToDate, -1, -1, -1)


                End If


            Else
                ''with notificationtype Filter
                If cmbProvince.SelectedValue <> -1 Then

                    If chkBranchSelectAll.Checked = True Then
                        dtblNotificationSummaryReport = tadpNotificationSummaryReport.GetData(6, dtFromDate, dteToDate, cmbNotification.SelectedValue, cmbProvince.SelectedValue, -1)
                    Else

                        For i As Integer = 0 To Request.Form.Keys.Count - 1

                            If Request.Form.Keys(i).StartsWith("chklstBranch") = True Then
                                Dim intBranchID As Integer = CInt(Request.Form(i))

                                dtblNotificationSummaryReport = tadpNotificationSummaryReport.GetData(4, dtFromDate, dteToDate, cmbNotification.SelectedValue, -1, intBranchID)
                                Exit For
                            End If

                        Next i


                    End If



                Else

                    ''Action 2

                    dtblNotificationSummaryReport = tadpNotificationSummaryReport.GetData(2, dtFromDate, dteToDate, cmbNotification.SelectedValue, -1, -1)


                End If

            End If

            Session("WarningNotificationSummaryReport") = dtblNotificationSummaryReport

            divResult.Visible = True
            tblResult.Visible = False
            tblResultCustomer.Visible = False
            tblResult2.Visible = True

            ''Fill Table
            Dim intCount As Integer = 0
            For Each drow As BusinessObject.dstReport.spr_NotificationSummary_ReportRow In dtblNotificationSummaryReport

                intCount += 1
                Dim TbRow As New HtmlTableRow
                Dim TbCell As HtmlTableCell

                TbCell = New HtmlTableCell
                TbCell.InnerHtml = CStr(intCount)
                TbCell.Align = "center"
                TbCell.NoWrap = True
                TbRow.Cells.Add(TbCell)


                TbCell = New HtmlTableCell
                TbCell.InnerHtml = drow.ProvinceName
                TbCell.NoWrap = True
                TbCell.Align = "center"
                TbCell.Attributes.Add("dir", "rtl")
                TbRow.Cells.Add(TbCell)

                TbCell = New HtmlTableCell
                Dim intTotalCount As Integer = drow.Count

                TbCell.InnerHtml = intTotalCount.ToString("n0")
                TbCell.NoWrap = True
                TbCell.Align = "center"
                TbRow.Cells.Add(TbCell)


                tblResult2.Rows.Add(TbRow)


            Next

            dtblNotificationSummaryReport = Nothing

        End If







    End Sub

    Private Sub Bootstrap_Panel1_Panel_Excel_Click(sender As Object, e As System.EventArgs) Handles Bootstrap_Panel1.Panel_Excel_Click

        Try


            Dim dtblUserLogin As BusinessObject.dstUser.spr_User_Login_SelectDataTable = CType(Session("dtblUserLogin"), BusinessObject.dstUser.spr_User_Login_SelectDataTable)
            Dim drwUserLogin As BusinessObject.dstUser.spr_User_Login_SelectRow = dtblUserLogin.Rows(0)


            Dim strPath As String = Server.MapPath("") & "\TempFile\" & drwUserLogin.ID & "\"
            Dim FileName As String = "WarningNotificationReport-" & drwUserLogin.ID.ToString()


            If rdbReportType.SelectedValue = 1 Then


                If Session("WarningNotificationReport") IsNot Nothing Then


                    '' Dim tblCSVResult As DataTable = dtblNotificationReport
                    Dim tblCSVResult As DataTable = Session("WarningNotificationReport")
                    If tblCSVResult.Rows.Count > 0 Then
                        If Not System.IO.Directory.Exists(strPath) Then
                            System.IO.Directory.CreateDirectory(strPath)
                        End If

                        Dim clsCSVWriter As New clsCSVWriter
                        Using strWriter As StreamWriter = New StreamWriter(strPath & FileName)
                            'If ViewState("WarningNotificationReportType") = "1" Then
                            '    WriteDataTableCustom(tblCSVResult, FileName, strWriter, True)
                            'Else
                            WriteDataTableCustom(tblCSVResult, FileName, strWriter, True)
                            'End If

                        End Using
                    Else
                        Bootstrap_Panel1.ShowMessage("امکان انتقال گزارش به فایل اکسل وجود ندارد", False)
                        Session("WarningNotificationReport") = Nothing
                    End If


                End If

            Else
                ''Summary Report
                If Session("WarningNotificationSummaryReport") IsNot Nothing Then

                    Dim tblCSVResult As DataTable = Session("WarningNotificationSummaryReport")
                    If tblCSVResult.Rows.Count > 0 Then
                        If Not System.IO.Directory.Exists(strPath) Then
                            System.IO.Directory.CreateDirectory(strPath)
                        End If

                        Dim clsCSVWriter As New clsCSVWriter
                        Using strWriter As StreamWriter = New StreamWriter(strPath & FileName)

                            WriteDataTableCustomSummary(tblCSVResult, FileName, strWriter, True)


                        End Using
                    Else
                        Bootstrap_Panel1.ShowMessage("امکان انتقال گزارش به فایل اکسل وجود ندارد", False)
                        Session("WarningNotificationSummaryReport") = Nothing
                    End If


                End If



            End If

        Catch ex As Exception
            Bootstrap_Panel1.ShowMessage("امکان انتقال گزارش به فایل اکسل وجود ندارد", False)
            Session("WarningNotificationReport") = Nothing
            Session("WarningNotificationSummaryReport") = Nothing
        End Try


    End Sub




    Protected Sub cmbProvince_DataBound(sender As Object, e As EventArgs) Handles cmbProvince.DataBound
        Dim li As New ListItem
        li.Text = "(همه استان ها)"
        li.Value = -1
        cmbProvince.Items.Insert(0, li)

    End Sub

    Protected Sub cmbProvince_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbProvince.SelectedIndexChanged

        Dim tadpBrnachList As New BusinessObject.dstBranchTableAdapters.spr_Branch_List_SelectTableAdapter
        Dim dtblBranchList As BusinessObject.dstBranch.spr_Branch_List_SelectDataTable = Nothing
        If cmbProvince.SelectedValue = -1 Then
            dtblBranchList = tadpBrnachList.GetData(1, -1)
        Else
            dtblBranchList = tadpBrnachList.GetData(2, cmbProvince.SelectedValue)
        End If

        Dim strBrnachesList As String = ""


        For Each drwBranchList As BusinessObject.dstBranch.spr_Branch_List_SelectRow In dtblBranchList.Rows
            strBrnachesList &= "<div class='checkbox'> <label> <input type='checkbox' value='" & drwBranchList.ID & "' name='chklstBranch" & drwBranchList.ID & "'><i class='fa " & drwBranchList.ID & " fa-1x'></i> " & drwBranchList.BrnachCode & "</label></div>"
        Next drwBranchList

        divBranches.InnerHtml = strBrnachesList
        chkBranchSelectAll.Checked = False

    End Sub



    Private Sub WriteDataTableCustom(ByVal sourceTable As DataTable, ByVal FileName As String, ByVal writer As TextWriter, ByVal includeHeaders As Boolean)

        Dim context As HttpContext = HttpContext.Current
        Dim memoryStream As New MemoryStream
        writer = New StreamWriter(memoryStream, Encoding.UTF8)
        If (includeHeaders) Then
            Dim headerValues As List(Of String) = New List(Of String)()
            For Each column As DataColumn In sourceTable.Columns
                Select Case column.ColumnName

                    Case "ReceiverInfo"

                        headerValues.Add(QuoteValue("شماره موبایل"))

                    Case "LoanNumber"

                        headerValues.Add(QuoteValue("شماره وام"))


                    Case "NotificationTypeID"
                        headerValues.Add(QuoteValue("نوع اطلاع رسانی"))


                    Case "SendDate"

                        headerValues.Add(QuoteValue("تاریخ ارسال"))

                    Case "BranchName"

                        headerValues.Add(QuoteValue("نام شعبه"))

                        ''Case "SentLogTime"

                        ''    headerValues.Add(QuoteValue("[زمان ارسال]"))

                        ''Case "SendStatus"

                        ''    headerValues.Add(QuoteValue("[وضعیت]"))

                        ''Case "NotificationType"

                        ''    headerValues.Add(QuoteValue("[نوع اطلاع رسانی]"))
                        ''Case "LoanNumber"

                        ''    headerValues.Add(QuoteValue("[وام]"))

                        ''Case "Branch"

                        ''    headerValues.Add(QuoteValue("[شعبه]"))

                        ''Case "Interval"

                        ''    headerValues.Add(QuoteValue("[بازه]"))
                End Select
                '  headerValues.Add(QuoteValue(column.ColumnName))
            Next
            writer.WriteLine(String.Join(",", headerValues.ToArray))
        End If
        Dim items() As String = Nothing
        ReDim items(6)
        For Each row As DataRow In sourceTable.Rows

            For i As Integer = 0 To sourceTable.Columns.Count - 1
                If i = 1 Then

                    items(i) = row.Item(i).ToString

                    ''get customer no
                    Dim chrDash As String() = items(i).Split("-")
                    items(5) = chrDash(2).ToString()
                    ''get branch
                    items(4) = "(" & chrDash(0).ToString() & ")"

                ElseIf i = 2 Then

                    Select Case row.Item(i)
                        Case 1

                            items(2) = "پیامک"
                        Case 2
                            items(2) = "تلفن"
                        Case 3
                            items(2) = "دعوتنامه"

                        Case 4
                            items(2) = "اخطاریه"

                        Case 5
                            items(2) = "اظهارنامه"
                        Case 6
                            items(2) = "صوتی"

                    End Select

                ElseIf i = 3 Then

                    items(3) = mdlGeneral.GetPersianDateTime(CDate(row.Item(i)))
                ElseIf i = 4 Then
                    items(4) = row.Item(i).ToString & items(4)
                Else
                    items(i) = row.Item(i).ToString
                End If

            Next i

            '    items = row.ItemArray.Select(Function(obj) QuoteValue(obj.ToString())).ToArray()
            writer.WriteLine(String.Join(",", items))
        Next

        writer.Flush()
        Dim bytesInStream As Byte() = memoryStream.ToArray()
        memoryStream.Close()
        context.Response.Clear()
        context.Response.Charset = String.Empty
        context.Response.ContentType = "text/csv"
        context.Response.AddHeader("Content-Disposition", "attachment; filename=" + FileName + ".csv")
        context.Response.BinaryWrite(bytesInStream)
        context.Response.End()
    End Sub

    Private Sub WriteDataTableCustomSummary(ByVal sourceTable As DataTable, ByVal FileName As String, ByVal writer As TextWriter, ByVal includeHeaders As Boolean)

        Dim context As HttpContext = HttpContext.Current
        Dim memoryStream As New MemoryStream
        writer = New StreamWriter(memoryStream, Encoding.UTF8)
        If (includeHeaders) Then
            Dim headerValues As List(Of String) = New List(Of String)()
            For Each column As DataColumn In sourceTable.Columns
                Select Case column.ColumnName

                    Case "ProvinceName"

                        headerValues.Add(QuoteValue("استان"))

                    Case "Count"

                        headerValues.Add(QuoteValue("تعداد"))



                End Select
                '  headerValues.Add(QuoteValue(column.ColumnName))
            Next
            writer.WriteLine(String.Join(",", headerValues.ToArray))
        End If
        Dim items() As String = Nothing
        ReDim items(2)
        For Each row As DataRow In sourceTable.Rows

            For i As Integer = 0 To sourceTable.Columns.Count - 1

                items(i) = row.Item(i).ToString


            Next i

            '    items = row.ItemArray.Select(Function(obj) QuoteValue(obj.ToString())).ToArray()
            writer.WriteLine(String.Join(",", items))
        Next

        writer.Flush()
        Dim bytesInStream As Byte() = memoryStream.ToArray()
        memoryStream.Close()
        context.Response.Clear()
        context.Response.Charset = String.Empty
        context.Response.ContentType = "text/csv"
        context.Response.AddHeader("Content-Disposition", "attachment; filename=" + FileName + ".csv")
        context.Response.BinaryWrite(bytesInStream)
        context.Response.End()
    End Sub



    Private Function QuoteValue(ByVal value As String) As String
        Return String.Concat("""", value.Replace("""", """"""), """")
    End Function

End Class