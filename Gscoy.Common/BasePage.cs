using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gscoy.Common
{
    /// <summary>
    /// 页面基类
    /// </summary>
    public class BasePage : System.Web.UI.Page
    {
        #region 私有方法及字段
        /// <summary>
        /// 默认编码类型 可考虑改为从配置读默认编码
        /// </summary>
        private static readonly System.Text.Encoding defaultEncoding = System.Text.Encoding.GetEncoding("gbk");

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
        #endregion

        #region 公有方法
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

        /// <summary>
        /// 获取客户端ip
        /// </summary>
        /// <returns></returns>
        public string GetClientIP()
        {
            return IPHelper.GetClientIP();
        }
        /// <summary>
        /// 获取客户端ip
        /// </summary>
        /// <returns></returns>
        public string[] GetAllClientIP()
        {
            return IPHelper.GetAllClientIP();
        }
        #endregion

        #region 事件
        protected void Page_Error(object sender, System.EventArgs e)
        {
            try
            {
                Exception ex = Server.GetLastError().GetBaseException();
                string url = Request.Url.AbsoluteUri;
                string refurl = Request.UrlReferrer != null ? "<br>来源: " + Request.UrlReferrer.AbsoluteUri : string.Empty;
                string msg = "<br>ex.Message异常信息:" + ex.Message + "<br>错误方法：" + ex.TargetSite;
                if (Request.Form != null)
                {
                    msg = msg + "接收参数为:" + Server.UrlDecode(Request.Form.ToString());
                }

                //Utility.LogNet.Error(url + msg, ex);

                //// 发邮件
                //string sendMail = Utility.ConfigHelper.GetConfig("SendMail");
                //int hashCode = (url + msg).GetHashCode();
                //if (sendMail == "1" && !string.IsNullOrEmpty(msg))
                //{
                //    // 现在我们将查看某错误是否在表中
                //    if (!ErrorDic.ContainsKey(hashCode))
                //    {
                //        ErrorDic.Add(hashCode, msg);

                //        // 发送邮件 取消
                //        //Utility.MailHelper.SendMail("程序出错", string.Format("{0} 城市:{1} 错误地址:{2}", refurl, this.CityName, url + msg));
                //    }
                //}
            }
            catch
            {
            }
            finally
            {
                //if (this.IsOnline)
                //{
                //    Server.ClearError();
                //    Server.Transfer(ERRORPAGE);
                //}
            }
        }
        #endregion
    }
}
