<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/MasterPage/SmartIntergace_Master.Master" CodeBehind="VoiceMessageFileNew.aspx.vb" Inherits="MehrVosul.VoiceMessageFileNew" %>
<%@ Register src="../../../UserControl/Bootstrap_Panel.ascx" tagname="Bootstrap_Panel" tagprefix="uc1" %>


<%@ Register src="../../../UserControl/UC_TimePicker.ascx" tagname="UC_TimePicker" tagprefix="uc2" %>


<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

    <script src="../../../Scripts/Integrated.js" type="text/javascript"></script>
    <script language="javascript" type="text/javascript">


        function StartthePage() {
            return true;
        }

       
        function SaveOperation_Validate() {

         

            var txtVoiceMessageName = document.getElementById("<%=txtVoiceMessageName.ClientID%>");


            if (trimall(txtVoiceMessageName.value) == "") {
                alert("عنوان فایل صوتی را وارد نمایید");
                txtVoiceMessageName.focus();
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
                                            <label>عنوان فایل صوتی</label>
                                          
                                            <asp:TextBox ID="txtVoiceMessageName" runat="server" cssclass="form-control" MaxLength="50" placeholder="عنوان فایل صوتی را وارد نمایید"></asp:TextBox>
                                          
                                        </div>         
                        <div class="panel panel-default" >
                        <div class="panel-heading">
                        <label>بارگذاری فایل صوتی</label>
                        </div>
                    
                      <div class="form-group has-error">
                                           
                                        
                                           <asp:FileUpload ID="file_contents" runat="server" Width="50%"   style="height:45px" cssclass="form-control" />
                                           
                                       
                                             
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
