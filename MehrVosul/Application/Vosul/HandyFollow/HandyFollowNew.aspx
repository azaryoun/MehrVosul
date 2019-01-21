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

        function CheckData1() {

            var chkbxCheckBack = document.getElementById("<%=chkbxCheckBack.ClientID%>");

            var Bootstrap_PersianDateTimePickerCheckDate = document.getElementById("<%= Bootstrap_PersianDateTimePickerCheckDate.ClientID%>");

            if (chkbxCheckBack.checked == true) {


            }
            return true;
        }


        function CheckDataEnter() {

            var txtLetterNO = document.getElementById("<%=txtLetterNO.ClientID%>");
            var txtRegisterNO = document.getElementById("<%=txtRegisterNO.ClientID%>");
            var txtInvitationDate = document.getElementById("<%=txtInvitationDate.ClientID%>");
            var txtInvitationTime = document.getElementById("<%=txtInvitationTime.ClientID%>");

            var cmbNotificationType = document.getElementById("<%=cmbNotificationType.ClientID%>");
            if (cmbNotificationType.options[cmbNotificationType.selectedIndex].value == 3) {
                if (trimall(txtInvitationDate.value) == "") {
                    alert("تاریخ دعوتنامه را وارد نمایید");
                    txtInvitationDate.focus();
                    return false;
                }


                if (trimall(txtInvitationTime.value) == "") {
                    alert("زمان دعوتنامه را وارد نمایید");
                    txtInvitationTime.focus();
                    return false;
                }
            }

            var txtRSLetterNo = document.getElementById("<%=txtRSLetterNo.ClientID%>");
            var txtRSLetterDate = document.getElementById("<%=txtRSLetterDate.ClientID%>");
            var txtInstallmentAmount = document.getElementById("<%=txtInstallmentAmount.ClientID%>");
            var txtDefferedAmount = document.getElementById("<%=txtDefferedAmount.ClientID%>");
            var txtRSRemarks = document.getElementById("<%=txtRSRemarks.ClientID%>");


            if (cmbNotificationType.options[cmbNotificationType.selectedIndex].value == 6) {
                if (trimall(txtRSLetterNo.value) == "") {
                    alert("شماره نامه عطفی را وارد نمایید");
                    txtRSLetterNo.focus();
                    return false;
                }


                if (trimall(txtRSLetterDate.value) == "") {
                    alert("تاریخ نامه عطفی را وارد نمایید");
                    txtRSLetterDate.focus();
                    return false;
                }

                if (trimall(txtInstallmentAmount.value) == "") {
                    alert("مبلغ قسط را وارد نمایید");
                    txtInstallmentAmount.focus();
                    return false;
                }

                if (trimall(txtDefferedAmount.value) == "") {
                    alert("مبلغ بدهی را وارد نمایید");
                    txtDefferedAmount.focus();
                    return false;
                }

                if (trimall(txtRSRemarks.value) == "") {
                    alert("توضیحات واریز را وارد نمایید");
                    txtRSRemarks.focus();
                    return false;
                }
            }

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

            var rdboToSponsors = document.getElementById("<%=rdboToSponsor.ClientID%>");
            var cmbSponsor = document.getElementById("<%=cmbSponsor.ClientID%>");

            var listcontrol = rdboToSponsors.rows[1].cells[0].childNodes[0];
            var listcontrol1 = rdboToSponsors.rows[0].cells[0].childNodes[0];
            if (listcontrol.checked) {
                if (cmbSponsor.options[cmbSponsor.selectedIndex].value == -1) {
                    alert("لطفا ضامن را مشخص نمائید.", "خطا");
                    cmbSponsor.focus();
                    return false;
                }
            }
            else if (!listcontrol1.checked) {
                alert("لطفا ضامن را مشخص نمائید.", "خطا");
                cmbSponsor.focus();
                return false;
            }


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
                                        <th>شماره چک(تاریخ)</th>
                                        <th>چک برگشتی</th>
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
                            <label>شماره تسهیلات:&nbsp;&nbsp;&nbsp; </label>
                            <label runat="server" style="font-weight: bold;" id="lblLCNO">"---"</label>
                        </div>
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

                                    <div class="form-group">
                                        <label>آدرس:&nbsp;&nbsp;&nbsp; </label>
                                        <label runat="server" style="font-weight: bold;" id="lblAddress">"---"</label>
                                        <br />
                                        <br />
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
                                    <asp:UpdatePanel ID="UpdatePanel11" runat="server">
                                        <ContentTemplate>
                                            <div class="form-group">

                                                <label>&nbsp;&nbsp;&nbsp; آدرس:</label>
                                                <label runat="server" id="lblSponsorAddress"></label>

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
                            <asp:UpdatePanel ID="UpdatePanel5" runat="server">
                                <ContentTemplate>
                                    <asp:DropDownList ID="cmbNotificationType" runat="server"
                                        CssClass="form-control" AutoPostBack="True">

                                        <asp:ListItem Value="2"> تماس تلفنی</asp:ListItem>
                                        <asp:ListItem Value="3">دعوت نامه</asp:ListItem>
                                        <asp:ListItem Value="4">اظهارنامه</asp:ListItem>
                                        <asp:ListItem Value="5">اخطاریه</asp:ListItem>
                                        <asp:ListItem Value="6">کسر از حقوق</asp:ListItem>

                                    </asp:DropDownList>
                                </ContentTemplate>
                            </asp:UpdatePanel>
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
                            <label style="font-weight: bold; font-size: medium">توضیحات پیگیری</label>
                            <asp:TextBox ID="txtRemark" runat="server" CssClass="form-control"
                                placeholder="توضیحات را وارد کنید" Height="100px" TextMode="MultiLine"></asp:TextBox>
                        </div>
                        <br />
                    </div>
                    <br />
                    <asp:UpdatePanel ID="UpdatePanel13" runat="server">
                        <ContentTemplate>
                            <div class="col-md-6" runat="server" id="divCheckInfo">
                                <div class="panel panel-default">
                                    <div class="panel-heading">
                                        <label>مشخصات چک</label>
                                    </div>
                                    <div class="form-group">

                                        <label>شماره چک</label>
                                        <asp:TextBox ID="txtCheckNO" runat="server" CssClass="form-control"
                                            placeholder="شماره چک را وارد کنید"></asp:TextBox>
                                    </div>
                                    <div class="form-group">
                                        <label>توضیحات چک(شماره حساب،مبلغ چک،بانک عامل)</label>

                                        <asp:TextBox ID="txtAccountNO" runat="server" CssClass="form-control"
                                            placeholder="توضیحات  یا شماره حساب را وارد کنید" Height="100px" TextMode="MultiLine"></asp:TextBox>

                                    </div>
                                    <asp:UpdatePanel ID="UpdatePanel9" runat="server">
                                        <ContentTemplate>
                                            <label>
                                                <asp:CheckBox ID="chkbxCheckBack" Text="چک برگشتی" AutoPostBack="true" runat="server" />
                                            </label>
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </div>
                            </div>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                    <asp:UpdatePanel ID="UpdatePanel12" runat="server">
                        <ContentTemplate>
                            <div class="col-md-6" runat="server" id="divCheckInfoDate">
                                <div class="panel panel-default">
                                    <div class="panel-heading">
                                        <label>تاریخ</label>
                                    </div>
                                    <div class="form-group">

                                        <label>تاریخ چک</label>
                                        <uc4:Bootstrap_PersianDateTimePicker ID="Bootstrap_PersianDateTimePickerCheckDate"
                                            runat="server" />
                                    </div>
                                    <div class="form-group">
                                        <label>تاریخ  تعهد چک</label>
                                        <uc4:Bootstrap_PersianDateTimePicker ID="Bootstrap_PersianDateTimePickerChekDateDuty"
                                            runat="server" />
                                    </div>

                                    <div class="form-group">

                                        <label>تاریخ برگشت چک</label>

                                        <uc4:Bootstrap_PersianDateTimePicker ID="Bootstrap_PersianDateTimeCheckReturnDate"
                                            runat="server" />

                                    </div>

                                    <div class="form-group">
                                        <label>تاریخ ارجاء به حقوقی</label>

                                        <uc4:Bootstrap_PersianDateTimePicker ID="Bootstrap_PersianDateTimeCheckLegalDate"
                                            runat="server" />
                                    </div>

                                </div>

                            </div>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </div>
            </div>


        </div>
    </div>
    <div class="row">
        <br />
        <div class="col-md-6">
            <div class="form-group">
                <div class="panel panel-default">
                    <div class="panel-heading">
                        <label>تغییر اطلاعات جهت چاپ یا ذخیره </label>
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
                            <label>آدرس2</label>
                            <asp:TextBox ID="txtAddress2" MaxLength="50" runat="server" CssClass="form-control" placeholder="آدرس را وارد کنید"></asp:TextBox>

                        </div>
                        <div class="form-group">
                            <label>کد ملی</label>
                            <asp:TextBox ID="txtNationalID" MaxLength="50" runat="server" CssClass="form-control" placeholder="کد ملی را وارد کنید"></asp:TextBox>

                        </div>
                    </div>
                </div>
            </div>
        </div>
        <asp:UpdatePanel ID="UpdatePanel8" runat="server">
            <ContentTemplate>
                <div id="divNotice" runat="server" visible="false" class="col-md-6">
                    <div class="form-group">
                        <div class="panel panel-default">
                            <div class="panel-heading">
                                <label>اطلاعات نامه جهت چاپ اخطاریه، اظهارنامه، دعوتنامه و کسر از حقوق</label>
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
                                    <label>شناسه ملی </label>
                                    <asp:TextBox ID="txtCompanyNationalID" MaxLength="50" runat="server" CssClass="form-control" placeholder="شناسه ملی را وارد کنید"></asp:TextBox>

                                </div>



                            </div>
                        </div>
                    </div>
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
    <asp:UpdatePanel ID="UpdatePanel6" runat="server">
        <ContentTemplate>
            <div class="row" id="divInvitation" runat="server" visible="false">
                <div class="col-md-12">
                    <div class="form-group">

                        <div class="panel panel-default">
                            <div class="panel-heading">
                                <label>اطلاعات جهت دعوتنامه</label>
                            </div>
                            <div class="col-md-6">
                                <div class="form-group">
                                    <label>تاریخ</label>
                                    <asp:TextBox ID="txtInvitationDate" MaxLength="50" runat="server" CssClass="form-control" placeholder="تاریخ را وارد کنید"></asp:TextBox>

                                </div>
                            </div>

                            <div class="col-md-6">
                                <div class="form-group">
                                    <label>ساعت</label>
                                    <asp:TextBox ID="txtInvitationTime" MaxLength="50" runat="server" CssClass="form-control" placeholder="ساعت را وارد کنید"></asp:TextBox>

                                </div>
                            </div>
                        </div>

                    </div>
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
    <asp:UpdatePanel ID="UpdatePanel7" runat="server">
        <ContentTemplate>
            <div class="row" id="divReductionSalary" runat="server" visible="false">
                <div class="col-md-12">
                    <div class="form-group">

                        <div class="panel panel-default">
                            <div class="panel-heading">
                                <label>اطلاعات جهت کسر از حقوق</label>
                            </div>
                            <div class="row">
                                <div class="col-md-6">
                                    <div class="form-group">
                                        <label>شماره نامه عطفی</label>
                                        <asp:TextBox ID="txtRSLetterNo" MaxLength="50" runat="server" CssClass="form-control" placeholder="شماره نامه عطفی را وارد کنید"></asp:TextBox>

                                    </div>
                                </div>

                                <div class="col-md-6">
                                    <div class="form-group">
                                        <label>تاریخ نامه عطفی</label>
                                        <asp:TextBox ID="txtRSLetterDate" MaxLength="50" runat="server" CssClass="form-control" placeholder="تاریخ نامه عطفی را وارد کنید"></asp:TextBox>

                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-md-6">
                                    <div class="form-group">
                                        <label>مبلغ قسط</label>
                                        <asp:TextBox ID="txtInstallmentAmount" MaxLength="50" runat="server" CssClass="form-control" placeholder="مبلغ قسط را وارد کنید"></asp:TextBox>

                                    </div>
                                </div>

                                <div class="col-md-6">
                                    <div class="form-group">
                                        <label>مبلغ بدهی</label>
                                        <asp:TextBox ID="txtDefferedAmount" MaxLength="50" runat="server" CssClass="form-control" placeholder="مبلغ بدهی را وارد کنید"></asp:TextBox>

                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-md-6">
                                    <div class="form-group">
                                        <label>نام گیرنده</label>
                                        <asp:TextBox ID="txtReceiver" TextMode="MultiLine" MaxLength="50" runat="server" CssClass="form-control" placeholder="گیرنده را وارد کنید"></asp:TextBox>

                                    </div>
                                </div>
                                <div class="col-md-6">
                                    <div class="form-group">
                                        <label>توضیحات واریز</label>
                                        <asp:TextBox ID="txtRSRemarks" TextMode="MultiLine" MaxLength="50" runat="server" CssClass="form-control" placeholder="توضیحات واریز را وارد کنید"></asp:TextBox>

                                    </div>
                                </div>


                            </div>
                        </div>

                    </div>
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
    <span class="form-group input-group-btn"></span>


    <span class="form-group input-group-btn">
        <asp:UpdatePanel ID="UpdatePanel14" runat="server">
            <ContentTemplate>
                <asp:LinkButton CssClass="btn btn-success" ID="btnAddToText" OnClientClick="return CheckData1();" runat="server" ToolTip="ثبت پیگیری"><i class="fa fa-plus-circle fa-lg"></i> </asp:LinkButton>
            </ContentTemplate>
        </asp:UpdatePanel>
        <br />
        <br />
        <asp:UpdatePanel ID="UpdatePanel10" runat="server">
            <ContentTemplate>
                <asp:LinkButton Visible="false" CssClass="btn btn-success" ID="btnPrint" runat="server" OnClientClick="return CheckDataEnter();" ToolTip="ذخیزه و چاپ"><i class="fa fa-print fa-lg"></i> </asp:LinkButton>
            </ContentTemplate>
        </asp:UpdatePanel>


    </span>
    <br />
   
        <asp:UpdatePanel ID="UpdatePanel15" runat="server">
            <ContentTemplate>
                <asp:Label ID="lblMessage" ForeColor="Red" Font-Bold="true" runat="server" Text=""></asp:Label>

            </ContentTemplate>
        </asp:UpdatePanel>
   

    <asp:HiddenField ID="hdnAction" runat="server" />
</asp:Content>
