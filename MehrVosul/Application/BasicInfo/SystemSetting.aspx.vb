Public Class SystemSetting
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



            Dim tadpSystemSetting As New BusinessObject.dstSystemSettingTableAdapters.spr_SystemSetting_SelectTableAdapter
            Dim dtblSystemSetting As BusinessObject.dstSystemSetting.spr_SystemSetting_SelectDataTable = Nothing
            dtblSystemSetting = tadpSystemSetting.GetData()

            Dim drwSystemSetting As BusinessObject.dstSystemSetting.spr_SystemSetting_SelectRow = dtblSystemSetting.Rows(0)

            With drwSystemSetting
                Session("intEditSystemSettingID") = drwSystemSetting.ID

                txtGatewayUserName.Text = drwSystemSetting.GatewayUsername
                txtCompanyGateway.Text = drwSystemSetting.GatewayCompany
                txtGatewayPassword.Text = drwSystemSetting.GatewayPassword
                txtGatewayNumber.Text = drwSystemSetting.GatewayNumber
                txtEmail.Text = drwSystemSetting.Email
                txtEmailUserName.Text = drwSystemSetting.EmailUsername
                txtEmailPassword.Text = drwSystemSetting.EmailPassword
                txtGatewayIP.Text = drwSystemSetting.GatewayIP
                txtTellNO.Text = If(.IsTelephoneNumberNull, "", drwSystemSetting.TelephoneNumber)
                txtEmailHost.Text = drwSystemSetting.EmailHost
                StartTimePicker.Hour = drwSystemSetting.UpdateTime.Hours
                StartTimePicker.Minute = drwSystemSetting.UpdateTime.Minutes
                cmbtryIntervalHour.SelectedValue = drwSystemSetting.tryIntervalHour
                cmbtryTime.SelectedValue = drwSystemSetting.tryTime
                DepositStartTimePicker.Hour = drwSystemSetting.UpdateTime_Deposit.Hours
                DepositStartTimePicker.Minute = drwSystemSetting.UpdateTime_Deposit.Minutes
                cmbDepositTryTime.SelectedValue = drwSystemSetting.tryTime_Deposit
                cmbDepositTryIntervalHour.SelectedValue = drwSystemSetting.tryIntervalHour_Deposit
                LoanStartTimePicker.Hour = drwSystemSetting.UpdateTime_Loan.Hours
                LoanStartTimePicker.Minute = drwSystemSetting.UpdateTime_Loan.Minutes
                cmbLoanTryTime.SelectedValue = drwSystemSetting.tryTime_Loan
                cmbLoanTryIntervalHour.SelectedValue = drwSystemSetting.tryIntervalHour_Loan
                txtVoiceSMSUID.Text = drwSystemSetting.VoiceSMSUID
                txtVoiceSMSToken.Text = drwSystemSetting.VoiceSMSToken
                chkHadiStatus.Checked = drwSystemSetting.HadiService
                chkVosoulStatus.Checked = drwSystemSetting.VosoulService

            End With

        End If



    End Sub



    


    Private Sub Bootstrap_Panel1_Panel_Save_Click(sender As Object, e As System.EventArgs) Handles Bootstrap_Panel1.Panel_Save_Click

        Dim intEditSystemSettingID As Integer = CInt(Session("intEditSystemSettingID"))


        Dim dtblUserLogin As BusinessObject.dstUser.spr_User_Login_SelectDataTable = CType(Session("dtblUserLogin"), BusinessObject.dstUser.spr_User_Login_SelectDataTable)
        Dim drwUserLogin As BusinessObject.dstUser.spr_User_Login_SelectRow = dtblUserLogin.Rows(0)


        Dim strNumberGateway As String = txtGatewayNumber.Text.Trim
        Dim strCompanyGateway As String = txtCompanyGateway.Text.Trim
        Dim strGatewayUserName As String = txtGatewayUserName.Text.Trim
        Dim strGatewayPassword As String = txtGatewayPassword.Text.Trim
        Dim strGatewayIP As String = txtGatewayIP.Text.Trim
        Dim strTell As String = txtTellNO.Text.Trim
        Dim strEmail As String = txtEmail.Text.Trim
        Dim strEmailHost As String = txtEmailHost.Text.Trim
        Dim strEmailUserName As String = txtEmailUserName.Text.Trim
        Dim strEmailPassword As String = txtEmailPassword.Text.Trim
        Dim timeUpdateTime As TimeSpan = TimeSpan.Parse(StartTimePicker.Value)
        Dim inttryTime As Integer = CInt(cmbtryTime.SelectedValue)
        Dim inttryIntervalHour As Integer = CInt(cmbtryIntervalHour.SelectedValue)
        Dim DepositTimeUpdateTime As TimeSpan = TimeSpan.Parse(DepositStartTimePicker.Value)
        Dim intDeposittryTime As Integer = CInt(cmbDepositTryTime.SelectedValue)
        Dim intDepositTryIntervalHour As Integer = CInt(cmbDepositTryIntervalHour.SelectedValue)
        Dim LoanTimeUpdateTime As TimeSpan = TimeSpan.Parse(LoanStartTimePicker.Value)
        Dim intLoanbtryTime As Integer = CInt(cmbLoanTryTime.SelectedValue)
        Dim intLoanTryIntervalHour As Integer = CInt(cmbLoanTryIntervalHour.SelectedValue)
        Dim strVoiceSMSUID As String = txtVoiceSMSUID.Text.Trim()
        Dim strVoiceSMSToken As String = txtVoiceSMSToken.Text.Trim()

        Dim blnVosoulStatus As Boolean = chkVosoulStatus.Checked
        Dim blnHadiStatus As Boolean = chkHadiStatus.Checked
        Dim blnHadiStatusLoan As Boolean = chkHadiStatusLoan.Checked

        Try
            Dim qrySystemSetting As New BusinessObject.dstSystemSettingTableAdapters.QueriesTableAdapter
            qrySystemSetting.spr_SystemSetting_Update(intEditSystemSettingID, strNumberGateway, strCompanyGateway, strGatewayUserName, strGatewayPassword, strGatewayIP, strEmailUserName, strEmailHost, strEmailPassword, strEmail, strTell, timeUpdateTime, inttryTime, inttryIntervalHour, drwUserLogin.ID, DepositTimeUpdateTime, intDeposittryTime, intDepositTryIntervalHour, LoanTimeUpdateTime, intLoanbtryTime, intLoanTryIntervalHour, strVoiceSMSUID, strVoiceSMSToken, blnVosoulStatus, blnHadiStatus, blnHadiStatusLoan)
            Bootstrap_Panel1.ShowMessage("تنظیمات سیستم با موفقیت تغییر کرد", False)
            Return

        Catch ex As Exception
            Bootstrap_Panel1.ShowMessage("در تغییر تنظیمات سیستمی خطا رخ داد: " & ex.Message, True)
            Return
        End Try





    End Sub
End Class