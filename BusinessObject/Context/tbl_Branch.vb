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

Partial Public Class tbl_Branch
    Public Property ID As Integer
    Public Property BrnachCode As String
    Public Property BranchName As String
    Public Property BranchAddress As String
    Public Property FK_CUserID As Nullable(Of Integer)
    Public Property STime As Nullable(Of Date)
    Public Property BranchIP As String
    Public Property Telephone As String
    Public Property Fk_ProvinceID As Nullable(Of Integer)

    Public Overridable Property tbl_User As ICollection(Of tbl_User) = New HashSet(Of tbl_User)
    Public Overridable Property tbl_Deposits As ICollection(Of tbl_Deposits) = New HashSet(Of tbl_Deposits)
    Public Overridable Property tbl_HadiOperation_Loan As ICollection(Of tbl_HadiOperation_Loan) = New HashSet(Of tbl_HadiOperation_Loan)
    Public Overridable Property tbl_HadiWarningIntervalsBranch As ICollection(Of tbl_HadiWarningIntervalsBranch) = New HashSet(Of tbl_HadiWarningIntervalsBranch)
    Public Overridable Property tbl_Province As tbl_Province
    Public Overridable Property tbl_Loan As ICollection(Of tbl_Loan) = New HashSet(Of tbl_Loan)
    Public Overridable Property tbl_PreWarningIntervalsBranch As ICollection(Of tbl_PreWarningIntervalsBranch) = New HashSet(Of tbl_PreWarningIntervalsBranch)
    Public Overridable Property tbl_WarningIntervalsBranch As ICollection(Of tbl_WarningIntervalsBranch) = New HashSet(Of tbl_WarningIntervalsBranch)

End Class
