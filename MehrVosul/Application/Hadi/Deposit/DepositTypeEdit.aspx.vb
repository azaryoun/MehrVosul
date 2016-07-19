Public Class DepositTypeEdit
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

            If Session("intEditDepositID") Is Nothing Then
                Response.Redirect("DepositTypeManagement.aspx")
                Return
            End If

            Dim intEditDepositID As Integer = CInt(Session("intEditDepositID"))

            Dim tadpDeposit As New BusinessObject.dstDepositTableAdapters.spr_DepositType_SelectTableAdapter
            Dim dtblDeposit As BusinessObject.dstDeposit.spr_DepositType_SelectDataTable = Nothing
            dtblDeposit = tadpDeposit.GetData(2, intEditDepositID)

            If dtblDeposit.Rows.Count = 0 Then
                Response.Redirect("DepositTypeManagement.aspx")
                Return
            End If

            Dim drwDeposit As BusinessObject.dstDeposit.spr_DepositType_SelectRow = dtblDeposit.Rows(0)

            With drwDeposit
                txtDepositName.Text = .DepositName
                txtDepositCode.Text = .DepositCode

            End With

        End If



    End Sub



    Private Sub Bootstrap_Panel1_Panel_Cancel_Click(sender As Object, e As System.EventArgs) Handles Bootstrap_Panel1.Panel_Cancel_Click
        Response.Redirect("DepositTypeManagement.aspx")
        Return
    End Sub


    Private Sub Bootstrap_Panel1_Panel_Save_Click(sender As Object, e As System.EventArgs) Handles Bootstrap_Panel1.Panel_Save_Click

        Dim intEditDepositID As Integer = CInt(Session("intEditDepositID"))


        Dim dtblUserLogin As BusinessObject.dstUser.spr_User_Login_SelectDataTable = CType(Session("dtblUserLogin"), BusinessObject.dstUser.spr_User_Login_SelectDataTable)
        Dim drwUserLogin As BusinessObject.dstUser.spr_User_Login_SelectRow = dtblUserLogin.Rows(0)

        Dim strDepositName As String = txtDepositName.Text.Trim
        Dim strDepositCode As String = txtDepositCode.Text.Trim




        Try
            Dim qryDeposit As New BusinessObject.dstDepositTableAdapters.QueriesTableAdapter

            qryDeposit.spr_DepositType_Update(intEditDepositID, strDepositCode, strDepositName, drwUserLogin.ID)





        Catch ex As Exception
            Response.Redirect("DepositTypeManagement.aspx?Edit=NO")
            Return
        End Try

        Response.Redirect("DepositTypeManagement.aspx?Edit=OK")



    End Sub
End Class