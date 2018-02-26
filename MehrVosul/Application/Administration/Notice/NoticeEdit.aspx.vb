Public Class NoticeEdit
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

            If drwUserLogin.IsDataAdmin = False AndAlso drwUserLogin.IsDataUserAdmin = False Then

                Bootstrap_Panel1.CanSave = False


            End If


            Dim intNoticeID As Integer = CInt(Session("intNoticeID"))


            Dim tadpNotice As New BusinessObject.dstNoticeTableAdapters.spr_Notice_SelectTableAdapter
            Dim dtblNotice As BusinessObject.dstNotice.spr_Notice_SelectDataTable = Nothing
            dtblNotice = tadpNotice.GetData(1, intNoticeID, -1)


            If dtblNotice.Rows.Count = 0 Then
                Response.Redirect("NoticeManagement.aspx")
                Return
            End If

            Dim drwNotice As BusinessObject.dstNotice.spr_Notice_SelectRow = dtblNotice.Rows(0)
            txtTitle.Text = drwNotice.Subject
            txtNoticeCode.Text = drwNotice.NoticeCode
            txtDesc.Text = drwNotice.Description
            chkActive.Checked = drwNotice.IsActive
            chkPublic.Checked = drwNotice.IsPublic
            If drwNotice.HasFile = True Then
                ViewState("FileName") = drwNotice.FileName
                lbtnNoticeFile.Visible = True
            End If



        End If
    End Sub

    Protected Sub lbtnNoticeFile_Click(sender As Object, e As EventArgs) Handles lbtnNoticeFile.Click

        If Not ViewState("FileName") Is Nothing Then
            If IO.File.Exists(Server.MapPath("~") & "\Application\Administration\Notice\Temp\" & ViewState("FileName").ToString()) = True Then
                Response.Clear()
                '' Response.ContentType = "application/excel"
                Response.AppendHeader("Content-Disposition", "attachment; filename=" & ViewState("FileName").ToString())
                Response.WriteFile(Server.MapPath("~") & "\Application\Administration\Notice\Temp\" & ViewState("FileName").ToString(), True)
                Response.End()
            Else
                Bootstrap_Panel1.ShowMessage(".فايل ذخیره نشده است ", True)
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
            qryNotice.spr_Notice_Update(strTitle, strNoticeCode, strDesc, blnHasFile, drwUserLogin.ID, blnIsPublic, drwUserLogin.Fk_ProvinceID, CInt(Session("intNoticeID")), blnActive, strFileName)


        Catch ex As Exception

            Response.Redirect("NoticeManagement.aspx?Edit=NO")
            Return

        End Try

        Response.Redirect("NoticeManagement.aspx?Edit=OK")
    End Sub
End Class