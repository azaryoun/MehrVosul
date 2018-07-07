<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/MasterPage/SmartIntergace_Master.Master" CodeBehind="HandyFollowNew.aspx.vb" Inherits="MehrVosul.HandyFollowNew" %>

<%@ Register Src="../../../UserControl/Bootstrap_Panel.ascx" TagName="Bootstrap_Panel" TagPrefix="uc1" %>
<%@ Register Src="../../../UserControl/UC_TimePicker.ascx" TagName="UC_TimePicker" TagPrefix="uc3" %>
<%@ Register Src="../../../UserControl/Bootstrap_PersianDateTimePicker.ascx" TagName="Bootstrap_PersianDateTimePicker" TagPrefix="uc4" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script language="javascript" type="text/javascript">


        function StartthePage() {


            return true;
        }


        function lnkbtnHandyDelete_ClientClick(Pkey) {

            if (!confirm("حذف شدن رکوردانتخاب شده را تایید نمایید"))
                return false;

            var hdnAction_Name = "<%=hdnAction.ClientID%>";
            var hdnAction = document.getElementById(hdnAction_Name);
            hdnAction.value = "D;" + Pkey + ";";
            window.document.forms[0].submit();
            return false;
        }



        function CheckDataEnter() {

            var txtLetterNO = document.getElementById("<%=txtLetterNO.ClientID%>");
            var txtRegisterNO = document.getElementById("<%=txtRegisterNO.ClientID%>");


            ////if (trimall(txtLetterNO.value) == "") {
            ////    alert("شماره نامه را وارد نمایید");
            ////    txtLetterNO.focus();
            ////    return false;
            ////}


            ////if (trimall(txtRegisterNO.value) == "") {
            ////    alert("شماره پیوست را وارد نمایید");
            ////    txtRegisterNO.focus();
            ////    return false;
            ////}

            if (!confirm("ذخیره و پیش نمایش چاپ اخطاریه یا اظهارنامه را تایید نمایید"))
                return false;

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

                <div class="panel panel-default" id="divResult" runat="server">
                    <div class="panel-heading">
                        لیست پیگیریهای ثبت شده
                    </div>
                    <div class="panel-body">

                        <div class="table-responsive">
                            <table class="table table-striped table-bordered table-hover" id="tblResult" runat="server">
                                <thead>
                                    <tr>

                                        <th>#</th>
                                        <th>تاریخ</th>
                                        <th>نوع اطلاع رسانی</th>

                                        <th>مخاطب(شماره مشتری)</th>
                                        <th>وضعیت تماس </th>
                                        <th>نتیجه تماس</th>
                                        <th>ملاحظات</th>

                                    </tr>
                                </thead>

                            </table>
                        </div>
                    </div>


                </div>


            </div>


        </div>
        <div class="col-md-12">

            <div class="panel panel-default">
                <div class="panel-heading">
                    <asp:Label ID="Label1" runat="server" Text="ثبت پیگیری"></asp:Label>
                </div>
                <div class="panel-body">

                    <div class="col-md-6">

                        <div class="form-group">
                            <label>مبلغ معوق:&nbsp;&nbsp;&nbsp; </label>
                            <label runat="server" style="font-weight: bold;" id="lblAmountDefferd">"---"</label>
                        </div>

                        <div class="form-group">
                            <div class="panel panel-default">
                                <div class="panel-heading">
                                    <label>مخاطب</label>
                                </div>
                                <div class="panel-body" style="max-height: 200px;">
                                    <asp:RadioButtonList ID="rdboToSponsor" runat="server">
                                        <asp:ListItem Selected="True" Value="0">وام گیرنده</asp:ListItem>
                                        <asp:ListItem Value="1">ضامن</asp:ListItem>
                                    </asp:RadioButtonList>
                                </div>
                            </div>
                        </div>

                        <div class="form-group">
                            <div class="panel panel-default">
                                <div class="panel-heading">
                                    <label>مشخصات وام گیرنده</label>
                                </div>
                                <div class="panel-body" style="max-height: 200px;">

                                    <div class="form-group">
                                        <label>نام و نام خانوادگی:&nbsp;&nbsp;&nbsp; </label>
                                        <label runat="server" style="font-weight: bold;" id="lblBorroweName">"---"</label>
                                    </div>
                                    <div class="form-group">
                                        <label>تلفن منزل:&nbsp;&nbsp;&nbsp; </label>
                                        <label runat="server" style="font-weight: bold;" id="lblBorrowerHomePhone">"---"</label>
                                    </div>
                                    <div class="form-group">
                                        <label>تلفن محل کار:&nbsp;&nbsp;&nbsp; </label>
                                        <label style="font-weight: bold;" runat="server" id="lblBorrowerPhone">"---"</label>
                                    </div>
                                    <div class="form-group">
                                        <label>موبایل:&nbsp;&nbsp;&nbsp; </label>
                                        <label runat="server" style="font-weight: bold;" id="lblBorrowerMobile">"---"</label>
                                    </div>


                                </div>



                            </div>
                        </div>

                        <asp:ObjectDataSource ID="odcSponsor"
                            runat="server" OldValuesParameterFormatString="original_{0}"
                            SelectMethod="GetData"
                            TypeName="BusinessObject.dstSponsor_ListTableAdapters.spr_SponsorList2_SelectByFileLoanTableAdapter">
                            <SelectParameters>
                                <asp:Parameter Name="FileNO" Type="String" />
                                <asp:Parameter Name="LoanNO" Type="String" />
                            </SelectParameters>
                        </asp:ObjectDataSource>
                        <div class="form-group">
                            <div class="panel panel-default">
                                <div class="panel-heading">
                                    <label>ضامن ها</label>
                                </div>
                                <br />
                                <div class="form-group">
                                    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                        <ContentTemplate>
                                            <asp:DropDownList ID="cmbSponsor" runat="server"
                                                CssClass="form-control" DataSourceID="odcSponsor"
                                                DataTextField="FullName" DataValueField="SponsorCustomerNo" AutoPostBack="True">
                                            </asp:DropDownList>
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                    <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                                        <ContentTemplate>
                                            <div class="form-group">
                                                <br />
                                                <label>&nbsp;&nbsp;&nbsp; تلفن منزل: </label>


                                                <label runat="server" id="lblSponsorPhone"></label>

                                            </div>
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                    <asp:UpdatePanel ID="UpdatePanel4" runat="server">
                                        <ContentTemplate>
                                            <div class="form-group">
                                                <label>&nbsp;&nbsp;&nbsp; تلفن محل کار:</label>


                                                <label runat="server" id="lblSponsorPhoneWork"></label>

                                            </div>
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                    <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                                        <ContentTemplate>
                                            <div class="form-group">

                                                <label>&nbsp;&nbsp;&nbsp; موبایل:</label>



                                                <label runat="server" id="lblSponsorMobile"></label>


                                            </div>
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </div>
                            </div>
                        </div>
                    </div>

                    <div class="col-md-6">
                        <div class="form-group">
                            <label>نوع اطلاع رسانی</label>

                            <asp:DropDownList ID="cmbNotificationType" runat="server"
                                CssClass="form-control">


                                <asp:ListItem Value="2" Selected="True"> تماس تلفنی</asp:ListItem>
                                <asp:ListItem Value="3">دعوت نامه</asp:ListItem>
                                <asp:ListItem Value="4">اخطاریه</asp:ListItem>
                                <asp:ListItem Value="5">اظهارنامه</asp:ListItem>


                            </asp:DropDownList>

                        </div>

                        <div class="form-group has-error">
                            <label>وضعیت تماس</label>

                            <div class="panel-body" style="max-height: 200px;">
                                <asp:RadioButtonList ID="rdbListAnswered" runat="server">
                                    <asp:ListItem Selected="True" Value="1">با پاسخ</asp:ListItem>
                                    <asp:ListItem Value="0">بدون پاسخ</asp:ListItem>
                                </asp:RadioButtonList>
                            </div>

                        </div>


                        <div class="form-group">
                            <label>نتیجه تماس</label>

                            <div class="panel-body" style="max-height: 200px;">
                                <asp:RadioButtonList ID="rdbListNotificationStatus" runat="server">
                                    <asp:ListItem Selected="True" Value="1">مثبت</asp:ListItem>
                                    <asp:ListItem Value="0">منفی</asp:ListItem>
                                </asp:RadioButtonList>
                            </div>

                        </div>

                        <div class="form-group">
                            <div class="panel panel-default">
                                <div class="panel-heading">
                                    <label>تاریخ و ساعت تماس</label>
                                </div>
                                <div class="panel-body" style="max-height: 200px;">


                                    <uc4:Bootstrap_PersianDateTimePicker ID="Bootstrap_PersianDateTimePicker_From"
                                        runat="server" />



                                </div>



                            </div>
                        </div>
                        <div class="form-group">
                            <div class="panel panel-default">
                                <div class="panel-heading">
                                    <label>تاریخ تعهد پرداخت</label>
                                </div>
                                <div class="panel-body" style="max-height: 200px;">


                                    <uc4:Bootstrap_PersianDateTimePicker ID="Bootstrap_PersianDateTimePicker_TO"
                                        runat="server" />



                                </div>



                            </div>
                        </div>




                        <div class="form-group">
                            <label>توضیحات</label>

                            <asp:TextBox ID="txtRemark" runat="server" CssClass="form-control"
                                placeholder="توضیحات را وارد کنید" Height="100px" TextMode="MultiLine"></asp:TextBox>

                        </div>

                    </div>




                </div>
            </div>



        </div>
    </div>
    <div class="row" id="divNotice" runat="server" visible="false">
        <br />
        <div class="col-md-6">
            <div class="form-group">
                <div class="panel panel-default">
                    <div class="panel-heading">
                        <label>تغییر اطلاعات جهت چاپ اخطاریه یا اظهارنامه</label>
                    </div>
                    <div class="panel-body" style="max-height: 300px;">
                        <div class="form-group">
                            <label>تلفن همراه</label>
                            <asp:TextBox ID="txtMobile" MaxLength="50" runat="server" CssClass="form-control" placeholder="تلفن همراه را وارد کنید"></asp:TextBox>

                        </div>

                        <div class="form-group">
                            <label>آدرس</label>
                            <asp:TextBox ID="txtAddress" MaxLength="50" runat="server" CssClass="form-control" placeholder="آدرس را وارد کنید"></asp:TextBox>

                        </div>

                        <div class="form-group">
                            <label>کد ملی</label>
                            <asp:TextBox ID="txtNationalID" MaxLength="50" runat="server" CssClass="form-control" placeholder="کد ملی را وارد کنید"></asp:TextBox>

                        </div>
                    </div>
                </div>
            </div>
        </div>
        <div class="col-md-6">
            <div class="form-group">
                <div class="panel panel-default">
                    <div class="panel-heading">
                        <label>اطلاعات نامه جهت چاپ اخطاریه یا اظهارنامه</label>
                    </div>
                    <div class="panel-body" style="max-height: 300px;">
                        <div class="form-group">
                            <label>شماره</label>
                            <asp:TextBox ID="txtLetterNO" MaxLength="50" runat="server" CssClass="form-control" placeholder="شماره نامه را وارد کنید"></asp:TextBox>

                        </div>

                        <div class="form-group">
                            <label>پیوست(شماره ثبت)</label>
                            <asp:TextBox ID="txtRegisterNO" MaxLength="50" runat="server" CssClass="form-control" placeholder="شماره پیوست نامه را وارد کنید"></asp:TextBox>

                        </div>

                         <div class="form-group">
                            <label></label>
                            <asp:TextBox ID="txtCompanyNationalID" MaxLength="50" runat="server" CssClass="form-control" placeholder="شناسه ملی را وارد کنید"></asp:TextBox>

                        </div>


                    </div>
                </div>
            </div>
        </div>
    </div>

    <span class="form-group input-group-btn">


        <asp:LinkButton CssClass="btn btn-success" ID="btnAddToText" runat="server" ToolTip="ثبت پیگیری"><i class="fa fa-plus-circle fa-lg"></i> </asp:LinkButton>

    </span>


    <span class="form-group input-group-btn">

      
        <asp:LinkButton Visible="false" CssClass="btn btn-success" ID="btnPrint" runat="server" OnClientClick="return CheckDataEnter();" ToolTip="ذخیزه و چاپ"><i class="fa fa-print fa-lg"></i> </asp:LinkButton>

    </span>
    <br />





    <asp:HiddenField ID="hdnAction" runat="server" />

</asp:Content>
