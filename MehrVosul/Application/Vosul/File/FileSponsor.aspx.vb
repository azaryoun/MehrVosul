Public Class FileSponsor
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


            Dim tadpLoanSponsor As New BusinessObject.dstLoanSponsorTableAdapters.spr_LoanSponsor_CheckFile_SelectTableAdapter
            Dim dtblLoanSponsor As BusinessObject.dstLoanSponsor.spr_LoanSponsor_CheckFile_SelectDataTable = Nothing

            If Not Session("intEditFileID") Is Nothing And Not Session("intLoanID") Is Nothing Then


                Dim dtblLoan As BusinessObject.dstLoan.spr_Loan_SelectDataTable = Nothing
                Dim tadpLoan As New BusinessObject.dstLoanTableAdapters.spr_Loan_SelectTableAdapter

                dtblLoan = tadpLoan.GetData(2, CInt(Session("intLoanID")), -1, -1)

                If dtblLoan.Rows.Count > 0 Then
                    Session("LoanNumber") = dtblLoan.First.LoanNumber.ToString()
                    lblInnerPageTitle.Text = "شماره وام: " & dtblLoan.First.LoanNumber.ToString()

                End If

                dtblLoanSponsor = tadpLoanSponsor.GetData(CInt(Session("intLoanID")))

                Dim intCount As Integer = 0

                For Each drwLoanSponsor As BusinessObject.dstLoanSponsor.spr_LoanSponsor_CheckFile_SelectRow In dtblLoanSponsor



                    intCount += 1
                    Dim TbRow As New HtmlTableRow
                    Dim TbCell As HtmlTableCell


                    TbCell = New HtmlTableCell
                    TbCell.InnerHtml = "<input type='hidden' id='hdnAmounts" & CStr(intCount) & "' >" & CStr(intCount)
                    TbCell.Align = "center"
                    TbCell.NoWrap = True
                    TbRow.Cells.Add(TbCell)

                    TbCell = New HtmlTableCell
                    TbCell.InnerHtml = drwLoanSponsor.CustomerNo
                    TbCell.NoWrap = True
                    TbCell.Align = "center"
                    TbRow.Cells.Add(TbCell)

                    TbCell = New HtmlTableCell
                    TbCell.InnerHtml = drwLoanSponsor.SponsorName
                    TbCell.NoWrap = True
                    TbCell.Align = "center"
                    TbRow.Cells.Add(TbCell)


                    TbCell = New HtmlTableCell
                    TbCell.InnerHtml = drwLoanSponsor.MobileNo
                    TbCell.NoWrap = True
                    TbCell.Align = "center"
                    TbRow.Cells.Add(TbCell)



                    TbCell = New HtmlTableCell
                    TbCell.InnerHtml = drwLoanSponsor.Address
                    TbCell.NoWrap = True
                    TbCell.Align = "center"
                    TbRow.Cells.Add(TbCell)



                    TbCell = New HtmlTableCell
                    TbCell.InnerHtml = drwLoanSponsor.WarantyTypeDesc
                    TbCell.NoWrap = True
                    TbCell.Align = "center"
                    TbRow.Cells.Add(TbCell)

                    tblResult.Rows.Add(TbRow)
            
                Next


            Else
                Return
            End If


        End If



    End Sub

    Private Sub Bootstrap_Panel1_Panel_Up_Click(sender As Object, e As System.EventArgs) Handles Bootstrap_Panel1.Panel_Up_Click
        Response.Redirect("FileLoanDetail.aspx")
        Return
    End Sub
End Class