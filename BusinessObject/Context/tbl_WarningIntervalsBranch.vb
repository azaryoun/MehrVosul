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

Partial Public Class tbl_WarningIntervalsBranch
    Public Property ID As Integer
    Public Property FK_WarningIntervalID As Nullable(Of Integer)
    Public Property FK_BranchID As Nullable(Of Integer)

    Public Overridable Property tbl_Branch As tbl_Branch
    Public Overridable Property tbl_WarningIntervalsExceptionBranch As ICollection(Of tbl_WarningIntervalsExceptionBranch) = New HashSet(Of tbl_WarningIntervalsExceptionBranch)

End Class
