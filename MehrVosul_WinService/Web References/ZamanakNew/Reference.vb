﻿'------------------------------------------------------------------------------
' <auto-generated>
'     This code was generated by a tool.
'     Runtime Version:4.0.30319.42000
'
'     Changes to this file may cause incorrect behavior and will be lost if
'     the code is regenerated.
' </auto-generated>
'------------------------------------------------------------------------------

Option Strict Off
Option Explicit On

Imports System
Imports System.ComponentModel
Imports System.Diagnostics
Imports System.Web.Services
Imports System.Web.Services.Protocols
Imports System.Xml.Serialization

'
'This source code was auto-generated by Microsoft.VSDesigner, Version 4.0.30319.42000.
'
Namespace ZamanakNew
    
    '''<remarks/>
    <System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.7.2046.0"),  _
     System.Diagnostics.DebuggerStepThroughAttribute(),  _
     System.ComponentModel.DesignerCategoryAttribute("code"),  _
     System.Web.Services.WebServiceBindingAttribute(Name:="MehrVoiceSoap", [Namespace]:="http://tempuri.org/"),  _
     System.Xml.Serialization.XmlIncludeAttribute(GetType(Object()))>  _
    Partial Public Class MehrVoice
        Inherits System.Web.Services.Protocols.SoapHttpClientProtocol
        
        Private SendVoiceSMS_MehrNewOperationCompleted As System.Threading.SendOrPostCallback
        
        Private SendMixedVoiceSMS_SynchNewOperationCompleted As System.Threading.SendOrPostCallback
        
        Private StatusVoiceSMS_Details_MehrOperationCompleted As System.Threading.SendOrPostCallback
        
        Private useDefaultCredentialsSetExplicitly As Boolean
        
        '''<remarks/>
        Public Sub New()
            MyBase.New
            Me.Url = Global.MehrVosul_WinService.My.MySettings.Default.MehrVosul_WinService_ZamanakNew_MehrVoice
            If (Me.IsLocalFileSystemWebService(Me.Url) = true) Then
                Me.UseDefaultCredentials = true
                Me.useDefaultCredentialsSetExplicitly = false
            Else
                Me.useDefaultCredentialsSetExplicitly = true
            End If
        End Sub
        
        Public Shadows Property Url() As String
            Get
                Return MyBase.Url
            End Get
            Set
                If (((Me.IsLocalFileSystemWebService(MyBase.Url) = true)  _
                            AndAlso (Me.useDefaultCredentialsSetExplicitly = false))  _
                            AndAlso (Me.IsLocalFileSystemWebService(value) = false)) Then
                    MyBase.UseDefaultCredentials = false
                End If
                MyBase.Url = value
            End Set
        End Property
        
        Public Shadows Property UseDefaultCredentials() As Boolean
            Get
                Return MyBase.UseDefaultCredentials
            End Get
            Set
                MyBase.UseDefaultCredentials = value
                Me.useDefaultCredentialsSetExplicitly = true
            End Set
        End Property
        
        '''<remarks/>
        Public Event SendVoiceSMS_MehrNewCompleted As SendVoiceSMS_MehrNewCompletedEventHandler
        
        '''<remarks/>
        Public Event SendMixedVoiceSMS_SynchNewCompleted As SendMixedVoiceSMS_SynchNewCompletedEventHandler
        
        '''<remarks/>
        Public Event StatusVoiceSMS_Details_MehrCompleted As StatusVoiceSMS_Details_MehrCompletedEventHandler
        
        '''<remarks/>
        <System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://tempuri.org/SendVoiceSMS_MehrNew", RequestNamespace:="http://tempuri.org/", ResponseNamespace:="http://tempuri.org/", Use:=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle:=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)>  _
        Public Function SendVoiceSMS_MehrNew(ByVal strUsername As String, ByVal strPassword As String, ByVal intUid As Integer, ByVal strToken As String, ByVal strName As String, ByVal arrDest() As Object, ByVal intVoiceID As Integer, ByVal repeatTotal As Integer, ByRef strMessage As String) As Integer
            Dim results() As Object = Me.Invoke("SendVoiceSMS_MehrNew", New Object() {strUsername, strPassword, intUid, strToken, strName, arrDest, intVoiceID, repeatTotal, strMessage})
            strMessage = CType(results(1),String)
            Return CType(results(0),Integer)
        End Function
        
        '''<remarks/>
        Public Overloads Sub SendVoiceSMS_MehrNewAsync(ByVal strUsername As String, ByVal strPassword As String, ByVal intUid As Integer, ByVal strToken As String, ByVal strName As String, ByVal arrDest() As Object, ByVal intVoiceID As Integer, ByVal repeatTotal As Integer, ByVal strMessage As String)
            Me.SendVoiceSMS_MehrNewAsync(strUsername, strPassword, intUid, strToken, strName, arrDest, intVoiceID, repeatTotal, strMessage, Nothing)
        End Sub
        
        '''<remarks/>
        Public Overloads Sub SendVoiceSMS_MehrNewAsync(ByVal strUsername As String, ByVal strPassword As String, ByVal intUid As Integer, ByVal strToken As String, ByVal strName As String, ByVal arrDest() As Object, ByVal intVoiceID As Integer, ByVal repeatTotal As Integer, ByVal strMessage As String, ByVal userState As Object)
            If (Me.SendVoiceSMS_MehrNewOperationCompleted Is Nothing) Then
                Me.SendVoiceSMS_MehrNewOperationCompleted = AddressOf Me.OnSendVoiceSMS_MehrNewOperationCompleted
            End If
            Me.InvokeAsync("SendVoiceSMS_MehrNew", New Object() {strUsername, strPassword, intUid, strToken, strName, arrDest, intVoiceID, repeatTotal, strMessage}, Me.SendVoiceSMS_MehrNewOperationCompleted, userState)
        End Sub
        
        Private Sub OnSendVoiceSMS_MehrNewOperationCompleted(ByVal arg As Object)
            If (Not (Me.SendVoiceSMS_MehrNewCompletedEvent) Is Nothing) Then
                Dim invokeArgs As System.Web.Services.Protocols.InvokeCompletedEventArgs = CType(arg,System.Web.Services.Protocols.InvokeCompletedEventArgs)
                RaiseEvent SendVoiceSMS_MehrNewCompleted(Me, New SendVoiceSMS_MehrNewCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState))
            End If
        End Sub
        
        '''<remarks/>
        <System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://tempuri.org/SendMixedVoiceSMS_SynchNew", RequestNamespace:="http://tempuri.org/", ResponseNamespace:="http://tempuri.org/", Use:=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle:=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)>  _
        Public Function SendMixedVoiceSMS_SynchNew(ByVal strUsername As String, ByVal strPassword As String, ByVal intUid As Integer, ByVal strToken As String, ByVal strName As String, ByVal arrDest() As Object, ByVal arrRecords() As Object, ByVal arrNumbers() As Object, ByVal strSayMathod As String, ByRef strMessage As String) As Integer
            Dim results() As Object = Me.Invoke("SendMixedVoiceSMS_SynchNew", New Object() {strUsername, strPassword, intUid, strToken, strName, arrDest, arrRecords, arrNumbers, strSayMathod, strMessage})
            strMessage = CType(results(1),String)
            Return CType(results(0),Integer)
        End Function
        
        '''<remarks/>
        Public Overloads Sub SendMixedVoiceSMS_SynchNewAsync(ByVal strUsername As String, ByVal strPassword As String, ByVal intUid As Integer, ByVal strToken As String, ByVal strName As String, ByVal arrDest() As Object, ByVal arrRecords() As Object, ByVal arrNumbers() As Object, ByVal strSayMathod As String, ByVal strMessage As String)
            Me.SendMixedVoiceSMS_SynchNewAsync(strUsername, strPassword, intUid, strToken, strName, arrDest, arrRecords, arrNumbers, strSayMathod, strMessage, Nothing)
        End Sub
        
        '''<remarks/>
        Public Overloads Sub SendMixedVoiceSMS_SynchNewAsync(ByVal strUsername As String, ByVal strPassword As String, ByVal intUid As Integer, ByVal strToken As String, ByVal strName As String, ByVal arrDest() As Object, ByVal arrRecords() As Object, ByVal arrNumbers() As Object, ByVal strSayMathod As String, ByVal strMessage As String, ByVal userState As Object)
            If (Me.SendMixedVoiceSMS_SynchNewOperationCompleted Is Nothing) Then
                Me.SendMixedVoiceSMS_SynchNewOperationCompleted = AddressOf Me.OnSendMixedVoiceSMS_SynchNewOperationCompleted
            End If
            Me.InvokeAsync("SendMixedVoiceSMS_SynchNew", New Object() {strUsername, strPassword, intUid, strToken, strName, arrDest, arrRecords, arrNumbers, strSayMathod, strMessage}, Me.SendMixedVoiceSMS_SynchNewOperationCompleted, userState)
        End Sub
        
        Private Sub OnSendMixedVoiceSMS_SynchNewOperationCompleted(ByVal arg As Object)
            If (Not (Me.SendMixedVoiceSMS_SynchNewCompletedEvent) Is Nothing) Then
                Dim invokeArgs As System.Web.Services.Protocols.InvokeCompletedEventArgs = CType(arg,System.Web.Services.Protocols.InvokeCompletedEventArgs)
                RaiseEvent SendMixedVoiceSMS_SynchNewCompleted(Me, New SendMixedVoiceSMS_SynchNewCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState))
            End If
        End Sub
        
        '''<remarks/>
        <System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://tempuri.org/StatusVoiceSMS_Details_Mehr", RequestNamespace:="http://tempuri.org/", ResponseNamespace:="http://tempuri.org/", Use:=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle:=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)>  _
        Public Function StatusVoiceSMS_Details_Mehr(ByVal strUsername As String, ByVal strPassword As String, ByVal intUid As Integer, ByVal strToken As String, ByVal strVoiceSMSID As String, ByVal intPageNo As Integer, ByRef strMessage As String) As <System.Xml.Serialization.XmlArrayItemAttribute(IsNullable:=false)> STC_Status()
            Dim results() As Object = Me.Invoke("StatusVoiceSMS_Details_Mehr", New Object() {strUsername, strPassword, intUid, strToken, strVoiceSMSID, intPageNo, strMessage})
            strMessage = CType(results(1),String)
            Return CType(results(0),STC_Status())
        End Function
        
        '''<remarks/>
        Public Overloads Sub StatusVoiceSMS_Details_MehrAsync(ByVal strUsername As String, ByVal strPassword As String, ByVal intUid As Integer, ByVal strToken As String, ByVal strVoiceSMSID As String, ByVal intPageNo As Integer, ByVal strMessage As String)
            Me.StatusVoiceSMS_Details_MehrAsync(strUsername, strPassword, intUid, strToken, strVoiceSMSID, intPageNo, strMessage, Nothing)
        End Sub
        
        '''<remarks/>
        Public Overloads Sub StatusVoiceSMS_Details_MehrAsync(ByVal strUsername As String, ByVal strPassword As String, ByVal intUid As Integer, ByVal strToken As String, ByVal strVoiceSMSID As String, ByVal intPageNo As Integer, ByVal strMessage As String, ByVal userState As Object)
            If (Me.StatusVoiceSMS_Details_MehrOperationCompleted Is Nothing) Then
                Me.StatusVoiceSMS_Details_MehrOperationCompleted = AddressOf Me.OnStatusVoiceSMS_Details_MehrOperationCompleted
            End If
            Me.InvokeAsync("StatusVoiceSMS_Details_Mehr", New Object() {strUsername, strPassword, intUid, strToken, strVoiceSMSID, intPageNo, strMessage}, Me.StatusVoiceSMS_Details_MehrOperationCompleted, userState)
        End Sub
        
        Private Sub OnStatusVoiceSMS_Details_MehrOperationCompleted(ByVal arg As Object)
            If (Not (Me.StatusVoiceSMS_Details_MehrCompletedEvent) Is Nothing) Then
                Dim invokeArgs As System.Web.Services.Protocols.InvokeCompletedEventArgs = CType(arg,System.Web.Services.Protocols.InvokeCompletedEventArgs)
                RaiseEvent StatusVoiceSMS_Details_MehrCompleted(Me, New StatusVoiceSMS_Details_MehrCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState))
            End If
        End Sub
        
        '''<remarks/>
        Public Shadows Sub CancelAsync(ByVal userState As Object)
            MyBase.CancelAsync(userState)
        End Sub
        
        Private Function IsLocalFileSystemWebService(ByVal url As String) As Boolean
            If ((url Is Nothing)  _
                        OrElse (url Is String.Empty)) Then
                Return false
            End If
            Dim wsUri As System.Uri = New System.Uri(url)
            If ((wsUri.Port >= 1024)  _
                        AndAlso (String.Compare(wsUri.Host, "localHost", System.StringComparison.OrdinalIgnoreCase) = 0)) Then
                Return true
            End If
            Return false
        End Function
    End Class
    
    '''<remarks/>
    <System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.7.2102.0"),  _
     System.SerializableAttribute(),  _
     System.Diagnostics.DebuggerStepThroughAttribute(),  _
     System.ComponentModel.DesignerCategoryAttribute("code"),  _
     System.Xml.Serialization.XmlTypeAttribute([Namespace]:="http://tempuri.org/")>  _
    Partial Public Class STC_Status
        
        Private receiverNumberField As String
        
        Private statusField As String
        
        Private answerDurationField As Integer
        
        Private responseField As String
        
        '''<remarks/>
        Public Property ReceiverNumber() As String
            Get
                Return Me.receiverNumberField
            End Get
            Set
                Me.receiverNumberField = value
            End Set
        End Property
        
        '''<remarks/>
        Public Property Status() As String
            Get
                Return Me.statusField
            End Get
            Set
                Me.statusField = value
            End Set
        End Property
        
        '''<remarks/>
        Public Property AnswerDuration() As Integer
            Get
                Return Me.answerDurationField
            End Get
            Set
                Me.answerDurationField = value
            End Set
        End Property
        
        '''<remarks/>
        Public Property Response() As String
            Get
                Return Me.responseField
            End Get
            Set
                Me.responseField = value
            End Set
        End Property
    End Class
    
    '''<remarks/>
    <System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.7.2046.0")>  _
    Public Delegate Sub SendVoiceSMS_MehrNewCompletedEventHandler(ByVal sender As Object, ByVal e As SendVoiceSMS_MehrNewCompletedEventArgs)
    
    '''<remarks/>
    <System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.7.2046.0"),  _
     System.Diagnostics.DebuggerStepThroughAttribute(),  _
     System.ComponentModel.DesignerCategoryAttribute("code")>  _
    Partial Public Class SendVoiceSMS_MehrNewCompletedEventArgs
        Inherits System.ComponentModel.AsyncCompletedEventArgs
        
        Private results() As Object
        
        Friend Sub New(ByVal results() As Object, ByVal exception As System.Exception, ByVal cancelled As Boolean, ByVal userState As Object)
            MyBase.New(exception, cancelled, userState)
            Me.results = results
        End Sub
        
        '''<remarks/>
        Public ReadOnly Property Result() As Integer
            Get
                Me.RaiseExceptionIfNecessary
                Return CType(Me.results(0),Integer)
            End Get
        End Property
        
        '''<remarks/>
        Public ReadOnly Property strMessage() As String
            Get
                Me.RaiseExceptionIfNecessary
                Return CType(Me.results(1),String)
            End Get
        End Property
    End Class
    
    '''<remarks/>
    <System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.7.2046.0")>  _
    Public Delegate Sub SendMixedVoiceSMS_SynchNewCompletedEventHandler(ByVal sender As Object, ByVal e As SendMixedVoiceSMS_SynchNewCompletedEventArgs)
    
    '''<remarks/>
    <System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.7.2046.0"),  _
     System.Diagnostics.DebuggerStepThroughAttribute(),  _
     System.ComponentModel.DesignerCategoryAttribute("code")>  _
    Partial Public Class SendMixedVoiceSMS_SynchNewCompletedEventArgs
        Inherits System.ComponentModel.AsyncCompletedEventArgs
        
        Private results() As Object
        
        Friend Sub New(ByVal results() As Object, ByVal exception As System.Exception, ByVal cancelled As Boolean, ByVal userState As Object)
            MyBase.New(exception, cancelled, userState)
            Me.results = results
        End Sub
        
        '''<remarks/>
        Public ReadOnly Property Result() As Integer
            Get
                Me.RaiseExceptionIfNecessary
                Return CType(Me.results(0),Integer)
            End Get
        End Property
        
        '''<remarks/>
        Public ReadOnly Property strMessage() As String
            Get
                Me.RaiseExceptionIfNecessary
                Return CType(Me.results(1),String)
            End Get
        End Property
    End Class
    
    '''<remarks/>
    <System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.7.2046.0")>  _
    Public Delegate Sub StatusVoiceSMS_Details_MehrCompletedEventHandler(ByVal sender As Object, ByVal e As StatusVoiceSMS_Details_MehrCompletedEventArgs)
    
    '''<remarks/>
    <System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.7.2046.0"),  _
     System.Diagnostics.DebuggerStepThroughAttribute(),  _
     System.ComponentModel.DesignerCategoryAttribute("code")>  _
    Partial Public Class StatusVoiceSMS_Details_MehrCompletedEventArgs
        Inherits System.ComponentModel.AsyncCompletedEventArgs
        
        Private results() As Object
        
        Friend Sub New(ByVal results() As Object, ByVal exception As System.Exception, ByVal cancelled As Boolean, ByVal userState As Object)
            MyBase.New(exception, cancelled, userState)
            Me.results = results
        End Sub
        
        '''<remarks/>
        Public ReadOnly Property Result() As STC_Status()
            Get
                Me.RaiseExceptionIfNecessary
                Return CType(Me.results(0),STC_Status())
            End Get
        End Property
        
        '''<remarks/>
        Public ReadOnly Property strMessage() As String
            Get
                Me.RaiseExceptionIfNecessary
                Return CType(Me.results(1),String)
            End Get
        End Property
    End Class
End Namespace
