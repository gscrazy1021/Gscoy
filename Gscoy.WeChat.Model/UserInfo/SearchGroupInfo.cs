using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gscoy.WeChat.Model.UserInfo
{
    public class SearchGroupInfo
    {
        /// <summary>
        ///  	分组id，由微信分配 
        /// </summary>
        public int id { get; set; }
        /// <summary>
        ///  	分组名字，UTF8编码 
        /// </summary>
        public string name { get; set; }
        /// <summary>
        ///  	分组内用户数量 
        /// </summary>
        public int count { get; set; }
    }

    public class SearchGroupList
    {
        /// <summary>
        /// 公众平台分组信息列表
        /// </summary>
        public List<SearchGroupInfo> groups { get; set; }
    }
}
