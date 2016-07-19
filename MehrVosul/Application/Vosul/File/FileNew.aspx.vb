Public Class FileNew
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

        


        End If


    End Sub



    Private Sub Bootstrap_Panel1_Panel_Cancel_Click(sender As Object, e As System.EventArgs) Handles Bootstrap_Panel1.Panel_Cancel_Click
        Response.Redirect("FileManagement.aspx")
        Return
    End Sub


    Private Sub Bootstrap_Panel1_Panel_Save_Click(sender As Object, e As System.EventArgs) Handles Bootstrap_Panel1.Panel_Save_Click

        Dim dtblUserLogin As BusinessObject.dstUser.spr_User_Login_SelectDataTable = CType(Session("dtblUserLogin"), BusinessObject.dstUser.spr_User_Login_SelectDataTable)
        Dim drwUserLogin As BusinessObject.dstUser.spr_User_Login_SelectRow = dtblUserLogin.Rows(0)



        Dim strCustomerNO As String = txtCustomerNO.Text.Trim
        Dim strName As String = txtName.Text.Trim
        Dim strLName As String = txtLName.Text.Trim
        Dim strFatherName As String = txtFatherName.Text.Trim
        Dim strMobileNO As String = txtMobile.Text.Trim
        Dim strNationalID As String = txtNationalID.Text.Trim
        Dim strIDNO As String = txtIDNO.Text.Trim
        Dim strEmail As String = txtEmail.Text.Trim
        Dim strAddress As String = txtAddress.Text.Trim
        Dim strHomeTel As String = txtHomeTel.Text.Trim
        Dim strWorkTel As String = txtWorkTel.Text.Trim
        Dim blnIsMale As Boolean = If(rdbListSex.SelectedValue = 0, False, True)

        Try
            Dim qryFiles As New BusinessObject.dstFileTableAdapters.QueriesTableAdapter

            If drwUserLogin.IsDataAdmin = True Then
                qryFiles.spr_File_Insert(strCustomerNO, strName, strLName, strFatherName, strMobileNO, strNationalID, strIDNO, strEmail, strAddress, strHomeTel, strWorkTel, blnIsMale, drwUserLogin.ID, 1, Nothing, Nothing)

            Else
                qryFiles.spr_File_Insert(strCustomerNO, strName, strLName, strFatherName, strMobileNO, strNationalID, strIDNO, strEmail, strAddress, strHomeTel, strWorkTel, blnIsMale, drwUserLogin.ID, 8, Nothing, drwUserLogin.ID)


            End If


        Catch ex As Exception
            Response.Redirect("FileManagement.aspx?Save=NO")
            Return
        End Try


        If drwUserLogin.IsDataAdmin = True Then

            Response.Redirect("FileManagement.aspx?Save=OK")

        Else
            Response.Redirect("FileManagement.aspx?Save=Requested")
        End If


    End Sub

  
End Class