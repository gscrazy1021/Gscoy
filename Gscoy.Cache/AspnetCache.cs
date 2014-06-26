using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Caching;
namespace Gscoy.Common
{
    /// <summary>
    /// 使用Asp.Net缓存
    /// </summary>
    /// <typeparam name="TValue">数据类型</typeparam>
    public class AspnetCache:ICache<string>
    {
        static object lockObj = new object();
        static System.Web.Caching.Cache cache = System.Web.HttpRuntime.Cache;//这样写可以兼容应用程序
        private static AspnetCache instance = null;

        public static AspnetCache Instance
        {
            get
            {
                if (instance == null)
                {
                    lock (lockObj)
                    {
                        if (instance == null)
                        {
                            instance = new AspnetCache();
                        }
                    }
                }
                return instance;
            }
        }

        private AspnetCache() 
        {
        }
        
        /// <summary>
        /// 将对象加入到缓存里，默认有效期固定为1小时
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public bool Set(string key, object value)
        {
            cache.Insert(key, value, null,DateTime.Now.AddHours(1), TimeSpan.Zero);
            return true;
        }
        /// <summary>
        /// 将对象加入到缓存里
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <param name="expireTime">固定时间有效期</param>
        /// <returns></returns>
        public bool Set(string key, object value, DateTime expireTime)
        {
            cache.Insert(key, value, null, expireTime, TimeSpan.Zero);
            return true;
        }

        /// <summary>
        /// 将对象加入到缓存里
        /// </summary>
        /// <param name="key">键</param>
        /// <param name="value">值</param>
        /// <param name="dependencies">缓存依赖</param>
        /// <param name="expireTime">固定时间有效期</param>
        /// <returns></returns>
        public bool Set(string key, object value, CacheDependency dependencies, DateTime expireTime)
        {
            cache.Insert(key, value, dependencies, expireTime, TimeSpan.Zero);
            return true;
        }

        /// <summary>
        /// 将对象加入到缓存里
        /// </summary>
        /// <param name="key">键</param>
        /// <param name="value">值</param>
        /// <param name="fileName">文件缓存依赖的文件所在路径</param>
        /// <param name="expireTime">固定时间有效期</param>
        /// <returns></returns>
        public bool Set(string key, object value, string fileName, DateTime expireTime)
        {
            System.Web.Caching.CacheDependency dependencies = new System.Web.Caching.CacheDependency(fileName);
            cache.Insert(key, value, dependencies, expireTime, TimeSpan.Zero);
            return true;
        }

        /// <summary>
        /// 将对象加入到缓存里
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <param name="slideTime">滑动有效期</param>
        /// <returns></returns>
        public bool Set(string key, object value, TimeSpan slideTime)
        {
            cache.Insert(key, value, null, DateTime.MaxValue,slideTime);
            return true;
        }
        /// <summary>
        /// 获取对象
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public TValue Get<TValue>(string key)
        {
            var obj = cache.Get(key);
            if (obj != null)
            {
                return (TValue)obj;
            }
            return default(TValue);
        }
        /// <summary>
        /// 删除键
        /// </summary>
        /// <param name="key"></param>
        /// <returns>键存在，返回true,键不存在,返回false</returns>
        public bool Delete(string key)
        {
            return cache.Remove(key) != null;
        }
        public bool KeyExists(string key)
        {
            return cache.Get(key) != null;
        }

        public Dictionary<string, TValue> GetMultiple<TValue>(IEnumerable<string> keys)
        {
            Dictionary<string, TValue> dic = new Dictionary<string, TValue>();
            foreach (var key in keys)
            {
                var obj = Get<TValue>(key);
                dic.Add(key,obj);
            }
            return dic;
        }
    }
}
