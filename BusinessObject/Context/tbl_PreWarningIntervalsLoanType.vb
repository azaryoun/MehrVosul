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

Partial Public Class tbl_PreWarningIntervalsLoanType
    Public Property ID As Integer
    Public Property FK_PreWarningIntervalID As Nullable(Of Integer)
    Public Property FK_LoanTypeID As Nullable(Of Integer)

    Public Overridable Property tbl_LoanType As tbl_LoanType
    Public Overridable Property tbl_PreWarningIntervals As tbl_PreWarningIntervals

End Class