<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/MasterPage/SmartIntergace_Master.Master" CodeBehind="HandyFollowNewSearch.aspx.vb" Inherits="MehrVosul.HandyFollowNewSearch" %>
<%@ Register src="../../../UserControl/Bootstrap_Panel.ascx" tagname="Bootstrap_Panel" tagprefix="uc1" %>
<%@ Register src="../../../UserControl/UC_TimePicker.ascx" tagname="UC_TimePicker" tagprefix="uc3" %>
<%@ Register src="../../../UserControl/Bootstrap_PersianDateTimePicker.ascx" tagname="Bootstrap_PersianDateTimePicker" tagprefix="uc4" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script language="javascript" type="text/javascript">


        function StartthePage() {


            return true;
        }


        function DisplayOperation_Validate() {

            var txtFrom = document.getElementById("<%=txtFrom.ClientID%>");
            var txtTo= document.getElementById("<%=txtTo.ClientID%>");

            if (trimall(txtFrom.value) == "" || trimall(txtTo.value) == "") {
                alert("بازه را وارد نمایید");
                txtFrom.focus();
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
                      

                                       <div class="form-group has-error" >
                                       <div class="panel panel-default" >
                                                <div class="panel-heading">
                                               <label>محدوده بازه</label>
                                                </div>
                                                <div class="panel-body" style="max-height: 200px;">

                                                      <div class="form-group input-group input-group-sm">
                                            <span class="input-group-addon">از </span>
                                            <asp:TextBox ID="txtFrom"  CssClass="form-control"  runat="server"  placeholder="شروع بازه را وارد نمایید" ></asp:TextBox>
                                            <span class="input-group-addon"> روز </span>
                                            </div>

                                            
                                            <div class="form-group input-group input-group-sm">
                                            <span class="input-group-addon">تا </span>
                                            <asp:TextBox ID="txtTo" runat="server" CssClass="form-control" placeholder="پایان بازه را وارد نمایید"></asp:TextBox>
                                            <span class="input-group-addon"> روز </span>
                                            </div>
                                           
                                            </div> 
                                                 
                                                
                                                    
                                        </div>

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
