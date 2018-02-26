Public Class NoticeNew
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Page.Response.Cache.SetCacheability(HttpCacheability.NoCache)

        Bootstrap_Panel1.CanNew = False
        Bootstrap_Panel1.CanSave = True
        Bootstrap_Panel1.CanDelete = False
        Bootstrap_Panel1.CanSearch = False
        Bootstrap_Panel1.CanCancel = True
        Bootstrap_Panel1.CanUp = False
        Bootstrap_Panel1.CanWizard = False
        Bootstrap_Panel1.CanConfirmRequest = False
        Bootstrap_Panel1.CanReject = False
        Bootstrap_Panel1.CanDisplay = False
        Bootstrap_Panel1.CanExcel = False
        Bootstrap_Panel1.Enable_Save_Client_Validate = True

        lblInnerPageTitle.Text = "پرکردن کادرهای قرمز رنگ، اجباری است."

        If Page.IsPostBack = False Then

            Dim dtblUserLogin As BusinessObject.dstUser.spr_User_Login_SelectDataTable = CType(Session("dtblUserLogin"), BusinessObject.dstUser.spr_User_Login_SelectDataTable)
            Dim drwUserLogin As BusinessObject.dstUser.spr_User_Login_SelectRow = dtblUserLogin.Rows(0)



            If drwUserLogin.IsDataAdmin = True Then

                chkPublic.Visible = True

            End If

        End If

    End Sub

    Private Sub Bootstrap_Panel1_Panel_Cancel_Click(sender As Object, e As EventArgs) Handles Bootstrap_Panel1.Panel_Cancel_Click
        Response.Redirect("NoticeManagement.aspx")
        Return
    End Sub

    Private Sub Bootstrap_Panel1_Panel_Save_Click(sender As Object, e As EventArgs) Handles Bootstrap_Panel1.Panel_Save_Click

        Try
            Dim dtblUserLogin As BusinessObject.dstUser.spr_User_Login_SelectDataTable = CType(Session("dtblUserLogin"), BusinessObject.dstUser.spr_User_Login_SelectDataTable)
            Dim drwUserLogin As BusinessObject.dstUser.spr_User_Login_SelectRow = dtblUserLogin.Rows(0)



            Dim blnHasFile As Boolean = False
            Dim strFileName As String = ""
            If fleNoticeFile.PostedFile IsNot Nothing AndAlso fleNoticeFile.PostedFile.ContentLength <> 0 Then
                ''If fleUserPhoto.PostedFile.ContentType.ToLower.IndexOf("png") = -1 Then
                ''    Bootstrap_Panel1.ShowMessage("فرمت تصویر باید png باشد", True)
                ''    Return
                ''End If

                Reset()

                If IO.File.Exists(Server.MapPath("~") & "\Application\Administration\Notice\Temp\" & fleNoticeFile.PostedFile.FileName) = True Then
                    IO.File.Delete(Server.MapPath("~") & "\Application\Administration\Notice\Temp\" & fleNoticeFile.PostedFile.FileName)
                End If

                fleNoticeFile.PostedFile.SaveAs(Server.MapPath("~") & "\Application\Administration\Notice\Temp\" & fleNoticeFile.PostedFile.FileName)

                ''save file
                blnHasFile = True
                strFileName = fleNoticeFile.PostedFile.FileName
            End If

            Dim strTitle As String = txtTitle.Text.Trim()
            Dim strNoticeCode As String = txtNoticeCode.Text.Trim()
            Dim strDesc As String = txtDesc.Text
            Dim blnActive As Boolean = chkActive.Checked
            Dim blnIsPublic As Boolean = True
            If chkPublic.Checked = True Then
                blnIsPublic = True
            End If
            Dim qryNotice As New BusinessObject.dstNoticeTableAdapters.QueriesTableAdapter
            Dim intNoticeID As Integer = qryNotice.spr_Notice_Insert(strTitle, strNoticeCode, strDesc, blnHasFile, drwUserLogin.ID, blnIsPublic, drwUserLogin.Fk_ProvinceID, blnActive, strFileName)


        Catch ex As Exception

            Response.Redirect("NoticeManagement.aspx?Save=NO")
            Return

        End Try

        Response.Redirect("NoticeManagement.aspx?Save=OK")


    End Sub
End Class