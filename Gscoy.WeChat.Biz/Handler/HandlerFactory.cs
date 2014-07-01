using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;

namespace Gscoy.WeChat.Biz.Handler
{
    /// <summary>
    /// 处理器工厂类
    /// </summary>
    public class HandlerFactory
    {
        /// <summary>
        /// 创建处理器
        /// </summary>
        /// <param name="requestXml">请求的xml</param>
        /// <returns>IHandler对象</returns>
        public static IHandler CreateHandler(string requestXml)
        {
            IHandler handler = null;
            if (!string.IsNullOrEmpty(requestXml))
            {
                //解析数据
                XmlDocument doc = new System.Xml.XmlDocument();
                doc.LoadXml(requestXml);
                XmlNode node = doc.SelectSingleNode("/xml/MsgType");
                if (node != null)
                {
                    XmlCDataSection section = node.FirstChild as XmlCDataSection;
                    if (section != null)
                    {
                        string msgType = section.Value;

                        switch (msgType)
                        {
                            case "text":
                                handler = new TextHandler(requestXml);
                                break;
                            case "image":
                                break;
                            case "voice":
                                break;
                            case "video":
                                break;
                            case "location":
                                break;
                            case "link":
                                break;
                            case "event":
                                var eventStr = doc.SelectSingleNode("/xml/EVENT");
                                if (eventStr == null) break;
                                switch (eventStr.Value)
                                {
                                    case "subscribe":
                                        break;
                                    case "SCAN":
                                        break;
                                    case "LOCATION":
                                        break;
                                    case "CLICK":
                                        break;
                                    case "VIEW":
                                        break;
                                }
                                break;
                        }
                    }
                }
            }

            return handler;
        }
    }
}
