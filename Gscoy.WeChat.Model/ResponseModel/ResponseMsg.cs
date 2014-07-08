using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gscoy.WeChat.Model.ResonseModel
{
    /// <summary>
    /// 接收文本消息
    /// </summary>
    public class ResponseText : BaseMsgEntity
    {
        /// <summary>
        /// 文本消息内容
        /// </summary>
        public string Content { get; set; }

        public ResponseText()
        {
            this.MsgType = ResponseMsgType.Text.ToString().ToLower();
        }

        public ResponseText(BaseMsgEntity info)
            : this()
        {
            this.FromUserName = info.ToUserName;
            this.ToUserName = info.FromUserName;
        }

        public override string ToXML()
        {
            string txt = string.Format(@"<xml><ToUserName><![CDATA[{0}]]></ToUserName><FromUserName><![CDATA[{1}]]></FromUserName><CreateTime>{2}</CreateTime><MsgType><![CDATA[text]]></MsgType><Content><![CDATA[{3}]]></Content></xml>", this.ToUserName, this.FromUserName, this.CreateTime, this.Content);
            return txt;
        }
    }
}
