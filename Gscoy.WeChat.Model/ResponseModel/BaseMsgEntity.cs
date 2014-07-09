using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gscoy.WeChat.Model.ResponseModel
{
    /// <summary>
    /// 接收消息基类
    /// </summary>
    public class BaseMsgEntity
    {
        /// <summary>
        /// 开发者微信号
        /// </summary>
        public string ToUserName { get; set; }

        /// <summary>
        /// 发送方帐号（一个OpenID） 
        /// </summary>
        public string FromUserName { get; set; }

        /// <summary>
        ///  	消息创建时间 （整型） 
        /// </summary>
        public int CreateTime
        {
            get
            {
                DateTime timeStamp = new DateTime(1970, 1, 1);  //得到1970年的时间戳
                long time = (DateTime.UtcNow.Ticks - timeStamp.Ticks) / 10000000;
                return int.Parse(time.ToString());
            }
        }

        /// <summary>
        ///  	消息类型
        /// </summary>
        public string MsgType { get; set; }

        public virtual string ToXML()
        {
            return string.Empty;
        }
    }
}
