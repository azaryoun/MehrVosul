Imports System
Imports System.IO
Imports System.Resources
Imports System.Net
Imports System.IO.FileStream
Imports System.IO.File
Imports System.Data.OleDb
Imports System.Xml
Imports System.Configuration

Public Class clsCorrespondSMS

    Public Function SendSMS_LikeToLike(ByVal Message() As String, ByVal DestinationAddress() As String, ByVal UserName As String, ByVal Password As String, ByVal SenderNumber As String, ByVal IPAddress As String, ByVal Company As String, ByVal BatchID As String) As String()

        Dim RetValue(1) As String
        RetValue(0) = "False"
        RetValue(1) = 0

        If Message.Length <> DestinationAddress.Length Then
            RetValue(1) = "Incorrect array size for Messages and Destinations"
            Return RetValue
        End If

        Dim DestSize As Integer = DestinationAddress.Length

        Try

            Dim txt As String = ""
            Dim ackType As String = "Full"
            txt = "<?xml version=""1.0"" encoding=""UTF-8""?>" & vbCrLf
            txt = txt & "<!DOCTYPE smsBatch PUBLIC ""-//PERVASIVE//DTD CPAS 1.0//EN"" ""http://www.ubicomp.ir/dtd/Cpas.dtd"">" & vbCrLf
            txt = txt & "<smsBatch  ackType=""" & ackType & """ company=""" & Company & """ batchID=""" & BatchID & """>" & vbCrLf

            For i = 0 To DestSize - 1

                Dim strMessage As String = Message(i)
                Dim strDestinationAddress As String = DestinationAddress(i)
                Dim IsFarsi As Boolean = Language(strMessage)

                If (IsFarsi) Then
                    strMessage = C2Unicode(strMessage)
                End If

                txt = txt & "<sms" & IIf(IsFarsi, " binary=""true"" dcs=""8""", " binary=""false"" dcs=""0""") & ">" & vbCrLf
                txt = txt & "<origAddr><![CDATA[" & correctNumber(SenderNumber) & "]]></origAddr>" & vbCrLf
                txt = txt & "<destAddr><![CDATA[" & correctNumber(strDestinationAddress) & "]]></destAddr>" & vbCrLf
                txt = txt & "<message><![CDATA[" & strMessage & "]]></message>" & vbCrLf
                txt = txt & "</sms>"
            Next
            txt = txt & vbCrLf & "</smsBatch>"

            Dim DataToPost As String = txt
            Dim WebRequest As WebRequest
            Dim RequestStream As Stream
            Dim StreamWriter As StreamWriter
            Dim WebResponse As WebResponse
            Dim ResponseStream As Stream
            Dim StreamReader As StreamReader

            WebRequest = WebRequest.Create(IPAddress)
            WebRequest.Method = "POST"
            WebRequest.ContentType = "text/xml"
            WebRequest.Headers.Add("authorization", "Basic " & Base64Encode(UserName & ":" & Password))
            RequestStream = WebRequest.GetRequestStream()
            StreamWriter = New StreamWriter(RequestStream)
            StreamWriter.Write(DataToPost)
            StreamWriter.Flush()
            StreamWriter.Close()
            RequestStream.Close()

            WebResponse = WebRequest.GetResponse()
            ResponseStream = WebResponse.GetResponseStream()
            StreamReader = New StreamReader(ResponseStream)
            Dim dataToReceive As String = StreamReader.ReadToEnd()
            StreamReader.Close()
            ResponseStream.Close()
            WebResponse.Close()

            If InStr(dataToReceive, "CHECK_OK", CompareMethod.Text) > 0 Then
                RetValue(0) = "CHECK_OK"
                RetValue(1) = BatchID
            Else
                Try
                    Dim Sindex, EIndex, MSG
                    Sindex = InStr(dataToReceive, "CDATA[", CompareMethod.Text)
                    EIndex = InStr(dataToReceive, "]]", CompareMethod.Text)
                    MSG = dataToReceive.Substring(Sindex + "CDATA[".Length - 1, EIndex - Sindex - "CDATA[".Length)
                    RetValue(1) = MSG
                    Return RetValue
                Catch ex As Exception
                    RetValue(1) = ex.Message.ToString()
                    Return RetValue
                End Try
            End If

        Catch ex As Exception
            RetValue(1) = ex.Message.ToString()
            Return RetValue
        End Try

        Return RetValue

    End Function

    Public Function correctNumber(ByVal uNumber As String) As String
        Dim ret As String = Trim(uNumber)
        If ret.Substring(0, 4) = "0098" Then ret = ret.Remove(0, 4)
        If ret.Substring(0, 3) = "098" Then ret = ret.Remove(0, 3)
        If ret.Substring(0, 3) = "+98" Then ret = ret.Remove(0, 3)
        If ret.Substring(0, 2) = "98" Then ret = ret.Remove(0, 2)
        If ret.Substring(0, 1) = "0" Then ret = ret.Remove(0, 1)
        'If ret.Substring(0, 2) = "91" Then ret = "0" + ret
        Return "+98" & ret
    End Function

    Public Function C2Unicode(ByVal uMessage As String) As String

        Dim i As Integer
        Dim IntPreUnicode As Integer
        Dim ret As String
        Dim PreUnicode As String
        ret = ""

        For i = 0 To Len(uMessage) - 1
            'ret = ret & String( Len(Conversion.Hex(AscW(Mid(uMessage, i, 1)))),"0") & Conversion.Hex(AscW(Mid(uMessage, i, 1)))
            '(4 - Conversion.Hex(Strings.AscW(Data.Substring(0, 1).ToString())).Length);
            IntPreUnicode = 4 - Conversion.Hex(AscW(uMessage.Substring(i, 1).ToString())).Length
            PreUnicode = String.Format("{0:D" + IntPreUnicode.ToString() + "}", 0)
            ret = ret & PreUnicode & Conversion.Hex(AscW(uMessage.Substring(i, 1)))
        Next
        C2Unicode = ret

    End Function

    Private Function MyASC(ByVal OneChar As String) As Integer
        If OneChar = "" Then MyASC = 0 Else MyASC = Asc(OneChar)
    End Function

    Private Function Base64Encode(ByVal inData As String) As String
        'rfc1521
        '2001 Antonin Foller, Motobit Software, http://Motobit.cz
        Const Base64 As String = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789+/"
        Dim sOut As String = ""
        Dim I As Integer

        'For each group of 3 bytes
        For I = 1 To Len(inData) Step 3
            Dim nGroup, pOut As String

            'Create one long from this 3 bytes.
            nGroup = &H10000 * Asc(Mid(inData, I, 1)) + _
              &H100 * MyASC(Mid(inData, I + 1, 1)) + MyASC(Mid(inData, I + 2, 1))

            'Oct splits the long To 8 groups with 3 bits
            nGroup = Oct(nGroup)

            'Add leading zeros
            nGroup = StrDup(8 - Len(nGroup), "0") & nGroup

            'Convert To base64
            pOut = Mid(Base64, CLng("&o" & Mid(nGroup, 1, 2)) + 1, 1) + _
              Mid(Base64, CLng("&o" & Mid(nGroup, 3, 2)) + 1, 1) + _
              Mid(Base64, CLng("&o" & Mid(nGroup, 5, 2)) + 1, 1) + _
              Mid(Base64, CLng("&o" & Mid(nGroup, 7, 2)) + 1, 1)


            'Add the part To OutPut string
            sOut = sOut + pOut

            'Add a new line For Each 76 chars In dest (76*3/4 = 57)
            'If (I + 2) Mod 57 = 0 Then sOut = sOut + vbCrLf
        Next
        Select Case Len(inData) Mod 3
            Case 1 '8 bit final
                'sOut = Left(sOut, Len(sOut) - 2) + "=="
                sOut = sOut.Substring(0, Len(sOut) - 2) + "=="



            Case 2 '16 bit final
                'sOut = Left(sOut, Len(sOut) - 1) + "="
                sOut = sOut.Substring(0, Len(sOut) - 1) + "="
        End Select
        Base64Encode = sOut
    End Function

    Private Function Language(ByVal Datas As String) As Boolean
        Dim Slng As Boolean = False
        If Datas.Substring(0, 2) = "00" Then ' Mean 's English 
            Slng = False ' 
        Else
            Slng = True
        End If
        Return Slng

    End Function
End Class
