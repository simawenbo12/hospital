using System;
using System.Collections.Generic;

namespace DataBase.Models
{
    public partial class HmAd
    {
        public int Id { get; set; }
        public int PositionId { get; set; }
        public string Name { get; set; }
        public string Image { get; set; }
        public string Url { get; set; }
    }
}
