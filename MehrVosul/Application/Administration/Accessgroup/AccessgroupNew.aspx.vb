Public Class AccessgroupNew
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

            Dim dtblUserLogin As BusinessObject.dstUser.spr_User_Login_SelectDataTable = CType(Session("dtblUserLogin"), BusinessObject.dstUser.spr_User_Login_SelectDataTable)
            Dim drwUserLogin As BusinessObject.dstUser.spr_User_Login_SelectRow = dtblUserLogin.Rows(0)



            Dim tadpMenuLeafList As New BusinessObject.dstMenuTableAdapters.spr_Menu_Leaf_List_SelectTableAdapter
            Dim dtblMenuLeafList As BusinessObject.dstMenu.spr_Menu_Leaf_List_SelectDataTable = Nothing
            dtblMenuLeafList = tadpMenuLeafList.GetData()

            Dim strchklstMenuLeaves As String = ""

            For Each drwMeneLeafList As BusinessObject.dstMenu.spr_Menu_Leaf_List_SelectRow In dtblMenuLeafList.Rows
                strchklstMenuLeaves &= "<div class='checkbox'> <label> <input type='checkbox' value='" & drwMeneLeafList.ID & "' name='chklstMenu" & drwMeneLeafList.ID & "'><i class='fa " & drwMeneLeafList.IconText & " fa-1x'></i> " & drwMeneLeafList.Menutitlepath & "</label></div>"
            Next drwMeneLeafList

            divchklstMenuItems.InnerHtml = strchklstMenuLeaves

        End If


    End Sub



    Private Sub Bootstrap_Panel1_Panel_Cancel_Click(sender As Object, e As System.EventArgs) Handles Bootstrap_Panel1.Panel_Cancel_Click
        Response.Redirect("AccessgroupManagement.aspx")
        Return
    End Sub


    Private Sub Bootstrap_Panel1_Panel_Save_Click(sender As Object, e As System.EventArgs) Handles Bootstrap_Panel1.Panel_Save_Click

        Dim dtblUserLogin As BusinessObject.dstUser.spr_User_Login_SelectDataTable = CType(Session("dtblUserLogin"), BusinessObject.dstUser.spr_User_Login_SelectDataTable)
        Dim drwUserLogin As BusinessObject.dstUser.spr_User_Login_SelectRow = dtblUserLogin.Rows(0)


        Dim strAccessgrouptitle As String = txtAccessgrouptitle.Text.Trim

        

        Try

            Dim qryAccessgroup As New BusinessObject.dstAccessgroupTableAdapters.QueriesTableAdapter
            Dim intAccessgroupID As Integer = qryAccessgroup.spr_Accessgroup_Insert(strAccessgrouptitle, Date.Now, drwUserLogin.ID)



            For i As Integer = 0 To Request.Form.Keys.Count - 1
                If Request.Form.Keys(i).StartsWith("chklstMenu") = True Then
                    Dim qryAccessgroupMenu As New BusinessObject.dstAccessgroupMenuTableAdapters.QueriesTableAdapter
                    qryAccessgroupMenu.spr_AccessgroupMenu_Insert(intAccessgroupID, CInt(Request.Form(i)))
                End If
               
            Next i

        Catch ex As Exception
            Response.Redirect("AccessgroupManagement.aspx?Save=NO")
            Return
        End Try

        Response.Redirect("AccessgroupManagement.aspx?Save=OK")







    End Sub
End Class