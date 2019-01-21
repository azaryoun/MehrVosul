Public Class UserNew
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
                divUserType.Visible = True

                cmbBranch.Enabled = True

                cmbUserType.Items.Add(New ListItem("Item Access(دسترسی ادمین-سطح استان یا مدیر شعب)", 2))
                cmbUserType.Items.Add(New ListItem("Full Access", 3))
                cmbBranch.SelectedIndex = 0


                Dim tadpAccessGroupList As New BusinessObject.dstAccessgroupTableAdapters.spr_Accessgroup_List_SelectTableAdapter
                Dim dtblAccessGroupList As BusinessObject.dstAccessgroup.spr_Accessgroup_List_SelectDataTable = Nothing

                dtblAccessGroupList = tadpAccessGroupList.GetData(1, -1)

                Dim trNodeProvince As New TreeNode
                trAccessGroup.Nodes.Add(trNodeProvince)

                For Each drwAccessGroupList As BusinessObject.dstAccessgroup.spr_Accessgroup_List_SelectRow In dtblAccessGroupList.Rows
                    Dim trNodeBranch As New TreeNode

                    trNodeBranch.Text = "&nbsp;&nbsp;" & drwAccessGroupList.Desp
                    trNodeBranch.Value = drwAccessGroupList.ID
                    trNodeBranch.ShowCheckBox = True
                    trNodeBranch.SelectAction = TreeNodeSelectAction.None
                    trNodeProvince.ChildNodes.Add(trNodeBranch)


                Next drwAccessGroupList

                ''Atieh Admin
            ElseIf drwUserLogin.FK_AccessGroupID = 3438 Then


                cmbBranch.Enabled = True
                cmbBranch.SelectedIndex = 0


                Dim tadpAccessGroupList As New BusinessObject.dstAccessgroupTableAdapters.spr_Accessgroup_List_SelectTableAdapter
                Dim dtblAccessGroupList As BusinessObject.dstAccessgroup.spr_Accessgroup_List_SelectDataTable = Nothing

                dtblAccessGroupList = tadpAccessGroupList.GetData(2, drwUserLogin.ID)

                Dim trNodeProvince As New TreeNode
                trAccessGroup.Nodes.Add(trNodeProvince)

                For Each drwAccessGroupList As BusinessObject.dstAccessgroup.spr_Accessgroup_List_SelectRow In dtblAccessGroupList.Rows


                    Dim trNodeBranch As New TreeNode

                    trNodeBranch.Text = "&nbsp;&nbsp;" & drwAccessGroupList.Desp
                    trNodeBranch.Value = drwAccessGroupList.ID
                    trNodeBranch.ShowCheckBox = True
                    trNodeBranch.SelectAction = TreeNodeSelectAction.None
                    trNodeProvince.ChildNodes.Add(trNodeBranch)


                Next drwAccessGroupList

            ElseIf drwUserLogin.IsDataAdmin = True AndAlso drwUserLogin.IsItemAdmin = False Then

                cmbUserType.Items.Add(New ListItem("Data Access", 1))
                cmbBranch.Enabled = True
                cmbBranch.SelectedIndex = 0


                Dim tadpAccessGroupList As New BusinessObject.dstAccessgroupTableAdapters.spr_Accessgroup_List_SelectTableAdapter
                Dim dtblAccessGroupList As BusinessObject.dstAccessgroup.spr_Accessgroup_List_SelectDataTable = Nothing

                dtblAccessGroupList = tadpAccessGroupList.GetData(1, -1)

                Dim trNodeProvince As New TreeNode
                trAccessGroup.Nodes.Add(trNodeProvince)

                For Each drwAccessGroupList As BusinessObject.dstAccessgroup.spr_Accessgroup_List_SelectRow In dtblAccessGroupList.Rows
                    Dim trNodeBranch As New TreeNode

                    trNodeBranch.Text = "&nbsp;&nbsp;" & drwAccessGroupList.Desp
                    trNodeBranch.Value = drwAccessGroupList.ID
                    trNodeBranch.ShowCheckBox = True
                    trNodeBranch.SelectAction = TreeNodeSelectAction.None
                    trNodeProvince.ChildNodes.Add(trNodeBranch)


                Next drwAccessGroupList

            ElseIf drwUserLogin.IsDataUserAdmin = True Then

                ''check the access Group id
                Dim tadpAccessgroupUser As New BusinessObject.dstAccessgroupUserTableAdapters.spr_AccessgroupUserByID_SelectTableAdapter
                Dim dtblAccessgroupUser As BusinessObject.dstAccessgroupUser.spr_AccessgroupUserByID_SelectDataTable = Nothing

                Dim blnAdminBranch As Boolean = False
                dtblAccessgroupUser = tadpAccessgroupUser.GetData(drwUserLogin.ID, 3431)
                If dtblAccessgroupUser.Rows.Count = 0 Then
                    dtblAccessgroupUser = tadpAccessgroupUser.GetData(drwUserLogin.ID, 3436)
                    If dtblAccessgroupUser.Rows.Count > 0 Then
                        blnAdminBranch = True
                    End If
                End If


                If blnAdminBranch = False Then
                    cmbUserType.Items.Add(New ListItem("Item Access(دسترسی ادمین-سطح استان یا مدیر شعب)", 2))
                End If


                Dim tadpUserProvince As New BusinessObject.dstBranchTableAdapters.spr_Province_Check_SelectTableAdapter
                Dim dtblUserProvince As BusinessObject.dstBranch.spr_Province_Check_SelectDataTable = Nothing

                dtblUserProvince = tadpUserProvince.GetData(drwUserLogin.FK_BrnachID)

                cmbProvince.DataBind()
                cmbProvince.SelectedValue = dtblUserProvince.First.ID
                cmbProvince.Enabled = False

                odsBranch.SelectParameters.Item("Action").DefaultValue = 2
                odsBranch.SelectParameters.Item("ProvinceID").DefaultValue = cmbProvince.SelectedValue
                odsBranch.DataBind()

                cmbBranch.Enabled = True
                cmbBranch.DataSourceID = "odsBranch"
                cmbBranch.DataTextField = "BrnachCode"
                cmbBranch.DataValueField = "ID"
                cmbBranch.SelectedValue = drwUserLogin.FK_BrnachID

                If blnAdminBranch = True Then
                    cmbBranch.Enabled = False
                End If

                Dim tadpAccessGroupList As New BusinessObject.dstAccessgroupTableAdapters.spr_Accessgroup_List_SelectTableAdapter
                Dim dtblAccessGroupList As BusinessObject.dstAccessgroup.spr_Accessgroup_List_SelectDataTable = Nothing

                dtblAccessGroupList = tadpAccessGroupList.GetData(2, drwUserLogin.ID)

                Dim trNodeProvince As New TreeNode
                trAccessGroup.Nodes.Add(trNodeProvince)

                For Each drwAccessGroupList As BusinessObject.dstAccessgroup.spr_Accessgroup_List_SelectRow In dtblAccessGroupList.Rows
                    Dim trNodeBranch As New TreeNode

                    trNodeBranch.Text = "&nbsp;&nbsp;" & drwAccessGroupList.Desp
                    trNodeBranch.Value = drwAccessGroupList.ID
                    trNodeBranch.ShowCheckBox = True
                    trNodeBranch.SelectAction = TreeNodeSelectAction.None
                    trNodeProvince.ChildNodes.Add(trNodeBranch)


                Next drwAccessGroupList



            ElseIf drwUserLogin.IsHadiSystem = True Then



                Dim tadpUserProvince As New BusinessObject.dstBranchTableAdapters.spr_Province_Check_SelectTableAdapter
                Dim dtblUserProvince As BusinessObject.dstBranch.spr_Province_Check_SelectDataTable = Nothing

                dtblUserProvince = tadpUserProvince.GetData(drwUserLogin.FK_BrnachID)

                cmbProvince.DataBind()
                cmbProvince.SelectedValue = dtblUserProvince.First.ID
                cmbProvince.Enabled = True

                odsBranch.SelectParameters.Item("Action").DefaultValue = 2
                odsBranch.SelectParameters.Item("ProvinceID").DefaultValue = cmbProvince.SelectedValue
                odsBranch.DataBind()

                cmbBranch.Enabled = True
                cmbBranch.DataSourceID = "odsBranch"
                cmbBranch.DataTextField = "BrnachCode"
                cmbBranch.DataValueField = "ID"
                cmbBranch.SelectedValue = drwUserLogin.FK_BrnachID


                cmbBranch.Enabled = True


                Dim tadpAccessGroupList As New BusinessObject.dstAccessgroupTableAdapters.spr_Accessgroup_List_SelectTableAdapter
                Dim dtblAccessGroupList As BusinessObject.dstAccessgroup.spr_Accessgroup_List_SelectDataTable = Nothing

                dtblAccessGroupList = tadpAccessGroupList.GetData(2, drwUserLogin.ID)

                Dim trNodeProvince As New TreeNode
                trAccessGroup.Nodes.Add(trNodeProvince)

                For Each drwAccessGroupList As BusinessObject.dstAccessgroup.spr_Accessgroup_List_SelectRow In dtblAccessGroupList.Rows
                    Dim trNodeBranch As New TreeNode

                    trNodeBranch.Text = "&nbsp;&nbsp;" & drwAccessGroupList.Desp
                    trNodeBranch.Value = drwAccessGroupList.ID
                    trNodeBranch.ShowCheckBox = True
                    trNodeBranch.SelectAction = TreeNodeSelectAction.None
                    trNodeProvince.ChildNodes.Add(trNodeBranch)


                Next drwAccessGroupList

            End If

            cmbUserType.SelectedIndex = 0



        End If


    End Sub



    Private Sub Bootstrap_Panel1_Panel_Cancel_Click(sender As Object, e As System.EventArgs) Handles Bootstrap_Panel1.Panel_Cancel_Click
        Response.Redirect("UserManagement.aspx")
        Return
    End Sub

  
    Private Sub Bootstrap_Panel1_Panel_Save_Click(sender As Object, e As System.EventArgs) Handles Bootstrap_Panel1.Panel_Save_Click

        Dim dtblUserLogin As BusinessObject.dstUser.spr_User_Login_SelectDataTable = CType(Session("dtblUserLogin"), BusinessObject.dstUser.spr_User_Login_SelectDataTable)
        Dim drwUserLogin As BusinessObject.dstUser.spr_User_Login_SelectRow = dtblUserLogin.Rows(0)


        Dim strUsername As String = txtUsername.Text.Trim

        Dim tadpCheckUsername As New BusinessObject.dstUserTableAdapters.spr_User_CheckUsername_SelectTableAdapter
        Dim dtblCheckUsername As BusinessObject.dstUser.spr_User_CheckUsername_SelectDataTable = Nothing
        dtblCheckUsername = tadpCheckUsername.GetData(strUsername)


        If dtblCheckUsername.Rows.Count > 0 Then
            Bootstrap_Panel1.ShowMessage("نام کاربری تکراری است", True)
            Return
        End If

        Dim strPassword As String = txtPassword.Text.Trim
        Dim strAddress As String = txtAddress.Text.Trim
        Dim strNationalID As String = txtNationalID.Text.Trim
        Dim strNationalNo As String = txtNationalNo.Text
        Dim strFname As String = txtFirstName.Text.Trim
        Dim strLname As String = txtLastName.Text.Trim
        Dim strPersonCode As String = txtPersonCode.Text.Trim
        Dim strEmail As String = txtEmail.Text.Trim
        Dim blnSex As Boolean = rdoSexFemale.Checked
        Dim strTel As String = txtTel.Text.Trim
        Dim strMobile As String = txtMobile.Text.Trim
        Dim intBranchID As Integer = cmbBranch.SelectedValue


        Dim blnDataAdmin As Boolean = False
        Dim blnItemAdmin As Boolean = False

        If cmbUserType.SelectedValue = "1" Then
            blnDataAdmin = True
        ElseIf cmbUserType.SelectedValue = "2" Then
            blnItemAdmin = True
        ElseIf cmbUserType.SelectedValue = "3" Then
            blnItemAdmin = True
            blnDataAdmin = True
        End If



        Dim arrUserPhoto() As Byte = Nothing

        If fleUserPhoto.PostedFile IsNot Nothing AndAlso fleUserPhoto.PostedFile.ContentLength <> 0 Then

            If fleUserPhoto.PostedFile.ContentType.ToLower.IndexOf("png") = -1 Then
                Bootstrap_Panel1.ShowMessage("فرمت تصویر باید png باشد", True)
                Return
            End If


            Dim strmUserPhoto As IO.Stream = fleUserPhoto.PostedFile.InputStream
            ReDim arrUserPhoto(strmUserPhoto.Length)
            strmUserPhoto.Read(arrUserPhoto, 0, arrUserPhoto.Length)

        End If

        Dim blnIsPartTime As Boolean = rdoIsPartTimeYes.Checked
        Dim blnHadiUser As Boolean = chkbxHadi.Checked
        If drwUserLogin.IsHadiSystem = True Then
            blnHadiUser = True
        End If

        Dim blnAtiehUser As Boolean = chkbxAtieh.Checked

        If drwUserLogin.IsDataAdmin = False Then
            If drwUserLogin.FK_AccessGroupID = 3438 Then
                blnAtiehUser = True
            End If
        End If


        Try
            Dim qryUser As New BusinessObject.dstUserTableAdapters.QueriesTableAdapter
            Dim intNewUserID As Integer
            If blnItemAdmin = True Then

                intNewUserID = qryUser.spr_User_Insert(strUsername, strPassword, True, blnDataAdmin, False, True, strFname, strLname, strEmail, blnSex, strTel, strMobile, Date.Now, drwUserLogin.ID, strPersonCode, strNationalID, strNationalNo, strAddress, arrUserPhoto, intBranchID, blnIsPartTime, blnHadiUser, blnAtiehUser)

            Else

                intNewUserID = qryUser.spr_User_Insert(strUsername, strPassword, True, blnDataAdmin, False, False, strFname, strLname, strEmail, blnSex, strTel, strMobile, Date.Now, drwUserLogin.ID, strPersonCode, strNationalID, strNationalNo, strAddress, arrUserPhoto, intBranchID, blnIsPartTime, blnHadiUser, blnAtiehUser)

            End If

            'Dim arrSelectedGroups() As Integer = lstAccessGroups.GetSelectedIndices()

            'For i As Integer = 0 To arrSelectedGroups.Length - 1
            '    Dim qryAccessgroupUser As New BusinessObject.dstAccessgroupUserTableAdapters.QueriesTableAdapter
            '    qryAccessgroupUser.spr_AccessgroupUser_Insert(intNewUserID, lstAccessGroups.Items(arrSelectedGroups(i)).Value)
            'Next i



            For Each trNode As TreeNode In trAccessGroup.Nodes
                If trNode.ChildNodes.Count = 0 Then
                    If trNode.Checked = True Then

                        Dim qryAccessgroupUser As New BusinessObject.dstAccessgroupUserTableAdapters.QueriesTableAdapter
                        qryAccessgroupUser.spr_AccessgroupUser_Insert(intNewUserID, trNode.Value)

                    End If

                Else

                    For Each trChildNode As TreeNode In trNode.ChildNodes

                        If trChildNode.ChildNodes.Count = 0 Then
                            If trChildNode.Checked = True Then
                                Dim ChildID As Integer = CInt(trChildNode.Value)
                                Dim qryAccessgroupUser As New BusinessObject.dstAccessgroupUserTableAdapters.QueriesTableAdapter
                                qryAccessgroupUser.spr_AccessgroupUser_Insert(intNewUserID, ChildID)


                            End If

                        End If


                    Next

                End If
            Next


        Catch ex As Exception
            Response.Redirect("UserManagement.aspx?Save=NO")
            Return
        End Try

        Response.Redirect("UserManagement.aspx?Save=OK")







    End Sub

    Protected Sub cmbBranch_DataBound(sender As Object, e As EventArgs) Handles cmbBranch.DataBound
        Dim li As New ListItem
        li.Text = "---"
        li.Value = -1
        cmbBranch.Items.Insert(0, li)
    End Sub

    Protected Sub cmbProvince_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbProvince.SelectedIndexChanged
        odsBranch.SelectParameters.Item("Action").DefaultValue = 2
        odsBranch.SelectParameters.Item("ProvinceID").DefaultValue = cmbProvince.SelectedValue
        odsBranch.DataBind()

        cmbBranch.DataSourceID = "odsBranch"
        cmbBranch.DataTextField = "BrnachCode"
        cmbBranch.DataValueField = "ID"


        cmbBranch.DataBind()
    End Sub

    Protected Sub cmbProvince_DataBound(sender As Object, e As EventArgs) Handles cmbProvince.DataBound
        Dim li As New ListItem
        li.Text = "---"
        li.Value = -1
        cmbProvince.Items.Insert(0, li)
    End Sub
End Class