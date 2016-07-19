<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="Bootstrap_Panel.ascx.vb" Inherits="MehrVosul.Bootstrap_Panel" %>
<script language="javascript" type="text/javascript">
    $(document).ready(function () {
        $(window).scroll(function () {
          
            if ($(this).scrollTop() > 200) {
                $("#divMainPanel").addClass("FixPanel");
            } else {
                $("#divMainPanel").removeClass("FixPanel");
            }
        });
    });

</script>
<style type="text/css">
  
    .FixPanel
    {
        position: fixed;
        top: 0;
        z-index: 999999999;
    }
</style>


<div class="row" id="divMainPanel">
<div class="col-md-6">
<div class="btn-group">
     <asp:LinkButton CssClass="btn btn-default" ID="btnUp" runat="server" ToolTip="بازگشت به مرحله پیشین"><i class="fa fa-level-up"  ></i> بالا</asp:LinkButton>
     <asp:LinkButton CssClass="btn btn-info" ID="btnWizard" runat="server" ToolTip="فراخوانی یک معجزه"><i class="fa fa-magic"></i> جادو</asp:LinkButton>

    <asp:LinkButton CssClass="btn btn-danger" ID="btnDelete" runat="server" ToolTip="حذف رکورد (های) انتخاب شده"><i class="fa fa-trash-o" ></i> حذف</asp:LinkButton>
  
    <asp:LinkButton CssClass="btn btn-warning" ID="btnCancel" runat="server" ToolTip="لغو عملیات"><i class="fa fa-times"  ></i> لغو</asp:LinkButton>
  
     <asp:LinkButton CssClass="btn btn-success" ID="btnSave" runat="server" ToolTip="ذخیره سازی عملیات"><i class=" fa fa-check"></i> ذخیره</asp:LinkButton>
  
  <asp:LinkButton CssClass="btn btn-primary" ID="btnNew" runat="server" ToolTip="ایجاد یک رکورد جدید"><i class=" fa fa-plus"></i> جدید</asp:LinkButton>
  
  <asp:LinkButton CssClass="btn btn-danger" ID="btnRejectRequest" runat="server" ToolTip="رد درخواست"><i class=" fa fa-times"></i> رد درخواست</asp:LinkButton>
   
  <asp:LinkButton CssClass="btn btn-success" ID="btnConfirmRequest" runat="server" ToolTip="پذیرش درخواست"><i class=" fa fa-check-circle"></i> پذیرش درخواست</asp:LinkButton>
 
  
  <asp:LinkButton CssClass="btn btn-info" Visible="false" ID="btnImportToPDF" runat="server" ToolTip="انتقال به PDF"><i class=" fa fa-file-pdf-o"></i> انتقال به PDF</asp:LinkButton>
 
  <asp:LinkButton CssClass="btn btn-primary" ID="btnImportToExcel" runat="server" ToolTip="انتقال به اکسل"><i class=" fa fa-file-excel-o"></i> انتقال به اکسل</asp:LinkButton>
  <asp:LinkButton CssClass="btn btn-success" ID="btnDisplay" runat="server" ToolTip="نمایش"><i class=" fa fa-file"></i> نمایش</asp:LinkButton>
 

</div>	
          
</div>
<div class="col-md-5">
  <div class="form-group input-group">
        
      <asp:TextBox ID="txtSearchBox" runat="server" CssClass="form-control" ToolTip="عبارت مورد نظر وارد نمایید" placeholder="عبارت جستجو"></asp:TextBox>



            <span class="input-group-btn">
            <asp:LinkButton CssClass="btn btn-info" ID="btnSearch" runat="server" ToolTip="یافتن عبارت مورد نظر"><i class="fa fa-search"></i> جستجو</asp:LinkButton>

            </span>
          
          </div>	
</div>
</div>

	<div class="form-group" style="display:none" id="divMessageInfo" runat="server">
    <br />
                                    <div class="alert alert-info" role="alert">
                                    <span class="glyphicon glyphicon-ok" aria-hidden="true"></span>
                                   <asp:Label     ID="lblMessageInfo" runat="server" Text=""></asp:Label>
                                    </div>
            							</div>

<div class="form-group" style="display:none" id="divMessageWarning" runat="server">
<br />
  <div class="alert alert-danger" role="alert">
 <span class="glyphicon glyphicon-exclamation-sign" aria-hidden="true"></span>
<asp:Label     ID="lblMessageWarning" runat="server" Text=""></asp:Label>
</div>

</div>