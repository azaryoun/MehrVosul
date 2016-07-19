<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/MasterPage/SmartIntergace_Master.Master" CodeBehind="AccessgroupEdit.aspx.vb" Inherits="MehrVosul.AccessgroupEdit" %>
<%@ Register src="../../../UserControl/Bootstrap_Panel.ascx" tagname="Bootstrap_Panel" tagprefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script language="javascript" type="text/javascript">

     
        function StartthePage() {
            return true;
        }

        function SaveOperation_Validate() {

            var txtAccessgrouptitle = document.getElementById("<%=txtAccessgrouptitle.ClientID%>");


            if (trimall(txtAccessgrouptitle.value) == "") {
                alert("عنوان گروه دسترسی را وارد نمایید");
                txtAccessgrouptitle.focus();
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
                                   
                                 
                                        <div class="form-group has-error">
                                            <label>عنوان گروه دسترسی</label>
                                          
                                            <asp:TextBox ID="txtAccessgrouptitle" runat="server" cssclass="form-control" MaxLength="50" placeholder="عنوان گروه را وارد نمایید"></asp:TextBox>
                                          
                                        </div>
                                      
                                   

                        <div class="panel panel-default" >
                        <div class="panel-heading">
                        <label>صفحات (اقلام) منو</label>
                        </div>
                        <div class="panel-body" style="max-height: 200px;overflow-y: scroll;">
                      <div class="form-group" runat="server" id="divchklstMenuItems">
                                           
                                        


                                             
                                           </div>
                               
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
