using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Gscoy.WeChat.Biz.Handler;
using Microsoft.VisualStudio.TestTools.UnitTesting;
namespace Gscoy.WeChat.Biz.Handler.Tests
{
    [TestClass()]
    public class TextHandlerTests
    {
        [TestMethod()]
        public void HandleRequestTest()
        {
            TextHandler txt = new TextHandler(@"<xml>
 <ToUserName><![CDATA[toUser]]></ToUserName>
 <FromUserName><![CDATA[fromUser]]></FromUserName> 
 <CreateTime>1348831860</CreateTime>
 <MsgType><![CDATA[text]]></MsgType>
 <Content><![CDATA[this is a test]]></Content>
 <MsgId>1234567890123456</MsgId>
 </xml>");
            var r = txt.HandleRequest();
            Assert.Fail();
        }
    }
}
