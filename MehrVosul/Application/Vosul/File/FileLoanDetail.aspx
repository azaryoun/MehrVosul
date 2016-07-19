<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/MasterPage/SmartIntergace_Master.Master" CodeBehind="FileLoanDetail.aspx.vb" Inherits="MehrVosul.FileLoanDetail" %>
<%@ Register src="../../../UserControl/Bootstrap_Panel.ascx" tagname="Bootstrap_Panel" tagprefix="uc1" %>
<%@ Register src="../../../UserControl/UC_TimePicker.ascx" tagname="UC_TimePicker" tagprefix="uc3" %>
<%@ Register src="../../../UserControl/Bootstrap_PersianDateTimePicker.ascx" tagname="Bootstrap_PersianDateTimePicker" tagprefix="uc4" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script language="javascript" type="text/javascript">


        function StartthePage() {
            return true;
        }

        function DisplayOperation_Validate() {


            return true;
        }


        function ExcelOperation_Validate() {



            return true;
        }

        function btnLoanLogDetail_ClientClick(Pkey) {
            var hdnAction_Name = "<%=hdnAction.ClientID%>";
            var hdnAction = document.getElementById(hdnAction_Name);
            hdnAction.value = "E;" + Pkey;
            window.document.forms[0].submit();
            return false;
        }


        function btnLoanLogSummary_ClientClick(Pkey) {
            var hdnAction_Name = "<%=hdnAction.ClientID%>";
            var hdnAction = document.getElementById(hdnAction_Name);
            hdnAction.value = "M;" + Pkey;
            window.document.forms[0].submit();
            return false;
        }

        function btnSponsor_ClientClick(Pkey) {
            var hdnAction_Name = "<%=hdnAction.ClientID%>";
            var hdnAction = document.getElementById(hdnAction_Name);
            hdnAction.value = "S;" + Pkey;
            window.document.forms[0].submit();
            return false;
        }

        function btnWhiteList_ClientClick(Pkey) {
            var hdnAction_Name = "<%=hdnAction.ClientID%>";
            var hdnAction = document.getElementById(hdnAction_Name);
            hdnAction.value = "W;" + Pkey;
            window.document.forms[0].submit();
            return false;
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

                      
                           <div class="panel panel-default" id="divResult" runat="server">
                     
                        <div class="panel-body">
                            <div class="table-responsive">
                                <table class="table table-striped table-bordered table-hover" id="tblResult" runat="server">
                                    <thead>
                                        <tr>
                                            <th>#</th>
                                            <th>شماره وام</th>
                                            <th>تاریخ وام</th>
                                            <th>نوع وام</th>
                                            <th>مبلغ وام</th>
                                            <th>تعداد روز معوقه</th>
                                            <th>تعداد کل اقساط</th>
                                            <th>تعداداقساط پرداخت شده</th>
                                            <th>تعداداقساط معوقه</th>
                                            <th>ضامن ها</th>
                                            <th>هزینه اطلاع رسانی</th>
                                            <th>نامه ها</th>
                                            <th>لیست سفید</th>
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
