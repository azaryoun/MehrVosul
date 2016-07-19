Public Class CurrentUserChangePassword
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Page.Response.Cache.SetCacheability(HttpCacheability.NoCache)

        Bootstrap_Panel1.CanNew = False
        Bootstrap_Panel1.CanSave = True
        Bootstrap_Panel1.CanDelete = False
        Bootstrap_Panel1.CanSearch = False
        Bootstrap_Panel1.CanCancel = False
        Bootstrap_Panel1.CanUp = True
        Bootstrap_Panel1.CanWizard = False
        Bootstrap_Panel1.CanConfirmRequest = False
        Bootstrap_Panel1.CanReject = False
        Bootstrap_Panel1.CanDisplay = False
        Bootstrap_Panel1.CanExcel = False
        Bootstrap_Panel1.Enable_Save_Client_Validate = True

        lblInnerPageTitle.Text = "پرکردن کادرهای قرمز رنگ، اجباری است."

        If Page.IsPostBack = False Then


            If Page.IsPostBack = False Then
                ViewState("BackPage") = Request.ServerVariables("HTTP_REFERER")
            End If


            Dim dtblUserLogin As BusinessObject.dstUser.spr_User_Login_SelectDataTable = CType(Session("dtblUserLogin"), BusinessObject.dstUser.spr_User_Login_SelectDataTable)
            Dim drwUserLogin As BusinessObject.dstUser.spr_User_Login_SelectRow = dtblUserLogin.Rows(0)
            txtUsername.Text = drwUserLogin.Username


        End If


    End Sub



    Private Sub Bootstrap_Panel1_Panel_Up_Click(sender As Object, e As System.EventArgs) Handles Bootstrap_Panel1.Panel_Up_Click
        Response.Redirect(ViewState("BackPage"))
        Return
    End Sub


    Private Sub Bootstrap_Panel1_Panel_Save_Click(sender As Object, e As System.EventArgs) Handles Bootstrap_Panel1.Panel_Save_Click

        Dim dtblUserLogin As BusinessObject.dstUser.spr_User_Login_SelectDataTable = CType(Session("dtblUserLogin"), BusinessObject.dstUser.spr_User_Login_SelectDataTable)
        Dim drwUserLogin As BusinessObject.dstUser.spr_User_Login_SelectRow = dtblUserLogin.Rows(0)

        Dim strCurrentPassword As String = txtCurrentPassword.Text.Trim


        Dim tadpCheckLogin As New BusinessObject.dstUserTableAdapters.spr_User_Login_SelectTableAdapter
        Dim dtblCheckLogin As BusinessObject.dstUser.spr_User_Login_SelectDataTable = Nothing
        dtblCheckLogin = tadpCheckLogin.GetData(drwUserLogin.Username, strCurrentPassword)
        If dtblCheckLogin.Rows.Count = 0 Then
            Bootstrap_Panel1.ShowMessage("کلمه عبور فعلی صحیح نمی باشد", True)
            Return
        End If



        Dim strPassword As String = txtNewPassword.Text


        Try
            Dim qryUser As New BusinessObject.dstUserTableAdapters.QueriesTableAdapter
            qryUser.spr_User_Password_Update(drwUserLogin.ID, strPassword)
            Bootstrap_Panel1.ShowMessage("کلمه عبور شما با موفقیت تغییر کرد", False)
            Return
        Catch ex As Exception
            Bootstrap_Panel1.ShowMessage("در تغییر کلمه عبور خطا رخ داد: " & ex.Message, True)
            Return
        End Try

        


    End Sub
End Class