Public Class UserEdit
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

            Dim tadpAccessGroupList As New BusinessObject.dstAccessgroupTableAdapters.spr_Accessgroup_List_SelectTableAdapter
            Dim dtblAccessGroupList As BusinessObject.dstAccessgroup.spr_Accessgroup_List_SelectDataTable = Nothing


            If drwUserLogin.IsDataAdmin = True AndAlso drwUserLogin.IsItemAdmin = True Then

                divUserType.Visible = True

                cmbBranch.Enabled = True
                cmbUserType.Items.Add(New ListItem("Normal Access(دسترسی نرمال-سطح شعبه)", 0))
                cmbUserType.Items.Add(New ListItem("Item Access(دسترسی ادمین-سطح استان یا مدیر شعب)", 2))
                cmbUserType.Items.Add(New ListItem("Full Access", 3))

                cmbBranch.SelectedIndex = 0

                dtblAccessGroupList = tadpAccessGroupList.GetData(1, -1)


                ''Atieh Admin
            ElseIf drwUserLogin.FK_AccessGroupID = 3438 Then


                cmbBranch.Enabled = True
                cmbBranch.SelectedIndex = 0

                dtblAccessGroupList = tadpAccessGroupList.GetData(2, drwUserLogin.ID)



            ElseIf drwUserLogin.IsDataAdmin = True AndAlso drwUserLogin.IsItemAdmin = False Then
                cmbUserType.Items.Add(New ListItem("Normal Access(دسترسی نرمال-سطح شعبه)", 0))
                cmbUserType.Items.Add(New ListItem("Item Access(دسترسی ادمین-سطح استان یا مدیر شعب)", 2))

                cmbBranch.Enabled = True
                cmbBranch.SelectedIndex = 0


                dtblAccessGroupList = tadpAccessGroupList.GetData(1, -1)


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

                cmbUserType.Items.Add(New ListItem("Normal Access(دسترسی نرمال-سطح شعبه)", 0))
                If blnAdminBranch = False Then
                    cmbUserType.Items.Add(New ListItem("Item Access(دسترسی ادمین-سطح استان یا مدیر شعب)", 2))

                End If

                cmbUserType.SelectedValue = 1
                Dim tadpUserProvince As New BusinessObject.dstBranchTableAdapters.spr_Province_Check_SelectTableAdapter
                Dim dtblUserProvince As BusinessObject.dstBranch.spr_Province_Check_SelectDataTable = Nothing

                dtblUserProvince = tadpUserProvince.GetData(drwUserLogin.FK_BrnachID)

                cmbProvince.DataBind()
                cmbProvince.SelectedValue = dtblUserProvince.First.ID
                cmbProvince.Enabled = False

                odsBranch.SelectParameters.Item("Action").DefaultValue = 2
                odsBranch.SelectParameters.Item("ProvinceID").DefaultValue = cmbProvince.SelectedValue
                odsBranch.DataBind()

                If blnAdminBranch = True Then
                    cmbBranch.Enabled = False

                End If
                cmbBranch.DataSourceID = "odsBranch"
                cmbBranch.DataTextField = "BrnachCode"
                cmbBranch.DataValueField = "ID"
                cmbBranch.SelectedValue = drwUserLogin.FK_BrnachID


                dtblAccessGroupList = tadpAccessGroupList.GetData(2, drwUserLogin.ID)




            ElseIf drwUserLogin.IsHadiSystem = True Then

                cmbUserType.Items.Add(New ListItem("Normal Access(دسترسی نرمال-سطح شعبه)", 0))

                Dim tadpUserProvince As New BusinessObject.dstBranchTableAdapters.spr_Province_Check_SelectTableAdapter
                Dim dtblUserProvince As BusinessObject.dstBranch.spr_Province_Check_SelectDataTable = Nothing

                dtblUserProvince = tadpUserProvince.GetData(drwUserLogin.FK_BrnachID)

                cmbProvince.DataBind()
                cmbProvince.SelectedValue = dtblUserProvince.First.ID
                cmbProvince.Enabled = True

                ''odsBranch.SelectParameters.Item("Action").DefaultValue = 2
                ''odsBranch.SelectParameters.Item("ProvinceID").DefaultValue = cmbProvince.SelectedValue
                ''odsBranch.DataBind()

                ''cmbBranch.Enabled = True
                ''cmbBranch.DataSourceID = "odsBranch"
                ''cmbBranch.DataTextField = "BrnachCode"
                ''cmbBranch.DataValueField = "ID"
                ''cmbBranch.SelectedValue = drwUserLogin.FK_BrnachID


                ''cmbBranch.Enabled = True

                dtblAccessGroupList = tadpAccessGroupList.GetData(2, drwUserLogin.ID)


                'Dim trNodeProvince As New TreeNode
                'trAccessGroup.Nodes.Add(trNodeProvince)

                'For Each drwAccessGroupList As BusinessObject.dstAccessgroup.spr_Accessgroup_List_SelectRow In dtblAccessGroupList.Rows
                '    Dim trNodeBranch As New TreeNode

                '    trNodeBranch.Text = "&nbsp;&nbsp;" & drwAccessGroupList.Desp
                '    trNodeBranch.Value = drwAccessGroupList.ID
                '    trNodeBranch.ShowCheckBox = True
                '    trNodeBranch.SelectAction = TreeNodeSelectAction.None
                '    trNodeProvince.ChildNodes.Add(trNodeBranch)


                'Next drwAccessGroupList

            End If



            If Session("intEditUserID") Is Nothing Then
                Response.Redirect("UserManagement.aspx")
                Return
            End If

            Dim intEditUserID As Integer = CInt(Session("intEditUserID"))

            Dim tadpUser As New BusinessObject.dstUserTableAdapters.spr_User_SelectTableAdapter
            Dim dtblUser As BusinessObject.dstUser.spr_User_SelectDataTable = Nothing
            dtblUser = tadpUser.GetData(intEditUserID)

            If dtblUser.Rows.Count = 0 Then
                Response.Redirect("UserManagement.aspx")
                Return
            End If


            Dim drwUser As BusinessObject.dstUser.spr_User_SelectRow = dtblUser.Rows(0)

            With drwUser
                If drwUserLogin.Username = .Username And drwUserLogin.IsDataAdmin = False Then

                    Bootstrap_Panel1.CanSave = False

                End If

                txtUsername.Text = .Username
                txtAddress.Text = .Address
                txtEmail.Text = .Email
                txtFirstName.Text = .FName
                txtLastName.Text = .LName
                txtMobile.Text = .Mobile
                txtNationalID.Text = .NationalID
                txtNationalNo.Text = .NationalNo
                txtPersonCode.Text = .PersonCode
                txtTel.Text = .Tel
                chkStatus.Checked = .IsActive
                rdoIsPartTimeNo.Checked = Not .IsPartTime
                rdoIsPartTimeYes.Checked = .IsPartTime

                If .IsUserPhotoNull = False Then
                    imgUserPhoto.ImageUrl = "data:image/png;base64," + Convert.ToBase64String(.UserPhoto)
                End If

                If .IsDataAdmin = True AndAlso .IsItemAdmin = True Then
                    cmbUserType.SelectedIndex = 3
                ElseIf .IsItemAdmin = True Then
                    cmbUserType.SelectedIndex = 2
                ElseIf .IsDataUserAdmin = True Then
                    cmbUserType.SelectedIndex = 1
                Else
                    cmbUserType.SelectedIndex = 0
                End If

                rdoSexFemale.Checked = .Sex
                rdoSexMale.Checked = Not (.Sex)


                Dim tadpUserProvince As New BusinessObject.dstBranchTableAdapters.spr_Province_Check_SelectTableAdapter
                Dim dtblUserProvince As BusinessObject.dstBranch.spr_Province_Check_SelectDataTable = Nothing

                dtblUserProvince = tadpUserProvince.GetData(drwUser.FK_BrnachID)

                cmbProvince.DataBind()
                cmbProvince.SelectedValue = dtblUserProvince.First.ID

                odsBranch.SelectParameters.Item("Action").DefaultValue = 2
                odsBranch.SelectParameters.Item("ProvinceID").DefaultValue = cmbProvince.SelectedValue
                odsBranch.DataBind()

                '' cmbBranch.Enabled = True
                cmbBranch.DataSourceID = "odsBranch"
                cmbBranch.DataTextField = "BrnachCode"
                cmbBranch.DataValueField = "ID"


                cmbBranch.DataBind()

                cmbBranch.SelectedValue = drwUser.FK_BrnachID

                chkbxHadi.Checked = .IsHadiSystem
                chkbxAtieh.Checked = .ISAtiehUser

            End With

            ''Dim tadpAccessgroupUserList As New BusinessObject.dstAccessgroupUserTableAdapters.spr_AccessgroupUser_List_SelectTableAdapter
            ''Dim dtblAccessgroupUserList As BusinessObject.dstAccessgroupUser.spr_AccessgroupUser_List_SelectDataTable = Nothing
            ''dtblAccessgroupUserList = tadpAccessgroupUserList.GetData(drwUser.ID)

            ''For Each drwAccessgroupUserList As BusinessObject.dstAccessgroupUser.spr_AccessgroupUser_List_SelectRow In dtblAccessgroupUserList.Rows
            ''    For i As Integer = 0 To lstAccessGroups.Items.Count - 1
            ''        If lstAccessGroups.Items(i).Value = drwAccessgroupUserList.FK_AccessGroupID Then
            ''            lstAccessGroups.Items(i).Selected = True
            ''        End If
            ''    Next

            ''Next drwAccessgroupUserList



            Dim trNodeProvince As New TreeNode
            trAccessGroup.Nodes.Add(trNodeProvince)



            Dim blnFlag As Boolean = False
            Dim blnChecked As Boolean = False

            Dim tadpAccessgroupUserList As New BusinessObject.dstAccessgroupUserTableAdapters.spr_AccessgroupUser_List_SelectTableAdapter
            Dim dtblAccessgroupUserList As BusinessObject.dstAccessgroupUser.spr_AccessgroupUser_List_SelectDataTable = Nothing
            dtblAccessgroupUserList = tadpAccessgroupUserList.GetData(drwUser.ID)


            For Each drwAccessGroupList As BusinessObject.dstAccessgroup.spr_Accessgroup_List_SelectRow In dtblAccessGroupList.Rows
                Dim trNodeBranch As New TreeNode

                trNodeBranch.Text = "&nbsp;&nbsp;" & drwAccessGroupList.Desp
                trNodeBranch.Value = drwAccessGroupList.ID
                trNodeBranch.ShowCheckBox = True
                trNodeBranch.SelectAction = TreeNodeSelectAction.None

                For Each drwAccessgroupUserList As BusinessObject.dstAccessgroupUser.spr_AccessgroupUser_List_SelectRow In dtblAccessgroupUserList


                    If drwAccessgroupUserList.FK_AccessGroupID = drwAccessGroupList.ID Then
                        trNodeBranch.Checked = True
                        trNodeProvince.Expanded = True

                        blnFlag = True
                    End If


                Next drwAccessgroupUserList
                trNodeProvince.ChildNodes.Add(trNodeBranch)

                If blnFlag = False Then
                trNodeProvince.CollapseAll()
            End If

            If blnChecked = True Then
                trNodeProvince.Checked = True
            End If


            Next drwAccessGroupList

        End If


    End Sub



    Private Sub Bootstrap_Panel1_Panel_Cancel_Click(sender As Object, e As System.EventArgs) Handles Bootstrap_Panel1.Panel_Cancel_Click
        Response.Redirect("UserManagement.aspx")
        Return
    End Sub


    Private Sub Bootstrap_Panel1_Panel_Save_Click(sender As Object, e As System.EventArgs) Handles Bootstrap_Panel1.Panel_Save_Click

        Dim intEditUserID As Integer = CInt(Session("intEditUserID"))


        Dim dtblUserLogin As BusinessObject.dstUser.spr_User_Login_SelectDataTable = CType(Session("dtblUserLogin"), BusinessObject.dstUser.spr_User_Login_SelectDataTable)
        Dim drwUserLogin As BusinessObject.dstUser.spr_User_Login_SelectRow = dtblUserLogin.Rows(0)

        Dim strUserName As String = txtUsername.Text.Trim
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
        Dim blnIsPartTime As Boolean = rdoIsPartTimeYes.Checked

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

        Dim blnISActive As Boolean = chkStatus.Checked
        Dim blnUserPhotoChanged As Boolean = False

        Dim arrUserPhoto() As Byte = Nothing

        If fleUserPhoto.PostedFile IsNot Nothing AndAlso fleUserPhoto.PostedFile.ContentLength <> 0 Then

            If fleUserPhoto.PostedFile.ContentType.ToLower.IndexOf("png") = -1 Then
                Bootstrap_Panel1.ShowMessage("فرمت تصویر باید png باشد", True)
                Return
            End If


            Dim strmUserPhoto As IO.Stream = fleUserPhoto.PostedFile.InputStream
            ReDim arrUserPhoto(strmUserPhoto.Length)
            strmUserPhoto.Read(arrUserPhoto, 0, arrUserPhoto.Length)

            blnUserPhotoChanged = True
        End If

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
            If blnItemAdmin = True Then
                qryUser.spr_User_Update(strUserName, intEditUserID, blnISActive, blnDataAdmin, False, True, strFname, strLname, strEmail, blnSex, strTel, strMobile, Date.Now, drwUserLogin.ID, strPersonCode, strNationalID, strNationalNo, strAddress, intBranchID, blnIsPartTime, blnHadiUser, blnAtiehUser)

            Else
                qryUser.spr_User_Update(strUserName, intEditUserID, blnISActive, blnDataAdmin, False, False, strFname, strLname, strEmail, blnSex, strTel, strMobile, Date.Now, drwUserLogin.ID, strPersonCode, strNationalID, strNationalNo, strAddress, intBranchID, blnIsPartTime, blnHadiUser, blnAtiehUser)

            End If


            If blnUserPhotoChanged = True Then
                qryUser.spr_User_Photo_Update(intEditUserID, arrUserPhoto)
            End If


            'Dim arrSelectedGroups() As Integer = lstAccessGroups.GetSelectedIndices()
            Dim qryAccessgroupUser As New BusinessObject.dstAccessgroupUserTableAdapters.QueriesTableAdapter
            qryAccessgroupUser.spr_AccessgroupUser_User_Delete(intEditUserID)
            For Each trNode As TreeNode In trAccessGroup.Nodes
                If trNode.ChildNodes.Count = 0 Then
                    If trNode.Checked = True Then

                        qryAccessgroupUser.spr_AccessgroupUser_Insert(intEditUserID, trNode.Value)

                    End If

                Else

                    For Each trChildNode As TreeNode In trNode.ChildNodes

                        If trChildNode.ChildNodes.Count = 0 Then
                            If trChildNode.Checked = True Then
                                Dim ChildID As Integer = CInt(trChildNode.Value)

                                qryAccessgroupUser.spr_AccessgroupUser_Insert(intEditUserID, ChildID)

                            End If

                        End If


                    Next

                End If
            Next




        Catch ex As Exception
            Response.Redirect("UserManagement.aspx?Edit=NO")
            Return
        End Try

        Response.Redirect("UserManagement.aspx?Edit=OK")



    End Sub

    Protected Sub cmbBranch_DataBound(sender As Object, e As EventArgs) Handles cmbBranch.DataBound

        Dim li As New ListItem
        li.Text = "---"
        li.Value = -1
        cmbBranch.Items.Insert(0, li)

    End Sub

    Protected Sub cmbProvince_DataBound(sender As Object, e As EventArgs) Handles cmbProvince.DataBound
        Dim li As New ListItem
        li.Text = "---"
        li.Value = -1
        cmbProvince.Items.Insert(0, li)
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
End Class