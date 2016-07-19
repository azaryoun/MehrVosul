﻿<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/MasterPage/SmartIntergace_Master.Master" CodeBehind="IncompleteFileInfoReport.aspx.vb" Inherits="MehrVosul.IncompleteFileInfoReport" %>
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
                                               <label>نوع نقص در پرونده</label>
                                                </div>
                                                    <div class="panel-body" style="max-height: 200px;">
                                             
                                                   <div class='checkbox'> <label> <input type='checkbox' id="chkbxName" runat="server" />
                                                   نام
                                                   
                                                    </label></div>

                                                       <div class='checkbox'> <label> <input type='checkbox' id="chkbxLName" runat="server" />
                                                   نام خانوادگی

                                                </label></div> 
                                                 <div class='checkbox'> <label> <input type='checkbox' id="chkbxNationalID" runat="server" />
                                                   شماره ملی
                                                </label></div> 
                                                  <div class='checkbox'> <label> <input type='checkbox' id="chkbxMobile" runat="server" />
                                                   شماره تلفن همراه
                                                </label></div> 
                                                   <div class='checkbox'> <label> <input type='checkbox' id="chkbxWorkTel" runat="server" />
                                                      تلفن محل کار

                                                </label></div> 
                                                  <div class='checkbox'> <label> <input type='checkbox' id="chkbxHomeTel" runat="server" />
                                                      تلفن منزل

                                                </label></div> 
                                                    <div class='checkbox'> <label> <input type='checkbox' id="chkbxAddress" runat="server" />
                                                     آدرس

                                                </label></div> 
                                                   </div>
                                                
                                                    
                                          </div>
                                     
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
                                    </asp:UpdatePanel></div> 
                                  
                                        <div class="form-group has-error">
                                            <label>شعبه ها</label>
                                         
                                             
                                         <div class="panel-body" style="max-height: 200px;overflow-y: scroll;">
                                           <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                                            <ContentTemplate>
                                                  <label> <input type="checkbox" value="" id="chkBranchSelectAll" onclick="return chkBranchSelectAll_Click();" runat="server" /> انتخاب/عدم انتخاب همه</label> 
                                           
                                                   <div  class="form-group" runat="server" id="divBranches">
                                                   </div></ContentTemplate> </asp:UpdatePanel>
                                                   

                                         </div></div>  </div>
                            
                            </div>

                        
                            </div>
                        </div>
                           
                           <div class="panel panel-default" id="divResult" runat="server" visible="false">
                        <div class="panel-heading">
                            گزارش اطلاعات ناقص</div>
                        <div class="panel-body">
                            <div class="table-responsive">
                                <table class="table table-striped table-bordered table-hover" id="tblResult" runat="server">
                                    <thead>
                                        <tr>
                                            <th>#</th>
                                            <th>شماره پرونده</th>
                                            <th>نام</th>
                                            <th>نام خانوادگی</th>
                                            <th>شماره ملی</th>
                                            <th>شماره تلفن همراه</th>
                                            <th>تلفن محل کار</th>
                                            <th>تلفن منزل</th>
                                         
                                           
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
