<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/MasterPage/SmartIntergace_Master.Master" CodeBehind="HandyFollowAssignMagic.aspx.vb" Inherits="MehrVosul.HandyFollowAssignMagic" %>

<%@ Register Src="../../../UserControl/Bootstrap_Panel.ascx" TagName="Bootstrap_Panel" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script language="javascript" type="text/javascript">


        function StartthePage() {
            return true;
        }

        function SaveOperation_Validate() {



            var txtAssignDay = document.getElementById("<%=txtAssignDay.ClientID%>");

            ////if (trimall(txtAssignDay.value) == "") {
            ////    {
            ////        alert("تعداد روز تاخیر را وارد نمایید");
            ////        txtAssignDay.focus();
            ////        return false;
            ////    }

            ////}

            var txtNotice = document.getElementById("<%=txtNotice.ClientID%>");
            if (trimall(txtNotice.value) == "") {
                {
                    alert("تعداد روز جهت ارسال اخطاریه را وارد نمایید");
                    txtNotice.focus();
                    return false;
                }

            }

            var txtDeclaration = document.getElementById("<%=txtDeclaration.ClientID%>");
            if (trimall(txtDeclaration.value) == "") {
                {
                    alert("تعداد روز جهت ارسال اظهارنامه را وارد نمایید");
                    txtDeclaration.focus();
                    return false;
                }

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

                            <div class="form-group  has-error">
                                <label>تعداد روز جهت آزاد سازی پرونده های تخصیص یافته</label>

                                <asp:TextBox ID="txtAssignDay" runat="server" CssClass="form-control" placeholder="تعدا روز را وارد کنید"></asp:TextBox>


                            </div>



                        </div>



                    </div>

                    <div class="row">
                        <div class="col-md-12">

                            <div class="form-group  has-error">
                                <label>تعداد روز جهت ارسال اخطاریه</label>

                                <asp:TextBox ID="txtNotice" runat="server" CssClass="form-control" placeholder="تعدا روز را وارد کنید"></asp:TextBox>


                            </div>



                        </div>



                    </div>

                    <div class="row">
                        <div class="col-md-12">

                            <div class="form-group  has-error">
                                <label>تعداد روز جهت ارسال اظهارنامه</label>

                                <asp:TextBox ID="txtDeclaration" runat="server" CssClass="form-control" placeholder="تعدا روز را وارد کنید"></asp:TextBox>


                            </div>



                        </div>



                    </div>
                </div>

            </div>

        </div>
    </div>


    <asp:HiddenField ID="hdnAction" runat="server" />

</asp:Content>
