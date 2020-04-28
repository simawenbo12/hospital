using System;
using System.Collections.Generic;

namespace DataBase.Models
{
    public partial class HmRbacPower
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int ParentId { get; set; }
        public string Icon { get; set; }
        public string Controller { get; set; }
        public string Action { get; set; }
        public int Sort { get; set; }
    }
}
