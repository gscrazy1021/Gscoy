using Gscoy.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gscoy.WeChat.Biz.Handler
{
    /// <summary>
    /// 位置事件
    /// </summary>
    public class LocationEventHandler : IHandler
    {
        /// <summary>
        /// 请求的XML
        /// </summary>
        private string RequestXml { get; set; }
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="requestXml">请求的xml</param>
        public LocationEventHandler(string requestXml)
        {
            LogHelper.Trace(requestXml);
            this.RequestXml = requestXml;
        }

        public string HandleRequest()
        {
            var val = string.Empty;
            try
            {
                val = "location11111";
            }
            catch (Exception ex)
            {
                val = ex.Message;
            }
            return string.Format(@"<xml><ToUserName><![CDATA[oQqXfjmUcuw2YnM-ccc2f1Le9SrI]]></ToUserName><FromUserName><![CDATA[gh_e5df289c1d17]]></FromUserName><CreateTime>1405072000</CreateTime><MsgType><![CDATA[text]]></MsgType><Content><![CDATA[{0}]]></Content></xml>", val);
        }
    }
}
