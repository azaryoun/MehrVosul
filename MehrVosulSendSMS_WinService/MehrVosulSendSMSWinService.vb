Public Class MehrVosulSendSMSWinService

    Protected Overrides Sub OnStart(ByVal args() As String)
        ' Add code here to start your service. This method should set things
        ' in motion so your service can do its work.
    End Sub

    Protected Overrides Sub OnStop()
        ' Add code here to perform any tear-down necessary to stop your service.
    End Sub

    Private Sub tmrSendSMS_Elapsed(sender As System.Object, e As System.Timers.ElapsedEventArgs) Handles tmrSendSMS.Elapsed

    End Sub
End Class
