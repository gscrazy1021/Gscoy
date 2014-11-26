using Gscoy.Common;
using Gscoy.Common.Web;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Gscoy.WebUI
{
    public class UIBase : PubPageBase
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

        /// <summary>
        /// select的脚本
        /// </summary>
        /// <param name="objid"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        protected string SelectScript(string objid, string value)
        {
            var str = string.Format(@"<script type='text/javascript'>var tradeStatus = '{1}';var SelOptAction = document.getElementById('{0}');for (k = 0; k < SelOptAction.length; k++) {{if (SelOptAction.options[k].value == tradeStatus) {{SelOptAction.options[k].selected = true;break;}}}}</script>", objid, value);
            return str;
        }
    }
}