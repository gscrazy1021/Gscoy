using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gscoy.WeChat.Biz.Response
{
    /// <summary>
    /// 多媒体文件下载回应信息
    /// </summary>
    public class DownloadMediaResponse : MpResponse
    {
        /// <summary>
        /// 下载文件保存路径
        /// </summary>
        public string SaveFileName { get; set; }
    }
}