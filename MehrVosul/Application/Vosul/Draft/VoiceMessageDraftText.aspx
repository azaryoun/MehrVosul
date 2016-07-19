<%@ Page Title=""  Async="true" Language="vb" AutoEventWireup="false" MasterPageFile="~/MasterPage/SmartIntergace_Master.Master" CodeBehind="VoiceMessageDraftText.aspx.vb" Inherits="MehrVosul.VoiceMessageDraftText" EnableEventValidation="false"  %>
<%@ Register src="../../../UserControl/Bootstrap_Panel.ascx" tagname="Bootstrap_Panel" tagprefix="uc1" %>


<%@ Register src="../../../UserControl/UC_TimePicker.ascx" tagname="UC_TimePicker" tagprefix="uc2" %>


<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script language="javascript" type="text/javascript">

   
        function StartthePage() {
           
            return true;
        }

        function SaveOperation_Validate() {

            
            var hdnColumnNumbers_Name = "<%=hdnColumnNumbers.ClientID%>";
            var hdnColumnNumbers = document.getElementById(hdnColumnNumbers_Name);

            var lstSMSText_Name = "<%=lstSMSText.ClientID%>";
            var lstSMSText = document.getElementById(lstSMSText_Name);

            var i;
            var strOutput = "";
            for (i = 0; i < lstSMSText.options.length; i++)
                if (lstSMSText.options[i].value >2) {
                    strOutput += ";" + lstSMSText.options[i].value;

                   
                }
                else {
                    strOutput += ";~?" + lstSMSText.options[i].value;
                }



          hdnColumnNumbers.value = strOutput;



            return true;
        }


        function btnSenSMS_Click() {


            var txtMobile_Name = "<%=txtMobile.ClientID%>";
            var txtMobile = document.getElementById(txtMobile_Name);

            var div_Msg_Name = "<%=div_Msg.ClientID%>";
            var div_Msg = document.getElementById(div_Msg_Name);

         
            
            if (trimall(div_Msg.innerHTML) == "") {
                alert("متن نهایی هنوز مشخص نشده است");
                return false;
            }

            if (trimall(txtMobile.value) == "") {
                alert("شماره موبایل را وارد نمایید");
                txtMobile.focus();
                return false;
            }
         
            var myRe = new RegExp("^[0][9][1][0-9]{8}$");
            var myRe1 = new RegExp("^[0][9][3][0|3|5-9][0-9]{7}$");
            var myRe2 = new RegExp("^[0][9][0][1|2][0-9]{7}$");
            var myRe3 = new RegExp("^[0][9][3][2|4][0-9]{7}$");
            var myRe4 = new RegExp("^[0][9][2][0-9]{8}$");

            if (myRe.test(txtMobile.value) == false && myRe1.test(txtMobile.value) == false && myRe2.test(txtMobile.value) == false && myRe3.test(txtMobile.value) == false && myRe4.test(txtMobile.value) == false) {

                alert("فرمت شماره موبایل صحیح نمی باشد");
                txtMobile.focus();
                return false;


            }


              var hdnColumnNumbers_Name = "<%=hdnColumnNumbers.ClientID%>";
            var hdnColumnNumbers = document.getElementById(hdnColumnNumbers_Name);

            var lstSMSText_Name = "<%=lstSMSText.ClientID%>";
            var lstSMSText = document.getElementById(lstSMSText_Name);

            var i;
            var strOutput = "";
            for (i = 0; i < lstSMSText.options.length; i++)
                if (lstSMSText.options[i].value >2) {
                    strOutput += ";" + lstSMSText.options[i].value;

                   
                }
                else {
                    strOutput += ";~?" + lstSMSText.options[i].value;
                }



          hdnColumnNumbers.value = strOutput;


            
        }


        function btnAddColumns(colHref) {
      
            var lstSMSText_Name = "<%=lstSMSText.ClientID%>";
            var lstSMSText = document.getElementById(lstSMSText_Name);

            var option = document.createElement("option");
            option.value = colHref;
          

            switch (colHref) {
            
                case 1:
                    option.text = "(شماره تسهیلات)";
                    break;
                case 2:
                    option.text = "(تعداد روز تاخیر)";
                    break;   
             
            }


            lstSMSText.add(option);


            CreateSampleText();


            return true;
        }


        function CreateSampleText() {

            
            var lstSMSText_Name = "<%=lstSMSText.ClientID%>";
            var lstSMSText = document.getElementById(lstSMSText_Name);

            var i;
            var strSampleText = "";
            for (i = 0; i < lstSMSText.options.length; i++)
                if (lstSMSText.options[i].value > 2) {
                    strSampleText += " " + lstSMSText.options[i].text;
                }
                else {




                    switch (lstSMSText.options[i].value) {
                       
                        case "1":
                            strSampleText += " " + "578956-5653"
                            break;
                        case "2":
                            strSampleText += " " + "23"
                            break;
                       
        
                    }//switch



                } //else


                strSampleText = trimall(strSampleText);

                var div_Msg_Name = "<%=div_Msg.ClientID%>";
                var div_Msg = document.getElementById(div_Msg_Name);
                div_Msg.innerHTML = strSampleText;


        }

        function RemoveColumns() {

         
            var lstSMSText_Name = "<%=lstSMSText.ClientID%>";
            var lstSMSText = document.getElementById(lstSMSText_Name);

        
            if (lstSMSText.options.length == 0) {
                alert('سطری برای حذف وجود ندارد');
                return false;
            }
          
             if (lstSMSText.selectedIndex < 0) {
                alert('یک سطر را انتخاب نمایید');
                return false;
            }

            lstSMSText.remove(lstSMSText.selectedIndex);

            CreateSampleText();

            lstSMSText.focus();
            lstSMSText.selectedIndex = lstSMSText.selectedIndex - 1;
            return true;

        }


        function AddExtraMsg() {

            
            var lstSMSText_Name = "<%=lstSMSText.ClientID%>";
            var lstSMSText = document.getElementById(lstSMSText_Name);
            var cmbVoiceRecords_Name = "<%=cmbVoiceRecords.ClientID%>";
            var cmbVoiceRecords = document.getElementById(cmbVoiceRecords_Name);

            if (trimall(cmbVoiceRecords.value) == -1) {

                alert('پیامک صوتی را انتخاب نمایید');
                cmbVoiceRecords.focus();
                return false;
         
            }


            var option = document.createElement("option");
            option.value = trimall(cmbVoiceRecords.value);
            option.text = trimall(cmbVoiceRecords[cmbVoiceRecords.selectedIndex].text);
            lstSMSText.add(option);
            CreateSampleText();
            cmbVoiceRecords.selectedIndex = 0;

            return true;
        }



        function lstSMSText_Click() {


            
            var lstSMSText_Name = "<%=lstSMSText.ClientID%>";
            var lstSMSText = document.getElementById(lstSMSText_Name);
            var cmbVoiceRecords_Name = "<%=cmbVoiceRecords.ClientID%>";
            var cmbVoiceRecords = document.getElementById(cmbVoiceRecords_Name);

            if (lstSMSText.selectedIndex < 0) {
                return false;
            }

            if (lstSMSText.options[lstSMSText.selectedIndex].value >2)
            cmbVoiceRecords.value = lstSMSText.options[lstSMSText.selectedIndex].value;


        
        
        }

  </script>



</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">


    <uc1:Bootstrap_Panel ID="Bootstrap_Panel1" runat="server"  />
<div class="row">
  <br />
    <div class="col-md-12">
                   
                  <div class="panel panel-default">
                        <div class="panel-heading">
                           <label>الگوی متن</label>
                          
                           <asp:Label ID="lblTitle" runat="server" Text=""></asp:Label>
                </div>
               
                        <div class="panel-body">
                            <div  class="row">
                                <div class="col-md-12">
                                   
                                 
                                          <div class="form-group has-error">
                                            <label>اطلاعات فرد</label>
                                          <br />
                                         
                                            <a href='#' onclick='return btnAddColumns(1);'>شماره تسهیلات</a>
                                          -
                                            <a href='#' onclick='return btnAddColumns(2);' >تعداد روز تاخیر</a>
                                        
                                        
                                           
                                        </div>
                                      <div class="form-group has-error">
                                            <label>لیست پیامکهای صوتی</label>
                                          <br />
                                            <asp:DropDownList ID="cmbVoiceRecords" runat="server" 
                                               CssClass="form-control" DataSourceID="odsVoiceRecordsList" DataTextField="Name" DataValueField="ID">  </asp:DropDownList>
                                        <span class="form-group input-group-btn">
                                                          
                                                 <a  id="btnAddToText" title="اضافه به الگوی متن"  class="btn btn-success" onclick="AddExtraMsg();"><i class="fa fa-plus-circle fa-lg"></i></a>
                                               
                                                  
                                                </span>
                                        </div>
                                    
                                            <label>متن ثبت شده</label>
                                              <div style="vertical-align:middle;" class="form-group input-group input-group-sm">
                                            
                                          
                                             <asp:ListBox runat="server"  CssClass="form-control"   ID="lstSMSText" Height="150px">
                                            </asp:ListBox>
                                           
                                                <span class="form-group input-group-btn">
                                                  <a  id="btnRemoveFromText" title="حذف از الگوی متن"  class="btn btn-danger" onclick="RemoveColumns();"><i class="fa fa-minus-circle fa-lg"></i></a>
                                                </span>
                                        
                                            
                                        </div>
                                        <div>
                                             <div class="form-group has-error"> <asp:Label Visible="false"  ID="lblCharchterCounter" runat="server" Text="70" Font-Bold="true"></asp:Label>
                                                 <asp:Label Visible ="false" ID="lblSMSCounter" runat="server" Text="(1)" Font-Bold="true"></asp:Label>
                                            </div>
                                        </div>

                                           <label>متن نهایی</label>
                                        <div id="div_Msg" dir="rtl" lang="fa" rows="2" cols="20" disabled="disabled"
                                                runat="server">
                                            </div>

                                            <br />
                                                  
                                                     
                                                <label>ارسال تستی پیامک صوتی الگو به تلفن همراه</label>
                                          <div class="form-group input-group input-group-sm">
                                            <asp:TextBox  ID="txtMobile"  CssClass="form-control"  runat="server"  placeholder="شماره تلفن همراه را وارد نمایید" ></asp:TextBox>
                                                          
                                              <span class="form-group input-group-btn">
                                    
                                    <asp:LinkButton CssClass="btn btn-success" ID="btnSenSMS" runat="server" ToolTip="ارسال پیامک تستی"><i class="fa fa-mobile fa-2x"></i> </asp:LinkButton>
  
   
                                    
                                                 </span>
                                             </div>
</div> 

</div> </div> </div> </div>
        
    </div>     

     
     <asp:HiddenField ID="hdnAction" runat="server" />
     <asp:HiddenField ID="hdnColumnNumbers" runat="server" />

   
 
   
    <asp:ObjectDataSource ID="odsVoiceRecordsList" runat="server" OldValuesParameterFormatString="original_{0}" SelectMethod="GetData" TypeName="BusinessObject.dstZamanakTableAdapters.spr_Record_List_SelectTableAdapter">
        <SelectParameters>
            <asp:Parameter Name="Action" Type="Int32" />
            <asp:Parameter Name="FK_UserID" Type="Int32" />
            <asp:Parameter Name="ID" Type="Int32" />
            <asp:Parameter Name="Name" Type="String" />
            <asp:Parameter Name="RecordID" Type="Int32" />
        </SelectParameters>
    </asp:ObjectDataSource>

   
 
   
</asp:Content>
