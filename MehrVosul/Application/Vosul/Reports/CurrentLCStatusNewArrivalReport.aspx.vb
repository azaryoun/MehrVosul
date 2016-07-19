Imports System.IO
Public Class CurrentLCStatusNewArrivalReport
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
        Bootstrap_Panel1.Enable_Display_Client_Validate = True
        Bootstrap_Panel1.Enable_Excel_Client_Validate = True


        If Page.IsPostBack = False Then

            Dim dtblUserLogin As BusinessObject.dstUser.spr_User_Login_SelectDataTable = CType(HttpContext.Current.Session("dtblUserLogin"), BusinessObject.dstUser.spr_User_Login_SelectDataTable)
            Dim drwUserLogin As BusinessObject.dstUser.spr_User_Login_SelectRow = dtblUserLogin.Rows(0)

            Dim tadpBrnachList As New BusinessObject.dstBranchTableAdapters.spr_Branch_List_SelectTableAdapter
            Dim dtblBranchList As BusinessObject.dstBranch.spr_Branch_List_SelectDataTable = Nothing

            Dim blnSingleBranch As Boolean = False

            If drwUserLogin.IsDataAdmin = True Then

                dtblBranchList = tadpBrnachList.GetData(1, -1)

            ElseIf drwUserLogin.IsDataAdmin = False And drwUserLogin.IsDataUserAdmin = True Then

                cmbProvince.SelectedValue = drwUserLogin.Fk_ProvinceID
                cmbProvince.Enabled = False
                dtblBranchList = tadpBrnachList.GetData(2, drwUserLogin.Fk_ProvinceID)

            Else

                cmbProvince.SelectedValue = drwUserLogin.Fk_ProvinceID
                cmbProvince.Enabled = False
                dtblBranchList = tadpBrnachList.GetData(2, drwUserLogin.Fk_ProvinceID)
                blnSingleBranch = True

            End If

            Dim strBrnachesList As String = ""

            If blnSingleBranch = True Then

                Dim tadpBranch As New BusinessObject.dstBranchTableAdapters.spr_Branch_SelectTableAdapter
                Dim dtblBranch As BusinessObject.dstBranch.spr_Branch_SelectDataTable = Nothing

                dtblBranch = tadpBranch.GetData(drwUserLogin.FK_BrnachID)

                strBrnachesList &= "<div class='checkbox'> <label> <input type='checkbox' value='" & drwUserLogin.FK_BrnachID & "' name='chklstBranch" & dtblBranch.First.ID & "'><i class='fa " & dtblBranch.First.ID & " fa-1x'></i> " & dtblBranch.First.BrnachCode & "(" & dtblBranch.First.BranchName & ")" & "</label></div>"


            Else
                For Each drwBranchList As BusinessObject.dstBranch.spr_Branch_List_SelectRow In dtblBranchList.Rows

                    strBrnachesList &= "<div class='checkbox'> <label> <input type='checkbox' value='" & drwBranchList.ID & "' name='chklstBranch" & drwBranchList.ID & "'><i class='fa " & drwBranchList.ID & " fa-1x'></i> " & drwBranchList.BrnachCode & "</label></div>"
                Next drwBranchList

            End If

            divBranches.InnerHtml = strBrnachesList


        Else

            Dim dtblUserLogin As BusinessObject.dstUser.spr_User_Login_SelectDataTable = CType(HttpContext.Current.Session("dtblUserLogin"), BusinessObject.dstUser.spr_User_Login_SelectDataTable)
            Dim drwUserLogin As BusinessObject.dstUser.spr_User_Login_SelectRow = dtblUserLogin.Rows(0)

            Dim tadpBrnachList As New BusinessObject.dstBranchTableAdapters.spr_Branch_List_SelectTableAdapter
            Dim dtblBranchList As BusinessObject.dstBranch.spr_Branch_List_SelectDataTable = Nothing

            Dim blnSingleBranch As Boolean = False

            If drwUserLogin.IsDataAdmin = True Then

                If cmbProvince.SelectedValue = -1 Then

                    dtblBranchList = tadpBrnachList.GetData(1, -1)

                Else
                    dtblBranchList = tadpBrnachList.GetData(2, CInt(cmbProvince.SelectedValue))

                End If


            ElseIf drwUserLogin.IsDataAdmin = False And drwUserLogin.IsDataUserAdmin = True Then

                cmbProvince.SelectedValue = drwUserLogin.Fk_ProvinceID
                cmbProvince.Enabled = False
                dtblBranchList = tadpBrnachList.GetData(2, drwUserLogin.Fk_ProvinceID)

            Else

                cmbProvince.SelectedValue = drwUserLogin.Fk_ProvinceID
                cmbProvince.Enabled = False
                dtblBranchList = tadpBrnachList.GetData(2, drwUserLogin.Fk_ProvinceID)
                blnSingleBranch = True

            End If

            Dim strBrnachesList As String = ""

            For Each drwBranchList As BusinessObject.dstBranch.spr_Branch_List_SelectRow In dtblBranchList.Rows

                Dim boolChecked As Boolean = False


                For i As Integer = 0 To Request.Form.Keys.Count - 2

                    If Request.Form.Keys(i).StartsWith("chklstBranch") = True Then

                        If CInt(Request.Form(i)) = drwBranchList.ID Then
                            boolChecked = True
                            Exit For
                        End If

                    End If

                Next

                If blnSingleBranch = False Then

                    If boolChecked = True Then
                        strBrnachesList &= "<div class='checkbox'> <label> <input type='checkbox' checked='checked' value='" & drwBranchList.ID & "' name='chklstBranch" & drwBranchList.ID & "'><i class='fa " & drwBranchList.ID & " fa-1x'></i> " & drwBranchList.BrnachCode & "</label></div>"

                    Else

                        strBrnachesList &= "<div class='checkbox'> <label> <input type='checkbox' value='" & drwBranchList.ID & "' name='chklstBranch" & drwBranchList.ID & "'><i class='fa " & drwBranchList.ID & " fa-1x'></i> " & drwBranchList.BrnachCode & "</label></div>"
                    End If
                End If


            Next drwBranchList

            divBranches.InnerHtml = strBrnachesList


            If blnSingleBranch = True Then

                Dim tadpBranch As New BusinessObject.dstBranchTableAdapters.spr_Branch_SelectTableAdapter
                Dim dtblBranch As BusinessObject.dstBranch.spr_Branch_SelectDataTable = Nothing

                dtblBranch = tadpBranch.GetData(drwUserLogin.FK_BrnachID)

                strBrnachesList &= "<div class='checkbox'> <label> <input type='checkbox' checked='checked' value='" & drwUserLogin.FK_BrnachID & "' name='chklstBranch" & dtblBranch.First.ID & "'><i class='fa " & dtblBranch.First.ID & " fa-1x'></i> " & dtblBranch.First.BrnachCode & "(" & dtblBranch.First.BranchName & ")" & "</label></div>"
                divBranches.InnerHtml = strBrnachesList



            End If

        End If

    End Sub

    

    Protected Sub cmbLoanType_DataBound(sender As Object, e As EventArgs) Handles cmbLoanType.DataBound
        Dim li As New ListItem
        li.Text = "(همه انواع وام)"
        li.Value = -1
        cmbLoanType.Items.Insert(0, li)
    End Sub

    Private Sub Bootstrap_Panel1_Panel_Display_Click(sender As Object, e As System.EventArgs) Handles Bootstrap_Panel1.Panel_Display_Click

        Bootstrap_Panel1.ClearMessage()

        Dim tadpReport As New BusinessObject.dstReportTableAdapters.spr_Report_CurrentLCStatus_NewArrivalWarningInterval_SelectTableAdapter
        Dim dtblReport As BusinessObject.dstReport.spr_Report_CurrentLCStatus_NewArrivalWarningInterval_SelectDataTable = Nothing


        Dim intBranchID As Integer = -1
        Dim intLoanTypeID As Integer = -1

        Dim blnHasBranch As Boolean = False
        Dim strBranchwhere As String = ""
        Dim strWhere As String = ""
        Dim blnProvince As Boolean = False

        If cmbProvince.SelectedValue <> -1 And chkBranchSelectAll.Checked = True Then

            blnProvince = True
            strWhere = " and  Vosul.tbl_Branch.Fk_ProvinceID= " & cmbProvince.SelectedValue

        End If


        If blnProvince = False Then


            For i As Integer = 0 To Request.Form.Keys.Count - 1

                If Request.Form.Keys(i).StartsWith("chklstBranch") = True Then

                    blnHasBranch = True
                    If strBranchwhere <> "" Then
                        strBranchwhere = strBranchwhere & " or " & "Vosul.tbl_Loan.FK_BranchID= " & Request.Form(i)
                    Else
                        strBranchwhere = " and (Vosul.tbl_Loan.FK_BranchID= " & Request.Form(i)
                    End If

                End If

            Next

            If strBranchwhere <> "" Then

                strWhere = strBranchwhere & ")"

            End If
        End If

        If cmbLoanType.SelectedValue <> -1 Then

            strWhere = strWhere & " and Vosul.tbl_Loan.FK_LoanTypeID= " & cmbLoanType.SelectedValue

        End If


        dtblReport = tadpReport.GetData(2, strWhere)


        If dtblReport.Rows.Count > 0 Then

            divResult.Visible = True

            ''get total report
            Dim intCount As Integer = 0


            For Each drwReport As BusinessObject.dstReport.spr_Report_CurrentLCStatus_NewArrivalWarningInterval_SelectRow In dtblReport.Rows

                intCount += 1
                Dim TbRow As New HtmlTableRow
                Dim TbCell As HtmlTableCell

                TbCell = New HtmlTableCell
                TbCell.InnerHtml = "<input type='hidden' id='hdnAmounts" & CStr(intCount) & "' >" & CStr(intCount)
                TbCell.Align = "center"
                TbCell.NoWrap = True
                TbRow.Cells.Add(TbCell)


                TbCell = New HtmlTableCell
                TbCell.InnerHtml = drwReport.CustomerNo
                TbCell.NoWrap = True
                TbCell.Align = "center"
                TbRow.Cells.Add(TbCell)

                TbCell = New HtmlTableCell
                TbCell.InnerHtml = drwReport.CustomerName
                TbCell.NoWrap = True
                TbCell.Align = "center"
                TbRow.Cells.Add(TbCell)

                TbCell = New HtmlTableCell
                TbCell.InnerHtml = drwReport.MobileNo
                TbCell.NoWrap = True
                TbCell.Align = "center"
                TbRow.Cells.Add(TbCell)

                TbCell = New HtmlTableCell
                TbCell.InnerHtml = drwReport.TelephoneHome
                TbCell.NoWrap = True
                TbCell.Align = "center"
                TbRow.Cells.Add(TbCell)


                TbCell = New HtmlTableCell
                TbCell.InnerHtml = drwReport.LoanNumber
                TbCell.NoWrap = True
                TbCell.Align = "center"
                TbRow.Cells.Add(TbCell)

                TbCell = New HtmlTableCell
                TbCell.InnerHtml = drwReport.BranchName
                TbCell.NoWrap = True
                TbCell.Align = "center"
                TbRow.Cells.Add(TbCell)


                TbCell = New HtmlTableCell
                TbCell.InnerHtml = drwReport.State
                TbCell.NoWrap = True
                TbCell.Align = "center"
                TbRow.Cells.Add(TbCell)

                TbCell = New HtmlTableCell
                TbCell.InnerHtml = mdlGeneral.GetPersianDate(drwReport.Date_P)
                TbCell.NoWrap = True
                TbCell.Align = "center"
                TbRow.Cells.Add(TbCell)


                TbCell = New HtmlTableCell
                TbCell.InnerHtml = drwReport.NotPiadDurationDay
                TbCell.NoWrap = True
                TbCell.Align = "center"
                TbRow.Cells.Add(TbCell)

                TbCell = New HtmlTableCell
                TbCell.InnerHtml = drwReport.LoanTypeName
                TbCell.NoWrap = True
                TbCell.Align = "center"
                TbRow.Cells.Add(TbCell)

                tblResult.Rows.Add(TbRow)

            Next

            Session("CurrentLCStatusNewArrivalReport") = dtblReport

        Else

            divResult.Visible = False


        End If


    End Sub

    Private Sub Bootstrap_Panel1_Panel_Excel_Click(sender As Object, e As System.EventArgs) Handles Bootstrap_Panel1.Panel_Excel_Click
        Try
            If Session("CurrentLCStatusNewArrivalReport") IsNot Nothing Then
                Dim dtblUserLogin As BusinessObject.dstUser.spr_User_Login_SelectDataTable = CType(Session("dtblUserLogin"), BusinessObject.dstUser.spr_User_Login_SelectDataTable)
                Dim drwUserLogin As BusinessObject.dstUser.spr_User_Login_SelectRow = dtblUserLogin.Rows(0)


                Dim strPath As String = Server.MapPath("") & "\TempFile\" & drwUserLogin.ID & "\"
                Dim FileName As String = "CurrentLCStatusNewArrivalReport-" & drwUserLogin.ID.ToString()

                Dim tblCSVResult As DataTable = Session("CurrentLCStatusNewArrivalReport")
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
                    Session("CurrentLCStatusNewArrivalReport") = Nothing
                End If
            Else
                Bootstrap_Panel1.ShowMessage("امکان انتقال گزارش به فایل اکسل وجود ندارد", False)
                Session("CurrentLCStatusNewArrivalReport") = Nothing
            End If
        Catch ex As Exception
            Bootstrap_Panel1.ShowMessage("امکان انتقال گزارش به فایل اکسل وجود ندارد", False)
            Session("CurrentLCStatusNewArrivalReport") = Nothing
        End Try
    End Sub

    
    Protected Sub cmbProvince_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbProvince.SelectedIndexChanged

        Dim tadpBrnachList As New BusinessObject.dstBranchTableAdapters.spr_Branch_List_SelectTableAdapter
        Dim dtblBranchList As BusinessObject.dstBranch.spr_Branch_List_SelectDataTable = Nothing
        If cmbProvince.SelectedValue = -1 Then
            dtblBranchList = tadpBrnachList.GetData(1, -1)
        Else
            dtblBranchList = tadpBrnachList.GetData(2, cmbProvince.SelectedValue)
        End If

        Dim strBrnachesList As String = ""


        For Each drwBranchList As BusinessObject.dstBranch.spr_Branch_List_SelectRow In dtblBranchList.Rows
            strBrnachesList &= "<div class='checkbox'> <label> <input type='checkbox' value='" & drwBranchList.ID & "' name='chklstBranch" & drwBranchList.ID & "'><i class='fa " & drwBranchList.ID & " fa-1x'></i> " & drwBranchList.BrnachCode & "</label></div>"
        Next drwBranchList

        divBranches.InnerHtml = strBrnachesList
        chkBranchSelectAll.Checked = False

    End Sub

    Protected Sub cmbProvince_DataBound(sender As Object, e As EventArgs) Handles cmbProvince.DataBound
        Dim li As New ListItem
        li.Text = "(همه استان ها)"
        li.Value = -1
        cmbProvince.Items.Insert(0, li)
    End Sub
End Class