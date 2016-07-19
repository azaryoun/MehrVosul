<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="WebForm1.aspx.vb" Inherits="MehrVosul.WebForm1" %>



<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
      <script src="/assets/js/jquery-1.10.2.js" type="text/javascript"></script>
          <script src="/assets/js/bootstrap.min.js" type="text/javascript"></script>

    <script src="/assets/js/bootstrap-datetimepicker.min.js" type="text/javascript"></script>

      <link href="/assets/css/bootstrap.css" rel="stylesheet" />
    <link rel="stylesheet" href="/assets/css/bootstrap-datetimepicker.min.css" />
      <link rel="stylesheet" href="/assets/css/bootstrap-combined.min.css" />

  
 

</head>
<body>
    <form id="form1" runat="server">

 <div id="datetimepicker" class="input-append date">
      <input type="text"></input>
      <span class="add-on">
        <i data-time-icon="icon-time" data-date-icon="icon-calendar"></i>
      </span>
    </div>
 
 
  
    </script>
    <script type="text/javascript">
        $('#datetimepicker').datetimepicker({
            format: 'dd/MM/yyyy hh:mm:ss',
            language: 'pt-US'


        });
    </script>
    </form>
</body>
</html>
