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

Partial Public Class spr_HadiWarningIntervals_Check_Select_Result
    Public Property ID As Integer
    Public Property FromDay As Nullable(Of Integer)
    Public Property ToDay As Nullable(Of Integer)
    Public Property FrequencyInDay As Nullable(Of Integer)
    Public Property StartTime As Nullable(Of System.TimeSpan)
    Public Property FrequencyPeriodHour As Nullable(Of Integer)
    Public Property SendSMS As Nullable(Of Boolean)
    Public Property CallTelephone As Nullable(Of Boolean)
    Public Property WarniningTitle As String
    Public Property VoiceMessage As Nullable(Of Boolean)
    Public Property FK_BranchID As Nullable(Of Integer)

End Class
