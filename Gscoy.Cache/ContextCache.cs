using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;

namespace Gscoy.Common
{
    /// <summary>
    /// 使用CallContext纯程上下文的缓存
    /// </summary>
    /// <typeparam name="TValue">数据类型</typeparam>
    public class ContextCache:ICache<string>
    {

        public bool Set(string key, object value)
        {
            try
            {
                CallContext.LogicalSetData(key, value);
                return true;
            }
            catch { return false; }
        }
        [Obsolete("不支持")]
        public bool Set(string key, object value, DateTime expireTime)
        {
            throw new NotImplementedException();
        }

        [Obsolete("不支持")]
        public bool Set(string key, object value, TimeSpan slideTime)
        {
            throw new NotImplementedException();
        }

        public TValue Get<TValue>(string key)
        {
            var obj = CallContext.LogicalGetData(key);
            if (obj == null)
            {
                return default(TValue);
            }
            return (TValue)obj;
        }

        public bool KeyExists(string key)
        {
            return CallContext.LogicalGetData(key) != null;
        }

        public bool Delete(string key)
        {
            try
            {
                CallContext.LogicalSetData(key, null);
                return true;
            }
            catch { return false; }
        }

        public Dictionary<string, TValue> GetMultiple<TValue>(IEnumerable<string> keys)
        {
            Dictionary<string, TValue> dic = new Dictionary<string, TValue>();
            foreach (var key in keys)
            {
                var obj = CallContext.LogicalGetData(key);
                if (obj == null)
                {
                    dic.Add(key, default(TValue));
                }
                else
                {
                    dic.Add(key, (TValue)obj);
                }
            }
            return dic;
        }
    }
}
