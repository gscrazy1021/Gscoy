using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Gscoy.Common;
using Microsoft.VisualStudio.TestTools.UnitTesting;
namespace Gscoy.Common.Tests
{
    [TestClass()]
    public class LogNetHelperTests
    {
        [TestMethod()]
        public void InitTest()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public void WriteTest()
        {
            LogNetHelper.Init();
            LogNetHelper.Write("info", LogNetHelper.LogMessageType.Error);
            Assert.Fail();
        }

        [TestMethod()]
        public void WriteTest1()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public void WriteTest2()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public void WriteTest3()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public void AssertTest()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public void AssertTest1()
        {
            Assert.Fail();
        }
    }
}
