Public Class NotificationTarrifEdit
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

            If Session("intEditNotificationTarrifID") Is Nothing Then
                Response.Redirect("NotificationTarrifManagement.aspx")
                Return
            End If

            Dim intEditNotificationTarrifID As Integer = CInt(Session("intEditNotificationTarrifID"))


            Dim tadpTarrif As New BusinessObject.dstTarrifTableAdapters.spr_NotificationTarif_SelectTableAdapter
            Dim dtblTarrif As BusinessObject.dstTarrif.spr_NotificationTarif_SelectDataTable = Nothing

            dtblTarrif = tadpTarrif.GetData(2, intEditNotificationTarrifID)

            cmbNotification.SelectedValue = dtblTarrif.First.NotificationTypeID
            txtAmount.Text = dtblTarrif.First.Amount.ToString("N2")

            Bootstrap_PersianDateTimePicker_From.GergorainDateTime = dtblTarrif.First.FromDate
            Bootstrap_PersianDateTimePicker_To.GergorainDateTime = dtblTarrif.First.ToDate

            Bootstrap_PersianDateTimePicker_From.PickerLabel = "از"
            Bootstrap_PersianDateTimePicker_To.PickerLabel = "تا"


        End If


        Bootstrap_PersianDateTimePicker_From.ShowTimePicker = True
        Bootstrap_PersianDateTimePicker_To.ShowTimePicker = True
        txtAmount.Attributes.Add("onkeypress", "return numbersonly(event, false);")


    End Sub

    Private Sub Bootstrap_Panel1_Panel_Cancel_Click(sender As Object, e As System.EventArgs) Handles Bootstrap_Panel1.Panel_Cancel_Click
        Response.Redirect("NotificationTarrifManagement.aspx")
        Return
    End Sub

    Private Sub Bootstrap_Panel1_Panel_Save_Click(sender As Object, e As System.EventArgs) Handles Bootstrap_Panel1.Panel_Save_Click

        Dim dtblUserLogin As BusinessObject.dstUser.spr_User_Login_SelectDataTable = CType(Session("dtblUserLogin"), BusinessObject.dstUser.spr_User_Login_SelectDataTable)
        Dim drwUserLogin As BusinessObject.dstUser.spr_User_Login_SelectRow = dtblUserLogin.Rows(0)



        Try

            Dim intEditNotificationTarrifID As Integer = CInt(Session("intEditNotificationTarrifID"))

            Dim intNotificationTypeID As Integer = CInt(cmbNotification.SelectedValue)
            Dim dblAmount As Double = CDbl(txtAmount.Text)
            Dim dtFromDate As Date = Bootstrap_PersianDateTimePicker_From.GergorainDateTime
            Dim dteToDate As Date = Bootstrap_PersianDateTimePicker_To.GergorainDateTime

            Dim qryTarrif As New BusinessObject.dstTarrifTableAdapters.QueriesTableAdapter
            qryTarrif.spr_NotificationTarif_Update(intEditNotificationTarrifID, intNotificationTypeID, dtFromDate, dteToDate, dblAmount)



        Catch ex As Exception
            Response.Redirect("NotificationTarrifManagement.aspx?Edit=NO")
            Return
        End Try

        Response.Redirect("NotificationTarrifManagement.aspx?Edit=OK")




    End Sub
End Class