using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Gscoy.Common
{
   public static class DataTableHelper
    {
        public static void AddColumn(DataTable dt, string columnName, Type type = null)
        {
            if (!dt.Columns.Contains(columnName))
            {
                if (type == null)
                {
                    dt.Columns.Add(columnName);
                }
                else
                {
                    dt.Columns.Add(columnName, type);
                }

            }
        }

        /// <summary>
        /// 从DataTable中获取满足条件的行,返回指定列columnName的值
        /// </summary>
        /// <param name="dt"></param>
        /// <param name="condition">条件</param>
        /// <param name="columnName">列名</param>
        /// <returns></returns>
        public static int GetValueFromTable(DataTable dt, string condition, string columnName)
        {
            int value = 0;
            if (dt != null)
            {
                DataRow[] drs = dt.Select(condition);
                if (drs != null && drs.Length > 0)
                {
                    int.TryParse(drs[0][columnName].ToString(), out value);
                }
            }

            return value;
        }

        /// <summary>
        /// 从DataTable中获取满足条件的行,返回指定列columnName的值
        /// </summary>
        /// <param name="dt"></param>
        /// <param name="condition">条件</param>
        /// <param name="columnName">列名</param>
        /// <returns></returns>
        public static long GetLongValueFromTable(DataTable dt, string condition, string columnName)
        {
            long value = 0;
            if (dt != null)
            {
                DataRow[] drs = dt.Select(condition);
                if (drs != null && drs.Length > 0)
                {
                    long.TryParse(drs[0][columnName].ToString(), out value);
                }
            }

            return value;
        }

        /// <summary>
        /// 从DataTable中获取满足条件的行,返回指定列columnName的值
        /// </summary>
        /// <param name="dt"></param>
        /// <param name="condition">条件</param>
        /// <param name="columnName">列名</param>
        /// <returns></returns>
        public static string GetStringValueFromTable(DataTable dt, string condition, string columnName)
        {
            string value = string.Empty;
            if (dt != null)
            {
                DataRow[] drs = dt.Select(condition);
                if (drs != null && drs.Length > 0)
                {
                    value = drs[0][columnName].ToString();
                }
            }

            return value;
        }

        public static DataTable MergeDataTable(DataTable dt1, DataTable dt2)
        {
            //判断是否需要合并
            if (dt1 == null && dt2 == null)
            {
                return null;
            }
            if (dt1 == null && dt2 != null)
            {
                return dt2.Copy();
            }
            else if (dt1 != null && dt2 == null)
            {
                return dt1.Copy();
            }
            //复制dt1的数据
            DataTable dt = dt1.Copy();
            //补充dt2的结构（dt1中没有的列）到dt中
            for (int i = 0; i < dt2.Columns.Count; i++)
            {
                string cName = dt2.Columns[i].ColumnName;
                if (!dt.Columns.Contains(cName))
                {
                    dt.Columns.Add(new DataColumn(cName));
                }
            }
            //复制dt2的数据
            if (dt2.Rows.Count > 0)
            {
                for (int i = 0; i < dt2.Rows.Count; i++)
                {
                    DataRow dr = dt.NewRow();
                    for (int j = 0; j < dt.Columns.Count; j++)
                    {
                        string cName = dt.Columns[j].ColumnName;
                        if (dt2.Columns.Contains(cName))
                        {
                            try
                            {
                                if (dt2.Rows[i][cName] != null && dt2.Rows[i][cName] != DBNull.Value && dt2.Rows[i][cName].ToString() != "")
                                {
                                    dr[cName] = dt2.Rows[i][cName];
                                }
                            }
                            catch
                            { }
                        }
                    }
                    dt.Rows.Add(dr);
                }
            }
            return dt;
        }

        /// <summary>
        /// 合并数据表
        /// </summary>
        /// <param name="dt1"></param>
        /// <param name="dt2"></param>
        /// <param name="relateColumns">关联字段</param>
        /// <returns></returns>
        public static DataTable MergeDataTable(DataTable dt1, DataTable dt2, List<string> keys)
        {
            if (keys == null || keys.Count <= 0)
            {
                return null;
            }
            DataTable dt = dt1.Copy();
            foreach (DataColumn column in dt2.Columns)
            {
                AddColumn(dt, column.ColumnName);
            }
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                var dr = dt.Rows[i];
                List<string> where = new List<string>();
                for (int j = 0; j < keys.Count(); j++)
                {
                    where.Add(string.Format("{0}='{1}'", keys[j], dr[keys[j]].ToString()));
                }
                var selectDrs = dt2.Select(string.Join(" and ", where));
                if (selectDrs.Length > 0)
                {
                    foreach (DataColumn column in dt2.Columns)
                    {
                        dr[column.ColumnName] = selectDrs[0][column.ColumnName];
                    }
                }
            }
            return dt;
        }

        /// <summary>
        /// 从DataTable中获取满足条件的行,返回指定列columnName的值
        /// </summary>
        /// <param name="dt"></param>
        /// <param name="condition">条件</param>
        /// <param name="columnName">列名</param>
        /// <returns></returns>
        public static int GetIntValueFromTable(DataTable dt, string condition, string columnName)
        {
            int value = 0;
            if (dt != null)
            {
                DataRow[] drs = dt.Select(condition);
                if (drs != null && drs.Length > 0)
                {
                    int.TryParse(drs[0][columnName].ToString(), out value);
                }
            }

            return value;
        }

        /// <summary>
        /// List<T> to DataTable
        /// </summary>
        /// <typeparam name="T">Entity</typeparam>
        /// <param name="entities">Entities</param>
        /// <returns>DataTable</returns>
        internal static DataTable ToDataTable<T>(this List<T> entities) where T : class,new()
        {
            //IsNull return
            if (null == entities || entities.Count == 0)
                return null;
            //Initial Columns
            DataTable dt = new DataTable();
            PropertyInfo[] pArray = typeof(T).GetProperties();
            try
            {
                Array.ForEach<PropertyInfo>(pArray, p =>
                {
                    dt.Columns.Add(p.Name);
                });
                //Initial Rows
                DataRow dr = dt.NewRow();
                entities.ForEach(t =>
                {
                    int i = 0;
                    Array.ForEach<PropertyInfo>(pArray, p =>
                    {
                        if (dt.Columns.Contains(p.Name))
                            dr[i] = p.GetValue(t,null); //Assigned to each column
                    });
                    i++;
                });
                return dt;
            }
            catch (Exception)
            {

                throw;
            }

        }

        /// <summary>
        /// DataTable to Entities
        /// *必须来在于数据库来自于文件可能存在问题*
        /// </summary>
        /// <typeparam name="T">Entity</typeparam>
        /// <param name="dt">DataTable</param>
        /// <returns>List<T</returns>
        internal static List<T> ToEntityList<T>(this DataTable dt)/*必须来在于数据库来自于文件可能存在问题*/ where T : class,new()
        {
            //IsNullable
            if (null == dt || dt.Rows.Count == 0)
                return null;
            //Initial Entities
            List<T> entities = new List<T>();
            try
            {
                foreach (DataRow row in dt.Rows)
                {
                    PropertyInfo[] pArray = typeof(T).GetProperties();
                    T entity = new T();
                    Array.ForEach<PropertyInfo>(pArray, p =>
                    {
                        p.SetValue(entity, row[p.Name], null);
                    });
                    entities.Add(entity);
                }
                return entities;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
