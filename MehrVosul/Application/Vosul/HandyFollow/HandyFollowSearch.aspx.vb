Imports System.Data.OracleClient
Imports System.Data.Entity

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


            If Not Request.QueryString("Installment") Is Nothing Then

                txt_InstallmentCount.Text = Request.QueryString("Installment")

                txtCustomerNO.Text = Request.QueryString("CustomerNo")

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


                DisplayFileList()

            End If


        End If

        If hdnAction.Value.StartsWith("S") = True Then
            Dim intFileID As Integer = CInt(hdnAction.Value.Split(";")(1))
            Dim intLoanID As Integer = CInt(hdnAction.Value.Split(";")(2))
            Session("AmountDeffed") = hdnAction.Value.Split(";")(3)
            Session("intFileID") = CObj(intFileID)
            Session("intLoanID") = CObj(intLoanID)

            Session("Installment") = CObj(txt_InstallmentCount.Text)
            Session("Province") = CObj(cmbProvince.SelectedValue)
            Session("Branch") = CObj(cmbBranch.SelectedValue)
            Session("LoanType") = CObj(cmbLoanType.SelectedValue)
            Session("customerNO") = CObj(txtCustomerNO.Text)
            Response.Redirect("HandyFollowNew.aspx")
        End If

    End Sub

    Private Sub Bootstrap_Panel1_Panel_Display_Click(sender As Object, e As System.EventArgs) Handles Bootstrap_Panel1.Panel_Display_Click

        DisplayFileList()

    End Sub

    Private Sub DisplayFileList()


        Dim dtblUserLogin As BusinessObject.dstUser.spr_User_Login_SelectDataTable = CType(Session("dtblUserLogin"), BusinessObject.dstUser.spr_User_Login_SelectDataTable)
        Dim drwUserLogin As BusinessObject.dstUser.spr_User_Login_SelectRow = dtblUserLogin.Rows(0)

        Dim tadpReport As New BusinessObject.dstReportTableAdapters.spr_Report_CurrentLCStatus_MaxLoanType_SelectTableAdapter
        Dim dtblReport As BusinessObject.dstReport.spr_Report_CurrentLCStatus_MaxLoanType_SelectDataTable = Nothing


        If txtCustomerNO.Text.Trim() = "" Then

            Dim intInstallmentCount As Integer = CInt(txt_InstallmentCount.Text)

            If cmbBranch.SelectedValue = -1 And cmbLoanType.SelectedValue = -1 And cmbProvince.SelectedValue = -1 Then

                dtblReport = tadpReport.GetData(1, -1, -1, intInstallmentCount, -1, -1)

            ElseIf cmbProvince.SelectedValue = -1 And cmbBranch.SelectedValue <> -1 And cmbLoanType.SelectedValue = -1 Then

                Dim intBranch As Integer = CInt(cmbBranch.SelectedValue)
                dtblReport = tadpReport.GetData(2, intBranch, -1, intInstallmentCount, -1, -1)

            ElseIf cmbProvince.SelectedValue = -1 And cmbBranch.SelectedValue = -1 And cmbLoanType.SelectedValue <> -1 Then

                Dim intLoanTypeID As Integer = CInt(cmbLoanType.SelectedValue)
                dtblReport = tadpReport.GetData(3, -1, intLoanTypeID, intInstallmentCount, -1, -1)

            ElseIf cmbProvince.SelectedValue = -1 And cmbBranch.SelectedValue <> -1 And cmbLoanType.SelectedValue <> -1 Then

                Dim intLoanTypeID As Integer = CInt(cmbLoanType.SelectedValue)
                Dim intBranch As Integer = CInt(cmbBranch.SelectedValue)
                dtblReport = tadpReport.GetData(4, intBranch, intLoanTypeID, intInstallmentCount, -1, -1)

            ElseIf cmbProvince.SelectedValue <> -1 And cmbBranch.SelectedValue = -1 And cmbLoanType.SelectedValue = -1 Then

                Dim intProvince As Integer = CInt(cmbProvince.SelectedValue)
                dtblReport = tadpReport.GetData(5, -1, -1, intInstallmentCount, intProvince, -1)

            ElseIf cmbProvince.SelectedValue <> -1 And cmbBranch.SelectedValue = -1 And cmbLoanType.SelectedValue <> -1 Then

                Dim intProvince As Integer = CInt(cmbProvince.SelectedValue)
                Dim intLoanTypeID As Integer = CInt(cmbLoanType.SelectedValue)
                dtblReport = tadpReport.GetData(6, -1, intLoanTypeID, intInstallmentCount, intProvince, -1)

            ElseIf cmbProvince.SelectedValue <> -1 And cmbBranch.SelectedValue <> -1 And cmbLoanType.SelectedValue <> -1 Then

                Dim intBranch As Integer = CInt(cmbBranch.SelectedValue)
                Dim intProvince As Integer = CInt(cmbProvince.SelectedValue)
                Dim intLoanTypeID As Integer = CInt(cmbLoanType.SelectedValue)
                dtblReport = tadpReport.GetData(7, intBranch, intLoanTypeID, intInstallmentCount, intProvince, -1)

            ElseIf cmbProvince.SelectedValue <> -1 And cmbBranch.SelectedValue <> -1 And cmbLoanType.SelectedValue = -1 Then

                Dim intBranch As Integer = CInt(cmbBranch.SelectedValue)
                Dim intProvince As Integer = CInt(cmbProvince.SelectedValue)
                Dim intLoanTypeID As Integer = CInt(cmbLoanType.SelectedValue)
                dtblReport = tadpReport.GetData(8, intBranch, intLoanTypeID, intInstallmentCount, intProvince, -1)

            End If

        Else


            dtblReport = tadpReport.GetData(9, -1, -1, -1, -1, txtCustomerNO.Text.Trim())

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
                TbCell.NoWrap = False
                TbCell.Width = "100px"
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
                TbCell.Width = "50px"
                TbCell.Align = "center"
                TbRow.Cells.Add(TbCell)

                TbCell = New HtmlTableCell
                TbCell.InnerHtml = drwReport.Branch
                TbCell.NoWrap = False
                TbCell.Width = "120px"
                TbCell.Align = "center"
                TbRow.Cells.Add(TbCell)


                Dim cntxVar As New BusinessObject.dbMehrVosulEntities1



                If drwUserLogin.IsDataUserAdmin = False Then


                    Dim intLogCount = cntxVar.tbl_HandyFollow.Where(Function(x) x.FK_FileID = drwReport.FileID And x.FK_LoanID = drwReport.LoanID).Count()
                    If intLogCount = 0 Then



                        TbCell = New HtmlTableCell
                        TbCell.InnerHtml = "<a ID='lnkbtnFollowing' href='#'  onclick= btnFollwoing_ClientClick(" & drwReport.FileID & "," & drwReport.LoanID & "," & drwReport.AmounDefferd & ")>ثبت پیگیری</a>"
                        TbCell.NoWrap = True
                        TbCell.Align = "center"
                        TbRow.Cells.Add(TbCell)

                        TbCell = New HtmlTableCell
                        TbCell.InnerHtml = "---"
                        TbCell.NoWrap = True
                        TbCell.Align = "center"
                        TbRow.Cells.Add(TbCell)

                    Else

                        Dim intLogCountByUser = cntxVar.tbl_HandyFollow.Where(Function(x) x.FK_FileID = drwReport.FileID And x.FK_LoanID = drwReport.LoanID And x.FK_UserID = drwUserLogin.ID).Count()

                        If intLogCountByUser = 0 Then

                            TbCell = New HtmlTableCell
                            TbCell.InnerHtml = "ثبت پیگیری"
                            TbCell.NoWrap = False
                            TbCell.Align = "center"
                            TbRow.Cells.Add(TbCell)

                            Dim lnqDetail = cntxVar.tbl_HandyFollow.Where(Function(x) x.FK_FileID = drwReport.FileID And x.FK_LoanID = drwReport.LoanID)
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
                            TbCell.InnerHtml = "<a ID='lnkbtnFollowing' href='#'  onclick= btnFollwoing_ClientClick(" & drwReport.FileID & "," & drwReport.LoanID & drwReport.AmounDefferd & ")>ثبت پیگیری</a>"
                            TbCell.NoWrap = True
                            TbCell.Align = "center"
                            TbRow.Cells.Add(TbCell)

                            Dim lnqDetail = cntxVar.tbl_HandyFollow.Where(Function(x) x.FK_FileID = drwReport.FileID And x.FK_LoanID = drwReport.LoanID)
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


                    TbCell = New HtmlTableCell
                    TbCell.InnerHtml = "<a ID='lnkbtnFollowing' href='#'  onclick= btnFollwoing_ClientClick(" & drwReport.FileID & "," & drwReport.LoanID & "," & drwReport.AmounDefferd & ")>ثبت پیگیری</a>"
                    TbCell.NoWrap = True
                    TbCell.Align = "center"
                    TbRow.Cells.Add(TbCell)


                    Dim lnqDetail = cntxVar.tbl_HandyFollow.Where(Function(x) x.FK_FileID = drwReport.FileID And x.FK_LoanID = drwReport.LoanID)
                    Dim followingUserName As String = "---"
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

    Protected Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click



        ''Dim cnnBuiler_BI As New OracleConnectionStringBuilder()
        ''cnnBuiler_BI.DataSource = "10.35.1.37:1522/bidb"
        ''cnnBuiler_BI.UserID = "deposit"
        ''cnnBuiler_BI.Password = "deposit"
        ''cnnBuiler_BI.Unicode = True

        ''Dim dteThisDate As Date = Date.Now.AddDays(-1)
        ''Dim strThisDatePersian As String = mdlGeneral.GetPersianDate(dteThisDate).Replace("/", "")

        ''Using cnnBI_Connection As New OracleConnection(cnnBuiler_BI.ConnectionString)

        ''    Dim cmd_BI As OracleCommand = cnnBI_Connection.CreateCommand()

        ''    Dim strLoan_Info_Query As String = "SELECT * from loan_info where Date_P='" & strThisDatePersian & "' and state in ('3','4','5','F') and (" & strIntervalText & ") and ("

        ''    Dim strBranchQuery As String = ""
        ''    Dim ctxMehr As New BusinessObject.dbMehrVosulEntities1

        ''    Dim lnqBranchCode = ctxMehr.tbl_Branch.Where(Function(x) x.ID = cmbBranch.SelectedValue).First

        ''    strLoan_Info_Query &= " ABRNCHCOD='" & lnqBranchCode.BrnachCode & "' ) and "

        ''    If cmbLoanType.SelectedValue <> -1 Then

        ''        strLoan_Info_Query &= " ABRNCHCOD='" & lnqBranchCode.BrnachCode & "' ) and "

        ''    End If

        ''    Dim strFromDay As String = ""
        ''    Dim strToDay As String = ""

        ''    strLoan_Info_Query &= " and (NPDURATION between " & strFromDay & " and " & strToDay & " )"



        ''    cmd_BI.CommandText = strLoan_Info_Query

        ''    Try
        ''        cnnBI_Connection.Open()
        ''    Catch ex As Exception

        ''        ''   qryLogHeader.spr_LogCurrentLCStatus_H_Insert(dteThisDate, Date.Now, False, ex.Message, intCurrentTryTime)
        ''        Return
        ''    End Try
        ''    Dim dataReader As OracleDataReader = Nothing

        ''    Try
        ''        dataReader = cmd_BI.ExecuteReader()
        ''    Catch ex As Exception

        ''        ''    qryLogHeader.spr_LogCurrentLCStatus_H_Insert(dteThisDate, Date.Now, False, ex.Message, intCurrentTryTime)
        ''        cnnBI_Connection.Close()

        ''        Return
        ''    End Try

        ''End Using





    End Sub
End Class