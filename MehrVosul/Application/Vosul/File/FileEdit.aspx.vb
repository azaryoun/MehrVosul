Public Class FileEdit
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


            If Session("intEditFileID") Is Nothing Then
                Response.Redirect("FileManagement.aspx")
                Return
            End If

            Dim intEditFileID As Integer = CInt(Session("intEditFileID"))

            Dim tadpFile As New BusinessObject.dstFileTableAdapters.spr_File_SelectTableAdapter
            Dim dtblFile As BusinessObject.dstFile.spr_File_SelectDataTable = Nothing

            dtblFile = tadpFile.GetData(1, intEditFileID)

            Dim drwFile As BusinessObject.dstFile.spr_File_SelectRow = dtblFile.Rows(0)


            With drwFile

                txtCustomerNO.Text = drwFile.CustomerNo
                txtName.Text = drwFile.FName
                txtLName.Text = drwFile.LName
                txtAddress.Text = drwFile.Address
                txtMobile.Text = drwFile.MobileNo
                txtFatherName.Text = drwFile.FatherName
                txtNationalID.Text = drwFile.NationalID
                txtIDNO.Text = drwFile.IDNumber
                txtWorkTel.Text = drwFile.TelephoneWork
                txtHomeTel.Text = drwFile.TelephoneHome
                rdbListSex.SelectedValue = If(.IsMale, 1, 0)

            End With


        End If


    End Sub



    Private Sub Bootstrap_Panel1_Panel_Cancel_Click(sender As Object, e As System.EventArgs) Handles Bootstrap_Panel1.Panel_Cancel_Click
        Response.Redirect("FileManagement.aspx")
        Return
    End Sub


    Private Sub Bootstrap_Panel1_Panel_Save_Click(sender As Object, e As System.EventArgs) Handles Bootstrap_Panel1.Panel_Save_Click

        Dim dtblUserLogin As BusinessObject.dstUser.spr_User_Login_SelectDataTable = CType(Session("dtblUserLogin"), BusinessObject.dstUser.spr_User_Login_SelectDataTable)
        Dim drwUserLogin As BusinessObject.dstUser.spr_User_Login_SelectRow = dtblUserLogin.Rows(0)

        Dim strFinalMessage As String = ""

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
            Dim intEditFileID As Integer = CInt(Session("intEditFileID"))

            Dim qryFiles As New BusinessObject.dstFileTableAdapters.QueriesTableAdapter

            If drwUserLogin.IsDataAdmin = True Then
                qryFiles.spr_File_Update(intEditFileID, strCustomerNO, strName, strLName, strFatherName, strMobileNO, strNationalID, strIDNO, strEmail, strAddress, strHomeTel, strWorkTel, blnIsMale, drwUserLogin.ID, 1)


            Else

                Dim tadpFile As New BusinessObject.dstFileTableAdapters.spr_File_SelectTableAdapter
                Dim dtblFile As BusinessObject.dstFile.spr_File_SelectDataTable = Nothing

                dtblFile = tadpFile.GetData(2, intEditFileID)
                Dim blnHasChanged As Boolean = False
                Dim intCUserID As Integer

                If dtblFile.Rows.Count > 0 Then

                    If dtblFile.First.State <> 2 AndAlso dtblFile.First.State <> 5 AndAlso dtblFile.First.State <> 8 Then

                        dtblFile = tadpFile.GetData(1, intEditFileID)
                   
                        Dim drwFile As BusinessObject.dstFile.spr_File_SelectRow = dtblFile.Rows(0)
                        intCUserID = drwFile.FK_CUserID


                        If txtCustomerNO.Text <> drwFile.CustomerNo Then
                            blnHasChanged = True

                        End If

                        If txtName.Text <> drwFile.FName Then
                            blnHasChanged = True

                        End If

                        If txtLName.Text <> drwFile.LName Then
                            blnHasChanged = True

                        End If

                        If txtAddress.Text <> drwFile.Address Then
                            blnHasChanged = True

                        End If

                        If txtMobile.Text <> drwFile.MobileNo Then
                            blnHasChanged = True

                        End If

                        If txtFatherName.Text <> drwFile.FatherName Then
                            blnHasChanged = True

                        End If

                        If txtNationalID.Text <> drwFile.NationalID Then
                            blnHasChanged = True

                        End If

                        If txtIDNO.Text <> drwFile.IDNumber Then
                            blnHasChanged = True

                        End If

                        If txtWorkTel.Text <> drwFile.TelephoneWork Then
                            blnHasChanged = True

                        End If

                        If txtHomeTel.Text <> drwFile.TelephoneHome Then
                            blnHasChanged = True

                        End If

                        If rdbListSex.SelectedValue <> If(drwFile.IsMale, 1, 0) Then
                            blnHasChanged = True

                        End If
                    Else

                        strFinalMessage = "NOTALLOWED"


                    End If
                Else


                    dtblFile = tadpFile.GetData(1, intEditFileID)

                    Dim drwFile As BusinessObject.dstFile.spr_File_SelectRow = dtblFile.Rows(0)

                    intCUserID = drwFile.FK_CUserID
                    For Each drwFileRequest As BusinessObject.dstFile.spr_File_SelectRow In dtblFile

                        intCUserID = drwFile.FK_CUserID

                        If txtCustomerNO.Text <> drwFile.CustomerNo Then
                            blnHasChanged = True
                            Exit For
                        End If

                        If txtName.Text <> drwFile.FName Then
                            blnHasChanged = True
                            Exit For
                        End If
                        If txtLName.Text <> drwFile.LName Then
                            blnHasChanged = True
                            Exit For
                        End If
                        If txtAddress.Text <> drwFile.Address Then
                            blnHasChanged = True
                            Exit For
                        End If
                        If txtMobile.Text <> drwFile.MobileNo Then
                            blnHasChanged = True
                            Exit For
                        End If
                        If txtFatherName.Text <> drwFile.FatherName Then
                            blnHasChanged = True
                            Exit For
                        End If
                        If txtNationalID.Text <> drwFile.NationalID Then
                            blnHasChanged = True
                            Exit For
                        End If
                        If txtIDNO.Text <> drwFile.IDNumber Then
                            blnHasChanged = True
                            Exit For
                        End If
                        If txtWorkTel.Text <> drwFile.TelephoneWork Then
                            blnHasChanged = True
                            Exit For
                        End If
                        If txtHomeTel.Text <> drwFile.TelephoneHome Then
                            blnHasChanged = True
                            Exit For
                        End If
                        If rdbListSex.SelectedValue <> If(drwFile.IsMale, 1, 0) Then
                            blnHasChanged = True
                            Exit For
                        End If

                    Next

                End If


                If blnHasChanged = True Then

                    qryFiles.spr_File_Insert(strCustomerNO, strName, strLName, strFatherName, strMobileNO, strNationalID, strIDNO, strEmail, strAddress, strHomeTel, strWorkTel, blnIsMale, intCUserID, 5, intEditFileID, drwUserLogin.ID)
                    strFinalMessage = "Requested"

                ElseIf strFinalMessage <> "NOTALLOWED" Then
                    strFinalMessage = "NOCHANGE"


                End If
               

            End If


        Catch ex As Exception
            Response.Redirect("FileManagement.aspx?Edit=NO")
            Return
        End Try


        If strFinalMessage = "" Then

            Response.Redirect("FileManagement.aspx?Edit=OK")

        Else
            Select Case strFinalMessage

                Case "Requested"
                    Response.Redirect("FileManagement.aspx?Edit=Requested")
                Case "NOCHANGE"
                    Response.Redirect("FileManagement.aspx?Edit=NOCHANGE")
                Case "NOTALLOWED"
                    Response.Redirect("FileManagement.aspx?Edit=NOTALLOWED")

            End Select


        End If
     


    End Sub
End Class