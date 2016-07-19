<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/MasterPage/SmartIntergace_Master.Master" CodeBehind="WarningNotificationLogDetailSummaryReport.aspx.vb" Inherits="MehrVosul.WarningNotificationLogDetailSummaryReport" %>
<%@ Register src="../../../UserControl/Bootstrap_Panel.ascx" tagname="Bootstrap_Panel" tagprefix="uc1" %>
<%@ Register src="../../../UserControl/UC_TimePicker.ascx" tagname="UC_TimePicker" tagprefix="uc3" %>
<%@ Register src="../../../UserControl/Bootstrap_PersianDateTimePicker.ascx" tagname="Bootstrap_PersianDateTimePicker" tagprefix="uc4" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script language="javascript" type="text/javascript">


        function StartthePage() {


            return true;
        }


        function btnStatus_ClientClick(Pkey) {
            var hdnAction_Name = "<%=hdnAction.ClientID%>";
            var hdnAction = document.getElementById(hdnAction_Name);
            hdnAction.value = "E;" + Pkey;
            window.document.forms[0].submit();
            return false;
        }

        function DisplayOperation_Validate() {

            return true;
        }


        function ExcelOperation_Validate() {



            return true;
        }

      

  </script>



</asp:Content>
<asp:Content  ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">


    <uc1:Bootstrap_Panel ID="Bootstrap_Panel1" runat="server" />
<div  class="row">
    <br />
        <div class="col-md-12">
                   
                  <div class="panel panel-default">
                        <div class="panel-heading">
                           <asp:Label ID="lblInnerPageTitle" runat="server" Text=""></asp:Label>
                </div>
                        <div class="panel-body">
                            <div class="row">
                                <div class="col-md-12">
                                   
                                 
                                        <div class="form-group has-error">
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
                                            <label>نوع اطلاع رسانی</label><asp:DropDownList ID="cmbNotificationType" runat="server" 
                                               CssClass="form-control">
                                             
                                                <asp:ListItem Value="3">دعوت نامه</asp:ListItem>
                                                <asp:ListItem Value="4">اخطاریه</asp:ListItem>
                                                 <asp:ListItem Value="5">اظهارنامه</asp:ListItem>
                                               
                                                </asp:DropDownList>
                                         </div>
                                      
                          
                            
                            </div>

                             
                                </div>
                        </div>
                           
                           <div class="panel panel-default" id="divResult" runat="server" visible="false">

                             <div class="panel-heading">
                                 لیست دعوت نامه، اخطاریه و اظهارنامه
                               </div>
                        <div class="panel-body">
                            <div class="table-responsive">
                                <table class="table table-striped table-bordered table-hover" id="tblResult" runat="server">
                                    <thead>
                                        <tr>
                                            <th>#</th>
                                            <th>تاریخ</th>
                                            <th>متن ارسالی
                                                </th>
                                           <th>
                                             
                                               وضعیت</th>
                                        
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
