Public Class HadiwarningIntervalsEdit
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

            Try

                If Session("intEditHadiWarningIntervalsID") Is Nothing Then
                    Response.Redirect("HadiwarningIntervalsManagement.aspx")
                    Return
                End If

                Dim intEditHadiwarningIntervalsID As Integer = CInt(Session("intEditHadiWarningIntervalsID"))

                StartTimePicker.Hour = 8


                Dim tadpHadiwarningIntervals As New BusinessObject.dstHadiWarningIntervalsTableAdapters.spr_HadiWarningIntervals_SelectTableAdapter
                Dim dtblHadiwarningIntervals As BusinessObject.dstHadiWarningIntervals.spr_HadiWarningIntervals_SelectDataTable = Nothing

                dtblHadiwarningIntervals = tadpHadiwarningIntervals.GetData(intEditHadiwarningIntervalsID)

                Dim drwHadiwarningIntervals As BusinessObject.dstHadiWarningIntervals.spr_HadiWarningIntervals_SelectRow = dtblHadiwarningIntervals.Rows(0)

                With drwHadiwarningIntervals

                    txtWarningIntervalsName.Text = drwHadiwarningIntervals.WarniningTitle
                    txtFrom.Text = drwHadiwarningIntervals.FromDay
                    txtTo.Text = drwHadiwarningIntervals.ToDay
                    cmbFrequencyInDay.SelectedValue = drwHadiwarningIntervals.FrequencyInDay
                    cmbFrequencyInHour.SelectedValue = drwHadiwarningIntervals.FrequencyPeriodHour
                    StartTimePicker.Hour = drwHadiwarningIntervals.StartTime.Hours
                    StartTimePicker.Minute = drwHadiwarningIntervals.StartTime.Minutes
                    chkbxCallTelephone.Checked = drwHadiwarningIntervals.CallTelephone
                    chkbxVoiceMessage.Checked = drwHadiwarningIntervals.VoiceMessage
                    chkbxSendSMS.Checked = drwHadiwarningIntervals.SendSMS
                    rdoNewDeposit.Checked = If(drwHadiwarningIntervals.ForDeposit = True, True, False)
                    rdoGetDeposit.Checked = If(drwHadiwarningIntervals.ForDeposit = True, False, True)
                    chkStatus.Checked = drwHadiwarningIntervals.IsActive
                    hdnWarningIntervalTypeNew.Value = If(drwHadiwarningIntervals.ForDeposit = True, "Deposit", "Loan")
                    rdboLoanApprovment.Checked = drwHadiwarningIntervals.LoanApprovment
                    rdboIssuingContract.Checked = drwHadiwarningIntervals.IssuingContract
                    rdboLaonPaid.Checked = drwHadiwarningIntervals.LaonPaid

                End With

                If drwHadiwarningIntervals.ForDeposit = True Then

                    divDeposit.Visible = True
                    divLoan.Visible = False

                    Dim tadpDepositList As New BusinessObject.dstDepositTableAdapters.spr_DepositType_List_SelectTableAdapter
                    Dim dtblDepositList As BusinessObject.dstDeposit.spr_DepositType_List_SelectDataTable = Nothing
                    dtblDepositList = tadpDepositList.GetData()

                    Dim tadpwarningDeposit As New BusinessObject.dstHadiWarningIntervalsDepositTableAdapters.spr_HadiWarningIntervalsDeposit_SelectTableAdapter
                    Dim dtbWarningDeposit As BusinessObject.dstHadiWarningIntervalsDeposit.spr_HadiWarningIntervalsDeposit_SelectDataTable = Nothing
                    dtbWarningDeposit = tadpwarningDeposit.GetData(3, -1, intEditHadiwarningIntervalsID)

                    Dim strchklstDepositLeaves As String = ""

                    For Each drwMeneLeafList As BusinessObject.dstDeposit.spr_DepositType_List_SelectRow In dtblDepositList.Rows

                        If dtbWarningDeposit.Select("FK_DepositTypeID=" & drwMeneLeafList.ID).Length > 0 Then
                            strchklstDepositLeaves &= "<div class='checkbox'> <label> <input type='checkbox' checked='checked' value='" & drwMeneLeafList.ID & "' name='chklstMenu" & drwMeneLeafList.ID & "'><i class='fa " & drwMeneLeafList.ID & " fa-1x'></i> " & drwMeneLeafList.Deposit & "</label></div>"
                        Else
                            strchklstDepositLeaves &= "<div class='checkbox'> <label> <input type='checkbox' value='" & drwMeneLeafList.ID & "' name='chklstMenu" & drwMeneLeafList.ID & "'><i class='fa " & drwMeneLeafList.ID & " fa-1x'></i> " & drwMeneLeafList.Deposit & "</label></div>"
                        End If


                    Next drwMeneLeafList

                    divchklstDepositItems.InnerHtml = strchklstDepositLeaves

                Else

                    divDeposit.Visible = False
                    divLoan.Visible = True

                    Dim tadpMLoanTypeList As New BusinessObject.dstLoanTypeTableAdapters.spr_LoanType_List_SelectTableAdapter
                    Dim dtblLoanMenuLeafList As BusinessObject.dstLoanType.spr_LoanType_List_SelectDataTable = Nothing
                    dtblLoanMenuLeafList = tadpMLoanTypeList.GetData()
                    Dim strchklstLoanTypeLeaves As String = ""

                    'Dim cntxVar As New BusinessObject.dbMehrVosulEntities1
                    '   Dim lnqHadiWarningIntervalsLaon = cntxVar.tbl_HadiWarningIntervalsLoan.Where(Function(x) x.FK_HadiWarningIntervalsID = intEditHadiwarningIntervalsID)

                    Dim tadpHadiWarngIntervalsLoan As New BusinessObject.dstHadiWarningIntervalsTableAdapters.spr_HadiWarningIntervalsLoan_SelectTableAdapter
                    Dim dtblHadiWarngIntervalsLoan As BusinessObject.dstHadiWarningIntervals.spr_HadiWarningIntervalsLoan_SelectDataTable = Nothing

                    dtblHadiWarngIntervalsLoan = tadpHadiWarngIntervalsLoan.GetData(intEditHadiwarningIntervalsID)
                    Dim blnIsChecked As Boolean = False



                    For Each drwMeneLeafList As BusinessObject.dstLoanType.spr_LoanType_List_SelectRow In dtblLoanMenuLeafList.Rows
                        For Each drwWarningIntrevalLoan As BusinessObject.dstHadiWarningIntervals.spr_HadiWarningIntervalsLoan_SelectRow In dtblHadiWarngIntervalsLoan

                            If drwMeneLeafList.ID = drwWarningIntrevalLoan.FK_LoanTypeID Then
                                strchklstLoanTypeLeaves &= "<div class='checkbox'> <label> <input type='checkbox' checked='checked' value='" & drwMeneLeafList.ID & "' name='LoanchklstMenu" & drwMeneLeafList.ID & "'><i class='fa " & drwMeneLeafList.ID & " fa-1x'></i> " & drwMeneLeafList.LoanType & "</label></div>"
                                blnIsChecked = True
                            End If

                        Next
                        If blnIsChecked = False Then
                                strchklstLoanTypeLeaves &= "<div class='checkbox'> <label> <input type='checkbox' value='" & drwMeneLeafList.ID & "' name='LoanchklstMenu" & drwMeneLeafList.ID & "'><i class='fa " & drwMeneLeafList.ID & " fa-1x'></i> " & drwMeneLeafList.LoanType & "</label></div>"
                            End If
                            blnIsChecked = False
                        Next drwMeneLeafList

                        divchklstLoanTypeItems.InnerHtml = strchklstLoanTypeLeaves


                End If


                ''Fill Branch Tree
                Dim tadpWarningIntervalBranchCheck As New BusinessObject.dstHadiWarningIntervalsBranchTableAdapters.spr_HadiWarningIntervalsBranch_Check_SelectTableAdapter
                Dim dtblWarningIntervalBranchCheck As BusinessObject.dstHadiWarningIntervalsBranch.spr_HadiWarningIntervalsBranch_Check_SelectDataTable = Nothing

                Dim tadpWarningIntervalBranchProvinceCheck As New BusinessObject.dstHadiWarningIntervalsBranchTableAdapters.spr_HadiWarningIntervalsBranchProvince_Check_SelectTableAdapter
                Dim dtblWarningIntervalBranchProvinceCheck As BusinessObject.dstHadiWarningIntervalsBranch.spr_HadiWarningIntervalsBranchProvince_Check_SelectDataTable = Nothing

                Dim tadpBrnachList As New BusinessObject.dstBranchTableAdapters.spr_Branch_List_SelectTableAdapter
                Dim dtblBranchList As BusinessObject.dstBranch.spr_Branch_List_SelectDataTable = Nothing


                Dim tadpProvince As New BusinessObject.dstBranchTableAdapters.spr_ProvinceList_SelectTableAdapter
                Dim dtblProvince As BusinessObject.dstBranch.spr_ProvinceList_SelectDataTable = Nothing

                dtblProvince = tadpProvince.GetData()

                For Each drwProvince As BusinessObject.dstBranch.spr_ProvinceList_SelectRow In dtblProvince



                    Dim trNodeProvince As New TreeNode
                    trNodeProvince.Value = drwProvince.ID
                    trNodeProvince.Text = "&nbsp;&nbsp;+" & drwProvince.Province
                    trNodeProvince.SelectAction = TreeNodeSelectAction.Expand
                    trState.Nodes.Add(trNodeProvince)
                    trNodeProvince.ShowCheckBox = True

                    Dim blnFlag As Boolean = False
                    Dim blnChecked As Boolean = False

                    dtblWarningIntervalBranchProvinceCheck = tadpWarningIntervalBranchProvinceCheck.GetData(intEditHadiwarningIntervalsID, drwProvince.ID)
                    dtblBranchList = tadpBrnachList.GetData(2, drwProvince.ID)

                    If dtblBranchList.Rows.Count = dtblWarningIntervalBranchProvinceCheck.Rows.Count Then
                        blnChecked = True
                    End If

                    For Each drwBranchList As BusinessObject.dstBranch.spr_Branch_List_SelectRow In dtblBranchList

                        Dim trNodeBranch As New TreeNode

                        trNodeBranch.Text = "&nbsp;&nbsp;" & drwBranchList.BrnachCode
                        trNodeBranch.Value = drwBranchList.ID
                        trNodeBranch.ShowCheckBox = True
                        trNodeBranch.SelectAction = TreeNodeSelectAction.None


                        For Each drwWarningIntervalBranchProvince As BusinessObject.dstHadiWarningIntervalsBranch.spr_HadiWarningIntervalsBranchProvince_Check_SelectRow In dtblWarningIntervalBranchProvinceCheck.Rows

                            If drwWarningIntervalBranchProvince.ID = drwBranchList.ID Then
                                trNodeBranch.Checked = True
                                trNodeProvince.Expanded = True

                                blnFlag = True
                            End If


                        Next


                        trNodeProvince.ChildNodes.Add(trNodeBranch)

                    Next drwBranchList

                    If blnFlag = False Then
                        trNodeProvince.CollapseAll()
                    End If

                    If blnChecked = True Then
                        trNodeProvince.Checked = True
                    End If

                Next



            Catch ex As Exception
                Dim str As String = ex.Message
            End Try

        Else


            Dim tadpDepositList As New BusinessObject.dstDepositTableAdapters.spr_DepositType_List_SelectTableAdapter
            Dim dtblDepositList As BusinessObject.dstDeposit.spr_DepositType_List_SelectDataTable = Nothing
            dtblDepositList = tadpDepositList.GetData()

            Dim strchklstDepositLeaves As String = ""

            For Each drwMeneLeafList As BusinessObject.dstDeposit.spr_DepositType_List_SelectRow In dtblDepositList.Rows


                Dim boolChecked As Boolean = False

                For i As Integer = 0 To Request.Form.Keys.Count - 1

                    If Request.Form.Keys(i).StartsWith("chklstMenu") = True Then

                        If CInt(Request.Form(i)) = drwMeneLeafList.ID Then
                            boolChecked = True
                            Exit For
                        End If

                    End If

                Next


                If boolChecked = True Then
                    strchklstDepositLeaves &= "<div class='checkbox'> <label> <input type='checkbox' checked='checked' value='" & drwMeneLeafList.ID & "' name='chklstMenu" & drwMeneLeafList.ID & "'><i class='fa " & drwMeneLeafList.ID & " fa-1x'></i> " & drwMeneLeafList.Deposit & "</label></div>"
                Else
                    strchklstDepositLeaves &= "<div class='checkbox'> <label> <input type='checkbox' value='" & drwMeneLeafList.ID & "' name='chklstMenu" & drwMeneLeafList.ID & "'><i class='fa " & drwMeneLeafList.ID & " fa-1x'></i> " & drwMeneLeafList.Deposit & "</label></div>"
                End If


            Next drwMeneLeafList

            divchklstDepositItems.InnerHtml = strchklstDepositLeaves




        End If


    End Sub

    Private Sub Bootstrap_Panel1_Panel_Cancel_Click(sender As Object, e As System.EventArgs) Handles Bootstrap_Panel1.Panel_Cancel_Click
        Response.Redirect("HadiwarningIntervalsManagement.aspx")
        Return
    End Sub


    Private Sub Bootstrap_Panel1_Panel_Save_Click(sender As Object, e As System.EventArgs) Handles Bootstrap_Panel1.Panel_Save_Click

        Dim dtblUserLogin As BusinessObject.dstUser.spr_User_Login_SelectDataTable = CType(Session("dtblUserLogin"), BusinessObject.dstUser.spr_User_Login_SelectDataTable)
        Dim drwUserLogin As BusinessObject.dstUser.spr_User_Login_SelectRow = dtblUserLogin.Rows(0)


        Dim intEditHadiwarningIntervalsID As Integer = CInt(Session("intEditHadiwarningIntervalsID"))

        Dim strWarniningTitle As String = txtWarningIntervalsName.Text.Trim

        Dim intFromDay As Integer = CInt(txtFrom.Text)
        Dim intToDay As Integer = CInt(txtTo.Text)
        Dim intFrequencyInDay As Integer = CInt(cmbFrequencyInDay.SelectedValue)
        Dim timeStartTime As TimeSpan = TimeSpan.Parse(StartTimePicker.Value)
        Dim intFrequencyperiodHour As Integer = CInt(cmbFrequencyInHour.SelectedValue)
        Dim blnSendSMS As Boolean = chkbxSendSMS.Checked
        Dim blnCallTelephone As Boolean = chkbxCallTelephone.Checked
        Dim blnVoiceMessage As Boolean = chkbxVoiceMessage.Checked
        Dim blnForDeposit As Boolean = If(rdoNewDeposit.Checked, True, False)
        Dim blnStatus As Boolean = chkStatus.Checked
        Dim blnLoanApprovment As Boolean = rdboLoanApprovment.Checked
        Dim blnIssuingContract As Boolean = rdboIssuingContract.Checked
        Dim blnLaonPaid As Boolean = rdboLaonPaid.Checked

        If rdoGetDeposit.Checked And hdnWarningIntervalTypeNew.Value <> "Loan" Then

            ''Delete related Draft
            Dim qryHadiWarningInrervalDraft As New BusinessObject.dstHadiDraftTableAdapters.QueriesTableAdapter
            qryHadiWarningInrervalDraft.spr_HadiDraftText_Delete(intEditHadiwarningIntervalsID)

        ElseIf rdoNewDeposit.Checked And hdnWarningIntervalTypeNew.Value <> "Deposit" Then

            ''Delete related Draft
            Dim qryHadiWarningInrervalDraft As New BusinessObject.dstHadiDraftTableAdapters.QueriesTableAdapter
            qryHadiWarningInrervalDraft.spr_HadiDraftText_Delete(intEditHadiwarningIntervalsID)

        End If

        Try

            Dim dtblWarningIntervalOverlap As BusinessObject.dstHadiWarningIntervals.spr_HadiWarningIntervals_CheckOverlap_SelectDataTable = Nothing
            Dim tadpWarningIntervalOverlap As New BusinessObject.dstHadiWarningIntervalsTableAdapters.spr_HadiWarningIntervals_CheckOverlap_SelectTableAdapter


            Dim tadpBrnachList As New BusinessObject.dstBranchTableAdapters.spr_Branch_List_SelectTableAdapter
            Dim dtblBranchList As BusinessObject.dstBranch.spr_Branch_List_SelectDataTable = Nothing

            ''For i As Integer = 0 To Request.Form.Keys.Count - 1

            ''    If rdoNewDeposit.Checked = True Then

            ''        If Request.Form.Keys(i).StartsWith("chklstMenu") = True Then

            ''            For Each trNode As TreeNode In trState.Nodes
            ''                If trNode.ChildNodes.Count = 0 Then
            ''                    If trNode.Checked = True Then

            ''                        dtblBranchList = tadpBrnachList.GetData(2, trNode.Value)

            ''                        For Each drwBranch As BusinessObject.dstBranch.spr_Branch_List_SelectRow In dtblBranchList

            ''                            dtblWarningIntervalOverlap = tadpWarningIntervalOverlap.GetData(2, intEditHadiwarningIntervalsID, CInt(Request.Form(i)), intFromDay, intToDay, drwBranch.ID)

            ''                            If dtblWarningIntervalOverlap.Rows.Count <> 0 Then

            ''                                Bootstrap_Panel1.ShowMessage("نوع سپرده " & dtblWarningIntervalOverlap.First.DepositName & " در بازه گردش کار انتخابی با گردش کار " & dtblWarningIntervalOverlap.First.WarniningTitle & " " & "تداخل دارد.", True)
            ''                                Return

            ''                            End If

            ''                        Next

            ''                    End If

            ''                Else

            ''                    For Each trChildNode As TreeNode In trNode.ChildNodes

            ''                        If trChildNode.ChildNodes.Count = 0 Then
            ''                            If trChildNode.Checked = True Then
            ''                                Dim ChildID As Integer = CInt(trChildNode.Value)
            ''                                dtblWarningIntervalOverlap = tadpWarningIntervalOverlap.GetData(2, intEditHadiwarningIntervalsID, CInt(Request.Form(i)), intFromDay, intToDay, ChildID)

            ''                                If dtblWarningIntervalOverlap.Rows.Count <> 0 Then

            ''                                    Bootstrap_Panel1.ShowMessage("نوع سپرده " & dtblWarningIntervalOverlap.First.DepositName & " در بازه گردش کار انتخابی با گردش کار " & dtblWarningIntervalOverlap.First.WarniningTitle & " " & "تداخل دارد.", True)
            ''                                    Return

            ''                                End If


            ''                            End If

            ''                        End If


            ''                    Next

            ''                End If
            ''            Next


            ''        End If

            ''    Else

            ''        If Request.Form.Keys(i).StartsWith("LoanchklstMenu") = True Then
            ''            For Each trNode As TreeNode In trState.Nodes
            ''                If trNode.ChildNodes.Count = 0 Then
            ''                    If trNode.Checked = True Then

            ''                        dtblBranchList = tadpBrnachList.GetData(2, trNode.Value)

            ''                        For Each drwBranch As BusinessObject.dstBranch.spr_Branch_List_SelectRow In dtblBranchList

            ''                            dtblWarningIntervalOverlap = tadpWarningIntervalOverlap.GetData(4, intEditHadiwarningIntervalsID, CInt(Request.Form(i)), intFromDay, intToDay, drwBranch.ID)

            ''                            If dtblWarningIntervalOverlap.Rows.Count <> 0 Then

            ''                                Bootstrap_Panel1.ShowMessage("نوع وام " & dtblWarningIntervalOverlap.First.DepositName & " در بازه گردش کار انتخابی با گردش کار " & dtblWarningIntervalOverlap.First.WarniningTitle & " " & "تداخل دارد.", True)
            ''                                Return

            ''                            End If

            ''                        Next

            ''                    End If

            ''                Else

            ''                    For Each trChildNode As TreeNode In trNode.ChildNodes

            ''                        If trChildNode.ChildNodes.Count = 0 Then
            ''                            If trChildNode.Checked = True Then
            ''                                Dim ChildID As Integer = CInt(trChildNode.Value)
            ''                                dtblWarningIntervalOverlap = tadpWarningIntervalOverlap.GetData(4, intEditHadiwarningIntervalsID, CInt(Request.Form(i)), intFromDay, intToDay, ChildID)

            ''                                If dtblWarningIntervalOverlap.Rows.Count <> 0 Then

            ''                                    Bootstrap_Panel1.ShowMessage("نوع وام " & dtblWarningIntervalOverlap.First.DepositName & " در بازه گردش کار انتخابی با گردش کار " & dtblWarningIntervalOverlap.First.WarniningTitle & " " & "تداخل دارد.", True)
            ''                                    Return

            ''                                End If


            ''                            End If

            ''                        End If


            ''                    Next

            ''                End If
            ''            Next
            ''        End If

            ''    End If

            ''Next i



            Dim qryHadiwarningIntervals As New BusinessObject.dstHadiWarningIntervalsTableAdapters.QueriesTableAdapter
            qryHadiwarningIntervals.spr_HadiWarningIntervals_Update(intEditHadiwarningIntervalsID, intFromDay, intToDay, strWarniningTitle, intFrequencyInDay, timeStartTime, intFrequencyperiodHour, blnSendSMS, blnCallTelephone, blnVoiceMessage, blnForDeposit, drwUserLogin.ID, blnStatus, blnLoanApprovment, blnIssuingContract, blnLaonPaid)


            Dim qryHadiwarningIntervalsDeposit As New BusinessObject.dstHadiWarningIntervalsDepositTableAdapters.QueriesTableAdapter
            qryHadiwarningIntervalsDeposit.spr_HadiWarningIntervalsDeposit_WarningInterval_Delete(intEditHadiwarningIntervalsID)
            qryHadiwarningIntervals.spr_HadiWarningIntervalsLoan_Delete(intEditHadiwarningIntervalsID)

            ''Dim cntxVar As New BusinessObject.dbMehrVosulEntities1

            ''Dim WarningIntervalsLoan = cntxVar.tbl_HadiWarningIntervalsLoan.Where(Function(x) x.FK_HadiWarningIntervalsID = intEditHadiwarningIntervalsID)

            ''If WarningIntervalsLoan IsNot Nothing Then
            ''    cntxVar.tbl_HadiWarningIntervalsLoan.RemoveRange(WarningIntervalsLoan)
            ''    cntxVar.SaveChanges()
            ''End If


            Dim qryWarningIntervalBracnh As New BusinessObject.dstHadiWarningIntervalsBranchTableAdapters.QueriesTableAdapter
            qryWarningIntervalBracnh.spr_HadiWarningIntervalsBranch_Delete(intEditHadiwarningIntervalsID)



            For i As Integer = 0 To Request.Form.Keys.Count - 1

                If Request.Form.Keys(i).StartsWith("chklstMenu") = True Then

                    qryHadiwarningIntervalsDeposit.spr_HadiWarningIntervalsDeposit_Insert(intEditHadiwarningIntervalsID, CInt(Request.Form(i)))

                End If

                If Request.Form.Keys(i).StartsWith("LoanchklstMenu") = True Then


                    ''Dim newHadiLoan As New BusinessObject.tbl_HadiWarningIntervalsLoan
                    ''newHadiLoan.FK_HadiWarningIntervalsID = intEditHadiwarningIntervalsID
                    ''newHadiLoan.FK_LoanTypeID = CInt(Request.Form(i))

                    ''cntxVar.tbl_HadiWarningIntervalsLoan.Add(newHadiLoan)
                    ''cntxVar.SaveChanges()
                    qryHadiwarningIntervals.spr_HadiWarningIntervalsLoan_Insert(intEditHadiwarningIntervalsID, CInt(Request.Form(i)))


                End If


            Next



            For Each trNode As TreeNode In trState.Nodes
                If trNode.ChildNodes.Count = 0 Then
                    If trNode.Checked = True Then

                        dtblBranchList = tadpBrnachList.GetData(2, trNode.Value)

                        For Each drwBranch As BusinessObject.dstBranch.spr_Branch_List_SelectRow In dtblBranchList

                            qryWarningIntervalBracnh.spr_HadiWarningIntervalsBranch_Insert(intEditHadiwarningIntervalsID, drwBranch.ID)
                        Next

                    End If

                Else

                    For Each trChildNode As TreeNode In trNode.ChildNodes

                        If trChildNode.ChildNodes.Count = 0 Then
                            If trChildNode.Checked = True Then
                                Dim ChildID As Integer = CInt(trChildNode.Value)
                                qryWarningIntervalBracnh.spr_HadiWarningIntervalsBranch_Insert(intEditHadiwarningIntervalsID, ChildID)

                            End If

                        End If


                    Next

                End If
            Next

        Catch ex As Exception
            Response.Redirect("HadiwarningIntervalsManagement.aspx?Edit=NO")
            Return
        End Try

        Response.Redirect("HadiwarningIntervalsManagement.aspx?Edit=OK")



    End Sub


End Class