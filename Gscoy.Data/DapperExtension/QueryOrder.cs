﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gscoy.Data.DapperExtension
{
    /// <summary>
    /// 排序
    /// </summary>
    public class QueryOrder
    {
        /// <summary>
        /// 排序字段
        /// </summary>
        public virtual string Field { get; set; }
        /// <summary>
        /// 是否倒序
        /// </summary>
        public virtual bool IsDesc { get; set; }
    }
}
