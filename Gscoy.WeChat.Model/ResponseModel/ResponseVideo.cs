using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gscoy.WeChat.Model.ResponseModel
{
    /// <summary>
    /// 回复视频消息
    /// </summary>
    public class ResponseVideo : BaseMsgEntity
    {
        /// <summary>
        /// 通过上传多媒体文件，得到的id 
        /// </summary>
        public string MediaId { get; set; }
        /// <summary>
        /// 视频消息的标题 
        /// </summary>
        public string Title { get; set; }
        /// <summary>
        /// 视频消息的描述 
        /// </summary>
        public string Description { get; set; }

        public ResponseVideo()
        {
            this.MsgType = "video";
        }

        public ResponseVideo(BaseMsgEntity info)
            : this()
        {
            this.FromUserName = info.ToUserName;
            this.ToUserName = info.FromUserName;
        }

        public override string ToXML()
        {
            var txt = string.Format(@"<xml><ToUserName><![CDATA[{0}]]></ToUserName><FromUserName><![CDATA[{1}]]></FromUserName><CreateTime>{2}</CreateTime><MsgType><![CDATA[video]]></MsgType><Video><MediaId><![CDATA[{3}]]></MediaId><Title><![CDATA[{4}]]></Title><Description><![CDATA[{5}]]></Description></Video> </xml>", this.ToUserName, this.FromUserName, this.CreateTime, this.MediaId, this.Title, this.Description);
            return txt;
        }
    }
}
