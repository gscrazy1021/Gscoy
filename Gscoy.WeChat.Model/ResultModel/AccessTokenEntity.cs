using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;

namespace Gscoy.WeChat.Model.ResultModel
{
    public class AccessTokenEntity 
    {
        /// <summary>
        /// accessToken
        /// </summary>
        [JsonProperty("access_token")]
        public string AccessToken { get; set; }
        /// <summary>
        /// expires_in有效时间，毫秒
        /// </summary>
        [JsonProperty("expires_in")]
        public int ExpiressInt { get; set; }
    }
}
