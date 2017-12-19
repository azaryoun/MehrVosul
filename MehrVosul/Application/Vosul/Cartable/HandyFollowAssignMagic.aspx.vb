Public Class HandyFollowAssignMagic
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



        End If
        txtAssignDay.Attributes.Add("onkeypress", "return numbersonly(event, false);")

    End Sub

    Private Sub Bootstrap_Panel1_Panel_Cancel_Click(sender As Object, e As EventArgs) Handles Bootstrap_Panel1.Panel_Cancel_Click

        Response.Redirect("HandyFollowManagement.aspx")

    End Sub

    Private Sub Bootstrap_Panel1_Panel_Save_Click(sender As Object, e As EventArgs) Handles Bootstrap_Panel1.Panel_Save_Click

        Try

            Dim dtblUserLogin As BusinessObject.dstUser.spr_User_Login_SelectDataTable = CType(Session("dtblUserLogin"), BusinessObject.dstUser.spr_User_Login_SelectDataTable)
            Dim drwUserLogin As BusinessObject.dstUser.spr_User_Login_SelectRow = dtblUserLogin.Rows(0)

            Dim qryHandyFollowassignUpdate As New BusinessObject.dstHandyFollowTableAdapters.QueriesTableAdapter

            Dim intDay As Integer = CInt(txtAssignDay.Text)
            qryHandyFollowassignUpdate.spr_HandyFollowAssignMagic_Update(drwUserLogin.FK_BrnachID, intDay)

            Bootstrap_Panel1.ShowMessage("پرونده های مورد نظر از حالت تخصیص خارج شد", False)

        Catch ex As Exception

            Bootstrap_Panel1.ShowMessage("در ذخیره خطا رخ داده است", True)

        End Try


    End Sub
End Class