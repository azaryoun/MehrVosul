<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/MasterPage/SmartIntergace_Master.Master" CodeBehind="HadiwarningIntervalsEdit.aspx.vb" Inherits="MehrVosul.HadiwarningIntervalsEdit" %>
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
            var cmbFrequencyInDay = document.getElementById("<%=cmbFrequencyInDay.ClientID%>");
            var cmbFrequencyInHour = document.getElementById("<%=cmbFrequencyInHour.ClientID%>");
            var chkbxCallTelephone = document.getElementById("<%=chkbxCallTelephone.ClientID%>");
            var chkbxVoiceMessage = document.getElementById("<%=chkbxVoiceMessage.ClientID%>");
            var chkbxSendSMS = document.getElementById("<%=chkbxSendSMS.ClientID%>");
            var divchklstDepossitItems = document.getElementById("<%=divchklstDepositItems.ClientID%>");
            var divchklstLoanTypeItems = document.getElementById("<%=divchklstLoanTypeItems.ClientID%>");
            
            var rdoNewDeposit = document.getElementById("<%=rdoNewDeposit.ClientID%>");
            var rdoGetDeposit = document.getElementById("<%=rdoNewDeposit.ClientID%>");
            var hdnWarningIntervalTypeNew =  document.getElementById("<%=hdnWarningIntervalTypeNew.ClientID%>");

            if (trimall(txtWarningIntervalsName.value) == "") {
                alert("عنوان گردش کار را وارد نمایید");
                txtWarningIntervalsName.focus();
                return false;
            }



            var divtmp;
            if (divchklstDepossitItems != null) {
                divtmp = divchklstDepossitItems.firstChild;
            }
            else {
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
                alert("حداقل یک نوع سپرده باید انتخاب شود");
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

            if (cmbFrequencyInDay.value == -1) {
                alert("تعداد اطلاع رسانی در روز را مشخص نمایید", "خطا");
                cmbFrequencyInDay.focus();
                return false;
            }



            if (cmbFrequencyInHour.value == -1) {
                alert("فاصله زمانی هر تکرار را مشخص نمائید", "خطا");
                cmbFrequencyInHour.focus();
                return false;
            }

            if (chkbxCallTelephone.checked == false && chkbxVoiceMessage.checked == false && chkbxSendSMS.checked == false) {
                alert("یکی از انواع اطلاع رسانی را مشخص نمایید", "خطا");
               
                return false;
            }


            if (chkbxCallTelephone.checked == true)
            {
                if (chkbxVoiceMessage.checked == true || chkbxSendSMS.checked == true) {
                    alert("امکان انتخاب نوع دیگر اطلاع رسانی با تماس تلفنی وجود ندارد", "خطا");

                    return false;
                }
            }

            
          
            if ((rdoNewDeposit.checked == true && hdnWarningIntervalTypeNew.value != "Deposit")  || (rdoNewDeposit.checked == false && hdnWarningIntervalTypeNew.value != "Loan"))
            {

                if (!confirm("در صورت تغییر نوع گردش کار الگو مربوط به گردش کار جاری حذف خواهد شد، آیا تایید می نمایید؟"))
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
                                <div class="col-md-6">
                                   
                                 
                                        <div class="form-group has-error">
                                            <label>عنوان</label>
                                          
                                            <asp:TextBox ID="txtWarningIntervalsName" runat="server" cssclass="form-control" MaxLength="50" placeholder="عنوان گردش کار را وارد کنید"></asp:TextBox>
                                            
                                        </div>
                                           
                                    
                                       <div id="divDeposit" runat="server"  class="panel panel-default" > 
                                           <div class="panel-heading">
                                            <label>نوع سپرده</label>
                                              </div>
                                             
                                         <div class="panel-body" style="max-height: 200px;overflow-y: scroll;">

                                                  <label> <input type="checkbox" value="" id="chkSelectAll" onclick="return chkSelectAll_Click();"/> انتخاب/عدم انتخاب همه</label> 
                                                   <div class="form-group" runat="server" id="divchklstDepositItems">
                                                   </div>
                                         </div>
                                         </div>
       
                                     <div id="divLoan" runat="server" visible="false"   class="panel panel-default" > 
                                           <div class="panel-heading">
                                            <label>نوع وام</label>
                                              </div>
                                             
                                         <div class="panel-body" style="max-height: 200px;overflow-y: scroll;">

                                                  <label> <input type="checkbox" value="" id="chkSelectAllLoan" onclick="return chkSelectAll_Click();"/> انتخاب/عدم انتخاب همه</label> 
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
                       
                       <div class="col-md-6">

                     
                                 <div class="form-group has-error">
                                           <label>تعداد اطلاع رسانی در روز</label>
                                            <asp:DropDownList ID="cmbFrequencyInDay" runat="server" 
                                               CssClass="form-control">
                                                <asp:ListItem Value="-1">---</asp:ListItem>
                                                <asp:ListItem>1</asp:ListItem>
                                                <asp:ListItem>2</asp:ListItem>
                                                <asp:ListItem>3</asp:ListItem>
                                                <asp:ListItem>4</asp:ListItem>
                                                <asp:ListItem>5</asp:ListItem>
                                                <asp:ListItem>6</asp:ListItem>
                                                <asp:ListItem>7</asp:ListItem>
                                                <asp:ListItem>8</asp:ListItem>
                                               
                                                </asp:DropDownList>
                                           
                                           </div>
              
                         <div class="form-group has-error" >
                                      

                                            </div>
                                               
                                                   <div class="form-group">
                                            <label>ساعت آغاز اطلاع رسانی
                                            </label>
                                          
                                            <uc2:UC_TimePicker ID="StartTimePicker" runat="server" />
                                         
                                           </div>  
                                         

                                                <div class="form-group has-error">
                             
                                            <label>فاصله زمانی هر تکرار</label>
                                                     <asp:DropDownList ID="cmbFrequencyInHour" runat="server" 
                                               CssClass="form-control">
                                                <asp:ListItem Value="-1">---</asp:ListItem>
                                                <asp:ListItem Value="1">هر یک ساعت</asp:ListItem>
                                                <asp:ListItem Value="2">هر دو ساعت</asp:ListItem>
                                                <asp:ListItem Value="3">هر سه ساعت</asp:ListItem>
                                              
                                               
                                                </asp:DropDownList>
                                                                            

                                        </div>
                                                <div class="form-group">

                                               <div class="panel panel-default" >
                                                <div class="panel-heading">
                                                <label>نوع اطلاع رسانی</label>
                                                </div>
                                                <div class="panel-body" style="max-height: 200px;">
                                             
                                                   <div class='checkbox'> <label> <input type='checkbox' id="chkbxSendSMS" runat="server" />
                                                    ارسال پیامک
                                                   
                                                    </label></div>

                                                       <div class='checkbox'> <label> <input type='checkbox' id="chkbxCallTelephone" runat="server" />
                                                    تماس تلفنی

                                                </label></div> 
                                                  <div class='checkbox'> <label> <input type='checkbox' id="chkbxVoiceMessage" runat="server" />
                                                   پیامک صوتی

                                                </label></div> 
                                                 
                                                   </div>
                                               

                                               

                                            </div>
                                             
                                      
                                          </div>

                                     
                 
       
                                           <div class="form-group">
                                            <div  class="panel panel-default" >
                                                <div class="panel-heading">
                                                <label>نوع گردش کار</label>
                                                </div>
                                                 <div class="radio">

                                                <asp:RadioButton Enabled ="false"  ID="rdoNewDeposit" Checked="true" GroupName="rdoForDeposit" Text="تجهیز منابع" runat="server" />
                                            
                                            </div>
                                            <div class="radio">
                                                 <asp:RadioButton ID="rdoGetDeposit"  GroupName="rdoForDeposit" Enabled ="false" Text="تخصیص منابع" runat="server" />
                                            
                                               
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
    
    <asp:HiddenField ID="hdnWarningIntervalTypeNew" runat="server" />
    <asp:HiddenField ID="hdnWarningIntervalTypeEdit" runat="server" />
     
</asp:Content>
