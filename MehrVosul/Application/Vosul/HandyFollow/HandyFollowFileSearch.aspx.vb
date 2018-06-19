Imports System.Data.OracleClient
Public Class HandyFollowFileSearch
    Inherits System.Web.UI.Page
    Private Structure stc_Loan_Info

        Public FullName As String '0
        Public Mobile As String '4
        Public LC_No As String '6
        Public BranchCode As String '7
        Public LoanTypeCode As String '8
        Public CustomerNo As String '9
        Public LoanSerial As Integer '10
        Public LCDate As String '11

        Public LCAmount As Double? '12

        Public IstlPaid As Integer? '16
        Public AmounDefferd As Double? '17

        Public NotPiadDurationDay As Integer? '21

    End Structure

    Private Structure stc_Branch_Info
        Public BranchName As String
        Public BranchAddress As String
    End Structure
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

    Protected Sub DisplayFileList()


        Bootstrap_Panel1.ClearMessage()

        Dim i As Integer
        Dim  intCount As Integer = 0

        Dim dtblUserLogin As BusinessObject.dstUser.spr_User_Login_SelectDataTable = CType(Session("dtblUserLogin"), BusinessObject.dstUser.spr_User_Login_SelectDataTable)
        Dim drwUserLogin As BusinessObject.dstUser.spr_User_Login_SelectRow = dtblUserLogin.Rows(0)


        ''check the access Group
        Dim tadpAccessgroupUser As New BusinessObject.dstAccessgroupUserTableAdapters.spr_AccessgroupUserByID_SelectTableAdapter
        Dim dtblAccessgroupUser As BusinessObject.dstAccessgroupUser.spr_AccessgroupUserByID_SelectDataTable = Nothing
        Dim blnProvinceAdmin As Boolean = False

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



        Dim tadpHandyFollow As New BusinessObject.dstHandyFollowTableAdapters.spr_HandyFollow_CheckFileLoan_SelectTableAdapter
        Dim dtblhandyFollow As BusinessObject.dstHandyFollow.spr_HandyFollow_CheckFileLoan_SelectDataTable = Nothing


        Dim qryErrorLog As New BusinessObject.dstLoginLogTableAdapters.QueriesTableAdapter

        ''Load Current Customer loan realted to current user branch and it's handyfollow
        Dim tadpFilebyCustomerNo As New BusinessObject.dstFileTableAdapters.spr_File_SelectTableAdapter

        Dim dtblFilebyCustomerNo As BusinessObject.dstFile.spr_File_SelectDataTable = Nothing

        dtblFilebyCustomerNo = tadpFilebyCustomerNo.GetData(3, -1, txtCustomerNO.Text.Trim)

        If dtblFilebyCustomerNo.Rows.Count > 0 Then


            Dim tadpLoan As New BusinessObject.dstLoanTableAdapters.spr_Loan_SelectTableAdapter
            Dim dtblLoan As BusinessObject.dstLoan.spr_Loan_SelectDataTable = Nothing

            If drwUserLogin.IsDataAdmin = True Then
                dtblLoan = tadpLoan.GetData(3, -1, dtblFilebyCustomerNo.First.ID, -1)
            Else
                dtblLoan = tadpLoan.GetData(4, -1, dtblFilebyCustomerNo.First.ID, drwUserLogin.FK_BrnachID)
            End If



            If dtblLoan.Rows.Count > 0 Then
                ''get current Loan's handy follow List

                For Each drwLoan As BusinessObject.dstLoan.spr_Loan_SelectRow In dtblLoan


                    Dim tadpTotalDeffredLC As New BusinessObject.dstTotalDeffredLCTableAdapters.spr_TotalDeffredLC_SelectByLCNOTableAdapter
                    Dim dtblTotalDeffredLC As BusinessObject.dstTotalDeffredLC.spr_TotalDeffredLC_SelectByLCNODataTable = Nothing
                    Dim NotPiadDurationDay As String = ""
                    Dim AmountDeffered As String = ""

                    dtblTotalDeffredLC = tadpTotalDeffredLC.GetData(drwLoan.LoanNumber)

                    If dtblTotalDeffredLC.Rows.Count > 0 Then
                        NotPiadDurationDay = dtblTotalDeffredLC.First.NotPiadDurationDay.ToString()
                        AmountDeffered = dtblTotalDeffredLC.First.AmounDefferd.ToString()
                    End If


                    ''getbranchname
                    Dim tadpBranch As New BusinessObject.dstBranchTableAdapters.spr_Branch_SelectTableAdapter
                    Dim dtblBranch As BusinessObject.dstBranch.spr_Branch_SelectDataTable = Nothing
                    dtblBranch = tadpBranch.GetData(drwLoan.FK_BranchID)

                    ''check handyfollow is assigned to any user or not
                    Dim tadpHandyFolloAssign As New BusinessObject.dstHandyFollowTableAdapters.spr_HandyFollowAssignByIDs_SelectTableAdapter
                    Dim dtblHandyFollowAssign As BusinessObject.dstHandyFollow.spr_HandyFollowAssignByIDs_SelectDataTable = Nothing
                    Dim strAssighedUser As String = ""

                    dtblHandyFollowAssign = tadpHandyFolloAssign.GetData(1, -1, dtblFilebyCustomerNo.First.ID, drwLoan.ID, 1)
                    If dtblHandyFollowAssign.Rows.Count > 0 Then

                        Dim tadpUser As New BusinessObject.dstUserTableAdapters.spr_User_SelectTableAdapter
                        Dim dtblUser As BusinessObject.dstUser.spr_User_SelectDataTable = Nothing

                        dtblUser = tadpUser.GetData(dtblHandyFollowAssign.First.FK_AssignUserID)
                        strAssighedUser = dtblUser.First.Username


                    End If

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
                    TbCell.InnerHtml = dtblFilebyCustomerNo.First.FName & " " & dtblFilebyCustomerNo.First.LName
                    TbCell.NoWrap = False
                    TbCell.Width = "100px"
                    TbCell.Align = "center"
                    TbRow.Cells.Add(TbCell)

                    TbCell = New HtmlTableCell
                    TbCell.InnerHtml = drwLoan.LoanNumber
                    TbCell.NoWrap = True
                    TbCell.Align = "center"
                    TbRow.Cells.Add(TbCell)


                    TbCell = New HtmlTableCell
                    TbCell.InnerHtml = dtblFilebyCustomerNo.First.MobileNo
                    TbCell.NoWrap = True
                    TbCell.Align = "center"
                    TbRow.Cells.Add(TbCell)


                    TbCell = New HtmlTableCell
                    TbCell.InnerHtml = NotPiadDurationDay
                    TbCell.NoWrap = True
                    TbCell.Width = "50px"
                    TbCell.Align = "center"
                    TbRow.Cells.Add(TbCell)

                    TbCell = New HtmlTableCell
                    TbCell.InnerHtml = dtblBranch.First.BranchName
                    TbCell.NoWrap = False
                    TbCell.Width = "120px"
                    TbCell.Align = "center"
                    TbRow.Cells.Add(TbCell)


                    If drwUserLogin.IsDataAdmin = True Then

                        TbCell = New HtmlTableCell
                        TbCell.InnerHtml = "<a ID='lnkbtnFollowing' href='#'  onclick= btnFollwoing_ClientClick(" & dtblFilebyCustomerNo.First.ID.ToString() & "," & drwLoan.ID.ToString() & "," & AmountDeffered & ")>ثبت پیگیری</a>"
                        TbCell.NoWrap = True
                        TbCell.Align = "center"
                        TbRow.Cells.Add(TbCell)

                        TbCell = New HtmlTableCell

                        TbCell.InnerHtml = strAssighedUser
                        TbCell.NoWrap = True
                        TbCell.Align = "center"
                        TbRow.Cells.Add(TbCell)



                    ElseIf drwUserLogin.FK_BrnachID = drwLoan.FK_BranchID OrElse blnProvinceAdmin = True Then

                        If strAssighedUser = drwUserLogin.Username OrElse strAssighedUser = "" OrElse drwUserLogin.IsDataUserAdmin = True Then
                            TbCell = New HtmlTableCell
                            TbCell.InnerHtml = "<a ID='lnkbtnFollowing' href='#'  onclick= btnFollwoing_ClientClick(" & dtblFilebyCustomerNo.First.ID.ToString() & "," & drwLoan.ID.ToString() & "," & AmountDeffered & ")>ثبت پیگیری</a>"
                            TbCell.NoWrap = True
                            TbCell.Align = "center"
                            TbRow.Cells.Add(TbCell)


                        Else

                            TbCell = New HtmlTableCell
                            TbCell.InnerHtml = "ثبت پیگیری"
                            TbCell.NoWrap = True
                            TbCell.Align = "center"
                            TbRow.Cells.Add(TbCell)

                        End If

                        TbCell = New HtmlTableCell
                        TbCell.InnerHtml = strAssighedUser
                        TbCell.NoWrap = True
                        TbCell.Align = "center"
                        TbRow.Cells.Add(TbCell)


                    End If


                    If Not TbRow Is Nothing Then
                        tblResult.Rows.Add(TbRow)
                        divResult.Visible = True

                    End If
                Next

            Else
                ''check in totaldefferd and insert loan
                Dim tadpTotalLC1 As New BusinessObject.dstTotalDeffredLCTableAdapters.spr_TotalDeffredLC_SelectByCustomerNOTableAdapter
                Dim dtblTotalLC1 As BusinessObject.dstTotalDeffredLC.spr_TotalDeffredLC_SelectByCustomerNODataTable = Nothing

                dtblTotalLC1 = tadpTotalLC1.GetData(txtCustomerNO.Text.Trim())


                If dtblTotalLC1.Rows.Count = 0 Then

                    Bootstrap_Panel1.ShowMessage("وامی برای این مشتری ثبت نشده است.", True)
                    Return

                End If

                Dim blnHasFound As Boolean = False

                For Each drwTotalLC1 As BusinessObject.dstTotalDeffredLC.spr_TotalDeffredLC_SelectByCustomerNORow In dtblTotalLC1.Rows



                    i += 1

                    Dim intLoanID As Integer = -1
                    Dim intFileID As Integer = dtblFilebyCustomerNo.First.ID
                    Dim intLoanTypeID As Integer = -1
                    Dim intBranchID As Integer = -1
                    Dim strLoanNO As String = drwTotalLC1.LCNumber.ToString()
                    intBranchID = GetBranchID(drwTotalLC1.BranchCode)
                    ''getbranchname
                    Dim tadpBranch As New BusinessObject.dstBranchTableAdapters.spr_Branch_SelectTableAdapter
                    Dim dtblBranch As BusinessObject.dstBranch.spr_Branch_SelectDataTable = Nothing
                    dtblBranch = tadpBranch.GetData(intBranchID)

                    intLoanID = InsertLoan(intFileID, intLoanTypeID, intBranchID, drwTotalLC1.LCNumber)

                    Session("CustomerNO") = drwTotalLC1.CustomerNO




                    If drwUserLogin.IsDataAdmin = True Then

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
                        TbCell.InnerHtml = dtblFilebyCustomerNo.First.FName & " " & dtblFilebyCustomerNo.First.LName
                        TbCell.NoWrap = False
                        TbCell.Width = "100px"
                        TbCell.Align = "center"
                        TbRow.Cells.Add(TbCell)

                        TbCell = New HtmlTableCell
                        TbCell.InnerHtml = drwTotalLC1.LCNumber
                        TbCell.NoWrap = True
                        TbCell.Align = "center"
                        TbRow.Cells.Add(TbCell)


                        TbCell = New HtmlTableCell
                        TbCell.InnerHtml = dtblFilebyCustomerNo.First.MobileNo
                        TbCell.NoWrap = True
                        TbCell.Align = "center"
                        TbRow.Cells.Add(TbCell)


                        TbCell = New HtmlTableCell
                        TbCell.InnerHtml = drwTotalLC1.NotPiadDurationDay
                        TbCell.NoWrap = True
                        TbCell.Width = "50px"
                        TbCell.Align = "center"
                        TbRow.Cells.Add(TbCell)

                        TbCell = New HtmlTableCell
                        TbCell.InnerHtml = dtblBranch.First.BranchName
                        TbCell.NoWrap = False
                        TbCell.Width = "120px"
                        TbCell.Align = "center"
                        TbRow.Cells.Add(TbCell)


                        TbCell = New HtmlTableCell
                        TbCell.InnerHtml = "<a ID='lnkbtnFollowing' href='#'  onclick= btnFollwoing_ClientClick(" & intFileID.ToString() & "," & intLoanID.ToString() & "," & drwTotalLC1.AmounDefferd & ")>ثبت پیگیری</a>"
                        TbCell.NoWrap = True
                        TbCell.Align = "center"
                        TbRow.Cells.Add(TbCell)


                        TbCell = New HtmlTableCell
                        TbCell.InnerHtml = "---"
                        TbCell.NoWrap = True
                        TbCell.Align = "center"
                        TbRow.Cells.Add(TbCell)

                        If Not TbRow Is Nothing Then
                            tblResult.Rows.Add(TbRow)
                            divResult.Visible = True

                        End If

                    ElseIf drwUserLogin.FK_BrnachID = intBranchID OrElse blnProvinceAdmin = True Then


                        blnHasFound = True

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
                        TbCell.InnerHtml = dtblFilebyCustomerNo.First.FName & " " & dtblFilebyCustomerNo.First.LName
                        TbCell.NoWrap = False
                        TbCell.Width = "100px"
                        TbCell.Align = "center"
                        TbRow.Cells.Add(TbCell)

                        TbCell = New HtmlTableCell
                        TbCell.InnerHtml = drwTotalLC1.LCNumber
                        TbCell.NoWrap = True
                        TbCell.Align = "center"
                        TbRow.Cells.Add(TbCell)


                        TbCell = New HtmlTableCell
                        TbCell.InnerHtml = dtblFilebyCustomerNo.First.MobileNo
                        TbCell.NoWrap = True
                        TbCell.Align = "center"
                        TbRow.Cells.Add(TbCell)


                        TbCell = New HtmlTableCell
                        TbCell.InnerHtml = drwTotalLC1.NotPiadDurationDay
                        TbCell.NoWrap = True
                        TbCell.Width = "50px"
                        TbCell.Align = "center"
                        TbRow.Cells.Add(TbCell)

                        TbCell = New HtmlTableCell
                        TbCell.InnerHtml = dtblBranch.First.BranchName
                        TbCell.NoWrap = False
                        TbCell.Width = "120px"
                        TbCell.Align = "center"
                        TbRow.Cells.Add(TbCell)

                        TbCell = New HtmlTableCell
                        TbCell.InnerHtml = "<a ID='lnkbtnFollowing' href='#'  onclick= btnFollwoing_ClientClick(" & intFileID.ToString() & "," & intLoanID.ToString() & "," & drwTotalLC1.AmounDefferd & ")>ثبت پیگیری</a>"
                        TbCell.NoWrap = True
                        TbCell.Align = "center"
                        TbRow.Cells.Add(TbCell)


                        TbCell = New HtmlTableCell
                        TbCell.InnerHtml = "---"
                        TbCell.NoWrap = True
                        TbCell.Align = "center"
                        TbRow.Cells.Add(TbCell)


                        If Not TbRow Is Nothing Then
                            tblResult.Rows.Add(TbRow)
                            divResult.Visible = True
                        End If

                    End If

                Next

                If blnHasFound = False Then

                    Bootstrap_Panel1.ShowMessage("وام مرتبط با این شماره مشتری یافت نشد.", True)
                End If

            End If



        Else

            ''check in totaldefferd and insert file and loan
            Dim tadpTotalLC1 As New BusinessObject.dstTotalDeffredLCTableAdapters.spr_TotalDeffredLC_SelectByCustomerNOTableAdapter
            Dim dtblTotalLC1 As BusinessObject.dstTotalDeffredLC.spr_TotalDeffredLC_SelectByCustomerNODataTable = Nothing

            dtblTotalLC1 = tadpTotalLC1.GetData(txtCustomerNO.Text.Trim())

            If dtblTotalLC1.Rows.Count = 0 Then
                Bootstrap_Panel1.ShowMessage("اطلاعات این مشتری در سیستم ثبت نشده است.", True)
                Return
            End If

            Dim blnHasFound As Boolean = False

            For Each drwTotalLC1 As BusinessObject.dstTotalDeffredLC.spr_TotalDeffredLC_SelectByCustomerNORow In dtblTotalLC1.Rows


                Dim TbRow As HtmlTableRow
                i += 1
                Dim intLoanID As Integer = -1

                Dim intFileID As Integer = -1
                Dim intLoanTypeID As Integer = -1

                Dim intBranchID As Integer = -1
                Dim strLoanNO As String = drwTotalLC1.LCNumber.ToString()

                intFileID = InsertFile(drwTotalLC1.CustomerNO, drwTotalLC1.FullName, drwTotalLC1.MobileNO, drwUserLogin.ID, drwTotalLC1.LoanTypeCode, intLoanTypeID)
                intBranchID = GetBranchID(drwTotalLC1.BranchCode)
                intLoanID = InsertLoan(intFileID, intLoanTypeID, intBranchID, drwTotalLC1.LCNumber)
                Session("CustomerNO") = drwTotalLC1.CustomerNO


                If drwUserLogin.IsDataAdmin = True Then
                    Dim TbCell As HtmlTableCell
                    TbRow = New HtmlTableRow

                    TbCell = New HtmlTableCell
                    TbCell.InnerHtml = "<a ID='lnkbtnFollowing' href='#'  onclick= btnFollwoing_ClientClick(" & intFileID.ToString() & "," & intLoanID.ToString() & "," & drwTotalLC1.AmounDefferd & ")>ثبت پیگیری</a>"
                    TbCell.NoWrap = True

                    TbCell.Align = "center"
                    TbRow.Cells.Add(TbCell)


                    TbCell = New HtmlTableCell
                    TbCell.InnerHtml = "---"
                    TbCell.NoWrap = True
                    TbCell.Align = "center"
                    TbRow.Cells.Add(TbCell)

                    If Not TbRow Is Nothing Then
                        tblResult.Rows.Add(TbRow)
                        divResult.Visible = True
                    End If

                ElseIf drwUserLogin.FK_BrnachID = intBranchID OrElse blnProvinceAdmin = True Then


                    blnHasFound = True

                        Dim TbCell As HtmlTableCell
                        TbRow = New HtmlTableRow
                        TbCell = New HtmlTableCell
                        TbCell.InnerHtml = "<a ID='lnkbtnFollowing' href='#'  onclick= btnFollwoing_ClientClick(" & intFileID.ToString() & "," & intLoanID.ToString() & "," & drwTotalLC1.AmounDefferd & ")>ثبت پیگیری</a>"
                        TbCell.NoWrap = True
                        TbCell.Align = "center"
                        TbRow.Cells.Add(TbCell)

                        TbCell = New HtmlTableCell
                        TbCell.InnerHtml = "---"
                        TbCell.NoWrap = True
                        TbCell.Align = "center"
                        TbRow.Cells.Add(TbCell)


                    If Not TbRow Is Nothing Then
                        tblResult.Rows.Add(TbRow)
                        divResult.Visible = True

                    End If


                End If

            Next

            If blnHasFound = False Then

                Bootstrap_Panel1.ShowMessage("وام مرتبط با این شماره مشتری یافت نشد.", True)
            End If


        End If





    End Sub

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

    Private Function GetBarnchName(ByVal strBranchCode As String) As stc_Branch_Info


        Dim obj_stc_Branch_Info As stc_Branch_Info
        obj_stc_Branch_Info.BranchAddress = ""
        obj_stc_Branch_Info.BranchName = strBranchCode

        Try


            Dim cnnBuiler_BI As New OracleConnectionStringBuilder()
            cnnBuiler_BI.DataSource = "10.35.1.37:1522/bidb"
            cnnBuiler_BI.UserID = "deposit"
            cnnBuiler_BI.Password = "deposit"
            cnnBuiler_BI.Unicode = True

            Using cnnBI_Connection As New OracleConnection(cnnBuiler_BI.ConnectionString)

                Dim cmd_BI As OracleCommand = cnnBI_Connection.CreateCommand()
                Dim strBrnaches As String = "SELECT * from BRANCHES where branch_code='" & strBranchCode & "'"
                cmd_BI.CommandText = strBrnaches

                Try
                    cnnBI_Connection.Open()
                Catch ex As Exception
                    Return obj_stc_Branch_Info
                End Try

                Dim dataReader As OracleDataReader = Nothing
                dataReader = cmd_BI.ExecuteReader()

                If dataReader.Read = False Then
                    dataReader.Close()
                    cnnBI_Connection.Close()
                    Return obj_stc_Branch_Info
                End If


                If dataReader.GetValue(6) Is DBNull.Value Then
                    obj_stc_Branch_Info.BranchName = strBranchCode
                Else
                    obj_stc_Branch_Info.BranchName = CStr(dataReader.GetValue(6)).Trim.Replace("'", "")
                End If

                If dataReader.GetValue(7) Is DBNull.Value Then
                    obj_stc_Branch_Info.BranchAddress = ""
                Else
                    obj_stc_Branch_Info.BranchAddress = CStr(dataReader.GetValue(7)).Trim.Replace("'", "")
                End If


                dataReader.Close()
                cnnBI_Connection.Close()

            End Using

            Return obj_stc_Branch_Info
        Catch ex As Exception
            Return obj_stc_Branch_Info
        End Try
    End Function

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


    Private Function GetBranchID(ByVal strBranchCode As String) As Integer

        Dim tadpBranchbyCode As New BusinessObject.dstBranchTableAdapters.spr_Branch_ByCode_SelectTableAdapter
        Dim dtblBranchbyCode As BusinessObject.dstBranch.spr_Branch_ByCode_SelectDataTable = Nothing
        dtblBranchbyCode = tadpBranchbyCode.GetData(strBranchCode)

        Dim intBranchID As Integer = -1

        Dim strBranchName As String = ""

        If dtblBranchbyCode.Rows.Count = 0 Then


            Dim obj_stc_BranchInfo As stc_Branch_Info = GetBarnchName(strBranchCode)
            Dim qryBranch As New BusinessObject.dstBranchTableAdapters.QueriesTableAdapter
            intBranchID = qryBranch.spr_Branch_Insert(strBranchCode, obj_stc_BranchInfo.BranchName, obj_stc_BranchInfo.BranchAddress, 2, Nothing, "", 1) 'UserID=2 is System User
            strBranchName = obj_stc_BranchInfo.BranchName


        Else

            Dim drwBranchbyCode As BusinessObject.dstBranch.spr_Branch_ByCode_SelectRow = dtblBranchbyCode.Rows(0)

            intBranchID = drwBranchbyCode.ID
            strBranchName = drwBranchbyCode.BranchName

        End If

        Return intBranchID

    End Function

    Private Sub Bootstrap_Panel1_Panel_Display_Click(sender As Object, e As EventArgs) Handles Bootstrap_Panel1.Panel_Display_Click
        DisplayFileList()
    End Sub


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




    ''Changed part
    ''   Dim tadpTotalLC As New BusinessObject.dstTotalDeffredLCTableAdapters.spr_TotalDeffredLC_SelectByCustomerNOTableAdapter
    ''Dim dtblTotalLC As BusinessObject.dstTotalDeffredLC.spr_TotalDeffredLC_SelectByCustomerNODataTable = Nothing

    ''    dtblTotalLC = tadpTotalLC.GetData(txtCustomerNO.Text.Trim())

    ''    If dtblTotalLC.Rows.Count > 0 Then


    ''For Each drwTotalLC As BusinessObject.dstTotalDeffredLC.spr_TotalDeffredLC_SelectByCustomerNORow In dtblTotalLC.Rows
    ''Dim TbRow As HtmlTableRow
    ''            i += 1


    ''            ''check if this file assign to current user or not
    ''            Dim tadpHandyFollowAssignBYIDs As New BusinessObject.dstHandyFollowTableAdapters.spr_HandyFollowAssignByIDs_SelectTableAdapter
    ''Dim dtblHandyFollowAssignByIDs As BusinessObject.dstHandyFollow.spr_HandyFollowAssignByIDs_SelectDataTable = Nothing




    ''Dim tadpBranchbyCode As New BusinessObject.dstBranchTableAdapters.spr_Branch_ByCode_SelectTableAdapter
    ''Dim dtblBranchbyCode As BusinessObject.dstBranch.spr_Branch_ByCode_SelectDataTable = Nothing
    ''            dtblBranchbyCode = tadpBranchbyCode.GetData(drwTotalLC.BranchCode)

    ''            Dim intBranchID As Integer = -1

    ''Dim strBranchName As String = ""

    ''If dtblBranchbyCode.Rows.Count = 0 Then


    ''Dim obj_stc_BranchInfo As stc_Branch_Info = GetBarnchName(drwTotalLC.BranchCode)
    ''Dim qryBranch As New BusinessObject.dstBranchTableAdapters.QueriesTableAdapter
    ''                intBranchID = qryBranch.spr_Branch_Insert(drwTotalLC.BranchCode, obj_stc_BranchInfo.BranchName, obj_stc_BranchInfo.BranchAddress, 2, Nothing, "", 1) 'UserID=2 is System User
    ''                strBranchName = obj_stc_BranchInfo.BranchName


    ''            Else

    ''Dim drwBranchbyCode As BusinessObject.dstBranch.spr_Branch_ByCode_SelectRow = dtblBranchbyCode.Rows(0)

    ''                    intBranchID = drwBranchbyCode.ID
    ''                    strBranchName = drwBranchbyCode.BranchName

    ''                End If

    ''If drwUserLogin.IsDataAdmin = False AndAlso drwUserLogin.IsDataUserAdmin = False Then

    ''If intBranchID <> drwUserLogin.FK_BrnachID Then
    ''Continue For
    ''End If
    ''End If

    ''            ''get total report
    ''            intCount += 1

    ''            Dim TbCell As HtmlTableCell
    ''            TbRow = New HtmlTableRow


    ''            TbCell = New HtmlTableCell
    ''            TbCell.InnerHtml = CStr(intCount)
    ''            TbCell.Align = "center"
    ''            TbCell.NoWrap = True
    ''            TbRow.Cells.Add(TbCell)


    ''            TbCell = New HtmlTableCell
    ''            TbCell.InnerHtml = drwTotalLC.CustomerNO
    ''            TbCell.NoWrap = True

    ''            TbCell.Align = "center"
    ''            TbRow.Cells.Add(TbCell)

    ''            TbCell = New HtmlTableCell
    ''            TbCell.InnerHtml = drwTotalLC.FullName
    ''            TbCell.NoWrap = False
    ''            TbCell.Width = "100px"
    ''            TbCell.Align = "center"
    ''            TbRow.Cells.Add(TbCell)

    ''            TbCell = New HtmlTableCell
    ''            TbCell.InnerHtml = drwTotalLC.LCNumber
    ''            TbCell.NoWrap = True
    ''            TbCell.Align = "center"
    ''            TbRow.Cells.Add(TbCell)


    ''            TbCell = New HtmlTableCell
    ''            TbCell.InnerHtml = drwTotalLC.MobileNO
    ''            TbCell.NoWrap = True
    ''            TbCell.Align = "center"
    ''            TbRow.Cells.Add(TbCell)


    ''            TbCell = New HtmlTableCell
    ''            TbCell.InnerHtml = drwTotalLC.NotPiadDurationDay
    ''            TbCell.NoWrap = True
    ''            TbCell.Width = "50px"
    ''            TbCell.Align = "center"
    ''            TbRow.Cells.Add(TbCell)

    ''            TbCell = New HtmlTableCell
    ''            TbCell.InnerHtml = strBranchName
    ''            TbCell.NoWrap = False
    ''            TbCell.Width = "120px"
    ''            TbCell.Align = "center"
    ''            TbRow.Cells.Add(TbCell)

    ''            Dim tadpFilebyCustomerNo As New BusinessObject.dstFileTableAdapters.spr_File_CustomerNo_SelectTableAdapter
    ''Dim dtblFilebyCustomerNo As BusinessObject.dstFile.spr_File_CustomerNo_SelectDataTable = Nothing
    ''            dtblFilebyCustomerNo = tadpFilebyCustomerNo.GetData(drwTotalLC.CustomerNO)

    ''            Dim qryLoan As New BusinessObject.dstLoanTableAdapters.QueriesTableAdapter
    ''Dim dteLoanDate? As Date = Nothing

    ''If dtblFilebyCustomerNo.Count > 0 Then


    ''If drwUserLogin.IsDataUserAdmin = False Then

    ''Dim tadpLoanByNumber As New BusinessObject.dstLoanTableAdapters.spr_Loan_ByLoanNumber_SelectTableAdapter
    ''Dim dtblLoanByNumber As BusinessObject.dstLoan.spr_Loan_ByLoanNumber_SelectDataTable = Nothing
    ''                    dtblLoanByNumber = tadpLoanByNumber.GetData(drwTotalLC.LCNumber, dtblFilebyCustomerNo.First.ID)

    ''                    Dim intFileID As Integer = dtblFilebyCustomerNo.First.ID
    ''Dim intLoanID As Integer = -1

    ''If dtblLoanByNumber.Count > 0 Then


    ''                        intLoanID = dtblLoanByNumber.First.ID

    ''                    Else



    ''Dim tadpLoanTypeByCode As New BusinessObject.dstLoanTypeTableAdapters.spr_LoanType_byCode_SelectTableAdapter
    ''Dim dtblLoanTypeByCode As BusinessObject.dstLoanType.spr_LoanType_byCode_SelectDataTable = Nothing
    ''                        dtblLoanTypeByCode = tadpLoanTypeByCode.GetData(drwTotalLC.LoanTypeCode)

    ''                        Dim intLoanTypeID As Integer = -1

    ''If dtblLoanTypeByCode.Rows.Count = 0 Then

    ''Dim strLoanTypeName As String = GetLoanTypeName(drwTotalLC.LoanTypeCode)
    ''Dim qryLoanType As New BusinessObject.dstLoanTypeTableAdapters.QueriesTableAdapter
    ''                            intLoanTypeID = qryLoanType.spr_LoanType_Insert(drwTotalLC.LoanTypeCode, strLoanTypeName, 2, "")

    ''                        Else

    ''Dim drwLoanTypeByCode As BusinessObject.dstLoanType.spr_LoanType_byCode_SelectRow = dtblLoanTypeByCode.Rows(0)
    ''                            intLoanTypeID = drwLoanTypeByCode.ID

    ''                        End If

    ''                        intLoanID = qryLoan.spr_Loan_Insert(intFileID, intLoanTypeID, intBranchID, dteLoanDate, drwTotalLC.LCNumber, Nothing, Date.Now, Nothing, Nothing)


    ''                    End If


    ''                    dtblHandyFollowAssignByIDs = tadpHandyFollowAssignBYIDs.GetData(1, drwUserLogin.ID, intFileID, intLoanID, 1)
    ''                    If dtblHandyFollowAssignByIDs.Rows.Count <> 0 Then

    ''''comment to show searched file to other user's of branch
    ''If drwUserLogin.ID <> dtblHandyFollowAssignByIDs.First.FK_AssignUserID Then
    ''Continue For
    ''End If

    ''End If


    ''                    '   Dim intLogCount = cntxVar.tbl_HandyFollow.Where(Function(x) x.FK_FileID = intFileID And x.FK_LoanID = intLoanID).Count()
    ''                    dtblhandyFollow = tadpHandyFollow.GetData(intLoanID, intFileID)

    ''                    If dtblhandyFollow.Rows.Count = 0 Then


    ''                        TbCell = New HtmlTableCell
    ''                        TbCell.InnerHtml = "<a ID='lnkbtnFollowing' href='#'  onclick= btnFollwoing_ClientClick(" & intFileID.ToString() & "," & intLoanID.ToString() & "," & drwTotalLC.AmounDefferd & ")>ثبت پیگیری</a>"
    ''                        TbCell.NoWrap = True
    ''                        TbCell.Align = "center"
    ''                        TbRow.Cells.Add(TbCell)

    ''                        TbCell = New HtmlTableCell
    ''                        TbCell.InnerHtml = "---"
    ''                        TbCell.NoWrap = True
    ''                        TbCell.Align = "center"
    ''                        TbRow.Cells.Add(TbCell)

    ''                    Else

    ''Dim intUserId As Integer = drwUserLogin.ID
    '''   Dim intLogCountByUser = cntxVar.tbl_HandyFollow.Where(Function(x) x.FK_FileID = intFileID And x.FK_LoanID = intLoanID And x.FK_UserID = intUserId).Count()

    ''Dim tadpHandyFollowByUser As New BusinessObject.dstHandyFollowTableAdapters.spr_HandyFollow_CheckFileLoanUser_SelectTableAdapter
    ''Dim dtblHandyFollowByUser As BusinessObject.dstHandyFollow.spr_HandyFollow_CheckFileLoanUser_SelectDataTable = Nothing

    ''                        dtblHandyFollowByUser = tadpHandyFollowByUser.GetData(intLoanID, intFileID, intUserId)

    ''                        If dtblHandyFollowByUser.Rows.Count = 0 Then


    ''''get Folowed UserName
    ''Dim intFollowedUserID As Integer = dtblhandyFollow.First.FK_UserID

    ''Dim tadpUser As New BusinessObject.dstUserTableAdapters.spr_User_SelectTableAdapter
    ''Dim dtblUser As BusinessObject.dstUser.spr_User_SelectDataTable = Nothing

    ''                            dtblUser = tadpUser.GetData(intFollowedUserID)

    ''                            TbCell = New HtmlTableCell
    ''                            TbCell.InnerHtml = "<a ID='lnkbtnFollowing' href='#'  onclick= btnFollwoing_ClientClick(" & intFileID.ToString() & "," & intLoanID.ToString() & "," & drwTotalLC.AmounDefferd & ")>ثبت پیگیری</a>"
    ''                            TbCell.NoWrap = True
    ''                            TbCell.Align = "center"
    ''                            TbRow.Cells.Add(TbCell)



    ''                            ''    TbCell = New HtmlTableCell
    ''                            ''    TbCell.InnerHtml = "ثبت پیگیری"
    ''                            ''    TbCell.NoWrap = False
    ''                            ''    TbCell.Align = "center"
    ''                            ''    TbRow.Cells.Add(TbCell)

    ''                            ''    ' Dim lnqDetail = cntxVar.tbl_HandyFollow.Where(Function(x) x.FK_FileID = intFileID And x.FK_LoanID = intLoanID)

    ''                            ''    '  If lnqDetail.Count > 0 Then

    ''                            ''    'Dim lnqDetailList = lnqDetail.ToList(0)
    ''                            ''    'Dim lnqUser = cntxVar.tbl_User.Where(Function(x) x.ID = lnqDetailList.FK_UserID).ToList(0)

    ''                            TbCell = New HtmlTableCell
    ''                            TbCell.InnerHtml = dtblUser.First.Username
    ''                            TbCell.NoWrap = True
    ''                            TbCell.Align = "center"
    ''                            TbRow.Cells.Add(TbCell)

    ''                            ''    '  End If

    ''                        Else


    ''                            TbCell = New HtmlTableCell
    ''                            TbCell.InnerHtml = "<a ID='lnkbtnFollowing' href='#'  onclick= btnFollwoing_ClientClick(" & intFileID.ToString() & "," & intLoanID.ToString() & "," & drwTotalLC.AmounDefferd & ")>ثبت پیگیری</a>"
    ''                            TbCell.NoWrap = True
    ''                            TbCell.Align = "center"
    ''                            TbRow.Cells.Add(TbCell)

    ''                            '  Dim lnqDetail = cntxVar.tbl_HandyFollow.Where(Function(x) x.FK_FileID = intFileID And x.FK_LoanID = intLoanID)

    ''                            '  If lnqDetail.Count > 0 Then
    ''                            '  Dim lnqDetailList = lnqDetail.ToList(0)
    ''                            'Dim lnqUser = cntxVar.tbl_User.Where(Function(x) x.ID = lnqDetailList.FK_UserID).ToList(0)

    ''                            TbCell = New HtmlTableCell
    ''                            TbCell.InnerHtml = dtblHandyFollowByUser.First.Username
    ''                            TbCell.NoWrap = True
    ''                            TbCell.Align = "center"
    ''                            TbRow.Cells.Add(TbCell)


    ''                            '  End If

    ''                        End If

    ''End If


    ''Else

    ''Dim tadpLoanByNumber As New BusinessObject.dstLoanTableAdapters.spr_Loan_ByLoanNumber_SelectTableAdapter
    ''Dim dtblLoanByNumber As BusinessObject.dstLoan.spr_Loan_ByLoanNumber_SelectDataTable = Nothing
    ''                    dtblLoanByNumber = tadpLoanByNumber.GetData(drwTotalLC.LCNumber, dtblFilebyCustomerNo.First.ID)

    ''                    Dim intFileID As Integer = dtblFilebyCustomerNo.First.ID
    ''Dim intLoanID As Integer = -1

    ''If dtblLoanByNumber.Count > 0 Then

    ''                        intLoanID = dtblLoanByNumber.First.ID

    ''                    Else





    ''Dim tadpLoanTypeByCode As New BusinessObject.dstLoanTypeTableAdapters.spr_LoanType_byCode_SelectTableAdapter
    ''Dim dtblLoanTypeByCode As BusinessObject.dstLoanType.spr_LoanType_byCode_SelectDataTable = Nothing
    ''                        dtblLoanTypeByCode = tadpLoanTypeByCode.GetData(drwTotalLC.LoanTypeCode)


    ''                        Dim intLoanTypeID As Integer = -1

    ''If dtblLoanTypeByCode.Rows.Count = 0 Then

    ''Dim strLoanTypeName As String = GetLoanTypeName(drwTotalLC.LoanTypeCode)
    ''Dim qryLoanType As New BusinessObject.dstLoanTypeTableAdapters.QueriesTableAdapter
    ''                            intLoanTypeID = qryLoanType.spr_LoanType_Insert(drwTotalLC.LoanTypeCode, strLoanTypeName, 2, "")

    ''                        Else

    ''Dim drwLoanTypeByCode As BusinessObject.dstLoanType.spr_LoanType_byCode_SelectRow = dtblLoanTypeByCode.Rows(0)
    ''                            intLoanTypeID = drwLoanTypeByCode.ID


    ''                        End If

    ''                        intLoanID = qryLoan.spr_Loan_Insert(intFileID, intLoanTypeID, intBranchID, dteLoanDate, drwTotalLC.LCNumber, Nothing, Date.Now, Nothing, Nothing)

    ''                    End If

    ''                    TbCell = New HtmlTableCell
    ''                    TbCell.InnerHtml = "<a ID='lnkbtnFollowing' href='#'  onclick= btnFollwoing_ClientClick(" & intFileID.ToString() & "," & intLoanID.ToString() & "," & drwTotalLC.AmounDefferd & ")>ثبت پیگیری</a>"
    ''                    TbCell.NoWrap = True
    ''                    TbCell.Align = "center"
    ''                    TbRow.Cells.Add(TbCell)




    ''                    dtblhandyFollow = tadpHandyFollow.GetData(intLoanID, intFileID)


    ''                    Dim followingUserName As String = "---"
    ''''********************

    ''If dtblhandyFollow.Rows.Count > 0 Then


    ''                        '   Dim lnqUser = cntxVar.tbl_User.Where(Function(x) x.ID = dtblhandyFollow.First.FK_UserID).ToList(0)
    ''                        followingUserName = dtblhandyFollow.First.Username

    ''                    End If


    ''                    TbCell = New HtmlTableCell
    ''                    TbCell.InnerHtml = followingUserName
    ''                    TbCell.NoWrap = True
    ''                    TbCell.Align = "center"
    ''                    TbRow.Cells.Add(TbCell)



    ''                End If


    ''Else

    ''Dim qryFile As New BusinessObject.dstFileTableAdapters.QueriesTableAdapter


    ''Dim intFileID = qryFile.spr_File_Insert(drwTotalLC.CustomerNO, "", drwTotalLC.FullName, "", drwTotalLC.MobileNO, "", "", "", "", "", "", True, 2, 1, Nothing, Nothing)

    ''Dim tadpLoanTypeByCode As New BusinessObject.dstLoanTypeTableAdapters.spr_LoanType_byCode_SelectTableAdapter
    ''Dim dtblLoanTypeByCode As BusinessObject.dstLoanType.spr_LoanType_byCode_SelectDataTable = Nothing
    ''                dtblLoanTypeByCode = tadpLoanTypeByCode.GetData(drwTotalLC.LoanTypeCode)

    ''                Dim intLoanTypeID As Integer = -1

    ''If dtblLoanTypeByCode.Rows.Count = 0 Then

    ''Dim strLoanTypeName As String = GetLoanTypeName(drwTotalLC.LoanTypeCode)
    ''Dim qryLoanType As New BusinessObject.dstLoanTypeTableAdapters.QueriesTableAdapter
    ''                    intLoanTypeID = qryLoanType.spr_LoanType_Insert(drwTotalLC.LoanTypeCode, strLoanTypeName, 2, "")


    ''                Else
    ''Dim drwLoanTypeByCode As BusinessObject.dstLoanType.spr_LoanType_byCode_SelectRow = dtblLoanTypeByCode.Rows(0)
    ''                    intLoanTypeID = drwLoanTypeByCode.ID


    ''                End If


    ''Dim intLoanID As Integer = -1
    ''Dim strLoanNO As String = drwTotalLC.LCNumber.ToString()

    ''                intLoanID = InsertLoan(intFileID, intLoanTypeID, intBranchID, drwTotalLC.LCNumber)


    ''                ''  intLoanID = qryLoan.spr_Loan_Insert(intFileID, intLoanTypeID, intBranchID, dteLoanDate, drwTotalLC.LCNumber, Nothing, Date.Now, Nothing, Nothing)

    ''                Session("BranchID") = intBranchID
    ''                Session("BranchName") = strBranchName
    ''                Session("CustomerNO") = drwTotalLC.CustomerNO


    ''                TbCell = New HtmlTableCell
    ''                TbCell.InnerHtml = "<a ID='lnkbtnFollowing' href='#'  onclick= btnFollwoing_ClientClick(" & intFileID.ToString() & "," & intLoanID.ToString() & "," & drwTotalLC.AmounDefferd & ")>ثبت پیگیری</a>"
    ''                TbCell.NoWrap = True
    ''                TbCell.Align = "center"
    ''                TbRow.Cells.Add(TbCell)


    ''                TbCell = New HtmlTableCell
    ''                TbCell.InnerHtml = "---"
    ''                TbCell.NoWrap = True
    ''                TbCell.Align = "center"
    ''                TbRow.Cells.Add(TbCell)




    ''            End If



    ''If Not TbRow Is Nothing Then
    ''                tblResult.Rows.Add(TbRow)


    ''            End If
    ''Next

    ''        divResult.Visible = True








    ''    Else

    ''        Bootstrap_Panel1.ShowMessage("جزییات معوقه برای این مشتری موجود نیست", True)
    ''        Return

    ''    End If

End Class