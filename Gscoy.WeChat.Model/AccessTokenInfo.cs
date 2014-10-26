using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gscoy.WeChat.Model
{
    /// <summary>
    /// access_token实体类
    /// </summary>
    public class AccessTokenInfo
    {
        /// <summary>
        /// access_token
        /// </summary>
        public string AccessToken { get; set; }

        /// <summary>
        /// 凭证有效时间，单位：秒 
        /// </summary>
        public long ExpiresIn { get; set; }
    }
}
