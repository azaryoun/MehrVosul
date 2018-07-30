<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="NoticePreview.aspx.vb" Inherits="MehrVosul.NoticePreview" %>

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

     

       
        .auto-style4 {
            width: 33%;
            height: 110px;
        }
        .auto-style5 {
            height: 110px;
        }


        .auto-style6 {
            width: 33%;
            height: 32px;
        }
        .auto-style7 {
            height: 32px;
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
                                       


                                    </table>
                                </td>
                                <td style="text-align: center;padding-left:80px;" dir="rtl"><span style="font-family: 'B Nazanin'">بسمه تعالی</span><br />
                                  
                                 </td>

                                <td style="text-align: center;" dir="rtl">
                                    <asp:Image ID="Image1" runat="server"  ImageUrl="~/Images/System/MehrLogoPrint.jpg" Height="52px" />
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr>
                    <td style="text-align: center">
                        <span style="font-family: Titr; font-size: larger;">برگ اظهارنامه</span></td>
                </tr>
                <tr>
                    <td style="padding-left: 8px">

                        <table style="align-content: center; width: 99%; border-style: solid; border-width: 2px; border-color: black; padding: 0; border-spacing: 0">
                            <tr>
                                <td style="border-right: 2px solid #000000; border-bottom: 2px solid #000000; text-align: center; font-family: 'B Nazanin'; font-size: medium; font-weight: bold; " class="auto-style6" >مشخصات و اقامتگاه مخاطب</td>
                                <td style="border-bottom-style: solid; border-bottom-width: 2px; border-bottom-color: #000000; text-align: center; font-family: 'B Nazanin'; font-size: medium; font-weight: bold; border-right-style: solid; border-right-width: 2px; border-right-color: #000000;width:250px">موضوع اظهارنامه</td>
                                <td style="border-bottom-style: solid; border-bottom-width: 2px; border-bottom-color: #000000; text-align: center; font-family: 'B Nazanin'; font-size: medium; font-weight: bold;" class="auto-style7">مشخصات و اقامتگاه اظهار کننده</td>
                            </tr>
                            <tr>
                                <td style="border-right: 2px solid #000000;  font-family: 'b nazanin'; font-size: medium; text-align: right; font-weight: bold; padding-right: 2px; " dir="rtl" class="auto-style4" >مخاطب:
                                    <asp:Label ID="lblFullName0" runat="server"></asp:Label>
                                    <br />
                                    آدرس:
                                    <asp:Label ID="lblAddress2" runat="server"></asp:Label><br />
                                    کد ملی:
                                    <asp:Label ID="lblNationalID" runat="server"></asp:Label><br />
                                    همراه:
                                    <asp:Label ID="lblMobileNO" runat="server"></asp:Label></td>
                                <td style="border-right: 2px solid #000000;  font-family: 'b nazanin'; font-size: medium; padding-right: 4px; font-weight: bold; padding-top: 0px; border-right-style: solid; border-right-width: 2px; border-right-color: #000000;" dir="rtl" >شماره تسهیلات:<asp:Label ID="lblLoan" runat="server"></asp:Label><br />
                                    <asp:Label ID="lblLoanType" runat="server"></asp:Label>

                                </td>

                                <td style=" text-align: right; font-family: 'b nazanin'; font-size: medium; padding-right: 4px; font-weight: bold; padding-top: 0px;" dir="rtl" class="auto-style5">بانک مهر اقتصاد<br />
                                    به نشانی:
                                    <asp:Label ID="lblAddress" runat="server"></asp:Label>
                                    <br />
                                    شماره ثبت:<asp:Label ID="lblRegisterNO" runat="server"></asp:Label>
                                    <br />
                                    شناسه ملی:<asp:Label ID="lblCompanyNationalID" runat="server"></asp:Label>
                                    </td>

                            </tr>
                            </table>
                         <table style="align-content: center; width: 99%; border-color: black; padding: 0; border-spacing: 0; border-right-style: solid; border-bottom-style: solid; border-left-style: solid; border-right-width: 2px; border-bottom-width: 2px; border-left-width: 2px;">
                       
                            <tr>
                                <td style="border-right: 2px solid #000000; font-family: 'b nazanin'; font-size: medium; font-weight: bold; vertical-align: top;width:48%;" dir="rtl" >&nbsp;</td>
                                <td dir="rtl" style="font-family: 'b nazanin'; font-size: medium; font-weight: normal; padding-right: 2px;text-align:right;" colspan="2">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; خلاصه اظهارات<br />
                                    <br />
                                    مخاطب محترم خانم / آقای
                                    <asp:Label ID="lblFullName" runat="server"></asp:Label>
                                    &nbsp;فرزند
                                    <asp:Label ID="lblFatherName" runat="server"></asp:Label>
                                    <br />
                                    <br />
                                    احتراما به استناد ماده 156 قانون آیین دادرسی مدنی مراتب ذیل رسما به شما ابلاغ و اعلام می گردد:<br />
                                    جنابعالی برابر اسناد و مدارک موجود در این بانک بدهکار می باشید نظر به اینکه تا کنون بدهی خود را پرداخت ننموده اید از&nbsp; این طریق موجب ایراد ضرر و زیان به این بانک شده اید علیهذا جهت اثبات حسن نیت خود بدینوسیله به جنابعالی اخطار می گردد ظرف مدت 10 روز پس از رویت این اظهارنامه کلیه دیون مزبور پرداخت کلیه خسارات قانونی اعم از هزینه های&nbsp; دادرسی و حق الزحمه نمایندگی قضایی و جریمه تاخیر تادیه به عهده جنابعالی خواهد بود.<br />
                                    <br />
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; با سپاس<br />
                                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; رییس شعبه<asp:Label ID="lblBranch" runat="server"></asp:Label>
                                    <br />
                                    <br />
                                    <br />
                                    <br />
                                    <br />
                                    <br />
                                    <br />
                                    <br />
                                    <br />
                                    <br />
                                    <br />
                                    <br />
                                    <br />
                                    <br />
                                    <br />
                                    <br />
                                    <br />
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
