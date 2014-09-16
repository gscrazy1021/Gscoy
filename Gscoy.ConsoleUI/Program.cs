using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Gscoy.Biz;
using Gscoy.Common;

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

            var result = WeatherHelper.GetWeatherInfo("101010100");
            Console.ReadKey();
        }
    }
}
