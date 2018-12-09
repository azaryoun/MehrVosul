Imports System.IO
Public Class LoanTypeMagic
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

    Private Sub Bootstrap_Panel1_Panel_Cancel_Click(sender As Object, e As EventArgs) Handles Bootstrap_Panel1.Panel_Cancel_Click

        Response.Redirect("LoanTypeManagement.aspx")
        Return

    End Sub

    Private Sub Bootstrap_Panel1_Panel_Save_Click(sender As Object, e As EventArgs) Handles Bootstrap_Panel1.Panel_Save_Click

        If fleUploadFile.PostedFile.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet" Or fleUploadFile.PostedFile.ContentType = "application/vnd.ms-excel" Then '

            Dim strPath As String = Server.MapPath("") & "\Temp\" & fleUploadFile.PostedFile.FileName.Substring(fleUploadFile.PostedFile.FileName.LastIndexOf("\") + 1)
            fleUploadFile.PostedFile.SaveAs(strPath)



            Dim dtExcelName As New DataTable

            If fleUploadFile.PostedFile.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet" Then '"application/vnd.ms-excel" Then


                dtExcelName = ReadExcelNew(strPath, "")
            Else

                dtExcelName = ReadExcelNew(strPath, "")
                dtExcelName = ReadExcel2003(strPath, "")
            End If

            Try
                Dim qryBlacklist As New BusinessObject.dstBlackListTableAdapters.QueriesTableAdapter

                If dtExcelName.Rows.Count > 0 Then

                    For i As Integer = 1 To dtExcelName.Rows.Count - 1

                        Dim strLCNO As String = dtExcelName.Rows(i)(0).ToString()

                        qryBlacklist.spr_BlackList_Insert(strLCNO)

                    Next

                End If

            Catch ex As Exception

                Bootstrap_Panel1.ShowMessage(ex.Message, True)
            End Try

        Else

            Bootstrap_Panel1.ShowMessage("فرمت فایل بایستی اکسل باشد", True)
            Return
        End If


        ''Response.Redirect("LoanTypeManagement.aspx?Save=OK")
        Bootstrap_Panel1.ShowMessage("لیست سیاه با موفقیت بارگزاری شد", False)


    End Sub

    Protected Sub lnkbtnSample_Click(sender As Object, e As EventArgs) Handles lnkbtnSample.Click

        Dim dtblUserLogin As BusinessObject.dstUser.spr_User_Login_SelectDataTable = CType(Session("dtblUserLogin"), BusinessObject.dstUser.spr_User_Login_SelectDataTable)
        Dim drwUserLogin As BusinessObject.dstUser.spr_User_Login_SelectRow = dtblUserLogin.Rows(0)


        Dim tadpBlackList As New BusinessObject.dstBlackListTableAdapters.spr_BlackListAll_SelectTableAdapter
        Dim dtblBlackList As BusinessObject.dstBlackList.spr_BlackListAll_SelectDataTable = Nothing

        dtblBlackList = tadpBlackList.GetData()

        Dim strPath As String = Server.MapPath("") & "\Temp\" & drwUserLogin.ID & "\"
        Dim FileName As String = "BlackList-" & drwUserLogin.ID.ToString()

        If dtblBlackList.Rows.Count > 0 Then
            If Not System.IO.Directory.Exists(strPath) Then
                System.IO.Directory.CreateDirectory(strPath)
            End If

            Dim clsCSVWriter As New clsCSVWriter
            Using strWriter As StreamWriter = New StreamWriter(strPath & FileName)
                clsCSVWriter.WriteDataTable(dtblBlackList, FileName, strWriter, True)
            End Using


            Response.Clear()
            Response.ContentType = "Application/xls"
            Response.AppendHeader("Content-Disposition", "attachment; filename=BlackList.xlsx")
            Response.WriteFile(Server.MapPath("~") & "Application\Vosul\LoanType\BlackList.xlsx")
            Response.End()

        End If

    End Sub
End Class