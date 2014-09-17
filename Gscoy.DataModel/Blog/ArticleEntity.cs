using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gscoy.DataModel.Blog
{
    public class ArticleEntity:BaseEntity
    {
        /// <summary>
        /// 文章ID
        /// </summary>
        public int ArticleID { get; set; }
        /// <summary>
        /// 标题
        /// </summary>
        public string Title { get; set; }
        /// <summary>
        /// 内容
        /// </summary>
        public string Content { get; set; }
        /// <summary>
        /// 文章类型
        /// </summary>
        public string ArticleType { get; set; }
        /// <summary>
        /// 文章标签
        /// </summary>
        public string ArticleTag { get; set; }
        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreateTime { get; set; }
        /// <summary>
        /// 修改时间
        /// </summary>
        public DateTime ModifyTime { get; set; }

    }
}
