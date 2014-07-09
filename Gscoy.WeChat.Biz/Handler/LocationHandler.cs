using Gscoy.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gscoy.WeChat.Biz.Handler
{
    /// <summary>
    /// 位置
    /// </summary>
    public class LocationHandler : IHandler
    {
         /// <summary>
        /// 请求的XML
        /// </summary>
        private string RequestXml { get; set; }
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="requestXml">请求的xml</param>
        public LocationHandler(string requestXml)
        {
            LogHelper.Trace(requestXml);
            this.RequestXml = requestXml;
        }

        public string HandleRequest()
        {
            throw new NotImplementedException();
        }
    }
}
