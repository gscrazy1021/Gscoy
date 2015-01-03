using Gscoy.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;

namespace Gscoy.MVCUI.Controllers
{
    public class BaseController : Controller
    {
        public string HostName
        {
            get
            {
                return ConfigHelper.GetConfig("HostName");
            }
        }

        protected override IActionInvoker CreateActionInvoker()
        {
            ViewBag.HostName = this.HostName;
            return base.CreateActionInvoker();
        }
    }
}
