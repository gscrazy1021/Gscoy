using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Gscoy.Common;
using Gscoy.DataModel.Baidu.LBS;

namespace Gscoy.Biz.Baidu
{
    public class LBSHelper
    {
        static string baiduAK = ConfigHelper.GetConfig("BaiduAK");

        private static string GetCityInfo(string city = "", string loactionX = "", string loactionY = "")
        {
            var value = string.Empty;
            if (string.IsNullOrEmpty(city))
            {
                if (string.IsNullOrEmpty(loactionX) && string.IsNullOrEmpty(loactionY))
                {
                    value = "北京";
                }
                else
                {
                    value = string.Format("{0},{1}", loactionX, loactionY);
                }
            }
            else
            {
                value = city;
            }
            value = HttpHelper.Encode(value);
            return value;
        }

        public static WeatherInfoEntity GetWeatherEntity(string city = "")
        {
            try
            {
                if (string.IsNullOrEmpty(city))
                {
                    city = "北京";
                }
                string url = string.Format("http://api.map.baidu.com/telematics/v3/weather?location={0}&output=json&ak={1}", city, baiduAK);
                HtmlHelper helper = new HtmlHelper();
                var msg = helper.GetHTML(url);
                return msg.Html.FromJson<WeatherInfoEntity>();
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public static EnGeocodingEntity GetEnGeocoding(string loactionX = "", string loactionY = "")
        {
            try
            {
                if (string.IsNullOrEmpty(loactionX) || string.IsNullOrEmpty(loactionY)) return null;
                string url = string.Format("http://api.map.baidu.com/telematics/v3/reverseGeocoding?location={0},{1}&coord_type=gcj02&ak={2}");
                HtmlHelper helper = new HtmlHelper();
                var msg = helper.GetHTML(url);
                return msg.Html.FromJson<EnGeocodingEntity>();
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public static TravelCityEntity GetTravelCity(string city = "")
        {
            try
            {
                if (string.IsNullOrEmpty(city))
                {
                    city = "北京";
                }
                string url = string.Format(" http://api.map.baidu.com/telematics/v3/travel_city?location={0}&output=json&ak={1}", city, baiduAK);
                HtmlHelper helper = new HtmlHelper();
                var msg = helper.GetHTML(url);
                return msg.Html.FromJson<TravelCityEntity>();
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public static SearchMovieEntity GetSearchMovie(string city = "", string movie = "")
        {
            try
            {
                if (string.IsNullOrEmpty(city))
                {
                    city = "北京";
                }
                string url = string.Format(" http://api.map.baidu.com/telematics/v3/movie?qt=search_movie&wd={0}&location={1}&ak={2}", movie, city, baiduAK);
                HtmlHelper helper = new HtmlHelper();
                var msg = helper.GetHTML(url);
                return msg.Html.FromJson<SearchMovieEntity>();
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public static HotMovieEntity GetHotMovie(string city = "")
        {
            try
            {
                if (string.IsNullOrEmpty(city))
                {
                    city = "北京";
                }
                string url = string.Format("  http://api.map.baidu.com/telematics/v3/movie?qt=hot_movie&location={0}&output=json&ak={1}", city, baiduAK);
                HtmlHelper helper = new HtmlHelper();
                var msg = helper.GetHTML(url);
                return msg.Html.FromJson<HotMovieEntity>();
            }
            catch (Exception ex)
            {
                return null;
            }
        }
    }

    public enum BaiduLBSOption
    {
        WeatherInfo,
        HotMovie,
        SearchMovie,
        TravelCity,
        EnGeocoding
    }
}
