using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Gscoy.Common;
using Microsoft.VisualStudio.TestTools.UnitTesting;
namespace Gscoy.Common.Tests
{
    [TestClass()]
    public class XMLHelperTests
    {
        [TestMethod()]
        public void GetElementValueTest()
        {
            var xml = @"<CityWeatherResponse><error>0</error><status>success</status><date>2014-07-11</date><results><currentCity>北京</currentCity><weather_data><date>周五 07月11日 (实时：34℃)</date><dayPictureUrl>http://api.map.baidu.com/images/weather/day/qing.png</dayPictureUrl><nightPictureUrl>http://api.map.baidu.com/images/weather/night/duoyun.png</nightPictureUrl><weather>晴转多云</weather><wind>微风</wind><temperature>36 ~ 23℃</temperature><date>周六</date><dayPictureUrl>http://api.map.baidu.com/images/weather/day/duoyun.png</dayPictureUrl><nightPictureUrl>http://api.map.baidu.com/images/weather/night/qing.png</nightPictureUrl><weather>多云转晴</weather><wind>微风</wind><temperature>33 ~ 24℃</temperature><date>周日</date><dayPictureUrl>http://api.map.baidu.com/images/weather/day/qing.png</dayPictureUrl><nightPictureUrl>http://api.map.baidu.com/images/weather/night/duoyun.png</nightPictureUrl><weather>晴转多云</weather><wind>微风</wind><temperature>34 ~ 23℃</temperature><date>周一</date><dayPictureUrl>http://api.map.baidu.com/images/weather/day/leizhenyu.png</dayPictureUrl><nightPictureUrl>http://api.map.baidu.com/images/weather/night/duoyun.png</nightPictureUrl><weather>雷阵雨转多云</weather><wind>微风</wind><temperature>34 ~ 24℃</temperature></weather_data><index><title>穿衣</title><zs>炎热</zs><tipt>穿衣指数</tipt><des>天气炎热，建议着短衫、短裙、短裤、薄型T恤衫等清凉夏季服装。</des><title>洗车</title><zs>适宜</zs><tipt>洗车指数</tipt><des>适宜洗车，未来持续两天无雨天气较好，适合擦洗汽车，蓝天白云、风和日丽将伴您的车子连日洁净。</des><title>感冒</title><zs>少发</zs><tipt>感冒指数</tipt><des>各项气象条件适宜，发生感冒机率较低。但请避免长期处于空调房间中，以防感冒。</des><title>运动</title><zs>较适宜</zs><tipt>运动指数</tipt><des>天气较好，户外运动请注意防晒。推荐您进行室内运动。</des><title>紫外线强度</title><zs>强</zs><tipt>紫外线强度指数</tipt><des>紫外线辐射强，建议涂擦SPF20左右、PA++的防晒护肤品。避免在10点至14点暴露于日光下。</des></index><pm25>59</pm25></results></CityWeatherResponse>";

            var val = XMLHelper.GetElementValue(xml);
            Assert.Fail();
        }
    }
}
