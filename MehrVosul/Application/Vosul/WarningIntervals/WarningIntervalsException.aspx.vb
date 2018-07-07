Public Class WarningIntervalsException
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

            If Session("intEditWarningIntervalsID") Is Nothing Then
                Response.Redirect("WarningIntervalsManagement.aspx")
                Return
            End If

            Dim intEditWarningIntervalsID As Integer = CInt(Session("intEditWarningIntervalsID"))
            ViewState("intEditWarningIntervalsID") = intEditWarningIntervalsID

            ''Dim cntxVar As New BusinessObject.dbMehrVosulEntities1
            ''Dim lnqWarningIntervalsException = cntxVar.tbl_WarningIntervalsException.Where(Function(x) x.FK_WarningIntervalID = intEditWarningIntervalsID)
            ''Dim intWarningIntervalsExceptionID As Integer
            ''If lnqWarningIntervalsException.Count > 0 Then

            ''    Dim lnqWarningIntervalsExceptionList = lnqWarningIntervalsException.ToList(0)

            ''    txtWarningIntervalsExeptionName.Text = lnqWarningIntervalsExceptionList.ExceptionName
            ''    chkStatus.Checked = lnqWarningIntervalsExceptionList.ISActive
            ''    intWarningIntervalsExceptionID = lnqWarningIntervalsExceptionList.ID
            ''    ViewState("WarningIntervalsExceptionID") = intWarningIntervalsExceptionID
            ''End If

            ''Dim lnqWarningIntervalsExeptionLoanType = cntxVar.tbl_WarningIntervalsExceptionLoanType.Where(Function(x) x.FK_WarningIntervalsExceptionID = intWarningIntervalsExceptionID)


            ''Dim tadpLoanTypeList As New BusinessObject.dstLoanTypeTableAdapters.spr_LoanType_SelectTableAdapter
            ''Dim dtblLoanTypeList As BusinessObject.dstLoanType.spr_LoanType_SelectDataTable = Nothing

            ''Dim tadpIntervalsLoanType As New BusinessObject.dstWarningIntervalsLoanTypeTableAdapters.spr_WarningIntervalsLoanType_SelectTableAdapter

            ''Dim dtblMenuLeafList As BusinessObject.dstWarningIntervalsLoanType.spr_WarningIntervalsLoanType_SelectDataTable = Nothing
            ''dtblMenuLeafList = tadpIntervalsLoanType.GetData(3, -1, intEditWarningIntervalsID)
            ''Dim strchklstLoanTypeLeaves As String = ""

            ''Dim blnHasChecked As Boolean = False
            ''Dim blnHasBranch As Boolean = False
            ''For Each drwMeneLeafList As BusinessObject.dstWarningIntervalsLoanType.spr_WarningIntervalsLoanType_SelectRow In dtblMenuLeafList.Rows
            ''    dtblLoanTypeList = tadpLoanTypeList.GetData(drwMeneLeafList.FK_LoanTypeID)
            ''    If lnqWarningIntervalsExeptionLoanType.Count > 0 Then

            ''        Dim lnqWarningIntervalsExeptionLoanTypeList = lnqWarningIntervalsExeptionLoanType.ToList()
            ''        For Each lnqWarningIntervalsExeptionLoanTypeItem In lnqWarningIntervalsExeptionLoanTypeList
            ''            If drwMeneLeafList.ID = lnqWarningIntervalsExeptionLoanTypeItem.FK_WariningIntervalLoanTypeID Then

            ''                strchklstLoanTypeLeaves &= "<div class='checkbox'> <label> <input type='checkbox' checked='checked' value='" & drwMeneLeafList.ID & "' name='chklstMenu" & drwMeneLeafList.ID & "'><i class='fa " & drwMeneLeafList.ID & " fa-1x'></i> " & dtblLoanTypeList.First.LoanTypeName & "</label></div>"
            ''                blnHasChecked = True
            ''                Exit For
            ''            End If

            ''        Next

            ''    End If
            ''    If blnHasChecked = True Then
            ''        blnHasChecked = False
            ''        Continue For
            ''    Else
            ''        strchklstLoanTypeLeaves &= "<div class='checkbox'> <label> <input type='checkbox' value='" & drwMeneLeafList.ID & "' name='chklstMenu" & drwMeneLeafList.ID & "'><i class='fa " & drwMeneLeafList.ID & " fa-1x'></i> " & dtblLoanTypeList.First.LoanTypeName & "</label></div>"
            ''    End If



            ''Next drwMeneLeafList

            ''divchklstLoanTypeItems.InnerHtml = strchklstLoanTypeLeaves

            ''''Fill Branch Tree
            ''Dim tadpProvince As New BusinessObject.dstBranchTableAdapters.spr_ProvinceList_SelectTableAdapter
            ''Dim dtblProvince As BusinessObject.dstBranch.spr_ProvinceList_SelectDataTable = Nothing
            ''dtblProvince = tadpProvince.GetData()
            ''Dim tadpBranchProvince As New BusinessObject.dstWarningIntervalsBranchTableAdapters.spr_WarningIntervalsBranchProvince_SelectByIntervalIDTableAdapter
            ''Dim dtblBranchProvince As BusinessObject.dstWarningIntervalsBranch.spr_WarningIntervalsBranchProvince_SelectByIntervalIDDataTable = Nothing
            ''dtblBranchProvince = tadpBranchProvince.GetData(intEditWarningIntervalsID)
            ''Dim tadpIntervalBranch As New BusinessObject.dstWarningIntervalsBranchTableAdapters.spr_WarningIntervalsBranch_SelectByIntervalIDTableAdapter
            ''Dim dtblIntervalBranch As BusinessObject.dstWarningIntervalsBranch.spr_WarningIntervalsBranch_SelectByIntervalIDDataTable = Nothing

            ''Dim lnqWarningIntervalsExeptionBranch = cntxVar.tbl_WarningIntervalsExceptionBranch.Where(Function(x) x.FK_WarningIntervalsExceptionID = intWarningIntervalsExceptionID)

            ''For Each drwProvince As BusinessObject.dstBranch.spr_ProvinceList_SelectRow In dtblProvince

            ''    Dim trNodeProvince As New TreeNode

            ''    For Each drwBranchProvince As BusinessObject.dstWarningIntervalsBranch.spr_WarningIntervalsBranchProvince_SelectByIntervalIDRow In dtblBranchProvince

            ''        If drwProvince.ID = drwBranchProvince.Fk_ProvinceID Then

            ''            dtblIntervalBranch = tadpIntervalBranch.GetData(intEditWarningIntervalsID, drwBranchProvince.Fk_ProvinceID)

            ''            trNodeProvince.Value = drwProvince.ID
            ''            trNodeProvince.Text = "&nbsp;&nbsp;+" & drwProvince.Province
            ''            trNodeProvince.SelectAction = TreeNodeSelectAction.Expand
            ''            trState.Nodes.Add(trNodeProvince)
            ''            trNodeProvince.ShowCheckBox = True


            ''            For Each drwBranch As BusinessObject.dstWarningIntervalsBranch.spr_WarningIntervalsBranch_SelectByIntervalIDRow In dtblIntervalBranch.Rows
            ''                Dim trNodeBranch As New TreeNode

            ''                trNodeBranch.Text = "&nbsp;&nbsp;" & drwBranch.BrnachCode
            ''                trNodeBranch.Value = drwBranch.ID
            ''                trNodeBranch.ShowCheckBox = True
            ''                trNodeBranch.SelectAction = TreeNodeSelectAction.None


            ''                If lnqWarningIntervalsExeptionBranch.Count > 0 Then
            ''                    Dim lnqWarningIntervalsExeptionBranchList = lnqWarningIntervalsExeptionBranch.ToList()

            ''                    For Each lnqWarningIntervalsExeptionBranchListItem In lnqWarningIntervalsExeptionBranchList

            ''                        If lnqWarningIntervalsExeptionBranchListItem.FK_WarningIntervalBranchID = drwBranch.ID Then

            ''                            trNodeBranch.Checked = True
            ''                            trNodeProvince.Expanded = True
            ''                            blnHasBranch = True
            ''                            Exit For
            ''                        End If

            ''                    Next



            ''                End If


            ''                trNodeProvince.ChildNodes.Add(trNodeBranch)


            ''            Next drwBranch


            ''        End If

            ''    Next
            ''Next

            '''''trState.ShowCheckBoxes = TreeNodeTypes.All
            ''''trState.CollapseAll()
            ''''treeViewDiv.Style.Add("display", "block")




        Else

            ''Dim tadpLoanTypeList As New BusinessObject.dstLoanTypeTableAdapters.spr_LoanType_SelectTableAdapter
            ''Dim dtblLoanTypeList As BusinessObject.dstLoanType.spr_LoanType_SelectDataTable = Nothing
            ''dtblLoanTypeList = tadpLoanTypeList.GetData()

            'Dim tadpIntervalsLoanType As New BusinessObject.dstWarningIntervalsLoanTypeTableAdapters.spr_WarningIntervalsLoanType_SelectTableAdapter

            'Dim dtblMenuLeafList As BusinessObject.dstWarningIntervalsLoanType.spr_WarningIntervalsLoanType_SelectDataTable = Nothing
            'dtblMenuLeafList = tadpIntervalsLoanType.GetData(3, -1, CInt(ViewState("intEditWarningIntervalsID")))

            'Dim strchklstLoanLeaves As String = ""

            'For Each drwMeneLeafList As BusinessObject.dstWarningIntervalsLoanType.spr_WarningIntervalsLoanType_SelectRow In dtblMenuLeafList.Rows


            '    Dim boolChecked As Boolean = False

            '    For i As Integer = 0 To Request.Form.Keys.Count - 1

            '        If Request.Form.Keys(i).StartsWith("chklstMenu") = True Then

            '            If CInt(Request.Form(i)) = drwMeneLeafList.ID Then
            '                boolChecked = True
            '                Exit For
            '            End If

            '        End If

            '    Next


            '    If boolChecked = True Then
            '        strchklstLoanLeaves &= "<div class='checkbox'> <label> <input type='checkbox' checked='checked' value='" & drwMeneLeafList.ID & "' name='chklstMenu" & drwMeneLeafList.ID & "'><i class='fa " & drwMeneLeafList.ID & " fa-1x'></i> " & drwMeneLeafList.LoanType & "</label></div>"
            '    Else
            '        strchklstLoanLeaves &= "<div class='checkbox'> <label> <input type='checkbox' value='" & drwMeneLeafList.ID & "' name='chklstMenu" & drwMeneLeafList.ID & "'><i class='fa " & drwMeneLeafList.ID & " fa-1x'></i> " & drwMeneLeafList.LoanType & "</label></div>"
            '    End If


            'Next drwMeneLeafList

            'divchklstLoanTypeItems.InnerHtml = strchklstLoanLeaves


        End If


    End Sub



    Private Sub Bootstrap_Panel1_Panel_Cancel_Click(sender As Object, e As System.EventArgs) Handles Bootstrap_Panel1.Panel_Cancel_Click
        Response.Redirect("WarningIntervalsManagement.aspx")
        Return
    End Sub


    Private Sub Bootstrap_Panel1_Panel_Save_Click(sender As Object, e As System.EventArgs) Handles Bootstrap_Panel1.Panel_Save_Click

        Dim dtblUserLogin As BusinessObject.dstUser.spr_User_Login_SelectDataTable = CType(Session("dtblUserLogin"), BusinessObject.dstUser.spr_User_Login_SelectDataTable)
        Dim drwUserLogin As BusinessObject.dstUser.spr_User_Login_SelectRow = dtblUserLogin.Rows(0)


        Dim intWarningIntervalsExceptionID As Integer = -1
        If Not ViewState("WarningIntervalsExceptionID") Is Nothing Then
            intWarningIntervalsExceptionID = CInt(ViewState("WarningIntervalsExceptionID"))
        End If

        Dim strWarniningTitle As String = txtWarningIntervalsExeptionName.Text.Trim
        Dim blnStatus As Boolean = chkStatus.Checked

        Try

            Dim tadpBrnachList As New BusinessObject.dstBranchTableAdapters.spr_Branch_List_SelectTableAdapter
            Dim dtblBranchList As BusinessObject.dstBranch.spr_Branch_List_SelectDataTable = Nothing

            Dim cntxVar As New BusinessObject.dbMehrVosulEntities1

            Dim oWrningIntervalsExeption As New BusinessObject.tbl_WarningIntervalsException
            oWrningIntervalsExeption.ID = intWarningIntervalsExceptionID
            oWrningIntervalsExeption.ExceptionName = strWarniningTitle
            oWrningIntervalsExeption.ISActive = blnStatus
            oWrningIntervalsExeption.FK_WarningIntervalID = CInt(ViewState("intEditWarningIntervalsID"))

            ''Insert Interval
            If intWarningIntervalsExceptionID = -1 Then
                cntxVar.tbl_WarningIntervalsException.Add(oWrningIntervalsExeption)
                cntxVar.SaveChanges()
                intWarningIntervalsExceptionID = oWrningIntervalsExeption.ID
            Else
                Dim WarningIntervalsException = (From rows In cntxVar.tbl_WarningIntervalsException Where rows.ID = intWarningIntervalsExceptionID).FirstOrDefault()
                WarningIntervalsException.ExceptionName = strWarniningTitle
                WarningIntervalsException.ISActive = blnStatus
                cntxVar.SaveChanges()
            End If



            ''    Dim intWarningIntervalID As Integer = CInt(ViewState("intEditWarningIntervalsID"))
            If intWarningIntervalsExceptionID <> -1 Then
                ''Delete WrningIntervalsExeptionLoanType
                Dim WarningIntervalsExceptionLoanType = cntxVar.tbl_WarningIntervalsExceptionLoanType.Where(Function(x) x.FK_WarningIntervalsExceptionID = intWarningIntervalsExceptionID)
                '(From rows In cntxVar.tbl_WarningIntervalsExceptionLoanType Where rows.FK_WarningIntervalsExceptionID = intWarningIntervalsExceptionID).FirstOrDefault()

                If WarningIntervalsExceptionLoanType IsNot Nothing Then

                    cntxVar.tbl_WarningIntervalsExceptionLoanType.RemoveRange(WarningIntervalsExceptionLoanType)
                    cntxVar.SaveChanges()
                End If

                ''Delete WrningIntervalsExeptionBranch

                Dim WarningIntervalsExceptionBranch = cntxVar.tbl_WarningIntervalsExceptionBranch.Where(Function(x) x.FK_WarningIntervalsExceptionID = intWarningIntervalsExceptionID)

                If WarningIntervalsExceptionBranch IsNot Nothing Then

                    cntxVar.tbl_WarningIntervalsExceptionBranch.RemoveRange(WarningIntervalsExceptionBranch)
                    cntxVar.SaveChanges()
                End If

            End If

            For i As Integer = 0 To Request.Form.Keys.Count - 1

                If Request.Form.Keys(i).StartsWith("chklstMenu") = True Then

                    Dim oWrningIntervalsExeptionLoanType As New BusinessObject.tbl_WarningIntervalsExceptionLoanType

                    Dim intLoanTypeID As Integer = CInt(Request.Form(i))

                    oWrningIntervalsExeptionLoanType.FK_WariningIntervalLoanTypeID = intLoanTypeID
                    oWrningIntervalsExeptionLoanType.FK_WarningIntervalsExceptionID = intWarningIntervalsExceptionID

                    cntxVar.tbl_WarningIntervalsExceptionLoanType.Add(oWrningIntervalsExeptionLoanType)
                    cntxVar.SaveChanges()

                End If

            Next i


            For Each trNode As TreeNode In trState.Nodes


                For Each trChildNode As TreeNode In trNode.ChildNodes

                    If trChildNode.ChildNodes.Count = 0 Then
                        If trChildNode.Checked = True Then
                            Dim ChildID As Integer = CInt(trChildNode.Value)

                            Dim oWrningIntervalsExeptionBranch As New BusinessObject.tbl_WarningIntervalsExceptionBranch

                            oWrningIntervalsExeptionBranch.FK_WarningIntervalBranchID = ChildID
                            oWrningIntervalsExeptionBranch.FK_WarningIntervalsExceptionID = intWarningIntervalsExceptionID

                            cntxVar.tbl_WarningIntervalsExceptionBranch.Add(oWrningIntervalsExeptionBranch)
                            cntxVar.SaveChanges()


                        End If

                    End If



                Next


            Next


        Catch ex As Exception
            Response.Redirect("WarningIntervalsManagement.aspx?Save=NO&Exeption=Yes")
            'Bootstrap_Panel1.ShowMessage(ex.Message, True)
            Return
        End Try

        Response.Redirect("WarningIntervalsManagement.aspx?Save=OK&Exeption=Yes")



    End Sub


End Class