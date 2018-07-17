Imports System.IO
Public Class HandyFollowAssignReport
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
                cmbProvince.Enabled = False

                odsBranch.SelectParameters.Item("Action").DefaultValue = 2
                odsBranch.SelectParameters.Item("ProvinceID").DefaultValue = drwUserLogin.Fk_ProvinceID
                odsBranch.DataBind()

                cmbBranch.DataSourceID = "odsBranch"
                cmbBranch.DataTextField = "BrnachCode"
                cmbBranch.DataValueField = "ID"

                cmbBranch.DataBind()
                cmbBranch.SelectedValue = drwUserLogin.FK_BrnachID

                ''check the access Group id
                Dim tadpAccessgroupUser As New BusinessObject.dstAccessgroupUserTableAdapters.spr_AccessgroupUserByID_SelectTableAdapter
                Dim dtblAccessgroupUser As BusinessObject.dstAccessgroupUser.spr_AccessgroupUserByID_SelectDataTable = Nothing

                dtblAccessgroupUser = tadpAccessgroupUser.GetData(drwUserLogin.ID, 3431)
                If dtblAccessgroupUser.Rows.Count = 0 Then
                    dtblAccessgroupUser = tadpAccessgroupUser.GetData(drwUserLogin.ID, 3436)
                    If dtblAccessgroupUser.Rows.Count > 0 Then
                        cmbBranch.Enabled = False
                    End If
                End If

                odsPerson.SelectParameters.Item("Action").DefaultValue = 1
                odsPerson.SelectParameters.Item("BranchID").DefaultValue = cmbBranch.SelectedValue
                odsPerson.SelectParameters.Item("ProvinceID").DefaultValue = -1

                cmbPerson.DataBind()


            End If

        End If

        txtAssignDay.Attributes.Add("onkeypress", "return numbersonly(event, false);")

        Bootstrap_PersianDateTimePicker_From.ShowTimePicker = True
        Bootstrap_PersianDateTimePicker_To.ShowTimePicker = True


    End Sub

    Private Sub Bootstrap_Panel1_Panel_Display_Click(sender As Object, e As EventArgs) Handles Bootstrap_Panel1.Panel_Display_Click


        Dim dtFromDate As Date = Bootstrap_PersianDateTimePicker_From.GergorainDateTime
        Dim dteToDate As Date = Bootstrap_PersianDateTimePicker_To.GergorainDateTime.AddDays(1)


        Dim tadphandyFollowReport As New BusinessObject.dstReportTableAdapters.spr_HandyFollowAssign_ReportTableAdapter
        Dim dtblHandyFollowReport As BusinessObject.dstReport.spr_HandyFollowAssign_ReportDataTable = Nothing
        Dim intAction As Int16 = 0


        Dim intBranchID As Integer = cmbBranch.SelectedValue
        Dim intProvinceID As Integer = cmbProvince.SelectedValue
        Dim strCustomerNO As String = txtCustomerNO.Text.Trim()
        Dim intAssignUser As Integer = cmbPerson.SelectedValue

        If cmbBranch.SelectedValue <> -1 AndAlso txtCustomerNO.Text.Trim <> "" AndAlso cmbPerson.SelectedValue <> -1 Then
            intAction = 4
        ElseIf cmbBranch.SelectedValue <> -1 AndAlso txtCustomerNO.Text.Trim <> "" AndAlso cmbPerson.SelectedValue = -1 Then
            intAction = 3
        ElseIf cmbBranch.SelectedValue <> -1 AndAlso txtCustomerNO.Text.Trim = "" AndAlso cmbPerson.SelectedValue <> -1 Then
            intAction = 2
        ElseIf cmbBranch.SelectedValue <> -1 AndAlso txtCustomerNO.Text.Trim = "" AndAlso cmbPerson.SelectedValue = -1 Then
            intAction = 1

        ElseIf cmbBranch.SelectedValue = -1 AndAlso txtCustomerNO.Text.Trim <> "" AndAlso cmbPerson.SelectedValue <> -1 Then
            intAction = 8

        ElseIf cmbBranch.SelectedValue = -1 AndAlso txtCustomerNO.Text.Trim <> "" AndAlso cmbPerson.SelectedValue = -1 Then
            intAction = 7
        ElseIf cmbBranch.SelectedValue = -1 AndAlso txtCustomerNO.Text.Trim = "" AndAlso cmbPerson.SelectedValue <> -1 Then
            intAction = 6

        ElseIf cmbBranch.SelectedValue = -1 AndAlso txtCustomerNO.Text.Trim = "" AndAlso cmbPerson.SelectedValue = -1 Then
            intAction = 5

        End If

        dtblHandyFollowReport = tadphandyFollowReport.GetData(intAction, intBranchID, intProvinceID, dtFromDate, dteToDate, intAssignUser, strCustomerNO)

        Dim intCount As Integer = 0

        If dtblHandyFollowReport.Rows.Count > 0 Then
            divResult2.Visible = True

        End If

        Dim blnIsRed As Boolean = False
        Dim blnYellow As Boolean = False
        Dim blnCheckTotalDeffred As Boolean = chkStatus.Checked

        Dim tadpTotalDeffred As New BusinessObject.dstTotalDeffredLCTableAdapters.spr_TotalDeffredLC_SelectByLCNOTableAdapter
        Dim dtblTotalDeffred As BusinessObject.dstTotalDeffredLC.spr_TotalDeffredLC_SelectByLCNODataTable = Nothing

        Dim tadplHandyFollow As New BusinessObject.dstHandyFollowTableAdapters.spr_HandyFollowByAssignID_SelectTableAdapter
        Dim dtblHandyFollow As BusinessObject.dstHandyFollow.spr_HandyFollowByAssignID_SelectDataTable = Nothing


        Session("dtblHandyFollowAssignReport") = dtblHandyFollowReport

        For Each drwhandyFollow As BusinessObject.dstReport.spr_HandyFollowAssign_ReportRow In dtblHandyFollowReport


            intCount += 1
            Dim TbRow As New HtmlTableRow
            Dim TbCell As HtmlTableCell

            If txtAssignDay.Text.Trim <> "" Then
                Dim intAssignDay As Integer = CInt(txtAssignDay.Text)



                If drwhandyFollow.AssignDate <= mdlGeneral.GetPersianDate(Date.Now.AddDays(-intAssignDay)) AndAlso drwhandyFollow.IsAssigned = True Then

                    dtblHandyFollow = tadplHandyFollow.GetData(1, drwhandyFollow.AssignUserID, drwhandyFollow.ID)
                    dtblTotalDeffred = tadpTotalDeffred.GetData(drwhandyFollow.LoanNumber)
                    If dtblHandyFollow.First.HandFollow <> 0 Then
                        If dtblTotalDeffred.Rows.Count > 0 Then
                            blnIsRed = True
                        End If
                    Else

                        'If dtblTotalDeffred.Rows.Count > 0 Then
                        blnYellow = True
                        'End If

                    End If

                End If
            End If


            TbCell = New HtmlTableCell
            TbCell.InnerHtml = CStr(intCount)
            TbCell.Align = "center"
            TbCell.NoWrap = True
            TbRow.Cells.Add(TbCell)
            If blnIsRed = True Then
                TbCell.Attributes.Add("style", "background-color:red")
            ElseIf blnYellow = True Then
                TbCell.Attributes.Add("style", "background-color:yellow")
            End If


            TbCell = New HtmlTableCell
            TbCell.InnerHtml = drwhandyFollow.BranchName
            TbCell.NoWrap = True
            TbCell.Align = "center"
            TbCell.Attributes.Add("dir", "rtl")
            TbRow.Cells.Add(TbCell)
            If blnIsRed = True Then
                TbCell.Attributes.Add("style", "background-color:red")
            ElseIf blnYellow = True Then
                TbCell.Attributes.Add("style", "background-color:yellow")
            End If

            TbCell = New HtmlTableCell
            TbCell.InnerHtml = drwhandyFollow.LoanNumber
            TbCell.NoWrap = True
            TbCell.Align = "center"
            TbCell.Attributes.Add("dir", "rtl")
            TbRow.Cells.Add(TbCell)
            If blnIsRed = True Then
                TbCell.Attributes.Add("style", "background-color:red")
            ElseIf blnYellow = True Then
                TbCell.Attributes.Add("style", "background-color:yellow")
            End If

            TbCell = New HtmlTableCell
            TbCell.InnerHtml = drwhandyFollow.Username
            TbCell.NoWrap = True
            TbCell.Align = "center"
            TbCell.Attributes.Add("dir", "rtl")
            TbRow.Cells.Add(TbCell)
            If blnIsRed = True Then
                TbCell.Attributes.Add("style", "background-color:red")
            ElseIf blnYellow = True Then
                TbCell.Attributes.Add("style", "background-color:yellow")
            End If

            TbCell = New HtmlTableCell
            TbCell.InnerHtml = drwhandyFollow.AssignDate
            TbCell.NoWrap = True
            TbCell.Align = "center"
            TbCell.Attributes.Add("dir", "ltr")
            TbRow.Cells.Add(TbCell)
            If blnIsRed = True Then
                TbCell.Attributes.Add("style", "background-color:red")
            ElseIf blnYellow = True Then
                TbCell.Attributes.Add("style", "background-color:yellow")
            End If

            TbCell = New HtmlTableCell
            If drwhandyFollow.IsAssigned = True Then
                TbCell.InnerHtml = "تخصیص یافته"
            Else
                TbCell.InnerHtml = "آزاد"
            End If
            If blnIsRed = True Then
                TbCell.Attributes.Add("style", "background-color:red")
            ElseIf blnYellow = True Then
                TbCell.Attributes.Add("style", "background-color:yellow")
            End If

            TbCell.NoWrap = True
            TbCell.Align = "center"
            TbCell.Attributes.Add("dir", "rtl")
            TbRow.Cells.Add(TbCell)
            If blnIsRed = True Then
                TbCell.Attributes.Add("style", "background-color:red")
            ElseIf blnYellow = True Then
                TbCell.Attributes.Add("style", "background-color:yellow")
            End If

            TbCell = New HtmlTableCell
            TbCell.InnerHtml = drwhandyFollow.AssignType
            TbCell.NoWrap = True
            TbCell.Align = "center"
            TbCell.Attributes.Add("dir", "rtl")
            TbRow.Cells.Add(TbCell)
            If blnIsRed = True Then
                TbCell.Attributes.Add("style", "background-color:red")
            ElseIf blnYellow = True Then
                TbCell.Attributes.Add("style", "background-color:yellow")
            End If


            tblResult2.Rows.Add(TbRow)

            blnIsRed = False
            blnYellow = False

        Next

    End Sub

    Protected Sub cmbProvince_DataBound(sender As Object, e As EventArgs) Handles cmbProvince.DataBound
        Dim li As New ListItem
        li.Text = "(همه استان ها)"
        li.Value = -1
        cmbProvince.Items.Insert(0, li)
    End Sub

    Protected Sub cmbBranch_DataBound(sender As Object, e As EventArgs) Handles cmbBranch.DataBound
        Dim li As New ListItem
        li.Text = "(همه)"
        li.Value = -1
        cmbBranch.Items.Insert(0, li)
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

    Private Sub Bootstrap_Panel1_Panel_Excel_Click(sender As Object, e As EventArgs) Handles Bootstrap_Panel1.Panel_Excel_Click
        Try
            If Session("dtblHandyFollowAssignReport") IsNot Nothing Then
                Dim dtblUserLogin As BusinessObject.dstUser.spr_User_Login_SelectDataTable = CType(Session("dtblUserLogin"), BusinessObject.dstUser.spr_User_Login_SelectDataTable)
                Dim drwUserLogin As BusinessObject.dstUser.spr_User_Login_SelectRow = dtblUserLogin.Rows(0)


                Dim strPath As String = Server.MapPath("") & "\TempFile\" & drwUserLogin.ID & "\"
                Dim FileName As String = "HandyFollowAssignReport-" & drwUserLogin.ID.ToString()

                Dim tblCSVResult As DataTable = Session("dtblHandyFollowAssignReport")
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
                    Session("dtblHandyFollowAssignReport") = Nothing
                End If
            Else
                Bootstrap_Panel1.ShowMessage("امکان انتقال گزارش به فایل اکسل وجود ندارد", False)
                Session("dtblHandyFollowAssignReport") = Nothing
            End If
        Catch ex As Exception
            Bootstrap_Panel1.ShowMessage("امکان انتقال گزارش به فایل اکسل وجود ندارد", False)
            Session("dtblHandyFollowAssignReport") = Nothing
        End Try

    End Sub
End Class