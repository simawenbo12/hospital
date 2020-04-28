using System;
using System.Collections.Generic;

namespace DataBase.Models
{
    public partial class HmArticle
    {
        public int Id { get; set; }
        public int CateId { get; set; }
        public string Title { get; set; }
        public string Image { get; set; }
        public string Description { get; set; }
        public string Content { get; set; }
        public int Addtime { get; set; }
        public int IsShow { get; set; }
        public int Type { get; set; }
        public int Sort { get; set; }
    }
}
