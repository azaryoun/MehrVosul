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

Partial Public Class tbl_Accessgroup
    Public Property ID As Integer
    Public Property Desp As String
    Public Property STime As Nullable(Of Date)
    Public Property FK_UserID As Nullable(Of Integer)

    Public Overridable Property tbl_User As tbl_User
    Public Overridable Property tbl_AccessgroupUser As ICollection(Of tbl_AccessgroupUser) = New HashSet(Of tbl_AccessgroupUser)
    Public Overridable Property tbl_AccessgroupMenu As ICollection(Of tbl_AccessgroupMenu) = New HashSet(Of tbl_AccessgroupMenu)

End Class
