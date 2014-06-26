using Gscoy.Common.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gscoy.Common
{
    public class DataBaseTypeHelper
    {
        /// <summary>
        /// 获取数据库类型
        /// </summary>
        /// <param name="dbt"></param>
        /// <returns></returns>
        public static DataBaseType GetDataBaseType(string dbStr)
        {
            switch (dbStr)
            {
                case "sqlserver":
                    return DataBaseType.MSSQL;
                case "mysql":
                    return DataBaseType.MYSQL;
                case "sqlite":
                    return DataBaseType.SQLITE;
                case "orcale":
                    return DataBaseType.ORCALE;
                case "access":
                    return DataBaseType.ACCESS;
                default:
                    return DataBaseType.MSSQL;
            }
        }
    }
}
