using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Gscoy.Biz.Baidu;
using Microsoft.VisualStudio.TestTools.UnitTesting;
namespace Gscoy.Biz.Baidu.Tests
{
    [TestClass()]
    public class LBSHelperTests
    {
        [TestMethod()]
        public void GetWeatherTest()
        {
            var s = LBSHelper.GetWeather(city: "邦均");
            Assert.Fail();
        }

        [TestMethod()]
        public void GetWeatherTest1()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public void GetWeatherEntityTest()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public void GetHotMovieTest()
        {
            var s = LBSHelper.GetHotMovie();
            Assert.Fail();
        }

        [TestMethod()]
        public void GetHotMovieEntityTest()
        {
            Assert.Fail();
        }
    }
}
