<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/MasterPage/SmartIntergace_Master.Master" CodeBehind="HandyFollowNew.aspx.vb" Inherits="MehrVosul.HandyFollowNew" %>
<%@ Register src="../../../UserControl/Bootstrap_Panel.ascx" tagname="Bootstrap_Panel" tagprefix="uc1" %>
<%@ Register src="../../../UserControl/UC_TimePicker.ascx" tagname="UC_TimePicker" tagprefix="uc3" %>
<%@ Register src="../../../UserControl/Bootstrap_PersianDateTimePicker.ascx" tagname="Bootstrap_PersianDateTimePicker" tagprefix="uc4" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script language="javascript" type="text/javascript">


        function StartthePage() {


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
                       
                           <div class="panel panel-default" id="divResult" runat="server" >
                        <div class="panel-heading">
                             لیست پیگیریهای ثبت شده</div>
                        <div class="panel-body">
                        
                            <div class="table-responsive">
                                <table class="table table-striped table-bordered table-hover" id="tblResult" runat="server">
                                    <thead>
                                        <tr>
                                           
                                            <th>#</th>
                                            <th> تاریخ</th>
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
                            <div class="row">
                             
                                    <div class="col-md-6">
                                 

                                        <div class="form-group has-error">
                                            <label>نوع اطلاع رسانی</label>
                                          
                                            <asp:DropDownList ID="cmbNotificationType" runat="server" 
                                               CssClass="form-control">
                                              
                                                <asp:ListItem Value="1"> ارسال پیامک</asp:ListItem>
                                                <asp:ListItem Value="2" Selected ="True"> تماس تلفنی</asp:ListItem>
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

                                        
                                     <div class="form-group has-error">
                                            <label>نتیجه تماس</label>
                                          
                                         <div class="panel-body" style="max-height: 200px;">
                                        <asp:RadioButtonList ID="rdbListNotificationStatus" runat="server">
                                            <asp:ListItem Selected="True" Value="1">مثبت</asp:ListItem>
                                            <asp:ListItem Value="0">منفی</asp:ListItem>
                                        </asp:RadioButtonList>
                                     </div>
                                            
                                        </div>           
                                        
                                      <div class="form-group has-error">
                                              <div class="panel panel-default" >
                                                <div class="panel-heading">
                                               <label>تاریخ و ساعت تماس</label>
                                                </div>
                                                <div class="panel-body" style="max-height: 200px;">

                                                    
                                                <uc4:Bootstrap_PersianDateTimePicker ID="Bootstrap_PersianDateTimePicker_From" 
                                                          runat="server" />

                                         

                                            </div>
                                                 
                                                
                                                    
                                          </div>
                                        </div>               
                              
                          </div>
                                  <div class="col-md-6">
                          
                                    <div class="form-group has-error">
                                              <div class="panel panel-default" >
                                                <div class="panel-heading">
                                               <label>تاریخ تعهد پرداخت</label>
                                                </div>
                                                <div class="panel-body" style="max-height: 200px;">

                                                    
                                                <uc4:Bootstrap_PersianDateTimePicker ID="Bootstrap_PersianDateTimePicker_TO" 
                                                          runat="server" />

                                         

                                            </div>
                                                 
                                                
                                                    
                                          </div>
                                        </div>         
                                    
                                    <div class="form-group has-error">
                                            <label>مخاطب</label>
                                          
                                         <div class="panel-body" style="max-height: 200px;">
                                        <asp:RadioButtonList ID="rdboToSponsor" runat="server">
                                            <asp:ListItem Selected="True" Value="0">وام گیرنده</asp:ListItem>
                                            <asp:ListItem Value="1">ضامن</asp:ListItem>
                                        </asp:RadioButtonList>
                                    </div>
                                            
                                        </div>

                                                                       
                                        <div class="form-group"><asp:ObjectDataSource ID="odcSponsor" 
                                                runat="server" OldValuesParameterFormatString="original_{0}" 
                                                SelectMethod="GetData" 
                                                TypeName="BusinessObject.dstLoanSponsorTableAdapters.spr_LoanSponsor_List_SelectTableAdapter">
                                            <SelectParameters>
                                                <asp:Parameter Name="LoanID" Type="Int32" />
                                            </SelectParameters>
                                            </asp:ObjectDataSource>
                                            <label>ضامن ها</label>
&nbsp;<asp:DropDownList ID="cmbSponsor" runat="server" 
                                               CssClass="form-control" DataSourceID="odcSponsor" 
                                                DataTextField="FileName" DataValueField="FK_SponsorID">
                 
                                                </asp:DropDownList>
                                            
                                        </div>

                                 <div class="form-group has-error">
                                <label>توضیحات</label>
                                          
                                <asp:TextBox ID="txtRemark" runat="server" cssclass="form-control"  
                                         placeholder="توضیحات را وارد کنید" Height="100px" TextMode="MultiLine"></asp:TextBox>
                                            
                            </div>   

                       </div>    
                       
                 
                                  
                                                       
                                </div> 
                            </div>

                    

            </div>  </div>

                              <span class="form-group input-group-btn">
                                                          
                                                
                                               <asp:LinkButton CssClass="btn btn-success" ID="btnAddToText" runat="server" ToolTip="ثبت پیگیری"><i class="fa fa-plus-circle fa-lg"></i> </asp:LinkButton>
                                                  
                                                </span>
                      
                   
    </div>

     
    <asp:HiddenField ID="hdnAction" runat="server" />

</asp:Content>
