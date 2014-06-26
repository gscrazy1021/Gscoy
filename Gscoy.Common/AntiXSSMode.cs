using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gscoy.Common
{
    /// <summary>
    /// 过滤跨站脚本的模式
    /// </summary>
    public enum AntiXSSMode
    {
        /// <summary>
        /// 标准模式 去除html标签
        /// </summary>
        Default = 0,
        /// <summary>
        /// 简单模式 过滤指定的标签和属性
        /// </summary>
        Simple = 1,
        /// <summary>
        /// 不过滤
        /// </summary>
        None = 2
    }
}
