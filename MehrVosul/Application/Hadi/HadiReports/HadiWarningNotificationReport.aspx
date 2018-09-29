<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/MasterPage/SmartIntergace_Master.Master" CodeBehind="HadiWarningNotificationReport.aspx.vb" Inherits="MehrVosul.HadiWarningNotificationReport" %>

<%@ Register Src="../../../UserControl/Bootstrap_Panel.ascx" TagName="Bootstrap_Panel" TagPrefix="uc1" %>
<%@ Register Src="../../../UserControl/UC_TimePicker.ascx" TagName="UC_TimePicker" TagPrefix="uc3" %>
<%@ Register Src="../../../UserControl/Bootstrap_PersianDateTimePicker.ascx" TagName="Bootstrap_PersianDateTimePicker" TagPrefix="uc4" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script language="javascript" type="text/javascript">


        function StartthePage() {
            var divContactNo = document.getElementById("divContactNo");
            var cmbNotification = document.getElementById("<%=cmbNotification.clientID%>");

            if (cmbNotification.selectedIndex == 0 || cmbNotification.selectedIndex == 3 || cmbNotification.selectedIndex == 4 || cmbNotification.selectedIndex == 5)
                divContactNo.style.display = "none";
            else
                divContactNo.style.display = "";


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

        function cmbNotification_onchange() {
            var divContactNo = document.getElementById("divContactNo");
            var cmbNotification = document.getElementById("<%=cmbNotification.clientID%>");
            var txtReciverNo = document.getElementById("<%=txtReciverNo.clientID%>");

            txtReciverNo.innerText = "";

            if (cmbNotification.selectedIndex == 0 || cmbNotification.selectedIndex == 3 || cmbNotification.selectedIndex == 4 || cmbNotification.selectedIndex == 5)
                divContactNo.style.display = "none";
            else
                divContactNo.style.display = "";


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
                                <div class="panel panel-default">
                                    <div class="panel-heading">
                                        <label>تاریخ</label>
                                    </div>
                                    <div class="panel-body" style="max-height: 200px;">


                                        <uc4:Bootstrap_PersianDateTimePicker ID="Bootstrap_PersianDateTimePicker_From"
                                            runat="server" />


                                        <uc4:Bootstrap_PersianDateTimePicker ID="Bootstrap_PersianDateTimePicker_To"
                                            runat="server" />




                                    </div>



                                </div>
                            </div>

                            <div class="form-group">
                                <label>
                                    پرونده
                                            
                                </label>

                                <asp:TextBox ID="txtFile" runat="server" CssClass="form-control" MaxLength="50"
                                    placeholder="شماره پرونده را وارد کنید"></asp:TextBox>

                            </div>

                            <div class="form-group">
                                <label>گیرندگان پیام</label>
                                <asp:DropDownList ID="cmbReceiver" runat="server" CssClass="form-control">
                                    <asp:ListItem Value="0">(همه)</asp:ListItem>
                                    <asp:ListItem Value="1">وام گیرنده</asp:ListItem>
                                    <asp:ListItem Value="2">ضامن</asp:ListItem>

                                </asp:DropDownList>

                            </div>

                            <div class="form-group">
                                <div class="radio">
                                    <asp:RadioButtonList ID="rdbReportType" runat="server">
                                        <asp:ListItem Value="0">خلاصه</asp:ListItem>
                                        <asp:ListItem Value="1" Selected="True">جزئیات</asp:ListItem>
                                    </asp:RadioButtonList>
                                </div>
                            </div>

                        </div>

                        <div class="col-md-6">



                            <div class="form-group">
                                <label>نوع اطلاع رسانی</label>
                                <asp:DropDownList ID="cmbNotification" runat="server" CssClass="form-control">
                                    <asp:ListItem Value="0">(همه)</asp:ListItem>
                                    <asp:ListItem Value="1">ارسال پیامک</asp:ListItem>
                                    <asp:ListItem Value="6">پیامک صوتی</asp:ListItem>
                                </asp:DropDownList>

                            </div>


                            <div class="form-group" id="divContactNo" style="display: none">
                                <label>شماره تماس </label>
                                <asp:TextBox ID="txtReciverNo" MaxLength="50" runat="server" CssClass="form-control" placeholder="شماره تماس"></asp:TextBox>

                            </div>

                            <div class="form-group">
                                <label>نوع اخطار</label>
                                <asp:ObjectDataSource ID="odsWarningType" runat="server"
                                    OldValuesParameterFormatString="original_{0}" SelectMethod="GetData"
                                    TypeName="BusinessObject.dstWarningIntervalsTableAdapters.spr_WarningIntervals_List_SelectTableAdapter"></asp:ObjectDataSource>

                                <asp:DropDownList ID="cmbWarningType" runat="server" CssClass="form-control"
                                    DataSourceID="odsWarningType" DataTextField="WarniningTitle"
                                    DataValueField="ID">
                                </asp:DropDownList>

                            </div>
                            <div class="form-group">
                                <label>استان</label>
                                <asp:ObjectDataSource ID="odcProvince" runat="server"
                                    OldValuesParameterFormatString="original_{0}" SelectMethod="GetData"
                                    TypeName="BusinessObject.dstBranchTableAdapters.spr_ProvinceList_SelectTableAdapter"></asp:ObjectDataSource>


                                <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                    <ContentTemplate>
                                        <asp:DropDownList ID="cmbProvince" runat="server" CssClass="form-control"
                                            DataSourceID="odcProvince" DataTextField="Province"
                                            DataValueField="ID" AutoPostBack="True">
                                        </asp:DropDownList>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </div>
                            <div class="panel-heading">
                                <label>شعبه ها</label>
                            </div>

                            <div class="panel-body" style="max-height: 200px; overflow-y: scroll;">
                                <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                                    <ContentTemplate>
                                        <label>
                                            <input type="checkbox" value="" id="chkBranchSelectAll" onclick="return chkBranchSelectAll_Click();" runat="server" />
                                            انتخاب/عدم انتخاب همه</label>

                                        <div class="form-group" runat="server" id="divBranches">
                                        </div>
                                    </ContentTemplate>
                                </asp:UpdatePanel>


                            </div>
                        </div>
                    </div>
                </div>
            </div>

            <div class="panel panel-default" id="divResult" runat="server" visible="false">
                <div class="panel-heading">
                    گزارش اطلاع رسانی
                </div>
                <div class="panel-body">
                    <div class="table-responsive">
                        <table class="table table-striped table-bordered table-hover" visible="false" id="tblResult" runat="server">
                            <thead>
                                <tr>
                                    <th>#</th>
                                    <th>شماره موبایل</th>
                                     <th>وام</th>
                                     <th>نوع اطلاع رسانی</th>                                            
                                    <th>زمان ارسال</th>
                                    <th>شعبه</th>
                                     <th>شماره مشتری</th>
                                      <th>متن ارسالی</th>
                                </tr>
                            </thead>

                        </table>

                          <table class="table table-striped table-bordered table-hover" visible="false" id="tblResultCustomer" runat="server">
                            <thead>
                                <tr>
                                    <th>#</th>
                                    <th>شماره موبایل</th>
                                     <th>وام</th>
                                     <th>نوع اطلاع رسانی</th>                                            
                                    <th>زمان ارسال</th>
                                    <th>شعبه</th>
                                     <th>شماره مشتری</th>
                                     <th>متن</th>
                                     <th>نام مشتری</th>
                                </tr>
                            </thead>

                        </table>
                        <table class="table table-striped table-bordered table-hover" id="tblResult2" visible="false" runat="server">
                            <thead>
                                <tr>
                                    <th>#</th>
                                    <th>شعبه</th>
                                    <th>تعداد</th>

                                </tr>
                            </thead>

                        </table>




                    </div>
                </div>
            </div>





        </div>


    </div>




    <asp:HiddenField ID="hdnAction" runat="server" />

</asp:Content>
