<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/MasterPage/SmartIntergace_Master.Master" CodeBehind="FileNotificationLogDetailSMS.aspx.vb" Inherits="MehrVosul.FileNotificationLogDetailSMS" %>
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
                <br />
                <div class="form-group has-error">
                <label>وضعیت ارسال</label>
                <br />
                           <asp:Label ID="lblSendStatus" runat="server" Text=""></asp:Label>
                </div>
                      <div class="form-group has-error">

           <label>متن پیام</label>
                                                         
                      <asp:TextBox ID="txt_Msg"  CssClass="form-control"  runat="server"  
                              placeholder="متن پیام را وارد نمایید" Height="200px" TextMode="MultiLine" 
                              Enabled="False" ></asp:TextBox>
                      </div>
                        <div class="panel-body">
                         <div class="panel-body" style="max-height: 200px;">

                                      <asp:Label ID="lblStatus" runat="server" Text=""></asp:Label>
                                            </div> 

                        </div>
                           
                           


                       </div>     

                            
                            </div>
                        </div>
             


     
    <asp:HiddenField ID="hdnAction" runat="server" />

</asp:Content>
