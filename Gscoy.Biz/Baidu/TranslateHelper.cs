using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Gscoy.Common;
using System.Web;
using Gscoy.DataModel.Baidu;

namespace Gscoy.Biz.Baidu
{
    public class TranslateHelper : BaseBiz
    {
        static Dictionary<string, string> dicLanguage = null;

        static TranslateHelper()
        {
            dicLanguage = new Dictionary<string, string>();
            dicLanguage.Add("中文", "zh");
            dicLanguage.Add("日语", "jp");
            dicLanguage.Add("西班牙语", "spa");
            dicLanguage.Add("泰语", "th");
            dicLanguage.Add("俄罗斯语", "ru");
            dicLanguage.Add("粤语", "yue");
            dicLanguage.Add("白话文", "zh");
            dicLanguage.Add("德语", "de");
            dicLanguage.Add("英语", "en");
            dicLanguage.Add("韩语", "kor");
            dicLanguage.Add("法语", "fra");
            dicLanguage.Add("阿拉伯语", "ara");
            dicLanguage.Add("葡萄牙语", "pt");
            dicLanguage.Add("文言文", "wyw");
            dicLanguage.Add("自动检测", "auto");
            dicLanguage.Add("意大利语", "it");
        }

        /// <summary>
        /// 获取能翻译的语言列表
        /// </summary>
        /// <returns></returns>
        public static List<string> GetTransList()
        {
            //存在
            if (dicLanguage.Count > 0)
                return dicLanguage.Keys.ToList();
            return null;
        }
        /// <summary>
        /// 翻译
        /// </summary>
        /// <param name="query">要翻译的内容</param>
        /// <param name="from">源语言语种：语言代码或auto </param>
        /// <param name="to">目标语言语种：语言代码或auto </param>
        /// <returns></returns>
        public static TranslateEntity Translate(string query, string from = "", string to = "")
        {
            //判断源与目标语言的有效性
            if (!dicLanguage.ContainsKey(from)) from = "auto";
            if (!dicLanguage.ContainsKey(to)) to = "auto";

            string url = string.Format("http://openapi.baidu.com/public/2.0/bmt/translate?client_id={0}&q={1}&from={2}&to={3}", baiduAK, HttpUtility.UrlEncode(query, Encoding.UTF8), from, to);
            HtmlHelper htmlHelper = new HtmlHelper();
            Response resp = htmlHelper.GetHTML(url);
            TranslateEntity entity = resp.Html.FromJson<TranslateEntity>();
            return entity;
        }
    }
}
