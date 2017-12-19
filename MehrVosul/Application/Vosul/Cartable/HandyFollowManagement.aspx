<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/MasterPage/SmartIntergace_Master.Master" CodeBehind="HandyFollowManagement.aspx.vb" Inherits="MehrVosul.HandyFollowManagement" %>

<%@ Register Src="../../../UserControl/Bootstrap_Panel.ascx" TagName="Bootstrap_Panel" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script language="javascript" type="text/javascript">


        var PageNo = 1;
        var PageFilter = null;
        var RecordCount = 0;
        var PageCount = 0;

        function StartthePage() {
            PageMethods.GetPageCount(this.PageFilter, GetPageCount_CallBack);
            return true;
        }

        function btnLastPage_Click() {
            var txtPageCounter = document.getElementById("txtPageCounter");

            if (this.PageCount == 0) {
                txtPageCounter.value = "0";
                return false;
            }

            txtPageCounter.value = this.PageCount;
            this.PageNo = this.PageCount;
            ShowPage();
            return false;
        }

        function btnNextPage_Click() {

            var txtPageCounter = document.getElementById("txtPageCounter");

            if (this.PageCount == 0) {
                txtPageCounter.value = "0";
                return false;
            }

            if (this.PageNo >= this.PageCount) {
                this.PageNo = this.PageCount;
                txtPageCounter.value = this.PageNo;
                return false;
            }
            this.PageNo++;
            txtPageCounter.value = this.PageNo;

            ShowPage();
            return false;
        }

        function btnPreviousPage_Click() {
            var txtPageCounter = document.getElementById("txtPageCounter");

            if (this.PageCount == 0) {
                txtPageCounter.value = "0";
                return false;
            }

            if (this.PageNo <= 1) {
                this.PageNo = 1;
                txtPageCounter.value = "1";
                return false;
            }
            this.PageNo--;
            txtPageCounter.value = this.PageNo;

            ShowPage();
            return false;
        }

        function btnFirstPage_Click() {
            var txtPageCounter = document.getElementById("txtPageCounter");

            if (this.PageCount == 0) {
                txtPageCounter.value = "0";
                return false;
            }

            txtPageCounter.value = "1";
            this.PageNo = 1;
            ShowPage();
            return false;
        }


        function GetPageCount_CallBack(result) {

            var tblMResult = document.getElementById("tblMResult")

            var i;


            while (tblMResult.rows.length > 1) {
                tblMResult.deleteRow(1);
            }

            if (this.PageNo > result[1])
                this.PageNo = result[1];

            this.RecordCount = result[0];
            this.PageCount = result[1];

            var txtPageCounter = document.getElementById("txtPageCounter");

            if (this.RecordCount == 0) {
                var spnTableFooyerText = document.getElementById("spnTableFooyerText");
                spnTableFooyerText.innerHTML = "رکوردی یافت نشد"
                txtPageCounter.value = "0";
                return;
            }


            var txtPageCounter = document.getElementById("txtPageCounter");
            txtPageCounter.value = this.PageNo;


            PageMethods.GetPageRecords(this.PageNo, this.PageFilter, GetPageRecords_CallBack);

        }



        function GetPageRecords_CallBack(result) {
            var btnSearch = document.getElementById("ContentPlaceHolder1_Bootstrap_Panel1_btnSearch");
            var txtPanelSearch = document.getElementById("ContentPlaceHolder1_Bootstrap_Panel1_txtSearchBox");

            txtPanelSearch.value = this.PageFilter;
            btnSearch.disable = false;


            if (result == "E") {
                alert("در فرایند دریافت اطلاعات خطا روی داده است");
                return;
            }
            var tblMResult = document.getElementById("tblMResult")
            var arrL = new Array();
            arrL = result.split(";n;");
            var strHtml = "";

            var intFrom;
            var intTo;


            for (k = 1; k < arrL.length; k++) {

                var arrV;
                arrV = arrL[k].split(";@;");


                strHtml += "<tr>";

                var PKey = parseInt(arrV[1]);
                var intRowID = parseInt(arrV[2]);
                var strEditCell = arrV[3];

                if (k == 1) {
                    intFrom = intRowID;
                }
                if (k == arrL.length - 1) {
                    intTo = intRowID;
                }

                strHtml += "<td title='انتخاب سطر جاری'><input type='checkbox' value='' /></td>";
                strHtml += "<td title='ردیف " + intRowID.toString() + "' ><input type='hidden' value='" + PKey.toString() + "'>" + intRowID.toString() + "</td>"
                strHtml += "<td title='برای ویرایش روی لینک، کلیک نمایید'><a href='#' onclick='btnEdit_ClientClick(" + PKey + ")'>&nbsp;" + strEditCell + "&nbsp;</a></td>";

                var i;
                for (i = 4; i < arrV.length; i++) {
                    strHtml += "<td>" + arrV[i] + "</td>";
                }




                strHtml += "</tr>";

            }

            tblMResult.innerHTML += strHtml;

            var chkSelectAll = document.getElementById("chkSelectAll");
            chkSelectAll.checked = false;

            var spnTableFooyerText = document.getElementById("spnTableFooyerText");
            spnTableFooyerText.innerHTML = "نمایش ردیف های " + intFrom.toString() + " تا " + intTo.toString() + " از " + this.RecordCount.toString() + " رکورد" + " (صفحه " + this.PageNo + " از " + this.PageCount + " صفحه)";


        }



        function chkSelectAll_Click() {

            var tblMResult = document.getElementById("tblMResult")
            var chkSelectAll = document.getElementById("chkSelectAll");
            var i;

            for (i = 1; i < tblMResult.rows.length; i++) {


                tblMResult.rows[i].cells[0].firstChild.checked = chkSelectAll.checked;
            }
            return true;
        }

        function DeleteOperation_Validate() {
            var tblMResult = document.getElementById("tblMResult")
            var theKeys = new Array();


            for (i = 1; i < tblMResult.rows.length; i++) {
                if (tblMResult.rows[i].cells[0].firstChild.checked) {

                    theKeys[theKeys.length] = tblMResult.rows[i].cells[1].firstChild.value;

                }
            }


            if (theKeys.length == 0) {
                alert("رکوردی انتخاب نشده است");
                return false;
            }

            if (!confirm("حذف شدن رکورد(های) انتخاب شده را تایید نمایید"))
                return false;

            PageMethods.DeleteOperation_Server(theKeys, DeleteOperation_Validate_CallBack);

            return false;


        }

        function DeleteOperation_Validate_CallBack(result) {


            if (result.startsWith == "E") {
                alert("فرایند حذف با شکست مواجه شده است: " + result.substring(1));
            }
            else if (result != "") {
                alert(result);
            }


            StartthePage();
        }



        function btnGoPage_Click() {
            var txtPageCounter = document.getElementById("txtPageCounter");

            if (this.PageCount == 0) {
                txtPageCounter.value = "0";
                return false;
            }

            var intPageNo = parseInt(txtPageCounter.value);
            if (intPageNo <= 0 || isNaN(intPageNo))
                intPageNo = 1;

            if (intPageNo > this.PageCount)
                intPageNo = this.PageCount;

            txtPageCounter.value = intPageNo;
            this.PageNo = intPageNo;
            ShowPage();
            return false;

        }

        function ShowPage() {
            var tblMResult = document.getElementById("tblMResult")

            while (tblMResult.rows.length > 1) {
                tblMResult.deleteRow(1);
            }


            PageMethods.GetPageRecords(this.PageNo, this.PageFilter, GetPageRecords_CallBack);
            //   window.scrollBy(0, window.innerHeight);
            return false;


        }

        function SearchOperation_Validate() {
            var txtPanelSearch = document.getElementById("ContentPlaceHolder1_Bootstrap_Panel1_txtSearchBox");
            var btnSearch = document.getElementById("ContentPlaceHolder1_Bootstrap_Panel1_btnSearch");

            if (trimall(txtPanelSearch.value) == "")
                this.PageFilter = null;
            else
                this.PageFilter = trimall(txtPanelSearch.value);
            this.PageNo = 1;

            txtPanelSearch.value = "در حال پردازش ..."
            btnSearch.disable = true;


            StartthePage();


            return false;
        }




        function btnEdit_ClientClick(Pkey) {
            var hdnAction_Name = "<%=hdnAction.ClientID%>";
            var hdnAction = document.getElementById(hdnAction_Name);
            hdnAction.value = "E;" + Pkey;
            window.document.forms[0].submit();
            return false;
        }



        function btnNotification_ClientClick(Pkey) {
            var hdnAction_Name = "<%=hdnAction.ClientID%>";
            var hdnAction = document.getElementById(hdnAction_Name);
            hdnAction.value = "C;" + Pkey;
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
                <div class="panel-body">
                    <div class="table-responsive">
                        <table class="table table-striped table-bordered table-hover" id="tblMResult">
                            <thead>
                                <tr>
                                    <th class="auto-style1">
                                        <input type="checkbox" value="" id="chkSelectAll" onclick="return chkSelectAll_Click();" title="Selects/Deselects all rows" /></th>
                                    <th class="auto-style1">#</th>
                                    <th class="auto-style1">شماره وام</th>
                                    <th class="auto-style1">شماره مشتری</th>
                                    <th class="auto-style1">تاریخ تخصیص</th>
                                    <th class="auto-style1">نام کاربری</th>
                                    <th class="auto-style1">وضعیت تخصیص</th>

                                </tr>
                            </thead>
                            <tbody>
                            </tbody>
                        </table>

                        <div class="row">
                            <div class="col-sm-8" style="padding-top: 5px">
                                <span id="spnTableFooyerText">رکوردی یافت نشد</span>
                            </div>
                            <div class="col-sm-4">

                                <div class="input-group">
                                    <button id="btnLastPage" class="btn btn-default" title="صفحه آخر" style="float: right" onclick="return btnLastPage_Click();"><i class="fa fa-fast-forward"></i></button>
                                    <button id="btnNextPage" class="btn btn-default" title="صفحه بعد" style="float: right" onclick="return btnNextPage_Click();"><i class="fa fa-forward"></i></button>
                                    <button id="btnGoPage" class="btn btn-primary" title="برو به صفحه" style="float: right" onclick="return btnGoPage_Click();"><i class="fa fa-hand-o-up"></i></button>


                                    <input type="text" class="form-control" title="شماره صفحه" id="txtPageCounter" maxlength="8" style="width: 100px; text-align: center; float: right" />
                                    <button id="btnPreviousPage" class="btn btn-default" title="صفحه قبل" style="float: right" onclick="return btnPreviousPage_Click();"><i class="fa fa-backward"></i></button>
                                    <button id="btnFirstPage" class="btn btn-default" title="صفحه اول" style="float: right" onclick="return btnFirstPage_Click();"><i class="fa fa-fast-backward"></i></button>



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
