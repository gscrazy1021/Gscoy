using Gscoy.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Gscoy.WebUI
{
    public partial class Site : System.Web.UI.MasterPage
    {
        /// <summary>
        /// 图片根目录
        /// </summary>
        protected string imgHost = string.Empty;
        /// <summary>
        /// 网站根目录
        /// </summary>
        protected string webHost = string.Empty;

        protected void Page_Load(object sender, EventArgs e)
        {
            imgHost = ConfigHelper.GetConfig("ImgHost");
            webHost = ConfigHelper.GetConfig("WebHost");
        }
    }
}