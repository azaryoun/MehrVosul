Public Class AdminSetting
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        Page.Response.Cache.SetCacheability(HttpCacheability.NoCache)

        Bootstrap_Panel1.CanNew = False
        Bootstrap_Panel1.CanSave = True
        Bootstrap_Panel1.CanDelete = False
        Bootstrap_Panel1.CanSearch = False
        Bootstrap_Panel1.CanCancel = False
        Bootstrap_Panel1.CanUp = False
        Bootstrap_Panel1.CanWizard = False
        Bootstrap_Panel1.CanConfirmRequest = False
        Bootstrap_Panel1.CanReject = False
        Bootstrap_Panel1.CanDisplay = False
        Bootstrap_Panel1.CanExcel = False
        Bootstrap_Panel1.Enable_Save_Client_Validate = True

        lblInnerPageTitle.Text = "پرکردن کادرهای قرمز رنگ، اجباری است."

        If Page.IsPostBack = False Then

            Dim tadpAdminSetting As New BusinessObject.dstSystemSettingTableAdapters.spr_AdminSetting_SelectTableAdapter
            Dim dtblAdminSetting As BusinessObject.dstSystemSetting.spr_AdminSetting_SelectDataTable = Nothing

            dtblAdminSetting = tadpAdminSetting.GetData()

            ViewState("AdminSettingID") = dtblAdminSetting.First.ID

            txtMassiveFilePeriod.Text = dtblAdminSetting.First.MassiveFilePeriod
            txtMassiveFileAmount.Text = dtblAdminSetting.First.MassiveFileAmount
            txtDueDateFilePeroid.Text = dtblAdminSetting.First.DueDateFilePeroid
            txtDueDateFileAmount.Text = dtblAdminSetting.First.DueDateFileAmount
            txtDueDateRecivedPeriod.Text = dtblAdminSetting.First.DueDateRecivedPeriod
            txtDueDateRecivedAmount.Text = dtblAdminSetting.First.DueDateRecivedAmount
            txtDeferredPeriod.Text = dtblAdminSetting.First.DeferredPeriod
            txtDeferredAmount.Text = dtblAdminSetting.First.DeferredAmount
            txtDoubtfulPaidPeriod.Text = dtblAdminSetting.First.DoubtfulPaidPeriod
            txtDoubtfulPaidAmount.Text = dtblAdminSetting.First.DoubtfulPaidAmount


        End If


        txtMassiveFilePeriod.Attributes.Add("onkeypress", "return numbersonly(event, false);")
        txtDueDateFilePeroid.Attributes.Add("onkeypress", "return numbersonly(event, false);")
        txtDueDateRecivedPeriod.Attributes.Add("onkeypress", "return numbersonly(event, false);")
        txtDeferredPeriod.Attributes.Add("onkeypress", "return numbersonly(event, false);")
        txtDoubtfulPaidPeriod.Attributes.Add("onkeypress", "return numbersonly(event, false);")
        txtMassiveFileAmount.Attributes.Add("onkeypress", "return numbersonly(event, false);")
        txtDueDateFileAmount.Attributes.Add("onkeypress", "return numbersonly(event, false);")
        txtDueDateRecivedAmount.Attributes.Add("onkeypress", "return numbersonly(event, false);")
        txtDeferredAmount.Attributes.Add("onkeypress", "return numbersonly(event, false);")
        txtDoubtfulPaidAmount.Attributes.Add("onkeypress", "return numbersonly(event, false);")


    End Sub



    Private Sub Bootstrap_Panel1_Panel_Save_Click(sender As Object, e As EventArgs) Handles Bootstrap_Panel1.Panel_Save_Click

        Dim dtblUserLogin As BusinessObject.dstUser.spr_User_Login_SelectDataTable = CType(Session("dtblUserLogin"), BusinessObject.dstUser.spr_User_Login_SelectDataTable)
        Dim drwUserLogin As BusinessObject.dstUser.spr_User_Login_SelectRow = dtblUserLogin.Rows(0)


        Try
            Dim qryAdminSetting As New BusinessObject.dstSystemSettingTableAdapters.QueriesTableAdapter

            Dim ID As Integer = CInt(ViewState("AdminSettingID"))

            Dim intMassiveFilePeriod As Integer = CInt(txtMassiveFilePeriod.Text)
            Dim intDueDateFilePeroid As Integer = CInt(txtDueDateFilePeroid.Text)
            Dim intDueDateRecivedPeriod As Integer = CInt(txtDueDateRecivedPeriod.Text)
            Dim intDeferredPeriod As Integer = CInt(txtDeferredPeriod.Text)
            Dim intDoubtfulPaidPeriod As Integer = CInt(txtDoubtfulPaidPeriod.Text)
            Dim dblMassiveFileAmount As Double = CDbl(txtMassiveFileAmount.Text)
            Dim dblDueDateFileAmount As Double = CDbl(txtDueDateFileAmount.Text)
            Dim dblDueDateRecivedAmount As Double = CDbl(txtDueDateRecivedAmount.Text)
            Dim dblDeferredAmount As Double = CDbl(txtDeferredAmount.Text)
            Dim dblDoubtfulPaidAmount As Double = CDbl(txtDoubtfulPaidAmount.Text)



            qryAdminSetting.spr_AdminSetting_Update(ID, intMassiveFilePeriod, intDueDateFilePeroid, intDueDateRecivedPeriod, intDeferredPeriod, intDoubtfulPaidPeriod, drwUserLogin.ID, dblMassiveFileAmount, dblDueDateFileAmount, dblDueDateRecivedAmount, dblDeferredAmount, dblDoubtfulPaidAmount)
            Bootstrap_Panel1.ShowMessage("تنظیمات پروند با موفقیت ثبت شد", False)

        Catch ex As Exception

            Bootstrap_Panel1.ShowMessage(ex.Message, True)
        End Try


    End Sub
End Class