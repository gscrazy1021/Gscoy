using Gscoy.Common;
using Gscoy.Data.CnBlogs;
using Gscoy.DataModel.CnBlogs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;

namespace Gscoy.Biz.CnBlogs
{
    public class ReadNewsRssBiz
    {
        ReadNewsRssDao dao = new ReadNewsRssDao();
        /// <summary>
        /// 读取博客园的新闻rss
        /// </summary>
        public IList<NewsXmlEntity> GetNewsRss()
        {
            return dao.GetNewsRss();
        }
        /// <summary>
        /// 插入数据到远程数据库
        /// </summary>
        /// <param name="list"></param>
        /// <returns></returns>
        public bool InsertNews(IList<NewsXmlEntity> list)
        {
            return true;
        }
    }
}
