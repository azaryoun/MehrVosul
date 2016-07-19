<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/MasterPage/SmartIntergace_Master.Master" CodeBehind="WarningIntervalsException.aspx.vb" Inherits="MehrVosul.WarningIntervalsException" %>
<%@ Register src="../../../UserControl/Bootstrap_Panel.ascx" tagname="Bootstrap_Panel" tagprefix="uc1" %>


<%@ Register src="../../../UserControl/UC_TimePicker.ascx" tagname="UC_TimePicker" tagprefix="uc2" %>


<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

    <script src="../../../Scripts/Integrated.js" type="text/javascript"></script>
    <script language="javascript" type="text/javascript">


        function StartthePage() {
            return true;
        }

       
        function SaveOperation_Validate() {

            var txtWarningIntervalsName = document.getElementById("<%=txtWarningIntervalsExeptionName.ClientID%>");
            var divchklstLoanTypeItems = document.getElementById("<%=divchklstLoanTypeItems.ClientID%>");
            
            


            if (trimall(txtWarningIntervalsName.value) == "") {
                alert("عنوان استثناء گردش کار را وارد نمایید");
                txtWarningIntervalsName.focus();
                return false;
            }



           var divtmp = divchklstLoanTypeItems.firstChild;

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
                alert("حداقل یک نوع وام باید انتخاب شود");
                return false;

            }

            
            var branchBoolChecked = false;
            var treeView = document.getElementById("<%= trState.ClientID %>");
            var checkBoxes = treeView.getElementsByTagName("input");
            var checkedCount = 0;
            for (var i = 0; i < checkBoxes.length; i++) {
                if (checkBoxes[i].checked) {
                    branchBoolChecked = true;
                    break;
                }
            }


            if (!branchBoolChecked) {
                alert("حداقل یک شعبه باید انتخاب شود");
                return false;

            }
          

            return true;
        }


        function chkSelectAll_Click() {


            var divchklstLoanTypeItems = document.getElementById("<%=divchklstLoanTypeItems.ClientID%>");
            var chkSelectAll = document.getElementById("chkSelectAll");
            var divtmp = divchklstLoanTypeItems.firstChild;

             while (divtmp) {
                var chktmp = divtmp.firstChild.nextSibling.firstChild.nextSibling;
                 chktmp.checked= chkSelectAll.checked;
     
                divtmp = divtmp.nextSibling;
            }

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
                                            <label>عنوان</label>
                                            <asp:TextBox ID="txtWarningIntervalsExeptionName" runat="server" cssclass="form-control" MaxLength="50" placeholder="عنوان استثناء گردش کار را وارد کنید"></asp:TextBox>
                                            
                                        </div>
                                           
                                             <div class="panel panel-default" > 
                                           <div class="panel-heading">
                                            <label>نوع وام</label>
                                              </div>
                                             
                                         <div class="panel-body" style="max-height: 200px;overflow-y: scroll;">

                                                  <label> <input type="checkbox" value="" id="chkSelectAll" onclick="return chkSelectAll_Click();"/> انتخاب/عدم انتخاب همه</label> 
                                                   <div class="form-group" runat="server" id="divchklstLoanTypeItems">
                                                   </div>
                                         </div>
                                         </div>
                                        
                                       

                                     


                                           <div class="panel panel-default" > 
                                           <div class="panel-heading">
                                            <label>شعبه ها</label>
                                              </div>
                                             
                                         <div class="panel-body" style="max-height: 200px;overflow: scroll;">
        
                                                   <div class="form-group" runat="server" id="divTree">
                                                   <asp:Panel ID="treeViewDiv"  runat="server">
                                                        
                                                                <asp:TreeView ID="trState" runat="server"  ShowLines="True" onclick="return TreeClick(event)"
                                                                    Width="100%">
                                                                </asp:TreeView>
                                                          
                                                    </asp:Panel></div>
                                         </div>
                                         </div>
                                         <div class="panel panel-default" > 
                                           <div class="panel-heading">
                                            <label>وضعیت</label>
                                              </div>
                                             
                                         <div class="panel-body" style="max-height: 200px;overflow-y: scroll;">

                                                  <label> <input type="checkbox" runat="server"  id="chkStatus"/> فعال</label> 
                                                   </div>
                                         </div>
                                         
                                          </div>
              
                              

     
                       </div>    
                       
                    
                            </div>

                    </div>

            </div>

               

    </div>     

     <div class="form-group" runat="server" id="divBranches"></div>
     <label> <input type="checkbox" value="" id="chkBranchSelectAll" style="visibility:hidden" /></label>

    <asp:HiddenField ID="hdnAction" runat="server" />
     
</asp:Content>
