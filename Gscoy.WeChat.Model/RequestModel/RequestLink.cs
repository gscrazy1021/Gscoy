using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using Gscoy.Common;

namespace Gscoy.WeChat.Model.RequestModel
{
    /// <summary>
    /// 链接消息
    /// </summary>
    public class RequestLink : BaseRequestMsg
    {
        /// <summary>
        /// 消息标题
        /// </summary>
        public string Title { get; set; }
        /// <summary>
        /// 消息描述 
        /// </summary>
        public string Description { get; set; }
        /// <summary>
        /// 消息链接 
        /// </summary>
        public string Url { get; set; }

        public static RequestLink LoadFromXml(string requestXml)
        {
            RequestLink link = new RequestLink();
            var xml = XElement.Parse(requestXml);
            link.ToUserName = xml.Element("ToUserName").Value;
            link.FromUserName = xml.Element("FromUserName").Value;
            link.CreateTime = xml.Element("CreateTime").Value.ToInt();
            link.MsgType = xml.Element("MsgType").Value;
            link.Title = xml.Element("Title").Value;
            link.Description = xml.Element("Description").Value;
            link.Url = xml.Element("Url").Value;
            link.MsgId = xml.Element("MsgId").Value;
            return link;
        }
    }
}
