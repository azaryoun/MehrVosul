<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/MasterPage/SmartIntergace_Master.Master" CodeBehind="FileNotificationLog.aspx.vb" Inherits="MehrVosul.FileNotificationLog" %>
<%@ Register src="../../../UserControl/Bootstrap_Panel.ascx" tagname="Bootstrap_Panel" tagprefix="uc1" %>
<%@ Register src="../../../UserControl/UC_TimePicker.ascx" tagname="UC_TimePicker" tagprefix="uc3" %>
<%@ Register src="../../../UserControl/Bootstrap_PersianDateTimePicker.ascx" tagname="Bootstrap_PersianDateTimePicker" tagprefix="uc4" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script language="javascript" type="text/javascript">


        function StartthePage() {
            return true;
        }

        function DisplayOperation_Validate() {


            return true;
        }


        function ExcelOperation_Validate() {



            return true;
        }

        function btnStatus_ClientClick(Pkey) {
            var hdnAction_Name = "<%=hdnAction.ClientID%>";
            var hdnAction = document.getElementById(hdnAction_Name);
            hdnAction.value = "E;" + Pkey;
            window.document.forms[0].submit();
            return false;
        }


        function btnStatusSMS_ClientClick(Pkey) {
            var hdnAction_Name = "<%=hdnAction.ClientID%>";
            var hdnAction = document.getElementById(hdnAction_Name);
            hdnAction.value = "S;" + Pkey;
            window.document.forms[0].submit();
            return false;
        }


  
  </script>



</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">


    <uc1:Bootstrap_Panel ID="Bootstrap_Panel1" runat="server" />
<div class="row">

  
    <br />
        <div class="col-md-12">
                   
                  <div class="panel panel-default">
                        <div class="panel-heading" dir="rtl">
                           <asp:Label  ID="lblInnerPageTitle" runat="server"  Text=""></asp:Label>
                </div>

                      
                        <div class="panel-body">

                           <div  class="form-group has-error">
                                              <div class="panel panel-default" >
                                                <div class="panel-heading">
                                               <label>تاریخ</label>
                                                </div>
                                                <div class="panel-body" style="max-height: 200px;">

                                                    
   <uc4:Bootstrap_PersianDateTimePicker ID="Bootstrap_PersianDateTimePicker_From" 
                                                          runat="server" />

                                            
                                             <uc4:Bootstrap_PersianDateTimePicker ID="Bootstrap_PersianDateTimePicker_To" 
                                                              runat="server" />
                                                
                                            


                                            </div> 
                                                 
                                                
                                                    
                                          </div>
                                        </div>
                              <div class="form-group">
                                           
                                               <asp:DropDownList ID="cmbNotification" runat="server" CssClass="form-control" 
                                                   Visible="False">
                                                   <asp:ListItem Value="0">(همه)</asp:ListItem>
                                                   <asp:ListItem Value="1">ارسال پیامک</asp:ListItem>
                                                   <asp:ListItem Value="2">تماس تلفنی</asp:ListItem>
                                                   <asp:ListItem Value="3">دعوت نامه</asp:ListItem>
                                                   <asp:ListItem Value="4">اخطاریه</asp:ListItem>
                                                   <asp:ListItem Value="5">اظهارنامه</asp:ListItem>
                                               
                                                </asp:DropDownList>

                                        </div>

                        </div>
                           
                           <div class="panel panel-default" id="divResult" visible="false" runat="server">
                        <div class="panel-heading">
                            هزینه اطلاع رسانی
                        </div>
                        <div class="panel-body">
                            <div class="table-responsive">
                                <table class="table table-striped table-bordered table-hover" id="tblResult" runat="server">
                                    <thead>
                                        <tr>
                                            <th>#</th>
                                            <th>نوع اطلاع رسانی</th>
                                            <th>تعداد</th>
                                            <th>هزینه(ریال)</th>
                                        </tr>
                                    </thead>
                             
                                </table>
                            </div>
                        </div>
                    </div>


                       </div>     

                            
                            </div>
                        </div>
             


     
    <asp:HiddenField ID="hdnAction" runat="server" />

</asp:Content>
