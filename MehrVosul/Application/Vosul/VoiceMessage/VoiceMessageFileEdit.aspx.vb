Imports System.IO
Imports NAudio.Wave
Imports System.Media

Public Class VoiceMessageFileEdit
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
        Bootstrap_Panel1.Enable_Save_Client_Validate = True

        lblInnerPageTitle.Text = "پرکردن کادرهای قرمز رنگ، اجباری است."

        If Page.IsPostBack = False Then

            If Not Session("intEditVoiceMessageID") Is Nothing Then

                Dim intEditVoiceMessageID = CInt(Session("intEditVoiceMessageID"))
                Dim tadpVoiceMessage As New BusinessObject.dstZamanakTableAdapters.spr_Record_List_SelectTableAdapter
                Dim dtblVoiceMessage As BusinessObject.dstZamanak.spr_Record_List_SelectDataTable = Nothing


                dtblVoiceMessage = tadpVoiceMessage.GetData(2, -1, intEditVoiceMessageID, "", -1)
                txtVoiceMessageName.Text = dtblVoiceMessage.First.Name

                ViewState("VoicrMSGFileName") = dtblVoiceMessage.First.FileName


            End If


        End If


    End Sub

    Private Sub Bootstrap_Panel1_Panel_Save_Click(sender As Object, e As EventArgs) Handles Bootstrap_Panel1.Panel_Save_Click

        ''   Try

        ''    Bootstrap_Panel1.ShowMessage("", True)

        ''    Dim dtblUserLogin As BusinessObject.dstUser.spr_User_Login_SelectDataTable = CType(Session("dtblUserLogin"), BusinessObject.dstUser.spr_User_Login_SelectDataTable)
        ''    Dim drwUserLogin As BusinessObject.dstUser.spr_User_Login_SelectRow = dtblUserLogin.Rows(0)



        ''    Dim strPath As String = ""
        ''    Dim strExt As String = ""
        ''    Dim strFileName As String = ""
        ''    Dim strRawFileName As String = ""
        ''    Dim MaxSize As Integer = 0
        ''    Dim duration As Double = 0
        ''    Dim qryZamanak As New BusinessObject.dstZamanakTableAdapters.QueriesTableAdapter
        ''    Dim clsZamanak As New ClsZamanak

        ''    If file_contents.PostedFile.ContentLength > 0 Then
        ''        strExt = Path.GetExtension(file_contents.PostedFile.FileName)
        ''        strFileName = Path.GetFileName(file_contents.PostedFile.FileName)
        ''        strRawFileName = Path.GetFileNameWithoutExtension(file_contents.PostedFile.FileName)

        ''        'If clsZamanak.CheckDuplicateVoice(Path.GetFileNameWithoutExtension(file_contents.PostedFile.FileName)) = True Then
        ''        If clsZamanak.CheckDuplicateVoice(txtVoiceMessageName.Text.Trim()) = True Then
        ''            Bootstrap_Panel1.ShowMessage("نام فایل بارگذاری شده تکراری می باشد.", True)
        ''            Exit Sub
        ''        End If

        ''        MaxSize = file_contents.PostedFile.ContentLength
        ''        strPath = Server.MapPath("") & "\Temp\" & strFileName
        ''        Dim strListenPath As String = Server.MapPath("") & "\Listen\" & strRawFileName & ".wav"
        ''        file_contents.SaveAs(strPath)

        ''        Select Case strExt
        ''            Case ".wav"
        ''                Dim reader As New WaveFileReader(strPath)
        ''                WaveFileWriter.CreateWaveFile(strListenPath, reader)
        ''                duration = reader.TotalTime.TotalSeconds
        ''            Case ".mp3"
        ''                Dim reader As New Mp3FileReader(strPath)
        ''                Dim pcmStream As WaveStream = WaveFormatConversionStream.CreatePcmStream(reader)
        ''                WaveFileWriter.CreateWaveFile(strListenPath, pcmStream)
        ''                duration = reader.TotalTime.TotalSeconds
        ''            Case Else
        ''                Bootstrap_Panel1.ShowMessage("فایل بارگذاری شده به فرمت صوتی معتبر نمی باشد.", True)
        ''                Exit Sub
        ''        End Select

        ''        If duration > 120 Then
        ''            File.Delete(strPath)
        ''            Bootstrap_Panel1.ShowMessage("زمان فایل صوتی انتخابی بیش از حد مجاز است.", True)
        ''            Exit Sub
        ''        End If

        ''        If MaxSize > 5242880 Then
        ''            File.Delete(strPath)
        ''            Bootstrap_Panel1.ShowMessage("حجم فایل صوتی انتخابی بیش از حد مجاز است.", True)
        ''            Exit Sub
        ''        End If

        ''        Dim strFileSize As String = ""
        ''        Select Case MaxSize
        ''            Case Is < 1048576
        ''                strFileSize = " KB"
        ''            Case Else
        ''                strFileSize = " MB"
        ''        End Select

        ''        Dim br As New BinaryReader(file_contents.PostedFile.InputStream)
        ''        Dim fileBytes As Byte() = br.ReadBytes(CInt(file_contents.PostedFile.InputStream.Length))

        ''        Dim intUid As Integer = 0
        ''        Dim strToken As String = ""
        ''        Dim strRefMessage As String = ""

        ''        Dim strPass As String = clsZamanak.GetRegisteredUser(drwUserLogin.ID, drwUserLogin.Mobile, intUid, strToken)
        ''        If strPass = "" Or intUid = 0 Or strToken = "" Then
        ''            Bootstrap_Panel1.ShowMessage("کاربر گرامی، ارسال پیامک صوتی برای شما فعال نشده است.", True)
        ''            Exit Sub
        ''        End If

        ''        Dim intVoiceID As Integer = clsZamanak.UploadAudio(intUid, strToken, fileBytes, strRefMessage)
        ''        Dim strVoiceFileName As String = txtVoiceMessageName.Text
        ''        If strRefMessage = "" Then
        ''            Dim intRecordID As Integer = qryZamanak.spr_VoiceRecords_Insert(intVoiceID, strVoiceFileName, fileBytes, duration, Math.Round(MaxSize / 1024) & strFileSize, drwUserLogin.ID, strRawFileName)


        ''            strListenPath = Server.MapPath("") & "\Listen\" & intRecordID.ToString() & strRawFileName & ".wav"
        ''            file_contents.SaveAs(strListenPath)

        ''        Else
        ''            Bootstrap_Panel1.ShowMessage("در فرآیند بارگذاری فایل صوتی خطا رخ داده است:" & strRefMessage, True)
        ''            Exit Sub
        ''        End If



        ''    Else
        ''        Bootstrap_Panel1.ShowMessage("فایلی برای بارگذاري انتخاب نگردیده است.", True)
        ''        Exit Sub
        ''    End If

        ''Catch ex As Exception
        ''    Bootstrap_Panel1.ShowMessage("در بارگذاری فایل(ها) خطا رخ داده است." & " خطا: " & ex.Message, True)
        ''End Try

        ''Response.Redirect("VoiceMessageManagement.aspx")


    End Sub

    Private Sub Bootstrap_Panel1_Panel_Up_Click(sender As Object, e As EventArgs) Handles Bootstrap_Panel1.Panel_Up_Click
        Response.Redirect("VoiceMessageManagement.aspx")
    End Sub

    Protected Sub imgbtnVoiceMessage_Click(sender As Object, e As ImageClickEventArgs) Handles imgbtnVoiceMessage.Click

        Dim intRecordID As Integer = CInt(Session("intEditVoiceMessageID"))


        Dim tadpRecords As New BusinessObject.dstZamanakTableAdapters.spr_Record_List_SelectTableAdapter
        Dim dtblRecords As BusinessObject.dstZamanak.spr_Record_List_SelectDataTable = Nothing
        dtblRecords = tadpRecords.GetData(2, 0, intRecordID, "", 0)
        If dtblRecords.Rows.Count > 0 Then

            Dim strFileName As String = dtblRecords.First().FileName
            If strFileName <> "" Then
                If Not File.Exists(Server.MapPath("") & "\Listen\" & intRecordID.ToString() & strFileName & ".wav") Then
                    Bootstrap_Panel1.ShowMessage("فایل صوتی مورد نظر وجود ندارد.", True)

                    Exit Sub
                End If

                Dim player As New SoundPlayer
                player.SoundLocation = Server.MapPath("") & "\Listen\" & intRecordID.ToString() & strFileName & ".wav"
                player.Play()
            Else
                Bootstrap_Panel1.ShowMessage("نام فایل صوتی نامعتبر است.", True)

                Exit Sub
            End If
        End If


    End Sub
End Class