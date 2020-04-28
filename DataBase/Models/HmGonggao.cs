using System;
using System.Collections.Generic;

namespace DataBase.Models
{
    public partial class HmGonggao
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Content { get; set; }
        public int Addtime { get; set; }
        public int IsShow { get; set; }
        public int Type { get; set; }
        public string Tougao { get; set; }
        public string Shenpi { get; set; }
        public int Dianji { get; set; }
    }
}
