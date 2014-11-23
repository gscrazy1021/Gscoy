using Gscoy.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Gscoy.WebUI
{
    public class UIBase : System.Web.UI.Page
    {
        /// <summary>
        /// 图片根目录
        /// </summary>
        protected string imgHost = string.Empty;
        /// <summary>
        /// 网站根目录
        /// </summary>
        protected string webHost = string.Empty;

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);
            imgHost = ConfigHelper.GetConfig("ImgHost");
            webHost = ConfigHelper.GetConfig("WebHost");
        }
    }
}