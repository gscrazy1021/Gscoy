using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Xml.Linq;
using Gscoy.Common;
using Gscoy.Common.Enums;

namespace Gscoy.Data
{
    public class DataBaseHelper
    {
        private static readonly IDataBase db = new DataBase(GetDataProvider(), GetConnectionString());

        private static readonly string dbType = ConfigHelper.GetConfig("DataProvider", "MSSQL");

        /// <summary>
        /// 从配置文件中选择数据库类型
        /// </summary>
        /// <returns>DataProvider枚举值</returns>
        private static DataProvider GetDataProvider()
        {
            string providerType = dbType;
            DataProvider dataProvider;
            switch (providerType)
            {
                case "Oracle":
                    dataProvider = DataProvider.Oracle;
                    break;
                case "SqlServer":
                    dataProvider = DataProvider.SqlServer;
                    break;
                case "OleDb":
                    dataProvider = DataProvider.OleDb;
                    break;
                case "Odbc":
                    dataProvider = DataProvider.Odbc;
                    break;
                case "MySql":
                    dataProvider = DataProvider.MySql;
                    break;
                default:
                    return DataProvider.Odbc;
            }
            return dataProvider;
        }

        /// <summary>
        /// 从配置文件获取连接字符串
        /// </summary>
        /// <returns>连接字符串</returns>
        private static string GetConnectionString()
        {
            //可以变成读取xml，按用户角色来分配权限
            var key = dbType + "_ConnStr";
            var connStr = ConfigHelper.GetConnectionString(key);
            return connStr;
        }

        /// <summary>
        /// 关闭数据库连接的方法
        /// </summary>
        public static void Close()
        {
            db.Dispose();
        }

        /// <summary>
        /// 创建参数
        /// </summary>
        /// <param name="paramsCount">参数个数</param>
        public static void CreateParameters(int paramsCount)
        {
            db.CreateParameters(paramsCount);
        }

        /// <summary>
        /// 添加参数
        /// </summary>
        /// <param name="index">参数索引</param>
        /// <param name="paramName">参数名</param>
        /// <param name="objValue">参数值</param>
        public static void AddParameters(int index, string paramName, object objValue)
        {
            db.AddParameters(index, paramName, objValue);
        }

        /// <summary>
        /// 执行增删改
        /// </summary>
        /// <param name="sqlString">安全的sql语句string.Format()</param>
        /// <returns>操作成功返回true</returns>
        public static bool ExecuteNonQuery(string sqlString)
        {
            try
            {
                db.Open();
                return db.ExecuteNonQuery(CommandType.Text, sqlString) > 0 ? true : false;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
            finally
            {
                db.Dispose();
            }
        }

        /// <summary>
        /// 执行查询
        /// </summary>
        /// <param name="sqlString">安全的sql语句string.Format()</param>
        /// <returns>返回IDataReader</returns>
        public static IDataReader ExecuteReader(string sqlString)
        {
            try
            {
                db.Open();
                return db.ExecuteReader(CommandType.Text, sqlString);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
    }
}
