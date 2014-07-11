using System.Data;
using System.Data.Odbc;
using System.Data.SqlClient;
using System.Data.OleDb;
using System.Data.OracleClient;
using System.Data.SQLite;
using Gscoy.Common.Enums;
using System;
using Gscoy.Common;
using System.Xml.Linq;

namespace Gscoy.Data
{
    /// <summary>
    /// 数据工厂
    /// </summary>
    public sealed class DataBaseFactory
    {
        public DataBaseFactory()
        { }

        private static readonly string dbType = ConfigHelper.GetConfig("DataProvider", "MSSQL");
        public static DataProvider providerType = DataProviderHelper.GetDataProvider(dbType);
        public static IDbConnection iDbConnection;
        /// <summary>
        /// 从配置文件获取连接字符串
        /// </summary>
        /// <returns>连接字符串</returns>
        public static string GetConnectionString(DBUserType userType)
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

        public static IDbConnection GetConnection(DBUserType user)
        {
            var connectionStr = GetConnectionString(user);
            switch (providerType)
            {
                case DataProvider.MSSQL:
                    iDbConnection = new SqlConnection();
                    break;
                case DataProvider.OleDb:
                    iDbConnection = new OleDbConnection();
                    break;
                case DataProvider.Odbc:
                    iDbConnection = new OdbcConnection();
                    break;
                case DataProvider.Sqlite:
                    iDbConnection = new SQLiteConnection();
                    break;
                default:
                    iDbConnection = new SqlConnection();
                    break;
            }
            return iDbConnection;
        }

        public static IDbCommand GetCommand(DataProvider providerType)
        {
            switch (providerType)
            {
                case DataProvider.MSSQL:
                    return new SqlCommand();
                case DataProvider.OleDb:
                    return new OleDbCommand();
                case DataProvider.Odbc:
                    return new OdbcCommand();
                case DataProvider.Sqlite:
                    return new SQLiteCommand();
                default:
                    return new SqlCommand();
            }
        }

        public static IDbDataAdapter GetDataAdapter(DataProvider providerType)
        {
            switch (providerType)
            {
                case DataProvider.MSSQL:
                    return new SqlDataAdapter();
                case DataProvider.OleDb:
                    return new OleDbDataAdapter();
                case DataProvider.Odbc:
                    return new OdbcDataAdapter();
                case DataProvider.Sqlite:
                    return new SQLiteDataAdapter();
                default:
                    return null;
            }
        }

        public static IDbTransaction GetTransaction(DataProvider providerType)
        {
            IDbTransaction tran = null;
            switch (providerType)
            {
                case DataProvider.MSSQL:
                    tran = (SqlTransaction)tran;
                    break;
                case DataProvider.Sqlite:
                    tran = (SQLiteTransaction)tran;
                    break;
                case DataProvider.OleDb:
                    tran = (OleDbTransaction)tran;
                    break;
                case DataProvider.Odbc:
                    tran = (OdbcTransaction)tran;
                    break;
                default:
                    tran = (SqlTransaction)tran;
                    break;
            }
            return tran;
        }

        public static IDbDataParameter[] GetParameters(DataProvider providerType, int paramsCount)
        {
            IDbDataParameter[] idbParams = new IDbDataParameter[paramsCount];
            switch (providerType)
            {
                case DataProvider.MSSQL:
                    for (int i = 0; i < paramsCount; i++)
                    {
                        idbParams[i] = new SqlParameter();
                    }
                    break;
                case DataProvider.OleDb:
                    for (int i = 0; i < paramsCount; i++)
                    {
                        idbParams[i] = new OleDbParameter();
                    }
                    break;
                case DataProvider.Odbc:
                    for (int i = 0; i < paramsCount; i++)
                    {
                        idbParams[i] = new OdbcParameter();
                    }
                    break;
                case DataProvider.Sqlite:
                    for (int i = 0; i < paramsCount; i++)
                    {
                        idbParams[i] = new SQLiteParameter();
                    }
                    break;
                default:
                    for (int i = 0; i < paramsCount; i++)
                    {
                        idbParams[i] = new SqlParameter();
                    }
                    break;
            }
            return idbParams;
        }
    }
}
