using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gscoy.DataModel.CnBlogs
{
    public class NewsXmlEntity
    {
        /// <summary>
        /// 主题
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// 链接
        /// </summary>
        public string Link { get; set; }

        /// <summary>
        /// 日期
        /// </summary>
        public string PubDate { get; set; }

        /// <summary>
        /// guid
        /// </summary>
        public string Guid { get; set; }

        /// <summary>
        /// 内容
        /// </summary>
        public string Content { get; set; }

        /// <summary>
        /// 简介
        /// </summary>
        public string ShotCut { get; set; }
    }
}
