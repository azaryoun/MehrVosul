Imports System.Text
Imports Microsoft.VisualStudio.TestTools.UnitTesting

<TestClass()> Public Class UnitTest1

    <TestMethod()> Public Sub TestMethod1()


        Dim ctnx As New BusinessObject.dbMehrVosulEntities1
        Dim dte As Date = Date.Now.Date
        '     Dim lnq = ctnx.tbl_SelfReport.Where(Function(x) x.STime.Value.Date = dte).ToList()




    End Sub

End Class