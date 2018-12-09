<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/MasterPage/SmartIntergace_Master.Master" CodeBehind="LoanTypeMagic.aspx.vb" Inherits="MehrVosul.LoanTypeMagic" %>

<%@ Register Src="../../../UserControl/Bootstrap_Panel.ascx" TagName="Bootstrap_Panel" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script language="javascript" type="text/javascript">


        function StartthePage() {
            return true;
        }

        function SaveOperation_Validate() {


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


                            <div dir="rtl" class="form-group has-error">


                                <asp:FileUpload ID="fleUploadFile" runat="server" />

                            </div>
                            <div class="form-group">
                                فایل اکسل لیست سیاه را
                                           <asp:LinkButton ID="lnkbtnSample" runat="server">از اینجا دانلود</asp:LinkButton>
                            </div>


                            <div class="form-group">
                                &nbsp;<asp:UpdatePanel ID="UpdatePanel3" runat="server">
                                </asp:UpdatePanel>
                            </div>
                            <div class="form-group">
                                &nbsp;
                            </div>

                        </div>



                    </div>
                </div>

            </div>

        </div>
    </div>


    <asp:HiddenField ID="hdnAction" runat="server" />

</asp:Content>
