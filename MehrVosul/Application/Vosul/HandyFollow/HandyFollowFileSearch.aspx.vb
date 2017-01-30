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
            Dim intFileID As Integer = CInt(hdnAction.Value.Split(";")(1))
            Dim intLoanID As Integer = CInt(hdnAction.Value.Split(";")(2))
            Session("AmountDeffed") = hdnAction.Value.Split(";")(3)
            Session("intFileID") = CObj(intFileID)
            Session("intLoanID") = CObj(intLoanID)


            Session("customerNO") = CObj(txtCustomerNO.Text)
            Response.Redirect("HandyFollowNew.aspx")
        End If
    End Sub

    Protected Sub DisplayFileList()


        Dim cnnBuiler_BI As New OracleConnectionStringBuilder()
        cnnBuiler_BI.DataSource = "10.35.1.37:1522/bidb"
        cnnBuiler_BI.UserID = "deposit"
        cnnBuiler_BI.Password = "deposit"
        cnnBuiler_BI.Unicode = True

        Dim dteThisDate As Date = Date.Now.AddDays(-1)
        Dim strThisDatePersian As String = mdlGeneral.GetPersianDate(dteThisDate).Replace("/", "")

        Using cnnBI_Connection As New OracleConnection(cnnBuiler_BI.ConnectionString)

            Dim cmd_BI As OracleCommand = cnnBI_Connection.CreateCommand()


            Dim ctxMehr As New BusinessObject.dbMehrVosulEntities1


            Dim strLoan_Info_Query As String = "SELECT * from loan_info where Date_P='" & strThisDatePersian & "' and state in ('3','4','5','F')  and ( CFCIFNO ='" & txtCustomerNO.Text.Trim() & "')"

            cmd_BI.CommandText = strLoan_Info_Query


            Try
                cnnBI_Connection.Open()
            Catch ex As Exception

                Bootstrap_Panel1.ShowMessage(ex.Message, True)
                Return
            End Try
            Dim dataReader As OracleDataReader = Nothing

            Try
                dataReader = cmd_BI.ExecuteReader()

                If dataReader.Read = False Then

                    Bootstrap_Panel1.ShowMessage(" اطلاعات مربوط  بروز رسانی نشده است. لطفا با مدیر سیستم تماس بگیرید ", True)

                    dataReader.Close()
                    cnnBI_Connection.Close()

                    Return
                End If

                Dim i As Integer
                Dim intCount As Integer = 0
                Dim stcVarLoanInfo As stc_Loan_Info

                Dim dtblUserLogin As BusinessObject.dstUser.spr_User_Login_SelectDataTable = CType(Session("dtblUserLogin"), BusinessObject.dstUser.spr_User_Login_SelectDataTable)
                Dim drwUserLogin As BusinessObject.dstUser.spr_User_Login_SelectRow = dtblUserLogin.Rows(0)

                Dim listOperationLoan As New ArrayList
                Dim listOperationCustomer As New ArrayList

                Dim qryErrorLog As New BusinessObject.dstLoginLogTableAdapters.QueriesTableAdapter


                Do
                    i += 1
                    Dim TbRow As HtmlTableRow


                    Try

                        If dataReader.GetValue(0) Is DBNull.Value Then
                            stcVarLoanInfo.FullName = ""
                        Else
                            stcVarLoanInfo.FullName = CStr(dataReader.GetValue(0)).Replace("'", "")
                        End If


                        If dataReader.GetValue(4) Is DBNull.Value Then
                            stcVarLoanInfo.Mobile = ""
                        Else
                            stcVarLoanInfo.Mobile = CStr(dataReader.GetValue(4)).Trim.Replace("'", "")
                        End If


                        If dataReader.GetValue(6) Is DBNull.Value Then
                            i -= 1
                            Continue Do
                        Else
                            stcVarLoanInfo.LC_No = CStr(dataReader.GetValue(6)).Trim.Replace("'", "")
                        End If


                        If dataReader.GetValue(7) Is DBNull.Value Then
                            i -= 1
                            Continue Do
                        Else
                            stcVarLoanInfo.BranchCode = CStr(dataReader.GetValue(7)).Trim
                        End If

                        If dataReader.GetValue(8) Is DBNull.Value Then
                            i -= 1
                            Continue Do
                        Else
                            stcVarLoanInfo.LoanTypeCode = CStr(dataReader.GetValue(8)).Trim.Replace("'", "")
                        End If

                        If dataReader.GetValue(9) Is DBNull.Value Then
                            i -= 1
                            Continue Do
                        Else
                            stcVarLoanInfo.CustomerNo = CStr(dataReader.GetValue(9)).Trim.Replace("'", "")
                        End If


                        If dataReader.GetValue(10) Is DBNull.Value Then
                            i -= 1
                            Continue Do
                        Else
                            stcVarLoanInfo.LoanSerial = CInt(dataReader.GetValue(10))
                        End If

                        If dataReader.GetValue(11) Is DBNull.Value Then
                            stcVarLoanInfo.LCDate = ""
                        Else
                            stcVarLoanInfo.LCDate = CStr(dataReader.GetValue(11)).Trim
                        End If

                        If dataReader.GetValue(12) Is DBNull.Value Then
                            stcVarLoanInfo.LCAmount = Nothing
                        Else
                            stcVarLoanInfo.LCAmount = CDbl(dataReader.GetValue(12))
                        End If



                        If dataReader.GetValue(17) Is DBNull.Value Then
                            stcVarLoanInfo.AmounDefferd = Nothing
                        Else
                            stcVarLoanInfo.AmounDefferd = CDbl(dataReader.GetValue(17))
                        End If



                        If dataReader.GetValue(21) Is DBNull.Value Then
                            i -= 1
                            Continue Do
                        Else
                            stcVarLoanInfo.NotPiadDurationDay = CInt(dataReader.GetValue(21))
                        End If

                        ''check if Customer Number and loan No is double or not, if it is then skip it
                        If i = 1 Then

                            listOperationCustomer.Add(stcVarLoanInfo.CustomerNo)
                            listOperationLoan.Add(stcVarLoanInfo.LC_No)

                        Else


                            For Each obj In listOperationCustomer
                                For Each obj2 In listOperationLoan
                                    If obj = stcVarLoanInfo.CustomerNo And obj2 = stcVarLoanInfo.LC_No Then
                                        Continue Do
                                    End If
                                Next
                            Next

                            listOperationLoan.Add(stcVarLoanInfo.LC_No)
                            listOperationCustomer.Add(stcVarLoanInfo.CustomerNo)

                        End If


                        Dim tadpBranchbyCode As New BusinessObject.dstBranchTableAdapters.spr_Branch_ByCode_SelectTableAdapter
                        Dim dtblBranchbyCode As BusinessObject.dstBranch.spr_Branch_ByCode_SelectDataTable = Nothing
                        dtblBranchbyCode = tadpBranchbyCode.GetData(stcVarLoanInfo.BranchCode)

                        Dim intBranchID As Integer = -1
                        Dim strBranchName As String = ""
                        If dtblBranchbyCode.Rows.Count = 0 Then

                            Dim obj_stc_BranchInfo As stc_Branch_Info = GetBarnchName(stcVarLoanInfo.BranchCode)
                            Dim qryBranch As New BusinessObject.dstBranchTableAdapters.QueriesTableAdapter
                            intBranchID = qryBranch.spr_Branch_Insert(stcVarLoanInfo.BranchCode, obj_stc_BranchInfo.BranchName, obj_stc_BranchInfo.BranchAddress, 2, Nothing, "", 1) 'UserID=2 is System User
                            strBranchName = obj_stc_BranchInfo.BranchName


                        Else

                            Dim drwBranchbyCode As BusinessObject.dstBranch.spr_Branch_ByCode_SelectRow = dtblBranchbyCode.Rows(0)

                            intBranchID = drwBranchbyCode.ID
                            strBranchName = drwBranchbyCode.BranchName

                        End If

                        If drwUserLogin.IsDataAdmin = False AndAlso drwUserLogin.IsDataUserAdmin = False Then

                            If intBranchID <> drwUserLogin.FK_BrnachID Then
                                Continue Do
                            End If
                        End If

                        ''get total report

                        With stcVarLoanInfo

                            intCount += 1


                            Dim TbCell As HtmlTableCell
                            TbRow = New HtmlTableRow


                            TbCell = New HtmlTableCell
                            TbCell.InnerHtml = CStr(intCount)
                            TbCell.Align = "center"
                            TbCell.NoWrap = True
                            TbRow.Cells.Add(TbCell)


                            TbCell = New HtmlTableCell
                            TbCell.InnerHtml = stcVarLoanInfo.CustomerNo
                            TbCell.NoWrap = True
                            TbCell.Align = "center"
                            TbRow.Cells.Add(TbCell)

                            TbCell = New HtmlTableCell

                            Dim arrFullName() As String = stcVarLoanInfo.FullName.Split("*")
                            Dim strFatherName As String = arrFullName(0)
                            Dim strFName As String = arrFullName(1)
                            Dim strLName As String = arrFullName(2)
                            TbCell.InnerHtml = strFName & " " & strLName
                            TbCell.NoWrap = False
                            TbCell.Width = "100px"
                            TbCell.Align = "center"
                            TbRow.Cells.Add(TbCell)


                            TbCell = New HtmlTableCell
                            TbCell.InnerHtml = stcVarLoanInfo.LC_No
                            TbCell.NoWrap = True
                            TbCell.Align = "center"
                            TbRow.Cells.Add(TbCell)



                            TbCell = New HtmlTableCell
                            TbCell.InnerHtml = stcVarLoanInfo.Mobile
                            TbCell.NoWrap = True
                            TbCell.Align = "center"
                            TbRow.Cells.Add(TbCell)


                            TbCell = New HtmlTableCell
                            TbCell.InnerHtml = stcVarLoanInfo.NotPiadDurationDay
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
                            dtblFilebyCustomerNo = tadpFilebyCustomerNo.GetData(stcVarLoanInfo.CustomerNo)

                            Dim cntxVar As New BusinessObject.dbMehrVosulEntities1



                            If dtblFilebyCustomerNo.Count > 0 Then

                                If drwUserLogin.IsDataUserAdmin = False Then

                                    Dim tadpLoanByNumber As New BusinessObject.dstLoanTableAdapters.spr_Loan_ByLoanNumber_SelectTableAdapter
                                    Dim dtblLoanByNumber As BusinessObject.dstLoan.spr_Loan_ByLoanNumber_SelectDataTable = Nothing
                                    dtblLoanByNumber = tadpLoanByNumber.GetData(stcVarLoanInfo.LC_No, dtblFilebyCustomerNo.First.ID)

                                    Dim intFileID As Integer = dtblFilebyCustomerNo.First.ID
                                    Dim intLoanID As Integer = -1

                                    If dtblLoanByNumber.Count > 0 Then


                                        intLoanID = dtblLoanByNumber.First.ID



                                    Else

                                        Dim qryLoan As New BusinessObject.dstLoanTableAdapters.QueriesTableAdapter


                                        Dim dteLoanDate? As Date = Nothing
                                        Try
                                            stcVarLoanInfo.LCDate = stcVarLoanInfo.LCDate.Insert(4, "/")
                                            stcVarLoanInfo.LCDate = stcVarLoanInfo.LCDate.Insert(7, "/")
                                            dteLoanDate = mdlGeneral.GetGregorianDate(stcVarLoanInfo.LCDate)
                                        Catch ex As Exception
                                            dteLoanDate = Nothing
                                        End Try

                                        Dim tadpLoanTypeByCode As New BusinessObject.dstLoanTypeTableAdapters.spr_LoanType_byCode_SelectTableAdapter
                                        Dim dtblLoanTypeByCode As BusinessObject.dstLoanType.spr_LoanType_byCode_SelectDataTable = Nothing
                                        dtblLoanTypeByCode = tadpLoanTypeByCode.GetData(stcVarLoanInfo.LoanTypeCode)

                                        Dim intLoanTypeID As Integer = -1

                                        If dtblLoanTypeByCode.Rows.Count = 0 Then

                                            Dim strLoanTypeName As String = GetLoanTypeName(stcVarLoanInfo.LoanTypeCode)
                                            Dim qryLoanType As New BusinessObject.dstLoanTypeTableAdapters.QueriesTableAdapter
                                            intLoanTypeID = qryLoanType.spr_LoanType_Insert(stcVarLoanInfo.LoanTypeCode, strLoanTypeName, 2, "")


                                        Else
                                            Dim drwLoanTypeByCode As BusinessObject.dstLoanType.spr_LoanType_byCode_SelectRow = dtblLoanTypeByCode.Rows(0)
                                            intLoanTypeID = drwLoanTypeByCode.ID


                                        End If


                                        intLoanID = qryLoan.spr_Loan_Insert(intFileID, intLoanTypeID, intBranchID, dteLoanDate, stcVarLoanInfo.LC_No, stcVarLoanInfo.LoanSerial, Date.Now, stcVarLoanInfo.LCAmount, Nothing)



                                    End If



                                    Dim intLogCount = cntxVar.tbl_HandyFollow.Where(Function(x) x.FK_FileID = intFileID And x.FK_LoanID = intLoanID).Count()


                                    If intLogCount = 0 Then



                                        TbCell = New HtmlTableCell
                                        TbCell.InnerHtml = "<a ID='lnkbtnFollowing' href='#'  onclick= btnFollwoing_ClientClick(" & intFileID.ToString() & "," & intLoanID.ToString() & "," & stcVarLoanInfo.AmounDefferd & ")>ثبت پیگیری</a>"
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
                                            Dim intLogCountByUser = cntxVar.tbl_HandyFollow.Where(Function(x) x.FK_FileID = intFileID And x.FK_LoanID = intLoanID And x.FK_UserID = intUserId).Count()

                                            If intLogCountByUser = 0 Then

                                                TbCell = New HtmlTableCell
                                                TbCell.InnerHtml = "ثبت پیگیری"
                                                TbCell.NoWrap = False
                                                TbCell.Align = "center"
                                                TbRow.Cells.Add(TbCell)

                                                Dim lnqDetail = cntxVar.tbl_HandyFollow.Where(Function(x) x.FK_FileID = intFileID And x.FK_LoanID = intLoanID)
                                                If lnqDetail.Count > 0 Then

                                                    Dim lnqDetailList = lnqDetail.ToList(0)
                                                    Dim lnqUser = cntxVar.tbl_User.Where(Function(x) x.ID = lnqDetailList.FK_UserID).ToList(0)

                                                    TbCell = New HtmlTableCell
                                                    TbCell.InnerHtml = lnqUser.Username
                                                    TbCell.NoWrap = True
                                                    TbCell.Align = "center"
                                                    TbRow.Cells.Add(TbCell)

                                                End If
                                            Else

                                                TbCell = New HtmlTableCell
                                            TbCell.InnerHtml = "<a ID='lnkbtnFollowing' href='#'  onclick= btnFollwoing_ClientClick(" & intFileID.ToString() & "," & intLoanID.ToString() & stcVarLoanInfo.AmounDefferd & ")>ثبت پیگیری</a>"
                                            TbCell.NoWrap = True
                                                TbCell.Align = "center"
                                                TbRow.Cells.Add(TbCell)

                                                Dim lnqDetail = cntxVar.tbl_HandyFollow.Where(Function(x) x.FK_FileID = intFileID And x.FK_LoanID = intLoanID)
                                                If lnqDetail.Count > 0 Then
                                                    Dim lnqDetailList = lnqDetail.ToList(0)
                                                    Dim lnqUser = cntxVar.tbl_User.Where(Function(x) x.ID = lnqDetailList.FK_UserID).ToList(0)

                                                    TbCell = New HtmlTableCell
                                                    TbCell.InnerHtml = lnqUser.Username
                                                    TbCell.NoWrap = True
                                                    TbCell.Align = "center"
                                                    TbRow.Cells.Add(TbCell)

                                                End If

                                            End If

                                        End If





                                Else

                                    Dim tadpLoanByNumber As New BusinessObject.dstLoanTableAdapters.spr_Loan_ByLoanNumber_SelectTableAdapter
                                    Dim dtblLoanByNumber As BusinessObject.dstLoan.spr_Loan_ByLoanNumber_SelectDataTable = Nothing
                                    dtblLoanByNumber = tadpLoanByNumber.GetData(stcVarLoanInfo.LC_No, dtblFilebyCustomerNo.First.ID)

                                    Dim intFileID As Integer = dtblFilebyCustomerNo.First.ID
                                    Dim intLoanID As Integer = -1

                                    If dtblLoanByNumber.Count > 0 Then


                                        intLoanID = dtblLoanByNumber.First.ID



                                    Else

                                        Dim qryLoan As New BusinessObject.dstLoanTableAdapters.QueriesTableAdapter


                                        Dim dteLoanDate? As Date = Nothing
                                        Try
                                            stcVarLoanInfo.LCDate = stcVarLoanInfo.LCDate.Insert(4, "/")
                                            stcVarLoanInfo.LCDate = stcVarLoanInfo.LCDate.Insert(7, "/")
                                            dteLoanDate = mdlGeneral.GetGregorianDate(stcVarLoanInfo.LCDate)
                                        Catch ex As Exception
                                            dteLoanDate = Nothing
                                        End Try

                                        Dim tadpLoanTypeByCode As New BusinessObject.dstLoanTypeTableAdapters.spr_LoanType_byCode_SelectTableAdapter
                                        Dim dtblLoanTypeByCode As BusinessObject.dstLoanType.spr_LoanType_byCode_SelectDataTable = Nothing
                                        dtblLoanTypeByCode = tadpLoanTypeByCode.GetData(stcVarLoanInfo.LoanTypeCode)

                                        Dim intLoanTypeID As Integer = -1

                                        If dtblLoanTypeByCode.Rows.Count = 0 Then

                                            Dim strLoanTypeName As String = GetLoanTypeName(stcVarLoanInfo.LoanTypeCode)
                                            Dim qryLoanType As New BusinessObject.dstLoanTypeTableAdapters.QueriesTableAdapter
                                            intLoanTypeID = qryLoanType.spr_LoanType_Insert(stcVarLoanInfo.LoanTypeCode, strLoanTypeName, 2, "")


                                        Else
                                            Dim drwLoanTypeByCode As BusinessObject.dstLoanType.spr_LoanType_byCode_SelectRow = dtblLoanTypeByCode.Rows(0)
                                            intLoanTypeID = drwLoanTypeByCode.ID


                                        End If


                                        intLoanID = qryLoan.spr_Loan_Insert(intFileID, intLoanTypeID, intBranchID, dteLoanDate, stcVarLoanInfo.LC_No, stcVarLoanInfo.LoanSerial, Date.Now, stcVarLoanInfo.LCAmount, Nothing)



                                    End If

                                    TbCell = New HtmlTableCell
                                    TbCell.InnerHtml = "<a ID='lnkbtnFollowing' href='#'  onclick= btnFollwoing_ClientClick(" & intFileID.ToString() & "," & intLoanID.ToString() & "," & stcVarLoanInfo.AmounDefferd & ")>ثبت پیگیری</a>"
                                    TbCell.NoWrap = True
                                    TbCell.Align = "center"
                                    TbRow.Cells.Add(TbCell)


                                    Dim lnqDetail = cntxVar.tbl_HandyFollow.Where(Function(x) x.FK_FileID = intFileID And x.FK_LoanID = intLoanID)
                                    Dim followingUserName As String = "---"
                                    ''********************
                                    If lnqDetail.Count > 0 Then
                                        Dim lnqDetailList = lnqDetail.ToList(0)

                                        Dim lnqUser = cntxVar.tbl_User.Where(Function(x) x.ID = lnqDetailList.FK_UserID).ToList(0)
                                        followingUserName = lnqUser.Username
                                    End If



                                    TbCell = New HtmlTableCell
                                    TbCell.InnerHtml = followingUserName
                                    TbCell.NoWrap = True
                                    TbCell.Align = "center"
                                    TbRow.Cells.Add(TbCell)


                                End If

                            Else


                                Session("BranchID") = intBranchID
                                Session("BranchName") = strBranchName
                                Session("CustomerNO") = stcVarLoanInfo.CustomerNo

                                TbCell = New HtmlTableCell
                                TbCell.InnerHtml = "<a ID='lnkbtnFollowing' href='#'  onclick= btnFollwoing_ClientClick(" & -1 & "," & -1 & "," & stcVarLoanInfo.AmounDefferd & ")>ثبت پیگیری</a>"
                                TbCell.NoWrap = True
                                TbCell.Align = "center"
                                TbRow.Cells.Add(TbCell)

                                TbCell = New HtmlTableCell
                                TbCell.InnerHtml = "---"
                                TbCell.NoWrap = True
                                TbCell.Align = "center"
                                TbRow.Cells.Add(TbCell)


                            End If



                        End With



                        divResult.Visible = True
                        tblResult.Rows.Add(TbRow)


                    Catch ex As Exception
                        '' qryErrorLog.spr_ErrorLog_Insert(ex.Message, 3, "5")
                        Continue Do

                    End Try


                Loop While dataReader.Read()
                dataReader.Close()

                listOperationLoan.Clear()

            Catch ex As Exception

                Bootstrap_Panel1.ShowMessage(ex.Message, True)

                cnnBI_Connection.Close()

                Return
            End Try

        End Using




    End Sub


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