using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Gscoy.Common;
using Gscoy.DataModel.Weather;

namespace Gscoy.Biz
{
    public class WeatherHelper
    {
        /// <summary>
        /// 根据城市id获取当前温度
        /// </summary>
        /// <param name="cityid"></param>
        /// <returns></returns>
        public static string GetWeatherInfo(string cityid)
        {
            string url = string.Format("http://www.weather.com.cn/data/sk/{0}.html", cityid);
            return GetWeather(url);
        }
        /// <summary>
        /// 私有 获取天气方法
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        private static string GetWeather(string url)
        {
            var json = HttpHelper.GetHtml(url);
            var weather = json.FromJson<WeatherInfoEntity>();
            var entity = weather.weather;
            var weatherInfo = string.Format(@"中央气象局{0}发布{1}天气预报：当前温度：{2}℃，风向：{3}，风力{4}，相对湿度：{5}", entity.time, entity.city, entity.temp, entity.WD, entity.WS, entity.SD);
            return weatherInfo;
        }

        static Dictionary<string, string> cityList = null;
        static WeatherHelper()
        {
            //取城市编号时加上缓存机制
            AspnetCache cache = AspnetCache.Instance;
            string key = string.Format("weather_city");
            cityList = cache.Get<Dictionary<string, string>>(key);
            if (cityList == null)
            {
                cityList = new Dictionary<string, string>();
                var weatherPath = AppDomain.CurrentDomain.BaseDirectory + ConfigHelper.GetConfig("weatherPath");
                try
                {
                    var lineList = FileHelper.ReadFileLines(weatherPath);
                    foreach (var item in lineList)
                    {
                        if (!string.IsNullOrEmpty(item))
                        {
                            var pars = item.Split('=');
                            if (!cityList.ContainsKey(pars[1]))
                                cityList.Add(pars[1], pars[0]);
                        }
                    }
                    cache.Set(key, cityList, DateTime.Now.AddHours(6));
                }
                catch (Exception ex)
                {

                    throw;
                }
            }
        }

        public static string GetWeatherInfoByCity(string city)
        {
            if (cityList.ContainsKey(city))
            {
                var cityid = cityList[city];
                var url = string.Format("http://www.weather.com.cn/data/sk/{0}.html", cityid);
                return GetWeather(url);
            }
            else
            {
                return string.Format("抱歉，没有找到您输入的城市，请重新输入试试。");
            }
        }
    }
}
