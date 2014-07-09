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
            cityList = new Dictionary<string, string>();
            var weatherPath = ConfigHelper.GetConfig("weatherPath");
            try
            {
                using (FileStream file = File.OpenRead(weatherPath))
                {
                    using (StreamReader sr = new StreamReader(file))
                    {
                        while (sr.Peek() > 0)
                        {
                            var line = sr.ReadLine();
                            if (!string.IsNullOrEmpty(line))
                            {
                                var parms = line.Split('=');
                                if (!cityList.ContainsKey(parms[1]))
                                    cityList.Add(parms[1], parms[0]);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {

                throw;
            }

        }

        public static string GetWeatherInfoByCity(string city)
        {
            var cityid = cityList[city];
            var url = string.Format("http://www.weather.com.cn/data/sk/{0}.html", cityid);
            return GetWeather(url);
        }
    }
}
