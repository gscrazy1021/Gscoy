using Gscoy.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Gscoy.WeChat.Model.RequestModel;
using Gscoy.WeChat.Model.ResponseModel;

namespace Gscoy.WeChat.Biz.Handler
{
    /// <summary>
    /// 关注事件
    /// </summary>
    public class SubscribeEventHandler : IHandler
    {
        /// <summary>
        /// 请求的XML
        /// </summary>
        private string RequestXml { get; set; }
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="requestXml">请求的xml</param>
        public SubscribeEventHandler(string requestXml)
        {
            LogHelper.Trace(requestXml);
            this.RequestXml = requestXml;
        }
        public string HandleRequest()
        {
            var entity = RequestEventSubscribe.LoadFromXml(RequestXml);
            var txt = new ResponseText();
            txt.ToUserName = entity.FromUserName;
            txt.FromUserName = entity.ToUserName;
            txt.Content = entity.Event+"hello!";
            var result = txt.ToXML();
            return result;
        }
    }
}
