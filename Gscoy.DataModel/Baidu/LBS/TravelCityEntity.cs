using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gscoy.DataModel.Baidu.LBS
{

    public class TravelCityEntity
    {
        public int error { get; set; }
        public string status { get; set; }
        public string date { get; set; }
        public TravelCityResult result { get; set; }
    }

    public class TravelCityResult
    {
        public int cityid { get; set; }
        public string cityname { get; set; }
        public TravelCityLocation location { get; set; }
        public string star { get; set; }
        public string url { get; set; }
        public string _abstract { get; set; }
        public string description { get; set; }
        public TravelCityItinerary[] itineraries { get; set; }
    }

    public class TravelCityLocation
    {
        public float lng { get; set; }
        public float lat { get; set; }
    }

    public class TravelCityItinerary
    {
        public string name { get; set; }
        public string description { get; set; }
        public TravelCityItinerary1[] itineraries { get; set; }
    }

    public class TravelCityItinerary1
    {
        public TravelCityPath[] path { get; set; }
        public string description { get; set; }
        public string dinning { get; set; }
        public string accommodation { get; set; }
    }

    public class TravelCityPath
    {
        public string name { get; set; }
        public string detail { get; set; }
    }

}
