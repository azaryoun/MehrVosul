Public Class ManifestPreview
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load


        If Page.IsPostBack = False Then


            If Not Session("intFileID") Is Nothing AndAlso Not Session("intLoanID") Is Nothing Then

                ''  If Not Request.QueryString("LetterNO") AndAlso Not Request.QueryString("RegisterNO") Is Nothing AndAlso Not Request.QueryString("Branch") Is Nothing Then
                If Not Request.QueryString("Branch") Is Nothing Then


                    ''get File Info
                    Dim tadpFile As New BusinessObject.dstHandyFollowTableAdapters.spr_File_SelectTableAdapter
                    Dim dtblFile As BusinessObject.dstHandyFollow.spr_File_SelectDataTable = Nothing

                    dtblFile = tadpFile.GetData(1, CInt(Session("intFileID")), "")


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
                    lblRegisterNO.Text = Request.QueryString("RegisterNO").ToString()
                    lblBranch.Text = dtblBranch.First.BranchName
                    lblAddress.Text = If(dtblBranch.First.IsBranchAddressNull = True, "", dtblBranch.First.BranchAddress)

                    Dim strFName As String = If(dtblFile.First.IsFNameNull = False, dtblFile.First.FName, "")
                    Dim strLName As String = If(dtblFile.First.IsLNameNull = False, dtblFile.First.LName, "")
                    lblFullName.Text = strFName & " " & strLName

                    lblAddress2.Text = If(dtblFile.First.IsAddress2Null = False AndAlso dtblFile.First.Address2 <> "", dtblFile.First.Address2, dtblFile.First.Address)

                    lblNationalID.Text = If(dtblFile.First.IsNationalID2Null = False AndAlso dtblFile.First.NationalID2 <> "", dtblFile.First.NationalID2, dtblFile.First.NationalID)

                    lblMobileNO.Text = If(dtblFile.First.IsMobileNo2Null = False AndAlso dtblFile.First.MobileNo2 <> "", dtblFile.First.MobileNo2, dtblFile.First.MobileNo)

                    lblProvince.Text = dtblProvince.First.ProvinceName
                    lblLoan.Text = dtblLoan.First.LoanNumber
                    lblLoanType.Text = dtblLoanType.First.LoanTypeName

                End If

            Else


                Return


            End If

        End If


    End Sub

End Class