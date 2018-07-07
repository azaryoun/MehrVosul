<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="ManifestPreview.aspx.vb" Inherits="MehrVosul.ManifestPreview" %>

<!DOCTYPE html>

<html>
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


    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <br />

            <table style="border: 2px solid #000000; height: 100%; width: 100%; padding: 0; border-spacing: 0;">
                <tr>
                    <td style="border-bottom-style: solid; border-bottom-width: 2px; border-bottom-color: #000000">
                        <table style="height: 100%; width: 100%; font-family: 'B Nazanin'; text-align: right; padding: 0; border-spacing: 0;">
                            <tr>
                                <td style="width: 150px;">
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


                                    </table>
                                </td>
                                <td style="text-align: center;" dir="rtl"><span style="font-family: 'B Nazanin'">بسمه تعالی</span><br />
                                    <span style="font-family: 'B Nazanin'; font-size: x-large; font-weight: bold">بانک مهر اقتصاد</span>
                                    <br />
                                    <span style="font-family: 'B Nazanin'; font-size: small;">شماره ثبت 7788</span></td>

                                <td style="text-align: center;" dir="rtl">
                                    <asp:Image ID="Image1" runat="server" CssClass="auto-style7" ImageUrl="~/Images/System/MehrLogoPrint.jpg" Height="52px" Width="92px" />
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr>
                    <td style="text-align: center">
                        <span style="font-family: Titr; font-size: larger;">(3)برگ اخطاریه</span>
                    </td>
                </tr>
                <tr>
                    <td style="padding-left: 8px">

                        <table style="align-content: center; width: 99%; border-style: solid; border-width: 2px; border-color: black; padding: 0; border-spacing: 0">
                            <tr>
                                <td style="border-right-style: solid; border-right-width: 2px; border-right-color: #000000; border-bottom-style: solid; border-bottom-width: 2px; border-bottom-color: #000000; text-align: center; font-family: 'B Nazanin'; font-size: medium; font-weight: bold; width: 50%">مشخصات و اقامتگاه مخاطب</td>
                                <td style="border-bottom-style: solid; border-bottom-width: 2px; border-bottom-color: #000000; text-align: center; font-family: 'B Nazanin'; font-size: medium; font-weight: bold;">مشخصات و اقامتگاه اخطار کننده</td>
                            </tr>
                            <tr>
                                <td style="font-family: 'b nazanin'; font-size: medium; border-right-style: solid; border-bottom-style: solid; border-right-width: 2px; border-bottom-width: 2px; border-right-color: #000000; border-bottom-color: #000000; text-align: right; width: 50%; font-weight: bold; padding-right: 2px;" dir="rtl">آدرس:
                                    <asp:Label ID="lblAddress2" runat="server"></asp:Label><br />
                                    کد ملی:
                                    <asp:Label ID="lblNationalID" runat="server"></asp:Label><br />
                                    همراه:
                                    <asp:Label ID="lblMobileNO" runat="server"></asp:Label></td>
                                <td style="border-bottom-style: solid; border-bottom-width: 2px; border-bottom-color: #000000; text-align: right; font-family: 'b nazanin'; font-size: medium; padding-right: 4px; font-weight: bold; padding-top: 0px;" dir="rtl">بانک مهر اقتصاد<br />
                                    شعبه:
                                    <asp:Label ID="lblBranch" runat="server"></asp:Label><br />
                                    به نشانی:
                                    <asp:Label ID="lblAddress" runat="server"></asp:Label>
                                </td>

                            </tr>

                            <tr>
                                <td style="border-right-style: solid; border-right-width: 2px; border-right-color: #000000; font-family: 'b nazanin'; font-size: medium; font-weight: bold; vertical-align: top" dir="rtl">شماره تسهیلات:<asp:Label ID="lblLoan" runat="server"></asp:Label><br />
                                    <asp:Label ID="lblLoanType" runat="server"></asp:Label>

                                </td>
                                <td dir="rtl" style="font-family: 'b nazanin'; font-size: medium; font-weight: normal; padding-right: 2px;">مخاطب محترم خانم / آقای
                                    <asp:Label ID="lblFullName" runat="server"></asp:Label>
                                    <br />
                                    <br />
                                    سلام علیکم<br />
                                    پیرو اخطاریه تلفنی به اطلاع می رساند جانبعالی به موجب اسناد تعهدآور موجود، به این بانک بدهکار می باشید.نظر یه اینکه تاکنون بدهی خود را پرداخت ننموده اید بدینوسیله برای آخرین بار جنابعالی اخطار می گردد کلیه دیون خود را حداکثر ظرف مهلت 3 روز پس از روئیت این اخطاریه پرداخت نمایید در غیر اینصورت نسبت به تعقیبات قضایی از طریق مراجع ذیصلاح اقدام و در این صورت کلیه خسارات قانونی به عهده جنابعالی خواهد بود.<br />
                                    <br />
                                    <br />
                                    مسئول شعبه<br />
                                    <br />
                                    رونوشت: مدیریت حقوقی سرپرستی استان <asp:Label ID="lblProvince" runat="server"></asp:Label>
                                    جهت اطلاع و اقدام<br />
                                </td>
                            </tr>
                        </table>

                        <br />
                    </td>
                </tr>
            </table>

            <button id="btnprint" style="visibility: visible" onclick="printPage();">چاپ</button>

        </div>
    </form>
</body>
</html>
