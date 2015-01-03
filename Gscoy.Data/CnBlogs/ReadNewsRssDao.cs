using Gscoy.Common;
using Gscoy.DataModel.CnBlogs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;

namespace Gscoy.Data.CnBlogs
{
    public class ReadNewsRssDao
    {
        /// <summary>
        /// 读取博客园的新闻rss
        /// </summary>
        public IList<NewsXmlEntity> GetNewsRss()
        {
            string key = "cnblogs_news";
            IList<NewsXmlEntity> rssXmls;
            AspnetCache cache = AspnetCache.Instance;
            rssXmls = cache.Get<IList<NewsXmlEntity>>(key);
            if (rssXmls != null && rssXmls.Count > 0)
            {
                return rssXmls;
            }
            rssXmls = new List<NewsXmlEntity>();
            string cnblogNewsRssPath = ConfigHelper.GetConfig("cnblog-news");
            XmlDocument doc = new XmlDocument();
            try
            {
                if (string.IsNullOrEmpty(cnblogNewsRssPath)) return null;
                //加载Xml文件  
                doc.Load(cnblogNewsRssPath);
                //根结点
                XmlNode dataTableSettingsNode = doc.SelectSingleNode("rss");
                if (dataTableSettingsNode != null)
                {
                    XmlNode dataCellsNode = dataTableSettingsNode.SelectSingleNode("channel");

                    if (dataCellsNode != null)
                    {
                        XmlNodeList dataCellNode = dataCellsNode.SelectNodes("item");

                        if (dataCellNode != null)
                            foreach (XmlNode node in dataCellNode)
                            {
                                NewsXmlEntity rssXml = new NewsXmlEntity();
                                var title = node.SelectSingleNode("title");
                                var link = node.SelectSingleNode("link");
                                var pubdate = node.SelectSingleNode("pubDate");
                                var guid = node.SelectSingleNode("guid");
                                var description = node.SelectSingleNode("description");
                                if (title != null)
                                    rssXml.Title = title.InnerText;
                                if (link != null)
                                    rssXml.Link = link.InnerText;
                                if (pubdate != null)
                                {
                                    DateTime time = new DateTime();
                                    DateTime.TryParse(pubdate.InnerText, out time);
                                    rssXml.PubDate = time.ToString("yyyy-MM-dd HH:mm:ss");
                                }
                                if (guid != null)
                                    rssXml.Guid = guid.InnerText;
                                if (description != null)
                                {
                                    rssXml.Content = description.InnerText.Replace(string.Format("<p><a href=\"{0}\" target=\"_blank\">本文链接</a></p>", rssXml.Link), "");
                                    int start = rssXml.Content.IndexOf("<p>");
                                    int end = rssXml.Content.IndexOf("</p>");
                                    rssXml.ShotCut = rssXml.Content.Substring(start, end - start > 0 ? end - start : 0);
                                }
                                rssXmls.Add(rssXml);
                            }
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
            cache.Set(key, rssXmls, DateTime.Now.AddHours(1));
            return rssXmls;
        }
    }
}
