Imports WkHtmlToXSharp
Imports System.IO


Public Class ScriptGenerator

    Public Property ColName() As String
        Get
            Return m_ColName
        End Get
        Set(ByVal value As String)
            m_ColName = value
        End Set
    End Property
    Private m_ColName As String


    Public Property ColCaption() As String
        Get
            Return m_ColCaption
        End Get
        Set(ByVal value As String)
            m_ColCaption = value
        End Set
    End Property
    Private m_ColCaption As String


    Public Property ColType() As String
        Get
            Return m_ColType
        End Get
        Set(ByVal value As String)
            m_ColType = value
        End Set
    End Property
    Private m_ColType As String

    Public Property ColVisibilty() As Boolean
        Get
            Return m_ColVisibilty
        End Get
        Set(ByVal value As Boolean)
            m_ColVisibilty = value
        End Set
    End Property
    Private m_ColVisibilty As Boolean




    Public Property ColShowOutput() As Boolean
        Get
            Return m_ColShowOutput
        End Get
        Set(ByVal value As Boolean)
            m_ColShowOutput = value
        End Set
    End Property
    Private m_ColShowOutput As Boolean


    Public Property ColSource() As String
        Get
            Return m_ColSource
        End Get
        Set(ByVal value As String)
            m_ColSource = value
        End Set
    End Property
    Private m_ColSource As String


  


End Class


Public Class clsMakePDFReport


    'Dim objMakePDFReport As New clsMakePDFReport
    '    strResTable = objMakePDFReport.MakeTableReport(dtblTheReport, UC_MakeFilter1.lstHeaders)

    'Dim strLogoPath As String = Server.MapPath("~") & "Images\nidc.jpg"
    'Dim rndVar As New Random
    'Dim objMakePDFReport As New clsMakePDFReport
    'Dim ba() As Byte = objMakePDFReport.MakePDFReport(strResTable, strLogoPath, strReportTitle)
    '        IO.File.WriteAllBytes(Server.MapPath("~") & "Temp\Report.pdf", ba)
    '        Response.Clear()
    '        Response.ContentType = "application/pdf"
    '        Response.AppendHeader("Content-Disposition", "attachment; filename=" &   rndVar.Next(1, 15000).tostring & ".pdf")
    '        Response.TransmitFile(Server.MapPath("~") & "Temp\Report.pdf")
    '        Response.End()



    Public Function MakeTableReport(ByVal dtblData As DataTable, ByVal lstScriptGenerator As List(Of ScriptGenerator), Optional LinkColumn As Integer = -1) As String


        Dim strHTMLTable As String = "<table border='1'   id='tblMakeTableReport_Results' dir='rtl' align='center' cellpadding='0' cellspacing='0'  >"
        strHTMLTable &= "<tr align='center' style='background-color:#D6D6D6'>"
        strHTMLTable &= "<th style='padding:2px 5px'>"
        strHTMLTable &= "ردیف"
        strHTMLTable &= "</th>"
        For Each scriptGenerator As ScriptGenerator In lstScriptGenerator
            If scriptGenerator.ColShowOutput Then
                strHTMLTable &= "<th style='padding:2px 5px'>"
                strHTMLTable &= scriptGenerator.ColCaption
                strHTMLTable &= "</th>"
            End If

        Next
        strHTMLTable &= "</tr>"

        For i As Integer = 0 To dtblData.Rows.Count - 1
            If i Mod 2 = 1 Then
                strHTMLTable &= "<tr style='background-color:#EEEEEE'>"
            Else
                strHTMLTable &= "<tr>"
            End If

            strHTMLTable &= "<td style='padding:3px;text-align:center'>"
            strHTMLTable &= (i + 1).ToString
            strHTMLTable &= "</td>"
            For k As Integer = 0 To (dtblData.Columns.Count) - 1
                If lstScriptGenerator(k).ColShowOutput Then

                    If k = LinkColumn Then
                        strHTMLTable &= "<td style='padding:3px'><a href='#' onclick='MakeTableReport(" & (i + 1).ToString & ");'>"

                    Else
                        strHTMLTable &= "<td style='padding:3px'>"

                    End If

                 

                    If dtblData.Rows(i)(lstScriptGenerator(k).ColCaption) Is DBNull.Value Then
                        strHTMLTable &= "&nbsp;"
                    Else

                        If lstScriptGenerator(k).ColType.ToLower = "datetime" Then
                            strHTMLTable &= mdlGeneral.GetPersianDate(CDate(dtblData.Rows(i)(lstScriptGenerator(k).ColCaption)))

                        ElseIf lstScriptGenerator(k).ColType.ToLower = "boolean" Then
                            strHTMLTable &= If(CBool(dtblData.Rows(i)(lstScriptGenerator(k).ColCaption)) = True, "بلی", "خیر")

                        ElseIf lstScriptGenerator(k).ColType.ToLower.StartsWith("int") = True Then
                            strHTMLTable &= CLng(dtblData.Rows(i)(lstScriptGenerator(k).ColCaption)).ToString("n0")

                        ElseIf lstScriptGenerator(k).ColType.ToLower.StartsWith("decimal") = True OrElse lstScriptGenerator(k).ColType.ToLower.StartsWith("single") = True Then
                            strHTMLTable &= CDbl(dtblData.Rows(i)(lstScriptGenerator(k).ColCaption)).ToString("n2")

                        Else

                            strHTMLTable &= dtblData.Rows(i)(lstScriptGenerator(k).ColCaption).ToString()
                        End If

                    End If

                    If k = LinkColumn Then
                        strHTMLTable &= "</a></td>"
                    Else
                        strHTMLTable &= "</td>"

                    End If

                

                End If
            Next k
            strHTMLTable &= "</tr>"
        Next

        strHTMLTable &= ""
        strHTMLTable &= "</table>"



        Return strHTMLTable




    End Function


    Public Function MakePDFReport(ByVal strHTMLTable As String, ByVal strLogoPath As String, ByVal strReportTitle As String) As Byte()




        Dim htmlToPdfConverter As New MultiplexingConverter
        Dim strHTMLforPDF As String = "<html><body dir='rtl'><table width='100%'><tr><td style='text-align:left;padding-left:10px'><table width='100%'><tr><td width='100%'><img src='" & strLogoPath & "' ></td><td>" & mdlGeneral.GetPersianDate(Date.Now) & " </td></tr></table></td></tr>"
        strHTMLforPDF &= "<tr><td style='text-align:center;font-size:35px;font-family:Tahoma'>" & strReportTitle & "</td></tr><tr><td><br/></td></tr>"
        strHTMLforPDF &= "<tr><td><div style='font-family:b titr;text-align:center'>" & strHTMLTable & "</div></td></tr></table></body></html>"


        htmlToPdfConverter.GlobalSettings.ImageQuality = 100
        htmlToPdfConverter.ObjectSettings.Load.BlockLocalFileAccess = False
        htmlToPdfConverter.ObjectSettings.Web.DefaultEncoding = "UTF-8"
        htmlToPdfConverter.ObjectSettings.Load.LoadErrorHandling = LoadErrorHandlingType.ignore


        htmlToPdfConverter.GlobalSettings.Margin.Top = "5mm"
        htmlToPdfConverter.GlobalSettings.Margin.Bottom = "5mm"
        htmlToPdfConverter.GlobalSettings.Margin.Left = "3mm"
        htmlToPdfConverter.GlobalSettings.Margin.Right = "3mm"

        htmlToPdfConverter.ObjectSettings.Web.PrintMediaType = True
        htmlToPdfConverter.ObjectSettings.Web.LoadImages = True
        htmlToPdfConverter.ObjectSettings.Web.EnablePlugins = False
        htmlToPdfConverter.ObjectSettings.Web.EnableJavascript = True

        Dim ba() As Byte = htmlToPdfConverter.Convert(strHTMLforPDF)
        htmlToPdfConverter.Dispose()

        Return ba


    End Function




End Class
