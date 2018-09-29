<%@ Page Title="" Language="vb" EnableEventValidation="true" AutoEventWireup="false" MasterPageFile="~/MasterPage/SmartIntergace_Master.Master" CodeBehind="HandyFollowAssign.aspx.vb" Inherits="MehrVosul.HandyFollowAssign" %>

<%@ Register Src="../../../UserControl/Bootstrap_Panel.ascx" TagName="Bootstrap_Panel" TagPrefix="uc1" %>
<%@ Register Src="../../../UserControl/UC_TimePicker.ascx" TagName="UC_TimePicker" TagPrefix="uc3" %>
<%@ Register Src="../../../UserControl/Bootstrap_PersianDateTimePicker.ascx" TagName="Bootstrap_PersianDateTimePicker" TagPrefix="uc4" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script lang="javascript" type="text/javascript">


        function StartthePage() {


            return true;
        }

        function CheckDataEnter() {

            var txtNotPiadDurationDayFrom = document.getElementById("<%=txtNotPiadDurationDayFrom.ClientID%>");
            var txtNotPiadDurationDayTo = document.getElementById("<%=txtNotPiadDurationDayTo.ClientID%>");

            var cmbDeferredPeriod = document.getElementById("<%=cmbDeferredPeriod.ClientID%>");
            var cmbBranch = document.getElementById("<%=cmbBranch.ClientID%>");

            if (cmbDeferredPeriod.options[cmbDeferredPeriod.selectedIndex].value == -1) {

                if (trimall(txtNotPiadDurationDayFrom.value) == "") {
                    alert("بازه از  را وارد نمایید");
                    txtNotPiadDurationDayFrom.focus();
                    return false;
                }


                if (trimall(txtNotPiadDurationDayTo.value) == "") {
                    alert("بازه تا را وارد نمایید");
                    txtNotPiadDurationDayTo.focus();
                    return false;
                }
            }


            if (cmbBranch.options[cmbBranch.selectedIndex].value == 0) {
                alert("کد شعبه را مشخص نمائید", "خطا");
                cmbBranch.focus();
                return false;
            }


            return true;
        }




        function SaveOperation_Validate() {




            var cmbAssignType = document.getElementById("<%=cmbAssignType.ClientID%>");



            if (cmbAssignType.options[cmbAssignType.selectedIndex].value == 2) {

                var tblNumbers_Name = "<%=tblNumbers.ClientID %>";
                var tblNumbers = document.getElementById(tblNumbers_Name);

                var i;
                var flg = false;
                for (i = 1; i < tblNumbers.rows.length; i++) {
                    if (tblNumbers.rows[i].cells[1].firstChild.checked) {
                        flg = true;
                        break;
                    }
                }

                if (flg == false) {

                    alert("موردی جهت تخصیص انتخاب نشده است");
                    return false;
                }


                ReadytoAssign();

            }
            else {

                var tblNumbers_Name = "<%=tblNumbers.ClientID %>";
                var tblNumbers = document.getElementById(tblNumbers_Name);

                var i;
                var flg = false;
                for (i = 1; i < tblNumbers.rows.length; i++) {
                    if (tblNumbers.rows[i].cells[9].firstChild.options[0].selected != true) {
                        flg = true;
                        break;
                    }
                }

                if (flg == false) {

                    alert("کارشناسی جهت پیگیری مشخص نشده است ");
                    return false;
                }
            }

            return true;
        }



        function chkSelectAll_Click() {



            var chkSelectAll = document.getElementById("chkSelectAll");

            var checkBoxes = document.getElementsByTagName("input");
            var checkedCount = 0;

            for (var i = 0; i < checkBoxes.length; i++) {
                if (chkSelectAll.checked) {
                    checkBoxes[i].checked = 1;

                }
                else {
                    checkBoxes[i].checked = 0;
                    ;
                }
            }


        }

        function ReadytoAssign() {



            var tblNumbers_Name = "ContentPlaceHolder1_tblNumbers";
            var tblNumbers = document.getElementById(tblNumbers_Name);

            var hdnSelected_Name = "<%=hdnSelected.ClientID%>";
            var hdnSelected = document.getElementById(hdnSelected_Name);


            var i;
            var flg = false;
            for (i = 1; i < tblNumbers.rows.length; i++) {
                if (tblNumbers.rows[i].cells[1].firstChild.checked) {
                    hdnSelected.value = hdnSelected.value + tblNumbers.rows[i].cells[1].firstChild.name + ',';
                }
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
                            <div class="form-group">
                                <label>استان</label>
                                <asp:ObjectDataSource ID="odcProvince" runat="server"
                                    OldValuesParameterFormatString="original_{0}" SelectMethod="GetData"
                                    TypeName="BusinessObject.dstBranchTableAdapters.spr_ProvinceList_SelectTableAdapter"></asp:ObjectDataSource>


                                <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                    <ContentTemplate>
                                        <asp:DropDownList ID="cmbProvince" runat="server" CssClass="form-control" AutoPostBack="True" DataSourceID="odcProvince" DataTextField="Province" DataValueField="ID">
                                        </asp:DropDownList>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </div>
                        </div>
                        <div class="col-md-6">
                            <div class="form-group">
                                <label>شعبه</label>
                                <asp:ObjectDataSource ID="odsBranch" runat="server"
                                    OldValuesParameterFormatString="original_{0}" SelectMethod="GetData"
                                    TypeName="BusinessObject.dstBranchTableAdapters.spr_Branch_List_SelectTableAdapter">
                                    <SelectParameters>
                                        <asp:Parameter DefaultValue="1" Name="Action" Type="Int32" />
                                        <asp:Parameter DefaultValue="-1" Name="ProvinceID" Type="Int32" />
                                    </SelectParameters>
                                </asp:ObjectDataSource>
                                <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                                    <ContentTemplate>
                                        <asp:DropDownList ID="cmbBranch" runat="server" AutoPostBack="True"
                                            CssClass="form-control">
                                        </asp:DropDownList>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </div>
                        </div>
                    </div>

                    <div class="row">


                        <div class="col-md-6">
                            <div class="panel-body" style="max-height: 200px;">
                                <asp:RadioButtonList ID="rdbListSelectType" runat="server">
                                    <asp:ListItem Value="0" Selected="True"> تعداد روز معوق 
                                    </asp:ListItem>
                                    <asp:ListItem Value="1"> مبلغ معوق  </asp:ListItem>
                                </asp:RadioButtonList>
                            </div>
                        </div>

                        <div class="col-md-6">

                            <div class="form-group">
                                <label>بازه معوق</label>
                                <asp:UpdatePanel ID="UpdatePanel6" runat="server">
                                    <ContentTemplate>
                                        <asp:DropDownList ID="cmbDeferredPeriod" runat="server" AutoPostBack="true" CssClass="form-control">
                                            <asp:ListItem Value="-1">---</asp:ListItem>

                                            <asp:ListItem Value="1">سررسید جاری از 1 تا 60</asp:ListItem>

                                            <asp:ListItem Value="2">سررسید گذشته از 61 تا 180</asp:ListItem>

                                            <asp:ListItem Value="3">معوق از 181 تا 540</asp:ListItem>

                                            <asp:ListItem Value="4">مشکوک از 541 به بالا</asp:ListItem>

                                        </asp:DropDownList>
                                    </ContentTemplate>
                                </asp:UpdatePanel>

                            </div>
                        </div>

                    </div>

                    <div class="row">

                        <div class="col-md-6">
                            <div class="form-group">

                                <label>از</label>
                                <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                                    <ContentTemplate>

                                        <asp:TextBox ID="txtNotPiadDurationDayFrom" AutoPostBack="true" runat="server" CssClass="form-control" placeholder="بازه از تعداد روز یا مبلغ معوق را وارد نمایید"></asp:TextBox>

                                    </ContentTemplate>
                                </asp:UpdatePanel>
                              

                            </div>
                        </div>
                        <div class="col-md-6">
                            <div class="form-group">
                                <label>تا</label>
                                <asp:UpdatePanel ID="UpdatePanel4" runat="server">
                                    <ContentTemplate>

                                        <asp:TextBox ID="txtNotPiadDurationDayTo" AutoPostBack="true" runat="server" CssClass="form-control" placeholder="بازه تا تعداد روز یا مبلغ معوق را وارد نمایید"></asp:TextBox>

                                    </ContentTemplate>
                                </asp:UpdatePanel>

                            </div>
                        </div>

                    </div>
                    <div class="row">

                        <div class="col-md-6">

                            <div class="form-group">

                                <div class="form-group">
                                    <label>نوع وام</label><asp:ObjectDataSource ID="odsLoanType" runat="server" OldValuesParameterFormatString="original_{0}" SelectMethod="GetData" TypeName="BusinessObject.dstLoanTypeTableAdapters.spr_LoanType_List_SelectTableAdapter">
                                    </asp:ObjectDataSource>
                                    &nbsp;<asp:DropDownList ID="cmbLoanType" runat="server" CssClass="form-control"
                                        DataSourceID="odsLoanType" DataTextField="LoanType" DataValueField="ID">
                                    </asp:DropDownList>
                                </div>
                                  <br />
                                <asp:LinkButton CssClass="btn btn-success" ID="btnCheckFiles" runat="server" ToolTip="نمایش پرونده"><i class="fa fa-filter fa-x"></i> </asp:LinkButton>

                            </div>
                        </div>
                    </div>
                    <div class="row">

                        <div class="col-md-6">

                            <div class="form-group">

                                <div class="form-group">

                                    <label>نوع تخصیص</label>
                                    <asp:UpdatePanel ID="UpdatePanel5" runat="server">
                                        <ContentTemplate>
                                            <asp:DropDownList ID="cmbAssignType" runat="server" CssClass="form-control" AutoPostBack="True">
                                                <asp:ListItem Value="1" Selected="True">تکی(انتخاب هر ردیف)</asp:ListItem>
                                                <asp:ListItem Value="2">گروهی(انتخاب کل ردیفها)</asp:ListItem>
                                            </asp:DropDownList>
                                        </ContentTemplate>
                                    </asp:UpdatePanel>

                                </div>
                            </div>
                        </div>

                        <div class="col-md-6">

                            <div class="form-group">
                                <label>کارشناس پیگیر</label>
                                <asp:ObjectDataSource ID="odsPerson" runat="server"
                                    OldValuesParameterFormatString="original_{0}" SelectMethod="GetData"
                                    TypeName="BusinessObject.dstUserTableAdapters.spr_User_CheckBranch_SelectTableAdapter">
                                    <SelectParameters>
                                        <asp:Parameter Name="Action" Type="Int32" />
                                        <asp:Parameter Name="BranchID" Type="Int32" />
                                        <asp:Parameter Name="ProvinceID" Type="Int32" />
                                    </SelectParameters>
                                </asp:ObjectDataSource>
                                <asp:UpdatePanel ID="UpdatePanel7" runat="server">
                                    <ContentTemplate>

                                        <asp:DropDownList ID="cmbPerson" runat="server" CssClass="form-control"
                                            DataSourceID="odsPerson" DataTextField="Username" DataValueField="ID" AutoPostBack="True" Enabled="False">
                                        </asp:DropDownList>
                                    </ContentTemplate>
                                </asp:UpdatePanel>

                            </div>
                        </div>

                    </div>



                    <div class="row">
                        <div class="col-md-12">

                            <div class="panel-body" style="max-height: 50px;">

                                <label>
                                    <input type="checkbox" value="" id="chkSelectAll" onclick="return chkSelectAll_Click();" />
                                    انتخاب/عدم انتخاب همه</label>
                                <div class="form-group" runat="server" id="divchklstLoanTypeItems">
                                </div>

                            </div>
                            <table class="table table-bordered table-striped table-condensed" id="tblNumbers" runat="server">

                                <tr style="text-align: center;">

                                    <td class="TableHeader1">ردیف</td>
                                    <td class="TableHeader1"></td>
                                    <td class="TableHeader1">نام و نام خانوادگی(گیرنده تسهیلات)</td>
                                    <td class="TableHeader1">شماره تسهیلات</td>
                                    <td class="TableHeader1">تعداد اقساط</td>
                                    <td class="TableHeader1">مبلغ تسهیلات</td>
                                    <td class="TableHeader1">نوع تسهیلات</td>

                                    <td class="TableHeader1">مبلغ معوق</td>
                                    <td class="TableHeader1">تعدادروزمعوق</td>
                                    <td class="TableHeader1">کارشناس پیگیر</td>

                                </tr>
                            </table>
                        </div>
                    </div>




                </div>

            </div>
        </div>


    </div>




    <asp:HiddenField ID="hdnAction" runat="server" />

    <asp:HiddenField ID="hdnSelected" runat="server" />

</asp:Content>
