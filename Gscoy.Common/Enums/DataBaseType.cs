using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gscoy.Common.Enums
{
    /// <summary>
    /// 数据库类型
    /// </summary>
    public enum DataBaseType
    {
        /// <summary>
        /// 微软sqlserver
        /// </summary>
        MSSQL = 1,
        /// <summary>
        /// mysql
        /// </summary>
        MYSQL = 2,
        /// <summary>
        /// sqlite
        /// </summary>
        SQLITE = 4,
        /// <summary>
        /// orcale
        /// </summary>
        ORCALE = 8,
        /// <summary>
        /// access
        /// </summary>
        ACCESS = 16
    }

}
