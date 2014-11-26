using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Gscoy.Common;
using System.Text;

namespace Gscoy.WebUI.Tools
{
    public partial class Index : UIBase
    {
        protected string content = string.Empty;
        protected string charset = string.Empty;

        protected void Page_Load(object sender, EventArgs e)
        {
            content = GetRequestString("content");
            charset = GetRequestString("charsetSelect");
            if (!string.IsNullOrEmpty(charset))
            {
                if (string.IsNullOrEmpty(GetRequestString("en")))
                {
                    content = content.UrlEncode(Encoding.GetEncoding(charset));
                }
                else if (string.IsNullOrEmpty(GetRequestString("de")))
                {
                    content = content.UrlDecode(Encoding.GetEncoding(charset));
                }
            }
        }
    }
}