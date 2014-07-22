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
    /// 声音
    /// </summary>
    public class VoiceHandler : IHandler
    {
        /// <summary>
        /// 请求的XML
        /// </summary>
        private string RequestXml { get; set; }
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="requestXml">请求的xml</param>
        public VoiceHandler(string requestXml)
        {
            LogHelper.Trace(requestXml);
            this.RequestXml = requestXml;
        }
        public string HandleRequest()
        {
            var entity = RequestVoice.LoadFromXml(RequestXml);
            var voice = new ResponseText();
            voice.FromUserName = entity.ToUserName;
            voice.ToUserName = entity.FromUserName;
            voice.Content = entity.Recognition + "" + entity.MediaId;
            var result = voice.ToXML();
            return result;
        }
    }
}
