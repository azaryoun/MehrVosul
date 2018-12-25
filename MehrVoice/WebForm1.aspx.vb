Public Class WebForm1
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load


        Dim ss As New MehrVoice
        Dim arrDes(1) As String
        arrDes(0) = "09123201844"
        arrDes(1) = "09018995673"
        Dim strMessage As String = ""


        ss.SendVoiceSMS_MehrNew("vesal", "matchautoreplay123", 9899, "ZamanakSoapV45815e7f52aaab", "VoiceSMSNew_Test", arrDes, "", 1, strMessage)

    End Sub

End Class