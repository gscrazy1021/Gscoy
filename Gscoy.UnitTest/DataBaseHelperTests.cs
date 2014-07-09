using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Gscoy.Data;
using Microsoft.VisualStudio.TestTools.UnitTesting;
namespace Gscoy.Data.Tests
{
    [TestClass()]
    public class DataBaseHelperTests
    {
        [TestMethod()]
        public void GetInstanceTest()
        {
            var db = DataBaseHelper.GetInstance(Common.Enums.DBUserType.User_R);
            var dt = db.ExecuteDateTable("select top 1 * from agentinfo with(nolock)");

            Assert.Fail();
        }

        [TestMethod()]
        public void CloseTest()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public void CreateParametersTest()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public void AddParametersTest()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public void ExecuteNonQueryTest()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public void ExecuteReaderTest()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public void ExecuteDateTableTest()
        {
            Assert.Fail();
        }
    }
}
