<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/MasterPage/SmartIntergace_Master.Master" CodeBehind="DraftManagement.aspx.vb" Inherits="MehrVosul.DraftManagement" %>
<%@ Register src="../../../UserControl/Bootstrap_Panel.ascx" tagname="Bootstrap_Panel" tagprefix="uc1" %>
<%@ Register src="../../../UserControl/UC_TimePicker.ascx" tagname="UC_TimePicker" tagprefix="uc3" %>
<%@ Register src="../../../UserControl/Bootstrap_PersianDateTimePicker.ascx" tagname="Bootstrap_PersianDateTimePicker" tagprefix="uc4" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script language="javascript" type="text/javascript">


        function StartthePage() {
            return true;
        }

        function DisplayOperation_Validate() {


            return true;
        }


        function ExcelOperation_Validate() {



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

                        </div>
                           
                           <div class="panel panel-default" id="divResult" runat="server">
                        <div class="panel-body">
                            <div class="table-responsive">
                                <table class="table table-striped table-bordered table-hover" id="tblResult" runat="server">
                                    <thead>
                                        <tr>
                                            <th>#</th>
                                            <th>نوع اطلاع رسانی</th>
                                             <th>وام گیرنده</th>
                                            <th>ضامن</th>
                                         
                                        </tr>
                                           <tr>
                                            <td>1</td>
                                            <td>پیامک</td>
                                            <td>
                                                  <asp:LinkButton ID="lnkbtnSMSBorrower" runat="server">وام گیرنده</asp:LinkButton>
                                               </td>
                                            <td> <asp:LinkButton ID="lnkbtnSMSSponsor" runat="server">ضامن</asp:LinkButton></td>
                                         
                                        </tr>
                                           <tr>
                                            <td>2</td>
                                            <td>دعوت نامه</td>
                                             <td>
                                                  <asp:LinkButton ID="lnkbtnIntroductionBorrower" runat="server">وام گیرنده</asp:LinkButton>
                                               </td>
                                            <td> <asp:LinkButton ID="lnkbtnIntroductionSponsor" runat="server">ضامن</asp:LinkButton></td>
                                         
                                        </tr>
                                           <tr>
                                            <td>3</td>
                                            <td>اخطاریه</td>
                                             <td> <asp:LinkButton ID="lnkbtnNoticeBorrower" runat="server">وام گیرنده</asp:LinkButton></td>
                                            <td> <asp:LinkButton ID="lnkbtnNoticeSponsor" runat="server">ضامن</asp:LinkButton></td>
                                         
                                        </tr>
                                           <tr>
                                            <td>4</td>
                                            <td>اظهارنامه</td>
                                             <td> <asp:LinkButton ID="lnkbtnManifestBorrower" runat="server">وام گیرنده</asp:LinkButton></td>
                                            <td> <asp:LinkButton ID="lnkbtnManifestSponsor" runat="server">ضامن</asp:LinkButton></td>
                                         
                                        </tr>
                                           <tr>
                                            <td>5</td>
                                            <td>پیامک صوتی</td>
                                             <td> <asp:LinkButton ID="lnkbtnVoiceMSGBorrower" runat="server">وام گیرنده</asp:LinkButton></td>
                                            <td> <asp:LinkButton ID="lnkbtnVoiceMSGSponsor" runat="server">ضامن</asp:LinkButton></td>
                                         
                                        </tr>
                                    </thead>
                             
                                </table>
                            </div>
                        </div>
                    </div>


                       </div>     

                            
                            </div>
                        </div>
             


     
    <asp:HiddenField ID="hdnAction" runat="server" />

</asp:Content>
