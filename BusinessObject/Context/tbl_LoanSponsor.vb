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

Partial Public Class tbl_LoanSponsor
    Public Property ID As Integer
    Public Property FK_LoanID As Nullable(Of Integer)
    Public Property FK_SponsorID As Nullable(Of Integer)
    Public Property WarantyTypeDesc As String

    Public Overridable Property tbl_File As tbl_File
    Public Overridable Property tbl_Loan As tbl_Loan
    Public Overridable Property tbl_LoanSponsor_WarantyType As ICollection(Of tbl_LoanSponsor_WarantyType) = New HashSet(Of tbl_LoanSponsor_WarantyType)

End Class
