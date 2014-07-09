using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;

namespace Gscoy.WeChat.Model.RequestModel
{
    public class RequestText : BaseRequestMsg
    {
        /// <summary>
        ///  	文本消息内容 
        /// </summary>
        public string Content { get; set; }

        public static RequestText LoadFromXml(string requestXml)
        {
            RequestText txt = new RequestText();
            var xml = XElement.Parse(requestXml);
            txt.ToUserName = xml.Element("ToUserName").Value;
            txt.FromUserName = xml.Element("FromUserName").Value;
            txt.CreateTime = int.Parse(xml.Element("CreateTime").Value);
            txt.Content = xml.Element("Content").Value;
            txt.MsgId = xml.Element("MsgId").Value;
            txt.MsgType = xml.Element("MsgType").Value;
            return txt;
        }
    }
}
