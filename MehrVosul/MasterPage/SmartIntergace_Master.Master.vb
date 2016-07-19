Public Class SmartIntergace_Master
    Inherits System.Web.UI.MasterPage

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Response.Cache.SetCacheability(HttpCacheability.NoCache)


        If Session("dtblUserLogin") Is Nothing Then
            Call lbtnSignOut_Click(sender, e)
            Return
        End If

        '   Dim DraftType As String = Request.QueryString("type")
        Dim strClassName As String = Request.ServerVariables("SCRIPT_NAME")
        Dim intPos As Integer = strClassName.LastIndexOf("/")
        strClassName = strClassName.Substring(intPos + 1)
        strClassName = strClassName.Substring(0, strClassName.Length - 5)


        Dim dtblUserLogin As BusinessObject.dstUser.spr_User_Login_SelectDataTable = CType(Session("dtblUserLogin"), BusinessObject.dstUser.spr_User_Login_SelectDataTable)
        Dim drwUserLogin As BusinessObject.dstUser.spr_User_Login_SelectRow = dtblUserLogin.Rows(0)
        Dim strPageTitle As String = "نرم افزار جامع وصول مطالبات بانک مهر (وصال)"


        Dim intActiveMenuID As Integer = -1
        Dim strPageDesc As String = drwUserLogin.FName & " , خوش آمدید، منتظر شما بودیم"
        Dim strPageCaption As String = strPageTitle

        If strClassName.ToLower <> "startpage" Then

            If drwUserLogin.IsItemAdmin = True Then
                Dim tadpPageTitle As New BusinessObject.dstMenuTableAdapters.spr_Menu_PageTitle_SelectTableAdapter
                Dim dtblPageTitle As BusinessObject.dstMenu.spr_Menu_PageTitle_SelectDataTable = Nothing
                dtblPageTitle = tadpPageTitle.GetData(strClassName)
                If dtblPageTitle.Rows.Count > 0 Then
                    Dim drwPageTitle As BusinessObject.dstMenu.spr_Menu_PageTitle_SelectRow = dtblPageTitle.Rows(0)
                    'If Not DraftType Is Nothing Then

                    '    Select Case DraftType

                    '        Case 1

                    '            strPageTitle = "متن پیامک"
                    '            intActiveMenuID = 45
                    '            strPageCaption = "متن پیامک"

                    '        Case 2
                    '            strPageTitle = "دعوت نامه"
                    '            intActiveMenuID = 46
                    '            strPageCaption = "دعوت نامه"

                    '        Case 3
                    '            strPageTitle = "اخطاریه"
                    '            intActiveMenuID = 47
                    '            strPageCaption = "اخطاریه"
                    '        Case 4
                    '            strPageTitle = "اظهارنامه"
                    '            intActiveMenuID = 48
                    '            strPageCaption = "اظهارنامه"

                    '    End Select


                    'Else

                    strPageTitle = drwPageTitle.PageTitle
                    intActiveMenuID = drwPageTitle.ID
                    strPageCaption = drwPageTitle.MenuTitle




                End If

            Else

                Dim tadpCheckAccess As New BusinessObject.dstMenuTableAdapters.spr_Menu_CheckUserAccess_SelectTableAdapter
                Dim dtblCheckAccess As BusinessObject.dstMenu.spr_Menu_CheckUserAccess_SelectDataTable = Nothing
                dtblCheckAccess = tadpCheckAccess.GetData(drwUserLogin.ID, strClassName)
                If dtblCheckAccess.Rows.Count = 0 Then
                    Response.Redirect(Request.ServerVariables("HTTP_REFERER"))
                    Return
                End If

                Dim drwCheckAccess As BusinessObject.dstMenu.spr_Menu_CheckUserAccess_SelectRow = dtblCheckAccess.Rows(0)
                strPageTitle = drwCheckAccess.PageTitle
                intActiveMenuID = drwCheckAccess.ID
                strPageCaption = drwCheckAccess.MenuTitle
            End If
            strPageDesc = strPageTitle
        End If


        h5PageDesc.InnerText = strPageDesc
        h2PageCaption.InnerText = strPageCaption
        Me.Page.Title = strPageTitle
        'lblProjectTitle.Text = "بانک مهر "
        'lblProjectTitle.ToolTip = "نرم افزار جامع وصول مطالبات بانک مهر (وصال)"
        lblToday.Text = mdlGeneral.GetPersianToday()

        If drwUserLogin.IsUserPhotoNull = False Then
            imgUser.ImageUrl = "data:image/png;base64," + Convert.ToBase64String(drwUserLogin.UserPhoto)
        End If
        imgUser.ToolTip = drwUserLogin.FName & " " & drwUserLogin.LName & " (" & drwUserLogin.Username & ")"
        lblUserInfo.Text = drwUserLogin.BranchName
        Bootstrap_Menu1.BuildMenu(drwUserLogin.IsItemAdmin, drwUserLogin.ID, intActiveMenuID)




    End Sub

 

    Protected Sub lbtnSignOut_Click(sender As Object, e As EventArgs) Handles lbtnSignOut.Click
        Session("dtblUserLogin") = Nothing
        Session.Abandon()
        Session.Clear()
        Web.Security.FormsAuthentication.SignOut()
        Web.Security.FormsAuthentication.RedirectToLoginPage()
    End Sub
End Class