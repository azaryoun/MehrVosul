Public Class FileRequestManagement
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        Page.Response.Cache.SetCacheability(HttpCacheability.NoCache)

        Bootstrap_Panel1.CanNew = False
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

        lblInnerPageTitle.Text = "فهرست  درخواست پرونده ها، لطفا طبق ضوابط عمل نمایید"


        If Page.IsPostBack = False Then


            If Request.QueryString("Edit") IsNot Nothing AndAlso Request.QueryString("Edit") = "OK" Then
                Bootstrap_Panel1.ShowMessage("درخواست تایید شد", False)
            ElseIf Request.QueryString("Edit") IsNot Nothing AndAlso Request.QueryString("Edit") = "NO" Then
                Bootstrap_Panel1.ShowMessage("در فرآیند ویرایش درخواست پرونده خطا رخ داده است", True)
            ElseIf Request.QueryString("Edit") IsNot Nothing AndAlso Request.QueryString("Edit") = "Reject" Then
                Bootstrap_Panel1.ShowMessage("درخواست رد شد", True)
            Else
                Bootstrap_Panel1.ClearMessage()
            End If


        End If

        If hdnAction.Value.StartsWith("E") = True Then
            Dim intFileTypeID As Integer = CInt(hdnAction.Value.Split(";")(1))
            Session("intFileRequestID") = CObj(intFileTypeID)
            Response.Redirect("FileRequestView.aspx")
        End If

    End Sub

#Region "Ajax"


    <System.Web.Services.WebMethod()> Public Shared Function DeleteOperation_Server(theKeys() As Integer) As String

       For i As Integer = 0 To theKeys.Length - 1
            Dim intPKey As Integer = CInt(theKeys(i))

            Dim qryFile As New BusinessObject.dstFileTableAdapters.QueriesTableAdapter
            Dim tadpFile As New BusinessObject.dstFileTableAdapters.spr_File_SelectTableAdapter
            Dim dtblFile As BusinessObject.dstFile.spr_File_SelectDataTable = Nothing

            Try

                dtblFile = tadpFile.GetData(1, intPKey, "")
                If dtblFile.First.State = 2 Or dtblFile.First.State = 5 And dtblFile.First.State = 8 Then
                    qryFile.spr_File_Delete(intPKey)
                Else
                    Return "امکان حذف این رکورد به دلیل اعمال تغییرات وجود ندارد "
                End If



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

            Dim intAction As Integer
            ''  If drwUserLogin.IsDataAdmin = True Then
            intAction = 1
            If strFilter IsNot Nothing Then
                intAction = 2
            End If
            '' ''  ElseIf drwUserLogin.IsDataUserAdmin = True Then
            ''intAction = 5
            ''If strFilter IsNot Nothing Then
            ''    intAction = 6
            ''End If
            ''Else
            ''intAction = 3
            ''If strFilter IsNot Nothing Then
            ''    intAction = 4
            ''End If
            ''End If

            Dim intToIndex As Integer = intPageNo * mdlGeneral.cnst_RowsCountInPage
            Dim intFromIndex As Integer = (intToIndex - mdlGeneral.cnst_RowsCountInPage) + 1
            Dim tadpFileRequestManagement As New BusinessObject.dstFileTableAdapters.spr_FileRequests_Management_SelectTableAdapter
            Dim dtblFileRequestManagement As BusinessObject.dstFile.spr_FileRequests_Management_SelectDataTable = Nothing
            dtblFileRequestManagement = tadpFileRequestManagement.GetData(intAction, intFromIndex, intToIndex, strFilter, drwUserLogin.FK_BrnachID, drwUserLogin.Fk_ProvinceID)

            Dim strResult As String = ""
            Dim intColumnCount As Integer = dtblFileRequestManagement.Columns.Count

            If intAction = 1 Or intAction = 3 Or intAction = 5 Then
                For Each drwFileRequestManagement As BusinessObject.dstFile.spr_FileRequests_Management_SelectRow In dtblFileRequestManagement.Rows


                    strResult &= ";n;;@;" & CStr(drwFileRequestManagement.Item(0))
                    For i As Integer = 1 To intColumnCount - 1
                        strResult &= ";@;" & CStr(drwFileRequestManagement.Item(i))
                    Next


                Next drwFileRequestManagement

            Else

                For Each drwFileRequestManagement As BusinessObject.dstFile.spr_FileRequests_Management_SelectRow In dtblFileRequestManagement.Rows


                    strResult &= ";n;;@;" & CStr(drwFileRequestManagement.Item(0))
                    strResult &= ";@;" & CStr(drwFileRequestManagement.Item(1))

                    For i As Integer = 2 To intColumnCount - 1
                        strResult &= ";@;" & CStr(drwFileRequestManagement.Item(i)).ToLower.Replace(strFilter.ToLower, "<b><font color='#0F17FF'>" & strFilter & "</font></b>")
                    Next


                Next drwFileRequestManagement


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
        '' If drwUserLogin.IsDataAdmin = True Then
        intAction = 1
        If strFilter IsNot Nothing Then
            intAction = 2
        End If
        ''ElseIf drwUserLogin.IsDataUserAdmin = True Then
        ''intAction = 5
        ''If strFilter IsNot Nothing Then
        ''    intAction = 6
        ''End If
        ''Else
        ''intAction = 3
        ''If strFilter IsNot Nothing Then
        ''    intAction = 4
        ''End If
        ''End If


        Dim tadpFileCount As New BusinessObject.dstFileTableAdapters.spr_FileRequests_Count_SelectTableAdapter
        Dim dtblFileCount As BusinessObject.dstFile.spr_FileRequests_Count_SelectDataTable = Nothing
        dtblFileCount = tadpFileCount.GetData(intAction, strFilter, drwUserLogin.FK_BrnachID, drwUserLogin.Fk_ProvinceID)
        Dim drwFileCount As BusinessObject.dstFile.spr_FileRequests_Count_SelectRow = dtblFileCount.Rows(0)
        Dim intPageCount As Integer = Math.Ceiling(drwFileCount.FilesCount / mdlGeneral.cnst_RowsCountInPage)
        Dim arrResult(1) As Integer
        arrResult(0) = drwFileCount.FilesCount
        arrResult(1) = intPageCount
        Return arrResult

    End Function

#End Region

    
End Class