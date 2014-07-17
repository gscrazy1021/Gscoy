using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gscoy.Data.DapperExtension
{
    // <summary>
    /// 用于动态生成类的一个属性
    /// </summary>
    public class DynamicParam
    {
        /// <summary>
        /// 属性名称
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 属性类型
        /// </summary>
        public Type PropertyType { get; set; }
    }

    /// <summary>
    /// 生成SQL时参数里面的列名和对应值名称
    /// </summary>
    internal class ColumnParam
    {
        /// <summary>
        /// 数据库列名
        /// </summary>
        public string ColumnName { get; set; }
        /// <summary>
        /// 对应类属性名
        /// </summary>
        public string FieldName { get; set; }
    }
}
