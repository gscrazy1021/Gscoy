using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Gscoy.Biz.Sina;
using Microsoft.VisualStudio.TestTools.UnitTesting;
namespace Gscoy.Biz.Sina.Tests
{
    [TestClass()]
    public class DreamBizTests
    {
        [TestMethod()]
        public void GetEntityTest()
        {
            DreamBiz biz = new DreamBiz();
            var entity = biz.GetEntityByID(2);
            Assert.Fail();
        }

        [TestMethod()]
        public void GetEntityByTitleTest()
        {
            DreamBiz biz = new DreamBiz();
            var entity = biz.GetEntityByTitle("护士");
            Assert.Fail();
        }
    }
}
