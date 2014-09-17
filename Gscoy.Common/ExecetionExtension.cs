using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gscoy.Common
{
    public static class ExecetionExtension
    {
        public static string ToStringEx(this Exception ex, int maxInnerExceptionDeath = 3)
        {
            StringBuilder bulider = new StringBuilder();
            bulider.AppendFormat("stacktrace:{0}\n", ex.StackTrace);
            bulider.AppendFormat("message:{0}\n", ex.Message);
            int death = 0;
            //循环异常，只到异常没有子异常或到达指定深度为止；
            while (ex.InnerException != null && death <= maxInnerExceptionDeath)
            {
                bulider.AppendFormat("stacktrace:{0}\n", ex.StackTrace);
                bulider.AppendFormat("message:{0}\n", ex.Message);
                death++;
            }
            return bulider.ToString();
        }
    }
}
