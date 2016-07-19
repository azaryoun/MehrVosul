<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/MasterPage/SmartIntergace_Master.Master" CodeBehind="LoanTypeNew.aspx.vb" Inherits="MehrVosul.LoanTypeNew" %>
<%@ Register src="../../../UserControl/Bootstrap_Panel.ascx" tagname="Bootstrap_Panel" tagprefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script language="javascript" type="text/javascript">


        function StartthePage() {
            return true;
        }

        function SaveOperation_Validate() {

            var txtLoanTypeName = document.getElementById("<%=txtLoanTypeName.ClientID%>");
            var txtLoanTypeCode = document.getElementById("<%=txtLoanTypeCode.ClientID%>");


           
            if (trimall(txtLoanTypeCode.value) == "") {
                alert("کد نوع وام را وارد نمایید");
                txtLoanTypeCode.focus();
                return false;
            }

            if (trimall(txtLoanTypeName.value) == "") {
                alert("نام نوع وام را وارد نمایید");
                txtLoanTypeName.focus();
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
                                              <asp:TextBox ID="txtLoanTypeCode" MaxLength="50" runat="server" cssclass="form-control" placeholder="کد را وارد کنید"></asp:TextBox>
                                         </div>
                          
                                 
                                        <div class="form-group has-error">
                                            <label>عنوان</label>
                                          
                                            <asp:TextBox ID="txtLoanTypeName" runat="server" cssclass="form-control" MaxLength="50" placeholder="عنوان را وارد کنید"></asp:TextBox>
                                            
                                        </div>
                          
                                       
                                          <div>
                                            <label>بخش</label>
                                          
                                            <asp:TextBox ID="txtSection" runat="server" cssclass="form-control" MaxLength="50" placeholder="بخش را وارد کنید"></asp:TextBox>
                                            
                                        </div>

                           
                       </div>     
                            </div>

                    </div>

            </div>
    </div>     

     
    <asp:HiddenField ID="hdnAction" runat="server" />

</asp:Content>
