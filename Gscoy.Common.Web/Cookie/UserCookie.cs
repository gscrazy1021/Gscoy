using SouFun.ESF.SFB.CommonData.Enums;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;

namespace Gscoy.Common.Web
{
    public class UserCookie : CookieBase
    {
        public UserCookie()
        {
        }
        public static void UserloginCookie(string userName, string pwd, string userId, UserType userType, string soufunEmail, AgentType agentType, bool isZfbUser)
        {
            string strValidation = Cookie_Encrypt(userName, userId);//校验码
            string strValidationNew = Cookie_EncryptNew(userName, userId);//校验码
            //string strEPwd = Encrypt(pwd, "gyztlhdl");//加密后的密码
            //string strEPwdNew = Encrypt(pwd, "w8f3k9c2");//加密后的密码
            string cookie_domain = "soufun.com";
            DateTime cookie_date = System.DateTime.Now.AddHours(12);

            System.Web.HttpContext.Current.Response.Cookies["agent_validation"].Domain = cookie_domain;
            System.Web.HttpContext.Current.Response.Cookies["agent_validation"]["u"] = System.Web.HttpContext.Current.Server.UrlEncode(userName);

            System.Web.HttpContext.Current.Response.Cookies["agent_validation"]["p"] = pwd;

            System.Web.HttpContext.Current.Response.Cookies["agent_validation"]["id"] = userId;

            System.Web.HttpContext.Current.Response.Cookies["agent_validation"]["v"] = strValidation;

            //通行证新cookie
            System.Web.HttpContext.Current.Response.Cookies["passport"].Domain = cookie_domain;
            System.Web.HttpContext.Current.Response.Cookies["passport"]["username"] = System.Web.HttpContext.Current.Server.UrlEncode(userName);

            System.Web.HttpContext.Current.Response.Cookies["passport"]["password"] = pwd;

            System.Web.HttpContext.Current.Response.Cookies["passport"]["userid"] = userId;

            System.Web.HttpContext.Current.Response.Cookies["passport"]["validation"] = strValidationNew;

            if (System.Web.HttpContext.Current.Request.Cookies["passport"] != null && System.Web.HttpContext.Current.Request.Cookies["passport"].HasKeys && !string.IsNullOrEmpty(System.Web.HttpContext.Current.Request.Cookies["passport"]["usertype"]) && !string.IsNullOrEmpty(System.Web.HttpContext.Current.Request.Cookies["passport"]["isvalid"]))
            {
                System.Web.HttpContext.Current.Response.Cookies["passport"]["usertype"] = System.Web.HttpContext.Current.Request.Cookies["passport"]["usertype"];
                System.Web.HttpContext.Current.Response.Cookies["passport"]["isvalid"] = System.Web.HttpContext.Current.Request.Cookies["passport"]["isvalid"];
            }

            if (userType == UserType.Agent)
            {
                //将经纪人邮箱写入Cookie
                if (!string.IsNullOrEmpty(soufunEmail))
                {
                    HttpContext.Current.Response.Cookies["agentemail"].Value = System.Web.HttpContext.Current.Server.UrlEncode(soufunEmail);
                    HttpContext.Current.Response.Cookies["agentemail"].Domain = cookie_domain;
                }
                ////写isagent
                //HttpContext.Current.Response.Cookies["agent_isagent"].Value = "1";
                //HttpContext.Current.Response.Cookies["agent_isagent"].Domain = cookie_domain;

                //增加租赁版经纪人判断 2010-7-2 shizir
                if (agentType == AgentType.LeaseOnly)
                {
                    HttpContext.Current.Response.Cookies["agent_validation"]["a"] = "4";
                }
                else
                {
                    HttpContext.Current.Response.Cookies["agent_validation"]["a"] = "1";
                }
            }
            else if (userType == UserType.Enterprise)
            {
                HttpContext.Current.Response.Cookies["agent_validation"]["a"] = "3";
            }
            else if (userType == UserType.Admin)
            {
                HttpContext.Current.Response.Cookies["agent_validation"]["a"] = "2";
            }
            else if (userType == UserType.Personal)
            {
                HttpContext.Current.Response.Cookies["agent_validation"]["a"] = "5";
            }
            if (isZfbUser)
            {
                HttpContext.Current.Response.Cookies["agent_validation"]["z"] = "1";
            }
        }

        /// <summary>
        /// 加密Cookie
        /// </summary>
        /// <param name="username">用户名</param>
        /// <param name="userid">用户编号</param>
        public static string Cookie_Encrypt(string username, string userid)
        {
            int length = username.Length;
            string strEncrypt = username.Substring(0, length - 1) + "仝" + userid + username.Substring(length - 1);
            string strKey = "uensocty";
            return Encrypt(strEncrypt, strKey);
        }
        /// <summary>
        /// 加密密码
        /// </summary>
        /// <param name="pToEncrypt">要加密的密码</param>
        /// <param name="sKey">加密密钥</param>
        public static string Encrypt(string pToEncrypt, string sKey)
        {
            DESCryptoServiceProvider des = new DESCryptoServiceProvider();
            //把字符串放到byte数组中  
            byte[] inputByteArray = Encoding.Default.GetBytes(pToEncrypt);

            //建立加密对象的密钥和偏移量  
            des.Key = ASCIIEncoding.ASCII.GetBytes(sKey);
            des.IV = ASCIIEncoding.ASCII.GetBytes(sKey);

            MemoryStream ms = new MemoryStream();
            CryptoStream cs = new CryptoStream(ms, des.CreateEncryptor(), CryptoStreamMode.Write);
            cs.Write(inputByteArray, 0, inputByteArray.Length);
            cs.FlushFinalBlock();

            StringBuilder ret = new StringBuilder();
            foreach (byte b in ms.ToArray())
            {
                ret.AppendFormat("{0:X2}", b);
            }
            return ret.ToString();
        }

        /// <summary>
        /// 加密Cookie
        /// </summary>
        /// <param name="username">用户名</param>
        /// <param name="userid">用户编号</param>
        public static string Cookie_EncryptNew(string username, string userid)
        {
            string strEncrypt = userid + "," + username;
            string strKey = ENTRYPTKEY_PASSPORT;
            return Encrypt(strEncrypt, strKey);
        }

    }
}
