﻿'------------------------------------------------------------------------------
' <auto-generated>
'     This code was generated from a template.
'
'     Manual changes to this file may cause unexpected behavior in your application.
'     Manual changes to this file will be overwritten if the code is regenerated.
' </auto-generated>
'------------------------------------------------------------------------------

Imports System
Imports System.Data.Entity
Imports System.Data.Entity.Infrastructure

Partial Public Class dbMehrVosulEntities1
    Inherits DbContext

    Public Sub New()
        MyBase.New("name=dbMehrVosulEntities1")
    End Sub

    Protected Overrides Sub OnModelCreating(modelBuilder As DbModelBuilder)
        Throw New UnintentionalCodeFirstException()
    End Sub

    Public Overridable Property tbl_Accessgroup() As DbSet(Of tbl_Accessgroup)
    Public Overridable Property tbl_AccessgroupMenu() As DbSet(Of tbl_AccessgroupMenu)
    Public Overridable Property tbl_AccessgroupUser() As DbSet(Of tbl_AccessgroupUser)
    Public Overridable Property tbl_Menu() As DbSet(Of tbl_Menu)
    Public Overridable Property tbl_SelfReport() As DbSet(Of tbl_SelfReport)
    Public Overridable Property tbl_User() As DbSet(Of tbl_User)
    Public Overridable Property tbl_SystemSetting() As DbSet(Of tbl_SystemSetting)
    Public Overridable Property sysdiagrams() As DbSet(Of sysdiagram)
    Public Overridable Property tbl_ErrorLog() As DbSet(Of tbl_ErrorLog)
    Public Overridable Property tbl_HadiDraftText() As DbSet(Of tbl_HadiDraftText)
    Public Overridable Property tbl_HadiLogCurrentLCStatus_H() As DbSet(Of tbl_HadiLogCurrentLCStatus_H)
    Public Overridable Property tbl_HadiLogLoanStatus() As DbSet(Of tbl_HadiLogLoanStatus)
    Public Overridable Property tbl_HadiOperation_Deposit() As DbSet(Of tbl_HadiOperation_Deposit)
    Public Overridable Property tbl_HadiOperation_Loan() As DbSet(Of tbl_HadiOperation_Loan)
    Public Overridable Property tbl_HadiWarningIntervals() As DbSet(Of tbl_HadiWarningIntervals)
    Public Overridable Property tbl_HadiWarningIntervalsBranch() As DbSet(Of tbl_HadiWarningIntervalsBranch)
    Public Overridable Property tbl_HadiWarningIntervalsDeposit() As DbSet(Of tbl_HadiWarningIntervalsDeposit)
    Public Overridable Property tbl_HadiWarningNotificationLog() As DbSet(Of tbl_HadiWarningNotificationLog)
    Public Overridable Property tbl_HadiWarningNotificationLogDetail() As DbSet(Of tbl_HadiWarningNotificationLogDetail)
    Public Overridable Property tbl_Date() As DbSet(Of tbl_Date)
    Public Overridable Property tbl_WarnatyType() As DbSet(Of tbl_WarnatyType)
    Public Overridable Property tbl_AlcatelLog() As DbSet(Of tbl_AlcatelLog)
    Public Overridable Property tbl_Branch() As DbSet(Of tbl_Branch)
    Public Overridable Property tbl_Commitment_Active() As DbSet(Of tbl_Commitment_Active)
    Public Overridable Property tbl_Commitment_Archive() As DbSet(Of tbl_Commitment_Archive)
    Public Overridable Property tbl_CurrentLCStatus() As DbSet(Of tbl_CurrentLCStatus)
    Public Overridable Property tbl_DraftText() As DbSet(Of tbl_DraftText)
    Public Overridable Property tbl_File() As DbSet(Of tbl_File)
    Public Overridable Property tbl_HandyFollow() As DbSet(Of tbl_HandyFollow)
    Public Overridable Property tbl_Loan() As DbSet(Of tbl_Loan)
    Public Overridable Property tbl_LoanSponsor() As DbSet(Of tbl_LoanSponsor)
    Public Overridable Property tbl_LoanSponsor_WarantyType() As DbSet(Of tbl_LoanSponsor_WarantyType)
    Public Overridable Property tbl_LoanType() As DbSet(Of tbl_LoanType)
    Public Overridable Property tbl_LogCurrentLCStatus_H() As DbSet(Of tbl_LogCurrentLCStatus_H)
    Public Overridable Property tbl_NotificationTarif() As DbSet(Of tbl_NotificationTarif)
    Public Overridable Property tbl_Province() As DbSet(Of tbl_Province)
    Public Overridable Property tbl_SMSCountLog() As DbSet(Of tbl_SMSCountLog)
    Public Overridable Property tbl_Sponsor_List_Log() As DbSet(Of tbl_Sponsor_List_Log)
    Public Overridable Property tbl_Sponsors_List() As DbSet(Of tbl_Sponsors_List)
    Public Overridable Property tbl_SystemLoginLog() As DbSet(Of tbl_SystemLoginLog)
    Public Overridable Property tbl_VoiceRecords() As DbSet(Of tbl_VoiceRecords)
    Public Overridable Property tbl_WarningIntervals() As DbSet(Of tbl_WarningIntervals)
    Public Overridable Property tbl_WarningIntervalsBranch() As DbSet(Of tbl_WarningIntervalsBranch)
    Public Overridable Property tbl_WarningIntervalsException() As DbSet(Of tbl_WarningIntervalsException)
    Public Overridable Property tbl_WarningIntervalsExceptionBranch() As DbSet(Of tbl_WarningIntervalsExceptionBranch)
    Public Overridable Property tbl_WarningIntervalsExceptionLoanType() As DbSet(Of tbl_WarningIntervalsExceptionLoanType)
    Public Overridable Property tbl_WarningIntervalsLoanType() As DbSet(Of tbl_WarningIntervalsLoanType)
    Public Overridable Property tbl_WarningNotificationLog() As DbSet(Of tbl_WarningNotificationLog)
    Public Overridable Property tbl_WarningNotificationLogDetail() As DbSet(Of tbl_WarningNotificationLogDetail)
    Public Overridable Property tbl_Deposits() As DbSet(Of tbl_Deposits)
    Public Overridable Property tbl_DepositType() As DbSet(Of tbl_DepositType)
    Public Overridable Property tbl_HadiWarningIntervalsException() As DbSet(Of tbl_HadiWarningIntervalsException)
    Public Overridable Property tbl_HadiWarningIntervalsExceptionBranch() As DbSet(Of tbl_HadiWarningIntervalsExceptionBranch)
    Public Overridable Property tbl_HadiWarningIntervalsExceptionDeposit() As DbSet(Of tbl_HadiWarningIntervalsExceptionDeposit)
    Public Overridable Property tbl_VoiceSMS_Report_Log() As DbSet(Of tbl_VoiceSMS_Report_Log)
    Public Overridable Property tbl_HadiWarningIntervalsLoan() As DbSet(Of tbl_HadiWarningIntervalsLoan)

End Class
