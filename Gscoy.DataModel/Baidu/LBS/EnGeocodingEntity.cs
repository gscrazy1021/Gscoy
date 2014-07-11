using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gscoy.DataModel.Baidu.LBS
{
    public class EnGeocodingEntity
    {
        /// <summary>
        /// 请求状态，如果成功返回0，如果失败返回其他数字，详细见状态码附录。 
        /// </summary>
        public string status { get; set; }
        /// <summary>
        /// 地点描述 
        /// </summary>
        public string description { get; set; }

        public string city { get; set; }

        public string province { get; set; }
        /// <summary>
        /// 所在街道 
        /// </summary>
        public string street { get; set; }
        /// <summary>
        /// 所在街区的名称
        /// </summary>
        public string district { get; set; }

        public string street_number { get; set; }
        /// <summary>
        ///  	Zip码 
        /// </summary>
        public string zipCode { get; set; }
    }
}
