Public Class BranchNew
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

         

        End If


    End Sub



    Private Sub Bootstrap_Panel1_Panel_Cancel_Click(sender As Object, e As System.EventArgs) Handles Bootstrap_Panel1.Panel_Cancel_Click
        Response.Redirect("BranchManagement.aspx")
        Return
    End Sub


    Private Sub Bootstrap_Panel1_Panel_Save_Click(sender As Object, e As System.EventArgs) Handles Bootstrap_Panel1.Panel_Save_Click

        Dim dtblUserLogin As BusinessObject.dstUser.spr_User_Login_SelectDataTable = CType(Session("dtblUserLogin"), BusinessObject.dstUser.spr_User_Login_SelectDataTable)
        Dim drwUserLogin As BusinessObject.dstUser.spr_User_Login_SelectRow = dtblUserLogin.Rows(0)


        Dim strBranchName As String = txtBranchName.Text.Trim
        Dim strBranchCode As String = txtBranchCode.Text.Trim
        Dim strBranchAddress As String = txtBranchAddress.Text.Trim
        Dim strBranchIP As String = txtBranchIP.Text.Trim
        Dim strBranchTel As String = txtTelephon.Text.Trim
        Dim intProvinceID As Integer = cmbProvince.SelectedValue

        Try
            Dim qryBranch As New BusinessObject.dstBranchTableAdapters.QueriesTableAdapter
            qryBranch.spr_Branch_Insert(strBranchCode, strBranchName, strBranchAddress, drwUserLogin.ID, strBranchIP, strBranchTel, intProvinceID)

        Catch ex As Exception
            Response.Redirect("BranchManagement.aspx?Save=NO")
            Return
        End Try

        Response.Redirect("BranchManagement.aspx?Save=OK")







    End Sub

 
    Protected Sub cmbProvince_DataBound(sender As Object, e As EventArgs) Handles cmbProvince.DataBound

        Dim li As New ListItem
        li.Text = "---"
        li.Value = -1
        cmbProvince.Items.Insert(0, li)


    End Sub
End Class