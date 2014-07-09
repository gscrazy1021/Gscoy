using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gscoy.WeChat.Model.ResponseModel
{
    /// <summary>
    /// 回复音乐消息
    /// </summary>
    public class ResponseMusic : BaseMsgEntity
    {
        /// <summary>
        /// 音乐标题
        /// </summary>
        public string Title { get; set; }
        /// <summary>
        /// 音乐描述
        /// </summary>
        public string Description { get; set; }
        /// <summary>
        /// 音乐链接
        /// </summary>
        public string MusicURL { get; set; }
        /// <summary>
        /// 高质量音乐链接，WIFI环境优先使用该链接播放音乐 
        /// </summary>
        public string HQMusicURL { get; set; }
        /// <summary>
        ///  	缩略图的媒体id，通过上传多媒体文件，得到的id 
        /// </summary>
        public string ThumbMediaId { get; set; }

        public ResponseMusic()
        {
            this.MsgType = "music";
        }

        public ResponseMusic(BaseMsgEntity info)
            : this()
        {
            this.FromUserName = info.ToUserName;
            this.ToUserName = info.FromUserName;
        }

        public override string ToXML()
        {
            string txt = string.Format(@"<xml><ToUserName><![CDATA[{0}]]></ToUserName><FromUserName><![CDATA[{1}]]></FromUserName><CreateTime>{2}</CreateTime><MsgType><![CDATA[music]]></MsgType><Music><Title><![CDATA[{3}]]></Title><Description><![CDATA[{4}]]></Description><MusicUrl><![CDATA[{5}]]></MusicUrl><HQMusicUrl><![CDATA[{6}]]></HQMusicUrl><ThumbMediaId><![CDATA[{7}]]></ThumbMediaId></Music></xml>", this.ToUserName, this.FromUserName, this.CreateTime, this.Title, this.Description, this.MusicURL, this.HQMusicURL, this.ThumbMediaId);
            return txt;
        }
    }
}
