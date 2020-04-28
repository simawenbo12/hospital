using System;
using System.Collections.Generic;

namespace DataBase.Models
{
    public partial class HmAdmin
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public int Lasttime { get; set; }
        public string Ip { get; set; }
    }
}
