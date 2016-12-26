<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/MasterPage/SmartIntergace_Master.Master" CodeBehind="PreWarningIntervalsNew.aspx.vb" Inherits="MehrVosul.PreWarningIntervalsNew" %>
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
        
            var chkbxSendSMS = document.getElementById("<%=chkbxSendSMS.ClientID%>");
            var chkbxVoiceMessage =  document.getElementById("<%=chkbxVoiceMessage.ClientID%>");

            var txtMinimumAmount = document.getElementById("<%=txtMinimumAmount.ClientID%>");
            var divchklstLoanTypeItems = document.getElementById("<%=divchklstLoanTypeItems.ClientID%>");
            
            


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
          

         

            if (isNaN(txtFrom.value) || trimall(txtFrom.value) == "") {
                alert("بازه اطلاع رسانی را وارد نمایید");
                txtFrom.focus();
                return false;
            }

            


      

            if (chkbxSendSMS.checked == false && chkbxVoiceMessage.checked == false) {
                alert("یکی از انواع اطلاع رسانی را مشخص نمایید", "خطا");
               
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

              
                              

     
            

                     
                         <div class="form-group has-error" >
                                       <div class="panel panel-default" >
                                                <div class="panel-heading">
                                               <label>محدوده اطلاع رسانی</label>
                                                </div>
                                                <div class="panel-body" style="max-height: 200px;">

                                                      <div class="form-group input-group input-group-sm">
                                        
                                            <asp:TextBox ID="txtFrom"  CssClass="form-control"  runat="server"  placeholder="محدوده اطلاع رسانی را وارد نمایید" ></asp:TextBox>
                                            <span class="input-group-addon"> روز </span>
                                            </div>

                                            
                                      
                                            </div> 
                                                 
                                                
                                                    
                                        </div>

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

                                                        
                                                    <div class='checkbox'> <label> <input type='checkbox' id="chkbxVoiceMessage" runat="server" />
                                                      پیامک صوتی

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
