using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gscoy.Data.DapperExtension.Attributes
{
    /// <summary>
    /// 主键
    /// </summary>
    [AttributeUsage(AttributeTargets.Field | AttributeTargets.Property)]
    public class PKAttribute : BaseAttribute
    {
    }
}
