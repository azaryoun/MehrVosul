Public Class PreWarningIntervalsEdit
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

                If Session("intEditPreWarningIntervalID") Is Nothing Then
                    Response.Redirect("PreWarningIntervalsManagement.aspx")
                    Return
                End If

                Dim intEditWarningIntervalsID As Integer = CInt(Session("intEditPreWarningIntervalID"))




                Dim tadpWarningIntervals As New BusinessObject.dstPreWarningIntervalTableAdapters.spr_PreWarningIntervals_SelectTableAdapter
                Dim dtblWarningIntervals As BusinessObject.dstPreWarningInterval.spr_PreWarningIntervals_SelectDataTable = Nothing

                dtblWarningIntervals = tadpWarningIntervals.GetData(intEditWarningIntervalsID)

                Dim drwWarningIntervals As BusinessObject.dstPreWarningInterval.spr_PreWarningIntervals_SelectRow = dtblWarningIntervals.Rows(0)

                With drwWarningIntervals

                    txtWarningIntervalsName.Text = drwWarningIntervals.WarniningTitle
                    txtFrom.Text = drwWarningIntervals.onDay

                    chkbxVoiceMessage.Checked = drwWarningIntervals.VoiceMessage
                    chkbxSendSMS.Checked = drwWarningIntervals.SendSMS

                    txtMinimumAmount.Text = drwWarningIntervals.MinimumAmount
                    txtLoanAmount.Text = drwWarningIntervals.LoanAmount
                    txtInstalmentAmount.Text = drwWarningIntervals.InstalmentAmount
                    chkStatus.Checked = drwWarningIntervals.ISActive

                End With

                Dim tadpLoanTypeList As New BusinessObject.dstLoanTypeTableAdapters.spr_LoanType_List_SelectTableAdapter
                Dim dtblLoanTypeList As BusinessObject.dstLoanType.spr_LoanType_List_SelectDataTable = Nothing
                dtblLoanTypeList = tadpLoanTypeList.GetData()

                Dim tadpwarningLoan As New BusinessObject.dstPreWarningIntervalTableAdapters.spr_PreWarningIntervalsLoanType_SelectTableAdapter
                Dim dtbWarningLoan As BusinessObject.dstPreWarningInterval.spr_PreWarningIntervalsLoanType_SelectDataTable = Nothing
                dtbWarningLoan = tadpwarningLoan.GetData(3, -1, intEditWarningIntervalsID)

                Dim strchklstLoanLeaves As String = ""

                For Each drwMeneLeafList As BusinessObject.dstLoanType.spr_LoanType_List_SelectRow In dtblLoanTypeList.Rows

                    If dtbWarningLoan.Select("FK_LoanTypeID=" & drwMeneLeafList.ID).Length > 0 Then
                        strchklstLoanLeaves &= "<div class='checkbox'> <label> <input type='checkbox' checked='checked' value='" & drwMeneLeafList.ID & "' name='chklstMenu" & drwMeneLeafList.ID & "'><i class='fa " & drwMeneLeafList.ID & " fa-1x'></i> " & drwMeneLeafList.LoanType & "</label></div>"
                    Else
                        strchklstLoanLeaves &= "<div class='checkbox'> <label> <input type='checkbox' value='" & drwMeneLeafList.ID & "' name='chklstMenu" & drwMeneLeafList.ID & "'><i class='fa " & drwMeneLeafList.ID & " fa-1x'></i> " & drwMeneLeafList.LoanType & "</label></div>"
                    End If


                Next drwMeneLeafList

                divchklstLoanTypeItems.InnerHtml = strchklstLoanLeaves



                ''Fill Branch Tree
                Dim tadpWarningIntervalBranchCheck As New BusinessObject.dstPreWarningIntervalTableAdapters.spr_PreWarningIntervalsBranch_Check_SelectTableAdapter
                Dim dtblWarningIntervalBranchCheck As BusinessObject.dstPreWarningInterval.spr_PreWarningIntervalsBranch_Check_SelectDataTable = Nothing

                Dim tadpWarningIntervalBranchProvinceCheck As New BusinessObject.dstPreWarningIntervalTableAdapters.spr_PreWarningIntervalsBranchProvince_Check_SelectTableAdapter
                Dim dtblWarningIntervalBranchProvinceCheck As BusinessObject.dstPreWarningInterval.spr_PreWarningIntervalsBranchProvince_Check_SelectDataTable = Nothing

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

                    dtblWarningIntervalBranchProvinceCheck = tadpWarningIntervalBranchProvinceCheck.GetData(intEditWarningIntervalsID, drwProvince.ID)
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


                        For Each drwWarningIntervalBranchProvince As BusinessObject.dstPreWarningInterval.spr_PreWarningIntervalsBranchProvince_Check_SelectRow In dtblWarningIntervalBranchProvinceCheck.Rows

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


            Dim tadpLoanTypeList As New BusinessObject.dstLoanTypeTableAdapters.spr_LoanType_List_SelectTableAdapter
            Dim dtblLoanTypeList As BusinessObject.dstLoanType.spr_LoanType_List_SelectDataTable = Nothing
            dtblLoanTypeList = tadpLoanTypeList.GetData()

            Dim strchklstLoanLeaves As String = ""

            For Each drwMeneLeafList As BusinessObject.dstLoanType.spr_LoanType_List_SelectRow In dtblLoanTypeList.Rows


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
                    strchklstLoanLeaves &= "<div class='checkbox'> <label> <input type='checkbox' checked='checked' value='" & drwMeneLeafList.ID & "' name='chklstMenu" & drwMeneLeafList.ID & "'><i class='fa " & drwMeneLeafList.ID & " fa-1x'></i> " & drwMeneLeafList.LoanType & "</label></div>"
                Else
                    strchklstLoanLeaves &= "<div class='checkbox'> <label> <input type='checkbox' value='" & drwMeneLeafList.ID & "' name='chklstMenu" & drwMeneLeafList.ID & "'><i class='fa " & drwMeneLeafList.ID & " fa-1x'></i> " & drwMeneLeafList.LoanType & "</label></div>"
                End If


            Next drwMeneLeafList

            divchklstLoanTypeItems.InnerHtml = strchklstLoanLeaves




        End If


    End Sub

    Private Sub Bootstrap_Panel1_Panel_Cancel_Click(sender As Object, e As System.EventArgs) Handles Bootstrap_Panel1.Panel_Cancel_Click
        Response.Redirect("PreWarningIntervalsManagement.aspx")
        Return
    End Sub


    Private Sub Bootstrap_Panel1_Panel_Save_Click(sender As Object, e As System.EventArgs) Handles Bootstrap_Panel1.Panel_Save_Click

        Dim dtblUserLogin As BusinessObject.dstUser.spr_User_Login_SelectDataTable = CType(Session("dtblUserLogin"), BusinessObject.dstUser.spr_User_Login_SelectDataTable)
        Dim drwUserLogin As BusinessObject.dstUser.spr_User_Login_SelectRow = dtblUserLogin.Rows(0)


        Dim intEditWarningIntervalsID As Integer = CInt(Session("intEditPreWarningIntervalID"))

        Dim strWarniningTitle As String = txtWarningIntervalsName.Text.Trim

        Dim intFromDay As Integer = CInt(txtFrom.Text)

        Dim blnSendSMS As Boolean = chkbxSendSMS.Checked

        Dim blnVoiceMessage As Boolean = chkbxVoiceMessage.Checked

        Dim dblMinimumAmount As Double = CDbl(txtMinimumAmount.Text)
        Dim dblLoanAmount As Double = CDbl(txtLoanAmount.Text)
        Dim dblInstalmentAmount As Double = CDbl(txtInstalmentAmount.Text)
        Dim blnStatus As Boolean = chkStatus.Checked


        Try

            Dim dtblWarningIntervalOverlap As BusinessObject.dstPreWarningInterval.spr_PreWarningIntervals_CheckOverlap_SelectDataTable = Nothing
            Dim tadpWarningIntervalOverlap As New BusinessObject.dstPreWarningIntervalTableAdapters.spr_PreWarningIntervals_CheckOverlap_SelectTableAdapter



            Dim tadpBrnachList As New BusinessObject.dstBranchTableAdapters.spr_Branch_List_SelectTableAdapter
            Dim dtblBranchList As BusinessObject.dstBranch.spr_Branch_List_SelectDataTable = Nothing

            For i As Integer = 0 To Request.Form.Keys.Count - 1

                If Request.Form.Keys(i).StartsWith("chklstMenu") = True Then


                    For Each trNode As TreeNode In trState.Nodes
                        If trNode.ChildNodes.Count = 0 Then
                            If trNode.Checked = True Then

                                dtblBranchList = tadpBrnachList.GetData(2, trNode.Value)

                                For Each drwBranch As BusinessObject.dstBranch.spr_Branch_List_SelectRow In dtblBranchList

                                    dtblWarningIntervalOverlap = tadpWarningIntervalOverlap.GetData(2, intEditWarningIntervalsID, CInt(Request.Form(i)), intFromDay, drwBranch.ID)

                                    If dtblWarningIntervalOverlap.Rows.Count <> 0 Then

                                        Bootstrap_Panel1.ShowMessage("نوع وام " & dtblWarningIntervalOverlap.First.LoanTypeName & " در بازه گردش کار انتخابی با گردش کار " & dtblWarningIntervalOverlap.First.WarniningTitle & " " & "تداخل دارد.", True)
                                        Return

                                    End If

                                Next

                            End If

                        Else

                            For Each trChildNode As TreeNode In trNode.ChildNodes

                                If trChildNode.ChildNodes.Count = 0 Then
                                    If trChildNode.Checked = True Then
                                        Dim ChildID As Integer = CInt(trChildNode.Value)
                                        dtblWarningIntervalOverlap = tadpWarningIntervalOverlap.GetData(2, intEditWarningIntervalsID, CInt(Request.Form(i)), intFromDay, ChildID)

                                        If dtblWarningIntervalOverlap.Rows.Count <> 0 Then

                                            Bootstrap_Panel1.ShowMessage("نوع وام " & dtblWarningIntervalOverlap.First.LoanTypeName & " در بازه گردش کار انتخابی با گردش کار " & dtblWarningIntervalOverlap.First.WarniningTitle & " " & "تداخل دارد.", True)
                                            Return

                                        End If


                                    End If

                                End If


                            Next

                        End If
                    Next


                End If

            Next i

            Dim qryWarningIntervals As New BusinessObject.dstPreWarningIntervalTableAdapters.QueriesTableAdapter
            qryWarningIntervals.spr_PreWarningIntervals_Update(intEditWarningIntervalsID, intFromDay, strWarniningTitle, blnSendSMS, blnVoiceMessage, drwUserLogin.ID, dblMinimumAmount, dblLoanAmount, dblInstalmentAmount, blnStatus)


            qryWarningIntervals.spr_PreWarningIntervalsLoanType_WarningInterval_Delete(intEditWarningIntervalsID)

            qryWarningIntervals.spr_PreWarningIntervalsBranch_Delete(intEditWarningIntervalsID)



            For i As Integer = 0 To Request.Form.Keys.Count - 1

                If Request.Form.Keys(i).StartsWith("chklstMenu") = True Then

                    qryWarningIntervals.spr_PreWarningIntervalsLoanType_Insert(intEditWarningIntervalsID, CInt(Request.Form(i)))

                End If

            Next



            For Each trNode As TreeNode In trState.Nodes
                If trNode.ChildNodes.Count = 0 Then
                    If trNode.Checked = True Then

                        dtblBranchList = tadpBrnachList.GetData(2, trNode.Value)

                        For Each drwBranch As BusinessObject.dstBranch.spr_Branch_List_SelectRow In dtblBranchList

                            qryWarningIntervals.spr_PreWarningIntervalsBranch_Insert(intEditWarningIntervalsID, drwBranch.ID)
                        Next

                    End If

                Else

                    For Each trChildNode As TreeNode In trNode.ChildNodes

                        If trChildNode.ChildNodes.Count = 0 Then
                            If trChildNode.Checked = True Then
                                Dim ChildID As Integer = CInt(trChildNode.Value)
                                qryWarningIntervals.spr_PreWarningIntervalsBranch_Insert(intEditWarningIntervalsID, ChildID)

                            End If

                        End If


                    Next

                End If
            Next

        Catch ex As Exception
            Response.Redirect("PreWarningIntervalsManagement.aspx?Edit=NO")
            Return
        End Try

        Response.Redirect("PreWarningIntervalsManagement.aspx?Edit=OK")



    End Sub


End Class