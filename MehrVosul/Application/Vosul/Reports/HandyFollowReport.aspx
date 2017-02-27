<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/MasterPage/SmartIntergace_Master.Master" CodeBehind="HandyFollowReport.aspx.vb" Inherits="MehrVosul.HandyFollowReport" %>
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
                                             <label>استان</label>
                                <asp:ObjectDataSource ID="odcProvince" runat="server" 
                                    OldValuesParameterFormatString="original_{0}" SelectMethod="GetData" 
       
                                        TypeName="BusinessObject.dstBranchTableAdapters.spr_ProvinceList_SelectTableAdapter">
                        </asp:ObjectDataSource>
                            
                             
                                    <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                                        <ContentTemplate>
                                            <asp:DropDownList ID="cmbProvince" runat="server" CssClass="form-control" 
                                DataSourceID="odcProvince" DataTextField="Province" 
                            DataValueField="ID" AutoPostBack="True">
                                            </asp:DropDownList>
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                  
                                        </div>

                             <div class="form-group">
                                            <label>شعبه</label>
                                            <asp:ObjectDataSource ID="odsBranch" runat="server" 
                                                OldValuesParameterFormatString="original_{0}" SelectMethod="GetData" 

                                                  
                                                  
                                                
                                                
                                                   TypeName="BusinessObject.dstBranchTableAdapters.spr_Branch_List_SelectTableAdapter">
                                                <SelectParameters>
                                                    <asp:Parameter Name="Action" Type="Int32" />
                                                    <asp:Parameter Name="ProvinceID" Type="Int32" />
                                                </SelectParameters>
                                            </asp:ObjectDataSource>
                                              <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                                  <ContentTemplate>
                                                      <asp:DropDownList ID="cmbBranch" runat="server" CssClass="form-control" 
                                                          AutoPostBack="True">
                                                      </asp:DropDownList>
                                                  </ContentTemplate>
                                            </asp:UpdatePanel>
                                         
                                        </div>
                                             <div class="form-group">
                                            <label>کارشناس پیگیری</label>
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
                                                         <asp:DropDownList ID="cmbPerson" 
    runat="server" CssClass="form-control" AutoPostBack="True" DataSourceID="odsPerson" DataTextField="Username" DataValueField="ID">
                                                         </asp:DropDownList>
                                                     </ContentTemplate>
                                                 </asp:UpdatePanel>
                                              

                                        </div>

                             <div class="form-group">
                                            <label>نوع اطلاع رسانی</label>
                                               <asp:DropDownList ID="cmbNotification" runat="server" CssClass="form-control">
                                                   <asp:ListItem Value="0">(همه)</asp:ListItem>
                                                   <asp:ListItem Value="1">ارسال پیامک</asp:ListItem>
                                                   <asp:ListItem Value="2">تماس تلفنی</asp:ListItem>
                                                   <asp:ListItem Value="3">دعوت نامه</asp:ListItem>
                                                   <asp:ListItem Value="4">اخطاریه</asp:ListItem>
                                                   <asp:ListItem Value="5">اظهارنامه</asp:ListItem>
                                               
                                                </asp:DropDownList>

                                        </div>

                                       
                                      <div class="form-group">
                                            <label>وضعیت تماس</label><asp:DropDownList ID="cmbStatus" runat="server" 
                                               CssClass="form-control">
                                                <asp:ListItem Value="-1">---</asp:ListItem>
                                                <asp:ListItem Value="1">باپاسخ</asp:ListItem>
                                                <asp:ListItem Value="2">بدون پاسخ</asp:ListItem>
                                             
                                              
                                               
                                                </asp:DropDownList>
                                         </div>   
                           

                                          <div class="form-group">
                                            <label>نتیجه تماس</label><asp:DropDownList ID="cmbSuccess" runat="server" 
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
                                 لیست پیگیری های ثبت شده</div>
                        <div class="panel-body">
                            <div class="table-responsive">
                                <table class="table table-striped table-bordered table-hover" id="tblResult" runat="server">
                                    <thead>
                                        <tr>
                                            <th>#</th>
                                            <th>تاریخ</th>
                                            <th>نوع اطلاع رسانی</th>
                                            <th>مخاطب</th>
                                            <th>وضعیت تماس</th>
                                             <th>نتیجه تماس</th>
                                             <th>کارشناس پیگیر</th>
                                             <th>شعبه</th>
                                             <th>ملاحظات</th>
                                             <th>تاریخ تعهد پرداخت</th>
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
