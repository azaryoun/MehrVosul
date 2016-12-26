Public Class PreWarningIntervalsManagement
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

        lblInnerPageTitle.Text = "فهرست گردش کار، لطفا طبق ضوابط عمل نمایید"


        If Page.IsPostBack = False Then

            If Request.QueryString("Save") IsNot Nothing AndAlso Request.QueryString("Save") = "OK" AndAlso Request.QueryString("Exeption") Is Nothing Then
                Bootstrap_Panel1.ShowMessage("گردش کار با موفقیت ذخیره شد", False)
            ElseIf Request.QueryString("Edit") IsNot Nothing AndAlso Request.QueryString("Edit") = "OK" Then
                Bootstrap_Panel1.ShowMessage("گردش کار با موفقیت ویرایش شد", False)
            ElseIf Request.QueryString("Save") IsNot Nothing AndAlso Request.QueryString("Save") = "NO" AndAlso Request.QueryString("Exeption") Is Nothing Then
                Bootstrap_Panel1.ShowMessage("در فرآیند ذخیره گردش کار خطا رخ داده است", True)
            ElseIf Request.QueryString("Edit") IsNot Nothing AndAlso Request.QueryString("Edit") = "NO" Then
                Bootstrap_Panel1.ShowMessage("در فرآیند ویرایش گردش کار خطا رخ داده است", True)
            ElseIf Request.QueryString("Save") IsNot Nothing AndAlso Request.QueryString("Save") = "OK" AndAlso Request.QueryString("Exeption") = "Yes" Then
                Bootstrap_Panel1.ShowMessage("استثناء گردش کار با موفقیت ذخیره شد", False)
            ElseIf Request.QueryString("Save") IsNot Nothing AndAlso Request.QueryString("Save") = "NO" AndAlso Request.QueryString("Exeption") = "Yes" Then
                Bootstrap_Panel1.ShowMessage("در فرآیند ذخیره استثناء گردش کار خطا رخ داده است", False)
            Else
                Bootstrap_Panel1.ClearMessage()
            End If


        End If

        If hdnAction.Value.StartsWith("E") = True Then
            Dim intPreWarningIntervalTypeID As Integer = CInt(hdnAction.Value.Split(";")(1))
            Session("intEditPreWarningIntervalID") = CObj(intPreWarningIntervalTypeID)
            Response.Redirect("PreWarningIntervalsEdit.aspx")

        ElseIf hdnAction.Value.StartsWith("C") = True Then

            Dim intEditPreWarningIntervalID As Long = CInt(hdnAction.Value.Split(";")(1))
            Session("intEditPreWarningIntervalID") = CObj(intEditPreWarningIntervalID)

            ''chek if telephon call select then redirect to related form
            Dim tadpWarningIntreval As New BusinessObject.dstPreWarningIntervalTableAdapters.spr_PreWarningIntervals_SelectTableAdapter
            Dim dtblWarningInterval As BusinessObject.dstPreWarningInterval.spr_PreWarningIntervals_SelectDataTable = Nothing

            dtblWarningInterval = tadpWarningIntreval.GetData(intEditPreWarningIntervalID)
            If dtblWarningInterval.First.VoiceMessage = True Then

                ' Response.Redirect("../VoiceMessage/VoiceMessageFileNew.aspx")
                Response.Redirect("../Draft/VoiceMessageDraftText.aspx?Type=5")

            Else

                Response.Redirect("../Draft/DraftManagement.aspx")

            End If

        ElseIf hdnAction.Value.StartsWith("X") = True Then

            ''Dim intPreWarningIntervalTypeID As Integer = CInt(hdnAction.Value.Split(";")(1))
            ''Session("intEditPrePreWarningIntervalID") = CObj(intPreWarningIntervalTypeID)
            ''Response.Redirect("PreWarningIntervalException.aspx")

        End If

    End Sub

#Region "Ajax"




    <System.Web.Services.WebMethod()> Public Shared Function DeleteOperation_Server(theKeys() As Integer) As String


        For i As Integer = 0 To theKeys.Length - 1
            Dim intPKey As Integer = CInt(theKeys(i))

            Dim qryPreWarningInterval As New BusinessObject.dstPreWarningIntervalTableAdapters.QueriesTableAdapter
            Try


                qryPreWarningInterval.spr_PreWarningIntervals_Delete(intPKey)


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


            Dim intAction As Integer = 1
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
            Dim tadpPreWarningIntervalManagement As New BusinessObject.dstPreWarningIntervalTableAdapters.spr_PreWarningIntervals_Management_SelectTableAdapter
            Dim dtblPreWarningIntervalManagement As BusinessObject.dstPreWarningInterval.spr_PreWarningIntervals_Management_SelectDataTable = Nothing
            dtblPreWarningIntervalManagement = tadpPreWarningIntervalManagement.GetData(intAction, intFromIndex, intToIndex, strFilter, drwUserLogin.FK_BrnachID, drwUserLogin.Fk_ProvinceID)

            Dim strResult As String = ""
            Dim intColumnCount As Integer = dtblPreWarningIntervalManagement.Columns.Count

            If intAction = 1 Or intAction = 3 Or intAction = 5 Then
                For Each drwPreWarningIntervalManagement As BusinessObject.dstPreWarningInterval.spr_PreWarningIntervals_Management_SelectRow In dtblPreWarningIntervalManagement.Rows


                    strResult &= ";n;;@;" & CStr(drwPreWarningIntervalManagement.Item(0))
                    For i As Integer = 1 To intColumnCount - 1
                        strResult &= ";@;" & CStr(drwPreWarningIntervalManagement.Item(i))
                    Next


                Next drwPreWarningIntervalManagement
            Else

                For Each drwPreWarningIntervalManagement As BusinessObject.dstPreWarningInterval.spr_PreWarningIntervals_Management_SelectRow In dtblPreWarningIntervalManagement.Rows


                    strResult &= ";n;;@;" & CStr(drwPreWarningIntervalManagement.Item(0))
                    strResult &= ";@;" & CStr(drwPreWarningIntervalManagement.Item(1))

                    For i As Integer = 2 To intColumnCount - 1
                        strResult &= ";@;" & CStr(drwPreWarningIntervalManagement.Item(i)).ToLower.Replace(strFilter.ToLower, "<b><font color='#0F17FF'>" & strFilter & "</font></b>")
                    Next


                Next drwPreWarningIntervalManagement

            End If




            Return strResult

        Catch ex As Exception
            Return "E"
        End Try


    End Function

    <System.Web.Services.WebMethod()> Public Shared Function GetPageCount(strFilter As String) As Integer()


        Dim dtblUserLogin As BusinessObject.dstUser.spr_User_Login_SelectDataTable = CType(HttpContext.Current.Session("dtblUserLogin"), BusinessObject.dstUser.spr_User_Login_SelectDataTable)
        Dim drwUserLogin As BusinessObject.dstUser.spr_User_Login_SelectRow = dtblUserLogin.Rows(0)


        Dim intAction As Integer = 1
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


        Dim tadpPreWarningIntervalCount As New BusinessObject.dstPreWarningIntervalTableAdapters.spr_PreWarningIntervals_Count_SelectTableAdapter
        Dim dtblPreWarningIntervalCount As BusinessObject.dstPreWarningInterval.spr_PreWarningIntervals_Count_SelectDataTable = Nothing
        dtblPreWarningIntervalCount = tadpPreWarningIntervalCount.GetData(intAction, strFilter, drwUserLogin.FK_BrnachID, drwUserLogin.Fk_ProvinceID)
        Dim drwPreWarningIntervalCount As BusinessObject.dstPreWarningInterval.spr_PreWarningIntervals_Count_SelectRow = dtblPreWarningIntervalCount.Rows(0)
        Dim intPageCount As Integer = Math.Ceiling(drwPreWarningIntervalCount.PreWarningIntervalsCount / mdlGeneral.cnst_RowsCountInPage)
        Dim arrResult(1) As Integer
        arrResult(0) = drwPreWarningIntervalCount.PreWarningIntervalsCount
        arrResult(1) = intPageCount
        Return arrResult

    End Function

#End Region

    Private Sub Bootstrap_Panel1_Panel_New_Click(sender As Object, e As System.EventArgs) Handles Bootstrap_Panel1.Panel_New_Click
        Response.Redirect("PreWarningIntervalsNew.aspx")
        Return
    End Sub
End Class