using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gscoy.DataModel.Blog
{
    public class ArticleLogEntity : BaseEntity
    {
        /// <summary>
        /// ID，作为主键
        /// </summary>
        public int ID { get; set; }
        /// <summary>
        /// 对应文章的id，可以加上索引
        /// </summary>
        public int ArticleID { get; set; }
        /// <summary>
        /// 操作人的ip
        /// </summary>
        public string DeallerIP { get; set; }
        /// <summary>
        /// 操作时间
        /// </summary>
        public DateTime CreateTime { get; set; }
        /// <summary>
        /// 上次的引用地址
        /// </summary>
        public string RefUrl { get; set; }
        /// <summary>
        /// 操作动作，修改/删除/新增
        /// </summary>
        public string Action { get; set; }
    }
}
