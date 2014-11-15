using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gscoy.DataModel.Baidu.LBS
{
    /// <summary>
    /// 天气预报
    /// </summary>
    public class WeatherInfoEntity
    {
        public int error { get; set; }
        public string status { get; set; }
        public string date { get; set; }
        public WeatherResult[] results { get; set; }
    }

    public class WeatherResult
    {
        public string currentCity { get; set; }
        public string pm25 { get; set; }
        public WeatherIndex[] index { get; set; }
        public Weather_Data[] weather_data { get; set; }
    }

    public class WeatherIndex
    {
        public string title { get; set; }
        public string zs { get; set; }
        public string tipt { get; set; }
        public string des { get; set; }
    }

    public class Weather_Data
    {
        public string date { get; set; }
        public string dayPictureUrl { get; set; }
        public string nightPictureUrl { get; set; }
        public string weather { get; set; }
        public string wind { get; set; }
        public string temperature { get; set; }
    }
}
