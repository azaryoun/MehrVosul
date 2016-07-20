<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/MasterPage/SmartIntergace_Master.Master" CodeBehind="HadiDraftTextDeposit.aspx.vb" Inherits="MehrVosul.HadiDraftTextDeposit" EnableEventValidation="false"  %>
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
                if (lstSMSText.options[i].value == "") {
                    strOutput += ";" + lstSMSText.options[i].text;

                   
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


            
        }


        function btnAddColumns(colHref) {
      
            var lstSMSText_Name = "<%=lstSMSText.ClientID%>";
            var lstSMSText = document.getElementById(lstSMSText_Name);

            var option = document.createElement("option");
            option.value = colHref;
          

            switch (colHref) {
                case 1:
                    option.text = "(جنسیت صاحب سپرده)";
                    break;
                case 2:
                    option.text = "(نام صاحب سپرده)";
                    break;
                case 3:
                    option.text = "(نام خانوادگی صاحب سپرده)";
                    break;
                case 4:
                    option.text = "(شماره پرونده)";
                    break;
                case 5:
                    option.text = "(تعداد روز از افتتاح حساب)";
                    break;
                case 6:
                    option.text = "(شعبه افتتاح سپرده)";
                    break;
                case 7:
                    option.text = "(نوع سپرده)";
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
                if (lstSMSText.options[i].value == "") {
                    strSampleText += " " + lstSMSText.options[i].text;
                }
                else {




                    switch (lstSMSText.options[i].value) {
                        case "1":
                            strSampleText +=" " + "آقای"
                            break;
                        case "2":
                            strSampleText += " " + "حسین"
                            break;
                        case "3":
                            strSampleText += " " + "قمصری"
                            break;
                        case "4":
                            strSampleText += " " + "578956-5653"
                            break;
                        case "5":
                            strSampleText += " " + "23"
                            break;
                        case "6":
                            strSampleText += " " + "میدان ونک"
                            break;

                        case "7":
                            strSampleText += " " + "سپرده قرض الحسنه جاري"
                            break;
                    }//switch



                } //else


                strSampleText = trimall(strSampleText);

                var div_Msg_Name = "<%=div_Msg.ClientID%>";
                var div_Msg = document.getElementById(div_Msg_Name);
                div_Msg.innerHTML = strSampleText;
                var lblSMSCounter_Name = "<%=lblSMSCounter.ClientID%>";
                var lblSMSCounter = document.getElementById(lblSMSCounter_Name);
                var lblCharchterCounter_Name = "<%=lblCharchterCounter.ClientID%>";
                var lblCharchterCounter = document.getElementById(lblCharchterCounter_Name);
     

                var intSMSCount = Math.ceil(strSampleText.length / 68)
       
                lblSMSCounter.innerHTML = "(" + intSMSCount.toString() + ")";
                lblCharchterCounter.innerHTML = (68 * intSMSCount - strSampleText.length).toString();

                if (intSMSCount > 1) {
                    lblSMSCounter.style.color = "red";
                }
                else { 
                         lblSMSCounter.style.color = "black";
                }
            
            
        

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
            var txt_ExtraMsg_Name = "<%=txt_ExtraMsg.ClientID%>";
            var txt_ExtraMsg = document.getElementById(txt_ExtraMsg_Name);

            if (trimall(txt_ExtraMsg.value) == "") {

                alert('متن را وارد نمایید');
                txt_ExtraMsg.focus();
                return false;
         
            }


            var option = document.createElement("option");
            option.value = "";
            option.text = trimall(txt_ExtraMsg.value);
            lstSMSText.add(option);
            CreateSampleText();
            txt_ExtraMsg.value = "";

            return true;
        }



        function lstSMSText_Click() {

            var lstSMSText_Name = "<%=lstSMSText.ClientID%>";
            var lstSMSText = document.getElementById(lstSMSText_Name);
            var txt_ExtraMsg_Name = "<%=txt_ExtraMsg.ClientID%>";
            var txt_ExtraMsg = document.getElementById(txt_ExtraMsg_Name);

            if (lstSMSText.selectedIndex < 0) {
                return false;
            }

            if (lstSMSText.options[lstSMSText.selectedIndex].value=="")
            txt_ExtraMsg.value = lstSMSText.options[lstSMSText.selectedIndex].text;


        
        
        }


        function Move_Items(direction) {

            var lstSMSText_Name = "<%=lstSMSText.ClientID%>";
            var lstSMSText = document.getElementById(lstSMSText_Name);
            var selIndex = lstSMSText.selectedIndex;

            if (selIndex == -1) {
                alert("Please select an option to move.");
                return;
            }

            var increment = -1;
            if (direction == 'up')
                increment = -1;
            else
                increment = 1;

            if ((selIndex + increment) < 0 ||
                (selIndex + increment) > (lstSMSText.options.length - 1)) {
                return;
            }

            var selValue = lstSMSText.options[selIndex].value;
            var selText = lstSMSText.options[selIndex].text;
            lstSMSText.options[selIndex].value = lstSMSText.options[selIndex + increment].value
            lstSMSText.options[selIndex].text = lstSMSText.options[selIndex + increment].text

            lstSMSText.options[selIndex + increment].value = selValue;
            lstSMSText.options[selIndex + increment].text = selText;

            lstSMSText.selectedIndex = selIndex + increment;

            CreateSampleText();
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
                                            <a  href='#' onclick='return btnAddColumns(1);' >جنسیت(صاحب سپرده)</a>
                                            -
                                            <a href='#'  onclick='return btnAddColumns(2);' >نام(صاحب سپرده)</a>
                                             -
                                            <a  href='#' onclick='return btnAddColumns(3);' >نام خانوادگی(صاحب سپرده)</a>
                                           -
                                            <a href='#' onclick='return btnAddColumns(4);'>شماره پرونده</a>
                                          -
                                            <a href='#' onclick='return btnAddColumns(5);' >تعداد روز از افتتاح حساب</a>
                                           -
                                            <a href='#' onclick='return btnAddColumns(6);' >شعبه</a>
                                             -
                                            <a href='#' onclick='return btnAddColumns(7);' >نوع سپرده</a>
                  
                                          
                                        </div>
                                          <label>متن دلخواه</label>
                                                      <div class="form-group input-group input-group-sm">
                                            
                                            <asp:TextBox ID="txt_ExtraMsg"  CssClass="form-control"  runat="server"  placeholder="متن دلخواه را وارد نمایید" ></asp:TextBox>
                                              <span class="form-group input-group-btn">
                                                          
                                                 <a  id="btnAddToText" title="اضافه به الگوی متن"  class="btn btn-success" onclick="AddExtraMsg();"><i class="fa fa-plus-circle fa-lg"></i></a>
                                               
                                                  
                                                </span>
                                            </div>
                                            <label>متن ثبت شده</label>
                                              <div style="vertical-align:middle;" class="form-group input-group input-group-sm">
                                            
                                          
                                             <asp:ListBox runat="server"  CssClass="form-control"   ID="lstSMSText" Height="150px">
                                            </asp:ListBox>
                                           
                                                <span class="form-group input-group-btn">
                                                  <a  id="btnRemoveFromText" title="حذف از الگوی متن"  class="btn btn-danger" onclick="RemoveColumns();"><i class="fa fa-minus-circle fa-lg"></i></a></span>
                                         <span class="form-group input-group-btn">  <a  id="btnUP" title="بالا"  class="btn btn-success" onclick="Move_Items('up');"><i class="fa fa-caret-square-o-up fa-lg"></i></a></span>
                                              <span class="form-group input-group-btn">    <a  id="btnDown" title="پایین"  class="btn btn-success" onclick="Move_Items('down');"><i class="fa fa-caret-square-o-down fa-lg"></i></a>
                                           </span>  
                                        
                                            
                                        </div>

                                        <div>
                                             <div class="form-group has-error"> <asp:Label ID="lblCharchterCounter" runat="server" Text="70" Font-Bold="true"></asp:Label>
                                                 <asp:Label ID="lblSMSCounter" runat="server" Text="(1)" Font-Bold="true"></asp:Label>
                                            </div>
                                        </div>

                                           <label>متن نهایی</label>
                                        <div id="div_Msg" dir="rtl" lang="fa" rows="2" cols="20" disabled="disabled"
                                                runat="server">
                                            </div>

                                            <br />
                                                  <label>ارسال تستی متن الگو به تلفن همراه</label>
                                                      <div class="form-group input-group input-group-sm">
                                            
                                            <asp:TextBox ID="txtMobile"  CssClass="form-control"  runat="server"  placeholder="شماره تلفن همراه را وارد نمایید" ></asp:TextBox>
                                              <span class="form-group input-group-btn">
                                    
                                    <asp:LinkButton CssClass="btn btn-success" ID="btnSenSMS" runat="server" ToolTip="ارسال پیامک تستی"><i class="fa fa-mobile fa-2x"></i> </asp:LinkButton>
  
   
                                    
                                                 </span>
                                             </div>
</div> 

</div> </div> </div> </div>
        
    </div>     

     
     <asp:HiddenField ID="hdnAction" runat="server" />
     <asp:HiddenField ID="hdnColumnNumbers" runat="server" />

   
 
   
</asp:Content>
