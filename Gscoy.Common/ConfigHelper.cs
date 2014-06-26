using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

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
            string val = System.Configuration.ConfigurationManager.AppSettings[key];
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
        public static string GetConnectionString(string key, string defaultValue="")
        {
            string val = System.Configuration.ConfigurationManager.ConnectionStrings[key].ConnectionString;
            return string.IsNullOrEmpty(val) ? defaultValue : val;
        }
    }
}
