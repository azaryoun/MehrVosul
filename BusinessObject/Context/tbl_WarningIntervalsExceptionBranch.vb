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

Partial Public Class tbl_WarningIntervalsExceptionBranch
    Public Property ID As Integer
    Public Property FK_WarningIntervalsExceptionID As Nullable(Of Integer)
    Public Property FK_WarningIntervalBranchID As Nullable(Of Integer)

    Public Overridable Property tbl_WarningIntervalsBranch As tbl_WarningIntervalsBranch
    Public Overridable Property tbl_WarningIntervalsException As tbl_WarningIntervalsException

End Class
