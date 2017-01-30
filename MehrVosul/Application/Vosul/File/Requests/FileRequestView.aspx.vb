Public Class FileRequestView
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        Page.Response.Cache.SetCacheability(HttpCacheability.NoCache)

        Dim dtblUserLogin As BusinessObject.dstUser.spr_User_Login_SelectDataTable = CType(Session("dtblUserLogin"), BusinessObject.dstUser.spr_User_Login_SelectDataTable)
        Dim drwUserLogin As BusinessObject.dstUser.spr_User_Login_SelectRow = dtblUserLogin.Rows(0)

        If drwUserLogin.IsDataAdmin = False Then

            Bootstrap_Panel1.CanConfirmRequest = False
            Bootstrap_Panel1.CanReject = False

        End If

        Bootstrap_Panel1.CanCancel = False
        Bootstrap_Panel1.CanNew = False
        Bootstrap_Panel1.CanSave = False
        Bootstrap_Panel1.CanDelete = False
        Bootstrap_Panel1.CanSearch = False
        Bootstrap_Panel1.CanDisplay = False
        Bootstrap_Panel1.CanExcel = False
        Bootstrap_Panel1.CanUp = True
        Bootstrap_Panel1.CanWizard = False
        Bootstrap_Panel1.Enable_Save_Client_Validate = True

        ''lblInnerPageTitle.Text = "پرکردن کادرهای قرمز رنگ، اجباری است."

        If Page.IsPostBack = False Then


            If Session("intFileRequestID") Is Nothing Then
                Response.Redirect("FileRequestManagement.aspx")
                Return
            End If

            Dim intFileRequestID As Integer = CInt(Session("intFileRequestID"))

            Dim tadpFile As New BusinessObject.dstFileTableAdapters.spr_File_SelectTableAdapter
            Dim dtblFile As BusinessObject.dstFile.spr_File_SelectDataTable = Nothing

            dtblFile = tadpFile.GetData(1, intFileRequestID, "")

            Dim drwFile As BusinessObject.dstFile.spr_File_SelectRow = dtblFile.Rows(0)

            Dim strState As String = ""
            With drwFile

                Select Case drwFile.State

                 
                    Case 2

                        strState = "درخواست حذف"

                    Case 3
                        strState = "حذف شد"

                    Case 4
                        strState = "عدم تایید حذف"

                    Case 5
                        strState = "درخواست ویرایش"


                    Case 6
                        strState = "ویرایش شد"

                    Case 7
                        strState = "عدم تایید ویرایش"

                    Case 8

                        strState = "درخواست تعریف"
                    Case 9

                        strState = "تعریف شد"
                    Case 10

                        strState = "عدم تایید تعریف"

                End Select

                Session("State") = drwFile.State
                If drwFile.IsFK_OriginalFileIDNull = False Then
                    Session("FileID") = drwFile.FK_OriginalFileID
                End If

                If drwFile.State <> 2 And drwFile.State <> 5 And drwFile.State <> 8 Then
                    Bootstrap_Panel1.CanConfirmRequest = False
                    Bootstrap_Panel1.CanReject = False

                End If

                Dim tadpUser As New BusinessObject.dstUserTableAdapters.spr_User_SelectTableAdapter
                Dim dtblUser As BusinessObject.dstUser.spr_User_SelectDataTable = Nothing

                ''get requested user UserName
                If drwFile.IsFK_RequestUserIDNull = False Then

                    dtblUser = tadpUser.GetData(drwFile.FK_RequestUserID)

                    lblRequestedUser.InnerText = dtblUser.First.Username
                End If

                ''get edited user UserName
                If drwFile.IsFK_EUserIDNull = False Then

                    dtblUser = tadpUser.GetData(drwFile.FK_EUserID)

                    lblCheckerUser.InnerText = dtblUser.First.Username
                Else
                    lblCheckerUser.InnerText = "---"
                End If

                lblCustomerNO.InnerText = drwFile.CustomerNo
                lblName.InnerText = drwFile.FName
                lblLName.InnerText = drwFile.LName
                lblAddress.InnerText = drwFile.Address
                lblMobile.InnerText = drwFile.MobileNo
                lblFatherName.InnerText = drwFile.FatherName
                lblHomeTel.InnerText = drwFile.TelephoneHome
                lblWorkTel.InnerText = drwFile.TelephoneWork
                lblNationalID.InnerText = drwFile.NationalID
                lblIDNO.InnerText = drwFile.IDNumber
                lblSex.InnerText = If(drwFile.IsMale, "مرد", "زن")
                lblRequestStatus.InnerText = strState

                If drwFile.State = 5 Then

                    dtblFile = tadpFile.GetData(1, drwFile.FK_OriginalFileID, "")
                    Dim drwOrginalFile As BusinessObject.dstFile.spr_File_SelectRow = dtblFile.Rows(0)


                    If lblCustomerNO.InnerText <> drwOrginalFile.CustomerNo Then
                        lblCustomerNO.Style.Value = "color: red;"

                    End If

                    If lblName.InnerText <> drwOrginalFile.FName Then
                        lblName.Style.Value = "color: red;"
                    End If

                    If lblLName.InnerText <> drwOrginalFile.LName Then
                        lblLName.Style.Value = "color: red;"
                    End If

                    If lblAddress.InnerText <> drwOrginalFile.Address Then
                        lblAddress.Style.Value = "color: red;"
                    End If

                    If lblMobile.InnerText <> drwOrginalFile.MobileNo Then

                        lblMobile.Style.Value = "color: red;"
                    End If

                    If lblFatherName.InnerText <> drwOrginalFile.FatherName Then

                        lblFatherName.Style.Value = "color: red;"
                    End If

                    If lblNationalID.InnerText <> drwOrginalFile.NationalID Then
                        lblNationalID.Style.Value = "color: red;"
                    End If

                    If lblIDNO.InnerText <> drwOrginalFile.IDNumber Then
                        lblIDNO.Style.Value = "color: red;"
                    End If

                    If lblWorkTel.InnerText <> drwOrginalFile.TelephoneWork Then

                        lblWorkTel.Style.Value = "color: red;"
                    End If

                    If lblHomeTel.InnerText <> drwOrginalFile.TelephoneHome Then
                        lblHomeTel.Style.Value = "color: red;"

                    End If

                    If lblSex.InnerText <> If(drwOrginalFile.IsMale, "مرد", "زن") Then
                        lblSex.Style.Value = "color: red;"
                    End If

                ElseIf drwFile.State = 8 Then

                    lblCustomerNO.Style.Value = "color: red;"
                    lblName.Style.Value = "color: red;"
                    lblLName.Style.Value = "color: red;"
                    lblAddress.Style.Value = "color: red;"
                    lblMobile.Style.Value = "color: red;"
                    lblFatherName.Style.Value = "color: red;"
                    lblNationalID.Style.Value = "color: red;"
                    lblIDNO.Style.Value = "color: red;"
                    lblWorkTel.Style.Value = "color: red;"  
                    lblHomeTel.Style.Value = "color: red;"
                    lblSex.Style.Value = "color: red;"



                End If

            End With


        End If


    End Sub

  


 
    Private Sub Bootstrap_Panel1_Panel_Confirm_Click(sender As Object, e As System.EventArgs) Handles Bootstrap_Panel1.Panel_ConfirmRequest_Click

        Dim intState As Integer = CInt(Session("State"))
        Dim intFileRequestID As Integer = CInt(Session("intFileRequestID"))

        Dim dtblUserLogin As BusinessObject.dstUser.spr_User_Login_SelectDataTable = CType(Session("dtblUserLogin"), BusinessObject.dstUser.spr_User_Login_SelectDataTable)
        Dim drwUserLogin As BusinessObject.dstUser.spr_User_Login_SelectRow = dtblUserLogin.Rows(0)

        Dim tadpFile As New BusinessObject.dstFileTableAdapters.spr_File_SelectTableAdapter
        Dim dtblFile As BusinessObject.dstFile.spr_File_SelectDataTable = Nothing

        dtblFile = tadpFile.GetData(1, intFileRequestID, "")


        Dim qryFile As New BusinessObject.dstFileTableAdapters.QueriesTableAdapter

        Try


            Select Case intState


                Case 2


                    qryFile.spr_FileState_Update(intFileRequestID, drwUserLogin.ID, 3, Nothing)
                    qryFile.spr_File_UpdateOrginalFile(CInt(Session("FileID")))
                    qryFile.spr_File_Delete(CInt(Session("FileID")))


                Case 5


                    ''update orginal file
                    qryFile.spr_File_Update(CInt(Session("FileID")), lblCustomerNO.InnerText, lblName.InnerText, lblLName.InnerText, lblFatherName.InnerText, lblMobile.InnerText, lblNationalID.InnerText, lblIDNO.InnerText, lblEmail.InnerText, lblAddress.InnerText, lblHomeTel.InnerText, lblWorkTel.InnerText, dtblFile.First.IsMale, drwUserLogin.ID, 1)
                    qryFile.spr_FileState_Update(intFileRequestID, drwUserLogin.ID, 6, CInt(Session("FileID")))



                Case 8


                    Dim intFile As Integer = qryFile.spr_File_Insert(lblCustomerNO.InnerText, lblName.InnerText, lblLName.InnerText, lblFatherName.InnerText, lblMobile.InnerText, lblNationalID.InnerText, lblIDNO.InnerText, lblEmail.InnerText, lblAddress.InnerText, lblHomeTel.InnerText, lblWorkTel.InnerText, dtblFile.First.IsMale, drwUserLogin.ID, 1, Nothing, Nothing)
                    qryFile.spr_FileState_Update(intFileRequestID, drwUserLogin.ID, 9, intFile)




            End Select



        Catch ex As Exception

            Response.Redirect("FileRequestManagement.aspx?Edit=NO")
            Return


        End Try

        Response.Redirect("FileRequestManagement.aspx?Edit=OK")


    End Sub

    Private Sub Bootstrap_Panel1_Panel_Reject_Click(sender As Object, e As System.EventArgs) Handles Bootstrap_Panel1.Panel_Reject_Click

        Dim intState As Integer = CInt(Session("State"))
        Dim intFileRequestID As Integer = CInt(Session("intFileRequestID"))

        Dim dtblUserLogin As BusinessObject.dstUser.spr_User_Login_SelectDataTable = CType(Session("dtblUserLogin"), BusinessObject.dstUser.spr_User_Login_SelectDataTable)
        Dim drwUserLogin As BusinessObject.dstUser.spr_User_Login_SelectRow = dtblUserLogin.Rows(0)



        Dim qryFile As New BusinessObject.dstFileTableAdapters.QueriesTableAdapter

        Try


            Select Case intState


                Case 2


                    qryFile.spr_FileState_Update(intFileRequestID, drwUserLogin.ID, 4, CInt(Session("FileID")))



                Case 5


                    qryFile.spr_FileState_Update(intFileRequestID, drwUserLogin.ID, 7, CInt(Session("FileID")))



                Case 8

                    qryFile.spr_FileState_Update(intFileRequestID, drwUserLogin.ID, 10, Nothing)




            End Select

        Catch ex As Exception


            Response.Redirect("FileRequestManagement.aspx?Edit=NO")
            Return


        End Try

        Response.Redirect("FileRequestManagement.aspx?Edit=Reject")


    End Sub

    Private Sub Bootstrap_Panel1_Panel_Up_Click(sender As Object, e As System.EventArgs) Handles Bootstrap_Panel1.Panel_Up_Click
        Response.Redirect("FileRequestManagement.aspx")
    End Sub
End Class