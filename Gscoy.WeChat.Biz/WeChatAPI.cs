using Gscoy.Common;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;

namespace Gscoy.WeChat.Biz
{
    /// <summary>
    /// 微信api
    /// </summary>
    public class WeChatAPI : IHttpHandler
    {
        BaseAPI baseApi = new BaseAPI();

        public void ProcessRequest(HttpContext context)
        {
            string postString = string.Empty;
            if (HttpContext.Current.Request.HttpMethod.ToUpper() == "POST")
            {
                using (Stream stream = HttpContext.Current.Request.InputStream)
                {
                    Byte[] postBytes = new Byte[stream.Length];
                    stream.Read(postBytes, 0, (Int32)stream.Length);
                    postString = Encoding.UTF8.GetString(postBytes);
                }

                if (!string.IsNullOrEmpty(postString))
                {
                    Execute(postString);
                }
            }
            else
            {
                Auth(); //微信接入的测试
            }
        }

        /// <summary>
        /// 验证微信接口有效性
        /// </summary>
        private void Auth()
        {
            var token = ConfigHelper.GetConfig("WeChatTooken");
            var echoString = ConfigHelper.GetConfig("echoStr");
            var signature = ConfigHelper.GetConfig("signature");
            var timestamp = ConfigHelper.GetConfig("timestamp");
            var nonce = ConfigHelper.GetConfig("nonce");

            if (baseApi.CheckSignture(token, signature, timestamp, nonce))
            {
                if (!string.IsNullOrEmpty(echoString))
                {
                    HttpContext.Current.Response.Write(echoString);
                    HttpContext.Current.Response.End();
                }
            }
        }

        /// <summary>
        /// 处理各种请求信息并应答（通过POST的请求）
        /// </summary>
        /// <param name="postString"></param>
        private void Execute(string postString)
        {

        }

        public bool IsReusable
        {
            get { return true; }
        }
    }
}
