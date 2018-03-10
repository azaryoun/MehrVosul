<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/MasterPage/SmartIntergace_Master.Master" CodeBehind="UserEdit.aspx.vb" Inherits="MehrVosul.UserEdit" %>

<%@ Register Src="../../../UserControl/Bootstrap_Panel.ascx" TagName="Bootstrap_Panel" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script language="javascript" type="text/javascript">


        function StartthePage() {
            return true;
        }

        function SaveOperation_Validate() {

            var txtUsername = document.getElementById("<%=txtUsername.ClientID%>");
            var txtFirstName = document.getElementById("<%=txtFirstName.ClientID%>");
            var txtLastName = document.getElementById("<%=txtLastName.ClientID%>");
            var cmbBranch = document.getElementById("<%=cmbBranch.ClientID%>");
            var txtMobile = document.getElementById("<%=txtMobile.ClientID%>");

            if (trimall(txtUsername.value) == "") {
                alert("نام کاربری را وارد نمایید");
                txtUsername.focus();
                return false;
            }

            if (trimall(txtFirstName.value) == "") {
                alert("نام را وارد نمایید");
                txtFirstName.focus();
                return false;
            }


         <%--   var ListBox = document.getElementById('<%=lstAccessGroups.ClientID %>');
            var length = ListBox.length;
            var i = 0;
            var SelectedItemCount = 0;
            var isSelected = false;

            for (i = 0; i < length; i++) {
                if (ListBox.options[i].selected == true) {
                    isSelected = true
                }
            }
            if (isSelected == false) {
                alert("گروه دسترسی را مشخص نمائید", "خطا");

                return false;
            }--%>


            var AccessBoolChecked = false;
            var treeView = document.getElementById("<%= trAccessGroup.ClientID %>");
            var checkBoxes = treeView.getElementsByTagName("input");
            var checkedCount = 0;
            for (var i = 0; i < checkBoxes.length; i++) {
                if (checkBoxes[i].checked) {
                    AccessBoolChecked = true;
                    break;
                }
            }


            if (!AccessBoolChecked) {
                alert("حداقل یک سطح دسترسی باید انتخاب شود");
                return false;

            }

            if (cmbProvince.options[cmbProvince.selectedIndex].value == 0) {
                alert("استان را مشخص نمائید", "خطا");
                cmbProvince.focus();
                return false;
            }


            if (cmbBranch.options[cmbBranch.selectedIndex].value == 0) {
                alert("کد شعبه را مشخص نمائید", "خطا");
                cmbBranch.focus();
                return false;
            }


            if (trimall(txtMobile.value) == "") {
                alert("تلفن همراه را وارد نمایید");
                txtMobile.focus();
                return false;
            }

            return true;
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


                            <div class="form-group has-success">
                                <label>نام کاربری</label>

                                <asp:TextBox ID="txtUsername" runat="server" CssClass="form-control"
                                    MaxLength="50" placeholder="Enter username"></asp:TextBox>
                            </div>

                            <div class="form-group has-error">
                                <label>نام</label>
                                <asp:TextBox ID="txtFirstName" MaxLength="50" runat="server" CssClass="form-control" placeholder="نام را وارد کنید"></asp:TextBox>
                            </div>
                            <div class="form-group has-error">
                                <label>نام خانوادگی</label>
                                <asp:TextBox ID="txtLastName" MaxLength="50" runat="server" CssClass="form-control" placeholder="نام خانوادگی را وارد کنید"></asp:TextBox>
                            </div>

                            <div class="form-group has-error">
                                <label>نوع دسترسی</label>
                                <asp:DropDownList ID="cmbUserType" runat="server" CssClass="form-control">
                                </asp:DropDownList>
                            </div>
                            <div class="form-group">
                                <label>گروه های دسترسی</label>
                                <asp:ObjectDataSource ID="odsAccessGroups" runat="server"
                                    OldValuesParameterFormatString="original_{0}" SelectMethod="GetData"
                                    TypeName="BusinessObject.dstAccessgroupTableAdapters.spr_Accessgroup_List_SelectTableAdapter">
                                    <SelectParameters>
                                        <asp:Parameter Name="Action" Type="Int32" />
                                        <asp:Parameter Name="UserID" Type="Int32" />
                                    </SelectParameters>
                                </asp:ObjectDataSource>
                                <asp:ListBox Visible="false" ID="lstAccessGroups" runat="server" CssClass="form-control" SelectionMode="Multiple" DataSourceID="odsAccessGroups" DataTextField="Desp"
                                    DataValueField="ID"></asp:ListBox>
                                <asp:Panel ID="treeViewDiv" runat="server">

                                    <asp:TreeView ID="trAccessGroup" runat="server" ShowLines="True" onclick="return TreeClick(event)"
                                        Width="100%">
                                    </asp:TreeView>

                                </asp:Panel>
                            </div>



                            <div class="form-group">
                                <label>جنسیت</label>
                                <div class="radio">

                                    <asp:RadioButton ID="rdoSexMale" Checked="true" GroupName="rdoSex" Text="مرد" runat="server" />

                                </div>
                                <div class="radio">
                                    <asp:RadioButton ID="rdoSexFemale" GroupName="rdoSex" Text="زن" runat="server" />


                                </div>



                            </div>

                            <div class="form-group">
                                <label>نشانی</label>
                                <asp:TextBox ID="txtAddress" MaxLength="250" runat="server" TextMode="MultiLine" Rows="3" CssClass="form-control" placeholder="نشانی را وارد کنید"></asp:TextBox>


                            </div>

                            <div class="form-group has-error">
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


                            <div class="form-group has-error">
                                <label>کد شعبه</label>
                                <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                                    <ContentTemplate>
                                        <asp:DropDownList ID="cmbBranch" runat="server"
                                            CssClass="form-control"  AutoPostBack="True">
                                            <asp:ListItem Selected="True" Value="0" Text="---"></asp:ListItem>
                                        </asp:DropDownList>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                                <asp:ObjectDataSource ID="odsBranch" runat="server"
                                    OldValuesParameterFormatString="original_{0}" SelectMethod="GetData"
                                    TypeName="BusinessObject.dstBranchTableAdapters.spr_Branch_List_SelectTableAdapter">
                                    <SelectParameters>
                                        <asp:Parameter Name="Action" Type="Int32" />
                                        <asp:Parameter Name="ProvinceID" Type="Int32" />
                                    </SelectParameters>
                                </asp:ObjectDataSource>

                            </div>
                        </div>



                        <div class="col-md-6">



                            <div class="form-group">
                                <label>کد پرسنلی</label>
                                <asp:TextBox ID="txtPersonCode" MaxLength="50" runat="server" CssClass="form-control" placeholder="کد پرسنلی را وارد کنید"></asp:TextBox>

                            </div>
                            <div class="form-group">
                                <label>نوع کاربر</label>
                                <div class="radio">

                                    <asp:RadioButton ID="rdoIsPartTimeNo" Checked="true" GroupName="rdoIsPartTime" Text="تمام وقت" runat="server" />

                                </div>
                                <div class="radio">
                                    <asp:RadioButton ID="rdoIsPartTimeYes" GroupName="rdoIsPartTime" Text="پاره وقت" runat="server" />


                                </div>

                            </div>
                            <div class="form-group">
                                <label>کد ملی</label>
                                <asp:TextBox ID="txtNationalID" MaxLength="50" runat="server" CssClass="form-control" placeholder="کد ملی را وارد کنید"></asp:TextBox>
                            </div>

                            <div class="form-group">
                                <label>شماره شناسنامه</label>
                                <asp:TextBox ID="txtNationalNo" MaxLength="50" runat="server" CssClass="form-control" placeholder="شماره شناسنامه را وارد کنید"></asp:TextBox>

                            </div>




                            <div class="form-group">
                                <label>تلفن</label>
                                <asp:TextBox ID="txtTel" MaxLength="50" runat="server" CssClass="form-control" placeholder="تلفن را وارد کنید"></asp:TextBox>

                            </div>

                            <div class="form-group has-error">
                                <label>تلفن همراه</label>
                                <asp:TextBox ID="txtMobile" MaxLength="50" runat="server" CssClass="form-control" placeholder="تلفن همراه را وارد کنید"></asp:TextBox>

                            </div>


                            <div class="form-group">
                                <label>پست الکترونیکی</label>
                                <asp:TextBox ID="txtEmail" MaxLength="50" runat="server" CssClass="form-control" placeholder="پست الکترونیکی را وارد کنید"></asp:TextBox>

                            </div>





                            <div class="form-group">
                                <label>عکس کاربر</label>
                                <asp:Image ID="imgUserPhoto" runat="server" CssClass="user-image img-responsive" ImageUrl="~/Images/System/find_user.png" />
                                <asp:FileUpload ID="fleUserPhoto" runat="server" CssClass="form-control" Height="38px" />
                                <p class="help-block">عکس باید فرمت png بوده  و سایز آن 64*64 باشد.</p>
                            </div>




                        </div>

                        <div class="row">
                            <div class="col-md-6">
                                <div class="panel panel-default">


                                    <div class="panel-body" style="max-height: 200px; overflow-y: scroll;">

                                        <label>
                                            <input type="checkbox" runat="server" id="chkStatus" />
                                            فعال</label>
                                    </div>
                                </div>


                            </div>
                        </div>
                    </div>


                </div>

            </div>

        </div>
    </div>


    <asp:HiddenField ID="hdnAction" runat="server" />
</asp:Content>
