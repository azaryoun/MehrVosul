<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/MasterPage/SmartIntergace_Master.Master" CodeBehind="DepositTypeNew.aspx.vb" Inherits="MehrVosul.DepositTypeNew" %>
<%@ Register src="../../../UserControl/Bootstrap_Panel.ascx" tagname="Bootstrap_Panel" tagprefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script language="javascript" type="text/javascript">


        function StartthePage() {
            return true;
        }

        function SaveOperation_Validate() {

            var txtDepositName = document.getElementById("<%=txtDepositName.ClientID%>");
            var txtDepositCode = document.getElementById("<%=txtDepositCode.ClientID%>");


           
            if (trimall(txtDepositCode.value) == "") {
                alert("کد نوع سپرده را وارد نمایید");
                txtDepositCode.focus();
                return false;
            }

            if (trimall(txtDepositName.value) == "") {
                alert("نام نوع سپرده را وارد نمایید");
                txtDepositName.focus();
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
                                            <label>کد</label>
                                              <asp:TextBox ID="txtDepositCode" MaxLength="50" runat="server" cssclass="form-control" placeholder="کد را وارد کنید"></asp:TextBox>
                                         </div>
                          
                                 
                                        <div class="form-group has-error">
                                            <label>عنوان</label>
                                          
                                            <asp:TextBox ID="txtDepositName" runat="server" cssclass="form-control" MaxLength="50" placeholder="عنوان را وارد کنید"></asp:TextBox>
                                            
                                        </div>
                          
                                       
                                      
                           
                       </div>     
                            </div>

                    </div>

            </div>
    </div>     

     
    <asp:HiddenField ID="hdnAction" runat="server" />

</asp:Content>
