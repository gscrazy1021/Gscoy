using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Gscoy.WeChat.Biz;
using Gscoy.WeChat.Model.UserInfo;
using Microsoft.VisualStudio.TestTools.UnitTesting;
namespace Gscoy.WeChat.Biz.Tests
{
    [TestClass()]
    public class UserInfoBizTests
    {
        UserInfoBiz user = new UserInfoBiz();
        [TestMethod()]
        public void SearchAllGroupTest()
        {
            var entity = user.SearchAllGroup();
            Assert.Fail();
        }

        [TestMethod()]
        public void SearchGroupByIDTest()
        {
            var openid = new UserOpenID();
            openid.openid = "ohKqXt752ft8dQQCiQ3dRgcDsFDE";
            var entity = user.SearchGroupByID(openid);
            Assert.Fail();
        }

        [TestMethod()]
        public void ModifyUserGroupTest()
        {
            var openid = new UserOpenID();
            openid.openid = "ohKqXt752ft8dQQCiQ3dRgcDsFDE";
            openid.to_groupid = 1;
            var e = user.ModifyUserGroup(openid);
            Assert.Fail();
        }
    }
}
