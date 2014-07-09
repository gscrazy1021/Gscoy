using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using Gscoy.Common;

namespace Gscoy.WeChat.Model.RequestModel
{
    /// <summary>
    /// 语音消息
    /// </summary>
    public class RequestVoice : BaseRequestMsg
    {
        /// <summary>
        /// 语音消息媒体id，可以调用多媒体文件下载接口拉取数据。 
        /// </summary>
        public string MediaId { get; set; }
        /// <summary>
        /// 语音格式，如amr，speex等 
        /// </summary>
        public string Format { get; set; }

        public static RequestVoice LoadFromXml(string requestXml)
        {
            RequestVoice voice = new RequestVoice();
            var xml = XElement.Parse(requestXml);
            voice.ToUserName = xml.Element("ToUserName").Value;
            voice.FromUserName = xml.Element("FromUserName").Value;
            voice.CreateTime = xml.Element("CreateTime").Value.ToInt();
            voice.MsgType = xml.Element("MsgType").Value;
            voice.Format = xml.Element("Format").Value;
            voice.MediaId = xml.Element("MediaId").Value;
            voice.MsgId = xml.Element("MsgId").Value;
            return voice;
        }
    }
}
