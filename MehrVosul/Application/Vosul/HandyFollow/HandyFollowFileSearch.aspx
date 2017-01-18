<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/MasterPage/SmartIntergace_Master.Master" CodeBehind="HandyFollowFileSearch.aspx.vb" Inherits="MehrVosul.HandyFollowFileSearch" %>
<%@ Register src="../../../UserControl/Bootstrap_Panel.ascx" tagname="Bootstrap_Panel" tagprefix="uc1" %>
<%@ Register src="../../../UserControl/UC_TimePicker.ascx" tagname="UC_TimePicker" tagprefix="uc3" %>
<%@ Register src="../../../UserControl/Bootstrap_PersianDateTimePicker.ascx" tagname="Bootstrap_PersianDateTimePicker" tagprefix="uc4" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script language="javascript" type="text/javascript">


        function StartthePage() {


            return true;
        }


        function DisplayOperation_Validate() {

           
            var txtCustomerNO= document.getElementById("<%=txtCustomerNO.ClientID%>");

            if (trimall(txtCustomerNO.value)== "") {
                alert("شماره مشتری را وارد نمایید");
                txtCustomerNO.focus();
                return false;
            }

            return true;
        }

    

        function btnFollwoing_ClientClick(Pkey, Pkey1,Pkey2) {
            var hdnAction_Name = "<%=hdnAction.ClientID%>";
            var hdnAction = document.getElementById(hdnAction_Name);
            hdnAction.value = "S;" + Pkey + ";" + Pkey1 + ";" + Pkey2 + ";";
            window.document.forms[0].submit();
            return false;
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

                            


                                <div class="form-group">
                                            <label>شماره مشتری</label>
                                            <asp:UpdatePanel ID="UpdatePanel5" runat="server">
                                                <ContentTemplate>
                                                    <asp:TextBox ID="txtCustomerNO"  CssClass="form-control"  
                                                runat="server"  
    placeholder="شماره مشتری را وارد نمایید" AutoPostBack="True" ></asp:TextBox>
                                                </ContentTemplate>
                                            </asp:UpdatePanel>
                                              

                                        </div>
                            </div>
                            </div>
                        </div>
                           
                           <div class="panel panel-default" id="divResult" runat="server" visible="false">
                        <div class="panel-heading">
                             لیست پرونده ها با تعداد اقساط معوق مشخص </div>
                        <div class="panel-body">
                        
                            <div class="table-responsive">
                                <table class="table table-striped table-bordered table-hover" id="tblResult" runat="server">
                                    <thead>
                                        <tr>
                                           
                                            <th>#</th>
                                            <th>شماره مشتری</th>
                                            <th>نام و نام خانوادگی</th>
                                             <th>تسهیلات</th>
                                              <th>همراه</th>
                                           <th>تعداد روز معوق </th>
                                            <th>شعبه </th>
                                           <th>ثبت پیگیری</th>
                                             <th>کارشناس پیگیر</th>
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
