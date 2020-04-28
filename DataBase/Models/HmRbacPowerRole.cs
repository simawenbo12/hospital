using System;
using System.Collections.Generic;

namespace DataBase.Models
{
    public partial class HmRbacPowerRole
    {
        public int Id { get; set; }
        public int PowerId { get; set; }
        public int RoleId { get; set; }
    }
}
