using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Gscoy.Biz;
using Gscoy.Common;
using Gscoy.DataModel.Blog;
using Gscoy.DBase;
using Gscoy.EF.Sqlite;
using Newtonsoft.Json.Linq;

namespace Gscoy.ConsoleUI
{
    class Program
    {
        static void Main(string[] args)
        {
            #region MyRegion
            //DBHelper db = DBHelper.GetDBHelper(OperationType.Write);
            //List<JieXiuZhuan> list = new List<JieXiuZhuan>();
            //for (int i = 1; i <= 1342; i++)
            //{
            //    var str = HttpHelper.GetHtml(string.Format("http://www.jiexiuzhuan.com/{0}.html", i));
            //    var content = Html2Article.GetArticle(str);
            //    FileHelper.WriteFile(@"F:\WebSite\jxz.txt", string.Format("\n\r{0}\n\r{1}", content.Title.Replace("_劫修传|劫修传全集txt下载|劫修传最新章节|劫修传无弹窗", ""), content.Content), true);
            //    Console.WriteLine(i);
            //    JieXiuZhuan entity = new JieXiuZhuan();
            //    entity.InnerID = i;
            //    entity.Title = content.Title.Replace("_劫修传|劫修传全集txt下载|劫修传最新章节|劫修传无弹窗", "");
            //    entity.Content = content.Content;
            //    entity.Html = content.ContentWithTags;
            //    list.Add(entity);
            //    db.Execute("insert into jiexiuzhuan(innerid,title,content,html) values (@innerid,@title,@content,@html)", new { innerid = entity.InnerID, title = entity.Title, content = entity.Content, html = entity.Html });
            //}
            ////from l in list select new {innerid=l.};
            ////db.Execute("insert into jiexiuzhuan(innerid,title,content,html) values (@innerid,@title,@content,@html)", new { innerid = entity.InnerID, title = entity.Title, content = entity.Content, html = entity.Html });

            //FileHelper.WriteFile(@"f:\1\2.txt", "123abc");

            ////ArticleEntity ae = new ArticleEntity();
            ////ae.ArticleID = 10001;
            ////ae.Title = "文章标题";
            ////ae.Content = "内容";
            ////ae.ArticleTag = "测试";
            ////ae.ArticleType = "随笔";
            ////ae.CreateTime = DateTime.Now;
            ////ae.ModifyTime = DateTime.Now;
            ////string json = ae.ToJson();
            ////Console.WriteLine(json);
            ////ArticleEntity entity = new ArticleEntity();
            ////entity.ArticleID = 10002;
            ////entity.Title = "{标题}";
            ////entity.Content = "\"{内。容;}\"";
            ////entity.ArticleTag = "测试";
            ////entity.ArticleType = "随笔";
            ////entity.CreateTime = DateTime.Now;
            ////entity.ModifyTime = DateTime.Now;
            ////json = entity.ToJson();
            ////Console.WriteLine(json);
            ////var r = json.FromJson<ArticleEntity>();
            ////Console.WriteLine(r.Content);
            ////Console.ReadKey(); 
            #endregion

            string str = "{\"3\":123,body:456,list:{title:'abc',body:'what'}}";
            JObject o = JObject.Parse(str);
            IEnumerable<JProperty> properties = o.Properties();
            foreach (var item in properties)
            {
                Console.WriteLine("{0}--{1}--{2}", item.Name, item.Value, item.Type);
                if (item.HasValues)
                {
                    JObject jobj = JObject.Parse(item.ToString());
                    foreach (var e in jobj.Properties())
                    {
                        Console.WriteLine("{0}--{1}--{2}", e.Name, e.Value, e.Type);
                    }
                }
            }
            var entity = properties.Where(m => m.Name == "title").FirstOrDefault();
            if (entity == null) return;
            Console.WriteLine(entity.Value);
        }
    }

    class JieXiuZhuan
    {
        public int InnerID { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public string Html { get; set; }
    }
}
