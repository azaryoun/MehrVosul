Public Class HadiWarningIntervalsException
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

            ''    Dim dtblUserLogin As BusinessObject.dstUser.spr_User_Login_SelectDataTable = CType(Session("dtblUserLogin"), BusinessObject.dstUser.spr_User_Login_SelectDataTable)
            ''    Dim drwUserLogin As BusinessObject.dstUser.spr_User_Login_SelectRow = dtblUserLogin.Rows(0)

            ''    If Session("intEditHadiWarningIntervalsID") Is Nothing Then
            ''        Response.Redirect("HadiwarningIntervalsManagement.aspx")
            ''        Return
            ''    End If

            ''    Dim intEditHadiWarningIntervalsID As Integer = CInt(Session("intEditHadiWarningIntervalsID"))
            ''    ViewState("intEditHadiWarningIntervalsID") = intEditHadiWarningIntervalsID

            ''    Dim cntxVar As New BusinessObject.dbMehrVosulEntities1
            ''    Dim lnqHadiWarningIntervalsException = cntxVar.tbl_HadiWarningIntervalsException.Where(Function(x) x.FK_HadiWarningIntervalID = intEditHadiWarningIntervalsID)
            ''    Dim intHadiWarningIntervalsExceptionID As Integer
            ''    If lnqHadiWarningIntervalsException.Count > 0 Then

            ''        Dim lnqHadiWarningIntervalsExceptionList = lnqHadiWarningIntervalsException.ToList(0)

            ''        txtWarningIntervalsExeptionName.Text = lnqHadiWarningIntervalsExceptionList.ExceptionName
            ''        chkStatus.Checked = lnqHadiWarningIntervalsExceptionList.ISActive
            ''        intHadiWarningIntervalsExceptionID = lnqHadiWarningIntervalsExceptionList.ID
            ''        ViewState("HadiWarningIntervalsExceptionID") = intHadiWarningIntervalsExceptionID
            ''    End If

            ''    Dim lnqWarningIntervalsExeptionDeposit = cntxVar.tbl_HadiWarningIntervalsExceptionDeposit.Where(Function(x) x.FK_HadiWarningIntervalsExceptionID = intHadiWarningIntervalsExceptionID)


            ''    Dim tadpDepositTypeList As New BusinessObject.dstDepositTableAdapters.spr_DepositType_SelectTableAdapter
            ''    Dim dtblDepositTypeList As BusinessObject.dstDeposit.spr_DepositType_SelectDataTable = Nothing

            ''    Dim tadpIntervalsDeposit As New BusinessObject.dstHadiWarningIntervalsDepositTableAdapters.spr_HadiWarningIntervalsDeposit_SelectTableAdapter

            ''    Dim dtblMenuLeafList As BusinessObject.dstHadiWarningIntervalsDeposit.spr_HadiWarningIntervalsDeposit_SelectDataTable = Nothing
            ''    dtblMenuLeafList = tadpIntervalsDeposit.GetData(3, -1, intEditHadiWarningIntervalsID)
            ''    Dim strchklstLoanTypeLeaves As String = ""

            ''    Dim blnHasChecked As Boolean = False
            ''    Dim blnHasBranch As Boolean = False
            ''    For Each drwMeneLeafList As BusinessObject.dstHadiWarningIntervalsDeposit.spr_HadiWarningIntervalsDeposit_SelectRow In dtblMenuLeafList.Rows
            ''        dtblDepositTypeList = tadpDepositTypeList.GetData(1, drwMeneLeafList.FK_DepositTypeID)
            ''        If lnqWarningIntervalsExeptionDeposit.Count > 0 Then

            ''            Dim lnqWarningIntervalsExeptionLoanTypeList = lnqWarningIntervalsExeptionDeposit.ToList()
            ''            For Each lnqWarningIntervalsExeptionLoanTypeItem In lnqWarningIntervalsExeptionLoanTypeList
            ''                If drwMeneLeafList.ID = lnqWarningIntervalsExeptionLoanTypeItem.FK_HadiWariningIntervalDepositID Then

            ''                    strchklstLoanTypeLeaves &= "<div class='checkbox'> <label> <input type='checkbox' checked='checked' value='" & drwMeneLeafList.ID & "' name='chklstMenu" & drwMeneLeafList.ID & "'><i class='fa " & drwMeneLeafList.ID & " fa-1x'></i> " & dtblDepositTypeList.First.DepositName & "</label></div>"
            ''                    blnHasChecked = True
            ''                    Exit For
            ''                End If

            ''            Next

            ''        End If
            ''        If blnHasChecked = True Then
            ''            blnHasChecked = False
            ''            Continue For
            ''        Else
            ''            strchklstLoanTypeLeaves &= "<div class='checkbox'> <label> <input type='checkbox' value='" & drwMeneLeafList.ID & "' name='chklstMenu" & drwMeneLeafList.ID & "'><i class='fa " & drwMeneLeafList.ID & " fa-1x'></i> " & dtblDepositTypeList.First.DepositName & "</label></div>"
            ''        End If



            ''    Next drwMeneLeafList

            ''    divchklstLoanTypeItems.InnerHtml = strchklstLoanTypeLeaves

            ''    ''Fill Branch Tree
            ''    Dim tadpProvince As New BusinessObject.dstBranchTableAdapters.spr_ProvinceList_SelectTableAdapter
            ''    Dim dtblProvince As BusinessObject.dstBranch.spr_ProvinceList_SelectDataTable = Nothing
            ''    dtblProvince = tadpProvince.GetData()
            ''    Dim tadpBranchProvince As New BusinessObject.dstHadiWarningIntervalsBranchTableAdapters.spr_HadiWarningIntervalsBranchProvince_SelectByIntervalIDTableAdapter
            ''    Dim dtblBranchProvince As BusinessObject.dstHadiWarningIntervalsBranch.spr_HadiWarningIntervalsBranchProvince_SelectByIntervalIDDataTable = Nothing
            ''    dtblBranchProvince = tadpBranchProvince.GetData(intEditHadiWarningIntervalsID)
            ''    Dim tadpIntervalBranch As New BusinessObject.dstHadiWarningIntervalsBranchTableAdapters.spr_HadiWarningIntervalsBranch_SelectByIntervalIDTableAdapter
            ''    Dim dtblIntervalBranch As BusinessObject.dstHadiWarningIntervalsBranch.spr_HadiWarningIntervalsBranch_SelectByIntervalIDDataTable = Nothing

            ''    Dim lnqWarningIntervalsExeptionBranch = cntxVar.tbl_HadiWarningIntervalsExceptionBranch.Where(Function(x) x.FK_HadiWarningIntervalsExceptionID = intHadiWarningIntervalsExceptionID)

            ''    For Each drwProvince As BusinessObject.dstBranch.spr_ProvinceList_SelectRow In dtblProvince

            ''        Dim trNodeProvince As New TreeNode

            ''        For Each drwBranchProvince As BusinessObject.dstHadiWarningIntervalsBranch.spr_HadiWarningIntervalsBranchProvince_SelectByIntervalIDRow In dtblBranchProvince

            ''            If drwProvince.ID = drwBranchProvince.Fk_ProvinceID Then

            ''                dtblIntervalBranch = tadpIntervalBranch.GetData(intEditHadiWarningIntervalsID, drwBranchProvince.Fk_ProvinceID)

            ''                trNodeProvince.Value = drwProvince.ID
            ''                trNodeProvince.Text = "&nbsp;&nbsp;+" & drwProvince.Province
            ''                trNodeProvince.SelectAction = TreeNodeSelectAction.Expand
            ''                trState.Nodes.Add(trNodeProvince)
            ''                trNodeProvince.ShowCheckBox = True


            ''                For Each drwBranch As BusinessObject.dstHadiWarningIntervalsBranch.spr_HadiWarningIntervalsBranch_SelectByIntervalIDRow In dtblIntervalBranch.Rows
            ''                    Dim trNodeBranch As New TreeNode

            ''                    trNodeBranch.Text = "&nbsp;&nbsp;" & drwBranch.BrnachCode
            ''                    trNodeBranch.Value = drwBranch.ID
            ''                    trNodeBranch.ShowCheckBox = True
            ''                    trNodeBranch.SelectAction = TreeNodeSelectAction.None


            ''                    If lnqWarningIntervalsExeptionBranch.Count > 0 Then
            ''                        Dim lnqWarningIntervalsExeptionBranchList = lnqWarningIntervalsExeptionBranch.ToList()

            ''                        For Each lnqWarningIntervalsExeptionBranchListItem In lnqWarningIntervalsExeptionBranchList

            ''                            If lnqWarningIntervalsExeptionBranchListItem.FK_HadiWarningIntervalBranchID = drwBranch.ID Then

            ''                                trNodeBranch.Checked = True
            ''                                trNodeProvince.Expanded = True
            ''                                blnHasBranch = True
            ''                                Exit For
            ''                            End If

            ''                        Next



            ''                    End If


            ''                    trNodeProvince.ChildNodes.Add(trNodeBranch)


            ''                Next drwBranch


            ''            End If

            ''        Next
            ''    Next




            ''Else


        End If


    End Sub



    Private Sub Bootstrap_Panel1_Panel_Cancel_Click(sender As Object, e As System.EventArgs) Handles Bootstrap_Panel1.Panel_Cancel_Click
        Response.Redirect("HadiwarningIntervalsManagement.aspx")
        Return
    End Sub


    Private Sub Bootstrap_Panel1_Panel_Save_Click(sender As Object, e As System.EventArgs) Handles Bootstrap_Panel1.Panel_Save_Click

        Dim dtblUserLogin As BusinessObject.dstUser.spr_User_Login_SelectDataTable = CType(Session("dtblUserLogin"), BusinessObject.dstUser.spr_User_Login_SelectDataTable)
        Dim drwUserLogin As BusinessObject.dstUser.spr_User_Login_SelectRow = dtblUserLogin.Rows(0)


        Dim intHadiWarningIntervalsExceptionID As Integer = -1
        If Not ViewState("HadiWarningIntervalsExceptionID") Is Nothing Then
            intHadiWarningIntervalsExceptionID = CInt(ViewState("HadiWarningIntervalsExceptionID"))
        End If

        Dim strWarniningTitle As String = txtWarningIntervalsExeptionName.Text.Trim
        Dim blnStatus As Boolean = chkStatus.Checked

        Try

            Dim tadpBrnachList As New BusinessObject.dstBranchTableAdapters.spr_Branch_List_SelectTableAdapter
            Dim dtblBranchList As BusinessObject.dstBranch.spr_Branch_List_SelectDataTable = Nothing

            Dim cntxVar As New BusinessObject.dbMehrVosulEntities1

            Dim oWrningIntervalsExeption As New BusinessObject.tbl_HadiWarningIntervalsException
            oWrningIntervalsExeption.ID = intHadiWarningIntervalsExceptionID
            oWrningIntervalsExeption.ExceptionName = strWarniningTitle
            oWrningIntervalsExeption.ISActive = blnStatus
            oWrningIntervalsExeption.FK_HadiWarningIntervalID = intHadiWarningIntervalsExceptionID

            ''Insert Interval
            If intHadiWarningIntervalsExceptionID = -1 Then
                cntxVar.tbl_HadiWarningIntervalsException.Add(oWrningIntervalsExeption)
                cntxVar.SaveChanges()
                intHadiWarningIntervalsExceptionID = oWrningIntervalsExeption.ID
            Else
                Dim HadiWarningIntervalsException = (From rows In cntxVar.tbl_HadiWarningIntervalsException Where rows.ID = intHadiWarningIntervalsExceptionID).FirstOrDefault()
                HadiWarningIntervalsException.ExceptionName = strWarniningTitle
                HadiWarningIntervalsException.ISActive = blnStatus
                cntxVar.SaveChanges()
            End If



            ''    Dim intWarningIntervalID As Integer = CInt(ViewState("intEditWarningIntervalsID"))
            If intHadiWarningIntervalsExceptionID <> -1 Then
                ''Delete WrningIntervalsExeptionLoanType
                Dim HadiWarningIntervalsExceptionDeposit = cntxVar.tbl_HadiWarningIntervalsExceptionDeposit.Where(Function(x) x.FK_HadiWarningIntervalsExceptionID = intHadiWarningIntervalsExceptionID)
                '(From rows In cntxVar.tbl_HadiWarningIntervalsExceptionLoanType Where rows.FK_HadiWarningIntervalsExceptionID = intHadiWarningIntervalsExceptionID).FirstOrDefault()

                If HadiWarningIntervalsExceptionDeposit IsNot Nothing Then

                    cntxVar.tbl_HadiWarningIntervalsExceptionDeposit.RemoveRange(HadiWarningIntervalsExceptionDeposit)
                    cntxVar.SaveChanges()
                End If

                ''Delete WrningIntervalsExeptionBranch

                Dim HadiWarningIntervalsExceptionBranch = cntxVar.tbl_HadiWarningIntervalsExceptionBranch.Where(Function(x) x.FK_HadiWarningIntervalsExceptionID = intHadiWarningIntervalsExceptionID)

                If HadiWarningIntervalsExceptionBranch IsNot Nothing Then

                    cntxVar.tbl_HadiWarningIntervalsExceptionBranch.RemoveRange(HadiWarningIntervalsExceptionBranch)
                    cntxVar.SaveChanges()
                End If

            End If

            For i As Integer = 0 To Request.Form.Keys.Count - 1

                If Request.Form.Keys(i).StartsWith("chklstMenu") = True Then

                    Dim oWrningIntervalsExeptionLoanType As New BusinessObject.tbl_HadiWarningIntervalsExceptionDeposit

                    Dim intLoanTypeID As Integer = CInt(Request.Form(i))

                    oWrningIntervalsExeptionLoanType.FK_HadiWariningIntervalDepositID = intLoanTypeID
                    oWrningIntervalsExeptionLoanType.FK_HadiWarningIntervalsExceptionID = intHadiWarningIntervalsExceptionID

                    cntxVar.tbl_HadiWarningIntervalsExceptionDeposit.Add(oWrningIntervalsExeptionLoanType)
                    cntxVar.SaveChanges()

                End If

            Next i


            For Each trNode As TreeNode In trState.Nodes


                For Each trChildNode As TreeNode In trNode.ChildNodes

                    If trChildNode.ChildNodes.Count = 0 Then
                        If trChildNode.Checked = True Then
                            Dim ChildID As Integer = CInt(trChildNode.Value)

                            Dim oWrningIntervalsExeptionBranch As New BusinessObject.tbl_HadiWarningIntervalsExceptionBranch

                            oWrningIntervalsExeptionBranch.FK_HadiWarningIntervalBranchID = ChildID
                            oWrningIntervalsExeptionBranch.FK_HadiWarningIntervalsExceptionID = intHadiWarningIntervalsExceptionID

                            cntxVar.tbl_HadiWarningIntervalsExceptionBranch.Add(oWrningIntervalsExeptionBranch)
                            cntxVar.SaveChanges()


                        End If

                    End If



                Next


            Next


        Catch ex As Exception
            Response.Redirect("HadiwarningIntervalsManagement.aspx?Save=NO&Exeption=Yes")

            Return
        End Try

        Response.Redirect("HadiwarningIntervalsManagement.aspx?Save=OK&Exeption=Yes")



    End Sub


End Class