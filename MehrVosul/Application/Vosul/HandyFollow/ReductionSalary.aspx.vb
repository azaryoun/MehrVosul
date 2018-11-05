Public Class ReductionSalary
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Page.IsPostBack = False Then

            If Not Session("intFileID") Is Nothing AndAlso Not Session("intLoanID") Is Nothing Then

                ''  If Not Request.QueryString("LetterNO") AndAlso Not Request.QueryString("RegisterNO") Is Nothing AndAlso Not Request.QueryString("Branch") Is Nothing Then
                If Not Request.QueryString("Branch") Is Nothing Then


                    ''get File Info
                    Dim tadpFile As New BusinessObject.dstHandyFollowTableAdapters.spr_File_SelectTableAdapter
                    Dim dtblFile As BusinessObject.dstHandyFollow.spr_File_SelectDataTable = Nothing

                    If Not Session("intAudienceFileID") Is Nothing Then
                        dtblFile = tadpFile.GetData(1, CInt(Session("intAudienceFileID")), "")

                    Else
                        dtblFile = tadpFile.GetData(1, CInt(Session("intFileID")), "")

                    End If


                    ''Get Loan Info

                    Dim tadpLoan As New BusinessObject.dstLoanTableAdapters.spr_Loan_SelectTableAdapter
                    Dim dtblLoan As BusinessObject.dstLoan.spr_Loan_SelectDataTable = Nothing

                    dtblLoan = tadpLoan.GetData(2, CInt(Session("intLoanID")), -1, -1)

                    ''get loan type
                    Dim tadpLoanType As New BusinessObject.dstLoanTypeTableAdapters.spr_LoanType_SelectTableAdapter
                    Dim dtblLoanType As BusinessObject.dstLoanType.spr_LoanType_SelectDataTable = Nothing


                    dtblLoanType = tadpLoanType.GetData(dtblLoan.First.FK_LoanTypeID)

                    ''Get Branch Info
                    Dim tadpBranch As New BusinessObject.dstBranchTableAdapters.spr_Branch_SelectTableAdapter
                    Dim dtblBranch As BusinessObject.dstBranch.spr_Branch_SelectDataTable = Nothing

                    dtblBranch = tadpBranch.GetData(CInt(Request.QueryString("Branch")))

                    ''get province
                    Dim tadpProvince As New BusinessObject.dstBranchTableAdapters.spr_Province_Check_SelectTableAdapter
                    Dim dtblProvince As BusinessObject.dstBranch.spr_Province_Check_SelectDataTable = Nothing

                    dtblProvince = tadpProvince.GetData(dtblBranch.First.ID)

                    lblDate.Text = mdlGeneral.GetPersianDate(Date.Now)
                    lblLetterNO.Text = Request.QueryString("LetterNO").ToString()

                    Dim strBranch As String = dtblBranch.First.BranchName
                    lblBranch.Text = strBranch
                    lblBranchName.Text = strBranch

                    Dim strFName As String = If(dtblFile.First.IsFNameNull = False, dtblFile.First.FName, "")
                    Dim strLName As String = If(dtblFile.First.IsLNameNull = False, dtblFile.First.LName, "")

                    Dim strFullName As String = strFName & " " & strLName
                    lblFullName.Text = strFullName

                    If Not Request.QueryString("RSLetterDate") Is Nothing Then
                        lblDSLetterDate.Text = Request.QueryString("RSLetterDate")
                    End If

                    If Not Request.QueryString("RSLetterNo") Is Nothing Then
                        lblDSLetterNO.Text = Request.QueryString("RSLetterNo")
                    End If


                    If Not Request.QueryString("InstallmentAmount") Is Nothing Then
                        Dim strInstallmentAmount As String = Request.QueryString("InstallmentAmount").ToString()
                        lblInstallmentAmount.Text = CDbl(strInstallmentAmount).ToString("N0")
                    End If

                    If Not Request.QueryString("DefferedAmount") Is Nothing Then
                        lblDefferedAmount.Text = CDbl(Request.QueryString("DefferedAmount")).ToString("N0")
                    End If

                    If Not Request.QueryString("Remarks") Is Nothing Then
                        lblDescription.Text = Request.QueryString("Remarks")
                    End If

                    If Not Request.QueryString("Receiver") Is Nothing Then
                        lblCompanyName.Text = Request.QueryString("Receiver")
                    End If

                    If Not Request.QueryString("NotifyType") Is Nothing Then
                        lblNotification.Text = Request.QueryString("NotifyType")
                    End If



                    '  Dim strBranchPhone As String = If(dtblBranch.First.IsTelephoneNull = True, "", dtblBranch.First.Telephone)

                    '   Dim strHTML As String = MakePrintFile(Request.QueryString("LetterNO").ToString(), Request.QueryString("RegisterNO").ToString(), strAdress2, strNationalID2, strMobileNo2, strBranch, strAddress, strLoan, strLoanType, strProvince, strFullName, strPhone, strBranchPhone, True, blnIsSponsor)

                    'divMain2.Visible = True
                    'divMain2.InnerHtml = strHTML


                End If

            Else


                Return


            End If


        End If
    End Sub

End Class