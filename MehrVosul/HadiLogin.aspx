<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="HadiLogin.aspx.vb" Inherits="MehrVosul.HadiLogin" %>

<!DOCTYPE html>

<html>
<head runat="server">
    <title>به نرم افزار جامع هادی بانک مهر اقتصاد خوش آمدید</title>

    <link href="/assets/css/bootstrap.css" rel="stylesheet" />
    <link href="/assets/css/font-awesome.css" rel="stylesheet" />
    <link href="/assets/css/font-awesome.min.css" rel="stylesheet" />
    <link href="/assets/js/morris/morris-0.4.3.min.css" rel="stylesheet" />
    <link href="/assets/css/custom.css" rel="stylesheet" />
    <link href="/assets/css/Site.css" rel="stylesheet" />

    <style type="text/css">
        .panel-heading {
            padding: 5px 15px;
        }

        .panel-footer {
            padding: 1px 15px;
            color: #A0A0A0;
        }

        .profile-img {
            width: 513px;
            height: 229px;
            margin: 0 auto 10px;
            display: block;
            -moz-border-radius: 50%;
            -webkit-border-radius: 50%;
            border-radius: 50%;
        }
    </style>
    <script language="javascript" type="text/javascript">
        function AccountValidate() {

            var txtUsermame = document.getElementById("<%=txtUsername.ClientID%>");
           var txtPassword = document.getElementById("<%=txtPassword.ClientID%>");
           if (txtUsermame.value == "" || txtPassword.value == "") {
               alert("نام کاربری یا رمز عبور را وارد نمایید");
               txtUsermame.focus();
               return false;
           }
           __doPostBack('btnSignIn', '');
           return false;
       }


    </script>
</head>
<body>
    <form id="form1" runat="server" role="form">
        <div class="container" style="margin-top: 40px">
            <div class="row">
                <div class="col-xs-3"></div>
                <div class="col-xs-6">
                    <div>
                        <div align="center">
                            <asp:Image ID="Image1" runat="server" Style="width: 100%"
                                ImageUrl="~/Images/System/HadiHeader2.jpg" />
                        </div>
                        <div class="panel-body">


                            <div class="row"></div>
                            <div class="row">
                                <div class="col-md-1">&nbsp;</div>


                                <div class="col-md-8"
                                    style="background-image: url('Images/System/bachgroundcolorHadi.png'); top: 0px; right: 0px; width: 100%">


                                    <div class="row">
                                        <table>

                                            <tr>
                                                <th style="width: 20px;"></th>
                                                <th>

                                                    <div class="input-group">

                                                        <label style="font-size: small">نام کاربری</label>
                                                    </div>
                                                </th>
                                                <th style="width: 20px;"></th>
                                                <th>

                                                    <div class="input-group">
                                                        <asp:TextBox ID="txtUsername" Width="200px" CssClass="form-control" runat="server" placeholder="نام کاربری" TextMode="SingleLine" MaxLength="30" BackColor="#c5e5e4"></asp:TextBox>

                                                    </div>
                                                </th>
                                            </tr>
                                        </table>
                                    </div>

                                    <div class="row" style="width: 100%;">
                                        <table>
                                            <tr style="height: 20px;"></tr>
                                            <tr>
                                                <th style="width: 20px;"></th>
                                                <th>
                                                    <div class="input-group" style="font-size: small">
                                                        <label>رمز عبور</label>
                                                    </div>
                                                </th>
                                                <th style="width: 31px"></th>
                                                <th>

                                                    <div class="input-group">
                                                        <asp:TextBox ID="txtPassword" CssClass="form-control" runat="server" placeholder="رمز عبور" TextMode="Password" MaxLength="30" Width="200px" BackColor="#c5e5e4"></asp:TextBox>
                                                    </div>

                                                </th>
                                            </tr>


                                        </table>
                                    </div>

                                    <div class="form-group">


                                        <table>

                                            <tr style="height: 20px">
                                            </tr>

                                            <tr>
                                                <th style="width: 90px;"></th>

                                                <th>
                                                    <button id="btnSignIn" title="press to log-in!" runat="server" class="btn btn-sh btn-primary" style="width: 100px; background-color: #339789; font-size: x-small; font-weight: bold;">ورود به سیستم</button>
                                                </th>
                                            </tr>
                                        </table>
                                    </div>


                                    <table>

                                        <tr>
                                            <th>
                                                <asp:CheckBox ID="CheckBox1" runat="server" Text="مرا بخاطر بسپار"
                                                    Font-Size="X-Small" />
                                            </th>

                                            <th></th>
                                        </tr>
                                    </table>
                                </div>

                                <div class="form-group" style="display: none" id="divMessage" runat="server">

                                    <div class="alert alert-danger" role="alert">
                                        <span class="glyphicon glyphicon-exclamation-sign" aria-hidden="true"></span>
                                        <span class="sr-only">Error:</span>
                                        نام کاربری یا رمز عبور نا معتبر است.
                                    </div>
                                </div>
                                <div class="form-group" style="display: none" id="divMessage1" runat="server">
                                    <div class="alert alert-danger" role="alert">
                                        <span class="glyphicon glyphicon-exclamation-sign" aria-hidden="true"></span>
                                        <span class="sr-only">Error:</span>
                                        شعبه مطابقت ندارد.
                                    </div>
                                </div>


                            </div>

                        </div>




                    </div>


                </div>
            </div>
        <div class="col-xs-4"></div>

       

    </form>
</body>
</html>
