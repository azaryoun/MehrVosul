<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/MasterPage/SmartIntergace_Master.Master" CodeBehind="HandyFollowCheckAlarm.aspx.vb" Inherits="MehrVosul.HandyFollowCheckAlarm" %>

<%@ Register Src="../../../UserControl/Bootstrap_Panel.ascx" TagName="Bootstrap_Panel" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script language="javascript" type="text/javascript">



        function StartthePage() {

            return true;
        }



        function btnFollwoing_ClientClick(Pkey, Pkey1, Pkey2) {
            var hdnAction_Name = "<%=hdnAction.ClientID%>";
               var hdnAction = document.getElementById(hdnAction_Name);
               hdnAction.value = "S;" + Pkey + ";" + Pkey1 + ";" + Pkey2 + ";";
               window.document.forms[0].submit();
               return false;
           }

    </script>



    <style type="text/css">
        .auto-style1 {
            height: 36px;
        }
    </style>



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

                <div class="col-md-6">

                    <div class="form-group">
                    </div>
                </div>
                <div class="panel-body">
                    <div class="table-responsive">
                        <table class="table table-striped table-bordered table-hover" runat="server" id="tblMResult">
                            <thead>
                                <tr>


                                    <th></th>
                                    <th>شماره وام</th>
                                    <th>شماره مشتری</th>
                                    <th>نام و نام خانودگی</th>
                                    <th>شماره تماس</th>
                                    <th>کاربر پیگیر</th>
                                    <th>شماره چک</th>
                                    <th>تاریخ چک</th>
                                    <th>ثبت پیگیری</th>


                                </tr>
                            </thead>
                            <tbody>
                            </tbody>
                        </table>


                    </div>
                </div>

            </div>
        </div>
    </div>

    <asp:HiddenField ID="hdnAction" runat="server" />

</asp:Content>
