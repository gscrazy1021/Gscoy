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
        /// Model主键字典缓存
        /// </summary>
        private static Dictionary<string, List<string>> _pkcache = new Dictionary<string, List<string>>();
        #endregion

        #region 静态属性
        /// <summary> 
        /// 获取，Model主键字段缓存
        /// </summary>
        public static Dictionary<string, List<string>> PKCache
        {
            get { return _pkcache; }
        }
        #endregion

        #region 缓存操作
        /// <summary>
        /// 私有方法，设置泛型实体类的主键字典
        /// </summary>
        /// <typeparam name="T">泛型实体类型</typeparam>
        /// <param name="t">实体</param>
        private void SetPKCache<T>(T t) where T : new()
        {
            List<string> pklist = new List<string>();
            PropertyInfo[] propcoll = t.GetType().GetProperties();//获取该类型的所有属性
            foreach (var item in propcoll)
            {
                object[] objattribute = item.GetCustomAttributes(typeof(ColumnAttribute), false);
                if (objattribute.Length > 0)
                    if ((objattribute[0] as ColumnAttribute).IsPrimary)//如果该属性是用主键特性标记为主键的则添加为该类型的主键
                        pklist.Add(item.Name);
            }
            if (PKCache.ContainsKey(t.GetType().FullName))
                PKCache[t.GetType().FullName] = pklist;//设置新的主键集合
            else
                PKCache.Add(t.GetType().FullName, pklist);
        }
        /// <summary>
        /// 私有方法，获取泛型实体类的主键集合
        /// </summary>
        /// <typeparam name="T">泛型实体类型</typeparam>
        /// <param name="t">实体</param>
        /// <returns>字典集合</returns>
        private List<string> GetPKCache<T>(T t) where T : new()
        {
            if (PKCache.Count <= 0)
                SetPKCache<T>(t);
            if (PKCache.ContainsKey(t.GetType().FullName))
                return PKCache[t.GetType().FullName];//直接返回
            SetPKCache<T>(t);
            if (PKCache.ContainsKey(t.GetType().FullName))
                return PKCache[t.GetType().FullName];//再次返回，如没有则说明该Model没有定义主键特性
            return null;
        }
        #endregion

        #region 私有方法 生成sql语句
        StringBuilder builder;
        List<string> Pklist;
        string TableName;
        IDbCommand cmd;
        /// <summary>
        /// 生成sql准备工作
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="t"></param>
        /// <param name="tableName"></param>
        private void GetSql<T>(T t, string tableName = null) where T : new()
        {
            builder = new StringBuilder();
            if (!string.IsNullOrEmpty(tableName))
            {
                TableName = tableName;
            }
            else
            {
                var type = typeof(T);
                var attributes = type.GetCustomAttributes(typeof(TableAttribute), false);
                if (attributes.Length > 0)
                {
                    TableName = (attributes[0] as TableAttribute).TableName;//获取特性的表名
                }
                else
                {
                    //如果没有用特性标记为表名称，默认用实体的名称作为表的名称
                    TableName = type.Name;
                }
            }
            List<string> Pklist = GetPKCache<T>(t);//获取该实体的所有主键列表
            if (Pklist.Count <= 0)
                throw new Exception("错误:没有主键的实体无法进行删除操作");
        }

        /// <summary>
        /// 根据属性拼接数据库参数
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="t"></param>
        /// <returns></returns>
        private void GetDataParam<T>(T t) where T : new()
        {
            CreateParameters(PKCache.Count);
            var type = typeof(T);
            int count = 0;
            //根据主键生成SQL语句
            foreach (string sub in Pklist)
            {
                var property = type.GetProperty(sub);
                var val = property.GetValue(type, null);
                AddParameters(count, sub, val);
            }
        }

        /// <summary>
        /// 生成insert语句
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="t"></param>
        /// <param name="tableName"></param>
        /// <returns></returns>
        public IDbCommand GetInsertSql<T>(T t, string tableName = null) where T : new()
        {
            GetSql<T>(t, tableName);
            GetDataParam<T>(t);
            bool isand = true;//控制语句拼接的and条件
            //根据主键生成SQL语句
            foreach (string sub in Pklist)
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
                cmd.Parameters.Add(item);
            }
            return cmd;
        }
        #endregion



    }
}
