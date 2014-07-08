using System.Data;
using System.Data.Odbc;
using System.Data.SqlClient;
using System.Data.OleDb;
using System.Data.OracleClient;
using System.Data.SQLite;

namespace Gscoy.Data
{
    /// <summary>
    /// 数据工厂
    /// </summary>
    public sealed class DataBaseFactory
    {
        public DataBaseFactory()
        { }
        public static IDbConnection GetConnection(DataProvider providerType)
        {
            IDbConnection iDbConnection;
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
            IDbConnection iDbConnection = GetConnection(providerType);
            IDbTransaction iDbTransaction = iDbConnection.BeginTransaction();
            return iDbTransaction;
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
