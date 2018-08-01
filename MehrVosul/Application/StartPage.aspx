<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/MasterPage/SmartIntergace_Master.Master" CodeBehind="StartPage.aspx.vb" Inherits="MehrVosul.StartPage" %>

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


    <div style="justify-content: center; align-items: center;display:flex;"  id="divBranchAdminInfo" visible="false" runat="server">


        <div class="col-md-12">

            <div class="panel-body" >
                <div class="table-responsive div-center">
                    <table id="tblSummary" style="width: 50%;" runat="server" class="table table-striped table-bordered table-hover">
                        <thead>


                            <tr>
                                <th><i class="fa fa-user fa-x" title="نام کاربری"></i>
                                    <asp:Label ID="lblUserName1" runat="server" Text="" Style="color: #31708f"></asp:Label></th>
                                <th><i class="fa fa-info-circle fa-x" title="گروه دسترسی"></i>
                                    <asp:Label ID="lblUserRole" runat="server" Text="" Style="color: #31708f"></asp:Label></th>

                            </tr>
                            <tr>

                                <th><i style="color: Blue" class="fa fa-file fa-x"></i><a style="color: black" href="/Application/Administration/Notice/NoticeManagement.aspx">
                                    <asp:Label ID="lblPublicNews" runat="server" Text="" Style="color: #31708f">&nbsp;&nbsp;0</asp:Label></a>&nbsp;&nbsp;اعلان عمومی</th>
                                <th><i style="color: green" class="fa fa-file fa-x"></i><a style="color: black" href="/Application/Administration/Notice/NoticeManagement.aspx">
                                    <asp:Label ID="lblProvinceNews" runat="server" Text="" Style="color: #31708f">&nbsp;&nbsp;0</asp:Label></a>&nbsp;&nbsp;اعلان استانی</th>
                            </tr>
                            <tr>

                                <th style="color: #FF0000"><a style="color: black" href="Vosul/HandyFollow/HandyFollowAlarm.aspx">
                                  <i style="color: red" class="fa fa-bell fa-x"></i><asp:Label ID="lblHandyFollowAlarmAdmin" runat="server" Text="&amp;nbsp;&amp;nbsp;0" Style="color: #31708f"></asp:Label></a>&nbsp;&nbsp;سررسید تاریخ تعهد</th>
                                <th style="color: #FF0000"><a style="color: black" href="Vosul/HandyFollow/HandyFollowCheckAlarm.aspx">  <i style="color: red" class="fa fa-bell fa-x"></i>
                                    <asp:Label ID="lblHandyFollowCheckAlarmAdmin" runat="server" Text="&amp;nbsp;&amp;nbsp;0" Style="color: #31708f"></asp:Label></a>&nbsp;&nbsp;سررسید تاریخ چک</th>
                            </tr>
                            <tr>

                                <th><i style="color: Blue" class="fa fa-folder-open fa-x"></i><a style="color: black" href="/Application/Vosul/Cartable/HandyFollowAssign.aspx">
                                    <asp:Label ID="lblNewFileNotAssign" runat="server" Text="" Style="color: #31708f">&nbsp;&nbsp;0</asp:Label></a>&nbsp;&nbsp;پرونده های معوق جدید تخصیص نیافته</th>
                                <th><i style="color: red" class="fa fa-folder-open fa-x"></i><a style="color: black" href="/Application/Vosul/Cartable/HandyFollowAssignNotFollowManagement.aspx">
                                    <asp:Label ID="lblFileAssignNotDone" runat="server" Text="" Style="color: #31708f">&nbsp;&nbsp;0</asp:Label></a>&nbsp;&nbsp;پرونده های ارجاع شده بدون پیگیری</th>
                            </tr>
                            <tr>


                                <th><i style="color: red" class="fa fa-usd fa-x"></i><a style="color: black">
                                    <asp:Label ID="lblMassiveFile" runat="server" Text="" Style="color: #31708f">&nbsp;&nbsp;0</asp:Label></a>&nbsp;&nbsp;پرونده های معوق کلان</th>
                                <th><i style="color: green" class="fa fa-bell fa-x"></i><a style="color: black">
                                    <asp:Label ID="lblDueDateFile" runat="server" Text="" Style="color: #31708f">&nbsp;&nbsp;0</asp:Label></a>&nbsp;&nbsp;پرونده های در حال سررسید</th>
                            </tr>

                            <tr>


                                <th><i style="color: purple" class="fa fa-bell-slash fa-x"></i><a style="color: black">
                                    <asp:Label ID="lblDueDateFilePeroid" runat="server" Text="" Style="color: #31708f">&nbsp;&nbsp;0</asp:Label></a>&nbsp;&nbsp;مبلغ پرونده های سررسید گذشته</th>
                                <th><i style="color: yellow" class="fa fa-bell-slash fa-x"></i><a style="color: black">
                                    <asp:Label ID="lblDueDateFileCount" runat="server" Text="" Style="color: #31708f">&nbsp;&nbsp;0</asp:Label></a>&nbsp;&nbsp;تعداد پرونده های سررسید گذشته</th>
                            </tr>

                            <tr>


                                <th><i style="color:Highlight" class="fa fa-calendar fa-x"></i><a style="color: black">
                                    <asp:Label ID="lblDeferredPeriod" runat="server" Text="" Style="color: #31708f">&nbsp;&nbsp;0</asp:Label></a>&nbsp;&nbsp;مبلغ پرونده های معوق</th>
                                <th><i style="color:chocolate" class="fa fa-calendar fa-x"></i><a style="color: black">
                                    <asp:Label ID="lblDeferredCount" runat="server" Text="" Style="color: #31708f">&nbsp;&nbsp;0</asp:Label></a>&nbsp;&nbsp;تعداد پرونده های معوق</th>
                            </tr>

                            <tr>


                                <th><i style="color: orange" class="fa fa-bullhorn fa-x"></i><a style="color: black">
                                    <asp:Label ID="lblDoubtfulPaidPeriod" runat="server" Text="" Style="color: #31708f">&nbsp;&nbsp;0</asp:Label></a>&nbsp;&nbsp;مبلغ پرونده های مشکوک الوصول</th>
                                <th><i style="color: aqua" class="fa fa-bullhorn fa-x"></i><a style="color: black" >
                                    <asp:Label ID="lblDoubtfulPaidCount" runat="server" Text="" Style="color: #31708f">&nbsp;&nbsp;0</asp:Label></a>&nbsp;&nbsp;تعداد پرونده های مشکوک الوصول</th>
                            </tr>
                        </thead>
                    </table>
                </div>
            </div>

        </div>

    </div>

      <div style="justify-content: center; align-items: center;display:flex;"  id="divUserAdminInfo" visible="false" runat="server">


        <div class="col-md-12">

            <div class="panel-body" >
                <div class="table-responsive div-center">
                    <table id="Table1" style="width: 50%;" runat="server" class="table table-striped table-bordered table-hover">
                        <thead>


                            <tr>
                                <th><i class="fa fa-user fa-x" title="نام کاربری"></i>
                                    <asp:Label ID="lblNormalUserName" runat="server" Text="" Style="color: #31708f"></asp:Label></th>
                                <th><i class="fa fa-info-circle fa-x" title="گروه دسترسی"></i>
                                    <asp:Label ID="lblNormalUserRole" runat="server" Text="" Style="color: #31708f"></asp:Label></th>

                            </tr>
                            <tr>

                                <th><i style="color: Blue" class="fa fa-file fa-x"></i><a style="color: black" href="/Application/Administration/Notice/NoticeManagement.aspx">
                                    <asp:Label ID="lblNormalPublicNotice" runat="server" Text="" Style="color: #31708f">&nbsp;&nbsp;0</asp:Label></a>&nbsp;&nbsp;اعلان عمومی</th>
                                <th><i style="color: green" class="fa fa-file fa-x"></i><a style="color: black" href="/Application/Administration/Notice/NoticeManagement.aspx">
                                    <asp:Label ID="lblNormalProvinceNotice" runat="server" Text="" Style="color: #31708f">&nbsp;&nbsp;0</asp:Label></a>&nbsp;&nbsp;اعلان استانی</th>
                            </tr>
                            <tr>

                                <th style="color: #FF0000"><a style="color: black" href="/Application/Vosul/HandyFollow/HandyFollowAlarm.aspx">
                                  <i style="color: red" class="fa fa-bell fa-x"></i><asp:Label ID="lblHandyFollowAlarm" runat="server" Text="&amp;nbsp;&amp;nbsp;0" Style="color: #31708f"></asp:Label></a>&nbsp;&nbsp;سررسید تاریخ تعهد</th>
                                <th style="color: #FF0000"><a style="color: black" href="/Application/Vosul/HandyFollow/HandyFollowCheckAlarm.aspx">
                                  <i style="color: red" class="fa fa-bell fa-x"></i><asp:Label ID="lblHandyFollowCheckAlarm" runat="server" Text="&amp;nbsp;&amp;nbsp;0" Style="color: #31708f"></asp:Label></a>&nbsp;&nbsp; سررسید تعهد چک</th>
                            </tr>
                            <tr>

                                <th><i style="color: Blue" class="fa fa-folder-open fa-x"></i><a style="color: black" href="/Application/Vosul/Cartable/HandyFollowManagement.aspx">
                                    <asp:Label ID="lblAssignedFiles" runat="server" Text="" Style="color: #31708f">&nbsp;&nbsp;0</asp:Label></a>&nbsp;&nbsp; پرونده های تخصیص یافته </th>
                                <th><i style="color: red" class="fa fa-folder-open fa-x"></i><a style="color: black" href="/Application/Vosul/Cartable/HandyFollowManagement.aspx">
                                    <asp:Label ID="lblDefferedFiles" runat="server" Text="" Style="color: #31708f">&nbsp;&nbsp;0</asp:Label></a>&nbsp;&nbsp;پرونده های تخصیص یافته بدون انجام پیگیری</th>
                            </tr>
                         
                        </thead>
                    </table>
                </div>
            </div>

        </div>

    </div>

      <div class="alert alert-info" role="alert" id="div1" runat="server">
                <strong><span class="glyphicon glyphicon-exclamation-sign" aria-hidden="true"></span>
                    <span class="sr-only">Info:</span>

                  
         <asp:LinkButton ID="lnkbtnUserManual"  runat="server" >دانلود راهنمای کاربری</asp:LinkButton></strong>
    </div>

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

                                <th>تعداد ارسال پیامک یادآوری پرداخت قسط</th>
                                <th>
                                    <asp:Label ID="lblLastDayPreSMS" runat="server" Text="---" Style="color: #31708f"></asp:Label></th>
                                <th>
                                    <asp:Label ID="lblTodayPreSMS" runat="server" Text="---" Style="color: #31708f"></asp:Label></th>
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
    <div class="row">

        <div class="col-md-6" runat="server" id="divBranchAdmin1">

            <div class="panel back-dash" style="background-color: #DB0630">
                <a style="color: White" href="/Application/Vosul/Notice/ManifestHandyFollowAssign.aspx"><i class="fa fa-users fa-4x"></i><strong style="display: -ms-inline-grid">&nbsp;تخصیص اظهارنامه</strong></a>
                <p class="text-muted">در این صفحه مدیر شعبه امکان تخصیص پرونده های شامل اظهارنامه جاری را به کارشناسان شعبه را دارد</p>



                <div class="panel-body" style="max-height: 200px; overflow-y: scroll;">
                    <div class="form-group" runat="server" id="divchklstAssignFiles">
                    </div>
                </div>
                <br />
            </div>
        </div>

        <div class="col-md-6" runat="server" id="divBranchAdmin">

            <div class="panel back-dash" style="background-color: #fa8144">
                <a style="color: White" href="/Application/Vosul/Notice/NoticeHandyFollowAssign.aspx"><i class="fa fa-users fa-4x"></i><strong style="display: -ms-inline-grid">&nbsp;تخصیص اخطاریه</strong></a>
                <p class="text-muted">در این صفحه مدیر شعبه امکان تخصیص پرونده های شامل اخطاریه جاری را به کارشناسان شعبه را دارد</p>

                <div class="panel-body" style="max-height: 200px; overflow-y: scroll;">
                    <div class="form-group" runat="server" id="divchklstAssignFiles1">
                    </div>
                </div>
                <br />
            </div>
        </div>





    </div>


    <div class="row">


        <div runat="server" id="divBranchAdmin3" class="col-md-6" style="visibility:hidden;" >
            <div class="panel back-dash" style="background-color: #00CE6F">
                <a style="color: White" href="/Application/Vosul/Reports/WarningNotificationReport.aspx"><i class="fa fa-bar-chart fa-4x"></i><strong style="display: -ms-inline-grid">&nbsp; گزارش گیری </strong></a>
                <p class="text-muted">
                    ارائه انواع گزارش های&nbsp; مفید از عملکرد سیستم مانند Log 
                                 اطلاع رسانی های انجام شده و درخواست های شعبات
                </p>
            </div>




        </div>

        <div runat="server" id="divBranchAdmin4" style="visibility:hidden;" class="col-md-6">

            <div class="panel back-dash">

                <a style="color: White" href="/Application/Vosul/Cartable/HandyFollowAssign.aspx"><i class="fa fa-tasks  fa-4x"></i><strong style="display: -ms-inline-grid">&nbsp;تخصیص پرونده معوق</strong></a>
                <p class="text-muted">در این صفحه مدیر شعبه امکان تخصیص پرونده های معوق جاری را به کارشناسان شعبه را دارد</p>
            </div>




        </div>
    </div>



    <div class="row">
        <br />
        <div class="col-md-6" runat="server" style="visibility:hidden;" id="divBranchUser">

            <div class="panel back-dash" style="background-color: #ffd800">
                <a style="color: White" href="/Application/Vosul/HandyFollow/HandyFollowFileSearch.aspx"><i class="fa fa-users fa-4x"></i><strong style="display: -ms-inline-grid">&nbsp;پیگیری پرسنلی</strong></a>
                <p class="text-muted">امکان ثبت پیگیریهای دستی انجام شده توسط  کارشناسان از طریق جستجوی شماره پرونده</p>
                <br />
            </div>




        </div>

        <div class="col-md-6" runat="server" style="visibility:hidden;" id="divBranchUser1">

            <div class="panel back-dash" style="background-color: #00ffff">
                <a style="color: White" href="/Application/Vosul/Cartable/HandyFollowManagement.aspx"><i class="fa fa-users fa-4x"></i><strong style="display: -ms-inline-grid">&nbsp;پیگیریهای تخصیص یافته</strong></a>
                <p class="text-muted">امکان مشاهده لیست پیگیریهای تخصیص یافته</p>
                <br />
            </div>




        </div>
        <div style="visibility:hidden;" class="col-md-6">


            <div runat="server" id="divadmin" class="panel back-dash" style="background-color: purple">
                <a style="color: White" href="/Application/Vosul/WarningIntervals/WarningIntervalsManagement.aspx"><i class="fa fa-tasks  fa-4x"></i><strong style="display: -ms-inline-grid">&nbsp;گردش کاری پویا</strong></a>
                <p class="text-muted">تعریف و تعیین نحوه اطلاع رسانی و رفتار سیستم در قبال پرونده های مختلف و انواع وام ها به صورت پویا </p>

            </div>
        </div>


    </div>

    <%--<div class="row">
    <br />
        <div class="col-md-6">
                   
                  <div class="panel back-dash"  style="background-color:#999">
                               <i class="fa fa-dashboard fa-4x"></i><strong> &nbsp; Dashboard</strong>
                             <p class="text-muted">Lorem ipsum dolor sit amet, consectetur adipiscing sit ametsit amet elit ftr. Lorem ipsum dolor sit amet, consectetur adipiscing elit. </p>
                        </div>
               
                
                   

            </div>

               <div class="col-md-6">
                   
                  <div class="panel back-dash" style="background-color:#8702A8">
                               <i class="fa fa-database fa-4x"></i><strong> &nbsp; Data Warehouse</strong>
                             <p class="text-muted">Lorem ipsum dolor sit amet, consectetur adipiscing sit ametsit amet elit ftr. Lorem ipsum dolor sit amet, consectetur adipiscing elit. </p>
                        </div>
               
                
                   

            </div>
  </div>--%>
</asp:Content>
