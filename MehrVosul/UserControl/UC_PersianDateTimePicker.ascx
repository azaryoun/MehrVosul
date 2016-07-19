<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="UC_PersianDateTimePicker.ascx.vb" Inherits="MehrVosul.UC_PersianDateTimePicker" %>
<%-- <script src="<%=strAbsPath & "JavaScript/JsFarsiCalendar.js" %>"   type="text/javascript"></script>--%>


<script src="../Scripts/jquery.ui.datepicker-cc.all.min.js" type="text/javascript"></script>
 <script type="text/javascript" language="javascript">

     $(function () {
         if (typeof (Sys) != "undefined") {
             var prm = Sys.WebForms.PageRequestManager.getInstance();
             prm.add_pageLoaded(MakeCalender);

         }
         else {

             MakeCalender();

         }
     
         function MakeCalender() {
             var txtDateName = "<%=txtDate.ClientID%>";
             $("#" + txtDateName).datepicker({
                 showOn: "button",
                 buttonImage: "/Images/PersianDatePicker/cal.gif",
                 dateFormat: 'yy/mm/dd',
                 autoSize: true,
                 yearRange: "1320:1450",
                 changeMonth: true,
                 changeYear: true

             });
         }

     });
 </script>
<link href="<%=strAbsPath & "App_Themes/Normal/calendar.css"%>"  rel="stylesheet" type="text/css" />
<table border="0" cellpadding="0" cellspacing="0" dir="rtl" width="100%" >
<tr>
<td>
   <%-- <asp:ImageButton ID="imgCalender" runat="server"  ImageUrl="~/Images/PersianDatePicker/cal.gif" TabIndex="2" />--%>
  
</td>
<td  align="right"   >
    <asp:TextBox ID="txtDate" runat="server" MaxLength="10" 
        style="text-align:center;direction:ltr"   TabIndex="1"  ReadOnly="true">
        </asp:TextBox>
</td>
</tr>
</table>