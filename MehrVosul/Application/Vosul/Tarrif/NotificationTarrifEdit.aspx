<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/MasterPage/SmartIntergace_Master.Master" CodeBehind="NotificationTarrifEdit.aspx.vb" Inherits="MehrVosul.NotificationTarrifEdit" %>
<%@ Register src="../../../UserControl/Bootstrap_Panel.ascx" tagname="Bootstrap_Panel" tagprefix="uc1" %>
<%@ Register src="../../../UserControl/UC_TimePicker.ascx" tagname="UC_TimePicker" tagprefix="uc3" %>
<%@ Register src="../../../UserControl/Bootstrap_PersianDateTimePicker.ascx" tagname="Bootstrap_PersianDateTimePicker" tagprefix="uc4" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script language="javascript" type="text/javascript">


        function StartthePage() {


            return true;
        }


        function SaveOperation_Validate() {


            var cmbNotification = document.getElementById("<%=cmbNotification.ClientID%>");
            var txtAmount = document.getElementById("<%=txtAmount.ClientID%>");


            if (cmbNotification.options(cmbNotification.selectedIndex).value == -1) {
                alert("نوع اطلاع رسانی را مشخص نمایید", "خطا");
                cmbFrequencyInDay.focus();
                return false;
            }


            if (trimall(txtAmount.value) == "") {
                alert("مبلغ را وارد نمایید");
                txtAmount.focus();
                return false;
            }


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
                                            <label>نوع اطلاع رسانی</label>
                                               <asp:DropDownList ID="cmbNotification" runat="server" CssClass="form-control">
                                                   <asp:ListItem Value="0">(همه)</asp:ListItem>
                                                   <asp:ListItem Value="1">ارسال پیامک</asp:ListItem>
                                                   <asp:ListItem Value="2">تماس تلفنی</asp:ListItem>
                                                   <asp:ListItem Value="3">دعوت نامه</asp:ListItem>
                                                   <asp:ListItem Value="4">اخطاریه</asp:ListItem>
                                                   <asp:ListItem Value="5">اظهارنامه</asp:ListItem>
                                               
                                                </asp:DropDownList>

                                        </div>

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
                          
                                          <div class="form-group has-error">
                                            <label>مبلغ(ریال)
                                            
                                              </label>
                                              
                                            <asp:TextBox ID="txtAmount" runat="server" cssclass="form-control" MaxLength="50" 
                                                  placeholder="مبلغ را وارد کنید"></asp:TextBox>
                                            
                                         </div>
                                      
                          
                            
                            </div>

                     
                            </div>
                        </div>
                           
                       

                       </div>     

                            
                            </div>
                        </div>
             


     
    <asp:HiddenField ID="hdnAction" runat="server" />

</asp:Content>
