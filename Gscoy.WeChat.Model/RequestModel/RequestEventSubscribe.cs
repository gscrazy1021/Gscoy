using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using Gscoy.Common;

namespace Gscoy.WeChat.Model.RequestModel
{
    /// <summary>
    /// 关注/取消关注事件
    /// </summary>
   public class RequestEventSubscribe:BaseRequestEvent
    {
       public static RequestEventSubscribe LoadFromXml(string requestXml)
       {
           RequestEventSubscribe sub = new RequestEventSubscribe();
           var xml = XElement.Parse(requestXml);
           sub.ToUserName = xml.Element("ToUserName").Value;
           sub.FromUserName = xml.Element("FromUserName").Value;
           sub.CreateTime = xml.Element("CreateTime").Value.ToInt();
           sub.MsgType = xml.Element("MsgType").Value;
           sub.Event = xml.Element("Event").Value;
           return sub;
       }
    }
}
