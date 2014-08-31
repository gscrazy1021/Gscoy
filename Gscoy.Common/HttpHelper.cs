﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Web;

namespace Gscoy.Common
{
    public class HttpHelper
    {
        #region 变量定义
        private static CookieContainer cc = new CookieContainer();
        private static string contentType = "application/x-www-form-urlencoded";
        private static string accept = "image/gif, image/x-xbitmap, image/jpeg, image/pjpeg, application/x-shockwave-flash, application/x-silverlight, application/vnd.ms-excel, application/vnd.ms-powerpoint, application/msword, application/x-ms-application, application/x-ms-xbap, application/vnd.ms-xpsdocument, application/xaml+xml, application/x-silverlight-2-b1, */*";
        private static string userAgent = "Mozilla/4.0 (compatible; MSIE 7.0; Windows NT 5.1; .NET CLR 2.0.50727; .NET CLR 3.0.04506.648; .NET CLR 3.5.21022)";
        private static Encoding encoding = Encoding.GetEncoding("utf-8");
        private static int delay = 1000;
        private static int maxTry = 300;
        private static int currentTry = 0;
        #endregion

        #region 变量赋值
        /// <summary> 
        /// Cookie
        /// </summary> 
        public static CookieContainer CookieContainer
        {
            get
            {
                return cc;
            }
        }

        /// <summary> 
        /// 语言
        /// </summary> 
        /// <value></value> 
        public static Encoding Encoding
        {
            get
            {
                return encoding;
            }
            set
            {
                encoding = value;
            }
        }

        public static int NetworkDelay
        {
            get
            {
                Random r = new Random();
                return (r.Next(delay, delay * 2));
            }
            set
            {
                delay = value;
            }
        }

        public static int MaxTry
        {
            get
            {
                return maxTry;
            }
            set
            {
                maxTry = value;
            }
        }
        #endregion

        #region 获取HTML
        /// <summary>
        /// 获取HTML
        /// </summary>
        /// <param name="url">地址</param>
        /// <param name="postData">post 提交的字符串</param>
        /// <param name="isPost">是否是post</param>
        /// <param name="cookieContainer">CookieContainer</param>
        /// <returns>html </returns>
        public static string GetHtml(string url, string postData, bool isPost, CookieContainer cookieContainer)
        {
            if (string.IsNullOrEmpty(postData))
            {
                return GetHtml(url, cookieContainer);
            }

            Thread.Sleep(NetworkDelay);//等待

            currentTry++;

            HttpWebRequest httpWebRequest = null;
            HttpWebResponse httpWebResponse = null;
            try
            {
                byte[] byteRequest = Encoding.Default.GetBytes(postData);

                httpWebRequest = (HttpWebRequest)HttpWebRequest.Create(url);
                httpWebRequest.CookieContainer = cookieContainer;
                httpWebRequest.ContentType = contentType;
                httpWebRequest.ServicePoint.ConnectionLimit = maxTry;
                httpWebRequest.Referer = url;
                httpWebRequest.Accept = accept;
                httpWebRequest.UserAgent = userAgent;
                httpWebRequest.Method = isPost ? "POST" : "GET";
                httpWebRequest.ContentLength = byteRequest.Length;

                Stream stream = httpWebRequest.GetRequestStream();
                stream.Write(byteRequest, 0, byteRequest.Length);
                stream.Close();

                httpWebResponse = (HttpWebResponse)httpWebRequest.GetResponse();
                encoding = GetResponseEncoding(httpWebResponse);
                Stream responseStream = httpWebResponse.GetResponseStream();
                StreamReader streamReader = new StreamReader(responseStream, encoding);
                string html = streamReader.ReadToEnd();
                streamReader.Close();
                responseStream.Close();
                currentTry = 0;

                httpWebRequest.Abort();
                httpWebResponse.Close();

                return html;
            }
            catch (Exception e)
            {
                //Console.ForegroundColor = ConsoleColor.Red;
                //Console.WriteLine(DateTime.Now.ToString("HH:mm:ss ") + e.Message);
                //Console.ForegroundColor = ConsoleColor.White;

                if (currentTry <= maxTry)
                {
                    GetHtml(url, postData, isPost, cookieContainer);
                }
                currentTry--;

                if (httpWebRequest != null)
                {
                    httpWebRequest.Abort();
                } if (httpWebResponse != null)
                {
                    httpWebResponse.Close();
                }
                return string.Empty;
            }
        }
        // <summary>
        /// 根据响应流获取网页的响应编码
        /// </summary>
        /// <param name="httpWebResponse"></param>
        /// <returns></returns>
        private static System.Text.Encoding GetResponseEncoding(HttpWebResponse httpWebResponse)
        {
            var encodingStr = "gb2312";
            var temp = httpWebResponse.ContentType.Split(new[] { ';' }, StringSplitOptions.RemoveEmptyEntries);
            if (temp.Length >= 2)
            {
                var t = temp[1].Split(new[] { '=' }, StringSplitOptions.RemoveEmptyEntries);
                if (t.Length >= 2) encodingStr = t[1];
            }
            if (string.IsNullOrEmpty(encodingStr)) encodingStr = "gb2312";//先取contenttype的编码，若为空，则默认gb2312
            var e = Encoding.GetEncoding(encodingStr);
            return e;
        }
        /// <summary>
        /// 获取HTML
        /// </summary>
        /// <param name="url">地址</param>
        /// <param name="cookieContainer">CookieContainer</param>
        /// <returns>HTML</returns>
        public static string GetHtml(string url, CookieContainer cookieContainer)
        {
            Thread.Sleep(NetworkDelay);

            currentTry++;
            HttpWebRequest httpWebRequest = null;
            HttpWebResponse httpWebResponse = null;
            try
            {

                httpWebRequest = (HttpWebRequest)HttpWebRequest.Create(url);
                httpWebRequest.CookieContainer = cookieContainer;
                httpWebRequest.ContentType = contentType;
                httpWebRequest.ServicePoint.ConnectionLimit = maxTry;
                httpWebRequest.Referer = url;
                httpWebRequest.Accept = accept;
                httpWebRequest.UserAgent = userAgent;
                httpWebRequest.Method = "GET";

                httpWebResponse = (HttpWebResponse)httpWebRequest.GetResponse();
                encoding = GetResponseEncoding(httpWebResponse);
                Stream responseStream = httpWebResponse.GetResponseStream();
                StreamReader streamReader = new StreamReader(responseStream, encoding);
                string html = streamReader.ReadToEnd();
                streamReader.Close();
                responseStream.Close();

                currentTry--;

                httpWebRequest.Abort();
                httpWebResponse.Close();

                return html;
            }
            catch (Exception e)
            {
                //Console.ForegroundColor = ConsoleColor.Red;
                //Console.WriteLine(DateTime.Now.ToString("HH:mm:ss ") + e.Message);
                //Console.ForegroundColor = ConsoleColor.White;

                if (currentTry <= maxTry)
                {
                    GetHtml(url, cookieContainer);
                }

                currentTry--;

                if (httpWebRequest != null)
                {
                    httpWebRequest.Abort();
                } if (httpWebResponse != null)
                {
                    httpWebResponse.Close();
                }
                return string.Empty;
            }
        }
        /// <summary>
        /// 获取HTML
        /// </summary>
        /// <param name="url">地址</param>
        /// <returns>HTML</returns>
        public static string GetHtml(string url)
        {
            return GetHtml(url, cc);
        }
        /// <summary>
        /// 获取HTML
        /// </summary>
        /// <param name="url">地址</param>
        /// <param name="postData">提交的字符串</param>
        /// <param name="isPost">是否是POST</param>
        /// <returns>HTML</returns>
        public static string GetHtml(string url, string postData, bool isPost)
        {
            return GetHtml(url, postData, isPost, cc);
        }
        /// <summary>
        /// 获取字符流
        /// </summary>
        /// <param name="url">地址</param>
        /// <param name="cookieContainer">cookieContainer</param>
        /// <returns>Stream</returns>
        public static Stream GetStream(string url, CookieContainer cookieContainer)
        {
            //Thread.Sleep(delay); 

            currentTry++;
            HttpWebRequest httpWebRequest = null;
            HttpWebResponse httpWebResponse = null;

            try
            {

                httpWebRequest = (HttpWebRequest)HttpWebRequest.Create(url);
                httpWebRequest.CookieContainer = cookieContainer;
                httpWebRequest.ContentType = contentType;
                httpWebRequest.ServicePoint.ConnectionLimit = maxTry;
                httpWebRequest.Referer = url;
                httpWebRequest.Accept = accept;
                httpWebRequest.UserAgent = userAgent;
                httpWebRequest.Method = "GET";

                httpWebResponse = (HttpWebResponse)httpWebRequest.GetResponse();
                Stream responseStream = httpWebResponse.GetResponseStream();
                currentTry--;

                //httpWebRequest.Abort(); 
                //httpWebResponse.Close(); 

                return responseStream;
            }
            catch (Exception e)
            {
                //Console.ForegroundColor = ConsoleColor.Red;
                //Console.WriteLine(DateTime.Now.ToString("HH:mm:ss ") + e.Message);
                //Console.ForegroundColor = ConsoleColor.White;

                if (currentTry <= maxTry)
                {
                    GetHtml(url, cookieContainer);
                }

                currentTry--;

                if (httpWebRequest != null)
                {
                    httpWebRequest.Abort();
                } if (httpWebResponse != null)
                {
                    httpWebResponse.Close();
                }
                return null;
            }
        }
        #endregion

        #region 编码与解码
        /// <summary>
        /// http    url编码
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string Encode(string value)
        {
            var result = HttpUtility.UrlEncode(value, Encoding);
            return result;
        }
        /// <summary>
        /// http    url解码
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string Dencode(string value)
        {
            var result = HttpUtility.UrlDecode(value, Encoding);
            return result;
        }

        #endregion
    }

}
