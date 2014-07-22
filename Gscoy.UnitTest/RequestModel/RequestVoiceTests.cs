using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Gscoy.WeChat.Model.RequestModel;
using Microsoft.VisualStudio.TestTools.UnitTesting;
namespace Gscoy.WeChat.Model.RequestModel.Tests
{
    [TestClass()]
    public class RequestVoiceTests
    {
        [TestMethod()]
        public void LoadFromXmlTest()
        {
            var responseXML = string.Format(@"<xml>
<ToUserName><![CDATA[toUser]]></ToUserName>
<FromUserName><![CDATA[fromUser]]></FromUserName>
<CreateTime>1357290913</CreateTime>
<MsgType><![CDATA[voice]]></MsgType>
<MediaId><![CDATA[media_id]]></MediaId>
<Format><![CDATA[Format]]></Format>
<MsgId>1234567890123456</MsgId>
</xml>");
            var entity = RequestVoice.LoadFromXml(responseXML);
            Assert.Fail();
        }
    }
}
