﻿Public Class HandyFollowAssign
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Page.Response.Cache.SetCacheability(HttpCacheability.NoCache)

        Bootstrap_Panel1.CanNew = False
        Bootstrap_Panel1.CanSave = True
        Bootstrap_Panel1.CanDelete = False
        Bootstrap_Panel1.CanSearch = False
        Bootstrap_Panel1.CanCancel = True
        Bootstrap_Panel1.CanUp = False
        Bootstrap_Panel1.CanWizard = False
        Bootstrap_Panel1.CanConfirmRequest = False
        Bootstrap_Panel1.CanReject = False
        Bootstrap_Panel1.CanDisplay = False
        Bootstrap_Panel1.CanExcel = False
        Bootstrap_Panel1.Enable_Save_Client_Validate = True

        lblInnerPageTitle.Text = "پرکردن کادرهای قرمز رنگ، اجباری است."

        If Page.IsPostBack = False Then

            Dim dtblUserLogin As BusinessObject.dstUser.spr_User_Login_SelectDataTable = CType(Session("dtblUserLogin"), BusinessObject.dstUser.spr_User_Login_SelectDataTable)
            Dim drwUserLogin As BusinessObject.dstUser.spr_User_Login_SelectRow = dtblUserLogin.Rows(0)




            If drwUserLogin.IsDataAdmin = False AndAlso drwUserLogin.IsDataUserAdmin = True Then

                cmbProvince.SelectedValue = drwUserLogin.Fk_ProvinceID

                odsBranch.SelectParameters.Item("Action").DefaultValue = 2
                odsBranch.SelectParameters.Item("ProvinceID").DefaultValue = drwUserLogin.Fk_ProvinceID
                odsBranch.DataBind()

                cmbBranch.DataSourceID = "odsBranch"
                cmbBranch.DataTextField = "BrnachCode"
                cmbBranch.DataValueField = "ID"


                cmbBranch.DataBind()
                cmbBranch.Enabled = False
                cmbBranch.SelectedValue = drwUserLogin.FK_BrnachID

                odsPerson.SelectParameters.Item("Action").DefaultValue = 1
                odsPerson.SelectParameters.Item("BranchID").DefaultValue = cmbBranch.SelectedValue
                odsPerson.SelectParameters.Item("ProvinceID").DefaultValue = -1

                cmbPerson.DataBind()





                cmbProvince.Enabled = False

            ElseIf drwUserLogin.IsDataAdmin = True Then


                cmbProvince.SelectedValue = drwUserLogin.Fk_ProvinceID

                odsBranch.SelectParameters.Item("Action").DefaultValue = 2
                odsBranch.SelectParameters.Item("ProvinceID").DefaultValue = drwUserLogin.Fk_ProvinceID
                odsBranch.DataBind()

                cmbBranch.DataSourceID = "odsBranch"
                cmbBranch.DataTextField = "BrnachCode"
                cmbBranch.DataValueField = "ID"


                cmbBranch.DataBind()
                cmbBranch.SelectedValue = drwUserLogin.FK_BrnachID

                odsPerson.SelectParameters.Item("Action").DefaultValue = 1
                odsPerson.SelectParameters.Item("BranchID").DefaultValue = cmbBranch.SelectedValue
                odsPerson.SelectParameters.Item("ProvinceID").DefaultValue = -1

                cmbPerson.DataBind()

            ElseIf drwUserLogin.IsDataAdmin = False AndAlso drwUserLogin.IsDataUserAdmin = False Then


                cmbProvince.SelectedValue = drwUserLogin.Fk_ProvinceID

                odsBranch.SelectParameters.Item("Action").DefaultValue = 2
                odsBranch.SelectParameters.Item("ProvinceID").DefaultValue = drwUserLogin.Fk_ProvinceID
                odsBranch.DataBind()

                cmbBranch.DataSourceID = "odsBranch"
                cmbBranch.DataTextField = "BrnachCode"
                cmbBranch.DataValueField = "ID"

                cmbBranch.DataBind()
                cmbBranch.SelectedValue = drwUserLogin.FK_BrnachID
                cmbBranch.Enabled = False
                cmbProvince.Enabled = False

                odsPerson.SelectParameters.Item("Action").DefaultValue = 1
                odsPerson.SelectParameters.Item("BranchID").DefaultValue = cmbBranch.SelectedValue
                odsPerson.SelectParameters.Item("ProvinceID").DefaultValue = -1

                cmbPerson.DataBind()




            End If

            txtNotPiadDurationDayFrom.Attributes.Add("onkeypress", "return numbersonly(event, false);")
            txtNotPiadDurationDayTo.Attributes.Add("onkeypress", "return numbersonly(event, false);")

        End If



    End Sub

    Private Sub Bootstrap_Panel1_Panel_Save_Click(sender As Object, e As EventArgs) Handles Bootstrap_Panel1.Panel_Save_Click

        Try

            Dim qryHandyFollow As New BusinessObject.dstHandyFollowTableAdapters.QueriesTableAdapter
            Dim dtblUserLogin As BusinessObject.dstUser.spr_User_Login_SelectDataTable = CType(Session("dtblUserLogin"), BusinessObject.dstUser.spr_User_Login_SelectDataTable)
            Dim drwUserLogin As BusinessObject.dstUser.spr_User_Login_SelectRow = dtblUserLogin.Rows(0)


            Dim blnFileCheck As Boolean = False

            For i As Integer = 0 To Request.Form.Keys.Count - 1



                ''If Request.Form("cmbRequestStatus" & CStr(i)) IsNot Nothing Then
                ''    intStatus = CInt(Request.Form("cmbRequestStatus" & CStr(i)))
                If Request.Form.Keys(i).StartsWith("cmbAssignPerson") = True Then

                    If Request.Form(i) <> "-1" Then
                        Dim strLCNO As String = Request.Form(i).Substring(Request.Form(i).IndexOf("(") + 1, Request.Form(i).IndexOf(")") - Request.Form(i).IndexOf("(") - 1)
                        'Request.Form(i).Substring(Request.Form(i).IndexOf("(") + 1, Request.Form(i).IndexOf(")") - 2)


                        ' get File ID
                        Dim tadpFile As New BusinessObject.dstFileTableAdapters.spr_File_SelectTableAdapter
                        Dim dtblFile As BusinessObject.dstFile.spr_File_SelectDataTable = Nothing

                        Dim strCustomerNO As String = Request.Form(i).Substring(Request.Form(i).IndexOf(")") + 1, Request.Form(i).IndexOf("/") - Request.Form(i).IndexOf(")") - 1)
                        dtblFile = tadpFile.GetData(3, -1, strCustomerNO)

                        If dtblFile.Rows.Count <> 0 Then
                            Dim intFileID As Integer = dtblFile.First.ID

                            Dim tadpLoan As New BusinessObject.dstLoanTableAdapters.spr_Loan_ByLoanNumber_SelectTableAdapter
                            Dim dtblLoan As BusinessObject.dstLoan.spr_Loan_ByLoanNumber_SelectDataTable = Nothing

                            dtblLoan = tadpLoan.GetData(strLCNO, intFileID)

                            Dim intLoanID As Integer = dtblLoan.First.ID

                            Dim intAssignUserID As Integer = CInt(Request.Form(i).Substring(0, Request.Form(i).IndexOf("(")))
                            '''Dim strRemark As String = txtRemark.Text
                            qryHandyFollow.spr_HandyFollowAssign_Insert(intAssignUserID, intFileID, Date.Now, drwUserLogin.ID, "", intLoanID, 1)

                        Else


                        End If


                    End If


                End If


                ''Dim strLCNO As String = Request.Form(i).Substring(Request.Form(i). .Text.IndexOf("(") + 1, cmbFiles.SelectedItem.Text.IndexOf(")") - cmbFiles.SelectedItem.Text.IndexOf("(") - 1)


            Next i

            'If blnFileCheck = True Then

            'Else
            '    Bootstrap_Panel1.ShowMessage("فایلی جهت تخصیص انتخاب نشده", True)
            '    Return

            'End If



        Catch ex As Exception


            Response.Redirect("HandyFollowManagement.aspx?Save=NO")
        End Try

        Response.Redirect("HandyFollowManagement.aspx?Save=OK")

    End Sub

    Protected Sub btnCheckFiles_ServerClick(sender As Object, e As EventArgs) Handles btnCheckFiles.Click

        Dim dtblUserLogin As BusinessObject.dstUser.spr_User_Login_SelectDataTable = CType(Session("dtblUserLogin"), BusinessObject.dstUser.spr_User_Login_SelectDataTable)
        Dim drwUserLogin As BusinessObject.dstUser.spr_User_Login_SelectRow = dtblUserLogin.Rows(0)

        Dim dtblPerson As BusinessObject.dstUser.spr_User_CheckBranch_SelectDataTable = Nothing
        Dim tadpPerson As New BusinessObject.dstUserTableAdapters.spr_User_CheckBranch_SelectTableAdapter



        Dim intNotPiadDurationDayFrom As Integer = CInt(txtNotPiadDurationDayFrom.Text)
        Dim intNotPiadDurationDayTo As Integer = CInt(txtNotPiadDurationDayTo.Text)
        Dim strBranchCode As String = cmbBranch.SelectedItem.Text.Substring(0, cmbBranch.SelectedItem.Text.IndexOf("("))


        Dim tadpBranch As New BusinessObject.dstBranchTableAdapters.spr_Branch_ByCode_SelectTableAdapter
        Dim dtblBranch As BusinessObject.dstBranch.spr_Branch_ByCode_SelectDataTable = Nothing

        dtblBranch = tadpBranch.GetData(strBranchCode)
        ''  odsFiles.SelectParameters.Item("BranchID").DefaultValue = dtblBranch.First.ID

        ''   cmbFiles.DataBind()

        Dim tadpTotalDeffredForAssign As New BusinessObject.dstTotalDeffredLCTableAdapters.spr_TotalDeffredLCFileAssign_SelectTableAdapter
        Dim dtblTotalDeffredForAssign As BusinessObject.dstTotalDeffredLC.spr_TotalDeffredLCFileAssign_SelectDataTable = Nothing

        dtblTotalDeffredForAssign = tadpTotalDeffredForAssign.GetData(strBranchCode, intNotPiadDurationDayFrom, intNotPiadDurationDayTo, dtblBranch.First.ID)

        ''Dim strchklstFiles As String = ""

        ''For Each drwAssignFile As BusinessObject.dstTotalDeffredLC.spr_TotalDeffredLCFileAssign_SelectRow In dtblTotalDeffredForAssign.Rows
        ''    strchklstFiles &= "<div class='checkbox'> <label> <input type='checkbox' value='" & drwAssignFile.CULN & "' name='chklstMenu" & drwAssignFile.CustomerNO & "'><i class='fa " & " fa-1x'></i> " & drwAssignFile.CULN & "</label></div>"
        ''Next drwAssignFile

        ''divchklstAssignFiles.InnerHtml = strchklstFiles

        Dim i As Integer = 1
        For Each drwRTotalDeffredLC As BusinessObject.dstTotalDeffredLC.spr_TotalDeffredLCFileAssign_SelectRow In dtblTotalDeffredForAssign.Rows

            Dim TbRow As New HtmlTableRow
            Dim TbCell As HtmlTableCell

            TbCell = New HtmlTableCell
            ' '"<input type='hidden' id='hdnAmounts" & CStr(i) & "' value='" & drwRTotalDeffredLC.LCNumber & "') >"
            TbCell.InnerHtml = CStr(i)
            TbCell.Align = "center"
            TbCell.NoWrap = True
            TbRow.Cells.Add(TbCell)

            TbCell = New HtmlTableCell
            TbCell.InnerHtml = drwRTotalDeffredLC.FullName
            TbCell.NoWrap = True
            TbCell.Align = "center"
            TbRow.Cells.Add(TbCell)

            TbCell = New HtmlTableCell
            TbCell.InnerHtml = drwRTotalDeffredLC.LCNumber
            TbCell.NoWrap = True
            TbCell.Align = "center"
            TbRow.Cells.Add(TbCell)


            TbCell = New HtmlTableCell
            TbCell.InnerHtml = drwRTotalDeffredLC.InstallmentsCount
            TbCell.NoWrap = True
            TbCell.Align = "center"
            TbRow.Cells.Add(TbCell)

            TbCell = New HtmlTableCell
            TbCell.InnerHtml = drwRTotalDeffredLC.LCAmount.ToString("N0")
            TbCell.NoWrap = True
            TbCell.Align = "center"
            TbRow.Cells.Add(TbCell)

            TbCell = New HtmlTableCell
            TbCell.InnerHtml = drwRTotalDeffredLC.LoanTypeName
            TbCell.NoWrap = True
            TbCell.Align = "center"
            TbRow.Cells.Add(TbCell)

            TbCell = New HtmlTableCell
            TbCell.InnerHtml = drwRTotalDeffredLC.GDeffered
            TbCell.NoWrap = True
            TbCell.Align = "center"
            TbRow.Cells.Add(TbCell)

            TbCell = New HtmlTableCell
            TbCell.InnerHtml = drwRTotalDeffredLC.AmounDefferd.ToString("N0")
            TbCell.NoWrap = True
            TbCell.Align = "center"
            TbRow.Cells.Add(TbCell)


            Dim strTemp As String = ""

            If drwUserLogin.IsDataAdmin = False AndAlso drwUserLogin.IsDataUserAdmin = True Then


                strTemp = "<select name='cmbAssignPerson" & CStr(i) & "' style='WIDTH:50;HEIGHT:25px;background-color:rgba(0,0,0,0.05)'>"
                strTemp = strTemp & "<Option value='-1' selected>انتخاب شود</Option>"

                dtblPerson = tadpPerson.GetData(1, cmbBranch.SelectedValue, -1)

                Dim j As Integer = 0
                For Each drwPerson As BusinessObject.dstUser.spr_User_CheckBranch_SelectRow In dtblPerson

                    strTemp = strTemp & "<Option value='" & drwPerson.ID & "(" & drwRTotalDeffredLC.LCNumber & ")" & drwRTotalDeffredLC.CustomerNO & "/'>" & drwPerson.Username & "</Option>"


                Next

                strTemp = strTemp & "</Select>"

            ElseIf drwUserLogin.IsDataAdmin = True Then



                strTemp = "<select name='cmbAssignPerson" & CStr(i) & "' style='WIDTH:50;HEIGHT:25px;background-color:rgba(0,0,0,0.05)'>"
                strTemp = strTemp & "<Option value='-1' selected>انتخاب شود</Option>"

                dtblPerson = tadpPerson.GetData(1, cmbBranch.SelectedValue, -1)
                Dim j As Integer = 0
                For Each drwPerson As BusinessObject.dstUser.spr_User_CheckBranch_SelectRow In dtblPerson

                    strTemp = strTemp & "<Option value='" & drwPerson.ID & "(" & drwRTotalDeffredLC.LCNumber & ")" & drwRTotalDeffredLC.CustomerNO & "/'>" & drwPerson.Username & "</Option>"

                Next

                strTemp = strTemp & "</Select>"

            ElseIf drwUserLogin.IsDataAdmin = False AndAlso drwUserLogin.IsDataUserAdmin = False Then


                strTemp = "<select name='cmbAssignPerson" & CStr(i) & "' style='WIDTH:50;HEIGHT:25px;background-color:rgba(0,0,0,0.05)'>"
                strTemp = strTemp & "<Option value='-1' selected>انتخاب شود</Option>"

                dtblPerson = tadpPerson.GetData(1, cmbBranch.SelectedValue, -1)
                Dim j As Integer = 0
                For Each drwPerson As BusinessObject.dstUser.spr_User_CheckBranch_SelectRow In dtblPerson

                    strTemp = strTemp & "<Option value='" & drwPerson.ID & "(" & drwRTotalDeffredLC.LCNumber & ")" & drwRTotalDeffredLC.CustomerNO & "/'>" & drwPerson.Username & "</Option>"


                Next

                strTemp = strTemp & "</Select>"



            End If


            TbCell = New HtmlTableCell
            TbCell.InnerHtml = strTemp
            TbCell.NoWrap = True
            TbCell.Align = "center"
            TbRow.Cells.Add(TbCell)



            i += 1



            tblNumbers.Rows.Add(TbRow)

        Next








    End Sub

    ''Protected Sub cmbFiles_DataBound(sender As Object, e As EventArgs) Handles cmbFiles.DataBound
    ''    Dim li As New ListItem
    ''    li.Text = "----"
    ''    li.Value = -1
    ''    cmbFiles.Items.Insert(0, li)
    ''End Sub

    Protected Sub cmbProvince_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbProvince.SelectedIndexChanged

        odsBranch.SelectParameters.Item("Action").DefaultValue = 2
        odsBranch.SelectParameters.Item("ProvinceID").DefaultValue = cmbProvince.SelectedValue
        odsBranch.DataBind()

        cmbBranch.DataSourceID = "odsBranch"
        cmbBranch.DataTextField = "BrnachCode"
        cmbBranch.DataValueField = "ID"


        cmbBranch.DataBind()

        cmbPerson.DataBind()

    End Sub

    Protected Sub cmbBranch_DataBound(sender As Object, e As EventArgs) Handles cmbBranch.DataBound
        Dim li As New ListItem
        li.Text = "---"
        li.Value = -1
        cmbBranch.Items.Insert(0, li)
    End Sub

    Protected Sub cmbBranch_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbBranch.SelectedIndexChanged

        odsPerson.SelectParameters.Item("Action").DefaultValue = 1
        odsPerson.SelectParameters.Item("BranchID").DefaultValue = cmbBranch.SelectedValue
        odsPerson.SelectParameters.Item("ProvinceID").DefaultValue = -1

        cmbPerson.DataBind()


    End Sub

    Protected Sub cmbPerson_DataBound(sender As Object, e As EventArgs) Handles cmbPerson.DataBound
        Dim li As New ListItem
        li.Text = "---"
        li.Value = -1
        cmbPerson.Items.Insert(0, li)
    End Sub

    Private Sub Bootstrap_Panel1_Panel_Cancel_Click(sender As Object, e As EventArgs) Handles Bootstrap_Panel1.Panel_Cancel_Click

        Response.Redirect("HandyFollowManagement.aspx")

    End Sub
End Class