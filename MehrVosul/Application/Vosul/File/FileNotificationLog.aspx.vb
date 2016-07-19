Imports System.IO
Imports System.Collections.Generic
Imports System.Data
Imports System.Drawing
Imports System.Linq
Imports System.Text
Imports System.Web.UI.WebControls
Imports WkHtmlToXSharp

Public Class FileNotificationLog
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
        Bootstrap_Panel1.CanDisplay = True
        Bootstrap_Panel1.CanExcel = False
        Bootstrap_Panel1.Enable_Save_Client_Validate = True
        Bootstrap_Panel1.CanPDF = True


        If Not Session("intLoanID") Is Nothing Then
            Dim dtblLoan As BusinessObject.dstLoan.spr_Loan_SelectDataTable = Nothing
            Dim tadpLoan As New BusinessObject.dstLoanTableAdapters.spr_Loan_SelectTableAdapter

            dtblLoan = tadpLoan.GetData(2, CInt(Session("intLoanID")), -1, -1)

            If dtblLoan.Rows.Count > 0 Then
                Session("LoanNumber") = dtblLoan.First.LoanNumber.ToString()
                lblInnerPageTitle.Text = "شماره وام: " & dtblLoan.First.LoanNumber.ToString()
            End If

        Else
            Return
        End If


        If Page.IsPostBack = False Then


            Bootstrap_PersianDateTimePicker_From.GergorainDateTime = Date.Now.AddHours(-24).Date
            Bootstrap_PersianDateTimePicker_To.GergorainDateTime = Date.Now

            Bootstrap_PersianDateTimePicker_From.PickerLabel = "از"
            Bootstrap_PersianDateTimePicker_To.PickerLabel = "تا"


            GetNotificationLog()

        End If

        If hdnAction.Value.StartsWith("E") = True Then
            Dim intWarningNotificationLogID As Integer = CInt(hdnAction.Value.Split(";")(1))
            Session("intWarningNotificationLogID") = CObj(intWarningNotificationLogID)
            Response.Redirect("FileNotificationLogDetail.aspx")
        End If


        If hdnAction.Value.StartsWith("S") = True Then
            Dim intWarningNotificationLogID As Integer = CInt(hdnAction.Value.Split(";")(1))
            Session("intWarningNotificationLogID") = CObj(intWarningNotificationLogID)
            Response.Redirect("FileNotificationLogDetailSMS.aspx")
        End If

        Bootstrap_PersianDateTimePicker_From.ShowTimePicker = True
        Bootstrap_PersianDateTimePicker_To.ShowTimePicker = True


    End Sub


    Private Sub GetNotificationLog()

        Bootstrap_Panel1.ClearMessage()

        Dim tadpReport As New BusinessObject.dstWarningNotificationLogTableAdapters.spr_NotificationCharge_SelectTableAdapter
        Dim dtblReport As BusinessObject.dstWarningNotificationLog.spr_NotificationCharge_SelectDataTable = Nothing

        Dim dtFromDate As Date = Bootstrap_PersianDateTimePicker_From.GergorainDateTime

        Dim dteToDate As Date = Bootstrap_PersianDateTimePicker_To.GergorainDateTime


        If Not Session("intLoanID") Is Nothing Then


            dtblReport = tadpReport.GetData(dtFromDate, dteToDate, CInt(Session("intLoanID")))

        Else

            Exit Sub
        End If


        If dtblReport.Rows.Count > 0 Then

            divResult.Visible = True

            ''get total report
            Dim intCount As Integer = 0
            Dim intTotalCount As Integer = 0
            Dim dblTotalCharge As Double = 0

            Dim strHTMLTable As String = "<table border='1'   id='tblResults' dir='rtl' align='center' cellpadding='0' cellspacing='0'  width='100%'>" & _
                                      "<tr align='center' >"

            strHTMLTable = strHTMLTable & "<td align='center' style='font-weight:bold'>ردیف</td>" & _
                                          "<td align='center' style='font-weight:bold'>نوع اطلاع رسانی</td>"

            strHTMLTable = strHTMLTable & "<td align='center' style='font-weight:bold'>تعداد</td>"
            strHTMLTable = strHTMLTable & "<td align='center' style='font-weight:bold' >هزینه(ریال)</td></tr>"

            For Each drwReport As BusinessObject.dstWarningNotificationLog.spr_NotificationCharge_SelectRow In dtblReport.Rows

                intCount += 1
                Dim TbRow As New HtmlTableRow
                Dim TbCell As HtmlTableCell

                strHTMLTable = strHTMLTable & "<tr>"

                TbCell = New HtmlTableCell
                TbCell.InnerHtml = "<input type='hidden' id='hdnAmounts" & CStr(intCount) & "' >" & CStr(intCount)
                TbCell.Align = "center"
                TbCell.NoWrap = True
                TbRow.Cells.Add(TbCell)

                strHTMLTable = strHTMLTable & "<td align='center'>" & CStr(intCount) & "</td>"


                TbCell = New HtmlTableCell
                TbCell.InnerHtml = drwReport.NotificationType
                TbCell.NoWrap = True
                TbCell.Align = "center"
                TbRow.Cells.Add(TbCell)

                strHTMLTable = strHTMLTable & "<td align='center'>" & drwReport.NotificationType & "</td>"


                TbCell = New HtmlTableCell
                TbCell.InnerHtml = drwReport.ChargeCount
                intTotalCount += drwReport.ChargeCount
                TbCell.NoWrap = True
                TbCell.Align = "center"
                TbRow.Cells.Add(TbCell)

                strHTMLTable = strHTMLTable & "<td align='center'>" & drwReport.ChargeCount & "</td>"

                TbCell = New HtmlTableCell
                TbCell.InnerHtml = drwReport.Amount.ToString("N2")
                dblTotalCharge += drwReport.Amount
                TbCell.NoWrap = True
                TbCell.Align = "center"
                TbRow.Cells.Add(TbCell)


                strHTMLTable = strHTMLTable & "<td align='center'>" & drwReport.Amount.ToString("N2") & "</td>"

                strHTMLTable = strHTMLTable & "</tr>"

                tblResult.Rows.Add(TbRow)
            Next

            Dim TbSumRow As New HtmlTableRow
            Dim TbSumCell As HtmlTableCell

            TbSumCell = New HtmlTableCell
            TbSumCell.ColSpan = 2
            TbSumCell.InnerHtml = "مجموع"
            TbSumCell.Align = "center"
            TbSumCell.NoWrap = True
            TbSumRow.Cells.Add(TbSumCell)

            strHTMLTable = strHTMLTable & "<tr align='center' style='background-color:#C3C1C3'><td colspan = 2>مجموع</td>"

            TbSumCell = New HtmlTableCell
            TbSumCell.InnerHtml = intTotalCount
            TbSumCell.Align = "center"
            TbSumCell.NoWrap = True
            TbSumRow.Cells.Add(TbSumCell)
            strHTMLTable = strHTMLTable & "<td align='center'>" & intTotalCount.ToString("N0") & "</td>"



            TbSumCell = New HtmlTableCell
            TbSumCell.InnerHtml = dblTotalCharge.ToString("N2")
            TbSumCell.Align = "center"
            TbSumCell.NoWrap = True
            TbSumRow.Cells.Add(TbSumCell)

            strHTMLTable = strHTMLTable & "<td align='center'>" & dblTotalCharge.ToString("N2") & "</td></tr></table>"


            TbSumRow.BgColor = "#81b40d"
            TbSumRow.Style("font-weight") = "bold"
            tblResult.Rows.Add(TbSumRow)

            Session("FileNotificationLog") = strHTMLTable


        End If


    End Sub

    Private Sub Bootstrap_Panel1_Panel_Display_Click(sender As Object, e As System.EventArgs) Handles Bootstrap_Panel1.Panel_Display_Click


        GetNotificationLog()
      

    End Sub

    Private Sub Bootstrap_Panel1_Panel_PDF_Click(sender As Object, e As System.EventArgs) Handles Bootstrap_Panel1.Panel_PDF_Click

        If Not Session("FileNotificationLog") Is Nothing Then

            ''        Dim dtblUserLogin As BusinessObject.dstUser.spr_User_Login_SelectDataTable = CType(Session("dtblUserLogin"), BusinessObject.dstUser.spr_User_Login_SelectDataTable)
            ''        Dim drwUserLogin As BusinessObject.dstUser.spr_User_Login_SelectRow = dtblUserLogin.Rows(0)

            ''        Dim htmlToPdfConverter As New MultiplexingConverter
            ''        htmlToPdfConverter.ObjectSettings.Load.LoadErrorHandling = LoadErrorHandlingType.ignore
            ''        Dim s As String = "<html><body dir='rtl'><div style='font-family:b titr;text-align:center'>" & Session("FileNotificationLog").ToString() & _
            ''"</div></body></html>"

            ''        htmlToPdfConverter.ObjectSettings.Web.DefaultEncoding = "UTF-8"
            ''        Dim ba() As Byte = htmlToPdfConverter.Convert(s)


            ''        If File.Exists(Server.MapPath("") & "\Temp\Report" & drwUserLogin.ID.ToString() & ".pdf") Then
            ''            File.Delete(Server.MapPath("") & "\Temp\Report" & drwUserLogin.ID.ToString() & ".pdf")
            ''        End If

            ''        File.WriteAllBytes(Server.MapPath("") & "\Temp\Report" & drwUserLogin.ID.ToString() & ".pdf", ba)
            ''        Response.Clear()
            ''        Response.ContentType = "application/pdf"
            ''        Response.AppendHeader("Content-Disposition", "attachment; filename=FileNotificationLog.pdf")
            ''        Response.WriteFile(Server.MapPath("") & "\Temp\Report" & drwUserLogin.ID.ToString() & ".pdf")
            ''        Response.End()



            Dim objMakePDFReport As New clsMakePDFReport
            'strResTable = objMakePDFReport.MakeTableReport(dtblTheReport, UC_MakeFilter1.lstHeaders)

            Dim strLogoPath As String = Server.MapPath("~") & "\Images\System\MehrLogo.jpg"
            Dim rndVar As New Random

            Dim ba() As Byte = objMakePDFReport.MakePDFReport(Session("FileNotificationLog").ToString(), strLogoPath, "لیست هزینه های اطلاع رسانی")
            IO.File.WriteAllBytes(Server.MapPath("") & "\Temp\Report.pdf", ba)
            Response.Clear()
            Response.ContentType = "application/pdf"
            Response.AppendHeader("Content-Disposition", "attachment; filename=" & rndVar.Next(1, 15000).ToString & ".pdf")
            Response.TransmitFile(Server.MapPath("") & "\Temp\Report.pdf")
            Response.End()


        Else

            Bootstrap_Panel1.ShowMessage("لیستی برای نمایش وجود ندارد", True)

        End If


    End Sub


    Private Sub Bootstrap_Panel1_Panel_Up_Click(sender As Object, e As System.EventArgs) Handles Bootstrap_Panel1.Panel_Up_Click
        Response.Redirect("FileLoanDetail.aspx")
        Return
    End Sub



End Class