using Gscoy.Biz;
using Gscoy.Biz.CnBlogs;
using Gscoy.DataModel.CnBlogs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Gscoy.MVCUI.Controllers
{
    public class HomeController : BaseController
    {
        public ActionResult Index()
        {
            ViewBag.Message = "Modify this template to jump-start your ASP.NET MVC application.";
            ReadNewsRssBiz biz = new ReadNewsRssBiz();
            IList<NewsXmlEntity> list = biz.GetNewsRss();
            //QiuBaiHelper helper = new QiuBaiHelper();
            //var list = helper.GetContentList();
            return View(list);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your app description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}
