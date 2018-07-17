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
        Bootstrap_Panel1.CanDisplay = False
        Bootstrap_Panel1.CanExcel = False
        Bootstrap_Panel1.Enable_Delete_Client_Validate = True
        Bootstrap_Panel1.Enable_Save_Client_Validate = True
        Bootstrap_Panel1.Enable_Search_Client_Validate = True

        lblInnerPageTitle.Text = "فهرست سررسید تعهد"
        Dim dtblUserLogin As BusinessObject.dstUser.spr_User_Login_SelectDataTable = CType(HttpContext.Current.Session("dtblUserLogin"), BusinessObject.dstUser.spr_User_Login_SelectDataTable)
        Dim drwUserLogin As BusinessObject.dstUser.spr_User_Login_SelectRow = dtblUserLogin.Rows(0)



        If Page.IsPostBack = False Then

            GetList()


        End If


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

    Private Sub GetList()

        Dim tadpHandyFollow As New BusinessObject.dstAlarmTableAdapters.spr_HandyFollowAlarm_SelectTableAdapter
        Dim dtblHandyFolow As BusinessObject.dstAlarm.spr_HandyFollowAlarm_SelectDataTable = Nothing

        Dim dtblUserLogin As BusinessObject.dstUser.spr_User_Login_SelectDataTable = CType(HttpContext.Current.Session("dtblUserLogin"), BusinessObject.dstUser.spr_User_Login_SelectDataTable)
        Dim drwUserLogin As BusinessObject.dstUser.spr_User_Login_SelectRow = dtblUserLogin.Rows(0)

        Try
            Dim intCount As Integer = 0

            If drwUserLogin.IsDataUserAdmin = True Then

                dtblHandyFolow = tadpHandyFollow.GetData(1, -1, drwUserLogin.FK_BrnachID)
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
                        TbCell.InnerHtml = "<a ID='lnkbtnFollowing' href='#'  onclick= btnFollwoing_ClientClick(" & drwHandyFollow.FileID.ToString() & "," & drwHandyFollow.LoanID.ToString() & "," & "0" & ")>ثبت پیگیری</a>"
                        TbCell.NoWrap = True
                        TbCell.Align = "center"
                        TbRow.Cells.Add(TbCell)



                        tblMResult.Rows.Add(TbRow)

                    Next


                End If


            Else

                dtblHandyFolow = tadpHandyFollow.GetData(2, drwUserLogin.ID, -1)
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