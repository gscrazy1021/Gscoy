using Gscoy.Common;
using Gscoy.WeChat.Model.ResultModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Security;

namespace Gscoy.WeChat.Biz
{
    public class BaseAPI
    {
        /// <summary>
        /// 验证微信签名
        /// </summary>
        /// <param name="tooken"></param>
        /// <param name="signature"></param>
        /// <param name="timestamp"></param>
        /// <param name="nonce"></param>
        /// <returns></returns>
        public bool CheckSignture(string token, string signature, string timestamp, string nonce)
        {
            string[] ArrTmp = { token, timestamp, nonce };

            Array.Sort(ArrTmp);
            string tmpStr = string.Join("", ArrTmp);

            tmpStr = FormsAuthentication.HashPasswordForStoringInConfigFile(tmpStr, "SHA1");
            tmpStr = tmpStr.ToLower();

            return tmpStr == signature;
        }

        /// <summary>
        /// 获取accessToken，默认7200毫秒
        /// </summary>
        /// <returns></returns>
        public string GetAccessToken()
        {
            var cache = AspnetCache.Instance;
            var key = "wechat_accesstoken";
            var accessToken = cache.Get<string>(key);
            if (string.IsNullOrEmpty(accessToken))
            {
                var appid = ConfigHelper.GetConfig("access_appid");
                var appsecret = ConfigHelper.GetConfig("access_appsecret");
                var grant_type = "client_credential";
                var url = string.Format("https://api.weixin.qq.com/cgi-bin/token?grant_type={0}&appid={1}&secret={2}", grant_type, appid, appsecret);
                var html = HttpHelper.GetHtml(url);
                var accessEntity = html.FromJson<AccessTokenEntity>();
                var tempToken = accessEntity.AccessToken;
                cache.Set(key, tempToken, DateTime.Now.AddMilliseconds(7200));
                accessToken = tempToken;
            }
            return accessToken;
        }
    }
}
