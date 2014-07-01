using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Gscoy.WeChat.Biz;
using Microsoft.VisualStudio.TestTools.UnitTesting;
namespace Gscoy.WeChat.Biz.Tests
{
    [TestClass()]
    public class WeChatAPITests
    {
        [TestMethod()]
        public void ExecuteTest()
        {
            var str = @"<xml>
<ToUserName><![CDATA[toUser]]></ToUserName>
<FromUserName><![CDATA[fromUser]]></FromUserName>
<CreateTime>1351776360</CreateTime>
<MsgType><![CDATA[link]]></MsgType>
<Title><![CDATA[公众平台官网链接]]></Title>
<Description><![CDATA[公众平台官网链接]]></Description>
<Url><![CDATA[url]]></Url>
<MsgId>1234567890123456</MsgId>
</xml> ";
            WeChatAPI api = new WeChatAPI();
            api.Execute(str);
            Assert.Fail();
        }
    }
}
