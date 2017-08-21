Public Class ManagerInfoNew
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


            Dim tadpProvinceManager As New BusinessObject.dstBranchTableAdapters.spr_ProvinceMangagerInfoByID_SelectTableAdapter
            Dim dtblProvinceManager As BusinessObject.dstBranch.spr_ProvinceMangagerInfoByID_SelectDataTable = Nothing

            dtblProvinceManager = tadpProvinceManager.GetData(1)

            If dtblProvinceManager.Rows.Count > 0 Then
                ''ViewState("ManagerInfoProvinceID") = dtblProvinceManager.First.Fk_ProvinceID
                txtLastName.Text = dtblProvinceManager.First.ManagerName
                txtMobile.Text = dtblProvinceManager.First.MobileNO

            End If

        End If




    End Sub

    Protected Sub cmbProvince_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbProvince.SelectedIndexChanged

        Dim tadpProvinceManager As New BusinessObject.dstBranchTableAdapters.spr_ProvinceMangagerInfoByID_SelectTableAdapter
        Dim dtblProvinceManager As BusinessObject.dstBranch.spr_ProvinceMangagerInfoByID_SelectDataTable = Nothing

        dtblProvinceManager = tadpProvinceManager.GetData(cmbProvince.SelectedValue)

        If dtblProvinceManager.Rows.Count > 0 Then
            ''ViewState("ManagerInfoProvinceID") = dtblProvinceManager.First.Fk_ProvinceID
            txtLastName.Text = dtblProvinceManager.First.ManagerName
            txtMobile.Text = dtblProvinceManager.First.MobileNO

        Else
            txtLastName.Text = ""
            txtMobile.Text = ""


        End If



    End Sub

    Private Sub Bootstrap_Panel1_Panel_Save_Click(sender As Object, e As EventArgs) Handles Bootstrap_Panel1.Panel_Save_Click

        Try

            Dim queryProvince As New BusinessObject.dstBranchTableAdapters.QueriesTableAdapter

            Dim intProvinceID As Integer = cmbProvince.SelectedValue
            queryProvince.spr_ProvinceMangagerInfo_Delete(intProvinceID)

            queryProvince.spr_ProvinceMangagerInfo_Insert(intProvinceID, txtLastName.Text.Trim(), txtMobile.Text.Trim())


            Bootstrap_Panel1.ShowMessage("ثبت اطلاعات مدیر استان با موفقیت انجام شد", False)

        Catch ex As Exception
            Bootstrap_Panel1.ShowMessage(ex.Message, True)
        End Try


    End Sub
End Class