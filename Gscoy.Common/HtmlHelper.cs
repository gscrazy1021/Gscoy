using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Net;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using System.Text;

namespace Gscoy.Common
{
    #region 发送方式
    public enum Method
    {
        GET, POST
    }
    #endregion

    #region 返回内容
    /// <summary>
    /// 返回内容
    /// </summary>
    public class Response
    {
        #region 远程服务器时间
        DateTime _RemoteDateTime = Convert.ToDateTime("1900-01-01 00:00:00.000");
        /// <summary>
        /// 远程服务器时间
        /// </summary>
        public DateTime RemoteDateTime
        {
            get { return _RemoteDateTime; }
            set { _RemoteDateTime = value; }
        }
        #endregion

        #region 返回内容
        string html = string.Empty;
        /// <summary>
        /// 返回内容
        /// </summary>
        public string Html
        {
            get { return html; }
            set { html = value; }
        }
        #endregion

        #region 返回的Cookies
        CookieContainer _Cookies = new CookieContainer();
        /// <summary>
        /// 返回的Cookies
        /// </summary>
        public CookieContainer Cookies
        {
            get { return _Cookies; }
            set { _Cookies = value; }
        }
        #endregion

        #region HTTP状态代码
        /// <summary>
        /// HTTP状态代码
        /// </summary>
        public HttpStatusCode StatusCode { get; set; }
        #endregion

        #region 返回的图片内容
        Image _Image = null;
        /// <summary>
        /// 返回的图片内容
        /// </summary>
        public Image Image
        {
            get { return _Image; }
            set { _Image = value; }
        }
        #endregion

        #region 当前URL
        string _Url = string.Empty;
        /// <summary>
        /// 当前URL
        /// </summary>
        public string Url
        {
            get { return _Url; }
            set { _Url = value; }
        }
        #endregion
    }
    #endregion

    #region 发送数据
    /// <summary>
    /// 发送数据
    /// </summary>
    [Serializable]
    public class Request
    {
        List<KeyValuePair<string, string>> sList = new List<KeyValuePair<string, string>>();
        Method _sendMethod = Method.GET;
        public Method Method
        {
            get { return _sendMethod; }
            set { _sendMethod = value; }
        }

        private CookieContainer _Cookies = new CookieContainer();
        public CookieContainer Cookies
        {
            get { return this._Cookies; }
            set { this._Cookies = value; }
        }
        public void Clear()
        {
            this.sList.Clear();
        }
        public void Add(string key, string value)
        {
            this.sList.Add(new KeyValuePair<string, string>(key, value));
        }
        public void Update(string key, string value)
        {
            int index = -1;
            for (int i = 0; i < this.List.Count; i++)
            {
                if (this.List[i].Key.Equals(key))
                {
                    index = i;
                    break;
                }
            }

            if (index > 0) this.List.RemoveAt(index);

            this.List.Add(new KeyValuePair<string, string>(key, value));
        }
        public override string ToString()
        {
            string sRet = string.Empty;

            foreach (KeyValuePair<string, string> val in this.sList)
            {
                if (sRet.Length == 0)
                    sRet = string.Format("{0}={1}", val.Key, val.Value);
                else
                    sRet = string.Format("{0}&{1}={2}", sRet, val.Key, val.Value);
            }

            return sRet;
        }

        public byte[] ToBytes()
        {
            return Encoding.ASCII.GetBytes(this.ToString());
        }

        public List<KeyValuePair<string, string>> List
        {
            set
            {
                this.sList = value;
            }
            get
            {
                return this.sList;
            }
        }

        public string Find(string key)
        {
            string sRet = string.Empty;

            KeyValuePair<string, string> val = this.List.Find(delegate(KeyValuePair<string, string> k) { return k.Key.Equals(key); });

            sRet = val.Value;

            return sRet;
        }

    }
    #endregion

    public class HtmlHelper
    {
        public static HtmlHelper Init()
        {
            return new HtmlHelper();
        }



        #region Cookies
        CookieContainer _Cookies = new CookieContainer();
        /// <summary>
        /// Cookies
        /// </summary>
        public CookieContainer Cookies
        {
            get { return _Cookies; }
            set { _Cookies = value; }
        }
        #endregion

        #region 连接远程服务器超时触发事件
        public delegate void Connection_TimeOut_Handle(string sUrl, Request request, Exception ex);
        /// <summary>
        /// 连接远程服务器超时触发事件
        /// </summary>
        public event Connection_TimeOut_Handle Connection_TimeOut;
        #endregion

        #region 获取数据完毕触发事件
        public delegate void Connection_Complete_Handle(Response response);
        /// <summary>
        ///  获取数据完毕触发事件
        /// </summary>
        public event Connection_Complete_Handle Connection_Complete;
        #endregion

        #region 设置发送头信息
        /// <summary>
        /// 设置发送头信息
        /// </summary>
        /// <param name="request"></param>
        private void SetRequestHeader(ref HttpWebRequest request, string referer)
        {
            request.Timeout = 5000;
            request.Accept = "*/*";
            request.UserAgent = "Mozilla/4.0 (compatible; MSIE 6.0; Windows NT 5.2; SV1; .NET CLR 2.0.50727;)";
            request.Headers["Accept-Encoding"] = "gzip, deflate";
            request.Headers["Accept-Language"] = "zh-cn";
            request.Headers["Accept-Charset"] = "utf-8;q=0.7,*;q=0.7";
            request.ContentType = "application/x-www-form-urlencoded";
            request.Referer = referer;
            request.KeepAlive = true;
        }
        #endregion

        #region 解压经过Gzip压缩的流
        /// <summary>
        /// 解压经过Gzip压缩的流
        /// </summary>
        /// <param name="contentEncoding">流编码</param>
        /// <param name="resonseStream">网页返回的流内容</param>
        /// <returns></returns>
        private string GzipStreame(string ecoding, Stream resonseStream)
        {
            return GzipStreame(ecoding, resonseStream, Encoding.UTF8);
        }
        private string GzipStreame(string ecoding, Stream resonseStream, Encoding coding)
        {
            string html = string.Empty;
            if (ecoding.ToLower().IndexOf("gzip") != -1)
            {
                html = new StreamReader(new GZipStream(resonseStream, CompressionMode.Decompress), coding).ReadToEnd();
            }
            else if (ecoding.ToLower().IndexOf("deflate") >= 0)
            {
                html = new StreamReader(new DeflateStream(resonseStream, CompressionMode.Decompress), coding).ReadToEnd();
            }
            else
            {
                html = new StreamReader(resonseStream, coding).ReadToEnd();
            }
            return html;
        }

        #endregion

        #region 获取远程HTML
        /// <summary>
        /// 获取远程HTML
        /// </summary>
        /// <param name="url">地址</param>
        /// <returns></returns>
        public Response GetHTML(string url)
        {
            return GetHTML(url, false, new Request(), new Uri(url).Host, Encoding.UTF8, null);
        }
        public Response GetHTML(string url, WebProxy proxy)
        {
            return GetHTML(url, false, new Request(), new Uri(url).Host, Encoding.UTF8, proxy);
        }
        public Response GetHTML(string url, Encoding coding)
        {
            return GetHTML(url, false, new Request(), new Uri(url).Host, coding, null);
        }
        public Response GetHTML(string url, Encoding coding, WebProxy proxy)
        {
            return GetHTML(url, false, new Request(), new Uri(url).Host, coding, proxy);
        }
        /// <summary>
        /// 获取远程HTML
        /// </summary>
        /// <param name="url">地址</param>
        /// <param name="send">是否自动重定向URL</param>
        /// <returns></returns>
        public Response GetHTML(string url, bool bAllowAutoRedirect)
        {
            return GetHTML(url, bAllowAutoRedirect, new Request() { }, new Uri(url).Host, Encoding.UTF8, null);
        }
        public Response GetHTML(string url, bool bAllowAutoRedirect, WebProxy porxy)
        {
            return GetHTML(url, bAllowAutoRedirect, new Request() { }, new Uri(url).Host, Encoding.UTF8, porxy);
        }
        /// <summary>
        /// 获取远程HTML
        /// </summary>
        /// <param name="sUrl">地址</param>
        /// <param name="send">发送内容</param>
        /// <returns></returns>
        public Response GetHTML(string url, Request request)
        {
            return GetHTML(url, request, Encoding.UTF8);
        }
        public Response GetHTML(string url, Request request, WebProxy porxy)
        {
            return GetHTML(url, false, request, new Uri(url).Host, Encoding.UTF8, porxy);
        }
        public Response GetHTML(string url, bool bAllowAutoRedirect, Request request)
        {
            return GetHTML(url, bAllowAutoRedirect, request, new Uri(url).Host, Encoding.UTF8, null);
        }
        public Response GetHTML(string url, bool bAllowAutoRedirect, Request request, WebProxy proxy)
        {
            return GetHTML(url, bAllowAutoRedirect, request, new Uri(url).Host, Encoding.UTF8, proxy);
        }
        public Response GetHTML(string url, Request request, Encoding coding)
        {
            return GetHTML(url, false, request, new Uri(url).Host, coding, null);
        }
        public Response GetHTML(string url, Request request, Encoding coding, WebProxy proxy)
        {
            return GetHTML(url, false, request, new Uri(url).Host, coding, proxy);
        }
        /// <summary>
        /// 获取远程HTML
        /// </summary>
        /// <param name="sUrl">地址</param>
        /// <param name="bAllowAutoRedirect">是否自动跳转</param>
        /// <param name="send">发送内容</param>
        /// <returns></returns>
        public Response GetHTML(string sUrl, bool bAllowAutoRedirect, Request request, string referer)
        {
            return GetHTML(sUrl, bAllowAutoRedirect, request, referer, Encoding.UTF8, null);
        }
        public Response GetHTML(string sUrl, bool bAllowAutoRedirect, Request request, string referer, WebProxy proxy)
        {
            return GetHTML(sUrl, bAllowAutoRedirect, request, referer, Encoding.UTF8, proxy);
        }
        /// <summary>
        /// 获取HTML页面内容
        /// </summary>
        /// <param name="url">地址</param>
        /// <param name="bAllowAutoRedirect">是否自动跳转</param>
        /// <param name="request">查询信息</param>
        /// <param name="referer">引用页</param>
        /// <param name="coding">编码格式</param>
        /// <returns></returns>
        public Response GetHTML(string url, bool bAllowAutoRedirect, Request request, string referer, Encoding coding, WebProxy proxy)
        {
            Response result = new Response();

            if (request.Method == Method.GET)
            {
                string sData = request.ToString();
                if (sData.Length > 0) url = string.Format("{0}?{1}", url, sData);
            }

            HttpWebRequest httpRequest = (HttpWebRequest)WebRequest.Create(url);
            if (proxy != null) httpRequest.Proxy = proxy;

            #region SSL方式
            if (url.Contains("https://"))
            {
                //这一句一定要写在创建连接的前面。使用回调的方法进行证书验证。
                ServicePointManager.ServerCertificateValidationCallback = new System.Net.Security.RemoteCertificateValidationCallback(CheckValidationResult);
                //创建证书文件
                X509Certificate objx509 = new X509Certificate();

                //添加到请求里
                httpRequest.ClientCertificates.Add(objx509);
            }
            #endregion

            // 设置发送头信息
            if (referer.Length == 0)
                SetRequestHeader(ref httpRequest, new Uri(url).Host);
            else
                SetRequestHeader(ref httpRequest, referer);
            // 自动重定向
            httpRequest.AllowAutoRedirect = bAllowAutoRedirect;
            httpRequest.Timeout = 60 * 1000;
            // 关联Cookies
            //if (httpRequest.CookieContainer == null)
            //    httpRequest.CookieContainer = new CookieContainer();
            httpRequest.CookieContainer = this.Cookies;

            try
            {

                if (request.Method == Method.POST)
                {
                    // 设置发送方式
                    httpRequest.Method = request.Method.ToString();
                    // 获取发送数据流
                    //HttpWebResponse response = httpRequest.GetResponse() as HttpWebResponse;

                    Stream strem = httpRequest.GetRequestStream();

                    // 写入发送数据
                    byte[] bs = request.ToBytes();
                    strem.Write(bs, 0, bs.Length);
                    strem.Close();
                }

                HttpWebResponse httpResponse = httpRequest.GetResponse() as HttpWebResponse;

                Stream responseStream = httpResponse.GetResponseStream();

                StreamReader sr = new StreamReader(responseStream, coding);

                //result.Html = sr.ReadToEnd();
                result.Html = System.Web.HttpUtility.HtmlDecode(GzipStreame(httpResponse.ContentEncoding, responseStream, coding));
                result.RemoteDateTime = Convert.ToDateTime(httpResponse.GetResponseHeader("Date"));

                result.Url = httpResponse.ResponseUri.OriginalString;

                this.Cookies.Add(httpResponse.Cookies);

                result.Cookies = this.Cookies;

                result.StatusCode = httpResponse.StatusCode;

                responseStream.Dispose();

                if (this.Connection_Complete != null)
                    this.Connection_Complete.Invoke(result);
            }
            catch (Exception ex)
            {
                if (this.Connection_TimeOut != null)
                    this.Connection_TimeOut.Invoke(url, request, ex);
            }
            return result;
        }

        private bool CheckValidationResult(object sender, X509Certificate certificate, X509Chain chain, SslPolicyErrors sslPolicyErrors)
        {
            return true;
        }

        #endregion

        #region 获取远程图片
        public Response GetImage(string sUrl)
        {
            return GetImage(sUrl, false, new Request());
        }
        public Response GetImage(string sUrl, WebProxy proxy)
        {
            return GetImage(sUrl, false, new Request(), proxy);
        }
        public Response GetImage(string sUrl, string referer)
        {
            return GetImage(sUrl, false, referer, null);
        }
        public Response GetImage(string sUrl, string referer, WebProxy proxy)
        {
            return GetImage(sUrl, false, referer, proxy);
        }
        public Response GetImage(string sUrl, bool bAllowAutoRedirect, string referer)
        {
            return GetImage(sUrl, bAllowAutoRedirect, new Request(), referer, null);
        }
        public Response GetImage(string sUrl, bool bAllowAutoRedirect, string referer, WebProxy proxy)
        {
            return GetImage(sUrl, bAllowAutoRedirect, new Request(), referer, null);
        }
        //public Image GetImage(string sUrl, bool bAllowAutoRedirect, Request request)
        //{
        //    return GetImage(sUrl, bAllowAutoRedirect, request, new CookieContainer(), "");
        ////}
        //public Image GetImage(string sUrl, bool bAllowAutoRedirect, Request request, string referer)
        //{
        //    return GetImage(sUrl, bAllowAutoRedirect, request,referer);
        //}
        /// <summary>
        /// 获取远程图片
        /// </summary>
        /// <param name="sUrl">图片地址</param>
        /// <param name="bAllowAutoRedirect">是否自动跳转</param>
        /// <param name="send">发送内容</param>
        /// <returns></returns>
        public Response GetImage(string sUrl, bool bAllowAutoRedirect, Request request)
        {
            return GetImage(sUrl, bAllowAutoRedirect, request, "", null);
        }
        public Response GetImage(string sUrl, bool bAllowAutoRedirect, Request request, string referer)
        {
            return GetImage(sUrl, bAllowAutoRedirect, request, referer, null);
        }
        public Response GetImage(string sUrl, bool bAllowAutoRedirect, Request request, WebProxy proxy)
        {
            return GetImage(sUrl, bAllowAutoRedirect, request, "", proxy);
        }
        /// <summary>
        /// 获取远程图片
        /// </summary>
        /// <param name="sUrl">图片地址</param>
        /// <param name="bAllowAutoRedirect">是否自动跳转</param>
        /// <param name="send">发送内容</param>
        /// <returns></returns>
        public Response GetImage(string url, bool bAllowAutoRedirect, Request request, string referer, WebProxy proxy)
        {
            Response response = new Response();

            if (request.Method == Method.GET)
            {
                string sData = request.ToString();
                if (sData.Length > 0) url = string.Format("{0}?{1}", url, sData);
            }

            HttpWebRequest httpRequest = (HttpWebRequest)WebRequest.Create(url);
            if (proxy != null) httpRequest.Proxy = proxy;

            #region SSL方式
            if (url.Contains("https://"))
            {
                //这一句一定要写在创建连接的前面。使用回调的方法进行证书验证。
                ServicePointManager.ServerCertificateValidationCallback = new System.Net.Security.RemoteCertificateValidationCallback(CheckValidationResult);
                //创建证书文件
                X509Certificate objx509 = new X509Certificate();

                //添加到请求里
                httpRequest.ClientCertificates.Add(objx509);
            }
            #endregion

            // 设置发送头信息
            if (referer.Length == 0)
                SetRequestHeader(ref httpRequest, new Uri(url).Host);
            else
                SetRequestHeader(ref httpRequest, referer);

            // 自动重定向
            httpRequest.AllowAutoRedirect = bAllowAutoRedirect;
            // 关联Cookies
            httpRequest.CookieContainer = this.Cookies;

            try
            {

                if (request.Method == Method.POST)
                {
                    // 设置发送方式
                    httpRequest.Method = request.Method.ToString();
                    // 获取发送数据流
                    //HttpWebResponse response = httpRequest.GetResponse() as HttpWebResponse;

                    Stream strem = httpRequest.GetRequestStream();

                    // 写入发送数据
                    byte[] bs = request.ToBytes();
                    strem.Write(bs, 0, bs.Length);
                    strem.Close();
                }

                HttpWebResponse httpResponse = httpRequest.GetResponse() as HttpWebResponse;

                Stream responseStream = httpResponse.GetResponseStream();

                //if (responseStream.CanSeek)
                //{
                response.Image = Image.FromStream(responseStream);
                //}
                //else
                //{
                //   response.Image = null;
                //}
                response.RemoteDateTime = Convert.ToDateTime(httpResponse.GetResponseHeader("Date"));

                this.Cookies.Add(httpResponse.Cookies);
                response.Cookies = this.Cookies;
                response.StatusCode = httpResponse.StatusCode;
                responseStream.Dispose();
                responseStream.Close();
            }
            catch (Exception ex)
            {
                if (this.Connection_TimeOut != null)
                    this.Connection_TimeOut.Invoke(url, request, ex);
            }

            return response;
        }
        #endregion

        #region 异步获取远程HTML
        /// <summary>
        /// 异步获取远程HTML
        /// </summary>
        /// <param name="sUrl">地址</param>
        /// <returns></returns>
        public void BeginGetHTML(string sUrl)
        {
            BeginGetHTML(sUrl, false, new Request() { });
        }
        /// <summary>
        /// 异步获取远程HTML
        /// </summary>
        /// <param name="sUrl">地址</param>
        /// <param name="send">是否自动重定向URL</param>
        /// <returns></returns>
        public void BeginGetHTML(string sUrl, bool bAllowAutoRedirect)
        {
            BeginGetHTML(sUrl, bAllowAutoRedirect, new Request() { });
        }
        /// <summary>
        /// 异步获取远程HTML
        /// </summary>
        /// <param name="sUrl">地址</param>
        /// <param name="send">发送内容</param>
        /// <returns></returns>
        public void BeginGetHTML(string sUrl, Request request)
        {
            BeginGetHTML(sUrl, false, request);
        }
        /// <summary>
        /// 异步获取远程HTML
        /// </summary>
        /// <param name="sUrl">地址</param>
        /// <param name="bAllowAutoRedirect">是否自动跳转</param>
        /// <param name="send">发送内容</param>
        /// <returns></returns>
        public void BeginGetHTML(string sUrl, bool bAllowAutoRedirect, Request request)
        {
            Response result = new Response();

            if (request.Method == Method.GET)
            {
                string sData = request.ToString();
                if (sData.Length > 0) sUrl = string.Format("{0}?{1}", sUrl, sData);
            }

            HttpWebRequest httpRequest = (HttpWebRequest)WebRequest.Create(sUrl);
            // 设置发送头信息
            SetRequestHeader(ref httpRequest, new Uri(sUrl).Host);
            // 自动重定向
            httpRequest.AllowAutoRedirect = bAllowAutoRedirect;

            if (request.Method == Method.POST)
            {
                // 设置发送方式
                httpRequest.Method = request.Method.ToString();

                // 获取发送数据流
                Stream stream = httpRequest.GetRequestStream();

                byte[] bs = request.ToBytes();
                stream.Write(bs, 0, bs.Length);
                stream.Close();
                //httpRequest.BeginGetRequestStream(new AsyncCallback(GetRequestStreamCallback),httpRequest);
                // 写入发送数据

            }

            //HttpWebResponse httpResponse = httpRequest.GetResponse() as HttpWebResponse;

            AsyncCallback callback = new AsyncCallback(GetResponseStreamCallback);
            try
            {
                httpRequest.BeginGetResponse(callback, httpRequest);
            }
            catch (WebException ex)
            {
                if (this.Connection_TimeOut != null)
                    this.Connection_TimeOut.Invoke(sUrl, request, ex);
            }
        }

        private void GetRequestStreamCallback(IAsyncResult ar)
        {
            //Request request = ar.AsyncState as Request;
            //Stream stream = async.EndGetRequestStream(ar);

            //byte[] bs = request.ToBytes();
            //stream.Write(bs, 0, bs.Length);
            //stream.Close();
        }

        private void GetResponseStreamCallback(IAsyncResult ar)
        {
            HttpWebRequest httpRequest = ar.AsyncState as HttpWebRequest;
            HttpWebResponse httpResponse = httpRequest.EndGetResponse(ar) as HttpWebResponse;

            Stream responseStream = httpResponse.GetResponseStream();

            Response response = new Response();

            response.Html = GzipStreame(httpResponse.ContentEncoding, responseStream);
            response.RemoteDateTime = Convert.ToDateTime(httpResponse.GetResponseHeader("Date"));
            //response.Cookies = httpResponse.Cookies;

            if (this.Connection_Complete != null)
                this.Connection_Complete.Invoke(response);
        }
        #endregion
    }
}
