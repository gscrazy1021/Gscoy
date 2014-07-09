using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using Gscoy.Common;

namespace Gscoy.WeChat.Model.RequestModel
{
    /// <summary>
    /// 视频消息
    /// </summary>
    public class RequestVideo : BaseRequestMsg
    {
        /// <summary>
        /// 视频消息媒体id，可以调用多媒体文件下载接口拉取数据。 
        /// </summary>
        public string MediaId { get; set; }
        /// <summary>
        /// 视频消息缩略图的媒体id，可以调用多媒体文件下载接口拉取数据。
        /// </summary>
        public string ThumbMediaId { get; set; }

        public static RequestVideo LoadFromXml(string requestXml)
        {
            RequestVideo video = new RequestVideo();
            var xml = XElement.Parse(requestXml);
            video.ToUserName = xml.Element("ToUserName").Value;
            video.FromUserName = xml.Element("FromUserName").Value;
            video.CreateTime = xml.Element("CreateTime").Value.ToInt();
            video.MsgType = xml.Element("MsgType").Value;
            video.ThumbMediaId = xml.Element("Format").Value;
            video.MediaId = xml.Element("MediaId").Value;
            video.MsgId = xml.Element("MsgId").Value;
            return video;
        }
    }
}
