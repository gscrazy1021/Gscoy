using Gscoy.Common.Enums;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace Gscoy.Data
{
    public class DataBase : IDataBase, IDisposable
    {
        #region 字段

        private DataProvider _providerType;
        private IDbConnection _idbConnection;
        private IDataReader _iDataReader;
        private IDbCommand _idbCommand;
        private IDbTransaction _idbTransaction;
        private IDbDataParameter[] _idbParameters;
        private string _connectionString;

        #endregion

        #region 构造方法

        public DataBase()
        {
        }

        public DataBase(DataProvider providerType)
        {
            ProviderType = providerType;
        }

        public DataBase(DataProvider providerType, string connectionString)
        {
            ProviderType = providerType;
            ConnectionString = connectionString;
        }

        #endregion

        #region 属性

        public DataProvider ProviderType
        {
            get { return _providerType; }
            set { _providerType = value; }
        }

        public IDbConnection Connection
        {
            get { return _idbConnection; }
            set { _idbConnection = value; }
        }

        public IDataReader DataReader
        {
            get { return _iDataReader; }
            set { _iDataReader = value; }
        }

        public IDbCommand Command
        {
            get { return _idbCommand; }
            set { _idbCommand = value; }
        }

        public IDbTransaction Transaction
        {
            get { return _idbTransaction; }
            set { _idbTransaction = value; }
        }

        public IDbDataParameter[] Parameters
        {
            get { return _idbParameters; }
            set { _idbParameters = value; }
        }

        public string ConnectionString
        {
            get { return _connectionString; }
            set { _connectionString = value; }
        }

        #endregion

        #region 公有方法

        public void Open()
        {
            Connection = DataBaseFactory.GetConnection(ProviderType);
            Connection.ConnectionString = ConnectionString;
            if (Connection.State != ConnectionState.Open)
            {
                Connection.Open();
            }
            Command = DataBaseFactory.GetCommand(ProviderType);
        }

        public void Close()
        {
            if (Connection.State != ConnectionState.Closed)
            {
                Connection.Close();
            }
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
            Close();
            Command = null;
            Transaction = null;
            Connection = null;
        }

        public void CreateParameters(int paramsCount)
        {
            Parameters = new IDbDataParameter[paramsCount];
            Parameters = DataBaseFactory.GetParameters(ProviderType, paramsCount);
        }

        public void AddParameters(int index, string paramName, object objValue)
        {
            if (index < Parameters.Length)
            {
                Parameters[index].ParameterName = paramName;
                Parameters[index].Value = objValue;
            }
        }

        public void BeginTransaction()
        {
            if (Transaction == null)
            {
                Transaction = DataBaseFactory.GetTransaction(ProviderType);
            }
            Command.Transaction = Transaction;
        }

        public void CommitTransaction()
        {
            if (Transaction != null)
            {
                Transaction.Commit();
            }
            Transaction = null;
        }

        public void CloseReader()
        {
            if (DataReader != null)
            {
                DataReader.Close();
            }
        }

        public IDataReader ExecuteReader(CommandType commandType, string commandText)
        {
            Command = DataBaseFactory.GetCommand(ProviderType);
            Command.Connection = Connection;
            PrepareCommand(Command, Connection, Transaction, commandType, commandText, Parameters);
            DataReader = Command.ExecuteReader();
            Command.Parameters.Clear();
            return DataReader;
        }

        public int ExecuteNonQuery(CommandType commandType, string commandText)
        {
            Command = DataBaseFactory.GetCommand(ProviderType);
            PrepareCommand(Command, Connection, Transaction, commandType, commandText, Parameters);
            int returnValue = Command.ExecuteNonQuery();
            Command.Parameters.Clear();
            return returnValue;
        }

        public object ExecuteScalar(CommandType commandType, string commandText)
        {
            Command = DataBaseFactory.GetCommand(ProviderType);
            PrepareCommand(Command, Connection, Transaction, commandType, commandText, Parameters);
            object returnValue = Command.ExecuteScalar();
            Command.Parameters.Clear();
            return returnValue;
        }

        public DataSet ExecuteDataSet(CommandType commandType, string commandText)
        {
            Command = DataBaseFactory.GetCommand(ProviderType);
            PrepareCommand(Command, Connection, Transaction, commandType, commandText, Parameters);
            IDbDataAdapter dataAdapter = DataBaseFactory.GetDataAdapter(ProviderType);
            dataAdapter.SelectCommand = Command;
            DataSet dataSet = new DataSet();
            dataAdapter.Fill(dataSet);
            Command.Parameters.Clear();
            return dataSet;
        }

        #endregion

        #region 私有方法

        private void AttachParameters(IDbCommand command, IDbDataParameter[] commandParameters)
        {
            foreach (IDbDataParameter idbParameter in commandParameters)
            {
                if (idbParameter.Direction == ParameterDirection.InputOutput && idbParameter.Value == null)
                {
                    idbParameter.Value = DBNull.Value;
                }
                command.Parameters.Add(idbParameter);
            }
        }

        private void PrepareCommand(IDbCommand command, IDbConnection connection, IDbTransaction transaction,
                                    CommandType commandType, string commandText, IDbDataParameter[] commandParameters)
        {
            command.Connection = connection;
            command.CommandText = commandText;
            command.CommandType = commandType;
            if (transaction != null)
            {
                command.Transaction = transaction;
            }
            if (commandParameters != null)
            {
                AttachParameters(command, commandParameters);
            }
        }

        #endregion

    }
}
