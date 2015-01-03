using Gscoy.Biz.CnBlogs;
using Gscoy.DataModel.CnBlogs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Gscoy.MVCUI.Controllers
{
    public class MobileController : Controller
    {
        //
        // GET: /Mobile/

        public ActionResult Index()
        {
            ReadNewsRssBiz biz = new ReadNewsRssBiz();
            IList<NewsXmlEntity> list = biz.GetNewsRss();
            //QiuBaiHelper helper = new QiuBaiHelper();
            //var list = helper.GetContentList();
            return View(list);
        }

    }
}
