using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gscoy.DataModel.Sina
{
    /// <summary>
    /// 解梦
    /// </summary>
    public class DreamEntity
    {
        public string id { get; set; }
        public string title { get; set; }
        public string content { get; set; }
    }
    /// <summary>
    /// 解梦列表
    /// </summary>
    public class DreamListEntity
    {
        public DreamEntity[] result { get; set; }
    }
}
