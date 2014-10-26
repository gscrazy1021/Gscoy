using Gscoy.WeChat.Biz;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Gscoy.WebUI
{
    /// <summary>
    /// WeChatHandler 的摘要说明
    ///// </summary>
    public class WeChatHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            //base.ProcessRequest(context);
            WechatAPI api = new WechatAPI();
            if (context.Request.HttpMethod.ToUpper() == "GET")
            {
                api.Valid();
            }
            else
            {
                api.GetReciveMsg();
            }
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