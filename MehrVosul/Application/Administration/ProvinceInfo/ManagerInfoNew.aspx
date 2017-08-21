<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/MasterPage/SmartIntergace_Master.Master" CodeBehind="ManagerInfoNew.aspx.vb" Inherits="MehrVosul.ManagerInfoNew" %>

<%@ Register Src="../../../UserControl/Bootstrap_Panel.ascx" TagName="Bootstrap_Panel" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script language="javascript" type="text/javascript">


        function StartthePage() {
            return true;
        }

        function SaveOperation_Validate() {


             var txtMobile = document.getElementById("<%=txtMobile.ClientID%>");
            var cmbProvince = document.getElementById("<%=cmbProvince.ClientID%>");

          if (cmbProvince.options[cmbProvince.selectedIndex].value == 0) {
                alert("استان را مشخص نمائید", "خطا");
                cmbProvince.focus();
                return false;
          }

          if (trimall(txtMobile.value) == "") {
              alert("شماره موبایل را وارد نمایید");
              txtMobile.focus();
              return false;
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

                         
                            <div class="form-group">
                                <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                                    <ContentTemplate>
                                        <label>نام و نام خانوادگی</label>
                                        <asp:TextBox ID="txtLastName" MaxLength="50" runat="server" CssClass="form-control" placeholder="نام خانودگی را وارد کنید"></asp:TextBox>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </div>

                            <div class="form-group has-error">
                                <asp:UpdatePanel ID="UpdatePanel4" runat="server">
                                    <ContentTemplate>
                                        <label>شماره موبایل</label>
                                        <asp:TextBox ID="txtMobile" MaxLength="50" runat="server" CssClass="form-control" placeholder="شماره موبایل را وارد کنید"></asp:TextBox>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </div>


                        </div>
                    </div>





                </div>
            </div>

        </div>
    </div>

    <asp:HiddenField ID="hdnAction" runat="server" />

</asp:Content>
