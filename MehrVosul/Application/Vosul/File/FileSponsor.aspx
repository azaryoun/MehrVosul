<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/MasterPage/SmartIntergace_Master.Master" CodeBehind="FileSponsor.aspx.vb" Inherits="MehrVosul.FileSponsor" %>
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

        function btnSponsor_ClientClick(Pkey) {
            var hdnAction_Name = "<%=hdnAction.ClientID%>";
            var hdnAction = document.getElementById(hdnAction_Name);
            hdnAction.value = "S;" + Pkey;
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
                                            <th>شماره مشتری</th>
                                            <th>نام و نام خانوادگی</th>
                                            <th>شماره موبایل</th>
                                            <th>آدرس</th>
                                            <th>اسناد تضمینی</th>

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
