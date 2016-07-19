Imports System.Media
Imports System.IO
Public Class VoiceMessageManagement
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

        lblInnerPageTitle.Text = "فهرست پیامکهای صوتی، لطفا طبق ضوابط عمل نمایید"


        If Page.IsPostBack = False Then

            If Request.QueryString("Save") IsNot Nothing AndAlso Request.QueryString("Save") = "OK" Then
                Bootstrap_Panel1.ShowMessage("پیامک صوتی با موفقیت ذخیره شد", False)
            ElseIf Request.QueryString("Edit") IsNot Nothing AndAlso Request.QueryString("Edit") = "OK" Then
                Bootstrap_Panel1.ShowMessage("پیامک صوتی با موفقیت ویرایش شد", False)
            ElseIf Request.QueryString("Save") IsNot Nothing AndAlso Request.QueryString("Save") = "NO" Then
                Bootstrap_Panel1.ShowMessage("در فرآیند ذخیره پیامک صوتی خطا رخ داده است", True)
            ElseIf Request.QueryString("Edit") IsNot Nothing AndAlso Request.QueryString("Edit") = "NO" Then
                Bootstrap_Panel1.ShowMessage("در فرآیند ویرایش پیامک صوتی خطا رخ داده است", True)
            Else
                Bootstrap_Panel1.ClearMessage()
            End If


        End If

        If hdnAction.Value.StartsWith("E") = True Then
            Dim intEditVoiceMessageID As Integer = CInt(hdnAction.Value.Split(";")(1))
            Session("intEditVoiceMessageID") = CObj(intEditVoiceMessageID)
            Response.Redirect("VoiceMessageFileEdit.aspx")


        ElseIf hdnAction.Value.StartsWith("L") = True Then
            Dim intRecordID As Long = 0
            'Query Strings
            intRecordID = CLng(hdnAction.Value.Split(";")(1))


            Dim tadpRecords As New BusinessObject.dstZamanakTableAdapters.spr_Record_List_SelectTableAdapter
            Dim dtblRecords As BusinessObject.dstZamanak.spr_Record_List_SelectDataTable = Nothing
            dtblRecords = tadpRecords.GetData(2, 0, intRecordID, "", 0)
            If dtblRecords.Rows.Count > 0 Then

                Dim strFileName As String = dtblRecords.First().FileName
                If strFileName <> "" Then
                    If Not File.Exists(Server.MapPath("") & "\Listen\" & intRecordID.ToString() & strFileName & ".wav") Then
                        Bootstrap_Panel1.ShowMessage("فایل صوتی مورد نظر وجود ندارد.", True)

                        Exit Sub
                    End If

                    Dim player As New SoundPlayer
                    player.SoundLocation = Server.MapPath("") & "\Listen\" & intRecordID.ToString() & strFileName & ".wav"
                    player.Play()
                Else
                    Bootstrap_Panel1.ShowMessage("نام فایل صوتی نامعتبر است.", True)

                    Exit Sub
                End If
            End If

        End If


    End Sub

#Region "Ajax"




    <System.Web.Services.WebMethod()> Public Shared Function DeleteOperation_Server(theKeys() As Integer) As String


        For i As Integer = 0 To theKeys.Length - 1
            Dim intPKey As Integer = CInt(theKeys(i))

            Dim qryVoiceMessage As New BusinessObject.dstZamanakTableAdapters.QueriesTableAdapter
            Try


                qryVoiceMessage.spr_VoiceRecords_Delete(intPKey)


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

            If strFilter IsNot Nothing Then
                intAction = 2
            End If


            Dim intToIndex As Integer = intPageNo * mdlGeneral.cnst_RowsCountInPage
            Dim intFromIndex As Integer = (intToIndex - mdlGeneral.cnst_RowsCountInPage) + 1
            Dim tadpVoiceMessageManagement As New BusinessObject.dstZamanakTableAdapters.spr_VoiceRecords_Management_SelectTableAdapter
            Dim dtblVoiceMessageManagement As BusinessObject.dstZamanak.spr_VoiceRecords_Management_SelectDataTable = Nothing
            dtblVoiceMessageManagement = tadpVoiceMessageManagement.GetData(intAction, intFromIndex, intToIndex, strFilter, drwUserLogin.ID)

            Dim strResult As String = ""
            Dim intColumnCount As Integer = dtblVoiceMessageManagement.Columns.Count


            For Each drwVoiceMessageManagement As BusinessObject.dstZamanak.spr_VoiceRecords_Management_SelectRow In dtblVoiceMessageManagement.Rows

                If intAction = 1 Then

                    strResult &= ";n;;@;" & CStr(drwVoiceMessageManagement.Item(0))
                    For i As Integer = 1 To intColumnCount - 1
                        strResult &= ";@;" & CStr(drwVoiceMessageManagement.Item(i))
                    Next
                Else

                    strResult &= ";n;;@;" & CStr(drwVoiceMessageManagement.Item(0))
                    strResult &= ";@;" & CStr(drwVoiceMessageManagement.Item(1))

                    For i As Integer = 2 To intColumnCount - 1
                        strResult &= ";@;" & CStr(drwVoiceMessageManagement.Item(i)).ToLower.Replace(strFilter.ToLower, "<b><font color='#0F17FF'>" & strFilter & "</font></b>")
                    Next

                End If

            Next drwVoiceMessageManagement



            Return strResult

        Catch ex As Exception
            Return "E"
        End Try


    End Function

    <System.Web.Services.WebMethod()> Public Shared Function GetPageCount(strFilter As String) As Integer()

        Dim dtblUserLogin As BusinessObject.dstUser.spr_User_Login_SelectDataTable = CType(HttpContext.Current.Session("dtblUserLogin"), BusinessObject.dstUser.spr_User_Login_SelectDataTable)
        Dim drwUserLogin As BusinessObject.dstUser.spr_User_Login_SelectRow = dtblUserLogin.Rows(0)


        Dim intAction As Integer = 1

        If strFilter IsNot Nothing Then
            intAction = 2
        End If


        Dim tadpVoiceMessageCount As New BusinessObject.dstZamanakTableAdapters.spr_VoiceRecords_Count_SelectTableAdapter
        Dim dtblVoiceMessageCount As BusinessObject.dstZamanak.spr_VoiceRecords_Count_SelectDataTable = Nothing
        dtblVoiceMessageCount = tadpVoiceMessageCount.GetData(intAction, strFilter, drwUserLogin.ID)
        Dim drwVoiceMessageCount As BusinessObject.dstZamanak.spr_VoiceRecords_Count_SelectRow = dtblVoiceMessageCount.Rows(0)
        Dim intPageCount As Integer = Math.Ceiling(drwVoiceMessageCount.VoiceRecords / mdlGeneral.cnst_RowsCountInPage)
        Dim arrResult(1) As Integer
        arrResult(0) = drwVoiceMessageCount.VoiceRecords
        arrResult(1) = intPageCount
        Return arrResult

    End Function

#End Region

    Private Sub Bootstrap_Panel1_Panel_New_Click(sender As Object, e As System.EventArgs) Handles Bootstrap_Panel1.Panel_New_Click
        Response.Redirect("VoiceMessageFileNew.aspx")
        Return
    End Sub
End Class