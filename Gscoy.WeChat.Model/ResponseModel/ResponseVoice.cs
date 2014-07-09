using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gscoy.WeChat.Model.ResponseModel
{
    /// <summary>
    /// 回复语音消息
    /// </summary>
   public class ResponseVoice:BaseMsgEntity
    {
        /// <summary>
        ///  	通过上传多媒体文件，得到的id。 
        /// </summary>
        public string MediaId { get; set; }

        public ResponseVoice()
        {
            this.MsgType = "voice";
        }

        public ResponseVoice(BaseMsgEntity info)
            : this()
        {
            this.FromUserName = info.ToUserName;
            this.ToUserName = info.FromUserName;
        }

        public override string ToXML()
        {
            string txt = string.Format(@"<xml><ToUserName><![CDATA[{0}]]></ToUserName><FromUserName><![CDATA[{1}]]></FromUserName><CreateTime>{2}</CreateTime><MsgType><![CDATA[voice]]></MsgType><Voice><MediaId><![CDATA[{4}]]></MediaId></Voice></xml>", this.ToUserName, this.FromUserName, this.CreateTime, this.MediaId);
            return txt;
        }
    }
}
