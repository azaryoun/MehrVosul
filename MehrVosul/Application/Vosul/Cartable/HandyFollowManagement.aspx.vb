Public Class HandyFollowManagement
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Page.Response.Cache.SetCacheability(HttpCacheability.NoCache)

        Bootstrap_Panel1.CanNew = False
        Bootstrap_Panel1.CanSave = False
        Bootstrap_Panel1.CanDelete = False
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

        lblInnerPageTitle.Text = "فهرست  پرونده ها، لطفا طبق ضوابط عمل نمایید"


        If Page.IsPostBack = False Then

            If Request.QueryString("Save") IsNot Nothing AndAlso Request.QueryString("Save") = "OK" Then
                Bootstrap_Panel1.ShowMessage("تخصیص پرونده با موفقیت انجام شد", False)
            ElseIf Request.QueryString("Edit") IsNot Nothing AndAlso Request.QueryString("Edit") = "OK" Then
                Bootstrap_Panel1.ShowMessage("تخصیص پرونده با موفقیت ویرایش شد", False)
            ElseIf Request.QueryString("Save") IsNot Nothing AndAlso Request.QueryString("Save") = "NO" Then
                Bootstrap_Panel1.ShowMessage("در فرآیند ذخیره تخصیص پرونده خطا رخ داده است", True)
            ElseIf Request.QueryString("Edit") IsNot Nothing AndAlso Request.QueryString("Edit") = "NO" Then
                Bootstrap_Panel1.ShowMessage("در فرآیند ویرایش تخصیص پرونده خطا رخ داده است", True)
            Else
                Bootstrap_Panel1.ClearMessage()
            End If

        End If

        If hdnAction.Value.StartsWith("E") = True Then


            Dim tadpHandyFollowAssign As New BusinessObject.dstHandyFollowTableAdapters.spr_HandyFollowAssign_SelectTableAdapter
            Dim dtblHandyFollowAssign As BusinessObject.dstHandyFollow.spr_HandyFollowAssign_SelectDataTable = Nothing



            Dim tadpTotalDeffredLCByLCNO As New BusinessObject.dstTotalDeffredLCTableAdapters.spr_TotalDeffredLC_SelectByLCNOTableAdapter
            Dim dtblTotalDeffredLCByLCNO As BusinessObject.dstTotalDeffredLC.spr_TotalDeffredLC_SelectByLCNODataTable = Nothing

            Dim intFollowAssignID As Integer = CInt(hdnAction.Value.Split(";")(1))


            Dim dtblUserLogin As BusinessObject.dstUser.spr_User_Login_SelectDataTable = CType(HttpContext.Current.Session("dtblUserLogin"), BusinessObject.dstUser.spr_User_Login_SelectDataTable)
            Dim drwUserLogin As BusinessObject.dstUser.spr_User_Login_SelectRow = dtblUserLogin.Rows(0)


            If drwUserLogin.IsDataUserAdmin = False Then
                dtblHandyFollowAssign = tadpHandyFollowAssign.GetData(intFollowAssignID)



                If dtblHandyFollowAssign.Rows.Count > 0 Then


                    dtblTotalDeffredLCByLCNO = tadpTotalDeffredLCByLCNO.GetData(dtblHandyFollowAssign.First.LoanNumber)


                    Dim intFileID As Long = dtblHandyFollowAssign.First.FK_FileID
                    Dim intLoanID As Long = dtblHandyFollowAssign.First.FK_LoanID

                    If dtblTotalDeffredLCByLCNO.Rows.Count > 0 Then
                        Session("AmountDeffed") = dtblTotalDeffredLCByLCNO.First.AmounDefferd
                        Session("customerNO") = dtblTotalDeffredLCByLCNO.First.CustomerNO
                    End If

                    Session("intFileID") = CObj(intFileID)
                    Session("intLoanID") = CObj(intLoanID)
                    Session("HandyFollowAssign") = dtblHandyFollowAssign.First.ID

                    Response.Redirect("../HandyFollow/HandyFollowNew.aspx")
                Else

                    Return

                End If
            Else
                Session("intFollowAssignID") = intFollowAssignID
                Response.Redirect("HandyFollowAssignEdit.aspx")
            End If


        End If


    End Sub

    <System.Web.Services.WebMethod()> Public Shared Function GetPageRecords(intPageNo As Integer, strFilter As String) As String
        Try


            Dim dtblUserLogin As BusinessObject.dstUser.spr_User_Login_SelectDataTable = CType(HttpContext.Current.Session("dtblUserLogin"), BusinessObject.dstUser.spr_User_Login_SelectDataTable)
            Dim drwUserLogin As BusinessObject.dstUser.spr_User_Login_SelectRow = dtblUserLogin.Rows(0)


            Dim intAction As Integer
            If drwUserLogin.IsDataAdmin = True Then
                intAction = 5
                If strFilter IsNot Nothing Then
                    intAction = 6
                End If
            ElseIf drwUserLogin.IsDataUserAdmin = True Then
                intAction = 3
                If strFilter IsNot Nothing Then
                    intAction = 4
                End If

            Else
                intAction = 1
                If strFilter IsNot Nothing Then
                    intAction = 2
                End If
            End If



            Dim intToIndex As Integer = intPageNo * mdlGeneral.cnst_RowsCountInPage
            Dim intFromIndex As Integer = (intToIndex - mdlGeneral.cnst_RowsCountInPage) + 1
            Dim tadpHandyFollowAssignManagement As New BusinessObject.dstHandyFollowTableAdapters.spr_HandyFollowAssign_Management_SelectTableAdapter
            Dim dtblHandyFollowAssignManagement As BusinessObject.dstHandyFollow.spr_HandyFollowAssign_Management_SelectDataTable = Nothing
            dtblHandyFollowAssignManagement = tadpHandyFollowAssignManagement.GetData(intAction, intFromIndex, intToIndex, strFilter, drwUserLogin.ID, drwUserLogin.FK_BrnachID)

            Dim strResult As String = ""
            Dim intColumnCount As Integer = dtblHandyFollowAssignManagement.Columns.Count

            If intAction = 1 Or intAction = 3 Or intAction = 5 Then
                For Each drwHandyFollowAssignManagement As BusinessObject.dstHandyFollow.spr_HandyFollowAssign_Management_SelectRow In dtblHandyFollowAssignManagement.Rows


                    strResult &= ";n;;@;" & CStr(drwHandyFollowAssignManagement.Item(0))
                    For i As Integer = 1 To intColumnCount - 1
                        strResult &= ";@;" & CStr(drwHandyFollowAssignManagement.Item(i))
                    Next


                Next drwHandyFollowAssignManagement

            Else

                For Each drwHandyFollowAssignManagement As BusinessObject.dstHandyFollow.spr_HandyFollowAssign_Management_SelectRow In dtblHandyFollowAssignManagement.Rows


                    strResult &= ";n;;@;" & CStr(drwHandyFollowAssignManagement.Item(0))
                    strResult &= ";@;" & CStr(drwHandyFollowAssignManagement.Item(1))
                    For i As Integer = 2 To intColumnCount - 1
                        strResult &= ";@;" & CStr(drwHandyFollowAssignManagement.Item(i)).ToLower.Replace(strFilter.ToLower, "<b><font color='#0F17FF'>" & strFilter & "</font></b>")
                    Next


                Next drwHandyFollowAssignManagement


            End If



            Return strResult

        Catch ex As Exception
            Return "E"
        End Try


    End Function

    <System.Web.Services.WebMethod()> Public Shared Function GetPageCount(strFilter As String) As Integer()


        Dim dtblUserLogin As BusinessObject.dstUser.spr_User_Login_SelectDataTable = CType(HttpContext.Current.Session("dtblUserLogin"), BusinessObject.dstUser.spr_User_Login_SelectDataTable)
        Dim drwUserLogin As BusinessObject.dstUser.spr_User_Login_SelectRow = dtblUserLogin.Rows(0)

        Dim intAction As Integer
        If drwUserLogin.IsDataAdmin = True Then
            intAction = 5
            If strFilter IsNot Nothing Then
                intAction = 6
            End If
        ElseIf drwUserLogin.IsDataUserAdmin = True Then
            intAction = 3
            If strFilter IsNot Nothing Then
                intAction = 4
            End If

        Else
            intAction = 1
            If strFilter IsNot Nothing Then
                intAction = 2
            End If
        End If


        Dim tadpHandyFollowAssignCount As New BusinessObject.dstHandyFollowTableAdapters.spr_HandyFollowAssign_Count_SelectTableAdapter
        Dim dtblHandyFollowAssignCount As BusinessObject.dstHandyFollow.spr_HandyFollowAssign_Count_SelectDataTable = Nothing
        dtblHandyFollowAssignCount = tadpHandyFollowAssignCount.GetData(intAction, strFilter, drwUserLogin.ID, drwUserLogin.FK_BrnachID)
        Dim drwHandyFollowAssignCount As BusinessObject.dstHandyFollow.spr_HandyFollowAssign_Count_SelectRow = dtblHandyFollowAssignCount.Rows(0)
        Dim intPageCount As Integer = Math.Ceiling(drwHandyFollowAssignCount.HandyFollowAssign / mdlGeneral.cnst_RowsCountInPage)
        Dim arrResult(1) As Integer
        arrResult(0) = drwHandyFollowAssignCount.HandyFollowAssign
        arrResult(1) = intPageCount
        Return arrResult

    End Function

    Private Sub Bootstrap_Panel1_Panel_Wizard_Click(sender As Object, e As EventArgs) Handles Bootstrap_Panel1.Panel_Wizard_Click
        Response.Redirect("HandyFollowAssignMagic.aspx")
    End Sub
End Class