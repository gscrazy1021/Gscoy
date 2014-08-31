using System.Web;
using System.Web.Mvc;

namespace Gscoy.Mvc.MobileUI
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }
    }
}