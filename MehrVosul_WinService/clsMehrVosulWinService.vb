Imports System.Net.Mail
Imports System
Imports System.IO
Imports System.Resources
Imports System.Net
Imports System.IO.FileStream
Imports System.IO.File
Imports System.Data.OleDb
Imports System.Xml
Imports System.Configuration
Imports System.Data.OracleClient
Imports System.Data.Entity



Public Class clsMehrVosulWinService
    Public Const CLIENT_ID As String = "api@zamanak.ir"
    Public Const CLIENT_SECRET As String = "9AmbEG61AgW3CQoSV1p3A4tS9CZ"

    ' '' For Test
    'Dim arrTempNumbers() As String = {"09128165662", "09122764983", "09363738886", "09128439051", "09128165662", "09122473046", "09125010426", "09122336256", "09128165662", "09125470419", "09128165662", "09128165662", "09128165662", "09128165662", "09128165662", "09128439051", "09128439051", "09128439051", "09128439051", "09128439051", "09128439051", "09128439051", "09128439051", "09128439051"}
    'Dim rndTempVar As New Random
    Private drwSystemSetting As BusinessObject.dstSystemSetting.spr_SystemSetting_SelectRow = Nothing


    Private Structure stc_Loan_Info

        Public FullName As String '0
        Public Address As String '1
        Public Telephone As String '2
        Public Fax As String '3
        Public Mobile As String '4
        Public Date_P As String '5
        Public LC_No As String '6
        Public BranchCode As String '7
        Public LoanTypeCode As String '8
        Public CustomerNo As String '9
        Public LoanSerial As Integer '10
        Public LCDate As String '11

        Public LCAmount As Double? '12
        Public LCProfit As Double? ' 13
        Public IstlNum As Integer? '14


        Public LCAmountPaid As Double? '15
        Public IstlPaid As Integer? '16
        Public AmounDefferd As Double? '17

        Public GDeffered As Integer? '18
        Public FirstNoPaidDate As String '19
        Public Status As String '20

        Public NotPiadDurationDay As Integer? '21
        Public LCBalance As Double? '22
        Public G_Mande As Integer? '23

        Public G_MustPay As Integer? '24
        Public Amount_MustPay As Double? '25
        Public FG_MustPayDate As String '26

        Public L_PayDate As String '27
        Public LG_PayDate As String '28

        Public lcupdate As String  '29

        Public NationalID As String  '30
        Public NationalNo As String  '31

        Public Sex As String  '32


    End Structure

    Private Structure stc_Deposit_Info

        Public CSTTYPDESC As String '0  مشتری حقیقی یا حقوقی
        Public CSTTYP As String '1 شماره حساب
        Public CustomerNo As String '2 CFCIFNO شماره مشتری
        Public Name As String '3 نام
        Public LastName As String '4 نام خانوادگی 
        Public FatherName As String '5 نام پدر
        Public IDNo As String  '6 شماره شناسنامه
        Public Sex As String  '7
        Public BirthDate As String '8
        Public NationalID As String  '9
        Public BranchCode As String '10
        Public Date_P As String '11 تاریخ افتتاح حساب
        Public Address As String  '12
        Public Telephone As String '13
        Public Mobile As String '14
        Public DepositTypeCode As String '16
        Public DepositDesc As String '17
        Public BranchName As String '---
        Public BranchAddress As String '---
        Public DepositAmount As String '---

    End Structure

    Private Structure stc_Loan_Hadi

        Public CustomerNO As String '0
        Public Name As String '1
        Public LastName As String '2
        Public Gender As String '3
        Public Mobile As String '4
        Public LCDateMiladi As String '5
        Public BranchCode As String '6
        Public LoanTypeCode As String '7
        Public SerialNumber As String '8
        Public LoanAmount As String '9
        Public LoanTitle As String '10
        Public LoanState As String '11
        '  Public SetelmentNumber As String '8
        ''  Public ?? As String '9
        '' Public LCDateShamsi As Integer '10

    End Structure

    Private Structure stc_Sponsor_Info
        Public BranchCode As String '0
        Public LoanTypeCode As String '1
        Public BorrowerCustomerNo As String '2
        Public LoanSerial As Integer '3 
        Public LCNo As String '4 
        Public FullName As String '5 
        Public FatherName As String '6 
        Public SponsorCustomerNo As String '7 
        Public NationalNo As String '8 
        Public NationalID As String '9 
        Public HomeTel As String '10 
        Public WorkTel As String '11
        Public Mobile As String '12 
        Public HomeAddress As String '13 
        Public WorkAddress As String '14
        Public WarantyTypeDesc As String '15 
        Public UpdateDate As String '16 


    End Structure

    Private Structure stc_Sponsor_Waranty
        Public SponsorID As Integer
        Public WarantyTypeDesc As String
    End Structure

    Private Structure stc_Branch_Info
        Public BranchName As String
        Public BranchAddress As String
    End Structure

    Protected Overrides Sub OnStart(ByVal args() As String)

        Dim tadpSystemSetting As New BusinessObject.dstSystemSettingTableAdapters.spr_SystemSetting_SelectTableAdapter
        Dim dtblSystemSetting As BusinessObject.dstSystemSetting.spr_SystemSetting_SelectDataTable = Nothing
        dtblSystemSetting = tadpSystemSetting.GetData()
        drwSystemSetting = dtblSystemSetting.Rows(0)

        Dim trSMSSend As New Threading.Thread(AddressOf SendSMS)
        trSMSSend.Start()

        'Dim trSMSDelivery As New Threading.Thread(AddressOf SMS_DeliveryUpdate)
        'trSMSDelivery.Start()

        Dim trNotification As New Threading.Thread(AddressOf SendNotification)
        trNotification.Start()

        ''Dim trNotification_Hadi_Loan As New Threading.Thread(AddressOf SendHadiNotification_Loan)
        ''trNotification_Hadi_Loan.Start()

    End Sub


    Protected Overrides Sub OnStop()
        ' Add code here to perform any tear-down necessary to stop your service.
    End Sub

    Private Sub SendNotification()



        Do

            Try

                Dim tadpLCStatus As New BusinessObject.dstCurrentLCStatusTableAdapters.spr_CurrentLCStatus_List_SelectTableAdapter
                Dim dtblLCStaus As BusinessObject.dstCurrentLCStatus.spr_CurrentLCStatus_List_SelectDataTable = Nothing
                dtblLCStaus = tadpLCStatus.GetData()

                If dtblLCStaus.Rows.Count = 0 Then
                    Threading.Thread.Sleep(5000)
                    Continue Do
                End If



                For Each drwLCStaus As BusinessObject.dstCurrentLCStatus.spr_CurrentLCStatus_List_SelectRow In dtblLCStaus.Rows
                    Try

                        Dim tadpWarningIntervalCheck As New BusinessObject.dstWarningIntervalsTableAdapters.spr_WarningIntervals_Check_SelectTableAdapter
                        Dim dtblWarningIntervalCheck As BusinessObject.dstWarningIntervals.spr_WarningIntervals_Check_SelectDataTable = Nothing
                        dtblWarningIntervalCheck = tadpWarningIntervalCheck.GetData(drwLCStaus.FK_LoanTypeID, drwLCStaus.NotPiadDurationDay, drwLCStaus.AmounDefferd, drwLCStaus.FK_BranchID)

                        If dtblWarningIntervalCheck.Rows.Count = 0 Then
                            Continue For

                        End If




                        Dim drwWarningIntervalCheck As BusinessObject.dstWarningIntervals.spr_WarningIntervals_Check_SelectRow = dtblWarningIntervalCheck.Rows(0)




                        Dim qryWarningNotificationLog As New BusinessObject.dstWarningNotificationLogTableAdapters.QueriesTableAdapter
                        Dim intWarningNotifcationLogID As Integer = qryWarningNotificationLog.spr_WarningNotificationLog_Insert(drwLCStaus.FK_LoanID, drwLCStaus.FK_FileID, drwWarningIntervalCheck.ID, drwLCStaus.Date_P, 1, Date.Now, False)




                        If drwWarningIntervalCheck.SendSMS = True Then

                            If drwLCStaus.IsMobileNoNull = True OrElse drwLCStaus.MobileNo.Length <> 11 Then
                                GoTo VoiceSMS
                            End If


                            If drwWarningIntervalCheck.ToBorrower = True Then

                                Dim strMessage As String = ""

                                strMessage = CreateMessage(1, drwLCStaus.IsMale, drwLCStaus.FName, drwLCStaus.LName, drwLCStaus.LoanNumber, drwLCStaus.NotPiadDurationDay, False, drwWarningIntervalCheck.ID, drwLCStaus.BranchName, "", "", True)

                                Dim qryWarningNotificationLogDetail As New BusinessObject.dstWarningNotificationLogDetailTableAdapters.QueriesTableAdapter
                                qryWarningNotificationLogDetail.spr_WarningNotificationLogDetail_Insert(intWarningNotifcationLogID, drwSystemSetting.GatewayNumber, drwLCStaus.MobileNo, True, strMessage, "", Date.Now, "", 1, 1, Date.Now)



                            End If

                            If drwWarningIntervalCheck.ToSponsor = True Then

                                Dim tadpSponsorList As New BusinessObject.dstLoanSponsorTableAdapters.spr_LoanSponsor_List_SelectTableAdapter
                                Dim dtblSponsorList As BusinessObject.dstLoanSponsor.spr_LoanSponsor_List_SelectDataTable = Nothing

                                dtblSponsorList = tadpSponsorList.GetData(drwLCStaus.FK_LoanID)

                                For Each drwSponsorList As BusinessObject.dstLoanSponsor.spr_LoanSponsor_List_SelectRow In dtblSponsorList.Rows



                                    Dim strMessage As String = ""

                                    strMessage = CreateMessage(1, drwLCStaus.IsMale, drwLCStaus.FName, drwLCStaus.LName, drwLCStaus.LoanNumber, drwLCStaus.NotPiadDurationDay, True, drwWarningIntervalCheck.ID, drwLCStaus.BranchName, drwSponsorList.FName, drwSponsorList.LName, drwSponsorList.IsMale)


                                    Dim qryWarningNotificationLogDetail As New BusinessObject.dstWarningNotificationLogDetailTableAdapters.QueriesTableAdapter
                                    qryWarningNotificationLogDetail.spr_WarningNotificationLogDetail_Insert(intWarningNotifcationLogID, drwSystemSetting.GatewayNumber, drwSponsorList.MobileNo, False, strMessage, "", Date.Now, "", 1, 1, Date.Now)
                                Next drwSponsorList



                            End If

                        End If

VoiceSMS:

                        If drwWarningIntervalCheck.VoiceMessage = True Then


                            If drwWarningIntervalCheck.ToBorrower = True Then

                                If drwLCStaus.IsMobileNoNull = False AndAlso drwLCStaus.MobileNo.Trim <> "" Then

                                    Dim arrTo() As String = {drwLCStaus.MobileNo}
                                    Dim arrRecords() As String = Nothing
                                    Dim arrNumbers() As String = Nothing
                                    Dim strSayMethod As String = "9"
                                    Dim strVoiceSMS_Name As String = "VoiceSMS_" & Date.Now.Millisecond
                                    GetVoiceSMSArrays_Vesal(drwWarningIntervalCheck.ID, False, arrRecords, arrNumbers)

                                    If arrRecords Is Nothing AndAlso arrNumbers Is Nothing Then
                                        Continue For
                                    End If

                                    For k As Integer = 0 To arrNumbers.Length - 1

                                        If arrNumbers(k) = 1 Then
                                            arrNumbers(k) = Val(drwLCStaus.LoanNumber.Replace("-", ""))
                                        Else
                                            arrNumbers(k) = Val(drwLCStaus.NotPiadDurationDay)
                                        End If
                                    Next k

                                    Try
                                        SendVoiceMixedSMS(drwSystemSetting.VoiceSMSUID, drwSystemSetting.VoiceSMSToken, strVoiceSMS_Name, arrTo, arrRecords, arrNumbers, strSayMethod)

                                    Catch ex As Exception

                                    End Try

                                    Dim qryWarningNotificationLogDetail As New BusinessObject.dstWarningNotificationLogDetailTableAdapters.QueriesTableAdapter
                                    qryWarningNotificationLogDetail.spr_WarningNotificationLogDetail_Insert(intWarningNotifcationLogID, "Voice SMS", drwLCStaus.MobileNo, True, "ارسال پیامک صوتی به موبایل وام گیرنده", "", Date.Now, "", 8, 6, Date.Now)


                                End If

                                ''If drwLCStaus.IsTelephoneHomeNull = False AndAlso drwLCStaus.TelephoneHome.Trim <> "" Then

                                ''    Dim arrTo() As String = {drwLCStaus.TelephoneHome}
                                ''    Dim arrRecords() As String = Nothing
                                ''    Dim arrNumbers() As String = Nothing
                                ''    Dim strSayMethod As String = "9"
                                ''    Dim strVoiceSMS_Name As String = "VoiceSMS_" & Date.Now.Millisecond
                                ''    GetVoiceSMSArrays_Vesal(drwWarningIntervalCheck.ID, False, arrRecords, arrNumbers)

                                ''    If arrRecords Is Nothing AndAlso arrNumbers Is Nothing Then
                                ''        Continue For
                                ''    End If

                                ''    For k As Integer = 0 To arrNumbers.Length - 1

                                ''        If arrNumbers(k) = 1 Then
                                ''            arrNumbers(k) = Val(drwLCStaus.LoanNumber.Replace("-", ""))
                                ''        Else
                                ''            arrNumbers(k) = Val(drwLCStaus.NotPiadDurationDay)
                                ''        End If
                                ''    Next k


                                ''    SendVoiceMixedSMS(drwSystemSetting.VoiceSMSUID, drwSystemSetting.VoiceSMSToken, strVoiceSMS_Name, arrTo, arrRecords, arrNumbers, strSayMethod)

                                ''    Dim qryWarningNotificationLogDetail As New BusinessObject.dstWarningNotificationLogDetailTableAdapters.QueriesTableAdapter
                                ''    qryWarningNotificationLogDetail.spr_WarningNotificationLogDetail_Insert(intWarningNotifcationLogID, "Voice SMS", drwLCStaus.TelephoneHome, True, "ارسال پیامک صوتی به تلفن منزل وام گیرنده", "", Date.Now, "", 8, 6, Date.Now)


                                ''End If

                                ''If drwLCStaus.IsTelephoneWorkNull = False AndAlso drwLCStaus.TelephoneWork.Trim <> "" Then


                                ''    Dim arrTo() As String = {drwLCStaus.TelephoneWork}
                                ''    Dim arrRecords() As String = Nothing
                                ''    Dim arrNumbers() As String = Nothing
                                ''    Dim strSayMethod As String = "9"
                                ''    Dim strVoiceSMS_Name As String = "VoiceSMS_" & Date.Now.Millisecond
                                ''    GetVoiceSMSArrays_Vesal(drwWarningIntervalCheck.ID, False, arrRecords, arrNumbers)

                                ''    If arrRecords Is Nothing AndAlso arrNumbers Is Nothing Then
                                ''        Continue For
                                ''    End If

                                ''    For k As Integer = 0 To arrNumbers.Length - 1

                                ''        If arrNumbers(k) = 1 Then
                                ''            arrNumbers(k) = Val(drwLCStaus.LoanNumber.Replace("-", ""))
                                ''        Else
                                ''            arrNumbers(k) = Val(drwLCStaus.NotPiadDurationDay)
                                ''        End If
                                ''    Next k


                                ''    SendVoiceMixedSMS(drwSystemSetting.VoiceSMSUID, drwSystemSetting.VoiceSMSToken, strVoiceSMS_Name, arrTo, arrRecords, arrNumbers, strSayMethod)

                                ''    Dim qryWarningNotificationLogDetail As New BusinessObject.dstWarningNotificationLogDetailTableAdapters.QueriesTableAdapter
                                ''    qryWarningNotificationLogDetail.spr_WarningNotificationLogDetail_Insert(intWarningNotifcationLogID, "Voice SMS", drwLCStaus.TelephoneWork, True, "ارسال پیامک صوتی به تلفن محل کار وام گیرنده", "", Date.Now, "", 8, 6, Date.Now)


                                ''End If




                            End If
                            If drwWarningIntervalCheck.ToSponsor = True Then



                                ''Dim tadpSponsorList As New BusinessObject.dstLoanSponsorTableAdapters.spr_LoanSponsor_List_SelectTableAdapter
                                ''Dim dtblSponsorList As BusinessObject.dstLoanSponsor.spr_LoanSponsor_List_SelectDataTable = Nothing

                                ''dtblSponsorList = tadpSponsorList.GetData(drwLCStaus.FK_LoanID)

                                ''For Each drwSponsorList As BusinessObject.dstLoanSponsor.spr_LoanSponsor_List_SelectRow In dtblSponsorList.Rows



                                ''    Dim qryInput As New BusinessObject.dstInputTableAdapters.QueriesTableAdapter


                                ''    If drwSponsorList.IsMobileNoNull = False AndAlso drwSponsorList.MobileNo.Trim <> "" Then


                                ''        Dim arrTo() As String = {drwSponsorList.MobileNo}
                                ''        Dim arrRecords() As String = Nothing
                                ''        Dim arrNumbers() As String = Nothing
                                ''        Dim strSayMethod As String = "9"
                                ''        Dim strVoiceSMS_Name As String = "VoiceSMS_" & Date.Now.Millisecond
                                ''        GetVoiceSMSArrays_Vesal(drwWarningIntervalCheck.ID, True, arrRecords, arrNumbers)

                                ''        If arrRecords Is Nothing AndAlso arrNumbers Is Nothing Then
                                ''            Continue For
                                ''        End If

                                ''        For k As Integer = 0 To arrNumbers.Length - 1

                                ''            If arrNumbers(k) = 1 Then
                                ''                arrNumbers(k) = Val(drwLCStaus.LoanNumber.Replace("-", ""))
                                ''            Else
                                ''                arrNumbers(k) = Val(drwLCStaus.NotPiadDurationDay)
                                ''            End If
                                ''        Next k

                                ''        SendVoiceMixedSMS(drwSystemSetting.VoiceSMSUID, drwSystemSetting.VoiceSMSToken, strVoiceSMS_Name, arrTo, arrRecords, arrNumbers, strSayMethod)

                                ''        Dim qryWarningNotificationLogDetail As New BusinessObject.dstWarningNotificationLogDetailTableAdapters.QueriesTableAdapter
                                ''        qryWarningNotificationLogDetail.spr_WarningNotificationLogDetail_Insert(intWarningNotifcationLogID, "Voice SMS", arrTo(0), True, "ارسال پیامک صوتی به موبایل ضامن", "", Date.Now, "", 8, 6, Date.Now)

                                ''    End If

                                ''    If drwSponsorList.IsTelephoneHomeNull = False AndAlso drwSponsorList.TelephoneHome.Trim <> "" Then

                                ''        Dim arrTo() As String = {drwSponsorList.TelephoneHome}
                                ''        Dim arrRecords() As String = Nothing
                                ''        Dim arrNumbers() As String = Nothing
                                ''        Dim strSayMethod As String = "9"
                                ''        Dim strVoiceSMS_Name As String = "VoiceSMS_" & Date.Now.Millisecond
                                ''        GetVoiceSMSArrays_Vesal(drwWarningIntervalCheck.ID, True, arrRecords, arrNumbers)

                                ''        If arrRecords Is Nothing AndAlso arrNumbers Is Nothing Then
                                ''            Continue For
                                ''        End If


                                ''        For k As Integer = 0 To arrNumbers.Length - 1

                                ''            If arrNumbers(k) = 1 Then
                                ''                arrNumbers(k) = Val(drwLCStaus.LoanNumber.Replace("-", ""))
                                ''            Else
                                ''                arrNumbers(k) = Val(drwLCStaus.NotPiadDurationDay)
                                ''            End If
                                ''        Next k

                                ''        SendVoiceMixedSMS(drwSystemSetting.VoiceSMSUID, drwSystemSetting.VoiceSMSToken, strVoiceSMS_Name, arrTo, arrRecords, arrNumbers, strSayMethod)

                                ''        Dim qryWarningNotificationLogDetail As New BusinessObject.dstWarningNotificationLogDetailTableAdapters.QueriesTableAdapter
                                ''        qryWarningNotificationLogDetail.spr_WarningNotificationLogDetail_Insert(intWarningNotifcationLogID, "Voice SMS", arrTo(0), True, "ارسال پیامک صوتی به تلفن منزل ضامن", "", Date.Now, "", 8, 6, Date.Now)




                                ''    End If

                                ''    If drwSponsorList.IsTelephoneWorkNull = False AndAlso drwSponsorList.TelephoneWork.Trim <> "" Then

                                ''        Dim arrTo() As String = {drwSponsorList.TelephoneWork}
                                ''        Dim arrRecords() As String = Nothing
                                ''        Dim arrNumbers() As String = Nothing
                                ''        Dim strSayMethod As String = "9"
                                ''        Dim strVoiceSMS_Name As String = "VoiceSMS_" & Date.Now.Millisecond
                                ''        GetVoiceSMSArrays_Vesal(drwWarningIntervalCheck.ID, True, arrRecords, arrNumbers)


                                ''        If arrRecords Is Nothing AndAlso arrNumbers Is Nothing Then
                                ''            Continue For
                                ''        End If


                                ''        For k As Integer = 0 To arrNumbers.Length - 1

                                ''            If arrNumbers(k) = 1 Then
                                ''                arrNumbers(k) = Val(drwLCStaus.LoanNumber.Replace("-", ""))
                                ''            Else
                                ''                arrNumbers(k) = Val(drwLCStaus.NotPiadDurationDay)
                                ''            End If
                                ''        Next k

                                ''        SendVoiceMixedSMS(drwSystemSetting.VoiceSMSUID, drwSystemSetting.VoiceSMSToken, strVoiceSMS_Name, arrTo, arrRecords, arrNumbers, strSayMethod)

                                ''        Dim qryWarningNotificationLogDetail As New BusinessObject.dstWarningNotificationLogDetailTableAdapters.QueriesTableAdapter
                                ''        qryWarningNotificationLogDetail.spr_WarningNotificationLogDetail_Insert(intWarningNotifcationLogID, "Voice SMS", arrTo(0), True, "ارسال پیامک صوتی به تلفن محل کار ضامن", "", Date.Now, "", 8, 6, Date.Now)



                                ''    End If


                                ''Next drwSponsorList

                            End If


                        End If




Telephone:

                        If drwWarningIntervalCheck.CallTelephone = True Then

                            If drwWarningIntervalCheck.ToBorrower = True Then

                                Dim qryInput As New BusinessObject.dstInputTableAdapters.QueriesTableAdapter


                                If drwLCStaus.IsMobileNoNull = False AndAlso drwLCStaus.MobileNo.Trim <> "" Then
                                    qryInput.spr_INPUT_Insert(drwLCStaus.MobileNo, "T1", drwLCStaus.LoanNumber, drwLCStaus.AmounDefferd, drwLCStaus.NotPiadDurationDay, drwLCStaus.NotPiadDurationDay, drwLCStaus.BrnachCode, Nothing, Nothing, Nothing, Nothing, 0, "pending", True, False, Nothing, Nothing, Nothing, Nothing, Nothing, "", Nothing, drwLCStaus.FName & " " & drwLCStaus.LName, "وام گیرنده", Nothing, Nothing, Nothing)
                                    Dim qryWarningNotificationLogDetail As New BusinessObject.dstWarningNotificationLogDetailTableAdapters.QueriesTableAdapter
                                    qryWarningNotificationLogDetail.spr_WarningNotificationLogDetail_Insert(intWarningNotifcationLogID, drwSystemSetting.TelephoneNumber, drwLCStaus.MobileNo, True, "تماس تلفنی با موبایل وام گیرنده", "", Date.Now, "", 8, 2, Date.Now)
                                End If

                                If drwLCStaus.IsTelephoneHomeNull = False AndAlso drwLCStaus.TelephoneHome.Trim <> "" Then
                                    qryInput.spr_INPUT_Insert(drwLCStaus.TelephoneHome, "T1", drwLCStaus.LoanNumber, drwLCStaus.AmounDefferd, drwLCStaus.NotPiadDurationDay, drwLCStaus.NotPiadDurationDay, drwLCStaus.BrnachCode, Nothing, Nothing, Nothing, Nothing, 0, "pending", True, False, Nothing, Nothing, Nothing, Nothing, Nothing, "", Nothing, drwLCStaus.FName & " " & drwLCStaus.LName, "وام گیرنده", Nothing, Nothing, Nothing)
                                    Dim qryWarningNotificationLogDetail As New BusinessObject.dstWarningNotificationLogDetailTableAdapters.QueriesTableAdapter
                                    qryWarningNotificationLogDetail.spr_WarningNotificationLogDetail_Insert(intWarningNotifcationLogID, drwSystemSetting.TelephoneNumber, drwLCStaus.TelephoneHome, True, "تماس تلفنی با تلفن منزل وام گیرنده", "", Date.Now, "", 8, 2, Date.Now)
                                End If

                                If drwLCStaus.IsTelephoneWorkNull = False AndAlso drwLCStaus.TelephoneWork.Trim <> "" Then
                                    qryInput.spr_INPUT_Insert(drwLCStaus.TelephoneWork, "T1", drwLCStaus.LoanNumber, drwLCStaus.AmounDefferd, drwLCStaus.NotPiadDurationDay, drwLCStaus.NotPiadDurationDay, drwLCStaus.BrnachCode, Nothing, Nothing, Nothing, Nothing, 0, "pending", True, False, Nothing, Nothing, Nothing, Nothing, Nothing, "", Nothing, drwLCStaus.FName & " " & drwLCStaus.LName, "وام گیرنده", Nothing, Nothing, Nothing)
                                    Dim qryWarningNotificationLogDetail As New BusinessObject.dstWarningNotificationLogDetailTableAdapters.QueriesTableAdapter
                                    qryWarningNotificationLogDetail.spr_WarningNotificationLogDetail_Insert(intWarningNotifcationLogID, drwSystemSetting.TelephoneNumber, drwLCStaus.TelephoneWork, True, "تماس تلفنی با تلفن محل کار وام گیرنده", "", Date.Now, "", 8, 2, Date.Now)
                                End If




                            End If
                            If drwWarningIntervalCheck.ToSponsor = True Then



                                Dim tadpSponsorList As New BusinessObject.dstLoanSponsorTableAdapters.spr_LoanSponsor_List_SelectTableAdapter
                                Dim dtblSponsorList As BusinessObject.dstLoanSponsor.spr_LoanSponsor_List_SelectDataTable = Nothing

                                dtblSponsorList = tadpSponsorList.GetData(drwLCStaus.FK_LoanID)

                                For Each drwSponsorList As BusinessObject.dstLoanSponsor.spr_LoanSponsor_List_SelectRow In dtblSponsorList.Rows



                                    Dim qryInput As New BusinessObject.dstInputTableAdapters.QueriesTableAdapter


                                    If drwSponsorList.IsMobileNoNull = False AndAlso drwSponsorList.MobileNo.Trim <> "" Then
                                        qryInput.spr_INPUT_Insert(drwSponsorList.MobileNo, "T3", drwLCStaus.LoanNumber, drwLCStaus.AmounDefferd, drwLCStaus.NotPiadDurationDay, drwLCStaus.NotPiadDurationDay, drwLCStaus.BrnachCode, Nothing, Nothing, Nothing, Nothing, 0, "pending", True, False, Nothing, Nothing, Nothing, Nothing, Nothing, "", Nothing, drwSponsorList.FName & " " & drwSponsorList.LName, "ضامن", Nothing, Nothing, Nothing)
                                        Dim qryWarningNotificationLogDetail As New BusinessObject.dstWarningNotificationLogDetailTableAdapters.QueriesTableAdapter
                                        qryWarningNotificationLogDetail.spr_WarningNotificationLogDetail_Insert(intWarningNotifcationLogID, drwSystemSetting.TelephoneNumber, drwSponsorList.MobileNo, False, "تماس تلفنی با موبایل ضامن", "", Date.Now, "", 8, 2, Date.Now)
                                    End If

                                    If drwSponsorList.IsTelephoneHomeNull = False AndAlso drwSponsorList.TelephoneHome.Trim <> "" Then
                                        qryInput.spr_INPUT_Insert(drwSponsorList.TelephoneHome, "T3", drwLCStaus.LoanNumber, drwLCStaus.AmounDefferd, drwLCStaus.NotPiadDurationDay, drwLCStaus.NotPiadDurationDay, drwLCStaus.BrnachCode, Nothing, Nothing, Nothing, Nothing, 0, "pending", True, False, Nothing, Nothing, Nothing, Nothing, Nothing, "", Nothing, drwSponsorList.FName & " " & drwSponsorList.LName, "ضامن", Nothing, Nothing, Nothing)
                                        Dim qryWarningNotificationLogDetail As New BusinessObject.dstWarningNotificationLogDetailTableAdapters.QueriesTableAdapter
                                        qryWarningNotificationLogDetail.spr_WarningNotificationLogDetail_Insert(intWarningNotifcationLogID, drwSystemSetting.TelephoneNumber, drwSponsorList.TelephoneHome, False, "تماس تلفنی با تلفن منزل ضامن", "", Date.Now, "", 8, 2, Date.Now)
                                    End If

                                    If drwSponsorList.IsTelephoneWorkNull = False AndAlso drwSponsorList.TelephoneWork.Trim <> "" Then
                                        qryInput.spr_INPUT_Insert(drwSponsorList.TelephoneWork, "T3", drwLCStaus.LoanNumber, drwLCStaus.AmounDefferd, drwLCStaus.NotPiadDurationDay, drwLCStaus.NotPiadDurationDay, drwLCStaus.BrnachCode, Nothing, Nothing, Nothing, Nothing, 0, "pending", True, False, Nothing, Nothing, Nothing, Nothing, Nothing, "", Nothing, drwSponsorList.FName & " " & drwSponsorList.LName, "ضامن", Nothing, Nothing, Nothing)
                                        Dim qryWarningNotificationLogDetail As New BusinessObject.dstWarningNotificationLogDetailTableAdapters.QueriesTableAdapter
                                        qryWarningNotificationLogDetail.spr_WarningNotificationLogDetail_Insert(intWarningNotifcationLogID, drwSystemSetting.TelephoneNumber, drwSponsorList.TelephoneWork, False, "تماس تلفنی با تلفن محل کار ضامن", "", Date.Now, "", 8, 2, Date.Now)
                                    End If


                                Next drwSponsorList

                            End If


                        End If
LetterL:

                        If drwWarningIntervalCheck.IssueIntroductionLetter = True Then

                            If drwWarningIntervalCheck.ToBorrower = True Then
                                Dim strMessage As String = ""

                                strMessage = CreateMessage(3, drwLCStaus.IsMale, drwLCStaus.FName, drwLCStaus.LName, drwLCStaus.LoanNumber, drwLCStaus.NotPiadDurationDay, False, drwWarningIntervalCheck.ID, drwLCStaus.BranchName, "", "", True)


                                Dim qryWarningNotificationLogDetail As New BusinessObject.dstWarningNotificationLogDetailTableAdapters.QueriesTableAdapter
                                qryWarningNotificationLogDetail.spr_WarningNotificationLogDetail_Insert(intWarningNotifcationLogID, "", "", True, strMessage, "", Date.Now, "", 5, 3, Nothing)


                            End If
                            If drwWarningIntervalCheck.ToSponsor = True Then


                                Dim tadpSponsorList As New BusinessObject.dstLoanSponsorTableAdapters.spr_LoanSponsor_List_SelectTableAdapter
                                Dim dtblSponsorList As BusinessObject.dstLoanSponsor.spr_LoanSponsor_List_SelectDataTable = Nothing

                                dtblSponsorList = tadpSponsorList.GetData(drwLCStaus.FK_LoanID)

                                For Each drwSponsorList As BusinessObject.dstLoanSponsor.spr_LoanSponsor_List_SelectRow In dtblSponsorList.Rows


                                    Dim strMessage As String = ""

                                    strMessage = CreateMessage(3, drwLCStaus.IsMale, drwLCStaus.FName, drwLCStaus.LName, drwLCStaus.LoanNumber, drwLCStaus.NotPiadDurationDay, True, drwWarningIntervalCheck.ID, drwLCStaus.BranchName, drwSponsorList.FName, drwSponsorList.LName, drwSponsorList.IsMale)


                                    Dim qryWarningNotificationLogDetail As New BusinessObject.dstWarningNotificationLogDetailTableAdapters.QueriesTableAdapter
                                    qryWarningNotificationLogDetail.spr_WarningNotificationLogDetail_Insert(intWarningNotifcationLogID, "", "", False, strMessage, "", Date.Now, "", 5, 3, Nothing)


                                Next drwSponsorList


                            End If


                        End If

                        If drwWarningIntervalCheck.IssueNotice = True Then

                            If drwWarningIntervalCheck.ToBorrower = True Then
                                Dim strMessage As String = ""

                                strMessage = CreateMessage(4, drwLCStaus.IsMale, drwLCStaus.FName, drwLCStaus.LName, drwLCStaus.LoanNumber, drwLCStaus.NotPiadDurationDay, False, drwWarningIntervalCheck.ID, drwLCStaus.BranchName, "", "", True)


                                Dim qryWarningNotificationLogDetail As New BusinessObject.dstWarningNotificationLogDetailTableAdapters.QueriesTableAdapter
                                qryWarningNotificationLogDetail.spr_WarningNotificationLogDetail_Insert(intWarningNotifcationLogID, "", "", True, strMessage, "", Date.Now, "", 5, 4, Nothing)

                            End If
                            If drwWarningIntervalCheck.ToSponsor = True Then


                                Dim tadpSponsorList As New BusinessObject.dstLoanSponsorTableAdapters.spr_LoanSponsor_List_SelectTableAdapter
                                Dim dtblSponsorList As BusinessObject.dstLoanSponsor.spr_LoanSponsor_List_SelectDataTable = Nothing

                                dtblSponsorList = tadpSponsorList.GetData(drwLCStaus.FK_LoanID)

                                For Each drwSponsorList As BusinessObject.dstLoanSponsor.spr_LoanSponsor_List_SelectRow In dtblSponsorList.Rows


                                    Dim strMessage As String = ""

                                    strMessage = CreateMessage(4, drwLCStaus.IsMale, drwLCStaus.FName, drwLCStaus.LName, drwLCStaus.LoanNumber, drwLCStaus.NotPiadDurationDay, True, drwWarningIntervalCheck.ID, drwLCStaus.BranchName, drwSponsorList.FName, drwSponsorList.LName, drwSponsorList.IsMale)


                                    Dim qryWarningNotificationLogDetail As New BusinessObject.dstWarningNotificationLogDetailTableAdapters.QueriesTableAdapter
                                    qryWarningNotificationLogDetail.spr_WarningNotificationLogDetail_Insert(intWarningNotifcationLogID, "", "", False, strMessage, "", Date.Now, "", 5, 4, Nothing)


                                Next drwSponsorList


                            End If


                        End If

                        If drwWarningIntervalCheck.IssueManifest = True Then

                            If drwWarningIntervalCheck.ToBorrower = True Then
                                Dim strMessage As String = ""

                                strMessage = CreateMessage(5, drwLCStaus.IsMale, drwLCStaus.FName, drwLCStaus.LName, drwLCStaus.LoanNumber, drwLCStaus.NotPiadDurationDay, False, drwWarningIntervalCheck.ID, drwLCStaus.BranchName, "", "", True)


                                Dim qryWarningNotificationLogDetail As New BusinessObject.dstWarningNotificationLogDetailTableAdapters.QueriesTableAdapter
                                qryWarningNotificationLogDetail.spr_WarningNotificationLogDetail_Insert(intWarningNotifcationLogID, "", "", True, strMessage, "", Date.Now, "", 5, 5, Nothing)

                            End If
                            If drwWarningIntervalCheck.ToSponsor = True Then


                                Dim tadpSponsorList As New BusinessObject.dstLoanSponsorTableAdapters.spr_LoanSponsor_List_SelectTableAdapter
                                Dim dtblSponsorList As BusinessObject.dstLoanSponsor.spr_LoanSponsor_List_SelectDataTable = Nothing

                                dtblSponsorList = tadpSponsorList.GetData(drwLCStaus.FK_LoanID)

                                For Each drwSponsorList As BusinessObject.dstLoanSponsor.spr_LoanSponsor_List_SelectRow In dtblSponsorList.Rows


                                    Dim strMessage As String = ""

                                    strMessage = CreateMessage(5, drwLCStaus.IsMale, drwLCStaus.FName, drwLCStaus.LName, drwLCStaus.LoanNumber, drwLCStaus.NotPiadDurationDay, True, drwWarningIntervalCheck.ID, drwLCStaus.BranchName, drwSponsorList.FName, drwSponsorList.LName, drwSponsorList.IsMale)


                                    Dim qryWarningNotificationLogDetail As New BusinessObject.dstWarningNotificationLogDetailTableAdapters.QueriesTableAdapter
                                    qryWarningNotificationLogDetail.spr_WarningNotificationLogDetail_Insert(intWarningNotifcationLogID, "", "", False, strMessage, "", Date.Now, "", 5, 5, Nothing)


                                Next drwSponsorList


                            End If


                        End If

                    Catch ex As Exception

                        SendTestSMS("Mehr Vosul For Error:" & ex.Message)


                        Exit For

                    End Try

                    '  k = 1


                Next drwLCStaus


                'strmWriter = New IO.StreamWriter(strLogFileName, True)
                'strmWriter.WriteLine("6. End of for Loop")
                'strmWriter.Close()


                Dim lngFirstID As Long = dtblLCStaus.First.ID
                Dim lngLastID As Long = dtblLCStaus.Last.ID

                Dim qryLCStatus As New BusinessObject.dstCurrentLCStatusTableAdapters.QueriesTableAdapter
                qryLCStatus.spr_CurrentLCStatus_Process_Update(lngFirstID, lngLastID)



                'strmWriter = New IO.StreamWriter(strLogFileName, True)
                'strmWriter.WriteLine("7. ""Process"" field updated: " & lngFirstID.ToString & "-" & lngLastID.ToString)
                'strmWriter.Close()

                ''Dim tadpSMSCount As New BusinessObject.dstWarningNotificationLogDetailTableAdapters.spr_WarningNotificationLogDetail_SMSCount_SelectTableAdapter
                ''Dim dtblSMSCount As BusinessObject.dstWarningNotificationLogDetail.spr_WarningNotificationLogDetail_SMSCount_SelectDataTable = Nothing
                ''dtblSMSCount = tadpSMSCount.GetData(Date.Now.Date)
                ''Dim drwSMSCount As BusinessObject.dstWarningNotificationLogDetail.spr_WarningNotificationLogDetail_SMSCount_SelectRow = dtblSMSCount.Rows(0)


                ''strmWriter = New IO.StreamWriter(strLogFileName, True)
                ''strmWriter.WriteLine("8. Total SMS sent Count for today: " & drwSMSCount.Expr1)
                ''strmWriter.WriteLine("9. End.")

                ''strmWriter.Close()






            Catch ex As Exception

                SendTestSMS("Mehr Vosul DO Error:" & ex.Message)

                Threading.Thread.Sleep(5000)
                GC.Collect()
                '' Exit Do
            End Try

        Loop



    End Sub


    '    Private Sub SendHadiNotification_Deposit()



    '        Do

    '            Try

    '                Dim tadpLCStatus As New BusinessObject.dstHadiOperation_DepositTableAdapters.spr_HadiOperationDeposit_List_SelectTableAdapter
    '                Dim dtblLCStaus As BusinessObject.dstHadiOperation_Deposit.spr_HadiOperationDeposit_List_SelectDataTable = Nothing
    '                dtblLCStaus = tadpLCStatus.GetData()

    '                If dtblLCStaus.Rows.Count = 0 Then
    '                    Threading.Thread.Sleep(5000)
    '                    Continue Do
    '                End If



    '                For Each drwLCStaus As BusinessObject.dstHadiOperation_Deposit.spr_HadiOperationDeposit_List_SelectRow In dtblLCStaus.Rows
    '                    Try

    '                        Dim tadpWarningIntervalCheck As New BusinessObject.dstHadiWarningIntervalsTableAdapters.spr_HadiWarningIntervals_Check_SelectTableAdapter
    '                        Dim dtblWarningIntervalCheck As BusinessObject.dstHadiWarningIntervals.spr_HadiWarningIntervals_Check_SelectDataTable = Nothing

    '                        dtblWarningIntervalCheck = tadpWarningIntervalCheck.GetData(drwLCStaus.FK_DepositTypeID, drwLCStaus.CFCRDateDiff, -1, drwLCStaus.FK_BracnhID)

    '                        If dtblWarningIntervalCheck.Rows.Count = 0 Then
    '                            Continue For

    '                        End If


    '                        Dim drwWarningIntervalCheck As BusinessObject.dstHadiWarningIntervals.spr_HadiWarningIntervals_Check_SelectRow = dtblWarningIntervalCheck.Rows(0)

    '                        Dim qryWarningNotificationLog As New BusinessObject.dstHadiWarningNotificationLogTableAdapters.QueriesTableAdapter
    '                        Dim intWarningNotifcationLogID As Integer = qryWarningNotificationLog.spr_HadiWarningNotificationLog_Insert(drwLCStaus.FK_FileID, drwWarningIntervalCheck.ID, drwLCStaus.Date_p, 1, Date.Now, False)

    '                        If drwWarningIntervalCheck.SendSMS = True Then

    '                            If drwLCStaus.IsMobileNoNull = True OrElse drwLCStaus.MobileNo.Length <> 11 Then
    '                                ''  GoTo Telephone
    '                            End If


    '                            Dim strMessage As String = ""

    '                            strMessage = CreateHadiMessage(drwLCStaus.IsMale, drwLCStaus.FName, drwLCStaus.LName, drwLCStaus.CustomerNo, drwLCStaus.CFCRDateDiff, drwWarningIntervalCheck.ID, drwLCStaus.BranchName, drwLCStaus.DepositName)
    '                            Dim qryWarningNotificationLogDetail As New BusinessObject.dstHadiWarningNotificationLogDetailTableAdapters.QueriesTableAdapter
    '                            qryWarningNotificationLogDetail.spr_HadiWarningNotificationLogDetail_Insert(intWarningNotifcationLogID, drwSystemSetting.GatewayNumber, drwLCStaus.MobileNo, strMessage, "", Date.Now, "", 1, 1, Date.Now)


    '                        End If

    'VoiceSMS:

    '                        ''If drwWarningIntervalCheck.VoiceMessage = True Then

    '                        ''    If drwLCStaus.IsMobileNoNull = False AndAlso drwLCStaus.MobileNo.Trim <> "" Then

    '                        ''        Dim arrTo() As String = {drwLCStaus.MobileNo}
    '                        ''        Dim arrRecords() As String = Nothing
    '                        ''        Dim arrNumbers() As String = Nothing
    '                        ''        Dim strSayMethod As String = "9"
    '                        ''        Dim strVoiceSMS_Name As String = "VoiceSMS_" & Date.Now.Millisecond
    '                        ''        GetVoiceSMSArrays_Vesal(drwWarningIntervalCheck.ID, False, arrRecords, arrNumbers)

    '                        ''        If arrRecords Is Nothing AndAlso arrNumbers Is Nothing Then
    '                        ''            Continue For
    '                        ''        End If

    '                        ''        For k As Integer = 0 To arrNumbers.Length - 1

    '                        ''            If arrNumbers(k) = 1 Then
    '                        ''                arrNumbers(k) = Val(drwLCStaus.DepositNumber.Replace("-", ""))
    '                        ''            Else
    '                        ''                arrNumbers(k) = Val(drwLCStaus.CFCRDateDiff)
    '                        ''            End If
    '                        ''        Next k


    '                        ''        SendVoiceMixedSMS(drwSystemSetting.VoiceSMSUID, drwSystemSetting.VoiceSMSToken, strVoiceSMS_Name, arrTo, arrRecords, arrNumbers, strSayMethod)

    '                        ''        Dim qryWarningNotificationLogDetail As New BusinessObject.dstHadiWarningNotificationLogDetailTableAdapters.QueriesTableAdapter
    '                        ''        qryWarningNotificationLogDetail.spr_HadiWarningNotificationLogDetail_Insert(intWarningNotifcationLogID, "Voice SMS", drwLCStaus.MobileNo, "ارسال پیامک صوتی به موبایل وام گیرنده", "", Date.Now, "", 6, 8, Date.Now)


    '                        ''    End If

    '                        ''    If drwLCStaus.IsTelephoneHomeNull = False AndAlso drwLCStaus.TelephoneHome.Trim <> "" Then

    '                        ''        Dim arrTo() As String = {drwLCStaus.TelephoneHome}
    '                        ''        Dim arrRecords() As String = Nothing
    '                        ''        Dim arrNumbers() As String = Nothing
    '                        ''        Dim strSayMethod As String = "9"
    '                        ''        Dim strVoiceSMS_Name As String = "VoiceSMS_" & Date.Now.Millisecond
    '                        ''        GetVoiceSMSArrays_Vesal(drwWarningIntervalCheck.ID, False, arrRecords, arrNumbers)

    '                        ''        If arrRecords Is Nothing AndAlso arrNumbers Is Nothing Then
    '                        ''            Continue For
    '                        ''        End If

    '                        ''        For k As Integer = 0 To arrNumbers.Length - 1

    '                        ''            If arrNumbers(k) = 1 Then
    '                        ''                arrNumbers(k) = Val(drwLCStaus.DepositNumber.Replace("-", ""))
    '                        ''            Else
    '                        ''                arrNumbers(k) = Val(drwLCStaus.CFCRDateDiff)
    '                        ''            End If
    '                        ''        Next k


    '                        ''        SendVoiceMixedSMS(drwSystemSetting.VoiceSMSUID, drwSystemSetting.VoiceSMSToken, strVoiceSMS_Name, arrTo, arrRecords, arrNumbers, strSayMethod)

    '                        ''        Dim qryWarningNotificationLogDetail As New BusinessObject.dstHadiWarningNotificationLogDetailTableAdapters.QueriesTableAdapter
    '                        ''        qryWarningNotificationLogDetail.spr_HadiWarningNotificationLogDetail_Insert(intWarningNotifcationLogID, "Voice SMS", drwLCStaus.TelephoneHome, "ارسال پیامک صوتی به تلفن منزل وام گیرنده", "", Date.Now, "", 6, 8, Date.Now)


    '                        ''    End If

    '                        ''    If drwLCStaus.IsTelephoneWorkNull = False AndAlso drwLCStaus.TelephoneWork.Trim <> "" Then


    '                        ''        Dim arrTo() As String = {drwLCStaus.TelephoneWork}
    '                        ''        Dim arrRecords() As String = Nothing
    '                        ''        Dim arrNumbers() As String = Nothing
    '                        ''        Dim strSayMethod As String = "9"
    '                        ''        Dim strVoiceSMS_Name As String = "VoiceSMS_" & Date.Now.Millisecond
    '                        ''        GetVoiceSMSArrays_Vesal(drwWarningIntervalCheck.ID, False, arrRecords, arrNumbers)

    '                        ''        If arrRecords Is Nothing AndAlso arrNumbers Is Nothing Then
    '                        ''            Continue For
    '                        ''        End If

    '                        ''        For k As Integer = 0 To arrNumbers.Length - 1

    '                        ''            If arrNumbers(k) = 1 Then
    '                        ''                arrNumbers(k) = Val(drwLCStaus.DepositNumber.Replace("-", ""))
    '                        ''            Else
    '                        ''                arrNumbers(k) = Val(drwLCStaus.CFCRDateDiff)
    '                        ''            End If
    '                        ''        Next k


    '                        ''        SendVoiceMixedSMS(drwSystemSetting.VoiceSMSUID, drwSystemSetting.VoiceSMSToken, strVoiceSMS_Name, arrTo, arrRecords, arrNumbers, strSayMethod)

    '                        ''        Dim qryWarningNotificationLogDetail As New BusinessObject.dstHadiWarningNotificationLogDetailTableAdapters.QueriesTableAdapter
    '                        ''        qryWarningNotificationLogDetail.spr_HadiWarningNotificationLogDetail_Insert(intWarningNotifcationLogID, "Voice SMS", drwLCStaus.TelephoneWork, "ارسال پیامک صوتی به تلفن محل کار وام گیرنده", "", Date.Now, "", 6, 8, Date.Now)


    '                        ''    End If

    '                        '' End If


    '                    Catch ex As Exception

    '                        SendTestSMS("Mehr Vosul For Error:" & ex.Message)


    '                        Exit For

    '                    End Try

    '                    '  k = 1


    '                Next drwLCStaus



    '                Dim lngFirstID As Long = dtblLCStaus.First.ID
    '                Dim lngLastID As Long = dtblLCStaus.Last.ID

    '                Dim qryLCStatus As New BusinessObject.dstHadiOperation_DepositTableAdapters.QueriesTableAdapter
    '                qryLCStatus.spr_HadiOperationDeposit_Process_Update(lngFirstID, lngLastID)





    '            Catch ex As Exception

    '                SendTestSMS("Mehr Vosul DO Error:" & ex.Message)

    '                Threading.Thread.Sleep(5000)
    '                GC.Collect()
    '                '' Exit Do
    '            End Try

    '        Loop



    '    End Sub

    'Private Sub SendHadiNotification_Loan()


    '    Do

    '        Try

    '            Dim tadpLCStatus As New BusinessObject.dstHadiOperation_LoanTableAdapters.spr_HadiOperationLoan_List_SelectTableAdapter
    '            Dim dtblLCStaus As BusinessObject.dstHadiOperation_Loan.spr_HadiOperationLoan_List_SelectDataTable = Nothing
    '            dtblLCStaus = tadpLCStatus.GetData()

    '            If dtblLCStaus.Rows.Count = 0 Then
    '                Threading.Thread.Sleep(5000)
    '                Continue Do
    '            End If



    '            For Each drwLCStaus As BusinessObject.dstHadiOperation_Loan.spr_HadiOperationLoan_List_SelectRow In dtblLCStaus.Rows
    '                Try

    '                    Dim tadpWarningIntervalCheck As New BusinessObject.dstHadiWarningIntervalsTableAdapters.spr_HadiWarningIntervals_Check_SelectTableAdapter
    '                    Dim dtblWarningIntervalCheck As BusinessObject.dstHadiWarningIntervals.spr_HadiWarningIntervals_Check_SelectDataTable = Nothing

    '                    dtblWarningIntervalCheck = tadpWarningIntervalCheck.GetData(2, drwLCStaus.FK_LoanTypeID, drwLCStaus.CFCRDateDiff, drwLCStaus.FK_branchID)

    '                    If dtblWarningIntervalCheck.Rows.Count = 0 Then
    '                        Continue For

    '                    End If


    '                    Dim drwWarningIntervalCheck As BusinessObject.dstHadiWarningIntervals.spr_HadiWarningIntervals_Check_SelectRow = dtblWarningIntervalCheck.Rows(0)

    '                    Dim qryWarningNotificationLog As New BusinessObject.dstHadiWarningNotificationLogTableAdapters.QueriesTableAdapter
    '                    Dim intWarningNotifcationLogID As Integer = qryWarningNotificationLog.spr_HadiWarningNotificationLog_Insert(drwLCStaus.FK_FileID, drwWarningIntervalCheck.ID, drwLCStaus.Date_P, 1, Date.Now, False)

    '                    If drwWarningIntervalCheck.SendSMS = True Then

    '                        If drwLCStaus.IsMobileNoNull = True OrElse drwLCStaus.MobileNo.Length <> 11 Then
    '                            ''  GoTo Telephone
    '                        End If


    '                        Dim strMessage As String = ""

    '                        strMessage = CreateHadiMessage(drwLCStaus.IsMale, drwLCStaus.FName, drwLCStaus.LName, drwLCStaus.CustomerNo, drwLCStaus.CFCRDateDiff, drwWarningIntervalCheck.ID, drwLCStaus.BranchName, drwLCStaus.LoanTypeName, drwLCStaus.LoanNumber, mdlGeneral.GetPersianDate(drwLCStaus.LCDate).ToString())
    '                        Dim qryWarningNotificationLogDetail As New BusinessObject.dstHadiWarningNotificationLogDetailTableAdapters.QueriesTableAdapter
    '                        qryWarningNotificationLogDetail.spr_HadiWarningNotificationLogDetail_Insert(intWarningNotifcationLogID, drwSystemSetting.GatewayNumber, drwLCStaus.MobileNo, strMessage, "", Date.Now, "", 1, 1, Date.Now)


    '                    End If


    '                Catch ex As Exception

    '                    SendTestSMS("Mehr Vosul For Error:" & ex.Message)


    '                    Exit For

    '                End Try

    '                '  k = 1


    '            Next drwLCStaus



    '            Dim lngFirstID As Long = dtblLCStaus.First.ID
    '            Dim lngLastID As Long = dtblLCStaus.Last.ID

    '            Dim qryLCStatus As New BusinessObject.dstHadiOperation_LoanTableAdapters.QueriesTableAdapter
    '            qryLCStatus.spr_HadiOperationLoan_Process_Update(lngFirstID, lngLastID)





    '        Catch ex As Exception

    '            SendTestSMS("Mehr Vosul DO Error:" & ex.Message)

    '            Threading.Thread.Sleep(5000)
    '            GC.Collect()
    '            '' Exit Do
    '        End Try

    '    Loop



    'End Sub


    Private Sub tmrSponsorList_Elapsed(ByVal sender As System.Object, ByVal e As System.Timers.ElapsedEventArgs) Handles tmrSponsorList.Elapsed


        ''  Checked
        If Date.Now.DayOfWeek <> DayOfWeek.Friday Then
            Return
        End If

        Dim tadpSponsorLog As New BusinessObject.dst_Sponsor_List_LogTableAdapters.spr_Sponsor_List_Log_Last_SelectTableAdapter
        Dim dtblSponsorLog As BusinessObject.dst_Sponsor_List_Log.spr_Sponsor_List_Log_Last_SelectDataTable = Nothing
        dtblSponsorLog = tadpSponsorLog.GetData()

        If dtblSponsorLog.Rows.Count > 0 Then
            Dim drwSponsorLog As BusinessObject.dst_Sponsor_List_Log.spr_Sponsor_List_Log_Last_SelectRow = dtblSponsorLog.Rows(0)
            If drwSponsorLog.theDay = Date.Now.Date Then
                Return
            End If
        End If


        Dim qrySponsorLog As New BusinessObject.dst_Sponsor_List_LogTableAdapters.QueriesTableAdapter
        Dim intLogID As Integer = qrySponsorLog.spr_Sponsor_List_Log_Insert(Date.Now.Date, Date.Now)



        Dim cnnBuiler_BI As New OracleConnectionStringBuilder()
            cnnBuiler_BI.DataSource = "10.35.1.37:1522/bidb"
            cnnBuiler_BI.UserID = "deposit"
            cnnBuiler_BI.Password = "deposit"
            cnnBuiler_BI.Unicode = True

            Using cnnBI_Connection As New OracleConnection(cnnBuiler_BI.ConnectionString)

                Dim cmd_BI As OracleCommand = cnnBI_Connection.CreateCommand()

                Dim strSponsorList As String = "SELECT * from KWV_NRB_LOAN_STARS2"

                cmd_BI.CommandText = strSponsorList

                Try
                    cnnBI_Connection.Open()
                Catch ex As Exception

                    Dim qryErrorLog As New DataSet1TableAdapters.QueriesTableAdapter
                qryErrorLog.spr_ErrorLog_Insert(ex.Message, 3, "tmrSponsorList_Elapsed_cnnBI_Connection")

                qrySponsorLog.spr_Sponsor_List_Log_Delete(intLogID)
                    Return
                End Try


                Dim qrySposorList As New BusinessObject.dstSponsor_ListTableAdapters.QueriesTableAdapter
            Try

                qrySposorList.spr_Sponsor_List_Delete()
            Catch ex As Exception
                Dim qryErrorLog As New DataSet1TableAdapters.QueriesTableAdapter
                qryErrorLog.spr_ErrorLog_Insert(ex.Message, 3, "tmrSponsorList_Elapsed_spr_Sponsor_List_Delete")
                Return
            End Try



                Dim dataReader As OracleDataReader = Nothing
                dataReader = cmd_BI.ExecuteReader()

                Dim i As Integer = 0
                Dim strBuilder As New Text.StringBuilder()
                Do While dataReader.Read
                    Try

                        Dim stcVarSponsorInfo As stc_Sponsor_Info = Nothing
                        i += 1

                        If dataReader.GetValue(0) Is DBNull.Value Then
                            i -= 1
                            Continue Do
                        Else
                            stcVarSponsorInfo.BranchCode = CStr(dataReader.GetValue(0)).Trim
                        End If

                        If dataReader.GetValue(1) Is DBNull.Value Then
                            i -= 1
                            Continue Do
                        Else
                            stcVarSponsorInfo.LoanTypeCode = CStr(dataReader.GetValue(1)).Trim
                        End If

                        If dataReader.GetValue(2) Is DBNull.Value Then
                            i -= 1
                            Continue Do
                        Else
                            stcVarSponsorInfo.BorrowerCustomerNo = CStr(dataReader.GetValue(2)).Trim
                        End If

                        If dataReader.GetValue(3) Is DBNull.Value Then
                            i -= 1
                            Continue Do
                        Else
                            stcVarSponsorInfo.LoanSerial = CInt(dataReader.GetValue(3))
                        End If

                        'If dataReader.GetValue(4) Is DBNull.Value Then
                        '    stcVarSponsorInfo.LCNo = ""
                        'Else
                        '    stcVarSponsorInfo.LCNo = CStr(dataReader.GetValue(4)).Trim
                        'End If

                        If dataReader.GetValue(5) Is DBNull.Value Then
                            stcVarSponsorInfo.FullName = ""
                        Else
                            stcVarSponsorInfo.FullName = CStr(dataReader.GetValue(5)).Replace("'", "")
                        End If

                        If dataReader.GetValue(6) Is DBNull.Value Then
                            stcVarSponsorInfo.FatherName = ""
                        Else
                            stcVarSponsorInfo.FatherName = CStr(dataReader.GetValue(6)).Replace("'", "")
                        End If

                        If dataReader.GetValue(7) Is DBNull.Value Then
                            i -= 1
                            Continue Do
                        Else
                            stcVarSponsorInfo.SponsorCustomerNo = CStr(dataReader.GetValue(7)).Trim
                        End If

                        If dataReader.GetValue(8) Is DBNull.Value Then
                            stcVarSponsorInfo.NationalNo = ""
                        Else
                            stcVarSponsorInfo.NationalNo = CStr(dataReader.GetValue(8)).Replace("'", "")
                        End If

                        If dataReader.GetValue(9) Is DBNull.Value Then
                            stcVarSponsorInfo.NationalID = ""
                        Else
                            stcVarSponsorInfo.NationalID = CStr(dataReader.GetValue(9)).Replace("'", "")
                        End If

                        If dataReader.GetValue(10) Is DBNull.Value Then
                            stcVarSponsorInfo.HomeTel = ""
                        Else
                            stcVarSponsorInfo.HomeTel = CStr(dataReader.GetValue(10)).Trim.Replace("'", "")
                        End If

                        If dataReader.GetValue(11) Is DBNull.Value Then
                            stcVarSponsorInfo.WorkTel = ""
                        Else
                            stcVarSponsorInfo.WorkTel = CStr(dataReader.GetValue(11)).Trim.Replace("'", "")
                        End If

                        If dataReader.GetValue(12) Is DBNull.Value Then
                            stcVarSponsorInfo.Mobile = ""
                        Else
                            stcVarSponsorInfo.Mobile = CStr(dataReader.GetValue(12)).Trim.Replace("'", "")
                        End If

                        If dataReader.GetValue(13) Is DBNull.Value Then
                            stcVarSponsorInfo.HomeAddress = ""
                        Else
                            stcVarSponsorInfo.HomeAddress = CStr(dataReader.GetValue(13)).Replace("'", "")
                        End If

                        If dataReader.GetValue(14) Is DBNull.Value Then
                            stcVarSponsorInfo.WorkAddress = ""
                        Else
                            stcVarSponsorInfo.WorkAddress = CStr(dataReader.GetValue(14)).Replace("'", "")
                        End If

                        If dataReader.GetValue(15) Is DBNull.Value Then
                            stcVarSponsorInfo.WarantyTypeDesc = ""
                        Else
                            stcVarSponsorInfo.WarantyTypeDesc = CStr(dataReader.GetValue(15)).Trim.Replace("'", "")
                        End If

                        stcVarSponsorInfo.UpdateDate = ""

                        'If dataReader.GetValue(16) Is DBNull.Value Then
                        '    stcVarSponsorInfo.UpdateDate = ""
                        'Else
                        '    stcVarSponsorInfo.UpdateDate = CStr(dataReader.GetValue(16)).Trim
                        'End If



                        Dim strTempSelectQuery As String = " union select '" & stcVarSponsorInfo.BranchCode & "','" & stcVarSponsorInfo.LoanTypeCode & "','" & stcVarSponsorInfo.BorrowerCustomerNo & "'," & stcVarSponsorInfo.LoanSerial
                        strTempSelectQuery &= ",'" & stcVarSponsorInfo.SponsorCustomerNo & "','" & stcVarSponsorInfo.FullName & "','" & stcVarSponsorInfo.FatherName & "','" & stcVarSponsorInfo.Mobile & "','" & stcVarSponsorInfo.NationalID
                        strTempSelectQuery &= "','" & stcVarSponsorInfo.NationalNo & "','" & stcVarSponsorInfo.WorkAddress & "/" & stcVarSponsorInfo.HomeAddress & "','" & stcVarSponsorInfo.HomeTel & "','" & stcVarSponsorInfo.WorkTel & "','" & stcVarSponsorInfo.WarantyTypeDesc & "'"

                        strBuilder.Append(strTempSelectQuery)


                        If i >= 700 Then
                            Dim strMainInsertQuery As String = strBuilder.ToString.Substring(7)
                            qrySposorList.spr_Sponsor_List_Bulk_Insert(strMainInsertQuery)

                            i = 0
                            strBuilder.Clear()


                        End If
                    Catch ex As Exception
                        Dim qryErrorLog As New DataSet1TableAdapters.QueriesTableAdapter
                    qryErrorLog.spr_ErrorLog_Insert(ex.Message, 3, "tmrSponsorList_Elapsed_Interal Loop")
                    Continue Do
                    End Try


                Loop



                dataReader.Close()
                cnnBI_Connection.Close()

                If i <> 0 Then
                    Dim strMainInsertQuery As String = strBuilder.ToString.Substring(6)
                    Try
                        qrySposorList.spr_Sponsor_List_Bulk_Insert(strMainInsertQuery)

                    Catch ex As Exception

                        Dim qryErrorLog As New DataSet1TableAdapters.QueriesTableAdapter
                    qryErrorLog.spr_ErrorLog_Insert(ex.Message, 3, "tmrSponsorList_Elapsed spr_Sponsor_List_Bulk_Insert(external)")

                End Try


                End If


            End Using

            qrySponsorLog.spr_Sponsor_List_Log_Update(intLogID, Date.Now, "Successed")



    End Sub


    Private Sub tmrUpdateData_Elapsed(ByVal sender As System.Object, ByVal e As System.Timers.ElapsedEventArgs) Handles tmrUpdateData.Elapsed
        Try
            Call UpdateBIData()
        Catch ex As Exception

        End Try


    End Sub


#Region "SMS"


    Private Sub SendSMS()

        Do
            Try
                Dim tadpSMSList As New BusinessObject.dstWarningNotificationLogDetailTableAdapters.spr_WarningNotificationLogDetail_NotSend_SMS_ListTableAdapter
                Dim dtblSMSList As BusinessObject.dstWarningNotificationLogDetail.spr_WarningNotificationLogDetail_NotSend_SMS_ListDataTable = Nothing
                dtblSMSList = tadpSMSList.GetData()

                Dim arrSMSMessages() As String = Nothing
                Dim arrSMSDestination() As String = Nothing

                Dim intFirstID As Integer = -1
                If dtblSMSList.Rows.Count = 0 Then
                    Threading.Thread.Sleep(5000)
                    Continue Do
                Else
                    intFirstID = dtblSMSList.First.ID
                End If
                Dim intLastID As Integer = -1

                ''  Dim strInsertQuery As String = "insert into easysms.outbound_messages (from_mobile_number,dest_mobile_number,message_body,due_date,system_type) "

                For Each drwSMSList As BusinessObject.dstWarningNotificationLogDetail.spr_WarningNotificationLogDetail_NotSend_SMS_ListRow In dtblSMSList.Rows

                    If drwSMSList.ReceiverInfo.Length <> 11 Then
                        Continue For
                    End If

                    If arrSMSDestination Is Nothing Then
                        ReDim arrSMSDestination(0)
                        ReDim arrSMSMessages(0)
                    Else
                        ReDim Preserve arrSMSDestination(arrSMSDestination.Length)
                        ReDim Preserve arrSMSMessages(arrSMSMessages.Length)
                    End If
                    arrSMSDestination(arrSMSDestination.Length - 1) = drwSMSList.ReceiverInfo
                    arrSMSMessages(arrSMSMessages.Length - 1) = drwSMSList.strMessage
                    intLastID = drwSMSList.ID


                    ''  strInsertQuery = strInsertQuery & "  select " & "'+98" & drwSMSList.SenderInfo & "', " & "'+98" & drwSMSList.ReceiverInfo & "', '" & drwSMSList.strMessage & "', '" & DateTime.Now.ToString() & "', 2" & "  UNION "


                Next drwSMSList


                ''Execute Query
                ''Dim cnnEasySMS As New MySql.Data.MySqlClient.MySqlConnection
                ''Dim strInstance As String = "127.0.0.1"
                ''Dim strUserID As String = "root"
                ''Dim strPassword As String = "123root"
                ''Dim strDataBase As String = "easysms"
                ''Dim strConnectionStrting As String = "server=" & strInstance & ";port=3306" & ";database=" & strDataBase & ";uid=" & strUserID & ";pwd=" & strPassword & ";"
                ''cnnEasySMS.ConnectionString = strConnectionStrting
                ''Try
                ''    cnnEasySMS.Open()
                ''Catch ex As Exception
                ''    Continue Do
                ''End Try

                ''strInsertQuery = strInsertQuery.Substring(0, strInsertQuery.Length - 6)
                ''Dim cmdInsert As MySqlCommand = New MySqlCommand(strInsertQuery, cnnEasySMS)
                ''cmdInsert.CommandType = CommandType.Text
                ''cmdInsert.ExecuteNonQuery()


                Dim strBatch As String = "MVosul+" & drwSystemSetting.GatewayCompany & "+" & Date.Now.ToString("yyMMddHHmmss") & Date.Now.Millisecond.ToString


                Dim objSMS As New clsSMS
                objSMS.SendSMS_LikeToLike(arrSMSMessages, arrSMSDestination, drwSystemSetting.GatewayUsername, drwSystemSetting.GatewayPassword, drwSystemSetting.GatewayNumber, drwSystemSetting.GatewayIP, drwSystemSetting.GatewayCompany, strBatch)

                Dim qryLogDetail As New BusinessObject.dstWarningNotificationLogDetailTableAdapters.QueriesTableAdapter
                qryLogDetail.spr_WarningNotificationLogDetail_Batch_Update(intFirstID, intLastID, strBatch)

            Catch ex As Exception
                Continue Do
            End Try

        Loop





    End Sub


    ''Private Sub HadiSendSMS()

    ''    Do
    ''        Try
    ''            Dim tadpSMSList As New BusinessObject.dstHadiWarningNotificationLogDetailTableAdapters.spr_HadiWarningNotificationLogDetail_NotSend_SMS_ListTableAdapter
    ''            Dim dtblSMSList As BusinessObject.dstHadiWarningNotificationLogDetail.spr_HadiWarningNotificationLogDetail_NotSend_SMS_ListDataTable = Nothing
    ''            dtblSMSList = tadpSMSList.GetData()

    ''            Dim arrSMSMessages() As String = Nothing
    ''            Dim arrSMSDestination() As String = Nothing

    ''            Dim intFirstID As Integer = -1
    ''            If dtblSMSList.Rows.Count = 0 Then
    ''                Threading.Thread.Sleep(5000)
    ''                Continue Do
    ''            Else
    ''                intFirstID = dtblSMSList.First.ID
    ''            End If
    ''            Dim intLastID As Integer = -1

    ''            ''  Dim strInsertQuery As String = "insert into easysms.outbound_messages (from_mobile_number,dest_mobile_number,message_body,due_date,system_type) "

    ''            For Each drwSMSList As BusinessObject.dstHadiWarningNotificationLogDetail.spr_HadiWarningNotificationLogDetail_NotSend_SMS_ListRow In dtblSMSList.Rows

    ''                If drwSMSList.ReceiverInfo.Length <> 11 Then
    ''                    Continue For
    ''                End If

    ''                If arrSMSDestination Is Nothing Then
    ''                    ReDim arrSMSDestination(0)
    ''                    ReDim arrSMSMessages(0)
    ''                Else
    ''                    ReDim Preserve arrSMSDestination(arrSMSDestination.Length)
    ''                    ReDim Preserve arrSMSMessages(arrSMSMessages.Length)
    ''                End If
    ''                arrSMSDestination(arrSMSDestination.Length - 1) = drwSMSList.ReceiverInfo
    ''                arrSMSMessages(arrSMSMessages.Length - 1) = drwSMSList.strMessage
    ''                intLastID = drwSMSList.ID


    ''            Next drwSMSList

    ''            Dim strBatch As String = "MHadi+" & drwSystemSetting.GatewayCompany & "+" & Date.Now.ToString("yyMMddHHmmss") & Date.Now.Millisecond.ToString


    ''            Dim objSMS As New clsSMS
    ''            objSMS.SendSMS_LikeToLike(arrSMSMessages, arrSMSDestination, drwSystemSetting.GatewayUsername, drwSystemSetting.GatewayPassword, drwSystemSetting.GatewayNumber, drwSystemSetting.GatewayIP, drwSystemSetting.GatewayCompany, strBatch)

    ''            Dim qryLogDetail As New BusinessObject.dstHadiWarningNotificationLogDetailTableAdapters.QueriesTableAdapter
    ''            qryLogDetail.spr_HadiWarningNotificationLogDetail_Batch_Update(intFirstID, intLastID, strBatch)

    ''        Catch ex As Exception
    ''            Continue Do
    ''        End Try

    ''    Loop


    ''End Sub

    Private Sub SMS_DeliveryUpdate()

        Do
            Try
                Dim tadpSMSDelivery As New BusinessObject.dstWarningNotificationLogDetailTableAdapters.spr_WarningNotificationLogDetail_DeliverySMS_List_SelectTableAdapter
                Dim dtblSMSDelivery As BusinessObject.dstWarningNotificationLogDetail.spr_WarningNotificationLogDetail_DeliverySMS_List_SelectDataTable = Nothing
                dtblSMSDelivery = tadpSMSDelivery.GetData

                If dtblSMSDelivery.Rows.Count = 0 Then
                    Threading.Thread.Sleep(120000)
                    Continue Do
                End If
                For Each drwSMSDelivery As BusinessObject.dstWarningNotificationLogDetail.spr_WarningNotificationLogDetail_DeliverySMS_List_SelectRow In dtblSMSDelivery.Rows


                    Dim strBatchID As String = drwSMSDelivery.BatchID
                    Dim objSMSStatus As New SMSStatus
                    Dim smsStatusArr() As SMSStatus.STC_SMSStatus
                    smsStatusArr = objSMSStatus.StatusSMS(drwSystemSetting.GatewayUsername, drwSystemSetting.GatewayPassword, drwSystemSetting.GatewayIP, drwSystemSetting.GatewayCompany, strBatchID)


                    If smsStatusArr IsNot Nothing Then

                        For i As Integer = 0 To smsStatusArr.Length - 1
                            Dim strDeliveryStatus As String = smsStatusArr(i).DeliveryStatus
                            Dim strReceiverNumber As String = correctNumber(smsStatusArr(i).ReceiveNumber)
                            If strDeliveryStatus = "MT_DELIVERED" OrElse strDeliveryStatus = "CHECK_OK" Then

                                Dim qryDetailLog As New BusinessObject.dstWarningNotificationLogDetailTableAdapters.QueriesTableAdapter
                                qryDetailLog.spr_WarningNotificationLogDetail_Delivery_Update(strReceiverNumber, strBatchID, 3)



                            Else 'Failed

                                Dim qryDetailLog As New BusinessObject.dstWarningNotificationLogDetailTableAdapters.QueriesTableAdapter
                                qryDetailLog.spr_WarningNotificationLogDetail_Delivery_Update(strReceiverNumber, strBatchID, 4)



                            End If


                        Next i



                    Else
                        Dim qryDetailLog As New BusinessObject.dstWarningNotificationLogDetailTableAdapters.QueriesTableAdapter
                        qryDetailLog.spr_WarningNotificationLogDetail_Delivery_Batch_Update(strBatchID, 3)

                    End If








                Next drwSMSDelivery

            Catch ex As Exception
                Continue Do
            End Try

        Loop


    End Sub


    'Private Sub HadiSMS_DeliveryUpdate()

    '    Do
    '        Try
    '            Dim tadpSMSDelivery As New BusinessObject.dstHadiWarningNotificationLogDetailTableAdapters.spr_HadiWarningNotificationLogDetail_DeliverySMS_List_SelectTableAdapter
    '            Dim dtblSMSDelivery As BusinessObject.dstHadiWarningNotificationLogDetail.spr_HadiWarningNotificationLogDetail_DeliverySMS_List_SelectDataTable = Nothing
    '            dtblSMSDelivery = tadpSMSDelivery.GetData

    '            If dtblSMSDelivery.Rows.Count = 0 Then
    '                Threading.Thread.Sleep(120000)
    '                Continue Do
    '            End If
    '            For Each drwSMSDelivery As BusinessObject.dstHadiWarningNotificationLogDetail.spr_HadiWarningNotificationLogDetail_DeliverySMS_List_SelectRow In dtblSMSDelivery.Rows


    '                Dim strBatchID As String = drwSMSDelivery.BatchID
    '                Dim objSMSStatus As New SMSStatus
    '                Dim smsStatusArr() As SMSStatus.STC_SMSStatus
    '                smsStatusArr = objSMSStatus.StatusSMS(drwSystemSetting.GatewayUsername, drwSystemSetting.GatewayPassword, drwSystemSetting.GatewayIP, drwSystemSetting.GatewayCompany, strBatchID)


    '                If smsStatusArr IsNot Nothing Then

    '                    For i As Integer = 0 To smsStatusArr.Length - 1
    '                        Dim strDeliveryStatus As String = smsStatusArr(i).DeliveryStatus
    '                        Dim strReceiverNumber As String = correctNumber(smsStatusArr(i).ReceiveNumber)
    '                        If strDeliveryStatus = "MT_DELIVERED" OrElse strDeliveryStatus = "CHECK_OK" Then

    '                            Dim qryDetailLog As New BusinessObject.dstHadiWarningNotificationLogDetailTableAdapters.QueriesTableAdapter
    '                            qryDetailLog.spr_HadiWarningNotificationLogDetail_Delivery_Update(strReceiverNumber, strBatchID, 3)



    '                        Else 'Failed

    '                            Dim qryDetailLog As New BusinessObject.dstHadiWarningNotificationLogDetailTableAdapters.QueriesTableAdapter
    '                            qryDetailLog.spr_HadiWarningNotificationLogDetail_Delivery_Update(strReceiverNumber, strBatchID, 4)



    '                        End If


    '                    Next i



    '                Else
    '                    Dim qryDetailLog As New BusinessObject.dstHadiWarningNotificationLogDetailTableAdapters.QueriesTableAdapter
    '                    qryDetailLog.spr_HadiWarningNotificationLogDetail_Delivery_Batch_Update(strBatchID, 3)

    '                End If








    '            Next drwSMSDelivery

    '        Catch ex As Exception
    '            Continue Do
    '        End Try

    '    Loop


    'End Sub

    Private Function correctNumber(ByVal uNumber As String) As String
        Dim ret As String = Trim(uNumber)
        If ret.Substring(0, 4) = "0098" Then ret = ret.Remove(0, 4)
        If ret.Substring(0, 3) = "098" Then ret = ret.Remove(0, 3)
        If ret.Substring(0, 3) = "+98" Then ret = ret.Remove(0, 3)
        If ret.Substring(0, 2) = "98" Then ret = ret.Remove(0, 2)
        If ret.Substring(0, 1) = "0" Then ret = ret.Remove(0, 1)
        'If ret.Substring(0, 2) = "91" Then ret = "0" + ret
        Return "+98" & ret
    End Function
#End Region



    Private Function CreateMessage(ByVal intDraftTypeID As Integer, ByVal blnIsMale As Boolean, ByVal strFName As String, ByVal strLName As String, ByVal strLoanFileNo As String, ByVal intNPDuration As Integer, ByVal blnToSponsor As Boolean, ByVal intIntervalID As Integer, ByVal strBranchName As String, ByVal strSponsorFName As String, ByVal strSponsorLName As String, IsMaleSponsor As Boolean) As String
        Try

            Dim strResult As String = ""

            Dim tadpDraftTextList As New BusinessObject.dstDraftTableAdapters.spr_DraftText_List_SelectTableAdapter
            Dim dtblDrafTextList As BusinessObject.dstDraft.spr_DraftText_List_SelectDataTable = Nothing
            dtblDrafTextList = tadpDraftTextList.GetData(intDraftTypeID, blnToSponsor, intIntervalID)

            For Each drwDraftTextList As BusinessObject.dstDraft.spr_DraftText_List_SelectRow In dtblDrafTextList.Rows

                If drwDraftTextList.IsDynamic = True Then

                    Select Case CInt(drwDraftTextList.DraftText.Trim)
                        Case 1 'Sex
                            If blnIsMale = True Then
                                strResult &= " آقای"
                            Else
                                strResult &= " خانم"
                            End If

                        Case 2 'FName
                            strResult &= " " & strFName
                        Case 3 'LName
                            strResult &= " " & strLName
                        Case 4 'Loan File No
                            strResult &= " " & strLoanFileNo
                        Case 5 'NPDuration
                            strResult &= " " & intNPDuration.ToString
                        Case 6 'Brnach
                            strResult &= " " & strBranchName
                        Case 7 'Sponsor FName
                            strResult &= " " & strBranchName
                        Case 8 'Sponsor LName
                            strResult &= " " & strBranchName
                        Case 9 'Sponsor Sex
                            If blnIsMale = True Then
                                strResult &= " آقای"
                            Else
                                strResult &= " خانم"
                            End If

                    End Select


                Else
                    strResult &= " " & drwDraftTextList.DraftText
                End If


            Next drwDraftTextList

            Return strResult.Trim
        Catch ex As Exception
            Return ""
        End Try


    End Function

    Private Function CreateHadiMessage(ByVal blnIsMale As Boolean, ByVal strFName As String, ByVal strLName As String, ByVal strLoanFileNo As String, ByVal intNPDuration As Integer, ByVal intIntervalID As Integer, ByVal strBranchName As String, ByVal strDepositTypeName As String, ByVal strLoanNumber As String, ByVal strLoanDate As String) As String
        Try

            Dim strResult As String = ""

            Dim tadpDraftTextList As New BusinessObject.dstHadiDraftTableAdapters.spr_HadiDraftText_List_SelectTableAdapter
            Dim dtblDrafTextList As BusinessObject.dstHadiDraft.spr_HadiDraftText_List_SelectDataTable = Nothing
            dtblDrafTextList = tadpDraftTextList.GetData(intIntervalID)

            For Each drwDraftTextList As BusinessObject.dstHadiDraft.spr_HadiDraftText_List_SelectRow In dtblDrafTextList.Rows

                If drwDraftTextList.IsDynamic = True Then

                    Select Case CInt(drwDraftTextList.DraftText.Trim)
                        Case 1 'Sex
                            If blnIsMale = True Then
                                strResult &= " آقای"
                            Else
                                strResult &= " خانم"
                            End If

                        Case 2 'FName
                            strResult &= " " & strFName
                        Case 3 'LName
                            strResult &= " " & strLName
                        Case 4 'Loan File No
                            strResult &= " " & strLoanFileNo
                        Case 5 'NPDuration
                            strResult &= " " & intNPDuration.ToString
                        Case 6 'Brnach
                            strResult &= " " & strBranchName
                        Case 7 'DepositType Name/LoanType Name
                            strResult &= " " & strDepositTypeName
                        Case 8 'LoanNumber
                            strResult &= " " & strLoanNumber
                        Case 9 'LoanDate
                            strResult &= " " & strLoanDate

                    End Select


                Else
                    strResult &= " " & drwDraftTextList.DraftText
                End If


            Next drwDraftTextList

            Return strResult.Trim
        Catch ex As Exception
            Return ""
        End Try


    End Function

#Region "UpdateData"

    Public Sub UpdateBIData()

        Dim tadpSystemSetting As New BusinessObject.dstSystemSettingTableAdapters.spr_SystemSetting_SelectTableAdapter
        Dim dtblSystemSetting As BusinessObject.dstSystemSetting.spr_SystemSetting_SelectDataTable = Nothing
        dtblSystemSetting = tadpSystemSetting.GetData()
        drwSystemSetting = dtblSystemSetting.Rows(0)

        If Date.Now.DayOfWeek = DayOfWeek.Friday Then
            Return
        End If


        If drwSystemSetting.VosoulService = False Then
            Return
        End If

        If drwSystemSetting.tryTime = 0 Then
            Return
        End If

        If drwSystemSetting.UpdateTime > Date.Now.TimeOfDay Then
            Return
        End If

        Dim dteThisDate As Date = Date.Now.AddDays(-1)

        Dim blnIsUpdateDay As Boolean = False
        If Date.Now.DayOfWeek = DayOfWeek.Sunday OrElse Date.Now.DayOfWeek = DayOfWeek.Tuesday Then
            blnIsUpdateDay = True
        End If

        '  If (Date.Now.DayOfWeek = DayOfWeek.Friday, True, False)

        ' Dim blnFriday As Boolean = If(Date.Now.DayOfWeek = DayOfWeek.Friday, True, False)



        Dim tadpLogHeader As New BusinessObject.dstLogCurrentLCStatus_HTableAdapters.spr_LogCurrentLCStatus_H_ForDate_SelectTableAdapter
        Dim dtblLogHeader As BusinessObject.dstLogCurrentLCStatus_H.spr_LogCurrentLCStatus_H_ForDate_SelectDataTable = Nothing
        dtblLogHeader = tadpLogHeader.GetData(dteThisDate)

        Dim intCurrentTryTime As Integer = 0

        If dtblLogHeader.Rows.Count > 0 Then
            Dim drwLogHeader As BusinessObject.dstLogCurrentLCStatus_H.spr_LogCurrentLCStatus_H_ForDate_SelectRow = dtblLogHeader.Rows(0)
            If drwLogHeader.Success = True Then
                Return
            End If

            If drwLogHeader.tryTime >= drwSystemSetting.tryTime Then
                Return
            End If

            If Date.Now.Subtract(drwLogHeader.STime).TotalHours < drwSystemSetting.tryIntervalHour Then
                Return
            End If

            intCurrentTryTime = drwLogHeader.tryTime

        End If


        'Dim tadpMinMaxInterval As New BusinessObject.dstWarningIntervalsTableAdapters.spr_WarningIntervals_MinMaxFromTo_SelectTableAdapter
        'Dim dtblMinMaxInterval As BusinessObject.dstWarningIntervals.spr_WarningIntervals_MinMaxFromTo_SelectDataTable = Nothing
        'dtblMinMaxInterval = tadpMinMaxInterval.GetData()
        'Dim intMaxInterval As Integer = Integer.MaxValue
        'Dim intMinInterval As Integer = Integer.MinValue

        'If dtblMinMaxInterval.Rows.Count > 0 Then
        '    Dim drwMinMaxInterval As BusinessObject.dstWarningIntervals.spr_WarningIntervals_MinMaxFromTo_SelectRow = dtblMinMaxInterval.Rows(0)
        '    If drwMinMaxInterval.IsMaxToNull = False Then
        '        intMaxInterval = drwMinMaxInterval.MaxTo
        '        intMinInterval = drwMinMaxInterval.MinFrom
        '    End If
        'End If

        Dim tadpIntervalList As New DataSet1TableAdapters.spr_WarningIntervals_Inerval_List_SelectTableAdapter
        Dim dtblIntervalList As DataSet1.spr_WarningIntervals_Inerval_List_SelectDataTable = Nothing
        dtblIntervalList = tadpIntervalList.GetData()

        Dim strIntervalText As String = ""

        For Each drwIntervalList As DataSet1.spr_WarningIntervals_Inerval_List_SelectRow In dtblIntervalList.Rows
            strIntervalText &= " or (NPDURATION between " & drwIntervalList.FromDay & " and " & drwIntervalList.ToDay & " )"
        Next drwIntervalList

        strIntervalText = strIntervalText.Substring(3)


        intCurrentTryTime += 1


        Dim qryLogHeader As New BusinessObject.dstLogCurrentLCStatus_HTableAdapters.QueriesTableAdapter



        Dim strThisDatePersian As String = mdlGeneral.GetPersianDate(dteThisDate).Replace("/", "")

        Dim cnnBuiler_BI As New OracleConnectionStringBuilder()
        cnnBuiler_BI.DataSource = "10.35.1.37:1522/bidb"
        cnnBuiler_BI.UserID = "deposit"
        cnnBuiler_BI.Password = "deposit"
        cnnBuiler_BI.Unicode = True

        Using cnnBI_Connection As New OracleConnection(cnnBuiler_BI.ConnectionString)

            Dim cmd_BI As OracleCommand = cnnBI_Connection.CreateCommand()

            Dim tadpWarningIntervalBranchList As New BusinessObject.dstWarningIntervalsBranchTableAdapters.spr_WarningIntervalsBranch_List_SelectTableAdapter
            Dim dtblWarningIntervalBranchList As BusinessObject.dstWarningIntervalsBranch.spr_WarningIntervalsBranch_List_SelectDataTable = Nothing
            dtblWarningIntervalBranchList = tadpWarningIntervalBranchList.GetData()

            If dtblWarningIntervalBranchList.Rows.Count = 0 Then
                cnnBI_Connection.Close()
                qryLogHeader.spr_LogCurrentLCStatus_H_Insert(dteThisDate, Date.Now, False, "گردش کارهای تعریف شده ناقص هستند", intCurrentTryTime)

                Return
            End If


            '  Dim strLoan_Info_Query As String = "SELECT * from loan_info where Date_P='" & strThisDatePersian & "' and state in ('3','4','5','F') and (NPDURATION between " & intMinInterval & " and " & intMaxInterval & ") and ("
            Dim strLoan_Info_Query As String = "SELECT * from loan_info where Date_P='" & strThisDatePersian & "' and state in ('3','4','5','F') and (" & strIntervalText & ") and ("



            Dim strBranchQuery As String = ""
            For Each drwWarningIntervalBranchList As BusinessObject.dstWarningIntervalsBranch.spr_WarningIntervalsBranch_List_SelectRow In dtblWarningIntervalBranchList.Rows
                strBranchQuery &= "or ABRNCHCOD='" & drwWarningIntervalBranchList.BrnachCode & "'"

            Next drwWarningIntervalBranchList

            strLoan_Info_Query &= strBranchQuery.Substring(3) & ")"

            ''Dim strExceptionQuery As String = ""

            ''Dim cntxVar As New BusinessObject.dbMehrVosulEntities1

            ''Dim lnqWarningIntervalsException = cntxVar.tbl_WarningIntervalsException.Where(Function(x) x.tbl_WarningIntervals.ISActive = True AndAlso x.ISActive = True)


            ''For Each itemWarningIntervalsException In lnqWarningIntervalsException

            ''    For Each itemWarningIntervalsExceptionBranch In itemWarningIntervalsException.tbl_WarningIntervalsExceptionBranch

            ''        For Each itemWarningIntervalsExceptionLoanType In itemWarningIntervalsException.tbl_WarningIntervalsExceptionLoanType

            ''            strExceptionQuery &= "and not(ABRNCHCOD='" & itemWarningIntervalsExceptionBranch.tbl_WarningIntervalsBranch.tbl_Branch.BrnachCode & "' and LNMINORTP='" & itemWarningIntervalsExceptionLoanType.tbl_WarningIntervalsLoanType.tbl_LoanType.LoanTypeCode & "')"

            ''        Next itemWarningIntervalsExceptionLoanType


            ''    Next itemWarningIntervalsExceptionBranch


            ''Next itemWarningIntervalsException


            ''strLoan_Info_Query &= strExceptionQuery

            cmd_BI.CommandText = strLoan_Info_Query

            Try
                cnnBI_Connection.Open()
            Catch ex As Exception

                qryLogHeader.spr_LogCurrentLCStatus_H_Insert(dteThisDate, Date.Now, False, ex.Message, intCurrentTryTime)
                Return
            End Try
            Dim dataReader As OracleDataReader = Nothing

            Try
                dataReader = cmd_BI.ExecuteReader()
            Catch ex As Exception

                qryLogHeader.spr_LogCurrentLCStatus_H_Insert(dteThisDate, Date.Now, False, ex.Message, intCurrentTryTime)
                cnnBI_Connection.Close()

                Return
            End Try

            Try
                If dataReader.Read = False Then

                    qryLogHeader.spr_LogCurrentLCStatus_H_Insert(dteThisDate, Date.Now, False, "اطلاعات مربوط به مورخ " & mdlGeneral.GetPersianDate(dteThisDate) & " بروز رسانی نشده است. لطفا با مدیر سیستم تماس بگیرید", intCurrentTryTime)
                    dataReader.Close()
                    cnnBI_Connection.Close()

                    Return
                End If

                Dim intLogHeaderID As Integer = qryLogHeader.spr_LogCurrentLCStatus_H_Insert(dteThisDate, Date.Now, True, "", intCurrentTryTime)

                Dim qryLCCurrentStatus As New BusinessObject.dstCurrentLCStatusTableAdapters.QueriesTableAdapter
                qryLCCurrentStatus.spr_CurrentLCStatus_Delete()

                Dim i As Integer = 0
                Dim strBuilder As New Text.StringBuilder()
                Dim qryCurrentLCStatus As New BusinessObject.dstCurrentLCStatusTableAdapters.QueriesTableAdapter






                Do
                    i += 1
                    Try
                        Dim stcVarLoanInfo As stc_Loan_Info

                        If dataReader.GetValue(0) Is DBNull.Value Then
                            stcVarLoanInfo.FullName = ""
                        Else
                            stcVarLoanInfo.FullName = CStr(dataReader.GetValue(0)).Replace("'", "")
                        End If

                        If dataReader.GetValue(1) Is DBNull.Value Then
                            stcVarLoanInfo.Address = ""
                        Else
                            stcVarLoanInfo.Address = CStr(dataReader.GetValue(1)).Replace("'", "")
                        End If

                        If dataReader.GetValue(2) Is DBNull.Value Then
                            stcVarLoanInfo.Telephone = ""
                        Else
                            stcVarLoanInfo.Telephone = CStr(dataReader.GetValue(2)).Trim.Replace("'", "")
                        End If

                        'If dataReader.GetValue(3) Is DBNull.Value Then
                        '    stcVarLoanInfo.Fax = ""
                        'Else
                        '    stcVarLoanInfo.Fax = CStr(dataReader.GetValue(3)).Trim
                        'End If


                        If dataReader.GetValue(4) Is DBNull.Value Then
                            stcVarLoanInfo.Mobile = ""
                        Else
                            stcVarLoanInfo.Mobile = CStr(dataReader.GetValue(4)).Trim.Replace("'", "")
                        End If

                        stcVarLoanInfo.Date_P = CStr(dataReader.GetValue(5))



                        If dataReader.GetValue(6) Is DBNull.Value Then
                            i -= 1
                            Continue Do
                        Else
                            stcVarLoanInfo.LC_No = CStr(dataReader.GetValue(6)).Trim.Replace("'", "")
                        End If


                        If dataReader.GetValue(7) Is DBNull.Value Then
                            i -= 1
                            Continue Do
                        Else
                            stcVarLoanInfo.BranchCode = CStr(dataReader.GetValue(7)).Trim
                        End If


                        If dataReader.GetValue(8) Is DBNull.Value Then
                            i -= 1
                            Continue Do
                        Else
                            stcVarLoanInfo.LoanTypeCode = CStr(dataReader.GetValue(8)).Trim
                        End If

                        If dataReader.GetValue(9) Is DBNull.Value Then
                            i -= 1
                            Continue Do
                        Else
                            stcVarLoanInfo.CustomerNo = CStr(dataReader.GetValue(9)).Trim.Replace("'", "")
                        End If


                        If dataReader.GetValue(10) Is DBNull.Value Then
                            i -= 1
                            Continue Do
                        Else
                            stcVarLoanInfo.LoanSerial = CInt(dataReader.GetValue(10))
                        End If

                        If dataReader.GetValue(11) Is DBNull.Value Then
                            stcVarLoanInfo.LCDate = ""
                        Else
                            stcVarLoanInfo.LCDate = CStr(dataReader.GetValue(11)).Trim
                        End If

                        If dataReader.GetValue(12) Is DBNull.Value Then
                            stcVarLoanInfo.LCAmount = Nothing
                        Else
                            stcVarLoanInfo.LCAmount = CDbl(dataReader.GetValue(12))
                        End If

                        If dataReader.GetValue(13) Is DBNull.Value Then
                            stcVarLoanInfo.LCProfit = Nothing
                        Else
                            stcVarLoanInfo.LCProfit = CDbl(dataReader.GetValue(13))
                        End If

                        If dataReader.GetValue(14) Is DBNull.Value Then
                            stcVarLoanInfo.IstlNum = Nothing
                        Else
                            stcVarLoanInfo.IstlNum = CInt(dataReader.GetValue(14))
                        End If


                        If dataReader.GetValue(15) Is DBNull.Value Then
                            stcVarLoanInfo.LCAmountPaid = Nothing
                        Else
                            stcVarLoanInfo.LCAmountPaid = CDbl(dataReader.GetValue(15))
                        End If

                        If dataReader.GetValue(16) Is DBNull.Value Then
                            stcVarLoanInfo.IstlPaid = Nothing
                        Else
                            stcVarLoanInfo.IstlPaid = CInt(dataReader.GetValue(16))
                        End If

                        If dataReader.GetValue(17) Is DBNull.Value Then
                            stcVarLoanInfo.AmounDefferd = Nothing
                        Else
                            stcVarLoanInfo.AmounDefferd = CDbl(dataReader.GetValue(17))
                        End If

                        If dataReader.GetValue(18) Is DBNull.Value Then
                            stcVarLoanInfo.GDeffered = Nothing
                        Else
                            stcVarLoanInfo.GDeffered = CInt(dataReader.GetValue(18))
                        End If

                        'If dataReader.GetValue(19) Is DBNull.Value Then
                        '    stcVarLoanInfo.FirstNoPaidDate = ""
                        'Else
                        '    stcVarLoanInfo.FirstNoPaidDate = CStr(dataReader.GetValue(19)).Trim
                        'End If

                        stcVarLoanInfo.Status = CStr(dataReader.GetValue(20)).Trim


                        If dataReader.GetValue(21) Is DBNull.Value Then
                            i -= 1
                            Continue Do
                        Else
                            stcVarLoanInfo.NotPiadDurationDay = CInt(dataReader.GetValue(21))
                        End If


                        If dataReader.GetValue(22) Is DBNull.Value Then
                            i -= 1
                            Continue Do
                        Else
                            stcVarLoanInfo.LCBalance = CDbl(dataReader.GetValue(22))
                        End If



                        'If dataReader.GetValue(23) Is DBNull.Value Then
                        '    stcVarLoanInfo.G_Mande = Nothing
                        'Else
                        '    stcVarLoanInfo.G_Mande = CInt(dataReader.GetValue(23))
                        'End If


                        'If dataReader.GetValue(24) Is DBNull.Value Then
                        '    stcVarLoanInfo.G_MustPay = Nothing
                        'Else
                        '    stcVarLoanInfo.G_MustPay = CInt(dataReader.GetValue(24))
                        'End If


                        If dataReader.GetValue(25) Is DBNull.Value Then
                            stcVarLoanInfo.Amount_MustPay = Nothing
                        Else
                            stcVarLoanInfo.Amount_MustPay = CDbl(dataReader.GetValue(25))
                        End If

                        'If dataReader.GetValue(26) Is DBNull.Value Then
                        '    stcVarLoanInfo.FG_MustPayDate = ""
                        'Else
                        '    stcVarLoanInfo.FG_MustPayDate = CStr(dataReader.GetValue(26)).Trim
                        'End If

                        'If dataReader.GetValue(27) Is DBNull.Value Then
                        '    stcVarLoanInfo.L_PayDate = ""
                        'Else
                        '    stcVarLoanInfo.L_PayDate = CStr(dataReader.GetValue(27)).Trim
                        'End If

                        'If dataReader.GetValue(28) Is DBNull.Value Then
                        '    stcVarLoanInfo.LG_PayDate = ""
                        'Else
                        '    stcVarLoanInfo.LG_PayDate = CStr(dataReader.GetValue(28)).Trim
                        'End If

                        'If dataReader.GetValue(29) Is DBNull.Value Then
                        '    stcVarLoanInfo.lcupdate = ""
                        'Else
                        '    stcVarLoanInfo.lcupdate = CStr(dataReader.GetValue(29)).Trim
                        'End If

                        If dataReader.GetValue(30) Is DBNull.Value Then
                            stcVarLoanInfo.NationalID = ""
                        Else
                            stcVarLoanInfo.NationalID = CStr(dataReader.GetValue(30)).Replace("'", "")
                        End If


                        If dataReader.GetValue(31) Is DBNull.Value Then
                            stcVarLoanInfo.NationalNo = ""
                        Else
                            stcVarLoanInfo.NationalNo = CStr(dataReader.GetValue(31)).Replace("'", "")
                        End If


                        If dataReader.GetValue(32) Is DBNull.Value Then
                            stcVarLoanInfo.Sex = ""
                        Else
                            stcVarLoanInfo.Sex = CStr(dataReader.GetValue(32)).Trim.Replace("'", "")
                        End If


                        Dim tadpFilebyCustomerNo As New BusinessObject.dstFileTableAdapters.spr_File_CustomerNo_SelectTableAdapter
                        Dim dtblFilebyCustomerNo As BusinessObject.dstFile.spr_File_CustomerNo_SelectDataTable = Nothing
                        dtblFilebyCustomerNo = tadpFilebyCustomerNo.GetData(stcVarLoanInfo.CustomerNo)
                        Dim intBorrowerFileID As Integer = -1


                        If dtblFilebyCustomerNo.Rows.Count = 0 Then

                            Dim qryFile As New BusinessObject.dstFileTableAdapters.QueriesTableAdapter

                            Dim arrFullName() As String = stcVarLoanInfo.FullName.Split("*")
                            Dim strFatherName As String = arrFullName(0)
                            Dim strFName As String = arrFullName(1)
                            Dim strLName As String = arrFullName(2)


                            Dim blnIsMale As Boolean = If(stcVarLoanInfo.Sex = "زن", False, True)
                            intBorrowerFileID = qryFile.spr_File_Insert(stcVarLoanInfo.CustomerNo, strFName, strLName, strFatherName, stcVarLoanInfo.Mobile, stcVarLoanInfo.NationalID, stcVarLoanInfo.NationalNo, "", stcVarLoanInfo.Address, stcVarLoanInfo.Telephone, stcVarLoanInfo.Telephone, blnIsMale, 2, 1, Nothing, Nothing)

                        Else

                            Dim drwFilebyCustomerNo As BusinessObject.dstFile.spr_File_CustomerNo_SelectRow = dtblFilebyCustomerNo.Rows(0)
                            intBorrowerFileID = drwFilebyCustomerNo.ID

                            If blnIsUpdateDay = True Then
                                Try
                                    Dim arrFullName() As String = stcVarLoanInfo.FullName.Split("*")
                                    Dim strFatherName As String = arrFullName(0)
                                    Dim strFName As String = arrFullName(1)
                                    Dim strLName As String = arrFullName(2)
                                    Dim blnIsMale As Boolean = If(stcVarLoanInfo.Sex = "زن", False, True)
                                    Dim qryFile As New BusinessObject.dstFileTableAdapters.QueriesTableAdapter
                                    qryFile.spr_File_MAT_Update(intBorrowerFileID, stcVarLoanInfo.Mobile, stcVarLoanInfo.Address, stcVarLoanInfo.Telephone, stcVarLoanInfo.Telephone, strFName, strLName, strFatherName, blnIsMale)
                                Catch ex As Exception

                                    Dim qryErrorLog As New DataSet1TableAdapters.QueriesTableAdapter
                                    qryErrorLog.spr_ErrorLog_Insert(ex.Message, 1, "UpdateBIData")

                                End Try

                            End If


                        End If


                        Dim tadpLoanByNumber As New BusinessObject.dstLoanTableAdapters.spr_Loan_ByLoanNumber_SelectTableAdapter
                        Dim dtblLoanByNumber As BusinessObject.dstLoan.spr_Loan_ByLoanNumber_SelectDataTable = Nothing
                        dtblLoanByNumber = tadpLoanByNumber.GetData(stcVarLoanInfo.LC_No, intBorrowerFileID)


                        Dim intLoanID As Integer = -1

                        If dtblLoanByNumber.Rows.Count = 0 Then
                            Dim qryLoan As New BusinessObject.dstLoanTableAdapters.QueriesTableAdapter


                            Dim dteLoanDate? As Date = Nothing
                            Try
                                stcVarLoanInfo.LCDate = stcVarLoanInfo.LCDate.Insert(4, "/")
                                stcVarLoanInfo.LCDate = stcVarLoanInfo.LCDate.Insert(7, "/")
                                dteLoanDate = mdlGeneral.GetGregorianDate(stcVarLoanInfo.LCDate)
                            Catch ex As Exception
                                dteLoanDate = Nothing
                            End Try

                            Dim tadpBranchbyCode As New BusinessObject.dstBranchTableAdapters.spr_Branch_ByCode_SelectTableAdapter
                            Dim dtblBranchbyCode As BusinessObject.dstBranch.spr_Branch_ByCode_SelectDataTable = Nothing
                            dtblBranchbyCode = tadpBranchbyCode.GetData(stcVarLoanInfo.BranchCode)

                            Dim intBranchID As Integer = -1

                            If dtblBranchbyCode.Rows.Count = 0 Then


                                Dim obj_stc_BranchInfo As stc_Branch_Info = GetBarnchName(stcVarLoanInfo.BranchCode)


                                Dim qryBranch As New BusinessObject.dstBranchTableAdapters.QueriesTableAdapter

                                intBranchID = qryBranch.spr_Branch_Insert(stcVarLoanInfo.BranchCode, obj_stc_BranchInfo.BranchName, obj_stc_BranchInfo.BranchAddress, 2, Nothing, "", 1) 'UserID=2 is System User



                            Else
                                Dim drwBranchbyCode As BusinessObject.dstBranch.spr_Branch_ByCode_SelectRow = dtblBranchbyCode.Rows(0)
                                intBranchID = drwBranchbyCode.ID

                            End If


                            Dim tadpLoanTypeByCode As New BusinessObject.dstLoanTypeTableAdapters.spr_LoanType_byCode_SelectTableAdapter
                            Dim dtblLoanTypeByCode As BusinessObject.dstLoanType.spr_LoanType_byCode_SelectDataTable = Nothing
                            dtblLoanTypeByCode = tadpLoanTypeByCode.GetData(stcVarLoanInfo.LoanTypeCode)

                            Dim intLoanTypeID As Integer = -1

                            If dtblLoanTypeByCode.Rows.Count = 0 Then

                                Dim strLoanTypeName As String = GetLoanTypeName(stcVarLoanInfo.LoanTypeCode)
                                Dim qryLoanType As New BusinessObject.dstLoanTypeTableAdapters.QueriesTableAdapter
                                intLoanTypeID = qryLoanType.spr_LoanType_Insert(stcVarLoanInfo.LoanTypeCode, strLoanTypeName, 2, "")


                            Else
                                Dim drwLoanTypeByCode As BusinessObject.dstLoanType.spr_LoanType_byCode_SelectRow = dtblLoanTypeByCode.Rows(0)
                                intLoanTypeID = drwLoanTypeByCode.ID


                            End If


                            intLoanID = qryLoan.spr_Loan_Insert(intBorrowerFileID, intLoanTypeID, intBranchID, dteLoanDate, stcVarLoanInfo.LC_No, stcVarLoanInfo.LoanSerial, Date.Now, stcVarLoanInfo.LCAmount, stcVarLoanInfo.IstlNum)


                            Dim arrintFileSponsors() As stc_Sponsor_Waranty = GetSponsorsList(stcVarLoanInfo.BranchCode, stcVarLoanInfo.LoanTypeCode, stcVarLoanInfo.CustomerNo, stcVarLoanInfo.LoanSerial, blnIsUpdateDay)

                            If arrintFileSponsors IsNot Nothing Then
                                For k As Integer = 0 To arrintFileSponsors.Length - 1


                                    Dim qryLoanSponsor As New BusinessObject.dstLoanSponsorTableAdapters.QueriesTableAdapter
                                    Dim intLoanSponsorID As Integer = qryLoanSponsor.spr_LoanSponsor_Insert(intLoanID, arrintFileSponsors(k).SponsorID, arrintFileSponsors(k).WarantyTypeDesc)

                                Next k
                            End If



                        Else


                            Dim drwLoanByNumber As BusinessObject.dstLoan.spr_Loan_ByLoanNumber_SelectRow = dtblLoanByNumber.Rows(0)
                            intLoanID = drwLoanByNumber.ID


                            'Update Sponsors

                            Dim arrintFileSponsors() As stc_Sponsor_Waranty = GetSponsorsList(stcVarLoanInfo.BranchCode, stcVarLoanInfo.LoanTypeCode, stcVarLoanInfo.CustomerNo, stcVarLoanInfo.LoanSerial, blnIsUpdateDay)



                            If arrintFileSponsors IsNot Nothing Then
                                For k As Integer = 0 To arrintFileSponsors.Length - 1


                                    Dim tadpLoanSponsorCheck As New BusinessObject.dstLoanSponsorTableAdapters.spr_LoanSponsor_Check_SelectTableAdapter
                                    Dim dtblLoanSponsorCheck As BusinessObject.dstLoanSponsor.spr_LoanSponsor_Check_SelectDataTable = Nothing
                                    dtblLoanSponsorCheck = tadpLoanSponsorCheck.GetData(intLoanID, arrintFileSponsors(k).SponsorID)


                                    If dtblFilebyCustomerNo.Rows.Count = 0 Then

                                        Dim qryLoanSponsor As New BusinessObject.dstLoanSponsorTableAdapters.QueriesTableAdapter
                                        Dim intLoanSponsorID As Integer = qryLoanSponsor.spr_LoanSponsor_Insert(intLoanID, arrintFileSponsors(k).SponsorID, arrintFileSponsors(k).WarantyTypeDesc)


                                    End If

                                Next k
                            End If




                        End If




                        'Dim dteFirstNotPaid? As Date = Nothing
                        'Try
                        '    stcVarLoanInfo.FirstNoPaidDate = stcVarLoanInfo.FirstNoPaidDate.Insert(4, "/")
                        '    stcVarLoanInfo.FirstNoPaidDate = stcVarLoanInfo.FirstNoPaidDate.Insert(7, "/")
                        '    dteFirstNotPaid = mdlGeneral.GetGregorianDate(stcVarLoanInfo.FirstNoPaidDate)
                        'Catch ex As Exception
                        '    dteFirstNotPaid = Nothing
                        'End Try

                        'Dim dteFG_MustPayDate? As Date = Nothing
                        'Try
                        '    stcVarLoanInfo.FG_MustPayDate = stcVarLoanInfo.FG_MustPayDate.Insert(4, "/")
                        '    stcVarLoanInfo.FG_MustPayDate = stcVarLoanInfo.FG_MustPayDate.Insert(7, "/")
                        '    dteFG_MustPayDate = mdlGeneral.GetGregorianDate(stcVarLoanInfo.FG_MustPayDate)
                        'Catch ex As Exception
                        '    dteFG_MustPayDate = Nothing
                        'End Try


                        'Dim dteL_PayDate? As Date = Nothing
                        'Try
                        '    stcVarLoanInfo.L_PayDate = stcVarLoanInfo.L_PayDate.Insert(4, "/")
                        '    stcVarLoanInfo.L_PayDate = stcVarLoanInfo.L_PayDate.Insert(7, "/")
                        '    dteL_PayDate = mdlGeneral.GetGregorianDate(stcVarLoanInfo.L_PayDate)
                        'Catch ex As Exception
                        '    dteL_PayDate = Nothing
                        'End Try


                        'Dim dteLG_PayDate? As Date = Nothing
                        'Try
                        '    stcVarLoanInfo.LG_PayDate = stcVarLoanInfo.LG_PayDate.Insert(4, "/")
                        '    stcVarLoanInfo.LG_PayDate = stcVarLoanInfo.LG_PayDate.Insert(7, "/")
                        '    dteLG_PayDate = mdlGeneral.GetGregorianDate(stcVarLoanInfo.LG_PayDate)
                        'Catch ex As Exception
                        '    dteLG_PayDate = Nothing
                        'End Try
                        '



                        With stcVarLoanInfo


                            '(@Date_P date
                            '       ,@LCProfit money
                            '       ,@LCAmountPaid money
                            '       ,@AmounDefferd money
                            '       ,@Status char(2)
                            '       ,@NotPiadDurationDay int
                            '       @NoDelayInstallment int
                            '       ,@LCBalance money
                            '        ,@Amount_MustPay int
                            '        ,@FK_FileID int
                            '       ,@FK_LoanID int
                            '       ,@NoPaidInstallment int
                            ',@Process
                            ')



                            Dim strTempInsertQuery As String = " union select '" & dteThisDate & "'," & .LCProfit.ToString & "," & .LCAmountPaid.ToString & "," & .AmounDefferd.ToString
                            strTempInsertQuery &= ",'" & .Status & "'," & .NotPiadDurationDay.ToString & "," & .GDeffered.ToString & "," & .LCBalance.ToString & "," & .Amount_MustPay.ToString & "," & intBorrowerFileID.ToString & "," & intLoanID.ToString & "," & .IstlPaid.ToString & ",0"
                            strBuilder.Append(strTempInsertQuery)


                            If i >= 500 Then
                                Dim strMainIntertQuery As String = strBuilder.ToString.Substring(7)
                                qryCurrentLCStatus.spr_CurrentLCStatus_Bulk_Insert(strMainIntertQuery)
                                i = 0
                                strBuilder.Clear()

                            End If






                        End With


                    Catch ex As Exception
                        Continue Do
                    End Try


                Loop While dataReader.Read()
                dataReader.Close()

                If i <> 0 Then
                    Dim strMainInsertQuery As String = strBuilder.ToString.Substring(6)
                    qryCurrentLCStatus.spr_CurrentLCStatus_Bulk_Insert(strMainInsertQuery)

                End If


            Catch ex As Exception

                qryLogHeader.spr_LogCurrentLCStatus_H_Insert(dteThisDate, Date.Now, False, ex.Message, intCurrentTryTime)
                Return
            End Try

            cnnBI_Connection.Close()

        End Using


        Call SendAdministratioSMSMessage_ForSMS()

        Call SendAdministratioSMSMessage_ForVoice()

    End Sub

    'Public Sub Hadi_BI_Deposit()

    '    Dim tadpSystemSetting As New BusinessObject.dstSystemSettingTableAdapters.spr_SystemSetting_SelectTableAdapter
    '    Dim dtblSystemSetting As BusinessObject.dstSystemSetting.spr_SystemSetting_SelectDataTable = Nothing
    '    dtblSystemSetting = tadpSystemSetting.GetData()
    '    drwSystemSetting = dtblSystemSetting.Rows(0)


    '    If drwSystemSetting.HadiService = False Then
    '        Return
    '    End If

    '    If drwSystemSetting.tryTime_Deposit = 0 Then
    '        Return
    '    End If

    '    If drwSystemSetting.UpdateTime_Deposit > Date.Now.TimeOfDay Then
    '        Return
    '    End If

    '    Dim dteThisDate As Date = Date.Now.AddDays(-1)

    '    Dim blnIsUpdateDay As Boolean = False
    '    If Date.Now.DayOfWeek = DayOfWeek.Sunday OrElse Date.Now.DayOfWeek = DayOfWeek.Tuesday Then
    '        blnIsUpdateDay = True
    '    End If



    '    Dim tadpLogHeader As New BusinessObject.dstHadiLogCurrentLCStatus_HTableAdapters.spr_HadiLogCurrentLCStatus_H_ForDate_SelectTableAdapter
    '    Dim dtblLogHeader As BusinessObject.dstHadiLogCurrentLCStatus_H.spr_HadiLogCurrentLCStatus_H_ForDate_SelectDataTable = Nothing
    '    dtblLogHeader = tadpLogHeader.GetData(dteThisDate)

    '    Dim intCurrentTryTime As Integer = 0

    '    If dtblLogHeader.Rows.Count > 0 Then
    '        Dim drwLogHeader As BusinessObject.dstHadiLogCurrentLCStatus_H.spr_HadiLogCurrentLCStatus_H_ForDate_SelectRow = dtblLogHeader.Rows(0)
    '        If drwLogHeader.Success = True Then
    '            Return
    '        End If

    '        If drwLogHeader.tryTime >= drwSystemSetting.tryTime_Deposit Then
    '            Return
    '        End If

    '        If Date.Now.Subtract(drwLogHeader.STime).TotalHours < drwSystemSetting.tryIntervalHour_Deposit Then
    '            Return
    '        End If

    '        intCurrentTryTime = drwLogHeader.tryTime

    '    End If




    '    Dim tadpIntervalList As New DataSet1TableAdapters.spr_HadiWarningIntervals_Inerval_List_SelectTableAdapter
    '    Dim dtblIntervalList As DataSet1.spr_HadiWarningIntervals_Inerval_List_SelectDataTable = Nothing
    '    dtblIntervalList = tadpIntervalList.GetData(1)

    '    Dim strIntervalText As String = ""

    '    For Each drwIntervalList As DataSet1.spr_HadiWarningIntervals_Inerval_List_SelectRow In dtblIntervalList.Rows
    '        'where m.ABRNCHCOD = 6131 And TO_CHAR(m.DATE_P) > to_char(13950405)
    '        Dim strFromDate As String = mdlGeneral.GetPersianDate(Date.Now.AddDays(-drwIntervalList.FromDay)).Replace("/", "")
    '        Dim strToDate As String = mdlGeneral.GetPersianDate(Date.Now.AddDays(-drwIntervalList.ToDay)).Replace("/", "")

    '        ' strIntervalText &= " Or (DATE_P between " & strToDate & " and " & strFromDate & ")"
    '        ' strIntervalText &= " Or (TO_CHAR(DATE_P) between to_char(" & strFromDate & ") and  " & " to_char(" & strToDate & "))"
    '        'strIntervalText &= " Or (TO_CHAR(DATE_P) > TO_CHAR(" & strToDate & "))"
    '        ' strIntervalText &= " Or ((SYSDATE-(TO_DATE(DATE_P, 'YYYY/MM/DD','NLS_CALENDAR=persian')) between " & drwIntervalList.FromDay & " and " & drwIntervalList.ToDay & " ))"
    '        ' strIntervalText &= " or ((SYSDATE-date_en) between " & drwIntervalList.FromDay & " and " & drwIntervalList.ToDay & " )"

    '        strIntervalText &= " Or (trunc(tdopndat)  between " & "to_date('" & strToDate & "','yyyymmdd','nls_calendar = persian')" & " And " & "to_date('" & strFromDate & "','yyyymmdd','nls_calendar = persian')" & ")"


    '    Next drwIntervalList

    '    strIntervalText = strIntervalText.Substring(3)


    '    intCurrentTryTime += 1


    '    Dim qryLogHeader As New BusinessObject.dstHadiLogCurrentLCStatus_HTableAdapters.QueriesTableAdapter


    '    'Dim strThisDatePersian As String = mdlGeneral.GetPersianDate(dteThisDate).Replace("/", "")

    '    Dim cnnBuiler_BI As New OracleConnectionStringBuilder()
    '    cnnBuiler_BI.DataSource = "10.35.1.37:1522/bidb"
    '    cnnBuiler_BI.UserID = "deposit"
    '    cnnBuiler_BI.Password = "deposit"
    '    cnnBuiler_BI.Unicode = True

    '    Using cnnBI_Connection As New OracleConnection(cnnBuiler_BI.ConnectionString)

    '        Dim cmd_BI As OracleCommand = cnnBI_Connection.CreateCommand()

    '        Dim tadpWarningIntervalBranchList As New BusinessObject.dstHadiWarningIntervalsBranchTableAdapters.spr_HadiWarningIntervalsBranch_List_SelectTableAdapter
    '        Dim dtblWarningIntervalBranchList As BusinessObject.dstHadiWarningIntervalsBranch.spr_HadiWarningIntervalsBranch_List_SelectDataTable = Nothing
    '        dtblWarningIntervalBranchList = tadpWarningIntervalBranchList.GetData()

    '        If dtblWarningIntervalBranchList.Rows.Count = 0 Then
    '            cnnBI_Connection.Close()
    '            qryLogHeader.spr_HadiLogCurrentLCStatus_H_Insert(dteThisDate, Date.Now, False, "گردش کارهای تعریف شده ناقص هستند", intCurrentTryTime)

    '            Return
    '        End If


    '        Dim strLoan_Info_Query As String = "SELECT * from customer.cbcif_hadi where  (" & strIntervalText & ") and  ("
    '        '   Dim strLoan_Info_Query As String = "SELECT * from customer.cbcif_hadi where  ROWNUM <=10 and  (" & strIntervalText & ") and ("

    '        'Dim strLoan_Info_Query As String = "SELECT customer.cbcif_hadi.*,SYSDATE from customer.cbcif_hadi where ROWNUM <=10 and ("


    '        Dim strBranchQuery As String = ""
    '        For Each drwWarningIntervalBranchList As BusinessObject.dstHadiWarningIntervalsBranch.spr_HadiWarningIntervalsBranch_List_SelectRow In dtblWarningIntervalBranchList.Rows
    '            strBranchQuery &= "or ABRNCHCOD ='" & drwWarningIntervalBranchList.BrnachCode & "'"

    '        Next drwWarningIntervalBranchList

    '        strLoan_Info_Query &= strBranchQuery.Substring(3) & ")"
    '        'strLoan_Info_Query = "select * from customer.cbcif_hadi where CFCIFNO  = 10228"
    '        cmd_BI.CommandText = strLoan_Info_Query


    '        Try
    '            cnnBI_Connection.Open()
    '        Catch ex As Exception

    '            qryLogHeader.spr_HadiLogCurrentLCStatus_H_Insert(dteThisDate, Date.Now, False, ex.Message, intCurrentTryTime)
    '            Return
    '        End Try
    '        Dim dataReader As OracleDataReader = Nothing

    '        Try
    '            dataReader = cmd_BI.ExecuteReader()
    '        Catch ex As Exception

    '            qryLogHeader.spr_HadiLogCurrentLCStatus_H_Insert(dteThisDate, Date.Now, False, ex.Message, intCurrentTryTime)
    '            cnnBI_Connection.Close()

    '            Return
    '        End Try

    '        Try
    '            If dataReader.Read = False Then

    '                qryLogHeader.spr_HadiLogCurrentLCStatus_H_Insert(dteThisDate, Date.Now, False, "اطلاعات مربوط به مورخ " & mdlGeneral.GetPersianDate(dteThisDate) & " بروز رسانی نشده است. لطفا با مدیر سیستم تماس بگیرید", intCurrentTryTime)
    '                dataReader.Close()
    '                cnnBI_Connection.Close()

    '                Return
    '            End If

    '            Dim intLogHeaderID As Integer = qryLogHeader.spr_HadiLogCurrentLCStatus_H_Insert(dteThisDate, Date.Now, True, "", intCurrentTryTime)

    '            Dim qryLCCurrentStatus As New BusinessObject.dstHadiOperation_DepositTableAdapters.QueriesTableAdapter
    '            qryLCCurrentStatus.spr_HadiOperation_Deposit_Delete()

    '            Dim i As Integer = 0
    '            Dim strBuilder As New Text.StringBuilder()

    '            Dim listOperationDeposit As New ArrayList


    '            Do
    '                i += 1

    '                Try
    '                    Dim stcVarDepositInfo As stc_Deposit_Info


    '                    If dataReader.GetValue(1) Is DBNull.Value Then
    '                        stcVarDepositInfo.CSTTYP = ""
    '                    Else
    '                        stcVarDepositInfo.CSTTYP = CStr(dataReader.GetValue(2)).Replace("'", "")
    '                    End If

    '                    If dataReader.GetValue(2) Is DBNull.Value Then
    '                        stcVarDepositInfo.CustomerNo = ""
    '                    Else
    '                        stcVarDepositInfo.CustomerNo = CStr(dataReader.GetValue(2)).Replace("'", "")
    '                    End If

    '                    If dataReader.GetValue(3) Is DBNull.Value Then
    '                        stcVarDepositInfo.Name = ""
    '                    Else
    '                        stcVarDepositInfo.Name = CStr(dataReader.GetValue(3)).Replace("'", "")
    '                    End If

    '                    If dataReader.GetValue(4) Is DBNull.Value Then
    '                        stcVarDepositInfo.LastName = ""
    '                    Else
    '                        stcVarDepositInfo.LastName = CStr(dataReader.GetValue(4)).Replace("'", "")
    '                    End If

    '                    If dataReader.GetValue(5) Is DBNull.Value Then
    '                        stcVarDepositInfo.FatherName = ""
    '                    Else
    '                        stcVarDepositInfo.FatherName = CStr(dataReader.GetValue(5)).Trim.Replace("'", "")
    '                    End If

    '                    If dataReader.GetValue(6) Is DBNull.Value Then
    '                        stcVarDepositInfo.IDNo = ""
    '                    Else
    '                        stcVarDepositInfo.IDNo = CStr(dataReader.GetValue(6)).Trim.Replace("'", "")
    '                    End If

    '                    If dataReader.GetValue(7) Is DBNull.Value Then
    '                        stcVarDepositInfo.Sex = ""
    '                    Else
    '                        stcVarDepositInfo.Sex = CStr(dataReader.GetValue(7)).Trim.Replace("'", "")
    '                    End If


    '                    If dataReader.GetValue(8) Is DBNull.Value Then
    '                        stcVarDepositInfo.BirthDate = ""
    '                    Else
    '                        stcVarDepositInfo.BirthDate = CStr(dataReader.GetValue(8)).Trim.Replace("'", "")
    '                    End If

    '                    If dataReader.GetValue(9) Is DBNull.Value Then
    '                        stcVarDepositInfo.NationalID = ""
    '                    Else
    '                        stcVarDepositInfo.NationalID = CStr(dataReader.GetValue(9)).Trim.Replace("'", "")
    '                    End If


    '                    If dataReader.GetValue(10) Is DBNull.Value Then
    '                        stcVarDepositInfo.BranchCode = ""
    '                    Else
    '                        stcVarDepositInfo.BranchCode = CStr(dataReader.GetValue(10)).Trim.Replace("'", "")
    '                    End If

    '                    If dataReader.GetValue(11) Is DBNull.Value Then
    '                        stcVarDepositInfo.Date_P = ""
    '                    Else
    '                        stcVarDepositInfo.Date_P = CStr(dataReader.GetValue(11)).Trim
    '                    End If

    '                    If dataReader.GetValue(12) Is DBNull.Value Then
    '                        stcVarDepositInfo.Address = ""
    '                    Else
    '                        stcVarDepositInfo.Address = CStr(dataReader.GetValue(12)).Trim.Replace("'", "")
    '                    End If

    '                    If dataReader.GetValue(13) Is DBNull.Value Then
    '                        stcVarDepositInfo.Telephone = ""
    '                    Else
    '                        stcVarDepositInfo.Telephone = CStr(dataReader.GetValue(13)).Trim.Replace("'", "")
    '                    End If


    '                    If dataReader.GetValue(14) Is DBNull.Value Then
    '                        stcVarDepositInfo.Mobile = ""
    '                    Else
    '                        stcVarDepositInfo.Mobile = CStr(dataReader.GetValue(14)).Trim.Replace("'", "")
    '                    End If

    '                    If dataReader.GetValue(16) Is DBNull.Value Then
    '                        stcVarDepositInfo.DepositTypeCode = ""
    '                    Else
    '                        stcVarDepositInfo.DepositTypeCode = CStr(dataReader.GetValue(16)).Trim.Replace("'", "")
    '                    End If

    '                    If dataReader.GetValue(17) Is DBNull.Value Then
    '                        stcVarDepositInfo.DepositDesc = ""
    '                    Else
    '                        stcVarDepositInfo.DepositDesc = CStr(dataReader.GetValue(17)).Trim.Replace("'", "")
    '                    End If


    '                    stcVarDepositInfo.BranchName = ""
    '                    stcVarDepositInfo.BranchAddress = ""
    '                    stcVarDepositInfo.DepositAmount = ""

    '                    Dim tadpFilebyCustomerNo As New BusinessObject.dstFileTableAdapters.spr_File_CustomerNo_SelectTableAdapter
    '                    Dim dtblFilebyCustomerNo As BusinessObject.dstFile.spr_File_CustomerNo_SelectDataTable = Nothing
    '                    dtblFilebyCustomerNo = tadpFilebyCustomerNo.GetData(stcVarDepositInfo.CustomerNo)
    '                    Dim intBorrowerFileID As Integer = -1


    '                    ''check if Customer Number is double or not, if it is then skip it
    '                    If i = 1 Then

    '                        listOperationDeposit.Add(stcVarDepositInfo.CustomerNo)

    '                    Else


    '                        For Each obj In listOperationDeposit
    '                            If obj = stcVarDepositInfo.CustomerNo Then
    '                                Continue Do
    '                            End If
    '                        Next

    '                        listOperationDeposit.Add(stcVarDepositInfo.CustomerNo)

    '                    End If

    '                    If dtblFilebyCustomerNo.Rows.Count = 0 Then

    '                        Dim qryFile As New BusinessObject.dstFileTableAdapters.QueriesTableAdapter

    '                        Dim blnIsMale As Boolean = If(stcVarDepositInfo.Sex = "زن", False, True)
    '                        intBorrowerFileID = qryFile.spr_File_Insert(stcVarDepositInfo.CustomerNo, stcVarDepositInfo.Name, stcVarDepositInfo.LastName, stcVarDepositInfo.FatherName, stcVarDepositInfo.Mobile, stcVarDepositInfo.NationalID, stcVarDepositInfo.IDNo, "", stcVarDepositInfo.Address, stcVarDepositInfo.Telephone, stcVarDepositInfo.Telephone, blnIsMale, 2, 1, Nothing, Nothing)

    '                    Else

    '                        Dim drwFilebyCustomerNo As BusinessObject.dstFile.spr_File_CustomerNo_SelectRow = dtblFilebyCustomerNo.Rows(0)
    '                        intBorrowerFileID = drwFilebyCustomerNo.ID

    '                        If blnIsUpdateDay = True Then
    '                            Try

    '                                Dim blnIsMale As Boolean = If(stcVarDepositInfo.Sex = "زن", False, True)
    '                                Dim qryFile As New BusinessObject.dstFileTableAdapters.QueriesTableAdapter
    '                                qryFile.spr_File_MAT_Update(intBorrowerFileID, stcVarDepositInfo.Mobile, stcVarDepositInfo.Address, stcVarDepositInfo.Telephone, stcVarDepositInfo.Telephone, stcVarDepositInfo.Name, stcVarDepositInfo.LastName, stcVarDepositInfo.FatherName, blnIsMale)

    '                            Catch ex As Exception

    '                                '
    '                                Dim qryErrorLog As New DataSet1TableAdapters.QueriesTableAdapter
    '                                qryErrorLog.spr_ErrorLog_Insert(ex.Message, 1, "Hadi_BI_Deposit")


    '                            End Try

    '                        End If


    '                    End If




    '                    Dim tadpDepositByNumber As New BusinessObject.dstDepositTableAdapters.spr_Deposits_ByDepositNumber_SelectTableAdapter
    '                    Dim dtblDepositByNumber As BusinessObject.dstDeposit.spr_Deposits_ByDepositNumber_SelectDataTable = Nothing
    '                    dtblDepositByNumber = tadpDepositByNumber.GetData(stcVarDepositInfo.CSTTYP, intBorrowerFileID)

    '                    Dim intDepositID As Integer = -1
    '                    Dim intBranchID As Integer = -1
    '                    Dim intDepositTypeID As Integer = -1


    '                    If dtblDepositByNumber.Rows.Count = 0 Then
    '                        Dim qryDeposit As New BusinessObject.dstDepositTableAdapters.QueriesTableAdapter


    '                        Dim dteDepositDate? As Date = Nothing
    '                        Try

    '                            ''stcVarDepositInfo.Date_P = stcVarDepositInfo.Date_P.Insert(4, "/")
    '                            ''stcVarDepositInfo.Date_P = stcVarDepositInfo.Date_P.Insert(7, "/")
    '                            ''dteDepositDate = mdlGeneral.GetGregorianDate(stcVarDepositInfo.Date_P)
    '                            dteDepositDate = CDate(stcVarDepositInfo.Date_P)
    '                        Catch ex As Exception
    '                            dteDepositDate = Nothing
    '                        End Try



    '                        Dim tadpBranchbyCode As New BusinessObject.dstBranchTableAdapters.spr_Branch_ByCode_SelectTableAdapter
    '                        Dim dtblBranchbyCode As BusinessObject.dstBranch.spr_Branch_ByCode_SelectDataTable = Nothing
    '                        dtblBranchbyCode = tadpBranchbyCode.GetData(stcVarDepositInfo.BranchCode)


    '                        If dtblBranchbyCode.Rows.Count = 0 Then


    '                            Dim obj_stc_BranchInfo As stc_Branch_Info = GetBarnchName(stcVarDepositInfo.BranchCode)
    '                            Dim qryBranch As New BusinessObject.dstBranchTableAdapters.QueriesTableAdapter

    '                            intBranchID = qryBranch.spr_Branch_Insert(stcVarDepositInfo.BranchCode, obj_stc_BranchInfo.BranchName, obj_stc_BranchInfo.BranchAddress, 2, Nothing, "", 1) 'UserID=2 is System User

    '                        Else

    '                            Dim drwBranchbyCode As BusinessObject.dstBranch.spr_Branch_ByCode_SelectRow = dtblBranchbyCode.Rows(0)
    '                            intBranchID = drwBranchbyCode.ID

    '                        End If


    '                        Dim tadpDepositTypeByCode As New BusinessObject.dstDepositTableAdapters.spr_DepositType_byCode_SelectTableAdapter
    '                        Dim dtblDepositTypeByCode As BusinessObject.dstDeposit.spr_DepositType_byCode_SelectDataTable = Nothing

    '                        dtblDepositTypeByCode = tadpDepositTypeByCode.GetData(stcVarDepositInfo.DepositTypeCode)


    '                        If dtblDepositTypeByCode.Rows.Count = 0 Then

    '                            Dim strDepositTypeName As String = stcVarDepositInfo.DepositDesc ' GetDepositTypeName(stcVarDepositInfo.DepositTypeCode)
    '                            ' Insert into DepositType
    '                            intDepositTypeID = qryDeposit.spr_DepositType_Insert(stcVarDepositInfo.DepositTypeCode, strDepositTypeName, 2)



    '                        Else
    '                            Dim drwSDepositTypeByCode As BusinessObject.dstDeposit.spr_DepositType_byCode_SelectRow = dtblDepositTypeByCode.Rows(0)
    '                            intDepositTypeID = drwSDepositTypeByCode.ID


    '                        End If


    '                        intDepositID = qryDeposit.spr_Deposits_Insert(intBorrowerFileID, intDepositTypeID, 0, intBranchID, stcVarDepositInfo.CSTTYP, dteDepositDate)


    '                    Else

    '                        Dim drwDeposit As BusinessObject.dstDeposit.spr_Deposits_ByDepositNumber_SelectRow = dtblDepositByNumber.Rows(0)
    '                        intDepositID = drwDeposit.ID
    '                        intDepositTypeID = drwDeposit.FK_DepositTypeID
    '                        intBranchID = drwDeposit.FK_BranchID

    '                        ''stcVarDepositInfo.Date_P = stcVarDepositInfo.Date_P.Insert(4, "/")
    '                        ''stcVarDepositInfo.Date_P = stcVarDepositInfo.Date_P.Insert(7, "/")

    '                    End If

    '                    With stcVarDepositInfo

    '                        ''Dim strTempInsertQuery As String = " union select '" & dteThisDate & "'," & .Date_P.ToString & "," & intBorrowerFileID.ToString & "," & .Date_P.ToString
    '                        ''strTempInsertQuery &= ",'" & intBranchID.ToString() & "'," & .CSTTYP.ToString & "," & intDepositTypeID.ToString & "," & intDepositID.ToString & ",0"
    '                        ''strBuilder.Append(strTempInsertQuery)
    '                        ''stcVarDepositInfo.Date_P = stcVarDepositInfo.Date_P.Insert(4, "/")
    '                        ''stcVarDepositInfo.Date_P = stcVarDepositInfo.Date_P.Insert(7, "/")
    '                        ''  Dim dteDepositDate As Date = mdlGeneral.GetGregorianDate(stcVarDepositInfo.Date_P)



    '                        Dim dteDepositDate As Date = CDate(stcVarDepositInfo.Date_P)

    '                        Dim strTempInsertQuery As String = " union select  & dteThisDate & "'," & intBorrowerFileID.ToString & ",'" & dteDepositDate.ToString()
    '                        strTempInsertQuery &= "'," & intBranchID.ToString() & "," & .CSTTYP.ToString & "," & intDepositTypeID.ToString & "," & intDepositID.ToString & ",0"
    '                        strBuilder.Append(strTempInsertQuery)

    '                        ''strTempInsertQuery.Split(",").Find()

    '                        If i >= 500 Then
    '                            Dim strMainIntertQuery As String = strBuilder.ToString.Substring(7)
    '                            qryLCCurrentStatus.spr_HadiOperation_Deposit_Bulk_Insert(strMainIntertQuery)
    '                            i = 0
    '                            strBuilder.Clear()

    '                        End If


    '                    End With


    '                Catch ex As Exception
    '                    Continue Do
    '                End Try


    '            Loop While dataReader.Read()
    '            dataReader.Close()



    '            If i <> 0 Then
    '                Dim strMainInsertQuery As String = strBuilder.ToString.Substring(6)
    '                qryLCCurrentStatus.spr_HadiOperation_Deposit_Bulk_Insert(strMainInsertQuery)

    '            End If

    '            listOperationDeposit.Clear()

    '        Catch ex As Exception

    '            qryLogHeader.spr_HadiLogCurrentLCStatus_H_Insert(dteThisDate, Date.Now, False, ex.Message, intCurrentTryTime)
    '            Return
    '        End Try

    '        cnnBI_Connection.Close()

    '    End Using
    '    ''Call SendAdministratioSMSMessage()


    'End Sub

    Public Sub Hadi_BI_Laon()

        Dim tadpSystemSetting As New BusinessObject.dstSystemSettingTableAdapters.spr_SystemSetting_SelectTableAdapter
        Dim dtblSystemSetting As BusinessObject.dstSystemSetting.spr_SystemSetting_SelectDataTable = Nothing
        dtblSystemSetting = tadpSystemSetting.GetData()
        drwSystemSetting = dtblSystemSetting.Rows(0)

        If drwSystemSetting.HadiServiceLoan = False Then
            Return
        End If
        If drwSystemSetting.tryTime_Loan = 0 Then
            Return
        End If

        If drwSystemSetting.UpdateTime_Loan > Date.Now.TimeOfDay Then
            Return
        End If

        Dim dteThisDate As Date = Date.Now 'Date.Now.AddDays(-1)

        Dim blnIsUpdateDay As Boolean = False
        If Date.Now.DayOfWeek = DayOfWeek.Sunday OrElse Date.Now.DayOfWeek = DayOfWeek.Tuesday Then
            blnIsUpdateDay = True
        End If


        Dim tadpLogHeader As New BusinessObject.dstHadiLogLoanStatusTableAdapters.spr_HadiLogLoanStatus_ForDate_SelectTableAdapter
        Dim dtblLogHeader As BusinessObject.dstHadiLogLoanStatus.spr_HadiLogLoanStatus_ForDate_SelectDataTable = Nothing
        dtblLogHeader = tadpLogHeader.GetData(dteThisDate)

        Dim intCurrentTryTime As Integer = 0

        If dtblLogHeader.Rows.Count > 0 Then
            Dim drwLogHeader As BusinessObject.dstHadiLogLoanStatus.spr_HadiLogLoanStatus_ForDate_SelectRow = dtblLogHeader.Rows(0)
            If drwLogHeader.Success = True Then
                Return
            End If

            If drwLogHeader.tryTime >= drwSystemSetting.tryTime_Loan Then
                Return
            End If

            If Date.Now.Subtract(drwLogHeader.STime).TotalHours < drwSystemSetting.tryIntervalHour_Loan Then
                Return
            End If

            intCurrentTryTime = drwLogHeader.tryTime

        End If


        Dim tadpIntervalList As New DataSet1TableAdapters.spr_HadiWarningIntervals_Inerval_List_SelectTableAdapter
        Dim dtblIntervalList As DataSet1.spr_HadiWarningIntervals_Inerval_List_SelectDataTable = Nothing
        dtblIntervalList = tadpIntervalList.GetData(2)

        Dim strIntervalText As String = ""

        For Each drwIntervalList As DataSet1.spr_HadiWarningIntervals_Inerval_List_SelectRow In dtblIntervalList.Rows

            '' strIntervalText &= " or (NPDURATION between " & drwIntervalList.FromDay & " and " & drwIntervalList.ToDay & " )"


            ''Dim strFromDate As String = mdlGeneral.GetPersianDate(Date.Now.AddDays(-drwIntervalList.FromDay)).Replace("/", "")
            ''Dim strToDate As String = mdlGeneral.GetPersianDate(Date.Now.AddDays(-drwIntervalList.ToDay)).Replace("/", "")

            Dim strFromDate As String = Date.Now.AddDays(-drwIntervalList.FromDay)
            Dim strToDate As String = Date.Now.AddDays(-drwIntervalList.ToDay)


            strIntervalText &= " Or (lc_date  between " & "to_date('" & strToDate & "','yyyymmdd')" & " And " & "to_date('" & strFromDate & "','yyyymmdd')" & ")"


            ''   strIntervalText &= " Or to_char(LCDATE)  between " & "to_char(" & strToDate & ")" & " And " & " to_char(" & strFromDate & ")"

        Next drwIntervalList

        strIntervalText = strIntervalText.Substring(3)


        intCurrentTryTime += 1


        Dim qryLogHeader As New BusinessObject.dstHadiLogLoanStatusTableAdapters.QueriesTableAdapter



        Dim strThisDatePersian As String = mdlGeneral.GetPersianDate(dteThisDate).Replace("/", "")

        Dim cnnBuiler_BI As New OracleConnectionStringBuilder()
        cnnBuiler_BI.DataSource = "10.35.1.37:1522/bidb"
        cnnBuiler_BI.UserID = "deposit"
        cnnBuiler_BI.Password = "deposit"
        cnnBuiler_BI.Unicode = True

        Using cnnBI_Connection As New OracleConnection(cnnBuiler_BI.ConnectionString)

            Dim cmd_BI As OracleCommand = cnnBI_Connection.CreateCommand()

            Dim tadpWarningIntervalBranchList As New BusinessObject.dstHadiWarningIntervalsBranchTableAdapters.spr_HadiWarningIntervalsBranch_List_SelectTableAdapter
            Dim dtblWarningIntervalBranchList As BusinessObject.dstHadiWarningIntervalsBranch.spr_HadiWarningIntervalsBranch_List_SelectDataTable = Nothing
            dtblWarningIntervalBranchList = tadpWarningIntervalBranchList.GetData()

            If dtblWarningIntervalBranchList.Rows.Count = 0 Then
                cnnBI_Connection.Close()
                qryLogHeader.spr_HadiLogLoanStatus_Insert(dteThisDate, Date.Now, False, "گردش کارهای تعریف شده ناقص هستند", intCurrentTryTime)

                Return
            End If


            '  Dim strLoan_Info_Query As String = "SELECT * from loan_info where Date_P='" & strThisDatePersian & "' and state in ('3','4','5','F') and (NPDURATION between " & intMinInterval & " and " & intMaxInterval & ") and ("
            ''   Dim strLoan_Info_Query As String = "SELECT * from lc_hadi where lc_date='" & "to_date('" & dteThisDate.ToString() & "','yyyymmdd')" & "' and " & strIntervalText & " and ("
            Dim strLoan_Info_Query As String = "SELECT * from lc_hadi where " & strIntervalText & " and ("





            Dim strBranchQuery As String = ""
            For Each drwWarningIntervalBranchList As BusinessObject.dstHadiWarningIntervalsBranch.spr_HadiWarningIntervalsBranch_List_SelectRow In dtblWarningIntervalBranchList.Rows
                strBranchQuery &= "or branch_code='" & drwWarningIntervalBranchList.BrnachCode & "'"

            Next drwWarningIntervalBranchList

            strLoan_Info_Query &= strBranchQuery.Substring(3) & ")"

            cmd_BI.CommandText = strLoan_Info_Query

            Try
                cnnBI_Connection.Open()
            Catch ex As Exception

                qryLogHeader.spr_HadiLogLoanStatus_Insert(dteThisDate, Date.Now, False, ex.Message, intCurrentTryTime)
                Return
            End Try
            Dim dataReader As OracleDataReader = Nothing

            Try
                dataReader = cmd_BI.ExecuteReader()
            Catch ex As Exception

                qryLogHeader.spr_HadiLogLoanStatus_Insert(dteThisDate, Date.Now, False, ex.Message, intCurrentTryTime)
                cnnBI_Connection.Close()

                Return
            End Try

            Try
                If dataReader.Read = False Then

                    qryLogHeader.spr_HadiLogLoanStatus_Insert(dteThisDate, Date.Now, False, "اطلاعات مربوط به مورخ " & mdlGeneral.GetPersianDate(dteThisDate) & " بروز رسانی نشده است. لطفا با مدیر سیستم تماس بگیرید", intCurrentTryTime)
                    dataReader.Close()
                    cnnBI_Connection.Close()

                    Return
                End If

                Dim intLogHeaderID As Integer = qryLogHeader.spr_HadiLogLoanStatus_Insert(dteThisDate, Date.Now, True, "", intCurrentTryTime)

                Dim qryLCCurrentStatus As New BusinessObject.dstHadiOperation_LoanTableAdapters.QueriesTableAdapter
                qryLCCurrentStatus.spr_HadiOperation_Loan_Delete()

                Dim i As Integer = 0
                Dim strBuilder As New Text.StringBuilder()
                Dim qryCurrentLCStatus As New BusinessObject.dstHadiOperation_LoanTableAdapters.QueriesTableAdapter

                Dim listOperationLoan As New ArrayList

                Do
                    i += 1
                    Try
                        Dim stcVarLoanInfo As stc_Loan_Hadi

                        If dataReader.GetValue(0) Is DBNull.Value Then
                            stcVarLoanInfo.CustomerNO = ""
                        Else
                            stcVarLoanInfo.CustomerNO = CStr(dataReader.GetValue(0))
                        End If


                        If dataReader.GetValue(1) Is DBNull.Value Then
                            stcVarLoanInfo.Name = ""
                        Else
                            stcVarLoanInfo.Name = CStr(dataReader.GetValue(1))
                        End If

                        If dataReader.GetValue(2) Is DBNull.Value Then
                            stcVarLoanInfo.LastName = ""
                        Else
                            stcVarLoanInfo.LastName = CStr(dataReader.GetValue(2))
                        End If

                        If dataReader.GetValue(3) Is DBNull.Value Then
                            stcVarLoanInfo.Gender = ""
                        Else
                            stcVarLoanInfo.Gender = CStr(dataReader.GetValue(3)).Trim
                        End If

                        If dataReader.GetValue(4) Is DBNull.Value Then
                            stcVarLoanInfo.Mobile = ""
                        Else
                            stcVarLoanInfo.Mobile = CStr(dataReader.GetValue(4)).Trim
                        End If

                        If dataReader.GetValue(5) Is DBNull.Value Then
                            stcVarLoanInfo.LCDateMiladi = ""
                        Else
                            stcVarLoanInfo.LCDateMiladi = CStr(dataReader.GetValue(5)).Trim
                        End If

                        If dataReader.GetValue(6) Is DBNull.Value Then
                            i -= 1
                            Continue Do
                        Else
                            stcVarLoanInfo.BranchCode = CStr(dataReader.GetValue(6)).Trim
                        End If


                        If dataReader.GetValue(7) Is DBNull.Value Then
                            i -= 1
                            Continue Do
                        Else
                            stcVarLoanInfo.LoanTypeCode = CStr(dataReader.GetValue(7)).Trim
                        End If



                        If dataReader.GetValue(8) Is DBNull.Value Then
                            i -= 1
                            Continue Do
                        Else
                            stcVarLoanInfo.SerialNumber = CInt(dataReader.GetValue(8))
                        End If



                        If dataReader.GetValue(9) Is DBNull.Value Then
                            stcVarLoanInfo.LoanAmount = Nothing
                        Else
                            stcVarLoanInfo.LoanAmount = CDbl(dataReader.GetValue(9))
                        End If

                        If dataReader.GetValue(10) Is DBNull.Value Then
                            stcVarLoanInfo.LoanTitle = Nothing
                        Else
                            stcVarLoanInfo.LoanTitle = CDbl(dataReader.GetValue(10))
                        End If

                        If dataReader.GetValue(11) Is DBNull.Value Then
                            stcVarLoanInfo.LoanState = Nothing
                        Else
                            stcVarLoanInfo.LoanState = CInt(dataReader.GetValue(11))
                        End If




                        ''  stcVarLoanInfo.Status = CStr(dataReader.GetValue(20)).Trim



                        'If dataReader.GetValue(30) Is DBNull.Value Then
                        '    stcVarLoanInfo.NationalID = ""
                        'Else
                        '    stcVarLoanInfo.NationalID = CStr(dataReader.GetValue(30))
                        'End If


                        'If dataReader.GetValue(31) Is DBNull.Value Then
                        '    stcVarLoanInfo.NationalNo = ""
                        'Else
                        '    stcVarLoanInfo.NationalNo = CStr(dataReader.GetValue(31))
                        'End If



                        ''Generate LC Number
                        Dim strLCNO As String = stcVarLoanInfo.BranchCode & "-" & stcVarLoanInfo.LoanTypeCode & "-" & stcVarLoanInfo.CustomerNO & "-"



                        Dim tadpFilebyCustomerNo As New BusinessObject.dstFileTableAdapters.spr_File_CustomerNo_SelectTableAdapter
                        Dim dtblFilebyCustomerNo As BusinessObject.dstFile.spr_File_CustomerNo_SelectDataTable = Nothing
                        dtblFilebyCustomerNo = tadpFilebyCustomerNo.GetData(stcVarLoanInfo.CustomerNo)
                        Dim intBorrowerFileID As Integer = -1


                        ''check if Customer Number is double or not, if it is then skip it
                        If i = 1 Then

                            listOperationLoan.Add(stcVarLoanInfo.CustomerNo)

                        Else


                            For Each obj In listOperationLoan
                                If obj = stcVarLoanInfo.CustomerNo Then
                                    Continue Do
                                End If
                            Next

                            listOperationLoan.Add(stcVarLoanInfo.CustomerNo)

                        End If


                        If dtblFilebyCustomerNo.Rows.Count = 0 Then

                            Dim qryFile As New BusinessObject.dstFileTableAdapters.QueriesTableAdapter



                            Dim blnIsMale As Boolean = If(stcVarLoanInfo.Gender = "زن", False, True)
                            intBorrowerFileID = qryFile.spr_File_Insert(stcVarLoanInfo.CustomerNO, stcVarLoanInfo.Name, stcVarLoanInfo.LastName, "", stcVarLoanInfo.Mobile, "", "", "", "", "", "", blnIsMale, 2, 1, Nothing, Nothing)

                        Else

                            Dim drwFilebyCustomerNo As BusinessObject.dstFile.spr_File_CustomerNo_SelectRow = dtblFilebyCustomerNo.Rows(0)
                            intBorrowerFileID = drwFilebyCustomerNo.ID

                            'If blnIsUpdateDay = True Then
                            '    Try
                            '        Dim arrFullName() As String = stcVarLoanInfo.FullName.Split("*")
                            '        Dim strFatherName As String = arrFullName(0)
                            '        Dim strFName As String = arrFullName(1)
                            '        Dim strLName As String = arrFullName(2)
                            '        Dim blnIsMale As Boolean = If(stcVarLoanInfo.Sex = "زن", False, True)
                            '        Dim qryFile As New BusinessObject.dstFileTableAdapters.QueriesTableAdapter
                            '        qryFile.spr_File_MAT_Update(intBorrowerFileID, stcVarLoanInfo.Mobile, stcVarLoanInfo.Address, stcVarLoanInfo.Telephone, stcVarLoanInfo.Telephone, strFName, strLName, strFatherName, blnIsMale)
                            '    Catch ex As Exception

                            '    End Try

                            'End If


                        End If


                        Dim tadpLoanByNumber As New BusinessObject.dstLoanTableAdapters.spr_Loan_ByLoanNumber_SelectTableAdapter
                        Dim dtblLoanByNumber As BusinessObject.dstLoan.spr_Loan_ByLoanNumber_SelectDataTable = Nothing
                        dtblLoanByNumber = tadpLoanByNumber.GetData(strLCNO, intBorrowerFileID)


                        Dim intLoanID As Integer = -1
                        Dim intBranchID As Integer = -1
                        Dim intLoanTypeID As Integer = -1

                        If dtblLoanByNumber.Rows.Count = 0 Then
                            Dim qryLoan As New BusinessObject.dstLoanTableAdapters.QueriesTableAdapter


                            Dim dteLoanDate? As Date = Nothing
                            Try
                                'Dim strDate1 As String = stcVarLoanInfo.LCDate.Insert(4, "/")
                                'Dim strDate2 As String = strDate1.Insert(7, "/")
                                dteLoanDate = stcVarLoanInfo.LCDateMiladi 'mdlGeneral.GetGregorianDate(strDate2)
                            Catch ex As Exception
                                dteLoanDate = Nothing
                            End Try

                            Dim tadpBranchbyCode As New BusinessObject.dstBranchTableAdapters.spr_Branch_ByCode_SelectTableAdapter
                            Dim dtblBranchbyCode As BusinessObject.dstBranch.spr_Branch_ByCode_SelectDataTable = Nothing
                            dtblBranchbyCode = tadpBranchbyCode.GetData(stcVarLoanInfo.BranchCode)



                            If dtblBranchbyCode.Rows.Count = 0 Then


                                Dim obj_stc_BranchInfo As stc_Branch_Info = GetBarnchName(stcVarLoanInfo.BranchCode)


                                Dim qryBranch As New BusinessObject.dstBranchTableAdapters.QueriesTableAdapter

                                intBranchID = qryBranch.spr_Branch_Insert(stcVarLoanInfo.BranchCode, obj_stc_BranchInfo.BranchName, obj_stc_BranchInfo.BranchAddress, 2, Nothing, "", 1) 'UserID=2 is System User



                            Else
                                Dim drwBranchbyCode As BusinessObject.dstBranch.spr_Branch_ByCode_SelectRow = dtblBranchbyCode.Rows(0)
                                intBranchID = drwBranchbyCode.ID

                            End If


                            Dim tadpLoanTypeByCode As New BusinessObject.dstLoanTypeTableAdapters.spr_LoanType_byCode_SelectTableAdapter
                            Dim dtblLoanTypeByCode As BusinessObject.dstLoanType.spr_LoanType_byCode_SelectDataTable = Nothing
                            dtblLoanTypeByCode = tadpLoanTypeByCode.GetData(stcVarLoanInfo.LoanTypeCode)



                            If dtblLoanTypeByCode.Rows.Count = 0 Then

                                Dim strLoanTypeName As String = GetLoanTypeName(stcVarLoanInfo.LoanTypeCode)
                                Dim qryLoanType As New BusinessObject.dstLoanTypeTableAdapters.QueriesTableAdapter
                                intLoanTypeID = qryLoanType.spr_LoanType_Insert(stcVarLoanInfo.LoanTypeCode, strLoanTypeName, 2, "")


                            Else
                                Dim drwLoanTypeByCode As BusinessObject.dstLoanType.spr_LoanType_byCode_SelectRow = dtblLoanTypeByCode.Rows(0)
                                intLoanTypeID = drwLoanTypeByCode.ID


                            End If


                            intLoanID = qryLoan.spr_Loan_Insert(intBorrowerFileID, intLoanTypeID, intBranchID, dteLoanDate, strLCNO, stcVarLoanInfo.SerialNumber, Date.Now, stcVarLoanInfo.LoanAmount, Nothing)



                        Else


                            Dim drwLoanByNumber As BusinessObject.dstLoan.spr_Loan_ByLoanNumber_SelectRow = dtblLoanByNumber.Rows(0)
                            intLoanID = drwLoanByNumber.ID

                            intLoanTypeID = drwLoanByNumber.FK_LoanTypeID
                            intBranchID = drwLoanByNumber.FK_BranchID



                        End If


                        With stcVarLoanInfo

                            'Dim strDate1 As String = stcVarLoanInfo.LCDate.Insert(4, "/")
                            'Dim strDate2 As String = strDate1.Insert(7, "/")
                            Dim dteLoanDate As Date = stcVarLoanInfo.LCDateMiladi 'mdlGeneral.GetGregorianDate(strDate2)

                            Dim strTempInsertQuery As String = " union select '" & stcVarLoanInfo.LoanAmount.ToString & "," & ""
                            strTempInsertQuery &= ",'" & stcVarLoanInfo.LoanState & "," & intBorrowerFileID.ToString & "," & intLoanTypeID.ToString & "," & ",0" & "," & intBranchID.ToString() & ",'" & dteLoanDate.ToString() & "'" & "," & intLoanID.ToString()
                            strBuilder.Append(strTempInsertQuery)


                            If i >= 500 Then
                                Dim strMainIntertQuery As String = strBuilder.ToString.Substring(7)
                                qryCurrentLCStatus.spr_HadiOperation_Loan_Bulk_Insert(strMainIntertQuery)
                                i = 0
                                strBuilder.Clear()

                            End If



                        End With



                    Catch ex As Exception
                        Continue Do
                    End Try


                Loop While dataReader.Read()
                dataReader.Close()

                If i <> 0 Then
                    Dim strMainInsertQuery As String = strBuilder.ToString.Substring(6)
                    qryCurrentLCStatus.spr_HadiOperation_Loan_Bulk_Insert(strMainInsertQuery)

                End If

                listOperationLoan.Clear()


            Catch ex As Exception

                qryLogHeader.spr_HadiLogLoanStatus_Insert(dteThisDate, Date.Now, False, ex.Message, intCurrentTryTime)
                Return
            End Try

            cnnBI_Connection.Close()

        End Using
        ''   Call SendAdministratioSMSMessage()
    End Sub


    Private Function GetBarnchName(ByVal strBranchCode As String) As stc_Branch_Info


        Dim obj_stc_Branch_Info As stc_Branch_Info
        obj_stc_Branch_Info.BranchAddress = ""
        obj_stc_Branch_Info.BranchName = strBranchCode

        Try


            Dim cnnBuiler_BI As New OracleConnectionStringBuilder()
            cnnBuiler_BI.DataSource = "10.35.1.37:1522/bidb"
            cnnBuiler_BI.UserID = "deposit"
            cnnBuiler_BI.Password = "deposit"
            cnnBuiler_BI.Unicode = True

            Using cnnBI_Connection As New OracleConnection(cnnBuiler_BI.ConnectionString)

                Dim cmd_BI As OracleCommand = cnnBI_Connection.CreateCommand()
                Dim strBrnaches As String = "SELECT * from BRANCHES where branch_code='" & strBranchCode & "'"
                cmd_BI.CommandText = strBrnaches

                Try
                    cnnBI_Connection.Open()
                Catch ex As Exception
                    Return obj_stc_Branch_Info
                End Try

                Dim dataReader As OracleDataReader = Nothing
                dataReader = cmd_BI.ExecuteReader()

                If dataReader.Read = False Then
                    dataReader.Close()
                    cnnBI_Connection.Close()
                    Return obj_stc_Branch_Info
                End If


                If dataReader.GetValue(6) Is DBNull.Value Then
                    obj_stc_Branch_Info.BranchName = strBranchCode
                Else
                    obj_stc_Branch_Info.BranchName = CStr(dataReader.GetValue(6)).Trim.Replace("'", "")
                End If

                If dataReader.GetValue(7) Is DBNull.Value Then
                    obj_stc_Branch_Info.BranchAddress = ""
                Else
                    obj_stc_Branch_Info.BranchAddress = CStr(dataReader.GetValue(7)).Trim.Replace("'", "")
                End If


                dataReader.Close()
                cnnBI_Connection.Close()

            End Using

            Return obj_stc_Branch_Info
        Catch ex As Exception
            Return obj_stc_Branch_Info
        End Try
    End Function

    Private Function GetLoanTypeName(ByVal strLoanTypeCode As String) As String
        Try
            Dim strLoanTypeDesc As String = strLoanTypeCode

            Dim cnnBuiler_BI As New OracleConnectionStringBuilder()
            cnnBuiler_BI.DataSource = "10.35.1.37:1522/bidb"
            cnnBuiler_BI.UserID = "deposit"
            cnnBuiler_BI.Password = "deposit"
            cnnBuiler_BI.Unicode = True

            Using cnnBI_Connection As New OracleConnection(cnnBuiler_BI.ConnectionString)

                Dim cmd_BI As OracleCommand = cnnBI_Connection.CreateCommand()
                Dim strLoanType As String = "SELECT * from lfloantyp where LNMINORTP='" & strLoanTypeCode & "'"
                cmd_BI.CommandText = strLoanType

                Try
                    cnnBI_Connection.Open()
                Catch ex As Exception
                    Return strLoanTypeDesc
                End Try

                Dim dataReader As OracleDataReader = Nothing
                dataReader = cmd_BI.ExecuteReader()

                If dataReader.Read = False Then
                    dataReader.Close()
                    cnnBI_Connection.Close()
                    Return strLoanTypeDesc
                End If


                If dataReader.GetValue(3) Is DBNull.Value Then
                    strLoanTypeDesc = strLoanTypeCode
                Else
                    strLoanTypeDesc = CStr(dataReader.GetValue(3)).Trim.Replace("'", "")
                End If



                dataReader.Close()
                cnnBI_Connection.Close()

            End Using

            Return strLoanTypeDesc

        Catch ex As Exception
            Return strLoanTypeCode
        End Try

    End Function

    Private Function GetDepositTypeName(ByVal strDepositTypeCode As String) As String
        Try
            Dim strDepositTypeDesc As String = strDepositTypeCode

            Dim cnnBuiler_BI As New OracleConnectionStringBuilder()
            cnnBuiler_BI.DataSource = "10.35.1.37:1522/bidb"
            cnnBuiler_BI.UserID = "deposit"
            cnnBuiler_BI.Password = "deposit"
            cnnBuiler_BI.Unicode = True

            Using cnnBI_Connection As New OracleConnection(cnnBuiler_BI.ConnectionString)

                Dim cmd_BI As OracleCommand = cnnBI_Connection.CreateCommand()
                Dim strLoanType As String = "SELECT * from lfloantyp where LNMINORTP='" & strDepositTypeCode & "'"
                cmd_BI.CommandText = strLoanType

                Try
                    cnnBI_Connection.Open()
                Catch ex As Exception
                    Return strDepositTypeDesc
                End Try

                Dim dataReader As OracleDataReader = Nothing
                dataReader = cmd_BI.ExecuteReader()

                If dataReader.Read = False Then
                    dataReader.Close()
                    cnnBI_Connection.Close()
                    Return strDepositTypeDesc
                End If


                If dataReader.GetValue(3) Is DBNull.Value Then
                    strDepositTypeDesc = strDepositTypeCode
                Else
                    strDepositTypeDesc = CStr(dataReader.GetValue(3)).Trim
                End If



                dataReader.Close()
                cnnBI_Connection.Close()

            End Using

            Return strDepositTypeDesc

        Catch ex As Exception
            Return strDepositTypeCode
        End Try

    End Function

    Private Function GetSponsorsList(ByVal strBranchCode As String, ByVal strLoanTypeCode As String, ByVal strCustomerNo As String, ByVal intLoanSerial As Integer, ByVal blnIsUpdateDay As Boolean) As stc_Sponsor_Waranty()


        Dim obj_stc_Sponsor_Waranty() As stc_Sponsor_Waranty = Nothing

        Dim tadpSponsorList As New BusinessObject.dstSponsor_ListTableAdapters.spr_Sponsors_List_ByLoanNumber_SelectTableAdapter
        Dim dtblSponsorList As BusinessObject.dstSponsor_List.spr_Sponsors_List_ByLoanNumber_SelectDataTable = Nothing
        dtblSponsorList = tadpSponsorList.GetData(strBranchCode, strLoanTypeCode, strCustomerNo, intLoanSerial)

        If dtblSponsorList.Rows.Count = 0 Then
            Return Nothing
        End If

        For Each drwSponsorList As BusinessObject.dstSponsor_List.spr_Sponsors_List_ByLoanNumber_SelectRow In dtblSponsorList.Rows


            Dim tadpFilebyCustomerNo As New BusinessObject.dstFileTableAdapters.spr_File_CustomerNo_SelectTableAdapter
            Dim dtblFilebyCustomerNo As BusinessObject.dstFile.spr_File_CustomerNo_SelectDataTable = Nothing
            dtblFilebyCustomerNo = tadpFilebyCustomerNo.GetData(drwSponsorList.SponsorCustomerNo)
            Dim intSponsorID As Integer = -1


            If dtblFilebyCustomerNo.Rows.Count = 0 Then

                Dim qryFile As New BusinessObject.dstFileTableAdapters.QueriesTableAdapter

                Dim strFName As String = ""
                Dim strLName As String = drwSponsorList.FullName
                Dim strFatherName As String = drwSponsorList.FatherName

                Dim blnIsMale As Boolean = True

                intSponsorID = qryFile.spr_File_Insert(drwSponsorList.SponsorCustomerNo, strFName, strLName, strFatherName, drwSponsorList.MobileNo, drwSponsorList.NationalID, drwSponsorList.IDNumber, "", drwSponsorList.Address, drwSponsorList.TelephoneHome, drwSponsorList.TelephoneWork, blnIsMale, 2, 1, Nothing, Nothing)


            Else

                Dim drwFilebyCustomerNo As BusinessObject.dstFile.spr_File_CustomerNo_SelectRow = dtblFilebyCustomerNo.Rows(0)
                intSponsorID = drwFilebyCustomerNo.ID

                If blnIsUpdateDay = True Then
                    'drwSponsorList
                    Try
                        Dim blnIsMale As Boolean = True
                        Dim qryFile As New BusinessObject.dstFileTableAdapters.QueriesTableAdapter
                        qryFile.spr_File_MAT_Update(intSponsorID, drwSponsorList.MobileNo, drwSponsorList.Address, drwSponsorList.TelephoneHome, drwSponsorList.TelephoneWork, "", drwSponsorList.FullName, drwSponsorList.FatherName, blnIsMale)

                    Catch ex As Exception

                        Dim qryErrorLog As New DataSet1TableAdapters.QueriesTableAdapter
                        qryErrorLog.spr_ErrorLog_Insert(ex.Message, 3, "GetSponsorsList")

                    End Try

                End If

            End If


            If obj_stc_Sponsor_Waranty Is Nothing Then
                ReDim obj_stc_Sponsor_Waranty(0)
            Else
                ReDim Preserve obj_stc_Sponsor_Waranty(obj_stc_Sponsor_Waranty.Length)
            End If

            obj_stc_Sponsor_Waranty(obj_stc_Sponsor_Waranty.Length - 1).SponsorID = intSponsorID
            obj_stc_Sponsor_Waranty(obj_stc_Sponsor_Waranty.Length - 1).WarantyTypeDesc = drwSponsorList.WarantyTypeDesc


        Next drwSponsorList

        Dim lnq = obj_stc_Sponsor_Waranty.GroupBy(Function(x) New With {.Key1 = x.SponsorID})

        Return lnq


    End Function


#End Region

    Public Sub Test()
        Dim tadpSystemSetting As New BusinessObject.dstSystemSettingTableAdapters.spr_SystemSetting_SelectTableAdapter
        Dim dtblSystemSetting As BusinessObject.dstSystemSetting.spr_SystemSetting_SelectDataTable = Nothing
        dtblSystemSetting = tadpSystemSetting.GetData()
        drwSystemSetting = dtblSystemSetting.Rows(0)
        '' Call UpdateBIData()

        ' '' '' ''   Call tmrSponsorList_Elapsed(Nothing, Nothing)

        'Call SendSMS()

        ' Call SendNotification()

        ''   Call tmrSelfReport_Elapsed(Nothing, Nothing)

        Call tmrVoiceSMS_Elapsed(Nothing, Nothing)



    End Sub

    Private Sub tmrSelfReport_Elapsed(ByVal sender As System.Object, ByVal e As System.Timers.ElapsedEventArgs) Handles tmrSelfReport.Elapsed

        Try



            If Date.Now.Hour < (drwSystemSetting.UpdateTime.Hours + 1) OrElse Date.Now.Hour > 15 OrElse Date.Now.DayOfWeek = DayOfWeek.Friday Then
                Return
            End If



            Dim tadpSelfReport As New BusinessObject.dstSelfReportTableAdapters.spr_SelfReport_SelectTableAdapter
            Dim dtblSelfReport As BusinessObject.dstSelfReport.spr_SelfReport_SelectDataTable = Nothing
            dtblSelfReport = tadpSelfReport.GetData()

            If dtblSelfReport.Rows.Count > 0 Then
                Dim drwSelfReport As BusinessObject.dstSelfReport.spr_SelfReport_SelectRow = dtblSelfReport.Rows(0)
                If drwSelfReport.theDay = Date.Now.Date Then
                    Return
                End If
            End If


            Dim qrySelfReport As New BusinessObject.dstSelfReportTableAdapters.QueriesTableAdapter

            Dim strResultMessage As String = ""



            Dim tadpLCLog As New BusinessObject.dstLogCurrentLCStatus_HTableAdapters.spr_LogCurrentLCStatus_H_ForDate_SelectTableAdapter
            Dim dtblLCLog As BusinessObject.dstLogCurrentLCStatus_H.spr_LogCurrentLCStatus_H_ForDate_SelectDataTable = Nothing

            dtblLCLog = tadpLCLog.GetData(Date.Now.AddDays(-1).Date)


            If dtblLCLog.Rows.Count = 0 Then

                strResultMessage = " برای مورخ " & mdlGeneral.GetPersianDate(Date.Now.AddDays(-1)) & " لاگ موجود نیست سرویس را ریست کنید" & ControlChars.NewLine & "code:0"

            Else
                Dim drwLCLog As BusinessObject.dstLogCurrentLCStatus_H.spr_LogCurrentLCStatus_H_ForDate_SelectRow = dtblLCLog.Rows(0)
                If drwLCLog.Success = False Then

                    strResultMessage = " برای مورخ " & mdlGeneral.GetPersianDate(Date.Now.AddDays(-1)) & "خواندن اطلاعات با خطا همراه شده است  مشکل در BI است " & drwLCLog.Remarks & ControlChars.NewLine & "code:1"
                Else

                    strResultMessage = "اطلاعات برای مورخ " & mdlGeneral.GetPersianDate(Date.Now.AddDays(-1)) & " با موفقیت در ساعت " & drwLCLog.STime.Hour & " از BI خوانده شده است"

                    Dim tadpSMSCount As New BusinessObject.dstWarningNotificationLogDetailTableAdapters.spr_WarningNotificationLogDetail_SMSCount_SelectTableAdapter
                    Dim dtblSMSCount As BusinessObject.dstWarningNotificationLogDetail.spr_WarningNotificationLogDetail_SMSCount_SelectDataTable = Nothing
                    dtblSMSCount = tadpSMSCount.GetData(Date.Now.Date)

                    If dtblSMSCount.Rows.Count = 0 Then
                        strResultMessage &= ControlChars.NewLine & "ولی هیچ پیامکی تاکنون ارسال نشده است مشکل در سرویس ارسال" & ControlChars.NewLine & "code:2"
                    Else
                        Dim drwSMSCount As BusinessObject.dstWarningNotificationLogDetail.spr_WarningNotificationLogDetail_SMSCount_SelectRow = dtblSMSCount.Rows(0)
                        If drwSMSCount.IsExpr1Null = True OrElse drwSMSCount.Expr1 = 0 Then
                            strResultMessage &= ControlChars.NewLine & "ولی هیچ پیامکی تاکنون ارسال نشده است مشکل در سرویس ارسال" & ControlChars.NewLine & "code:2"
                        Else

                            Dim tadpWarningNotificationLogDetailFirstLastLogSelect As New DataSet1TableAdapters.spr_WarningNotificationLogDetailFirstLastLog_SelectTableAdapter
                            Dim dtblWarningNotificationLogDetailFirstLastLog As DataSet1.spr_WarningNotificationLogDetailFirstLastLog_SelectDataTable = Nothing

                            dtblWarningNotificationLogDetailFirstLastLog = tadpWarningNotificationLogDetailFirstLastLogSelect.GetData()

                            Dim drwWarningNotificationLogDetailFirstLastLog As DataSet1.spr_WarningNotificationLogDetailFirstLastLog_SelectRow = dtblWarningNotificationLogDetailFirstLastLog.Rows(0)

                            Try


                                Dim tmSpan As TimeSpan = drwWarningNotificationLogDetailFirstLastLog.Last.Subtract(drwWarningNotificationLogDetailFirstLastLog.First)

                                strResultMessage = "مورخ: " & mdlGeneral.GetPersianDate(Date.Now) & ControlChars.NewLine
                                strResultMessage &= "وضعیت: OK" & ControlChars.NewLine
                                strResultMessage &= "ساعت خواندن اطلاعات از BI:" & drwLCLog.STime.ToString("HH:mm") & ControlChars.NewLine
                                strResultMessage &= " زمان اولین ارسال: " & drwWarningNotificationLogDetailFirstLastLog.First.ToString("HH:mm") & ControlChars.NewLine & " زمان آخرین ارسال تا این لحظه: " & drwWarningNotificationLogDetailFirstLastLog.Last.ToString("HH:mm") & ControlChars.NewLine
                                strResultMessage &= " کل مدت زمان ارسال تا این لحظه: " & Math.Floor(tmSpan.TotalHours) & "h" & Math.Floor(tmSpan.Minutes) & "m" & ControlChars.NewLine
                                strResultMessage &= " تعداد پیامک ارسال شده تا این لحظه: " & drwSMSCount.Expr1.ToString("n0")
                                qrySelfReport.spr_SelfReport_Insert(Date.Now.Date, Date.Now, strResultMessage)

                            Catch ex As Exception



                            End Try





                        End If
                    End If
                End If
            End If




            Dim objSMS As New clsSMS
            Dim arrMessage(5) As String
            Dim arrDestination(5) As String

            arrMessage(0) = strResultMessage
            'arrDestination(0) = "09122764983"
              arrDestination(0) = "09125781487"
            arrMessage(1) = strResultMessage
            arrDestination(1) = "09125470419"
            arrMessage(2) = strResultMessage
            arrDestination(2) = "09363738886"

            arrMessage(3) = strResultMessage
            arrDestination(3) = "09125010426"

            arrMessage(4) = strResultMessage
            arrDestination(4) = "09128165662"


            arrMessage(5) = strResultMessage
            arrDestination(5) = "09355066075"

            objSMS.SendSMS_LikeToLike(arrMessage, arrDestination, drwSystemSetting.GatewayUsername, drwSystemSetting.GatewayPassword, drwSystemSetting.GatewayNumber, drwSystemSetting.GatewayIP, drwSystemSetting.GatewayCompany, "Keiwan+" & Date.Now.ToLongTimeString)


        Catch ex As Exception
            Dim m As Integer = 3
        End Try

    End Sub

    Private Sub SendTestSMS(ByVal strMessage As String)

        Dim objSMS As New clsSMS
        Dim arrSMSMessages(0) As String
        arrSMSMessages(0) = strMessage
        Dim arrSMSDestination(0) As String
        ''arrSMSDestination(0) = "09122764983"
        arrSMSDestination(0) = "09125781487"

        objSMS.SendSMS_LikeToLike(arrSMSMessages, arrSMSDestination, drwSystemSetting.GatewayUsername, drwSystemSetting.GatewayPassword, drwSystemSetting.GatewayNumber, drwSystemSetting.GatewayIP, drwSystemSetting.GatewayCompany, "KTest" & Date.Now.ToLongTimeString)


    End Sub


    Public Sub SendAdministratioSMSMessage_ForSMS()

        Dim qryWarningNotificationLog As New BusinessObject.dstWarningNotificationLogDetailTableAdapters.QueriesTableAdapter


        If Date.Now.DayOfWeek = DayOfWeek.Friday Then
            Return
        End If

        Try

            Do

                Threading.Thread.Sleep(200000)

                Dim tadpPendingCount As New BusinessObject.dstWarningNotificationLogDetailTableAdapters.spr_WarningNotificationLogDetail_Pending_SMSCount_SelectTableAdapter

                tadpPendingCount.ClearBeforeFill = True
                Dim dtblPendingCount As BusinessObject.dstWarningNotificationLogDetail.spr_WarningNotificationLogDetail_Pending_SMSCount_SelectDataTable = Nothing

                dtblPendingCount = tadpPendingCount.GetData(Date.Now.Date)


                If dtblPendingCount.Rows.Count > 0 Then

                    Dim drwPendingCount As BusinessObject.dstWarningNotificationLogDetail.spr_WarningNotificationLogDetail_Pending_SMSCount_SelectRow = dtblPendingCount.Rows(0)
                    If drwPendingCount.IsExpr1Null = False Then
                        If drwPendingCount.Expr1 > 0 Then
                            Continue Do
                        End If
                    End If
                End If
                Exit Do
            Loop

        Catch ex As Exception

            Dim qryErrorLog As New DataSet1TableAdapters.QueriesTableAdapter
            qryErrorLog.spr_ErrorLog_Insert(ex.Message, 2, "SendAdministratioSMSMessage_ForSMS1")


        End Try




        Try


            Dim strResultMessage As String = ""



            Dim tadpLCLog As New BusinessObject.dstLogCurrentLCStatus_HTableAdapters.spr_LogCurrentLCStatus_H_ForDate_SelectTableAdapter
            Dim dtblLCLog As BusinessObject.dstLogCurrentLCStatus_H.spr_LogCurrentLCStatus_H_ForDate_SelectDataTable = Nothing

            dtblLCLog = tadpLCLog.GetData(Date.Now.AddDays(-1).Date)


            If dtblLCLog.Rows.Count = 0 Then
                Return
            Else
                Dim drwLCLog As BusinessObject.dstLogCurrentLCStatus_H.spr_LogCurrentLCStatus_H_ForDate_SelectRow = dtblLCLog.Rows(0)
                If drwLCLog.Success = False Then

                    Return
                Else

                    Dim tadpSMSCount As New BusinessObject.dstWarningNotificationLogDetailTableAdapters.spr_WarningNotificationLogDetail_SMSCount_SelectTableAdapter
                    Dim dtblSMSCount As BusinessObject.dstWarningNotificationLogDetail.spr_WarningNotificationLogDetail_SMSCount_SelectDataTable = Nothing
                    dtblSMSCount = tadpSMSCount.GetData(Date.Now.Date)

                    If dtblSMSCount.Rows.Count = 0 Then
                        Return

                    Else
                        Dim drwSMSCount As BusinessObject.dstWarningNotificationLogDetail.spr_WarningNotificationLogDetail_SMSCount_SelectRow = dtblSMSCount.Rows(0)
                        If drwSMSCount.IsExpr1Null = True OrElse drwSMSCount.Expr1 = 0 Then
                            Return

                        Else
                            Try
                                Dim tadpWarningNotificationLogDetailFirstLastLogSelect As New DataSet1TableAdapters.spr_WarningNotificationLogDetailFirstLastLog_SelectTableAdapter
                                Dim dtblWarningNotificationLogDetailFirstLastLog As DataSet1.spr_WarningNotificationLogDetailFirstLastLog_SelectDataTable = Nothing

                                dtblWarningNotificationLogDetailFirstLastLog = tadpWarningNotificationLogDetailFirstLastLogSelect.GetData()

                                Dim drwWarningNotificationLogDetailFirstLastLog As DataSet1.spr_WarningNotificationLogDetailFirstLastLog_SelectRow = dtblWarningNotificationLogDetailFirstLastLog.Rows(0)




                                Dim tmSpan As TimeSpan = drwWarningNotificationLogDetailFirstLastLog.Last.Subtract(drwWarningNotificationLogDetailFirstLastLog.First)

                                strResultMessage = "گزارش نهایی مورخ: " & mdlGeneral.GetPersianDate(Date.Now) & ControlChars.NewLine
                                strResultMessage &= "وضعیت: OK" & ControlChars.NewLine
                                strResultMessage &= "ساعت خواندن اطلاعات از BI:" & drwLCLog.STime.ToString("HH:mm") & ControlChars.NewLine
                                strResultMessage &= " زمان اولین ارسال: " & drwWarningNotificationLogDetailFirstLastLog.First.ToString("HH:mm") & ControlChars.NewLine & " زمان آخرین ارسال: " & drwWarningNotificationLogDetailFirstLastLog.Last.ToString("HH:mm") & ControlChars.NewLine
                                strResultMessage &= " کل مدت زمان ارسال : " & Math.Floor(tmSpan.TotalHours) & "h" & Math.Floor(tmSpan.Minutes) & "m" & ControlChars.NewLine
                                strResultMessage &= " تعداد پیامک ارسال شده: " & drwSMSCount.Expr1.ToString("n0")

                                Dim intTotalCount As Integer = 0
                                Try
                                    Dim oDbContext As New BusinessObject.dbMehrVosulEntities1
                                    intTotalCount = oDbContext.tbl_CurrentLCStatus.Count()
                                Catch ex As Exception

                                End Try


                                qryWarningNotificationLog.spr_SMSCountLog_Insert(Date.Now.Date, drwSMSCount.Expr1, drwLCLog.ID, drwWarningNotificationLogDetailFirstLastLog.First, drwWarningNotificationLogDetailFirstLastLog.Last, intTotalCount, 1)


                            Catch ex As Exception

                                Dim qryErrorLog As New DataSet1TableAdapters.QueriesTableAdapter
                                qryErrorLog.spr_ErrorLog_Insert(ex.Message, 2, "SendAdministratioSMSMessage_ForSMS2")

                                Return

                            End Try





                        End If
                    End If
                End If
            End If




            Dim objSMS As New clsSMS
            Dim arrMessage(5) As String
            Dim arrDestination(5) As String

            arrMessage(0) = strResultMessage
            ''  arrDestination(0) = "09122764983"
            arrDestination(0) = "09125781487"
            arrMessage(1) = strResultMessage
            arrDestination(1) = "09125470419"
            arrMessage(2) = strResultMessage
            arrDestination(2) = "09363738886"

            arrMessage(3) = strResultMessage
            arrDestination(3) = "09123201844"

            arrMessage(4) = strResultMessage
            arrDestination(4) = "09128165662"


            arrMessage(5) = strResultMessage
            arrDestination(5) = "09355066075"

            objSMS.SendSMS_LikeToLike(arrMessage, arrDestination, drwSystemSetting.GatewayUsername, drwSystemSetting.GatewayPassword, drwSystemSetting.GatewayNumber, drwSystemSetting.GatewayIP, drwSystemSetting.GatewayCompany, "Keiwan+" & Date.Now.ToLongTimeString)


        Catch ex As Exception
            Dim qryErrorLog As New DataSet1TableAdapters.QueriesTableAdapter
            qryErrorLog.spr_ErrorLog_Insert(ex.Message, 2, "SendAdministratioSMSMessage_ForSMS3")
            Dim m As Integer = 3
        End Try


    End Sub


    Public Sub SendAdministratioSMSMessage_ForVoice()

        Dim qryWarningNotificationLog As New BusinessObject.dstWarningNotificationLogDetailTableAdapters.QueriesTableAdapter


        If Date.Now.DayOfWeek = DayOfWeek.Friday Then
            Return
        End If

        Try


            Dim strResultMessage As String = ""


            Dim tadpLCLog As New BusinessObject.dstLogCurrentLCStatus_HTableAdapters.spr_LogCurrentLCStatus_H_ForDate_SelectTableAdapter
            Dim dtblLCLog As BusinessObject.dstLogCurrentLCStatus_H.spr_LogCurrentLCStatus_H_ForDate_SelectDataTable = Nothing

            dtblLCLog = tadpLCLog.GetData(Date.Now.AddDays(-1).Date)


            If dtblLCLog.Rows.Count = 0 Then
                Return
            Else
                Dim drwLCLog As BusinessObject.dstLogCurrentLCStatus_H.spr_LogCurrentLCStatus_H_ForDate_SelectRow = dtblLCLog.Rows(0)
                If drwLCLog.Success = False Then
                    Return
                Else

                    Dim oDbContext As New BusinessObject.dbMehrVosulEntities1
                    Dim dteToday As Date = Date.Now.Date

                    Dim lnqWarningNotificationLogDetail = oDbContext.tbl_WarningNotificationLogDetail.Where(Function(x) x.NotificationTypeID = 7 AndAlso DbFunctions.TruncateTime(x.STime) = dteToday)
                    Dim intVoiceSMSCount = lnqWarningNotificationLogDetail.Count
                    If intVoiceSMSCount = 0 Then
                        Return
                    End If

                    Dim dteFirstSend = lnqWarningNotificationLogDetail.Min(Function(x) x.SendDate)
                    Dim dteLastSend = lnqWarningNotificationLogDetail.Max(Function(x) x.SendDate)


                    Dim tmSpan As TimeSpan = dteLastSend.Value.Subtract(dteFirstSend.Value)

                    strResultMessage = "گزارش نهایی مورخ: " & mdlGeneral.GetPersianDate(Date.Now) & ControlChars.NewLine
                    strResultMessage &= "وضعیت: OK" & ControlChars.NewLine
                    strResultMessage &= "پیامک صوتی" & ControlChars.NewLine
                    strResultMessage &= " زمان اولین ارسال: " & dteFirstSend.Value.ToString("HH:mm") & ControlChars.NewLine & " زمان آخرین ارسال: " & dteLastSend.Value.ToString("HH:mm") & ControlChars.NewLine
                    strResultMessage &= " کل مدت زمان ارسال : " & Math.Floor(tmSpan.TotalHours) & "h" & Math.Floor(tmSpan.Minutes) & "m" & ControlChars.NewLine
                    strResultMessage &= " تعداد پیامک صوتی ارسال شده: " & intVoiceSMSCount.ToString("n0")



                End If
            End If




            Dim objSMS As New clsSMS
            Dim arrMessage(0) As String
            Dim arrDestination(0) As String

            arrMessage(0) = strResultMessage
            arrDestination(0) = "09123201844"

            ''Dim arrMessage(5) As String
            ''Dim arrDestination(5) As String

            ''arrMessage(0) = strResultMessage
            ''arrDestination(0) = "09125781487"
            ''arrMessage(1) = strResultMessage
            ''arrDestination(1) = "09125470419"
            ''arrMessage(2) = strResultMessage
            ''arrDestination(2) = "09363738886"

            ''arrMessage(3) = strResultMessage
            ''arrDestination(3) = "09123201844"

            ''arrMessage(4) = strResultMessage
            ''arrDestination(4) = "09128165662"


            ''arrMessage(5) = strResultMessage
            ''arrDestination(5) = "09355066075"

            objSMS.SendSMS_LikeToLike(arrMessage, arrDestination, drwSystemSetting.GatewayUsername, drwSystemSetting.GatewayPassword, drwSystemSetting.GatewayNumber, drwSystemSetting.GatewayIP, drwSystemSetting.GatewayCompany, "Keiwan+" & Date.Now.ToLongTimeString)


        Catch ex As Exception
            Dim qryErrorLog As New DataSet1TableAdapters.QueriesTableAdapter
            qryErrorLog.spr_ErrorLog_Insert(ex.Message, 2, "SendAdministratioSMSMessage_ForVoice")
            Dim m As Integer = 3
        End Try


    End Sub


    Public Sub GetVoiceSMSArrays_Vesal(ByVal warningIntervalId As Integer, toSponsor As Boolean, ByRef records() As String, ByRef numbers() As String)

        Dim cntxMehrVosul As New BusinessObject.dbMehrVosulEntities1

        Dim lnqDraftText = cntxMehrVosul.tbl_DraftText.Where(Function(x) x.FK_WarningIntervalsID = warningIntervalId AndAlso x.ToSponsor = toSponsor AndAlso x.DraftType = 5)

        'records = lnqDraftText.Where(Function(x) x.IsDynamic = False).OrderBy(Function(x) x.OrderInLevel).Select(Function(x) New With {.RecordID = x.FK_VoiceRecordID}).ToArray().Select(Function(x) x.RecordID.ToString())

        Dim lnqrecords = lnqDraftText.Where(Function(x) x.IsDynamic = False).OrderBy(Function(x) x.OrderInLevel)

        For Each itm In lnqrecords
            If records Is Nothing Then
                ReDim records(0)
            Else
                ReDim Preserve records(records.Length)
            End If
            ''    records(records.Length - 1) = itm.FK_VoiceRecordID.Value.ToString()
            records(records.Length - 1) = itm.tbl_VoiceRecords.RecordID.Value.ToString()

        Next



        numbers = lnqDraftText.Where(Function(x) x.IsDynamic = True).OrderBy(Function(x) x.OrderInLevel).Select(Function(x) x.DraftText).ToArray()


    End Sub


    Private Sub GetVoiceSMSArrays_Hadi(ByVal warningIntervalId As Integer, ByRef records() As String, ByRef numbers() As String)

        Dim cntxMehrVosul As New BusinessObject.dbMehrVosulEntities1

        Dim lnqHadiDraftText = cntxMehrVosul.tbl_HadiDraftText.Where(Function(x) x.FK_HadiWarningIntervalsID = warningIntervalId)

        records = lnqHadiDraftText.Where(Function(x) x.IsDynamic = False).OrderBy(Function(x) x.OrderInLevel).Select(Function(x) New With {.RecordID = x.FK_VoiceRecordID}).ToArray().Select(Function(x) x.RecordID.ToString)

        numbers = lnqHadiDraftText.Where(Function(x) x.IsDynamic = True).OrderBy(Function(x) x.OrderInLevel).Select(Function(x) x.DraftText).ToArray()

    End Sub

    Public Function SendVoiceMixedSMS(ByVal uId As Integer, ByVal token As String, ByVal name As String, ByVal tos() As Object, ByVal records() As Object, ByVal numbers() As String, ByVal sayMathod As String) As Boolean

        Try
            Dim oVoiceSMS As New VoiceSMS.RahyabVoiceSend  'ZamanakWebService.Default_Service_SoapServer_ZamanakV4Service
            Dim strMessage As String = ""
            Threading.Thread.Sleep(125)
            oVoiceSMS.SendMixedVoiceSMS_SynchAsync("vesal", "matchautoreplay123", uId, token, name, tos, records, numbers, sayMathod, strMessage)
            Return True
        Catch ex As Exception
            Return False
        End Try

    End Function

    Private Sub tmrVoiceSMS_Elapsed(sender As Object, e As Timers.ElapsedEventArgs) Handles tmrVoiceSMS.Elapsed


        If Date.Now.Hour <= 9 OrElse Date.Now.DayOfWeek = DayOfWeek.Friday Then
            Return
        End If

        Try
            Dim cntxVar As New BusinessObject.dbMehrVosulEntities1
            Dim dteToday As Date = Date.Now.Date
            Dim intLogCount = cntxVar.tbl_VoiceSMS_Report_Log.Where(Function(x) x.Date = dteToday).Count()
            If intLogCount > 0 Then
                Return
            End If
            Dim oLogReport As New BusinessObject.tbl_VoiceSMS_Report_Log
            oLogReport.Date = dteToday
            oLogReport.STime = Date.Now
            cntxVar.tbl_VoiceSMS_Report_Log.Add(oLogReport)
            cntxVar.SaveChanges()

            If dteToday.DayOfWeek = DayOfWeek.Friday Then
                Return
            End If

            Dim arrDestination(1) As String
            arrDestination(0) = "09122075899"
            arrDestination(1) = "09123201844"

            ''Dim arrDestination(5) As String
            ''arrDestination(0) = "09125781487"
            ''arrDestination(1) = "09125470419"
            ''arrDestination(2) = "09363738886"
            ''arrDestination(3) = "09123201844"
            ''arrDestination(4) = "09128165662"
            ''arrDestination(5) = "09355066075"

            Dim oVoiceSMS As New VoiceSMS.RahyabVoiceSend
            Dim strMessage As String = ""
            Dim arrRecords(1) As String
            arrRecords(0) = "49412"
            arrRecords(1) = "49413"
            Dim arrNumbers(1) As String
            arrNumbers(0) = "21"
            arrNumbers(1) = "35"

            ' oVoiceSMS.SendVoiceSMS_Mehr("vesal", "matchautoreplay123", drwSystemSetting.VoiceSMSUID, drwSystemSetting.VoiceSMSToken, "testVoice_" & Date.Now.Millisecond.ToString, arrDestination, "49478", 1, strMessage)

            SendVoiceMixedSMS(drwSystemSetting.VoiceSMSUID, drwSystemSetting.VoiceSMSToken, "testVoice_" & Date.Now.Millisecond.ToString, arrDestination, arrRecords, arrNumbers, 9)

        Catch ex As Exception

            Dim qryErrorLog As New DataSet1TableAdapters.QueriesTableAdapter
            qryErrorLog.spr_ErrorLog_Insert(ex.Message, 4, "tmrVoiceSMS_Elapsed")


        End Try


    End Sub

    Private Sub tmrUpdateData_Hadi_Deposit_Elapsed(sender As Object, e As Timers.ElapsedEventArgs) Handles tmrUpdateData_Hadi_Deposit.Elapsed
        Try
            '  Call Hadi_BI_Deposit()

        Catch ex As Exception

        End Try

    End Sub

    Private Sub tmrUpdateData_Hadi_Loan_Elapsed(sender As Object, e As Timers.ElapsedEventArgs) Handles tmrUpdateData_Hadi_Loan.Elapsed
        'Call Hadi_BI_Laon()
    End Sub
End Class
