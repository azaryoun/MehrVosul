Public Class AccessgroupEdit
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



            If Session("intAccessgroupID") Is Nothing Then
                Response.Redirect("AccessgroupManagement.aspx")
                Return
            End If

            Dim intAccessgroupID As Integer = CInt(Session("intAccessgroupID"))


            Dim tadpAccessgroup As New BusinessObject.dstAccessgroupTableAdapters.spr_Accessgroup_SelectTableAdapter
            Dim dtblAccessgroup As BusinessObject.dstAccessgroup.spr_Accessgroup_SelectDataTable = Nothing
            dtblAccessgroup = tadpAccessgroup.GetData(intAccessgroupID)


            If dtblAccessgroup.Rows.Count = 0 Then
                Response.Redirect("AccessgroupManagement.aspx")
                Return
            End If


            Dim drwAccessgroup As BusinessObject.dstAccessgroup.spr_Accessgroup_SelectRow = dtblAccessgroup.Rows(0)
            txtAccessgrouptitle.Text = drwAccessgroup.Desp
            chkVisibility.Checked = drwAccessgroup.Visiblity

            Dim tadpMenuLeafList As New BusinessObject.dstMenuTableAdapters.spr_Menu_Leaf_List_SelectTableAdapter
            Dim dtblMenuLeafList As BusinessObject.dstMenu.spr_Menu_Leaf_List_SelectDataTable = Nothing
            dtblMenuLeafList = tadpMenuLeafList.GetData()

            Dim tadpAccessgroupMenu As New BusinessObject.dstAccessgroupMenuTableAdapters.spr_AccessgroupMenu_AccessGroup_SelectTableAdapter
            Dim dtblAccessgroupMenu As BusinessObject.dstAccessgroupMenu.spr_AccessgroupMenu_AccessGroup_SelectDataTable = Nothing
            dtblAccessgroupMenu = tadpAccessgroupMenu.GetData(intAccessgroupID)

            Dim strchklstMenuLeaves As String = ""

            For Each drwMeneLeafList As BusinessObject.dstMenu.spr_Menu_Leaf_List_SelectRow In dtblMenuLeafList.Rows

                If dtblAccessgroupMenu.Select("FK_MenuID=" & drwMeneLeafList.ID).Length > 0 Then
                    strchklstMenuLeaves &= "<div class='checkbox'> <label> <input type='checkbox' checked='checked' value='" & drwMeneLeafList.ID & "' name='chklstMenu" & drwMeneLeafList.ID & "'><i class='fa " & drwMeneLeafList.IconText & " fa-1x'></i> " & drwMeneLeafList.Menutitlepath & "</label></div>"
                Else
                    strchklstMenuLeaves &= "<div class='checkbox'> <label> <input type='checkbox' value='" & drwMeneLeafList.ID & "' name='chklstMenu" & drwMeneLeafList.ID & "'><i class='fa " & drwMeneLeafList.IconText & " fa-1x'></i> " & drwMeneLeafList.Menutitlepath & "</label></div>"
                End If


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

        Dim intAccessgroupID As Integer = CInt(Session("intAccessgroupID"))


        Dim strAccessgrouptitle As String = txtAccessgrouptitle.Text.Trim
        Dim blnVisibility As Boolean = chkVisibility.Checked


        Try

            Dim qryAccessgroup As New BusinessObject.dstAccessgroupTableAdapters.QueriesTableAdapter
            qryAccessgroup.spr_Accessgroup_Update(intAccessgroupID, strAccessgrouptitle, Date.Now, drwUserLogin.ID, blnVisibility)


            Dim qryAccessgroupMenu As New BusinessObject.dstAccessgroupMenuTableAdapters.QueriesTableAdapter
            qryAccessgroupMenu.spr_AccessgroupMenu_Accessgroup_Delete(intAccessgroupID)


            For i As Integer = 0 To Request.Form.Keys.Count - 1
                If Request.Form.Keys(i).StartsWith("chklstMenu") = True Then
                   qryAccessgroupMenu.spr_AccessgroupMenu_Insert(intAccessgroupID, CInt(Request.Form(i)))
                End If

            Next i

        Catch ex As Exception
            Response.Redirect("AccessgroupManagement.aspx?Edit=NO")
            Return
        End Try

        Response.Redirect("AccessgroupManagement.aspx?Edit=OK")







    End Sub
End Class