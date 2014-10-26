using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gscoy.WeChat.Model
{
    /// <summary>
    /// 关注着列表
    /// </summary>
    public class Attentions
    {
        public int total { get; set; }
        public int count { get; set; }
        public string next_openid { get; set; }
        public AttentionsData data { get; set; }
    }

    /// <summary>
    /// 关注着列表数据
    /// </summary>
    public class AttentionsData
    {
        public List<string> openid { get; set; }
    }
}
