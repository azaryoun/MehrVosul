﻿Imports System.Windows.Forms.VisualStyles.VisualStyleElement.Header

Public Class HandyFollowManagement
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Page.Response.Cache.SetCacheability(HttpCacheability.NoCache)

        Bootstrap_Panel1.CanNew = False
        Bootstrap_Panel1.CanSave = False
        Bootstrap_Panel1.CanDelete = False
        Bootstrap_Panel1.CanSearch = True
        Bootstrap_Panel1.CanCancel = False
        Bootstrap_Panel1.CanUp = False
        Bootstrap_Panel1.CanWizard = False
        Bootstrap_Panel1.CanConfirmRequest = False
        Bootstrap_Panel1.CanReject = False
        Bootstrap_Panel1.CanDisplay = False
        Bootstrap_Panel1.CanExcel = False
        Bootstrap_Panel1.Enable_Delete_Client_Validate = True
        Bootstrap_Panel1.Enable_Save_Client_Validate = True
        Bootstrap_Panel1.Enable_Search_Client_Validate = True

        lblInnerPageTitle.Text = "فهرست  تخصیص ها، لطفا طبق ضوابط عمل نمایید"
        Dim dtblUserLogin As BusinessObject.dstUser.spr_User_Login_SelectDataTable = CType(HttpContext.Current.Session("dtblUserLogin"), BusinessObject.dstUser.spr_User_Login_SelectDataTable)
        Dim drwUserLogin As BusinessObject.dstUser.spr_User_Login_SelectRow = dtblUserLogin.Rows(0)



        If Page.IsPostBack = False Then

            If Request.QueryString("Save") IsNot Nothing AndAlso Request.QueryString("Save") = "OK" Then
                Bootstrap_Panel1.ShowMessage("تخصیص پرونده با موفقیت انجام شد", False)
            ElseIf Request.QueryString("Edit") IsNot Nothing AndAlso Request.QueryString("Edit") = "OK" Then
                Bootstrap_Panel1.ShowMessage("تخصیص پرونده با موفقیت ویرایش شد", False)
            ElseIf Request.QueryString("Save") IsNot Nothing AndAlso Request.QueryString("Save") = "NO" Then
                Bootstrap_Panel1.ShowMessage("در فرآیند ذخیره تخصیص پرونده خطا رخ داده است", True)
            ElseIf Request.QueryString("Edit") IsNot Nothing AndAlso Request.QueryString("Edit") = "NO" Then
                Bootstrap_Panel1.ShowMessage("در فرآیند ویرایش تخصیص پرونده خطا رخ داده است", True)
            Else
                Bootstrap_Panel1.ClearMessage()
            End If


            If drwUserLogin.IsDataAdmin = True Then

                odsPerson.SelectParameters.Item("Action").DefaultValue = 1
                odsPerson.SelectParameters.Item("BranchID").DefaultValue = drwUserLogin.FK_BrnachID
                odsPerson.SelectParameters.Item("ProvinceID").DefaultValue = -1

                cmbPerson.DataBind()

                Bootstrap_Panel1.CanSave = True
                cmbPerson.Visible = True
                lblPerson.Visible = True
            Else

                ''check the access Group id
                Dim tadpAccessgroupUser As New BusinessObject.dstAccessgroupUserTableAdapters.spr_AccessgroupUserByID_SelectTableAdapter
                Dim dtblAccessgroupUser As BusinessObject.dstAccessgroupUser.spr_AccessgroupUserByID_SelectDataTable = Nothing

                dtblAccessgroupUser = tadpAccessgroupUser.GetData(drwUserLogin.ID, 3436)
                If dtblAccessgroupUser.Rows.Count > 0 Then
                    dtblAccessgroupUser = tadpAccessgroupUser.GetData(drwUserLogin.ID, 3431)
                    If dtblAccessgroupUser.Count = 0 Then
                        Bootstrap_Panel1.CanSave = True
                        cmbPerson.Visible = True
                        lblPerson.Visible = True
                    End If
                ElseIf drwUserLogin.FK_AccessGroupID = 3438 Then
                    Bootstrap_Panel1.CanSave = True
                    cmbPerson.Visible = True
                    lblPerson.Visible = True
                End If


                If drwUserLogin.FK_AccessGroupID = 3438 Then

                    odsPerson.SelectParameters.Item("Action").DefaultValue = 4
                    odsPerson.SelectParameters.Item("BranchID").DefaultValue = -1
                    odsPerson.SelectParameters.Item("ProvinceID").DefaultValue = -1

                    cmbPerson.DataBind()

                ElseIf drwUserLogin.IsDataAdmin = False AndAlso drwUserLogin.IsDataUserAdmin = True Then

                    odsPerson.SelectParameters.Item("Action").DefaultValue = 1
                    odsPerson.SelectParameters.Item("BranchID").DefaultValue = drwUserLogin.FK_BrnachID
                    odsPerson.SelectParameters.Item("ProvinceID").DefaultValue = -1

                    cmbPerson.DataBind()



                ElseIf drwUserLogin.IsDataAdmin = False AndAlso drwUserLogin.IsDataUserAdmin = False Then

                    odsPerson.SelectParameters.Item("Action").DefaultValue = 1
                    odsPerson.SelectParameters.Item("BranchID").DefaultValue = drwUserLogin.FK_BrnachID
                    odsPerson.SelectParameters.Item("ProvinceID").DefaultValue = -1

                    cmbPerson.DataBind()

                End If
            End If

        Else


            If drwUserLogin.IsDataAdmin = True Then
                Bootstrap_Panel1.CanSave = True
                cmbPerson.Visible = True
                lblPerson.Visible = True
            Else
                ''check the access Group id
                Dim tadpAccessgroupUser As New BusinessObject.dstAccessgroupUserTableAdapters.spr_AccessgroupUserByID_SelectTableAdapter
                Dim dtblAccessgroupUser As BusinessObject.dstAccessgroupUser.spr_AccessgroupUserByID_SelectDataTable = Nothing

                dtblAccessgroupUser = tadpAccessgroupUser.GetData(drwUserLogin.ID, 3436)
                If dtblAccessgroupUser.Rows.Count > 0 Then
                    dtblAccessgroupUser = tadpAccessgroupUser.GetData(drwUserLogin.ID, 3431)
                    If dtblAccessgroupUser.Count = 0 Then
                        Bootstrap_Panel1.CanSave = True
                        cmbPerson.Visible = True
                        lblPerson.Visible = True
                    End If
                ElseIf drwUserLogin.FK_AccessGroupID = 3438 Then
                    Bootstrap_Panel1.CanSave = True
                    cmbPerson.Visible = True
                    lblPerson.Visible = True
                End If

            End If

        End If


        If hdnAction.Value.StartsWith("E") = True Then


            Dim tadpHandyFollowAssign As New BusinessObject.dstHandyFollowTableAdapters.spr_HandyFollowAssign_SelectTableAdapter
            Dim dtblHandyFollowAssign As BusinessObject.dstHandyFollow.spr_HandyFollowAssign_SelectDataTable = Nothing

            Dim tadpTotalDeffredLCByLCNO As New BusinessObject.dstTotalDeffredLCTableAdapters.spr_TotalDeffredLC_SelectByLCNOTableAdapter
            Dim dtblTotalDeffredLCByLCNO As BusinessObject.dstTotalDeffredLC.spr_TotalDeffredLC_SelectByLCNODataTable = Nothing

            Dim intFollowAssignID As Integer = CInt(hdnAction.Value.Split(";")(1))


            If drwUserLogin.IsDataAdmin = True Then
                Session("intFollowAssignID") = intFollowAssignID
                Response.Redirect("HandyFollowAssignEdit.aspx")
            Else
                If drwUserLogin.FK_AccessGroupID = 3438 Then

                    Session("intFollowAssignID") = intFollowAssignID
                    Response.Redirect("HandyFollowAssignEdit.aspx")

                ElseIf drwUserLogin.IsDataUserAdmin = False Then

                    dtblHandyFollowAssign = tadpHandyFollowAssign.GetData(intFollowAssignID)

                    If dtblHandyFollowAssign.Rows.Count > 0 Then


                        dtblTotalDeffredLCByLCNO = tadpTotalDeffredLCByLCNO.GetData(dtblHandyFollowAssign.First.LoanNumber)


                        Dim intFileID As Long = dtblHandyFollowAssign.First.FK_FileID
                        Dim intLoanID As Long = dtblHandyFollowAssign.First.FK_LoanID

                        If dtblTotalDeffredLCByLCNO.Rows.Count > 0 Then
                            Session("AmountDeffed") = dtblTotalDeffredLCByLCNO.First.AmounDefferd
                            Session("customerNO") = dtblTotalDeffredLCByLCNO.First.CustomerNO
                        End If

                        Session("intFileID") = CObj(intFileID)
                        Session("intLoanID") = CObj(intLoanID)
                        Session("HandyFollowAssign") = dtblHandyFollowAssign.First.ID
                        Session("AssignType") = dtblHandyFollowAssign.First.AssignType

                        Response.Redirect("../HandyFollow/HandyFollowNew.aspx")
                    Else

                        Return

                    End If


                Else
                    Session("intFollowAssignID") = intFollowAssignID
                    Response.Redirect("HandyFollowAssignEdit.aspx")
                End If

            End If



        End If


    End Sub

    <System.Web.Services.WebMethod()> Public Shared Function GetPageRecords(intPageNo As Integer, strFilter As String) As String
        Try


            Dim dtblUserLogin As BusinessObject.dstUser.spr_User_Login_SelectDataTable = CType(HttpContext.Current.Session("dtblUserLogin"), BusinessObject.dstUser.spr_User_Login_SelectDataTable)
            Dim drwUserLogin As BusinessObject.dstUser.spr_User_Login_SelectRow = dtblUserLogin.Rows(0)


            Dim intAction As Integer
            If drwUserLogin.IsDataAdmin = True Then

                intAction = 5
                If strFilter IsNot Nothing Then
                    intAction = 6
                End If


            Else

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


                If drwUserLogin.FK_AccessGroupID = 3438 Then

                    intAction = 9
                    If strFilter IsNot Nothing Then
                        intAction = 10
                    End If

                ElseIf drwUserLogin.IsDataUserAdmin = True AndAlso blnAdminBranch = True Then
                    intAction = 3
                    If strFilter IsNot Nothing Then
                        intAction = 4
                    End If
                ElseIf drwUserLogin.IsDataUserAdmin = True AndAlso blnAdminBranch = False Then
                    intAction = 7
                    If strFilter IsNot Nothing Then
                        intAction = 8
                    End If

                Else
                    intAction = 1
                    If strFilter IsNot Nothing Then
                        intAction = 2
                    End If
                End If

            End If





            Dim intToIndex As Integer = intPageNo * mdlGeneral.cnst_RowsCountInPage
            Dim intFromIndex As Integer = (intToIndex - mdlGeneral.cnst_RowsCountInPage) + 1
            Dim tadpHandyFollowAssignManagement As New BusinessObject.dstHandyFollowTableAdapters.spr_HandyFollowAssign_Management_SelectTableAdapter
            Dim dtblHandyFollowAssignManagement As BusinessObject.dstHandyFollow.spr_HandyFollowAssign_Management_SelectDataTable = Nothing
            dtblHandyFollowAssignManagement = tadpHandyFollowAssignManagement.GetData(intAction, intFromIndex, intToIndex, strFilter, drwUserLogin.ID, drwUserLogin.FK_BrnachID, drwUserLogin.Fk_ProvinceID)

            Dim strResult As String = ""
            Dim intColumnCount As Integer = dtblHandyFollowAssignManagement.Columns.Count

            If intAction = 1 Or intAction = 3 Or intAction = 5 Or intAction = 7 Or intAction = 9 Then
                For Each drwHandyFollowAssignManagement As BusinessObject.dstHandyFollow.spr_HandyFollowAssign_Management_SelectRow In dtblHandyFollowAssignManagement.Rows


                    strResult &= ";n;;@;" & CStr(drwHandyFollowAssignManagement.Item(0))
                    For i As Integer = 1 To intColumnCount - 1
                        strResult &= ";@;" & CStr(drwHandyFollowAssignManagement.Item(i))
                    Next

                    Dim tadpHandyFollow As New BusinessObject.dstHandyFollowTableAdapters.spr_HandyFollowByAssignID_SelectTableAdapter
                    Dim dtblHandyFollow As BusinessObject.dstHandyFollow.spr_HandyFollowByAssignID_SelectDataTable = Nothing

                    dtblHandyFollow = tadpHandyFollow.GetData(2, -1, drwHandyFollowAssignManagement.Item(0))
                    If dtblHandyFollow.First.HandFollow > 0 Then
                        strResult &= ";@;ثبت شده"
                    Else
                        strResult &= ";@;ثبت نشده"
                    End If




                Next drwHandyFollowAssignManagement

            Else

                For Each drwHandyFollowAssignManagement As BusinessObject.dstHandyFollow.spr_HandyFollowAssign_Management_SelectRow In dtblHandyFollowAssignManagement.Rows


                    strResult &= ";n;;@;" & CStr(drwHandyFollowAssignManagement.Item(0))
                    strResult &= ";@;" & CStr(drwHandyFollowAssignManagement.Item(1))
                    For i As Integer = 2 To intColumnCount - 1
                        strResult &= ";@;" & CStr(drwHandyFollowAssignManagement.Item(i)).ToLower.Replace(strFilter.ToLower, "<b><font color='#0F17FF'>" & strFilter & "</font></b>")
                    Next


                    Dim tadpHandyFollow As New BusinessObject.dstHandyFollowTableAdapters.spr_HandyFollowByAssignID_SelectTableAdapter
                    Dim dtblHandyFollow As BusinessObject.dstHandyFollow.spr_HandyFollowByAssignID_SelectDataTable = Nothing

                    dtblHandyFollow = tadpHandyFollow.GetData(2, -1, drwHandyFollowAssignManagement.Item(0))
                    If dtblHandyFollow.First.HandFollow > 0 Then
                        strResult &= ";@;ثبت شده"
                    Else
                        strResult &= ";@;ثبت نشده"
                    End If

                Next drwHandyFollowAssignManagement


            End If



            Return strResult

        Catch ex As Exception
            Return "E"
        End Try


    End Function

    <System.Web.Services.WebMethod()> Public Shared Function GetPageCount(strFilter As String) As Integer()


        Dim dtblUserLogin As BusinessObject.dstUser.spr_User_Login_SelectDataTable = CType(HttpContext.Current.Session("dtblUserLogin"), BusinessObject.dstUser.spr_User_Login_SelectDataTable)
        Dim drwUserLogin As BusinessObject.dstUser.spr_User_Login_SelectRow = dtblUserLogin.Rows(0)

        Dim intAction As Integer
        If drwUserLogin.IsDataAdmin = True Then
            intAction = 5
            If strFilter IsNot Nothing Then
                intAction = 6

            End If
        Else


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


            If drwUserLogin.FK_AccessGroupID = 3438 Then

                intAction = 9
                If strFilter IsNot Nothing Then
                    intAction = 10
                End If


            ElseIf drwUserLogin.IsDataUserAdmin = True AndAlso blnAdminBranch = True Then
                intAction = 3
                If strFilter IsNot Nothing Then
                    intAction = 4
                End If
            ElseIf drwUserLogin.IsDataUserAdmin = True AndAlso blnAdminBranch = False Then
                intAction = 7
                If strFilter IsNot Nothing Then
                    intAction = 8
                End If

            Else
                intAction = 1
                If strFilter IsNot Nothing Then
                    intAction = 2
                End If
            End If
        End If


        Dim tadpHandyFollowAssignCount As New BusinessObject.dstHandyFollowTableAdapters.spr_HandyFollowAssign_Count_SelectTableAdapter
        Dim dtblHandyFollowAssignCount As BusinessObject.dstHandyFollow.spr_HandyFollowAssign_Count_SelectDataTable = Nothing
        dtblHandyFollowAssignCount = tadpHandyFollowAssignCount.GetData(intAction, strFilter, drwUserLogin.ID, drwUserLogin.FK_BrnachID, drwUserLogin.Fk_ProvinceID)
        Dim drwHandyFollowAssignCount As BusinessObject.dstHandyFollow.spr_HandyFollowAssign_Count_SelectRow = dtblHandyFollowAssignCount.Rows(0)
        Dim intPageCount As Integer = Math.Ceiling(drwHandyFollowAssignCount.HandyFollowAssign / mdlGeneral.cnst_RowsCountInPage)
        Dim arrResult(1) As Integer
        arrResult(0) = drwHandyFollowAssignCount.HandyFollowAssign
        arrResult(1) = intPageCount
        Return arrResult

    End Function

    <System.Web.Services.WebMethod()> Public Shared Function DeleteOperation_Server(theKeys() As Integer, theLoans() As String, assignUser As Integer) As String

        Dim dtblUserLogin As BusinessObject.dstUser.spr_User_Login_SelectDataTable = CType(HttpContext.Current.Session("dtblUserLogin"), BusinessObject.dstUser.spr_User_Login_SelectDataTable)
        Dim drwUserLogin As BusinessObject.dstUser.spr_User_Login_SelectRow = dtblUserLogin.Rows(0)

        Dim strLoan As String() = Nothing
        Dim list As New ArrayList
        Dim blnHasFind As Boolean = False

        For i As Integer = 0 To theKeys.Length - 1

            Dim intPKey As Integer = CInt(theKeys(i))

            Dim qryWarningIntervals As New BusinessObject.dstWarningIntervalsTableAdapters.QueriesTableAdapter
            Try


                For Each element As String In list

                    If element = theLoans(i) Then

                        blnHasFind = True

                    End If

                Next

                If blnHasFind = True Then
                    Continue For
                End If

                Dim tadpHandyFollowAsssign As New BusinessObject.dstHandyFollowTableAdapters.spr_HandyFollowAssign_SelectTableAdapter
                Dim dtblHandyFollowAssign As BusinessObject.dstHandyFollow.spr_HandyFollowAssign_SelectDataTable = Nothing

                dtblHandyFollowAssign = tadpHandyFollowAsssign.GetData(intPKey)

                If dtblHandyFollowAssign.Rows.Count > 0 Then

                    Dim qryHandyFollowAssign As New BusinessObject.dstHandyFollowTableAdapters.QueriesTableAdapter

                    If dtblHandyFollowAssign.First.IsAssigned = True Then
                        qryHandyFollowAssign.spr_HandyFollowAssign_Update(intPKey, drwUserLogin.ID, "")
                    End If

                    qryHandyFollowAssign.spr_HandyFollowAssign_Insert(assignUser, dtblHandyFollowAssign.First.FK_FileID, DateTime.Now, drwUserLogin.ID, "", dtblHandyFollowAssign.First.FK_LoanID, dtblHandyFollowAssign.First.AssignType)


                End If

                list.Add(theLoans(i).ToString())

            Catch ex As Exception
                Return ex.Message
            End Try



        Next i

        Return ""



    End Function

    Private Sub Bootstrap_Panel1_Panel_Wizard_Click(sender As Object, e As EventArgs) Handles Bootstrap_Panel1.Panel_Wizard_Click
        '' Response.Redirect("HandyFollowAssignMagic.aspx")
    End Sub


    <System.Web.Services.WebMethod()> Public Shared Function PrintOperation_Server(theKeys() As Integer) As String


        Try

            Dim strHTMLMain As String = ""
            For i As Integer = 0 To theKeys.Length - 1

                Dim intPKey As Integer = CInt(theKeys(i))

                Dim qryWarningIntervals As New BusinessObject.dstWarningIntervalsTableAdapters.QueriesTableAdapter

                Dim tadpFollowAssign As New BusinessObject.dstHandyFollowTableAdapters.spr_HandyFollowAssign_SelectTableAdapter
                Dim dtblFollowAssign As BusinessObject.dstHandyFollow.spr_HandyFollowAssign_SelectDataTable = Nothing

                dtblFollowAssign = tadpFollowAssign.GetData(intPKey)

                If dtblFollowAssign.First.AssignType <> 2 OrElse dtblFollowAssign.First.IsAssigned = False Then

                    Continue For

                End If


                ''get File ID
                Dim tadpFile As New BusinessObject.dstFileTableAdapters.spr_File_SelectTableAdapter
                Dim dtblFile As BusinessObject.dstFile.spr_File_SelectDataTable = Nothing
                dtblFile = tadpFile.GetData(1, dtblFollowAssign.First.FK_FileID, "")

                Dim strCustomerNO As String = dtblFile.First.CustomerNo

                Dim tadpLoan As New BusinessObject.dstLoanTableAdapters.spr_Loan_SelectTableAdapter
                Dim dtblLoan As BusinessObject.dstLoan.spr_Loan_SelectDataTable = Nothing

                dtblLoan = tadpLoan.GetData(2, dtblFollowAssign.First.FK_LoanID, -1, -1)

                Dim strLCNO As String = dtblLoan.First.LoanNumber


                strHTMLMain &= strCustomerNO & ";" & strLCNO & ","



            Next i

            ''     Response.Redirect("../HandyFollow/ManifestPreview.aspx?STRHTML=" & strHTMLMain)


            Return strHTMLMain

        Catch ex As Exception

            Return ""

        End Try

    End Function


End Class