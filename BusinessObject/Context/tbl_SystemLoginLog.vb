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

Partial Public Class tbl_SystemLoginLog
    Public Property ID As Integer
    Public Property FK_UserID As Nullable(Of Integer)
    Public Property LoginDateTime As Nullable(Of Date)

    Public Overridable Property tbl_User As tbl_User

End Class
