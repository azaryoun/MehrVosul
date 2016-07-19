Public Class FileNotificationLogDetail
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
        Bootstrap_Panel1.CanPDF = True
        Bootstrap_Panel1.Enable_Save_Client_Validate = True

        If Not Session("LoanNumber") Is Nothing Then

            lblInnerPageTitle.Text = "شماره وام: " & Session("LoanNumber").ToString()
        End If

        If Page.IsPostBack = False Then

            If Request.QueryString("Save") IsNot Nothing AndAlso Request.QueryString("Save") = "OK" Then
                Bootstrap_Panel1.ShowMessage("جزئیات با موفقیت ذخیره شد", False)

            ElseIf Request.QueryString("Save") IsNot Nothing AndAlso Request.QueryString("Save") = "NO" Then
                Bootstrap_Panel1.ShowMessage("در فرآیند ذخیره جزئیات خطا رخ داده است", True)
               
            End If



            Bootstrap_PersianDateTimePicker_From.GergorainDateTime = Date.Now


            Bootstrap_PersianDateTimePicker_From.PickerLabel = "تاریخ ارسال"
          

            If Not Session("intWarningNotificationLogID") Is Nothing Then

                Dim tadpWarningNotificationLogDetail As New BusinessObject.dstWarningNotificationLogDetailTableAdapters.spr_WarningNotificationLogDetail_SelectTableAdapter
                Dim dtblWarningNotificationLogDetail As BusinessObject.dstWarningNotificationLogDetail.spr_WarningNotificationLogDetail_SelectDataTable = Nothing

                dtblWarningNotificationLogDetail = tadpWarningNotificationLogDetail.GetData(2, CInt(Session("intWarningNotificationLogID")))

                If dtblWarningNotificationLogDetail.Rows.Count <> 0 Then

                    Dim drwWarningNotificationLogDetail As BusinessObject.dstWarningNotificationLogDetail.spr_WarningNotificationLogDetail_SelectRow = dtblWarningNotificationLogDetail.Rows(0)

                    If drwWarningNotificationLogDetail.SendStatus = 5 Then

                        Bootstrap_Panel1.CanSave = True

                        txt_Msg.Enabled = True
                        txtLetterNO.Enabled = True
                        chkbxSent.Disabled = False

                    ElseIf drwWarningNotificationLogDetail.SendStatus = 6 Then

                        divDate.Visible = False
                        Bootstrap_PersianDateTimePicker_From.Visible = False

                        Bootstrap_Panel1.CanSave = False
                        txt_Msg.Enabled = False
                        txtLetterNO.Enabled = False
                        chkbxSent.Visible = False

                        lblStatus.Visible = True
                        divchkbxSent.Visible = False
                        lblStatus.Text = "ارسال شده(" & mdlGeneral.GetPersianDate(drwWarningNotificationLogDetail.SendDate) & ")"

                    ElseIf drwWarningNotificationLogDetail.SendStatus = 7 Then

                        divDate.Visible = False
                        Bootstrap_PersianDateTimePicker_From.Visible = False

                        Bootstrap_Panel1.CanSave = False
                        txt_Msg.Enabled = False
                        txtLetterNO.Enabled = False
                        chkbxSent.Visible = False

                        lblStatus.Visible = True
                        divchkbxSent.Visible = False
                        lblStatus.Text = "تحویل شده(" & mdlGeneral.GetPersianDate(drwWarningNotificationLogDetail.DeliveryDate) & ")"

                    End If

                    txt_Msg.Text = drwWarningNotificationLogDetail.strMessage

                    Session("WarningNotificationLogMessage") = drwWarningNotificationLogDetail.strMessage

                    If drwWarningNotificationLogDetail.IsSendDateNull = False Then

                        Bootstrap_PersianDateTimePicker_From.GergorainDateTime = drwWarningNotificationLogDetail.SendDate


                    End If


                    If drwWarningNotificationLogDetail.IsRemarksNull = False Then

                        txtLetterNO.Text = drwWarningNotificationLogDetail.Remarks

                    End If

                End If



            End If



        End If



    End Sub

    Private Sub Bootstrap_Panel1_Panel_PDF_Click(sender As Object, e As System.EventArgs) Handles Bootstrap_Panel1.Panel_PDF_Click




        If Not Session("WarningNotificationLogMessage") Is Nothing Then

            Dim strHTMLTable As String = "<table  id='tblResults' dir='rtl' align='center' cellpadding='0' cellspacing='0'  width='100%'>" & _
                                 "<tr align='center' >"

            strHTMLTable = strHTMLTable & "<tr><td align='right' style='white-space:pre-wrap'>" & Session("WarningNotificationLogMessage").ToString() & "</td></tr>"

            Dim objMakePDFReport As New clsMakePDFReport
          
            Dim strLogoPath As String = Server.MapPath("~") & "\Images\System\MehrLogo.jpg"
            Dim rndVar As New Random

            Dim ba() As Byte = objMakePDFReport.MakePDFReport(strHTMLTable, strLogoPath, "جزئیات نامه")
            IO.File.WriteAllBytes(Server.MapPath("") & "\Temp\Report.pdf", ba)
            Response.Clear()
            Response.ContentType = "application/pdf"
            Response.AppendHeader("Content-Disposition", "attachment; filename=" & rndVar.Next(1, 15000).ToString & ".pdf")
            Response.TransmitFile(Server.MapPath("") & "\Temp\Report.pdf")
            Response.End()



        Else

            Bootstrap_Panel1.ShowMessage("متن برای نمایش وجود ندارد", True)

        End If

    End Sub

    Private Sub Bootstrap_Panel1_Panel_Save_Click(sender As Object, e As System.EventArgs) Handles Bootstrap_Panel1.Panel_Save_Click

        Try

            Dim qryWarningNotificationLogDetail As New BusinessObject.dstWarningNotificationLogDetailTableAdapters.QueriesTableAdapter

            Dim intWarningNotificationLogDetail As Integer = CInt(Session("intWarningNotificationLogID"))
            Dim strRemarks As String = txtLetterNO.Text.Trim
            Dim strMessage As String = txt_Msg.Text
            Dim intStatus As Int16 = 5
            If chkbxSent.Checked = True Then
                intStatus = 6
            End If


            qryWarningNotificationLogDetail.spr_WarningNotificationLogDetail_Status_Update(intWarningNotificationLogDetail, strRemarks, intStatus, strMessage, Bootstrap_PersianDateTimePicker_From.GergorainDateTime)


        Catch ex As Exception
            Response.Redirect("FileNotificationLogDetail.aspx?Save=NO")

            Return
        End Try

        Response.Redirect("FileNotificationLogDetail.aspx?Save=OK")
        Return




    End Sub

    Private Sub Bootstrap_Panel1_Panel_Up_Click(sender As Object, e As System.EventArgs) Handles Bootstrap_Panel1.Panel_Up_Click
        Response.Redirect("../Reports/WarningNotificationLogDetailSummaryReport.aspx")
        Return
    End Sub
End Class