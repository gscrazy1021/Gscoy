using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;

namespace Gscoy.WeChat.Model.UserInfo
{
    public class UserOpenID
    {
        /// <summary>
        ///  用户的OpenID 
        /// </summary>
        public string openid { get; set; }
        /// <summary>
        ///  分组id 
        /// </summary>
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public int to_groupid { get; set; }
    }
}
