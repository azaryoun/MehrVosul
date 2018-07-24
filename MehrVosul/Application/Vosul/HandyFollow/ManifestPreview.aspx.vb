Public Class ManifestPreview
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load


        If Page.IsPostBack = False Then

            If Not Request.QueryString("STRHTML") Is Nothing Then



                Dim strHTMLMain As String = ""
                Dim strLoanCustomer As String()


                strLoanCustomer = Request.QueryString("STRHTML").ToString().Split(",")
                For j As Integer = 0 To strLoanCustomer.Length - 2

                    ''get File ID
                    Dim tadpFile As New BusinessObject.dstHandyFollowTableAdapters.spr_File_SelectTableAdapter
                    Dim dtblFile As BusinessObject.dstHandyFollow.spr_File_SelectDataTable = Nothing

                    Dim strLCNO As String = strLoanCustomer(j).Split(";")(1)
                    Dim strCustomerNO As String = strLoanCustomer(j).Split(";")(0)
                    dtblFile = tadpFile.GetData(3, -1, strCustomerNO)
                    Dim intFileID As Integer = dtblFile.First.ID

                    Dim strFName As String = If(dtblFile.First.IsFNameNull = False, dtblFile.First.FName, "")
                    Dim strLName As String = If(dtblFile.First.IsLNameNull = False, dtblFile.First.LName, "")

                    Dim strFullName As String = strFName & " " & strLName

                    Dim tadpLoan As New BusinessObject.dstLoanTableAdapters.spr_Loan_ByLoanNumber_SelectTableAdapter
                    Dim dtblLoan As BusinessObject.dstLoan.spr_Loan_ByLoanNumber_SelectDataTable = Nothing

                    dtblLoan = tadpLoan.GetData(strLCNO, intFileID)

                    ''get loan type
                    Dim tadpLoanType As New BusinessObject.dstLoanTypeTableAdapters.spr_LoanType_SelectTableAdapter
                    Dim dtblLoanType As BusinessObject.dstLoanType.spr_LoanType_SelectDataTable = Nothing

                    dtblLoanType = tadpLoanType.GetData(dtblLoan.First.FK_LoanTypeID)
                    Dim strLoanType As String = dtblLoanType.First.LoanTypeName

                    Dim intLoanID As Integer = dtblLoan.First.ID

                    ''Get Branch Info
                    Dim tadpBranch As New BusinessObject.dstBranchTableAdapters.spr_Branch_SelectTableAdapter
                    Dim dtblBranch As BusinessObject.dstBranch.spr_Branch_SelectDataTable = Nothing

                    dtblBranch = tadpBranch.GetData(dtblLoan.First.FK_BranchID)

                    ''get province
                    Dim tadpProvince As New BusinessObject.dstBranchTableAdapters.spr_Province_Check_SelectTableAdapter
                    Dim dtblProvince As BusinessObject.dstBranch.spr_Province_Check_SelectDataTable = Nothing

                    dtblProvince = tadpProvince.GetData(dtblBranch.First.ID)
                    Dim strProvince As String = dtblProvince.First.ProvinceName


                    Dim strAddress As String = If(dtblBranch.First.IsBranchAddressNull = True, "", dtblBranch.First.BranchAddress)

                    Dim strAdress2 As String = If(dtblFile.First.IsAddress2Null = False AndAlso dtblFile.First.Address2 <> "", dtblFile.First.Address2, If(dtblFile.First.IsAddressNull = False, dtblFile.First.Address, ""))


                    Dim strNationalID2 As String = If(dtblFile.First.IsNationalID2Null = False AndAlso dtblFile.First.NationalID2 <> "", dtblFile.First.NationalID2, If(dtblFile.First.IsNationalIDNull = False, dtblFile.First.NationalID, ""))


                    Dim strMobileNo2 As String = If(dtblFile.First.IsMobileNo2Null = False AndAlso dtblFile.First.MobileNo2 <> "", dtblFile.First.MobileNo2, If(dtblFile.First.IsMobileNoNull = False, dtblFile.First.MobileNo, ""))
                    Dim strPhone As String = If(dtblFile.First.IsTelephoneHomeNull = True, dtblFile.First.TelephoneWork, dtblFile.First.TelephoneHome)
                    Dim strBranchPhone As String = If(dtblBranch.First.IsTelephoneNull = True, "", dtblBranch.First.Telephone)

                    strHTMLMain &= MakePrintFile("", "", strAdress2, strNationalID2, strMobileNo2, dtblBranch.First.BranchName, strAddress, strLCNO, strLoanType, strProvince, strFullName, strPhone, strBranchPhone)

                    If j <> strLoanCustomer.Length - 2 Then

                        strHTMLMain &= "<br /><br /><br /><br /><br />"
                    End If
                Next


                divMain.InnerHtml = strHTMLMain

            ElseIf Not Session("intFileID") Is Nothing AndAlso Not Session("intLoanID") Is Nothing Then

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

                    Dim strBranch As String = dtblBranch.First.BranchName
                    lblBranch.Text = strBranch
                    Dim strAddress As String = If(dtblBranch.First.IsBranchAddressNull = True, "", dtblBranch.First.BranchAddress)
                    lblAddress.Text = strAddress


                    Dim strFName As String = If(dtblFile.First.IsFNameNull = False, dtblFile.First.FName, "")
                    Dim strLName As String = If(dtblFile.First.IsLNameNull = False, dtblFile.First.LName, "")

                    Dim strFullName As String = strFName & " " & strLName
                    lblFullName.Text = strFullName

                    Dim strAdress2 As String = If(dtblFile.First.IsAddress2Null = False AndAlso dtblFile.First.Address2 <> "", dtblFile.First.Address2, dtblFile.First.Address)
                    lblAddress2.Text = strAdress2

                    Dim strNationalID2 As String = If(dtblFile.First.IsNationalID2Null = False AndAlso dtblFile.First.NationalID2 <> "", dtblFile.First.NationalID2, dtblFile.First.NationalID)
                    lblNationalID.Text = strNationalID2

                    Dim strMobileNo2 As String = If(dtblFile.First.IsMobileNo2Null = False AndAlso dtblFile.First.MobileNo2 <> "", dtblFile.First.MobileNo2, dtblFile.First.MobileNo)
                    lblMobileNO.Text = strMobileNo2

                    Dim strProvince As String = dtblProvince.First.ProvinceName
                    lblProvince.Text = strProvince

                    Dim strLoan As String = dtblLoan.First.LoanNumber
                    lblLoan.Text = strLoan

                    Dim strLoanType As String = dtblLoanType.First.LoanTypeName
                    lblLoanType.Text = strLoanType

                    Dim strPhone As String = If(dtblFile.First.IsTelephoneHomeNull = True, dtblFile.First.TelephoneWork, dtblFile.First.TelephoneHome)
                    lblPhone.Text = strPhone

                    Dim strBranchPhone As String = If(dtblBranch.First.IsTelephoneNull = True, "", dtblBranch.First.Telephone)

                    Dim strHTML As String = MakePrintFile(Request.QueryString("LetterNO").ToString(), Request.QueryString("RegisterNO").ToString(), strAdress2, strNationalID2, strMobileNo2, strBranch, strAddress, strLoan, strLoanType, strProvince, strFullName, strPhone, strBranchPhone)

                    divMain2.Visible = True
                    divMain2.InnerHtml = strHTML


                End If

            Else


                Return


            End If


        End If


    End Sub


    Private Function MakePrintFile(ByVal strLetter As String, ByVal strRegiterNO As String, ByVal strAddress2 As String, ByVal strNationalNO As String, ByVal strMobileNO As String, ByVal strBranch As String, ByVal strAddress As String, ByVal strLoan As String, ByVal strLoanType As String, ByVal strProvince As String, ByVal strFullName As String, ByVal strPhone As String, ByVal strBranchPhone As String) As String

        Dim strHTMLMain As String = ""
        Dim strNewLoan As String() = strLoan.Split("-")
        Dim strLoanNew As String = ""
        If strNewLoan.Length = 4 Then
            strLoanNew &= strNewLoan(3) & "-" & strNewLoan(2) & "-" & strNewLoan(1) & "-" & strNewLoan(0)

        Else
            strLoanNew = strLoan
        End If



        strHTMLMain += " <table style='border: 2px solid #000000; height: 100%; width: 100%; padding: 0; border-spacing: 0;'>" _
           & "<tr><td style='border-bottom-style: solid; border-bottom-width: 2px; border-bottom-color: #000000'>" _
           & "<table style='height: 100%; width: 100%; font-family:B Nazanin; text-align: right; padding: 0; border-spacing: 0;'>" _
           & "<tr><td style='width: 150px;'> <table> <tr dir='rtl'> <td>" _
           & "<asp:Label ID='lblDate' runat='server'>" & mdlGeneral.GetPersianDate(Date.Now).ToString() & "</asp:Label></td><td>تاریخ</td></tr>" _
           & "<tr><td <asp:Label ID='lblLetterNO' runat='server'>" & strLetter & "</asp:Label></td><td>شماره</td></tr>" _
           & "<tr><td> <asp:Label ID='lblRegisterNO' runat='server'>" & strRegiterNO & "</asp:Label></td><td>پیوست</td></tr></table></td>" _
           & "<td style='text-align: center;' dir='rtl'><span style='font-family:B Nazanin'>بسمه تعالی</span><br/>" _
           & "<span style='font-family:B Nazanin; font-size: x-large; font-weight: bold'>بانک مهر اقتصاد</span><br/>" _
           & "<span style='font-family:B Nazanin; font-size: small;'>شماره ثبت 7788</span></td>" _
           & "<td style='text-align: center;' dir='rtl'><img alt='' src='../../../Images/System/MehrLogoPrint.jpg' Height='52px' Width='92px' /></td>  </tr> </table> </td>  </tr>" _
           & "<tr><td style='text-align: center'><span style='font-family: Titr; font-size: larger;'>برگ اخطاریه</span></td></tr>" _
           & "<tr> <td style='padding-left: 8px'><table style='align-content center; width: 99%; border-style: solid; border-width: 2px; border-color: black; padding: 0; border-spacing: 0'>" _
           & "<tr><td style='border-right: 2px solid #000000; border-bottom: 2px solid #000000; text-align: center; font-family:B Nazanin; font-size: medium; font-weight: bold; '  >مشخصات و اقامتگاه مخاطب</td>" _
           & "<td style='border-bottom-style: solid; border-bottom-width: 2px; border-bottom-color: #000000; text-align: center; font-family:B Nazanin; font-size: medium; font-weight: bold;'>مشخصات و اقامتگاه اخطار کننده</td></tr>" _
           & "<tr><td style='font-family:b nazanin; font-size: medium; border-right-style: solid; border-bottom-style: solid; border-right-width: 2px; border-bottom-width: 2px; border-right-color: #000000; border-bottom-color: #000000; text-align: right; width: 50%; font-weight: bold; padding-right: 2px;' dir='rtl'>آدرس:" _
           & "<asp:Label ID='lblAddress2' runat='server'>" & strAddress2 & "</asp:Label><br />" _
           & "کد ملی:<asp:Label ID='lblNationalID' runat='server'>" & strNationalNO & "</asp:Label><br />" _
           & "همراه:<asp:Label ID='lblMobileNO' runat='server'>" & strMobileNO & "</asp:Label></td>" _
           & "<td style='border-bottom-style: solid; border-bottom-width: 2px; border-bottom-color: #000000; text-align: Right; font-family:B Nazanin; font-size: medium; padding-right: 4px; font-weight: bold; padding-top: 0px;' dir='rtl'>بانک مهر اقتصاد<br />" _
           & "شعبه:<asp:Label ID='lblBranch' runat='server'>" & strBranch & "</asp:Label><br />" _
           & "به نشانی:<asp:Label ID='lblAddress' runat='server'>" & strAddress & "</asp:Label></td></tr>" _
           & "<tr><td style='border-right-style:solid; border-right-width:2px; border-right-color: #000000; font-family:B Nazanin; font-size: medium; font-weight: bold; vertical-align: top' dir='rtl'>شماره تسهیلات:<asp:Label ID='lblLoan' runat='server'>" & strLoanNew & "</asp:Label><br />" _
           & "<asp:Label ID='lblLoanType' runat='server'>" & strLoanType & "</asp:Label>" _
           & "<br /> تلفن ثابت:<asp:Label ID='lblPhone' runat='server'>" & strPhone & "</asp:Label></td>" _
           & "<td dir='rtl' style='font-family:B Nazanin; font-size: medium; font-weight: normal; padding-right: 2px;'>مخاطب محترم خانم / آقای" _
           & "<asp:Label ID='lblFullName' runat='server'>" & strFullName & "</asp:Label><br /><br />سلام علیکم<br />" _
           & "پیرو اخطاریه تلفنی به اطلاع می رساند جنابعالی به موجب اسناد تعهدآور موجود، به این بانک بدهکار می باشید.نظر یه اینکه تاکنون بدهی خود را پرداخت ننموده اید بدینوسیله برای آخرین بار جنابعالی اخطار می گردد کلیه دیون خود را حداکثر ظرف مهلت 3 روز پس از روئیت این اخطاریه پرداخت نمایید در غیر اینصورت نسبت به تعقیبات قضایی از طریق مراجع ذیصلاح اقدام و در این صورت کلیه خسارات قانونی به عهده جنابعالی خواهد بود.<br />" _
           & "<br /><br /> مسئول شعبه <br /> تلفن شعبه:<asp:Label ID='lblBranchNO' runat='server'>" & strBranchPhone & "</asp:Label><br />رونوشت: مدیریت حقوقی سرپرستی استان <asp: Label ID = 'lblProvince' runat='server'>" & strProvince & "</asp: Label> جهت اطلاع و اقدام<br />" _
           & "</td></tr> </table> <br /></td></tr> </table>"



        '' divMain.InnerHtml = strHTMLMain

        Return strHTMLMain

    End Function

End Class