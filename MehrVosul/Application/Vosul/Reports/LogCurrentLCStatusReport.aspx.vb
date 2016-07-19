Imports System.IO
Public Class LogCurrentLCStatusReport
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Page.Response.Cache.SetCacheability(HttpCacheability.NoCache)

        Bootstrap_Panel1.CanNew = False
        Bootstrap_Panel1.CanSave = False
        Bootstrap_Panel1.CanDelete = False
        Bootstrap_Panel1.CanSearch = False
        Bootstrap_Panel1.CanCancel = False
        Bootstrap_Panel1.CanUp = False
        Bootstrap_Panel1.CanWizard = False
        Bootstrap_Panel1.CanConfirmRequest = False
        Bootstrap_Panel1.CanReject = False
        Bootstrap_Panel1.CanDisplay = True
        Bootstrap_Panel1.CanExcel = True
        Bootstrap_Panel1.Enable_Save_Client_Validate = True
        Bootstrap_Panel1.Enable_Display_Client_Validate = True

        lblInnerPageTitle.Text = "پرکردن کادرهای قرمز رنگ، اجباری است."

        If Page.IsPostBack = False Then

            Dim dtblUserLogin As BusinessObject.dstUser.spr_User_Login_SelectDataTable = CType(Session("dtblUserLogin"), BusinessObject.dstUser.spr_User_Login_SelectDataTable)
            Dim drwUserLogin As BusinessObject.dstUser.spr_User_Login_SelectRow = dtblUserLogin.Rows(0)


            Dim tadpReport As New BusinessObject.dstReportTableAdapters.spr_LogCurrentLCStatus_ReportTableAdapter
            Dim dtblReport As BusinessObject.dstReport.spr_LogCurrentLCStatus_ReportDataTable = Nothing

           
            Bootstrap_PersianDateTimePicker_From.GergorainDateTime = Date.Now.AddDays(-7).Date
            Bootstrap_PersianDateTimePicker_To.GergorainDateTime = Date.Now

            Bootstrap_PersianDateTimePicker_From.PickerLabel = "از"
            Bootstrap_PersianDateTimePicker_To.PickerLabel = "تا"

            GetReport()

        End If



        Bootstrap_PersianDateTimePicker_From.ShowTimePicker = True
        Bootstrap_PersianDateTimePicker_To.ShowTimePicker = True

    End Sub

    


    Private Sub Bootstrap_Panel1_Panel_Display_Click(sender As Object, e As System.EventArgs) Handles Bootstrap_Panel1.Panel_Display_Click

        GetReport()

      

    End Sub

    Private Sub GetReport()

        Bootstrap_Panel1.ClearMessage()

        Dim tadpReport As New BusinessObject.dstReportTableAdapters.spr_LogCurrentLCStatus_ReportTableAdapter
        Dim dtblReport As BusinessObject.dstReport.spr_LogCurrentLCStatus_ReportDataTable = Nothing

        Dim dtFromDate As Date = Bootstrap_PersianDateTimePicker_From.GergorainDateTime

        Dim dteToDate As Date = Bootstrap_PersianDateTimePicker_To.GergorainDateTime

        Dim intAction As Integer = 1

        Dim blnSuccess As Boolean = False

        If cmbSuccess.SelectedValue <> -1 Then
            intAction = 2
            blnSuccess = If(cmbSuccess.SelectedValue = 1, True, False)
        End If

        dtblReport = tadpReport.GetData(intAction, dtFromDate, dteToDate, blnSuccess)




        If dtblReport.Rows.Count > 0 Then

            divResult.Visible = True

            ''get total report
            Dim intCount As Integer = 0



            For Each drwReport As BusinessObject.dstReport.spr_LogCurrentLCStatus_ReportRow In dtblReport.Rows

                intCount += 1
                Dim TbRow As New HtmlTableRow
                Dim TbCell As HtmlTableCell

                TbCell = New HtmlTableCell
                TbCell.InnerHtml = "<input type='hidden' id='hdnAmounts" & CStr(intCount) & "' >" & CStr(intCount)
                TbCell.Align = "center"
                TbCell.NoWrap = True
                TbRow.Cells.Add(TbCell)


                TbCell = New HtmlTableCell
                TbCell.InnerHtml = mdlGeneral.GetPersianDate(drwReport.DateG)
                TbCell.NoWrap = True
                TbCell.Align = "center"
                TbRow.Cells.Add(TbCell)

                If drwReport.IsSuccessNull = False Then

                    TbCell = New HtmlTableCell
                    TbCell.InnerHtml = If(drwReport.Success = True, "موفق", "ناموفق")
                    TbCell.NoWrap = True
                    TbCell.Align = "center"
                    TbRow.Cells.Add(TbCell)

                    TbCell = New HtmlTableCell
                    TbCell.InnerHtml = drwReport.tryTime
                    TbCell.NoWrap = True
                    TbCell.Align = "center"
                    TbRow.Cells.Add(TbCell)

                    TbCell = New HtmlTableCell
                    TbCell.InnerHtml = If(drwReport.IsRemarksNull = True, "", drwReport.Remarks)
                    TbCell.NoWrap = True
                    TbCell.Align = "center"
                    TbRow.Cells.Add(TbCell)



                Else
                    TbCell = New HtmlTableCell
                    TbCell.InnerHtml = "ناموفق"
                    TbCell.NoWrap = True
                    TbCell.Align = "center"
                    TbRow.Cells.Add(TbCell)

                    TbCell = New HtmlTableCell
                    TbCell.InnerHtml = "--"
                    TbCell.NoWrap = True
                    TbCell.Align = "center"
                    TbRow.Cells.Add(TbCell)

                    TbCell = New HtmlTableCell
                    TbCell.InnerHtml = "عدم دریافت اطلاعات"
                    TbCell.NoWrap = True
                    TbCell.Align = "center"
                    TbRow.Cells.Add(TbCell)
                End If



                tblResult.Rows.Add(TbRow)
            Next

            Session("LogCurrentLCStatusReport") = dtblReport

        Else

            divResult.Visible = False


        End If


    End Sub

    Private Sub Bootstrap_Panel1_Panel_Excel_Click(sender As Object, e As System.EventArgs) Handles Bootstrap_Panel1.Panel_Excel_Click

        Try
            If Session("LogCurrentLCStatusReport") IsNot Nothing Then
                Dim dtblUserLogin As BusinessObject.dstUser.spr_User_Login_SelectDataTable = CType(Session("dtblUserLogin"), BusinessObject.dstUser.spr_User_Login_SelectDataTable)
                Dim drwUserLogin As BusinessObject.dstUser.spr_User_Login_SelectRow = dtblUserLogin.Rows(0)


                Dim strPath As String = Server.MapPath("") & "\TempFile\" & drwUserLogin.ID & "\"
                Dim FileName As String = "LogCurrentLCStatusReport-" & drwUserLogin.ID.ToString()

                Dim tblCSVResult As DataTable = Session("LogCurrentLCStatusReport")
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
                    Session("LogCurrentLCStatusReport") = Nothing
                End If
            Else
                Bootstrap_Panel1.ShowMessage("امکان انتقال گزارش به فایل اکسل وجود ندارد", False)
                Session("LogCurrentLCStatusReport") = Nothing
            End If
        Catch ex As Exception
            Bootstrap_Panel1.ShowMessage("امکان انتقال گزارش به فایل اکسل وجود ندارد", False)
            Session("LogCurrentLCStatusReport") = Nothing
        End Try



    End Sub

  
End Class