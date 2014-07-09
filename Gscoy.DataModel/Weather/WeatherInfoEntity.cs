using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;

namespace Gscoy.DataModel.Weather
{
    /// <summary>
    ///中国天气通----天气实体
    /// </summary>
    public class WeatherEntity
    {
        /// <summary>
        /// 城市中文名
        /// </summary>
        [JsonProperty("city")]
        public string city { get; set; }
        /// <summary>
        /// 城市 ID
        /// </summary>
        [JsonProperty("cityid")]
        public string cityid { get; set; }
        /// <summary>
        /// 温度
        /// </summary>
        [JsonProperty("temp")]
        public string temp { get; set; }
        /// <summary>
        /// 风向
        /// </summary>
        [JsonProperty("WD")]
        public string WD { get; set; }
        /// <summary>
        /// 风力
        /// </summary>
        [JsonProperty("WS")]
        public string WS { get; set; }
        /// <summary>
        /// 湿度
        /// </summary>
        [JsonProperty("SD")]
        public string SD { get; set; }
        /// <summary>
        /// 
        /// </summary>
        [JsonProperty("WSE")]
        public string WSE { get; set; }
        /// <summary>
        /// 发布时间
        /// </summary>
        [JsonProperty("time")]
        public string time { get; set; }
        /// <summary>
        /// 是否有雷达图
        /// </summary>
        [JsonProperty("isRadar")]
        public string isRadar { get; set; }
        /// <summary>
        /// 雷达图编号，雷达图的地址在 http://www.weather.com.cn/html/radar/雷达图编号.shtml
        /// </summary>
        [JsonProperty("Radar")]
        public string Radar { get; set; }
    }

    public class WeatherInfoEntity
    {
        [JsonProperty("weatherinfo")]
        public WeatherEntity weather { get; set; }
    }
}
