Public Class DraftManagement
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load


        Page.Response.Cache.SetCacheability(HttpCacheability.NoCache)

        Bootstrap_Panel1.CanNew = False
        Bootstrap_Panel1.CanSave = False
        Bootstrap_Panel1.CanDelete = False
        Bootstrap_Panel1.CanSearch = False
        Bootstrap_Panel1.CanCancel = False
        Bootstrap_Panel1.CanUp = True
        Bootstrap_Panel1.CanWizard = False
        Bootstrap_Panel1.CanConfirmRequest = False
        Bootstrap_Panel1.CanReject = False
        Bootstrap_Panel1.CanDisplay = False
        Bootstrap_Panel1.CanExcel = False
        Bootstrap_Panel1.Enable_Delete_Client_Validate = True
        Bootstrap_Panel1.Enable_Search_Client_Validate = True


        lblInnerPageTitle.Text = "فهرست الگوها"


        If Page.IsPostBack = False Then
            ''get warning intervals title 

            Dim intEditWarningIntervalsID As Integer = Session("intEditWarningIntervalsID")
            Dim tadpwarning As New BusinessObject.dstWarningIntervalsTableAdapters.spr_WarningIntervals_SelectTableAdapter
            Dim dtblWarning As BusinessObject.dstWarningIntervals.spr_WarningIntervals_SelectDataTable = Nothing

            dtblWarning = tadpwarning.GetData(intEditWarningIntervalsID)


            lblInnerPageTitle.Text = lblInnerPageTitle.Text & " (گردش کار " & dtblWarning.First.WarniningTitle & ")"

        End If


    End Sub

    Protected Sub lnkbtnSMSBorrower_Click(sender As Object, e As EventArgs) Handles lnkbtnSMSBorrower.Click

        Response.Redirect("DraftText.aspx?Type=1&ToSponsor=False")

    End Sub


    Protected Sub lnkbtnSMSSponsor_Click(sender As Object, e As EventArgs) Handles lnkbtnSMSSponsor.Click
        Response.Redirect("DraftText.aspx?Type=1&ToSponsor=True")
    End Sub



    Private Sub Bootstrap_Panel1_Panel_Up_Click(sender As Object, e As System.EventArgs) Handles Bootstrap_Panel1.Panel_Up_Click
        Response.Redirect("../WarningIntervals/WarningIntervalsManagement.aspx")
    End Sub

    Protected Sub lnkbtnIntroductionBorrower_Click(sender As Object, e As EventArgs) Handles lnkbtnIntroductionBorrower.Click
        Response.Redirect("DraftText.aspx?Type=2&ToSponsor=False")
    End Sub

    Protected Sub lnkbtnNoticeBorrower_Click(sender As Object, e As EventArgs) Handles lnkbtnNoticeBorrower.Click
        Response.Redirect("DraftText.aspx?Type=3&ToSponsor=False")
    End Sub

    Protected Sub lnkbtnManifestBorrower_Click(sender As Object, e As EventArgs) Handles lnkbtnManifestBorrower.Click
        Response.Redirect("DraftText.aspx?Type=4&ToSponsor=False")
    End Sub


    Protected Sub lnkbtnIntroductionSponsor_Click(sender As Object, e As EventArgs) Handles lnkbtnIntroductionSponsor.Click
        Response.Redirect("DraftText.aspx?Type=2&ToSponsor=True")
    End Sub

    Protected Sub lnkbtnNoticeSponsor_Click(sender As Object, e As EventArgs) Handles lnkbtnNoticeSponsor.Click
        Response.Redirect("DraftText.aspx?Type=3&ToSponsor=True")
    End Sub

    Protected Sub lnkbtnManifestSponsor_Click(sender As Object, e As EventArgs) Handles lnkbtnManifestSponsor.Click
        Response.Redirect("DraftText.aspx?Type=4&ToSponsor=True")
    End Sub

    Protected Sub lnkbtnVoiceMSGBorrower_Click(sender As Object, e As EventArgs) Handles lnkbtnVoiceMSGBorrower.Click
        Response.Redirect("VoiceMessageDraftText.aspx?Type=5&ToSponsor=False")
    End Sub

    Protected Sub lnkbtnVoiceMSGSponsor_Click(sender As Object, e As EventArgs) Handles lnkbtnVoiceMSGSponsor.Click
        Response.Redirect("VoiceMessageDraftText.aspx?Type=5&ToSponsor=True")
    End Sub
End Class