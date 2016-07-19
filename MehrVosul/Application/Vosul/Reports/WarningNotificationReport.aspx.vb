Imports System.IO
Public Class WarningNotificationReport
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

        tblResult.Visible = False
        tblResult2.Visible = False
        Bootstrap_Panel1.ClearMessage()

        Dim intTotalRecord As Integer = GetWarningReportRecordCount()
        If intTotalRecord <> 0 Then
            Session("TotalPage") = Math.Floor(intTotalRecord / 25) + 1
            Session("CurrentPage") = "1"
            GenerateWarningListReport()
        Else
            Session("TotalPage") = "0"
            Session("CurrentPage") = "0"
        End If

        txtPageCounter.Value = Session("CurrentPage")

    End Sub

    Private Sub Bootstrap_Panel1_Panel_Excel_Click(sender As Object, e As System.EventArgs) Handles Bootstrap_Panel1.Panel_Excel_Click

        Try
            If Session("WarningNotificationReport") IsNot Nothing Then
                Dim dtblUserLogin As BusinessObject.dstUser.spr_User_Login_SelectDataTable = CType(Session("dtblUserLogin"), BusinessObject.dstUser.spr_User_Login_SelectDataTable)
                Dim drwUserLogin As BusinessObject.dstUser.spr_User_Login_SelectRow = dtblUserLogin.Rows(0)


                Dim strPath As String = Server.MapPath("") & "\TempFile\" & drwUserLogin.ID & "\"
                Dim FileName As String = "WarningNotificationReport-" & drwUserLogin.ID.ToString()

                Dim tblCSVResult As DataTable = Session("WarningNotificationReport")
                If tblCSVResult.Rows.Count > 0 Then
                    If Not System.IO.Directory.Exists(strPath) Then
                        System.IO.Directory.CreateDirectory(strPath)
                    End If

                    Dim clsCSVWriter As New clsCSVWriter
                    Using strWriter As StreamWriter = New StreamWriter(strPath & FileName)
                        If ViewState("WarningNotificationReportType") = "1" Then
                            WriteDataTableCustom(tblCSVResult, FileName, strWriter, True)
                        Else
                            WriteDataTableCustom(tblCSVResult, FileName, strWriter, True)
                        End If

                    End Using
                Else
                    Bootstrap_Panel1.ShowMessage("امکان انتقال گزارش به فایل اکسل وجود ندارد", False)
                    Session("WarningNotificationReport") = Nothing
                End If
            Else
                Bootstrap_Panel1.ShowMessage("امکان انتقال گزارش به فایل اکسل وجود ندارد", False)
                Session("WarningNotificationReport") = Nothing
            End If
        Catch ex As Exception
            Bootstrap_Panel1.ShowMessage("امکان انتقال گزارش به فایل اکسل وجود ندارد", False)
            Session("WarningNotificationReport") = Nothing
        End Try



    End Sub
    Private Function GetWarningReportRecordCount() As Integer


        Dim dtFromDate As Date = Bootstrap_PersianDateTimePicker_From.GergorainDateTime
        Dim dteToDate As Date = Bootstrap_PersianDateTimePicker_To.GergorainDateTime


        Dim ctxMehr As New BusinessObject.dbMehrVosulEntities1
        Dim lnqWarningNotificationLogDetail = ctxMehr.tbl_WarningNotificationLogDetail.Where(Function(x) x.tbl_WarningNotificationLog.STime >= dtFromDate AndAlso x.tbl_WarningNotificationLog.STime <= dteToDate)



        tblResult.Visible = False
        tblResult2.Visible = False
        Bootstrap_Panel1.ClearMessage()


        Dim blnProvince As Boolean = False

        If cmbProvince.SelectedValue <> -1 And chkBranchSelectAll.Checked = True Then
            lnqWarningNotificationLogDetail = lnqWarningNotificationLogDetail.Where(Function(x) x.tbl_WarningNotificationLog.tbl_Loan.tbl_Branch.Fk_ProvinceID = cmbProvince.SelectedValue)
            blnProvince = True
        End If


        If txtFile.Text.Trim() <> "" Then

            lnqWarningNotificationLogDetail = lnqWarningNotificationLogDetail.Where(Function(x) x.tbl_WarningNotificationLog.tbl_File.CustomerNo.Contains(txtFile.Text.Trim))


        End If

        If cmbReceiver.SelectedIndex <> 0 Then



            If cmbReceiver.SelectedValue = 1 Then
                lnqWarningNotificationLogDetail = lnqWarningNotificationLogDetail.Where(Function(x) x.ToBorrower = 1)
            Else
                lnqWarningNotificationLogDetail = lnqWarningNotificationLogDetail.Where(Function(x) x.ToBorrower = 0)

            End If

        End If

        If cmbNotification.SelectedIndex <> 0 Then


            lnqWarningNotificationLogDetail = lnqWarningNotificationLogDetail.Where(Function(x) x.NotificationTypeID = cmbNotification.SelectedValue)
        End If

        If cmbWarningType.SelectedIndex <> 0 Then

            lnqWarningNotificationLogDetail = lnqWarningNotificationLogDetail.Where(Function(x) x.tbl_WarningNotificationLog.FK_WarningIntervalID = cmbWarningType.SelectedValue)



        End If


        If blnProvince = False Then



            Dim lstBranches As New List(Of Integer)

            For i As Integer = 0 To Request.Form.Keys.Count - 1

                If Request.Form.Keys(i).StartsWith("chklstBranch") = True Then
                    Dim intBranchID As Integer = CInt(Request.Form(i))
                    lstBranches.Add(intBranchID)


                End If

            Next i

            lnqWarningNotificationLogDetail = lnqWarningNotificationLogDetail.Where(Function(x) lstBranches.Contains(x.tbl_WarningNotificationLog.tbl_Loan.FK_BranchID))


        End If


        If txtReciverNo.Text.Trim() <> "" Then


            lnqWarningNotificationLogDetail = lnqWarningNotificationLogDetail.Where(Function(x) x.ReceiverInfo.Contains(txtReciverNo.Text))


        End If



        If rdbReportType.SelectedValue = 0 Then
            'Summary

            Dim lnqWarningNotificationLogDetailGroupCount = lnqWarningNotificationLogDetail.GroupBy(Function(x) x.tbl_WarningNotificationLog.tbl_Loan.tbl_Branch.BranchName).Count

            Return lnqWarningNotificationLogDetailGroupCount


        Else 'Detail

            Dim lnqWarningNotificationLogDetailCount = lnqWarningNotificationLogDetail.Count

            Return lnqWarningNotificationLogDetailCount


        End If






    End Function


    Private Sub GenerateWarningListReport()


        Dim dtFromDate As Date = Bootstrap_PersianDateTimePicker_From.GergorainDateTime
        Dim dteToDate As Date = Bootstrap_PersianDateTimePicker_To.GergorainDateTime


        Dim ctxMehr As New BusinessObject.dbMehrVosulEntities1
        Dim lnqWarningNotificationLogDetail = ctxMehr.tbl_WarningNotificationLogDetail.Where(Function(x) x.tbl_WarningNotificationLog.STime >= dtFromDate AndAlso x.tbl_WarningNotificationLog.STime <= dteToDate)


        tblResult.Visible = False
        tblResult2.Visible = False
        Bootstrap_Panel1.ClearMessage()


        Dim blnProvince As Boolean = False

        If cmbProvince.SelectedValue <> -1 And chkBranchSelectAll.Checked = True Then
            lnqWarningNotificationLogDetail = lnqWarningNotificationLogDetail.Where(Function(x) x.tbl_WarningNotificationLog.tbl_Loan.tbl_Branch.Fk_ProvinceID = cmbProvince.SelectedValue)
            blnProvince = True
        End If


        If txtFile.Text.Trim() <> "" Then

            lnqWarningNotificationLogDetail = lnqWarningNotificationLogDetail.Where(Function(x) x.tbl_WarningNotificationLog.tbl_File.CustomerNo.Contains(txtFile.Text.Trim))


        End If

        If cmbReceiver.SelectedIndex <> 0 Then

            If cmbReceiver.SelectedValue = 1 Then
                lnqWarningNotificationLogDetail = lnqWarningNotificationLogDetail.Where(Function(x) x.ToBorrower = 1)
            Else
                lnqWarningNotificationLogDetail = lnqWarningNotificationLogDetail.Where(Function(x) x.ToBorrower = 0)

            End If

        End If

        If cmbNotification.SelectedIndex <> 0 Then


            lnqWarningNotificationLogDetail = lnqWarningNotificationLogDetail.Where(Function(x) x.NotificationTypeID = cmbNotification.SelectedValue)
        End If

        If cmbWarningType.SelectedIndex <> 0 Then

            lnqWarningNotificationLogDetail = lnqWarningNotificationLogDetail.Where(Function(x) x.tbl_WarningNotificationLog.FK_WarningIntervalID = cmbWarningType.SelectedValue)



        End If


        If blnProvince = False Then


            Dim lstBranches As New List(Of Integer)

            For i As Integer = 0 To Request.Form.Keys.Count - 1

                If Request.Form.Keys(i).StartsWith("chklstBranch") = True Then
                    Dim intBranchID As Integer = CInt(Request.Form(i))
                    lstBranches.Add(intBranchID)


                End If

            Next i

            lnqWarningNotificationLogDetail = lnqWarningNotificationLogDetail.Where(Function(x) lstBranches.Contains(x.tbl_WarningNotificationLog.tbl_Loan.FK_BranchID))

        End If


        If txtReciverNo.Text.Trim() <> "" Then



            lnqWarningNotificationLogDetail = lnqWarningNotificationLogDetail.Where(Function(x) x.ReceiverInfo.Contains(txtReciverNo.Text))


        End If



        If rdbReportType.SelectedValue = 0 Then
            'Summary

            tblResult.Visible = False
            tblResult2.Visible = True

            Dim intCurrentPage As Integer = CInt(Session("CurrentPage"))
            Dim intFromRecord As Integer = (25 * (intCurrentPage - 1))


            Dim lnqWarningNotificationLogDetailGroup = lnqWarningNotificationLogDetail.GroupBy(Function(x) x.tbl_WarningNotificationLog.tbl_Loan.tbl_Branch.BranchName).OrderBy(Function(x) x.Key).Skip(intFromRecord).Take(25)
            Dim lnqWarningNotificationLogDetailGroupList = lnqWarningNotificationLogDetailGroup.ToList()

            If lnqWarningNotificationLogDetailGroupList.Count > 0 Then


                ''get total report
                Dim intCount As Integer = intFromRecord
                For Each lnqWarningNotificationLogDetailListGroupItem In lnqWarningNotificationLogDetailGroupList

                    intCount += 1
                    Dim TbRow As New HtmlTableRow
                    Dim TbCell As HtmlTableCell

                    TbCell = New HtmlTableCell
                    TbCell.InnerHtml = CStr(intCount)
                    TbCell.Align = "center"
                    TbCell.NoWrap = True
                    TbRow.Cells.Add(TbCell)


                    TbCell = New HtmlTableCell
                    TbCell.InnerHtml = lnqWarningNotificationLogDetailListGroupItem.Key
                    TbCell.NoWrap = True
                    TbCell.Align = "center"
                    TbCell.Attributes.Add("dir", "rtl")
                    TbRow.Cells.Add(TbCell)

                    TbCell = New HtmlTableCell
                    Dim intTotalCount As Integer = lnqWarningNotificationLogDetailListGroupItem.Count

                    TbCell.InnerHtml = intTotalCount.ToString("n0")
                    TbCell.NoWrap = True
                    TbCell.Align = "center"
                    TbRow.Cells.Add(TbCell)


                    tblResult2.Rows.Add(TbRow)


                Next

                divResult.Visible = True
                tblResult2.Visible = True

                '' Session("WarningNotificationReport") = dtblReport
                ViewState("WarningNotificationReportType") = "1"

            Else

                divResult.Visible = False

            End If



        Else
            'Detail
            Dim intCurrentPage As Integer = CInt(Session("CurrentPage"))
            Dim intFromRecord As Integer = (25 * (intCurrentPage - 1))
            lnqWarningNotificationLogDetail = lnqWarningNotificationLogDetail.OrderBy(Function(x) x.tbl_WarningNotificationLog.STime).Skip(intFromRecord).Take(25)


            Dim lnqWarningNotificationLogDetailList = lnqWarningNotificationLogDetail.ToList()

            If lnqWarningNotificationLogDetailList.Count > 0 Then


                tblResult.Visible = True
                tblResult2.Visible = False

                ''get total report
                Dim intCount As Integer = intFromRecord
                For Each lnqWarningNotificationLogDetailListItem In lnqWarningNotificationLogDetailList

                    intCount += 1
                    Dim TbRow As New HtmlTableRow
                    Dim TbCell As HtmlTableCell

                    TbCell = New HtmlTableCell
                    TbCell.InnerHtml = "<input type='hidden' id='hdnAmounts" & CStr(intCount) & "' >" & CStr(intCount)
                    TbCell.Align = "center"
                    TbCell.NoWrap = True
                    TbRow.Cells.Add(TbCell)


                    TbCell = New HtmlTableCell
                    TbCell.InnerHtml = mdlGeneral.GetPersianDate(lnqWarningNotificationLogDetailListItem.tbl_WarningNotificationLog.theDay)
                    TbCell.NoWrap = True
                    TbCell.Align = "center"
                    TbCell.Attributes.Add("dir", "rtl")
                    TbRow.Cells.Add(TbCell)

                    TbCell = New HtmlTableCell
                    TbCell.InnerHtml = lnqWarningNotificationLogDetailListItem.tbl_WarningNotificationLog.tbl_WarningIntervals.FromDay.ToString() & "-" & lnqWarningNotificationLogDetailListItem.tbl_WarningNotificationLog.tbl_WarningIntervals.ToDay.ToString()
                    TbCell.NoWrap = True
                    TbCell.Align = "center"
                    TbRow.Cells.Add(TbCell)


                    TbCell = New HtmlTableCell

                    Dim intNotificationTypeID As Integer = lnqWarningNotificationLogDetailListItem.NotificationTypeID
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

                    End Select


                    TbCell.InnerHtml = strNotification
                    TbCell.NoWrap = True
                    TbCell.Align = "center"
                    TbRow.Cells.Add(TbCell)

                    TbCell = New HtmlTableCell
                    TbCell.InnerHtml = lnqWarningNotificationLogDetailListItem.tbl_WarningNotificationLog.tbl_Loan.LoanNumber
                    TbCell.NoWrap = True
                    TbCell.Align = "center"
                    TbRow.Cells.Add(TbCell)


                    TbCell = New HtmlTableCell
                    TbCell.InnerHtml = lnqWarningNotificationLogDetailListItem.tbl_WarningNotificationLog.tbl_File.CustomerNo
                    TbCell.NoWrap = False
                    TbCell.Align = "center"
                    TbRow.Cells.Add(TbCell)

                    TbCell = New HtmlTableCell
                    TbCell.InnerHtml = lnqWarningNotificationLogDetailListItem.strMessage
                    TbCell.NoWrap = False
                    TbCell.Align = "center"
                    TbRow.Cells.Add(TbCell)


                    TbCell = New HtmlTableCell
                    TbCell.InnerHtml = mdlGeneral.GetPersianDate(lnqWarningNotificationLogDetailListItem.SendDate)

                    TbCell.NoWrap = True
                    TbCell.Align = "center"
                    TbCell.Attributes.Add("dir", "rtl")
                    TbRow.Cells.Add(TbCell)


                    TbCell = New HtmlTableCell
                    TbCell.InnerHtml = lnqWarningNotificationLogDetailListItem.tbl_WarningNotificationLog.tbl_Loan.tbl_Branch.BrnachCode & "(" & lnqWarningNotificationLogDetailListItem.tbl_WarningNotificationLog.tbl_Loan.tbl_Branch.BranchName & ")"
                    TbCell.NoWrap = True
                    TbCell.Align = "center"
                    TbRow.Cells.Add(TbCell)


                    TbCell = New HtmlTableCell
                    Select Case lnqWarningNotificationLogDetailListItem.SendStatus
                        Case 1
                            TbCell.InnerHtml = "ارسال نشده"
                        Case 2
                            TbCell.InnerHtml = "ارسال شده"
                        Case 3
                            TbCell.InnerHtml = "رسیده به گوشی"
                        Case 4
                            TbCell.InnerHtml = "خطا در ارسال"
                        Case 5
                            TbCell.InnerHtml = "صادر شده"
                        Case 6
                            TbCell.InnerHtml = "ارسال شده"
                        Case 7
                            TbCell.InnerHtml = "تحویل شده"
                        Case 8
                            TbCell.InnerHtml = "بدون پاسخ"
                        Case 9
                            TbCell.InnerHtml = "پاسخ داده شده"
                    End Select
                    TbCell.NoWrap = True
                    TbCell.Align = "center"
                    TbRow.Cells.Add(TbCell)

                    tblResult.Rows.Add(TbRow)


                Next



                Session("WarningNotificationReport") = lnqWarningNotificationLogDetailList
                ViewState("WarningNotificationReportType") = "1"

            Else

                divResult.Visible = False

            End If

        End If


    End Sub



    'Protected Sub cmbBranch_DataBound(sender As Object, e As EventArgs) Handles cmbBranch.DataBound
    '    Dim li As New ListItem
    '    li.Text = "(همه)"
    '    li.Value = -1
    '    cmbBranch.Items.Insert(0, li)
    'End Sub

    'Protected Sub cmbProvince_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbProvince.SelectedIndexChanged


    '    odsBranch.SelectParameters.Item("Action").DefaultValue = 2
    '    odsBranch.SelectParameters.Item("ProvinceID").DefaultValue = cmbProvince.SelectedValue
    '    odsBranch.DataBind()

    '    cmbBranch.DataSourceID = "odsBranch"
    '    cmbBranch.DataTextField = "BrnachCode"
    '    cmbBranch.DataValueField = "ID"


    '    cmbBranch.DataBind()

    'End Sub

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
                'Select Case column.ColumnName

                '    Case "BranchName"

                '        headerValues.Add(QuoteValue("[نام شعبه]"))

                '    Case "SMSCount"

                '        headerValues.Add(QuoteValue("[تعداد]"))


                '    Case "theDay"
                '        headerValues.Add(QuoteValue("[روز]"))


                '    Case "CustomerNo"

                '        headerValues.Add(QuoteValue("[مشتری]"))

                '    Case "strMessage"

                '        headerValues.Add(QuoteValue("[متن ارسالی]"))

                '    Case "SentLogTime"

                '        headerValues.Add(QuoteValue("[زمان ارسال]"))

                '    Case "SendStatus"

                '        headerValues.Add(QuoteValue("[وضعیت]"))

                '    Case "NotificationType"

                '        headerValues.Add(QuoteValue("[نوع اطلاع رسانی]"))
                '    Case "LoanNumber"

                '        headerValues.Add(QuoteValue("[وام]"))

                '    Case "Branch"

                '        headerValues.Add(QuoteValue("[شعبه]"))

                '    Case "Interval"

                '        headerValues.Add(QuoteValue("[بازه]"))
                'End Select
                headerValues.Add(QuoteValue(column.ColumnName))
            Next
            writer.WriteLine(String.Join(",", headerValues.ToArray))
        End If
        Dim items() As String = Nothing
        ReDim items(sourceTable.Columns.Count - 1)
        For Each row As DataRow In sourceTable.Rows
            For i As Integer = 0 To sourceTable.Columns.Count - 1
                If i = 0 OrElse i = 3 Then
                    items(i) = mdlGeneral.GetPersianDate(CDate(row.Item(i)))
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

    Private Function QuoteValue(ByVal value As String) As String
        Return String.Concat("""", value.Replace("""", """"""), """")
    End Function

    Private Sub btnLastPage_ServerClick(sender As Object, e As EventArgs) Handles btnLastPage.ServerClick



        Dim intTotalPage As Integer = Session("TotalPage")

        If intTotalPage = 0 Then
            Return
        End If
        Session("CurrentPage") = intTotalPage
        Call GenerateWarningListReport()
        txtPageCounter.Value = Session("CurrentPage")

    End Sub

    Private Sub btnFirstPage_ServerClick(sender As Object, e As EventArgs) Handles btnFirstPage.ServerClick

        Dim intTotalPage As Integer = Session("TotalPage")

        If intTotalPage = 0 Then
            Return
        End If

        Dim intCurrentPage As Integer = 1
        Session("CurrentPage") = 1
        Call GenerateWarningListReport()
        txtPageCounter.Value = Session("CurrentPage")


    End Sub


    Private Sub btnPreviousPage_ServerClick(sender As Object, e As EventArgs) Handles btnPreviousPage.ServerClick
        Dim intTotalPage As Integer = Session("TotalPage")

        If intTotalPage = 0 Then
            Return
        End If

        Dim intCurrentPage As Integer = CInt(Session("CurrentPage"))
        If intCurrentPage > 1 Then
            intCurrentPage -= 1
        End If
        Session("CurrentPage") = CObj(intCurrentPage)
        Call GenerateWarningListReport()
        txtPageCounter.Value = Session("CurrentPage")
    End Sub

    Private Sub btnNextPage_ServerClick(sender As Object, e As EventArgs) Handles btnNextPage.ServerClick

        Dim intTotalPage As Integer = Session("TotalPage")

        If intTotalPage = 0 Then
            Return
        End If


        Dim intCurrentPage As Integer = CInt(Session("CurrentPage"))
        If intCurrentPage < intTotalPage Then
            intCurrentPage += 1
        End If
        Session("CurrentPage") = CObj(intCurrentPage)
        Call GenerateWarningListReport()

        txtPageCounter.Value = Session("CurrentPage")
    End Sub

    Private Sub btnGoPage_ServerClick(sender As Object, e As EventArgs) Handles btnGoPage.ServerClick

        Dim intTotalPage As Integer = Session("TotalPage")

        If intTotalPage = 0 Then
            Return
        End If

        Dim intCurrentPage As Integer

        If CInt(txtPageCounter.Value) < 1 Then

            intCurrentPage = 1
            txtPageCounter.Value = 1

        ElseIf CInt(txtPageCounter.Value) > CInt(Session("TotalPage")) Then
            intCurrentPage = CInt(Session("TotalPage"))

        ElseIf CInt(txtPageCounter.Value) > CInt(Session("CurrentPage")) Then
            intCurrentPage = intCurrentPage + CInt(txtPageCounter.Value)

        ElseIf CInt(txtPageCounter.Value) < CInt(Session("CurrentPage")) Then
            intCurrentPage = intCurrentPage - CInt(txtPageCounter.Value)

        End If




        Session("CurrentPage") = CObj(intCurrentPage)
        Call GenerateWarningListReport()

        txtPageCounter.Value = Session("CurrentPage")

    End Sub




End Class