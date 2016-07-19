Public Class WebForm2
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Bootstrap_PersianDateTimePicker1.ShowTimePicker = True
        Bootstrap_PersianDateTimePicker1.PersianDateTime = mdlGeneral.GetPersianDate(Date.Now) & " " & Date.Now.ToString("HH:mm:ss")

    End Sub

    Protected Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click


        Response.Write(Bootstrap_PersianDateTimePicker1.PersianDate)

        Response.Write(Bootstrap_PersianDateTimePicker1.PersianTime)


    End Sub
End Class