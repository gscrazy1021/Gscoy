using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gscoy.Common
{
    public interface ICache<TKey>
    {
        /// <summary>
        /// 缓存数据
        /// </summary>
        /// <param name="key">键</param>
        /// <param name="value">值</param>
        bool Set(TKey key,object value);

        /// <summary>
        /// 缓存数据
        /// </summary>
        /// <param name="name">键</param>
        /// <param name="obj">值</param>
        /// <param name="expireTime">过期时间（绝对值）</param>
        /// <returns></returns>
        bool Set(TKey key, object value, DateTime expireTime);

        /// <summary>
        /// 缓存数据
        /// </summary>
        /// <param name="name">键</param>
        /// <param name="obj">值</param>
        /// <param name="slideTime">过期时间（相对时间（滑动过期，从最后一次访问时计算此时间））</param>
        /// <returns></returns>
        bool Set(TKey key, object value, TimeSpan slideTime);

        /// <summary>
        /// 从缓存中获取数据
        /// </summary>
        /// <param name="key">键</param>
        /// <returns></returns>
        TValue Get<TValue>(TKey key);

        /// <summary>
        /// 判断某个键在缓存中是否存在
        /// </summary>
        /// <param name="key">键</param>
        /// <returns>是否存在</returns>
        bool KeyExists(TKey key);

        /// <summary>
        /// 删除数据
        /// </summary>
        /// <param name="key">键</param>
        /// <returns>是否成功删除</returns>
        bool Delete(TKey key);

        /// <summary>
        /// 获取多个缓存值
        /// </summary>
        /// <typeparam name="TValue"></typeparam>
        /// <param name="keys"></param>
        /// <returns></returns>
        Dictionary<TKey, TValue> GetMultiple<TValue>(IEnumerable<TKey> keys);
    }
}
