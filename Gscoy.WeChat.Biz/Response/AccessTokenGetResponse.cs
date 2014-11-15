using Gscoy.WeChat.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gscoy.WeChat.Biz.Response
{
    /// <summary>
    /// access_token请求回应信息
    /// </summary>
    public class AccessTokenGetResponse : MpResponse 
    {
        /// <summary>
        /// AccessToken
        /// </summary>
        public AccessTokenInfo AccessToken { get; set; }
    }
}