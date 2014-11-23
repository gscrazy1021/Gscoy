using Gscoy.Biz.CnBlogs;
using Gscoy.DataModel.CnBlogs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Gscoy.WebUI.News
{
    public partial class Index : UIBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            ReadNewsRssBiz newsBiz = new ReadNewsRssBiz();
            IList<NewsXmlEntity> newsList = newsBiz.GetNewsRss();
            newsRpt.DataSource = newsList;
            newsRpt.DataBind();
        }
    }
}