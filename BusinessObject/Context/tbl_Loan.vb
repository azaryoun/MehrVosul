'------------------------------------------------------------------------------
' <auto-generated>
'     This code was generated from a template.
'
'     Manual changes to this file may cause unexpected behavior in your application.
'     Manual changes to this file will be overwritten if the code is regenerated.
' </auto-generated>
'------------------------------------------------------------------------------

Imports System
Imports System.Collections.Generic

Partial Public Class tbl_Loan
    Public Property ID As Integer
    Public Property FK_FileID As Nullable(Of Integer)
    Public Property FK_LoanTypeID As Nullable(Of Integer)
    Public Property FK_BranchID As Nullable(Of Integer)
    Public Property LoanAmount As Nullable(Of Decimal)
    Public Property LoanDate As Nullable(Of Date)
    Public Property LoanNumber As String
    Public Property LoanSerial As Nullable(Of Integer)
    Public Property STime As Nullable(Of Date)
    Public Property TotalInstallment As Nullable(Of Integer)

    Public Overridable Property tbl_HadiOperation_Loan As ICollection(Of tbl_HadiOperation_Loan) = New HashSet(Of tbl_HadiOperation_Loan)
    Public Overridable Property tbl_Branch As tbl_Branch
    Public Overridable Property tbl_File As tbl_File
    Public Overridable Property tbl_HandyFollow As ICollection(Of tbl_HandyFollow) = New HashSet(Of tbl_HandyFollow)
    Public Overridable Property tbl_LoanType As tbl_LoanType
    Public Overridable Property tbl_LoanSponsor As ICollection(Of tbl_LoanSponsor) = New HashSet(Of tbl_LoanSponsor)
    Public Overridable Property tbl_PreNotifiyCurrentLCStatus As ICollection(Of tbl_PreNotifiyCurrentLCStatus) = New HashSet(Of tbl_PreNotifiyCurrentLCStatus)
    Public Overridable Property tbl_WarningNotificationLog As ICollection(Of tbl_WarningNotificationLog) = New HashSet(Of tbl_WarningNotificationLog)

End Class
