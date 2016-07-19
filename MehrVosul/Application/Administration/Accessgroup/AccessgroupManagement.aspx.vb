﻿Public Class AccessgroupManagement
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Page.Response.Cache.SetCacheability(HttpCacheability.NoCache)

        Bootstrap_Panel1.CanNew = True
        Bootstrap_Panel1.CanSave = False
        Bootstrap_Panel1.CanDelete = True
        Bootstrap_Panel1.CanSearch = True
        Bootstrap_Panel1.CanCancel = False
        Bootstrap_Panel1.CanUp = False
        Bootstrap_Panel1.CanWizard = False
        Bootstrap_Panel1.CanConfirmRequest = False
        Bootstrap_Panel1.CanReject = False
        Bootstrap_Panel1.CanDisplay = False
        Bootstrap_Panel1.CanExcel = False
        Bootstrap_Panel1.Enable_Delete_Client_Validate = True
        Bootstrap_Panel1.Enable_Search_Client_Validate = True

        lblInnerPageTitle.Text = "فهرست گروه های دسترسی، لطفا طبق ضوابط عمل نمایید"


        If Page.IsPostBack = False Then

            If Request.QueryString("Save") IsNot Nothing AndAlso Request.QueryString("Save") = "OK" Then
                Bootstrap_Panel1.ShowMessage("گروه دسترسی با موفقیت تعریف شد", False)
            ElseIf Request.QueryString("Edit") IsNot Nothing AndAlso Request.QueryString("Edit") = "OK" Then
                Bootstrap_Panel1.ShowMessage("گروه دسترسی با موفقیت ویرایش شد", False)
            ElseIf Request.QueryString("Save") IsNot Nothing AndAlso Request.QueryString("Save") = "NO" Then
                Bootstrap_Panel1.ShowMessage("فرآیند تعریف گروه دسترسی با شکست مواجه شده است", True)
            ElseIf Request.QueryString("Edit") IsNot Nothing AndAlso Request.QueryString("Edit") = "NO" Then
                Bootstrap_Panel1.ShowMessage("فرآیند ویرایش گروه دسترسی با شکست مواجه شده است", True)
            Else
                Bootstrap_Panel1.ClearMessage()
            End If


        End If

        If hdnAction.Value.StartsWith("E") = True Then
            Dim intAccessgroupID As Integer = CInt(hdnAction.Value.Split(";")(1))
            Session("intAccessgroupID") = CObj(intAccessgroupID)
            Response.Redirect("AccessgroupEdit.aspx")
        End If


    End Sub

#Region "Ajax"


    <System.Web.Services.WebMethod()> Public Shared Function DeleteOperation_Server(theKeys() As Integer) As String



        For i As Integer = 0 To theKeys.Length - 1
            Dim intPKey As Integer = CInt(theKeys(i))

            Dim qryAccessgroup As New BusinessObject.dstAccessgroupTableAdapters.QueriesTableAdapter
            Try

                qryAccessgroup.spr_Accessgroup_Delete(intPKey)


            Catch ex As Exception
                Return ex.Message
            End Try

        Next i

        Return ""



    End Function


    <System.Web.Services.WebMethod()> Public Shared Function GetPageRecords(intPageNo As Integer, strFilter As String) As String
        Try
            Dim intAction As Integer = 1
            If strFilter IsNot Nothing Then
                intAction = 2
            End If
            Dim intToIndex As Integer = intPageNo * mdlGeneral.cnst_RowsCountInPage
            Dim intFromIndex As Integer = (intToIndex - mdlGeneral.cnst_RowsCountInPage) + 1
            Dim tadpAccessgroupManagement As New BusinessObject.dstAccessgroupTableAdapters.spr_Accessgroup_Management_SelectTableAdapter
            Dim dtblAccessgroupManagement As BusinessObject.dstAccessgroup.spr_Accessgroup_Management_SelectDataTable = Nothing
            dtblAccessgroupManagement = tadpAccessgroupManagement.GetData(intAction, intFromIndex, intToIndex, strFilter)

            Dim strResult As String = ""
            Dim intColumnCount As Integer = dtblAccessgroupManagement.Columns.Count

            If intAction = 1 Then
                For Each drwAccessgroupManagement As BusinessObject.dstAccessgroup.spr_Accessgroup_Management_SelectRow In dtblAccessgroupManagement.Rows


                    strResult &= ";n;;@;" & CStr(drwAccessgroupManagement.Item(0))
                    For i As Integer = 1 To intColumnCount - 1
                        strResult &= ";@;" & CStr(drwAccessgroupManagement.Item(i))
                    Next


                Next drwAccessgroupManagement
            Else
                For Each drwAccessgroupManagement As BusinessObject.dstAccessgroup.spr_Accessgroup_Management_SelectRow In dtblAccessgroupManagement.Rows

                    strResult &= ";n;;@;" & CStr(drwAccessgroupManagement.Item(0))
                    strResult &= ";@;" & CStr(drwAccessgroupManagement.Item(1))

                    For i As Integer = 2 To intColumnCount - 1
                        strResult &= ";@;" & CStr(drwAccessgroupManagement.Item(i)).ToLower.Replace(strFilter.ToLower, "<b><font color='#0F17FF'>" & strFilter & "</font></b>")
                    Next
                Next drwAccessgroupManagement
            End If

            Return strResult

        Catch ex As Exception
            Return "E"
        End Try


    End Function

    <System.Web.Services.WebMethod()> Public Shared Function GetPageCount(strFilter As String) As Integer()

        Dim intAction As Integer = 1
        If strFilter IsNot Nothing Then
            intAction = 2
        End If
        Dim tadpAccessgroupCount As New BusinessObject.dstAccessgroupTableAdapters.spr_Accessgroup_Count_SelectTableAdapter
        Dim dtblAccessgroupCount As BusinessObject.dstAccessgroup.spr_Accessgroup_Count_SelectDataTable = Nothing
        dtblAccessgroupCount = tadpAccessgroupCount.GetData(intAction, strFilter)
        Dim drwAccessgroupCount As BusinessObject.dstAccessgroup.spr_Accessgroup_Count_SelectRow = dtblAccessgroupCount.Rows(0)
        Dim intPageCount As Integer = Math.Ceiling(drwAccessgroupCount.AccessgroupCount / mdlGeneral.cnst_RowsCountInPage)
        Dim arrResult(1) As Integer
        arrResult(0) = drwAccessgroupCount.AccessgroupCount
        arrResult(1) = intPageCount
        Return arrResult

    End Function

#End Region

    Private Sub Bootstrap_Panel1_Panel_New_Click(sender As Object, e As System.EventArgs) Handles Bootstrap_Panel1.Panel_New_Click
        Response.Redirect("AccessgroupNew.aspx")
        Return
    End Sub
End Class