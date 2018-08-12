<%@ Page Language="vb" AutoEventWireup="false" MasterPageFile="~/MasterPage/SmartIntergace_Master.Master" CodeBehind="HadiStartPage.aspx.vb" Inherits="MehrVosul.HadiStartPage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script language="javascript" type="text/javascript">


        function StartthePage() {
            return true;
        }
        function ReadData() {

            return false;
        }

    </script>
    <style type="text/css">
        .div-center {
            display: flex;
            align-items: center;
            justify-content: center;
        }

    </style> 
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
     <div class="row">
      
        <div class="col-md-12">


            <div class="alert alert-danger" role="alert" id="divLogError" runat="server">
                <strong><span class="glyphicon glyphicon-exclamation-sign" aria-hidden="true"></span>
                    <span class="sr-only">Error:</span>

                    <asp:Label ID="lblLogError" runat="server" Text="Label"></asp:Label></strong>
            </div>



            <div class="alert alert-info" role="alert" id="divLogSuccess" runat="server">
                <strong><span class="glyphicon glyphicon-exclamation-sign" aria-hidden="true"></span>
                    <span class="sr-only">Info:</span>

                    <a href="/Application/Vosul/Reports/LogCurrentLCStatusReport.aspx" style="color: #31708f">
                        <asp:Label ID="lblLogSuccess" runat="server" Text="Label"></asp:Label></a></strong>
            </div>

            <div class="alert alert-danger" role="alert" id="divSMSError" runat="server">
                <strong><span class="glyphicon glyphicon-exclamation-sign" aria-hidden="true"></span>
                    <span class="sr-only">Error:</span>

                    <asp:Label ID="lblSMSError" runat="server" Text="Label"></asp:Label></strong>
            </div>

            <div class="alert alert-info" role="alert" id="divSMSSuccess" runat="server">
                <strong><span class="glyphicon glyphicon-exclamation-sign" aria-hidden="true"></span>
                    <span class="sr-only">Info:</span>

                    <asp:Label ID="lblSMSSuccess" runat="server" Text="Label"></asp:Label></strong>
            </div>

            <div class="alert alert-info" role="alert" id="divItemAdmin" visible="false" runat="server">
                <strong><span class="glyphicon glyphicon-exclamation-sign" aria-hidden="true"></span>
                    <span class="sr-only">Info:</span>

                    <asp:Label ID="lblItemAdmin" runat="server" Text=""></asp:Label></strong>
            </div>


        </div>

        <div class="col-md-12">
            <div class="panel-body">
                <div class="table-responsive">
                    <table id="tblLogDetaile" runat="server" class="table table-striped table-bordered table-hover">
                        <thead>
                            <tr>
                                <th></th>
                                <th>روزگذشته</th>
                                <th>امروز(تا این لحظه)</th>
                            </tr>
                            <tr>

                                <th>وضعیت</th>
                                <th>
                                    <asp:Label ID="lblLastDayStatus" runat="server" Text="" Style="color: #31708f"></asp:Label></th>
                                <th>
                                    <asp:Label ID="lblTodayStatus" runat="server" Text="" Style="color: #31708f"></asp:Label></th>
                            </tr>
                            <tr>

                                <th>ساعت خواندن از BI</th>
                                <th>
                                    <asp:Label ID="lblLastDayBI" runat="server" Text="---" Style="color: #31708f"></asp:Label></th>
                                <th>
                                    <asp:Label ID="lblTodayBI" runat="server" Text="---" Style="color: #31708f"></asp:Label></th>
                            </tr>
                            <tr>

                                <th>ساعت اولین ارسال</th>
                                <th>
                                    <asp:Label ID="lblLastDayFirstSent" runat="server" Text="---" Style="color: #31708f"></asp:Label></th>
                                <th>
                                    <asp:Label ID="lblTodayFirstSent" runat="server" Text="---" Style="color: #31708f"></asp:Label></th>
                            </tr>
                            <tr>

                                <th>ساعت آخرین ارسال</th>
                                <th>
                                    <asp:Label ID="lblLastDayLastSent" runat="server" Text="---" Style="color: #31708f"></asp:Label></th>
                                <th>
                                    <asp:Label ID="lblTodayLastSent" runat="server" Text="---" Style="color: #31708f"></asp:Label></th>
                            </tr>
                            <tr>

                                <th>تعداد ارسال پیامک</th>
                                <th>
                                    <asp:Label ID="lblLastDaySMSCount" runat="server" Text="---" Style="color: #31708f"></asp:Label></th>
                                <th>
                                    <asp:Label ID="lblTodaySMSCount" runat="server" Text="---" Style="color: #31708f"></asp:Label></th>
                            </tr>
                            <tr>

                                <th>تعداد ارسال پیامک صوتی</th>
                                <th>
                                    <asp:Label ID="lblLastDaySMSVoice" runat="server" Text="---" Style="color: #31708f"></asp:Label></th>
                                <th>
                                    <asp:Label ID="lblTodaySMSVoice" runat="server" Text="---" Style="color: #31708f"></asp:Label></th>
                            </tr>
                          
                            <tr>

                                <th>تعداد BI</th>
                                <th>
                                    <asp:Label ID="lblBILastDayCount" runat="server" Text="---" Style="color: #31708f"></asp:Label></th>
                                <th>
                                    <asp:Label ID="lblBITodayCount" runat="server" Text="---" Style="color: #31708f"></asp:Label></th>
                            </tr>
                            <tr>

                                <th>مدت ارسال</th>
                                <th>
                                    <asp:Label ID="lblLastDaySendTime" runat="server" Text="---" Style="color: #31708f"></asp:Label></th>
                                <th>
                                    <asp:Label ID="lblTodaySendTime" runat="server" Text="---" Style="color: #31708f"></asp:Label></th>
                            </tr>
                        </thead>

                    </table>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
