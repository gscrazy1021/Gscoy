using Gscoy.Common;
using Gscoy.WeChat.Biz;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;

namespace Gscoy.WebUI
{
    /// <summary>
    /// Handler1 的摘要说明
    /// </summary>
    public class Handler1 : WeChatAPI
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