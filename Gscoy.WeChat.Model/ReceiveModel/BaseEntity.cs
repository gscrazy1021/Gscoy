using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gscoy.WeChat.Model.ReceiveModel
{
    [Serializable]
    public class BaseEntity
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
                return int.Parse(DateTime.Now.ToString("yyyyMMddHHmmss"));
            }
        }

        /// <summary>
        ///  	消息类型
        /// </summary>
        public string MsgType { get; set; }
    }
}
