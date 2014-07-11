using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Gscoy.WeChat.Biz.Handler;
using Microsoft.VisualStudio.TestTools.UnitTesting;
namespace Gscoy.WeChat.Biz.Handler.Tests
{
    [TestClass()]
    public class LocationHandlerTests
    {
        [TestMethod()]
        public void HandleRequestTest()
        {
            LocationHandler handler = new LocationHandler("");
            var ss = handler.HandleRequest();
            Assert.Fail();
        }
    }
}
