using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;

namespace Gscoy.DataModel.Baidu
{
    public class TokenEntity
    {
        /// <summary>
        /// 要获取的Access Token
        /// </summary>
        [JsonProperty("access_token")]
        public string AccessToken { get; set; }

        /// <summary>
        /// Access Token的有效期，以秒为单位
        /// </summary>
        [JsonProperty("expires_in")]
        public string ExpressIn { get; set; }

        /// <summary>
        /// 用于刷新Access Token 的 Refresh Token,所有应用都会返回该参数；（10年的有效期）
        /// </summary>
        [JsonProperty("refresh_token")]
        public string RefreshToken { get; set; }

        /// <summary>
        /// Access Token最终的访问范围，即用户实际授予的权限列表
        /// basic 	用户基本权限，可以获取用户的基本信息
        /// super_msg 	往用户的百度首页上发送消息提醒，相关API任何应用都能使用，但要想将消息提醒在百度首页显示，需要第三方在注册应用时额外填写相关信息
        /// netdisk 	获取用户在个人云存储中存放的数据
        /// </summary>
        [JsonProperty("scope")]
        public string Scope { get; set; }

        /// <summary>
        /// 基于http调用Open API时所需要的Session Key，其有效期与Access Token一致
        /// </summary>
        [JsonProperty("session_key")]
        public string SessionKey { get; set; }

        /// <summary>
        /// 基于http调用Open API时计算参数签名用的签名密钥
        /// </summary>
        [JsonProperty("session_secret")]
        public string SessionSecret { get; set; }
    }
}
