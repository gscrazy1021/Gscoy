using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gscoy.Common.Web
{
    public abstract class CookieBase
    {
        #region 常量
        /// <summary>
        /// 加密类型 经纪人
        /// </summary>
        protected const int ENCRYPTTYPE_AGENT = 1;

        /// <summary>
        /// 加密类型 通行证
        /// </summary>
        protected const int ENCRYPTTYPE_PASSPORT = 2;

        /// <summary>
        /// 经纪人加密密钥
        /// </summary>
        protected const string ENCRYPTKEY_AGENT = "uensocty";

        /// <summary>
        /// 通行证加密密钥
        /// </summary>
        protected static string ENTRYPTKEY_PASSPORT = ConfigHelper.GetConfig("PassportEntryKey", "w8f3k9c2");

        public const string DOMAIN = "soufun.com";

        #endregion
        
        /// <summary>
        /// cookie是否有效
        /// </summary>
        public bool IsValid { get; protected set; }

        /// <summary>
        /// 用户ID
        /// </summary>
        public long UserId { get; set; }

        /// <summary>
        /// 用户名
        /// </summary>
        public string UserName { get; set; }

        /// <summary>
        /// 密码
        /// </summary>
        public string Password { get; set; }

        /// <summary>
        /// 验证码
        /// </summary>
        public string Validation { get; set; }

        /// <summary>
        /// cookie加密
        /// </summary>
        public virtual string CookieEncrypt(int type)
        {
            return string.Empty;
        }

        /// <summary>
        /// 写cookie
        /// </summary>
        public virtual void SetCookie()
        {
 
        }

        public CookieBase()
        {

        }
    }
}
