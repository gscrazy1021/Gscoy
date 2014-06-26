using Microsoft.Security.Application;
using System;
using System.IO;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;

namespace Gscoy.Common
{
    public class HtmlHelper
    {
        /// <summary>
        /// 过滤跨站脚本
        /// 注意：该方法会清除掉style 所有事件、及img标签等
        /// </summary>
        /// <param name="str">待的字符串</param>
        /// <returns>过滤后的字符串</returns>
        public static string FilterXSS(string str)
        {
            return Sanitizer.GetSafeHtmlFragment(str);
            //return Sanitizer.GetSafeHtml(str);
        }

        public static string RemoveScriptTags(string input)
        {
            return RemoveHtmlTags(input, true);
        }

        /// <summary>
        /// 删除style标签内容
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static string RemoveStyleTags(string input)
        {
            input = Regex.Replace(input, @"<style(.|\n)+</style>", "", RegexOptions.IgnoreCase);
            return input;
        }

        /// <summary>
        /// 删除quote标签内容
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static string RemoveQuoteTags(string input)
        {
            input = Regex.Replace(input, @"\[quote\][^\[]+\[/quote\]", "", RegexOptions.IgnoreCase);
            return input;
        }

        public static string RemoveScriptNameFromBookmarks(string input, string url)
        {
            input = input.Replace("href=\"" + url, "href=\"");
            input = input.Replace("href=" + url, "href=");
            return input;
        }

        /// <summary>
        /// 删除各种html标签
        /// </summary>
        /// <param name="input"></param>
        /// <param name="removescript"></param>
        /// <returns></returns>
        public static string RemoveHtmlTags(string input, bool removescript)
        {
            //remove <html>
            input = Regex.Replace(input, @"<HTML>", "", RegexOptions.IgnoreCase);
            //remove <HEAD>
            input = Regex.Replace(input, @"<HEAD>(.|\n)>+</HEAD>", "", RegexOptions.IgnoreCase);
            //remove <META>
            input = Regex.Replace(input, @"<META[^>]+>", "", RegexOptions.IgnoreCase);
            //remove <title>
            input = Regex.Replace(input, @"<title(.|\n)>+</title>", "", RegexOptions.IgnoreCase);
            //remove <body>
            input = Regex.Replace(input, @"<body>", "", RegexOptions.IgnoreCase);
            //remove </body>
            input = Regex.Replace(input, @"</body>", "", RegexOptions.IgnoreCase);
            //remove </HTML>
            input = Regex.Replace(input, @"</HTML>", "", RegexOptions.IgnoreCase);
            //remove <frameset>
            input = Regex.Replace(input, @"<frameset(.|\n)+</frameset>", "", RegexOptions.IgnoreCase);
            //remove <frame>
            input = Regex.Replace(input, @"<frame[^>]+>", "", RegexOptions.IgnoreCase);
            if (removescript)
            {
                input = Regex.Replace(input, @"<script(.|\n)+</script>", "", RegexOptions.IgnoreCase);
            }
            else
            {
                //remove  window.location.href
                input = Regex.Replace(input, @"window.location.href", "", RegexOptions.IgnoreCase);
                //remove  window.location.href
                input = Regex.Replace(input, @"location.href", "", RegexOptions.IgnoreCase);
            }
            return input;
        }

        /// <summary>
        /// 删除各种html和script标签
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static string RemoveHtmlAndScriptTags(string input)
        {
            //remove <script>...</ script>
            input = Regex.Replace(input, "<script[^>]+>[\\s\\S]*?/script>", "", RegexOptions.IgnoreCase | RegexOptions.Multiline);
            //remove <...>
            input = Regex.Replace(input, "<[^>]+>", "", RegexOptions.IgnoreCase | RegexOptions.Multiline);

            return input.Trim();
        }

        /// <summary>
        /// 删除各种事件标签
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static string RemoveClickTags(string input)
        {
            //remove onclick
            input = Regex.Replace(input, @"onclick\s*=(.*)\)", "", RegexOptions.IgnoreCase);
            //remove onkeydown
            input = Regex.Replace(input, @"onkeydown\s*=(.*)\)", "", RegexOptions.IgnoreCase);
            //remove onmousedown
            input = Regex.Replace(input, @"onmousedown\s*=(.*)\)", "", RegexOptions.IgnoreCase);
            //remove onmousemove
            input = Regex.Replace(input, @"onmousemove\s*=(.*)\)", "", RegexOptions.IgnoreCase);
            //remove onmouseover
            input = Regex.Replace(input, @"onmouseover\s*=(.*)\)", "", RegexOptions.IgnoreCase);
            return input;
        }

        /// <summary>
        /// 删除style、html、事件标签
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static string RemoveCode(string input)
        {
            //input = cls_common.RemoveScriptTags(input); 合并成一个方法
            input = RemoveStyleTags(input);
            input = RemoveHtmlTags(input, true);
            input = RemoveClickTags(input);
            return input;
        }

        /// <summary>
        /// 删除一些截断HTML后无效的代码，例如<table></table>
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static string ShouldCutHtml(string str)
        {
            string lastHtml = "";
            int index = str.LastIndexOf("</");
            if (index >= 0)
            {
                return str;
            }
            if (str.LastIndexOf(">") != str.Length - 1)
            {
                return str;
            }
            index = str.LastIndexOf("<");
            if (index >= 0)
            {
                string efficiencySign = str.Substring(index + 1);
                int spacing = efficiencySign.IndexOf(" ");
                int length = efficiencySign.Length;
                if (spacing > 0)
                {
                    lastHtml = efficiencySign.Substring(0, spacing);
                }
                else
                {
                    lastHtml = efficiencySign.Substring(0, length - 1);
                    //lastHtml=str.Remove(lastHtml.Length-1,1);
                }
            }

            switch (lastHtml.ToLower())
            {
                case "table"://暂时只有table,tr
                    str = str.Remove(index, str.Length - index);
                    break;
                case "tr":
                    str = str.Remove(index, str.Length - index);
                    str = ShouldCutHtml(str);
                    break;
            }
            return str;
        }

        /// <summary>
        /// 修复html代码
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static string RepairHTML(string str)
        {
            int idx2 = str.LastIndexOf('>');
            int idx1 = str.LastIndexOf('<');
            string lose = "";
            if (idx1 > idx2 && idx1 > -1)
            {
                lose = str.Substring(str.Length - 1) + ">";
                str = str + lose;
                idx2 = str.LastIndexOf('>');
            }
            int result = 0;
            int len = str.Length;
            string add = "";
            if (idx1 == -1)
            {
                return "";
            }
            int idx3 = -1;
            string ct = str.Substring(idx1 + 1, idx2 - idx1 - 1);
            string ctlast;
            if (ct.IndexOf("/") == 0)
            {
                string tag = "<" + str.Substring(idx1 + 2, idx2 - idx1 - 2);
                string tt = str;//中间变量
                while (true)
                {
                    idx3 = tt.LastIndexOf(tag);
                    ctlast = tt.Substring(idx3);

                    result = IsTure(ctlast, ct);
                    if (result <= 0 || result == ctlast.LastIndexOf(ct))
                    {
                        break;
                    }
                    else
                    {
                        tt = tt.Remove(idx3, result + 6);
                    }
                }
            }
            else if (ct.IndexOf("/") != 0)
            {
                int space = ct.IndexOf(' ');
                string repairdata = ct;
                if (space > 0)
                {
                    repairdata = ct.Substring(0, space);
                }
                if (ct.Trim() != "br" && ct.Trim() != "hr" && repairdata.Trim() != "img" && repairdata.Trim() != "param" && ct.Substring(ct.Length - 1) != "/")
                {
                    add += lose + "</" + repairdata + ">";
                }
            }

            if (idx3 == -1)
            {
                idx3 = idx1;
            }
            string strnew = str.Substring(0, idx3);

            add += RepairHTML(strnew);
            return add;

        }

        /// <summary>
        /// 获取目标字符串第一次出现的位置
        /// </summary>
        /// <param name="str"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        public static int IsTure(string str, string key)
        {
            return str.IndexOf(key);
        }

        /// <summary>
        /// 将换行符等替换为空格
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static string RemoveWhiteSpace(string input)
        {
            input = input.Replace("\n", "  ");
            input = input.Replace("\r", "  ");
            input = input.Replace("\t", "  ");
            while (input.IndexOf("    ") != -1)
            {
                input = input.Replace("    ", "  ");
            }
            return input;
        }

        /// <summary>
        /// POST请求传递页面数据
        /// </summary>
        /// <param name="posturl"></param>
        /// <param name="postData"></param>
        /// <returns></returns>
        public static string PostPageData(string posturl, string postData)
        {
            Stream outstream = null;
            Stream instream = null;
            StreamReader sr = null;
            HttpWebResponse response = null;
            HttpWebRequest request = null;
            Encoding encoding = Encoding.UTF8;
            byte[] data = encoding.GetBytes(postData);
            // 准备请求...
            try
            {
                // 设置参数
                request = WebRequest.Create(posturl) as HttpWebRequest;
                CookieContainer cookieContainer = new CookieContainer();
                request.CookieContainer = cookieContainer;
                request.AllowAutoRedirect = true;
                request.Method = "POST";
                request.ContentType = "application/x-www-form-urlencoded";
                request.ContentLength = data.Length;
                outstream = request.GetRequestStream();
                outstream.Write(data, 0, data.Length);
                outstream.Close();
                //发送请求并获取相应回应数据
                response = request.GetResponse() as HttpWebResponse;
                //直到request.GetResponse()程序才开始向目标网页发送Post请求
                instream = response.GetResponseStream();
                sr = new StreamReader(instream, encoding);
                //返回结果网页（html）代码
                string content = sr.ReadToEnd();
                string err = string.Empty;
                //Response.Write(content);
                HttpContext.Current.Response.Write(content);
                return content;
            }
            catch (Exception ex)
            {
                string err = ex.Message;
                return string.Empty;
            }
        }

        /// <summary>
        /// GET请求传递页面数据
        /// </summary>
        /// <param name="posturl"></param>
        /// <returns></returns>
        public static string GetPageData(string posturl)
        {
            Stream instream = null;
            StreamReader sr = null;
            HttpWebResponse response = null;
            HttpWebRequest request = null;
            Encoding encoding = Encoding.UTF8;
            // 准备请求...
            try
            {
                // 设置参数
                request = WebRequest.Create(posturl) as HttpWebRequest;
                CookieContainer cookieContainer = new CookieContainer();
                request.CookieContainer = cookieContainer;
                request.AllowAutoRedirect = true;
                request.Method = "GET";
                request.ContentType = "application/x-www-form-urlencoded";
                //发送请求并获取相应回应数据
                response = request.GetResponse() as HttpWebResponse;
                //直到request.GetResponse()程序才开始向目标网页发送Post请求
                instream = response.GetResponseStream();
                sr = new StreamReader(instream, encoding);
                //返回结果网页（html）代码
                string content = sr.ReadToEnd();
                string err = string.Empty;
                //Response.Write(content);
                HttpContext.Current.Response.Write(content);
                return content;
            }
            catch (Exception ex)
            {
                string err = ex.Message;
                return string.Empty;
            }
        }
    }
}
