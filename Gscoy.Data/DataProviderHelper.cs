using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gscoy.Data
{
    /// <summary>
    /// db类型
    /// </summary>
    public class DataProviderHelper
    {
        /// <summary>
        /// 根据字符串获取db类型
        /// </summary>
        /// <param name="dbType"></param>
        /// <returns></returns>
        public static DataProvider GetDataProvider(string dbType)
        {
            DataProvider provider = DataProvider.MSSQL;
            switch (dbType.ToLower())
            {
                case "mssql":
                    provider = DataProvider.MSSQL;
                    break;
                case "sqlite":
                    provider = DataProvider.Sqlite;
                    break;
                case "oledb":
                    provider = DataProvider.OleDb;
                    break;
                case "odbc":
                    provider = DataProvider.Odbc;
                    break;
                default:
                    provider = DataProvider.MSSQL;
                    break;
            }
            return provider;
        }
    }
}
