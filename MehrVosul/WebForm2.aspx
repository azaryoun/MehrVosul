<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="WebForm2.aspx.vb" Inherits="MehrVosul.WebForm2" %>

<%@ Register src="UserControl/Bootstrap_PersianDateTimePicker.ascx" tagname="Bootstrap_PersianDateTimePicker" tagprefix="uc1" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>

</head>
<body>
    <form id="form1" runat="server">
    <div>
    
        <uc1:Bootstrap_PersianDateTimePicker ID="Bootstrap_PersianDateTimePicker1" 
            runat="server" />
    
    </div>
    <asp:Button ID="Button1" runat="server" Text="Button" />
    <uc1:Bootstrap_PersianDateTimePicker ID="Bootstrap_PersianDateTimePicker2" 
        runat="server" />
    </form>
</body>
</html>
