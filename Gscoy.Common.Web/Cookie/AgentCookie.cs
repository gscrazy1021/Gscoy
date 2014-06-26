using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gscoy.Common.Web
{
    using SouFun.ESF.SFB.CommonData.Enums;
    using SouFun.ESF.SFB.Utility.Extension;
    using System.Web.Security;

    public class AgentCookie : CookieBase
    {

        #region 属性
        /// <summary>
        /// 经纪人邮箱
        /// </summary>
        public string AgentEmail { get; set; }

        /// <summary>
        /// 验证码
        /// </summary>
        public string AgentValidation { get; set; }

        /// <summary>
        /// 通行证验证码
        /// </summary>
        public string PassportValidation { get; set; }

        /// <summary>
        /// 通行证用户类型
        /// </summary>
        public string PassportUserType { get; set; }

        ///// <summary>
        ///// PassportIsvalid
        ///// </summary>
        //public string PassportIsvalid { get; set; }

        /// <summary>
        /// 用户类型
        /// </summary>
        public string UserType { get; set; }
        #endregion

        #region 构造函数

        public AgentCookie(long userId, string userName, string OriginalPWD, string agentEmail, UserType userType, AgentType agentType)
        {
            this.UserId = userId;
            this.UserName = userName;
            this.Password = FormsAuthentication.HashPasswordForStoringInConfigFile(OriginalPWD, "md5");
            this.AgentValidation = CookieEncrypt(ENCRYPTTYPE_AGENT);
            if (userType == CommonData.Enums.UserType.Agent)
            {
                if (agentType == AgentType.LeaseOnly)
                {
                    this.UserType = "4";
                }
                else
                {
                    this.UserType = "1";
                }
            }
            else if (userType == CommonData.Enums.UserType.Enterprise)
            {
                this.UserType = "3";
            }
            else if (userType == CommonData.Enums.UserType.Admin)
            {
                this.UserType = "2";
            }
            else if (userType == CommonData.Enums.UserType.Personal)
            {
                this.UserType = "5";
            }
            this.AgentEmail = agentEmail;
            this.IsValid = true;
        }

        public AgentCookie(long userId, string userName, string CiphertextPWD, string agentEmail, UserType userType, AgentType agentType, int pMethodVersion)
        {
            this.UserId = userId;
            this.UserName = userName;
            this.Password = CiphertextPWD;
            this.AgentValidation = CookieEncrypt(ENCRYPTTYPE_AGENT);
            if (userType == CommonData.Enums.UserType.Agent)
            {
                if (agentType == AgentType.LeaseOnly)
                {
                    this.UserType = "4";
                }
                else
                {
                    this.UserType = "1";
                }
            }
            else if (userType == CommonData.Enums.UserType.Enterprise)
            {
                this.UserType = "3";
            }
            else if (userType == CommonData.Enums.UserType.Admin)
            {
                this.UserType = "2";
            }
            else if (userType == CommonData.Enums.UserType.Personal)
            {
                this.UserType = "5";
            }
            this.AgentEmail = agentEmail;
            this.IsValid = true;
        }

        /// <summary>
        /// 获取cookie 读cookie实例化
        /// </summary>
        public AgentCookie()
        {
            System.Web.HttpRequest request = System.Web.HttpContext.Current.Request;
            System.Web.HttpServerUtility server = System.Web.HttpContext.Current.Server;
            if (request.Cookies["agent_validation"] != null && request.Cookies["agent_validation"].HasKeys && !string.IsNullOrEmpty(request.Cookies["agent_validation"]["id"]) && !string.IsNullOrEmpty(request.Cookies["agent_validation"]["v"]))
            {
                try
                {
                    UserId = request.Cookies["agent_validation"]["id"].Parse<int>();
                    UserName = server.UrlDecode(request.Cookies["agent_validation"]["u"]);
                    Password = request.Cookies["agent_validation"]["p"];
                    AgentValidation = request.Cookies["agent_validation"]["v"];
                    if (AgentValidation == CookieEncrypt(ENCRYPTTYPE_AGENT))
                    {
                        if (!string.IsNullOrEmpty(request.Cookies["agent_validation"]["a"]))
                        {
                            UserType = request.Cookies["agent_validation"]["a"];
                        }
                        if (request.Cookies["agentemail"] != null && request.Cookies["agentemail"].Value != string.Empty)
                        {
                            AgentEmail = request.Cookies["agentemail"].Value;
                        }
                        IsValid = true;
                    }
                    else
                    {
                        UserId = 0;
                        UserName = string.Empty;
                        IsValid = false;
                    }
                }
                catch
                {
                    UserId = 0;
                    UserName = string.Empty;
                    IsValid = false;
                }
            }
            else
            {
                UserId = 0;
                UserName = string.Empty;
                IsValid = false;
            }
        }
        #endregion

        #region 公用方法
        /// <summary>
        /// 加密cookie 
        /// <param name="encryptType">类型 1 经纪人cookie；2 通行证cookie</param>
        /// </summary>
        public override string CookieEncrypt(int encryptType)
        {
            if (encryptType == ENCRYPTTYPE_AGENT)
            {
                int length = UserName.Length;
                string strEncrypt = UserName.Substring(0, length - 1) + "仝" + UserId + UserName.Substring(length - 1);
                return Security.Encrypt(strEncrypt, ENCRYPTKEY_AGENT);
            }
            else if (encryptType == ENCRYPTTYPE_PASSPORT)
            {

                string strEncrypt = string.Format("{0},{1}", UserId, UserName);
                return Security.Encrypt(strEncrypt, ENTRYPTKEY_PASSPORT);
            }
            else
            {
                return string.Empty;
            }
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
            string strValidationNew = CookieEncrypt(ENCRYPTTYPE_PASSPORT);//校验码供通行证cookie使用
            string cookieDomain = DOMAIN;
            DateTime cookie_date = System.DateTime.Now.AddHours(12);

            response.Cookies["agent_validation"].Domain = cookieDomain;
            response.Cookies["agent_validation"]["u"] = server.UrlEncode(UserName);

            response.Cookies["agent_validation"]["p"] = Password;

            response.Cookies["agent_validation"]["id"] = UserId.ToString();

            response.Cookies["agent_validation"]["v"] = strValidation;

            //通行证新cookie
            response.Cookies["passport"].Domain = cookieDomain;
            response.Cookies["passport"]["username"] = server.UrlEncode(UserName);

            response.Cookies["passport"]["password"] = Password;

            response.Cookies["passport"]["userid"] = UserId.ToString();

            response.Cookies["passport"]["validation"] = strValidationNew;

            response.Cookies["passport"]["usertype"] = "1"; //usertype默认写1

            if (request.Cookies["passport"] != null && request.Cookies["passport"].HasKeys && !string.IsNullOrEmpty(request.Cookies["passport"]["usertype"]) && !string.IsNullOrEmpty(request.Cookies["passport"]["isvalid"]))
            {
                response.Cookies["passport"]["usertype"] = request.Cookies["passport"]["usertype"];
                response.Cookies["passport"]["isvalid"] = request.Cookies["passport"]["isvalid"];
            }
            switch (UserType)
            {
                case CommonData.Constant.UserTypeConstant.AGENT:
                    //将经纪人邮箱写入Cookie
                    if (!string.IsNullOrEmpty(AgentEmail))
                    {
                        response.Cookies["agentemail"].Value = server.UrlEncode(AgentEmail);
                        response.Cookies["agentemail"].Domain = cookieDomain;
                    }
                    response.Cookies["agent_validation"]["a"] = CommonData.Constant.UserTypeConstant.AGENT;
                    break;
                case CommonData.Constant.UserTypeConstant.ZFBUSER:
                    //将经纪人邮箱写入Cookie
                    if (!string.IsNullOrEmpty(AgentEmail))
                    {
                        response.Cookies["agentemail"].Value = server.UrlEncode(AgentEmail);
                        response.Cookies["agentemail"].Domain = cookieDomain;
                    }
                    response.Cookies["agent_validation"]["a"] = CommonData.Constant.UserTypeConstant.ZFBUSER;
                    break;
                case CommonData.Constant.UserTypeConstant.ADMIN:
                    response.Cookies["agent_validation"]["a"] = CommonData.Constant.UserTypeConstant.ADMIN;
                    break;
                case CommonData.Constant.UserTypeConstant.ENTERPRISE:
                    response.Cookies["agent_validation"]["a"] = CommonData.Constant.UserTypeConstant.ENTERPRISE;
                    break;
                case CommonData.Constant.UserTypeConstant.PERSONAL:
                    response.Cookies["agent_validation"]["a"] = CommonData.Constant.UserTypeConstant.PERSONAL;
                    break;
                default:
                    break;
            }
        }
        #endregion

        /// <summary>
        /// 清除cookie
        /// </summary>
        public static void ClearCookie()
        {
            DateTime expir = DateTime.Now.AddDays(-1);
            string cookieDomain = DOMAIN;

            System.Web.HttpContext.Current.Response.Cookies["agent_city"].Domain = cookieDomain;
            System.Web.HttpContext.Current.Response.Cookies["agent_city"].Value = string.Empty;
            System.Web.HttpContext.Current.Response.Cookies["agent_city"].Expires = expir;

            System.Web.HttpContext.Current.Response.Cookies["agent_validation"].Domain = cookieDomain;
            System.Web.HttpContext.Current.Response.Cookies["agent_validation"].Value = string.Empty;

            System.Web.HttpContext.Current.Response.Cookies["agentemail"].Domain = cookieDomain;
            System.Web.HttpContext.Current.Response.Cookies["agentemail"].Value = string.Empty;

            System.Web.HttpContext.Current.Response.Cookies["passport"].Domain = cookieDomain;
            System.Web.HttpContext.Current.Response.Cookies["passport"].Value = string.Empty;
            System.Web.HttpContext.Current.Response.Cookies["passport"].Expires = expir;
        }
    }
}
