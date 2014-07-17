using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gscoy.DataModel.Baidu.LBS
{
    /// <summary>
    /// 天气预报
    /// </summary>
    public class BaiduWeatherEntity
    {
        public string error { get; set; }
        /// <summary>
        /// 返回结果状态信息
        /// 请求状态，如果成功返回0，如果失败返回其他数字，详细见状态码附录。
        /// </summary>
        public string status { get; set; }
        /// <summary>
        /// 当前时间
        ///  	年-月-日 
        /// </summary>
        public string date { get; set; }
        /// <summary>
        /// 天气预报信息
        /// 白天可返回近期3天的天气情况（今天、明天、后天）、晚上可返回近期4天的天气情况（今天、明天、后天、大后天） 
        /// </summary>
        public List<BaiduWeatherResult> results { get; set; }
    }

    public class BaiduWeatherResult
    {
        /// <summary>
        /// 当前城市
        /// </summary>
        public string currentCity { get; set; }
        /// <summary>
        /// pm2.5
        /// 0~50，一级，优，绿色；51~100，二级，良，黄色； 101~150，三级，轻度污染，橙色； 151~200，四级，中度污染 ，红色； 201~300，五级，重度污染 ，紫色； >300，六级，严重污染， 褐红色
        /// </summary>
        public string pm25 { get; set; }

        public List<BaiduWeatherIndex> index { get; set; }

        public List<BaiduWeatherData> weather_data { get; set; }
    }

    public class BaiduWeatherIndex
    {
        /// <summary>
        /// 指数title
        /// 分为:穿衣、洗车、感冒、运动、紫外线这几个类型。 
        /// </summary>
        public string title { get; set; }
        /// <summary>
        /// 指数取值
        /// </summary>
        public string zs { get; set; }
        /// <summary>
        /// 指数含义
        /// </summary>
        public string tipt { get; set; }
        /// <summary>
        /// 指数详情
        /// </summary>
        public string des { get; set; }
    }

    public class BaiduWeatherData
    {
        /// <summary>
        /// 天气预报时间
        /// </summary>
        public string date { get; set; }
        /// <summary>
        /// 白天的天气预报图片url 
        /// </summary>
        public string dayPictureUrl { get; set; }
        /// <summary>
        /// 晚上的天气预报图片url 
        /// </summary>
        public string nightPictureUrl { get; set; }
        /// <summary>
        /// 天气状况
        /// </summary>
        public string weather { get; set; }
        /// <summary>
        /// 风力
        /// </summary>
        public string wind { get; set; }
        /// <summary>
        /// 温度
        /// </summary>
        public string temperature { get; set; }
    }
}
