using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Gscoy.Dapper.Data;
using Microsoft.VisualStudio.TestTools.UnitTesting;
namespace Gscoy.Dapper.Data.Tests
{
    [TestClass()]
    public class DBHelperTests
    {
        [TestMethod()]
        public void ExecuteListTest()
        {
            var db = DBHelper.Instance;

            Assert.Fail();
        }

        [TestMethod()]
        public void DBHelperTest()
        {
            var entity = DBHelper.Instance;
            Assert.Fail();
        }

        [TestMethod()]
        public void ExecuteListTest1()
        {
            Assert.Fail();
        }
    }
}
