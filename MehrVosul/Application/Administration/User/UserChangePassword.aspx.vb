Public Class UserChangePassword
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

      
            If Session("intEditUserID") Is Nothing Then
                Response.Redirect("UserManagement.aspx")
                Return
            End If

            Dim intEditUserID As Integer = CInt(Session("intEditUserID"))

            Dim tadpUser As New BusinessObject.dstUserTableAdapters.spr_User_SelectTableAdapter
            Dim dtblUser As BusinessObject.dstUser.spr_User_SelectDataTable = Nothing
            dtblUser = tadpUser.GetData(intEditUserID)

            If dtblUser.Rows.Count = 0 Then
                Response.Redirect("UserManagement.aspx")
                Return
            End If
            Dim drwUser As BusinessObject.dstUser.spr_User_SelectRow = dtblUser.Rows(0)

            txtUsername.Text = drwUser.Username
            

        End If


    End Sub



    Private Sub Bootstrap_Panel1_Panel_Cancel_Click(sender As Object, e As System.EventArgs) Handles Bootstrap_Panel1.Panel_Cancel_Click
        Response.Redirect("UserManagement.aspx")
        Return
    End Sub


    Private Sub Bootstrap_Panel1_Panel_Save_Click(sender As Object, e As System.EventArgs) Handles Bootstrap_Panel1.Panel_Save_Click

        Dim intEditUserID As Integer = CInt(Session("intEditUserID"))

        Dim strPassword As String = txtPassword.Text.Trim


        Try
            Dim qryUser As New BusinessObject.dstUserTableAdapters.QueriesTableAdapter
            qryUser.spr_User_Password_Update(intEditUserID, strPassword)
        Catch ex As Exception
            Response.Redirect("UserManagement.aspx?ChangePass=NO")
            Return
        End Try

        Response.Redirect("UserManagement.aspx?ChangePass=OK")



    End Sub
End Class