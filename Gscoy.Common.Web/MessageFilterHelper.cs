using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace Gscoy.Common.Web
{
    /// <summary>
    /// 议价，委托留言关键字处理
    /// </summary>
    public class MessageFilterHelper
    {
        private static string url = ConfigHelper.GetConfig("MessageKeyWordUrl");
        private static object lockObject = new object();

        /// <summary>
        /// 获取议价，委托留言关键字列表
        /// </summary>
        /// <returns></returns>
        private static string[] GetKeywords()
        {
            string cacheName = "keywords_message";
            System.Web.Caching.Cache cache = System.Web.HttpContext.Current.Cache;
            string[] keywords = cache.Get(cacheName) as string[];
            if (keywords == null)
            {
                lock (lockObject)
                {
                    if (keywords == null)
                    {
                        DataSet ds = new DataSet();
                        try
                        {
                            ds.ReadXml(url);
                            DataTable dt = ds.Tables["KeyWord"];
                            keywords = new string[dt.Rows.Count];
                            for (int i = 0; i < dt.Rows.Count; i++)
                            {
                                keywords[i] = dt.Rows[i]["KeyWord_Text"].ToString();
                            }
                            cache.Insert(cacheName, keywords, null, DateTime.Now.AddHours(24), System.TimeSpan.Zero);
                        }
                        catch
                        { }
                    }

                }

            }
            return keywords;
        }

        /// <summary>
        /// 议价，委托留言关键字
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static bool FilterMessageWord(string value)
        {
            bool hasKeyword = false;
            string[] keywords = GetKeywords();
            if (keywords != null)
            {
                foreach (string s in keywords)
                {
                    if (value.Contains(s))
                    {
                        hasKeyword = true;
                        break;
                    }
                }
            }
            return hasKeyword;
        }

        /// <summary>
        /// 议价，委托留言关键字
        /// </summary>
        /// <param name="values"></param>
        /// <returns></returns>
        public static bool FilterMessageWord(IEnumerable<string> values)
        {
            bool hasKeyword = false;
            string[] keywords = GetKeywords();
            if (keywords != null)
            {
                foreach (var value in values)
                {
                    foreach (string s in keywords)
                    {
                        if (value.Contains(s))
                        {
                            hasKeyword = true;
                            break;
                        }
                    }
                    if (hasKeyword)
                    {
                        break;
                    }
                }
            }
            return hasKeyword;
        }
    }
}
