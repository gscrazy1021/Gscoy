using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Gscoy.Common;
using Microsoft.VisualStudio.TestTools.UnitTesting;
namespace Gscoy.Common.Tests
{
    [TestClass()]
    public class MailHelperTests
    {
        [TestMethod()]
        public void SendMailTest()
        {
            MailHelper mail = new MailHelper();
            mail.Body = "body";
            mail.Priority = MailHelper.PriorityEnum.High;
            mail.Subject = "subject";
            mail.MailCc = "crazyresult@126.com";
            mail.MailTo = "crazyresult@126.com";
            mail.AttachUrl = @"D:\own\Gscoy.zip";
            var re=mail.SendMailAttach();
            Assert.Fail();
        }

        [TestMethod()]
        public void SendMailAttachTest()
        {
            Assert.Fail();
        }
    }
}
