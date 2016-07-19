<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/MasterPage/SmartIntergace_Master.Master" CodeBehind="WhiteListNew.aspx.vb" Inherits="MehrVosul.WhiteListNew" %>
<%@ Register src="../../../UserControl/Bootstrap_Panel.ascx" tagname="Bootstrap_Panel" tagprefix="uc1" %>



<%@ Register src="../../../UserControl/Bootstrap_PersianDateTimePicker.ascx" tagname="Bootstrap_PersianDateTimePicker" tagprefix="uc2" %>



<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script language="javascript" type="text/javascript">


        function StartthePage() {
            return true;
        }

        function SaveOperation_Validate() {


            var txtRemarks = document.getElementById("<%=txtRemarks.ClientID%>");
            if (trimall(txtRemarks.value) == "") {
                alert("توضیحات را وارد نمایید");
                txtRemarks.focus();
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
                                <div class="col-md-12">
                                   
                                 
                                        <div class="form-group has-error">
                                            <label>توضیحات</label>
                                          
                                            <asp:TextBox ID="txtRemarks" runat="server" cssclass="form-control" 
                                                MaxLength="50" placeholder="توضیحات را وارد کنید" Height="60px" 
                                                TextMode="MultiLine"></asp:TextBox>
                                            
                                        </div>
                          
                                                                      
                   <div class="form-group has-error">
                                              <div class="panel panel-default" >
                                                <div class="panel-heading">
                                               <label>تاریخ انقضا</label></div>
                                                <div class="panel-body" style="max-height: 200px;">

                                                    <uc2:Bootstrap_PersianDateTimePicker ID="Bootstrap_PersianDateExpireDate" 
                                                        runat="server" />


                                            </div> 
                                                 
                                                
                                                    
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
