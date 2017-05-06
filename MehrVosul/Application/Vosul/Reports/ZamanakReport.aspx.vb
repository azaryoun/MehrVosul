Public Class ZamanakReport
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

            Dim dtblUserLogin As BusinessObject.dstUser.spr_User_Login_SelectDataTable = CType(Session("dtblUserLogin"), BusinessObject.dstUser.spr_User_Login_SelectDataTable)
            Dim drwUserLogin As BusinessObject.dstUser.spr_User_Login_SelectRow = dtblUserLogin.Rows(0)


            Bootstrap_PersianDateTimePicker_From.GergorainDateTime = Date.Now.AddDays(-3).Date
            Bootstrap_PersianDateTimePicker_To.GergorainDateTime = Date.Now

            Bootstrap_PersianDateTimePicker_From.PickerLabel = "از"
            Bootstrap_PersianDateTimePicker_To.PickerLabel = "تا"


        End If





    End Sub

    Private Sub Bootstrap_Panel1_Panel_Display_Click(sender As Object, e As EventArgs) Handles Bootstrap_Panel1.Panel_Display_Click


        Dim dtFromDate As Date = Bootstrap_PersianDateTimePicker_From.GergorainDateTime
        Dim dteToDate As Date = Bootstrap_PersianDateTimePicker_To.GergorainDateTime.AddDays(1)


        Dim tadpZamanakReport As New BusinessObject.dstZamanakTableAdapters.spr_VoiceMessageStatusReport_SelectTableAdapter
        Dim dtblZamanakReport As BusinessObject.dstZamanak.spr_VoiceMessageStatusReport_SelectDataTable = Nothing

        dtblZamanakReport = tadpZamanakReport.GetData(dtFromDate, dteToDate)

        If dtblZamanakReport.Rows.Count > 0 Then

            divResult.Visible = True

            Dim intCount As Integer = 0
            Dim intTotalCount As Integer = 0

            Dim i As Integer = 0
            Dim currentDate As DateTime
            For Each drwZamanak As BusinessObject.dstZamanak.spr_VoiceMessageStatusReport_SelectRow In dtblZamanakReport

                intCount += 1
                Dim TbRow As HtmlTableRow
                Dim TbCell As HtmlTableCell



                currentDate = drwZamanak.SendTime
                If i = 0 Then

                    TbRow = New HtmlTableRow ''  Dim TbRow As New HtmlTableRow

                    TbCell = New HtmlTableCell
                    TbCell.InnerHtml = CStr(intCount)
                    TbCell.Align = "center"
                    TbCell.NoWrap = True
                    TbRow.Cells.Add(TbCell)


                    TbCell = New HtmlTableCell
                    TbCell.InnerHtml = mdlGeneral.GetPersianDate(drwZamanak.SendTime)
                    TbCell.NoWrap = True
                    TbCell.Align = "center"
                    TbCell.Attributes.Add("dir", "rtl")
                    TbRow.Cells.Add(TbCell)


                    TbCell = New HtmlTableCell
                    TbCell.InnerHtml = drwZamanak.Answered.ToString("n0")
                    TbCell.NoWrap = True
                    TbCell.Align = "center"
                    TbRow.Cells.Add(TbCell)


                ElseIf currentDate = drwZamanak.SendTime Then


                    TbCell = New HtmlTableCell
                    TbCell.InnerHtml = drwZamanak.Answered.ToString("n0")
                    TbCell.NoWrap = True
                    TbCell.Align = "center"
                    TbRow.Cells.Add(TbCell)

                End If


                i += 1

                If i = 4 Then
                    tblResult.Rows.Add(TbRow)
                    i = 0
                End If




            Next

            'Dim TbRow1 As New HtmlTableRow
            'Dim TbCell1 As HtmlTableCell

            'TbCell1 = New HtmlTableCell
            'TbCell1.InnerHtml = "مجموع"
            'TbCell1.NoWrap = True
            'TbCell1.Align = "center"
            'TbCell1.ColSpan = 2
            'TbRow1.Cells.Add(TbCell1)

            'TbCell1 = New HtmlTableCell
            'TbCell1.InnerHtml = intTotalCount.ToString("n0")
            'TbCell1.NoWrap = True
            'TbCell1.Align = "center"

            'TbRow1.Cells.Add(TbCell1)






        End If




    End Sub

    Private Sub Bootstrap_Panel1_Panel_Excel_Click(sender As Object, e As EventArgs) Handles Bootstrap_Panel1.Panel_Excel_Click





    End Sub
End Class