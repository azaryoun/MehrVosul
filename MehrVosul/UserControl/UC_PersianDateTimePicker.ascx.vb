Public Partial Class UC_PersianDateTimePicker
    Inherits System.Web.UI.UserControl
    Public strAbsPath As String
    Public strCalenderFuncName As String


    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Call SetAbosulePath()

            For i As Integer = 0 To Request.Form.Count - 1

                'ctl00_ContentPlaceHolder1_UC_PersianDateTimePicker_To_
                ' Dim m As String = Me.ID
                'If Request.Form.Keys(i).IndexOf("txtDate") <> -1 Then
                If Request.Form.Keys(i) Is Nothing Then Continue For

                If Request.Form.Keys(i).IndexOf(Me.ID & "$txtDate") <> -1 Then
                    txtDate.Text = Request.Form(i)
                End If
            Next
            If txtDate.Text = "" Then
                '  PersianDate = mdlGeneral.GetPersianDate(Date.Now)
                ' txtDate.Text = mdlGeneral.GetPersianDate(Date.Now)
            End If

        '  strCalenderFuncName = "func_" & Me.ID & "()"
            'imgCalender.Attributes.Add("onclick", "return  strCalenderFuncName()")
            Dim cs As ClientScriptManager = Page.ClientScript

            Dim cstext1 As String = ""






        'strCalenderFuncName = "    $(function () {" _
        '     & "" _
        '     & "var txtDateName = '" & txtDate.ClientID & "';" _
        '     & "$('#' + txtDateName).datepicker({" _
        '        & "showOn: 'button'," _
        '        & "buttonImage: '/Images/PersianDatePicker/cal.gif'," _
        '        & "dateFormat:  'yy/mm/dd'," _
        '        & "autoSize: true," _
        '        & "yearRange: '1380:1400'," _
        '        & "changeMonth: true," _
        '        & "changeYear: true" _
        '        & " });});"

        '      If Me.Parent.FindControl("ScriptManager1") IsNot Nothing Then

        '    Dim ctrl As ScriptManager = Me.Parent.FindControl("ScriptManager1")
        '    ctrl.RegisterStartupScrip(ctrl, Me.GetType, "K" & Rnd(), strCalenderFuncName, True)


        'End If


        '  If Me.p Then


        ''    cs.RegisterStartupScript(Me.GetType(), "PopupScript", cstext1, True)

        'Response.Write("<script>" & cstext1 & "</script>")



    End Sub

    Private _PersianDate As String

    Public Property PersianDate() As String
        Get
            Return txtDate.Text
        End Get
        Set(ByVal value As String)
            _PersianDate = value
            txtDate.Text = _PersianDate
        End Set
    End Property


    Public Property TabIndex() As Integer
        Get
            Return txtDate.TabIndex
        End Get
        Set(ByVal value As Integer)
            txtDate.TabIndex = value
            'imgCalender.TabIndex = value

        End Set
    End Property


    Public Property Enabled() As Boolean
        Get
            'Return imgCalender.Enabled
        End Get
        Set(ByVal value As Boolean)
            'imgCalender.Enabled = value
        End Set
    End Property




    

    Private Sub SetAbosulePath()
        Dim strTemp As String = ""
        Dim intPos As Integer = Request.ServerVariables("APPL_MD_PATH").ToUpper.IndexOf("ROOT")

        If intPos = -1 Then
            strAbsPath = "http://" & Request.ServerVariables("HTTP_HOST") & "/"
        Else
            strAbsPath = "http://" & Request.ServerVariables("HTTP_HOST") & Request.ServerVariables("APPL_MD_PATH").Substring(intPos + 4) & "/"
        End If
    End Sub



    Public Sub AddBlurFunction(ByVal strFunctionName As String)

        txtDate.Attributes.Add("onblur", "return " & strFunctionName)



    End Sub




End Class