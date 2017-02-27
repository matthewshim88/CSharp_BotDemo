using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ChipotleNews
{
    public class TrendingNewsInfo
    {
        public string _type { get; set; }
        public string readLink { get; set; }
        public NewsInfo[] value { get; set; }
    }

    public class NewsInfo
    {
        public string name { get; set; }
        public string URL { get; set; }
        public Image img { get; set; }
        public string description { get; set; }
        public string category { get; set; }
    }
    public class Image
    {
        public Thumbnail thumbnail { get; set; }
    }
    public class Thumbnail
    {
        public string contentURL { get; set; }
        public int width { get; set; }
        public int height { get; set; }
    }
}