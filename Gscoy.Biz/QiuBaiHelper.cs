using Gscoy.Common;
using Gscoy.DataModel.QiuBai;
using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gscoy.Biz
{
    public class QiuBaiHelper
    {
        public List<QiuBaiEntity> GetContentList()
        {
            string url = "http://www.qiushibaike.com/";
            string div = "article block untagged mb15";
            List<QiuBaiEntity> list = new List<QiuBaiEntity>();
            HtmlDocument htmlDoc = new HtmlDocument
            {
                OptionAddDebuggingAttributes = false,
                OptionAutoCloseOnEnd = true,
                OptionFixNestedTags = true,
                OptionReadEncoding = true
            };
            HtmlHelper http = new HtmlHelper();
            var resp = http.GetHTML(url);
            //resp.Html
            htmlDoc.LoadHtml(resp.Html);
            HtmlNodeCollection categoryNodeList = htmlDoc.DocumentNode.SelectNodes("//div[@class=\"article block untagged mb15\"]");
            HtmlNode node = null;
            foreach (var categoryNode in categoryNodeList)
            {
                node = HtmlNode.CreateNode(categoryNode.OuterHtml);
                QiuBaiEntity entity = new QiuBaiEntity();
                HtmlNode temp = null;
                temp = node.SelectSingleNode("//div[@class=\"author clearfix\"]/a/img");
                if (temp != null)
                {
                    entity.AuthorUrl = temp.Attributes["src"].Value;
                    entity.Author = temp.Attributes["alt"].Value;
                }
                temp = node.SelectSingleNode("//div[@class=\"content\"]");
                if (temp != null)
                {
                    entity.Content = temp.InnerText.Replace("\n", "");
                    entity.PublishDate = temp.Attributes["title"].Value;
                }
                temp = node.SelectSingleNode("//div[@class=\"thumb\"]/a/img");
                if (temp != null)
                    entity.ContentImgUrl = temp.Attributes["src"].Value;
                list.Add(entity);
            }
            return list;
        }
    }
}
