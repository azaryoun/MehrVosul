Public Class UserMagic
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

            If drwUserLogin.IsDataAdmin = True AndAlso drwUserLogin.IsItemAdmin = True Then

                'cmbBranch.Enabled = True

                'cmbBranch.SelectedIndex = 0

                odsAccessGroups.SelectParameters.Item("Action").DefaultValue = 1
                odsAccessGroups.SelectParameters.Item("UserID").DefaultValue = -1
                odsAccessGroups.DataBind()

                lstAccessGroups.DataBind()

            ElseIf drwUserLogin.IsDataAdmin = True AndAlso drwUserLogin.IsItemAdmin = False Then


                'cmbBranch.Enabled = True
                'cmbBranch.SelectedIndex = 0

                odsAccessGroups.SelectParameters.Item("Action").DefaultValue = 1
                odsAccessGroups.SelectParameters.Item("UserID").DefaultValue = -1
                odsAccessGroups.DataBind()

                lstAccessGroups.DataBind()

            ElseIf drwUserLogin.IsDataUserAdmin = True Then

                '' cmbUserType.Items.Add(New ListItem("Item Access", 2))

                Dim tadpUserProvince As New BusinessObject.dstBranchTableAdapters.spr_Province_Check_SelectTableAdapter
                Dim dtblUserProvince As BusinessObject.dstBranch.spr_Province_Check_SelectDataTable = Nothing

                dtblUserProvince = tadpUserProvince.GetData(drwUserLogin.FK_BrnachID)

                'cmbProvince.DataBind()
                'cmbProvince.SelectedValue = dtblUserProvince.First.ID
                'cmbProvince.Enabled = False

                'odsBranch.SelectParameters.Item("Action").DefaultValue = 2
                'odsBranch.SelectParameters.Item("ProvinceID").DefaultValue = cmbProvince.SelectedValue
                'odsBranch.DataBind()

                'cmbBranch.Enabled = True
                'cmbBranch.DataSourceID = "odsBranch"
                'cmbBranch.DataTextField = "BrnachCode"
                'cmbBranch.DataValueField = "ID"
                'cmbBranch.SelectedValue = drwUserLogin.FK_BrnachID

                odsAccessGroups.SelectParameters.Item("Action").DefaultValue = 2
                odsAccessGroups.SelectParameters.Item("UserID").DefaultValue = drwUserLogin.ID
                odsAccessGroups.DataBind()

                lstAccessGroups.DataBind()




            End If





        End If
    End Sub

    Private Sub Bootstrap_Panel1_Panel_Save_Click(sender As Object, e As EventArgs) Handles Bootstrap_Panel1.Panel_Save_Click
        Dim dtblUserLogin As BusinessObject.dstUser.spr_User_Login_SelectDataTable = CType(Session("dtblUserLogin"), BusinessObject.dstUser.spr_User_Login_SelectDataTable)
        Dim drwUserLogin As BusinessObject.dstUser.spr_User_Login_SelectRow = dtblUserLogin.Rows(0)


        If fleUploadFile.PostedFile.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet" Or fleUploadFile.PostedFile.ContentType = "application/vnd.ms-excel" Then '
            Dim strPath As String = Server.MapPath("") & "\Temp\" & fleUploadFile.PostedFile.FileName.Substring(fleUploadFile.PostedFile.FileName.LastIndexOf("\") + 1)
            fleUploadFile.PostedFile.SaveAs(strPath)

            Dim dtExcelName As New DataTable

            If fleUploadFile.PostedFile.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet" Then '"application/vnd.ms-excel" Then
                dtExcelName = ReadExcel2007(strPath)
            Else
                dtExcelName = ReadExcel2003(strPath, "")
            End If

            If dtExcelName.Rows.Count > 0 Then
                Try
                    Dim qryUser As New BusinessObject.dstUserTableAdapters.QueriesTableAdapter

                    Dim tadpUser As New BusinessObject.dstUserTableAdapters.spr_User_CheckUsername_SelectTableAdapter
                    Dim dtblUser As BusinessObject.dstUser.spr_User_CheckUsername_SelectDataTable = Nothing

                    Dim strMessge As String = ""
                    Dim strDoubleUserName As String = ""
                    Dim strEmptyUserName As String = ""
                    Dim strEmptyBranchCode As String = ""

                    For i = 1 To dtExcelName.Rows.Count - 1
                        Dim strName As String = dtExcelName.Rows(i)(0).ToString()
                        Dim strLastName As String = dtExcelName.Rows(i)(1).ToString()
                        Dim strUserName As String = dtExcelName.Rows(i)(2).ToString()

                        ''check user  Name duplicate
                        If strUserName.Trim() <> "" Then
                            dtblUser = tadpUser.GetData(strUserName)
                            If dtblUser.Count > 0 Then

                                strDoubleUserName &= "،نام کاربری " & strUserName
                                Continue For
                            End If
                        Else

                            strEmptyUserName &= "، نام کاربری ردیف " & i.ToString()
                            Continue For

                        End If


                        Dim strProvinceCode As String
                        Dim intProvinceID As Integer = -1

                        strProvinceCode = dtExcelName.Rows(i)(3).ToString()


                        Dim strBranchCode As String
                        Dim intBranchID As Integer = -1

                        strBranchCode = dtExcelName.Rows(i)(4).ToString()
                        If strBranchCode.Trim() <> "" Then
                            Dim dtblBranch As BusinessObject.dstBranch.spr_Branch_ByCode_SelectDataTable = Nothing
                            Dim tadpBranch As New BusinessObject.dstBranchTableAdapters.spr_Branch_ByCode_SelectTableAdapter


                            dtblBranch = tadpBranch.GetData(strBranchCode)
                            intBranchID = dtblBranch.First.ID
                        Else
                            strEmptyBranchCode &= "کد شعبه ردیف " & i.ToString()
                            Continue For
                        End If


                        Dim intUserID As Integer = qryUser.spr_User_Insert(strUserName, "123", True, False, False, False, strName, strLastName, "", False, "", "", Date.Now, drwUserLogin.ID, strUserName, "", "", "", Nothing, intBranchID, False)

                        Dim arrSelectedGroups() As Integer = lstAccessGroups.GetSelectedIndices()

                        For j As Integer = 0 To arrSelectedGroups.Length - 1
                            Dim qryAccessgroupUser As New BusinessObject.dstAccessgroupUserTableAdapters.QueriesTableAdapter
                            qryAccessgroupUser.spr_AccessgroupUser_Insert(intUserID, lstAccessGroups.Items(arrSelectedGroups(j)).Value)
                        Next j
                    Next

                    If strEmptyUserName <> "" Then

                        strMessge &= strEmptyUserName & " وارد نشده است "

                    End If

                    If strEmptyBranchCode <> "" Then

                        strMessge &= strEmptyBranchCode & " وارد نشده است "

                    End If


                    If strDoubleUserName <> "" Then

                        strMessge &= strDoubleUserName & "تکراری می باشد"

                    End If

                    If strMessge <> "" Then


                        Bootstrap_Panel1.ShowMessage(strMessge, True)
                        Return

                    End If





                Catch ex As Exception

                    Bootstrap_Panel1.ShowMessage(ex.Message, True)

                End Try



            End If
        Else

            Bootstrap_Panel1.ShowMessage("فرمت فایل بایستی اکسل باشد", True)
            Return
        End If

        Response.Redirect("UserManagement.aspx?Save=OK")

    End Sub

    ''Protected Sub cmbProvince_DataBound(sender As Object, e As EventArgs) Handles cmbProvince.DataBound
    ''    Dim li As New ListItem
    ''    li.Text = "---"
    ''    li.Value = -1
    ''    cmbProvince.Items.Insert(0, li)
    ''End Sub

    ''Protected Sub cmbBranch_DataBound(sender As Object, e As EventArgs) Handles cmbBranch.DataBound
    ''    Dim li As New ListItem
    ''    li.Text = "---"
    ''    li.Value = -1
    ''    cmbBranch.Items.Insert(0, li)
    ''End Sub

    ''Protected Sub cmbProvince_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbProvince.SelectedIndexChanged
    ''    odsBranch.SelectParameters.Item("Action").DefaultValue = 2
    ''    odsBranch.SelectParameters.Item("ProvinceID").DefaultValue = cmbProvince.SelectedValue
    ''    odsBranch.DataBind()

    ''    cmbBranch.DataSourceID = "odsBranch"
    ''    cmbBranch.DataTextField = "BrnachCode"
    ''    cmbBranch.DataValueField = "ID"


    ''    cmbBranch.DataBind()
    ''End Sub

    ''Protected Sub chkbxProvince_CheckedChanged(sender As Object, e As EventArgs) Handles chkbxProvince.CheckedChanged
    ''    If chkbxProvince.Checked Then
    ''        cmbProvince.Enabled = True
    ''        cmbBranch.Enabled = True
    ''    Else
    ''        cmbProvince.Enabled = False
    ''        cmbBranch.Enabled = False
    ''    End If

    ''End Sub

    Protected Sub lnkbtnSample_Click(sender As Object, e As EventArgs) Handles lnkbtnSample.Click
        Response.Clear()
        Response.ContentType = "Application/xls"
        Response.AppendHeader("Content-Disposition", "attachment; filename=sample.xlsx")
        Response.WriteFile(Server.MapPath("~") & "Application\Administration\User\Sample\sample.xlsx")
        Response.End()
    End Sub

    Private Sub Bootstrap_Panel1_Panel_Cancel_Click(sender As Object, e As EventArgs) Handles Bootstrap_Panel1.Panel_Cancel_Click
        Response.Redirect("UserManagement.aspx")
        Return
    End Sub
End Class