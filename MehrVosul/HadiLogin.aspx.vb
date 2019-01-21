Public Class HadiLogin
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        btnSignIn.Attributes.Add("onclick", "return AccountValidate();")
        divMessage1.Style("display") = "none"
        divMessage.Style("display") = "none"
    End Sub
    Protected Sub btnSignin_Click(sender As Object, e As EventArgs) Handles btnSignIn.ServerClick

        Dim strUsername As String = txtUsername.Text.Trim
        Dim strPassword As String = txtPassword.Text.Trim
        Dim tadpUserLogin As New BusinessObject.dstUserTableAdapters.spr_User_Login_SelectTableAdapter
        Dim dtblUserLogin As BusinessObject.dstUser.spr_User_Login_SelectDataTable = Nothing
        dtblUserLogin = tadpUserLogin.GetData(strUsername, strPassword)
        If dtblUserLogin.Rows.Count = 0 Then
            divMessage.Style("display") = "inline"
            Return
        End If


        'If dtblUserLogin.First.IsDataAdmin = False AndAlso dtblUserLogin.First.IsItemAdmin = False Then
        '    Dim strIP As String = System.Web.HttpContext.Current.Request.UserHostAddress

        '    Dim tadpBranch As New BusinessObject.dstBranchTableAdapters.spr_Branch_SelectTableAdapter
        '    Dim dtblBranch As BusinessObject.dstBranch.spr_Branch_SelectDataTable = Nothing

        '    dtblBranch = tadpBranch.GetData(dtblUserLogin.First.FK_BrnachID)
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

        Session("dtblUserLogin") = CObj(dtblUserLogin)

        Dim qryUserLogin As New BusinessObject.dstLoginLogTableAdapters.QueriesTableAdapter
        qryUserLogin.spr_SystemLoginLog_Insert(dtblUserLogin.First.ID)

        Web.Security.FormsAuthentication.SetAuthCookie("TCS_Username", False)

        Response.Redirect("Application/HadiStartPage.aspx")
        Return

    End Sub

End Class