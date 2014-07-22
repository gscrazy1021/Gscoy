using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gscoy.WeChat.Model.UserInfo
{
    public class ResopnseErrorMsg
    {
        /// <summary>
        /// 错误返回码
        /// </summary>
        public string errcode { get; set; }
        /// <summary>
        /// 错误消息
        /// </summary>
        public string errmsg { get; set; }
    }
}
