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

Partial Public Class tbl_AccessgroupMenu
    Public Property ID As Integer
    Public Property FK_AccessGroupID As Nullable(Of Integer)
    Public Property FK_MenuID As Nullable(Of Integer)

    Public Overridable Property tbl_Accessgroup As tbl_Accessgroup
    Public Overridable Property tbl_Menu As tbl_Menu

End Class
