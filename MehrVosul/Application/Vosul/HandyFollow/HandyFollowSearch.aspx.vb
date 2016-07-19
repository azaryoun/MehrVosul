Public Class HandyFollowSearch
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
        txt_InstallmentCount.Attributes.Add("onkeypress", "return numbersonly(event, false);")
     
        If Page.IsPostBack = False Then

            ''Get current User Brnach

            Dim dtblUserLogin As BusinessObject.dstUser.spr_User_Login_SelectDataTable = CType(Session("dtblUserLogin"), BusinessObject.dstUser.spr_User_Login_SelectDataTable)
            Dim drwUserLogin As BusinessObject.dstUser.spr_User_Login_SelectRow = dtblUserLogin.Rows(0)


            If drwUserLogin.IsDataAdmin = True Then

                odsBranch.SelectParameters.Item("Action").DefaultValue = 2
                odsBranch.SelectParameters.Item("ProvinceID").DefaultValue = drwUserLogin.Fk_ProvinceID
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


                cmbBranch.DataBind()

                cmbBranch.SelectedValue = drwUserLogin.FK_BrnachID
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

        If hdnAction.Value.StartsWith("S") = True Then
            Dim intFileID As Integer = CInt(hdnAction.Value.Split(";")(1))
            Dim intLoanID As Integer = CInt(hdnAction.Value.Split(";")(2))
            Session("intFileID") = CObj(intFileID)
            Session("intLoanID") = CObj(intLoanID)
            Response.Redirect("HandyFollowNew.aspx")
        End If

    End Sub

    Private Sub Bootstrap_Panel1_Panel_Display_Click(sender As Object, e As System.EventArgs) Handles Bootstrap_Panel1.Panel_Display_Click
        Dim tadpReport As New BusinessObject.dstReportTableAdapters.spr_Report_CurrentLCStatus_MaxLoanType_SelectTableAdapter
        Dim dtblReport As BusinessObject.dstReport.spr_Report_CurrentLCStatus_MaxLoanType_SelectDataTable = Nothing

        Dim intInstallmentCount As Integer = CInt(txt_InstallmentCount.Text)

        If cmbBranch.SelectedValue = -1 And cmbLoanType.SelectedValue = -1 And cmbProvince.SelectedValue = -1 Then

            dtblReport = tadpReport.GetData(1, -1, -1, intInstallmentCount, -1)

        ElseIf cmbProvince.SelectedValue = -1 And cmbBranch.SelectedValue <> -1 And cmbLoanType.SelectedValue = -1 Then

            Dim intBranch As Integer = CInt(cmbBranch.SelectedValue)
            dtblReport = tadpReport.GetData(2, intBranch, -1, intInstallmentCount, -1)

        ElseIf cmbProvince.SelectedValue = -1 And cmbBranch.SelectedValue = -1 And cmbLoanType.SelectedValue <> -1 Then

            Dim intLoanTypeID As Integer = CInt(cmbLoanType.SelectedValue)
            dtblReport = tadpReport.GetData(3, -1, intLoanTypeID, intInstallmentCount, -1)

        ElseIf cmbProvince.SelectedValue = -1 And cmbBranch.SelectedValue <> -1 And cmbLoanType.SelectedValue <> -1 Then

            Dim intLoanTypeID As Integer = CInt(cmbLoanType.SelectedValue)
            Dim intBranch As Integer = CInt(cmbBranch.SelectedValue)
            dtblReport = tadpReport.GetData(4, intBranch, intLoanTypeID, intInstallmentCount, -1)

        ElseIf cmbProvince.SelectedValue <> -1 And cmbBranch.SelectedValue = -1 And cmbLoanType.SelectedValue = -1 Then

            Dim intProvince As Integer = CInt(cmbProvince.SelectedValue)
            dtblReport = tadpReport.GetData(5, -1, -1, intInstallmentCount, intProvince)

        ElseIf cmbProvince.SelectedValue <> -1 And cmbBranch.SelectedValue = -1 And cmbLoanType.SelectedValue <> -1 Then

            Dim intProvince As Integer = CInt(cmbProvince.SelectedValue)
            Dim intLoanTypeID As Integer = CInt(cmbLoanType.SelectedValue)
            dtblReport = tadpReport.GetData(6, -1, intLoanTypeID, intInstallmentCount, intProvince)

        ElseIf cmbProvince.SelectedValue <> -1 And cmbBranch.SelectedValue <> -1 And cmbLoanType.SelectedValue <> -1 Then

            Dim intBranch As Integer = CInt(cmbBranch.SelectedValue)
            Dim intProvince As Integer = CInt(cmbProvince.SelectedValue)
            Dim intLoanTypeID As Integer = CInt(cmbLoanType.SelectedValue)
            dtblReport = tadpReport.GetData(7, intBranch, intLoanTypeID, intInstallmentCount, intProvince)

        ElseIf cmbProvince.SelectedValue <> -1 And cmbBranch.SelectedValue <> -1 And cmbLoanType.SelectedValue = -1 Then

            Dim intBranch As Integer = CInt(cmbBranch.SelectedValue)
            Dim intProvince As Integer = CInt(cmbProvince.SelectedValue)
            Dim intLoanTypeID As Integer = CInt(cmbLoanType.SelectedValue)
            dtblReport = tadpReport.GetData(8, intBranch, intLoanTypeID, intInstallmentCount, intProvince)

        End If



        If dtblReport.Rows.Count > 0 Then

            divResult.Visible = True

            ''get total report
            Dim intCount As Integer = 0

            Dim strchklstMenuLeaves As String = ""
            For Each drwReport As BusinessObject.dstReport.spr_Report_CurrentLCStatus_MaxLoanType_SelectRow In dtblReport.Rows

                intCount += 1
                Dim TbRow As New HtmlTableRow
                Dim TbCell As HtmlTableCell


                ''strchklstMenuLeaves = "<input type='checkbox' value='" & drwReport.ID & "' name='chklstMenu" & drwReport.MobileNo & "'>"

                ''TbCell = New HtmlTableCell
                ''TbCell.InnerHtml = strchklstMenuLeaves
                ''TbCell.Align = "center"
                ''TbCell.NoWrap = True
                ''TbRow.Cells.Add(TbCell)



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
                TbCell.InnerHtml = drwReport.Branch
                TbCell.NoWrap = False
                TbCell.Align = "center"
                TbRow.Cells.Add(TbCell)


                TbCell = New HtmlTableCell
                TbCell.InnerHtml = "<a ID='lnkbtnFollowing' href='#'  onclick= btnFollwoing_ClientClick(" & drwReport.FileID & "," & drwReport.LoanID & ")>ثبت پیگیری</a>"
                TbCell.NoWrap = True
                TbCell.Align = "center"
                TbRow.Cells.Add(TbCell)


                tblResult.Rows.Add(TbRow)

            Next


        Else

            divResult.Visible = False


        End If
    End Sub




    Protected Sub cmbLoanType_DataBound(sender As Object, e As EventArgs) Handles cmbLoanType.DataBound
        Dim li As New ListItem
        li.Text = "(همه انواع وام)"
        li.Value = -1
        cmbLoanType.Items.Insert(0, li)

    End Sub

    Protected Sub cmbBranch_DataBound(sender As Object, e As EventArgs) Handles cmbBranch.DataBound
        Dim li As New ListItem
        li.Text = "---"
        li.Value = -1
        cmbBranch.Items.Insert(0, li)
    End Sub

    Protected Sub cmbProvince_DataBound(sender As Object, e As EventArgs) Handles cmbProvince.DataBound
        Dim li As New ListItem
        li.Text = "---"
        li.Value = -1
        cmbProvince.Items.Insert(0, li)
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


End Class