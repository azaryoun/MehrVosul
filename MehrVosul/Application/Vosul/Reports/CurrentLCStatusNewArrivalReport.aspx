<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/MasterPage/SmartIntergace_Master.Master" CodeBehind="CurrentLCStatusNewArrivalReport.aspx.vb" Inherits="MehrVosul.CurrentLCStatusNewArrivalReport" %>
<%@ Register src="../../../UserControl/Bootstrap_Panel.ascx" tagname="Bootstrap_Panel" tagprefix="uc1" %>
<%@ Register src="../../../UserControl/UC_TimePicker.ascx" tagname="UC_TimePicker" tagprefix="uc3" %>
<%@ Register src="../../../UserControl/Bootstrap_PersianDateTimePicker.ascx" tagname="Bootstrap_PersianDateTimePicker" tagprefix="uc4" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script language="javascript" type="text/javascript">


        function StartthePage() {


            return true;
        }


        function DisplayOperation_Validate() {
        

            var divBranches = document.getElementById("<%=divBranches.ClientID%>");
            var divtmp = divBranches.firstChild;

            var boolChecked = false;

            while (divtmp) {
                var chktmp = divtmp.firstChild.nextSibling.firstChild.nextSibling;
                if (chktmp.checked) {
                    boolChecked = true;
                    break;
                }

                divtmp = divtmp.nextSibling;
            }

            if (!boolChecked) {
                alert("حداقل یک شعبه باید انتخاب شود");
                return false;

            }

            return true;
        }


        function ExcelOperation_Validate() {

            var divBranches = document.getElementById("<%=divBranches.ClientID%>");
            var divtmp = divBranches.firstChild;

            var boolChecked = false;

            while (divtmp) {
                var chktmp = divtmp.firstChild.nextSibling.firstChild.nextSibling;
                if (chktmp.checked) {
                    boolChecked = true;
                    break;
                }

                divtmp = divtmp.nextSibling;
            }

            if (!boolChecked) {
                alert("حداقل یک شعبه باید انتخاب شود");
                return false;

            }

            return true;
        }



        function chkBranchSelectAll_Click() {

            var divBranches = document.getElementById("<%=divBranches.ClientID%>");
            var chkBranchSelectAll = document.getElementById("<%=chkBranchSelectAll.ClientID%>");
            var divtmp = divBranches.firstChild;

            while (divtmp) {
                var chktmp = divtmp.firstChild.nextSibling.firstChild.nextSibling;
                chktmp.checked = chkBranchSelectAll.checked;

                divtmp = divtmp.nextSibling;
            }
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

                               <div class="panel panel-default" > 
                                 <div class="panel-heading">
                                            <label>شعبه</label>
                                              </div>
                                <div class="form-group">
                                     <label>استان</label>
                                <asp:ObjectDataSource ID="odcProvince" runat="server" 
                                    OldValuesParameterFormatString="original_{0}" SelectMethod="GetData" 
        
                                        TypeName="BusinessObject.dstBranchTableAdapters.spr_ProvinceList_SelectTableAdapter">
                        </asp:ObjectDataSource>
                               <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                        <ContentTemplate>
                                            <asp:DropDownList ID="cmbProvince" runat="server" CssClass="form-control" 
                                DataSourceID="odcProvince" DataTextField="Province" 
                            DataValueField="ID" AutoPostBack="True">
                                            </asp:DropDownList>
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                    </div> 

                                      <div class="form-group has-error">
                                       <label>شعبه ها</label>
                                         
                                             
                                         <div class="panel-body" style="max-height: 200px;overflow-y: scroll;">
                                           <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                                            <ContentTemplate>
                                                  <label> <input type="checkbox" value="" id="chkBranchSelectAll" runat="server" onclick="return chkBranchSelectAll_Click();"/> انتخاب/عدم انتخاب همه</label> 
                                          
                                                   <div  class="form-group" runat="server" id="divBranches">
                                                   </div></ContentTemplate> </asp:UpdatePanel>
                                                   

                                         </div>
                            

                    </div></div>


                      <div class="form-group">
                                <label>نوع وام</label>
                                <asp:ObjectDataSource ID="odsLoanType" runat="server" 
                                    OldValuesParameterFormatString="original_{0}" SelectMethod="GetData" 
        
                                                
                                
                                    TypeName="BusinessObject.dstLoanTypeTableAdapters.spr_LoanType_List_SelectTableAdapter">
                        </asp:ObjectDataSource>
                                              
                            <asp:DropDownList ID="cmbLoanType" runat="server" CssClass="form-control" 
                                DataSourceID="odsLoanType" DataTextField="LoanType" 
                            DataValueField="ID">
                                               
                            </asp:DropDownList>



                    </div>
                    
                            </div>
                            </div>
                        </div>
                           
                           <div class="panel panel-default" id="divResult" runat="server" visible="false">
                        <div class="panel-heading">
                            لیست پرونده های جدید معوق</div>
                        <div class="panel-body">
                            <div class="table-responsive">
                                <table class="table table-striped table-bordered table-hover" id="tblResult" runat="server">
                                    <thead>
                                        <tr>
                                            <th>#</th>
                                           <th>شماره مشتری</th>
                                           <th>نام مشتری</th>
                                           <th>شماره موبایل</th>
                                           <th>شماره تلفن</th>
                                            <th>شماره وام</th>
                                           <th>نام شعبه</th>
                                           <th>وضعیت</th>
                                           <th>تاریخ</th>
                                           <th>تعداد روز اقساط معوق</th>
                                           <th>نوع وام</th>
                                           
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
