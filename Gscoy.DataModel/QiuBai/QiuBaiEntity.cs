using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gscoy.DataModel.QiuBai
{
    public class QiuBaiEntity
    {
        /// <summary>
        /// 作者头像
        /// </summary>
        public string AuthorUrl { get; set; }
        /// <summary>
        /// 作者昵称
        /// </summary>
        public string Author { get; set; }
        /// <summary>
        /// 正文
        /// </summary>
        public string Content { get; set; }
        /// <summary>
        /// 发布时间
        /// </summary>
        public string PublishDate { get; set; }
        /// <summary>
        /// 正文的图片
        /// </summary>
        public string ContentImgUrl { get; set; }
    }
}
