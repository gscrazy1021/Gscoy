using Gscoy.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.SessionState;

namespace Gscoy.WebUI
{
    public class Global : System.Web.HttpApplication
    {

        protected void Application_Start(object sender, EventArgs e)
        {
            LogNetHelper.Init();
            LogNetHelper.Write("[web站点运行...]", LogNetHelper.LogMessageType.Info);
        }

        protected void Session_Start(object sender, EventArgs e)
        {

        }

        protected void Application_BeginRequest(object sender, EventArgs e)
        {

        }

        protected void Application_AuthenticateRequest(object sender, EventArgs e)
        {

        }

        protected void Application_Error(object sender, EventArgs e)
        {
            // 在出现未处理的错误时运行的代码
            Exception ex = Server.GetLastError();
            if (ex != null)
            {
                //写入错误日志
                LogNetHelper.Write("[Exception]:", LogNetHelper.LogMessageType.Error, ex);
                throw ex;
            }
        }

        protected void Session_End(object sender, EventArgs e)
        {

        }

        protected void Application_End(object sender, EventArgs e)
        {
            LogNetHelper.Write("[web站点关闭...]", LogNetHelper.LogMessageType.Info);
        }
    }
}