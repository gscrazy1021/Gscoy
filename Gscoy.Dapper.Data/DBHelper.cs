using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.Odbc;
using System.Data.OracleClient;
using System.Data.SqlClient;
using System.Data.SQLite;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Xml.Linq;
using Gscoy.Common;
using Gscoy.Common.Enums;
using Gscoy.Dapper;
using Gscoy.Dapper;
using Gscoy.DapperExtension;
using Gscoy.Data;

namespace Gscoy.Dapper.Data
{
    public class DBHelper
    {
        #region 构造方法
        private DBHelper(DataBaseType dbtype)
        {
            // System.Data.OleDb
            // System.Data.Odbc
            // System.Data.SqlClient
            // System.Data.OracleClient
            var connectionStr = ConfigHelper.GetConfig("DBConnection");
            var dbProviderName = string.Empty;
            switch (dbtype)
            {
                case DataBaseType.MSSQL:
                    dbProviderName = "System.Data.SqlClient";
                    break;
                case DataBaseType.MYSQL:
                    break;
                case DataBaseType.SQLITE:
                    break;
                case DataBaseType.ORCALE:
                    dbProviderName = "System.Data.OracleClient";
                    break;
                case DataBaseType.ACCESS:
                    dbProviderName = "System.Data.OleDb";
                    break;
                default:
                    dbProviderName = "System.Data.SqlClient";
                    break;
            }
            new DbBase(connectionStr, dbProviderName);
        }

        private DBHelper(string connStr)
        {
            new DBHelper(connStr);
        }
        #endregion

        #region 私有字段
        static object lockobj = new object();
        #endregion

        #region 属性
        /// <summary>
        /// 数据库连接串
        /// </summary>
        public string ConnectionString { get; private set; }

        /// <summary>
        /// 数据库连接
        /// </summary>
        public IDbConnection DBConn { get; private set; }
        #endregion

        #region 私有方法
        private void GetDbConnection(DBUserType userType)
        {
            var xmlPath = ConfigHelper.GetConfig("DBConnection");
            var dbtypeStr = ConfigHelper.GetConfig("DBType", "MSSQL");
            var dbtype = DataBaseTypeHelper.GetDataBaseType(dbtypeStr);
            var element = XElement.Load(xmlPath);
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
            switch (dbtype)
            {
                case DataBaseType.MSSQL:
                    DBConn = new SqlConnection(ConnectionString);
                    break;
                case DataBaseType.MYSQL:
                    //DBConn=new 
                    break;
                case DataBaseType.SQLITE:
                    DBConn = new SQLiteConnection(ConnectionString);
                    break;
                case DataBaseType.ORCALE:
                    DBConn = new OracleConnection(ConnectionString);
                    break;
                case DataBaseType.ACCESS:
                    DBConn = new OdbcConnection(ConnectionString);
                    break;
                default:
                    DBConn = new SqlConnection(ConnectionString);
                    break;
            }
            var temp = from u in element.Elements(dbtypeStr).Descendants(user) select u.Value;
            ConnectionString = temp.ElementAt<string>(0);
        }
        #endregion

        #region 公有方法
        /// <summary>
        /// 获取db操作的唯一实例
        /// </summary>
        /// <param name="userType"></param>
        /// <returns></returns>
        public DBHelper GetInstance(DBUserType userType)
        {
            DBHelper db = null;
            lock (lockobj)
            {
                if (CallContext.GetData("DBType") == null)
                {
                    //var dbtype =GetDbConnection(userType);
                    GetDbConnection(userType);
                    switch (userType)
                    {
                        //读写账号
                        case DBUserType.User_W:
                            DBConn.BeginTransaction();
                            break;
                        //只读账号
                        case DBUserType.User_R:
                        default:
                            break;
                    }
                    CallContext.SetData("DBType", db);
                }
                else
                {
                    db = (DBHelper)CallContext.GetData("DBType");
                }
                return db;
            }
        }

        public void Test()
        {
            var db = GetInstance(DBUserType.User_R);
            var sql = string.Format("");
            db.DBConn.Execute(sql);
        }
        #endregion
    }
}
