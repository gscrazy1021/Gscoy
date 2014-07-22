using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;

namespace Gscoy.WeChat.Model.UserInfo
{
    [JsonObject("group")]
    public class CreateGroupInfo
    {
        /// <summary>
        /// 分组id，由微信分配 
        /// </summary>
        [JsonProperty("id", NullValueHandling = NullValueHandling.Ignore)]
        public int ID { get; set; }
        /// <summary>
        ///  	分组名字，UTF8编码 
        /// </summary>
        [JsonProperty("name")]
        public string Name { get; set; }
    }
}
