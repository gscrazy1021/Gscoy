using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using Gscoy.Common;

namespace Gscoy.WeChat.Model.RequestModel
{
    /// <summary>
    /// 地理位置消息
    /// </summary>
    public class RequestLocation : BaseRequestMsg
    {
        /// <summary>
        /// 地理位置纬度
        /// </summary>
        public string Location_X { get; set; }
        /// <summary>
        /// 地理位置经度 
        /// </summary>
        public string Location_Y { get; set; }
        /// <summary>
        /// 地图缩放大小 
        /// </summary>
        public int Scale { get; set; }
        /// <summary>
        ///  	地理位置信息 
        /// </summary>
        public string Label { get; set; }

        public static RequestLocation LoadFromXml(string requestXml)
        {
            RequestLocation location = new RequestLocation();
            var xml = XElement.Parse(requestXml);
            location.ToUserName = xml.Element("ToUserName").Value;
            location.FromUserName = xml.Element("FromUserName").Value;
            location.CreateTime = xml.Element("CreateTime").Value.ToInt();
            location.MsgType = xml.Element("MsgType").Value;
            location.Location_X = xml.Element("Location_X").Value;
            location.Location_Y = xml.Element("Location_Y").Value;
            location.Label = xml.Element("Label").Value;
            location.Scale = xml.Element("Scale").Value.ToInt();
            location.MsgId = xml.Element("MsgId").Value;
            return location;
        }
    }
}
