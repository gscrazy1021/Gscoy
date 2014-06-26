using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Gscoy.Service.WebService;
using Microsoft.VisualStudio.TestTools.UnitTesting;
namespace Gscoy.Service.WebService.Tests
{
    [TestClass()]
    public class WebServiceHelperTests
    {
        [TestMethod()]
        public void WebServiceHelperTest()
        {
            string url = "http://webservice.webxml.com.cn/webservices/qqOnlineWebService.asmx";
            string @namespace = "";
            string methodname = "qqCheckOnline";//需要调用的webservice中的方法
            string Invoke = "315455372";//QQ号码
            string result = WebService.WebServiceHelper.InvokeWebservice(url, @namespace, methodname, Invoke).ToString();
            Assert.Fail();
        }

        [TestMethod()]
        public void InvokeWebserviceTest()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public void InvokeWebserviceTest1()
        {
            Assert.Fail();
        }
    }
}
