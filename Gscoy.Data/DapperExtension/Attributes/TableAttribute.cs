using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gscoy.Data.DapperExtension.Attributes
{
    /// <summary>
    /// 数据库表
    /// </summary>
    [AttributeUsage(AttributeTargets.Class)]
    public class TableAttribute : BaseAttribute
    {
        /// <summary>
        /// 别名，对应数据里面的名字
        /// </summary>
        public string TableName { get; set; }
    }
}
