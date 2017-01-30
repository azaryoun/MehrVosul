Public Class FileManagement
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

        lblInnerPageTitle.Text = "فهرست  پرونده ها، لطفا طبق ضوابط عمل نمایید"


        If Page.IsPostBack = False Then

            If Request.QueryString("Save") IsNot Nothing AndAlso Request.QueryString("Save") = "OK" Then
                Bootstrap_Panel1.ShowMessage("پرونده با موفقیت ذخیره شد", False)
            ElseIf Request.QueryString("Edit") IsNot Nothing AndAlso Request.QueryString("Edit") = "OK" Then
                Bootstrap_Panel1.ShowMessage("پرونده با موفقیت ویرایش شد", False)
            ElseIf Request.QueryString("Save") IsNot Nothing AndAlso Request.QueryString("Save") = "NO" Then
                Bootstrap_Panel1.ShowMessage("در فرآیند ذخیره پرونده خطا رخ داده است", True)
            ElseIf Request.QueryString("Edit") IsNot Nothing AndAlso Request.QueryString("Edit") = "NO" Then
                Bootstrap_Panel1.ShowMessage("در فرآیند ویرایش پرونده خطا رخ داده است", True)
            ElseIf Request.QueryString("Edit") IsNot Nothing AndAlso Request.QueryString("Edit") = "NOTALLOWED" Then
                Bootstrap_Panel1.ShowMessage("امکان ویرایش این پرونده به دلیل داشتن درخواست معوق دیگر وجود ندارد", True)
            ElseIf Request.QueryString("Edit") IsNot Nothing AndAlso Request.QueryString("Edit") = "NOCHANGE" Then
                Bootstrap_Panel1.ShowMessage("تغییری در این پرونده انجام نشد", True)
            ElseIf Request.QueryString("Edit") IsNot Nothing AndAlso Request.QueryString("Edit") = "Requested" Then
                Bootstrap_Panel1.ShowMessage("درخواست ویرایش پرونده با موفقیت ثبت شد", False)
            ElseIf Request.QueryString("Save") IsNot Nothing AndAlso Request.QueryString("Save") = "Requested" Then
                Bootstrap_Panel1.ShowMessage("درخواست ثبت پرونده با موفقیت ذخیره شد", False)
            Else
                Bootstrap_Panel1.ClearMessage()
            End If


        End If

        If hdnAction.Value.StartsWith("E") = True Then
            Dim intFileTypeID As Integer = CInt(hdnAction.Value.Split(";")(1))
            Session("intEditFileID") = CObj(intFileTypeID)
            Response.Redirect("FileEdit.aspx")
        ElseIf hdnAction.Value.StartsWith("C") = True Then
            Dim intFileID As Long = CInt(hdnAction.Value.Split(";")(1))
            Session("intEditFileID") = CObj(intFileID)
            Response.Redirect("FileLoanDetail.aspx")
        End If

    End Sub

#Region "Ajax"


    <System.Web.Services.WebMethod()> Public Shared Function DeleteOperation_Server(theKeys() As Integer) As String

        Dim dtblUserLogin As BusinessObject.dstUser.spr_User_Login_SelectDataTable = CType(HttpContext.Current.Session("dtblUserLogin"), BusinessObject.dstUser.spr_User_Login_SelectDataTable)
        Dim drwUserLogin As BusinessObject.dstUser.spr_User_Login_SelectRow = dtblUserLogin.Rows(0)


        For i As Integer = 0 To theKeys.Length - 1
            Dim intPKey As Integer = CInt(theKeys(i))

            Dim qryFile As New BusinessObject.dstFileTableAdapters.QueriesTableAdapter
            Try

                If drwUserLogin.IsDataAdmin = True Then
                    qryFile.spr_File_Delete(intPKey)

                Else
                    Dim tadpFile As New BusinessObject.dstFileTableAdapters.spr_File_SelectTableAdapter
                    Dim dtblFile As BusinessObject.dstFile.spr_File_SelectDataTable = Nothing

                    dtblFile = tadpFile.GetData(2, intPKey, "")
                    If dtblFile.Rows.Count = 0 Then

                        dtblFile = tadpFile.GetData(1, intPKey, "")
                        qryFile.spr_File_Insert(dtblFile.First.CustomerNo, dtblFile.First.FName, dtblFile.First.LName, dtblFile.First.FatherName, dtblFile.First.MobileNo, dtblFile.First.NationalID, dtblFile.First.IDNumber, dtblFile.First.Email, dtblFile.First.Address, dtblFile.First.TelephoneHome, dtblFile.First.TelephoneWork, dtblFile.First.IsMale, dtblFile.First.FK_CUserID, 2, intPKey, drwUserLogin.ID)
                        Return "درخواست حذف این رکورد ثبت شد"
                    Else

                        If dtblFile.First.State <> 2 And dtblFile.First.State <> 5 And dtblFile.First.State <> 8 Then
                            dtblFile = tadpFile.GetData(1, intPKey, "")
                            qryFile.spr_File_Insert(dtblFile.First.CustomerNo, dtblFile.First.FName, dtblFile.First.LName, dtblFile.First.FatherName, dtblFile.First.MobileNo, dtblFile.First.NationalID, dtblFile.First.IDNumber, dtblFile.First.Email, dtblFile.First.Address, dtblFile.First.TelephoneHome, dtblFile.First.TelephoneWork, dtblFile.First.IsMale, dtblFile.First.FK_CUserID, 2, intPKey, drwUserLogin.ID)
                            Return "درخواست حذف این رکورد ثبت شد"
                        Else
                            Return "امکان حذف این رکورد به دلیل داشتن درخواست معوق دیگر وجود ندارد "
                        End If
                End If
                    

                End If
            

            Catch ex As Exception
                Return "E" & ex.Message
            End Try

        Next i

        Return ""



    End Function


    <System.Web.Services.WebMethod()> Public Shared Function GetPageRecords(intPageNo As Integer, strFilter As String) As String
        Try


            Dim dtblUserLogin As BusinessObject.dstUser.spr_User_Login_SelectDataTable = CType(HttpContext.Current.Session("dtblUserLogin"), BusinessObject.dstUser.spr_User_Login_SelectDataTable)
            Dim drwUserLogin As BusinessObject.dstUser.spr_User_Login_SelectRow = dtblUserLogin.Rows(0)

            Dim intAction As Integer
            If drwUserLogin.IsDataAdmin = True Then
                intAction = 1
                If strFilter IsNot Nothing Then
                    intAction = 2
                End If
            ElseIf drwUserLogin.IsDataUserAdmin = True Then
                intAction = 5
                If strFilter IsNot Nothing Then
                    intAction = 6
                End If
            Else
                intAction = 3
                If strFilter IsNot Nothing Then
                    intAction = 4
                End If
            End If

            Dim intToIndex As Integer = intPageNo * mdlGeneral.cnst_RowsCountInPage
            Dim intFromIndex As Integer = (intToIndex - mdlGeneral.cnst_RowsCountInPage) + 1
            Dim tadpFileManagement As New BusinessObject.dstFileTableAdapters.spr_File_Management_SelectTableAdapter
            Dim dtblFileManagement As BusinessObject.dstFile.spr_File_Management_SelectDataTable = Nothing
            dtblFileManagement = tadpFileManagement.GetData(intAction, intFromIndex, intToIndex, strFilter, drwUserLogin.FK_BrnachID, drwUserLogin.Fk_ProvinceID)

            Dim strResult As String = ""
            Dim intColumnCount As Integer = dtblFileManagement.Columns.Count

            If intAction = 1 Or intAction = 3 Or intAction = 5 Then
                For Each drwFileManagement As BusinessObject.dstFile.spr_File_Management_SelectRow In dtblFileManagement.Rows


                    strResult &= ";n;;@;" & CStr(drwFileManagement.Item(0))
                    For i As Integer = 1 To intColumnCount - 1
                        strResult &= ";@;" & CStr(drwFileManagement.Item(i))
                    Next


                Next drwFileManagement

            Else

                For Each drwFileManagement As BusinessObject.dstFile.spr_File_Management_SelectRow In dtblFileManagement.Rows


                    strResult &= ";n;;@;" & CStr(drwFileManagement.Item(0))
                    strResult &= ";@;" & CStr(drwFileManagement.Item(1))
                    For i As Integer = 2 To intColumnCount - 1
                        strResult &= ";@;" & CStr(drwFileManagement.Item(i)).ToLower.Replace(strFilter.ToLower, "<b><font color='#0F17FF'>" & strFilter & "</font></b>")
                    Next


                Next drwFileManagement


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
            intAction = 1
            If strFilter IsNot Nothing Then
                intAction = 2
            End If
        ElseIf drwUserLogin.IsDataUserAdmin = True Then
            intAction = 5
            If strFilter IsNot Nothing Then
                intAction = 6
            End If

        Else
            intAction = 3
            If strFilter IsNot Nothing Then
                intAction = 4
            End If
        End If


        Dim tadpFileCount As New BusinessObject.dstFileTableAdapters.spr_File_Count_SelectTableAdapter
        Dim dtblFileCount As BusinessObject.dstFile.spr_File_Count_SelectDataTable = Nothing
        dtblFileCount = tadpFileCount.GetData(intAction, strFilter, drwUserLogin.FK_BrnachID, drwUserLogin.Fk_ProvinceID)
        Dim drwFileCount As BusinessObject.dstFile.spr_File_Count_SelectRow = dtblFileCount.Rows(0)
        Dim intPageCount As Integer = Math.Ceiling(drwFileCount.FilesCount / mdlGeneral.cnst_RowsCountInPage)
        Dim arrResult(1) As Integer
        arrResult(0) = drwFileCount.FilesCount
        arrResult(1) = intPageCount
        Return arrResult

    End Function

#End Region

    Private Sub Bootstrap_Panel1_Panel_New_Click(sender As Object, e As System.EventArgs) Handles Bootstrap_Panel1.Panel_New_Click
        Response.Redirect("FileNew.aspx")
        Return
    End Sub
End Class