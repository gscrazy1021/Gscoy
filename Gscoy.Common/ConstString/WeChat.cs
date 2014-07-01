using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gscoy.Common.ConstString
{
    public static class WeChat
    {
        /// <summary>
        /// 获取token的微信地址
        /// {0} --appid
        /// {1} --secret
        /// </summary>
        public const string GetTokenString = "https://api.weixin.qq.com/cgi-bin/token?grant_type=client_credential&appid={0}&secret={1}";

        /// <summary>
        /// 上传文件接口
        /// {0} --accesstoken
        /// {1} --type上传类型:媒体文件类型，分别有图片（image）、语音（voice）、视频（video）和缩略图（thumb） 
        /// </summary>
        public const string UploadFileString = "http://file.api.weixin.qq.com/cgi-bin/media/upload?access_token={0}&type={1}";
    }
}
