Public Class LoanTypeEdit
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
        Bootstrap_Panel1.CanDisplay = False
        Bootstrap_Panel1.CanExcel = False
        Bootstrap_Panel1.CanConfirmRequest = False
        Bootstrap_Panel1.CanReject = False
        Bootstrap_Panel1.Enable_Save_Client_Validate = True

        lblInnerPageTitle.Text = "پرکردن کادرهای قرمز رنگ، اجباری است."

        If Page.IsPostBack = False Then

            If Session("intEditLoanTypeID") Is Nothing Then
                Response.Redirect("LoanTypeManagement.aspx")
                Return
            End If

            Dim intEditLoanTypeID As Integer = CInt(Session("intEditLoanTypeID"))

            Dim tadpLoanType As New BusinessObject.dstLoanTypeTableAdapters.spr_LoanType_SelectTableAdapter
            Dim dtblLoanType As BusinessObject.dstLoanType.spr_LoanType_SelectDataTable = Nothing
            dtblLoanType = tadpLoanType.GetData(intEditLoanTypeID)

            If dtblLoanType.Rows.Count = 0 Then
                Response.Redirect("LoanTypeManagement.aspx")
                Return
            End If

            Dim drwLoanType As BusinessObject.dstLoanType.spr_LoanType_SelectRow = dtblLoanType.Rows(0)

            With drwLoanType
                txtLoanTypeName.Text = .LoanTypeName
                txtLoanTypeCode.Text = .LoanTypeCode
                If .IsSectionNull = False Then
                    txtSection.Text = .Section
                End If



            End With

        End If



    End Sub



    Private Sub Bootstrap_Panel1_Panel_Cancel_Click(sender As Object, e As System.EventArgs) Handles Bootstrap_Panel1.Panel_Cancel_Click
        Response.Redirect("LoanTypeManagement.aspx")
        Return
    End Sub


    Private Sub Bootstrap_Panel1_Panel_Save_Click(sender As Object, e As System.EventArgs) Handles Bootstrap_Panel1.Panel_Save_Click

        Dim intEditLoanTypeID As Integer = CInt(Session("intEditLoanTypeID"))


        Dim dtblUserLogin As BusinessObject.dstUser.spr_User_Login_SelectDataTable = CType(Session("dtblUserLogin"), BusinessObject.dstUser.spr_User_Login_SelectDataTable)
        Dim drwUserLogin As BusinessObject.dstUser.spr_User_Login_SelectRow = dtblUserLogin.Rows(0)

        Dim strLoanTypeName As String = txtLoanTypeName.Text.Trim
        Dim strLoanTypeCode As String = txtLoanTypeCode.Text.Trim

       
       

        Try
            Dim qryLoanType As New BusinessObject.dstLoanTypeTableAdapters.QueriesTableAdapter
            If txtSection.Text.Trim = "" Then
                qryLoanType.spr_LoanType_Update(intEditLoanTypeID, strLoanTypeCode, strLoanTypeName, drwUserLogin.ID, Nothing)
            Else
                qryLoanType.spr_LoanType_Update(intEditLoanTypeID, strLoanTypeCode, strLoanTypeName, drwUserLogin.ID, txtSection.Text.Trim)
            End If





        Catch ex As Exception
            Response.Redirect("LoanTypeManagement.aspx?Edit=NO")
            Return
        End Try

        Response.Redirect("LoanTypeManagement.aspx?Edit=OK")



    End Sub
End Class