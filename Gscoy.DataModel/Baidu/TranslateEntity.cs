

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gscoy.DataModel.Baidu
{
    public class TranslateEntity
    {
        public string from { get; set; }
        public string to { get; set; }
        public Trans_Result[] trans_result { get; set; }
    }

    public class Trans_Result
    {
        public string src { get; set; }
        public string dst { get; set; }
    }
}
