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
            //string time = string.Format("Sat, 05 Nov 2005 14:06:25 GMT");
            //DateTime tt = new DateTime();
            //DateTime.TryParse(time, out tt);
            //FTPHelper ftp = new FTPHelper("112.124.182.184", "", "hxw0070148", "lengshou01");
            //var list = ftp.GetFilesDetailList();
            //foreach (var item in list)
            //{
            //    try
            //    {
            //        var detail = ftp.GetFileList(item);
            //    }
            //    catch (Exception ex)
            //    {

            //    }
            //}
            HtmlAgilityPackHelper helper = new HtmlAgilityPackHelper();
            var str = helper.GetContentList("http://www.qiushibaike.com/", "article block untagged mb15");
            var json = str.ToJson();
            Console.WriteLine(json);
            Console.ReadKey();
        }
    }
}
