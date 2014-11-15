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
using System.Data;
using System.Text.RegularExpressions;
using System.Xml;
using Newtonsoft.Json;
using Gscoy.DataModel.Baidu.LBS;
using Gscoy.Biz.Baidu;
using Gscoy.WeChat.Model;

namespace Gscoy.ConsoleUI
{
    class Program
    {
        static void Main(string[] args)
        {
            for (int i = 1; i < 727; i++)
            {
                string url = string.Format("http://www.qingdou.net/chapter_26698_{0}.html", i);
                Gscoy.Common.HtmlHelper html = new HtmlHelper();
                var result = html.GetHTML(url);
                //result.Html
                var sss = Html2Article.GetArticle(result.Html);
                var content = Regex.Replace(sss.Content, "言情后花园，提供精品言情小说阅读！[0-9A-Za-z]*", "").Replace("/\">言情小说，尽在言情后花园。请记住本站：www.yqhhy.cc", "").Replace("(四库书www.sikushu.com)上一章返回目录下一章小提示：按 回车[Enter]键 返回书目，按 ←键 返回上一页， 按 →键 进入下一页。", "").Replace("█最/█", "").Replace("█新/█", "").Replace("█章/█", "").Replace("█节/█", "").Replace("█最/█", "").Replace("█最/█", "").Replace("█最/█", "").Replace("█最/█", "").Replace("█最/█", "").Replace("█最/█", "");
                string title = sss.Title.Replace("_不死武尊_青豆小说", "");

                DBHelper db = DBHelper.GetDBHelper(OperationType.Write);
                db.Execute(string.Format("insert into BuSiWuZun (ChaterID,Content,Title,PublishDate) values ({0},'{1}','{2}','{3}')", i, content, title, DateTime.Now.ToString("yyyy-MM-dd dd:HH:mm")));
                Console.WriteLine(i);
            }
        }
    }
}
