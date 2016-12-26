Public Class HandyFollowNew
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
        Bootstrap_Panel1.Enable_Display_Client_Validate = True
     
        If Page.IsPostBack = False Then

            ''Get Handy Follow related List
            If Not Session("intFileID") Is Nothing And Not Session("intLoanID") Is Nothing Then


                odcSponsor.SelectParameters.Item("LoanID").DefaultValue = CInt(Session("intLoanID"))
              
                cmbSponsor.DataBind()

                Bootstrap_PersianDateTimePicker_From.GergorainDateTime = Date.Now

                ''    Bootstrap_PersianDateTimePicker_From.PickerLabel = "زمان ثبت پیگیری"

                Dim tadpFile As New BusinessObject.dstFileTableAdapters.spr_File_SelectTableAdapter
                Dim dtblFile As BusinessObject.dstFile.spr_File_SelectDataTable = Nothing

                dtblFile = tadpFile.GetData(1, CInt(Session("intFileID")))

                If dtblFile.First.IsTelephoneWorkNull = False Then
                    lblBorrowerPhone.InnerText = dtblFile.First.TelephoneWork
                End If
                If dtblFile.First.IsTelephoneHomeNull = False Then
                    lblBorrowerHomePhone.InnerText = dtblFile.First.TelephoneHome
                End If

                If dtblFile.First.IsMobileNoNull = False Then
                    lblBorrowerMobile.InnerText = dtblFile.First.MobileNo
                End If


                GetHandyFollowList()



                Else


                    Response.Redirect("HandyFollowSearch.aspx")
            End If
          

        End If

        Bootstrap_PersianDateTimePicker_From.ShowTimePicker = True
        Bootstrap_PersianDateTimePicker_TO.ShowTimePicker = False

      
    End Sub

    Protected Sub btnAddToText_Click(sender As Object, e As EventArgs) Handles btnAddToText.Click


        Try

            Dim qryHnadyFollow As New BusinessObject.dstHandyFollowTableAdapters.QueriesTableAdapter


            Dim dtblUserLogin As BusinessObject.dstUser.spr_User_Login_SelectDataTable = CType(Session("dtblUserLogin"), BusinessObject.dstUser.spr_User_Login_SelectDataTable)
            Dim drwUserLogin As BusinessObject.dstUser.spr_User_Login_SelectRow = dtblUserLogin.Rows(0)

            Dim intFileID As Integer = CInt(Session("intFileID"))
            Dim intLonaID As Integer = CInt(Session("intLoanID"))
            Dim intNotificationTypeID As Integer = CInt(cmbNotificationType.SelectedValue)
            Dim blnToSponsor As Boolean = If(rdboToSponsor.SelectedValue = 1, True, False)
            Dim intAudienceFileID As Integer = CInt(Session("intFileID"))


            If blnToSponsor = True Then
                If cmbSponsor.SelectedValue <> -1 Then

                    intAudienceFileID = cmbSponsor.SelectedValue

                Else

                    ''
                    Bootstrap_Panel1.ShowMessage("ضامن را مشخص نمایید", True)
                    Return

                End If
            End If
       

            Dim blnAnswered As Boolean = If(rdbListAnswered.SelectedValue = 1, True, False)
            Dim blnIsSuccess As Boolean = If(rdbListNotificationStatus.SelectedValue = 1, True, False)
            Dim strRemarks As String = txtRemark.Text
            Dim dtFromDate As DateTime = Bootstrap_PersianDateTimePicker_From.GergorainDateTime
            Dim dteDutyDate As Date? = Nothing
            If Bootstrap_PersianDateTimePicker_TO.PersianDateTime <> "" Then
                dteDutyDate = Bootstrap_PersianDateTimePicker_TO.GergorainDateTime
            End If

            qryHnadyFollow.spr_HandyFollow_Insert(intFileID, intLonaID, intNotificationTypeID, blnToSponsor, intAudienceFileID, dtFromDate, drwUserLogin.ID, Date.Now, strRemarks, blnAnswered, blnIsSuccess, dteDutyDate)

            Bootstrap_Panel1.ShowMessage("ثبت پیگیری با موفقیت انجام شد", False)

            GetHandyFollowList()


            cmbNotificationType.SelectedValue = 2
            rdbListAnswered.SelectedValue = 1
            rdbListNotificationStatus.SelectedValue = 1
            rdboToSponsor.SelectedValue = 0
            cmbSponsor.SelectedValue = -1
            txtRemark.Text = ""
            Bootstrap_PersianDateTimePicker_From.GergorainDateTime = Date.Now
            Bootstrap_PersianDateTimePicker_From.GergorainDateTime = Date.Now.AddDays(10)


        Catch ex As Exception

            Bootstrap_Panel1.ShowMessage("در ثبت پیگیری خطا رخ داده است", True)

            Return

        End Try
     





    End Sub

    Private Sub Bootstrap_Panel1_Panel_Up_Click(sender As Object, e As System.EventArgs) Handles Bootstrap_Panel1.Panel_Up_Click
        Response.Redirect("HandyFollowSearch.aspx")
    End Sub

    Protected Sub cmbSponsor_DataBound(sender As Object, e As EventArgs) Handles cmbSponsor.DataBound
        Dim li As New ListItem
        li.Text = "---"
        li.Value = -1
        cmbSponsor.Items.Insert(0, li)
    End Sub


    Private Sub GetHandyFollowList()

        Dim intFileID As Integer = CInt(Session("intFileID"))
        Dim intLonaID As Integer = CInt(Session("intLoanID"))

        Dim tadpHandyFollowList As New BusinessObject.dstHandyFollowTableAdapters.spr_HandyFollow_CheckFileLoan_SelectTableAdapter
        Dim dtblHandyFollow As BusinessObject.dstHandyFollow.spr_HandyFollow_CheckFileLoan_SelectDataTable = Nothing

        dtblHandyFollow = tadpHandyFollowList.GetData(intLonaID, intFileID)

        Dim intCount As Integer = 0

        For Each drwHandyFollow As BusinessObject.dstHandyFollow.spr_HandyFollow_CheckFileLoan_SelectRow In dtblHandyFollow.Rows

            tblResult.Visible = True
            intCount += 1
            Dim TbRow As New HtmlTableRow
            Dim TbCell As HtmlTableCell


            TbCell = New HtmlTableCell
            TbCell.InnerHtml = CStr(intCount)
            TbCell.Align = "center"
            TbCell.NoWrap = True
            TbRow.Cells.Add(TbCell)


            TbCell = New HtmlTableCell
            TbCell.InnerHtml = mdlGeneral.GetPersianDateTime(drwHandyFollow.ContactDate)
            TbCell.NoWrap = True
            TbCell.Align = "center"
            TbRow.Cells.Add(TbCell)

            TbCell = New HtmlTableCell
            TbCell.InnerHtml = drwHandyFollow.NotificationType
            TbCell.NoWrap = True
            TbCell.Align = "center"
            TbRow.Cells.Add(TbCell)


            TbCell = New HtmlTableCell
            TbCell.InnerHtml = drwHandyFollow.FileName
            TbCell.NoWrap = True
            TbCell.Align = "center"
            TbRow.Cells.Add(TbCell)


            TbCell = New HtmlTableCell
            TbCell.InnerHtml = If(drwHandyFollow.IsSuccess = True, "باپاسخ", "بدون پاسخ")
            TbCell.NoWrap = True
            TbCell.Align = "center"
            TbRow.Cells.Add(TbCell)


            TbCell = New HtmlTableCell
            TbCell.InnerHtml = If(drwHandyFollow.Answered = True, "مثبت", "منفی")
            TbCell.NoWrap = True
            TbCell.Align = "center"
            TbRow.Cells.Add(TbCell)


            TbCell = New HtmlTableCell
            TbCell.InnerHtml = drwHandyFollow.Remarks
            TbCell.NoWrap = False
            TbCell.Align = "center"
            TbRow.Cells.Add(TbCell)



            tblResult.Rows.Add(TbRow)




        Next


    End Sub

    Protected Sub cmbSponsor_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbSponsor.SelectedIndexChanged


        If cmbSponsor.SelectedValue <> -1 Then

            Dim tadpFile As New BusinessObject.dstFileTableAdapters.spr_File_SelectTableAdapter
            Dim dtblFile As BusinessObject.dstFile.spr_File_SelectDataTable = Nothing

            dtblFile = tadpFile.GetData(1, CInt(cmbSponsor.SelectedValue))

            If dtblFile.First.IsTelephoneHomeNull = False Then
                lblSponsorPhone.InnerText = dtblFile.First.TelephoneHome
            End If

            If dtblFile.First.IsTelephoneWorkNull = False Then
                lblSponsorPhoneWork.InnerText = dtblFile.First.TelephoneWork
            End If

            If dtblFile.First.IsMobileNoNull = False Then
                lblBorrowerMobile.InnerText = dtblFile.First.MobileNo
            End If

        Else
            lblSponsorPhone.InnerText = ""
            lblSponsorPhoneWork.InnerText = ""
            lblBorrowerMobile.InnerText = ""

        End If


    End Sub
End Class