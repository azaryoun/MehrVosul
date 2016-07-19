Public Class FileNotificationLogDetailSMS
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        Page.Response.Cache.SetCacheability(HttpCacheability.NoCache)

        Bootstrap_Panel1.CanNew = False
        Bootstrap_Panel1.CanSave = False
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

        If Not Session("LoanNumber") Is Nothing Then

            lblInnerPageTitle.Text = "شماره وام: " & Session("LoanNumber").ToString()
        End If

        If Page.IsPostBack = False Then

            

            If Not Session("intWarningNotificationLogID") Is Nothing Then

                Dim tadpWarningNotificationLogDetail As New BusinessObject.dstWarningNotificationLogDetailTableAdapters.spr_WarningNotificationLogDetail_SelectTableAdapter
                Dim dtblWarningNotificationLogDetail As BusinessObject.dstWarningNotificationLogDetail.spr_WarningNotificationLogDetail_SelectDataTable = Nothing

                dtblWarningNotificationLogDetail = tadpWarningNotificationLogDetail.GetData(2, CInt(Session("intWarningNotificationLogID")))

                If dtblWarningNotificationLogDetail.Rows.Count <> 0 Then

                    Dim drwWarningNotificationLogDetail As BusinessObject.dstWarningNotificationLogDetail.spr_WarningNotificationLogDetail_SelectRow = dtblWarningNotificationLogDetail.Rows(0)

                    Bootstrap_Panel1.CanSave = False
                    txt_Msg.Enabled = False
                 
                    txt_Msg.Text = drwWarningNotificationLogDetail.strMessage
                    Select Case drwWarningNotificationLogDetail.SendStatus

                        Case 1

                            lblSendStatus.Text = "در حال ارسال"

                        Case 2
                            lblSendStatus.Text = "تحویل به مخابرات"

                        Case 3
                            lblSendStatus.Text = "رسیده به گوشی"
                        Case 4
                            lblSendStatus.Text = "تحویل به مخابرات"

                    End Select


                    If drwWarningNotificationLogDetail.IsDeliveryDateNull = False Then
                        lblSendStatus.Text = lblSendStatus.Text & "(" & mdlGeneral.GetPersianDate(drwWarningNotificationLogDetail.DeliveryDate) & ")"

                    ElseIf drwWarningNotificationLogDetail.IsSendDateNull = False Then

                        lblSendStatus.Text = lblSendStatus.Text & "(" & mdlGeneral.GetPersianDate(drwWarningNotificationLogDetail.SendDate) & ")"

                    Else

                        lblSendStatus.Text = lblSendStatus.Text & "(" & mdlGeneral.GetPersianDate(drwWarningNotificationLogDetail.STime) & ")"

                    End If


                End If



            End If



        End If

    End Sub

    Private Sub Bootstrap_Panel1_Panel_Up_Click(sender As Object, e As System.EventArgs) Handles Bootstrap_Panel1.Panel_Up_Click
        Response.Redirect("FileNotificationLog.aspx")
        Return
    End Sub
End Class