using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using China.ICBC;
using China.ICBC.SWIFT;
using China.ICBC.SWIFT.Fields;
using China.ICBC.SWIFT.Fields.Common;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Tests.UnitTests
{
    [TestClass]
    public class NameWithHieroglyphTest
    {
        public const string CONNECTION_STRING = @"data source=ZEUS;initial catalog=Snova_table_СССБ;integrated security=True;multipleactiveresultsets=True;App=EntityFramework";
        //public const string CONNECTION_STRING = @"data source=APOLON;initial catalog=Snova_table_СССБ;integrated security=True;multipleactiveresultsets=True;App=EntityFramework";

        [TestMethod]
        public void GetHieroglyphListTest()
        {
            try
            {
                var result = NameWithHieroglyph.GetHieroglyphList(CONNECTION_STRING, "bao");
                var result2 = NameWithHieroglyph.GetHieroglyphList(CONNECTION_STRING, "Bo");
                var result3 = NameWithHieroglyph.GetHieroglyphList(CONNECTION_STRING, "Xilai");
                var result4 = NameWithHieroglyph.GetHieroglyphList(CONNECTION_STRING, "Xi");
                var result5 = NameWithHieroglyph.GetHieroglyphList(CONNECTION_STRING, "lai");

                Assert.IsTrue(result.Any());
            }
            catch (Exception e)
            {
                Assert.Fail(e.ToString());
            }
        }
    }
}
