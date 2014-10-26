using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gscoy.WeChat.Model
{
    /// <summary>
    /// AppId信息
    /// </summary>
    public class AppIdInfo
    {
        /// <summary>
        /// AppID
        /// </summary>
        public string AppID { get; set; }

        /// <summary>
        /// AppSecret
        /// </summary>
        public string AppSecret { get; set; }

        /// <summary>
        /// 回调地址
        /// </summary>
        public string CallBack { get; set; }
    }
}