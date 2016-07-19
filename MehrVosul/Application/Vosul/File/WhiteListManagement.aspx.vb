Public Class WhiteListManagement
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        Page.Response.Cache.SetCacheability(HttpCacheability.NoCache)

        Bootstrap_Panel1.CanNew = True
        Bootstrap_Panel1.CanSave = False
        Bootstrap_Panel1.CanDelete = True
        Bootstrap_Panel1.CanSearch = True
        Bootstrap_Panel1.CanCancel = False
        Bootstrap_Panel1.CanUp = False
        Bootstrap_Panel1.CanWizard = False
        Bootstrap_Panel1.CanConfirmRequest = False
        Bootstrap_Panel1.CanReject = False
        Bootstrap_Panel1.CanDisplay = False
        Bootstrap_Panel1.CanExcel = False
        Bootstrap_Panel1.Enable_Delete_Client_Validate = True
        Bootstrap_Panel1.Enable_Search_Client_Validate = True

        ''lblInnerPageTitle.Text = "فهرست  لیست سفید، لطفا طبق ضوابط عمل نمایید"

        If Not Session("intLoanID") Is Nothing Then
            Dim dtblLoan As BusinessObject.dstLoan.spr_Loan_SelectDataTable = Nothing
            Dim tadpLoan As New BusinessObject.dstLoanTableAdapters.spr_Loan_SelectTableAdapter

            hdnLoanID.Value = Session("intLoanID")
            dtblLoan = tadpLoan.GetData(2, CInt(Session("intLoanID")), -1, -1)

            If dtblLoan.Rows.Count > 0 Then
                Session("LoanNumber") = dtblLoan.First.LoanNumber.ToString()
                lblInnerPageTitle.Text = "شماره وام: " & dtblLoan.First.LoanNumber.ToString()
            End If

        Else
            Return
        End If

        If Page.IsPostBack = False Then

            If Request.QueryString("Save") IsNot Nothing AndAlso Request.QueryString("Save") = "OK" Then
                Bootstrap_Panel1.ShowMessage("لیست سفید با موفقیت ذخیره شد", False)
            ElseIf Request.QueryString("Edit") IsNot Nothing AndAlso Request.QueryString("Edit") = "OK" Then
                Bootstrap_Panel1.ShowMessage("لیست سفید با موفقیت ویرایش شد", False)
            ElseIf Request.QueryString("Save") IsNot Nothing AndAlso Request.QueryString("Save") = "NO" Then
                Bootstrap_Panel1.ShowMessage("در فرآیند ذخیره لیست سفید خطا رخ داده است", True)
            ElseIf Request.QueryString("Edit") IsNot Nothing AndAlso Request.QueryString("Edit") = "NO" Then
                Bootstrap_Panel1.ShowMessage("در فرآیند ویرایش لیست سفید خطا رخ داده است", True)
            Else
                Bootstrap_Panel1.ClearMessage()
            End If


        End If

     

    End Sub

#Region "Ajax"


    <System.Web.Services.WebMethod()> Public Shared Function DeleteOperation_Server(theKeys() As String) As String

        Dim dtblUserLogin As BusinessObject.dstUser.spr_User_Login_SelectDataTable = CType(HttpContext.Current.Session("dtblUserLogin"), BusinessObject.dstUser.spr_User_Login_SelectDataTable)
        Dim drwUserLogin As BusinessObject.dstUser.spr_User_Login_SelectRow = dtblUserLogin.Rows(0)


        For i As Integer = 0 To theKeys.Length - 1
            Dim strPKey As String = theKeys(i)

            Dim qryWhiteList As New BusinessObject.dstCommitmentActiveTableAdapters.QueriesTableAdapter
            Try



                If strPKey.Substring(0, 2) = "AC" Then

                    Dim intPKey As Integer = CInt(strPKey.Substring(2, strPKey.Length - 2))
                    qryWhiteList.spr_CommitmentActive_Delete(intPKey)


                Else

                    Return "امکان حذف رکورد آرشیو وجود ندارد"

                End If



            Catch ex As Exception
                Return ex.Message
            End Try

        Next i

        Return ""



    End Function


    <System.Web.Services.WebMethod()> Public Shared Function GetPageRecords(intPageNo As Integer, strFilter As String, intLoanID As Integer) As String
        Try



            Dim intAction As Integer

            intAction = 1
            If strFilter IsNot Nothing Then
                intAction = 2
            End If

            Dim intToIndex As Integer = intPageNo * mdlGeneral.cnst_RowsCountInPage
            Dim intFromIndex As Integer = (intToIndex - mdlGeneral.cnst_RowsCountInPage) + 1
            Dim tadpWhiteListManagement As New BusinessObject.dstCommitmentActiveTableAdapters.spr_CommitmentManagement_SelectTableAdapter
            Dim dtblWhiteListManagement As BusinessObject.dstCommitmentActive.spr_CommitmentManagement_SelectDataTable = Nothing
            dtblWhiteListManagement = tadpWhiteListManagement.GetData(intAction, intFromIndex, intToIndex, strFilter, intLoanID)

            Dim strResult As String = ""
            Dim intColumnCount As Integer = dtblWhiteListManagement.Columns.Count

            If intAction = 1 Or intAction = 3 Then
                For Each drwWhiteListManagement As BusinessObject.dstCommitmentActive.spr_CommitmentManagement_SelectRow In dtblWhiteListManagement.Rows


                    strResult &= ";n;;@;" & CStr(drwWhiteListManagement.Item(0))
                    For i As Integer = 1 To intColumnCount - 1
                        strResult &= ";@;" & CStr(drwWhiteListManagement.Item(i))
                    Next


                Next drwWhiteListManagement

            Else

                For Each drwWhiteListManagement As BusinessObject.dstCommitmentActive.spr_CommitmentManagement_SelectRow In dtblWhiteListManagement.Rows


                    strResult &= ";n;;@;" & CStr(drwWhiteListManagement.Item(0))
                    strResult &= ";@;" & CStr(drwWhiteListManagement.Item(1))
                    For i As Integer = 2 To intColumnCount - 1
                        strResult &= ";@;" & CStr(drwWhiteListManagement.Item(i)).ToLower.Replace(strFilter.ToLower, "<b><font color='#0F17FF'>" & strFilter & "</font></b>")
                    Next


                Next drwWhiteListManagement


            End If



            Return strResult

        Catch ex As Exception
            Return "E"
        End Try


    End Function

    <System.Web.Services.WebMethod()> Public Shared Function GetPageCount(strFilter As String, intLoanID As Integer) As Integer()



        Dim intAction As Integer

        intAction = 1
        If strFilter IsNot Nothing Then
            intAction = 2
        End If


        Dim tadpWhiteListCount As New BusinessObject.dstCommitmentActiveTableAdapters.spr_Commitment_Count_SelectTableAdapter
        Dim dtblWhiteListCount As BusinessObject.dstCommitmentActive.spr_Commitment_Count_SelectDataTable = Nothing
        dtblWhiteListCount = tadpWhiteListCount.GetData(intAction, strFilter, intLoanID)
        Dim drwWhiteListCount As BusinessObject.dstCommitmentActive.spr_Commitment_Count_SelectRow = dtblWhiteListCount.Rows(0)
        Dim intPageCount As Integer = Math.Ceiling(drwWhiteListCount.CommitmentCount / mdlGeneral.cnst_RowsCountInPage)
        Dim arrResult(1) As Integer
        arrResult(0) = drwWhiteListCount.CommitmentCount
        arrResult(1) = intPageCount
        Return arrResult

    End Function

#End Region

    Private Sub Bootstrap_Panel1_Panel_New_Click(sender As Object, e As System.EventArgs) Handles Bootstrap_Panel1.Panel_New_Click
        Response.Redirect("WhiteListNew.aspx")
        Return
    End Sub
End Class