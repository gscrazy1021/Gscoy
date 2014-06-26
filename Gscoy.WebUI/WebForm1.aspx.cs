using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Gscoy.WebUI
{
    public partial class WebForm1 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            pc.PageCount = 10;
            pc.CurPage = 2;
            pc.AllCount = 20;

            var val = Gscoy.Common.ConfigHelper.GetConfig<string>("version");
            Response.Write(val);
        }
    }
}