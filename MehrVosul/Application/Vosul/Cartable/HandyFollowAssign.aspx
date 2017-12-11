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
            
            var txtNotPiadDurationDay = document.getElementById("<%=txtNotPiadDurationDay.ClientID%>");
        
          
              if (trimall(txtNotPiadDurationDay.value) == "") {
                  alert("تعداد روز معوق را وارد نمایید");
                  txtNotPiadDurationDay.focus();
                  return false;
              }

              return true;
          }




        function SaveOperation_Validate() {


            var cmbFiles = document.getElementById("<%=cmbFiles.ClientID%>");


            if (cmbFiles.options[cmbFiles.selectedIndex].value == -1) {
                {
                    alert("پرونده را مشخص نمایید");
                    cmbFiles.focus();
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



                var txtNotPiadDurationDay = document.getElementById("<%=txtNotPiadDurationDay.ClientID%>");


            if (parseInt(txtNotPiadDurationDay.value)<60) {
                    {
                        alert("تعداد روز تاخیر بایستی از 60 بیشتر باشد");
                        txtNotPiadDurationDay.focus();
                        return false;
                    }

                }
                
            
            return true;
        }

      
    </script>



    <style type="text/css">
        .auto-style1 {
            position: relative;
            min-height: 1px;
            float: right;
            width: 100%;
            right: -3px;
            top: 4px;
            padding-left: 15px;
            padding-right: 15px;
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

                                <label>تعداد روز معوق</label>
                                <asp:UpdatePanel ID="UpdatePanel5" runat="server">
                                    <ContentTemplate>

                                        <asp:TextBox ID="txtNotPiadDurationDay" AutoPostBack="true" runat="server" CssClass="form-control" placeholder="تعدا روز معوق را وارد کنید"></asp:TextBox>




                                    </ContentTemplate>
                                </asp:UpdatePanel>


                            </div>
                        </div>
                        <div class="col-md-6">
                            <div class="form-group">
                                <br />

                                <asp:LinkButton CssClass="btn btn-success" OnClientClick="return CheckDataEnter();" ID="btnCheckFiles" runat="server" ToolTip="نمایش پرونده"><i class="fa fa-filter fa-x"></i> </asp:LinkButton>


                            </div>
                        </div>

                    </div>

                    <div class="row">
                        <div class="col-md-6">
                            <div class="form-group">
                                <label>پرونده های معوق</label>
                                <asp:ObjectDataSource ID="odsFiles" runat="server"
                                    OldValuesParameterFormatString="original_{0}" SelectMethod="GetData"
                                    TypeName="BusinessObject.dstTotalDeffredLCTableAdapters.spr_TotalDeffredLCFileAssign_SelectTableAdapter">
                                    <SelectParameters>
                                        <asp:Parameter Name="BranchCode" Type="String" />
                                        <asp:Parameter Name="NotPiadDurationDay" Type="Int32" />
                                        <asp:Parameter Name="BranchID" Type="Int32" />
                                    </SelectParameters>
                                </asp:ObjectDataSource>

                                <asp:UpdatePanel ID="UpdatePanel4" runat="server">
                                    <ContentTemplate>
                                        <asp:DropDownList ID="cmbFiles" runat="server" CssClass="form-control"
                                            AutoPostBack="True" DataSourceID="odsFiles" DataTextField="CULN" DataValueField="CustomerNO">
                                        </asp:DropDownList>
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

                                <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                                    <ContentTemplate>
                                        <asp:DropDownList ID="cmbPerson" runat="server" CssClass="form-control"
                                            AutoPostBack="True" DataSourceID="odsPerson" DataTextField="Username" DataValueField="ID">
                                        </asp:DropDownList>
                                    </ContentTemplate>
                                </asp:UpdatePanel>



                            </div>
                        </div>
                    </div>
                    <div class="row">

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
