using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Gscoy.Biz.Baidu;
using Microsoft.VisualStudio.TestTools.UnitTesting;
namespace Gscoy.Biz.Baidu.Tests
{
    [TestClass()]
    public class TranslateHelperTests
    {
        [TestMethod()]
        public void TranslateTest()
        {
            var entity = TranslateHelper.Translate("hello", "", "");
            Assert.Fail();
        }
    }
}
