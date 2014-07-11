using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Gscoy.Common;
using Microsoft.VisualStudio.TestTools.UnitTesting;
namespace Gscoy.Common.Tests
{
    [TestClass()]
    public class ConventJineTests
    {
        [TestMethod()]
        public void GetRMBTest()
        {
            //ConventJine.GetRMB();
            Assert.Fail();
        }

        [TestMethod()]
        public void GetRMBConvertTest()
        {
            var m = ConvertJine.GetRMBConvert("2131.23");
            Assert.Fail();
        }
    }
}
