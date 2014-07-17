using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gscoy.DataModel.Baidu.LBS
{
    /// <summary>
    /// 热映影片
    /// </summary>
    public class BaiduHotMovieEntity
    {
        public string error { get; set; }
        /// <summary>
        /// 返回结果状态信息
        /// </summary>
        public string status { get; set; }
        public string date { get; set; }
        /// <summary>
        /// 返回结果
        /// </summary>
        public BaiduHotMovieResultEntity result { get; set; }
    }

    public class BaiduHotMovieResultEntity
    {
        /// <summary>
        /// 城市代码
        /// </summary>
        public string cityid { get; set; }
        /// <summary>
        /// 城市名称
        /// </summary>
        public string cityname { get; set; }
        /// <summary>
        /// 当前城市坐标（经纬度） 
        /// </summary>
        public BaiduLoactionEntity location { get; set; }
        /// <summary>
        /// 每项电影信息
        /// </summary>
        public List<BaiduHotMovieItem> movie { get; set; }
    }

    public class BaiduHotMovieItem
    {
        /// <summary>
        /// 影片id
        /// </summary>
        public string movie_id { get; set; }
        /// <summary>
        /// 影片名称
        /// </summary>
        public string movie_name { get; set; }
        /// <summary>
        /// 影片类型
        /// </summary>
        public string movie_type { get; set; }
        /// <summary>
        /// 影片上映时间
        /// </summary>
        public string movie_release_date { get; set; }
        /// <summary>
        /// 影片所属国家
        /// </summary>
        public string movie_nation { get; set; }
        /// <summary>
        /// 影片演员
        /// </summary>
        public string movie_starring { get; set; }
        /// <summary>
        /// 影片时长(分钟)
        /// </summary>
        public string movie_length { get; set; }
        /// <summary>
        /// 影片图片地址
        /// </summary>
        public string movie_picture { get; set; }
        /// <summary>
        /// 影片评分
        /// </summary>
        public string movie_score { get; set; }
        /// <summary>
        /// 影片导演
        /// </summary>
        public string moview_director { get; set; }
        /// <summary>
        /// 影片所属类型
        /// </summary>
        public string movie_tags { get; set; }
        /// <summary>
        /// 影片概要信息
        /// </summary>
        public string movie_message { get; set; }
        /// <summary>
        /// 是否imax类型 
        /// </summary>
        public string is_imax { get; set; }
        /// <summary>
        /// 是否首次上映
        /// </summary>
        public string is_new { get; set; }
        /// <summary>
        /// 影片大图
        /// </summary>
        public string movie_big_picture { get; set; }
        /// <summary>
        /// 影片关键字
        /// </summary>
        public string movies_wd { get; set; }
    }
}
