Imports System.IO
Public Class FileLoanDetail
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

        '  lblInnerPageTitle.Text = "پرکردن کادرهای قرمز رنگ، اجباری است."

        If Page.IsPostBack = False Then

            Dim dtblUserLogin As BusinessObject.dstUser.spr_User_Login_SelectDataTable = CType(HttpContext.Current.Session("dtblUserLogin"), BusinessObject.dstUser.spr_User_Login_SelectDataTable)
            Dim drwUserLogin As BusinessObject.dstUser.spr_User_Login_SelectRow = dtblUserLogin.Rows(0)

            If drwUserLogin.IsDataAdmin = True Then
                GetNotificationLog(3, -1)
            Else
                GetNotificationLog(4, drwUserLogin.FK_BrnachID)
            End If


        End If

        If hdnAction.Value.StartsWith("E") = True Then
            Dim intLoanID As Integer = CInt(hdnAction.Value.Split(";")(1))
            Session("intLoanID") = CObj(intLoanID)
            Response.Redirect("FileNotificationLog.aspx")
        End If

        If hdnAction.Value.StartsWith("S") = True Then
            Dim intLoanID As Integer = CInt(hdnAction.Value.Split(";")(1))
            Session("intLoanID") = CObj(intLoanID)
            Response.Redirect("FileSponsor.aspx")
        End If

        If hdnAction.Value.StartsWith("M") = True Then
            Dim intLoanID As Integer = CInt(hdnAction.Value.Split(";")(1))
            Session("intLoanID") = CObj(intLoanID)
            Response.Redirect("../Reports/WarningNotificationLogDetailSummaryReport.aspx")
        End If

        If hdnAction.Value.StartsWith("W") = True Then
            Dim intLoanID As Integer = CInt(hdnAction.Value.Split(";")(1))
            Session("intLoanID") = CObj(intLoanID)
            Response.Redirect("WhiteListManagement.aspx")
        End If

    End Sub




    Private Sub GetNotificationLog(ByVal intAction As Integer, ByVal intBranchID As Integer)

        ''Dim tadpReport As New BusinessObject.dstWarningNotificationLogDetailTableAdapters.spr__WarningNotificationLogDetail_ShowDetail_SelectTableAdapter
        ''Dim dtblReport As BusinessObject.dstWarningNotificationLogDetail.spr__WarningNotificationLogDetail_ShowDetail_SelectDataTable = Nothing

        Dim tadpReport As New BusinessObject.dstLoanTableAdapters.spr_Loan_SelectTableAdapter
        Dim dtblReport As BusinessObject.dstLoan.spr_Loan_SelectDataTable = Nothing

        Dim tadpCurrentLCStatus As New BusinessObject.dstCurrentLCStatusTableAdapters.spr_NotPiadDurationDay_SelectTableAdapter
        Dim dtblCurrentLCStatus As BusinessObject.dstCurrentLCStatus.spr_NotPiadDurationDay_SelectDataTable = Nothing

        Dim tadpFile As New BusinessObject.dstFileTableAdapters.spr_File_SelectTableAdapter
        Dim dtblFile As BusinessObject.dstFile.spr_File_SelectDataTable = Nothing


        If Not Session("intEditFileID") Is Nothing Then


            dtblReport = tadpReport.GetData(intAction, -1, CInt(Session("intEditFileID")), intBranchID)


            dtblFile = tadpFile.GetData(1, CInt(Session("intEditFileID")))
            If dtblFile.Rows.Count > 0 Then

                lblInnerPageTitle.Text = lblInnerPageTitle.Text & " (پرونده " & dtblFile.First.CustomerNo & "/" & dtblFile.First.FName & " " & dtblFile.First.LName & ")"

            End If


        Else

            Exit Sub
        End If




        If dtblReport.Rows.Count > 0 Then


            divResult.Visible = True

            ''get total report
            Dim intCount As Integer = 0


            ''For Each drwReport As BusinessObject.dstWarningNotificationLogDetail.spr__WarningNotificationLogDetail_ShowDetail_SelectRow In dtblReport.Rows
            For Each drwReport As BusinessObject.dstLoan.spr_Loan_SelectRow In dtblReport.Rows


                intCount += 1
                Dim TbRow As New HtmlTableRow
                Dim TbCell As HtmlTableCell


                TbCell = New HtmlTableCell
                TbCell.InnerHtml = "<input type='hidden' id='hdnAmounts" & CStr(intCount) & "' >" & CStr(intCount)
                TbCell.Align = "center"
                TbCell.NoWrap = True
                TbRow.Cells.Add(TbCell)

                TbCell = New HtmlTableCell
                TbCell.InnerHtml = drwReport.LoanNumber
                TbCell.NoWrap = True
                TbCell.Align = "center"
                TbRow.Cells.Add(TbCell)

                TbCell = New HtmlTableCell
                TbCell.InnerHtml = mdlGeneral.GetPersianDate(drwReport.LoanDate)
                TbCell.NoWrap = True
                TbCell.Align = "center"
                TbRow.Cells.Add(TbCell)


                TbCell = New HtmlTableCell
                TbCell.InnerHtml = drwReport.LoanTypeName
                TbCell.NoWrap = True
                TbCell.Align = "center"
                TbRow.Cells.Add(TbCell)



                TbCell = New HtmlTableCell
                TbCell.InnerHtml = drwReport.LoanAmount
                TbCell.NoWrap = True
                TbCell.Align = "center"
                TbRow.Cells.Add(TbCell)

                tadpCurrentLCStatus.ClearBeforeFill = True
                dtblCurrentLCStatus = tadpCurrentLCStatus.GetData(drwReport.ID)
                Dim intNotPaidDuration As Integer? = Nothing
                Dim intNoNotPaidInstallment? As Integer = Nothing
                Dim intNoPaidInstallment? As Integer = Nothing

                If dtblCurrentLCStatus.Rows.Count > 0 Then
                    intNotPaidDuration = dtblCurrentLCStatus.First.NotPiadDurationDay
                    intNoNotPaidInstallment = dtblCurrentLCStatus.First.NoDelayInstallment
                    intNoPaidInstallment = dtblCurrentLCStatus.First.NoPaidInstallment
                End If


                If intNotPaidDuration IsNot Nothing Then
                    TbCell = New HtmlTableCell
                    TbCell.InnerHtml = intNotPaidDuration.ToString
                    TbCell.NoWrap = True
                    TbCell.Align = "center"
                    TbRow.Cells.Add(TbCell)
                Else

                    TbCell = New HtmlTableCell
                    TbCell.InnerHtml = "نامشخص"
                    TbCell.NoWrap = True
                    TbCell.Align = "center"
                    TbRow.Cells.Add(TbCell)
                End If

                If drwReport.IsTotalInstallmentNull = False Then
                    TbCell = New HtmlTableCell
                    TbCell.InnerHtml = drwReport.TotalInstallment
                    TbCell.NoWrap = True
                    TbCell.Align = "center"
                    TbRow.Cells.Add(TbCell)
                Else
                    TbCell = New HtmlTableCell
                    TbCell.InnerHtml = "نا مشخص"
                    TbCell.NoWrap = True
                    TbCell.Align = "center"
                    TbRow.Cells.Add(TbCell)
                End If



                If intNoPaidInstallment IsNot Nothing Then

                    TbCell = New HtmlTableCell
                    TbCell.InnerHtml = intNoPaidInstallment.ToString
                    TbCell.NoWrap = True
                    TbCell.Align = "center"
                    TbRow.Cells.Add(TbCell)
                Else

                    TbCell = New HtmlTableCell
                    TbCell.InnerHtml = "نامشخص"
                    TbCell.NoWrap = True
                    TbCell.Align = "center"
                    TbRow.Cells.Add(TbCell)


                End If




                If intNoNotPaidInstallment IsNot Nothing Then

                    TbCell = New HtmlTableCell
                    TbCell.InnerHtml = intNoNotPaidInstallment.ToString
                    TbCell.NoWrap = True
                    TbCell.Align = "center"
                    TbRow.Cells.Add(TbCell)
                Else

                    TbCell = New HtmlTableCell
                    TbCell.InnerHtml = "نامشخص"
                    TbCell.NoWrap = True
                    TbCell.Align = "center"
                    TbRow.Cells.Add(TbCell)


                End If



                TbCell = New HtmlTableCell
                TbCell.InnerHtml = "<a ID='lnkbtnSponsor' href='#'  onclick= btnSponsor_ClientClick(" & drwReport.ID & ")>ضامن ها</a>"
                TbCell.NoWrap = False
                TbCell.Align = "center"
                TbRow.Cells.Add(TbCell)

                TbCell = New HtmlTableCell
                TbCell.InnerHtml = "<a ID='lnkbtnLoanDetail' href='#'  onclick= btnLoanLogDetail_ClientClick(" & drwReport.ID & ")>هزینه اطلاع رسانی</a>"
                TbCell.NoWrap = False
                TbCell.Align = "center"
                TbRow.Cells.Add(TbCell)

                TbCell = New HtmlTableCell
                TbCell.InnerHtml = "<a ID='lnkbtnLoanDetail' href='#'  onclick= btnLoanLogSummary_ClientClick(" & drwReport.ID & ")>نامه ها</a>"
                TbCell.NoWrap = False
                TbCell.Align = "center"
                TbRow.Cells.Add(TbCell)

                TbCell = New HtmlTableCell
                TbCell.InnerHtml = "<a ID='lnkbtnWhiteList' href='#'  onclick= btnWhiteList_ClientClick(" & drwReport.ID & ")>لیست سفید</a>"
                TbCell.NoWrap = False
                TbCell.Align = "center"
                TbRow.Cells.Add(TbCell)



                tblResult.Rows.Add(TbRow)

            Next


            Session("FileLoanDetail") = dtblReport


        End If


    End Sub


    Private Sub Bootstrap_Panel1_Panel_Up_Click(sender As Object, e As System.EventArgs) Handles Bootstrap_Panel1.Panel_Up_Click
        Response.Redirect("FileManagement.aspx")
        Return
    End Sub
End Class