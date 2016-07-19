Public Class WhiteListNew
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

            If Session("intEditFileID") Is Nothing And Session("intLoanID") Is Nothing Then
                Return
            End If

            Bootstrap_PersianDateExpireDate.GergorainDateTime = Date.Now.AddDays(3)
            Bootstrap_PersianDateExpireDate.PickerLabel = "تاریخ انقضا"

        End If

        Bootstrap_PersianDateExpireDate.ShowTimePicker = False


    End Sub



    Private Sub Bootstrap_Panel1_Panel_Cancel_Click(sender As Object, e As System.EventArgs) Handles Bootstrap_Panel1.Panel_Cancel_Click
        Response.Redirect("WhiteListManagement.aspx")
        Return
    End Sub


    Private Sub Bootstrap_Panel1_Panel_Save_Click(sender As Object, e As System.EventArgs) Handles Bootstrap_Panel1.Panel_Save_Click

        Dim dtblUserLogin As BusinessObject.dstUser.spr_User_Login_SelectDataTable = CType(Session("dtblUserLogin"), BusinessObject.dstUser.spr_User_Login_SelectDataTable)
        Dim drwUserLogin As BusinessObject.dstUser.spr_User_Login_SelectRow = dtblUserLogin.Rows(0)

        Dim tadpWhiteList As New BusinessObject.dstCommitmentActiveTableAdapters.spr_CommitmentActive_SelectTableAdapter
        Dim dtblWhiteList As BusinessObject.dstCommitmentActive.spr_CommitmentActive_SelectDataTable = Nothing

        dtblWhiteList = tadpWhiteList.GetData(3, -1, CInt(Session("intEditFileID")), CInt(Session("intLoanID")))

        If dtblWhiteList.Rows.Count > 0 Then

            Bootstrap_Panel1.ShowMessage("لیست سفید برای این پرونده وجود دارد", True)
            Exit Sub
        End If

        Dim strRemark As String = txtRemarks.Text
        Dim dtExpireDate As Date = Bootstrap_PersianDateExpireDate.GergorainDateTime

        If dtExpireDate < Date.Now Then

            Bootstrap_Panel1.ShowMessage("تاریخ انقضا باید از تاریخ امروز بزرگتر باشد", True)
            Exit Sub
        End If


        Try
            Dim qryWhiteList As New BusinessObject.dstCommitmentActiveTableAdapters.QueriesTableAdapter

            qryWhiteList.spr_CommitmentActive_Insert(CInt(Session("intEditFileID")), CInt(Session("intLoanID")), strRemark, drwUserLogin.ID, dtExpireDate)

  
        Catch ex As Exception
            Response.Redirect("WhiteListManagement.aspx?Save=NO")
            Return
        End Try

        Response.Redirect("WhiteListManagement.aspx?Save=OK")

       


    End Sub


End Class