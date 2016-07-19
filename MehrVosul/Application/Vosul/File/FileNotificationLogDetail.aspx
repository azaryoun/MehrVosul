<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/MasterPage/SmartIntergace_Master.Master" CodeBehind="FileNotificationLogDetail.aspx.vb" Inherits="MehrVosul.FileNotificationLogDetail" %>
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



        function SaveOperation_Validate() {

            var txt_Msg = document.getElementById("<%=txt_Msg.ClientID%>");
            var txt_Msg = document.getElementById("<%=txt_Msg.ClientID%>");


            if (trimall(txt_Msg.value) == "") {
                alert("نام شعبه را وارد نمایید");
                txt_Msg.focus();
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

                      <div class="form-group has-error">

           <label>متن پیام</label>
                                                         
                      <asp:TextBox ID="txt_Msg"  CssClass="form-control"  runat="server"  
                              placeholder="متن پیام را وارد نمایید" Height="200px" TextMode="MultiLine" 
                              Enabled="False" ></asp:TextBox>
                      </div>
                        <div class="panel-body">
                         <div class="panel-body" style="max-height: 200px;">

                                                      <div id="divchkbxSent" runat="server"  class='checkbox'> <label> <input type='checkbox' id="chkbxSent" runat="server" 
                                                              disabled="disabled" />
                                                     ارسال شده

                                                </label>  </div> 
                                                  <div class="form-group has-error">  <asp:Label ID="lblStatus" Visible="false" runat="server" Text=""></asp:Label></div> 
                                       
                                            </div> 
                           <div class="form-group has-error">
                                              <div id="divDate" runat="server" class="panel panel-default" >
                                                <div class="panel-heading">
                                               <label>جزئیات ارسال</label>
                                                </div>
                                                <div class="panel-body" style="max-height: 200px;">

                                                    
   <uc4:Bootstrap_PersianDateTimePicker ID="Bootstrap_PersianDateTimePicker_From" 
                                                          runat="server" />

                                            
                                             
                                                
                                            


                                            </div> 
                                                 
                                                
                                                    
                                          </div>
                                        </div>
                               

                                              <div class="form-group has-error">
                                            <label>شماره نامه</label>
                                    
                                            <asp:TextBox ID="txtLetterNO" runat="server" cssclass="form-control" MaxLength="50" 
                                                      placeholder="شماره نامه را وارد کنید" Enabled="False"></asp:TextBox>
                                            
                                        </div>

                        </div>
                           
                           


                       </div>     

                            
                            </div>
                        </div>
             


     
    <asp:HiddenField ID="hdnAction" runat="server" />

</asp:Content>
