using System;
using China.ICBC.SWIFT.Utils;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Tests.UnitTests
{
    [TestClass]
    public class TransliteratorTest
    {
        [TestMethod]
        public void TransliteratorFrontTest()
        {
            try
            {
                string result = Transliterator.Transliterate("Иванов Иван Иваныч, г.Москва, Солнцевский пр-т, д. 7, к.1, кв. 86");

                Assert.IsTrue(true);
            }
            catch (Exception e)
            {
                Assert.Fail(e.ToString());
            }
        }
    }
}
