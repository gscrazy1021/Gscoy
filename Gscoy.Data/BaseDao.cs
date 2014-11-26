using Gscoy.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace Gscoy.Data
{
    public class BaseDao
    {
        public bool PostData(string url, Dictionary<string, string> dic)
        {
            if (dic == null) return false;
            HtmlHelper helper = new HtmlHelper();
            var resp = helper.GetHTML(url, dic);
            if (CheckSinaResponseString(resp.Html) == "success")
                return true;
            return false;
        }
        /// <summary>
        /// 返回有效的字符串
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public string CheckSinaResponseString(string input)
        {
            //["success"]
            var matches = input.GetMatches("<body>(.|\\s|\r|\n|\f)*</body>");
            if (matches.Count > 0)
            {
                var json = matches[0].Value;
                return json.Replace("<body>", "").Replace("</body>", "").Replace(" ", "").Replace("\n", "").Replace("[", "").Replace("]", "").Replace("\"", "");
            }
            return string.Empty;
        }
    }
}
