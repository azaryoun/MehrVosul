Imports System.IO
Public Class CurrentLCStatusMaxLoanTypeReport
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
        txt_InstallmentCount.Attributes.Add("onkeypress", "return numbersonly(event, false);")
        btnSendSMS.Attributes.Add("onclick", "return btnSenSMS_Click();")
        lblInnerPageTitle.Text = "پرکردن کادرهای قرمز رنگ، اجباری است."

        If Page.IsPostBack = False Then


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


    End Sub



    Private Sub Bootstrap_Panel1_Panel_Display_Click(sender As Object, e As System.EventArgs) Handles Bootstrap_Panel1.Panel_Display_Click

        Try

            Bootstrap_Panel1.ClearMessage()

            Dim tadpReport As New BusinessObject.dstReportTableAdapters.spr_Report_CurrentLCStatus_MaxLoanTypeParameter_SelectTableAdapter
            Dim dtblReport As BusinessObject.dstReport.spr_Report_CurrentLCStatus_MaxLoanTypeParameter_SelectDataTable = Nothing


            Dim intInstallmentCount As Integer = CInt(txt_InstallmentCount.Text)
            Dim strWhere As String = " Vosul.tbl_CurrentLCStatus.NoDelayInstallment= " + intInstallmentCount.ToString
            Dim blnProvince As Boolean = False

            If cmbProvince.SelectedValue <> -1 And chkBranchSelectAll.Checked = True Then
                blnProvince = True
                strWhere = strWhere & " and Vosul.tbl_Branch.Fk_ProvinceID= " & cmbProvince.SelectedValue

            End If

            If blnProvince = False Then
                Dim blnHasBranch As Boolean = False
                Dim strBranchwhere As String = ""

                For i As Integer = 0 To Request.Form.Keys.Count - 1

                    If Request.Form.Keys(i).StartsWith("chklstBranch") = True Then

                        blnHasBranch = True
                        If strBranchwhere <> "" Then
                            strBranchwhere = strBranchwhere & " or " & "Vosul.tbl_Loan.FK_BranchID= " & Request.Form(i)
                        Else
                            strBranchwhere = "  (Vosul.tbl_Loan.FK_BranchID= " & Request.Form(i)
                        End If

                    End If

                Next

                If strBranchwhere <> "" Then

                    strWhere = strWhere & " and " & strBranchwhere & ")"

                End If

            End If
          
            If cmbLoanType.SelectedValue <> -1 Then

                strWhere = strWhere & " and Vosul.tbl_Loan.FK_LoanTypeID= " & cmbLoanType.SelectedValue

            End If

        


            dtblReport = tadpReport.GetData(2, strWhere)


            If dtblReport.Rows.Count > 0 Then

                divResult.Visible = True

                ''get total report
                Dim intCount As Integer = 0

                Dim strchklstMenuLeaves As String = ""
                For Each drwReport As BusinessObject.dstReport.spr_Report_CurrentLCStatus_MaxLoanTypeParameter_SelectRow In dtblReport.Rows

                    intCount += 1
                    Dim TbRow As New HtmlTableRow
                    Dim TbCell As HtmlTableCell


                    strchklstMenuLeaves = "<input type='checkbox' value='" & drwReport.ID & "' name='chklstMenu" & drwReport.MobileNo & "'>"

                    TbCell = New HtmlTableCell
                    TbCell.InnerHtml = strchklstMenuLeaves
                    TbCell.Align = "center"
                    TbCell.NoWrap = True
                    TbRow.Cells.Add(TbCell)



                    TbCell = New HtmlTableCell
                    TbCell.InnerHtml = CStr(intCount)
                    TbCell.Align = "center"
                    TbCell.NoWrap = True
                    TbRow.Cells.Add(TbCell)


                    TbCell = New HtmlTableCell
                    TbCell.InnerHtml = drwReport.CustomerNo
                    TbCell.NoWrap = True
                    TbCell.Align = "center"
                    TbRow.Cells.Add(TbCell)

                    TbCell = New HtmlTableCell
                    TbCell.InnerHtml = drwReport.FName & " " & drwReport.LName
                    TbCell.NoWrap = True
                    TbCell.Align = "center"
                    TbRow.Cells.Add(TbCell)


                    TbCell = New HtmlTableCell
                    TbCell.InnerHtml = drwReport.Loan
                    TbCell.NoWrap = True
                    TbCell.Align = "center"
                    TbRow.Cells.Add(TbCell)


                    TbCell = New HtmlTableCell
                    TbCell.InnerHtml = drwReport.Branch
                    TbCell.NoWrap = True
                    TbCell.Align = "center"
                    TbRow.Cells.Add(TbCell)


                    TbCell = New HtmlTableCell
                    TbCell.InnerHtml = drwReport.MobileNo
                    TbCell.NoWrap = True
                    TbCell.Align = "center"
                    TbRow.Cells.Add(TbCell)


                    TbCell = New HtmlTableCell
                    TbCell.InnerHtml = drwReport.NotPiadDurationDay
                    TbCell.NoWrap = True
                    TbCell.Align = "center"
                    TbRow.Cells.Add(TbCell)


                    TbCell = New HtmlTableCell
                    TbCell.InnerHtml = mdlGeneral.GetPersianDate(drwReport.Date_P)
                    TbCell.NoWrap = True
                    TbCell.Align = "center"
                    TbRow.Cells.Add(TbCell)


                    tblResult.Rows.Add(TbRow)

                Next

                Session("CurrentLCStatusMaxLoanTypeReport") = dtblReport

            Else

                divResult.Visible = False


            End If

        Catch ex As Exception

            Bootstrap_Panel1.ShowMessage(ex.Message, True)

        End Try
    End Sub

    Protected Sub cmbLoanType_DataBound(sender As Object, e As EventArgs) Handles cmbLoanType.DataBound

        Dim li As New ListItem
        li.Text = "(همه انواع وام)"
        li.Value = -1
        cmbLoanType.Items.Insert(0, li)

    End Sub

    Protected Sub btnSendSMS_Click(sender As Object, e As EventArgs) Handles btnSendSMS.Click

        Dim drwSystemSetting As BusinessObject.dstSystemSetting.spr_SystemSetting_SelectRow = Nothing
        Dim tadpSystemSetting As New BusinessObject.dstSystemSettingTableAdapters.spr_SystemSetting_SelectTableAdapter
        Dim dtblSystemSetting As BusinessObject.dstSystemSetting.spr_SystemSetting_SelectDataTable = Nothing
        dtblSystemSetting = tadpSystemSetting.GetData()
        drwSystemSetting = dtblSystemSetting.Rows(0)



        Try

            For i As Integer = 0 To Request.Form.Keys.Count - 1
                If Request.Form.Keys(i).StartsWith("chklstMenu") = True Then

                    Dim intLCStatusID As Integer = CInt(Request.Form(i))

                    Dim tadpLCStatus As New BusinessObject.dstCurrentLCStatusTableAdapters.spr_CurrentLCStatus_SelectTableAdapter
                    Dim dtblLCStaus As BusinessObject.dstCurrentLCStatus.spr_CurrentLCStatus_SelectDataTable = Nothing
                    dtblLCStaus = tadpLCStatus.GetData(intLCStatusID)
                    Dim drwLCStaus As BusinessObject.dstCurrentLCStatus.spr_CurrentLCStatus_SelectRow = dtblLCStaus.Rows(0)


                    Dim tadpWarningIntervalCheck As New BusinessObject.dstWarningIntervalsTableAdapters.spr_WarningIntervals_Check_SelectTableAdapter
                    Dim dtblWarningIntervalCheck As BusinessObject.dstWarningIntervals.spr_WarningIntervals_Check_SelectDataTable = Nothing
                    dtblWarningIntervalCheck = tadpWarningIntervalCheck.GetData(drwLCStaus.FK_LoanTypeID, drwLCStaus.NotPiadDurationDay, drwLCStaus.LCBalance, drwLCStaus.FK_BranchID)

                    If dtblWarningIntervalCheck.Rows.Count = 0 Then Continue For
                    Dim drwWarningIntervalCheck As BusinessObject.dstWarningIntervals.spr_WarningIntervals_Check_SelectRow = dtblWarningIntervalCheck.Rows(0)

                    Dim qryWarningNotificationLog As New BusinessObject.dstWarningNotificationLogTableAdapters.QueriesTableAdapter
                    Dim intWarningNotifcationLogID As Integer = qryWarningNotificationLog.spr_WarningNotificationLog_Insert(drwLCStaus.FK_LoanID, drwLCStaus.FK_FileID, drwWarningIntervalCheck.ID, drwLCStaus.Date_P, 0, Date.Now, True)

                    If drwWarningIntervalCheck.SendSMS = True Then

                        If drwWarningIntervalCheck.ToBorrower = True Then

                            Dim strMessage As String = ""

                            If rdbInterval.Checked = True Then
                                strMessage = CreateMessage(1, drwLCStaus.IsMale, drwLCStaus.FName, drwLCStaus.LName, drwLCStaus.LoanNumber, drwLCStaus.NotPiadDurationDay, False, drwWarningIntervalCheck.ID, drwLCStaus.BranchName)

                            Else
                                strMessage = txtMessage.Text

                            End If
                            
                            Dim qryWarningNotificationLogDetail As New BusinessObject.dstWarningNotificationLogDetailTableAdapters.QueriesTableAdapter
                            qryWarningNotificationLogDetail.spr_WarningNotificationLogDetail_Insert(intWarningNotifcationLogID, drwSystemSetting.GatewayNumber, drwLCStaus.MobileNo, True, strMessage, "ارسال پیامک به صورت دستی از صفحه گزارش تسهیلات معوق", Date.Now, "", 1, 1, Date.Now)


                        End If


                    End If
                End If

            Next

            Bootstrap_Panel1.ShowMessage("ارسال پیامک با موفقیت انجام شد", False)

        Catch ex As Exception

            Bootstrap_Panel1.ShowMessage("در ارسال پیامک خطا رخ داده است", True)


        End Try

      
    End Sub



    Private Function CreateMessage(ByVal intDraftTypeID As Integer, ByVal blnIsMale As Boolean, ByVal strFName As String, ByVal strLName As String, ByVal strLoanFileNo As String, ByVal intNPDuration As Integer, ByVal blnToSponsor As Boolean, ByVal intIntervalID As Integer, strBranchName As String) As String
        Try

            Dim strResult As String = ""

            Dim tadpDraftTextList As New BusinessObject.dstDraftTableAdapters.spr_DraftText_List_SelectTableAdapter
            Dim dtblDrafTextList As BusinessObject.dstDraft.spr_DraftText_List_SelectDataTable = Nothing
            dtblDrafTextList = tadpDraftTextList.GetData(intDraftTypeID, blnToSponsor, intIntervalID)

            For Each drwDraftTextList As BusinessObject.dstDraft.spr_DraftText_List_SelectRow In dtblDrafTextList.Rows

                If drwDraftTextList.IsDynamic = True Then

                    Select Case CInt(drwDraftTextList.DraftText.Trim)
                        Case 1 'Sex
                            If blnIsMale = True Then
                                strResult &= " آقای"
                            Else
                                strResult &= " خانم"
                            End If

                        Case 2 'FName
                            strResult &= " " & strFName
                        Case 3 'LName
                            strResult &= " " & strLName
                        Case 4 'Loan File No
                            strResult &= " " & strLoanFileNo
                        Case 5 'NPDuration
                            strResult &= " " & intNPDuration.ToString
                        Case 6 'Brnach
                            strResult &= " " & strBranchName
                    End Select


                Else
                    strResult &= " " & drwDraftTextList.DraftText
                End If


            Next drwDraftTextList

            Return strResult.Trim
        Catch ex As Exception
            Return ""
        End Try


    End Function

    Private Sub Bootstrap_Panel1_Panel_Excel_Click(sender As Object, e As System.EventArgs) Handles Bootstrap_Panel1.Panel_Excel_Click

        Try
            If Session("CurrentLCStatusMaxLoanTypeReport") IsNot Nothing Then
                Dim dtblUserLogin As BusinessObject.dstUser.spr_User_Login_SelectDataTable = CType(Session("dtblUserLogin"), BusinessObject.dstUser.spr_User_Login_SelectDataTable)
                Dim drwUserLogin As BusinessObject.dstUser.spr_User_Login_SelectRow = dtblUserLogin.Rows(0)


                Dim strPath As String = Server.MapPath("") & "\TempFile\" & drwUserLogin.ID & "\"
                Dim FileName As String = "CurrentLCStatusMaxLoanTypeReport-" & drwUserLogin.ID.ToString()

                Dim tblCSVResult As DataTable = Session("CurrentLCStatusMaxLoanTypeReport")
                If tblCSVResult.Rows.Count > 0 Then
                    If Not System.IO.Directory.Exists(strPath) Then
                        System.IO.Directory.CreateDirectory(strPath)
                    End If

                    Dim clsCSVWriter As New clsCSVWriter
                    Using strWriter As StreamWriter = New StreamWriter(strPath & FileName)
                        clsCSVWriter.WriteDataTable(tblCSVResult, FileName, strWriter, True)
                    End Using
                Else
                    Bootstrap_Panel1.ShowMessage("امکان انتقال گزارش به فایل اکسل وجود ندارد", False)
                    Session("CurrentLCStatusMaxLoanTypeReport") = Nothing
                End If
            Else
                Bootstrap_Panel1.ShowMessage("امکان انتقال گزارش به فایل اکسل وجود ندارد", False)
                Session("CurrentLCStatusMaxLoanTypeReport") = Nothing
            End If
        Catch ex As Exception
            Bootstrap_Panel1.ShowMessage("امکان انتقال گزارش به فایل اکسل وجود ندارد", False)
            Session("CurrentLCStatusMaxLoanTypeReport") = Nothing
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



    Protected Sub rdbInterval_CheckedChanged(sender As Object, e As EventArgs) Handles rdbInterval.CheckedChanged
        txtMessage.Visible = False
    End Sub

    Protected Sub rdbText_CheckedChanged(sender As Object, e As EventArgs) Handles rdbText.CheckedChanged
        txtMessage.Visible = True
    End Sub
End Class