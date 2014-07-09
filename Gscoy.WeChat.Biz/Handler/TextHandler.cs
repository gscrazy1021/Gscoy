using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Gscoy.Common;
using Gscoy.WeChat.Model.RequestModel;
using Gscoy.WeChat.Model.ResonseModel;

namespace Gscoy.WeChat.Biz.Handler
{
    public class TextHandler : IHandler
    {
        /// <summary>
        /// 请求的XML
        /// </summary>
        private string RequestXml { get; set; }
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="requestXml">请求的xml</param>
        public TextHandler(string requestXml)
        {
            LogHelper.Trace(requestXml);
            this.RequestXml = requestXml;
        }
        #region IHandler 成员

        public string HandleRequest()
        {
            string response = string.Empty;
            RequestText txt = RequestText.LoadFromXml(RequestXml);
            string content = txt.Content.Trim();
            if (string.IsNullOrEmpty(content))
            {
                response = "您什么都没输入，没法帮您啊，%>_<%。";
            }
            else
            {
                var msgType = content.Split('@')[0];
                switch (msgType.ToLower())
                {
                    case "tq":
                        response = "tq";
                        break;
                    default:
                        response = "输入的类型不对撒~";
                        break;
                }
            }
            ResponseText rt = new ResponseText();
            rt.Content = response;
            rt.ToUserName = txt.FromUserName;
            rt.FromUserName = txt.ToUserName;
            rt.MsgType = txt.MsgType;
            response = rt.ToXML();
            return response;
        }

        #endregion
    }
}
