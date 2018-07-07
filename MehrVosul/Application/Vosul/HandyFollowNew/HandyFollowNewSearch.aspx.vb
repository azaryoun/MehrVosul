Imports System.Data.OracleClient
Imports System.Data.Entity

Public Class HandyFollowNewSearch
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
            cmbLoanType.DataValueField = "LoanTypeCode1"
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
                cmbBranch.DataValueField = "BrnachCode1"
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
                cmbBranch.DataValueField = "BrnachCode1"

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
                cmbBranch.DataValueField = "BrnachCode1"
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


                DisplayFileList()

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

        DisplayFileList()

    End Sub

    Private Sub DisplayFileList()


        ''Dim dtblUserLogin As BusinessObject.dstUser.spr_User_Login_SelectDataTable = CType(Session("dtblUserLogin"), BusinessObject.dstUser.spr_User_Login_SelectDataTable)
        ''Dim drwUserLogin As BusinessObject.dstUser.spr_User_Login_SelectRow = dtblUserLogin.Rows(0)

        ''Dim tadpReport As New BusinessObject.dstReportTableAdapters.spr_Report_CurrentLCStatus_MaxLoanType_SelectTableAdapter
        ''Dim dtblReport As BusinessObject.dstReport.spr_Report_CurrentLCStatus_MaxLoanType_SelectDataTable = Nothing

        ''Dim intFrom As Integer = txtFrom.Text.Trim()
        ''Dim intTo As Integer = txtTo.Text.Trim()


        ''Dim cntxVar As New BusinessObject.dbMehrVosulEntities1
        ''Dim lnqHandyFollow

        ''If cmbBranch.SelectedValue <> -1 AndAlso cmbLoanType.SelectedValue = -1 Then

        ''    lnqHandyFollow = cntxVar.tbl_TotalDeffredLC.Where(Function(x) x.NotPiadDurationDay >= intFrom AndAlso x.NotPiadDurationDay <= intTo AndAlso x.BranchCode = cmbBranch.SelectedValue)

        ''End If

        ''If cmbBranch.SelectedValue = -1 AndAlso cmbLoanType.SelectedValue <> -1 Then

        ''    lnqHandyFollow = cntxVar.tbl_TotalDeffredLC.Where(Function(x) x.NotPiadDurationDay >= intFrom AndAlso x.NotPiadDurationDay <= intTo AndAlso x.LoanTypeCode = cmbLoanType.SelectedValue)

        ''End If

        ''If cmbBranch.SelectedValue <> -1 AndAlso cmbLoanType.SelectedValue <> -1 Then

        ''    lnqHandyFollow = cntxVar.tbl_TotalDeffredLC.Where(Function(x) x.NotPiadDurationDay >= intFrom AndAlso x.NotPiadDurationDay <= intTo AndAlso x.LoanTypeCode = cmbLoanType.SelectedValue AndAlso x.BranchCode = cmbBranch.SelectedValue)

        ''End If

        ''Dim lnqHandyFollowGroupList = lnqHandyFollow.ToList()


        ''Dim intCount As Integer = 0
        ''For Each lnqHandyFollowGroupListItem In lnqHandyFollowGroupList


        ''    divResult.Visible = True
        ''    Dim strchklstMenuLeaves As String = ""

        ''    intCount += 1
        ''    Dim TbRow As New HtmlTableRow
        ''    Dim TbCell As HtmlTableCell

        ''    TbCell = New HtmlTableCell
        ''    TbCell.InnerHtml = CStr(intCount)
        ''    TbCell.Align = "center"
        ''    TbCell.NoWrap = True
        ''    TbRow.Cells.Add(TbCell)

        ''    TbCell = New HtmlTableCell
        ''    TbCell.InnerHtml = lnqHandyFollowGroupListItem.CustomerNo
        ''    TbCell.NoWrap = True
        ''    TbCell.Align = "center"
        ''    TbRow.Cells.Add(TbCell)


        ''    TbCell = New HtmlTableCell
        ''    TbCell.InnerHtml = lnqHandyFollowGroupListItem.FullName
        ''    TbCell.NoWrap = False
        ''    TbCell.Width = "100px"
        ''    TbCell.Align = "center"
        ''    TbRow.Cells.Add(TbCell)


        ''    TbCell = New HtmlTableCell
        ''    TbCell.InnerHtml = lnqHandyFollowGroupListItem.LCNumber
        ''    TbCell.NoWrap = True
        ''    TbCell.Align = "center"
        ''    TbRow.Cells.Add(TbCell)

        ''    TbCell = New HtmlTableCell
        ''    TbCell.InnerHtml = lnqHandyFollowGroupListItem.MobileNo
        ''    TbCell.NoWrap = True
        ''    TbCell.Align = "center"
        ''    TbRow.Cells.Add(TbCell)

        ''    TbCell = New HtmlTableCell
        ''    TbCell.InnerHtml = lnqHandyFollowGroupListItem.NotPiadDurationDay
        ''    TbCell.NoWrap = True
        ''    TbCell.Width = "50px"
        ''    TbCell.Align = "center"
        ''    TbRow.Cells.Add(TbCell)

        ''    TbCell = New HtmlTableCell
        ''    TbCell.InnerHtml = lnqHandyFollowGroupListItem.BranchCode
        ''    TbCell.NoWrap = False
        ''    TbCell.Width = "120px"
        ''    TbCell.Align = "center"
        ''    TbRow.Cells.Add(TbCell)


        ''    Dim strCustomerNO As String = lnqHandyFollowGroupListItem.CustomerNO
        ''    Dim strLoanNO As String = lnqHandyFollowGroupListItem.LCNumber

        ''    Dim intFileID As Integer = -1
        ''    Dim intLoanID As Integer = -1

        ''    Dim lnqFile = cntxVar.tbl_File.Where(Function(x) x.CustomerNo = strCustomerNO).Count()

        ''    If lnqFile > 0 Then

        ''        Dim lnqFile1 = cntxVar.tbl_File.Where(Function(x) x.CustomerNo = strCustomerNO).First()
        ''        intFileID = lnqFile1.ID

        ''        Dim lnqLoan = cntxVar.tbl_Loan.Where(Function(x) x.LoanNumber = strLoanNO).Count()
        ''        If lnqLoan > 0 Then

        ''            Dim lnqLoan1 = cntxVar.tbl_Loan.Where(Function(x) x.LoanNumber = strLoanNO).First()
        ''            intLoanID = lnqLoan1.ID

        ''        Else

        ''            ''get from bi

        ''        End If




        ''    Else
        ''        ''get From BI


        ''    End If




        ''    If drwUserLogin.IsDataUserAdmin = False Then


        ''        Dim intLogCount = cntxVar.tbl_HandyFollow.Where(Function(x) x.FK_FileID = intFileID And x.FK_LoanID = intLoanID).Count()
        ''        If intLogCount = 0 Then

        ''            TbCell = New HtmlTableCell
        ''            TbCell.InnerHtml = "<a ID='lnkbtnFollowing' href='#'  onclick= btnFollwoing_ClientClick(" & intFileID & "," & intLoanID & "," & lnqHandyFollowGroupListItem.AmounDefferd & ")>ثبت پیگیری</a>"
        ''            TbCell.NoWrap = True

        ''            TbCell.Align = "center"
        ''            TbRow.Cells.Add(TbCell)

        ''            TbCell = New HtmlTableCell
        ''            TbCell.InnerHtml = "---"
        ''            TbCell.NoWrap = True
        ''            TbCell.Align = "center"
        ''            TbRow.Cells.Add(TbCell)

        ''        Else

        ''            Dim intLogCountByUser = cntxVar.tbl_HandyFollow.Where(Function(x) x.FK_FileID = intFileID And x.FK_LoanID = intLoanID And x.FK_UserID = drwUserLogin.ID).Count()


        ''            If intLogCountByUser = 0 Then

        ''                TbCell = New HtmlTableCell
        ''                TbCell.InnerHtml = "ثبت پیگیری"
        ''                TbCell.NoWrap = False
        ''                TbCell.Align = "center"
        ''                TbRow.Cells.Add(TbCell)

        ''                Dim lnqDetail = cntxVar.tbl_HandyFollow.Where(Function(x) x.FK_FileID = intFileID And x.FK_LoanID = intLoanID)
        ''                If lnqDetail.Count > 0 Then
        ''                    Dim lnqDetailList = lnqDetail.ToList(0)
        ''                    Dim lnqUser = cntxVar.tbl_User.Where(Function(x) x.ID = lnqDetailList.FK_UserID).ToList(0)

        ''                    TbCell = New HtmlTableCell
        ''                    TbCell.InnerHtml = lnqUser.Username
        ''                    TbCell.NoWrap = True
        ''                    TbCell.Align = "center"
        ''                    TbRow.Cells.Add(TbCell)

        ''                End If
        ''            Else

        ''                TbCell = New HtmlTableCell

        ''                TbCell.InnerHtml = "<a ID='lnkbtnFollowing' href='#'  onclick= btnFollwoing_ClientClick(" & intFileID & "," & intLoanID & lnqHandyFollowGroupListItem.AmounDefferd & ")>ثبت پیگیری</a>"

        ''                TbCell.NoWrap = True

        ''                TbCell.Align = "center"
        ''                TbRow.Cells.Add(TbCell)

        ''                Dim lnqDetail = cntxVar.tbl_HandyFollow.Where(Function(x) x.FK_FileID = intFileID And x.FK_LoanID = intLoanID)
        ''                If lnqDetail.Count > 0 Then
        ''                    Dim lnqDetailList = lnqDetail.ToList(0)
        ''                    Dim lnqUser = cntxVar.tbl_User.Where(Function(x) x.ID = lnqDetailList.FK_UserID).ToList(0)

        ''                    TbCell = New HtmlTableCell
        ''                    TbCell.InnerHtml = lnqUser.Username
        ''                    TbCell.NoWrap = True
        ''                    TbCell.Align = "center"
        ''                    TbRow.Cells.Add(TbCell)

        ''                End If

        ''            End If



        ''        End If
        ''    Else


        ''        TbCell = New HtmlTableCell
        ''        TbCell.InnerHtml = "<a ID='lnkbtnFollowing' href='#'  onclick= btnFollwoing_ClientClick(" & intFileID & "," & intLoanID & "," & lnqHandyFollowGroupListItem.AmounDefferd & ")>ثبت پیگیری</a>"
        ''        TbCell.NoWrap = True
        ''            TbCell.Align = "center"
        ''            TbRow.Cells.Add(TbCell)


        ''        Dim lnqDetail = cntxVar.tbl_HandyFollow.Where(Function(x) x.FK_FileID = intFileID And x.FK_LoanID = intLoanID)
        ''        Dim followingUserName As String = "---"
        ''            If lnqDetail.Count > 0 Then
        ''                Dim lnqDetailList = lnqDetail.ToList(0)

        ''                Dim lnqUser = cntxVar.tbl_User.Where(Function(x) x.ID = lnqDetailList.FK_UserID).ToList(0)
        ''                followingUserName = lnqUser.Username
        ''            End If


        ''            TbCell = New HtmlTableCell
        ''            TbCell.InnerHtml = followingUserName
        ''            TbCell.NoWrap = True
        ''            TbCell.Align = "center"
        ''            TbRow.Cells.Add(TbCell)


        ''        End If


        ''    tblResult.Rows.Add(TbRow)


        ''Next




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
        cmbBranch.DataValueField = "BrnachCode1"


        cmbBranch.DataBind()


    End Sub







End Class