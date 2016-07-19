Imports System.IO
Imports NAudio.Wave
Public Class VoiceMessageFileNew
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Page.Response.Cache.SetCacheability(HttpCacheability.NoCache)

        Bootstrap_Panel1.CanNew = False
        Bootstrap_Panel1.CanSave = True
        Bootstrap_Panel1.CanDelete = False
        Bootstrap_Panel1.CanSearch = False
        Bootstrap_Panel1.CanCancel = False
        Bootstrap_Panel1.CanUp = True
        Bootstrap_Panel1.CanWizard = False
        Bootstrap_Panel1.CanConfirmRequest = False
        Bootstrap_Panel1.CanReject = False
        Bootstrap_Panel1.CanDisplay = False
        Bootstrap_Panel1.CanExcel = False
        Bootstrap_Panel1.Enable_Save_Client_Validate = True

        lblInnerPageTitle.Text = "پرکردن کادرهای قرمز رنگ، اجباری است."

        If Page.IsPostBack = False Then



        End If


    End Sub

    Private Sub Bootstrap_Panel1_Panel_Save_Click(sender As Object, e As EventArgs) Handles Bootstrap_Panel1.Panel_Save_Click

        Try

            Bootstrap_Panel1.ShowMessage("", True)

            Dim dtblUserLogin As BusinessObject.dstUser.spr_User_Login_SelectDataTable = CType(Session("dtblUserLogin"), BusinessObject.dstUser.spr_User_Login_SelectDataTable)
            Dim drwUserLogin As BusinessObject.dstUser.spr_User_Login_SelectRow = dtblUserLogin.Rows(0)



            Dim strPath As String = ""
            Dim strExt As String = ""
            Dim strFileName As String = ""
            Dim strRawFileName As String = ""
            Dim MaxSize As Integer = 0
            Dim duration As Double = 0
            Dim qryZamanak As New BusinessObject.dstZamanakTableAdapters.QueriesTableAdapter
            Dim clsZamanak As New ClsZamanak
            Dim oVoiceSMS As New VoiceSMS.RahyabVoiceSend



            If file_contents.PostedFile.ContentLength > 0 Then
                strExt = Path.GetExtension(file_contents.PostedFile.FileName)
                strFileName = Path.GetFileName(file_contents.PostedFile.FileName)
                strRawFileName = Path.GetFileNameWithoutExtension(file_contents.PostedFile.FileName)

                'If clsZamanak.CheckDuplicateVoice(Path.GetFileNameWithoutExtension(file_contents.PostedFile.FileName)) = True Then
                If clsZamanak.CheckDuplicateVoice(txtVoiceMessageName.Text.Trim()) = True Then
                    Bootstrap_Panel1.ShowMessage("نام فایل بارگذاری شده تکراری می باشد.", True)
                    Exit Sub
                End If

                MaxSize = file_contents.PostedFile.ContentLength
                strPath = Server.MapPath("") & "\Temp\" & strFileName
                Dim strListenPath As String = Server.MapPath("") & "\Listen\" & strRawFileName & ".wav"
                file_contents.SaveAs(strPath)

                Select Case strExt
                    Case ".wav"
                        Dim reader As New WaveFileReader(strPath)
                        WaveFileWriter.CreateWaveFile(strListenPath, reader)
                        duration = reader.TotalTime.TotalSeconds
                    Case ".mp3"
                        Dim reader As New Mp3FileReader(strPath)
                        Dim pcmStream As WaveStream = WaveFormatConversionStream.CreatePcmStream(reader)
                        WaveFileWriter.CreateWaveFile(strListenPath, pcmStream)
                        duration = reader.TotalTime.TotalSeconds
                    Case Else
                        Bootstrap_Panel1.ShowMessage("فایل بارگذاری شده به فرمت صوتی معتبر نمی باشد.", True)
                        Exit Sub
                End Select

                If duration > 120 Then
                    File.Delete(strPath)
                    Bootstrap_Panel1.ShowMessage("زمان فایل صوتی انتخابی بیش از حد مجاز است.", True)
                    Exit Sub
                End If

                If MaxSize > 5242880 Then
                    File.Delete(strPath)
                    Bootstrap_Panel1.ShowMessage("حجم فایل صوتی انتخابی بیش از حد مجاز است.", True)
                    Exit Sub
                End If

                Dim strFileSize As String = ""
                Select Case MaxSize
                    Case Is < 1048576
                        strFileSize = " KB"
                    Case Else
                        strFileSize = " MB"
                End Select

                Dim br As New BinaryReader(file_contents.PostedFile.InputStream)
                Dim fileBytes As Byte() = br.ReadBytes(CInt(file_contents.PostedFile.InputStream.Length))

                Dim intUid As Integer = 0
                Dim strToken As String = ""
                Dim strRefMessage As String = ""

                'Dim strPass As String = clsZamanak.GetRegisteredUser(drwUserLogin.ID, drwUserLogin.Mobile, intUid, strToken)

                'If strPass = "" Or intUid = 0 Or strToken = "" Then
                '    Bootstrap_Panel1.ShowMessage("کاربر گرامی، ارسال پیامک صوتی برای شما فعال نشده است.", True)
                '    Exit Sub
                'End If

                ''Dim intVoiceID As Integer = clsZamanak.UploadAudio(intUid, strToken, fileBytes, strRefMessage)
                Dim intVoiceID As Integer = oVoiceSMS.UploadVoiceFile("09121485002", "ati6077", fileBytes, strFileName, strExt, duration, MaxSize, strRefMessage)
                Dim strVoiceFileName As String = txtVoiceMessageName.Text
                If strRefMessage = "" Then
                    Dim intRecordID As Integer = qryZamanak.spr_VoiceRecords_Insert(intVoiceID, strVoiceFileName, fileBytes, duration, Math.Round(MaxSize / 1024) & strFileSize, drwUserLogin.ID, strRawFileName)


                    strListenPath = Server.MapPath("") & "\Listen\" & intRecordID.ToString() & strRawFileName & ".wav"
                    file_contents.SaveAs(strListenPath)

                Else
                        Bootstrap_Panel1.ShowMessage("در فرآیند بارگذاری فایل صوتی خطا رخ داده است:" & strRefMessage, True)
                    Exit Sub
                End If



            Else
                Bootstrap_Panel1.ShowMessage("فایلی برای بارگذاري انتخاب نگردیده است.", True)
                Exit Sub
            End If

        Catch ex As Exception
            Bootstrap_Panel1.ShowMessage("در بارگذاری فایل(ها) خطا رخ داده است." & " خطا: " & ex.Message, True)
        End Try

        Response.Redirect("VoiceMessageManagement.aspx")


    End Sub

    Private Sub Bootstrap_Panel1_Panel_Up_Click(sender As Object, e As EventArgs) Handles Bootstrap_Panel1.Panel_Up_Click
        Response.Redirect("VoiceMessageManagement.aspx")
    End Sub
End Class