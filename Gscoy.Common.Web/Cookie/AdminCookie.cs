using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gscoy.Common.Web
{
    //using SouFun.ESF.SFB.Utility.Extension;

    public class AdminCookie : CookieBase
    {

        /// <summary>
        /// 是否需要修改密码 0 不需要修改  1密码过期 2第一次登录
        /// </summary>
        public int NeedModifyPassword { get; set; }

        public AdminCookie(int userId, string userName, string pwd, int needModifyPassword)
        {
            this.UserId = userId;
            this.UserName = userName;
            this.Password = pwd;
            this.NeedModifyPassword = needModifyPassword;
            this.Validation = this.CookieEncrypt(1);
            this.IsValid = true;
        }

        public AdminCookie()
        {
            System.Web.HttpRequest request = System.Web.HttpContext.Current.Request;
            System.Web.HttpServerUtility server = System.Web.HttpContext.Current.Server;
            if (request.Cookies["agent_kf"] != null && request.Cookies["agent_kf"].HasKeys && !string.IsNullOrEmpty(request.Cookies["agent_kf"]["id"]) && !string.IsNullOrEmpty(request.Cookies["agent_kf"]["u"]) && !string.IsNullOrEmpty(request.Cookies["agent_kf"]["v"]))
            {
                UserId = request.Cookies["agent_kf"]["id"].Parse<int>();
                UserName = server.UrlDecode(request.Cookies["agent_kf"]["u"]);
                Password = request.Cookies["agent_kf"]["p"];
                Validation = request.Cookies["agent_kf"]["v"];
                if (Validation == CookieEncrypt(ENCRYPTTYPE_AGENT))
                {
                    IsValid = true;
                    if (!string.IsNullOrWhiteSpace(request.Cookies["agent_kf"]["pp"]))
                    {
                        //string tempStr = Utility.Security.Decrypt(request.Cookies["agent_kf"]["pp"], "souxbfun");
                        //NeedModifyPassword = int.Parse(tempStr[tempStr.Length - 1].ToString());
                    }
                }
                else
                {
                    this.UserId = 0;
                    this.UserName = string.Empty;
                }
            }
        }

        #region 公用方法

        /// <summary>
        /// 清除cookie
        /// </summary>
        public static void ClearCookie()
        {
            System.Web.HttpResponse response = System.Web.HttpContext.Current.Response;
            DateTime expir = DateTime.Now.AddDays(-1);
            response.Cookies["agent_kf"].Domain = "soufun.com";
            response.Cookies["agent_kf"].Value = string.Empty;
            response.Cookies["agent_kf"].Expires = expir;

        }

        /// <summary>
        /// 加密cookie 
        /// <param name="encryptType">类型 待扩展</param>
        /// </summary>
        public override string CookieEncrypt(int encryptType)
        {
            int length = UserName.Length;
            string strEncrypt = UserName.Substring(0, length - 1) + "仝" + UserId + UserName.Substring(length - 1);
            return Security.Encrypt(strEncrypt, ENCRYPTKEY_AGENT);
        }

        /// <summary>
        /// 写cookie
        /// </summary>
        public override void SetCookie()
        {
            if (!IsValid) return;
            System.Web.HttpRequest request = System.Web.HttpContext.Current.Request;
            System.Web.HttpResponse response = System.Web.HttpContext.Current.Response;
            System.Web.HttpServerUtility server = System.Web.HttpContext.Current.Server;
            string strValidation = CookieEncrypt(ENCRYPTTYPE_AGENT);//校验码 供经纪人cookie使用
            string cookieDomain = DOMAIN;
            DateTime cookie_date = System.DateTime.Now.AddHours(12);

            response.Cookies["agent_kf"].Domain = cookieDomain;
            response.Cookies["agent_kf"]["u"] = server.UrlEncode(this.UserName);

            response.Cookies["agent_kf"]["p"] = this.Password;

            response.Cookies["agent_kf"]["id"] = this.UserId.ToString();

            response.Cookies["agent_kf"]["v"] = strValidation;
            //存储是否要修改密码
            response.Cookies["agent_kf"]["pp"] = Utility.Security.Encrypt(new Random().Next(1000000, 9999999).ToString() + this.NeedModifyPassword.ToString(), "souxbfun");

            response.Cookies["agent_kf"].Expires = DateTime.Now.AddHours(1);
        }
        #endregion
    }
}
