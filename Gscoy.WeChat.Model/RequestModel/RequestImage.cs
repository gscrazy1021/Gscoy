using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using Gscoy.Common;

namespace Gscoy.WeChat.Model.RequestModel
{
    /// <summary>
    /// 图片消息
    /// </summary>
    public class RequestImage : BaseRequestMsg
    {
        /// <summary>
        /// 图片链接 
        /// </summary>
        public string PicUrl { get; set; }

        /// <summary>
        /// 图片消息媒体id，可以调用多媒体文件下载接口拉取数据。 
        /// </summary>
        public string MediaId { get; set; }

        public static RequestImage LoadFromXml(string requestXml)
        {
            RequestImage img = new RequestImage();
            var xml = XElement.Parse(requestXml);
            img.ToUserName = xml.Element("ToUserName").Value;
            img.FromUserName = xml.Element("FromUserName").Value;
            img.CreateTime = xml.Element("CreateTime").Value.ToInt();
            img.MsgType = xml.Element("MsgType").Value;
            img.PicUrl = xml.Element("PicUrl").Value;
            img.MediaId = xml.Element("MediaId").Value;
            img.MsgId = xml.Element("MsgId").Value;
            return img;
        }
    }
}
