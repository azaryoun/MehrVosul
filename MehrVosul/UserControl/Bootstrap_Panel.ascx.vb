Public Class Bootstrap_Panel
    Inherits System.Web.UI.UserControl
    Public Event Panel_New_Click As EventHandler
    Public Event Panel_Save_Click As EventHandler
    Public Event Panel_Cancel_Click As EventHandler
    Public Event Panel_Delete_Click As EventHandler
    Public Event Panel_Search_Click(strSearchText As String)
    Public Event Panel_Up_Click As EventHandler
    Public Event Panel_Wizard_Click As EventHandler
    Public Event Panel_ConfirmRequest_Click As EventHandler
    Public Event Panel_Reject_Click As EventHandler
    Public Event Panel_Display_Click As EventHandler
    Public Event Panel_Excel_Click As EventHandler
    Public Event Panel_PDF_Click As EventHandler
    Private P_blnEnable_New_Client_Validate As Boolean = False
    Private P_blnEnable_Save_Client_Validate As Boolean = False
    Private P_blnEnable_Cancel_Client_Validate As Boolean = False
    Private P_blnEnable_Delete_Client_Validate As Boolean = False
    Private P_blnEnable_Search_Client_Validate As Boolean = False
    Private P_blnEnable_Wizard_Client_Validate As Boolean = False
    Private P_blnEnable_Up_Client_Validate As Boolean = False
    Private P_blnEnable_ConfirmRequest_Client_Validate As Boolean = False
    Private P_blnEnable_RejectRequest_Client_Validate As Boolean = False
    Private P_blnEnable_Display_Client_Validate As Boolean = False
    Private P_blnEnable_Excel_Client_Validate As Boolean = False
    Private P_blnEnable_PDF_Client_Validate As Boolean = False

 
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

    End Sub

    Private Sub btnNew_ServerClick(sender As Object, e As System.EventArgs) Handles btnNew.Click
        RaiseEvent Panel_New_Click(sender, e)
    End Sub
    Private Sub btnSave_ServerClick(sender As Object, e As System.EventArgs) Handles btnSave.Click
        RaiseEvent Panel_Save_Click(sender, e)
    End Sub
    Private Sub btnCancel_ServerClick(sender As Object, e As System.EventArgs) Handles btnCancel.Click
        RaiseEvent Panel_Cancel_Click(sender, e)
    End Sub
    Private Sub btnDelete_ServerClick(sender As Object, e As System.EventArgs) Handles btnDelete.Click
        RaiseEvent Panel_Delete_Click(sender, e)
    End Sub
    Private Sub btnSearch_ServerClick(sender As Object, e As System.EventArgs) Handles btnSearch.Click
        RaiseEvent Panel_Search_Click(txtSearchBox.Text.Trim)
    End Sub
    Private Sub btnWizard_ServerClick(sender As Object, e As System.EventArgs) Handles btnWizard.Click
        RaiseEvent Panel_Wizard_Click(sender, e)
    End Sub
    Private Sub btnUp_ServerClick(sender As Object, e As System.EventArgs) Handles btnUp.Click
        RaiseEvent Panel_Up_Click(sender, e)
    End Sub
    Private Sub btnConfirmRequest_ServerClick(sender As Object, e As System.EventArgs) Handles btnConfirmRequest.Click
        RaiseEvent Panel_ConfirmRequest_Click(sender, e)
    End Sub
    Private Sub btnReject_ServerClick(sender As Object, e As System.EventArgs) Handles btnRejectRequest.Click
        RaiseEvent Panel_Reject_Click(sender, e)
    End Sub

    Private Sub btnDispaly_ServerClick(sender As Object, e As System.EventArgs) Handles btnDisplay.Click
        RaiseEvent Panel_Display_Click(sender, e)
    End Sub
    Private Sub btnExcel_ServerClick(sender As Object, e As System.EventArgs) Handles btnImportToExcel.Click
        RaiseEvent Panel_Excel_Click(sender, e)
    End Sub
    Private Sub btnPDF_ServerClick(sender As Object, e As System.EventArgs) Handles btnImportToPDF.Click
        RaiseEvent Panel_PDF_Click(sender, e)
    End Sub

    Public Property CanNew As Boolean
        Get
            Return btnNew.Visible
        End Get
        Set(value As Boolean)
            btnNew.Visible = value
        End Set
    End Property


    Public Property CanSave As Boolean
        Get
            Return btnSave.Visible
        End Get
        Set(value As Boolean)
            btnSave.Visible = value
        End Set
    End Property

    Public Property CanCancel As Boolean
        Get
            Return btnCancel.Visible
        End Get
        Set(value As Boolean)
            btnCancel.Visible = value
        End Set
    End Property

    Public Property CanDelete As Boolean
        Get
            Return btnDelete.Visible
        End Get
        Set(value As Boolean)
            btnDelete.Visible = value
        End Set
    End Property

    Public Property CanSearch As Boolean
        Get
            Return btnSearch.Visible
        End Get
        Set(value As Boolean)
            btnSearch.Visible = value
            txtSearchBox.Visible = value
        End Set
    End Property

    Public Property CanWizard As Boolean
        Get
            Return btnWizard.Visible
        End Get
        Set(value As Boolean)
            btnWizard.Visible = value
        End Set
    End Property

    Public Property CanUp As Boolean
        Get
            Return btnUp.Visible
        End Get
        Set(value As Boolean)
            btnUp.Visible = value
        End Set
    End Property

    Public Property CanConfirmRequest As Boolean
        Get
            Return btnConfirmRequest.Visible
        End Get
        Set(value As Boolean)
            btnConfirmRequest.Visible = value
        End Set
    End Property

    Public Property CanReject As Boolean
        Get
            Return btnRejectRequest.Visible
        End Get
        Set(value As Boolean)
            btnRejectRequest.Visible = value
        End Set
    End Property

    Public Property CanDisplay As Boolean
        Get
            Return btnDisplay.Visible
        End Get
        Set(value As Boolean)
            btnDisplay.Visible = value
        End Set
    End Property

    Public Property CanExcel As Boolean
        Get
            Return btnImportToExcel.Visible
        End Get
        Set(value As Boolean)
            btnImportToExcel.Visible = value
        End Set
    End Property

    Public Property CanPDF As Boolean
        Get
            btnImportToPDF.Visible = True
            Return btnImportToPDF.Visible
        End Get
        Set(value As Boolean)
            btnImportToPDF.Visible = value
        End Set


    End Property

    Public Sub ShowMessage(strMessage As String, blnWarning As Boolean)

        If blnWarning = True Then
            divMessageWarning.Style("display") = "inline"
            divMessageInfo.Style("display") = "none"
            lblMessageWarning.Text = " " & strMessage
        Else
            divMessageInfo.Style("display") = "inline"
            divMessageWarning.Style("display") = "none"
            lblMessageInfo.Text = " " & strMessage
        End If

    End Sub

    Public Sub ClearMessage()
        divMessageWarning.Style("display") = "none"
        divMessageInfo.Style("display") = "none"
    End Sub



    Public Property Enable_New_Client_Validate() As Boolean
        Get
            Return P_blnEnable_New_Client_Validate
        End Get
        Set(ByVal value As Boolean)
            P_blnEnable_New_Client_Validate = value
            If value = True Then
                btnNew.Attributes.Item("onclick") = "return NewOperation_Validate();"
            Else
                btnNew.Attributes.Item("onclick") = "return true;"
            End If
        End Set
    End Property

    Public Property Enable_Save_Client_Validate() As Boolean
        Get
            Return P_blnEnable_Save_Client_Validate
        End Get
        Set(ByVal value As Boolean)
            P_blnEnable_Save_Client_Validate = value
            If value = True Then
                btnSave.Attributes.Item("onclick") = "return SaveOperation_Validate();"
            Else
                btnSave.Attributes.Item("onclick") = "return true;"
            End If
        End Set
    End Property

    Public Property Enable_Cancel_Client_Validate() As Boolean
        Get
            Return P_blnEnable_Cancel_Client_Validate
        End Get
        Set(ByVal value As Boolean)
            P_blnEnable_Cancel_Client_Validate = value
            If value = True Then
                btnCancel.Attributes.Item("onclick") = "return CancelOperation_Validate();"
            Else
                btnCancel.Attributes.Item("onclick") = "return true;"
            End If
        End Set
    End Property


    Public Property Enable_Delete_Client_Validate() As Boolean
        Get
            Return P_blnEnable_Delete_Client_Validate
        End Get
        Set(ByVal value As Boolean)
            P_blnEnable_Delete_Client_Validate = value
            If value = True Then
                btnDelete.Attributes.Item("onclick") = "return DeleteOperation_Validate();"
            Else
                btnDelete.Attributes.Item("onclick") = "return true;"
            End If
        End Set
    End Property
    Public Property Enable_Search_Client_Validate() As Boolean
        Get
            Return P_blnEnable_Search_Client_Validate
        End Get
        Set(ByVal value As Boolean)
            P_blnEnable_Search_Client_Validate = value
            If value = True Then
                btnSearch.Attributes.Item("onclick") = "return SearchOperation_Validate();"
            Else
                btnSearch.Attributes.Item("onclick") = "return true;"
            End If
        End Set
    End Property
    Public Property Enable_Wizard_Client_Validate() As Boolean
        Get
            Return P_blnEnable_Wizard_Client_Validate
        End Get
        Set(ByVal value As Boolean)
            P_blnEnable_Wizard_Client_Validate = value
            If value = True Then
                btnWizard.Attributes.Item("onclick") = "return WizardOperation_Validate();"
            Else
                btnWizard.Attributes.Item("onclick") = "return true;"
            End If
        End Set
    End Property
    Public Property Enable_Up_Client_Validate() As Boolean
        Get
            Return P_blnEnable_Up_Client_Validate
        End Get
        Set(ByVal value As Boolean)
            P_blnEnable_Up_Client_Validate = value
            If value = True Then
                btnUp.Attributes.Item("onclick") = "return UpOperation_Validate();"
            Else
                btnUp.Attributes.Item("onclick") = "return true;"
            End If
        End Set
    End Property

    Public Property Enable_ConfirmRequest_Client_Validate() As Boolean
        Get
            Return P_blnEnable_ConfirmRequest_Client_Validate
        End Get
        Set(ByVal value As Boolean)
            P_blnEnable_ConfirmRequest_Client_Validate = value
            If value = True Then
                btnConfirmRequest.Attributes.Item("onclick") = "return ConfirmRequestOperation_Validate();"
            Else
                btnConfirmRequest.Attributes.Item("onclick") = "return true;"
            End If
        End Set
    End Property

    Public Property Enable_RejectRequest_Client_Validate() As Boolean
        Get
            Return P_blnEnable_RejectRequest_Client_Validate
        End Get
        Set(ByVal value As Boolean)
            P_blnEnable_RejectRequest_Client_Validate = value
            If value = True Then
                btnRejectRequest.Attributes.Item("onclick") = "return RejectRequestOperation_Validate();"
            Else
                btnRejectRequest.Attributes.Item("onclick") = "return true;"
            End If
        End Set
    End Property

    Public Property Enable_Display_Client_Validate() As Boolean
        Get
            Return P_blnEnable_Display_Client_Validate
        End Get
        Set(ByVal value As Boolean)
            P_blnEnable_Display_Client_Validate = value
            If value = True Then
                btnDisplay.Attributes.Item("onclick") = "return DisplayOperation_Validate();"
            Else
                btnDisplay.Attributes.Item("onclick") = "return true;"
            End If
        End Set
    End Property

    Public Property Enable_Excel_Client_Validate() As Boolean
        Get
            Return P_blnEnable_Excel_Client_Validate
        End Get
        Set(ByVal value As Boolean)
            P_blnEnable_Excel_Client_Validate = value
            If value = True Then
                btnImportToExcel.Attributes.Item("onclick") = "return ExcelOperation_Validate();"
            Else
                btnImportToExcel.Attributes.Item("onclick") = "return true;"
            End If
        End Set
    End Property

    Public Property Enable_PDF_Client_Validate() As Boolean
        Get
            Return P_blnEnable_PDF_Client_Validate
        End Get
        Set(ByVal value As Boolean)
            P_blnEnable_PDF_Client_Validate = value
            If value = True Then
                btnImportToPDF.Attributes.Item("onclick") = "return PDFOperation_Validate();"
            Else
                btnImportToPDF.Attributes.Item("onclick") = "return true;"
            End If
        End Set
    End Property

End Class