Public Class HandyFollowPersonReport
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

                cmbBranch.SelectedValue = drwUserLogin.FK_BrnachID
                cmbBranch.DataBind()


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

                cmbProvince.Enabled = False
                odsPerson.SelectParameters.Item("Action").DefaultValue = 1
                odsPerson.SelectParameters.Item("BranchID").DefaultValue = cmbBranch.SelectedValue
                odsPerson.SelectParameters.Item("ProvinceID").DefaultValue = -1

                cmbPerson.DataBind()


            End If

        End If



        Bootstrap_PersianDateTimePicker_From.ShowTimePicker = True
        Bootstrap_PersianDateTimePicker_To.ShowTimePicker = True
    End Sub

    Protected Sub cmbBranch_DataBound(sender As Object, e As EventArgs) Handles cmbBranch.DataBound
        Dim li As New ListItem
        li.Text = "(همه)"
        li.Value = -1
        cmbBranch.Items.Insert(0, li)
    End Sub

    Protected Sub cmbProvince_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbProvince.SelectedIndexChanged

        Dim dtblUserLogin As BusinessObject.dstUser.spr_User_Login_SelectDataTable = CType(Session("dtblUserLogin"), BusinessObject.dstUser.spr_User_Login_SelectDataTable)
        Dim drwUserLogin As BusinessObject.dstUser.spr_User_Login_SelectRow = dtblUserLogin.Rows(0)

        If cmbProvince.SelectedValue = -1 Then


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

        Else
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
        End If


    End Sub

    Protected Sub cmbProvince_DataBound(sender As Object, e As EventArgs) Handles cmbProvince.DataBound
        Dim li As New ListItem
        li.Text = "(همه استان ها)"
        li.Value = -1
        cmbProvince.Items.Insert(0, li)
    End Sub

    Private Sub Bootstrap_Panel1_Panel_Display_Click(sender As Object, e As EventArgs) Handles Bootstrap_Panel1.Panel_Display_Click


        Dim dtFromDate As Date = Bootstrap_PersianDateTimePicker_From.GergorainDateTime
        Dim dteToDate As Date = Bootstrap_PersianDateTimePicker_To.GergorainDateTime.AddDays(1)


        Dim tadphandyFollowReport As New BusinessObject.dstHandyFollowTableAdapters.spr_HandyFollow_ReportByUserTableAdapter
        Dim dtblHandyFollowReport As BusinessObject.dstHandyFollow.spr_HandyFollow_ReportByUserDataTable = Nothing
        Dim intReportType As Int16 = 0


        If cmbPerson.SelectedValue <> -1 Then

            dtblHandyFollowReport = tadphandyFollowReport.GetData(4, dtFromDate, dteToDate, -1, -1, cmbPerson.SelectedValue)
            intReportType = 3

        ElseIf cmbBranch.SelectedValue <> -1 Then

            dtblHandyFollowReport = tadphandyFollowReport.GetData(3, dtFromDate, dteToDate, -1, cmbBranch.SelectedValue, -1)
            intReportType = 3
        ElseIf cmbProvince.SelectedValue <> -1 Then
            dtblHandyFollowReport = tadphandyFollowReport.GetData(2, dtFromDate, dteToDate, cmbProvince.SelectedValue, -1, -1)
            intReportType = 2
        Else
            dtblHandyFollowReport = tadphandyFollowReport.GetData(1, dtFromDate, dteToDate, -1, -1, -1)
            intReportType = 1
        End If


        Dim intCount As Integer = 0
        Dim intTotalCount As Integer = 0

        divResult.Visible = False
        divResult2.Visible = False
        divResult3.Visible = False

        If dtblHandyFollowReport.Count > 0 Then

            If intReportType = 1 Then
                divResult.Visible = True
            ElseIf intReportType = 2 Then
                divResult2.Visible = True
            ElseIf intReportType = 3 Then
                divResult3.Visible = True
            End If


        End If

        For Each drwhandyFollow As BusinessObject.dstHandyFollow.spr_HandyFollow_ReportByUserRow In dtblHandyFollowReport


            intCount += 1
            Dim TbRow As New HtmlTableRow
            Dim TbCell As HtmlTableCell

            TbCell = New HtmlTableCell
            TbCell.InnerHtml = CStr(intCount)
            TbCell.Align = "center"
            TbCell.NoWrap = True
            TbRow.Cells.Add(TbCell)


            TbCell = New HtmlTableCell
            TbCell.InnerHtml = drwhandyFollow.ProvinceName
            TbCell.NoWrap = True
            TbCell.Align = "center"
            TbCell.Attributes.Add("dir", "rtl")
            TbRow.Cells.Add(TbCell)

            TbCell = New HtmlTableCell
            TbCell.InnerHtml = drwhandyFollow.FollowingCount.ToString("n0")
            TbCell.NoWrap = True
            TbCell.Align = "center"
            TbRow.Cells.Add(TbCell)
            If intReportType = 1 Then
                tblResult.Rows.Add(TbRow)
            ElseIf intReportType = 2 Then
                tblResult2.Rows.Add(TbRow)
            ElseIf intReportType = 3 Then
                tblResult3.Rows.Add(TbRow)
            End If


            intTotalCount = intTotalCount + drwhandyFollow.FollowingCount

        Next

        Dim TbRow1 As New HtmlTableRow
        Dim TbCell1 As HtmlTableCell

        TbCell1 = New HtmlTableCell
        TbCell1.InnerHtml = "مجموع"
        TbCell1.NoWrap = True
        TbCell1.Align = "center"
        TbCell1.ColSpan = 2
        TbRow1.Cells.Add(TbCell1)

        TbCell1 = New HtmlTableCell
        TbCell1.InnerHtml = intTotalCount.ToString("n0")
        TbCell1.NoWrap = True
        TbCell1.Align = "center"

        TbRow1.Cells.Add(TbCell1)
        If intReportType = 1 Then
            tblResult.Rows.Add(TbRow1)
        ElseIf intReportType = 2 Then
            tblResult2.Rows.Add(TbRow1)
        ElseIf intReportType = 3 Then
            tblResult3.Rows.Add(TbRow1)
        End If



    End Sub

    Protected Sub cmbBranch_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbBranch.SelectedIndexChanged

        Dim dtblUserLogin As BusinessObject.dstUser.spr_User_Login_SelectDataTable = CType(Session("dtblUserLogin"), BusinessObject.dstUser.spr_User_Login_SelectDataTable)
        Dim drwUserLogin As BusinessObject.dstUser.spr_User_Login_SelectRow = dtblUserLogin.Rows(0)


        If cmbBranch.SelectedValue = -1 Then


            If drwUserLogin.IsDataAdmin = True Then

                If cmbProvince.SelectedValue = -1 Then

                    odsPerson.SelectParameters.Item("Action").DefaultValue = 2
                    odsPerson.SelectParameters.Item("BranchID").DefaultValue = -1
                    odsPerson.SelectParameters.Item("ProvinceID").DefaultValue = -1


                    cmbPerson.DataBind()
                Else

                    odsPerson.SelectParameters.Item("Action").DefaultValue = 3
                    odsPerson.SelectParameters.Item("BranchID").DefaultValue = -1
                    odsPerson.SelectParameters.Item("ProvinceID").DefaultValue = cmbProvince.SelectedValue

                    cmbPerson.DataBind()

                End If


            ElseIf drwUserLogin.IsDataAdmin = False And drwUserLogin.IsDataUserAdmin = True Then



                cmbProvince.Enabled = False
                odsPerson.SelectParameters.Item("Action").DefaultValue = 3
                odsPerson.SelectParameters.Item("BranchID").DefaultValue = -1
                odsPerson.SelectParameters.Item("ProvinceID").DefaultValue = cmbProvince.SelectedValue

                cmbPerson.DataBind()


            End If

        Else

            odsPerson.SelectParameters.Item("Action").DefaultValue = 1
            odsPerson.SelectParameters.Item("BranchID").DefaultValue = CInt(cmbBranch.SelectedValue)
            odsPerson.SelectParameters.Item("ProvinceID").DefaultValue = -1


            cmbPerson.DataBind()

        End If


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
End Class