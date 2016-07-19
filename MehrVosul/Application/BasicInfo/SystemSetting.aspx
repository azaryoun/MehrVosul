<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/MasterPage/SmartIntergace_Master.Master" CodeBehind="SystemSetting.aspx.vb" Inherits="MehrVosul.SystemSetting" %>
<%@ Register src="../../UserControl/Bootstrap_Panel.ascx" tagname="Bootstrap_Panel" tagprefix="uc1" %>


<%@ Register src="../../UserControl/UC_TimePicker.ascx" tagname="UC_TimePicker" tagprefix="uc2" %>


<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script language="javascript" type="text/javascript">


        function StartthePage() {
            return true;
        }

        function SaveOperation_Validate() {

            var txtGatewayNumber = document.getElementById("<%=txtGatewayNumber.ClientID%>");
            var txtCompanyGateway = document.getElementById("<%=txtCompanyGateway.ClientID%>");
            var txtGatewayUserName = document.getElementById("<%=txtGatewayUserName.ClientID%>");
            var txtGatewayPassword = document.getElementById("<%=txtGatewayPassword.ClientID%>");
            var txtGatewayIP = document.getElementById("<%=txtGatewayIP.ClientID%>");
            var txtEmail = document.getElementById("<%=txtEmail.ClientID%>");
            var txtEmailUserName = document.getElementById("<%=txtEmailUserName.ClientID%>");
            var txtEmailPassword = document.getElementById("<%=txtEmailPassword.ClientID%>");
            var txtEmailHost = document.getElementById("<%=txtEmailHost.ClientID%>");
            var cmbtryTime = document.getElementById("<%=cmbtryTime.ClientID%>");
            var cmbtryIntervalHour = document.getElementById("<%=cmbtryIntervalHour.ClientID%>");

            if (trimall(txtGatewayNumber.value) == "") {
                alert("شماره درگاه را وارد نمایید");
                txtGatewayNumber.focus();
                return false;
            }


            if (trimall(txtCompanyGateway.value) == "") {
                alert("شرکت در درگاه را وارد نمایید");
                txtCompanyGateway.focus();
                return false;
            }

            if (trimall(txtGatewayUserName.value) == "") {
                alert("نام کاربری درگاه را وارد نمایید");
                txtGatewayUserName.focus();
                return false;
            }

            if (trimall(txtGatewayPassword.value) == "") {
                alert("کلمه عبور درگاه را وارد نمایید");
                txtGatewayPassword.focus();
                return false;
            }


            if (trimall(txtGatewayIP.value) == "") {
                alert("IP درگاه را وارد نمایید");
                txtGatewayIP.focus();
                return false;
            }


            if (trimall(txtEmail.value) == "") {
                alert("پست الکترونیکی را وارد نمایید");
                txtEmail.focus();
                return false;
            }

            if (trimall(txtEmailUserName.value) == "") {
                alert("نام کاربری پست الکترونیکی را وارد نمایید");
                txtEmailUserName.focus();
                return false;
            }


            if (trimall(txtEmailPassword.value) == "") {
                alert("کلمه عبور پست الکترونیکی را وارد نمایید");
                txtEmailPassword.focus();
                return false;
            }


            if (trimall(txtEmailHost.value) == "") {
                alert("هاست پست الکترونیکی را وارد نمایید");
                txtEmailHost.focus();
                return false;
            }

            if (cmbtryTime.options(cmbtryTime.selectedIndex).value == -1) {
                alert("تعداد تکرار را مشخص نمایید", "خطا");
                cmbtryTime.focus();
                return false;
            }

            if (cmbtryIntervalHour.options(cmbtryIntervalHour.selectedIndex).value == -1) {
                alert("فاصله زمانی تکرار را مشخص نمائید", "خطا");
                cmbtryIntervalHour.focus();
                return false;
            }

           
            return true;
        }



      
  </script>



</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">


    <uc1:Bootstrap_Panel ID="Bootstrap_Panel1" runat="server" />
<div class="row">
    <br />
        <div class="col-md-12">
                   
                  <div class="panel panel-default">
                        <div class="panel-heading">
                           <asp:Label ID="lblInnerPageTitle" runat="server" Text=""></asp:Label>
                </div>
                        <div class="panel-body">
                            <div class="row">
                                <div class="col-md-6">
                                   
                                 
                                        <div class="form-group has-error">
                                            <label>شماره پیامک درگاه</label>
                                          
                                            <asp:TextBox ID="txtGatewayNumber" runat="server" cssclass="form-control" MaxLength="50" placeholder="شماره پیامک درگاه را وارد کنید"></asp:TextBox>
                                            
                                        </div>

                                        <div class="form-group has-error">
                                            <label>شرکت درگاه</label>
                                          
                                            <asp:TextBox ID="txtCompanyGateway" runat="server" cssclass="form-control" MaxLength="50" placeholder="شرکت درگاه را وارد کنید"></asp:TextBox>
                                            
                                        </div>

                                         <div class="form-group has-error">
                                            <label>نام کاربری درگاه</label>
                                          
                                            <asp:TextBox ID="txtGatewayUserName" runat="server" cssclass="form-control" MaxLength="50" placeholder="نام کاربری درگاه را وارد کنید"></asp:TextBox>
                                            
                                        </div>

                                         <div class="form-group has-error">
                                            <label>رمز عبور درگاه</label>
                                          
                                            <asp:TextBox ID="txtGatewayPassword" runat="server" cssclass="form-control" MaxLength="50" placeholder="رمز عبور درگاه را وارد کنید"></asp:TextBox>
                                            
                                        </div>

                                          <div class="form-group has-error">
                                            <label>IP درگاه</label>
                                          
                                            <asp:TextBox ID="txtGatewayIP" runat="server" cssclass="form-control" MaxLength="50" placeholder="IP درگاه را وارد کنید"></asp:TextBox>
                                            
                                        </div>
                          
                                     
                                          <div class="form-group has-error">
                                            <label>تلفن سامانه هوشمند</label>
                                          
                                            <asp:TextBox ID="txtTellNO" runat="server" cssclass="form-control" MaxLength="50" placeholder="تلفن سامانه هوشمند را وارد کنید"></asp:TextBox>
                                            
                                        </div>

                                         <div class="form-group has-error">
                                            <label>پست الکترونیکی</label>
                                          
                                            <asp:TextBox ID="txtEmail" runat="server" cssclass="form-control" MaxLength="50" placeholder="پست الکترونیکی را وارد کنید"></asp:TextBox>
                                            
                                        </div>


                                            
                                     <div class="form-group has-error">
                                            <label>نام کاربری پست الکترونیکی</label>
                                          
                                            <asp:TextBox ID="txtEmailUserName" runat="server" cssclass="form-control" MaxLength="50" placeholder="نام کاربری پست الکترونیکی را وارد کنید"></asp:TextBox>
                                            
                                        </div>

                                          <div class="form-group has-error">
                                            <label>رمز عبور پست الکترونیکی</label>
                                          
                                            <asp:TextBox ID="txtEmailPassword" runat="server" cssclass="form-control" MaxLength="50" placeholder="رمز عبور پست الکترونیکی را وارد کنید"></asp:TextBox>
                                            
                                        </div>

                                          <div class="form-group has-error">
                                            <label>هاست پست الکترونیکی</label>
                                          
                                            <asp:TextBox ID="txtEmailHost" runat="server" cssclass="form-control" MaxLength="50" placeholder="هاست پست الکترونیکی را وارد کنید"></asp:TextBox>
                                            
                                        </div>

                                              <div class="panel panel-default" >
                                                <div class="panel-heading">
                                               <label>جزئیات زمانبندی وصال</label>
                                                </div>
                                                <div class="panel-body" style="max-height: 300px;">

                                                   <div class="form-group">
                                            <label>ساعت بروزرسانی
                                            </label>
                                          
                                            <uc2:UC_TimePicker ID="StartTimePicker" runat="server" />
                                         
                                           </div>  


                                             <div class="form-group has-error">
                             
                                            <label>تعداد تکرار</label>
                                                     <asp:DropDownList ID="cmbtryTime" runat="server" 
                                               CssClass="form-control">
                                                <asp:ListItem Value="-1">---</asp:ListItem>
                                                <asp:ListItem Value="1">1</asp:ListItem>
                                                <asp:ListItem Value="2">2</asp:ListItem>
                                                <asp:ListItem Value="3">3</asp:ListItem>
                                                <asp:ListItem Value="4">4</asp:ListItem>
                                                <asp:ListItem Value="5">5</asp:ListItem>
                                               
                                                </asp:DropDownList>
                                                                            

                                        </div>

                                        
                                             <div class="form-group has-error">
                             
                                            <label>فاصله زمانی تکرار(ساعت)</label>
                                                     <asp:DropDownList ID="cmbtryIntervalHour" runat="server" 
                                               CssClass="form-control">
                                                <asp:ListItem Value="-1">---</asp:ListItem>
                                                <asp:ListItem Value="1">1</asp:ListItem>
                                                <asp:ListItem Value="2">2</asp:ListItem>
                                                <asp:ListItem Value="3">3</asp:ListItem>
                                              
                                               
                                                </asp:DropDownList>
                                                                            

                                        </div>
                                         


                                            </div> 
                                                 
                                                
                                                    
                                        </div> 
         
                           
                       </div>    
                       


                       <div class="col-md-6">

  
                              <div class="panel panel-default" >
                                                <div class="panel-heading">
                                               <label>جزئیات زمانبندی تجهیز اعتبار</label>
                                                </div>
                                                <div class="panel-body" style="max-height: 300px;">

                                                   <div class="form-group">
                                            <label>ساعت بروزرسانی
                                            </label>
                                          
                                            <uc2:UC_TimePicker ID="DepositStartTimePicker" runat="server" />
                                         
                                           </div>  


                                             <div class="form-group has-error">
                             
                                            <label>تعداد تکرار</label>
                                                     <asp:DropDownList ID="cmbDepositTryTime" runat="server" 
                                               CssClass="form-control">
                                                <asp:ListItem Value="-1">---</asp:ListItem>
                                                <asp:ListItem Value="1">1</asp:ListItem>
                                                <asp:ListItem Value="2">2</asp:ListItem>
                                                <asp:ListItem Value="3">3</asp:ListItem>
                                                <asp:ListItem Value="4">4</asp:ListItem>
                                                <asp:ListItem Value="5">5</asp:ListItem>
                                               
                                                </asp:DropDownList>
                                                                            

                                        </div>

                                        
                                             <div class="form-group has-error">
                             
                                            <label>فاصله زمانی تکرار(ساعت)</label>
                                                     <asp:DropDownList ID="cmbDepositTryIntervalHour" runat="server" 
                                               CssClass="form-control">
                                                <asp:ListItem Value="-1">---</asp:ListItem>
                                                <asp:ListItem Value="1">1</asp:ListItem>
                                                <asp:ListItem Value="2">2</asp:ListItem>
                                                <asp:ListItem Value="3">3</asp:ListItem>
                                              
                                               
                                                </asp:DropDownList>
                                                                            

                                        </div>
                                         


                                            </div> 
                                                 
                                                
                                                    
                                        </div>                
                                         

                                 <div class="panel panel-default" >
                                                <div class="panel-heading">
                                               <label>جزئیات زمانبندی تخصیص اعتبار</label>
                                                </div>
                                                <div class="panel-body" style="max-height: 300px;">

                                                   <div class="form-group">
                                            <label>ساعت بروزرسانی
                                            </label>
                                          
                                            <uc2:UC_TimePicker ID="LoanStartTimePicker" runat="server" />
                                         
                                           </div>  


                                             <div class="form-group has-error">
                             
                                            <label>تعداد تکرار</label>
                                  <asp:DropDownList ID="cmbLoanTryTime" runat="server" 
                                               CssClass="form-control">
                                                <asp:ListItem Value="-1">---</asp:ListItem>
                                                <asp:ListItem Value="1">1</asp:ListItem>
                                                <asp:ListItem Value="2">2</asp:ListItem>
                                                <asp:ListItem Value="3">3</asp:ListItem>
                                                <asp:ListItem Value="4">4</asp:ListItem>
                                                <asp:ListItem Value="5">5</asp:ListItem>
                                               
                                                </asp:DropDownList>
                                                                            

                                        </div>

                                        
                                             <div class="form-group has-error">
                             
                                            <label>فاصله زمانی تکرار(ساعت)</label>
                                                     <asp:DropDownList ID="cmbLoanTryIntervalHour" runat="server" 
                                               CssClass="form-control">
                                                <asp:ListItem Value="-1">---</asp:ListItem>
                                                <asp:ListItem Value="1">1</asp:ListItem>
                                                <asp:ListItem Value="2">2</asp:ListItem>
                                                <asp:ListItem Value="3">3</asp:ListItem>
                                              
                                               
                                                </asp:DropDownList>

                                        </div>
                                         
                                            </div> 
                                                 
                                     
                                        </div>      
                           
                           
                                <div class="panel panel-default" >
                                                <div class="panel-heading">
                                               <label>تنظیمات پیامک صوتی</label>
                                                </div>
                                                <div class="panel-body" style="max-height: 300px;">

                                               


                                             <div class="form-group has-error">
                             
                                            <label>VoiceSMSUID</label>
                                   <asp:TextBox ID="txtVoiceSMSUID" runat="server" cssclass="form-control" MaxLength="50" placeholder="VoiceSMSUID را وارد کنید"></asp:TextBox>
                                            
                                                                            
                                        </div>
                                      
                                             <div class="form-group has-error">
                             
                                            <label>VoiceSMSToken</label>
                                                 <asp:TextBox ID="txtVoiceSMSToken" runat="server" cssclass="form-control" MaxLength="50" placeholder="VoiceSMSToken را وارد کنید"></asp:TextBox>
                                       

                                        </div>
                                         
                                            </div> 
                                                             
                                                    
                                        </div>    
                           
                           
                              <div class="panel panel-default" > 
                                           <div class="panel-heading">
                                            <label>وضعیت سرویسها</label>
                                              </div>
                                             
                                         <div class="panel-body" style="max-height: 200px;overflow-y: scroll;">

                                                  <label> <input type="checkbox" runat="server"  id="chkHadiStatus"/> سرویس هادی</label> 
                                                   </div>
                                  <div class="panel-body" style="max-height: 200px;overflow-y: scroll;">

                                                  <label> <input type="checkbox" runat="server"  id="chkVosoulStatus"/> سرویس وصال</label> 
                                                   </div>
                                         </div>
                                              

                                </div> </div> </div>        
                                
                    </div>

            </div>

    </div>     

     
    <asp:HiddenField ID="hdnAction" runat="server" />
   
</asp:Content>
