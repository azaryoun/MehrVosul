Public Class NoticePreview
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Page.IsPostBack = False Then


            If Not Session("intFileID") Is Nothing AndAlso Not Session("intLoanID") Is Nothing Then

                ''If Not Request.QueryString("LetterNO") AndAlso Not Request.QueryString("RegisterNO") Is Nothing AndAlso Not Request.QueryString("Branch") Is Nothing Then

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
                    lblCompanyNationalID.Text = Request.QueryString("CompanyNational").ToString()
                    lblBranch.Text = dtblBranch.First.BranchName
                    lblAddress.Text = If(dtblBranch.First.IsBranchAddressNull = True, "", dtblBranch.First.BranchAddress)

                    Dim strFName As String = If(dtblFile.First.IsFNameNull = False, dtblFile.First.FName, "")
                    Dim strLName As String = If(dtblFile.First.IsLNameNull = False, dtblFile.First.LName, "")
                    lblFullName.Text = strFName & " " & strLName
                    lblFullName0.Text = strFName & " " & strLName
                    lblFatherName.Text = If(dtblFile.First.IsFatherNameNull = False, dtblFile.First.FatherName, "")


                    lblAddress2.Text = If(dtblFile.First.IsAddress2Null = False AndAlso dtblFile.First.Address2 <> "", dtblFile.First.Address2, dtblFile.First.Address)

                    lblNationalID.Text = If(dtblFile.First.IsNationalID2Null = False AndAlso dtblFile.First.NationalID2 <> "", dtblFile.First.NationalID2, dtblFile.First.NationalID)

                    lblMobileNO.Text = If(dtblFile.First.IsMobileNo2Null = False AndAlso dtblFile.First.MobileNo2 <> "", dtblFile.First.MobileNo2, dtblFile.First.MobileNo)

                    ''  lblPovince.Text = dtblProvince.First.ProvinceName

                    Dim strNewLoan As String() = dtblLoan.First.LoanNumber.Split("-")
                    Dim strLoanNew As String = ""
                    If strNewLoan.Length = 4 Then
                        strLoanNew &= strNewLoan(3) & "-" & strNewLoan(2) & "-" & strNewLoan(1) & "-" & strNewLoan(0)

                    Else
                        strLoanNew = dtblLoan.First.LoanNumber
                    End If
                    lblLoan.Text = strLoanNew
                    lblLoanType.Text = dtblLoanType.First.LoanTypeName

                End If

            Else


                Return


            End If

        End If

    End Sub


    Private Sub MakePrintFile(ByVal strLetter As String)

        Dim strHTMLMain As String = ""


        strHTMLMain += " <table style='border: 2px solid #000000; height: 100%; width: 100%; padding: 0; border-spacing: 0;'>" _
           & " <tr><td style='border-bottom-style: solid; border-bottom-width: 2px; border-bottom-color: #000000'>" _
           & "  <table style='height: 100%; width: 100%; font-family: 'B Nazanin'; text-align: right; padding: 0; border-spacing: 0;'>" _
           & " <tr> <td style='width: 150px;'> <table> <tr dir='rtl'> <td>" _
           & "<asp:Label ID='lblDate' runat='server'>" & mdlGeneral.GetPersianDate(Date.Now).ToString() & "</asp:Label></td><td>تاریخ</td></tr>" _
           & "<tr><td <asp:Label ID='lblLetterNO' runat='server'>" & strLetter & "</asp:Label></td><td>شماره</td></tr></table></td>" _
           & "<td style='text-align: center;' dir='rtl'><span style='font-family: ' B Nazanin''>بسمه تعالی</span><br/><br/><span style='font-family: 'B Nazanin'; font-size: small;'>&nbsp;</span></td>" _
           & "<td style='text-align: center;' dir='rtl'><asp:Image ID='Image1' runat='server' CssClass='auto-style7' ImageUrl='~/Images/System/MehrLogoPrint.jpg' Height='52px' Width='92px'/></td>  </tr> </table> </td>  </tr>" _
           & "<tr>   <td style='text-align: center'><span style='font-family: Titr; font-size: larger;'>برگ اظهارنامه</span></td></tr>" _
           & "<tr> <td style='padding-left: 8px'><table style='align-content center; width: 99%; border-style: solid; border-width: 2px; border-color: black; padding: 0; border-spacing: 0'>" _
           & "<tr><td style='border-right: 2px solid #000000; border-bottom: 2px solid #000000; text-align: center; font-family 'B Nazanin'; font-size: medium; font-weight: bold; ' class='auto-style6' >مشخصات و اقامتگاه مخاطب</td>"




    End Sub

End Class