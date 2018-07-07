Imports System.Data.OracleClient
Imports System.Data.Entity

Public Class HandyFollowSearch
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
        txtFrom.Attributes.Add("onkeypress", "return numbersonly(event, false);")
        txtTo.Attributes.Add("onkeypress", "return numbersonly(event, false);")

        If Page.IsPostBack = False Then

            ''Get current User Brnach

            Dim dtblUserLogin As BusinessObject.dstUser.spr_User_Login_SelectDataTable = CType(Session("dtblUserLogin"), BusinessObject.dstUser.spr_User_Login_SelectDataTable)
            Dim drwUserLogin As BusinessObject.dstUser.spr_User_Login_SelectRow = dtblUserLogin.Rows(0)


            odcProvince.DataBind()

            cmbProvince.DataSourceID = "odcProvince"
            cmbProvince.DataTextField = "Province"
            cmbProvince.DataValueField = "ID"
            cmbProvince.DataBind()

            odsLoanType.DataBind()

            cmbLoanType.DataSourceID = "odsLoanType"
            cmbLoanType.DataTextField = "LoanType"
            cmbLoanType.DataValueField = "ID"
            cmbLoanType.DataBind()

            If drwUserLogin.IsDataAdmin = True Then
                If CInt(Request.QueryString("Province")) <> -1 Then
                    cmbProvince.SelectedValue = CInt(Request.QueryString("Province"))
                Else
                    cmbProvince.SelectedValue = drwUserLogin.Fk_ProvinceID
                End If

                odsBranch.SelectParameters.Item("Action").DefaultValue = 2
                odsBranch.SelectParameters.Item("ProvinceID").DefaultValue = cmbProvince.SelectedValue
                odsBranch.DataBind()

                cmbBranch.DataSourceID = "odsBranch"
                cmbBranch.DataTextField = "BrnachCode"
                cmbBranch.DataValueField = "ID"
                cmbBranch.DataBind()

                If CInt(Request.QueryString("Branch")) <> -1 Then
                    cmbBranch.SelectedValue = CInt(Request.QueryString("Branch"))
                Else
                    cmbBranch.SelectedValue = -1
                End If



            ElseIf drwUserLogin.IsDataAdmin = False And drwUserLogin.IsDataUserAdmin = False Then

                cmbProvince.SelectedValue = drwUserLogin.Fk_ProvinceID

                odsBranch.SelectParameters.Item("Action").DefaultValue = 2
                odsBranch.SelectParameters.Item("ProvinceID").DefaultValue = drwUserLogin.Fk_ProvinceID
                odsBranch.DataBind()

                cmbBranch.DataSourceID = "odsBranch"
                cmbBranch.DataTextField = "BrnachCode"
                cmbBranch.DataValueField = "ID"

                If CInt(Request.QueryString("Branch")) <> -1 AndAlso Not Request.QueryString("Branch") Is Nothing Then
                    cmbBranch.SelectedValue = CInt(Request.QueryString("Branch"))
                Else
                    cmbBranch.SelectedValue = drwUserLogin.FK_BrnachID
                End If

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

                If CInt(Request.QueryString("Branch")) <> -1 AndAlso Not Request.QueryString("Branch") Is Nothing Then

                    cmbBranch.SelectedValue = CInt(Request.QueryString("Branch"))

                Else
                    cmbBranch.SelectedValue = drwUserLogin.FK_BrnachID

                End If

                cmbProvince.Enabled = False

            End If



            If Not Request.QueryString("From") Is Nothing Then

                txtFrom.Text = Request.QueryString("From")

                txtTo.Text = Request.QueryString("To")

                If CInt(Request.QueryString("Province")) <> -1 Then
                    cmbProvince.SelectedValue = CInt(Request.QueryString("Province"))
                Else
                    cmbProvince.SelectedValue = -1
                End If

                If CInt(Request.QueryString("Branch")) <> -1 Then
                    cmbBranch.SelectedValue = CInt(Request.QueryString("Branch"))
                Else
                    cmbBranch.SelectedValue = -1
                End If

                If CInt(Request.QueryString("LoanType")) <> -1 Then
                    cmbLoanType.SelectedValue = CInt(Request.QueryString("LoanType"))
                Else
                    cmbLoanType.SelectedValue = -1
                End If


                '' DisplayFileList()

            End If



        End If

        If hdnAction.Value.StartsWith("S") = True Then


            Dim intFileID As Integer = CInt(hdnAction.Value.Split(";")(1))
            Dim intLoanID As Integer = CInt(hdnAction.Value.Split(";")(2))
            Session("AmountDeffed") = hdnAction.Value.Split(";")(3)
            Session("intFileID") = CObj(intFileID)
            Session("intLoanID") = CObj(intLoanID)

            Session("Province") = CObj(cmbProvince.SelectedValue)
            Session("Branch") = CObj(cmbBranch.SelectedValue)
            Session("LoanType") = CObj(cmbLoanType.SelectedValue)
            Session("From") = CObj(txtFrom.Text)
            Session("TO") = CObj(txtTo.Text)
            Response.Redirect("HandyFollowNew.aspx")

        End If

    End Sub

    Private Sub Bootstrap_Panel1_Panel_Display_Click(sender As Object, e As System.EventArgs) Handles Bootstrap_Panel1.Panel_Display_Click

        '' DisplayFileList()

    End Sub

    'Private Sub DisplayFileList()


    '    Dim dtblUserLogin As BusinessObject.dstUser.spr_User_Login_SelectDataTable = CType(Session("dtblUserLogin"), BusinessObject.dstUser.spr_User_Login_SelectDataTable)
    '    Dim drwUserLogin As BusinessObject.dstUser.spr_User_Login_SelectRow = dtblUserLogin.Rows(0)

    '    Dim tadpReport As New BusinessObject.dstReportTableAdapters.spr_Report_CurrentLCStatus_MaxLoanType_SelectTableAdapter
    '    Dim dtblReport As BusinessObject.dstReport.spr_Report_CurrentLCStatus_MaxLoanType_SelectDataTable = Nothing


    '    If txtCustomerNO.Text.Trim() = "" Then

    '        Dim intInstallmentCount As Integer = CInt(txt_InstallmentCount.Text)

    '        If cmbBranch.SelectedValue = -1 And cmbLoanType.SelectedValue = -1 And cmbProvince.SelectedValue = -1 Then

    '            dtblReport = tadpReport.GetData(1, -1, -1, intInstallmentCount, -1, -1)

    '        ElseIf cmbProvince.SelectedValue = -1 And cmbBranch.SelectedValue <> -1 And cmbLoanType.SelectedValue = -1 Then

    '            Dim intBranch As Integer = CInt(cmbBranch.SelectedValue)
    '            dtblReport = tadpReport.GetData(2, intBranch, -1, intInstallmentCount, -1, -1)

    '        ElseIf cmbProvince.SelectedValue = -1 And cmbBranch.SelectedValue = -1 And cmbLoanType.SelectedValue <> -1 Then

    '            Dim intLoanTypeID As Integer = CInt(cmbLoanType.SelectedValue)
    '            dtblReport = tadpReport.GetData(3, -1, intLoanTypeID, intInstallmentCount, -1, -1)

    '        ElseIf cmbProvince.SelectedValue = -1 And cmbBranch.SelectedValue <> -1 And cmbLoanType.SelectedValue <> -1 Then

    '            Dim intLoanTypeID As Integer = CInt(cmbLoanType.SelectedValue)
    '            Dim intBranch As Integer = CInt(cmbBranch.SelectedValue)
    '            dtblReport = tadpReport.GetData(4, intBranch, intLoanTypeID, intInstallmentCount, -1, -1)

    '        ElseIf cmbProvince.SelectedValue <> -1 And cmbBranch.SelectedValue = -1 And cmbLoanType.SelectedValue = -1 Then

    '            Dim intProvince As Integer = CInt(cmbProvince.SelectedValue)
    '            dtblReport = tadpReport.GetData(5, -1, -1, intInstallmentCount, intProvince, -1)

    '        ElseIf cmbProvince.SelectedValue <> -1 And cmbBranch.SelectedValue = -1 And cmbLoanType.SelectedValue <> -1 Then

    '            Dim intProvince As Integer = CInt(cmbProvince.SelectedValue)
    '            Dim intLoanTypeID As Integer = CInt(cmbLoanType.SelectedValue)
    '            dtblReport = tadpReport.GetData(6, -1, intLoanTypeID, intInstallmentCount, intProvince, -1)

    '        ElseIf cmbProvince.SelectedValue <> -1 And cmbBranch.SelectedValue <> -1 And cmbLoanType.SelectedValue <> -1 Then

    '            Dim intBranch As Integer = CInt(cmbBranch.SelectedValue)
    '            Dim intProvince As Integer = CInt(cmbProvince.SelectedValue)
    '            Dim intLoanTypeID As Integer = CInt(cmbLoanType.SelectedValue)
    '            dtblReport = tadpReport.GetData(7, intBranch, intLoanTypeID, intInstallmentCount, intProvince, -1)

    '        ElseIf cmbProvince.SelectedValue <> -1 And cmbBranch.SelectedValue <> -1 And cmbLoanType.SelectedValue = -1 Then

    '            Dim intBranch As Integer = CInt(cmbBranch.SelectedValue)
    '            Dim intProvince As Integer = CInt(cmbProvince.SelectedValue)
    '            Dim intLoanTypeID As Integer = CInt(cmbLoanType.SelectedValue)
    '            dtblReport = tadpReport.GetData(8, intBranch, intLoanTypeID, intInstallmentCount, intProvince, -1)

    '        End If

    '    Else


    '        dtblReport = tadpReport.GetData(9, -1, -1, -1, -1, txtCustomerNO.Text.Trim())

    '    End If


    '    If dtblReport.Rows.Count > 0 Then

    '        divResult.Visible = True

    '        ''get total report
    '        Dim intCount As Integer = 0

    '        Dim strchklstMenuLeaves As String = ""
    '        For Each drwReport As BusinessObject.dstReport.spr_Report_CurrentLCStatus_MaxLoanType_SelectRow In dtblReport.Rows

    '            intCount += 1
    '            Dim TbRow As New HtmlTableRow
    '            Dim TbCell As HtmlTableCell


    '            ''strchklstMenuLeaves = "<input type='checkbox' value='" & drwReport.ID & "' name='chklstMenu" & drwReport.MobileNo & "'>"

    '            ''TbCell = New HtmlTableCell
    '            ''TbCell.InnerHtml = strchklstMenuLeaves
    '            ''TbCell.Align = "center"
    '            ''TbCell.NoWrap = True
    '            ''TbRow.Cells.Add(TbCell)



    '            TbCell = New HtmlTableCell
    '            TbCell.InnerHtml = CStr(intCount)
    '            TbCell.Align = "center"
    '            TbCell.NoWrap = True
    '            TbRow.Cells.Add(TbCell)


    '            TbCell = New HtmlTableCell
    '            TbCell.InnerHtml = drwReport.CustomerNo
    '            TbCell.NoWrap = True
    '            TbCell.Align = "center"
    '            TbRow.Cells.Add(TbCell)

    '            TbCell = New HtmlTableCell
    '            TbCell.InnerHtml = drwReport.FName & " " & drwReport.LName
    '            TbCell.NoWrap = False
    '            TbCell.Width = "100px"
    '            TbCell.Align = "center"
    '            TbRow.Cells.Add(TbCell)


    '            TbCell = New HtmlTableCell
    '            TbCell.InnerHtml = drwReport.Loan
    '            TbCell.NoWrap = True
    '            TbCell.Align = "center"
    '            TbRow.Cells.Add(TbCell)



    '            TbCell = New HtmlTableCell
    '            TbCell.InnerHtml = drwReport.MobileNo
    '            TbCell.NoWrap = True
    '            TbCell.Align = "center"
    '            TbRow.Cells.Add(TbCell)


    '            TbCell = New HtmlTableCell
    '            TbCell.InnerHtml = drwReport.NotPiadDurationDay
    '            TbCell.NoWrap = True
    '            TbCell.Width = "50px"
    '            TbCell.Align = "center"
    '            TbRow.Cells.Add(TbCell)

    '            TbCell = New HtmlTableCell
    '            TbCell.InnerHtml = drwReport.Branch
    '            TbCell.NoWrap = False
    '            TbCell.Width = "120px"
    '            TbCell.Align = "center"
    '            TbRow.Cells.Add(TbCell)


    '            Dim cntxVar As New BusinessObject.dbMehrVosulEntities1



    '            If drwUserLogin.IsDataUserAdmin = False Then


    '                Dim intLogCount = cntxVar.tbl_HandyFollow.Where(Function(x) x.FK_FileID = drwReport.FileID And x.FK_LoanID = drwReport.LoanID).Count()
    '                If intLogCount = 0 Then



    '                    TbCell = New HtmlTableCell
    '                    TbCell.InnerHtml = "<a ID='lnkbtnFollowing' href='#'  onclick= btnFollwoing_ClientClick(" & drwReport.FileID & "," & drwReport.LoanID & "," & drwReport.AmounDefferd & ")>ثبت پیگیری</a>"
    '                    TbCell.NoWrap = True
    '                    TbCell.Align = "center"
    '                    TbRow.Cells.Add(TbCell)

    '                    TbCell = New HtmlTableCell
    '                    TbCell.InnerHtml = "---"
    '                    TbCell.NoWrap = True
    '                    TbCell.Align = "center"
    '                    TbRow.Cells.Add(TbCell)

    '                Else

    '                    Dim intLogCountByUser = cntxVar.tbl_HandyFollow.Where(Function(x) x.FK_FileID = drwReport.FileID And x.FK_LoanID = drwReport.LoanID And x.FK_UserID = drwUserLogin.ID).Count()

    '                    If intLogCountByUser = 0 Then

    '                        TbCell = New HtmlTableCell
    '                        TbCell.InnerHtml = "ثبت پیگیری"
    '                        TbCell.NoWrap = False
    '                        TbCell.Align = "center"
    '                        TbRow.Cells.Add(TbCell)

    '                        Dim lnqDetail = cntxVar.tbl_HandyFollow.Where(Function(x) x.FK_FileID = drwReport.FileID And x.FK_LoanID = drwReport.LoanID)
    '                        If lnqDetail.Count > 0 Then
    '                            Dim lnqDetailList = lnqDetail.ToList(0)
    '                            Dim lnqUser = cntxVar.tbl_User.Where(Function(x) x.ID = lnqDetailList.FK_UserID).ToList(0)

    '                            TbCell = New HtmlTableCell
    '                            TbCell.InnerHtml = lnqUser.Username
    '                            TbCell.NoWrap = True
    '                            TbCell.Align = "center"
    '                            TbRow.Cells.Add(TbCell)

    '                        End If
    '                    Else

    '                        TbCell = New HtmlTableCell
    '                        TbCell.InnerHtml = "<a ID='lnkbtnFollowing' href='#'  onclick= btnFollwoing_ClientClick(" & drwReport.FileID & "," & drwReport.LoanID & drwReport.AmounDefferd & ")>ثبت پیگیری</a>"
    '                        TbCell.NoWrap = True
    '                        TbCell.Align = "center"
    '                        TbRow.Cells.Add(TbCell)

    '                        Dim lnqDetail = cntxVar.tbl_HandyFollow.Where(Function(x) x.FK_FileID = drwReport.FileID And x.FK_LoanID = drwReport.LoanID)
    '                        If lnqDetail.Count > 0 Then
    '                            Dim lnqDetailList = lnqDetail.ToList(0)
    '                            Dim lnqUser = cntxVar.tbl_User.Where(Function(x) x.ID = lnqDetailList.FK_UserID).ToList(0)

    '                            TbCell = New HtmlTableCell
    '                            TbCell.InnerHtml = lnqUser.Username
    '                            TbCell.NoWrap = True
    '                            TbCell.Align = "center"
    '                            TbRow.Cells.Add(TbCell)

    '                        End If

    '                    End If



    '                End If
    '            Else


    '                TbCell = New HtmlTableCell
    '                TbCell.InnerHtml = "<a ID='lnkbtnFollowing' href='#'  onclick= btnFollwoing_ClientClick(" & drwReport.FileID & "," & drwReport.LoanID & "," & drwReport.AmounDefferd & ")>ثبت پیگیری</a>"
    '                TbCell.NoWrap = True
    '                TbCell.Align = "center"
    '                TbRow.Cells.Add(TbCell)


    '                Dim lnqDetail = cntxVar.tbl_HandyFollow.Where(Function(x) x.FK_FileID = drwReport.FileID And x.FK_LoanID = drwReport.LoanID)
    '                Dim followingUserName As String = "---"
    '                If lnqDetail.Count > 0 Then
    '                    Dim lnqDetailList = lnqDetail.ToList(0)

    '                    Dim lnqUser = cntxVar.tbl_User.Where(Function(x) x.ID = lnqDetailList.FK_UserID).ToList(0)
    '                    followingUserName = lnqUser.Username
    '                End If


    '                TbCell = New HtmlTableCell
    '                TbCell.InnerHtml = followingUserName
    '                TbCell.NoWrap = True
    '                TbCell.Align = "center"
    '                TbRow.Cells.Add(TbCell)


    '            End If




    '            tblResult.Rows.Add(TbRow)

    '        Next


    '    Else

    '        divResult.Visible = False


    '    End If

    'End Sub


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


    ''Protected Sub DisplayFileList()

    ''    Bootstrap_Panel1.ClearMessage()

    ''    Dim cnnBuiler_BI As New OracleConnectionStringBuilder()
    ''    cnnBuiler_BI.DataSource = "10.35.1.37:1522/bidb"
    ''    cnnBuiler_BI.UserID = "deposit"
    ''    cnnBuiler_BI.Password = "deposit"
    ''    cnnBuiler_BI.Unicode = True

    ''    Dim dteThisDate As Date = Date.Now.AddDays(-1)
    ''    Dim strThisDatePersian As String = mdlGeneral.GetPersianDate(dteThisDate).Replace("/", "")

    ''    Using cnnBI_Connection As New OracleConnection(cnnBuiler_BI.ConnectionString)

    ''        Dim cmd_BI As OracleCommand = cnnBI_Connection.CreateCommand()

    ''        Dim strLoan_Info_Query As String = "SELECT * from loan_info where Date_P='" & strThisDatePersian & "' and state in ('3','4','5','F')  and ("

    ''        Dim strBranchQuery As String = ""
    ''        Dim ctxMehr As New BusinessObject.dbMehrVosulEntities1


    ''        If cmbBranch.SelectedValue <> -1 Then
    ''            Dim lnqBranchCode = ctxMehr.tbl_Branch.Where(Function(x) x.ID = cmbBranch.SelectedValue).First
    ''            strLoan_Info_Query &= " ABRNCHCOD='" & lnqBranchCode.BrnachCode & "' )  "
    ''        End If


    ''        If cmbLoanType.SelectedValue <> -1 Then
    ''            Dim lnqLoanTypeCode = ctxMehr.tbl_LoanType.Where(Function(x) x.ID = cmbLoanType.SelectedValue).First
    ''            strLoan_Info_Query &= "  and ( LNMINORTP='" & lnqLoanTypeCode.LoanTypeCode & "' )  "

    ''        End If

    ''        Dim strFromDay As String = txtFrom.Text
    ''        Dim strToDay As String = txtTo.Text

    ''        strLoan_Info_Query &= " and (NPDURATION between " & strFromDay & " and " & strToDay & " )"



    ''        cmd_BI.CommandText = strLoan_Info_Query

    ''        Try
    ''            cnnBI_Connection.Open()
    ''        Catch ex As Exception

    ''            Bootstrap_Panel1.ShowMessage(ex.Message, True)
    ''            Return
    ''        End Try
    ''        Dim dataReader As OracleDataReader = Nothing

    ''        Try
    ''            dataReader = cmd_BI.ExecuteReader()

    ''            If dataReader.Read = False Then

    ''                Bootstrap_Panel1.ShowMessage(" موردی یافت نشد ", True)

    ''                dataReader.Close()
    ''                cnnBI_Connection.Close()

    ''                Return
    ''            End If

    ''            Dim i As Integer
    ''            Dim intCount As Integer = 0
    ''            Dim stcVarLoanInfo As stc_Loan_Info

    ''            Dim dtblUserLogin As BusinessObject.dstUser.spr_User_Login_SelectDataTable = CType(Session("dtblUserLogin"), BusinessObject.dstUser.spr_User_Login_SelectDataTable)
    ''            Dim drwUserLogin As BusinessObject.dstUser.spr_User_Login_SelectRow = dtblUserLogin.Rows(0)

    ''            Dim listOperationLoan As New ArrayList
    ''            Dim qryErrorLog As New BusinessObject.dstLoginLogTableAdapters.QueriesTableAdapter


    ''            Do
    ''                i += 1
    ''                Dim TbRow As HtmlTableRow


    ''                Try

    ''                    If dataReader.GetValue(0) Is DBNull.Value Then
    ''                        stcVarLoanInfo.FullName = ""
    ''                    Else
    ''                        stcVarLoanInfo.FullName = CStr(dataReader.GetValue(0)).Replace("'", "")
    ''                    End If


    ''                    If dataReader.GetValue(4) Is DBNull.Value Then
    ''                        stcVarLoanInfo.Mobile = ""
    ''                    Else
    ''                        stcVarLoanInfo.Mobile = CStr(dataReader.GetValue(4)).Trim.Replace("'", "")
    ''                    End If


    ''                    If dataReader.GetValue(6) Is DBNull.Value Then
    ''                        i -= 1
    ''                        Continue Do
    ''                    Else
    ''                        stcVarLoanInfo.LC_No = CStr(dataReader.GetValue(6)).Trim.Replace("'", "")
    ''                    End If


    ''                    If dataReader.GetValue(7) Is DBNull.Value Then
    ''                        i -= 1
    ''                        Continue Do
    ''                    Else
    ''                        stcVarLoanInfo.BranchCode = CStr(dataReader.GetValue(7)).Trim
    ''                    End If


    ''                    If dataReader.GetValue(9) Is DBNull.Value Then
    ''                        i -= 1
    ''                        Continue Do
    ''                    Else
    ''                        stcVarLoanInfo.CustomerNo = CStr(dataReader.GetValue(9)).Trim.Replace("'", "")
    ''                    End If


    ''                    If dataReader.GetValue(10) Is DBNull.Value Then
    ''                        i -= 1
    ''                        Continue Do
    ''                    Else
    ''                        stcVarLoanInfo.LoanSerial = CInt(dataReader.GetValue(10))
    ''                    End If

    ''                    If dataReader.GetValue(11) Is DBNull.Value Then
    ''                        stcVarLoanInfo.LCDate = ""
    ''                    Else
    ''                        stcVarLoanInfo.LCDate = CStr(dataReader.GetValue(11)).Trim
    ''                    End If

    ''                    If dataReader.GetValue(12) Is DBNull.Value Then
    ''                        stcVarLoanInfo.LCAmount = Nothing
    ''                    Else
    ''                        stcVarLoanInfo.LCAmount = CDbl(dataReader.GetValue(12))
    ''                    End If



    ''                    If dataReader.GetValue(17) Is DBNull.Value Then
    ''                        stcVarLoanInfo.AmounDefferd = Nothing
    ''                    Else
    ''                        stcVarLoanInfo.AmounDefferd = CDbl(dataReader.GetValue(17))
    ''                    End If



    ''                    If dataReader.GetValue(21) Is DBNull.Value Then
    ''                        i -= 1
    ''                        Continue Do
    ''                    Else
    ''                        stcVarLoanInfo.NotPiadDurationDay = CInt(dataReader.GetValue(21))
    ''                    End If

    ''                    ''check if Customer Number is double or not, if it is then skip it
    ''                    If i = 1 Then

    ''                        listOperationLoan.Add(stcVarLoanInfo.CustomerNo)

    ''                    Else


    ''                        For Each obj In listOperationLoan
    ''                            If obj = stcVarLoanInfo.CustomerNo Then
    ''                                Continue Do
    ''                            End If
    ''                        Next

    ''                        listOperationLoan.Add(stcVarLoanInfo.CustomerNo)

    ''                    End If


    ''                    Dim tadpBranchbyCode As New BusinessObject.dstBranchTableAdapters.spr_Branch_ByCode_SelectTableAdapter
    ''                    Dim dtblBranchbyCode As BusinessObject.dstBranch.spr_Branch_ByCode_SelectDataTable = Nothing
    ''                    dtblBranchbyCode = tadpBranchbyCode.GetData(stcVarLoanInfo.BranchCode)

    ''                    Dim intBranchID As Integer = -1
    ''                    Dim strBranchName As String = ""
    ''                    If dtblBranchbyCode.Rows.Count = 0 Then

    ''                        Dim obj_stc_BranchInfo As stc_Branch_Info = GetBarnchName(stcVarLoanInfo.BranchCode)
    ''                        Dim qryBranch As New BusinessObject.dstBranchTableAdapters.QueriesTableAdapter
    ''                        intBranchID = qryBranch.spr_Branch_Insert(stcVarLoanInfo.BranchCode, obj_stc_BranchInfo.BranchName, obj_stc_BranchInfo.BranchAddress, 2, Nothing, "", 1) 'UserID=2 is System User
    ''                        strBranchName = obj_stc_BranchInfo.BranchName


    ''                    Else

    ''                        Dim drwBranchbyCode As BusinessObject.dstBranch.spr_Branch_ByCode_SelectRow = dtblBranchbyCode.Rows(0)

    ''                        intBranchID = drwBranchbyCode.ID
    ''                        strBranchName = drwBranchbyCode.BranchName

    ''                    End If



    ''                    ''get total report

    ''                    With stcVarLoanInfo

    ''                        intCount += 1


    ''                        Dim TbCell As HtmlTableCell
    ''                        TbRow = New HtmlTableRow


    ''                        TbCell = New HtmlTableCell
    ''                        TbCell.InnerHtml = CStr(intCount)
    ''                        TbCell.Align = "center"
    ''                        TbCell.NoWrap = True
    ''                        TbRow.Cells.Add(TbCell)


    ''                        TbCell = New HtmlTableCell
    ''                        TbCell.InnerHtml = stcVarLoanInfo.CustomerNo
    ''                        TbCell.NoWrap = True
    ''                        TbCell.Align = "center"
    ''                        TbRow.Cells.Add(TbCell)

    ''                        TbCell = New HtmlTableCell

    ''                        Dim arrFullName() As String = stcVarLoanInfo.FullName.Split("*")
    ''                        Dim strFatherName As String = arrFullName(0)
    ''                        Dim strFName As String = arrFullName(1)
    ''                        Dim strLName As String = arrFullName(2)
    ''                        TbCell.InnerHtml = strFName & " " & strLName
    ''                        TbCell.NoWrap = False
    ''                        TbCell.Width = "100px"
    ''                        TbCell.Align = "center"
    ''                        TbRow.Cells.Add(TbCell)


    ''                        TbCell = New HtmlTableCell
    ''                        TbCell.InnerHtml = stcVarLoanInfo.LC_No
    ''                        TbCell.NoWrap = True
    ''                        TbCell.Align = "center"
    ''                        TbRow.Cells.Add(TbCell)



    ''                        TbCell = New HtmlTableCell
    ''                        TbCell.InnerHtml = stcVarLoanInfo.Mobile
    ''                        TbCell.NoWrap = True
    ''                        TbCell.Align = "center"
    ''                        TbRow.Cells.Add(TbCell)


    ''                        TbCell = New HtmlTableCell
    ''                        TbCell.InnerHtml = stcVarLoanInfo.NotPiadDurationDay
    ''                        TbCell.NoWrap = True
    ''                        TbCell.Width = "50px"
    ''                        TbCell.Align = "center"
    ''                        TbRow.Cells.Add(TbCell)

    ''                        TbCell = New HtmlTableCell
    ''                        TbCell.InnerHtml = strBranchName
    ''                        TbCell.NoWrap = False
    ''                        TbCell.Width = "120px"
    ''                        TbCell.Align = "center"
    ''                        TbRow.Cells.Add(TbCell)



    ''                        Dim tadpFilebyCustomerNo As New BusinessObject.dstFileTableAdapters.spr_File_CustomerNo_SelectTableAdapter
    ''                        Dim dtblFilebyCustomerNo As BusinessObject.dstFile.spr_File_CustomerNo_SelectDataTable = Nothing
    ''                        dtblFilebyCustomerNo = tadpFilebyCustomerNo.GetData(stcVarLoanInfo.CustomerNo)

    ''                        Dim cntxVar As New BusinessObject.dbMehrVosulEntities1



    ''                        If dtblFilebyCustomerNo.Count > 0 Then

    ''                            If drwUserLogin.IsDataUserAdmin = False Then

    ''                                Dim tadpLoanByNumber As New BusinessObject.dstLoanTableAdapters.spr_Loan_ByLoanNumber_SelectTableAdapter
    ''                                Dim dtblLoanByNumber As BusinessObject.dstLoan.spr_Loan_ByLoanNumber_SelectDataTable = Nothing
    ''                                dtblLoanByNumber = tadpLoanByNumber.GetData(stcVarLoanInfo.LC_No, dtblFilebyCustomerNo.First.ID)

    ''                                If dtblLoanByNumber.Count > 0 Then

    ''                                    'Dim tadpHandyFollow As New BusinessObject.dstHandyFollowTableAdapters.spr_HandyFollow_CheckFileLoan_SelectTableAdapter
    ''                                    'Dim dtblHandyFollow As BusinessObject.dstHandyFollow.spr_HandyFollow_CheckFileLoan_SelectDataTable = Nothing

    ''                                    'dtblHandyFollow = tadpHandyFollow.GetData(dtblLoanByNumber.First.ID, dtblFilebyCustomerNo.First.ID)
    ''                                    Dim intFileID As Integer = dtblFilebyCustomerNo.First.ID
    ''                                    Dim intLoanID As Integer = dtblLoanByNumber.First.ID
    ''                                    Dim intLogCount = cntxVar.tbl_HandyFollow.Where(Function(x) x.FK_FileID = intFileID And x.FK_LoanID = intLoanID).Count()
    ''                                    If intLogCount = 0 Then



    ''                                        TbCell = New HtmlTableCell
    ''                                        TbCell.InnerHtml = "<a ID='lnkbtnFollowing' href='#'  onclick= btnFollwoing_ClientClick(" & dtblFilebyCustomerNo.First.ID & "," & dtblLoanByNumber.First.ID & "," & stcVarLoanInfo.AmounDefferd & ")>ثبت پیگیری</a>"
    ''                                        TbCell.NoWrap = True
    ''                                        TbCell.Align = "center"
    ''                                        TbRow.Cells.Add(TbCell)

    ''                                        TbCell = New HtmlTableCell
    ''                                        TbCell.InnerHtml = "---"
    ''                                        TbCell.NoWrap = True
    ''                                        TbCell.Align = "center"
    ''                                        TbRow.Cells.Add(TbCell)

    ''                                    Else

    ''                                        Dim intUserId As Integer = drwUserLogin.ID
    ''                                        Dim intLogCountByUser = cntxVar.tbl_HandyFollow.Where(Function(x) x.FK_FileID = intFileID And x.FK_LoanID = intLoanID And x.FK_UserID = intUserId).Count()



    ''                                        If intLogCountByUser = 0 Then

    ''                                            TbCell = New HtmlTableCell
    ''                                            TbCell.InnerHtml = "ثبت پیگیری"
    ''                                            TbCell.NoWrap = False
    ''                                            TbCell.Align = "center"
    ''                                            TbRow.Cells.Add(TbCell)

    ''                                            Dim lnqDetail = cntxVar.tbl_HandyFollow.Where(Function(x) x.FK_FileID = intFileID And x.FK_LoanID = intLoanID)
    ''                                            If lnqDetail.Count > 0 Then

    ''                                                Dim lnqDetailList = lnqDetail.ToList(0)
    ''                                                Dim lnqUser = cntxVar.tbl_User.Where(Function(x) x.ID = lnqDetailList.FK_UserID).ToList(0)

    ''                                                TbCell = New HtmlTableCell
    ''                                                TbCell.InnerHtml = lnqUser.Username
    ''                                                TbCell.NoWrap = True
    ''                                                TbCell.Align = "center"
    ''                                                TbRow.Cells.Add(TbCell)

    ''                                            End If
    ''                                        Else

    ''                                            TbCell = New HtmlTableCell
    ''                                            TbCell.InnerHtml = "<a ID='lnkbtnFollowing' href='#'  onclick= btnFollwoing_ClientClick(" & dtblFilebyCustomerNo.First.ID & "," & dtblLoanByNumber.First.ID & stcVarLoanInfo.AmounDefferd & ")>ثبت پیگیری</a>"
    ''                                            TbCell.NoWrap = True
    ''                                            TbCell.Align = "center"
    ''                                            TbRow.Cells.Add(TbCell)

    ''                                            Dim lnqDetail = cntxVar.tbl_HandyFollow.Where(Function(x) x.FK_FileID = intFileID And x.FK_LoanID = intLoanID)
    ''                                            If lnqDetail.Count > 0 Then
    ''                                                Dim lnqDetailList = lnqDetail.ToList(0)
    ''                                                Dim lnqUser = cntxVar.tbl_User.Where(Function(x) x.ID = lnqDetailList.FK_UserID).ToList(0)

    ''                                                TbCell = New HtmlTableCell
    ''                                                TbCell.InnerHtml = lnqUser.Username
    ''                                                TbCell.NoWrap = True
    ''                                                TbCell.Align = "center"
    ''                                                TbRow.Cells.Add(TbCell)

    ''                                            End If

    ''                                        End If



    ''                                    End If
    ''                                End If


    ''                            Else

    ''                                Dim tadpLoanByNumber As New BusinessObject.dstLoanTableAdapters.spr_Loan_ByLoanNumber_SelectTableAdapter
    ''                                Dim dtblLoanByNumber As BusinessObject.dstLoan.spr_Loan_ByLoanNumber_SelectDataTable = Nothing
    ''                                dtblLoanByNumber = tadpLoanByNumber.GetData(stcVarLoanInfo.LC_No, dtblFilebyCustomerNo.First.ID)

    ''                                Dim intFileID As Integer = dtblFilebyCustomerNo.First.ID
    ''                                Dim intLoanID As Integer = dtblLoanByNumber.First.ID

    ''                                TbCell = New HtmlTableCell
    ''                                TbCell.InnerHtml = "<a ID='lnkbtnFollowing' href='#'  onclick= btnFollwoing_ClientClick(" & dtblFilebyCustomerNo.First.ID & "," & dtblLoanByNumber.First.ID & "," & stcVarLoanInfo.AmounDefferd & ")>ثبت پیگیری</a>"
    ''                                TbCell.NoWrap = True
    ''                                TbCell.Align = "center"
    ''                                TbRow.Cells.Add(TbCell)


    ''                                Dim lnqDetail = cntxVar.tbl_HandyFollow.Where(Function(x) x.FK_FileID = intFileID And x.FK_LoanID = intLoanID)
    ''                                Dim followingUserName As String = "---"
    ''                                ''********************
    ''                                If lnqDetail.Count > 0 Then
    ''                                    Dim lnqDetailList = lnqDetail.ToList(0)

    ''                                    Dim lnqUser = cntxVar.tbl_User.Where(Function(x) x.ID = lnqDetailList.FK_UserID).ToList(0)
    ''                                    followingUserName = lnqUser.Username
    ''                                End If



    ''                                TbCell = New HtmlTableCell
    ''                                TbCell.InnerHtml = followingUserName
    ''                                TbCell.NoWrap = True
    ''                                TbCell.Align = "center"
    ''                                TbRow.Cells.Add(TbCell)


    ''                            End If

    ''                        Else


    ''                            Session("BranchID") = intBranchID
    ''                            Session("BranchName") = strBranchName
    ''                            Session("CustomerNO") = stcVarLoanInfo.CustomerNo

    ''                            TbCell = New HtmlTableCell
    ''                            TbCell.InnerHtml = "<a ID='lnkbtnFollowing' href='#'  onclick= btnFollwoing_ClientClick(" & -1 & "," & -1 & "," & stcVarLoanInfo.AmounDefferd & ")>ثبت پیگیری</a>"
    ''                            TbCell.NoWrap = True
    ''                            TbCell.Align = "center"
    ''                            TbRow.Cells.Add(TbCell)

    ''                            TbCell = New HtmlTableCell
    ''                            TbCell.InnerHtml = "---"
    ''                            TbCell.NoWrap = True
    ''                            TbCell.Align = "center"
    ''                            TbRow.Cells.Add(TbCell)


    ''                        End If



    ''                    End With



    ''                    divResult.Visible = True
    ''                    tblResult.Rows.Add(TbRow)


    ''                Catch ex As Exception

    ''                    qryErrorLog.spr_ErrorLog_Insert(ex.Message, 3, drwUserLogin.ID.ToString())
    ''                    Continue Do

    ''                End Try


    ''            Loop While dataReader.Read()
    ''            dataReader.Close()

    ''            listOperationLoan.Clear()

    ''        Catch ex As Exception

    ''            Bootstrap_Panel1.ShowMessage(ex.Message, True)

    ''            cnnBI_Connection.Close()

    ''            Return
    ''        End Try

    ''    End Using




    ''End Sub


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


End Class