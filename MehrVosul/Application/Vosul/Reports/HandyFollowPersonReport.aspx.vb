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
        odsBranch.SelectParameters.Item("Action").DefaultValue = 2
        odsBranch.SelectParameters.Item("ProvinceID").DefaultValue = cmbProvince.SelectedValue
        odsBranch.DataBind()

        cmbBranch.DataSourceID = "odsBranch"
        cmbBranch.DataTextField = "BrnachCode"
        cmbBranch.DataValueField = "ID"

        cmbBranch.DataBind()
    End Sub

    Protected Sub cmbProvince_DataBound(sender As Object, e As EventArgs) Handles cmbProvince.DataBound
        Dim li As New ListItem
        li.Text = "(همه استان ها)"
        li.Value = -1
        cmbProvince.Items.Insert(0, li)
    End Sub

    Private Sub Bootstrap_Panel1_Panel_Display_Click(sender As Object, e As EventArgs) Handles Bootstrap_Panel1.Panel_Display_Click


        Dim dtFromDate As Date = Bootstrap_PersianDateTimePicker_From.GergorainDateTime
        Dim dteToDate As Date = Bootstrap_PersianDateTimePicker_To.GergorainDateTime


        Dim ctxMehr As New BusinessObject.dbMehrVosulEntities1
        Dim lnqHandyFollow = ctxMehr.tbl_HandyFollow '.Where(Function(x) x.STime >= dtFromDate AndAlso x.STime <= dteToDate)

        If cmbProvince.SelectedValue <> -1 Then

            lnqHandyFollow = lnqHandyFollow.Where(Function(x) x.tbl_User.tbl_Branch.Fk_ProvinceID = cmbProvince.SelectedValue)

        ElseIf cmbBranch.SelectedValue <> -1 Then

            lnqHandyFollow = lnqHandyFollow.Where(Function(x) x.tbl_User.FK_BrnachID = cmbBranch.SelectedValue)

        End If

        If cmbNotification.SelectedValue <> 0 Then

            lnqHandyFollow = lnqHandyFollow.Where(Function(x) x.NotificationTypeID = cmbNotification.SelectedValue)

        End If

        If cmbSuccess.SelectedValue <> -1 Then

            Dim blnIsSuccess As Boolean = If(cmbSuccess.SelectedValue = 1, True, False)
            lnqHandyFollow = lnqHandyFollow.Where(Function(x) x.IsSuccess = blnIsSuccess)

        End If

        If cmbStatus.SelectedValue <> -1 Then

            Dim blnIsAnswered As Boolean = If(cmbStatus.SelectedValue = 1, True, False)
            lnqHandyFollow = lnqHandyFollow.Where(Function(x) x.Answered = blnIsAnswered)

        End If

        Dim lnqHandyFollowGroup = lnqHandyFollow.GroupBy(Function(x) x.tbl_User.Username)
        Dim lnqHandyFollowGroupList = lnqHandyFollowGroup.ToList()

        Dim intCount As Integer = 0
        For Each lnqHandyFollowGroupListItem In lnqHandyFollowGroupList

            intCount += 1
            Dim TbRow As New HtmlTableRow
            Dim TbCell As HtmlTableCell

            TbCell = New HtmlTableCell
            TbCell.InnerHtml = CStr(intCount)
            TbCell.Align = "center"
            TbCell.NoWrap = True
            TbRow.Cells.Add(TbCell)


            TbCell = New HtmlTableCell
            TbCell.InnerHtml = lnqHandyFollowGroupListItem.Key
            TbCell.NoWrap = True
            TbCell.Align = "center"
            TbCell.Attributes.Add("dir", "rtl")
            TbRow.Cells.Add(TbCell)

            TbCell = New HtmlTableCell
            Dim intTotalCount As Integer = lnqHandyFollowGroupListItem.Count

            TbCell.InnerHtml = intTotalCount.ToString("n0")
            TbCell.NoWrap = True
            TbCell.Align = "center"
            TbRow.Cells.Add(TbCell)


            tblResult.Rows.Add(TbRow)


        Next




    End Sub
End Class