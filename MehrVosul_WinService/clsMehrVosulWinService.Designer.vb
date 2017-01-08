Imports System.ServiceProcess

<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class clsMehrVosulWinService
    Inherits System.ServiceProcess.ServiceBase

    'UserService overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()>
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    ' The main entry point for the process
    <MTAThread()>
    <System.Diagnostics.DebuggerNonUserCode()>
    Shared Sub Main()
        Dim ServicesToRun() As System.ServiceProcess.ServiceBase

        ' More than one NT Service may run within the same process. To add
        ' another service to this process, change the following line to
        ' create a second service object. For example,
        '
        '   ServicesToRun = New System.ServiceProcess.ServiceBase () {New Service1, New MySecondUserService}
        '
        ServicesToRun = New System.ServiceProcess.ServiceBase() {New clsMehrVosulWinService}

        System.ServiceProcess.ServiceBase.Run(ServicesToRun)
    End Sub

    'Required by the Component Designer
    Private components As System.ComponentModel.IContainer

    ' NOTE: The following procedure is required by the Component Designer
    ' It can be modified using the Component Designer.  
    ' Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        Me.tmrUpdateData = New System.Timers.Timer()
        Me.tmrSponsorList = New System.Timers.Timer()
        Me.tmrSelfReport = New System.Timers.Timer()
        Me.tmrVoiceSMS = New System.Timers.Timer()
        Me.tmrUpdateData_Hadi_Deposit = New System.Timers.Timer()
        Me.tmrUpdateData_Hadi_Loan = New System.Timers.Timer()
        Me.tmrFinalReport = New System.Timers.Timer()
        CType(Me.tmrUpdateData, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.tmrSponsorList, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.tmrSelfReport, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.tmrVoiceSMS, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.tmrUpdateData_Hadi_Deposit, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.tmrUpdateData_Hadi_Loan, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.tmrFinalReport, System.ComponentModel.ISupportInitialize).BeginInit()
        '
        'tmrUpdateData
        '
        Me.tmrUpdateData.Enabled = True
        Me.tmrUpdateData.Interval = 1800000.0R
        '
        'tmrSponsorList
        '
        Me.tmrSponsorList.Enabled = True
        Me.tmrSponsorList.Interval = 7200000.0R
        '
        'tmrSelfReport
        '
        Me.tmrSelfReport.Enabled = True
        Me.tmrSelfReport.Interval = 3600000.0R
        '
        'tmrVoiceSMS
        '
        Me.tmrVoiceSMS.Enabled = True
        Me.tmrVoiceSMS.Interval = 3600000.0R
        '
        'tmrUpdateData_Hadi_Deposit
        '
        Me.tmrUpdateData_Hadi_Deposit.Interval = 3600000.0R
        '
        'tmrUpdateData_Hadi_Loan
        '
        Me.tmrUpdateData_Hadi_Loan.Interval = 3600000.0R
        '
        'tmrFinalReport
        '
        Me.tmrFinalReport.Enabled = True
        Me.tmrFinalReport.Interval = 3600000.0R
        '
        'clsMehrVosulWinService
        '
        Me.ServiceName = "Service1"
        CType(Me.tmrUpdateData, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.tmrSponsorList, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.tmrSelfReport, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.tmrVoiceSMS, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.tmrUpdateData_Hadi_Deposit, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.tmrUpdateData_Hadi_Loan, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.tmrFinalReport, System.ComponentModel.ISupportInitialize).EndInit()

    End Sub
    Friend WithEvents tmrUpdateData As System.Timers.Timer
    Friend WithEvents tmrSponsorList As System.Timers.Timer
    Friend WithEvents tmrSelfReport As System.Timers.Timer
    Friend WithEvents tmrVoiceSMS As Timers.Timer
    Friend WithEvents tmrUpdateData_Hadi_Deposit As Timers.Timer
    Friend WithEvents tmrUpdateData_Hadi_Loan As Timers.Timer
    Friend WithEvents tmrFinalReport As Timers.Timer
End Class
