using System;

namespace BolgSite.Models
{
    public class Post
    {
        public int Id { get; set; }

        public string Title { get; set; } = "";
        public string Body { get; set; } = "";
        public string ImagePath { get; set; } = "";

        public DateTime CrateTime { get; set; } = DateTime.Now;
    }
}