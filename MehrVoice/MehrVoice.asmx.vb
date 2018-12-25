Imports System.Web.Services
Imports System.Web.Services.Protocols
Imports System.ComponentModel
Imports Newtonsoft.Json
Imports RestSharp
Imports System.IO
Imports System.Web.HttpResponse
Imports System.Web.Script.Serialization


' To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line.
' <System.Web.Script.Services.ScriptService()> _
<System.Web.Services.WebService(Namespace:="http://tempuri.org/")> _
<System.Web.Services.WebServiceBinding(ConformsTo:=WsiProfiles.BasicProfile1_1)> _
<ToolboxItem(False)> _
Public Class MehrVoice
    Inherits System.Web.Services.WebService



    Public Class JSON_result_send
        Public result As String
        Public [Error] As String

    End Class

    Public Class JSON_resultStatus

        Public requestId As String
        Public targetNumber As String
        Public status As String
        Public billSec As String
        Public requestDate As String
        Public executionDate As String
        Public transactionId As String
        Public balanceSubtracted As String

    End Class

    <WebMethod()>
    Public Function SendVoiceSMS_MehrNew(ByVal strUsername As String, ByVal strPassword As String, ByVal intUid As Integer, ByVal strToken As String, ByVal strName As String, ByVal arrDest() As Object, ByVal intVoiceID As Integer, ByVal repeatTotal As Integer, ByRef strMessage As String) As Integer



        If strUsername = "vesal" AndAlso strPassword = "matchautoreplay123" Then

            Try


                Dim client = New RestClient("http://api.bitel.rest/api/v1/voice/campaign")
                Dim request = New RestRequest(Method.POST)


                request.AddHeader("content-type", "application/json")
                request.AddHeader("authorization", "bearer eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJzdWIiOiIxMyIsImh0dHA6Ly9zY2hlbWFzLm1pY3Jvc29mdC5jb20vd3MvMjAwOC8wNi9pZGVudGl0eS9jbGFpbXMvcm9sZSI6IjEiLCJleHAiOjE1NDcxMDA1MjksImlzcyI6IkJpdGVsIiwiYXVkIjoiQml0ZWwifQ.V6_xPVCQ_6FoovtwqcVtRM_-rKr6mtj42rxfZu7G6bU")

                Dim strDes As String = ""
                For i = 0 To arrDest.Length - 1
                    strDes &= """" & arrDest(i).ToString() & """" & ","
                Next

                strDes = strDes.Substring(0, strDes.Length - 1)

                Dim strVoiceID As String = "15458f5c56da49c8ac53d550e9946df1"

                request.AddParameter("application/json", "{""phoneNumbers"":[" & strDes & "],""voiceId"":""" & strVoiceID & """,""timeToLive"":14400,""retryInterval"":60,""retryCount"":2,""serverId"":""mirdamad1""}", ParameterType.RequestBody)
                Dim response As IRestResponse = client.Execute(request)

                Dim retval As JSON_result_send = (JsonConvert.DeserializeObject(Of JSON_result_send)(response.Content))

                If retval.result Is Nothing Then
                    strMessage = retval.Error
                    Return 0
                Else
                    strMessage = retval.result
                    Return 1

                End If


            Catch ex As Exception
                strMessage = "MEHR: " & ex.Message
                ''  qryZamanak.spr_ZamanakServiceErrors_Insert(strMessage, intUid, intVoiceID)
                Return 0
            End Try

        Else
            strMessage = "MEHR: " & "Invalid username or password."
            '' qryZamanak.spr_ZamanakServiceErrors_Insert(strMessage & "," & strUsername & "," & strPassword, intUid, intVoiceID)
            Return 0
        End If

    End Function


    <WebMethod()>
    Public Function SendMixedVoiceSMS_SynchNew(ByVal strUsername As String, ByVal strPassword As String, ByVal intUid As Integer, ByVal strToken As String, strName As String, ByVal arrDest() As Object, arrRecords() As Object, arrNumbers() As Object, strSayMathod As String, ByRef strMessage As String) As Integer



        If strUsername = "vesal" AndAlso strPassword = "matchautoreplay123" Then

            Try
                Dim client = New RestClient("http://api.bitel.rest/api/v1/voice/campaign/mix")
                Dim request = New RestRequest(Method.POST)


                request.AddHeader("content-type", "application/json")
                request.AddHeader("authorization", "bearer eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJzdWIiOiIxMyIsImh0dHA6Ly9zY2hlbWFzLm1pY3Jvc29mdC5jb20vd3MvMjAwOC8wNi9pZGVudGl0eS9jbGFpbXMvcm9sZSI6IjEiLCJleHAiOjE1NDcxMDA1MjksImlzcyI6IkJpdGVsIiwiYXVkIjoiQml0ZWwifQ.V6_xPVCQ_6FoovtwqcVtRM_-rKr6mtj42rxfZu7G6bU")


                ''request.AddParameter("application/json", "{""phoneNumbers"":[" & strDes & "],""voiceIds"":[""9d08455a81484e53bcb628c53ad496dd"", ""4661ea712eea43cbad1858cb13ef5c81""],""messages"":[" & strNOs & "],""timeToLive"":1800,""retryInterval"":60,""retryCount"":3,""speaker:male"",""serverId:mirdamad1""}", ParameterType.RequestBody)
                ''   Dim response As IRestResponse = client.Execute(request)

                Dim value As String = String.Join(",", arrDest)



                Dim data = New With {
        .phoneNumbers = arrDest,
        .voiceIds = New String() {"9d08455a81484e53bcb628c53ad496dd", "4661ea712eea43cbad1858cb13ef5c81"},
        .messages = arrNumbers,
        .timeToLive = 14400,
        .retryInterval = 5,
        .retryCount = 2,
        .speaker = "male",
        .serverId = "mirdamad1"
    }



                request.AddParameter("application/json", JsonConvert.SerializeObject(data), ParameterType.RequestBody)
                Dim response As IRestResponse = client.Execute(request)



                Dim retval As JSON_result_send = (JsonConvert.DeserializeObject(Of JSON_result_send)(response.Content))

                If retval.result Is Nothing Then
                    strMessage = retval.Error
                    Return 0
                Else
                    strMessage = retval.result
                    Return 1

                End If

            Catch ex As Exception
                strMessage = ex.Message

                Return 0
            End Try

        Else
            strMessage = "MEHR: " & "Invalid username or password."

            Return 0
        End If

    End Function


    Public Structure STC_Status
        Public ReceiverNumber As String
        Public Status As String
        Public AnswerDuration As Integer
        Public Response As String
    End Structure

    <WebMethod()>
    Public Function StatusVoiceSMS_Details_Mehr(ByVal strUsername As String, ByVal strPassword As String, ByVal intUid As Integer, ByVal strToken As String, ByVal strVoiceSMSID As String, ByVal intPageNo As Integer, ByRef strMessage As String) As STC_Status()

        Dim qryZamanak As New dstZamanakTableAdapters.QueriesTableAdapter
        Dim stc_Result As STC_Status() = Nothing
        Dim strError As String = ""

        If strUsername = "vesal" AndAlso strPassword = "matchautoreplay123" Then

            Try
                If strVoiceSMSID = "" Then
                    strMessage = "MEHR: " & "Invalid VoiceSMSID."
                    qryZamanak.spr_ZamanakServiceErrors_Insert(strMessage & "-" & strVoiceSMSID, intUid, -1)
                    Return stc_Result
                End If

                If intPageNo < 1 Then
                    strMessage = "MEHR: " & "Invalid page number."
                    qryZamanak.spr_ZamanakServiceErrors_Insert(strMessage & "-" & strVoiceSMSID, intUid, -1)
                    Return stc_Result
                End If

                ''This IP has been deactived by zamanak company

                Dim client = New RestClient("http://api.bitel.rest/api/v1/voice/campaign/cdr/" & strVoiceSMSID & "")
                Dim request = New RestRequest(Method.[GET])
                request.AddHeader("content-type", "application/json")
                request.AddHeader("authorization", "Bearer eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJzdWIiOiIxMyIsImh0dHA6Ly9zY2hlbWFzLm1pY3Jvc29mdC5jb20vd3MvMjAwOC8wNi9pZGVudGl0eS9jbGFpbXMvcm9sZSI6IjEiLCJleHAiOjE1NDcxMDA1MjksImlzcyI6IkJpdGVsIiwiYXVkIjoiQml0ZWwifQ.V6_xPVCQ_6FoovtwqcVtRM_-rKr6mtj42rxfZu7G6bU")
                Dim response As IRestResponse = client.Execute(request)
                Dim retval As String = response.Content.ToString()

                Dim i As Integer = 0
                ReDim Preserve stc_Result(99)

                For Each Element As String In SplitJSON(CharTrim(retval))
                    Dim elName As String = CharTrim(Element.Split(":")(0).Trim)
                    Select Case Element.Split(":")(1).Trim.Substring(0, 1)
                        Case "{"

                        Case "["
                            Dim ElArray As New List(Of JSON_resultStatus)
                            For Each ArrayElement As String In SplitJSON(CharTrim(Element.Substring(InStr(Element, ":"))))

                                Dim retval1 As JSON_resultStatus = JsonConvert.DeserializeObject(Of JSON_resultStatus)(ArrayElement)
                                stc_Result(i).ReceiverNumber = retval1.targetNumber
                                stc_Result(i).Status = retval1.status
                                stc_Result(i).AnswerDuration = retval1.billSec

                                i += 1
                            Next

                        Case Else
                            strError = Element.Split(":")(1)

                    End Select

                Next


                If strError <> "null" Then
                    strMessage = "MEHR: " & strError
                    qryZamanak.spr_ZamanakServiceErrors_Insert(strMessage & "-" & strVoiceSMSID, intUid, -1)
                    Return stc_Result

                End If

                ''If result.Count() = 2 Then
                ''    strMessage = "MEHR: " &    ''DirectCast(result(1), System.Xml.XmlElement).InnerText.Remove(0, 6)
                ''    qryZamanak.spr_ZamanakServiceErrors_Insert(strMessage, intUid, intVoiceSMSID)
                ''    Return stc_Result
                ''End If

                Return stc_Result

            Catch ex As Exception
                strMessage = "MEHR: " & ex.Message
                qryZamanak.spr_ZamanakServiceErrors_Insert(strMessage & "-" & strVoiceSMSID, intUid, -1)
                Return stc_Result
            End Try

        Else
            strMessage = "MEHR: " & "Invalid username or password."
            qryZamanak.spr_ZamanakServiceErrors_Insert(strMessage & "," & strUsername & "," & strPassword, intUid, -1)
            Return stc_Result
        End If

    End Function


    Public Shared Function CharTrim(ByVal Str As String) As String
        Return Str.Trim.Substring(1, Str.Length - 2)
    End Function
    Private Function SplitJSON(ByVal str As String) As String()

        Dim ret() As String = Nothing
        Dim sqCount As Integer = 0
        Dim clCount As Integer = 0
        Dim buildStr As New System.Text.StringBuilder

        For i As Integer = 0 To str.Length - 1
            Select Case str.Substring(i, 1)
                Case ","
                    If sqCount = 0 And clCount = 0 Then
                        Try
                            ReDim Preserve ret(UBound(ret) + 1)
                        Catch ex As Exception
                            ReDim ret(0)
                        Finally
                            ret(UBound(ret)) = buildStr.ToString
                            buildStr = New System.Text.StringBuilder
                        End Try
                    Else
                        buildStr.Append(str.Substring(i, 1))
                    End If
                Case Else
                    buildStr.Append(str.Substring(i, 1))
                    Select Case str.Substring(i, 1)
                        Case "["
                            sqCount += 1
                        Case "]"
                            sqCount -= 1
                        Case "{"
                            clCount += 1
                        Case "}"
                            clCount -= 1

                    End Select
            End Select
        Next
        If buildStr.ToString.Length > 0 Then
            Try
                ReDim Preserve ret(UBound(ret) + 1)
            Catch ex As Exception
                ReDim ret(0)
            Finally
                ret(UBound(ret)) = buildStr.ToString
            End Try
        End If
        Return ret

    End Function


End Class