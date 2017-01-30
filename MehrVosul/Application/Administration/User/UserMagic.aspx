<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/MasterPage/SmartIntergace_Master.Master" CodeBehind="UserMagic.aspx.vb" Inherits="MehrVosul.UserMagic" %>
<%@ Register src="../../../UserControl/Bootstrap_Panel.ascx" tagname="Bootstrap_Panel" tagprefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script language="javascript" type="text/javascript">

     
        function StartthePage() {
            return true;
        }

        function SaveOperation_Validate() {

          

          
      
            
            var ListBox = document.getElementById('<%=lstAccessGroups.ClientID %>');
            var length = ListBox.length;
            var i = 0;
            var SelectedItemCount = 0;
            var isSelected = false;

            for (i = 0; i < length; i++) {
                if (ListBox.options[i].selected == true) {
                    isSelected = true
                }
            }
            if (isSelected == false)
            {
                alert("گروه دسترسی را مشخص نمائید", "خطا");
              
                return false;
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
                                   
                                 
                                        <div dir="rtl" class="form-group has-error">
                                          
                                            
                                                <asp:FileUpload ID="fleUploadFile" runat="server" />
                                          
                                        </div>
                                    <div class="form-group" >
                                        فایل اکسل شما باید دقیقا طبق فایل نمونه باشد ، فایل نمونه را می توانید
                                           <asp:LinkButton ID="lnkbtnSample" runat="server">از اینجا دانلود</asp:LinkButton>
                                    </div>
                                    
                                     <div class="form-group  has-error">
                                            <label>گروه های دسترسی</label>
                                            <asp:ObjectDataSource ID="odsAccessGroups" runat="server" 
                                                OldValuesParameterFormatString="original_{0}" SelectMethod="GetData" 
                                                TypeName="BusinessObject.dstAccessgroupTableAdapters.spr_Accessgroup_List_SelectTableAdapter">
                                                <SelectParameters>
                                                    <asp:Parameter Name="Action" Type="Int32" />
                                                    <asp:Parameter Name="UserID" Type="Int32" />
                                                </SelectParameters>
                                            </asp:ObjectDataSource>
                                                    <asp:ListBox ID="lstAccessGroups" runat="server"  CssClass="form-control" SelectionMode="Multiple"  DataSourceID="odsAccessGroups" DataTextField="Desp" 
                                                DataValueField="ID"></asp:ListBox>

                                             
                                           </div>
                                    

                            <div class="form-group">
                                            &nbsp;<asp:UpdatePanel ID="UpdatePanel3" runat="server">
                                            </asp:UpdatePanel>
                                        </div>
                                        <div  class="form-group">
                                              &nbsp;<asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                    </asp:UpdatePanel>
                                        </div>
                                                <div class="form-group">
                                                    <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                                                    </asp:UpdatePanel>
                                                    
                                          </div>
                            
                            </div>



                       </div>     
                            </div>

                    </div>

            </div>
    </div>     

     
    <asp:HiddenField ID="hdnAction" runat="server" />

</asp:Content>
