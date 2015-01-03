using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Gscoy.Common;

namespace Gscoy.WebUI.Handler
{
    /// <summary>
    /// Summary description for JsonHandler
    /// </summary>
    public class JsonHandler : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            var entity = new { title = "fsdfdsffsfsf", content = "放松放松放松" };
            context.Response.Write(entity.ToJson());
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