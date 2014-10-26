using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Gscoy.DBase;
using Microsoft.VisualStudio.TestTools.UnitTesting;
namespace Gscoy.DBase.Tests
{
    [TestClass()]
    public class DBHelperTests
    {
        [TestMethod()]
        public void GetDBHelperTest()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public void QueryTest()
        {
            DBHelper db = DBHelper.GetDBHelper(OperationType.Read);
            var ss = db.Query<UserInfo>("select * from userinfo");

            var sssss = db.Query<int>("select count(1) from userinfo");
            Assert.Fail();
        }

        class UserInfo
        {
            public int Id { get; set; }
            public string UserName { get; set; }
            public string Pwd { get; set; }
        }

        [TestMethod()]
        public void ExecuteTest()
        {
            Assert.Fail();
        }
    }
}
