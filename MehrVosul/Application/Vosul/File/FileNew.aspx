<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/MasterPage/SmartIntergace_Master.Master" CodeBehind="FileNew.aspx.vb" Inherits="MehrVosul.FileNew" %>
<%@ Register src="../../../UserControl/Bootstrap_Panel.ascx" tagname="Bootstrap_Panel" tagprefix="uc1" %>


<%@ Register src="../../../UserControl/UC_TimePicker.ascx" tagname="UC_TimePicker" tagprefix="uc2" %>


<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script language="javascript" type="text/javascript">


        function StartthePage() {
            return true;
        }

        function SaveOperation_Validate() {

            var txtCustomerNO = document.getElementById("<%=txtCustomerNO.ClientID%>");
            var txtName = document.getElementById("<%=txtName.ClientID%>");
            var txtLName = document.getElementById("<%=txtLName.ClientID%>");
            var txtFatherName = document.getElementById("<%=txtFatherName.ClientID%>");
            var txtMobile = document.getElementById("<%=txtMobile.ClientID%>");
            var txtNationalID = document.getElementById("<%=txtNationalID.ClientID%>");
            var txtIDNO = document.getElementById("<%=txtIDNO.ClientID%>");
            var txtAddress = document.getElementById("<%=txtAddress.ClientID%>");
            var txtHomeTel = document.getElementById("<%=txtHomeTel.ClientID%>");
            var txtWorkTel = document.getElementById("<%=txtWorkTel.ClientID%>");
        
            if (trimall(txtCustomerNO.value) == "") {
                alert("شماره مشتری را وارد نمایید");
                txtCustomerNO.focus();
                return false;
            }

            if (trimall(txtName.value) == "") {
                alert("نام را وارد نمایید");
                txtName.focus();
                return false;
            }

            if (trimall(txtLName.value) == "") {
                alert("نام خانوادگی را وارد نمایید");
                txtLName.focus();
                return false;
            }


            if (trimall(txtFatherName.value) == "") {
                alert("نام پدر را وارد نمایید");
                txtFatherName.focus();
                return false;
            }

            if (trimall(txtMobile.value) == "") {
                alert("تلفن همراه را وارد نمایید");
                txtMobile.focus();
                return false;
            }


            if (trimall(txtNationalID.value) == "") {
                alert("کد ملی را وارد نمایید");
                txtNationalID.focus();
                return false;
            }


            if (trimall(txtIDNO.value) == "") {
                alert("شماره شناسنامه را وارد نمایید");
                txtIDNO.focus();
                return false;
            }


            if (trimall(txtAddress.value) == "") {
                alert("آدرس را وارد نمایید");
                txtAddress.focus();
                return false;
            }

            if (trimall(txtHomeTel.value) == "") {
                alert("تلفن منزل را وارد نمایید");
                txtHomeTel.focus();
                return false;
            }


            if (trimall(txtWorkTel.value) == "") {
                alert("تلفن محل کار را وارد نمایید");
                txtWorkTel.focus();
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
                                            <label>شماره مشتری</label>
                                          
                                            <asp:TextBox ID="txtCustomerNO" runat="server" cssclass="form-control" MaxLength="50" placeholder="شماره مشتری را وارد کنید"></asp:TextBox>
                                            
                                        </div>
                          
                                                                      
                                        <div class="form-group has-error">
                                            <label>نام</label>
                                          
                                            <asp:TextBox ID="txtName" runat="server" cssclass="form-control" MaxLength="50" placeholder="نام را وارد کنید"></asp:TextBox>
                                            
                                        </div>

                                                                       
                                        <div class="form-group has-error">
                                            <label>نام خانوادگی</label>
                                          
                                            <asp:TextBox ID="txtLName" runat="server" cssclass="form-control" MaxLength="50" placeholder="نام خانوادکی را وارد کنید"></asp:TextBox>
                                            
                                        </div>

                                        <div class="form-group has-error">
                                            <label>نام پدر</label>
                                          
                                            <asp:TextBox ID="txtFatherName" runat="server" cssclass="form-control" MaxLength="50" placeholder="نام پدر را وارد کنید"></asp:TextBox>
                                            
                                        </div>


                                        <div class="form-group has-error">
                                            <label>شماره تلفن همراه</label>
                                          
                                            <asp:TextBox ID="txtMobile" runat="server" cssclass="form-control" MaxLength="50" placeholder="شماره تلفن همراه را وارد کنید"></asp:TextBox>
                                            
                                        </div>

                                        <div class="form-group has-error">
                                            <label>کد ملی</label>
                                          
                                            <asp:TextBox ID="txtNationalID" runat="server" cssclass="form-control" MaxLength="50" placeholder="کد ملی را وارد کنید"></asp:TextBox>
                                            
                                        </div>


                                     
                                     
                                    
             
                       </div>    
                       
                       <div class="col-md-6">

             
                            <div class="form-group">

                                        <div class="form-group has-error">
                                            <label>شماره شناسنامه</label>
                                          
                                            <asp:TextBox ID="txtIDNO" runat="server" cssclass="form-control" MaxLength="50" placeholder="شماره شناسنامه را وارد کنید"></asp:TextBox>
                                            
                                        </div>
                                           
                                     <div class="form-group">
                                     
                                       <label>پست الکترونیکی</label>
                                          
                                            <asp:TextBox ID="txtEmail" runat="server" cssclass="form-control" MaxLength="50" placeholder="پست الکترونیکی را وارد کنید"></asp:TextBox>
                                            
                                         
                                  </div>

                                   <div class="form-group has-error">
                                     
                                       <label>نشانی</label>
                                          
                                            <asp:TextBox ID="txtAddress" runat="server" cssclass="form-control" 
                                           MaxLength="50" placeholder="نشانی را وارد کنید" TextMode="MultiLine"></asp:TextBox>
                                            
                                         
                                  </div>


                                     <div class="form-group has-error">
                                     
                                       <label>تلفن منزل</label>
                                          
                                            <asp:TextBox ID="txtHomeTel" runat="server" cssclass="form-control" MaxLength="50" placeholder="تلفن منزل را وارد کنید"></asp:TextBox>
                                            
                                         
                                  </div>
                           
                                    <div class="form-group has-error">
                                     
                                       <label>تلفن محل کار</label>
                                          
                                            <asp:TextBox ID="txtWorkTel" runat="server" cssclass="form-control" MaxLength="50" placeholder="تلفن محل کار را وارد کنید"></asp:TextBox>
                                            
                                         
                                  </div>       
                                       <label>جنسیت</label>             
                                    <div class="panel-body" style="max-height: 200px;">
                                        <asp:RadioButtonList ID="rdbListSex" runat="server">
                                            <asp:ListItem Selected="True" Value="0">زن</asp:ListItem>
                                            <asp:ListItem Value="1">مرد</asp:ListItem>
                                        </asp:RadioButtonList>
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
