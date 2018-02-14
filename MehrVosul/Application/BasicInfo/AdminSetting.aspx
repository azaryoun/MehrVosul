<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/MasterPage/SmartIntergace_Master.Master" CodeBehind="AdminSetting.aspx.vb" Inherits="MehrVosul.AdminSetting" %>

<%@ Register Src="../../UserControl/Bootstrap_Panel.ascx" TagName="Bootstrap_Panel" TagPrefix="uc1" %>


<%@ Register Src="../../UserControl/UC_TimePicker.ascx" TagName="UC_TimePicker" TagPrefix="uc2" %>


<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script language="javascript" type="text/javascript">


        function StartthePage() {
            return true;
        }

        function SaveOperation_Validate() {

            var txtMassiveFilePeriod = document.getElementById("<%=txtMassiveFilePeriod.ClientID%>");
            var txtDueDateFilePeroid = document.getElementById("<%=txtDueDateFilePeroid.ClientID%>");
            var txtDueDateRecivedPeriod = document.getElementById("<%=txtDueDateRecivedPeriod.ClientID%>");
            var txtDeferredPeriod = document.getElementById("<%=txtDeferredPeriod.ClientID%>");
            var txtDoubtfulPaidPeriod = document.getElementById("<%=txtDoubtfulPaidPeriod.ClientID%>");
            var txtMassiveFileAmount = document.getElementById("<%=txtMassiveFileAmount.ClientID%>");
            var txtDueDateFileAmount = document.getElementById("<%=txtDueDateFileAmount.ClientID%>");
            var txtDueDateRecivedAmount = document.getElementById("<%=txtDueDateRecivedAmount.ClientID%>");
            var txtDeferredAmount = document.getElementById("<%=txtDeferredAmount.ClientID%>");
            var txtDoubtfulPaidAmount = document.getElementById("<%=txtDoubtfulPaidAmount.ClientID%>");

            if (trimall(txtMassiveFilePeriod.value) == "") {
                alert("بازه پرونده های کلان را وارد نمایید");
                txtMassiveFilePeriod.focus();
                return false;
            }


            if (trimall(txtDueDateFilePeroid.value) == "") {
                alert("بازه پرونده های سررسید را وارد نمایید");
                txtDueDateFilePeroid.focus();
                return false;
            }

            if (trimall(txtDueDateRecivedPeriod.value) == "") {
                alert("بازه پرونده های سررسید گذشته را وارد نمایید");
                txtDueDateRecivedPeriod.focus();
                return false;
            }

            if (trimall(txtDeferredPeriod.value) == "") {
                alert("بازه پرونده های معوق را وارد نمایید");
                txtDeferredPeriod.focus();
                return false;
            }


            if (trimall(txtDoubtfulPaidPeriod.value) == "") {
                alert("IP بازه پرونده های مشکوک الوصول را وارد نمایید");
                txtDoubtfulPaidPeriod.focus();
                return false;
            }


            if (trimall(txtMassiveFileAmount.value) == "") {
                alert("مبلغ پرونده های کلان را وارد نمایید");
                txtMassiveFileAmount.focus();
                return false;
            }

            if (trimall(txtDueDateFileAmount.value) == "") {
                alert("مبلغ پرونده های سررسید را وارد نمایید");
                txtDueDateFileAmount.focus();
                return false;
            }


            if (trimall(txtDueDateRecivedAmount.value) == "") {
                alert("مبلغ پرونده های سررسید گذشته را وارد نمایید");
                txtDueDateRecivedAmount.focus();
                return false;
            }


            if (trimall(txtDeferredAmount.value) == "") {
                alert("مبلغ پرونده های معوق را وارد نمایید");
                txtDeferredAmount.focus();
                return false;
            }

            if (trimall(txtDoubtfulPaidAmount.value) == "") {
                alert("مبلغ پرونده های مشکوک الوصول را وارد نمایید");
                txtDoubtfulPaidAmount.focus();
                return false;
            }


            return true;
        }




    </script>



</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">


    <uc1:Bootstrap_Panel ID="Bootstrap_Panel1" runat="server" />
  

            <div class="panel panel-default">
                <div class="panel-heading">
                    <asp:Label ID="lblInnerPageTitle" runat="server" Text=""></asp:Label>
                </div>
                <div class="panel-body">
                    <div class="row">
                        <div class="col-md-6">


                            <div class="form-group has-error">
                                <label>بازه پرونده های کلان</label>

                                <asp:TextBox ID="txtMassiveFilePeriod" runat="server" CssClass="form-control" MaxLength="50" placeholder="بازه پرونده های کلان را وارد کنید"></asp:TextBox>

                            </div>

                            <div class="form-group has-error">
                                <label>بازه پرونده های سررسید</label>

                                <asp:TextBox ID="txtDueDateFilePeroid" runat="server" CssClass="form-control" MaxLength="50" placeholder="بازه پرونده های سررسید را وارد کنید"></asp:TextBox>

                            </div>

                            <div class="form-group has-error">
                                <label>بازه پرونده های سررسید گذشته </label>

                                <asp:TextBox ID="txtDueDateRecivedPeriod" runat="server" CssClass="form-control" MaxLength="50" placeholder="بازه پرونده های سررسید گذشته را وارد کنید"></asp:TextBox>

                            </div>

                            <div class="form-group has-error">
                                <label>بازه پرونده های معوق</label>

                                <asp:TextBox ID="txtDeferredPeriod" runat="server" CssClass="form-control" MaxLength="50" placeholder="بازه پرونده های معوق را وارد کنید"></asp:TextBox>

                            </div>

                            <div class="form-group has-error">
                                <label>بازه پرونده های مشکوک الوصول</label>

                                <asp:TextBox ID="txtDoubtfulPaidPeriod" runat="server" CssClass="form-control" MaxLength="50" placeholder="IP بازه پرونده های مشکوک الوصول را وارد کنید"></asp:TextBox>

                            </div>

                        </div>

                

        
                    <div class="col-md-6">

                        <div class="form-group has-error">
                            <label>مبلغ پرونده های کلان</label>

                            <asp:TextBox ID="txtMassiveFileAmount" runat="server" CssClass="form-control" MaxLength="50" placeholder="مبلغ پرونده های کلان را وارد کنید"></asp:TextBox>

                        </div>

                        <div class="form-group has-error">
                            <label>مبلغ پرونده های سررسید</label>

                            <asp:TextBox ID="txtDueDateFileAmount" runat="server" CssClass="form-control" MaxLength="50" placeholder="مبلغ پرونده های سررسید را وارد کنید"></asp:TextBox>

                        </div>

                        <div class="form-group has-error">
                            <label>مبلغ پرونده های سررسید گذشته </label>

                            <asp:TextBox ID="txtDueDateRecivedAmount" runat="server" CssClass="form-control" MaxLength="50" placeholder="مبلغ پرونده های سررسید گذشته را وارد کنید"></asp:TextBox>

                        </div>

                        <div class="form-group has-error">
                            <label>مبلغ پرونده های معوق</label>

                            <asp:TextBox ID="txtDeferredAmount" runat="server" CssClass="form-control" MaxLength="50" placeholder="مبلغ پرونده های معوق را وارد کنید"></asp:TextBox>

                        </div>

                        <div class="form-group has-error">
                            <label>مبلغ پرونده های مشکوک الوصول</label>

                            <asp:TextBox ID="txtDoubtfulPaidAmount" runat="server" CssClass="form-control" MaxLength="50" placeholder="مبلغ پرونده های مشکوک الوصول را وارد کنید"></asp:TextBox>

                        </div>


                    </div>

                </div>
            </div>
     </div>



    <asp:HiddenField ID="hdnAction" runat="server" />

</asp:Content>
