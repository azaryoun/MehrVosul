<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/MasterPage/SmartIntergace_Master.Master" CodeBehind="LogCurrentLCStatusReport.aspx.vb" Inherits="MehrVosul.LogCurrentLCStatusReport" %>
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
<asp:Content  ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">


    <uc1:Bootstrap_Panel ID="Bootstrap_Panel1" runat="server" />
<div  class="row">
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
                                              <div class="panel panel-default" >
                                                <div class="panel-heading">
                                               <label>تاریخ</label>
                                                </div>
                                                <div class="panel-body" style="max-height: 200px;">

                                                    
   <uc4:Bootstrap_PersianDateTimePicker ID="Bootstrap_PersianDateTimePicker_From" 
                                                          runat="server" />

                                            
                                             <uc4:Bootstrap_PersianDateTimePicker ID="Bootstrap_PersianDateTimePicker_To" 
                                                              runat="server" />
                                                
                                            


                                            </div> 
                                                 
                                                
                                                    
                                          </div>
                                        </div>
                          
                                          <div class="form-group">
                                            <label>وضعیت</label><asp:DropDownList ID="cmbSuccess" runat="server" 
                                               CssClass="form-control">
                                                <asp:ListItem Value="-1">---</asp:ListItem>
                                                <asp:ListItem Value="1">موفق</asp:ListItem>
                                                <asp:ListItem Value="2">ناموفق</asp:ListItem>
                                             
                                              
                                               
                                                </asp:DropDownList>
                                         </div>
                                      
                          
                            
                            </div>

                             
                                </div>
                        </div>
                           
                           <div class="panel panel-default" id="divResult" runat="server" visible="false">

                             <div class="panel-heading">
                             لیست سوابق بروزرسانی اطلاعات
                        </div>
                        <div class="panel-body">
                            <div class="table-responsive">
                                <table class="table table-striped table-bordered table-hover" id="tblResult" runat="server">
                                    <thead>
                                        <tr>
                                            <th>#</th>
                                            <th>تاریخ</th>
                                            <th>وضعیت</th>
                                            <th>دفعات تکرار</th>
                                            <th>توضیحات</th>
                                           
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
