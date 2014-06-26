using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gscoy.WeChat.Model
{
    public class WeChatRetInfo
    {
        public string Ret { get; set; }
        public string ErrMsg { get; set; }
        public string ShowVerifyCode { get; set; }
        public string ErrCode { get; set; }
    }
}
