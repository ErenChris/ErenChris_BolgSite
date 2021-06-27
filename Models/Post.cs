using System;

namespace BolgSite.Models
{
    public class Post
    {
        public string Title { get; set; } = "";
        public string Body { get; set; } = "";

        public DateTime CrateTime { get; set; } = DateTime.Now;
    }
}
