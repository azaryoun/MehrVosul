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

Partial Public Class tbl_PreDraftText
    Public Property ID As Integer
    Public Property FK_PreWarningIntervalsID As Nullable(Of Integer)
    Public Property OrderInLevel As Nullable(Of Integer)
    Public Property DraftText As String
    Public Property IsDynamic As Nullable(Of Boolean)
    Public Property DraftType As Nullable(Of Byte)
    Public Property FK_VoiceRecordID As Nullable(Of Integer)

    Public Overridable Property tbl_PreWarningIntervals As tbl_PreWarningIntervals

End Class
