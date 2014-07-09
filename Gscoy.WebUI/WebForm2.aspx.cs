using Gscoy.Biz;
using Gscoy.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Gscoy.WebUI
{
    public partial class WebForm2 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            var msg = "message";
            SFB s = new SFB();
           msg+="               "+ s.GetHouse();
            LogNetHelper.Write(msg, LogNetHelper.LogMessageType.Warn);
            LogHelper.Trace(msg);
            Response.Write(msg);
        }
    }
}