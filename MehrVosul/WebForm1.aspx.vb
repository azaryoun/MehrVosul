Imports System.Data.Entity
Public Class WebForm1
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        Dim ctnx As New BusinessObject.dbMehrVosulEntities1
        Dim dte As Date = Date.Now.Date

        Dim lnq = ctnx.tbl_SelfReport.Where(Function(x) DbFunctions.TruncateTime(x.STime.Value) = dte).ToList()

    End Sub

End Class