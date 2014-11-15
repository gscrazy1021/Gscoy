using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gscoy.DataModel.Baidu.LBS
{

    public class EnGeocodingEntity
    {
        public string status { get; set; }
        public string description { get; set; }
        public string province { get; set; }
        public string city { get; set; }
        public string district { get; set; }
        public string street { get; set; }
        public string street_number { get; set; }
        public string zipCode { get; set; }
    }

}
