using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;
using Gscoy.Data.DapperExtension.Attributes;

namespace Gscoy.Data
{
    public partial class DataBaseHelper
    {
        #region 静态字段
        /// <summary>
        /// Model列名字典缓存
        /// </summary>
        private static Dictionary<string, List<string>> _columnCache = new Dictionary<string, List<string>>();
        /// <summary>
        /// 表名字典缓存
        /// </summary>
        private static Dictionary<string, string> _tableCache = new Dictionary<string, string>();

        /// <summary>
        /// 主键字典缓存
        /// </summary>
        private static Dictionary<string, string> _pkCache = new Dictionary<string, string>();
        #endregion

        #region 静态属性
        /// <summary> 
        /// 获取，Model列名字段缓存
        /// </summary>
        public static Dictionary<string, List<string>> ColumnCache
        {
            get { return _columnCache; }
        }
        /// <summary>
        /// 表名字典缓存
        /// </summary>
        public static Dictionary<string, string> TableCache
        {
            get
            {
                return _tableCache;
            }
        }

        /// <summary>
        /// 主键字典缓存
        /// </summary>
        public static Dictionary<string, string> PkCache
        {
            get
            {
                return _pkCache;
            }
        }
        #endregion

        #region 缓存操作
        /// <summary>
        /// 私有方法，设置泛型实体类的主键字典
        /// </summary>
        /// <typeparam name="T">泛型实体类型</typeparam>
        /// <param name="t">实体</param>
        private void SetCache<T>(T t) where T : new()
        {
            #region key
            var attrspk = t.GetType().GetCustomAttributes(typeof(PKAttribute), false);
            var pkname = string.Empty;
            pkname = t.GetType().Name;

            if (PkCache.ContainsKey(t.GetType().FullName))
                PkCache[t.GetType().FullName] = pkname;
            else
                PkCache.Add(t.GetType().FullName, pkname);
            #endregion

            #region column
            List<string> colList = new List<string>();
            PropertyInfo[] propcoll = t.GetType().GetProperties();//获取该类型的所有属性
            foreach (var item in propcoll)
            {
                object[] objattribute = item.GetCustomAttributes(typeof(ColumnAttribute), false);
                if (objattribute.Length > 0)
                    //if ((objattribute[0] as ColumnAttribute).IsPrimary)//如果该属性是用主键特性标记为主键的则添加为该类型的主键
                    colList.Add(item.Name);
            }
            if (ColumnCache.ContainsKey(t.GetType().FullName))
                ColumnCache[t.GetType().FullName] = colList;//设置新的主键集合
            else
                ColumnCache.Add(t.GetType().FullName, colList);
            #endregion

            #region table
            var attrs = t.GetType().GetCustomAttributes(typeof(TableAttribute), false);
            var name = string.Empty;
            if (attrs.Length > 0)
            {
                name = (attrs[0] as TableAttribute).TableName;//获取特性的表名
            }
            else
            {
                name = t.GetType().Name;
            }
            if (TableCache.ContainsKey(t.GetType().FullName))
                TableCache[t.GetType().FullName] = name;
            else
                TableCache.Add(t.GetType().FullName, name);
            #endregion
        }
        /// <summary>
        /// 私有方法，获取泛型实体类的主键集合
        /// </summary>
        /// <typeparam name="T">泛型实体类型</typeparam>
        /// <param name="t">实体</param>
        /// <returns>字典集合</returns>
        private List<string> GetCache<T>(T t, ref string tableName) where T : new()
        {
            var list = new List<string>();
            if (ColumnCache.Count <= 0)
                SetCache<T>(t);
            list = CheckCacheValue<T>(t, ref tableName);//直接返回
            if (list.Count > 0) return list;
            SetCache<T>(t);
            list = CheckCacheValue<T>(t, ref tableName);//再次返回，如没有则说明该Model没有定义column特性
            if (list.Count > 0) return list;
            return null;
        }

        /// <summary>
        /// 检查是否存在于缓存中，并返回相应的值
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="t"></param>
        /// <param name="tableName"></param>
        /// <returns></returns>
        private static List<string> CheckCacheValue<T>(T t, ref string tableName) where T : new()
        {
            var columns = new List<string>();
            if (ColumnCache.ContainsKey(t.GetType().FullName))
            {
                columns = ColumnCache[t.GetType().FullName];
            }
            if (string.IsNullOrEmpty(tableName))//只有没有传入表名时才会去从缓存里面取
            {
                if (TableCache.ContainsKey(t.GetType().FullName))
                {
                    tableName = TableCache[t.GetType().FullName];
                }
            }
            return columns;
        }
        #endregion

        #region 私有方法 生成sql语句
        StringBuilder builder;
        List<string> ColumnList;
        string TableName;
        DapperCommand cmd;
        /// <summary>
        /// 生成sql准备工作
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="t"></param>
        /// <param name="tableName"></param>
        private void GetSql<T>(T t, TableAction action = TableAction.Insert, string tableName = null) where T : new()
        {
            ColumnList = GetCache<T>(t, ref tableName);//获取该实体的所有主键列表
            if (ColumnList.Count <= 0)
                throw new Exception("错误:没有列名的实体无法进行sql语句拼接操作");
            builder = new StringBuilder();
            GetDataParam<T>(t);
            switch (action)
            {
                case TableAction.Insert:
                    break;
                case TableAction.Delete:
                    break;
                case TableAction.Update:
                    break;
                case TableAction.Select:
                    break;
                default:
                    break;
            }
        }

        /// <summary>
        /// 根据属性拼接数据库参数
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="t"></param>
        /// <returns></returns>
        private void GetDataParam<T>(T t) where T : new()
        {
            var type = typeof(T);
            CreateParameters(ColumnCache[type.FullName].Count);
            int count = 0;
            //根据主键生成SQL语句
            foreach (string sub in ColumnList)
            {
                var property = type.GetProperty(sub);
                var val = property.GetValue(t, null);
                AddParameters(count, sub, val);
                count++;
            }
        }

        /// <summary>
        /// 生成insert语句
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="t"></param>
        /// <param name="tableName"></param>
        /// <returns></returns>
        private DapperCommand GetInsertSql<T>(T t, string tableName = null) where T : new()
        {
            bool isand = true;//控制语句拼接的and条件
            //根据列名生成SQL语句
            foreach (string sub in ColumnList)
            {
                if (isand)
                {
                    builder.AppendFormat("{0}=@{0}", sub);
                }
                else
                {
                    builder.AppendFormat("and {0}=@{0}", sub);
                }
            }
            var sql = string.Format("DELETE FROM {0} WHERE {1}", TableName, builder.ToString());
            cmd.CommandText = sql;
            foreach (var item in db.Parameters)
            {
                cmd.CommandParameter.Add(item);
            }
            return cmd;
        }
        #endregion
    }

    /// <summary>
    /// 数据库的常规表操作
    /// </summary>
    enum TableAction
    {
        /// <summary>
        /// 增
        /// </summary>
        Insert,
        /// <summary>
        /// 删
        /// </summary>
        Delete,
        /// <summary>
        /// 改
        /// </summary>
        Update,
        /// <summary>
        /// 查
        /// </summary>
        Select
    }

    class DapperCommand
    {
        /// <summary>
        /// sql
        /// </summary>
        public string CommandText { get; set; }

        /// <summary>
        /// sql paras
        /// </summary>
        public IList<IDbDataParameter> CommandParameter { get; set; }
    }
}
