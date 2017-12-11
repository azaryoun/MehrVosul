<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/MasterPage/SmartIntergace_Master.Master" CodeBehind="HadiwarningIntervalsMagic.aspx.vb" Inherits="MehrVosul.HadiwarningIntervalsMagic" %>
<%@ Register src="../../../UserControl/Bootstrap_Panel.ascx" tagname="Bootstrap_Panel" tagprefix="uc1" %>


<%@ Register src="../../../UserControl/UC_TimePicker.ascx" tagname="UC_TimePicker" tagprefix="uc2" %>


<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

    <script src="../../../Scripts/Integrated.js" type="text/javascript"></script>
    <script language="javascript" type="text/javascript">


        function StartthePage() {
            return true;
        }

       
        function SaveOperation_Validate() {
            
            var txtWarningIntervalsName = document.getElementById("<%=txtWarningIntervalsName.ClientID%>");
           
            var txtFrom = document.getElementById("<%=txtFrom.ClientID%>");
            var txtTo = document.getElementById("<%=txtTo.ClientID%>");
        
            var divchklstDepossitItems = document.getElementById("<%=divchklstDepositItems.ClientID%>");
            var divchklstLoanTypeItems = document.getElementById("<%=divchklstLoanTypeItems.ClientID%>");
            
            var cmbHadiWarningIntervals = document.getElementById("<%=cmbHadiWarningIntervals.ClientID%>");
            var hdnForDeposit = document.getElementById("<%=hdnForDeposit.ClientID%>");
            

            if (cmbHadiWarningIntervals.value == -1) {
                alert("نوع گردش کار را مشخص نمایید", "خطا");
                cmbHadiWarningIntervals.focus();
                return false;
            }

            if (trimall(txtWarningIntervalsName.value) == "") {
                alert("عنوان گردش کار را وارد نمایید");
                txtWarningIntervalsName.focus();
                return false;
            }


            var divtmp
            if (divchklstDepossitItems != null)
            {
                 divtmp = divchklstDepossitItems.firstChild;
            }
            else
            {
                divtmp = divchklstLoanTypeItems.firstChild;
            }


             

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
                    alert("حداقل یک نوع سپرده و یا نوع وام باید انتخاب شود");
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
     

         
            if (isNaN(txtFrom.value) || trimall(txtFrom.value) == "") {
                alert("بازه از را وارد نمایید");
                txtFrom.focus();
                return false;
            }

            if (isNaN(txtTo.value) || trimall(txtTo.value) == "") {
                alert("بازه تا را وارد نمایید");
                txtTo.focus();
                return false;
            }


            if (parseInt(txtFrom.value) > parseInt(txtTo.value)) {
                alert("شروع بازه باید از پایان بازه کوچکتر باشد");
                txtTo.focus();
                return false;
            }

          
          

            return true;
        }


        function chkSelectAll_Click() {


            var divchklstDepositItems = document.getElementById("<%=divchklstDepositItems.ClientID%>");
            var divchklstLoanTypeItems = document.getElementById("<%=divchklstLoanTypeItems.ClientID%>");

            var chkSelectAll = document.getElementById("chkSelectAll");
            var chkSelectAllLoan = document.getElementById("chkSelectAllLoan");

            var chkSelectAll1;
            var divtmp;
            if (divchklstDepositItems != null) {
                divtmp = divchklstDepositItems.firstChild;
                chkSelectAll1 = chkSelectAll;
            }
            else {
                divtmp = divchklstLoanTypeItems.firstChild;
                chkSelectAll1 = chkSelectAllLoan;
            }


             while (divtmp) {
                var chktmp = divtmp.firstChild.nextSibling.firstChild.nextSibling;
                 chktmp.checked= chkSelectAll1.checked;
     
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
                                   
                                 
                                   <div  class="form-group has-error">
                                              <label>گردش کار</label>
                                <asp:ObjectDataSource ID="odcHadiWarningList" runat="server" 
                                    OldValuesParameterFormatString="original_{0}" SelectMethod="GetData" 
        
                                        TypeName="BusinessObject.dstHadiWarningIntervalsTableAdapters.spr_HadiWarningIntervals_List_SelectTableAdapter">
                                    <SelectParameters>
                                        <asp:Parameter Name="FKCUserID" Type="Int32" />
                                    </SelectParameters>
                        </asp:ObjectDataSource>
                            
                             
                                    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                        <ContentTemplate>
                                            <asp:DropDownList ID="cmbHadiWarningIntervals" runat="server" CssClass="form-control" 
                                DataSourceID="odcHadiWarningList" DataTextField="WarniningTitle" 
                            DataValueField="ID" AutoPostBack="True">
                                            </asp:DropDownList>
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                        </div>

                                        <div class="form-group has-error">
                                            <label>عنوان</label>
                                          
                                            <asp:TextBox ID="txtWarningIntervalsName" runat="server" cssclass="form-control" MaxLength="50" placeholder="عنوان گردش کار را وارد کنید"></asp:TextBox>
                                            
                                        </div>
                                           
                                      <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                                        <ContentTemplate>
                                             <div  id="divDeposit" runat="server"  visible="false"  class="panel panel-default" > 
                                           <div class="panel-heading">
                                            <label>نوع سپرده</label>
                                              </div>
                                             
                                         <div class="panel-body" style="max-height: 200px;overflow-y: scroll;">

                                                  <label> <input type="checkbox" value="" id="chkSelectAll" onclick="return chkSelectAll_Click();"/> انتخاب/عدم انتخاب همه</label> 
                                                   <div class="form-group" runat="server" id="divchklstDepositItems">
                                                   </div>
                                         </div>
                                         </div>
                                            </ContentTemplate>
                                        </asp:UpdatePanel>

                                   <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                                     <ContentTemplate>
                                     <div id="divLoan" runat="server" visible="false"  class="panel panel-default" > 
                                           <div class="panel-heading">
                                            <label>نوع وام</label>
                                              </div>
                                             
                                         <div class="panel-body" style="max-height: 200px;overflow-y: scroll;">

                                                  <label> <input type="checkbox" value="" id="chkSelectAllLoan" onclick="return chkSelectAll_Click();"/> انتخاب/عدم انتخاب همه</label> 
                                                   <div class="form-group" runat="server" id="divchklstLoanTypeItems">
                                                   </div>
                                         </div>
                                         </div>
                                       </ContentTemplate>
                                       </asp:UpdatePanel>

                                           <div class="panel panel-default" > 
                                           <div class="panel-heading">
                                            <label>شعبه ها</label>
                                              </div>
                                             
                                         <div class="panel-body" style="max-height: 200px;overflow: scroll;">
        
                                                   <div class="form-group" runat="server" id="divTree">
                                                       <asp:UpdatePanel ID="UpdatePanel4" runat="server">
                                                           <ContentTemplate>
                                                               <asp:Panel ID="treeViewDiv" runat="server">
                                                                   <asp:TreeView ID="trState" runat="server" onclick="return TreeClick(event)" ShowLines="True" Width="100%">
                                                                   </asp:TreeView>
                                                               </asp:Panel>
                                                           </ContentTemplate>
                                                       </asp:UpdatePanel>
                                                   </div>
                                         </div>
                                         </div>
                                        

                              
                            <div class="form-group">


                                <div class="panel panel-default">
                                    <div class="panel-heading">
                                        <label>زمان اطلاع رسانی</label>
                                    </div>
                                    <div class="panel-body" style="max-height: 200px;">

                                        <div class='radio'>
                                              <asp:RadioButton ID="rdboLoanApprovment" Checked="true" GroupName="rdoPeriodTime" Text="تصویب وام" runat="server" />
                                           
                                        </div>

                                        <div class='radio'>
                                                <asp:RadioButton ID="rdboIssuingContract"  GroupName="rdoPeriodTime" Text="صدور قرارداد" runat="server" />
                                        
                                        </div>
                                        <div class='radio'>

                                                   <asp:RadioButton ID="rdboLaonPaid"  GroupName="rdoPeriodTime" Text="پرداخت وام" runat="server" />
                                        
                                        </div>

                                    </div>




                                </div>


                            </div>

                              
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
    <asp:HiddenField ID="hdnForDeposit" runat="server" />
     
</asp:Content>
