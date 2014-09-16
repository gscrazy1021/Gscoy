using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Gscoy.Biz;
using Microsoft.VisualStudio.TestTools.UnitTesting;
namespace Gscoy.Biz.Tests
{
    [TestClass()]
    public class WeatherHelperTests
    {
        [TestMethod()]
        public void GetWeatherInfoTest()
        {
            var t = WeatherHelper.GetWeatherInfoByCity("101010100");
            Assert.Fail();
        }

        [TestMethod()]
        public void GetWeatherInfoByCityTest()
        {
            var t = WeatherHelper.GetWeatherInfoByCity("蓟县");
            Assert.Fail();
        }

        [TestMethod()]
        public void GetWeatherInfoTest1()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public void GetWeatherInfoByCityTest1()
        {
            Assert.Fail();
        }
    }
}
