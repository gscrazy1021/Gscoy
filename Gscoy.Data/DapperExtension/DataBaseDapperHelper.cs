using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Gscoy.Dapper;
using System.Data;
using Gscoy.Data.DapperExtension;

namespace Gscoy.Data
{
    /// <summary>
    /// Dapper扩展
    /// </summary>
    public partial class DataBaseHelper
    {
        IDbConnection iconn = db.Connection;
        /// <summary>
        /// 得到列表
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="sql"></param>
        /// <returns></returns>
        public IEnumerable<T> QueryList<T>(string sql)
        {
            var s = iconn.Query<T>(sql, transaction: null, commandType: CommandType.Text);
            return s;
        }

        /// <summary>
        /// ExecuteNonQuery
        /// </summary>
        /// <param name="sql"></param>
        /// <returns></returns>
        public int ExecuteNonQueryDapper(string sql)
        {
            db.BeginTransaction();
            var tran = db.Transaction;
            var result = 0;
            try
            {
                result = iconn.Execute(sql, transaction: tran, commandType: CommandType.Text);
            }
            catch (Exception ex)
            {
                db.RollBackTranscation();
                throw;
            }
            finally
            {
                db.CommitTransaction();
                db.Dispose();
            }
            return result;
        }

        /// <summary>
        /// 插入数据
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="dbs"></param>
        /// <param name="t"></param>
        /// <param name="transaction"></param>
        /// <param name="commandTimeout"></param>
        /// <returns></returns>
        public bool Insert<T>(T t, int? commandTimeout = null) where T : class,new()
        {
            var sql = SqlMerge<T>.Builder();
            db.BeginTransaction();
            var tran = db.Transaction;
            var result = 0;
            try
            {
                result = iconn.Execute(sql.InsertSql, t, tran, commandTimeout);
            }
            catch (Exception ex)
            {
                db.RollBackTranscation();
            }
            finally
            {
                db.CommitTransaction();
                db.Dispose();
            }
            return result == 1;
        }
        /// <summary>
        ///  批量插入数据
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="dbs"></param>
        /// <param name="lt"></param>
        /// <param name="transaction"></param>
        /// <param name="commandTimeout"></param>
        /// <returns></returns>
        public bool InsertBatch<T>(IList<T> lt, int? commandTimeout = null) where T : class,new()
        {
            var sql = SqlMerge<T>.Builder();
            db.BeginTransaction();
            var tran = db.Transaction;
            var result = 0;
            try
            {
                result = iconn.Execute(sql.InsertSql, lt, tran, commandTimeout);
            }
            catch (Exception ex)
            {
                db.RollBackTranscation();
            }
            finally
            {
                db.CommitTransaction();
                db.Dispose();
            }
            return result == lt.Count;
        }
        /// <summary>
        /// 获取默认一条数据，没有则为NULL
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="dbs"></param>
        /// <param name="sql"></param>
        /// <returns></returns>
        public T FirstOrDefault<T>(string tableName = "") where T : class
        {
            var sql = SqlMerge<T>.Builder(tableName);
            db.BeginTransaction();
            var tran = db.Transaction;
            sql = sql.SelectTop(1);
            var result = iconn.Query<T>(sql.QuerySql, sql.Param, tran);
            db.CommitTransaction();
            return result.FirstOrDefault();
        }
        /// <summary>
        /// 按条件删除
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="dbs"></param>
        /// <param name="sql"></param>
        /// <returns></returns>
        public bool Delete<T>(string tableName = "") where T : class
        {
            var sql = SqlMerge<T>.Builder(tableName);
            db.BeginTransaction();
            var tran = db.Transaction;
            var result = 0;
            try
            {
                result = iconn.Execute(sql.DeleteSql, sql.Param, tran);
            }
            catch (Exception ex)
            {
                db.RollBackTranscation();
            }
            finally
            {
                db.CommitTransaction();
                db.Dispose();
            }
            return result > 0;
        }
        /// <summary>
        /// 修改
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="dbs"></param>
        /// <param name="t">如果sql为null，则根据t的主键进行修改</param>
        /// <param name="sql">按条件修改</param>
        /// <returns></returns>
        public bool Update<T>(T t, string tableName = "") where T : class,new()
        {
            var sql = SqlMerge<T>.Builder(tableName);
            db.BeginTransaction();
            var tran = db.Transaction;
            var result = 0;
            try
            {
                //sql = sql.AppendParam<T>(t);
                result = iconn.Execute(sql.UpdateSql, sql.Param);
            }
            catch (Exception ex)
            {
                db.RollBackTranscation();
            }
            finally
            {
                db.CommitTransaction();
                db.Dispose();
            }
            return result > 0;
        }

        /// <summary>
        /// 查询
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="dbs"></param>
        /// <param name="sql"></param>
        /// <returns></returns>
        public IEnumerable<T> Query<T>(string tableName = "") where T : class
        {
            var sql = SqlMerge<T>.Builder(tableName);
            db.BeginTransaction();
            var tran = db.Transaction;
            try
            {
                var result = iconn.Query<T>(sql.QuerySql, sql.Param, tran);
                return result.ToList();
            }
            catch (Exception ex)
            {
                db.RollBackTranscation();
                return null;
            }
            finally
            {
                db.CommitTransaction();
                db.Dispose();
            }
        }
        /// <summary>
        /// 数据数量
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="dbs"></param>
        /// <param name="sql"></param>
        /// <returns></returns>
        public long Count<T>(string tableName = "") where T : class
        {
            var sql = SqlMerge<T>.Builder(tableName);
            db.BeginTransaction();
            var tran = db.Transaction;
            try
            {
                var result = Query<T>(tableName);
                return 0;
            }
            catch (Exception ex)
            {
                db.RollBackTranscation();
                return -1;
            }
            finally
            {
                db.CommitTransaction();
                db.Dispose();
            }
        }
    }
}
