Public Class HandyFollowAssign
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

            Dim tadpAdminSetting As New BusinessObject.dstSystemSettingTableAdapters.spr_AdminSetting_SelectTableAdapter
            Dim dtblAdminSetting As BusinessObject.dstSystemSetting.spr_AdminSetting_SelectDataTable = Nothing

            dtblAdminSetting = tadpAdminSetting.GetData()

            ''check the access Group id
            Dim tadpAccessgroupUser As New BusinessObject.dstAccessgroupUserTableAdapters.spr_AccessgroupUserByID_SelectTableAdapter
            Dim dtblAccessgroupUser As BusinessObject.dstAccessgroupUser.spr_AccessgroupUserByID_SelectDataTable = Nothing

            If drwUserLogin.IsDataAdmin = False Then

                dtblAccessgroupUser = tadpAccessgroupUser.GetData(drwUserLogin.ID, 3436)
                If dtblAccessgroupUser.Rows.Count > 0 Then
                    dtblAccessgroupUser = tadpAccessgroupUser.GetData(drwUserLogin.ID, 3431)
                    If dtblAccessgroupUser.Count = 0 Then
                        Bootstrap_Panel1.CanSave = True
                    End If
                ElseIf drwUserLogin.FK_AccessGroupID = 3438 Then
                    Bootstrap_Panel1.CanSave = True
                End If

            Else
                Bootstrap_Panel1.CanSave = True
            End If


            odsLoanType.DataBind()

            If drwUserLogin.IsDataAdmin = True Then


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


            ElseIf drwUserLogin.FK_AccessGroupID = 3438 Then


                cmbProvince.SelectedValue = drwUserLogin.Fk_ProvinceID

                    odsBranch.SelectParameters.Item("Action").DefaultValue = 2
                    odsBranch.SelectParameters.Item("ProvinceID").DefaultValue = drwUserLogin.Fk_ProvinceID
                    odsBranch.DataBind()

                    cmbBranch.DataSourceID = "odsBranch"
                    cmbBranch.DataTextField = "BrnachCode"
                    cmbBranch.DataValueField = "ID"


                    cmbBranch.DataBind()
                    cmbBranch.SelectedValue = drwUserLogin.FK_BrnachID

                    odsPerson.SelectParameters.Item("Action").DefaultValue = 4
                    odsPerson.SelectParameters.Item("BranchID").DefaultValue = -1
                    odsPerson.SelectParameters.Item("ProvinceID").DefaultValue = -1

                    cmbPerson.DataBind()


            ElseIf drwUserLogin.IsDataAdmin = False AndAlso drwUserLogin.IsDataUserAdmin = True Then

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



                    Dim blnAdminBranch As Boolean = False
                    dtblAccessgroupUser = tadpAccessgroupUser.GetData(drwUserLogin.ID, 3431)

                    If dtblAccessgroupUser.Rows.Count = 0 Then
                        dtblAccessgroupUser = tadpAccessgroupUser.GetData(drwUserLogin.ID, 3436)
                        If dtblAccessgroupUser.Rows.Count > 0 Then
                            cmbBranch.Enabled = False
                        End If
                    End If

                    cmbProvince.Enabled = False



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

            txtNotPiadDurationDayFrom.Attributes.Add("onkeypress", "return numbersonly(event, false);")
            txtNotPiadDurationDayTo.Attributes.Add("onkeypress", "return numbersonly(event, false);")

            ''If Request.QueryString("AdminDelaidDay") <> Nothing Then

            ''    Dim intDelaidDay As Integer = CInt(Request.QueryString("AdminDelaidDay"))
            ''    ShowDelaiedFiles(intDelaidDay)


            ''End If

        Else

            Dim dtblUserLogin As BusinessObject.dstUser.spr_User_Login_SelectDataTable = CType(Session("dtblUserLogin"), BusinessObject.dstUser.spr_User_Login_SelectDataTable)

            Dim drwUserLogin As BusinessObject.dstUser.spr_User_Login_SelectRow = dtblUserLogin.Rows(0)

            Dim tadpAdminSetting As New BusinessObject.dstSystemSettingTableAdapters.spr_AdminSetting_SelectTableAdapter
            Dim dtblAdminSetting As BusinessObject.dstSystemSetting.spr_AdminSetting_SelectDataTable = Nothing

            dtblAdminSetting = tadpAdminSetting.GetData()


            If drwUserLogin.IsDataAdmin = True Then
                Bootstrap_Panel1.CanSave = True

            Else
                ''check the access Group id
                Dim tadpAccessgroupUser As New BusinessObject.dstAccessgroupUserTableAdapters.spr_AccessgroupUserByID_SelectTableAdapter
                Dim dtblAccessgroupUser As BusinessObject.dstAccessgroupUser.spr_AccessgroupUserByID_SelectDataTable = Nothing

                dtblAccessgroupUser = tadpAccessgroupUser.GetData(drwUserLogin.ID, 3436)
                If dtblAccessgroupUser.Rows.Count > 0 Then
                    dtblAccessgroupUser = tadpAccessgroupUser.GetData(drwUserLogin.ID, 3431)
                    If dtblAccessgroupUser.Count = 0 Then
                        Bootstrap_Panel1.CanSave = True

                    End If
                ElseIf drwUserLogin.FK_AccessGroupID = 3438 Then
                    Bootstrap_Panel1.CanSave = True
                End If
            End If



        End If

        btnCheckFiles.Attributes.Add("onClick", "return CheckDataEnter();")

    End Sub

    Private Sub Bootstrap_Panel1_Panel_Save_Click(sender As Object, e As EventArgs) Handles Bootstrap_Panel1.Panel_Save_Click


        Dim blnHasErrorHappened As Boolean = False

        Try


            Dim qryHandyFollow As New BusinessObject.dstHandyFollowTableAdapters.QueriesTableAdapter
            Dim dtblUserLogin As BusinessObject.dstUser.spr_User_Login_SelectDataTable = CType(Session("dtblUserLogin"), BusinessObject.dstUser.spr_User_Login_SelectDataTable)
            Dim drwUserLogin As BusinessObject.dstUser.spr_User_Login_SelectRow = dtblUserLogin.Rows(0)

            Dim intAssignType As Integer = cmbNotificationType.SelectedValue

            Dim blnFileCheck As Boolean = False
            Dim blnGroupAssign As Boolean = If(cmbAssignType.SelectedValue = 2, True, False)

            If blnGroupAssign = False Then

                For i As Integer = 0 To Request.Form.Keys.Count - 1

                    If Request.Form.Keys(i).StartsWith("cmbAssignPerson") = True Then

                        If Request.Form(i) <> "-1" Then

                            Try


                                Dim strLCNO As String = Request.Form(i).Substring(Request.Form(i).IndexOf("(") + 1, Request.Form(i).IndexOf(")") - Request.Form(i).IndexOf("(") - 1)
                                'Request.Form(i).Substring(Request.Form(i).IndexOf("(") + 1, Request.Form(i).IndexOf(")") - 2)

                                ' get File ID
                                Dim tadpFile As New BusinessObject.dstFileTableAdapters.spr_File_SelectTableAdapter
                                Dim dtblFile As BusinessObject.dstFile.spr_File_SelectDataTable = Nothing

                                Dim strCustomerNO As String = Request.Form(i).Substring(Request.Form(i).IndexOf(")") + 1, Request.Form(i).IndexOf(",") - Request.Form(i).IndexOf(")") - 1)
                                dtblFile = tadpFile.GetData(3, -1, strCustomerNO)

                                If dtblFile.Rows.Count <> 0 Then
                                    Dim intFileID As Integer = dtblFile.First.ID

                                    Dim tadpLoan As New BusinessObject.dstLoanTableAdapters.spr_Loan_ByLoanNumber_SelectTableAdapter
                                    Dim dtblLoan As BusinessObject.dstLoan.spr_Loan_ByLoanNumber_SelectDataTable = Nothing

                                    dtblLoan = tadpLoan.GetData(strLCNO, intFileID)

                                    Dim intLoanID As Integer = -1
                                    If dtblLoan.Rows.Count > 0 Then

                                        intLoanID = dtblLoan.First.ID

                                    Else

                                        ''insert loan
                                        Dim qryLoan As New BusinessObject.dstLoanTableAdapters.QueriesTableAdapter

                                        Dim strInfo As String = Request.Form(i).Substring(Request.Form(i).IndexOf(",") + 1, Request.Form(i).IndexOf("/") - Request.Form(i).IndexOf(",") - 1)

                                        Dim strLoanTypeCode As String = strInfo.Split(";")(2)
                                        ''get loan typeid
                                        Dim tadpLoantypeCode As New BusinessObject.dstLoanTypeTableAdapters.spr_LoanType_byCode_SelectTableAdapter
                                        Dim dtlbLoantypeCode As BusinessObject.dstLoanType.spr_LoanType_byCode_SelectDataTable = Nothing

                                        dtlbLoantypeCode = tadpLoantypeCode.GetData(strLoanTypeCode)
                                        Dim intLoanTypeCode As Integer = dtlbLoantypeCode.First.ID

                                        ''get  branch code
                                        Dim intBranchID As Integer = cmbBranch.SelectedValue

                                        Dim dteLoanDate? As Date = Nothing
                                        intLoanID = qryLoan.spr_Loan_Insert(intFileID, intLoanTypeCode, intBranchID, dteLoanDate, strLCNO, Nothing, Date.Now, Nothing, Nothing)


                                    End If


                                    Dim intAssignUserID As Integer = CInt(Request.Form(i).Substring(0, Request.Form(i).IndexOf("(")))

                                    If CheckFileAssign(intAssignUserID, intLoanID, 1) = False Then
                                        qryHandyFollow.spr_HandyFollowAssign_Insert(intAssignUserID, intFileID, Date.Now, drwUserLogin.ID, "", intLoanID, intAssignType)

                                    End If

                                Else

                                    Dim strInfo As String = Request.Form(i).Substring(Request.Form(i).IndexOf(",") + 1, Request.Form(i).IndexOf("/") - Request.Form(i).IndexOf(",") - 1)

                                    Dim strMobileNO As String = strInfo.Split(";")(1)
                                    Dim strLoanTypeCode As String = strInfo.Split(";")(2)
                                    Dim strFullName As String = strInfo.Split(";")(0)

                                    ''Insert to File
                                    Dim qryFile As New BusinessObject.dstFileTableAdapters.QueriesTableAdapter

                                    Dim intFileID As Integer = qryFile.spr_File_Insert(strCustomerNO, "", strFullName, "", strMobileNO, "", "", "", "", "", "", False, 1, Nothing, Nothing, Nothing)

                                    ''get  branch code
                                    Dim intBranchID As Integer = cmbBranch.SelectedValue

                                    ''get loan typeid
                                    Dim tadpLoantypeCode As New BusinessObject.dstLoanTypeTableAdapters.spr_LoanType_byCode_SelectTableAdapter
                                    Dim dtlbLoantypeCode As BusinessObject.dstLoanType.spr_LoanType_byCode_SelectDataTable = Nothing

                                    dtlbLoantypeCode = tadpLoantypeCode.GetData(strLoanTypeCode)
                                    Dim intLoanTypeCode As Integer = dtlbLoantypeCode.First.ID

                                    ''insert loan
                                    Dim qryLoan As New BusinessObject.dstLoanTableAdapters.QueriesTableAdapter

                                    Dim dteLoanDate? As Date = Nothing
                                    Dim intLoanID As Integer = qryLoan.spr_Loan_Insert(intFileID, intLoanTypeCode, intBranchID, dteLoanDate, strLCNO, Nothing, Date.Now, Nothing, Nothing)

                                    Dim intAssignUserID As Integer = CInt(Request.Form(i).Substring(0, Request.Form(i).IndexOf("(")))

                                    If CheckFileAssign(intAssignUserID, intLoanID, 1) = False Then
                                        qryHandyFollow.spr_HandyFollowAssign_Insert(intAssignUserID, intFileID, Date.Now, drwUserLogin.ID, "", intLoanID, intAssignType)

                                    End If

                                End If


                            Catch ex As Exception


                                blnHasErrorHappened = True
                                Exit For
                            End Try

                        End If

                    End If
                Next i

            Else

                If hdnSelected.Value <> "" Then




                    Dim strLCNumber As String() = hdnSelected.Value.Split(",")
                    For j As Integer = 0 To strLCNumber.Length - 2
                        Try
                            Dim strLCNO As String = strLCNumber(j).Split(";")(0)

                            ' get File ID
                            Dim tadpFile As New BusinessObject.dstFileTableAdapters.spr_File_SelectTableAdapter
                            Dim dtblFile As BusinessObject.dstFile.spr_File_SelectDataTable = Nothing

                            Dim strCustomerNO As String = strLCNumber(j).Split(";")(1)
                            dtblFile = tadpFile.GetData(3, -1, strCustomerNO)

                            If dtblFile.Rows.Count <> 0 Then
                                Dim intFileID As Integer = dtblFile.First.ID

                                Dim tadpLoan As New BusinessObject.dstLoanTableAdapters.spr_Loan_ByLoanNumber_SelectTableAdapter
                                Dim dtblLoan As BusinessObject.dstLoan.spr_Loan_ByLoanNumber_SelectDataTable = Nothing

                                dtblLoan = tadpLoan.GetData(strLCNO, intFileID)
                                Dim intLoanID As Integer = -1
                                If dtblLoan.Rows.Count > 0 Then
                                    intLoanID = dtblLoan.First.ID
                                Else

                                    ''insert loan
                                    Dim qryLoan As New BusinessObject.dstLoanTableAdapters.QueriesTableAdapter

                                    Dim strLoanTypeCode As String = strLCNumber(j).Split(";")(3)
                                    ''get loan typeid
                                    Dim tadpLoantypeCode As New BusinessObject.dstLoanTypeTableAdapters.spr_LoanType_byCode_SelectTableAdapter
                                    Dim dtlbLoantypeCode As BusinessObject.dstLoanType.spr_LoanType_byCode_SelectDataTable = Nothing

                                    dtlbLoantypeCode = tadpLoantypeCode.GetData(strLoanTypeCode)
                                    Dim intLoanTypeCode As Integer = dtlbLoantypeCode.First.ID

                                    ''get  branch code
                                    Dim intBranchID As Integer = cmbBranch.SelectedValue

                                    Dim dteLoanDate? As Date = Nothing
                                    intLoanID = qryLoan.spr_Loan_Insert(intFileID, intLoanTypeCode, intBranchID, dteLoanDate, strLCNO, Nothing, Date.Now, Nothing, Nothing)


                                End If

                                Dim intAssignUserID As Integer = CInt(cmbPerson.SelectedValue)

                                If CheckFileAssign(intAssignUserID, intLoanID, 1) = False Then
                                    qryHandyFollow.spr_HandyFollowAssign_Insert(intAssignUserID, intFileID, Date.Now, drwUserLogin.ID, "", intLoanID, intAssignType)

                                End If


                            Else


                                Dim strMobileNO As String = strLCNumber(j).Split(";")(2)
                                Dim strLoanTypeCode As String = strLCNumber(j).Split(";")(3)
                                Dim strFullName As String = strLCNumber(j).Split(";")(4)

                                ''Insert to File
                                Dim qryFile As New BusinessObject.dstFileTableAdapters.QueriesTableAdapter

                                Dim intFileID As Integer = qryFile.spr_File_Insert(strCustomerNO, "", strFullName, "", strMobileNO, "", "", "", "", "", "", False, 1, Nothing, Nothing, Nothing)

                                ''get  branch code
                                Dim intBranchID As Integer = cmbBranch.SelectedValue

                                ''get loan typeid
                                Dim tadpLoantypeCode As New BusinessObject.dstLoanTypeTableAdapters.spr_LoanType_byCode_SelectTableAdapter
                                Dim dtlbLoantypeCode As BusinessObject.dstLoanType.spr_LoanType_byCode_SelectDataTable = Nothing

                                dtlbLoantypeCode = tadpLoantypeCode.GetData(strLoanTypeCode)
                                Dim intLoanTypeCode As Integer = dtlbLoantypeCode.First.ID


                                ''insert loan
                                Dim qryLoan As New BusinessObject.dstLoanTableAdapters.QueriesTableAdapter

                                Dim dteLoanDate? As Date = Nothing
                                Dim intLoanID As Integer = qryLoan.spr_Loan_Insert(intFileID, intLoanTypeCode, intBranchID, dteLoanDate, strLCNO, Nothing, Date.Now, Nothing, Nothing)

                                Dim intAssignUserID As Integer = CInt(cmbPerson.SelectedValue)

                                If CheckFileAssign(intAssignUserID, intLoanID, 1) = False Then
                                    qryHandyFollow.spr_HandyFollowAssign_Insert(intAssignUserID, intFileID, Date.Now, drwUserLogin.ID, "", intLoanID, intAssignType)

                                End If


                            End If

                        Catch ex As Exception

                            blnHasErrorHappened = True
                            Exit For
                        End Try

                    Next

                End If

            End If


        Catch ex As Exception

            Response.Redirect("HandyFollowManagement.aspx?Save=NO")
        End Try

        If blnHasErrorHappened = True Then
            Bootstrap_Panel1.ShowMessage("در فرآیند تخصیص پیگیری خطا رخ داده است ", False)
            Return

        Else
            Response.Redirect("HandyFollowManagement.aspx?Save=OK")

        End If

    End Sub

    Protected Sub btnCheckFiles_ServerClick(sender As Object, e As EventArgs) Handles btnCheckFiles.Click

        Dim dtblUserLogin As BusinessObject.dstUser.spr_User_Login_SelectDataTable = CType(Session("dtblUserLogin"), BusinessObject.dstUser.spr_User_Login_SelectDataTable)
        Dim drwUserLogin As BusinessObject.dstUser.spr_User_Login_SelectRow = dtblUserLogin.Rows(0)

        Dim dtblPerson As BusinessObject.dstUser.spr_User_CheckBranch_SelectDataTable = Nothing
        Dim tadpPerson As New BusinessObject.dstUserTableAdapters.spr_User_CheckBranch_SelectTableAdapter


        Dim intNotPiadDurationDayFrom As Integer = 0
        Dim intNotPiadDurationDayTo As Integer = 0
        Dim strBranchCode As String = cmbBranch.SelectedItem.Text.Substring(0, cmbBranch.SelectedItem.Text.IndexOf("("))



        Dim tadpBranch As New BusinessObject.dstBranchTableAdapters.spr_Branch_ByCode_SelectTableAdapter
        Dim dtblBranch As BusinessObject.dstBranch.spr_Branch_ByCode_SelectDataTable = Nothing

        dtblBranch = tadpBranch.GetData(strBranchCode)

        Dim tadpTotalDeffredForAssign As New BusinessObject.dstTotalDeffredLCTableAdapters.spr_TotalDeffredLCFileAssign_SelectTableAdapter
        Dim dtblTotalDeffredForAssign As BusinessObject.dstTotalDeffredLC.spr_TotalDeffredLCFileAssign_SelectDataTable = Nothing


        If cmbDeferredPeriod.SelectedValue <> -1 Then


            Select Case cmbDeferredPeriod.SelectedValue
                Case 1
                    intNotPiadDurationDayFrom = 1
                    intNotPiadDurationDayTo = 60
                Case 2
                    intNotPiadDurationDayFrom = 61
                    intNotPiadDurationDayTo = 180
                Case 3
                    intNotPiadDurationDayFrom = 181
                    intNotPiadDurationDayTo = 540
                Case 4
                    intNotPiadDurationDayFrom = 541
                    intNotPiadDurationDayTo = 100000
            End Select

            If cmbLoanType.SelectedValue = -1 Then

                dtblTotalDeffredForAssign = tadpTotalDeffredForAssign.GetData(strBranchCode, intNotPiadDurationDayFrom, intNotPiadDurationDayTo, dtblBranch.First.ID, 1, -1, -1, -1)

            Else

                Dim intLoanTypeID As Integer = cmbLoanType.SelectedValue
                dtblTotalDeffredForAssign = tadpTotalDeffredForAssign.GetData(strBranchCode, intNotPiadDurationDayFrom, intNotPiadDurationDayTo, dtblBranch.First.ID, 3, -1, -1, intLoanTypeID)

            End If


        Else

            ''Max time out inserted in dataset
            If rdbListSelectType.SelectedValue = 0 Then

                intNotPiadDurationDayFrom = CInt(txtNotPiadDurationDayFrom.Text)
                intNotPiadDurationDayTo = CInt(txtNotPiadDurationDayTo.Text)


                If cmbLoanType.SelectedValue = -1 Then
                    dtblTotalDeffredForAssign = tadpTotalDeffredForAssign.GetData(strBranchCode, intNotPiadDurationDayFrom, intNotPiadDurationDayTo, dtblBranch.First.ID, 1, -1, -1, -1)

                Else

                    Dim intLoanTypeID As Integer = cmbLoanType.SelectedValue
                    dtblTotalDeffredForAssign = tadpTotalDeffredForAssign.GetData(strBranchCode, intNotPiadDurationDayFrom, intNotPiadDurationDayTo, dtblBranch.First.ID, 3, -1, -1, intLoanTypeID)

                End If

            Else

                Dim dblLCAmountFrom As Double = CDbl(txtNotPiadDurationDayFrom.Text)
                Dim dblLCAmountTo As Double = CDbl(txtNotPiadDurationDayTo.Text)
                If cmbLoanType.SelectedValue = -1 Then
                    dtblTotalDeffredForAssign = tadpTotalDeffredForAssign.GetData(strBranchCode, -1, -1, dtblBranch.First.ID, 2, dblLCAmountFrom, dblLCAmountTo, -1)

                Else
                    Dim intLoanTypeID As Integer = cmbLoanType.SelectedValue
                    dtblTotalDeffredForAssign = tadpTotalDeffredForAssign.GetData(strBranchCode, -1, -1, dtblBranch.First.ID, 4, dblLCAmountFrom, dblLCAmountTo, intLoanTypeID)

                End If

            End If
        End If



        Dim strchklstFiles As String = ""

        'For Each drwAssignFile As BusinessObject.dstTotalDeffredLC.spr_TotalDeffredLCFileAssign_SelectRow In dtblTotalDeffredForAssign.Rows
        '    strchklstFiles &= "<div class='checkbox'> <label> <input type='checkbox' value='" & drwAssignFile.LCNumber & "' name='chklstMenu" & drwAssignFile.CustomerNO & "'><i class='fa " & " fa-1x'></i> " & drwAssignFile.LCNumber & "</label></div>"
        'Next drwAssignFile

        ''divchklstAssignFiles.InnerHtml = strchklstFiles

        Dim i As Integer = 1
        For Each drwRTotalDeffredLC As BusinessObject.dstTotalDeffredLC.spr_TotalDeffredLCFileAssign_SelectRow In dtblTotalDeffredForAssign.Rows

            Dim TbRow As New HtmlTableRow
            Dim TbCell As HtmlTableCell


            TbCell = New HtmlTableCell
            ' '"<input type='hidden' id='hdnAmounts" & CStr(i) & "' value='" & drwRTotalDeffredLC.LCNumber & "') >"
            TbCell.InnerHtml = CStr(i)
            TbCell.Align = "center"
            TbCell.NoWrap = True
            TbRow.Cells.Add(TbCell)

            TbCell = New HtmlTableCell
            strchklstFiles = "<input type='checkbox' name='" & drwRTotalDeffredLC.LCNumber.ToString() & ";" & drwRTotalDeffredLC.CustomerNO.ToString() & ";" & drwRTotalDeffredLC.MobileNO.ToString() & ";" & drwRTotalDeffredLC.LoanTypeCode.ToString() & ";" & drwRTotalDeffredLC.FullName.ToString() & "' /> "
            TbCell.InnerHtml = strchklstFiles
            TbCell.Align = "center"
            TbCell.NoWrap = True
            TbRow.Cells.Add(TbCell)



            TbCell = New HtmlTableCell
            TbCell.InnerHtml = drwRTotalDeffredLC.FullName
            TbCell.NoWrap = True
            TbCell.Align = "center"
            TbRow.Cells.Add(TbCell)

            TbCell = New HtmlTableCell
            TbCell.InnerHtml = drwRTotalDeffredLC.LCNumber
            TbCell.NoWrap = True
            TbCell.Align = "center"
            TbRow.Cells.Add(TbCell)


            TbCell = New HtmlTableCell
            TbCell.InnerHtml = drwRTotalDeffredLC.InstallmentsCount
            TbCell.NoWrap = True
            TbCell.Align = "center"
            TbRow.Cells.Add(TbCell)

            TbCell = New HtmlTableCell
            TbCell.InnerHtml = drwRTotalDeffredLC.LCAmount.ToString("N0")
            TbCell.NoWrap = True
            TbCell.Align = "center"
            TbRow.Cells.Add(TbCell)

            TbCell = New HtmlTableCell
            TbCell.InnerHtml = drwRTotalDeffredLC.LoanTypeName
            TbCell.NoWrap = True
            TbCell.Align = "center"
            TbRow.Cells.Add(TbCell)

            ''TbCell = New HtmlTableCell
            ''TbCell.InnerHtml = drwRTotalDeffredLC.GDeffered
            ''TbCell.NoWrap = True
            ''TbCell.Align = "center"
            ''TbRow.Cells.Add(TbCell)

            TbCell = New HtmlTableCell
            TbCell.InnerHtml = drwRTotalDeffredLC.AmounDefferd.ToString("N0")
            TbCell.NoWrap = True
            TbCell.Align = "center"
            TbRow.Cells.Add(TbCell)

            TbCell = New HtmlTableCell
            TbCell.InnerHtml = drwRTotalDeffredLC.NotPiadDurationDay
            TbCell.NoWrap = True
            TbCell.Align = "center"
            TbRow.Cells.Add(TbCell)


            Dim strTemp As String = ""

            If drwUserLogin.IsDataAdmin = True Then



                strTemp = "<select name='cmbAssignPerson" & CStr(i) & "' style='WIDTH:50;HEIGHT:25px;background-color:rgba(0,0,0,0.05)'>"
                strTemp = strTemp & "<Option value='-1' selected>انتخاب شود</Option>"

                dtblPerson = tadpPerson.GetData(1, cmbBranch.SelectedValue, -1)
                Dim j As Integer = 0
                For Each drwPerson As BusinessObject.dstUser.spr_User_CheckBranch_SelectRow In dtblPerson

                    strTemp = strTemp & "<Option value='" & drwPerson.ID & "(" & drwRTotalDeffredLC.LCNumber & ")" & drwRTotalDeffredLC.CustomerNO & "," & drwRTotalDeffredLC.FullName & ";" & drwRTotalDeffredLC.MobileNO & ";" & drwRTotalDeffredLC.LoanTypeCode & "/'>" & drwPerson.Username & "</Option>"

                Next

                strTemp = strTemp & "</Select>"

            ElseIf drwUserLogin.FK_AccessGroupID = 3438 Then

                strTemp = "<select name='cmbAssignPerson" & CStr(i) & "' style='WIDTH:50;HEIGHT:25px;background-color:rgba(0,0,0,0.05)'>"
                    strTemp = strTemp & "<Option value='-1' selected>انتخاب شود</Option>"

                    dtblPerson = tadpPerson.GetData(4, -1, -1)

                    Dim j As Integer = 0
                    For Each drwPerson As BusinessObject.dstUser.spr_User_CheckBranch_SelectRow In dtblPerson

                        strTemp = strTemp & "<Option value='" & drwPerson.ID & "(" & drwRTotalDeffredLC.LCNumber & ")" & drwRTotalDeffredLC.CustomerNO & "," & drwRTotalDeffredLC.FullName & ";" & drwRTotalDeffredLC.MobileNO & ";" & drwRTotalDeffredLC.LoanTypeCode & "/'>" & drwPerson.Username & "</Option>"


                    Next

                    strTemp = strTemp & "</Select>"



            ElseIf drwUserLogin.IsDataAdmin = False AndAlso drwUserLogin.IsDataUserAdmin = True Then


                strTemp = "<select name='cmbAssignPerson" & CStr(i) & "' style='WIDTH:50;HEIGHT:25px;background-color:rgba(0,0,0,0.05)'>"
                    strTemp = strTemp & "<Option value='-1' selected>انتخاب شود</Option>"

                    dtblPerson = tadpPerson.GetData(1, cmbBranch.SelectedValue, -1)

                    Dim j As Integer = 0
                    For Each drwPerson As BusinessObject.dstUser.spr_User_CheckBranch_SelectRow In dtblPerson

                        strTemp = strTemp & "<Option value='" & drwPerson.ID & "(" & drwRTotalDeffredLC.LCNumber & ")" & drwRTotalDeffredLC.CustomerNO & "," & drwRTotalDeffredLC.FullName & ";" & drwRTotalDeffredLC.MobileNO & ";" & drwRTotalDeffredLC.LoanTypeCode & "/'>" & drwPerson.Username & "</Option>"


                    Next

                    strTemp = strTemp & "</Select>"


            ElseIf drwUserLogin.IsDataAdmin = False AndAlso drwUserLogin.IsDataUserAdmin = False Then


                strTemp = "<select name='cmbAssignPerson" & CStr(i) & "' style='WIDTH:50;HEIGHT:25px;background-color:rgba(0,0,0,0.05)'>"
                strTemp = strTemp & "<Option value='-1' selected>انتخاب شود</Option>"

                dtblPerson = tadpPerson.GetData(1, cmbBranch.SelectedValue, -1)
                Dim j As Integer = 0
                For Each drwPerson As BusinessObject.dstUser.spr_User_CheckBranch_SelectRow In dtblPerson

                    strTemp = strTemp & "<Option value='" & drwPerson.ID & "(" & drwRTotalDeffredLC.LCNumber & ")" & drwRTotalDeffredLC.CustomerNO & "," & drwRTotalDeffredLC.FullName & ";" & drwRTotalDeffredLC.MobileNO & ";" & drwRTotalDeffredLC.LoanTypeCode & "/'>" & drwPerson.Username & "</Option>"


                Next

                strTemp = strTemp & "</Select>"



            End If


            TbCell = New HtmlTableCell
            TbCell.InnerHtml = strTemp
            TbCell.NoWrap = True
            TbCell.Align = "center"
            TbRow.Cells.Add(TbCell)



            i += 1



            tblNumbers.Rows.Add(TbRow)

        Next



    End Sub

    ''Private Sub ShowDelaiedFiles(ByVal NotPaidDay As Integer)

    ''    Dim strBranchCode As String = cmbBranch.SelectedItem.Text.Substring(0, cmbBranch.SelectedItem.Text.IndexOf("("))

    ''    Dim dtblUserLogin As BusinessObject.dstUser.spr_User_Login_SelectDataTable = CType(Session("dtblUserLogin"), BusinessObject.dstUser.spr_User_Login_SelectDataTable)
    ''    Dim drwUserLogin As BusinessObject.dstUser.spr_User_Login_SelectRow = dtblUserLogin.Rows(0)

    ''    Dim tadpBranch As New BusinessObject.dstBranchTableAdapters.spr_Branch_ByCode_SelectTableAdapter
    ''    Dim dtblBranch As BusinessObject.dstBranch.spr_Branch_ByCode_SelectDataTable = Nothing

    ''    dtblBranch = tadpBranch.GetData(strBranchCode)

    ''    Dim tadpTotalDeffredForAssign As New BusinessObject.dstTotalDeffredLCTableAdapters.spr_TotalDeffredLCFileAssign_SelectTableAdapter
    ''    Dim dtblTotalDeffredForAssign As BusinessObject.dstTotalDeffredLC.spr_TotalDeffredLCFileAssign_SelectDataTable = Nothing

    ''    dtblTotalDeffredForAssign = tadpTotalDeffredForAssign.GetData(strBranchCode, NotPaidDay, NotPaidDay, dtblBranch.First.ID, 1, -1, -1, -1)

    ''    Dim dtblPerson As BusinessObject.dstUser.spr_User_CheckBranch_SelectDataTable = Nothing
    ''    Dim tadpPerson As New BusinessObject.dstUserTableAdapters.spr_User_CheckBranch_SelectTableAdapter


    ''    Dim strchklstFiles As String = ""
    ''    Dim i As Integer = 1
    ''    For Each drwRTotalDeffredLC As BusinessObject.dstTotalDeffredLC.spr_TotalDeffredLCFileAssign_SelectRow In dtblTotalDeffredForAssign.Rows

    ''        Dim TbRow As New HtmlTableRow
    ''        Dim TbCell As HtmlTableCell


    ''        TbCell = New HtmlTableCell
    ''        ' '"<input type='hidden' id='hdnAmounts" & CStr(i) & "' value='" & drwRTotalDeffredLC.LCNumber & "') >"
    ''        TbCell.InnerHtml = CStr(i)
    ''        TbCell.Align = "center"
    ''        TbCell.NoWrap = True
    ''        TbRow.Cells.Add(TbCell)

    ''        TbCell = New HtmlTableCell
    ''        strchklstFiles = "<input type='checkbox' name='" & drwRTotalDeffredLC.LCNumber.ToString() & ";" & drwRTotalDeffredLC.CustomerNO.ToString() & ";" & drwRTotalDeffredLC.MobileNO.ToString() & ";" & drwRTotalDeffredLC.LoanTypeCode.ToString() & ";" & drwRTotalDeffredLC.FullName.ToString() & "' /> "
    ''        TbCell.InnerHtml = strchklstFiles
    ''        TbCell.Align = "center"
    ''        TbCell.NoWrap = True
    ''        TbRow.Cells.Add(TbCell)



    ''        TbCell = New HtmlTableCell
    ''        TbCell.InnerHtml = drwRTotalDeffredLC.FullName
    ''        TbCell.NoWrap = True
    ''        TbCell.Align = "center"
    ''        TbRow.Cells.Add(TbCell)

    ''        TbCell = New HtmlTableCell
    ''        TbCell.InnerHtml = drwRTotalDeffredLC.LCNumber
    ''        TbCell.NoWrap = True
    ''        TbCell.Align = "center"
    ''        TbRow.Cells.Add(TbCell)


    ''        TbCell = New HtmlTableCell
    ''        TbCell.InnerHtml = drwRTotalDeffredLC.InstallmentsCount
    ''        TbCell.NoWrap = True
    ''        TbCell.Align = "center"
    ''        TbRow.Cells.Add(TbCell)

    ''        TbCell = New HtmlTableCell
    ''        TbCell.InnerHtml = drwRTotalDeffredLC.LCAmount.ToString("N0")
    ''        TbCell.NoWrap = True
    ''        TbCell.Align = "center"
    ''        TbRow.Cells.Add(TbCell)

    ''        TbCell = New HtmlTableCell
    ''        TbCell.InnerHtml = drwRTotalDeffredLC.LoanTypeName
    ''        TbCell.NoWrap = True
    ''        TbCell.Align = "center"
    ''        TbRow.Cells.Add(TbCell)

    ''        TbCell = New HtmlTableCell
    ''        TbCell.InnerHtml = drwRTotalDeffredLC.AmounDefferd.ToString("N0")
    ''        TbCell.NoWrap = True
    ''        TbCell.Align = "center"
    ''        TbRow.Cells.Add(TbCell)

    ''        TbCell = New HtmlTableCell
    ''        TbCell.InnerHtml = drwRTotalDeffredLC.NotPiadDurationDay
    ''        TbCell.NoWrap = True
    ''        TbCell.Align = "center"
    ''        TbRow.Cells.Add(TbCell)


    ''        Dim strTemp As String = ""

    ''        If drwUserLogin.IsDataAdmin = True Then



    ''            strTemp = "<select name='cmbAssignPerson" & CStr(i) & "' style='WIDTH:50;HEIGHT:25px;background-color:rgba(0,0,0,0.05)'>"
    ''            strTemp = strTemp & "<Option value='-1' selected>انتخاب شود</Option>"

    ''            dtblPerson = tadpPerson.GetData(1, cmbBranch.SelectedValue, -1)
    ''            Dim j As Integer = 0
    ''            For Each drwPerson As BusinessObject.dstUser.spr_User_CheckBranch_SelectRow In dtblPerson

    ''                strTemp = strTemp & "<Option value='" & drwPerson.ID & "(" & drwRTotalDeffredLC.LCNumber & ")" & drwRTotalDeffredLC.CustomerNO & "," & drwRTotalDeffredLC.FullName & ";" & drwRTotalDeffredLC.MobileNO & ";" & drwRTotalDeffredLC.LoanTypeCode & "/'>" & drwPerson.Username & "</Option>"

    ''            Next

    ''            strTemp = strTemp & "</Select>"

    ''        ElseIf drwUserLogin.FK_AccessGroupID = 3438 Then

    ''            strTemp = "<select name='cmbAssignPerson" & CStr(i) & "' style='WIDTH:50;HEIGHT:25px;background-color:rgba(0,0,0,0.05)'>"
    ''            strTemp = strTemp & "<Option value='-1' selected>انتخاب شود</Option>"

    ''            dtblPerson = tadpPerson.GetData(4, -1, -1)

    ''            Dim j As Integer = 0
    ''            For Each drwPerson As BusinessObject.dstUser.spr_User_CheckBranch_SelectRow In dtblPerson

    ''                strTemp = strTemp & "<Option value='" & drwPerson.ID & "(" & drwRTotalDeffredLC.LCNumber & ")" & drwRTotalDeffredLC.CustomerNO & "," & drwRTotalDeffredLC.FullName & ";" & drwRTotalDeffredLC.MobileNO & ";" & drwRTotalDeffredLC.LoanTypeCode & "/'>" & drwPerson.Username & "</Option>"


    ''            Next

    ''            strTemp = strTemp & "</Select>"


    ''        ElseIf drwUserLogin.IsDataAdmin = False AndAlso drwUserLogin.IsDataUserAdmin = True Then


    ''            strTemp = "<select name='cmbAssignPerson" & CStr(i) & "' style='WIDTH:50;HEIGHT:25px;background-color:rgba(0,0,0,0.05)'>"
    ''            strTemp = strTemp & "<Option value='-1' selected>انتخاب شود</Option>"

    ''            dtblPerson = tadpPerson.GetData(1, cmbBranch.SelectedValue, -1)

    ''            Dim j As Integer = 0
    ''            For Each drwPerson As BusinessObject.dstUser.spr_User_CheckBranch_SelectRow In dtblPerson

    ''                strTemp = strTemp & "<Option value='" & drwPerson.ID & "(" & drwRTotalDeffredLC.LCNumber & ")" & drwRTotalDeffredLC.CustomerNO & "," & drwRTotalDeffredLC.FullName & ";" & drwRTotalDeffredLC.MobileNO & ";" & drwRTotalDeffredLC.LoanTypeCode & "/'>" & drwPerson.Username & "</Option>"


    ''            Next

    ''            strTemp = strTemp & "</Select>"


    ''        ElseIf drwUserLogin.IsDataAdmin = False AndAlso drwUserLogin.IsDataUserAdmin = False Then


    ''            strTemp = "<select name='cmbAssignPerson" & CStr(i) & "' style='WIDTH:50;HEIGHT:25px;background-color:rgba(0,0,0,0.05)'>"
    ''            strTemp = strTemp & "<Option value='-1' selected>انتخاب شود</Option>"

    ''            dtblPerson = tadpPerson.GetData(1, cmbBranch.SelectedValue, -1)
    ''            Dim j As Integer = 0
    ''            For Each drwPerson As BusinessObject.dstUser.spr_User_CheckBranch_SelectRow In dtblPerson

    ''                strTemp = strTemp & "<Option value='" & drwPerson.ID & "(" & drwRTotalDeffredLC.LCNumber & ")" & drwRTotalDeffredLC.CustomerNO & "," & drwRTotalDeffredLC.FullName & ";" & drwRTotalDeffredLC.MobileNO & ";" & drwRTotalDeffredLC.LoanTypeCode & "/'>" & drwPerson.Username & "</Option>"


    ''            Next

    ''            strTemp = strTemp & "</Select>"



    ''        End If


    ''        TbCell = New HtmlTableCell
    ''        TbCell.InnerHtml = strTemp
    ''        TbCell.NoWrap = True
    ''        TbCell.Align = "center"
    ''        TbRow.Cells.Add(TbCell)



    ''        i += 1



    ''        tblNumbers.Rows.Add(TbRow)

    ''    Next

    ''End Sub

    ''Private Sub ShowFiles(FileType As String, RoleID As Integer, ProvinceID As Integer, BranchCode As String, MassiveFilePeriod As Integer, DueDateFilePeroid As Integer, DueDateRecivedPeriod As Integer, DoubtfulPaidPeriod As Integer, DeferredPeriod As Integer)

    ''    Dim tadTotalDeffredLCByProvinceDoAssign As New BusinessObject.dstTotalDeffredLCTableAdapters.spr_TotalDeffredLCByProvinceDoAssign_SelectTableAdapter
    ''    Dim dtblTotalDeffredLCByProvinceDoAssign As BusinessObject.dstTotalDeffredLC.spr_TotalDeffredLCByProvinceDoAssign_SelectDataTable = Nothing

    ''    Dim tadTotalDeffredLCByBranchDoAssign As New BusinessObject.dstTotalDeffredLCTableAdapters.spr_TotalDeffredLCByBranchDoAssign_SelectTableAdapter
    ''    Dim dtblTotalDeffredLCByBranchDoAssign As BusinessObject.dstTotalDeffredLC.spr_TotalDeffredLCByBranchDoAssign_SelectDataTable = Nothing


    ''    If FileType = "MassiveFile" AndAlso RoleID = 1 Then

    ''        dtblTotalDeffredLCByProvinceDoAssign = tadTotalDeffredLCByProvinceDoAssign.GetData(1, ProvinceID, MassiveFilePeriod, -1, -1, -1, -1)


    ''    ElseIf FileType = "MassiveFile" AndAlso RoleID = 2 Then

    ''        dtblTotalDeffredLCByBranchDoAssign = tadTotalDeffredLCByBranchDoAssign.GetData(1, BranchCode, MassiveFilePeriod, -1, -1, -1, -1)

    ''    ElseIf FileType = "DueDateFilePeroid" AndAlso RoleID = 1 Then

    ''        dtblTotalDeffredLCByProvinceDoAssign = tadTotalDeffredLCByProvinceDoAssign.GetData(2, ProvinceID, -1, DueDateFilePeroid, -1, -1, -1)

    ''    ElseIf FileType = "DueDateFilePeroid" AndAlso RoleID = 2 Then

    ''        dtblTotalDeffredLCByProvinceDoAssign = tadTotalDeffredLCByProvinceDoAssign.GetData(2, -1, BranchCode, DueDateFilePeroid, -1, -1, -1)

    ''    ElseIf FileType = "DueDateRecivedPeriod" AndAlso RoleID = 1 Then

    ''        dtblTotalDeffredLCByProvinceDoAssign = tadTotalDeffredLCByProvinceDoAssign.GetData(3, ProvinceID, -1, -1, DueDateRecivedPeriod, DoubtfulPaidPeriod, -1)

    ''    ElseIf FileType = "DueDateRecivedPeriod" AndAlso RoleID = 2 Then

    ''        dtblTotalDeffredLCByProvinceDoAssign = tadTotalDeffredLCByProvinceDoAssign.GetData(3, -1, BranchCode, -1, DueDateRecivedPeriod, DoubtfulPaidPeriod, -1)

    ''    ElseIf FileType = "DoubtfulPaidPeriod" AndAlso RoleID = 1 Then

    ''        dtblTotalDeffredLCByProvinceDoAssign = tadTotalDeffredLCByProvinceDoAssign.GetData(4, ProvinceID, -1, -1, -1, DoubtfulPaidPeriod, -1)

    ''    ElseIf FileType = "DoubtfulPaidPeriod" AndAlso RoleID = 2 Then

    ''        dtblTotalDeffredLCByProvinceDoAssign = tadTotalDeffredLCByProvinceDoAssign.GetData(4, -1, BranchCode, -1, -1, DoubtfulPaidPeriod, -1)

    ''    ElseIf FileType = "DeferredPeriod" AndAlso RoleID = 1 Then

    ''        dtblTotalDeffredLCByProvinceDoAssign = tadTotalDeffredLCByProvinceDoAssign.GetData(5, ProvinceID, -1, -1, -1, -1, DeferredPeriod)

    ''    ElseIf FileType = "DeferredPeriod" AndAlso RoleID = 2 Then

    ''        dtblTotalDeffredLCByProvinceDoAssign = tadTotalDeffredLCByProvinceDoAssign.GetData(5, -1, BranchCode, -1, -1, -1, DeferredPeriod)
    ''    End If



    ''End Sub

    Protected Sub cmbProvince_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbProvince.SelectedIndexChanged

        odsBranch.SelectParameters.Item("Action").DefaultValue = 2
        odsBranch.SelectParameters.Item("ProvinceID").DefaultValue = cmbProvince.SelectedValue
        odsBranch.DataBind()

        cmbBranch.DataSourceID = "odsBranch"
        cmbBranch.DataTextField = "BrnachCode"
        cmbBranch.DataValueField = "ID"


        cmbBranch.DataBind()

        ''  cmbPerson.DataBind()

    End Sub

    Protected Sub cmbBranch_DataBound(sender As Object, e As EventArgs) Handles cmbBranch.DataBound
        Dim li As New ListItem
        li.Text = "---"
        li.Value = -1
        cmbBranch.Items.Insert(0, li)
    End Sub

    Protected Sub cmbBranch_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbBranch.SelectedIndexChanged

        ''odsPerson.SelectParameters.Item("Action").DefaultValue = 1
        ''odsPerson.SelectParameters.Item("BranchID").DefaultValue = cmbBranch.SelectedValue
        ''odsPerson.SelectParameters.Item("ProvinceID").DefaultValue = -1

        ''   cmbPerson.DataBind()


    End Sub

    ''Protected Sub cmbPerson_DataBound(sender As Object, e As EventArgs) Handles cmbPerson.DataBound
    ''    Dim li As New ListItem
    ''    li.Text = "---"
    ''    li.Value = -1
    ''    cmbPerson.Items.Insert(0, li)
    ''End Sub

    Private Sub Bootstrap_Panel1_Panel_Cancel_Click(sender As Object, e As EventArgs) Handles Bootstrap_Panel1.Panel_Cancel_Click

        Response.Redirect("HandyFollowManagement.aspx")

    End Sub

    ''Protected Sub rdoSingleSelect_CheckedChanged(sender As Object, e As EventArgs) Handles rdoSingleSelect.CheckedChanged

    ''    If rdoSingleSelect.Checked = True Then
    ''        cmbPerson.Enabled = False
    ''    End If


    ''End Sub

    ''Protected Sub rdoGroupSelect_CheckedChanged(sender As Object, e As EventArgs) Handles rdoGroupSelect.CheckedChanged

    ''    If rdoGroupSelect.Checked = True Then
    ''        cmbPerson.Enabled = True
    ''    End If

    ''End Sub


    Public Function CheckFileAssign(AssignUerID As Integer, LoanID As Integer, AssignType As Short) As Boolean

        Dim tadpHandyFollowAssignByIDs As New BusinessObject.dstHandyFollowTableAdapters.spr_HandyFollowAssignByIDs_SelectTableAdapter
        Dim dtblHandyFollowAssignByIDs As BusinessObject.dstHandyFollow.spr_HandyFollowAssignByIDs_SelectDataTable = Nothing

        dtblHandyFollowAssignByIDs = tadpHandyFollowAssignByIDs.GetData(2, AssignUerID, -1, LoanID, AssignType)

        If dtblHandyFollowAssignByIDs.Rows.Count > 0 Then

            Return True
        End If

        Return False

    End Function

    Protected Sub cmbAssignType_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbAssignType.SelectedIndexChanged
        If cmbAssignType.SelectedValue = 2 Then

            cmbPerson.Enabled = True
        Else
            cmbPerson.Enabled = False
        End If
    End Sub

    Protected Sub cmbDeferredPeriod_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbDeferredPeriod.SelectedIndexChanged
        If cmbDeferredPeriod.SelectedValue <> -1 Then

            txtNotPiadDurationDayFrom.Enabled = False
            txtNotPiadDurationDayTo.Enabled = False
        Else
            txtNotPiadDurationDayFrom.Enabled = True
            txtNotPiadDurationDayTo.Enabled = True
        End If
    End Sub

    Protected Sub cmbLoanType_DataBound(sender As Object, e As EventArgs) Handles cmbLoanType.DataBound
        Dim li As New ListItem
        li.Text = "---"
        li.Value = -1
        cmbLoanType.Items.Insert(0, li)
    End Sub
End Class