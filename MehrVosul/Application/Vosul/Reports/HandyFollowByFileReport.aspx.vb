Imports System.IO
Public Class HandyFollowByFileReport
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

        lblInnerPageTitle.Text = "پرکردن کادرهای قرمز رنگ، اجباری است."

        If Page.IsPostBack = False Then



            'Bootstrap_PersianDateTimePicker_From.GergorainDateTime = Date.Now.AddDays(-3).Date
            'Bootstrap_PersianDateTimePicker_To.GergorainDateTime = Date.Now

            'Bootstrap_PersianDateTimePicker_From.PickerLabel = "از"
            'Bootstrap_PersianDateTimePicker_To.PickerLabel = "تا"





        End If



        'Bootstrap_PersianDateTimePicker_From.ShowTimePicker = True
        'Bootstrap_PersianDateTimePicker_To.ShowTimePicker = True

    End Sub

    Private Sub Bootstrap_Panel1_Panel_Display_Click(sender As Object, e As EventArgs) Handles Bootstrap_Panel1.Panel_Display_Click


        Bootstrap_Panel1.ClearMessage()

        'Dim dtFromDate As Date = Bootstrap_PersianDateTimePicker_From.GergorainDateTime
        'Dim dteToDate As Date = Bootstrap_PersianDateTimePicker_To.GergorainDateTime


        Dim dtblUserLogin As BusinessObject.dstUser.spr_User_Login_SelectDataTable = CType(Session("dtblUserLogin"), BusinessObject.dstUser.spr_User_Login_SelectDataTable)
        Dim drwUserLogin As BusinessObject.dstUser.spr_User_Login_SelectRow = dtblUserLogin.Rows(0)


        Dim tadpHandyFollowFileReport As New BusinessObject.dstHandyFollowTableAdapters.spr_HandyFollowByFile_ReportTableAdapter
        Dim dtblHandyFollowFileReport As BusinessObject.dstHandyFollow.spr_HandyFollowByFile_ReportDataTable = Nothing

        Dim strCustomerNO As String = txtCustomerNO.Text

        dtblHandyFollowFileReport = tadpHandyFollowFileReport.GetData(strCustomerNO, drwUserLogin.Fk_ProvinceID)


        Session("dtblHandyFollowFileReport") = dtblHandyFollowFileReport
        If dtblHandyFollowFileReport.Rows.Count > 0 Then

            divResult.Visible = True

            ''get total report
            Dim intCount As Integer = 0



            For Each drwReport As BusinessObject.dstHandyFollow.spr_HandyFollowByFile_ReportRow In dtblHandyFollowFileReport.Rows



                intCount += 1
                Dim TbRow As New HtmlTableRow
                Dim TbCell As HtmlTableCell

                TbCell = New HtmlTableCell
                TbCell.InnerHtml = "<input type='hidden' id='hdnAmounts" & CStr(intCount) & "' >" & CStr(intCount)
                TbCell.Align = "center"
                TbCell.NoWrap = True

                TbRow.Cells.Add(TbCell)


                TbCell = New HtmlTableCell
                TbCell.InnerHtml = mdlGeneral.GetPersianDateTime(drwReport.ContactDate)
                TbCell.NoWrap = True
                TbCell.Align = "left"
                TbCell.Attributes.Add("dir", "ltr")

                TbRow.Cells.Add(TbCell)

                TbCell = New HtmlTableCell
                TbCell.InnerHtml = drwReport.NotificationType
                TbCell.NoWrap = True
                TbCell.Align = "center"
                TbRow.Cells.Add(TbCell)


                TbCell = New HtmlTableCell
                TbCell.InnerHtml = drwReport.FileName & If(drwReport.ToSponsor = True, "-ضامن", "-وام گیرنده")
                TbCell.NoWrap = False
                TbCell.Align = "center"
                TbRow.Cells.Add(TbCell)

                TbCell = New HtmlTableCell
                TbCell.InnerHtml = If(drwReport.Answered = True, "باپاسخ", "بدون پاسخ")
                TbCell.NoWrap = True
                TbCell.Align = "center"
                TbRow.Cells.Add(TbCell)


                TbCell = New HtmlTableCell
                TbCell.InnerHtml = If(drwReport.IsSuccess = True, "موفق", "ناموفق")
                TbCell.NoWrap = True
                TbCell.Align = "center"
                TbRow.Cells.Add(TbCell)


                TbCell = New HtmlTableCell
                TbCell.InnerHtml = drwReport.Username
                TbCell.NoWrap = True
                TbCell.Align = "center"
                TbRow.Cells.Add(TbCell)


                TbCell = New HtmlTableCell
                TbCell.InnerHtml = drwReport.Branch
                TbCell.NoWrap = True
                TbCell.Align = "center"
                TbRow.Cells.Add(TbCell)


                TbCell = New HtmlTableCell
                TbCell.InnerHtml = drwReport.Remarks
                TbCell.NoWrap = False
                TbCell.Align = "center"
                TbRow.Cells.Add(TbCell)


                TbCell = New HtmlTableCell
                TbCell.InnerHtml = If(drwReport.IsDutyDateNull = True, "", mdlGeneral.GetPersianDate(drwReport.DutyDate))
                TbCell.NoWrap = False
                TbCell.Align = "center"
                TbRow.Cells.Add(TbCell)


                tblResult.Rows.Add(TbRow)

            Next

            Session("HandyFollowReport") = dtblHandyFollowFileReport

        Else

            divResult.Visible = False


        End If

    End Sub

    Private Sub Bootstrap_Panel1_Panel_Excel_Click(sender As Object, e As EventArgs) Handles Bootstrap_Panel1.Panel_Excel_Click


        Try
            If Session("dtblHandyFollowFileReport") IsNot Nothing Then
                Dim dtblUserLogin As BusinessObject.dstUser.spr_User_Login_SelectDataTable = CType(Session("dtblUserLogin"), BusinessObject.dstUser.spr_User_Login_SelectDataTable)
                Dim drwUserLogin As BusinessObject.dstUser.spr_User_Login_SelectRow = dtblUserLogin.Rows(0)


                Dim strPath As String = Server.MapPath("") & "\TempFile\" & drwUserLogin.ID & "\"
                Dim FileName As String = "HandyFollowFileReport-" & drwUserLogin.ID.ToString()

                Dim tblCSVResult As DataTable = Session("dtblHandyFollowFileReport")
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
                    Session("dtblHandyFollowFileReport") = Nothing
                End If
            Else
                Bootstrap_Panel1.ShowMessage("امکان انتقال گزارش به فایل اکسل وجود ندارد", False)
                Session("dtblHandyFollowFileReport") = Nothing
            End If
        Catch ex As Exception
            Bootstrap_Panel1.ShowMessage("امکان انتقال گزارش به فایل اکسل وجود ندارد", False)
            Session("dtblHandyFollowFileReport") = Nothing
        End Try


    End Sub
End Class