using Gscoy.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Gscoy.WebUI.Blog
{
    public partial class Index :UIBase
    {
        protected string barHtml = string.Empty;

        protected void Page_Load(object sender, EventArgs e)
        {
            blogRpt.DataSource = new object[] {1,2,3,4,5,6 };
            blogRpt.DataBind();
            barHtml = BarCodeToHTML.getEAN13("aaaaaaaaaaaab", 100, 20);
        }
    }
}