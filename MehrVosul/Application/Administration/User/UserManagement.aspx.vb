Public Class UserManagement
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Page.Response.Cache.SetCacheability(HttpCacheability.NoCache)

        Bootstrap_Panel1.CanNew = True
        Bootstrap_Panel1.CanSave = False
        Bootstrap_Panel1.CanDelete = True
        Bootstrap_Panel1.CanSearch = True
        Bootstrap_Panel1.CanCancel = False
        Bootstrap_Panel1.CanUp = False
        Bootstrap_Panel1.CanWizard = True
        Bootstrap_Panel1.CanConfirmRequest = False
        Bootstrap_Panel1.CanReject = False
        Bootstrap_Panel1.CanDisplay = False
        Bootstrap_Panel1.CanExcel = False
        Bootstrap_Panel1.Enable_Delete_Client_Validate = True
        Bootstrap_Panel1.Enable_Search_Client_Validate = True

        lblInnerPageTitle.Text = "فهرست کاربران سامانه، لطفا طبق ضوابط عمل نمایید"


        If Page.IsPostBack = False Then
         
            If Request.QueryString("Save") IsNot Nothing AndAlso Request.QueryString("Save") = "OK" Then
                Bootstrap_Panel1.ShowMessage("کاربر با موفقیت ذخیره شد", False)
            ElseIf Request.QueryString("Edit") IsNot Nothing AndAlso Request.QueryString("Edit") = "OK" Then
                Bootstrap_Panel1.ShowMessage("کاربر با موفقیت ویرایش شد", False)
            ElseIf Request.QueryString("Save") IsNot Nothing AndAlso Request.QueryString("Save") = "NO" Then
                Bootstrap_Panel1.ShowMessage("در فرآیند ذخیره کاربر خطا رخ داده است", True)
            ElseIf Request.QueryString("ChangePass") IsNot Nothing AndAlso Request.QueryString("ChangePass") = "OK" Then
                Bootstrap_Panel1.ShowMessage("کلمه عبور کاربر با موفقیت تغییر کرد", False)
            ElseIf Request.QueryString("Edit") IsNot Nothing AndAlso Request.QueryString("Edit") = "NO" Then
                Bootstrap_Panel1.ShowMessage("در فرآیند ویرایش کاربر خطا رخ داده است", True)
            Else
                Bootstrap_Panel1.ClearMessage()
            End If


        End If

        If hdnAction.Value.StartsWith("E") = True Then
            Dim intEditUserID As Integer = CInt(hdnAction.Value.Split(";")(1))
            Session("intEditUserID") = CObj(intEditUserID)
            Response.Redirect("UserEdit.aspx")
        ElseIf hdnAction.Value.StartsWith("C") = True Then
            Dim intEditUserID As Long = CInt(hdnAction.Value.Split(";")(1))
            Session("intEditUserID") = CObj(intEditUserID)
            Response.Redirect("UserChangePassword.aspx")
        End If


    End Sub

#Region "Ajax"




    <System.Web.Services.WebMethod()> Public Shared Function DeleteOperation_Server(theKeys() As Integer) As String

        Dim dtblUserLogin As BusinessObject.dstUser.spr_User_Login_SelectDataTable = CType(HttpContext.Current.Session("dtblUserLogin"), BusinessObject.dstUser.spr_User_Login_SelectDataTable)
        Dim drwUserLogin As BusinessObject.dstUser.spr_User_Login_SelectRow = dtblUserLogin.Rows(0)



        For i As Integer = 0 To theKeys.Length - 1
            Dim intPKey As Integer = CInt(theKeys(i))

            Dim qryUser As New BusinessObject.dstUserTableAdapters.QueriesTableAdapter
            Try
                If drwUserLogin.ID = intPKey Then
                    Throw New Exception("شما امکان حذف کاربر را ندارید")
                End If


                Dim qryUserAccessGroup As New BusinessObject.dstAccessgroupUserTableAdapters.QueriesTableAdapter

                qryUserAccessGroup.spr_AccessgroupUser_User_Delete(intPKey)
                qryUser.spr_User_Delete(intPKey)


            Catch ex As Exception
                Return ex.Message
            End Try

        Next i

        Return ""



    End Function


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
                If strFilter IsNot Nothing Then
                    intAction = 2
                End If
            ElseIf drwUserLogin.IsHadiSystem = True Then


                intAction = 7
                If strFilter IsNot Nothing Then
                    intAction = 8
                End If
            ElseIf drwUserLogin.FK_AccessGroupID = 3438 Then

                intAction = 9
                If strFilter IsNot Nothing Then
                    intAction = 10
                End If


            ElseIf drwUserLogin.IsDataUserAdmin = True And blnAdminBranch = False Then
                intAction = 5
                If strFilter IsNot Nothing Then
                    intAction = 6
                End If

            ElseIf drwUserLogin.IsDataUserAdmin = True And blnAdminBranch = True Then

                intAction = 3
                If strFilter IsNot Nothing Then
                    intAction = 4
                End If




            End If

           
            Dim intToIndex As Integer = intPageNo * mdlGeneral.cnst_RowsCountInPage
            Dim intFromIndex As Integer = (intToIndex - mdlGeneral.cnst_RowsCountInPage) + 1
            Dim tadpUserManagement As New BusinessObject.dstUserTableAdapters.spr_User_Management_SelectTableAdapter
            Dim dtblUserManagement As BusinessObject.dstUser.spr_User_Management_SelectDataTable = Nothing
            dtblUserManagement = tadpUserManagement.GetData(intAction, intFromIndex, intToIndex, strFilter, drwUserLogin.FK_BrnachID, drwUserLogin.Fk_ProvinceID)

            Dim strResult As String = ""
            Dim intColumnCount As Integer = dtblUserManagement.Columns.Count

            If intAction = 1 Or intAction = 3 Or intAction = 5 Or intAction = 9 Or intAction = 7 Then
                For Each drwUserManagement As BusinessObject.dstUser.spr_User_Management_SelectRow In dtblUserManagement.Rows


                    strResult &= ";n;;@;" & CStr(drwUserManagement.Item(0))
                    For i As Integer = 1 To intColumnCount - 1
                        strResult &= ";@;" & CStr(drwUserManagement.Item(i))
                    Next


                Next drwUserManagement
            Else
                For Each drwUserManagement As BusinessObject.dstUser.spr_User_Management_SelectRow In dtblUserManagement.Rows


                    strResult &= ";n;;@;" & CStr(drwUserManagement.Item(0))
                    strResult &= ";@;" & CStr(drwUserManagement.Item(1))

                    For i As Integer = 2 To intColumnCount - 1
                        strResult &= ";@;" & CStr(drwUserManagement.Item(i)).ToLower.Replace(strFilter.ToLower, "<b><font color='#0F17FF'>" & strFilter & "</font></b>")
                    Next
                Next drwUserManagement
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
            If strFilter IsNot Nothing Then
                intAction = 2
            End If

        ElseIf drwUserLogin.IsHadiSystem = True Then


            intAction = 7
            If strFilter IsNot Nothing Then
                intAction = 8
            End If

        ElseIf drwUserLogin.FK_AccessGroupID = 3438 Then

            intAction = 9
            If strFilter IsNot Nothing Then
                intAction = 10
            End If

        ElseIf drwUserLogin.IsDataUserAdmin = True And blnAdminBranch = False Then
            intAction = 5
            If strFilter IsNot Nothing Then
                intAction = 6
            End If

        ElseIf drwUserLogin.IsDataUserAdmin = True And blnAdminBranch = True Then

            intAction = 3
            If strFilter IsNot Nothing Then
                intAction = 4
            End If


        End If




        Dim tadpUserCount As New BusinessObject.dstUserTableAdapters.spr_User_Count_SelectTableAdapter
        Dim dtblUserCount As BusinessObject.dstUser.spr_User_Count_SelectDataTable = Nothing
        dtblUserCount = tadpUserCount.GetData(intAction, strFilter, drwUserLogin.FK_BrnachID, drwUserLogin.Fk_ProvinceID)
        Dim drwUserCount As BusinessObject.dstUser.spr_User_Count_SelectRow = dtblUserCount.Rows(0)
        Dim intPageCount As Integer = Math.Ceiling(drwUserCount.UserCount / mdlGeneral.cnst_RowsCountInPage)
        Dim arrResult(1) As Integer
        arrResult(0) = drwUserCount.UserCount
        arrResult(1) = intPageCount
        Return arrResult

    End Function

#End Region

    Private Sub Bootstrap_Panel1_Panel_New_Click(sender As Object, e As System.EventArgs) Handles Bootstrap_Panel1.Panel_New_Click
        Response.Redirect("UserNew.aspx")
        Return
    End Sub



    Private Sub Bootstrap_Panel1_Panel_Wizard_Click(sender As Object, e As EventArgs) Handles Bootstrap_Panel1.Panel_Wizard_Click
        Response.Redirect("UserMagic.aspx")
        Return
    End Sub
End Class