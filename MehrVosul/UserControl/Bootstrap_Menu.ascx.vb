Public Class Bootstrap_Menu
    Inherits System.Web.UI.UserControl
    Public strMenuText As String = ""


    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

    End Sub

    Public Sub BuildMenu(ForAdmin As Boolean, Optional intUserID As Integer = 0, Optional intActiveMenuID As Integer = 0)

        Dim intAction As Integer = 2
        If ForAdmin = True Then
            intAction = 1
        End If


        Dim tadpParentMenu As New BusinessObject.dstMenuTableAdapters.spr_Menu_Parent_SelectTableAdapter
        Dim dtblParentMenu As BusinessObject.dstMenu.spr_Menu_Parent_SelectDataTable = Nothing
        dtblParentMenu = tadpParentMenu.GetData(intAction, intUserID)

        For Each drwParentMenu As BusinessObject.dstMenu.spr_Menu_Parent_SelectRow In dtblParentMenu.Rows
            strMenuText &= "<li>"
            Dim strURL As String = "#"
            If drwParentMenu.IsURLNull = False Then
                strURL = drwParentMenu.URL
            End If
            Dim strActiveMenu As String = ""
            If intActiveMenuID = drwParentMenu.ID Then
                strActiveMenu = "class='active-menu'"
            End If
            Dim strIconTextSize As String = ""
            If drwParentMenu.IsIconTextNull = False Then
                strIconTextSize = "class='fa " & drwParentMenu.IconText & " fa-" & drwParentMenu.IconSize & "x'"
            End If

            strMenuText &= "<a  href='" & strURL & "' " & strActiveMenu & " ><i " & strIconTextSize & " ></i> " & drwParentMenu.MenuTitle
            strActiveMenu = ""
            strURL = "#"
            strIconTextSize = ""

            Dim tadpChildMenu_Level2 As New BusinessObject.dstMenuTableAdapters.spr_Menu_ChildSelectTableAdapter
            Dim dtblChildMenu_Level2 As BusinessObject.dstMenu.spr_Menu_ChildSelectDataTable = Nothing
            dtblChildMenu_Level2 = tadpChildMenu_Level2.GetData(intAction, intUserID, drwParentMenu.ID)

            If dtblChildMenu_Level2.Rows.Count = 0 Then
                strMenuText &= "</a></li>"
            Else
                strMenuText &= "<span class='fa arrow'></span></a>"
                Dim strMenuTextSecondLevel As String = ""
                Dim strMenuTextSecondLevelHead As String = "<ul class='nav nav-second-level'>"

            
                For Each drwChildMenu_Level2 As BusinessObject.dstMenu.spr_Menu_ChildSelectRow In dtblChildMenu_Level2.Rows
                    strMenuTextSecondLevel &= "<li>"
                    If intActiveMenuID = drwChildMenu_Level2.ID Then
                        strMenuTextSecondLevelHead = "<ul class='nav nav-second-level collapse in'>"
                        strActiveMenu = "class='active-menu'"
                    End If
                    If drwChildMenu_Level2.IsURLNull = False Then
                        strURL = drwChildMenu_Level2.URL
                    End If
                    If drwChildMenu_Level2.IsIconTextNull = False Then
                        strIconTextSize = "class='fa " & drwChildMenu_Level2.IconText & " fa-" & drwChildMenu_Level2.IconSize & "x'"
                    End If


                    strMenuTextSecondLevel &= "<a  href='" & strURL & "' " & strActiveMenu & " ><i " & strIconTextSize & " ></i> " & drwChildMenu_Level2.MenuTitle
                    strActiveMenu = ""
                    strURL = "#"
                    strIconTextSize = ""

                    Dim tadpChildMenu_Level3 As New BusinessObject.dstMenuTableAdapters.spr_Menu_ChildSelectTableAdapter
                    Dim dtblChildMenu_Level3 As BusinessObject.dstMenu.spr_Menu_ChildSelectDataTable = Nothing
                    dtblChildMenu_Level3 = tadpChildMenu_Level3.GetData(intAction, intUserID, drwChildMenu_Level2.ID)
                    If dtblChildMenu_Level3.Rows.Count = 0 Then
                        strMenuTextSecondLevel &= "</a></li>"
                    Else
                        strMenuTextSecondLevel &= "<span class='fa arrow'></span></a>"
                 
                        Dim strMenuTextThirdLevel As String = ""
                        Dim strMenuTextThirdLevelHead As String = "<ul class='nav nav-third-level'>"





                        For Each drwChildMenu_Level3 As BusinessObject.dstMenu.spr_Menu_ChildSelectRow In dtblChildMenu_Level3.Rows
                            strMenuTextThirdLevel &= "<li>"
                            If intActiveMenuID = drwChildMenu_Level3.ID Then
                                strActiveMenu = "class='active-menu'"
                                strMenuTextSecondLevelHead = "<ul class='nav nav-second-level collapse in'>"
                                strMenuTextThirdLevelHead = "<ul class='nav nav-third-level collapse in'>"
                            End If
                            If drwChildMenu_Level3.IsURLNull = False Then
                                strURL = drwChildMenu_Level3.URL
                            End If
                            If drwChildMenu_Level3.IsIconTextNull = False Then
                                strIconTextSize = "class='fa " & drwChildMenu_Level3.IconText & " fa-" & drwChildMenu_Level3.IconSize & "x'"
                            End If

                            strMenuTextThirdLevel &= "<a  href='" & strURL & "' " & strActiveMenu & " ><i " & strIconTextSize & " ></i> " & drwChildMenu_Level3.MenuTitle & "</a></li>"
                            strActiveMenu = ""
                            strURL = "#"
                            strIconTextSize = ""

                        Next drwChildMenu_Level3

                        strMenuTextThirdLevel &= "</ul></li>"

                        strMenuTextSecondLevel &= strMenuTextThirdLevelHead & strMenuTextThirdLevel

                    End If


                Next drwChildMenu_Level2

                strMenuText &= strMenuTextSecondLevelHead & strMenuTextSecondLevel

                strMenuText &= "</ul></li>"


            End If


        Next drwParentMenu



    End Sub


    Private Sub BuildChildMenu(ForAdmin As Boolean, intParentID As Integer, Optional intUserID As Integer = 0)

        Dim intAction As Integer = 2
        If ForAdmin = True Then
            intAction = 1
        End If


        Dim tadpParentMenu As New BusinessObject.dstMenuTableAdapters.spr_Menu_Parent_SelectTableAdapter
        Dim dtblParentMenu As BusinessObject.dstMenu.spr_Menu_Parent_SelectDataTable = Nothing
        dtblParentMenu = tadpParentMenu.GetData(intAction, intUserID)

        For Each drwParentMenu As BusinessObject.dstMenu.spr_Menu_Parent_SelectRow In dtblParentMenu.Rows
            strMenuText &= "<li>"
            Dim strURL As String = "#"
            If drwParentMenu.IsURLNull = False Then
                strURL = drwParentMenu.URL
            End If
            strMenuText &= "<a  href='" & strURL & "'><i class='fa " & drwParentMenu.IconText & " fa-" & drwParentMenu.IconSize & "x'></i> " & drwParentMenu.MenuTitle & "</a>"
            strMenuText &= "</li>"


        Next drwParentMenu



    End Sub


End Class