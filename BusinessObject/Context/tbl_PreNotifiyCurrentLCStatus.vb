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

Partial Public Class tbl_PreNotifiyCurrentLCStatus
    Public Property ID As Integer
    Public Property FK_FileID As Nullable(Of Integer)
    Public Property InstallmentDate As Nullable(Of Date)
    Public Property FK_BranchID As Nullable(Of Integer)
    Public Property FK_LoanID As Nullable(Of Integer)
    Public Property InstallmentAmount As Nullable(Of Decimal)
    Public Property Process As Nullable(Of Boolean)

    Public Overridable Property tbl_File As tbl_File
    Public Overridable Property tbl_Loan As tbl_Loan
    Public Overridable Property tbl_PreNotifiyCurrentLCStatus1 As tbl_PreNotifiyCurrentLCStatus
    Public Overridable Property tbl_PreNotifiyCurrentLCStatus2 As tbl_PreNotifiyCurrentLCStatus

End Class