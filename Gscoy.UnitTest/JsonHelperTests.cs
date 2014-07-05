using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Gscoy.Common;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Data;
namespace Gscoy.Common.Tests
{
    [TestClass()]
    public class JsonHelperTests
    {
        [TestMethod()]
        public void ToJsonTest()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public void FromJsonTest()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public void ToJsonTest1()
        {
            var time = DateTime.Now.AddHours(1).ToString("yyyy-MM-dd HH:00:00");
            DataTable dt = new DataTable();
            dt.Columns.Add("Name");
            dt.Columns.Add("Age");
            dt.Rows.Add(new[] { "a", "1" });
            dt.Rows.Add(new[] { "b", "2" });
            var json = JsonHelper.ToJson(dt);
            var dd = JsonHelper.ToDataTable(json);
            Assert.Fail();
        }

        [TestMethod()]
        public void ToDataTableTest()
        {
            
            Assert.Fail();
        }
    }
}
