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

        /// <summary>
        /// 处理各种请求信息并应答（通过POST的请求）
        /// </summary>
        /// <param name="postString"></param>
        public void Execute(string postString)
        {
            postString = postString.Replace("<![CDATA[", "").Replace("]]>", "");
            var xmlElement = XElement.Parse(postString);
            var msgType = xmlElement.Element("MsgType").Value;
            switch (msgType)
            {
                case "text":
                    break;
                case "image":
                    break;
                case "voice":
                    break;
                case "video":
                    break;
                case "location":
                    break;
                case "link":
                    break;
                case "event":
                    var eventStr = xmlElement.Element("EVENT").Value;
                    switch (eventStr)
                    {
                        case "subscribe":
                            break;
                        case "SCAN":
                            break;
                        case "LOCATION":
                            break;
                        case "CLICK":
                            break;
                        case "VIEW":
                            break;
                    }
                    break;
              
            }
        }

        public bool IsReusable
        {
            get { return true; }
        }
    }
}
