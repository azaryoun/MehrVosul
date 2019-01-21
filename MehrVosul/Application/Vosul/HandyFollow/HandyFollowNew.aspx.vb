Imports System.Data.OracleClient
Public Class HandyFollowNew
    Inherits System.Web.UI.Page
    Private Structure stc_Loan_Info

        Public FullName As String '0
        Public Address As String '1
        Public Telephone As String '2
        Public Fax As String '3
        Public Mobile As String '4
        Public Date_P As String '5
        Public LC_No As String '6
        Public BranchCode As String '7
        Public LoanTypeCode As String '8
        Public CustomerNo As String '9
        Public LoanSerial As Integer '10
        Public LCDate As String '11

        Public LCAmount As Double? '12
        Public LCProfit As Double? ' 13
        Public IstlNum As Integer? '14


        Public LCAmountPaid As Double? '15
        Public IstlPaid As Integer? '16
        Public AmounDefferd As Double? '17

        Public GDeffered As Integer? '18
        Public FirstNoPaidDate As String '19
        Public Status As String '20

        Public NotPiadDurationDay As Integer? '21
        Public LCBalance As Double? '22
        Public G_Mande As Integer? '23

        Public G_MustPay As Integer? '24
        Public Amount_MustPay As Double? '25
        Public FG_MustPayDate As String '26

        Public L_PayDate As String '27
        Public LG_PayDate As String '28

        Public lcupdate As String  '29

        Public NationalID As String  '30
        Public NationalNo As String  '31

        Public Sex As String  '32

    End Structure

    Private Structure stc_Sponsor_Waranty
        Public SponsorID As Integer
        Public WarantyTypeDesc As String
    End Structure


    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load


        Page.Response.Cache.SetCacheability(HttpCacheability.NoCache)

        Bootstrap_Panel1.CanNew = False
        Bootstrap_Panel1.CanSave = False
        Bootstrap_Panel1.CanDelete = False
        Bootstrap_Panel1.CanSearch = False
        Bootstrap_Panel1.CanCancel = False
        Bootstrap_Panel1.CanUp = True
        Bootstrap_Panel1.CanWizard = False
        Bootstrap_Panel1.CanConfirmRequest = False
        Bootstrap_Panel1.CanReject = False
        Bootstrap_Panel1.CanDisplay = False
        Bootstrap_Panel1.CanExcel = False
        Bootstrap_Panel1.Enable_Display_Client_Validate = True


        If Page.IsPostBack = False Then

            ''Get Handy Follow related List
            If Not Session("intFileID") Is Nothing And Not Session("intLoanID") Is Nothing Then

                Bootstrap_PersianDateTimePicker_From.GergorainDateTime = Date.Now
                GetHandyFollowList()

                If Not Session("AmountDeffed") Is Nothing Then
                    lblAmountDefferd.InnerText = CLng(Session("AmountDeffed")).ToString("N0")

                Else
                    lblAmountDefferd.InnerText = "--"

                End If

                If Not Session("AssignType") Is Nothing Then

                    If Session("AssignType") <> 1 Then

                        ViewState("AssignType") = Session("AssignType")
                        btnPrint.Visible = True

                        btnAddToText.Visible = False

                        Select Case Session("AssignType")

                            Case 2
                                cmbNotificationType.SelectedValue = 5
                                divNotice.Visible = True
                                divCheckInfo.Visible = False
                                divCheckInfoDate.Visible = False
                            Case 3
                                cmbNotificationType.SelectedValue = 4
                                divNotice.Visible = True
                                divCheckInfo.Visible = False
                                divCheckInfoDate.Visible = False
                            Case 4
                                cmbNotificationType.SelectedValue = 3
                                divInvitation.Visible = True
                                divCheckInfo.Visible = False
                                divCheckInfoDate.Visible = False
                            Case 5

                                cmbNotificationType.SelectedValue = 6
                                divReductionSalary.Visible = True
                                divCheckInfo.Visible = False
                                divCheckInfoDate.Visible = False

                        End Select


                    Else

                        cmbNotificationType.SelectedValue = 2
                        btnPrint.Visible = False
                        divNotice.Visible = False
                        btnAddToText.Visible = True
                    End If

                End If

                Dim tadpFile As New BusinessObject.dstHandyFollowTableAdapters.spr_File_SelectTableAdapter
                Dim dtblFile As BusinessObject.dstHandyFollow.spr_File_SelectDataTable = Nothing


                If Session("intFileID") = -1 AndAlso Not Session("CustomerNO") Is Nothing Then

                    dtblFile = tadpFile.GetData(3, -1, Session("CustomerNO"))


                    If dtblFile.Rows.Count > 0 Then

                        Session("intFileID") = dtblFile.First.ID

                        Dim strName As String = ""
                        If dtblFile.First.IsFNameNull = False Then
                            strName = dtblFile.First.FName & " "
                        End If

                        If dtblFile.First.IsLNameNull = False Then
                            strName &= dtblFile.First.LName
                        End If

                        lblBorroweName.InnerText = strName

                        If dtblFile.First.IsTelephoneWorkNull = False Then
                            lblBorrowerPhone.InnerText = dtblFile.First.TelephoneWork
                        End If

                        If dtblFile.First.IsTelephoneHomeNull = False Then
                            lblBorrowerHomePhone.InnerText = dtblFile.First.TelephoneHome
                        End If

                        If dtblFile.First.IsMobileNoNull = False Then
                            lblBorrowerMobile.InnerText = dtblFile.First.MobileNo
                        End If

                        If dtblFile.First.IsAddressNull = False Then
                            lblAddress.InnerText = dtblFile.First.Address
                        End If

                        If dtblFile.First.IsMobileNo2Null = False Then
                            txtMobile.Text = dtblFile.First.MobileNo2
                        End If

                        If dtblFile.First.IsAddress2Null = False Then
                            txtAddress.Text = dtblFile.First.Address2
                        End If

                        If dtblFile.First.IsNationalID2Null = False Then

                            txtNationalID.Text = dtblFile.First.NationalID2
                        End If

                        If dtblFile.First.IsAddress3Null = False Then
                            txtAddress2.Text = dtblFile.First.Address3
                        End If

                    Else

                        '' GetCustomerInfo(Session("CustomerNO"))

                        ''Insert to file
                        dtblFile = tadpFile.GetData(1, CInt(Session("intFileID")), "")

                        Dim strName As String = ""
                        If dtblFile.First.IsFNameNull = False Then
                            strName = dtblFile.First.FName & " "
                        End If

                        If dtblFile.First.IsLNameNull = False Then
                            strName &= dtblFile.First.LName
                        End If

                        lblBorroweName.InnerText = strName

                        If dtblFile.First.IsTelephoneWorkNull = False Then
                            lblBorrowerPhone.InnerText = dtblFile.First.TelephoneWork
                        End If

                        If dtblFile.First.IsTelephoneHomeNull = False Then
                            lblBorrowerHomePhone.InnerText = dtblFile.First.TelephoneHome
                        End If

                        If dtblFile.First.IsMobileNoNull = False Then
                            lblBorrowerMobile.InnerText = dtblFile.First.MobileNo
                        End If

                        If dtblFile.First.IsAddressNull = False Then
                            lblAddress.InnerText = dtblFile.First.Address
                        End If

                        If dtblFile.First.IsMobileNo2Null = False Then
                            txtMobile.Text = dtblFile.First.MobileNo2
                        End If

                        If dtblFile.First.IsAddress2Null = False Then
                            txtAddress.Text = dtblFile.First.Address2
                        End If

                        If dtblFile.First.IsNationalID2Null = False Then

                            txtNationalID.Text = dtblFile.First.NationalID2
                        End If

                        If dtblFile.First.IsAddress3Null = False Then
                            txtAddress2.Text = dtblFile.First.Address3
                        End If

                    End If

                    ''get loan no
                    Dim tadpLoan As New BusinessObject.dstLoanTableAdapters.spr_Loan_SelectTableAdapter
                    Dim dtblLoan As BusinessObject.dstLoan.spr_Loan_SelectDataTable = Nothing

                    dtblLoan = tadpLoan.GetData(2, CInt(Session("intLoanID")), -1, -1)
                    If dtblLoan.Count > 0 Then
                        odcSponsor.SelectParameters.Item("FileNO").DefaultValue = Session("CustomerNO")  'CInt(Session("intLoanID"))
                        odcSponsor.SelectParameters.Item("LoanNO").DefaultValue = dtblLoan.First.LoanNumber

                        cmbSponsor.DataBind()


                        lblLCNO.InnerText = dtblLoan.First.LoanNumber
                    End If


                Else

                    'odcSponsor.SelectParameters.Item("LoanID").DefaultValue = CInt(Session("intLoanID"))

                    'cmbSponsor.DataBind()

                    Dim tadpLoan As New BusinessObject.dstLoanTableAdapters.spr_Loan_SelectTableAdapter
                    Dim dtblLoan As BusinessObject.dstLoan.spr_Loan_SelectDataTable = Nothing

                    dtblLoan = tadpLoan.GetData(2, CInt(Session("intLoanID")), -1, -1)
                    If dtblLoan.Count > 0 Then
                        odcSponsor.SelectParameters.Item("FileNO").DefaultValue = Session("CustomerNO")  'CInt(Session("intLoanID"))
                        odcSponsor.SelectParameters.Item("LoanNO").DefaultValue = dtblLoan.First.LoanNumber

                        cmbSponsor.DataBind()

                        lblLCNO.InnerText = dtblLoan.First.LoanNumber
                    End If


                    dtblFile = tadpFile.GetData(1, CInt(Session("intFileID")), "")

                    Dim strName As String = ""
                    If dtblFile.First.IsFNameNull = False Then
                        strName = dtblFile.First.FName & " "
                    End If

                    If dtblFile.First.IsLNameNull = False Then
                        strName &= dtblFile.First.LName
                    End If

                    lblBorroweName.InnerText = strName

                    If dtblFile.First.IsTelephoneWorkNull = False Then
                        lblBorrowerPhone.InnerText = dtblFile.First.TelephoneWork
                    End If

                    If dtblFile.First.IsTelephoneHomeNull = False Then
                        lblBorrowerHomePhone.InnerText = dtblFile.First.TelephoneHome
                    End If

                    If dtblFile.First.IsMobileNoNull = False Then
                        lblBorrowerMobile.InnerText = dtblFile.First.MobileNo
                    End If

                    If dtblFile.First.IsAddressNull = False Then
                        lblAddress.InnerText = dtblFile.First.Address
                    End If

                    If dtblFile.First.IsNationalIDNull = False Then
                        txtNationalID.Text = dtblFile.First.NationalID
                    End If

                    If dtblFile.First.IsMobileNo2Null = False Then
                        txtMobile.Text = dtblFile.First.MobileNo2
                    End If

                    If dtblFile.First.IsAddress2Null = False Then
                        txtAddress.Text = dtblFile.First.Address2
                    End If

                    If dtblFile.First.IsNationalID2Null = False Then

                        txtNationalID.Text = dtblFile.First.NationalID2
                    End If

                    If dtblFile.First.IsAddress3Null = False Then
                        txtAddress2.Text = dtblFile.First.Address3
                    End If

                End If

            Else


                Response.Redirect("HandyFollowSearch.aspx")

            End If


        End If

        If hdnAction.Value.StartsWith("D") = True Then


            Dim intHandFollowID As Integer = CInt(hdnAction.Value.Split(";")(1))
            ''Delete
            Dim qryHandyFollow As New BusinessObject.dstHandyFollowTableAdapters.QueriesTableAdapter
            qryHandyFollow.spr_HandyFollow_Delete(intHandFollowID)

            GetHandyFollowList()


        End If

        txtDefferedAmount.Attributes.Add("onkeypress", "return numbersonly(event, false);")
        txtInstallmentAmount.Attributes.Add("onkeypress", "return numbersonly(event, false);")


        Bootstrap_PersianDateTimePicker_From.ShowTimePicker = True
        Bootstrap_PersianDateTimePicker_TO.ShowTimePicker = False

    End Sub

    Protected Sub btnAddToText_Click(sender As Object, e As EventArgs) Handles btnAddToText.Click

        AddFollow()


    End Sub

    Private Sub Bootstrap_Panel1_Panel_Up_Click(sender As Object, e As System.EventArgs) Handles Bootstrap_Panel1.Panel_Up_Click

        ''If Not Session("From") Is Nothing Then

        ''    Response.Redirect("HandyFollowFileSearch1.aspx?Branch=" & Session("Branch").ToString & "&LoanType=" & Session("LoanType").ToString() & "&Province=" & Session("Province").ToString & "&From=" & Session("From").ToString() & "&To=" & Session("To").ToString())

        ''ElseIf Not Session("customerNO") Is Nothing Then

        ''    Response.Redirect("HandyFollowFileSearch1.aspx?customerNO=" & Session("customerNO").ToString())

        ''Else

        ''    Response.Redirect("HandyFollowFileSearch1.aspx")
        ''End If


        If Not Session("HandyFollowAssign") Is Nothing Then

            Session("intFileID") = Nothing
            Session("intLoanID") = Nothing
            Session("HandyFollowAssign") = Nothing
            Session("AssignType") = Nothing
            Session("AmountDeffed") = Nothing
            Session("customerNO") = Nothing
            Response.Redirect("../Cartable/HandyFollowManagement.aspx")
        Else

            Session("AmountDeffed") = Nothing
            Session("intFileID") = Nothing
            Session("intLoanID") = Nothing

            Response.Redirect("HandyFollowFileSearch1.aspx?customerNO=" & Session("customerNO").ToString())
        End If


    End Sub

    Protected Sub cmbSponsor_DataBound(sender As Object, e As EventArgs) Handles cmbSponsor.DataBound
        Dim li As New ListItem
        li.Text = "---"
        li.Value = -1
        cmbSponsor.Items.Insert(0, li)
    End Sub


    Private Sub GetHandyFollowList()

        Dim intFileID As Integer = CInt(Session("intFileID"))
        Dim intLonaID As Integer = CInt(Session("intLoanID"))

        Dim tadpHandyFollowList As New BusinessObject.dstHandyFollowTableAdapters.spr_HandyFollow_CheckFileLoan_SelectTableAdapter
        Dim dtblHandyFollow As BusinessObject.dstHandyFollow.spr_HandyFollow_CheckFileLoan_SelectDataTable = Nothing

        dtblHandyFollow = tadpHandyFollowList.GetData(intLonaID, intFileID)

        Dim intCount As Integer = 0

        For Each drwHandyFollow As BusinessObject.dstHandyFollow.spr_HandyFollow_CheckFileLoan_SelectRow In dtblHandyFollow.Rows

            tblResult.Visible = True
            intCount += 1
            Dim TbRow As New HtmlTableRow
            Dim TbCell As HtmlTableCell


            TbCell = New HtmlTableCell
            TbCell.InnerHtml = CStr(intCount)
            TbCell.Align = "center"
            TbCell.NoWrap = True
            TbRow.Cells.Add(TbCell)


            TbCell = New HtmlTableCell
            TbCell.InnerHtml = mdlGeneral.GetPersianDateTime(drwHandyFollow.ContactDate)
            TbCell.Attributes.Add("dir", "ltr")
            TbCell.NoWrap = True
            TbCell.Align = "center"
            TbRow.Cells.Add(TbCell)

            TbCell = New HtmlTableCell
            TbCell.InnerHtml = drwHandyFollow.NotificationType
            TbCell.NoWrap = True
            TbCell.Align = "center"
            TbRow.Cells.Add(TbCell)


            TbCell = New HtmlTableCell
            TbCell.InnerHtml = drwHandyFollow.FileName
            TbCell.NoWrap = True
            TbCell.Align = "center"
            TbRow.Cells.Add(TbCell)


            TbCell = New HtmlTableCell
            TbCell.InnerHtml = If(drwHandyFollow.IsSuccess = True, "باپاسخ", "بدون پاسخ")
            TbCell.NoWrap = True
            TbCell.Align = "center"
            TbRow.Cells.Add(TbCell)


            TbCell = New HtmlTableCell
            TbCell.InnerHtml = If(drwHandyFollow.Answered = True, "مثبت", "منفی")
            TbCell.NoWrap = True
            TbCell.Align = "center"
            TbRow.Cells.Add(TbCell)


            TbCell = New HtmlTableCell
            Dim strCheckDesc As String = ""
            If drwHandyFollow.IsCheckDateNull = False AndAlso drwHandyFollow.IsCheckDateNull = False Then
                strCheckDesc = drwHandyFollow.CheckNO & "(" & mdlGeneral.GetPersianDate(drwHandyFollow.CheckDate) & ")"
            End If
            TbCell.Attributes.Add("dir", "ltr")
            TbCell.InnerHtml = strCheckDesc
            TbCell.NoWrap = False
            TbCell.Align = "center"
            TbRow.Cells.Add(TbCell)

            TbCell = New HtmlTableCell
            Dim strReturnCheckDesc As String = ""
            Dim strRetDate As String = ""
            Dim strLegalDate As String = ""

            If drwHandyFollow.IsCheckReturend = True Then

                If drwHandyFollow.IsCheckReturnDateNull = False Then
                    strRetDate = mdlGeneral.GetPersianDate(drwHandyFollow.CheckReturnDate)
                End If

                If drwHandyFollow.IsCheckLegalDateNull = False Then
                    strLegalDate = mdlGeneral.GetPersianDate(drwHandyFollow.CheckLegalDate)
                End If

                strReturnCheckDesc = "تاریخ برگشت:" & strRetDate & "-" & "تاریخ ارجاء به حقوقی" & strLegalDate
            End If
            TbCell.Attributes.Add("dir", "ltr")
            TbCell.InnerHtml = strReturnCheckDesc
            TbCell.NoWrap = False
            TbCell.Align = "center"
            TbRow.Cells.Add(TbCell)


            TbCell = New HtmlTableCell
            TbCell.InnerHtml = drwHandyFollow.Remarks
            TbCell.NoWrap = False
            TbCell.Align = "center"
            TbRow.Cells.Add(TbCell)


            ''TbCell = New HtmlTableCell
            ''TbCell.InnerHtml = "<a ID='lnkbtnHandyDelete' href='#'  onclick= lnkbtnHandyDelete_ClientClick(" & drwHandyFollow.ID & ")>حذف</a>"
            ''TbCell.NoWrap = False
            ''TbCell.Align = "center"
            ''TbRow.Cells.Add(TbCell)

            tblResult.Rows.Add(TbRow)




        Next


    End Sub

    Protected Sub cmbSponsor_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbSponsor.SelectedIndexChanged


        If cmbSponsor.SelectedValue <> -1 Then

            Dim tadpFile As New BusinessObject.dstFileTableAdapters.spr_File_SelectTableAdapter
            Dim dtblFile As BusinessObject.dstFile.spr_File_SelectDataTable = Nothing

            dtblFile = tadpFile.GetData(3, -1, cmbSponsor.SelectedValue)

            If dtblFile.Rows.Count > 0 Then

                If dtblFile.First.IsTelephoneHomeNull = False Then
                    lblSponsorPhone.InnerText = dtblFile.First.TelephoneHome
                End If

                If dtblFile.First.IsTelephoneWorkNull = False Then
                    lblSponsorPhoneWork.InnerText = dtblFile.First.TelephoneWork
                End If

                If dtblFile.First.IsMobileNoNull = False Then
                    lblSponsorMobile.InnerText = dtblFile.First.MobileNo
                End If


                If dtblFile.First.IsAddressNull = False Then
                    lblSponsorAddress.InnerText = dtblFile.First.Address
                End If
            Else

                ''get sponsor info
                Dim tadpSponsor As New BusinessObject.dstSponsor_ListTableAdapters.spr_SponsorList2_SelectByFileTableAdapter
                Dim dtblSponsor As BusinessObject.dstSponsor_List.spr_SponsorList2_SelectByFileDataTable = Nothing

                dtblSponsor = tadpSponsor.GetData(cmbSponsor.SelectedValue)


                lblSponsorPhone.InnerText = dtblSponsor.First.TelephoneHome

                lblSponsorPhoneWork.InnerText = dtblSponsor.First.TelephoneWork

                lblSponsorMobile.InnerText = dtblSponsor.First.MobileNo

                lblSponsorAddress.InnerText = dtblSponsor.First.Address

                ''Insert to File
                Dim qryFile As New BusinessObject.dstFileTableAdapters.QueriesTableAdapter

                Dim intFileID As Integer = qryFile.spr_File_Insert(cmbSponsor.SelectedValue, "", dtblSponsor.First.FullName, dtblSponsor.First.FatherName, dtblSponsor.First.MobileNo, "", "", "", dtblSponsor.First.Address, dtblSponsor.First.TelephoneHome, dtblSponsor.First.TelephoneWork, False, 1, Nothing, Nothing, Nothing)


                ''Insert sponsorLoan
                Dim qrySponsorLoan As New BusinessObject.dstLoanSponsorTableAdapters.QueriesTableAdapter
                qrySponsorLoan.spr_LoanSponsor_Insert(CInt(Session("intLoanID")), intFileID, dtblSponsor.First.WarantyTypeDesc)


            End If



        Else
            lblSponsorPhone.InnerText = ""
            lblSponsorPhoneWork.InnerText = ""
            lblSponsorMobile.InnerText = ""

        End If


    End Sub



    Private Sub AddFollow()

        Try

            Dim qryHnadyFollow As New BusinessObject.dstHandyFollowTableAdapters.QueriesTableAdapter


            Dim dtblUserLogin As BusinessObject.dstUser.spr_User_Login_SelectDataTable = CType(Session("dtblUserLogin"), BusinessObject.dstUser.spr_User_Login_SelectDataTable)
            Dim drwUserLogin As BusinessObject.dstUser.spr_User_Login_SelectRow = dtblUserLogin.Rows(0)

            Dim intFileID As Integer = CInt(Session("intFileID"))
            Dim intLonaID As Integer = CInt(Session("intLoanID"))
            Dim intNotificationTypeID As Integer = CInt(cmbNotificationType.SelectedValue)
            Dim blnToSponsor As Boolean = If(rdboToSponsor.SelectedValue = 1, True, False)
            Dim intAudienceFileID As Integer = CInt(Session("intFileID"))


            If blnToSponsor = True Then
                If cmbSponsor.SelectedValue <> -1 Then

                    ''get file ID
                    Dim tadpFile As New BusinessObject.dstFileTableAdapters.spr_File_SelectTableAdapter
                    Dim dtblFile As BusinessObject.dstFile.spr_File_SelectDataTable = Nothing

                    dtblFile = tadpFile.GetData(3, -1, cmbSponsor.SelectedValue)

                    If dtblFile.Rows.Count > 0 Then
                        intAudienceFileID = dtblFile.First.ID
                    End If

                    Session("intAudienceFileID") = intAudienceFileID

                Else

                    ''
                    Bootstrap_Panel1.ShowMessage("ضامن را مشخص نمایید", True)
                    Return

                End If
            End If

            Dim intHandFollowAssignID? As Integer = Nothing
            Dim blnUpdateHandyFollowAssign As Boolean = False
            If Not Session("HandyFollowAssign") Is Nothing Then

                intHandFollowAssignID = CInt(Session("HandyFollowAssign"))
                blnUpdateHandyFollowAssign = True


            Else
                ''check Handy Follow Assign
                Dim tadpHandyFollowAssignByUser As New BusinessObject.dstHandyFollowTableAdapters.spr_HandyFollowAssignByUser_SelectTableAdapter
                Dim dtblHandyFollowAssignByUser As BusinessObject.dstHandyFollow.spr_HandyFollowAssignByUser_SelectDataTable = Nothing

                dtblHandyFollowAssignByUser = tadpHandyFollowAssignByUser.GetData(drwUserLogin.ID, CInt(Session("intLoanID")))
                If dtblHandyFollowAssignByUser.Rows.Count > 0 Then

                    intHandFollowAssignID = dtblHandyFollowAssignByUser.First.ID
                    blnUpdateHandyFollowAssign = True
                End If

            End If

            Dim blnAnswered As Boolean = If(rdbListAnswered.SelectedValue = 1, True, False)
            Dim blnIsSuccess As Boolean = If(rdbListNotificationStatus.SelectedValue = 1, True, False)
            Dim strRemarks As String = txtRemark.Text
            Dim dtFromDate As DateTime = Bootstrap_PersianDateTimePicker_From.GergorainDateTime
            Dim dteDutyDate As Date? = Nothing
            If Bootstrap_PersianDateTimePicker_TO.PersianDateTime <> "" Then
                dteDutyDate = Bootstrap_PersianDateTimePicker_TO.GergorainDateTime
            End If

            Dim blnHasCheck As Boolean = If(txtCheckNO.Text.Trim <> "", True, False)
            Dim strCheckNO As String = txtCheckNO.Text.Trim
            Dim dteCheckDate As Date? = Nothing
            If Bootstrap_PersianDateTimePickerCheckDate.PersianDateTime <> "" Then
                dteCheckDate = Bootstrap_PersianDateTimePickerCheckDate.GergorainDateTime
            End If
            Dim dteChekDateDuty As Date? = Nothing
            If Bootstrap_PersianDateTimePickerChekDateDuty.PersianDateTime <> "" Then
                dteChekDateDuty = Bootstrap_PersianDateTimePickerChekDateDuty.GergorainDateTime
            End If
            Dim strCheckDesk As String = txtAccountNO.Text

            Dim blnCheckBack As Boolean = chkbxCheckBack.Checked
            Dim dteCheckReturnDate As Date? = Nothing
            Dim dteCheckLegalDate As Date? = Nothing



            If chkbxCheckBack.Checked = True Then

                If Bootstrap_PersianDateTimeCheckReturnDate.PersianDateTime <> "" Then
                    dteCheckReturnDate = Bootstrap_PersianDateTimeCheckReturnDate.GergorainDateTime
                End If

                If Bootstrap_PersianDateTimeCheckLegalDate.PersianDateTime <> "" Then
                    dteCheckLegalDate = Bootstrap_PersianDateTimeCheckLegalDate.GergorainDateTime
                End If

            End If

            qryHnadyFollow.spr_HandyFollow_Insert(intFileID, intLonaID, intNotificationTypeID, blnToSponsor, intAudienceFileID, dtFromDate, drwUserLogin.ID, Date.Now, strRemarks, blnAnswered, blnIsSuccess, dteDutyDate, intHandFollowAssignID, blnHasCheck, strCheckNO, dteCheckDate, strCheckDesk, dteChekDateDuty, dteCheckReturnDate, dteCheckLegalDate, blnCheckBack)

            ''If Not ViewState("AssignType") Is Nothing Then

            ''    If ViewState("AssignType") <> 1 Then

            Dim qryFile As New BusinessObject.dstHandyFollowTableAdapters.QueriesTableAdapter

            Dim strMoileNO As String = txtMobile.Text.Trim()
            Dim strAddress As String = txtAddress.Text
            Dim strNationalID As String = txtNationalID.Text.Trim()
            Dim strAddress2 As String = txtAddress2.Text
            qryFile.spr_FileInfo_Update(intFileID, strMoileNO, strAddress, strNationalID, drwUserLogin.ID, strAddress2)

            ''    End If

            ''End If

            Bootstrap_Panel1.ShowMessage("ثبت پیگیری با موفقیت انجام شد", False)

            GetHandyFollowList()

            rdbListAnswered.SelectedValue = 1
            rdbListNotificationStatus.SelectedValue = 1

            txtRemark.Text = ""

            If blnUpdateHandyFollowAssign = True Then

                Dim qryHandyFollowAssign As New BusinessObject.dstHandyFollowTableAdapters.QueriesTableAdapter
                qryHandyFollowAssign.spr_HandyFollowAssignStatus_Update(intHandFollowAssignID)

            End If

            lblMessage.Text = "ثبت پیگیری با موفقیت انجام شد، لطفا جهت مشاهده آن صفحه را refresh نمایید"
        Catch ex As Exception

            Bootstrap_Panel1.ShowMessage("در ثبت پیگیری خطا رخ داده است", True)

            Return

        End Try


    End Sub

    Protected Sub btnPrint_Click(sender As Object, e As EventArgs) Handles btnPrint.Click


        Try

            AddFollow()

        Catch ex As Exception


            Bootstrap_Panel1.ShowMessage("در ثبت پیگیری خطا رخ داده است", True)

            Return


        End Try




        Dim dtblUserLogin As BusinessObject.dstUser.spr_User_Login_SelectDataTable = CType(Session("dtblUserLogin"), BusinessObject.dstUser.spr_User_Login_SelectDataTable)
        Dim drwUserLogin As BusinessObject.dstUser.spr_User_Login_SelectRow = dtblUserLogin.Rows(0)


        If cmbNotificationType.SelectedValue = 3 Then

            Response.Redirect("InvitationPreview.aspx?LetterNO=" & txtLetterNO.Text.Trim() & "&RegisterNO=" & txtRegisterNO.Text.Trim & "&Branch=" & drwUserLogin.FK_BrnachID & "&InvitationDate=" & txtInvitationDate.Text.Trim() & "&InvitationTime=" & txtInvitationTime.Text.Trim())

        ElseIf cmbNotificationType.SelectedValue = 4 Then
            ''اظهارنامه
            Response.Redirect("NoticePreview.aspx?LetterNO=" & txtLetterNO.Text.Trim() & "&RegisterNO=" & txtRegisterNO.Text.Trim & "&Branch=" & drwUserLogin.FK_BrnachID & "&CompanyNational=" & txtCompanyNationalID.Text.Trim() & "&Sponsor=" & cmbSponsor.SelectedValue)

        ElseIf cmbNotificationType.SelectedValue = 5 Then

            ''اخطاریه
            Response.Redirect("ManifestPreview.aspx?LetterNO=" & txtLetterNO.Text.Trim() & "&RegisterNO=" & txtRegisterNO.Text.Trim & "&Branch=" & drwUserLogin.FK_BrnachID & "&ManifestType=" & rdboToSponsor.SelectedValue)

        ElseIf cmbNotificationType.SelectedValue = 6 Then

            Dim strNotifyType = ""
            If rdboToSponsor.SelectedValue = 1 Then

                strNotifyType = cmbSponsor.SelectedItem.Text '"ضامن"

            Else
                strNotifyType = cmbSponsor.SelectedItem.Text '"وام گیرنده"
            End If


            Response.Redirect("ReductionSalary.aspx?LetterNO=" & txtLetterNO.Text.Trim() & "&RegisterNO=" & txtRegisterNO.Text.Trim & "&Branch=" & drwUserLogin.FK_BrnachID & "&RSLetterNo=" & txtRSLetterNo.Text.Trim() & "&RSLetterDate=" & txtRSLetterDate.Text.Trim() & "&InstallmentAmount=" & txtInstallmentAmount.Text.Trim() & "&DefferedAmount=" & txtDefferedAmount.Text.Trim() & "&Remarks=" & txtRSRemarks.Text & "&Receiver=" & txtReceiver.Text & "&NotifyType=" & strNotifyType)


        End If


    End Sub

    Protected Sub cmbNotificationType_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbNotificationType.SelectedIndexChanged

        If cmbNotificationType.SelectedValue = 3 Then
            divReductionSalary.Visible = False
            divInvitation.Visible = True
            divNotice.Visible = False
            btnPrint.Visible = True
            divCheckInfo.Visible = False
            divCheckInfoDate.Visible = False
            btnAddToText.Visible = False
            btnPrint.Visible = True

        ElseIf cmbNotificationType.SelectedValue = 6 Then
            divReductionSalary.Visible = True
            divInvitation.Visible = False
            divNotice.Visible = False
            btnPrint.Visible = True
            divCheckInfo.Visible = False
            divCheckInfoDate.Visible = False
            btnAddToText.Visible = False
            btnPrint.Visible = True

        ElseIf cmbNotificationType.SelectedValue = 4 OrElse cmbNotificationType.SelectedValue = 5 Then

            divNotice.Visible = True
            divReductionSalary.Visible = False
            divInvitation.Visible = False
            btnPrint.Visible = True
            divCheckInfo.Visible = False
            divCheckInfoDate.Visible = False
            btnAddToText.Visible = False
            btnPrint.Visible = True
        Else
            divReductionSalary.Visible = False
            divInvitation.Visible = False
            divNotice.Visible = False
            divCheckInfo.Visible = True
            divCheckInfoDate.Visible = True
            btnAddToText.Visible = True
            btnPrint.Visible = False
        End If


    End Sub



    'Private Sub GetCustomerInfo(ByVal strCustomerNO As String)


    '    Dim cnnBuiler_BI As New OracleConnectionStringBuilder()
    '    cnnBuiler_BI.DataSource = "10.35.1.37:1522/bidb"
    '    cnnBuiler_BI.UserID = "deposit"
    '    cnnBuiler_BI.Password = "deposit"
    '    cnnBuiler_BI.Unicode = True




    '    Dim dteThisDate As Date = Date.Now.AddDays(-1)
    '    Dim strThisDatePersian As String = mdlGeneral.GetPersianDate(dteThisDate).Replace("/", "")

    '    Using cnnBI_Connection As New OracleConnection(cnnBuiler_BI.ConnectionString)

    '        Dim cmd_BI As OracleCommand = cnnBI_Connection.CreateCommand()

    '        Dim strLoan_Info_Query As String = "SELECT * from loan_info where Date_P='" & strThisDatePersian & "' and state in ('3','4','5','F')  and ( CFCIFNO ='" & strCustomerNO & "')"

    '        cmd_BI.CommandText = strLoan_Info_Query

    '        Try
    '            cnnBI_Connection.Open()
    '        Catch ex As Exception

    '            Bootstrap_Panel1.ShowMessage(ex.Message, True)
    '            Return

    '        End Try
    '        Dim dataReader As OracleDataReader = Nothing

    '        Try
    '            dataReader = cmd_BI.ExecuteReader()

    '            If dataReader.Read = False Then

    '                Bootstrap_Panel1.ShowMessage(" اطلاعات مربوط  بروز رسانی نشده است. لطفا با مدیر سیستم تماس بگیرید ", True)

    '                dataReader.Close()
    '                cnnBI_Connection.Close()

    '                Return
    '            End If

    '            Dim i As Integer
    '            Dim intCount As Integer = 0

    '            Dim stcVarLoanInfo As stc_Loan_Info
    '            Dim dtblUserLogin As BusinessObject.dstUser.spr_User_Login_SelectDataTable = CType(Session("dtblUserLogin"), BusinessObject.dstUser.spr_User_Login_SelectDataTable)
    '            Dim drwUserLogin As BusinessObject.dstUser.spr_User_Login_SelectRow = dtblUserLogin.Rows(0)



    '            Do
    '                i += 1
    '                Dim TbRow As HtmlTableRow


    '                Try

    '                    If dataReader.GetValue(0) Is DBNull.Value Then
    '                        stcVarLoanInfo.FullName = ""
    '                    Else
    '                        stcVarLoanInfo.FullName = CStr(dataReader.GetValue(0)).Replace("'", "")
    '                    End If

    '                    If dataReader.GetValue(1) Is DBNull.Value Then
    '                        stcVarLoanInfo.Address = ""
    '                    Else
    '                        stcVarLoanInfo.Address = CStr(dataReader.GetValue(1)).Replace("'", "")
    '                    End If

    '                    If dataReader.GetValue(2) Is DBNull.Value Then
    '                        stcVarLoanInfo.Telephone = ""
    '                    Else
    '                        stcVarLoanInfo.Telephone = CStr(dataReader.GetValue(2)).Trim.Replace("'", "")
    '                    End If



    '                    If dataReader.GetValue(4) Is DBNull.Value Then
    '                        stcVarLoanInfo.Mobile = ""
    '                    Else
    '                        stcVarLoanInfo.Mobile = CStr(dataReader.GetValue(4)).Trim.Replace("'", "")
    '                    End If

    '                    ''stcVarLoanInfo.Date_P = CStr(dataReader.GetValue(5))



    '                    If dataReader.GetValue(6) Is DBNull.Value Then
    '                        i -= 1
    '                        Continue Do
    '                    Else
    '                        stcVarLoanInfo.LC_No = CStr(dataReader.GetValue(6)).Trim.Replace("'", "")
    '                    End If


    '                    If dataReader.GetValue(7) Is DBNull.Value Then
    '                        i -= 1
    '                        Continue Do
    '                    Else
    '                        stcVarLoanInfo.BranchCode = CStr(dataReader.GetValue(7)).Trim
    '                    End If


    '                    If dataReader.GetValue(8) Is DBNull.Value Then
    '                        i -= 1
    '                        Continue Do
    '                    Else
    '                        stcVarLoanInfo.LoanTypeCode = CStr(dataReader.GetValue(8)).Trim
    '                    End If

    '                    If dataReader.GetValue(9) Is DBNull.Value Then
    '                        i -= 1
    '                        Continue Do
    '                    Else
    '                        stcVarLoanInfo.CustomerNo = CStr(dataReader.GetValue(9)).Trim.Replace("'", "")
    '                    End If


    '                    If dataReader.GetValue(10) Is DBNull.Value Then
    '                        i -= 1
    '                        Continue Do
    '                    Else
    '                        stcVarLoanInfo.LoanSerial = CInt(dataReader.GetValue(10))
    '                    End If

    '                    If dataReader.GetValue(11) Is DBNull.Value Then
    '                        stcVarLoanInfo.LCDate = ""
    '                    Else
    '                        stcVarLoanInfo.LCDate = CStr(dataReader.GetValue(11)).Trim
    '                    End If

    '                    If dataReader.GetValue(12) Is DBNull.Value Then
    '                        stcVarLoanInfo.LCAmount = Nothing
    '                    Else
    '                        stcVarLoanInfo.LCAmount = CDbl(dataReader.GetValue(12))
    '                    End If

    '                    If dataReader.GetValue(13) Is DBNull.Value Then
    '                        stcVarLoanInfo.LCProfit = Nothing
    '                    Else
    '                        stcVarLoanInfo.LCProfit = CDbl(dataReader.GetValue(13))
    '                    End If

    '                    If dataReader.GetValue(14) Is DBNull.Value Then
    '                        stcVarLoanInfo.IstlNum = Nothing
    '                    Else
    '                        stcVarLoanInfo.IstlNum = CInt(dataReader.GetValue(14))
    '                    End If


    '                    If dataReader.GetValue(15) Is DBNull.Value Then
    '                        stcVarLoanInfo.LCAmountPaid = Nothing
    '                    Else
    '                        stcVarLoanInfo.LCAmountPaid = CDbl(dataReader.GetValue(15))
    '                    End If

    '                    If dataReader.GetValue(16) Is DBNull.Value Then
    '                        stcVarLoanInfo.IstlPaid = Nothing
    '                    Else
    '                        stcVarLoanInfo.IstlPaid = CInt(dataReader.GetValue(16))
    '                    End If

    '                    If dataReader.GetValue(17) Is DBNull.Value Then
    '                        stcVarLoanInfo.AmounDefferd = Nothing
    '                    Else
    '                        stcVarLoanInfo.AmounDefferd = CDbl(dataReader.GetValue(17))
    '                    End If

    '                    If dataReader.GetValue(18) Is DBNull.Value Then
    '                        stcVarLoanInfo.GDeffered = Nothing
    '                    Else
    '                        stcVarLoanInfo.GDeffered = CInt(dataReader.GetValue(18))
    '                    End If


    '                    stcVarLoanInfo.Status = CStr(dataReader.GetValue(20)).Trim


    '                    If dataReader.GetValue(21) Is DBNull.Value Then
    '                        i -= 1
    '                        Continue Do
    '                    Else
    '                        stcVarLoanInfo.NotPiadDurationDay = CInt(dataReader.GetValue(21))
    '                    End If


    '                    If dataReader.GetValue(22) Is DBNull.Value Then
    '                        i -= 1
    '                        Continue Do
    '                    Else
    '                        stcVarLoanInfo.LCBalance = CDbl(dataReader.GetValue(22))
    '                    End If


    '                    If dataReader.GetValue(25) Is DBNull.Value Then
    '                        stcVarLoanInfo.Amount_MustPay = Nothing
    '                    Else
    '                        stcVarLoanInfo.Amount_MustPay = CDbl(dataReader.GetValue(25))
    '                    End If


    '                    If dataReader.GetValue(30) Is DBNull.Value Then
    '                        stcVarLoanInfo.NationalID = ""
    '                    Else
    '                        stcVarLoanInfo.NationalID = CStr(dataReader.GetValue(30)).Replace("'", "")
    '                    End If


    '                    If dataReader.GetValue(31) Is DBNull.Value Then
    '                        stcVarLoanInfo.NationalNo = ""
    '                    Else
    '                        stcVarLoanInfo.NationalNo = CStr(dataReader.GetValue(31)).Replace("'", "")
    '                    End If


    '                    If dataReader.GetValue(32) Is DBNull.Value Then
    '                        stcVarLoanInfo.Sex = ""
    '                    Else
    '                        stcVarLoanInfo.Sex = CStr(dataReader.GetValue(32)).Trim.Replace("'", "")
    '                    End If




    '                    Dim tadpFilebyCustomerNo As New BusinessObject.dstFileTableAdapters.spr_File_CustomerNo_SelectTableAdapter
    '                    Dim dtblFilebyCustomerNo As BusinessObject.dstFile.spr_File_CustomerNo_SelectDataTable = Nothing
    '                    dtblFilebyCustomerNo = tadpFilebyCustomerNo.GetData(stcVarLoanInfo.CustomerNo)
    '                    Dim intBorrowerFileID As Integer = -1


    '                    If dtblFilebyCustomerNo.Rows.Count = 0 Then

    '                        Dim qryFile As New BusinessObject.dstFileTableAdapters.QueriesTableAdapter

    '                        Dim arrFullName() As String = stcVarLoanInfo.FullName.Split("*")
    '                        Dim strFatherName As String = arrFullName(0)
    '                        Dim strFName As String = arrFullName(1)
    '                        Dim strLName As String = arrFullName(2)


    '                        Dim blnIsMale As Boolean = If(stcVarLoanInfo.Sex = "زن", False, True)
    '                        intBorrowerFileID = qryFile.spr_File_Insert(stcVarLoanInfo.CustomerNo, strFName, strLName, strFatherName, stcVarLoanInfo.Mobile, stcVarLoanInfo.NationalID, stcVarLoanInfo.NationalNo, "", stcVarLoanInfo.Address, stcVarLoanInfo.Telephone, stcVarLoanInfo.Telephone, blnIsMale, 2, 1, Nothing, Nothing)

    '                    Else

    '                        Dim drwFilebyCustomerNo As BusinessObject.dstFile.spr_File_CustomerNo_SelectRow = dtblFilebyCustomerNo.Rows(0)
    '                        intBorrowerFileID = drwFilebyCustomerNo.ID

    '                    End If


    '                    Dim tadpLoanByNumber As New BusinessObject.dstLoanTableAdapters.spr_Loan_ByLoanNumber_SelectTableAdapter
    '                    Dim dtblLoanByNumber As BusinessObject.dstLoan.spr_Loan_ByLoanNumber_SelectDataTable = Nothing
    '                    dtblLoanByNumber = tadpLoanByNumber.GetData(stcVarLoanInfo.LC_No, intBorrowerFileID)


    '                    Dim intLoanID As Integer = -1

    '                    If dtblLoanByNumber.Rows.Count = 0 Then
    '                        Dim qryLoan As New BusinessObject.dstLoanTableAdapters.QueriesTableAdapter


    '                        Dim dteLoanDate? As Date = Nothing
    '                        Try
    '                            stcVarLoanInfo.LCDate = stcVarLoanInfo.LCDate.Insert(4, "/")
    '                            stcVarLoanInfo.LCDate = stcVarLoanInfo.LCDate.Insert(7, "/")
    '                            dteLoanDate = mdlGeneral.GetGregorianDate(stcVarLoanInfo.LCDate)
    '                        Catch ex As Exception
    '                            dteLoanDate = Nothing
    '                        End Try

    '                        Dim tadpBranchbyCode As New BusinessObject.dstBranchTableAdapters.spr_Branch_ByCode_SelectTableAdapter
    '                        Dim dtblBranchbyCode As BusinessObject.dstBranch.spr_Branch_ByCode_SelectDataTable = Nothing
    '                        dtblBranchbyCode = tadpBranchbyCode.GetData(stcVarLoanInfo.BranchCode)

    '                        Dim intBranchID As Integer = CInt(Session("BranchID"))


    '                        Dim tadpLoanTypeByCode As New BusinessObject.dstLoanTypeTableAdapters.spr_LoanType_byCode_SelectTableAdapter
    '                        Dim dtblLoanTypeByCode As BusinessObject.dstLoanType.spr_LoanType_byCode_SelectDataTable = Nothing
    '                        dtblLoanTypeByCode = tadpLoanTypeByCode.GetData(stcVarLoanInfo.LoanTypeCode)

    '                        Dim intLoanTypeID As Integer = -1

    '                        If dtblLoanTypeByCode.Rows.Count = 0 Then

    '                            Dim strLoanTypeName As String = GetLoanTypeName(stcVarLoanInfo.LoanTypeCode)
    '                            Dim qryLoanType As New BusinessObject.dstLoanTypeTableAdapters.QueriesTableAdapter
    '                            intLoanTypeID = qryLoanType.spr_LoanType_Insert(stcVarLoanInfo.LoanTypeCode, strLoanTypeName, 2, "")


    '                        Else
    '                            Dim drwLoanTypeByCode As BusinessObject.dstLoanType.spr_LoanType_byCode_SelectRow = dtblLoanTypeByCode.Rows(0)
    '                            intLoanTypeID = drwLoanTypeByCode.ID


    '                        End If


    '                        intLoanID = qryLoan.spr_Loan_Insert(intBorrowerFileID, intLoanTypeID, intBranchID, dteLoanDate, stcVarLoanInfo.LC_No, stcVarLoanInfo.LoanSerial, Date.Now, stcVarLoanInfo.LCAmount, stcVarLoanInfo.IstlNum)


    '                        Dim arrintFileSponsors() As stc_Sponsor_Waranty = GetSponsorsList(stcVarLoanInfo.BranchCode, stcVarLoanInfo.LoanTypeCode, stcVarLoanInfo.CustomerNo, stcVarLoanInfo.LoanSerial)

    '                        If arrintFileSponsors IsNot Nothing Then
    '                            For k As Integer = 0 To arrintFileSponsors.Length - 1


    '                                Dim qryLoanSponsor As New BusinessObject.dstLoanSponsorTableAdapters.QueriesTableAdapter
    '                                Dim intLoanSponsorID As Integer = qryLoanSponsor.spr_LoanSponsor_Insert(intLoanID, arrintFileSponsors(k).SponsorID, arrintFileSponsors(k).WarantyTypeDesc)

    '                            Next k
    '                        End If



    '                    Else


    '                        Dim drwLoanByNumber As BusinessObject.dstLoan.spr_Loan_ByLoanNumber_SelectRow = dtblLoanByNumber.Rows(0)
    '                        intLoanID = drwLoanByNumber.ID


    '                        'Update Sponsors

    '                        Dim arrintFileSponsors() As stc_Sponsor_Waranty = GetSponsorsList(stcVarLoanInfo.BranchCode, stcVarLoanInfo.LoanTypeCode, stcVarLoanInfo.CustomerNo, stcVarLoanInfo.LoanSerial)



    '                        If arrintFileSponsors IsNot Nothing Then
    '                            For k As Integer = 0 To arrintFileSponsors.Length - 1


    '                                Dim tadpLoanSponsorCheck As New BusinessObject.dstLoanSponsorTableAdapters.spr_LoanSponsor_Check_SelectTableAdapter
    '                                Dim dtblLoanSponsorCheck As BusinessObject.dstLoanSponsor.spr_LoanSponsor_Check_SelectDataTable = Nothing
    '                                dtblLoanSponsorCheck = tadpLoanSponsorCheck.GetData(intLoanID, arrintFileSponsors(k).SponsorID)


    '                                If dtblFilebyCustomerNo.Rows.Count = 0 Then

    '                                    Dim qryLoanSponsor As New BusinessObject.dstLoanSponsorTableAdapters.QueriesTableAdapter
    '                                    Dim intLoanSponsorID As Integer = qryLoanSponsor.spr_LoanSponsor_Insert(intLoanID, arrintFileSponsors(k).SponsorID, arrintFileSponsors(k).WarantyTypeDesc)


    '                                End If

    '                            Next k
    '                        End If




    '                    End If

    '                    Session("intFileID") = intBorrowerFileID
    '                    Session("intLoanID") = intLoanID

    '                Catch ex As Exception
    '                    Continue Do
    '                End Try



    '            Loop While dataReader.Read()
    '            dataReader.Close()


    '        Catch ex As Exception

    '            Bootstrap_Panel1.ShowMessage(ex.Message, True)

    '            cnnBI_Connection.Close()

    '            Return
    '        End Try

    '    End Using

    'End Sub

    'Private Function GetLoanTypeName(ByVal strLoanTypeCode As String) As String
    '    Try
    '        Dim strLoanTypeDesc As String = strLoanTypeCode

    '        Dim cnnBuiler_BI As New OracleConnectionStringBuilder()
    '        cnnBuiler_BI.DataSource = "10.35.1.37:1522/bidb"
    '        cnnBuiler_BI.UserID = "deposit"
    '        cnnBuiler_BI.Password = "deposit"
    '        cnnBuiler_BI.Unicode = True

    '        Using cnnBI_Connection As New OracleConnection(cnnBuiler_BI.ConnectionString)

    '            Dim cmd_BI As OracleCommand = cnnBI_Connection.CreateCommand()
    '            Dim strLoanType As String = "SELECT * from lfloantyp where LNMINORTP='" & strLoanTypeCode & "'"
    '            cmd_BI.CommandText = strLoanType

    '            Try
    '                cnnBI_Connection.Open()
    '            Catch ex As Exception
    '                Return strLoanTypeDesc
    '            End Try

    '            Dim dataReader As OracleDataReader = Nothing
    '            dataReader = cmd_BI.ExecuteReader()

    '            If dataReader.Read = False Then
    '                dataReader.Close()
    '                cnnBI_Connection.Close()
    '                Return strLoanTypeDesc
    '            End If


    '            If dataReader.GetValue(3) Is DBNull.Value Then
    '                strLoanTypeDesc = strLoanTypeCode
    '            Else
    '                strLoanTypeDesc = CStr(dataReader.GetValue(3)).Trim.Replace("'", "")
    '            End If



    '            dataReader.Close()
    '            cnnBI_Connection.Close()

    '        End Using

    '        Return strLoanTypeDesc

    '    Catch ex As Exception
    '        Return strLoanTypeCode
    '    End Try

    'End Function

    'Private Function GetSponsorsList(ByVal strBranchCode As String, ByVal strLoanTypeCode As String, ByVal strCustomerNo As String, ByVal intLoanSerial As Integer) As stc_Sponsor_Waranty()


    '    Dim obj_stc_Sponsor_Waranty() As stc_Sponsor_Waranty = Nothing

    '    Dim tadpSponsorList As New BusinessObject.dstSponsor_ListTableAdapters.spr_Sponsors_List_ByLoanNumber_SelectTableAdapter
    '    Dim dtblSponsorList As BusinessObject.dstSponsor_List.spr_Sponsors_List_ByLoanNumber_SelectDataTable = Nothing
    '    dtblSponsorList = tadpSponsorList.GetData(strBranchCode, strLoanTypeCode, strCustomerNo, intLoanSerial)

    '    If dtblSponsorList.Rows.Count = 0 Then
    '        Return Nothing
    '    End If

    '    For Each drwSponsorList As BusinessObject.dstSponsor_List.spr_Sponsors_List_ByLoanNumber_SelectRow In dtblSponsorList.Rows


    '        Dim tadpFilebyCustomerNo As New BusinessObject.dstFileTableAdapters.spr_File_CustomerNo_SelectTableAdapter
    '        Dim dtblFilebyCustomerNo As BusinessObject.dstFile.spr_File_CustomerNo_SelectDataTable = Nothing
    '        dtblFilebyCustomerNo = tadpFilebyCustomerNo.GetData(drwSponsorList.SponsorCustomerNo)
    '        Dim intSponsorID As Integer = -1


    '        If dtblFilebyCustomerNo.Rows.Count = 0 Then

    '            Dim qryFile As New BusinessObject.dstFileTableAdapters.QueriesTableAdapter

    '            Dim strFName As String = ""
    '            Dim strLName As String = drwSponsorList.FullName
    '            Dim strFatherName As String = drwSponsorList.FatherName

    '            Dim blnIsMale As Boolean = True

    '            intSponsorID = qryFile.spr_File_Insert(drwSponsorList.SponsorCustomerNo, strFName, strLName, strFatherName, drwSponsorList.MobileNo, drwSponsorList.NationalID, drwSponsorList.IDNumber, "", drwSponsorList.Address, drwSponsorList.TelephoneHome, drwSponsorList.TelephoneWork, blnIsMale, 2, 1, Nothing, Nothing)


    '        Else

    '            Dim drwFilebyCustomerNo As BusinessObject.dstFile.spr_File_CustomerNo_SelectRow = dtblFilebyCustomerNo.Rows(0)
    '            intSponsorID = drwFilebyCustomerNo.ID


    '        End If


    '        If obj_stc_Sponsor_Waranty Is Nothing Then
    '            ReDim obj_stc_Sponsor_Waranty(0)
    '        Else
    '            ReDim Preserve obj_stc_Sponsor_Waranty(obj_stc_Sponsor_Waranty.Length)
    '        End If

    '        obj_stc_Sponsor_Waranty(obj_stc_Sponsor_Waranty.Length - 1).SponsorID = intSponsorID
    '        obj_stc_Sponsor_Waranty(obj_stc_Sponsor_Waranty.Length - 1).WarantyTypeDesc = drwSponsorList.WarantyTypeDesc


    '    Next drwSponsorList

    '    '' Dim lnq = obj_stc_Sponsor_Waranty.GroupBy(Function(x) New With {.Key1 = x.SponsorID})


    '    Return obj_stc_Sponsor_Waranty


    'End Function
End Class