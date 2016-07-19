Public Class BranchManagement
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

        lblInnerPageTitle.Text = "فهرست شعبه ها، لطفا طبق ضوابط عمل نمایید"


        If Page.IsPostBack = False Then

            If Request.QueryString("Save") IsNot Nothing AndAlso Request.QueryString("Save") = "OK" Then
                Bootstrap_Panel1.ShowMessage("شعبه با موفقیت ذخیره شد", False)
            ElseIf Request.QueryString("Edit") IsNot Nothing AndAlso Request.QueryString("Edit") = "OK" Then
                Bootstrap_Panel1.ShowMessage("شعبه با موفقیت ویرایش شد", False)
            ElseIf Request.QueryString("Save") IsNot Nothing AndAlso Request.QueryString("Save") = "NO" Then
                Bootstrap_Panel1.ShowMessage("در فرآیند ذخیره شعبه خطا رخ داده است", True)
            ElseIf Request.QueryString("Edit") IsNot Nothing AndAlso Request.QueryString("Edit") = "NO" Then
                Bootstrap_Panel1.ShowMessage("در فرآیند ویرایش شعبه خطا رخ داده است", True)
            Else
                Bootstrap_Panel1.ClearMessage()
            End If


        End If

        If hdnAction.Value.StartsWith("E") = True Then
            Dim intEditBranchID As Integer = CInt(hdnAction.Value.Split(";")(1))
            Session("intEditBranchID") = CObj(intEditBranchID)
            Response.Redirect("BranchEdit.aspx")
        End If


    End Sub

#Region "Ajax"




    <System.Web.Services.WebMethod()> Public Shared Function DeleteOperation_Server(theKeys() As Integer) As String

     
        For i As Integer = 0 To theKeys.Length - 1
            Dim intPKey As Integer = CInt(theKeys(i))

            Dim qryBranch As New BusinessObject.dstBranchTableAdapters.QueriesTableAdapter
            Try
                

                qryBranch.spr_Branch_Delete(intPKey)


            Catch ex As Exception
                Return ex.Message
            End Try

        Next i

        Return ""



    End Function


    <System.Web.Services.WebMethod()> Public Shared Function GetPageRecords(intPageNo As Integer, strFilter As String) As String
        Try

            'Dim dtblUserLogin As BusinessObject.dstUser.spr_User_Login_SelectDataTable = CType(HttpContext.Current.Session("dtblUserLogin"), BusinessObject.dstUser.spr_User_Login_SelectDataTable)
            'Dim drwUserLogin As BusinessObject.dstUser.spr_User_Login_SelectRow = dtblUserLogin.Rows(0)

            Dim intAction As Integer = 1
           
            If strFilter IsNot Nothing Then
                intAction = 2
            End If
      

            Dim intToIndex As Integer = intPageNo * mdlGeneral.cnst_RowsCountInPage
            Dim intFromIndex As Integer = (intToIndex - mdlGeneral.cnst_RowsCountInPage) + 1
            Dim tadpBranchManagement As New BusinessObject.dstBranchTableAdapters.spr_Branch_Management_SelectTableAdapter
            Dim dtblBranchManagement As BusinessObject.dstBranch.spr_Branch_Management_SelectDataTable = Nothing
            dtblBranchManagement = tadpBranchManagement.GetData(intAction, intFromIndex, intToIndex, strFilter)

            Dim strResult As String = ""
            Dim intColumnCount As Integer = dtblBranchManagement.Columns.Count


            For Each drwBranchManagement As BusinessObject.dstBranch.spr_Branch_Management_SelectRow In dtblBranchManagement.Rows

                If intAction = 1 Then

                    strResult &= ";n;;@;" & CStr(drwBranchManagement.Item(0))
                    For i As Integer = 1 To intColumnCount - 1
                        strResult &= ";@;" & CStr(drwBranchManagement.Item(i))
                    Next
                Else

                    strResult &= ";n;;@;" & CStr(drwBranchManagement.Item(0))
                    strResult &= ";@;" & CStr(drwBranchManagement.Item(1))

                    For i As Integer = 2 To intColumnCount - 1
                        strResult &= ";@;" & CStr(drwBranchManagement.Item(i)).ToLower.Replace(strFilter.ToLower, "<b><font color='#0F17FF'>" & strFilter & "</font></b>")
                    Next

                End If

            Next drwBranchManagement



            Return strResult

        Catch ex As Exception
            Return "E"
        End Try


    End Function

    <System.Web.Services.WebMethod()> Public Shared Function GetPageCount(strFilter As String) As Integer()

      
        Dim intAction As Integer = 1

        If strFilter IsNot Nothing Then
            intAction = 2
        End If
     

        Dim tadpBranchCount As New BusinessObject.dstBranchTableAdapters.spr_Branch_Count_SelectTableAdapter
        Dim dtblBranchCount As BusinessObject.dstBranch.spr_Branch_Count_SelectDataTable = Nothing
        dtblBranchCount = tadpBranchCount.GetData(intAction, strFilter)
        Dim drwBranchCount As BusinessObject.dstBranch.spr_Branch_Count_SelectRow = dtblBranchCount.Rows(0)
        Dim intPageCount As Integer = Math.Ceiling(drwBranchCount.BranchCount / mdlGeneral.cnst_RowsCountInPage)
        Dim arrResult(1) As Integer
        arrResult(0) = drwBranchCount.BranchCount
        arrResult(1) = intPageCount
        Return arrResult

    End Function

#End Region

    Private Sub Bootstrap_Panel1_Panel_New_Click(sender As Object, e As System.EventArgs) Handles Bootstrap_Panel1.Panel_New_Click
        Response.Redirect("BranchNew.aspx")
        Return
    End Sub
End Class