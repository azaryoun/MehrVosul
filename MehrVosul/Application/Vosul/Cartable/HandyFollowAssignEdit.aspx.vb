Public Class HandyFollowAssignEdit
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Page.Response.Cache.SetCacheability(HttpCacheability.NoCache)

        Bootstrap_Panel1.CanNew = False
        Bootstrap_Panel1.CanSave = False
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

            If Session("intFollowAssignID") Is Nothing Then
                Response.Redirect("HandyFollowManagement.aspx")
                Return
            End If

            Dim intFollowAssignID As Integer = CInt(Session("intFollowAssignID"))

            Dim dtblUserLogin As BusinessObject.dstUser.spr_User_Login_SelectDataTable = CType(Session("dtblUserLogin"), BusinessObject.dstUser.spr_User_Login_SelectDataTable)
            Dim drwUserLogin As BusinessObject.dstUser.spr_User_Login_SelectRow = dtblUserLogin.Rows(0)

            ''check the access Group id
            Dim tadpAccessgroupUser As New BusinessObject.dstAccessgroupUserTableAdapters.spr_AccessgroupUserByID_SelectTableAdapter
            Dim dtblAccessgroupUser As BusinessObject.dstAccessgroupUser.spr_AccessgroupUserByID_SelectDataTable = Nothing

            dtblAccessgroupUser = tadpAccessgroupUser.GetData(drwUserLogin.ID, 3436)
            If dtblAccessgroupUser.Rows.Count > 0 Then
                dtblAccessgroupUser = tadpAccessgroupUser.GetData(drwUserLogin.ID, 3431)
                If dtblAccessgroupUser.Count = 0 Then
                    Bootstrap_Panel1.CanSave = True
                End If
            ElseIf drwUserLogin.IsDataAdmin = True Then
                Bootstrap_Panel1.CanSave = True
            End If

            odsPerson.SelectParameters.Item("Action").DefaultValue = 1
            odsPerson.SelectParameters.Item("BranchID").DefaultValue = drwUserLogin.FK_BrnachID
            odsPerson.SelectParameters.Item("ProvinceID").DefaultValue = -1

            cmbPerson.DataBind()


            Dim dtblHandyFollowAssign As BusinessObject.dstHandyFollow.spr_HandyFollowAssign_SelectDataTable = Nothing
            Dim tadpHandyFollowAssign As New BusinessObject.dstHandyFollowTableAdapters.spr_HandyFollowAssign_SelectTableAdapter

            dtblHandyFollowAssign = tadpHandyFollowAssign.GetData(intFollowAssignID)

            Dim tadpFile As New BusinessObject.dstFileTableAdapters.spr_File_SelectTableAdapter
            Dim dtblFile As BusinessObject.dstFile.spr_File_SelectDataTable = Nothing

            dtblFile = tadpFile.GetData(1, dtblHandyFollowAssign.First.FK_FileID, "")

            Dim tadpLoan As New BusinessObject.dstLoanTableAdapters.spr_Loan_SelectTableAdapter
            Dim dtblLoan As BusinessObject.dstLoan.spr_Loan_SelectDataTable = Nothing

            dtblLoan = tadpLoan.GetData(2, dtblHandyFollowAssign.First.FK_LoanID, -1, -1)

            txtLoan.Text = dtblLoan.First.LoanNumber
            txtAssignDate.Text = mdlGeneral.GetPersianDateTime(dtblHandyFollowAssign.First.AssignDate)
            cmbPerson.SelectedValue = dtblHandyFollowAssign.First.FK_AssignUserID
            txtRemark.Text = dtblHandyFollowAssign.First.Remark

            ViewState("FileID") = dtblHandyFollowAssign.First.FK_FileID
            ViewState("LoanID") = dtblHandyFollowAssign.First.FK_LoanID
            ViewState("AssignType") = dtblHandyFollowAssign.First.AssignType


        Else

            Dim dtblUserLogin As BusinessObject.dstUser.spr_User_Login_SelectDataTable = CType(Session("dtblUserLogin"), BusinessObject.dstUser.spr_User_Login_SelectDataTable)
            Dim drwUserLogin As BusinessObject.dstUser.spr_User_Login_SelectRow = dtblUserLogin.Rows(0)

            ''check the access Group id
            Dim tadpAccessgroupUser As New BusinessObject.dstAccessgroupUserTableAdapters.spr_AccessgroupUserByID_SelectTableAdapter
            Dim dtblAccessgroupUser As BusinessObject.dstAccessgroupUser.spr_AccessgroupUserByID_SelectDataTable = Nothing

            dtblAccessgroupUser = tadpAccessgroupUser.GetData(drwUserLogin.ID, 3436)
            If dtblAccessgroupUser.Rows.Count > 0 Then
                dtblAccessgroupUser = tadpAccessgroupUser.GetData(drwUserLogin.ID, 3431)
                If dtblAccessgroupUser.Count = 0 Then
                    Bootstrap_Panel1.CanSave = True
                End If
            ElseIf drwUserLogin.IsDataAdmin = True Then
                Bootstrap_Panel1.CanSave = True
            End If

        End If


    End Sub

    Private Sub Bootstrap_Panel1_Panel_Cancel_Click(sender As Object, e As EventArgs) Handles Bootstrap_Panel1.Panel_Cancel_Click
        Response.Redirect("HandyFollowManagement.aspx")
    End Sub

    Private Sub Bootstrap_Panel1_Panel_Save_Click(sender As Object, e As EventArgs) Handles Bootstrap_Panel1.Panel_Save_Click
        Try

            Dim dtblUserLogin As BusinessObject.dstUser.spr_User_Login_SelectDataTable = CType(Session("dtblUserLogin"), BusinessObject.dstUser.spr_User_Login_SelectDataTable)
            Dim drwUserLogin As BusinessObject.dstUser.spr_User_Login_SelectRow = dtblUserLogin.Rows(0)


            Dim intAssignUserID As Integer = cmbPerson.SelectedValue
            Dim strRemark As String = txtRemark.Text

            Dim strNewRemark As String = txtNewRemark.Text
            Dim intFileID As Integer = CInt(ViewState("FileID"))
            Dim intLoanID As Integer = CInt(ViewState("LoanID"))
            Dim intAssignType As Int16 = CInt(ViewState("AssignType"))

            Dim HandyFollowAssign As New HandyFollowAssign
            If HandyFollowAssign.CheckFileAssign(intAssignUserID, intLoanID, intAssignType) = True Then
                Bootstrap_Panel1.ShowMessage("این پرونده قبلا به همین کارشناس اختصاص داده شده است", True)
                Return
            End If

            Dim qryHandyFollowAssign As New BusinessObject.dstHandyFollowTableAdapters.QueriesTableAdapter
            qryHandyFollowAssign.spr_HandyFollowAssign_Update(CInt(Session("intFollowAssignID")), drwUserLogin.ID, strRemark)

            qryHandyFollowAssign.spr_HandyFollowAssign_Insert(intAssignUserID, intFileID, DateTime.Now, drwUserLogin.ID, strNewRemark, intLoanID, intAssignType)


        Catch
            Response.Redirect("HandyFollowManagement.aspx?Edit=NO")
            Return
        End Try

        Response.Redirect("HandyFollowManagement.aspx?Edit=OK")

    End Sub
End Class