using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Web;
using Gscoy.Common;

namespace Gscoy.Common.Web
{
    /// <summary>
    /// page基类
    /// </summary>
    public class PubPageBase : BasePage
    {
        #region 私有字段
        /// <summary>
        /// 过滤方式 socket接口
        /// </summary>
        private const string FILTERBYSOCKET = "1";

        /// <summary>
        /// 过滤方式 xml
        /// </summary>
        private const string FILTERBYXML = "2";

        /// <summary>
        /// 默认编码类型 可考虑改为从配置读默认编码
        /// </summary>
        private static readonly System.Text.Encoding defaultEncoding = System.Text.Encoding.GetEncoding("gbk");

        /// <summary>
        /// 过滤关键字的类型
        /// </summary>
        private static readonly string filterlKeywordsMode = ConfigHelper.GetConfig("FilterlKeywordsType", FILTERBYSOCKET);

        /// <summary>
        /// 过滤关键字xml文件路径
        /// </summary>
        private static string filterlKeywordsXmlUrl = ConfigHelper.GetConfig("KeywordsPath");
        #endregion

        #region 私有方法
        /// <summary>
        /// 获取get和post的参数
        /// </summary>
        /// <param name="name">参数名</param>
        /// <param name="encoding">编码</param>
        /// <returns>值</returns>
        private static string GetOrPost(string name, bool isNeedDecode = false, System.Text.Encoding encoding = null)
        {
            if (encoding == null)
            {
                encoding = defaultEncoding;
            }
            System.Web.HttpRequest Request = System.Web.HttpContext.Current.Request;
            string strVal = !string.IsNullOrEmpty(Request.QueryString[name]) ? Request.QueryString[name] : Request.Form[name];
            if (!string.IsNullOrWhiteSpace(strVal))
            {
                strVal = isNeedDecode ? System.Web.HttpUtility.UrlDecode(strVal, encoding) : strVal;
            }
            else
            {
                strVal = string.Empty;
            }
            return strVal;
        }

        /// <summary>
        /// 通过xml文件过滤关键字
        /// </summary>
        /// <param name="value">待过滤的值</param>
        /// <param name="keywords">包含的关键字</param>
        /// <returns>过滤结果 是否包含关键字</returns>
        private bool FilterKeywordsByXml(string value, ref string keywords)
        {
            bool hasKeywords = false;
            string[] templates = null;
            string cacheName = "keywords_bad_all";
            // 获取keywords列表
            if (this.Cache.Get(cacheName) != null)
            {
                templates = this.Cache.Get(cacheName) as string[];
            }
            if (templates == null)
            {
                try
                {
                    DataSet ds = new DataSet();
                    ds.ReadXml(filterlKeywordsXmlUrl);
                    DataTable dt = ds.Tables["keyword"];
                    templates = new string[dt.Rows.Count];
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        templates[i] = dt.Rows[i]["Keyword_Text"].ToString();
                    }
                    this.Cache.Insert(cacheName, templates, null, DateTime.Now.AddHours(24), System.TimeSpan.Zero);
                }
                catch
                {
                }
            }

            foreach (string key in templates)
            {
                if (!string.IsNullOrEmpty(key))
                {
                    hasKeywords = value.Contains(key);
                    if (hasKeywords)
                    {
                        keywords = key;
                        break;
                    }
                }

            }
            return hasKeywords;
        }

        ///// <summary>
        ///// 通过xml文件过滤关键字
        ///// </summary>
        ///// <param name="value">待过滤的值</param>
        ///// <param name="keywords">包含的关键字</param>
        ///// <returns>过滤结果 是否包含关键字</returns>
        //private bool FilterKeywordsByXmlAPI(string value, ref string keywords)
        //{
        //    bool hasKeywords = false;
        //    string[] templates = null;
        //    string cacheName = "keywords_bad_all";
        //    // 获取keywords列表
        //    if (System.Web.HttpContext.Current.Cache.Get(cacheName) != null)
        //    {
        //        templates = System.Web.HttpContext.Current.Cache.Get(cacheName) as string[];
        //    }
        //    if (templates == null)
        //    {
        //        try
        //        {
        //            DataSet ds = new DataSet();
        //            ds.ReadXml(filterlKeywordsXmlUrl);
        //            DataTable dt = ds.Tables["keyword"];
        //            templates = new string[dt.Rows.Count];
        //            for (int i = 0; i < dt.Rows.Count; i++)
        //            {
        //                templates[i] = dt.Rows[i]["Keyword_Text"].ToString();
        //            }
        //            System.Web.HttpContext.Current.Cache.Insert(cacheName, templates, null, DateTime.Now.AddHours(24), System.TimeSpan.Zero);
        //        }
        //        catch
        //        {
        //        }
        //    }

        //    foreach (string key in templates)
        //    {
        //        if (!string.IsNullOrEmpty(key))
        //        {
        //            hasKeywords = value.Contains(key);
        //            if (hasKeywords)
        //            {
        //                keywords = key;
        //                break;
        //            }
        //        }

        //    }
        //    return hasKeywords;
        //}
        /// <summary>
        /// 统一过滤关键字
        /// </summary>
        //private void DoKeywordFilter(string cityName = "")
        //{
        //    Type type = this.GetType();
        //    FilterKeywordAttribute attribute = null;
        //    if (AttributeCache.FilterKeywordAttributeCache.ContainsKey(type))
        //    {
        //        attribute = AttributeCache.FilterKeywordAttributeCache[type];
        //    }
        //    else
        //    {
        //        object[] os = type.GetCustomAttributes(typeof(FilterKeywordAttribute), true);
        //        if (os != null && os.Length > 0)
        //        {
        //            attribute = os[0] as FilterKeywordAttribute;
        //        }
        //        else
        //        {
        //            // 默认为不过滤
        //            attribute = new FilterKeywordAttribute(FilterKeywordAttribute.FilterKeywordType.None);
        //            AttributeCache.FilterKeywordAttributeCache.Add(type, attribute);
        //        }
        //    }
        //    List<string> keys = new List<string>();
        //    switch (attribute.FilterType)
        //    {
        //        case FilterKeywordAttribute.FilterKeywordType.None:
        //            // 不做处理
        //            break;
        //        case FilterKeywordAttribute.FilterKeywordType.Part:
        //            keys.AddRange(attribute.Names.Split(','));
        //            break;
        //        case FilterKeywordAttribute.FilterKeywordType.AllBut:
        //            keys.AddRange(Request.QueryString.AllKeys);
        //            keys.AddRange(Request.Form.AllKeys);
        //            keys.Except(attribute.Names.Split(','));
        //            break;
        //        case FilterKeywordAttribute.FilterKeywordType.All:
        //            keys.AddRange(Request.QueryString.AllKeys);
        //            keys.AddRange(Request.Form.AllKeys);
        //            break;
        //        default:
        //            break;
        //    }
        //    if (keys.Count > 0)
        //    {
        //        string keywords = string.Empty;
        //        if (ContainsKeyword(keys, ref keywords, cityName))
        //        {
        //            // 统一提示含关键字
        //            OutPut("提交的数据中含有非法信息！");
        //            Response.End();
        //            return;
        //        }
        //    }
        //}
        #endregion

        #region 公共方法
        /// <summary>
        /// 获取配置节点值
        /// </summary>
        /// <param name="key">key</param>
        /// <param name="defaultValue">获取失败后的默认值 默认为空字符串</param>
        /// <returns>节点值</returns>
        public string GetConfig(string key, string defaultValue = "")
        {
            return ConfigHelper.GetConfig(key, defaultValue);
        }

        /// <summary>
        /// 获取页面标题前缀
        /// </summary>
        /// <returns></returns>
        protected string GetTitlePrefix()
        {
            bool isOnline = GetConfig("isonline") == "1";

            string dbConfig = GetConfig("NewDbConfig");
            if (!isOnline)
            {
                if (!string.IsNullOrEmpty(dbConfig) && dbConfig.ToLower().Contains("test"))
                {
                    return "测试环境-";
                }
                else
                {
                    return "仿真环境-";
                }
            }
            else
            {
                return string.Empty;
            }
        }

        /// <summary>
        /// 获取客户端IP
        /// </summary>
        /// <returns>IP</returns>
        public string GetClientIP()
        {
            return IPHelper.GetClientIP();
        }

        /// <summary>
        /// 获取客户端IP
        /// </summary>
        /// <returns>IP</returns>
        public string[] GetAllClientIP()
        {
            return IPHelper.GetAllClientIP();
        }

        /// <summary>
        /// 获取页面参数值(get or post)
        /// </summary>
        /// <typeparam name="T">返回类型</typeparam>
        /// <param name="name">参数名</param>
        /// <param name="defaultValue">获取失败时的默认值</param>
        /// <returns>参数值</returns>
        public T GetRequest<T>(string name, T defaultValue = default(T)) where T : struct
        {
            string strVal = GetOrPost(name);
            return string.IsNullOrWhiteSpace(strVal) ? defaultValue : strVal.Parse<T>(defaultValue);
        }

        /// <summary>
        /// 获取页面参数值并转换成集合(get or post)
        /// </summary>
        /// <typeparam name="T">返回类型</typeparam>
        /// <param name="name">参数名</param>
        /// <param name="separator">分隔符</param>
        /// <returns>参数值集合</returns>
        public List<T> GetRequestList<T>(string name, string[] separator = null) where T : struct
        {
            List<T> list = null;
            string strVal = GetOrPost(name);
            if (!string.IsNullOrEmpty(strVal))
            {
                list = strVal.ParseToList<T>(separator);
            }
            //如果list为null，返回一个长度为0的list，便于后续处理
            if (list == null)
            {
                list = new List<T>();
            }
            return list;
        }

        /// <summary>
        /// 获取页面参数值(get or post)
        /// </summary>
        /// <typeparam name="T">返回类型</typeparam>
        /// <param name="name">参数名</param>
        /// <param name="defaultValue">获取失败时的默认值</param>
        /// <returns>参数值</returns>
        public T? GetRequestNullable<T>(string name) where T : struct
        {
            string strVal = GetOrPost(name);
            return string.IsNullOrWhiteSpace(strVal) ? null : strVal.ParseToNullable<T>();
        }

        /// <summary>
        /// 获取页面参数值
        /// </summary>
        /// <param name="name">参数名</param>
        /// <param name="encoding">编码，默认为gbk编码</param>
        /// <param name="needAntiXss">是否需要做xss过滤 默认为是</param>
        /// <returns>参数值</returns>
        public string GetRequestString(string name, System.Text.Encoding encoding = null, AntiXSSMode antiXssMode = AntiXSSMode.Default)
        {
            string str = GetOrPost(name, encoding: encoding);
            if (!string.IsNullOrWhiteSpace(str))
            {
                str = str.Trim().GetSafeString(antiXssMode);
            }
            return str;
        }

        /// <summary>
        /// 获取页面参数值
        /// </summary>
        /// <param name="name">参数名</param>
        /// <param name="encoding">编码，默认为gbk编码</param>
        /// <param name="needAntiXss">是否需要做xss过滤 默认为是</param>
        /// <returns>参数值</returns>
        public string GetRequestString(string name, ref StringBuilder sb, System.Text.Encoding encoding = null, AntiXSSMode antiXssMode = AntiXSSMode.Default)
        {
            string str = GetOrPost(name, encoding: encoding);
            if (!string.IsNullOrWhiteSpace(str))
            {
                str = str.Trim().GetSafeString(antiXssMode);
            }
            sb.Append(str);
            return str;
        }
        /// <summary>
        /// 获取int型页面参数
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public int? GetRequestInt(string key)
        {
            return GetRequestNullable<int>(key);
        }
        /// <summary>
        /// 获取long型页面参数
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public long? GetRequestLong(string key)
        {
            return GetRequestNullable<long>(key);
        }

        //public virtual bool ContainsKeyword(IEnumerable<string> values, ref string keywords)
        //{
        //    return ContainsKeyword(values, ref keywords, string.Empty);
        //}
        /// <summary>
        /// 检查是否含过滤关键字
        /// </summary>
        /// <param name="values">要过滤的值列表</param>
        /// <param name="keywords">包含的关键字</param>
        /// <returns>是否包含</returns>
        //public bool ContainsKeyword(IEnumerable<string> values, ref string keywords, string cityName)
        //{
        //    if (filterlKeywordsMode == FILTERBYSOCKET)
        //    {
        //        return KeywordsHelper.Instance.CheckKeywords(values, ref keywords, cityName);
        //    }
        //    else
        //    {
        //        StringBuilder sb = new StringBuilder();
        //        foreach (var item in values)
        //        {
        //            sb.AppendFormat("{0}~", item);
        //        }
        //        return FilterKeywordsByXml(sb.ToString().Trim('~'), ref keywords);
        //    }
        //}

        //public virtual bool ContainsKeyword(string value, ref string keywords)
        //{
        //    return ContainsKeyword(value, ref keywords, string.Empty);
        //}

        /// <summary>
        /// 检查是否含过滤关键字
        /// </summary>
        /// <param name="value">要过滤的值</param>
        /// <param name="keywords">包含的关键字</param>
        /// <returns>是否包含</returns>
        //public bool ContainsKeyword(string value, ref string keywords, string cityName)
        //{
        //    if (filterlKeywordsMode == FILTERBYSOCKET)
        //    {
        //        return KeywordsHelper.Instance.CheckKeywords(value, ref keywords, cityName);
        //    }
        //    else
        //    {
        //        return FilterKeywordsByXml(value, ref keywords);
        //    }
        //}


        /// <summary>
        /// 检查是否包含议价，委托留言关键字
        /// </summary>
        /// <param name="values">要过滤的值列表</param>
        /// <returns>是否包含</returns>
        public bool ContainMessageKeyword(IEnumerable<string> values)
        {
            return MessageFilterHelper.FilterMessageWord(values);
        }

        /// <summary>
        /// 检查是否包含议价，委托留言关键字
        /// </summary>
        /// <param name="value">要过滤的值</param>
        /// <returns>是否包含</returns>
        public bool ContainMessageKeyword(string value)
        {
            return MessageFilterHelper.FilterMessageWord(value);
        }

        #endregion

        /// <summary>
        /// 设置无页面缓存
        /// </summary>
        protected void SetPageCacheNone()
        {
            Response.Buffer = true;
            Response.ExpiresAbsolute = DateTime.Now.AddHours(-1);
            Response.Expires = 0;
            Response.AppendHeader("Cache-Control", "no-cache, no-store, must-revalidate, max-age=-1");
            Response.Cache.SetCacheability(HttpCacheability.NoCache);
            Response.AppendHeader("Pragma", "No-Cache");
        }

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);
            //this.DoKeywordFilter();
        }

        /// <summary>
        /// 输出信息 如果需要不同的格式 可覆盖本方法
        /// </summary>
        /// <param name="str">要输出的信息</param>
        protected virtual void OutPut(string str)
        {
            JsHelper.AlertAndGoBack(str);
        }

        /// <summary>
        /// 判断是否为整数
        /// </summary>
        public static bool isInt(string itemValue)
        {
            return (IsRegEx("^(-|\\+)?(\\d)*$", itemValue));
        }

        /// <summary>
        /// 判断是否为正整数
        /// </summary>
        /// <param name="itemValue"></param>
        /// <returns></returns>
        public static bool isUnSignedInt(string itemValue)
        {
            return (IsRegEx("^\\d+$", itemValue));
        }

        /// <summary>
        /// 执行正则
        /// </summary>
        /// <param name="regExValue"></param>
        /// <param name="itemValue"></param>
        /// <returns></returns>
        public static bool IsRegEx(string regExValue, string itemValue)
        {

            try
            {
                System.Text.RegularExpressions.Regex regex = new System.Text.RegularExpressions.Regex(regExValue);
                if (regex.IsMatch(itemValue)) return true;
                else return false;
            }
            catch (Exception)
            {
                return false;
            }
            finally
            {
            }
        }

        /// <summary>
        /// 将日期转化为格式化的字符串
        /// </summary>
        /// <param name="dTime"></param>
        /// <param name="format"></param>
        /// <returns></returns>
        public static string ToDateTime(object dTime, string format)
        {
            return dTime.ToString().Parse<DateTime>().ToString(format);
        }

        public static string GetHttpContextUrl()
        {
            return System.Web.HttpContext.Current.Request.RawUrl;

        }
    }
}
