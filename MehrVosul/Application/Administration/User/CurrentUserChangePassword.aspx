<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/MasterPage/SmartIntergace_Master.Master" CodeBehind="CurrentUserChangePassword.aspx.vb" Inherits="MehrVosul.CurrentUserChangePassword" %>
<%@ Register src="../../../UserControl/Bootstrap_Panel.ascx" tagname="Bootstrap_Panel" tagprefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script language="javascript" type="text/javascript">

     
        function StartthePage() {
            return true;
        }

        function SaveOperation_Validate() {

            var txtCurrentPassword = document.getElementById("<%=txtCurrentPassword.ClientID%>");
            var txtNewPassword = document.getElementById("<%=txtNewPassword.ClientID%>");
            var txtRetypePassword = document.getElementById("<%=txtRetypePassword.ClientID%>");


            if (trimall(txtCurrentPassword.value) == "") {
                alert("Enter current password");
                txtCurrentPassword.focus();
                return false;
            }



            if (trimall(txtNewPassword.value) == "") {
                alert("Enter new Password");
                txtNewPassword.focus();
                return false;
            }


            if (trimall(txtNewPassword.value) != trimall(txtRetypePassword.value)) {
                alert("New passowrd does not match with the retyped password");
                txtNewPassword.focus();
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
                                   
                                 
                                        <div class="form-group has-success">
                                            <label>نام کاربری</label>
                                          
                                            <asp:TextBox ID="txtUsername" runat="server" cssclass="form-control" 
                                                MaxLength="50" placeholder="نام کاربری را وارد کنید" ReadOnly="True"></asp:TextBox>
                                        </div>
                                         
                                          <div class="form-group has-error">
                                            <label>کلمه عبور فعلی </label>
                                              <asp:TextBox ID="txtCurrentPassword" MaxLength="50" runat="server" 
                                                  cssclass="form-control" placeholder="کلمه عبور را وارد کنید"  TextMode="Password"></asp:TextBox>
                                         </div>

                                          <div class="form-group has-error">
                                            <label>کلمه عبور جدید</label>
                                              <asp:TextBox ID="txtNewPassword" MaxLength="50" runat="server" 
                                                  cssclass="form-control" placeholder="کلمه عبور جدید را وارد کنید"  TextMode="Password"></asp:TextBox>
                                         </div>

                                          <div class="form-group has-error">
                                            <label>تکرار کلمه عبور جدید</label>
                                              <asp:TextBox ID="txtRetypePassword" MaxLength="50" runat="server" 
                                                  cssclass="form-control" placeholder="تکرار کلمه عبور جدید را وارد کنید"  TextMode="Password"></asp:TextBox>
                                         </div>

                             
                            </div>



                        
                       </div>     
                            </div>

                    </div>

            </div>
    </div>     

     
    <asp:HiddenField ID="hdnAction" runat="server" />

</asp:Content>
