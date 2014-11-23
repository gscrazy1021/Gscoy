using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;
using System.Web;
using System.Collections;

namespace Gscoy.Common
{
    /// <summary>
    /// 配置帮助类
    /// </summary>
    public class ConfigHelper
    {
        /// <summary>
        /// 获取配置节点值
        /// </summary>
        /// <param name="key">key</param>
        /// <param name="defaultValue">获取失败后的默认值 默认为空字符串</param>
        /// <returns>节点值</returns>
        public static string GetConfig(string key, string defaultValue = "")
        {
            string cacheKey = string.Format("config_{0}", key);
            object obj = CacheHelper.GetCache(cacheKey);
            if (obj != null) return obj.ToString();
            string val = ConfigurationManager.AppSettings[key];
            CacheHelper.SetCache(cacheKey, val);
            return string.IsNullOrEmpty(val) ? defaultValue : val;
        }

        /// <summary>
        /// 获取配置节点值
        /// </summary>
        /// <param name="key">key</param>
        /// <param name="defaultValue">获取失败后的默认值 默认为空字符串</param>
        /// <returns>节点值</returns>
        public static T GetConfig<T>(string key, T defaultValue = default(T)) where T : IConvertible
        {
            string val = System.Configuration.ConfigurationManager.AppSettings[key];
            var result = false;
            var value = val.TryParse<T>(out result);
            return result ? value : defaultValue;
        }

        /// <summary>
        /// 获取配置连接串
        /// </summary>
        /// <param name="key">key</param>
        /// <param name="defaultValue">获取失败后的默认值 默认为空字符串</param>
        /// <returns>节点值</returns>
        public static string GetConnectionString(string key, string defaultValue = "")
        {
            string cacheKey = string.Format("config_{0}", key);
            object obj = CacheHelper.GetCache(cacheKey);
            if (obj != null) return obj.ToString();
            string val = System.Configuration.ConfigurationManager.ConnectionStrings[key].ConnectionString;
            CacheHelper.SetCache(cacheKey, val);
            return string.IsNullOrEmpty(val) ? defaultValue : val;
        }
    }

    class CacheHelper
    {
        /// <summary>
        /// 获取数据缓存
        /// </summary>
        /// <param name="CacheKey">键</param>
        public static object GetCache(string CacheKey)
        {
            System.Web.Caching.Cache objCache = HttpRuntime.Cache;
            return objCache[CacheKey];
        }

        /// <summary>
        /// 设置数据缓存
        /// </summary>
        public static void SetCache(string CacheKey, object objObject)
        {
            System.Web.Caching.Cache objCache = HttpRuntime.Cache;
            if (objObject != null)
                objCache.Insert(CacheKey, objObject);
        }

        /// <summary>
        /// 设置数据缓存
        /// </summary>
        public static void SetCache(string CacheKey, object objObject, TimeSpan Timeout)
        {
            System.Web.Caching.Cache objCache = HttpRuntime.Cache;
            objCache.Insert(CacheKey, objObject, null, DateTime.MaxValue, Timeout, System.Web.Caching.CacheItemPriority.NotRemovable, null);
        }

        /// <summary>
        /// 设置数据缓存
        /// </summary>
        public static void SetCache(string CacheKey, object objObject, DateTime absoluteExpiration, TimeSpan slidingExpiration)
        {
            System.Web.Caching.Cache objCache = HttpRuntime.Cache;
            objCache.Insert(CacheKey, objObject, null, absoluteExpiration, slidingExpiration);
        }

        /// <summary>
        /// 移除指定数据缓存
        /// </summary>
        public static void RemoveAllCache(string CacheKey)
        {
            System.Web.Caching.Cache _cache = HttpRuntime.Cache;
            _cache.Remove(CacheKey);
        }

        /// <summary>
        /// 移除全部缓存
        /// </summary>
        public static void RemoveAllCache()
        {
            System.Web.Caching.Cache _cache = HttpRuntime.Cache;
            IDictionaryEnumerator CacheEnum = _cache.GetEnumerator();
            while (CacheEnum.MoveNext())
            {
                _cache.Remove(CacheEnum.Key.ToString());
            }
        }
    }

}
