using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gscoy.WeChat.Model
{
    public class ErrorMsg
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
