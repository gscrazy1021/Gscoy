using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gscoy.WeChat.Model
{
    public class WeChatUserInfoModel
    {
        /// <summary>
        /// openid?
        /// </summary>
        public string id { get; set; }
        /// <summary>
        /// 昵称
        /// </summary>
        public string nick_name { get; set; }
        /// <summary>
        /// 备注
        /// </summary>
        public string remark_name { get; set; }
        /// <summary>
        /// 组
        /// </summary>
        public string group_id { get; set; }
    }
}
