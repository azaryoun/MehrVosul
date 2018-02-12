<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/MasterPage/SmartIntergace_Master.Master" CodeBehind="WarningIntervalsNew.aspx.vb" Inherits="MehrVosul.WarningIntervalsNew" %>
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
            var chkbxIssueNotice = document.getElementById("<%=chkbxIssueNotice.ClientID%>");
            var chkbxSendSMS = document.getElementById("<%=chkbxSendSMS.ClientID%>");
            var chkbxIssueIntroductionLetter = document.getElementById("<%=chkbxIssueIntroductionLetter.ClientID%>");
            var chkbxIssueManifest = document.getElementById("<%=chkbxIssueManifest.ClientID%>");
            var chkbxToBorrower = document.getElementById("<%=chkbxToBorrower.ClientID%>");
            var chkbxToSponsor = document.getElementById("<%=chkbxToSponsor.ClientID%>");
            var txtMinimumAmount = document.getElementById("<%=txtMinimumAmount.ClientID%>");
            var divchklstLoanTypeItems = document.getElementById("<%=divchklstLoanTypeItems.ClientID%>");
            var chkbxVoiceMessage = document.getElementById("<%=chkbxVoiceMessage.ClientID%>");
            


            if (trimall(txtWarningIntervalsName.value) == "") {
                alert("عنوان گردش کار را وارد نمایید");
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
          debugger

            if (cmbFrequencyInDay.options[cmbFrequencyInDay.selectedIndex].value == -1) {
                alert("تعداد اطلاع رسانی در روز را مشخص نمایید", "خطا");
                cmbFrequencyInDay.focus();
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


            if (cmbFrequencyInHour.options[cmbFrequencyInHour.selectedIndex].value == -1) {
                alert("فاصله زمانی هر تکرار را مشخص نمائید", "خطا");
                cmbFrequencyInHour.focus();
                return false;
            }

            if (chkbxCallTelephone.checked == false && chkbxIssueNotice.checked == false && chkbxSendSMS.checked == false && chkbxIssueIntroductionLetter.checked == false && chkbxIssueManifest.checked == false && chkbxVoiceMessage.checked == false) {
                alert("یکی از انواع اطلاع رسانی را مشخص نمایید", "خطا");
               
                return false;
            }


            if (chkbxCallTelephone.checked == true)
            {
                if (chkbxIssueNotice.checked == true || chkbxSendSMS.checked == true || chkbxIssueIntroductionLetter.checked == true || chkbxIssueManifest.checked == true) {
                    alert("امکان انتخاب نوع دیگر اطلاع رسانی با تماس تلفنی وجود ندارد", "خطا");

                    return false;
                }
            }
            
            if (chkbxToBorrower.checked == false && chkbxToSponsor.checked == false) {
                alert("یکی از گیرنگان پیامک را مشخص نمایید", "خطا");

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
                                <div class="col-md-6">
                                   
                                 
                                        <div class="form-group has-error">
                                            <label>عنوان</label>
                                          
                                            <asp:TextBox ID="txtWarningIntervalsName" runat="server" cssclass="form-control" MaxLength="50" placeholder="عنوان گردش کار را وارد کنید"></asp:TextBox>
                                            
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
                                          <div class="form-group">
                                            <label>مبلغ تسهیلات</label>
                                             
                                            <asp:TextBox ID="txtLoanAmount" runat="server" cssclass="form-control" MaxLength="50" placeholder="0">0</asp:TextBox>
                                            
                                        </div>

                                            <div class="form-group">
                                            <label>مبلغ اقساط</label>
                                          
                                            <asp:TextBox ID="txtInstalmentAmount" runat="server" cssclass="form-control" MaxLength="50" placeholder="0">0</asp:TextBox>
                                            
                                        </div>

                                            <div class="form-group">
                                            <label>حداقل مبلغ(ریال)</label>
                                          
                                            <asp:TextBox ID="txtMinimumAmount" runat="server" cssclass="form-control" MaxLength="50" placeholder="0">0</asp:TextBox>
                                            
                                        </div>

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
              
                              

     
                       </div>    
                       
                       <div class="col-md-6">

                     
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
                                                  <div class='checkbox'> <label> <input type='checkbox' id="chkbxIssueIntroductionLetter" runat="server" />
                                                    دعوت نامه

                                                </label></div> 
                                                   <div class='checkbox'> <label> <input type='checkbox' id="chkbxIssueNotice" runat="server" />
                                                      اخطاریه

                                                </label></div> 
                                                  <div class='checkbox'> <label> <input type='checkbox' id="chkbxIssueManifest" runat="server" />
                                                      اظهارنامه

                                                </label></div> 
                                                   

                                                   </div>
                                               

                                               

                                            </div>
                                             
                                      
                                          </div>

                                     
                 
       
                                           <div class="form-group">
                                            <div class="panel panel-default" >
                                                <div class="panel-heading">
                                                <label>گیرندگان پیام</label>
                                                </div>
                                                <div class="panel-body" style="max-height: 200px;">

                                                      <div class='checkbox'> <label> <input type='checkbox' id="chkbxToBorrower" runat="server" />
                                                      ارسال به وام گیرنده

                                                </label></div> 
                                                      <div class='checkbox'> <label> <input type='checkbox' id="chkbxToSponsor" runat="server" />
                                                      ارسال به ضامن(ها)

                                                </label></div> 

                                       
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
     
</asp:Content>
