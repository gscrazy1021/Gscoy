using Gscoy.Common;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Xml.Linq;

namespace Gscoy.WeChat.Biz
{
    /// <summary>
    /// 微信api
    /// </summary>
    public class WeChatAPI : BasePage, IHttpHandler
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
                    var handler = Handler.HandlerFactory.CreateHandler(postString);
                    var response = handler.HandleRequest();
                    HttpContext.Current.Response.Write(response);
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
            var token = ConfigHelper.GetConfig("WeChatToken");
            var echoString = GetRequestString("echoStr");
            var signature = GetRequestString("signature");
            var timestamp = GetRequestString("timestamp");
            var nonce = GetRequestString("nonce");

            if (baseApi.CheckSignture(token, signature, timestamp, nonce))
            {
                if (!string.IsNullOrEmpty(echoString))
                {
                    HttpContext.Current.Response.Write(echoString);
                    HttpContext.Current.Response.End();
                }
            }
        }

        public bool IsReusable
        {
            get { return true; }
        }
    }
}
