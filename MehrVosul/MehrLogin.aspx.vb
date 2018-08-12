Public Class MehrLogin
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        btnSignIn.Attributes.Add("onclick", "return AccountValidate();")
        divMessage1.Style("display") = "none"
        divMessage.Style("display") = "none"
        'Test
        'Test
    End Sub

    Protected Sub btnSignin_Click(sender As Object, e As EventArgs) Handles btnSignIn.ServerClick
        Dim strUsername As String = txtUsername.Text.Trim
        Dim strPassword As String = txtPassword.Text.Trim
        Dim tadpUserMehrLogin As New BusinessObject.dstUserTableAdapters.spr_User_Login_SelectTableAdapter
        Dim dtblUserMehrLogin As BusinessObject.dstUser.spr_User_Login_SelectDataTable = Nothing
        dtblUserMehrLogin = tadpUserMehrLogin.GetData(strUsername, strPassword)
        If dtblUserMehrLogin.Rows.Count = 0 Then
            divMessage.Style("display") = "inline"
            Return
        End If


        'If dtblUserMehrLogin.First.IsDataAdmin = False AndAlso dtblUserMehrLogin.First.IsItemAdmin = False Then
        '    Dim strIP As String = System.Web.HttpContext.Current.Request.UserHostAddress

        '    Dim tadpBranch As New BusinessObject.dstBranchTableAdapters.spr_Branch_SelectTableAdapter
        '    Dim dtblBranch As BusinessObject.dstBranch.spr_Branch_SelectDataTable = Nothing

        '    dtblBranch = tadpBranch.GetData(dtblUserMehrLogin.First.FK_BrnachID)
        '    If dtblBranch.Rows.Count > 0 Then

        '        If dtblBranch.First.IsBranchIPNull = True Then

        '            Dim qryBranch As New BusinessObject.dstBranchTableAdapters.QueriesTableAdapter
        '            qryBranch.spr_BranchIP_Update(dtblBranch.First.ID, strIP)

        '        Else

        '            If dtblBranch.First.BranchIP <> strIP Then

        '                divMessage1.Style("display") = "inline"
        '                Return

        '            End If

        '        End If
        '    End If
        'End If

        Session("dtblUserLogin") = CObj(dtblUserMehrLogin)

        Dim qryUserMehrLogin As New BusinessObject.dstLoginLogTableAdapters.QueriesTableAdapter
        qryUserMehrLogin.spr_SystemLoginLog_Insert(dtblUserMehrLogin.First.ID)

        Web.Security.FormsAuthentication.SetAuthCookie("TCS_Username", False)

        Response.Redirect("Application/StartPage.aspx?BankName=MehrIran")
        Return

    End Sub




    ''Protected Sub lnkbtnUserManula_Click(sender As Object, e As EventArgs) Handles lnkbtnUserManula.Click

    ''    If IO.File.Exists(Server.MapPath("~") & "\UserManual\MehrUserManual.pdf") = True Then
    ''        Response.Clear()
    ''        Response.ContentType = "Application/zip"
    ''        Response.AppendHeader("Content-Disposition", "attachment; filename=MehrUserManual.pdf")
    ''        Response.WriteFile(Server.MapPath("~") & "\UserManual\MehrUserManual.pdf")
    ''        Response.End()
    ''    End If

    ''End Sub
End Class