using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Gscoy.Biz.Baidu;
using Gscoy.Common;
using Gscoy.WeChat.Biz;
using Gscoy.WeChat.Model.UserInfo;

namespace Gscoy.WebUI.Wap
{
    public partial class WapIndex : BasePage
    {
        protected string PageResult = string.Empty;
        protected string weather_city = "请输入要查询的城市天气...";

        private UserInfoBiz userBiz = new UserInfoBiz();

        protected void Page_Load(object sender, EventArgs e)
        {
            var action = GetRequestString("action");
            switch (action.ToLower())
            {
                case "weather":
                    weather_city = GetRequestString("txtWeatherCity");
                    PageResult = LBSHelper.GetWeather(weather_city);
                    Response.Write(PageResult);
                    Response.End();
                    break;
                case "usermanager":
                    var entity = userBiz.SearchAllGroup();
                    PageResult = GetAllGroupDiv(entity);
                    Response.Write(PageResult);
                    Response.End();
                    break;
                default:
                    break;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        private string GetAllGroupDiv(SearchGroupList entity)
        {
            var div = string.Empty;
            foreach (var item in entity.groups)
            {
                div += string.Format("<div data-role=\"collapsible\"><h3>{0}-({1})</h3><p>我是可折叠的内容。</p></div>", item.name, item.count);
            }
            return div;
        }
    }
}