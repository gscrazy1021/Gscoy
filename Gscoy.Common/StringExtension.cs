using System;
using System.Collections;
using System.Collections.Generic;
using System.Security;
using System.Text.RegularExpressions;

namespace Gscoy.Common
{
    public static class StringExtension
    {
        private static readonly string[] defaultSeparator = new string[1] { "," };
        private static readonly string[] sqlBadChar = { "script", "exec", "select", "update", "delete", "declare", "'" };
        private static readonly string[] sqlNewChar = { "ＳＣＲＩＰＴ", "ｅｘｅｃ", "ｓｅｌｅｃｔ", "ｕｐｄａｔｅ", "ｄｅｌｅｔｅ", "ｄｅｃｌａｒｅ", "\"" };
        private static readonly string[] xssStrings = { "javascript", "vbscript", "expression", "applet", "meta", "xml", "blink", "link", "script", "embed", "object", "iframe", "frame", "frameset", "ilayer", "layer", "bgsound", "title", "base", "onabort", "onactivate", "onafterprint", "onafterupdate", "onbeforeactivate", "onbeforecopy", "onbeforecut", "onbeforedeactivate", "onbeforeeditfocus", "onbeforepaste", "onbeforeprint", "onbeforeunload", "onbeforeupdate", "onblur", "onbounce", "oncellchange", "onchange", "onclick", "oncontextmenu", "oncontrolselect", "oncopy", "oncut", "ondataavailable", "ondatasetchanged", "ondatasetcomplete", "ondblclick", "ondeactivate", "ondrag", "ondragend", "ondragenter", "ondragleave", "ondragover", "ondragstart", "ondrop", "onerror", "onerrorupdate", "onfilterchange", "onfinish", "onfocus", "onfocusin", "onfocusout", "onhelp", "onkeydown", "onkeypress", "onkeyup", "onlayoutcomplete", "onload", "onlosecapture", "onmousedown", "onmouseenter", "onmouseleave", "onmousemove", "onmouseout", "onmouseover", "onmouseup", "onmousewheel", "onmove", "onmoveend", "onmovestart", "onpaste", "onpropertychange", "onreadystatechange", "onreset", "onresize", "onresizeend", "onresizestart", "onrowenter", "onrowexit", "onrowsdelete", "onrowsinserted", "onscroll", "onselect", "onselectionchange", "onselectstart", "onstart", "onstop", "onsubmit", "onunload" };
        //, "style"
        /// <summary>
        /// 执行正则
        /// </summary>
        /// <param name="strVal"></param>
        /// <param name="regExValue"></param>
        /// <returns></returns>
        public static bool IsRegEx(this string strVal, string regExValue)
        {

            try
            {
                Regex regex = new System.Text.RegularExpressions.Regex(regExValue);
                return regex.IsMatch(strVal);
            }
            catch (Exception)
            {
                return false;
            }
        }

        /// <summary>
        /// 解析,解析失败返回空
        /// </summary>
        /// <typeparam name="T">类型</typeparam>
        /// <param name="strVal">字符串值</param>
        /// <returns>返回解析后的数据</returns>
        public static T? ParseToNullable<T>(this string strVal) where T : struct
        {
            if (string.IsNullOrWhiteSpace(strVal))
            {
                return null;
            }
            dynamic retVal = null;
            Type type = typeof(T);
            if (type == typeof(int))
            {
                int tempV;
                if (int.TryParse(strVal, out tempV))
                {
                    retVal = tempV;
                }
            }
            else if (type == typeof(long))
            {
                long tempV;
                if (long.TryParse(strVal, out tempV))
                {
                    retVal = tempV;
                }
            }
            else if (type == typeof(short))
            {
                short tempV;
                if (short.TryParse(strVal, out tempV))
                {
                    retVal = tempV;
                }
            }
            else if (type == typeof(double))
            {
                double tempV;
                if (double.TryParse(strVal, out tempV))
                {
                    retVal = tempV;
                }
            }
            else if (type == typeof(float))
            {
                float tempV;
                if (float.TryParse(strVal, out tempV))
                {
                    retVal = tempV;
                }
            }
            else if (type == typeof(decimal))
            {
                decimal tempV;
                if (decimal.TryParse(strVal, out tempV))
                {
                    retVal = tempV;
                }
            }
            else if (type == typeof(DateTime))
            {
                DateTime tempV;
                if (DateTime.TryParse(strVal, out tempV))
                {
                    retVal = tempV;
                }
            }
            else if (type == typeof(bool))
            {
                string tempV = strVal.ToLower();
                if (tempV == "true" || tempV == "1" || tempV == "t")
                {
                    retVal = true;
                }
                else if (tempV == "false" || tempV == "0" || tempV == "f")
                {
                    retVal = false;
                }
            }
            else
            {
                throw new Exception("类型错误,不支持该类型");
            }
            return retVal;
        }

        /// <summary>
        /// 解析
        /// </summary>
        /// <typeparam name="T">类型</typeparam>
        /// <param name="strVal">字符串值</param>
        /// <param name="defaultValue">解析失败后的默认值</param>
        /// <returns>返回解析后的数据</returns>
        public static T Parse<T>(this string strVal, T defaultValue = default(T)) where T : struct
        {
            System.Nullable<T> val = ParseToNullable<T>(strVal);
            return val.HasValue ? val.Value : defaultValue;
        }

        /// <summary>
        /// 解析为列表
        /// </summary>
        /// <typeparam name="TItem">类型</typeparam>
        /// <param name="val">字符串值</param>
        /// <param name="separator">分隔符 默认为','</param>
        /// <param name="defaultValue">解析失败后的默认值</param>
        /// <returns>返回解析后的列表</returns>
        public static List<TItem> ParseToList<TItem>(this string strVal, string[] separator = null) where TItem : struct
        {
            string val = strVal;
            List<TItem> list = null;
            if (separator == null)
            {
                separator = defaultSeparator;
            }

            if (!string.IsNullOrWhiteSpace(val))
            {
                list = new List<TItem>();
                string[] vals = val.Trim().Split(separator, StringSplitOptions.RemoveEmptyEntries);
                for (int i = 0; i < vals.Length; i++)
                {
                    TItem? item = ParseToNullable<TItem>(vals[i]);
                    if (item.HasValue)
                    {
                        list.Add(item.Value);
                    }
                }
            }
            return list;
        }

        /// <summary>
        /// 计算字符串的长度(一个汉字算两个字符)
        /// </summary>
        /// <param name="val">字符串</param>
        /// <returns>返回字符串的长度</returns>
        public static int LengthByByte(this string val)
        {
            try
            {
                byte[] s = System.Text.Encoding.Default.GetBytes(val);
                return s.Length;
            }
            catch
            {
                return 0;
            }
        }

        /// <summary>
        /// 按字节截取字符串
        /// </summary>
        /// <param name="val">字符串</param>
        /// <param name="length">要截取的长度</param>
        /// <returns>截取后的字符串</returns>
        public static string Cut(this string val, int length)
        {
            //如果为NULL或者为空字符串，直接返回
            if (string.IsNullOrEmpty(val))
            {
                return string.Empty;
            }
            string retStr = val.Trim();
            if (retStr.Length * 2 <= length)
            {
                return retStr;
            }
            int i = 0, j = 0;
            foreach (char Char in retStr)
            {
                if ((int)Char > 127)
                    i += 2;
                else
                    i++;
                if (i > length)
                {
                    retStr = retStr.Substring(0, j);
                    break;
                }
                j++;
            }
            return retStr;
        }
        public static string GetSafeString(this string val, bool needAntiXss)
        {
            return GetSafeString(val, needAntiXss ? AntiXSSMode.Default : AntiXSSMode.None);
        }
        /// <summary>
        /// 对字符串值做xss和sql注入过滤
        /// </summary>
        /// <param name="val">字符串</param>
        /// <param name="antiXssMode">是否需要做xss过滤，默认为是</param>
        /// <returns>过滤后的值</returns>
        public static string GetSafeString(this string val, AntiXSSMode antiXssMode = AntiXSSMode.Default)
        {
            string strVal = val;

            if (!string.IsNullOrWhiteSpace(strVal))
            {
                if (antiXssMode == AntiXSSMode.Default)
                {
                    strVal = HtmlHelper.FilterXSS(strVal);
                    if (strVal.Contains("&#"))
                    {
                        //还原+号，防止有类似1+1这样的公司名称等
                        strVal = strVal.Replace("&#43;", "+");

                        strVal = strVal.Replace("&#20028;", "丼");
                        strVal = strVal.Replace("&#20284;", "似");
                        strVal = strVal.Replace("&#20540;", "值");
                        strVal = strVal.Replace("&#20796;", "儼");
                        strVal = strVal.Replace("&#21052;", "刼");
                        strVal = strVal.Replace("&#21308;", "匼");
                        strVal = strVal.Replace("&#21564;", "吼");
                        strVal = strVal.Replace("&#21820;", "唼");
                        strVal = strVal.Replace("&#22076;", "嘼");
                        strVal = strVal.Replace("&#22332;", "圼");
                        strVal = strVal.Replace("&#22588;", "堼");
                        strVal = strVal.Replace("&#23612;", "尼");
                        strVal = strVal.Replace("&#26684;", "格");
                        strVal = strVal.Replace("&#22844;", "夼");
                        strVal = strVal.Replace("&#23100;", "娼");
                        strVal = strVal.Replace("&#23356;", "嬼");
                        strVal = strVal.Replace("&#23868;", "崼");
                        strVal = strVal.Replace("&#24124;", "帼");
                        strVal = strVal.Replace("&#24380;", "弼");
                        strVal = strVal.Replace("&#24636;", "怼");
                        strVal = strVal.Replace("&#24892;", "愼");
                        strVal = strVal.Replace("&#25148;", "戼");
                        strVal = strVal.Replace("&#25404;", "挼");
                        strVal = strVal.Replace("&#25660;", "搼");
                        strVal = strVal.Replace("&#25916;", "攼");
                        strVal = strVal.Replace("&#26172;", "昼");
                        strVal = strVal.Replace("&#26428;", "朼");
                        strVal = strVal.Replace("&#26940;", "椼");
                        strVal = strVal.Replace("&#27196;", "樼");
                        strVal = strVal.Replace("&#27452;", "欼");
                        strVal = strVal.Replace("&#27708;", "氼");
                        strVal = strVal.Replace("&#27964;", "洼");
                        strVal = strVal.Replace("&#28220;", "渼");
                        strVal = strVal.Replace("&#28476;", "漼");
                        strVal = strVal.Replace("&#28732;", "瀼");
                        strVal = strVal.Replace("&#28988;", "焼");
                        strVal = strVal.Replace("&#29244;", "爼");
                        strVal = strVal.Replace("&#29500;", "猼");
                        strVal = strVal.Replace("&#29756;", "琼");
                        strVal = strVal.Replace("&#30012;", "甼");
                        strVal = strVal.Replace("&#30268;", "瘼");
                        strVal = strVal.Replace("&#30524;", "眼");
                        strVal = strVal.Replace("&#30780;", "砼");
                        strVal = strVal.Replace("&#31036;", "礼");
                        strVal = strVal.Replace("&#31292;", "稼");
                        strVal = strVal.Replace("&#31548;", "笼");
                        strVal = strVal.Replace("&#31804;", "簼");
                        strVal = strVal.Replace("&#32060;", "紼");
                        strVal = strVal.Replace("&#32316;", "縼");
                        strVal = strVal.Replace("&#32572;", "缼");
                        strVal = strVal.Replace("&#32828;", "耼");
                        strVal = strVal.Replace("&#33084;", "脼");
                        strVal = strVal.Replace("&#33340;", "舼");
                        strVal = strVal.Replace("&#33596;", "茼");
                        strVal = strVal.Replace("&#33852;", "萼");
                        strVal = strVal.Replace("&#34108;", "蔼");
                        strVal = strVal.Replace("&#36156;", "贼");
                        strVal = strVal.Replace("&#39740;", "鬼");

                    }
                }
                else if (antiXssMode == AntiXSSMode.Simple)
                {
                    for (int i = 0; i < xssStrings.Length; i++)
                    {
                        strVal = Regex.Replace(strVal, xssStrings[i], string.Empty, RegexOptions.IgnoreCase);
                    }

                    for (int i = 0; i < xssStrings.Length; i++)
                    {
                        string pattern = "/";
                        for (int j = 0; j < xssStrings[i].Length; j++)
                        {
                            if (j > 0)
                            {
                                pattern += "(";
                                pattern += "(&#[xX]0{0,8}([9ab]);)";
                                pattern += "|";
                                pattern += "|(&#0{0,8}([9|10|13]);)";
                                pattern += ")*";
                            }
                            pattern += xssStrings[i][j];
                        }
                        pattern += "/i";
                        string replacement = xssStrings[i].Substring(0, 2) + "<x>" + xssStrings[i].Substring(2);
                        strVal = Regex.Replace(strVal, pattern, replacement);
                    }
                }
                strVal = FilterSqlChars(strVal);
            }
            return strVal;
        }

        /// <summary>
        /// 过滤特殊符号
        /// </summary>
        public static string ReplaceSpecialChar(this string val)
        {
            string retVal = val;
            if (!string.IsNullOrEmpty(retVal))
            {
                string[] spchar = { "~", "!", "@", "#", "$", "%", "^", "&", "*", "，", "。", "；", "：", "？", "…", "‘", "“", "《", "｛", "〖", "【", "】", "〗", "｝", "》", "’", "＃", "～", "?", "※", "＋", "－", "×", "÷", "№", "＄", "￥", "§", "‰", "—", "§", "№", "☆", "★", "○", "●", "◎", "◇", "◆", "□", "℃", "‰", "€", "■", "△", "▲", "※", "→", "←", "↑", "↓", "〓", "¤", "°", "＃", "＆", "＠", "＼", "︿", "＿", "￣", "―", "♂", "♀", "￠", "￡", "α", "β", "γ", "δ", "ε", "ζ", "η", "θ", "ι", "κ", "λ", "μ", "ν", "ξ", "ο", "π", "ρ", "σ", "τ", "υ", "φ", "χ", "ψ", "ω", "！", "％", "…", "…", "＊" };
                foreach (string s in spchar)
                {
                    retVal = retVal.Replace(s, "");
                }
            }
            return retVal;
        }

        /// <summary>
        /// 过滤sql注入字符
        /// </summary>
        /// <param name="oraString"></param>
        /// <returns></returns>
        private static string FilterSqlChars(this string oraString)
        {
            string strVal = oraString.Trim();
            if (!string.IsNullOrWhiteSpace(strVal))
            {
                for (int i = 0; i < sqlBadChar.Length; i++)
                {
                    oraString = oraString.Replace(sqlBadChar[i], sqlNewChar[i]);
                }
            }
            return oraString;
        }
        /// <summary>
        /// 截取中介公司前省份城市名称及特殊字符(获取公司简称)
        /// </summary>
        /// <param name="value">待过滤中介公司名称</param>
        /// <param name="len">截取后需保留字符长度</param>
        /// <returns>字符串：返回截取结果</returns>
        public static string FilterComName(this string value, int len)
        {
            if (!string.IsNullOrEmpty(value))
            {
                try
                {
                    value = Regex.Replace(value.Substring(0, 2), @"[`~!@#$%^&*()=_|\-\\\][\]\{\}:;'\,.<>/?·！：；，。？‘’“”【】（）——……]", string.Empty) + value.Substring(2);
                }
                catch
                {
                    ;
                }
                try
                {
                    int i = 0;
                    i = value.IndexOf("省");
                    if (i > -1)
                    {

                        if (value.Length > value.IndexOf("省") + 1)
                        {
                            value = value.Substring(value.IndexOf("省") + 1);
                        }

                    }
                    i = value.IndexOf("市");
                    if (i > -1)
                    {
                        if (value.Length > value.IndexOf("市") + 1)
                        {
                            value = value.Substring(value.IndexOf("市") + 1);
                        }
                    }
                    if (len != 0 && value.Length > len)
                    {
                        value = value.Substring(0, len);
                    }
                }
                catch { }
            }
            return value;
        }
        /// <summary>
        /// 截取指定长度的字符串+省略号	
        /// </summary>
        /// <param name="str">源字符串</param>
        /// <param name="Len">截断的长度</param>
        /// <returns>截断后的字符串 +省略号</returns>
        public static string GetSubString(this string str, int Len)
        {
            int i = 0, j = 0;
            foreach (char Char in str)
            {
                if ((int)Char > 127)
                    i += 2;
                else
                    i++;
                if (i > Len)
                {
                    str = str.Substring(0, j) + "...";
                    break;
                }
                j++;
            }
            return str;
        }

        /// <summary>
        /// 截取指定长度的字符串 不加省略号	
        /// </summary>
        /// <param name="str">源字符串</param>
        /// <param name="Len">截断的长度</param>
        /// <returns>截断后的字符串不加省略号</returns>
        public static string GetSubStringNoDot(this string str, int Len)
        {
            int i = 0, j = 0;
            foreach (char Char in str)
            {
                if ((int)Char > 127)
                    i += 2;
                else
                    i++;
                if (i > Len)
                {
                    str = str.Substring(0, j);
                    break;
                }
                j++;
            }
            return str;
        }

        /// <summary>
        /// 判断是否为整数
        /// </summary>
        /// <param name="itemValue"></param>
        /// <returns></returns>
        public static bool isInt(this string itemValue)
        {
            return (IsRegEx("^(-|\\+)?(\\d)*$", itemValue));
        }

        /// <summary> 转全角为半角的函数(DBC case) </summary>
        /// <param name="input">任意字符串</param>
        /// <returns>半角字符串</returns>
        ///<remarks>
        ///全角空格为12288，半角空格为32
        ///其他字符半角(33-126)与全角(65281-65374)的对应关系是：均相差65248
        ///</remarks>
        public static string ToDBC(this string input)
        {
            char[] c = input.ToCharArray();
            for (int i = 0; i < c.Length; i++)
            {
                if (c[i] == 12288)
                {
                    c[i] = (char)32;
                    continue;
                }
                if (c[i] > 65280 && c[i] < 65375)
                    c[i] = (char)(c[i] - 65248);
            }
            return new string(c);
        }
        /// <summary>
        /// 字符串排重
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        public static string[] GetUnique(string[] source)
        {
            string[] returnVal = null;
            Hashtable ht = new Hashtable();
            for (int i = 0, j = 0; i < source.Length; i++)
            {
                if (ht[source[i]] == null)
                {
                    ht[source[i]] = j++;
                }
            }
            returnVal = new string[ht.Count];
            int n = 0;
            foreach (string key in ht.Keys)
            {
                returnVal[n++] = key;
            }
            return returnVal;
        }
        /// <summary>
        /// 修正xml格式
        /// </summary>
        /// <param name="old_value"></param>
        /// <returns></returns>
        public static string RegularXmlValue(string old_value)
        {
            string new_value = old_value;
            new_value = new_value.Replace("<", "&lt;");
            new_value = new_value.Replace(">", "&gt;");
            new_value = new_value.Replace("&", "&amp;");
            return new_value;
        }

        /// <summary>
        /// 类型T必须具备
        /// (1)无参构造方法 
        /// (2)方法签名为 bool TryParse(string,T)的方法
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="value"></param>
        /// <returns></returns>
        [SecuritySafeCritical]
        public static bool TryParse<T>(this string value, out T t) where T : IConvertible
        {
            var type = typeof(T);
            Type[] types = { typeof(string), type.MakeByRefType() };
            var Method = type.GetMethod("TryParse", types);
            if (Method == null)
            {
                t = default(T);
                throw new TryParseException();
            }
            try
            {
                T Convertible = Activator.CreateInstance<T>();
                t = default(T);
                return (bool)Method.Invoke(Convertible, new object[] { value, Convertible });
            }
            catch
            {
                t = default(T);
                throw new TryParseException();
            }
        }

        /// <summary>
        /// 类型T必须具备
        /// (1)无参构造方法 
        /// (2)方法签名为 bool TryParse(string,T)的方法
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="value"></param>
        /// <returns></returns>
        [SecuritySafeCritical]
        public static T TryParse<T>(this string value, out bool result) where T : IConvertible
        {
            var type = typeof(T);
            Type[] types = { typeof(string), type.MakeByRefType() };
            var Method = type.GetMethod("TryParse", types);
            if (Method == null)
            {
                result = false;
                throw new TryParseException();
            }
            try
            {
                T Convertible = Activator.CreateInstance<T>();
                var objs = new object[] { value, Convertible };
                result = (bool)Method.Invoke(null, objs);
                return (T)objs[1];
            }
            catch
            {
                result = false;
                throw new TryParseException();
            }
        }

        /// <summary>
        /// 得到short整形
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static short ToShort(this string value)
        {
            short temp = 0;
            short.TryParse(value, out temp);
            return temp;
        }

        /// <summary>
        /// 得到int整形
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static int ToInt(this string value)
        {
            var temp = 0;
            int.TryParse(value, out temp);
            return temp;
        }

        /// <summary>
        /// 得到long整形
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static long ToLong(this string value)
        {
            long temp = 0;
            long.TryParse(value, out temp);
            return temp;
        }

        /// <summary>
        /// 得到decimal浮点型
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static decimal ToDecimal(this string value)
        {
            decimal temp = 0;
            decimal.TryParse(value, out temp);
            return temp;
        }

        /// <summary>
        /// 得到DateTime
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static DateTime ToDateTime(this string value)
        {
            DateTime temp = DateTime.Now;
            DateTime.TryParse(value, out temp);
            return temp;
        }

        /// <summary>
        /// 得到Float
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static float ToFloat(this string value)
        {
            float temp = 0;
            float.TryParse(value, out temp);
            return temp;
        }

        /// <summary>
        /// 得到Double
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static double ToDouble(this string value)
        {
            double temp = 0;
            double.TryParse(value, out temp);
            return temp;
        }

        /// <summary>
        /// 得到Byte
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static byte ToByte(this string value)
        {
            byte temp = 0;
            byte.TryParse(value, out temp);
            return temp;
        }

        /// <summary>
        /// 得到Byte
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static char ToChar(this string value)
        {
            char temp = new char();
            char.TryParse(value, out temp);
            return temp;
        }
    }
    public class TryParseException : Exception
    {
    }
}
