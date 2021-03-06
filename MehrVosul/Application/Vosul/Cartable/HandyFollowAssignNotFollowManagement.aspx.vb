﻿Public Class HandyFollowAssignNotFollowManagement
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

            ''check the access Group id
            Dim tadpAccessgroupUser As New BusinessObject.dstAccessgroupUserTableAdapters.spr_AccessgroupUserByID_SelectTableAdapter
            Dim dtblAccessgroupUser As BusinessObject.dstAccessgroupUser.spr_AccessgroupUserByID_SelectDataTable = Nothing

            Dim blnAdminBranch As Boolean = False
            dtblAccessgroupUser = tadpAccessgroupUser.GetData(drwUserLogin.ID, 3431)
            If dtblAccessgroupUser.Rows.Count = 0 Then
                dtblAccessgroupUser = tadpAccessgroupUser.GetData(drwUserLogin.ID, 3436)
                If dtblAccessgroupUser.Rows.Count > 0 Then
                    blnAdminBranch = True
                End If
            End If


            Dim intAction As Integer
            If drwUserLogin.IsDataAdmin = True Then
                intAction = 1

            ElseIf drwUserLogin.IsDataUserAdmin = True AndAlso blnAdminBranch = False Then
                intAction = 2


            ElseIf drwUserLogin.IsDataUserAdmin = True AndAlso blnAdminBranch = True Then
                intAction = 3

            End If



            Dim intToIndex As Integer = intPageNo * mdlGeneral.cnst_RowsCountInPage
            Dim intFromIndex As Integer = (intToIndex - mdlGeneral.cnst_RowsCountInPage) + 1
            Dim tadpHandyFollowAssignManagement As New BusinessObject.dstHandyFollowTableAdapters.spr_FilesAssignNoFollow_SelectTableAdapter
            Dim dtblHandyFollowAssignManagement As BusinessObject.dstHandyFollow.spr_FilesAssignNoFollow_SelectDataTable = Nothing
            dtblHandyFollowAssignManagement = tadpHandyFollowAssignManagement.GetData(intAction, intFromIndex, intToIndex, drwUserLogin.Fk_ProvinceID, drwUserLogin.FK_BrnachID)

            Dim strResult As String = ""
            Dim intColumnCount As Integer = dtblHandyFollowAssignManagement.Columns.Count

            If intAction = 1 Or intAction = 2 Or intAction = 3 Then
                For Each drwHandyFollowAssignManagement As BusinessObject.dstHandyFollow.spr_FilesAssignNoFollow_SelectRow In dtblHandyFollowAssignManagement.Rows


                    strResult &= ";n;;@;" & CStr(drwHandyFollowAssignManagement.Item(0))
                    For i As Integer = 1 To intColumnCount - 1
                        strResult &= ";@;" & CStr(drwHandyFollowAssignManagement.Item(i))
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

        ''check the access Group id
        Dim tadpAccessgroupUser As New BusinessObject.dstAccessgroupUserTableAdapters.spr_AccessgroupUserByID_SelectTableAdapter
        Dim dtblAccessgroupUser As BusinessObject.dstAccessgroupUser.spr_AccessgroupUserByID_SelectDataTable = Nothing

        Dim blnAdminBranch As Boolean = False
        dtblAccessgroupUser = tadpAccessgroupUser.GetData(drwUserLogin.ID, 3431)
        If dtblAccessgroupUser.Rows.Count = 0 Then
            dtblAccessgroupUser = tadpAccessgroupUser.GetData(drwUserLogin.ID, 3436)
            If dtblAccessgroupUser.Rows.Count > 0 Then
                blnAdminBranch = True
            End If
        End If

        Dim intAction As Integer
        If drwUserLogin.IsDataAdmin = True Then
            intAction = 1
        ElseIf drwUserLogin.IsDataUserAdmin = True AndAlso blnAdminBranch = False Then
            intAction = 2

        ElseIf drwUserLogin.IsDataUserAdmin = True AndAlso blnAdminBranch = True Then
            intAction = 3
        End If


        Dim tadpHandyFollowAssignCount As New BusinessObject.dstHandyFollowTableAdapters.spr_FilesAssignNoFollowCount_SelectTableAdapter
        Dim dtblHandyFollowAssignCount As BusinessObject.dstHandyFollow.spr_FilesAssignNoFollowCount_SelectDataTable = Nothing
        dtblHandyFollowAssignCount = tadpHandyFollowAssignCount.GetData(intAction, drwUserLogin.Fk_ProvinceID, drwUserLogin.FK_BrnachID)
        Dim drwHandyFollowAssignCount As BusinessObject.dstHandyFollow.spr_FilesAssignNoFollowCount_SelectRow = dtblHandyFollowAssignCount.Rows(0)
        Dim intPageCount As Integer = Math.Ceiling(drwHandyFollowAssignCount.HandyFollowAssign / mdlGeneral.cnst_RowsCountInPage)
        Dim arrResult(1) As Integer
        arrResult(0) = drwHandyFollowAssignCount.HandyFollowAssign
        arrResult(1) = intPageCount
        Return arrResult

    End Function


End Class