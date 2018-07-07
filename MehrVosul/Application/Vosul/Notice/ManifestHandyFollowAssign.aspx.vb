Public Class ManifestHandyFollowAssign
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Page.Response.Cache.SetCacheability(HttpCacheability.NoCache)

        Bootstrap_Panel1.CanNew = False
        Bootstrap_Panel1.CanSave = False
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


            ''check the access Group id
            Dim tadpAccessgroupUser As New BusinessObject.dstAccessgroupUserTableAdapters.spr_AccessgroupUserByID_SelectTableAdapter
            Dim dtblAccessgroupUser As BusinessObject.dstAccessgroupUser.spr_AccessgroupUserByID_SelectDataTable = Nothing

            dtblAccessgroupUser = tadpAccessgroupUser.GetData(drwUserLogin.ID, 3436)
            If dtblAccessgroupUser.Rows.Count > 0 Then
                dtblAccessgroupUser = tadpAccessgroupUser.GetData(drwUserLogin.ID, 3431)
                If dtblAccessgroupUser.Count = 0 Then
                    Bootstrap_Panel1.CanSave = True
                End If
            ElseIf drwUserLogin.IsDataAdmin = True Then
                Bootstrap_Panel1.CanSave = True
            End If

            odsPerson.SelectParameters.Item("Action").DefaultValue = 1
            odsPerson.SelectParameters.Item("BranchID").DefaultValue = drwUserLogin.FK_BrnachID
            odsPerson.SelectParameters.Item("ProvinceID").DefaultValue = -1

            cmbPerson.DataBind()


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

            '' txtNotPiadDurationDay.Attributes.Add("onkeypress", "return numbersonly(event, false);")

        Else

            Dim dtblUserLogin As BusinessObject.dstUser.spr_User_Login_SelectDataTable = CType(Session("dtblUserLogin"), BusinessObject.dstUser.spr_User_Login_SelectDataTable)
            Dim drwUserLogin As BusinessObject.dstUser.spr_User_Login_SelectRow = dtblUserLogin.Rows(0)


            ''check the access Group id
            Dim tadpAccessgroupUser As New BusinessObject.dstAccessgroupUserTableAdapters.spr_AccessgroupUserByID_SelectTableAdapter
            Dim dtblAccessgroupUser As BusinessObject.dstAccessgroupUser.spr_AccessgroupUserByID_SelectDataTable = Nothing

            dtblAccessgroupUser = tadpAccessgroupUser.GetData(drwUserLogin.ID, 3436)
            If dtblAccessgroupUser.Rows.Count > 0 Then
                dtblAccessgroupUser = tadpAccessgroupUser.GetData(drwUserLogin.ID, 3431)
                If dtblAccessgroupUser.Count = 0 Then
                    Bootstrap_Panel1.CanSave = True
                End If
            ElseIf drwUserLogin.IsDataAdmin = True Then
                Bootstrap_Panel1.CanSave = True
            End If
        End If
    End Sub

    Protected Sub btnCheckFiles_Click(sender As Object, e As EventArgs) Handles btnCheckFiles.Click
        ''get related warninginterwal
        Dim tadpWarningIntrevalInvoice As New BusinessObject.dstWarningIntervalsTableAdapters.spr_WarningIntervalsManifest_SelectTableAdapter
        Dim dtblWarningIntervalInvoice As BusinessObject.dstWarningIntervals.spr_WarningIntervalsManifest_SelectDataTable = Nothing


        Dim strBranchCode As String = cmbBranch.SelectedItem.Text.Substring(0, cmbBranch.SelectedItem.Text.IndexOf("("))


        Dim tadpBranch As New BusinessObject.dstBranchTableAdapters.spr_Branch_ByCode_SelectTableAdapter
        Dim dtblBranch As BusinessObject.dstBranch.spr_Branch_ByCode_SelectDataTable = Nothing

        dtblBranch = tadpBranch.GetData(strBranchCode)

        dtblWarningIntervalInvoice = tadpWarningIntrevalInvoice.GetData(dtblBranch.First.ID)

        If dtblWarningIntervalInvoice.Rows.Count > 0 Then
            Dim tadpTotalDeffredForAssign As New BusinessObject.dstTotalDeffredLCTableAdapters.spr_TotalDeffredLCNoticeAssign_SelectTableAdapter
            Dim dtblTotalDeffredForAssign As BusinessObject.dstTotalDeffredLC.spr_TotalDeffredLCNoticeAssign_SelectDataTable = Nothing

            dtblTotalDeffredForAssign = tadpTotalDeffredForAssign.GetData(strBranchCode, dtblWarningIntervalInvoice.First.FromDay, dtblWarningIntervalInvoice.First.ToDay, dtblBranch.First.ID)

            Dim strchklstFiles As String = ""

            For Each drwAssignFile As BusinessObject.dstTotalDeffredLC.spr_TotalDeffredLCNoticeAssign_SelectRow In dtblTotalDeffredForAssign.Rows
                strchklstFiles &= "<div class='checkbox'> <label> <input type='checkbox' value='" & drwAssignFile.CULN & "' name='chklstMenu" & drwAssignFile.CustomerNO & "'><i class='fa " & " fa-1x'></i> " & drwAssignFile.CULN & "</label></div>"
            Next drwAssignFile

            divchklstAssignFiles.InnerHtml = strchklstFiles
        End If



    End Sub

    Private Sub Bootstrap_Panel1_Panel_Cancel_Click(sender As Object, e As EventArgs) Handles Bootstrap_Panel1.Panel_Cancel_Click
        Response.Redirect("../Cartable/HandyFollowManagement.aspx")
    End Sub

    Private Sub Bootstrap_Panel1_Panel_Save_Click(sender As Object, e As EventArgs) Handles Bootstrap_Panel1.Panel_Save_Click
        Try

            Dim qryHandyFollow As New BusinessObject.dstHandyFollowTableAdapters.QueriesTableAdapter
            Dim dtblUserLogin As BusinessObject.dstUser.spr_User_Login_SelectDataTable = CType(Session("dtblUserLogin"), BusinessObject.dstUser.spr_User_Login_SelectDataTable)
            Dim drwUserLogin As BusinessObject.dstUser.spr_User_Login_SelectRow = dtblUserLogin.Rows(0)

            Dim intAssignUserID As Integer = cmbPerson.SelectedValue
            Dim blnFileCheck As Boolean = False

            For i As Integer = 0 To Request.Form.Keys.Count - 1


                If Request.Form.Keys(i).StartsWith("chklstMenu") = True Then

                    ''get File ID
                    Dim tadpFile As New BusinessObject.dstFileTableAdapters.spr_File_SelectTableAdapter
                    Dim dtblFile As BusinessObject.dstFile.spr_File_SelectDataTable = Nothing
                    Dim strLCNO As String = Request.Form(i).Substring(Request.Form(i).IndexOf("(") + 1, Request.Form(i).IndexOf(")") - Request.Form(i).IndexOf("(") - 1)
                    Dim strCustomerNO As String = Request.Form(i).Substring(0, Request.Form(i).IndexOf("("))
                    dtblFile = tadpFile.GetData(3, -1, strCustomerNO)
                    Dim intFileID As Integer = dtblFile.First.ID

                    Dim tadpLoan As New BusinessObject.dstLoanTableAdapters.spr_Loan_ByLoanNumber_SelectTableAdapter
                    Dim dtblLoan As BusinessObject.dstLoan.spr_Loan_ByLoanNumber_SelectDataTable = Nothing

                    dtblLoan = tadpLoan.GetData(strLCNO, intFileID)

                    Dim intLoanID As Integer = dtblLoan.First.ID

                    Dim strRemark As String = txtRemark.Text
                    qryHandyFollow.spr_HandyFollowAssign_Insert(intAssignUserID, intFileID, Date.Now, drwUserLogin.ID, strRemark, intLoanID, 3)

                    blnFileCheck = True
                End If



                ''Dim strLCNO As String = Request.Form(i).Substring(Request.Form(i). .Text.IndexOf("(") + 1, cmbFiles.SelectedItem.Text.IndexOf(")") - cmbFiles.SelectedItem.Text.IndexOf("(") - 1)


            Next i

            If blnFileCheck = True Then

            Else
                Bootstrap_Panel1.ShowMessage("فایلی جهت تخصیص انتخاب نشده", True)
                Return

            End If



        Catch ex As Exception


            Response.Redirect("../Cartable/HandyFollowManagement.aspx?Save=NO")
        End Try

        Response.Redirect("../Cartable/HandyFollowManagement.aspx?Save=OK")
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