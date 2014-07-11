using Gscoy.Common;
using Gscoy.DataModel.Baidu.LBS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gscoy.WeChat.Biz.Handler
{
    /// <summary>
    /// 位置
    /// </summary>
    public class LocationHandler : IHandler
    {
        /// <summary>
        /// 请求的XML
        /// </summary>
        private string RequestXml { get; set; }
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="requestXml">请求的xml</param>
        public LocationHandler(string requestXml)
        {
            LogHelper.Trace(requestXml);
            this.RequestXml = requestXml;
        }

        public string HandleRequest()
        {
            var val = string.Empty;
            try
            {
                var url = string.Format("http://api.map.baidu.com/telematics/v3/reverseGeocoding?location=116.3017193083,40.050743859593&coord_type=gcj02&ak=KuNkGjYofpyHMo6aXnIV4hvm&output=json");
                var json = HttpHelper.GetHtml(url);
                var entity = json.FromJson<EnGeocodingEntity>();
                val = string.Format("地点:{0},所在街道{1},所在街区的名称{2}", entity.description, entity.street, entity.district);
            }
            catch (Exception ex)
            {
                val = ex.Message;
            }
            return string.Format(@"<xml><ToUserName><![CDATA[oQqXfjmUcuw2YnM-ccc2f1Le9SrI]]></ToUserName><FromUserName><![CDATA[gh_e5df289c1d17]]></FromUserName><CreateTime>1405072000</CreateTime><MsgType><![CDATA[text]]></MsgType><Content><![CDATA[{0}]]></Content></xml>", val);
        }
    }
}
