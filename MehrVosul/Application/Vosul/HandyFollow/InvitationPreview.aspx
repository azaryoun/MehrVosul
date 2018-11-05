<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="InvitationPreview.aspx.vb" Inherits="MehrVosul.InvitationPreview" %>

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
            font-family: "B Nazanin";
            font-weight: bold;
            font-size: medium;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">

        <div id="divMain" runat="server">
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
                                        <tr>

                                            <td>
                                                <asp:Label ID="lblRegisterNO" runat="server"></asp:Label></td>
                                            <td>پیوست</td>
                                        </tr>


                                        <tr>

                                            <td colspan="2">طبقه بندی عادی</td>
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
                                <td style="text-align: center;" class="auto-style1" colspan="2">اقتصاد مقاومتی ، اقدام و عمل</td>
                            </tr>
                            <tr>
                                <td style="font-family: 'b nazanin'; font-size: medium; text-align: right; font-weight: bold; padding-right: 2px;" dir="rtl" colspan="2">جناب آقای/خانم:
                                    <asp:Label ID="lblFullName" runat="server"></asp:Label>
                                    <br />
                                    موضوع: دعوتنامه</td>

                            </tr>

                            <tr>

                                <td dir="rtl" style="font-family: 'b nazanin'; font-size: medium; font-weight: normal; padding-right: 2px;"><strong>سلام علیکم</strong><br />
                                    <br />
                                    <span style="font-family: 'B Badr'">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; با صلوات بر محمد وآل محمد(ص</span>); با احترام ، پیرو پیگیری و مکاتبات صورت پذیرفته در خصوص عدم ایفای تعهدات جنابعالی، مستدعیست جهت مذاکره در خصوص موضوع در مورخه&nbsp;
                                    <asp:Label ID="lblLetterDate" runat="server"></asp:Label>
                                    &nbsp;راس ساعت
                                    <asp:Label ID="lblTime" runat="server"></asp:Label>
                                    &nbsp;به                                
                                    محل شعبه مراجعه فرمایید.
                                    <br />
                                    <br />
                                    <br />
                                    <br />
                                </td>
                            </tr>
                            <tr>
                                <td dir="rtl" style="font-family: 'b nazanin'; font-size: medium; font-weight: normal; padding-right: 2px; text-align: center">با سپاس<br />
                                    رئیس کمیسیون مصالحه<br />
                                    شعبه:<asp:Label ID="lblBranch" runat="server"></asp:Label><br />
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
