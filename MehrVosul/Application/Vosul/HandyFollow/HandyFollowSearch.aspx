<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/MasterPage/SmartIntergace_Master.Master" CodeBehind="HandyFollowSearch.aspx.vb" Inherits="MehrVosul.HandyFollowSearch" %>
<%@ Register src="../../../UserControl/Bootstrap_Panel.ascx" tagname="Bootstrap_Panel" tagprefix="uc1" %>
<%@ Register src="../../../UserControl/UC_TimePicker.ascx" tagname="UC_TimePicker" tagprefix="uc3" %>
<%@ Register src="../../../UserControl/Bootstrap_PersianDateTimePicker.ascx" tagname="Bootstrap_PersianDateTimePicker" tagprefix="uc4" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script language="javascript" type="text/javascript">


        function StartthePage() {


            return true;
        }


        function DisplayOperation_Validate() {

            var txt_InstallmentCount = document.getElementById("<%=txt_InstallmentCount.ClientID%>");
            var txtCustomerNO= document.getElementById("<%=txtCustomerNO.ClientID%>");

            if (trimall(txt_InstallmentCount.value) == "" && trimall(txtCustomerNO.value)== "") {
                alert("تعداد اقساط معوق را وارد نمایید");
                txt_InstallmentCount.focus();
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
                                

                                <asp:Button ID="Button1" runat="server" Text="Button" />
                                

                           <div class="col-md-12">

                              <label>استان</label>
                                <asp:ObjectDataSource ID="odcProvince" runat="server" 
                                    OldValuesParameterFormatString="original_{0}" SelectMethod="GetData" 
        
                                        TypeName="BusinessObject.dstBranchTableAdapters.spr_ProvinceList_SelectTableAdapter">
                        </asp:ObjectDataSource>
                            
                             
                                    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                        <ContentTemplate>
                                            <asp:DropDownList ID="cmbProvince" runat="server" CssClass="form-control" AutoPostBack="True">
                                            </asp:DropDownList>
                                        </ContentTemplate>
                                    </asp:UpdatePanel>

                                <div class="form-group">
                                <label>شعبه</label>
                                <asp:ObjectDataSource ID="odsBranch" runat="server" 
                                    OldValuesParameterFormatString="original_{0}" SelectMethod="GetData" 
        
                                    
                                                
                                TypeName="BusinessObject.dstBranchTableAdapters.spr_Branch_List_SelectTableAdapter">
                                    <SelectParameters>
                                        <asp:Parameter DefaultValue="1" Name="Action" Type="Int32" />
                                        <asp:Parameter DefaultValue="-1" Name="ProvinceID" Type="Int32" />
                                    </SelectParameters>
                        </asp:ObjectDataSource>
                                    <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                                        <ContentTemplate>
                                            <asp:DropDownList ID="cmbBranch" runat="server" AutoPostBack="True" 
                                                CssClass="form-control">
                                            </asp:DropDownList>
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                    
                                              
                              <div class="form-group">
                                <label>نوع وام</label>
                                <asp:ObjectDataSource ID="odsLoanType" runat="server" 
                                    OldValuesParameterFormatString="original_{0}" SelectMethod="GetData" 
        
                                                
                                
                                    TypeName="BusinessObject.dstLoanTypeTableAdapters.spr_LoanType_List_SelectTableAdapter">
                        </asp:ObjectDataSource>
                                              
                                  <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                                      <ContentTemplate>
                                          <asp:DropDownList ID="cmbLoanType" runat="server" CssClass="form-control" 
                                              AutoPostBack="True">
                                          </asp:DropDownList>
                                      </ContentTemplate>
                                  </asp:UpdatePanel>



                    </div>

                    </div>
                       <div class="form-group has-error">
                                            <label>تعداد اقساط معوق</label>
                                            <asp:UpdatePanel ID="UpdatePanel4" runat="server">
                                                <ContentTemplate>
                                                    <asp:TextBox ID="txt_InstallmentCount"  CssClass="form-control"  
                                                runat="server"  
    placeholder="تعداد اقساط معوق را وارد نمایید" AutoPostBack="True" ></asp:TextBox>
                                                </ContentTemplate>
                                            </asp:UpdatePanel>
                                              

                                        </div>


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
