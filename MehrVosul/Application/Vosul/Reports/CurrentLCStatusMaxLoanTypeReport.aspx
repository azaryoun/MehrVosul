<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/MasterPage/SmartIntergace_Master.Master" CodeBehind="CurrentLCStatusMaxLoanTypeReport.aspx.vb" Inherits="MehrVosul.CurrentLCStatusMaxLoanTypeReport" %>
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


            var txt_InstallmentCount = document.getElementById("<%=txt_InstallmentCount.ClientID%>");
            if (trimall(txt_InstallmentCount.value) == "") {
                alert("تعداد اقساط معوق را وارد نمایید");
                txt_InstallmentCount.focus();
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

            var txt_InstallmentCount = document.getElementById("<%=txt_InstallmentCount.ClientID%>");
            if (trimall(txt_InstallmentCount.value) == "") {
                alert("تعداد اقساط معوق را وارد نمایید");
                txt_InstallmentCount.focus();
                return false;
            }

            return true;
        }


        function chkCheckAll() {

            var allInputs = document.getElementsByTagName("input");
            var chkSelectAll = document.getElementById("chkSelectAll");


            if (chkSelectAll.checked) {
                for (var i = 0, max = allInputs.length; i < max; i++) {
                    if (allInputs[i].type === 'checkbox')
                        allInputs[i].checked = true;
                }
            }
            else {

                for (var i = 0, max = allInputs.length; i < max; i++) {
                    if (allInputs[i].type === 'checkbox')
                        allInputs[i].checked = false;
                }
            }
        }


        function btnSenSMS_Click() {

         
            var allInputs1 = document.getElementsByTagName("input");


            for (var i = 0, max = allInputs1.length; i < max; i++) {
                if (allInputs1[i].type == 'radio' && allInputs1[i].id == 'ContentPlaceHolder1_rdbText') {
                    var txtMessage = document.getElementById("<%=txtMessage.ClientID%>");
                    if (allInputs1[i].checked == true && trimall(txtMessage.value) == "") {
                        alert("متن دلخواه را وارد نمایید");
                        txtMessage.focus();
                        return false;
                    }
                }

            }


            var allInputs = document.getElementsByTagName("input");
            var chkSelectAll = document.getElementById("chkSelectAll");

            for (var i = 0, max = allInputs.length; i < max; i++) {
                if (allInputs[i].type === 'checkbox')
                    if (allInputs[i].checked == true)
                        return true;


            }

            alert("حداقل یک پرونده باید انتخاب شود");
            return false;
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
                                    </asp:UpdatePanel></div> 
                                  
                                        <div class="form-group has-error">
                                            <label>شعبه ها</label>
                                         
                                             
                                         <div class="panel-body" style="max-height: 200px;overflow-y: scroll;">
                                          <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                                            <ContentTemplate>
                                             
                                                  <label> <input type="checkbox" value="" id="chkBranchSelectAll" onclick="return chkBranchSelectAll_Click();" runat="server"/> انتخاب/عدم انتخاب همه</label> 
                                            
                                                   <div  class="form-group" runat="server" id="divBranches">
                                                   </div></ContentTemplate> </asp:UpdatePanel>
                                                   

                                         </div></div>  </div>
                              

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

                   
                       <div class="form-group has-error">
                                            <label>تعداد اقساط معوق</label>
                                           
                                            <asp:TextBox ID="txt_InstallmentCount"  CssClass="form-control"  
                                                runat="server"  placeholder="تعداد اقساط معوق را وارد نمایید" ></asp:TextBox>
                                              

                                        </div>
                            </div>
                            </div>
                        </div>
                           
                           <div class="panel panel-default" id="divResult" runat="server" visible="false">
                        <div class="panel-heading">
                             گزارش پرونده ها با تعداد اقساط معوق مشخص </div>
                        <div class="panel-body">
                        
                            <div class="table-responsive">
                                <table class="table table-striped table-bordered table-hover" id="tblResult" runat="server">
                                    <thead>
                                        <tr>
                                            <th align="center"><input type="checkbox" value="" onclick="chkCheckAll();" id="chkSelectAll" title ="انتخاب/عدم انتخاب همه"/>  </th>
                                            <th>#</th>
                                            <th>مشتری</th>
                                            <th>نام و نام خانوادگی</th>
                                             <th>تسهیلات</th>
                                              <th>شعبه</th>
                                              <th>همراه</th>
                                           <th>تعداد روز معوق </th>
                                           <th>تاریخ</th>
                                        </tr>
                                    </thead>
                             
                                </table>
                            </div>
                        </div>

                           <div class="panel-body" style="max-height: 200px;">
                                        <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                                            <ContentTemplate>
                                               <asp:RadioButton ID="rdbText" runat="server"
                                                    GroupName="Text" Text=" ارسال متن دلخواه" AutoPostBack="True" 
                                                    Font-Bold="True" Font-Names="B Nazanin" Font-Size="12pt" />
                                                &nbsp;
                                                <asp:RadioButton ID="rdbInterval" runat="server" AutoPostBack="True" 
                                                    Checked="True" Font-Bold="True" Font-Names="B Nazanin" Font-Size="12pt" 
                                                    GroupName="Text" Text=" ارسال متن بر اساس گردش کار " />
                                            </ContentTemplate>
                                        </asp:UpdatePanel>
                                    </div>
                                    <div class="form-group has-error">
                                      
                                            <asp:UpdatePanel ID="UpdatePanel4" runat="server">
                                                <ContentTemplate>
                                                    <asp:TextBox ID="txtMessage" runat="server" AutoPostBack="True" 
                                                        CssClass="form-control" Height="70px" placeholder="متن دلخواه را وارد نمایید" 
                                                        TextMode="MultiLine" Visible="False"></asp:TextBox>
                                                </ContentTemplate>
                                            </asp:UpdatePanel>
                                              

                                    </div> 

                          <div class="form-group has-error">
                                          


                                    <asp:LinkButton CssClass="btn btn-success" ID="btnSendSMS" runat="server" ToolTip="ارسال پیامک"><i class="fa fa-mobile fa-2x"></i> </asp:LinkButton>
  
   
                                    
                                                 </div>
                                        </div>

                                        
                    </div>

                    
                       </div>     

                            
                            </div>
                      
             


     
    <asp:HiddenField ID="hdnAction" runat="server" />

</asp:Content>
