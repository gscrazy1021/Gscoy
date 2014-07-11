using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Gscoy.Common;
using Microsoft.VisualStudio.TestTools.UnitTesting;
namespace Gscoy.Common.Tests
{
    [TestClass()]
    public class Hanzi2PinyinTests
    {
        [TestMethod()]
        public void GetChineseSpellTest()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public void GetFirstPinyinTest()
        {
            //var r = Hanzi2Pinyin.convert("高爽");

            Assert.Fail();
        }

        [TestMethod()]
        public void AddFirstPinyinTest()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public void ChsString2SpellTest()
        {
            var r = Hanzi2Pinyin.ChsString2Spell("高爽");
            var m = Hanzi2Pinyin.GetHeadOfChs("高爽");
            var n = Hanzi2Pinyin.GetHeadOfSingleChs("高爽");
            var s = Hanzi2Pinyin.SingleChs2Spell("高爽");
            Assert.Fail();
        }

        [TestMethod()]
        public void GetHeadOfChsTest()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public void SingleChs2SpellTest()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public void GetHeadOfSingleChsTest()
        {
            Assert.Fail();
        }
    }
}
