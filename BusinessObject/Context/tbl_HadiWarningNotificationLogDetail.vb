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

Partial Public Class tbl_HadiWarningNotificationLogDetail
    Public Property ID As Integer
    Public Property FK_HadiWarningNotificationLogID As Nullable(Of Integer)
    Public Property SenderInfo As String
    Public Property ReceiverInfo As String
    Public Property strMessage As String
    Public Property Remarks As String
    Public Property STime As Nullable(Of Date)
    Public Property BatchID As String
    Public Property SendStatus As Nullable(Of Byte)
    Public Property NotificationTypeID As Nullable(Of Byte)
    Public Property DeliveryDate As Nullable(Of Date)
    Public Property SendDate As Nullable(Of Date)

    Public Overridable Property tbl_HadiWarningNotificationLog As tbl_HadiWarningNotificationLog

End Class
