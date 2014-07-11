using Gscoy.Biz;
using Gscoy.WeChat.Biz;
using Gscoy.WeChat.Biz.Handler;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Gscoy.WebUI
{
    public partial class WebForm1 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            pc.PageCount = 10;
            pc.CurPage = 2;
            pc.AllCount = 20;
            SFB sf = new SFB();
            var s = sf.GetHouse();
            var val = Gscoy.Common.ConfigHelper.GetConfig("version");
            var requestXml = string.Format(@"<xml><ToUserName><![CDATA[gh_e5df289c1d17]]></ToUserName>
<FromUserName><![CDATA[oQqXfjmUcuw2YnM-ccc2f1Le9SrI]]></FromUserName>
<CreateTime>1404964131</CreateTime>
<MsgType><![CDATA[text]]></MsgType>
<Content><![CDATA[tq]]></Content>
<MsgId>6034274994898509029</MsgId>
</xml>");
            TextHandler txt = new TextHandler(requestXml);
            var m = txt.HandleRequest();
            Response.Write(val + s);
        }
    }
}