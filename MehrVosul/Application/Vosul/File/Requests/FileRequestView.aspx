<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/MasterPage/SmartIntergace_Master.Master" CodeBehind="FileRequestView.aspx.vb" Inherits="MehrVosul.FileRequestView" %>
<%@ Register src="../../../../UserControl/Bootstrap_Panel.ascx" tagname="Bootstrap_Panel" tagprefix="uc1" %>


<%@ Register src="../../../../UserControl/UC_TimePicker.ascx" tagname="UC_TimePicker" tagprefix="uc2" %>


<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script language="javascript" type="text/javascript">


        function StartthePage() {
            return true;
        }

        function SaveOperation_Validate() {

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
                                            <label>شماره مشتری:</label>
                                            <label ID="lblCustomerNO" style="font-weight: normal" runat="server"></label>
                                            
                                        </div>
                          
                                                                      
                                        <div class="form-group has-error">
                                            <label>نام:</label>
                                            <label ID="lblName" style="font-weight: normal" runat="server" ></label>
                                          
                                        </div>

                                                                       
                                        <div class="form-group has-error">
                                            <label>نام خانوادگی:</label>
                                           <label ID="lblLName" style="font-weight: normal" runat="server"></label>
                                      </div>

                                        <div class="form-group has-error">
                                            <label>نام پدر:</label>
                                            <label ID="lblFatherName" style="font-weight: normal" runat="server"></label>
                                           
                                        </div>


                                        <div class="form-group has-error">
                                            <label>شماره تلفن همراه:</label>
                                            <label ID="lblMobile" style="font-weight: normal" runat="server"></label>
                                           
                                        </div>

                                        <div class="form-group has-error">
                                            <label>کد ملی:</label>
                                            <label ID="lblNationalID" style="font-weight: normal" runat="server"></label>
                                           
                                        </div>

                                         <div class="form-group has-error">
                                            <label>شماره شناسنامه:</label>
                                            <label ID="lblIDNO" style="font-weight: normal" runat="server"></label>
                                            
                                        </div>
                                           
                                     <div class="form-group">
                                     
                                       <label>پست الکترونیک:</label>
                                       <label ID="lblEmail" style="font-weight: normal" runat="server"></label>
         
                                         
                                  </div>
                                     
                                     
                                    
             
                       </div>    
                       
                       <div class="col-md-6">

             
                            <div class="form-group">

                                       

                                   <div class="form-group has-error">
                                     
                                       <label>آدرس:</label>
                                       <label ID="lblAddress" style="font-weight: normal"  runat="server"></label>
            
                                           
                                  </div>


                                     <div class="form-group has-error">
                                     
                                       <label>تلفن منزل:</label>
                                       <label ID="lblHomeTel"  style="font-weight: normal" runat="server"></label>
                                            
                                         
                                  </div>
                           
                                    <div class="form-group has-error">
                                     
                                       <label>تلفن محل کار:</label>
                                       <label ID="lblWorkTel" style="font-weight: normal" runat="server"></label>
                                                                            
                                  </div>       
                                                 
                                    <div class="form-group has-error" >
                                          <label>جنسیت:</label>
                                          <label ID="lblSex"  style="font-weight: normal" runat="server"></label>
                                    </div>


                                       <div class="form-group has-error">
                                     
                                       <label>وضعیت درخواست:</label>
                                       <label ID="lblRequestStatus" style="font-weight: normal"  runat="server"></label>
                                            
                                         
                                  </div>
                           
                                    <div class="form-group has-error">
                                     
                                       <label>کاربر درخواست دهنده:</label>
                                       <label ID="lblRequestedUser"  style="font-weight: normal" runat="server"></label>
                                                                            
                                  </div>       
                                                 
                                    <div class="form-group has-error" >
                                          <label>کاربر بررسی کننده:</label>
                                          <label ID="lblCheckerUser" style="font-weight: normal;" 
                                              runat="server"></label>
                                    </div>
                                                    
                                </div>
  
                            </div>
                                  
                                                       
                                </div> 
                            </div>

                    </div>

            </div>
    </div>     

     
    <asp:HiddenField ID="hdnAction" runat="server" />

</asp:Content>
