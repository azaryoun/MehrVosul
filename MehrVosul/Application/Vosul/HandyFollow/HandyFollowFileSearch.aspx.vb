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



        Dim i As Integer
        Dim  intCount As Integer = 0

        Dim dtblUserLogin As BusinessObject.dstUser.spr_User_Login_SelectDataTable = CType(Session("dtblUserLogin"), BusinessObject.dstUser.spr_User_Login_SelectDataTable)
        Dim drwUserLogin As BusinessObject.dstUser.spr_User_Login_SelectRow = dtblUserLogin.Rows(0)

        Dim tadpHandyFollow As New BusinessObject.dstHandyFollowTableAdapters.spr_HandyFollow_CheckFileLoan_SelectTableAdapter
        Dim dtblhandyFollow As BusinessObject.dstHandyFollow.spr_HandyFollow_CheckFileLoan_SelectDataTable = Nothing


        Dim qryErrorLog As New BusinessObject.dstLoginLogTableAdapters.QueriesTableAdapter
        ' Dim cntxVar As New BusinessObject.dbMehrVosulEntities1

        Dim tadpTotalLC As New BusinessObject.dstTotalDeffredLCTableAdapters.spr_TotalDeffredLC_SelectByCustomerNOTableAdapter
        Dim dtblTotalLC As BusinessObject.dstTotalDeffredLC.spr_TotalDeffredLC_SelectByCustomerNODataTable = Nothing

        dtblTotalLC = tadpTotalLC.GetData(txtCustomerNO.Text.Trim())

        If dtblTotalLC.Rows.Count > 0 Then


            For Each drwTotalLC As BusinessObject.dstTotalDeffredLC.spr_TotalDeffredLC_SelectByCustomerNORow In dtblTotalLC.Rows
                Dim TbRow As HtmlTableRow
                i += 1


                ''check if this file assign to current user or not
                Dim tadpHandyFollowAssignBYIDs As New BusinessObject.dstHandyFollowTableAdapters.spr_HandyFollowAssignByIDs_SelectTableAdapter
                Dim dtblHandyFollowAssignByIDs As BusinessObject.dstHandyFollow.spr_HandyFollowAssignByIDs_SelectDataTable = Nothing




                Dim tadpBranchbyCode As New BusinessObject.dstBranchTableAdapters.spr_Branch_ByCode_SelectTableAdapter
                Dim dtblBranchbyCode As BusinessObject.dstBranch.spr_Branch_ByCode_SelectDataTable = Nothing
                dtblBranchbyCode = tadpBranchbyCode.GetData(drwTotalLC.BranchCode)

                Dim intBranchID As Integer = -1

                Dim strBranchName As String = ""

                If dtblBranchbyCode.Rows.Count = 0 Then


                    Dim obj_stc_BranchInfo As stc_Branch_Info = GetBarnchName(drwTotalLC.BranchCode)
                    Dim qryBranch As New BusinessObject.dstBranchTableAdapters.QueriesTableAdapter
                    intBranchID = qryBranch.spr_Branch_Insert(drwTotalLC.BranchCode, obj_stc_BranchInfo.BranchName, obj_stc_BranchInfo.BranchAddress, 2, Nothing, "", 1) 'UserID=2 is System User
                    strBranchName = obj_stc_BranchInfo.BranchName


                Else

                    Dim drwBranchbyCode As BusinessObject.dstBranch.spr_Branch_ByCode_SelectRow = dtblBranchbyCode.Rows(0)

                        intBranchID = drwBranchbyCode.ID
                        strBranchName = drwBranchbyCode.BranchName

                    End If

                    If drwUserLogin.IsDataAdmin = False AndAlso drwUserLogin.IsDataUserAdmin = False Then

                        If intBranchID <> drwUserLogin.FK_BrnachID Then
                        Continue For
                    End If
                    End If

                ''get total report
                intCount += 1

                Dim TbCell As HtmlTableCell
                TbRow = New HtmlTableRow


                TbCell = New HtmlTableCell
                TbCell.InnerHtml = CStr(intCount)
                TbCell.Align = "center"
                TbCell.NoWrap = True
                TbRow.Cells.Add(TbCell)


                TbCell = New HtmlTableCell
                TbCell.InnerHtml = drwTotalLC.CustomerNO
                TbCell.NoWrap = True

                TbCell.Align = "center"
                TbRow.Cells.Add(TbCell)

                TbCell = New HtmlTableCell
                TbCell.InnerHtml = drwTotalLC.FullName
                TbCell.NoWrap = False
                TbCell.Width = "100px"
                TbCell.Align = "center"
                TbRow.Cells.Add(TbCell)

                TbCell = New HtmlTableCell
                TbCell.InnerHtml = drwTotalLC.LCNumber
                TbCell.NoWrap = True
                TbCell.Align = "center"
                TbRow.Cells.Add(TbCell)


                TbCell = New HtmlTableCell
                TbCell.InnerHtml = drwTotalLC.MobileNO
                TbCell.NoWrap = True
                TbCell.Align = "center"
                TbRow.Cells.Add(TbCell)


                TbCell = New HtmlTableCell
                TbCell.InnerHtml = drwTotalLC.NotPiadDurationDay
                TbCell.NoWrap = True
                TbCell.Width = "50px"
                TbCell.Align = "center"
                TbRow.Cells.Add(TbCell)

                TbCell = New HtmlTableCell
                TbCell.InnerHtml = strBranchName
                TbCell.NoWrap = False
                TbCell.Width = "120px"
                TbCell.Align = "center"
                TbRow.Cells.Add(TbCell)

                Dim tadpFilebyCustomerNo As New BusinessObject.dstFileTableAdapters.spr_File_CustomerNo_SelectTableAdapter
                Dim dtblFilebyCustomerNo As BusinessObject.dstFile.spr_File_CustomerNo_SelectDataTable = Nothing
                dtblFilebyCustomerNo = tadpFilebyCustomerNo.GetData(drwTotalLC.CustomerNO)

                Dim qryLoan As New BusinessObject.dstLoanTableAdapters.QueriesTableAdapter
                Dim dteLoanDate? As Date = Nothing

                If dtblFilebyCustomerNo.Count > 0 Then


                    If drwUserLogin.IsDataUserAdmin = False Then

                        Dim tadpLoanByNumber As New BusinessObject.dstLoanTableAdapters.spr_Loan_ByLoanNumber_SelectTableAdapter
                        Dim dtblLoanByNumber As BusinessObject.dstLoan.spr_Loan_ByLoanNumber_SelectDataTable = Nothing
                        dtblLoanByNumber = tadpLoanByNumber.GetData(drwTotalLC.LCNumber, dtblFilebyCustomerNo.First.ID)

                        Dim intFileID As Integer = dtblFilebyCustomerNo.First.ID
                        Dim intLoanID As Integer = -1

                        If dtblLoanByNumber.Count > 0 Then


                            intLoanID = dtblLoanByNumber.First.ID

                        Else



                            Dim tadpLoanTypeByCode As New BusinessObject.dstLoanTypeTableAdapters.spr_LoanType_byCode_SelectTableAdapter
                            Dim dtblLoanTypeByCode As BusinessObject.dstLoanType.spr_LoanType_byCode_SelectDataTable = Nothing
                            dtblLoanTypeByCode = tadpLoanTypeByCode.GetData(drwTotalLC.LoanTypeCode)

                            Dim intLoanTypeID As Integer = -1

                            If dtblLoanTypeByCode.Rows.Count = 0 Then

                                Dim strLoanTypeName As String = GetLoanTypeName(drwTotalLC.LoanTypeCode)
                                Dim qryLoanType As New BusinessObject.dstLoanTypeTableAdapters.QueriesTableAdapter
                                intLoanTypeID = qryLoanType.spr_LoanType_Insert(drwTotalLC.LoanTypeCode, strLoanTypeName, 2, "")

                            Else

                                Dim drwLoanTypeByCode As BusinessObject.dstLoanType.spr_LoanType_byCode_SelectRow = dtblLoanTypeByCode.Rows(0)
                                intLoanTypeID = drwLoanTypeByCode.ID

                            End If

                            intLoanID = qryLoan.spr_Loan_Insert(intFileID, intLoanTypeID, intBranchID, dteLoanDate, drwTotalLC.LCNumber, Nothing, Date.Now, Nothing, Nothing)


                        End If


                        dtblHandyFollowAssignByIDs = tadpHandyFollowAssignBYIDs.GetData(drwUserLogin.ID, intFileID, intLoanID)
                        If dtblHandyFollowAssignByIDs.Rows.Count <> 0 Then

                            If drwUserLogin.ID <> dtblHandyFollowAssignByIDs.First.FK_AssignUserID Then
                                Continue For
                            End If

                        End If


                        '   Dim intLogCount = cntxVar.tbl_HandyFollow.Where(Function(x) x.FK_FileID = intFileID And x.FK_LoanID = intLoanID).Count()
                        dtblhandyFollow = tadpHandyFollow.GetData(intLoanID, intFileID)

                        If dtblhandyFollow.Rows.Count = 0 Then


                            TbCell = New HtmlTableCell
                            TbCell.InnerHtml = "<a ID='lnkbtnFollowing' href='#'  onclick= btnFollwoing_ClientClick(" & intFileID.ToString() & "," & intLoanID.ToString() & "," & drwTotalLC.AmounDefferd & ")>ثبت پیگیری</a>"
                            TbCell.NoWrap = True
                            TbCell.Align = "center"
                            TbRow.Cells.Add(TbCell)

                            TbCell = New HtmlTableCell
                            TbCell.InnerHtml = "---"
                            TbCell.NoWrap = True
                            TbCell.Align = "center"
                            TbRow.Cells.Add(TbCell)

                        Else

                            Dim intUserId As Integer = drwUserLogin.ID
                            '   Dim intLogCountByUser = cntxVar.tbl_HandyFollow.Where(Function(x) x.FK_FileID = intFileID And x.FK_LoanID = intLoanID And x.FK_UserID = intUserId).Count()

                            Dim tadpHandyFollowByUser As New BusinessObject.dstHandyFollowTableAdapters.spr_HandyFollow_CheckFileLoanUser_SelectTableAdapter
                            Dim dtblHandyFollowByUser As BusinessObject.dstHandyFollow.spr_HandyFollow_CheckFileLoanUser_SelectDataTable = Nothing

                            dtblHandyFollowByUser = tadpHandyFollowByUser.GetData(intLoanID, intFileID, intUserId)

                            If dtblHandyFollowByUser.Rows.Count = 0 Then

                                TbCell = New HtmlTableCell
                                TbCell.InnerHtml = "ثبت پیگیری"
                                TbCell.NoWrap = False
                                TbCell.Align = "center"
                                TbRow.Cells.Add(TbCell)

                                ' Dim lnqDetail = cntxVar.tbl_HandyFollow.Where(Function(x) x.FK_FileID = intFileID And x.FK_LoanID = intLoanID)

                                '  If lnqDetail.Count > 0 Then

                                'Dim lnqDetailList = lnqDetail.ToList(0)
                                'Dim lnqUser = cntxVar.tbl_User.Where(Function(x) x.ID = lnqDetailList.FK_UserID).ToList(0)

                                TbCell = New HtmlTableCell
                                TbCell.InnerHtml = dtblhandyFollow.First.Username
                                TbCell.NoWrap = True
                                TbCell.Align = "center"
                                TbRow.Cells.Add(TbCell)

                                '  End If

                            Else


                                TbCell = New HtmlTableCell
                                TbCell.InnerHtml = "<a ID='lnkbtnFollowing' href='#'  onclick= btnFollwoing_ClientClick(" & intFileID.ToString() & "," & intLoanID.ToString() & "," & drwTotalLC.AmounDefferd & ")>ثبت پیگیری</a>"
                                TbCell.NoWrap = True
                                TbCell.Align = "center"
                                TbRow.Cells.Add(TbCell)

                                '  Dim lnqDetail = cntxVar.tbl_HandyFollow.Where(Function(x) x.FK_FileID = intFileID And x.FK_LoanID = intLoanID)

                                '  If lnqDetail.Count > 0 Then
                                '  Dim lnqDetailList = lnqDetail.ToList(0)
                                'Dim lnqUser = cntxVar.tbl_User.Where(Function(x) x.ID = lnqDetailList.FK_UserID).ToList(0)

                                TbCell = New HtmlTableCell
                                TbCell.InnerHtml = dtblHandyFollowByUser.First.Username
                                TbCell.NoWrap = True
                                TbCell.Align = "center"
                                TbRow.Cells.Add(TbCell)


                                '  End If

                            End If

                        End If

                    Else

                        Dim tadpLoanByNumber As New BusinessObject.dstLoanTableAdapters.spr_Loan_ByLoanNumber_SelectTableAdapter
                        Dim dtblLoanByNumber As BusinessObject.dstLoan.spr_Loan_ByLoanNumber_SelectDataTable = Nothing
                        dtblLoanByNumber = tadpLoanByNumber.GetData(drwTotalLC.LCNumber, dtblFilebyCustomerNo.First.ID)

                        Dim intFileID As Integer = dtblFilebyCustomerNo.First.ID
                        Dim intLoanID As Integer = -1

                        If dtblLoanByNumber.Count > 0 Then

                            intLoanID = dtblLoanByNumber.First.ID

                        Else





                            Dim tadpLoanTypeByCode As New BusinessObject.dstLoanTypeTableAdapters.spr_LoanType_byCode_SelectTableAdapter
                            Dim dtblLoanTypeByCode As BusinessObject.dstLoanType.spr_LoanType_byCode_SelectDataTable = Nothing
                            dtblLoanTypeByCode = tadpLoanTypeByCode.GetData(drwTotalLC.LoanTypeCode)


                            Dim intLoanTypeID As Integer = -1

                            If dtblLoanTypeByCode.Rows.Count = 0 Then

                                Dim strLoanTypeName As String = GetLoanTypeName(drwTotalLC.LoanTypeCode)
                                Dim qryLoanType As New BusinessObject.dstLoanTypeTableAdapters.QueriesTableAdapter
                                intLoanTypeID = qryLoanType.spr_LoanType_Insert(drwTotalLC.LoanTypeCode, strLoanTypeName, 2, "")

                            Else

                                Dim drwLoanTypeByCode As BusinessObject.dstLoanType.spr_LoanType_byCode_SelectRow = dtblLoanTypeByCode.Rows(0)
                                intLoanTypeID = drwLoanTypeByCode.ID


                            End If

                            intLoanID = qryLoan.spr_Loan_Insert(intFileID, intLoanTypeID, intBranchID, dteLoanDate, drwTotalLC.LCNumber, Nothing, Date.Now, Nothing, Nothing)

                        End If

                        TbCell = New HtmlTableCell
                        TbCell.InnerHtml = "<a ID='lnkbtnFollowing' href='#'  onclick= btnFollwoing_ClientClick(" & intFileID.ToString() & "," & intLoanID.ToString() & "," & drwTotalLC.AmounDefferd & ")>ثبت پیگیری</a>"
                        TbCell.NoWrap = True
                        TbCell.Align = "center"
                        TbRow.Cells.Add(TbCell)




                        dtblhandyFollow = tadpHandyFollow.GetData(intLoanID, intFileID)


                        Dim followingUserName As String = "---"
                        ''********************

                        If dtblhandyFollow.Rows.Count > 0 Then


                            '   Dim lnqUser = cntxVar.tbl_User.Where(Function(x) x.ID = dtblhandyFollow.First.FK_UserID).ToList(0)
                            followingUserName = dtblhandyFollow.First.Username

                        End If


                        TbCell = New HtmlTableCell
                        TbCell.InnerHtml = followingUserName
                        TbCell.NoWrap = True
                        TbCell.Align = "center"
                        TbRow.Cells.Add(TbCell)



                    End If


                Else

                    Dim qryFile As New BusinessObject.dstFileTableAdapters.QueriesTableAdapter


                    Dim intFileID = qryFile.spr_File_Insert(drwTotalLC.CustomerNO, "", drwTotalLC.FullName, "", drwTotalLC.MobileNO, "", "", "", "", "", "", True, 2, 1, Nothing, Nothing)

                    Dim tadpLoanTypeByCode As New BusinessObject.dstLoanTypeTableAdapters.spr_LoanType_byCode_SelectTableAdapter
                    Dim dtblLoanTypeByCode As BusinessObject.dstLoanType.spr_LoanType_byCode_SelectDataTable = Nothing
                    dtblLoanTypeByCode = tadpLoanTypeByCode.GetData(drwTotalLC.LoanTypeCode)

                    Dim intLoanTypeID As Integer = -1

                    If dtblLoanTypeByCode.Rows.Count = 0 Then

                        Dim strLoanTypeName As String = GetLoanTypeName(drwTotalLC.LoanTypeCode)
                        Dim qryLoanType As New BusinessObject.dstLoanTypeTableAdapters.QueriesTableAdapter
                        intLoanTypeID = qryLoanType.spr_LoanType_Insert(drwTotalLC.LoanTypeCode, strLoanTypeName, 2, "")


                    Else
                        Dim drwLoanTypeByCode As BusinessObject.dstLoanType.spr_LoanType_byCode_SelectRow = dtblLoanTypeByCode.Rows(0)
                        intLoanTypeID = drwLoanTypeByCode.ID


                    End If


                    Dim intLoanID As Integer = -1
                    Dim strLoanNO As String = drwTotalLC.LCNumber.ToString()

                    intLoanID = InsertLoan(intFileID, intLoanTypeID, intBranchID, drwTotalLC.LCNumber)


                    ''  intLoanID = qryLoan.spr_Loan_Insert(intFileID, intLoanTypeID, intBranchID, dteLoanDate, drwTotalLC.LCNumber, Nothing, Date.Now, Nothing, Nothing)

                    Session("BranchID") = intBranchID
                    Session("BranchName") = strBranchName
                    Session("CustomerNO") = drwTotalLC.CustomerNO


                    TbCell = New HtmlTableCell
                    TbCell.InnerHtml = "<a ID='lnkbtnFollowing' href='#'  onclick= btnFollwoing_ClientClick(" & intFileID.ToString() & "," & intLoanID.ToString() & "," & drwTotalLC.AmounDefferd & ")>ثبت پیگیری</a>"
                    TbCell.NoWrap = True
                    TbCell.Align = "center"
                    TbRow.Cells.Add(TbCell)


                    TbCell = New HtmlTableCell
                    TbCell.InnerHtml = "---"
                    TbCell.NoWrap = True
                    TbCell.Align = "center"
                    TbRow.Cells.Add(TbCell)




                End If



                If Not TbRow Is Nothing Then
                    tblResult.Rows.Add(TbRow)


                End If
            Next

            divResult.Visible = True








        Else

            Bootstrap_Panel1.ShowMessage("جزییات معوقه برای این مشتری موجود نیست", True)
            Return

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
End Class