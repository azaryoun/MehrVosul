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

Partial Public Class tbl_SMSCountLog
    Public Property ID As Integer
    Public Property SendDate As Nullable(Of Date)
    Public Property SMSCount As Nullable(Of Integer)
    Public Property STime As Nullable(Of Date)
    Public Property FK_LogCurrentLCStatusID As Nullable(Of Integer)
    Public Property FirstSent As Nullable(Of Date)
    Public Property LastSent As Nullable(Of Date)
    Public Property BITotal As Nullable(Of Integer)
    Public Property SMSVoice As Nullable(Of Integer)
    Public Property PreNotifySMS As Nullable(Of Integer)

End Class
