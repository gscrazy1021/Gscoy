using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Gscoy.Biz;
using Microsoft.VisualStudio.TestTools.UnitTesting;
namespace Gscoy.Biz.Tests
{
    [TestClass()]
    public class SFBTests
    {
        [TestMethod()]
        public void GetHouseTest()
        {
            var s = new SFB().GetHouse();

            Assert.Fail();
        }
    }
}
