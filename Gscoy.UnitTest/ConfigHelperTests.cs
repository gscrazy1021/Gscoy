using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Gscoy.Common;
using Microsoft.VisualStudio.TestTools.UnitTesting;
namespace Gscoy.Common.Tests
{
    [TestClass()]
    public class ConfigHelperTests
    {
        [TestMethod()]
        public void GetConfigParseTest()
        {
            var value = ConfigHelper.GetConfig<int>("version",1);
            Assert.Fail();
        }
    }
}
