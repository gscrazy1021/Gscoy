using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gscoy.DataModel
{
    /// <summary>
    /// 静态资源实体
    /// </summary>
    public class StaticSourceEntity
    {
        /// <summary>
        /// 键
        /// </summary>
        public string Key { get; set; }
        /// <summary>
        /// 本地资源路径
        /// </summary>
        public string LocalPath { get; set; }
        /// <summary>
        /// 线上资源路径
        /// </summary>
        public string OnlinePath { get; set; }
    }
}
