<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="ReductionSalary.aspx.vb" Inherits="MehrVosul.ReductionSalary" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <script language="javascript" type="text/javascript">
        function printPage() {

            btnprint.style.visibility = 'hidden';
            window.print();
        }
    </script>
    <style type="text/css">
        @page {
            size: auto; /* auto is the initial value */
            margin: 0; /* this affects the margin in the printer settings */
        }

        .auto-style1 {
            height: 28px;
        }
        .auto-style2 {
            text-align: right;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">

        <div id="divMain" runat="server">
            <br />
        </div>

        <div id="divMain2" runat="server">
            <table style="padding: 0; border-spacing: 0;">
                <tr>
                    <td>
                        <table style="height: 100%; width: 100%; font-family: 'B Nazanin'; text-align: right; padding: 0; border-spacing: 0;">
                            <tr>
                                <td style="width: 180px;">
                                    <table>

                                        <tr dir="rtl">

                                            <td>
                                                <asp:Label ID="lblDate" runat="server"></asp:Label></td>
                                            <td>تاریخ
                                            </td>

                                        </tr>

                                        <tr>

                                            <td>
                                                <asp:Label ID="lblLetterNO" runat="server"></asp:Label>
                                            </td>
                                            <td>شماره</td>
                                        </tr>


                                    </table>
                                </td>
                                <td style="text-align: center;" dir="rtl"><span style="font-family: 'B Nazanin'">بسمه تعالی</span><br />
                                    <br />
                                </td>

                                <td style="text-align: center;" dir="rtl">
                                    <asp:Image ID="Image1" runat="server" CssClass="auto-style7" ImageUrl="~/Images/System/MehrLogoPrint.jpg" Height="52px" Width="92px" />
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr>
                    <td style="padding-left: 8px">

                        <table style="align-content: center; padding: 0; border-spacing: 0">

                            <tr>
                                <td style="font-family: 'b nazanin'; font-size: medium; text-align: right; padding-right: 2px;" dir="rtl">&nbsp;</td>

                                </tr>

                            <tr>
                                <td style="font-family: 'b nazanin'; font-size: medium; text-align: right; font-weight: bold; padding-right: 2px;" dir="rtl">به :
                                    <asp:Label ID="lblCompanyName" runat="server"></asp:Label>
                                    &nbsp; </td>
                            </tr>

                            <tr>
                                <td style="font-family: 'b nazanin'; font-size: medium; text-align: right; font-weight: bold; padding-right: 2px;" dir="rtl">از: بانک مهر اقتصاد شعبه
                                    <asp:Label ID="lblBranchName" runat="server"></asp:Label></td>

                            </tr>

                            <tr>
                                <td style="font-family: 'b nazanin'; font-size: medium; text-align: right; padding-right: 2px;" dir="rtl" class="auto-style1">موضوع: کسر از حقوق</td>

                            </tr>

                            <tr>

                                <td dir="rtl" style="font-family: 'b nazanin'; font-size: medium; font-weight: normal; padding-right: 2px;" class="auto-style2">با سلام<br />
                                    <br />
                                    <span style="font-family: 'B Badr'">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; </span>احتراما، عطف به نامه کسر از حقوق&nbsp;
                                    <asp:Label ID="lblDSLetterNO" runat="server"></asp:Label>
                                    &nbsp;مورخ
                                    <asp:Label ID="lblDSLetterDate" runat="server"></asp:Label>
                                    &nbsp;آن سازمان محترم و به پیوست تصویر نامه، به استحضار می رساند اقساط وام آقا/خانم                                  
                                    &nbsp;<asp:Label ID="lblFullName" runat="server"></asp:Label>
                                    &nbsp;به ضمانت
                                    <asp:Label ID="lblNotification" runat="server"></asp:Label>
                                    &nbsp;معوق گردیده است که مبلغ هر فسط ایشان ماهیانه&nbsp;
                                    <asp:Label ID="lblInstallmentAmount" runat="server"></asp:Label>
                                    &nbsp; ریال و مبلغ کل بدهی&nbsp;
                                    <asp:Label ID="lblDefferedAmount" runat="server"></asp:Label>
                                    &nbsp;ریال میباشد. خواهشمند است نسبت به&nbsp; کسر از حقوق نامبرده اقدام و تا مادامی، که نامه ای جهت عدم کسر از حقوق از سوی این سازمان مکاتبه نگردیده از حقوق نامبرده کسر و به
                                    حساب&nbsp;
                                    <asp:Label ID="lblDescription" runat="server"></asp:Label>
                                    &nbsp;و یا&nbsp; در صورت صدور چک، در وجه <span style="font-weight: bold">&quot;بانک مهر اقتصاد&quot;</span> صادر و به شعبه ارسال نمایید.<br />
                                    <br />
                                    <br />
                                </td>
                            </tr>
                            <tr>
                                <td dir="rtl" style="font-family: 'b nazanin'; font-size: medium; font-weight: normal; padding-right: 2px; text-align: center">با تشکر<br />
                                    رئیس شعبه&nbsp;
                                    <asp:Label ID="lblBranch" runat="server"></asp:Label><br />
                                    <br />
                                    <br />
                                </td>
                            </tr>
                        </table>

                        <br />
                    </td>
                </tr>
            </table>



        </div>

        <button id="btnprint" style="visibility: visible" onclick="printPage();">چاپ</button>
    </form>
</body>
</html>
