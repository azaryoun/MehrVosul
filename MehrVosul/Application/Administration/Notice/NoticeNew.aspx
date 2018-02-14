<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/MasterPage/SmartIntergace_Master.Master" CodeBehind="NoticeNew.aspx.vb" Inherits="MehrVosul.NoticeNew" %>

<%@ Register Src="../../../UserControl/Bootstrap_Panel.ascx" TagName="Bootstrap_Panel" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script language="javascript" type="text/javascript">


        function StartthePage() {
            return true;
        }

        function SaveOperation_Validate() {

            var txtAccessgrouptitle = document.getElementById("<%=txtAccessgrouptitle.ClientID%>");


            if (trimall(txtAccessgrouptitle.value) == "") {
                alert("عنوان گروه دسترسی را وارد نمایید");
                txtAccessgrouptitle.focus();
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
                        <div class="col-md-12">


                            <div class="form-group has-error">
                                <label>عنوان </label>

                                <asp:TextBox ID="txtTitle" runat="server" CssClass="form-control" MaxLength="50" placeholder="عنوان را وارد نمایید"></asp:TextBox>

                            </div>

                            <div class="form-group has-error">
                                <label>کد اعلان </label>

                                <asp:TextBox ID="txtNoticeCode" runat="server" CssClass="form-control" MaxLength="50" placeholder="کد اعلان را وارد نمایید"></asp:TextBox>

                            </div>

                            <div class="form-group has-error">
                                <label>توضیحات </label>

                                <asp:TextBox ID="txtDesc" runat="server" CssClass="form-control" MaxLength="50" placeholder="توضیحات را وارد نمایید"></asp:TextBox>

                            </div>

                         
                            <div class="panel panel-default">
                                <div class="panel-heading">
                                    <label>وضعیت</label>
                                </div>

                                <div class="panel-body" style="max-height: 200px; overflow-y: scroll;">

                                    <label>
                                        <input type="checkbox" runat="server" id="chkVisibility" checked="checked" />
                                        نمایش به کاربر</label>
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
