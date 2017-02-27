Imports System.IO
Public Class HandyFollowReport
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
        Bootstrap_Panel1.Enable_Save_Client_Validate = True

        lblInnerPageTitle.Text = "پرکردن کادرهای قرمز رنگ، اجباری است."

        If Page.IsPostBack = False Then

            Dim dtblUserLogin As BusinessObject.dstUser.spr_User_Login_SelectDataTable = CType(Session("dtblUserLogin"), BusinessObject.dstUser.spr_User_Login_SelectDataTable)
            Dim drwUserLogin As BusinessObject.dstUser.spr_User_Login_SelectRow = dtblUserLogin.Rows(0)


            Bootstrap_PersianDateTimePicker_From.GergorainDateTime = Date.Now.AddDays(-3).Date
            Bootstrap_PersianDateTimePicker_To.GergorainDateTime = Date.Now

            Bootstrap_PersianDateTimePicker_From.PickerLabel = "از"
            Bootstrap_PersianDateTimePicker_To.PickerLabel = "تا"



            If drwUserLogin.IsDataAdmin = True Then

                odsBranch.SelectParameters.Item("Action").DefaultValue = 1
                odsBranch.SelectParameters.Item("ProvinceID").DefaultValue = -1
                odsBranch.DataBind()

                cmbBranch.DataSourceID = "odsBranch"
                cmbBranch.DataTextField = "BrnachCode"
                cmbBranch.DataValueField = "ID"


                cmbBranch.DataBind()

                odsPerson.SelectParameters.Item("Action").DefaultValue = 2
                odsPerson.SelectParameters.Item("BranchID").DefaultValue = -1
                odsPerson.SelectParameters.Item("ProvinceID").DefaultValue = -1


                cmbPerson.DataBind()


            ElseIf drwUserLogin.IsDataAdmin = False And drwUserLogin.IsDataUserAdmin = False Then

                cmbProvince.SelectedValue = drwUserLogin.Fk_ProvinceID

                odsBranch.SelectParameters.Item("Action").DefaultValue = 2
                odsBranch.SelectParameters.Item("ProvinceID").DefaultValue = drwUserLogin.Fk_ProvinceID
                odsBranch.DataBind()

                cmbBranch.DataSourceID = "odsBranch"
                cmbBranch.DataTextField = "BrnachCode"
                cmbBranch.DataValueField = "ID"


                cmbBranch.DataBind()

                cmbBranch.SelectedValue = drwUserLogin.FK_BrnachID
                cmbBranch.Enabled = False
                cmbProvince.Enabled = False

                odsPerson.SelectParameters.Item("Action").DefaultValue = 1
                odsPerson.SelectParameters.Item("BranchID").DefaultValue = drwUserLogin.FK_BrnachID
                odsPerson.SelectParameters.Item("ProvinceID").DefaultValue = -1

                cmbPerson.DataBind()



            ElseIf drwUserLogin.IsDataAdmin = False And drwUserLogin.IsDataUserAdmin = True Then

                cmbProvince.SelectedValue = drwUserLogin.Fk_ProvinceID

                odsBranch.SelectParameters.Item("Action").DefaultValue = 2
                odsBranch.SelectParameters.Item("ProvinceID").DefaultValue = drwUserLogin.Fk_ProvinceID
                odsBranch.DataBind()

                cmbBranch.DataSourceID = "odsBranch"
                cmbBranch.DataTextField = "BrnachCode"
                cmbBranch.DataValueField = "ID"


                cmbBranch.DataBind()
                cmbBranch.SelectedValue = drwUserLogin.FK_BrnachID

                odsPerson.SelectParameters.Item("Action").DefaultValue = 1
                odsPerson.SelectParameters.Item("BranchID").DefaultValue = cmbBranch.SelectedValue
                odsPerson.SelectParameters.Item("ProvinceID").DefaultValue = -1

                cmbPerson.DataBind()

                cmbProvince.Enabled = False



            End If







        End If



        Bootstrap_PersianDateTimePicker_From.ShowTimePicker = True
        Bootstrap_PersianDateTimePicker_To.ShowTimePicker = True

    End Sub




    Private Sub Bootstrap_Panel1_Panel_Display_Click(sender As Object, e As System.EventArgs) Handles Bootstrap_Panel1.Panel_Display_Click

        Bootstrap_Panel1.ClearMessage()

        Dim tadpReport As New BusinessObject.dstReportTableAdapters.spr_Report_HandyFollow_SelectTableAdapter
        Dim dtblReport As BusinessObject.dstReport.spr_Report_HandyFollow_SelectDataTable = Nothing

        Dim dtFromDate As Date = Bootstrap_PersianDateTimePicker_From.GergorainDateTime
        Dim dteToDate As Date = Bootstrap_PersianDateTimePicker_To.GergorainDateTime


        Dim strWhere As String = ""

        If cmbBranch.SelectedIndex <> 0 Then

            If strWhere <> "" Then

                strWhere = strWhere & " and "

            End If

            strWhere = strWhere & " Vosul.tbl_Loan.FK_BranchID = " & cmbBranch.SelectedValue


        ElseIf cmbProvince.SelectedIndex <> 0 Then

            If strWhere <> "" Then

                strWhere = strWhere & " and "

            End If

            strWhere = strWhere & " Vosul.tbl_Branch.FK_ProvinceID= " & cmbProvince.SelectedValue


        End If

        If cmbPerson.SelectedIndex <> 0 Then

            If strWhere <> "" Then

                strWhere = strWhere & " and "


            End If

            strWhere = strWhere & " Vosul.tbl_HandyFollow.FK_UserID =" & cmbPerson.SelectedValue

        End If

        If cmbNotification.SelectedIndex <> 0 Then

            If strWhere <> "" Then

                strWhere = strWhere & " and "

            End If

            strWhere = strWhere & "  Vosul.tbl_HandyFollow.NotificationTypeID = " & cmbNotification.SelectedValue


        End If


        If cmbStatus.SelectedIndex <> 0 Then

            If strWhere <> "" Then

                strWhere = strWhere & " and "

            End If

            strWhere = strWhere & "  Vosul.tbl_HandyFollow.Answered = " & If(cmbStatus.SelectedValue = 1, "1", "0")


        End If


        If cmbSuccess.SelectedIndex <> 0 Then

            If strWhere <> "" Then

                strWhere = strWhere & " and "

            End If

            strWhere = strWhere & "  Vosul.tbl_HandyFollow.IsSuccess = " & If(cmbSuccess.SelectedValue = 1, "1", "0")


        End If

        If strWhere = "" Then

            dtblReport = tadpReport.GetData(1, dtFromDate, dteToDate, "")

        Else

            strWhere = strWhere & " and Vosul.tbl_HandyFollow.ContactDate between " & "convert(datetime,'" & dtFromDate & "')   and  convert(datetime,'" & dteToDate.Date & " 23:00:00 ')"

            dtblReport = tadpReport.GetData(2, dtFromDate, dteToDate, strWhere)

        End If


        If dtblReport.Rows.Count > 0 Then

            divResult.Visible = True

            ''get total report
            Dim intCount As Integer = 0



            For Each drwReport As BusinessObject.dstReport.spr_Report_HandyFollow_SelectRow In dtblReport.Rows

                intCount += 1
                Dim TbRow As New HtmlTableRow
                Dim TbCell As HtmlTableCell

                TbCell = New HtmlTableCell
                TbCell.InnerHtml = "<input type='hidden' id='hdnAmounts" & CStr(intCount) & "' >" & CStr(intCount)
                TbCell.Align = "center"
                TbCell.NoWrap = True
                TbRow.Cells.Add(TbCell)


                TbCell = New HtmlTableCell
                TbCell.InnerHtml = mdlGeneral.GetPersianDateTime(drwReport.ContactDate)
                TbCell.NoWrap = True
                TbCell.Align = "center"
                TbRow.Cells.Add(TbCell)

                TbCell = New HtmlTableCell
                TbCell.InnerHtml = drwReport.NotificationType
                TbCell.NoWrap = True
                TbCell.Align = "center"
                TbRow.Cells.Add(TbCell)


                TbCell = New HtmlTableCell
                TbCell.InnerHtml = drwReport.FileName & If(drwReport.ToSponsor = True, "-ضامن", "-وام گیرنده")
                TbCell.NoWrap = False
                TbCell.Align = "center"
                TbRow.Cells.Add(TbCell)

                TbCell = New HtmlTableCell
                TbCell.InnerHtml = If(drwReport.Answered = True, "باپاسخ", "بدون پاسخ")
                TbCell.NoWrap = True
                TbCell.Align = "center"
                TbRow.Cells.Add(TbCell)


                TbCell = New HtmlTableCell
                TbCell.InnerHtml = If(drwReport.IsSuccess = True, "موفق", "ناموفق")
                TbCell.NoWrap = True
                TbCell.Align = "center"
                TbRow.Cells.Add(TbCell)


                TbCell = New HtmlTableCell
                TbCell.InnerHtml = drwReport.Username
                TbCell.NoWrap = True
                TbCell.Align = "center"
                TbRow.Cells.Add(TbCell)

                TbCell = New HtmlTableCell
                TbCell.InnerHtml = drwReport.Branch
                TbCell.NoWrap = True
                TbCell.Align = "center"
                TbRow.Cells.Add(TbCell)


                TbCell = New HtmlTableCell
                TbCell.InnerHtml = drwReport.Remarks
                TbCell.NoWrap = False
                TbCell.Align = "center"
                TbRow.Cells.Add(TbCell)


                TbCell = New HtmlTableCell
                TbCell.InnerHtml = If(drwReport.IsDutyDateNull = True, "", mdlGeneral.GetPersianDate(drwReport.DutyDate))
                TbCell.NoWrap = False
                TbCell.Align = "center"
                TbRow.Cells.Add(TbCell)


                tblResult.Rows.Add(TbRow)

            Next

            Session("HandyFollowReport") = dtblReport

        Else

            divResult.Visible = False


        End If


    End Sub

    Private Sub Bootstrap_Panel1_Panel_Excel_Click(sender As Object, e As System.EventArgs) Handles Bootstrap_Panel1.Panel_Excel_Click

        Try
            If Session("HandyFollowReport") IsNot Nothing Then
                Dim dtblUserLogin As BusinessObject.dstUser.spr_User_Login_SelectDataTable = CType(Session("dtblUserLogin"), BusinessObject.dstUser.spr_User_Login_SelectDataTable)
                Dim drwUserLogin As BusinessObject.dstUser.spr_User_Login_SelectRow = dtblUserLogin.Rows(0)


                Dim strPath As String = Server.MapPath("") & "\TempFile\" & drwUserLogin.ID & "\"
                Dim FileName As String = "HandyFollowReport-" & drwUserLogin.ID.ToString()

                Dim tblCSVResult As DataTable = Session("HandyFollowReport")
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
                    Session("HandyFollowReport") = Nothing
                End If
            Else
                Bootstrap_Panel1.ShowMessage("امکان انتقال گزارش به فایل اکسل وجود ندارد", False)
                Session("HandyFollowReport") = Nothing
            End If
        Catch ex As Exception
            Bootstrap_Panel1.ShowMessage("امکان انتقال گزارش به فایل اکسل وجود ندارد", False)
            Session("HandyFollowReport") = Nothing
        End Try



    End Sub

    Protected Sub cmbBranch_DataBound(sender As Object, e As EventArgs) Handles cmbBranch.DataBound
        Dim li As New ListItem
        li.Text = "(همه)"
        li.Value = -1
        cmbBranch.Items.Insert(0, li)
    End Sub

    Protected Sub cmbBranch_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbBranch.SelectedIndexChanged

        odsPerson.SelectParameters.Item("Action").DefaultValue = 1
        odsPerson.SelectParameters.Item("BranchID").DefaultValue = CInt(cmbBranch.SelectedValue)
        odsPerson.SelectParameters.Item("ProvinceID").DefaultValue = -1


        cmbPerson.DataBind()

    End Sub

    Protected Sub cmbPerson_DataBound(sender As Object, e As EventArgs) Handles cmbPerson.DataBound
        Dim dtblUserLogin As BusinessObject.dstUser.spr_User_Login_SelectDataTable = CType(Session("dtblUserLogin"), BusinessObject.dstUser.spr_User_Login_SelectDataTable)
        Dim drwUserLogin As BusinessObject.dstUser.spr_User_Login_SelectRow = dtblUserLogin.Rows(0)

        If drwUserLogin.IsDataAdmin = False AndAlso drwUserLogin.IsDataUserAdmin = False Then

            cmbPerson.SelectedValue = drwUserLogin.ID
            cmbPerson.Enabled = False
        Else

            Dim li As New ListItem
            li.Text = "(همه)"
            li.Value = -1
            cmbPerson.Items.Insert(0, li)

        End If

    End Sub

    Protected Sub cmbProvince_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbProvince.SelectedIndexChanged
        odsBranch.SelectParameters.Item("Action").DefaultValue = 2
        odsBranch.SelectParameters.Item("ProvinceID").DefaultValue = cmbProvince.SelectedValue
        odsBranch.DataBind()

        cmbBranch.DataSourceID = "odsBranch"
        cmbBranch.DataTextField = "BrnachCode"
        cmbBranch.DataValueField = "ID"

        cmbBranch.DataBind()

        odsPerson.SelectParameters.Item("Action").DefaultValue = 3
        odsPerson.SelectParameters.Item("BranchID").DefaultValue = -1
        odsPerson.SelectParameters.Item("ProvinceID").DefaultValue = cmbProvince.SelectedValue

        cmbPerson.DataBind()


    End Sub

    Protected Sub cmbProvince_DataBound(sender As Object, e As EventArgs) Handles cmbProvince.DataBound
        Dim li As New ListItem
        li.Text = "(همه استان ها)"
        li.Value = -1
        cmbProvince.Items.Insert(0, li)
    End Sub
End Class