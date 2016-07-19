Imports System.IO
Imports System.Collections.Generic
Imports System.Data
Imports System.Drawing
Imports System.Linq
Imports System.Text
Imports System.Web.UI.WebControls
Imports WkHtmlToXSharp
Public Class WarningNotificationLogDetailSummaryReport
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



        If Page.IsPostBack = False Then

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

            Bootstrap_PersianDateTimePicker_From.GergorainDateTime = Date.Now.AddDays(-3).Date
            Bootstrap_PersianDateTimePicker_To.GergorainDateTime = Date.Now

            Bootstrap_PersianDateTimePicker_From.PickerLabel = "از"
            Bootstrap_PersianDateTimePicker_To.PickerLabel = "تا"



        End If

        If hdnAction.Value.StartsWith("E") = True Then
            Dim intWarningNotificationLogID As Integer = CInt(hdnAction.Value.Split(";")(1))
            Session("intWarningNotificationLogID") = CObj(intWarningNotificationLogID)
            Response.Redirect("../File/FileNotificationLogDetail.aspx")
        End If


        Bootstrap_PersianDateTimePicker_From.ShowTimePicker = True
        Bootstrap_PersianDateTimePicker_To.ShowTimePicker = True


    End Sub

    Private Sub Bootstrap_Panel1_Panel_Display_Click(sender As Object, e As System.EventArgs) Handles Bootstrap_Panel1.Panel_Display_Click

        Bootstrap_Panel1.ClearMessage()

        
        Dim tadpReport As New BusinessObject.dstReportTableAdapters.spr_Report_WarningNotificationLogDetail_Summary_SelectTableAdapter
        Dim dtblReport As BusinessObject.dstReport.spr_Report_WarningNotificationLogDetail_Summary_SelectDataTable = Nothing


        Dim dtFromDate As Date = Bootstrap_PersianDateTimePicker_From.GergorainDateTime
        Dim dteToDate As Date = Bootstrap_PersianDateTimePicker_To.GergorainDateTime

        Dim intNotificationTypeID As Integer = CInt(cmbNotificationType.SelectedValue)
        Dim intLoanID As Integer = CInt(Session("intLoanID"))

        dtblReport = tadpReport.GetData(dtFromDate, dteToDate, intNotificationTypeID, intLoanID)

        If dtblReport.Rows.Count > 0 Then

            divResult.Visible = True

            ''get total report
            Dim intCount As Integer = 0
            Dim intTotalCount As Integer = 0


            Dim strHTMLTable As String = "<table border='1'   id='tblResults' dir='rtl' align='center' cellpadding='0' cellspacing='0'  width='100%'>" & _
                                   "<tr align='center' >"

            strHTMLTable = strHTMLTable &  "<td align='center' style='font-weight:bold'>ردیف</td>" & _
                                          "<td align='center' style='font-weight:bold'>تاریخ</td>"

            strHTMLTable = strHTMLTable & "<td align='center' style='font-weight:bold'>متن ارسالی</td>"
            strHTMLTable = strHTMLTable & "<td align='center' style='font-weight:bold' >وضعیت</td></tr>"

            For Each drwReport As BusinessObject.dstReport.spr_Report_WarningNotificationLogDetail_Summary_SelectRow In dtblReport.Rows

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
                TbCell.InnerHtml = mdlGeneral.GetPersianDate(drwReport.theDay)
                TbCell.NoWrap = True
                TbCell.Align = "center"
                TbRow.Cells.Add(TbCell)

                strHTMLTable = strHTMLTable & "<td align='center'>" & mdlGeneral.GetPersianDate(drwReport.theDay) & "</td>"




                TbCell = New HtmlTableCell
                TbCell.InnerHtml = drwReport.strMessage
                TbCell.NoWrap = False
                TbCell.Align = "center"
                TbRow.Cells.Add(TbCell)

                strHTMLTable = strHTMLTable & "<td align='center'>" & drwReport.strMessage & "</td>"


                TbCell = New HtmlTableCell
                TbCell.InnerHtml = "<a ID='lnkbtnSponsor' href='#'  onclick= btnStatus_ClientClick(" & drwReport.ID & ")>" & drwReport.SendStatus & "</a>"
                TbCell.NoWrap = False
                TbCell.Align = "center"
                TbRow.Cells.Add(TbCell)


                strHTMLTable = strHTMLTable & "<td align='center'>" & drwReport.SendStatus & "</td>"
                strHTMLTable = strHTMLTable & "</tr>"

                tblResult.Rows.Add(TbRow)

            Next

            strHTMLTable = strHTMLTable & "</table>"


            Session("WarningNotificationLogDetailSummaryPDF") = strHTMLTable
            Session("WarningNotificationLogDetailSummaryReport") = dtblReport

        Else

            divResult.Visible = False


        End If

    End Sub

    Private Sub Bootstrap_Panel1_Panel_Excel_Click(sender As Object, e As System.EventArgs) Handles Bootstrap_Panel1.Panel_Excel_Click

        Try
            If Session("WarningNotificationLogDetailSummaryReport") IsNot Nothing Then
                Dim dtblUserLogin As BusinessObject.dstUser.spr_User_Login_SelectDataTable = CType(Session("dtblUserLogin"), BusinessObject.dstUser.spr_User_Login_SelectDataTable)
                Dim drwUserLogin As BusinessObject.dstUser.spr_User_Login_SelectRow = dtblUserLogin.Rows(0)


                Dim strPath As String = Server.MapPath("") & "\TempFile\" & drwUserLogin.ID & "\"
                Dim FileName As String = "WarningNotificationLogDetailSummaryReport-" & drwUserLogin.ID.ToString()

                Dim tblCSVResult As DataTable = Session("WarningNotificationLogDetailSummaryReport")
                If tblCSVResult.Rows.Count > 0 Then
                    If Not System.IO.Directory.Exists(strPath) Then
                        System.IO.Directory.CreateDirectory(strPath)
                    End If

                    Dim clsCSVWriter As New clsCSVWriter
                    Using strWriter As StreamWriter = New StreamWriter(strPath & FileName)
                        clsCSVWriter.WriteDataTable(tblCSVResult, FileName, strWriter, True)
                    End Using
                Else
                    Bootstrap_Panel1.ShowMessage("امکان انتقال گزارش به فایل اکسل وجود ندارد", False)
                    Session("WarningNotificationLogDetailSummaryReport") = Nothing
                End If
            Else
                Bootstrap_Panel1.ShowMessage("امکان انتقال گزارش به فایل اکسل وجود ندارد", False)
                Session("WarningNotificationLogDetailSummaryReport") = Nothing
            End If
        Catch ex As Exception
            Bootstrap_Panel1.ShowMessage("امکان انتقال گزارش به فایل اکسل وجود ندارد", False)
            Session("WarningNotificationLogDetailSummaryReport") = Nothing
        End Try

    End Sub

    Private Sub Bootstrap_Panel1_Panel_PDF_Click(sender As Object, e As System.EventArgs) Handles Bootstrap_Panel1.Panel_PDF_Click
        If Not Session("WarningNotificationLogDetailSummaryPDF") Is Nothing Then

            Dim objMakePDFReport As New clsMakePDFReport
            'strResTable = objMakePDFReport.MakeTableReport(dtblTheReport, UC_MakeFilter1.lstHeaders)

            Dim strLogoPath As String = Server.MapPath("~") & "\Images\System\MehrLogo.jpg"
            Dim rndVar As New Random

            Dim ba() As Byte = objMakePDFReport.MakePDFReport(Session("WarningNotificationLogDetailSummaryPDF").ToString(), strLogoPath, "لیست نامه ها")
            IO.File.WriteAllBytes(Server.MapPath("") & "\TempFile\Report.pdf", ba)
            Response.Clear()
            Response.ContentType = "application/pdf"
            Response.AppendHeader("Content-Disposition", "attachment; filename=" & rndVar.Next(1, 15000).ToString & ".pdf")
            Response.TransmitFile(Server.MapPath("") & "\TempFile\Report.pdf")
            Response.End()



        Else

            Bootstrap_Panel1.ShowMessage("لیستی برای نمایش وجود ندارد", True)

        End If
    End Sub

  

    Private Sub Bootstrap_Panel1_Panel_Up_Click(sender As Object, e As System.EventArgs) Handles Bootstrap_Panel1.Panel_Up_Click

        Response.Redirect("../File/FileLoanDetail.aspx")
        Return

    End Sub
End Class