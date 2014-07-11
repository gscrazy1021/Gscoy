using System;
using System.Text.RegularExpressions;

namespace Gscoy.Common
{
    /// <summary>
    /// 数值转化为人民币大写
    /// </summary>
    public class ConvertJine
    {
        /// <summary>
        /// 转化为人民币大写
        /// </summary>
        /// <param name="money"></param>
        /// <returns></returns>
        public static string GetRMBConvert(string money)
        {
            var m = money.ToDecimal();
            if (m == 0)
            {
                return string.Format("0");
            }
            else
            {
                return GetRMB(m);
            }
        }

        private static string GetRMB(decimal myMoney)
        {
            string SHUZI; //保存數字小寫
            string ZIFU; //保存數字轉換后的結果
            int tmp1; //臨時變量

            string[] shu = new String[14];
            string[] SHU1 = new String[10];

            shu[13] = "仟";
            shu[12] = "佰"; shu[11] = "拾"; shu[10] = "億";
            shu[9] = "仟"; shu[8] = "佰"; shu[7] = "拾";
            shu[6] = "萬"; shu[5] = "仟"; shu[4] = "佰";
            shu[3] = "拾"; shu[2] = "元"; shu[1] = "角";
            shu[0] = "分";

            SHU1[0] = "零"; SHU1[1] = "壹"; SHU1[2] = "貳";
            SHU1[3] = "叁"; SHU1[4] = "肆"; SHU1[5] = "伍";
            SHU1[6] = "陆"; SHU1[7] = "柒"; SHU1[8] = "捌";
            SHU1[9] = "玖";

            //最大值為9999,9999,9999.99

            if (myMoney > Convert.ToDecimal(999999999999.99))//過大時返回原來值
            {
                return Convert.ToString(myMoney) + "元整";
            }
            else
            {
                SHUZI = Convert.ToString(myMoney * 100);//先將數值轉化

                if (SHUZI.Substring(0, 1) == "-")//為負數時去掉負號
                {
                    SHUZI = SHUZI.Substring(1);
                }

                if (SHUZI.IndexOf(".") > 0)//當小數位過兩位時，去掉小數位
                {
                    SHUZI = SHUZI.Substring(0, SHUZI.IndexOf("."));
                }

                ZIFU = "";
                tmp1 = 0; //從最首位開始轉化
                while (tmp1 < SHUZI.Length)
                {
                    if (SHUZI.Substring(tmp1, 1) != "0")//當數字位不為零時，得到當前的　漢數　和　幣字
                    {
                        ZIFU = ZIFU + SHU1[Convert.ToInt32(SHUZI.Substring(tmp1, 1))] + shu[SHUZI.Length - tmp1 - 1];
                    }

                    else//當數字位不為零時
                    {
                        if (tmp1 == SHUZI.Length - 3)//最未位的單位為　元
                        {
                            ZIFU = ZIFU + shu[2];
                        }

                        if (tmp1 == SHUZI.Length - 6) //以　萬　記
                        {
                            ZIFU = ZIFU + shu[6];
                        }
                        if (tmp1 == SHUZI.Length - 10)
                        {
                            ZIFU = ZIFU + shu[10];
                        }
                    }
                    tmp1 = tmp1 + 1;
                }
            }
            return ZIFU + "整";
        }

    }
}

