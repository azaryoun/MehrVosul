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
        .style1
        {
            width: 187px;
        }
        .style2
        {
            width: 293px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
            <div class="row">
    <br />
      
          <div class="col-md-12">
                
               
               <div class="alert alert-danger" role="alert" id="divLogError" runat="server">
                              <strong>    <span class="glyphicon glyphicon-exclamation-sign" aria-hidden="true"></span>
                                    <span class="sr-only">Error:</span>
                                    
              <asp:Label ID="lblLogError" runat="server" Text="Label"></asp:Label></strong>  
                                    </div>
                

                      
               <div class="alert alert-info" role="alert"  id="divLogSuccess" runat="server">
                                     <strong>    <span class="glyphicon glyphicon-exclamation-sign" aria-hidden="true"></span>
                                    <span class="sr-only">Info:</span>
                                  
                 <a href="/Application/Vosul/Reports/LogCurrentLCStatusReport.aspx" style="color:#31708f">  <asp:Label ID="lblLogSuccess" runat="server" Text="Label"></asp:Label></a></strong> 
                                    </div>
                   
                  <div class="alert alert-danger" role="alert" id="divSMSError" runat="server">
                              <strong>    <span class="glyphicon glyphicon-exclamation-sign" aria-hidden="true"></span>
                                    <span class="sr-only">Error:</span>
                                    
              <asp:Label ID="lblSMSError" runat="server" Text="Label"></asp:Label></strong>  
                                    </div>

              <div class="alert alert-info" role="alert"  id="divSMSSuccess" runat="server">
                                     <strong><span class="glyphicon glyphicon-exclamation-sign" aria-hidden="true"></span>
                                    <span class="sr-only">Info:</span>
                                  
                <asp:Label ID="lblSMSSuccess" runat="server" Text="Label"></asp:Label></strong> 
                                    </div>
                   
                 <div class="alert alert-info" role="alert"  id="divItemAdmin" visible="false" runat="server">
                                     <strong><span class="glyphicon glyphicon-exclamation-sign" aria-hidden="true"></span>
                                    <span class="sr-only">Info:</span>
                                  
                <asp:Label ID="lblItemAdmin" runat="server" Text=""></asp:Label></strong> 
                                    </div>


            </div>

              <div class="col-md-12" >
                 <div class="panel-body">
                 <div class="table-responsive">
                <table id ="tblLogDetaile" runat ="server" class="table table-striped table-bordered table-hover">
                                    <thead>
                                       <tr>
                                          <th></th>
                                          <th >روزگذشته</th>
                                          <th>امروز(تا این لحظه)</th>
                                       </tr>
                                        <tr>
                                            
                                            <th>وضعیت</th>
                                            <th><asp:Label ID="lblLastDayStatus" runat="server" Text="" style="color:#31708f"></asp:Label></th>
                                            <th><asp:Label ID="lblTodayStatus" runat="server" Text="" style="color:#31708f"></asp:Label></th>
                                        </tr>
                                        <tr>
                                            
                                            <th>ساعت خواندن از BI</th>
                                            <th><asp:Label ID="lblLastDayBI" runat="server" Text="---" style="color:#31708f"></asp:Label></th>
                                             <th><asp:Label ID="lblTodayBI" runat="server" Text="---" style="color:#31708f"></asp:Label></th>
                                        </tr>
                                        <tr>
                                            
                                            <th>ساعت اولین ارسال</th>
                                            <th><asp:Label ID="lblLastDayFirstSent" runat="server" Text="---" style="color:#31708f"></asp:Label></th>
                                             <th><asp:Label ID="lblTodayFirstSent" runat="server" Text="---" style="color:#31708f"></asp:Label></th>
                                        </tr>
                                        <tr>
                                            
                                            <th>ساعت آخرین ارسال</th>
                                            <th><asp:Label ID="lblLastDayLastSent" runat="server" Text="---" style="color:#31708f"></asp:Label></th>
                                             <th><asp:Label ID="lblTodayLastSent" runat="server" Text="---" style="color:#31708f"></asp:Label></th>
                                        </tr>
                                        <tr>
                                            
                                            <th>تعداد ارسال پیامک</th>
                                              <th><asp:Label ID="lblLastDaySMSCount" runat="server" Text="---" style="color:#31708f"></asp:Label></th>
                                             <th><asp:Label ID="lblTodaySMSCount" runat="server" Text="---" style="color:#31708f"></asp:Label></th>
                                        </tr>
                                        <tr>
                                            
                                            <th>تعداد ارسال پیامک صوتی</th>
                                             <th><asp:Label ID="lblLastDaySMSVoice" runat="server" Text="---" style="color:#31708f"></asp:Label></th>
                                             <th><asp:Label ID="lblTodaySMSVoice" runat="server" Text="---" style="color:#31708f"></asp:Label></th>
                                        </tr>
                                           <tr>
                                            
                                            <th>تعداد ارسال پیامک یادآوری پرداخت قسط</th>
                                             <th><asp:Label ID="lblLastDayPreSMS" runat="server" Text="---" style="color:#31708f"></asp:Label></th>
                                             <th><asp:Label ID="lblTodayPreSMS" runat="server" Text="---" style="color:#31708f"></asp:Label></th>
                                        </tr>
                                         <tr>
                                            
                                            <th>تعداد BI</th>
                                              <th><asp:Label ID="lblBILastDayCount" runat="server" Text="---" style="color:#31708f"></asp:Label></th>
                                             <th><asp:Label ID="lblBITodayCount" runat="server" Text="---" style="color:#31708f"></asp:Label></th>
                                        </tr>
                                        <tr>
                                            
                                            <th>مدت ارسال</th>
                                              <th><asp:Label ID="lblLastDaySendTime" runat="server" Text="---" style="color:#31708f"></asp:Label></th>
                                             <th><asp:Label ID="lblTodaySendTime" runat="server" Text="---" style="color:#31708f"></asp:Label></th>
                                        </tr>
                                    </thead>
                             
                                </table></div> </div>
              </div> 
                
                     <div class="col-md-6">
                   
                  <div class="panel back-dash" style="background-color:#DB0630">
                                <a style="color:White" href="/Application/Vosul/HandyFollow/HandyFollowFileSearch.aspx"><i class="fa fa-users fa-4x"></i><strong style="display:-ms-inline-grid"> &nbsp;پیگیری پرسنلی</strong></a> 
                             <p class="text-muted">امکان ثبت پیگیریهای دستی انجام شده توسط  کارشناسان از طریق جستجوی شماره پرونده</p>
                          <br />
                        </div>
               
                
                   

            </div>
               
                
                   

               <div class="col-md-6">
                   
                  <div class="panel back-dash">
                               
                       <a style="color:White" href="/Application/Vosul/Cartable/HandyFollowAssign.aspx"><i class="fa fa-tasks  fa-4x"></i><strong  style="display:-ms-inline-grid"> &nbsp;تخصیص پرونده معوق</strong></a>
                             <p class="text-muted">در این صفحه مدیر شعبه امکان تخصیص پرونده های معوق جاری را به کارشناسان شعبه را دارد</p>
                        </div>
               
                
                   

            </div>
  </div>

          

     <div class="row">
    <br />
        <div class="col-md-6">
                   

                    <div class="panel back-dash"  style="background-color:#fa8144">
                          <a style="color:White" href="/Application/Vosul/WarningIntervals/WarningIntervalsManagement.aspx"><i class="fa fa-tasks  fa-4x"></i><strong  style="display:-ms-inline-grid"> &nbsp;گردش کاری پویا</strong></a>
                            <p class="text-muted">تعریف و تعیین نحوه اطلاع رسانی و رفتار سیستم در قبال پرونده های مختلف و انواع وام ها به صورت پویا </p>
                    
                    </div>
               </div>
               <div class="col-md-6">
                  <div class="panel back-dash" style="background-color:#00CE6F">
                             <a style="color:White" href="/Application/Vosul/Reports/WarningNotificationReport.aspx"><i class="fa fa-bar-chart fa-4x"></i><strong style="display:-ms-inline-grid"> &nbsp; گزارش گیری </strong></a>
                             <p class="text-muted">ارائه انواع گزارش های&nbsp; مفید از عملکرد سیستم مانند Log 
                                 اطلاع رسانی های انجام شده و درخواست های شعبات</p>
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
