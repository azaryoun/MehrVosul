<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/MasterPage/SmartIntergace_Master.Master" CodeBehind="HandyFollowAssign.aspx.vb" Inherits="MehrVosul.HandyFollowAssign" %>

<%@ Register Src="../../../UserControl/Bootstrap_Panel.ascx" TagName="Bootstrap_Panel" TagPrefix="uc1" %>
<%@ Register Src="../../../UserControl/UC_TimePicker.ascx" TagName="UC_TimePicker" TagPrefix="uc3" %>
<%@ Register Src="../../../UserControl/Bootstrap_PersianDateTimePicker.ascx" TagName="Bootstrap_PersianDateTimePicker" TagPrefix="uc4" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script language="javascript" type="text/javascript">


        function StartthePage() {


            return true;
        }

        function CheckDataEnter() {

            var txtNotPiadDurationDayFrom = document.getElementById("<%=txtNotPiadDurationDayFrom.ClientID%>");
            var txtNotPiadDurationDayTo = document.getElementById("<%=txtNotPiadDurationDayTo.ClientID%>");

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


            return true;
        }




        function SaveOperation_Validate() {



            var txtNotPiadDurationDayFrom = document.getElementById("<%=txtNotPiadDurationDayFrom.ClientID%>");
            var txtNotPiadDurationDayTo = document.getElementById("<%=txtNotPiadDurationDayTo.ClientID%>");

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

            var tblNumbers_Name = "<%=tblNumbers.ClientID %>";
            var tblNumbers = document.getElementById(tblNumbers_Name);


            debugger
            var i;
            var bf = false;
            for (i = 1; i <= tblNumbers.rows.length - 1; i++) {

                var cmbCol = tblNumbers.rows[i].cells[8].firstChild;
                if (cmbCol.options == null)
                    continue;

                if (cmbCol.options[cmbCol.selectedIndex].value != -1) {
                    bf = true;
                    break;
                }

            }

            if (bf == false) {



                alert("پرونده ایی برای تخصیص مشخص نشده است");
                return false;


            }

            return true;

        }


            <%--<%--     if (parseInt(txtNotPiadDurationDay.value) < 60) {
                {
                    alert("تعداد روز تاخیر بایستی از 60 بیشتر باشد");
                    txtNotPiadDurationDay.focus();
                    return false;
                }

            }

            var cmbPerson = document.getElementById("<%=cmbPerson.ClientID%>");


            if (cmbPerson.options[cmbPerson.selectedIndex].value == -1) {
                {
                    alert("کارشناس پیگیر را مشخص نمایید");
                    cmbPerson.focus();
                    return false;
                }

            }
           var divchklstAssignFiles = document.getElementById("<%=divchklstAssignFiles.ClientID%>");

            var divtmp;
            if (divchklstAssignFiles != null) {
                divtmp = divchklstAssignFiles.firstChild;
            }
         

            var boolChecked = false;
       
            while (divtmp) {
                var chktmp = divchklstAssignFiles.firstChild.nextSibling.firstChild.nextSibling;
                if (chktmp.checked) {
                    boolChecked = true;
                    break;
                }

                divtmp = divtmp.nextSibling;
            }

            if (!boolChecked) {
                alert("حداقل یک فایل باید انتخاب شود");
                return false;

            }--%>



          


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
                            <div class="form-group">

                                <label >از</label>
                                <asp:UpdatePanel ID="UpdatePanel5" runat="server">
                                    <ContentTemplate>

                                        <asp:TextBox ID="txtNotPiadDurationDayFrom" AutoPostBack="true" runat="server" CssClass="form-control" placeholder="بازه از تعداد روز معوق را واردنمایید"></asp:TextBox>

                                    </ContentTemplate>
                                </asp:UpdatePanel>


                            </div>
                        </div>
                        <div class="col-md-6">
                            <div class="form-group">
                                <label>تا</label>
                                <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                                    <ContentTemplate>

                                        <asp:TextBox ID="txtNotPiadDurationDayTo" AutoPostBack="true" runat="server" CssClass="form-control" placeholder="بازه تا تعداد روز معوق را واردنمایید"></asp:TextBox>

                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </div>
                        </div>

                    </div>

                    <div class="row">

                        <div class="col-md-12">
                            <div class="form-group">

                                <br />

                                <asp:LinkButton CssClass="btn btn-success" OnClientClick="return CheckDataEnter();" ID="btnCheckFiles" runat="server" ToolTip="نمایش پرونده"><i class="fa fa-filter fa-x"></i> </asp:LinkButton>


                            </div>
                        </div>
                    </div>

              


                    <div class="row">
                        <table class="table table-bordered table-striped table-condensed" id="tblNumbers" runat="server">
                            <tr style="text-align: center;">

                                <td class="TableHeader1">ردیف</td>
                                <td class="TableHeader1">نام و نام خانوادگی(گیرنده تسهیلات)</td>
                                <td class="TableHeader1">شماره تسهیلات</td>
                                <td class="TableHeader1">تعداد اقساط</td>
                                <td class="TableHeader1">مبلغ تسهیلات</td>
                                <td class="TableHeader1">نوع تسهیلات</td>
                                <td class="TableHeader1">تعداد اقساط معوق</td>
                                <td class="TableHeader1">مبلغ معوق</td>
                                <td class="TableHeader1">کارشناس پیگیر</td>
                             
                            </tr>
                        </table>
                    </div>

                          <div class="row" style="visibility: hidden;">
                        <div class="col-md-6">
                            <div class="form-group">



                                <asp:UpdatePanel ID="UpdatePanel4" runat="server">
                                    <ContentTemplate>

                                        <div class="panel-heading">
                                            <label>پرونده های معوق</label>
                                            <label>
                                                (شماره مشتری- شماره وام- نام مشتری - تعداد روز معوق)</label>
                                        </div>
                                        <div class="panel-body" style="max-height: 200px; overflow-y: scroll;">
                                            <div class="form-group" runat="server" id="divchklstAssignFiles">
                                            </div>
                                        </div>
                                    </ContentTemplate>
                                </asp:UpdatePanel>



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


                                <asp:DropDownList ID="cmbPerson" runat="server" CssClass="form-control"
                                    DataSourceID="odsPerson" DataTextField="Username" DataValueField="ID">
                                </asp:DropDownList>


                            </div>
                        </div>
                    </div>
                    <div class="row" style="visibility: hidden;">

                        <div class="col-md-12">
                            <div class="form-group">

                                <label>توضیحات</label>

                                <asp:TextBox ID="txtRemark" runat="server" TextMode="MultiLine" CssClass="form-control" MaxLength="50" placeholder="توضیحات را وارد کنید"></asp:TextBox>


                            </div>




                        </div>
                    </div>
                </div>

            </div>


        </div>


    </div>




    <asp:HiddenField ID="hdnAction" runat="server" />

</asp:Content>
