using Gscoy.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gscoy.WeChat.Biz.Handler
{
    /// <summary>
    /// 图像
    /// </summary>
    public class VideoHandler : IHandler
    {
          /// <summary>
        /// 请求的XML
        /// </summary>
        private string RequestXml { get; set; }
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="requestXml">请求的xml</param>
        public VideoHandler(string requestXml)
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
