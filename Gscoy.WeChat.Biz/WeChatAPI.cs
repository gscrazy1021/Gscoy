using Gscoy.Common;
using Gscoy.WeChat.Biz.Request;
using Gscoy.WeChat.Biz.Response;
using Gscoy.WeChat.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gscoy.WeChat.Biz
{
    public class WechatAPI
    {
        string token = ConfigHelper.GetConfig("WeChatToken", "");
        public void GetReciveMsg()
        {
            ReceiveMessageBase msg = MessageHandler.ConvertMsgToObject(token);
            if (msg is TextReceiveMessage)
            {
                MessageHandler.SendTextReplyMessage(msg.ToUserName, msg.FromUserName, "text");
            }
            if (msg is ImageReceiveMessage)
            {
                MessageHandler.SendImageReplyMessage(token, msg.ToUserName, msg.FromUserName, "img");
            }
            if (msg is VideoReceiveMessage)
            {
                MessageHandler.SendVideoReplyMessage(token, msg.ToUserName, msg.FromUserName, "videotitle", "videodescription", "videopath");
            }
            if (msg is VideoReceiveMessage)
            {
                MessageHandler.SendVoiceReplyMessage(token, msg.ToUserName, msg.FromUserName, "voicepath");
            }
            if (msg is LocationReceiveMessage)
            {
                var entity = msg as LocationReceiveMessage;
                MessageHandler.SendTextReplyMessage(msg.ToUserName, msg.FromUserName, string.Format("{0}--{1}--{2}--{3}", entity.Location_X, entity.Location_Y, entity.MessageBody, entity.Scale));
            }
            if (msg is MenuEventMessage)
            {
                List<NewsReplyMessageItem> list = new List<NewsReplyMessageItem>() { 
                new NewsReplyMessageItem(){Description="法国葡萄酒产区介绍",PicUrl="http://api.hongjiubaike.com/userfiles/b_large_GHlG_4deb000001631263.jpg",Title="法国葡萄酒产区介绍",Url="http://api.hongjiubaike.com/2013/437.html"},new NewsReplyMessageItem(){Description="法国葡萄酒产区介绍",PicUrl="http://api.hongjiubaike.com/userfiles/b_large_GHlG_4deb000001631263.jpg",Title="法国葡萄酒产区介绍",Url="http://api.hongjiubaike.com/2013/437.html"},new NewsReplyMessageItem(){Description="法国葡萄酒产区介绍",PicUrl="http://api.hongjiubaike.com/userfiles/b_large_GHlG_4deb000001631263.jpg",Title="法国葡萄酒产区介绍",Url="http://api.hongjiubaike.com/2013/437.html"},new NewsReplyMessageItem(){Description="法国葡萄酒产区介绍",PicUrl="http://api.hongjiubaike.com/userfiles/b_large_GHlG_4deb000001631263.jpg",Title="法国葡萄酒产区介绍",Url="http://api.hongjiubaike.com/2013/437.html"},new NewsReplyMessageItem(){Description="法国葡萄酒产区介绍",PicUrl="http://api.hongjiubaike.com/userfiles/b_large_GHlG_4deb000001631263.jpg",Title="法国葡萄酒产区介绍",Url="http://api.hongjiubaike.com/2013/437.html"},new NewsReplyMessageItem(){Description="法国葡萄酒产区介绍",PicUrl="http://api.hongjiubaike.com/userfiles/b_large_GHlG_4deb000001631263.jpg",Title="法国葡萄酒产区介绍",Url="http://api.hongjiubaike.com/2013/437.html"},new NewsReplyMessageItem(){Description="法国葡萄酒产区介绍",PicUrl="http://api.hongjiubaike.com/userfiles/b_large_GHlG_4deb000001631263.jpg",Title="法国葡萄酒产区介绍",Url="http://api.hongjiubaike.com/2013/437.html"}
                };
                MessageHandler.SendNewsReplyMessage(token, msg.ToUserName, msg.FromUserName, list);
            }
        }

        public void Valid()
        {
            MessageHandler.Valid(token);
        }

        private string GetAccessToken()
        {
            string appId = ConfigHelper.GetConfig("WechatAppID", "");
            string appSecret = ConfigHelper.GetConfig("WechatAppSecret", "");
            IMpClient mpClient = new MpClient();
            AccessTokenGetRequest request = new AccessTokenGetRequest()
            {
                AppIdInfo = new AppIdInfo() { AppID = appId, AppSecret = appSecret }
            };
            AccessTokenGetResponse response = mpClient.Execute(request);
            if (response.IsError)
            {
                Console.WriteLine("获取AccessToken发生错误，错误编码为：{0}，错误消息为：{1}", response.ErrInfo.ErrCode, response.ErrInfo.ErrMsg);
                return string.Empty;
            }
            else
            {
                Console.WriteLine("获取到AccessToken，值为：{0}，有效期：{1}秒", response.AccessToken.AccessToken, response.AccessToken.ExpiresIn);
                return response.AccessToken.AccessToken;
            }
        }
    }
}
