Public Class HandyFollowAssign
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Page.Response.Cache.SetCacheability(HttpCacheability.NoCache)

        Bootstrap_Panel1.CanNew = False
        Bootstrap_Panel1.CanSave = True
        Bootstrap_Panel1.CanDelete = False
        Bootstrap_Panel1.CanSearch = False
        Bootstrap_Panel1.CanCancel = True
        Bootstrap_Panel1.CanUp = False
        Bootstrap_Panel1.CanWizard = False
        Bootstrap_Panel1.CanConfirmRequest = False
        Bootstrap_Panel1.CanReject = False
        Bootstrap_Panel1.CanDisplay = False
        Bootstrap_Panel1.CanExcel = False
        Bootstrap_Panel1.Enable_Save_Client_Validate = True

        lblInnerPageTitle.Text = "پرکردن کادرهای قرمز رنگ، اجباری است."

        If Page.IsPostBack = False Then

            Dim dtblUserLogin As BusinessObject.dstUser.spr_User_Login_SelectDataTable = CType(Session("dtblUserLogin"), BusinessObject.dstUser.spr_User_Login_SelectDataTable)
            Dim drwUserLogin As BusinessObject.dstUser.spr_User_Login_SelectRow = dtblUserLogin.Rows(0)




            If drwUserLogin.IsDataAdmin = False AndAlso drwUserLogin.IsDataUserAdmin = True Then

                cmbProvince.SelectedValue = drwUserLogin.Fk_ProvinceID

                odsBranch.SelectParameters.Item("Action").DefaultValue = 2
                odsBranch.SelectParameters.Item("ProvinceID").DefaultValue = drwUserLogin.Fk_ProvinceID
                odsBranch.DataBind()

                cmbBranch.DataSourceID = "odsBranch"
                cmbBranch.DataTextField = "BrnachCode"
                cmbBranch.DataValueField = "ID"


                cmbBranch.DataBind()
                cmbBranch.Enabled = False
                cmbBranch.SelectedValue = drwUserLogin.FK_BrnachID

                odsPerson.SelectParameters.Item("Action").DefaultValue = 1
                odsPerson.SelectParameters.Item("BranchID").DefaultValue = cmbBranch.SelectedValue
                odsPerson.SelectParameters.Item("ProvinceID").DefaultValue = -1

                cmbPerson.DataBind()





                cmbProvince.Enabled = False

            ElseIf drwUserLogin.IsDataAdmin = True Then


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

            ElseIf drwUserLogin.IsDataAdmin = False AndAlso drwUserLogin.IsDataUserAdmin = False Then


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
                odsPerson.SelectParameters.Item("BranchID").DefaultValue = cmbBranch.SelectedValue
                odsPerson.SelectParameters.Item("ProvinceID").DefaultValue = -1

                cmbPerson.DataBind()




            End If

            txtNotPiadDurationDay.Attributes.Add("onkeypress", "return numbersonly(event, false);")
        End If



    End Sub

    Private Sub Bootstrap_Panel1_Panel_Save_Click(sender As Object, e As EventArgs) Handles Bootstrap_Panel1.Panel_Save_Click

        Try

            Dim qryHandyFollow As New BusinessObject.dstHandyFollowTableAdapters.QueriesTableAdapter
            Dim dtblUserLogin As BusinessObject.dstUser.spr_User_Login_SelectDataTable = CType(Session("dtblUserLogin"), BusinessObject.dstUser.spr_User_Login_SelectDataTable)
            Dim drwUserLogin As BusinessObject.dstUser.spr_User_Login_SelectRow = dtblUserLogin.Rows(0)

            Dim intAssignUserID As Integer = cmbPerson.SelectedValue
            ''get File ID
            Dim tadpFile As New BusinessObject.dstFileTableAdapters.spr_File_SelectTableAdapter
            Dim dtblFile As BusinessObject.dstFile.spr_File_SelectDataTable = Nothing
            dtblFile = tadpFile.GetData(3, -1, cmbFiles.SelectedValue)
            Dim intFileID As Integer = dtblFile.First.ID

            Dim strLCNO As String = cmbFiles.SelectedItem.Text.Substring(cmbFiles.SelectedItem.Text.IndexOf("(") + 1, cmbFiles.SelectedItem.Text.IndexOf(")") - cmbFiles.SelectedItem.Text.IndexOf("(") - 1)

            Dim tadpLoan As New BusinessObject.dstLoanTableAdapters.spr_Loan_ByLoanNumber_SelectTableAdapter
            Dim dtblLoan As BusinessObject.dstLoan.spr_Loan_ByLoanNumber_SelectDataTable = Nothing

            dtblLoan = tadpLoan.GetData(strLCNO, intFileID)

            Dim intLoanID As Integer = dtblLoan.First.ID

            Dim strRemark As String = txtRemark.Text
            qryHandyFollow.spr_HandyFollowAssign_Insert(intAssignUserID, intFileID, Date.Now, drwUserLogin.ID, strRemark, intLoanID)

            cmbFiles.DataBind()
            Bootstrap_Panel1.ShowMessage("پرونده مورد نظر با موفقیت تخصیص یافت", False)

        Catch ex As Exception

            Bootstrap_Panel1.ShowMessage("در تخصیص پرونده خطا رخ داده است", True)

        End Try



    End Sub

    Protected Sub btnCheckFiles_ServerClick(sender As Object, e As EventArgs) Handles btnCheckFiles.Click

        Dim intNotPiadDurationDay As Integer = CInt(txtNotPiadDurationDay.Text)
        Dim strBranchCode As String = cmbBranch.SelectedItem.Text.Substring(0, cmbBranch.SelectedItem.Text.IndexOf("("))
        odsFiles.SelectParameters.Item("BranchCode").DefaultValue = strBranchCode
        odsFiles.SelectParameters.Item("NotPiadDurationDay").DefaultValue = intNotPiadDurationDay


        Dim tadpBranch As New BusinessObject.dstBranchTableAdapters.spr_Branch_ByCode_SelectTableAdapter
        Dim dtblBranch As BusinessObject.dstBranch.spr_Branch_ByCode_SelectDataTable = Nothing

        dtblBranch = tadpBranch.GetData(strBranchCode)
        odsFiles.SelectParameters.Item("BranchID").DefaultValue = dtblBranch.First.ID

        cmbFiles.DataBind()


    End Sub

    Protected Sub cmbFiles_DataBound(sender As Object, e As EventArgs) Handles cmbFiles.DataBound
        Dim li As New ListItem
        li.Text = "----"
        li.Value = -1
        cmbFiles.Items.Insert(0, li)
    End Sub

    Protected Sub cmbProvince_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbProvince.SelectedIndexChanged

        odsBranch.SelectParameters.Item("Action").DefaultValue = 2
        odsBranch.SelectParameters.Item("ProvinceID").DefaultValue = cmbProvince.SelectedValue
        odsBranch.DataBind()

        cmbBranch.DataSourceID = "odsBranch"
        cmbBranch.DataTextField = "BrnachCode"
        cmbBranch.DataValueField = "ID"


        cmbBranch.DataBind()

        cmbPerson.DataBind()

    End Sub

    Protected Sub cmbBranch_DataBound(sender As Object, e As EventArgs) Handles cmbBranch.DataBound
        Dim li As New ListItem
        li.Text = "---"
        li.Value = -1
        cmbBranch.Items.Insert(0, li)
    End Sub

    Protected Sub cmbBranch_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbBranch.SelectedIndexChanged

        odsPerson.SelectParameters.Item("Action").DefaultValue = 1
        odsPerson.SelectParameters.Item("BranchID").DefaultValue = cmbBranch.SelectedValue
        odsPerson.SelectParameters.Item("ProvinceID").DefaultValue = -1

        cmbPerson.DataBind()


    End Sub

    Protected Sub cmbPerson_DataBound(sender As Object, e As EventArgs) Handles cmbPerson.DataBound
        Dim li As New ListItem
        li.Text = "---"
        li.Value = -1
        cmbPerson.Items.Insert(0, li)
    End Sub
End Class