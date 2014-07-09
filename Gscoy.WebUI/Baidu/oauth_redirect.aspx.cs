using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Gscoy.Common;
using Gscoy.DataModel.Baidu;

namespace Gscoy.WebUI.Baidu
{
    public partial class oauth_redirect : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            var code = GetRequestString("code");
            var url = ConfigHelper.GetConfig("BaiduTokenUrl");
            var ak = ConfigHelper.GetConfig("BaiduAK");
            var sk = ConfigHelper.GetConfig("BaiduSK");
            var redictUrl = string.Format("{0}:{2}/{1}", HttpContext.Current.Request.Url.Host, ConfigHelper.GetConfig("BaiduRedictUrl"), HttpContext.Current.Request.Url.Port);
            var postUrl = string.Format("{0}?grant_type=authorization_code&code={1}&client_id={2}&client_secret={3}&redirect_uri={4}", url, code, ak, sk, redictUrl);
            var data = HttpHelper.GetHtml(postUrl, "", true);
            var tokenEntity = JsonHelper.FromJson<TokenEntity>(data);
            AspnetCache cache = AspnetCache.Instance;
            var key = string.Format("baidukey");
            cache.Set(key, tokenEntity.AccessToken, DateTime.Now.AddSeconds(double.Parse(tokenEntity.ExpressIn)));
        }
    }
}