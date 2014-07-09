using Gscoy.WeChat.Biz;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Gscoy.WebUI
{
    /// <summary>
    /// WeChatHandler 的摘要说明
    /// </summary>
    public class WeChatHandler : WeChatAPI
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            base.ProcessRequest(context);
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}