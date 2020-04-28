using System;
using System.Collections.Generic;

namespace DataBase.Models
{
    public partial class HmUserLog
    {
        public int Id { get; set; }
        public string Ip { get; set; }
        public DateTime Time { get; set; }
    }
}
