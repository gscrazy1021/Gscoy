using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Gscoy.WeChat.Model.UserInfo;
using Gscoy.Common;

namespace Gscoy.WeChat.Biz
{
    public class UserInfoBiz
    {
        BaseAPI api = new BaseAPI();
        /// <summary>
        /// 创建分组
        /// </summary>
        /// <param name="group"></param>
        /// <returns></returns>
        public CreateGroupInfo CreateGroup(CreateGroupInfo group)
        {
            var requestJson = group.ToJson();
            var token = api.GetAccessToken();
            var url = string.Format("https://api.weixin.qq.com/cgi-bin/groups/create?access_token={0}", token);
            var json = HttpHelper.GetHtml(url, requestJson, true);
            var entity = json.FromJson<CreateGroupInfo>();
            return entity;
        }
        /// <summary>
        /// 查询所有分组
        /// </summary>
        /// <returns></returns>
        public SearchGroupList SearchAllGroup()
        {
            var token = api.GetAccessToken();
            var url = string.Format(@"https://api.weixin.qq.com/cgi-bin/groups/get?access_token={0}", token);
            var json = HttpHelper.GetHtml(url);
            var entity = json.FromJson<SearchGroupList>();
            return entity;
        }
        /// <summary>
        /// 查询用户所在分组
        /// </summary>
        /// <param name="openid"></param>
        /// <returns></returns>
        public SearchGroup SearchGroupByID(UserOpenID openid)
        {
            var requestJson = openid.ToJson();
            var token = api.GetAccessToken();
            var url = string.Format(@"https://api.weixin.qq.com/cgi-bin/groups/getid?access_token={0}", token);
            var json = HttpHelper.GetHtml(url, requestJson, true);
            var entity = json.FromJson<SearchGroup>();
            return entity;
        }
        /// <summary>
        /// 修改分组名
        /// </summary>
        /// <param name="openid"></param>
        /// <returns></returns>
        public bool ModifyUserGroup(UserOpenID openid)
        {
            var requestJson = openid.ToJson();
            var token = api.GetAccessToken();
            var url = string.Format(@"https://api.weixin.qq.com/cgi-bin/groups/update?access_token={0}", token);
            var json = HttpHelper.GetHtml(url, requestJson, true);
            var entity = json.FromJson<ResopnseErrorMsg>();
            return entity.errmsg == "ok";
        }
    }
}
