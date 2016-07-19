Public Class SystemLoginLogReport
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
        Bootstrap_Panel1.CanExcel = False
        Bootstrap_Panel1.Enable_Display_Client_Validate = True
        Bootstrap_Panel1.Enable_Excel_Client_Validate = True


        lblInnerPageTitle.Text = "پرکردن کادرهای قرمز رنگ، اجباری است."

        If Page.IsPostBack = False Then

            Dim dtblUserLogin As BusinessObject.dstUser.spr_User_Login_SelectDataTable = CType(Session("dtblUserLogin"), BusinessObject.dstUser.spr_User_Login_SelectDataTable)
            Dim drwUserLogin As BusinessObject.dstUser.spr_User_Login_SelectRow = dtblUserLogin.Rows(0)


            Bootstrap_PersianDateTimePicker_From.GergorainDateTime = Date.Now.AddDays(-7).Date
            Bootstrap_PersianDateTimePicker_To.GergorainDateTime = Date.Now

            Bootstrap_PersianDateTimePicker_From.PickerLabel = "از"
            Bootstrap_PersianDateTimePicker_To.PickerLabel = "تا"


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

    Private Sub GetReport()


        Dim tadpReport As New BusinessObject.dstLoginLogTableAdapters.spr_SystemLoginLog_ReportTableAdapter
        Dim dtblReport As BusinessObject.dstLoginLog.spr_SystemLoginLog_ReportDataTable = Nothing

        Dim dtFromDate As Date = Bootstrap_PersianDateTimePicker_From.GergorainDateTime
        Dim dteToDate As Date = Bootstrap_PersianDateTimePicker_To.GergorainDateTime

        Dim blnProvince As Boolean = False
        Dim strWhere As String = " Vosul.tbl_SystemLoginLog.LoginDateTime  between " & "convert(datetime,'" & dtFromDate & "')   and  convert(datetime,'" & dteToDate.Date & "')"


        If cmbProvince.SelectedValue <> -1 And chkBranchSelectAll.Checked = True Then
            blnProvince = True
            strWhere = strWhere & " and Vosul.tbl_Branch.Fk_ProvinceID = " & cmbProvince.SelectedValue

        End If

        If blnProvince = False Then
            Dim blnHasBranch As Boolean = False
            Dim strBranchwhere As String = ""

            For i As Integer = 0 To Request.Form.Keys.Count - 1

                If Request.Form.Keys(i).StartsWith("chklstBranch") = True Then

                    blnHasBranch = True
                    If strBranchwhere <> "" Then
                        strBranchwhere = strBranchwhere & " or " & "Administration.tbl_User.FK_BrnachID = " & Request.Form(i)
                    Else
                        strBranchwhere = "  (Administration.tbl_User.FK_BrnachID= " & Request.Form(i)
                    End If

                End If

            Next

            If strBranchwhere <> "" Then

                strWhere = strWhere & " and " & strBranchwhere & ")"

            End If

        End If

       
        dtblReport = tadpReport.GetData(2, dtFromDate, dteToDate.AddDays(1), strWhere)


        If dtblReport.Rows.Count > 0 Then

            divResult.Visible = True

            ''get total report
            Dim intCount As Integer = 0



            For Each drwReport As BusinessObject.dstLoginLog.spr_SystemLoginLog_ReportRow In dtblReport.Rows

                intCount += 1
                Dim TbRow As New HtmlTableRow
                Dim TbCell As HtmlTableCell

                TbCell = New HtmlTableCell
                TbCell.InnerHtml = CStr(intCount)
                TbCell.Align = "center"
                TbCell.NoWrap = True
                TbRow.Cells.Add(TbCell)


                TbCell = New HtmlTableCell
                TbCell.InnerHtml = drwReport.Username
                TbCell.NoWrap = True
                TbCell.Align = "center"
                TbRow.Cells.Add(TbCell)


                TbCell = New HtmlTableCell
                TbCell.InnerHtml = drwReport.Name
                TbCell.NoWrap = True
                TbCell.Align = "center"
                TbRow.Cells.Add(TbCell)


                TbCell = New HtmlTableCell
                TbCell.InnerHtml = drwReport.BranchName
                TbCell.NoWrap = True
                TbCell.Align = "center"
                TbRow.Cells.Add(TbCell)


                TbCell = New HtmlTableCell
                TbCell.InnerHtml = If(drwReport.IsPartTime = True, "پاره وقت", "تمام وقت")
                TbCell.NoWrap = True
                TbCell.Align = "center"
                TbRow.Cells.Add(TbCell)


                TbCell = New HtmlTableCell
                TbCell.InnerHtml = mdlGeneral.GetPersianDateTime(drwReport.LoginDateTime)
                TbCell.NoWrap = True
                TbCell.Align = "center"
                TbRow.Cells.Add(TbCell)




                tblResult.Rows.Add(TbRow)
            Next

            Session("LogCurrentLCStatusReport") = dtblReport

        Else

            divResult.Visible = False


        End If
    End Sub

    Private Sub Bootstrap_Panel1_Panel_Display_Click(sender As Object, e As System.EventArgs) Handles Bootstrap_Panel1.Panel_Display_Click

        GetReport()

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
End Class