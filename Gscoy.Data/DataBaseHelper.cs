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
    public sealed class DataBaseHelper
    {
        #region 私有方法及变量
        private static DataBase db = null;//new DataBase(GetDataProvider(), GetConnectionString());

        private static readonly string dbType = ConfigHelper.GetConfig("DataProvider", "MSSQL");

        /// <summary>
        /// 从配置文件中选择数据库类型
        /// </summary>
        /// <returns>DataProvider枚举值</returns>
        private static DataProvider GetDataProvider()
        {
            string providerType = dbType;
            DataProvider dataProvider;
            switch (providerType.ToLower())
            {
                case "mssql":
                    dataProvider = DataProvider.MSSQL;
                    break;
                case "oledb":
                    dataProvider = DataProvider.OleDb;
                    break;
                case "odbc":
                    dataProvider = DataProvider.Odbc;
                    break;
                case "sqlite":
                    dataProvider = DataProvider.Sqlite;
                    break;
                default:
                    return DataProvider.MSSQL;
            }
            return dataProvider;
        }

        /// <summary>
        /// 从配置文件获取连接字符串
        /// </summary>
        /// <returns>连接字符串</returns>
        private static string GetConnectionString(DBUserType userType)
        {
            var xmlPath = AppDomain.CurrentDomain.BaseDirectory + ConfigHelper.GetConfig("DBConnectionPath");
            LogHelper.Trace(xmlPath);
            var xml = XElement.Load(xmlPath);
            var key = dbType.ToUpper();
            var user = string.Empty;
            switch (userType)
            {
                //读写账号
                case DBUserType.User_W:
                    user = "User_W";
                    break;
                //只读账号
                case DBUserType.User_R:
                default:
                    user = "User_R";
                    break;
            }
            var connStr = xml.Element(key).Element(user).Value;
            return connStr;
        }
        #endregion

        #region 隐藏构造方法
        private DataBaseHelper()
        {

        }
        #endregion

        #region 单例
        public static DataBaseHelper helper;
        private static object lockObj = new object();

        public static DataBaseHelper GetInstance(DBUserType userType)
        {
            if (helper == null)
            {
                lock (lockObj)
                {
                    if (helper == null)
                    {
                        var connStr = GetConnectionString(userType);
                        db = new DataBase(GetDataProvider(), connStr);
                        helper = new DataBaseHelper();
                    }
                }
            }
            return helper;
        }
        #endregion

        #region 公有方法
        /// <summary>
        /// 关闭数据库连接的方法
        /// </summary>
        public void Close()
        {
            db.Dispose();
        }

        /// <summary>
        /// 创建参数
        /// </summary>
        /// <param name="paramsCount">参数个数</param>
        public void CreateParameters(int paramsCount)
        {
            db.CreateParameters(paramsCount);
        }

        /// <summary>
        /// 添加参数
        /// </summary>
        /// <param name="index">参数索引</param>
        /// <param name="paramName">参数名</param>
        /// <param name="objValue">参数值</param>
        public void AddParameters(int index, string paramName, object objValue)
        {
            db.AddParameters(index, paramName, objValue);
        }

        /// <summary>
        /// 执行增删改
        /// </summary>
        /// <param name="sqlString">安全的sql语句string.Format()</param>
        /// <returns>操作成功返回true</returns>
        public bool ExecuteNonQuery(string sqlString)
        {
            try
            {
                db.Open();
                db.BeginTransaction();
                return db.ExecuteNonQuery(CommandType.Text, sqlString) > 0 ? true : false;
            }
            catch (Exception e)
            {
                db.Transaction.Rollback();
                throw new Exception(e.Message);
            }
            finally
            {
                db.Transaction.Commit();
                db.Dispose();
            }
        }

        /// <summary>
        /// 执行查询
        /// </summary>
        /// <param name="sqlString">安全的sql语句string.Format()</param>
        /// <returns>返回IDataReader</returns>
        public IDataReader ExecuteReader(string sqlString)
        {
            try
            {
                db.Open();
                db.BeginTransaction();
                return db.ExecuteReader(CommandType.Text, sqlString);
            }
            catch (Exception e)
            {
                db.Transaction.Rollback();
                throw new Exception(e.Message);
            }
            finally
            {
                db.Transaction.Commit();
                db.Dispose();
            }
        }
        /// <summary>
        /// 执行查询
        /// </summary>
        /// <param name="sql">安全的sql语句string.Format()</param>
        /// <returns>返回DataTable</returns>
        public DataTable ExecuteDateTable(string sql)
        {
            try
            {
                db.Open();
                var dataSet = db.ExecuteDataSet(CommandType.Text, sql);
                return dataSet.Tables[0];
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                db.Dispose();
            }
        }
        #endregion
    }
}
