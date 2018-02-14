Public Class NoticeManagement
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

        lblInnerPageTitle.Text = "فهرست اعلان ها، لطفا طبق ضوابط عمل نمایید"


        If Page.IsPostBack = False Then

            If Request.QueryString("Save") IsNot Nothing AndAlso Request.QueryString("Save") = "OK" Then
                Bootstrap_Panel1.ShowMessage("اعلان با موفقیت تعریف شد", False)
            ElseIf Request.QueryString("Edit") IsNot Nothing AndAlso Request.QueryString("Edit") = "OK" Then
                Bootstrap_Panel1.ShowMessage("اعلان با موفقیت ویرایش شد", False)
            ElseIf Request.QueryString("Save") IsNot Nothing AndAlso Request.QueryString("Save") = "NO" Then
                Bootstrap_Panel1.ShowMessage("فرآیند تعریف اعلان با شکست مواجه شده است", True)
            ElseIf Request.QueryString("Edit") IsNot Nothing AndAlso Request.QueryString("Edit") = "NO" Then
                Bootstrap_Panel1.ShowMessage("فرآیند ویرایش اعلان با شکست مواجه شده است", True)
            Else
                Bootstrap_Panel1.ClearMessage()
            End If


        End If

        If hdnAction.Value.StartsWith("E") = True Then
            Dim intNoticeID As Integer = CInt(hdnAction.Value.Split(";")(1))
            Session("intNoticeID") = CObj(intNoticeID)
            Response.Redirect("NoticeEdit.aspx")
        End If

    End Sub


#Region "Ajax"


    <System.Web.Services.WebMethod()> Public Shared Function DeleteOperation_Server(theKeys() As Integer) As String



        For i As Integer = 0 To theKeys.Length - 1
            Dim intPKey As Integer = CInt(theKeys(i))

            Dim qryNotice As New BusinessObject.dstNoticeTableAdapters.QueriesTableAdapter
            Try

                qryNotice.spr_Notice_Delete(intPKey)


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
            If drwUserLogin.IsDataAdmin = True Then

                intAction = 1
                If strFilter IsNot Nothing Then
                    intAction = 2
                End If

            Else
                intAction = 3
                If strFilter IsNot Nothing Then
                    intAction = 4
                End If
            End If



            Dim intToIndex As Integer = intPageNo * mdlGeneral.cnst_RowsCountInPage
            Dim intFromIndex As Integer = (intToIndex - mdlGeneral.cnst_RowsCountInPage) + 1
            Dim tadpNoticeManagement As New BusinessObject.dstNoticeTableAdapters.spr_NoticeManagement_SelectTableAdapter
            Dim dtblNoticeManagement As BusinessObject.dstNotice.spr_NoticeManagement_SelectDataTable = Nothing
            dtblNoticeManagement = tadpNoticeManagement.GetData(intAction, intFromIndex, intToIndex, strFilter, drwUserLogin.Fk_ProvinceID)

            Dim strResult As String = ""
            Dim intColumnCount As Integer = dtblNoticeManagement.Columns.Count

            If intAction = 1 Then
                For Each drwNoticeManagement As BusinessObject.dstNotice.spr_NoticeManagement_SelectRow In dtblNoticeManagement.Rows


                    strResult &= ";n;;@;" & CStr(drwNoticeManagement.Item(0))
                    For i As Integer = 1 To intColumnCount - 1
                        strResult &= ";@;" & CStr(drwNoticeManagement.Item(i))
                    Next


                Next drwNoticeManagement
            Else
                For Each drwNoticeManagement As BusinessObject.dstNotice.spr_NoticeManagement_SelectRow In dtblNoticeManagement.Rows

                    strResult &= ";n;;@;" & CStr(drwNoticeManagement.Item(0))
                    strResult &= ";@;" & CStr(drwNoticeManagement.Item(1))

                    For i As Integer = 2 To intColumnCount - 1
                        strResult &= ";@;" & CStr(drwNoticeManagement.Item(i)).ToLower.Replace(strFilter.ToLower, "<b><font color='#0F17FF'>" & strFilter & "</font></b>")
                    Next
                Next drwNoticeManagement
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

        Else
            intAction = 3
            If strFilter IsNot Nothing Then
                intAction = 4
            End If
        End If

        Dim tadpNoticeCount As New BusinessObject.dstNoticeTableAdapters.spr_NoticeCount_SelectTableAdapter
        Dim dtblNoticeCount As BusinessObject.dstNotice.spr_NoticeCount_SelectDataTable = Nothing
        dtblNoticeCount = tadpNoticeCount.GetData(intAction, strFilter, -1)
        Dim drwNoticeCount As BusinessObject.dstNotice.spr_NoticeCount_SelectRow = dtblNoticeCount.Rows(0)
        Dim intPageCount As Integer = Math.Ceiling(drwNoticeCount.NoticeCount / mdlGeneral.cnst_RowsCountInPage)
        Dim arrResult(1) As Integer
        arrResult(0) = drwNoticeCount.NoticeCount
        arrResult(1) = intPageCount
        Return arrResult

    End Function

    Private Sub Bootstrap_Panel1_Panel_New_Click(sender As Object, e As EventArgs) Handles Bootstrap_Panel1.Panel_New_Click

        Response.Redirect("NoticeNew.aspx")


    End Sub
#End Region

End Class