using System;
using System.Collections.Generic;

namespace DataBase.Models
{
    public partial class HmDoctor
    {
        public int Id { get; set; }
        public int Sex { get; set; }
        public string Job { get; set; }
        public int Edu { get; set; }
        public string Room { get; set; }
        public string OpRoom { get; set; }
        public string OpTime { get; set; }
        public string Description { get; set; }
        public string Name { get; set; }
        public string Image { get; set; }
    }
}
