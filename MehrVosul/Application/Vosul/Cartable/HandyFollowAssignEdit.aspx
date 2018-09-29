<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/MasterPage/SmartIntergace_Master.Master" CodeBehind="HandyFollowAssignEdit.aspx.vb" Inherits="MehrVosul.HandyFollowAssignEdit" %>

<%@ Register Src="../../../UserControl/Bootstrap_Panel.ascx" TagName="Bootstrap_Panel" TagPrefix="uc1" %>
<%@ Register Src="../../../UserControl/UC_TimePicker.ascx" TagName="UC_TimePicker" TagPrefix="uc3" %>
<%@ Register Src="../../../UserControl/Bootstrap_PersianDateTimePicker.ascx" TagName="Bootstrap_PersianDateTimePicker" TagPrefix="uc4" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script language="javascript" type="text/javascript">


        function StartthePage() {


            return true;
        }

        function CheckDataEnter() {

            var cmbPerson = document.getElementById("<%=cmbPerson.ClientID%>");


            if (cmbPerson.options[cmbPerson.selectedIndex].value == -1) {
                {
                    alert("کارشناس پیگیر را مشخص نمایید");
                    cmbPerson.focus();
                    return false;
                }

            }
            return true;
        }




        function SaveOperation_Validate() {


            var cmbPerson = document.getElementById("<%=cmbPerson.ClientID%>");


            if (cmbPerson.options[cmbPerson.selectedIndex].value == -1) {
                {
                    alert("کارشناس پیگیر را مشخص نمایید");
                    cmbPerson.focus();
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
                            <div class="panel panel-default" id="divResult" runat="server">
                                <div class="panel-heading">
                                    لیست تخصیصهای ثبت شده
                                </div>
                                <div class="panel-body">

                                    <div class="table-responsive">
                                        <table class="table table-striped table-bordered table-hover" id="tblResult" runat="server">
                                            <thead>
                                                <tr>

                                                    <th>#</th>
                                                    <th>تاریخ تخصیص</th>
                                                    <th>نام کاربری</th>
                                                    <th>وضعیت تخصیص</th>
                                                    <th>تاریخ لغو تخصیص</th>
                                                    <th>پیگیری</th>
                                                    

                                                </tr>
                                            </thead>

                                        </table>
                                    </div>
                                </div>

                            </div>
                        </div>
                    </div>

                    <div class="row">
                        <div class="col-md-6">
                            <div class="form-group">


                                <asp:UpdatePanel ID="UpdatePanel4" runat="server">
                                    <ContentTemplate>


                                        <label>تسهیلات</label>

                                        <div class="form-grou">

                                            <asp:TextBox ID="txtLoan" runat="server" ReadOnly="true" CssClass="form-control" MaxLength="50"></asp:TextBox>

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

                    <div class="row">
                        <div class="col-md-6">
                            <div class="form-group">

                                <label>تاریخ اختصاص</label>

                                <asp:TextBox ID="txtAssignDate" Style="direction: ltr" runat="server" ReadOnly="true" CssClass="form-control" MaxLength="50"></asp:TextBox>


                            </div>
                        </div>
                        <div class="col-md-6">
                            <div class="form-group">

                                <label>توضیحات لغو تخصیص</label>

                                <asp:TextBox ID="txtRemark" runat="server" TextMode="MultiLine" CssClass="form-control" MaxLength="50" placeholder="توضیحات را وارد کنید"></asp:TextBox>


                            </div>




                        </div>
                    </div>
                    <div class="row">

                        <div class="col-md-12">
                            <div class="form-group">

                                <label>توضیحات تخصیص جدید</label>

                                <asp:TextBox ID="txtNewRemark" runat="server" TextMode="MultiLine" CssClass="form-control" MaxLength="50" placeholder="توضیحات را وارد کنید"></asp:TextBox>


                            </div>




                        </div>
                    </div>
                </div>




            </div>


        </div>


    </div>




    <asp:HiddenField ID="hdnAction" runat="server" />

</asp:Content>
