<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/MasterPage/SmartIntergace_Master.Master" CodeBehind="UserChangePassword.aspx.vb" Inherits="MehrVosul.UserChangePassword" %>
<%@ Register src="../../../UserControl/Bootstrap_Panel.ascx" tagname="Bootstrap_Panel" tagprefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script language="javascript" type="text/javascript">

     
        function StartthePage() {
            return true;
        }

        function SaveOperation_Validate() {

            var txtPassword = document.getElementById("<%=txtPassword.ClientID%>");



            if (trimall(txtPassword.value) == "") {
                alert("کلمه عبور جدید را وارد کنید");
                txtPassword.focus();
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
                                            <label>کلمه عبور جدید</label>
                                              <asp:TextBox ID="txtPassword" MaxLength="50" runat="server" 
                                                  cssclass="form-control" placeholder="کلمه عبور جدید را وارد کنید"  TextMode="Password"></asp:TextBox>
                                         </div>
                             
                            </div>



                        
                       </div>     
                            </div>

                    </div>

            </div>
    </div>     

     
    <asp:HiddenField ID="hdnAction" runat="server" />

</asp:Content>
