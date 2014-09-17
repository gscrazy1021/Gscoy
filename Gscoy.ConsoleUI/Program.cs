using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Gscoy.Biz;
using Gscoy.Common;
using Gscoy.DataModel.Blog;

namespace Gscoy.ConsoleUI
{
    class Program
    {
        static void Main(string[] args)
        {
            //for (int i = 1234; i < 1270; i++)
            //{
            //    var str = HttpHelper.GetHtml(string.Format("http://www.jiexiuzhuan.com/{0}.html", i));
            //    var content = Html2Article.GetArticle(str);
            //    FileHelper.WriteFile(@"F:\WebSite\jxz.txt", string.Format("\n\r{0}\n\r{1}", content.Title.Replace("_劫修传|劫修传全集txt下载|劫修传最新章节|劫修传无弹窗", ""), content.Content), true);
            //    Console.WriteLine(i);
            //}
            ////FileHelper.WriteFile(@"f:\1\2.txt", "123abc");
            ArticleEntity ae = new ArticleEntity();
            ae.ArticleID = 10001;
            ae.Title = "文章标题";
            ae.Content = "内容";
            ae.ArticleTag = "测试";
            ae.ArticleType = "随笔";
            ae.CreateTime = DateTime.Now;
            ae.ModifyTime = DateTime.Now;
            string json = ae.ToJson();
            Console.WriteLine(json);
            ArticleEntity entity = new ArticleEntity();
            entity.ArticleID = 10002;
            entity.Title = "{标题}";
            entity.Content = "\"{内。容;}\"";
            entity.ArticleTag = "测试";
            entity.ArticleType = "随笔";
            entity.CreateTime = DateTime.Now;
            entity.ModifyTime = DateTime.Now;
            json = entity.ToJson();
            Console.WriteLine(json);
            var r = json.FromJson<ArticleEntity>();
            Console.WriteLine(r.Content);
            Console.ReadKey();
        }
    }
}
