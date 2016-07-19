<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/MasterPage/SmartIntergace_Master.Master" CodeBehind="BranchNew.aspx.vb" Inherits="MehrVosul.BranchNew" %>
<%@ Register src="../../../UserControl/Bootstrap_Panel.ascx" tagname="Bootstrap_Panel" tagprefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script language="javascript" type="text/javascript">


        function StartthePage() {
            return true;
        }

        function SaveOperation_Validate() {

            var txtBranchName = document.getElementById("<%=txtBranchName.ClientID%>");
            var txtBranchCode = document.getElementById("<%=txtBranchCode.ClientID%>");
            var cmbProvince = document.getElementById("<%=cmbProvince.ClientID%>");

            

            if (trimall(txtBranchCode.value) == "") {
                alert("کد شعبه را وارد نمایید");
                txtBranchCode.focus();
                return false;
            }

            if (trimall(txtBranchName.value) == "") {
                alert("نام شعبه را وارد نمایید");
                txtBranchName.focus();
                return false;
            }


            if (cmbProvince.options(cmbProvince.selectedIndex).value == -1) {
                alert("استان را مشخص نمائید", "خطا");
                cmbProvince.focus();
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
                                            <label>کد شعبه</label>
                                              <asp:TextBox ID="txtBranchCode" MaxLength="50" runat="server" cssclass="form-control"  placeholder="کد شعبه را وارد کنید"></asp:TextBox>
                                         </div>

                                            <div class="form-group has-error">
                                            <label>نام شعبه</label>
                                          
                                            <asp:TextBox ID="txtBranchName" runat="server" cssclass="form-control" MaxLength="50" placeholder="نام شعبه را وارد کنید"></asp:TextBox>
                                            
                                        </div>

                                             <div class="form-group has-error"><asp:ObjectDataSource ID="odcProvince" 
                                                runat="server" OldValuesParameterFormatString="original_{0}" 
                                                SelectMethod="GetData" 
                                                
                                                     TypeName="BusinessObject.dstBranchTableAdapters.spr_ProvinceList_SelectTableAdapter">
                                            </asp:ObjectDataSource>
                                            <label>استان</label>
                                            &nbsp;<asp:DropDownList ID="cmbProvince" runat="server" 
                                                        CssClass="form-control" DataSourceID="odcProvince" 
                                                        DataTextField="Province" DataValueField="ID">
                 
                                                        </asp:DropDownList>
                                            
                                        </div>

                                      
                                            <div class="form-group">
                                            <label>تلفن شعبه</label>
                                          
                                            <asp:TextBox ID="txtTelephon" runat="server" cssclass="form-control" MaxLength="50" placeholder="تلفن شعبه را وارد کنید"></asp:TextBox>
                                            
                                        </div>
                            <div class="form-group">
                                            <label>نشانی شعبه</label>
                                              <asp:TextBox ID="txtBranchAddress" MaxLength="250" runat="server"  TextMode="MultiLine" Rows="3" cssclass="form-control" placeholder="نشانی شعبه را وارد کنید"></asp:TextBox>
                                        

                                        </div>

                                        <div class="form-group">
                                            <label>IP شعبه</label>
                                              <asp:TextBox ID="txtBranchIP" MaxLength="250" runat="server"   Rows="3" cssclass="form-control" placeholder="IP شعبه را وارد کنید"></asp:TextBox>
                                        

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
