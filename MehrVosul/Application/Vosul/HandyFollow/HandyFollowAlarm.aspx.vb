Public Class HandyFollowAlarm
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
        Bootstrap_Panel1.CanExcel = False
        Bootstrap_Panel1.Enable_Delete_Client_Validate = True
        Bootstrap_Panel1.Enable_Save_Client_Validate = True
        Bootstrap_Panel1.Enable_Search_Client_Validate = True

        lblInnerPageTitle.Text = "فهرست سررسید تعهد"
        Dim dtblUserLogin As BusinessObject.dstUser.spr_User_Login_SelectDataTable = CType(HttpContext.Current.Session("dtblUserLogin"), BusinessObject.dstUser.spr_User_Login_SelectDataTable)
        Dim drwUserLogin As BusinessObject.dstUser.spr_User_Login_SelectRow = dtblUserLogin.Rows(0)



        If Page.IsPostBack = False Then


            Bootstrap_PersianDateTimePicker_From.ShowTimePicker = False
            Bootstrap_PersianDateTimePicker_To.ShowTimePicker = False

            Bootstrap_PersianDateTimePicker_From.GergorainDateTime = Date.Now
            Bootstrap_PersianDateTimePicker_To.GergorainDateTime = Date.Now


            GetList()


        End If

        Bootstrap_PersianDateTimePicker_From.ShowTimePicker = False
        Bootstrap_PersianDateTimePicker_To.ShowTimePicker = False

        If hdnAction.Value.StartsWith("S") = True Then
            Dim intFileID As Long = CLng(hdnAction.Value.Split(";")(1))
            Dim intLoanID As Long = CLng(hdnAction.Value.Split(";")(2))
            Session("AmountDeffed") = Nothing
            Session("customerNO") = "" 'hdnAction.Value.Split(";")(4)
            Session("intFileID") = CObj(intFileID)
            Session("intLoanID") = CObj(intLoanID)

            Response.Redirect("HandyFollowNew.aspx")
        End If




    End Sub

    Private Sub Bootstrap_Panel1_Panel_Display_Click(sender As Object, e As EventArgs) Handles Bootstrap_Panel1.Panel_Display_Click
        GetList()
    End Sub

    Private Sub GetList()

        Dim tadpHandyFollow As New BusinessObject.dstAlarmTableAdapters.spr_HandyFollowAlarm_SelectTableAdapter
        Dim dtblHandyFolow As BusinessObject.dstAlarm.spr_HandyFollowAlarm_SelectDataTable = Nothing

        Dim dtblUserLogin As BusinessObject.dstUser.spr_User_Login_SelectDataTable = CType(HttpContext.Current.Session("dtblUserLogin"), BusinessObject.dstUser.spr_User_Login_SelectDataTable)
        Dim drwUserLogin As BusinessObject.dstUser.spr_User_Login_SelectRow = dtblUserLogin.Rows(0)


        Dim dtFromDate As Date = Bootstrap_PersianDateTimePicker_From.GergorainDateTime
        Dim dteToDate As Date = Bootstrap_PersianDateTimePicker_To.GergorainDateTime

        Try
            Dim intCount As Integer = 0
            Dim intAction As Integer
            If drwUserLogin.IsDataUserAdmin = True Then

                If dtFromDate.Date <> Date.Now.Date OrElse dteToDate.Date <> Date.Now.Date Then
                    intAction = 3
                Else
                    intAction = 1
                End If
                dtblHandyFolow = tadpHandyFollow.GetData(intAction, -1, drwUserLogin.FK_BrnachID, dtFromDate, dteToDate)
                If dtblHandyFolow.Rows.Count > 0 Then

                    For Each drwHandyFollow As BusinessObject.dstAlarm.spr_HandyFollowAlarm_SelectRow In dtblHandyFolow

                        intCount += 1


                        Dim TbCell As HtmlTableCell
                        Dim TbRow As HtmlTableRow
                        TbRow = New HtmlTableRow

                        TbCell = New HtmlTableCell
                        TbCell.InnerHtml = CStr(intCount)
                        TbCell.Align = "center"
                        TbCell.NoWrap = True
                        TbRow.Cells.Add(TbCell)


                        TbCell = New HtmlTableCell
                        TbCell.InnerHtml = drwHandyFollow.LoanNumber
                        TbCell.NoWrap = True
                        TbCell.Align = "center"
                        TbRow.Cells.Add(TbCell)

                        TbCell = New HtmlTableCell
                        TbCell.InnerHtml = drwHandyFollow.CustomerNo
                        TbCell.NoWrap = True
                        TbCell.Align = "center"
                        TbRow.Cells.Add(TbCell)


                        TbCell = New HtmlTableCell
                        TbCell.InnerHtml = drwHandyFollow.FName & " " & drwHandyFollow.LName
                        TbCell.NoWrap = False
                        TbCell.Width = "100px"
                        TbCell.Align = "center"
                        TbRow.Cells.Add(TbCell)


                        TbCell = New HtmlTableCell
                        TbCell.InnerHtml = drwHandyFollow.MobileNo
                        TbCell.NoWrap = True
                        TbCell.Align = "center"
                        TbRow.Cells.Add(TbCell)



                        TbCell = New HtmlTableCell
                        TbCell.InnerHtml = drwHandyFollow.Username
                        TbCell.NoWrap = True
                        TbCell.Align = "center"
                        TbRow.Cells.Add(TbCell)

                        TbCell = New HtmlTableCell
                        TbCell.InnerHtml = drwHandyFollow.DutyDate
                        TbCell.NoWrap = True
                        TbCell.Align = "center"
                        TbRow.Cells.Add(TbCell)


                        TbCell = New HtmlTableCell
                        TbCell.InnerHtml = "<a ID='lnkbtnFollowing' href='#'  onclick= btnFollwoing_ClientClick(" & drwHandyFollow.FileID.ToString() & "," & drwHandyFollow.LoanID.ToString() & "," & "0" & ")>ثبت پیگیری</a>"
                        TbCell.NoWrap = True
                        TbCell.Align = "center"
                        TbRow.Cells.Add(TbCell)



                        tblMResult.Rows.Add(TbRow)

                    Next


                End If


            Else
                If dtFromDate.Date <> Date.Now.Date OrElse dteToDate.Date <> Date.Now.Date Then
                    intAction = 4
                Else
                    intAction = 2
                End If
                dtblHandyFolow = tadpHandyFollow.GetData(intAction, drwUserLogin.ID, -1, dtFromDate, dteToDate)
                If dtblHandyFolow.Rows.Count > 0 Then

                    For Each drwHandyFollow As BusinessObject.dstAlarm.spr_HandyFollowAlarm_SelectRow In dtblHandyFolow

                        intCount += 1


                        Dim TbCell As HtmlTableCell
                        Dim TbRow As HtmlTableRow
                        TbRow = New HtmlTableRow

                        TbCell = New HtmlTableCell
                        TbCell.InnerHtml = CStr(intCount)
                        TbCell.Align = "center"
                        TbCell.NoWrap = True
                        TbRow.Cells.Add(TbCell)


                        TbCell = New HtmlTableCell
                        TbCell.InnerHtml = drwHandyFollow.LoanNumber
                        TbCell.NoWrap = True
                        TbCell.Align = "center"
                        TbRow.Cells.Add(TbCell)

                        TbCell = New HtmlTableCell
                        TbCell.InnerHtml = drwHandyFollow.CustomerNo
                        TbCell.NoWrap = True
                        TbCell.Align = "center"
                        TbRow.Cells.Add(TbCell)


                        TbCell = New HtmlTableCell
                        TbCell.InnerHtml = drwHandyFollow.FName & " " & drwHandyFollow.LName
                        TbCell.NoWrap = False
                        TbCell.Width = "100px"
                        TbCell.Align = "center"
                        TbRow.Cells.Add(TbCell)


                        TbCell = New HtmlTableCell
                        TbCell.InnerHtml = drwHandyFollow.MobileNo
                        TbCell.NoWrap = True
                        TbCell.Align = "center"
                        TbRow.Cells.Add(TbCell)



                        TbCell = New HtmlTableCell
                        TbCell.InnerHtml = drwHandyFollow.Username
                        TbCell.NoWrap = True
                        TbCell.Align = "center"
                        TbRow.Cells.Add(TbCell)

                        TbCell = New HtmlTableCell
                        TbCell.InnerHtml = drwHandyFollow.DutyDate
                        TbCell.NoWrap = True
                        TbCell.Align = "center"
                        TbRow.Cells.Add(TbCell)

                        TbCell = New HtmlTableCell
                        TbCell.InnerHtml = "<a ID='lnkbtnFollowing' href='#'  onclick= btnFollwoing_ClientClick(" & drwHandyFollow.FileID.ToString() & "," & drwHandyFollow.LoanID.ToString() & "," & "0" & ")>ثبت پیگیری</a>"
                        TbCell.NoWrap = True
                        TbCell.Align = "center"
                        TbRow.Cells.Add(TbCell)



                        tblMResult.Rows.Add(TbRow)


                    Next


                End If


            End If

        Catch ex As Exception

        End Try

    End Sub

End Class