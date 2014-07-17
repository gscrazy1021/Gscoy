using Gscoy.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace Gscoy.Data.DapperExtension
{
    /// <summary>
    /// sql加工与拼装
    /// </summary>
    public class SqlMerge
    {
        #region 变量与属性
        private static readonly string dbType = ConfigHelper.GetConfig("DataProvider", "MSSQL");
        public static DataProvider providerType = DataProviderHelper.GetDataProvider(dbType);

        private object lockObj = new object();
        /// <summary>
        /// 查询TOP
        /// </summary>
        public int Top { get; set; }
        /// <summary>
        /// 排序
        /// </summary>
        public QueryOrder Order { get; set; }
        /// <summary>
        /// 组装的SQL WHERE部分
        /// </summary>
        public StringBuilder Sql { get; set; }
        /// <summary>
        /// 参数动态类
        /// </summary>
        public IList<DynamicParam> Param { get; set; }
        /// <summary>
        /// 参数前缀
        /// </summary>
        protected string ParamPrefix = "@";
        /// <summary>
        /// 处理的实体对象描述
        /// </summary>
        public ModelDes ModelDescription { get; set; }
        protected int pageIndex = 0;
        protected int pageSize = 10;
        /// <summary>
        /// 数据库类型
        /// </summary>
        public DataProvider Provider { get { return providerType; } }
        //动态参数类缓存
        protected static Dictionary<string, Type> DynamicParamModelCache = new Dictionary<string, Type>();
        /// <summary>
        /// 记录参数名和值
        /// </summary>
        protected Dictionary<string, object> ParamValues = new Dictionary<string, object>();
        #endregion

        /// <summary>
        /// SQL字符串,只表示包括Where部分
        /// </summary>
        public string WhereSql
        {
            get
            {
                var bulider = new StringBuilder();
                var arr = Sql.ToString().Split(' ').Where(m => !string.IsNullOrEmpty(m)).ToList();
                if (arr.Count > 0)
                {
                    bulider.Append(" where ");
                }
                for (int i = 0; i < arr.Count; i++)
                {
                    if (i == 0 && (arr[i].ToUpper() == "AND" || arr[i].ToUpper() == "OR"))
                    {
                        continue;
                    }
                    if (i > 0 && arr[i - 1] == "(" && (arr[i].ToUpper() == "AND" || arr[i].ToUpper() == "OR"))
                    {
                        continue;
                    }
                    bulider.Append(" ");
                    bulider.Append(arr[i]);
                }
                return bulider.ToString();
            }
        }

        /// <summary>
        /// 排序
        /// </summary>
        public string OrderSql
        {
            get
            {
                var builder = new StringBuilder();
                if (Order != null)
                {
                    builder.AppendFormat(" order by {0} {1}", Order.Field, Order.IsDesc ? "desc" : "asc");
                }
                return builder.ToString();
            }
        }

        /// <summary>
        /// 查询参数对象
        /// </summary>
        public object QueryParams
        {
            get
            {
                if (this.Param != null && Param.Count > 0)
                {
                    #region 处理参数对象是否存在缓存中
                    var paramKeys = this.ParamValues.Keys.ToList();//当前使用的参数集合
                    var listCacheKeys = new List<string>();
                    listCacheKeys.Add(ModelDescription.TableName);
                    listCacheKeys.AddRange(paramKeys);

                    var cacheKey = string.Empty;
                    var keyList = DynamicParamModelCache.Keys.Where(m => m.StartsWith(ModelDescription.TableName));
                    foreach (var key in keyList)
                    {
                        if (listCacheKeys.All(m => key.Split('_').Contains(m)))//当前参数是否是一样已经缓存的子集
                        {
                            cacheKey = key;
                            break;
                        }
                    }
                    if (string.IsNullOrEmpty(cacheKey))//为空则说明缓存不存在相应数据类型
                    {
                        cacheKey = string.Join("_", listCacheKeys);
                    }
                    #endregion

                    Type modelType;
                    lock (lockObj)//防止多线程同时操作DynamicParamModelCache
                    {
                        DynamicParamModelCache.TryGetValue(cacheKey, out modelType);
                        if (modelType == null)
                        {
                            lock (lockObj)//防止多线程同时操作DynamicParamModelCache
                            {
                                var tyName = "CustomDynamicParamClass";
                                modelType = CustomDynamicBuilder.DynamicCreateType(tyName, Param);
                                DynamicParamModelCache.Add(cacheKey, modelType);
                            }
                        }
                    }
                    var model = Activator.CreateInstance(modelType);
                    foreach (var item in this.ParamValues)
                    {
                        modelType.GetProperty(item.Key).SetValue(model, item.Value, null);
                    }

                    return model;
                }
                else
                {
                    return null;
                }
            }
        }
        /// <summary>
        /// 插入语句SQL
        /// </summary>
        public string InsertSql
        {
            get
            {
                StringBuilder sql = new StringBuilder();
                sql.Append(string.Format("INSERT INTO {0}(", ModelDescription.TableName));
                var colums = MergeHelper.GetExecColumns(ModelDescription);
                for (int i = 0; i < colums.Count; i++)
                {
                    if (i == 0) sql.Append(colums[i].ColumnName);
                    else sql.Append(string.Format(",{0}", colums[i].ColumnName));
                }
                sql.Append(")");
                sql.Append(" VALUES(");
                for (int i = 0; i < colums.Count; i++)
                {
                    if (i == 0) sql.Append(string.Format("{0}{1}", ParamPrefix, colums[i].FieldName));
                    else sql.Append(string.Format(",{0}{1}", ParamPrefix, colums[i].FieldName));
                }
                sql.Append(") ");
                return sql.ToString();
            }
        }
        /// <summary>
        /// 删除SQL
        /// </summary>
        public virtual string DeleteSql
        {
            get
            {
                return string.Format("DELETE FROM {0} {1}", ModelDescription.TableName, this.WhereSql);
            }
        }
        /// <summary>
        /// 修改SQL
        /// </summary>
        public virtual string UpdateSql
        {
            get
            {
                StringBuilder sql = new StringBuilder();
                sql.Append(string.Format("UPDATE {0} SET", ModelDescription.TableName));
                var colums = MergeHelper.GetExecColumns(ModelDescription, false);
                for (int i = 0; i < colums.Count; i++)
                {
                    if (i != 0) sql.Append(",");
                    sql.Append(" ");
                    sql.Append(colums[i].ColumnName);
                    sql.Append(" ");
                    sql.Append("=");
                    sql.Append(" ");
                    sql.Append(ParamPrefix + colums[i].FieldName);
                }
                if (string.IsNullOrEmpty(WhereSql))//没有where条件的情况
                {
                    var p = MergeHelper.GetPrimary(ModelDescription);
                    sql.Append(string.Format(" WHERE {0}={1}", p.Column, ParamPrefix + p.Field));
                }
                else
                {
                    sql.Append(string.Format(" {0}", WhereSql));
                }

                return sql.ToString();
            }
        }
        /// <summary>
        /// 查询SQL
        /// </summary>
        public virtual string QuerySql
        {
            get
            {
                var sqlStr = "";
                if (Top > 0)
                {
                    if (Provider == DataProvider.MSSQL)
                        sqlStr = string.Format("SELECT TOP {0} {1} FROM {2} {3} {4}", Top, "*", ModelDescription.TableName, this.WhereSql, this.OrderSql);
                    //else if (Provider == DataProvider.Sqlite)
                    //{
                    //    sqlStr = string.Format("SELECT  {1} FROM {2} {3} {4} limit", Top, "*", ModelDescription.TableName, this.WhereSql, this.OrderSql);
                    //}
                    else
                    {
                        sqlStr = string.Format("SELECT {0} FROM {1} {2} {3} LIMIT {4}", "*", ModelDescription.TableName, this.WhereSql, this.OrderSql, Top);
                    }
                }
                else
                {
                    sqlStr = string.Format("SELECT {0} FROM {1} {2} {3}", "*", ModelDescription.TableName, this.WhereSql, this.OrderSql);
                }
                return sqlStr;
            }
        }
        /// <summary>
        /// 分页SQL
        /// </summary>
        public virtual string PageSql
        {
            get
            {
                var sqlPage = "";
                var orderStr = string.IsNullOrEmpty(this.OrderSql) ? "ORDER BY " + ModelDescription.Properties.FirstOrDefault().Column : this.OrderSql;

                if (Provider == DataProvider.MSSQL)
                {
                    sqlPage = string.Format("SELECT * FROM (SELECT ROW_NUMBER() OVER ({0}) rid, {1} {2}) p_paged WHERE rid>{3} AND rid<={4}",
                                             orderStr, "*", this.WhereSql, (pageIndex - 1) * pageSize, (pageIndex - 1) * pageSize + pageSize);
                }
                else
                {
                    sqlPage = string.Format("SELECT * FROM {0} {1} {2} LIMIT {1} OFFSET {2}",
                        ModelDescription.TableName, this.WhereSql, orderStr, (pageIndex - 1) * pageSize, (pageIndex - 1) * pageSize + pageSize);
                }
                return sqlPage;
            }
        }
        /// <summary>
        /// 数据总是SQL	
        /// </summary>
        public virtual string CountSql
        {
            get
            {
                return string.Format("SELECT COUNT(*) DataCount FROM {0} {1}", ModelDescription.TableName, this.WhereSql);
            }
        }

        protected SqlMerge()
        {
            this.Sql = new StringBuilder();
        }
        /// <summary>
        /// TOP
        /// </summary>
        /// <param name="top"></param>
        /// <returns></returns>
        public SqlMerge SelectTop(int top)
        {
            this.Top = top;
            return this;
        }
        /// <summary>
        /// 奖其它参数添加到参数对象中
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="t"></param>
        /// <returns></returns>
        internal SqlMerge AppendParam<T>(T t) where T : class
        {
            if (this.Param == null)
            {
                this.Param = new List<DynamicParam>();
            }
            var model = MergeHelper.GetModelDes<T>();
            foreach (var item in model.Properties)
            {
                var value = model.ClassType.GetProperty(item.Field).GetValue(t, null);
                this.ParamValues.Add(item.Field, value);
                var pmodel = new DynamicParam();
                pmodel.Name = item.Field;
                pmodel.PropertyType = value.GetType();
                this.Param.Add(pmodel);
            }
            return this;
        }
        /// <summary>
        /// 分页条件
        /// </summary>
        /// <param name="pindex"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public SqlMerge Page(int pindex, int pageSize)
        {
            this.pageIndex = pindex;
            this.pageSize = pageSize;
            return this;
        }
    }

    /// <summary>
    ///  组装查询
    /// </summary>
    public class SqlMerge<T> : SqlMerge where T : class
    {
        private SqlMerge(string tableName = "")
            : base()
        {
            ModelDescription = MergeHelper.GetModelDes<T>(tableName);
        }

        public static SqlMerge<T> Builder(string tableName = "")
        {
            var result = new SqlMerge<T>(tableName);
            return result;
        }

        /// <summary>
        /// 创建排序
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="expr"></param>
        /// <param name="desc"></param>
        /// <returns></returns>
        public SqlMerge<T> OrderBy(Expression<Func<T, object>> expr, bool desc)
        {
            var field = MergeHelper.GetPropertyByExpress<T>(ModelDescription, expr).Column;
            this.Order = new QueryOrder() { Field = field, IsDesc = desc };
            return this;
        }
        /// <summary>
        /// TOP
        /// </summary>
        /// <param name="top"></param>
        /// <returns></returns>
        public new SqlMerge<T> SelectTop(int top)
        {
            this.Top = top;
            return this;
        }
        /// <summary>
        /// 左括号(
        /// </summary>
        /// <param name="isAnd">true为AND false为OR</param>
        /// <returns></returns>
        public SqlMerge<T> LeftInclude(bool isAnd = true)
        {
            var cn = isAnd ? "AND" : "OR";
            this.Sql.Append(" ");
            this.Sql.Append(cn);
            this.Sql.Append(" ");
            this.Sql.Append("(");
            return this;
        }
        /// <summary>
        /// 右括号)
        /// </summary>
        /// <returns></returns>
        public SqlMerge<T> RightInclude()
        {
            this.Sql.Append(" ");
            this.Sql.Append(")");
            return this;
        }
        /// <summary>
        /// AND方式连接一条查询条件
        /// </summary>
        /// <param name="expr"></param>
        /// <param name="operation"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public SqlMerge<T> AndWhere(Expression<Func<T, object>> expr, OperationMethod operation, object value)
        {
            return Where(expr, operation, value, true);
        }
        /// <summary>
        ///  Or方式连接一条查询条件
        /// </summary>
        /// <param name="expr"></param>
        /// <param name="operation"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public SqlMerge<T> OrWhere(Expression<Func<T, object>> expr, OperationMethod operation, object value)
        {
            return Where(expr, operation, value, false);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="expr"></param>
        /// <param name="operation"></param>
        /// <param name="value"></param>
        /// <param name="isAnd">true为AND false为OR</param>
        /// <returns></returns>
        private SqlMerge<T> Where(Expression<Func<T, object>> expr, OperationMethod operation, object value, bool isAnd)
        {
            var cn = isAnd ? "AND" : "OR";
            var field = MergeHelper.GetPropertyByExpress<T>(ModelDescription, expr).Column;
            var op = GetOpStr(operation);
            StringBuilder sb = new StringBuilder();
            this.Sql.Append(" ");
            this.Sql.Append(cn);
            this.Sql.Append(" ");
            this.Sql.Append(field);
            this.Sql.Append(" ");
            this.Sql.Append(op);
            this.Sql.Append(" ");
            var model = AddParam(operation, field, value);
            this.Sql.Append(this.ParamPrefix + model.Name);

            return this;
        }
        /// <summary>
        /// 比较符
        /// </summary>
        /// <param name="method"></param>
        /// <returns></returns>
        private string GetOpStr(OperationMethod method)
        {
            switch (method)
            {
                case OperationMethod.Contains:
                    return "LIKE";
                case OperationMethod.EndsWith:
                    return "LIKE";
                case OperationMethod.Equal:
                    return "=";
                case OperationMethod.Greater:
                    return ">";
                case OperationMethod.GreaterOrEqual:
                    return ">=";
                case OperationMethod.In:
                    return "IN";
                case OperationMethod.Less:
                    return "<";
                case OperationMethod.LessOrEqual:
                    return "<=";
                case OperationMethod.NotEqual:
                    return "<>";
                case OperationMethod.StartsWith:
                    return "LIKE";
            }
            return "=";
        }
        /// <summary>
        /// 创建参数
        /// </summary>
        /// <param name="operation"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        private object CreateParam(OperationMethod method, object value)
        {
            switch (method)
            {
                case OperationMethod.Contains:
                    return string.Format("%{0}%", value);
                case OperationMethod.EndsWith:
                    return string.Format("%{0}", value);
                case OperationMethod.Equal:
                    return value;
                case OperationMethod.Greater:
                    return value;
                case OperationMethod.GreaterOrEqual:
                    return value;
                case OperationMethod.In:
                    return value;
                case OperationMethod.Less:
                    return value;
                case OperationMethod.LessOrEqual:
                    return value;
                case OperationMethod.NotEqual:
                    return value;
                case OperationMethod.StartsWith:
                    return string.Format("{0}%", value);
            }
            return value;
        }
        /// <summary>
        /// 通过方法和值创建一个参数对象并记录
        /// </summary>
        /// <param name="method"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        private DynamicParam AddParam(OperationMethod method, string field, object value)
        {
            if (this.Param == null)
            {
                this.Param = new List<DynamicParam>();
            }

            var model = new DynamicParam();
            model.Name = field + GetParamIndex(field);
            model.PropertyType = value.GetType();
            this.Param.Add(model);

            switch (method)
            {
                #region
                case OperationMethod.Contains:
                    this.ParamValues.Add(model.Name, string.Format("%{0}%", value));
                    break;
                case OperationMethod.EndsWith:
                    this.ParamValues.Add(model.Name, string.Format("%{0}", value));
                    break;
                case OperationMethod.Equal:
                    this.ParamValues.Add(model.Name, value);
                    break;
                case OperationMethod.Greater:
                    this.ParamValues.Add(model.Name, value);
                    break;
                case OperationMethod.GreaterOrEqual:
                    this.ParamValues.Add(model.Name, value);
                    break;
                case OperationMethod.In:
                    this.ParamValues.Add(model.Name, value);
                    break;
                case OperationMethod.Less:
                    this.ParamValues.Add(model.Name, value);
                    break;
                case OperationMethod.LessOrEqual:
                    this.ParamValues.Add(model.Name, value);
                    break;
                case OperationMethod.NotEqual:
                    this.ParamValues.Add(model.Name, value);
                    break;
                case OperationMethod.StartsWith:
                    this.ParamValues.Add(model.Name, string.Format("{0}%", value));
                    break;
                #endregion
            }
            return model;
        }

        private string GetParamIndex(string field)
        {
            var key = this.ParamValues.Keys.Where(m => m.StartsWith(field)).FirstOrDefault();
            if (key == null)
            {
                return "1";
            }
            else
            {
                return (int.Parse(key.Remove(0, field.Length)) + 1).ToString();
            }
        }
    }
}
