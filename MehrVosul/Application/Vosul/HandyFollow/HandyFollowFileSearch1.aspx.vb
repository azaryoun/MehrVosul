Imports System.Data.OracleClient
Public Class HandyFollowFileSearch1
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

        If Page.IsPostBack = False Then

            ''Get current User Brnach

            Dim dtblUserLogin As BusinessObject.dstUser.spr_User_Login_SelectDataTable = CType(Session("dtblUserLogin"), BusinessObject.dstUser.spr_User_Login_SelectDataTable)
            Dim drwUserLogin As BusinessObject.dstUser.spr_User_Login_SelectRow = dtblUserLogin.Rows(0)

            If Not Request.QueryString("CustomerNo") Is Nothing Then


                txtCustomerNO.Text = Request.QueryString("CustomerNo")


                DisplayFileList()

            End If


        End If

        If hdnAction.Value.StartsWith("S") = True Then
            Dim intFileID As Long = CLng(hdnAction.Value.Split(";")(1))
            Dim intLoanID As Long = CLng(hdnAction.Value.Split(";")(2))
            Session("AmountDeffed") = hdnAction.Value.Split(";")(3)
            Session("intFileID") = CObj(intFileID)
            Session("intLoanID") = CObj(intLoanID)


            Session("customerNO") = CObj(txtCustomerNO.Text)
            Response.Redirect("HandyFollowNew.aspx")
        End If

    End Sub

    Private Sub Bootstrap_Panel1_Panel_Display_Click(sender As Object, e As EventArgs) Handles Bootstrap_Panel1.Panel_Display_Click
        DisplayFileList()
    End Sub

    ''' <summary>
    ''' 
    ''' </summary>
    Private Sub DisplayFileList()


        ''First Check Customer No and BranchCode if the user is branch admin or normal user
        Bootstrap_Panel1.ClearMessage()

        Dim blnHasFind As Boolean = False
        Dim blnShowFile As Boolean = False

        Dim intCount As Integer = 0

        Dim dtblUserLogin As BusinessObject.dstUser.spr_User_Login_SelectDataTable = CType(Session("dtblUserLogin"), BusinessObject.dstUser.spr_User_Login_SelectDataTable)
        Dim drwUserLogin As BusinessObject.dstUser.spr_User_Login_SelectRow = dtblUserLogin.Rows(0)


        ''check the access Group
        Dim tadpAccessgroupUser As New BusinessObject.dstAccessgroupUserTableAdapters.spr_AccessgroupUserByID_SelectTableAdapter
        Dim dtblAccessgroupUser As BusinessObject.dstAccessgroupUser.spr_AccessgroupUserByID_SelectDataTable = Nothing
        Dim blnProvinceAdmin As Boolean = False
        Dim blnBranchAdmin As Boolean = False
        Dim blnNormalUser As Boolean = False

        ''ادمین  استان ها
        dtblAccessgroupUser = tadpAccessgroupUser.GetData(drwUserLogin.ID, 3431)
        If dtblAccessgroupUser.Rows.Count > 0 AndAlso drwUserLogin.IsDataUserAdmin = True Then
            blnProvinceAdmin = True
        End If

        ''کارشناس حقوقی
        dtblAccessgroupUser = tadpAccessgroupUser.GetData(drwUserLogin.ID, 3437)
        If dtblAccessgroupUser.Rows.Count > 0 AndAlso drwUserLogin.IsDataUserAdmin = True Then
            blnProvinceAdmin = True
        End If

        ''مدیر شعبه
        dtblAccessgroupUser = tadpAccessgroupUser.GetData(drwUserLogin.ID, 3436)
        If dtblAccessgroupUser.Rows.Count > 0 Then
            blnBranchAdmin = True
        End If


        Dim blnFollowable As Boolean = True


        Dim strCustomerNO As String = txtCustomerNO.Text.Trim()
        Dim tadpHandyFollowSearch As New BusinessObject.dstHandyFollowTableAdapters.spr_HandyFollow_SearchTableAdapter
        Dim dtblHandyFollowSearch As BusinessObject.dstHandyFollow.spr_HandyFollow_SearchDataTable = Nothing

        Dim tadpBranch As New BusinessObject.dstBranchTableAdapters.spr_Branch_SelectTableAdapter
        Dim dtblBranch As BusinessObject.dstBranch.spr_Branch_SelectDataTable = Nothing

        dtblBranch = tadpBranch.GetData(drwUserLogin.FK_BrnachID)


        If blnProvinceAdmin = False AndAlso blnBranchAdmin = False Then

            blnNormalUser = True
            '' Normal User 
            dtblHandyFollowSearch = tadpHandyFollowSearch.GetData(1, strCustomerNO, dtblBranch.First.BrnachCode, -1)

        ElseIf blnProvinceAdmin = True Then

            dtblHandyFollowSearch = tadpHandyFollowSearch.GetData(2, strCustomerNO, -1, drwUserLogin.Fk_ProvinceID)

        Else

            dtblHandyFollowSearch = tadpHandyFollowSearch.GetData(1, strCustomerNO, dtblBranch.First.BrnachCode, -1)


        End If

        If dtblHandyFollowSearch.Rows.Count > 0 Then



            For Each drwHandyFollowSerch As BusinessObject.dstHandyFollow.spr_HandyFollow_SearchRow In dtblHandyFollowSearch

                Dim strBranchName As String = ""
                Dim intBranchID As Integer = GetBranchID(drwHandyFollowSerch.BranchCode, strBranchName)

                ''check LoanID to check im handy follow assign if is normal user
                Dim tadpLoan As New BusinessObject.dstLoanTableAdapters.spr_Loan_ByLoanNumber_SelectTableAdapter
                Dim dtblLoan As BusinessObject.dstLoan.spr_Loan_ByLoanNumber_SelectDataTable = Nothing

                ''get file id 
                Dim tadpFile As New BusinessObject.dstFileTableAdapters.spr_File_SelectTableAdapter
                Dim dtblFile As BusinessObject.dstFile.spr_File_SelectDataTable = Nothing

                dtblFile = tadpFile.GetData(3, -1, strCustomerNO)
                Dim intFileID As Integer = -1
                Dim intLoanID As Integer = -1

                If dtblFile.Rows.Count = 0 Then

                    Dim intLoanTypeID As Integer
                    ''Insert File 
                    intFileID = InsertFile(drwHandyFollowSerch.CustomerNO, drwHandyFollowSerch.FullName, drwHandyFollowSerch.MobileNO, 1, drwHandyFollowSerch.LoanTypeCode, intLoanTypeID)

                    ''Insert  Loan
                    intLoanID = InsertLoan(intFileID, intLoanTypeID, intBranchID, drwHandyFollowSerch.LCNumber)

                Else

                    intFileID = dtblFile.First.ID

                    dtblLoan = tadpLoan.GetData(drwHandyFollowSerch.LCNumber, intFileID)
                    If dtblLoan.Rows.Count = 0 Then



                        Dim tadpLoanTypeByCode As New BusinessObject.dstLoanTypeTableAdapters.spr_LoanType_byCode_SelectTableAdapter
                        Dim dtblLoanTypeByCode As BusinessObject.dstLoanType.spr_LoanType_byCode_SelectDataTable = Nothing
                        dtblLoanTypeByCode = tadpLoanTypeByCode.GetData(drwHandyFollowSerch.LoanTypeCode)

                        Dim drwLoanTypeByCode As BusinessObject.dstLoanType.spr_LoanType_byCode_SelectRow = dtblLoanTypeByCode.Rows(0)
                        Dim intLoanTypeID As Integer = drwLoanTypeByCode.ID


                        ''insert Loan
                        intLoanID = InsertLoan(intFileID, intLoanTypeID, intBranchID, drwHandyFollowSerch.LCNumber)
                    Else


                        intLoanID = dtblLoan.First.ID
                    End If

                End If



                ''get current Loan's handy follow List
                Dim tadpHandyFollowAssign As New BusinessObject.dstHandyFollowTableAdapters.spr_HandyFollowAssignByLoanID_SelectTableAdapter
                Dim dtblHandyFollowAssign As BusinessObject.dstHandyFollow.spr_HandyFollowAssignByLoanID_SelectDataTable = Nothing

                dtblHandyFollowAssign = tadpHandyFollowAssign.GetData(intLoanID)


                Dim tadpUser As New BusinessObject.dstUserTableAdapters.spr_User_SelectTableAdapter
                Dim dtblUser As BusinessObject.dstUser.spr_User_SelectDataTable = Nothing
                Dim strAssignUserID As String = ""

                If dtblHandyFollowAssign.Rows.Count > 0 Then

                    dtblUser = tadpUser.GetData(dtblHandyFollowAssign.First.FK_AssignUserID)
                    If dtblHandyFollowAssign.First.IsAssigned = True Then
                        strAssignUserID = dtblUser.First.Username

                    End If


                    ''CHECK IF USER IS NORMAL USER SELECT THE RELATED HANDYFOLLOW ASSIGN
                    If blnNormalUser = True Then
                        If dtblHandyFollowAssign.First.IsAssigned = False OrElse dtblHandyFollowAssign.First.FK_AssignUserID = drwUserLogin.ID Then
                            blnHasFind = True
                            ''fill table

                        Else

                            blnShowFile = True
                            ''Continue For

                        End If
                    Else


                        ''fill table



                    End If

                Else

                    ''fill the table



                End If

                blnHasFind = True


                intCount += 1


                Dim TbCell As HtmlTableCell
                Dim TbRow As HtmlTableRow
                TbRow = New HtmlTableRow

                TbCell = New HtmlTableCell
                TbCell.InnerHtml = CStr(intCount)
                TbCell.Align = "center"
                TbCell.NoWrap = True
                TbRow.Cells.Add(TbCell)


                TbCell = New HtmlTableCell
                TbCell.InnerHtml = txtCustomerNO.Text.Trim
                TbCell.NoWrap = True

                TbCell.Align = "center"
                TbRow.Cells.Add(TbCell)

                TbCell = New HtmlTableCell
                TbCell.InnerHtml = drwHandyFollowSerch.FullName
                TbCell.NoWrap = False
                TbCell.Width = "100px"
                TbCell.Align = "center"
                TbRow.Cells.Add(TbCell)

                TbCell = New HtmlTableCell
                TbCell.InnerHtml = drwHandyFollowSerch.LCNumber
                TbCell.NoWrap = True
                TbCell.Align = "center"
                TbRow.Cells.Add(TbCell)


                TbCell = New HtmlTableCell
                TbCell.InnerHtml = drwHandyFollowSerch.MobileNO
                TbCell.NoWrap = True
                TbCell.Align = "center"
                TbRow.Cells.Add(TbCell)


                TbCell = New HtmlTableCell
                TbCell.InnerHtml = drwHandyFollowSerch.NotPiadDurationDay
                TbCell.NoWrap = True
                TbCell.Width = "50px"
                TbCell.Align = "center"
                TbRow.Cells.Add(TbCell)

                TbCell = New HtmlTableCell
                TbCell.InnerHtml = strBranchName & "(" & drwHandyFollowSerch.BranchCode & ")"
                TbCell.NoWrap = False
                TbCell.Width = "120px"
                TbCell.Align = "center"
                TbRow.Cells.Add(TbCell)


                ''get the last handyfollow
                Dim tadpLastHandyFollow As New BusinessObject.dstHandyFollowTableAdapters.spr_HandyFollow_SelectTableAdapter
                Dim dtblLastHandyFollow As BusinessObject.dstHandyFollow.spr_HandyFollow_SelectDataTable = Nothing

                dtblLastHandyFollow = tadpLastHandyFollow.GetData(4, -1, intLoanID, -1, -1)

                If dtblLastHandyFollow.Rows.Count > 0 Then

                    TbCell = New HtmlTableCell
                    TbCell.InnerHtml = If(dtblLastHandyFollow.First.IsRemarksNull = False, dtblLastHandyFollow.First.Remarks, "---")
                    TbCell.NoWrap = True
                    TbCell.Width = "50px"
                    TbCell.Align = "center"
                    TbRow.Cells.Add(TbCell)

                    TbCell = New HtmlTableCell
                    TbCell.InnerHtml = If(dtblLastHandyFollow.First.IsDutyDateNull = False, mdlGeneral.GetPersianDate(dtblLastHandyFollow.First.DutyDate), "---")
                    TbCell.NoWrap = False
                    TbCell.Width = "90px"
                    TbCell.Align = "center"
                    TbRow.Cells.Add(TbCell)


                    ''get username

                    dtblUser = tadpUser.GetData(dtblLastHandyFollow.First.FK_UserID)

                    TbCell = New HtmlTableCell
                    TbCell.InnerHtml = dtblUser.First.Username
                    TbCell.NoWrap = False
                    TbCell.Width = "120px"
                    TbCell.Align = "center"
                    TbRow.Cells.Add(TbCell)

                Else



                    TbCell = New HtmlTableCell
                    TbCell.InnerHtml = "---"
                    TbCell.NoWrap = True
                    TbCell.Width = "50px"
                    TbCell.Align = "center"
                    TbRow.Cells.Add(TbCell)

                    TbCell = New HtmlTableCell
                    TbCell.InnerHtml = "---"
                    TbCell.NoWrap = False
                    TbCell.Align = "center"
                    TbRow.Cells.Add(TbCell)

                    TbCell = New HtmlTableCell
                    TbCell.InnerHtml = "---"
                    TbCell.NoWrap = False
                    TbCell.Align = "center"
                    TbRow.Cells.Add(TbCell)

                End If

                TbCell = New HtmlTableCell
                TbCell.InnerHtml = strAssignUserID
                TbCell.NoWrap = True
                TbCell.Align = "center"
                TbRow.Cells.Add(TbCell)

                TbCell = New HtmlTableCell
                If blnShowFile = True Then
                    TbCell.InnerHtml = "ثبت پیگیری بدلیل تخصیص پرونده به کاربر دیگر امکان پذیر نمی باشد."
                Else
                    TbCell.InnerHtml = "<a ID='lnkbtnFollowing' href='#'  onclick= btnFollwoing_ClientClick(" & intFileID.ToString() & "," & intLoanID.ToString() & "," & drwHandyFollowSerch.AmounDefferd & ")>ثبت پیگیری</a>"
                End If

                TbCell.NoWrap = False
                TbCell.Align = "center"
                TbRow.Cells.Add(TbCell)


                If Not TbRow Is Nothing Then
                    tblResult.Rows.Add(TbRow)
                    divResult.Visible = True

                End If



            Next

        Else
            ''
            If blnProvinceAdmin = False Then
                Bootstrap_Panel1.ShowMessage("در حال حاضر وامی قابل پیگیری برای پرونده فوق یافت نشد", True)
                Exit Sub
            End If

            dtblHandyFollowSearch = tadpHandyFollowSearch.GetData(3, strCustomerNO, "", drwUserLogin.Fk_ProvinceID)
            If dtblHandyFollowSearch.Rows.Count > 0 Then


                For Each drwHandyFollowSerch As BusinessObject.dstHandyFollow.spr_HandyFollow_SearchRow In dtblHandyFollowSearch

                    Dim strBranchName As String = ""
                    Dim intBranchID As Integer = GetBranchID(drwHandyFollowSerch.BranchCode, strBranchName)


                    ''check LoanID to check im handy follow assign if is normal user
                    Dim tadpLoan As New BusinessObject.dstLoanTableAdapters.spr_Loan_ByLoanNumber_SelectTableAdapter
                    Dim dtblLoan As BusinessObject.dstLoan.spr_Loan_ByLoanNumber_SelectDataTable = Nothing

                    ''get file id 
                    Dim tadpFile As New BusinessObject.dstFileTableAdapters.spr_File_SelectTableAdapter
                    Dim dtblFile As BusinessObject.dstFile.spr_File_SelectDataTable = Nothing

                    dtblFile = tadpFile.GetData(3, -1, strCustomerNO)
                    Dim intFileID As Integer = -1
                    Dim intLoanID As Integer = -1

                    If dtblFile.Rows.Count = 0 Then

                        Dim intLoanTypeID As Integer
                        ''Insert File 
                        intFileID = InsertFile(drwHandyFollowSerch.CustomerNO, drwHandyFollowSerch.FullName, drwHandyFollowSerch.MobileNO, 1, drwHandyFollowSerch.LoanTypeCode, intLoanTypeID)


                        ''Insert  Loan
                        intLoanID = InsertLoan(intFileID, intLoanTypeID, intBranchID, drwHandyFollowSerch.LCNumber)

                    Else

                        intFileID = dtblFile.First.ID

                        dtblLoan = tadpLoan.GetData(drwHandyFollowSerch.LCNumber, intFileID)
                        If dtblLoan.Rows.Count = 0 Then


                            Dim tadpLoanTypeByCode As New BusinessObject.dstLoanTypeTableAdapters.spr_LoanType_byCode_SelectTableAdapter
                            Dim dtblLoanTypeByCode As BusinessObject.dstLoanType.spr_LoanType_byCode_SelectDataTable = Nothing
                            dtblLoanTypeByCode = tadpLoanTypeByCode.GetData(drwHandyFollowSerch.LoanTypeCode)

                            Dim drwLoanTypeByCode As BusinessObject.dstLoanType.spr_LoanType_byCode_SelectRow = dtblLoanTypeByCode.Rows(0)
                            Dim intLoanTypeID As Integer = drwLoanTypeByCode.ID


                            ''insert Loan
                            intLoanID = InsertLoan(intFileID, intLoanTypeID, intBranchID, drwHandyFollowSerch.LCNumber)
                        Else


                            intLoanID = dtblLoan.First.ID
                        End If

                    End If



                    ''get current Loan's handy follow List
                    Dim tadpHandyFollowAssign As New BusinessObject.dstHandyFollowTableAdapters.spr_HandyFollowAssignByLoanID_SelectTableAdapter
                    Dim dtblHandyFollowAssign As BusinessObject.dstHandyFollow.spr_HandyFollowAssignByLoanID_SelectDataTable = Nothing

                    dtblHandyFollowAssign = tadpHandyFollowAssign.GetData(intLoanID)


                    Dim tadpUser As New BusinessObject.dstUserTableAdapters.spr_User_SelectTableAdapter
                    Dim dtblUser As BusinessObject.dstUser.spr_User_SelectDataTable = Nothing
                    Dim strAssignUserID As String = ""

                    If dtblHandyFollowAssign.Rows.Count > 0 Then

                        dtblUser = tadpUser.GetData(dtblHandyFollowAssign.First.FK_AssignUserID)
                        If dtblHandyFollowAssign.First.IsAssigned = True Then
                            strAssignUserID = dtblUser.First.Username
                        End If

                        ''CHECK IF USER IS NORMAL USER SELECT THE RELATED HANDYFOLLOW ASSIGN
                        If blnNormalUser = True Then
                            If dtblHandyFollowAssign.First.IsAssigned = False OrElse dtblHandyFollowAssign.First.FK_AssignUserID = drwUserLogin.ID Then
                                blnHasFind = True
                                ''fill table

                            Else
                                blnShowFile = True
                                '' Continue For

                            End If
                        Else


                            ''fill table



                        End If

                    Else

                        ''fill the table



                    End If

                    blnHasFind = True


                    intCount += 1


                    Dim TbCell As HtmlTableCell
                    Dim TbRow As HtmlTableRow
                    TbRow = New HtmlTableRow

                    TbCell = New HtmlTableCell
                    TbCell.InnerHtml = CStr(intCount)
                    TbCell.Align = "center"
                    TbCell.NoWrap = True
                    TbRow.Cells.Add(TbCell)


                    TbCell = New HtmlTableCell
                    TbCell.InnerHtml = txtCustomerNO.Text.Trim
                    TbCell.NoWrap = True

                    TbCell.Align = "center"
                    TbRow.Cells.Add(TbCell)

                    TbCell = New HtmlTableCell
                    TbCell.InnerHtml = drwHandyFollowSerch.FullName
                    TbCell.NoWrap = False
                    TbCell.Width = "100px"
                    TbCell.Align = "center"
                    TbRow.Cells.Add(TbCell)

                    TbCell = New HtmlTableCell
                    TbCell.InnerHtml = drwHandyFollowSerch.LCNumber
                    TbCell.NoWrap = True
                    TbCell.Align = "center"
                    TbRow.Cells.Add(TbCell)


                    TbCell = New HtmlTableCell
                    TbCell.InnerHtml = drwHandyFollowSerch.MobileNO
                    TbCell.NoWrap = True
                    TbCell.Align = "center"
                    TbRow.Cells.Add(TbCell)


                    TbCell = New HtmlTableCell
                    TbCell.InnerHtml = drwHandyFollowSerch.NotPiadDurationDay
                    TbCell.NoWrap = True
                    TbCell.Width = "50px"
                    TbCell.Align = "center"
                    TbRow.Cells.Add(TbCell)

                    TbCell = New HtmlTableCell
                    TbCell.InnerHtml = strBranchName & "(" & drwHandyFollowSerch.BranchCode & ")"
                    TbCell.NoWrap = False
                    TbCell.Width = "120px"
                    TbCell.Align = "center"
                    TbRow.Cells.Add(TbCell)


                    ''get the last handyfollow
                    Dim tadpLastHandyFollow As New BusinessObject.dstHandyFollowTableAdapters.spr_HandyFollow_SelectTableAdapter
                    Dim dtblLastHandyFollow As BusinessObject.dstHandyFollow.spr_HandyFollow_SelectDataTable = Nothing

                    dtblLastHandyFollow = tadpLastHandyFollow.GetData(4, -1, intLoanID, -1, -1)

                    If dtblLastHandyFollow.Rows.Count > 0 Then

                        TbCell = New HtmlTableCell
                        TbCell.InnerHtml = If(dtblLastHandyFollow.First.IsRemarksNull = False, dtblLastHandyFollow.First.Remarks, "---")
                        TbCell.NoWrap = True
                        TbCell.Width = "50px"
                        TbCell.Align = "center"
                        TbRow.Cells.Add(TbCell)

                        TbCell = New HtmlTableCell
                        TbCell.InnerHtml = If(dtblLastHandyFollow.First.IsDutyDateNull = False, mdlGeneral.GetPersianDate(dtblLastHandyFollow.First.DutyDate), "---")
                        TbCell.NoWrap = False
                        TbCell.Width = "90px"
                        TbCell.Align = "center"
                        TbRow.Cells.Add(TbCell)


                        ''get username

                        dtblUser = tadpUser.GetData(dtblLastHandyFollow.First.FK_UserID)

                        TbCell = New HtmlTableCell
                        TbCell.InnerHtml = dtblUser.First.Username
                        TbCell.NoWrap = False
                        TbCell.Width = "120px"
                        TbCell.Align = "center"
                        TbRow.Cells.Add(TbCell)

                    Else



                        TbCell = New HtmlTableCell
                        TbCell.InnerHtml = "---"
                        TbCell.NoWrap = True
                        TbCell.Width = "50px"
                        TbCell.Align = "center"
                        TbRow.Cells.Add(TbCell)

                        TbCell = New HtmlTableCell
                        TbCell.InnerHtml = "---"
                        TbCell.NoWrap = False
                        TbCell.Align = "center"
                        TbRow.Cells.Add(TbCell)

                        TbCell = New HtmlTableCell
                        TbCell.InnerHtml = "---"
                        TbCell.NoWrap = False
                        TbCell.Align = "center"
                        TbRow.Cells.Add(TbCell)

                    End If

                    TbCell = New HtmlTableCell
                    TbCell.InnerHtml = strAssignUserID
                    TbCell.NoWrap = True
                    TbCell.Align = "center"
                    TbRow.Cells.Add(TbCell)

                    TbCell = New HtmlTableCell
                    TbCell.InnerHtml = "<a ID='lnkbtnFollowing' href='#'  onclick= btnFollwoing_ClientClick(" & intFileID.ToString() & "," & intLoanID.ToString() & "," & drwHandyFollowSerch.AmounDefferd & ")>ثبت پیگیری</a>"
                    TbCell.NoWrap = True
                    TbCell.Align = "center"
                    TbRow.Cells.Add(TbCell)


                    If Not TbRow Is Nothing Then
                        tblResult.Rows.Add(TbRow)
                        divResult.Visible = True

                    End If



                Next


            Else
                Bootstrap_Panel1.ShowMessage("در حال حاضر وامی قابل پیگیری برای پرونده فوق یافت نشد", True)
            End If

        End If



        If blnHasFind = False AndAlso blnShowFile = False Then

            Bootstrap_Panel1.ShowMessage("در حال حاضر وامی قابل پیگیری برای پرونده فوق یافت نشد", True)


        End If





    End Sub



    Private Function InsertFile(ByVal CustomerNO As String, ByVal FullName As String, ByVal MobileNo As String, ByVal UserID As Integer, ByVal LoanTypeCode As Integer, ByRef LoanTypeID As Integer) As Integer

        Dim qryFile As New BusinessObject.dstFileTableAdapters.QueriesTableAdapter


        Dim intFileID = qryFile.spr_File_Insert(CustomerNO, "", FullName, "", MobileNo, "", "", "", "", "", "", True, UserID, 1, Nothing, Nothing)

        Dim tadpLoanTypeByCode As New BusinessObject.dstLoanTypeTableAdapters.spr_LoanType_byCode_SelectTableAdapter
        Dim dtblLoanTypeByCode As BusinessObject.dstLoanType.spr_LoanType_byCode_SelectDataTable = Nothing
        dtblLoanTypeByCode = tadpLoanTypeByCode.GetData(LoanTypeCode)

        If dtblLoanTypeByCode.Rows.Count = 0 Then

            Dim strLoanTypeName As String = GetLoanTypeName(LoanTypeCode)
            Dim qryLoanType As New BusinessObject.dstLoanTypeTableAdapters.QueriesTableAdapter
            LoanTypeID = qryLoanType.spr_LoanType_Insert(LoanTypeCode, strLoanTypeName, 2, "")


        Else
            Dim drwLoanTypeByCode As BusinessObject.dstLoanType.spr_LoanType_byCode_SelectRow = dtblLoanTypeByCode.Rows(0)
            LoanTypeID = drwLoanTypeByCode.ID

        End If

        Return intFileID

    End Function

    Private Function InsertLoan(ByVal FileID As Integer, ByVal LoanTypeID As Integer, ByVal BranchID As Integer, ByVal LCNO As String) As Integer
        Try
            Dim qryLoan As New BusinessObject.dstLoanTableAdapters.QueriesTableAdapter

            Dim dteLoanDate? As Date = Nothing
            Dim intLoanID As Integer = qryLoan.spr_Loan_Insert(FileID, LoanTypeID, BranchID, dteLoanDate, LCNO, Nothing, Date.Now, Nothing, Nothing)

            Return intLoanID
        Catch ex As Exception

            Dim str As String = ex.Message

        End Try


    End Function

    Private Function GetBranchID(ByVal strBranchCode As String, ByRef strBranchName As String) As Integer

        Dim tadpBranchbyCode As New BusinessObject.dstBranchTableAdapters.spr_Branch_ByCode_SelectTableAdapter
        Dim dtblBranchbyCode As BusinessObject.dstBranch.spr_Branch_ByCode_SelectDataTable = Nothing
        dtblBranchbyCode = tadpBranchbyCode.GetData(strBranchCode)

        Dim intBranchID As Integer = -1


        If dtblBranchbyCode.Rows.Count = 0 Then

            strBranchName = ""
            Return -1


        Else

            Dim drwBranchbyCode As BusinessObject.dstBranch.spr_Branch_ByCode_SelectRow = dtblBranchbyCode.Rows(0)

            intBranchID = drwBranchbyCode.ID
            strBranchName = drwBranchbyCode.BranchName

        End If

        Return intBranchID

    End Function


    Private Function GetLoanTypeName(ByVal strLoanTypeCode As String) As String
        Try
            Dim strLoanTypeDesc As String = strLoanTypeCode

            Dim cnnBuiler_BI As New OracleConnectionStringBuilder()
            cnnBuiler_BI.DataSource = "10.35.1.37:1522/bidb"
            cnnBuiler_BI.UserID = "deposit"
            cnnBuiler_BI.Password = "deposit"
            cnnBuiler_BI.Unicode = True

            Using cnnBI_Connection As New OracleConnection(cnnBuiler_BI.ConnectionString)

                Dim cmd_BI As OracleCommand = cnnBI_Connection.CreateCommand()
                Dim strLoanType As String = "SELECT * from lfloantyp where LNMINORTP='" & strLoanTypeCode & "'"
                cmd_BI.CommandText = strLoanType

                Try
                    cnnBI_Connection.Open()
                Catch ex As Exception
                    Return strLoanTypeDesc
                End Try

                Dim dataReader As OracleDataReader = Nothing
                dataReader = cmd_BI.ExecuteReader()

                If dataReader.Read = False Then
                    dataReader.Close()
                    cnnBI_Connection.Close()
                    Return strLoanTypeDesc
                End If


                If dataReader.GetValue(3) Is DBNull.Value Then
                    strLoanTypeDesc = strLoanTypeCode
                Else
                    strLoanTypeDesc = CStr(dataReader.GetValue(3)).Trim.Replace("'", "")
                End If



                dataReader.Close()
                cnnBI_Connection.Close()

            End Using

            Return strLoanTypeDesc

        Catch ex As Exception
            Return strLoanTypeCode
        End Try

    End Function
End Class