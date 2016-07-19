Public Class HadiWarningIntervalsNew
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
            StartTimePicker.Hour = 8


            Dim tadpDepositList As New BusinessObject.dstDepositTableAdapters.spr_DepositType_List_SelectTableAdapter
            Dim dtblMenuLeafList As BusinessObject.dstDeposit.spr_DepositType_List_SelectDataTable = Nothing
            dtblMenuLeafList = tadpDepositList.GetData()

            Dim strchklstLoanTypeLeaves As String = ""

            For Each drwMeneLeafList As BusinessObject.dstDeposit.spr_DepositType_List_SelectRow In dtblMenuLeafList.Rows
                strchklstLoanTypeLeaves &= "<div class='checkbox'> <label> <input type='checkbox' value='" & drwMeneLeafList.ID & "' name='chklstMenu" & drwMeneLeafList.ID & "'><i class='fa " & drwMeneLeafList.ID & " fa-1x'></i> " & drwMeneLeafList.Deposit & "</label></div>"
            Next drwMeneLeafList

            divchklstDepositItems.InnerHtml = strchklstLoanTypeLeaves



            ''Fill Branch Tree
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



                Dim tadpBrnachList As New BusinessObject.dstBranchTableAdapters.spr_Branch_List_SelectTableAdapter
                Dim dtblBranchList As BusinessObject.dstBranch.spr_Branch_List_SelectDataTable = Nothing

                dtblBranchList = tadpBrnachList.GetData(2, drwProvince.ID)




                'Dim FirsChild = dtblBranchList.AsEnumerable()

                For Each drwBranch As BusinessObject.dstBranch.spr_Branch_List_SelectRow In dtblBranchList.Rows
                    Dim trNodeBranch As New TreeNode

                    trNodeBranch.Text = "&nbsp;&nbsp;" & drwBranch.BrnachCode
                    trNodeBranch.Value = drwBranch.ID
                    trNodeBranch.ShowCheckBox = True
                    trNodeBranch.SelectAction = TreeNodeSelectAction.None
                    trNodeProvince.ChildNodes.Add(trNodeBranch)


                Next drwBranch




            Next
            'trState.ShowCheckBoxes = TreeNodeTypes.All
            trState.CollapseAll()
            treeViewDiv.Style.Add("display", "block")


        Else

            Dim tadpDepositList As New BusinessObject.dstDepositTableAdapters.spr_DepositType_List_SelectTableAdapter
            Dim dtblDepositList As BusinessObject.dstDeposit.spr_DepositType_List_SelectDataTable = Nothing
            dtblDepositList = tadpDepositList.GetData()

            Dim strchklstLoanLeaves As String = ""

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
                    strchklstLoanLeaves &= "<div class='checkbox'> <label> <input type='checkbox' checked='checked' value='" & drwMeneLeafList.ID & "' name='chklstMenu" & drwMeneLeafList.ID & "'><i class='fa " & drwMeneLeafList.ID & " fa-1x'></i> " & drwMeneLeafList.Deposit & "</label></div>"
                Else
                    strchklstLoanLeaves &= "<div class='checkbox'> <label> <input type='checkbox' value='" & drwMeneLeafList.ID & "' name='chklstMenu" & drwMeneLeafList.ID & "'><i class='fa " & drwMeneLeafList.ID & " fa-1x'></i> " & drwMeneLeafList.Deposit & "</label></div>"
                End If


            Next drwMeneLeafList

            divchklstDepositItems.InnerHtml = strchklstLoanLeaves


        End If


    End Sub



    Private Sub Bootstrap_Panel1_Panel_Cancel_Click(sender As Object, e As System.EventArgs) Handles Bootstrap_Panel1.Panel_Cancel_Click
        Response.Redirect("HadiWarningIntervalsManagement.aspx")
        Return
    End Sub


    Private Sub Bootstrap_Panel1_Panel_Save_Click(sender As Object, e As System.EventArgs) Handles Bootstrap_Panel1.Panel_Save_Click

        Dim dtblUserLogin As BusinessObject.dstUser.spr_User_Login_SelectDataTable = CType(Session("dtblUserLogin"), BusinessObject.dstUser.spr_User_Login_SelectDataTable)
        Dim drwUserLogin As BusinessObject.dstUser.spr_User_Login_SelectRow = dtblUserLogin.Rows(0)



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

        Try


            Dim dtblWarningIntervalOverlap As BusinessObject.dstHadiWarningIntervals.spr_HadiWarningIntervals_CheckOverlap_SelectDataTable = Nothing
            Dim tadpWarningIntervalOverlap As New BusinessObject.dstHadiWarningIntervalsTableAdapters.spr_HadiWarningIntervals_CheckOverlap_SelectTableAdapter

            Dim qryHadiWarningIntervals As New BusinessObject.dstHadiWarningIntervalsTableAdapters.QueriesTableAdapter
            Dim qryWarningIntervalBranch As New BusinessObject.dstHadiWarningIntervalsBranchTableAdapters.QueriesTableAdapter

            Dim tadpBrnachList As New BusinessObject.dstBranchTableAdapters.spr_Branch_List_SelectTableAdapter
            Dim dtblBranchList As BusinessObject.dstBranch.spr_Branch_List_SelectDataTable = Nothing


            For i As Integer = 0 To Request.Form.Keys.Count - 1

                If Request.Form.Keys(i).StartsWith("chklstMenu") = True Then


                    For Each trNode As TreeNode In trState.Nodes
                        If trNode.ChildNodes.Count = 0 Then
                            If trNode.Checked = True Then

                                dtblBranchList = tadpBrnachList.GetData(2, trNode.Value)

                                For Each drwBranch As BusinessObject.dstBranch.spr_Branch_List_SelectRow In dtblBranchList

                                    dtblWarningIntervalOverlap = tadpWarningIntervalOverlap.GetData(1, -1, CInt(Request.Form(i)), intFromDay, intToDay, drwBranch.ID)

                                    If dtblWarningIntervalOverlap.Rows.Count <> 0 Then

                                        Bootstrap_Panel1.ShowMessage("نوع سپرده " & dtblWarningIntervalOverlap.First.DepositName & " در بازه گردش کار انتخابی با گردش کار " & dtblWarningIntervalOverlap.First.WarniningTitle & " " & "تداخل دارد.", True)
                                        Return

                                    End If

                                Next

                            End If

                        Else

                            For Each trChildNode As TreeNode In trNode.ChildNodes

                                If trChildNode.ChildNodes.Count = 0 Then
                                    If trChildNode.Checked = True Then
                                        Dim ChildID As Integer = CInt(trChildNode.Value)
                                        dtblWarningIntervalOverlap = tadpWarningIntervalOverlap.GetData(1, -1, CInt(Request.Form(i)), intFromDay, intToDay, ChildID)

                                        If dtblWarningIntervalOverlap.Rows.Count <> 0 Then

                                            Bootstrap_Panel1.ShowMessage("نوع سپرده " & dtblWarningIntervalOverlap.First.DepositName & " در بازه گردش کار انتخابی با گردش کار " & dtblWarningIntervalOverlap.First.WarniningTitle & " " & "تداخل دارد.", True)
                                            Return

                                        End If


                                    End If

                                End If


                            Next

                        End If
                    Next


                End If

            Next i

            ''Insert Interval
            Dim intHadiWarningIntervals As Integer = qryHadiWarningIntervals.spr_HadiWarningIntervals_Insert(intFromDay, intToDay, strWarniningTitle, intFrequencyInDay, timeStartTime, intFrequencyperiodHour, blnSendSMS, blnCallTelephone, blnVoiceMessage, blnForDeposit, drwUserLogin.ID, blnStatus)


            For i As Integer = 0 To Request.Form.Keys.Count - 1

                If Request.Form.Keys(i).StartsWith("chklstMenu") = True Then
                    Dim qryHadiWarningIntervalsDeposit As New BusinessObject.dstHadiWarningIntervalsDepositTableAdapters.QueriesTableAdapter
                    qryHadiWarningIntervalsDeposit.spr_HadiWarningIntervalsDeposit_Insert(intHadiWarningIntervals, CInt(Request.Form(i)))

                End If

            Next i


            For Each trNode As TreeNode In trState.Nodes
                If trNode.ChildNodes.Count = 0 Then
                    If trNode.Checked = True Then

                        dtblBranchList = tadpBrnachList.GetData(2, trNode.Value)

                        For Each drwBranch As BusinessObject.dstBranch.spr_Branch_List_SelectRow In dtblBranchList

                            qryWarningIntervalBranch.spr_HadiWarningIntervalsBranch_Insert(intHadiWarningIntervals, drwBranch.ID)
                        Next

                    End If

                Else

                    For Each trChildNode As TreeNode In trNode.ChildNodes

                        If trChildNode.ChildNodes.Count = 0 Then
                            If trChildNode.Checked = True Then
                                Dim ChildID As Integer = CInt(trChildNode.Value)
                                qryWarningIntervalBranch.spr_HadiWarningIntervalsBranch_Insert(intHadiWarningIntervals, ChildID)

                            End If

                        End If


                    Next

                End If
            Next


        Catch ex As Exception
            Response.Redirect("HadiWarningIntervalsManagement.aspx?Save=NO")
            Return
        End Try

        Response.Redirect("HadiWarningIntervalsManagement.aspx?Save=OK")



    End Sub


End Class