using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gscoy.DataModel.Baidu.LBS
{

    public class SearchMovieEntity
    {
        public int error { get; set; }
        public string status { get; set; }
        public string date { get; set; }
        public SearchMovieResult[] result { get; set; }
    }

    public class SearchMovieResult
    {
        public string uid { get; set; }
        public string name { get; set; }
        public string telephone { get; set; }
        public SearchMovieLocation location { get; set; }
        public string address { get; set; }
        public string rating { get; set; }
        public SearchMovieTime_Table[] time_table { get; set; }
    }

    public class SearchMovieLocation
    {
        public float lng { get; set; }
        public float lat { get; set; }
    }

    public class SearchMovieTime_Table
    {
        public string time { get; set; }
        public string date { get; set; }
        public string lan { get; set; }
        public string type { get; set; }
        public object price { get; set; }
    }

}
