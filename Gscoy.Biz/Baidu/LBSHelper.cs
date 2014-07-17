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

        public static string GetWeather(string city = "", string loactionX = "", string loactionY = "")
        {
            var result = string.Empty;
            var value = GetCityInfo(city, loactionX, loactionY);
            var entity = GetWeatherEntity(city, loactionX, loactionY);
            if (entity != null && entity.results != null)
            {
                foreach (var item in entity.results)
                {
                    result += string.Format("当前城市：{0}，pm2.5：{1}\n", item.currentCity, string.IsNullOrEmpty(item.pm25) ? "未知" : item.pm25);
                    if (item.weather_data != null)
                    {
                        foreach (var data in item.weather_data)
                        {
                            result += string.Format("日期：{0}\n天气：{1}\n风力：{2}\n温度：{3}\n", data.date, data.weather, data.wind, data.temperature);
                        }
                    }
                    if (item.index != null)
                    {
                        foreach (var index in item.index)
                        {
                            result += string.Format("{0}:{1}\n", index.tipt, index.des);
                        }
                    }
                }
            }
            return result;
        }

        public static BaiduWeatherEntity GetWeatherEntity(string city = "", string loactionX = "", string loactionY = "")
        {
            var value = GetCityInfo(city, loactionX, loactionY);
            var url = string.Format("http://api.map.baidu.com/telematics/v3/weather?location={0}&output=json&ak={1}", value, baiduAK);
            var json = HttpHelper.GetHtml(url);
            var entity = json.FromJson<BaiduWeatherEntity>();
            return entity;
        }

        public static string GetHotMovie(string city = "", string loactionX = "", string loactionY = "")
        {
            var result = string.Empty;
            var entity = GetHotMovieEntity(city, loactionX, loactionY);
            if (entity != null && entity.result != null)
            {
                result += string.Format("当前位置：{0}\n", entity.result.cityname);
                if (entity.result.movie != null)
                {
                    foreach (var item in entity.result.movie)
                    {
                        result += string.Format("影片名称：{0}，类型：{1}，支持IMAX：{9}，上映时间：{2}，地区：{3}，主要演员：{4}，影片时长：{5}分，影片评分：{6}分，导演：{7}，标签：{8}，简介：{10}\n", item.movie_name, item.movie_type, item.movie_release_date, item.movie_nation, item.movie_starring, item.movie_length, item.movie_score, item.moview_director, item.movie_tags, item.is_imax == "1" ? "是" : "否", item.movie_message);
                    }
                }
            }
            return result;
        }

        public static BaiduHotMovieEntity GetHotMovieEntity(string city = "", string loactionX = "", string loactionY = "")
        {
            var value = GetCityInfo(city, loactionX, loactionY);
            var url = string.Format("http://api.map.baidu.com/telematics/v3/movie?qt=hot_movie&location={0}&ak={1}&output=json", value, baiduAK);
            var json = HttpHelper.GetHtml(url);
            var entity = json.FromJson<BaiduHotMovieEntity>();
            return entity;
        }
    }
}
