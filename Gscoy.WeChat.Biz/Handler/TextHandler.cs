using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Gscoy.Biz;
using Gscoy.Common;
using Gscoy.WeChat.Model.RequestModel;
using Gscoy.WeChat.Model.ResponseModel;

namespace Gscoy.WeChat.Biz.Handler
{
    public class TextHandler : IHandler
    {
        /// <summary>
        /// 请求的XML
        /// </summary>
        private string RequestXml { get; set; }
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="requestXml">请求的xml</param>
        public TextHandler(string requestXml)
        {
            LogHelper.Trace(requestXml);
            this.RequestXml = requestXml;
        }
        #region IHandler 成员

        public string HandleRequest()
        {
            string response = string.Empty;
            RequestText txt = RequestText.LoadFromXml(RequestXml);
            string content = txt.Content.Trim();
            if (string.IsNullOrEmpty(content))
            {
                response = "您什么都没输入，没法帮您啊，%>_<%。";
            }
            else
            {
                try
                {
                    var inputStr = content.Split('@');
                    var msgType = inputStr[0];
                    switch (msgType.ToLower())
                    {
                        case "tq":
                            if (inputStr.Length == 1 || string.IsNullOrEmpty(inputStr[1]))
                            {
                                //response = WeatherHelper.GetWeatherInfo("101010100");
                                //SFB sf = new SFB();
                                //var s = sf.GetHouse();
                                //response += s;
                            }
                            else
                            {
                                response = WeatherHelper.GetWeatherInfoByCity(inputStr[1]);
                            }
                            break;
                        default:
                            response = "输入的类型不对撒~";
                            break;
                    }
                }
                catch (Exception ex)
                {
                    response = string.Format(ex.Message);
                    LogHelper.Trace(ex);
                }
            }
            ResponseText rt = new ResponseText();
            rt.Content = response;
            rt.ToUserName = txt.FromUserName;
            rt.FromUserName = txt.ToUserName;
            rt.MsgType = txt.MsgType;
            response = rt.ToXML();
            return response;
        }

        #endregion
    }
}
